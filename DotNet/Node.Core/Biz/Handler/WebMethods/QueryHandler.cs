using System;
using System.Web.Services.Protocols;
using System.Collections;

using Node.Core.Biz.Interfaces.Query;
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
    /// QueryHandler is core class for Query Web Service
    /// </summary>
    public class QueryHandler : BaseHandler
    {
        private string Request = null;
        private int RowID = 0;
        private int MaxRows = -1;
        private string[] Parameters = null;
        private Operation QueryOp = null;
        /// <summary>
        /// This method is constructor of  QueryHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="request">The name of Plug-In Query Oparation to be processed.</param>
        /// <param name="rowID">The start row for the result set. </param>
        /// <param name="maxRows">The Maximum number of rows to be returned.</param>
        /// <param name="parameters">Array of Parameter for the query request</param>
        public QueryHandler(string requestorIP, string hostName, string token, string request, int rowID, int maxRows, string[] parameters) : base (requestorIP, hostName)
        {
            this.Token = token;
            this.Request = request;
            this.RowID = rowID;
            this.MaxRows = maxRows;
            this.Parameters = parameters;
            this.QueryOp = new Operation(request, Phrase.WEB_SERVICE_QUERY);
        }
        /// <summary>
        /// Returns Query Parameterlist.
        /// </summary>
        protected string[] ParameterList
        {
            get { return this.Parameters; }
            set { this.Parameters = value; }
        }
        /// <summary>
        /// Initialize process of QueryHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.QueryOp != null && this.QueryOp.ID >= 0)
            {
                if (this.QueryOp.DomainStatus != null && this.QueryOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.QueryOp.Status != null && this.QueryOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        string[] names = new string[] { "Request", "RowID", "MaxRows", "Parameters" };
                        object[] values = new object[] { this.Request, this.RowID, this.MaxRows, "" };
                        if (this.Parameters != null)
                        {
                            string temp = "";
                            for (int i = 0; i < this.Parameters.Length; i++)
                            {
                                if (i != 0) temp += ", ";
                                temp += this.Parameters[i];
                            }
                            values[3] = temp;
                        }
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.QueryOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null,
                            this.Token, null, null, null, this.HostName, names, values);
                    }
                    else
                        throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
            {
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
        }
        /// <summary>
        /// Authorize process of QueryHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_QUERY, this.Request);
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
                throw e;
            }
            catch (Exception)
            {
                ILogging logDB = new DBManager().GetLoggingDB();
                logDB.CopyUserFromToken(this.Token, this.TransID);
            }
            return user;
        }
        /// <summary>
        /// Excute DataFlow process of QueryHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.request.ToString(), this.Request);
            process.CreateActionParameter(WebServiceParameter.rowId.ToString(), this.RowID);
            process.CreateActionParameter(WebServiceParameter.maxRows.ToString(), this.MaxRows);

            DBManager dbMgr = new DBManager();
            Hashtable ht = dbMgr.GetOperationsDB().GetQueryNameParameterPairs();
            object obj = ht[this.Request];
            if (obj != null)
            {
                string[] pars = (obj + "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (this.Parameters != null && pars.Length == this.Parameters.Length)
                {
                    for (int i = 0; i < pars.Length; i++)
                        process.CreateActionParameter(pars[i].Trim(), this.Parameters[i]);
                }
            }
            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of QueryHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.QueryOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetQueryPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.Request, this.RowID, this.MaxRows, this.Parameters, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetQueryProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Token, this.Request, this.RowID, this.MaxRows, this.Parameters, param);

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
            IPostProcess process = new DllManager().GetQueryPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Token, this.Request, this.RowID, this.MaxRows, this.Parameters, param);
        }
    }
}
