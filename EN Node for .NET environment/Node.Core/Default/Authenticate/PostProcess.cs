using System;

using Node.Core.API;
using Node.Core.Biz.Interfaces.Authenticate;
using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Default.Authenticate
{
    /// <summary>
    /// The defalut plug-in PostProcess class for Authenticate Operation.
    /// </summary>
    public class PostProcess : IPostProcess
    {
        /// <summary>
        /// Constructor of Authenticate PostProcess.
        /// </summary>
        public PostProcess()
        {
        }
        /// <summary>
        /// The entry point of authentication post process.
        /// </summary>
        /// <param name="userID">NAAS user account.</param>
        /// <param name="credential">The credential</param>
        /// <param name="authenticationMethod">The authentication Method.</param>
        /// <param name="param">The operation parameters.</param>
        public void Execute(string userID, string credential, string authenticationMethod, PostParam param)
        {
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            logger.UpdateOperationLog(param.OpLogID, "PostProcess", "Post Proccessing " + userID + "'s Authentication Request", userID, true);
        }
    }
}
