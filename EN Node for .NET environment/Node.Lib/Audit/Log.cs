#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		Log.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		05/05/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Collections;
using System.Diagnostics;

using Node.Lib.Data;

namespace Node.Lib.Audit
{
	/// <summary>
	/// Represents the Log class.
	/// </summary>
	public class Log
	{
		#region private variable
		//define constant used by this class.
        /// <summary>
        /// Defines constant value for SaveMode_File. The value will save log to file.
        /// </summary>
        public const string SaveMode_File = "file";
        /// <summary>
        /// Defines constant value for SaveMode_Database. The value will save log to database.
        /// </summary>
        public const string SaveMode_Database = "database";
        /// <summary>
        /// Defines constant value for SaveMode_System. The value will save log to system event log.
        /// </summary>
        public const string SaveMode_System = "system";

		private const string LOG_SET = "DataLOG";
		private const string LOG_ID = "LogID";
		private const string LOG_DATETIME = "LogDateTime";
		private const string LOG_MESG = "LogMessage";
		private const string LOG_DETAIL = "LogDetailMessage";
		private const string LOG_TYPE = "LogType";
		private const string LOG_CATEGORY = "LogCategory";
		private const int INITIAL_LOG_ID = 0;
		private const int LOG_SAVE_IN_FILE = 0;
		private const int LOG_SAVE_IN_DB = 1;
		private const int LOG_SAVE_IN_SYSTEM = 2;
        private const int LOG_SAVE_NOWHERE = 3;

		private string fileName = null;
		private string tableName = LOG_SET;
		private string logID = LOG_ID;
		private string logDateTime = LOG_DATETIME;
		private string logMessage = LOG_MESG;
		private string logDetail = LOG_DETAIL;
		private string logType = LOG_TYPE;
		private string logCategory = LOG_CATEGORY;
        private int logSaveMode = LOG_SAVE_NOWHERE;
		private int logMessageSize = -1;
		private int logDetailSize = -1;

		private DBAdapter db = null;
		private DataSet dataSet = null;
		private LogLevel level = LogLevel.Information;
		#endregion

		#region Constructor
		/// <overloads>This constructor has four overloads.</overloads>
        /// <summary>
        /// Initializes a new instance of the Log. The log information will write to system event log.
        /// </summary>
        public Log()
        {
        }

		/// <summary>
		/// Initializes a new instance of the Log. The log information will write to system event log.
		/// </summary>
        /// <param name="logLevel">The log level.</param>
		public Log(LogLevel logLevel)
		{
			this.level = logLevel;
			this.logSaveMode = LOG_SAVE_IN_SYSTEM;
		}

		/// <summary>
		/// Initializes a new instance of the Log.
		/// </summary>
		/// <param name="fileName">The filename of log file.</param>
		/// <param name="logLevel">The log level.</param>
		public Log(string fileName, LogLevel logLevel)
		{
			this.level = logLevel;
			this.fileName = fileName;
			this.logSaveMode = LOG_SAVE_IN_FILE;
		}

		/// <summary>
		/// Initializes a new instance of the Log.
		/// </summary>
		/// <param name="db">The object contains the database connection information.</param>
		/// <param name="tableName">The table name which saves the log information.</param>
		/// <param name="logLevel">The log level.</param>
		public Log(DBAdapter db, string tableName, LogLevel logLevel)
		{
			this.db = db;
			this.level = logLevel;
			this.tableName = tableName;
			this.logSaveMode = LOG_SAVE_IN_DB;
		}
		#endregion

