package Node.DB.Oracle;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.sql.Blob;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Iterator;
import java.util.TimeZone;

import oracle.jdbc.OracleResultSet;
import oracle.sql.BLOB;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Administration.Document;
import Node.DB.Interfaces.INodeFileCabin;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
/**
 * <p>This class create NodeFileCabin.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeFileCabin extends NodeDB implements INodeFileCabin {
  private String TableName = "NODE_FILE_CABIN";
  private String ID = "FILE_ID";
  private String DocumentID = "DOCUMENT_ID";
  private String TransID = "TRANS_ID";
  private String Name = "FILE_NAME";
  private String Type = "FILE_TYPE";
  private String Status = "STATUS_CD";
  private String DataFlow = "DATAFLOW_NAME";
  private String SubmitURL = "SUBMIT_URL";
  private String Token = "SUBMIT_TOKEN";
  private String SubmitDate = "SUBMIT_DTTM";
  private String Content = "FILE_CONTENT";
  private String Size = "FILE_SIZE";
  private String CreatedDate = "CREATED_DTTM";
  private String CreatedBy = "CREATED_BY";
  private String UpdatedDate = "UPDATED_DTTM";
  private String UpdatedBy = "UPDATED_BY";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeFileCabin(String loggerName) {
    super(loggerName);
  }

  /**
   * GetDocuments
   * @param tIDorDataFlow
   * @param isTransID
   * @return ClsNodeDocument[]
   */
  public ClsNodeDocument[] GetDocuments(String tIDorDataFlow, boolean isTransID) {
    ClsNodeDocument[] retDocuments = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = null;
      if (isTransID) {
        sql = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID},new String[]{tIDorDataFlow});
     //   sql += " and " + this.DataFlow + " is NULL";
      }
      else
        sql = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content},this.TableName,new String[]{this.DataFlow},new String[]{tIDorDataFlow});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retDocuments = new ClsNodeDocument[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retDocuments.length; i++) {
          retDocuments[i] = new ClsNodeDocument();
          retDocuments[i].setId(rs.getString(this.DocumentID));
          retDocuments[i].setName(rs.getString(this.Name));
          retDocuments[i].setType(rs.getString(this.Type));
          retDocuments[i].setContent(this.GetBlobBytes(((OracleResultSet)rs).getBLOB(this.Content)));
        }
      }
    }
    catch (Exception e) {
      this.LogException("Could not Get Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDocuments;
  }

  /**
   * GetDocuments
   * @param transID
   * @param dataFlow
   * @return ClsNodeDocument[]
   */
  public ClsNodeDocument[] GetDocuments(String transID, String dataFlow) {
    ClsNodeDocument[] retDocuments = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID,this.DataFlow},new String[]{transID,dataFlow});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retDocuments = new ClsNodeDocument[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retDocuments.length; i++) {
          retDocuments[i] = new ClsNodeDocument();
          retDocuments[i].setId(rs.getString(this.DocumentID));
          retDocuments[i].setName(rs.getString(this.Name));
          retDocuments[i].setType(rs.getString(this.Type));
          retDocuments[i].setContent(this.GetBlobBytes(((OracleResultSet)rs).getBLOB(this.Content)));
        }
      }
    }
    catch (Exception e) {
      this.LogException("Could not Get Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDocuments;
  }

  /**
   * GetDocuments
   * @param searchDocs
   * @return ClsNodeDocument[]
   */
  public ClsNodeDocument[] GetDocuments(ClsNodeDocument[] searchDocs) {
    if (searchDocs == null || searchDocs.length == 0)
      return null;
    ClsNodeDocument[] retDocuments = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[][] whereVals = new String[1][searchDocs.length];
      for (int i = 0; i < searchDocs.length; i++)
        whereVals[0][i] = searchDocs[i].getName();
      String sql = this.GetSelectOrStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.Name},whereVals);
      sql += " and (" + this.DataFlow + " is NULL)";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retDocuments = new ClsNodeDocument[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retDocuments.length; i++) {
          retDocuments[i] = new ClsNodeDocument();
          retDocuments[i].setId(rs.getString(this.DocumentID));
          retDocuments[i].setName(rs.getString(this.Name));
          retDocuments[i].setType(rs.getString(this.Type));
          retDocuments[i].setContent(this.GetBlobBytes(((OracleResultSet)rs).getBLOB(this.Content)));
        }
      }
    }
    catch (Exception e) {
      this.LogException("Could not Get Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDocuments;
  }

  /**
   * GetDocuments
   * @param transID
   * @param dataFlow
   * @param searchDocs
   * @return ClsNodeDocument[]
   */
  public ClsNodeDocument[] GetDocuments(String transID, String dataFlow,
                                        ClsNodeDocument[] searchDocs) {
    ClsNodeDocument[] retDocuments = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.PrepareSQL(transID, dataFlow, searchDocs);
      if (sql != null) {
        db = this.GetNodeDB();
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() > 0) {
          retDocuments = new ClsNodeDocument[rs.getRow()];
          rs.beforeFirst();
          for (int i = 0; rs.next() && i < retDocuments.length; i++) {
            retDocuments[i] = new ClsNodeDocument();
			retDocuments[i].setId(rs.getString(this.DocumentID));
            retDocuments[i].setName(rs.getString(this.Name));
            retDocuments[i].setType(rs.getString(this.Type));
            retDocuments[i].setContent(this.GetBlobBytes( ( (OracleResultSet)
                rs).getBLOB(this.Content)));
          }
        }
      }
    }
    catch (Exception e) {
      this.LogException("Could not Get Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDocuments;
  }

  /**
   * GetDocuments
   * @param transID
   * @param dataFlow
   * @param operationArr
   * @param searchDocs
   * @return ClsNodeDocument[]
   */
  public ClsNodeDocument[] GetDocuments(String transID, String dataFlow, String[] operationArr,
		  ClsNodeDocument[] searchDocs) {
	  ClsNodeDocument[] retDocuments = null;
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  String sql = "";
	  String operation = "";
	  try {
		  for(int i=0;operationArr!=null && i<operationArr.length;i++){
			  if(i==0){
				  operation += "'"+operationArr[i]+"'";
			  }else{
				  operation += ",'" + operationArr[i]+"'";
			  }
		  }
		  if(searchDocs!=null && searchDocs.length>0){
			  sql  = "select FILE_NAME, FILE_TYPE, FILE_CONTENT,DOCUMENT_ID from NODE_FILE_CABIN where TRANS_ID = '"+transID+ "'";
			  if(operation!=null && !operation.equalsIgnoreCase("") && !operation.equalsIgnoreCase("default") && dataFlow != null && !dataFlow.equalsIgnoreCase("") && !dataFlow.equalsIgnoreCase("NODE")){
				  sql += " and (DATAFLOW_NAME in ("+operation+")) and (";
				  // WI 21157
			  }else if(operation!=null && !operation.equalsIgnoreCase("") && !operation.equalsIgnoreCase("default") && dataFlow != null && !dataFlow.equalsIgnoreCase("") && dataFlow.equalsIgnoreCase("NODE")){
				  sql += " and (UPPER(DATAFLOW_NAME) in ("+operation+")) and (";
			  }
			  else{
				  sql += " and (";
			  }
			  // WI 20786
			  for(int i=0;i<searchDocs.length;i++){
				  String documentName = searchDocs[i].getName()==null || searchDocs[i].getName().equalsIgnoreCase("null")?"":searchDocs[i].getName().trim();
				  String documentType = searchDocs[i].getType()==null || searchDocs[i].getType().equalsIgnoreCase("null")?"":searchDocs[i].getType().trim();
				  if(searchDocs[i].getId()!=null && !searchDocs[i].getId().trim().equals("")&& !searchDocs[i].getId().trim().equals("null")){
					  if(i==0){
						  sql += "(DOCUMENT_ID = '"+searchDocs[i].getId().trim()+"')";
					  }else{
						  sql += " or (DOCUMENT_ID = '"+searchDocs[i].getId().trim()+"')";
					  }					  
				  }else if(!documentName.equals("") && !documentType.equals("")){
					  if(i==0){
						  sql += "(FILE_NAME = '"+documentName +"' and UPPER(FILE_TYPE) = UPPER('"+documentType+"'))";					  
					  }else{
						  sql += " or (FILE_NAME = '"+documentName+"' and UPPER(FILE_TYPE) = UPPER('"+documentType+"'))";
					  }					  
				  }else if(!documentName.equals("") && documentType.equals("")){
					  if(i==0){
						  sql += "(FILE_NAME = '"+documentName +"')";					  
					  }else{
						  sql += " or (FILE_NAME = '"+documentName +"')";
					  }					  
				  }else if(documentName.equals("") && !documentType.equals("")){
					  if(i==0){
						  sql += "(UPPER(FILE_TYPE) = UPPER('"+documentType+"'))";					  
					  }else{
						  sql += " or (UPPER(FILE_TYPE) = UPPER('"+documentType+"'))";
					  }					  
				  }
			  }			  
			  sql += ")";
		  }else{
			  // WI 21157
			  sql  = "select FILE_NAME, FILE_TYPE, FILE_CONTENT,DOCUMENT_ID from NODE_FILE_CABIN where TRANS_ID = '"+transID+ "'";			  
			  if(operation!=null && !operation.equalsIgnoreCase("") && !operation.equalsIgnoreCase("default") && dataFlow != null && !dataFlow.equalsIgnoreCase("") && !dataFlow.equalsIgnoreCase("NODE")){
				  sql += " and (DATAFLOW_NAME in ("+operation+"))";
			  }else if(operation!=null && !operation.equalsIgnoreCase("") && !operation.equalsIgnoreCase("default") && dataFlow != null && !dataFlow.equalsIgnoreCase("") && dataFlow.equalsIgnoreCase("NODE")){
				  sql += " and (UPPER(DATAFLOW_NAME) in ("+operation+"))";
			  }
		  }
		  if (sql != null) {
			  LoggingUtils.Log("NodeFileCabin>>> sql is: "+sql, Level.DEBUG, this.LoggerName);
			  db = this.GetNodeDB();
			  rs = db.GetResultSet(sql);
			  if (rs != null && rs.last() && rs.getRow() > 0) {
				  retDocuments = new ClsNodeDocument[rs.getRow()];
				  rs.beforeFirst();
				  for (int i = 0; rs.next() && i < retDocuments.length; i++) {
					  retDocuments[i] = new ClsNodeDocument();
					  retDocuments[i].setId(rs.getString(this.DocumentID));
					  retDocuments[i].setName(rs.getString(this.Name));
					  retDocuments[i].setType(rs.getString(this.Type));
					  retDocuments[i].setContent(this.GetBlobBytes( ( (OracleResultSet)
							  rs).getBLOB(this.Content)));
				  }
			  }
		  }
	  }
	  catch (Exception e) {
		  this.LogException("Could not Get Documents: " + e.toString());
	  }
	  finally {
		  try {
			  if (rs != null)
				  rs.close();
			  if (db != null)
				  db.Close();
		  }
		  catch (Exception e) {
			  this.LogException(e.toString());
		  }	
	  }
	  return retDocuments;
  }


  /**
   * UploadDocuments
   * @param docs
   * @param transID
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param token
   * @param submitted
   * @param submittedTS
   * @param user
   * @return boolean
   */
  public boolean UploadDocuments(ClsNodeDocument[] docs, String transID, String status, String dataFlow, String submitURL,
                                 String token, Date submitted, Timestamp submittedTS, String user) {
    boolean retBool = false;
    if (docs == null || docs.length <= 0)
      return retBool;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] {
          this.ID, this.DocumentID, this.TransID, this.Name, this.Type, this.Status,
          this.DataFlow, this.SubmitURL, this.Token, this.SubmitDate,
          this.Content,this.Size,
          this.CreatedDate, this.CreatedBy, this.UpdatedDate, this.UpdatedBy
      };
      String sql = this.GetSelectStr(select,this.TableName,new String[]{"1"},new String[]{"-1"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        for (int i = 0; i < docs.length; i++) {
          rs.moveToInsertRow();
          int newInt = this.GetIncrementID(this.TableName, this.ID);
          rs.updateInt(this.ID, newInt);
          NodeUtils nodeUtils = new NodeUtils();
          rs.updateString(this.TransID,transID != null ? transID : nodeUtils.GenerateTransID(Phrase.UUID));
          rs.updateString(this.Name,docs[i].getName());
          rs.updateString(this.Type,docs[i].getType());
          rs.updateString(this.DocumentID,docs[i].getId());
          rs.updateString(this.Status,status != null && status.length() <= 10 ? status : "A");
          rs.updateString(this.DataFlow, dataFlow);
          rs.updateString(this.SubmitURL, submitURL);
          rs.updateString(this.Token, token);
          if (submitted != null && submittedTS != null) {
            rs.updateDate(this.SubmitDate, submitted);
            rs.updateTimestamp(this.SubmitDate, submittedTS);
          }
          else {
            rs.updateDate(this.SubmitDate,Utility.GetNowDate());
            rs.updateTimestamp(this.SubmitDate,Utility.GetNowTimeStamp());
          }
          ((OracleResultSet)rs).updateBLOB(this.Content,this.CreateBLOB(db,docs[i].getContent()));
          if (docs[i].getContent() != null)
            rs.updateInt(this.Size,docs[i].getContent().length);
          rs.updateDate(this.CreatedDate, Utility.GetNowDate());
          rs.updateTimestamp(this.CreatedDate, Utility.GetNowTimeStamp());
          rs.updateDate(this.UpdatedDate, Utility.GetNowDate());
          rs.updateTimestamp(this.UpdatedDate, Utility.GetNowTimeStamp());
          if (user != null && !user.trim().equals(""))
          {
            rs.updateString(this.CreatedBy, user);
            rs.updateString(this.UpdatedBy, user);
          }
          else
          {
            rs.updateString(this.CreatedBy, "system");
            rs.updateString(this.UpdatedBy, "system");
          }
          rs.insertRow();
        }
        retBool = true;
        
		// Change parameters for search
		NodeUtils utils = new NodeUtils();
		utils.UpdateOperationLogParameter(this.LoggerName,transID,dataFlow,docs);

      }
    }
    catch (Exception e) {
      StringWriter sw = new java.io.StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      this.LogException("Could not Upload Documents: " + sw.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

/*  public boolean UploadHugeDocuments(ClsNodeDocument[] docs, String transID,
			String status, String dataFlow, String submitURL, String token,
			Date submitted, Timestamp submittedTS, String user) {
		boolean retBool = false;
		if (docs == null || docs.length <= 0)
			return retBool;
		IDBAdapter db = null;
		ResultSet rs = null;
		InputStream inStream = null;
		OutputStream os = null;
		ArrayList fileList = new ArrayList();
		NodeUtils nodeUtils = new NodeUtils();
		String sql="";
		Connection conn = null;
		PreparedStatement stmt = null;
		BLOB blob = null;
		try {
			db = this.GetNodeDB();
			db.BeginTransaction();
			conn = db.GetConnection();
				for (int i = 0; i < docs.length; i++) {
					int newInt = this.GetIncrementID(this.TableName, this.ID);
					

					blob = BLOB.createTemporary(conn, true,BLOB.DURATION_SESSION);
					blob.open(BLOB.MODE_READWRITE);
					os = blob.getBinaryOutputStream();

					int byteread = 0;
					int bytesum = 0;
					String filePath = new String(docs[i].getContent());
					File inputfile = new File(filePath);
					if (inputfile.exists()) { // file exist
						inStream = new FileInputStream(filePath); // read source
																	// file
						byte[] buffer = new byte[1024];
						while ((byteread = inStream.read(buffer)) != -1) {
							os.write(buffer, 0, byteread);
							bytesum += byteread;
							System.out.println(bytesum);  
						}
						os.flush();
						fileList.add(filePath);
					}

				    sql = "insert into " + this.TableName + " (" 
	    			+ this.ID +","
	    			+ this.TransID +","
	    			+ this.Name +","
	    			+ this.Type +","
	    			+ this.Status +","
	    			+ this.DataFlow +","
	    			+ this.SubmitURL +","
	    			+ this.Token +","
	    			+ this.SubmitDate +","
	    			+ this.Content +","
	    			+ this.Size +","
	    			+ this.CreatedDate +","
	    			+ this.CreatedBy +","
	    			+ this.UpdatedDate +","
	    			+ this.UpdatedBy +","
	    			+ this.DocumentID +")"
	    			+ " values ("
	    			+ newInt +","
	    			+ "'" +(transID != null ? transID: nodeUtils.GenerateTransID(Phrase.UUID)) +"',"
	    			+ "'" + docs[i].getName() +"',"
	    			+ "'" + docs[i].getType() +"',"
	    			+ "'" + (status != null && status.length() <= 10 ? status : "A") +"',"
	    			+ "'" + dataFlow +"',"
	    			+ "'" + submitURL +"',"
	    			+ "'" + token +"',"
	    			+ "?" +","
	    			+ "?" +","
	    			+ inputfile.length() +","
	    			+ "?" +","
	    			+ "'" + (user != null && !user.trim().equals("")?user:"system")+"',"
	    			+ "?" +","
	    			+ "'" + (user != null && !user.trim().equals("")?user:"system") +"',"
	    			+ docs[i].getId()
	    			+")";
	    
				}
				stmt = conn.prepareStatement(sql);
				stmt.setObject(1, (submitted != null && submittedTS != null?submittedTS:Utility.GetNowTimeStamp()));
				stmt.setObject(2, blob);
				stmt.setObject(3, Utility.GetNowTimeStamp());
				stmt.setObject(4, Utility.GetNowTimeStamp());
				stmt.execute();
				db.CommitTransaction();
				blob = null;
				// Change parameters for search
				nodeUtils.UpdateOperationLogParameter(this.LoggerName, transID,
						dataFlow, docs);
				retBool = true;

			
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				e1.printStackTrace();
			} finally {
					try {
						inStream.close();
						os.close();
						if(fileList!=null){
							Iterator it = fileList.iterator();
							while(it.hasNext()){
								Utility.delFile((String)it.next());
							}
						}
						if (rs != null)
							rs.close();
						if (db != null)
							db.Close();
					} catch (Exception ex) {
						this.LogException(ex.toString());
					}
				}
			StringWriter sw = new java.io.StringWriter();
			e.printStackTrace(new PrintWriter(sw));
			this.LogException("Could not Upload Documents: " + sw.toString());
		} finally {
			try {
				inStream.close();
				os.close();
				if(fileList!=null){
					Iterator it = fileList.iterator();
					while(it.hasNext()){
						Utility.delFile((String)it.next());
					}
				}
				if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return retBool;
	}
*/
  /**
   * UploadHugeDocuments
   * @param docs
   * @param transID
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param token
   * @param submitted
   * @param submittedTS
   * @param user
   * @return boolean
   */
  public boolean UploadHugeDocuments(ClsNodeDocument[] docs, String transID,
			String status, String dataFlow, String submitURL, String token,
			Date submitted, Timestamp submittedTS, String user) {
		boolean retBool = false;
		if (docs == null || docs.length <= 0)
			return retBool;
		IDBAdapter db = null;
		ResultSet rs = null;
		InputStream inStream = null;
		OutputStream os = null;
		ArrayList fileList = new ArrayList();
		try {
			String[] select = new String[] { this.ID, this.DocumentID,
					this.TransID, this.Name, this.Type, this.Status,
					this.DataFlow, this.SubmitURL, this.Token, this.SubmitDate,
					this.Content, this.Size, this.CreatedDate, this.CreatedBy,
					this.UpdatedDate, this.UpdatedBy };
			String sql = this.GetSelectStr(select, this.TableName,
					new String[] { "1" }, new String[] { "-1" });
			db = this.GetNodeDB();
			db.BeginTransaction();
			rs = db.GetResultSet(sql);
			if (rs != null) {
				for (int i = 0; i < docs.length; i++) {
					rs.moveToInsertRow();
					int newInt = this.GetIncrementID(this.TableName, this.ID);
					rs.updateInt(this.ID, newInt);
					NodeUtils nodeUtils = new NodeUtils();
					rs.updateString(this.TransID, transID != null ? transID	: nodeUtils.GenerateTransID(Phrase.UUID));
					rs.updateString(this.Name, docs[i].getName());
					rs.updateString(this.Type, docs[i].getType());
					rs.updateString(this.DocumentID, docs[i].getId());
					rs.updateString(this.Status, status != null && status.length() <= 10 ? status : "A");
					rs.updateString(this.DataFlow, dataFlow);
					rs.updateString(this.SubmitURL, submitURL);
					rs.updateString(this.Token, token);
					if (submitted != null && submittedTS != null) {
						rs.updateDate(this.SubmitDate, submitted);
						rs.updateTimestamp(this.SubmitDate, submittedTS);
					} else {
						rs.updateDate(this.SubmitDate, Utility.GetNowDate());
						rs.updateTimestamp(this.SubmitDate, Utility.GetNowTimeStamp());
					}

					BLOB blob = BLOB.createTemporary(db.GetConnection(), true,
							BLOB.DURATION_SESSION);
					blob.open(BLOB.MODE_READWRITE);
					os = blob.getBinaryOutputStream();

					int byteread = 0;
					String filePath = new String(docs[i].getContent());
					File inputfile = new File(filePath);
					if (inputfile.exists()) { // file exist
						inStream = new FileInputStream(filePath); // read source
																	// file
						byte[] buffer = new byte[1024];
						while ((byteread = inStream.read(buffer)) != -1) {
							os.write(buffer, 0, byteread);
							os.flush();
						}
						fileList.add(filePath);
					}

					((OracleResultSet) rs).updateBLOB(this.Content, blob);
					if (blob != null)
						rs.updateLong(this.Size,inputfile.length());
					//if (docs[i].getContent() != null)
					//rs.updateInt(this.Size,docs[i].getContent().length);

					rs.updateDate(this.CreatedDate, Utility.GetNowDate());
					rs.updateTimestamp(this.CreatedDate, Utility.GetNowTimeStamp());
					rs.updateDate(this.UpdatedDate, Utility.GetNowDate());
					rs.updateTimestamp(this.UpdatedDate, Utility.GetNowTimeStamp());
					if (user != null && !user.trim().equals("")) {
						rs.updateString(this.CreatedBy, user);
						rs.updateString(this.UpdatedBy, user);
					} else {
						rs.updateString(this.CreatedBy, "system");
						rs.updateString(this.UpdatedBy, "system");
					}
					rs.insertRow();
				}
				db.CommitTransaction();
				// Change parameters for search
				NodeUtils utils = new NodeUtils();
				utils.UpdateOperationLogParameter(this.LoggerName, transID,
						dataFlow, docs);
				retBool = true;

			}
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				e1.printStackTrace();
			} finally {
					try {
						inStream.close();
						os.close();
						if(fileList!=null){
							Iterator it = fileList.iterator();
							while(it.hasNext()){
								Utility.delFile((String)it.next());
							}
						}
						if (rs != null)
							rs.close();
						if (db != null)
							db.Close();
					} catch (Exception ex) {
						this.LogException(ex.toString());
					}
				}
			StringWriter sw = new java.io.StringWriter();
			e.printStackTrace(new PrintWriter(sw));
			this.LogException("Could not Upload Documents: " + sw.toString());
		} finally {
			try {
				inStream.close();
				os.close();
				if(fileList!=null){
					Iterator it = fileList.iterator();
					while(it.hasNext()){
						Utility.delFile((String)it.next());
					}
				}
				if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return retBool;
	}

	// WI 22695
  /**
   * UploadHugeDocumentsWithoutDelete
   * @param docs
   * @param transID
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param token
   * @param submitted
   * @param submittedTS
   * @param user
   * @return boolean
   */
  public boolean UploadHugeDocumentsWithoutDelete(ClsNodeDocument[] docs, String transID,
			String status, String dataFlow, String submitURL, String token,
			Date submitted, Timestamp submittedTS, String user) {
		boolean retBool = false;
		if (docs == null || docs.length <= 0)
			return retBool;
		IDBAdapter db = null;
		ResultSet rs = null;
		InputStream inStream = null;
		OutputStream os = null;
		ArrayList fileList = new ArrayList();
		try {
			String[] select = new String[] { this.ID, this.DocumentID,
					this.TransID, this.Name, this.Type, this.Status,
					this.DataFlow, this.SubmitURL, this.Token, this.SubmitDate,
					this.Content, this.Size, this.CreatedDate, this.CreatedBy,
					this.UpdatedDate, this.UpdatedBy };
			String sql = this.GetSelectStr(select, this.TableName,
					new String[] { "1" }, new String[] { "-1" });
			db = this.GetNodeDB();
			db.BeginTransaction();
			rs = db.GetResultSet(sql);
			if (rs != null) {
				for (int i = 0; i < docs.length; i++) {
					rs.moveToInsertRow();
					int newInt = this.GetIncrementID(this.TableName, this.ID);
					rs.updateInt(this.ID, newInt);
					NodeUtils nodeUtils = new NodeUtils();
					rs.updateString(this.TransID, transID != null ? transID	: nodeUtils.GenerateTransID(Phrase.UUID));
					rs.updateString(this.Name, docs[i].getName());
					rs.updateString(this.Type, docs[i].getType());
					rs.updateString(this.DocumentID, docs[i].getId());
					rs.updateString(this.Status, status != null && status.length() <= 10 ? status : "A");
					rs.updateString(this.DataFlow, dataFlow);
					rs.updateString(this.SubmitURL, submitURL);
					rs.updateString(this.Token, token);
					if (submitted != null && submittedTS != null) {
						rs.updateDate(this.SubmitDate, submitted);
						rs.updateTimestamp(this.SubmitDate, submittedTS);
					} else {
						rs.updateDate(this.SubmitDate, Utility.GetNowDate());
						rs.updateTimestamp(this.SubmitDate, Utility.GetNowTimeStamp());
					}

					BLOB blob = BLOB.createTemporary(db.GetConnection(), true,
							BLOB.DURATION_SESSION);
					blob.open(BLOB.MODE_READWRITE);
					os = blob.getBinaryOutputStream();

					int byteread = 0;
					String filePath = new String(docs[i].getContent());
					File inputfile = new File(filePath);
					if (inputfile.exists()) { // file exist
						inStream = new FileInputStream(filePath); // read source
																	// file
						byte[] buffer = new byte[1024];
						while ((byteread = inStream.read(buffer)) != -1) {
							os.write(buffer, 0, byteread);
							os.flush();
						}
						fileList.add(filePath);
					}

					((OracleResultSet) rs).updateBLOB(this.Content, blob);
					if (blob != null)
						rs.updateLong(this.Size,inputfile.length());
					//if (docs[i].getContent() != null)
					//rs.updateInt(this.Size,docs[i].getContent().length);

					rs.updateDate(this.CreatedDate, Utility.GetNowDate());
					rs.updateTimestamp(this.CreatedDate, Utility.GetNowTimeStamp());
					rs.updateDate(this.UpdatedDate, Utility.GetNowDate());
					rs.updateTimestamp(this.UpdatedDate, Utility.GetNowTimeStamp());
					if (user != null && !user.trim().equals("")) {
						rs.updateString(this.CreatedBy, user);
						rs.updateString(this.UpdatedBy, user);
					} else {
						rs.updateString(this.CreatedBy, "system");
						rs.updateString(this.UpdatedBy, "system");
					}
					rs.insertRow();
				}
				db.CommitTransaction();
				// Change parameters for search
				NodeUtils utils = new NodeUtils();
				utils.UpdateOperationLogParameter(this.LoggerName, transID,
						dataFlow, docs);
				retBool = true;

			}
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				e1.printStackTrace();
			} finally {
					try {
						if(inStream != null){
							inStream.close();					
						}
						if(os != null){
							os.close();					
						}
						/* if(fileList!=null){
							Iterator it = fileList.iterator();
							while(it.hasNext()){
								Utility.delFile((String)it.next());
							}
						}*/
						if (rs != null)
							rs.close();
						if (db != null)
							db.Close();
					} catch (Exception ex) {
						this.LogException(ex.toString());
					}
				}
			StringWriter sw = new java.io.StringWriter();
			e.printStackTrace(new PrintWriter(sw));
			this.LogException("Could not Upload Documents: " + sw.toString());
		} finally {
			try {
				if(inStream != null){
					inStream.close();					
				}
				if(os != null){
					os.close();					
				}
				/*if(fileList!=null){
					Iterator it = fileList.iterator();
					while(it.hasNext()){
						Utility.delFile((String)it.next());
					}
				}*/
				if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return retBool;
	}

  /**
   * QueryDocs
   * @param transID
   * @param dataFlow
   * @param names
   * @return XmlDocument
   */
  public XmlDocument QueryDocs(String transID, String dataFlow, String[] names) {
    XmlDocument retDoc = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.PrepareSQL(transID, dataFlow, names);
      if (sql != null) {
        db = this.GetNodeDB();
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() > 0) {
          StringBuffer xml = new StringBuffer("");
          xml.append("<Documents>\n");
          rs.beforeFirst();
          while (rs.next()) {
            xml.append("\t<Document>\n");
            xml.append("\t\t<Name>" + rs.getString(this.Name) + "</Name>\n");
            xml.append("\t\t<Type>" + rs.getString(this.Type) + "</Type>\n");
            xml.append("\t</Document>\n");
          }
          xml.append("</Documents>");
          retDoc = new XmlDocument();
          retDoc.LoadXml(xml.toString());
        }
      }
    }
    catch (Exception e) {
      this.LogException("Could not Query Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDoc;
  }

  /**
   * SearchDocuments
   * @param docName
   * @param transID
   * @param domainName
   * @param opName
   * @param start
   * @param end
   * @param adminDomains
   * @param version_no
   * @return Document[]
   */
  public Document[] SearchDocuments (String docName, String transID, String domainName, String opName, Date start, Date end, String[] adminDomains, String version_no)
  {
    Document[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    if (version_no == null || version_no.equals(""))
    	version_no = "VER_11";
    try {
      String sql = "select unique(A."+this.ID+"),A."+this.CreatedBy+",A."+this.CreatedDate+",A."+this.DataFlow;
      sql += ",A."+this.Name+",A."+this.Size+",A."+this.Status+",A."+this.SubmitDate+",A."+this.SubmitURL;
      sql += ",A."+this.Token+",A."+this.TransID+",A."+this.Type+",A."+this.UpdatedBy+",A."+this.UpdatedDate+",C."+this.NodeDomain;
      sql += " from "+this.TableName+" A";
      sql += " left join "+this.OperationTableName+" B on A."+this.DataFlow+" = B."+this.OperationName;
      sql += " left join "+this.NodeDomainTableName+" C on B."+this.NodeDomainID+" = C."+this.NodeDomainID;
      boolean needAnd = false;
      if (docName != null && !docName.equals("")) {
        sql += " where upper(A."+this.Name+") like upper('%"+docName+"%')";
        needAnd = true;
      }
      if (transID != null && !transID.equals("")) {
        if (needAnd) sql += " and";
        else sql += " where";
        sql += " A."+this.TransID+" like '%"+transID+"%'";
        needAnd = true;
      }
      if (domainName != null && !domainName.equals("")) {
        if (needAnd) sql += " and";
        else sql += " where";
        sql += " C."+this.NodeDomain+" = '"+domainName+"'";
        needAnd = true;
      }
      if (opName != null && !opName.equals("")) {
        if (needAnd) sql += " and";
        else sql += " where";
        sql += " A."+this.DataFlow+" = '"+opName+"'";
        needAnd = true;
      }
      if (start != null) {
        if (needAnd) sql += " and";
        else sql += " where";
        SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
        String temp = dateFormat.format(start);
        sql += " A." + this.SubmitDate + " >= '" + temp + "'";
        needAnd = true;
      }
      if (end != null) {
        if (needAnd) sql += " and";
        else sql += " where";
        SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
        cal.setTime(end);
        cal.add(Calendar.DAY_OF_MONTH, 1);
        java.util.Date d = cal.getTime();
        String temp = dateFormat.format(d);
        sql += " A." + this.SubmitDate + " <= '" + temp + "'";
        needAnd = true;
      }
      if (adminDomains != null && adminDomains.length > 0)
      {
        if (needAnd) sql += " and";
        else sql += " where";
        sql += " C."+this.NodeDomain+" in (";
        for (int i = 0; i < adminDomains.length; i++)
        {
          if (i != 0) sql += ", ";
          sql += "'" + adminDomains[i] + "'";
        }
        sql += ")";
        needAnd = true;
      }
      sql += " B." + this.VersionNo + " = '" + version_no + "'";
      sql += " order by A."+this.SubmitDate+" desc";
      /*
      String sql = "select A.*, C."+this.OperationName+", D."+this.NodeDomain;
      if ((opName != null && !opName.equals("")) || (domainName != null && !domainName.equals(""))) {
        sql += " from "+this.TableName+" A,"+this.OperationLogTableName+" B,"+this.OperationTableName+" C,";
        sql += this.NodeDomainTableName+" D";
        sql += " where A."+this.TransID+" = B."+this.TransID+" and B."+this.OperationID+" = C."+this.OperationID;
        sql += " and C." + this.NodeDomainID + " = D." + this.NodeDomainID;
        if (docName != null && !docName.equals(""))
          sql += " and upper(A." + this.Name + ") like upper('%" + docName +"%')";
        if (transID != null && !transID.equals(""))
          sql += " and A." + this.TransID + " like '%" + transID + "%'";
        if (domainName != null && !domainName.equals(""))
          sql += " and D." + this.NodeDomain + " = '" + domainName + "'";
        if (opName != null && !opName.equals(""))
          sql += " and C." + this.OperationName + " = '" + opName + "'";
        if (start != null) {
          SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
          String temp = dateFormat.format(start);
          sql += " and A." + this.SubmitDate + " >= '" + temp + "'";
        }
        if (end != null) {
          SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
          Calendar cal = Calendar.getInstance(TimeZone.getDefault());
          cal.setTime(end);
          cal.add(Calendar.DAY_OF_MONTH, 1);
          java.util.Date d = cal.getTime();
          String temp = dateFormat.format(d);
          sql += " and A." + this.SubmitDate + " <= '" + temp + "'";
        }
      }
      else {
        sql += " from "+this.TableName+" A";
        sql += " left join "+this.OperationLogTableName+" B on A."+this.TransID+" = B."+this.TransID;
        sql += " left join "+this.OperationTableName+" C on B."+this.OperationID+" = C."+this.OperationID;
        sql += " left join "+this.NodeDomainTableName+" D on C."+this.NodeDomainID+" = D."+this.NodeDomainID;
        boolean needAnd = false;
        if (docName != null && !docName.equals("")) {
          sql += " where upper(A."+this.Name+") like upper('%"+docName+"%')";
          needAnd = true;
        }
        if (transID != null && !transID.equals("")) {
          if (needAnd) sql += " and";
          else sql += " where";
          sql += " A."+this.TransID+" like '%"+transID+"%'";
          needAnd = true;
        }
        if (start != null) {
          if (needAnd) sql += " and";
          else sql += " where";
          SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
          String temp = dateFormat.format(start);
          sql += " A." + this.SubmitDate + " >= '" + temp + "'";
          needAnd = true;
        }
        if (end != null) {
          if (needAnd) sql += " and";
          else sql += " where";
          SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MMM-yyyy");
          Calendar cal = Calendar.getInstance(TimeZone.getDefault());
          cal.setTime(end);
          cal.add(Calendar.DAY_OF_MONTH, 1);
          java.util.Date d = cal.getTime();
          String temp = dateFormat.format(d);
          sql += " A." + this.SubmitDate + " <= '" + temp + "'";
          needAnd = true;
        }
      }*/
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Document[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          retArray[i] = new Document(rs.getInt(this.ID));
          retArray[i].SetCreatedBy(rs.getString(this.CreatedBy));
          retArray[i].SetCreatedDate(rs.getString(this.CreatedDate));
          retArray[i].SetDataFlow(rs.getString(this.DataFlow));
          retArray[i].SetDomain(rs.getString(this.NodeDomain));
          retArray[i].SetName(rs.getString(this.Name));
          retArray[i].SetOperation(rs.getString(this.DataFlow));
          retArray[i].SetSize(rs.getInt(this.Size));
          retArray[i].SetStatus(rs.getString(this.Status));
          retArray[i].SetSubmitDate(rs.getString(this.SubmitDate));
          retArray[i].SetSubmitURL(rs.getString(this.SubmitURL));
          retArray[i].SetToken(rs.getString(this.Token));
          retArray[i].SetTransID(rs.getString(this.TransID));
          retArray[i].SetType(rs.getString(this.Type));
          retArray[i].SetUpdatedBy(rs.getString(this.UpdatedBy));
          retArray[i].SetUpdatedDate(rs.getString(this.UpdatedDate));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Search Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

  /**
   * GetOperationNames
   * @param domains
   * @return String[]
   */
  public String[] GetOperationNames (String[] domains)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "unique(A."+this.OperationName+")" };
      String tableNames = this.OperationTableName+" A,"+this.NodeDomainTableName+" B";
      String condition = "A."+this.NodeDomainID+" = B."+this.NodeDomainID;
      if (domains != null && domains.length > 0) {
        condition += " and B."+this.NodeDomain+" in (";
        for (int i = 0; i < domains.length; i++) {
          if (i != 0) condition += ",";
          condition += "'" + domains[i] + "'";
        }
        condition += ")";
      }
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OperationName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation Names: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

  /**
   * GetDocument
   * @param fileID
   * @return Document
   */
  public Document GetDocument (int fileID)
  {
    Document retDoc = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A.*, D."+this.NodeDomain+", C."+this.OperationName+", E."+this.WebService;
      sql += " from "+this.TableName+" A";
      sql += " left join "+this.OperationLogTableName+" B on A."+this.TransID+" = B."+this.TransID;
      sql += " left join "+this.OperationTableName+" C on B."+this.OperationID+" = C."+this.OperationID;
      sql += " left join "+this.NodeDomainTableName+" D on C."+this.NodeDomainID+" = D."+this.NodeDomainID;
      sql += " left join "+this.WebServiceTableName+" E on C."+this.WebServiceID+" = E."+this.WebServiceID;
      sql += " where "+this.ID+" = "+fileID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        retDoc = new Document(rs.getInt(this.ID));
        retDoc.SetContent(this.GetBlobBytes((BLOB)rs.getBlob(this.Content)));
        retDoc.SetCreatedBy(rs.getString(this.CreatedBy));
        retDoc.SetCreatedDate(rs.getString(this.CreatedDate));
        retDoc.SetDataFlow(rs.getString(this.DataFlow));
        retDoc.SetDomain(rs.getString(this.NodeDomain));
        retDoc.SetName(rs.getString(this.Name));
        retDoc.SetOperation(rs.getString(this.OperationName));
        retDoc.SetWebService(rs.getString(this.WebService));
        retDoc.SetSize(rs.getInt(this.Size));
        retDoc.SetStatus(rs.getString(this.Status));
        retDoc.SetSubmitDate(rs.getString(this.SubmitDate));
        retDoc.SetSubmitURL(rs.getString(this.SubmitURL));
        retDoc.SetToken(rs.getString(this.Token));
        retDoc.SetTransID(rs.getString(this.TransID));
        retDoc.SetType(rs.getString(this.Type));
        retDoc.SetUpdatedBy(rs.getString(this.UpdatedBy));
        retDoc.SetUpdatedDate(rs.getString(this.UpdatedDate));
      }
    } catch (Exception e) {
      this.LogException("Could not Get Document: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDoc;
  }
  
  // The content of Document object is the temporary file path, not real data
  /**
   * GetHugeDocument
   * @param fileID
   * @return Document
   */
  public Document GetHugeDocument (int fileID)
  {
    Document retDoc = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A.*, D."+this.NodeDomain+", C."+this.OperationName+", E."+this.WebService;
      sql += " from "+this.TableName+" A";
      sql += " left join "+this.OperationLogTableName+" B on A."+this.TransID+" = B."+this.TransID;
      sql += " left join "+this.OperationTableName+" C on B."+this.OperationID+" = C."+this.OperationID;
      sql += " left join "+this.NodeDomainTableName+" D on C."+this.NodeDomainID+" = D."+this.NodeDomainID;
      sql += " left join "+this.WebServiceTableName+" E on C."+this.WebServiceID+" = E."+this.WebServiceID;
      sql += " where "+this.ID+" = "+fileID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        retDoc = new Document(rs.getInt(this.ID));
        Blob blob = rs.getBlob(this.Content);
        String prefix = rs.getString(this.Name)+rs.getString(this.TransID);
        String suffix = rs.getString(this.Type);
        File tmpFile = null;
        if(blob != null){
            tmpFile = NodeUtils.createTempFile(prefix,suffix);
            InputStream is = blob.getBinaryStream();
            Utility.writeFile(tmpFile.getAbsolutePath(), is);
        }

        retDoc.SetContent(tmpFile.getAbsolutePath().getBytes());
        retDoc.SetCreatedBy(rs.getString(this.CreatedBy));
        retDoc.SetCreatedDate(rs.getString(this.CreatedDate));
        retDoc.SetDataFlow(rs.getString(this.DataFlow));
        retDoc.SetDomain(rs.getString(this.NodeDomain));
        retDoc.SetName(rs.getString(this.Name));
        retDoc.SetOperation(rs.getString(this.OperationName));
        retDoc.SetWebService(rs.getString(this.WebService));
        retDoc.SetSize(rs.getInt(this.Size));
        retDoc.SetStatus(rs.getString(this.Status));
        retDoc.SetSubmitDate(rs.getString(this.SubmitDate));
        retDoc.SetSubmitURL(rs.getString(this.SubmitURL));
        retDoc.SetToken(rs.getString(this.Token));
        retDoc.SetTransID(rs.getString(this.TransID));
        retDoc.SetType(rs.getString(this.Type));
        retDoc.SetUpdatedBy(rs.getString(this.UpdatedBy));
        retDoc.SetUpdatedDate(rs.getString(this.UpdatedDate));
      }
    } catch (Exception e) {
      this.LogException("Could not Get Document: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDoc;
  }

  /**
   * RemoveDocuments
   * @param fileIDs
   * @return boolean
   */
  public boolean RemoveDocuments (int[] fileIDs)
  {
    if (fileIDs == null || fileIDs.length <= 0)
      return true;
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "delete from "+this.TableName+" where "+this.ID+" in (";
      for (int i = 0; i < fileIDs.length; i++) {
        if (i != 0) sql += ",";
        sql += fileIDs[i];
      }
      sql += ")";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Remove Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

  /**
   * RemoveDocuments
   * @param transID
   * @param names
   * @return boolean
   */
  public boolean RemoveDocuments (String transID, String[] names)
  {
    if ((transID == null || transID.equals("")) && (names == null || names.length <= 0))
      return true;
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "delete from "+this.TableName+" A";
      boolean needAnd = false;
      if (transID != null && !transID.equals("")) {
        sql += " where A."+this.TransID+" = '"+transID+"'";
        needAnd = true;
      }
      if (names != null && names.length > 0) {
        if (needAnd) sql += " and";
        else sql += " where";
        sql += " A."+this.Name+" in (";
        for (int i = 0; i < names.length; i++) {
          if (i != 0) sql += ",";
          sql += "'"+names[i]+"'";
        }
        sql += ")";
      }
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Remove Documents: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }
  
  /**
   * GetDocumentTransactionID
   * @param fileID
   * @return String
   */
  public String GetDocumentTransactionID (int fileID)
  {
    String transID = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select A.TRANS_ID ";
      sql += " from "+this.TableName+" A";
      sql += " where "+this.ID+" = "+fileID;
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
    	  transID = rs.getString("TRANS_ID");
      }
    } catch (Exception e) {
      this.LogException("Could not Get Document: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return transID;
  }

  /**
   * SaveDocument
   * @param fileID
   * @param documentID
   * @param transID
   * @param fileName
   * @param fileType
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param submitToken
   * @param submitDate
   * @param content
   * @param user
   * @return String
   */
  public String SaveDocument (int fileID, String documentID,String transID, String fileName, String fileType, String status, String dataFlow,
                               String submitURL, String submitToken, Date submitDate, byte[] content, String user)
  {
    String retReason = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      db = this.GetNodeDB();
      String sql = "select " + this.ID + ", " + this.DocumentID + ", "+ this.TransID + ", " + this.Name + ", " + this.Type + ", " + this.Status;
      sql += ", " + this.DataFlow + ", " + this.SubmitURL + ", " + this.Token + ", " + this.SubmitDate + ", " + this.Content;
      sql += ", " + this.Size + ", " + this.CreatedDate + ", " + this.CreatedBy + ", " + this.UpdatedDate + ", " + this.UpdatedBy;
      sql += " from " + this.TableName;
      sql += " where " + this.ID + " = " + fileID;
      rs = db.GetResultSet(sql);
      if (rs != null)
      {
        boolean isUpdate = true;
        // Insert
        if (!rs.last() || rs.getRow() <= 0)
        {
            isUpdate = false;
            rs.moveToInsertRow();
            int id = fileID;
            if (id < 0)
              id = this.GetIncrementID(this.TableName, this.ID);
            rs.updateInt(this.ID, id);
        }
        rs.updateString(this.DocumentID, documentID);
        rs.updateString(this.TransID, transID);
        rs.updateString(this.Name, fileName);
        rs.updateString(this.Type, fileType);
        rs.updateString(this.Status, status);
        rs.updateString(this.DataFlow, dataFlow);
        rs.updateString(this.SubmitURL, submitURL);
        rs.updateString(this.Token, submitToken);
		  if (submitDate != null)
		  {
			  rs.updateDate(this.SubmitDate, submitDate);
		  }
        if (content != null)
        {
          ((OracleResultSet)rs).updateBLOB(this.Content, this.CreateBLOB(db, content));
          rs.updateInt(this.Size, content.length);
        }
        else
        {
          rs.updateBlob(this.Content, this.CreateBLOB(db, content));
          rs.updateInt(this.Size, 0);
        }
        rs.updateDate(this.UpdatedDate, Utility.GetNowDate());
        rs.updateTimestamp(this.UpdatedDate, Utility.GetNowTimeStamp());
        if (user != null && !user.trim().equals(""))
          rs.updateString(this.UpdatedBy, user);
        else
          rs.updateString(this.UpdatedBy, "system");
        if (!isUpdate)
        {
          rs.updateDate(this.CreatedDate, Utility.GetNowDate());
          rs.updateTimestamp(this.CreatedDate, Utility.GetNowTimeStamp());
          if (user != null && !user.trim().equals(""))
            rs.updateString(this.CreatedBy, user);
          else
            rs.updateString(this.CreatedBy, "system");
          rs.insertRow();
        }
        else
          rs.updateRow();
      }
    } catch (Exception e) {
      this.LogException("Could not Save Document: " + e.toString());
      retReason = e.toString();
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      }
      catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retReason;
  }

  // This is only for version1.1 table without documentID
  /**
   * SaveDocument
   * @param fileID
   * @param transID
   * @param fileName
   * @param fileType
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param submitToken
   * @param submitDate
   * @param content
   * @param user
   * @return String
   */
  public String SaveDocument (int fileID, String transID, String fileName, String fileType, String status, String dataFlow,
		  String submitURL, String submitToken, Date submitDate, byte[] content, String user)
  {
	  String retReason = null;
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  try {
		  db = this.GetNodeDB();
		  String sql = "select " + this.ID + ", " + this.TransID + ", " + this.Name + ", " + this.Type + ", " + this.Status;
		  sql += ", " + this.DataFlow + ", " + this.SubmitURL + ", " + this.Token + ", " + this.SubmitDate + ", " + this.Content;
		  sql += ", " + this.Size + ", " + this.CreatedDate + ", " + this.CreatedBy + ", " + this.UpdatedDate + ", " + this.UpdatedBy;
		  sql += " from " + this.TableName;
		  sql += " where " + this.ID + " = " + fileID;
		  rs = db.GetResultSet(sql);
		  if (rs != null)
		  {
			  boolean isUpdate = true;
			  // Insert
			  if (!rs.last() || rs.getRow() <= 0)
			  {
				  isUpdate = false;
				  rs.moveToInsertRow();
				  int id = fileID;
				  if (id < 0)
					  id = this.GetIncrementID(this.TableName, this.ID);
				  rs.updateInt(this.ID, id);
			  }
			  rs.updateString(this.TransID, transID);
			  rs.updateString(this.Name, fileName);
			  rs.updateString(this.Type, fileType);
			  rs.updateString(this.Status, status);
			  rs.updateString(this.DataFlow, dataFlow);
			  rs.updateString(this.SubmitURL, submitURL);
			  rs.updateString(this.Token, submitToken);
			  if (submitDate != null)
			  {
				  rs.updateDate(this.SubmitDate, submitDate);
			  }
			  if (content != null)
			  {
				  ((OracleResultSet)rs).updateBLOB(this.Content, this.CreateBLOB(db, content));
				  rs.updateInt(this.Size, content.length);
			  }
			  else
			  {
				  rs.updateBlob(this.Content, this.CreateBLOB(db, content));
				  rs.updateInt(this.Size, 0);
			  }
			  rs.updateDate(this.UpdatedDate, Utility.GetNowDate());
			  rs.updateTimestamp(this.UpdatedDate, Utility.GetNowTimeStamp());
			  if (user != null && !user.trim().equals(""))
				  rs.updateString(this.UpdatedBy, user);
			  else
				  rs.updateString(this.UpdatedBy, "system");
			  if (!isUpdate)
			  {
				  rs.updateDate(this.CreatedDate, Utility.GetNowDate());
				  rs.updateTimestamp(this.CreatedDate, Utility.GetNowTimeStamp());
				  if (user != null && !user.trim().equals(""))
					  rs.updateString(this.CreatedBy, user);
				  else
					  rs.updateString(this.CreatedBy, "system");
				  rs.insertRow();
			  }
			  else
				  rs.updateRow();
		  }
	  } catch (Exception e) {
		  this.LogException("Could not Save Document: " + e.toString());
		  retReason = e.toString();
	  }
	  finally {
		  try {
			  if (rs != null)
				  rs.close();
			  if (db != null)
				  db.Close();
		  }
		  catch (Exception e) {
			  this.LogException(e.toString());
		  }
	  }
	  return retReason;
  }

  /**
   * PrepareSQL
   * @param transID
   * @param dataFlow
   * @param docs
   * @return String
   */
  private String PrepareSQL (String transID, String dataFlow, ClsNodeDocument[] docs) {
    String[] names = null;
    if (docs != null && docs.length > 0) {
      names = new String[docs.length];
      for (int i = 0; i < docs.length; i++)
        names[i] = docs[i].getName();
    }
    return this.PrepareSQL(transID, dataFlow, names);
  }

  /**
   * PrepareSQL
   * @param transID
   * @param dataFlow
   * @param names
   * @return String
   */
  private String PrepareSQL(String transID, String dataFlow, String[] names) {
    String retString = null;
    if (transID != null) {
    // WI 21591
      if (dataFlow != null && !dataFlow.equals("")) {
        if (names != null && names.length > 0) { // transID, dataFlow, names
          String[][] vals = new String[3][];
          vals[0] = new String[] {
              transID};
          vals[1] = new String[] {
              dataFlow};
          vals[2] = new String[names.length];
          for (int i = 0; i < names.length; i++)
            vals[2][i] = names[i];
          retString = this.GetSelectOrStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID,this.DataFlow,this.Name},vals);
        }
        else // transID, dataFlow
          retString = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID,this.DataFlow},new String[]{transID,dataFlow});
      }
      else {
        if (names != null && names.length > 0) { // transID, names
          String[][] vals = new String[2][];
          vals[0] = new String[] {
              transID};
          vals[1] = new String[names.length];
          for (int i = 0; i < names.length; i++)
            vals[1][i] = names[i];
          retString = this.GetSelectOrStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID,this.Name},vals);
        }
        else // transID
          retString = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.TransID},new String[]{transID});
      }
    }
    else {
      // WI 21591
      if (dataFlow != null && !dataFlow.equals("")) {
        if (names != null && names.length > 0) { // dataFlow, names
          String[][] vals = new String[2][];
          vals[0] = new String[] {
              dataFlow};
          vals[1] = new String[names.length];
          for (int i = 0; i < names.length; i++)
            vals[1][i] = names[i];
          retString = this.GetSelectOrStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.DataFlow,this.Name},vals);
        }
        else // dataFlow
          retString = this.GetSelectStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.DataFlow},new String[]{dataFlow});
      }
      else {
        if (names != null && names.length > 0) { // names
          String[][] vals = new String[1][names.length];
          for (int i = 0; i < names.length; i++)
            vals[0][i] = names[i];
          retString = this.GetSelectOrStr(new String[]{this.Name,this.Type,this.Content,this.DocumentID},this.TableName,new String[]{this.Name},vals);
        }
      }
    }
    return retString;
  }
}
