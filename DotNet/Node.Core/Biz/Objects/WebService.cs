using System;
using System.Data;

using Node.Core.Data;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// WebService Class retrieves WebService Informatuion.
    /// Database Source: NODE_WEB_SERVICE.
    /// </summary>
    public class WebService
    {
        #region Public Static Methods
        /// <summary>
        /// Get the List of Web Services
        /// </summary>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public static DataTable GetWebServicesList()
        {
            DataTable dt = new DBManager().GetWebServicesDB().GetWebServicesList();
            DataRow dr = dt.NewRow();
            dr["WEB_SERVICE_ID"] = -1;
            dr["WEB_SERVICE_NAME"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        /// <summary>
        /// Get the List of Web Services that this Domain can create Operations For
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public static DataTable GetWebServicesList(string domain)
        {
            return new DBManager().GetWebServicesDB().GetWebServicesList(domain);
        }
        /// <summary>
        /// Get the List of Web Services that this Domain can create Operations For
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public static DataTable GetWebServicesListVer11(string domain)
        {
            return new DBManager().GetWebServicesDB().GetWebServicesListVer11(domain);
        }

        #endregion
    }
}
