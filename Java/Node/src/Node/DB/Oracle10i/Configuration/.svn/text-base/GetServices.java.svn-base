package Node.DB.Oracle10i.Configuration;

import java.io.InputStream;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.DB.Oracle.NodeDB;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;

import com.enfotech.basecomponent.db.IDBAdapter;

/**
 * <p>This class create GetServices.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetServices extends NodeDB implements IGetServices {

	public GetServices(String loggerName) {
		super(loggerName);
	}
		
	// WI 21296
	/**
	 * getDedlFile.
	 * @param version.
	 * @param fileName.
	 * @return byte[]
	 */
	public byte[] getConfigFile(String version,String fileName){
	    IDBAdapter db = this.GetNodeDB();
		String sql=null;
		Clob clob = null;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;
		String configName = null;

		try {
			configName = Utility.getConfigFileNameWithVersion(version, fileName);					
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select extract(CONFIG_XML,'/').getClobVal() CONFIG from SYS_CONFIG where CONFIG_NAME = '"+configName+"'";
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
			this.LogException("Could not get config file: " + fileName + "  " + e.toString());			
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

		return retBytes;
	}

	// WI 21296
	/**
	 * SaveDedlFile.
	 * @param xmlFile.
	 * @param version.
	 * @param fileName.
	 * @return boolean
	 */
	public boolean saveConfigFile(byte[] xmlFile, String version,String fileName) {
	    IDBAdapter db = this.GetNodeDB();
		boolean ret = false;
		ResultSet rs = null;
		String sql=null;
		String xml = null;
		Clob clob = null;
		Connection con = null;
		PreparedStatement st = null;
		String configName = null;
		try {
			configName = Utility.getConfigFileNameWithVersion(version, fileName);					
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select * from SYS_CONFIG where CONFIG_NAME = '" + configName +"'";
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
				sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system' WHERE CONFIG_NAME = '"+configName+"'";
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.execute();
				LoggingUtils.Log("GetServices>>>saveConfigFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }else{
			    int id = this.GetIncrementID("SYS_CONFIG","CONFIG_ID");
				sql = "insert into SYS_CONFIG (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY)" +
				" VALUES ("+id+",'"+configName+"',";			
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml = "";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "XMLType.createXML(?),'"+ Phrase.XML_TYPE + "','A',sysdate,'system',sysdate,'system')";
				LoggingUtils.Log("GetServices>>>saveConfigFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
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
				this.LogException("Could not save " + fileName + " file: " + e1.toString());			
			}
			this.LogException("Could not save " + fileName + " file: " + e.toString());			
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
	 * SaveGetServices.
	 * @param nodeIdentifier.
	 * @param nodeName.
	 * @param nodeAddress.
	 * @param organizationIdentifier.
	 * @param nodeContact.
	 * @param nodeVersionIdentifier.
	 * @param nodeDeploymentTypeCode.
	 * @param nodeStatus.
	 * @param nodePropertyName.
	 * @param nodePropertyValue.
	 * @param north.
	 * @param south.
	 * @param east.
	 * @param west.
	 * @return boolean
	 */
	public boolean SaveGetServices(String nodeIdentifier, String nodeName,
			String nodeAddress, String organizationIdentifier,
			String nodeContact, String nodeVersionIdentifier,
			String nodeDeploymentTypeCode, String nodeStatus,
			String[] nodePropertyName, String[] nodePropertyValue, String north,
			String south, String east, String west) {
		// TODO Auto-generated method stub
		return false;
	}
}
