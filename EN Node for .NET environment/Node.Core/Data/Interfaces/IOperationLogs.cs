using System;
using System.Collections;
using System.Data;

using Node.Core.Biz.Objects;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve OperationLogs information.
    /// </summary>
    public interface IOperationLogs
    {
        /// <summary>
        /// Search Operation Logs Table
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="opType">Operation Type, null or empty string if not supplied</param>
        /// <param name="wsID">Web Service ID, less than 0 if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, WEB_SERVICE_NAME, DOMAIN_NAME,
        ///     USER_NAME, TRANS_ID, STATUS_CD, CREATED_DTTM
        /// </returns>
        DataTable SearchOperationLogs(string opName, string opType, int wsID, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin);

        /// <summary>
        /// Search Operation Logs Table
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, DOMAIN_NAME,
        ///     USER_NAME, TRANS_ID, STATUS_CD, CREATED_DTTM, MESSAGE
        /// </returns>
        DataTable SearchOperationTasks(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin);
        
        /// <summary>
        /// Search Operation Logs Table
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, DOMAIN_NAME,
        ///     USER_NAME, TRANS_ID, STATUS_CD, CREATED_DTTM, MESSAGE
        /// </returns>
        DataSet SearchNotifications(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin);

        /// <summary>
        /// Get Web Service ID-Name Pairs
        /// </summary>
        /// <returns>
        /// Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME
        /// </returns>
        DataTable GetWebServiceIDNamePairs();

        /// <summary>
        /// Get Unique Operation Log Statuses
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is logged in</param>
        /// <returns>
        /// Unique Statuses of Operation Logs
        /// </returns>
        ArrayList GetUniqueOpStatuses(string domainAdmin);

        /// <summary>
        /// Get the Business Operation Log Object from the Database
        /// </summary>
        /// <param name="opLogID">The Operation Log ID of the Operation Log</param>
        /// <returns>The OperationLog Business Object</returns>
        OperationLog GetOperationLog(int opLogID);

        /// <summary>
        /// Gets the operation status by specified transaction id.
        /// </summary>
        /// <param name="transID">The specified transaction id.</param>
        /// <param name="status">The status code.</param>
        /// <returns>An ArrayList contains the status and status messages.</returns>
        ArrayList GetOperationStatus(string transID, string status);
    }
}
