using System;
using System.Collections;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Xml;

using Node.Lib.Security;
using Node.Core.API;
using Node.Core.Biz.Interfaces.Solicit;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Logging;

namespace Node.Core.Biz.Handler.WebMethods
{
    /// <summary>
    /// SolicitProcess is threaded process for Solicit Web Service
    /// </summary>
    public class SolicitProcess
    {
        private Operation SolicitOp = null;
        private int OpLogID = -1;
        private string TransID = null;
        private string RequestorIP = null;
        private string UserID = null;
        private string Token = null;
        private string ReturnURL = null;
        private string Request = null;
        private string[] Parameters = null;
        private Logger AppLog = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opName">Name of Operation</param>
        /// <param name="opLogID">Database Key of Node Operation Log</param>
        /// <param name="transID">Unique ID generation by each of requested operation.</param>
        /// <param name="requestorIP">IP Address of Requester</param>
        /// <param name="user">The user ID of the person or system. It is recommended that an email address be used as the userID in the Exchange NetWork.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="returnURL">It can be recipient email address or NofificationURI.</param>
        /// <param name="request">The name of Plug-In Solicit Oparation to be processed.</param>
        /// <param name="parameters">Array of Parameter for the solicit request</param>
        public SolicitProcess(string opName, int opLogID, string transID, string requestorIP, string user, string token, string returnURL, string request, string[] parameters)
        {
            this.SolicitOp = new Operation(opName, Phrase.WEB_SERVICE_SOLICIT);
            this.OpLogID = opLogID;
            this.TransID = transID;
            this.RequestorIP = requestorIP;
            this.UserID = user;
            this.Token = token;
            this.ReturnURL = returnURL;
            this.Request = request;
            this.Parameters = parameters;
            try
            {
                SystemConfiguration config = new SystemConfiguration();
                this.AppLog = new Logger(ConfigurationManager.AppSettings[Phrase.SOLICIT_LOG_PATH_KEY], config.GetLoggingLevel("WebServices"));
            }
            catch (Exception) { }
        }
        /// <summary>
        /// The entry point of SolicitProcess.
        /// </summary>
        /// <returns></returns>
        public object Execute()
        {
            object retObj = null;
            try
            {
                this.Initialize();

                retObj = this.ExecuteProcess();

                ILogging logDB = new DBManager().GetLoggingDB();
                if (!logDB.HasEndDate(this.TransID))
                    this.Log(Phrase.STATUS_COMPLETED, Phrase.MESSAGE_COMPLETED, true);
            }
            catch (SoapException e)
            {
                this.Log("Failed", e.ToString() + "\n" + e.StackTrace, true);
            }
            catch (Exception e)
            {
                this.Log("Failed", "Exception Thrown\n" + e.ToString() + "\n" + e.StackTrace, true);
                this.AppLog.Log(e);
            }
            return retObj;
        }
        /// <summary>
        /// Initialize process of AuthenticateHandler.
        /// </summary>
        private void Initialize()
        {
            // Delete Task
            IConfigurations configDB = new DBManager().GetConfigurationsDB();
            XmlDocument taskConfig = configDB.GetTaskConfig();
            XmlNode remove = taskConfig.SelectSingleNode("/Tasks/Task[TaskName/text() = 'Solicit " + this.TransID + "']");
            if (remove != null)
            {
                XmlElement root = taskConfig.DocumentElement;
                root.RemoveChild(remove);
                configDB.UpdateTaskConfig(taskConfig);
            }

            if (this.SolicitOp != null && this.SolicitOp.ID >= 0)
            {
                this.Log("Processing", "Processing Solicit " + this.SolicitOp.Name, false);
            }
            else
                throw new Exception("Unable to Refresh Solicit " + this.SolicitOp.Name + " from Database");
        }
        private object ExecuteProcess()
        {
            Node.Core.Document.NodeDocument[] result = null;

            // Process
            XmlNode procNode = this.SolicitOp.Config.SelectSingleNode("/Operation/Process");
            ProcParam procParam = new ProcParam(this.TransID, this.RequestorIP, this.UserID, this.OpLogID, new Hashtable(), this.SolicitOp.ID);
            if (procNode != null)
            {
                XmlNode classNode = procNode.SelectSingleNode("ClassName");
                XmlNode dllNode = procNode.SelectSingleNode("DllName");
                IProcess process = new DllManager().GetSolicitProcess(dllNode.InnerText, classNode.InnerText);
                if (process != null)
                    result = process.Execute(this.Token, this.ReturnURL == null || (!this.ReturnURL.Trim().Equals("") && !this.ReturnURL.Equals("null")) ? this.ReturnURL : null, this.Request, this.Parameters, procParam);
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);



