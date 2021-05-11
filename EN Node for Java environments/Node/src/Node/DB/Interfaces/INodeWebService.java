package Node.DB.Interfaces;
/**
 * <p>This class create INodeWebService interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeWebService {
  /**
   * Get WEB_SERVICE_ID from SYS_WEB_SERVICE provided WEB_SERVICE_NAME
   * @param wsName String WEB_SERVICE_NAME
   * @return int WEB_SERVICE_ID if successful, -1 otherwise
   */
  public int GetWebServiceID (String wsName);

  /**
   * Get List of Web Services
   * @return String[] List of Web Service Names
   */
  public String[] GetWSList ();

  /**
   * Get WebService Name
   * @param wsID int WebService ID
   * @return String WebService Name, null if not found
   */
  public String GetWSName (int wsID);

  /**
   * Get Web Service IDs
   * @return int[] [0] = submit, [1] = download, [2] = query, [3] = solicit, [4] = notify
   */
  public int[] GetWSIDs ();
}
