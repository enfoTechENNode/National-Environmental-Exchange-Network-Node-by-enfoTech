package Node.Task.model;

import java.sql.Blob;
import java.sql.Date;
/**
 * <p>This class create GetStatusModel.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetStatusModel {
	int submitID = 0;
	String submitUserID = null;
	String submitPassword = null;
	String operationName = null;
	String statusCD = null;
	Date submitDate = null;
	String submitURL = null;
	String versionNo = null;
	String transID = null;
	String supplyTransID = null;
	Blob fileContent = null;
	String dataFlow = null;
	
	
	/**
	 * @return the submitUserID
	 */
	public String getSubmitUserID() {
		return submitUserID;
	}
	/**
	 * @param submitUserID the submitUserID to set
	 */
	public void setSubmitUserID(String submitUserID) {
		this.submitUserID = submitUserID;
	}
	/**
	 * @return the submitPassword
	 */
	public String getSubmitPassword() {
		return submitPassword;
	}
	/**
	 * @param submitPassword the submitPassword to set
	 */
	public void setSubmitPassword(String submitPassword) {
		this.submitPassword = submitPassword;
	}
	  /**
	   * getSubmitID
	   * @param 
	   * @return int
	   */
	public int getSubmitID() {
		return submitID;
	}
	  /**
	   * setSubmitID
	   * @param submitID
	   * @return
	   */
	public void setSubmitID(int submitID) {
		this.submitID = submitID;
	}
	  /**
	   * getOperationName
	   * @param 
	   * @return String
	   */
	public String getOperationName() {
		return operationName;
	}
	  /**
	   * setOperationName
	   * @param operationName
	   * @return
	   */
	public void setOperationName(String operationName) {
		this.operationName = operationName;
	}
	  /**
	   * getStatusCD
	   * @param 
	   * @return String
	   */
	public String getStatusCD() {
		return statusCD;
	}
	  /**
	   * setStatusCD
	   * @param statusCD
	   * @return
	   */
	public void setStatusCD(String statusCD) {
		this.statusCD = statusCD;
	}
	  /**
	   * getSubmitURL
	   * @param 
	   * @return String
	   */
	public String getSubmitURL() {
		return submitURL;
	}
	  /**
	   * setSubmitURL
	   * @param submitURL
	   * @return
	   */
	public void setSubmitURL(String submitURL) {
		this.submitURL = submitURL;
	}
	  /**
	   * getVersionNo
	   * @param 
	   * @return String
	   */
	public String getVersionNo() {
		return versionNo;
	}
	  /**
	   * setVersionNo
	   * @param versionNo
	   * @return
	   */
	public void setVersionNo(String versionNo) {
		this.versionNo = versionNo;
	}
	  /**
	   * getTransID
	   * @param 
	   * @return String
	   */
	public String getTransID() {
		return transID;
	}
	  /**
	   * setTransID
	   * @param transID
	   * @return
	   */
	public void setTransID(String transID) {
		this.transID = transID;
	}
	  /**
	   * getSupplyTransID
	   * @param 
	   * @return String
	   */
	public String getSupplyTransID() {
		return supplyTransID;
	}
	  /**
	   * setSupplyTransID
	   * @param supplyTransID
	   * @return
	   */
	public void setSupplyTransID(String supplyTransID) {
		this.supplyTransID = supplyTransID;
	}
	  /**
	   * getFileContent
	   * @param 
	   * @return Blob
	   */
	public Blob getFileContent() {
		return fileContent;
	}
	  /**
	   * setFileContent
	   * @param fileContent
	   * @return
	   */
	public void setFileContent(Blob fileContent) {
		this.fileContent = fileContent;
	}
	  /**
	   * getDataFlow
	   * @param 
	   * @return String
	   */
	public String getDataFlow() {
		return dataFlow;
	}
	  /**
	   * setDataFlow
	   * @param dataFlow
	   * @return
	   */
	public void setDataFlow(String dataFlow) {
		this.dataFlow = dataFlow;
	}
	  /**
	   * getSubmitDate
	   * @param 
	   * @return Date
	   */
	public Date getSubmitDate() {
		return submitDate;
	}
	  /**
	   * setSubmitDate
	   * @param submitDate
	   * @return
	   */
	public void setSubmitDate(Date submitDate) {
		this.submitDate = submitDate;
	}
	
	
}
