package Node.DB.Interfaces;
/**
 * <p>This class create INodeOperationLogStatus interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeOperationLogStatus {
  // Add a Status
	  /**
	   * CreateLogStatus
	   * @param opLogID 
	   * @param status 
	   * @param message 
	   * @return String STATUS_CD
	   */
  public int CreateLogStatus (int opLogID, String status, String message);

  /**
   * Get Latest Status
   * @param opLogID int OPERATION_LOG_ID
   * @return String STATUS_CD
   */
  public String GetLatestStatus (int opLogID);
}
