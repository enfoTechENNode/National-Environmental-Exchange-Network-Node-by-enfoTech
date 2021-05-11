package Node2.service.Domains;

import java.util.List;

import Node2.model.Domains.Webservice;
/**
 * <p>This class create WebserviceManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface WebserviceManager {
	/**
     * getWebservices
     * @param startIndex
     * @param recordsReturned
     * @param webserviceName
     * @param status
     * @return List
     */
    public List getWebservices(String startIndex,String recordsReturned,String webserviceName,String status);

	/**
     * getWebservice
     * @param webserviceId
     * @return Webservice
     */
    public Webservice getWebservice(String webserviceId);

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
    public void removeWebservice(String webserviceId);
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords();

}