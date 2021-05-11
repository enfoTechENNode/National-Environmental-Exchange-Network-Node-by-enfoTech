package Node.DB.Oracle;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;

import Node.Biz.Administration.Domain;
import Node.Biz.Administration.Status;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Oracle.Configuration.SystemConfiguration;
import Node.Phrase;
import Node.Utils.Utility;
/**
 * <p>This class create NodeDomain.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeDomain extends NodeDB implements INodeDomain {
  private static int[] DomainIDs = null;

  private String TableName = "NODE_DOMAIN";
  private String ID = "DOMAIN_ID";
  private String Name = "DOMAIN_NAME";
  private String Description = "DOMAIN_DESC";
  private String Status = "DOMAIN_STATUS_CD";
  private String Message = "DOMAIN_STATUS_MSG";
  private String CreatedDate = "CREATED_DTTM";
  private String CreatedBy = "CREATED_BY";
  private String UpdatedDate = "UPDATED_DTTM";
  private String UpdatedBy = "UPDATED_BY";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeDomain(String loggerName) {
    super(loggerName);
  }

  /**
   * GetDomainStatus
   * @param domainID
   * @return String
   */
  public String GetDomainStatus (int domainID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.ID},new String[]{domainID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.Status);
    } catch (Exception e) {
      this.LogException("Could Not Get Domain Status: " + e.toString());
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
   * GetDomainStatus
   * @param domainName
   * @return String
   */
  public String GetDomainStatus (String domainName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.Name},new String[]{domainName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.Status);
    } catch (Exception e) {
      this.LogException("Could Not Get Domain Status: " + e.toString());
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
   * GetDomainName
   * @param domainID
   * @return String
   */
  public String GetDomainName (int domainID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Name},this.TableName,new String[]{this.ID},new String[]{domainID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.Name);
    } catch (Exception e) {
      this.LogException("Could Not Get Domain Name: " + e.toString());
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
   * GetDomainName
   * @param opName
   * @param wsName
   * @return String
   */
  public String GetDomainName (String opName, String wsName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A." + this.Name + " from " + this.TableName + " A";
      sql += ", " + this.OperationTableName + " B";
      sql += " left join " + this.WebServiceTableName + " C on B." + this.WebServiceID + " = C." + this.WebServiceID;
      sql += " where upper(B." + this.OperationName + ") = '" + opName.toUpperCase() + "' and B." + this.ID + " = A." + this.ID;
      if (wsName != null)
        sql += " and upper(C." + this.WebService + ") = '" + wsName.toUpperCase() + "'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last())
        retString = rs.getString(this.Name);
    } catch (Exception e) {
      this.LogException("Could Not Get Domain Name: " + e.toString());
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
   * GetDomains
   * @param 
   * @param 
   * @return String[]
   */
  public String[] GetDomains ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Name},this.TableName,null,null);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.Name);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Domain List: " + e.toString());
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
   * GetDomains
   * @param domainNames
   * @param 
   * @return Domain[]
   */
  public Domain[] GetDomains (String[] domainNames)
  {
    Domain[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A."+this.ID+", A."+this.Name+" from "+this.TableName+" A";
      if (domainNames != null && domainNames.length > 0) {
        sql += " where A."+this.Name+" in (";
        for (int i = 0; i < domainNames.length; i++) {
          if (i != 0) sql += ",";
          sql += "'"+domainNames[i]+"'";
        }
        sql += ")";
      }
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Domain [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          retArray[i] = new Domain(rs.getInt(this.ID));
          retArray[i].SetDomainName(rs.getString(this.Name));
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Domain List: " + e.toString());
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
   * GetOperations
   * @param domainName
   * @param 
   * @return String[]
   */
  public String[] GetOperations (String domainName)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.ID},this.TableName,new String[]{this.Name},new String[]{domainName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        NodeOperation opDB = new NodeOperation(this.LoggerName);
        retArray = opDB.GetOperations(rs.getInt(this.ID));
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Operation List: " + e.toString());
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
   * GetDomainID
   * @param domain
   * @param 
   * @return int
   */
  public int GetDomainID (String domain)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.ID},this.TableName,new String[]{this.Name},new String[]{domain});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.ID);
    } catch (Exception e) {
      this.LogException("Could Not Get Domain ID: " + e.toString());
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
   * GetOppositeDomains
   * @param domains
   * @param 
   * @return String[]
   */
  public String[] GetOppositeDomains (String[] domains)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select " + this.Name + " from " + this.TableName;
      if (domains != null && domains.length > 0) {
        sql += " where";
        for (int i = 0; i < domains.length; i++) {
          if (i != 0)
            sql += " and";
          sql += " " + this.Name + " != '" + domains[i] + "'";
        }
      }
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.Name);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Domain ID: " + e.toString());
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
   * SearchDomains
   * @param domainPermissions
   * @param domainName
   * @param status
   * @return Domain[]
   */
  public Domain[] SearchDomains (String[] domainPermissions, String domainName, String status)
  {
    Domain[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select * from "+this.TableName;
      boolean needAnd = false;
      if (domainPermissions != null && domainPermissions.length > 0) {
        sql += " where "+this.Name+" in (";
        for (int i = 0; i < domainPermissions.length; i++) {
          if (i != 0)
            sql += ",";
          sql += "'"+domainPermissions[i]+"'";
        }
        sql += ")";
        needAnd = true;
      }
      if (domainName != null) {
        if (needAnd)
          sql += " and";
        else
          sql += " where";
        sql += " "+this.Name+" = '" + domainName + "'";
        needAnd = true;
      }
      if (status != null) {
        if (needAnd)
          sql += " and";
        else
          sql += " where";
        if (status.equalsIgnoreCase("Active"))
          sql += " ("+this.Status+" = 'Running' or "+this.Status+" = 'Stopped')";
        else
          sql += " "+this.Status+" = '"+status+"'";
        needAnd = true;
      }
      sql += " order by substr("+this.Status+",2) desc, "+this.Name+" asc";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Domain [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          int domainID = rs.getInt(this.ID);
          retArray[i] = new Domain(domainID);
          retArray[i].SetDomainName(rs.getString(this.Name));
          retArray[i].SetDomainDesc(rs.getString(this.Description));
          retArray[i].SetDomainStatusCD(rs.getString(this.Status));
          retArray[i].SetDomainStatusMsg(rs.getString(this.Message));
          retArray[i].SetCreatedDate(rs.getDate(this.CreatedDate));
          retArray[i].SetCreatedBy(rs.getString(this.CreatedBy));
          retArray[i].SetUpdatedDate(rs.getDate(this.UpdatedDate));
          retArray[i].SetUpdatedBy(rs.getString(this.UpdatedBy));
          String tableNames2 = this.SysUserInfoTableName+" A,"+this.TableName+" B,"+this.NodeAccountTypeTableName+" C,";
          tableNames2 += this.NodeAccountTypeXREFTableName+" D";
          String condition2 = "B."+this.ID+" = "+domainID;
          condition2 += " and B."+this.ID+" = D."+this.ID;
          condition2 += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
          condition2 += " and C."+this.NodeAccountTypeID+" = D."+this.NodeAccountTypeID;
          condition2 += " and D."+this.SysUserInfoUserID+" = A."+this.SysUserInfoUserID;
          String sql2 = this.GetSelectStr(new String[]{"A."+this.SysUserInfoLoginName},tableNames2,condition2);
          ResultSet rs2 = db.GetResultSet(sql2);
          if (rs2 != null) {
            rs2.beforeFirst();
            while (rs2.next())
              retArray[i].AddAdmin(rs2.getString(this.SysUserInfoLoginName));
            rs2.close();
          }
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Search Domains: " + e.toString());
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
   * GetDomain
   * @param domainID
   * @return Domain
   */
  public Domain GetDomain (int domainID)
  {
    return this.GetDomainByIDOrName(domainID,null);
  }

  /**
   * GetDomain
   * @param domainName
   * @return Domain
   */
  public Domain GetDomain (String domainName)
  {
    return this.GetDomainByIDOrName(-1,domainName);
  }

  /**
   * GetDomainByIDOrName
   * @param domainID
   * @param domainName
   * @return Domain
   */
  private Domain GetDomainByIDOrName (int domainID, String domainName)
  {
    if (domainID < 0 && domainName == null)
      return null;
    Domain retDomain = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A.*,C."+this.WebService+",F."+this.SysUserInfoLoginName;
      sql += " from "+this.TableName+" A";
      sql += " left join "+this.DomainWSXREFTableName+" B on A."+this.ID+" = B."+this.ID;
      sql += " left join "+this.WebServiceTableName+" C on B."+this.WebServiceID+" = C."+this.WebServiceID;
      sql += " left join "+this.NodeAccountTypeXREFTableName+" D on A."+this.ID+" = D."+this.ID;
      sql += " left join "+this.NodeAccountTypeTableName+" E on D."+this.NodeAccountTypeID+" = E."+this.NodeAccountTypeID;
      sql += " and E."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
      sql += " left join "+this.SysUserInfoTableName+" F on D."+this.SysUserInfoUserID+" = F."+this.SysUserInfoUserID;
      if  (domainID >= 0)
        sql += " where A."+this.ID+" = "+domainID;
      else
        sql += " where A."+this.Name+" = '"+domainName+"'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        rs.beforeFirst();
        for (int i = 0; rs.next(); i++) {
          if (i == 0) {
            retDomain = new Domain(rs.getInt(this.ID));
            retDomain.AddAdmin(rs.getString(this.SysUserInfoLoginName));
            String ws = rs.getString(this.WebService);
            if (ws != null) {
              retDomain.SetAllowDownload(ws.equals(Phrase.WEB_METHOD_DOWNLOAD));
              retDomain.SetAllowNotify(ws.equals(Phrase.WEB_METHOD_NOTIFY));
              retDomain.SetAllowQuery(ws.equals(Phrase.WEB_METHOD_QUERY));
              retDomain.SetAllowSolicit(ws.equals(Phrase.WEB_METHOD_SOLICIT));
              retDomain.SetAllowSubmit(ws.equals(Phrase.WEB_METHOD_SUBMIT));
            }
            retDomain.SetCreatedBy(rs.getString(this.CreatedBy));
            retDomain.SetCreatedDate(rs.getDate(this.CreatedDate));
            retDomain.SetDomainDesc(rs.getString(this.Description));
            retDomain.SetDomainName(rs.getString(this.Name));
            retDomain.SetDomainStatusCD(rs.getString(this.Status));
            retDomain.SetDomainStatusMsg(rs.getString(this.Message));
            retDomain.SetUpdatedBy(rs.getString(this.UpdatedBy));
            retDomain.SetUpdatedDate(rs.getDate(this.UpdatedDate));
          }
          else {
            retDomain.AddAdmin(rs.getString(this.SysUserInfoLoginName));
            String ws = rs.getString(this.WebService);
            if (ws != null) {
              if (ws.equals(Phrase.WEB_METHOD_DOWNLOAD)) retDomain.SetAllowDownload(true);
              if (ws.equals(Phrase.WEB_METHOD_NOTIFY)) retDomain.SetAllowNotify(true);
              if (ws.equals(Phrase.WEB_METHOD_QUERY)) retDomain.SetAllowQuery(true);
              if (ws.equals(Phrase.WEB_METHOD_SOLICIT)) retDomain.SetAllowSolicit(true);
              if (ws.equals(Phrase.WEB_METHOD_SUBMIT)) retDomain.SetAllowSubmit(true);
            }
          }
        }
      }
      /*
      String[] select = new String[] { "A.*","B."+this.WebServiceID };
      String tableNames = this.TableName+" A, "+this.DomainWSXREFTableName+" B";
      String condition = "";
      if (domainID >= 0)
        condition += "A."+this.ID + " = " + domainID;
      else if (domainName != null)
        condition += "A."+this.Name + " = '" + domainName + "'";
      condition += " and A."+this.ID+" = B."+this.ID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      boolean foundPermissions = true;
      if (rs == null || !rs.first()) {
        foundPermissions = false;
        if (domainID >= 0)
          condition = this.ID + " = " + domainID;
        else if (domainName != null)
          condition = this.Name + " = '" + domainName + "'";
        sql = this.GetSelectStr(new String[]{"*"},this.TableName,condition);
        if (rs != null)
          rs.close();
        rs = db.GetResultSet(sql);
      }
      if (foundPermissions) {
        select = new String[] { "A.*", "B." + this.WebServiceID, "C." + this.SysUserInfoLoginName};
        tableNames = this.TableName+" A, "+this.DomainWSXREFTableName+" B, "+this.SysUserInfoTableName+" C, ";
        tableNames += this.NodeAccountTypeXREFTableName+" D, "+this.NodeAccountTypeTableName+" E";
        condition = "";
        if (domainID >= 0)
          condition += "A."+this.ID + " = " + domainID;
        else if (domainName != null)
          condition += "A."+this.Name + " = '" + domainName + "'";
        condition += " and A."+this.ID+" = B."+this.ID;
        condition += " and A."+this.ID+" = D."+this.ID;
        condition += " and D."+this.SysUserInfoUserID+" = C."+this.SysUserInfoUserID;
        condition += " and E."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
        condition += " and E."+this.NodeAccountTypeID+" = D."+this.NodeAccountTypeID;
      }
      else {
        select = new String[] { "A.*","B."+this.SysUserInfoLoginName };
        tableNames = this.TableName+" A, "+this.SysUserInfoTableName+" B, "+this.NodeAccountTypeXREFTableName+" C, "+this.NodeAccountTypeTableName+" D";
        condition = "";
        if (domainID >= 0)
          condition += "A."+this.ID + " = " + domainID;
        else if (domainName != null)
          condition += "A."+this.Name + " = '" + domainName + "'";
        condition += " and A."+this.ID+" = C."+this.ID;
        condition += " and C."+this.SysUserInfoUserID+" = B."+this.SysUserInfoUserID;
        condition += " and D."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
        condition += " and D."+this.NodeAccountTypeID+" = C."+this.NodeAccountTypeID;
      }
      sql = this.GetSelectStr(select,tableNames,condition);
      if (rs != null)
        rs.close();
      rs = db.GetResultSet(sql);
      boolean hasAdmins = true;
      if (rs != null && !rs.first()) {
        hasAdmins = false;
        if (foundPermissions) {
          select = new String[] { "A.*","B."+this.WebServiceID };
          tableNames = this.TableName+" A, "+this.DomainWSXREFTableName+" B";
          condition = "";
          if (domainID >= 0)
            condition += "A."+this.ID + " = " + domainID;
          else if (domainName != null)
            condition += "A."+this.Name + " = '" + domainName + "'";
          condition += " and A."+this.ID+" = B."+this.ID;
        }
        else {
          select = new String[] { "A.*" };
          tableNames = this.TableName+" A";
          condition = "";
          if (domainID >= 0)
            condition += "A."+this.ID + " = " + domainID;
          else if (domainName != null)
            condition += "A."+this.Name + " = '" + domainName + "'";
        }
      }
      if (rs != null)
        rs.close();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.beforeFirst();
        HashMap map = new HashMap();
        int id = -1;
        while (rs.next()) {
          id = rs.getInt(this.ID);
          if (id >= 0 && !map.containsKey(id+"")) {
            Domain temp = new Domain(id);
            temp.SetDomainName(rs.getString(this.Name));
            temp.SetDomainDesc(rs.getString(this.Description));
            temp.SetDomainStatusCD(rs.getString(this.Status));
            temp.SetDomainStatusMsg(rs.getString(this.Message));
            if (foundPermissions)
              this.SetAllowPermission(rs.getInt(this.WebServiceID),temp);
            if (hasAdmins)
              temp.AddAdmin(rs.getString(this.SysUserInfoLoginName));
            temp.SetCreatedDate(rs.getDate(this.CreatedDate));
            temp.SetCreatedBy(rs.getString(this.CreatedBy));
            temp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
            temp.SetUpdatedBy(rs.getString(this.UpdatedBy));
            map.put(id+"",temp);
          }
          else if (id >= 0) {
            Domain temp = (Domain)map.get(id+"");
            if (foundPermissions)
              this.SetAllowPermission(rs.getInt(this.WebServiceID),temp);
            if (hasAdmins)
              temp.AddAdmin(rs.getString(this.SysUserInfoLoginName));
          }
        }
        if (id >= 0 && map.containsKey(id+""))
          retDomain = (Domain)map.get(id+"");
      }*/
    } catch (Exception e) {
      this.LogException("Could Not Get Domain: " + e.toString());
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
    return retDomain;
  }

  /**
   * SaveDomain
   * @param domainID
   * @param description
   * @param status
   * @param message
   * @param isAllowSubmit
   * @param isAllowDownload
   * @param isAllowQuery
   * @param isAllowSolicit
   * @param isAllowNotify
   * @param admins
   * @return boolean
   */
  public boolean SaveDomain (int domainID, String description, String status, String message, boolean isAllowSubmit,
                             boolean isAllowDownload, boolean isAllowQuery, boolean isAllowSolicit, boolean isAllowNotify,
                             String[] admins)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String condition = "A."+this.ID + " = " + domainID;
      String sql = this.GetSelectStr(new String[] {"A.*"},this.TableName+" A",condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      boolean temp = false;
      if (rs != null && rs.last() && rs.getRow() == 1) {
        rs.updateString(this.Description,description);
        rs.updateString(this.Status,status);
        rs.updateString(this.Message,message);
        rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
        rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
        rs.updateString(this.UpdatedBy,"system");
        rs.updateRow();
        temp = true;
      }
      retBool = temp && this.SaveAllowedWebServices(domainID,isAllowSubmit,isAllowDownload,isAllowQuery,isAllowSolicit,isAllowNotify);
      retBool = temp && this.SaveAdmins(domainID,admins);
    } catch (Exception e) {
      this.LogException("Could Not Save Domain: " + e.toString());
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
   * SaveDomain
   * @param domainName
   * @param description
   * @param status
   * @param message
   * @param isAllowSubmit
   * @param isAllowDownload
   * @param isAllowQuery
   * @param isAllowSolicit
   * @param isAllowNotify
   * @param admins
   * @return boolean
   */
  public boolean SaveDomain (String domainName, String description, String status, String message, boolean isAllowSubmit,
                             boolean isAllowDownload, boolean isAllowQuery, boolean isAllowSolicit, boolean isAllowNotify,
                             String[] admins)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String condition = "A."+this.Name + " = '" + domainName + "'";
      String sql = this.GetSelectStr(new String[] {"A.*"},this.TableName+" A",condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      int newDomainID = -1;
      boolean temp = false;
      if (rs != null && !rs.first()) {
        rs.moveToInsertRow();
        newDomainID = this.GetIncrementID(this.TableName,this.ID);
        rs.updateInt(this.ID,newDomainID);
        rs.updateString(this.Name,domainName);
        rs.updateString(this.Description,description);
        rs.updateString(this.Status,status);
        rs.updateString(this.Message,message);
        rs.updateDate(this.CreatedDate,Utility.GetNowDate());
        rs.updateTimestamp(this.CreatedDate,Utility.GetNowTimeStamp());
        rs.updateString(this.CreatedBy,"system");
        rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
        rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
        rs.updateString(this.UpdatedBy,"system");
        rs.insertRow();
        temp = true;
      }
      temp = newDomainID >= 0 && temp && this.SaveAllowedWebServices(newDomainID,isAllowSubmit,isAllowDownload,isAllowQuery,isAllowSolicit,isAllowNotify);
      retBool = temp && this.SaveAdmins(newDomainID,admins);
    } catch (Exception e) {
      this.LogException("Could Not Save Domain: " + e.toString());
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
   * GetAdmins
   * @param domainNames
   * @return String[]
   */
  public String[] GetAdmins (String[] domainNames)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.SysUserInfoTableName+" A,"+this.NodeAccountTypeTableName+" C,";
      tableNames += this.NodeAccountTypeXREFTableName+" D";
      tableNames += " left join "+this.TableName+" B on D."+this.NodeDomainID+" = B."+this.NodeDomainID;
      String condition = "C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"'";
      condition += " and C."+this.NodeAccountTypeID+" = D."+this.NodeAccountTypeID;
      condition += " and D."+this.SysUserInfoUserID+" = A."+this.SysUserInfoUserID;
      if (domainNames != null && domainNames.length > 0) {
        condition += " and B."+this.Name+" in (";
        for (int i = 0; i < domainNames.length; i++) {
          if (i != 0)
            condition += ", ";
          condition += "'"+domainNames[i]+"'";
        }
        condition += ")";
      }
      String sql = this.GetSelectStr(new String[]{"A."+this.SysUserInfoLoginName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.SysUserInfoLoginName);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Admins: " + e.toString());
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
   * GetAdminEmailAddresses
   * @param domain
   * @return String[]
   */
  public String[] GetAdminEmailAddresses (String domain)
  {
    if (domain == null || domain.trim().equals(""))
      return null;
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select eml." + this.SysEmail;
      sql += " from " + this.TableName + " dom, " + this.NodeAccountTypeXREFTableName + " accXref";
      sql += ", " + this.NodeAccountTypeTableName + " accType, " + this.SysUserInfoTableName + " us";
      sql += ", " + this.SysUserEmailTableName + " userEml, " + this.SysEmailTableName + " eml";
      sql += " where dom." + this.Name + " = '" + domain + "'";
      sql += " and dom." + this.ID + " = accXref." + this.ID;
      sql += " and accXref." + this.NodeAccountTypeID + " = accType." + this.NodeAccountTypeID;
      sql += " and accType." + this.NodeAccountType + " = '" + Phrase.ConsoleUser + "'";
      sql += " and accXref." + this.SysUserInfoUserID + " = us." + this.SysUserInfoUserID;
      sql += " and us." + this.SysUserInfoUserID + " = userEml." + this.SysUserInfoUserID;
      sql += " and userEml." + this.SysEmailID + " = eml." + this.SysEmailID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.SysEmail);
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Eamil Addresses of Admins: " + e.toString());
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
   * GetDomainIDs
   * @param 
   * @return int[]
   */
  public int[] GetDomainIDs ()
  {
    if (this.DomainIDs == null) {
      IDBAdapter db = null;
      ResultSet rs = null;
      try {
        String condition = this.Name+" = '"+Phrase.NODE_DOMAIN+"'";
        String sql = this.GetSelectStr(new String[]{this.Name,this.ID},this.TableName,condition);
        db = this.GetNodeDB();
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() > 0) {
          this.DomainIDs = new int [1];
          rs.beforeFirst();
          while (rs.next()) {
            String name = rs.getString(this.Name);
            if (name.equalsIgnoreCase(Phrase.NODE_DOMAIN))
              this.DomainIDs[0] = rs.getInt(this.ID);
          }
        }
      } catch (Exception e) {
        this.LogException("Could Not Get Domain IDs: " + e.toString());
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
    return this.DomainIDs;
  }

  /**
   * GetNodeStatus
   * @param 
   * @return Status
   */
  public Status GetNodeStatus ()
  {
    Status retStatus = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] {
          "A."+this.Name,"A."+this.Status,"A."+this.Message,"B."+this.OperationName,"B."+this.OperationStatus,
          "B."+this.OperationMessage
      };
      String tableNames = this.TableName+" A, "+this.OperationTableName+" B";
      String condition = "A."+this.ID+" = B."+this.ID+" and B."+this.OperationType+" = '"+Phrase.WEB_SERVICE_OPERATION+"'";
      condition += " and A."+this.Status+" != '"+Phrase.INACTIVE_STATUS+"'";
      condition += " order by A."+this.Name+" asc";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retStatus = new Status();
        rs.beforeFirst();
        HashMap map = new HashMap();
        while (rs.next()) {
          String name = rs.getString(this.Name);
          if (!map.containsKey(name)) {
            ArrayList list = new ArrayList();
            list.add(name);
            list.add(rs.getString(this.Status));
            list.add(rs.getString(this.Message));
            retStatus.AddDomain(list);
            map.put(name,name);
          }
          ArrayList ops = new ArrayList();
          ops.add(name);
          ops.add(rs.getString(this.OperationName));
          ops.add(rs.getString(this.OperationStatus));
          ops.add(rs.getString(this.OperationMessage));
          retStatus.AddOperation(ops);
        }
      }
      if (retStatus == null)
        retStatus = new Status();
      SystemConfiguration configDB = new SystemConfiguration(this.LoggerName);
      retStatus.SetNodeStatus(configDB.GetNodeStatus());
      retStatus.SetNodeMessage(configDB.GetNodeMessage());
    } catch (Exception e) {
      this.LogException("Could Not Get Node Status: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retStatus;
  }

  /**
   * SaveAllowedWebServices
   * @param domainID
   * @param isAllowSubmit
   * @param isAllowDownload
   * @param isAllowQuery
   * @param isAllowSolicit
   * @param isAllowNotify
   * @return boolean
   */
  private boolean SaveAllowedWebServices (int domainID, boolean isAllowSubmit, boolean isAllowDownload, boolean isAllowQuery,
                                          boolean isAllowSolicit, boolean isAllowNotify)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.WebServiceID,"B."+this.WebService };
      String tableNames = this.DomainWSXREFTableName + " A, " + this.WebServiceTableName + " B";
      String condition = "A."+this.ID+" = "+domainID+" and A."+this.WebServiceID+" = B."+this.WebServiceID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      boolean[] needToRemove = new boolean [5]; // [0] = submit, [1] = download, [2] = query, [3] = solicit, [4] = notify
      boolean[] needToAdd = new boolean [5];
      for (int i = 0; i < 5; i++)
        needToAdd[i] = true;
      if (rs != null) {
        rs.beforeFirst();
        while (rs.next()) {
          String temp = rs.getString(this.WebService);
          if (temp != null) {
            if (temp.equalsIgnoreCase("SUBMIT")) {
              if (!isAllowSubmit) {
                needToRemove[0] = true;
                needToAdd[0] = false;
              }
              else
                needToAdd[0] = false;
            }
            if (temp.equalsIgnoreCase("DOWNLOAD")) {
              if (!isAllowDownload) {
                needToRemove[1] = true;
                needToAdd[1] = false;
              }
              else
                needToAdd[1] = false;
            }
            if (temp.equalsIgnoreCase("QUERY")) {
              if (!isAllowQuery) {
                needToRemove[2] = true;
                needToAdd[2] = false;
              }
              else
                needToAdd[2] = false;
            }
            if (temp.equalsIgnoreCase("SOLICIT")) {
              if (!isAllowSolicit) {
                needToRemove[3] = true;
                needToAdd[3] = false;
              }
              else
                needToAdd[3] = false;
            }
            if (temp.equalsIgnoreCase("NOTIFY")) {
              if (!isAllowNotify) {
                needToRemove[4] = true;
                needToAdd[4] = false;
              }
              else
                needToAdd[4] = false;
            }
          }
        }
      }
      sql = this.GetSelectStr(new String[]{"A.*"},this.DomainWSXREFTableName+" A",new String[]{"A."+this.ID},new String[]{""+domainID});
      if (rs != null)
        rs.close();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        NodeWebService wsDB = new NodeWebService(this.LoggerName);
        int[] wsIDs = wsDB.GetWSIDs();
        rs.beforeFirst();
        while (rs.next()) {
          int id = rs.getInt(this.WebServiceID);
          if ((id == wsIDs[0] && needToRemove[0]) || (id == wsIDs[1] && needToRemove[1]) || (id == wsIDs[2] && needToRemove[2]) ||
              (id == wsIDs[3] && needToRemove[3]) || (id == wsIDs[4] && needToRemove[4]))
            rs.deleteRow();
        }
        if (needToAdd[0] && isAllowSubmit) {
          rs.moveToInsertRow();
          rs.updateInt(this.WebServiceID,wsIDs[0]);
          rs.updateInt(this.ID,domainID);
          rs.insertRow();
        }
        if (needToAdd[1] && isAllowDownload) {
          rs.moveToInsertRow();
          rs.updateInt(this.WebServiceID,wsIDs[1]);
          rs.updateInt(this.ID,domainID);
          rs.insertRow();
        }
        if (needToAdd[2] && isAllowQuery) {
          rs.moveToInsertRow();
          rs.updateInt(this.WebServiceID,wsIDs[2]);
          rs.updateInt(this.ID,domainID);
          rs.insertRow();
        }
        if (needToAdd[3] && isAllowSolicit) {
          rs.moveToInsertRow();
          rs.updateInt(this.WebServiceID,wsIDs[3]);
          rs.updateInt(this.ID,domainID);
          rs.insertRow();
        }
        if (needToAdd[4] && isAllowNotify) {
          rs.moveToInsertRow();
          rs.updateInt(this.WebServiceID,wsIDs[4]);
          rs.updateInt(this.ID,domainID);
          rs.insertRow();
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Save Allowed Web Services: " + e.toString());
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
   * SetAllowPermission
   * @param wsID
   * @param d
   * @return 
   */
  private void SetAllowPermission (int wsID, Domain d)
  {
    NodeWebService wsDB = new NodeWebService(this.LoggerName);
    int[] wsIDs = wsDB.GetWSIDs();
    if (wsIDs != null && wsIDs.length == 5) {
      if (wsIDs[0] == wsID)
        d.SetAllowSubmit(true);
      if (wsIDs[1] == wsID)
        d.SetAllowDownload(true);
      if (wsIDs[2] == wsID)
        d.SetAllowQuery(true);
      if (wsIDs[3] == wsID)
        d.SetAllowSolicit(true);
      if (wsIDs[4] == wsID)
        d.SetAllowNotify(true);
    }
  }

  /**
   * SaveAdmins
   * @param domainID
   * @param admins
   * @return boolean
   */
  private boolean SaveAdmins (int domainID, String[] admins)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      if (this.RemoveAdmins(domainID,admins)) {
        if (admins != null && admins.length > 0) {
          String[] select = new String[] { this.SysUserInfoUserID, this.SysUserInfoLoginName};
          String tableNames = this.SysUserInfoTableName;
          String condition = null;
          if (admins != null && admins.length > 0) {
            condition = this.SysUserInfoLoginName + " in (";
            for (int i = 0; i < admins.length; i++) {
              if (i != 0)
                condition += ",";
              condition += "'" + admins[i] + "'";
            }
            condition += ")";
          }
          String sql = this.GetSelectStr(select, tableNames, condition);
          db = this.GetNodeDB();
          rs = db.GetResultSet(sql);
          HashMap map = new HashMap();
          if (rs != null) {
            rs.beforeFirst();
            while (rs.next())
              map.put(rs.getString(this.SysUserInfoLoginName),rs.getString(this.SysUserInfoUserID));
          }
          select = new String[] { "B." + this.SysUserInfoLoginName};
          tableNames = this.NodeAccountTypeXREFTableName+" A,"+this.SysUserInfoTableName+" B,"+this.NodeAccountTypeTableName+" C";
          condition = "A." + this.ID+" = "+domainID+" and A."+this.SysUserInfoUserID + " = B." + this.SysUserInfoUserID;
          condition += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"' and C."+this.NodeAccountTypeID+" = A."+this.NodeAccountTypeID;
          condition += " and B." + this.SysUserInfoLoginName + " in (";
          for (int i = 0; i < admins.length; i++) {
            if (i != 0)
              condition += ",";
            condition += "'" + admins[i] + "'";
          }
          condition += ")";
          sql = this.GetSelectStr(select, tableNames, condition);
          db = this.GetNodeDB();
          rs = db.GetResultSet(sql);
          if (rs != null) {
            rs.beforeFirst();
            while (rs.next())
              map.remove(rs.getString(this.SysUserInfoLoginName));
          }
          if (!map.isEmpty()) {
            sql = this.GetSelectStr(new String[] {"A.*"},this.NodeAccountTypeXREFTableName+" A","-1 = 1");
            if (rs != null)
              rs.close();
            rs = db.GetResultSet(sql);
            if (rs != null) {
              Iterator iter = map.values().iterator();
              NodeAccountType typeDB = new NodeAccountType(this.LoggerName);
              int consoleID = typeDB.GetAccountTypeIDs()[0];
              while (iter.hasNext()) {
                rs.moveToInsertRow();
                int id = this.GetIncrementID(this.NodeAccountTypeXREFTableName,this.AccountTypeXREFID);
                rs.updateInt(this.AccountTypeXREFID, id);
                rs.updateInt(this.NodeAccountTypeID,consoleID);
                rs.updateInt(this.SysUserInfoUserID,Integer.parseInt((String)iter.next()));
                rs.updateInt(this.NodeDomainID,domainID);
                rs.insertRow();
              }
            }
          }
        }
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Save Domain Admins: " + e.toString());
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
   * RemoveAdmins
   * @param domainID
   * @param admins
   * @return boolean
   */
  private boolean RemoveAdmins (int domainID, String[] admins)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String [] { "unique A."+this.SysUserInfoUserID };
      String tableNames = this.NodeAccountTypeXREFTableName+" A,"+this.SysUserInfoTableName+" B, "+this.NodeAccountTypeTableName+" C";
      String condition = "A."+this.ID+" != "+domainID+" and A."+this.SysUserInfoUserID+" = B."+this.SysUserInfoUserID;
      condition += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"' and C."+this.NodeAccountTypeID+" = A."+this.NodeAccountTypeID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      HashMap moreThanOne = new HashMap();
      if (rs != null) {
        rs.beforeFirst();
        while (rs.next()) {
          String id = rs.getString(this.SysUserInfoUserID);
          moreThanOne.put(id,id);
        }
      }
      select = new String [] { "A."+this.AccountTypeXREFID };
      tableNames = this.NodeAccountTypeXREFTableName+" A,"+this.SysUserInfoTableName+" B, "+this.NodeAccountTypeTableName+" C";
      condition = "A."+this.ID+" = "+domainID+" and A."+this.SysUserInfoUserID+" = B."+this.SysUserInfoUserID;
      condition += " and C."+this.NodeAccountType+" = '"+Phrase.ConsoleUser+"' and C."+this.NodeAccountTypeID+" = A."+this.NodeAccountTypeID;
      if (admins != null && admins.length > 0) {
        condition += " and not B."+this.SysUserInfoLoginName+" in (";
        for (int i = 0; i < admins.length; i++) {
          if (i != 0)
            condition += ",";
          condition += "'"+admins[i]+"'";
        }
        condition += ")";
      }
      sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);

      int[] removeInts = null;
      if (rs != null && rs.last() && rs.getRow() > 0) {
        removeInts = new int [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < removeInts.length; i++)
          removeInts[i] = rs.getInt(this.AccountTypeXREFID);
      }
      if (removeInts != null && removeInts.length > 0) {
        condition = "A."+this.AccountTypeXREFID+" in (";
        for (int i = 0; i < removeInts.length; i++) {
          if (i != 0)
            condition += ", ";
          condition += ""+removeInts[i];
        }
        condition += ")";
        sql = this.GetSelectStr(new String[]{"A.*"},this.NodeAccountTypeXREFTableName+" A",condition);
        if (rs != null)
          rs.close();
        rs = db.GetResultSet(sql);
        if (rs != null) {
          rs.beforeFirst();
          while (rs.next()) {
            String id = rs.getString(this.SysUserInfoUserID);
            if (moreThanOne.containsKey(id))
              rs.deleteRow();
            else {
              rs.updateObject(this.ID,null);
       //       rs.updateInt(this.ID,null);
              rs.updateRow();
            }
          }
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Remove Admins: " + e.toString());
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
