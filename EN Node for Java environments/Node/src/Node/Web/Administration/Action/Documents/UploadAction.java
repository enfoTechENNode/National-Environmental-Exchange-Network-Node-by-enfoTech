package Node.Web.Administration.Action.Documents;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.API.DocumentManagement;
import Node.Biz.Administration.Document;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Utils.Utility;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Documents.UploadBean;
/**
 * <p>This class create UploadAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class UploadAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public UploadAction() {
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
    this.Log("Executing Upload.do",Level.INFO);
    UploadBean bean = (UploadBean)form;
    bean.setMessage("");
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("Upload.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("UPLOAD"))
        this.Upload(bean, request.getSession());
    }
    this.SetWebPage(bean,request.getSession());
    return mapping.findForward("success");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (UploadBean bean, HttpSession session)
  {
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      bean.setOps(Document.GetUniqueOperationNames(Phrase.AdministrationLoggerName,admin));
      // WI 19928
      if(bean.getUpOpName() == null || bean.getUpOpName().equalsIgnoreCase("")){
    	  bean.setUpOpName("DEFAULT");
      }
    } catch (Exception e) {
      this.Log("Could Not Set Documents Search Page: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * Upload
   * @param bean
   * @param session
   * @return boolean
   */
  private boolean Upload (UploadBean bean, HttpSession session)
  {
    boolean retBool = false;
    try {
      DocumentManagement docManager = new DocumentManagement(Phrase.AdministrationLoggerName);
      String[] names = new String[] { bean.getUpDocName() };
      byte[][] content = new byte[][] { bean.getUpFile().getFileData() };
      String[] types = new String[] { bean.getUpDocType() };
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION), Phrase.AdministrationLoggerName);
      retBool = docManager.Upload(names,content,types,bean.getTransID(),null,bean.getUpOpName(),null,null,Utility.GetNowDate(),Utility.GetNowTimeStamp(), admin.GetLoginName());
      if (retBool)
        bean.setMessage("Successful Upload");
      else
        bean.setMessage("Error Uploading File");
    } catch (Exception e) {
      this.Log("Could Not Upload File: "+e.toString(),Level.ERROR);
    }
    return retBool;
  }
}
