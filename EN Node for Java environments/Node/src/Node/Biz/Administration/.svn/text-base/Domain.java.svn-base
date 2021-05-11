package Node.Biz.Administration;

import java.sql.Date;
import java.util.Collection;
import java.util.HashMap;

import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeUser;
/**
 * <p>This class create Domain class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Domain {
  private int DomainID = -1;
  private String DomainName = null;
  private String DomainDesc = null;
  private String DomainStatusCD = null;
  private String DomainStatusMsg = null;
  private boolean AllowSubmit = false;
  private boolean AllowDownload = false;
  private boolean AllowQuery = false;
  private boolean AllowSolicit = false;
  private boolean AllowNotify = false;
  private HashMap Admins = null;
  private Date CreatedDate = null;
  private String CreatedBy = null;
  private Date UpdatedDate = null;
  private String UpdatedBy = null;

  /**
   * Constructor.
   * @param domainID.
   * @param loggerName.
   * @return 
   */
  public Domain (int domainID, String loggerName) throws Exception
  {
    Domain dom = null;
    if (domainID >= 0) {
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      dom = domainDB.GetDomain(domainID);
    }
    if (dom != null)
      this.Init(dom);
    else if (domainID >= 0)
      this.DomainID = domainID;
  }

  /**
   * Constructor.
   * @param domainName.
   * @param loggerName.
   * @return 
   */
  public Domain (String domainName, String loggerName) throws Exception
  {
    Domain dom = null;
    if (domainName != null) {
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      dom = domainDB.GetDomain(domainName);
    }
    if (dom != null)
      this.Init(dom);
    else if (domainName != null)
      this.DomainName = domainName;
  }

  /**
   * Constructor.
   * @param domainID.
   * @return 
   */
  public Domain (int domainID)
  {
    this.DomainID = domainID;
  }

  /**
   * Constructor.
   * @param domainName.
   * @return 
   */
  public Domain (String domainName)
  {
    this.DomainName = domainName;
  }

  /**
   * Initial Class.
   * @param d.
   * @return 
   */
  private void Init (Domain d)
  {
    this.DomainID = d.GetDomainID();
    this.DomainName = d.GetDomainName();
    this.DomainDesc = d.GetDomainDesc();
    this.DomainStatusCD = d.GetDomainStatusCD();
    this.DomainStatusMsg = d.GetDomainStatusMsg();
    this.AllowSubmit = d.GetAllowSubmit();
    this.AllowDownload = d.GetAllowDownload();
    this.AllowQuery = d.GetAllowQuery();
    this.AllowSolicit = d.GetAllowSolicit();
    this.AllowNotify = d.GetAllowNotify();
    this.SetAdmins(d.GetAdmins());
    this.CreatedDate = d.GetCreatedDate();
    this.CreatedBy = d.GetCreatedBy();
    this.UpdatedDate = d.GetUpdatedDate();
    this.UpdatedBy = d.GetUpdatedBy();
  }

  public void SetDomainID (int domainID)
  {
    this.DomainID = domainID;
  }
  public int GetDomainID ()
  {
    return this.DomainID;
  }

  public void SetDomainName (String input)
  {
    this.DomainName = input;
  }
  public String GetDomainName ()
  {
    return this.DomainName;
  }

  public void SetDomainDesc (String input)
  {
    this.DomainDesc = input;
  }
  public String GetDomainDesc ()
  {
    return this.DomainDesc;
  }

  public void SetDomainStatusCD (String input)
  {
    this.DomainStatusCD = input;
  }
  public String GetDomainStatusCD ()
  {
    return this.DomainStatusCD;
  }

  public void SetDomainStatusMsg (String input)
  {
    this.DomainStatusMsg = input;
  }
  public String GetDomainStatusMsg ()
  {
    return this.DomainStatusMsg;
  }

  public void SetAllowSubmit (boolean input)
  {
    this.AllowSubmit = input;
  }
  public boolean GetAllowSubmit ()
  {
    return this.AllowSubmit;
  }

  public void SetAllowDownload (boolean input)
  {
    this.AllowDownload = input;
  }
  public boolean GetAllowDownload ()
  {
    return this.AllowDownload;
  }

  public void SetAllowQuery (boolean input)
  {
    this.AllowQuery = input;
  }
  public boolean GetAllowQuery ()
  {
    return this.AllowQuery;
  }

  public void SetAllowSolicit (boolean input)
  {
    this.AllowSolicit = input;
  }
  public boolean GetAllowSolicit ()
  {
    return this.AllowSolicit;
  }

  public void SetAllowNotify (boolean input)
  {
    this.AllowNotify = input;
  }
  public boolean GetAllowNotify ()
  {
    return this.AllowNotify;
  }

  /**
   * GetAllowedWS.
   * @param
   * @return String Array
   */
  public String[] GetAllowedWS ()
  {
    String[] retArray = null;
    int count = 0;
    if (this.AllowDownload)
      count++;
    if (this.AllowNotify)
      count++;
    if (this.AllowQuery)
      count++;
    if (this.AllowSolicit)
      count++;
    if (this.AllowSubmit)
      count++;
    if (count > 0) {
      retArray = new String [count];
      int index = 0;
      if (this.AllowDownload) {
        retArray[index] = "DOWNLOAD";
        index++;
      }
      if (this.AllowNotify) {
        retArray[index] = "NOTIFY";
        index++;
      }
      if (this.AllowQuery) {
        retArray[index] = "QUERY";
        index++;
      }
      if (this.AllowSolicit) {
        retArray[index] = "SOLICIT";
        index++;
      }
      if (this.AllowSubmit) {
        retArray[index] = "SUBMIT";
        index++;
      }
    }
    return retArray;
  }

  public void SetAdmins (String[] admins)
  {
    if (admins != null && admins.length > 0) {
      this.Admins = new HashMap();
      for (int i = 0; i < admins.length; i++)
        this.Admins.put(admins[i],admins[i]);
    }
    else
      this.Admins = null;
  }
  public String[] GetAdmins ()
  {
    String[] retArray = null;
    if (this.Admins != null) {
      Collection coll = this.Admins.values();
      if (coll != null && coll.size() > 0) {
        Object[] array = coll.toArray();
        retArray = new String [array.length];
        for (int i = 0; i < array.length; i++)
          retArray[i] = (String)array[i];
      }
    }
    return retArray;
  }
  /**
   * GetAdminIDs.
   * @param loggerName
   * @return int Array
   */
  public int[] GetAdminIDs (String loggerName) throws Exception
  {
    int[] retArray = null;
    String[] admins = this.GetAdmins();
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    User[] adminList = userDB.GetConsoleUsers(admins);
    if (adminList != null && adminList.length > 0) {
      retArray = new int [adminList.length];
      for (int i = 0; i < adminList.length; i++)
        retArray[i] = adminList[i].GetUserID();
    }
    return retArray;
  }
  /**
   * AddAdmin.
   * @param admin
   * @return 
   */
  public void AddAdmin (String admin)
  {
    if (admin != null) {
      if (this.Admins == null)
        this.Admins = new HashMap();
      if (!this.Admins.containsKey(admin))
        this.Admins.put(admin,admin);
    }
  }

  public void SetCreatedDate (Date input)
  {
    this.CreatedDate = input;
  }
  public Date GetCreatedDate ()
  {
    return this.CreatedDate;
  }

  public void SetCreatedBy (String input)
  {
    this.CreatedBy = input;
  }
  public String GetCreatedBy ()
  {
    return this.CreatedBy;
  }

  public void SetUpdatedDate (Date input)
  {
    this.UpdatedDate = input;
  }
  public Date GetUpdatedDate ()
  {
    return this.UpdatedDate;
  }

  public void SetUpdatedBy (String input)
  {
    this.UpdatedBy = input;
  }
  public String GetUpdatedBy ()
  {
    return this.UpdatedBy;
  }

  /**
   * Save Domain.
   * @param loggerName
   * @return if success
   */
  public boolean Save (String loggerName) throws Exception
  {
    boolean retBool = false;
    if (this.DomainID >= 0) {
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      retBool = domainDB.SaveDomain(this.DomainID,this.DomainDesc,this.DomainStatusCD,this.DomainStatusMsg,this.AllowSubmit,
                                    this.AllowDownload,this.AllowQuery,this.AllowSolicit,this.AllowNotify,this.GetAdmins());
    }
    else if (this.DomainName != null) {
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      retBool = domainDB.SaveDomain(this.DomainName.toUpperCase(),this.DomainDesc,this.DomainStatusCD,this.DomainStatusMsg,this.AllowSubmit,
                                    this.AllowDownload,this.AllowQuery,this.AllowSolicit,this.AllowNotify,this.GetAdmins());
    }
    return retBool;
  }

  /**
   * Get Available Admins.
   * @param loggerName
   * @return String array
   */
  public String[] GetAvailableAdmins (String loggerName) throws Exception
  {
    String[] retArray = null;
    INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
    String[] admins = domainDB.GetAdmins(null);
    if (admins != null && admins.length > 0) {
      HashMap map = new HashMap();
      if (this.Admins != null && !this.Admins.isEmpty()) {
        for (int i = 0; i < admins.length; i++)
          if (!this.Admins.containsKey(admins[i]))
            map.put(admins[i], admins[i]);
      }
      else {
        for (int i = 0; i < admins.length; i++)
          map.put(admins[i], admins[i]);
      }
      if (!map.isEmpty()) {
        Object[] temp = map.values().toArray();
        if (temp != null && temp.length > 0) {
          retArray = new String [temp.length];
          for (int i = 0; i < retArray.length; i++)
            retArray[i] = (String)temp[i];
        }
      }
    }
    return retArray;
  }

  /**
   * Search Domains.
   * @param loggerName
   * @param admin
   * @param domainName
   * @param status
   * @return Domain array
   */
  public static Domain[] SearchDomains (String loggerName, User admin, String domainName, String status) throws Exception
  {
    Domain[] retArray = null;
    if (admin.IsConsoleUser()) {
      String[] domainPermission = null;
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      if (admin.IsNodeAdmin()) {
        domainPermission = domainDB.GetDomains();
        HashMap map = new HashMap();
        if (domainPermission != null)
          for (int i = 0; i < domainPermission.length; i++)
            map.put(domainPermission[i],domainPermission[i]);
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          domainPermission = new String [temp.length];
          for (int i = 0; i < domainPermission.length; i++)
            domainPermission[i] = (String)temp[i];
        }
        else
          domainPermission = null;
      }
      else
        domainPermission = admin.GetAssignedDomains();
      String name = domainName != null && domainName.equals("") ? null : domainName;
      String tempStatus = status != null && status.equals("") ? null : status;
      retArray = domainDB.SearchDomains(domainPermission,name,tempStatus);
    }
    return retArray;
  }

  /**
   * Get Domains.
   * @param admin
   * @param loggerName
   * @return Domain array
   */
  public static Domain[] GetDomains (User admin, String loggerName) throws Exception
  {
    Domain[] retArray = null;
    if (admin != null) {
      INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
      if (admin.IsNodeAdmin())
        retArray = domainDB.GetDomains(null);
      else
        retArray = domainDB.GetDomains(admin.GetAssignedDomains());
    }
    return retArray;
  }

  /**
   * Is Unique Name.
   * @param loggerName
   * @param domainName
   * @return Domain array
   */
  public static boolean IsUniqueName (String loggerName, String domainName) throws Exception
  {
    INodeDomain domain = DBManager.GetNodeDomain(loggerName);
    Domain d = domain.GetDomain(domainName);
    if (d == null)
      return true;
    else
      return false;
  }
}
