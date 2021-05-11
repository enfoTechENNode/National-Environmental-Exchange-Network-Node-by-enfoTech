using System;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Submit;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core;
using Node.Core.Document;

using DataFlow.Component.Interface;

namespace Node.Core.Default.Submit
{
    /// <summary>
    /// The defalut plug-in class for Submit Operation.
    /// </summary>
    public class Process : IProcess, IActionComponent
    {
        private string aliasName;
        /// <summary>
        /// AliasName of Submit Process.(DataWizard)
        /// </summary>
        public string AliasName
        {
            get { return this.aliasName; }
            set { this.aliasName = value; }
        }
        /// <summary>
        /// The entry point of Submit process using DataWizard.
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

            output.ParameterValue = Execute(operationLog.SecureToken, operationLog.TransactionID, operationLog.OperationName, 
                (NodeDocument[])operationLog.Documents, operationLog.UserName);

            return output;
        }
        /// <summary>
        /// The entry point of submit process.
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="transID">The transation id.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <param name="docs">The uploaded document.</param>
        /// <param name="param">The operation paramters.</param>
        /// <returns>The document id stored in node.</returns>
        public string Execute(string token, string transID, string dataFlow, NodeDocument[] docs, ProcParam param)
        {
            return Execute(token, transID, dataFlow, docs, param.User);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="transID">The transation id.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <param name="docs">The uploaded document.</param>
        /// <param name="userName">NAAS user accournt</param>
        /// <returns>The document id stored in node.</returns>
        public string Execute(string token, string transID, string dataFlow, NodeDocument[] docs, string userName)
        {
            try
            {
                if (docs != null && docs.Length > 0)
                {
                    DocManager manager = new DocManager();
                    foreach (NodeDocument doc in docs)
                    {
                        if (doc != null)
                        {
                            manager.UploadDocuments(doc, transID, dataFlow, "Submitted", DateTime.Now, userName);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode, e);
            }
            return transID;
        }

    }
}
