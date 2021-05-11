using System;
using System.Text;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.Notify;
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
    /// NotifyHandler is core class for Notify Web Service
    /// </summary>
    public class NotifyHandler : BaseHandler
    {
        private string NodeAddress = null;
        private string DataFlow = null;
        private Node.Core.Document.NodeDocument[] Documents = null;
        private Operation NotifyOp = null;
        private string[] extraName = null;
        private object[] extraValue = null;
        /// <summary>
        /// This method is constructor of  NodtifyHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="nodeAddress">For document nofification, the parameter contains a network node address where the document can be downloaded. It should contain the initiator's node address, or be empty if not applicable, for event and status notifications.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used. </param>
        /// <param name="docs">The reserved container for the requesed documents.</param>
        public NotifyHandler(string requestorIP, string hostName, string token, string nodeAddress, string dataFlow, Node.Core.Document.NodeDocument[] docs)
            : base(requestorIP, hostName)
        {
            this.Token = token;
            this.NodeAddress = nodeAddress;
            this.DataFlow = dataFlow;
            this.Documents = docs;
            string opName = dataFlow;
            if (dataFlow == null || dataFlow.Trim().Equals(""))
            {
                if (NodeVersion == NodeVer.VER_11)
                {
                    opName = "NODE";
                    this.DataFlow = "NODE";
                }
                else if (NodeVersion == NodeVer.VER_20)
                {
                    opName = "NODE2";
                    this.DataFlow = "NODE2";
                }
            }
            this.NotifyOp = new Operation(opName, Phrase.WEB_SERVICE_NOTIFY);
            if ((this.NotifyOp == null || this.NotifyOp.ID < 0))
                throw new Exception(Phrase.E_INVALID_DATA_FLOW);

            //if ((this.NotifyOp == null || this.NotifyOp.ID < 0) && !opName.Equals("NODE"))
            //    this.NotifyOp = new Operation("NODE", Phrase.WEB_SERVICE_NOTIFY);

        }
        /// <summary>
        /// Initialize process of NotifyHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.NotifyOp != null && this.NotifyOp.ID >= 0)
            {
                if (this.NotifyOp.DomainStatus != null && this.NotifyOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.NotifyOp.Status != null && this.NotifyOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        string[] names = null;
                        object[] values = null;

                        if (this.ExtraName == null)
                        {
                            int count = this.Documents == null ? 0 : this.Documents.Length;
                            if (count == 0)
                            {
                                names = new string[] { Phrase.NP_DATA_FLOW, Phrase.NP_MESSAGE_CATEGORY, Phrase.NP_MESSAGE_NAME, Phrase.NP_MESSAGE_STATUS, Phrase.NP_MESSAGE_DETAIL, Phrase.NP_OBJECT_ID };
                                values = new object[] { this.DataFlow, "", "", "", "", "" };
                            }
                            else
                            {
                                names = new string[count * 6];
                                values = new object[count * 6];

                                int i = 0;
                                foreach (Node.Core.Document.NodeDocument doc in this.Documents)
                                {
                                    names[i] = Phrase.NP_DATA_FLOW;
                                    values[i++] = this.DataFlow;
                                    names[i] = Phrase.NP_MESSAGE_CATEGORY;
                                    values[i++] = "";
                                    names[i] = Phrase.NP_MESSAGE_NAME;
                                    values[i++] = doc.name;
                                    names[i] = Phrase.NP_MESSAGE_STATUS;
                                    values[i++] = doc.type;
                                    names[i] = Phrase.NP_MESSAGE_DETAIL;
                                    values[i++] = new UTF8Encoding().GetString(doc.content);
                                    names[i] = Phrase.NP_OBJECT_ID;
                                    values[i++] = "";
                                }
                            }
                        }
                        else
                        {
                            names = this.ExtraName;
                            values = this.ExtraValue;
                        }
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.NotifyOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null,
                            this.Token, this.NodeAddress, null, null, this.HostName, names, values);
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
        /// Authorize process of NotifyHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_NOTIFY, this.NotifyOp.Name);
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
        /// Excute DataFlow process of NotifyHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            //data flow
            IActionProcess process = GetActionProcess();
            process.ActionOperationLog.Documents = this.Documents;

            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.nodeAddress.ToString(), this.NodeAddress);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.DataFlow);
            process.CreateActionParameter(WebServiceParameter.documents.ToString(), this.Documents);

            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of NotifyHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.NotifyOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetNotifyPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.NodeAddress, this.DataFlow, this.Documents, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetNotifyProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.NodeAddress, this.DataFlow, this.Documents, param);
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
            IPostProcess process = new DllManager().GetNotifyPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.NodeAddress, this.DataFlow, this.Documents, param);
        }
        /// <summary>
        /// ExtraName
        /// </summary>
        protected string[] ExtraName
        {
            get { return this.extraName; }
            set { this.extraName = value; }
        }
        /// <summary>
        /// ExtraValue
        /// </summary>
        protected object[] ExtraValue
        {
            get { return this.extraValue; }
            set { this.extraValue = value; }
        }
    }
}
