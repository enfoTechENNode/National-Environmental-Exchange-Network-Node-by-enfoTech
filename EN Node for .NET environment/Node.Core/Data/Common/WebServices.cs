using System;
using System.Collections;
using System.Data;

using Node.Lib.Data;

using Node.Core.Data.Interfaces;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// Database object for accessing NODE_WEB_SERVICE.
    /// </summary>
    public class WebServices : BaseData, IWebServices
    {
        /// <summary>
        /// Get the List of Web Services
        /// </summary>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public DataTable GetWebServicesList()
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.WSID + ", A." + this.WSName;
                sql += " from " + this.TblWebService + " A";
                sql += " order by A." + this.WSName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblWebService, sql, ds);
                dt = ds.Tables[this.TblWebService];
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
        /// Get the List of Web Services that this Domain can create Operations For
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public DataTable GetWebServicesList(string domain)
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.WSID + ", A." + this.WSName;
                sql += " from " + this.TblWebService + " A";
                if (!domain.ToUpper().Equals("NODE"))
                {
                    sql += ", " + this.TblDomain + " B, " + this.TblDomainWS + " C";
                    sql += " where A." + this.WSID + " = C." + this.WSID + " and C." + this.DomID + " = B." + this.DomID;
                    sql += " and upper(B." + this.DomName + ") = @" + this.DomName;
                    parameters.Add(new Parameter(this.DomName, domain.ToUpper()));
                }
                sql += " order by A." + this.WSName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblWebService, sql, parameters, ds);
                dt = ds.Tables[this.TblWebService];
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
        /// Get the List of Web Services that this Domain can create Operations For
        /// Node Spec. V1.1
        /// </summary>
        /// <param name="domain">The Domain</param>
        /// <returns>Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME</returns>
        public DataTable GetWebServicesListVer11(string domain)
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.WSID + ", A." + this.WSName;
                sql += " from " + this.TblWebService + " A";
                if (!domain.ToUpper().Equals("NODE"))
                {
                    sql += ", " + this.TblDomain + " B, " + this.TblDomainWS + " C";
                    sql += " where A." + this.WSID + " = C." + this.WSID + " and C." + this.DomID + " = B." + this.DomID;
                    sql += " and upper(B." + this.DomName + ") = @" + this.DomName;
                    sql += " and A." + this.WSID + "<> 9";
                    parameters.Add(new Parameter(this.DomName, domain.ToUpper()));
                }else
                {
                    sql+= " where A."+ this.WSID + "<> 9";
                }
                sql += " order by A." + this.WSName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblWebService, sql, parameters, ds);
                dt = ds.Tables[this.TblWebService];
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
