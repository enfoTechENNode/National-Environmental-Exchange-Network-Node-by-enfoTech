package Node.Web.WebServices.Action;

import Node.Web.WebServices.BaseAction;

import java.util.ArrayList;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Status;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.WebServices.Bean.StatusBean;
/**
 * <p>This class create StatusAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class StatusAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public StatusAction() {
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse resonse) throws Exception
  {
    StatusBean bean = (StatusBean)form;
    bean.setMessage("");
    String userName = (String)request.getSession().getAttribute("statusUser");
    String selDomain = request.getParameter("selDomain");
    String act = request.getParameter("act");
    if (userName != null && !userName.equals("")) {
      this.ClearWebPage(bean);
      this.SetWebPage(bean,new Status(Phrase.WebServicesLoggerName));
      bean.setUserName(userName);
      request.getSession().removeAttribute("statusUser");
    }
    else if (selDomain != null && !selDomain.equals(""))
      bean.setSelDomain(selDomain);
    else if (act != null && !act.equals(""))
      if (act.equalsIgnoreCase("CHANGE_PWD"))
        this.ChangePWD(bean);
    else
      return mapping.findForward("back");
    return mapping.findForward("view");
  }

  /**
   * SetWebPage
   * @param bean
   * @param status
   * @return 
   */
  private void SetWebPage (StatusBean bean, Status status)
  {
    if (status != null) {
      ArrayList list = status.GetDomains();
      if (list != null && list.size() > 0) {
        ArrayList temp = (ArrayList)list.get(0);
        if (temp != null && temp.size() > 0)
          bean.setSelDomain((String)temp.get(0));
      }
      bean.setDomains(list);
      bean.setOperations(status.GetOperations());
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @return 
   */
  private void ClearWebPage (StatusBean bean)
  {
    bean.setDomains(null);
    bean.setMessage("");
    bean.setNewPWD1("");
    bean.setNewPWD2("");
    bean.setOperations(null);
    bean.setSelDomain("");
    bean.setUserName("");
  }

  /**
   * ChangePWD
   * @param bean
   * @return 
   */
  private void ChangePWD (StatusBean bean)
  {
    try {
      String newPWD1 = bean.getNewPWD1();
      String newPWD2 = bean.getNewPWD2();
      if (newPWD1 != null && newPWD1.equals(newPWD2)) {
        User user = new User(bean.getUserName(), Phrase.WebServicesLoggerName);
        if (user.GetUserID() < 0)
          user.SetAccountType(Phrase.NAASNodeUser);
        if (user.ChangePWD(Phrase.WebServicesLoggerName, newPWD1))
          bean.setMessage("Successful Password Change");
        else
          bean.setMessage("Error Changing Password");
      }
      else
        bean.setMessage("Enter in valid Old Passwords and a new Password");
    } catch (Exception e) {
      this.Log("Could Not Change Password: "+e.toString(),Level.ERROR);
    }
  }
}
