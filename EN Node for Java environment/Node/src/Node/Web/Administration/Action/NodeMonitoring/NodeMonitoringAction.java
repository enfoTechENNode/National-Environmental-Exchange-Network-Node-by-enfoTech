package Node.Web.Administration.Action.NodeMonitoring;

import java.sql.Date;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.TimeZone;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.OperationLog;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.NodeMonitoring.NodeMonitoringBean;
/**
 * <p>This class create NodeMonitoringAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeMonitoringAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public NodeMonitoringAction() {
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
    this.Log("Executing NodeMonitoring.do",Level.INFO);
    NodeMonitoringBean bean = (NodeMonitoringBean)form;
    String back = request.getParameter("back");
    if (back != null && back.equals("true"))
      ;
    else if (request.getMethod().equalsIgnoreCase("GET"))
      this.ClearWebPage(bean);
    this.SetWebPage(bean,request.getSession());
    this.Search(bean,request.getSession());
    return mapping.findForward("search");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (NodeMonitoringBean bean, HttpSession session)
  {
    this.Log("NodeMonitoring.do: Setting Web Page",Level.DEBUG);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      bean.setDomainList(admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName));
      bean.setStatusList(OperationLog.GetUniqueStatusList(Phrase.AdministrationLoggerName,admin));
      bean.setOpNameList(OperationLog.GetUniqueOperationNameList(Phrase.AdministrationLoggerName,admin));
    } catch (Exception e) {
      this.Log("NodeMonitoring.do: Could Not Set Web Page: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @return 
   */
  private void ClearWebPage (NodeMonitoringBean bean)
  {
    bean.setOpName("");
    bean.setOpType("");
    bean.setWebService("");
    bean.setStatus("");
    bean.setDomain("");
    bean.setUserID("");
    bean.setToken("");
    bean.setTransID("");
    this.SetCalendar(bean);
  }

  /**
   * Search
   * @param bean
   * @param session
   * @return 
   */
  private void Search (NodeMonitoringBean bean, HttpSession session)
  {
    this.Log("NodeMonitoring.do: Searching Logs",Level.DEBUG);
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      String[] domains = null;
      String domain = bean.getDomain();
      if (domain != null && !domain.equals(""))
        domains = new String[] { domain };
      Date start = null;
      Date end = null;
      if (bean.getStart() != null && !bean.getStart().equals(""))
        start = Date.valueOf(bean.getStart());
      if (bean.getEnd() != null && !bean.getEnd().equals(""))
        end = Date.valueOf(bean.getEnd());
      String version = (String)session.getAttribute(Phrase.NodeVersion);
      bean.setSearchedLogs(OperationLog.SearchOperationLog(Phrase.AdministrationLoggerName,admin,bean.getOpName(),bean.getOpType(),
          bean.getWebService(),bean.getStatus(),domains,bean.getUserID(),bean.getToken(),bean.getTransID(),start,end, version));
    } catch (Exception e) {
      this.Log("NodeMonitoring.do: Could Not Search Logs: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * SetCalendar
   * @param bean
   * @return 
   */
  private void SetCalendar (NodeMonitoringBean bean)
  {
    SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
    Calendar cal = Calendar.getInstance(TimeZone.getDefault());
    bean.setEnd(format.format(cal.getTime()));
    //cal.add(Calendar.DAY_OF_MONTH,-7);
    bean.setStart(format.format(cal.getTime()));
  }
}
