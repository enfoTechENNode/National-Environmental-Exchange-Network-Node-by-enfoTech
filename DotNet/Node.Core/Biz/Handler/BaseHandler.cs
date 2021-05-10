using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml;
using System.Data;

//using Node.Core.API;
using Node.Core.Biz;
using Node.Core.Biz.Interfaces.Authenticate;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.NAAS;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core;
using Node.Core.Logging;
using Node.Core.Util;

using DataFlow.Component.Interface;

namespace Node.Core.Biz.Handler
{
    /// <summary>
    /// The base class of all webmethod handler.
    /// </summary>
    public abstract class BaseHandler
    {   
        /// <summary>
        /// The enum of NodeVer.
        /// </summary>
        public enum NodeVer { 
            /// <summary>
            /// VER_11
            /// </summary>
            VER_11,
            /// <summary>
            /// VER_20
            /// </summary>
            VER_20 };
        /// <summary>
        /// The static variable NodeVer.
        /// </summary>
        public static NodeVer NodeVersion = NodeVer.VER_11;
        /// <summary>
        /// Transaction ID of the operation.
        /// </summary>
        protected string TransID = null;
        /// <summary>
        /// Requestor IP of the operation.
        /// </summary>
        protected string RequestorIP = null;
        /// <summary>
        /// Host name of the operation.
        /// </summary>
        protected string HostName = null;
        /// <summary>
        /// Security token of the operation.
        /// </summary>
        protected string Token = null;
        /// <summary>
        /// NAAS user account of the operation.
        /// </summary>
        protected string UserName = null;
        /// <summary>
        /// Operation log id of the operation.
        /// </summary>
        protected int OpLogID = -1;
        /// <summary>
        /// Application log for the operation.
        /// </summary>
        protected Logger AppLog = null;
        /// <summary>
        /// Exception log for the operation.
        /// </summary>
        protected Logger ExceptionLog = null;
        /// <summary>
        /// XML file for the operation.
        /// </summary>
        private XmlDocument Config = null;
        /// <summary>
        /// Result of Opeation.
        /// </summary>
        private object Result = null;
        /// <summary>
        /// Temporary storage for the operation parameters.
        /// </summary>
        private Hashtable Table = null;
        /// <summary>
        /// Constructor of BaseHandler.
        /// </summary>
        /// <param name="requestorIP">The endpoint of ip address.</param>
        /// <param name="hostName">The host name.</param>
        public BaseHandler(string requestorIP, string hostName)
        {
            this.RequestorIP = requestorIP;
            this.HostName = hostName;
            this.AppLog = new Logger();       
        }
        /// <summary>
        /// The entry point of operation.
        /// </summary>
        /// <returns>result of operation.</returns>
        public object Invoke()
        {
            object retObj = null;
            try
            {
                // Check Status of Node
                string status = new DBManager().GetConfigurationsDB().GetNodeStatus();
                if (status == null || !status.Equals(Phrase.STATUS_RUNNING))
                {
                    this.AppLog.Log(Phrase.E_SERVICE_UNAVAILABLE, "The Node is not running", Logger.LEVEL_INFO);
                    this.ThrowSoapException(Phrase.E_SERVICE_UNAVAILABLE, Phrase.E_SERVICE_UNAVAILABLE, "Unable to Authenticate User");
                    //throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }

                // Generate Transaction ID
                NodeUtility utility = new NodeUtility();
                this.TransID = utility.GenerateTransactionID();

                // Initialize Handler
                this.Initialize();

                // Authorize
                this.UserName = this.Authorize();
                if (this.UserName != null)
                    retObj = this.Execute();
                else
                    this.ThrowSoapException(Phrase.E_INVALID_TOKEN, Phrase.E_INVALID_TOKEN, "Invalid Token");
                    //throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);

                string result = null;
                if (retObj != null)
                {
                    if (retObj.GetType() == typeof(NodeDocument[]))
                        result = "Returned " + ((NodeDocument[])retObj).Length + " documents";
                    else if (retObj.GetType() == typeof(string[]))
                    {
                        result = "";
                        string[] array = (string[])retObj;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (i != 0) result += ", ";
                            result += array[i];
                        }
                    }
                    else
                        result = "" + retObj;
                }
                this.AppLog.Log("Returning Value", result, Logger.LEVEL_DEBUG);
            }
            catch (SoapException e)
            {
                Node.Core.API.Logging logger = new Node.Core.API.Logging();
                logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_FAILED, Phrase.MESSAGE_FAILED + e.Message, this.UserName, true);
                this.AppLog.Log("Operation Error", e.Message, Logger.LEVEL_WARN);
                this.ThrowSoapException(e.Message, e.Message, "Unable to Authenticate User");
            }
            catch (Exception e)
            {
                // Log Failed Status
                Node.Core.API.Logging logger = new Node.Core.API.Logging();
                logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_FAILED, Phrase.MESSAGE_FAILED + e.ToString(), this.UserName, true);
                this.AppLog.Log("Operation Error", e.ToString(), Logger.LEVEL_WARN);
                if (e.Message == Phrase.E_AUTH_METHOD)
                {
                    //throw new SoapException(Phrase.E_AUTH_METHOD, SoapException.ClientFaultCode);
                    this.ThrowSoapException(e.Message, Phrase.E_AUTH_METHOD, "Unable to Authenticate User");
                }
                else if (e.Message == Phrase.E_UNKNOWN_USER)
                {
                    //throw new SoapException(Phrase.E_UNKNOWN_USER, SoapException.ClientFaultCode);
                    this.ThrowSoapException(e.Message, Phrase.E_UNKNOWN_USER, "Unable to Authenticate User");
                }
                else if (e.Message == Phrase.E_TRANSACTION_NOT_FOUND)
                {
                    this.ThrowSoapException(e.Message, Phrase.E_TRANSACTION_NOT_FOUND, "Unable to find the Transation");
                }
                else
                {
                    //throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode);
                    this.ThrowSoapException(e.Message, Phrase.E_UNKNOWN, "System Failure");
                }
                
            }
            return retObj;
        }

        /// <summary>
        /// Initialize Handler According to the Web Method
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Does Custom Authorization
        /// For Instance, NodePing or Authenticate May Just Return ""
        /// Other Web Methods May Want to Authorize Security Token
        /// </summary>
        /// <returns>User Name of Node User who Owns the Security Token</returns>
        protected abstract string Authorize();

        /// <summary>
        /// Does Custom Execution
        /// Solicit Handles the Execution of the Operation Differently than Other Web Methods
        /// </summary>
        /// <returns>Returned Result</returns>
        protected abstract object Execute();

        /// <summary>
        /// Does Custom Execution by using data flow wizard.
        /// </summary>
        /// <param name="dataflowConfig">The config of data flow.</param>
        /// <returns>Returned Result.</returns>
        protected abstract object ExecuteDataflow(string dataflowConfig);
        /// <summary>
        /// The abstract function of PreProcess.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">operation parameters.</param>
        protected abstract void ExecutePreProcess(string dllName, string className, PreParam param);
        /// <summary>
        /// The abstract function of Process.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">operation parameters.</param>
        protected abstract object ExecuteProcess(string dllName, string className, ProcParam param);
        /// <summary>
        /// The abstract function of PostProcess.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">operation parameters.</param>
        protected abstract void ExecutePostProcess(string dllName, string className, PostParam param);
        /// <summary>
        /// The authozie process of BaseHander.
        /// </summary>
        /// <param name="webMethod">The name web method.</param>
        /// <param name="request">The name of request.</param>
        /// <returns></returns>
        protected string Authorize(string webMethod, string request)
        {
            string userName = null;
            if (this.Token.StartsWith("ndlc:"))
            {
                IUsers userDB = new DBManager().GetUsersDB();
                userName = userDB.LocalAuthorize(this.Token, request, webMethod);
            }
            else if (this.Token == "PublicUsers")
            {
                userName = "PublicUsers";
            }
            else
            {
                Authenticator auth = new Authenticator();
                userName = auth.Authorize(this.Token, this.RequestorIP, webMethod, request);
            }
            return userName;
        }
        /// <summary>
        /// The method retrieve Operation XML file from NODE_OPERATION.
        /// Parsing Operation XML to get PreProcess,Process,PostProcess PlugIn dll.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        protected object ExecuteOperation(Operation op)
        {
            if (op == null)
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);

            this.Config = op.Config;
            if (this.Config.DocumentElement.Name == "process")
            {
                object outputobject = this.ExecuteDataflow(this.Config.OuterXml);
                if (outputobject is IActionParameter)
                {
                    IActionParameter output = (IActionParameter)outputobject;
                    this.Result = (output == null) ? null : output.ParameterValue;
                }
                else
                {
                    this.Result = outputobject;
                }
            }
            else
            {
                // Pre Processes
                XmlNode preNode = this.Config.SelectSingleNode("/Operation/PreProcess");
                PreParam preParam = new PreParam(this.TransID, this.RequestorIP, this.UserName, this.OpLogID);
                if (preNode != null)
                {
                    Hashtable table = new Hashtable();
                    XmlNodeList list = preNode.SelectNodes("Sequence");
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
                        this.ExecutePreProcess(split[0], split[1], preParam);
                    }
                }

                // Process
                XmlNode procNode = this.Config.SelectSingleNode("/Operation/Process");
                ProcParam procParam = new ProcParam(this.TransID, this.RequestorIP, this.UserName, this.OpLogID, preParam.ValueTable,op.ID);
                if (procNode != null)
                {
                    XmlNode classNode = procNode.SelectSingleNode("ClassName");
                    XmlNode dllNode = procNode.SelectSingleNode("DllName");
                    this.Result = this.ExecuteProcess(dllNode.InnerText, classNode.InnerText, procParam);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);

                // Post Processes (New Thread)
                XmlNode postNode = this.Config.SelectSingleNode("/Operation/PostProcess");
                if (postNode != null)
                {
                    this.Table = procParam.ValueTable;
                    Thread thread = new Thread(new ThreadStart(this.PostProcess));
                    thread.Start();

                    return this.Result;
                }
            }
            ILogging logDB = new DBManager().GetLoggingDB();
            if (!logDB.HasEndDate(this.TransID))
            {
                string result = null;
                if (this.Result != null)
                {
                    if (this.Result.GetType() == typeof(NodeDocument[]))
                        result = "Returned " + ((NodeDocument[])this.Result).Length + " documents";
                    else if (this.Result.GetType() == typeof(string[]))
                    {
                        result = "";
                        string[] array = (string[])this.Result;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (i != 0) result += ", ";
                            result += array[i];
                        }
                    }
                }
                if (result == null)
                    result = "" + this.Result;

                Node.Core.API.Logging logger = new Node.Core.API.Logging();
                if (op.WebServiceName != Phrase.WEB_SERVICE_QUERY)
                {
                    logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_COMPLETED, "Result: " + result, this.UserName, true);
                }
                else
                {
                    logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_COMPLETED, "Result: "+ result.Substring(0,15) +"...." , this.UserName, true);
                }
            }
            return this.Result;
        }
        /// <summary>
        /// The method executs standard PostProcess logic.
        /// </summary>
        public void PostProcess()
        {
            try
            {
                XmlNode postNode = this.Config.SelectSingleNode("/Operation/PostProcess");
                PostParam postParam = new PostParam(this.TransID, this.RequestorIP, this.UserName, this.OpLogID, this.Result, this.Table);
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
                        this.ExecutePostProcess(split[0], split[1], postParam);
                    }
                }
                ILogging logDB = new DBManager().GetLoggingDB();
                if (!logDB.HasEndDate(this.TransID))
                {
                    string result = null;
                    if (this.Result != null)
                    {
                        if (this.Result.GetType() == typeof(NodeDocument[]))
                            result = "Returned " + ((NodeDocument[])this.Result).Length + " documents";
                        else if (this.Result.GetType() == typeof(string[]))
                        {
                            result = "";
                            string[] array = (string[])this.Result;
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (i != 0) result += ", ";
                                result += array[i];
                            }
                        }
                    }
                    if (result == null)
                        result = "" + this.Result;

                    Node.Core.API.Logging logger = new Node.Core.API.Logging();
                    logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_COMPLETED, Phrase.MESSAGE_COMPLETED, this.UserName, true);
                }
            }
            catch (Exception e)
            {
                // Log Failed Status
                Node.Core.API.Logging logger = new Node.Core.API.Logging();
                logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_FAILED, Phrase.MESSAGE_FAILED + e.ToString(), this.UserName, true);
                this.AppLog.Log(e);
            }
        }
        /// <summary>
        /// Check if recipient is email address.
        /// </summary>
        /// <param name="recipient">The input string</param>
        /// <returns>True if recipient is email address</returns>
        public bool IsEmailAddress(string recipient)
        {
            if (recipient == null || recipient.Trim() == String.Empty)
                return false;

            if (!recipient.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) &&
                !recipient.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) && (recipient.IndexOf("@") != -1))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Check if recipient is url.
        /// </summary>
        /// <param name="recipient">The input string</param>
        /// <returns>True if recipeint is url string</returns>
        public bool IsNodeURI(string recipient)
        {
            if (recipient == null || recipient.Trim() == String.Empty)
                return false;

            if (recipient.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) ||
                recipient.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Gets Operation status.
        /// </summary>
        /// <param name="transID">Transation ID</param>
        /// <param name="status">The type of status</param>
        /// <returns>A list of status under the opeation.</returns>
        public ArrayList GetOperationStatus(string transID, string status)
        {
            IOperationLogs logs = new DBManager().GetOperationLogsDB();
            return logs.GetOperationStatus(transID, status);
        }
        /// <summary>
        /// Get ActionProcess. (DataWizard)
        /// </summary>
        /// <returns>IActionProcess</returns>
        public IActionProcess GetActionProcess()
        {
            IActionProcess process = new DllManager().GetActionProcess();
            
            SystemConfiguration sys = new SystemConfiguration();
            if (NodeVersion == NodeVer.VER_20)
            {
                process.NodeURI = sys.GetNodeAddress_V2();
            }
            else
            {
                process.NodeURI = sys.GetNodeAddress();
            }
            IActionOperationLog opLog = process.ActionOperationLog;
            opLog.OperationLogID = this.OpLogID + "";
            opLog.TransactionID = this.TransID;
            opLog.UserName = this.UserName;
            opLog.RequestedIP = this.RequestorIP;
            opLog.SecureToken = this.Token;

            return process;
        }

        private void ThrowSoapException(string message, string errorcode, string description)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element,"soap","Detail","http://www.w3.org/2003/05/soap-envelope");
           
            string sNameSpace = "";
            if (NodeVersion == NodeVer.VER_20)
            {
                sNameSpace = "http://www.exchangenetwork.net/schema/node/2";
            }
            else
            {
                sNameSpace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd";
            }

            System.Xml.XmlNode details = doc.CreateNode(XmlNodeType.Element, "NodeFaultDetailType", sNameSpace);
            System.Xml.XmlNode errorCode = doc.CreateNode(XmlNodeType.Element, "errorCode", sNameSpace);
            errorCode.InnerText = errorcode;
            details.AppendChild(errorCode);
            System.Xml.XmlNode desc = doc.CreateNode(XmlNodeType.Element, "description", sNameSpace);
            desc.InnerText = description;
            details.AppendChild(desc);

            // Append the two child elements to the detail node.
            node.AppendChild(details);

            //Throw the exception.    
            SoapException e = new SoapException("Fault occured while processing", SoapException.ClientFaultCode,this.RequestorIP,node);
            //string s = e.Detail.OuterXml;
            throw e;
        }

    }
}
