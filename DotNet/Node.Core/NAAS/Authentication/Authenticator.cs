using System;
using System.Net;

using Node.Core;
using Node.Core.CertificatePolicy;
using Node.Core.NAAS;
using Node.Core.NAASAuth;

namespace Node.Core.NAAS.Authentication
{
    /// <summary>
    /// Authenticator is making http request to NAAS for Authentication.
    /// </summary>
    public class Authenticator : cdxSecurity
    {
        /// <summary>
        /// Constructor of Authenticator.
        /// </summary>
        /// <param name="authURL">The endpoint for authentication.</param>
        /// <param name="proxyServer">IP address of proxy server.</param>
        /// <param name="proxyUID">The user id for the proxy server.</param>
        /// <param name="proxyPWD">The password for the proxy server.</param>
        public Authenticator(string authURL, string proxyServer, string proxyUID, string proxyPWD)
        {
            this.Url = authURL;
            if (proxyServer != null && !proxyServer.Trim().Equals(""))
            {
                WebProxy wp = new WebProxy(proxyServer);
                wp.Address = new Uri(proxyServer);
                wp.BypassProxyOnLocal = true;
                if (proxyUID != null && !proxyUID.Trim().Equals("") && proxyPWD != null && !proxyPWD.Trim().Equals(""))
                    wp.Credentials = new NetworkCredential(proxyUID, proxyPWD);
                this.Proxy = wp;
            }
            ServicePointManager.ServerCertificateValidationCallback = Authenticator.ValidateServerCertificate;
        }
        /// <summary>
        /// A method to invoke authentication process to NAAS.
        /// </summary>
        /// <param name="userId">NAAS user account.</param>
        /// <param name="credential">NAAS credential.</param>
        /// <param name="authenticationMethod">The authentication method to be used.</param>
        /// <returns>The security token string.</returns>
        public string Authenticate(string userId, string credential, string authenticationMethod)
        {
            switch (authenticationMethod.ToUpper())
            {
                case "CERTIFICATE":
                    return this.Authenticate(userId, credential, AuthMethod.certificate);
                case "DIGEST":
                    return this.Authenticate(userId, credential, AuthMethod.digest);
                case "XKMS":
                    return this.Authenticate(userId, credential, AuthMethod.xkms);
                case "HMAC":
                    return this.Authenticate(userId, credential, AuthMethod.hmac);
                default:
                    return this.Authenticate(userId, credential, AuthMethod.password);
            }
        }
        /// <summary>
        /// A method to invoke authentication process to NAAS.
        /// </summary>
        /// <param name="userId">NAAS user account.</param>
        /// <param name="credential">NAAS credential.</param>
        /// <param name="authenticationMethod">The authentication method to be used.</param>
        /// <param name="clientHost">The endpoint for client host.</param>
        /// <returns>The security token string.</returns>
        public string CentralAuth(string userId, string credential, string authenticationMethod, string clientHost)
        {
            switch (authenticationMethod.ToUpper())
            {
                case "CERTIFICATE":
                    return this.CentralAuth(userId, credential, AuthMethod.certificate, clientHost);
                case "DIGEST":
                    return this.CentralAuth(userId, credential, AuthMethod.digest, clientHost);
                case "XKMS":
                    return this.CentralAuth(userId, credential, AuthMethod.xkms, clientHost);
                case "HMAC":
                    return this.CentralAuth(userId, credential, AuthMethod.hmac, clientHost);
                default:
                    return this.CentralAuth(userId, credential, AuthMethod.password, clientHost);
            }
        }
        /// <summary>
        /// A method to Validate NAAS security token. 
        /// </summary>
        /// <param name="authToken">The security token.</param>
        /// <param name="clientHost">The endpoint of Client Host.</param>
        /// <param name="nodeURL">The URL of the node.</param>
        /// <param name="nodeName">The name of the node.</param>
        /// <param name="webMethod">The name of web method.</param>
        /// <param name="request">The name of request.</param>
        /// <param name="parameters">An array of parameters.</param>
        /// <returns>NAAS User Name if the token is valid.</returns>
        public string Authorize(string authToken, string clientHost, string nodeURL, string nodeName, string webMethod, string request, string[] parameters)
        {
            string resourceURI = nodeURL + "?node=" + nodeName + "&method=" + webMethod;
            if (request != null)
                resourceURI += "&request=" + request;
            if (parameters != null && parameters.Length > 0)
            {
                resourceURI += "&Param=" + parameters[0];
                for (int i = 1; i < parameters.Length; i++)
                    resourceURI += ";" + parameters[i];
            }
            return this.Validate(authToken, clientHost, resourceURI);
        }
        /// <summary>
        /// A method to check server certificate.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="certificate">certificate</param>
        /// <param name="chain">chain</param>
        /// <param name="errors">error</param>
        /// <returns>true if correct.</returns>
        public static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }
    }
}
