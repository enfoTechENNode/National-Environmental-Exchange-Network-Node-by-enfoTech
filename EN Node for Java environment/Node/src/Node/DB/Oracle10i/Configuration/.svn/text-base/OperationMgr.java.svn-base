package Node.DB.Oracle10i.Configuration;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import oracle.sql.BLOB;

import org.apache.log4j.Level;
import org.apache.struts.upload.FormFile;
import org.dom4j.Attribute;
import org.dom4j.Element;
import org.dom4j.Node;
import org.dom4j.io.SAXReader;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.OperationManager;
import Node.DB.Interfaces.Configuration.IOperationMgr;
import Node.DB.Oracle.NodeDB;
import Node.DB.Oracle.SequenceNo;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;

/**
 * <p>This class create OperationMgr.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationMgr extends NodeDB implements IOperationMgr {

	public OperationMgr(String loggerName) {
		super(loggerName);
	}
	
	/**
	 * GetOperationListFile.
	 * @param .
	 * @return byte[]
	 */
	public byte[] GetOperationListFile(){
	    IDBAdapter db = this.GetNodeDB();
		String ret = null;
		String sql=null;
		String xml = null;
		Clob clob = null;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;
	    InputStream is = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select extract(CONFIG_XML,'/').getClobVal() CONFIG from SYS_CONFIG where CONFIG_NAME = '"+Phrase.OPERATIONLIST_FILE_NAME+"'";
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	 clob = oracleRs.getClob("CONFIG");
		    	 if (clob != null) {
		    		 retBytes = new byte [(int)clob.length()];
		    	     is = clob.getAsciiStream();
		    		 is.read(retBytes);
		    	 }
			    db.CommitTransaction();
		    }
		} catch (Exception e) {
			this.LogException("Could not get Existing Operation list: " + e.toString());			
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
				if(is != null)
					is.close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}

		return retBytes;
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
	
	/**
	 * SaveOperationListFile.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveOperationListFile(byte[] xmlFile) {
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
		    sql = "select * from SYS_CONFIG where CONFIG_NAME = '"+Phrase.OPERATIONLIST_FILE_NAME+"'";
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
				sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system',CONFIG_CLOB = (?) WHERE CONFIG_NAME = '"+Phrase.OPERATIONLIST_FILE_NAME+"'";
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.setObject(2, clob);
				st.execute();
				LoggingUtils.Log("OperationMgr>>>SaveOperationListFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }else{
			    int id = this.GetIncrementID("SYS_CONFIG","CONFIG_ID");
				sql = "insert into SYS_CONFIG (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY,CONFIG_CLOB)" +
				" VALUES ("+id+",'"+Phrase.OPERATIONLIST_FILE_NAME+"',";			
				if (xmlFile != null){
					xml = new String(xmlFile);
				}else{
					xml = "";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "XMLType.createXML(?),'XML','A',sysdate,'system',sysdate,'system',(?))";
				LoggingUtils.Log("OperationMgr>>>SaveOperationListFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.setObject(2, clob);
				st.execute();
			    db.CommitTransaction();
			    ret = true;		    	
		    }

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save OperationList file: " + e1.toString());			
			}
			this.LogException("Could not save OperationList file: " + e.toString());			
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
	 * SaveSubListFile.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveSubListFile(FormFile xmlFile) {
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
		    sql = "select * from SYS_CONFIG where CONFIG_NAME = '"+xmlFile.getFileName()+"'";
		    rs = db.GetResultSet(sql);
		    if(rs != null && rs.first()){
			    sql = "UPDATE SYS_CONFIG SET ";
				if (xmlFile != null){
					xml = new String(xmlFile.getFileData());
				}else{
					xml="";
				}
				clob = this.CreateCLOB(db,xml);
				sql += "CONFIG_XML = XMLType.createXML(?)";
				sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system',CONFIG_CLOB = (?) WHERE CONFIG_NAME = '"+xmlFile.getFileName()+"'";
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.setObject(2, clob);
				st.execute();
				LoggingUtils.Log("OperationMgr>>>SaveSubListFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }else{
			    int id = this.GetIncrementID("SYS_CONFIG","CONFIG_ID");
				sql = "insert into SYS_CONFIG (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY,CONFIG_CLOB)" +
				" VALUES ("+id+",'"+xmlFile.getFileName()+"',";			
				if (xmlFile != null){
					xml = new String(xmlFile.getFileData());
				}else{
					xml = "";
				}
				clob = this.CreateCLOB(db,xml);
				String[] fileNameArr = xmlFile.getFileName().split("[-.]");
				sql += "XMLType.createXML(?),'"+fileNameArr[fileNameArr.length-1]+"','A',sysdate,'system',sysdate,'system',(?))";
				LoggingUtils.Log("OperationMgr>>>SaveSubListFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, clob);
				st.setObject(2, clob);
				st.execute();
			    db.CommitTransaction();
			    ret = true;		    	
		    }

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save OperationSubList file: " + e1.toString());			
			}
			this.LogException("Could not save OperationSubList file: " + e.toString());			
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
	 * GetOperationSubFileID.
	 * @param name.
	 * @return String
	 */
	public String GetOperationSubFileID(String name){
	    IDBAdapter db = this.GetNodeDB();
		String ret = null;
		String sql=null;
		String xml = null;
		int id = -1;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select CONFIG_ID from SYS_CONFIG where CONFIG_NAME = '"+name+"'";
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	id = oracleRs.getInt("CONFIG_ID");
			    db.CommitTransaction();
		    }
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save GetOperationSubFileID: " + e1.toString());			
			}
			this.LogException("Could not get Existing OperationSubFile id: " + e.toString());			
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

		return Integer.toString(id);
	}

	/**
	 * GetOperationSubmitList.
	 * @param name.
	 * @return String
	 */
	public String GetOperationSubmitList(String name){
	    IDBAdapter db = this.GetNodeDB();
		String ret = null;
		String sql=null;
		String xml = null;
		int id = -1;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "select CONFIG_ID from SYS_CONFIG where CONFIG_NAME = '"+name+"'";
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	id = oracleRs.getInt("CONFIG_ID");
			    db.CommitTransaction();
		    }
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save GetOperationTemplateFileID: " + e1.toString());			
			}
			this.LogException("Could not get Existing Operation template id: " + e.toString());			
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

		return Integer.toString(id);
	}

	/**
	 * DeleteSubFileByID.
	 * @param id.
	 * @return boolean
	 */
	public boolean DeleteSubFileByID(String id){
	    IDBAdapter db = this.GetNodeDB();
	    boolean ret = false;
		String sql=null;
		String xml = null;
		Connection oracleCon = null;
		PreparedStatement oracleStm = null;
		ResultSet oracleRs = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    sql = "delete from SYS_CONFIG where CONFIG_ID = " + Integer.parseInt(id);
		    oracleStm = oracleCon.prepareStatement(sql);
		    oracleStm.execute();
		    if(db.CommitTransaction()) ret = true;
		    
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save DeleteSubFileByID: " + e1.toString());			
			}
			this.LogException("Could not save DeleteSubFileByID: " + e.toString());			
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

		return ret;
	}

	/**
	 * AddOperationManger.
	 * @param operationName.
	 * @param statusCD.
	 * @param submitURL.
	 * @param version.
	 * @param transID.
	 * @param supTransID.
	 * @param xmlFile.
	 * @param dataFlow.
	 * @return boolean
	 */
	public boolean AddOperationManger(String operationName,String statusCD,String submitURL,
			String version,String transID,String supTransID,byte[] xmlFile,String dataFlow) {
	    IDBAdapter db = this.GetNodeDB();
		boolean ret = false;
		ResultSet rs = null;
		String sql=null;
		BLOB blob = null;
		Connection con = null;
		PreparedStatement st = null;
		int submitId = 0; 
		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    submitId = this.GetIncrementID("NODE_OPERATION_MANAGER", "SUBMIT_ID");
		    sql =  "insert into NODE_OPERATION_MANAGER (SUBMIT_ID,OPERATION_NAME,STATUS_CD,SUBMITTED_DTTM,SUBMITTED_URL,VERSION_NO,TRANS_ID,SUPPLIED_TRANS_ID,FILE_CONTENT,DATA_FLOW) values ("
	    		+ submitId + ","
	    		+ "'" + operationName + "',"
	    		+ "'" + statusCD + "',"
	    		+ "sysdate,"
	    		+ "'" + submitURL + "',"
	    		+ "'" + version + "',"
	    		+ "'" + transID + "',"
	    		+ "'" + supTransID + "',"
	    		+ "(?)" + ","
	    		+ "'" + dataFlow + "'"
	    		+ ")";
		    if(xmlFile == null){
		    	xmlFile = " ".getBytes();
		    }
		    this.CreateBLOB(db,xmlFile);
		    con = db.GetConnection();
			st = con.prepareStatement(sql);
			st.setObject(1, blob);
			st.execute();
			LoggingUtils.Log("OperationMgr>>>SaveOperationMangerFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
		    db.CommitTransaction();
			ret = true;		    	

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save OperationManger file: " + e1.toString());			
			}
			this.LogException("Could not save OperationManger file: " + e.toString());			
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
	 * SaveOperationMangerFile.
	 * @param submitId.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveOperationMangerFile(int submitId, byte[] xmlFile) {
	    IDBAdapter db = this.GetNodeDB();
		boolean ret = false;
		ResultSet rs = null;
		String sql=null;
		BLOB blob = null;
		Connection con = null;
		PreparedStatement st = null;
		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    if(xmlFile != null){
			    sql = "UPDATE NODE_OPERATION_MANAGER SET FILE_CONTENT = (?) where SUBMIT_ID = "+submitId;
			    blob = this.CreateBLOB(db,xmlFile);
				con = db.GetConnection();
				st = con.prepareStatement(sql);
				st.setObject(1, blob);
				st.execute();
				LoggingUtils.Log("OperationMgr>>>SaveOperationMangerFile>>> sql is: "+sql, Level.DEBUG, Phrase.AdministrationLoggerName);
			    db.CommitTransaction();
				ret = true;		    	
		    }

		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save OperationManger file: " + e1.toString());			
			}
			this.LogException("Could not save OperationManger file: " + e.toString());			
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
	 * CheckGetStatus.
	 * @param opId.
	 * @return boolean
	 */
	public boolean CheckGetStatus(int opId) {
		boolean ret = false;
		byte[] fileByte = null;
		org.dom4j.Document doc4jVR = null;
		
		try {
		    if(opId != -1){
		    	Operation op = new Operation(opId,Phrase.AdministrationLoggerName);
				if (op != null){
				    String config = op.GetConfig();
				    if(config!=null){
				        XmlDocument doc = new XmlDocument();
				        doc.LoadXml(op.GetConfig());		
					    if (doc.DocumentElement().Name().toLowerCase().trim().equals("process")) {
					    	fileByte = this.GetOperationListFile();
							if (fileByte != null){
							    SAXReader reader = new SAXReader();
							    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
							    Element rootElm = doc4jVR.getRootElement();
							    // get operationId and Name
							    List operationList = rootElm.elements("Operation");
				
							    if (operationList != null && operationList.size() > 0) {
						    		for(Iterator i = operationList.iterator(); i.hasNext(); ){
						    			Element operation = (Element) i.next();
						    			// get parameter
								    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(Integer.toString(opId))){			    			
									    	Element getStatus = operation.element("GetStatus");
									    	String status = this.getAttributeValue(getStatus, "@Enable");
									    	if(status.equalsIgnoreCase("True")){
										    	ret = true;									    		
									    	}else{
										    	ret = false;
									    	}
									    	break;								    	
							    		}
							    	}
							    }
							}
					    }else ret = false;				    				    	
				    }
				}
		    }

		} catch (Exception e) {
			this.LogException("Could not CheckGetStatus: " + e.toString());			
		} finally {
		}

		return ret;
	}	

	/**
	   * GetIncrementID
	   * @param tableName
	   * @param columnName
	   * @return ArrayList
	   */
	protected int GetIncrementID (String tableName, String columnName)
	  {
	    SequenceNo seqDB = new SequenceNo(this.LoggerName);
	    return seqDB.GetNextSeqNumber(tableName,columnName);
	  }
	
	private String getAttributeValue(Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			Attribute att = (Attribute) attNode;
			value = att.getValue();
		}

		return value;
	}

	/**
	   * GetOperationsManagerTableList
	   * @param transId
	   * @return ArrayList
	   */
	public ArrayList GetOperationsManagerTableList(String transId) {
	    IDBAdapter db = this.GetNodeDB();
	    ArrayList ret = new ArrayList();
		String sql=null;
		ResultSet rs = null;
		OperationManager opMgr = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select * from NODE_OPERATION_MANAGER where TRANS_ID = '"+transId+"'";
		    rs = db.ExecuteReader(sql);
		    while(rs.next()){
		    	opMgr = new OperationManager();
		    	opMgr.setSubmitId(rs.getInt("SUBMIT_ID"));
		    	opMgr.setOperationName(rs.getString("OPERATION_NAME"));
		    	opMgr.setStatusCD(rs.getString("STATUS_CD"));
		    	opMgr.setSubmitDate(rs.getDate("SUBMITTED_DTTM"));
		    	opMgr.setSubmitURL(rs.getString("SUBMITTED_URL"));
		    	opMgr.setVersion(rs.getString("VERSION_NO"));
		    	opMgr.setTransId(rs.getString("TRANS_ID"));
		    	opMgr.setSubmitURL(rs.getString("SUPPLIED_TRANS_ID"));
		    	opMgr.setDataFlow(rs.getString("DATA_FLOW"));
		    	ret.add(opMgr);
		    }
		    
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could not save DeleteSubFileByID: " + e1.toString());			
			}
			this.LogException("Could not save DeleteSubFileByID: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
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
	 * GetUserNameFromOperation_log_parameter.
	 * @param token.
	 * @param webserviceName.
	 * @return String
	 */
	public String GetUserNameFromOperation_log_parameter(String token,
			String webserviceName) {
	    IDBAdapter db = this.GetNodeDB();
	    String ret = null;
		String sql=null;
		ResultSet rs = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select parameter_value from node_operation_log_parameter "
		    	+ " where parameter_name = 'User ID' "
		    	+ "	and operation_log_id = (select operation_log_id from node_operation_log a, node_operation b,node_web_service c "
                + " where a.operation_id = b.operation_id "
                + " and b.web_service_id = c.web_service_id "
                + " and a.token = '" + token + "'"
                + " and c.web_service_name = '" + webserviceName + "') ";
		    rs = db.ExecuteReader(sql);
		    while(rs.next()){
		    	ret = rs.getString("parameter_value");
		    }
		    
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("CheckUserNameFromOperation_log_parameter>>> Could not get User Name" + e1.toString());			
			}
			this.LogException("CheckUserNameFromOperation_log_parameter>>> Could not get User Name" + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
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
	 * GetPasswordFromOperation_log_parameter.
	 * @param token.
	 * @param webserviceName.
	 * @return String
	 */
	public String GetPasswordFromOperation_log_parameter(String token,
			String webserviceName) {
	    IDBAdapter db = this.GetNodeDB();
	    String ret = null;
		String sql=null;
		ResultSet rs = null;

		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    sql = "select parameter_value from node_operation_log_parameter "
		    	+ " where parameter_name = 'Credential' "
		    	+ "	and operation_log_id = (select operation_log_id from node_operation_log a, node_operation b,node_web_service c "
                + " where a.operation_id = b.operation_id "
                + " and b.web_service_id = c.web_service_id "
                + " and a.token = '" + token + "'"
                + " and c.web_service_name = '" + webserviceName + "') ";
		    rs = db.ExecuteReader(sql);
		    while(rs.next()){
		    	ret = rs.getString("parameter_value");
		    }
		    
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("CheckPasswordFromOperation_log_parameter>>> Could not get Password" + e1.toString());			
			}
			this.LogException("CheckPasswordFromOperation_log_parameter>>> Could not get Password" + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
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

}
