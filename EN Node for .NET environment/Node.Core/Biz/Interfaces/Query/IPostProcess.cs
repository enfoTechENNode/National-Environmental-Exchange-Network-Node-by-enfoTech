using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.Query
{
    /// <summary>
    /// The Interface for Query PostProcess
    /// </summary>
    public interface IPostProcess
    {
        /// <summary>
        /// PostProcess of Query.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="request">The name of Plug-In Query Oparation to be processed.</param>
        /// <param name="rowID">The start row for the result set. </param>
        /// <param name="maxRows">The Maximum number of rows to be returned.</param>
        /// <param name="parameters">Array of Parameter for the query request</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>string of result</returns>
        void Execute(string token, string request, int rowID, int maxRows, string[] parameters, PostParam param);
    }
}
