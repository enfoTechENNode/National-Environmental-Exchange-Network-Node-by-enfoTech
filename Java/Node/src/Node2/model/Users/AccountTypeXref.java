package Node2.model.Users;

import java.util.Date;
import java.util.HashSet;
import java.util.Set;

import Node2.model.BaseObject;
import Node2.model.Configurations.PageLayout;
import Node2.model.Domains.Domain;
/**
 * <p>This class create AccountTypeXref.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AccountTypeXref extends BaseObject {

	private Long accountTypeXrefId;
    private AccountType accountType=null;
    private User user=null;
    private Domain domain=null;
	/**
	 * @return the accountTypeXrefId
	 */
	public Long getAccountTypeXrefId() {
		return accountTypeXrefId;
	}
	/**
	 * @param accountTypeXrefId the accountTypeXrefId to set
	 */
	public void setAccountTypeXrefId(Long accountTypeXrefId) {
		this.accountTypeXrefId = accountTypeXrefId;
	}
	/**
	 * @return the accountType
	 */
	public AccountType getAccountType() {
		return accountType;
	}
	/**
	 * @param accountType the accountType to set
	 */
	public void setAccountType(AccountType accountType) {
		this.accountType = accountType;
	}
	/**
	 * @return the user
	 */
	public User getUser() {
		return user;
	}
	/**
	 * @param user the user to set
	 */
	public void setUser(User user) {
		this.user = user;
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
    
   
}
