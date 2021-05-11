package Node.Web.Client.Action.WebMethods;

import java.net.URL;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import net.exchangenetwork.www.schema.node._2.NotificationMessageCategoryType;
import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.TransactionStatusCode;

import org.apache.axis2.databinding.types.Id;
import org.apache.axis2.databinding.types.NCName;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;

import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.NotifyBean;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create NotifyAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NotifyAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public NotifyAction() {
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
    NotifyBean bean = (NotifyBean)form;
    String ret = null;
    String version = request.getParameter("version");
    this.SetPage(bean);
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equalsIgnoreCase("NOTIFY"))
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.Notify(bean);
      		ret = "notify";
      	}else{
            this.NotifyV2(bean);
      		ret = "notifyV2";
      	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "notify";
    	}else{
    		ret = "notifyV2";
    	}    	
    }
	return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (NotifyBean bean)
  {
    super.SetPage(bean);
  }

  /**
   * Notify
   * @param bean
   * @return 
   */
  private void Notify (NotifyBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String nodeAddress = bean.getNodeAddress5();
    if (nodeAddress != null && nodeAddress.equals(""))
      nodeAddress = null;
    String dataFlow = bean.getDataFlow();
    if (dataFlow != null && dataFlow.equals(""))
      dataFlow = null;
    String docName1 = bean.getDocName1();
    if (docName1 != null && docName1.equals(""))
      docName1 = null;
    String docType1 = bean.getDocType1();
    if (docType1 != null && docType1.equals(""))
      docType1 = null;
    String docName2 = bean.getDocName2();
    if (docName2 != null && docName2.equals(""))
      docName2 = null;
    String docType2 = bean.getDocType2();
    if (docType2 != null && docType2.equals(""))
      docType2 = null;
    String docName3 = bean.getDocName3();
    if (docName3 != null && docName3.equals(""))
      docName3 = null;
    String docType3 = bean.getDocType3();
    if (docType3 != null && docType3.equals(""))
      docType3 = null;
    try {
      int count = 0;
      FormFile docContent1 = bean.getDocContent1();
      ClsNodeDocument doc1 = null;
      if (docName1 != null || docContent1 != null) {
        doc1 = new ClsNodeDocument();
        doc1.setName(docName1 != null ? docName1 : docContent1.getFileName());
        doc1.setType(docType1 != null ? docType1 : Phrase.OTHER_TYPE);
        if (docContent1 != null)
          doc1.setContent(docContent1.getFileData());
        count++;
      }
      FormFile docContent2 = bean.getDocContent2();
      ClsNodeDocument doc2 = null;
      if (docName2 != null || docContent2 != null) {
        doc2 = new ClsNodeDocument();
        doc2.setName(docName2 != null ? docName2 : docContent2.getFileName());
        doc2.setType(docType2 != null ? docType2 : Phrase.OTHER_TYPE);
        if (docContent2 != null)
          doc2.setContent(docContent2.getFileData());
        count++;
      }
      FormFile docContent3 = bean.getDocContent3();
      ClsNodeDocument doc3 = null;
      if (docName3 != null || docContent3 != null) {
        doc3 = new ClsNodeDocument();
        doc3.setName(docName3 != null ? docName3 : docContent3.getFileName());
        doc3.setType(docType3 != null ? docType3 : Phrase.OTHER_TYPE);
        if (docContent3 != null)
          doc3.setContent(docContent3.getFileData());
        count++;
      }
      ClsNodeDocument[] docs = null;
      if (count > 0) {
        docs = new ClsNodeDocument[count];
        count = 0;
        if (doc1 != null) {
          docs[count] = doc1;
          count++;
        }
        if (doc2 != null) {
          docs[count] = doc2;
          count++;
        }
        if (doc3 != null) {
          docs[count] = doc3;
          count++;
        }
      }
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setResult(requestor.notify(token, nodeAddress, dataFlow, docs));
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    } catch (Exception e) {
      bean.setError("Could Not Submit Files");
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    }
  }
  /**
   * NotifyV2
   * @param bean
   * @return 
   */
  private void NotifyV2 (NotifyBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = "";
    String nodeAddress = bean.getNodeAddress5();
    if (nodeAddress != null && nodeAddress.equals(""))
      nodeAddress = "";
    String dataFlow = bean.getDataFlow();
    if (dataFlow != null && dataFlow.equals(""))
      dataFlow = "";
    String objectId = bean.getObjectId();
    if (objectId != null && objectId.equals(""))
    	objectId = "";
    String messageName = bean.getMessageName();
    if (messageName != null && messageName.equals(""))
    	messageName = "";
    String messageType = bean.getMessageType();
    if (messageType != null && messageType.equals(""))
    	messageType = "";
    String status = bean.getStatus();
    if (status != null && status.equals(""))
    	status = "";
    String statusDetail = bean.getStatusDetail();
    if (statusDetail != null && statusDetail.equals(""))
    	statusDetail = "";
    try {
      URL node = new URL(this.GetNodeAddress_V2(bean));
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      NotificationMessageType[] notificationMessageType = new NotificationMessageType[1];

      Id id = null;
      if(objectId.indexOf("_")==-1){
          id = new Id("_"+objectId);
      }else{
    	  id = new Id(objectId);
      }
      notificationMessageType[0] = new NotificationMessageType();
      notificationMessageType[0].setObjectId(id);
      notificationMessageType[0].setMessageName(messageName);
      NotificationMessageCategoryType notificationMessageCategoryType = NotificationMessageCategoryType.Factory.fromValue(messageType);
      notificationMessageType[0].setMessageCategory(notificationMessageCategoryType);
      TransactionStatusCode transactionStatusCode = TransactionStatusCode.Factory.fromValue(status);
      notificationMessageType[0].setStatus(transactionStatusCode);
      notificationMessageType[0].setStatusDetail(statusDetail);
      String ret[] = requestor.notify(token, nodeAddress, dataFlow, notificationMessageType);
      
      bean.setResult(ret[0] + ".   " + ret[1]);
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    } catch (Exception e) {
      bean.setError("Could Not Submit Files");
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    }
  }
}
