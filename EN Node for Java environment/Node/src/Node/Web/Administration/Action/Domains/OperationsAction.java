package Node.Web.Administration.Action.Domains;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Operation;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Domains.OperationsBean;
/**
 * <p>This class create OperationsAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationsAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public OperationsAction() {
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
    this.Log("Executing Operations.do",Level.INFO);
    OperationsBean bean = (OperationsBean)form;
    String domain = request.getParameter("domain");
    String version = request.getParameter("version");
    if (domain != null && !domain.equals("")) {
      this.ClearWebPage(bean);
      bean.setDomain(domain);
      request.getSession().setAttribute(Phrase.DOMAIN_SESSION,domain);
      if(version!=null && !version.equalsIgnoreCase("")){
          request.getSession().setAttribute(Phrase.NodeVersion,version);    	  
      }
    }
    else if (bean.getDomain() == null || bean.getDomain().equals(""))
      return mapping.findForward("failed");
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("Operations.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("NEW_OPERATION"))
        return mapping.findForward("create");
      else if(act.equalsIgnoreCase("WIZARD"))
          return mapping.findForward("wizard");
    }
    if (request.getMethod().equalsIgnoreCase("GET"))
    	bean.setStatus("Active");
    this.SearchOperations(bean,(String)request.getSession().getAttribute(Phrase.NodeVersion));
    bean.setVersion((String)request.getSession().getAttribute(Phrase.NodeVersion));
    return mapping.findForward("search");
  }

  /**
   * SearchOperations
   * @param bean
   * @param version
   * @return 
   */
  private void SearchOperations (OperationsBean bean,String version)
  {
    this.Log("Operations.do: Search Operations",Level.DEBUG);
    try {
      bean.setOperationList(Operation.SearchOperations(Phrase.AdministrationLoggerName,bean.getDomain(),bean.getOperationsName(),bean.getOperationsType(),bean.getWebMethod(),bean.getStatus(),version));
    } catch (Exception e) {
      this.Log("Could Not Search Operations: " + e.toString(),Level.ERROR);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @return 
   */
  private void ClearWebPage (OperationsBean bean)
  {
    this.Log("Operations.do: Clearing Web Page",Level.DEBUG);
    bean.setDomain("");
    bean.setOperationList(null);
    bean.setOperationsName("");
    bean.setOperationsType("");
    bean.setStatus("");
    bean.setWebMethod("");
  }
}
