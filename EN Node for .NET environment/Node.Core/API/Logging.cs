using System;
using System.Collections.Generic;
using System.Text;

using Node.Core.Data;
using Node.Core.Data.Interfaces;
using System.Data;

namespace Node.Core.API
{
    /// <summary>
    /// Database object for access NODE_OPERATION_LOG.
    /// </summary>
    public class Logging
    {
        ILogging logDB;
        /// <summary>
        /// Construcator of Logging.
        /// </summary>
        public Logging()
        {
            logDB = new DBManager().GetLoggingDB();
        }

        /// <summary>
        /// Update the Status of an Operation Log
        /// </summary>
        /// <param name="opLogID">Operation Log ID of the Transaction</param>
        /// <param name="status">The New Status of the Transaction</param>
        /// <param name="message">The New Status message of the Transaction</param>
        /// <param name="userName">The User Associated with the Transaction</param>
        /// <param name="isLastUpdate">true if Update is the Last Update of the Transaction, false otherwise</param>
        /// <returns>Operation Log ID</returns>
        public int UpdateOperationLog(int opLogID, string status, string message, string userName, bool isLastUpdate)
        {
            //ILogging logDB = new DBManager().GetLoggingDB();
            return logDB.UpdateOperationLog(opLogID, status, message, userName, isLastUpdate);                        
        }

        /// <summary>
        /// Get Latest Task Status and execution time
        /// </summary>
        /// <param name="taskName">Task/Operation Name</param>
        /// <returns>table</returns>
        public DataTable GetLatestTaskStatus(string taskName)
        {
            return logDB.GetLatestTaskStatus(taskName);
        }

       /// <summary>
       /// Get Last Tast Execution Date
       /// </summary>
       /// <param name="taskName"></param>
       /// <param name="statusCD"></param>
       /// <returns>date string</returns>
        public string GetLastExecDate(string taskName, string statusCD)
        {
            return logDB.GetLastExecDate(taskName, statusCD);
        }

        /// <summary>
        /// Get Last Tast Execution Date
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="statusCD"></param>
        /// <returns>date string</returns>
        public string[] GetLastSubmittedTaskTransID(string taskName, string statusCD)
        {
            return logDB.GetLastSubmittedTask(taskName, statusCD);
        }
    }
}
