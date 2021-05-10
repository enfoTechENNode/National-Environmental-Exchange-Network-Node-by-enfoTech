using System;
using System.Collections;
using System.Data;
using System.Web;

using Node.Core.Biz.Objects;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// Database object for retrieve Domain Information
    /// </summary>
    public class Domains : BaseData, IDomains
    {
        /// <summary>
        /// Get a DataTable of Domains
        /// </summary>
        /// <param name="domainAdmin">LoginName of Domain Administrator</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME</returns>
        public DataTable GetDomainDropDownList(string domainAdmin)
        {
            DataTable table = null;
            DBAdapter db = null;
            try
            {
                string sql = " select D." + this.DomName;
                sql += " from " + this.TblUser + " A, " + this.TblAccTypeXREF + " B, " + this.TblAccType + " C";
                sql += ", " + this.TblDomain + " D";
                sql += " where A." + this.LoginName + " = @" + this.LoginName + " and A." + this.UserID + " = B." + this.UserID;
                sql += " and B." + this.AccTypeID + " = C." + this.AccTypeID + " and C." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and B." + this.DomID + " = D." + this.DomID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                db = this.GetNodeDB();
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblUser, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblUser))
                {
                    ArrayList domNames = new ArrayList();
                    bool isNodeAdmin = false;
                    foreach (DataRow dr in ds.Tables[this.TblUser].Rows)
                    {
                        if (dr[this.DomName].ToString().Equals("NODE"))
                        {
                            isNodeAdmin = true;
                            break;
                        }
                        else
                            domNames.Add(dr[this.DomName]);
                    }
                    sql = "select A." + this.DomID + ", A." + this.DomName;
                    sql += " from " + this.TblDomain + " A";
                    if (!isNodeAdmin)
                    {
                        if (domNames.Count > 0)
                        {
                            sql += " where A." + this.DomName;
                            sql += " in (";
                            for (int i = 0; i < domNames.Count; i++)
                            {
                                if (i != 0) sql += ", ";
                                sql += "'" + domNames[i] + "'";
                            }
                            sql += ")";
                        }
                        else
                        {
                            sql += " where 1 = -1";
                        }
                    }
                    sql += " order by A." + this.DomName;
                    db.GetDataSet(this.TblDomain, sql, ds);
                    if (ds.Tables.Contains(this.TblDomain))
                        table = ds.Tables[this.TblDomain];
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
            return table;
        }
        /// <summary>
        /// Get the DataTable for the Domain Search Grid
        /// </summary>
        /// <param name="domainID">The id of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainStatus">The status of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged into the system.</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME, DOMAIN_STATUS_CD, DOMAIN_STATUS_MSG</returns>
        public DataTable GetDomainSearchGrid(string domainID, string domainStatus, string domainAdmin)
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select distinct A." + this.DomID + ", A." + this.DomName + ", A." + this.DomStatus + ", A." + this.DomMessage;
                sql += " from " + this.TblDomain + " A, " + this.TblUser + " B, " + this.TblAccTypeXREF + " C";
                sql += ", " + this.TblAccType + " D, " + this.TblDomain + " E";
                sql += " where B." + this.LoginName + " = @" + this.LoginName + " and B." + this.UserID + " = C." + this.UserID;
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                sql += " and C." + this.AccTypeID + " = D." + this.AccTypeID + " and D." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and C." + this.DomID + " = E." + this.DomID;
                sql += " and ((E." + this.DomName + " = 'NODE') or (E." + this.DomID + " = A." + this.DomID + " ))";
                //sql += " and E." + this.DomID + " = A." + this.DomID; only for Domain, need to select all if user is NODE admin user
                if (domainID != null && !domainID.Trim().Equals("") && !domainID.Trim().Equals("-1"))
                {
                    sql += " and A." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domainID));
                }
                if (domainStatus != null && !domainStatus.Trim().Equals(""))
                {
                    sql += " and A." + this.DomStatus + " = @" + this.DomStatus;
                    parameters.Add(new Parameter(this.DomStatus, domainStatus));
                }
                sql += " group by A." + this.DomID + ", A." + this.DomName + ", A." + this.DomStatus + ", A." + this.DomMessage;
                DataSet ds = new DataSet();
                
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
                db.GetDataSet(this.TblDomain, sql, parameters, ds);
                dt = ds.Tables[this.TblDomain];
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
        /// Get the Domain Business Object by the Input Parameter Domain's Name
        /// </summary>
        /// <param name="domainName">The name of the Domain</param>
        /// <returns>The Domain Business Object</returns>
        public Domain GetDomain(string domainName)
        {
            if (domainName == null || domainName.Trim().Equals(""))
                return null;
            Domain retDom = new Domain();
            retDom.Name = domainName;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A.*, B." + this.UserID + ", D." + this.WSName;
                sql += " from " + this.TblDomain + " A";
                sql += " left join " + this.TblAccTypeXREF + " B on A." + this.DomID + " = B." + this.DomID;
                sql += " left join " + this.TblDomainWS + " C on A." + this.DomID + " = C." + this.DomID;
                sql += " left join " + this.TblWebService + " D on C." + this.WSID + " = D." + this.WSID;
                sql += " where upper(A." + this.DomName + ") = @" + this.DomName;
                parameters.Add(new Parameter(this.DomName, domainName.ToUpper()));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblDomain, sql, parameters, ds);
                if (ds.Tables[this.TblDomain].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[this.TblDomain].Rows[0];
                    retDom.ID = int.Parse(dr[this.DomID].ToString());
                    object obj = dr[this.DomDesc];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.Description = obj.ToString();
                    obj = dr[this.DomStatus];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.Status = obj.ToString();
                    obj = dr[this.DomMessage];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.StatusMessage = obj.ToString();
                    obj = dr[this.CreatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.CreatedDate = (DateTime)obj;
                    obj = dr[this.CreatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.CreatedBy = obj.ToString();
                    obj = dr[this.UpdatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.UpdatedDate = (DateTime)obj;
                    obj = dr[this.UpdatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retDom.UpdatedBy = obj.ToString();
                    foreach (DataRow r in ds.Tables[this.TblDomain].Rows)
                    {
                        obj = r[this.WSName];
                        if (obj != null && !obj.Equals(DBNull.Value))
                        {
                            switch (obj.ToString())
                            {
                                case Phrase.WEB_SERVICE_SUBMIT:
                                    retDom.AllowSubmit = true;
                                    break;
                                case Phrase.WEB_SERVICE_DOWNLOAD:
                                    retDom.AllowDownload = true;
                                    break;
                                case Phrase.WEB_SERVICE_QUERY:
                                    retDom.AllowQuery = true;
                                    break;
                                case Phrase.WEB_SERVICE_SOLICIT:
                                    retDom.AllowSolicit = true;
                                    break;
                                case Phrase.WEB_SERVICE_NOTIFY:
                                    retDom.AllowNotify = true;
                                    break;
                            }
                        }
                    }
                    Hashtable collection = new Hashtable();
                    ArrayList adminIDs = new ArrayList();
                    foreach (DataRow r in ds.Tables[this.TblDomain].Rows)
                    {
                        obj = r[this.UserID];
                        if (obj != null && !obj.Equals(DBNull.Value))
                        {
                            int id = int.Parse(obj.ToString());
                            if (!collection.Contains(id))
                            {
                                collection.Add(id, id);
                                adminIDs.Add(id);
                            }
                        }
                    }
                    retDom.AdminIDs = adminIDs;
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
            return retDom;
        }
        /// <summary>
        /// Save a Domain
        /// </summary>
        /// <param name="d">The Domain to Save</param>
        /// <param name="domainAdmin">The Domain Administrator logged into the System</param>
        public void SaveDomain(Domain d, string domainAdmin)
        {
            DBAdapter db = null;
            bool error = false;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select * from " + this.TblDomain + " where " + this.DomID + " = @" + this.DomID;
                parameters.Add(new Parameter(this.DomID, d.ID));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.BeginTransaction();
                db.GetDataSet(this.TblDomain, sql, parameters, ds);
                DataTable domainTable = ds.Tables[this.TblDomain];
                bool isInsert = false;
                DataRow dr = null;
                if (domainTable.Rows.Count > 0)
                    dr = domainTable.Rows[0];
                else
                {
                    isInsert = true;
                    dr = domainTable.NewRow();
                    d.ID = this.GetSequenceNumber(this.TblDomain, this.DomID);
                    dr[this.DomID] = d.ID;
                    dr[this.DomName] = d.Name;
                }
                dr[this.DomDesc] = d.Description;
                dr[this.DomStatus] = d.Status;
                dr[this.DomMessage] = d.StatusMessage;
                dr[this.UpdatedDate] = DateTime.Now;
                dr[this.UpdatedBy] = domainAdmin;
                if (isInsert)
                {
                    dr[this.CreatedDate] = DateTime.Now;
                    dr[this.CreatedBy] = domainAdmin;
                    domainTable.Rows.Add(dr);
                }
                db.UpdateDataSet(this.TblDomain, ds);
                this.UpdateAdmins(d, db);
                this.UpdateDomainWS(d, db);
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                    db.Close();
                }
                error = true;
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    if (!error)
                        db.CommitTransaction();
                    db.Close();
                }
            }
        }
        /// <summary>
        /// The mehtod updates NODE_ACCOUNT_TYPE_XREF table.
        /// </summary>
        /// <param name="d">Name of domain.</param>
        /// <param name="db">DataAdapter of Node database.</param>
        private void UpdateAdmins(Domain d, DBAdapter db)
        {
            if (d.AdminIDs.Count > 0)
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.AccTypeID + " from " + this.TblAccType + " A";
                sql += " where A." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblAccType, sql, ds);
                int accTypeID = int.Parse(ds.Tables[this.TblAccType].Rows[0][this.AccTypeID].ToString());

                ArrayList toAdd = new ArrayList(d.AdminIDs);

                sql = "select * from " + this.TblAccTypeXREF + " where " + this.DomID + " = @" + this.DomID;
                parameters.Add(new Parameter(this.DomID, d.ID));
                db.GetDataSet(this.TblAccTypeXREF, sql, parameters, ds);
                DataTable dt = ds.Tables[this.TblAccTypeXREF];
                foreach (DataRow dr in dt.Rows)
                {
                    int userID = int.Parse(dr[this.UserID].ToString());
                    if (toAdd.Contains(userID))
                        toAdd.Remove(userID);
                    else
                        dr.Delete();
                }
                foreach (object obj in toAdd)
                {
                    DataRow dr = dt.NewRow();
                    dr[this.AccTypeXREFID] = this.GetSequenceNumber(this.TblAccTypeXREF, this.AccTypeXREFID);
                    dr[this.AccTypeID] = accTypeID;
                    dr[this.UserID] = int.Parse(obj.ToString());
                    dr[this.DomID] = d.ID;
                    dt.Rows.Add(dr);
                }
                db.UpdateDataSet(this.TblAccTypeXREF, ds);
            }
            else
            {
                DataSet ds = new DataSet();
                string sql = "select * from " + this.TblAccTypeXREF;
                db.GetDataSet(this.TblAccTypeXREF, sql, ds);
                DataTable dt = ds.Tables[this.TblAccTypeXREF];
                DataRow[] drs = dt.Select(this.DomID + " = " + d.ID);
                foreach (DataRow dr in drs)
                {
                    DataRow[] userRows = dt.Select(this.UserID + " = " + dr[this.UserID] + " and " + this.DomID + " <> " + d.ID);
                    if (userRows != null && userRows.Length > 0)
                        dr.Delete();
                    else
                        dr[this.DomID] = DBNull.Value;
                }
                db.UpdateDataSet(this.TblAccTypeXREF, ds);
            }
        }
        /// <summary>
        /// The method updates NODE_DOMAIN_WEB_SERVICE_XREF table. 
        /// </summary>
        /// <param name="d">Name of domain.</param>
        /// <param name="db">DataAdapter of Node database.</param>
        private void UpdateDomainWS(Domain d, DBAdapter db)
        {
            if (d.AllowSubmit || d.AllowDownload || d.AllowQuery || d.AllowSolicit || d.AllowNotify)
            {
                string sql = "select A." + this.WSID + " from " + this.TblWebService + " A where " + this.WSName + " in (";
                if (d.AllowSubmit)
                    sql += "'" + Phrase.WEB_SERVICE_SUBMIT + "'";
                if (d.AllowDownload)
                {
                    if (!sql.EndsWith("(")) sql += ", ";
                    sql += "'" + Phrase.WEB_SERVICE_DOWNLOAD + "'";
                }
                if (d.AllowQuery)
                {
                    if (!sql.EndsWith("(")) sql += ", ";
                    sql += "'" + Phrase.WEB_SERVICE_QUERY + "'";
                }
                if (d.AllowSolicit)
                {
                    if (!sql.EndsWith("(")) sql += ", ";
                    sql += "'" + Phrase.WEB_SERVICE_SOLICIT + "'";
                }
                if (d.AllowNotify)
                {
                    if (!sql.EndsWith("(")) sql += ", ";
                    sql += "'" + Phrase.WEB_SERVICE_NOTIFY + "'";
                }
                sql += ")";
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblWebService, sql, ds);

                ArrayList toAdd = new ArrayList();
                foreach (DataRow dr in ds.Tables[this.TblWebService].Rows)
                    toAdd.Add(dr[this.WSID]);

                sql = "select * from " + this.TblDomainWS + " where " + this.DomID + " = @" + this.DomID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DomID, d.ID));
                db.GetDataSet(this.TblDomainWS, sql, parameters, ds);

                foreach (DataRow dr in ds.Tables[this.TblDomainWS].Rows)
                {
                    int wsID = int.Parse(dr[this.WSID].ToString());
                    if (toAdd.Contains(wsID))
                        toAdd.Remove(wsID);
                    else
                        dr.Delete();
                }
                foreach (object obj in toAdd)
                {
                    DataRow dr = ds.Tables[this.TblDomainWS].NewRow();
                    dr[this.WSID] = obj;
                    dr[this.DomID] = d.ID;
                    ds.Tables[this.TblDomainWS].Rows.Add(dr);
                }
                db.UpdateDataSet(this.TblDomainWS, ds);
            }
            else
            {
                ArrayList parameters = new ArrayList();
                string sql = "delete from " + this.TblDomainWS + " where " + this.DomID + " = @" + this.DomID;
                parameters.Add(new Parameter(this.DomID, d.ID));
                db.ExecuteNonQuery(sql, parameters);
            }
        }
        /// <summary>
        /// Check if console user is Node Domain Admin.
        /// </summary>
        /// <param name="userName">The LogIn UserName</param>
        /// <returns>True if is Node Domain Admin</returns>
        public bool IsNodeDomainAdmin(string userName)
        {
            bool bFlag = false;
            DBAdapter db = null;
            try
            {
                string sql = " select D." + this.DomName;
                sql += " from " + this.TblUser + " A, " + this.TblAccTypeXREF + " B, " + this.TblAccType + " C";
                sql += ", " + this.TblDomain + " D";
                sql += " where A." + this.LoginName + " = @" + this.LoginName + " and A." + this.UserID + " = B." + this.UserID;
                sql += " and B." + this.AccTypeID + " = C." + this.AccTypeID + " and C." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and B." + this.DomID + " = D." + this.DomID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, userName));
                db = this.GetNodeDB();
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblUser, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblUser))
                {
                    foreach (DataRow dr in ds.Tables[this.TblUser].Rows)
                    {
                        if (dr[this.DomName].ToString().Equals("NODE"))
                        {
                            bFlag = true;
                            break;
                        }
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
            return bFlag;
        }
    }
}
