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
import Node.Web.Client.BaseBean;
import Node.Web.Client.Bean.WebMethods.NodePingBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create NodePingAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodePingAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public NodePingAction() {
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
    NodePingBean bean = (NodePingBean)form;
    String act = request.getParameter("act");
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    if (act != null && !act.equals("")) {
        if (act.equalsIgnoreCase("NODE_PING"))
        	if (version.equalsIgnoreCase(Phrase.ver_1)){
        		this.NodePing(bean);
        		ret = "nodeping";
        	}else{
        		this.NodePingV2(bean);
        		ret = "nodepingV2";
        	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "nodeping";
    	}else{
    		ret = "nodepingV2";
    	}    	
    }
	return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (BaseBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * NodePing
   * @param bean
   * @return 
   */
  private void NodePing (NodePingBean bean)
  {
    String string = bean.getString();
    if (string != null && string.equals(""))
      string = null;
    try {
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      String result = requestor.nodePing(string);
      if (result != null)
        bean.setResult(result);
      else
        bean.setResult("");
    } catch (RemoteException e) {
      bean.setResult(e.getMessage());
    } catch (Exception e) {
      bean.setError("Could Not Find Node URL");
      bean.setResult("");
    }
  }
  
  /**
   * NodePingV2
   * @param bean
   * @return 
   */
  private void NodePingV2 (NodePingBean bean)
  {
    String string = bean.getString();
    if (string == null || string.equals(""))
      string = "enfotech";
    try {
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      String result = requestor.nodePing(string);
      if (result != null)
        bean.setResult(result);
      else
        bean.setResult("");
    } catch (RemoteException e) {
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    } catch (Exception e) {
    	bean.setError("Could Not Find Node URL");
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    }
  }
}
