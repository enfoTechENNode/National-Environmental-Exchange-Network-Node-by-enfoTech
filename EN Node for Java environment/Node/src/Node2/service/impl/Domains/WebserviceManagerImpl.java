package Node2.service.impl.Domains;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Domains.WebserviceDAO;
import Node2.model.Domains.Webservice;
import Node2.service.Domains.WebserviceManager;

/**
 * <p>This class create WebserviceManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class WebserviceManagerImpl implements WebserviceManager {
    private static Log log = LogFactory.getLog(WebserviceManagerImpl.class);
    private WebserviceDAO dao;

	/**
     * setWebserviceDAO
     * @param dao
     * @return 
     */
    public void setWebserviceDAO(WebserviceDAO dao) {
        this.dao = dao;
    }

	/**
     * getWebservices
     * @param startIndex
     * @param recordsReturned
     * @param webserviceName
     * @param status
     * @return List
     */
    public List getWebservices(String startIndex,String recordsReturned,String webserviceName,String status) {
        return dao.getWebservices( startIndex, recordsReturned, webserviceName, status);
    }

	/**
     * getWebservice
     * @param webserviceId
     * @return Webservice
     */
    public Webservice getWebservice(String WebserviceId) {
        Webservice webservice = dao.getWebservice(Long.valueOf(WebserviceId));

        if (webservice == null) {
            log.warn("WebserviceId '" + WebserviceId + "' not found in database.");
        }

        return webservice;
    }

	/**
     * saveWebservice
     * @param webservice
     * @return 
     */
    public void saveWebservice(Webservice webservice) {
        dao.saveWebservice(webservice);
    }

	/**
     * removeWebservice
     * @param webserviceId
     * @return 
     */
    public void removeWebservice(String webserviceId) {
        dao.removeWebservice(Long.valueOf(webserviceId));
    }
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords(){
    	return Integer.toString(dao.getTotalRecords());
    }

}
