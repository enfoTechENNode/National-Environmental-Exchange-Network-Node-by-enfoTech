using System;
using System.Collections.Generic;

using Node.Core.Biz.Interfaces.GetStatus;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.API;

using DataFlow.Component.Interface;

namespace Node.Core.Default.GetStatus
{
    /// <summary>
    /// The defalut plug-in class for GetStatus Operation.
    /// </summary>
    public class Process : IProcess, IActionComponent
    {
        private string aliasName;
        /// <summary>
        /// Constructor of GetStatus process.
        /// </summary>
        public Process()
        {
        }
        /// <summary>
        /// AliasName of GetStatus Process.(DataWizard)
        /// </summary>
        public string AliasName
        {
            get { return this.aliasName; }
            set { this.aliasName = value; }
        }
        /// <summary>
        /// The entry point of GetStatus process using DataWizard.
        /// </summary>
        /// <param name="input">DataWizard input paramter.</param>
        /// <param name="operationLog">operationlog</param>
        /// <returns>DataWizard output paramter.</returns>
        public IActionParameter Execute(List<IActionParameter> input, IActionOperationLog operationLog)
        {
            ActionParameter output = new ActionParameter();
            output.Direction = ActionParameterDirection.Output;
            output.ParameterName = "output";
            output.ParameterType = "System.Object";
            output.ParameterValue = Execute(operationLog.SecureToken, operationLog.TransactionID, null);

            return output;
        }
        /// <summary>
        /// The entry point of GetStatus.
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="transID">The transation id.</param>
        /// <param name="param">The operation parameters.</param>
        /// <returns></returns>
        public string Execute(string token, string transID, ProcParam param)
        {
            ILogging logDB = new DBManager().GetLoggingDB();
            string status = logDB.GetLatestStatus(transID);
            if (status == null)
                throw new Exception(Phrase.E_TRANSACTION_NOT_FOUND);
            return status;
        }
    }
}
