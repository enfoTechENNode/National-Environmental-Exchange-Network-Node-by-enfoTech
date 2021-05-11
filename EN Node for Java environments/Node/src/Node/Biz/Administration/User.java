package Node.Biz.Administration;

import com.enfotech.basecomponent.utility.security.Cryptography;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.TreeMap;
import org.apache.log4j.Level;

import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeUser;
import Node.DB.Interfaces.INodeUserOperation;
import Node.NAAS.Requestor.NAASRequestor;
import Node.NAAS.Types.Policy.PolicyInfo;
import Node.NAAS.Types.UserMgr.UserInfo;
import Node.Phrase;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
/**
 * <p>This class create User class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class User {
  // Save Return Constants
  public static int DATABASE_ERROR = -1;
  public static int SUCCESS = 0;
  public static int NAAS_ERROR = -2;
  public static int EMAIL_ERROR = -3;

  private int UserID = -1;
  private boolean ChangePWD = false;
  private String FirstName = null;
  private String MiddleInitial = null;
  private String LastName = null;
  private String LoginName = null;
  private String LoginPassword = null;
  private String EmailAddress = null;
  private String Address = null;
  private String SupplAddress = null;
  private String City = null;
  private String State = null;
  private String ZipCode = null;
  private String Country = null;
  private String Status = null;
  private String Last4SSN = null;
  private String Phone = null;
  private String Comments = null;
  private HashMap AssignedDomains = null;
  private HashMap AssignedOperations = null;
  private String AccountType = null;
  private String CreatedDate = null;
  private String CreatedBy = null;
  private String UpdatedDate = null;
  private String UpdatedBy = null;

  /**
   * Constructor.
   * @param loginName .
   * @param loggerName .
   * @return 
   */
  public User (String loginName, String loggerName) throws Exception {
    User user = null;
    if (loginName != null) {
      INodeUser userDB = DBManager.GetNodeUser(loggerName);
      user = userDB.GetUser(loginName);
      if (user == null) {
        NAASIntegration naas = new NAASIntegration(loggerName);
        UserInfo[] list = naas.GetUserList(loginName);
        if (list != null && list.length > 0) {
          user = new User(loginName);
          user.SetAccountType(Phrase.NAASNodeUser);
        }
      }
    }
    if (user != null)
      this.Init(user, loggerName);
    else
      this.LoginName = loginName;
  }

  /**
   * Constructor.
   * @param loggerName .
   * @return 
   */
  public User (String loginName)
  {
    if (loginName != null)
      this.LoginName = loginName;
  }

  /**
   * Constructor.
   * @param userID .
   * @param loggerName .
   * @return 
   */
  public User(int userID, String loggerName) throws Exception {
    User user = null;
    if (userID >= 0) {
      INodeUser userDB = DBManager.GetNodeUser(loggerName);
      user = userDB.GetUser(userID);
    }
    if (user != null)
      this.Init(user, loggerName);
    else
      this.UserID = userID;
  }

  /**
   * Constructor.
   * @param userID .
   * @return 
   */
  public User (int userID)
  {
    if (userID >= 0)
      this.UserID = userID;
  }

  /**
   * Initial Class.
   * @param user.
   * @param loggerName.
   * @return 
   */
  private void Init (User user, String loggerName) throws Exception
  {
    this.AccountType = user.GetAccountType();
    this.Address = user.GetAddress();
    if (this.IsConsoleUser()) {
      String[] domains = user.GetAssignedDomains();
      if (domains != null && domains.length > 0) {
        this.AssignedDomains = new HashMap();
        for (int i = 0; i < domains.length; i++)
          this.AssignedDomains.put(domains[i], domains[i]);
      }
    }
    else {
      String[] ops = null;
      if (this.IsLocalNodeUser()) {
        INodeUserOperation userOpDB = DBManager.GetNodeUserOperation(loggerName);
        ops = userOpDB.GetAssignedOperations(user.GetUserID());
      }
      else if (this.IsNAASNodeUser()) {
        NAASIntegration naas = new NAASIntegration(loggerName);
        PolicyInfo[] info = naas.GetPolicyList(user.GetLoginName());
        if (info != null && info.length > 0) {
          int count = 0;
          for (int i = 0; i < info.length; i++) {
            if (info[i].getAction().equalsIgnoreCase(NAASRequestor.ACTION_PERMIT))
              count++;
          }
          ops = new String [count];
          count = 0;
          INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
          for (int i = 0; i < info.length && count < ops.length; i++) {
            if (info[i].getAction().equalsIgnoreCase(NAASRequestor.ACTION_PERMIT)) {
              String opName = info[i].getRequest().toUpperCase();
              ops[count] = opDB.GetDomainName(opName,info[i].getMethod().toUpperCase()) + ": " + info[i].getMethod().toUpperCase() + "." + opName;
              count++;
            }
          }
        }
      }
      if (ops != null && ops.length > 0) {
        this.AssignedOperations = new HashMap();
        for (int i = 0; i < ops.length; i++)
          this.AssignedOperations.put(ops[i],ops[i]);
      }
    }
    this.ChangePWD = user.GetChangePWD();
    this.City = user.GetCity();
    this.Comments = user.GetComments();
    this.Country = user.GetCountry();
    this.CreatedBy =  user.GetCreatedBy();
    this.CreatedDate = user.GetCreatedDate();
    this.EmailAddress = user.GetEmailAddress();
    this.FirstName = user.GetFirstName();
    this.Last4SSN = user.GetLast4SSN();
    this.LastName = user.GetLastName();
    this.LoginName = user.GetLoginName();
    this.LoginPassword = user.GetLoginPassword();
    this.MiddleInitial = user.GetMiddleInitial();
    this.Phone = user.GetPhone();
    this.State = user.GetState();
    this.Status = user.GetStatus();
    this.SupplAddress = user.GetSupplAddress();
    this.UpdatedBy = user.GetUpdatedBy();
    this.UpdatedDate = user.GetUpdatedDate();
    this.UserID = user.GetUserID();
    this.ZipCode = user.GetZipCode();
  }

  public void SetUserID (int input)
  {
    this.UserID = input;
  }
  public int GetUserID ()
  {
    return this.UserID;
  }

  public void SetChangePWD (boolean input)
  {
    this.ChangePWD = input;
  }
  public boolean GetChangePWD ()
  {
    return this.ChangePWD;
  }

  public void SetFirstName (String input)
  {
    if (input != null && input.equals(""))
      this.FirstName = null;
    else
      this.FirstName = input;
  }
  public String GetFirstName ()
  {
    return this.FirstName;
  }

  public void SetMiddleInitial (String input)
  {
    if (input != null && input.equals(""))
      this.MiddleInitial = null;
    else
      this.MiddleInitial = input;
  }
  public String GetMiddleInitial ()
  {
    return this.MiddleInitial;
  }

  public void SetLastName (String input)
  {
    if (input != null && input.equals(""))
      this.LastName = null;
    else
      this.LastName = input;
  }
  public String GetLastName ()
  {
    return this.LastName;
  }

  public void SetLoginName (String input)
  {
    if (input != null && input.equals(""))
      this.LoginName = null;
    else
      this.LoginName = input;
  }
  public String GetLoginName ()
  {
    return this.LoginName;
  }

  public void SetLoginPassword (String input) throws Exception
  {
    if (input == null || input.equals(""))
      this.LoginPassword = null;
    else if (input != null) {
      Cryptography crypt = new Cryptography();
      this.LoginPassword = crypt.Decrypting(input,Phrase.CryptKey);
    }
  }
  public String GetLoginPassword ()
  {
    return this.LoginPassword;
  }

  public void SetEmailAddress (String input)
  {
    if (input != null && input.equals(""))
      this.EmailAddress = null;
    else
      this.EmailAddress = input;
  }
  public String GetEmailAddress ()
  {
    return this.EmailAddress;
  }

  public void SetAddress (String input)
  {
    if (input != null && input.equals(""))
      this.Address = null;
    else
      this.Address = input;
  }
  public String GetAddress ()
  {
    return this.Address;
  }

  public void SetSupplAddress (String input)
  {
    if (input != null && input.equals(""))
      this.SupplAddress = null;
    else
      this.SupplAddress = input;
  }
  public String GetSupplAddress ()
  {
    return this.SupplAddress;
  }

  public void SetCity (String input)
  {
    if (input != null && input.equals(""))
      this.City = null;
    else
      this.City = input;
  }
  public String GetCity ()
  {
    return this.City;
  }

  public void SetState (String input)
  {
    if (input != null && input.equals(""))
      this.State = null;
    else
      this.State = input;
  }
  public String GetState ()
  {
    return this.State;
  }

  public void SetZipCode (String input)
  {
    if (input != null && input.equals(""))
      this.ZipCode = null;
    else
      this.ZipCode = input;
  }
  public String GetZipCode ()
  {
    return this.ZipCode;
  }

  public void SetCountry (String input)
  {
    if (input != null && input.equals(""))
      this.Country = null;
    else
      this.Country = input;
  }
  public String GetCountry ()
  {
    return this.Country;
  }

  public void SetComments (String input)
  {
    if (input != null && input.equals(""))
      this.Comments = null;
    else
      this.Comments = input;
  }
  public String GetComments ()
  {
    return this.Comments;
  }

  public void SetStatus (String input)
  {
    if (input != null && input.equals(""))
      this.Status = null;
    else
      this.Status = input;
  }
  public String GetStatus ()
  {
    return this.Status;
  }

  public void SetLast4SSN (String input)
  {
    if (input != null && input.equals(""))
      this.Last4SSN = null;
    else
      this.Last4SSN = input;
  }
  public String GetLast4SSN ()
  {
    return this.Last4SSN;
  }

  public void SetPhone (String input)
  {
    if (input != null && input.equals(""))
      this.Phone = null;
    else
      this.Phone = input;
  }
  public String GetPhone ()
  {
    return this.Phone;
  }

  public void SetAssignedDomains (String[] input)
  {
    if (input != null && input.length > 0) {
      this.AssignedDomains = new HashMap();
      for (int i = 0; i < input.length; i++)
        this.AssignedDomains.put(input[i],input[i]);
      if (this.AssignedDomains.isEmpty())
        this.AssignedDomains = null;
    }
    else
      this.AssignedDomains = null;
  }
  public String[] GetAssignedDomains ()
  {
    String[] retArray = null;
    if (this.AssignedDomains != null) {
      Collection coll = this.AssignedDomains.values();
      if (coll != null && coll.size() > 0) {
        Object[] array = coll.toArray();
        retArray = new String [array.length];
        for (int i = 0; i < array.length; i++)
          retArray[i] = (String)array[i];
      }
    }
    return retArray;
  }
  public int[] GetAssignedDomainIDs (String loggerName) throws Exception
  {
    int[] retArray = null;
    if (this.AssignedDomains != null && !this.AssignedDomains.isEmpty()) {
      Object[] temp = this.AssignedDomains.values().toArray();
      if (temp != null && temp.length > 0) {
        String[] domainNames = new String [temp.length];
        for (int i = 0; i < domainNames.length; i++)
          domainNames[i] = (String)temp[i];
        INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
        Domain[] domains = domainDB.GetDomains(domainNames);
        if (domains != null && domains.length > 0) {
          retArray = new int [domains.length];
          for (int i = 0; i < retArray.length; i++)
            retArray[i] = domains[i].GetDomainID();
        }
      }
    }
    return retArray;
  }
  public void AddAssignedDomain (String domain)
  {
    if (domain != null) {
      if (this.AssignedDomains == null)
        this.AssignedDomains = new HashMap();
      if (!this.AssignedDomains.containsKey(domain))
        this.AssignedDomains.put(domain,domain);
    }
  }

  public void SetAssignedOperations (String[] input)
  {
    if (input != null && input.length > 0) {
      this.AssignedOperations = new HashMap();
      for (int i = 0; i < input.length; i++)
        this.AssignedOperations.put(input[i],input[i]);
    }
    else
      this.AssignedOperations = null;
  }
  public String[] GetAssignedOperations (String loggerName) throws Exception
  {
    String[] retArray = null;
    if (this.IsNAASNodeUser()) {
      NAASIntegration naas = new NAASIntegration(loggerName);
      PolicyInfo[] policies = naas.GetPolicyList(this.LoginName);
      if (policies != null && policies.length > 0) {
        String[] opNames = new String [policies.length];
        String[] wsNames = new String [policies.length];
        for (int i = 0; i < policies.length; i++) {
          opNames[i] = policies[i].getRequest();
          if (opNames[i] != null) opNames[i] = opNames[i].toUpperCase();
          wsNames[i] = policies[i].getMethod();
          if (wsNames[i] != null) wsNames[i] = wsNames[i].toUpperCase();
        }
        INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
        retArray = opDB.GetOperationFullNames(opNames,wsNames);
      }
    }
    else {
      if (this.AssignedOperations != null) {
        Collection coll = this.AssignedOperations.values();
        if (coll != null && coll.size() > 0) {
          Object[] array = coll.toArray();
          retArray = new String[array.length];
          for (int i = 0; i < array.length; i++)
            retArray[i] = (String) array[i];
        }
      }
    }
    return retArray;
  }
  public String[] GetAssignedOperations (String loggerName, String[] adminDomains) throws Exception
  {
    ArrayList list = new ArrayList();
    String[] ops = this.GetAssignedOperations(loggerName);
    if (ops != null)
    {
      for (int i = 0; i < ops.length; i++)
      {
        int index = ops[i].indexOf(":");
        String domainName = ops[i].substring(0, index).trim();
        if (adminDomains != null)
        {
          for (int j = 0; j < adminDomains.length; j++) {
            if (domainName.equalsIgnoreCase(adminDomains[j])) {
              list.add(ops[i]);
              break;
            }
          }
        }
        else
          list.add(ops[i]);
      }
    }
    String[] retList = null;
    if (list.size() > 0)
    {
      retList = new String[list.size()];
      for (int i = 0; i < list.size(); i++)
        retList[i] = (String)list.get(i);
    }
    return retList;
  }
  public int[] GetAssignedOperationIDs (String loggerName) throws Exception
  {
    int[] retArray = null;
    if (this.IsNAASNodeUser()) {
      NAASIntegration naas = new NAASIntegration(loggerName);
      PolicyInfo[] policies = naas.GetPolicyList(this.LoginName);
      if (policies != null && policies.length > 0) {
        String[] opNames = new String [policies.length];
        String[] wsNames = new String [policies.length];
        for (int i = 0; i < policies.length; i++) {
          opNames[i] = policies[i].getRequest();
          if (opNames[i] != null) opNames[i] = opNames[i].toUpperCase();
          wsNames[i] = policies[i].getMethod();
          if (wsNames[i] != null) wsNames[i] = wsNames[i].toUpperCase();
        }
        INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
        retArray = opDB.GetOperationIDs(wsNames,opNames);
      }
    }
    else {
      if (this.AssignedOperations != null) {
        String[] opStrings = null;
        Collection coll = this.AssignedOperations.values();
        if (coll != null && coll.size() > 0) {
          Object[] array = coll.toArray();
          opStrings = new String[array.length];
          for (int i = 0; i < array.length; i++)
            opStrings[i] = (String) array[i];
        }
        if (opStrings != null && opStrings.length > 0) {
          String[] opNames = new String[opStrings.length];
          String[] wsNames = new String[opStrings.length];
          for (int i = 0; i < opStrings.length; i++) {
            String[] temp = this.ConvertOperationFullNames(opStrings[i]);
            if (temp != null && temp.length == 2) {
              wsNames[i] = temp[0];
              opNames[i] = temp[1];
            }
          }
          INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
          retArray = opDB.GetOperationIDs(wsNames,opNames);
        }
      }
    }
    return retArray;
  }
  public void AddAssignedOperation (String operation)
  {
    if (operation != null) {
      if (this.AssignedOperations == null)
        this.AssignedOperations = new HashMap();
      if (!this.AssignedOperations.containsKey(operation))
        this.AssignedOperations.put(operation,operation);
    }
  }

  public void SetAccountType (String input)
  {
    if (input != null && input.equals(""))
      this.AccountType = null;
    else
      this.AccountType = input;
  }
  public String GetAccountType ()
  {
    return this.AccountType;
  }

  public void SetCreatedDate (String input)
  {
    this.CreatedDate = input;
  }
  public String GetCreatedDate ()
  {
    return this.CreatedDate;
  }

  public void SetCreatedBy (String input)
  {
    if (input != null && input.equals(""))
      this.CreatedBy = null;
    else
      this.CreatedBy = input;
  }
  public String GetCreatedBy ()
  {
    return this.CreatedBy;
  }

  public void SetUpdatedDate (String input)
  {
    this.UpdatedDate = input;
  }
  public String GetUpdatedDate ()
  {
    return this.UpdatedDate;
  }

  public void SetUpdatedBy (String input)
  {
    if (input != null && input.equals(""))
      this.UpdatedBy = null;
    else
      this.UpdatedBy = input;
  }
  public String GetUpdatedBy ()
  {
    return this.UpdatedBy;
  }

  /**
   * Is Console User.
   * @return success
   */
  public boolean IsConsoleUser ()
  {
    if (this.AccountType != null && this.AccountType.equals(Phrase.ConsoleUser))
      return true;
    return false;
  }

  /**
   * Is Node Admin.
   * @return success
   */
  public boolean IsNodeAdmin ()
  {
    if (this.AccountType != null && this.IsConsoleUser() && this.AssignedDomains != null && this.AssignedDomains.containsKey(Phrase.NODE_DOMAIN))
      return true;
    return false;
  }

  /**
   * Is Local Node User.
   * @return success
   */
  public boolean IsLocalNodeUser ()
  {
    if (this.AccountType != null && this.AccountType.equals(Phrase.LocalNodeUser))
      return true;
    return false;
  }

  /**
   * Is NAAS Node User.
   * @return success
   */
  public boolean IsNAASNodeUser ()
  {
    if (this.AccountType != null && this.AccountType.equals(Phrase.NAASNodeUser))
      return true;
    return false;
  }

  /**
   * BelongsTo.
   * @param admin.
   * @return success
   */
  public boolean BelongsTo (User admin)
  {
    boolean retBool = false;
    if (admin.IsNodeAdmin())
      retBool = true;
    else if (this.AssignedDomains != null) {
      String[] domains = admin.GetAssignedDomains();
      if (domains != null) {
        for (int i = 0; i < domains.length; i++) {
          if (this.AssignedDomains.containsKey(domains[i])) {
            retBool = true;
            break;
          }
        }
      }
    }
    return retBool;
  }

  /**
   * Save New User.
   * @param loggerName.
   * @param admin.
   * @return success
   */
  public int SaveNewUser (String loggerName, User admin) throws Exception
  {
    int retInt = -1;
    String email = null;
    String password = null;
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    String[] domains = null;
    if (!admin.IsNodeAdmin())
      domains = admin.GetAssignedDomains();
    if (this.AccountType != null && this.AccountType.equals(Phrase.NAASNodeUser)) {  // Update NAAS
      if (this.LoginName != null) {
        NAASIntegration naas = new NAASIntegration(loggerName);
        password = Utility.GeneratePassword();
        email = this.LoginName;
        if (naas.AddNAASUser(this.LoginName,password) && this.SetNAASOperationPriviledges(loggerName, domains))
          retInt = this.SUCCESS;
        else
          retInt = this.NAAS_ERROR;
      }
    }
    else { // Update Database
      String[] adminDomains = null;
      String[] opNames = null;
      String[] wsNames = null;
      if (this.AccountType.equals(Phrase.ConsoleUser))
        adminDomains = this.GetAssignedDomains();
      else {
        String[][] array = this.GetOpWsNames(loggerName);
        if (array != null && array.length == 2) {
          opNames = array[0];
          wsNames = array[1];
        }
      }
      this.UserID = userDB.SaveNewUser(this.LoginName,this.Status,this.FirstName,this.MiddleInitial,this.LastName,
                                       this.EmailAddress,this.Phone,this.Last4SSN,this.Comments,this.Address,this.SupplAddress,
                                       this.City,this.State,this.ZipCode,this.Country,this.AccountType,adminDomains,opNames,
                                       wsNames, domains);
      if (this.UserID >= 0) {
        this.Init(new User(this.UserID,loggerName),loggerName);
        email = this.EmailAddress;
        password = this.LoginPassword;
        retInt = this.SUCCESS;
      }
      else
        retInt = this.DATABASE_ERROR;
    }
    if (retInt == this.SUCCESS) {
      if (!userDB.SendEmail(email,this.FirstName,this.LastName,this.LoginName,password))
        retInt = this.EMAIL_ERROR;
    }
    return retInt;
  }

  /**
   * Save User.
   * @param loggerName.
   * @param admin.
   * @return success
   */
  public int SaveUser (String loggerName, User admin)
  {
    int retInt = -1;
    try {
      String[] domains = null;
      if (!admin.IsNodeAdmin())
        domains = admin.GetAssignedDomains();
      if (this.AccountType != null && !this.AccountType.equals(Phrase.NAASNodeUser)) {  // Update Database
        INodeUser userDB = DBManager.GetNodeUser(loggerName);
        String[] adminDomains = null;
        String[] opNames = null;
        String[] wsNames = null;
        if (this.AccountType.equals(Phrase.ConsoleUser))
          adminDomains = this.GetAssignedDomains();
        else {
          String[][] array = this.GetOpWsNames(loggerName);
          if (array != null && array.length == 2) {
            opNames = array[0];
            wsNames = array[1];
          }
        }
        if (this.UserID >= 0)
        {
          this.UserID = userDB.SaveUser(this.UserID,this.Status,this.FirstName,this.MiddleInitial,this.LastName,
                                        this.EmailAddress,this.Phone,this.Last4SSN,this.Comments,this.Address,this.SupplAddress,
                                        this.City,this.State,this.ZipCode,this.Country,this.AccountType,adminDomains,opNames,
                                        wsNames, domains);
        }
        if (this.UserID >= 0){       	
        	if(userDB.DeleteUserLayout(this.UserID))
        		retInt = this.SUCCESS;
        	else retInt = this.DATABASE_ERROR;
        }
        else
          retInt = this.DATABASE_ERROR;
      }
      else {
        if (this.SetNAASOperationPriviledges(loggerName, domains))
          retInt = this.SUCCESS;
        else
          retInt = this.NAAS_ERROR;
      }
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Save User: " + e.toString(),Level.ERROR,loggerName);
    }
    return retInt;
  }

  /**
   * Search Local and NAAS Users
   * @param loggerName String
   * @param loginName String
   * @param userType String
   * @param assocDomain String
   * @param firstName String
   * @param lastName String
   * @param allNAAS boolean true if all NAAS Users, false if just NAAS with permission on own Node
   * @throws Exception
   * @return User[][] User[2][numOfUsers], User[0] = list of Local Users, User[1] = list of NAAS Users
   */
  public static User[][] SearchUsers (String loggerName, String loginName, String userType, String assocDomain, String firstName, String lastName, boolean allNAAS) throws Exception
  {
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    User[][] retList = new User[2][];
    if (userType == null || !userType.equalsIgnoreCase(Phrase.NAASNodeUser))
      retList[0] =  userDB.SearchUser(loginName,userType,assocDomain,firstName,lastName);
    else
      retList[0] = new User[0];
    if (userType == null || userType.equals("") || userType.equalsIgnoreCase(Phrase.NAASNodeUser)) {
      NAASIntegration naas = new NAASIntegration(loggerName);
      try {
        if (allNAAS)
        	// WI 33506
          retList[1] = User.GetUserList(naas.GetUserList(loginName),loginName);
        else
          retList[1] = User.GetUserList(naas.GetUsersOwned(),naas.GetPolicyList(null),loginName);
      } catch (Exception e) {
        retList[1] = null;
      }
    }
    else
      retList[1] = new User[0];
    return retList;
  }

  /**
   * Get User List.
   * @param users.
   * @param searchLogin.
   * @return user array
   */
  private static User[] GetUserList (UserInfo[] users, String searchLogin)
  {
    User[] retArray = null;
    if (users != null && users.length > 0) {
      TreeMap map = new TreeMap();
      for (int i = 0; i < users.length; i++) {
        String userID = users[i].getUserId();
        if (searchLogin == null || searchLogin.equals(""))
          map.put(userID,new User(userID));
        else if (userID.equalsIgnoreCase(searchLogin))
          map.put(userID,new User(userID));
        else {
          String[] temp = userID.split("@");
          if (temp != null && temp.length > 0) {
            for (int j = 0; j < temp.length; j++) {
              if (temp[j].equalsIgnoreCase(searchLogin)) {
                map.put(userID,new User(userID));
                break;
              }
            }
          }
        }
      }
      if (!map.isEmpty()) {
        Object[] temp = map.values().toArray();
        if (temp != null && temp.length > 0) {
          retArray = new User [temp.length];
          for (int i = 0; i < retArray.length; i++)
            retArray[i] = (User)temp[i];
        }
      }
    }
    if (retArray == null)
      retArray = new User[0];
    return retArray;
  }

  /**
   * Get User List.
   * @param users.
   * @param policies.
   * @param searchLogin.
   * @return user array
   */
  private static User[] GetUserList (UserInfo[] users, PolicyInfo[] policies, String searchLogin)
  {
    User[] retArray = null;
    TreeMap usersMap = new TreeMap();
    if (users != null) {
      for (int i = 0; i < users.length; i++) {
        String userID = users[i].getUserId();
        if (searchLogin == null || searchLogin.equals(""))
          usersMap.put(userID,new User(userID));
        else if (searchLogin.equalsIgnoreCase(userID))
          usersMap.put(userID,new User(userID));
        else {
          String[] temp = userID.split("@");
          if (temp != null) {
            for (int j = 0; j < temp.length; j++) {
              if (temp[j].equalsIgnoreCase(searchLogin)) {
                usersMap.put(userID,new User(userID));
                break;
              }
            }
          }
        }
      }
    }
    if (policies != null) {
      for (int i = 0; i < policies.length; i++) {
        String userID = policies[i].getSubject();
        if (searchLogin == null || searchLogin.equals(""))
          usersMap.put(userID,new User(userID));
        else if (userID.equalsIgnoreCase(searchLogin))
          usersMap.put(userID,new User(userID));
        else {
          String[] temp = userID.split("@");
          if (temp != null && temp.length > 0) {
            for (int j = 0; j < temp.length; j++) {
              if (temp[j].equalsIgnoreCase(searchLogin)) {
                usersMap.put(userID,new User(userID));
                break;
              }
            }
          }
        }
      }
    }
    if (!usersMap.isEmpty()) {
      Object[] temp = usersMap.values().toArray();
      if (temp != null && temp.length > 0) {
        retArray = new User [temp.length];
        for (int i = 0; i < retArray.length; i++)
          retArray[i] = (User)temp[i];
      }
    }
    if (retArray == null)
      retArray = new User [0];
    return retArray;
  }

  /**
   * Get UserID From DB.
   * @param loginName.
   * @param loggerName.
   * @return userID
   */
  public static int GetUserIDFromDB (String loginName, String loggerName) throws Exception
  {
    int retInt = -1;
    if (loginName != null) {
      INodeUser userDB = DBManager.GetNodeUser(loggerName);
      retInt = userDB.GetUserID(loginName);
    }
    return retInt;
  }

  /**
   * Get Console Users.
   * @param loggerName.
   * @return User array
   */
  public static User[] GetConsoleUsers (String loggerName) throws Exception
  {
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    return userDB.GetConsoleUsers();
  }

  /**
   * Get LoginName From DB.
   * @param userID.
   * @param loggerName.
   * @return LoginName
   */
  public static String GetLoginNameFromDB (int userID, String loggerName) throws Exception
  {
    String retString = null;
    if (userID >= 0) {
      INodeUser userDB = DBManager.GetNodeUser(loggerName);
      retString = userDB.GetUserLogin(userID);
    }
    return retString;
  }

  /**
   * Authenticate Login.
   * @param loggerName.
   * @param userName.
   * @param password.
   * @return success
   */
  public static int AuthenticateLogin (String loggerName, String userName, String password) throws Exception
  {
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    return userDB.AuthenticateAnyLogin(userName,password);
  }

  /**
   * Change PWD.
   * @param loggerName.
   * @return success
   */
  public boolean ChangePWD (String loggerName) throws Exception
  {
    boolean retBool = false;
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    String email = null;
    String password = null;
    if (this.AccountType.equals(Phrase.NAASNodeUser)) {
      NAASIntegration naas = new NAASIntegration(loggerName);
      password = Utility.GeneratePassword();
      if (naas.ChangePassword(this.LoginName,password)) {
        email = this.LoginName;
        retBool = true;
      }
    }
    else {
      String newPWD = userDB.ChangePassword(this.LoginName);
      if (newPWD != null) {
        retBool = true;
        this.LoginPassword = newPWD;
        email = this.EmailAddress;
        password = this.LoginPassword;
      }
    }
    retBool = retBool && userDB.SendEmail(email,this.FirstName,this.LastName,this.LoginName,password);
    return retBool;
  }

  /**
   * Change PWD.
   * @param loggerName.
   * @param newPWD.
   * @return success
   */
  public boolean ChangePWD (String loggerName, String newPWD) throws Exception
  {
    boolean retBool = false;
    INodeUser userDB = DBManager.GetNodeUser(loggerName);
    if (this.AccountType.equals(Phrase.NAASNodeUser)) {
      NAASIntegration naas = new NAASIntegration(loggerName);
      if (naas.ChangePassword(this.LoginName,newPWD)) {
        retBool = true;
      }
    }
    else {
      newPWD = userDB.ChangePassword(this.LoginName,newPWD);
      if (newPWD != null) {
        retBool = true;
        this.LoginPassword = newPWD;
      }
    }
    return retBool;
  }

  /**
   * Get Operations Available.
   * @param domain.
   * @param loggerName.
   * @return success
   */
  public String[] GetOperationsAvailable (String domain, String loggerName) throws Exception
  {
    String[] retArray = null;
    if (domain != null) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      String[] operations = opDB.GetOperationFullNames(domain);
      if (operations != null && operations.length > 0) {
        HashMap map = new HashMap();
        for (int i = 0; i < operations.length; i++)
          map.put(operations[i],operations[i]);
        String[] assignedOps = this.GetAssignedOperations(loggerName);
        if (assignedOps != null)
          for (int i = 0; i < assignedOps.length; i++)
            if (map.containsKey(assignedOps[i]))
              map.remove(assignedOps[i]);
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          retArray = new String[temp.length];
          for (int i = 0; i < temp.length; i++)
            retArray[i] = (String)temp[i];
        }
      }
    }
    return retArray;
  }

  /**
   * Get Domains Available.
   * @param domainsAssigned.
   * @param loggerName.
   * @return Domains array
   */
  public String[] GetDomainsAvailable (String[] domainsAssigned, String loggerName) throws Exception
  {
    String[] retArray = null;
    if (domainsAssigned != null) {
      HashMap map = new HashMap();
      if (this.IsNodeAdmin()) {
        INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
        String[] domains = domainDB.GetDomains();
        if (domains != null) {
          for (int i = 0; i < domains.length; i++)
            map.put(domains[i],domains[i]);
        }
      }
      else if (this.AssignedDomains != null)
        map.putAll(this.AssignedDomains);
      for (int i = 0; i < domainsAssigned.length; i++)
        if (map.containsKey(domainsAssigned[i]))
          map.remove(domainsAssigned[i]);
      if (!map.isEmpty()) {
        Collection coll = map.values();
        Object[] temp = coll.toArray();
        if (temp != null && temp.length > 0) {
          retArray = new String[temp.length];
          for (int i = 0; i < temp.length; i++)
            retArray[i] = (String)temp[i];
        }
      }
    }
    else {
      if (this.IsNodeAdmin()) {
        INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
        retArray = domainDB.GetDomains();
      }
      else
        retArray = this.GetAssignedDomains();
    }
    return retArray;
  }

  /**
   * Get Domains Assigned.
   * @param admin.
   * @return Domains name
   */
  public String[] GetDomainsAssigned (User admin)
  {
    String[] retArray = null;
    if (this.AssignedDomains != null && admin != null) {
      HashMap retMap = new HashMap();
      if (admin.IsNodeAdmin()) {
        retMap.putAll(this.AssignedDomains);
      }
      else {
        String[] adminDomains = admin.GetAssignedDomains();
        if (adminDomains != null) {
          for (int i = 0; i < adminDomains.length; i++)
            if (this.AssignedDomains.containsKey(adminDomains[i]))
              retMap.put(adminDomains[i], adminDomains[i]);
        }
      }
      if (!retMap.isEmpty()) {
        Object[] temp = retMap.values().toArray();
        retArray = new String [temp.length];
        for (int i = 0; i < retArray.length; i++)
          retArray[i] = (String)temp[i];
      }
    }
    return retArray;
  }

  /**
   * Get Console Users Available.
   * @param loggerName.
   * @return Users name
   */
  public String[] GetConsoleUsersAvailable (String loggerName) throws Exception
  {
    INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
    if (this.IsNodeAdmin())
      return domainDB.GetAdmins(domainDB.GetDomains());
    else
      return domainDB.GetAdmins(this.GetAssignedDomains());
  }

  /**
   * Get Op Ws Names.
   * @param loggerName.
   * @return name
   */
  private String[][] GetOpWsNames (String loggerName) throws Exception
  {
    String[][] retArray = null;
    String[] fullOps = this.GetAssignedOperations(loggerName);
    if (fullOps != null && fullOps.length > 0) {
      retArray = new String[2][];
      retArray[0] = new String[fullOps.length];
      retArray[1] = new String[fullOps.length];
      for (int i = 0; i < fullOps.length; i++) {
        String op = fullOps[i];
        int index = op.indexOf(": ");
        if (index >= 0) {
          index += 2;
          if (index < op.length()) {
            String temp = op.substring(index);
            index = temp.indexOf(".");
            if (index >= 0) {
              if (index+1 < temp.length()) {
                retArray[0][i] = temp.substring(index+1);
                retArray[1][i] = temp.substring(0,index);
              }
            }
          }
        }
      }
    }
    return retArray;
  }

  /**
   * Set NAAS Operation Priviledges.
   * @param loggerName.
   * @param adminDomains.
   * @return success
   */
  private boolean SetNAASOperationPriviledges (String loggerName, String[] adminDomains) throws Exception
  {
    NAASIntegration naas = new NAASIntegration(loggerName);
    HashMap map = new HashMap();
    if (this.AssignedOperations != null)
    {
      Collection collection = this.AssignedOperations.values();
      Iterator iterator = collection.iterator();
      while (iterator.hasNext())
      {
        String opFullString = (String)iterator.next();
        String[] parsed = Utility.ParseOperationString(opFullString);
        if (adminDomains != null)
        {
          int index = opFullString.indexOf(":");
          String domainName = opFullString.substring(0, index);
          for (int i = 0; i < adminDomains.length; i++)
          {
            if (domainName.equalsIgnoreCase(adminDomains[i]))
            {
              map.put(parsed[0].toLowerCase()+"."+parsed[1].toLowerCase(), opFullString);
              break;
            }
          }
        }
        else
          map.put(parsed[0].toLowerCase()+"."+parsed[1].toLowerCase(), opFullString);
      }
    }
    PolicyInfo[] ops = naas.GetPolicyList(this.LoginName);
    if (ops != null && ops.length > 0) {
      for (int i = 0; i < ops.length; i++) {
        if (adminDomains != null)
        {
          INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
          String domain = domainDB.GetDomainName(ops[i].getRequest(), ops[i].getMethod());
          for (int j = 0; j < adminDomains.length; j++)
          {
            if (domain != null && domain.trim().equalsIgnoreCase(adminDomains[j]))
            {
              if (map.containsKey(ops[i].getMethod()+"."+ops[i].getRequest()))
                map.remove(ops[i].getRequest());
              else
                naas.SetPolicy(this.LoginName,ops[i].getMethod(),ops[i].getRequest(),false);
            }
          }
        }
        else
        {
            if (map.containsKey(ops[i].getMethod()+"."+ops[i].getRequest()))
              map.remove(ops[i].getRequest());
            else
              naas.SetPolicy(this.LoginName,ops[i].getMethod(),ops[i].getRequest(),false);
        }
      }
    }
    if (!map.isEmpty()) {
      Iterator iter = map.values().iterator();
      while (iter.hasNext()) {
        String opName = (String)iter.next();
        String[] parsed = Utility.ParseOperationString(opName);
        if (parsed != null && parsed.length == 2)
          naas.SetPolicy(this.LoginName,parsed[0],parsed[1],true);
      }
    }
    return true;
  }

  /**
   * Add NAAS User.
   * @param loggerName.
   * @return success
   */
  private boolean AddNAASUser (String loggerName)
  {
    NAASIntegration naas = new NAASIntegration(loggerName);
    return naas.AddNAASUser(this.LoginName,this.LoginPassword);
  }

  /**
   * Split Operation Full Name into return[0] = wsName and return[1] = opName
   * @param operation String
   * @return String[]
   */
  private String[] ConvertOperationFullNames (String operation)
  {
    String[] retArray = null;
    if (operation != null) {
      String[] splitDomain = operation.split(":");
      if (splitDomain != null && splitDomain.length == 2 && splitDomain[1] != null) {
        String[] split = splitDomain[1].split("\\.",2);
        if (split != null && split.length == 2 && split[0] != null && split[1] != null) {
          retArray = new String[2];
          retArray[0] = split[0].trim();
          retArray[1] = split[1].trim();
        }
      }
    }
    return retArray;
  }
}
