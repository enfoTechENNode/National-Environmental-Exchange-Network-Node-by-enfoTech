using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Node.Core;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class QueryHandler : Node.Core.Biz.Handler.WebMethods.QueryHandler
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
        private Query query;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public QueryHandler(string requestorIP, string hostName, Query query) : 
            base (requestorIP, hostName, query.securityToken, query.request, int.Parse(query.rowId), int.Parse(query.maxRows), null)
        {
            this.query = query;
            if (this.query.parameters != null)
            {
                string[] parameters = new string[this.query.parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterType type = this.query.parameters[i];
                    //parameters[i] = type.parameterName + ":" + type.parameterEncoding.ToString() + ":" + type.Value;
                    parameters[i] = type.Value;
                }

                base.ParameterList = parameters;
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

        #endregion

        //***********************************************************************
        // Protected Methods
        //***********************************************************************
        #region Protected Methods
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.query.securityToken);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.query.dataflow);
            process.CreateActionParameter(WebServiceParameter.request.ToString(), this.query.request);
            process.CreateActionParameter(WebServiceParameter.rowId.ToString(), this.query.rowId);
            process.CreateActionParameter(WebServiceParameter.maxRows.ToString(), this.query.maxRows);
            if (query.parameters != null)
            {
                foreach (ParameterType type in query.parameters)
                    process.CreateActionParameter(type.parameterName, type.Value);
            }

            return process.Execute(dataflowConfig);
        }

        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_QUERY_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_QUERY_OBJECT, this.query);

            base.ExecutePreProcess(dllName, className, param);
        }

        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_QUERY_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_QUERY_OBJECT, this.query);

            return base.ExecuteProcess(dllName, className, param);
        }

        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_QUERY_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_QUERY_OBJECT, this.query);

            base.ExecutePostProcess(dllName, className, param);
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
