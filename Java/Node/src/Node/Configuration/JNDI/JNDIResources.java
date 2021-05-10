package Node.Configuration.JNDI;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.jndi.JNDIAccess;

import java.util.ResourceBundle;

import Node.Phrase;
import Node.Utils.Utility;
/**
 * <p>This class create JNDIResource Class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

	public class JNDIResources {
		public static String DBConnectionString = null;
		private JNDIResources() {
	}

/**
 * Initial Get Direct DB.
 * @param .
 * @return IDBAdapter
 */
	public static IDBAdapter GetDirectDB (String conString)
	{
		IDBAdapter ret = null;
		if (conString != null && !conString.equals("")) {
			DBConnectionString = conString;
			ret = new Node.DB.Oracle.OracleDBAdapter(conString);
		}
		return ret;
	}

	/**
	 * Initial Get Direct SqlServer DB.
	 * @param .
	 * @return IDBAdapter
	 */
	public static IDBAdapter GetDirectSqlServerDB (String conString)
	{
		IDBAdapter ret = null;
		if (conString != null && !conString.equals("")) {
			DBConnectionString = conString;
			ret = new Node.DB.SqlServer.SqlServerDBAdapter(conString);
		}
		return ret;
	}

		
/**
 * Initial GetNodeDB.
 * @param .
 * @return IDBAdapter
 */
public static IDBAdapter GetNodeDB ()
  {
    String dbType = (String)JNDIAccess.GetJNDIValue(Phrase.dbType, false);
    String jndiName = (String)JNDIAccess.GetJNDIValue(Phrase.dbJNDI, false);
    if (!Utility.isNullOrEmpty(dbType) && Utility.isNullOrEmpty(jndiName)) {
      String connect = null;
      String fullString = (String)JNDIAccess.GetJNDIValue(Phrase.dbFullString, false);
      if (fullString != null && !fullString.equals("")) {
        String dbUser = (String) JNDIAccess.GetJNDIValue(Phrase.dbUser, false);
        String dbPassword = (String) JNDIAccess.GetJNDIValue(Phrase.dbPassword, false);
        connect = "jdbc:oracle:thin:" + dbUser + "/" + dbPassword + "@" + fullString;
        DBConnectionString = connect;
      }
      else {
        String dbServer = (String)JNDIAccess.GetJNDIValue(Phrase.dbServer, false);
        String dbPort = (String)JNDIAccess.GetJNDIValue(Phrase.dbPort, false);
        String dbSID = (String)JNDIAccess.GetJNDIValue(Phrase.dbSID, false);
        String dbUser = (String)JNDIAccess.GetJNDIValue(Phrase.dbUser, false);
        String dbPassword = (String)JNDIAccess.GetJNDIValue(Phrase.dbPassword, false);
        connect = Node.DB.Oracle.OracleDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
        DBConnectionString = connect;
      }
      return new Node.DB.Oracle.OracleDBAdapter(connect);
    }
    else if (dbType != null)
    {
        return new Node.DB.Oracle.OracleDBAdapter(jndiName);
    }
    else {
      ResourceBundle bundle = ResourceBundle.getBundle("application");
      dbType = (String)bundle.getObject(Phrase.dbType);
      if (dbType != null && !dbType.equals("")) {
        String connect = null;
        try {
          String fullString= (String)bundle.getObject(Phrase.dbFullString);
          String dbUser = (String)bundle.getObject(Phrase.dbUser);
          String dbPassword = (String)bundle.getObject(Phrase.dbPassword);
          connect = "jdbc:oracle:thin:"+dbUser+"/"+dbPassword+"@"+fullString;
          DBConnectionString = connect;
        } catch (Exception e) {
          String dbServer = (String)bundle.getObject(Phrase.dbServer);
          String dbPort = (String)bundle.getObject(Phrase.dbPort);
          String dbSID = (String)bundle.getObject(Phrase.dbSID);
          String dbUser = (String)bundle.getObject(Phrase.dbUser);
          String dbPassword = (String)bundle.getObject(Phrase.dbPassword);
          connect = Node.DB.Oracle.OracleDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
          DBConnectionString = connect;
        }
        return new Node.DB.Oracle.OracleDBAdapter(connect);
      }
    }
    return null;
  }