        #region private static function
        /// <summary>
        /// Gets an log instance.
        /// </summary>
        /// <returns>A Log instance object.</returns>
        public static Log GetInstance()
        {
            Log log = null;
            string logSaveMode = Properties.Settings.Default.LogSaveMode;
            string logFileName = Properties.Settings.Default.LogFileName;
            string logTableName = Properties.Settings.Default.LogTableName;
            string dataSource = Properties.Settings.Default.DataSource;
            string logLevel = Properties.Settings.Default.LogLevel;

            string logID = Properties.Settings.Default.LogID;
            string logMessage = Properties.Settings.Default.LogMessage;
            string logDetail = Properties.Settings.Default.LogDetailMessage;
            string logDateTime = Properties.Settings.Default.LogDateTime;
            string logType = Properties.Settings.Default.LogType;
            string logCat = Properties.Settings.Default.LogCategory;

            LogLevel levelObj = LogLevel.Information;
            // if can not get level, then set LogLevel.Informaiton as default.
            try
            {
                levelObj = (LogLevel)Enum.Parse(typeof(LogLevel), logLevel, true);
            }
            catch { }

            if (logSaveMode.ToLower().Trim() == SaveMode_File)
                log = new Log(logFileName, levelObj);
            else if (logSaveMode.ToLower().Trim() == SaveMode_Database)
                log = new Log(new DBAdapter(dataSource), logTableName, levelObj);
            else if (logSaveMode.ToLower().Trim() == SaveMode_System)
                log = new Log(levelObj);
            else
                log = new Log();

            if (logCat != null && logCat.Trim() != String.Empty)
                log.ColumnName_LogCategory = logCat;

            log.TableName = logTableName;
            log.ColumnName_LogID = logID;
            log.ColumnName_LogMessage = logMessage;
            log.ColumnName_LogDetailMessage = logDetail;
            log.ColumnName_LogDateTime = logDateTime;
            log.ColumnName_LogType = logType;
            log.Initialize();

            return log;
        }
        #endregion

		#region public functions
		/// <summary>
		/// Initializes log.
		/// </summary>
		public void Initialize()
		{
			if (this.logSaveMode == LOG_SAVE_IN_FILE)
				InitLogByFileName();
			else if (this.logSaveMode == LOG_SAVE_IN_DB)
				InitLogByDB();
		}

		/// <overloads>This method has six overloads.</overloads>
		/// <summary>
		/// Writes a message and its detail information into a log file or a log table by a specified log type.
		/// </summary>
		/// <param name="message"><see cref="System.String">string</see>, the short message.</param>
		/// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
		/// <param name="logType"><see cref="System.String">string</see>, the log Type.</param>
		public void Write(string message, string detail, string logType)
		{
			Write(message, detail, logType, "", null);
		}

        /// <overloads>This method has two overloads.</overloads>
        /// <summary>
        /// Writes a message and its detail information into a log file or a log table by a specified log type.
        /// </summary>
        /// <param name="message"><see cref="System.String">string</see>, the short message.</param>
        /// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
        /// <param name="logType"><see cref="EAF.Lib.Audit.LogLevel">LogLevel</see>, the log Type.</param>
        public void Write(string message, string detail, LogLevel logType)
        {
            Write(message, detail, logType, "", null);
        }

		/// <summary>
		/// Writes a message and its detail information into a log file or a log table by a specified log type.
		/// </summary>
		/// <param name="message"><see cref="System.String">string</see>, the short message.</param>
		/// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
		/// <param name="logType"><see cref="System.String">string</see>, the log Type.</param>
		/// <param name="logCategory"><see cref="System.String">string</see>, the log category.</param>
		public void Write(string message, string detail, string logType, string logCategory)
		{
			Write(message, detail, logType, logCategory, null);
		}

        /// <summary>
        /// Writes a message and its detail information into a log file or a log table by a specified log type.
        /// </summary>
        /// <param name="message"><see cref="System.String">string</see>, the short message.</param>
        /// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
        /// <param name="logType"><see cref="EAF.Lib.Audit.LogLevel">LogLevel</see>, the log Type.</param>
        /// <param name="logCategory"><see cref="System.String">string</see>, the log category.</param>
        public void Write(string message, string detail, LogLevel logType, string logCategory)
        {
            Write(message, detail, logType, logCategory, null);
        }

