/**
 * NetworkNodeBindingImpl.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 *
 * 1/22/04 Modified by Maggie
 * - Add impelmentation of Node.Biz.ClsManager()
 */

package Node.WebServices;

import java.net.InetAddress;

import org.apache.log4j.Level;
import Node.Biz.Handler.WebMethods.*;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ArrayofDocHolder;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Document.NodeDocument;
import Node.WebServices.Document.NodeDocumentContentConverter;
import Node.WebServices.Interfaces.NetworkNodePortType;

import java.rmi.*;
import Node.Phrase;
import org.apache.axis.*;
import javax.servlet.http.HttpServletRequest;
import org.apache.axis.transport.http.HTTPConstants;

import Node.Utils.LoggingUtils;

public class NetworkNodeBindingImpl implements NetworkNodePortType{

  /**
   * authenticate
   *
   * @param uid the user id.
   * @param cred the password.
   * @param authenticationMethod
   * @param clientHost the ip address of the requestor
   * @return token returned.
   */
  public java.lang.String authenticate(java.lang.String userId, java.lang.String credential, java.lang.String authenticationMethod) throws java.rmi.RemoteException {
    String result = null;
    try {
      AuthenticateHandler handler = new AuthenticateHandler(this.getClientHost(),this.GetClientHostName(),userId,credential,authenticationMethod);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Handle Authenticate " + authenticationMethod + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
      throw new RemoteException(Phrase.InternalError);
    }
    return result;
  }

