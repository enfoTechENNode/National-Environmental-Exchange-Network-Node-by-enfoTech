using System;

using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Biz.Interfaces.Submit
{
    /// <summary>
    /// The Interface for Submit PostProcess
    /// </summary>
    public interface IPostProcess
    {
        /// <summary>
        /// Process of Submit.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used.</param>
        /// <param name="docs">Array of files being submitted</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>Transaction ID</returns>
        void Execute(string token, string transID, string dataFlow, NodeDocument[] docs, PostParam param);
    }
}
