package Node.Web.Client.Bean.WebMethods;

import Node.Web.Client.BaseBean;
/**
 * <p>This class create AuthenticateBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AuthenticateBean extends BaseBean {
  private String user = "";
  private String password = "";
  private String authMethod = "";
  private String domainName = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public AuthenticateBean() {
  }

  /**
   * setUser
   * @param input
   * @return 
   */
  public void setUser (String input)
  {
    this.user = input;
  }
  /**
   * getUser
   * @param 
   * @return String
   */
  public String getUser ()
  {
    return this.user;
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
   * setAuthMethod
   * @param input
   * @return 
   */
  public void setAuthMethod (String input)
  {
    this.authMethod = input;
  }
  /**
   * getAuthMethod
   * @param 
   * @return String
   */
  public String getAuthMethod ()
  {
    if (this.authMethod == null || this.authMethod.equals(""))
      this.authMethod = "PASSWORD";
    return this.authMethod;
  }

  /**
   * getDomainName
   * @param 
   * @return String
   */
	public String getDomainName() {
		return domainName;
	}

/**
 * setDomainName
 * @param domainName
 * @return 
 */
	public void setDomainName(String domainName) {
		this.domainName = domainName;
	}
}
