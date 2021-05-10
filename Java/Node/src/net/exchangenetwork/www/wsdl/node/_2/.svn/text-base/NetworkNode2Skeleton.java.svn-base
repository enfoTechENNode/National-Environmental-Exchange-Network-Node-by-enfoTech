/**
 * NetworkNode2Skeleton.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:39 LKT)
 */
package net.exchangenetwork.www.wsdl.node._2;

import java.io.ByteArrayInputStream;
import java.math.BigInteger;
import java.net.InetAddress;
import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLStreamReader;

import org.apache.axiom.om.OMAbstractFactory;
import org.apache.axiom.om.OMContainer;
import org.apache.axiom.om.OMDocument;
import org.apache.axiom.om.OMElement;
import org.apache.axiom.om.OMFactory;
import org.apache.axiom.om.OMNode;
import org.apache.axiom.om.impl.builder.StAXOMBuilder;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.axis2.context.MessageContext;
import org.apache.log4j.Level;
import Node.Phrase;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Document.NodeDocument;
import Node2.webservice.Document.NodeDocumentContentConverter;
import Node2.webservice.Handler.WebMethods.AuthenticateHandler;
import Node2.webservice.Handler.WebMethods.DownloadHandler;
import Node2.webservice.Handler.WebMethods.ExecuteHandler;
import Node2.webservice.Handler.WebMethods.GetServicesHandler;
import Node2.webservice.Handler.WebMethods.GetStatusHandler;
import Node2.webservice.Handler.WebMethods.NodePingHandler;
import Node2.webservice.Handler.WebMethods.NotifyHandler;
import Node2.webservice.Handler.WebMethods.QueryHandler;
import Node2.webservice.Handler.WebMethods.SolicitHandler;
import Node2.webservice.Handler.WebMethods.SubmitHandler;
import net.exchangenetwork.www.schema.node._2.Authenticate;
import net.exchangenetwork.www.schema.node._2.AuthenticateResponse;
import net.exchangenetwork.www.schema.node._2.DocumentFormatType;
import net.exchangenetwork.www.schema.node._2.DownloadResponse;
import net.exchangenetwork.www.schema.node._2.ErrorCodeList;
import net.exchangenetwork.www.schema.node._2.ExecuteResponse;
import net.exchangenetwork.www.schema.node._2.GenericXmlType;
import net.exchangenetwork.www.schema.node._2.GetServicesResponse;
import net.exchangenetwork.www.schema.node._2.GetStatusResponse;
import net.exchangenetwork.www.schema.node._2.NodeDocumentType;
import net.exchangenetwork.www.schema.node._2.NodeFaultDetailType;
import net.exchangenetwork.www.schema.node._2.NodePingResponse;
import net.exchangenetwork.www.schema.node._2.NodeStatusCode;
import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;
import net.exchangenetwork.www.schema.node._2.NotifyResponse;
import net.exchangenetwork.www.schema.node._2.ParameterType;
import net.exchangenetwork.www.schema.node._2.QueryResponse;
import net.exchangenetwork.www.schema.node._2.ResultSetType;
import net.exchangenetwork.www.schema.node._2.SolicitResponse;
import net.exchangenetwork.www.schema.node._2.StatusResponseType;
import net.exchangenetwork.www.schema.node._2.SubmitResponse;
import net.exchangenetwork.www.schema.node._2.TransactionStatusCode;


/**
 * NetworkNode2Skeleton java skeleton for the axisService
 */
public class NetworkNode2Skeleton implements NetworkNode2SkeletonInterface {

