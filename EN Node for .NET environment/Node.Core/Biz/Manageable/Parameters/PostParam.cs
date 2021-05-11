using System;
using System.Collections;

using Node.Core.Util;

namespace Node.Core.Biz.Manageable.Parameters
{
    /// <summary>
    /// PostParam is stored data from PostProcess.
    /// </summary>
    public class PostParam
    {
        private string sUniqueKey = null;
        private string sTransID = null;
        private string sRequestorIP = null;
        private string sUser = null;
        private int iOpLogID = -1;
        private object oResult = null;
        private Hashtable hTable = null;
        /// <summary>
        /// Constructor for PreParam.
        /// </summary>
        /// <param name="transID">Transaction id.</param>
        /// <param name="requestorIP">Requestor IP address.</param>
        /// <param name="user">User invokes the operation.</param>
        /// <param name="opLogID">Identifier for operation log.</param>
        /// <param name="result">The result of Process.</param>
        /// <param name="table">The Additional Parameters.</param>
        public PostParam(string transID, string requestorIP, string user, int opLogID, object result, Hashtable table)
        {
            this.sTransID = transID;
            this.sRequestorIP = requestorIP;
            this.iOpLogID = opLogID;
            this.oResult = result;
            this.hTable = table;
            this.sUser = user;
            this.sUniqueKey = new NodeUtility().GenerateTransactionID();
        }
        /// <summary>
        /// Transaction ID for PostParam.
        /// </summary>
        public string TransactionID
        {
            get { return this.sTransID; }
        }
        /// <summary>
        /// Requestor IP Address for PostParam.
        /// </summary>
        public string RequestorIP
        {
            get { return this.sRequestorIP; }
        }
        /// <summary>
        /// User Name for PostParam.
        /// </summary>
        public string User
        {
            get { return this.sUser; }
        }
        /// <summary>
        /// Operation Log Identifier for PostParam.
        /// </summary>
        public int OpLogID
        {
            get { return this.iOpLogID; }
        }
        /// <summary>
        /// The result of Process.
        /// </summary>
        public object Result
        {
            get { return this.oResult; }
        }
        /// <summary>
        /// Unique Key for PostParam.
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
