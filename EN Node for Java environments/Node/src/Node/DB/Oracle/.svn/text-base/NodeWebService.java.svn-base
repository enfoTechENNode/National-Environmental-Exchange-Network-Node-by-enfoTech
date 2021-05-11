package Node.DB.Oracle;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;

import Node.DB.Interfaces.INodeWebService;
import Node.DB.Oracle.NodeDB;
/**
 * <p>This class create NodeWebService.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeWebService extends NodeDB implements INodeWebService {
  // Table Name
  private String TableName = this.WebServiceTableName;

  // Column Names
  private String WSID = this.WebServiceID;
  private String WSName = this.WebService;
  private String WSDescription = this.WebServiceDesc;

  private static int[] WebServiceIDs = null;

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeWebService(String loggerName) {
    super(loggerName);
    if (NodeWebService.WebServiceIDs == null)
    {
      this.SetWebServiceIDs();
    }
  }

  /**
   * SetWebServiceIDs
   * @param
   * @return 
   */
  private void SetWebServiceIDs ()
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String condition = this.WSName+" in ('SUBMIT','DOWNLOAD','QUERY','SOLICIT','NOTIFY')";
      String sql = this.GetSelectStr(new String[]{this.WSID,this.WSName},this.TableName,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.beforeFirst();
        NodeWebService.WebServiceIDs = new int [5];
        while (rs.next()) {
          String name = rs.getString(this.WSName);
          if (name != null) {
            if (name.equalsIgnoreCase("SUBMIT"))
              NodeWebService.WebServiceIDs[0] = rs.getInt(this.WSID);
            if (name.equalsIgnoreCase("DOWNLOAD"))
              NodeWebService.WebServiceIDs[1] = rs.getInt(this.WSID);
            if (name.equalsIgnoreCase("QUERY"))
              NodeWebService.WebServiceIDs[2] = rs.getInt(this.WSID);
            if (name.equalsIgnoreCase("SOLICIT"))
              NodeWebService.WebServiceIDs[3] = rs.getInt(this.WSID);
            if (name.equalsIgnoreCase("NOTIFY"))
              NodeWebService.WebServiceIDs[4] = rs.getInt(this.WSID);
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Set WebServiceIDs: " + e.toString());
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
   * GetWebServiceID
   * @param wsName
   * @return int
   */
  public int GetWebServiceID (String wsName)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    int retInt = -1;
    try {
      String sql = this.GetSelectStr(new String[]{this.WSID},this.TableName,new String[]{this.WSName},new String[]{wsName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.WSID);
    } catch (Exception e) {
      this.LogException("Could not Get WebServiceID: " + e.toString());
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
   * GetWSList
   * @param 
   * @return String[]
   */
  public String[] GetWSList ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.WSName},this.TableName,null,null);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.WSName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get WebService List: " + e.toString());
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
   * GetWSName
   * @param wsID
   * @return String
   */
  public String GetWSName (int wsID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.WSName},this.TableName,new String[]{this.WSID},new String[]{wsID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.WSName);
    } catch (Exception e) {
      this.LogException("Could not Get WebService Name: " + e.toString());
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
    return retString;
  }

  /**
   * GetWSIDs
   * @param 
   * @return int[]
   */
  public int[] GetWSIDs ()
  {
    return NodeWebService.WebServiceIDs;
  }
}
