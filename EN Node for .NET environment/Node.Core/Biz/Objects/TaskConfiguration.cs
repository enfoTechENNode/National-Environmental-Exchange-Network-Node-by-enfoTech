using System;
using System.Xml;

using Node.Core.Data;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// TaskConfiguration retrieve task configuration information.
    /// </summary>
    public class TaskConfiguration
    {
        #region Public Constructors
        /// <summary>
        /// Constructor of TaskConfiguration.
        /// </summary>
        public TaskConfiguration()
        {
            this.TaskConfig = new DBManager().GetConfigurationsDB().GetTaskConfig();
        }
        /// <summary>
        /// Constructor of TaskConfiguration.
        /// </summary>
        /// <param name="config">XML document contains task configuration information</param>
        public TaskConfiguration(XmlDocument config)
        {
            this.TaskConfig = config;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Add Task.
        /// </summary>
        /// <param name="taskID">The task identifier</param>
        /// <param name="active">Status for the task</param>
        /// <param name="taskName">Name for the task</param>
        /// <param name="parameters">Parameters for the Task</param>
        /// <param name="schedule">Schedule for the task</param>
        /// <returns></returns>
        public XmlDocument AddTask(int taskID, bool active, string taskName, string[] parameters, TaskSchedule schedule)
        {
            int id = taskID;
            if (taskID >= 0)
            {
                XmlNode existingTaskNode = this.TaskConfig.SelectSingleNode("/Tasks/Task[TaskID/text() = '" + taskID + "']");
                if (existingTaskNode != null)
                {
                    XmlElement root = this.TaskConfig.DocumentElement;
                    root.RemoveChild(existingTaskNode);
                }
            }
            else
            {
                int min = -1;
                XmlNodeList tasks = this.TaskConfig.SelectNodes("/Tasks/Task");
                foreach (XmlNode task in tasks)
                {
                    XmlNode taskIDNode = task.SelectSingleNode("TaskID");
                    int temp = int.Parse(taskIDNode.InnerText);
                    if (temp < min)
                        min = temp;
                }
                id = min - 1;
            }

            XmlElement taskElement = this.TaskConfig.CreateElement("Task");
            taskElement.Attributes.Append(this.TaskConfig.CreateAttribute("status"));
            if (active)
                taskElement.Attributes["status"].Value = "A";
            else
                taskElement.Attributes["status"].Value = "I";

            XmlElement taskIDElement = this.TaskConfig.CreateElement("TaskID");
            taskIDElement.InnerText = "" + id;
            taskElement.AppendChild(taskIDElement);

            XmlElement taskNameElement = this.TaskConfig.CreateElement("TaskName");
            taskNameElement.InnerText = taskName;
            taskElement.AppendChild(taskNameElement);

            XmlElement taskModeElement = this.TaskConfig.CreateElement("TaskMode");
            taskModeElement.InnerText = "exe";
            taskElement.AppendChild(taskModeElement);

            XmlElement taskFullPathElement = this.TaskConfig.CreateElement("TaskFullPath");
            taskFullPathElement.InnerText = System.Configuration.ConfigurationManager.AppSettings["Node.TaskHandler.Path"];
            taskElement.AppendChild(taskFullPathElement);

            XmlElement taskParameterElement = this.TaskConfig.CreateElement("TaskParameter");
            if (parameters != null && parameters.Length > 0)
            {
                string parameterList = "";
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i != 0) parameterList += " ";
                    parameterList += parameters[i];
                }
                if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_ONCE)
                {
                    if (!parameterList.Equals(""))
                        parameterList += " ";
                    parameterList += "ONCE";
                }
                taskParameterElement.InnerText = parameterList;
            }
            taskElement.AppendChild(taskParameterElement);

            taskElement.AppendChild(this.TaskConfig.CreateElement("TaskType"));

            taskElement.AppendChild(schedule.GetScheduleNode(this.TaskConfig));

            this.TaskConfig.DocumentElement.AppendChild(taskElement);

            return this.TaskConfig;
        }
        /// <summary>
        /// Delete Task.
        /// </summary>
        /// <param name="taskID">The Task identifier</param>
        /// <returns>XmlDocument</returns>
        public XmlDocument Delete(string taskID)
        {
            if (!string.IsNullOrEmpty(taskID))
            {
                XmlNode existingTaskNode = this.TaskConfig.SelectSingleNode("/Tasks/Task[TaskID/text() = '" + taskID + "']");
                if (existingTaskNode != null)
                {
                    XmlElement root = this.TaskConfig.DocumentElement;
                    root.RemoveChild(existingTaskNode);
                }
            }

            return this.TaskConfig;
        }
        /// <summary>
        /// Save Task Configuration.
        /// </summary>
        public void Save()
        {
            new DBManager().GetConfigurationsDB().UpdateTaskConfig(this.TaskConfig);
        }

        #endregion

        #region Private Fields

        private XmlDocument TaskConfig = null;

        #endregion
    }
}
