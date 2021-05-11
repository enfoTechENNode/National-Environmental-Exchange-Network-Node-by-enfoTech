using System;

using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;

namespace Node.Core.Biz.Interfaces.GetServices
{
    /// <summary>
    /// The Interface for GetServices PreProcess
    /// </summary>
    public interface IPreProcess
    {
        /// <summary>
        /// PreProcess for GetServices.
        /// </summary>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="serviceType">Specifies which type of service are to be used.</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>A list of Opeation Name</returns>
        void Execute(string token, string serviceType, PreParam param);
    }
}
