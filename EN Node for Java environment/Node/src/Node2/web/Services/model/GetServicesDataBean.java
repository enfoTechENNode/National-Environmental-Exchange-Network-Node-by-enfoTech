package Node2.web.Services.model;

/**
 * <p>
 * This class create GetServicesDataBean.
 * </p>
 * <p>
 * Company: enfoTech & Consulting, Inc.
 * </p>
 * 
 * @author enfoTech
 * @version 2.0
 */

public class GetServicesDataBean {
	private String nodeIdentifier = "";
	private String nodeName = "";
	private String nodeAddress = "";
	private String organizationIdentifier = "";
	private String nodeContact = null;
	private String nodeVersionIdentifier = "";
	private String nodeDeploymentTypeCode = "";
	private String nodeStatus = "";
	private String north = "";
	private String south = "";
	private String east = "";
	private String west = "";

	private String[] nodePropertyList = null;

	/**
	 * @return the nodeIdentifier
	 */
	public String getNodeIdentifier() {
		return nodeIdentifier;
	}

	/**
	 * @param nodeIdentifier the nodeIdentifier to set
	 */
	public void setNodeIdentifier(String nodeIdentifier) {
		this.nodeIdentifier = nodeIdentifier;
	}

	/**
	 * @return the nodeName
	 */
	public String getNodeName() {
		return nodeName;
	}

	/**
	 * @param nodeName the nodeName to set
	 */
	public void setNodeName(String nodeName) {
		this.nodeName = nodeName;
	}

	/**
	 * @return the nodeAddress
	 */
	public String getNodeAddress() {
		return nodeAddress;
	}

	/**
	 * @param nodeAddress the nodeAddress to set
	 */
	public void setNodeAddress(String nodeAddress) {
		this.nodeAddress = nodeAddress;
	}

	/**
	 * @return the organizationIdentifier
	 */
	public String getOrganizationIdentifier() {
		return organizationIdentifier;
	}

	/**
	 * @param organizationIdentifier the organizationIdentifier to set
	 */
	public void setOrganizationIdentifier(String organizationIdentifier) {
		this.organizationIdentifier = organizationIdentifier;
	}

	/**
	 * @return the nodeContact
	 */
	public String getNodeContact() {
		return nodeContact;
	}

	/**
	 * @param nodeContact the nodeContact to set
	 */
	public void setNodeContact(String nodeContact) {
		this.nodeContact = nodeContact;
	}

	/**
	 * @return the nodeVersionIdentifier
	 */
	public String getNodeVersionIdentifier() {
		return nodeVersionIdentifier;
	}

	/**
	 * @param nodeVersionIdentifier the nodeVersionIdentifier to set
	 */
	public void setNodeVersionIdentifier(String nodeVersionIdentifier) {
		this.nodeVersionIdentifier = nodeVersionIdentifier;
	}

	/**
	 * @return the nodeDeploymentTypeCode
	 */
	public String getNodeDeploymentTypeCode() {
		return nodeDeploymentTypeCode;
	}

	/**
	 * @param nodeDeploymentTypeCode the nodeDeploymentTypeCode to set
	 */
	public void setNodeDeploymentTypeCode(String nodeDeploymentTypeCode) {
		this.nodeDeploymentTypeCode = nodeDeploymentTypeCode;
	}

	/**
	 * @return the nodeStatus
	 */
	public String getNodeStatus() {
		return nodeStatus;
	}

	/**
	 * @param nodeStatus the nodeStatus to set
	 */
	public void setNodeStatus(String nodeStatus) {
		this.nodeStatus = nodeStatus;
	}

	/**
	 * @return the north
	 */
	public String getNorth() {
		return north;
	}

	/**
	 * @param north the north to set
	 */
	public void setNorth(String north) {
		this.north = north;
	}

	/**
	 * @return the south
	 */
	public String getSouth() {
		return south;
	}

	/**
	 * @param south the south to set
	 */
	public void setSouth(String south) {
		this.south = south;
	}

	/**
	 * @return the east
	 */
	public String getEast() {
		return east;
	}

	/**
	 * @param east the east to set
	 */
	public void setEast(String east) {
		this.east = east;
	}

	/**
	 * @return the west
	 */
	public String getWest() {
		return west;
	}

	/**
	 * @param west the west to set
	 */
	public void setWest(String west) {
		this.west = west;
	}

	/**
	 * @return the nodePropertyList
	 */
	public String[] getNodePropertyList() {
		return nodePropertyList;
	}

	/**
	 * @param nodePropertyList the nodePropertyList to set
	 */
	public void setNodePropertyList(String[] nodePropertyList) {
		this.nodePropertyList = nodePropertyList;
	}

}
