#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		LogManager.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		05/09/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

using Node.Lib.Audit;
using Node.Lib.Data;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Represents the LogManager class.
	/// </summary>
	public class LogManager
	{
		#region public const variable
		/// <summary>
		/// The constant value of Information log type.
		/// </summary>
		public const string Information = "Information";
		/// <summary>
		/// The constant value of Warning log type.
		/// </summary>
		public const string Warning = "Warning";
		/// <summary>
		/// The constant value of Error log type.
		/// </summary>
		public const string Error = "Error";
		/// <summary>
		/// The constant value of Verbose log type.
		/// </summary>
		public const string Verbose = "Verbose";
		/// <summary>
		/// The constant value of Critical log type.
		/// </summary>
		public const string Critical = "Critical";
		#endregion

		private Log log = null;

		/// <summary>
		/// Initializes a LogManager object.
		/// </summary>
		public LogManager()
		{
            this.log = Log.GetInstance();
		}

		#region public function
		/// <summary>
		/// Gets logs by specified period and log type.
		/// </summary>
		/// <param name="starttime">The start time.</param>
		/// <param name="endtime">The end time.</param>
		/// <param name="type">The log type.</param>
		/// <returns>A DataSet contains the log information.</returns>
		public DataSet GetLogs(string starttime, string endtime, string type)
		{
			return GetLogs(starttime, endtime, type, "", null);
		}

		/// <summary>
		/// Gets logs by specified period and log type.
		/// </summary>
		/// <param name="starttime">The start time.</param>
		/// <param name="endtime">The end time.</param>
		/// <param name="type">The log type.</param>
		/// <param name="category">The log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
		/// <returns>A DataSet contains the log information.</returns>
		public DataSet GetLogs(string starttime, string endtime, string type, string category, Hashtable extraInfo)
		{
			return this.log.GetLogs(starttime, endtime, type, category, extraInfo);
		}

		/// <summary>
		/// Deletes logs by specified period and log type.
		/// </summary>
		/// <param name="starttime">The start time.</param>
		/// <param name="endtime">The end time.</param>
		/// <param name="type">The log type.</param>
		public void DeleteLogs(string starttime, string endtime, string type)
		{
			DeleteLogs(starttime, endtime, type, "", null);
		}

		/// <summary>
		/// Deletes logs by specified period and log type.
		/// </summary>
		/// <param name="starttime">The start time.</param>
		/// <param name="endtime">The end time.</param>
		/// <param name="type">The log type.</param>
		/// <param name="category">The log category.</param>
		/// <param name="extraInfo">The Hashtable contains the extran field which want to be logged.</param>
		public void DeleteLogs(string starttime, string endtime, string type, string category, Hashtable extraInfo)
		{
			this.log.DeleteLogs(starttime, endtime, type, category, extraInfo);
		}
		#endregion

		#region public static function
		/// <summary>
		/// A static method to write a message.
		/// </summary>
		/// <param name="message">The message to write.</param>
		/// <param name="detail">The detailed message to write.</param>
		/// <param name="type">The message type.</param>
		public static void WriteMessage(string message, string detail, string type)
		{
			Log.GetInstance().Write(message, detail, type);
		}

		/// <summary>
		/// A static method to write a message.
		/// </summary>
		/// <param name="message">The message to write.</param>
		/// <param name="detail">The detailed message to write.</param>
		/// <param name="type">The message type.</param>
		/// <param name="category">The category name.</param>
		public static void WriteMessage(string message, string detail, string type, string category)
		{
			Log.GetInstance().Write(message, detail, type, category, null);
		}

		/// <summary>
		/// A static method to write a message.
		/// </summary>
		/// <param name="message">The message to write.</param>
		/// <param name="detail">The detailed message to write.</param>
		/// <param name="type">The message type.</param>
		/// <param name="category">The category name.</param>
		/// <param name="extraInfo">The Hashtable contains the extro information. The key is the field name in database.</param>
		public static void WriteMessage(string message, string detail, string type, string category, Hashtable extraInfo)
		{
			Log.GetInstance().Write(message, detail, type, category, extraInfo);
		}
		#endregion
	}
}
