using System;
using System.Data;

using Node.Core.Biz.Objects;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve Domain Information
    /// </summary>
    public interface IDomains
    {
        /// <summary>
        /// Get a DataTable of Domains
        /// </summary>
        /// <param name="domainAdmin">LoginName of Domain Administrator</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME</returns>
        DataTable GetDomainDropDownList(string domainAdmin);

        /// <summary>
        /// Get the DataTable for the Domain Search Grid
        /// </summary>
        /// <param name="domainID">The id of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainStatus">The status of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged into the system.</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME, DOMAIN_STATUS_CD, DOMAIN_STATUS_MSG</returns>
        DataTable GetDomainSearchGrid(string domainID, string domainStatus, string domainAdmin);

        /// <summary>
        /// Get the Domain Business Object by the Input Parameter Domain's Name
        /// </summary>
        /// <param name="domainName">The name of the Domain</param>
        /// <returns>The Domain Business Object</returns>
        Domain GetDomain(string domainName);

        /// <summary>
        /// Save a Domain
        /// </summary>
        /// <param name="d">The Domain to Save</param>
        /// <param name="domainAdmin">The Domain Administrator logged into the System</param>
        void SaveDomain(Domain d, string domainAdmin);
        /// <summary>
        /// Check if console user is Node Domain Admin.
        /// </summary>
        /// <param name="userName">The LogIn UserName</param>
        /// <returns>True if is Node Domain Admin</returns>
        bool IsNodeDomainAdmin(string userName);
    }
}
