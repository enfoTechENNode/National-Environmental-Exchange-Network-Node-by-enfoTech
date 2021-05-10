package Node.Web.Administration.Action.Documents;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.ServletOutputStream;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Biz.Administration.Document;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
import Node.Utils.Utility;
import Node.Web.Administration.BaseAction;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create DownloadStreamAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DownloadStreamAction extends BaseAction {
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
		  String docID = (String)request.getSession().getAttribute(Phrase.DOWNLOAD_SESSION);
		  String operationDocID = (String)request.getSession().getAttribute(Phrase.DOWNLOAD_OPERATIONDOCUMENT_SESSION);
		  String tempDocName = (String)request.getSession().getAttribute(Phrase.DOWNLOAD_TEMPFILE_SESSION);
		  String path = Utility.GetTempFilePath() + "/temp/";

		  if (docID != null && !docID.equals("")) {
			  Document doc = new Document(Integer.parseInt(docID),Phrase.AdministrationLoggerName);
			  if (doc != null) {
				  response.setContentType(this.GetType(doc.GetType()));
				  response.addHeader("Content-Disposition","attachment;filename="+doc.GetName());
				  ServletOutputStream out = response.getOutputStream();
				  byte[] content = doc.GetContent();
				  if (content != null)
					  for (int i = 0; i < content.length; i++)
						  out.write(content[i]);
				  out.close();
				  //request.getSession().removeAttribute(Phrase.DOWNLOAD_SESSION);
			  }
		  }else if (operationDocID != null && !operationDocID.equals("")) {
			  INodeOperation opDB = DBManager.GetNodeOperation(Phrase.AdministrationLoggerName);
			  ClsNodeDocument doc = opDB.GetOperationsManagerDocument(operationDocID);
			  String filePath = doc.getName();
			  if (doc != null) {
				  response.setContentType(this.GetType(doc.getType()));
				  response.addHeader("Content-Disposition","attachment;filename="+"submit_report.zip");
				  ServletOutputStream out = response.getOutputStream();
				  WriteToClient(out,filePath);
				  out.close();
				  Utility.delFile(filePath);
			  }    	  
		  }else if (tempDocName != null && !tempDocName.equals("")) {
			  String filePath = path+tempDocName;
			  String fileType = tempDocName.substring(tempDocName.indexOf(",")+1);
			  if (filePath != null) {
				  File temFile = new File(filePath);
				  if(temFile.length()!=0){
					  response.setContentType(this.GetType(fileType));
					  response.addHeader("Content-Disposition","attachment;filename="+tempDocName);
					  ServletOutputStream out = response.getOutputStream();
					  WriteToClient(out,filePath);
					  out.close();
					  Utility.delFile(filePath);
				  }else{
						String ret = "The file has been downloaded.";
						response.getOutputStream().write(ret.getBytes());
						response.getOutputStream().close();
				  }
				  temFile = null;
			  }    	  
		  }
	  } catch (Exception e) {
		  this.Log("DownloadStream.do: Could Not Download Document: "+e.toString(),Level.ERROR);
	  }
	  return null;
  }

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
  
  private boolean WriteToClient (ServletOutputStream out,String filePath){
		try {
			int byteread = 0;
			File oldfile = new File(filePath);
			if (oldfile.exists()) {
				InputStream inStream = new FileInputStream(filePath);
				byte[] buffer = new byte[1024];
				while ( (byteread = inStream.read(buffer)) != -1) {
 					out.write(buffer, 0, byteread);
				}
				inStream.close();
				return true;
			}else {
				this.Log("DownloadStreamAction>>>No file found.", Level.DEBUG);
				return false;
			}
		}
		catch (Exception e) {
			this.Log("DownloadStreamAction>>>Read file error." + e.toString(), Level.DEBUG);
			return false;
		}
	  
  }
  
}
