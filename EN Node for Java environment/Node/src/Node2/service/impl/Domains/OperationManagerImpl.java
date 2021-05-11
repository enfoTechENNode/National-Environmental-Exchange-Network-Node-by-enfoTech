package Node2.service.impl.Domains;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Domains.OperationDAO;
import Node2.model.Domains.Operation;
import Node2.service.Domains.OperationManager;

/**
 * <p>This class create OperationManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationManagerImpl implements OperationManager {
    private static Log log = LogFactory.getLog(OperationManagerImpl.class);
    private OperationDAO dao;

	/**
     * setOperationDAO
     * @param dao
     * @return 
     */
    public void setOperationDAO(OperationDAO dao) {
        this.dao = dao;
    }

	/**
     * getOperations
     * @param startIndex
     * @param recordsReturned
     * @param opName
     * @param opType
     * @param opWebservice
     * @param opStatus
     * @param opDomain
     * @return List
     */
    public List getOperations(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain) {
        return dao.getOperations(startIndex, recordsReturned,opName,opType,opWebservice,opStatus,opDomain);
    }

	/**
     * getOperation
     * @param OperationId
     * @return Operation
     */
    public Operation getOperation(String OperationId) {
        Operation Operation = dao.getOperation(Long.valueOf(OperationId));

        if (Operation == null) {
            log.warn("OperationId '" + OperationId + "' not found in database.");
        }

        return Operation;
    }

	/**
     * saveOperation
     * @param Operation
     * @return Operation
     */
    public Operation saveOperation(Operation Operation) {
        dao.saveOperation(Operation);

        return Operation;
    }

	/**
     * removeOperation
     * @param OperationId
     * @return 
     */
    public void removeOperation(String OperationId) {
        dao.removeOperation(Long.valueOf(OperationId));
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
