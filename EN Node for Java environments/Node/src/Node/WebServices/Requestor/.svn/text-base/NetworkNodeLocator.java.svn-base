/**
 * NetworkNodeLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.WebServices.Requestor;
import Node.WebServices.Interfaces.NetworkNode;
import Node.WebServices.Interfaces.NetworkNodePortType;

public class NetworkNodeLocator extends org.apache.axis.client.Service implements NetworkNode {

    // A set of services for the National Environmental Information Exchange
    // Network (NEIEN)

    // Use to get a proxy class for NetworkNodePortType
    private final java.lang.String NetworkNodePortType_address = "http://localhost/Node.WebServices/NodeServices.asmx";

    public java.lang.String getNetworkNodePortTypeAddress() {
        return NetworkNodePortType_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String NetworkNodePortTypeWSDDServiceName = "NetworkNodePortType";

    /**
     * getNetworkNodePortTypeWSDDServiceName
     * @param 
     * @return String
     */
    public java.lang.String getNetworkNodePortTypeWSDDServiceName() {
        return NetworkNodePortTypeWSDDServiceName;
    }

    /**
     * setNetworkNodePortTypeWSDDServiceName
     * @param name
     * @return 
     */
    public void setNetworkNodePortTypeWSDDServiceName(java.lang.String name) {
        NetworkNodePortTypeWSDDServiceName = name;
    }

    /**
     * getNetworkNodePortType
     * @param 
     * @return NetworkNodePortType
     */
    public NetworkNodePortType getNetworkNodePortType() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(NetworkNodePortType_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getNetworkNodePortType(endpoint);
    }

    /**
     * getNetworkNodePortType
     * @param portAddress
     * @return NetworkNodePortType
     */
    public NetworkNodePortType getNetworkNodePortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            NetworkNodeBindingStub _stub = new NetworkNodeBindingStub(portAddress, this);
            _stub.setPortName(getNetworkNodePortTypeWSDDServiceName());
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
            if (NetworkNodePortType.class.isAssignableFrom(serviceEndpointInterface)) {
                NetworkNodeBindingStub _stub = new NetworkNodeBindingStub(new java.net.URL(NetworkNodePortType_address), this);
                _stub.setPortName(getNetworkNodePortTypeWSDDServiceName());
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
        if ("NetworkNodePortType".equals(inputPortName)) {
            return getNetworkNodePortType();
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
        return new javax.xml.namespace.QName("http://www.exchangenetwork.net/schema/v1.0/node.wsdl", "NetworkNode");
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
            ports.add(new javax.xml.namespace.QName("NetworkNodePortType"));
        }
        return ports.iterator();
    }

}
