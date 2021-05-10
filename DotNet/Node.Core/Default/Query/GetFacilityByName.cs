using System;
using System.Collections;
using System.Collections.Generic;
using Node.Core.Biz.Interfaces.Query;
using Node.Core.Biz.Manageable.Parameters;

using Node.Core.API;
using DataFlow.Component.Interface;

namespace Node.Core.Default.Query
{
    /// <summary>
    /// The defalut plug-in class for Query Operation.
    /// </summary>
    public class GetFacilityByName : IProcess, IActionComponent
    {
        private string aliasName;
        /// <summary>
        /// The constructor of Query process.
        /// </summary>
        public GetFacilityByName()
        {

        }
        /// <summary>
        /// AliasName of Query Process.(DataWizard)
        /// </summary>
        public string AliasName
        {
            get { return this.aliasName; }
            set { this.aliasName = value; }
        }
        /// <summary>
        /// The entry point of Query process using DataWizard.
        /// </summary>
        /// <param name="input">DataWizard input paramter.</param>
        /// <param name="operationLog">operationlog</param>
        /// <returns>DataWizard output paramter.</returns>
        public IActionParameter Execute(List<IActionParameter> input, IActionOperationLog operationLog)
        {
            ActionParameter output = new ActionParameter();
            output.Direction = ActionParameterDirection.Output;
            output.ParameterName = "output";
            output.ParameterType = "System.String";

            string request = "";
            int rowId = 0;
            int maxRows = -1;
            string[] pars = null;
            ArrayList arr = new ArrayList();
            foreach (IActionParameter comp in input)
            {
                if (comp.ParameterName == WebServiceParameter.request.ToString())
                    request = comp.ParameterValue + "";
                else if (comp.ParameterName == WebServiceParameter.rowId.ToString())
                    rowId = int.Parse(comp.ParameterValue + "");
                else if (comp.ParameterName == WebServiceParameter.maxRows.ToString())
                    maxRows = int.Parse(comp.ParameterValue + "");
                arr.Add(comp.ParameterValue);
            }
            pars = new string[arr.Count];
            int i = 0;
            foreach (object obj in arr)
                pars[i++] = obj + "";

            output.ParameterValue = Execute(operationLog.SecureToken, request, rowId, maxRows, pars, null);
            return output;
        }
        /// <summary>
        /// The entry point of Query process.
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="request">The name of request.</param>
        /// <param name="rowID">The start point of record.</param>
        /// <param name="maxRows">The number of record return.</param>
        /// <param name="parameters">The parameters for Query Operation</param>
        /// <param name="param">The operation paraemter.</param>
        /// <returns>The query result string.(XML format)</returns>
        public string Execute(string token, string request, int rowID, int maxRows, string[] parameters, ProcParam param)
        {
            string ret = "<Result>Query of " + request + " with ";
            if (parameters != null && parameters.Length > 0)
            {
                ret += "Parameters: ";
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i != 0) ret += ", ";
                    ret += parameters[i];
                }
            }
            else
                ret += "no Parameters";
            ret += "</Result>";
            return ret;
        }
    }
}
