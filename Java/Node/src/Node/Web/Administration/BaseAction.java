package Node.Web.Administration;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.log4j.Level;
import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create BaseAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public abstract class BaseAction extends Action {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public BaseAction() {
  }

  /**
   * execute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward execute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) throws Exception
  {
    if (this.IsSessionActive(request))
      return formExecute (mapping, form, request, response);
    return mapping.findForward("invalidSession");
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public abstract ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse resonse) throws Exception;

  /**
   * IsSessionActive
   * @param request
   * @return boolean
   */
  private boolean IsSessionActive (HttpServletRequest request)
  {
    boolean retBool = false;
    String uri = request.getRequestURI().toString();
    if (uri != null && !uri.equals("/Node.Administration/Page/Entry/Login.do")) {
      HttpSession session = request.getSession();
      String login = (String) session.getAttribute(Phrase.USER_SESSION);
      if (login != null && !login.equals(""))
        retBool = true;
    }
    else if (uri != null)
      retBool = true;
    return retBool;
  }

  /**
   * Log
   * @param message
   * @param level
   * @return 
   */
  protected void Log (String message, Level level)
  {
    LoggingUtils.Log(message, level, Phrase.AdministrationLoggerName);
  }

  /**
   * GetLogSessionValue
   * @param 
   * @return String
   */
  protected String GetLogSessionValue ()
  {
    return (String)this.getServlet().getServletContext().getAttribute(Phrase.LoggerSessionKey);
  }

  /**
   * EnterValueArray
   * @param original
   * @param value
   * @param index
   * @return String[]
   */
  protected String[] EnterValueArray (String[] original, String value, int index)
  {
    if (original == null || original.length == 0)
      return new String[]{value};
    if (index < original.length) {
      original[index] = value;
      return original;
    }
    String[] retArray = new String [original.length*2];
    for (int i = 0; i < original.length; i++)
      retArray[i] = original[i];
    retArray[original.length] = value;
    return retArray;
  }
}
