using System;
using System.Collections;
using System.Data;

using Node.Core;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// Database Object for accesssing NODE_OPERATION_LOG.
    /// </summary>
    public class Logging : BaseData, ILogging
    {
        /// <summary>
        /// Constructor of Logging.
        /// </summary>
        public Logging()
        {

        }
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
        public int CreateOperationLog(int opID, string transID, string userName, string status, string message,
            string requestorIP, string suppliedTransID, string token, string nodeAddress, string returnURL,
            string serviceType, string hostName, string[] paramNames, object[] paramValues)
        {
            if (opID < 0 || transID == null || transID.Equals(""))
                return -1;
            int opLogID = -1;
            DBAdapter db = null;
            try
            {
                int newOpLogID = -1;
                string command = "select * from " + this.TblOperationLog + " where " + this.OpLogID + " = -1";
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperationLog, command, ds);
                if (ds != null && ds.Tables.Contains(this.TblOperationLog))
                {
                    // Insert Record into OPERATION_LOG
                    DataRow newRow = ds.Tables[this.TblOperationLog].NewRow();
                    newOpLogID = this.GetSequenceNumber(this.TblOperationLog, this.OpLogID);
                    if (newOpLogID >= 0)
                        newRow[this.OpLogID] = newOpLogID;
                    else
                        throw new Exception("Sequence Number Generation Error");
                    newRow[this.TransID] = transID;
                    newRow[this.OpID] = opID;
                    newRow[this.UserName] = userName;
                    newRow[this.ReqIP] = requestorIP;
                    newRow[this.SupplTransID] = suppliedTransID;
                    newRow[this.Token] = token;
                    newRow[this.NodeAddr] = nodeAddress;
                    newRow[this.RetURL] = returnURL;
                    newRow[this.ServiceType] = serviceType;
                    newRow[this.StartDate] = DateTime.Now;
                    newRow[this.HostName] = hostName;
                    newRow[this.CreatedDate] = DateTime.Now;
                    newRow[this.CreatedBy] = userName;
                    newRow[this.UpdatedDate] = DateTime.Now;
                    newRow[this.UpdatedBy] = userName;
                    ds.Tables[this.TblOperationLog].LoadDataRow(newRow.ItemArray, false);
                    int num = db.UpdateDataSet(this.TblOperationLog, ds);
                }
                if (paramNames != null && paramNames.Length > 0 && paramValues != null && paramValues.Length == paramNames.Length)
                {
                    command = "select * from " + this.TblOperationLogParam + " where 1 = -1";
                    db.GetDataSet(this.TblOperationLogParam, command, ds);
                    if (ds != null && ds.Tables.Contains(this.TblOperationLogParam))
                    {
                        for (int i = 0; i < paramNames.Length; i++)
                        {
                            DataRow newRow = ds.Tables[this.TblOperationLogParam].NewRow();
                            newRow[this.OpLogID] = newOpLogID;
                            newRow[this.ParamName] = paramNames[i];
                            newRow[this.ParamValue] = paramValues[i];
                            ds.Tables[this.TblOperationLogParam].LoadDataRow(newRow.ItemArray, false);
                        }
                        db.UpdateDataSet(this.TblOperationLogParam, ds);
                    }
                }
                command = "select * from " + this.TblOperationLogStatus + " where 1 = -1";
                db.GetDataSet(this.TblOperationLogStatus, command, ds);
                if (ds != null && ds.Tables.Contains(this.TblOperationLogStatus))
                {
                    DataRow newRow = ds.Tables[this.TblOperationLogStatus].NewRow();
                    newRow[this.OpLogStatusID] = this.GetSequenceNumber(this.TblOperationLogStatus, this.OpLogStatusID);
                    newRow[this.OpLogID] = newOpLogID;
                    newRow[this.Status] = status;
                    newRow[this.Message] = message;
                    newRow[this.CreatedDate] = DateTime.Now;
                    newRow[this.CreatedBy] = userName;
                    ds.Tables[this.TblOperationLogStatus].LoadDataRow(newRow.ItemArray, false);
                    db.UpdateDataSet(this.TblOperationLogStatus, ds);
                }
                if (newOpLogID >= 0)
                    opLogID = newOpLogID;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return opLogID;
        }

        /*
                public int UpdateOperationLog(int opLogID, string status, string message, string userName, bool isLastUpdate)
                {
                    return this.UpdateOperationLog(opLogID, status, message, userName, isLastUpdate);
                }

                public int UpdateOperationLog(string transID, string status, string message, string userName, bool isLastUpdate)
                {
                    return this.UpdateOperationLog(-1, transID, status, message, userName, isLastUpdate);
                }
                */
        /// <summary>
        /// Insert the User Name of the Transaction Log
        /// </summary>
        /// <param name="transID">The Transaction ID of the Transaction</param>
        /// <param name="userName">The User Name to Insert</param>
        /// <returns>Operation Log ID</returns>
        public int UpdateOperationLogUserName(string transID, string userName)
        {
            int opLogID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select " + this.OpLogID + ", " + this.UserName + ", " + this.UpdatedDate;
                command += ", " + this.UpdatedBy + " from " + this.TblOperationLog + " where ";
                command += this.TransID + " = @" + this.TransID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLog, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    object obj = dt.Rows[0][this.OpLogID];
                    if (obj != null)
                    {
                        System.Int32.TryParse("" + obj, out opLogID);
                        dt.Rows[0][this.UserName] = userName;
                        dt.Rows[0][this.UpdatedDate] = DateTime.Now;
                        dt.Rows[0][this.UpdatedBy] = userName;
                        db.UpdateDataTable(this.TblOperationLog, dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return opLogID;
        }
        /// <summary>
        /// Insert the Token of the Transaction Log
        /// </summary>
        /// <param name="transID">The Transaction ID of the Transaction</param>
        /// <param name="token">The Token to Insert</param>
        /// <param name="userName">The User Associated with the Transaction</param>
        public void UpdateOperationLogToken(string transID, string token, string userName)
        {
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "update " + this.TblOperationLog + " set " + this.Token + " = @" + this.Token;
                command += ", " + this.UpdatedDate + " = @" + this.UpdatedDate + ", " + this.UpdatedBy;
                command += " = @" + this.UpdatedBy + " where " + this.TransID + " = @" + this.TransID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.Token, token));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.UpdatedBy, userName));
                parameters.Add(new Parameter(this.TransID, transID));
                db.ExecuteNonQuery(command, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        /// <summary>
        /// Insert the Token of the Transaction Log
        /// </summary>
        /// <param name="opLogID">The operation log ID of the Transaction</param>
        /// <param name="transID">The Transaction ID of the Transaction</param>
        /// <param name="token">The Token to Insert</param>
        /// <param name="userName">The User Associated with the Transaction</param>
        public void UpdateOperationLogToken(int opLogID, string transID, string token, string userName)
        {
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "update " + this.TblOperationLog + " set " + this.TransID + " = @" + this.TransID + ", " + this.Token + " = @" + this.Token;
                command += ", " + this.UpdatedDate + " = @" + this.UpdatedDate + ", " + this.UpdatedBy;
                command += " = @" + this.UpdatedBy + " where " + this.OpLogID + " = " + opLogID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.Token, token));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.UpdatedBy, userName));
                parameters.Add(new Parameter(this.TransID, transID));
                db.ExecuteNonQuery(command, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
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
            if (opLogID < 0)
                return -1;
            int retID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select " + this.OpLogID + ", " + this.EndDate + ", " + this.UpdatedDate;
                command += ", " + this.UpdatedBy + " from " + this.TblOperationLog + " where ";
                command += this.OpLogID + " = " + opLogID;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLog, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    object obj = dt.Rows[0][this.OpLogID];
                    int result = -1;
                    System.Int32.TryParse(obj + "", out result);
                    retID = result;
                    if (isLastUpdate)
                    {
                        command = "update " + this.TblOperationLog + " set " + this.EndDate + " = @" + this.EndDate;
                        command += ", " + this.UpdatedDate + " = @" + this.UpdatedDate + ", " + this.UpdatedBy;
                        command += " = '" + userName + "' where " + this.OpLogID + " = @" + this.OpLogID;
                        ArrayList parameters = new ArrayList();
                        parameters.Add(new Parameter(this.OpLogID, opLogID));
                        parameters.Add(new Parameter(this.EndDate, DateTime.Now));
                        parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                        db.ExecuteNonQuery(command, parameters);
                    }
                    command = "select " + this.OpLogStatusID + ", " + this.OpLogID + ", ";
                    command += this.Status + ", " + this.Message + ", " + this.CreatedDate + ", ";
                    command += this.CreatedBy + " from " + this.TblOperationLogStatus;
                    command += " where " + this.OpLogID + " = -1";
                    dt = new DataTable();
                    db.GetDataTable(this.TblOperationLogStatus, command, dt);
                    if (dt != null)
                    {
                        int nextID = this.GetSequenceNumber(this.TblOperationLogStatus, this.OpLogStatusID);
                        DataRow row = dt.NewRow();
                        row[this.OpLogStatusID] = nextID;
                        row[this.OpLogID] = retID;
                        row[this.Status] = status;
                        row[this.Message] = message;
                        row[this.CreatedDate] = DateTime.Now;
                        row[this.CreatedBy] = userName;
                        dt.LoadDataRow(row.ItemArray, false);
                        db.UpdateDataTable(this.TblOperationLogStatus, dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retID;
        }
        /// <summary>
        /// Get Latest Status of a Transaction
        /// </summary>
        /// <param name="transID">Transaction ID of the Transaction</param>
        /// <returns>Status String</returns>
        public string GetLatestStatus(string transID)
        {
            string retStatus = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select " + this.Status + " from " + this.TblOperationLogStatus;
                command += " where " + this.OpLogStatusID + " = (select max(A." + this.OpLogStatusID;
                command += ") from " + this.TblOperationLogStatus + " A, " + this.TblOperationLog;
                command += " B where B." + this.TransID + " = @" + this.TransID + " and A.";
                command += this.OpLogID + " = B." + this.OpLogID + ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLogStatus, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retStatus = "" + dt.Rows[0][this.Status];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
            return retStatus;
        }
        /// <summary>
        /// Checks to see if Log has an End Date on it
        /// </summary>
        /// <param name="transID">Transaction ID of the Transaction</param>
        /// <returns>true if has end date, false otherwise</returns>
        public bool HasEndDate(string transID)
        {
            bool hasEndDate = false;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.EndDate;
                command += " from " + this.TblOperationLog + " A";
                command += " where A." + this.TransID + " = @" + this.TransID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLog, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    object obj = dt.Rows[0][this.EndDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        hasEndDate = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
            return hasEndDate;
        }
        /// <summary>
        /// Look up Token in previous Authenticate Request and Copy User Name over
        /// </summary>
        /// <param name="token">Token of Authenticate Request</param>
        /// <param name="transID">Transaction to Update</param>
        /// <returns>Operation Log ID</returns>
        public int CopyUserFromToken(string token, string transID)
        {
            int retID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.UserName + " from " + this.TblOperationLog + " A, ";
                command += this.TblOperation + " B, " + this.TblWebService + " C";
                command += " where A." + this.Token + " = @" + this.Token;
                command += " and A." + this.OpID + " = B." + this.OpID;
                command += " and B." + this.WSID + " = C." + this.WSID;
                command += " and C." + this.WSName + " = '" + Phrase.WEB_SERVICE_AUTHENTICATE + "'";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.Token, token));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLog, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string userName = "" + dt.Rows[0][this.UserName];
                    command = "select " + this.UserName + ", " + this.OpLogID + " from ";
                    command += this.TblOperationLog + " where " + this.TransID + " = @" + this.TransID;
                    parameters = new ArrayList();
                    parameters.Add(new Parameter(this.TransID, transID));
                    dt = new DataTable();
                    db.GetDataTable(this.TblOperationLog, command, parameters, dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Rows[0][this.UserName] = userName;
                        db.UpdateDataTable(this.TblOperationLog, dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retID;
        }
        /// <summary>
        /// Gets the DataTable with a single row that contains the Status of the Task and the last time that the task executed
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns>DataTable(STATUS_CD, START_DTTM)</returns>
        public DataTable GetLatestTaskStatus(string taskName)
        {
            DataTable retTable = null;
            DBAdapter db = null;
            try
            {
                string sql = "select C." + this.Status + ", B." + this.StartDate;
                sql += " from " + this.TblOperation + " A, " + this.TblOperationLog + " B";
                sql += ", " + this.TblOperationLogStatus + " C";
                sql += " where A." + this.OpName + " = @" + this.OpName;
                sql += " and A." + this.OpType + " = '" + Phrase.OPERATION_TYPE_SCHEDULED_TASK + "'";
                sql += " and A." + this.OpStatus + " = '" + Phrase.STATUS_RUNNING + "'";
                sql += " and B." + this.OpLogID + " = (select max(D." + this.OpLogID + ")";
                sql += " from " + this.TblOperationLog + " D where D." + this.OpID + " = A." + this.OpID + ")";
                sql += " and C." + this.OpLogStatusID + " = (select max(E." + this.OpLogStatusID + ")";
                sql += " from " + this.TblOperationLogStatus + " E where E." + this.OpLogID + " = B." + this.OpLogID + ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.OpName, taskName));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperationLog, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblOperationLog))
                    retTable = ds.Tables[this.TblOperationLog];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retTable;
        }
        /// <summary>
        /// Get Last Task Executed Date by Task Name and Status.
        /// </summary>
        /// <param name="taskName">Name of Task or Operation</param>
        /// <param name="statusCD">Specified status value</param>
        /// <returns>last executed date in string format</returns>
        public string GetLastExecDate(string taskName, string statusCD)
        {
            string date = null;
            DBAdapter db = null;
            try
            {
                //string sql = "select C." + this.CreatedDate;
                //sql += " from " + this.TblOperation + " A, " + this.TblOperationLog + " B";
                //sql += ", " + this.TblOperationLogStatus + " C";
                //sql += " where A." + this.OpName + " = @" + this.OpName;
                //sql += " and A." + this.OpType + " = '" + Phrase.OPERATION_TYPE_SCHEDULED_TASK + "'";
                //sql += " and B." + this.OpLogID + " = (select max(D." + this.OpLogID + ")";
                //sql += " from " + this.TblOperationLog + " D where D." + this.OpID + " = A." + this.OpID + ")";
                //sql += " and C." + this.Status + " = @"+ this.Status;
                //sql += " and C." + this.OpLogStatusID + " = (select max(E." + this.OpLogStatusID + ")";
                //sql += " from " + this.TblOperationLogStatus + " E where E." + this.OpLogID + " = B." + this.OpLogID + ")";

                string sql = "select C." + this.CreatedDate + " from " + this.TblOperationLog + " B inner join ";
                sql += this.TblOperationLogStatus + " C on B." + this.OpLogID + " = C." + this.OpLogID + " inner join ";
                sql += this.TblOperation + " A on B." + this.OpID + " = A." + this.OpID + " where A." + this.OpName + " = @" + this.OpName;
                sql += " and C." + this.Status + " = @"+ this.Status + " order by C." + this.CreatedDate +" desc";

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.OpName, taskName));
                parameters.Add(new Parameter(this.Status, statusCD));
                db = this.GetNodeDB();
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLogStatus, sql, parameters, dt);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                    date = dt.Rows[0][this.CreatedDate].ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
            return date;
        }
        /// <summary>
        /// Get Last Task submitted Date by Task Name and Status.
        /// </summary>
        /// <param name="taskName">Name of Task or Operation</param>
        /// <param name="statusCD">Specified status value</param>
        /// <returns>Two value return:OpLogID,OpLogStatusMsg</returns>
        public string[] GetLastSubmittedTask(string taskName, string statusCD)
        {
            string[] ID = { "", "" };

            DBAdapter db = null;
            try
            {
                string sql = "select C." + this.CreatedDate + ", C." + this.OpLogStatusMsg + ", B." + this.OpLogID + " from " + this.TblOperationLog + " B inner join ";
                sql += this.TblOperationLogStatus + " C on B." + this.OpLogID + " = C." + this.OpLogID + " inner join ";
                sql += this.TblOperation + " A on B." + this.OpID + " = A." + this.OpID + " where A." + this.OpName + " = @" + this.OpName;
                sql += " and C." + this.Status + " = @" + this.Status + " order by C." + this.CreatedDate + " desc";

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.OpName, taskName));
                parameters.Add(new Parameter(this.Status, statusCD));
                db = this.GetNodeDB();
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLogStatus, sql, parameters, dt);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    ID[0] = dt.Rows[0][this.OpLogStatusMsg].ToString();
                    ID[1] = dt.Rows[0][this.OpLogID].ToString();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
            return ID;
        }
    }
}
