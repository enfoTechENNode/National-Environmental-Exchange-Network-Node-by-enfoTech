package Node2.service.impl.Domains;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Domains.OperationLogStatusDAO;
import Node2.model.Domains.OperationLogStatus;
import Node2.service.Domains.OperationLogStatusManager;

/**
 * <p>This class create OperationLogStatusManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationLogStatusManagerImpl implements OperationLogStatusManager {
    private static Log log = LogFactory.getLog(OperationLogStatusManagerImpl.class);
    private OperationLogStatusDAO dao;

	/**
     * setOperationLogStatusDAO
     * @param dao
     * @return 
     */
    public void setOperationLogStatusDAO(OperationLogStatusDAO dao) {
        this.dao = dao;
    }

	  /**
	   * getOperationLogStatuss
	   * @param startIndex
	   * @param recordsReturned
	   * @param opName
	   * @param opType
	   * @param opWebservice
	   * @param opStatus
	   * @param opDomain
	   * @param userId
	   * @param token
	   * @param transactionId
	   * @param startDate
	   * @param endDate
	   * @return List
	   */
    public List getOperationLogStatuss(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate){
        return dao.getOperationLogStatuss( startIndex, recordsReturned, opName, opType, opWebservice, opStatus, opDomain, userId, token, transactionId, startDate, endDate);
    }

	  /**
	   * getOperationLogStatus
	   * @param operLogStatusId
	   * @return OperationLogStatus
	   */
    public OperationLogStatus getOperationLogStatus(String operationLogStatusId) {
        OperationLogStatus operationLogStatus = dao.getOperationLogStatus(Long.valueOf(operationLogStatusId));

        if (operationLogStatus == null) {
            log.warn("OperationId '" + operationLogStatusId + "' not found in database.");
        }

        return operationLogStatus;
    }

	  /**
	   * saveOperationLogStatus
	   * @param operationLogStatus
	   * @return 
	   */
    public void saveOperationLogStatus(OperationLogStatus operationLogStatus) {
        dao.saveOperationLogStatus(operationLogStatus);
    }

	  /**
	   * removeOperationLogStatus
	   * @param operId
	   * @return 
	   */
    public void removeOperationLogStatus(String operationLogStatusId) {
        dao.removeOperationLogStatus(Long.valueOf(operationLogStatusId));
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
