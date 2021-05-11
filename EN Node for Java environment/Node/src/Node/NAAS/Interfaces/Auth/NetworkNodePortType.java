/**
 * NetworkNodePortType.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.NAAS.Interfaces.Auth;

public interface NetworkNodePortType extends java.rmi.Remote {

    // Direct authentication for network users. The security token can then
    // be used to access all participating network nodes. Network node should
    // call the CentralAuth method instead.
	/**
	 * authenticate.
	 * @param userId.
	 * @param credential.
	 * @param authenticationMethod.
	 * @return String
	 */
    public java.lang.String authenticate(java.lang.String userId, Node.NAAS.Types.Auth.PasswordType credential, Node.NAAS.Types.Auth.AuthMethod authenticationMethod) throws java.rmi.RemoteException;

    // Central authentication method, used only by participating newwork
    // nodes. Users should call the Authenticate method directly
	/**
	 * centralAuth.
	 * @param uid.
	 * @param cred.
	 * @param authMethod.
	 * @param clientHost.
	 * @return String
	 */
    public java.lang.String centralAuth(java.lang.String uid, java.lang.String cred, Node.NAAS.Types.Auth.AuthMethod authMethod, java.lang.String clientHost) throws java.rmi.RemoteException;

    // Validate a previously issued authToken.
	/**
	 * centralAuth.
	 * @param authToken.
	 * @param clientHost.
	 * @param resourceURI.
	 * @return String
	 */
    public java.lang.String validate(java.lang.String authToken, java.lang.String clientHost, java.lang.String resourceURI) throws java.rmi.RemoteException;
}
