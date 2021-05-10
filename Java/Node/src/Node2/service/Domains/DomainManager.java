package Node2.service.Domains;

import java.util.List;

import Node.Biz.Administration.User;
import Node2.model.Domains.Domain;
/**
 * <p>This class create DomainManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface DomainManager {
	  /**
	   * getDomains
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param user
	   * @param loggerName
	   * @param domainName
	   * @param status
	   * @param statusMessage
	   * @return List
	   */
    public List getDomains(String startIndex,String recordsReturned,String sort,String dir,User user,String loggerName,String domainName,String status,String statusMessage) throws Exception;

	/**
     * getDomain
     * @param domainId
     * @return Domain
     */
    public Domain getDomain(String domainId);

	/**
     * saveDomain
     * @param domain
     * @return 
     */
    public void saveDomain(Domain domain);

	/**
     * removeDomain
     * @param domainId
     * @return 
     */
    public void removeDomain(String domainId);
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords();

}