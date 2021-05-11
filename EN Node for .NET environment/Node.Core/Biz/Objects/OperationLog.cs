using System;
using System.Collections;
using System.Data;

using Node.Core.Data;
using Node.Core.Data.Interfaces;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// OperationLog Class retrieves operation log related Informatuion.
    /// Database Source: NODE_OPERATION_LOG.
    /// </summary>
    public class OperationLog
    {
        #region Public Static Methods

        /// <summary>
        /// Return DataTable With Searched Logs
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="opType">Operation Type, null or empty string if not supplied</param>
        /// <param name="wsID">Web Service ID, less than 0 if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">The Domain Administrator logged in</param>
        /// <returns>
        /// Columns: OPERATION_NAME
        /// </returns>
        public static DataTable SearchLogs(string opName, string opType, int wsID, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            return new DBManager().GetOperationLogsDB().SearchOperationLogs(opName, opType, wsID, status, domainID, userName, token, transID, startDate, endDate, domainAdmin);
        }

        /// <summary>
        /// Return DataTable With Searched Tasks
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">The Domain Administrator logged in</param>
        /// <returns>
        /// Columns: OPERATION_NAME
        /// </returns>
        public static DataTable SearchTasks(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            return new DBManager().GetOperationLogsDB().SearchOperationTasks(opName, status, domainID, userName, token, transID, startDate, endDate, domainAdmin);
        }

            /// <summary>
        /// Return DataTable With Searched Tasks
        /// </summary>
        /// <param name="opName">Operation Name, null or empty string if not supplied</param>
        /// <param name="status">Status, null or empty string if not supplied</param>
        /// <param name="domainID">Domain, less than 0 if not supplied</param>
        /// <param name="userName">User Name, null or empty string if not supplied</param>
        /// <param name="token">Security Token, null or empty string if not supplied</param>
        /// <param name="transID">Transaction ID, null or empty string if not supplied</param>
        /// <param name="startDate">Start Range for Log, DateTime.MinValue if not supplied</param>
        /// <param name="endDate">Emd Range for Log, DateTime.MaxValue if not supplied</param>
        /// <param name="domainAdmin">The Domain Administrator logged in</param>
        /// <returns>
        /// Columns: OPERATION_NAME
        /// </returns>
        public static DataSet SearchNotifications(string opName, string status, int domainID, string userName, string token, string transID, DateTime startDate, DateTime endDate, string domainAdmin)
        {
            return new DBManager().GetOperationLogsDB().SearchNotifications(opName, status, domainID, userName, token, transID, startDate, endDate, domainAdmin);
        }

        /// <summary>
        /// Get Web Service ID-Name Pairs
        /// First entry is blank
        /// </summary>
        /// <returns>
        /// Columns: WEB_SERVICE_ID, WEB_SERVICE_NAME
        /// </returns>
        public static DataTable GetWebServiceIDNamePairs()
        {
            DataTable dt = new DBManager().GetOperationLogsDB().GetWebServiceIDNamePairs();
            DataRow dr = dt.NewRow();
            dr["WEB_SERVICE_ID"] = -1;
            dr["WEB_SERVICE_NAME"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        /// <summary>
        /// Get Unique Operation Log Statuses
        /// First entry is blank
        /// </summary>
        /// <returns>
        /// Unique Statuses of Operation Logs
        /// </returns>
        public static ArrayList GetUniqueOperationStatuses(string domainAdmin)
        {
            ArrayList list = new DBManager().GetOperationLogsDB().GetUniqueOpStatuses(domainAdmin);
            list.Insert(0, "");
            return list;
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// OperationLogID of OperationLog
        /// </summary>
        public int OperationLogID
        {
            get { return this.logID; }
            set { this.logID = value; }
        }
        /// <summary>
        /// TransactionID of OperationLog
        /// </summary>
        public string TransactionID
        {
            get { return this.transID; }
            set { this.transID = value; }
        }
        /// <summary>
        /// OperationName of OperationLog
        /// </summary>
        public string OperationName
        {
            get { return this.opName; }
            set { this.opName = value; }
        }
        /// <summary>
        /// DomainName of OperationLog
        /// </summary>
        public string DomainName
        {
            get { return this.domName; }
            set { this.domName = value; }
        }
        /// <summary>
        /// OperationType of OperationLog
        /// </summary>
        public string OperationType
        {
            get { return this.opType; }
            set
            {
                switch (value)
                {
                    case Phrase.OPERATION_TYPE_WEB_SERVICE:
                        this.opType = value;
                        break;
                    case Phrase.OPERATION_TYPE_SCHEDULED_TASK:
                        this.opType = value;
                        break;
                    default:
                        throw new Exception("Invalid Operation Type: OperationLog.OperationType must be one of " + Phrase.OPERATION_TYPE_WEB_SERVICE + " or " + Phrase.OPERATION_TYPE_SCHEDULED_TASK);
                }
            }
        }
        /// <summary>
        /// WebServiceName of OperationLog
        /// </summary>
        public string WebServiceName
        {
            get { return this.wsName; }
            set { this.wsName = value; }
        }
        /// <summary>
        /// UserName of OperationLog
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        /// <summary>
        /// RequestorIP of OperationLog
        /// </summary>
        public string RequestorIP
        {
            get { return this.requestorIP; }
            set { this.requestorIP = value; }
        }
        /// <summary>
        /// SuppliedTransactionID of OperationLog
        /// </summary>
        public string SuppliedTransactionID
        {
            get { return this.supplTransID; }
            set { this.supplTransID = value; }
        }
        /// <summary>
        /// Token of OperationLog
        /// </summary>
        public string Token
        {
            get { return this.token; }
            set { this.token = value; }
        }
        /// <summary>
        /// NodeAddress of OperationLog
        /// </summary>
        public string NodeAddress
        {
            get { return this.nodeAddresss; }
            set { this.nodeAddresss = value; }
        }
        /// <summary>
        /// ReturnURL of OperationLog
        /// </summary>
        public string ReturnURL
        {
            get { return this.returnURL; }
            set { this.returnURL = value; }
        }
        /// <summary>
        /// ServiceType of OperationLog
        /// </summary>
        public string ServiceType
        {
            get { return this.serviceType; }
            set { this.serviceType = value; }
        }
        /// <summary>
        /// StartDate of OperationLog
        /// </summary>
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
        /// <summary>
        /// EndDate of OperationLog
        /// </summary>
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        /// <summary>
        /// HostName of OperationLog
        /// </summary>
        public string HostName
        {
            get { return this.hostName; }
            set { this.hostName = value; }
        }
        /// <summary>
        /// Parameters of OperationLog
        /// </summary>
        public ArrayList Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }
        /// <summary>
        /// Statuses of OperationLog
        /// </summary>
        public ArrayList Statuses
        {
            get { return this.statuses; }
            set { this.statuses = value; }
        }
        /// <summary>
        /// CreatedDate of OperationLog
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }
        /// <summary>
        /// CreatedBy of OperationLog
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }
        /// <summary>
        /// UpdatedDate of OperationLog
        /// </summary>
        public DateTime UpdatedDate
        {
            get { return this.updatedDate; }
            set { this.updatedDate = value; }
        }
        /// <summary>
        /// UpdatedBy of OperationLog
        /// </summary>
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set { this.updatedBy = value; }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Contructor of OperationLog.
        /// </summary>
        public OperationLog()
        {
        }
        /// <summary>
        /// Constructor of OpeationLog.
        /// </summary>
        /// <param name="opLogID">The id of operation log.</param>
        public OperationLog(int opLogID)
        {
            this.logID = opLogID;
            OperationLog inputLog = new DBManager().GetOperationLogsDB().GetOperationLog(opLogID);
            this.Init(inputLog);
        }

        #endregion

        #region Private Fields

        private int logID = -1;
        private string transID = null;
        private string opName = null;
        private string domName = null;
        private string wsName = null;
        private string opType = null;
        private string userName = null;
        private string requestorIP = null;
        private string supplTransID = null;
        private string token = null;
        private string nodeAddresss = null;
        private string returnURL = null;
        private string serviceType = null;
        private DateTime startDate = DateTime.MinValue;
        private DateTime endDate = DateTime.MaxValue;
        private string hostName = null;
        private ArrayList parameters = new ArrayList();
        private ArrayList statuses = new ArrayList();
        private DateTime createdDate = DateTime.MinValue;
        private string createdBy = null;
        private DateTime updatedDate = DateTime.MinValue;
        private string updatedBy = null;

        #endregion

        #region Private Methods

        private void Init(OperationLog log)
        {
            this.logID = log.logID;
            this.transID = log.transID;
            this.opName = log.opName;
            this.domName = log.domName;
            this.opType = log.opType;
            this.wsName = log.wsName;
            this.userName = log.userName;
            this.requestorIP = log.requestorIP;
            this.supplTransID = log.supplTransID;
            this.token = log.token;
            this.returnURL = log.returnURL;
            this.serviceType = log.serviceType;
            this.startDate = log.startDate;
            this.endDate = log.endDate;
            this.hostName = log.hostName;
            this.parameters = log.parameters;
            this.statuses = log.statuses;
            this.createdDate = log.createdDate;
            this.createdBy = log.createdBy;
            this.updatedDate = log.updatedDate;
            this.updatedBy = log.updatedBy;
        }

        #endregion
    }
}
