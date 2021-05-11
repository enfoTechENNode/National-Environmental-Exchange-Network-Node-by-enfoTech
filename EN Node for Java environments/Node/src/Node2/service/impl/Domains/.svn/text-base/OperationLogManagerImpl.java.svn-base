package Node2.service.impl.Domains;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import Node2.dao.Domains.OperationLogDAO;
import Node2.model.Domains.OperationLog;
import Node2.service.Domains.OperationLogManager;

/**
 * <p>This class create OperationLogManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationLogManagerImpl implements OperationLogManager {
    private static Log log = LogFactory.getLog(OperationLogManagerImpl.class);
    private OperationLogDAO dao;

	/**
     * setOperationLogDAO
     * @param dao
     * @return 
     */
    public void setOperationLogDAO(OperationLogDAO dao) {
        this.dao = dao;
    }

	  /**
	   * getOperations
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
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
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getOperationLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate, String[] domainPermission, String version){
        return dao.getOperationLogs( startIndex, recordsReturned, sort, dir,opName, opType, opWebservice, opStatus, opDomain, userId, token, transactionId, startDate, endDate,domainPermission, version);
    }

	  /**
	   * getScheduledTasksLogs
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param opName
	   * @param opType
	   * @param opWebservice
	   * @param opStatus
	   * @param opDomain
	   * @param userId
	   * @param token
	   * @param transId
	   * @param startDate
	   * @param endDate
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getScheduledTasksLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate, String[] domainPermission,String version){
        return dao.getScheduledTasksLogs( startIndex, recordsReturned, sort, dir,opName, opType, opWebservice, opStatus, opDomain, userId, token, transactionId, startDate, endDate, domainPermission,version);
    }
    
	  /**
	   * getNotificationLogs
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param nodeAddress
	   * @param opWebservice
	   * @param startDate
	   * @param endDate
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getNotificationLogs(String startIndex,String recordsReturned,String sort,String dir,String nodeAddress,String opWebservice,String startDate, String endDate, String[] domainPermission,String version){
        return dao.getNotificationLogs( startIndex, recordsReturned, sort, dir, nodeAddress, opWebservice, startDate, endDate,domainPermission, version);
    }

	  /**
	   * getOperationLog
	   * @param OperationLogId
	   * @return OperationLog
	   */
    public OperationLog getOperationLog(String OperationLogId) {
        OperationLog operationLog = dao.getOperationLog(Long.valueOf(OperationLogId));

        if (operationLog == null) {
            log.warn("OperationId '" + OperationLogId + "' not found in database.");
        }

        return operationLog;
    }

	  /**
	   * saveOperationLog
	   * @param OperationLog
	   * @return 
	   */
    public void saveOperationLog(OperationLog operationLog) {
        dao.saveOperationLog(operationLog);
    }

	  /**
	   * removeOperationLog
	   * @param operationLogId
	   * @return 
	   */
    public void removeOperationLog(String operationLogId) {
        dao.removeOperationLog(Long.valueOf(operationLogId));
    }
    
	  /**
	   * getTotalOperationLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalOperationLogRecords(){
    	return dao.getTotalOperationLogRecords();
    }
    
	  /**
	   * getTotalScheduledTasksLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalScheduledTasksLogRecords(){
    	return dao.getTotalScheduledTasksLogRecords();
    }
    
	  /**
	   * getTotalNotificationLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalNotificationLogRecords(){
    	return dao.getTotalNotificationLogRecords();
    }

}
