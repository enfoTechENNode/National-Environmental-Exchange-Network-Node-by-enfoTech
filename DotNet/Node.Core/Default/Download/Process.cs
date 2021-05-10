using System;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Download;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core;
using Node.Core.Document;

using DataFlow.Component.Interface;

namespace Node.Core.Default.Download
{
    /// <summary>
    /// The defalut plug-in class for Download Operation.
    /// </summary>
    public class Process : IProcess, IActionComponent
    {
        private string aliasName;
        /// <summary>
        /// AliasName of Download Process.(DataWizard)
        /// </summary>
        public string AliasName
        {
            get { return this.aliasName; }
            set { this.aliasName = value; }
        }
        /// <summary>
        /// The entry point of Download process using DataWizard.
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
            output.ParameterValue = Execute(operationLog.SecureToken, operationLog.TransactionID, operationLog.OperationName, (NodeDocument[])operationLog.Documents, null);
            return output;
        }
        /// <summary>
        /// The entry point of Download process.
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="transID">The transation id.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <param name="docs">The container for download.</param>
        /// <param name="param">The operation parameters.</param>
        /// <returns>The requested download files.</returns>
        public NodeDocument[] Execute(string token, string transID, string dataFlow, NodeDocument[] docs, ProcParam param)
        {
            NodeDocument[] retDocs = null;
            try
            {
                bool bTransIDOnly = false;

                string sText = "";
                string sText2 = "";
                string[] names = null;
                string[] ids = null;

                if (docs != null && docs.Length > 0)
                {
                    names = new string[docs.Length];
                    ids = new string[docs.Length];

                    for (int i = 0; i < docs.Length; i++)
                        if (docs[i] != null)
                        {
                            names[i] = docs[i].name;
                            sText += names[i];
                            ids[i] = docs[i].href;
                            sText2 += ids[i];
                        }
                }

                if (sText.Trim().Equals("") && sText2.Trim().Equals(""))
                    bTransIDOnly = true;

                if (!bTransIDOnly)
                {
                    if (!sText.Trim().Equals(""))
                    {
                        retDocs = new DocManager().GetDocuments(transID, names, null);
                    }
                    else if (!sText2.Trim().Equals(""))
                    {
                        retDocs = new DocManager().GetDocuments(transID, ids);
                    }
                }
                else
                {
                    retDocs = new DocManager().GetDocuments(transID); 
                }

            }
            catch (Exception e)
            {
                
                throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode, e);
            }

            // if documents are not found, then throws exception.
            if (retDocs == null || retDocs.Length == 0)
                throw new SoapException(Phrase.E_FILE_NOT_FOUND, SoapException.ClientFaultCode);

            return retDocs;
        }
    }
}
