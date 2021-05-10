package Node2.service.impl.Status;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Status.StatusDAO;
import Node2.model.Status.Status;
import Node2.service.Status.StatusManager;
/**
 * <p>This class create StatusManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class StatusManagerImpl implements StatusManager {
    private static Log log = LogFactory.getLog(StatusManagerImpl.class);
    private StatusDAO dao;

	/**
     * setStatusDAO
     * @param dao
     * @return 
     */
    public void setStatusDAO(StatusDAO dao) {
        this.dao = dao;
    }

	/**
     * getStatusList
     * @param startIndex
     * @param recordsReturned
     * @return List
     */
    public List getStatusList(String startIndex,String recordsReturned) {
        return dao.getStatusList(startIndex, recordsReturned);
    }

	/**
     * getStatus
     * @param pId
     * @return Status
     */
    public Status getStatus(String pId) {
        Status Status = dao.getStatus(Long.valueOf(pId));

        if (Status == null) {
            log.warn("process Id '" + pId + "' not found in database.");
        }

        return Status;
    }
    
	/**
     * getTotalRecords
     * @param 
     * @return int
     */
    public int getTotalRecords(){
    	return dao.getTotalRecords();
    }

}
