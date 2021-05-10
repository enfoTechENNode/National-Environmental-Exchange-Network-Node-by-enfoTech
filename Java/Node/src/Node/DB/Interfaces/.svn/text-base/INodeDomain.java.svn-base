package Node.DB.Interfaces;

import Node.Biz.Administration.Domain;
import Node.Biz.Administration.Status;
/**
 * <p>This class create INodeDomain interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeDomain {
  /**
   * Get Running Status of Domain
   * @param doaminID int DOMAIN_ID
   * @return String Status
   */
  public String GetDomainStatus (int domainID);

  /**
   * Get Domain Status
   * @param domain String Domain Name
   * @return String
   */
  public String GetDomainStatus (String domain);

  /**
   * Get Domain Name
   * @param domainID int DOMAIN_ID
   * @return String DOMAIN_NAME
   */
  public String GetDomainName (int domainID);

  /**
   * Get Domain Name
   * @param opName String Operation Name
   * @param wsName String Web Service Name
   * @return String Domain Name
   */
  public String GetDomainName (String opName, String wsName);

  /**
   * Get List of Domains
   * @return String[] List of Domains
   */
  public String[] GetDomains ();

  /**
   * Get List of Domains
   * @param domainNames String[]
   * @return Domain[]
   */
  public Domain[] GetDomains (String[] domainNames);

  /**
   * Get a List of Operations in a Domain
   * @param domainName String DOMAIN_NAME
   * @return String[] List of Operations
   */
  public String[] GetOperations (String domainName);

  /**
   * Get Domain ID
   * @param domain String Name of Domain
   * @return int Domain ID, -1 if not found
   */
  public int GetDomainID (String domain);

  /**
   * Get Domain Names not in Current List
   * @param domains String[] Array of Domain Names
   * @return String[] Array of Domain Names Available, null if none left
   */
  public String[] GetOppositeDomains (String[] domains);

  /**
   * Search Domain List
   * @param domainPermissions String[] Domain Permissions
   * @param domainName String Domain Name
   * @param status String Domain Status
   * @return Domain[] List of Domains that fit Search Criteria
   */
  public Domain[] SearchDomains (String[] domainPermissions, String domainName, String status);

  /**
   * Get Domain
   * @param domainID int Domain ID
   * @return Domain
   */
  public Domain GetDomain (int domainID);

  /**
   * Get Domain
   * @param domainName String Domain Name
   * @return Domain
   */
  public Domain GetDomain (String domainName);

  /**
   * Save Domain
   * @param domainID int Domain ID
   * @param description String Domain Description
   * @param status String Domain Status
   * @param message String Domain Message
   * @return boolean true if sucessful, false otherwise
   */
  public boolean SaveDomain (int domainID, String description, String status, String message, boolean isAllowSubmit,
                             boolean isAllowDonwload, boolean isAllowQuery, boolean isAllowSolciit, boolean isAllowNotify,
                             String[] admins);

  /**
   * Save New Domain
   * @param domainName String Domain Name
   * @param description String Domain Description
   * @param status String Domain Status
   * @param message String Domain Message
   * @return boolean true if sucessful, false otherwise
   */
  public boolean SaveDomain (String domainName, String description, String status, String message, boolean isAllowSubmit,
                             boolean isAllowDonwload, boolean isAllowQuery, boolean isAllowSolciit, boolean isAllowNotify,
                             String[] admins);

  /**
   * Get Admins of a Domain
   * @param domainNames[] String Domain Names
   * @return String[] Array of Admins, null if none found
   */
  public String[] GetAdmins (String[] domainNames);

  /**
   * Get Email Addresses of Admins of a Domain
   * @param domain String Domain Name
   * @return String[] Array of Email Addresses of Admins, null if none found
   */
  public String[] GetAdminEmailAddresses (String domain);

  /**
   * Get IDs of Node Domain and or None Domain
   * @return int[] [0] = Node Domain, [1] = None Domain
   */
  public int[] GetDomainIDs ();

  /**
   * Get Status Biz Object
   * @return Status
   */
  public Status GetNodeStatus ();
}
