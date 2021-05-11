package Node.Web.Administration.Action.Entry;

import java.io.PrintWriter;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.enfotech.basecomponent.jndi.JNDIAccess;

import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.INodeUser;
import Node.Phrase;
import Node.Utils.Utility;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Entry.LoginBean;
/**
 * <p>This class create LoginAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LoginAction extends BaseAction {
	  private HttpServletRequest Request = null;
	  private HttpServletResponse Response = null;

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public LoginAction() {
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
	String act = request.getParameter("act");
    this.Log("Executing Login.do",Level.DEBUG);
    LoginBean bean = (LoginBean)form;
    this.Request = request;
    this.Response = response;
    HttpSession session = request.getSession();

    // From Logout
    if (this.isLogout(bean, request.getParameter("logout"))) {
	  if (session.getAttribute(Phrase.USER_SESSION) != null){
          session.removeAttribute(Phrase.USER_SESSION);
      }
	  if (session.getAttribute(Phrase.STATUS_SESSION_COUNTER) != null){
          session.removeAttribute(Phrase.STATUS_SESSION_COUNTER);
      }
      session.invalidate();
      return mapping.findForward("failed");
    }

    // show dashboard internal Invalid Session
    if (!session.isNew() && this.IsInvalidSession(bean, request.getParameter("invalid")))
      return mapping.findForward("invalidSession");

    // Jump Invalid Session to login page
    if (!session.isNew() && this.IsInvalidSession(bean, request.getParameter("sessionTimeOut")))
      return mapping.findForward("failed");

    // From Change Password
    if (act != null && act.equalsIgnoreCase("changePWD")){
    	bean.setChangePWD(true);
        return mapping.findForward("changePWD");
    }
    // WI 33893
    // From Reset Password
    if (act != null && act.equalsIgnoreCase("resetPWD")){
    	bean.setResetPWD(true);
        return mapping.findForward("resetPWD");
    }
    
    // From Cancel Change Password
    if (act != null && act.equalsIgnoreCase("CANCEL")){
    	bean.setChangePWD(false);
        return mapping.findForward("failed");
    }
    
    
    if (act != null && !act.equals("")) {
      this.Log("Login.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("LOGIN") && (bean.getLoginPassword()!=null || !bean.getLoginPassword().equalsIgnoreCase(""))) {
        // Trying to login
        int result = this.isLoginSuccessful(bean);
        if (result >= 0) {
          session = request.getSession();
          String user = (String)session.getAttribute(Phrase.USER_SESSION);
          if (user != null)
            session.removeAttribute(Phrase.USER_SESSION);
          session.setAttribute(Phrase.USER_SESSION, bean.getLoginName());
          session.setAttribute(Phrase.STATUS_SESSION_COUNTER,new Integer(0));
          if (result == INodeUser.CHANGE_PWD) {
            bean.setChangePWD(true);
            return mapping.findForward("changePWD");
          }
          else
          {
            this.SetLoggerLevel();
            bean.setLoginPassword("");
            return mapping.findForward("success");
          }
        }
        else
          return mapping.findForward("failed");
      }
      if (act.equalsIgnoreCase("CHANGE_PWD")) {
        String pwd1 = bean.getNewPWD1();
        String pwd2 = bean.getNewPWD2();
        boolean isSaved = false;
        if (pwd1 != null && !pwd1.equals("") && pwd2 != null && !pwd2.equals("") && pwd1.equals(pwd2)) {
          session = request.getSession();
          String login = (String)session.getAttribute(Phrase.USER_SESSION);
          int result = this.isLoginSuccessful(bean);
          if ((login != null && !login.equals(""))||result >= 0) {
            try {
              if(result >= 0){
            	  session.setAttribute(Phrase.USER_SESSION, bean.getLoginName());
                  session.setAttribute(Phrase.STATUS_SESSION_COUNTER,new Integer(0));
            	  login = bean.getLoginName();
              }
              INodeUser userDB = DBManager.GetNodeUser(Phrase.AdministrationLoggerName);
              int temp = userDB.SavePassword(login, pwd1);
              if (temp >= 0)
                isSaved = true;
            } catch (Exception e) {
              this.Log("Could Not Save Password: " + e.toString(), Level.ERROR);
            }
          }else{
              bean.setMessage("Make sure the username and password are correct.");
              return mapping.findForward("changePWD");
          }
        }
        if (!isSaved) {
          bean.setChangePWD(true);
          bean.setMessage("Enter the Same Password in Both 'New Password' and 'Confirm Password' Boxes");
          return mapping.findForward("changePWD");
        }
        else
        {
            this.SetLoggerLevel();
            return mapping.findForward("success");
        }
      }
      // WI 33893
      if (act.equalsIgnoreCase("RESET_PWD")) {
          String email = bean.getEmail();
          if (!Utility.isNullOrEmpty(email)) {
              INodeUser userDB = DBManager.GetNodeUser(this.GetLogSessionValue());
              if (userDB != null) {
            	  int userId = userDB.verifyEmail(email);
            	  if( userId != -99){
            		  try {
        		        User temp = new User (userId,Phrase.AdministrationLoggerName);
        		        if (temp.ChangePWD(Phrase.AdministrationLoggerName)){
        		          bean.setMessage("The temporary password has been sent to email: " + email);
        		        }else {
        		          bean.setMessage("Failed Password Generation,Please check database, Email server or user Email account.");
        		          this.Log("Login.do: Could Not Change Password",Level.WARN);
        		        }
        		      } catch (Exception e) {
        		        this.Log("Could Not Change Password: " + e.toString(), Level.ERROR);
        		        bean.setMessage("Password Could Not Be Sent");
        		      }
                      return mapping.findForward("resetPWD");
            	  }else{
            		  bean.setMessage("Invalid Email Address.");
            	  }
              }       	  
          }
      }
      if (act.equalsIgnoreCase("SET_VERSION")) {
    	  session.setAttribute(Phrase.NodeVersion, request.getParameter("version"));
    	  return null;
      }
      if (act.equalsIgnoreCase("GET_DOTNETURL")) {
	      // trigger wizard
			String dotNetHost = (String) JNDIAccess.GetJNDIValue(Phrase.dotNetHost, false);
			String dotNetHostPort = (String) JNDIAccess.GetJNDIValue(Phrase.dotNetHostPort, false);
			String DotNetURL = null;
			if(dotNetHost==null || dotNetHost.equals("")){
				DotNetURL = "Wizard function is not available. Please check web.xml to find configuration of node/dotNetHost and node/dotNetHostPort.";
			}else{
				DotNetURL = "http://"+dotNetHost+(dotNetHostPort.equalsIgnoreCase("")?"":":"+dotNetHostPort)+"/Node.DataWizard/Pages/DataWizard/DataFlowWizard.aspx";    				  
			}
			printMsgToClient(DotNetURL, response);
			return null;
      }

    }
    return mapping.findForward("failed");
  }

  /**
   * isLogout
   * @param bean
   * @param logout
   * @return boolean
   */
  private boolean isLogout (LoginBean bean, String logout)
  {
    boolean ret = false;
    if (logout != null && logout.equals("true")) {
      bean.setMessage("Successfully Logged Out");
      ret = true;
    }
    return ret;
  }

  /**
   * IsInvalidSession
   * @param bean
   * @param invalid
   * @return boolean
   */
  private boolean IsInvalidSession (LoginBean bean, String invalid)
  {
    boolean retBool = false;
    if (invalid != null && invalid.equals("true")) {
      bean.setMessage("Session Timeout or Invalid Session");
      retBool = true;
    }
    return retBool;
  }

  /**
   * isLoginSuccessful
   * @param bean
   * @return int
   */
  private int isLoginSuccessful (LoginBean bean)
  {
    int retInt = -1;
    try {
      INodeUser userDB = DBManager.GetNodeUser(this.GetLogSessionValue());
      if (userDB != null) {
        int result = userDB.ValidateLogin(bean.getLoginName(),bean.getLoginPassword());
        if (result == userDB.INVALID_PASSWORD) {
          retInt = result;
          bean.setMessage("Invalid Password");
        }
        else if (result == userDB.USER_DOES_NOT_EXIST) {
          retInt = result;
          bean.setMessage("User Does Not Exist");
        }
        else if (result == userDB.INACTIVE_USER) {
          retInt = result;
          bean.setMessage("User is Inactive");
        }
        else if (result == userDB.DATABASE_UNAVAILABLE) {
          retInt = result;
          bean.setMessage("Database Connection Unavailable");
        }
        else if (result == userDB.CHANGE_PWD) {
          retInt = result;
          bean.setMessage("Please Provide a New Password");
        }
        else if (result == userDB.INVALID_PERMISSION) {
          retInt = result;
          bean.setMessage("Invalid Permission");
        }
        else if (result == userDB.SUCCESSFUL) {
          retInt = result;
        }
      }
      else {
        this.Log("Database Connection Failure", Level.WARN);
        bean.setMessage("Database Connection Unavailable");
      }
    } catch (Exception e) {
      this.Log("Database Connection Failure: "+e.toString(), Level.ERROR);
      bean.setMessage("Database Connection Unavailable");
    }
    return retInt;
  }

  /**
   * SetLoggerLevel
   * @param 
   * @return 
   */
  private void SetLoggerLevel()
  {
    Logger logger = Logger.getLogger(Phrase.AdministrationLoggerName);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
      if (config != null) {
        Level level = config.GetAdministrationLogLevel();
        if (level != null)
          logger.setLevel(level);
        else
          logger.error("Could Not Find Administration Log Level in system.config");
      }
      else
        logger.error("Could not get system.config from Configuration File");
    } catch (Exception e) {
      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }
  
  /**
   * printMsgToClient
   * @param result
   * @param response
   * @return 
   */
	public static void printMsgToClient(String result,
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
