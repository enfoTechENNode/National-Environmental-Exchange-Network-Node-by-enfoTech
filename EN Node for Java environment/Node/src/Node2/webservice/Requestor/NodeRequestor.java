package Node2.webservice.Requestor;

import java.io.IOException;
import java.math.BigInteger;
import java.net.PasswordAuthentication;
import java.net.URL;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.List;

import javax.xml.namespace.QName;

import net.exchangenetwork.www.schema.node._2.Authenticate;
import net.exchangenetwork.www.schema.node._2.AuthenticateResponse;
import net.exchangenetwork.www.schema.node._2.Download;
import net.exchangenetwork.www.schema.node._2.DownloadResponse;
import net.exchangenetwork.www.schema.node._2.EncodingType;
import net.exchangenetwork.www.schema.node._2.Execute;
import net.exchangenetwork.www.schema.node._2.ExecuteResponse;
import net.exchangenetwork.www.schema.node._2.GenericXmlType;
import net.exchangenetwork.www.schema.node._2.GetServices;
import net.exchangenetwork.www.schema.node._2.GetServicesResponse;
import net.exchangenetwork.www.schema.node._2.GetStatus;
import net.exchangenetwork.www.schema.node._2.GetStatusResponse;
import net.exchangenetwork.www.schema.node._2.NodeDocumentType;
import net.exchangenetwork.www.schema.node._2.NodePing;
import net.exchangenetwork.www.schema.node._2.NodePingResponse;
import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.NotificationTypeCode;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;
import net.exchangenetwork.www.schema.node._2.Notify;
import net.exchangenetwork.www.schema.node._2.NotifyResponse;
import net.exchangenetwork.www.schema.node._2.ParameterType;
import net.exchangenetwork.www.schema.node._2.Query;
import net.exchangenetwork.www.schema.node._2.QueryResponse;
import net.exchangenetwork.www.schema.node._2.ResultSetType;
import net.exchangenetwork.www.schema.node._2.Solicit;
import net.exchangenetwork.www.schema.node._2.SolicitResponse;
import net.exchangenetwork.www.schema.node._2.StatusResponseType;
import net.exchangenetwork.www.schema.node._2.Submit;
import net.exchangenetwork.www.schema.node._2.SubmitResponse;
import net.exchangenetwork.www.wsdl.node._2.NetworkNode2Stub;
import net.exchangenetwork.www.wsdl.node._2.NodeFaultMessage;

import org.apache.axis2.client.Options;
import org.apache.axis2.context.MessageContextConstants;
import org.apache.axis2.databinding.types.NCName;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.axis2.transport.http.HttpTransportProperties;
import org.apache.axis2.transport.http.HttpTransportProperties.Authenticator;
import org.apache.commons.httpclient.HttpClient;
import org.apache.commons.httpclient.HttpConstants;
import org.apache.commons.httpclient.HttpState;
import org.apache.commons.httpclient.UsernamePasswordCredentials;
import org.apache.commons.httpclient.auth.AuthPolicy;
import org.apache.commons.httpclient.auth.AuthScope;
import org.apache.commons.httpclient.methods.GetMethod;
import org.apache.commons.httpclient.protocol.DefaultProtocolSocketFactory;
import org.apache.commons.httpclient.protocol.Protocol;
import org.apache.commons.httpclient.protocol.ProtocolSocketFactory;
import org.apache.commons.httpclient.protocol.ReflectionSocketFactory;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.AppUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;
import Node2.webservice.Document.NodeDocumentContentConverter;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import com.enfotech.basecomponent.utility.security.WebProxy;

/**
 * <p>This object is used to call Node Web Services on a Node. Each object
 * represents a connection to one Node.
 * 
 * @author enfoTech
 * @version 2.0
 * </p>
 */
public class NodeRequestor {

