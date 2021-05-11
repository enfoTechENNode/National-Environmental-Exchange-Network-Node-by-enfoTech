/**
 * AuthorizationPolicy.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Interfaces.Policy;

public interface AuthorizationPolicy extends javax.xml.rpc.Service {

    // A set of services for Network Authorization.
	/**
	 * getNetworkPolicyPortTypeAddress.
	 * @param .
	 * @return String
	 */
    public java.lang.String getNetworkPolicyPortTypeAddress();

	/**
	 * getNetworkPolicyPortType.
	 * @param .
	 * @return NetworkPolicyPortType
	 */
    public Node.NAAS.Interfaces.Policy.NetworkPolicyPortType getNetworkPolicyPortType() throws javax.xml.rpc.ServiceException;

	/**
	 * getNetworkPolicyPortType.
	 * @param portAddress.
	 * @return NetworkPolicyPortType
	 */
    public Node.NAAS.Interfaces.Policy.NetworkPolicyPortType getNetworkPolicyPortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException;
}
