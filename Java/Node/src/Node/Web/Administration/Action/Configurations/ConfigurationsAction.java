package Node.Web.Administration.Action.Configurations;

import java.io.PrintWriter;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;

import Node.Phrase;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.Utils.LoggingUtils;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Configurations.ConfigurationsBean;
/**
 * <p>This class create ConfigurationsAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ConfigurationsAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return
	   */
	public ConfigurationsAction() {
	}

	  /**
	   * formExecute
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) throws Exception
	{
		HttpSession session = request.getSession();
		this.Log("Executing Configurations.do",Level.INFO);
		ConfigurationsBean bean = (ConfigurationsBean)form;
		String act = request.getParameter("act");
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		boolean isConfigurations = false;
		boolean isClient = false;
		boolean isUpdated = true;
		FormFile file = null;
		byte[] fileByte = null;
	    boolean isSaved = false;
	    bean.setMessage("");

	    if(user.IsNodeAdmin()){
		    IConfigurationMgr configMgr = DBManager.GetConfigurationMgr(Phrase.AdministrationLoggerName);
			if (act != null && !act.equals("")) {
				this.Log("Configurations.do act = " + act,Level.DEBUG);
				if (act.equalsIgnoreCase("SAVE_CONFIG")) {
					isUpdated = this.SaveConfiguration(bean, request);
					isConfigurations = true;
				}
				if (act.equalsIgnoreCase("SELECT_CONFIG") || isConfigurations) {
					if (isUpdated)
						this.SetConfigurationsWebPage(bean);
					bean.setSelectedIndex(0);
					isConfigurations = true;
				}
				if (act.equalsIgnoreCase("REMOVE_CLIENT_URL")) {
					isUpdated = this.RemoveSelectedClientURLs(bean, request);
					isClient = true;
				}
				if (act.equalsIgnoreCase("SAVE_CLIENT")) {
					isUpdated = this.SaveClient(bean, request);
					isClient = true;
				}
				if (act.equalsIgnoreCase("SELECT_CLIENT") || isClient) {
					if (isUpdated)
						this.SetClientWebPage(bean);
					bean.setSelectedIndex(2);
					isClient = true;
				}
				if (act.equalsIgnoreCase("UPLOAD")) {
					return mapping.findForward("upload");			    	  
				}
				if(act!=null && act.equalsIgnoreCase("saveUpload")){
					file = bean.getUploadFile();
					if (file != null && !file.getFileName().trim().equals("")){
						fileByte = file.getFileData();
						isSaved = configMgr.SaveConfig(fileByte);
						if (!isSaved) {
							this.Log("Configuration.do>>> Unable to Save upload file",Level.FATAL);
							bean.setMessage("Fail to upload file.");
						}
						else bean.setMessage("Upload file successfully.");
					}else{
						bean.setMessage("Please select a file.");					
					}
					return mapping.findForward("upload");				
				}      
				if (act.equalsIgnoreCase("DOWNLOAD")) {
					return mapping.findForward("download");			
				}
				else if(act!=null && act.equalsIgnoreCase("saveDownload")){
					response.setContentType("text/xml");
					response.addHeader("Content-Disposition","attachment;filename="+Phrase.SYSTEM_FILE_NAME);
					ServletOutputStream out = response.getOutputStream();
					fileByte = configMgr.GetConfig(Phrase.SYSTEM_FILE_NAME, Phrase.XML_TYPE).getBytes();
					if (fileByte != null)
						for (int i = 0; i < fileByte.length; i++)
							out.write(fileByte[i]);
					out.close();
					return mapping.findForward("download");			
				}
				if (!isConfigurations && !isClient) {
					if (act.equals("REMOVE_USER_CC"))
						this.RemoveSelectedUserCC(bean, request);
					else
						bean.setUserCC(this.GetUserCC(request));
					if (act.equals("REMOVE_USER_BCC"))
						this.RemoveSelectedUserBCC(bean, request);
					else
						bean.setUserBCC(this.GetUserBCC(request));
					if (act.equals("REMOVE_TASK_CC"))
						this.RemoveSelectedTaskCC(bean, request);
					else
						bean.setTaskCC(this.GetTaskCC(request));
					if (act.equals("REMOVE_TASK_BCC"))
						this.RemoveSelectedTaskBCC(bean, request);
					else
						bean.setTaskBCC(this.GetTaskBCC(request));
					if (act.equals("SAVE_EMAIL"))
						isUpdated = this.SaveEmail(bean, request);
					if (isUpdated)
						this.SetEmailWebPage(bean);
					bean.setSelectedIndex(1);
				}
			}
			else
				this.SetConfigurationsWebPage(bean);
			return mapping.findForward("save");	    	
	    }else{
            printMsgToClient("Only Administrator group users can change Data Wizard Configuration. ", response);
            return null;
        }
	}

	  /**
	   * IsValidConfigurationsInput
	   * @param bean
	   * @return boolean
	   */
	private boolean IsValidConfigurationsInput (ConfigurationsBean bean)
	{
		this.Log("Configurations.do: Validating Configuration Input",Level.DEBUG);
		boolean isValid = true;
		if (!bean.getNodeRunning().equals("Running") && !bean.getNodeRunning().equals("Stopped")) {
			bean.setNodeRunningError("Enter Node Status");
			isValid = false;
		}
		else
			bean.setNodeRunningError("");
		if (bean.getNodeName().equals("")) {
			bean.setNodeNameError("Enter Node Name");
			isValid = false;
		}
		else
			bean.setNodeNameError("");
		if (bean.getNaasAddress().equals("")) {
			bean.setNaasAddressError("Enter a NAAS Address");
			isValid = false;
		}
		else
			bean.setNaasAddressError("");
		if (bean.getNodeAddress().equals("")) {
			bean.setNodeAddressError("Enter a Node Address");
			isValid = false;
		}
		else
			bean.setNodeAddressError("");
		if (bean.getNodeAddressV2().equals("")) {
			bean.setNodeAddressErrorV2("Enter a Node Address");
			isValid = false;
		}
		else
			bean.setNodeAddressErrorV2("");
		if (bean.getHasProxy().equals("on") && bean.getProxyServer().equals("")) {
			bean.setProxyServerError("Enter a Proxy Server Address");
			isValid = false;
		}
		else
			bean.setProxyServerError("");
		if (bean.getNodeAdminUID().equals("")) {
			bean.setNodeAdminUIDError("Enter a Node Admin User ID");
			isValid = false;
		}
		else
			bean.setNodeAdminUIDError("");
		if (bean.getNodeAdminPWD().equals("")) {
			bean.setNodeAdminPWDError("Enter a Node Admin Password");
			isValid = false;
		}
		else
			bean.setNodeAdminPWDError("");
		if (isValid) {
			String tokenLifeTime = bean.getTokenLifeTime();
			if (tokenLifeTime == null || tokenLifeTime.equals(""))
				bean.setTokenLifeTime("-1");
			bean.setMessage("");
		}
		else {
			bean.setMessage("Fill in all Required Fields");
			this.Log("Invalid Input Configurations.do",Level.DEBUG);
		}
		return isValid;
	}

	  /**
	   * RemoveSelectedClientURLs
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean RemoveSelectedClientURLs (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Removing Client URLs",Level.DEBUG);
		boolean retBool = false;
		String[] clientURLs = null;
		String[] clientURLs2 = null;
		int index = 0;
		ISystemConfiguration configDB = null;
		// remove client url of version1
		for (int i = 0; true; i++) {
			String selected = request.getParameter("removeClientURLV1_" + i);
			String url = request.getParameter("clientURLV1_" + i);
			if (url != null && !url.equals("") && !(selected != null && selected.equals("on"))) {
				clientURLs = this.EnterValueArray(clientURLs, url, index);
				index++;
			}
			else if (url == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = clientURLs[j];
					}
					configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveClientURLs(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected Client URLs: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool) {
			bean.setMessage("Database Error");
			this.Log("Unable to Remove Selected Client URLs",Level.WARN);
		}
		// remove client url of version2
		index = 0;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("removeClientURLV2_" + i);
			String url = request.getParameter("clientURLV2_" + i);
			if (url != null && !url.equals("") && !(selected != null && selected.equals("on"))) {
				clientURLs2 = this.EnterValueArray(clientURLs2, url, index);
				index++;
			}
			else if (url == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = clientURLs2[j];
					}
					configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveClientURLs_V2(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected Client URLs: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool) {
			bean.setMessage("Database Error");
			this.Log("Unable to Remove Selected Client URLs",Level.WARN);
		}
		return retBool;
	}

	  /**
	   * SetConfigurationsWebPage
	   * @param bean
	   * @return 
	   */
	private void SetConfigurationsWebPage (ConfigurationsBean bean)
	{
		this.Log("Configurations.do: Setting Configurations Web Page",Level.DEBUG);
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			bean.setNodeRunning(config.GetNodeStatus());
			bean.setTokenLifeTime(""+config.GetTokenLifeTime());
			bean.setNodeName(config.GetNodeName());
			bean.setNodeMessage(config.GetNodeMessage());
			bean.setNaasAddress(config.GetNAASURL());
			bean.setNodeAddress(config.GetNodeURL());
			bean.setNodeAddressV2(config.GetNodeURL_V2());
			if (config.GetProxyStatus())
				bean.setHasProxy("on");
			bean.setProxyServer(config.GetProxyHost());
			bean.setProxyPort(config.GetProxyPort());
			bean.setProxyUser(config.GetProxyUID());
			bean.setProxyPassword(config.GetProxyPWD());
			bean.setNodeAdminName(config.GetNodeAdminName());
			bean.setNodeAdminUID(config.GetNodeAdminUID());
			bean.setNodeAdminPWD(config.GetNodeAdminPWD());
			bean.setNodeAdminLog(config.GetAdministrationLogLevel().toString());
			bean.setNodeClientLog(config.GetClientLogLevel().toString());
			bean.setNodeTaskLog(config.GetTaskLogLevel().toString());
			bean.setNodeWebServicesLog(config.GetWebServicesLogLevel().toString());
		} catch (Exception e) {
			this.Log("Could Not Get All of Configuration Settings Values: " + e.getMessage(), Level.ERROR);
		}
	}

	  /**
	   * SetClientWebPage
	   * @param bean
	   * @return 
	   */
	private void SetClientWebPage (ConfigurationsBean bean)
	{
		this.Log("Configurations.do: Setting Client Web Page",Level.DEBUG);
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			bean.setClientURLs(config.GetClientNodeURLs());
			bean.setClientURLs2(config.GetClientNodeURLs_V2());
		} catch (Exception e) {
			this.Log("Could Not Get Client Settings Values: " + e.getMessage(), Level.ERROR);
		}
	}

	  /**
	   * SaveConfiguration
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean SaveConfiguration (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Saving Configurations",Level.DEBUG);
		boolean isSaved = false;
		try {
			if (this.IsValidConfigurationsInput(bean)) {
				ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
				boolean proxyStatus = false;
				if (bean.getHasProxy().equalsIgnoreCase("on"))
					proxyStatus = true;
				isSaved = config.SaveConfigSettings(bean.getNodeRunning(), Integer.parseInt(bean.getTokenLifeTime()), bean.getNodeName(),
						bean.getNodeMessage(), bean.getNaasAddress(), bean.getNodeAddress(),bean.getNodeAddressV2(), proxyStatus,
						bean.getProxyServer(), bean.getProxyPort(), bean.getProxyUser(), bean.getProxyPassword(),
						bean.getNodeAdminName(), bean.getNodeAdminUID(), bean.getNodeAdminPWD(),
						LoggingUtils.ParseLevel(bean.getNodeAdminLog()),LoggingUtils.ParseLevel(bean.getNodeClientLog()),
						LoggingUtils.ParseLevel(bean.getNodeTaskLog()),
						LoggingUtils.ParseLevel(bean.getNodeWebServicesLog()),this.GetSaveClientURLArray(request));
				if (!isSaved) {
					bean.setMessage("Error Saving to Database");
					this.Log("Configurations.do: Unable to Save Configurations Settings",Level.WARN);
				}
				else
					bean.setMessage("Saved");
			}
		} catch (Exception e) {
			bean.setMessage("Error Saving Node Configurations");
			this.Log("Could not Save Configurations: " + e.toString(), Level.ERROR);
		}
		return isSaved;
	}

	  /**
	   * SaveClient
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean SaveClient (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Saving Client Settings",Level.DEBUG);
		boolean isSaved = false;
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			isSaved = config.SaveClientURLs(this.GetSaveClientURLArray(request));
			if (!isSaved) {
				bean.setMessage("Error Saving to Database");
				this.Log("Configurations.do: Unable to Save Client Settings", Level.WARN);
			}
			else{
				isSaved = config.SaveClientURLs_V2(this.GetSaveClientURLArray_V2(request));
				if (!isSaved) {
					bean.setMessage("Error Saving to Database");
					this.Log("Configurations.do: Unable to Save Client Settings", Level.WARN);
				}
				else bean.setMessage("Saved");				
			}
		} catch (Exception e) {
			bean.setMessage("Error Saving Client Settings");
			this.Log("Could not Save Client Settings: " + e.toString(), Level.ERROR);
		}
		return isSaved;
	}

	  /**
	   * GetSaveClientURLArray
	   * @param request
	   * @return String[]
	   */
	private String[] GetSaveClientURLArray (HttpServletRequest request)
	{
		this.Log("Configurations.do: Getting Save Client URL Array",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String url = request.getParameter("clientURLV1_" + i);
			if (url != null && !url.equals("")) {
				temp = this.EnterValueArray(temp, url, index);
				index++;
			}
			else if (url == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (url == null)
				break;
		}
		return retArray;
	}

	  /**
	   * GetSaveClientURLArray_V2
	   * @param request
	   * @return String[]
	   */
	private String[] GetSaveClientURLArray_V2 (HttpServletRequest request)
	{
		this.Log("Configurations.do: Getting Save Client URL2 Array",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String url = request.getParameter("clientURLV2_" + i);
			if (url != null && !url.equals("")) {
				temp = this.EnterValueArray(temp, url, index);
				index++;
			}
			else if (url == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (url == null)
				break;
		}
		return retArray;
	}

	  /**
	   * SetEmailWebPage
	   * @param bean
	   * @return 
	   */
	private void SetEmailWebPage (ConfigurationsBean bean)
	{
		this.Log("Configurations.do: Setting Email Web Page",Level.DEBUG);
		try {
			//bean.setMessage("");
			ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			bean.setHost(configDB.GetEmailServerHost());
			bean.setPort(configDB.GetEmailServerPort());
			bean.setUserSenderName(configDB.GetUserEmailSender());
			bean.setUserEmailAddress(configDB.GetUserEmailSenderEmail());
			bean.setUserUID(configDB.GetUserEmailUID());
			bean.setUserPWD(configDB.GetUserEmailPWD());
			bean.setUserCC(configDB.GetUserEmailCCList());
			bean.setUserBCC(configDB.GetUserEmailBCCList());
			ITaskConfiguration taskConfigDB = DBManager.GetTaskConfiguration(Phrase.AdministrationLoggerName);
			bean.setTaskSenderName(taskConfigDB.GetTaskEmailSender());
			bean.setTaskEmailAddress(taskConfigDB.GetTaskEmailSenderEmail());
			bean.setTaskUID(taskConfigDB.GetTaskEmailUID());
			bean.setTaskPWD(taskConfigDB.GetTaskEmailPWD());
			bean.setTaskCC(taskConfigDB.GetTaskEmailCCList());
			bean.setTaskBCC(taskConfigDB.GetTaskEmailBCCList());
		} catch (Exception e) {
			this.Log("Could Not Get Email Settings: " + e.toString(), Level.ERROR);
		}
	}

	  /**
	   * GetUserCC
	   * @param request
	   * @return String[]
	   */
	private String[] GetUserCC (HttpServletRequest request)
	{
		this.Log("Configurations.do: Getting User CC List",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String cc = request.getParameter("userCCList" + i);
			if (cc != null && !cc.equals("")) {
				temp = this.EnterValueArray(temp, cc, index);
				index++;
			}
			else if (cc == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (cc == null)
				break;
		}
		return retArray;
	}

	  /**
	   * GetUserBCC
	   * @param request
	   * @return String[]
	   */
	private String[] GetUserBCC (HttpServletRequest request)
	{
		this.Log("Configurations.do: Getting User BCC List",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String bcc = request.getParameter("userBCCList" + i);
			if (bcc != null && !bcc.equals("")) {
				temp = this.EnterValueArray(temp, bcc, index);
				index++;
			}
			else if (bcc == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (bcc == null)
				break;
		}
		return retArray;
	}

	  /**
	   * GetTaskCC
	   * @param request
	   * @return String[]
	   */
	private String[] GetTaskCC (HttpServletRequest request)
	{
		this.Log("Configurations.do: Get Task CC List",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String cc = request.getParameter("taskCCList" + i);
			if (cc != null && !cc.equals("")) {
				temp = this.EnterValueArray(temp, cc, index);
				index++;
			}
			else if (cc == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (cc == null)
				break;
		}
		return retArray;
	}

	  /**
	   * GetTaskBCC
	   * @param request
	   * @return String[]
	   */
	private String[] GetTaskBCC (HttpServletRequest request)
	{
		this.Log("Configurations.do: Getting Task BCC List",Level.DEBUG);
		String[] retArray = null;
		String[] temp = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String bcc = request.getParameter("taskBCCList" + i);
			if (bcc != null && !bcc.equals("")) {
				temp = this.EnterValueArray(temp, bcc, index);
				index++;
			}
			else if (bcc == null && temp != null) {
				retArray = new String [index];
				for (int j = 0; j < index; j++)
					retArray[j] = temp[j];
				break;
			}
			else if (bcc == null)
				break;
		}
		return retArray;
	}

	  /**
	   * RemoveSelectedUserCC
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean RemoveSelectedUserCC (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Removing User CC List",Level.DEBUG);
		boolean retBool = false;
		String[] ccList = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("userCCSelect" + i);
			String cc = request.getParameter("userCCList" + i);
			if (cc != null && !cc.equals("") && (selected == null || !selected.equals("on"))) {
				ccList = this.EnterValueArray(ccList, cc, index);
				index++;
			}
			if (cc == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = ccList[j];
					}
					ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveUserCCList(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected User CCList: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool)
			this.Log("Configurations.do: Could Not Save Removed Selected User CC List",Level.WARN);
		return retBool;
	}

	  /**
	   * RemoveSelectedUserBCC
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean RemoveSelectedUserBCC (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Removing User BCC List",Level.DEBUG);
		boolean retBool = false;
		String[] bccList = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("userBCCSelect" + i);
			String bcc = request.getParameter("userBCCList" + i);
			if (bcc != null && !bcc.equals("") && (selected == null || !selected.equals("on"))) {
				bccList = this.EnterValueArray(bccList, bcc, index);
				index++;
			}
			if (bcc == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = bccList[j];
					}
					ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveUserBCCList(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected User BCCList: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool)
			this.Log("Configurations.do: Could Not Save Removed Selected User BCC List",Level.WARN);
		return retBool;
	}

	  /**
	   * RemoveSelectedTaskCC
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean RemoveSelectedTaskCC (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Removing Task CC List",Level.DEBUG);
		boolean retBool = false;
		String[] ccList = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("taskCCSelect" + i);
			String cc = request.getParameter("taskCCList" + i);
			if (cc != null && !cc.equals("") && (selected == null || !selected.equals("on"))) {
				ccList = this.EnterValueArray(ccList, cc, index);
				index++;
			}
			if (cc == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = ccList[j];
					}
					ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveTaskCCList(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected Task CCList: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool)
			this.Log("Configurations.do: Could Not Save Removed Selected Task CC List",Level.WARN);
		return retBool;
	}

	  /**
	   * RemoveSelectedTaskBCC
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean RemoveSelectedTaskBCC (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Removing Task BCC List",Level.DEBUG);
		boolean retBool = false;
		String[] bccList = null;
		int index = 0;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("taskBCCSelect" + i);
			String bcc = request.getParameter("taskBCCList" + i);
			if (bcc != null && !bcc.equals("") && (selected == null || !selected.equals("on"))) {
				bccList = this.EnterValueArray(bccList, bcc, index);
				index++;
			}
			if (bcc == null) {
				try {
					String[] input = null;
					if (index > 0) {
						input = new String [index];
						for (int j = 0; j < index; j++)
							input[j] = bccList[j];
					}
					ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.AdministrationLoggerName);
					retBool = configDB.SaveTaskBCCList(input);
				} catch (Exception e) {
					this.Log("Could Not Remove Selected Task BCCList: " + e.toString(), Level.ERROR);
				}
				break;
			}
		}
		if (!retBool)
			this.Log("Configurations.do: Could Not Save Removed Selected Task BCC List",Level.WARN);
		return retBool;
	}

	  /**
	   * SaveEmail
	   * @param bean
	   * @param request
	   * @return boolean
	   */
	private boolean SaveEmail (ConfigurationsBean bean, HttpServletRequest request)
	{
		this.Log("Configurations.do: Saving Email Settings",Level.DEBUG);
		boolean isSaved = false;
		if (this.IsValidEmailInput(bean)) {
			try {
				ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
				isSaved = configDB.SaveEmailSettings(bean.getHost(),bean.getPort(),bean.getUserSenderName(),bean.getUserEmailAddress(),
						bean.getUserUID(),bean.getUserPWD(),this.GetUserCC(request),this.GetUserBCC(request),
						bean.getTaskSenderName(),bean.getTaskEmailAddress(),bean.getTaskUID(),bean.getTaskPWD(),
						this.GetTaskCC(request),this.GetTaskBCC(request));
				if (!isSaved) {
					bean.setMessage("Error Saving to Database");
					this.Log("Configurations.do: Could Not Save to Database",Level.WARN);
				}
				else
					bean.setMessage("Saved");
			} catch (Exception e) {
				this.Log("Could Not Save Email Settings: " + e.toString(), Level.ERROR);
			}
		}
		return isSaved;
	}

	  /**
	   * IsValidEmailInput
	   * @param bean
	   * @return boolean
	   */
	private boolean IsValidEmailInput (ConfigurationsBean bean)
	{
		this.Log("Configurations.do: Validating Email Input",Level.DEBUG);
		boolean isValid = true;
		String temp = bean.getHost();
		if (temp == null || temp.equals("")) {
			bean.setHostError("Enter Email Server Address");
			isValid = false;
		}
		else
			bean.setHostError("");
		temp = bean.getUserSenderName();
		if (temp == null || temp.equals("")) {
			bean.setUserSenderNameError("Enter a Sender Name");
			isValid = false;
		}
		else
			bean.setUserSenderNameError("");
		temp = bean.getUserEmailAddress();
		if (temp == null || temp.equals("")) {
			bean.setUserEmailAddressError("Enter a Sender Email Address");
			isValid = false;
		}
		else
			bean.setUserEmailAddressError("");
		temp = bean.getTaskSenderName();
		if (temp == null || temp.equals("")) {
			bean.setTaskSenderNameError("Enter a Sender Name");
			isValid = false;
		}
		else
			bean.setTaskSenderNameError("");
		temp = bean.getTaskEmailAddress();
		if (temp == null || temp.equals("")) {
			bean.setTaskEmailAddressError("Enter a Sender Email Address");
			isValid = false;
		}
		else
			bean.setTaskEmailAddressError("");
		if (isValid)
			bean.setMessage("");
		else {
			bean.setMessage("Fill in all Required Fields");
			this.Log("Configurations.do: Invalid Email Input",Level.DEBUG);
		}
		return isValid;
	}
	  /**
	   * printMsgToClient
	   * @param result
	   * @param response
	   * @return 
	   */
	private static void printMsgToClient(String result,
			HttpServletResponse response) throws Exception {
		//response.setCharacterEncoding("UTF-8");
		PrintWriter out = response.getWriter();
		try {
			out.print(result);
		} finally {
			out.close();
		}
	}

}
