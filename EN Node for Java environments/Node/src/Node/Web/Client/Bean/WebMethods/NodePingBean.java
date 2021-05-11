package Node.Web.Client.Bean.WebMethods;

import Node.Web.Client.BaseBean;
/**
 * <p>This class create NodePingBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodePingBean extends BaseBean {
  private String string = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public NodePingBean() {
  }

  /**
   * setString
   * @param input
   * @return 
   */
  public void setString (String input)
  {
    this.string = input;
  }
  /**
   * getString
   * @param 
   * @return String
   */
  public String getString ()
  {
    return this.string;
  }
}
