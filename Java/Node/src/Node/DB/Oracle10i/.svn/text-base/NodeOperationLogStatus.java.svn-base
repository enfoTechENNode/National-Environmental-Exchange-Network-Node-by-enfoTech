package Node.DB.Oracle10i;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;

import Node.DB.Interfaces.INodeOperationLogStatus;
import Node.Utils.Utility;
/**
 * <p>This class create NodeOperationLogStatus.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeOperationLogStatus extends NodeDB implements INodeOperationLogStatus {
  private String TableName = "NODE_OPERATION_LOG_STATUS";
  private String OpLogStatusID = "OPERATION_LOG_STATUS_ID";
  private String OpLogID = "OPERATION_LOG_ID";
  private String Status = "STATUS_CD";
  private String Message = "MESSAGE";
  private String CreatedDate = "CREATED_DTTM";
  private String CreatedBy = "CREATED_BY";

  /**
   * Constructor
   * @param loggerName
   * @return 
    * @deprecated
   */
  public NodeOperationLogStatus(String loggerName) {
    super(loggerName);
  }

  /**
   * CreateLogStatus
   * @param opLogID
   * @param status
   * @param message
   * @return int
    * @deprecated
   */
  public int CreateLogStatus (int opLogID, String status, String message)
  {
     int retInt = -1;
     IDBAdapter db = null;
     ResultSet rs = null;
     try {
       String[] select = new String[]{
         this.OpLogStatusID,this.OpLogID,this.Status,this.Message,this.CreatedDate,this.CreatedBy
       };
       String sql = this.GetSelectStr(select,this.TableName,new String[]{this.OpLogStatusID},new String[]{"-999999"});
       db = this.GetNodeDB();
       rs = db.GetResultSet(sql);
       if (rs != null) {
         rs.moveToInsertRow();
         retInt = this.GetIncrementID(this.TableName,this.OpLogStatusID);
         rs.updateInt(this.OpLogStatusID,retInt);
         rs.updateInt(this.OpLogID,opLogID); rs.updateString(this.Status,status);
         String updateMessage = message;
         if (message != null && message.length() > 4000)
           updateMessage = message.substring(0, 4000);
         rs.updateString(this.Message,updateMessage);
         rs.updateDate(this.CreatedDate,Utility.GetNowDate()); rs.updateTimestamp(this.CreatedDate,Utility.GetNowTimeStamp());
         rs.updateString(this.CreatedBy,"system");
         rs.insertRow();
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
   * GetLatestStatus
   * @param opLogID
   * @return String
    * @deprecated
   */
  public String GetLatestStatus (int opLogID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.OpLogID},new String[]{opLogID+""},"order by " + this.CreatedDate + " desc");
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.first())
        retString = rs.getString(this.Status);
    } catch (Exception e) {
      this.LogException("Could Not Get Operation Status: " + e.toString());
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
}
