package Node.DB.Interfaces;
/**
 * <p>This class create INodeAccountType interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeAccountType {
  /**
   * Find out if User is Locally Managed Node User
   * @param userName String LOGIN_NAME
   * @return boolean true if NODE_USER, false otherwise
   */
  public boolean IsLocallyManagedNodeUser (String userName);

  /**
   * Get Account Type String provided AccountTypeID
   * @param acountTypeID int ACCOUNT_TYPE_ID
   * @return String ACCOUNT_TYPE
   */
  public String GetAccountType (int acountTypeID);

  /**
   * Get List of Account Types
   * @return String[] List of Account Types, null if none found
   */
  public String[] GetAccountTypes ();

  /**
   * Get Account Type ID
   * @param accountType String Account Type Name
   * @return int Account Type ID, -1 if not found
   */
  public int GetAccountTypeID (String accountType);

  /**
   * Get Account Type IDs
   * @return int[] [0] = CONSOLE_USER, [1] = LOCAL_NODE_USER, [2] = NAAS_NODE_USER
   */
  public int[] GetAccountTypeIDs ();
}