  /**
   * submit
   *
   * @param securityToken the token string
   * @param transactionId the transaction ID (optional, currently is not in use) string
   * @param dataFlow the data flow string
   * @param documents the documents Node.CDX.ClsNodeDocument[]
   * @return the transaction ID
   */
  public java.lang.String submit(java.lang.String securityToken, java.lang.String transactionId, java.lang.String dataflow,
                                 Node.WebServices.Document.NodeDocument[] documents) throws java.rmi.RemoteException {
    String result = null;
    try {
      if (documents != null && documents instanceof NodeDocument[])
        LoggingUtils.Log("Submitting " + documents.length + " documents", Level.DEBUG, Phrase.WebServicesLoggerName);
      else
        LoggingUtils.Log("Submitting no documents", Level.DEBUG, Phrase.WebServicesLoggerName);
      ClsNodeDocument[] inputDocs = NodeDocumentContentConverter.convertToNodeDocument(documents);
      SubmitHandler handler = new SubmitHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId,dataflow,inputDocs);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Handle Submit " + dataflow + ": " + e.toString(),Level.ERROR,Phrase.WebServicesLoggerName);
      throw new RemoteException(Phrase.InternalError);
    }
    return result;
  }

  /**
   * getStatus
   *
   * @param securityToken the token string
   * @param transactionId the transaction ID
   * @return the status of specified transaction.
   */
  public java.lang.String getStatus(java.lang.String securityToken, java.lang.String transactionId) throws java.rmi.RemoteException {
    String result = null;
    try {
      GetStatusHandler handler = new GetStatusHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle GetStatus: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return result;
  }

  /**
   * Used by download web method
   *
   * @param securityToken the token string
   * @param transactionId the transaction ID (optional, currently is not in use) string
   * @param dataFlow the data flow string
   * @param documents the documents
   * @return the transaction ID
   */
  public void download(java.lang.String securityToken, java.lang.String transactionId, java.lang.String dataflow,
                       Node.WebServices.Document.ArrayofDocHolder documents) throws java.rmi.RemoteException {
    try {
      ClsNodeDocument[] receivedDocs = NodeDocumentContentConverter.convertToNodeDocument(documents.value);
      DownloadHandler handler = new DownloadHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId,dataflow,receivedDocs);
      ClsNodeDocument[] retDocs = (ClsNodeDocument[])handler.Invoke();
      documents.value = NodeDocumentContentConverter.convertToNodeDocument(retDocs);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle Download " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return;
  }

  /**
   * notify
   *
   * @param securityToken the token string
   * @param nodeAddress the node address
   * @param dataFlow the flow type
   * @param documents the documents
   * @return the string
   */
  public java.lang.String notify(java.lang.String securityToken, java.lang.String nodeAddress,
                                 java.lang.String dataflow, Node.WebServices.Document.NodeDocument[] documents) throws java.rmi.RemoteException {

    String result = null;
    try {
      ClsNodeDocument[] inputDocs = NodeDocumentContentConverter.convertToNodeDocument(documents);
      NotifyHandler handler = new NotifyHandler(this.getClientHost(),this.GetClientHostName(),securityToken,nodeAddress,dataflow,inputDocs);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle Notify " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return result;
  }

  /**
   * query
   *
   * @param securityToken the token string
   * @param request the data source string
   * @param rowId the number of rows
   * @param maxRows the max number of returning rows
   * @param parameters the parameters of query
   * @return the result set or transaction ID.
   */
  public java.lang.String query(java.lang.String securityToken, java.lang.String request,
                                java.math.BigInteger rowId, java.math.BigInteger maxRows, java.lang.String[] parameters) throws java.rmi.RemoteException {

    String result = null;
    try {
      QueryHandler handler = new QueryHandler(this.getClientHost(),this.GetClientHostName(),securityToken,request,rowId,maxRows,parameters);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle Query " + request + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return result;
  }

  /**
   * solicit
   *
   * @param securityToken the token string
   * @param returnURL the node address
   * @param dataFlow the flow type
   * @param parameters the parameters
   * @return the string
   */
  public java.lang.String solicit(java.lang.String securityToken, java.lang.String returnURL,
                                  java.lang.String request, java.lang.String[] parameters) throws java.rmi.RemoteException {
    String result = null;
    try {
      SolicitHandler handler = new SolicitHandler(this.getClientHost(),this.GetClientHostName(),securityToken,returnURL,request,parameters);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle Solicit " + request + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return result;
  }

  //** not implement
  /**
   * execute
   * @param securityToken
   * @param request
   * @param parameters
   * @return String
   */
   public java.lang.String execute(java.lang.String securityToken, java.lang.String request, java.lang.String[] parameters) throws java.rmi.RemoteException {
	    String result = null;
	    try {
	      ExecuteHandler handler = new ExecuteHandler(this.getClientHost(),this.GetClientHostName(),securityToken,request,Phrase.WEB_METHOD_EXECUTE,parameters);
	      result = (String)handler.Invoke();
	    } catch (RemoteException e) {
	      throw e;
	    } catch (Exception e) {
	      LoggingUtils.Log("Could not Handle Execute " + request + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    }
	    return result;
   }

	  /**
	   * nodePing
	   * @param hello
	   * @return String
	   */
  public java.lang.String nodePing(java.lang.String hello) throws java.rmi.RemoteException {
    String result = null;
    try {
      NodePingHandler handler = new NodePingHandler(this.getClientHost(),this.GetClientHostName(),hello);
      result = (String)handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle NodePing: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    // WI 21131
    //if (hello == null) {
    	return Utility.getVersion();
    //}else{
    //   	return hello;    	
    //}
  }

  /**
   * getServices
   *
   * @param securityToken the token string
   * @param serviceType the service type
   * @return list of services
   */
  public java.lang.String[] getServices(java.lang.String securityToken, java.lang.String serviceType) throws java.rmi.RemoteException {
    String[] r = null;
    try {
      GetServicesHandler handler = new GetServicesHandler(this.getClientHost(),this.GetClientHostName(),securityToken,serviceType);
      r = (String[])handler.Invoke();
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Handle NodePing: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
    }
    return r;
  }

  //** added by Maggie H. 1-22-2004
   /*
    private ClsManager getBizManager() throws Exception
    {
        return new ClsManager("node");
    }
       }
    */
   //** added by Maggie H. 1-28-2004
  /**
   * getClientHost
   * @param 
   * @return String
   */
  private String getClientHost()
  {
    // Get clienthost from the Axis Servlet Request
    org.apache.axis.MessageContext msgContext = org.apache.axis.MessageContext.getCurrentContext();
    HttpServletRequest req = (HttpServletRequest) msgContext.getProperty(HTTPConstants.MC_HTTP_SERVLETREQUEST);
    // WI 23197 
    //String clientHost = req.getRemoteAddr();
    String clientHost = Utility.getIpFromRequest(req);
    //log.debug("[getclienthost]: clienthost = " + clientHost);
    if(clientHost == null) clientHost = "000.000.000.000";
    return clientHost;
  }

  /**
   * GetClientHostName
   * @param 
   * @return String
   */
  private String GetClientHostName () throws Exception
  {
    return InetAddress.getLocalHost().getHostName();
  }
}
