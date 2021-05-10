package Node2.model.Users;

import java.util.Date;
import java.util.HashSet;
import java.util.Set;

import Node2.model.BaseObject;
import Node2.model.Configurations.PageLayout;
/**
 * <p>This class create User.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class User extends BaseObject {
    private Long id;
    private String firstName;
    private String lastName;
    private String midInit;
    private String loginName;
    private String loginPassword;
    private String userStatusCD;
    private String last4SSN;
    private String changePWDFlag;
    private String phone;
    private String comments;
    private Date createdDate;
    private String createdBy;
    private Date updatedDate;
    private String updatedBy;    
    private PageLayout pageLayout=null;
    private Set accountTypeXrefs = new HashSet();
	/**
	 * @return the id
	 */
	public Long getId() {
		return id;
	}
	/**
	 * @param id the id to set
	 */
	public void setId(Long id) {
		this.id = id;
	}
	/**
	 * @return the firstName
	 */
	public String getFirstName() {
		return firstName;
	}
	/**
	 * @param firstName the firstName to set
	 */
	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}
	/**
	 * @return the lastName
	 */
	public String getLastName() {
		return lastName;
	}
	/**
	 * @param lastName the lastName to set
	 */
	public void setLastName(String lastName) {
		this.lastName = lastName;
	}
	/**
	 * @return the midInit
	 */
	public String getMidInit() {
		return midInit;
	}
	/**
	 * @param midInit the midInit to set
	 */
	public void setMidInit(String midInit) {
		this.midInit = midInit;
	}
	/**
	 * @return the loginName
	 */
	public String getLoginName() {
		return loginName;
	}
	/**
	 * @param loginName the loginName to set
	 */
	public void setLoginName(String loginName) {
		this.loginName = loginName;
	}
	/**
	 * @return the loginPassword
	 */
	public String getLoginPassword() {
		return loginPassword;
	}
	/**
	 * @param loginPassword the loginPassword to set
	 */
	public void setLoginPassword(String loginPassword) {
		this.loginPassword = loginPassword;
	}
	/**
	 * @return the userStatusCD
	 */
	public String getUserStatusCD() {
		return userStatusCD;
	}
	/**
	 * @param userStatusCD the userStatusCD to set
	 */
	public void setUserStatusCD(String userStatusCD) {
		this.userStatusCD = userStatusCD;
	}
	/**
	 * @return the last4SSN
	 */
	public String getLast4SSN() {
		return last4SSN;
	}
	/**
	 * @param last4SSN the last4SSN to set
	 */
	public void setLast4SSN(String last4SSN) {
		this.last4SSN = last4SSN;
	}
	/**
	 * @return the changePWDFlag
	 */
	public String getChangePWDFlag() {
		return changePWDFlag;
	}
	/**
	 * @param changePWDFlag the changePWDFlag to set
	 */
	public void setChangePWDFlag(String changePWDFlag) {
		this.changePWDFlag = changePWDFlag;
	}
	/**
	 * @return the phone
	 */
	public String getPhone() {
		return phone;
	}
	/**
	 * @param phone the phone to set
	 */
	public void setPhone(String phone) {
		this.phone = phone;
	}
	/**
	 * @return the comments
	 */
	public String getComments() {
		return comments;
	}
	/**
	 * @param comments the comments to set
	 */
	public void setComments(String comments) {
		this.comments = comments;
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
	 * @return the pageLayout
	 */
	public PageLayout getPageLayout() {
		return pageLayout;
	}
	/**
	 * @param pageLayout the pageLayout to set
	 */
	public void setPageLayout(PageLayout pageLayout) {
		this.pageLayout = pageLayout;
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