        /// <summary>
		/// Writes a message and its detail information into a log file or a log table by a specified log type.
		/// </summary>
		/// <param name="message"><see cref="System.String">string</see>, the short message.</param>
		/// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
		/// <param name="logType"><see cref="EAF.Lib.Audit.LogLevel">LogLevel</see>, the log Type.</param>
		/// <param name="logCategory"><see cref="System.String">string</see>, the log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
        public void Write(string message, string detail, LogLevel logType, string logCategory, Hashtable extraInfo)
        {
            Write(message, detail, Enum.GetName(typeof(LogLevel), logType), logCategory, extraInfo);
        }

		/// <summary>
		/// Writes a message and its detail information into a log file or a log table by a specified log type.
		/// </summary>
		/// <param name="message"><see cref="System.String">string</see>, the short message.</param>
		/// <param name="detail"><see cref="System.String">string</see>, the detailed information.</param>
		/// <param name="logType"><see cref="System.String">string</see>, the log Type.</param>
		/// <param name="logCategory"><see cref="System.String">string</see>, the log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
		public void Write(string message, string detail, string logType, string logCategory, Hashtable extraInfo)
		{
			message = (message == null) ? "" : message;
			detail = (detail == null) ? "" : detail;
			logType = (logType == null) ? Enum.GetName(typeof(LogLevel), LogLevel.Information) : logType;

			// to see if log type is less than pre-setting log level, then don't log. if log type is not in LogLevel list, then log it anyway.
			foreach (string name in Enum.GetNames(typeof(LogLevel)))
			{
				if (name.ToUpper().Trim() == logType.ToUpper().Trim())
				{
					if (this.level.CompareTo(Enum.Parse(typeof(LogLevel), name)) > 0)
						return;
				}
			}
			if (this.logSaveMode == LOG_SAVE_IN_SYSTEM)
				WriteToEventLog(message, detail, logType, logCategory);
			else if (this.logSaveMode == LOG_SAVE_IN_FILE || this.logSaveMode == LOG_SAVE_IN_DB)
				WriteToFileAndDB(message, detail, logType, logCategory, extraInfo);
		}

		/// <summary>
		/// Reads all log information from a log file or a log table to a <see cref="System.String">string</see>.
		/// </summary>
		/// <returns>A string value contains all log data.</returns>
		public string ReadAll()
		{
			return ReadDataSet().GetXml();
		}

		/// <summary>
		/// Reads all log data from a <see cref="System.Data.DataSet">DataSet</see> object.
		/// </summary>
		/// <returns>A <see cref="System.Data.DataSet">DataSet</see> object contains all log data.</returns>
		public DataSet ReadDataSet()
		{
			if (this.logSaveMode == LOG_SAVE_IN_FILE)
			{
				return this.dataSet;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_DB)
			{
				string sql = "Select * from " + this.tableName;
				DataSet ds = new DataSet();
				this.db.GetDataSet(this.tableName, sql, new ArrayList(), ds);
				return ds;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_SYSTEM)
			{
				DataSet ds = CreateDataSet();
				EventLog log = new EventLog(this.tableName);
				foreach (EventLogEntry entry in log.Entries)
				{
					DataRow row = ds.Tables[this.tableName].NewRow();
					row[this.logID] = entry.Index;
					row[this.logDateTime] = entry.TimeWritten.ToString();
					row[this.logMessage] = entry.Message;
					row[this.logDetail] = new UTF8Encoding().GetString(entry.Data);
					row[this.logType] = entry.Source;
					row[this.logCategory] = entry.CategoryNumber + "";
					ds.Tables[this.tableName].Rows.Add(row);
				}
				return ds;
			}
			return null;
		}

        /// <summary>
		/// Reads all log data from a <see cref="System.Data.DataSet">DataSet</see> object by specified start time and end time.
		/// </summary>
		/// <param name="startdatetime">The start datetime.</param>
		/// <param name="enddatetime">The end datetime.</param>
		/// <param name="logType">The log type.</param>
		/// <param name="logCategory">The log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
		/// <returns>A DataSet contains the log information.</returns>
        public DataSet GetLogs(string startdatetime, string enddatetime, string logType, string logCategory, Hashtable extraInfo)
        {
            return GetLogs(startdatetime, enddatetime, logType, logCategory, extraInfo, true);
        }

