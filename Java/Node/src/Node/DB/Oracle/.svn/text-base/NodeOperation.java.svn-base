package Node.DB.Oracle;

import java.io.BufferedReader;
import java.io.InputStream;
import java.sql.Blob;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Hashtable;

import oracle.jdbc.OracleResultSet;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.Schedule;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlAttributeCollection;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
/**
 * <p>This class create NodeOperation.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeOperation extends NodeDB implements INodeOperation {
  // Table Name
  private String TableName = "NODE_OPERATION";

  // Column Names
  private String OpID = "OPERATION_ID";
  private String DomainID = "DOMAIN_ID";
  private String WSID = "WEB_SERVICE_ID";
  private String OpName = "OPERATION_NAME";
  private String Description = "OPERATION_DESC";
  private String OpType = this.OperationType;
  private String OpConfig = "OPERATION_CONFIG";
  private String Status = "OPERATION_STATUS_CD";
  private String Message = "OPERATION_STATUS_MSG";
  private String PublishInd = "PUBLISH_IND";
  // WI 33641
  private String RestInd = "REST_IND";

  /**
   * Constructor
   * @param loggerName
   * @return 
   */
  public NodeOperation(String loggerName) {
    super(loggerName);
  }

  /**
   * GetOperationID
   * @param wsID
   * @param opName
   * @return int
   */
  public int GetOperationID (int wsID, String opName)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    int retInt = -1;
    try {
      //String sql = this.GetSelectStr(new String[]{this.OpID},this.TableName,new String[]{this.WSID,this.OpName},new String[]{wsID+"",opName});
      String sql = "select "+this.OpID+" from "+this.TableName+" where "+this.WSID+" = "+wsID;
      sql += " and "+this.OpName+" = '"+opName+"' and "+this.Status+" != 'Inactive'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.OpID);
    } catch (Exception e) {
      this.LogException("Could not Get OperationID: " + e.toString());
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

  /**
   * GetOperationID
   * @param opName
   * @return int
   */
  public int GetOperationID (String opName)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    int retInt = -1;
    try {
      //String sql = this.GetSelectStr(new String[]{this.OpID},this.TableName,new String[]{this.OpName},new String[]{opName});
      String sql = "select "+this.OpID+" from "+this.TableName+" where "+this.OpName+" = '"+opName+"'";
      sql += " and "+this.Status+" != 'Inactive'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.OpID);
    } catch (Exception e) {
      this.LogException("Could not Get OperationID: " + e.toString());
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

  /**
   * GetOperationID
   * @param wsName
   * @param opName
   * @return int
   */
  public int GetOperationID (String wsName, String opName)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpID };
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.OpName+" = '"+opName+"'";
      condition += " and A."+this.Status+" = 'Running'";
      condition += " and B."+this.WebService+" = '"+wsName+"' and A."+this.WSID+" = B."+this.WSID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.OpID);
    } catch (Exception e) {
      this.LogException("Could not Get Operation ID: " + e.toString());
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

  /**
   * GetOperationIDs
   * @param wsNames
   * @param opNames
   * @return int[]
   */
  public int[] GetOperationIDs (String[] wsNames, String[] opNames)
  {
    if (wsNames == null || wsNames.length <= 0 || opNames == null || wsNames.length != opNames.length)
      return null;
    int[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A, "+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and (";
      for (int i = 0; i < opNames.length; i++) {
        if (i != 0) condition += " or ";
        condition += "(upper(A."+this.OpName+") = '"+opNames[i].toUpperCase()+"'";
        condition += " and upper(B."+this.WebService+") = '"+wsNames[i].toUpperCase()+"')";
      }
      condition += ") and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OpID},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new int [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getInt(this.OpID);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation IDs: " + e.toString());
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
    return retArray;
  }

  /**
   * GetDomainID
   * @param opID
   * @return int
   */
  public int GetDomainID (int opID)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.DomainID},this.TableName,new String[]{this.OpID},new String[]{opID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.DomainID);
    } catch (Exception e) {
      this.LogException("Could not Get DomainID: " + e.toString());
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

  /**
   * GetDomainID
   * @param opName
   * @param wsName
   * @return int
   */
  public int GetDomainID (String opName, String wsName)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.DomainID };
      String tableNames = this.TableName+" A, "+this.WebServiceTableName+" B";
      String condition = "A."+this.OpName+" = '"+opName+"' and B."+this.WebService+" = '"+wsName+"'";
      condition += " and A."+this.WSID+" = B."+this.WSID+" and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.DomainID);
    } catch (Exception e) {
      this.LogException("Could not Get DomainID: " + e.toString());
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

  /**
   * GetOperationStatus
   * @param opID
   * @return String
   */
  public String GetOperationStatus (int opID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.OpID},new String[]{opID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.Status);
    } catch (Exception e) {
      this.LogException("Could not Get Operation Status: " + e.toString());
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
    return retString;
  }

  /**
   * GetOperationStatus
   * @param opName
   * @return String
   */
  public String GetOperationStatus (String opName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      int opID = this.GetOperationID(opName);
      if (opID >= 0)
        retString = this.GetOperationStatus(opID);
    } catch (Exception e) {
      this.LogException("Could not Get Operation Status: " + e.toString());
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
    return retString;
  }

  /**
   * GetOperationName
   * @param opID
   * @return String
   */
  public String GetOperationName (int opID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      //String sql = this.GetSelectStr(new String[]{this.OpName},this.TableName,new String[]{this.OpID},new String[]{opID+""});
      String sql = "select "+this.OpName+" from "+this.TableName+" where "+this.OpID+" = "+opID+" and "+this.Status+" != 'Inactive'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.OpName);
    } catch (Exception e) {
      this.LogException("Could not Get Operation Name: " + e.toString());
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
    return retString;
  }

  /**
   * GetOperationConfig
   * @param opID
   * @return String
   */
  public String GetOperationConfig(int opID)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] {
          "extract(" + this.OpConfig + ",'/').getclobval() xml"
      };
      String sql = this.GetSelectStr(select,this.TableName,new String[]{this.OpID},new String[]{opID+""});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        Clob clob = rs.getClob("xml");
        BufferedReader br = new BufferedReader(clob.getCharacterStream());
        retString = "";
        String temp = null;
        while ((temp = br.readLine()) != null)
          retString += temp;
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation Config: " + e.toString());
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
    return retString;
  }

  /**
   * GetOperationConfig
   * @param wsName
   * @param opName
   * @return String
   */
  public String GetOperationConfig (String wsName, String opName)
  {
    String config = null;
    int opID = this.GetOperationID(wsName, opName);
    if (opID >= 0)
      config = this.GetOperationConfig(opID);
    return config;
  }

  /**
   * GetOperations
   * @param domainID
   * @return String[]
   */
  public String[] GetOperations (int domainID)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      //String sql = this.GetSelectStr(new String[]{this.OpName,this.WSID},this.TableName,new String[]{this.DomainID},new String[]{domainID+""});
      String sql = "select "+this.OpName+","+this.WSID+" from "+this.TableName+" where "+this.DomainID+" = "+domainID;
      sql += " and "+this.Status+" != 'Inactive'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        NodeWebService wsDB = new NodeWebService(this.LoggerName);
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          String wsIDString = rs.getString(this.WSID);
          if (wsIDString != null)
          {
            int wsID = rs.getInt(this.WSID);
            if (wsID >= 0)
              retArray[i] = wsDB.GetWSName(wsID) + ": " + rs.getString(this.OpName);
          }
          else
            retArray[i] = "TASK: " + rs.getString(this.OpName);
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation List: " + e.toString());
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
    return retArray;
  }

  /**
   * GetOperations
   * @param domainName
   * @return String[]
   */
  public String[] GetOperations (String domainName)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpName };
      String tableNames = this.TableName + " A, " + this.NodeDomainTableName + " B";
      String condition = "B."+this.NodeDomain+" = '"+domainName+"' and A."+this.DomainID+" = B."+this.DomainID;
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last()) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation List: " + e.toString());
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
    return retArray;
  }

  /**
   * GetOperationFullNames
   * @param domainName
   * @return String[]
   */
  public String[] GetOperationFullNames (String domainName)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpName, "B."+this.NodeDomain, "C."+this.WebService };
      String tableNames = this.TableName + " A, " + this.NodeDomainTableName + " B, " + this.WebServiceTableName + " C";
      String condition = "B."+this.NodeDomain+" = '"+domainName+"' and A."+this.DomainID+" = B."+this.DomainID;
      condition += " and A."+this.WebServiceID+" = C."+this.WebServiceID+" and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last()) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.NodeDomain) + ": " + rs.getString(this.WebService) + "." + rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation List: " + e.toString());
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
    return retArray;
  }

  /**
   * GetOperationFullNames
   * @param opNames
   * @param wsNames
   * @return String[]
   */
  public String[] GetOperationFullNames (String[] opNames, String[] wsNames)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpName, "B."+this.NodeDomain, "C."+this.WebService };
      String tableNames = this.TableName + " A, " + this.NodeDomainTableName + " B, " + this.WebServiceTableName + " C";
      String condition = "A."+this.DomainID+" = B."+this.DomainID;
      condition += " and A."+this.WebServiceID+" = C."+this.WebServiceID+" and A."+this.Status+" != 'Inactive'";
      if (opNames != null && opNames.length > 0 && wsNames != null && wsNames.length == opNames.length) {
        condition += " and upper(A."+this.OpName+") in (";
        for (int i = 0; i < opNames.length; i++) {
          if (i != 0) condition += ",";
          condition += "'"+opNames[i]+"'";
        }
        condition += ") and upper(C."+this.WebService+") in (";
        for (int i = 0; i < wsNames.length; i++) {
          if (i != 0) condition += ",";
          condition += "'"+wsNames[i]+"'";
        }
        condition += ")";
      }
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last()) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.NodeDomain) + ": " + rs.getString(this.WebService) + "." + rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation List: " + e.toString());
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
    return retArray;
  }

  /**
   * GetWebService
   * @param opName
   * @return String
   */
  public String GetWebService (String opName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tables = this.TableName+" A, " + this.WebServiceTableName + " B";
      String condition = "A."+this.OpName + " = '" + opName + "'";
      condition += " and A."+this.WSID + " = B."+this.WebServiceID+" and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"B."+this.WebService},tables,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.WebService);
    } catch (Exception e) {
      this.LogException("Could not Get Web Service Name: " + e.toString());
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
    return retString;
  }

  /**
   * GetDomainName
   * @param opName
   * @param wsName
   * @return String
   */
  public String GetDomainName (String opName, String wsName)
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "B."+this.NodeDomain };
      String tableNames = this.TableName+" A, "+this.NodeDomainTableName+" B, "+this.WebServiceTableName+" C";
      String condition = "upper(A."+this.OpName+") = '"+opName.toUpperCase()+"'";
      condition += " and upper(C."+this.WebService+") = '"+wsName.toUpperCase()+"'";
      condition += " and A."+this.DomainID+" = B."+this.DomainID+" and A."+this.WSID+" = C."+this.WSID;
      condition += " and A."+this.Status+" != 'Inactive'";;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString(this.NodeDomain);
    } catch (Exception e) {
      this.LogException("Could not Get Domain Name: " + e.toString());
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
    return retString;
  }

  /**
   * GetParameters
   * @param opID
   * @return String[]
   */
  public String[] GetParameters (int opID)
  {
    String[] parameters = null;
    String xml = this.GetOperationConfig(opID);
    ArrayList variableList = new ArrayList();
    boolean isNull = true;
    if (xml != null) {
      try {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        if (doc.DocumentElement().Name().toLowerCase().equals("process"))
        {
        	XmlNodeList list = doc.SelectNodes("//variables/variable");
        	if (list != null && list.Count() > 0) {
        		for (int i = 0; i < list.Count(); i++){
        			String pName = list.ItemOf(i).Attributes().GetNamedItem("name").GetValue().trim();
        			if (!WebServiceParameter.getParameters().contains(pName))
        				variableList.add(pName);
        		}
        		parameters = new String[variableList.size()];
        		for(int i=0;i<variableList.size();i++){
    				parameters[i] = (String)variableList.get(i);        			        			
        		}
        	}
        }
        else
        {
	        XmlNodeList list = doc.SelectNodes("/Operation/Process/ParameterName");
	        if (list != null && list.Count() > 0) {
	          parameters = new String [list.Count()];
	          for (int i = 0; i < list.Count(); i++)
	            parameters[i] = list.ItemOf(i).GetInnerText();
	        }
        }
        for(int i=0;parameters != null && i<parameters.length;i++){
        	if(parameters[i]!=null){
        		isNull = false;
        		break;
        	}
        }
        if(isNull){
        	parameters = null;
        }
      }
       catch (Exception e) {
        this.LogException("Could Not Get Parameter List: " + e.toString());
      }
    }
    return parameters;
  }

  /**
   * GetParameters
   * @param wsName
   * @param opName
   * @return String[]
   */
  public String[] GetParameters (String wsName, String opName)
  {
    String[] parameters = null;
    int opID = this.GetOperationID(wsName, opName);
    if (opID >= 0)
      parameters = this.GetParameters(opID);
    return parameters;
  }

  // WI 21296
  /**
   * GetWebServiceParameters
   * @param opID
   * @return ArrayList
   */
  public ArrayList GetWebServiceParameters (int opID)
  {
	  ArrayList parameters = null;
	  String xml = this.GetOperationConfig(opID);
	  int cnt = 0;
	  if (xml != null) {
		  try {
			  XmlDocument doc = new XmlDocument();
			  doc.LoadXml(xml);
			  if (doc.DocumentElement().Name().toLowerCase().equals("process")){
				  XmlNodeList list = doc.SelectNodes("//variables/variable");
				  if (list != null && list.Count() > 0) {
					  parameters = new ArrayList();
					  for (int i = 0; i < list.Count(); i++){
						  XmlNode node = list.ItemOf(i);
						  if (node != null) {
							  XmlAttributeCollection coll = node.Attributes();
							  String name = coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_NAME).GetValue();
							  if (coll != null && coll.Count() > 0 && !WebServiceParameter.getParameters().contains(name)) {
								  ArrayList temp = new ArrayList();
								  temp.add(String.valueOf(cnt+1));
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_NAME)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_NAME).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_ENCODING)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_ENCODING).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR).GetValue());
								  cnt++;
								  parameters.add(temp);
							  }
							  /*else{
								  temp.add(String.valueOf(i+1));
								  temp.add("");
								  temp.add("");
								  temp.add("");
								  temp.add("");
								  temp.add("");
								  temp.add("");
							  }*/
						  }
					  }
				  }
			  }
			  else
			  {
				  XmlNodeList list = doc.SelectNodes("/Operation/Process/ParameterName");
				  if (list != null && list.Count() > 0) {
					  parameters = new ArrayList();
					  for (int i = 0; i < list.Count(); i++){
						  XmlNode node = list.ItemOf(i);
						  if (node != null) {
							  XmlAttributeCollection coll = node.Attributes();
							  ArrayList temp = new ArrayList();
							  temp.add(String.valueOf(i+1));
							  temp.add(node.GetInnerText());
							  if (coll != null && coll.Count() > 0) {
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_ENCODING)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_ENCODING).GetValue());
								  temp.add(coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR)==null?"":coll.ItemOf(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR).GetValue());
							  }else{
								  temp.add("");
								  temp.add("");
								  temp.add("");
								  temp.add("");
								  temp.add("");
							  }
							  parameters.add(temp);
						  }
					  }
				  }
			  }
		  }
		  catch (Exception e) {
			  this.LogException("Could Not Get Parameter List: " + e.toString());
		  }
	  }
	  return parameters;
  }

  /**
   * GetWebServiceParameters
   * @param wsName
   * @param opName
   * @return ArrayList
   */
  public ArrayList GetWebServiceParameters (String wsName, String opName)
  {
	  ArrayList parameters = null;
    int opID = this.GetOperationID(wsName, opName);
    if (opID >= 0)
      parameters = this.GetWebServiceParameters(opID);
    return parameters;
  }

  /**
   * GetLastStartTime
   * @param opName
   * @return Calendar
   */
  public Calendar GetLastStartTime (String opName)
  {
    Calendar retCal = null;
    try {
      int opID = this.GetOperationID(opName);
      if (opID >= 0) {
        NodeOperationLog logDB = new NodeOperationLog(this.LoggerName);
        retCal = logDB.GetLastStartTime(opID);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation Last Start Time: " + e.toString());
    }
    return retCal;
  }

  /**
   * StartTask
   * @param opName
   * @return 
   */
  public void StartTask (String opName)
  {
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  String status = null;
	  try {
		  String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.OpName},new String[]{opName});
		  db = this.GetNodeDB();
		  rs = db.GetResultSet(sql);
		  if (rs != null && rs.last() && rs.getRow() == 1) {
			  status = rs.getString("OPERATION_STATUS_CD");
			  if(status.equalsIgnoreCase(Phrase.EXECUTING_STATUS)){
				  LoggingUtils.Log("NodeOperation>>>StartTask>>>Task " +opName+" is executing."
						  , Level.DEBUG, Phrase.TaskLoggerName);
			  }else{
				  LoggingUtils.Log("NodeOperation>>>StartTask>>> the OPERATION_STATUS_CD is: "+rs.getString("OPERATION_STATUS_CD")
						  , Level.DEBUG, Phrase.TaskLoggerName);
				  rs.updateString(this.Status, Phrase.EXECUTING_STATUS);
				  rs.updateRow();				  
			  }
		  }
	  } catch (Exception e) {
		  this.LogException("Could not Start Operation: " + e.toString());
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
  }

  /**
   * StopTask
   * @param opName
   * @return 
   */
  public void StopTask (String opName)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.Status},this.TableName,new String[]{this.OpName},new String[]{opName});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        // changed by charlie zhang 2007-9-18 begin
        LoggingUtils.Log("NodeOperation>>>StopTask>>> the OPERATION_STATUS_CD is: "+rs.getString("OPERATION_STATUS_CD")
                         , Level.DEBUG, Phrase.TaskLoggerName);
        if(rs.getString(this.Status).equalsIgnoreCase("Executing")){
          rs.updateString(this.Status, Phrase.RUNNING_STATUS);
          rs.updateRow();
       }
        // changed by charlie zhang 2007-9-18 begin
      }
    } catch (Exception e) {
      this.LogException("Could not Start Operation: " + e.toString());
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
  }

  /**
   * GetDomainID
   * @param opName
   * @return int
   */
  public int GetDomainID (String opName)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      //String sql = this.GetSelectStr(new String[]{this.DomainID},this.TableName,new String[]{this.OpName},new String[]{opName});
      String sql = "select "+this.DomainID+" from "+this.TableName+" where "+this.OpName+" = '"+opName+"' and ";
      sql += this.Status+" != 'Inactive'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retInt = rs.getInt(this.DomainID);
    } catch (Exception e) {
      this.LogException("Could not Get Domain ID: " + e.toString());
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

  /**
   * GetOperation
   * @param opID
   * @return Operation
   */
  public Operation GetOperation (int opID)
  {
    return this.GetOperationByIDOrName(opID,null,null);
  }

  /**
   * GetOperation
   * @param opName
   * @param webServiceName
   * @return Operation
   */
  public Operation GetOperation (String opName, String webServiceName)
  {
    return this.GetOperationByIDOrName(-1,opName,webServiceName);
  }

  /**
   * GetActiveOperation
   * @param opName
   * @param webServiceName
   * @return Operation
   */
  public Operation GetActiveOperation (String opName, String webServiceName)
  {
    if (opName == null)
      return null;
    Operation retOp = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A.*","B."+this.NodeDomain,"C."+this.WebService,
    		  " XMLType.getCLOBVal(A."+this.OpConfig+") CONFIG" };
      String tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.WebServiceTableName+" C";
      String condition = "A."+this.DomainID+" = B."+this.DomainID+" and A."+this.WSID+" = C."+this.WSID;
      condition += " and upper(A."+this.OpName+") = upper('"+opName+"')";
      if (webServiceName != null)
        condition += " and upper(C."+this.WebService+") = upper('"+webServiceName+"')";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int id = rs.getInt(this.OpID);
        retOp = new Operation(id);
        retOp.SetCreatedBy(rs.getString(this.CreatedBy));
        retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
        retOp.SetDescription(rs.getString(this.Description));
        retOp.SetDomain(rs.getString(this.NodeDomain));
        retOp.SetMessage(rs.getString(this.Message));
        retOp.SetOpName(rs.getString(this.OpName));
        retOp.SetStatus(rs.getString(this.Status));
        retOp.SetType(rs.getString(this.OpType));
        retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
        retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
        retOp.SetWebService(rs.getString(this.WebService));
        retOp.setVersion(rs.getString(this.VersionNo));
        Clob clob = rs.getClob("CONFIG");
        String config = clob.getSubString(1, (int)clob.length());
        retOp.SetConfig(config);
        retOp.SetParamNames(this.GetParameters(webServiceName,opName));
        // WI 21296
        retOp.setWebServiceParameters(this.GetWebServiceParameters(webServiceName,opName));
        retOp.setIsPublish(rs.getString(this.PublishInd));
        // WI 33641
        ResultSetMetaData rsMetaData = rs.getMetaData();
        int numberOfColumns = rsMetaData.getColumnCount();
        // get the column names; column indexes start from 1
        for (int i = 1; i < numberOfColumns + 1; i++) {
            String columnName = rsMetaData.getColumnName(i);
            // Get the name of the column's table name
            if (this.RestInd.equalsIgnoreCase(columnName)) {
                retOp.setIsRest(rs.getString(this.RestInd));
                break;
            }
        }
      }
      else if (webServiceName == null) {
        if (rs != null) rs.close();
        select = new String[] { "A.*","B."+this.NodeDomain,
            "extract(D."+this.SysConfigConfigXML+",'/Configuration').getStringVal() CONFIG"
        };
        tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.SysConfigTableName+" D";
        condition = "A."+this.DomainID+" = B."+this.DomainID;
        condition += " and A." + this.OpName + " = '" + opName + "'";
        condition += " and D."+this.SysConfigConfigName+" = 'task.config'";
        condition += " and A."+this.Status+" != 'Inactive'";
        sql = this.GetSelectStr(select,tableNames,condition);
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() == 1) {
          int id = rs.getInt(this.OpID);
          retOp = new Operation(id);
          retOp.SetCreatedBy(rs.getString(this.CreatedBy));
          retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
          retOp.SetDescription(rs.getString(this.Description));
          retOp.SetDomain(rs.getString(this.NodeDomain));
          retOp.SetMessage(rs.getString(this.Message));
          retOp.SetOpName(rs.getString(this.OpName));
          retOp.SetStatus(rs.getString(this.Status));
          retOp.SetType(rs.getString(this.OpType));
          retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
          retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
          retOp.setVersion(rs.getString(this.VersionNo));
          this.SetTaskOperation(retOp,rs.getString("CONFIG"));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation: " + e.toString());
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
    return retOp;
  }

  /**
   * GetOperationByIDOrName
   * @param opID
   * @param opName
   * @param wsName
   * @return Operation
   */
  private Operation GetOperationByIDOrName (int opID, String opName, String wsName)
  {
    if (opID < 0 && opName == null)
      return null;
    Operation retOp = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A.*","B."+this.NodeDomain,"C."+this.WebService,
          " XMLType.getCLOBVal(A."+this.OpConfig+") CONFIG" };
      String tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.WebServiceTableName+" C";
      String condition = "A."+this.DomainID+" = B."+this.DomainID+" and A."+this.WSID+" = C."+this.WSID;
      if (opID >= 0)
        condition += " and A." + this.OpID + " = " + opID;
      else if (opName != null) {
        condition += " and upper(A."+this.OpName+") = upper('"+opName+"')";
        if (wsName != null)
          condition += " and upper(C."+this.WebService+") = upper('"+wsName+"')";
      }
      //condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int id = rs.getInt(this.OpID);
        retOp = new Operation(id);
        retOp.SetCreatedBy(rs.getString(this.CreatedBy));
        retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
        retOp.SetDescription(rs.getString(this.Description));
        retOp.SetDomain(rs.getString(this.NodeDomain));
        retOp.SetMessage(rs.getString(this.Message));
        retOp.SetOpName(rs.getString(this.OpName));
        retOp.SetStatus(rs.getString(this.Status));
        retOp.SetType(rs.getString(this.OpType));
        retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
        retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
        retOp.SetWebService(rs.getString(this.WebService));
        retOp.setVersion(rs.getString(this.VersionNo));
        Clob clob = rs.getClob("CONFIG");
        String config = clob.getSubString(1, (int)clob.length());
        retOp.SetConfig(config);
        retOp.SetParamNames(this.GetParameters(opID));
        // WI 21296
        retOp.setWebServiceParameters(this.GetWebServiceParameters(opID));
        retOp.setIsPublish(rs.getString(this.PublishInd));
        // WI 33641
        ResultSetMetaData rsMetaData = rs.getMetaData();
        int numberOfColumns = rsMetaData.getColumnCount();
        // get the column names; column indexes start from 1
        for (int i = 1; i < numberOfColumns + 1; i++) {
            String columnName = rsMetaData.getColumnName(i);
            // Get the name of the column's table name
            if (this.RestInd.equalsIgnoreCase(columnName)) {
                retOp.setIsRest(rs.getString(this.RestInd));
                break;
            }
        }
      }
      else if (wsName == null || wsName.equalsIgnoreCase("")) {
        if (rs != null) rs.close();
        select = new String[] { "A.*","B."+this.NodeDomain,"extract(A."+this.OpConfig+",'/process').getClobVal() OPERATION_CONFIGSTR",
            "extract(D."+this.SysConfigConfigXML+",'/Configuration').getClobVal() CONFIG"
        };
        tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.SysConfigTableName+" D";
        condition = "A."+this.DomainID+" = B."+this.DomainID;
        if (opID >= 0)
          condition += " and A." + this.OpID + " = " + opID;
        else if (opName != null)
          condition += " and A." + this.OpName + " = '" + opName + "'";
        condition += " and D."+this.SysConfigConfigName+" = 'task.config'";
        sql = this.GetSelectStr(select,tableNames,condition);
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() == 1) {
          int id = rs.getInt(this.OpID);
          retOp = new Operation(id);
          retOp.SetCreatedBy(rs.getString(this.CreatedBy));
          retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
          retOp.SetDescription(rs.getString(this.Description));
          retOp.SetDomain(rs.getString(this.NodeDomain));
          retOp.SetMessage(rs.getString(this.Message));
          retOp.SetOpName(rs.getString(this.OpName));
          retOp.SetStatus(rs.getString(this.Status));
          retOp.SetType(rs.getString(this.OpType));
          retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
          retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
          retOp.setVersion(rs.getString(this.VersionNo));
          this.SetTaskOperation(retOp,this.GetClobString(((OracleResultSet)rs).getCLOB("CONFIG")));
          retOp.SetConfig(this.GetClobString(((OracleResultSet)rs).getCLOB("OPERATION_CONFIGSTR")));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation: " + e.toString());
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
    return retOp;
  }

  /**
   * GetOperationByIDOrNameNoAccurate
   * @param opID
   * @param opName
   * @param wsName
   * @return Operation
   */
  public Operation GetOperationByIDOrNameNoAccurate (int opID, String opName, String wsName)
  {
    if (opID < 0 && opName == null)
      return null;
    Operation retOp = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A.*","B."+this.NodeDomain,"C."+this.WebService,
    		  " XMLType.getCLOBVal(A."+this.OpConfig+") CONFIG" };
      String tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.WebServiceTableName+" C";
      String condition = "A."+this.DomainID+" = B."+this.DomainID+" and A."+this.WSID+" = C."+this.WSID;
      if (opID >= 0)
        condition += " and A." + this.OpID + " like " + opID;
      else if (opName != null) {
        condition += " and upper(A."+this.OpName+") like upper('"+opName+"')";
        if (wsName != null)
          condition += " and upper(C."+this.WebService+") like upper('"+wsName+"')";
      }
      //condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      LoggingUtils.Log("NodeOperation>>>GetOperationByIDOrNameNoAccurate>>> sql: "+sql
                      , Level.DEBUG, Phrase.AdministrationLoggerName);
     db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        int id = rs.getInt(this.OpID);
        retOp = new Operation(id);
        retOp.SetCreatedBy(rs.getString(this.CreatedBy));
        retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
        retOp.SetDescription(rs.getString(this.Description));
        retOp.SetDomain(rs.getString(this.NodeDomain));
        retOp.SetMessage(rs.getString(this.Message));
        retOp.SetOpName(rs.getString(this.OpName));
        retOp.SetStatus(rs.getString(this.Status));
        retOp.SetType(rs.getString(this.OpType));
        retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
        retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
        retOp.SetWebService(rs.getString(this.WebService));
        retOp.setVersion(rs.getString(this.VersionNo));
        Clob clob = rs.getClob("CONFIG");
        String config = clob.getSubString(1, (int)clob.length());
        retOp.SetConfig(config);
        retOp.SetParamNames(this.GetParameters(opID));
        // WI 21296
        retOp.setWebServiceParameters(this.GetWebServiceParameters(opID));
        retOp.setIsPublish(rs.getString(this.PublishInd));
        // WI 33641
        ResultSetMetaData rsMetaData = rs.getMetaData();
        int numberOfColumns = rsMetaData.getColumnCount();
        // get the column names; column indexes start from 1
        for (int i = 1; i < numberOfColumns + 1; i++) {
            String columnName = rsMetaData.getColumnName(i);
            // Get the name of the column's table name
            if (this.RestInd.equalsIgnoreCase(columnName)) {
                retOp.setIsRest(rs.getString(this.RestInd));
                break;
            }
        }
      }
      else if (wsName == null) {
        if (rs != null) rs.close();
        select = new String[] { "A.*","B."+this.NodeDomain,
            "extract(D."+this.SysConfigConfigXML+",'/Configuration').getClobVal() CONFIG"
        };
        tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B,"+this.SysConfigTableName+" D";
        condition = "A."+this.DomainID+" = B."+this.DomainID;
        if (opID >= 0)
          condition += " and A." + this.OpID + " like " + opID;
        else if (opName != null)
          condition += " and upper(A."+this.OpName+") like upper('"+opName+"')";
        condition += " and D."+this.SysConfigConfigName+" = 'task.config'";
        sql = this.GetSelectStr(select,tableNames,condition);
        LoggingUtils.Log("NodeOperation>>>GetOperationByIDOrNameNoAccurate>>> sql: "+sql
                        , Level.DEBUG, Phrase.AdministrationLoggerName);
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() == 1) {
          int id = rs.getInt(this.OpID);
          retOp = new Operation(id);
          retOp.SetCreatedBy(rs.getString(this.CreatedBy));
          retOp.SetCreatedDate(rs.getDate(this.CreatedDate));
          retOp.SetDescription(rs.getString(this.Description));
          retOp.SetDomain(rs.getString(this.NodeDomain));
          retOp.SetMessage(rs.getString(this.Message));
          retOp.SetOpName(rs.getString(this.OpName));
          retOp.SetStatus(rs.getString(this.Status));
          retOp.SetType(rs.getString(this.OpType));
          retOp.SetUpdatedBy(rs.getString(this.UpdatedBy));
          retOp.SetUpdatedDate(rs.getDate(this.UpdatedDate));
          retOp.setVersion(rs.getString(this.VersionNo));
          this.SetTaskOperation(retOp,this.GetClobString(((OracleResultSet)rs).getCLOB("CONFIG")));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get Operation: " + e.toString());
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
    return retOp;
  }

  /**
   * SetTaskOperation
   * @param op
   * @param config
   * @return SetTaskOperation
   */
  private void SetTaskOperation (Operation op, String config) throws Exception
  {
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(config);
    String classBlockName = null;
    String serviceName = null;
    String scheduleName = null;
    XmlNodeList list = doc.SelectNodes("/Configuration/scheduledTasks/task");
    if (list != null && list.Count() > 0) {
      for (int i = 0; i < list.Count(); i++) {
        XmlAttributeCollection coll = list.ItemOf(i).Attributes();
        if (coll != null && coll.Count() > 0) {
          if (coll.GetNamedItem("name") != null) {
            if (coll.GetNamedItem("name").GetValue().equals(op.GetOpName())) {
              serviceName = coll.GetNamedItem("service").GetValue();
              scheduleName = coll.GetNamedItem("schedule").GetValue();
              break;
            }
          }
        }
      }
    }
    list = doc.SelectNodes("/Configuration/ServicesSetting/service");
    if (list != null && list.Count() > 0) {
      for (int i = 0; i < list.Count(); i++) {
        XmlAttributeCollection coll = list.ItemOf(i).Attributes();
        if (coll != null && coll.Count() > 0) {
          if (coll.GetNamedItem("name") != null) {
            if (coll.GetNamedItem("name").GetValue().equals(serviceName)) {
              classBlockName = coll.GetNamedItem("service").GetValue();
              op.SetMethodName(coll.GetNamedItem("method").GetValue());
              op.SetTaskParameters(this.GetTaskParameters(list.ItemOf(i)));
              break;
            }
          }
        }
      }
    }
    list = doc.SelectNodes("/Configuration/scheduledTasks/schedule");
    if (list != null && list.Count() > 0) {
      for (int i = 0; i < list.Count(); i++) {
        XmlAttributeCollection coll = list.ItemOf(i).Attributes();
        if (coll != null && coll.Count() > 0) {
          if (coll.GetNamedItem("uniqueName") != null) {
            if (coll.GetNamedItem("uniqueName").GetValue().equals(scheduleName)) {
              op.SetTaskSchedule(this.GetTaskSchedule(list.ItemOf(i)));
              break;
            }
          }
        }
      }
    }
    list = doc.SelectNodes("/Configuration/classSetting/javaClass");
    if (list != null && list.Count() > 0) {
      for (int i = 0; i < list.Count(); i++) {
        XmlAttributeCollection coll = list.ItemOf(i).Attributes();
        if (coll != null && coll.Count() > 0) {
          if (coll.GetNamedItem("uniqueName") != null) {
            if (coll.GetNamedItem("uniqueName").GetValue().equals(classBlockName)) {
              op.SetClassName(coll.GetNamedItem("class").GetValue());
              break;
            }
          }
        }
      }
    }
  }

  /**
   * GetTaskParameters
   * @param service
   * @return ArrayList
   */
  private ArrayList GetTaskParameters (XmlNode service) throws Exception
  {
    ArrayList retList = null;
    XmlNodeList list = service.SelectNodes("serviceParm");
    if (list != null && list.Count() > 0) {
      retList = new ArrayList();
      for (int i = 0; i < list.Count(); i++) {
        XmlNode node = list.ItemOf(i);
        if (node != null) {
          XmlAttributeCollection coll = node.Attributes();
          if (coll != null && coll.Count() > 0) {
            ArrayList temp = new ArrayList();
            temp.add(coll.ItemOf("key").GetValue());
            temp.add(coll.ItemOf("name").GetValue());
            temp.add(coll.ItemOf("value").GetValue());
            retList.add(temp);
          }
        }
      }
    }
    return retList;
  }

  /**
   * GetTaskSchedule
   * @param element
   * @return Schedule
   */
  private Schedule GetTaskSchedule (XmlNode element) throws Exception
  {
    Schedule schedule = null;
    if (element != null) {
      XmlAttributeCollection attrs = element.Attributes();
      if (attrs != null && attrs.Count() > 0) {
        String type = attrs.GetNamedItem("type").GetValue();
        schedule = new Schedule(type);
        if (!schedule.GetType().equalsIgnoreCase(schedule.TYPE_INACTIVE)) {
          SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
          schedule.SetStartDate((Date)dateFormat.parse(attrs.GetNamedItem("startDateTime").GetValue()));
          if (!type.equalsIgnoreCase(schedule.TYPE_ONCE))
            schedule.SetEndDate((Date)dateFormat.parse(attrs.GetNamedItem("endDateTime").GetValue()));
          if (type.equalsIgnoreCase(schedule.TYPE_SECONDS) || type.equalsIgnoreCase(schedule.TYPE_DAYS))
            schedule.SetInterval(attrs.GetNamedItem("interval").GetValue());
          else if (type.equalsIgnoreCase(schedule.TYPE_WEEKS)) {
            String days = attrs.GetNamedItem("dayOfWeek").GetValue();
            if (days != null && !days.equals(""))
              schedule.SetDayOfWeek(days.split(","));
          }
          else if (!type.equalsIgnoreCase(schedule.TYPE_ONCE)) {
            String days = attrs.GetNamedItem("dayOfMonth").GetValue();
            if (days != null && !days.equals(""))
              schedule.SetDayOfMonth(days.split(","));
            if (type.equalsIgnoreCase(schedule.TYPE_YEARS)) {
              String months = attrs.GetNamedItem("monthOfYear").GetValue();
              if (months != null && !months.equals(""))
                schedule.SetMonthOfYear(months.split(","));
            }
          }
        }
      }
    }
    return schedule;
  }

  /**
   * Search
   * @param domain
   * @param name
   * @param type
   * @param method
   * @param status
   * @param version
   * @return Operation[]
   */
  public Operation[] Search (String domain, String name, String type, String method, String status,String version)
  {
    if (domain == null || domain.equals(""))
      return null;
    Operation[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      //Operation[] wsOPs = null;
      db = this.GetNodeDB();
      // if (type == null || type.equals("") || type.equals(Phrase.WEB_SERVICE_OPERATION)) {
      String sql = "select A."+this.OpID+",A."+this.OpName+",A."+this.OpType+",A."+this.Status+",A."+this.Message;
      sql += ",C."+this.WebService;
      sql += " from "+this.TableName+" A left join "+this.WebServiceTableName+" C on A."+this.WSID+" = C."+this.WSID;
      sql += ","+this.NodeDomainTableName+" B";
      sql += " where B."+this.NodeDomain+" = '"+domain+"'";
      sql += " and B."+this.DomainID+" = A."+this.DomainID;
      if (name != null && !name.equals(""))
        sql += " and A."+this.OpName+" like '%"+name.replaceAll(" ","%")+"%'";
      if (type != null && !type.equals(""))
        sql += " and A." + this.OpType + " = '" + type + "'";
      if (method != null && !method.equals(""))
        sql += " and C."+this.WebService+" = '"+method+"'";
      if (status != null && !status.equals("")) {
        if (status.equalsIgnoreCase("Active"))
          sql += " and A."+this.Status+" != 'Inactive'";
        else
          sql += " and A."+this.Status+" = '"+status+"'";
      }if(version != null && !version.equals("")){
          sql += " and A."+this.VersionNo+" = '"+version+"'";    	  
      }
      sql += " order by substr(A."+this.Status+",2) desc, A."+this.OpName;
      LoggingUtils.Log("NodeOperation>>>Search>>>Sql is: " + sql, Level.DEBUG
                       , Phrase.AdministrationLoggerName);
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Operation [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          int id = rs.getInt(this.OpID);
          retArray[i] = new Operation(id);
          retArray[i].SetOpName(rs.getString(this.OpName));
          retArray[i].SetType(rs.getString(this.OpType));
          retArray[i].SetMessage(rs.getString(this.Message));
          retArray[i].SetStatus(rs.getString(this.Status));
          retArray[i].SetWebService(rs.getString(this.WebService));
        }
      }
     // }
    //  if (type == null || type.equals("") || type.equals(Phrase.SCHEDULED_TASK_OPERATION)) {
     /*   String[] select = new String[] { "A."+this.OpID,"A."+this.OpName,"A."+this.OpType,"A."+this.Status,"A."+this.Message };
        String tableNames = this.TableName+" A,"+this.NodeDomainTableName+" B";
        String condition = "B."+this.NodeDomain+" = '"+domain+"'";
        condition += " and B."+this.DomainID+" = A."+this.DomainID;
        if (name != null && !name.equals(""))
          condition += " and A."+this.OpName+" like '%"+name.replaceAll(" ","%")+"%'";
        condition += " and A." + this.OpType + " = '" + Phrase.SCHEDULED_TASK_OPERATION + "'";
        if (status != null && !status.equals(""))
          condition += " and A."+this.Status+" = '"+status+"'";
        String sql = this.GetSelectStr(select,tableNames,condition);
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() > 0) {
          if (wsOPs != null)
            retArray = new Operation [wsOPs.length+rs.getRow()];
          else
            retArray = new Operation [rs.getRow()];
          int count = 0;
          if (wsOPs != null) {
            for (int i = 0; i < wsOPs.length; i++)
              retArray[i] = wsOPs[i];
            count = wsOPs.length;
          }
          rs.beforeFirst();
          for (int i = count; rs.next() && i < retArray.length; i++) {
            int id = rs.getInt(this.OpID);
            retArray[i] = new Operation(id);
            retArray[i].SetOpName(rs.getString(this.OpName));
            retArray[i].SetType(rs.getString(this.OpType));
            retArray[i].SetMessage(rs.getString(this.Message));
            retArray[i].SetStatus(rs.getString(this.Status));
          }
        }
        else
          retArray = wsOPs;*/
   //   }
    //  else
     //   retArray = wsOPs;
    } catch (Exception e) {
      this.LogException("Could not Search Operations: " + e.toString());
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
    return retArray;
  }

  /**
   * SaveOperation
   * @param opID
   * @param description
   * @param config
   * @param status
   * @param message
   * @return boolean
   */
  public boolean SaveOperation (int opID, String description, String config, String status, String message)
  {
	  boolean retBool = false;
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  Clob clob = null;
	  Connection con = null;
	  PreparedStatement st = null;
	  String realStatus = null;
	  String version =  null;
	  // WI 21296
	  String isPublish = null;
	  // WI 33641
	  String isRest = null;
	  String[] params = null;
	  
	  try {
		  // WI 21296
		  if(status.indexOf(";")!= -1){
			  params = status.split(";");
			  realStatus = params[0];
			  version = params[1];
			  isPublish = params[2];
			// WI 33641
			  if(params.length > 3){
				  isRest = params[3];				  
			  }
		  }else{
			  realStatus = status;
		  }
		  db = this.GetNodeDB();
		  db.KeepConnectionAfterExecute(true);
		  db.BeginTransaction();
		  con = db.GetConnection();

		  String sql = "update "+this.TableName+" set ";
		  if (description != null)
			  sql += this.Description+" = '"+description+"'";
		  else
			  sql += this.Description+" = null";
		  if (config != null){
	          clob = this.CreateCLOB(db,config);
			  sql += ","+this.OpConfig+" = XMLType.createXML(?)";
		  }
		  /*else{
			  sql += ","+this.OpConfig+" = null";
			  config = "";
		  }*/
		  if (status != null)
			  sql += ","+this.Status+" = '"+realStatus+"'";
		  else
			  sql += ","+this.Status+" = null";
		  if (message != null)
			  sql += ","+this.Message+" = '"+message+"'";
		  else
			  sql += ","+this.Message+" = null";
			  sql += ","+this.UpdatedDate+" = sysdate,"+this.UpdatedBy+" = 'system'";
		  if (config != null){
			  sql += ",OPERATION_CONFIG_CLOB = ?";
		  }
		  if(version != null){
			  if(version.equals("") || version.equalsIgnoreCase(Phrase.ver_1) ){
				  sql += ","+this.VersionNo+" = '"+ Phrase.ver_1 +"'";				  
			  }else if(version.equals("") || version.equalsIgnoreCase(Phrase.ver_2) ){
				  sql += ","+this.VersionNo+" = '"+ Phrase.ver_2 +"'";				  
			  }
		  }
		  // WI 21296		  
		  if(isPublish != null && !isPublish.equalsIgnoreCase("null") && !isPublish.equalsIgnoreCase("")){
			  sql += ","+this.IsPublish+" = '"+ isPublish +"'";				  			  
		  }
		  // WI 33641		  
		  if(isRest != null && !isRest.equalsIgnoreCase("null") && !isRest.equalsIgnoreCase("")){
			  sql += ","+this.IsRest+" = '"+ isRest +"'";				  			  
		  }
		  sql += " where "+this.OpID+" = "+opID;
		  st = con.prepareStatement(sql);
		  if (config != null){
			  st.setObject(1, clob);
			  st.setObject(2, clob);
		  }
		  st.execute();
		  db.CommitTransaction();
		  LoggingUtils.Log("NodeOperation>>>SaveOperation>>> sql is: "+sql
				  , Level.DEBUG, Phrase.TaskLoggerName);
		  retBool = true;
	  } catch (Exception e) {
		  try {
			  db.RollBackTransaction();
		  } catch (SQLException e1) {
			  this.LogException("Could not Save Operation: " + e.toString());
		  }
		  this.LogException("Could not Save Operation: " + e.toString());
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
	  return retBool;
  }

  /**
   * SaveOperation
   * @param opName
   * @param domain
   * @param webService
   * @param description
   * @param type
   * @param config
   * @param status
   * @param message
   * @return int
   */
  public int SaveOperation (String opName, String domain, String webService, String description, String type, String config,
                                String status, String message)
  {
    int retInt = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
	Clob clob = null;
	Connection con = null;
	PreparedStatement st = null;
	String realStatus = null;
	String version =  null;
    // WI 21296
    String isPublish = null;
    // WI 33641
    String isRest = null;
    String[] params = null;
	  
	try {
	  // WI 21296
	  if(status.indexOf(";")!= -1){
		  params = status.split(";");
		  realStatus = params[0];
		  version = params[1];
		  isPublish = params[2];
		  // WI 33641
		  if(params.length > 3){
			  isRest = params[3];			  
		  }
	  }else{
		  realStatus = status;
	  }
      NodeDomain domainDB = new NodeDomain(this.LoggerName);
      int domainID = domainDB.GetDomainID(domain);
      int wsID = -1;
      if (webService != null) {
        NodeWebService wsDB = new NodeWebService(this.LoggerName);
        wsID = wsDB.GetWebServiceID(webService);
      }
      if (opName != null && type != null && domainID >= 0 && (webService == null || wsID >= 0)) {
        db = this.GetNodeDB();
  	    db.KeepConnectionAfterExecute(true);
  	    db.BeginTransaction();
        con = db.GetConnection();
        String sql = "insert into "+this.TableName+" ("+this.OpID+","+this.OpName+","+this.DomainID+","+this.WSID+",";
        sql += this.Description+","+this.OpType+","+this.OpConfig+","+this.Status+","+this.Message+","+this.CreatedDate;
        // WI 21296
        if (version != null)
            sql += ","+this.CreatedBy+","+this.UpdatedDate+","+this.UpdatedBy+","+this.VersionNo;
        else
            sql += ","+this.CreatedBy+","+this.UpdatedDate+","+this.UpdatedBy;
        
        // WI 33641
        if (isPublish != null)
            sql += ","+this.IsPublish;
        
        if (!Utility.isNullOrEmpty(isRest))
            sql += ","+this.IsRest;
        
        sql += " ) values ( ";

        int id = this.GetIncrementID(this.TableName, this.OpID);
        sql += id + ",'"+opName+"',"+domainID+",";
        if (webService != null)
          sql += wsID+",";
        else
          sql += "null,";
        if (description != null)
          sql += "'"+description+"'";
        else
          sql += "null";
        sql += ",'"+type+"'";
        if (config != null){
            clob = this.CreateCLOB(db,config);
            sql += ",XMLType.createXML(?)";       	
        }
        else{
          sql += ",null";
		}
        if (realStatus != null)
          sql += ",'"+realStatus+"'";
        else
          sql += ",null";
        if (message != null)
          sql += ",'"+message+"'";
        else
          sql += ",null";
        // WI 21296
        if (version != null)
            sql+= ",sysdate,'system',sysdate,'system','"+version+"'";
        else
            sql+= ",sysdate,'system',sysdate,'system'";
        // WI 33641
		if(isPublish != null && !isPublish.equalsIgnoreCase("null") && !isPublish.equalsIgnoreCase("")){
		    sql += ",'"+ isPublish +"'";				  			  
	    }else{
	    	sql += ",'N'";
	    }
		if(isRest != null && !isRest.equalsIgnoreCase("null") && !isRest.equalsIgnoreCase("")){
		    sql += ",'"+ isRest +"'";				  			  
	    }
		sql += " ) ";	
        st = con.prepareStatement(sql);
        if (config != null){
        	st.setObject(1, clob);
        }
        st.execute();
        if (config != null){
	        sql = "update NODE_OPERATION set OPERATION_CONFIG_CLOB = ?";
			st = con.prepareStatement(sql);
			st.setObject(1, clob);
			st.execute();
        }
	    db.CommitTransaction();
        
        retInt = id;
      }
    } catch (Exception e) {
		try {
			db.RollBackTransaction();
		} catch (SQLException e1) {
			this.LogException("Could not Save New Operation: " + e.toString());
		}
		this.LogException("Could not Save New Operation: " + e.toString());
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
    return retInt;
  }

  /**
   * IsUniqueName
   * @param opName
   * @param wsName
   * @return boolean
   */
  public boolean IsUniqueName (String opName, String wsName)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpID };
      String tableNames = this.TableName+" A";
      String condition = "A."+this.OpName+" = '"+opName+"'";
      if (wsName != null) {
        tableNames += ","+this.WebServiceTableName+" B";
        //condition += " and B."+this.WebService+" = '"+wsName+"' and A."+this.WSID+" = B."+this.WSID;
      }
      //condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && !rs.first())
        retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Check Unique Name: " + e.toString());
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
    return retBool;
  }

  /**
   * CanMarkActive
   * @param opID
   * @param opName
   * @param wsName
   * @return boolean
   */
  public boolean CanMarkActive (int opID, String opName, String wsName)
  {
    boolean retBool = true;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpID };
      String tableNames = this.TableName+" A";
      String condition = "A."+this.OpID+" != "+opID+" and A."+this.OpName+" = '"+opName+"'";
      if (wsName != null) {
        tableNames += ","+this.WebServiceTableName+" B";
        condition += " and B."+this.WebService+" = '"+wsName+"' and A."+this.WSID+" = B."+this.WSID;
      }
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0)
        retBool = false;
    } catch (Exception e) {
      this.LogException("Could not Check if Can Mark Active: " + e.toString());
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
    return retBool;
  }

  /**
   * GetSubmits
   * @param 
   * @return String[]
   */
  public String[] GetSubmits ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_SUBMIT+"'";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Submits: " + e.toString());
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
    return retArray;
  }

  // WI 21296
  /**
   * GetSubmits
   * @param version
   * @return String[]
   */
  public String[] GetSubmits (String version)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_SUBMIT+"'";
      condition += " and A."+this.Status+" != 'Inactive'"
      + " and A."+this.IsPublish+"='Y'"
      + " and A."+this.VersionNo+"='"+version+"'"
      ;
     String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Submits: " + e.toString());
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
    return retArray;
  }

  /**
   * GetQueries
   * @param 
   * @return String[]
   */
  public String[] GetQueries ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_QUERY+"'";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Queries: " + e.toString());
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
    return retArray;
  }

  // WI 21296
  /**
   * GetQueries
   * @param version
   * @return String[]
   */
  public String[] GetQueries (String version)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_QUERY+"'";
      condition += " and A."+this.Status+" != 'Inactive'"
      + " and A."+this.IsPublish+"='Y'"
      + " and A."+this.VersionNo+"='"+version+"'"
      ;
     String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Queries: " + e.toString());
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
    return retArray;
  }

  /**
   * GetSolicits
   * @param 
   * @return String[]
   */
  public String[] GetSolicits ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_SOLICIT+"'";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Solicits: " + e.toString());
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
    return retArray;
  }
  
  // WI 21296
  /**
   * GetSolicits
   * @param version
   * @return String[]
   */
  public String[] GetSolicits (String version)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_SOLICIT+"'";
      condition += " and A."+this.Status+" != 'Inactive'"
      + " and A."+this.IsPublish+"='Y'"
      + " and A."+this.VersionNo+"='"+version+"'"
      ;
     String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Solicits: " + e.toString());
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
    return retArray;
  }

  /**
   * GetExecutes
   * @param 
   * @return String[]
   */
  public String[] GetExecutes()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_EXECUTE+"'";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Executes: " + e.toString());
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
    return retArray;
  }
  
  /**
   * GetExecutes
   * @param version
   * @return String[]
   */
  public String[] GetExecutes(String version)
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and B."+this.WebService+" = '"+Phrase.WEB_METHOD_EXECUTE+"'";
      condition += " and A."+this.Status+" != 'Inactive'"
      + " and A."+this.IsPublish+"='Y'"
      + " and A."+this.VersionNo+"='"+version+"'"
      ;
      String sql = this.GetSelectStr(new String[]{"A."+this.OpName},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new String [rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getString(this.OpName);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Executes: " + e.toString());
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
    return retArray;
  }

  /**
   * GetAllQSEServicesID
   * @param 
   * @return int[]
   */
  public int[] GetAllQSEServicesID()
  {
    int[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and (B."+this.WebService+" = '"+Phrase.WEB_METHOD_QUERY+"'"
      +" or B."+this.WebService+" = '"+Phrase.WEB_METHOD_SOLICIT+"'"
      +" or B."+this.WebService+" = '"+Phrase.WEB_METHOD_EXECUTE+"')";
      condition += " and A."+this.Status+" != 'Inactive'";
      String sql = this.GetSelectStr(new String[]{"A."+this.OperationID},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new int[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getInt(this.OperationID);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Queries: " + e.toString());
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
    return retArray;
  }

  // WI 21296
  /**
   * GetAllQSEServicesID
   * @param version
   * @return int[]
   */
  public int[] GetAllQSEServicesID(String version)
  {
    int[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String tableNames = this.TableName+" A,"+this.WebServiceTableName+" B";
      String condition = "A."+this.WSID+" = B."+this.WSID+" and (B."+this.WebService+" = '"+Phrase.WEB_METHOD_QUERY+"'"
      +" or B."+this.WebService+" = '"+Phrase.WEB_METHOD_SOLICIT+"'"
      +" or B."+this.WebService+" = '"+Phrase.WEB_METHOD_EXECUTE+"')";
      condition += " and A."+this.Status+" != 'Inactive'"
      + " and A."+this.IsPublish+"='Y'"
      + " and A."+this.VersionNo+"='"+version+"'"
      ;
      String sql = this.GetSelectStr(new String[]{"A."+this.OperationID},tableNames,condition);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new int[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++)
          retArray[i] = rs.getInt(this.OperationID);
      }
    } catch (Exception e) {
      this.LogException("Could not Get Queries: " + e.toString());
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
    return retArray;
  }

  /**
   * DeleteOperation
   * @param opID
   * @return boolean
   */
  public boolean DeleteOperation (int opID)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      if (opID >= 0) {
        String sql = "select A.* from "+this.OperationLogTableName+" A where A."+this.OpID+" = "+opID;
        db = this.GetNodeDB();
        LoggingUtils.Log("NodeOperation>>>DeleteOperation>>> sql is: "+sql
                         , Level.DEBUG, Phrase.TaskLoggerName);
        rs = db.GetResultSet(sql);
        if (rs != null && !rs.first()) {
          rs.close();
          sql = "delete from "+this.UserOpXREFTableName+" where "+this.OpID+" = "+opID;
          LoggingUtils.Log("NodeOperation>>>DeleteOperation>>> sql is: "+sql
                           , Level.DEBUG, Phrase.TaskLoggerName);
          rs = db.GetResultSet(sql);
          if (rs != null)
            rs.close();
          sql = "delete from "+this.TableName+" where "+this.OpID+" = "+opID;
          LoggingUtils.Log("NodeOperation>>>DeleteOperation>>> sql is: "+sql
                           , Level.DEBUG, Phrase.TaskLoggerName);
          rs = db.GetResultSet(sql);
          retBool = true;
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Delete Operation: " + e.toString());
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
    return retBool;
  }

  /**
   * GetOperationsList
   * @param domains
   * @return Operation[]
   */
  public Operation[] GetOperationsList (String[] domains)
  {
    Operation[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpID,"A."+this.OpType,"A."+this.OpName,"B."+this.WebService,"C."+this.NodeDomain };
      String tableNames = this.TableName+" A left join "+this.WebServiceTableName+" B on A."+this.WSID+" = B."+this.WSID;
      tableNames += ", "+this.NodeDomainTableName+" C";
      String condition = "A."+this.DomainID+" = C."+this.DomainID+" and A."+this.OpType+" = '"+Phrase.WEB_SERVICE_OPERATION+"'";
      if (domains != null && domains.length > 0) {
        condition += " and C."+this.NodeDomain+" in (";
        for (int i = 0; i < domains.length; i++) {
          if (i != 0) condition += ",";
          condition += "'"+domains[i]+"'";
        }
        condition += ")";
      }
      condition += " and A."+this.Status+" != 'Inactive'";
      condition += " order by A."+this.DomainID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      LoggingUtils.Log("NodeOperation>>>GetOperationsList>>> sql is: "+sql
                       , Level.DEBUG, Phrase.TaskLoggerName);
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Operation[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          retArray[i] = new Operation(rs.getInt(this.OpID));
          retArray[i].SetType(rs.getString(this.OpType));
          retArray[i].SetDomain(rs.getString(this.NodeDomain));
          retArray[i].SetWebService(rs.getString(this.WebService));
          retArray[i].SetOpName(rs.getString(this.OpName));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get List of Operations: " + e.toString());
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
    return retArray;
  }

  // WI 22317
  /**
   * GetAllOperationsList
   * @param domains
   * @return Operation[]
   */
  public Operation[] GetAllOperationsList (String[] domains)
  {
    Operation[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] select = new String[] { "A."+this.OpID,"A."+this.OpType,"A."+this.OpName,"B."+this.WebService,"C."+this.NodeDomain };
      String tableNames = this.TableName+" A left join "+this.WebServiceTableName+" B on A."+this.WSID+" = B."+this.WSID;
      tableNames += ", "+this.NodeDomainTableName+" C";
      String condition = "A."+this.DomainID+" = C."+this.DomainID;
      if (domains != null && domains.length > 0) {
        condition += " and C."+this.NodeDomain+" in (";
        for (int i = 0; i < domains.length; i++) {
          if (i != 0) condition += ",";
          condition += "'"+domains[i]+"'";
        }
        condition += ")";
      }
      condition += " and A."+this.Status+" != 'Inactive'";
      condition += " order by A."+this.DomainID;
      String sql = this.GetSelectStr(select,tableNames,condition);
      db = this.GetNodeDB();
      LoggingUtils.Log("NodeOperation>>>GetOperationsList>>> sql is: "+sql
                       , Level.DEBUG, Phrase.TaskLoggerName);
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() > 0) {
        retArray = new Operation[rs.getRow()];
        rs.beforeFirst();
        for (int i = 0; rs.next() && i < retArray.length; i++) {
          retArray[i] = new Operation(rs.getInt(this.OpID));
          retArray[i].SetType(rs.getString(this.OpType));
          retArray[i].SetDomain(rs.getString(this.NodeDomain));
          retArray[i].SetWebService(rs.getString(this.WebService));
          retArray[i].SetOpName(rs.getString(this.OpName));
        }
      }
    } catch (Exception e) {
      this.LogException("Could not Get List of Operations: " + e.toString());
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
    return retArray;
  }

  /**
   * GetOperationsManagerDocument
   * @param submitID
   * @return ClsNodeDocument
   */
  public ClsNodeDocument GetOperationsManagerDocument (String submitID)
  {
	byte[] content = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    Blob blob = null;
    InputStream is = null;
    ClsNodeDocument retDoc = new ClsNodeDocument();
    String path = Utility.GetTempFilePath()+"/temp/";
    String filePathAndName = path + "submit_report" + Utility.GetSysTimeString() + ".zip";
    
    try {
      String sql = "select FILE_CONTENT from NODE_OPERATION_MANAGER where SUBMIT_ID =" + Integer.parseInt(submitID);
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      while (rs.next()) {
    	  blob = rs.getBlob("FILE_CONTENT");
      }
      is = blob.getBinaryStream();
      Utility.writeFile(filePathAndName, is);
      retDoc.setName(filePathAndName);
      retDoc.setType(Phrase.ZIP_TYPE);
    } catch (Exception e) {
      this.LogException("Could not Get Queries: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
        if (is != null)
        	is.close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retDoc;
  }
  
  /**
   * GetAllDataWizardOperationsIDList
   * @param 
   * @return Hashtable
   */
	public Hashtable GetAllDataWizardOperationsIDList() {
		Hashtable ret = new Hashtable();
		IDBAdapter db = null;
		ResultSet rs = null;
		String xml = null;
		String opID = null;
		String opName = null;
		Clob xmlClob = null;
		
		try {
			String sql = "select "+this.OpID + "," +this.OpName +",extract(" + this.OpConfig + ",'/').getclobval() xml from " + this.TableName;
			db = this.GetNodeDB();
			rs = db.GetResultSet(sql);
			while (rs.next()) {
				xmlClob = rs.getClob("xml");
				if(xmlClob!=null){
					xml = xmlClob.getSubString(1, new Long(xmlClob.length()).intValue());
					if(xml.indexOf("<extension>") != -1){//WI 22691
						opID = Integer.toString(rs.getInt(this.OpID));
						opName = rs.getString(this.OpName);
						ret.put(opID, opName);
					}					
				}
			}
		} catch (Exception e) {
			this.LogException("Could not Get Operation List: " + e.toString());
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
		return ret;

	}

}
