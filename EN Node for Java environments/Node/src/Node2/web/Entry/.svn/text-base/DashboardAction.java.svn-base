package Node2.web.Entry;

import java.io.PrintWriter;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.actions.DispatchAction;
/**
 * <p>This class create DashboardAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class DashboardAction extends DispatchAction {

	private static Log log = LogFactory.getLog(DashboardAction.class);

	  /**
	   * checkSession
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward checkSession(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
//		HttpSession session = request.getSession();
//		String user= (String)session.getAttribute(Phrase.USER_SESSION);
//		String ret = "false";
//		if (log.isDebugEnabled()) {
//			log.debug("entering 'checkSession' method...");
//		}
//		if(!session.isNew() && user!=null && !user.equals("") && session.getAttribute(Phrase.STATUS_SESSION_COUNTER)!=null && ((Integer)session.getAttribute(Phrase.STATUS_SESSION_COUNTER)).intValue()==0){
//			ret = "true";
//			printMsgToClient(ret, response);
//		}else printMsgToClient(ret, response);
//
		return null;
	}

	  /**
	   * printMsgToClient
	   * @param result
	   * @param response
	   * @return 
	   */
	public static void printMsgToClient(String result,
			HttpServletResponse response) throws Exception {
		//response.setCharacterEncoding("UTF-8");
		PrintWriter out = response.getWriter();
		try {
			out.print(result);
		} finally {
			out.close();
		}
	}

}
