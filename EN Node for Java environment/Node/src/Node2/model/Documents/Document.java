package Node2.model.Documents;

import java.sql.Blob;
import java.sql.Timestamp;

import Node2.model.BaseObject;
import Node2.model.Users.User;
/**
 * <p>This class create Document.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Document extends BaseObject {

	/**
	 * 
	 */
	private Long fileId;
    private String documentId;
    private String transId;
    private String fileName;
    private String fileType;
    private String statusCD;
    private String dataFlowName;
    private String submitUrl;
    private String submitToken;
    private Timestamp submitDTTM;
    private Blob fileContent;
    private int fileSize;
    private Timestamp createdDate;
    private String createdBy;
    private Timestamp updatedDate;
    private String updatedBy;
	/**
	 * @return the fileId
	 */
	public Long getFileId() {
		return fileId;
	}
	/**
	 * @param fileId the fileId to set
	 */
	public void setFileId(Long fileId) {
		this.fileId = fileId;
	}
	/**
	 * @return the documentId
	 */
	public String getDocumentId() {
		return documentId;
	}
	/**
	 * @param documentId the documentId to set
	 */
	public void setDocumentId(String documentId) {
		this.documentId = documentId;
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
	 * @return the fileName
	 */
	public String getFileName() {
		return fileName;
	}
	/**
	 * @param fileName the fileName to set
	 */
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}
	/**
	 * @return the fileType
	 */
	public String getFileType() {
		return fileType;
	}
	/**
	 * @param fileType the fileType to set
	 */
	public void setFileType(String fileType) {
		this.fileType = fileType;
	}
	/**
	 * @return the statusCD
	 */
	public String getStatusCD() {
		return statusCD;
	}
	/**
	 * @param statusCD the statusCD to set
	 */
	public void setStatusCD(String statusCD) {
		this.statusCD = statusCD;
	}
	/**
	 * @return the dataFlowName
	 */
	public String getDataFlowName() {
		return dataFlowName;
	}
	/**
	 * @param dataFlowName the dataFlowName to set
	 */
	public void setDataFlowName(String dataFlowName) {
		this.dataFlowName = dataFlowName;
	}
	/**
	 * @return the submitUrl
	 */
	public String getSubmitUrl() {
		return submitUrl;
	}
	/**
	 * @param submitUrl the submitUrl to set
	 */
	public void setSubmitUrl(String submitUrl) {
		this.submitUrl = submitUrl;
	}
	/**
	 * @return the submitToken
	 */
	public String getSubmitToken() {
		return submitToken;
	}
	/**
	 * @param submitToken the submitToken to set
	 */
	public void setSubmitToken(String submitToken) {
		this.submitToken = submitToken;
	}
	/**
	 * @return the submitDTTM
	 */
	public Timestamp getSubmitDTTM() {
		return submitDTTM;
	}
	/**
	 * @param submitDTTM the submitDTTM to set
	 */
	public void setSubmitDTTM(Timestamp submitDTTM) {
		this.submitDTTM = submitDTTM;
	}
	/**
	 * @return the fileContent
	 */
	public Blob getFileContent() {
		return fileContent;
	}
	/**
	 * @param fileContent the fileContent to set
	 */
	public void setFileContent(Blob fileContent) {
		this.fileContent = fileContent;
	}
	/**
	 * @return the fileSize
	 */
	public int getFileSize() {
		return fileSize;
	}
	/**
	 * @param fileSize the fileSize to set
	 */
	public void setFileSize(int fileSize) {
		this.fileSize = fileSize;
	}
	/**
	 * @return the createdDate
	 */
	public Timestamp getCreatedDate() {
		return createdDate;
	}
	/**
	 * @param createdDate the createdDate to set
	 */
	public void setCreatedDate(Timestamp createdDate) {
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
	public Timestamp getUpdatedDate() {
		return updatedDate;
	}
	/**
	 * @param updatedDate the updatedDate to set
	 */
	public void setUpdatedDate(Timestamp updatedDate) {
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
    
}
