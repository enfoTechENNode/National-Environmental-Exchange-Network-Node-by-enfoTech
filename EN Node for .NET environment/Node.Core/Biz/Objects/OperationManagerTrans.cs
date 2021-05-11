using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// OperationManagerTrans retrieve transaction information of operation manager function.
    /// DataSource: NODE_OPERATION_MANAGER
    /// </summary>
    public class OperationManagerTrans
    {
        /// <summary>
        /// Identifier of OperationManagerTrans.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Operation Name of OperationManagerTrans.
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Status of OperationManagerTrans.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Submitted Date of OperationManagerTrans.
        /// </summary>
        public DateTime? SubmittedDate { get; set; }
        /// <summary>
        /// Submitted URL of OperationManagerTrans.
        /// </summary>
        public string SubmittedURL { get; set; }
        /// <summary>
        /// Node Version of of OperationManagerTrans.
        /// </summary>
        public string NodeVersion { get; set; }
        /// <summary>
        /// Transaction ID of of OperationManagerTrans.
        /// </summary>
        public string TransID { get; set; }
        /// <summary>
        /// Supplied Tranasaction ID 
        /// </summary>
        public string TransIDSupplied { get; set; }
        /// <summary>
        /// File Content of OperationManagerTrans.
        /// </summary>
        public byte[] FileContent { get; set; }
        /// <summary>
        /// DataFlow Name of of OperationManagerTrans.
        /// </summary>
        public string DataFlow { get; set; }
    }
}
