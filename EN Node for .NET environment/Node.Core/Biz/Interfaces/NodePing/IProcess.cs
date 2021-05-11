using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.NodePing
{
    /// <summary>
    /// The Interface for NodePing Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process for NodePing.
        /// </summary>
        /// <param name="hello">Input string</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>Input string </returns>
        string Execute(string hello, ProcParam param);
    }
}