		/// <summary>
		/// Reads all log data from a <see cref="System.Data.DataSet">DataSet</see> object by specified start time and end time.
		/// </summary>
		/// <param name="startdatetime">The start datetime.</param>
		/// <param name="enddatetime">The end datetime.</param>
		/// <param name="logType">The log type.</param>
		/// <param name="logCategory">The log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
        /// <param name="bOrderByDesc">Indicates the sort order. If it is true, then order by desc.</param>
		/// <returns>A DataSet contains the log information.</returns>
		public DataSet GetLogs(string startdatetime, string enddatetime, string logType, string logCategory, Hashtable extraInfo, bool bOrderByDesc)
		{
			string expr = "1=1";
			string dateTimeFormat = (this.db != null && this.db.ProviderName == DBAdapter.Oracle_Provider) ? "dd-MMM-yyyy" : "yyyy-MM-dd";

			if (this.logSaveMode == LOG_SAVE_IN_SYSTEM && logCategory != null)
				logCategory = logCategory.Length + "";

			// build where clause.
			if (startdatetime != null && startdatetime.Trim() != "")
				expr += " and " + this.logDateTime + ">='" + DateTime.Parse(startdatetime).ToString(dateTimeFormat) + "'";
			if (enddatetime != null && enddatetime.Trim() != "")
				expr += " and " + this.logDateTime + "<='" + DateTime.Parse(enddatetime).ToString(dateTimeFormat) + "'";
			if (logType != null && logType.Trim() != "")
				expr += " and " + this.logType + "='" + logType + "'";
			if (logCategory != null && logCategory.Trim() != "")
				expr += " and " + this.logCategory + "='" + logCategory + "'";
			if (extraInfo != null && this.logSaveMode == LOG_SAVE_IN_DB)
			{
				foreach (string key in extraInfo.Keys)
					expr += " and " + key + " like ('" + extraInfo[key] + "')";
			}
			if (this.logSaveMode == LOG_SAVE_IN_FILE)
			{
				DataRow[] rows = this.dataSet.Tables[this.tableName].Select(expr, this.logDateTime + (bOrderByDesc? " desc" : ""));
				DataSet ds = CreateDataSet();
				foreach (DataRow row in rows)
					ds.Tables[0].ImportRow(row);
				return ds;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_DB)
			{
				string sql = "Select * from " + this.tableName + " where " + expr + " order by " + (this.logDateTime + (bOrderByDesc? " desc" : ""));
				DataSet ds = new DataSet();
				this.db.GetDataSet(this.tableName, sql, new ArrayList(), ds);
				return ds;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_SYSTEM)
			{
				DataSet ds = ReadDataSet();
                DataRow[] rows = ds.Tables[this.tableName].Select(expr, this.logDateTime + (bOrderByDesc ? " desc" : ""));
				DataSet dSet = CreateDataSet();
				foreach (DataRow row in rows)
					dSet.Tables[0].ImportRow(row);
				return dSet;
			}
			return null;
		}

