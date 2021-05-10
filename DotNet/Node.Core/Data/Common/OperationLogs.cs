using System;
using System.Collections;
using System.Data;
using System.Web;

using Node.Lib.Data;
using Node.Core.Biz.Objects;
using Node.Core.Data.Interfaces;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// Database object class for retrieve OperationLogs information.
    /// </summary>
    public class OperationLogs : BaseData, IOperationLogs
    {
        #region Public Constructors

        #endregion

        #region Public Methods

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
        /// <param name="startDate">Start Range for Log, "01/01/2000" if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.Now.AddMonths(6) if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, WEB_SERVICE_NAME, DOMAIN_NAME,
        ///     USER_NAME, TRANS_ID, STATUS_CD, CREATED_DTTM
        /// </returns>
        public DataTable SearchOperationLogs(string opName, string opType, int wsID, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            DataTable dt = null;
            DBAdapter db = null;
            string versionNo = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                    versionNo = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
                else
                    versionNo = Phrase.VERSION_11;

                ArrayList parameters = new ArrayList();
                string sql = "select distinct A." + this.OpLogID + ", B." + this.OpName + ", B." + this.OpType;
                sql += ", C." + this.WSName + ", D." + this.DomName;
                sql += ", A." + this.UserName + ", A." + this.TransID + ", A." + this.CreatedDate + " as LOG_CREATED_DATE";
                sql += ", A." + this.EndDate;
                sql += ", E." + this.Status + ", E." + this.CreatedDate;
                sql += " from " + this.TblOperationLog + " A, " + this.TblOperation + " B";
                sql += " left join " + this.TblWebService + " C on B." + this.WSID + " = C." + this.WSID;
                sql += ", " + this.TblDomain + " D, " + this.TblOperationLogStatus + " E";
                sql += ", " + this.TblUser + " F, " + this.TblAccTypeXREF + " G, " + this.TblAccType + " H";
                sql += ", " + this.TblDomain + " I";
                sql += " where A." + this.OpID + " = B." + this.OpID;
                sql += " and B." + this.OpVersionNo + " = '" + versionNo + "'";
                sql += " and B." + this.DomID + " = D." + this.DomID;
                sql += " and E." + this.OpLogStatusID + " = (select max(F." + this.OpLogStatusID + ")";
                sql += " from " + this.TblOperationLogStatus + " F where A." + this.OpLogID + " = F." + this.OpLogID + ")";
                sql += " and F." + this.LoginName + " = @" + this.LoginName + " and F." + this.UserID + " = G." + this.UserID;
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                sql += " and G." + this.AccTypeID + " = H." + this.AccTypeID + " and H." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and G." + this.DomID + " = I." + this.DomID;
                //sql += " and ((I." + this.DomName + " = 'NODE') or (I." + this.DomID + " = D." + this.DomID + "))";
                sql += " and I." + this.DomID + " = D." + this.DomID;
                if (opName != null && !opName.Trim().Equals(""))
                {
                    sql += " and B." + this.OpName + " = @" + this.OpName;
                    parameters.Add(new Parameter(this.OpName, opName));
                }
                if (opType != null && !opType.Trim().Equals(""))
                {
                    sql += " and B." + this.OpType + " = @" + this.OpType;
                    parameters.Add(new Parameter(this.OpType, opType));
                }
                if (wsID >= 0)
                {
                    sql += " and C." + this.WSID + " = @" + this.WSID;
                    parameters.Add(new Parameter(this.WSID, wsID));
                }
                if (status != null && !status.Trim().Equals(""))
                {
                    sql += " and E." + this.Status + " = @" + this.Status;
                    parameters.Add(new Parameter(this.Status, status));
                }
                if (domainID >= 0)
                {
                    sql += " and D." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domainID));
                }
                if (userName != null && !userName.Trim().Equals(""))
                {
                    sql += " and A." + this.UserName + " = @" + this.UserName;
                    parameters.Add(new Parameter(this.UserName, userName));
                }
                if (token != null && !token.Trim().Equals(""))
                {
                    sql += " and A." + this.Token + " = @" + this.Token;
                    parameters.Add(new Parameter(this.Token, token));
                }
                if (transID != null && !transID.Trim().Equals(""))
                {
                    sql += " and A." + this.TransID + " = @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, transID));
                }
                if (startDate.CompareTo(DateTime.MinValue) > 0)
                {
                    sql += " and E." + this.CreatedDate + " > @" + this.CreatedDate;
                    parameters.Add(new Parameter(this.CreatedDate, startDate));
                }
                if (endDate.CompareTo(DateTime.MaxValue) < 0)
                {
                    sql += " and A." + this.StartDate + " < @" + this.StartDate;
                    parameters.Add(new Parameter(this.StartDate, endDate.AddDays(1.0)));
                }
                sql += " order by A." + this.OpLogID + " desc";
                
                db = this.GetNodeDB();
                if (HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM] != null
                    && HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString() != "-1")
                {
                    string rownum = HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString();
                    if (db.ProviderName.Trim() == DBAdapter.MSSQL_Provider)
                    {
                        sql = sql.Replace("select distinct", "select distinct top " + rownum + " ");
                    }
                    else
                    {
                        sql = "SELECT * FROM (" + sql + ")WHERE ROWNUM <=" + rownum;
                    }
                }
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblOperationLog, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblOperationLog))
                    dt = ds.Tables[this.TblOperationLog];
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
            return dt;
        }

        /// <summary>
        /// Search Operation Logs Table
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, "01/01/2000" if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.Now.AddMonths(6) if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, DOMAIN_NAME,
        ///     USER_NAME, TRANS_ID, STATUS_CD, CREATED_DTTM, MESSAGE
        /// </returns>
        public DataTable SearchOperationTasks(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            DataTable dt = null;
            DBAdapter db = null;
            string versionNo = string.Empty;
            try
            {
                db = this.GetNodeDB();
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                    versionNo = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
                else
                    versionNo = Phrase.VERSION_11;

                ArrayList parameters = new ArrayList();
                string sql = "select distinct A." + this.OpLogID + ", B." + this.OpName;
                sql += ", D." + this.DomName + ", A." + this.UserName;
                sql += ", A." + this.TransID + ", E." + this.OpLogStatusID;
                sql += ", E." + this.Status + ", E." + this.CreatedDate;
                //str(E.OPERATION_LOG_STATUS_ID) + '_' + str(A.OPERATION_LOG_ID) as LOG_AND_STATUS_IDS 
                if (db.ProviderName.Trim() == DBAdapter.MSSQL_Provider)
                    sql += ", str(E." + this.OpLogStatusID + ") + '_' + str(A." + this.OpLogID + ") as LOG_AND_STATUS_IDS";
                else
                    sql += ", E." + this.OpLogStatusID + " || '_' || A." + this.OpLogID + " as LOG_AND_STATUS_IDS";
                sql += " from " + this.TblOperationLog + " A, " + this.TblOperation + " B";
                sql += ", " + this.TblDomain + " D, " + this.TblOperationLogStatus + " E";
                sql += ", " + this.TblUser + " F, " + this.TblAccTypeXREF + " G, " + this.TblAccType + " H";
                sql += ", " + this.TblDomain + " I";
                sql += " where A." + this.OpID + " = B." + this.OpID;
                sql += " and B." + this.OpVersionNo + " = '" + versionNo + "'";
                sql += " and B." + this.OpType + " = '" + Phrase.OPERATION_TYPE_SCHEDULED_TASK + "'";
                sql += " and B." + this.DomID + " = D." + this.DomID;
                sql += " and E." + this.OpLogID + " = A." + this.OpLogID;
                sql += " and F." + this.LoginName + " = @" + this.LoginName + " and F." + this.UserID + " = G." + this.UserID;
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                sql += " and G." + this.AccTypeID + " = H." + this.AccTypeID + " and H." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and G." + this.DomID + " = I." + this.DomID;
                //sql += " and ((I." + this.DomName + " = 'NODE') or (I." + this.DomID + " = D." + this.DomID + "))";
                sql += " and I." + this.DomID + " = D." + this.DomID;
                if (opName != null && !opName.Trim().Equals(""))
                {
                    sql += " and B." + this.OpName + " = @" + this.OpName;
                    parameters.Add(new Parameter(this.OpName, opName));
                }
                if (status != null && !status.Trim().Equals(""))
                {
                    sql += " and E." + this.Status + " = @" + this.Status;
                    parameters.Add(new Parameter(this.Status, status));
                }
                if (domainID >= 0)
                {
                    sql += " and D." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domainID));
                }
                if (userName != null && !userName.Trim().Equals(""))
                {
                    sql += " and A." + this.UserName + " = @" + this.UserName;
                    parameters.Add(new Parameter(this.UserName, userName));
                }
                if (token != null && !token.Trim().Equals(""))
                {
                    sql += " and A." + this.Token + " = @" + this.Token;
                    parameters.Add(new Parameter(this.Token, token));
                }
                if (transID != null && !transID.Trim().Equals(""))
                {
                    sql += " and A." + this.TransID + " = @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, transID));
                }
                if (startDate.CompareTo(DateTime.MinValue) > 0)
                {
                    sql += " and E." + this.CreatedDate + " > @" + this.CreatedDate;
                    parameters.Add(new Parameter(this.CreatedDate, startDate));
                }
                if (endDate.CompareTo(DateTime.MaxValue) < 0)
                {
                    sql += " and A." + this.StartDate + " < @" + this.StartDate;
                    parameters.Add(new Parameter(this.StartDate, endDate.AddDays(1.0)));
                }
                sql += " order by E." + this.OpLogStatusID + " desc";

                if (HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM] != null
                    && HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString() != "-1")
                {
                    string rownum = HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString();
                    if (db.ProviderName.Trim() == DBAdapter.MSSQL_Provider)
                    {
                        sql = sql.Replace("select distinct", "select distinct top " + rownum + " ");
                    }
                    else
                    {
                        sql = "SELECT * FROM (" + sql + ")WHERE ROWNUM <=" + rownum;
                    }
                }
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblOperationLog, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblOperationLog))
                    dt = ds.Tables[this.TblOperationLog];
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
            return dt;
        }

        /// <summary>
        /// Search Node_Operation_Log_Parameter Table
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, "01/01/2000" if not supplied</param>
        /// <param name="endDate">End Range for Log, DateTime.Now.AddMonths(6) if not supplied</param>
        /// <param name="domainAdmin">Administration User who is logged in</param>
        /// <returns>
        /// Columns: OPERATION_LOG_ID, OPERATION_NAME, PARAMETER_NAME,
        ///     PARAMETER_VALUE
        /// </returns>
        public DataSet SearchNotifications(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            DBAdapter db = null;
            DataSet ds = new DataSet();
            string versionNo = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                    versionNo = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
                else
                    versionNo = Phrase.VERSION_11;

                ArrayList parameters = new ArrayList();
                string sql = "select distinct A." + this.OpLogID + ", A." + this.NodeAddr + ", C." + this.ParamValue + " as DATA_FLOW, A." + this.CreatedDate;
                sql += " from " + this.TblOperationLog + " A, " + this.TblOperation + " B, " + this.TblOperationLogParam;
                sql += " C, " + this.TblDomain + " D, " + this.TblOperationLogStatus + " E";
                sql += ", " + this.TblUser + " F, " + this.TblAccTypeXREF + " G, " + this.TblAccType + " H";
                sql += ", " + this.TblDomain + " I";
                sql += " where A." + this.OpID + " = B." + this.OpID;
                sql += " and B." + this.OpVersionNo + " = '" + versionNo + "'";
                sql += " and B." + this.OpType + " = '" + Phrase.OPERATION_TYPE_WEB_SERVICE + "'";
                sql += " and B." + this.DomID + " = D." + this.DomID;
                sql += " and B." + this.WSID + " = 5";//THIS MEANS NOTIFY
                sql += " and C." + this.OpLogID + " = A." + this.OpLogID;
                sql += " and E." + this.OpLogID + " = A." + this.OpLogID;
                sql += " and C." + this.ParamName + " = '" + Phrase.NP_DATA_FLOW + "'";
                sql += " and F." + this.LoginName + " = @" + this.LoginName + " and F." + this.UserID + " = G." + this.UserID;
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                sql += " and G." + this.AccTypeID + " = H." + this.AccTypeID + " and H." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and G." + this.DomID + " = I." + this.DomID;
                //sql += " and ((I." + this.DomName + " = 'NODE') or (I." + this.DomID + " = D." + this.DomID + "))";
                sql += " and I." + this.DomID + " = D." + this.DomID;
                if (opName != null && !opName.Trim().Equals(""))
                {
                    sql += " and B." + this.OpName + " = @" + this.OpName;
                    parameters.Add(new Parameter(this.OpName, opName));
                }
                if (status != null && !status.Trim().Equals(""))
                {
                    sql += " and E." + this.Status + " = @" + this.Status;
                    parameters.Add(new Parameter(this.Status, status));
                }
                if (domainID >= 0)
                {
                    sql += " and D." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domainID));
                }
                if (userName != null && !userName.Trim().Equals(""))
                {
                    sql += " and A." + this.UserName + " = @" + this.UserName;
                    parameters.Add(new Parameter(this.UserName, userName));
                }
                if (token != null && !token.Trim().Equals(""))
                {
                    sql += " and A." + this.Token + " = @" + this.Token;
                    parameters.Add(new Parameter(this.Token, token));
                }
                if (transID != null && !transID.Trim().Equals(""))
                {
                    sql += " and A." + this.TransID + " = @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, transID));
                }
                if (startDate.CompareTo(DateTime.MinValue) > 0)
                {
                    sql += " and E." + this.CreatedDate + " > @" + this.CreatedDate;
                    parameters.Add(new Parameter(this.CreatedDate, startDate));
                }
                if (endDate.CompareTo(DateTime.MaxValue) < 0)
                {
                    sql += " and A." + this.StartDate + " < @" + this.StartDate;
                    parameters.Add(new Parameter(this.StartDate, endDate.AddDays(1.0)));
                }
                sql += " order by A." + this.OpLogID + " desc";

                db = this.GetNodeDB();
                if (HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM] != null
                    && HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString() != "-1")
                {
                    string rownum = HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString();
                    if (db.ProviderName.Trim() == DBAdapter.MSSQL_Provider)
                    {
                        sql = sql.Replace("select distinct", "select distinct top " + rownum + " ");
                    }
                    else
                    {
                        sql = "SELECT * FROM (" + sql + ")WHERE ROWNUM <=" + rownum;
                    }
                }
                
                db.GetDataSet(this.TblOperationLog, sql, parameters, ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string ids = string.Empty;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                        ids += "," + dr[0];
                    if (ids.Length > 0)
                        ids = ids.Substring(1);
                    sql = "Select * from " + this.TblOperationLogParam 
                        + " where " + this.OpLogID + " in (" + ids + ") order by " + this.OpLogID + " desc";
                    db.GetDataSet(this.TblOperationLogParam, sql, ds);
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
            return ds;
        }

        /// <summary>
        /// Get Web Service ID-Name Pairs
        /// </summary>
        /// <returns>
        /// Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME
        /// </returns>
        public DataTable GetWebServiceIDNamePairs()
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                string sql = "select A." + this.WSID + ", A." + this.WSName;
                sql += " from " + this.TblWebService + " A";
                sql += " order by A." + this.WSName;
                db = this.GetNodeDB();
                dt = new DataTable();
                db.GetDataTable(this.TblWebService, sql, dt);
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
            return dt;
        }

        /// <summary>
        /// Get Unique Operation Log Statuses
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is logged in</param>
        /// <returns>
        /// Unique Statuses of Operation Logs
        /// </returns>
        public ArrayList GetUniqueOpStatuses(string domainAdmin)
        {
            ArrayList retList = new ArrayList();
            DBAdapter db = null;
            try
            {
                string sql = "select distinct A." + this.Status;
                sql += " from " + this.TblOperationLogStatus + " A ";
                //sql += " from " + this.TblOperationLogStatus + " A, " + this.TblOperation + " B, " + this.TblDomain + " C";
                //sql += ", " + this.TblAccType + " D, " + this.TblAccTypeXREF + " E, " + this.TblUser + " F";
                //sql += ", " + this.TblOperationLog + " G";
                //sql += " where F." + this.LoginName + " = @" + this.LoginName + " and F." + this.UserID + " = E." + this.UserID;
                //sql += " and E." + this.AccTypeID + " = D." + this.AccTypeID + " and D." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                //sql += " and E." + this.DomID + " = C." + this.DomID;
                //sql += " and ((C." + this.DomName + " = 'NODE') or (C." + this.DomID + " = B." + this.DomID;
                //sql += " and B." + this.OpID + " = G." + this.OpID + " and G." + this.OpLogID + " = A." + this.OpLogID + "))";
                //sql += " group by A." + this.Status;
                sql += " order by A." + this.Status;
                ArrayList parameters = new ArrayList();
                //parameters.Add(new Parameter(this.LoginName, domainAdmin));
                db = this.GetNodeDB();
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperationLogStatus, sql, parameters, dt);
                foreach (DataRow dr in dt.Rows)
                    retList.Add(dr[this.Status]);
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
            return retList;
        }

        /// <summary>
        /// Get the Business Operation Log Object from the Database
        /// </summary>
        /// <param name="opLogID">The Operation Log ID of the Operation Log</param>
        /// <returns>The OperationLog Business Object</returns>
        public OperationLog GetOperationLog(int opLogID)
        {
            OperationLog log = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.TransID + ", B." + this.OpName;
                sql += ", C." + this.DomName + ", B." + this.OpType;
                sql += ", D." + this.WSName + ", A." + this.UserName;
                sql += ", A." + this.ReqIP + ", A." + this.SupplTransID;
                sql += ", A." + this.Token + ", A." + this.NodeAddr;
                sql += ", A." + this.RetURL + ", A." + this.ServiceType;
                sql += ", A." + this.StartDate + ", A." + this.EndDate;
                sql += ", A." + this.HostName;
                sql += ", E." + this.ParamName + ", E." + this.ParamValue;
                sql += ", F." + this.OpLogStatusID;
                sql += ", F." + this.Status + ", F." + this.Message;
                sql += ", F." + this.CreatedDate + " STATUS_CREATE_DATE";
                sql += ", F." + this.CreatedBy + " STATUS_CREATE_BY";
                sql += ", A." + this.CreatedDate + ", A." + this.CreatedBy;
                sql += ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                sql += " from " + this.TblOperationLog + " A";
                sql += " left join " + this.TblOperationLogParam + " E on A." + this.OpLogID + " = E." + this.OpLogID;
                sql += " left join " + this.TblOperationLogStatus + " F on A." + this.OpLogID + " = F." + this.OpLogID;
                sql += ", " + this.TblOperation + " B";
                sql += " left join " + this.TblWebService + " D on B." + this.WSID + " = D." + this.WSID;
                sql += ", " + this.TblDomain + " C";
                sql += " where A." + this.OpLogID + " = @" + this.OpLogID;
                parameters.Add(new Parameter(this.OpLogID, opLogID));
                sql += " and A." + this.OpID + " = B." + this.OpID;
                sql += " and B." + this.DomID + " = C." + this.DomID;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperationLog, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblOperationLog) && ds.Tables[this.TblOperationLog].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[this.TblOperationLog].Rows[0];
                    log = new OperationLog();
                    log.OperationLogID = opLogID;
                    log.TransactionID = dr[this.TransID].ToString();
                    log.OperationName = dr[this.OpName].ToString();
                    log.DomainName = dr[this.DomName].ToString();
                    log.OperationType = dr[this.OpType].ToString();
                    object obj = null;
                    if (log.OperationType.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
                    {
                        log.WebServiceName = dr[this.WSName].ToString();
                        obj = dr[this.UserName];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.UserName = obj.ToString();
                        obj = dr[this.ReqIP];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.RequestorIP = obj.ToString();
                        obj = dr[this.SupplTransID];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.SuppliedTransactionID = obj.ToString();
                        obj = dr[this.Token];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.Token = obj.ToString();
                        obj = dr[this.NodeAddr];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.NodeAddress = obj.ToString();
                        obj = dr[this.RetURL];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.ReturnURL = obj.ToString();
                        obj = dr[this.ServiceType];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            log.ServiceType = obj.ToString();
                    }
                    obj = dr[this.StartDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.StartDate = (DateTime)obj;
                    obj = dr[this.EndDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.EndDate = (DateTime)obj;
                    obj = dr[this.HostName];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.HostName = obj.ToString();
                    obj = dr[this.CreatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.CreatedDate = (DateTime)obj;
                    obj = dr[this.CreatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.CreatedBy = obj.ToString();
                    obj = dr[this.UpdatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.UpdatedDate = (DateTime)obj;
                    obj = dr[this.UpdatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        log.UpdatedBy = obj.ToString();
                    Hashtable logParameterHash = new Hashtable();
                    ArrayList logParameterList = new ArrayList();
                    Hashtable logStatusHash = new Hashtable();
                    ArrayList logStatusList = new ArrayList();
                    foreach (DataRow row in ds.Tables[this.TblOperationLog].Rows)
                    {
                        obj = row[this.ParamName];
                        if (obj != null && !obj.Equals(DBNull.Value) && !logParameterHash.ContainsKey(row[this.ParamName]))
                        {
                            OperationLogParameter param = new OperationLogParameter();
                            param.ParameterName = obj.ToString();
                            obj = row[this.ParamValue];
                            if (obj != null && !obj.Equals(DBNull.Value))
                                param.ParameterValue = obj.ToString();
                            logParameterHash.Add(param.ParameterName, param);
                            logParameterList.Add(param);
                        }
                        if (!logStatusHash.ContainsKey(row[this.OpLogStatusID]))
                        {
                            OperationLogStatus status = new OperationLogStatus(int.Parse(row[this.OpLogStatusID].ToString()));
                            obj = row[this.Status];
                            if (obj != null && !obj.Equals(DBNull.Value))
                                status.Status = obj.ToString();
                            obj = row[this.Message];
                            if (obj != null && !obj.Equals(DBNull.Value))
                                status.Message = obj.ToString();
                            obj = row["STATUS_CREATE_DATE"];
                            if (obj != null && !obj.Equals(DBNull.Value))
                                status.CreatedDate = (DateTime)obj;
                            obj = row["STATUS_CREATE_BY"];
                            if (obj != null && !obj.Equals(DBNull.Value))
                                status.CreatedBy = obj.ToString();
                            logStatusHash.Add(row[this.OpLogStatusID], status);
                            logStatusList.Add(status);
                        }
                    }
                    log.Parameters = logParameterList;
                    log.Statuses = logStatusList;
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
            return log;
        }

        /// <summary>
        /// Gets the operation status by specified transaction id.
        /// </summary>
        /// <param name="transID">The specified transaction id.</param>
        /// <param name="status">The status code.</param>
        /// <returns>An ArrayList contains the status and status messages.</returns>
        public ArrayList GetOperationStatus(string transID, string status)
        {
            ArrayList parameters = new ArrayList();
            string sql = "SELECT " + this.TblOperationLogStatus + ".* ";
            sql += "FROM " + this.TblOperationLogStatus + " INNER JOIN ";
            sql += this.TblOperationLog + " ON " + this.TblOperationLog + "." + this.OpLogID + "=" + this.TblOperationLogStatus + "." + this.OpLogID + " ";
            sql += "WHERE " + this.TblOperationLog + ".TRANS_ID=@transid ";
            if (status != null && status.Trim() != String.Empty)
            {
                sql += " AND " + this.TblOperationLogStatus + ".STATUS_CD=@status ";
                parameters.Add(new Parameter("@status", status));
            }
            sql += "ORDER BY " + this.TblOperationLogStatus + ".CREATED_DTTM ";

            parameters.Add(new Parameter("@transid", transID));
            DBAdapter db = GetNodeDB();
            DataTable dt = new DataTable();

            db.GetDataTable(this.TblOperationLogStatus, sql, parameters, dt);

            ArrayList arr = new ArrayList();
            foreach (DataRow row in dt.Rows)
                arr.Add(new string[] { row["STATUS_CD"] + "", row["MESSAGE"] + "", row["CREATED_DTTM"] + "" });

            return arr;
        }

        /// <summary>
        /// Check if Operation has Log Created.
        /// </summary>
        /// <param name="OpID">The specified operation id.</param>
        /// <returns>An Boolean which indicated true for having operation logs</returns>
        public bool IsOperationLogExisted(string OpID)
        {
            bool bOk = false;
            ArrayList parameters = new ArrayList();

            string sql = " SELECT " + OpLogID ;
            sql += " FROM " + this.TblOperationLog;
            sql += " WHERE " + this.TblOperationLog + "."+this.OpID+"=@OpID ";

            parameters.Add(new Parameter("@OpID", OpID));
            DBAdapter db = GetNodeDB();
            DataTable dt = new DataTable();

            db.GetDataTable(this.TblOperationLog, sql, parameters, dt);

            if (dt.Rows.Count > 0)
                bOk = true;
            return bOk;
        }

        #endregion
    }
}
