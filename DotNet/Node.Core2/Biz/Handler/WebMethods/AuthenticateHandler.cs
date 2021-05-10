using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class AuthenticateHandler : Node.Core.Biz.Handler.WebMethods.AuthenticateHandler
    {
        #region variable
        private Authenticate auth;
        #endregion

        #region constructor
        public AuthenticateHandler(string requestorIP, string hostName, Authenticate authenticate)
            : base(requestorIP, hostName, authenticate.userId, authenticate.credential, authenticate.authenticationMethod,authenticate.domain)
        {
            this.auth = authenticate;
        }
        #endregion

        #region protected methods
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.userId.ToString(), this.auth.userId);
            process.CreateActionParameter(WebServiceParameter.credential.ToString(), this.auth.credential);
            process.CreateActionParameter(WebServiceParameter.domain.ToString(), this.auth.domain);
            process.CreateActionParameter(WebServiceParameter.authenticationMethod.ToString(), this.auth.authenticationMethod);
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);

            return process.Execute(dataflowConfig);
        }
        #endregion
    }
}
