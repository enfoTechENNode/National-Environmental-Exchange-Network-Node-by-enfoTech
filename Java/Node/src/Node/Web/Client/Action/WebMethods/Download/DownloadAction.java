package Node.Web.Client.Action.WebMethods.Download;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.WebServices.Document.ClsNodeDocument;
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
    Object o = request.getSession().getAttribute(Phrase.DOWNLOAD_ARRAY_SESSION);
    if (docID != null && !docID.equals("") && o!=null && o.getClass().isArray()) {
      ClsNodeDocument[] docs = (ClsNodeDocument[])request.getSession().getAttribute(Phrase.DOWNLOAD_ARRAY_SESSION);
      int i = Integer.parseInt(docID);
      if (docs != null && docs.length > i) {
        request.getSession().setAttribute(Phrase.DOWNLOAD_SESSION, docs[i]);
      }
    }else if(docID != null && !docID.equals("") && o!=null){
        ClsNodeDocument doc = (ClsNodeDocument)request.getSession().getAttribute(Phrase.DOWNLOAD_ARRAY_SESSION);
        request.getSession().setAttribute(Phrase.DOWNLOAD_SESSION, doc);
    }
    return mapping.findForward("success");
  }
  
}