/**
 * Initial GetDB.
 * @param dbName.
 * @return IDBAdapter
 */

  public static IDBAdapter GetDB(String dbName)
  {
    String dbType = (String)JNDIAccess.GetJNDIValue(dbName + "/dbType", false);
    String jndiName = (String)JNDIAccess.GetJNDIValue(dbName + "/dbJNDIName", false);

    if (dbType != null && jndiName == null) {
      String connect = null;
      String fullString = (String)JNDIAccess.GetJNDIValue(dbName + "/dbFullString", false);
      if (fullString != null && !fullString.equals("")) {
        String dbUser = (String) JNDIAccess.GetJNDIValue(dbName + "/dbUser", false);
        String dbPassword = (String) JNDIAccess.GetJNDIValue(dbName + "/dbPassword", false);
        connect = "jdbc:oracle:thin:" + dbUser + "/" + dbPassword + "@" + fullString;
        DBConnectionString = connect;
      }
      else {
        String dbServer = (String)JNDIAccess.GetJNDIValue(dbName + "/dbServer", false);
        String dbPort = (String)JNDIAccess.GetJNDIValue(dbName + "/dbPort", false);
        String dbSID = (String)JNDIAccess.GetJNDIValue(dbName + "/dbSID", false);
        String dbUser = (String) JNDIAccess.GetJNDIValue(dbName + "/dbUser", false);
        String dbPassword = (String) JNDIAccess.GetJNDIValue(dbName + "/dbPassword", false);

        connect = Node.DB.Oracle.OracleDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
        DBConnectionString = connect;
      }
      return new Node.DB.Oracle.OracleDBAdapter(connect);
    }
    else if (dbType != null)
    {
        return new Node.DB.Oracle.OracleDBAdapter(jndiName);
    }
    else {
      ResourceBundle bundle = ResourceBundle.getBundle("application");
      dbType = (String)bundle.getObject(dbName + "/dbType");
      if (dbType != null && !dbType.equals("")) {
        String connect = null;
        try {
          String fullString= (String)bundle.getObject(dbName + "/dbFullString");
          String dbUser = (String)bundle.getObject(dbName + "/dbUser");
          String dbPassword = (String)bundle.getObject(dbName + "/dbPassword");
          connect = "jdbc:oracle:thin:"+dbUser+"/"+dbPassword+"@"+fullString;
          DBConnectionString = connect;
        } catch (Exception e) {
          String dbServer = (String)bundle.getObject(dbName + "/dbServer");
          String dbPort = (String)bundle.getObject(dbName + "/dbPort");
          String dbSID = (String)bundle.getObject(dbName + "/dbSID");
          String dbUser = (String)bundle.getObject(dbName + "/dbUser");
          String dbPassword = (String)bundle.getObject(dbName + "/dbPassword");
          connect = Node.DB.Oracle.OracleDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
          DBConnectionString = connect;
        }
        return new Node.DB.Oracle.OracleDBAdapter(connect);
      }
    }
    return null;
  }
  
  /**
   * Initial GetDB.
   * @param dbName.
   * @return IDBAdapter
   */

    public static IDBAdapter GetSqlServerDB(String dbName)
    {
      String dbType = (String)JNDIAccess.GetJNDIValue(dbName + "/dbType", false);
      String jndiName = (String)JNDIAccess.GetJNDIValue(dbName + "/dbJNDIName", false);

      if (dbType != null && jndiName == null) {
        String connect = null;
        String fullString = (String)JNDIAccess.GetJNDIValue(dbName + "/dbFullString", false);
        if (fullString != null && !fullString.equals("")) {
          String dbUser = (String) JNDIAccess.GetJNDIValue(dbName + "/dbUser", false);
          String dbPassword = (String) JNDIAccess.GetJNDIValue(dbName + "/dbPassword", false);
          connect = fullString + " User=" + dbUser + ";Password=" + dbPassword;
          DBConnectionString = connect;
        }
        else {
          String dbServer = (String)JNDIAccess.GetJNDIValue(dbName + "/dbServer", false);
          String dbPort = (String)JNDIAccess.GetJNDIValue(dbName + "/dbPort", false);
          String dbSID = (String)JNDIAccess.GetJNDIValue(dbName + "/dbSID", false);
          String dbUser = (String) JNDIAccess.GetJNDIValue(dbName + "/dbUser", false);
          String dbPassword = (String) JNDIAccess.GetJNDIValue(dbName + "/dbPassword", false);

          connect = Node.DB.SqlServer.SqlServerDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
          DBConnectionString = connect;
        }
        return new Node.DB.SqlServer.SqlServerDBAdapter(connect);
      }
      else if (dbType != null)
      {
          return new Node.DB.SqlServer.SqlServerDBAdapter(jndiName);
      }
      else {
        ResourceBundle bundle = ResourceBundle.getBundle("application");
        dbType = (String)bundle.getObject(dbName + "/dbType");
        if (dbType != null && !dbType.equals("")) {
          String connect = null;
          try {
            String fullString= (String)bundle.getObject(dbName + "/dbFullString");
            String dbUser = (String)bundle.getObject(dbName + "/dbUser");
            String dbPassword = (String)bundle.getObject(dbName + "/dbPassword");
            connect = fullString + " User=" + dbUser + ";Password=" + dbPassword;
            DBConnectionString = connect;
          } catch (Exception e) {
            String dbServer = (String)bundle.getObject(dbName + "/dbServer");
            String dbPort = (String)bundle.getObject(dbName + "/dbPort");
            String dbSID = (String)bundle.getObject(dbName + "/dbSID");
            String dbUser = (String)bundle.getObject(dbName + "/dbUser");
            String dbPassword = (String)bundle.getObject(dbName + "/dbPassword");
            connect = Node.DB.SqlServer.SqlServerDBAdapter.GetConnectionString(dbServer,dbPort,dbSID,dbUser,dbPassword);
            DBConnectionString = connect;
          }
          return new Node.DB.SqlServer.SqlServerDBAdapter(connect);
        }
      }
      return null;
    }

}
