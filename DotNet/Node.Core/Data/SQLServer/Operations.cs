using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Text;

using Node.Core.Biz.Objects;
using Node.Core.Data.Interfaces;
using Node.Lib.AppSystem;
using Node.Lib.Data;

using DataFlow.Component.Interface;

namespace Node.Core.Data.SQLServer
{
    /// <summary>
    /// Database object for retrieve Operation information.
    /// </summary>
    public class Operations : BaseData, IOperations
    {
        /// <summary>
        /// Get Operation class by operation identifier.
        /// </summary>
        /// <param name="opID">Identifier of operation</param>
        /// <returns>Operation Class</returns>
        public Operation GetOperation(int opID)
        {
            return this.GetOperation(opID, null, null);
        }
        /// <summary>
        /// Get Operation class by operation name
        /// </summary>
        /// <param name="opName">Name of Operation</param>
        /// <returns>Operation Class</returns>
        public Operation GetOperation(string opName)
        {
            return this.GetOperation(-1, opName, null);
        }
        /// <summary>
        /// Get Operation class by operation name and web service name
        /// </summary>
        /// <param name="opName">Name of Operation</param>
        /// <param name="wsName">Name of Web Service</param>
        /// <returns></returns>
        public Operation GetOperation(string opName, string wsName)
        {
            return this.GetOperation(-1, opName, wsName);
        }
        /// <summary>
        /// Get unique opeation name under specified Domain
        /// </summary>
        /// <param name="domainAdmin">Name of Domain</param>
        /// <returns>Array of operation names</returns>
        public string[] GetUniqueOperationNames(string domainAdmin)
        {
            string[] opNames = null;
            DBAdapter db = null;
            try
            {
                string sql = "select A." + this.OpName;
                sql += " from " + this.TblOperation + " A, " + this.TblDomain + " B, " + this.TblAccTypeXREF + " C";
                sql += ", " + this.TblAccType + " D, " + this.TblUser + " E";
                sql += " where E." + this.LoginName + " = @" + this.LoginName + " and E." + this.UserID + " = C." + this.UserID;
                sql += " and C." + this.AccTypeID + " = D." + this.AccTypeID + " and D." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and C." + this.DomID + " = B." + this.DomID;
                //sql += " and ((B." + this.DomName + " = 'NODE') or (B." + this.DomID + " = A." + this.DomID + "))";
                sql += " and B." + this.DomID + " = A." + this.DomID;
                sql += " group by A." + this.OpName;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblOperation) && ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    opNames = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < opNames.Length; i++)
                        opNames[i] = "" + ds.Tables[this.TblOperation].Rows[i][this.OpName];
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
        /// Get names of request under solicit web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        public string[] GetSolicitNames()
        {
            string[] retNames = null;
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_SOLICIT + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation) && ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    retNames = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                        retNames[i] = "" + ds.Tables[this.TblOperation].Rows[i][this.OpName];
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
            return retNames;
        }
        /// <summary>
        /// Get names of request under solicit web service and specified node version.
        /// </summary>
        /// <param name="version">Node Version. The value can be <see cref="Node.Core.Phrase.VERSION_11">VER_11</see> or <see cref="Node.Core.Phrase.VERSION_20">VER_20</see></param>
        /// <returns>Array of request names</returns>
        public string[] GetSolicitNames(string version)
        {
            string[] retNames = null;
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_SOLICIT + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " and B." + this.OpVersionNo + " = '" + version + "'"; 
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation) && ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    retNames = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                        retNames[i] = "" + ds.Tables[this.TblOperation].Rows[i][this.OpName];
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
            return retNames;
        }
        /// <summary>
        /// Get a list of parameters under solicit web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        public Hashtable GetSolicitNameParameterPairs()
        {
            Hashtable retPairs = new Hashtable();
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName + ", B." + this.OpConfig;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_SOLICIT + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation))
                {
                    foreach (DataRow dr in ds.Tables[this.TblOperation].Rows)
                    {
                        string paramList = null;
                        XmlDocument parameters = new XmlDocument();
                        parameters.LoadXml("" + dr[this.OpConfig]);
                        if (parameters.DocumentElement.Name.ToLower() == "process")
                        {
                            XmlNodeList paramListNodes = parameters.SelectNodes("//variables/variable");
                            foreach (XmlNode var in paramListNodes)
                            {
                                string pName = var.Attributes.GetNamedItem("name").Value.Trim();
                                if (!Enum.IsDefined(typeof(WebServiceParameter), pName))
                                    paramList += pName + ", ";
                            }
                            if (paramList != null && paramList.Trim().Length > 0 )
                            {
                                paramList = paramList.Trim().TrimEnd(",".ToCharArray());
                            }
                        }
                        else
                        {
                            XmlNodeList paramListNodes = parameters.SelectNodes("/Operation/Parameters/Parameter/Name");
                            for (int i = 0; i < paramListNodes.Count; i++)
                            {
                                XmlNode paramNameNode = paramListNodes.Item(i);
                                if (i != 0) paramList += ", ";
                                else paramList = "";
                                paramList += paramNameNode.InnerText;
                            }
                        }
                        retPairs.Add(dr[this.OpName], paramList);
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
            return retPairs;
        }
        /// <summary>
        /// Get names of request under query web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        public string[] GetQueryNames()
        {
            string[] retNames = null;
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_QUERY + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation) && ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    retNames = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                        retNames[i] = "" + ds.Tables[this.TblOperation].Rows[i][this.OpName];
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
            return retNames;
        }
        /// <summary>
        /// Get names of request under query web service and specified node version.
        /// </summary>
        /// <returns>Array of request names</returns>
        public string[] GetQueryNames(string version)
        {
            string[] retNames = null;
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_QUERY + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " and B." + this.OpVersionNo + " = '" + version + "'";
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation) && ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    retNames = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                        retNames[i] = "" + ds.Tables[this.TblOperation].Rows[i][this.OpName];
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
            return retNames;
        }
        /// <summary>
        /// Get a list of parameters under query web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        public Hashtable GetQueryNameParameterPairs()
        {
            Hashtable retPairs = new Hashtable();
            DBAdapter db = null;
            try
            {
                string sql = "select B." + this.OpName + ", B." + this.OpConfig;
                sql += " from " + this.TblWebService + " A, " + this.TblOperation + " B";
                sql += " where A." + this.WSName + " = '" + Phrase.WEB_SERVICE_QUERY + "'";
                sql += " and A." + this.WSID + " = B." + this.WSID;
                sql += " order by B." + this.OpName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, ds);
                if (ds.Tables.Contains(this.TblOperation))
                {
                    foreach (DataRow dr in ds.Tables[this.TblOperation].Rows)
                    {
                        string paramList = "";
                        XmlDocument parameters = new XmlDocument();
                        parameters.LoadXml("" + dr[this.OpConfig]);
                        if (parameters.DocumentElement.Name.ToLower() == "process")
                        {
                            XmlNodeList paramListNodes = parameters.SelectNodes("//variables/variable");
                            foreach (XmlNode var in paramListNodes)
                            {
                                string pName = var.Attributes.GetNamedItem("name").Value.Trim();
                                if (!Enum.IsDefined(typeof(WebServiceParameter), pName))
                                    paramList += pName + ", ";
                            }
                            if (paramList != null && paramList.Trim().Length > 0)
                            {
                                paramList = paramList.Trim().TrimEnd(",".ToCharArray());
                            }
                        }
                        else
                        {
                            XmlNodeList paramListNodes = parameters.SelectNodes("/Operation/Parameters/Parameter/Name");
                            for (int i = 0; i < paramListNodes.Count; i++)
                            {
                                XmlNode paramNameNode = paramListNodes.Item(i);
                                if (i != 0) paramList += ", ";
                                else paramList = "";
                                paramList += paramNameNode.InnerText;
                            }
                        }
                        retPairs.Add(dr[this.OpName], paramList);
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
            return retPairs;
        }
        /// <summary>
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, DOMAIN_NAME, WEB_SERVICE_NAME</returns>
        public DataTable GetOperationsDataGrid(string domainAdmin)
        {
            DBAdapter db = null;
            try
            {
                string sql = "select A." + this.OpID + ", A." + this.OpName + ", B." + this.DomName + ", C." + this.WSName;
                sql += " from " + this.TblOperation + " A, " + this.TblDomain + " B, " + this.TblWebService + " C";
                sql += ", " + this.TblUser + " D, " + this.TblAccTypeXREF + " E, " + this.TblAccType + " F, " + this.TblDomain + " G";
                sql += " where A." + this.DomID + " = B." + this.DomID + " and A." + this.WSID + " = C." + this.WSID;
                sql += " and D." + this.LoginName + " = @" + this.LoginName + " and D." + this.UserID + " = E." + this.UserID;
                sql += " and E." + this.AccTypeID + " = F." + this.AccTypeID + " and F." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and E." + this.DomID + " = G." + this.DomID;
                //sql += " and ((G." + this.DomName + " = 'NODE') or (G." + this.DomID + " = B." + this.DomID + "))";
                sql += " and G." + this.DomID + " = B." + this.DomID;
                sql += " group by A." + this.OpID + ", A." + this.OpName + ", B." + this.DomName + ", C." + this.WSName;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                return ds.Tables[this.TblOperation];
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
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public DataTable GetOperationsByUser(string domainAdmin, string versionNo)
        {
            DBAdapter db = null;

            try
            {
                string sql = "select A." + this.OpID + ", A." + this.OpName;
                sql += " from " + this.TblOperation + " A, " + this.TblDomain + " B, " + this.TblWebService + " C";
                sql += ", " + this.TblUser + " D, " + this.TblAccTypeXREF + " E, " + this.TblAccType + " F, " + this.TblDomain + " G";
                sql += " where A." + this.DomID + " = B." + this.DomID + " and A." + this.WSID + " = C." + this.WSID;
                sql += " and D." + this.LoginName + " = @" + this.LoginName + " and D." + this.UserID + " = E." + this.UserID;
                sql += " and E." + this.AccTypeID + " = F." + this.AccTypeID + " and F." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and E." + this.DomID + " = G." + this.DomID;
                sql += " and G." + this.DomID + " = B." + this.DomID + " and A." + this.OpVersionNo + " = '" + versionNo + "'"; 
                sql += " group by A." + this.OpID + ", A." + this.OpName + ", B." + this.DomName + ", C." + this.WSName;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                return ds.Tables[this.TblOperation];
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
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public DataTable GetOperationsByUserForOperationMgr(string domainAdmin, string versionNo)
        {
            DBAdapter db = null;

            try
            {
                string sql = "select A." + this.OpID + ", A." + this.OpName;
                sql += " from " + this.TblOperation + " A, " + this.TblDomain + " B ";
                sql += ", " + this.TblUser + " D, " + this.TblAccTypeXREF + " E, " + this.TblAccType + " F, " + this.TblDomain + " G";
                sql += " where A." + this.DomID + " = B." + this.DomID ;
                sql += " and D." + this.LoginName + " = @" + this.LoginName + " and D." + this.UserID + " = E." + this.UserID;
                sql += " and E." + this.AccTypeID + " = F." + this.AccTypeID + " and F." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and E." + this.DomID + " = G." + this.DomID;
                sql += " and G." + this.DomID + " = B." + this.DomID + " and A." + this.OpVersionNo + " = '" + versionNo + "'";
                sql += " and (A." + this.WSID + " in (7,8) or A."+this.WSID+" is null )and A." + this.OpName +" not in ('NODE','NODE2')" ;
                sql += " group by A." + this.OpID + ", A." + this.OpName + ", B." + this.DomName;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);

                return ds.Tables[this.TblOperation];
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
        /// Get the Operations of the IDs of the Operation Names and Web Service Names specified
        /// It is assumed that opNames[0] corresponds to wsNames[0], opNames[1] to wsNames[1], etc.
        /// If the input parameter arrays are different sizes, then only the first array.Length of lesser size input parameters are used in the query
        /// </summary>
        /// <param name="opNames">Array of Operation Names</param>
        /// <param name="wsNames">Array of Web Service Names</param>
        /// <returns></returns>
        public int[] GetOperationIDs(string[] opNames, string[] wsNames)
        {
            if (opNames == null || opNames.Length <= 0 || wsNames == null || wsNames.Length <= 0)
                return null;
            int[] opIDs = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.OpID;
                sql += " from " + this.TblOperation + " A, " + this.TblWebService + " B";
                sql += " where A." + this.WSID + " = B." + this.WSID;
                sql += " and (";
                for (int i = 0; i < opNames.Length && i < wsNames.Length; i++)
                {
                    if (i != 0) sql += " or ";
                    sql += "(upper(A." + this.OpName + ") = @" + this.OpName + i;
                    sql += " and upper(B." + this.WSName + ") = @" + this.WSName + i + ")";
                    parameters.Add(new Parameter(this.OpName + i, opNames[i].ToUpper()));
                    parameters.Add(new Parameter(this.WSName + i, wsNames[i].ToUpper()));
                }
                sql += ")";
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                if (ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    opIDs = new int[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                        opIDs[i] = int.Parse(ds.Tables[this.TblOperation].Rows[i][this.OpID].ToString());
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
            return opIDs;
        }
        /// <summary>
        /// Get the Operation Names and Web Service Names from the list of Operation IDs
        /// </summary>
        /// <param name="opIDs"></param>
        /// <returns>A double string array, string[0] has opNames and string[1] has wsNames</returns>
        public string[][] GetOpNamesWSNames(int[] opIDs)
        {
            string[][] names = new string[2][];
            if (opIDs == null || opIDs.Length <= 0)
                return names;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.OpName + ", B." + this.WSName;
                sql += " from " + this.TblOperation + " A, " + this.TblWebService + " B";
                sql += " where A." + this.WSID + " = B." + this.WSID + " and A." + this.OpID + " in (";
                for (int i = 0; i < opIDs.Length; i++)
                {
                    if (i != 0) sql += ", ";
                    sql += "@" + this.OpID + i;
                    parameters.Add(new Parameter(this.OpID + i, opIDs[i]));
                }
                sql += ")";
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                if (ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    names[0] = new string[ds.Tables[this.TblOperation].Rows.Count];
                    names[1] = new string[ds.Tables[this.TblOperation].Rows.Count];
                    for (int i = 0; i < ds.Tables[this.TblOperation].Rows.Count; i++)
                    {
                        names[0][i] = ds.Tables[this.TblOperation].Rows[i][this.OpName].ToString();
                        names[1][i] = ds.Tables[this.TblOperation].Rows[i][this.WSName].ToString();
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
            return names;
        }
        /// <summary>
        /// Get the Operations List for a Domain
        /// </summary>
        /// <param name="domainName">The Domain Name</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public DataTable GetOperationsList(string domainName)
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select A." + this.OpID + ", A." + this.OpName;
                sql += " from " + this.TblOperation + " A";
                if (domainName != null && !domainName.Trim().Equals(""))
                {
                    sql += ", " + this.TblDomain + " B";
                    sql += " where A." + this.DomID + " = B." + this.DomID;
                    sql += " and B." + this.DomName + " = @" + this.DomName;
                    parameters.Add(new Parameter(this.DomName, domainName));
                }
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                dt = ds.Tables[this.TblOperation];
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
        /// Search the Node Database for Operations
        /// </summary>
        /// <param name="domain">The Domain of the Operation(s)</param>
        /// <param name="opID">The Operation ID of the Operation</param>
        /// <param name="opType">The Operation Type of the Operation(s)</param>
        /// <param name="wsID">The Web Service ID of the Operation(s)</param>
        /// <param name="status">The Status of the Operation(s)</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, OPERATION_TYPE, WEB_SERVICE_NAME, OPERATION_STATUS_CD, OPERATION_STATUS_MSG</returns>
        public DataTable SearchOperations(string domain, int opID, string opType, int wsID, string status)
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
                string sql = "select A." + this.OpID + ", A." + this.OpName + ", A." + this.OpType + ", B." + this.WSName;
                sql += ", A." + this.OpStatus + ", A." + this.OpMessage;
                sql += " from " + this.TblOperation + " A left join " + this.TblWebService + " B on A." + this.WSID + " = B." + this.WSID;
                string whereAnd = " where ";
                if (domain != null && !domain.Trim().Equals(""))
                {
                    sql += ", " + this.TblDomain + " C";
                    sql += " where A." + this.DomID + " = C." + this.DomID + " and upper(C." + this.DomName + ") = @" + this.DomName;
                    parameters.Add(new Parameter(this.DomName, domain.ToUpper()));
                    whereAnd = " and ";
                }
                if (opID >= 0)
                {
                    sql += whereAnd + "A." + this.OpID + " = @" + this.OpID;
                    parameters.Add(new Parameter(this.OpID, opID));
                    whereAnd = " and ";
                }
                if (opType != null && !opType.Trim().Equals(""))
                {
                    sql += whereAnd + "A." + this.OpType + " = @" + this.OpType;
                    parameters.Add(new Parameter(this.OpType, opType));
                    whereAnd = " and ";
                }
                if (wsID >= 0)
                {
                    sql += whereAnd + "B." + this.WSID + " = @" + this.WSID;
                    parameters.Add(new Parameter(this.WSID, wsID));
                    whereAnd = " and ";
                }
                if (status != null && !status.Trim().Equals(""))
                {
                    sql += whereAnd + "A." + this.OpStatus + " = @" + this.OpStatus;
                    parameters.Add(new Parameter(this.OpStatus, status));
                    whereAnd = " and ";
                }

                sql += " and A." + this.OpVersionNo + " = '" + versionNo + "'";
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);
                dt = ds.Tables[this.TblOperation];
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
        /// Save an Operation to the Database
        /// </summary>
        /// <param name="op">The Operation to Save</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        public void SaveOperation(Operation op, string domainAdmin)
        {
            DBAdapter db = null;
            bool isError = false;
            try
            {
                ArrayList parameters = new ArrayList();
                string sql = "select * from " + this.TblOperation + " where " + this.OpID + " = @" + this.OpID;
                parameters.Add(new Parameter(this.OpID, op.ID));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.BeginTransaction();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);

                sql = "select A." + this.DomID + " from " + this.TblDomain + " A";
                sql += " where A." + this.DomName + " = @" + this.DomName;
                parameters.Add(new Parameter(this.DomName, op.DomainName));
                db.GetDataSet(this.TblDomain, sql, parameters, ds);

                int wsID = -1;
                if (op.Type.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE) && op.WebServiceName != null && !op.WebServiceName.Trim().Equals(""))
                {
                    sql = "select A." + this.WSID + " from " + this.TblWebService + " A";
                    sql += " where A." + this.WSName + " = @" + this.WSName;
                    parameters.Add(new Parameter(this.WSName, op.WebServiceName));
                    db.GetDataSet(this.TblWebService, sql, parameters, ds);
                    wsID = int.Parse(ds.Tables[this.TblWebService].Rows[0][this.WSID].ToString());
                }

                DataRow dr = null;
                bool isInsert = false;
                if (ds.Tables[this.TblOperation].Rows.Count > 0)
                    dr = ds.Tables[this.TblOperation].Rows[0];
                else
                {
                    isInsert = true;
                    dr = ds.Tables[this.TblOperation].NewRow();
                    op.ID = this.GetSequenceNumber(this.TblOperation, this.OpID);
                    dr[this.OpID] = op.ID;
                    dr[this.OpName] = op.Name;
                    dr[this.OpType] = op.Type;
                }
                System.IO.StringWriter sw = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(sw);
                writer.Indentation = 4;
                writer.Formatting = Formatting.Indented;
                op.Config.WriteTo(writer);
                writer.Flush();
                dr[this.OpConfig] = sw.ToString();
                dr[this.DomID] = ds.Tables[this.TblDomain].Rows[0][this.DomID];
                if (wsID >= 0)
                    dr[this.WSID] = wsID;
                else
                    dr[this.WSID] = DBNull.Value;
                dr[this.OpStatus] = op.Status;
                dr[this.OpMessage] = op.Message;
                dr[this.OpDesc] = op.Description;
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                    dr[this.OpVersionNo] = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
                else
                    dr[this.OpVersionNo] = Phrase.VERSION_11;
                op.UpdatedDate = DateTime.Now;
                op.UpdatedBy = (domainAdmin == null)? op.UpdatedBy : domainAdmin;
                dr[this.UpdatedDate] = op.UpdatedDate;
                dr[this.UpdatedBy] = op.UpdatedBy;
                dr[this.OpPublishInd] = op.PublishInd;
                dr[this.OpRESTInd] = op.RESTInd;
                if (isInsert)
                {
                    op.CreatedDate = DateTime.Now;
                    op.CreatedBy = domainAdmin;
                    dr[this.CreatedDate] = op.CreatedDate;
                    dr[this.CreatedBy] = op.CreatedBy;
                    ds.Tables[this.TblOperation].Rows.Add(dr);
                }
                db.UpdateDataSet(this.TblOperation, ds);
                if (op.Type.Equals(Phrase.OPERATION_TYPE_SCHEDULED_TASK))
                {
                    if (isInsert)
                    {
                        TaskConfiguration config = new TaskConfiguration();
                        config.AddTask(op.ID, op.Status == Phrase.STATUS_RUNNING, op.Name, new string[] { op.Name }, op.TaskSchedule);
                        config.Save();
                    }
                    TaskManager manager = new TaskManager("task.config");
                    Task t = manager.GetTask(op.Name);
                    if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_ONCE)
                        t.Parameter = op.Name + " ONCE";
                    else
                        t.Parameter = op.Name;
                    if (!op.TaskSchedule.Status.Equals("A"))
                        t.Status = "I";
                    else
                        t.Status = "A";
                    t.FullPath = System.Configuration.ConfigurationManager.AppSettings["Node.TaskHandler.Path"];
                    t.Schedule.Status = op.TaskSchedule.Status;
                    t.Schedule.StartDateTime = op.TaskSchedule.StartDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                    t.Schedule.EndDateTime = op.TaskSchedule.EndDate.ToString("MM/dd/yyyy hh:mm:ss tt");


                    t.Schedule.DailyMinutes = "";
                    t.Schedule.DailyDays = "";

                    t.Schedule.WeeklyDayOfWeek = "";
                    t.Schedule.WeeklyWeeks = "";

                    t.Schedule.MonthlyDayOfWeek = "";
                    t.Schedule.MonthlyDays = "";
                    t.Schedule.MonthlyMonthOfYear = "";
                    t.Schedule.MonthlyWeekOfMonth = "";
                   
                    if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_MINUTES ||
                        op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_ONCE)
                    {
                        t.Schedule.IsDailySchedule = true;
                        t.Schedule.IsDailyMinutes = true;
                        if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_ONCE)
                            t.Schedule.DailyMinutes = "2";
                        else
                            t.Schedule.DailyMinutes = op.TaskSchedule.IntervalMinutes.ToString();
                    }
                    else if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_DAILY)
                    {
                        t.Schedule.IsDailySchedule = true;
                        t.Schedule.IsDailyDays = true;
                        t.Schedule.DailyDays = op.TaskSchedule.IntervalDays.ToString();
                    }
                    else if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_WEEKLY)
                    {
                        t.Schedule.IsWeeklySchedule = true;
                        t.Schedule.WeeklyWeeks = op.TaskSchedule.IntervalWeeks.ToString();
                        t.Schedule.WeeklyDayOfWeek = op.TaskSchedule.DaysOfWeek;
                    }
                    else if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS)
                    {
                        t.Schedule.IsMonthlySchedule = true;
                        t.Schedule.IsMonthlyDays = true;
                        t.Schedule.MonthlyDays = op.TaskSchedule.DaysOfMonth;
                        t.Schedule.MonthlyMonthOfYear = op.TaskSchedule.MonthsOfYear;
                    }
                    else if (op.TaskSchedule.Type == Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS)
                    {
                        t.Schedule.IsMonthlySchedule = true;
                        t.Schedule.IsMonthlyWeekdays = true;
                        t.Schedule.MonthlyWeekOfMonth = op.TaskSchedule.WeekOfMonth.ToString();
                        t.Schedule.MonthlyDayOfWeek = op.TaskSchedule.DaysOfWeek;
                        t.Schedule.MonthlyMonthOfYear = op.TaskSchedule.MonthsOfYear;
                    }
                    manager.SaveTasks();
                }
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                isError = true;
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    if (!isError)
                        db.CommitTransaction();
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Delete an Operation from the Database
        /// </summary>
        /// <param name="op">The Operation to delete</param>
        public void DeleteOperation(Operation op)
        {
            DBAdapter db = null;
            bool isError = false;
            try
            {
                if (op.Type.Equals(Phrase.OPERATION_TYPE_SCHEDULED_TASK))
                {
                    TaskConfiguration config = new TaskConfiguration();
                    TaskManager manager = new TaskManager("task.config");
                    Task t = manager.GetTask(op.Name);
                    if (t != null)
                    {
                        config.Delete(t.ID);
                        config.Save();
                    }
                }

                ArrayList parameters = new ArrayList();
                string sql = "select * from " + this.TblOperation + " where " + this.OpID + " = @" + this.OpID;
                parameters.Add(new Parameter(this.OpID, op.ID));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.BeginTransaction();
                db.GetDataSet(this.TblOperation, sql, parameters, ds);

                if (ds.Tables[this.TblOperation].Rows.Count > 0)
                    ds.Tables[this.TblOperation].Rows[0].Delete();

                db.UpdateDataSet(this.TblOperation, ds);
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                isError = true;
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    if (!isError)
                        db.CommitTransaction();
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Update operation_config in Operation
        /// </summary>
        /// <param name="opID">The Operation ID</param>
        /// <param name="config">The Operation config XML string </param>
        public bool UpdateOperationConfig(string opID, string config)
        {
            DBAdapter db = this.GetNodeDB();
            bool bXMLType = false;

            string sql = "select " + this.OpConfig + ", " + this.OpID + " from " + this.TblOperation + " where " + this.OpID + "=@opId";

            if (this.IsXMLTYPE(db))
            {
                sql = "select SYS.XMLTYPE.getClobVal(" + this.OpConfig + ") "+ this.OpConfig +"," + this.OpID + ",OPERATION_CONFIG_CLOB from " + this.TblOperation + " where " + this.OpID + "=@opId";
               bXMLType = true;
            }

            ArrayList arr = new ArrayList();
            arr.Add(new Parameter("@opID", opID));
            DataTable dt = new DataTable();
            db.GetDataTable(this.TblOperation, sql, arr, dt);

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0][0] = config;
                if (bXMLType)
                {
                    dt.Rows[0][2] = config;
                }
            }
            db.UpdateDataTable(this.TblOperation, dt);

            if (bXMLType)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" update ");
                sb.Append(this.TblOperation);
                sb.Append(" set ");
                sb.Append(this.OpConfig);
                sb.Append(" = SYS.XMLTYPE.CREATEXML(OPERATION_CONFIG_CLOB)");
                sb.Append(" where ");
                sb.Append(this.OpID);
                sb.Append(" = " + opID);
                db.ExecuteNonQuery(sb.ToString());
                sb.Replace(this.OpConfig, "OPERATION_CONFIG_CLOB");
                sb.Replace("SYS.XMLTYPE.CREATEXML(OPERATION_CONFIG_CLOB)", "NULL");
                db.ExecuteNonQuery(sb.ToString());   
            }

            return true;
        }
        /// <summary>
        /// Get a DataTable for the Current process information
        /// </summary>
        /// <returns>Columns: PROCESS_NAME, UPDATED_DTTM, OPERATION_NAME</returns>
        public DataTable GetProcesses()
        {
            DataTable dtProcess = new DataTable();
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "SELECT A.PROCESS_NAME, "
                               + " A.UPDATED_DTTM, B.OPERATION_NAME"
                               + " FROM NODE_PROCESS_MONITOR A, NODE_OPERATION B"
                               + " WHERE A.OPERATION_ID=B.OPERATION_ID "
                               + " ORDER BY A.UPDATED_DTTM";
                db.GetDataTable(this.TblOperation, command, dtProcess);
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
            return dtProcess;
        }

        private Operation GetOperation(int opID, string opName, string wsName)
        {
            if (opID < 0 && (opName == null || opName.Trim().Equals("")))
                return null;
            Operation retOp = null;
            DBAdapter db = null;
            try
            {
                string command = "select A.* , B." + this.DomID + ", B." + this.DomName + ", B." + this.DomStatus + ", C.";
                command += this.WSID + ", C." + this.WSName + " from " + this.TblOperation + " A";
                command += " left join " + this.TblWebService + " C on A." + this.WSID + " = C." + this.WSID;
                command += ", " + this.TblDomain + " B where ";
                ArrayList parameters = new ArrayList();
                if (opID >= 0)
                {
                    command += "A." + this.OpID + " = @" + this.OpID;
                    Parameter param = new Parameter(this.OpID, opID);
                    parameters.Add(param);
                }
                else
                {
                    command += "upper(A." + this.OpName + ") = @" + this.OpName;
                    Parameter param = new Parameter(this.OpName, opName != null ? opName.Trim().ToUpper() : null);
                    parameters.Add(param);
                    if (wsName != null && !wsName.Equals(""))
                    {
                        command += " and C." + this.WSName + " = @" + this.WSName;
                        param = new Parameter(this.WSName, wsName != null ? wsName.Trim().ToUpper() : null);
                        parameters.Add(param);
                    }
                }
                command += " and A." + this.DomID + " = B." + this.DomID;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();

                if (this.IsXMLTYPE(db))
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("A.OPERATION_ID,A.DOMAIN_ID,A.WEB_SERVICE_ID,A.OPERATION_NAME,A.OPERATION_DESC,");
                    sb.Append("A.OPERATION_TYPE,A.OPERATION_STATUS_CD,A.OPERATION_STATUS_MSG,A.CREATED_DTTM,");
                    sb.Append("A.CREATED_BY,A.UPDATED_DTTM,A.UPDATED_BY,A.VERSION_NO");
                    sb.Append("SYS.XMLTYPE.getClobVal(A.OPERATION_CONFIG) operation_config,A.PUBLISH_IND,A.REST_IND");
                    command = command.Replace("A.*", sb.ToString());
                }

                db.GetDataSet(this.TblOperation, command, parameters, ds);
                if (ds.Tables[this.TblOperation].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[this.TblOperation].Rows[0];
                    retOp = new Operation();
                    retOp.ID = int.Parse(dr[this.OpID].ToString());
                    retOp.Name = dr[this.OpName].ToString();
                    retOp.Type = dr[this.OpType].ToString();
                    XmlDocument config = new XmlDocument();
                    config.LoadXml(dr[this.OpConfig].ToString());
                    retOp.Config = config;
                    retOp.DomainID = int.Parse(dr[this.DomID].ToString());
                    retOp.DomainName = dr[this.DomName].ToString();
                    retOp.DomainStatus = dr[this.DomStatus].ToString();
                    object obj = dr[this.WSID];
                    if (obj != null && !obj.Equals(DBNull.Value))
                    {
                        retOp.WebServiceID = int.Parse(obj.ToString());
                        retOp.WebServiceName = dr[this.WSName].ToString();
                    }
                    retOp.Status = dr[this.OpStatus].ToString();
                    obj = dr[this.OpVersionNo];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.Version = obj.ToString();
                    obj = dr[this.OpMessage];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.Message = obj.ToString();
                    obj = dr[this.OpDesc];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.Description = obj.ToString();
                    obj = dr[this.CreatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.CreatedDate = (DateTime)obj;
                    obj = dr[this.CreatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.CreatedBy = obj.ToString();
                    obj = dr[this.UpdatedDate];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.UpdatedDate = (DateTime)obj;
                    obj = dr[this.UpdatedBy];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.UpdatedBy = obj.ToString();
                    if (retOp.Type.Equals(Phrase.OPERATION_TYPE_SCHEDULED_TASK))
                    {
                        TaskManager manager = new TaskManager("task.config");
                        retOp.Task = manager.GetTask(retOp.Name);
                        if (retOp.Task != null)
                        {
                            retOp.TaskSchedule = new Node.Core.Biz.Objects.TaskSchedule(retOp.Task.Schedule);
                            string[] split = retOp.Task.Parameter.Split(new char[] { ' ' });
                            if (split != null && split.Length > 1 && split[split.Length - 1] != null && split[split.Length - 1].Equals("ONCE"))
                            {
                                retOp.TaskSchedule.Type = Node.Core.Biz.Objects.TaskSchedule.SCHEDULE_TYPE_ONCE;
                            }
                        }
                    }
                    obj = dr[this.OpPublishInd];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.PublishInd = obj.ToString();
                    obj = dr[this.OpRESTInd];
                    if (obj != null && !obj.Equals(DBNull.Value))
                        retOp.RESTInd = obj.ToString();
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
            return retOp;
        }

        private XmlNode CopyTaskStructure(XmlDocument taskDoc, XmlElement toCopy, int id, string opName)
        {
            XmlElement newElement = taskDoc.CreateElement(toCopy.Name);
            foreach (XmlAttribute attr in toCopy.Attributes)
            {
                XmlAttribute newAttr = taskDoc.CreateAttribute(attr.Name);
                if (attr.Name.Equals("status") && toCopy.Name.Equals("Task"))
                    newAttr.Value = "I";
                else
                    newAttr.Value = attr.Value;
                newElement.Attributes.Append(newAttr);
            }
            if (toCopy.ChildNodes.Count == 0 || (toCopy.ChildNodes.Count == 1 && toCopy.ChildNodes[0].NodeType == XmlNodeType.Text))
            {
                if (toCopy.Name.Equals("TaskName"))
                    newElement.InnerText = opName;
                else if (toCopy.Name.Equals("TaskID"))
                    newElement.InnerText = "" + id;
                else
                    newElement.InnerText = toCopy.InnerText;
                return newElement;
            }
            foreach (XmlNode child in toCopy.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element)
                    newElement.AppendChild(this.CopyTaskStructure(taskDoc, (XmlElement)child, id, opName));
            }
            return newElement;
        }
    }
}
