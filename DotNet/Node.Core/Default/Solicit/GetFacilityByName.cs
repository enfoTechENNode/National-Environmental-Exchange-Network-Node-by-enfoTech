using System;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Solicit;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Default.Solicit
{
    /// <summary>
    /// The defalut plug-in class for Solicit Operation.
    /// </summary>
    public class GetFacilityByName : IProcess
    {
        /// <summary>
        /// The entry point of solict process.
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="returnURL">The return endpoint url.</param>
        /// <param name="request">The name of request.</param>
        /// <param name="parameters">The input paramters for solicit operation.</param>
        /// <param name="param">The operation parameters.</param>
        /// <returns>The result of solict.</returns>
        public NodeDocument[] Execute(string token, string returnURL, string request, string[] parameters, ProcParam param)
        {
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            string message = "Received Solicit " + token + ", " + returnURL + ", " + request;
            if (parameters != null && parameters.Length > 0)
                foreach (string s in parameters)
                    message += ", " + s;
            logger.UpdateOperationLog(param.OpLogID, "Soliciting", message, param.User, false);
            NodeDocument[] retDocs = new NodeDocument[1];
            retDocs[0] = new NodeDocument();
            retDocs[0].name = "Solicit.txt";
            retDocs[0].type = "TEXT";
            retDocs[0].content = new System.Text.ASCIIEncoding().GetBytes(message);
            return retDocs;
        }
    }
}
