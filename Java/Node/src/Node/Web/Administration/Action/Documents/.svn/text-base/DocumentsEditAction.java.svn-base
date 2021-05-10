package Node.Web.Administration.Action.Documents;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;

import Node.Biz.Administration.Document;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Documents.DocumentsEditBean;
/**
 * <p>This class create DocumentsEditAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class DocumentsEditAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public DocumentsEditAction() {
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
    DocumentsEditBean bean = (DocumentsEditBean)form;
    bean.setMessage("");
    String documentID = request.getParameter("documentid");
    if (documentID != null)
    {
      Document doc = new Document(Integer.parseInt(documentID), Phrase.AdministrationLoggerName);
      this.SetWebPage(bean, doc, request.getSession());
    }
    String act = request.getParameter("act");
    if (act != null && !act.trim().equals(""))
    {
      if (act.equalsIgnoreCase("SAVE")) {
        Document savedDoc = this.Save(bean, request.getSession());
        if (savedDoc != null)
          this.SetWebPage(bean, savedDoc, request.getSession());
      }
    }
    return mapping.findForward("update");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (DocumentsEditBean bean, Document doc, HttpSession session)
  {
    this.Log("DocumentsEdit.do: Setting Web Page Document Edit "+doc!=null?doc.GetName():"",Level.DEBUG);
    if (doc != null)
    {
      try {
        bean.setTitle(doc.GetName());
        bean.setId(doc.GetFileID());
        bean.setName(doc.GetName());
        bean.setType(doc.GetType());
        bean.setSize("" + doc.GetSize());
        bean.setDataFlow(doc.GetDataFlow());
        User admin = new User( (String) session.getAttribute(Phrase.USER_SESSION), Phrase.AdministrationLoggerName);
        bean.setOps(Document.GetUniqueOperationNames(Phrase.AdministrationLoggerName, admin));
        bean.setTransID(doc.GetTransID());
        String wsName = doc.GetWebService();
        if (wsName != null && !wsName.trim().equals(""))
        {
          bean.setSource("Web Service");
          bean.setSourceName(wsName + "." + doc.GetOperation());
        }
        else
        {
            bean.setSource("Domain Administrator");
            bean.setSourceName(doc.GetUpdatedBy());
        }
      }
      catch (Exception e) {
        this.Log("Could Not Set Documents Edit Page: "+e.toString(),Level.ERROR);
      }
    }
    else {
      bean.setMessage("Database Error");
      this.Log("DomainsEdit.do: Document is null",Level.WARN);
    }
  }

  /**
   * Save
   * @param bean
   * @param session
   * @return Document
   */
  private Document Save (DocumentsEditBean bean, HttpSession session)
  {
    this.Log("DocumentsEdit.do: Saving Document "+bean.getName(),Level.DEBUG);
    Document retDocument = null;
    if (this.IsValidInput(bean))
    {
      try{
        Document doc = new Document(bean.getId(), Phrase.AdministrationLoggerName);
        doc.SetName(bean.getName());
        doc.SetType(bean.getType());
        doc.SetDataFlow(bean.getDataFlow());
        FormFile file = bean.getUpFile();
        if (file != null && !file.getFileName().trim().equals(""))
          doc.SetContent(file.getFileData());
        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
        doc.SetUpdatedBy(admin.GetLoginName());
        String retReason = doc.Save(Phrase.AdministrationLoggerName);
        if (retReason != null && !retReason.trim().equals(""))
          throw new Exception(retReason);
        else
        {
          retDocument = doc;
          bean.setMessage("Sucessful Save");
        }
      }
      catch (Exception e) {
        this.Log("Could Not Save Document: " + e.toString(),Level.ERROR);
        bean.setMessage("Database Error");
      }
    }
    return retDocument;
  }

  /**
   * IsValidInput
   * @param bean
   * @return boolean
   */
  private boolean IsValidInput (DocumentsEditBean bean)
  {
    return true;
  }
}
