#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		TaskManager.cs
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
	/// Summary description for TaskManager.
	/// </summary>
	public class TaskManager
	{
		private string filename = null;
		private XmlDocument taskDoc = null;

		/// <summary>
		/// Initializes a TaskManager object by specified file name or key name.
		/// </summary>
		/// <param name="name">The file name or key name for task config.</param>
		public TaskManager(string name)
		{
			this.filename = name;
			this.taskDoc = SystemConfig.GetInstance().GetSystemConfig(this.filename);
		}

		/// <summary>
		/// Initializes a TaskManager object by specified XmlDocument object.
		/// </summary>
		/// <param name="taskDoc">An XmlDocument contains the task config information.</param>
		public TaskManager(XmlDocument taskDoc)
		{
			this.taskDoc = taskDoc;
		}

		/// <summary>
		/// Initializes a TaskManager object by using defualt TaskSetting value.
		/// </summary>
		public TaskManager()
		{
			this.filename = Properties.Settings.Default.TaskSetting;
			this.taskDoc = SystemConfig.GetInstance().GetSystemConfig(this.filename);
		}

		/// <summary>
		/// Get Tasks.
		/// </summary>
		/// <returns>A Task object array.</returns>
		public Task[] GetTasks()
		{
			if (this.taskDoc == null)
				return null;

			XmlNodeList tasks = this.taskDoc.SelectNodes(".//Task");
			Task[] taskList = new Task[tasks.Count];
			int i = 0;
			foreach (XmlNode task in tasks)
				taskList[i++] = new Task(task);

			return taskList;
		}

		/// <summary>
		/// Gets the task by specified task name.
		/// </summary>
		/// <param name="taskName">The specified task name.</param>
		/// <returns>The Task object return. It will be null, if the task name does not exist.</returns>
		public Task GetTask(string taskName)
		{
			if (this.taskDoc == null)
				return null;

			XmlNode task = this.taskDoc.SelectSingleNode(".//Task[TaskName='" + taskName + "']");
			return (task != null)? new Task(task) : null;
		}

		/// <summary>
		/// Saves all tasks.
		/// </summary>
		public void SaveTasks()
		{
			if (this.filename == null)
				throw new ApplicationException("The file name or key name is null. Can not save it!");
			SystemConfig.GetInstance().SetSystemConfig(this.filename, this.taskDoc);
		}

		/// <summary>
		/// Gets the Task Xml document.
		/// </summary>
		public XmlDocument TaskDocument
		{
			get { return this.taskDoc; }
		}
	}
}
