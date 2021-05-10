using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Node.Core;
using Node.Core2.Requestor;
using Node.Core.Biz.Manageable.Parameters;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class NotifyHandler : Node.Core.Biz.Handler.WebMethods.NotifyHandler
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
        private Notify notify;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public NotifyHandler(string requestorIP, string hostName, Notify notify) :
            base(requestorIP, hostName, notify.securityToken, notify.nodeAddress, notify.dataflow, null)
        {
            //save notify parameter value to database.
            NotificationMessageType[] types = notify.messages;
            string[] names = new string[types.Length * 6];
            object[] values = new string[types.Length * 6];

            int i = 0;
            foreach (NotificationMessageType type in types)
            {
                names[i] = Phrase.NP_DATA_FLOW;
                values[i++] = notify.dataflow;
                names[i] = Phrase.NP_MESSAGE_CATEGORY;
                values[i++] = type.messageCategory.ToString();
                names[i] = Phrase.NP_MESSAGE_NAME;
                values[i++] = type.messageName;
                names[i] = Phrase.NP_MESSAGE_STATUS;
                values[i++] = type.status.ToString();
                names[i] = Phrase.NP_MESSAGE_DETAIL;
                values[i++] = type.statusDetail;
                names[i] = Phrase.NP_OBJECT_ID;
                values[i++] = type.objectId;
            }
            base.ExtraName = names;
            base.ExtraValue = values;
            this.notify = notify;
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
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.notify.securityToken);
            process.CreateActionParameter(WebServiceParameter.nodeAddress.ToString(), this.notify.nodeAddress);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.notify.dataflow);
            process.CreateActionParameter(WebServiceParameter.messages.ToString(), this.notify.messages);

            return process.Execute(dataflowConfig);
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
