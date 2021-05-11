using System;
using System.Net;

using Node.Core.NAASUserManager;

namespace Node.Core.NAAS.UserManagment
{
    /// <summary>
    /// This class can be used to Invoke Methods that Admniister NAAS Users
    /// </summary>
    public class UserManager : usermgr
    {
        #region Public Constructor

        /// <summary>
        /// This Constructor constructs an instance of a NAAS User Manager
        /// </summary>
        /// <param name="url">The url of the NAAS User Management Web Service</param>
        /// <param name="proxyServer">The proxy server needed, null or empty string if none needed</param>
        /// <param name="proxyUID">The proxy user id needed, null or empty string if none needed</param>
        /// <param name="proxyPWD">The proxy password needed, null or empty string if none needed</param>
        public UserManager(string url, string proxyServer, string proxyUID, string proxyPWD)
        {
            this.Url = url;
            if (proxyServer != null && !proxyServer.Trim().Equals(""))
            {
                WebProxy wp = new WebProxy(proxyServer);
                wp.Address = new Uri(proxyServer);
                wp.BypassProxyOnLocal = true;
                if (proxyUID != null && !proxyUID.Trim().Equals("") && proxyPWD != null && !proxyPWD.Trim().Equals(""))
                    wp.Credentials = new NetworkCredential(proxyUID, proxyPWD);
                this.Proxy = wp;
            }
            ServicePointManager.ServerCertificateValidationCallback = UserManager.ValidateServerCertificate;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Needed to call Web Services
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }

        #endregion
    }
}
