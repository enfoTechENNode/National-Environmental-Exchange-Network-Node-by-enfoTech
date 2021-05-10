using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Text;
using System.Data.OracleClient;

using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;
using Node.Core.Biz.Objects;
using System.Collections.Generic;

namespace Node.Core.Data.SQLServer
{
    /// <summary>
    /// Data Access Object for Operation Manager related funcation.
    /// </summary>
    public class OperationManager : BaseData, IOperationManager
    {
        private DBAdapter db = null;
        /// <summary>
        /// Constructor for OperationManager
        /// </summary>
        /// <returns>System Configuration Xml Document</returns>
        public OperationManager()
        {
            this.db = GetNodeDB();
        }
        /// <summary>
        /// Get System Configuration File from Database
        /// </summary>
        /// <returns>System Configuration Xml Document</returns>
        public XmlDocument GetOperationManagerConfig()
        {
            XmlDocument retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'OperationManager.xml'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, command, ds);
                DataTable dt = ds.Tables[this.TblConfig];
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = new XmlDocument();
                    retDoc.LoadXml("" + dt.Rows[0][this.ConfigXml]);
                }
                else
                {
                    command = "select * from " + this.TblConfig + " where 1<>1";
                    ds.Tables.Clear();
                    this.db.GetDataSet(this.TblConfig, command, ds);
                    dt = ds.Tables[this.TblConfig];
                    retDoc = new XmlDocument();
                    XmlDeclaration newDec = retDoc.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
                    retDoc.AppendChild(newDec);
                    XmlNode newNode = retDoc.CreateNode(XmlNodeType.Element, "OperationList", string.Empty);
                    retDoc.AppendChild(newNode);
                    string sXML = retDoc.OuterXml;

                    int nextId = base.GetSequenceNumber(this.TblConfig, this.ConfigID);
                    string query = "select max(" + this.ConfigID + ") from " + this.TblConfig;
                    object obj = this.db.ExecuteScalar(query);

                    int.TryParse(obj + "", out nextId);
                    nextId = nextId + 1;


                    DataRow newRow = dt.NewRow();
                    newRow[this.ConfigID] = nextId;
                    newRow[this.ConfigName] = "OperationManager.xml";
                    newRow[this.ConfigType] = DBNull.Value;
                    newRow[this.ConfigXml] = sXML;
                    newRow[this.Status] = "A";
                    newRow[this.UpdatedBy] = "admin";
                    newRow[this.UpdatedDate] = DateTime.Now.ToString();
                    newRow[this.CreatedBy] = "admin";
                    newRow[this.CreatedDate] = DateTime.Now.ToString();

                    dt.Rows.Add(newRow);

                    this.db.UpdateDataSet(this.TblConfig, ds);
                    this.db.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    this.db.Close();
            }
            return retDoc;
        }
        /// <summary>
        /// Update System Configuration File in Database
        /// </summary>
        /// <param name="xml">The new System Configuration file</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool UpdateOperationManagerConfig(XmlDocument xml)
        {
            bool isSuccessful = false;
            try
            {
                bool bXMLType = false;
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'OperationManager.xml'";
                if (this.IsXMLTYPE(db))
                {
                    sql = "select CONFIG_ID,SYS.XMLTYPE.getClobVal(CONFIG_XML) CONFIG_XML,CONFIG_CLOB from " + this.TblConfig + " where " + this.ConfigName + " = 'OperationManager.xml'";
                    bXMLType = true;
                }

                DataSet ds = new DataSet();
                string id = "";
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = xml.OuterXml;
                    if (bXMLType)
                    {
                        ds.Tables[this.TblConfig].Rows[0]["CONFIG_CLOB"] = xml.OuterXml; ;
                        id = ""+ds.Tables[this.TblConfig].Rows[0][this.ConfigID];
                    }
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
                }

                if (bXMLType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" update ");
                    sb.Append(this.TblConfig);
                    sb.Append(" set ");
                    sb.Append(this.ConfigXml);
                    sb.Append(" = SYS.XMLTYPE.CREATEXML(CONFIG_CLOB)");
                    sb.Append(" where ");
                    sb.Append(this.ConfigID);
                    sb.Append(" = " + id);
                    db.ExecuteNonQuery(sb.ToString());
                    sb.Replace(this.ConfigXml, "CONFIG_CLOB");
                    sb.Replace("SYS.XMLTYPE.CREATEXML(CONFIG_CLOB)", "NULL");
                    db.ExecuteNonQuery(sb.ToString());
                }



            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    this.db.Close();
            }
            return isSuccessful;
        }
        /// <summary>
        /// Saves the config file.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <param name="content">The config content.</param>
        /// <returns>If successfully saved, return true; otherwise, return false.</returns>
        public bool SaveOperationManagerConfig(string id, string filename, string type, string content)
        {
            bool bXMLType = false;
            string sql = "select * from " + this.TblConfig + " where " + this.ConfigID + "=@configId";
            if (this.IsXMLTYPE(db))
            {
                sql = "select CONFIG_ID,CONFIG_NAME,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY,SYS.XMLTYPE.getClobVal(CONFIG_XML) CONFIG_XML,CONFIG_CLOB from " + this.TblConfig + " where " + this.ConfigID + "=@configId";
                bXMLType = true;
            }
            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();
            arr.Add(new Parameter("@configId", id));

            try
            {
                this.db.BeginTransaction();
                this.db.GetDataTable(this.TblConfig, sql, arr, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows[0][this.ConfigName] = filename;
                    dt.Rows[0][this.ConfigType] = type;
                    dt.Rows[0][this.ConfigXml] = content;
                    dt.Rows[0][this.UpdatedDate] = DateTime.Now.ToString();
                    if (bXMLType)
                    {
                        dt.Rows[0]["CONFIG_CLOB"] = content;
                    }
                    this.db.UpdateDataTable(this.TblConfig, dt);
                }
                this.db.CommitTransaction();

                if (bXMLType)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" update ");
                    sb.Append(this.TblConfig);
                    sb.Append(" set ");
                    sb.Append(this.ConfigXml);
                    sb.Append(" = SYS.XMLTYPE.CREATEXML(CONFIG_CLOB)");
                    sb.Append(" where ");
                    sb.Append(this.ConfigID);
                    sb.Append(" = " + id);
                    db.ExecuteNonQuery(sb.ToString());
                    sb.Replace(this.ConfigXml, "CONFIG_CLOB");
                    sb.Replace("SYS.XMLTYPE.CREATEXML(CONFIG_CLOB)", "NULL");
                    db.ExecuteNonQuery(sb.ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                this.db.RollbackTransaction();
                throw ex;
            }
        }
        /// <summary>
        /// Get Documents by Operation ID
        /// </summary>                                      
        /// <param name="opID">Operation ID</param>
        /// <param name="opName">Operation Name</param> 
        /// <returns>Data Table of File Cabinet.</returns>
        public DataTable GetDocumentsByOperationID(string opID,string opName)
        {
            DataTable retDT = null;
            DBAdapter db = null;
            try
            {
                db = GetNodeDB();
                string sSql = " SELECT distinct " + this.TblOperationLogParam + "." + this.ParamName + " FROM " + this.TblOperationLog + " INNER JOIN " + this.TblOperationLogParam +
                                " ON " + this.TblOperationLog + "." + OpLogID + " = " + this.TblOperationLogParam + "." + this.OpLogID +
                                " WHERE " + this.TblOperationLog + "." + this.OpID + "=@" + this.OpID;
                                
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.OpID,opID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, sSql, parameters, dt);
                
                string prefixSql = " SELECT FResult.FILE_ID,FResult.FILE_NAME,FResult.TRANS_ID,";

                if (dt != null && dt.Rows.Count > 0)
                {
                    sSql = "select * from (";
                    sSql = sSql + " SELECT a.FILE_ID,a.FILE_NAME,b.TRANS_ID, ";

                    foreach (DataRow aRow in dt.Rows)
                    {
                       
                        if (aRow[0] != DBNull.Value && CheckParameters(aRow[0].ToString()))
                        {
                            prefixSql = prefixSql + "FResult.[" + aRow[0]+"],";
                            string sSql2 = " (SELECT d.PARAMETER_VALUE FROM NODE_OPERATION_LOG_PARAMETER d WHERE d.PARAMETER_NAME = '" + aRow[0] + "' and c.OPERATION_LOG_ID = d.OPERATION_LOG_ID) [" + aRow[0] + "],";
                            sSql = sSql + sSql2;
                        }
                    }
                    sSql = sSql + " a.DATAFLOW_NAME,";
                    sSql = sSql + " a.CREATED_DTTM";

                    sSql = sSql + " FROM NODE_OPERATION_LOG_PARAMETER c "; 
                    sSql = sSql + " INNER JOIN	NODE_OPERATION_LOG  b ON c.OPERATION_LOG_ID = b.OPERATION_LOG_ID"; 
                    sSql = sSql + " INNER JOIN  NODE_FILE_CABIN   a ON b.TRANS_ID = a.TRANS_ID";
                    sSql = sSql + " WHERE a.DATAFLOW_NAME = '"+opName+"'";
                    sSql = sSql + " Group by a.FILE_ID,a.CREATED_DTTM,b.TRANS_ID,a.FILE_NAME,c.OPERATION_LOG_ID,a.DATAFLOW_NAME) result ";

                    prefixSql = prefixSql + "( SELECT STATUS_CD FROM NODE_OPERATION_MANAGER WHERE submit_id = (select max(submit_id) from NODE_OPERATION_MANAGER WHERE TRANS_ID = FResult.TRANS_ID)) Status,";
                    prefixSql = prefixSql + "( SELECT SUBMITTED_DTTM FROM NODE_OPERATION_MANAGER WHERE submit_id = (select max(submit_id) from NODE_OPERATION_MANAGER WHERE TRANS_ID = FResult.TRANS_ID)) [Submitted Date],";
                    sSql = prefixSql + "FResult.CREATED_DTTM [Created Date] from (" + sSql + ") FResult";

                    dt = new DataTable();

                    if (db.ProviderName.Trim() == DBAdapter.Oracle_Provider)
                    {
                        sSql = sSql.Replace("[", "\"");
                        sSql = sSql.Replace("]", "\"");
                    }

                    db.GetDataTable(this.TblFileCabin, sSql,dt);
                    if (dt != null && dt.Rows.Count > 0)
                        retDT = dt;
                }
                else
                {
                    string command = "select A." + this.FileID + ", A." + this.TransID + ", A." + this.FileName;
                    command += ", A." + this.FileType + ", A." + this.Status + ", A." + this.DataFlowName;
                    command += ", A." + this.SubmitURL + ", A." + this.SubmitToken;
                    command += ", A." + this.SubmitDate;
                    command += ", A." + this.Size + ", A." + this.CreatedDate + ", A." + this.CreatedBy;
                    command += ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                    command += " from " + this.TblFileCabin + " A ";
                    command += " where A." + this.DataFlowName + " = @" + this.DataFlowName;
                    parameters = new ArrayList();
                    parameters.Add(new Parameter(this.DataFlowName, opName));
                    dt = new DataTable();
                    db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                    if (dt != null && dt.Rows.Count > 0)
                        retDT = dt;
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
            return retDT;

        }
        /// <summary>
        /// Get Operation Parameter Name
        /// </summary>
        /// <param name="opID">The operation ID</param>
        /// <returns>A list of paramters name</returns>
        public List<string> GetParametersName(string opID)
        { 
            DBAdapter db = null;
            List<string> result = new List<string>();
            try
            {
                db = GetNodeDB();
                string sSql = " SELECT distinct " + this.TblOperationLogParam + "." + this.ParamName + " FROM " + this.TblOperationLog + " INNER JOIN " + this.TblOperationLogParam +
                                " ON " + this.TblOperationLog + "." + OpLogID + " = " + this.TblOperationLogParam + "." + this.OpLogID +
                                " WHERE " + this.TblOperationLog + "." + this.OpID + "=@" + this.OpID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.OpID, opID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, sSql, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {   
                    foreach(DataRow aRow in dt.Rows)
                    {
                        if (aRow[0] != DBNull.Value && this.CheckParameters(aRow[0].ToString()))
                        {
                            result.Add(aRow[0].ToString());
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
            return result; 
        }
        /// <summary>
        /// Get Operation Parameter value by Parameter name
        /// </summary>
        /// <param name="opID">The operation id</param>
        /// <param name="paraName">The name of parameter</param>
        /// <returns></returns>
        public DataTable GetParameterValue(string opID, string paraName)
        {
            DBAdapter db = null;
            DataTable dt = null;
            try
            {
                db = GetNodeDB();
                string sSql =   " SELECT distinct NODE_OPERATION_LOG_PARAMETER.PARAMETER_VALUE "+
                                " FROM NODE_OPERATION "+
                                " INNER JOIN NODE_OPERATION_LOG ON NODE_OPERATION.OPERATION_ID = NODE_OPERATION_LOG.OPERATION_ID "+
                                " INNER JOIN NODE_OPERATION_LOG_PARAMETER ON NODE_OPERATION_LOG.OPERATION_LOG_ID = NODE_OPERATION_LOG_PARAMETER.OPERATION_LOG_ID "+
                                " WHERE (NODE_OPERATION_LOG.OPERATION_ID = " + opID + ") AND (NODE_OPERATION_LOG_PARAMETER.PARAMETER_NAME = '" + paraName + "')"; 
                ArrayList parameters = new ArrayList();
                //parameters.Add(new Parameter(this.OpID, opID));
                //parameters.Add(new Parameter(this.OpID, opID));
                dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, sSql, parameters, dt);

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
        /// Create OperationManagerTrans 
        /// </summary>    
        /// <returns>OperationManagerTrans object</returns>
        public OperationManagerTrans CreateOperationManagerTrans()
        {
            OperationManagerTrans obj = null;
            try
            {

                string sSql = "select * from NODE_OPERATION_MANAGER where 1 <> 1";
                DataSet ds = new DataSet();

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, ds);

                DataRow newRow = ds.Tables[0].NewRow();
                int id = this.GetSequenceNumber("NODE_OPERATION_MANAGER", "SUBMIT_ID"); 
                newRow["SUBMIT_ID"] = id;
                ds.Tables[0].Rows.Add(newRow);
                if (db.UpdateDataSet("NODE_OPERATION_MANAGER", ds) > 0)
                {
                    obj = new OperationManagerTrans();
                    obj.ID = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close(); 
            }
            return obj;
        }
        /// <summary>
        /// Create OperationManagerTrans by Operation
        /// </summary>
        /// <param name="omtObject">OperationManagerTrans object</param>       
        /// <returns>OperationManagerTrans object</returns>
        public void UpdateOperationManagerTrans(OperationManagerTrans omtObject)
        {
            try
            {
                string sSql = "select * from NODE_OPERATION_MANAGER where SUBMIT_ID = @SUBMIT_ID";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("@SUBMIT_ID", omtObject.ID));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow aRow = ds.Tables[0].Rows[0];
                    aRow["OPERATION_NAME"] = (aRow["OPERATION_NAME"] == DBNull.Value ? omtObject.OperationName : aRow["OPERATION_NAME"]);
                    aRow["SUBMITTED_DTTM"] = (aRow["SUBMITTED_DTTM"] == DBNull.Value ? omtObject.SubmittedDate : aRow["SUBMITTED_DTTM"]);
                    aRow["SUBMITTED_URL"] = (aRow["SUBMITTED_URL"] == DBNull.Value ? omtObject.SubmittedURL : aRow["SUBMITTED_URL"]);
                    aRow["VERSION_NO"] = (aRow["VERSION_NO"] == DBNull.Value ? omtObject.NodeVersion : aRow["VERSION_NO"]);
                    aRow["TRANS_ID"] = (aRow["TRANS_ID"] == DBNull.Value ? omtObject.TransID : aRow["TRANS_ID"]);
                    aRow["SUPPLIED_TRANS_ID"] = (aRow["SUPPLIED_TRANS_ID"] == DBNull.Value ? omtObject.TransIDSupplied : aRow["SUPPLIED_TRANS_ID"]);

                    aRow["STATUS_CD"] = omtObject.Status != "" ? omtObject.Status : aRow["STATUS_CD"];
                    aRow["FILE_CONTENT"] = omtObject.FileContent != null ? omtObject.FileContent : aRow["FILE_CONTENT"];
                    aRow["DATA_FLOW"] = omtObject.DataFlow != "" ? omtObject.DataFlow : aRow["DATA_FLOW"];
                    db.UpdateDataSet("NODE_OPERATION_MANAGER", ds);
    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }                                                                                                            
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }
        }
        /// <summary>
        /// Get OperationManagerTrans by ID
        /// </summary>
        /// <param name="opName">Operation Name</param>       
        /// <returns>DataTable object</returns>
        public DataTable GetOperationManagerTransByOpName(string opName)
        {
            DataTable dt = null;
            try
            {

                string sSql = "select * from NODE_OPERATION_MANAGER where OPERATION_NAME = @OPERATION_NAME and STATUS_CD=@STATUS_CD";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("OPERATION_NAME", opName));
                parameters.Add(new Parameter("STATUS_CD",Phrase.STATUS_SUBMITTED));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0]; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }


            return dt;
        }
        /// <summary>
        /// Get OperationManagerTrans by Status
        /// </summary>
        /// <param name="Status">Status</param>       
        /// <returns>DataTable object</returns>
        public DataTable GetOperationManagerTransByStatus(string Status)
        {
            DataTable dt = null;
            try
            {

                string sSql = "select * from NODE_OPERATION_MANAGER where STATUS_CD=@STATUS_CD order by OPERATION_NAME";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("STATUS_CD", Status));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }


            return dt;
        }
        /// <summary>
        /// Get Check OperationManagerTrans for DownloadReport 
        /// </summary>
        /// <param name="opName">Operation Name</param> 
        /// <param name="transID">Transaction ID</param>
        /// <returns>boolean</returns>
        public bool CheckOperationManagerTransDownloadReport(string opName, string transID)
        {
            bool bFlag = false; 
            try
            {

                string sSql = "select SUBMIT_ID from NODE_OPERATION_MANAGER where ";
                sSql = sSql + "OPERATION_NAME = @OPERATION_NAME and TRANS_ID = @TRANS_ID ";
                sSql = sSql + "and FILE_CONTENT IS NOT NULL and STATUS_CD <> 'Submitted'";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("@OPERATION_NAME", opName));
                parameters.Add(new Parameter("@TRANS_ID", transID));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);
                if (ds.Tables[0].Rows.Count > 0)
                    bFlag = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }
            return bFlag;
        }
        /// <summary>
        /// Get OperationManagerTrans by ID
        /// </summary>
        /// <param name="id">OperationManagerTrans id</param>       
        /// <returns>OperationManagerTrans object</returns>
        public OperationManagerTrans GetOperationManagerTrans(int id)
        {
            OperationManagerTrans obj = null;
            try
            {

                string sSql = "select * from NODE_OPERATION_MANAGER where SUBMIT_ID = @SUBMIT_ID";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("@SUBMIT_ID", id));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow aRow = ds.Tables[0].Rows[0];

                    obj = new OperationManagerTrans();
                    obj.ID = int.Parse(""+aRow["SUBMIT_ID"]);
                    obj.OperationName = aRow["OPERATION_NAME"] != DBNull.Value ? aRow["OPERATION_NAME"].ToString() : "";
                    obj.Status = aRow["STATUS_CD"] != DBNull.Value ? aRow["STATUS_CD"].ToString() : "";
                    obj.SubmittedDate = aRow["SUBMITTED_DTTM"] != DBNull.Value ? (DateTime)aRow["SUBMITTED_DTTM"] : (DateTime?)null;
                    obj.SubmittedURL = aRow["SUBMITTED_URL"] != DBNull.Value ? aRow["SUBMITTED_URL"].ToString() : "";
                    obj.NodeVersion = aRow["VERSION_NO"] != DBNull.Value ? aRow["VERSION_NO"].ToString() : "";
                    obj.TransID = aRow["TRANS_ID"] != DBNull.Value ? aRow["TRANS_ID"].ToString() : "";
                    obj.TransIDSupplied = aRow["SUPPLIED_TRANS_ID"] != DBNull.Value ? aRow["SUPPLIED_TRANS_ID"].ToString() : "";
                    obj.FileContent = aRow["FILE_CONTENT"] != DBNull.Value ? (byte[])aRow["FILE_CONTENT"] : null;
                    obj.DataFlow = aRow["DATA_FLOW"] != DBNull.Value ? aRow["DATA_FLOW"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }
            return obj;
        }
        /// <summary>
        /// Get OperationManagerTrans by Operation Name and Transaction ID
        /// </summary>
        /// <param name="opName">Operation Name</param> 
        /// <param name="transID">Transaction ID</param>      
        /// <returns>OperationManagerTrans object</returns>
        public OperationManagerTrans GetOperationManagerTrans(string opName, string transID)
        {
            OperationManagerTrans obj = null;
            try
            {

                string sSql = "select * from NODE_OPERATION_MANAGER where ";
                sSql = sSql + "OPERATION_NAME = @OPERATION_NAME and TRANS_ID = @TRANS_ID";
                sSql = sSql + " AND STATUS_CD in ('"+Phrase.STATUS_FAILED+"','"+Phrase.STATUS_COMPLETED+"')";
                sSql = sSql +" ORDER BY SUBMIT_ID DESC ";
                DataSet ds = new DataSet();

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("@OPERATION_NAME", opName));
                parameters.Add(new Parameter("@TRANS_ID", transID));

                db.GetDataSet("NODE_OPERATION_MANAGER", sSql, parameters, ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow aRow = ds.Tables[0].Rows[0];

                    obj = new OperationManagerTrans();
                    obj.ID = int.Parse("" + aRow["SUBMIT_ID"]); 
                    obj.OperationName = aRow["OPERATION_NAME"] != DBNull.Value ? aRow["OPERATION_NAME"].ToString() : "";
                    obj.Status = aRow["STATUS_CD"] != DBNull.Value ? aRow["STATUS_CD"].ToString() : "";
                    obj.SubmittedDate = aRow["SUBMITTED_DTTM"] != DBNull.Value ? (DateTime)aRow["SUBMITTED_DTTM"] : (DateTime?)null;
                    obj.SubmittedURL = aRow["SUBMITTED_URL"] != DBNull.Value ? aRow["SUBMITTED_URL"].ToString() : "";
                    obj.NodeVersion = aRow["VERSION_NO"] != DBNull.Value ? aRow["VERSION_NO"].ToString() : "";
                    obj.TransID = aRow["TRANS_ID"] != DBNull.Value ? aRow["TRANS_ID"].ToString() : "";
                    obj.TransIDSupplied = aRow["SUPPLIED_TRANS_ID"] != DBNull.Value ? aRow["SUPPLIED_TRANS_ID"].ToString() : "";
                    obj.FileContent = aRow["FILE_CONTENT"] != DBNull.Value ? (byte[])aRow["FILE_CONTENT"] : null;
                    obj.DataFlow = aRow["DATA_FLOW"] != DBNull.Value ? aRow["DATA_FLOW"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.db != null)
                    this.db.Close();
            }
            return obj;
        }

        private bool CheckParameters(string sInput)
        {
            bool bFlag = true;

            if (sInput.Contains("."))
                bFlag = false;
            switch (sInput)
            {
                case "DataFlow":
                case "Documents":
                case "Document":
                case "Request":
                case "Parameters":
                    bFlag = false;
                    break;
            }
            return bFlag;
        }
    }
}
