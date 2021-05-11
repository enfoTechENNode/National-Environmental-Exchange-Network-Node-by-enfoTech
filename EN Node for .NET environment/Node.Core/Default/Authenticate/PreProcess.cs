using System;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Authenticate;
using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Default.Authenticate
{
    /// <summary>
    /// The defalut plug-in PreProcess class for Authenticate Operation.
    /// </summary>
    public class PreProcess : IPreProcess
    {
        /// <summary>
        /// Constructor of Authenticate PreProcess.
        /// </summary>
        public PreProcess()
        {
        }
        /// <summary>
        /// The entry point of authentication pre process.
        /// </summary>
        /// <param name="userID">NAAS user account.</param>
        /// <param name="credential">The credential</param>
        /// <param name="authenticationMethod">The authentication Method.</param>
        /// <param name="param">The operation parameters.</param>
        public void Execute(string userID, string credential, string authenticationMethod, PreParam param)
        {
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            logger.UpdateOperationLog(param.OpLogID, "PreProcess", "Pre Proccessing " + userID + "'s Authentication Request", userID, false);
        }
    }
}
