using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.Solicit
{
    /// <summary>
    /// The Interface for Solicit PreProcess
    /// </summary>
    public interface IPreProcess
    {
        /// <summary>
        /// PreProcess of Solicit.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="returnURL">It can be recipient email address or NofificationURI.</param>
        /// <param name="request">The name of Plug-In Solicit Oparation to be processed.</param>
        /// <param name="parameters">Array of Parameter for the solicit request</param>
        /// <param name="param">Operation Related Parameters</param>
        /// <returns>Transaction id</returns>
        void Execute(string token, string returnURL, string request, string[] parameters, PreParam param);
    }
}
