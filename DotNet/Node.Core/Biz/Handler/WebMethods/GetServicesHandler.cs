using System;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.GetServices;
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
    /// GetServicesHandler is core class for GetServices Web Service .
    /// </summary>
    public class GetServicesHandler : BaseHandler
    {
        private string ServiceType = null;
        private Operation GetServicesOp = null;
        /// <summary>
        /// This method is constructor of  GetServicesHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="serviceType">Specifies which type of service are to be used.</param>
        public GetServicesHandler(string requestorIP, string hostName, string token, string serviceType) : base(requestorIP, hostName)
        {
            this.Token = token;
            this.ServiceType = serviceType;
            string opName = (NodeVersion == NodeVer.VER_11) ? "NODE" : "NODE2";
            this.GetServicesOp = new Operation(opName, Phrase.WEB_SERVICE_GETSERVICES);
        }
        /// <summary>
        /// Initialize process of AuthenticateHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.GetServicesOp != null && this.GetServicesOp.ID >= 0)
            {
                if (this.GetServicesOp.DomainStatus != null && this.GetServicesOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.GetServicesOp.Status != null && this.GetServicesOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.GetServicesOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null,
                            this.Token, null, null, this.ServiceType, this.HostName, null, null);
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
        /// Authorize process of AuthenticateHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                if (NodeVersion == NodeVer.VER_11)
                {
                    user = this.Authorize(Phrase.WEB_SERVICE_GETSERVICES, "NODE");
                }
                else if (NodeVersion == NodeVer.VER_20)
                {
                    user = this.Authorize(Phrase.WEB_SERVICE_GETSERVICES, "NODE2");
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
        /// Excute DataFlow process of AuthenticateHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            //for dataflow
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.serviceType.ToString(), this.ServiceType);

            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of AuthenticateHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.GetServicesOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetGetServicesPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.ServiceType, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetGetServicesProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.ServiceType, param);
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
            IPostProcess process = new DllManager().GetGetServicesPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.ServiceType, param);
        }
    }
}
