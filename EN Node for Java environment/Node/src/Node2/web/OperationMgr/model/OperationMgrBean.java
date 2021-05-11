package Node2.web.OperationMgr.model;

import org.apache.struts.upload.FormFile;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create OperationMgrBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class OperationMgrBean extends BaseBean {
	private String operationId = "";
	private String operationName = "";
	private String operationVersion = "";
	private String dataFlow = "";
	private String upload = "";
	private String generate = "";
	private String submit = "";
	private String URL = "";
	private String userName = "";
	private String password = "";
	private String domainName = "";
	private String getStatus = "";
	private String getStatusComplete = "";
	private String getStatusError = "";
	private String view = "";

	// for upload file
	private String message = "";
	private String transID = "";
	private String submissionFileId = "";
	private String validationFileId = "";
	private FormFile uploadFile = null;


	/**
	 * @return the operationId
	 */
	public String getOperationId() {
		return operationId;
	}


	/**
	 * @param operationId the operationId to set
	 */
	public void setOperationId(String operationId) {
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
	 * @return the operationVersion
	 */
	public String getOperationVersion() {
		return operationVersion;
	}


	/**
	 * @param operationVersion the operationVersion to set
	 */
	public void setOperationVersion(String operationVersion) {
		this.operationVersion = operationVersion;
	}


	/**
	 * @return the dataFlow
	 */
	public String getDataFlow() {
		return dataFlow;
	}


	/**
	 * @param dataFlow the dataFlow to set
	 */
	public void setDataFlow(String dataFlow) {
		this.dataFlow = dataFlow;
	}


	/**
	 * @return the upload
	 */
	public String getUpload() {
		return upload;
	}


	/**
	 * @param upload the upload to set
	 */
	public void setUpload(String upload) {
		this.upload = upload;
	}


	/**
	 * @return the generate
	 */
	public String getGenerate() {
		return generate;
	}


	/**
	 * @param generate the generate to set
	 */
	public void setGenerate(String generate) {
		this.generate = generate;
	}


	/**
	 * @return the submit
	 */
	public String getSubmit() {
		return submit;
	}


	/**
	 * @param submit the submit to set
	 */
	public void setSubmit(String submit) {
		this.submit = submit;
	}


	/**
	 * @return the uRL
	 */
	public String getURL() {
		return URL;
	}


	/**
	 * @param url the uRL to set
	 */
	public void setURL(String url) {
		URL = url;
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
	 * @return the password
	 */
	public String getPassword() {
		return password;
	}


	/**
	 * @param password the password to set
	 */
	public void setPassword(String password) {
		this.password = password;
	}


	/**
	 * @return the domainName
	 */
	public String getDomainName() {
		return domainName;
	}


	/**
	 * @param domainName the domainName to set
	 */
	public void setDomainName(String domainName) {
		this.domainName = domainName;
	}


	/**
	 * @return the getStatus
	 */
	public String getGetStatus() {
		return getStatus;
	}


	/**
	 * @param getStatus the getStatus to set
	 */
	public void setGetStatus(String getStatus) {
		this.getStatus = getStatus;
	}


	/**
	 * @return the getStatusComplete
	 */
	public String getGetStatusComplete() {
		return getStatusComplete;
	}


	/**
	 * @param getStatusComplete the getStatusComplete to set
	 */
	public void setGetStatusComplete(String getStatusComplete) {
		this.getStatusComplete = getStatusComplete;
	}


	/**
	 * @return the getStatusError
	 */
	public String getGetStatusError() {
		return getStatusError;
	}


	/**
	 * @param getStatusError the getStatusError to set
	 */
	public void setGetStatusError(String getStatusError) {
		this.getStatusError = getStatusError;
	}


	/**
	 * @return the view
	 */
	public String getView() {
		return view;
	}


	/**
	 * @param view the view to set
	 */
	public void setView(String view) {
		this.view = view;
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
	 * @return the transID
	 */
	public String getTransID() {
		return transID;
	}


	/**
	 * @param transID the transID to set
	 */
	public void setTransID(String transID) {
		this.transID = transID;
	}


	/**
	 * @return the submissionFileId
	 */
	public String getSubmissionFileId() {
		return submissionFileId;
	}


	/**
	 * @param submissionFileId the submissionFileId to set
	 */
	public void setSubmissionFileId(String submissionFileId) {
		this.submissionFileId = submissionFileId;
	}


	/**
	 * @return the validationFileId
	 */
	public String getValidationFileId() {
		return validationFileId;
	}


	/**
	 * @param validationFileId the validationFileId to set
	 */
	public void setValidationFileId(String validationFileId) {
		this.validationFileId = validationFileId;
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


	public void clean() {
		this.operationId = "";
		this.operationName = "";
		this.operationVersion = "";
		this.dataFlow = "";
		this.upload = "";
		this.generate = "";
		this.submit = "";
		this.URL = "";
		this.userName = "";
		this.password = "";
		this.domainName = "";
		this.getStatus = "";
		this.getStatusComplete = "";
		this.getStatusError = "";
		this.view = "";
		this.message = "";
		this.transID = "";
		this.submissionFileId = "";
		this.validationFileId = "";
		this.uploadFile = null;
	}

}
