using System;
using System.Collections;
using System.Data;
using System.Xml;

using Node.Core.Biz.Interfaces.Task;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Handler;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core;
using Node.Core.Logging;
using Node.Core.Util;
using NodeLib = Node.Lib.AppSystem;

using DataFlow.Component.Interface;

namespace Node.Core.Biz.Handler
{
    /// <summary>
    /// The base class of schedule task handler.
    /// </summary>
    public class TaskHandler
    {
        #region Public Constructors
        /// <summary>
        /// Constructor of TaskHandler.
        /// </summary>
        /// <param name="taskName">The name of task.</param>
        public TaskHandler(string taskName)
        {
            this.TaskOp = new Operation(taskName);
            this.AppLog = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// The entry point of TaskHandler.
        /// </summary>
        public void Execute()
        {
            bool triedToRun = false;
            try
            {
                // Initialize
                this.Initialize();

                if (this.CanRun())
                {
                    triedToRun = true;

                    // Execute Plug In
                    this.ExecutePlugIn();

                    // Log to Stopped
                    if (bLogging)
                    {
                        ILogging logDB = new DBManager().GetLoggingDB();
                        logDB.UpdateOperationLog(this.OpLogID, Phrase.STATUS_STOPPED, "Task " + this.TaskOp.Name + " has completed successfully", null, true);
                    }
                }
                else
                {
                    this.AppLog.Log("Unable To Run", "Task " + this.TaskOp.Name + " was still running", Logger.LEVEL_WARN);
                }
            }
            catch (Exception e)
            {
                if (this.OpLogID >= 0)
                {
                    if (bLogging)
                    {
                        ILogging logDB = new DBManager().GetLoggingDB();
                        logDB.UpdateOperationLog(this.OpLogID, Phrase.STATUS_FAILED, e.ToString(), null, true);
                    }
                    this.AppLog.Log(e);
                }
                else
                {
                    this.AppLog.Log(e);
                }

                CheckRunOnceTask(this.TaskOp.Name);

            }
            finally
            {
                if (triedToRun && this.OpLogID >= 0)
                {
                    if (this.TaskOp.EmailReceivers.Count > 0)
                    {
                        string toList = "";
                        for (int i = 0; i < this.TaskOp.EmailReceivers.Count; i++)
                        {
                            if (i != 0) toList += ";";
                            toList += "" + this.TaskOp.EmailReceivers[i];
                        }
                        EmailManager emailMgr = new EmailManager();
                        string emailResult = emailMgr.SendTaskEmail(toList, this.OpLogID);
                        if (emailResult != null && emailResult.Trim() != "")
                        {
                            if (bLogging)
                            {
                                ILogging logDB = new DBManager().GetLoggingDB();
                                logDB.UpdateOperationLog(this.OpLogID, Phrase.STATUS_FAILED, "Email Failed to be Sent: " + emailResult, null, true);
                            }
                            this.AppLog.Log(Phrase.STATUS_FAILED, "Email Failed to be Sent: " + emailResult, Logger.LEVEL_ERROR);
                        }
                    }
                }
            }
        }

        #endregion

        #region Private Fields

        private string[] InputParameterValues = null;
        private Operation TaskOp = null;
        private string TransID = null;
        private int OpLogID = -1;
        Logger AppLog = null;
        private bool bLogging = true;

        #endregion

        #region Private Methods

        private bool CanRun()
        {
            bool canRun = true;
            ILogging logDB = new DBManager().GetLoggingDB();
            DataTable dt = logDB.GetLatestTaskStatus(this.TaskOp.Name);
            if (dt != null && dt.Rows.Count > 0)
            {
                object obj = dt.Rows[0]["STATUS_CD"];
                if (obj != null && !obj.Equals(DBNull.Value))
                {
                    string status = "" + obj;
                    if (status.Equals(Phrase.STATUS_RUNNING))
                    {
                        obj = dt.Rows[0]["START_DTTM"];
                        if (obj != null && !obj.Equals(DBNull.Value))
                        {
                            DateTime startDT = (DateTime)obj;
                            if (startDT.CompareTo(DateTime.Now.AddHours(-1.0)) > 0)
                                canRun = false;
                        }
                    }
                }
            }
            return canRun;
        }

        private void Initialize()
        {
            // Refresh from Database
            if (this.TaskOp == null || this.TaskOp.ID < 0)
                throw new Exception("Could Not Initialize Task " + this.TaskOp.Name);

            if (this.TaskOp.Parameters != null && this.TaskOp.Parameters.Count >0)
            {
                foreach (OpParameter para in this.TaskOp.Parameters)
                {
                    if (para.Name.ToUpper() == "LOG" && para.Value.ToUpper() == "FALSE")
                    {
                        this.bLogging = false;
                        break;
                    } 
                }
            }
        }

        private string[] GetParameterNames(XmlNode parameterRoot)
        {
            string[] retNames = null;
            if (parameterRoot != null)
            {
                XmlNodeList parameters = parameterRoot.SelectNodes("Parameter");
                if (parameters.Count > 0)
                {
                    retNames = new string[parameters.Count];
                    for (int i = 0; i < parameters.Count; i++)
                        retNames[i] = parameters.Item(i).SelectSingleNode("Name").InnerText;
                }
            }
            return retNames;
        }

        private string[] GetParameterValues(XmlNode parameterRoot)
        {
            string[] retValues = null;
            if (parameterRoot != null)
            {
                XmlNodeList parameters = parameterRoot.SelectNodes("Parameter");
                if (parameters.Count > 0)
                {
                    retValues = new string[parameters.Count];
                    for (int i = 0; i < parameters.Count; i++)
                        retValues[i] = parameters.Item(i).SelectSingleNode("Value").InnerText;
                }
            }
            return retValues;
        }

        private void ExecutePlugIn()
        {
            // Get Parameter Names from Operation Config File
            this.TransID = new NodeUtility().GenerateTransactionID();
            ILogging logDB = new DBManager().GetLoggingDB();
            if (this.TaskOp.Config.DocumentElement.Name == "process")
            {
                if (this.bLogging)
                {
                    this.OpLogID = logDB.CreateOperationLog(this.TaskOp.ID, this.TransID, null, Phrase.STATUS_RUNNING,
                        "Task " + this.TaskOp.Name + " has begun execution.", null, null, null, null, null, null,
                        null, null, null);
                }

                IActionProcess process = new DllManager().GetActionProcess();

                SystemConfiguration sys = new SystemConfiguration();
                if (this.TaskOp.Version == BaseHandler.NodeVer.VER_20.ToString())
                    process.NodeURI = sys.GetNodeAddress_V2();
                else
                    process.NodeURI = sys.GetNodeAddress();

                IActionOperationLog opLog = process.ActionOperationLog;
                opLog.OperationLogID = this.OpLogID + "";
                opLog.TransactionID = this.TransID;

                process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
                process.Execute(this.TaskOp.Config.OuterXml);

            }
            else
            {
                string[] paramNames = this.GetParameterNames(this.TaskOp.Config.SelectSingleNode("/Operation/Parameters"));
                this.InputParameterValues = this.GetParameterValues(this.TaskOp.Config.SelectSingleNode("/Operation/Parameters"));

                // Log Initial Task
                if (this.bLogging)
                {
                    this.OpLogID = logDB.CreateOperationLog(this.TaskOp.ID, this.TransID, null, Phrase.STATUS_RUNNING,
                        "Task " + this.TaskOp.Name + " has begun execution.", null, null, null, null, null, null,
                        null, paramNames, this.InputParameterValues);
                }
                string dllName = this.TaskOp.Process.DllPath;
                string className = this.TaskOp.Process.ClassName;
                if (dllName != null && !dllName.Trim().Equals("") && className != null && !className.Trim().Equals(""))
                {
                    IProcess process = new DllManager().GetTaskProcess(dllName, className);
                    if (process != null)
                    {
                        ProcParam param = new ProcParam(this.TransID, null, null, this.OpLogID, new Hashtable(),this.TaskOp.ID);
                        process.Execute(this.InputParameterValues, param);
                    }
                    else
                        throw new Exception("Could Not Location Class " + className + " in DLL " + dllName);
                }
                else
                    throw new Exception("Unable to Locate Task Dll and/or Class");
            }

            CheckRunOnceTask(this.TaskOp.Name);
        }

        private void CheckRunOnceTask(string taskName)
        {
            try
            {
                // Check if Run Once Task, Stop if it is.
                NodeLib.TaskManager manager = new NodeLib.TaskManager("task.config");
                NodeLib.Task task = manager.GetTask(this.TaskOp.Name);
                string[] split = task.Parameter.Split(new char[] { ' ' });
                if (split != null && split.Length > 1 && split[split.Length - 1] != null && split[split.Length - 1].Equals("ONCE"))
                {
                    task.Status = "I";
                    task.Schedule.Status = "I";
                    manager.SaveTasks();
                }
            }
            catch (Exception) { }
        }


        #endregion
    }
}
