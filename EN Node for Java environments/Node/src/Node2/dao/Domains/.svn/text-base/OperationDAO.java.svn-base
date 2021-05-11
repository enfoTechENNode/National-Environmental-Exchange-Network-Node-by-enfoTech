package Node2.dao.Domains;

import java.util.List;
import Node2.dao.DAO;
import Node2.model.Domains.Operation;
/**
 * <p>This class create OperationDAO.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface OperationDAO extends DAO {
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
    public List getOperations(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain);

	  /**
	   * getOperation
	   * @param operId
	   * @return Operation
	   */
    public Operation getOperation(Long operId);

	  /**
	   * saveOperation
	   * @param Operation
	   * @return 
	   */
    public void saveOperation(Operation Operation);

	  /**
	   * removeOperation
	   * @param operId
	   * @return 
	   */
    public void removeOperation(Long operId);
    
	  /**
	   * getTotalRecords
	   * @param 
	   * @return int
	   */
    public int getTotalRecords();
}
