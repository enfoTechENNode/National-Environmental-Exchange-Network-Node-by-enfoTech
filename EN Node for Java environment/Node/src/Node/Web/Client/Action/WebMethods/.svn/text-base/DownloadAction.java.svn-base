package Node.Web.Client.Action.WebMethods;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.URL;
import java.rmi.RemoteException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Utils.Utility;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.DownloadBean;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create DownloadAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class DownloadAction extends BaseAction {
	private static String path = Utility.GetTempFilePath();

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
    DownloadBean bean = (DownloadBean)form;
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    if (request.getMethod().equalsIgnoreCase("GET") && (request.getParameter("act") == null || !request.getParameter("act").equalsIgnoreCase("upload"))) {
      bean.setTransID("");
      bean.setDataflow("");
      bean.setDownloadDocs(null);
      bean.setFile1("");
      bean.setFile2("");
      bean.setFile3("");
      bean.setFileType1("");
      bean.setFileType2("");
      bean.setFileType3("");
    }
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equals("DOWNLOAD") && bean.getTransID()!=null && !bean.getTransID().equals("")){
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.Download(bean,request.getSession());
    		ret = "download";
    	}else{
            this.DownloadV2(bean,request.getSession());
    		ret = "downloadV2";
    	}
      }else if(bean.getTransID()==null || bean.getTransID().equals("")){
    	  bean.setError("Transaction ID cannot be null!");
    	  if (version.equalsIgnoreCase(Phrase.ver_1)){
    		  ret = "download";
    	  }else ret = "downloadV2";
      }
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "download";
    	}else{
    		ret = "downloadV2";
    	}    	
    }
    return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (DownloadBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * Download
   * @param bean
   * @param session
   * @return 
   */
  private void Download (DownloadBean bean, HttpSession session)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String transID = bean.getTransID();
    if (transID != null && transID.equals(""))
      transID = null;
    String dataFlow = bean.getDataflow();
    if (dataFlow != null && dataFlow.equals(""))
      dataFlow = null;
    int count = 0;
    String file1 = bean.getFile1();
    if (file1 != null && !file1.equals(""))
      count++;
    String file2 = bean.getFile2();
    if (file2 != null && !file2.equals(""))
      count++;
    String file3 = bean.getFile3();
    if (file3 != null && !file3.equals(""))
      count++;
    try {
      ClsNodeDocument[] docs = null;
      if (count > 0) {
        docs = new ClsNodeDocument [count];
        count = 0;
        if (file1 != null && !file1.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file1);
          docs[count].setType(GetFileType(bean.getFileType1()));
          docs[count].setContent(new byte[0]);
          count++;
        }
        if (file2 != null && !file2.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file2);
          docs[count].setType(GetFileType(bean.getFileType2()));
          docs[count].setContent(new byte[0]);
          count++;
        }
        if (file3 != null && !file3.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file3);
          docs[count].setType(GetFileType(bean.getFileType3()));
          docs[count].setContent(new byte[0]);
          count++;
        }
      }
      // Delete old files
      ClsNodeDocument[] oldDocs = (ClsNodeDocument[])session.getAttribute(Phrase.DOWNLOAD_ARRAY_SESSION);
      if(oldDocs!=null && oldDocs.length>0){
    	  for(int i=0;i<oldDocs.length;i++){
    	      Utility.delFile(path + "/temp/"+oldDocs[i].getName());    		  
    	  }
      }
      // Download new files
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      ClsNodeDocument[] retDocs = requestor.download(token, transID, dataFlow, docs);
      ClsNodeDocument[] retBeanDocs = null;
      if(retDocs!=null && retDocs.length>0){
    	  retBeanDocs = new ClsNodeDocument[retDocs.length];
          for(int i=0;i<retDocs.length;i++){	// write download files to disk 
              InputStream is = new ByteArrayInputStream(retDocs[i].getContent());
              retBeanDocs[i] = new ClsNodeDocument();
        	  retBeanDocs[i].setName(retDocs[i].getName());
              String tmpName = retDocs[i].getName()+"_tmp"+Utility.GetSysTimeString();
              Utility.writeFile(path + "/temp/"+tmpName,is);
              retDocs[i].setName(tmpName);
        	  retDocs[i].setContent(null);
          }    	  
      }else{
          bean.setError("E_FileNotFound");
          bean.setResult("E_FileNotFound");    	  
      }
      bean.setDownloadDocs(retBeanDocs);
      session.setAttribute(Phrase.DOWNLOAD_ARRAY_SESSION,retDocs);
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      bean.setDownloadDocs(null);
      bean.setError(sw.toString());
    } catch (Exception e) {
      bean.setDownloadDocs(null);
      if(e.getMessage().indexOf("Could not convert to bytes ") != -1){
          bean.setError("E_FileNotFound.");    	  
      }else  bean.setError("Could Not Download Files: " + e.getMessage());
    }
  }

  /**
   * DownloadV2
   * @param bean
   * @param session
   * @return 
   */
  private void DownloadV2 (DownloadBean bean, HttpSession session)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String transID = bean.getTransID();
    if (transID != null && transID.equals(""))
      transID = null;
    String dataFlow = bean.getDataflow();
    if (dataFlow != null && dataFlow.equals(""))
      dataFlow = null;
    int count = 0;
    String file1 = bean.getFile1();
    if (file1 != null && !file1.equals(""))
      count++;
    String file2 = bean.getFile2();
    if (file2 != null && !file2.equals(""))
      count++;
    String file3 = bean.getFile3();
    if (file3 != null && !file3.equals(""))
      count++;
    String fileType1 = GetFileType(bean.getFileType1());
    String fileType2 = GetFileType(bean.getFileType2());
    String fileType3 = GetFileType(bean.getFileType3());
    
    try {
      ClsNodeDocument[] docs = null;
      if (count > 0) {
        docs = new ClsNodeDocument [count];
        count = 0;
        if (file1 != null && !file1.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file1);
          docs[count].setType(fileType1);
          docs[count].setContent(new byte[0]);
          count++;
        }
        if (file2 != null && !file2.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file2);
          docs[count].setType(fileType2);
          docs[count].setContent(new byte[0]);
          count++;
        }
        if (file3 != null && !file3.equals("")) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file3);
          docs[count].setType(fileType3);
          docs[count].setContent(new byte[0]);
          count++;
        }
      }
      // Delete old files
      ClsNodeDocument[] oldDocs = (ClsNodeDocument[])session.getAttribute(Phrase.DOWNLOAD_ARRAY_SESSION);
      if(oldDocs!=null && oldDocs.length>0){
    	  for(int i=0;i<oldDocs.length;i++){
    	      Utility.delFile(path + "/temp/"+oldDocs[i].getName());    		  
    	  }
      }
      
      // Download new files
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      ClsNodeDocument[] retDocs = requestor.download(token, dataFlow, transID, docs);
      ClsNodeDocument[] retBeanDocs = null;
      if(retDocs!=null && retDocs.length>0){
    	  retBeanDocs = new ClsNodeDocument[retDocs.length];
          for(int i=0;i<retDocs.length;i++){	// write download files to disk 
              InputStream is = new ByteArrayInputStream(retDocs[i].getContent());
              retBeanDocs[i] = new ClsNodeDocument();
        	  retBeanDocs[i].setName(retDocs[i].getName());
              String tmpName = retDocs[i].getName()+"_tmp"+Utility.GetSysTimeString();
              Utility.writeFile(path + "/temp/"+tmpName,is);
              retDocs[i].setName(tmpName);
        	  retDocs[i].setContent(null);
          }    	  
      }else{
          bean.setError("E_FileNotFound");
          bean.setResult("E_FileNotFound");    	  
      }
      bean.setDownloadDocs(retBeanDocs);
      session.setAttribute(Phrase.DOWNLOAD_ARRAY_SESSION,retDocs);
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      bean.setError(sw.toString());
      bean.setResult(sw.toString());
    } catch (Exception e) {
//    	bean.setError("Could Not Download Files");
    	StringWriter sw = new StringWriter();
    	e.printStackTrace(new PrintWriter(sw));
    	bean.setResult(sw.toString());
    }
  }

  /**
   * GetFileType
   * @param name
   * @return String
   */
  private String GetFileType (String name)
  {
    String retString = Phrase.OTHER_TYPE;
    if(name !=null && !name.equalsIgnoreCase("")){
    	String temp = name.toLowerCase();
    	if (temp.equalsIgnoreCase("XML"))
    		retString = Phrase.XML_TYPE;
    	if (temp.equalsIgnoreCase("FLAT"))
    		retString = Phrase.FLAT_TYPE;
    	if (temp.equalsIgnoreCase("BIN"))
    		retString = Phrase.BIN_TYPE;
    	if (temp.equalsIgnoreCase("ODF"))
    		retString = Phrase.ODF_TYPE;
    	if (temp.equalsIgnoreCase("ZIP"))
    		retString = Phrase.ZIP_TYPE;
    }
    return retString;
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
    if (temp.endsWith(".exe") || temp.endsWith(".dll") || temp.endsWith(".bin") || temp.endsWith(".odf") )
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
