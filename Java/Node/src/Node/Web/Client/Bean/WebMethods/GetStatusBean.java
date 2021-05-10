package Node.Web.Client.Bean.WebMethods;

import Node.Web.Client.BaseBean;
/**
 * <p>This class create GetStatusBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetStatusBean extends BaseBean {
  private String transID = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public GetStatusBean() {
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
}
