using System;
using System.Reflection;
using System.IO;
using System.Web.Hosting;

namespace Node.Core.Biz.Manageable
{
    /// <summary>
    /// DllManager is using reflection technique to load plug-in class. 
    /// </summary>
    public class DllManager
    {
        /// <summary>
        /// Constructor of DllManager.
        /// </summary>
        public DllManager()
        {
        }
        /// <summary>
        /// Initiate Authenticate PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Authenticate.IPreProcess GetAuthPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Authenticate.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Authenticate Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Authenticate.IProcess GetAuthProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Authenticate.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Authenticate PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Authenticate.IPostProcess GetAuthPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Authenticate.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Download PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Download.IPreProcess GetDownloadPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Download.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Download Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Download.IProcess GetDownloadProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Download.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Download PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Download.IPostProcess GetDownloadPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Download.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetServices PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetServices.IPreProcess GetGetServicesPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetServices.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetServices Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetServices.IProcess GetGetServicesProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetServices.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetServices PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetServices.IPostProcess GetGetServicesPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetServices.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetStatus PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetStatus.IPreProcess GetGetStatusPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetStatus.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetStatus Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetStatus.IProcess GetGetStatusProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetStatus.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate GetStatus PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.GetStatus.IPostProcess GetGetStatusPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.GetStatus.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate NodePing PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.NodePing.IPreProcess GetNodePingPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.NodePing.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate NodePing Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.NodePing.IProcess GetNodePingProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.NodePing.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate NodePing PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.NodePing.IPostProcess GetNodePingPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.NodePing.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Notify PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Notify.IPreProcess GetNotifyPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Notify.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Notify Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Notify.IProcess GetNotifyProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Notify.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Notify PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Notify.IPostProcess GetNotifyPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Notify.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Query PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Query.IPreProcess GetQueryPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Query.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Query Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Query.IProcess GetQueryProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Query.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Query PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Query.IPostProcess GetQueryPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Query.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Solicit PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Solicit.IPreProcess GetSolicitPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Solicit.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Solicit Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Solicit.IProcess GetSolicitProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Solicit.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Solicit PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Solicit.IPostProcess GetSolicitPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Solicit.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Submit PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Submit.IPreProcess GetSubmitPreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Submit.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Submit Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Submit.IProcess GetSubmitProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Submit.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Submit PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Submit.IPostProcess GetSubmitPostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Submit.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Execute PreProcess Plug-In. 
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Execute.IPreProcess GetExecutePreProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Execute.IPreProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Execute Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Execute.IProcess GetExecuteProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Execute.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Execute PostProcess Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Execute.IPostProcess GetExecutePostProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Execute.IPostProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate Schedule Task Process Plug-In.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>PlugIn Instance.</returns>
        public Node.Core.Biz.Interfaces.Task.IProcess GetTaskProcess(string dllName, string className)
        {
            return (Node.Core.Biz.Interfaces.Task.IProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataFlow.Component.Interface.IActionProcess GetActionProcess()
        {
            string plugin = Properties.Settings.Default.DataFlowPlugIn;

            string[] settings = plugin.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (settings.Length != 2)
                return null;

            string dllName = settings[0].Split("=".ToCharArray())[1];
            string className = settings[1].Split("=".ToCharArray())[1];

            return (DataFlow.Component.Interface.IActionProcess)this.GetClass(dllName, className);
        }
        /// <summary>
        /// Initiate the class with location of dll and name of class.
        /// </summary>
        /// <param name="dllName">Name of Plug-In dll.</param>
        /// <param name="className">Name of class.</param>
        /// <returns>class instance.</returns>
        public object GetClass(string dllName, string className)
        {
            object retObj = null;
            if (dllName == null || className == null)
                return null;
            Type t = Type.GetType(className);
            if (t == null)
            {
                Assembly assem = null;
                try
                {
                    FileInfo fileInfo = new FileInfo(dllName);
                    if (!fileInfo.Exists)
                        fileInfo = new FileInfo(HostingEnvironment.ApplicationPhysicalPath + dllName);
                    if (!fileInfo.Exists)
                        fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + dllName);
                    if (!fileInfo.Exists)
                        throw new Exception("Can't find the file '" + dllName + "'!");

                    dllName = fileInfo.FullName;
                    assem = Assembly.LoadFrom(dllName);
                }
                catch (Exception)
                {
                    assem = Assembly.LoadFrom(dllName);
                }
                retObj = assem.CreateInstance(className);
            }
            else
                retObj = t.Assembly.CreateInstance(className);

            return retObj;
        }
    }
}
