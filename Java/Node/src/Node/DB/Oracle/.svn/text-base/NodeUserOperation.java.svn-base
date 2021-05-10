package Node.DB.Oracle;

import java.sql.ResultSet;
import java.util.HashMap;
import java.util.Iterator;

import Node.DB.Interfaces.INodeUserOperation;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create NodeUserOperation.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeUserOperation extends NodeDB implements INodeUserOperation {
  private String TableName = this.UserOpXREFTableName;
  private String UserID = "USER_ID";
  private String OpID = "OPERATION_ID";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeUserOperation(String loggerName) {
    super(loggerName);
  }

  /**
   * IsUserAllowed
   * @param userID
   * @param opID
   * @return int
   */
  public int IsUserAllowed (int userID, int opID)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.UserID},this.TableName,new String[]{this.UserID,this.OpID},new String[]{userID+"",opID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0)
        retInt = rs.getInt(this.UserID);
    } catch (Exception e) {
      this.LogException("Could Not Authorize User Through Database: " + e.toString());
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
    return retInt;
  }

  /**
   * SetOperationPriviledge
   * @param userID
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return boolean
   */
  public boolean SetOperationPriviledge (int userID, String[] opNames, String[] wsNames, String[] adminDomains)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      HashMap map = new HashMap();
      db = this.GetNodeDB();
      if (opNames != null && opNames.length > 0) {
        String sql = null;
        if (wsNames != null && wsNames.length == opNames.length) {
          String[] select = new String[] { "A."+this.OperationID,"A."+this.OperationName };
          String tableNames = this.OperationTableName+" A,"+this.WebServiceTableName+" B";
          String condition = "A."+this.WebServiceID+" = B."+this.WebServiceID;
          condition += " and (";
          for (int i = 0; i < opNames.length; i++) {
            if (i != 0) condition += " or ";
            condition += "(A."+this.OperationName+" = '"+opNames[i]+"' and B."+this.WebService+" = '"+wsNames[i]+"')";
          }
          condition += ")";
          sql = this.GetSelectStr(select,tableNames,condition);
        }
        else {
          String[] select = new String[] { this.OperationID,this.OperationName };
          String tableNames = this.OperationTableName;
          String condition = "";
          for (int i = 0; i < opNames.length; i++) {
            if (i != 0) condition+= " or ";
            condition += "("+this.OperationName+" = '"+opNames[i]+"')";
          }
          condition += ")";
          sql = this.GetSelectStr(select,tableNames,condition);
        }
        rs = db.GetResultSet(sql);
        if (rs != null) {
          rs.beforeFirst();
          while (rs.next()) {
            int opID = rs.getInt(this.OperationID);
            String opName = rs.getString(this.OperationName);
            if (opID >= 0 && opName != null)
              map.put(opID+"",opName);
          }
          rs.close();
        }
      }
      this.RemoveOperations(userID,map,adminDomains);
      if (!map.isEmpty()) {
        String sql = this.GetSelectStr(new String[]{this.UserID,this.OpID},this.TableName,"1 = -1");
        rs = db.GetResultSet(sql);
        if (rs != null) {
          Iterator iter = map.keySet().iterator();
          while (iter.hasNext()) {
            rs.moveToInsertRow();
            rs.updateInt(this.UserID,userID);
            rs.updateInt(this.OpID,Integer.parseInt((String)iter.next()));
            rs.insertRow();
          }
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Set Operations Privileges: " + e.toString());
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
   * RemoveOperations
   * @param userID
   * @param operations
   * @param adminDomains
   * @return 
   */
  private void RemoveOperations (int userID, HashMap operations, String[] adminDomains)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A." + this.OpID + " from " + this.TableName + " A, " + this.OperationTableName + " B";
      sql += ", " + this.NodeDomainTableName + " C";
      sql += " where A." + this.OpID + " = B." + this.OpID + " and B." + this.NodeDomainID + " = C." + this.NodeDomainID;
      sql += " and A." + this.UserID + " = " + userID;
      if (adminDomains != null && adminDomains.length > 0)
      {
        sql += " and C." + this.NodeDomain + " in (";
        for (int i = 0; i < adminDomains.length; i++)
        {
          if (i != 0) sql += ", ";
          sql += "'" + adminDomains[i] + "'";
        }
        sql += ")";
      }
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.beforeFirst();
        String sql2 = "select A." + this.UserID + ", A." + this.OpID + " from " + this.TableName + " A where";
        for (int i = 0; rs.next(); i++) {
          if (i != 0) sql2 += " or";
          sql2 += "(A." + this.UserID + " = " + userID + " and A." + this.OpID + " = " + rs.getString(this.OpID) + ")";
        }
        ResultSet rs2 = db.GetResultSet(sql2);
        if (rs2 != null) {
          rs2.beforeFirst();
          while (rs2.next())
          {
            String opID = rs2.getString(this.OpID);
            if (operations.containsKey(opID))
              operations.remove(opID);
            else
              rs2.deleteRow();
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Remove Operations: " + e.toString());
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
  }

  /**
   * GetAssignedOperations
   * @param userID
   * @return String[]
   */
  public String[] GetAssignedOperations (int userID)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "B."+this.OperationName, "C."+this.WebService, "D."+this.NodeDomain };
      String tableNames = this.TableName + " A, " + this.OperationTableName + " B, " + this.WebServiceTableName;
      tableNames += " C, " + this.NodeDomainTableName + " D";
      String condition = "A."+this.UserID+" = "+userID+" and A."+this.OpID+" = B."+this.OpID;
      condition += " and B."+this.WebServiceID+" = C."+this.WebServiceID+" and B."+this.NodeDomainID+" = D."+this.NodeDomainID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.NodeDomain) + ": " + rs.getString(this.WebService) + "." + rs.getString(this.OperationName);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Assigned Operations: " + e.toString());
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
}
