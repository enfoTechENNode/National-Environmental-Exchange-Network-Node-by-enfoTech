using System;
using System.Web.Services.Protocols;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Notify;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Default.Notify
{
    /// <summary>
    /// The defalut plug-in class for Notify Operation.
    /// </summary>
    public class Process : IProcess
    {
        /// <summary>
        /// The entry point of Notify process.
        /// </summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="nodeAddress">The endpoint url.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <param name="documents">The container of document.</param>
        /// <param name="param">The operation parameters</param>
        /// <returns>The result of Notify operation.</returns>
        public string Execute(string securityToken, string nodeAddress, string dataFlow, NodeDocument[] documents, ProcParam param)
        {
            try
            {
                if (documents != null && documents.Length > 0)
                {
                    DocManager manager = new DocManager();
                    foreach (NodeDocument doc in documents)
                    {
                        if (doc != null)
                        {
                            manager.UploadDocuments(doc.name, doc.type, doc.Stream, param.TransactionID, dataFlow, "Notified", DateTime.Now, param.User);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode, e);
            }
            return param.TransactionID;
        }
    }
}
