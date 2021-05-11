package Node.Task;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.net.URL;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import oracle.jdbc.OracleResultSet;
import oracle.sql.BLOB;

import org.apache.log4j.Level;
import org.dom4j.Attribute;
import org.dom4j.Node;
import org.dom4j.io.SAXReader;

import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Task.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDB;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.Configuration.IOperationMgr;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Task.model.GetStatusModel;
import Node.Utils.LoggingUtils;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create GetStatusTask.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetStatusTask implements IProcess {

	INodeDB nodeDB = DBManager.getNodeDB(Phrase.TaskLoggerName);
    IDBAdapter db = nodeDB.GetNodeDB();

	  /**
	   * Execute
	   * @param parameters
	   * @param param
	   * @return String
	   */
    public String Execute(String[] parameters, ProcParam param) {
		
		String ret = Phrase.STOPPED_STATUS;
		String sql=null;
		String xml = null;
		BLOB blob = null;
		Connection oracleCon = null;
	    Statement oracleStm = null;
		ResultSet rs = null;
		ResultSet oracleRs = null;
	    byte[] retBytes = null;
	    InputStream is = null;
	    String token = null;
	    ArrayList getStatusModelList = new ArrayList();
	    GetStatusModel getStatusModel = null;
        IOperationMgr operationMgr = DBManager.getOperationMgr(Phrase.TaskLoggerName);
		byte[] fileByte = null;
		org.dom4j.Document doc4jVR = null;
		List operationMgrBeanList = null;
		Node node = null;
		String user = null;
		String password = null;
		String domainName = null;
		String version = null;
		String transID = null;
		String supplyTransID = null;
		String submitURL = null;
		String dataFlow = null;
		String operationName = null;
		String[] status = null;
		ClsNodeDocument[] docs = null;
		boolean success = false;
		String monthInterval = "1";
		
		try {
		    db.KeepConnectionAfterExecute(true);
		    db.BeginTransaction();
		    oracleCon = db.GetConnection();
		    // WI 23016
		    if(parameters.length >= 2 && parameters[1] != null && !parameters[1].equals("")){
		    	monthInterval = parameters[1];
		    }
		    sql = "select * from NODE_OPERATION_MANAGER where STATUS_CD = '"+ Phrase.PendingStatus+"' AND MONTHS_BETWEEN(SYSDATE, SUBMITTED_DTTM) < " + monthInterval;
		    oracleStm = oracleCon.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
		    oracleRs = oracleStm.executeQuery(sql);
		    while (oracleRs.next()) {
		    	GetStatusModel tmpGetStatusModel = new GetStatusModel();
		    	tmpGetStatusModel.setSubmitID(oracleRs.getInt("SUBMIT_ID"));
		    	tmpGetStatusModel.setOperationName(oracleRs.getString("OPERATION_NAME"));
		    	tmpGetStatusModel.setStatusCD(oracleRs.getString("STATUS_CD"));
		    	tmpGetStatusModel.setSubmitDate(oracleRs.getDate("SUBMITTED_DTTM"));
		    	tmpGetStatusModel.setSubmitURL(oracleRs.getString("SUBMITTED_URL"));
		    	tmpGetStatusModel.setVersionNo(oracleRs.getString("VERSION_NO"));
		    	tmpGetStatusModel.setTransID(oracleRs.getString("TRANS_ID"));
		    	tmpGetStatusModel.setSupplyTransID(oracleRs.getString("SUPPLIED_TRANS_ID"));
		    	tmpGetStatusModel.setFileContent(oracleRs.getBlob("FILE_CONTENT"));
		    	tmpGetStatusModel.setDataFlow(oracleRs.getString("DATA_FLOW"));
		    	
		    	getStatusModelList.add(tmpGetStatusModel);		    	
		    }
		    oracleRs.close();
		    if(getStatusModelList!=null && !getStatusModelList.isEmpty()){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
				}

				Iterator it = getStatusModelList.iterator();
		    	while(it.hasNext()){
		    		getStatusModel = (GetStatusModel)it.next();
		    		
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	for(int i=0;i<operationMgrBeanList.size();i++){
				    		node = (Node) operationMgrBeanList.get(i);
				    		if(this.getAttributeValue(node,"@name").trim().equalsIgnoreCase(getStatusModel.getOperationName().trim())){
				    			Node getStatus = node.selectSingleNode("GetStatus");
				    			String isGetStatus = this.getAttributeValue(getStatus, "@Enable");
				    			if(isGetStatus!=null && isGetStatus.trim().equalsIgnoreCase("True")){
				    				String completeSign = this.getElementValue(getStatus, "Complete");
				    				String errorSign = this.getElementValue(getStatus, "Error");
				    				
				    				Node submit = node.selectSingleNode("Submit");
				    				
				    				user = this.getElementValue(submit, "UserName");;
				    				password = this.getElementValue(submit, "Password");
				    			    Cryptography crypt = new Cryptography();
				    				password = crypt.Decrypting(password,Phrase.CryptKey);
				    				domainName = this.getElementValue(submit, "DomainName");
				    				version = getStatusModel.getVersionNo();
				    				transID = getStatusModel.getTransID();
				    				supplyTransID = getStatusModel.getSupplyTransID();
				    				submitURL = getStatusModel.getSubmitURL();
				    				dataFlow = getStatusModel.getDataFlow();
				    				operationName = getStatusModel.getOperationName();
				    				
				    				if(supplyTransID.startsWith("_")){
					    		    	token = this.Authenticate(version,user,password,domainName,submitURL);
					    		    	// Call GetStatus webservice
					    				if (token != null && !token.equalsIgnoreCase(Phrase.InvalidCredential)) {
					    					if(version!=null){
					    						status = this.CallGetStatus(token,version, supplyTransID, submitURL,dataFlow);
					    						
					    						if(status != null && status.length > 0 && (status[0].equalsIgnoreCase(completeSign) || status[0].equalsIgnoreCase(errorSign))){				    							
					    							if(status.length==1){	// Handle node1.1
				    									docs = this.Download(version, token, supplyTransID, operationName, docs, submitURL);
					    							}else{	// Handle node2.0
				    									docs = this.Download(version, token, supplyTransID, dataFlow, docs, submitURL);				    								
					    							}
			    									if(status[0].equalsIgnoreCase(completeSign) || status[0].equalsIgnoreCase(errorSign)){
					    								if(status[0].equalsIgnoreCase(completeSign)){
					    									success = this.updateStatusFile(blob, docs, db, transID, completeSign);
					    								}else if(status[0].equalsIgnoreCase(errorSign)){
					    									success = this.updateStatusFile(blob, docs, db, transID, errorSign);
					    								}
					    								if(!success){
					    									this.Log("GetStatusTask>>> Could not update Status of " + transID, Level.ERROR);
					    								}
			    									}else{
			    				    		    		this.Log("GetStatusTask>>> Could not download files from " + submitURL, Level.ERROR);
			    				    		    	}					    											    													    							
					    						}
					    					}
					    		    	}else{
					    		    		this.Log("GetStatusTask>>> Could not get token from " + submitURL, Level.ERROR);
					    		    	}				    					
				    				}else{
				    		    		this.Log("GetStatusTask>>> Could not get status because there is no supply TransID for transaction: " + transID, Level.ERROR);
				    		    	}
				    			}
				    			break;
				    		}
				    	}
				    }

		    		
		    		
		    	}
		    }
		    db.CommitTransaction();
		} catch (Exception e) {
			this.Log("GetStatusTask>>> " + e.toString(), Level.ERROR);			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (oracleStm != null)
		        	oracleStm.close();
		        if (oracleRs != null)
		        	oracleRs.close();
		        if (rs != null)
		        	rs.close();
				if (db != null)
					db.Close();
				if(is != null)
					is.close();
			} catch (Exception e) {
				this.Log(e.toString(), Level.ERROR);
			}
		}		
		return ret;
	}
	
	  /**
	   * Authenticate
	   * @param version
	   * @param userID
	   * @param password
	   * @param domainName
	   * @param submitURL
	   * @return String
	   */
	private String Authenticate(String version,String userID,String password,String domainName, String submitURL) {
		String token = null;
		try {
			String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
			String nodeAddress = null;
			URL node = null;
			
			
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				nodeAddress = submitURL;
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod);
			}else{
				nodeAddress = submitURL;
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod, domainName);
			}
			if (token != null && !token.equals("")) {
				return token;
			} 
			
		} catch (Exception e) {
			this.Log("GetStatusTask>>> Error - Fail to authenticate. " + e.getMessage(),
					Level.ERROR);
		}
		return token;
	}

	  /**
	   * CallGetStatus
	   * @param token
	   * @param version
	   * @param transID
	   * @param nodeAddress
	   * @param dataFlow
	   * @return String[]
	   */
	private String[] CallGetStatus(String token, String version, String transID,String nodeAddress,String dataFlow) {
		URL node = null;
		String[] result = null;
		String ret = null;

		try {
	        
	        if (token !=null ) {
				if (version.equalsIgnoreCase(Phrase.ver_1)) {
					node = new URL(nodeAddress);
					NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
					result = new String[1];
					result[0] = requestor.getStatus(token, transID);
				} else if (version.equalsIgnoreCase(Phrase.ver_2)){
					node = new URL(nodeAddress);
					Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(
							node, Phrase.AdministrationLoggerName);

				    result = requestor.getStatus(token, transID);
				}
	        }
	        else{
	        	this.Log("GetStatusTask>>> Fail to call getstatus file.",Level.FATAL);
	        }		        				
		} catch (Exception e) {
			this.Log("GetStatusTask>>> Error - Fail to call getstatus file. " + e.getMessage(),
					Level.ERROR);
		}
		return result;
	}
		  
	  /**
	   * CallGetStatus
	   * @param version
	   * @param token
	   * @param transID
	   * @param dataFlow
	   * @param docs
	   * @param nodeAddress
	   * @return ClsNodeDocument[]
	   */
	 private ClsNodeDocument[] Download(String version, String token, String transID, String dataFlow, ClsNodeDocument[] docs,String nodeAddress) {
			URL node = null;
			ClsNodeDocument[] result = null;
		try {
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				result = requestor.download(token, transID, dataFlow, docs);
			}else{
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
			    
			    result = requestor.download(token, dataFlow, transID, docs);
			}
		}catch (Exception e) {
			this.Log("GetStatusTask>>> Error - Download " + e.getMessage(), Level.ERROR);
		}
		return result;
	}

	  /**
	   * CallGetStatus
	   * @param docs
	   * @param db
	   * @return BLOB
	   */
	 private BLOB getZipFileBlob(ClsNodeDocument[] docs,IDBAdapter db) {
		 BLOB bl = null;
		 NodeUtils util = new NodeUtils();
		 byte[] tmp = null;
		 
		 try {
			if(docs != null && docs.length>0){
				tmp = util.Zip(docs);
			}
			 bl = this.nodeDB.CreateBLOB(db, tmp);
			 tmp = null;
		 } catch (Exception e) {
				this.Log("GetStatusTask>>> Error - getZipDoc " + e.getMessage(), Level.ERROR);
		 }

		 return bl;
	 }

	  /**
	   * updateStatusFile
	   * @param blob
	   * @param docs
	   * @param db
	   * @param transID
	   * @param status
	   * @return boolean
	   */
	 private boolean updateStatusFile(BLOB blob, ClsNodeDocument[] docs,IDBAdapter db,String transID,String status) {
		 boolean ret = false;
		 ResultSet rs = null;
		 try {
			blob = this.getZipFileBlob(docs, db);
		    String sql = "select a.* from NODE_OPERATION_MANAGER a WHERE a.TRANS_ID = '"+ transID +"'";
		    rs = db.GetResultSet(sql);
		    while (rs.next()) {
		    	rs.updateString("STATUS_CD", status);
		        ((OracleResultSet)rs).updateBLOB("FILE_CONTENT",blob);
			    rs.updateRow();
		    }
		    ret = true;
		 } catch (Exception e) {
				this.Log("GetStatusTask>>> Error - updateStatusFile " + e.getMessage(), Level.ERROR);
		 } finally {
				try {
			        if (rs != null)
			        	rs.close();
				} catch (Exception e) {
					this.Log(e.toString(), Level.ERROR);
				}
			}
		 return ret;
	 }

	 /**
	   * getElementValue
	   * @param node
	   * @param xPath
	   * @return String
	   */
	 private String getElementValue(Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			value = attNode.getText();			
		}
		
		return value;
	}

	 /**
	   * getAttributeValue
	   * @param node
	   * @param xPath
	   * @return String
	   */
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
	   * Log
	   * @param message
	   * @param level
	   * @return 
	   */
	protected void Log (String message, Level level)
	  {
	    LoggingUtils.Log(message, level, Phrase.AdministrationLoggerName);
	  }	  
}
