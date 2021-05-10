using System;
using System.Web.Services.Protocols;
using System.Collections.Generic;

using Node.Lib.Security;

using Node.Core.Biz;
using Node.Core.Biz.Interfaces.Authenticate;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.NAAS;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core;
using Node.Core.Util;
using Node.Core.API;

using DataFlow.Component.Interface;

namespace Node.Core.Default.Authenticate
{
    /// <summary>
    /// The defalut plug-in class for Authenticate Operation.
    /// </summary>
    public class Process : IProcess, IActionComponent
    {
        private string aliasName;
        /// <summary>
        /// Constructor of Authenticate Process.
        /// </summary>
        public Process()
        {
        }
        /// <summary>
        /// AliasName of Authenticate Process.(DataWizard)
        /// </summary>
        public string AliasName
        {
            get { return this.aliasName; }
            set { this.aliasName = value; }
        }
        /// <summary>
        /// The entry point of authenticate process using DataWizard.
        /// </summary>
        /// <param name="input">DataWizard input paramter.</param>
        /// <param name="operationLog">operationlog</param>
        /// <returns>DataWizard output paramter.</returns>
        public IActionParameter Execute(List<IActionParameter> input, IActionOperationLog operationLog)
        {
            ActionParameter output = new ActionParameter();
            output.Direction = ActionParameterDirection.Output;
            output.ParameterName = "output";
            output.ParameterType = "System.Object";
            output.ParameterValue = Execute(operationLog.UserName, operationLog.Credential, operationLog.AuthMethod, operationLog.RequestedIP);
            return output;
        }
        /// <summary>
        /// The entry point of authenticate process.
        /// </summary>
        /// <param name="userID">NAAS user account.</param>
        /// <param name="credential">The credential</param>
        /// <param name="authenticationMethod">The authentication method.</param>
        /// <param name="param">The operation parameters.</param>
        /// <returns>The security token string.</returns>
        public string Execute(string userID, string credential, string authenticationMethod, ProcParam param)
        {
            return Execute(userID, credential, authenticationMethod, param.RequestorIP);
        }
        /// <summary>
        /// The entry point of authenticate process.
        /// </summary>
        /// <param name="userID">NAAS user account.</param>
        /// <param name="credential">The credential</param>
        /// <param name="authenticationMethod">The authentication Method.</param>
        /// <param name="requestedIP">The endpoint of request</param>
        /// <returns></returns>
        private string Execute(string userID, string credential, string authenticationMethod, string requestedIP)
        {
            string token = null;
            try
            {
                Authenticator requestor = new Authenticator();
                token = requestor.Authenticate(userID, credential, authenticationMethod, requestedIP);
            }
            catch (Exception)
            {

                UserManager uMgr = null;
                NAASUser u = null;
                try
                {
                    uMgr = new UserManager();
                    u = uMgr.GetUser(userID, true);
                }
                catch (Exception)
                {
                    token = CheckLocalUserProcess(userID, credential);
                }

                if (u != null && u.UserID == 0)
                {
                    throw new SoapException(Phrase.E_INVALID_CREDENTIAL, SoapException.ClientFaultCode);
                }
                else
                {
                    token = CheckLocalUserProcess(userID, credential);
                }
            }
            return token;
        }
        private string CheckLocalUserProcess(string userID, string credential)
        {
            string token = null;

            IUsers userDB = new DBManager().GetUsersDB();
            int authResult = userDB.LocalAuthenticate(userID, new Cryptography().Encrypting(credential, Phrase.CryptKey));
            if (authResult == 0)
            {
                NodeUtility utility = new NodeUtility();
                token = utility.GenerateToken();
            }
            else if (authResult == -1)
                throw new SoapException(Phrase.E_UNKNOWN_USER, SoapException.ClientFaultCode);
            else if (authResult == -2)
                throw new SoapException(Phrase.E_INVALID_CREDENTIAL, SoapException.ClientFaultCode);
            else if (authResult == -3)
                throw new SoapException(Phrase.E_INACTIVE_USER, SoapException.ClientFaultCode);
            else if (authResult == -4)
                throw new SoapException(Phrase.E_INVALID_PERMISSION, SoapException.ClientFaultCode);
            else
                throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode);

            return token;
        }
    }
}
