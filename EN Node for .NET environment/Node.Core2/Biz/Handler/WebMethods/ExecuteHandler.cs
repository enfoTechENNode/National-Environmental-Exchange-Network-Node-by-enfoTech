using System;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Biz.Interfaces.Execute;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class ExecuteHandler : Node.Core.Biz.Handler.BaseHandler
    {
        //***********************************************************************
        // Public Members
        //***********************************************************************
        #region Public Members

        #endregion

        //***********************************************************************
        // Private Members
        //***********************************************************************
        #region Private Members
        private string[] Parameters;
        private Operation ExecuteOp;
        private Execute execute;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public ExecuteHandler(string requestorIP, string hostName, Execute execute)
            : base(requestorIP, hostName)
        {
            this.execute = execute;
            this.Token = this.execute.securityToken;
            this.ExecuteOp = new Operation(this.execute.interfaceName, Phrase.WEB_SERVICE_EXECUTE);

            //get parameters
            this.Parameters = new string[this.execute.parameters.Length];
            for (int i = 0; i < this.Parameters.Length; i++)
            {
                ParameterType type = this.execute.parameters[i];
                this.Parameters[i] = type.parameterName + ":" + type.parameterEncoding.ToString() + ":" + type.Value;
            }
        }
        #endregion

        //***********************************************************************
        // Delegate Events
        //***********************************************************************
        #region Delegate Events

        #endregion

        //***********************************************************************
        // Public Properties
        //***********************************************************************
        #region Public Properties

        #endregion

        //***********************************************************************
        // Protected Properties
        //***********************************************************************
        #region Protected Properties

        #endregion

        //***********************************************************************
        // Public Methods
        //***********************************************************************
        #region Public Methods
        public string TransactionId
        {
            get { return base.TransID; }
        }

        #endregion

        //***********************************************************************
        // Protected Methods
        //***********************************************************************
        #region Protected Methods
        protected override void Initialize()
        {
            if (this.ExecuteOp != null && this.ExecuteOp.ID >= 0)
            {
                if (this.ExecuteOp.DomainStatus != null && this.ExecuteOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.ExecuteOp.Status != null && this.ExecuteOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        string[] names = new string[] { "InterfaceName", "MethodName", "Parameters" };
                        object[] values = new object[] { this.execute.interfaceName, this.execute.methodName, "" };

                        if (this.Parameters != null)
                        {
                            string temp = "";
                            for (int i = 0; i < this.Parameters.Length; i++)
                            {
                                if (i != 0) temp += ", ";
                                temp += this.Parameters[i];
                            }
                            values[2] = temp;
                        }
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.ExecuteOp.ID, base.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null,
                            this.execute.securityToken, null, null, null, base.HostName, names, values);
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

        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_EXECUTE, this.ExecuteOp.Name);
                if (user != null)
                {
                    ILogging logDB = new DBManager().GetLoggingDB();
                    logDB.UpdateOperationLogUserName(base.TransID, user);
                }
                else
                    throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);
            }
            catch (SoapException e)
            {
                throw e;
            }
            catch (Exception)
            {
                ILogging logDB = new DBManager().GetLoggingDB();
                logDB.CopyUserFromToken(this.Token, this.TransID);
            }
            return user;
        }

        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransactionId);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.interfaceName.ToString(), this.execute.interfaceName);
            process.CreateActionParameter(WebServiceParameter.methodName.ToString(), this.execute.methodName);
            process.CreateActionParameter(WebServiceParameter.parameters.ToString(), this.Parameters);

            return process.Execute(dataflowConfig);
        }

        protected override object Execute()
        {
            return this.ExecuteOperation(this.ExecuteOp);
        }

        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_EXECUTE_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_EXECUTE_OBJECT, this.execute);

            IPreProcess process = new DllManager().GetExecutePreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.execute.interfaceName, this.execute.methodName, this.Parameters, param);
        }

        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_EXECUTE_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_EXECUTE_OBJECT, this.execute);

            IProcess process = new DllManager().GetExecuteProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.execute.interfaceName, this.execute.methodName, this.Parameters, param);

            throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }

        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_EXECUTE_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_EXECUTE_OBJECT, this.execute);

            IPostProcess process = new DllManager().GetExecutePostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.execute.interfaceName, this.execute.methodName, this.Parameters, param);
        }

        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods

        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion

				
    }
}
