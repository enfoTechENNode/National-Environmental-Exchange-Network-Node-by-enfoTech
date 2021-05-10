package Node.Web.Administration.Bean.Entry;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import Node.Phrase;
import Node.Web.Administration.BaseBean;
/**
 * <p>This class create LeftBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LeftBean extends BaseBean {
  private boolean isNodeAdmin = false;
  private boolean isNJNEIAdmin = false;
  private String version = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public LeftBean()
  {
    String vers = (String)JNDIAccess.GetJNDIValue(Phrase.jndiNodeVersion, false);
    if (vers != null)
      this.setVersion(vers);
  }

  /**
   * setIsNodeAdmin
   * @param input
   * @return 
   */
  public void setIsNodeAdmin (boolean input)
  {
    this.isNodeAdmin = input;
  }
  /**
   * getIsNodeAdmin
   * @param 
   * @return boolean
   */
  public boolean getIsNodeAdmin ()
  {
    return this.isNodeAdmin;
  }

  /**
   * setIsNJNEIAdmin
   * @param input
   * @return 
   */
  public void setIsNJNEIAdmin (boolean input)
  {
    this.isNJNEIAdmin = input;
  }
  /**
   * getIsNJNEIAdmin
   * @param 
   * @return boolean
   */
  public boolean getIsNJNEIAdmin ()
  {
    return this.isNJNEIAdmin;
  }

  /**
   * setVersion
   * @param input
   * @return 
   */
  public void setVersion (String input)
  {
    this.version = input;
  }
  /**
   * getVersion
   * @param 
   * @return String
   */
  public String getVersion ()
  {
    return this.version;
  }
}
