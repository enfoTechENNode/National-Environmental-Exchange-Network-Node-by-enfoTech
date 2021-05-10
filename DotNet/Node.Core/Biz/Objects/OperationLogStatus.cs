using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// OperationLogStatus Class retrieves Operation Log status Informatuion.
    /// Database Source: NODE_OPERATION_lOG_STATUS
    /// </summary>
    public class OperationLogStatus
    {
        #region Public Properties
        /// <summary>
        /// Identifier of OperationLogStatus.
        /// </summary>
        public int OperationLogStatusID { get { return this.opLogStatusID; } }

        /// <summary>
        /// status of operation log.
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        /// <summary>
        /// Message of operation log status. 
        /// </summary>
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
        /// <summary>
        /// The created date of operation log status message.
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }
        /// <summary>
        /// The creator of operation log status.
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }

        #endregion

        #region Public Constructor
        /// <summary>
        /// Constructor of OpLogStatus.
        /// </summary>
        /// <param name="opLogStatusID">opLogStatusID</param>
        public OperationLogStatus(int opLogStatusID)
        {
            this.opLogStatusID = opLogStatusID;
        }

        #endregion

        #region Private Fields

        private int opLogStatusID = -1;
        private string status = null;
        private string message = null;
        private DateTime createdDate = DateTime.MinValue;
        private string createdBy = null;

        #endregion
    }
}
