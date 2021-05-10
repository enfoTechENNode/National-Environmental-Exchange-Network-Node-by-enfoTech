package Node.DB.Oracle.Configuration;

import java.io.InputStream;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.apache.log4j.Level;

import com.enfotech.basecomponent.db.IDBAdapter;

import Node.Phrase;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;
import Node.DB.Oracle.NodeDB;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create ConfigurationMgr.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ConfigurationMgr extends NodeDB implements IConfigurationMgr {
	public ConfigurationMgr(String loggerName) {
		super(loggerName);
	}

	/**
	 * Initial GetConfig.
	 * @param configName.
	 * @param fileType.
	 * @return String
	 */
	public String GetConfig(String configName, String fileType)
	{
	    IDBAdapter db = this.GetNodeDB();
		String sql=null;
		Clob clob = null;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select extract(CONFIG_XML,'/').getClobVal() CONFIG from SYS_CONFIG where CONFIG_NAME = '"+configName+"' and CONFIG_TYPE_CD='" + fileType + "'";
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	 clob = oracleRs.getClob("CONFIG");
		    	 if (clob != null) {
		    		 retBytes = new byte [(int)clob.length()];
		    	     InputStream is = clob.getAsciiStream();
		    		 is.read(retBytes);
		    	 }
			    db.CommitTransaction();
		    }
		} catch (Exception e) {
			this.LogException("Could not Save Existing Operation: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (oracleStm != null)
		        	oracleStm.close();
		        if (oracleRs != null)
		        	oracleRs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return (retBytes == null)? null : new String(retBytes);
	}

	/**
	 * Initial GetConfig.
	 * @param configID.
	 * @return String
	 */
	public String GetConfig(int configID)
	{
	    IDBAdapter db = this.GetNodeDB();
		String sql=null;
		Clob clob = null;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select extract(CONFIG_XML,'/').getClobVal() CONFIG from SYS_CONFIG where CONFIG_ID = "+configID;
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	 clob = oracleRs.getClob("CONFIG");
		    	 if (clob != null) {
		    		 retBytes = new byte [(int)clob.length()];
		    	     InputStream is = clob.getAsciiStream();
		    		 is.read(retBytes);
		    	 }
			    db.CommitTransaction();
		    }
		} catch (Exception e) {
			this.LogException("Could not Save Existing Operation: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (oracleStm != null)
		        	oracleStm.close();
		        if (oracleRs != null)
		        	oracleRs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return (retBytes == null)? null : new String(retBytes);
	}
	

	/**
	 * Initial SaveConfig.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveConfig(byte[] xmlFile)
	{
	    IDBAdapter db = this.GetNodeDB();
		boolean ret = false;
		ResultSet rs = null;
		String sql=null;
		String xml = null;
		Clob clob = null;
		Connection con = null;
		PreparedStatement st = null;
		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select * from SYS_CONFIG where CONFIG_NAME = '"+Phrase.SYSTEM_FILE_NAME+"'";
		    rs = db.GetResultSet(sql);
		    if(rs != null && rs.first()){
			    sql = "UPDATE SYS_CONFIG SET ";
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml="";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "CONFIG_XML = XMLType.createXML(?)";
				sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system' WHERE CONFIG_NAME = '"+Phrase.SYSTEM_FILE_NAME+"'";
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.execute();
				LoggingUtils.Log("ConfigurationMgr>>>SaveConfig>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }else{
			    int id = this.GetIncrementID("SYS_CONFIG","CONFIG_ID");
				sql = "insert into SYS_CONFIG (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY)" +
				" VALUES ("+id+",'"+Phrase.SYSTEM_FILE_NAME+"',";			
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml = "";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "XMLType.createXML(?),'"+ Phrase.XML_TYPE + "','A',sysdate,'system',sysdate,'system')";
				LoggingUtils.Log("ConfigurationMgr>>>SaveConfig>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.execute();
			    db.CommitTransaction();
			    ret = true;		    	
		    }

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save system.config file: " + e1.toString());			
			}
			this.LogException("Could not save system.config file: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (st != null)
		        	st.close();
		        if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}

		return ret;
	}

	/**
	 * Initial SaveConfig.
	 * @param fileName.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveGeneralConfigFile(String fileName,byte[] xmlFile)
	{
	    IDBAdapter db = this.GetNodeDB();
		boolean ret = false;
		ResultSet rs = null;
		String sql=null;
		String xml = null;
		Clob clob = null;
		Connection con = null;
		PreparedStatement st = null;
		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select * from SYS_CONFIG where CONFIG_NAME = '"+fileName+"'";
		    rs = db.GetResultSet(sql);
		    if(rs != null && rs.first()){
			    sql = "UPDATE SYS_CONFIG SET ";
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml="";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "CONFIG_XML = XMLType.createXML(?)";
				sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system' WHERE CONFIG_NAME = '"+fileName+"'";
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.execute();
				LoggingUtils.Log("ConfigurationMgr>>>SaveConfig>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }else{
			    int id = this.GetIncrementID(fileName,"CONFIG_ID");
				sql = "insert into SYS_CONFIG (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY)" +
				" VALUES ("+id+",'"+fileName+"',";			
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml = "";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "XMLType.createXML(?),'"+ Phrase.XML_TYPE + "','A',sysdate,'system',sysdate,'system')";
				LoggingUtils.Log("ConfigurationMgr>>>SaveConfig>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.execute();
			    db.CommitTransaction();
			    ret = true;		    	
		    }

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save system.config file: " + e1.toString());			
			}
			this.LogException("Could not save system.config file: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (st != null)
		        	st.close();
		        if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}

		return ret;
	}

	/**
	 * Initial GetConfig.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveConfig(String id, String configName, String fileType,
			String content) {
		// TODO Auto-generated method stub
		return false;
	}

}
