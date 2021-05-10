package Node.DB.Oracle10i;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.jndi.JNDIAccess;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.Writer;
import oracle.sql.BLOB;
import oracle.sql.CLOB;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;

import Node.Phrase;
import Node.Configuration.JNDI.JNDIResources;
import Node.DB.Interfaces.INodeDB;
import Node.DB.Oracle.SequenceNo;
/**
 * <p>This class create NodeDB.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeDB implements INodeDB {
  protected String LoggerName = null;

  // Table Names
  protected String SysConfigTableName = "SYS_CONFIG";
  protected String SysUserInfoTableName = "SYS_USER_INFO";
  protected String SysAddressTableName = "SYS_ADDRESS";
  protected String SysEmailTableName = "SYS_EMAIL";
  protected String SysUserAddressTableName = "SYS_USER_ADDRESS";
  protected String SysUserEmailTableName = "SYS_USER_EMAIL";
  protected String NodeAccountTypeTableName = "NODE_ACCOUNT_TYPE";
  protected String NodeAccountTypeXREFTableName = "NODE_ACCOUNT_TYPE_XREF";
  protected String NodeDomainTableName = "NODE_DOMAIN";
  protected String UserOpXREFTableName = "NODE_USER_OPERATION_XREF";
  protected String OperationTableName = "NODE_OPERATION";
  protected String OperationLogTableName = "NODE_OPERATION_LOG";
  protected String OperationLogStatusTableName = "NODE_OPERATION_LOG_STATUS";
  protected String OperationLogParameterTableName = "NODE_OPERATION_LOG_PARAMETER";
  protected String WebServiceTableName = "NODE_WEB_SERVICE";
  protected String DomainWSXREFTableName = "NODE_DOMAIN_WEB_SERVICE_XREF";
  protected String SysUserPageLayoutTableName = "SYS_USER_PAGELAYOUT";

  // Column Names
  // General
  protected String CreatedDate = "CREATED_DTTM";
  protected String CreatedBy = "CREATED_BY";
  protected String UpdatedDate = "UPDATED_DTTM";
  protected String UpdatedBy = "UPDATED_BY";
  // SYS_CONFIG
  protected String SysConfigConfigName = "CONFIG_NAME";
  protected String SysConfigConfigXML = "CONFIG_XML";
  // SYS_USER_INFO
  protected String SysUserInfoUserID = "USER_ID";
  protected String SysUserInfoLastName = "LAST_NAME";
  protected String SysUserInfoFirstName = "FIRST_NAME";
  protected String SysUserInfoMiddleInitial = "MIDDLE_INITIAL";
  protected String SysUserInfoLoginName = "LOGIN_NAME";
  protected String SysUserInfoLoginPassword = "LOGIN_PASSWORD";
  protected String SysUserInfoUserStatusCD = "USER_STATUS_CD";
  protected String SysUserInfoLast4SSN = "LAST_4_SSN";
  protected String SysUserInfoPhone = "PHONE_NUMBER";
  protected String SysUserInfoComments = "COMMENTS";
  // SYS_ADDRESS
  protected String SysAddressID = "ADDRESS_ID";
  protected String SysAddress = "ADDRESS";
  protected String SysAddressSuppl = "SUPPLEMENTAL_ADDRESS";
  protected String SysAddressCity = "LOCALITY_NAME";
  protected String SysAddressState = "STATE_CD";
  protected String SysAddressZipCode = "ZIP_CODE";
  protected String SysAddressCountry = "COUNTRY_CD";
  // SYS_EMAIL
  protected String SysEmailID = "EMAIL_ID";
  protected String SysEmail = "EMAIL_ADDRESS";
  // NODE_ACCOUNT_TYPE
  protected String NodeAccountTypeID = "ACCOUNT_TYPE_ID";
  protected String NodeAccountType = "ACCOUNT_TYPE";
  protected String NodeAccountTypeDesc = "ACCOUNT_DESC";
  // NODE_ACCOUNT_TYPE_XREF
  protected String AccountTypeXREFID = "ACCOUNT_TYPE_XREF_ID";
  // NODE_DOMAIN
  protected String NodeDomainID = "DOMAIN_ID";
  protected String NodeDomain = "DOMAIN_NAME";
  // NODE_OPERATION
  protected String OperationID = "OPERATION_ID";
  protected String OperationName = "OPERATION_NAME";
  protected String OperationType = "OPERATION_TYPE";
  protected String OperationStatus = "OPERATION_STATUS_CD";
  protected String OperationMessage = "OPERATION_STATUS_MSG";
  protected String VersionNo = "VERSION_NO";
  // WI 21296
  protected String IsPublish = "PUBLISH_IND";
  // NODE_OPERATION_LOG_STATUS
  protected String OperationLogStatusID = "OPERATION_LOG_STATUS_ID";
  protected String OperationLogStatus = "STATUS_CD";
  protected String OperationLogStatusMsg = "MESSAGE";
  // NODE_OPERATION_LOG_PARAMETER
  protected String OperationLogParamName = "PARAMETER_NAME";
  protected String OperationLogParamValue = "PARAMETER_VALUE";
  // NODE_WEB_SERVICE
  protected String WebServiceID = "WEB_SERVICE_ID";
  protected String WebService = "WEB_SERVICE_NAME";
  protected String WebServiceDesc = "WEB_SERVICE_DESC";

  /**
   * Constructor
   * @param loggerName
   * @return
   * @deprecated
   */
  public NodeDB(String loggerName) {
    this.LoggerName = loggerName;
  }

  /**
   * GetNodeDB
   * @param
   * @return IDBAdapter
   * @deprecated
   */
  public IDBAdapter GetNodeDB ()
  {
    IDBAdapter retDB = null;
    try {
      retDB = JNDIResources.GetNodeDB();
    } catch (Exception e) {
      this.LogException("Could Not Get NodeDB: " + e.toString());
    }
    return retDB;
  }

  /**
   * GetDB
   * @param dbName
   * @return IDBAdapter
   * @deprecated
   */
  public IDBAdapter GetDB (String dbName)
  {
    IDBAdapter retDB = null;
    try {
      retDB = JNDIResources.GetDB(dbName);
    } catch (Exception e) {
      this.LogException("Could Not Get DB: " + e.toString());
    }
    return retDB;
  }

  /**
   * LogException
   * @param message
   * @return 
   * @deprecated
   */
  protected void LogException (String message)
  {
    Logger logger = Logger.getLogger(this.LoggerName);
    logger.error(message);
  }

  /**
   * GetSelectStr
   * @param cols
   * @param tableName
   * @param whereCols
   * @param whereVals
   * @return String
   * @deprecated
   */
  protected String GetSelectStr (String[] cols, String tableName, String[] whereCols, String[] whereVals)
  {
    StringBuffer retBuf = new StringBuffer();
    if (cols == null || cols.length == 0 || tableName == null || tableName.equals(""))
      return null;
    retBuf.append("select ");
    for (int i = 0; i < cols.length; i++) {
      if (i != 0)
        retBuf.append(", ");
      retBuf.append(cols[i]);
    }
    retBuf.append(" from " + tableName);
    if (whereCols != null && whereCols.length > 0 && whereVals != null && whereVals.length > 0) {
      retBuf.append(" where ");
      for (int i = 0; i < whereCols.length && i < whereCols.length; i++) {
        if (i != 0)
          retBuf.append(" and ");
        retBuf.append(whereCols[i] + " = '" + whereVals[i] + "'");
      }
    }
    return retBuf.toString();
  }

  /**
   * GetSelectOrStr
   * @param cols
   * @param tableName
   * @param whereCols
   * @param whereVals
   * @return String
   * @deprecated
   */
  protected String GetSelectOrStr (String[] cols, String tableName, String[] whereCols, String[][] whereVals)
  {
    StringBuffer retBuf = new StringBuffer();
    if (cols == null || cols.length == 0 || tableName == null || tableName.equals(""))
      return null;
    retBuf.append("select ");
    for (int i = 0; i < cols.length; i++) {
      if (i != 0)
        retBuf.append(", ");
      retBuf.append(cols[i]);
    }
    retBuf.append(" from " + tableName);
    if (whereCols != null && whereCols.length > 0 && whereVals != null && whereVals.length > 0) {
      retBuf.append(" where ");
      for (int i = 0; i < whereCols.length && i < whereCols.length; i++) {
        if (i != 0)
          retBuf.append(" and ");
        retBuf.append("(");
        for (int j = 0; j < whereVals[i].length; j++) {
          if (j != 0)
            retBuf.append(" or ");
          retBuf.append(whereCols[i] + " = '" + whereVals[i][j] + "'");
        }
        retBuf.append(")");
      }
    }
    return retBuf.toString();
  }

  /**
   * GetSelectStr
   * @param cols
   * @param tableName
   * @param whereCols
   * @param whereVals
   * @param order
   * @return String
   * @deprecated
   */
  protected String GetSelectStr (String[] cols, String tableName, String[] whereCols, String[] whereVals, String order)
  {
    StringBuffer retBuf = new StringBuffer();
    if (cols == null || cols.length == 0 || tableName == null || tableName.equals(""))
      return null;
    retBuf.append("select ");
    for (int i = 0; i < cols.length; i++) {
      if (i != 0)
        retBuf.append(", ");
      retBuf.append(cols[i]);
    }
    retBuf.append(" from " + tableName);
    if (whereCols != null && whereCols.length > 0 && whereVals != null && whereVals.length > 0) {
      retBuf.append(" where ");
      for (int i = 0; i < whereCols.length && i < whereCols.length; i++) {
        if (i != 0)
          retBuf.append(" and ");
        retBuf.append(whereCols[i] + " = '" + whereVals[i] + "'");
      }
    }
    if (order != null)
      retBuf.append(" " + order);
    return retBuf.toString();
  }

  /**
   * GetSelectStr
   * @param cols
   * @param tableName
   * @param condition
   * @return String
   * @deprecated
   */
  protected String GetSelectStr (String[] cols, String tableName, String condition)
  {
    StringBuffer retBuf = new StringBuffer();
    if (cols == null || cols.length == 0 || tableName == null || tableName.equals(""))
      return null;
    retBuf.append("select ");
    for (int i = 0; i < cols.length; i++) {
      if (i != 0)
        retBuf.append(", ");
      retBuf.append(cols[i]);
    }
    retBuf.append(" from " + tableName + " where " + condition);
    return retBuf.toString();
  }

  /**
   * GetUpdateXDBString
   * @param tableName
   * @param columnName
   * @param xpaths
   * @param values
   * @param where
   * @param conditions
   * @return String
   * @deprecated
   */
  protected String GetUpdateXDBString (String tableName, String columnName, String[] xpaths, String[] values, String[] where, String[] conditions)
  {
    StringBuffer retBuf = new StringBuffer("update " + tableName + " set " + columnName + " = updateXML(" + columnName);
    if (xpaths != null && xpaths.length > 0 && values != null && values.length > 0) {
      for (int i = 0; i < xpaths.length && i < values.length; i++)
        retBuf.append(", '" + xpaths[i] + "', '" + values[i] + "'");
    }
    retBuf.append(")");
    if (where != null && where.length > 0 && conditions != null && conditions.length > 0) {
      retBuf.append(" where");
      for (int i = 0; i < where.length && i < conditions.length; i++) {
        if (i != 0)
          retBuf.append(",");
        retBuf.append(" " + where[i] + " = '" + conditions[i] + "'");
      }
    }
    return retBuf.toString();
  }

  /**
   * GetXDBExtractText
   * @param xdbColName
   * @param xpath
   * @return String
   * @deprecated
   */
  protected String GetXDBExtractText (String xdbColName, String xpath)
  {
    return "extractValue("+xdbColName + ", '" + xpath + "')";
  }

  /**
   * GetXDBExtractStr
   * @param xdbColName
   * @param xpath
   * @return String
   * @deprecated
   */
  protected String GetXDBExtractStr (String xdbColName, String xpath)
  {
    return "extract(" + xdbColName + ", '" + xpath + "')";
  }

  /**
   * GetXDBExistsNodeStr
   * @param xdbColName
   * @param xpath
   * @return String
   * @deprecated
   */
  protected String GetXDBExistsNodeStr (String xdbColName, String xpath)
  {
    return "existsNode(" + xdbColName + ", '" + xpath + "')";
  }

  /**
   * GetXDBSelectStr
   * @param xdbColName
   * @param xpath
   * @param name
   * @param tableName
   * @param whereCols
   * @param whereVals
   * @return String
   * @deprecated
   */
  protected String GetXDBSelectStr (String xdbColName, String[] xpath, String[] name, String tableName, String[] whereCols, String[] whereVals)
  {
    StringBuffer retBuf = new StringBuffer("select");
    if (xpath != null && name != null) {
      for (int i = 0; i < xpath.length && i < name.length; i++) {
        if (i != 0)
          retBuf.append(",");
        retBuf.append(" extractValue(" + xdbColName + ",'" + xpath[i] + "') " + name[i]);
      }
    }
    retBuf.append(" from " + tableName);
    if (whereCols != null && whereCols.length > 0 && whereVals != null && whereVals.length > 0) {
      retBuf.append(" where ");
      for (int i = 0; i < whereCols.length && i < whereCols.length; i++) {
        if (i != 0)
          retBuf.append(" and ");
        retBuf.append(whereCols[i] + " = '" + whereVals[i] + "'");
      }
    }
    return retBuf.toString();
  }

  /**
   * GetXDBSelectStr
   * @param xdbColName
   * @param xpath
   * @param name
   * @param extractMethod
   * @param tableName
   * @param whereCols
   * @param whereVals
   * @return String
   * @deprecated
   */
  protected String GetXDBSelectStr (String xdbColName, String[] xpath, String[] name, String[] extractMethod, String tableName, String[] whereCols, String[] whereVals)
  {
    StringBuffer retBuf = new StringBuffer("select");
    if (xpath != null && name != null && extractMethod != null) {
      for (int i = 0; i < xpath.length && i < name.length && i < extractMethod.length; i++) {
        if (i != 0)
          retBuf.append(",");
        retBuf.append(" extract(" + xdbColName + ",'" + xpath[i] + "')." + extractMethod[i] + " " + name[i]);
      }
    }
    retBuf.append(" from " + tableName);
    if (whereCols != null && whereCols.length > 0 && whereVals != null && whereVals.length > 0) {
      retBuf.append(" where ");
      for (int i = 0; i < whereCols.length && i < whereCols.length; i++) {
        if (i != 0)
          retBuf.append(" and ");
        retBuf.append(whereCols[i] + " = '" + whereVals[i] + "'");
      }
    }
    return retBuf.toString();
  }

  /**
   * ParseLevel
   * @param input
   * @return Level
   * @deprecated
   */
  protected Level ParseLevel (String input)
  {
    if (input.equalsIgnoreCase(Level.FATAL.toString()))
      return Level.FATAL;
    if (input.equalsIgnoreCase(Level.ERROR.toString()))
      return Level.ERROR;
    if (input.equalsIgnoreCase(Level.WARN.toString()))
      return Level.WARN;
    if (input.equalsIgnoreCase(Level.INFO.toString()))
      return Level.INFO;
    else
      return Level.DEBUG;
  }

  /**
   * GetIncrementID
   * @param tableName
   * @param columnName
   * @return int
   * @deprecated
   */
  protected int GetIncrementID (String tableName, String columnName)
  {
    SequenceNo seqDB = new SequenceNo(this.LoggerName);
    return seqDB.GetNextSeqNumber(tableName,columnName);
  }


  /**
   * GetBlobBytes
   * @param blob
   * @return byte[]
   * @deprecated
   */
  public byte[] GetBlobBytes (BLOB blob) throws Exception
  {
    byte[] retBytes = null;
    if (blob != null) {
      retBytes = new byte [(int)blob.length()];
      InputStream is = blob.getBinaryStream();
      is.read(retBytes);
    }
    return retBytes;
  }

  /**
   * CreateBLOB
   * @param db
   * @param content
   * @return BLOB
   * @deprecated
   */
  public BLOB CreateBLOB (IDBAdapter db, byte[] content) throws Exception
  {
		String dbType = (String)JNDIAccess.GetJNDIValue(Phrase.dbType, false);
	    BLOB tmpBlob = null;
	    if(content!=null && content.length>0){
	    	tmpBlob = BLOB.createTemporary(db.GetConnection(), true, BLOB.DURATION_SESSION);
	        OutputStream tmpBlobOS = null;
	        tmpBlob.open(BLOB.MODE_READWRITE);
	        /*//OutputStream tmpBlobOS = tmpBlob.setBinaryStream(0);
	        OutputStream tmpBlobOS = tmpBlob.getBinaryOutputStream();*/
	        if(dbType.equalsIgnoreCase(Phrase.ORACLE9i)){
	            tmpBlobOS = tmpBlob.getBinaryOutputStream();
	        }else if(dbType.equalsIgnoreCase(Phrase.ORACLE10i) || dbType.equalsIgnoreCase(Phrase.ORACLE10g)){
	            tmpBlobOS = tmpBlob.setBinaryStream(0);		    	
	        }else tmpBlobOS = tmpBlob.setBinaryStream(0);	
	        tmpBlobOS.write(content);
	        tmpBlobOS.flush();
	        tmpBlobOS.close();    	
	    }
	    return tmpBlob;
  }

  /**
   * GetTrueClobString
   * @param clob
   * @return String
   * @deprecated
   */
  public String GetTrueClobString (CLOB clob) throws Exception
  {
    String retString = null;
    if (clob != null) {
      BufferedReader br = new BufferedReader(clob.getCharacterStream());
      retString = "";
      String temp = null;
      if (br == null){
        retString = null;
      }
      else{
        StringBuffer sb = new StringBuffer();
        char[] charbuf = new char[4096];
        for (int i = br.read(charbuf); i > 0; i = br.read(charbuf)) {
          sb.append(charbuf, 0, i);
        }
        retString = sb.toString();
      }
    }
    return retString;
  }

  /**
   * GetClobString
   * @param clob
   * @return String
   * @deprecated
   */
  public String GetClobString (CLOB clob) throws Exception
  {
    String retString = null;
    if (clob != null) {
      BufferedReader br = new BufferedReader(clob.getCharacterStream());
      retString = "";
      String temp = null;
      while ((temp = br.readLine()) != null)
        retString += temp;
      if (retString.equals(""))
        retString = null;
    }
    return retString;
  }

  /**
   * CreateCLOB
   * @param db
   * @param content
   * @return CLOB
   * @deprecated
   */
  public CLOB CreateCLOB (IDBAdapter db, String content) throws Exception
  {
		String dbType = (String)JNDIAccess.GetJNDIValue(Phrase.dbType, false);
	    CLOB retClob = null;
	    if(content!=null && content.length()>0){	
	    	retClob = CLOB.createTemporary(db.GetConnection(),true,CLOB.DURATION_SESSION);
		    Writer writer = null;
		    retClob.open(CLOB.MODE_READWRITE);
		    /*//Writer writer = retClob.setCharacterStream(0);
		    Writer writer = retClob.getCharacterOutputStream();*/
		    if(dbType.equalsIgnoreCase(Phrase.ORACLE9i)){
		    	writer = retClob.getCharacterOutputStream();
		    }else if(dbType.equalsIgnoreCase(Phrase.ORACLE10i) || dbType.equalsIgnoreCase(Phrase.ORACLE10g)){
		        writer = retClob.setCharacterStream(0);
		    }else writer = retClob.setCharacterStream(0);
		    writer.write(content);
		    writer.flush();
		    writer.close();
	    }
	    return retClob;
  }

  /**
   * CreateBLOBByDB
   * @param db
   * @param dbName
   * @param content
   * @return BLOB
   * @deprecated
    */
	public BLOB CreateBLOBByDB(IDBAdapter db, String dbName, byte[] content)
			throws Exception {
		// TODO Auto-generated method stub
		return null;
	}

/**
 * CreateCLOBByDB
 * @param db
 * @param dbName
 * @param content
 * @return CLOB
 * @deprecated
 */
	public CLOB CreateCLOBByDB(IDBAdapter db, String dbName, String content)
			throws Exception {
		// TODO Auto-generated method stub
		return null;
	}
}
