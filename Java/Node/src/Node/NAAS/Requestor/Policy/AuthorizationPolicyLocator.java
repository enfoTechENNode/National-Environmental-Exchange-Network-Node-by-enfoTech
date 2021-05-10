/**
 * AuthorizationPolicyLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Requestor.Policy;

public class AuthorizationPolicyLocator extends org.apache.axis.client.Service implements Node.NAAS.Interfaces.Policy.AuthorizationPolicy {

    // A set of services for Network Authorization.

    // Use to get a proxy class for NetworkPolicyPortType
    private java.lang.String NetworkPolicyPortType_address = "https://naas.epacdxnode.net/xml/policy.wsdl";

	  /**
	   * getNetworkPolicyPortTypeAddress
	   * @param 
	   * @return String
	   */
    public java.lang.String getNetworkPolicyPortTypeAddress() {
        return NetworkPolicyPortType_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String NetworkPolicyPortTypeWSDDServiceName = "NetworkPolicyPortType";

	  /**
	   * getNetworkPolicyPortTypeWSDDServiceName
	   * @param 
	   * @return String
	   */
    public java.lang.String getNetworkPolicyPortTypeWSDDServiceName() {
        return NetworkPolicyPortTypeWSDDServiceName;
    }

	  /**
	   * setNetworkPolicyPortTypeWSDDServiceName
	   * @param name
	   * @return 
	   */
    public void setNetworkPolicyPortTypeWSDDServiceName(java.lang.String name) {
        NetworkPolicyPortTypeWSDDServiceName = name;
    }

	  /**
	   * getNetworkPolicyPortType
	   * @param 
	   * @return NetworkPolicyPortType
	   */
    public Node.NAAS.Interfaces.Policy.NetworkPolicyPortType getNetworkPolicyPortType() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(NetworkPolicyPortType_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getNetworkPolicyPortType(endpoint);
    }

	  /**
	   * getNetworkPolicyPortType
	   * @param portAddress
	   * @return NetworkPolicyPortType
	   */
    public Node.NAAS.Interfaces.Policy.NetworkPolicyPortType getNetworkPolicyPortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            Node.NAAS.Requestor.Policy.NetworkPolicyBindingStub _stub = new Node.NAAS.Requestor.Policy.NetworkPolicyBindingStub(portAddress, this);
            _stub.setPortName(getNetworkPolicyPortTypeWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

	  /**
	   * setNetworkPolicyPortTypeEndpointAddress
	   * @param address
	   * @return 
	   */
    public void setNetworkPolicyPortTypeEndpointAddress(java.lang.String address) {
        NetworkPolicyPortType_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
	  /**
	   * getPort
	   * @param serviceEndpointInterface
	   * @return Remote
	   */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (Node.NAAS.Interfaces.Policy.NetworkPolicyPortType.class.isAssignableFrom(serviceEndpointInterface)) {
                Node.NAAS.Requestor.Policy.NetworkPolicyBindingStub _stub = new Node.NAAS.Requestor.Policy.NetworkPolicyBindingStub(new java.net.URL(NetworkPolicyPortType_address), this);
                _stub.setPortName(getNetworkPolicyPortTypeWSDDServiceName());
                return _stub;
            }
        }
        catch (java.lang.Throwable t) {
            throw new javax.xml.rpc.ServiceException(t);
        }
        throw new javax.xml.rpc.ServiceException("There is no stub implementation for the interface:  " + (serviceEndpointInterface == null ? "null" : serviceEndpointInterface.getName()));
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
	  /**
	   * getPort
	   * @param portName
	   * @param serviceEndpointInterface
	   * @return Remote
	   */
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        String inputPortName = portName.getLocalPart();
        if ("NetworkPolicyPortType".equals(inputPortName)) {
            return getNetworkPolicyPortType();
        }
        else  {
            java.rmi.Remote _stub = getPort(serviceEndpointInterface);
            ((org.apache.axis.client.Stub) _stub).setPortName(portName);
            return _stub;
        }
    }

	  /**
	   * getServiceName
	   * @param 
	   * @return QName
	   */
    public javax.xml.namespace.QName getServiceName() {
        return new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.wsdl", "AuthorizationPolicy");
    }

    private java.util.HashSet ports = null;

	  /**
	   * getPorts
	   * @param 
	   * @return Iterator
	   */
    public java.util.Iterator getPorts() {
        if (ports == null) {
            ports = new java.util.HashSet();
            ports.add(new javax.xml.namespace.QName("NetworkPolicyPortType"));
        }
        return ports.iterator();
    }

    /**
    * Set the endpoint address for the specified port name.
    */
	  /**
	   * setEndpointAddress
	   * @param portName
	   * @param address
	   * @return 
	   */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        if ("NetworkPolicyPortType".equals(portName)) {
            setNetworkPolicyPortTypeEndpointAddress(address);
        }
        else { // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * Set the endpoint address for the specified port name.
    */
	  /**
	   * setEndpointAddress
	   * @param portName
	   * @param address
	   * @return 
	   */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
