package Node.DB.Oracle;

import com.enfotech.basecomponent.db.IDBAdapter;
import java.sql.ResultSet;
/**
 * <p>This class create SYSEmail.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SYSEmail extends NodeDB {
  private String TableName = this.SysEmailTableName;

  private String EmailAddress = this.SysEmail;
  private String EmailID = this.SysEmailID;

  private String XREFTableName = this.SysUserEmailTableName;
  private String UserID = this.SysUserInfoUserID;

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public SYSEmail(String loggerName) {
    super(loggerName);
  }

  /**
   * GetEmail
   * @param userID
   * @return String
   */
  public String GetEmail (int userID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A." + this.EmailAddress + " from " + this.TableName + " A, " + this.XREFTableName + " B";
      sql += " where B." + this.UserID + " = " + userID + " and B." + this.EmailID + " = A." + this.EmailID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        rs.first();
        retString = rs.getString(this.EmailAddress);
      }
    } catch (Exception e) {
      this.LogException("Could Get Email Address: " + e.toString());
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
}
