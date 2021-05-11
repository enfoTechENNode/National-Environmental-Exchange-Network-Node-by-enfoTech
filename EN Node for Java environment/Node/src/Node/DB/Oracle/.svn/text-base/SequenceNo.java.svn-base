package Node.DB.Oracle;

import java.sql.ResultSet;

import Node.DB.Interfaces.ISequenceNo;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create SequenceNo.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SequenceNo extends NodeDB implements ISequenceNo {
  private String TableName = "SYS_SEQUENCE_NO";
  private String SeqID = "SEQUENCE_ID";
  private String TblName = "TABLE_NAME";
  private String ColumnName = "COLUMN_NAME";
  private String MinNo = "MIN_NUMBER";
  private String MaxNo = "MAX_NUMBER";
  private String LastUsed = "LAST_USED_NUMBER";
  private String Status = "STATUS_CD";
  private String CreatedDate = "CREATED_DTTM";
  private String CreatedBy = "CREATED_BY";
  private String UpdatedDate = "UPDATED_DTTM";
  private String UpdatedBy = "UPDATED_BY";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public SequenceNo(String loggerName) {
    super(loggerName);
  }

  /**
   * GetNextSeqNumber
   * @param tableName
   * @param columnName
   * @return int
   */
  public int GetNextSeqNumber (String tableName, String columnName)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.LastUsed},this.TableName,new String[]{this.TblName,this.ColumnName},new String[]{tableName,columnName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        retInt = rs.getInt(this.LastUsed) + 1;
        rs.updateInt(this.LastUsed, retInt);
        rs.updateRow();
      }
    } catch (Exception e) {
       this.LogException("Could Not Get Next Sequence Number: " + e.toString());
       retInt = -1;
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
}
