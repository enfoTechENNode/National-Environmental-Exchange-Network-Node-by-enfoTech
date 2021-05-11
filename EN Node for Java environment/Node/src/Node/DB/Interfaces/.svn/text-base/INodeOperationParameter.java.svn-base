package Node.DB.Interfaces;

import java.util.List;
/**
 * <p>This class create INodeOperationParameter interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeOperationParameter {
  /**
   * Update Parameter List for Operation
   * @param opLogID int OPERATION_LOG_ID in SYS_OPERATION_LOG (FK)
   * @param paramNames String[] Parameter Names
   * @param paramValues Object[] Parameter Values
   * @return boolean true if success, false otherwise
   */
  public boolean UpdateParameterValues (int opLogID, String[] paramNames, Object[] paramValues);
  /**
   * UpdateExistParameterValues
   * @param opLogID int OPERATION_LOG_ID in SYS_OPERATION_LOG (FK)
   * @param paramNames String[] Parameter Names
   * @param paramValues Object[] Parameter Values
   * @return boolean true if success, false otherwise
   */
  public boolean UpdateExistParameterValues (int opLogID, String[] paramNames, Object[] paramValues);
  /**
   * GetParameterValues
   * @param opLogId int OPERATION_LOG_ID in SYS_OPERATION_LOG (FK)
   * @return Object[]
   */
  public Object[] GetParameterValues (int opLogId);
  /**
   * GetParameterValues
   * @param transID
   * @return Object[]
   */
  public Object[] GetParameterValues (String transID);
  /**
   * GetParameterNames
   * @param opID
   * @return List
   */  public List GetParameterNames(int opID);
}
