using System;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace Node.Core.CertificatePolicy
{
    /// <summary>
    /// TrustedCertificatePolicy is used to implement Trusted Certificate Policy for NAAS Authentication. 
    /// </summary>
    public class TrustedCertificatePolicy : System.Net.ICertificatePolicy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp">End Point of NAAS Authenication Service</param>
        /// <param name="certificate">Encoded certificate passing</param>
        /// <param name="request">Name of Request</param>
        /// <param name="problem"></param>
        /// <returns>True if the certificate is valid</returns>
        public bool CheckValidationResult(System.Net.ServicePoint sp, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Net.WebRequest request, int problem)
        {
            return true;
        }
    }
}

