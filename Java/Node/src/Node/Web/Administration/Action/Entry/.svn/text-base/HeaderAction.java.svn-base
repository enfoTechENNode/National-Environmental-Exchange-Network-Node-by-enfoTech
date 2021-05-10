package Node.Web.Administration.Action.Entry;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Entry.HeaderBean;
/**
 * <p>This class create HeaderAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class HeaderAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public HeaderAction() {
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
    this.Log("Executing Header.do",Level.INFO);
    HeaderBean bean = (HeaderBean)form;
    bean.setUserName((String)request.getSession().getAttribute(Phrase.USER_SESSION));
    return mapping.findForward("header");
  }
}
