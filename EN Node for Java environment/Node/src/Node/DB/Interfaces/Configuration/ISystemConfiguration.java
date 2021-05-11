package Node.DB.Interfaces.Configuration;

import org.apache.log4j.Level;
/**
 * <p>This class create ISystemConfiguration interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface ISystemConfiguration {

  // Save Entire Configuration Setting
  public boolean SaveConfigSettings (String nodeStatus, int tokenLifeTime, String nodeName, String nodeMessage, String naasURL,
                                     String nodeURL, String nodeURLV2, boolean proxyStatus, String proxyHost, String proxyPort, String proxyUID,
                                     String proxyPWD, String nodeAdminName, String nodeAdminUID, String nodeAdminPWD,
                                     Level adminLogLevel, Level clientLogLevel, Level taskLogLevel, Level webServicesLogLevel,
                                     String[] clientURLs);

  // Save Client URLs
	/**
	 * SaveClientURLs.
	 * @param clientURLs.
	 * @return boolean
	 */
  public boolean SaveClientURLs (String[] clientURLs);
	/**
	 * SaveClientURLs_V2.
	 * @param clientURLs.
	 * @return boolean
	 */
  public boolean SaveClientURLs_V2 (String[] clientURLs);

  // Email Server Settings
	/**
	 * GetEmailServerHost.
	 * @param .
	 * @return String
	 */
  public String GetEmailServerHost ();
	/**
	 * GetEmailServerPort.
	 * @param .
	 * @return String
	 */
  public String GetEmailServerPort ();
	/**
	 * SaveEmailSettings.
	 * @param host.
	 * @param port.
	 * @param userSenderName.
	 * @param userEmail.
	 * @param userUID.
	 * @param userPWD.
	 * @param userCCList.
	 * @param userBCCList.
	 * @param taskSenderName.
	 * @param taskEmail.
	 * @param taskUID.
	 * @param taskPWD.
	 * @param taskCCList.
	 * @param taskBCCList.
	 * @return boolean
	 */
  public boolean SaveEmailSettings (String host, String port, String userSenderName, String userEmail, String userUID, String userPWD,
                                    String[] userCCList, String[] userBCCList, String taskSenderName, String taskEmail, String taskUID,
                                    String taskPWD, String[] taskCCList, String[] taskBCCList);
	/**
	 * SaveUserCCList.
	 * @param ccList.
	 * @return boolean
	 */
  public boolean SaveUserCCList (String[] ccList);
	/**
	 * SaveUserBCCList.
	 * @param bccList.
	 * @return boolean
	 */
  public boolean SaveUserBCCList (String[] bccList);

  // Get Status of Node
	/**
	 * GetNodeStatus.
	 * @param .
	 * @return String
	 */
  public String GetNodeStatus ();

  // Get Token Life Time
	/**
	 * GetTokenLifeTime.
	 * @param .
	 * @return int
	 */
  public int GetTokenLifeTime ();

  // Get Node Name
	/**
	 * GetNodeName.
	 * @param .
	 * @return String
	 */
  public String GetNodeName ();

  // Get Node Message
	/**
	 * GetNodeMessage.
	 * @param .
	 * @return String
	 */
  public String GetNodeMessage ();

  // Get NAAS URL
	/**
	 * GetNAASURL.
	 * @param .
	 * @return String
	 */
  public String GetNAASURL ();

  // Get Node URL
	/**
	 * GetNodeURL.
	 * @param .
	 * @return String
	 */
  public String GetNodeURL ();

  // Get Node URL for Version 2.0
	/**
	 * GetNodeURL_V2.
	 * @param .
	 * @return String
	 */
  public String GetNodeURL_V2 ();

  // Get Queries
	/**
	 * GetQueries.
	 * @param .
	 * @return String[]
	 */
  public String[] GetQueries ();

  // Proxy
	/**
	 * GetProxyStatus.
	 * @param .
	 * @return boolean
	 */
  public boolean GetProxyStatus ();
	/**
	 * GetProxyHost.
	 * @param .
	 * @return boolean
	 */
  public String GetProxyHost ();
	/**
	 * GetProxyPort.
	 * @param .
	 * @return String
	 */
  public String GetProxyPort ();
	/**
	 * GetProxyUID.
	 * @param .
	 * @return String
	 */
  public String GetProxyUID ();
	/**
	 * GetProxyPWD.
	 * @param .
	 * @return String
	 */
  public String GetProxyPWD ();

  // FTP
	/**
	 * GetFTPStatus.
	 * @param .
	 * @return boolean
	 */
  public boolean GetFTPStatus ();
	/**
	 * GetFTPHost.
	 * @param .
	 * @return String
	 */
  public String GetFTPHost ();
	/**
	 * GetFTPPort.
	 * @param .
	 * @return String
	 */
  public String GetFTPPort ();
	/**
	 * GetFTPUID.
	 * @param .
	 * @return String
	 */
  public String GetFTPUID ();
	/**
	 * GetFTPPWD.
	 * @param .
	 * @return String
	 */
  public String GetFTPPWD ();

  // Node Administrator
	/**
	 * GetNodeAdminName.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminName ();
	/**
	 * GetNodeAdminUID.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminUID ();
	/**
	 * GetNodeAdminPWD.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminPWD ();

  // Get Logging Level Web Apps
	/**
	 * GetAdministrationLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetAdministrationLogLevel ();
	/**
	 * GetClientLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetClientLogLevel ();
	/**
	 * GetTaskLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetTaskLogLevel ();
	/**
	 * GetWebServicesLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetWebServicesLogLevel ();

  // Get Client Node URLs
	/**
	 * GetClientNodeURLs.
	 * @param .
	 * @return String[]
	 */
  public String[] GetClientNodeURLs ();
  // Get Client Node URLs for Version 2.0
	/**
	 * GetClientNodeURLs_V2.
	 * @param .
	 * @return String[]
	 */
  public String[] GetClientNodeURLs_V2 ();

  // Get Email Server Information
	/**
	 * GetUserEmailSender.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSender ();
	/**
	 * GetUserEmailSenderEmail.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSenderEmail ();
	/**
	 * GetUserEmailUID.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailUID ();
	/**
	 * GetUserEmailPWD.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailPWD ();
	/**
	 * GetUserEmailCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetUserEmailCCList ();
	/**
	 * GetUserEmailBCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetUserEmailBCCList ();
	/**
	 * GetUserEmailSubject.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSubject ();
	/**
	 * GetEmailTemplateLocation.
	 * @param .
	 * @return String
	 */
  public String GetEmailTemplateLocation ();
  
  // WI 33641
	/**
	 * GetRestServiceIntroductionHeader.
	 * @param .
	 * @return String
	 */
  public String GetRestServiceIntroductionHeader ();
	/**
	 * GetRestServiceIntroductionContent.
	 * @param .
	 * @return String
	 */
  public String GetRestServiceIntroductionContent ();
  
  /**
	 * SaveRestServiceIntroduction.
	 * 
	 * @param introduction
	 *            .
	 * @return boolean
	 */
  public boolean SaveRestServiceIntroduction(String[] introduction);

}
