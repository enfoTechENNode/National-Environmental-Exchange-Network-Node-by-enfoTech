package Node2.service.Domains;

import java.util.List;

import Node2.model.Domains.OperationLogStatus;
/**
 * <p>This class create OperationLogStatusManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface OperationLogStatusManager {
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
    public List getOperationLogStatuss(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate);

	  /**
	   * getOperationLogStatus
	   * @param operLogStatusId
	   * @return OperationLogStatus
	   */
    public OperationLogStatus getOperationLogStatus(String operLogStatusId);

	  /**
	   * saveOperationLogStatus
	   * @param operationLogStatus
	   * @return 
	   */
    public void saveOperationLogStatus(OperationLogStatus OperationLogStatus);

	  /**
	   * removeOperationLogStatus
	   * @param operId
	   * @return 
	   */
    public void removeOperationLogStatus(String operLogStatusId);
    
	  /**
	   * getTotalRecords
	   * @param 
	   * @return int
	   */
    public int getTotalRecords();

}