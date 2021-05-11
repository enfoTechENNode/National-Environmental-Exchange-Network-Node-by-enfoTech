package Node.Web.Administration.Bean.Configurations;

import javax.servlet.http.HttpServletRequest;
import Node.Web.Administration.BaseBean;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;
/**
 * <p>This class create ConfigurationsBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ConfigurationsBean extends BaseBean {
	private int selectedIndex = 0;

	private String tokenLifeTime = "";
	private String naasAddress = "";
	private String naasAddressError = "";
	private String nodeAddress = "";
	private String nodeAddressError = "";
	private String nodeAddressV2 = "";
	private String nodeAddressErrorV2 = "";
	private String nodeName = "";
	private String nodeNameError = "";
	private String hasProxy = "false";
	private String proxyServer = "";
	private String proxyServerError = "";
	private String proxyPort = "";
	private String proxyUser = "";
	private String proxyPassword = "";
	private String message = "";
	private String nodeRunning = "";
	private String nodeRunningError = "";
	private String nodeMessage = "";
	private String nodeAdminName = "";
	private String nodeAdminUID = "";
	private String nodeAdminUIDError = "";
	private String nodeAdminPWD = "";
	private String nodeAdminPWDError = "";
	private String nodeAdminLog = "";
	private String nodeClientLog = "";
	private String nodeTaskLog = "";
	private String nodeWebServicesLog = "";
	private String[] clientURLs = new String[0];
	private String[] clientURLs2 = new String[0];

	private String host = "";
	private String hostError = "";
	private String port = "";
	private String userSenderName = "";
	private String userSenderNameError = "";
	private String userEmailAddress = "";
	private String userEmailAddressError = "";
	private String userUID = "";
	private String userPWD = "";
	private String[] userCC = new String[0];
	private String[] userBCC = new String[0];
	private String taskSenderName = "";
	private String taskSenderNameError = "";
	private String taskEmailAddress = "";
	private String taskEmailAddressError = "";
	private String taskUID = "";
	private String taskPWD = "";
	private String[] taskCC = new String[0];
	private String[] taskBCC = new String[0];

	private FormFile uploadFile = null;


	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public ConfigurationsBean() {
	}

	  /**
	   * reset
	   * @param mapping
	   * @param request
	   * @return 
	   */
	public void reset (ActionMapping mapping, HttpServletRequest request)
	{
		if (request.getParameter("hasProxy") == null)
			this.hasProxy = "";
	}

	  /**
	   * setSelectedIndex
	   * @param input
	   * @return 
	   */
	public void setSelectedIndex (int input)
	{
		this.selectedIndex = input;
	}
	  /**
	   * getSelectedIndex
	   * @param 
	   * @return String
	   */
	public int getSelectedIndex ()
	{
		return this.selectedIndex;
	}

	  /**
	   * setTokenLifeTime
	   * @param tokenLifeTime
	   * @return 
	   */
	public void setTokenLifeTime (String tokenLifeTime)
	{
		this.tokenLifeTime = tokenLifeTime;
	}
	  /**
	   * getTokenLifeTime
	   * @param 
	   * @return String
	   */
	public String getTokenLifeTime ()
	{
		return this.tokenLifeTime;
	}

	  /**
	   * setNaasAddress
	   * @param naasAddress
	   * @return 
	   */
	public void setNaasAddress (String naasAddress)
	{
		this.naasAddress = naasAddress;
	}
	  /**
	   * getNaasAddress
	   * @param 
	   * @return String
	   */
	public String getNaasAddress ()
	{
		return this.naasAddress;
	}

	  /**
	   * setNaasAddressError
	   * @param naasAddress
	   * @return 
	   */
	public void setNaasAddressError (String naasAddress)
	{
		this.naasAddressError = naasAddress;
	}
	  /**
	   * getNaasAddressError
	   * @param 
	   * @return String
	   */
	public String getNaasAddressError ()
	{
		return this.naasAddressError;
	}

	  /**
	   * setNodeAddress
	   * @param nodeAddress
	   * @return 
	   */
	public void setNodeAddress (String nodeAddress)
	{
		this.nodeAddress = nodeAddress;
	}
	  /**
	   * getNodeAddress
	   * @param 
	   * @return String
	   */
	public String getNodeAddress ()
	{
		return this.nodeAddress;
	}

	  /**
	   * setNodeAddressError
	   * @param nodeAddress
	   * @return 
	   */
	public void setNodeAddressError (String nodeAddress)
	{
		this.nodeAddressError = nodeAddress;
	}
	  /**
	   * getNodeAddressError
	   * @param 
	   * @return String
	   */
	public String getNodeAddressError ()
	{
		return this.nodeAddressError;
	}

	  /**
	   * setNodeName
	   * @param nodeName
	   * @return 
	   */
	public void setNodeName (String nodeName)
	{
		this.nodeName = nodeName;
	}
	  /**
	   * getNodeName
	   * @param 
	   * @return String
	   */
	public String getNodeName ()
	{
		return this.nodeName;
	}

	  /**
	   * setNodeNameError
	   * @param nodeName
	   * @return 
	   */
	public void setNodeNameError (String nodeName)
	{
		this.nodeNameError = nodeName;
	}
	  /**
	   * getNodeNameError
	   * @param 
	   * @return String
	   */
	public String getNodeNameError ()
	{
		return this.nodeNameError;
	}

	  /**
	   * setHasProxy
	   * @param hasProxy
	   * @return 
	   */
	public void setHasProxy (String hasProxy)
	{
		this.hasProxy = hasProxy;
	}
	  /**
	   * getHasProxy
	   * @param 
	   * @return String
	   */
	public String getHasProxy ()
	{
		return this.hasProxy;
	}

	  /**
	   * setProxyServer
	   * @param proxyServer
	   * @return 
	   */
	public void setProxyServer (String proxyServer)
	{
		this.proxyServer = proxyServer;
	}
	  /**
	   * getProxyServer
	   * @param 
	   * @return String
	   */
	public String getProxyServer ()
	{
		return this.proxyServer;
	}

	  /**
	   * setProxyServerError
	   * @param proxyServer
	   * @return 
	   */
	public void setProxyServerError (String proxyServer)
	{
		this.proxyServerError = proxyServer;
	}
	  /**
	   * getProxyServerError
	   * @param 
	   * @return String
	   */
	public String getProxyServerError ()
	{
		return this.proxyServerError;
	}

	  /**
	   * setProxyPort
	   * @param proxyPort
	   * @return 
	   */
	public void setProxyPort (String proxyPort)
	{
		this.proxyPort = proxyPort;
	}
	  /**
	   * getProxyPort
	   * @param 
	   * @return String
	   */
	public String getProxyPort ()
	{
		return this.proxyPort;
	}

	  /**
	   * setProxyUser
	   * @param proxyUser
	   * @return 
	   */
	public void setProxyUser (String proxyUser)
	{
		this.proxyUser = proxyUser;
	}
	  /**
	   * getProxyUser
	   * @param 
	   * @return String
	   */
	public String getProxyUser ()
	{
		return this.proxyUser;
	}

	  /**
	   * setProxyPassword
	   * @param proxyPassword
	   * @return 
	   */
	public void setProxyPassword (String proxyPassword)
	{
		this.proxyPassword = proxyPassword;
	}
	  /**
	   * getProxyPassword
	   * @param 
	   * @return String
	   */
	public String getProxyPassword ()
	{
		return this.proxyPassword;
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
	   * setNodeRunning
	   * @param running
	   * @return 
	   */
	public void setNodeRunning (String running)
	{
		this.nodeRunning = running;
	}
	  /**
	   * getNodeRunning
	   * @param 
	   * @return String
	   */
	public String getNodeRunning ()
	{
		return this.nodeRunning;
	}

	  /**
	   * setNodeRunningError
	   * @param running
	   * @return 
	   */
	public void setNodeRunningError (String running)
	{
		this.nodeRunningError = running;
	}
	  /**
	   * getNodeRunningError
	   * @param 
	   * @return String
	   */
	public String getNodeRunningError ()
	{
		return this.nodeRunningError;
	}

	  /**
	   * setNodeMessage
	   * @param mesg
	   * @return 
	   */
	public void setNodeMessage (String mesg)
	{
		this.nodeMessage = mesg;
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
	   * setNodeAdminName
	   * @param name
	   * @return 
	   */
	public void setNodeAdminName (String name)
	{
		this.nodeAdminName = name;
	}
	  /**
	   * getNodeAdminName
	   * @param 
	   * @return String
	   */
	public String getNodeAdminName ()
	{
		return this.nodeAdminName;
	}

	  /**
	   * setNodeAdminUID
	   * @param uid
	   * @return 
	   */
	public void setNodeAdminUID (String uid)
	{
		this.nodeAdminUID = uid;
	}
	  /**
	   * getNodeAdminUID
	   * @param 
	   * @return String
	   */
	public String getNodeAdminUID ()
	{
		return this.nodeAdminUID;
	}

	  /**
	   * setNodeAdminUIDError
	   * @param uid
	   * @return 
	   */
	public void setNodeAdminUIDError (String uid)
	{
		this.nodeAdminUIDError = uid;
	}
	  /**
	   * getNodeAdminUIDError
	   * @param 
	   * @return String
	   */
	public String getNodeAdminUIDError ()
	{
		return this.nodeAdminUIDError;
	}

	  /**
	   * setNodeAdminPWD
	   * @param pwd
	   * @return 
	   */
	public void setNodeAdminPWD (String pwd)
	{
		this.nodeAdminPWD = pwd;
	}
	  /**
	   * getNodeAdminPWD
	   * @param 
	   * @return String
	   */
	public String getNodeAdminPWD ()
	{
		return this.nodeAdminPWD;
	}

	  /**
	   * setNodeAdminPWDError
	   * @param pwd
	   * @return 
	   */
	public void setNodeAdminPWDError (String pwd)
	{
		this.nodeAdminPWDError = pwd;
	}
	  /**
	   * getNodeAdminPWDError
	   * @param 
	   * @return String
	   */
	public String getNodeAdminPWDError ()
	{
		return this.nodeAdminPWDError;
	}

	  /**
	   * setNodeAdminLog
	   * @param level
	   * @return 
	   */
	public void setNodeAdminLog (String level)
	{
		this.nodeAdminLog = level;
	}
	  /**
	   * getNodeAdminLog
	   * @param 
	   * @return String
	   */
	public String getNodeAdminLog ()
	{
		return this.nodeAdminLog;
	}

	  /**
	   * setNodeClientLog
	   * @param level
	   * @return 
	   */
	public void setNodeClientLog (String level)
	{
		this.nodeClientLog = level;
	}
	  /**
	   * getNodeClientLog
	   * @param 
	   * @return String
	   */
	public String getNodeClientLog ()
	{
		return this.nodeClientLog;
	}

	  /**
	   * setNodeTaskLog
	   * @param level
	   * @return 
	   */
	public void setNodeTaskLog (String level)
	{
		this.nodeTaskLog = level;
	}
	  /**
	   * getNodeTaskLog
	   * @param 
	   * @return String
	   */
	public String getNodeTaskLog ()
	{
		return this.nodeTaskLog;
	}

	  /**
	   * setNodeWebServicesLog
	   * @param level
	   * @return 
	   */
	public void setNodeWebServicesLog (String level)
	{
		this.nodeWebServicesLog = level;
	}
	  /**
	   * getNodeWebServicesLog
	   * @param 
	   * @return String
	   */
	public String getNodeWebServicesLog ()
	{
		return this.nodeWebServicesLog;
	}

	  /**
	   * setClientURLs
	   * @param input
	   * @return 
	   */
	public void setClientURLs (String[] input)
	{
		if (input != null)
			this.clientURLs = input;
		else
			this.clientURLs = new String[0];
	}
	  /**
	   * getClientURLs
	   * @param 
	   * @return String[]
	   */
	public String[] getClientURLs ()
	{
		return this.clientURLs;
	}

	  /**
	   * setClientURLs2
	   * @param input
	   * @return 
	   */
	public void setClientURLs2 (String[] input)
	{
		if (input != null)
			this.clientURLs2 = input;
		else
			this.clientURLs2 = new String[0];
	}
	  /**
	   * getClientURLs2
	   * @param 
	   * @return String[]
	   */
	public String[] getClientURLs2 ()
	{
		return this.clientURLs2;
	}

	  /**
	   * setHost
	   * @param input
	   * @return 
	   */
	public void setHost (String input)
	{
		this.host = input;
	}
	  /**
	   * getHost
	   * @param 
	   * @return String
	   */
	public String getHost ()
	{
		return this.host;
	}

	  /**
	   * setHostError
	   * @param input
	   * @return 
	   */
	public void setHostError (String input)
	{
		this.hostError = input;
	}
	  /**
	   * getHostError
	   * @param 
	   * @return String
	   */
	public String getHostError ()
	{
		return this.hostError;
	}

	  /**
	   * setPort
	   * @param input
	   * @return 
	   */
	public void setPort (String input)
	{
		this.port = input;
	}
	  /**
	   * getPort
	   * @param 
	   * @return String
	   */
	public String getPort ()
	{
		return this.port;
	}

	  /**
	   * setUserSenderName
	   * @param input
	   * @return 
	   */
	public void setUserSenderName (String input)
	{
		this.userSenderName = input;
	}
	  /**
	   * getUserSenderName
	   * @param 
	   * @return String
	   */
	public String getUserSenderName ()
	{
		return this.userSenderName;
	}

	  /**
	   * setUserSenderNameError
	   * @param input
	   * @return 
	   */
	public void setUserSenderNameError (String input)
	{
		this.userSenderNameError = input;
	}
	  /**
	   * getUserSenderNameError
	   * @param 
	   * @return String
	   */
	public String getUserSenderNameError ()
	{
		return this.userSenderNameError;
	}

	  /**
	   * setUserEmailAddress
	   * @param input
	   * @return 
	   */
	public void setUserEmailAddress (String input)
	{
		this.userEmailAddress = input;
	}
	  /**
	   * getUserEmailAddress
	   * @param 
	   * @return String
	   */
	public String getUserEmailAddress ()
	{
		return this.userEmailAddress;
	}

	  /**
	   * setUserEmailAddressError
	   * @param input
	   * @return 
	   */
	public void setUserEmailAddressError (String input)
	{
		this.userEmailAddressError = input;
	}
	  /**
	   * getUserEmailAddressError
	   * @param 
	   * @return String
	   */
	public String getUserEmailAddressError ()
	{
		return this.userEmailAddressError;
	}

	  /**
	   * setUserUID
	   * @param input
	   * @return 
	   */
	public void setUserUID (String input)
	{
		this.userUID = input;
	}
	  /**
	   * getUserUID
	   * @param 
	   * @return String
	   */
	public String getUserUID ()
	{
		return this.userUID;
	}

	  /**
	   * setUserPWD
	   * @param input
	   * @return 
	   */
	public void setUserPWD (String input)
	{
		this.userPWD = input;
	}
	  /**
	   * getUserPWD
	   * @param 
	   * @return String
	   */
	public String getUserPWD ()
	{
		return this.userPWD;
	}

	  /**
	   * setTaskSenderName
	   * @param input
	   * @return 
	   */
	public void setTaskSenderName (String input)
	{
		this.taskSenderName = input;
	}
	  /**
	   * getTaskSenderName
	   * @param 
	   * @return String
	   */
	public String getTaskSenderName ()
	{
		return this.taskSenderName;
	}

	  /**
	   * setTaskSenderNameError
	   * @param input
	   * @return 
	   */
	public void setTaskSenderNameError (String input)
	{
		this.taskSenderNameError = input;
	}
	  /**
	   * getTaskSenderNameError
	   * @param 
	   * @return String
	   */
	public String getTaskSenderNameError ()
	{
		return this.taskSenderNameError;
	}

	  /**
	   * setTaskEmailAddress
	   * @param input
	   * @return 
	   */
	public void setTaskEmailAddress (String input)
	{
		this.taskEmailAddress = input;
	}
	  /**
	   * getTaskEmailAddress
	   * @param 
	   * @return String
	   */
	public String getTaskEmailAddress ()
	{
		return this.taskEmailAddress;
	}

	  /**
	   * setTaskEmailAddressError
	   * @param input
	   * @return 
	   */
	public void setTaskEmailAddressError (String input)
	{
		this.taskEmailAddressError = input;
	}
	  /**
	   * getTaskEmailAddressError
	   * @param 
	   * @return String
	   */
	public String getTaskEmailAddressError ()
	{
		return this.taskEmailAddressError;
	}

	  /**
	   * setTaskUID
	   * @param input
	   * @return 
	   */
	public void setTaskUID (String input)
	{
		this.taskUID = input;
	}
	  /**
	   * getTaskUID
	   * @param 
	   * @return String
	   */
	public String getTaskUID ()
	{
		return this.taskUID;
	}

	  /**
	   * setTaskPWD
	   * @param input
	   * @return 
	   */
	public void setTaskPWD (String input)
	{
		this.taskPWD = input;
	}
	  /**
	   * getTaskPWD
	   * @param 
	   * @return String
	   */
	public String getTaskPWD ()
	{
		return this.taskPWD;
	}

	  /**
	   * setUserCC
	   * @param input
	   * @return 
	   */
	public void setUserCC (String[] input)
	{
		if (input != null)
			this.userCC = input;
		else
			this.userCC = new String[0];
	}
	  /**
	   * getUserCC
	   * @param 
	   * @return String[]
	   */
	public String[] getUserCC ()
	{
		return this.userCC;
	}

	  /**
	   * setUserBCC
	   * @param input
	   * @return 
	   */
	public void setUserBCC (String[] input)
	{
		if (input != null)
			this.userBCC = input;
		else
			this.userBCC = new String[0];
	}
	  /**
	   * getUserBCC
	   * @param 
	   * @return String[]
	   */
	public String[] getUserBCC ()
	{
		return this.userBCC;
	}

	  /**
	   * setTaskCC
	   * @param input
	   * @return 
	   */
	public void setTaskCC (String[] input)
	{
		if (input != null)
			this.taskCC = input;
		else
			this.taskCC = new String[0];
	}
	  /**
	   * getTaskCC
	   * @param 
	   * @return String[]
	   */
	public String[] getTaskCC ()
	{
		return this.taskCC;
	}

	  /**
	   * setTaskBCC
	   * @param input
	   * @return 
	   */
	public void setTaskBCC (String[] input)
	{
		if (input != null)
			this.taskBCC = input;
		else
			this.taskBCC = new String[0];
	}
	  /**
	   * getTaskBCC
	   * @param 
	   * @return String[]
	   */
	public String[] getTaskBCC ()
	{
		return this.taskBCC;
	}

	  /**
	   * getNodeAddressV2
	   * @param 
	   * @return String
	   */
	public String getNodeAddressV2() {
		return nodeAddressV2;
	}

	  /**
	   * setNodeAddressV2
	   * @param nodeAddressV2
	   * @return 
	   */
	public void setNodeAddressV2(String nodeAddressV2) {
		this.nodeAddressV2 = nodeAddressV2;
	}

	  /**
	   * getNodeAddressErrorV2
	   * @param 
	   * @return String
	   */
	public String getNodeAddressErrorV2() {
		return nodeAddressErrorV2;
	}

	  /**
	   * setNodeAddressErrorV2
	   * @param nodeAddressErrorV2
	   * @return 
	   */
	public void setNodeAddressErrorV2(String nodeAddressErrorV2) {
		this.nodeAddressErrorV2 = nodeAddressErrorV2;
	}

	  /**
	   * getUploadFile
	   * @param 
	   * @return FormFile
	   */
	public FormFile getUploadFile() {
		return uploadFile;
	}

	  /**
	   * setUploadFile
	   * @param uploadFile
	   * @return 
	   */
	public void setUploadFile(FormFile uploadFile) {
		this.uploadFile = uploadFile;
	}

}
