using System;
using System.Collections;
using System.Data;
using System.Xml;
using System.Text;
using System.Data.OracleClient;


using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;
using System.Xml.Linq;

namespace Node.Core.Data.SQLServer
{
    /// <summary>
    /// Database object for retrieving System Configuration for Node Application.
    /// </summary>
    public class Configurations : BaseData, IConfigurations
    {
        private DBAdapter db = null;
        /// <summary>
        /// Constructor of Configurations.
        /// </summary>
        public Configurations()
        {
            this.db = GetNodeDB();
        }
        /// <summary>
        /// Get the Current Status of the Node as Set Through the Configurations Settings Screen
        /// </summary>
        /// <returns>"Running" if Node is Running, "Stopped" Otherwise</returns>
        public string GetNodeStatus()
        {
            string retString = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig + " where ";
                command += this.ConfigName + " = @" + this.ConfigName;
                ArrayList parameters = new ArrayList();
                Parameter param = new Parameter(this.ConfigName, "system.config");
                parameters.Add(param);
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml("" + dt.Rows[0][this.ConfigXml]);
                    XmlNode node = doc.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Status");
                    if (node != null)
                        retString = node.InnerText;
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
            return retString;
        }
        /// <summary>
        /// Get System Configuration File from Database
        /// </summary>
        /// <returns>System Configuration Xml Document</returns>
        public XmlDocument GetSystemConfig()
        {
            XmlDocument retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'system.config'";
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = new XmlDocument();
                    retDoc.LoadXml("" + dt.Rows[0][this.ConfigXml]);
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
        public bool UpdateSystemConfig(XmlDocument xml)
        {
            bool isSuccessful = false;
            try
            {
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'system.config'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    XmlTextWriter writer = new XmlTextWriter(sw);
                    writer.Indentation = 4;
                    writer.Formatting = Formatting.Indented;
                    xml.WriteTo(writer);
                    writer.Flush();
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = sw.ToString();
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
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
        /// Get Task Configuration File from Database
        /// </summary>
        /// <returns>Task Configuration Xml Document</returns>
        public XmlDocument GetTaskConfig()
        {
            XmlDocument retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'task.config'";
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = new XmlDocument();
                    retDoc.LoadXml("" + dt.Rows[0][this.ConfigXml]);
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
        /// Update Task Configuration File in Database
        /// </summary>
        /// <param name="xml">The new Task Configuration file</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool UpdateTaskConfig(XmlDocument xml)
        {
            bool isSuccessful = false;
            try
            {
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'task.config'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    XmlTextWriter writer = new XmlTextWriter(sw);
                    writer.Indentation = 4;
                    writer.Formatting = Formatting.Indented;
                    xml.WriteTo(writer);
                    writer.Flush();
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = sw.ToString();
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
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
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        public XmlDocument GetServiceRegistration()
        {
            XmlDocument retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'getService.xml'";
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = new XmlDocument();
                    retDoc.LoadXml("" + dt.Rows[0][this.ConfigXml]);
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
        /// Update Service Registration File in Database
        /// </summary>
        /// <param name="xml">The new Service Registration file</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool UpdateServiceRegistration(XmlDocument xml)
        {
            bool isSuccessful = false;
            try
            {
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'getService.xml'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    XmlTextWriter writer = new XmlTextWriter(sw);
                    writer.Indentation = 4;
                    writer.Formatting = Formatting.Indented;
                    xml.WriteTo(writer);
                    writer.Flush();
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = sw.ToString();
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
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
        /// Gets the list of config names by specified config type.
        /// </summary>
        /// <param name="type">The config type.</param>
        /// <returns>A DataTable contains the list of config name.</returns>
        public DataTable GetConfigNames(string type)
        {
            ArrayList arr = new ArrayList();
            string sql = "select " + this.ConfigID + "," + this.ConfigName + "," + this.ConfigType + " from " + this.TblConfig;
            if (type != null && type.Trim() != String.Empty)
            {
                sql += " where " + this.ConfigType + "=@configType";
                arr.Add(new Parameter("@configType", type));
            }
            else
                sql += " where " + this.ConfigType + " is not null or " + this.ConfigType + "<> ''";
            sql += " order by " + this.ConfigName;

            DataTable dt = new DataTable();
            this.db.GetDataTable(this.TblConfig, sql, arr, dt);

            return dt;
        }
        /// <summary>
        /// Gets the config file content by config name and config type.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <returns>The  config content.</returns>
        public string GetConfig(string filename, string type)
        {
            string sql = "select " + this.ConfigID + "," + this.ConfigXml + " from " + this.TblConfig +
               " where " + this.ConfigName + "=@configName and " + this.ConfigType + "=@configType";

            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();
            arr.Add(new Parameter("@configType", type));
            arr.Add(new Parameter("@configName", filename));

            this.db.GetDataTable(this.TblConfig, sql, arr, dt);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][this.ConfigXml] + "";
            else
                return "";
        }
        /// <summary>
        /// Gets the config file id by config name and config type.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <returns>The  config id.</returns>
        public string GetConfigID(string filename, string type)
        {
            string sql = "select " + this.ConfigID + "," + this.ConfigXml + " from " + this.TblConfig +
               " where " + this.ConfigName + "=@configName and " + this.ConfigType + "=@configType";

            DataTable dt = new DataTable();
            ArrayList arr = new ArrayList();
            arr.Add(new Parameter("@configType", type));
            arr.Add(new Parameter("@configName", filename));

            this.db.GetDataTable(this.TblConfig, sql, arr, dt);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][this.ConfigID] + "";
            else
                return string.Empty;
        }
        /// <summary>
        /// Deletes a config by config id.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <returns>If successfully deleted, return true; otherwise, return false.</returns>
        public bool DeleteConfig(string id)
        {
            if (this.IsXMLTYPE(db))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete " + this.TblConfig + " where " + this.ConfigID + " = " + id);
                db.ExecuteNonQuery(sb.ToString());
                return true;
            }
            else
            {
                string sql = "select * from " + this.TblConfig + " where " + this.ConfigID + "=@configId";

                DataTable dt = new DataTable();
                ArrayList arr = new ArrayList();
                arr.Add(new Parameter("@configId", id));

                try
                {
                    this.db.BeginTransaction();
                    this.db.GetDataTable(this.TblConfig, sql, arr, dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dt.Rows[0].Delete();
                        this.db.UpdateDataTable(this.TblConfig, dt);
                    }
                    this.db.CommitTransaction();
                    return true;
                }
                catch (Exception ex)
                {
                    this.db.RollbackTransaction();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Saves the config file.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <param name="content">The config content.</param>
        /// <returns>If successfully saved, return true; otherwise, return false.</returns>
        public bool SaveConfig(string id, string filename, string type, string content)
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
        /// Adds a new config to database.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config type.</param>
        /// <param name="content"></param>
        /// <returns>The config id newly added.</returns>
        public int AddConfig(string filename, string type, string content)
        {
            bool bXMLType = false;
            string sql = "select * from " + this.TblConfig + " where " + this.ConfigID + "=-1";
            if (this.IsXMLTYPE(db))
            {
                sql = "select CONFIG_ID,CONFIG_NAME,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY,SYS.XMLTYPE.getClobVal(CONFIG_XML) CONFIG_XML,CONFIG_CLOB from " + this.TblConfig + " where " + this.ConfigID + "=-1";
                bXMLType = true;
            }
            DataTable dt = new DataTable();
            try
            {
                this.db.BeginTransaction();
                this.db.GetDataTable(this.TblConfig, sql, dt);
                int nextId = 0;
                if (dt != null)
                {
                    string query = "select max(" + this.ConfigID + ") from " + this.TblConfig;
                    object obj = this.db.ExecuteScalar(query);
                    
                    int.TryParse(obj + "", out nextId);
                    nextId = nextId + 1;

                    DataRow row = dt.NewRow();
                    row[this.ConfigID] = nextId;
                    row[this.ConfigName] = filename;
                    row[this.ConfigType] = type;
                    row[this.ConfigXml] = content;
                    row[this.Status] = "A";
                    row[this.CreatedDate] = DateTime.Now.ToString();
                    row[this.CreatedBy] = "system";
                    row[this.UpdatedDate] = DateTime.Now.ToString();
                    row[this.UpdatedBy] = "system";
                    if (bXMLType)
                    {
                        row["CONFIG_CLOB"] = content;
                    }
                    dt.Rows.Add(row);

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
                    sb.Append(" = " + nextId);
                    db.ExecuteNonQuery(sb.ToString());
                    sb.Replace(this.ConfigXml, "CONFIG_CLOB");
                    sb.Replace("SYS.XMLTYPE.CREATEXML(CONFIG_CLOB)", "NULL");
                    db.ExecuteNonQuery(sb.ToString());
                }

                return nextId;
            }
            catch (Exception ex)
            {
                this.db.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        public string GetENDSServiceRegistration()
        {
            string retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'ENDv2ServiceRegistrion.xml'";
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = "" + dt.Rows[0][this.ConfigXml];
                }
                else
                {
                    string sEND = CreateENDSConfig();
                    AddConfig("ENDv2ServiceRegistrion.xml", "XML", sEND);
                    retDoc = sEND;
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
        /// Update Service Registration File in Database
        /// </summary>
        /// <param name="xml">The new Service Registration file</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool UpdateENDSServiceRegistration(string xml)
        {
            bool isSuccessful = false;
            try
            {
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'ENDv2ServiceRegistrion.xml'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = xml;
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
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
        /// Get DEDL File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        public string GetDEDL()
        {
            string retDoc = null;
            try
            {
                string command = "select " + this.ConfigXml + " from " + this.TblConfig;
                command += " where " + this.ConfigName + " = 'DEDL.xml'";
                DataTable dt = new DataTable();
                this.db.GetDataTable(this.TblConfig, command, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDoc = "" + dt.Rows[0][this.ConfigXml];
                }
                else
                {
                    string sDEDL = CreateDEDL();
                    AddConfig("DEDL.xml", "XML", sDEDL);
                    retDoc = sDEDL;
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
        /// Update DEDL File in Database
        /// </summary>
        /// <param name="xml">The new Service Registration file</param>
        /// <returns>true if successful, false otherwise</returns>
        public bool UpdateDEDL(string xml)
        {
            bool isSuccessful = false;
            try
            {
                string sql = "select " + this.ConfigID + ", " + this.ConfigXml + " from " + this.TblConfig;
                sql += " where " + this.ConfigName + " = 'DEDL.xml'";
                DataSet ds = new DataSet();
                this.db.GetDataSet(this.TblConfig, sql, ds);
                if (ds.Tables.Contains(this.TblConfig) && ds.Tables[this.TblConfig].Rows.Count > 0)
                {
                    ds.Tables[this.TblConfig].Rows[0][this.ConfigXml] = xml;
                    if (this.db.UpdateDataSet(this.TblConfig, ds) > 0)
                        isSuccessful = true;
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

        private string CreateENDSConfig()
        {
            string sResult="";
            
            XDocument xdoc = new XDocument();
            xdoc.Declaration = new XDeclaration("1.0", "UTF-8", null);

            XNamespace ends = "http://www.exchangenetwork.net/schema/ends/2";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace schemaLocation = "http://www.exchangenetwork.net/schema/ends/2 http://www.exchangenetwork.net/schema/ends/2/GetServices_v2.0.xsd";

            XElement root = new XElement(ends + "NetworkNodes");
            XAttribute newAtt = new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName);
            root.Add(newAtt);
            newAtt = new XAttribute(xsi + "schemaLocation", schemaLocation.NamespaceName);
            root.Add(newAtt);
            xdoc.Add(root);

            XElement xeNetWork = new XElement("NetworkNodeDetails");

            XElement xe = new XElement("NodeIdentifier");
            xeNetWork.Add(xe);
            xe = new XElement("NodeName");
            xeNetWork.Add(xe);
            xe = new XElement("NodeAddress");
            xeNetWork.Add(xe);
            xe = new XElement("OrganizationIdentifier");
            xeNetWork.Add(xe);
            xe = new XElement("NodeContact");
            xeNetWork.Add(xe);
            xe = new XElement("NodeVersionIdentifier");
            xe.Value = "1.1";
            xeNetWork.Add(xe);
            xe = new XElement("NodeDeploymentTypeCode");
            xeNetWork.Add(xe);
            xe = new XElement("NodeStatus");
            xeNetWork.Add(xe);

            XElement xeBound = new XElement("BoundingBoxDetails");
            xe = new XElement("BoundingCoordinateEast");
            xeBound.Add(xe);
            xe = new XElement("BoundingCoordinateNorth");
            xeBound.Add(xe);
            xe = new XElement("BoundingCoordinateSouth");
            xeBound.Add(xe);
            xe = new XElement("BoundingCoordinateWest");
            xeBound.Add(xe);
            xeNetWork.Add(xeBound);

            root.Add(xeNetWork);

            XElement xeNetWork2 = XElement.Parse(xeNetWork.ToString());
            xeNetWork2.Element("NodeVersionIdentifier").Value = "2.0";
            root.Add(xeNetWork2);
            sResult = xdoc.Declaration.ToString() +  Environment.NewLine +xdoc.ToString();

            return sResult;
        }
        private string CreateDEDL()
        {
            string sResult = "";
            XDocument xdoc = new XDocument();
            xdoc.Declaration = new XDeclaration("1.0", "UTF-8", null);

            XNamespace dedl = "http://www.exchangenetwork.net/schema/dedl/1";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace schemaLocation = "http://www.exchangenetwork.net/schema/dedl/1";


            XElement root = new XElement(dedl+"DataElementList");
            XAttribute newAtt = new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName);
            root.Add(newAtt);
            newAtt = new XAttribute(xsi + "schemaLocation",schemaLocation.NamespaceName);
            root.Add(newAtt);
            xdoc.Add(root);
            sResult = xdoc.Declaration.ToString()+  Environment.NewLine + xdoc.ToString();

            return sResult;
        }
    }
}
