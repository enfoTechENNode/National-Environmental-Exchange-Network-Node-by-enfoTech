using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.Execute
{
    /// <summary>
    /// The Interface for Execute Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process of Excute.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="interfaceName">Name of the internal interface.</param>
        /// <param name="methodName">Name of the method to be invoked within the interface.</param>
        /// <param name="parameters">Array of Parameter for the solicit request</param>
        /// <param name="param">Operation related parameters</param>
        void Execute(string token, string interfaceName, string methodName, string[] parameters, ProcParam param);
    }
}
