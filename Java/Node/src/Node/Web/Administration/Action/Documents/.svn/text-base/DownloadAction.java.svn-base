package Node.Web.Administration.Action.Documents;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Web.Administration.BaseAction;
/**
 * <p>This class create DownloadAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DownloadAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public DownloadAction() {
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
    String docID = request.getParameter("doc");
    String operationDocID = request.getParameter("operationDoc");
    String tempDocName = request.getParameter("tempDoc");
    
    if (docID != null && !docID.equals("")) {
    	request.getSession().setAttribute(Phrase.DOWNLOAD_SESSION,docID);
    	request.getSession().setAttribute(Phrase.DOWNLOAD_OPERATIONDOCUMENT_SESSION,"");
    	request.getSession().setAttribute(Phrase.DOWNLOAD_TEMPFILE_SESSION,"");
    }else if(operationDocID != null && !operationDocID.equals("")) {
        request.getSession().setAttribute(Phrase.DOWNLOAD_OPERATIONDOCUMENT_SESSION,operationDocID);
        request.getSession().setAttribute(Phrase.DOWNLOAD_SESSION,"");
    	request.getSession().setAttribute(Phrase.DOWNLOAD_TEMPFILE_SESSION,"");
    }else if(tempDocName != null && !tempDocName.equals("")) {
    	request.getSession().setAttribute(Phrase.DOWNLOAD_TEMPFILE_SESSION,tempDocName);
    	request.getSession().setAttribute(Phrase.DOWNLOAD_SESSION,"");
    	request.getSession().setAttribute(Phrase.DOWNLOAD_OPERATIONDOCUMENT_SESSION,"");
    }
    return mapping.findForward("success");
  }
}