            if (result != null && result.Length > 0)
            {
                // Save in Database
                DocManager docManager = new DocManager();
                foreach (Node.Core.Document.NodeDocument doc in result)
                    docManager.UploadDocuments(doc, this.TransID, this.Request, "Uploaded", DateTime.Now, this.UserID);

                // Submit back
                if (this.ReturnURL != null && !this.ReturnURL.Trim().Equals("") && !this.ReturnURL.Trim().Equals("null"))
                {
                    XmlNode solicitSubmitNode = this.SolicitOp.Config.SelectSingleNode("/Operation/Process/Solicit/SubmitCredentials");
                    if (solicitSubmitNode != null)
                    {
                        XmlNode solicitUser = solicitSubmitNode.SelectSingleNode("UserID");
                        XmlNode solicitPWD = solicitSubmitNode.SelectSingleNode("Password");
                        if (solicitUser != null && !solicitUser.InnerText.Trim().Equals("") &&
                            solicitPWD != null && !solicitPWD.InnerText.Trim().Equals(""))
                        {
                            NodeRequestor requestor = new NodeRequestor(this.ReturnURL);
                            string token = null;
                            try
                            {
                                token = requestor.Authenticate(solicitUser.InnerText, new Cryptography().Decrypting(solicitPWD.InnerText, Phrase.CryptKey), "PASSWORD");
                            }
                            catch (Exception e)
                            {
                                this.Log("Authenticate Failed", "The Authenticate to " + this.ReturnURL + " Failed: " + e.ToString() + "\n" + e.StackTrace, false);
                            }
                            XmlNode solicitDataFlowNode = solicitSubmitNode.SelectSingleNode("DataFlowName");
                            string dataFlow = this.Request;
                            if (solicitDataFlowNode != null)
                                dataFlow = solicitDataFlowNode.InnerText;
                            try
                            {
                                string transID = requestor.Submit(token, this.TransID, dataFlow, result);

                                if (transID != null && !transID.Trim().Equals(""))
                                    this.Log("Submitted", result.Length + " Document(s) were Submitted to " + this.ReturnURL + " with returned Transaction ID: " + transID, false);
                                else
                                    this.Log("Submit Failed", "The Transaction ID for " + result.Length + " Document(s) was null or empty for the Submit to " + this.ReturnURL, false);
                            }
                            catch (Exception e)
                            {
                                this.Log("Submit Failed", "The Submit of " + result.Length + " Document(s) to " + this.ReturnURL + " Failed: " + e.ToString() + "\n" + e.StackTrace, false);
                            }
                        }
                    }
                }
            }

            XmlNode postNode = this.SolicitOp.Config.SelectSingleNode("/Operation/PostProcess");
            PostParam postParam = new PostParam(this.TransID, this.RequestorIP, this.UserID, this.OpLogID, result, procParam.ValueTable);
            if (postNode != null)
            {
                Hashtable table = new Hashtable();
                XmlNodeList list = postNode.SelectNodes("Sequence");
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        XmlNode classNode = list.Item(i).SelectSingleNode("ClassName");
                        XmlNode dllNode = list.Item(i).SelectSingleNode("DllName");
                        XmlAttribute attr = list.Item(i).Attributes["number"];
                        table.Add(attr.Value, dllNode.InnerText + " " + classNode.InnerText);
                    }
                }
                string temp = null;
                for (int i = 1; (temp = (string)table["" + i]) != null; i++)
                {
                    string[] split = temp.Split(new char[] { ' ' });
                    IPostProcess process = new DllManager().GetSolicitPostProcess(split[0], split[1]);
                    process.Execute(this.Token, this.ReturnURL, this.Request, this.Parameters, postParam);
                }
            }
            return result;
        }
        private void Log(string status, string message, bool lastUpdate)
        {
            new Node.Core.API.Logging().UpdateOperationLog(this.OpLogID, status, message, this.UserID, lastUpdate);
        }
    }
}
