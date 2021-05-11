package Node.Biz.Default.Submit;

import java.net.URL;
import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.NotificationMessageCategoryType;
import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.DocumentManagement;
import Node.API.Email;
import Node.API.NodeUtils;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Submit.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;

import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create Submit Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Process implements IProcess {
	  /**
	   * Constructor.
	   * @param
	   * @return 
	   */
  public Process() {
  }

/*  public String Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException
  {
    if (docs == null || docs.length <= 0)
      throw new RemoteException(Phrase.InvalidParameter);
    String retString = null;
    try {
      DocumentManagement docManage = new DocumentManagement(Phrase.WebServicesLoggerName);
      docManage.Upload(docs,param.GetTransID(),"Received",dataFlow,null,token,Utility.GetNowDate(),Utility.GetNowTimeStamp(), null);
      retString = param.GetTransID();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retString;
  }
*/  
  /**
   * Execute
   * @param token The authentication token
   * @param transID The transaction ID
   * @param dataFlow The data flow name
   * @param docs The submit file array
   * @param param The process parameter 
   * @throws RemoteException
   * @return String The query result
   */
	public String Execute(String token, String transID, String dataFlow,
			ClsNodeDocument[] docs, ProcParam param) throws RemoteException {
		String ret=null;
		NodeUtils utils = new NodeUtils();
		boolean retBool = false;
        String[] opList = null;
        boolean validDataFlow = false;
        boolean validFlowOperation = false;
        String trasactionID = transID;
        String flowOperation=null;
        
		String[] recipients = (String[])param.GetHashtable().get("recipients");
		NotificationURIType[] notificationURIType = (NotificationURIType[])param.GetHashtable().get("notificationURIType");
		String[] notificationURIs = notificationURIType==null || notificationURIType.length==0?null: new String[notificationURIType.length];
		if(transID==null || transID.equalsIgnoreCase("")) trasactionID = param.GetTransID();// create a new transaction ID
        INodeDomain domainDB = null;
        INodeOperation opDB = null;
        String[] domainNameList;
		try {	// valide dataflow
			domainDB = DBManager.GetNodeDomain(Phrase.WebServicesLoggerName);
			opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
			domainNameList = domainDB.GetDomains();
			if(param.GetHashtable().get(WebServiceParameter.dataflow)!=null && !param.GetHashtable().get(WebServiceParameter.dataflow).equals("")){
				for(int i=0;i<domainNameList.length;i++){
					if(domainNameList[i].equalsIgnoreCase((String)param.GetHashtable().get(WebServiceParameter.dataflow))){
						validDataFlow = true;
						break;
					}            		
				}    	  
			}else{
				validDataFlow = true;
			}
		} catch (Exception e1) {
			throw new RemoteException(Phrase.InternalError, e1);
		}
		if(validDataFlow && param.GetHashtable().containsKey(WebServiceParameter.flowOperation)){	// handle 2.0
			try {	// valide flow Operation
				flowOperation = (String)param.GetHashtable().get(WebServiceParameter.flowOperation);
				if(flowOperation!=null && !(flowOperation.equals(""))){
					for(int i=0;i<domainNameList.length;i++){
						if(validFlowOperation) break;
						opList = opDB.GetOperations(domainNameList[i]);
						for(int j=0;opList != null && j<opList.length;j++){
							if(opList[j]!=null && opList[j].equalsIgnoreCase(flowOperation)){
								validFlowOperation = true;
								break;
							}
						}
					}    	  
				}
			} catch (Exception e1) {
				throw new RemoteException(Phrase.InternalError, e1);
			}
			if(!validFlowOperation){
				utils.UpdateOperationLog(param.GetLoggerName(),
						trasactionID, Phrase.FailedStatus, "Invalid Flow Operation: " + flowOperation, true);								
				return Phrase.InvalidFlowOperation;
			}
			
			if (docs == null || docs.length <= 0){				
				utils.UpdateOperationLog(param.GetLoggerName(),
						trasactionID, Phrase.FailedStatus, "File is not found. ", true);								
				return Phrase.FileNotFound;				
			}
			try {
				DocumentManagement docManage = new DocumentManagement(param.GetLoggerName());
				retBool = docManage.Upload(docs, trasactionID, "Received", (String)param.GetHashtable().get(WebServiceParameter.flowOperation),
						null, token, Utility.GetNowDate(), Utility.GetNowTimeStamp(), null);
				if(retBool){
				    LoggingUtils.Log(" token is:"+ token +" transID is: "+trasactionID +" dataFlow is:"+dataFlow+" docs name is:"+ (docs!=null && docs.length>0?docs[0].getName():"null")+" docs type is:"+ (docs!=null && docs.length>0?docs[0].getType():"null")
				    		,Level.DEBUG,Phrase.WebServicesLoggerName);
					ISystemConfiguration configDB = DBManager.GetSystemConfiguration(param.GetLoggerName());
				    String content = null;
					Email email = new Email(configDB.GetEmailServerHost(), configDB.GetEmailServerPort(), configDB.GetUserEmailUID(), configDB.GetUserEmailPWD());
					if (recipients != null && recipients.length>0) {
						for(int i=0;i<recipients.length;i++){
						    LoggingUtils.Log("recipients["+i+"] is:" + recipients[i],Level.DEBUG,Phrase.WebServicesLoggerName);
							retBool = Utility.checkEmail(recipients[i]);
							if(retBool){	// send email
								content = this.getRecipientsTemplate(configDB.GetEmailTemplateLocation(), trasactionID, configDB.GetNodeURL(), param.GetRequestorIP());
								retBool = email.SendEmail(configDB.GetUserEmailSubject(),
										content, configDB.GetUserEmailSenderEmail(),
										recipients[i], null, null);
								if(!retBool){
									utils.UpdateOperationLog(param.GetLoggerName(),
											trasactionID, Phrase.FailedStatus, "Fail to send email to " + recipients[i], true);								
								}
							}else if(recipients[i]!=null && !recipients[i].equalsIgnoreCase("") && recipients[i].substring(0, 4).equalsIgnoreCase("http")){	// transfer to another node
								retBool = sendAnotherNode(recipients[i], trasactionID, dataFlow,flowOperation, docs, param);
								if(!retBool){
									utils.UpdateOperationLog(param.GetLoggerName(),
											trasactionID, Phrase.FailedStatus, "Fail to submit document to " + recipients[i], true);								
								}
							}
						}
					}
					if(notificationURIType!=null && notificationURIType.length>0){
						for(int i=0; i<notificationURIType.length;i++){
							notificationURIs[i] = notificationURIType[i].getString();
							 LoggingUtils.Log("notificationURIs["+i+"] is:" + notificationURIType[i].getString()
									 ,Level.DEBUG,Phrase.WebServicesLoggerName);
							if(notificationURIs[i]!=null && !notificationURIs[i].equalsIgnoreCase("")&& !notificationURIs[i].equalsIgnoreCase("null")){
								retBool = Utility.checkEmail(notificationURIs[i]);
								if(retBool){	// send email
									content = this.getNotificationTemplate(configDB.GetEmailTemplateLocation(), param.GetUserName(), null, null);
									retBool = email.SendEmail(configDB.GetUserEmailSubject(),
											content, configDB.GetUserEmailSenderEmail(),
											notificationURIs[i], null, null);
									if(!retBool){
										utils.UpdateOperationLog(param.GetLoggerName(),
												trasactionID, Phrase.FailedStatus, "Fail to send email to " + notificationURIs[i], true);								
									}else{
										utils.UpdateOperationLog(param.GetLoggerName(),
												trasactionID, Phrase.ProcessingStatus, "Sending email to: " + notificationURIs[i], true);																	
									}
								}else if(notificationURIs[i]!=null && !notificationURIs[i].equalsIgnoreCase("") && notificationURIs[i].substring(0, 4).equalsIgnoreCase("http")){	// transfer to another node
									retBool = notifyAnotherNode(notificationURIs[i], dataFlow, docs, param);
									if(!retBool){
										utils.UpdateOperationLog(param.GetLoggerName(),
												trasactionID, Phrase.FailedStatus, "Fail to notify: " + notificationURIs[i], true);								
									}else{
										utils.UpdateOperationLog(param.GetLoggerName(),
												trasactionID, Phrase.ProcessingStatus, "Notify another node: " + notificationURIs[i], true);								
										
									}
								}							
							}
						}
					}
					if(retBool){
						utils.UpdateOperationLog(param.GetLoggerName(),
								trasactionID, Phrase.ReceivedStatus, "File received. Finish sending email or notifying other nodes. ", true);								
						ret = trasactionID+";"
						+	Phrase.ReceivedStatus+";"
						+	Phrase.ReceivedMessage;			
					}else{
						//transactionStatusCode = TransactionStatusCode.Factory.fromValue(Phrase.WarningStatus);
						// TODO : must be set back to warning
						utils.UpdateOperationLog(param.GetLoggerName(),
								trasactionID, Phrase.FailedStatus, "File received. But send email or send to other Nodes fail. "+ Phrase.WarningMessage, true);								
						ret = trasactionID+";"
						+	Phrase.UnknownStatus+";"
						+	Phrase.WarningMessage;			
					}					
				}else{
					utils.UpdateOperationLog(param.GetLoggerName(),
							trasactionID, Phrase.FailedStatus, "Fail to add file.", true);								
					ret = Phrase.InternalError;					
				}
			} catch (Exception e) {
				throw new RemoteException(Phrase.InternalError, e);
			}
		}else if(validDataFlow){		// handle 1.1
		    if (docs == null || docs.length <= 0)
		        return Phrase.InvalidParameter;
		      try {
		        DocumentManagement docManage = new DocumentManagement(Phrase.WebServicesLoggerName);
		        docManage.Upload(docs,trasactionID,"Received",dataFlow,null,token,Utility.GetNowDate(),Utility.GetNowTimeStamp(), null);
		        ret = trasactionID;
		      } catch (RemoteException e) {
		        throw e;
		      } catch (Exception e) {
		        throw new RemoteException(Phrase.InternalError, e);
		      }
		}else if(!validDataFlow){
			utils.UpdateOperationLog(param.GetLoggerName(),
					trasactionID, Phrase.FailedStatus, "Invalid dataflow:  " + dataFlow, true);								
			ret = Phrase.InvalidDataFlow;
		}else ret = Phrase.Unknown;
		
		return ret;
	}

/*	public StatusResponseType Execute(String token, String transID,
			String dataFlow, String flowOperation, String[] recipients,
			NotificationURIType[] notificationURIType, ClsNodeDocument[] docs,
			ProcParam param) throws RemoteException {
		boolean retBool = false;
		StatusResponseType statusResponseType = new StatusResponseType();
		TransactionStatusCode transactionStatusCode = null;

		if (docs == null || docs.length <= 0)
			throw new RemoteException(Phrase.InvalidParameter);
		try {
			DocumentManagement docManage = new DocumentManagement(
					Phrase.WebServicesLoggerName);
			retBool = docManage.Upload(docs, param.GetTransID(), "Received", dataFlow,
					null, token, Utility.GetNowDate(), Utility.GetNowTimeStamp(), null);
			SystemConfiguration configDB = new SystemConfiguration(param
					.GetLoggerName());
		    String content = this.GetTemplate(configDB.GetEmailTemplateLocation(), null, null, null, null);
			Email email = new Email(configDB.GetEmailServerHost(), configDB.GetEmailServerPort(), configDB.GetUserEmailUID(), configDB.GetUserEmailPWD());
			if (recipients != null) {
				for(int i=0;i<recipients.length;i++){
					retBool = Utility.checkEmail(recipients[i]);
					if(retBool){
						retBool = email.SendEmail(configDB.GetUserEmailSubject(),
								content, configDB.GetUserEmailSenderEmail(),
								recipients[i], null, null);
						if(retBool) break;
					}else if(recipients[i]!=null && !recipients[i].equalsIgnoreCase("") && recipients[i].substring(0, 3).equalsIgnoreCase("http")){
						//System.out.println("invoke submit");
					}
				}
			}
			if(notificationURIType!=null){
				;
			}
			if(retBool){
				transactionStatusCode = TransactionStatusCode.Factory.fromValue(Phrase.ReceivedStatus);
				statusResponseType.setStatusDetail(Phrase.ReceivedMessage);
			}else{
				//transactionStatusCode = TransactionStatusCode.Factory.fromValue(Phrase.WarningStatus);
				// TODO : must be set back to warning
				transactionStatusCode = TransactionStatusCode.Factory.fromValue(Phrase.UnknownStatus);
				statusResponseType.setStatusDetail(Phrase.WarningMessage);
			}
			statusResponseType.setStatus(transactionStatusCode);
			statusResponseType.setTransactionId(param.GetTransID());
		} catch (Exception e) {
			throw new RemoteException(Phrase.InternalError, e);
		}
		return statusResponseType;
	}
*/
	  /**
	   * getRecipientsTemplate
	   * @param templateName The template name
	   * @param transId The transaction ID
	   * @param receivingNode The receive node name
	   * @param originatingNode The originating node name
	   * @throws RemoteException
	   * @return String The template
	   */
	private String getRecipientsTemplate(String templateName, String transId,String receivingNode,String originatingNode)
			throws RemoteException {
		String content = null;
		String webservicePath = Utility.getWebservicePath();
		try {
			// content = Utility.ReadToEnd(AppUtils.AppRoot + "/WEB-INF/config/"
			// + templateName);
//			content = com.enfotech.basecomponent.utility.Utility
//					.ReadToEnd(webservicePath + "../Node.Administration/WEB-INF/config/"
//							+ templateName);
			content = "Node Administration System Message"
				+"System Time: <%var_systime%>"
				+"Subject: Node Administration Submit Status"
				+"-------------------------------------------------------"
				+"This e-mail is in response to your Node submit request. Your submit information is as follows:"
				+"Transaction ID:"
				+"<%var_transId%>"
				+"Receiving Node: "
				+"<%var_receivingNode%>"
				+"Originating Node: "
				+"<%originatingNode%>";
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_systime%>",
					com.enfotech.basecomponent.utility.Utility.GetNow());
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_transId%>", transId);
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_receivingNode%>", receivingNode);
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%originatingNode%>", originatingNode);
		} catch (Exception e) {
			throw new RemoteException("Could Not Get Email Template: ", e);
		}
		return content;
	}

	  /**
	   * getNotificationTemplate
	   * @param templateName The template name
	   * @param login The login name
	   * @param statusCode The status code
	   * @param statusDetail The status detail
	   * @throws RemoteException
	   * @return String The template
	   */
 	private String getNotificationTemplate(String templateName, String login, String statusCode,
			String statusDetail)
			throws RemoteException {
		String content = null;
		String webservicePath = Utility.getWebservicePath();
		try {
			// content = Utility.ReadToEnd(AppUtils.AppRoot + "/WEB-INF/config/"
			// + templateName);
//			content = com.enfotech.basecomponent.utility.Utility
//					.ReadToEnd(webservicePath + "../Node.Administration/WEB-INF/config/"
//							+ templateName);
			content = "Node Administration System Message"
				+"System Time: <%var_systime%>"
				+"Subject: Node Administration Submit Status"
				+"-------------------------------------------------------"
				+"This e-mail is in response to your Node submit request. Your submit information is as follows:"
				+"Logon ID:"
				+"<%var_login%>"
				+"Status is: "
				+"<%var_status_code%>"
				+"Status message is: "
				+"<%var_status_detail%>";
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_systime%>",
					com.enfotech.basecomponent.utility.Utility.GetNow());
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_login%>", login);
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_status_code%>", statusCode);
			content = com.enfotech.basecomponent.utility.Utility.replace(
					content, "<%var_status_detail%>", statusDetail);
		} catch (Exception e) {
			throw new RemoteException("Could Not Get Email Template: ", e);
		}
		return content;
	}

	  /**
	   * sendAnotherNode
	   * @param url The url of Node
	   * @param transID The transaction ID
	   * @param dataFlow The data flow name
	   * @param flowOperation The flow operation name
	   * @param docs The document array
	   * @param param The process parameter 
	   * @throws RemoteException
	   * @return String The template
	   */
	private boolean sendAnotherNode (String url, String transID,String dataFlow,String flowOperation,
			ClsNodeDocument[] docs, ProcParam param){
		String userName = param.GetUserName();
        Cryptography crypt = new Cryptography();
		String password = null;
		String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
		String domainName = "";
		try {
			password = crypt.Decrypting(param.GetPassword(),Phrase.CryptKey);
			URL node = new URL(url);
			Node2.webservice.Requestor.NodeRequestor request = new Node2.webservice.Requestor.NodeRequestor(node, param.GetLoggerName());
			String token = request.authenticate(userName, password, authMethod,domainName);
			if (token != null && !token.equals("")) {
			      request.submit(token,transID, dataFlow, null,null,null,"All",docs);
			      return true;
			}else {
				return false;
			}
		} catch (RemoteException e) {
			return false;
		} catch (Exception e) {
			return false;
		}
	}
	
	  /**
	   * notifyAnotherNode
	   * @param url The url of Node
	   * @param dataFlow The data flow name
	   * @param docs The document array
	   * @param param The process parameter 
	   * @throws RemoteException
	   * @return String The template
	   */
	private boolean notifyAnotherNode (String url, String dataFlow,
			ClsNodeDocument[] docs, ProcParam param){
		String userName = param.GetUserName();
        Cryptography crypt = new Cryptography();
		String password = null;
		String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
		String domainName = "";
		try {
			password = crypt.Decrypting(param.GetPassword(),Phrase.CryptKey);
			URL node = new URL(url);
			Node2.webservice.Requestor.NodeRequestor request = new Node2.webservice.Requestor.NodeRequestor(node, param.GetLoggerName());
			String token = request.authenticate(userName, password, authMethod,domainName);
			if (token != null && !token.equals("")) {
				NotificationMessageType[] notificationMessageType = new NotificationMessageType[1];
				NotificationMessageCategoryType notificationMessageCategoryType = NotificationMessageCategoryType.Factory.fromValue("All");
				notificationMessageType[0].setMessageCategory(notificationMessageCategoryType);
				request.notify(token, url, dataFlow, notificationMessageType);
				return true;
			}else {
				return false;
			}
		} catch (RemoteException e) {
			return false;
		} catch (Exception e) {
			return false;
		}
	}

}
