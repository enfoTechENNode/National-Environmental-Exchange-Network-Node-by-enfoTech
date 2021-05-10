using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

using Node.Core.Biz.Interfaces.Submit;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Biz.Objects;
using Node.Core2.Requestor;
using Node.Core;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class SubmitHandler : Node.Core.Biz.Handler.WebMethods.SubmitHandler
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
        private Submit submit;
        private SystemConfiguration sysConfig;
        private ProcParam procParam;
        private Node.Core.Document.NodeDocument[] documents;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public SubmitHandler(string requestorIP, string hostName, Submit submit, Node.Core.Document.NodeDocument[] docs) :
            base(requestorIP, hostName, submit.securityToken, submit.transactionId, submit.dataflow,submit.flowOperation, docs)
        {
            this.submit = submit;
            this.sysConfig = new SystemConfiguration();
            this.documents = docs;
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
            process.CreateActionParameter(WebServiceParameter.securityToken.ToString(), this.submit.securityToken);
            process.CreateActionParameter(WebServiceParameter.transactionId.ToString(), this.submit.transactionId);
            process.CreateActionParameter(WebServiceParameter.dataflow.ToString(), this.submit.dataflow);
            process.CreateActionParameter(WebServiceParameter.flowOperation.ToString(), this.submit.flowOperation);
            process.CreateActionParameter(WebServiceParameter.documents.ToString(), this.documents);
            process.CreateActionParameter(WebServiceParameter.recipient.ToString(), this.submit.recipient);
            process.CreateActionParameter(WebServiceParameter.notificationURI.ToString(), this.submit.notificationURI);

            process.Execute(dataflowConfig);

            Thread thread = new Thread(new ThreadStart(this.DoPostSubmission));
            thread.Start();

            return base.TransID;

        }

        protected override object Execute()
        {
            object obj = base.Execute();

            Thread thread = new Thread(new ThreadStart(this.DoPostSubmission));
            thread.Start();

            return obj;

        }
        protected override void ExecutePreProcess(string dllName, string className, PreParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_FLOW_OPERATION))
                param.ValueTable.Add(Phrase.OP_PAR_FLOW_OPERATION, submit.flowOperation);

            base.ExecutePreProcess(dllName, className, param);
        }

        protected override object ExecuteProcess(string dllName, string className, ProcParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_FLOW_OPERATION))
                param.ValueTable.Add(Phrase.OP_PAR_FLOW_OPERATION, submit.flowOperation);

            if (!param.ValueTable.Contains(Phrase.OP_PAR_SUBMIT_OBJECT))
                param.ValueTable.Add(Phrase.OP_PAR_SUBMIT_OBJECT, this.submit);

            object obj = base.ExecuteProcess(dllName, className, param);
            this.procParam = param;

            return obj;
        }

        protected override void ExecutePostProcess(string dllName, string className, PostParam param)
        {
            if (!param.ValueTable.Contains(Phrase.OP_PAR_FLOW_OPERATION))
                param.ValueTable.Add(Phrase.OP_PAR_FLOW_OPERATION, submit.flowOperation);

            base.ExecutePostProcess(dllName, className, param);
        }
        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods
        private void DoPostSubmission()
        {
            if (!Properties.Settings.Default.AutoPostHandling)
                return;

            if (this.submit == null)
                return;

            //get transaction id.
            this.submit.transactionId = base.TransID;

            if (this.procParam == null)
            {
                this.procParam = new ProcParam(base.TransID, base.RequestorIP, base.UserName, base.OpLogID, new Hashtable(),SubmitOp.ID);
            }
            if (!this.procParam.ValueTable.Contains(Phrase.BM_TRANSACTION_ID))
                this.procParam.ValueTable.Add(Phrase.BM_TRANSACTION_ID, this.submit.transactionId);
            if (!this.procParam.ValueTable.Contains(Phrase.BM_RECEIVING_NODE_ADDRESS))
                this.procParam.ValueTable.Add(Phrase.BM_RECEIVING_NODE_ADDRESS, this.sysConfig.GetNodeAddress_V2());
            if (!this.procParam.ValueTable.Contains(Phrase.BM_ORIGINATING_NODE_ADDRESS))
                this.procParam.ValueTable.Add(Phrase.BM_ORIGINATING_NODE_ADDRESS, base.RequestorIP);

           
            EmailManager emailMgr = new EmailManager();
            Node.Lib.AppSystem.EmailTemplate template = emailMgr.GetSubmitRecipient();
            if (template != null)
            {
                foreach (string key in this.procParam.ValueTable.Keys)
                {
                    if (!template.BookMarks.Contains(key))
                        template.BookMarks.Add(key, this.procParam.ValueTable[key]);
                }
            }
            Node.Core.API.Logging logger = new Node.Core.API.Logging();
            
            //handle recipients
            if (this.submit.recipient != null && template != null)
            {
                foreach (string recipient in this.submit.recipient)
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

                        Submit recSubmit = new Submit();
                        recSubmit.dataflow = this.submit.dataflow;
                        recSubmit.flowOperation = this.submit.flowOperation;
                        recSubmit.securityToken = this.submit.securityToken;
                        recSubmit.transactionId = this.submit.transactionId;
                        recSubmit.documents = (NodeDocumentType[])this.submit.documents.Clone();
                        requestor.Submit(recSubmit);
                        //requestor.Submit(this.submit);
                        logger.UpdateOperationLog(this.OpLogID, Phrase.STATUS_DONE, "Submission Package has been submitted recipient URI: " + recipient, this.UserName, false);
                    }
                }
            }
            //handle notificationURI
            if (this.submit.notificationURI != null)
            {
                foreach (NotificationURIType uriType in this.submit.notificationURI)
                {
                    ArrayList arr = new ArrayList();
                    switch (uriType.notificationType)
                    {
                        case NotificationTypeCode.Error:
                            arr = GetOperationStatus(this.submit.transactionId, Phrase.STATUS_FAILED);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                        //case NotificationTypeCode.Warning:
                        //    break;
                        case NotificationTypeCode.Status:
                            arr = GetOperationStatus(this.submit.transactionId, Phrase.STATUS_COMPLETED);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                        case NotificationTypeCode.All:
                        default:
                            arr = GetOperationStatus(this.submit.transactionId, null);
                            if (arr.Count > 0)
                                SendNotify(uriType.Value, arr);
                            break;
                    }
                }
            }
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
                Node.Lib.AppSystem.EmailTemplate template = emailMgr.GetSubmitNotification();
                template.BookMarks.Add(Phrase.BM_TRANSACTION_ID, this.submit.transactionId);
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
                notify.securityToken = this.submit.securityToken;
                notify.nodeAddress = this.sysConfig.GetNodeAddress_V2();
                notify.dataflow = this.submit.dataflow;
                notify.messages = new NotificationMessageType[arr.Count];

                int i = 0;
                foreach (string[] ss in arr)
                {
                    notify.messages[i] = new NotificationMessageType();
                    notify.messages[i].messageCategory = NotificationMessageCategoryType.Status;
                    notify.messages[i].messageName = "Submit Notification";
                    TransactionStatusCode ts = TransactionStatusCode.Unknown;
                    try
                    {
                        ts = (TransactionStatusCode)Enum.Parse(typeof(TransactionStatusCode), ss[0], true);
                    }
                    catch { }
                    notify.messages[i].status = ts;
                    notify.messages[i].statusDetail = ss[1];
                    notify.messages[i].objectId = this.submit.transactionId;
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
