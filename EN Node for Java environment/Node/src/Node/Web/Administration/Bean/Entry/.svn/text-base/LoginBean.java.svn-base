package Node.Web.Administration.Bean.Entry;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Properties;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import Node.Phrase;
import Node.API.PropertyFiles;
import Node.Utils.AppUtils;
import Node.Web.Administration.BaseBean;
/**
 * <p>This class create LoginBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LoginBean extends BaseBean {
	private String loginName = "";
	private String loginPassword = "";
	private String message = "";
	private boolean changePWD = false;
	private String newPWD1 = "";
	private String newPWD2 = "";
	private String version = "";
	private String buildDate = "";
	// WI 33893
	private boolean resetPWD = false;
	private String email = "";
	

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public LoginBean()
	{
		String vers = (String)JNDIAccess.GetJNDIValue(Phrase.jndiNodeVersion, false);
        Properties myProp = PropertyFiles.loadProperties(AppUtils.getAppRoot()+"WEB-INF/ApplicationResources.properties");        
		String buildDate = myProp.getProperty(Phrase.buildDate);

		if (vers != null){
			this.setBuildDate(buildDate);
			this.setVersion(vers);
		}
	}

	  /**
	   * setLoginName
	   * @param name
	   * @return 
	   */
	public void setLoginName (String name)
	{
		this.loginName = name;
	}
	  /**
	   * getLoginName
	   * @param 
	   * @return String
	   */
	public String getLoginName ()
	{
		return this.loginName;
	}

	  /**
	   * setLoginPassword
	   * @param password
	   * @return 
	   */
	public void setLoginPassword (String password)
	{
		this.loginPassword = password;
	}
	  /**
	   * getLoginPassword
	   * @param 
	   * @return String
	   */
	public String getLoginPassword ()
	{
		return this.loginPassword;
	}

	  /**
	   * setMessage
	   * @param message
	   * @return 
	   */
	public void setMessage (String message)
	{
		this.message = message;
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

	  /**
	   * setChangePWD
	   * @param input
	   * @return 
	   */
	public void setChangePWD (boolean input)
	{
		this.changePWD = input;
	}
	  /**
	   * getChangePWD
	   * @param 
	   * @return String
	   */
	public boolean getChangePWD ()
	{
		return this.changePWD;
	}

	  /**
	   * setNewPWD1
	   * @param input
	   * @return 
	   */
	public void setNewPWD1 (String input)
	{
		this.newPWD1 = input;
	}
	  /**
	   * getNewPWD1
	   * @param 
	   * @return String
	   */
	public String getNewPWD1 ()
	{
		return this.newPWD1;
	}

	  /**
	   * setNewPWD2
	   * @param input
	   * @return 
	   */
	public void setNewPWD2 (String input)
	{
		this.newPWD2 = input;
	}
	  /**
	   * getNewPWD2
	   * @param 
	   * @return String
	   */
	public String getNewPWD2 ()
	{
		return this.newPWD2;
	}

	  /**
	   * setVersion
	   * @param input
	   * @return 
	   */
	public void setVersion (String input)
	{
		String buildVer = "";
		Properties myProp = PropertyFiles.loadProperties(AppUtils.getAppRoot()+"WEB-INF/ApplicationResources.properties");

		String buildDate = myProp.getProperty(Phrase.buildDate);
		try
		{
			SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
			Date release = dateFormat.parse(buildDate);
			Date start = dateFormat.parse("2000-01-01 00:00");
			long diff = release.getTime() - start.getTime();
			buildVer = "." + diff / (1000 * 60 * 60 * 24);
		}
		catch (Exception ex)
		{
			ex.printStackTrace();
		}
		this.version = input+buildVer;
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

	  /**
	   * getBuildDate
	   * @param 
	   * @return String
	   */
	public String getBuildDate() {
		return buildDate;
	}

	  /**
	   * setBuildDate
	   * @param buildDate
	   * @return 
	   */
	public void setBuildDate(String buildDate) {
		this.buildDate = buildDate;
	}

	/**
	 * @return the resetPWD
	 */
	public boolean isResetPWD() {
		return resetPWD;
	}

	/**
	 * @param resetPWD the resetPWD to set
	 */
	public void setResetPWD(boolean resetPWD) {
		this.resetPWD = resetPWD;
	}

	/**
	 * @return the email
	 */
	public String getEmail() {
		return email;
	}

	/**
	 * @param email the email to set
	 */
	public void setEmail(String email) {
		this.email = email;
	}
	
}
