package Node.DB.Interfaces;

import java.util.Calendar;
import java.sql.Date;

import Node.Biz.Administration.Operation;
import Node.Biz.Administration.OperationLog;
/**
 * <p>This class create INodeOperationLog interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface INodeOperationLog {
  // Create Transaction Log
	  /**
	   * CreateOperationLog
	   * @param transID
	   * @param operID
	   * @param userName
	   * @param status
	   * @param message
	   * @param requestorIP
	   * @param suppliedTransID
	   * @param token
	   * @param nodeAddress
	   * @param returnURL
	   * @param serviceType
	   * @param hostName
	   * @param paramNames
	   * @param paramValues
	   * @return int
	   */
  public int CreateOperationLog (String transID, int operID, String userName, String status, String message, String requestorIP,
                                 String suppliedTransID, String token, String nodeAddress, String returnURL, String serviceType,
                                 String hostName,String[] paramNames,Object[] paramValues);
  /**
   * UpdateOperationLog
   * @param operLogID
   * @param status
   * @param message
   * @param isLastUpdate
   * @param isDebug
   * @return int
   */
   public int UpdateOperationLog (int operLogID, String status, String message, boolean isLastUpdate, boolean isDebug);

   /**
    * UpdateOperationLog
    * @param operLogID
    * @param status
    * @param message
    * @param isLastUpdate
    * @param isDebug
    * @return int
    */
   public int UpdateOperationLog (String transID, String status, String message, boolean isLastUpdate, boolean isDebug);

// Log Operation Status
  /**
   * CreateOperationLog
   * @param operLogID
   * @param status
   * @param message
   * @param isLastUpdate
   * @return int
   */
  public int UpdateOperationLog (int operLogID, String status, String message, boolean isLastUpdate);

  // Log Operation Status
  /**
   * UpdateOperationLog
   * @param transID
   * @param status
   * @param message
   * @param isLastUpdate
   * @return int
   */
  public int UpdateOperationLog (String transID, String status, String message, boolean isLastUpdate);

  /**
   * UpdateOperationLogUserName
   * @param transID
   * @param userName
   * @return int
   */
  public int UpdateOperationLogUserName (String transID, String userName);

  /**
   * UpdateOperationLogToken
   * @param transID
   * @param token
   * @return int
   */
  public int UpdateOperationLogToken (String transID, String token);


  /**
   * Get Transaction Status
   * @param transactionID String TRANS_ID
   * @return String STATUS
   */
  public String GetStatus (String transID);

  /**
   * Authorize Token given token and Operation ID
   * @param token String TOKEN
   * @param opID int OPERATION_ID
   * @return String User Name, null otherwise
   */
  public String AuthorizeToken (String token, int opID);

  /**
   * Authorize Token given token, web method name, and operation name
   * @param token String
   * @param webMethod String
   * @param opName String
   * @return String User Name if successfull, null otherwise
   */
  public String AuthorizeToken (String token, String webMethod, String opName);

  /**
   * Get Last Time Operation (Task) Ran
   * @param opID int Operation ID
   * @return Calendar Time, null if not found
   */
  public Calendar GetLastStartTime (int opID);

//changed by charlie zhang 2007-9-20 begin
  /**
   * Get Last Time Operation (Task) Ran
   * @param opID int Operation ID
   * @return Calendar Time, null if not found
   */
  public Calendar GetAccurateLastStartTime(int opID);
//changed by charlie zhang 2007-9-20 end
  /**
   * Search Operation Log
   * @param opName String
   * @param opType String
   * @param wsName String
   * @param status String
   * @param domains String[]
   * @param userName String
   * @param token String
   * @param transID String
   * @param start Date
   * @param end Date
   * @return OperationLog[]
   */
  public OperationLog[] SearchOperationLog (String opName, String opType, String wsName, String status, String[] domains,
                                            String userName, String token, String transID, Date start, Date end, String version_no);

  /**
   * Get Unique Status List
   * @param domains String[] Only for Operation of the Domains in this list
   * @return String[]
   */
  public String[] GetUniqueStatusList (String[] domains);

  /**
   * Get List of Operations
   * @param domains String[] Only for Operation of the Domains in this list
   * @return String[]
   */
  public String[] GetUniqueOperationNameList (String [] domains);

  /**
   * Get Operation Log
   * @param opLogID int
   * @return OperationLog
   */
  public OperationLog GetOperationLog (int opLogID);

  /**
   * Get Operation Log
   * @param transID String
   * @return OperationLog
   */
  public OperationLog GetOperationLog (String transID);

  /**
   * Get User Name based on Token Provided
   * @param token String
   * @return String
   */
  public String GetUserName (String token);

  /**
   * Get (encrypted) password from Authentication Request
   * @param token String the token returned from the Authenticate
   * @return String encrypted password
   */
  public String GetPassword (String token);

  /**
   * Get Authentication Operation that Produced the Token
   * If No Authentication Operation is found, then the default (PASSWORD) Authentication Operation is returned
   * @param token String token that the Authentication Method returned
   * @return Operation
   */
  public Operation GetAuthenticationOperation (String token);
  /**
   * Check if a operation has operation log
   * @param operation id
   * @return boolean
   */
  public boolean hasOperationLog (int operationID);
  /**
   * Check if a operation has operation log
   * @param user name
   * @return boolean
   */
  public boolean hasOperationLog (String username);
}
