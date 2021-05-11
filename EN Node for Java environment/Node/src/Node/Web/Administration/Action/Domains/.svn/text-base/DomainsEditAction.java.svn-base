package Node.Web.Administration.Action.Domains;

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
import Node.DB.Interfaces.INodeDomain;
import Node.Phrase;
import Node.Web.Administration.Bean.Domains.DomainsEditBean;
import Node.Web.Administration.BaseAction;
/**
 * <p>This class create DomainsEditAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DomainsEditAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public DomainsEditAction() {
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
    this.Log("Executing DomainsEdit.do",Level.INFO);
    DomainsEditBean bean = (DomainsEditBean)form;
    bean.setMessage("");
    this.ClearErrors(bean);
    String newDomain = request.getParameter("new");
    
    // WI 19929
    ActionForward af = null;
    if (newDomain != null && newDomain.equals("true")) {
      this.ClearWebPage(bean,request.getSession());
      bean.setTitle("New Domain");
    }
    String existingDomain = request.getParameter("domainid");
    // WI 19929
    if(existingDomain == null || existingDomain.equals(""))
    	existingDomain = bean.getId();
    if (existingDomain != null && !existingDomain.equals("")) {
        Domain domain = new Domain(Integer.parseInt(existingDomain),Phrase.AdministrationLoggerName);
        this.SetWebPage(bean,domain,request.getSession());
    }
    
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("DomainsEdit.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("SAVE")) {
        Domain d = this.Save(bean, request);
        if (d != null){
          int id = d.GetDomainID();
          if(id==-1){
        	  INodeDomain nd = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
        	  id = nd.GetDomainID(d.GetDomainName().toUpperCase());
        	  bean.setId(id+"");
          }
          this.SetWebPage(bean,d,request.getSession());
        }
      }
      if (act.equalsIgnoreCase("ADD_ADMIN"))
        this.AddAdmin(bean);
      if (act.equalsIgnoreCase("REMOVE_ADMIN"))
        this.RemoveAdmin(bean);
    }
    if(af == null)
    	return mapping.findForward("update");
    else return af;
  }

  /**
   * AddAdmin
   * @param bean
   * @return 
   */
  private void AddAdmin (DomainsEditBean bean)
  {
    this.Log("DomainsEdit.do: Adding Admin",Level.DEBUG);
    String domain = bean.getAdminAvailSel();
    if (domain != null && !domain.equals("")) {
      String[] avail = bean.getAdminsAvailable();
      if (avail == null || avail.length < 2)
        bean.setAdminsAvailable(null);
      else {
        String[] temp = new String [avail.length - 1];
        int index = 0;
        for (int i = 0; i < avail.length; i++) {
          if (!avail[i].equals(domain)) {
            temp[index] = avail[i];
            index++;
          }
        }
        bean.setAdminsAvailable(temp);
      }
      String[] assigned = bean.getAdminsAssigned();
      String[] newAssigned = null;
      if (assigned == null || assigned.length == 0)
        newAssigned = new String [1];
      else
        newAssigned = new String [assigned.length + 1];
      if (assigned != null)
        for (int i = 0; i < newAssigned.length-1; i++)
          newAssigned[i] = assigned[i];
      newAssigned[newAssigned.length - 1] = domain;
      bean.setAdminsAssigned(newAssigned);
    }
  }

  /**
   * RemoveAdmin
   * @param bean
   * @return 
   */
  private void RemoveAdmin (DomainsEditBean bean)
  {
    this.Log("DomainsEdit.do: Removing Admin",Level.DEBUG);
    String domain = bean.getAdminAssignSel();
    if (domain != null && !domain.equals("")) {
      String[] assigned = bean.getAdminsAssigned();
      if (assigned == null || assigned.length < 2)
        bean.setAdminsAssigned(null);
      else {
        String[] temp = new String [assigned.length - 1];
        int index = 0;
        for (int i = 0; i < assigned.length; i++) {
          if (!assigned[i].equals(domain)) {
            temp[index] = assigned[i];
            index++;
          }
        }
        bean.setAdminsAssigned(temp);
      }
      String[] avail = bean.getAdminsAvailable();
      String[] newAvail = null;
      if (avail == null || avail.length == 0)
        newAvail = new String [1];
      else
        newAvail = new String [avail.length + 1];
      for (int i = 0; i < newAvail.length-1; i++)
        newAvail[i] = avail[i];
      newAvail[newAvail.length - 1] = domain;
      bean.setAdminsAvailable(newAvail);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void ClearWebPage (DomainsEditBean bean, HttpSession session)
  {
    this.Log("DomainsEdit.do: Clear Web Page",Level.DEBUG);
    bean.setAllowDownload("");
    bean.setAllowNotify("");
    bean.setAllowQuery("");
    bean.setAllowSolicit("");
    bean.setAllowSubmit("");
    bean.setDescription("");
    bean.setMessage("");
    bean.setName("");
    bean.setNameError("");
    bean.setStatus("Running");
    bean.setStatusMsg("");
    bean.setTitle("");
    bean.setAdminsAssigned(null);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      bean.setIsNodeAdmin(admin.IsNodeAdmin());
      User[] admins = User.GetConsoleUsers(Phrase.AdministrationLoggerName);
      ArrayList list = null;
      if (admins != null && admins.length > 0) {
        list = new ArrayList();
        for (int i = 0; i < admins.length; i++) {
          ArrayList temp = new ArrayList();
          temp.add("false");
          temp.add(admins[i].GetUserID()+"");
          temp.add(admins[i].GetLoginName());
          list.add(temp);
        }
      }
      bean.setAdmins(list);
    } catch (Exception e) {
      this.Log("Could Not Set Admins Available: " + e.toString(),Level.ERROR);
    }
  }

  /**
   * ClearErrors
   * @param bean
   * @param 
   * @return 
   */
  private void ClearErrors (DomainsEditBean bean)
  {
    bean.setNameError("");
  }

  /**
   * SetWebPage
   * @param bean
   * @param domain
   * @param session
   * @return 
   */
  private void SetWebPage (DomainsEditBean bean, Domain domain, HttpSession session)
  {
    this.Log("DomainsEdit.do: Setting Web Page Domain "+domain!=null?domain.GetDomainName():"",Level.DEBUG);
    if (domain != null) {
      if (domain.GetAllowSubmit())
        bean.setAllowSubmit("on");
      else
        bean.setAllowSubmit("");
      if (domain.GetAllowDownload())
        bean.setAllowDownload("on");
      else
        bean.setAllowDownload("");
      if (domain.GetAllowQuery())
        bean.setAllowQuery("on");
      else
        bean.setAllowQuery("");
      if (domain.GetAllowSolicit())
        bean.setAllowSolicit("on");
      else
        bean.setAllowSolicit("");
      if (domain.GetAllowNotify())
        bean.setAllowNotify("on");
      else
        bean.setAllowNotify("");
      bean.setDescription(domain.GetDomainDesc());
      bean.setName(domain.GetDomainName());
      String status = domain.GetDomainStatusCD();
      if (status != null && status.equalsIgnoreCase(Phrase.INACTIVE_STATUS)) {
        bean.setStatus(Phrase.STOPPED_STATUS);
        bean.setActiveStatus(Phrase.INACTIVE_STATUS);
      }
      else {
        bean.setStatus(status);
        bean.setActiveStatus(Phrase.ACTIVE_STATUS);
      }
      bean.setStatusMsg(domain.GetDomainStatusMsg());
      try {
        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        bean.setIsNodeAdmin(admin.IsNodeAdmin());
        User[] admins = User.GetConsoleUsers(Phrase.AdministrationLoggerName);
        int[] adminIDs = domain.GetAdminIDs(Phrase.AdministrationLoggerName);
        HashMap map = new HashMap();
        if (adminIDs != null)
          for (int i = 0; i < adminIDs.length; i++)
            map.put(adminIDs[i]+"",adminIDs[i]+"");
        ArrayList list = null;
        if (admins != null && admins.length > 0) {
          list = new ArrayList();
          for (int i = 0; i < admins.length; i++) {
            ArrayList temp = new ArrayList();
            temp.add(map.containsKey(admins[i].GetUserID()+"")+"");
            temp.add(admins[i].GetUserID()+"");
            temp.add(admins[i].GetLoginName());
            list.add(temp);
          }
        }
        bean.setAdmins(list);
      } catch (Exception e) {
        this.Log("Could Not Set Admins Available: " + e.toString(),Level.ERROR);
      }
      bean.setTitle(domain.GetDomainName());
    }
    else {
      bean.setMessage("Database Error");
      this.Log("DomainsEdit.do: Domain is null",Level.WARN);
    }
  }

  /**
   * Save
   * @param bean
   * @param request
   * @return Domain
   */
  private Domain Save (DomainsEditBean bean, HttpServletRequest request)
  {
    this.Log("DomainsEdit.do: Saving Domain "+bean.getName(),Level.DEBUG);
    Domain updatedDomain = null;
    if (this.IsValidInput(bean,request)) {
      try {
        Domain domain = new Domain(bean.getName(),Phrase.AdministrationLoggerName);
        if (request.getParameter("allowDownload") != null && request.getParameter("allowDownload").equals("on"))
          domain.SetAllowDownload(true);
        else
          domain.SetAllowDownload(false);
        if (request.getParameter("allowNotify") != null && request.getParameter("allowNotify").equals("on"))
          domain.SetAllowNotify(true);
        else
          domain.SetAllowNotify(false);
        if (request.getParameter("allowQuery") != null && request.getParameter("allowQuery").equals("on"))
          domain.SetAllowQuery(true);
        else
          domain.SetAllowQuery(false);
        if (request.getParameter("allowSolicit") != null && request.getParameter("allowSolicit").equals("on"))
          domain.SetAllowSolicit(true);
        else
          domain.SetAllowSolicit(false);
        if (request.getParameter("allowSubmit") != null && request.getParameter("allowSubmit").equals("on"))
          domain.SetAllowSubmit(true);
        else
          domain.SetAllowSubmit(false);
        domain.SetAdmins(this.GetAdmins(bean,request));
        domain.SetDomainDesc(bean.getDescription());
        String active = bean.getActiveStatus();
        if (active.equals(Phrase.INACTIVE_STATUS))
          domain.SetDomainStatusCD(Phrase.INACTIVE_STATUS);
        else
          domain.SetDomainStatusCD(bean.getStatus());
        domain.SetDomainStatusMsg(bean.getStatusMsg());
        if (domain.Save(Phrase.AdministrationLoggerName)) {
          updatedDomain = domain;
          bean.setMessage("Save Sucessfully");
        }
        else {
          bean.setMessage("Unsuccessful Save");
          this.Log("DomainsEdit.do: Could Not save Domain "+bean.getName(),Level.DEBUG);
        }
      } catch (Exception e) {
        this.Log("Could Not Save Domain: " + e.toString(),Level.ERROR);
        bean.setMessage("Database Error");
      }
    }
    return updatedDomain;
  }

  /**
   * IsValidInput
   * @param bean
   * @return boolean
   */
  private boolean IsValidInput (DomainsEditBean bean, HttpServletRequest request)
  {
    this.Log("Validating Input DomainsEdit.do",Level.DEBUG);
    boolean retBool = true;
    String temp = bean.getName();
    if (bean.getTitle() != null && bean.getTitle().equals("New Domain") && temp == null || temp.equals("")) {
      bean.setNameError("Enter a Domain Name");
      retBool = false;
    }else if (bean.getTitle() != null && bean.getTitle().equals("New Domain")) {
      try {
        if (!Domain.IsUniqueName(Phrase.AdministrationLoggerName, temp)) {
          bean.setNameError("Enter a New Domain Name");
          retBool = false;
        }
      } catch (Exception e) {
        this.Log("Could Not Check Unique Domain Name: " + e.toString(),Level.ERROR);
        bean.setNameError("Enter a New Domain Name");
        retBool = false;
      }
    }else if (bean.getName() != null && bean.getName().length()>50) {
    	bean.setMessage("The length of Domain Name is no longer than 50 characters.");
    	retBool = false;
    }else if (bean.getDescription()!= null && bean.getDescription().length()>100) {
    	bean.setMessage("The length of Domain Description is no longer than 100 characters.");
    	retBool = false;
    }else if (bean.getName() != null && bean.getName().length()>1000) {
    	bean.setMessage("The length of Domain Description is no longer than 1000 characters.");
    	retBool = false;
    }
    // WI 19929
    if(request.getParameter("allowDownload") != null && request.getParameter("allowDownload").equals("on")
    || request.getParameter("allowNotify") != null && request.getParameter("allowNotify").equals("on")
    || request.getParameter("allowQuery") != null && request.getParameter("allowQuery").equals("on")
    || request.getParameter("allowSolicit") != null && request.getParameter("allowSolicit").equals("on")
    || request.getParameter("allowSubmit") != null && request.getParameter("allowSubmit").equals("on")){
    }else{
    	bean.setMessage("Please select at least one web service.");
    	retBool = false;
    }
    	
    
    if (!retBool) {
      //bean.setMessage("Fill in all fields as required standard.");
      this.Log("Invalid Input DomainsEdit.do",Level.DEBUG);
    }
    else
      bean.setMessage("");
    return retBool;
  }

  /**
   * GetAdmins
   * @param bean
   * @param request
   * @return String[]
   */
  private String[] GetAdmins (DomainsEditBean bean, HttpServletRequest request)
  {
    String[] retArray = null;
    HashMap map = new HashMap();
    ArrayList list = bean.getAdmins();
    if (list != null) {
      for (int i = 0; i < list.size(); i++) {
        ArrayList temp = (ArrayList)list.get(i);
        if (temp != null && temp.size() > 2) {
          String test = request.getParameter("admin"+temp.get(1));
          if (test != null && test.equalsIgnoreCase("on"))
            map.put(temp.get(2)+"",temp.get(2)+"");
        }
      }
    }
    if (!map.isEmpty()) {
      Object[] temp = map.values().toArray();
      if (temp != null && temp.length > 0) {
        retArray = new String [temp.length];
        for (int i = 0; i < temp.length; i++)
          retArray[i] = (String)temp[i];
      }
    }
    return retArray;
  }
}
