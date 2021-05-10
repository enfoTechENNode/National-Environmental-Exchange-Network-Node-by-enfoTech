package Node.Web.Administration.Action.NodeMonitoring;

import java.util.ArrayList;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.OperationLog;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.NodeMonitoring.TransactionViewBean;
/**
 * <p>This class create TransactionViewAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class TransactionViewAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public TransactionViewAction() {
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
    this.Log("Executing TransactionView.do",Level.INFO);
    TransactionViewBean bean = (TransactionViewBean)form;
    String existingLog = request.getParameter("opLogID");
    if (existingLog != null && !existingLog.equals("")) {
      OperationLog log = new OperationLog(Integer.parseInt(existingLog),Phrase.AdministrationLoggerName);
      this.SetWebPage(bean,log);
    }
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("TransactionView.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("BACK"))
        return mapping.findForward("back?back=true");
    }
    return mapping.findForward("view");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (TransactionViewBean bean, OperationLog log)
  {
    if (log != null) {
      ArrayList details = new ArrayList();
      String opName = log.GetOperationName();
      if (opName != null) {
        ArrayList temp = new ArrayList();
        temp.add("opName");
        temp.add("Operation Name");
        temp.add(opName);
        details.add(temp);
      }
      String transID = log.GetTransID();
      if (transID != null) {
        ArrayList temp = new ArrayList();
        temp.add("transID");
        temp.add("Transaction ID");
        temp.add(transID);
        details.add(temp);
      }
      String opType = log.GetOperationType();
      if (opType != null) {
        ArrayList temp = new ArrayList();
        temp.add("opType");
        temp.add("Operation Type");
        temp.add(opType);
        details.add(temp);
      }
      String wsName = log.GetWebServiceName();
      if (wsName != null) {
        ArrayList temp = new ArrayList();
        temp.add("wsName");
        temp.add("Web Service Name");
        temp.add(wsName);
        details.add(temp);
      }
      String domain = log.GetDomain();
      if (domain != null) {
        ArrayList temp = new ArrayList();
        temp.add("domain");
        temp.add("Domain");
        temp.add(domain);
        details.add(temp);
      }
      String userName = log.GetUserName();
      if (userName != null) {
        ArrayList temp = new ArrayList();
        temp.add("userName");
        temp.add("User Name");
        temp.add(userName);
        details.add(temp);
      }
      String reqIP = log.GetRequestorIP();
      if (reqIP != null) {
        ArrayList temp = new ArrayList();
        temp.add("reqIP");
        temp.add("Requestor IP Address");
        temp.add(reqIP);
        details.add(temp);
      }
      String hostName = log.GetHostName();
      if (hostName != null) {
        ArrayList temp = new ArrayList();
        temp.add("hostName");
        temp.add("Host Name");
        temp.add(hostName);
        details.add(temp);
      }
      String token = log.GetToken();
      if (token != null) {
        ArrayList temp = new ArrayList();
        temp.add("token");
        temp.add("Security Token");
        temp.add(token);
        details.add(temp);
      }
      String suppTransID = log.GetSupplTransID();
      if (suppTransID != null) {
        ArrayList temp = new ArrayList();
        temp.add("suppTransID");
        temp.add("Supplied Transaction ID");
        temp.add(suppTransID);
        details.add(temp);
      }
      String nodeAddress = log.GetNodeAddress();
      if (nodeAddress != null) {
        ArrayList temp = new ArrayList();
        temp.add("nodeAddress");
        temp.add("Node Address");
        temp.add(nodeAddress);
        details.add(temp);
      }
      String retURL = log.GetReturnURL();
      if (retURL != null) {
        ArrayList temp = new ArrayList();
        temp.add("retURL");
        temp.add("Return URL");
        temp.add(retURL);
        details.add(temp);
      }
      String servType = log.GetServiceType();
      if (servType != null) {
        ArrayList temp = new ArrayList();
        temp.add("servType");
        temp.add("Service Type");
        temp.add(servType);
        details.add(temp);
      }
      String startDate = log.GetStartDate();
      if (startDate != null) {
        ArrayList temp = new ArrayList();
        temp.add("startDate");
        temp.add("Start Date");
        temp.add(startDate);
        details.add(temp);
      }
      String endDate = log.GetEndDate();
      if (endDate != null) {
        ArrayList temp = new ArrayList();
        temp.add("endDate");
        temp.add("End Date");
        temp.add(endDate);
        details.add(temp);
      }
      bean.setDetails(details);
      bean.setParameters(log.GetParameters());
      ArrayList status = log.GetStatus();
      ArrayList statusInput = null;
      if (status != null && status.size() > 0) {
        statusInput = new ArrayList();
        for (int i = 0; i < status.size(); i++) {
          ArrayList stat = (ArrayList)status.get(i);
          if (stat != null && stat.size() >= 3) {
            ArrayList temp = new ArrayList();
            temp.add("status"+i);
            temp.add(stat.get(0));
            temp.add(stat.get(1));
            temp.add(stat.get(2));
            statusInput.add(temp);
          }
        }
      }
      bean.setStatus(statusInput);
    }
  }
}
