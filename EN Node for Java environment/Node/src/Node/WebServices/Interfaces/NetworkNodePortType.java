/**
 * NetworkNodePortType.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.WebServices.Interfaces;

public interface NetworkNodePortType extends java.rmi.Remote {

    // User authentication method, must be called initially
	  /**
	   * authenticate
	   * @param userId
	   * @param credential
	   * @param authenticationMethod
	   * @return String
	   */
    public java.lang.String authenticate(java.lang.String userId, java.lang.String credential, java.lang.String authenticationMethod) throws java.rmi.RemoteException;

    // Submit one or more documents to the node.
	  /**
	   * submit
	   * @param securityToken
	   * @param transactionId
	   * @param dataflow
	   * @param documents
	   * @return String
	   */
    public java.lang.String submit(java.lang.String securityToken, java.lang.String transactionId, java.lang.String dataflow, Node.WebServices.Document.NodeDocument[] documents) throws java.rmi.RemoteException;

    // Check the status of a transaction
	  /**
	   * getStatus
	   * @param securityToken
	   * @param transactionId
	   * @return String
	   */
    public java.lang.String getStatus(java.lang.String securityToken, java.lang.String transactionId) throws java.rmi.RemoteException;

    // Notify document availability, network events, submission statuses
	  /**
	   * notify
	   * @param securityToken
	   * @param nodeAddress
	   * @param dataflow
	   * @param documents
	   * @return String
	   */
    public java.lang.String notify(java.lang.String securityToken, java.lang.String nodeAddress, java.lang.String dataflow, Node.WebServices.Document.NodeDocument[]/*node.axis.v11.holders.ArrayofDocHolder*/ documents) throws java.rmi.RemoteException;

    // Download one or more documents from the node
	  /**
	   * download
	   * @param securityToken
	   * @param transactionId
	   * @param dataflow
	   * @param documents
	   * @return 
	   */
    public void download(java.lang.String securityToken, java.lang.String transactionId, java.lang.String dataflow, Node.WebServices.Document.ArrayofDocHolder documents) throws java.rmi.RemoteException;

    // Execute an SQL query
	  /**
	   * query
	   * @param securityToken
	   * @param request
	   * @param rowId
	   * @param maxRows
	   * @param parameters
	   * @return String
	   */
    public java.lang.String query(java.lang.String securityToken, java.lang.String request, java.math.BigInteger rowId, java.math.BigInteger maxRows, java.lang.String[] parameters) throws java.rmi.RemoteException;

    // Solicit an SQL query
	  /**
	   * solicit
	   * @param securityToken
	   * @param returnURL
	   * @param request
	   * @param parameters
	   * @return String
	   */
    public java.lang.String solicit(java.lang.String securityToken, java.lang.String returnURL, java.lang.String request, java.lang.String[] parameters) throws java.rmi.RemoteException;

    // Execute an SQL statement (DML)
	  /**
	   * execute
	   * @param securityToken
	   * @param request
	   * @param parameters
	   * @return String
	   */
    public java.lang.String execute(java.lang.String securityToken, java.lang.String request, java.lang.String[] parameters) throws java.rmi.RemoteException;

    // Check the status of the service
	  /**
	   * nodePing
	   * @param hello
	   * @return String
	   */
    public java.lang.String nodePing(java.lang.String hello) throws java.rmi.RemoteException;

    // Query services offered by the node
	  /**
	   * getServices
	   * @param securityToken
	   * @param serviceType
	   * @return String[]
	   */
    public java.lang.String[] getServices(java.lang.String securityToken, java.lang.String serviceType) throws java.rmi.RemoteException;
}
