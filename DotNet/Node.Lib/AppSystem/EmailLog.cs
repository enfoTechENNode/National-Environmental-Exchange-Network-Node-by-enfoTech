#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		EmailLog.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50727)
//				Microsoft .NET Framework 2.0 (Version 2.0.50727)
// History:		04/10/2006 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

using Node.Lib.Data;
using Node.Lib.Audit;

namespace Node.Lib.AppSystem
{
    /// <summary>
    /// The class represents EmailLog class. This class used to log email history. 
    /// All emails will be automatically log into database.
    /// </summary>
    public class EmailLog
    {
        /// <summary>
        /// Represents two email status: Success, Failure.
        /// </summary>
        public enum EmailStatus 
        { 
            /// <summary>
            /// Represents Success status.
            /// </summary>
            Success, 
            /// <summary>
            /// Represents Failure status.
            /// </summary>
            Failure 
        };

        private const string LogCategory = "Email";
        private Log log = null;

        #region Constructor
        /// <summary>
        /// Initializes a EmailLog object.
        /// </summary>
        public EmailLog()
        {
            DBAdapter db = new DBAdapter(Properties.Settings.Default.DataSource);   
            this.log = new Log(db, Properties.Settings.Default.EmailLogTableName, LogLevel.Information);
            Initializes();
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Writes a email log.
        /// </summary>
        /// <param name="message">The email error message or other message.</param>
        /// <param name="detail">The email content or xml string comes from email template.</param>
        /// <param name="status">The status value. It should be EmailStatus.Success or EmailStatus.Failure.</param>
        /// <param name="extraInfo">A Hashtable contains extra information to write.</param>
        public void WriteEmailLog(string message, string detail, EmailStatus status, Hashtable extraInfo)
        {
            if (extraInfo == null)
                extraInfo = new Hashtable();
            extraInfo.Add(Properties.Settings.Default.EmailLogStatus, status.ToString());
            this.log.Write(message, detail, LogManager.Information, LogCategory, extraInfo);
        }

        /// <summary>
        /// Gets logs by specified period and log type.
        /// </summary>
        /// <param name="starttime">The start time.</param>
        /// <param name="endtime">The end time.</param>
        /// <returns>A DataSet contains the log information.</returns>
        public DataSet GetLogs(string starttime, string endtime)
        {
            return GetLogs(starttime, endtime, null);
        }

        /// <summary>
        /// Gets logs by specified period and email status.
        /// </summary>
        /// <param name="starttime">The start time.</param>
        /// <param name="endtime">The end time.</param>
        /// <param name="status">An EmailStatus value to indicate status.</param>
        /// <returns></returns>
        public DataSet GetLogs(string starttime, string endtime, EmailStatus status)
        {
            Hashtable ht = new Hashtable();
            ht.Add(Properties.Settings.Default.EmailLogStatus, status.ToString());
            return GetLogs(starttime, endtime, ht);
        }

        /// <summary>
        /// Gets logs by specified period and log type.
        /// </summary>
        /// <param name="starttime">The start time.</param>
        /// <param name="endtime">The end time.</param>
        /// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
        /// <returns>A DataSet contains the log information.</returns>
        public DataSet GetLogs(string starttime, string endtime, Hashtable extraInfo)
        {
            return this.log.GetLogs(starttime, endtime, LogManager.Information, LogCategory, extraInfo);
        }

        /// <summary>
        /// Deletes logs by specified period and log type.
        /// </summary>
        /// <param name="starttime">The start time.</param>
        /// <param name="endtime">The end time.</param>
        public void DeleteLogs(string starttime, string endtime)
        {
            DeleteLogs(starttime, endtime, null);
        }

        /// <summary>
        /// Deletes logs by specified period and log type.
        /// </summary>
        /// <param name="starttime">The start time.</param>
        /// <param name="endtime">The end time.</param>
        /// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
        public void DeleteLogs(string starttime, string endtime, Hashtable extraInfo)
        {
            this.log.DeleteLogs(starttime, endtime, LogManager.Information, LogCategory, extraInfo);
        }
        #endregion

        #region Private Functions
        private void Initializes()
        {
            string logType = Properties.Settings.Default.EmailLogType;
            string logCat = Properties.Settings.Default.EmailLogCategory;
            string logTableName = Properties.Settings.Default.EmailLogTableName;
            string logID = Properties.Settings.Default.EmailLogID;
            string logMessage = Properties.Settings.Default.EmailLogMessage;
            string logDetail = Properties.Settings.Default.EmailLogDetail;
            string logDateTime = Properties.Settings.Default.EmailLogDateTime;

            this.log.TableName = logTableName;
            this.log.ColumnName_LogID = logID;
            this.log.ColumnName_LogMessage = logMessage;
            this.log.ColumnName_LogDetailMessage = logDetail;
            this.log.ColumnName_LogDateTime = logDateTime;
            this.log.ColumnName_LogType = logType;
            this.log.ColumnName_LogCategory = logCat;
            this.log.Initialize();
        }
        #endregion

    }
}
