package Node.Web.Client;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create BaseBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class BaseBean extends ActionForm {
  private String nodeAddress1 = "";
  private String nodeAddress2 = "";
  private String nodeAddress3 = "";
  private String nodeAddress4 = "";
  private String token = "";
  private String result = "";
  private String error = "";
  private String[] clientURLs = null;
  private String[] clientURLs2 = null;

  /**
   * Constructor
   * @param 
   * @return 
   */
  public BaseBean() {
  }

  /**
   * setNodeAddress1
   * @param input
   * @return 
   */
  public void setNodeAddress1 (String input)
  {
    this.nodeAddress1 = input;
  }
  /**
   * getNodeAddress1
   * @param 
   * @return String
   */
  public String getNodeAddress1 ()
  {
    return this.nodeAddress1;
  }

  /**
   * setNodeAddress2
   * @param input
   * @return 
   */
  public void setNodeAddress2 (String input)
  {
    this.nodeAddress2 = input;
  }
  /**
   * getNodeAddress2
   * @param 
   * @return String
   */
  public String getNodeAddress2 ()
  {
    return this.nodeAddress2;
  }

  /**
   * getNodeAddress3
   * @param 
   * @return String
   */
  public String getNodeAddress3() {
	return nodeAddress3;
}

  /**
   * setNodeAddress3
   * @param nodeAddress3
   * @return 
   */
public void setNodeAddress3(String nodeAddress3) {
	this.nodeAddress3 = nodeAddress3;
}

/**
 * getNodeAddress4
 * @param 
 * @return String
 */
public String getNodeAddress4() {
	return nodeAddress4;
}
/**
 * setNodeAddress4
 * @param nodeAddress4
 * @return 
 */

public void setNodeAddress4(String nodeAddress4) {
	this.nodeAddress4 = nodeAddress4;
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
  public String getToken ()
  {
    return this.token;
  }

  /**
   * setResult
   * @param input
   * @return 
   */
  public void setResult (String input)
  {
    this.result = input;
  }
  /**
   * getResult
   * @param 
   * @return String
   */
  public String getResult ()
  {
    return this.result;
  }

  /**
   * setError
   * @param input
   * @return 
   */
  public void setError (String input)
  {
    this.error = input;
  }
  /**
   * getError
   * @param 
   * @return String
   */
  public String getError ()
  {
    return this.error;
  }
/*
  public void setClientURLs (String[] input)
  {
    this.clientURLs = input;
  }*/
  public String[] getClientURLs ()
  {
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.ClientLoggerName);
      this.clientURLs = configDB.GetClientNodeURLs();
    } catch (Exception e) {
      LoggingUtils.Log("Could Not get Client URLs: " + e.toString(), Level.ERROR, Phrase.ClientLoggerName);
    }
    return this.clientURLs;
  }
  /**
   * getClientURLs_V2
   * @param 
   * @return String[]
   */
  public String[] getClientURLs_V2 ()
  {
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.ClientLoggerName);
      this.clientURLs2 = configDB.GetClientNodeURLs_V2();
    } catch (Exception e) {
      LoggingUtils.Log("Could Not get Client URLs2: " + e.toString(), Level.ERROR, Phrase.ClientLoggerName);
    }
    return this.clientURLs2;
  }
}
