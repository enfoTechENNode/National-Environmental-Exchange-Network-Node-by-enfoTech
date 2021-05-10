package Node.Web.Client.Action.WebMethods.Download;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Utils.Utility;
import Node.Web.Client.BaseAction;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create DownloadStreamAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class DownloadStreamAction extends BaseAction {
	private static String path = Utility.GetTempFilePath();
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public DownloadStreamAction() {
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
    try {
      ClsNodeDocument doc = (ClsNodeDocument)request.getSession().getAttribute(Phrase.DOWNLOAD_SESSION);
      if (doc != null) {
        response.setContentType(this.GetType(doc.getType()));
        response.addHeader("Content-Disposition","attachment;filename="+(doc.getName().split("_tmp"))[0]);
        ServletOutputStream out = response.getOutputStream();
        byte[] content = Utility.readFile(path + "/temp/"+doc.getName());
        if (content != null)
          for (int i = 0; i < content.length; i++)
            out.write(content[i]);
        out.close();
      }
    } catch (Exception e) {
      this.Log("DownloadStream.do: Could Not Download Document: "+e.toString(),Level.ERROR);
    }
    return null;
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
}
