using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

using Node.Lib.Security;
using Node.Core.API;
using Node.Core.Biz.Interfaces.Authenticate;
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
    /// AuthenticateHandler is core class for Authenticate Web Service.
    /// </summary>
    public class AuthenticateHandler : BaseHandler
    {
        private string UserID = null;
        private string Credential = null;
        private string AuthenticationMethod = null;
        private string Domain = null;
        private Operation AuthOp = null;

        /// <summary>
        /// This method is constructor of  AuthenticateHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor</param>
        /// <param name="hostName">The Host Name for Node Operations</param>
        /// <param name="userID">The user ID of the person or system. It is recommended that an email address be used as the userID in the Exchange NetWork.</param>
        /// <param name="credential">The user credential for accessing the network services. </param>
        /// <param name="authMethod">Specifies which authentication methods are to be used.</param>
        /// <param name="domain">Specifies which domain are to be used.</param>
        public AuthenticateHandler(string requestorIP, string hostName, string userID, string credential, string authMethod,string domain): base(requestorIP, hostName)
        {
            this.UserID = userID;
            this.Credential = credential;
            this.AuthenticationMethod = authMethod.ToUpper();
            this.Domain = domain;
            string opName = "";

            if (this.isValidAuthMethod(this.AuthenticationMethod))
            {
                opName = this.AuthenticationMethod;
                if (NodeVersion == NodeVer.VER_20)
                {
                    opName = opName + "2";
                }
            }
            else
            {
                if (NodeVersion == NodeVer.VER_20)
                {
                    opName = "PASSWORD2";
                }
                else
                {
                    opName = "PASSWORD";
                }
            }

            this.AuthOp = new Operation(opName, Phrase.WEB_SERVICE_AUTHENTICATE);
        }
        /// <summary>
        /// This method is constructor of  AuthenticateHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor</param>
        /// <param name="hostName">The Host Name for Node Operations</param>
        /// <param name="userID">The user ID of the person or system. It is recommended that an email address be used as the userID in the Exchange NetWork.</param>
        /// <param name="credential">The user credential for accessing the network services. </param>
        /// <param name="authMethod">Specifies which authentication methods are to be used.</param>
        public AuthenticateHandler(string requestorIP, string hostName, string userID, string credential, string authMethod) : base(requestorIP,hostName)
        {
            this.UserID = userID;
            this.Credential = credential;
            this.AuthenticationMethod = authMethod.ToUpper();
            this.Domain = "";
            string opName = "";

            if (this.isValidAuthMethod(this.AuthenticationMethod))
            {
                opName = this.AuthenticationMethod;
                if (NodeVersion == NodeVer.VER_20)
                {
                    opName = opName + "2";
                }
            }
            else
            {
                if (NodeVersion == NodeVer.VER_20)
                {
                    opName = "PASSWORD2";
                }
                else
                {
                    opName = "PASSWORD";
                }
            }
            this.AuthOp = new Operation(opName, Phrase.WEB_SERVICE_AUTHENTICATE);
        }
        /// <summary>
        /// Initialize process of AuthenticateHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.AuthOp != null && this.AuthOp.ID >= 0)
            {
                if (this.AuthOp.DomainStatus != null && this.AuthOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.AuthOp.Status != null && this.AuthOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        string[] paramNames = new string[] { "User Name", "Credential", "Authentication Method","Domain" };
                        object[] paramValues = new object[] { this.UserID, new Cryptography().Encrypting(this.Credential,Phrase.CryptKey), this.AuthenticationMethod,this.Domain };
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.AuthOp.ID, this.TransID, this.UserID,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null, null, null,
                            null, null, this.HostName, paramNames, paramValues);
                    }
                    else
                        throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);

            if (!this.isValidAuthMethod(this.AuthenticationMethod))
            {
                throw new Exception(Phrase.E_AUTH_METHOD);
            }
        }
        /// <summary>
        /// Authorize process of AuthenticateHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            return this.UserID;
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
            process.ActionOperationLog.Credential = this.Credential;
            process.ActionOperationLog.AuthMethod = this.AuthenticationMethod;
            
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.userId.ToString(), this.UserID);
            process.CreateActionParameter(WebServiceParameter.credential.ToString(), this.Credential);
            process.CreateActionParameter(WebServiceParameter.authenticationMethod.ToString(), this.AuthenticationMethod);

            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of AuthenticateHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            string ret = "" + this.ExecuteOperation(this.AuthOp);

            if (this.Domain != null && !this.Domain.Trim().Equals(""))
            {
                if (this.Domain != "NAAS" && this.Domain.ToUpper() != "DEFAULT")
                {
                    IDomains domainDB = new DBManager().GetDomainsDB();
                    Domain domain = domainDB.GetDomain(this.Domain);
                    if (domain.ID == -1)
                    {
                        throw new Exception(Phrase.E_UNKNOWN_USER);
                    }
                }
                else
                {
                    ILogging logDB = new DBManager().GetLoggingDB();
                    logDB.UpdateOperationLogToken(this.TransID, ret, this.UserID);
                }
            }
            else
            {
                ILogging logDB = new DBManager().GetLoggingDB();
                logDB.UpdateOperationLogToken(this.TransID, ret, this.UserID);
            }
            return ret;
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetAuthPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.UserID, this.Credential, this.AuthenticationMethod, param);
        }
        /// <summary>
        /// Excute Process Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        /// <returns>result of process in object type.</returns>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetAuthProcess(dllName, className);
            if (process != null)
                return process.Execute(this.UserID, this.Credential, this.AuthenticationMethod, param);
            throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }
        /// <summary>
        /// Excute PostProcess Plug-in dll. 
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            IPostProcess process = new DllManager().GetAuthPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.UserID, this.Credential, this.AuthenticationMethod, param);
        }

        private bool isValidAuthMethod(string authMethod)
        {
            bool bOk = false;

            switch (authMethod)
            { 
                case "PASSWORD":
                case "DIGEST":
                case "CERTIFICATE":
                case "TOKEN":
                    bOk = true;
                    break;
                default:
                    bOk = false;
                    break;
            }

            return bOk;
        }
    }
}
