package Node.Web.WebServices;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
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
    return formExecute (mapping, form, request, response);
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
   * Log
   * @param message
   * @param level
   * @return 
   */
  protected void Log (String message, Level level)
  {
    LoggingUtils.Log(message, level, Phrase.WebServicesLoggerName);
  }
}
