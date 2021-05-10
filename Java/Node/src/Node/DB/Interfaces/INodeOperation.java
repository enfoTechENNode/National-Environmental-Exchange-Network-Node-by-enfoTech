package Node.DB.Interfaces;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Hashtable;

import Node.Biz.Administration.Operation;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create INodeOperation interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeOperation {
  /**
   * Get OPERATION_ID when provided WEB_SERVICE_ID and OPERATION_NAME
   * @param opName String OPERATION_NAME
   * @return int OPERATION_ID, -1 when not found
   */
  public int GetOperationID (int wsID, String opName);

  /**
   * Get OPERATION_ID when provided OPERATION_NAME
   * @param opName String OPERATION_NAME
   * @return int OPERATION_ID, -1 when not found
   */
  public int GetOperationID (String opName);

  /**
   * Get Operation provided WebService Name and Operation Name
   * @param wsName String WebService Name
   * @param opName String Operation Name
   * @return int OperationID, -1 Otherwise
   */
  public int GetOperationID (String wsName, String opName);

  /**
   * Get Operation IDs
   * @param wsNames String[] web service names
   * @param opNames String[] operation names
   * @return int[]
   */
  public int[] GetOperationIDs (String[] wsNames, String[] opNames);

  /**
   * Get DOMAIN_ID when provided OPERATION_ID
   * @param opID int OPERATION_ID
   * @return int DOMAIN_ID, -1 when not found
   */
  public int GetDomainID (int opID);

  /**
   * Get Domain ID
   * @param opName String Operation Name
   * @param wsName String Web Service Name
   * @return int
   */
  public int GetDomainID (String opName, String wsName);

  /**
   * Get Domain Name
   * @param opName String Operation Name
   * @param wsName String Web Service Name
   * @return String Domain Name, null if not found
   */
  public String GetDomainName (String opName, String wsName);

  /**
   * Get OPERATION_STATUS_CD
   * @param opID int OPERATION_ID
   * @return String OPERATION_STATUS_CD
   */
  public String GetOperationStatus (int opID);

  /**
   * Get Operation Status
   * @param opName String String Operation Name
   * @return String Operation Status, null if not found
   */
  public String GetOperationStatus (String opName);

  /**
   * Get Operation Name
   * @param opID int OPERATION_ID
   * @return String OPERATION_NAME
   */
  public String GetOperationName (int opID);

  /**
   * Get Operation Configuration File
   * @param opID int OPERATION_ID
   * @return String OPERATION_CONFIG
   */
  public String GetOperationConfig (int opID);

  /**
   * Get Operation Configuration File
   * @param wsName String WebService name
   * @param opName String Operation Name
   * @return String Configuration File
   */
  public String GetOperationConfig (String wsName, String opName);

  /**
   * Get Operations in a Domain
   * @param domainID int DOMAIN_ID
   * @return String[] List of Operations
   */
  public String[] GetOperations (int domainID);

  /**
   * Get Operations in a Domain
   * @param domainName String Domain Name
   * @return String[] List of Operations, null if none found
   */
  public String[] GetOperations (String domainName);

  /**
   * Get Operations in a Domain
   * @param domainName String Domain Name
   * @return String[] List of Operations, null if none found
   */
  public String[] GetOperationFullNames (String domainName);

  /**
   * Get Operations Provided List of opNames and wsNames
   * @param opNames String[]
   * @param wsNames String[]
   * @return String[] Operation Full Names list if found, null otherwise
   */
  public String[] GetOperationFullNames (String[] opNames, String[] wsNames);

  /**
   * Get Web Service of Operation
   * @param opName String Operation Name
   * @return String Web Service Name
   */
  public String GetWebService (String opName);

  /**
   * Get Request Parameters for Operation (if exists)
   * @param opID int OperationID
   * @return String[] List of parameters, null if not exist
   */
  public String[] GetParameters (int opID);

  /**
   * Get Request Parameters for Operation (if exists)
   * @param wsName String WebService Name
   * @param opName String Operation Name
   * @return String[] List of parameters, null if not exist
   */
  public String[] GetParameters (String wsName, String opName);

  // WI 21296
  /**
   * Get Request Parameters for Operation (if exists)
   * @param opID int OperationID
   * @return ArrayList List of parameters, null if not exist
   */
  public ArrayList GetWebServiceParameters (int opID);

  /**
   * Get Request Parameters for Operation (if exists)
   * @param wsName String WebService Name
   * @param opName String Operation Name
   * @return String[] List of parameters, null if not exist
   */
  public ArrayList GetWebServiceParameters (String wsName, String opName);

  /**
   * Get Last Time Operation (Task) Ran
   * @param opName String Operation Name
   * @return Calendar Time, null if not found
   */
  public Calendar GetLastStartTime (String opName);

// added by charlie zhang by 2008-6-1
  public Operation GetOperationByIDOrNameNoAccurate (int opID, String opName, String wsName);

  /**
   * Set Status to Running
   * @param opName String Operation Name
   */
  public void StartTask (String opName);

  /**
   * Set Status to Stopped
   * @param opName String Operation Name
   */
  public void StopTask (String opName);

  /**
   * Get Operation
   * @param opID int Operation ID
   * @return Operation
   */
  public Operation GetOperation (int opID);

  /**
   * Get Operation
   * @param opName String Operation Name
   * @param webServiceName String Web Service Name
   * @return Operation
   */
  public Operation GetOperation (String opName, String webServiceName);

  /**
   * Get Active Operation
   * @param opName String
   * @param webServiceName String
   * @return Operation
   */
  public Operation GetActiveOperation (String opName, String webServiceName);

  /**
   * Search Operations
   * @param domain String Domain Name
   * @param name String Operation Name
   * @param type String Operation Type
   * @param method String Web Method (ignored if Type = Scheduled Task)
   * @param status String Status
   * @param version String version
   * @return Operation[] Array of Operations, null if not found
   */
  public Operation[] Search (String domain, String name, String type, String method, String status, String version);

  /**
   * Save Existing Operation
   * @param opID int
   * @param domain String
   * @param webService String
   * @param description String
   * @param type String
   * @param config String
   * @param status String
   * @param message String
   * @return boolean
   */
  public boolean SaveOperation (int opID, String description, String config, String status, String message);

  /**
   * Save New Operation
   * @param opName String
   * @param domain String
   * @param webService String
   * @param description String
   * @param type String
   * @param config String
   * @param status String
   * @param message String
   * @return boolean
   */
  public int SaveOperation (String opName, String domain, String webService, String description, String type, String config,
                            String status, String message);

  /**
   * Checks Unique Name
   * @param opName String
   * @param wsName String
   * @return boolean
   */
  public boolean IsUniqueName (String opName, String wsName);

  /**
   * Checks to see if operation can be made active
   * @param opID int
   * @param opName String
   * @param wsName String
   * @return boolean
   */
  public boolean CanMarkActive (int opID, String opName, String wsName);
  
  /**
   * Get Submit Operation Names
   * @return String[]
   */  
  public String[] GetSubmits ();
  
  /**
   * Get Query Operation Names
   * @return String[]
   */  
  public String[] GetQueries ();

  // WI 21296
  /**
   * Get Query Operation Names
   * @param version String
   * @return String[]
   */  
  public String[] GetQueries (String version);

  /**
   * Get Solicit Operation Names
   * @return String[]
   */
  public String[] GetSolicits ();

  // WI 21296
 /**
   * Get Solicit Operation Names
   * @param version String
   * @return String[]
   */
  public String[] GetSolicits (String version);

  /**
   * Get Execute Operation Names
   * @return String[]
   */
  public String[] GetExecutes();

  // WI 21296
  /**
   * Get Execute Operation Names
   * @param version String
   * @return String[]
   */
  public String[] GetExecutes(String version);

  /**
   * Get Query,Solicit,Execute Operation Names
   * @return String[]
   */
  public int[] GetAllQSEServicesID();

  // WI 21296
   /**
   * Get Query,Solicit,Execute Operation Names
   * @param version String
   * @return String[]
   */ 
  public int[] GetAllQSEServicesID(String version);

  /**
   * Delete Operation
   * @param opID int
   * @return boolean
   */
  public boolean DeleteOperation (int opID);

  /**
   * Get Operations List, sorted by Domains
   * @param domains String[] list of allowable Domains
   * @return Operation[]
   */
  public Operation[] GetOperationsList (String[] domains);

  // WI 22317
  /**
   * Get Operations List, sorted by Domains
   * @param domains String[] list of allowable Domains
   * @return Operation[]
   */
  public Operation[] GetAllOperationsList (String[] domains);
  
  /**
   * Get Operations Manager Document, sorted by Domains
   * @param submitID String submit id
   * @return byte[]
   */
  public ClsNodeDocument GetOperationsManagerDocument (String submitID);

  /**
   * Get Operations List
   * @return Operation[]
   */
  public Hashtable GetAllDataWizardOperationsIDList ();
}
