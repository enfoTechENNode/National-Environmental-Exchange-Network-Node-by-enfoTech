package Node.Web.Administration.Action.Entry;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Entry.LeftBean;
/**
 * <p>This class create LeftAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LeftAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public LeftAction() {
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
    this.Log("Executing Left.do",Level.INFO);
    LeftBean bean = (LeftBean)form;
    HttpSession session = request.getSession();
    User admin = new User ((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
    boolean nodeAdmin = admin.IsNodeAdmin();
    bean.setIsNodeAdmin(nodeAdmin);
    if (nodeAdmin)
      bean.setIsNJNEIAdmin(true);
    else {
      boolean isNEIAdmin = false;
      String[] domains = admin.GetAssignedDomains();
      if (domains != null)
        for (int i = 0; i < domains.length; i++)
          if (domains[i] != null && domains[i].equalsIgnoreCase("NEI"))
            isNEIAdmin = true;
      bean.setIsNJNEIAdmin(isNEIAdmin);
    }
    return mapping.findForward("left");
  }
}
