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
import Node.Web.Client.Bean.WebMethods.GetServicesBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create GetServicesAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetServicesAction extends BaseAction{
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public GetServicesAction() {
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
    GetServicesBean bean = (GetServicesBean)form;
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equals("GET_SERVICES"))
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.GetServices(bean);
    		ret = "getservices";
    	}else{
            this.GetServicesV2(bean);
    		ret = "getservicesV2";
    	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "getservices";
    	}else{
    		ret = "getservicesV2";
    	}    	
    }
    return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (GetServicesBean bean)
  {
    super.SetPage(bean);
    bean.setGetServicesResult(null);
  }

  /**
   * GetServices
   * @param bean
   * @return 
   */
  private void GetServices (GetServicesBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String serviceType = bean.getServiceType();
    if (serviceType != null && serviceType.equals(""))
      serviceType = null;
    try {
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setGetServicesResult(requestor.getServices(token, serviceType));
    } catch (RemoteException e) {
      bean.setGetServicesResult(new String[]{e.getMessage()});
    } catch (Exception e) {
      bean.setError("Could Not Get Node URL");
      bean.setGetServicesResult(null);
    }
  }
  
  /**
   * GetServicesV2
   * @param bean
   * @return 
   */
  private void GetServicesV2 (GetServicesBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String serviceType = bean.getServiceType();
    if (serviceType != null && serviceType.equals(""))
      serviceType = null;
    try {
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new  Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setServiceXML(requestor.getServices(token, serviceType));
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
      bean.setServiceXML(sw.toString());
    } catch (Exception e) {
      bean.setError("Could Not Get Node URL");
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
      bean.setServiceXML(sw.toString());
    }
  }

}
