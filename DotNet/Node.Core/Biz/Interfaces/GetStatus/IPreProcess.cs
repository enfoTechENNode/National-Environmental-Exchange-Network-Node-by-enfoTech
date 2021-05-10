using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.GetStatus
{
    /// <summary>
    /// The Interface for GetStatus PreProcess
    /// </summary>
    public interface IPreProcess
    {
        /// <summary>
        /// PreProcess for GetStatus.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="transID">The transaction id is result of Solicict/Submiet Operation.</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>Status String</returns>
        void Execute(string token, string transID, PreParam param);
    }
}
