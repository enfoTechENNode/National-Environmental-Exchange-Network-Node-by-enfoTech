package Node.Web.Administration.Action.Users;

import java.io.PrintWriter;

import org.apache.log4j.Level;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Users.UsersBean;
/**
 * <p>This class create UsersAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class UsersAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public UsersAction() {
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
	User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
    this.Log("Executing Users.do",Level.INFO);
    UsersBean bean = (UsersBean)form;
    String act = request.getParameter("act");
    bean.setAct(act);
    boolean isSearchable = true;
    if(user.IsNodeAdmin()){
        if (act != null && !act.equals("")) {
            this.Log("Users.do act = " + act,Level.DEBUG);
            if (act.equalsIgnoreCase("NEW_NODE_USER"))
              return mapping.findForward("newNodeUser");
            if (act.equalsIgnoreCase("NEW_ADMIN_CONSOLE_USER"))
              return mapping.findForward("newConsoleUser");
            if (act.equalsIgnoreCase("INITIAL"))
              isSearchable = false;
          }
          this.SetWebPage(bean, request.getSession());
          if (isSearchable)
            this.Search(bean);
          return mapping.findForward("search");    	
    }else{
        printMsgToClient("Only Administrator group users can change user Configuration. ", response);
        return null;
    }
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (UsersBean bean, HttpSession session)
  {
    this.Log("Users.do: Setting Web Page",Level.DEBUG);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION), Phrase.AdministrationLoggerName);
      bean.setDomains(admin.GetDomainsAvailable(null,Phrase.AdministrationLoggerName));
    } catch (Exception e) {
      this.Log("Could Not Pre-Populate Users Seach Page: " + e.toString(), Level.ERROR);
    }
  }

  /**
   * Search
   * @param bean
   * @return 
   */
  private void Search (UsersBean bean)
  {
    this.Log("Users.do: Searching Users",Level.DEBUG);
    try {
      User[][] users = null;
      if (bean.getShowNAAS().equals("on")) {
        this.Log("Users.do: Searching All NAAS Users", Level.DEBUG);
        users = User.SearchUsers(Phrase.AdministrationLoggerName,bean.getLogin(),bean.getType(),bean.getDomain(),bean.getFirstName(),bean.getLastName(),true);
      }
      else {
        this.Log("Users.do: Searching NAAS Users with Privileges",Level.DEBUG);
        users = User.SearchUsers(Phrase.AdministrationLoggerName,bean.getLogin(),bean.getType(),bean.getDomain(),bean.getFirstName(),bean.getLastName(),false);
      }
      if (users != null && users.length == 2) {
        if (users[1] == null)
          bean.setMessage("NAAS Server Currently Unavailable");
        bean.setUserList(users[0]);
        bean.setNaasUserList(users[1]);
      }
      else {
        bean.setUserList(null);
        bean.setNaasUserList(null);
      }
    } catch (Exception e) {
      this.Log("Could Not Search Users Database: " +e.toString(), Level.ERROR);
    }
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
