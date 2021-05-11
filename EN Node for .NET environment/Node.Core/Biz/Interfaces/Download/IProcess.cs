using System;

using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Biz.Interfaces.Download
{
    /// <summary>
    /// The Interface for Download Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process of Download.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used. </param>
        /// <param name="docs">The reserved container for the download requesed documents.</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>Array of NodeDocument</returns>
        NodeDocument[] Execute(string token, string transID, string dataFlow, NodeDocument[] docs, ProcParam param);
    }
}
