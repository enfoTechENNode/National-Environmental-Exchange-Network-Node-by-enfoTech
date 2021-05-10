#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		Task.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/27/2005 Danwen Sun Creation
// 
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Summary description for Task.
	/// </summary>
	public class Task
	{
		private XmlNode taskNode = null;
		private TaskSchedule schedule = null;

		/// <summary>
		/// Create a Task instance.
		/// </summary>
		protected internal Task(XmlNode taskNode)
		{
			this.taskNode = taskNode;
			this.schedule = new TaskSchedule(this.taskNode.SelectSingleNode(".//Schedule"));
		}

		/// <summary>
		/// Gets or Sets Task Status.
		/// </summary>
		public string Status
		{
			get { return this.taskNode.Attributes.GetNamedItem("status").Value; }
			set { this.taskNode.Attributes.GetNamedItem("status").Value = value; }
		}

		/// <summary>
		/// Gets or Sets Task ID.
		/// </summary>
		public string ID
		{
			get { return this.taskNode.SelectSingleNode(".//TaskID").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskID").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets Task Name.
		/// </summary>
		public string Name
		{
			get { return this.taskNode.SelectSingleNode(".//TaskName").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskName").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets Task Mode.
		/// </summary>
		public string Mode
		{
			get { return this.taskNode.SelectSingleNode(".//TaskMode").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskMode").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the full path of Task executable file.
		/// </summary>
		public string FullPath
		{
			get { return this.taskNode.SelectSingleNode(".//TaskFullPath").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskFullPath").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets Task Parameter.
		/// </summary>
		public string Parameter
		{
			get { return this.taskNode.SelectSingleNode(".//TaskParameter").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskParameter").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets Task Type.
		/// </summary>
		public string Type
		{
			get { return this.taskNode.SelectSingleNode(".//TaskType").InnerText; }
			set { this.taskNode.SelectSingleNode(".//TaskType").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets Task Schedule.
		/// </summary>
		public TaskSchedule Schedule
		{
			get { return this.schedule; }
			set { this.schedule = value; }
		}

		/// <summary>
		/// Gets the Task Node.
		/// </summary>
		public XmlNode TaskNode
		{
			get { return this.taskNode; }
		}
	}
}
