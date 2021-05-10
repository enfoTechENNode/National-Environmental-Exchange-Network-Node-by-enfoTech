using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.NodePing
{
    /// <summary>
    /// The Interface for NodePing PreProcess
    /// </summary>
    public interface IPreProcess
    {
        /// <summary>
        /// PreProcess for NodePing.
        /// </summary>
        /// <param name="hello">Input string</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>Input string </returns>
        void Execute(string hello, PreParam param);
    }
}
