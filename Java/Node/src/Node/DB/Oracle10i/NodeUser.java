package Node.DB.Oracle10i;

import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;

import Node.Phrase;
import Node.API.Email;
import Node.Biz.Administration.User;
import Node.DB.Interfaces.INodeUser;
import Node.DB.Oracle.Configuration.SystemConfiguration;
import Node.Utils.AppUtils;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.utility.Utility;
import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create NodeUser.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeUser extends NodeDB implements INodeUser {
  // Table Name
  private String TableName = this.SysUserInfoTableName;

  // Column Names
  private String UserID = this.SysUserInfoUserID;
  private String FirstName = this.SysUserInfoFirstName;
  private String LastName = this.SysUserInfoLastName;
  private String MidInit = this.SysUserInfoMiddleInitial;
  private String LoginName = this.SysUserInfoLoginName;
  private String LoginPassword = this.SysUserInfoLoginPassword;
  private String UserStatusCD = this.SysUserInfoUserStatusCD;
  private String Last4SSN = this.SysUserInfoLast4SSN;
  private String ChangePWDFlag = "CHANGE_PWD_FLAG";
  private String Phone = this.SysUserInfoPhone;
  private String Comments = this.SysUserInfoComments;

  /**
   * Constructor
   * @param loggerName
   * @return 
   * @deprecated
   */
  public NodeUser(String loggerName) {
    super(loggerName);
  }

  /**
   * ValidateLogin
   * @param login
   * @param password
   * @return int
   * @deprecated
   */
  public int ValidateLogin (String login, String password)
  {
    int retInt = this.USER_DOES_NOT_EXIST;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.LoginPassword,"A."+this.UserStatusCD,"A."+this.ChangePWDFlag,"C."+this.NodeAccountType };
      String tableNames = this.TableName+" A";
      tableNames += " left join "+this.NodeAccountTypeXREFTableName+" B on A."+this.UserID+" = B."+this.UserID;
      tableNames += " left join "+this.NodeAccountTypeTableName+" C on B."+this.NodeAccountTypeID+" = C."+this.NodeAccountTypeID;
      String condition = "A."+this.LoginName+" = '"+login+"'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        if (rs.first()) {
          Cryptography crypt = new Cryptography();
          String pwd = crypt.Decrypting(rs.getString(this.LoginPassword),Phrase.CryptKey);
          if (password != null && password.equals(pwd)) {
            String status = rs.getString(this.UserStatusCD);
            if (status != null && status.equalsIgnoreCase("A")) {
              String accType = rs.getString(this.NodeAccountType);
              if (accType != null && accType.equalsIgnoreCase(Phrase.ConsoleUser)) {
                String changePWDFlag = rs.getString(this.ChangePWDFlag);
                if (changePWDFlag != null && changePWDFlag.equalsIgnoreCase("Y"))
                  retInt = this.CHANGE_PWD;
                else
                  retInt = this.SUCCESSFUL;
              }
              else
                retInt = this.INVALID_PERMISSION;
            }
            else
              retInt = this.INACTIVE_USER;
          }
          else
            retInt = this.INVALID_PASSWORD;
        }
        else
          retInt = this.USER_DOES_NOT_EXIST;
      }
      else
        retInt = this.DATABASE_UNAVAILABLE;
    } catch (Exception e) {
      this.LogException("Could Not Validate Console Login: " + e.toString());
      retInt = this.DATABASE_UNAVAILABLE;
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

/*
  public int ValidateLogin (String login, String password)
  {
    int retInt = this.USER_DOES_NOT_EXIST;
    User user = this.GetUser(login);
    if (user != null && user.GetUserID() >= 0) {
      if (user.GetStatus().equals("A")) {
        if (user.IsConsoleUser()) {
          try {
            if (user.GetLoginPassword().equals(password)) {
              if (user.GetChangePWD())
                retInt = this.CHANGE_PWD;
              else
                retInt = this.SUCCESSFUL;
            }
            else
              retInt = this.INVALID_PASSWORD;
          } catch (Exception e) {
            retInt = this.INVALID_PASSWORD;
          }
        }
        else
          retInt = this.INVALID_PERMISSION;
      }
      else
        retInt = this.INACTIVE_USER;
    }
    //else if (user == null)
    //  retInt = this.DATABASE_UNAVAILABLE;
    return retInt;
  }
*/
  /**
   * AuthenticateLogin
   * @param user
   * @param pwd
   * @return int
   * @deprecated
   */
  public int AuthenticateLogin (String user, String pwd)
  {
    int retInt = this.USER_DOES_NOT_EXIST;
    if (user == null || pwd == null || pwd.equals(""))
      return retInt;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.UserStatusCD,"A."+this.LoginPassword,"B."+this.NodeAccountType };
      String tableNames = this.TableName+" A,"+this.NodeAccountTypeTableName+" B,"+this.NodeAccountTypeXREFTableName+" C";
      String condition = "A."+this.LoginName+" = '"+user+"'";
      condition += " and A."+this.UserID+" = C."+this.UserID+" and B."+this.NodeAccountTypeID+" = C."+this.NodeAccountTypeID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        String encrypted = rs.getString(this.LoginPassword);
        Cryptography crypt = new Cryptography();
        String password = crypt.Encrypting(pwd,Phrase.CryptKey);
        if (password.equals(encrypted)) {
          String status = rs.getString(this.UserStatusCD);
          if (status.equals("A")) {
            String accType = rs.getString(this.NodeAccountType);
            if (accType != null && accType.equalsIgnoreCase(Phrase.LocalNodeUser))
              retInt = this.SUCCESSFUL;
            else
              retInt = this.INVALID_PERMISSION;
          }
          else
            retInt = this.INACTIVE_USER;
        }
        else
          retInt = this.INVALID_PASSWORD;
      }
    } catch (Exception e) {
      this.LogException("Could Not Locally Authenticate User: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

  /**
   * AuthenticateAnyLogin
   * @param user
   * @param pwd
   * @return int
   * @deprecated
   */
  public int AuthenticateAnyLogin (String user, String pwd)
  {
    int retInt = this.USER_DOES_NOT_EXIST;
    if (user == null || pwd == null || pwd.equals(""))
      return retInt;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { this.UserStatusCD,this.LoginPassword };
      String tableNames = this.TableName;
      String condition = this.LoginName+" = '"+user+"'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        String encrypted = rs.getString(this.LoginPassword);
        Cryptography crypt = new Cryptography();
        String password = crypt.Encrypting(pwd,Phrase.CryptKey);
        if (password.equals(encrypted)) {
          String status = rs.getString(this.UserStatusCD);
          if (status.equals("A"))
            retInt = this.SUCCESSFUL;
          else
            retInt = this.INACTIVE_USER;
        }
        else
          retInt = this.INVALID_PASSWORD;
      }
    } catch (Exception e) {
      this.LogException("Could Not Locally Authenticate User: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

  /**
   * GetUserID
   * @param loginName
   * @return int
   * @deprecated
   */
  public int GetUserID (String loginName)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    int retInt = -1;
    try {
      String sql = this.GetSelectStr(new String[]{this.UserID},this.TableName,new String[]{this.LoginName},new String[]{loginName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.UserID);
    } catch (Exception e) {
      this.LogException("Could Not Get UserID: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

  /**
   * GetUserLogin
   * @param userID
   * @return String
   * @deprecated
   */
  public String GetUserLogin (int userID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.LoginName},this.TableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.LoginName);
    } catch (Exception e) {
      this.LogException("Could Not Get LoginName: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retString;
  }

  /**
   * GetUser
   * @param userID
   * @return User
   * @deprecated
   */
  public User GetUser (int userID)
  {
    return this.GetUserByIDOrLogin(userID,null);
  }

  /**
   * GetUser
   * @param loginName
   * @return User
   * @deprecated
   */
  public User GetUser (String loginName)
  {
    return this.GetUserByIDOrLogin(-1,loginName);
  }

  /**
   * GetUserByIDOrLogin
   * @param userID
   * @param login
   * @return User
   * @deprecated
   */
  private User GetUserByIDOrLogin (int userID, String login)
  {
    User user = null;
    if (userID < 0 && login == null)
      return null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String [] {
          "A."+this.SysUserInfoLastName,"A."+this.SysUserInfoFirstName,"A."+this.SysUserInfoMiddleInitial,"A."+this.ChangePWDFlag,
          "A."+this.SysUserInfoLoginName,"A."+this.SysUserInfoLoginPassword,"A."+this.SysUserInfoUserStatusCD,
          "A."+this.SysUserInfoLast4SSN,"A."+this.SysUserInfoPhone,"A."+this.SysUserInfoComments,"B."+this.SysAddress,
          "B."+this.SysAddressSuppl,"B."+this.SysAddressCity,"B."+this.SysAddressState,"B."+this.SysAddressZipCode,
          "B."+this.SysAddressCountry,"C."+this.SysEmail,"F."+this.NodeAccountType,"H."+this.NodeDomain,"A."+this.UserID
      };
      String tableNames = this.SysUserInfoTableName + " A, " + this.SysAddressTableName + " B, " + this.SysEmailTableName + " C, ";
      tableNames += this.SysUserAddressTableName + " D, " + this.SysUserEmailTableName + " E, " + this.NodeAccountTypeTableName + " F, ";
      tableNames += this.NodeAccountTypeXREFTableName + " G left join " + this.NodeDomainTableName + " H on G.";
      tableNames += this.NodeDomainID+" = H."+this.NodeDomainID;
      String condition = "";
      if (userID < 0 && login != null)
        condition = "A."+this.LoginName+" = '"+login+"'";
      else
        condition = "A."+this.UserID+" = "+userID;
      condition += " AND A."+this.SysUserInfoUserID+" = D."+this.SysUserInfoUserID;
      condition += " AND D."+this.SysAddressID+" = B."+this.SysAddressID+" AND A."+this.SysUserInfoUserID+" = E.";
      condition += this.SysUserInfoUserID+" AND E."+this.SysEmailID+" = C."+this.SysEmailID;
      condition += " AND A."+this.SysUserInfoUserID+" = G."+this.SysUserInfoUserID;
      condition += " AND G."+this.NodeAccountTypeID+" = F."+this.NodeAccountTypeID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.beforeFirst();
        HashMap map = new HashMap();
        while (rs.next()) {
          int id = rs.getInt(this.UserID);
          if (!map.containsKey(id+"")) {
            User temp = new User(id);
            temp.SetLastName(rs.getString(this.SysUserInfoLastName));
            temp.SetFirstName(rs.getString(this.SysUserInfoFirstName));
            temp.SetMiddleInitial(rs.getString(this.SysUserInfoMiddleInitial));
            temp.SetLoginName(rs.getString(this.SysUserInfoLoginName));
            temp.SetLoginPassword(rs.getString(this.SysUserInfoLoginPassword));
            String changePWD = rs.getString(this.ChangePWDFlag);
            if (changePWD != null && changePWD.equals("N"))
              temp.SetChangePWD(false);
            else
              temp.SetChangePWD(true);
            temp.SetStatus(rs.getString(this.SysUserInfoUserStatusCD));
            temp.SetLast4SSN(rs.getString(this.SysUserInfoLast4SSN));
            temp.SetPhone(rs.getString(this.SysUserInfoPhone));
            temp.SetComments(rs.getString(this.SysUserInfoComments));
            temp.SetAddress(rs.getString(this.SysAddress));
            temp.SetSupplAddress(rs.getString(this.SysAddressSuppl));
            temp.SetCity(rs.getString(this.SysAddressCity));
            temp.SetState(rs.getString(this.SysAddressState));
            temp.SetZipCode(rs.getString(this.SysAddressZipCode));
            temp.SetCountry(rs.getString(this.SysAddressCountry));
            temp.SetEmailAddress(rs.getString(this.SysEmail));
            temp.SetAccountType(rs.getString(this.NodeAccountType));
            String domain = rs.getString(this.NodeDomain);
            if (domain != null)
              temp.SetAssignedDomains(new String[] {domain});
            else
              temp.SetAssignedDomains(null);
            map.put(id+"",temp);
          }
          else {
            User temp = (User)map.get(id+"");
            String domain = rs.getString(this.NodeDomain);
            if (domain != null)
              temp.AddAssignedDomain(domain);
          }
        }
        if (!map.isEmpty()) {
          Iterator iter = map.values().iterator();
          if (iter.hasNext())
            user = (User) iter.next();
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get User: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return user;
  }

  /**
   * SearchUser
   * @param loginName
   * @param userType
   * @param assocDomain
   * @param firstName
   * @param lastName
   * @return User[]
   * @deprecated
   */
  public User[] SearchUser (String loginName, String userType, String assocDomain, String firstName, String lastName)
  {
    User[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A."+this.UserID+",A."+this.LoginName+",A."+this.FirstName+",A."+this.MidInit+",A."+this.LastName;
      sql += ",A."+this.UserStatusCD+",A."+this.CreatedDate+",C."+this.SysEmail+",E."+this.NodeAccountType;
      sql += " from "+this.SysUserInfoTableName+" A,"+this.SysUserEmailTableName+" B,"+this.SysEmailTableName;
      sql += " C,"+this.NodeAccountTypeXREFTableName+" D";
      sql += " left join "+this.NodeDomainTableName+" F on D."+this.NodeDomainID+" = F."+this.NodeDomainID;
      sql += ","+this.NodeAccountTypeTableName+" E";
      sql += " where A."+this.SysUserInfoUserID+" = B."+this.SysUserInfoUserID;
      sql += " and B."+this.SysEmailID+" = C."+this.SysEmailID;
      sql += " and A."+this.SysUserInfoUserID+" = D."+this.SysUserInfoUserID;
      sql += " and D."+this.NodeAccountTypeID+" = E."+this.NodeAccountTypeID;
      if (loginName != null && !loginName.equals(""))
        sql += " and upper(A."+this.SysUserInfoLoginName+") like upper('%"+loginName+"%')";
      if (userType != null && !userType.equals("")) {
        if (userType.equals(Phrase.LocalNodeUser))
          sql += " and E."+this.NodeAccountType+" = '"+Phrase.LocalNodeUser+"'";
        else
          sql += " and E."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
      }
      if (assocDomain != null && !assocDomain.equals(""))
        sql += " and F."+this.NodeDomain+" = '"+assocDomain+"'";
      if (firstName != null && !firstName.equals(""))
        sql += " and upper(A."+this.SysUserInfoFirstName+") like upper('%"+firstName+"%')";
      if (lastName != null && !lastName.equals(""))
        sql += " and upper(A."+this.SysUserInfoLastName+") like upper('%"+lastName+"%')";
      sql += " order by E."+this.NodeAccountType+", A."+this.LoginName;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        rs.beforeFirst();
        HashMap map = new HashMap();
        ArrayList list = new ArrayList();
        while (rs.next()) {
          int userID = rs.getInt(this.SysUserInfoUserID);
          if (!map.containsKey(userID + "")) {
            User temp = new User(userID);
            temp.SetAccountType(rs.getString(this.NodeAccountType));
            temp.SetCreatedDate(rs.getString(this.CreatedDate));
            temp.SetEmailAddress(rs.getString(this.SysEmail));
            temp.SetFirstName(rs.getString(this.SysUserInfoFirstName));
            temp.SetLastName(rs.getString(this.SysUserInfoLastName));
            temp.SetLoginName(rs.getString(this.SysUserInfoLoginName));
            temp.SetMiddleInitial(rs.getString(this.SysUserInfoMiddleInitial));
            temp.SetStatus(rs.getString(this.SysUserInfoUserStatusCD));
            map.put(userID+"",temp);
            list.add(temp);
          }
        }
        if (list.size() > 0) {
          retArray = new User [list.size()];
          for (int i = 0; i < list.size(); i++)
            retArray[i] = (User)list.get(i);
        }
        /*
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            TreeMap tMap = new TreeMap();
            for (int i = 0; i < temp.length; i++) {
              User user = (User)temp[i];
              if (user != null)
                tMap.put(user.GetLoginName(),user);
            }
            if (!tMap.isEmpty()) {
              temp = tMap.values().toArray();
              if (temp != null && temp.length > 0) {
                retArray = new User [temp.length];
                for (int i = 0; i < temp.length; i++)
                  retArray[i] = (User)temp[i];
              }
            }
          }
        }*/
      }
    } catch (Exception e) {
      this.LogException("Could Not Search User: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

  /**
   * SaveUser
   * @param userID
   * @param status
   * @param firstName
   * @param midInit
   * @param lastName
   * @param email
   * @param phone
   * @param last4SSN
   * @param comments
   * @param address
   * @param suppAddress
   * @param city
   * @param state
   * @param zipCode
   * @param country
   * @param accType
   * @param domains
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return int
   * @deprecated
   */
  public int SaveUser (int userID, String status,  String firstName, String midInit, String lastName,
                             String email, String phone, String last4SSN, String comments, String address, String suppAddress,
                             String city, String state, String zipCode, String country, String accType, String[] domains,
                             String[] opNames, String[] wsNames,String[] adminDomains)
  {
    int retInt = -1;
    NodeUser userDB = new NodeUser(this.LoggerName);
    int temp = userDB.SaveUser(userID,email,null,status,firstName,midInit,lastName,phone,last4SSN,comments);
    boolean isUpdated = false;
    if (temp >= 0 && temp == userID) {
      isUpdated = this.SaveEmail(temp, email);
      isUpdated = isUpdated && this.SaveAddress(userID, address, suppAddress, city, state,zipCode, country);
      isUpdated = isUpdated && this.SaveDomainAdmins(temp,accType,domains);
      isUpdated = isUpdated && this.SaveOperations(temp,accType,opNames,wsNames,adminDomains);
    }
    if (isUpdated)
      retInt = temp;
    return retInt;
  }

  /**
   * SaveNewUser
   * @param loginName
   * @param status
   * @param firstName
   * @param midInit
   * @param lastName
   * @param email
   * @param phone
   * @param last4SSN
   * @param comments
   * @param address
   * @param suppAddress
   * @param city
   * @param state
   * @param zipCode
   * @param country
   * @param accType
   * @param domains
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return int
   * @deprecated
   */
  public int SaveNewUser (String loginName, String status,  String firstName, String midInit, String lastName,
                       String email, String phone, String last4SSN, String comments, String address, String suppAddress,
                       String city, String state, String zipCode, String country, String accType, String[] domains,
                       String[] opNames, String[] wsNames, String[] adminDomains)
  {
    int retInt = -1;
    NodeUser userDB = new NodeUser(this.LoggerName);
    int userID = userDB.SaveUser(-1,email,loginName,status,firstName,midInit,lastName,phone,last4SSN,comments);
    boolean isUpdated = false;
    if (userID >= 0) {
      isUpdated = this.SaveEmail(userID, email);
      isUpdated = isUpdated && this.SaveAddress(userID, address, suppAddress, city, state,zipCode, country);
      isUpdated = isUpdated && this.SaveDomainAdmins(userID,accType,domains);
      isUpdated = isUpdated && this.SaveOperations(userID,accType,opNames,wsNames,adminDomains);
      if (!isUpdated)
        this.DeleteUser(userID);
    }
    if (isUpdated)
      retInt = userID;
    return retInt;
  }


  /**
   * SaveEmail
   * @param userID
   * @param emailAddress
   * @return int
   * @deprecated
   */
  private boolean SaveEmail (int userID, String emailAddress)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A." + this.SysEmailID + " from " + this.SysEmailTableName + " A, " + this.SysUserEmailTableName + " B";
      sql += " where B." + this.SysUserInfoUserID + " = " + userID;
      sql += " and B." + this.SysEmailID + " = A." + this.SysEmailID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs == null || !rs.first()) {
        if (rs != null)
          rs.close();
        sql = "insert into " + this.SysEmailTableName + " (" + this.SysEmailID + ", " + this.SysEmail + ", " + this.CreatedDate;
        sql += ", " + this.CreatedBy + ", " + this.UpdatedDate + ", " + this.UpdatedBy + ")";
        int newEmailID = this.GetIncrementID(this.SysEmailTableName,this.SysEmailID);
        if (emailAddress != null)
          sql += " values (" + newEmailID + ", '" + emailAddress + "'";
        else
          sql += " values (" + newEmailID + ", null";
        sql += ", sysdate,'system',sysdate,'system')";
        rs = db.GetResultSet(sql);
        sql = "insert into " + this.SysUserEmailTableName + " (" + this.SysUserInfoUserID + ", " + this.SysEmailID + ")";
        sql += " values (" + userID + ", " + newEmailID + ")";
        if (rs != null)
          rs.close();
        rs = db.GetResultSet(sql);
        retBool = true;
      }
      else if (rs != null && rs.first()) {
        sql = "update " + this.SysEmailTableName + " set " + this.SysEmail + " = ";
        if (emailAddress != null)
          sql += "'" + emailAddress + "'";
        else
          sql += "null";
        sql += ", " + this.UpdatedDate + " = sysdate, " + this.UpdatedBy + " = 'system'";
        sql += " where " + this.SysEmailID + " = " + rs.getInt(this.SysEmailID);
        rs.close();
        rs = db.GetResultSet(sql);
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Email: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * DeleteEmail
   * @param userID
   * @return boolean
   * @deprecated
   */
  private boolean DeleteEmail (int userID)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.SysEmailID},this.SysUserEmailTableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int emailID = rs.getInt(this.SysEmailID);
        rs.deleteRow();
        if (emailID >= 0) {
          sql = "delete from "+this.SysEmailTableName+" where "+this.SysEmailID+" = "+emailID;
          rs.close();
          rs = db.GetResultSet(sql);
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Delete Email: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * SaveAddress
   * @param userID
   * @param address
   * @param suppl
   * @param city
   * @param state
   * @param zipCode
   * @param country
   * @return boolean
   * @deprecated
   */
  private boolean SaveAddress (int userID, String address, String suppl, String city, String state, String zipCode, String country)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select " + this.SysAddressID + " from " + this.SysUserAddressTableName;
      sql += " where " + this.SysUserInfoUserID + " = " + userID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs == null || !rs.first()) {
        sql = "insert into " + this.SysAddressTableName + " (" + this.SysAddressID + ", " + this.SysAddress + ", ";
        sql += this.SysAddressSuppl + ", " + this.SysAddressCity + ", " + this.SysAddressState + ", " + this.SysAddressZipCode;
        sql += ", " + this.SysAddressCountry + ", " + this.CreatedDate + ", " + this.CreatedBy + ", " + this.UpdatedDate;
        sql += ", " + this.UpdatedBy + ")";
        int newAddressID = this.GetIncrementID(this.SysAddressTableName,this.SysAddressID);
        sql += " values (" + newAddressID + ", ";
        if (address != null)
          sql += "'" + address + "',";
        else
          sql += "null,";
        if (suppl != null)
          sql += "'" + suppl + "',";
        else
          sql += "null,";
        if (city != null)
          sql += "'" + city + "',";
        else
          sql += "null,";
        if (state != null)
          sql += "'" +state + "',";
        else
          sql += "null,";
        if (zipCode != null)
          sql += "'" + zipCode + "',";
        else
          sql += "null,";
        if (country != null)
          sql += "'" + country + "',";
        else
          sql += "null,";
        sql += "sysdate,'system',sysdate,'system')";
        if (rs != null)
          rs.close();
        rs = db.GetResultSet(sql);
        sql = "insert into " + this.SysUserAddressTableName + " (" + this.SysUserInfoUserID + ", " + this.SysAddressID + ")";
        sql += " values (" + userID + ", " + newAddressID + ")";
        if (rs != null)
          rs.close();
        rs = db.GetResultSet(sql);
        retBool = true;
      }
      else if (rs != null) {
        sql = "update " + this.SysAddressTableName + " set " + this.SysAddress + " = ";
        if (address != null)
          sql += "'" + address + "',";
        else
          sql += "null,";
        sql += this.SysAddressSuppl + " = ";
        if (suppl != null)
          sql += "'" + suppl + "',";
        else
          sql += "null,";
        sql += this.SysAddressCity + " = ";
        if (city != null)
          sql += "'" + city + "',";
        else
          sql += "null,";
        sql += this.SysAddressState + " = ";
        if (state != null)
          sql += "'" + state + "',";
        else
          sql += "null,";
        sql += this.SysAddressZipCode + " = ";
        if (zipCode != null)
          sql += "'" + zipCode + "',";
        else
          sql += "null,";
        sql += this.SysAddressCountry + " = ";
        if (country != null)
          sql += "'" + country + "',";
        else
          sql += "null,";
        sql += this.UpdatedDate + " = sysdate, " + this.UpdatedBy + " = 'system'";
        sql += " where " + this.SysAddressID + " = " + rs.getInt(this.SysAddressID);
        rs.close();
        rs = db.GetResultSet(sql);
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Address: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * DeleteAddress
   * @param userID
   * @return boolean
   * @deprecated
   */
  private boolean DeleteAddress (int userID)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.SysAddressID},this.SysUserAddressTableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int addressID = rs.getInt(this.SysAddressID);
        rs.deleteRow();
        if (addressID >= 0) {
          sql = "delete from "+this.SysAddressTableName+" where "+this.SysAddressID+" = "+addressID;
          rs.close();
          rs = db.GetResultSet(sql);
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Delete Address: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * SaveDomainAdmins
   * @param userID
   * @param accType
   * @param adminDomains
   * @return boolean
   * @deprecated
   */
  public boolean SaveDomainAdmins (int userID, String accType, String[] adminDomains)
  {
    boolean retBool = false;
    if (accType.equals(Phrase.ConsoleUser)) {
      if (adminDomains != null && adminDomains.length > 0) {
        int[] accTypes = new int[adminDomains.length];
        int[] domainIDs = new int[adminDomains.length];
        NodeAccountType accTypeDB = new NodeAccountType(this.LoggerName);
        int consoleUserID = accTypeDB.GetAccountTypeID(Phrase.ConsoleUser);
        NodeDomain domainDB = new NodeDomain(this.LoggerName);
        for (int i = 0; i < adminDomains.length; i++) {
          accTypes[i] = consoleUserID;
          domainIDs[i] = domainDB.GetDomainID(adminDomains[i]);
        }
        NodeAccountTypeXREF xrefDB = new NodeAccountTypeXREF(this.LoggerName);
        retBool = xrefDB.SaveAccountTypes(userID, accTypes, domainIDs);
      }
      else {
        NodeAccountType accTypeDB = new NodeAccountType(this.LoggerName);
        NodeAccountTypeXREF xrefDB = new NodeAccountTypeXREF(this.LoggerName);
        retBool = xrefDB.SaveAccountType(userID,accTypeDB.GetAccountTypeID(Phrase.ConsoleUser));
      }
    }
    else
      retBool = true;
    return retBool;
  }

  /**
   * SaveOperations
   * @param userID
   * @param accType
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return boolean
   * @deprecated
   */
  public boolean SaveOperations (int userID, String accType, String[] opNames, String[] wsNames, String[] adminDomains)
  {
    boolean retBool = false;

    // Set Account Type to Local Node User and Domain to null
    if (accType.equals(Phrase.LocalNodeUser)) {
      NodeAccountTypeXREF xrefDB = new NodeAccountTypeXREF(this.LoggerName);
      NodeAccountType accTypeDB = new NodeAccountType(this.LoggerName);
      retBool = xrefDB.SaveAccountType(userID, accTypeDB.GetAccountTypeID(accType));
      if (opNames != null && opNames.length > 0 && wsNames != null && wsNames.length == opNames.length) {
        // Save Operation Priviledges
        NodeUserOperation userOpXREFDB = new NodeUserOperation(this.LoggerName);
        retBool = retBool && userOpXREFDB.SetOperationPriviledge(userID,opNames,wsNames,adminDomains);
      }
    }
    else
      retBool = true;
    return retBool;
  }

  /**
   * SaveUser
   * @param userID
   * @param email
   * @param loginName
   * @param status
   * @param firstName
   * @param midInit
   * @param lastName
   * @param phone
   * @param last4SSN
   * @param comments
   * @return int
   * @deprecated
   */
  private int SaveUser (int userID, String email, String loginName, String status, String firstName,
                       String midInit, String lastName, String phone, String last4SSN, String comments)
  {
    int retInt = -1;
    if (userID >= 0)
      retInt = this.UpdateUser(userID, email, status, firstName, midInit, lastName, phone,last4SSN, comments);
    else
      retInt = this.InsertUser(email,loginName,status,firstName,midInit,lastName,phone,last4SSN,comments);
    return retInt;
  }

  /**
   * SavePassword
   * @param loginName
   * @param password
   * @return int
   * @deprecated
   */
  public int SavePassword (String loginName, String password)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.UserID,this.LoginPassword,this.ChangePWDFlag},this.TableName,new String[]{this.LoginName},new String[]{loginName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int temp = rs.getInt(this.UserID);
        Cryptography crypt = new Cryptography();
        rs.updateString(this.LoginPassword,crypt.Encrypting(password,Phrase.CryptKey));
        rs.updateString(this.ChangePWDFlag,"N");
        rs.updateRow();
        retInt = temp;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Password: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

  /**
   * ChangePassword
   * @param loginName
   * @return String
   * @deprecated
   */
  public String ChangePassword (String loginName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.LoginPassword,this.ChangePWDFlag},this.TableName,new String[]{this.LoginName},new String[]{loginName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String password = Node.Utils.Utility.GeneratePassword();
        Cryptography crypt = new Cryptography();
        rs.updateString(this.LoginPassword,crypt.Encrypting(password,Phrase.CryptKey));
        rs.updateString(this.ChangePWDFlag,"Y");
        rs.updateRow();
        retString = password;
      }
    } catch (Exception e) {
      this.LogException("Could Not Change Password: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retString;
  }

  /**
   * ChangePassword
   * @param loginName
   * @param password
   * @return String
   * @deprecated
   */
  public String ChangePassword (String loginName, String password)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.LoginPassword,this.ChangePWDFlag},this.TableName,new String[]{this.LoginName},new String[]{loginName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        Cryptography crypt = new Cryptography();
        rs.updateString(this.LoginPassword,crypt.Encrypting(password,Phrase.CryptKey));
        rs.updateString(this.ChangePWDFlag,"N");
        rs.updateRow();
        retString = password;
      }
    } catch (Exception e) {
      this.LogException("Could Not Change Password: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retString;
  }

  /**
   * UpdateUser
   * @param userID
   * @param email
   * @param status
   * @param firstName
   * @param midInit
   * @param lastName
   * @param phone
   * @param last4SSN
   * @param comments
   * @return int
   * @deprecated
   */
  private int UpdateUser (int userID, String email, String status, String firstName, String midInit,
                          String lastName, String phone, String last4SSN, String comments)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "update " + this.TableName + " set";
      sql += " " + this.LastName + " = '" + lastName + "', " + this.FirstName + " = '" + firstName + "'";
      if (midInit != null)
        sql += ", " + this.MidInit + " = '" + midInit + "'";
      sql += ", " + this.UserStatusCD + " = '" + status + "'";
      if (last4SSN != null)
        sql += ", " + this.Last4SSN + " = '" + last4SSN + "'";
      if (phone != null)
        sql += ", " + this.Phone + " = '" + phone + "'";
      if (comments != null)
        sql += ", " + this.Comments + " = '" + comments + "'";
      sql += ", " + this.UpdatedDate + " = sysdate, " + this.UpdatedBy + " = 'system'";
      sql += " where " + this.UserID + " = " + userID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retInt = userID;
    } catch (Exception e) {
      this.LogException("Could Not Update User: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retInt;
  }

  /**
   * InsertUser
   * @param email
   * @param loginName
   * @param status
   * @param firstName
   * @param midInit
   * @param lastName
   * @param phone
   * @param last4SSN
   * @param comments
   * @return int
   * @deprecated
   */
  private int InsertUser (String email, String loginName, String status,  String firstName, String midInit, String lastName,
                          String phone, String last4SSN, String comments)
   {
     int retInt = -1;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "insert into " + this.TableName + " (" + this.UserID + ", " + this.LastName + ", " + this.FirstName;
       if (midInit != null)
         sql += ", " + this.MidInit;
       sql += ", " + this.LoginName + ", " + this.LoginPassword + ", " + this.UserStatusCD;
       if (last4SSN != null)
         sql += ", " + this.Last4SSN;
       sql += ", " + this.ChangePWDFlag;
       if (phone != null)
         sql += ", " + this.Phone;
       if (comments != null)
         sql += ", " + this.Comments;
       sql += ", " + this.CreatedDate + ", " + this.CreatedBy + ", " + this.UpdatedDate + ", " + this.UpdatedBy + ")";
       int userID = this.GetIncrementID(this.TableName,this.UserID);
       sql += " values (" + userID + ", '" + lastName + "','" + firstName + "'";
       if (midInit != null)
         sql += ",'" + midInit + "'";
       Cryptography crypt = new Cryptography();
       String password = crypt.Encrypting(Node.Utils.Utility.GeneratePassword(), Phrase.CryptKey);
       sql += ",'" + loginName + "','" + password + "','" + status + "'";
       if (last4SSN != null)
         sql += ",'" + last4SSN + "'";
       sql += ",'Y'";
       if (phone != null)
         sql += ",'" + phone + "'";
       if (comments != null)
         sql += ",'" + comments + "'";
       sql += ",sysdate,'system',sysdate,'system')";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       retInt = userID;
     } catch (Exception e) {
       this.LogException("Could Not Insert User: " + e.toString());
     }
     finally {
       try {
         if (rs != null)
           rs.close();
         if (db != null)
           db.Close();
       } catch (Exception e) {
         this.LogException(e.toString());
       }
     }
     return retInt;
  }

  /**
   * DeleteUserLayout
   * @param userID
   * @return int
   * @deprecated
   */
  public boolean DeleteUserLayout (int userID)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.UserID},this.SysUserPageLayoutTableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int layoutID = rs.getInt(this.UserID);
        rs.deleteRow();
        if (layoutID >= 0) {
          sql = "delete from "+this.SysUserPageLayoutTableName+" where "+this.UserID+" = "+layoutID;
          rs.close();
          rs = db.GetResultSet(sql);
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Delete User Layout: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * DeleteUserLayout
   * @param userID
   * @return boolean
   * @deprecated
   */
  public boolean DeleteUser (int userID)
  {
     boolean retBool = false;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "delete from "+this.UserOpXREFTableName+" where "+this.UserID+" = "+userID;
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null)
         rs.close();
       sql = "delete from "+this.NodeAccountTypeXREFTableName+" where "+this.UserID+" = "+userID;
       rs = db.GetResultSet(sql);
       if (rs != null)
         rs.close();
       sql = "delete from "+this.UserOpXREFTableName+" where "+this.UserID+" = "+userID;
       rs = db.GetResultSet(sql);
       if (rs != null)
         rs.close();
       this.DeleteAddress(userID);
       this.DeleteEmail(userID);
       this.DeleteUserLayout(userID);
       sql = "delete from "+this.TableName+" where "+this.UserID+" = "+userID;
       rs = db.GetResultSet(sql);
       retBool = true;
     } catch (Exception e) {
       this.LogException("Could Not Delete User: " + e.toString());
     }
     finally {
       try {
         if (rs != null)
           rs.close();
         if (db != null)
           db.Close();
       } catch (Exception e) {
         this.LogException(e.toString());
       }
     }
     return retBool;
  }

  /**
   * SendEmail
   * @param receiverEmail
   * @param firstName
   * @param lastName
   * @param loginName
   * @param password
   * @return boolean
   * @deprecated
   */
  public boolean SendEmail (String receiverEmail, String firstName, String lastName, String loginName, String password)
  {
    boolean retBool = false;
    SystemConfiguration configDB = new SystemConfiguration(this.LoggerName);
    Email email = new Email(configDB.GetEmailServerHost(),configDB.GetEmailServerPort(),configDB.GetUserEmailUID(),configDB.GetUserEmailPWD());
    String content = this.GetTemplate(configDB.GetEmailTemplateLocation(), firstName, lastName, loginName, password);
    String[] ccList = configDB.GetUserEmailCCList();
    ArrayList cc = null;
    if (ccList != null && ccList.length > 0) {
      cc = new ArrayList();
      for (int i = 0; i < ccList.length; i++)
        cc.add(ccList[i]);
    }
    String[] bccList = configDB.GetUserEmailBCCList();
    ArrayList bcc = null;
    if (bccList != null && bccList.length > 0) {
      bcc = new ArrayList();
      for (int i = 0; i < bccList.length; i++)
        bcc.add(bccList[i]);
    }
    retBool = email.SendEmail(configDB.GetUserEmailSubject(), content, configDB.GetUserEmailSenderEmail(),receiverEmail,cc,bcc);
    return retBool;
  }

  /**
   * GetConsoleUsers
   * @param 
   * @return User[]
   * @deprecated
   */
  public User[] GetConsoleUsers ()
  {
    User[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.UserID,"A."+this.LoginName,"C."+this.NodeAccountType };
      String tableNames = this.TableName+" A, "+this.NodeAccountTypeXREFTableName+" B, "+this.NodeAccountTypeTableName+" C";
      String condition = "A."+this.UserID+" = B."+this.UserID+" and B."+this.NodeAccountTypeID+" = C."+this.NodeAccountTypeID;
      condition += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        HashMap map = new HashMap();
        rs.beforeFirst();
        while (rs.next()) {
          int id = rs.getInt(this.UserID);
          if (!map.containsKey(id+"")) {
            User user = new User(id);
            user.SetAccountType(rs.getString(this.NodeAccountType));
            user.SetLoginName(rs.getString(this.LoginName));
            map.put(id+"",user);
          }
        }
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            retArray = new User [temp.length];
            for (int i = 0; i < temp.length; i++)
              retArray[i] = (User)temp[i];
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get List of Console Users: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

  /**
   * GetConsoleUsers
   * @param loginNames
   * @return User[]
   * @deprecated
   */
  public User[] GetConsoleUsers (String[] loginNames)
  {
    if (loginNames == null || loginNames.length <= 0)
      return null;
    User[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.UserID,"A."+this.LoginName,"C."+this.NodeAccountType };
      String tableNames = this.TableName+" A, "+this.NodeAccountTypeXREFTableName+" B, "+this.NodeAccountTypeTableName+" C";
      String condition = "A."+this.UserID+" = B."+this.UserID+" and B."+this.NodeAccountTypeID+" = C."+this.NodeAccountTypeID;
      condition += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
      if (loginNames != null && loginNames.length > 0) {
        condition += " and A."+this.LoginName+" in (";
        for (int i = 0; i < loginNames.length; i++) {
          if (i != 0) condition += ",";
          condition += "'"+loginNames[i]+"'";
        }
        condition += ")";
      }
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        HashMap map = new HashMap();
        rs.beforeFirst();
        while (rs.next()) {
          int id = rs.getInt(this.UserID);
          if (!map.containsKey(id+"")) {
            User user = new User(id);
            user.SetAccountType(rs.getString(this.NodeAccountType));
            user.SetLoginName(rs.getString(this.LoginName));
            map.put(id+"",user);
          }
        }
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            retArray = new User [temp.length];
            for (int i = 0; i < temp.length; i++)
              retArray[i] = (User)temp[i];
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get List of Console Users: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

  /**
   * GetTemplate
   * @param templateName
   * @param firstName
   * @param lastName
   * @param login
   * @param password
   * @return String
   * @deprecated
   */
  private String GetTemplate (String templateName, String firstName, String lastName, String login, String password)
  {
    String content = null;
    try {
      //content = Utility.ReadToEnd(AppUtils.AppRoot + "/WEB-INF/config/" + templateName);
      content = Utility.ReadToEnd(AppUtils.getAppRoot() + "/WEB-INF/config/" + templateName);
      content = Utility.replace(content,"<%var_systime%>",Utility.GetNow());
      content = Utility.replace(content,"<%var_first%>",firstName);
      content = Utility.replace(content,"<%var_last%>",lastName);
      content = Utility.replace(content,"<%var_login%>",login);
      content = Utility.replace(content,"<%var_special_msg%>",password);
    } catch (Exception e) {
      this.LogException("Could Not Get Email Template: " + e);
    }
    return content;
  }
  /**
   * verifyEmail
   * @param email
   * @return int
   * @deprecated
   */

	public int verifyEmail(String email) {
		// TODO Auto-generated method stub
		return -99;
	}
}
