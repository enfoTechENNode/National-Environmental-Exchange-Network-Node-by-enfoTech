package Node2.dao.Domains;

import java.util.List;

import Node2.dao.DAO;
import Node2.model.Domains.Webservice;
/**
 * <p>This class create WebserviceDAO.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface WebserviceDAO extends DAO {
	  /**
	   * getWebservices
	   * @param startIndex
	   * @param recordsReturned
	   * @param webserviceName
	   * @param webserviceDesc
	   * @return List
	   */
    public List getWebservices(String startIndex,String recordsReturned,String webserviceName,String webserviceDesc);

	  /**
	   * getWebservice
	   * @param webserviceId
	   * @return Webservice
	   */
    public Webservice getWebservice(Long webserviceId);

	  /**
	   * saveWebservice
	   * @param webservice
	   * @return 
	   */
    public void saveWebservice(Webservice webservice);

	  /**
	   * removeWebservice
	   * @param webserviceId
	   * @return 
	   */
    public void removeWebservice(Long webserviceId);
    
	  /**
	   * getTotalRecords
	   * @param 
	   * @return int
	   */
    public int getTotalRecords();
}
