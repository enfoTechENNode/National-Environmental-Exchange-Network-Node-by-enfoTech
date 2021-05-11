using System;
using System.Data;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieving information from NODE_WEB_SERVICE table in Node Database.
    /// </summary>
    public interface IWebServices
    {
        /// <summary>
        /// Get the List of Web Services
        /// </summary>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        DataTable GetWebServicesList();

        /// <summary>
        /// Get the List of Web Services that this Domain can create Operations For
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        DataTable GetWebServicesList(string domain);

                /// <summary>
        /// Get the List of Web Services Version 1.1 that this Domain can create Operations For
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        DataTable GetWebServicesListVer11(string domain);
    }
}
