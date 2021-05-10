using System;
using System.Collections;

using Node.Core.Util;

namespace Node.Core.Biz.Manageable.Parameters
{
    /// <summary>
    /// PreParm is stored data from PreProcess.
    /// </summary>
    public class PreParam
    {
        private string sUniqueKey = null;
        private string sTransID = null;
        private string sRequestorIP = null;
        private string sUser = null;
        private int iOpLogID = -1;
        private Hashtable hTable = null;
        /// <summary>
        /// Constructor for PreParam.
        /// </summary>
        /// <param name="transID">Transaction id.</param>
        /// <param name="requestorIP">Requestor IP address.</param>
        /// <param name="user">User invokes the operation.</param>
        /// <param name="opLogID">Identifier for operation log.</param>
        public PreParam(string transID, string requestorIP, string user, int opLogID)
        {
            this.sTransID = transID;
            this.sRequestorIP = requestorIP;
            this.sUser = user;
            this.iOpLogID = opLogID;
            this.hTable = new Hashtable();
            this.sUniqueKey = new NodeUtility().GenerateTransactionID();
        }
        /// <summary>
        /// Constructor for PreParam.
        /// </summary>
        /// <param name="transID">Transaction id.</param>
        /// <param name="requestorIP">Requestor IP address.</param>
        /// <param name="user">User invokes the operation.</param>
        /// <param name="opLogID">Identifier for operation log.</param>
        /// <param name="table">The Additional Parameters.</param>
        public PreParam(string transID, string requestorIP, string user, int opLogID, Hashtable table)
        {
            this.sTransID = transID;
            this.sRequestorIP = requestorIP;
            this.sUser = user;
            this.iOpLogID = opLogID;
            this.hTable = table;
            this.sUniqueKey = new NodeUtility().GenerateTransactionID();
        }
       /// <summary>
       /// Transaction ID for PreParam.
       /// </summary>
        public string TransactionID
        {
            get { return this.sTransID; }
        }
        /// <summary>
        /// Requestor IP Address for PreParam.
        /// </summary>
        public string RequestorIP
        {
            get { return this.sRequestorIP; }
        }
        /// <summary>
        /// User Name for PreParam.
        /// </summary>
        public string User
        {
            get { return this.sUser; }
        }
        /// <summary>
        /// Operation Log Identifier for PreParam.
        /// </summary>
        public int OpLogID
        {
            get { return this.iOpLogID; }
        }
        /// <summary>
        /// Unique Key for PreParam.
        /// </summary>
        public string UniqueKey
        {
            get { return this.sUniqueKey; }
        }
        /// <summary>
        /// Set additional Parameter Value.
        /// </summary>
        /// <param name="key">Key for extra parameter HashTable.</param>
        /// <param name="value">Value for additional parameter HashTable.</param>
        public void SetKeyValue(string key, object value)
        {
            this.hTable.Add(key, value);
        }
        /// <summary>
        /// Get Additional Paramter value.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Object Type</returns>
        public object GetValue(string key)
        {
            return this.hTable[key];
        }
        /// <summary>
        /// Get Additional paramters HashTable.
        /// </summary>
        public Hashtable ValueTable
        {
            get { return this.hTable; }
        }
    }
}
