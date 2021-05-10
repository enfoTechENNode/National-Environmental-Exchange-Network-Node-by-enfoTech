package Node2.web.Users;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.action.ActionMessage;
import org.apache.struts.action.ActionMessages;
import org.apache.struts.action.DynaActionForm;
import org.apache.struts.actions.DispatchAction;
import Node2.model.Users.User;
import Node2.service.Users.UserManager;
/**
 * <p>This class create UserAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class UserAction extends DispatchAction {
    private static Log log = LogFactory.getLog(UserAction.class);
    private UserManager mgr = null;

	  /**
	   * setUserManager
	   * @param userManager
	   * @return 
	   */
    public void setUserManager(UserManager userManager) {
        this.mgr = userManager;
    }

	  /**
	   * delete
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
    public ActionForward delete(ActionMapping mapping, ActionForm form,
                                HttpServletRequest request,
                                HttpServletResponse response)
    throws Exception {
        if (log.isDebugEnabled()) {
            log.debug("entering 'delete' method...");
        }

        mgr.removeUser(request.getParameter("user.id"));

        ActionMessages messages = new ActionMessages();
        messages.add(ActionMessages.GLOBAL_MESSAGE, 
                     new ActionMessage("user.deleted"));

        saveMessages(request, messages);

        return list(mapping, form, request, response);
    }

	  /**
	   * edit
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
    public ActionForward edit(ActionMapping mapping, ActionForm form,
                              HttpServletRequest request,
                              HttpServletResponse response)
    throws Exception {
        if (log.isDebugEnabled()) {
            log.debug("entering 'edit' method...");
        }

        DynaActionForm userForm = (DynaActionForm) form;
        String userId = request.getParameter("id");

        // null userId indicates an add
        if (userId != null) {
            User user = mgr.getUser(userId);

            if (user == null) {
                ActionMessages errors = new ActionMessages();
                errors.add(ActionMessages.GLOBAL_MESSAGE,
                           new ActionMessage("user.missing"));
                //saveErrors(request, errors);

                return mapping.findForward("list");
            }

            userForm.set("user", user);
        }

        return mapping.findForward("edit");
    }

	  /**
	   * list
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
    public ActionForward list(ActionMapping mapping, ActionForm form,
                              HttpServletRequest request,
                              HttpServletResponse response)
    throws Exception {
        if (log.isDebugEnabled()) {
            log.debug("entering 'list' method...");
        }

        request.setAttribute("users", mgr.getUsers());

        return mapping.findForward("list");
    }

	  /**
	   * save
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
   public ActionForward save(ActionMapping mapping, ActionForm form,
                              HttpServletRequest request,
                              HttpServletResponse response)
    throws Exception {
        if (log.isDebugEnabled()) {
            log.debug("entering 'save' method...");
        }
        
        // run validation rules on this form
        ActionMessages errors = form.validate(mapping, request);
        if (!errors.isEmpty()) {
            //saveErrors(request, errors);
        	return mapping.findForward("edit");
        }

        DynaActionForm userForm = (DynaActionForm) form;
        mgr.saveUser((User)userForm.get("user"));

        ActionMessages messages = new ActionMessages();
        messages.add(ActionMessages.GLOBAL_MESSAGE,
                     new ActionMessage("user.saved"));
        saveMessages(request, messages);

        return list(mapping, form, request, response);
    }
    
	  /**
	   * unspecified
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
    public ActionForward unspecified(ActionMapping mapping, ActionForm form,
            HttpServletRequest request,
            HttpServletResponse response)
	throws Exception {
    	return list(mapping, form, request, response);
    }
}
