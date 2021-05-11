package Node.DB.Oracle10i;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;

import Node.DB.Interfaces.INodeAccountType;
import Node.Phrase;
/**
 * <p>This class create NodeAccountType.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeAccountType extends NodeDB implements INodeAccountType {
  private static int[] AccountTypeIDs = null;

  private String TableName = this.NodeAccountTypeTableName;
  private String ID = this.NodeAccountTypeID;
  private String Type = this.NodeAccountType;
  private String Description = "ACCOUNT_DESC";

  /**
   * Constructor
   * @param loggerName
   * @deprecated
   * @return 
   */
  public NodeAccountType(String loggerName) {
    super(loggerName);
  }

  /**
   * GetAccountTypeIDs
   * @param 
   * @deprecated
   * @return int[]
   */
  public int[] GetAccountTypeIDs ()
  {
    if (this.AccountTypeIDs == null) {
      IDBAdapter db = null;
      ResultSet rs = null;
      try {
        String sql = this.GetSelectStr(new String[]{this.ID,this.Type},this.TableName,null,null);
        db = this.GetNodeDB();
        rs = db.GetResultSet(sql);
        if (rs != null) {
          this.AccountTypeIDs = new int [3];
          rs.beforeFirst();
          while (rs.next()) {
            String type = rs.getString(this.Type);
            if (type != null) {
              if (type.equals(Phrase.ConsoleUser))
                this.AccountTypeIDs[0] = rs.getInt(this.ID);
              if (type.equals(Phrase.LocalNodeUser))
                this.AccountTypeIDs[1] = rs.getInt(this.ID);
              if (type.equals(Phrase.NAASNodeUser))
                this.AccountTypeIDs[2] = rs.getInt(this.ID);
            }
          }
        }
      } catch (Exception e) {
        this.LogException("Could Not Get Get Account Type IDs: " + e.toString());
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
    }
    return this.AccountTypeIDs;
  }

  /**
   * IsLocallyManagedNodeUser
   * @param userName
   * @deprecated
   * @return boolean
   */
  public boolean IsLocallyManagedNodeUser (String userName)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      NodeUser userDB = new NodeUser(this.LoggerName);
      int userID = userDB.GetUserID(userName);
      if (userID >= 0) {
        NodeAccountTypeXREF xrefDB = new NodeAccountTypeXREF(this.LoggerName);
        int[] accountTypeIDs = xrefDB.GetAccountTypeID(userID);
        if (accountTypeIDs != null) {
          for (int i = 0; i < accountTypeIDs.length; i++) {
            String type = this.GetAccountType(accountTypeIDs[i]);
            if (type != null && type.equalsIgnoreCase("LOCAL_NODE_USER")) {
              retBool = true;
              break;
            }
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Determine if the user is Locally Managed: " + e.toString());
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
   * GetAccountType
   * @param accountTypeID
   * @deprecated
   * @return String
   */
  public String GetAccountType (int accountTypeID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Type},this.TableName,new String[]{this.ID},new String[]{accountTypeID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.Type);
    } catch (Exception e) {
      this.LogException("Could Not Get Account Type: " + e.toString());
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
   * GetAccountTypes
   * @param 
   * @deprecated
   * @return String[]
   */
  public String[] GetAccountTypes ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Type},this.TableName,new String[]{},new String[]{});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last()) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.Type);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get List of Account Types: " + e.toString());
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
    return retArray;
  }

  /**
   * GetAccountTypeID
   * @param accountType
   * @deprecated
   * @return 
   */
  public int GetAccountTypeID (String accountType)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      if (this.AccountTypeIDs == null)
        this.GetAccountTypeIDs();
      if (accountType.equals(Phrase.ConsoleUser))
        retInt = this.AccountTypeIDs[0];
      else if (accountType.equals(Phrase.LocalNodeUser))
        retInt = this.AccountTypeIDs[1];
      else if (accountType.equals(Phrase.NAASNodeUser))
        retInt = this.AccountTypeIDs[2];
    } catch (Exception e) {
      this.LogException("Could Not Get ID of Account Type: " + e.toString());
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
}
