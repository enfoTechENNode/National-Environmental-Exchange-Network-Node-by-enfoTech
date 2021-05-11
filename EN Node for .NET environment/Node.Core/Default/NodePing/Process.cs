using System;

using Node.Core.Biz.Interfaces.NodePing;
using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Default.NodePing
{
    /// <summary>
    /// The defalut plug-in class for NodePing Operation.
    /// </summary>
    public class Process : IProcess
    {   
        /// <summary>
        /// Constructor of Process
        /// </summary>
        public Process()
        {

        }
        /// <summary>
        /// Method to Invoke Process.
        /// </summary>
        /// <param name="hello">Input string for NodePing Operation.</param>
        /// <param name="param">Operation Parameters.</param>
        /// <returns></returns>
        public string Execute(string hello, ProcParam param)
        {
            return hello;
        }
    }
}
