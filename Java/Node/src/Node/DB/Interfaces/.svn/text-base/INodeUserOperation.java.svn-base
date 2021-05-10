package Node.DB.Interfaces;
/**
 * <p>This class create INodeUserOperation interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeUserOperation {
  /**
   * IsUserAllowed
   * @param userID int USER_ID
   * @param opID int OPERATION_ID
   * @return int UserID if Authorized, -1 otherwise
   */
  public int IsUserAllowed (int userID, int opID);

  /**
   * SetOperationPriviledge
   * @param userID
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return boolean
   */
  public boolean SetOperationPriviledge (int userID, String[] opNames, String[] wsNames, String[] adminDomains);

  /**
   * Get a List of Assigned Operations
   * @param userID int User ID
   * @return String[] List of Operation Names
   */
  public String[] GetAssignedOperations (int userID);
}