		/// <summary>
		/// Delete logs by specified start datetime, end datetime, and log type.
		/// </summary>
		/// <param name="startdatetime">The specified start datetime.</param>
		/// <param name="enddatetime">The specified end datetime.</param>
		/// <param name="logType">The log type.</param>
		/// <param name="logCategory">The log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
		public void DeleteLogs(string startdatetime, string enddatetime, string logType, string logCategory, Hashtable extraInfo)
		{
			string expr = "1=1";
			string dateTimeFormat = (this.db != null && this.db.ProviderName == DBAdapter.Oracle_Provider) ? "dd-MMM-yyyy" : "yyyy-MM-dd";

			if (this.logSaveMode == LOG_SAVE_IN_SYSTEM && logCategory != null)
				logCategory = logCategory.Length + "";

			// build where clause.
			if (startdatetime != null && startdatetime.Trim() != "")
				expr += " and " + this.logDateTime + ">='" + DateTime.Parse(startdatetime).ToString(dateTimeFormat) + "'";
			if (enddatetime != null && enddatetime.Trim() != "")
				expr += " and " + this.logDateTime + "<='" + DateTime.Parse(enddatetime).ToString(dateTimeFormat) + "'";
			if (logType != null && logType.Trim() != "")
				expr += " and " + this.logType + "='" + logType + "'";
			if (logCategory != null && logCategory.Trim() != "")
				expr += " and " + this.logCategory + "='" + logCategory + "'";
			if (extraInfo != null && this.logSaveMode == LOG_SAVE_IN_DB)
			{
				foreach (string key in extraInfo.Keys)
					expr += " and " + key + "='" + extraInfo[key] + "'";
			}
			if (this.logSaveMode == LOG_SAVE_IN_FILE)
			{
				DataRow[] rows = this.dataSet.Tables[this.tableName].Select(expr, this.logDateTime);
				foreach (DataRow row in rows)
					row.Delete();
				this.dataSet.AcceptChanges();
				this.dataSet.WriteXml(this.fileName, XmlWriteMode.WriteSchema);
			}
			else if (this.logSaveMode == LOG_SAVE_IN_DB)
			{
				string sql = "delete from " + this.tableName + " where " + expr;
				DataSet ds = new DataSet();
				this.db.ExecuteNonQuery(sql);
			}
			else if (this.logSaveMode == LOG_SAVE_IN_SYSTEM)
			{
				EventLog.Delete(this.tableName);
			}
		}
		#endregion

		#region public properties
		/// <summary>
		/// Gets or sets a log file name.
		/// </summary>
		public string FileName
		{
			get { return this.fileName; }
			set { this.fileName = value; }
		}

