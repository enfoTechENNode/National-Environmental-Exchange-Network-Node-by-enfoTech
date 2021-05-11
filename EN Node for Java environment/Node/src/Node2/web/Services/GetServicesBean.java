package Node2.web.Services;

import org.apache.struts.upload.FormFile;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create GetServicesBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetServicesBean extends BaseBean {
  private String nodeIdentifier = "";
  private String nodeName = "";
  private String nodeAddress = "";
  private String organizationIdentifier = "";
  private String nodeContact = null;
  private String nodeVersionIdentifier = "";
  private String nodeDeploymentTypeCode = "";
  private String nodeStatus = "";
  private String nodePropertyName = "";
  
  private String message = "";
  private FormFile uploadFile = null;
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
 * @return the nodePropertyName
 */
public String getNodePropertyName() {
	return nodePropertyName;
}
/**
 * @param nodePropertyName the nodePropertyName to set
 */
public void setNodePropertyName(String nodePropertyName) {
	this.nodePropertyName = nodePropertyName;
}
/**
 * @return the message
 */
public String getMessage() {
	return message;
}
/**
 * @param message the message to set
 */
public void setMessage(String message) {
	this.message = message;
}
/**
 * @return the uploadFile
 */
public FormFile getUploadFile() {
	return uploadFile;
}
/**
 * @param uploadFile the uploadFile to set
 */
public void setUploadFile(FormFile uploadFile) {
	this.uploadFile = uploadFile;
}

  
}
