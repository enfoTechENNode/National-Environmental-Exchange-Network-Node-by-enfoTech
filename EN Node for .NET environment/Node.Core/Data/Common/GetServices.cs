using System;
using System.Collections;
using System.Data;

using Node.Core;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// GetServices related Database object.
    /// </summary>
    public class GetServices : BaseData, IGetServices
    {
        /// <summary>
        /// Constructor of GetServices.
        /// </summary>
        public GetServices()
        {

        }
        /// <summary>
        /// Get a list of service type from existing operation.
        /// </summary>
        /// <returns>The string array of servcie type.</returns>
        public string[] GetServiceTypes()
        {
            string[] retArray = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.OpName + " from " + this.TblOperation + " A";
                command += ", " + this.TblWebService + " B";
                command += " where B." + this.WSName + " = '" + Phrase.WEB_SERVICE_GETSERVICES + "'";
                command += " and A." + this.WSID + " = B." + this.WSID;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperation, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retArray = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        retArray[i] = "" + dt.Rows[i][this.OpName];
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
            return retArray;
        }
        /// <summary>
        /// Get a list of avaliable web servcies type.
        /// </summary>
        /// <returns>The string array of web service type.</returns>
        public string[] GetWebServices()
        {
            string[] retArray = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select " + this.WSName + " from " + this.TblWebService;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblWebService, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retArray = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        retArray[i] = "" + dt.Rows[i][this.WSName];
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
            return retArray;
        }
        /// <summary>
        /// Get a list of operation name belong to query operation.
        /// </summary>
        /// <returns>The string array of operation name.</returns>
        public string[] GetQueries()
        {
            string[] retArray = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.OpName + " from " + this.TblOperation + " A";
                command += ", " + this.TblWebService + " B";
                command += " where B." + this.WSName + " = '" + Phrase.WEB_SERVICE_QUERY + "'";
                command += " and A." + this.WSID + " = B." + this.WSID;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperation, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retArray = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        retArray[i] = "" + dt.Rows[i][this.OpName];
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
            return retArray;
        }
        /// <summary>
        /// Get a list of operation name belong to solict operation. 
        /// </summary>
        /// <returns>the string array of operation name.</returns>
        public string[] GetSolicits()
        {
            string[] retArray = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.OpName + " from " + this.TblOperation + " A";
                command += ", " + this.TblWebService + " B";
                command += " where B." + this.WSName + " = '" + Phrase.WEB_SERVICE_SOLICIT + "'";
                command += " and A." + this.WSID + " = B." + this.WSID;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblOperation, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retArray = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                        retArray[i] = "" + dt.Rows[i][this.OpName];
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
            return retArray;
        }
        /// <summary>
        /// Check if the input domain name existed.
        /// </summary>
        /// <param name="domainName">input domain name.</param>
        /// <returns>True if input domain name existed.</returns>
        public bool TestDomainName(string domainName)
        {
            bool exists = false;
            DBAdapter db = null;
            try
            {
                string command = "select count(*) from " + this.TblDomain;
                command += " where " + this.DomName + " = @" + this.DomName;
                command += " and " + this.DomStatus + " = '" + Phrase.STATUS_RUNNING + "'";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DomName, domainName));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                object obj = db.ExecuteScalar(command, parameters);
                if (obj != null && !obj.Equals(DBNull.Value))
                {
                    int num = int.Parse("" + obj);
                    if (num > 0)
                        exists = true;
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
            return exists;
        }
        /// <summary>
        /// Get a list of operation name under a domain.
        /// </summary>
        /// <param name="domainName">The name of domain.</param>
        /// <returns>The string list of operation name.</returns>
        public string[] GetOpNamesFromDomain(string domainName)
        {
            string[] opNames = null;
            DBAdapter db = null;
            try
            {
                string command = "select B." + this.OpName + ", C." + this.WSName;
                command += " from " + this.TblDomain + " A, " + this.TblOperation + " B, " + this.TblWebService + " C";
                command += " where A." + this.DomName + " = @" + this.DomName;
                command += " and A." + this.DomStatus + " = '" + Phrase.STATUS_RUNNING + "'";
                command += " and A." + this.DomID + " = B." + this.DomID;
                command += " and B." + this.OpType + " = '" + Phrase.OPERATION_TYPE_WEB_SERVICE + "'";
                command += " and B." + this.OpStatus + " = '" + Phrase.STATUS_RUNNING + "'";
                command += " and B." + this.WSID + " = C." + this.WSID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DomName, domainName));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblDomain, command, parameters, ds);
                if (ds.Tables.Contains(this.TblDomain) && ds.Tables[this.TblDomain].Rows.Count > 0)
                {
                    opNames = new string[ds.Tables[this.TblDomain].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblDomain].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[this.TblDomain].Rows[i];
                        opNames[i] = dr[this.WSName] + "." + dr[this.OpName];
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
            return opNames;
        }
        /// <summary>
        /// Get a list of domain name with status running.
        /// </summary>
        /// <returns>The array of domain name</returns>
        public ArrayList RetrieveGetServicesOperationNames()
        {
            ArrayList retList = new ArrayList();
            DBAdapter db = null;
            try
            {
                string command = "select C." + this.DomName + " NAME from " + this.TblDomain + " C";
                command += " where C." + this.DomStatus + " = '" + Phrase.STATUS_RUNNING + "'";
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, command, ds);
                if (ds.Tables.Contains(this.TblOperation))
                    foreach (DataRow dr in ds.Tables[this.TblOperation].Rows)
                        retList.Add(dr["NAME"]);

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
        /// Get a DataTable which contains information for ENDS.
        /// </summary>
        /// <returns>The array of domain name</returns>
        public DataTable GetServiceForENDS()
        {
            DBAdapter db = null;
            DataSet ds = new DataSet();
            DataTable dt;
            try
            {

                string command = "SELECT OPERATION_ID FROM NODE_OPERATION WHERE (NODE_OPERATION.PUBLISH_IND = 'Y') AND (NODE_OPERATION.WEB_SERVICE_ID IN (6, 7))";

                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, command, ds);
                dt = ds.Tables[0];
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

    }
}
