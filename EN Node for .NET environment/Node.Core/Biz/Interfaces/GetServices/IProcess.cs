using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.GetServices
{
    /// <summary>
    /// The Interface for GetServices Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process for GetServices.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="serviceType">Specifies which type of service are to be used.</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>A list of Opeation Name</returns>
        string[] Execute(string token, string serviceType, ProcParam param);
    }
}
