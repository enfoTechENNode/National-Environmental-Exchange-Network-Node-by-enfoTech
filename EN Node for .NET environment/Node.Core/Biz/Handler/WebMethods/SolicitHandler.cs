using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Web.Services.Protocols;
using System.Xml;
using System.Collections.Generic;

using Node.Core.Biz.Interfaces.Solicit;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

using DataFlow.Component.Interface;

namespace Node.Core.Biz.Handler.WebMethods
{
    /// <summary>
    /// SolicitHandler is core class for Solicit Web Service
    /// </summary>
    public class SolicitHandler : BaseHandler
    {
        private string ReturnURL = null;
        private string Request = null;
        protected string[] Parameters = null;
        private Operation SolicitOp = null;
        /// <summary>
        /// This method is constructor of  SolicitHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="token">The Token which is result of Authentication Operation.</param>
        /// <param name="returnURL">It can be recipient email address or NofificationURI.</param>
        /// <param name="request">The name of Plug-In Solicit Oparation to be processed.</param>
        /// <param name="parameters">Array of Parameter for the solicit request</param>
        public SolicitHandler(string requestorIP, string hostName, string token, string returnURL, string request, string[] parameters) : base (requestorIP, hostName)
        {
            this.Token = token;
            this.ReturnURL = returnURL;
            this.Request = request;
            this.Parameters = parameters;
            this.SolicitOp = new Operation(request, Phrase.WEB_SERVICE_SOLICIT);
            if (parameters != null && parameters.Length > 0 && this.SolicitOp.Parameters.Count > 0)
            {
                if (parameters.Length == this.SolicitOp.Parameters.Count)
                {
                    this.ParameterHash = new Hashtable();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        OpParameter opPara = (OpParameter)this.SolicitOp.Parameters[i];
                        this.ParameterHash.Add(opPara.Name, Parameters[i]);
                    }
                }
            }
            else if (parameters != null && parameters.Length > 0)
            {
                DBManager dbMgr = new DBManager();
                Hashtable ht = dbMgr.GetOperationsDB().GetSolicitNameParameterPairs();
                object obj = ht[this.Request];
                if (obj != null)
                {
                    string[] pars = (obj + "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (pars.Length == this.Parameters.Length)
                    {
                        this.ParameterHash = new Hashtable();
                        for (int i = 0; i < pars.Length; i++)
                            this.ParameterHash.Add(pars[i].Trim(), this.Parameters[i]);
                    }
                }
            }

        }

