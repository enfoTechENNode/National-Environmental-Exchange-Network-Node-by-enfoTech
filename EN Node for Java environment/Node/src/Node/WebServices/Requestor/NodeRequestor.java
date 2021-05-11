package Node.WebServices.Requestor;

import java.net.URL;
import java.rmi.RemoteException;
import java.math.BigInteger;
import org.apache.axis.components.net.SocketFactoryFactory;
import org.apache.axis.AxisProperties;
import org.apache.axis.components.net.SecureSocketFactory;
import com.enfotech.basecomponent.utility.security.WebProxy;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.WebServices.Document.NodeDocument;
import Node.WebServices.Requestor.NetworkNodeLocator;
import Node.WebServices.Requestor.NetworkNodeBindingStub;
import Node.WebServices.Document.ArrayofDocHolder;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Document.NodeDocumentContentConverter;

/**
 * This object is used to call Node Web Services on a Node.  Each object represents a
 * connection to one Node.
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfotech
 * @version 1.8
 */
public class NodeRequestor {
  private URL url;

  /**
   * NodeRequestor constructor.
   * <p>No attempt to set the proxy server is made when using this constructor.
   * Any proxy settings must be set up by calling the setProxy() method.</p>
   * @param nodeUrl the url of the Node being requested.
   */
  public NodeRequestor(URL nodeUrl) {
    this.url = nodeUrl;
    try {
      SocketFactoryFactory.getFactory(null, null);
    }
    catch (Exception ex) {
      //ignore exception
    }
    AxisProperties.setClassDefault(SecureSocketFactory.class,
                                   "org.apache.axis.components.net.SunFakeTrustSocketFactory");
  }
  /**
   * NodeRequestor constructor.
   * <p>This constructor attempts to set the proxy settings from the Node
   * configuration files (if one is needed).</p>
   * @param nodeUrl the url of the Node being requested.
   * @param loggerName (Required) Name of the log4j Logger Name (in case Logging should take place).  See Node.Phrase for details.
   */
  public NodeRequestor(URL nodeUrl, String loggerName) {
    this.url = nodeUrl;
    try {
      SocketFactoryFactory.getFactory(null, null);
    }
    catch (Exception ex) {
      //ignore exception
    }
    AxisProperties.setClassDefault(SecureSocketFactory.class,
                                   "org.apache.axis.components.net.SunFakeTrustSocketFactory");

    if (this.url != null && this.url.getHost() != null && !this.url.getHost().equalsIgnoreCase("localhost")) {
//    AxisProperties.setProperty("axis.http.client.connection.pool.timeout", "1000");
        try
        {
          ISystemConfiguration config = DBManager.GetSystemConfiguration(loggerName);
          //get url
          if((this.url+"").equalsIgnoreCase("null") || (this.url+"").equalsIgnoreCase(""))
            throw new Exception ("Endpoint is not defined. Please check system.config file.");
          //get proxy setting
          if (config.GetProxyStatus()) {
            String sHost = config.GetProxyHost();
            String sPort = config.GetProxyPort();
            String sUser = config.GetProxyUID();
            String sPassword = config.GetProxyPWD();
            this.setProxy(sHost, sPort, sUser, sPassword);
          }
        }
        catch(Exception e)
        {
        }
    }
  }

  /**
   * Get the Node URL used in constructing this object.
   * @return url Node URL
   */
  public URL getUrl() {
    return url;
  }

  /**
   * Set the URL of the Node being requested.
   * @param url Node URL
   */
  public void setUrl(URL url) {
    this.url = url;
  }