	public URL nodeUrl = null;
	public NetworkNode2Stub stub = null;
	/**
	 * NodeRequestor constructor.
	 * <p>
	 * No attempt to set the proxy server is made when using this constructor.
	 * Any proxy settings must be set up by calling the setProxy() method.
	 * </p>
	 * 
	 * @param nodeUrl
	 *            the url of the Node being requested.
	 */
	public NodeRequestor(URL url) {
		this.nodeUrl = url;
		try {			
			this.stub = new NetworkNode2Stub(this.nodeUrl.toString());
		} catch (Exception ex) {
			// ignore exception
		}
	}

	/**
	 * NodeRequestor constructor.
	 * <p>
	 * This constructor attempts to set the proxy settings from the Node
	 * configuration files (if one is needed).
	 * </p>
	 * 
	 * @param nodeUrl
	 *            the url of the Node being requested.
	 * @param loggerName
	 *            (Required) Name of the log4j Logger Name (in case Logging
	 *            should take place). See Node.Phrase for details.
	 * @throws Exception 
	 */
	public NodeRequestor(URL url, String loggerName) throws Exception {
		this.nodeUrl = url;
		String sHost=null;
		try {
			this.stub = new NetworkNode2Stub(this.nodeUrl.toString());
			//in order to make sure that we use HTTP 1.0/1.1 to transfer mtom
			String httpVer = "1.0";
			httpVer = (String) JNDIAccess.GetJNDIValue(Phrase.httpVersion, false);
			Options options = this.stub._getServiceClient().getOptions();
			options.setProperty(org.apache.axis2.transport.http.HTTPConstants.CHUNKED, org.apache.axis2.Constants.VALUE_FALSE);				
			options.setProperty(org.apache.axis2.Constants.Configuration.ENABLE_MTOM, org.apache.axis2.Constants.VALUE_TRUE);
			if(httpVer.equalsIgnoreCase("1.0")){
				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.HTTP_PROTOCOL_VERSION,HTTPConstants.HEADER_PROTOCOL_10);
			}else if(httpVer.equalsIgnoreCase("1.1")){
				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.HTTP_PROTOCOL_VERSION,HTTPConstants.HEADER_PROTOCOL_11);
			}
			if (this.nodeUrl != null && this.nodeUrl.getHost() != null
				&& !this.nodeUrl.getHost().equalsIgnoreCase("localhost")) {
				ISystemConfiguration config = DBManager.GetSystemConfiguration(loggerName);
				// get url
				if ((this.nodeUrl + "").equalsIgnoreCase("null")
					|| (this.nodeUrl + "").equalsIgnoreCase(""))
					throw new Exception("Endpoint is not defined. Please check system.config file.");
				// get proxy setting
				if (config.GetProxyStatus()) {
					sHost = config.GetProxyHost();
					String sPort = config.GetProxyPort();
					String sUser = config.GetProxyUID();
					String sPassword = config.GetProxyPWD();
					// must remove Java Net proxy setting first to avoid Axis1 bug
					this.removeJavaNetProxy();
					this.setAxis2Proxy(sHost, sPort, sUser, sPassword);
					//this.restoreJavaNetProxy(sHost, sPort, sUser, sPassword);
				}
			}else this.removeJavaNetProxy();
		} catch (NodeFaultMessage e) {
			throw new RemoteException(e.getFaultMessage().getErrorCode().getValue());
		} catch (Exception ex) {
			throw ex;
		}

	}

	/**
	 * Get the Node URL used in constructing this object.
	 * 
	 * @return url Node URL
	 */
	public URL getUrl() {
		return this.nodeUrl;
	}

	/**
	 * Set the URL of the Node being requested.
	 * 
	 * @param url
	 *            Node URL
	 */
	public void setUrl(URL url) {
		this.nodeUrl = url;
	}

	/**
	 * Set the proxy server settings
	 * 
	 * @param proxyAddress
	 *            proxy server address
	 * @param proxyPort
	 *            proxy server port number
	 * @param proxyUser
	 *            proxy server user name
	 * @param proxyPassword
	 *            proxy server password
	 */
	public void setAxis2Proxy(String proxyAddress, String proxyPort,
			String proxyUser, String proxyPassword) throws Exception {
		String httpVer = "1.0";
        try {
			// enable proxy authenticate
			Options options = this.stub._getServiceClient().getOptions();
			HttpTransportProperties.ProxyProperties proxyProperties = new HttpTransportProperties.ProxyProperties();
			proxyProperties.setProxyName(proxyAddress);
			if(proxyPort!=null && !proxyPort.equals("")){
				proxyProperties.setProxyPort(Integer.parseInt(proxyPort));				
			}
			proxyProperties.setUserName(proxyUser);
			proxyProperties.setPassWord(proxyPassword);
			//proxyProperties.setDomain("Enfotech");
			options.setProperty(org.apache.axis2.transport.http.HTTPConstants.PROXY,proxyProperties);
			//in order to make sure that we use HTTP 1.0 to transfer mtom
//			httpVer = (String) JNDIAccess.GetJNDIValue(Phrase.httpVersion, false);
//			if(httpVer.equalsIgnoreCase("1.0")){
//				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.HTTP_PROTOCOL_VERSION,HTTPConstants.HEADER_PROTOCOL_10);
//				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.CHUNKED, org.apache.axis2.Constants.VALUE_FALSE);				
//			}else if(httpVer.equalsIgnoreCase("1.1")){
//				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.HTTP_PROTOCOL_VERSION,HTTPConstants.HEADER_PROTOCOL_11);
//				options.setProperty(org.apache.axis2.transport.http.HTTPConstants.CHUNKED, org.apache.axis2.Constants.VALUE_FALSE);				
//				options.setProperty(org.apache.axis2.Constants.Configuration.ENABLE_MTOM, org.apache.axis2.Constants.VALUE_TRUE);
//			}

			// enable basic authenticate
//			Authenticator authenticator = new Authenticator();
//			List auth = new ArrayList();
//			auth.add(AuthPolicy.BASIC);
//			authenticator.setAuthSchemes(auth);
//			authenticator.setHost(proxyAddress);
//			authenticator.setPort(Integer.parseInt(proxyPort));
//			authenticator.setUsername(proxyUser);
//			authenticator.setPassword(proxyPassword);
////			String s = ((Authenticator)options.getProperty(org.apache.axis2.transport.http.HTTPConstants.AUTHENTICATE)).getDomain();
//			authenticator.setRealm("Enfotech");
//			authenticator.setDomain("Enfotech");
//			authenticator.setPreemptiveAuthentication(true);
//			options.setProperty(org.apache.axis2.transport.http.HTTPConstants.AUTHENTICATE,authenticator);
			
		} catch (Exception ex) {
			throw ex;
		}
	}

	  /**
	   * removeJavaNetProxy
	   * @param 
	   * @throws IOException
	   * @return 
	   */
	public  void removeJavaNetProxy() throws IOException {
		if(System.getProperty("http.proxySet")!=null){
			System.getProperties().remove("http.proxySet");
		}
		if(System.getProperty("https.proxySet")!=null){
			System.getProperties().remove("https.proxySet");
		}
		if(System.getProperty("http.proxyHost")!=null){
			System.getProperties().remove("http.proxyHost");
		}
		if(System.getProperty("https.proxyHost")!=null){
			System.getProperties().remove("https.proxyHost");
		}
		if(System.getProperty("http.proxyPort")!=null){
			System.getProperties().remove("http.proxyPort");
		}
		if(System.getProperty("https.proxyPort")!=null){
			System.getProperties().remove("https.proxyPort");
		}
		if(System.getProperty("http.proxyUser")!=null){
			System.getProperties().remove("http.proxyUser");
		}
		if(System.getProperty("https.proxyUser")!=null){
			System.getProperties().remove("https.proxyUser");
		}
		if(System.getProperty("http.proxyPassword")!=null){
			System.getProperties().remove("http.proxyPassword");
		}
		if(System.getProperty("https.proxyPassword")!=null){
			System.getProperties().remove("https.proxyPassword");
		}
		if(System.getProperty("socksProxyHost")!=null){
			System.getProperties().remove("http.socksProxyHost");
		}
	}
	
	  /**
	   * restoreJavaNetProxy
	   * @param proxyAddress
	   * @param proxyPort
	   * @param proxyUser
	   * @param proxyPassword
	   * @return 
	   */
	public  void restoreJavaNetProxy(String proxyAddress, String proxyPort, String proxyUser,
            String proxyPassword) throws Exception {
	    boolean isSSL = false;
	    try {
	      if (this.nodeUrl != null && this.nodeUrl.getProtocol().equalsIgnoreCase("https")) {
	        isSSL = true;
	      }
	      WebProxy.SetProxy(proxyAddress, proxyPort, proxyUser, proxyPassword,
	                        isSSL);
	    }
	    catch (Exception e) {
	      throw e;
	    }
	}
	
	/**
	 * get networkNodeBindingStub
	 * 
	 * @return A networkNodeBindingStub
	 * @throws RemoteException
	 */
	private NetworkNode2Stub getStub() throws RemoteException {
		//NetworkNode2Stub stub = new NetworkNode2Stub(this.nodeUrl.toString());

		return this.stub;
	}

	/**
	 * Node Ping
	 * <p>
	 * Ping a Node to make sure that the Node represented by this object exists
	 * and is reeiving requests.
	 * </p>
	 * 
	 * @param hello
	 *            Input String.
	 * @return Status String.
	 * @throws Exception 
	 */
	public String nodePing(String hello) throws Exception {
		String response = null;
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		NodePing input = new NodePing();
		input.setHello(hello);
		NodePingResponse ret = new NodePingResponse();
		try {
			ret = port.NodePing(input);
			response = ret.getStatusDetail();
		} catch (NodeFaultMessage e) {
						return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		return response;
	}

	/**
	 * Authenicate
	 * <p>
	 * Authenticate a user and supply and security token uniquely identifying
	 * that user.
	 * </p>
	 * 
	 * @param userId
	 *            user name
	 * @param credential
	 *            credential
	 * @param authMethod
	 *            method of authentication
	 * @param domainName
	 * @return security token
	 * @throws Exception
	 */
	public String authenticate(String userId, String credential,
			String authMethod, String domainName) throws Exception {
		String response = null;
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		Authenticate input = new Authenticate();
		input.setUserId(userId);
		input.setCredential(credential);
		input.setAuthenticationMethod(authMethod);
		input.setDomain(domainName);
		AuthenticateResponse ret = new AuthenticateResponse();
		try {
			ret = port.Authenticate(input);
			response = ret.getSecurityToken();
		} catch (NodeFaultMessage e) {
			//			return e.getFaultMessage().getErrorCode().getValue();
			return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		return response;
	}

	/**
	 * Submit
	 * <p>
	 * Submit Documents to a Node. A document is wrapped up in ClsNodeDocument
	 * object.
	 * </p>
	 * 
	 * @param securityToken token to identify user.
	 * @param transactionId transaction id of document to download.
	 * @param dataflowType dataflow of document to download.
	 * @param flowOperation flow Operation type.
	 * @param recipient recipient.
	 * @param notificationURI notification URI.
	 * @param notificationType notification Type.
	 * @param localDocuments documents specifying which document(s) to submit.
	 * @return transactionId. returned transaction id.
	 * @throws Exception
	 */
	public String submit(String securityToken, String transactionId,
			String dataflowType, String flowOperation, String[] recipient,
			String[] notificationURI, String notificationType,
			ClsNodeDocument[] localDocuments) throws Exception {
		String[] response = new String[2];
		NodeDocumentType[] documents = null;

		try {
			// convert LocalDocuments to NodeDocuments
			documents = NodeDocumentContentConverter.convertToNodeDocument(localDocuments);
			StatusResponseType r = new StatusResponseType();
			// calling private method to get the port
			NetworkNode2Stub port = this.getStub();
			Submit input = new Submit();
			input.setSecurityToken(securityToken);
			input.setTransactionId(transactionId);
			NCName ncName = new NCName();
			ncName.setValue(dataflowType);
			input.setDataflow(ncName);
			input.setFlowOperation(flowOperation);
			input.setRecipient(recipient);
			NotificationURIType[] notificationURIType = null;
			NotificationTypeCode notificationTypeCode = NotificationTypeCode.Factory.fromValue(notificationType);
			if(notificationURI!=null){
				notificationURIType = new NotificationURIType[notificationURI.length];
				for(int i=0;i<notificationURI.length;i++){
					notificationURIType[i] = new NotificationURIType();
					notificationURIType[i].setString(notificationURI[i]);
					notificationURIType[i].setNotificationType(notificationTypeCode);
				}				
			}
			input.setNotificationURI(notificationURIType);
			input.setDocuments(documents);
			
			SubmitResponse ret = new SubmitResponse();

			ret = port.Submit(input);
			r = ret.getSubmitResponse();
			for (int i = 0; i < 2; i++) {
				if (i == 0)
					response[0] = "Status is: "+r.getStatus().toString();
				else
					response[1] = r.getTransactionId();
			}
		} catch (NodeFaultMessage e) {
						return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		return response[1];
	}

	/**
	 * Download
	 * <p>
	 * Download Documents to a Node.
	 * </p>
	 * 
	 * @param securityToken
	 *            token to identify user.
	 * @param transactionId
	 *            transaction id of document to download.
	 * @param dataflowType
	 *            dataflow of document to download.
	 * @param localDocuments
	 *            documents specifying which document(s) to download.
	 * @throws RemoteException
	 *             if soap exception is thrown.
	 * @return downloaded documents.
	 */
	public ClsNodeDocument[] download(String securityToken, String dataflowType,
			String transactionId, ClsNodeDocument[] localDocuments) throws Exception {
		String[] response = null;
		NodeDocumentType[] documents = null;
		NodeDocumentType[] retNodeDocs = null;
		ClsNodeDocument[] retLocalDocs = null;

		try {
			// convert LocalDocuments to NodeDocuments
			documents = NodeDocumentContentConverter.convertToNodeDocument(localDocuments);
			DownloadResponse ret = new DownloadResponse();
			// calling private method to get the port
			NetworkNode2Stub port = this.getStub();
			Download input = new Download();
			input.setSecurityToken(securityToken);
			input.setTransactionId(transactionId);
			NCName ncName = new NCName();
			if(dataflowType!=null && !dataflowType.equalsIgnoreCase("")){
				ncName.setValue(dataflowType);
				input.setDataflow(ncName);
				input.setDocuments(documents);

				ret = port.Download(input);
				retNodeDocs = ret.getDocuments();
				if(retNodeDocs!=null && retNodeDocs.length>0){
					retLocalDocs = new ClsNodeDocument[retNodeDocs.length];
					retLocalDocs = NodeDocumentContentConverter.convertToLocalNodeDocument(retNodeDocs);				
				}				
			}else throw new RemoteException(Phrase.InvalidDataFlow); 
		} catch (NodeFaultMessage e) {
			String error = e.getFaultMessage().getErrorCode().getValue();
			if(error.equalsIgnoreCase("E_FileNotFound")){
				return retLocalDocs;
			}else{
				throw new RemoteException(e.getFaultMessage().getErrorCode().getValue());				
			}
		} catch (Exception ex) {
			throw ex;
		}
		return retLocalDocs;
	}

	/**
	 * Query
	 * <p>
	 * Query a Node for Data.
	 * </p>
	 * 
	 * @param securityToken
	 *            token to identify user.
	 * @param dataFlow
	 *            Data flow.
	 * @param request
	 *            request to execute.
	 * @param rowId
	 *            starting row of return data.
	 * @param maxRows
	 *            max rows to return.
	 * @param paramNames  parameter names to query data by.
	 * @param parameters
	 *            parameters to query data by.
	 * @param paramTypes parameter types to query data by.
	 * @param paramEncodes parameter encodes to query data by.
	 * @throws Exception
	 *             if soap exception is thrown.
	 * @return query result.
	 */
	public String query(String securityToken, String dataFlow,String request, int rowId,
			int maxRows, String[] paramNames,String[] parameters,String[] paramTypes,String[] paramEncodes) throws Exception {
		GenericXmlType response = null;
		String content = null;
		ResultSetType resultSetType = new ResultSetType();
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		Query input = new Query();
		// Set time out 5 minutes
		// WI 22711
		port._getServiceClient().getOptions().setTimeOutInMilliSeconds(300000);
		input.setSecurityToken(securityToken);
		NCName ncName = new NCName();
		ncName.setValue(dataFlow);
		input.setDataflow(ncName);
		input.setRequest(request);
		input.setRowId(BigInteger.valueOf(rowId));
		input.setMaxRows(BigInteger.valueOf(maxRows));
		ParameterType[] parameterType = null;
		if(parameters!=null && parameters.length > 0){
			parameterType = new ParameterType[parameters.length];
			for(int i=0;i<parameterType.length;i++){
				parameterType[i] = ParameterType.Factory.fromString(parameters[i], "");
				parameterType[i].setParameterName(paramNames[i]);
				QName type = new QName(paramTypes[i]);
				parameterType[i].setParameterType(type);
				EncodingType encodeType = EncodingType.Factory.fromValue(paramEncodes[i]);
				parameterType[i].setParameterEncoding(encodeType);
			}
		}
		input.setParameters(parameterType);		
		QueryResponse ret = new QueryResponse();
		try {
			ret = port.Query(input);
			resultSetType = ret.getQueryResponse();
			response = resultSetType.getResults();
			
		} catch (NodeFaultMessage e) {
						return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		// WI 23720
		if(response != null && response.getFormat() != null && response.getFormat().getValue() != null && response.getFormat().getValue().equalsIgnoreCase(Phrase.ZIP_TYPE)){
			content = response.getExtraElement().getText();
    	    String path = Utility.GetTempFilePath() + "/temp/";
    		//String path = AppUtils.ClientRoot+"temp/";
    		String timeId = Utility.GetSysTimeString();
    		String zipFile = "QueryResultTmp"+ timeId +".zip";
			Utility.decode(content,path+zipFile);
			// WI 21088
			ArrayList fileList = Utility.unZipFile(zipFile, path);
			if(fileList != null && fileList.size() > 0){
				for(int i=0;i<fileList.size();i++){
					content = new String(Utility.readFile(path + (String)fileList.get(i)));				
		      		Utility.delFile(path+(String)fileList.get(i));					
				}
			}
      		Utility.delFile(path+zipFile);
		}else{
			content = response.getExtraElement().toString();
			
		}
  		return content;
	}

	/**
	 * GetServices
	 * <p>
	 * Get List of Services that a Node provides.
	 * </p>
	 * 
	 * @param securityToken
	 *            token to identify user.
	 * @param serviceCategory
	 *            type of service to return.
	 * @throws RemoteException
	 *             if soap exception is thrown.
	 * @return services of type serviceType offered.
	 * @throws Exception 
	 */
	public String getServices(String securityToken, String serviceCategory)
			throws Exception {
		GenericXmlType response = null;
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		GetServices input = new GetServices();
		input.setSecurityToken(securityToken);
		input.setServiceCategory(serviceCategory);
		GetServicesResponse ret = new GetServicesResponse();
		try {
			ret = port.GetServices(input);
			response = ret.getGetServicesResponse();
		} catch (NodeFaultMessage e) {
			return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		return response.getExtraElement().toString();
	}

	/**
	 * GetStatus
	 * <p>
	 * Get the Status of a previous transaction.
	 * </p>
	 * 
	 * @param securityToken
	 *            token to identify user.
	 * @param transactionId
	 *            transaction id of the transaction to query.
	 * @return status
	 * @throws Exception 
	 */
	public String[] getStatus(String securityToken, String transactionId)
			throws Exception {
		String[] response = new String[2];
		StatusResponseType r = new StatusResponseType();
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		GetStatus input = new GetStatus();
		input.setSecurityToken(securityToken);
		input.setTransactionId(transactionId);
		GetStatusResponse ret = new GetStatusResponse();
		try {
			ret = port.GetStatus(input);
			r = ret.getGetStatusResponse();
			for (int i = 0; i < 2; i++) {
				if (i == 0)
					response[0] = r.getStatus().getValue();
				else
					response[1] = r.getStatusDetail();
			}
		} catch (NodeFaultMessage e) {
			String [] retList = new String[1]; 
			retList[0] = e.getFaultMessage().getErrorCode().getValue();
			return retList;
		} catch (Exception ex) {
			throw ex;
		}
		return response;
	}

	/**
	 * Notify
	 * <p>
	 * Notify a Node of an event, status, or documents made available.
	 * </p>
	 * 
	 * @param securityToken
	 *            token to identify user.
	 * @param nodeAddress
	 *            node address this notify refers to.
	 * @param dataflowType
	 * @param messageType
	 * @throws Exception
	 *             if soap exception is thrown.
	 * @return transaction id or some other status string.
	 */
	public String[] notify(String securityToken, String nodeAddress,
			String dataflowType, NotificationMessageType[] messageType)
			throws Exception {
		String[] response = new String[3];
		StatusResponseType r = new StatusResponseType();
		NetworkNode2Stub port = this.getStub();
		Notify input = new Notify();
		input.setSecurityToken(securityToken);
		input.setNodeAddress(nodeAddress);
		NCName ncName = new NCName();
		ncName.setValue(dataflowType);
		input.setDataflow(ncName);
		input.setMessages(messageType);		
		NotifyResponse ret = new NotifyResponse();
		try {
			ret = port.Notify(input);
			r = ret.getNotifyResponse();
			for (int i = 0; i < 3; i++) {
				if (i == 0)
					response[0] = r.getStatus().toString();
				else if(i == 1)
					response[1] = r.getStatusDetail();
				else response[2] = r.getTransactionId();
			}			
		} catch (NodeFaultMessage e) {
			String [] retList = new String[2]; 
			retList[0] = e.getFaultMessage().getErrorCode().getValue();
			retList[1] = e.getFaultMessage().getDescription();
			return retList;
		} catch (Exception ex) {
			throw ex;
		}
		return response;
	}

	/**
	 * Solicit
	 * <p>
	 * Asynchronously query a Node for Data. The data can be retrieved later
	 * after processing is done, or can be done by having the processing party
	 * submit the data upon completion of processing.
	 * </p>
	 * 
	 * @param securityToken token to identify user.
	 * @param dataflowType dataflow of document to download.
	 * @param request
	 *            request to execute.
	 * @param recipients recipient List
	 * @param notificationURIs notification URI
	 * @param notificationType notification Type
	 * @param paramNames  parameter names to query data by.
	 * @param parameters parameters to query data by.
	 * @param paramTypes parameter types to query data by.
	 * @param paramEncodes parameter encodes to query data by.
	 * @throws RemoteException if soap exception is thrown.
	 * @return transaction id of solicit request.
	 */
	public String solicit(String securityToken, String dataflowType, String request, String[] recipients,
			String[] notificationURIs,  String notificationType, String[] paramNames,String[] parameters,String[] paramTypes,String[] paramEncodes) throws Exception {
		String[] response = new String[2];
		StatusResponseType r = new StatusResponseType();
		NetworkNode2Stub port = this.getStub();
		Solicit input = new Solicit();
		input.setSecurityToken(securityToken);
		input.setRequest(request);
		NCName ncName = new NCName();
		ncName.setValue(dataflowType);
		input.setDataflow(ncName);
		ParameterType[] parameterType = null;
		if(parameters!=null && parameters.length > 0){
			parameterType = new ParameterType[parameters.length];
			for(int i=0;i<parameterType.length;i++){
				parameterType[i] = ParameterType.Factory.fromString(parameters[i], "");
				parameterType[i].setParameterName(paramNames[i]);
				QName type = new QName(paramTypes[i]);
				parameterType[i].setParameterType(type);
				EncodingType encodeType = EncodingType.Factory.fromValue(paramEncodes[i]);
				parameterType[i].setParameterEncoding(encodeType);
			}
		}
		input.setParameters(parameterType);
		input.setRecipient(recipients);
		NotificationURIType[] notificationURITypeList = null;
		if(notificationURIs!=null){
			notificationURITypeList = new NotificationURIType[notificationURIs.length];		
			for(int i=0;i<notificationURIs.length;i++){
				notificationURITypeList[i] = new NotificationURIType();
				NotificationTypeCode notificationTypeCode = NotificationTypeCode.Factory.fromValue(notificationType);
				notificationURITypeList[i].setNotificationType(notificationTypeCode);
				notificationURITypeList[i].setString(notificationURIs[i]);
			}			
		}
		input.setNotificationURI(notificationURITypeList);
		SolicitResponse ret = new SolicitResponse();
		try {
			ret = port.Solicit(input);
			r = ret.getSolicitResponse();
			for (int i = 0; i < 2; i++) {
				if (i == 0)
					response[0] = r.getStatus().toString();
				else
					response[1] = r.getTransactionId();
			}			
		} catch (NodeFaultMessage e) {
			return e.getFaultMessage().getErrorCode().getValue();
		} catch (Exception ex) {
			throw ex;
		}
		return response[1];
	}

	  /**
	   * Execute
	   * <p>Execute a webservice.</p>
	   * @param securityToken token to identify user.
	   * @param interfaceName interface to execute.
	   * @param methodName method to execute.
	   * @param parameters parameters.
	   * @throws RemoteException if soap exception is thrown.
	   * @return execute result.
	   */
	public String[] execute(String securityToken, String interfaceName,String methodName, String[] parameters) throws Exception {
		GenericXmlType results = null;
		String transId = null;
		String status = null;
		String[] response = new String[3];
		ExecuteResponse ret = new ExecuteResponse();
		// calling private method to get the port
		NetworkNode2Stub port = this.getStub();
		Execute input = new Execute();
		input.setSecurityToken(securityToken);
		input.setInterfaceName(interfaceName);
		input.setMethodName(methodName);
		ParameterType[] parameterType = null;
		if(parameters!=null && parameters.length > 0){
			parameterType = new ParameterType[parameters.length];
			for(int i=0;i<parameterType.length;i++){
				parameterType[i] = ParameterType.Factory.fromString(parameters[i], "");
			}
		}
		input.setParameters(parameterType);		
		try {
			ret = port.Execute(input);
			transId = ret.getTransactionId();
			status = ret.getStatus().getValue();
			results = ret.getResults();
			response[0] = transId;
			response[1] = status;
			response[2] = results.getExtraElement().toString();
		} catch (NodeFaultMessage e) {
			String [] retList = new String[1]; 
			retList[0] = e.getFaultMessage().getErrorCode().getValue();
			return retList;
		} catch (Exception ex) {
			throw ex;
		}
		return response;
	}


}
