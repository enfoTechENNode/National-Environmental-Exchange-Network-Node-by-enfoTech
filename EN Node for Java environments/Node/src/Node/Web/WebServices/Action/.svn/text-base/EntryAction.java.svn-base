package Node.Web.WebServices.Action;

import Node.Utils.Utility;
import Node.Web.WebServices.BaseAction;

import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Status;
import Node.Biz.Administration.User;
import Node.Biz.Handler.NAASIntegration;
import Node.DB.Interfaces.INodeUser;
import Node.Phrase;
import Node.Web.WebServices.Bean.EntryBean;
/**
 * <p>This class create EntryAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class EntryAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public EntryAction() {
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
    EntryBean bean = (EntryBean)form;
    bean.setMessage("");
    //request.getRemoteAddr();
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equalsIgnoreCase("LOGIN")) {
    	// WI 23197
        // if (this.IsAuthenticatedUser(bean,request.getRemoteAddr())) {
        if (this.IsAuthenticatedUser(bean,Utility.getIpFromRequest(request))) {
          request.getSession().setAttribute("statusUser",bean.getUserName());
          return mapping.findForward("login");
        }
      }
    }
    this.SetWebPage(bean);
    return mapping.findForward("view");
  }

  /**
   * SetWebPage
   * @param bean
   * @return 
   */
  private void SetWebPage (EntryBean bean)
  {
    try {
      Status status = new Status(Phrase.WebServicesLoggerName);
      bean.setNodeStatus(status.GetNodeStatus());
      bean.setNodeMessage(status.GetNodeMessage());
    } catch (Exception e) {
      this.Log("Could Not Set Node Status Page: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * IsAuthenticatedUser
   * @param bean
   * @param requestorIP
   * @return boolean
   */
  private boolean IsAuthenticatedUser (EntryBean bean, String requestorIP)
  {
    this.Log("EntryAction.do: Logging In user "+bean.getUserName()+" with credentials: "+bean.getPassword(),Level.DEBUG);
    boolean retBool = false;
    try {
      int result = User.AuthenticateLogin(Phrase.WebServicesLoggerName,bean.getUserName(),bean.getPassword());
      this.Log("Result: "+result,Level.DEBUG);
      if (result == INodeUser.SUCCESSFUL)
        retBool = true;
      else if (result == INodeUser.DATABASE_UNAVAILABLE)
        bean.setMessage("Database Unavailable");
      else if (result == INodeUser.INACTIVE_USER || result == INodeUser.INVALID_PERMISSION)
        bean.setMessage("Invalid Permission");
      else if (result == INodeUser.INVALID_PASSWORD)
        bean.setMessage("Invalid Password");
      else if (result == INodeUser.USER_DOES_NOT_EXIST) {
        NAASIntegration naas = new NAASIntegration(Phrase.WebServicesLoggerName);
        String token = naas.Authenticate(bean.getUserName(),bean.getPassword(),requestorIP);
        if (token != null && !token.equals(""))
          retBool = true;
        else
          bean.setMessage("Invalid Credentials");
      }
    } catch (RemoteException e) {
      bean.setMessage("Invalid Credentials");
    } catch (Exception e) {
      this.Log("Could Not Authenticate User: "+e.toString(),Level.ERROR);
    }
    return retBool;
  }
}
