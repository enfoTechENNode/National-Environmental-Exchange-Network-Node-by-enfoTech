/**
 * NetworkNode.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.WebServices.Interfaces;

public interface NetworkNode extends javax.xml.rpc.Service {

    // A set of services for the National Environmental Information Exchange
    // Network (NEIEN)
	  /**
	   * getNetworkNodePortTypeAddress
	   * @param 
	   * @return String
	   */
    public java.lang.String getNetworkNodePortTypeAddress();

	  /**
	   * getNetworkNodePortType
	   * @param 
	   * @return NetworkNodePortType
	   */
    public NetworkNodePortType getNetworkNodePortType() throws javax.xml.rpc.ServiceException;

	  /**
	   * getNetworkNodePortType
	   * @param portAddress
	   * @return NetworkNodePortType
	   */
    public NetworkNodePortType getNetworkNodePortType(java.net.URL portAddress) throws javax.xml.rpc.ServiceException;
}
