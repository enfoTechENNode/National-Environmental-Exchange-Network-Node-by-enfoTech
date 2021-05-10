package Node2.service.impl.Domains;

import java.util.HashMap;
import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node2.dao.Domains.DomainDAO;
import Node2.model.Domains.Domain;
import Node2.service.Domains.DomainManager;
/**
 * <p>This class create DomainManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DomainManagerImpl implements DomainManager {
    private static Log log = LogFactory.getLog(DomainManagerImpl.class);
    private DomainDAO dao;

	/**
     * setDomainDAO
     * @param dao
     * @return 
     */
    public void setDomainDAO(DomainDAO dao) {
        this.dao = dao;
    }

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
    public List getDomains(String startIndex,String recordsReturned,String sort,String dir,User admin,String loggerName,String domainName,String status,String statusMessage) throws Exception {
        Domain[] retArray = null;
        String[] domainPermission = null;
        String name=null;
        String tempStatus=null;
        
        if (admin.IsConsoleUser()) {
          domainPermission = null;
          INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
          if (admin.IsNodeAdmin()) {
            domainPermission = domainDB.GetDomains();
            HashMap map = new HashMap();
            if (domainPermission != null)
              for (int i = 0; i < domainPermission.length; i++)
                map.put(domainPermission[i],domainPermission[i]);
            if (!map.isEmpty()) {
              Object[] temp = map.values().toArray();
              domainPermission = new String [temp.length];
              for (int i = 0; i < domainPermission.length; i++)
                domainPermission[i] = (String)temp[i];
            }
            else
              domainPermission = null;
          }
          else
            domainPermission = admin.GetAssignedDomains();
          name = domainName != null && domainName.equals("") ? null : domainName;
          tempStatus = status != null && status.equals("") ? null : status;
        }
    	
    	return dao.getDomains( startIndex, recordsReturned, sort, dir, domainPermission, name, tempStatus, statusMessage);
    }

	/**
     * getDomain
     * @param domainId
     * @return Domain
     */
    public Domain getDomain(String DomainId) {
        Domain domain = dao.getDomain(Long.valueOf(DomainId));

        if (domain == null) {
            log.warn("DomainId '" + DomainId + "' not found in database.");
        }

        return domain;
    }

	/**
     * saveDomain
     * @param domain
     * @return 
     */
    public void saveDomain(Domain domain) {
        dao.saveDomain(domain);
    }

	/**
     * removeDomain
     * @param domainId
     * @return 
     */
    public void removeDomain(String domainId) {
        dao.removeDomain(Long.valueOf(domainId));
    }
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords(){
    	return Long.toString(dao.getTotalRecords());
    }

}
