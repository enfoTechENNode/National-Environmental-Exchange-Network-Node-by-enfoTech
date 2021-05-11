package Node.Web.Administration.Action.Documents;

import java.text.SimpleDateFormat;
import java.sql.Date;
import java.util.Calendar;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.TimeZone;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
import javax.servlet.ServletOutputStream;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Document;
import Node.Biz.Administration.User;
import Node.Phrase;
import Node.Web.Administration.Bean.Documents.DocumentsBean;
import Node.Web.Administration.BaseAction;
/**
 * <p>This class create DocumentsAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DocumentsAction extends BaseAction {
  /**
   * Constructor
   * @param 
   * @return 
   */
  public DocumentsAction() {
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
    this.Log("Executing Documents.do",Level.INFO);
    DocumentsBean bean = (DocumentsBean)form;
    if (request.getMethod().equalsIgnoreCase("GET"))
      this.ClearWebPage(bean);
    String doc = request.getParameter("doc");
    if (doc != null && !doc.equals("")) {
      this.PrepareDownload(doc, response);
      return null;
    }
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      this.Log("Documents.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("UPLOAD_FILE"))
        return mapping.findForward("upload");
      if (act.equalsIgnoreCase("REMOVE"))
        this.RemoveFiles(request);
    }
    this.SetWebPage(bean,request.getSession());
    this.Search(bean,request.getSession());
    return mapping.findForward("update");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (DocumentsBean bean, HttpSession session)
  {
    try {
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      //bean.setDomainNames(admin.GetAssignedDomains());
      bean.setDomainNames(admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName));
      //bean.setDomainNames(admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName));
      bean.setOpNames(Document.GetUniqueOperationNames(Phrase.AdministrationLoggerName,admin));
    } catch (Exception e) {
      this.Log("Could Not Set Documents Search Page: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * Search
   * @param bean
   * @param session
   * @return 
   */
  private void Search (DocumentsBean bean, HttpSession session)
  {
    try {
      Date start = null;
      Date end = null;
      if (bean.getBeginDate() != null && !bean.getBeginDate().equals(""))
        start = Date.valueOf(bean.getBeginDate());
      if (bean.getEndDate() != null && !bean.getEndDate().equals(""))
        end = Date.valueOf(bean.getEndDate());
      User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
      bean.setDocs(Document.SearchDocuments(Phrase.AdministrationLoggerName,bean.getDocName(),bean.getTransID(),
                                            bean.getDomainName(),bean.getOpName(),start,end, admin, "VER_11"));//hard coded for testing, need to change later on
    } catch (Exception e) {      this.Log("Could Not Search Documents: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @return 
   */
  private void ClearWebPage (DocumentsBean bean)
  {
    bean.setDocName("");
    bean.setDocs(null);
    bean.setDomainName("");
    bean.setDomainNames(null);
    bean.setOpName("");
    bean.setOpNames(null);
    bean.setTransID("");
    this.SetCalendar(bean);
  }

  /**
   * SetCalendar
   * @param bean
   * @return 
   */
  private void SetCalendar (DocumentsBean bean)
  {
    SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
    Calendar cal = Calendar.getInstance(TimeZone.getDefault());
    bean.setEndDate(format.format(cal.getTime()));
    cal.add(Calendar.DAY_OF_MONTH,-7);
    bean.setBeginDate(format.format(cal.getTime()));
  }

  /**
   * PrepareDownload
   * @param id
   * @param response
   * @return 
   */
  private void PrepareDownload (String id, HttpServletResponse response)
  {
    try {
      Document doc = new Document(Integer.parseInt(id),Phrase.AdministrationLoggerName);
      if (doc != null) {
        response.setContentType(this.GetType(doc.GetType()));
        response.addHeader("Content-Disposition","attachment;filename=" + doc.GetName());
        ServletOutputStream out = response.getOutputStream();
        byte[] content = doc.GetContent();
        if (content != null)
          for (int i = 0; i < content.length; i++)
            out.write(content[i]);
        //out.flush();
        out.close();
      }
      else {
        this.Log("Could Not Get Document",Level.WARN);
      }
    } catch (Exception e) {
      this.Log("Could Not Prepare Download: "+e.toString(),Level.ERROR);
    }
  }

  /**
   * GetType
   * @param name
   * @return String
   */
  private String GetType (String name)
  {
    String retString = Phrase.OTHER_TYPE;
    String temp = name.toLowerCase();
    if (temp.endsWith(".xml") || temp.endsWith(".xslt") || temp.endsWith(".config"))
      retString = "text/xml";
    if (temp.endsWith(".doc"))
      retString = "application/msword";
    if (temp.endsWith(".jpg"))
      retString = "image/pjpeg";
    if (temp.endsWith(".html"))
      retString = "text/html";
    if (temp.endsWith(".gif"))
      retString = "image/gif";
    if (temp.endsWith(".exe") || temp.endsWith(".dll"))
      retString = "application/octet-stream";
    if (temp.endsWith(".zip") || temp.endsWith(".war") || temp.endsWith(".jar"))
      retString = "application/x-zip-compressed";
    if (temp.endsWith(".txt") || temp.endsWith(".sql"))
      retString = "text/plain";
    if (temp.endsWith(".ppt"))
      retString = "application/vnd.ms-powerpoint";
    if (temp.endsWith(".xls"))
      retString = "application/vnd.ms-excel";
    return retString;
  }

  /**
   * RemoveFiles
   * @param request
   * @return 
   */
  private void RemoveFiles (HttpServletRequest request)
  {
    try {
      Enumeration e = request.getParameterNames();
      if (e != null) {
        HashMap map = new HashMap();
        while (e.hasMoreElements()) {
          String name = (String)e.nextElement();
          if (name.startsWith("remove")) {
            String paramValue = request.getParameter(name);
            if (paramValue.equals("on"))
              map.put(name,name.substring(6));
          }
        }
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            int[] input = new int [temp.length];
            for (int i = 0; i < input.length; i++)
              input[i] = Integer.parseInt((String)temp[i]);
            Document.RemoveDocuments(Phrase.AdministrationLoggerName,input);
          }
        }
      }
    } catch (Exception e) {
      this.Log("Could Not Remove Documents: "+e.toString(),Level.ERROR);
    }
  }
}
