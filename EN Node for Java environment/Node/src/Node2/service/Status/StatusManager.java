package Node2.service.Status;

import java.util.List;

import Node2.model.Status.Status;
/**
 * <p>This class create StatusManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface StatusManager {
	/**
     * getStatusList
     * @param startIndex
     * @param recordsReturned
     * @return List
     */
    public List getStatusList(String startIndex,String recordsReturned);

	/**
     * getStatus
     * @param pId
     * @return Status
     */
    public Status getStatus(String pId);
    
	/**
     * getTotalRecords
     * @param 
     * @return int
     */
    public int getTotalRecords();

}