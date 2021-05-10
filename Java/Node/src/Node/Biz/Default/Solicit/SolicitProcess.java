package Node.Biz.Default.Solicit;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.net.URL;
import java.rmi.RemoteException;
import java.util.Hashtable;
import java.util.ResourceBundle;

import javax.naming.InitialContext;
import javax.naming.NamingException;

import net.exchangenetwork.www.schema.node._2.NotificationMessageCategoryType;
import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;
import net.exchangenetwork.www.schema.node._2.TransactionStatusCode;

import org.apache.axis2.databinding.types.Id;
import org.apache.log4j.Level;

import Node.Phrase;
import Node.API.DocumentManagement;
import Node.API.Email;
import Node.API.NodeUtils;
import Node.Biz.Administration.OperationLog;
import Node.Biz.Custom.PostParam;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create Solicit Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SolicitProcess {
  private XmlDocument Config = null;
  private String TransID = null;
  private String RequestorIP = null;

  /**
   * Constructor.
   * @param doc The input xml document
   * @param transID The transaction ID
   * @param requestorIP The requestor ID
   * @throws Exception
   * @return 
   */
  public SolicitProcess(XmlDocument doc, String transID, String requestorIP) throws Exception {
    if (doc == null)
      throw new Exception("No Process Configuration File Found");
    this.Config = doc;
    this.TransID = transID;
    this.RequestorIP = requestorIP;
  }

  /**
   * ExecuteSolicit
   * @param token The authentication token
   * @param returnURL The reutrn URL of node
   * @param request The request type
   * @param params The parameter object array
   * @param loggerName The name of logger
   * @param userName The user name 
   * @param password The user password 
   * @throws RemoteException
   * @return 
   */
  // handle node 1.1
  public void ExecuteSolicit (String token, String returnURL, String request, Object[] params, String loggerName, String userName, String password) throws RemoteException
  {
    try {
      NodeUtils nodeUtils = new NodeUtils();
      // Execute Process
      XmlNode procNode = this.Config.SelectSingleNode("/Operation/Process");
      ProcParam procParam = new ProcParam(this.TransID,this.RequestorIP,loggerName,userName,password,new Hashtable());
      Object[] p = new Object[] { token, returnURL, request, params, procParam };
      ClsNodeDocument retDoc = this.Execute(procNode,p);
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "Execute ended successfully.",false);
      // Store In Database
      DocumentManagement docManager = new DocumentManagement(Phrase.WebServicesLoggerName);
      // WI 21157
      if(retDoc != null && retDoc.getName() != null)
      {
          docManager.Upload(new ClsNodeDocument[]{retDoc},this.TransID,"Executed",request,returnURL,token,Utility.GetNowDate(),Utility.GetNowTimeStamp(), null);
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Saved", "Document is saved successfully with Transaction ID "+this.TransID,false);    	  
          // Return to URL
          String submitTranId = "";
          if(returnURL != null && !returnURL.equalsIgnoreCase(""))
          {
            nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Submitting", "Try to submit documen to "+returnURL,false);
            submitTranId = this.ReturnToURL(this.Config.SelectSingleNode(
                "/Operation/Process/Solicit/SubmitCredentials"), returnURL, retDoc,
                                            request);
            nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Submitted", "Completed successfully with return transaction id "+submitTranId,false);
          }
      }else if(retDoc != null && retDoc.getName() == null ){
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "Missing document name, there is no document generated ",false);    	      	  
      }else{
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "There is no document generated.",false);    	      	  
      }

      // Execute Post Process
      ExecuteOperation execute = new ExecuteOperation();
      p = new Object[] { token, returnURL, request, params };
      PostParam postParam = execute.ExecutePostProcesses(Phrase.WEB_METHOD_SOLICIT,this.Config.SelectSingleNode("/Operation/PostProcess"),p,this.TransID,this.RequestorIP,loggerName,userName,password,procParam.GetHashtable(),retDoc);

      // Handle Status
      //INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
      //String status = logDB.GetStatus(this.TransID);
      //if (status != null && status.equalsIgnoreCase(Phrase.ReceivedStatus)) {

        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, Phrase.CompleteStatus, Phrase.CompleteMessage,true);
      //}
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
  }

  /**
   * ExecuteSolicit
   * @param token The authentication token
   * @param dataflow The dataflow name
   * @param request The request type
   * @param recipients The recipients array
   * @param notificationURIType The notificationURI object array
   * @param params The parameter object array
   * @param loggerName The name of logger
   * @param userName The user name 
   * @param password The user password 
   * @throws RemoteException
   * @return 
   */
  // handle node 2.0
  public void ExecuteSolicit (String token, String dataflow,String request,String[] recipients,Object[] notificationURIType, Object[] params, String loggerName, String userName, String password) throws RemoteException
  {
	String recipientStr = null;
    try {
      NodeUtils nodeUtils = new NodeUtils();
      // Execute Process
      XmlNode procNode = this.Config.SelectSingleNode("/Operation/Process");
      ProcParam procParam = new ProcParam(this.TransID,this.RequestorIP,loggerName,userName,password,new Hashtable());
      Object[] p = new Object[] { token, dataflow, request, recipients,notificationURIType, params, procParam };
      ClsNodeDocument retDoc = this.Execute(procNode,p);
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "Execute ended successfully.",false);
      // WI 21157
      if(retDoc != null && retDoc.getName() != null)
      {
          // Store In Database
          DocumentManagement docManager = new DocumentManagement(Phrase.WebServicesLoggerName);
          for(int i=0;recipients!=null && i<recipients.length;i++){
          	if(i==0){
          		recipientStr = recipients[i];
          	}else{
          		recipientStr = recipientStr + "," + recipients[i];
          	}       	
          }
          docManager.Upload(new ClsNodeDocument[]{retDoc},this.TransID,"Executed",request,recipientStr,token,Utility.GetNowDate(),Utility.GetNowTimeStamp(), null);
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Saved", "Document is saved successfully with Transaction ID "+this.TransID,false);
          // Return to recipients
          String retTranId = "";
          String[] retTranIdArr = null;
          if(recipients != null){
              for(int i=0;i<recipients.length;i++){
            	  if(Utility.IsValidateEmail(recipients[i])){
            		  this.sendRecipientEmail(procParam, recipients[i],request);
            	  }else{
                      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Submitting", "Try to submit document to " + recipients[i],false);
                      retTranId = this.ReturnToURLV2(this.Config.SelectSingleNode(
                          "/Operation/Process/Solicit/SubmitCredentials"), recipients[i], retDoc,
                                                      request);
                      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Submitted", "Call Submit successfully with return transaction id: "+retTranId,false);        		  
            	  }       	  
              }
          }
          // Return to notificationURI
          if(notificationURIType != null){
        	  ISystemConfiguration configDB = DBManager.GetSystemConfiguration(loggerName);
        	  String returnURL = configDB.GetNodeURL_V2();
    	      for(int i=0;i<notificationURIType.length;i++){
    	    	  String URI = ((NotificationURIType)notificationURIType[i]).getString();
    	    	  if(Utility.IsValidateEmail(URI)){
    	    		  this.sendNotificationEmail(procParam, URI,request);
    	    	  }else{
    	              nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, Phrase.ProcessingStatus, "Try to call Notify to " + URI,false);
    	              retTranIdArr = this.ReturnToNotification(this.Config.SelectSingleNode(
    	                  "/Operation/Process/Solicit/SubmitCredentials"), URI,returnURL, retDoc,
    	                                              request,procParam);
    	              nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Notify", "Call Notify successfully with return transaction id: "+retTranIdArr[2]+".",false);        		  
    	    	  }       	  
    	      }
          }      
      }else if(retDoc != null && retDoc.getName() == null ){
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "Missing document name, there is no document generated ",false);    	      	  
      }else{
          nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, "Executed", "There is no document generated.",false);    	      	      	  
      }

      // Execute Post Process
      ExecuteOperation execute = new ExecuteOperation();
      p = new Object[] { token, recipients, request, params };
      PostParam postParam = execute.ExecutePostProcesses(Phrase.WEB_METHOD_SOLICIT,this.Config.SelectSingleNode("/Operation/PostProcess"),p,this.TransID,this.RequestorIP,loggerName,userName,password,procParam.GetHashtable(),retDoc);

      // Handle Status
      //INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
      //String status = logDB.GetStatus(this.TransID);
      //if (status != null && status.equalsIgnoreCase(Phrase.ReceivedStatus)) {

        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, Phrase.CompleteStatus, Phrase.CompleteMessage,true);
      //}
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
  }

  /**
   * Execute
   * @param procNode The process xml node which includes class name
   * @param params The parameter object array
   * @throws RemoteException
   * @return ClsNodeDocument The node document object
   */
  private ClsNodeDocument Execute (XmlNode procNode, Object[] params) throws RemoteException
  {
    ClsNodeDocument retDoc = null;
    try {
      XmlNode classNameNode = procNode.SelectSingleNode("ClassName");
      if (classNameNode != null)
        retDoc = this.Execute(classNameNode.GetInnerText(), params);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retDoc;
  }

  /**
   * Execute
   * @param className The process class name
   * @param params The parameter object array
   * @throws RemoteException
   * @return ClsNodeDocument The node document object
   */
  private ClsNodeDocument Execute (String className, Object[] params) throws RemoteException
  {
    ClsNodeDocument retDoc = null;
    try {
      Class executeClass = Class.forName(className);
      Class[] interfaces = executeClass.getInterfaces();
      if (interfaces != null && interfaces.length > 0) {
        Object obj = executeClass.newInstance();
        for (int i = 0; i < interfaces.length; i++) {
          String name = interfaces[i].getName();
          if (name.equalsIgnoreCase("Node.Biz.Interfaces.Solicit.IProcess")) {
            Node.Biz.Interfaces.Solicit.IProcess executeInterface = (Node.Biz.Interfaces.Solicit.IProcess)obj;
            if(params.length<6) retDoc = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(ProcParam)params[4]);
            else{
            	String[] tList = {""};
            	NotificationURIType[] nList = new NotificationURIType[1];
            	String t = "";
            	nList[0] = new NotificationURIType();
            	((ProcParam)params[6]).GetHashtable().put("recipients", params[3]==null?tList:(String[])params[3]);
            	((ProcParam)params[6]).GetHashtable().put("notificationURIType", params[4]==null?nList:(NotificationURIType[])params[4]);
                   	
            	retDoc = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[5],(ProcParam)params[6]);
            }
          }
        }
      }
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retDoc;
  }

  /**
   * ReturnToURL
   * @param urlNode The xml node of url
   * @param returnURL The reutrn url
   * @param result The return node document object
   * @param request The request type
   * @throws RemoteException
   * @return String The return transaction ID
   */
  private String ReturnToURL (XmlNode urlNode, String returnURL, ClsNodeDocument result, String request) throws RemoteException
  {
    String retTransID = null;
    try {
      if (urlNode != null && returnURL != null) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.TaskLoggerName);
        String dataFlow = opDB.GetDomainName(request, Phrase.WEB_METHOD_SOLICIT);
        NodeRequestor requestor = new NodeRequestor(new URL(returnURL),Phrase.WebServicesLoggerName);
        String uid = urlNode.SelectSingleNode("UserID").GetInnerText();
        String pwd = urlNode.SelectSingleNode("Password").GetInnerText();
        if (uid != null && pwd != null) {
          Cryptography crypt = new Cryptography();
          String token = requestor.authenticate(uid,crypt.Decrypting(pwd,Phrase.CryptKey),"PASSWORD");
          if (token != null) {
            //CDX does not take transId
            //retTransID = requestor.submit(token,this.TransID,dataFlow,new ClsNodeDocument[]{result});
            retTransID = requestor.submit(token,"",dataFlow,new ClsNodeDocument[]{result});
          }
        }
      }
    } catch (RemoteException e) {
      LoggingUtils.Log("Could Not Submit to Return URL: "+e.toString(),Level.WARN,Phrase.WebServicesLoggerName);
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retTransID;
  }

  /**
   * ReturnToURLV2
   * @param urlNode The xml node of url
   * @param returnURL The reutrn url
   * @param result The return node document object
   * @param request The request type
   * @throws RemoteException
   * @return String The return transaction ID
   */
  private String ReturnToURLV2 (XmlNode urlNode, String returnURL, ClsNodeDocument result, String request) throws RemoteException
  {
    String retTransID = null;
    try {
      if (urlNode != null && returnURL != null) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.TaskLoggerName);
        String dataFlow = opDB.GetDomainName(request, Phrase.WEB_METHOD_SOLICIT);
        Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(new URL(returnURL),Phrase.WebServicesLoggerName);
        String uid = urlNode.SelectSingleNode("UserID").GetInnerText();
        String pwd = urlNode.SelectSingleNode("Password").GetInnerText();
        if (uid != null && pwd != null) {
          Cryptography crypt = new Cryptography();
          String password = crypt.Decrypting(pwd,Phrase.CryptKey);
          String token = requestor.authenticate(uid,password,"PASSWORD",null);
          if (token != null) {
            retTransID = requestor.submit(token, "", dataFlow, "default", null, null, "None", new ClsNodeDocument[]{result});
          }
        }
      }
    } catch (RemoteException e) {
      LoggingUtils.Log("Could Not Submit to Return URL: "+e.toString(),Level.WARN,Phrase.WebServicesLoggerName);
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retTransID;
  }
  
  /**
   * ReturnToNotification
   * @param urlNode The xml node of url
   * @param nodeAddress The remote node address where need to be send notify
   * @param returnURL The reutrn url
   * @param result The return node document object
   * @param request The request type
   * @param procParam The process parameter
   * @throws RemoteException
   * @return String[] The return transaction ID array
   */
  private String[] ReturnToNotification (XmlNode urlNode,String nodeAddress, String returnURL, ClsNodeDocument result, String request,ProcParam procParam) throws RemoteException
  {
    String[] retTransID = null;
    try {
      if (urlNode != null && nodeAddress != null) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        String dataFlow = opDB.GetDomainName(request, Phrase.WEB_METHOD_SOLICIT);
        Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(new URL(nodeAddress),Phrase.WebServicesLoggerName);
        String uid = urlNode.SelectSingleNode("UserID").GetInnerText();
        String pwd = urlNode.SelectSingleNode("Password").GetInnerText();
        if (uid != null && pwd != null) {
	        Cryptography crypt = new Cryptography();
	        String password = crypt.Decrypting(pwd,Phrase.CryptKey);
	        String token = requestor.authenticate(uid,password,"PASSWORD",null);
	        if (token != null) {
	            NotificationMessageType[] notificationMessageType = new NotificationMessageType[1];
	            
	            Id id = new Id(procParam.GetTransID());
	            String messageName = request;
	            String messageType = Phrase.NOTIFY_DOCUMENT;
	            String status = Phrase.ProcessingStatus;
	            String statusDetail = Phrase.ProcessingMessage;
	            notificationMessageType[0] = new NotificationMessageType();
	            notificationMessageType[0].setObjectId(id);
	            notificationMessageType[0].setMessageName(messageName);
	            NotificationMessageCategoryType notificationMessageCategoryType = NotificationMessageCategoryType.Factory.fromValue(messageType);
	            notificationMessageType[0].setMessageCategory(notificationMessageCategoryType);
	            TransactionStatusCode transactionStatusCode = TransactionStatusCode.Factory.fromValue(status);
	            notificationMessageType[0].setStatus(transactionStatusCode);
	            notificationMessageType[0].setStatusDetail(statusDetail);
	            retTransID = requestor.notify(token, returnURL, dataFlow, notificationMessageType);
	        }
        }
      }
    } catch (RemoteException e) {
      LoggingUtils.Log("Could Not Submit to Return Notification: "+e.toString(),Level.WARN,Phrase.WebServicesLoggerName);
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retTransID;
  }

  /**
   * sendRecipientEmail
   * @param procParam The process parameter
   * @param receiver The email receiver
   * @param request The request type
   * @throws Exception
   * @return boolean The status of sending email
   */
  public boolean sendRecipientEmail(ProcParam param,String receiver,String request) throws Exception {
	    boolean ret = false;
		ISystemConfiguration configDB = DBManager.GetSystemConfiguration(param.GetLoggerName());
		/*OperationLog[] oplog = OperationLog.SearchOperationLog(param.GetLoggerName(),new Node.Biz.Administration.User(param.GetUserName()),request,Phrase.WEB_SERVICE_OPERATION,
				Phrase.WEB_METHOD_SOLICIT,null,null,null,null,param.GetTransID(),null,null, Phrase.ver_2);*/
		Email email = new Email(configDB.GetEmailServerHost(), configDB.GetEmailServerPort(), configDB.GetUserEmailUID(), configDB.GetUserEmailPWD());
		String subject = "Node submission Notification.";
		String content = " This email provides notice that a user has submited a file with transaction ID: " + param.GetTransID()
						+ ",\n The receiving Node address is: " + configDB.GetNodeURL_V2()
						+ ",\n The originating Node address is: " + param.GetRequestorIP();
						//+ ",\n The originating Node address is: " + oplog[0].GetHostName();
		ret = email.SendEmail(subject,content, param.GetLoggerName(),
				receiver, null, null,null,null,null);
	    return ret;
	  }

  /**
   * sendNotificationEmail
   * @param procParam The process parameter
   * @param receiver The email receiver
   * @param request The request type
   * @throws Exception
   * @return boolean The status of sending email
   */
  public boolean sendNotificationEmail(ProcParam param,String receiver,String request) throws Exception {
	    boolean ret = false;
		ISystemConfiguration configDB = DBManager.GetSystemConfiguration(param.GetLoggerName());
		Email email = new Email(configDB.GetEmailServerHost(), configDB.GetEmailServerPort(), configDB.GetUserEmailUID(), configDB.GetUserEmailPWD());
		String subject = "Node submission Notification.";
		String content = " This email provides notice that a user has submited a file with transaction ID: " + param.GetTransID()
						+ ",\n The status code is: " + Phrase.ProcessingStatus
						+ ",\n The status detail is: " + Phrase.ProcessingMessage;
		ret = email.SendEmail(subject,content, param.GetLoggerName(),
				receiver, null, null,null,null,null);
	    return ret;
	  }

}
