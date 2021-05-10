package Node.Web.WebServices.Bean;

import Node.Web.WebServices.BaseBean;
/**
 * <p>This class create EntryBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class EntryBean extends BaseBean {
  private String nodeStatus = "";
  private String nodeMessage = "";
  private String userName = "";
  private String password = "";
  private String message = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public EntryBean() {
  }

  /**
   * setDomainName
   * @param input
   * @return 
   */
  public void setNodeStatus (String input)
  {
    this.nodeStatus = input;
  }
  public String getNodeStatus ()
  {
    return this.nodeStatus;
  }

  /**
   * setNodeMessage
   * @param input
   * @return 
   */
  public void setNodeMessage (String input)
  {
    this.nodeMessage = input;
  }
  /**
   * getNodeMessage
   * @param 
   * @return String
   */
  public String getNodeMessage ()
  {
    return this.nodeMessage;
  }

  /**
   * setUserName
   * @param input
   * @return 
   */
  public void setUserName (String input)
  {
    this.userName = input;
  }
  /**
   * getUserName
   * @param 
   * @return String
   */
  public String getUserName ()
  {
    return this.userName;
  }

  /**
   * setPassword
   * @param input
   * @return 
   */
  public void setPassword (String input)
  {
    this.password = input;
  }
  /**
   * getPassword
   * @param 
   * @return String
   */
  public String getPassword ()
  {
    return this.password;
  }

  /**
   * setMessage
   * @param input
   * @return 
   */
  public void setMessage (String input)
  {
    this.message = input;
  }
  /**
   * getMessage
   * @param 
   * @return String
   */
  public String getMessage ()
  {
    return this.message;
  }
}
