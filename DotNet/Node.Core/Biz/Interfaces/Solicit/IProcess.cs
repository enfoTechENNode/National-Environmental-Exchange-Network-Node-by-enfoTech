using System;

using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Biz.Interfaces.Solicit
{
    /// <summary>
    /// The Interface for Solicit Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process of Solicit.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="returnURL">It can be recipient email address or NofificationURI.</param>
        /// <param name="request">The name of Plug-In Solicit Oparation to be processed.</param>
        /// <param name="parameters">Array of Parameter for the solicit request</param>
        /// <param name="param">Operation Related Parameters</param>
        /// <returns>Transaction id</returns>
        NodeDocument[] Execute(string token, string returnURL, string request, string[] parameters, ProcParam param);
    }
}
