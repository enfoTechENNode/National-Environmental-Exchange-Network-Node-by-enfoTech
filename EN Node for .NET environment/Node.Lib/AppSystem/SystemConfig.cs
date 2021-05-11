#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		SystemConfig.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/27/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Node.Lib.Data;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Represents the management of system configuration.
	/// </summary>
	public class SystemConfig : DataConfigReader
	{
		#region Variable definition
		private static SystemConfig sysConfig = new SystemConfig();
		#endregion

		#region Constructor
		private SystemConfig()
			: base()
		{
			Initialize();
		}
		#endregion

		#region Public functions.
		/// <summary>
		/// Static method to get an Instance of <see cref="EAF.Domain.AppSystem.SystemConfig">SystemConfig</see> object.
		/// </summary>
		/// <returns></returns>
		public static SystemConfig GetInstance()
		{
			return sysConfig;
		}

		/// <summary>
		/// Gets <see cref="System.Xml.XmlDocument">XmlDocument</see> object which contains the system configuration.
		/// </summary>
		/// <param name="name">The specified file name of system config file.</param>
		/// <returns>An <see cref="System.Xml.XmlDocument">XmlDocument</see> object.</returns>
		public XmlDocument GetSystemConfig(string name)
		{
			object obj = base.LoadDataConfig(name);
			if (obj == null || (obj + "").Trim() == "")
				return null;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(obj + "");
			return doc;
		}

		/// <summary>
		/// Sets the system configuration XmlDocument.
		/// </summary>
		/// <param name="name">The specified file name of system config file.</param>
		/// <param name="doc">An <see cref="System.Xml.XmlDocument">XmlDocument</see> contains the system configuration.</param>
		public void SetSystemConfig(string name, XmlDocument doc)
		{
			if (doc == null)
				return;
			base.SaveDataConfig(name, doc.OuterXml);
		}
		#endregion

		#region Private functions.
		private void Initialize()
		{
			base.SourceType = Properties.Settings.Default.SourceType;
            base.DataSource = Properties.Settings.Default.DataSource;
            base.TableName = Properties.Settings.Default.Table;
            base.KeyColumnName = Properties.Settings.Default.KeyColumnName;
            base.KeyValue = Properties.Settings.Default.KeyValue;
            base.SrcColumnName = Properties.Settings.Default.SrcColumnName;
            base.Path = Properties.Settings.Default.Path;
		}
		#endregion
	}
}
