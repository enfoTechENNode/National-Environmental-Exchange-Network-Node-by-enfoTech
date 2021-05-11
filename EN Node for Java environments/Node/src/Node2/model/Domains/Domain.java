package Node2.model.Domains;

import java.util.Date;
import java.util.HashSet;
import java.util.Set;
import Node2.model.BaseObject;
/**
 * <p>This class create Domain.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class Domain extends BaseObject {

	/**
	 * 
	 */
	private Long domainId;
    private String domainName;
    private String domainDesc;
    private String domainStatusCD;
    private String domainStatusMSG;
    private Date createdDate;
    private String createdBy;
    private Date updatedDate;
    private String updatedBy;
    private Set operations = new HashSet();
    private Set webservices = new HashSet();
    private Set accountTypeXrefs = new HashSet();
	/**
	 * @return the domainId
	 */
	public Long getDomainId() {
		return domainId;
	}
	/**
	 * @param domainId the domainId to set
	 */
	public void setDomainId(Long domainId) {
		this.domainId = domainId;
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
	 * @return the domainDesc
	 */
	public String getDomainDesc() {
		return domainDesc;
	}
	/**
	 * @param domainDesc the domainDesc to set
	 */
	public void setDomainDesc(String domainDesc) {
		this.domainDesc = domainDesc;
	}
	/**
	 * @return the domainStatusCD
	 */
	public String getDomainStatusCD() {
		return domainStatusCD;
	}
	/**
	 * @param domainStatusCD the domainStatusCD to set
	 */
	public void setDomainStatusCD(String domainStatusCD) {
		this.domainStatusCD = domainStatusCD;
	}
	/**
	 * @return the domainStatusMSG
	 */
	public String getDomainStatusMSG() {
		return domainStatusMSG;
	}
	/**
	 * @param domainStatusMSG the domainStatusMSG to set
	 */
	public void setDomainStatusMSG(String domainStatusMSG) {
		this.domainStatusMSG = domainStatusMSG;
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
	 * @return the operations
	 */
	public Set getOperations() {
		return operations;
	}
	/**
	 * @param operations the operations to set
	 */
	public void setOperations(Set operations) {
		this.operations = operations;
	}
	/**
	 * @return the webservices
	 */
	public Set getWebservices() {
		return webservices;
	}
	/**
	 * @param webservices the webservices to set
	 */
	public void setWebservices(Set webservices) {
		this.webservices = webservices;
	}
	/**
	 * @return the accountTypeXrefs
	 */
	public Set getAccountTypeXrefs() {
		return accountTypeXrefs;
	}
	/**
	 * @param accountTypeXrefs the accountTypeXrefs to set
	 */
	public void setAccountTypeXrefs(Set accountTypeXrefs) {
		this.accountTypeXrefs = accountTypeXrefs;
	}
    
 }
