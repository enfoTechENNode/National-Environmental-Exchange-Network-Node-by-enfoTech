package Node;
/**
 * <p>This class create Phrase.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Phrase {
  public Phrase() {
  }

  // Database Type
  /**
   * Name of Database Type
   */
  public static String ORACLE9i = "oracle9i";
  /**
   * Name of Database Type
   */
  public static String ORACLE10i = "oracle10i";
  public static String ORACLE10g = "oracle10g";

  // Session Constants
  /**
   * Name of the User Session Attribute
   */
  public static String USER_SESSION = "USER_SESSION";
  /**
   * Name of the Domain Session Attribute
   */
  public static String DOMAIN_SESSION = "DOMAIN_SESSION";
  /**
   * Name of the status Session counter Attribute
   */
  public static String STATUS_SESSION_COUNTER = "STATUS_SESSION_COUNTER";
  /**
   * Name of the status Session Max counter Attribute
   */
  public static String STATUS_SESSION_TIMEOUT = "sessionTimeOut";
  /**
   * ID of Document to Download
   */
  public static String DOWNLOAD_ARRAY_SESSION = "DOWNLOAD_ARRAY_SESSION";
  /**
   * ID of Document to Download
   */
  public static String DOWNLOAD_SESSION = "DOWNLOAD_SESSION";
  /**
   * ID of Document to Download
   */
  public static String DOWNLOAD_OPERATIONDOCUMENT_SESSION = "DOWNLOAD_OPERATIONDOCUMENT_SESSION";

  /**
   * ID of Document to Download
   */
  public static String DOWNLOAD_TEMPFILE_SESSION = "DOWNLOAD_TEMPFILE_SESSION";
  /*Changed 7-20-2007 Begin */
  public static String AQS_SESSION = "AQS_SESSION";

  public static String AQS_UUID = "AQS_UUID";

