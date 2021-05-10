package Node2.service.Domains;

import java.util.List;

import Node2.model.Domains.OperationLog;
/**
 * <p>This class create OperationLogManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface OperationLogManager {
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
    public List getOperationLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate,String[] domainPermission, String version);

	  /**
	   * getOperationLog
	   * @param operId
	   * @return OperationLog
	   */
    public OperationLog getOperationLog(String operId);

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
    public List getScheduledTasksLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transId,String startDate,String endDate, String[] domainPermission,String version);

    public List getNotificationLogs(String startIndex,String recordsReturned,String sort,String dir,String nodeAddress,String opWebservice,String startDate, String endDate, String[] domainPermission,String version);

	  /**
	   * saveOperationLog
	   * @param OperationLog
	   * @return 
	   */
    public void saveOperationLog(OperationLog OperationLog);

	  /**
	   * removeOperationLog
	   * @param operId
	   * @return 
	   */
    public void removeOperationLog(String operLogId);
    
	  /**
	   * getTotalOperationLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalOperationLogRecords();
    
	  /**
	   * getTotalScheduledTasksLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalScheduledTasksLogRecords();

	  /**
	   * getTotalNotificationLogRecords
	   * @param 
	   * @return long
	   */
    public long getTotalNotificationLogRecords();

}