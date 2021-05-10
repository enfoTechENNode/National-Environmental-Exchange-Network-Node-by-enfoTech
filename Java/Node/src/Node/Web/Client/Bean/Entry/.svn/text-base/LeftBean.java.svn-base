package Node.Web.Client.Bean.Entry;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import Node.Phrase;
import Node.Web.Client.BaseBean;
/**
 * <p>This class create LeftBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LeftBean extends BaseBean {
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