/*Changed 7-20-2007 End */

  // JNDI Constants
  /**
   * Name of the JNDI Lookup Name
   */
  public static String httpVersion = "http/PROTOCOL_VERSION";
  /**
   * Name of the Node version
   */
  public static String NodeVersion = "version";
 
  /**
   * Node version
   */
  public static String ver_1 = "VER_11";
  public static String ver_2 = "VER_20";

  /**
   * Name of the JNDI Lookup Name
   */
  public static String jndiNodeVersion = "node/version";

  /**
   * Name of the JNDI Lookup Name
   */
  public static String jndiNodeBuildDate = "node/buildDate";

  // JNDI DB Constants
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbFullString = "node/dbFullString";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbType = "node/dbType";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbJNDI = "node/dbJNDIName";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbServer = "node/dbServer";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbPort = "node/dbPort";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbSID = "node/dbSID";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbUser = "node/dbUser";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dbPassword = "node/dbPassword";
  /**
   * Name of the JNDI Lookup Name
   */
  public static String StartWizardTask = "DataFlow.Engine.StartTask";  
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dotNetHost = "node/dotNetHost";  
  /**
   * Name of the JNDI Lookup Name
   */
  public static String dotNetHostPort = "node/dotNetHostPort";
  // Logging Constants
  /**
   * Name of the Logging File Location
   */
  public static String JNDIAdministrationLogLocation = "adminLog";
  /**
   * Name of the Logging File Location
   */
  public static String JNDIClientLogLocation = "clientLog";
  /**
   * Name of the Logging File Location
   */
  public static String JNDITaskLogLocation = "taskLog";
  /**
   * Name of the Logging File Location
   */
  public static String JNDIWebServicesLogLocation = "webServicesLog";
  /**
   * Name of the temporary File Location
   */
  public static String JNDITempFilePathLocation = "tempFilePath";
  /**
   * Name of the Logging Session Key
   */
  public static String LoggerSessionKey = "LOGGER_SESSION_KEY";
  /**
   * Name of the log4j Logger
   */
  public static String AdministrationLoggerName = "node.administration.logger";
  /**
   * Name of the log4j Logger
   */
  public static String ClientLoggerName = "node.client.logger";
  /**
   * Name of the log4j Logger
   */
  public static String TaskLoggerName = "node.task.logger";
  /**
   * Name of the log4j Logger
   */
  public static String WebServicesLoggerName = "node.webservices.logger";

  // Cryptography Key
  /**
   * Cryptograhpy Key used to Encrypt/Decrypt Passwords
   */
  public static String CryptKey = "oe3nvjasodjigf";

  // Transaction generate style
  /**
   * Transaction generate style MD5/UUID
   */
  public static String MD5 = "MD5";
  public static String UUID = "UUID";

  // Web Service Exceptions
  /**
   * Soap Exception String
   */
  public static String AccessDenied = "E_AccessDenied";
  public static String AccessDeniedMSG = "The operation could not be performed due to lack of privilege.";
  /**
   * Soap Exception String
   */
  public static String AccessRight = "E_AccessRight";
  /**
   * Soap Exception String
   */
  public static String FileNotFound = "E_FileNotFound";
  public static String FileNotFoundMSG = "The requested file could not be located.";
  /**
   * Soap Exception String
   */
  public static String InternalError = "E_InternalError";
  public static String InternalErrorMSG = "An unrecoverable error occurred while processing the request.";
  /**
   * Soap Exception String
   */
  public static String InvalidParameter = "E_InvalidParameter";
  public static String InvalidParameterMSG = "One of the input parameters is invalid.";
  /**
   * Soap Exception String
   */
  public static String InvalidPreParam = "E_InvalidPreProcess";
  public static String InvalidPreParamMSG = "One of the input parameters is invalid.";
  /**
   * Soap Exception String
   */
  public static String InvalidPostParam = "E_InvalidPostProcess";
  public static String InvalidPostParamMSG = "One of the input parameters is invalid.";
  /**
   * Soap Exception String
   */
  public static String InvalidToken = "E_InvalidToken";
  public static String InvalidTokenMSG = "The security token is invalid.";
  /**
   * Soap Exception String
   */
  public static String ServiceUnavailable = "E_ServiceUnavailable";
  public static String ServiceUnavailableMSG = "The requested data service or web service is undefined.";
  /**
   * Soap Exception String
   */
  public static String UnknownUser = "E_UnknownUser";
  public static String UnknownUserMSG = "The user could not be found.";

  /**
   * Soap Exception String
   */
  public static String InvalidCredential = "E_InvalidCredential";
  public static String InvalidCredentialMSG = "The user credential is invalid.";

  /**
   * Soap Exception String
   */
  public static String Query = "E_Query";
  public static String QueryMSG = "The supplied database logic failed.";
  /**
   * Soap Exception String
   */
  public static String TransactionId = "E_TransactionId";
  public static String TransactionIdMSG = "A transaction ID could not be found.";
  /**
   * Soap Exception String
   */
  public static String UnknownMethod = "E_UnknownMethod";
  public static String UnknownMethodMSG = "The requested method is not supported.";
  /**
   * Soap Exception String
   */
  public static String TokenExpired = "E_TokenExpired";
  public static String TokenExpiredMSG = "The security token has expired.";
  /**
   * Soap Exception String
   */
  public static String ValidationFailed = "E_ValidationFailed";
  public static String ValidationFailedMSG = "XML schema or schematron validation error.";
  /**
   * Soap Exception String
   */
  public static String ServerBusy = "E_ServerBusy";
  public static String ServerBusyMSG = "The service is too busy to handle the request at this time, please try later.";
  /**
   * Soap Exception String
   */
  public static String RowIdOutofRange = "E_RowIdOutofRange";
  public static String RowIdOutofRangeMSG = "The RowId parameter is out of range.";
  /**
   * Soap Exception String
   */
  public static String FeatureUnsupported = "E_FeatureUnsupported";
  public static String FeatureUnsupportedMSG = "The requested feature is not supported.";
  /**
   * Soap Exception String
   */
  public static String VersionMismatch = "E_VersionMismatch";
  public static String VersionMismatchMSG = "The request is a different version of the protocol.";
  /**
   * Soap Exception String
   */
  public static String InvalidFileName = "E_InvalidFileName";
  public static String InvalidFileNameMSG = "The name element in the nodeDocument structure is invalid.";
  /**
   * Soap Exception String
   */
  public static String InvalidFileType = "E_InvalidFileType";
  public static String InvalidFileTypeMSG = "The type element in the nodeDocument structure is invalid or not supported.";
  /**
   * Soap Exception String
   */
  public static String InvalidDataFlow = "E_InvalidDataFlow";
  public static String InvalidDataFlowMSG = "The dataflow element in a request message is not supported.";
  /**
   * Soap Exception String
   */
  public static String InvalidFlowOperation = "E_InvalidFlowOperation";
  public static String InvalidFlowOperationMSG = "The Flow Operation element in a request message is not supported.";
  /**
   * Soap Exception String
   */
  public static String InvalidRequest = "E_InvalidRequest";
  public static String InvalidRequestMSG = "The Request element in a request message is not supported.";
  /**
   * Soap Exception String
   */
  public static String InvalidSQL = "E_InvalidSQL";
  public static String InvalidSQLMSG = "Syntax error in the SQL statement. ";
  /**
   * Soap Exception String
   */
  public static String AuthMethod = "E_AuthMethod";
  public static String AuthMethodMSG = "The authentication method is not supported. ";
  /**
   * Soap Exception String
   */
  public static String Unknown = "E_Unknown";
  public static String UnknownMSG = "An unknown or undefined error has occurred.";
  
  // Web Service Status
  /**
   * Web Service Status
   */
  public static String StartStatus = "Start";
  /**
   * Web Service Status
   */
  public static String ReceivedStatus = "Received";
  /**
   * Web Service Status
   */
  public static String CompleteStatus = "Completed";
  /**
   * Web Service Status
   */
  public static String FailedStatus = "Failed";
  /**
   * Web Service Status
   */
  public static String ProcessingStatus = "Processing";
  /**
   * Web Service Status
   */
  public static String PendingStatus = "Pending";
  /**
   * Web Service Status
   */
  public static String ApprovedStatus = "Approved";
  /**
   * Web Service Status
   */
  public static String ProcessedStatus = "Processed";
  /**
   * Web Service Status
   */
  public static String SubmittedStatus = "Submitted";
  /**
   * Web Service Status
   */
  public static String SavedStatus = "Saved";
  /**
   * Web Service Status
   */
  public static String NoTokenStatus = "No Token";
  /**
   * Web Service Status
   */
  public static String WarningStatus = "Warning";
  /**
   * Web Service Status
   */
  public static String CanceledStatus = "Canceled";
  /**
   * Web Service Status
   */
  public static String UnknownStatus = "Unknown";
  /**
   * Web Service Status
   */
  public static String DoneStatus = "Done";

  /**
   * Web Service Message
   */
  public static String ReceivedMessage = "The transaction was received by the service.";
  /**
   * Web Service Message
   */
  public static String ProcessingMessage = "The transaction is currently being processed.";

  /**
   * Web Service Message
   */
  public static String PendingMessage = "One or more documents are to be downloaded and processed by the service.";
  /**
   * Web Service Message
   */
  public static String ApprovedMessage = "The submission has been approved or certified if it needs approval. However, the documents have not been delivered to the receiver yet.";
  /**
   * Web Service Message
   */
  public static String ProcessedMessage = "The request/submission has been processed.";
  /**
   * Web Service Message
   */
  public static String CanceledMessage = "The transaction has been cancelled by the originator of the request.";
  /**
   * Web Service Message
   */
  public static String UnknownMessage = "The status of the transaction cannot be determined.";
  /**
   * Web Service Message
   */
  public static String CompleteMessage = "The transaction has completed.  No further action(s) will be taken on the request/submission except Node Wizard thread.";

  /**
   * Web Service Message
   */
  public static String WarningMessage = "A non-fatal error has occured while processing the transaction.";

  /**
   * Web Service Message
   */
  public static String FailedMessage = "The transaction has failed. No further action(s) will be taken on the request/submission. The requester should resubmit.";

  // Predefined Param Names
  /**
   * Param Name of Credential used to ensure passwords are encrypted in the database.
   */
  public static String ParamCredential = "Credential";

  // Document Types
  /**
   * Document Type Name
   */
  public static String XML_TYPE = "XML";
  /**
   * Document Type Name
   */
  public static String ZIP_TYPE = "ZIP";
  /**
   * Document Type Name
   */
  public static String BIN_TYPE = "Bin";
  /**
   * Document Type Name
   */
  public static String FLAT_TYPE = "Flat";
  /**
   * Document Type Name
   */
  public static String BASE64_TYPE = "Base64";
  /**
   * Document Type Name
   */
  public static String ODF_TYPE = "ODF";
  /**
   * Document Type Name
   */
  public static String OTHER_TYPE = "Other";

  /**
   * Document Type Name
   */
  public static String NONE_TYPE = "None";
  /**
   * Document Type Name
   */
  public static String ENCRYPT_TYPE = "Encrypt";
  /**
   * Document Type Name
   */
  public static String DIGEST_TYPE = "Digest";
  /**
   * Array of Document Types
   */
  public static String[] DOC_TYPES = new String [] {
    "XML", "ZIP", "Bin", "Flat"
  };

  // GetServices Constants
  /**
   * Service Type Name
   */
  public static String ServiceType = "ServiceType";
  /**
   * Service Type Name
   */
  public static String Interfaces = "Interfaces";
  /**
   * Service Type Name
   */
  public static String Queries = "Query";
  /**
   * Service Type Name
   */
  public static String Domains = "Domain";
  /**
   * Service Type Name
   */
  public static String AllServices = "AllServices";


  
  /**
   * Service Type Name Array
   */
  public static String[] SERVICE_TYPES = new String [] {
    "ServiceType","Interfaces","Query","Domain"
  };

  // Account Types
  /**
   * Account Type Name
   */
  public static String ConsoleUser = "CONSOLE_USER";
  /**
   * Account Type Name
   */
  public static String LocalNodeUser = "LOCAL_NODE_USER";
  /**
   * Account Type Name
   */
  public static String NAASNodeUser = "NAAS_NODE_USER";

  // Domain Names
  /**
   * Node Domain Name
   */
  public static String NODE_DOMAIN = "NODE";

  // Operation Types
  /**
   * Operation Type
   */
  public static String WEB_SERVICE_OPERATION = "WEB_SERVICE";
  /**
   * Operation Type
   */
  public static String SCHEDULED_TASK_OPERATION = "SCHEDULED_TASK";

  // Default Process Names
  /**
   * Default Class Name
   */
  public static String DEFAULT_AUTHENTICATE = "Node.Biz.Default.Authenticate.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_DOWNLOAD = "Node.Biz.Default.Download.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_GETSERVICES = "Node.Biz.Default.GetServices.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_GETSTATUS = "Node.Biz.Default.GetStatus.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_NODEPING = "Node.Biz.Default.NodePing.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_NOTIFY = "Node.Biz.Default.Notify.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_SUBMIT = "Node.Biz.Default.Submit.Process";
  /**
   * Default Class Name
   */
  public static String DEFAULT_EXECUTE = "Node.Biz.Default.Execute.Process";


  // Solicit Constants
  /**
   * Solicit Class Name
   */
  public static String SOLICIT_TASK = "Node.Biz.Default.Solicit.SolicitTask";
  /**
   * Solicit Task Class Name
   */
  public static String SOLICIT_TASK_METHOD = "Execute";

  // Web Method Names
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_AUTHENTICATE = "AUTHENTICATE";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_DOWNLOAD = "DOWNLOAD";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_GETSERVICES = "GETSERVICES";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_GETSTATUS = "GETSTATUS";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_NODEPING = "NODEPING";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_NOTIFY = "NOTIFY";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_QUERY = "QUERY";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_SOLICIT = "SOLICIT";
  /**
   * Web Method Name
   */
  public static String WEB_METHOD_SUBMIT = "SUBMIT";

  /**
   * Web Method Name
   */
  public static String WEB_METHOD_EXECUTE = "EXECUTE";

  /**
   * Status of Tasks/Operations
   */
  public static String EXECUTING_STATUS = "Executing";
  /**
   * Status of Tasks/Operations
   */
  public static String RUNNING_STATUS = "Running";
  /**
   * Status of Tasks/Operations
   */
  public static String STOPPED_STATUS = "Stopped";
  /**
   * Status of Tasks/Operations
   */
  public static String ACTIVE_STATUS = "Active";
  /**
   * Status of Tasks/Operations
   */
  public static String INACTIVE_STATUS = "Inactive";

  /**
   * Status of Document 
   */
  public static String UPLOADED = "Uploaded";
  public static String DOWNLOADED = "Downloaded";

  /**
   * Type of Code
   */
  public static int PRE_PROCESS = 0;
  /**
   * Type of Code
   */
  public static int PROCESS = 1;
  /**
   * Type of Code
   */
  public static int POST_PROCESS = 2;
  
  /**
   * Type of database update/insert
   */
  public static String UPDATE = "update";
  public static String INSERT = "insert";
  
  /**
   * Notify message type
   */
  public static String NOTIFY_DOCUMENT = "Document";
  public static String NOTIFY_STATUS = "Status";
  public static String NOTIFY_EVENT = "Event";

  /*
   *  System config file Name
   */
  public static String SYSTEM_FILE_NAME  = "system.config";
  // WI 21296
  /*
   *  Registration file Name
   */
  public static String REGISTRATION_FILE_NAME  = "GetServices.config";
  /*
   *  Dedl file Name
   */
  public static String DEDL_FILE_NAME  = "dedl.config";
  /*
   *  OperationList file Name
   */
  public static String OPERATIONLIST_FILE_NAME  = "OperationList.config";

  /*
   *  Authentication Method
   */
  public static String AUTHENTICATION_METHOD_PASSWORD  = "PASSWORD";
  
  /**
   * Configuration types.
   */
  public static String CONFIG_TYPE_XSLT = "XSLT";
  public static String CONFIG_TYPE_RULE = "RULE";
  public static String CONFIG_TYPE_COMPOSE = "COMPOSE";
  public static String CONFIG_TYPE_POPULATE = "POPULATE";
  public static String CONFIG_TYPE_COMPOSE_TEMPLATE = "COMPOSE_TEMPLATE";

  /**
   * Special built in Document Name.
   */
  public static String Node20_Report = "Node20.Report";
  public static String Node20_Error = "Node20.Error";
  public static String Node20_Orignal = "Node20.Orignal";
  public static String Node20_Processed = "Node20.Processed";

  /**
   * Special text encode.
   */
  public static String UTF_8 = "UTF-8";
  public static String ISO_8859_1 = "ISO-8859-1";
  public static String US_ASCII = "US-ASCII";

  /**
   * Name of the build date
   */
  public static String buildDate = "buildDate";

  /**
   * Name of the decompressor
   */
  public static String DECOMPRESSOR = "NodeDecompressor";

  /**
   * Name of the unzip
   */
  public static String UNZIP = "unzip";
  
  /**
   * Name of the built in GetStatusTask
   */
  public static String GetStatusTask = "GetStatusTask";
  
  /**
   * Name of the MaxGridRecords
   */
  public static String MaxGridRecords = "MaxGridRecords";

  // WI 21296
  /**
   * Name of the Web Service Parameter Attribute
   */
  public static String WEBSERVICE_PARAMETER_NAME = "name";
  public static String WEBSERVICE_PARAMETER_TYPE = "type";
  public static String WEBSERVICE_PARAMETER_TYPE_DESCRIPTION = "typeDescription";
  public static String WEBSERVICE_PARAMETER_OCCURENCE_NUMBER = "occurenceNumber";
  public static String WEBSERVICE_PARAMETER_ENCODING = "encoding";
  public static String WEBSERVICE_PARAMETER_REQIRED_INDICATOR = "requiredIndicator";
  
  // WI 23658
  /**
   * Name of the AQS Web Service
   */
  public static String WEBSERVICE_NAME_AQS = "WEBSERVICE_AQS";

	// WI 29446
  /**
   * Parameters of the default FTP server
   */
  public static String FTP_SERVER = "host";
  public static String FTP_PORT = "port";
  public static String FTP_USER = "UserID";
  public static String FTP_PASSWORD = "Password";
  public static String FTP_DIRECTORY = "ftp_directory";

  /**
   * String of general use
   */
  public static String TRUE_STRING = "true";
  public static String FALSE_STRING = "false";
  
  /**
   * MIME Type
   */
  public static String MIME_HTML = "text/html";
  public static String MIME_CSV = "text/csv";
  public static String MIME_XML = "application/xml";
  public static String MIME_JSON = "application/json";
  public static String MIME_XLS = "application/vnd.ms-excel";
  public static String MIME_ZIP = "application/octet-stream";

//WI 33641
  /**
   * Simple Yes/No
   */
  public static String SIMPLE_YES = "Y";
  public static String SIMPLE_NO = "N";

  
  /**
   * Rest Service parameters
   */
	public static String DATAFLOW = "dataflow";
	public static String REQUEST = "request";
	public static String PARAMS = "params";
	public static String ROWID = "rowId";
	public static String MAXROWS = "maxRows";
	public static String FORMAT = "format";
	public static String SECURITY_TOKEN = "securitytoken";
	public static String PUBLIC_USERS = "PublicUsers";
	public static String OPID = "opID";

}
