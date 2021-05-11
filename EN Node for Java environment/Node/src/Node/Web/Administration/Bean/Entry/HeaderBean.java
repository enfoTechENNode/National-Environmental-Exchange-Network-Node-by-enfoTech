package Node.Web.Administration.Bean.Entry;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create HeaderBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class HeaderBean extends BaseBean {
  private String userName = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public HeaderBean() {
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
}
