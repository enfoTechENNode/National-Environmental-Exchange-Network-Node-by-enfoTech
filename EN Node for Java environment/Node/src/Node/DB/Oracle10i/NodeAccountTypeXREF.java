package Node.DB.Oracle10i;

import java.sql.ResultSet;

import Node.DB.Interfaces.INodeAccountTypeXREF;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create NodeAccountTypeXREF.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeAccountTypeXREF extends NodeDB implements INodeAccountTypeXREF {
  private String TableName = "NODE_ACCOUNT_TYPE_XREF";
  private String XrefID = "ACCOUNT_TYPE_XREF_ID";
  private String AccountTypeID = "ACCOUNT_TYPE_ID";
  private String UserID = "USER_ID";
  private String DomainID = "DOMAIN_ID";

  /**
   * Constructor
   * @param loggerName
   * @return
   * @deprecated
   */
  public NodeAccountTypeXREF(String loggerName) {
    super(loggerName);
  }

  /**
   * GetAccountTypeID
   * @param userID
   * @return int[]
   * @deprecated
   */
  public int[] GetAccountTypeID (int userID)
  {
    int[] retInt = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.AccountTypeID},this.TableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        if (rs.last()) {
          retInt = new int[rs.getRow()];
          rs.beforeFirst();
          for (int i = 0; rs.next() && i < retInt.length; i++)
            retInt[i] = rs.getInt(this.AccountTypeID);
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Account TypeID: " + e.toString());
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
   * SaveAccountTypes
   * @param userID
   * @param accTypes
   * @param domainIDs
   * @return boolean
   * @deprecated
   */
  public boolean SaveAccountTypes (int userID, int[] accTypes, int[] domainIDs)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      if (accTypes != null && domainIDs != null && accTypes.length == domainIDs.length && accTypes.length > 0) {
        if (this.RemoveExtra(userID,accTypes,domainIDs)) {
          db = this.GetNodeDB();
          for (int i = 0; i < accTypes.length; i++) {
            if (accTypes[i] >= 0 && domainIDs[i] >= 0) {
              String sql = "insert into " + this.TableName + " (" + this.XrefID + "," + this.UserID + "," + this.AccountTypeID + "," + this.DomainID + ")";
              sql += " values ("+this.GetIncrementID(this.TableName,this.XrefID)+","+userID+","+accTypes[i]+","+domainIDs[i]+")";
              if (rs != null)
                rs.close();
              rs = db.GetResultSet(sql);
            }
          }
        }
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Account Types: " + e.toString());
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
   * SaveAccountType
   * @param userID
   * @param accTypes
   * @return boolean
   * @deprecated
   */
  public boolean SaveAccountType (int userID, int accType)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      if (userID >= 0 && accType >= 0) {
        String sql = "select "+this.XrefID+" from "+this.TableName+" where "+this.UserID+" = "+userID;
        sql += " and ("+this.AccountTypeID+" != "+accType+" or "+this.DomainID+" is not null)";
        db = this.GetNodeDB();
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() > 0) {
          rs.beforeFirst();
          while (rs.next())
            rs.deleteRow();
        }
        if (rs != null)
          rs.close();
        sql = "select "+this.XrefID+","+this.UserID+","+this.AccountTypeID+" from "+this.TableName;
        sql += " where "+this.UserID+" = "+userID+" and "+this.AccountTypeID+" = "+accType;
        rs = db.GetResultSet(sql);
        if (rs != null && !rs.first()) {
          rs.moveToInsertRow();
          int id = this.GetIncrementID(this.TableName,this.XrefID);
          rs.updateInt(this.XrefID,id);
          rs.updateInt(this.AccountTypeID,accType);
          rs.updateInt(this.UserID,userID);
          rs.insertRow();
        }
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Account Types: " + e.toString());
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
   * RemoveExtra
   * @param userID
   * @param accTypes
   * @param domainIDs
   * @return boolean
   * @deprecated
   */
  private boolean RemoveExtra (int userID, int[] accTypes, int[] domainIDs)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.XrefID,this.UserID,this.AccountTypeID,this.DomainID},this.TableName,
                                     new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.beforeFirst();
        while (rs.next()) {
          int accType = rs.getInt(this.AccountTypeID);
          int domain = rs.getInt(this.DomainID);
          boolean isFound = false;
          for (int i = 0; i < accTypes.length && i < domainIDs.length; i++) {
            if (accType == accTypes[i] && domain == domainIDs[i]) {
              accTypes[i] = -1;
              domainIDs[i] = -1;
              isFound = true;
              break;
            }
          }
          if (!isFound)
            rs.deleteRow();
        }
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Remove Extras: " + e.toString());
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
}
