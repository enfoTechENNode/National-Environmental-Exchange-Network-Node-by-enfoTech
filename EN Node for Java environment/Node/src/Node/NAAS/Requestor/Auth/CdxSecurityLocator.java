/**
 * CdxSecurityLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.NAAS.Requestor.Auth;

public class CdxSecurityLocator extends org.apache.axis.client.Service implements Node.NAAS.Interfaces.Auth.CdxSecurity {

    // A set of security services for Network nodes.

    // Use to get a proxy class for NetworkSecurityPortType
    private final java.lang.String NetworkSecurityPortType_address = "https://naas.epacdxnode.net/xml/Auth.wsdl";

	  /**
	   * getNetworkSecurityPortTypeAddress
	   * @param 
	   * @return String
	   */
    public java.lang.String getNetworkSecurityPortTypeAddress() {
        return NetworkSecurityPortType_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String NetworkSecurityPortTypeWSDDServiceName = "NetworkSecurityPortType";

	  /**
	   * getNetworkSecurityPortTypeWSDDServiceName
	   * @param 
	   * @return String
	   */
    public java.lang.String getNetworkSecurityPortTypeWSDDServiceName() {
        return NetworkSecurityPortTypeWSDDServiceName;
    }

	  /**
	   * setNetworkSecurityPortTypeWSDDServiceName
	   * @param name
	   * @return 
	   */
    public void setNetworkSecurityPortTypeWSDDServiceName(java.lang.String name) {
        NetworkSecurityPortTypeWSDDServiceName = name;
    }

	  /**
	   * getNetworkSecurityPortType
	   * @param 
	   * @return NetworkNodePortType
	   */
    public Node.NAAS.Interfaces.Auth.NetworkNodePortType getNetworkSecurityPortType() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(NetworkSecurityPortType_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getNetworkSecurityPortType(endpoint);
    }

	  /**
	   * getNetworkSecurityPortType
	   * @param portAddress
	   * @return NetworkNodePortType
	   */
    public Node.NAAS.Interfaces.Auth.NetworkNodePortType getNetworkSecurityPortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            Node.NAAS.Requestor.Auth.NetworkSecurityBindingStub _stub = new Node.NAAS.Requestor.Auth.NetworkSecurityBindingStub(portAddress, this);
            _stub.setPortName(getNetworkSecurityPortTypeWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
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
            if (Node.NAAS.Interfaces.Auth.NetworkNodePortType.class.isAssignableFrom(serviceEndpointInterface)) {
                Node.NAAS.Requestor.Auth.NetworkSecurityBindingStub _stub = new Node.NAAS.Requestor.Auth.NetworkSecurityBindingStub(new java.net.URL(NetworkSecurityPortType_address), this);
                _stub.setPortName(getNetworkSecurityPortTypeWSDDServiceName());
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
        if ("NetworkSecurityPortType".equals(inputPortName)) {
            return getNetworkSecurityPortType();
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
        return new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxSecurity.wsdl", "cdxSecurity");
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
            ports.add(new javax.xml.namespace.QName("NetworkSecurityPortType"));
        }
        return ports.iterator();
    }

}
