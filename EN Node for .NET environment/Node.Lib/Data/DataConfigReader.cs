#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		DataConfigReader.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.41115)
// History:		01/26/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Web.Hosting;
using System.Xml.XPath;
using System.Deployment.Application;

namespace Node.Lib.Data
{
	/// <summary>
	/// Represents to get data configuration setting.
	/// </summary>
	public abstract class DataConfigReader
	{
		/// <summary>
		/// Constant variable of Source_Type_File.
		/// </summary>
		public const string Source_Type_File = "file";
		/// <summary>
		/// Constant variable of Source_Type_Database.
		/// </summary>
		public const string Source_Type_Database = "database";

		private string sourceType = null;
		private string dataSource = null;
		private string tableName = null;
		private string keyColumnName = null;
		private string keyValue = null;
		private string srcColumnName = null;
		private string path = null;

		private Hashtable htDataConfig = new Hashtable();

		/// <summary>
		/// Initializes a DataConfigReader object.
		/// </summary>
		public DataConfigReader()
		{
		}

		#region Public properties
		/// <summary>
		/// Gets or sets the source type. It should be file or database.
		/// </summary>
		public string SourceType
		{
			get { return this.sourceType; }
			set { this.sourceType = value; }
		}

		/// <summary>
		/// Gets or sets the data source. It should be a name defined in connection setting section in web.config or app.config.
		/// </summary>
		public string DataSource
		{
			get { return this.dataSource; }
			set { this.dataSource = value; }
		}

		/// <summary>
		/// Gets or sets the table name.
		/// </summary>
		public string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		/// <summary>
		/// Gets or sets the column name of key.
		/// </summary>
		public string KeyColumnName
		{
			get { return this.keyColumnName; }
			set { this.keyColumnName = value; }
		}

		/// <summary>
		/// Gets or sets the value of key.
		/// </summary>
		public string KeyValue
		{
			get { return this.keyValue; }
			set { this.keyValue = value; }
		}

		/// <summary>
		/// Gets or sets the column name of config file.
		/// </summary>
		public string SrcColumnName
		{
			get { return this.srcColumnName; }
			set { this.srcColumnName = value; }
		}

		/// <summary>
		/// Gets or sets the file path. Used for file source type.
		/// </summary>
		public string Path
		{
			get { return this.path; }
			set { this.path = value; }
		}
		#endregion

		#region Public functions
		/// <summary>
		/// Gets the data config by name.
		/// </summary>
		/// <param name="name">The key value of file name defined in the settings.</param>
		/// <returns>A string value contains the configuration.</returns>
		public virtual string GetDataConfig(string name)
		{
			// if already loaded, then get it from Hashtable
			if (this.htDataConfig.ContainsKey(name))
				return (string)this.htDataConfig[name];

			// if not, then get it from data source or file system.
			object obj = LoadDataConfig(name);
			if (obj != null)
				this.htDataConfig.Add(name, obj);

			return (string)obj;
		}

		/// <summary>
		/// Gets <see cref="System.Xml.XmlDocument">XmlDocument</see> object which contains the data configuration.
		/// </summary>
		/// <param name="name">The specified name of data configuration. The name is the key value of file name defined in the setting config file.</param>
		/// <returns>A <see cref="System.Xml.XmlDocument">XmlDocument</see> object.</returns>
		public virtual XmlDocument GetXmlDataConfig(string name)
		{
			// if already loaded, then get it from Hashtable
			if (this.htDataConfig.ContainsKey(name))
				return (XmlDocument)this.htDataConfig[name];

			// if not, then get it from data source or file system.
			object obj = LoadDataConfig(name);
			if (obj != null)
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml((string)obj);
				this.htDataConfig.Add(name, doc);
				return doc;
			}
			else
				return null;
		}

		/// <summary>
		/// Gets <see cref="System.Xml.XPath.XPathDocument">XPathDocument</see> object which contains the data configuration.
		/// </summary>
		/// <param name="name">The specified name of data configuration. The name is the key value of file name defined in the setting config file.</param>
		/// <returns>A <see cref="System.Xml.XPath.XPathDocument">XPathDocument</see> object.</returns>
		public virtual XPathDocument GetXPathDataConfig(string name)
		{
			// if already loaded, then get it from Hashtable
			if (this.htDataConfig.ContainsKey(name))
				return (XPathDocument)this.htDataConfig[name];

			// if not, then get it from data source or file system.
			object obj = LoadDataConfig(name);
			if (obj != null)
			{
				XPathDocument doc = new XPathDocument(new StringReader((string)obj));
				this.htDataConfig.Add(name, doc);
				return doc;
			}
			else
				return null;
		}

		/// <summary>
		/// Refreshes the data configuration list.
		/// </summary>
		public void Refresh()
		{
			this.htDataConfig.Clear();
		}	
		#endregion

		#region Protected functions.
		/// <summary>
		/// Load config file by specified name.
		/// </summary>
		/// <param name="name">The name of config file.</param>
		/// <returns>An object value contains the config file.</returns>
		protected object LoadDataConfig(string name)
		{
            if (name == null)
                return null;

