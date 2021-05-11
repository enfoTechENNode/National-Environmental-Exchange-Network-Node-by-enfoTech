using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Core
{
    /// <summary>
    /// The Class is defined and stored the constants which are being used in Node Application
    /// </summary>
    public class Phrase
    {   
        /// <summary>
        /// Node Version Variable or Session Key.
        /// </summary>
        public const string VERSION_NO = "VERSION_NO";
        /// <summary>
        /// Node Spec 1.1 version
        /// </summary>
        public const string VERSION_11 = "VER_11";
        /// <summary>
        /// Node Spec. 2.0.
        /// </summary>
        public const string VERSION_20 = "VER_20";
        /// <summary>
        /// The file location of Logger.
        /// </summary>
        public static string LoggerPath = System.AppDomain.CurrentDomain.BaseDirectory + "FatalExceptionLog.txt";
        /// <summary>
        /// The LoggerLeveal. The default value is Node.Core.Logging.Logger.LEVEL_DEBUG.
        /// </summary>
        public static int LoggerLevel = Node.Core.Logging.Logger.LEVEL_DEBUG;
        /// <summary>
        /// The key for LOG_PATH.
        /// </summary>
        public const string LOG_PATH_KEY = "LOG_PATH";
        /// <summary>
        /// The key for LOG_PATH_KEY.
        /// </summary>
        public const string SOLICIT_LOG_PATH_KEY = "SOLICIT_LOG_PATH";
        /// <summary>
        /// The key for TRANSFORMRESULT.
        /// </summary>
        public const string TRANSFORMRESULT = "TRANSFORMRESULT";
        /// <summary>
        /// The crypted key for password.
        /// </summary>
        public const string CryptKey = "sefldfjlksdfj";
        /// <summary>
        /// The key for USER_SESSION_KEY.
        /// </summary>
        public const string USER_SESSION_KEY = "USER_SESSION_KEY";

        #region Node Web Service Error Messages

        /// <summary>
        /// Indicates that the Security Token Provided does Not Authorize the Owner of the Token to Call this Operation
        /// </summary>
        public const string E_INVALID_TOKEN = "E_InvalidToken";

        /// <summary>
        /// Indicates that the Service is Unavailable
        /// </summary>
        public const string E_SERVICE_UNAVAILABLE = "E_ServiceUnavailable";

        /// <summary>
        /// Indicates that the User is Unknown
        /// </summary>
        public const string E_UNKNOWN_USER = "E_UnknownUser";

        /// <summary>
        /// Indicates that the Provided Password is Incorrect
        /// </summary>
        public const string E_INVALID_PASSWORD = "E_InvalidPassword";

        /// <summary>
        /// Indicates that the User is Inactive
        /// </summary>
        public const string E_INACTIVE_USER = "E_InactiveUser";

        /// <summary>
        /// Indicates that the User Does Not have Permission
        /// </summary>
        public const string E_INVALID_PERMISSION = "E_InvalidPermission";

        /// <summary>
        /// Indicates the there was some Internal Error
        /// </summary>
        public const string E_INTERNAL_ERROR = "E_InternalError";

        /// <summary>
        /// Indicates the the Transaction Could Not Be Located
        /// </summary>
        public const string E_TRANSACTION_NOT_FOUND = "E_TransactionId";

        /// <summary>
        /// Indicates one or more of the Input Parameters was Invalid
        /// </summary>
        public const string E_INVALID_PARAMETER = "E_InvalidParameter";

        /// <summary>
        /// The user credential is invalid.
        /// </summary>
        public const string E_INVALID_CREDENTIAL = "E_InvalidCredential";

        /// <summary>
        /// A transaction ID could not be found.
        /// </summary>
        public const string E_TRANSACTION_ID = "E_TransactionId";

        /// <summary>
        /// The requested method is not supported.
        /// </summary>
        public const string E_UNKNOWN_METHOD = "E_UnknownMethod";

        /// <summary>
        /// The operation could not be performed due to lack of privilege.
        /// </summary>
        public const string E_ACCESS_DENIED = "E_AccessDenied";

        /// <summary>
        /// The security token has expired.
        /// </summary>
        public const string E_TOKEN_EXPIRED = "E_TokenExpired";

        /// <summary>
        /// The requested file could not be located.
        /// </summary>
        public const string E_FILE_NOT_FOUND = "E_FileNotFound";

        /// <summary>
        /// XML schema or schematron validation error.
        /// </summary>
        public const string E_VALIDATION_FAILED = "E_ValidationFailed";

        /// <summary>
        /// The service is too busy to handle the request at this time, please try later.
        /// </summary>
        public const string E_SERVER_BUSY = "E_ServerBusy";

        /// <summary>
        /// The RowId parameter is out of range.
        /// </summary>
        public const string E_ROWID_OUT_OF_RANGE = "E_RowIdOutofRange";

        /// <summary>
        /// The requested feature is not supported.
        /// </summary>
        public const string E_FEATURE_UNSUPPORTED = "E_FeatureUnsupported";

        /// <summary>
        /// The request is a different version of the protocol.
        /// </summary>
        public const string E_VERSION_MISMATCH = "E_VersionMismatch";

        /// <summary>
        /// The name element in the nodeDocument structure is invalid.
        /// </summary>
        public const string E_INVALID_FILE_NAME = "E_InvalidFileName";

        /// <summary>
        /// The type element in the nodeDocument structure is invalid or not supported.
        /// </summary>
        public const string E_INVALID_FILE_TYPE = "E_InvalidFileType";

        /// <summary>
        /// The dataflow element in a request message is not supported.
        /// </summary>
        public const string E_INVALID_DATA_FLOW = "E_InvalidDataFlow";

        /// <summary>
        /// The authentication method is not supported.
        /// </summary>
        public const string E_AUTH_METHOD = "E_AuthMethod";

        /// <summary>
        /// An unknown or undefined error has occurred.
        /// </summary>
        public const string E_UNKNOWN = "E_Unknown";

        /// <summary>
        /// The result set specified is too large to return.
        /// </summary>
        public const string E_QUERY_RETURN_SET_TOO_BIG = "E_QueryReturnSetTooBig";

        /// <summary>
        /// The database returned an error.
        /// </summary>
        public const string E_DBMS_ERROR = "E_DBMSError";

        /// <summary>
        /// The recipient functionality is not supported
        /// </summary>
        public const string E_RECIPIENT_NOT_SUPPORTED = "E_RecipientNotSupported";

        /// <summary>
        /// The NotificationURI functionality is not supported.
        /// </summary>
        public const string E_NOTIFICATION_URI_NOT_SUPPORTED = "E_NotificationURINotSupported";
        #endregion

        #region Node Status Constants

        /// <summary>
        /// Running Status String
        /// </summary>
        public const string STATUS_RUNNING = "Running";

        /// <summary>
        /// Stopped Status String
        /// </summary>
        public const string STATUS_STOPPED = "Stopped";


        #endregion

        #region Operation Status Constants

        /// <summary>
        /// Completed Status String
        /// </summary>
        public const string STATUS_COMPLETED = "Completed";

        /// <summary>
        /// Received Status String
        /// </summary>
        public const string STATUS_RECEIVED = "Received";

        /// <summary>
        /// Failed Status String
        /// </summary>
        public const string STATUS_FAILED = "Failed";

        /// <summary>
        /// Parsed Error Status String
        /// </summary>
        public const string STATUS_PROCESSED= "Processed";

        /// <summary>
        /// Submitted Status String
        /// </summary>
        public const string STATUS_SUBMITTED = "Submitted";

        /// <summary>
        /// Done Status String
        /// </summary>
        public const string STATUS_DONE = "Done";

        /// <summary>
        /// Parsed Status String
        /// </summary>
        public const string STATUS_PARSED = "Parsed";
        /// <summary>
        /// Parsed Error Status String
        /// </summary>
        public const string STATUS_PARSED_ERROR = "ParsedError";


        /// <summary>
        /// Completed Status Message
        /// </summary>
        public const string MESSAGE_COMPLETED = "The Transaction Request has been Completed.";

        /// <summary>
        /// Received Status Message
        /// </summary>
        public static string MESSAGE_RECEIVED = "The Transaction Request has been Received and will begin Processing Soon";

        /// <summary>
        /// Failed Status Message
        /// </summary>
        public const string MESSAGE_FAILED = "The Transaction Encountered a Problem and cannot continue: ";


        /// <summary>
        /// Completed Status Message
        /// </summary>
        public const string MESSAGE_PROCESSED = "The Transaction Request been Processed.";

        #endregion

        #region Node Web Service Names
        /// <summary>
        /// Name of Node WebService for AUTHENTICATE.
        /// </summary>
        public const string WEB_SERVICE_AUTHENTICATE = "AUTHENTICATE";
        /// <summary>
        /// Name of Node WebService for DOWNLOAD.
        /// </summary>
        public const string WEB_SERVICE_DOWNLOAD = "DOWNLOAD";
        /// <summary>
        /// Name of Node WebService for GETSERVICES.
        /// </summary>
        public const string WEB_SERVICE_GETSERVICES = "GETSERVICES";
        /// <summary>
        /// Name of Node WebService for GETSTATUS.
        /// </summary>
        public const string WEB_SERVICE_GETSTATUS = "GETSTATUS";
        /// <summary>
        /// Name of Node WebService for NODEPING.
        /// </summary>
        public const string WEB_SERVICE_NODEPING = "NODEPING";
        /// <summary>
        /// Name of Node WebService for NOTIFY.
        /// </summary>
        public const string WEB_SERVICE_NOTIFY = "NOTIFY";
        /// <summary>
        /// Name of Node WebService for QUERY.
        /// </summary>
        public const string WEB_SERVICE_QUERY = "QUERY";
        /// <summary>
        /// Name of Node WebService for SOLICIT.
        /// </summary>
        public const string WEB_SERVICE_SOLICIT = "SOLICIT";
        /// <summary>
        /// Name of Node WebService for SUBMIT.
        /// </summary>
        public const string WEB_SERVICE_SUBMIT = "SUBMIT";
        /// <summary>
        /// Name of Node WebService for EXECUTE.
        /// </summary>
        public const string WEB_SERVICE_EXECUTE = "EXECUTE";
        /// <summary>
        /// Name of Node WebService for AllServices.
        /// </summary>
        public const string WEB_SERVICE_ALL = "AllServices";
        #endregion

        #region Operation Types
        /// <summary>
        /// Operation Type: Web Service. 
        /// </summary>
        public const string OPERATION_TYPE_WEB_SERVICE = "WEB_SERVICE";
        /// <summary>
        /// Operation Type: Schedule Task. 
        /// </summary>
        public const string OPERATION_TYPE_SCHEDULED_TASK = "SCHEDULED_TASK";

        #endregion

        #region User
        /// <summary>
        /// The session key, it is used in User Module of Node Application.
        /// </summary>
        public const string USER_SELECTED_PRIVILEDGES = "USER_SELECTED_PRIVILEDGES";
        /// <summary>
        /// The session key, it is used in User Module of Node Application.
        /// </summary>
        public const string USER_UNSELECTED_PRIVILEDGES = "USER_UNSELECTED_PRIVILEDGES";

        #endregion

        #region Database Types
        /// <summary>
        /// Assambly Name for SQL Server Provider. 
        /// </summary>
        public const string DB_TYPE_SQL_SERVER = "SYSTEM.DATA.SQLCLIENT";
        /// <summary>
        /// Assambly Name for Oracle Provider. 
        /// </summary>
        public const string DB_TYPE_ORACLE_9I = "SYSTEM.DATA.ORACLECLIENT";
        /// <summary>
        /// The Version for SQL Server Provider. 
        /// </summary>
        public const string DB_PROVIDER_MSSQL2000 = "MSSQL2000";
        /// <summary>
        /// The Version for SQL Server Provider. 
        /// </summary>
        public const string DB_PROVIDER_MSSQL2005 = "MSSQL2005";
        /// <summary>
        /// The Version for Oracle Provider. 
        /// </summary>
        public const string DB_PROVIDER_ORACLE = "ORACLE";        

        #endregion

        #region Account Types
        /// <summary>
        /// Node Admin User Type for Local Node User 
        /// </summary>
        public const string LOCAL_NODE_USER = "LOCAL_NODE_USER";
        /// <summary>
        /// Node Admin User Type for Console User 
        /// </summary>
        public const string CONSOLE_USER = "CONSOLE_USER";
        /// <summary>
        /// Node Admin User Type for NAAS Node User 
        /// </summary>
        public const string NAAS_NODE_USER = "NAAS_NODE_USER";
        /// <summary>
        /// Node DOMAIN Admin.  
        /// </summary>
        public const string NODE_DOMAIN_ADMIN = "NODE_DOMAIN_ADMIN";


        #endregion

        #region Default Processes
        /// <summary>
        /// The DLL file location in Node Application. 
        /// </summary>
        public const string DEFAULT_DLL = "Bin\\Node.Core.dll";
        /// <summary>
        /// The class name for default authenticate PlugIn. 
        /// </summary>
        public const string DEFAULT_AUTHENTICATE = "Node.Core.Default.Authenticate.Process";
        /// <summary>
        /// The class name for default download PlugIn. 
        /// </summary>
        public const string DEFAULT_DOWNLOAD = "Node.Core.Default.Download.Process";
        /// <summary>
        /// The class name for default getservices PlugIn. 
        /// </summary>
        public const string DEFAULT_GETSERVICES = "Node.Core.Default.GetServices.Process";
        /// <summary>
        /// The class name for default getstatus PlugIn. 
        /// </summary>
        public const string DEFAULT_GETSTATUS = "Node.Core.Default.GetStatus.Process";
        /// <summary>
        /// The class name for default nodeping PlugIn. 
        /// </summary>
        public const string DEFAULT_NODEPING = "Node.Core.Default.NodePing.Process";
        /// <summary>
        /// The class name for default notify PlugIn. 
        /// </summary>
        public const string DEFAULT_NOTIFY = "Node.Core.Default.Notify.Process";
        /// <summary>
        /// The class name for default submit PlugIn. 
        /// </summary>
        public const string DEFAULT_SUBMIT = "Node.Core.Default.Submit.Process";
        
        #endregion

        #region Node Admin Home Page Dashboard
        /// <summary>
        /// The Session Key for default row number. 
        /// </summary>
        public const string DEFAULT_ROWNUM = "DEFAULT_ROWNUM";
        /// <summary>
        /// The session key for default top number. 
        /// </summary>
        public const string DEFAULT_TOPNUM = "DEFAULT_TOPNUM";
        /// <summary>
        /// The session key for default pagesize. 
        /// </summary>
        public const string DEFAULT_PAGESIZE = "DEFAULT_PAGESIZE";
        #endregion

        #region Operation Parameter Keys
        /// <summary>
        /// A operation parameter kay for flow operation.
        /// </summary>
        public const string OP_PAR_FLOW_OPERATION = "flowOperation";
        /// <summary>
        /// A operation parameter key for submitObject
        /// </summary>
        public const string OP_PAR_SUBMIT_OBJECT = "submitObject";
        /// <summary>
        /// A operation parameter key for queryObject
        /// </summary>
        public const string OP_PAR_QUERY_OBJECT = "queryObject";
        /// <summary>
        /// A operation parameter key for solicitObject
        /// </summary>
        public const string OP_PAR_SOLICIT_OBJECT = "solicitObject";
        /// <summary>
        /// A operation parameter key for executeObject
        /// </summary>
        public const string OP_PAR_EXECUTE_OBJECT = "executeObject";
        #endregion

        #region Email Template Bookmark keys
        /// <summary>
        /// Bookmark key TransactionID for Email Template
        /// </summary>
        public const string BM_TRANSACTION_ID = "TransactionID";
        /// <summary>
        /// Bookmark key ReceivingNodeAddress for Email Template
        /// </summary>
        public const string BM_RECEIVING_NODE_ADDRESS = "ReceivingNodeAddress";
        /// <summary>
        /// Bookmark key OriginatingNodeAddress for Email Template
        /// </summary>
        public const string BM_ORIGINATING_NODE_ADDRESS = "OriginatingNodeAddress";
        /// <summary>
        /// Bookmark key StatusCode for Email Template
        /// </summary>
        public const string BM_STATUS_CODE = "StatusCode";
        /// <summary>
        /// Bookmark key StatusDetail for Email Template
        /// </summary>
        public const string BM_STATUS_DETAIL = "StatusDetail";
        #endregion

        #region Node DocumentName
        /// <summary>
        /// The document name for Node Report in Node 2.0 spec.
        /// </summary>
        public const string DN_NODE20_REPORT = "Node20.Report";
        /// <summary>
        /// The document name for Node Error in Node 2.0 spec.
        /// </summary>
        public const string DN_NODE20_ERROR = "Node20.Error";
        /// <summary>
        /// The document name for Node Original in Node 2.0 spec.
        /// </summary>
        public const string DN_NODE20_ORIGINAL = "Node20.Original";
        /// <summary>
        /// The document name for Node Processed in Node 2.0 spec.
        /// </summary>
        public const string DN_NODE20_PROCESSED = "Node20.Processed";
        #endregion

        #region Node Notify Message Parameter
        /// <summary>
        /// Node Notify Message Parameter for DataFlow.
        /// </summary>
        public const string NP_DATA_FLOW = "DataFlow";
        /// <summary>
        /// Node Notify Message Parameter for MessageCategory.
        /// </summary>
        public const string NP_MESSAGE_CATEGORY = "MessageCategory";
        /// <summary>
        /// Node Notify Message Parameter for MessageName.
        /// </summary>
        public const string NP_MESSAGE_NAME = "MessageName";
        /// <summary>
        /// Node Notify Message Parameter for MessageStatus.
        /// </summary>
        public const string NP_MESSAGE_STATUS = "MessageStatus";
        /// <summary>
        /// Node Notify Message Parameter for MessageDetail.
        /// </summary>
        public const string NP_MESSAGE_DETAIL = "MessageDetail";
        /// <summary>
        /// Node Notify Message Parameter for ObjectId.
        /// </summary>
        public const string NP_OBJECT_ID = "ObjectId";
        #endregion

        #region Table Name
        /// <summary>
        /// Table name for NODE_OPERATION_LOG in Node Database.
        /// </summary>
        public const string TBL_OPERATION_LOG = "NODE_OPERATION_LOG";
        /// <summary>
        /// Table name for NODE_OPERATION_LOG_PARAMETER in Node Database.
        /// </summary>
        public const string TBL_OPERATION_LOG_PARAMETER = "NODE_OPERATION_LOG_PARAMETER";
        #endregion

        #region SysConfig Type Code
        /// <summary>
        /// Config type XSLT for SYS_CONFIG
        /// </summary>
        public const string CONFIG_TYPE_XSLT = "XSLT";
        /// <summary>
        /// Config type RULE for SYS_CONFIG
        /// </summary>
        public const string CONFIG_TYPE_RULE = "RULE";
        /// <summary>
        /// Config type COMPOSE for SYS_CONFIG
        /// </summary>
        public const string CONFIG_TYPE_COMPOSE = "COMPOSE";
        /// <summary>
        /// Config type POPULATE for SYS_CONFIG
        /// </summary>
        public const string CONFIG_TYPE_POPULATE = "POPULATE";
        /// <summary>
        /// Config type COMPOSE_TEMPLATE for SYS_CONFIG
        /// </summary>
        public const string CONFIG_TYPE_COMPOSE_TEMPLATE = "COMPOSE_TEMPLATE";
        #endregion

    }
}
