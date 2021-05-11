using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.Services.Protocols;

using Node.Core;
using Node.Core.Document;
using Node.Core2.Requestor;

using DataFlow.Component.Interface;

namespace Node.Core2.Biz.Handler.WebMethods
{
    public class DownloadHandler : Node.Core.Biz.Handler.WebMethods.DownloadHandler
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
        private Download download;
        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
        public DownloadHandler(string requestorIP, string hostName, Download download, Node.Core.Document.NodeDocument[] docs) :
            base(requestorIP, hostName, download.securityToken, download.transactionId, download.dataflow, docs)
        {
            this.download = download;
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
            object obj = base.ExecuteDataflow(dataflowConfig);
            return PostDownload(obj);
        }

        protected override object Execute()
        {
            object obj = base.Execute();
            return PostDownload(obj);
        }
        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods
        private object PostDownload(object obj)
        {
            if (!Properties.Settings.Default.AutoPostHandling)
                return obj;

            NodeDocument[] docs = null;
            ArrayList arr = new ArrayList();
            if (this.download.documents != null)
            {
                foreach (NodeDocumentType type in this.download.documents)
                {
                    if (type.documentName != null)
                    {
                        if (type.documentName.Equals(Phrase.DN_NODE20_REPORT, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //throw new SoapException(Phrase.E_FILE_NOT_FOUND, SoapException.ServerFaultCode);
                        }
                        else if (type.documentName.Equals(Phrase.DN_NODE20_ERROR, StringComparison.CurrentCultureIgnoreCase))
                        {
                            string content = "";
                            ArrayList errs = base.GetOperationStatus(this.download.transactionId, Phrase.STATUS_FAILED);

                            foreach (string[] ss in errs)
                            {
                                content += ss[0] + ": " + ss[1] + " (" + ss[3] + ")" + Environment.NewLine;
                            }
                            NodeDocument doc = new NodeDocument();
                            doc.name = type.documentName;
                            doc.type = DocumentFormatType.FLAT.ToString();
                            doc.content = new UTF8Encoding().GetBytes(content);
                            arr.Add(doc);
                        }
                        else if (type.documentName.Equals(Phrase.DN_NODE20_ORIGINAL, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //already handled 
                        }
                        else if (type.documentName.Equals(Phrase.DN_NODE20_PROCESSED, StringComparison.CurrentCultureIgnoreCase))
                        {
                            string content = "";
                            ArrayList errs = base.GetOperationStatus(this.download.transactionId, null);

                            foreach (string[] ss in errs)
                            {
                                content += ss[0] + ": " + ss[1] + " (" + ss[3] + ")" + Environment.NewLine;
                            }
                            NodeDocument doc = new NodeDocument();
                            doc.name = type.documentName;
                            doc.type = DocumentFormatType.FLAT.ToString();
                            doc.content = new UTF8Encoding().GetBytes(content);
                            arr.Add(doc);
                        }
                    }
                }
            }
            //merge the NodeDocuments.
            NodeDocument[] nds = (NodeDocument[])obj;
            docs = new NodeDocument[((nds == null) ? 0 : nds.Length) + arr.Count];

            int i = 0;
            foreach (NodeDocument doc in nds)
                docs[i] = nds[i++];
            foreach (NodeDocument doc in arr)
                docs[i] = nds[i++];

            return docs;
        }
        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion

				
    }
}
