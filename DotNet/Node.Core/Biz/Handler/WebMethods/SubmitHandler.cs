using System;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.Submit;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

using DataFlow.Component.Interface;
using System.Data;
using Node.Lib.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Node.Core.Biz.Handler.WebMethods
{
    /// <summary>
    /// SubmitHandler is core class for Submit Web Service
    /// </summary>
    public class SubmitHandler : BaseHandler
    {
        private string SupplTransID = null;
        private string DataFlow = null;
        private Node.Core.Document.NodeDocument[] Documents = null;
        protected Operation SubmitOp = null;
        private List<string> _FileStored = null;
        //private string sRecipient = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestorIP">IP address of requestor</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used.</param>
        /// <param name="docs">Array of files being submitted</param>
        public SubmitHandler(string requestorIP, string hostName, string token, string transID, string dataFlow, Node.Core.Document.NodeDocument[] docs)
            : base(requestorIP, hostName)
        {
            this.Token = token;
            this.SupplTransID = transID;
            this.DataFlow = dataFlow;
            this.Documents = docs;
            string opName = dataFlow;
            if (dataFlow == null || dataFlow.Trim().Equals(""))
                opName = (NodeVersion == NodeVer.VER_11) ? "NODE" : "NODE2";
            this.SubmitOp = new Operation(opName, Phrase.WEB_SERVICE_SUBMIT);
            
            if ((this.SubmitOp == null || this.SubmitOp.ID < 0))
                throw new Exception(Phrase.E_INVALID_DATA_FLOW);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestorIP">IP address of requestor</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used. </param>
        /// <param name="operationName">A flow specific operation identifier.</param>
        /// <param name="docs">Array of files being submitted</param>
        public SubmitHandler(string requestorIP, string hostName, string token, string transID, string dataFlow,string operationName, Node.Core.Document.NodeDocument[] docs)
            : base(requestorIP, hostName)
        {
            this.Token = token;
            this.SupplTransID = transID;
            this.DataFlow = dataFlow;
            this.Documents = docs;
            string opName = operationName;

            if (this.checkOpName(""+operationName))
            {
                opName = operationName;
            }
            else if (this.checkOpName(""+this.DataFlow))
            {
                opName = dataFlow;
            }

            if (string.IsNullOrEmpty(this.DataFlow) && string.IsNullOrEmpty(opName))
            {
                opName = (NodeVersion == NodeVer.VER_11) ? "NODE" : "NODE2";
            }

            this.SubmitOp = new Operation(opName, Phrase.WEB_SERVICE_SUBMIT);

            if ((this.SubmitOp == null || this.SubmitOp.ID < 0))
                throw new Exception(Phrase.E_INVALID_DATA_FLOW);

        }

        private bool checkOpName(string opName)
        {
            DBManager db = new DBManager();
            Operation op = db.GetOperationsDB().GetOperation(opName);
            if (op == null || op.ID < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Initialize process of SubmitHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.SubmitOp != null && this.SubmitOp.ID >= 0)
            {
                if (this.SubmitOp.DomainStatus != null && this.SubmitOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.SubmitOp.Status != null && this.SubmitOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        int count = this.Documents == null ? 0 : this.Documents.Length;

                        List<string> names = new List<string>();
                        List<object> values = new List<object>();
                        if (count == 0)
                        {
                            names.Add("DataFlow");
                            names.Add("Documents");
                            values.Add(this.DataFlow);
                            values.Add("No Documents Submitted");
                        }
                        else
                        {
                            names.Add("DataFlow");
                            values.Add(this.DataFlow);
                            int i = 0;
                            foreach(Node.Core.Document.NodeDocument doc in this.Documents)
                            {
                                if (doc != null && doc.name != null)
                                {
                                    names.Add(doc.name);
                                }
                                else
                                {
                                    names.Add("Document" + i);
                                    i++;
                                }

                                if (doc != null && doc.Stream != null && doc.Stream.Length > 0)
                                    values.Add("Document Size: " + doc.Stream.Length + " bytes");
                                else
                                    values.Add("Document Size: 0 bytes");
                            }
                            Hashtable ht = this.ParsingParameterforSubmit();
                            if (ht != null)
                            {
                                IDictionaryEnumerator idc = ht.GetEnumerator();
                                idc.Reset();
                                while (idc.MoveNext())
                                {
                                    names.Add(idc.Entry.Key.ToString());
                                    values.Add(idc.Entry.Value);
                                }
                            }

                        }

                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.SubmitOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, this.SupplTransID,
                            this.Token, null, null, null, this.HostName, names.ToArray(), values.ToArray());
                    }
                    else
                        throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }
        /// <summary>
        /// Authorize process of SubmitHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_SUBMIT, this.SubmitOp.Name);
                if (user != null)
                {
                    ILogging logDB = new DBManager().GetLoggingDB();
                    logDB.UpdateOperationLogUserName(this.TransID, user);
                }
                else
                    throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);
            }
            catch (SoapException e)
            {
                if (e.Message == "Invalid security token")
                {
                    throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);
                }
                else
                {
                    throw e;
                }                             
            }
            catch (Exception)
            {
                ILogging logDB = new DBManager().GetLoggingDB();
                logDB.CopyUserFromToken(this.Token, this.TransID);
            }
            return user;
        }
        /// <summary>
        /// Excute DataFlow process of SubmitHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object  ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.DataFlow);
            process.CreateActionParameter(WebServiceParameter.documents.ToString(), this.Documents);

            process.Execute(dataflowConfig);
            return base.TransID;
        }
        /// <summary>
        /// Excute Plug-in process of SubmitHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            if (this.SupplTransID == null || this.SupplTransID.Trim() == String.Empty)
                this.SupplTransID = base.TransID;
            return this.ExecuteOperation(this.SubmitOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetSubmitPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.SupplTransID, this.DataFlow, this.Documents, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetSubmitProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.SupplTransID, this.SubmitOp.Name, this.Documents, param);
            throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }
        /// <summary>
        /// Excute PostProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            IPostProcess process = new DllManager().GetSubmitPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.SupplTransID, this.DataFlow, this.Documents, param);
        }

        private Hashtable ParsingParameterforSubmit()
        {
            Hashtable ht = new Hashtable();
            ManageOperation mgrOp = new ManageOperation();
            DataTable dt = mgrOp.GetParametersByID(SubmitOp.ID + "");
            try
            {
                if (dt != null)
                {
                    _FileStored = new List<string>();

                    UnzippedFiles(this.Documents[0].name, this.Documents[0].type, this.Documents[0].content);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(_FileStored[0]);
                    foreach (DataRow aRow in dt.Rows)
                    {
                        if (!aRow["id"].ToString().Equals("")
                            && !aRow["ParameterName"].ToString().Equals("")
                            && !aRow["XPath"].ToString().Equals(""))
                        {
                            XmlNode aNode = doc.SelectSingleNode(aRow["XPath"].ToString());
                            if (aNode != null)
                            {
                                ht.Add(aRow["ParameterName"].ToString(), aNode.InnerText);
                            }
                        }
                    }
                }
                else
                {
                    ht = null;
                }
            }
            catch (Exception)
            {
                ht = null;
            }
            return ht;
        }
        private void UnzippedFiles(string fileName, string fileType, byte[] content)
        {
            if (fileType.ToUpper() == "ZIP")
            {
                WinZip wz = new WinZip();
                Hashtable resulthash = wz.ExtractZip(content);
                if (resulthash != null && resulthash.Keys.Count > 0)
                {
                    foreach (string key in resulthash.Keys)
                    {
                        UnzippedFiles(key, GetFileType(key), (byte[])resulthash[key]);
                    }
                }
            }
            else
            {
                _FileStored.Add(new UTF8Encoding().GetString(content));
            }
        }
        private string GetFileType(string filename)
        {
            string ext = filename.Substring(filename.LastIndexOf(".") + 1);
            if (ext.Length == filename.Length)
                return "";

            ext = ext.ToUpper();
            //if (ext == "TXT")
            //    return "FLAT";
            //else
            return ext;
        }

    }
}
