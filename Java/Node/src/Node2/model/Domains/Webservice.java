package Node2.model.Domains;

import java.util.HashSet;
import java.util.Set;

import Node2.model.BaseObject;
/**
 * <p>This class create Webservice.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Webservice extends BaseObject {

	/**
	 * 
	 */
	private Long webServiceId;
    private String webServiceName;
    private String webServiceDesc;
    private Set operations = new HashSet();
    private Set domains = new HashSet();
	/**
	 * @return the webServiceId
	 */
	public Long getWebServiceId() {
		return webServiceId;
	}
	/**
	 * @param webServiceId the webServiceId to set
	 */
	public void setWebServiceId(Long webServiceId) {
		this.webServiceId = webServiceId;
	}
	/**
	 * @return the webServiceName
	 */
	public String getWebServiceName() {
		return webServiceName;
	}
	/**
	 * @param webServiceName the webServiceName to set
	 */
	public void setWebServiceName(String webServiceName) {
		this.webServiceName = webServiceName;
	}
	/**
	 * @return the webServiceDesc
	 */
	public String getWebServiceDesc() {
		return webServiceDesc;
	}
	/**
	 * @param webServiceDesc the webServiceDesc to set
	 */
	public void setWebServiceDesc(String webServiceDesc) {
		this.webServiceDesc = webServiceDesc;
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
	 * @return the domains
	 */
	public Set getDomains() {
		return domains;
	}
	/**
	 * @param domains the domains to set
	 */
	public void setDomains(Set domains) {
		this.domains = domains;
	}
    
    
 }
