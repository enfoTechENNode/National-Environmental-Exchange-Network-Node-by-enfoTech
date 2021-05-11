package Node.DB.Oracle10i;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import Node.DB.Interfaces.INodeOperationParameter;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create NodeOperationParameter.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeOperationParameter extends NodeDB implements INodeOperationParameter {
  // Table Name
  private String TableName = "NODE_OPERATION_LOG_PARAMETER";

  // Column Names
  private String OpLogID = "OPERATION_LOG_ID";
  private String ParamName = "PARAMETER_NAME";
  private String ParamValue = "PARAMETER_VALUE";

  /**
   * Constructor
   * @param loggerName
   * @return 
   * @deprecated
   */
  public NodeOperationParameter(String loggerName) {
    super(loggerName);
  }

  /**
   * UpdateParameterValues
   * @param opLogID
   * @param paramNames
   * @param paramValues
   * @return boolean
   * @deprecated
   */
  public boolean UpdateParameterValues (int opLogID, String[] paramNames, Object[] paramValues)
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    boolean retBool = false;
    if (paramNames == null || paramNames.length <= 0 || paramValues == null || paramNames.length != paramValues.length || opLogID < 0)
      return retBool;
    try {
      String sql = this.GetSelectStr(new String[]{this.OpLogID,this.ParamName,this.ParamValue},this.TableName,
                                     new String[]{this.OpLogID},new String[]{"-999999"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null) {
        for (int i = 0; i < paramNames.length; i++) {
        	if(paramNames[i]!=null && !paramNames[i].equals("")){
                rs.moveToInsertRow();
                rs.updateInt(this.OpLogID,opLogID);
                rs.updateString(this.ParamName,paramNames[i]);
                if (paramValues[i] != null) {
                  /*
                  if (paramNames[i].equalsIgnoreCase(Phrase.ParamCredential)) {
                    Cryptography crypt = new Cryptography();
                    rs.updateString(this.ParamValue, crypt.Encrypting((String)paramValues[i],Phrase.CryptKey));
                  }
                  else*/
                    rs.updateString(this.ParamValue, paramValues[i].toString());
                }
                rs.insertRow();        		
        	}
        }
        retBool = true;
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Update Parameter List: " + e.toString());
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
    return retBool;
  }
  
  /**
   * UpdateExistParameterValues
   * @param opLogID
   * @param paramNames
   * @return boolean
   * @deprecated
   */
  public boolean UpdateExistParameterValues(int opLogID, String[] paramNames,
			Object[] paramValues) {
		IDBAdapter db = null;
		ResultSet rs = null;
		boolean retBool = false;
		Connection con = null;
		PreparedStatement ps = null;
		if (paramNames == null || paramNames.length <= 0 || paramValues == null
				|| paramNames.length != paramValues.length || opLogID < 0)
			return retBool;
		try {
			String sql = "select " + this.ParamName + "," + this.ParamValue + " from " + this.TableName + " where "
					+ this.OpLogID + " = " + opLogID;
			db = this.GetNodeDB();
			db.KeepConnectionAfterExecute(true);
			db.BeginTransaction();
			con = db.GetConnection();
			ps = con.prepareStatement(sql, ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
			rs = ps.executeQuery();
			if (rs != null && rs.isBeforeFirst()) {
				for (int i = 0; i < paramNames.length; i++) {
					while(rs.next()){
						if(rs.getString(this.ParamName).equalsIgnoreCase(paramNames[i])){
							if (paramValues[i] != null) {
								/*
								 * if
								 * (paramNames[i].equalsIgnoreCase(Phrase.ParamCredential
								 * )) { Cryptography crypt = new Cryptography();
								 * rs.updateString(this.ParamValue,
								 * crypt.Encrypting((String
								 * )paramValues[i],Phrase.CryptKey)); } else
								 */
								rs.updateString(this.ParamValue, paramValues[i].toString());
							}
							rs.updateRow();							
						}
					}
					rs.beforeFirst();						
				}
				retBool = true;
			}
			db.CommitTransaction();
		} catch (Exception e) {
			try {
				db.RollBackTransaction();
			} catch (SQLException e1) {
				this.LogException("Could Not Get Update Parameter List: " + e1.toString());			
			}
			this.LogException("Could not save registration file: " + e.toString());			
		} finally {
			try {
		        db.KeepConnectionAfterExecute(false);
		        db.EndTransaction();
		        if (ps != null)
		        	ps.close();
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
   * GetParameterValues
   * @param opLogId
   * @return Object[]
   * @deprecated
   */
  public Object[] GetParameterValues (int opLogId)
  {
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  ArrayList myName = new ArrayList();
	  ArrayList myValue = new ArrayList();
	  Object[] ret = new Object[2];
	  try {
		  String sql = "select * from " + this.TableName + " where " + this.OpLogID + " = " + opLogId;
		  db = this.GetNodeDB();
		  rs = db.GetResultSet(sql);
		  while(rs.next()) {
			  myName.add(rs.getString(2));
			  myValue.add(rs.getString(3));
		  }
		  ret[0] = myName.toArray();
		  ret[1] = myValue.toArray();

	  } catch (Exception e) {
		  this.LogException("Could Not Get Update Parameter List: " + e.toString());
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
	  return ret;
  }

  /**
   * GetParameterValues
   * @param transID
   * @return Object[]
   * @deprecated
   */
  public Object[] GetParameterValues (String transID)
  {
	  IDBAdapter db = null;
	  ResultSet rs = null;
	  ArrayList myName = new ArrayList();
	  ArrayList myValue = new ArrayList();
	  Object[] ret = new Object[2];
	  try {
		  String sql = "select B.* from NODE_OPERATION_LOG A, NODE_OPERATION_LOG_PARAMETER B WHERE A.operation_log_id = B.operation_log_id AND A.trans_id = '" + transID +"'";
		  db = this.GetNodeDB();
		  rs = db.GetResultSet(sql);
		  while(rs.next()) {
			  myName.add(rs.getString(2));
			  myValue.add(rs.getString(3));
		  }
		  ret[0] = myName.toArray();
		  ret[1] = myValue.toArray();

	  } catch (Exception e) {
		  this.LogException("Could Not Get Update Parameter List: " + e.toString());
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
	  return ret;
  }
  
  /**
   * GetParameterNames
   * @param opID
   * @return List
   * @deprecated
   */
  public List GetParameterNames(int opID) {
		IDBAdapter db = null;
		ResultSet rs = null;
		ArrayList nameList = new ArrayList();
		try {
			String sql = "select DISTINCT A.PARAMETER_NAME from NODE_OPERATION_LOG_PARAMETER A,NODE_OPERATION_LOG B,NODE_OPERATION C where A.OPERATION_LOG_ID = B.OPERATION_LOG_ID AND B.OPERATION_ID = C.OPERATION_ID AND  C.OPERATION_ID = "
					+ opID;
			db = this.GetNodeDB();
			rs = db.GetResultSet(sql);
			while (rs.next()) {
				nameList.add(rs.getString("PARAMETER_NAME"));
			}

		} catch (Exception e) {
			this.LogException("Could Not Get Update Parameter List: "
					+ e.toString());
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
		return nameList;
	}


}
