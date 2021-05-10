using System;
using System.Xml;

using Node.Core.Biz.Objects;
using Node.Core.Biz.Handler;

namespace Node.Core.Biz.NAAS
{
    /// <summary>
    /// This class provides methods for calling Authenticate and Authorize on the NAAS Server
    /// </summary>
    public class Authenticator
    {
        #region Public Constructor

        /// <summary>
        /// Constructs an Object that Allows For Authentication/Authorization with NAAS
        /// </summary>
        public Authenticator()
        {
            SystemConfiguration config = new SystemConfiguration();
            string naasURL = config.GetNAASAuthenticationAddress();
            if (naasURL != null && !naasURL.Trim().Equals(""))
            {
                string proxyServer = config.GetProxyHost();
                string proxyUID = null;
                string proxyPWD = null;
                if (proxyServer != null && !proxyServer.Trim().Equals(""))
                {
                    proxyUID = config.GetProxyUID();
                    proxyPWD = config.GetProxyPWD();
                    if (proxyUID == null || proxyUID.Trim().Equals(""))
                        proxyUID = null;
                    if (proxyPWD == null || proxyPWD.Trim().Equals(""))
                        proxyPWD = null;
                }
                this.auth = new Node.Core.NAAS.Authentication.Authenticator(naasURL, proxyServer, proxyUID, proxyPWD);
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// A method to invoke authentication process to NAAS.
        /// </summary>
        /// <param name="userName">NAAS user account.</param>
        /// <param name="password">NAAS credential.</param>
        /// <param name="authMethod">The authentication method to be used.</param>
        /// <returns>The security token string.</returns>
        public string Authenticate(string userName, string password, string authMethod)
        {
            return this.auth.Authenticate(userName, password, authMethod);
        }
        /// <summary>
        /// A method to invoke authentication process to NAAS.
        /// </summary>
        /// <param name="userName">NAAS user account.</param>
        /// <param name="password">NAAS credential.</param>
        /// <param name="authMethod">The authentication method to be used.</param>
        /// <param name="clientHost">The endpoint for client host.</param>
        /// <returns>The security token string.</returns>
        public string Authenticate(string userName, string password, string authMethod, string clientHost)
        {
            return this.auth.CentralAuth(userName, password, authMethod, clientHost);
        }
        /// <summary>
        /// A method to Validate NAAS security token. 
        /// </summary>
        /// <param name="token">The security token.</param>
        /// <param name="clientHost">The endpoint of Client Host.</param>
        /// <param name="webMethod">The name of web method.</param>
        /// <param name="request">The name of request.</param>
        /// <returns>NAAS User Name if the token is valid.</returns>
        public string Authorize(string token, string clientHost, string webMethod, string request)
        {
            SystemConfiguration config = new SystemConfiguration();

            string nodeURL = "";

            if (BaseHandler.NodeVersion == BaseHandler.NodeVer.VER_20)
            {
                nodeURL = config.GetNodeAddress_V2();
            }
            else
            {
                nodeURL = config.GetNodeAddress();
            }
            string name = config.GetNodeName();
            return this.auth.Authorize(token, clientHost, nodeURL, name, webMethod, request, null);
        }

        #endregion

        #region Private Fields

        private Node.Core.NAAS.Authentication.Authenticator auth = null;

        #endregion
    }
}
