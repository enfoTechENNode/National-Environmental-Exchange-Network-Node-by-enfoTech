package Node.Web.Client.Action.WebMethods;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.URL;
import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.GetStatusBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create GetStatusAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetStatusAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public GetStatusAction() {
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
    GetStatusBean bean = (GetStatusBean)form;
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equals("GET_STATUS"))
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.GetStatus(bean);
    		ret = "getstatus";
    	}else{
            this.GetStatusV2(bean);
    		ret = "getstatusV2";
    	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "getstatus";
    	}else{
    		ret = "getstatusV2";
    	}    	
    }
	return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (GetStatusBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * GetStatus
   * @param bean
   * @return 
   */
  private void GetStatus (GetStatusBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String transID = bean.getTransID();
    if (transID != null && transID.equals(""))
      transID = null;
    try {
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      String status = requestor.getStatus(token, transID);
      if (status != null)
        bean.setResult(status);
    } catch (RemoteException e) {
      bean.setResult(e.getMessage());
    } catch (Exception e) {
      bean.setError("Could Not Get Node URL");
    }
  }
  
  /**
   * GetStatusV2
   * @param bean
   * @return 
   */
  private void GetStatusV2 (GetStatusBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String transID = bean.getTransID();
    if (transID != null && transID.equals(""))
      transID = null;
    try {
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      String status[] = requestor.getStatus(token, transID);
      if (status != null)
        bean.setResult(status[0] + ".   " + status[1]);
    } catch (RemoteException e) {
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    } catch (Exception e) {
    	bean.setError("URL or parameters are error.");
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    }
  }
  
  
}
