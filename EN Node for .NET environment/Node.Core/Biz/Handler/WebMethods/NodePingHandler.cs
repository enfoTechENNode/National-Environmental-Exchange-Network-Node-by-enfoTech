using System;

using Node.Core.Biz.Interfaces.NodePing;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

using DataFlow.Component.Interface;

namespace Node.Core.Biz.Handler.WebMethods
{
    /// <summary>
    /// NodePingHandler is core class for NodePing Web Service
    /// </summary>
    public class NodePingHandler : BaseHandler
    {
        private string Hello = null;
        private Operation NodePingOp = null;
        /// <summary>
        /// This method is constructor of  NodePingHandler.
        /// </summary>
        /// <param name="requestorIP">IP address of requestor.</param>
        /// <param name="hostName">The Host Name for Node Operations.</param>
        /// <param name="hello">Input string for arbitrary text.</param>
        public NodePingHandler(string requestorIP, string hostName, string hello) : base (requestorIP, hostName)
        {
            this.Hello = hello;
            string opName = (NodeVersion == NodeVer.VER_11) ? "NODE" : "NODE2";
            this.NodePingOp = new Operation(opName, Phrase.WEB_SERVICE_NODEPING);
        }
        /// <summary>
        /// Initialize process of NodePingHandler.
        /// </summary>
        protected override void Initialize()
        {
            if (this.NodePingOp != null && this.NodePingOp.ID >= 0)
            {
                if (this.NodePingOp.DomainStatus != null && this.NodePingOp.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (this.NodePingOp.Status != null && this.NodePingOp.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                    {
                        string[] names = new string[] { "Hello" };
                        object[] values = new object[] { this.Hello };
                        ILogging logDB = new DBManager().GetLoggingDB();
                        this.OpLogID = logDB.CreateOperationLog(this.NodePingOp.ID, this.TransID, null, Phrase.STATUS_RECEIVED,
                            Phrase.MESSAGE_RECEIVED, this.RequestorIP, null, null, null, null, null,
                            this.HostName, names, values);
                    }
                    else
                        throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
                }
                else
                    throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
            }
            else
                throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }
        /// <summary>
        /// Authorize process of NodePingHandler.
        /// </summary>
        /// <returns></returns>
        protected override string Authorize()
        {
            return "";
        }
        /// <summary>
        /// Excute DataFlow process of NodePingHandler.
        /// </summary>
        /// <param name="dataflowConfig"></param>
        /// <returns></returns>
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            return process.Execute(dataflowConfig);
        }
        /// <summary>
        /// Excute Plug-in process of NodePingHandler.
        /// </summary>
        /// <returns></returns>
        protected override object Execute()
        {
            return this.ExecuteOperation(this.NodePingOp);
        }
        /// <summary>
        /// Excute PreProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            IPreProcess process = new DllManager().GetNodePingPreProcess(dllName, className);
            if (process != null)
                process.Execute(this.Hello, param);
        }
        /// <summary>
        /// Excute Processs Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            IProcess process = new DllManager().GetNodePingProcess(dllName, className);
            if (process != null)
                return process.Execute(this.Hello, param);
            throw new Exception(Phrase.E_SERVICE_UNAVAILABLE);
        }
        /// <summary>
        /// Excute PostProcesss Plug-in dll.
        /// </summary>
        /// <param name="dllName">location of plugin dll.</param>
        /// <param name="className">name of class to be invoked.</param>
        /// <param name="param">parameters.</param>
        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            IPostProcess process = new DllManager().GetNodePingPostProcess(dllName, className);
            if (process != null)
                process.Execute(this.Hello, param);
        }
    }
}
