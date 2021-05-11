package Node.DB.Oracle;

import java.sql.Date;
import java.sql.ResultSet;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.HashMap;
import java.util.TimeZone;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.OperationLog;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.Utility;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create NodeOperationLog.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeOperationLog extends NodeDB implements INodeOperationLog {
  // Table Name
  private String TableName = this.OperationLogTableName;

  // Column Names
  private String OpLogID = "OPERATION_LOG_ID";
  private String TransID = "TRANS_ID";
  private String OpID = "OPERATION_ID";
  private String UserName = "USER_NAME";
  private String RequestorIP = "REQUESTOR_IP";
  private String SuppliedTransID = "SUPPLIED_TRANS_ID";
  private String Token = "TOKEN";
  private String NodeAddress = "NODE_ADDRESS";
  private String ReturnURL = "RETURN_URL";
  private String ServiceType = "SERVICE_TYPE";
  private String StartDate = "START_DTTM";
  private String EndDate = "END_DTTM";
  private String HostName = "HOST_NAME";
  private String CreatedBy = "CREATED_BY";
  private String CreatedDate = "CREATED_DTTM";
  private String UpdatedBy = "UPDATED_BY";
  private String UpdatedDate = "UPDATED_DTTM";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeOperationLog(String loggerName) {
    super(loggerName);
  }

  /**
   * CreateOperationLog
   * @param transID
   * @param operID
   * @param userName
   * @param status
   * @param message
   * @param requestorIP
   * @param suppliedTransID
   * @param token
   * @param nodeAddress
   * @param returnURL
   * @param serviceType
   * @param hostName
   * @param paramNames
   * @param paramValues
   * @return int
   */
  public int CreateOperationLog (String transID, int operID, String userName, String status, String message, String requestorIP,
                                 String suppliedTransID, String token, String nodeAddress, String returnURL, String serviceType,
                                 String hostName,String[] paramNames,Object[] paramValues)
   {
	 IDBAdapter db = null;
     ResultSet rs = null;
     int retInt = -1;
     try {
       String sql = this.GetSelectStr(new String[]{this.OpLogID},this.TableName,new String[]{this.TransID},new String[]{transID});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null) {
         if (!rs.first()) {
           rs.close();
           String[] select = new String[]{
               this.OpLogID,this.TransID,this.OpID,this.UserName,this.RequestorIP,this.SuppliedTransID,this.Token,this.NodeAddress,
               this.ReturnURL,this.ServiceType,this.StartDate,this.HostName,this.CreatedBy,this.CreatedDate,this.UpdatedBy,
               this.UpdatedDate
           };
           sql = this.GetSelectStr(select,this.TableName,new String[]{this.OpLogID},new String[]{"-99999"});
           rs = db.GetResultSet(sql);
           if (rs != null) {
             int newID = this.GetIncrementID(this.TableName,this.OpLogID);
             if (newID >= 0) {
               rs.moveToInsertRow();
               rs.updateInt(this.OpLogID, newID); rs.updateString(this.TransID,transID); rs.updateInt(this.OpID, operID);
               if (userName != null)
                 rs.updateString(this.UserName,userName);
               else
                 rs.updateString(this.UserName, null);
               rs.updateString(this.RequestorIP,requestorIP); rs.updateString(this.SuppliedTransID,suppliedTransID);
               rs.updateString(this.Token,token); rs.updateString(this.NodeAddress,nodeAddress);
               rs.updateString(this.ReturnURL,returnURL); rs.updateString(this.ServiceType,serviceType);
               rs.updateDate(this.StartDate,Utility.GetNowDate()); rs.updateTimestamp(this.StartDate, Utility.GetNowTimeStamp());
               rs.updateString(this.HostName,hostName);
               rs.updateDate(this.CreatedDate,Utility.GetNowDate()); rs.updateTimestamp(this.CreatedDate,Utility.GetNowTimeStamp());
               rs.updateString(this.CreatedBy,"system"); rs.updateString(this.UpdatedBy,"system");
               rs.updateDate(this.UpdatedDate,Utility.GetNowDate()); rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
               rs.insertRow();
               retInt = newID;
               NodeOperationParameter paramDB = new NodeOperationParameter(this.LoggerName);
               paramDB.UpdateParameterValues(newID, paramNames, paramValues);
               NodeOperationLogStatus statusDB = new NodeOperationLogStatus(this.LoggerName);
               statusDB.CreateLogStatus(newID, status, message);
             }
           }
         }
       }
     } catch (Exception e) {
       this.LogException("Could Not Update Operation Log: " + e.toString());
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
   * UpdateOperationLog
   * @param operLogID
   * @param status
   * @param message
   * @param isLastUpdate
   * @param isDebug
   * @return int
   */
   public int UpdateOperationLog (int operLogID, String status, String message, boolean isLastUpdate, boolean isDebug){
	   int ret = -1;
 	   if(isDebug){
 		   try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			if(config.GetAdministrationLogLevel().toString().equalsIgnoreCase(Level.DEBUG.toString()) ){
				   return (this.UpdateOperationLog (operLogID,  status,  message,  isLastUpdate));
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
 	   }
 	   return ret;
   }

   /**
    * UpdateOperationLog
    * @param operLogID
    * @param status
    * @param message
    * @param isLastUpdate
    * @param isDebug
    * @return int
    */
   public int UpdateOperationLog (String transID, String status, String message, boolean isLastUpdate, boolean isDebug){
	   int ret = -1;
 	   if(isDebug){
 		   try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			if(config.GetAdministrationLogLevel().toString().equalsIgnoreCase(Level.DEBUG.toString()) ){
				ret = this.UpdateOperationLog ( transID,  status,  message,  isLastUpdate);
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
 	   }
 	   return ret;
    }

    /**
   * UpdateOperationLog
   * @param operLogID
   * @param status
   * @param message
   * @param isLastUpdate
   * @return int
   */
   public int UpdateOperationLog (int operLogID, String status, String message, boolean isLastUpdate)
   {
     IDBAdapter db = null;
     ResultSet rs = null;
     int retInt = -1;
     try {
       String[] select = new String[]{
           this.EndDate,this.UpdatedBy,this.UpdatedDate
       };
       String sql = this.GetSelectStr(select,this.TableName,new String[]{this.OpLogID},new String[]{operLogID+""});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1) {
         NodeOperationLogStatus statusDB = new NodeOperationLogStatus(this.LoggerName);
         statusDB.CreateLogStatus(operLogID,status,message);
         if (isLastUpdate) {
           rs.updateDate(this.EndDate,Utility.GetNowDate()); rs.updateTimestamp(this.EndDate,Utility.GetNowTimeStamp());
         }
         rs.updateDate(this.UpdatedDate,Utility.GetNowDate()); rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
         rs.updateString(this.UpdatedBy,"system");
         rs.updateRow();
         retInt = operLogID;
       }
     } catch (Exception e) {
       this.LogException("Could Not Update Operation Log: " + e.toString());
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
    * UpdateOperationLog
    * @param transID
    * @param status
    * @param message
    * @param isLastUpdate
    * @return int
    */
   public int UpdateOperationLog (String transID, String status, String message, boolean isLastUpdate)
   {
     IDBAdapter db = null;
     ResultSet rs = null;
     int retInt = -1;
     try {
       String[] select = new String[]{
           this.OpLogID,this.EndDate,this.UpdatedBy,this.UpdatedDate
       };
       String sql = this.GetSelectStr(select,this.TableName,new String[]{this.TransID},new String[]{transID});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1) {
         int opLogID = rs.getInt(this.OpLogID);
         NodeOperationLogStatus statusDB = new NodeOperationLogStatus(this.LoggerName);
         statusDB.CreateLogStatus(opLogID,status,message);
         if (isLastUpdate) {
           rs.updateDate(this.EndDate,Utility.GetNowDate()); rs.updateTimestamp(this.EndDate,Utility.GetNowTimeStamp());
         }
         rs.updateDate(this.UpdatedDate,Utility.GetNowDate()); rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
         rs.updateString(this.UpdatedBy,"system");
         rs.updateRow();
         retInt = opLogID;
       }
     } catch (Exception e) {
       this.LogException("Could Not Update Operation Log: " + e.toString());
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
    * UpdateOperationLogUserName
    * @param transID
    * @param userName
    * @return int
    */
   public int UpdateOperationLogUserName (String transID, String userName)
   {
     int retInt = -1;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[] { this.OpLogID,this.UserName,this.UpdatedDate,this.UpdatedBy };
       String sql = this.GetSelectStr(select,this.TableName,new String[]{this.TransID},new String[]{transID});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1) {
         int opLogID = rs.getInt(this.OpLogID);
         rs.updateString(this.UserName,userName);
         rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
         rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
         rs.updateString(this.UpdatedBy,"system");
         rs.updateRow();
         retInt = opLogID;
       }
     } catch (Exception e) {
       this.LogException("Could Not Update Operation Log: " + e.toString());
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
    * UpdateOperationLogToken
    * @param transID
    * @param token
    * @return int
    */
   public int UpdateOperationLogToken (String transID, String token)
   {
     int retInt = -1;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[] { this.OpLogID,this.Token,this.UpdatedDate,this.UpdatedBy };
       String sql = this.GetSelectStr(select,this.TableName,new String[]{this.TransID},new String[]{transID});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1) {
         int opLogID = rs.getInt(this.OpLogID);
         rs.updateString(this.Token,token);
         rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
         rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
         rs.updateString(this.UpdatedBy,"system");
         rs.updateRow();
         retInt = opLogID;
       }
     } catch (Exception e) {
       this.LogException("Could Not Update Operation Log: " + e.toString());
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
    * GetStatus
    * @param transID
    * @return String
    */
   public String GetStatus (String transID)
   {
     String retString = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select A."+this.OperationLogStatus;
       sql += " from "+this.OperationLogStatusTableName+" A,"+this.TableName+" B";
       sql += " where B."+this.TransID+" = '"+transID+"' and B."+this.OpLogID+" = A."+this.OpLogID;
       sql += " and A."+this.OperationLogStatusID+" = (select max(A1."+this.OperationLogStatusID+")";
       sql += " from "+this.OperationLogStatusTableName+" A1 where A1."+this.OpLogID+" = B."+this.OpLogID+")";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1)
         retString = rs.getString(this.OperationLogStatus);
       /*
       String sql = this.GetSelectStr(new String[]{this.OpLogID},this.TableName,new String[]{this.TransID},new String[]{transID});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1) {
         int opLogID = rs.getInt(this.OpLogID);
         if (opLogID >= 0) {
           NodeOperationLogStatus statusDB = new NodeOperationLogStatus(this.LoggerName);
           retString = statusDB.GetLatestStatus(opLogID);
         }
       }
     */
     } catch (Exception e) {
       this.LogException("Could Not Get Transaction Status: " + e.toString());
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
    * AuthorizeToken
    * @param token
    * @param opID
    * @return String
    */
   public String AuthorizeToken (String token, int opID)
   {
     String retString = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[] { "A."+this.UserName };
       String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B,"+this.OperationTableName+" C,";
       tableNames += this.SysUserInfoTableName+" D,"+this.UserOpXREFTableName+" E";
       String condition = "A."+this.Token+" = '"+token+"'";
       condition += " and B."+this.WebService+" = '"+Phrase.WEB_METHOD_AUTHENTICATE+"'";
       condition += " and B."+this.WebServiceID+" = C."+this.WebServiceID+" and A."+this.OpID+" = C."+this.OpID;
       condition += " and A."+this.UserName+" = D."+this.SysUserInfoLoginName;
       condition += " and D."+this.SysUserInfoUserID+" = E."+this.SysUserInfoUserID;
       condition += " and E."+this.OpID+" = "+opID;
       String sql = this.GetSelectStr(select,tableNames,condition);
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first())
         retString = rs.getString(this.UserName);
     } catch (Exception e) {
       this.LogException("Could Not Authorize Token: " + e.toString());
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
    * AuthorizeToken
    * @param token
    * @param webMethod
    * @param opName
    * @return String
    */
   public String AuthorizeToken (String token, String webMethod, String opName)
   {
     String retString = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select A." + this.UserName;
       sql += " from " + this.TableName + " A, " + this.SysUserInfoTableName + " B, " + this.OperationTableName + " C";
       sql += ", " + this.WebServiceTableName + " D, " + this.OperationTableName + " E";
       sql += ", " + this.WebServiceTableName + " F, " + this.UserOpXREFTableName + " G";
       sql += " where A."+this.Token+" = '"+token+"'";
       sql += " and A."+this.UserName+" = B."+this.SysUserInfoLoginName;
       sql += " and A."+this.OpID+" = C."+this.OpID+" and C."+this.WebServiceID+" = D."+this.WebServiceID;
       sql += " and upper(D."+this.WebService+") = '"+Phrase.WEB_METHOD_AUTHENTICATE+"'";
       sql += " and B."+this.SysUserInfoUserID+" = G."+this.SysUserInfoUserID;
       sql += " and upper(E." + this.OperationName + ") = '" + opName.toUpperCase() + "'";
       sql += " and upper(F." + this.WebService + ") = '" + webMethod.toUpperCase() + "'";
       sql += " and E." + this.WebServiceID + " = F." + this.WebServiceID;
       sql += " and E."+this.OpID+" = G." + this.OpID;
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first())
         retString = rs.getString(this.UserName);
     } catch (Exception e) {
       this.LogException("Could Not Authorize Token: " + e.toString());
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
    * GetLastStartTime
    * @param opID
    * @return String
    */
   public Calendar GetLastStartTime (int opID)
   {
     Calendar retCal = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = this.GetSelectStr(new String[]{this.CreatedDate},this.TableName,new String[]{this.OpID},new String[]{opID+""},"order by " + this.CreatedDate + " desc");
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first()) {
         Date date = rs.getDate(this.CreatedDate);
         if (date != null) {
           retCal = new GregorianCalendar();
           retCal.setTime(date);
         }
       }
     } catch (Exception e) {
       this.LogException("Could Not Get Last Start Time: " + e.toString());
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
     return retCal;
   }
//added by charlie zhang 2007-9-20 begin
   /**
    * GetAccurateLastStartTime
    * @param opID
    * @return Calendar
    */
   public Calendar GetAccurateLastStartTime (int opID)
   {
     Calendar retCal = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select CREATED_DTTM from NODE_OPERATION_LOG where OPERATION_ID = '" +opID+ "' order by CREATED_DTTM desc";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first()) {
         Timestamp createdDate = rs.getTimestamp("CREATED_DTTM");
         if (createdDate != null) {
           Node.Utils.LoggingUtils.Log("NodeOperationLog>>>GetAccurateLastStartTime>>>LastStartTime is: " + new Date(createdDate.getTime()), Level.DEBUG
                            , Phrase.TaskLoggerName);

           //SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
           retCal = new GregorianCalendar();
           retCal.setTime(new Date(createdDate.getTime()));
         }
       }
     } catch (Exception e) {
       this.LogException("Could Not Get Last Start Time: " + e.toString());
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
     return retCal;
   }
// added by charlie zhang 2007-9-20 end
   /**
    * SearchOperationLog
    * @param opName
    * @param opType
    * @param wsName
    * @param status
    * @param domains
    * @param userName
    * @param token
    * @param transID
    * @param start
    * @param end
    * @param version_no
    * @return OperationLog[]
    */
   public OperationLog[] SearchOperationLog (String opName, String opType, String wsName, String status, String[] domains,
                                             String userName, String token, String transID, Date start, Date end, String version_no)
   {
	   if (version_no == null || version_no.equals(""))
		   version_no = "VER_11";
	     
     OperationLog[] retArray = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select A."+this.OpLogID+",B."+this.OperationName+",B."+this.OperationType+",C."+this.WebService+",D."+this.NodeDomain;
       sql += ",A."+this.UserName+",A."+this.TransID+",E."+this.OperationLogStatus+",A."+this.StartDate+",A."+this.EndDate;
       sql += " from "+this.TableName+" A,"+this.OperationTableName+" B,"+this.WebServiceTableName+" C,";
       sql += this.NodeDomainTableName+" D,"+this.OperationLogStatusTableName+" E";
       sql += " where A."+this.OperationID+" = B."+this.OperationID+" and A."+this.OpLogID+" = E."+this.OpLogID;
       sql += " and B."+this.WebServiceID+" = C."+this.WebServiceID+" (+) and B."+this.NodeDomainID+" = D."+this.NodeDomainID;
       sql += " and B."+this.VersionNo+" = '"+version_no+"'";//Added by Helen 20081030
       if (opName != null)
         sql += " and B."+this.OperationName+" = '"+opName+"'";
       if (opType != null)
         sql += " and B." + this.OperationType + " = '" + opType + "'";
       if (wsName != null)
         sql += " and C."+this.WebService+" = '"+wsName+"'";
       if (domains != null && domains.length > 0) {
         sql += " and (D."+this.NodeDomain+" in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += ")";
         sql += " or (C." + this.WebService + " = '" + Phrase.WEB_METHOD_AUTHENTICATE + "'";
         sql += " and A." + this.Token + " in (select F." + this.Token + " from " + this.TableName + " F";
         sql += ", " + this.OperationTableName + " G, " + this.NodeDomainTableName + " H";
         sql += " where F." + this.OperationID + " = G." + this.OperationID + " and G." + this.NodeDomainID + " = H." + this.NodeDomainID;
         sql += " and H." + this.NodeDomain + " in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += ")))";
         sql += " or (C." + this.WebService + " = '" + Phrase.WEB_METHOD_GETSERVICES + "'";
         sql += " and A.SERVICE_TYPE in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += "))";
         sql += " or (C." + this.WebService + " = '" + Phrase.WEB_METHOD_GETSTATUS + "'";
         sql += " and A." + this.SuppliedTransID + " in (select I." + this.TransID + " from " + this.TableName + " I";
         sql += ", " + this.OperationTableName + " J, " + this.NodeDomainTableName + " K";
         sql += " where I." + this.OperationID + " = J." + this.OperationID + " and J." + this.NodeDomainID + " = K." + this.NodeDomainID;
         sql += " and K." + this.NodeDomain + " in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += ")))";
         sql += ")";
       }
       if (userName != null)
         sql += " and upper(A."+this.UserName+") like upper('%"+userName+"%')";
       if (token != null)
         sql += " and A."+this.Token+" like '%"+token+"%'";
       if (transID != null)
         sql += " and (A."+this.TransID+" like '%"+transID+"%' or A."+this.SuppliedTransID+" like '%"+transID+"%')";
       SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
       if (start != null)
         sql += " and (A."+this.StartDate+" > '"+dateFormat.format(start)+"' or A."+this.EndDate+" > '"+dateFormat.format(start)+"')";
       if (end != null) {
         Calendar cal = Calendar.getInstance(TimeZone.getDefault());
         cal.setTime(end);
         cal.add(Calendar.DAY_OF_MONTH,1);
         java.util.Date searchEnd = cal.getTime();
         sql += " and (A."+this.StartDate+" < '"+dateFormat.format(searchEnd)+"' or A."+this.EndDate+" < '"+dateFormat.format(searchEnd)+"')";
       }
       sql += " and E."+this.OperationLogStatusID+" = (select max(E2."+this.OperationLogStatusID+")";
       sql += " from "+this.OperationLogStatusTableName+" E2 where E2."+this.OpLogID+" = E."+this.OpLogID+")";
       if (status != null && !status.equals(""))
         sql += " and E."+this.OperationLogStatus+" = '"+status+"'";
       sql += " and B."+this.VersionNo+" = '"+version_no+"'";
       sql += " order by A."+this.OpLogID+" desc";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() > 0) {
         retArray = new OperationLog[rs.getRow()];
         rs.beforeFirst();
         for (int i = 0; rs.next() && i < retArray.length; i++) {
           OperationLog temp = new OperationLog(rs.getInt(this.OpLogID));
           temp.SetOperationName(rs.getString(this.OperationName));
           temp.SetOperationType(rs.getString(this.OperationType));
           temp.SetDomain(rs.getString(this.NodeDomain));
           temp.SetUserName(rs.getString(this.UserName));
           temp.SetTransID(rs.getString(this.TransID));
           ArrayList statusList = new ArrayList();
           ArrayList tempList = new ArrayList();
           tempList.add(rs.getString(this.OperationLogStatus));
           statusList.add(tempList);
           temp.SetStatus(statusList);
           temp.SetStartDate(rs.getString(this.StartDate));
           temp.SetEndDate(rs.getString(this.EndDate));
           if (temp.GetOperationType() != null)
             if (temp.GetOperationType().equals(Phrase.WEB_SERVICE_OPERATION))
               temp.SetWebServiceName(rs.getString(this.WebService));
           retArray[i] = temp;
         }
       }
     } catch (Exception e) {
       this.LogException("Could Not Search Operation Log: " + e.toString());
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
    * GetUniqueStatusList
    * @param domains
    * @return String[]
    */
   public String[] GetUniqueStatusList (String[] domains)
   {
     String[] retArray = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select unique(A."+this.OperationLogStatus+")";
       sql += " from "+this.OperationLogStatusTableName+" A,"+this.TableName+" B,"+this.OperationTableName+" C,";
       sql += this.NodeDomainTableName+" D";
       sql += " where A."+this.OpLogID+" = B."+this.OpLogID+" and B."+this.OperationID+" = C."+this.OperationID;
       sql += " and C."+this.NodeDomainID+" = D."+this.NodeDomainID;
       if (domains != null && domains.length > 0) {
         sql += " and D."+this.NodeDomain+" in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += ")";
       }
       // WI 19619
       sql += " ORDER BY upper(A." + this.OperationLogStatus + ") ASC ";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() > 0) {
         retArray = new String[rs.getRow()];
         rs.beforeFirst();
         for (int i = 0; rs.next() && i < retArray.length; i++)
           retArray[i] = rs.getString(this.OperationLogStatus);
       }
     } catch (Exception e) {
       this.LogException("Could Not Get Unique Status: " + e.toString());
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
    * GetUniqueOperationNameList
    * @param domains
    * @return String[]
    */
   public String[] GetUniqueOperationNameList (String[] domains)
   {
     String[] retArray = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select unique(A."+this.OperationName+")";
       sql += " from "+this.OperationTableName+" A,"+this.NodeDomainTableName+" B";
       sql += " where A."+this.NodeDomainID+" = B."+this.NodeDomainID;
       if (domains != null && domains.length > 0) {
         sql += " and B."+this.NodeDomain+" in (";
         for (int i = 0; i < domains.length; i++) {
           if (i != 0) sql += ",";
           sql += "'"+domains[i]+"'";
         }
         sql += ")";
       }
       // WI 19619
       sql += " ORDER BY upper(A." + this.OperationName + ") ASC ";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() > 0) {
         retArray = new String[rs.getRow()];
         rs.beforeFirst();
         for (int i = 0; rs.next() && i < retArray.length; i++)
           retArray[i] = rs.getString(this.OperationName);
       }
     } catch (Exception e) {
       this.LogException("Could Not Get Unique Operation Names: " + e.toString());
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
    * GetOperationLog
    * @param opLogID
    * @return OperationLog
    */
   public OperationLog GetOperationLog (int opLogID)
   {
     return this.GetOperationLog(opLogID,null);
   }

   /**
    * GetOperationLog
    * @param transID
    * @return OperationLog
    */
   public OperationLog GetOperationLog (String transID)
   {
     return this.GetOperationLog(-1,transID);
   }

   /**
    * GetOperationLog
    * @param opLogID
    * @param transID
    * @return OperationLog
    */
   private OperationLog GetOperationLog (int opLogID, String transID)
   {
     if (opLogID < 0 && transID == null)
       return null;
     OperationLog retLog = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select A.*,B."+this.OperationName+",B."+this.OperationType+",C."+this.WebService+",D."+this.NodeDomain;
       sql += ",E."+this.OperationLogStatusID+",E."+this.OperationLogStatus+",E."+this.OperationLogStatusMsg+",E."+this.CreatedDate+" STATUS_DATE,E.";
       sql += this.CreatedBy+" STATUS_BY,F."+this.OperationLogParamName+",F."+this.OperationLogParamValue;
       sql += " from "+this.TableName+" A";
       sql += " left join "+this.OperationTableName+" B on A."+this.OperationID+" = B."+this.OperationID;
       sql += " left join "+this.NodeDomainTableName+" D on B."+this.NodeDomainID+" = D."+this.NodeDomainID;
       sql += " left join "+this.OperationLogStatusTableName+" E on A."+this.OpLogID+" = E."+this.OpLogID;
       sql += " left join "+this.OperationLogParameterTableName+" F on A."+this.OpLogID+" = F."+this.OpLogID;
       sql += " left join "+this.WebServiceTableName+" C on B."+this.WebServiceID+" = C."+this.WebServiceID;
       if (opLogID >= 0)
         sql += " where A."+this.OpLogID+" = "+opLogID;
       else if (transID != null)
         sql += " where A."+this.TransID+" = '"+transID+"'";
       sql += " order by E."+this.OperationLogStatusID+" desc";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null) {
         HashMap logs = new HashMap();
         HashMap stati = new HashMap();
         HashMap params = new HashMap();
         ArrayList statiList = new ArrayList();
         ArrayList paramsList = new ArrayList();
         rs.beforeFirst();
         while (rs.next()) {
           int id = rs.getInt(this.OpLogID);
           if (!logs.containsKey(id+"")) {
             OperationLog temp = new OperationLog(id);
             temp.SetTransID(rs.getString(this.TransID));
             temp.SetOperationName(rs.getString(this.OperationName));
             temp.SetOperationType(rs.getString(this.OperationType));
             temp.SetDomain(rs.getString(this.NodeDomain));
             temp.SetUserName(rs.getString(this.UserName));
             temp.SetStartDate(rs.getString(this.StartDate));
             temp.SetEndDate(rs.getString(this.EndDate));
             temp.SetHostName(rs.getString(this.HostName));
             temp.SetCreatedDate(rs.getString(this.CreatedDate));
             temp.SetCreatedBy(rs.getString(this.CreatedBy));
             temp.SetUpdatedDate(rs.getString(this.UpdatedDate));
             temp.SetUpdatedBy(rs.getString(this.UpdatedBy));
             if (temp.GetOperationType() != null && temp.GetOperationType().equals(Phrase.WEB_SERVICE_OPERATION)) {
               temp.SetWebServiceName(rs.getString(this.WebService));
               temp.SetRequestorIP(rs.getString(this.RequestorIP));
               temp.SetSupplTransID(rs.getString(this.SuppliedTransID));
               temp.SetToken(rs.getString(this.Token));
               temp.SetNodeAddress(rs.getString(this.NodeAddress));
               temp.SetReturnURL(rs.getString(this.ReturnURL));
               temp.SetServiceType(rs.getString(this.ServiceType));
             }
             ArrayList tempStatus = new ArrayList();
             tempStatus.add(rs.getString(this.OperationLogStatus));
             tempStatus.add(rs.getString(this.OperationLogStatusMsg));
             tempStatus.add(rs.getString("STATUS_DATE"));
             tempStatus.add(rs.getString("STATUS_BY"));
             statiList.add(tempStatus);
             stati.put(rs.getString(this.OperationLogStatusID),tempStatus);
             ArrayList tempParam = new ArrayList();
             String name = rs.getString(this.OperationLogParamName);
             tempParam.add(name);
             tempParam.add(rs.getString(this.OperationLogParamValue));
             paramsList.add(tempParam);
             params.put(name,tempParam);
             logs.put(id+"",temp);
           }
           else {
             String statusID = rs.getString(this.OperationLogStatusID);
             if (!stati.containsKey(statusID)) {
               ArrayList tempStatus = new ArrayList();
               tempStatus.add(rs.getString(this.OperationLogStatus));
               tempStatus.add(rs.getString(this.OperationLogStatusMsg));
               tempStatus.add(rs.getString("STATUS_DATE"));
               tempStatus.add(rs.getString("STATUS_BY"));
               statiList.add(tempStatus);
               stati.put(rs.getString(this.OperationLogStatusID),tempStatus);
             }
             String name = rs.getString(this.OperationLogParamName);
             if (!params.containsKey(name)) {
               ArrayList tempParam = new ArrayList();
               tempParam.add(name);
               tempParam.add(rs.getString(this.OperationLogParamValue));
               paramsList.add(tempParam);
               params.put(name,tempParam);
             }
           }
         }
         if (!logs.isEmpty() && logs.size() == 1) {
           retLog = (OperationLog)logs.values().toArray()[0];
           if (statiList.size() > 0)
             retLog.SetStatus(statiList);
           if (paramsList.size() > 0)
             retLog.SetParameters(paramsList);
         }
       }
     } catch (Exception e) {
       this.LogException("Could Not Get Operation Log: " + e.toString());
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
     return retLog;
   }

   /**
    * GetUserName
    * @param token
    * @return String
    */
   public String GetUserName (String token)
   {
     String retString = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[] { "A."+this.UserName };
       String tableNames = this.TableName+" A, "+this.OperationTableName+" B, "+this.WebServiceTableName+" C";
       String condition = "A."+this.Token+" = '"+token+"' and A."+this.OpID+" = B."+this.OpID;
       condition += " and B."+this.WebServiceID+" = C."+this.WebServiceID;
       condition += " and C."+this.WebService+" = '"+Phrase.WEB_METHOD_AUTHENTICATE+"'";
       String sql = this.GetSelectStr(select,tableNames,condition);
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first())
         retString = rs.getString(this.UserName);
     } catch (Exception e) {
       this.LogException("Could Not Get User Name: " + e.toString());
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
    * GetPassword
    * @param token
    * @return String
    */
   public String GetPassword (String token)
   {
     String retString = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[] { "D."+this.OperationLogParamValue };
       String tableNames = this.TableName+" A, "+this.OperationTableName+" B, "+this.WebServiceTableName+" C";
       tableNames += ", " + this.OperationLogParameterTableName + " D";
       String condition = "A."+this.Token+" = '"+token+"' and A."+this.OpID+" = B."+this.OpID;
       condition += " and B."+this.WebServiceID+" = C."+this.WebServiceID;
       condition += " and C."+this.WebService+" = '"+Phrase.WEB_METHOD_AUTHENTICATE+"'";
       condition += " and A." + this.OpLogID + " = D." + this.OpLogID;
       condition += " and D." + this.OperationLogParamName + " = '" + Phrase.ParamCredential + "'";
       String sql = this.GetSelectStr(select,tableNames,condition);
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.first())
         retString = rs.getString(this.OperationLogParamValue);
     } catch (Exception e) {
       this.LogException("Could Not Get Password: " + e.toString());
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
    * GetAuthenticationOperation
    * @param token
    * @return Operation
    */
   public Operation GetAuthenticationOperation (String token)
   {
     Operation retOp = null;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String sql = "select A." + this.OpID;
       sql += " from " + this.TableName + " A, " + this.OperationTableName + " B, " + this.WebServiceTableName + " C";
       sql += " where A." + this.Token + " = '" + token + "'";
       sql += " and A." + this.OpID + " = B." + this.OpID + " and B." + this.WebServiceID + " = C." + this.WebServiceID;
       sql += " and C." + this.WebService + " = '" + Phrase.WEB_METHOD_AUTHENTICATE + "'";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       NodeOperation opDB = new NodeOperation(this.LoggerName);
       if (rs != null && rs.first())
         retOp = opDB.GetOperation(rs.getInt(this.OpID));
       else
         retOp = opDB.GetOperation("PASSWORD", Phrase.WEB_METHOD_AUTHENTICATE);
     } catch (Exception e) {
       this.LogException("Could Not Get Authentication Operation: " + e.toString());
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
     return retOp;
   }
   
   /**
    * hasOperationLog
    * @param opID
    * @return boolean
    */
   public boolean hasOperationLog (int opID)
   {
     IDBAdapter db = null;
     ResultSet rs = null;
     boolean ret = false;
     int retInt = 0;
     try {
       String sql = "select count(*) a from NODE_OPERATION_LOG A where A.OPERATION_ID = '"+opID + "'";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1)
         retInt = rs.getInt("a");
     } catch (Exception e) {
       e.printStackTrace();
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
     if(retInt>0){
    	 ret = true;
     }
     return ret;
   }
   
   /**
    * hasOperationLog
    * @param username
    * @return boolean
    */
   public boolean hasOperationLog (String username)
   {
     IDBAdapter db = null;
     ResultSet rs = null;
     boolean ret = false;
     int retInt = 0;
     try {
       String sql = "select count(*) a from NODE_OPERATION_LOG A where A.USER_NAME = '"+username + "'";
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null && rs.last() && rs.getRow() == 1)
         retInt = rs.getInt("a");
     } catch (Exception e) {
       e.printStackTrace();
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
     if(retInt>0){
    	 ret = true;
     }
     return ret;
   }

 }
