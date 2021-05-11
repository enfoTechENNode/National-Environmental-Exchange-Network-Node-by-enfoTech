using System;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.Download;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

using DataFlow.Component.Interface;

namespace Node.Core.Biz.Handler.WebMethods
{
    /// <summary>
    /// DownloadHandler is core class for Download Web Service.
    /// </summary>
    public class DownloadHandler : BaseHandler
    {
        private string SupplTransID = null;
        private string DataFlow = null;
        private Node.Core.Document.NodeDocument[] Documents = null;
        private Operation DownloadOp = null;
        /// <summary>
        /// This method is constructor of  DownloadHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used. </param>
        /// <param name="docs">The reserved container for the download requesed documents.</param>
        public DownloadHandler(string requestorIP, string hostName, string token, string transID, string dataFlow, Node.Core.Document.NodeDocument[] docs)
            : base(requestorIP, hostName)
        {
            this.Token = token;
            this.SupplTransID = transID;
            this.DataFlow = dataFlow;
            this.Documents = docs;
            string opName = dataFlow;

            opName = checkOpName(dataFlow, opName);

            this.DownloadOp = new Operation(opName, Phrase.WEB_SERVICE_DOWNLOAD);

            if ((this.DownloadOp == null || this.DownloadOp.ID < 0))
            {              
                throw new Exception(Phrase.E_INVALID_DATA_FLOW);
            }
            //if ((this.DownloadOp == null || this.DownloadOp.ID < 0) && !opName.Equals("NODE"))
            //    this.DownloadOp = new Operation("NODE", Phrase.WEB_SERVICE_DOWNLOAD);
        }

        private string checkOpName(string dataFlow, string opName)
        {
            string name = opName;

            DBManager db = new DBManager();
            Operation op = db.GetOperationsDB().GetOperation(opName);
            
            if (op == null || op.ID < 0)
            {
                if (NodeVersion == NodeVer.VER_20)
                {
                    name = "NODE2";
                }
                else
                {
                    name = "NODE";
                }
            }
            return name;
        }
        /// <summary>
        /// Initialize process of DownloadHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.DownloadOp != null && this.DownloadOp.ID >= 0)
            {
                if (this.DownloadOp.DomainStatus != null && this.DownloadOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.DownloadOp.Status != null && this.DownloadOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        int count = this.Documents == null ? 0 : this.Documents.Length;
                        string[] names = null;
                        object[] values = null;
                        if (count == 0)
                        {
                            names = new string[] { "DataFlow", "Documents" };
                            values = new object[] { this.DataFlow, "No Documents Submitted" };
                        }
                        else
                        {
                            names = new string[1 + count];
                            values = new object[1 + count];
                            names[0] = "DataFlow";
                            values[0] = this.DataFlow;
                            for (int i = 1; i < count + 1; i++)
                            {
                                if (this.Documents[i - 1] != null && this.Documents[i - 1].name != null)
                                    names[i] = this.Documents[i - 1].name;
                                else
                                    names[i] = "Document " + i;
                                if (this.Documents[i - 1] != null && this.Documents[i - 1].Stream != null && this.Documents[i - 1].Stream.Length > 0)
                                    values[i] = "Document Size: " + this.Documents[i - 1].Stream.Length + " bytes";
                                else
                                    values[i] = "Doucment Size: 0 bytes";
                            }
                        }
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.DownloadOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, this.SupplTransID,
                            this.Token, null, null, null, this.HostName, names, values);
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
        /// Authorize process of DownloadHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_DOWNLOAD, this.DownloadOp.Name);
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
        /// Excute DataFlow process of DownloadHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            //for dataflow
            IActionProcess process = GetActionProcess();
            process.ActionOperationLog.Documents = this.Documents;

            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.DataFlow);
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.documents.ToString(), this.Documents);
            process.CreateActionParameter(WebServiceParameter.SuppliedTransID.ToString(), this.SupplTransID);

            IActionParameter retObj = process.Execute(dataflowConfig) as IActionParameter;
            Node.Core.Document.NodeDocument[] docs = new Node.Core.Document.NodeDocument[1];
            Node.Core.Document.NodeDocument doc = new Node.Core.Document.NodeDocument();
            docs[0] = doc;

            if (retObj.ParameterValue is string)
            {
                doc.content = System.Text.ASCIIEncoding.ASCII.GetBytes(retObj.ParameterValue.ToString());
            }
            else if (retObj.ParameterValue is byte[])
            {
                doc.content = retObj.ParameterValue as byte[];
            }
            doc.name = retObj.ParameterName;

            return docs;
        }
        /// <summary>
        /// Excute Plug-in process of DownloadHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.DownloadOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetDownloadPreProcess(dllName, className);
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
            IProcess process = new DllManager().GetDownloadProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.SupplTransID, this.DataFlow, this.Documents, param);
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
            IPostProcess process = new DllManager().GetDownloadPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.SupplTransID, this.DataFlow, this.Documents, param);
        }

    }
}
