package Node.Web.Administration.Bean.NodeMonitoring;

import Node.Biz.Administration.OperationLog;
import Node.Web.Administration.BaseBean;
/**
 * <p>This class create NodeMonitoringBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeMonitoringBean extends BaseBean {
  private String opName = "";
  private String[] opNameList = null;
  private String opType = "";
  private String webService = "";
  private String status = "";
  private String[] statusList = null;
  private String domain = "";
  private String[] domainList = null;
  private String userID = "";
  private String token = "";
  private String transID = "";
  private String start = "";
  private String end = "";
  private OperationLog[] searchedLogs = null;

  /**
   * Constructor
   * @param 
   * @return 
   */
  public NodeMonitoringBean() {
  }

  /**
   * setOpName
   * @param input
   * @return 
   */
  public void setOpName (String input)
  {
    this.opName = input;
  }
  /**
   * getOpName
   * @param 
   * @return String
   */
  public String getOpName ()
  {
    return this.opName;
  }

  /**
   * setOpNameList
   * @param input
   * @return 
   */
  public void setOpNameList (String[] input)
  {
    this.opNameList = input;
  }
  /**
   * getOpNameList
   * @param 
   * @return String[]
   */
  public String[] getOpNameList ()
  {
    return this.opNameList;
  }

  /**
   * setOpType
   * @param input
   * @return 
   */
  public void setOpType (String input)
  {
    this.opType = input;
  }
  /**
   * getOpType
   * @param 
   * @return String
   */
  public String getOpType ()
  {
    return this.opType;
  }

  /**
   * setWebService
   * @param input
   * @return 
   */
  public void setWebService (String input)
  {
    this.webService = input;
  }
  /**
   * getWebService
   * @param 
   * @return String
   */
  public String getWebService ()
  {
    return this.webService;
  }

  /**
   * setStatus
   * @param input
   * @return 
   */
  public void setStatus (String input)
  {
    this.status = input;
  }
  /**
   * getStatus
   * @param 
   * @return String
   */
  public String getStatus ()
  {
    return this.status;
  }

  /**
   * setStatusList
   * @param input
   * @return 
   */
  public void setStatusList (String[] input)
  {
    this.statusList = input;
  }
  /**
   * getStatusList
   * @param 
   * @return String[]
   */
  public String[] getStatusList ()
  {
    return this.statusList;
  }

  /**
   * setDomain
   * @param input
   * @return 
   */
  public void setDomain (String input)
  {
    this.domain = input;
  }
  /**
   * getDomain
   * @param 
   * @return String
   */
  public String getDomain ()
  {
    return this.domain;
  }

  /**
   * setDomainList
   * @param input
   * @return 
   */
  public void setDomainList (String[] input)
  {
    this.domainList = input;
  }
  /**
   * getDomainList
   * @param 
   * @return String[]
   */
  public String[] getDomainList ()
  {
    return this.domainList;
  }

  /**
   * setUserID
   * @param input
   * @return 
   */
  public void setUserID (String input)
  {
    this.userID = input;
  }
  /**
   * getUserID
   * @param 
   * @return String
   */
  public String getUserID ()
  {
    return this.userID;
  }

  /**
   * setToken
   * @param input
   * @return 
   */
  public void setToken (String input)
  {
    this.token = input;
  }
  /**
   * getToken
   * @param 
   * @return String
   */
  public String getToken ()
  {
    return this.token;
  }

  /**
   * setTransID
   * @param input
   * @return 
   */
  public void setTransID (String input)
  {
    this.transID = input;
  }
  /**
   * getTransID
   * @param 
   * @return String
   */
  public String getTransID ()
  {
    return this.transID;
  }

  /**
   * setStart
   * @param input
   * @return 
   */
  public void setStart (String input)
  {
    this.start = input;
  }
  /**
   * getStart
   * @param 
   * @return String
   */
  public String getStart ()
  {
    return this.start;
  }

  /**
   * setEnd
   * @param input
   * @return 
   */
  public void setEnd (String input)
  {
    this.end = input;
  }
  /**
   * getEnd
   * @param 
   * @return String
   */
  public String getEnd ()
  {
    return this.end;
  }

  /**
   * setSearchedLogs
   * @param input
   * @return 
   */
  public void setSearchedLogs (OperationLog[] input)
  {
    this.searchedLogs = input;
  }
  /**
   * getSearchedLogs
   * @param 
   * @return OperationLog[]
   */
  public OperationLog[] getSearchedLogs ()
  {
    return this.searchedLogs;
  }
}
