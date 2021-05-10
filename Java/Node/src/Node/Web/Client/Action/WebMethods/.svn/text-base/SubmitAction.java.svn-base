package Node.Web.Client.Action.WebMethods;

import java.net.URL;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;

import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.SubmitBean;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create SubmitAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SubmitAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public SubmitAction() {
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse resonse) throws Exception
  {
    SubmitBean bean = (SubmitBean)form;
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equals("SUBMIT"))
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.Submit(bean);
    		ret = "submit";
    	}else{
    		this.SubmitV2(bean);
    		ret = "submitV2";
    	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "submit";
    	}else{
    		ret = "submitV2";
    	}    	
    }
	return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (SubmitBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * Submit
   * @param bean
   * @return 
   */
  private void Submit (SubmitBean bean)
  {
    this.Log("SubmitAction.do: Submitting", Level.INFO);
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
    FormFile file1 = bean.getFile1();
    if (file1.getFileSize() > 0)
      count++;
    FormFile file2 = bean.getFile2();
    if (file2.getFileSize() > 0)
      count++;
    FormFile file3 = bean.getFile3();
    if (file3.getFileSize() > 0)
      count++;
    this.Log("SubmitAction.do: Trying to Submit " + count + " Documents", Level.DEBUG);
    try {
      ClsNodeDocument[] docs = null;
      if (count > 0) {
        docs = new ClsNodeDocument [count];
        count = 0;
        if (file1.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file1.getFileName());
          docs[count].setType(file1.getContentType());
          if(bean.getFile1_type().equalsIgnoreCase(""))
            docs[count].setType(file1.getContentType());
          else
            docs[count].setType(bean.getFile1_type());
          docs[count].setContent(file1.getFileData());
          // debug
          /*
          if(file1.getFileData() == null){
            System.out.println("*** file1.getFileDate() is null. ***");
          }else if(file1.getFileData().length <= 0){
            System.out.println("*** The length of file1.getFileDate() is <= 0. ***");
          }else{
            System.out.println("*** The length of file1.getFileDate() is " + file1.getFileData().length + ". ***");
            if(docs[count].getContent() != null){
              System.out.println("*** the length of docs[" + count + "].getContent() is " + docs[count].getContent().length + ". ***");
            }else{
              System.out.println("*** the length of docs[" + count + "].getContent() is null. ***");
            }
          }
          */
          // end debug
          count++;
        }
        if (file2.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file2.getFileName());
          if(bean.getFile2_type().equalsIgnoreCase(""))
            docs[count].setType(file2.getContentType());
          else
            docs[count].setType(bean.getFile2_type());
          docs[count].setContent(file2.getFileData());
          count++;
        }
        if (file3.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file3.getFileName());
          if(bean.getFile3_type().equalsIgnoreCase(""))
            docs[count].setType(file3.getContentType());
          else
            docs[count].setType(bean.getFile3_type());
          docs[count].setContent(file3.getFileData());
          count++;
        }
      }
      if (docs != null)
        this.Log("SubmitAction.do: The Doc Array has " + docs.length + " Documents", Level.DEBUG);
      else
        this.Log("SubmitAction.do: The Doc Array is Null", Level.DEBUG);
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      // debug
      /*
      System.out.println("*** SubmitAction: submit method --> check document ***");
      for(int i = 0; i < docs.length; i++){
        System.out.println("doc " + (i+1) + ": " + docs[i].getName() + ", " + docs[i].getType());
        if(docs[i].getContent() == null)
          System.out.println("content of document " + (i+1) + " is null.");
      }
      System.out.println("*** SubmitAction: end of submit method --> check document ***");
      */
      // end debug
      bean.setResult(requestor.submit(token, transID, dataFlow, docs));
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    } catch (Exception e) {
      this.Log("SubmitAction.do: Error - " + e.getMessage(), Level.ERROR);
      bean.setError("Could Not Submit Files");
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    }
  }
  
  /**
   * SubmitV2
   * @param bean
   * @return 
   */
  private void SubmitV2 (SubmitBean bean)
  {
    this.Log("SubmitAction.do: Submitting", Level.INFO);
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = "";
    String transID = bean.getTransID();
    if (transID != null && transID.equals(""))
      transID = "";
    String dataFlow = bean.getDataflow();
    if (dataFlow != null && dataFlow.equals(""))
      dataFlow = "";
    String flowOperation = bean.getFlowOperation();
    String recipients = bean.getRecipients();
    String[] recipientList = null;    	
    if (recipients != null && !recipients.equals("")){
        recipientList = recipients.split(",");    	
    }
    String notificationTypes = bean.getNotificationType();
    String notificationURI = bean.getNotificationURI();
    String[] notificationURIList = null;
    if (notificationURI != null && !notificationURI.equals("")){
    	notificationURIList = notificationURI.split(",");    	
    }
    int count = 0;
    FormFile file1 = bean.getFile1();
    if (file1.getFileSize() > 0)
      count++;
    FormFile file2 = bean.getFile2();
    if (file2.getFileSize() > 0)
      count++;
    FormFile file3 = bean.getFile3();
    if (file3.getFileSize() > 0)
      count++;
    this.Log("SubmitAction.do: Trying to Submit " + count + " Documents", Level.DEBUG);
    try {
      ClsNodeDocument[] docs = null;
      if (count > 0) {
        docs = new ClsNodeDocument [count];
        count = 0;
        if (file1.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file1.getFileName());
          docs[count].setType(file1.getContentType());
          if(bean.getFile1_type().equalsIgnoreCase(""))
            docs[count].setType(file1.getContentType());
          else
            docs[count].setType(bean.getFile1_type());
          docs[count].setContent(file1.getFileData());
          // debug
          /*
          if(file1.getFileData() == null){
            System.out.println("*** file1.getFileDate() is null. ***");
          }else if(file1.getFileData().length <= 0){
            System.out.println("*** The length of file1.getFileDate() is <= 0. ***");
          }else{
            System.out.println("*** The length of file1.getFileDate() is " + file1.getFileData().length + ". ***");
            if(docs[count].getContent() != null){
              System.out.println("*** the length of docs[" + count + "].getContent() is " + docs[count].getContent().length + ". ***");
            }else{
              System.out.println("*** the length of docs[" + count + "].getContent() is null. ***");
            }
          }
          */
          // end debug
          count++;
        }
        if (file2.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file2.getFileName());
          if(bean.getFile2_type().equalsIgnoreCase(""))
            docs[count].setType(file2.getContentType());
          else
            docs[count].setType(bean.getFile2_type());
          docs[count].setContent(file2.getFileData());
          count++;
        }
        if (file3.getFileSize() > 0) {
          docs[count] = new ClsNodeDocument();
          docs[count].setName(file3.getFileName());
          if(bean.getFile3_type().equalsIgnoreCase(""))
            docs[count].setType(file3.getContentType());
          else
            docs[count].setType(bean.getFile3_type());
          docs[count].setContent(file3.getFileData());
          count++;
        }
      }
      if (docs != null)
        this.Log("SubmitAction.do: The Doc Array has " + docs.length + " Documents", Level.DEBUG);
      else
        this.Log("SubmitAction.do: The Doc Array is Null", Level.DEBUG);
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      // debug
      /*
      System.out.println("*** SubmitAction: submit method --> check document ***");
      for(int i = 0; i < docs.length; i++){
        System.out.println("doc " + (i+1) + ": " + docs[i].getName() + ", " + docs[i].getType());
        if(docs[i].getContent() == null)
          System.out.println("content of document " + (i+1) + " is null.");
      }
      System.out.println("*** SubmitAction: end of submit method --> check document ***");
      */
      // end debug
      bean.setResult(requestor.submit(token, transID, dataFlow, flowOperation,recipientList,notificationURIList,notificationTypes,docs));
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    } catch (Exception e) {
      this.Log("SubmitAction.do: Error - " + e.getMessage(), Level.ERROR);
      bean.setError("Could Not Submit Files");
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    }
  }

}
