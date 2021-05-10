using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class GetServicesHandler : Node.Core.Biz.Handler.WebMethods.GetServicesHandler
    {
        //***********************************************************************
        // Public Members
        //***********************************************************************
        #region Public Members

        #endregion

        //***********************************************************************
        // Private Members
        //***********************************************************************
        #region Private Members
        private GetServices getServices;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public GetServicesHandler(string requestorIP, string hostName, GetServices getServices) :
            base(requestorIP, hostName, getServices.securityToken, getServices.serviceCategory)
        {
            this.getServices = getServices;
        }
        #endregion

        //***********************************************************************
        // Delegate Events
        //***********************************************************************
        #region Delegate Events

        #endregion

        //***********************************************************************
        // Public Properties
        //***********************************************************************
        #region Public Properties

        #endregion

        //***********************************************************************
        // Protected Properties
        //***********************************************************************
        #region Protected Properties

        #endregion

        //***********************************************************************
        // Public Methods
        //***********************************************************************
        #region Public Methods

        #endregion

        //***********************************************************************
        // Protected Methods
        //***********************************************************************
        #region Protected Methods
        protected override object ExecuteDataflow(string dataflowConfig)
        {
            IActionProcess process = GetActionProcess();
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.TransID);
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.getServices.securityToken);
            process.CreateActionParameter(WebServiceParameter.serviceCategory.ToString(), this.getServices.serviceCategory);

            return process.Execute(dataflowConfig);
        }

        protected override object ExecuteProcess(string dllName, string className, Node.Core.Biz.Manageable.Parameters.ProcParam param)
        {
            //ServiceRegistration services = new ServiceRegistration();
            //XmlNodeList list = null;
            
            //if (this.getServices.serviceCategory.Equals(Phrase.WEB_SERVICE_ALL, StringComparison.CurrentCultureIgnoreCase))
            //    list = services.GetService();
            //else
            //    list = services.GetService(this.getServices.serviceCategory);

            //XmlNode[] retNodes = new XmlNode[list.Count];
            //int i = 0;
            //foreach (XmlNode node in list)
            //    retNodes[i++] = node;

            //return retNodes;
            //ServiceRegistration services = new ServiceRegistration();
            //return services.GetServiceXML();

            ENDSServiceRegistration ends = new ENDSServiceRegistration();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(ends.BuildENDS());
            return xdoc;
        }

        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods

        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion

				
    }
}
