package Node.Web.Administration.Action.Domains;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Domain;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.Bean.Domains.DomainsBean;
import Node.Web.Administration.BaseAction;
/**
 * <p>This class create DomainsAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DomainsAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public DomainsAction() {
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
    this.Log("Executing Domains.do",Level.INFO);
    DomainsBean bean = (DomainsBean)form;
    if (request.getMethod().equalsIgnoreCase("GET"))
      bean.setStatus("Active");
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("Domains.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("NEW_DOMAIN"))
        return mapping.findForward("create");
      if (act.equalsIgnoreCase("OPERATIONS"))
        return mapping.findForward("operations");
    }
    this.SetWebPage(bean,request.getSession());
    this.SearchPage(bean,request.getSession());
    return mapping.findForward("search");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (DomainsBean bean, HttpSession session)
  {
    this.Log("Domains.do: Setting Web Page",Level.DEBUG);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION), Phrase.AdministrationLoggerName);
      bean.setSelDomainList(admin.GetDomainsAvailable(null,Phrase.AdministrationLoggerName));
      bean.setIsNodeAdmin(admin.IsNodeAdmin());
    } catch (Exception e) {
      this.Log("Could Not Pre-Populate Domains Seach Page: " + e.toString(), Level.ERROR);
    }
  }

  /**
   * SearchPage
   * @param bean
   * @param session
   * @return 
   */
  private void SearchPage (DomainsBean bean, HttpSession session)
  {
    this.Log("Domains.do: Searching",Level.DEBUG);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      bean.setDomainList(Domain.SearchDomains(Phrase.AdministrationLoggerName,admin,bean.getDomainName(),bean.getStatus()));
    } catch (Exception e) {
      this.Log("Could Not Search Page: " + e.toString(), Level.ERROR);
    }
  }
}
