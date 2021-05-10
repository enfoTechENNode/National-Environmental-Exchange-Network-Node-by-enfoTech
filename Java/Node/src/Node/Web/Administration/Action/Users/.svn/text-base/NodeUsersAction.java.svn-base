package Node.Web.Administration.Action.Users;

import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.INodeUser;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.NAAS.Requestor.NAASRequestor;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Users.NodeUsersBean;
/**
 * <p>This class create NodeUsersAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeUsersAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public NodeUsersAction() {
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
    this.Log("Executing NodeUsers.do",Level.INFO);
    NodeUsersBean bean = (NodeUsersBean)form;
    bean.setMessage("");
    this.ClearErrors(bean);
    String isNewUser = request.getParameter("new");
    if (isNewUser != null && isNewUser.equals("true")) {
      this.ClearWebPage(bean,request.getSession());
      bean.setTitle("New Node User");
    }
    String isExistingUser = request.getParameter("userid");
    if (isExistingUser != null) {
      int userID = Integer.parseInt(isExistingUser);
      this.SetWebPage(bean, new User(userID,Phrase.AdministrationLoggerName),request.getSession(),false);
    }
    String isNAASUser = request.getParameter("username");
    if (isNAASUser != null) {
      User user = new User(isNAASUser,Phrase.AdministrationLoggerName);
      this.SetWebPage(bean,user,request.getSession(),true);
    }
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("NodeUsers.do act = " + act,Level.DEBUG);
      if (act.equals("SAVE")) {
        User updated = this.Save(bean,request);
        if (updated != null)
          this.SetWebPage(bean, updated,request.getSession(),updated.GetAccountType().equals(Phrase.NAASNodeUser)?true:false);
      }
      if (act.equals("ADD_OPERATION"))
        this.AddOperation(bean,request);
      if (act.equals("REMOVE_OPERATION"))
        this.RemoveOperation(bean,request);
      if (act.equalsIgnoreCase("NEW_PASSWORD"))
        this.ChangePassword(bean);
      if (act.equalsIgnoreCase("DOMAIN_CHANGE"))
        this.ChangeDomain(bean);
      if (act.equalsIgnoreCase("DELETE"))
          this.DeleteUser(bean,request);
    }

    return mapping.findForward("save");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (NodeUsersBean bean, User user, HttpSession session, boolean isNAAS)
  {
    this.Log("NodeUsers.do: Setting Web Page User "+user!=null?user.GetLoginName():"",Level.DEBUG);
    if (user != null) {
      if (isNAAS) {
        bean.setUserID(user.GetLoginName());
        bean.setTitle(user.GetLoginName());
        bean.setUserType(Phrase.NAASNodeUser);
      }
      else {
        bean.setAddress(user.GetAddress());
        bean.setCity(user.GetCity());
        bean.setComments(user.GetComments());
        bean.setCountry(user.GetCountry());
        bean.setEmail(user.GetEmailAddress());
        bean.setFirstName(user.GetFirstName());
        bean.setLastName(user.GetLastName());
        bean.setMidInitial(user.GetMiddleInitial());
        bean.setPhone(user.GetPhone());
        bean.setState(user.GetState());
        bean.setStatus(user.GetStatus());
        bean.setSuppAddress(user.GetSupplAddress());
        bean.setTitle(user.GetLoginName());
        bean.setUserID(user.GetLoginName());
        bean.setUserType(user.GetAccountType());
        bean.setZipCode(user.GetZipCode());
      }
      try {
        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        Operation[] ops = Operation.GetOperationsList(Phrase.AdministrationLoggerName,admin);
        int[] assigned = user.GetAssignedOperationIDs(Phrase.AdministrationLoggerName);
        HashMap map = new HashMap();
        if (assigned != null) {
          for (int i = 0; i < assigned.length; i++)
            map.put(assigned[i]+"",assigned[i]+"");
        }
        ArrayList list = new ArrayList();
        if (ops != null) {
          for (int i = 0; i < ops.length; i++) {
            ArrayList temp = new ArrayList();
            temp.add(map.containsKey(ops[i].GetOperationID()+"")+"");
            temp.add(ops[i].GetOperationID()+"");
            temp.add(ops[i].GetDomain());
            temp.add(ops[i].GetWebService());
            temp.add(ops[i].GetOpName());
            list.add(temp);
          }
        }
        bean.setOps(list);
        /*
        String[] domains = admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName);
        bean.setDomains(domains);
        bean.setOperationsAssigned(user.GetAssignedOperations(Phrase.AdministrationLoggerName));
        if (domains != null && domains.length > 0) {
          bean.setDomain(domains[0]);
          bean.setOperationsAvailable(user.GetOperationsAvailable(bean.getDomain(), Phrase.AdministrationLoggerName));
        }
       */
      }
      catch (Exception e) {
        this.Log("NodeUsers.do: Could Not Get Set Web Page: " + e.toString(), Level.ERROR);
      }
    }
    else {
      this.ClearWebPage(bean, session);
      bean.setMessage("Database Error");
      this.Log("NodeUsers.do: User is null",Level.WARN);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void ClearWebPage (NodeUsersBean bean, HttpSession session)
  {
    this.Log("NodeUsers.do: Clearing Web Page",Level.DEBUG);
    bean.setAddress("");
    bean.setCity("");
    bean.setComments("");
    bean.setCountry("");
    bean.setEmail("");
    bean.setEmailError("");
    bean.setFirstName("");
    bean.setFirstNameError("");
    bean.setLastName("");
    bean.setLastNameError("");
    bean.setMidInitial("");
    bean.setOperationsAssigned(null);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      Operation[] ops = Operation.GetOperationsList(Phrase.AdministrationLoggerName,admin);
      ArrayList list = new ArrayList();
      if (ops != null) {
        for (int i = 0; i < ops.length; i++) {
          ArrayList temp = new ArrayList();
          temp.add("false");
          temp.add(ops[i].GetOperationID()+"");
          temp.add(ops[i].GetDomain());
          temp.add(ops[i].GetWebService());
          temp.add(ops[i].GetOpName());
          list.add(temp);
        }
      }
      bean.setOps(list);
    } catch (Exception e) {
      this.Log("Could Not Get Session User: " + e.toString(), Level.ERROR);
    }
    bean.setPhone("");
    bean.setState("");
    bean.setStatus("");
    bean.setSuppAddress("");
    bean.setTitle("");
    bean.setUserID("");
    bean.setUserType("");
    bean.setZipCode("");
  }

  /**
   * ClearErrors
   * @param bean
   * @return 
   */
  private void ClearErrors (NodeUsersBean bean)
  {
    bean.setEmailError("");
    bean.setFirstNameError("");
    bean.setLastNameError("");
    bean.setUserIDError("");
    bean.setUserTypeError("");
  }

  /**
   * DeleteUser
   * @param bean
   * @return 
   */
  private void DeleteUser (NodeUsersBean bean,HttpServletRequest request)
  {
	  User user = null;
	  try {
		  user = new User(bean.getUserID(),Phrase.AdministrationLoggerName);
		  if (user!= null && user.IsLocalNodeUser()){
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
		  }else if(user.IsNAASNodeUser()){
			  // WI 19722
			  String ret = null;
		      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
		      String adminUID = configDB.GetNodeAdminUID();
		      String adminPWD = configDB.GetNodeAdminPWD();
		      if (adminUID == null || adminPWD == null )
		        throw new RemoteException(Phrase.AccessRight);
		      NAASRequestor requestor = new NAASRequestor(Phrase.AdministrationLoggerName);
		      
		      String[] opArr = user.GetAssignedOperations(Phrase.AdministrationLoggerName);
		      boolean retbool = false;
		      if(opArr == null){
		    	  /*for(int i=0;i<opArr.length;i++){
		    		  String wsopName = opArr[i].split(":")[1];
		    		  String[] s = wsopName.split("\\.");
		    		  String wsName = s[0].trim();
		    		  String opName = s[1].trim();
		    		  ret = requestor.DeletePolicy(adminUID,adminPWD,user.GetLoginName(),this.ConvertWSNames(wsName),opName,null);		    		  		    		  
		    	  }*/
		    	  retbool = true;
		      }else if(opArr != null && opArr.length > 0){
				  bean.setMessage("Please remove user: " + user.GetLoginName() + " privileges before you want to delete it.");	
				  return;
		      }
		      /*if(bean.getOps().size()!=0){
		    	  Iterator it = bean.getOps().iterator();
		    	  while(it.hasNext()){
		    		  ArrayList al = (ArrayList)it.next();
		    		  ret = requestor.DeletePolicy(adminUID,adminPWD,user.GetLoginName(),this.ConvertWSNames((String)al.get(3)),(String)al.get(4),null);		    		  
		    	  }
		      }*/
		      if(retbool){
		    	  ret = requestor.DeleteUser(adminUID, adminPWD, user.GetLoginName());
				  bean.setMessage("NAAS User: " + user.GetLoginName() + " has been deleted from EPA but its log messages are still keeped in local Node database.");
		      }else{
				  bean.setMessage("User: " + user.GetLoginName() + " can not be deleted because fail to delete priviledges.");		    	  
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
  private User Save (NodeUsersBean bean, HttpServletRequest request)
  {
    this.Log("NodeUsers.do: Saving User "+bean.getUserID(),Level.DEBUG);
    User updatedUser = null;
    if (this.IsValidInput(bean)) {
      try {
        User user = new User(bean.getUserID(),Phrase.AdministrationLoggerName);
        user.SetAccountType(bean.getUserType());
        user.SetAssignedOperations(this.GetAssignedOperations(bean,request));
        if (!bean.getUserType().equals(Phrase.NAASNodeUser)) {
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
        }
        int result = -1;
        User admin = new User((String)request.getSession().getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        if (!bean.getTitle().equals("New Node User"))
          result = user.SaveUser(Phrase.AdministrationLoggerName, admin);
        else
          result = user.SaveNewUser(Phrase.AdministrationLoggerName, admin);
        if (result == User.SUCCESS) {
          updatedUser = user;
          if (!bean.getTitle().equals("New Node User"))
            bean.setMessage("Saved Successfully");
          else {
            String message = "Saved Successfully and New Password sent to ";
            if (bean.getUserType().equalsIgnoreCase(Phrase.NAASNodeUser))
              message += bean.getUserID();
            else
              message += bean.getEmail();
            bean.setMessage(message);
          }
        }
        else {
          if (result == User.DATABASE_ERROR)
            bean.setMessage("Database Error");
          else if (result == User.NAAS_ERROR)
            bean.setMessage("NAAS Integration Error");
          else if (result == User.EMAIL_ERROR)
              bean.setMessage("Create User successfully but send notify email Error.Please check email server setting or email sender account.");
          this.Log("NodeUsers.do: Could Not Save User "+bean.getUserID(),Level.WARN);
        }
      } catch (Exception e) {
        this.Log("Could Not Save Node User: " + e.toString(), Level.ERROR);
        bean.setMessage("Database Error");
      }
    }
    return updatedUser;
  }

  /**
   * IsValidInput
   * @param bean
   * @return boolean
   */
  private boolean IsValidInput (NodeUsersBean bean)
  {
    this.Log("Validating Input NodeUsers.do",Level.DEBUG);
    boolean isValid = true;
    String temp = bean.getUserID();
    String newUser = bean.getTitle();
    if (newUser.equals("New Node User") && temp == null || temp.equals("")) {
      bean.setUserIDError("Enter a User ID");
      isValid = false;
    }
    else if (newUser.equals("New Node User")) {
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
    temp = bean.getUserType();
    if (temp == null || !(temp.equals(Phrase.NAASNodeUser) || temp.equals(Phrase.LocalNodeUser))) {
      bean.setUserTypeError("Enter a User Type");
      isValid = false;
    }
    if (isValid && bean.getUserType().equals(Phrase.LocalNodeUser)) {
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
    }
    if (!isValid) {
      bean.setMessage("Fill in all Required Fields");
      this.Log("Invalid Input NodeUsers.do",Level.DEBUG);
    }
    else
      bean.setMessage("");
    return isValid;
  }

  /**
   * AddOperation
   * @param bean
   * @param request
   * @return 
   */
  private void AddOperation (NodeUsersBean bean, HttpServletRequest request)
  {
    this.Log("NodeUsers.do: Adding Operation",Level.DEBUG);
    String operation = bean.getOpAvailSel();
    if (operation != null && !operation.equals("")) {
      String[] avail = bean.getOperationsAvailable();
      if (avail == null || avail.length < 2)
        bean.setOperationsAvailable(null);
      else {
        String[] temp = new String [avail.length - 1];
        int index = 0;
        for (int i = 0; i < avail.length; i++) {
          if (!avail[i].equals(operation)) {
            temp[index] = avail[i];
            index++;
          }
        }
        bean.setOperationsAvailable(temp);
      }
      String[] assigned = bean.getOperationsAssigned();
      String[] newAssigned = null;
      if (assigned == null || assigned.length == 0)
        newAssigned = new String [1];
      else
        newAssigned = new String [assigned.length + 1];
      if (assigned != null)
        for (int i = 0; i < assigned.length; i++)
          newAssigned[i] = assigned[i];
      newAssigned[newAssigned.length - 1] = operation;
      bean.setOperationsAssigned(newAssigned);
    }
  }

  /**
   * RemoveOperation
   * @param bean
   * @param request
   * @return 
   */
  private void RemoveOperation (NodeUsersBean bean, HttpServletRequest request)
  {
    this.Log("NodeUsers.do: Removing Operation(s)",Level.DEBUG);
    String operation = bean.getOpAssSel();
    if (operation != null && !operation.equals("")) {
      String[] assigned = bean.getOperationsAssigned();
      if (assigned == null || assigned.length < 2)
        bean.setOperationsAssigned(null);
      else {
        String[] temp = new String [assigned.length - 1];
        int index = 0;
        for (int i = 0; i < assigned.length; i++) {
          if (!assigned[i].equals(operation)) {
            temp[index] = assigned[i];
            index++;
          }
        }
        bean.setOperationsAssigned(temp);
      }
      String[] avail = bean.getOperationsAvailable();
      String[] newAvail = null;
      if (avail == null || avail.length == 0)
        newAvail = new String [1];
      else
        newAvail = new String [avail.length + 1];
      for (int i = 0; i < newAvail.length-1; i++)
        newAvail[i] = avail[i];
      newAvail[newAvail.length - 1] = operation;
      bean.setOperationsAvailable(newAvail);
    }
  }

  /**
   * ChangePassword
   * @param bean
   * @return 
   */
  private void ChangePassword (NodeUsersBean bean)
  {
    this.Log("NodeUsers.do: Changing Password for User "+bean.getUserID(),Level.DEBUG);
    try {
      User temp = new User (bean.getUserID(),Phrase.AdministrationLoggerName);
      temp.SetAccountType(bean.getUserType());
      if (temp.ChangePWD(Phrase.AdministrationLoggerName))
        bean.setMessage("Password Sent to User " + bean.getUserID());
      else {
        bean.setMessage("Failed Password Generation,Please check database, Email server or user Email account.");
        this.Log("NodeUsers.do: Could Not Change Password",Level.WARN);
      }
    } catch (Exception e) {
      this.Log("Could Not Change Password: " + e.toString(), Level.ERROR);
      bean.setMessage("Password Could Not Be Sent");
    }
  }

  /**
   * ChangeDomain
   * @param bean
   * @return 
   */
  private void ChangeDomain (NodeUsersBean bean)
  {
    this.Log("NodeUsers.do: Changing Domain",Level.DEBUG);
    try {
      User user = new User(bean.getUserID(), Phrase.AdministrationLoggerName);
      bean.setOperationsAvailable(user.GetOperationsAvailable(bean.getDomain(),Phrase.AdministrationLoggerName));
    } catch (Exception e) {
      this.Log("NodeUser.do: Could Not Change Domain: " + e.toString(),Level.ERROR);
    }
  }

  /**
   * GetAssignedOperations
   * @param bean
   * @param request
   * @return String[]
   */
  private String[] GetAssignedOperations (NodeUsersBean bean, HttpServletRequest request)
  {
    String[] retArray = null;
    ArrayList list = bean.getOps();
    HashMap map = new HashMap();
    if (list != null) {
      for (int i = 0; i < list.size(); i++) {
        ArrayList temp = (ArrayList)list.get(i);
        if (temp != null && temp.size() >= 5) {
          String id = (String)temp.get(1);
          if (id != null && !id.equals("")) {
            String test = request.getParameter("operation"+id);
            if (test != null && test.equalsIgnoreCase("on"))
              map.put(id,temp.get(2)+": "+temp.get(3)+"."+temp.get(4));
          }
        }
      }
    }
    if (!map.isEmpty()) {
      Object[] temp = map.values().toArray();
      if (temp != null && temp.length > 0) {
        retArray = new String[temp.length];
        for (int i = 0; i < temp.length; i++)
          retArray[i] = (String)temp[i];
      }
    }
    return retArray;
  }
  
  /**
   * Convert WSNames.
   * @param input
   * @return webservice Name
   */
  private String ConvertWSNames (String input)
  {
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_ANY))
      return NAASRequestor.METHOD_ANY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_AUTHENTICATE))
      return NAASRequestor.METHOD_AUTHENTICATE;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_DOWNLOAD))
      return NAASRequestor.METHOD_DOWNLOAD;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_GETSERVICES))
      return NAASRequestor.METHOD_GETSERVICES;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_GETSTATUS))
      return NAASRequestor.METHOD_GETSTATUS;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_NOTIFY))
      return NAASRequestor.METHOD_NOTIFY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_QUERY))
      return NAASRequestor.METHOD_QUERY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_SOLICIT))
      return NAASRequestor.METHOD_SOLICIT;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_SUBMIT))
      return NAASRequestor.METHOD_SUBMIT;
    return null;
  }
}
