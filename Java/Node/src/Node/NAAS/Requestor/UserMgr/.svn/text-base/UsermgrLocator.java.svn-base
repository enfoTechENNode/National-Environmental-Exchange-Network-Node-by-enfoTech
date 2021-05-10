/**
 * UsermgrLocator.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Requestor.UserMgr;

public class UsermgrLocator extends org.apache.axis.client.Service implements Node.NAAS.Interfaces.UserMgr.Usermgr {

    // A simple user identity management service.

    // Use to get a proxy class for UserMgrPortType
    private java.lang.String UserMgrPortType_address = "https://naas.epacdxnode.net/xml/usermgr.wsdl";

	  /**
	   * getUserMgrPortTypeAddress
	   * @param 
	   * @return String
	   */
    public java.lang.String getUserMgrPortTypeAddress() {
        return UserMgrPortType_address;
    }

    // The WSDD service name defaults to the port name.
    private java.lang.String UserMgrPortTypeWSDDServiceName = "UserMgrPortType";

	  /**
	   * getUserMgrPortTypeWSDDServiceName
	   * @param 
	   * @return String
	   */
    public java.lang.String getUserMgrPortTypeWSDDServiceName() {
        return UserMgrPortTypeWSDDServiceName;
    }

	  /**
	   * setUserMgrPortTypeWSDDServiceName
	   * @param name
	   * @return 
	   */
    public void setUserMgrPortTypeWSDDServiceName(java.lang.String name) {
        UserMgrPortTypeWSDDServiceName = name;
    }

	  /**
	   * getUserMgrPortType
	   * @param 
	   * @return UserMgrPortType
	   */
    public Node.NAAS.Interfaces.UserMgr.UserMgrPortType getUserMgrPortType() throws javax.xml.rpc.ServiceException {
       java.net.URL endpoint;
        try {
            endpoint = new java.net.URL(UserMgrPortType_address);
        }
        catch (java.net.MalformedURLException e) {
            throw new javax.xml.rpc.ServiceException(e);
        }
        return getUserMgrPortType(endpoint);
    }

	  /**
	   * getUserMgrPortType
	   * @param portAddress
	   * @return UserMgrPortType
	   */
    public Node.NAAS.Interfaces.UserMgr.UserMgrPortType getUserMgrPortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException {
        try {
            Node.NAAS.Requestor.UserMgr.UserMgrBindingStub _stub = new Node.NAAS.Requestor.UserMgr.UserMgrBindingStub(portAddress, this);
            _stub.setPortName(getUserMgrPortTypeWSDDServiceName());
            return _stub;
        }
        catch (org.apache.axis.AxisFault e) {
            return null;
        }
    }

	  /**
	   * setUserMgrPortTypeEndpointAddress
	   * @param address
	   * @return 
	   */
    public void setUserMgrPortTypeEndpointAddress(java.lang.String address) {
        UserMgrPortType_address = address;
    }

    /**
     * For the given interface, get the stub implementation.
     * If this service has no port for the given interface,
     * then ServiceException is thrown.
     */
    public java.rmi.Remote getPort(Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        try {
            if (Node.NAAS.Interfaces.UserMgr.UserMgrPortType.class.isAssignableFrom(serviceEndpointInterface)) {
                Node.NAAS.Requestor.UserMgr.UserMgrBindingStub _stub = new Node.NAAS.Requestor.UserMgr.UserMgrBindingStub(new java.net.URL(UserMgrPortType_address), this);
                _stub.setPortName(getUserMgrPortTypeWSDDServiceName());
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
    public java.rmi.Remote getPort(javax.xml.namespace.QName portName, Class serviceEndpointInterface) throws javax.xml.rpc.ServiceException {
        if (portName == null) {
            return getPort(serviceEndpointInterface);
        }
        String inputPortName = portName.getLocalPart();
        if ("UserMgrPortType".equals(inputPortName)) {
            return getUserMgrPortType();
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
        return new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.wsdl", "usermgr");
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
            ports.add(new javax.xml.namespace.QName("UserMgrPortType"));
        }
        return ports.iterator();
    }

    /**
    * setEndpointAddress.
    * @param portName
    * @param address
    * @return 
    */
    public void setEndpointAddress(java.lang.String portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        if ("UserMgrPortType".equals(portName)) {
            setUserMgrPortTypeEndpointAddress(address);
        }
        else { // Unknown Port Name
            throw new javax.xml.rpc.ServiceException(" Cannot set Endpoint Address for Unknown Port" + portName);
        }
    }

    /**
    * setEndpointAddress.
    * @param portName
    * @param address
    * @return 
    */
    public void setEndpointAddress(javax.xml.namespace.QName portName, java.lang.String address) throws javax.xml.rpc.ServiceException {
        setEndpointAddress(portName.getLocalPart(), address);
    }

}
