using System;
using System.Data;
//using System.Data.Common;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve OperationLogs information.
    /// </summary>
    public interface ILogging
    {
        /// <summary>
        /// Create Operation Log
        /// Not all Parameters are Required
        /// </summary>
        /// <param name="opID">Operation ID (Required)</param>
        /// <param name="transID">Transaction ID (Required)</param>
        /// <param name="userName">User Name of the Request</param>
        /// <param name="status">Status of transaction</param>
        /// <param name="message">Message of transaction</param>
        /// <param name="requestorIP">Requestor's IP Address</param>
        /// <param name="suppliedTransID">Supplied Transaction ID</param>
        /// <param name="token">Security Token of Address</param>
        /// <param name="nodeAddress">Node Address (Notify)</param>
        /// <param name="returnURL">Return URL (Solicit)</param>
        /// <param name="serviceType">Service Type (GetServices)</param>
        /// <param name="hostName">Host Name</param>
        /// <param name="paramNames">Input Parameter Names</param>
        /// <param name="paramValues">Input Parameter Values</param>
        /// <returns>Operation Log ID</returns>
        int CreateOperationLog(int opID, string transID, string userName, string status, string message,
            string requestorIP, string suppliedTransID, string token, string nodeAddress, string returnURL,
            string serviceType, string hostName, string[] paramNames, object[] paramValues);

        /// <summary>
        /// Update the Status of an Operation Log
        /// </summary>
        /// <param name="opLogID">Operation Log ID of the Transaction</param>
        /// <param name="status">The New Status of the Transaction</param>
        /// <param name="message">The New Status message of the Transaction</param>
        /// <param name="userName">The User Associated with the Transaction</param>
        /// <param name="isLastUpdate">true if Update is the Last Update of the Transaction, false otherwise</param>
        /// <returns>Operation Log ID</returns>
        int UpdateOperationLog(int opLogID, string status, string message, string userName, bool isLastUpdate);

        /// <summary>
        /// Insert the User Name of the Transaction Log
        /// </summary>
        /// <param name="transID">The Transaction ID of the Transaction</param>
        /// <param name="userName">The User Name to Insert</param>
        /// <returns>Operation Log ID</returns>
        int UpdateOperationLogUserName(string transID, string userName);

        /// <summary>
        /// Insert the Token of the Transaction Log
        /// </summary>
        /// <param name="transID">The Transaction ID of the Transaction</param>
        /// <param name="token">The Token to Insert</param>
        /// <param name="userName">The User Associated with the Transaction</param>
        void UpdateOperationLogToken(string transID, string token, string userName);

        /// <summary>
        /// Get Latest Status of a Transaction
        /// </summary>
        /// <param name="transID">Transaction ID of the Transaction</param>
        /// <returns>Status String</returns>
        string GetLatestStatus(string transID);

        /// <summary>
        /// Checks to see if Log has an End Date on it
        /// </summary>
        /// <param name="transID">Transaction ID of the Transaction</param>
        /// <returns>true if has end date, false otherwise</returns>
        bool HasEndDate(string transID);

        /// <summary>
        /// Look up Token in previous Authenticate Request and Copy User Name over
        /// </summary>
        /// <param name="token">Token of Authenticate Request</param>
        /// <param name="transID">Transaction to Update</param>
        /// <returns>Operation Log ID</returns>
        int CopyUserFromToken(string token, string transID);

        /// <summary>
        /// Gets the DataTable with a single row that contains the Status of the Task and the last time that the task executed
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns>DataTable(STATUS_CD, START_DTTM)</returns>
        DataTable GetLatestTaskStatus(string taskName);

        /// <summary>
        /// Get Last Task Executed Date by Task Name and Status.
        /// </summary>
        /// <param name="taskName">Name of Task or Operation</param>
        /// <param name="statusCD">Specified status value</param>
        /// <returns>last executed date in string format</returns>
        String GetLastExecDate(string taskName, string statusCD);

        /// <summary>
        /// Get Last Task submitted Date by Task Name and Status.
        /// </summary>
        /// <param name="taskName">Name of Task or Operation</param>
        /// <param name="statusCD">Specified status value</param>
        /// <returns>Two value return:OpLogID,OpLogStatusMsg</returns>
        String[] GetLastSubmittedTask(string taskName, string statusCD);
    }
}
