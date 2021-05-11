using System;
using System.Data;
using System.IO;

using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Util;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// NodeDocument Class retrieves document Informatuion.
    /// Database Source: NODE_FILE_CABIN.
    /// </summary>
    public class NodeDocument
    {
        private int iFileID = -1;
        private string sFileName = null;
        private string sFileType = null;
        private Stream msStream = null;
        private string sDataFlow = null;
        private string sSubmitURL = null;
        private string sSubmitToken = null;
        private string sDomain = null;
        private string sTransID = null;
        private string sStatus = null;
        private DateTime dSubmitDate;
        private int iFileSize = -1;
        private DateTime dCreatedDate;
        private string sCreatedBy = null;
        private DateTime dUpdatedDate;
        private string sUpdatedBy = null;
        /// <summary>
        /// Constructor of NodeDocument.
        /// </summary>
        /// <param name="fileID">Identifier from NODE_FILE_CABIN</param>
        public NodeDocument(int fileID)
        {
            this.FileID = fileID;
        }

        #region Public Methods

        /// <summary>
        /// Refresh Document From Database
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        public bool RefreshFromDB()
        {
            bool retVal = false;
            if (this.FileID >= 0)
            {
                IDocuments docDB = new DBManager().GetDocumentsDB();
                DataTable dt = docDB.GetDocument(this.FileID);
                if (dt != null && dt.Rows.Count > 0)
                    retVal = this.Init(dt);
            }
            return retVal;
        }

        /// <summary>
        /// Save the Document to the Database
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator that is Logged In</param>
        /// <returns>true if successful, false otherwise</returns>
        public void SaveDocument(string domainAdmin)
        {
            if (this.sTransID == null || this.sTransID.Trim().Equals(""))
                this.sTransID = new NodeUtility().GenerateTransactionID();
            if (this.sStatus == null || this.sStatus.Trim().Equals(""))
                this.sStatus = "A";
            if (this.dSubmitDate.CompareTo(DateTime.MinValue) == 0)
                this.dSubmitDate = DateTime.Now;
            IDocuments docDB = new DBManager().GetDocumentsDB();
            this.iFileID = docDB.UpdateDocument(this.iFileID, this.sTransID, this.sFileName, this.sFileType, this.sStatus, this.sDataFlow, this.sSubmitURL,
                this.sSubmitToken, this.dSubmitDate, this.msStream, domainAdmin);
            this.RefreshFromDB();
        }

        #endregion

        private bool Init(DataTable dt)
        {
            bool retVal = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                object obj = dt.Rows[0]["FILE_ID"];
                if (obj != null)
                    this.FileID = int.Parse("" + obj);
                else
                    return false;
                this.DataFlow = dt.Rows[0]["DATAFLOW_NAME"] != null ? "" + dt.Rows[0]["DATAFLOW_NAME"] : null;
                this.Domain = dt.Rows[0]["DOMAIN_NAME"] != null ? "" + dt.Rows[0]["DOMAIN_NAME"] : null;
                this.TransactionID = "" + dt.Rows[0]["TRANS_ID"];
                byte[] content = dt.Rows[0]["FILE_CONTENT"] != null ? (byte[])dt.Rows[0]["FILE_CONTENT"] : null;
                if (content != null && content.Length > 0)
                    this.msStream = new MemoryStream(content);
                else
                    this.msStream = new MemoryStream();
                this.iFileSize = (int)this.msStream.Length;
                this.FileName = dt.Rows[0]["FILE_NAME"] != null ? "" + dt.Rows[0]["FILE_NAME"] : null;
                this.FileType = dt.Rows[0]["FILE_TYPE"] != null ? "" + dt.Rows[0]["FILE_TYPE"] : null;
                this.Status = dt.Rows[0]["STATUS_CD"] != null ? "" + dt.Rows[0]["STATUS_CD"] : null;
                this.SubmitURL = dt.Rows[0]["SUBMIT_URL"] != null ? "" + dt.Rows[0]["SUBMIT_URL"] : null;
                this.SubmitToken = dt.Rows[0]["SUBMIT_TOKEN"] != null ? "" + dt.Rows[0]["SUBMIT_TOKEN"] : null;
                obj = dt.Rows[0]["SUBMIT_DTTM"];
                if (obj != null)
                    this.SubmitDate = (DateTime)obj;
                obj = dt.Rows[0]["CREATED_DTTM"];
                if (obj != null)
                    this.CreatedDate = (DateTime)obj;
                this.CreatedBy = dt.Rows[0]["CREATED_BY"] != null ? "" + dt.Rows[0]["CREATED_BY"] : null;
                obj = dt.Rows[0]["UPDATED_DTTM"];
                if (obj != null)
                    this.UpdatedDate = (DateTime)obj;
                this.UpdatedBy = dt.Rows[0]["UPDATED_BY"] != null ? "" + dt.Rows[0]["UPDATED_BY"] : null;
            }
            return retVal;
        }

        #region Public Properties
        /// <summary>
        /// Identifier of NodeDocument.
        /// </summary>
        public int FileID
        {
            get { return this.iFileID; }
            set { this.iFileID = value; }
        }
        /// <summary>
        /// File Name of NodeDocument.
        /// </summary>
        public string FileName
        {
            get { return this.sFileName; }
            set { this.sFileName = value; }
        }
        /// <summary>
        /// File Type of NodeDocument.
        /// </summary>
        public string FileType
        {
            get { return this.sFileType; }
            set { this.sFileType = value; }
        }
        /// <summary>
        /// File Content of NodeDocument.
        /// </summary>
        public Stream FileContent
        {
            get { return this.msStream; }
            set { this.msStream = value; }
        }
        /// <summary>
        /// DataFlow of NodeDocument.
        /// </summary>
        public string DataFlow
        {
            get { return this.sDataFlow; }
            set { this.sDataFlow = value; }
        }
        /// <summary>
        /// Submitted URL of NodeDocument.
        /// </summary>
        public string SubmitURL
        {
            get { return this.sSubmitURL; }
            set { this.sSubmitURL = value; }
        }
        /// <summary>
        /// Submitted Security Token of NodeDocument.
        /// </summary>
        public string SubmitToken
        {
            get { return this.sSubmitToken; }
            set { this.sSubmitToken = value; }
        }
        /// <summary>
        /// Domain of NodeDocument.
        /// </summary>
        public string Domain
        {
            get { return this.sDomain; }
            set { this.sDomain = value; }
        }
        /// <summary>
        /// Transaction ID of NodeDocument.
        /// </summary>
        public string TransactionID
        {
            get { return this.sTransID; }
            set { this.sTransID = value; }
        }
        /// <summary>
        /// Status of NodeDocument.
        /// </summary>
        public string Status
        {
            get { return this.sStatus; }
            set { this.sStatus = value; }
        }
        /// <summary>
        /// Submitted Date of NodeDocument.
        /// </summary>
        public DateTime SubmitDate
        {
            get { return this.dSubmitDate; }
            set { this.dSubmitDate = value; }
        }
        /// <summary>
        /// File Size of NodeDocument.
        /// </summary>
        public int FileSize
        {
            get { return this.iFileSize; }
            set { this.iFileSize = value; }
        }
        /// <summary>
        /// Created Date of NodeDocument.
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.dCreatedDate; }
            set { this.dCreatedDate = value; }
        }
        /// <summary>
        /// Created by of NodeDocument.
        /// </summary>
        public string CreatedBy
        {
            get { return this.sCreatedBy; }
            set { this.sCreatedBy = value; }
        }
        /// <summary>
        /// Updated Date of NodeDocument.
        /// </summary>
        public DateTime UpdatedDate
        {
            get { return this.dUpdatedDate; }
            set { this.dUpdatedDate = value; }
        }
        /// <summary>
        /// Updated by of NodeDocument.
        /// </summary>
        public string UpdatedBy
        {
            get { return this.sUpdatedBy; }
            set { this.sUpdatedBy = value; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Search the Node Document Database for Documents
        /// </summary>
        /// <param name="docName">The Document Name, null or empty string if not searchable</param>
        /// <param name="transID">The Transaction ID of the Document, null or empty string if not searchable</param>
        /// <param name="domID">The Domain of the Document, -1 if not searchable</param>
        /// <param name="opName">The Operation Name of the Document, null or empty string if not searchable</param>
        /// <param name="start">The Starting Range of the Submit Date</param>
        /// <param name="end">The Ending Range of the Submit Date</param>
        /// <param name="domainAdmin">Name of the Logged in Domain Administrator</param>
        /// <returns>
        /// DataTable with Columns: FILE_ID, FILE_NAME, FILE_TYPE, FILE_SIZE, TRANS_ID, DOMAIN_NAME, DATAFLOW_NAME, SUBMIT_DTTM
        /// </returns>
        public static DataTable SearchDocuments(string docName, string transID, int domID, string opName, DateTime start, DateTime end, string domainAdmin)
        {
            return new DBManager().GetDocumentsDB().SearchDocuments(docName, transID, domID, opName, start, end, domainAdmin);
        }

        /// <summary>
        /// Delete Documents with the Document ID's in the input string array parameter
        /// </summary>
        /// <param name="ids">string version of ids to be deleted</param>
        public static void DeleteDocuments(string[] ids)
        {
            new DBManager().GetDocumentsDB().DeleteDocuments(ids);
        }

        #endregion
    }
}