        //protected string[] ParameterList
        //{
        //    get { return this.Parameters; }
        //    set { this.Parameters = value; }
        //}
        /// <summary>
        /// Returns Solicit Parameter. 
        /// </summary>
        protected Hashtable ParameterHash { get; set; }
        /// <summary>
        /// Initialize process of SolicitHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.SolicitOp != null && this.SolicitOp.ID >= 0)
            {
                if (this.SolicitOp.DomainStatus != null && this.SolicitOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.SolicitOp.Status != null && this.SolicitOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        List<string> names = new List<string>();
                        List<object> values = new List<object>();

                        names.Add("Request");
                        values.Add(this.Request);

                        if (this.ParameterHash != null)
                        {
                            IDictionaryEnumerator idc = this.ParameterHash.GetEnumerator();
                            idc.Reset();
                            while (idc.MoveNext())
                            {
                                names.Add(idc.Entry.Key.ToString());
                                values.Add(idc.Entry.Value);
                            }
                        }


                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.SolicitOp.ID, this.TransID, null,
                            Phrase.STATUS_RECEIVED, Phrase.MESSAGE_RECEIVED, this.RequestorIP, null,
                            this.Token, null, this.ReturnURL, null, this.HostName, names.ToArray(), values.ToArray());
                    }
                    else
                        throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
            {
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
        }
        /// <summary>
        /// Authorize process of SolicitHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            string user = null;
            try
            {
                user = this.Authorize(Phrase.WEB_SERVICE_SOLICIT, this.Request);
                if (user != null)
                {
                    ILogging logDB = new DBManager().GetLoggingDB();
                    logDB.UpdateOperationLogUserName(this.TransID, user);
                }
                else
                    throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);
            }
            catch (SoapException e)
            {
                if (e.Message == "Invalid security token")
                {
                    throw new SoapException(Phrase.E_INVALID_TOKEN, SoapException.ClientFaultCode);
                }
                else
                {
                    throw e;
                } 
            }
            catch (Exception)
            {
                ILogging logDB = new DBManager().GetLoggingDB();
                logDB.CopyUserFromToken(this.Token, this.TransID);
            }
            return user;
        }
        /// <summary>
        /// Excute DataFlow process of SolicitHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.Token);
            process.CreateActionParameter(WebServiceParameter.returnURL.ToString(), this.ReturnURL);
            process.CreateActionParameter(WebServiceParameter.request.ToString(), this.Request);


            IDictionaryEnumerator idc = this.ParameterHash.GetEnumerator();
            while (idc.MoveNext())
            {
                process.CreateActionParameter(idc.Key.ToString(), idc.Value);
            }
            process.Execute(dataflowConfig);
            return base.TransID;
        }
        /// <summary>
        /// Excute Plug-in process of SolicitHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            if (this.SolicitOp.Config.DocumentElement.Name == "process")
            {
                return this.ExecuteDataflow(this.SolicitOp.Config.OuterXml);
            }

            // Pre Processes
            XmlNode preNode = this.SolicitOp.Config.SelectSingleNode("/Operation/PreProcess");
            PreParam preParam = new PreParam(this.TransID, this.RequestorIP, this.UserName, this.OpLogID);
            if (preNode != null)
            {
                Hashtable table = new Hashtable();
                XmlNodeList list = preNode.SelectNodes("Sequence");
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        XmlNode classNode = list.Item(i).SelectSingleNode("ClassName");
                        XmlNode dllNode = list.Item(i).SelectSingleNode("DllName");
                        XmlAttribute attr = list.Item(i).Attributes["number"];
                        table.Add(attr.Value, dllNode.InnerText + " " + classNode.InnerText);
                    }
                }
                string temp = null;
                for (int i = 1; (temp = (string)table["" + i]) != null; i++)
                {
                    string[] split = temp.Split(new char[] { ' ' });
                    this.ExecutePreProcess(split[0], split[1], preParam);
                }
            }

            // Schedule or Start Thread
            XmlNode solicitNode = this.SolicitOp.Config.SelectSingleNode("/Operation/Process/Solicit");
            if (solicitNode != null)
            {
                Thread thread = new Thread(new ThreadStart(this.ExecuteSolicitProcess));
                thread.Start();
            //    XmlNode startTimeNode = solicitNode.SelectSingleNode("SolicitStartTime");
            //    XmlNode endTimeNode = solicitNode.SelectSingleNode("SolicitEndTime");
            //    if (startTimeNode != null && !startTimeNode.InnerText.Trim().Equals("") &&
            //        endTimeNode != null && !endTimeNode.InnerText.Trim().Equals(""))
            //    {
            //        DateTime startTime = DateTime.ParseExact(startTimeNode.InnerText, "HH:mm", CultureInfo.InstalledUICulture);
            //        DateTime endTime = DateTime.ParseExact(endTimeNode.InnerText, "HH:mm", CultureInfo.InstalledUICulture);
            //        if (endTime.CompareTo(DateTime.Now) <= 0)
            //        {
            //            startTime = startTime.AddDays(1.0);
            //            endTime = endTime.AddDays(1.0);
            //        }
            //        TimeSpan span = endTime - startTime;
            //        Random rand = new Random((int)DateTime.Now.Ticks);
            //        double totalSeconds = span.TotalSeconds;
            //        int secondsToAdd = rand.Next((int)totalSeconds);
            //        DateTime execTime = startTime.AddSeconds((double)secondsToAdd);
            //        DateTime execEndTime = execTime.AddMonths(6);

            //        IConfigurations configDB = new DBManager().GetConfigurationsDB();
            //        TaskConfiguration taskConfig = new TaskConfiguration(configDB.GetTaskConfig());
            //        TaskSchedule schedule = new TaskSchedule("A", execTime, execEndTime, TaskSchedule.SCHEDULE_TYPE_DAILY, -1, 1, -1, null, null, -1, null);
            //        int count = 9;
            //        if (this.Parameters != null && this.Parameters.Length > 0)
            //            count += this.Parameters.Length;
            //        string[] pars = new string[count];
            //        pars[0] = "Solicit"; pars[1] = this.SolicitOp.Name; pars[2] = "" + this.OpLogID;
            //        pars[3] = this.TransID; pars[4] = this.RequestorIP; pars[5] = this.UserName;
            //        pars[6] = this.Token;
            //        pars[7] = this.ReturnURL != null && !this.ReturnURL.Trim().Equals("") ? this.ReturnURL : "null"; 
            //        pars[8] = this.Request;
            //        if (this.Parameters != null)
            //            for (int i = 9; i < 9 + this.Parameters.Length; i++)
            //                pars[i] = this.Parameters[i - 9];
            //        configDB.UpdateTaskConfig(taskConfig.AddTask(-1, true, "Solicit " + this.TransID, pars, schedule));
            //    }
            //    else
            //    {
            //        Thread thread = new Thread(new ThreadStart(this.ExecuteSolicitProcess));
            //        thread.Start();
            //    }
            }
            else
                throw new Exception("Solicit Operation Information is Not in Solicit Configuration");
            return this.TransID;
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetSolicitPreProcess(dllName, className);
            if (process != null)
            {              
                process.Execute(this.Token, this.ReturnURL, this.Request, this.Parameters, param);
            }
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            return null;
        }
        /// <summary>
        /// Excute PostProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
        }

        private void ExecuteSolicitProcess()
        {
            SolicitProcess process = new SolicitProcess(this.SolicitOp.Name, this.OpLogID, this.TransID, this.RequestorIP, this.UserName, this.Token, this.ReturnURL, this.Request, this.Parameters);
            object obj = process.Execute();

            PostParam param = new PostParam(base.TransID, base.RequestorIP, base.UserName, base.OpLogID, obj, null);
            this.ExecutePostProcess(null, null, param);
        }
    }
}
