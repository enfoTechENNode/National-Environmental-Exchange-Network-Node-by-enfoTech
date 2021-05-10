package Node.Web.Administration.Action.Users;

import java.util.ArrayList;
import java.util.HashMap;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Domain;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.INodeUser;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Users.ConsoleUsersBean;
/**
 * <p>This class create ConsoleUsersAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ConsoleUsersAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public ConsoleUsersAction() {
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
    this.Log("Executing ConsoleUsers.do",Level.INFO);
    ConsoleUsersBean bean = (ConsoleUsersBean)form;
    bean.setMessage("");
    this.ClearErrors(bean);
    String isNewUser = request.getParameter("new");
    if (isNewUser != null && isNewUser.equals("true")) {
      this.ClearWebPage(bean, request.getSession());
      bean.setTitle("New Administration Console User");
    }
    String isExistingUser = request.getParameter("userid");
    if (isExistingUser != null) {
      int userID = Integer.parseInt(isExistingUser);
      this.SetWebPage(bean, new User(userID,Phrase.AdministrationLoggerName), request.getSession());
    }
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("ConsoleUsers.do act = " + act,Level.DEBUG);
      if (act.equals("SAVE")) {
        User updated = this.Save(bean,request);
        if (updated != null)
          this.SetWebPage(bean, updated, request.getSession());
      }
      if (act.equals("ADD_DOMAIN"))
        this.AddDomain(bean);
      if (act.equals("REMOVE_DOMAIN"))
        this.RemoveDomain(bean);
      if (act.equalsIgnoreCase("NEW_PASSWORD"))
          this.ChangePassword(bean);
      if (act.equalsIgnoreCase("DELETE"))
          this.DeleteUser(bean);
    }
    return mapping.findForward("save");
  }

  /**
   * AddDomain
   * @param bean
   * @return 
   */
  private void AddDomain (ConsoleUsersBean bean)
  {
    this.Log("ConsoleUsers.do: Adding a Domain",Level.DEBUG);
    String domain = bean.getDomAvailSel();
    if (domain != null && !domain.equals("")) {
      String[] avail = bean.getDomainsAvailable();
      if (avail == null || avail.length < 2)
        bean.setDomainsAvailable(null);
      else {
        String[] temp = new String [avail.length - 1];
        int index = 0;
        for (int i = 0; i < avail.length; i++) {
          if (!avail[i].equals(domain)) {
            temp[index] = avail[i];
            index++;
          }
        }
        bean.setDomainsAvailable(temp);
      }
      String[] assigned = bean.getDomainsAssigned();
      String[] newAssigned = null;
      if (assigned == null || assigned.length == 0)
        newAssigned = new String [1];
      else
        newAssigned = new String [assigned.length + 1];
      if (assigned != null)
        for (int i = 0; i < newAssigned.length-1; i++)
          newAssigned[i] = assigned[i];
      newAssigned[newAssigned.length - 1] = domain;
      bean.setDomainsAssigned(newAssigned);
    }
  }

  /**
   * RemoveDomain
   * @param bean
   * @return 
   */
  private void RemoveDomain (ConsoleUsersBean bean)
  {
    this.Log("ConsoleUsers.do: Removing a Domain",Level.DEBUG);
    String domain = bean.getDomAssSel();
    if (domain != null && !domain.equals("")) {
      String[] assigned = bean.getDomainsAssigned();
      if (assigned == null || assigned.length < 2)
        bean.setDomainsAssigned(null);
      else {
        String[] temp = new String [assigned.length - 1];
        int index = 0;
        for (int i = 0; i < assigned.length; i++) {
          if (!assigned[i].equals(domain)) {
            temp[index] = assigned[i];
            index++;
          }
        }
        bean.setDomainsAssigned(temp);
      }
      String[] avail = bean.getDomainsAvailable();
      String[] newAvail = null;
      if (avail == null || avail.length == 0)
        newAvail = new String [1];
      else
        newAvail = new String [avail.length + 1];
      for (int i = 0; i < newAvail.length-1; i++)
        newAvail[i] = avail[i];
      newAvail[newAvail.length - 1] = domain;
      bean.setDomainsAvailable(newAvail);
    }
  }
  /**
   * DeleteUser
   * @param bean
   * @return 
   */
  private void DeleteUser (ConsoleUsersBean bean)
  {
	  User user = null;
	  try {
		  user = new User(bean.getUserID(),Phrase.AdministrationLoggerName);
		  if (user!= null && user.IsConsoleUser()){
			  INodeOperationLog opLogDB = DBManager.GetNodeOperationLog(Phrase.AdministrationLoggerName);
			  if(opLogDB.hasOperationLog(user.GetLoginName())){
				  bean.setMessage("Could Not delete User: " + user.GetLoginName() + " since this user has operation Log.");
				  return;
			  }
			  INodeUser userDB = DBManager.GetNodeUser(Phrase.AdministrationLoggerName);
			  int userId = user.GetUserID();
			  boolean ret = userDB.DeleteUser(userId);
			  if(!ret){
				  this.Log("Could Not delete User: " + user.GetLoginName(), Level.ERROR);
				  bean.setMessage("Could Not delete User: " + user.GetLoginName());
			  }else{
				  bean.setMessage("User: " + user.GetLoginName() + " has been deleted.");
			  }
		  }
	  } catch (Exception e) {
	        this.Log("Could Not delete User: " + e.toString(), Level.ERROR);
	        bean.setMessage("Database Error");
	  }

  }
  /**
   * Save
   * @param bean
   * @param request
   * @return User
   */
  private User Save (ConsoleUsersBean bean, HttpServletRequest request)
  {
    this.Log("ConsoleUsers.do: Saving User",Level.DEBUG);
    User updatedUser = null;
    if (this.IsValidInput(bean)) {
      try {
        User user = null;
        user = new User(bean.getUserID(),Phrase.AdministrationLoggerName);
        if (user.GetUserID() >= 0)
          user.SetChangePWD(false);
        user.SetStatus(bean.getStatus());
        user.SetFirstName(bean.getFirstName());
        user.SetMiddleInitial(bean.getMidInitial());
        user.SetLastName(bean.getLastName());
        user.SetEmailAddress(bean.getEmail());
        user.SetPhone(bean.getPhone());
        user.SetComments(bean.getComments());
        user.SetAddress(bean.getAddress());
        user.SetSupplAddress(bean.getSuppAddress());
        user.SetCity(bean.getCity());
        user.SetState(bean.getState());
        user.SetZipCode(bean.getZipCode());
        user.SetCountry(bean.getCountry());
        user.SetAssignedDomains(this.GetAssignedDomains(bean,request));
        user.SetAccountType(Phrase.ConsoleUser);
        int result = -1;
        User admin = new User((String)request.getSession().getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        if (bean.getTitle().equals("New Administration Console User"))
          result = user.SaveNewUser(Phrase.AdministrationLoggerName,admin);
        else
          result = user.SaveUser(Phrase.AdministrationLoggerName,admin);
        if (result == User.SUCCESS) {
          updatedUser = user;
          if (!bean.getTitle().equals("New Administration Console User"))
            bean.setMessage("Saved Successfully");
          else
            bean.setMessage("Saved Successfully and New Password sent to " + bean.getEmail());
        }
        else {
          if (result == User.DATABASE_ERROR)
            bean.setMessage("Database Error");
          else if (result == User.EMAIL_ERROR)
            bean.setMessage("Create User successfully but send notify email Error.Please check email server setting or email sender account.");
          this.Log("ConsoleUsers.do: Could Not Save Console User "+user.GetLoginName()+": "+bean.getMessage(),Level.WARN);
        }
      } catch (Exception e) {
        this.Log("Could Not Save Console User: " + e.toString(), Level.ERROR);
        bean.setMessage("Database Error");
      }
    }
    return updatedUser;
  }

  /**
   * SetWebPage
   * @param bean
   * @param user
   * @param session
   * @return 
   */
  private void SetWebPage (ConsoleUsersBean bean, User user, HttpSession session)
  {
    this.Log("ConsoleUsers.do: Setting Web Page for User "+user !=null?user.GetLoginName():"",Level.DEBUG);
    if (user != null) {
      bean.setTitle(user.GetLoginName());
      bean.setUserID(user.GetLoginName());
      bean.setStatus(user.GetStatus());
      bean.setFirstName(user.GetFirstName());
      bean.setMidInitial(user.GetMiddleInitial());
      bean.setLastName(user.GetLastName());
      bean.setEmail(user.GetEmailAddress());
      bean.setPhone(user.GetPhone());
      bean.setAddress(user.GetAddress());
      bean.setSuppAddress(user.GetSupplAddress());
      bean.setCity(user.GetCity());
      bean.setState(user.GetState());
      bean.setZipCode(user.GetZipCode());
      bean.setCountry(user.GetCountry());
      bean.setComments(user.GetComments());
      try {
        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        Domain[] domains = Domain.GetDomains(admin,Phrase.AdministrationLoggerName);
        int[] domainIDs = user.GetAssignedDomainIDs(Phrase.AdministrationLoggerName);
        HashMap map = new HashMap();
        if (domainIDs != null)
          for (int i = 0; i < domainIDs.length; i++)
            map.put(domainIDs[i]+"",domainIDs[i]+"");
        ArrayList list = null;
        if (domains != null && domains.length > 0) {
          list = new ArrayList();
          for (int i = 0; i < domains.length; i++) {
            ArrayList temp = new ArrayList();
            temp.add(map.containsKey(domains[i].GetDomainID()+"")+"");
            temp.add(domains[i].GetDomainID()+"");
            temp.add(domains[i].GetDomainName());
            list.add(temp);
          }
        }
        bean.setDomains(list);
        //bean.setDomainsAssigned(user.GetDomainsAssigned(admin));
        //bean.setDomainsAvailable(admin.GetDomainsAvailable(user.GetAssignedDomains(),Phrase.AdministrationLoggerName));
        //bean.setIsEditable(user.BelongsTo(admin));
        bean.setIsEditable(true);
      } catch (Exception e) {
        this.Log("Could Not Get Session User: " + e.toString(), Level.ERROR);
      }
    }
    else {
      this.ClearWebPage(bean, session);
      bean.setMessage("Database Error");
      this.Log("ConsoleUsers.do: Could Not Set Web Page, User is null",Level.WARN);
    }
  }

  /**
   * ChangePassword
   * @param bean
   * @return 
   */
  private void ChangePassword (ConsoleUsersBean bean)
  {
    this.Log("ConsoleUsers.do: Changing password for "+bean.getUserID(),Level.DEBUG);
    try {
      User temp = new User (bean.getUserID(),Phrase.AdministrationLoggerName);
      if (temp.ChangePWD(Phrase.AdministrationLoggerName))
        bean.setMessage("Password Sent to User " + bean.getUserID());
      else {
        bean.setMessage("Failed Password Generation,Please check database, Email server or user Email account.");
        this.Log("ConsoleUsers.do: Could Not Change Password",Level.WARN);
      }
    } catch (Exception e) {
      this.Log("Could Not Change Password: " + e.toString(), Level.ERROR);
      bean.setMessage("Password Could Not Be Sent");
    }
  }

  /**
   * IsValidInput
   * @param bean
   * @return boolean
   */
  private boolean IsValidInput (ConsoleUsersBean bean)
  {
    this.Log("Validating Input ConsoleUsers.do",Level.DEBUG);
    boolean isValid = true;
    String temp = bean.getUserID();
    String newUser = bean.getTitle();
    if (newUser.equals("New Administration Console User") && temp == null || temp.equals("")) {
      bean.setUserIDError("Enter a Login Name");
      isValid = false;
    }
    else if (newUser.equals("New Administration Console User")) {
      try {
        int userID = User.GetUserIDFromDB(temp,Phrase.AdministrationLoggerName);
        if (userID >= 0) {
          bean.setUserID("");
          bean.setUserIDError("Enter a Different Login Name");
          isValid = false;
        }
      }
      catch (Exception e) {
        this.Log("Could Not Verify Unique User ID: " + e.toString(),Level.ERROR);
      }
    }
    temp = bean.getFirstName();
    if (temp == null || temp.equals("")) {
      bean.setFirstNameError("Enter a First Name");
      isValid = false;
    }
    temp = bean.getLastName();
    if (temp == null || temp.equals("")) {
      bean.setLastNameError("Enter Last Name");
      isValid = false;
    }
    temp = bean.getEmail();
    if (temp == null || temp.equals("")) {
      bean.setEmailError("Enter an Email Address");
      isValid = false;
    }
    if (!isValid)
      bean.setMessage("Fill in all Required Fields");
    else {
      bean.setMessage("");
      this.Log("Invalid Input ConsoleUsers.do",Level.DEBUG);
    }
    return isValid;
  }

  /**
   * ClearWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void ClearWebPage (ConsoleUsersBean bean, HttpSession session)
  {
    this.Log("ConsoleUsers.do: Clearing Web Page",Level.DEBUG);
    bean.setAddress("");
    bean.setCity("");
    bean.setComments("");
    bean.setCountry("");
    bean.setDomainsAssigned(null);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      Domain[] domains = Domain.GetDomains(admin,Phrase.AdministrationLoggerName);
      ArrayList list = null;
      if (domains != null && domains.length > 0) {
        list = new ArrayList();
        for (int i = 0; i < domains.length; i++) {
          ArrayList temp = new ArrayList();
          temp.add("false");
          temp.add(domains[i].GetDomainID()+"");
          temp.add(domains[i].GetDomainName());
          list.add(temp);
        }
      }
      bean.setDomains(list);
    } catch (Exception e) {
      this.Log("Could Not Get Session User: " + e.toString(), Level.ERROR);
    }
    bean.setIsEditable(true);
    bean.setDomAssSel("");
    bean.setDomAvailSel("");
    bean.setEmail("");
    bean.setEmailError("");
    bean.setFirstName("");
    bean.setFirstNameError("");
    bean.setLastName("");
    bean.setLastNameError("");
    bean.setMessage("");
    bean.setMidInitial("");
    bean.setPhone("");
    bean.setState("");
    bean.setStatus("");
    bean.setSuppAddress("");
    bean.setUserID("");
    bean.setUserIDError("");
    bean.setZipCode("");
  }

  /**
   * ClearErrors
   * @param bean
   * @return 
   */
  private void ClearErrors (ConsoleUsersBean bean)
  {
    bean.setEmailError("");
    bean.setFirstNameError("");
    bean.setLastNameError("");
    bean.setUserIDError("");
  }

  /**
   * GetAssignedDomains
   * @param bean
   * @param request
   * @return String[]
   */
  private String[] GetAssignedDomains (ConsoleUsersBean bean, HttpServletRequest request)
  {
    String[] retArray = null;
    HashMap map = new HashMap();
    ArrayList list = bean.getDomains();
    if (list != null) {
      for (int i = 0; i < list.size(); i++) {
        ArrayList temp = (ArrayList)list.get(i);
        if (temp != null) {
          String test = request.getParameter("domain"+temp.get(1));
          if (test != null && test.equalsIgnoreCase("on"))
            map.put(temp.get(2)+"",temp.get(2)+"");
        }
      }
    }
    if (!map.isEmpty()) {
      Object[] temp = map.values().toArray();
      if (temp != null && temp.length > 0) {
        retArray =  new String[temp.length];
        for (int i = 0; i < temp.length; i++)
          retArray[i] = (String)temp[i];
      }
    }
    return retArray;
  }
}