	/**
	 * Auto generated method signature Request the node to invoke a specified
	 * web services.
	 * 
	 * @param execute0
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.ExecuteResponse Execute(
			net.exchangenetwork.www.schema.node._2.Execute execute0)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Execute " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String[] r = null;
		String securityToken = execute0.getSecurityToken();
		String interfaceName = execute0.getInterfaceName();
		String methodName = execute0.getMethodName();
		ParameterType[] parameters = execute0.getParameters();
		ExecuteResponse executeResponse = new ExecuteResponse();
		OMFactory fac = OMAbstractFactory.getOMFactory();
		DocumentFormatType documentFormatType = DocumentFormatType.Factory.fromValue(DocumentFormatType.XML.getValue());
		GenericXmlType genericXmlType = new GenericXmlType();
		try {
			ExecuteHandler handler = new ExecuteHandler(this.getClientHost(),this.GetClientHostName(),securityToken,interfaceName,methodName,parameters);
			r = (String[])handler.Invoke();
			if(r!=null && r.length==1){	// handle 1.1
				OMElement value = fac.createOMElement("Result", "","");
				genericXmlType.setExtraElement(value);
				value.setText(r[0]);
				genericXmlType.setFormat(documentFormatType);
				executeResponse.setResults(genericXmlType);
			}else if(r!=null){						//handle 2.0
				executeResponse.setTransactionId(r[0]);
				executeResponse.setStatus(TransactionStatusCode.Factory.fromValue(r[1]));
//				OMElement value = fac.createOMElement("Result", "","");
//				genericXmlType.setExtraElement(value);
//				value.setText(r[2]);
				XMLStreamReader parser = XMLInputFactory.newInstance().createXMLStreamReader(new ByteArrayInputStream(r[2].getBytes()));
				StAXOMBuilder builder = new StAXOMBuilder(parser);
				OMElement value =  builder.getDocumentElement();
				genericXmlType.setExtraElement(value);                
				genericXmlType.setFormat(documentFormatType);
				executeResponse.setResults(genericXmlType);
			}
		} catch (Exception e) {
			LoggingUtils.Log("Could not Handle Execute: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
		}
		return executeResponse;
	}

	/**
	 * Auto generated method signature User authentication method, must be
	 * called initially.
	 * 
	 * @param authenticate2
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.AuthenticateResponse Authenticate(
			net.exchangenetwork.www.schema.node._2.Authenticate authenticate2)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Authenticate " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String userId = authenticate2.getUserId();
		String credential = authenticate2.getCredential();
		String domain = authenticate2.getDomain();
		String authenticationMethod = authenticate2.getAuthenticationMethod();
		String result = null;
		AuthenticateResponse ret = new AuthenticateResponse();
		
	    try {
	      AuthenticateHandler handler = new AuthenticateHandler(this.getClientHost(),this.GetClientHostName(),userId,credential,domain,authenticationMethod);
	      result = (String)handler.Invoke();
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could Not Handle Authenticate " + authenticationMethod + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    ret.setSecurityToken(result);
	    return ret;
	}

	/**
	 * Auto generated method signature Download one or more documents from the
	 * node
	 * 
	 * @param download4
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.DownloadResponse Download(
			net.exchangenetwork.www.schema.node._2.Download download4)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Download " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = download4.getSecurityToken();
		String transactionId = download4.getTransactionId();
		String dataflow = download4.getDataflow()==null?null:download4.getDataflow().toString();
		NodeDocumentType[] documents = download4.getDocuments();
		DownloadResponse ret = new DownloadResponse();
		
	    try {
	        ClsNodeDocument[] receivedDocs = NodeDocumentContentConverter.convertToLocalNodeDocument(documents);
	        DownloadHandler handler = new DownloadHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId.equalsIgnoreCase("")?null:transactionId,dataflow,receivedDocs);
	        ClsNodeDocument[] retDocs = (ClsNodeDocument[])handler.Invoke();
	        documents = NodeDocumentContentConverter.convertToNodeDocument(retDocs);
	        if(documents==null){
	        	throw new RemoteException(Phrase.FileNotFound);
	        }
	        ret.setDocuments(documents);
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could not Handle Download " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return ret;
	}

	/**
	 * Auto generated method signature Check the status of a transaction
	 * 
	 * @param getStatus6
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.GetStatusResponse GetStatus(
			net.exchangenetwork.www.schema.node._2.GetStatus getStatus6)
	throws NodeFaultMessage {
	    LoggingUtils.Log("Go into GetStatus " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = getStatus6.getSecurityToken();
		String transactionId = getStatus6.getTransactionId();
		GetStatusResponse ret = new GetStatusResponse();
		StatusResponseType statusResponseType = null;

		try {
			GetStatusHandler handler = new GetStatusHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId);
			statusResponseType = (StatusResponseType)handler.Invoke();
			ret.setGetStatusResponse(statusResponseType);
		}catch (Exception e) {
			LoggingUtils.Log("Could not Handle GetStatus: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
		}
		return ret;
	}

	/**
	 * Auto generated method signature Check the status of the service
	 * 
	 * @param nodePing8
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.NodePingResponse NodePing(
			net.exchangenetwork.www.schema.node._2.NodePing nodePing8)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into NodePing " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String hello = nodePing8.getHello();
		String result = null;
		NodePingResponse ret = new NodePingResponse();
		try {
			NodePingHandler handler = new NodePingHandler(this.getClientHost(),
					this.GetClientHostName(), hello);
			result = (String) handler.Invoke();
			if (hello == null || (hello.equalsIgnoreCase("enfotech"))) {
				if(result.equalsIgnoreCase(NodeStatusCode._Ready)){
					ret.setNodeStatus(NodeStatusCode.Ready);					
					ret.setStatusDetail(Utility.getVersion());
				}else if(result.equalsIgnoreCase(NodeStatusCode._Offline)){
					ret.setNodeStatus(NodeStatusCode.Offline);									
					ret.setStatusDetail(NodeStatusCode._Offline);
				}else if(result.equalsIgnoreCase(NodeStatusCode._Busy)){
					ret.setNodeStatus(NodeStatusCode.Busy);					
					ret.setStatusDetail(NodeStatusCode._Busy);
				}else if(result.equalsIgnoreCase(NodeStatusCode._Unknown)){
					ret.setNodeStatus(NodeStatusCode.Unknown);										
					ret.setStatusDetail(NodeStatusCode._Unknown);
				}
			}else {
				if(result.equalsIgnoreCase(NodeStatusCode._Ready)){
					ret.setNodeStatus(NodeStatusCode.Ready);					
				    // WI 21131
					ret.setStatusDetail(Utility.getVersion());
					//ret.setStatusDetail(hello);
				}else if(result.equalsIgnoreCase(NodeStatusCode._Offline)){
					ret.setNodeStatus(NodeStatusCode.Offline);									
					ret.setStatusDetail(NodeStatusCode._Offline);
				}else if(result.equalsIgnoreCase(NodeStatusCode._Busy)){
					ret.setNodeStatus(NodeStatusCode.Busy);					
					ret.setStatusDetail(NodeStatusCode._Busy);
				}else if(result.equalsIgnoreCase(NodeStatusCode._Unknown)){
					ret.setNodeStatus(NodeStatusCode.Unknown);										
					ret.setStatusDetail(NodeStatusCode._Unknown);
				}
			}
		}catch (Exception e) {
			LoggingUtils.Log("Could not Handle NodePing: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
			NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
			NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
			String[] ex=e.toString().split(":");
			String errorCode = ex[1].split(";")[0].substring(1);
			ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
			nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
			nodeFaultDetailType.setErrorCode(errorCodeList);
			nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
			throw nodeFaultMessage;
		}
		return ret;
		
/*		String hello = nodePing8.getHello();
		String[] statusList = null;
		NodePingResponse ret = new NodePingResponse();
		NodeStatusCode nodeStatusCode = ret.getNodeStatus();

		if (hello != null) {
			ret.setNodeStatus(nodeStatusCode.Ready);
			ret.setStatusDetail(hello);
		} else {
			ret.setNodeStatus(nodeStatusCode.Ready);
		}
		return ret;
*/		 
	}

	/**
	 * Auto generated method signature Query services offered by the node
	 * 
	 * @param getServices10
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.GetServicesResponse GetServices(
			net.exchangenetwork.www.schema.node._2.GetServices getServices10)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into GetServices " , Level.DEBUG, Phrase.WebServicesLoggerName);
	    String[] r = null;
		String securityToken = getServices10.getSecurityToken();
		String serviceCategory = getServices10.getServiceCategory();
		GetServicesResponse getServicesResponse = new GetServicesResponse();
    	OMFactory fac = OMAbstractFactory.getOMFactory();
        DocumentFormatType documentFormatType = DocumentFormatType.Factory.fromValue(DocumentFormatType.XML.getValue());
        GenericXmlType genericXmlType = new GenericXmlType();
	    try {
	      GetServicesHandler handler = new GetServicesHandler(this.getClientHost(),this.GetClientHostName(),securityToken,serviceCategory);
	      r = (String[])handler.Invoke();
	      LoggingUtils.Log("NetworkNode2SkeletonGetServices>>> Return from GetServicesHandler", Level.DEBUG, Phrase.WebServicesLoggerName);
	      if(r!=null && r.length==1){
	    	  XMLStreamReader parser = XMLInputFactory.newInstance().createXMLStreamReader(new ByteArrayInputStream(r[0].getBytes()));
	    	  StAXOMBuilder builder = new StAXOMBuilder(parser);
	    	  OMElement value =  builder.getDocumentElement();
              genericXmlType.setExtraElement(value);                
              genericXmlType.setFormat(documentFormatType);
              getServicesResponse.setGetServicesResponse(genericXmlType);
/*		        OMElement value = fac.createOMElement("Result", "","");
                genericXmlType.setExtraElement(value);
                value.setText(r[0]);
//                value.addChild(fac.createOMText(value, r[0]));
 */
	      }else{
		        OMElement value = fac.createOMElement("Result", "","");
                genericXmlType.setExtraElement(value);                
                genericXmlType.setFormat(documentFormatType);
                getServicesResponse.setGetServicesResponse(genericXmlType);
	      }
	    } catch (Exception e) {
			LoggingUtils.Log("Could not Handle GetServices: " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return getServicesResponse;
	}

	/**
	 * Auto generated method signature Submit one or more documents to the node.
	 * 
	 * @param submit12
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.SubmitResponse Submit(
			net.exchangenetwork.www.schema.node._2.Submit submit12)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Submit " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = submit12.getSecurityToken();
		String transactionId = submit12.getTransactionId();
		String dataflow = submit12.getDataflow()==null?"":submit12.getDataflow().toString();
		String flowOperation = submit12.getFlowOperation();
		String[] recipients = submit12.getRecipient();
		NotificationURIType[] notificationURIType = submit12.getNotificationURI();
		NodeDocumentType[] documents = submit12.getDocuments();
		SubmitResponse ret = new SubmitResponse();
		StatusResponseType statusResponseType = new StatusResponseType();
		String retStr = null;
		
	    try {
	        ClsNodeDocument[] receivedDocs = NodeDocumentContentConverter.convertToLocalNodeDocument(documents);
	        SubmitHandler handler = new SubmitHandler(this.getClientHost(),this.GetClientHostName(),securityToken,transactionId.equalsIgnoreCase("")?null:transactionId,dataflow,flowOperation,recipients,notificationURIType,receivedDocs);
	        retStr = (String)handler.Invoke();
    		String[] split = retStr.split(";");
    		if (split != null && split.length > 1){	// For Node 2.0
    			statusResponseType.setTransactionId(split[0]);
    			statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue(split[1]));
    			statusResponseType.setStatusDetail(split[2]);
    		}else{	// For Node 1.1
    			statusResponseType.setTransactionId(split[0]);
    			statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue(Phrase.ReceivedStatus));
    			statusResponseType.setStatusDetail(Phrase.ReceivedMessage);
    		}

	        ret.setSubmitResponse(statusResponseType);
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could not Handle Submit " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return ret;

	}

	/**
	 * Auto generated method signature Notify document availability, network
	 * events, submission statuses
	 * 
	 * @param notify14
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.NotifyResponse Notify(
			net.exchangenetwork.www.schema.node._2.Notify notify14)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Notify " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = notify14.getSecurityToken();
		String nodeAddress = notify14.getNodeAddress();
		String dataflow = notify14.getDataflow()==null?null:notify14.getDataflow().toString();
		NotificationMessageType[] notificationMessageType = notify14.getMessages();
		String result = null;
		NotifyResponse ret = new NotifyResponse();
		StatusResponseType statusResponseType = null;
		
	    try {
	        NotifyHandler handler = new NotifyHandler(this.getClientHost(),this.GetClientHostName(),securityToken,nodeAddress,dataflow,notificationMessageType);
	        statusResponseType = (StatusResponseType)handler.Invoke();
	        ret.setNotifyResponse(statusResponseType);
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could not Handle Notify " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return ret;
	}

	/**
	 * Auto generated method signature Solicit a lengthy database operation.
	 * 
	 * @param solicit16
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.SolicitResponse Solicit(
			net.exchangenetwork.www.schema.node._2.Solicit solicit16)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Solicit " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = solicit16.getSecurityToken();
		String dataflow = solicit16.getDataflow()==null?"":solicit16.getDataflow().toString();
		String request = solicit16.getRequest();
		String[] recipients = solicit16.getRecipient();
		NotificationURIType[] notificationURIType = solicit16.getNotificationURI();
		ParameterType[] parameters = solicit16.getParameters();
		SolicitResponse ret = new SolicitResponse();
		
	    try {
	    /*	if (parameters != null && parameters.length > 0) {
	    		String[] paramTypes = new String[parameters.length];
	    		String[] paramEncodes = new String[parameters.length];
	    		for (int i = 0; i < parameters.length; i++) {
	    			paramTypes[i] = ((ParameterType)parameters[i]).getParameterType().toString();
	    			paramEncodes[i] = ((ParameterType)parameters[i]).getParameterEncoding().getValue();
	    			if((paramTypes[i].equalsIgnoreCase(Phrase.XML_TYPE) && !paramEncodes[i].equalsIgnoreCase(Phrase.XML_TYPE))||(!paramTypes[i].equalsIgnoreCase(Phrase.XML_TYPE) && paramEncodes[i].equalsIgnoreCase(Phrase.XML_TYPE))){
	    				throw new RemoteException(Phrase.InvalidParameter + ":  XML type value must be set as XML encode.");
	    			}
	    		}
	    	}	    	*/
	        SolicitHandler handler = new SolicitHandler(this.getClientHost(),this.GetClientHostName(),securityToken,dataflow,recipients,request,notificationURIType,parameters);
	        ret = (SolicitResponse)handler.Invoke();
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could not Handle Solicit " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return ret;
	}

	/**
	 * Auto generated method signature Execute a database query
	 * 
	 * @param query18
	 * @throws NodeFaultMessage :
	 */

	public net.exchangenetwork.www.schema.node._2.QueryResponse Query(
			net.exchangenetwork.www.schema.node._2.Query query18)
			throws NodeFaultMessage {
	    LoggingUtils.Log("Go into Query " , Level.DEBUG, Phrase.WebServicesLoggerName);
		String securityToken = query18.getSecurityToken();
		String dataflow = query18.getDataflow()==null?"":query18.getDataflow().toString();
		String request = query18.getRequest();
		BigInteger rowId = query18.getRowId();
		BigInteger maxRows = query18.getMaxRows();
		ParameterType[] parameters = query18.getParameters();
		String[] paras = null;
		QueryResponse ret = new QueryResponse();
		ResultSetType resultSetType = new ResultSetType();
		
	    try {
	    	/*if (parameters != null && parameters.length > 0) {
	    		String[] paramTypes = new String[parameters.length];
	    		String[] paramEncodes = new String[parameters.length];
	    		for (int i = 0; i < parameters.length; i++) {
	    			paramTypes[i] = ((ParameterType)parameters[i]).getParameterType().toString();
	    			paramEncodes[i] = ((ParameterType)parameters[i]).getParameterEncoding().getValue();
	    			if((paramTypes[i].equalsIgnoreCase(Phrase.XML_TYPE) && !paramEncodes[i].equalsIgnoreCase(Phrase.XML_TYPE))||(!paramTypes[i].equalsIgnoreCase(Phrase.XML_TYPE) && paramEncodes[i].equalsIgnoreCase(Phrase.XML_TYPE))){
	    				throw new RemoteException(Phrase.InvalidParameter + ":  XML type value must be set as XML encode.");
	    			}
	    		}
	    	}	    */	
	        QueryHandler handler = new QueryHandler(this.getClientHost(),this.GetClientHostName(),securityToken,dataflow,request,rowId,maxRows,parameters);
	        resultSetType = (ResultSetType)handler.Invoke();
	        ret.setQueryResponse(resultSetType);
	    }catch (Exception e) {
	    	LoggingUtils.Log("Could not Handle Query " + dataflow + ": " + e.toString(), Level.ERROR, Phrase.WebServicesLoggerName);
	    	NodeFaultMessage nodeFaultMessage = new NodeFaultMessage();
	    	NodeFaultDetailType nodeFaultDetailType = new NodeFaultDetailType();
	    	String[] ex=e.toString().split(":");
	    	String errorCode = ex[1].split(";")[0].substring(1);
	    	ErrorCodeList errorCodeList = ErrorCodeList.Factory.fromValue(errorCode);
	    	nodeFaultDetailType.setDescription(Utility.getPhraseFaultMsgByCode(errorCodeList.getValue()));
	    	nodeFaultDetailType.setErrorCode(errorCodeList);
	    	nodeFaultMessage.setFaultMessage(nodeFaultDetailType);
	    	throw nodeFaultMessage;
	    }
	    return ret;

	}

	private String getClientHost() {
		// Get clienthost from the Axis Servlet Request
		MessageContext msgContext = MessageContext.getCurrentMessageContext();
		HttpServletRequest req = (HttpServletRequest) msgContext
				.getProperty(HTTPConstants.MC_HTTP_SERVLETREQUEST);
		// WI 23197
		//String clientHost = req.getRemoteAddr();
		String clientHost = Utility.getIpFromRequest(req);
		// log.debug("[getclienthost]: clienthost = " + clientHost);
		if (clientHost == null)
			clientHost = "000.000.000.000";
		return clientHost;
	}

	private String GetClientHostName() throws Exception {
		return InetAddress.getLocalHost().getHostName();
	}

}
