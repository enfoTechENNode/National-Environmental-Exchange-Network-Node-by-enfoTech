package Node2.model.Domains;

import java.util.Date;
import java.util.HashSet;
import java.util.Set;

import Node2.model.BaseObject;
import Node2.model.Domains.Webservice;
import Node2.model.Domains.Domain;
/**
 * <p>This class create Operation.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Operation extends BaseObject {

	private Long operationId;
    private String operationName;
    private String operationDesc;
    private String operationType;
    private String operationConfig;
    private String operationStatusCD;
    private String operationStatusMSG;
    private Date createdDate;
    private String createdBy;
    private Date updatedDate;
    private String updatedBy;
    private String versionNo;
    private Set operationLogs = new HashSet();
	private Domain domain = null;
	private Webservice webservice = null;
	/**
	 * @return the operationId
	 */
	public Long getOperationId() {
		return operationId;
	}
	/**
	 * @param operationId the operationId to set
	 */
	public void setOperationId(Long operationId) {
		this.operationId = operationId;
	}
	/**
	 * @return the operationName
	 */
	public String getOperationName() {
		return operationName;
	}
	/**
	 * @param operationName the operationName to set
	 */
	public void setOperationName(String operationName) {
		this.operationName = operationName;
	}
	/**
	 * @return the operationDesc
	 */
	public String getOperationDesc() {
		return operationDesc;
	}
	/**
	 * @param operationDesc the operationDesc to set
	 */
	public void setOperationDesc(String operationDesc) {
		this.operationDesc = operationDesc;
	}
	/**
	 * @return the operationType
	 */
	public String getOperationType() {
		return operationType;
	}
	/**
	 * @param operationType the operationType to set
	 */
	public void setOperationType(String operationType) {
		this.operationType = operationType;
	}
	/**
	 * @return the operationConfig
	 */
	public String getOperationConfig() {
		return operationConfig;
	}
	/**
	 * @param operationConfig the operationConfig to set
	 */
	public void setOperationConfig(String operationConfig) {
		this.operationConfig = operationConfig;
	}
	/**
	 * @return the operationStatusCD
	 */
	public String getOperationStatusCD() {
		return operationStatusCD;
	}
	/**
	 * @param operationStatusCD the operationStatusCD to set
	 */
	public void setOperationStatusCD(String operationStatusCD) {
		this.operationStatusCD = operationStatusCD;
	}
	/**
	 * @return the operationStatusMSG
	 */
	public String getOperationStatusMSG() {
		return operationStatusMSG;
	}
	/**
	 * @param operationStatusMSG the operationStatusMSG to set
	 */
	public void setOperationStatusMSG(String operationStatusMSG) {
		this.operationStatusMSG = operationStatusMSG;
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
	 * @return the versionNo
	 */
	public String getVersionNo() {
		return versionNo;
	}
	/**
	 * @param versionNo the versionNo to set
	 */
	public void setVersionNo(String versionNo) {
		this.versionNo = versionNo;
	}
	/**
	 * @return the operationLogs
	 */
	public Set getOperationLogs() {
		return operationLogs;
	}
	/**
	 * @param operationLogs the operationLogs to set
	 */
	public void setOperationLogs(Set operationLogs) {
		this.operationLogs = operationLogs;
	}
	/**
	 * @return the domain
	 */
	public Domain getDomain() {
		return domain;
	}
	/**
	 * @param domain the domain to set
	 */
	public void setDomain(Domain domain) {
		this.domain = domain;
	}
	/**
	 * @return the webservice
	 */
	public Webservice getWebservice() {
		return webservice;
	}
	/**
	 * @param webservice the webservice to set
	 */
	public void setWebservice(Webservice webservice) {
		this.webservice = webservice;
	}
   
    
}
