using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

using Node.Core;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Objects;
using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class SolicitHandler : Node.Core.Biz.Handler.WebMethods.SolicitHandler
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
        private PreParam preParam;
        private PostParam postParam;
        private Solicit solicit;
        private SystemConfiguration sysConfig;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public SolicitHandler(string requestorIP, string hostName, Solicit solicit) :
            base(requestorIP, hostName, solicit.securityToken, null, solicit.request, null)
        {
            Hashtable parameterhash = new Hashtable();
            if (solicit.parameters != null)
            {
                this.Parameters = new string[solicit.parameters.Count()];
                int i = 0;
                foreach (ParameterType type in solicit.parameters)
                {
                    parameterhash.Add(type.parameterName, type.Value);
                    Parameters[i] = type.Value;
                    i++;
                }
            }
            base.ParameterHash = parameterhash;
            this.solicit = solicit;
            this.sysConfig = new SystemConfiguration();
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
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.solicit.securityToken);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.solicit.dataflow);
            process.CreateActionParameter(WebServiceParameter.request.ToString(), this.solicit.request);
            process.CreateActionParameter(WebServiceParameter.recipient.ToString(), this.solicit.recipient);
            process.CreateActionParameter(WebServiceParameter.notificationURI.ToString(), this.solicit.notificationURI);
            if (this.solicit.parameters != null)
            {
                foreach (ParameterType type in this.solicit.parameters)
                    process.CreateActionParameter(type.parameterName, type.Value);
            }
            process.Execute(dataflowConfig);

            Thread thread = new Thread(new ThreadStart(this.DoPostSolicit));
            thread.Start();

            return base.TransID;
        }

        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_SOLICIT_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_SOLICIT_OBJECT, this.solicit);

            base.ExecutePreProcess(dllName, className, param);
            this.preParam = param;
        }

        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            if (param == null)
                return;

            this.postParam = param;
            Thread thread = new Thread(new ThreadStart(this.DoPostSolicit));
            thread.Start();
        }
        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods
        private void DoPostSolicit()
        {
            if (!Properties.Settings.Default.AutoPostHandling)
                return;

            if (this.solicit == null)
                return;

            if (this.preParam == null)
            {
                this.preParam = new PreParam(base.TransID, base.RequestorIP, base.UserName, base.OpLogID);
            }
            if (!this.preParam.ValueTable.Contains(Phrase.BM_TRANSACTION_ID))
                this.preParam.ValueTable.Add(Phrase.BM_TRANSACTION_ID, base.TransID);
            if (!this.preParam.ValueTable.Contains(Phrase.BM_RECEIVING_NODE_ADDRESS))
                this.preParam.ValueTable.Add(Phrase.BM_RECEIVING_NODE_ADDRESS, this.sysConfig.GetNodeAddress_V2());
            if (!this.preParam.ValueTable.Contains(Phrase.BM_ORIGINATING_NODE_ADDRESS))
                this.preParam.ValueTable.Add(Phrase.BM_ORIGINATING_NODE_ADDRESS, base.RequestorIP);

            EmailManager emailMgr = new EmailManager();
            Node.Lib.AppSystem.EmailTemplate template = emailMgr.GetSolicitRecipient();
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            if (template != null)
            {
                foreach (string key in this.preParam.ValueTable.Keys)
                {
                    if (!template.BookMarks.Contains(key))
                        template.BookMarks.Add(key, this.preParam.ValueTable[key]);
                }

                
                //handle recipients
                if (this.solicit.recipient != null)
                {
                    foreach (string recipient in this.solicit.recipient)
                    {
                        if (IsEmailAddress(recipient))
                        {
                            template.ToList = recipient;
                            string mesg = emailMgr.SendEmail(template);
                            logger.UpdateOperationLog(this.OpLogID, (mesg.Trim() == String.Empty) ? Phrase.STATUS_DONE : Phrase.STATUS_FAILED, "Email has been sent to " + recipient + ": " + mesg, this.UserName, false);
                        }
                        else if (IsNodeURI(recipient))
                        {
                            NodeRequestor requestor = new NodeRequestor(recipient);
                            Submit submit = new Submit();
                            submit.transactionId = base.TransID;
                            submit.securityToken = this.solicit.securityToken;
                            submit.dataflow = this.solicit.dataflow;
                            submit.recipient = this.solicit.recipient;
                            submit.notificationURI = this.solicit.notificationURI;
                            // for using dataflow wizard, the submit function should be handled by dataflow to define a NodeWebService for submit.
                            if (this.postParam != null)
                            {
                                submit.documents = NodeRequestor.Convert((Node.Core.Document.NodeDocument[])this.postParam.Result);
                                requestor.Submit(submit);
                                logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_DONE, "Submission Package has been submitted recipient URI: " + recipient, this.UserName, false);
                            }
                        }
                    }
                }
            }
            //handle notificationURI
            if (this.solicit.notificationURI != null)
            {
                foreach (NotificationURIType uriType in this.solicit.notificationURI)
                {
                    ArrayList arr = new ArrayList();
                    switch (uriType.notificationType)
                    {
                        case NotificationTypeCode.Error:
                            arr = GetOperationStatus(base.TransID, Phrase.STATUS_FAILED);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                        case NotificationTypeCode.Warning:
                            break;
                        case NotificationTypeCode.Status:
                            arr = GetOperationStatus(base.TransID, Phrase.STATUS_COMPLETED);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                        case NotificationTypeCode.All:
                        default:
                            arr = GetOperationStatus(base.TransID, null);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                    }
                }
            }

            logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_COMPLETED, "Post-Solict Process has been completed", this.UserName,true);
        }

        private void SendNotify(string uri, ArrayList arr)
        {
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            if (IsEmailAddress(uri))
            {
                string status = "";
                string message = "";
                if (arr.Count == 1)
                {
                    string[] ss = (string[])arr[0];
                    status = ss[0];
                    message = ss[1];
                }
                else
                {
                    status = "See Below.";
                    foreach (string[] ss in arr)
                        message += ss[0] + ":" + ss[1] + Environment.NewLine;
                }

                EmailManager emailMgr = new EmailManager();
                Node.Lib.AppSystem.EmailTemplate template = emailMgr.GetSolicitNotification();
                template.BookMarks.Add(Phrase.BM_TRANSACTION_ID, base.TransID);
                template.BookMarks.Add(Phrase.BM_STATUS_CODE, status);
                template.BookMarks.Add(Phrase.BM_STATUS_DETAIL, message);
                template.ToList = uri;
                string mesg = emailMgr.SendEmail(template);

                logger.UpdateOperationLog(this.OpLogID, (mesg.Trim() == String.Empty) ? Phrase.STATUS_DONE : Phrase.STATUS_FAILED, "Email has been sent to " + uri + ": " + mesg, this.UserName, false);
            }
            else if (IsNodeURI(uri))
            {
                NodeRequestor requestor = new NodeRequestor(uri);
                Notify notify = new Notify();
                notify.securityToken = this.solicit.securityToken;
                notify.nodeAddress = this.sysConfig.GetNodeAddress_V2();
                notify.dataflow = this.solicit.dataflow;
                notify.messages = new NotificationMessageType[arr.Count];

                int i = 0;
                foreach (string[] ss in arr)
                {
                    notify.messages[i].messageCategory = NotificationMessageCategoryType.Status;
                    notify.messages[i].messageName = "Solicit Notification";
                    TransactionStatusCode ts = TransactionStatusCode.Unknown;
                    try
                    {
                        ts = (TransactionStatusCode)Enum.Parse(typeof(TransactionStatusCode), ss[0], true);
                    }
                    catch { }
                    notify.messages[i].status = ts;
                    notify.messages[i].statusDetail = ss[1];
                    notify.messages[i].objectId = base.TransID;
                }
                requestor.Notify(notify);
                logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_DONE, "Notification message has been sent to recipient URI: " + uri, this.UserName, false);
            }
        }
        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion

				
    }
}
