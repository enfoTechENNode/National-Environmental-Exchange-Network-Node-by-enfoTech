using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.Task
{
    /// <summary>
    /// The Interface for Task Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process of Task.
        /// </summary>
        /// <param name="inputParameters">Task related parameters</param>
        /// <param name="param">Operation related Parameters</param>
        void Execute(string[] inputParameters, ProcParam param);
    }
}
