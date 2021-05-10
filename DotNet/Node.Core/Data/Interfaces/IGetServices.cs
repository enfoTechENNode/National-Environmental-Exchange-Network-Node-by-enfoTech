using System;
using System.Collections;
using System.Text;
using System.Data;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve the registered service information.
    /// </summary>
    public interface IGetServices
    {
        /// <summary>
        /// GetServices with ServiceType = 'ServiceType'
        /// </summary>
        /// <returns>String Array of Service Types Offered</returns>
        string[] GetServiceTypes();

        /// <summary>
        /// GetServices with ServiceType = 'Interfaces'
        /// </summary>
        /// <returns>String Array of Web Services Offered</returns>
        string[] GetWebServices();

        /// <summary>
        /// GetServices with ServiceType = 'Query'
        /// </summary>
        /// <returns>String Array of Queries Offered</returns>
        string[] GetQueries();

        /// <summary>
        /// GetServices with ServiceType = 'Solicit'
        /// </summary>
        /// <returns>String Array of Solicits Offered</returns>
        string[] GetSolicits();

        /// <summary>
        /// Test whether Domain Name exists
        /// </summary>
        /// <param name="domainName">Name of Domain</param>
        /// <returns>true if Domain Name exists, false otherwise</returns>
        bool TestDomainName(string domainName);

        /// <summary>
        /// GetServices with ServiceType = 'DOMAIN_NAME'
        /// </summary>
        /// <param name="domainName">Name of Domain</param>
        /// <returns>String Array of Operations Available in that Domain</returns>
        string[] GetOpNamesFromDomain(string domainName);

        /// <summary>
        /// Get List of Domains that can be Used in GetServices Calls on this Node
        /// </summary>
        /// <returns>ArrayList of Domain Name</returns>
        ArrayList RetrieveGetServicesOperationNames();
        /// <summary>
        /// Get a DataTable which contains information for ENDS.
        /// </summary>
        /// <returns>The array of domain name</returns>
        DataTable GetServiceForENDS();
    }
}
