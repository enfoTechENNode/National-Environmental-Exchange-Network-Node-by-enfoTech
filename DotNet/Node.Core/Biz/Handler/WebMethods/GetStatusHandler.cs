using System;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.GetStatus;
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
    /// GetStatusHandler is core class for GetStatus Web Service
    /// </summary>
    public class GetStatusHandler : BaseHandler
    {
        private string SupplTransID = null;
        private Operation GetStatusOp = null;
        /// <summary>
        /// This method is constructor of  GetStatusHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        public GetStatusHandler(string requestorIP, string hostName, string token, string transID) : base (requestorIP, hostName)
        {
            this.Token = token;
            this.SupplTransID = transID;
            string opName = (NodeVersion == NodeVer.VER_11) ? "NODE" : "NODE2";
            this.GetStatusOp = new Operation(opName, Phrase.WEB_SERVICE_GETSTATUS);
        }
        /// <summary>
        /// Initialize process of GetStatusHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.GetStatusOp != null && this.GetStatusOp.ID >= 0)
            {
                if (this.GetStatusOp.DomainStatus != null && this.GetStatusOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.GetStatusOp.Status != null && this.GetStatusOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.GetStatusOp.ID, this.TransID, null, Phrase.STATUS_RECEIVED,
                            Phrase.MESSAGE_RECEIVED, this.RequestorIP, this.SupplTransID, this.Token, null,
                            null, null, this.HostName, null, null);
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
        /// Authorize process of GetStatusHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                if (NodeVersion == NodeVer.VER_11)
                {
                    user = this.Authorize(Phrase.WEB_SERVICE_GETSTATUS, "NODE");
                }
                else if (NodeVersion == NodeVer.VER_20)
                {
                    user = this.Authorize(Phrase.WEB_SERVICE_GETSTATUS, "NODE2");
                }

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
        /// Excute DataFlow process of GetStatusHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            //data flow
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);

            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of GetStatusHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.GetStatusOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetGetStatusPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.SupplTransID, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetGetStatusProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.SupplTransID, param);
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
            IPostProcess process = new DllManager().GetGetStatusPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.SupplTransID, param);
        }
    }
}
