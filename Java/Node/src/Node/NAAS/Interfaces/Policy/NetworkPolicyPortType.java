/**
 * NetworkPolicyPortType.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Interfaces.Policy;

public interface NetworkPolicyPortType extends java.rmi.Remote {

    // Set authorization policy for a subject.Suject, node and method are
    // required parameters.
	/**
	 * setPolicy.
	 * @param userId.
	 * @param credential.
	 * @param subject.
	 * @param method.
	 * @param request.
	 * @param params.
	 * @param decision.
	 * @return String
	 */
    public java.lang.String setPolicy(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params, Node.NAAS.Types.Policy.ActionType decision) throws java.rmi.RemoteException;

    // Remove an authorization policy
	/**
	 * deletePolicy.
	 * @param userId.
	 * @param credential.
	 * @param subject.
	 * @param method.
	 * @param request.
	 * @param params.
	 * @return deletePolicy
	 */
    public java.lang.String deletePolicy(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params) throws java.rmi.RemoteException;

    // Get All Policy associated with a subject
	/**
	 * getPolicyList.
	 * @param userId.
	 * @param credential.
	 * @param subject.
	 * @param rowId.
	 * @param maxRows.
	 * @return PolicyInfo[]
	 */
    public Node.NAAS.Types.Policy.PolicyInfo[] getPolicyList(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException;

    // Get a list of security events
	/**
	 * getAuthEvents.
	 * @param userId.
	 * @param credential.
	 * @param subject.
	 * @param rowId.
	 * @param maxRows.
	 * @return AuthEventType[]
	 */
    public Node.NAAS.Types.Policy.AuthEventType[] getAuthEvents(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException;

    // Verify whether or not a person is authorized to access the specified
    // resource.
	/**
	 * verifyPolicy.
	 * @param subject.
	 * @param node.
	 * @param method.
	 * @param request.
	 * @param params.
	 * @return verifyPolicy
	 */
    public java.lang.String verifyPolicy(java.lang.String subject, Node.NAAS.Types.Policy.NodeId node, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params) throws java.rmi.RemoteException;
}
