package Node2.dao.Domains;

import java.util.List;

import Node2.dao.DAO;
import Node2.model.Domains.Domain;
/**
 * <p>This class create DomainDAO.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface DomainDAO extends DAO {
	  /**
	   * getDomains
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param domainPermissions
	   * @param domainName
	   * @param status
	   * @param statusMessage
	   * @return List
	   */
    public List getDomains(String startIndex,String recordsReturned,String sort,String dir,String[] domainPermissions,String domainName,String status,String statusMessage);

	  /**
	   * getDomain
	   * @param domainId
	   * @return Domain
	   */
    public Domain getDomain(Long domainId);

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
    public void removeDomain(Long domainId);
    
	  /**
	   * getTotalRecords
	   * @param 
	   * @return long
	   */
    public long getTotalRecords();
}
