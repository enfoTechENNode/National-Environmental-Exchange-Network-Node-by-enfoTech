package Node2.model.Domains;

import java.sql.Timestamp;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;
/**
 * <p>This class create OperationLog.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationLog {
	// Column Names
	private long operationLogId;
	private String transId = "";
	private String userName = "";
	private String requestorIP = "";
	private String suppliedTransId = "";
	private String token = "";
	private String nodeAddress = "";
	private String returnURL = "";
	private String serviceType = "";
	private Timestamp startDate;
	private Timestamp endDate;
	private String hostName = "";
	private String createdBy = "";
	private Date createdDate;
	private String updatedBy = "";
	private Date updatedDate;
	private Operation operation = null;
    private Set operationLogStatus = new HashSet();
	/**
	 * @return the operationLogId
	 */
	public long getOperationLogId() {
		return operationLogId;
	}
	/**
	 * @param operationLogId the operationLogId to set
	 */
	public void setOperationLogId(long operationLogId) {
		this.operationLogId = operationLogId;
	}
	/**
	 * @return the transId
	 */
	public String getTransId() {
		return transId;
	}
	/**
	 * @param transId the transId to set
	 */
	public void setTransId(String transId) {
		this.transId = transId;
	}
	/**
	 * @return the userName
	 */
	public String getUserName() {
		return userName;
	}
	/**
	 * @param userName the userName to set
	 */
	public void setUserName(String userName) {
		this.userName = userName;
	}
	/**
	 * @return the requestorIP
	 */
	public String getRequestorIP() {
		return requestorIP;
	}
	/**
	 * @param requestorIP the requestorIP to set
	 */
	public void setRequestorIP(String requestorIP) {
		this.requestorIP = requestorIP;
	}
	/**
	 * @return the suppliedTransId
	 */
	public String getSuppliedTransId() {
		return suppliedTransId;
	}
	/**
	 * @param suppliedTransId the suppliedTransId to set
	 */
	public void setSuppliedTransId(String suppliedTransId) {
		this.suppliedTransId = suppliedTransId;
	}
	/**
	 * @return the token
	 */
	public String getToken() {
		return token;
	}
	/**
	 * @param token the token to set
	 */
	public void setToken(String token) {
		this.token = token;
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
	 * @return the returnURL
	 */
	public String getReturnURL() {
		return returnURL;
	}
	/**
	 * @param returnURL the returnURL to set
	 */
	public void setReturnURL(String returnURL) {
		this.returnURL = returnURL;
	}
	/**
	 * @return the serviceType
	 */
	public String getServiceType() {
		return serviceType;
	}
	/**
	 * @param serviceType the serviceType to set
	 */
	public void setServiceType(String serviceType) {
		this.serviceType = serviceType;
	}
	/**
	 * @return the startDate
	 */
	public Timestamp getStartDate() {
		return startDate;
	}
	/**
	 * @param startDate the startDate to set
	 */
	public void setStartDate(Timestamp startDate) {
		this.startDate = startDate;
	}
	/**
	 * @return the endDate
	 */
	public Timestamp getEndDate() {
		return endDate;
	}
	/**
	 * @param endDate the endDate to set
	 */
	public void setEndDate(Timestamp endDate) {
		this.endDate = endDate;
	}
	/**
	 * @return the hostName
	 */
	public String getHostName() {
		return hostName;
	}
	/**
	 * @param hostName the hostName to set
	 */
	public void setHostName(String hostName) {
		this.hostName = hostName;
	}
	/**
	 * @return the createdBy
	 */
	public String getCreatedBy() {
		return createdBy;
	}
	/**
	 * @param createdBy the createdBy to set
	 */
	public void setCreatedBy(String createdBy) {
		this.createdBy = createdBy;
	}
	/**
	 * @return the createdDate
	 */
	public Date getCreatedDate() {
		return createdDate;
	}
	/**
	 * @param createdDate the createdDate to set
	 */
	public void setCreatedDate(Date createdDate) {
		this.createdDate = createdDate;
	}
	/**
	 * @return the updatedBy
	 */
	public String getUpdatedBy() {
		return updatedBy;
	}
	/**
	 * @param updatedBy the updatedBy to set
	 */
	public void setUpdatedBy(String updatedBy) {
		this.updatedBy = updatedBy;
	}
	/**
	 * @return the updatedDate
	 */
	public Date getUpdatedDate() {
		return updatedDate;
	}
	/**
	 * @param updatedDate the updatedDate to set
	 */
	public void setUpdatedDate(Date updatedDate) {
		this.updatedDate = updatedDate;
	}
	/**
	 * @return the operation
	 */
	public Operation getOperation() {
		return operation;
	}
	/**
	 * @param operation the operation to set
	 */
	public void setOperation(Operation operation) {
		this.operation = operation;
	}
	/**
	 * @return the operationLogStatus
	 */
	public Set getOperationLogStatus() {
		return operationLogStatus;
	}
	/**
	 * @param operationLogStatus the operationLogStatus to set
	 */
	public void setOperationLogStatus(Set operationLogStatus) {
		this.operationLogStatus = operationLogStatus;
	}
	
}
