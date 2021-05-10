package Node.DB.Oracle;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;
/**
 * <p>This class create SYSUserEmail.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SYSUserEmail extends NodeDB {
  private String TableName = this.SysUserEmailTableName;

  private String UserID = this.SysUserInfoUserID;
  private String EmailID = this.SysEmailID;

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public SYSUserEmail(String loggerName) {
    super(loggerName);
  }

  /**
   * GetEmailID
   * @param userID
   * @return int
   */
  public int GetEmailID (int userID)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.EmailID},this.TableName,new String[]{this.UserID},new String[]{userID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.EmailID);
    } catch (Exception e) {
      this.LogException("Could Not Get EmailID: " + e.toString());
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