  /**
   * Set the proxy server settings
   * @param proxyAddress proxy server address
   * @param proxyPort proxy server port number
   * @param proxyUser proxy server user name
   * @param proxyPassword proxy server password
   */
  public void setProxy(String proxyAddress, String proxyPort, String proxyUser,
                       String proxyPassword) throws Exception {
    boolean isSSL = false;
    try {
      if (this.url != null && this.url.getProtocol().equalsIgnoreCase("https")) {
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
   * @return A networkNodeBindingStub
   * @throws RemoteException
   */
  private NetworkNodeBindingStub getStub() throws RemoteException {
    NetworkNodeLocator locator = new NetworkNodeLocator();
    NetworkNodeBindingStub stub = new NetworkNodeBindingStub(this.url, locator);

    //this sets the operation id in order to use in any kind of handler for
    //tracking purposes
    /*
             if (operationId!=null)
             {
        stub._setProperty("operationId", operationId);
             }
     */

    return stub;
  }

  /**
   * Node Ping
   * <p>Ping a Node to make sure that the Node represented by this object exists and is reeiving requests.</p>
   * @param hello Input String.
   * @throws RemoteException if soap exception is thrown.
   * @return Status String.
   */
  public String nodePing(String hello) throws RemoteException {
    String response = null;
    //calling private method to get the port
    NetworkNodeBindingStub port = this.getStub();

    response = port.nodePing(hello);
    return response;
  }

  /**
   * Authenicate
   * <p>Authenticate a user and supply and security token uniquely identifying that user.</p>
   * @param user user name
   * @param password credential
   * @param authMethod method of authentication
   * @throws RemoteException if soap exception is thrown.
   * @return security token
   */
  public String authenticate(String user, String password, String authMethod) throws
      RemoteException {
    String token = null;

    NetworkNodeBindingStub stub = this.getStub();
    token = stub.authenticate(user, password, authMethod);

    return token;
  }

  /**
   * Submit
   * <p>Submit Documents to a Node.  A document is wrapped up in ClsNodeDocument object.</p>
   * @param securityToken token to identify user.
   * @param transactionId transaction id for submits following solicit processing.
   * @param dataflowType dataflow parameter determining which dataflow these documents belong to.
   * @param cdxDocuments documents to be submitted.
   * @throws RemoteException if soap exception is thrown.
   * @return transactionId.
   */
  public String submit(String securityToken, String transactionId, String dataflowType, ClsNodeDocument[] cdxDocuments) throws RemoteException {
    String transactionIdToReturn = null;
    NodeDocument[] documents = null;

    try{
      //convert CDXDocuments to NodeDocuments
      documents = this.convertToNodeDocuments(cdxDocuments);

      //calling private method to get the port
      NetworkNodeBindingStub port = this.getStub();
      port.setTimeout(4800000);

      //call submit on the port passing in the same parameters recieved from the user
      // debug
      /*
      System.out.println("*** NodeRequestor: submit method --> check document ***");
      for(int i = 0; i < documents.length; i++){
        System.out.println("doc " + (i+1) + ": " + documents[i].getName() + ", " + documents[i].getType());
        if(documents[i].getContent() == null)
          System.out.println("content of document " + (i+1) + " is null.");
      }
      System.out.println("*** NodeRequestor: end of submit method --> check document ***");
      */
      // end debug
      transactionIdToReturn = port.submit(securityToken, transactionId, dataflowType, documents);
    }catch(Exception ex){
/*      System.out.println("NodeRequestor: Exception occurs when invoking submit method: ");
      System.out.println("token: " + securityToken);
      System.out.println("transactionID: " + transactionId);
      System.out.println("dataFlow: " + dataflowType);
      System.out.println(" ********** begin printStackTrace() **********");
*/      ex.printStackTrace();
//      System.out.println(" ********** end printStackTrace() **********");
      RemoteException rex = new RemoteException();
      rex.setStackTrace(ex.getStackTrace());
      throw rex;
    }
    return transactionIdToReturn;
  }

  /**
   * Download
   * <p>Download Documents to a Node.</p>
   * @param securityToken token to identify user.
   * @param transactionId transaction id of document to download.
   * @param dataflowType dataflow of document to download.
   * @param cdxDocuments documents specifying which document(s) to download.
   * @throws RemoteException if soap exception is thrown.
   * @return downloaded documents.
   */
  public ClsNodeDocument[] download(String securityToken, String transactionId, String dataflowType, ClsNodeDocument[] cdxDocuments) throws RemoteException
  {
    ClsNodeDocument[] returnCdxDocuments = null;

    //calling private method to get the port
    NetworkNodeBindingStub port = this.getStub();

    //convert cdxDocuments to NodeDocuments
    NodeDocument[] documents = this.convertToNodeDocuments(cdxDocuments);
    ArrayofDocHolder array = new ArrayofDocHolder(documents);

    //call download method
    port.download(securityToken, transactionId, dataflowType, array);

    //convert recieved NodeDocuments to CdxDocuments
    returnCdxDocuments = this.convertToCDXDocuments(array.value);

    return returnCdxDocuments;
  }

  /**
   * Query
   * <p>Query a Node for Data.</p>
   * @param securityToken token to identify user.
   * @param request request to execute.
   * @param rowId starting row of return data.
   * @param maxRows max rows to return.
   * @param parameters parameters to query data by.
   * @throws RemoteException if soap exception is thrown.
   * @return query result.
   */
  public String query(String securityToken, String request, int rowId, int maxRows,
                      String[] parameters) throws RemoteException {
    NetworkNodeBindingStub stub = this.getStub();
    return stub.query(securityToken, request, BigInteger.valueOf( (long) rowId),
                      BigInteger.valueOf( (long) maxRows), parameters);
  }

  /**
   * GetServices
   * <p>Get List of Services that a Node provides.</p>
   * @param securityToken token to identify user.
   * @param serviceType type of service to return.
   * @throws RemoteException if soap exception is thrown.
   * @return services of type serviceType offered.
   */
  public String[] getServices(String securityToken, String serviceType) throws RemoteException {
    String[] results = null;

    NetworkNodeBindingStub stub = this.getStub();
    results = stub.getServices(securityToken, serviceType);

    return results;
  }

  /**
   * GetStatus
   * <p>Get the Status of a previous transaction.</p>
   * @param securityToken token to identify user.
   * @param transactionId transaction id of the transaction to query.
   * @throws RemoteException if soap exception is thrown.
   * @return status
   */
  public String getStatus(String securityToken, String transactionId) throws
      RemoteException {
    NetworkNodeBindingStub stub = this.getStub();
    return stub.getStatus(securityToken, transactionId);
  }

  /**
   * Notify
   * <p>Notify a Node of an event, status, or documents made available.</p>
   * @param securityToken token to identify user.
   * @param nodeAddress node address this notify refers to.
   * @param dataFlow data flow this notify refers to.
   * @param cdxDocuments documents containing information regarding this notify.
   * @throws RemoteException if soap exception is thrown.
   * @return transaction id or some other status string.
   */
  public String notify(String securityToken, String nodeAddress, String dataFlow, ClsNodeDocument[] cdxDocuments) throws RemoteException {
    NetworkNodeBindingStub stub = this.getStub();

    NodeDocument[] documents = null;

    //convert CDXDocuments to NodeDocuments
    documents = this.convertToNodeDocuments(cdxDocuments);

    return stub.notify(securityToken, nodeAddress, dataFlow, documents);
  }

  /**
   * Solicit
   * <p>Asynchronously query a Node for Data.  The data can be retrieved later after processing
   * is done, or can be done by having the processing party submit the data upon completion
   * of processing.</p>
   * @param securityToken token to identify user.
   * @param return URL to return data to when finished processing.
   * @param request request to execute.
   * @param parms parameters to query data by.
   * @throws RemoteException if soap exception is thrown.
   * @return transaction id of solicit request.
   */
  public String solicit(String securityToken, String returnURL, String request, String[] parms) throws RemoteException {
    NetworkNodeBindingStub stub = this.getStub();
    return stub.solicit(securityToken, returnURL, request, parms);
  }

  /**
   * Execute
   * <p>Execute a webservice.</p>
   * @param securityToken token to identify user.
   * @param request request to execute.
   * @param parameters parameters.
   * @throws RemoteException if soap exception is thrown.
   * @return execute result.
   */
  public String execute(String securityToken, String request, String[] parameters) throws RemoteException {
    NetworkNodeBindingStub stub = this.getStub();
    return stub.execute(securityToken, request, parameters);
  }

  // added by Maggie H.
  /**
   * Convert Enfotech Node document to NodeDocuments
   * @param cdxDocs
   * @return Node documents
   */
  protected NodeDocument[] convertToNodeDocuments(ClsNodeDocument[] cdxDocs) {
    NodeDocument[] docs = null;
    if (cdxDocs != null) {
      docs = new NodeDocument[cdxDocs.length];
      for (int i = 0; i < docs.length; i++) {
        NodeDocument doc = new NodeDocument();
        // debug
        /*
        System.out.println("***** convertToNodeDocuments() methods in NodeReqestor *****");
        System.out.println("ClsNodeDocument Type: " + cdxDocs[i].getType() + ", content type: Attachment");
        */
        // end debug
        doc.putContent(cdxDocs[i].getContent(), NodeDocumentContentConverter.CONTENT_TYPE_ATTACHMENT);
        doc.setName(cdxDocs[i].getName());
        doc.setType(cdxDocs[i].getType());

        docs[i] = doc;
      }
    }
    return docs;
  }

  // added by Maggie H.
  /**
   * Convert NodeDocuments to EnfoTech Node Document
   * @param nodeDocs
   * @return enfoTech node documents
   */
  protected ClsNodeDocument[] convertToCDXDocuments(NodeDocument[] nodeDocs) {
    ClsNodeDocument[] docs = null;
    if (nodeDocs != null) {
      docs = new ClsNodeDocument[nodeDocs.length];
      for (int i = 0; i < docs.length; i++) {
        ClsNodeDocument doc = new ClsNodeDocument();
        doc.setType(nodeDocs[i].getType());
        doc.setName(nodeDocs[i].getName());
        doc.setContent(nodeDocs[i].obtainContentBytes());
        docs[i] = doc;
      }
    }
    return docs;
  }
}