		/// <summary>
		/// Gets or sets a table name.
		/// </summary>
		public string TableName
		{
			get { return this.tableName; }
			set { this.tableName = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogID.
		/// </summary>
		public string ColumnName_LogID
		{
			get { return this.logID; }
			set { this.logID = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogDateTime.
		/// </summary>
		public string ColumnName_LogDateTime
		{
			get { return this.logDateTime; }
			set { this.logDateTime = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogMessage.
		/// </summary>
		public string ColumnName_LogMessage
		{
			get { return this.logMessage; }
			set { this.logMessage = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogDetail.
		/// </summary>
		public string ColumnName_LogDetailMessage
		{
			get { return this.logDetail; }
			set { this.logDetail = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogType.
		/// </summary>
		public string ColumnName_LogType
		{
			get { return this.logType; }
			set { this.logType = value; }
		}

		/// <summary>
		/// Gets or sets the column name of LogCategory. This is optional.
		/// </summary>
		public string ColumnName_LogCategory
		{
			get { return this.logCategory; }
			set { this.logCategory = value; }
		}
		#endregion

		#region private functions

		private void InitLogByFileName()
		{
			if (this.fileName == null || this.fileName.Trim() == "")
				throw new AuditExceptionHandler("File name is empty.");

			FileInfo info = new FileInfo(this.fileName);
			if (!info.Exists)
			{
				this.dataSet = CreateDataSet();
				this.dataSet.WriteXml(info.FullName, XmlWriteMode.WriteSchema);
			}
			else
			{
				this.dataSet = new DataSet();
				this.dataSet.ReadXml(this.fileName);
                if (this.dataSet.Tables.Count > 0)
                    this.dataSet.Tables[0].TableName = this.tableName;
			}
		}

		private void InitLogByDB()
		{
			string sql = "Select * from " + this.tableName + " where 1 = -1";
			this.dataSet = new DataSet();
			this.db.GetDataSet(this.tableName, sql, this.dataSet);
			// get the max length for message.
			this.logMessageSize = this.dataSet.Tables[this.tableName].Columns[this.logMessage].MaxLength;
			this.logDetailSize = this.dataSet.Tables[this.tableName].Columns[this.logDetail].MaxLength;
		}

		private int GetMaxLogID()
		{
			if (this.logSaveMode == LOG_SAVE_IN_FILE)
			{
				DataRow[] row = this.dataSet.Tables[this.tableName].Select("1=1", this.logID + " desc");
				if (row.Length == 0)
					return INITIAL_LOG_ID;
				else
					return Convert.ToInt32(row[0][this.logID]) + 1;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_DB)
			{
				string sql = "Select max(" + this.logID + ") from " + this.tableName;
				object obj = this.db.ExecuteScalar(sql, new ArrayList());
				if (obj is DBNull)
					return INITIAL_LOG_ID;
				else
					return Convert.ToInt32(obj) + 1;
			}
			else if (this.logSaveMode == LOG_SAVE_IN_SYSTEM)
				return 100;
			return -1;
		}

		private DataSet CreateDataSet()
		{
			DataTable dt = new DataTable(this.tableName);
			dt.Columns.Add(this.logID, Type.GetType("System.Int32"));
			dt.Columns.Add(this.logDateTime, Type.GetType("System.DateTime"));
			dt.Columns.Add(this.logMessage, Type.GetType("System.String"));
			dt.Columns.Add(this.logDetail, Type.GetType("System.String"));
			dt.Columns.Add(this.logType, Type.GetType("System.String"));
			dt.Columns.Add(this.logCategory, Type.GetType("System.String"));
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add(dt);

			return dataSet;
		}

		private void WriteToEventLog(string message, string detail, string logType, string logCategory)
		{
			EventLog log = new EventLog(this.TableName);
			log.Source = logType;
			UTF8Encoding en = new UTF8Encoding();
			byte[] bb = en.GetBytes(detail);
			EventLogEntryType type = EventLogEntryType.Information;
			switch (logType)
			{
				case "Information": type = EventLogEntryType.Information; break;
				case "Warning": type = EventLogEntryType.Warning; break;
				case "Error": type = EventLogEntryType.Error; break;
			}
			log.WriteEntry(message, type, GetMaxLogID(), (short)logCategory.Length, bb);
		}

		private void WriteToFileAndDB(string message, string detail, string logType, string logCategory, Hashtable extraInfo)
		{
            lock (this)
            {
                try
                {
                    if (this.logSaveMode == LOG_SAVE_IN_DB)
                        this.db.BeginTransaction(IsolationLevel.Serializable);

                    //cut the message, if it is too long.
                    if (message.Length > this.logMessageSize && this.logMessageSize != -1)
                        message = message.Substring(0, this.logMessageSize);
                    if (detail.Length > this.logDetailSize && this.logDetailSize != -1)
                        detail = detail.Substring(0, this.logDetailSize);

                    DataRow row = this.dataSet.Tables[this.tableName].NewRow();
                    row[this.logID] = GetMaxLogID();
                    row[this.logDateTime] = System.DateTime.Now.ToString();
                    row[this.logMessage] = message;
                    row[this.logDetail] = detail;
                    row[this.logType] = logType;
                    if (this.dataSet.Tables[this.tableName].Columns.Contains(this.logCategory))
                        row[this.logCategory] = logCategory;

                    // save extra information
                    if (extraInfo != null)
                    {
                        foreach (string key in extraInfo.Keys)
                        {
                            if (this.dataSet.Tables[this.tableName].Columns.Contains(key))
                                row[key] = extraInfo[key];
                        }
                    }
                    this.dataSet.Tables[this.tableName].Rows.Add(row);

                    if (this.logSaveMode == LOG_SAVE_IN_FILE)
                    {
                        this.dataSet.WriteXml(this.fileName, XmlWriteMode.WriteSchema);
                    }
                    else if (this.logSaveMode == LOG_SAVE_IN_DB)
                    {
                        this.db.UpdateDataSet(this.tableName, this.dataSet);
                        this.db.CommitTransaction();
                    }
                }
                catch (Exception ex)
                {
                    if (this.logSaveMode == LOG_SAVE_IN_DB)
                        this.db.RollbackTransaction();
                    throw new AuditExceptionHandler(ex.Message, ex);
                }
            }
		}
		#endregion
	}
}