			string sourceType = this.SourceType;
			if (sourceType.ToLower() == Source_Type_File)
			{
				string path = this.Path;
				path = (path.EndsWith(@"\")) ? path : path + @"\";
				string fileName = path + name;
				FileInfo fileInfo = new FileInfo(fileName);
                // if can not find the file, then windows application.
                if (!fileInfo.Exists)
                    fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileName);
                // if can not find the file, then try hosting for web application.
                if (!fileInfo.Exists)
                    fileInfo = new FileInfo(HostingEnvironment.ApplicationPhysicalPath + fileName);
                if (!fileInfo.Exists && ApplicationDeployment.IsNetworkDeployed)
                    fileInfo = new FileInfo(ApplicationDeployment.CurrentDeployment.DataDirectory + fileName);
                if (!fileInfo.Exists)
                    throw new DataExceptionHandler("Can not find file '" + fileInfo.FullName + "'.");

                string extension = fileInfo.Extension.ToUpper().Trim();
                if (extension == ".TXT" || extension == ".XML")
                    return File.ReadAllText(fileInfo.FullName);
                else
                    return File.ReadAllBytes(fileInfo.FullName);
			}
			else if (sourceType.ToLower() == Source_Type_Database)
			{
				string sql = "select " + this.SrcColumnName + " from " + this.TableName + " where upper(" + this.KeyColumnName + ")='" + name.ToUpper() + "'";
				
                DBAdapter dbAdapter = new DBAdapter("node");

                if (this.IsXMLTYPE(dbAdapter))
                {
                    sql = "select SYS.XMLTYPE.getClobVal(" + this.SrcColumnName + ") " + this.SrcColumnName + " from " + this.TableName + " where upper(" + this.KeyColumnName + ")='" + name.ToUpper() + "'";

                }

                if (dbAdapter == null || string.IsNullOrEmpty(dbAdapter.ConnectionString))
                {
                    if (System.Web.HttpContext.Current.Session["VERSION_NO"] != null
                        && System.Web.HttpContext.Current.Session["VERSION_NO"].ToString().ToUpper() == "VER_11")
                        dbAdapter = new DBAdapter("node_v11");
                    else
                        dbAdapter = new DBAdapter("node_v20");
                }

                object obj = dbAdapter.ExecuteScalar(sql);
				if (obj == null)
					return null;
				else
					return obj;
			}
			else
				throw new DataExceptionHandler("Unsupported source type defined in configuration file.");
		}

		/// <summary>
		/// Saves config file.
		/// </summary>
		/// <param name="name">The name of config file.</param>
		/// <param name="obj">The object contains the config file content.</param>
		protected void SaveDataConfig(string name, object obj)
		{
			string sourceType = this.SourceType;
			if (sourceType.ToLower() == Source_Type_File)
			{
				string path = this.Path;
				path = (path.EndsWith(@"\")) ? path : path + @"\";
				string fileName = path + name;
				FileInfo fileInfo = new FileInfo(fileName);
				// if can not find the file, then try hosting for web application.
				if (!fileInfo.Exists)
				{
					fileName = HostingEnvironment.ApplicationPhysicalPath + fileName;
				}
				StreamWriter writer = new StreamWriter(fileName);
				writer.Write(obj);
				writer.Close();
			}
			else if (sourceType.ToLower() == Source_Type_Database)
			{
				DBAdapter db = new DBAdapter(this.DataSource);
				try
				{
					string sql = "select * from " + this.TableName + " where " + this.KeyColumnName + "='" + name + "'";
					db.BeginTransaction(IsolationLevel.Serializable);
					DataTable dt = new DataTable();
					db.GetDataTable(this.TableName, sql, dt);
                    if (dt.Rows.Count > 0)
                        dt.Rows[0][this.SrcColumnName] = obj;
                    else
                        throw new DataExceptionHandler("Can not find '" + name + "' in table '" + this.TableName + "'.");
					db.UpdateDataTable(this.tableName, dt);
					db.CommitTransaction();
				}
				catch (Exception ex)
				{
					db.RollbackTransaction();
					throw new DataExceptionHandler(ex.Message, ex);
				}
			}
			else
				throw new DataExceptionHandler("Unsupported source type defined in configuration file.");
		}

        /// <summary>
        /// Checking if NODE_OPERATION table contain Oracle database type XMLTYPE.
        /// </summary>
        /// <param name="db">DBAdapter</param>
        /// <returns>True if XMLTYPE is used.</returns>
        protected bool IsXMLTYPE(DBAdapter db)
        {
            bool bFlag = false;

            try
            {
                if (db.ProviderName == Node.Lib.Data.DBAdapter.Oracle_Provider)
                {
                    string sSql = "select data_type from user_tab_columns where table_name = 'SYS_CONFIG' and column_name='CONFIG_XML'";
                    DataTable dt = new DataTable();
                    db.GetDataTable("user_tab_columns", sSql, dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["data_type"].ToString().ToUpper() == "XMLTYPE")
                            bFlag = true;
                    }
                }
            }
            catch (System.Exception)
            {
                bFlag = false;
            }
            return bFlag;
        }

		#endregion


	}
}
