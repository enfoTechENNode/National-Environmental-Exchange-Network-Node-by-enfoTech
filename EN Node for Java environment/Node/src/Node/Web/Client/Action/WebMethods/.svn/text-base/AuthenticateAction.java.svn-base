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
import Node.Web.Client.Bean.WebMethods.AuthenticateBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create AuthenticateAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AuthenticateAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public AuthenticateAction() {
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
    AuthenticateBean bean = (AuthenticateBean)form;
    this.SetPage(bean);
    String act = request.getParameter("act");
    String version = request.getParameter("version");
    String ret = null;
    if (act != null && !act.equals("")) {
        if (act.equalsIgnoreCase("AUTHENTICATE"))
    	  if (version.equalsIgnoreCase(Phrase.ver_1)){
    	      this.Authenticate(bean);
    		  ret = "authenticate";
    	  }
    	  else{
    	      this.AuthenticateV2(bean);
    		  ret = "authenticateV2";
    	  }
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "authenticate";
    	}else{
    		ret = "authenticateV2";
    	}    	
    }
     return mapping.findForward(ret);
  }

  /**
   * SetWebPage
   * @param bean
   * @return 
   */
  protected void SetPage (BaseBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * Authenticate
   * @param bean
   * @return 
   */
  private void Authenticate (AuthenticateBean bean)
  {
    String userID = bean.getUser();
    String password = bean.getPassword();
    String authMethod = bean.getAuthMethod();
    if (userID != null && userID.equals(""))
      userID = null;
    if (password != null && password.equals(""))
      password = null;
    if (authMethod != null && authMethod.equals(""))
      authMethod = null;
    try {
      String nodeAddress = this.GetNodeAddress(bean);
      URL node = new URL(nodeAddress);
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      String token = requestor.authenticate(userID, password, authMethod);
      if (token != null && !token.equals("")) {
        bean.setResult(token);
        this.SetToken(token);
      }
      else {
        bean.setResult("");
        this.SetToken("");
      }
    } catch (RemoteException e) {
      bean.setResult(e.getMessage());
      this.SetToken("");
    } catch (Exception e) {
      bean.setError("Could Not Get Node URL");
      bean.setResult("");
      this.SetToken("");
    }
  }
  
  /**
   * AuthenticateV2
   * @param bean
   * @return 
   */
  private void AuthenticateV2 (AuthenticateBean bean)
  {
    String userID = bean.getUser();
    String password = bean.getPassword();
    String authMethod = bean.getAuthMethod();
    String domainName = bean.getDomainName();
    try {
      String nodeAddress = this.GetNodeAddress_V2(bean);
      URL node = new URL(nodeAddress);
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      String token = requestor.authenticate(userID, password, authMethod,domainName);
      if (token != null && !token.equals("")) {
        bean.setResult(token);
        this.SetToken(token);
      }
      else {
        bean.setResult("");
        this.SetToken("");
      }
    } catch (RemoteException e) {
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    	this.SetToken("");
    } catch (Exception e) {
    	bean.setError("Could Not Get Node URL");
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    	this.SetToken("");
    }
  }
  
  
}
