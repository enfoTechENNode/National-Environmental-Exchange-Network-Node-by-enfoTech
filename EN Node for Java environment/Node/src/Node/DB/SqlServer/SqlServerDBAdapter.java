package Node.DB.SqlServer;
/**
 * <p>This class create SqlServerDBAdapter.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

import java.io.Reader;
import java.sql.Blob;
import java.sql.CallableStatement;
import java.sql.Clob;
import java.sql.Connection;
import java.sql.Driver;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Hashtable;

import javax.naming.InitialContext;
import javax.sql.DataSource;

import oracle.jdbc.driver.OracleTypes;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.data.DataAdapter;
import com.enfotech.basecomponent.typelib.data.DataColumn;
import com.enfotech.basecomponent.typelib.data.DataRow;
import com.enfotech.basecomponent.typelib.data.DataSet;
import com.enfotech.basecomponent.typelib.data.DataTable;
import com.enfotech.basecomponent.typelib.data.IsolationLevel;
import com.enfotech.basecomponent.typelib.data.Parameter;
import com.enfotech.basecomponent.typelib.data.ParameterCollection;
import com.enfotech.basecomponent.typelib.data.ParameterDirection;
import com.microsoft.sqlserver.jdbc.SQLServerConnection;

/**
 * This class is used to handle sqlserver database connection.
 * <p>Title: SqlServerDBAdapter.java</p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2004</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 1.0
 */
public class SqlServerDBAdapter
    implements IDBAdapter {

    /**
     * Gets or Sets the timeout seconds.
     */
    public int TimeoutSecond = 0;

    private static final int FIRST_COLUMN = 1;
    private static final String COLUMN_NAME = "ColumnName";
    private static final String COLUMN_SIZE = "ColumnSize";
    private static final String DATA_TYPE = "DataType";
    private static final String DRIVER_CLASS_NAME = "com.microsoft.sqlserver.jdbc.SQLServerDriver";

    private String strSource = null;
    private boolean bKeepConnection = false;
    private boolean bInTranscation = false;
    private Hashtable htSavePoint = null;
    private Hashtable dbHash = null;
    private Statement statement = null;
    private CallableStatement callableStatement = null;
    protected Connection connection = null;
    protected Connection dsConnection = null;
    private int defaultRowPrefetch = -1;
    protected boolean jndiConn = false;

    /**
     * Constructs a SqlServerDBAdapter object with a connection string.
     * @param source The database connection string.
     */
    public SqlServerDBAdapter(String source) {
        this.strSource = source;
        this.htSavePoint = new Hashtable();
        this.dbHash = new Hashtable();
    }

    /**
     * This method is a 100% Java driver for client-side use.
     * @param server The database host name.
     * @param database The database sid name.
     * @param userId The user ID.
     * @param password The password for specified user.
     * @return A connection string like "jdbc:microsoft:sqlserver://server1:1433;DatabaseName=northwind;User=test;Password=secret".
     */
    public static String GetConnectionString(String server, String database,
                                             String userId, String password) {
        return "jdbc:sqlserver://" + server + ":1433;DatabaseName=" +
            database + ";SelectMethod=Cursor;User=" + userId + ";Password=" + password;
    }

    /**
     * This method is a 100% Java driver for client-side use.
     * @param server The database host name.
     * @param port The port number of the database host.
     * @param database The database sid name.
     * @param userId The user ID.
     * @param password The password for specified user.
     * @return A connection string like "jdbc:microsoft:sqlserver://server1:1433;DatabaseName=northwind;User=test;Password=secret".
     */
    public static String GetConnectionString(String server, String port,
                                             String database, String userId,
                                             String password) {
        return "jdbc:sqlserver://" + server + ":" + port +
            ";DatabaseName=" + database + ";SelectMethod=Cursor;User=" + userId +
            ";Password=" + password;
    }

    /**
     * Keeps the database connection after execute sql statement.
     * @param isKeepConnection It is true to keep the database connection; otherwise, false.
     */
    public void KeepConnectionAfterExecute(boolean isKeepConnection) {
        this.bKeepConnection = isKeepConnection;
    }

    /**
     * Gets the database type.
     * @return A byte value indicates the database type. It should be one of com.enfotech.basecomponent.db.Phrase.
     */
    public byte GetDbType() {
        return com.enfotech.basecomponent.db.Phrase.ValueMSSql;
    }

    /**
     * Begins transaction.
     * @param isolationLevel The isolation level. It should be one of IsolationLevel.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean BeginTransaction(IsolationLevel isolationLevel) throws
        SQLException {
        try {
            if (!this.Open()) {
                return false;
            }
            this.connection.setTransactionIsolation(isolationLevel.
                isolationLevel);
            this.connection.setAutoCommit(false);
            this.bInTranscation = true;
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            if (!this.bKeepConnection) {
                this.Close();
            }
            throw ex;
        }
        return true;
    }

    /**
     * Begins transaction.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean BeginTransaction() throws SQLException {
        try {
            if (!this.Open()) {
                return false;
            }
            this.connection.setAutoCommit(false);
            this.bInTranscation = true;
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            if (!this.bKeepConnection) {
                this.Close();
            }
            throw ex;
        }
        return true;
    }

    /**
     * Sets the save point for a transaction.
     * @param name The name of save point.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */

    public boolean SetSavePoint(String name) throws SQLException {
        try {
            if (this.connection == null) {
                return false;
            }
            java.sql.Savepoint sp = this.connection.setSavepoint(name);
            //oracle.jdbc.OracleSavepoint sp = this.connection.oracleSetSavepoint(
            //    name);
            // If the key already exists in Hashtable, then remove it first.
            if (this.htSavePoint.containsKey(name)) {
                this.htSavePoint.remove(name);
            }
            this.htSavePoint.put(name, sp);
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            throw ex;
        }
        return true;
    }

    /**
     * Ends the transaction.
     * @throws SQLException Any error occurs.
     */
    public void EndTransaction() throws SQLException {
        this.bInTranscation = false;
        if(this.connection != null){
          this.connection.setAutoCommit(true);
          if (!this.bKeepConnection) {
            this.Close();
          }
        }else if(!this.bKeepConnection)
          this.Close();
    }

    /**
     * Commits a transaction.
     * @throws SQLException Any error occurs.
     * @return If is successful, return true; otherwise, false.
     */
    public boolean CommitTransaction() throws SQLException {
        boolean bFlag = false;

        try {
            if (this.connection == null) {
                return bFlag;
            }
            this.connection.commit();
            bFlag = true;
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            throw ex;
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
        return bFlag;
    }

    /**
     * Rollbacks a transaction.
     * @param name The name of save point.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean RollBackTransaction(String name) throws SQLException {
        boolean bFlag = false;

        try {
            if (this.connection == null) {
                return bFlag;
            }
            Object obj = this.htSavePoint.get(name);
            if (obj != null) {
                //this.connection.oracleRollback( (oracle.jdbc.OracleSavepoint)
                //                              obj);
                this.connection.rollback((java.sql.Savepoint) obj);
                bFlag = true;
            }
            else {
                bFlag = false;
            }
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            throw ex;
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
        return bFlag;
    }

    /**
     * Rollbacks a transaction.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean RollBackTransaction() throws SQLException {
        boolean bFlag = false;

        try {
            if (this.connection == null) {
                return bFlag;
            }
            this.connection.rollback();
            bFlag = true;
        }
        catch (SQLException ex) {
            this.bInTranscation = false;
            throw ex;
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
        return bFlag;
    }

    /**
     * Executes a insert, update or delete sql statement.
     * @param command The sql command string.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean ExecuteNonQuery(String command) throws SQLException {
        boolean bFlag = false;
        Statement st = null;

        try {
            this.Open();
            st = this.connection.createStatement();
            if (this.TimeoutSecond > 0) {
                st.setQueryTimeout(this.TimeoutSecond);
            }
            st.executeUpdate(command);
            bFlag = true;
        }
        catch (SQLException ex) {
            throw ex;
        }
        finally {
        	try {
                if (st != null) {
                    st.close();
                }
        	} catch (Exception e) {        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                }
        	}
        }
        return bFlag;
    }

    /**
     * Executes a sql statement and return the value of the first row and first column.
     * @param command The sql command string.
     * @throws SQLException Any error occurs.
     * @return An Object return.
     */
    public Object ExecuteScalar(String command) throws SQLException {
        Statement st = null;
        Object obj = null;
        ResultSet rs = null;
        try {
            this.Open();
            st = this.connection.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
            if (this.TimeoutSecond > 0) {
                st.setQueryTimeout(this.TimeoutSecond);
            }
            rs = st.executeQuery(command);
            ResultSetMetaData meta = rs.getMetaData();
            if (rs.next()) {
                if (meta.getColumnType(FIRST_COLUMN) == Types.CLOB) {
                    Clob c_lob = rs.getClob(FIRST_COLUMN);
                    Reader reader = c_lob.getCharacterStream();
                    char[] ch_buf = new char[ (int) c_lob.length()];
                    reader.read(ch_buf);
                    obj = new String(ch_buf);
                    reader.close();
                }
                else if (meta.getColumnType(FIRST_COLUMN) == Types.BLOB) {
                    Blob b_lob = rs.getBlob(FIRST_COLUMN);
                    byte[] b_buf = new byte[ (int) b_lob.length()];
                    b_buf = b_lob.getBytes(1, (int) b_lob.length());
                    obj = b_buf;
                }
                else {
                    obj = rs.getObject(FIRST_COLUMN);
                }
            }
        }
        catch (SQLException ex) {
        	throw ex;
        }
        catch (Exception ex) {
            throw new SQLException(ex.getMessage());
        }
        finally {
        	try {
                if(rs != null){
                    rs.close();
                    rs = null;
                  }
                  if (st != null) {
                    st.close();
                    st = null;
                  }
        	} catch (Exception e) {
        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                  }
        	}
        }
        return obj;
    }

    /**
     * Gets ResultSet for a specified sql command.
     * @param command A SQL command.
     * @throws SQLException An exception that provides information on a database access error or other errors.
     * @return ResultSet Call Close() method to close ResultSet after use it.
     */
    public ResultSet ExecuteReader(String command) throws SQLException {
        ResultSet rs = null;

        try {
            this.Open();
            this.statement = this.connection.createStatement(ResultSet.
                TYPE_SCROLL_INSENSITIVE,
                ResultSet.CONCUR_READ_ONLY);
            if (this.TimeoutSecond > 0) {
                this.statement.setQueryTimeout(this.TimeoutSecond);
            }
            rs = this.statement.executeQuery(command);
        }
        catch (SQLException ex) {
            throw ex;
        }
        return rs;
    }

    /**
     * Calls a stored procedure with specified name and parameter collection.
     * @param procedureName The name of stored procedure.
     * @param parameterCollection The ParameterCollection contains all parameters.
     * @throws SQLException Any error occurs.
     * @return A ParameterCollection object contains return information.
     */
    public ParameterCollection CallProceduerNonQuery(String procedureName,
            ParameterCollection parameterCollection) throws SQLException {
        CallableStatement cs = null;

        try {
            this.Open();
            String sql = BuildSQL(procedureName, parameterCollection);
            cs = this.connection.prepareCall(sql);
            PrepareParameters(cs, parameterCollection);
            if (this.TimeoutSecond > 0) {
                cs.setQueryTimeout(this.TimeoutSecond);
            }
            cs.execute();
            return GetParameterCollection(cs, parameterCollection);
        }
        catch (SQLException ex) {        	
            throw ex;
        }
        finally {
        	try {
                if (cs != null) {
                    cs.close();
                }
        	} catch (Exception e) {
        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                }
        	}
        }
    }

    /**
     * Calls a stored procedure with specified name and parameters.
     * @param procedureName The name of stored procedure.
     * @param parameterList The ArrayList contains Parameter objects.
     * @param valueList ArrayList The ArrayList contains the value of input parameters.
     * @throws SQLException Any error occurs.
     * @return A ParameterCollection object contains return information.
     */
    public ParameterCollection CallProcedureNonQuery(String procedureName,
            ArrayList parameterList, ArrayList valueList) throws SQLException {
        CallableStatement cs = null;

        try {
            this.Open();
            String sql = BuildSQL(procedureName, parameterList);
            cs = this.connection.prepareCall(sql);
            ParameterCollection parameterCollection = PrepareParameters(cs,
                parameterList, valueList);
            if (this.TimeoutSecond > 0) {
                cs.setQueryTimeout(this.TimeoutSecond);
            }
            cs.execute();
            return GetParameterCollection(cs, parameterCollection);
        }
        catch (SQLException ex) {
            throw ex;
        }
        finally {
        	try {
                if (cs != null) {
                    cs.close();
                }
        	} catch (Exception e) {
        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                }
        	}
        }
    }

    /**
     * Calls stored procedure to return a ResultSet with specified stored procedure name and parameters.
     * To use this method, you have to manually call Close() method after using ResultSet.
     * @param procedureName The name of stored procedure.
     * @param parameterCollection A ParameterCollection contains Parameter objects.
     * @throws SQLException Any error occurs.
     * @return A ResultSet contains data information.
     */
    public ResultSet CallProcedureReader(String procedureName,
    		ParameterCollection
    		parameterCollection) throws
    		SQLException {

    	try {
    		this.Open();
    		String sql = BuildSQL(procedureName, parameterCollection);
    		this.callableStatement = this.connection.prepareCall(sql);
    		PrepareParameters(this.callableStatement, parameterCollection);
    		if (this.TimeoutSecond > 0) {
    			this.callableStatement.setQueryTimeout(this.TimeoutSecond);
    		}
    		this.callableStatement.execute();

    		// Gets ResultSet from output parameter.
    		for (int i = 0; i < parameterCollection.Count(); i++) {
    			Parameter parameter = parameterCollection.Item(i);
    			if (parameter.Direction == ParameterDirection.Output &&
    					parameter.DbType == OracleTypes.CURSOR) {
    				return (ResultSet)this.callableStatement.getObject(
    						parameter.ParameterName);
    			}
    		}
    		return null;
    	}
    	catch (SQLException ex) {
    		throw ex;
    	}
    }

    /**
     * Calls stored procedure to return a ResultSet with specified stored procedure name and parameters.
     * To use this method, you have to manually call Close() method after using ResultSet.
     * @param procedureName The name of stored procedure.
     * @param parameterList An ArrayList contains Parameter objects.
     * @param valueList An ArrayList contains the values of Parameter objects.
     * @throws SQLException Any error occurs.
     * @return A ResultSet contains data information.
     */
    public ResultSet CallProcedureReader(String procedureName,
                                         ArrayList parameterList,
                                         ArrayList valueList) throws
        SQLException {

        try {
            this.Open();
            String sql = BuildSQL(procedureName, parameterList);
            this.callableStatement = this.connection.prepareCall(sql);
            ParameterCollection parameterCollection = PrepareParameters(this.
                callableStatement,
                parameterList, valueList);
            if (this.TimeoutSecond > 0) {
                this.callableStatement.setQueryTimeout(this.TimeoutSecond);
            }
            this.callableStatement.execute();

            // Gets ResultSet from output parameter.
            for (int i = 0; i < parameterCollection.Count(); i++) {
                Parameter parameter = parameterCollection.Item(i);
                if (parameter.Direction == ParameterDirection.Output &&
                    parameter.DbType == OracleTypes.CURSOR) {
                    return (ResultSet)this.callableStatement.getObject(
                        parameter.ParameterName);
                }
            }
            return null;
        }
        catch (SQLException ex) {
            throw ex;
        }
    }

    /**
     * Calls stored procedure to return a DataSet object.
     * @param procedureName The name of stored procedure.
     * @param parameterCollection A ParameterCollection contains Parameter objects.
     * @throws SQLException Any error occurs.
     * @return A DataSet object contains all data.
     */
    public DataSet CallProcedureDataSet(String procedureName,
                                        ParameterCollection parameterCollection) throws
        SQLException {
        DataSet dataSet = new DataSet();
        try {
            ResultSet resultSet = CallProcedureReader(procedureName,
                parameterCollection);
            if (resultSet == null) {
                return dataSet;
            }

            DataAdapter dataAdapter = new DataAdapter(resultSet);
            dataAdapter.Fill(dataSet);
        }
        catch (SQLException ex) {
            throw ex;
        }
        catch (Exception ex) {
            throw new SQLException(ex.getMessage());
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
        return dataSet;
    }

    /**
     * Calls stored procedure to return a DataSet object.
     * @param procedureName The name of stored procedure.
     * @param parameterList An ArrayList contains Parameter objects.
     * @param valueList An ArrayList contains values of Parameter objects.
     * @throws SQLException Any error occurs.
     * @return A DataSet object contains all data.
     */
    public DataSet CallProcedureDataSet(String procedureName,
                                        ArrayList parameterList,
                                        ArrayList valueList) throws
        SQLException {
        DataSet dataSet = new DataSet();
        try {
            ResultSet resultSet = CallProcedureReader(procedureName,
                parameterList, valueList);
            if (resultSet == null) {
                return dataSet;
            }

            DataAdapter dataAdapter = new DataAdapter(resultSet);
            dataAdapter.Fill(dataSet);
        }
        catch (SQLException ex) {
            throw ex;
        }
        catch (Exception ex) {
            throw new SQLException(ex.getMessage());
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
        return dataSet;
    }

    /**
     * Gets ResultSet.
     * @param command A SQL statement.
     * @throws SQLException Any error occurs.
     * @return An ResultSet object.
     */
    public ResultSet GetResultSet(String command) throws
        SQLException {
        try {
            if (!this.Open()) {
                return null;
            }
         // WI 23186
            this.statement = this.connection.createStatement(ResultSet.
                TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);

            if (this.TimeoutSecond > 0) {
                this.statement.setQueryTimeout(this.TimeoutSecond);
            }
            ResultSet rs = this.statement.executeQuery(command);
            return rs;
        }
        catch (SQLException ex) {
            throw ex;
        }
    }

    /**
     * Gets DataSet object by specified sql command. If the returned DataSet object
     * is used for update, using transaction is suggested. If it is read only, suggest to use
     * GetDataTable() method. This method will not close database connection automatically.
     * You can call Close() manually or call UpdateDataSet() method which will call Close() automatically.
     * @param tableName The name of table which is used for update dataset.
     * @param command The SQL command string.
     * @param dataSet A DataSet object returned with data.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean GetDataSet(String tableName, String command, DataSet dataSet) throws
    SQLException {
    	ResultSet rs = null;
    	try {
    		if (!this.Open()) {
    			return false;
    		}
    		if (dataSet == null) {
    			dataSet = new DataSet();
    		}
    		// WI 23186
    		this.statement = this.connection.createStatement(ResultSet.
    				TYPE_SCROLL_SENSITIVE, ResultSet.CONCUR_UPDATABLE);

    		if (this.TimeoutSecond > 0) {
    			this.statement.setQueryTimeout(this.TimeoutSecond);
    		}

    		rs = this.statement.executeQuery(command);
    		if (this.dbHash.containsKey(tableName)) {
    			this.dbHash.remove(tableName);
    		}
    		this.dbHash.put(tableName, rs);

    		DataAdapter dataAdapter = new DataAdapter(rs);
    		dataAdapter.Fill(tableName, dataSet);

    		return true;
    	}
    	catch (SQLException ex) {
    		throw ex;
    	}
    	catch (Exception ex) {
    		throw new SQLException(ex.getMessage());
    	}
    	finally {
    		if (!this.bKeepConnection && !this.bInTranscation) {
    			try {
    				if(rs != null){
    					rs.close();
    					rs = null;
    				}
    				if (this.statement != null) {
    					this.statement.close();
    				}
    			} catch (Exception e) {

    			} finally {
    				this.Close();
    			}
    		}
    	}
    }

    /**
     * Gets DataTable object by specified sql PreparedStatement. The returned DataTable object
     * is readonly and can not be used for updating.
     * @param preparedStatement The sql command.
     * @param dataTable A DataTable object returned with data.
     * @param values for preparedStatement
     * @throws SQLException Any SQL error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean GetDataTable(String prepareStatement, DataTable dataTable,
                                ArrayList values) throws
        SQLException {
        PreparedStatement st = null;
        ResultSet rs = null;
        boolean bFlag = false;
        boolean hasException = false;
        try {
            if (!this.Open()) {
                return bFlag;
            }
            if (dataTable == null) {
                dataTable = new DataTable();
            }
            // WI 23186
            st = this.connection.prepareStatement(prepareStatement, ResultSet.
                                                 TYPE_SCROLL_INSENSITIVE,
                                                 ResultSet.CONCUR_READ_ONLY);
           //set values
           if(values != null)
           {
               for (int i = 0; i < values.size(); i++)
                   st.setObject(i+1, values.get(i));
           }

           if (this.TimeoutSecond > 0) {
                st.setQueryTimeout(this.TimeoutSecond);
            }

            rs = st.executeQuery();

            DataAdapter dataAdapter = new DataAdapter(rs);
            dataAdapter.Fill(dataTable);
            bFlag = true;
        }
        catch (SQLException ex) {
        	hasException = true;
        	throw ex;
        }
        catch (Exception ex) {
        	hasException = true;
        	throw new SQLException(ex.getMessage());
        }
        finally {
        	try {
                if(hasException){
                  	// debug
//                  	SystemMessage sm = new SystemMessage();
//                  	String valueStr = "";
//                  	if(values != null){
//        	          	for(int i = 0; i < values.size(); i++){
//        	          		if(i == 0){
//        	          			valueStr += values.get(i) + "";
//        	          		}else{
//        	          			valueStr += ", " + values.get(i);
//        	          		}
//        	          	}
//                  	}
//                  	sm.writeMessage("*** SQLException, SQL: " + prepareStatement + "; values: " + 
//                  			valueStr + " ***");
//                  	sm = null;
//                  	// end of debugging        	  
                  }
                  if(rs != null){
                    rs.close();
                    rs = null;
                  }
                  if (st != null) {
                    st.close();
                    st = null;
                  }
        	} catch (Exception e) {
        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                  }
        	}
        }
        return bFlag;
    }

    /**
     * Gets DataTable object by specified sql command. The returned DataTable object
     * is readonly and can not be used for updating.
     * @param command The sql command string.
     * @param dataTable A DataTable object returned with data.
     * @throws SQLException Any SQL error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean GetDataTable(String command, DataTable dataTable) throws
        SQLException {
        Statement st = null;
        ResultSet rs = null;
        boolean bFlag = false;
        boolean hasException = false;
        
        try {
            if (!this.Open()) {
                return bFlag;
            }
            if (dataTable == null) {
                dataTable = new DataTable();
            }
            // WI 23186
            st = this.connection.createStatement(ResultSet.
                                                 TYPE_SCROLL_INSENSITIVE,
                                                 ResultSet.CONCUR_READ_ONLY);

            if (this.TimeoutSecond > 0) {
                st.setQueryTimeout(this.TimeoutSecond);

            }
            rs = st.executeQuery(command);

            DataAdapter dataAdapter = new DataAdapter(rs);
            dataAdapter.Fill(dataTable);
            bFlag = true;
        }
        catch (SQLException ex) {
        	hasException = true;
            throw ex;
        }
        catch (Exception ex) {
        	hasException = true;
            throw new SQLException(ex.getMessage());
        }
        finally {
        	try {
                if(hasException){
                    // debug
//                    SystemMessage sm = new SystemMessage();
//                    sm.writeMessage("*** SQLException, SQL: " + command + " ***");
//                    sm = null;
                    // end of debugging        	  
                  }        	
                  if(rs != null){
                    rs.close();
                    rs = null;
                  }
                  if (st != null) {
                    st.close();
                    st = null;
                  }
        	} catch (Exception e) {
        		
        	} finally {
                if (!this.bKeepConnection && !this.bInTranscation) {
                    this.Close();
                  }
        	}
        }
        return bFlag;
    }

    /**
     * Updates a DataSet to database.
     * @param tableName The name of table. It should be the same as GetDataSet() method.
     * @param dataSet A DataSet contains data to be updated.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean UpdateDataSet(String tableName, DataSet dataSet) throws
        SQLException {
        try {
            if (!this.Open()) {
                return false;
            }
            if (dataSet == null) {
                return false;
            }
            Object obj = this.dbHash.get(tableName);
            if (obj == null) {
                return false;
            }
            DataAdapter dataAdapter = new DataAdapter( (ResultSet) obj);
            dataAdapter.Update(dataSet);
            return true;
        }
        catch (SQLException ex) {
            throw ex;
        }
        catch (Exception ex) {
            throw new SQLException(ex.getMessage());
        }
        finally {
            if (!this.bKeepConnection && !this.bInTranscation) {
                this.Close();
            }
        }
    }

    /**
     * Gets the database connection object.
     * @return The OracleConnection.
     */
    public Connection GetConnection() {
        return this.connection;
    }

    /**
     * Gets the first row record by specified table name.
     * @param realTableName The real table name.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains one row record.
     */
    public DataTable GetOneRowTable(String realTableName) throws SQLException {
        DataTable dataTable = new DataTable();
        // WI 23186
        String sql = " SELECT TOP 1 * FROM " + realTableName;
        GetDataTable(sql, dataTable);

        return dataTable;
    }

    /**
     * Gets the table list.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains all table list.
     */
    public DataTable GetTableList() throws SQLException {
        DataTable dataTable = new DataTable();
        String sql = " SELECT * FROM USER_TABLES ORDER BY TABLE_NAME ";
        GetDataTable(sql, dataTable);

        return dataTable;
    }

    /**
     * Gets the view list.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains all view list.
     */
    public DataTable GetViewList() throws SQLException {
        DataTable dataTable = new DataTable();
        String sql = " SELECT * FROM USER_VIEWS ORDER BY VIEW_NAME ";
        GetDataTable(sql, dataTable);

        return dataTable;
    }

    /**
     * Gets the list of stored procedures.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains all list of stored procedures.
     */
    public DataTable GetStoredProcedureList() throws SQLException {
        DataTable dataTable = new DataTable();
        String sql = " SELECT * FROM USER_PROCEDURES ORDER BY PROCEDURE_NAME ";
        GetDataTable(sql, dataTable);

        return dataTable;
    }

    /**
     * Gets the schema table by specified command.
     * @param command The SQL command.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains column name, size and type.
     */
    public DataTable GetSchemaTable(String command) throws SQLException {
        ResultSet resultSet = ExecuteReader(command);
        return GetSchemaTable(resultSet);
    }

    /**
     * Gets the schema table by specified stored procedure name and parameters.
     * @param procedureName The name of stored procedure.
     * @param parameterCollection The ParameterCollection contains all parameters.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains column name, size and type.
     */
    public DataTable GetSchemaTable(String procedureName,
                                    ParameterCollection parameterCollection) throws
        SQLException {
        ResultSet resultSet = CallProcedureReader(procedureName,
                                                  parameterCollection);
        return GetSchemaTable(resultSet);
    }

    /**
     * Gets the schema table by specified stored procedure name and parameters.
     * @param procedureName The name of stored procedure.
     * @param parameterList The ArrayList contains the Parameter objects.
     * @param valueList The ArrayList contains the value of Parameter objects.
     * @throws SQLException Any error occurs.
     * @return A DataTable contains column name, size and type.
     */
    public DataTable GetSchemaTable(String procedureName,
                                    ArrayList parameterList,
                                    ArrayList valueList) throws SQLException {
        ResultSet resultSet = CallProcedureReader(procedureName, parameterList,
                                                  valueList);
        return GetSchemaTable(resultSet);
    }

    /**
     * Open a database connection.
     * @throws SQLException Any error occurs.
     * @return If it is successful, return true; otherwise, false.
     */
    public boolean Open() throws SQLException {
    	boolean bFlag = true;

    	if (this.connection == null || this.connection.isClosed()) {
    		try {
    			if(this.strSource.indexOf("jdbc:sqlserver") == 0){
    				this.jndiConn = false;
    				Driver driver = null;
    				try {
    					driver = DriverManager.getDriver(this.strSource);
    				}
    				catch (Exception ex) {
    					driver = null;
    				}
    				if (driver == null) {
    					Class.forName(DRIVER_CLASS_NAME);
    				}
    				if (this.connection == null || this.connection.isClosed()) {
    					this.connection = (SQLServerConnection)DriverManager.getConnection(this.
    							strSource);
    				}
    			}else if(this.strSource.indexOf("jdbc/") == 0){
    				this.jndiConn = true;
    				InitialContext ic = new InitialContext();
    				//Context envCtx = (Context)ic.lookup("java:comp/env");

    				//DataSource ds = (DataSource)envCtx.lookup(this.strSource);
    				Object obj = ic.lookup("java:comp/env/" + this.strSource);
    				// debug
    				//System.out.println("lookup obj class name: " + obj.getClass().getName());
    				// end debug
    				DataSource ds = (DataSource)obj;
    				this.dsConnection = ds.getConnection();
    				// WI 22083
    				if(this.dsConnection instanceof org.apache.commons.dbcp.DelegatingConnection ){
    					this.connection = (SQLServerConnection)((org.apache.commons.dbcp.DelegatingConnection)this.dsConnection).getInnermostDelegate();
    				}/*else if(this.dsConnection instanceof org.apache.tomcat.dbcp.dbcp.DelegatingConnection) {
              	this.connection = (OracleConnection)((org.apache.tomcat.dbcp.dbcp.DelegatingConnection)this.dsConnection).getInnermostDelegate();	            	
              }*/

    			}else{
    			}
    		}
    		catch (Exception ex) {
    			bFlag = false;
    			//          	System.out.println("Error on open connection:" + ex.getMessage());
    			ex.printStackTrace();
    		}
    	}
    	//natalius-change sqlserver connection to generic connection
    	//if(this.connection!=null && this.defaultRowPrefetch>0)
    	//  this.connection.setDefaultRowPrefetch(this.defaultRowPrefetch);

    	return bFlag;
      }

    /**
     * Close database connection and clear up unused object.
     * @throws SQLException Any error occurs.
     */
    public void Close() throws SQLException {
        this.bInTranscation = false;

        if (this.statement != null) {
            this.statement.close();
            //org.apache.commons.dbutils.DbUtils.closeQuietly(this.statement);
            this.statement = null;
        }
        if (this.callableStatement != null) {
            this.callableStatement.close();
            this.callableStatement = null;
        }
        //WI 22083
        if(this.jndiConn){
        	if (this.connection != null && this.connection == this.dsConnection) {
        		if (!this.connection.isClosed()) {
	            	//System.out.println("Close Connection: " +this.connection + " at " + Utility.GetNowTimeStamp());
        			this.connection.close();            	
        		}
        	}
        	if (this.dsConnection != null) {
        		if (!this.dsConnection.isClosed()) {
	            	//System.out.println("Close dsConnection: " +this.dsConnection + " at " + Utility.GetNowTimeStamp());
        			this.dsConnection.close();
        			//org.apache.commons.dbutils.DbUtils.closeQuietly(this.dsConnection);
        		}
        	}
    		this.dsConnection = null;
    		this.connection = null;
          /*
          if (this.connection != null) {
        	  this.connection.close();
              this.connection = null;
          }*/
        }else{
            //WI 22083
            if(this.connection != null){
                if(!this.connection.isClosed()){
                  this.connection.close();
                  //org.apache.commons.dbutils.DbUtils.closeQuietly(this.connection);
                }
                this.connection = null;
            }
        }
    }

    /**
     * GetSchemaTable
     * @param resultSet
     * @return DataTable
     */
    private DataTable GetSchemaTable(ResultSet resultSet) throws SQLException {
        DataTable dataTable = new DataTable();
        dataTable.Columns().Add(new DataColumn(COLUMN_NAME));
        dataTable.Columns().Add(new DataColumn(COLUMN_SIZE));
        dataTable.Columns().Add(new DataColumn(DATA_TYPE));

        ResultSetMetaData metaData = resultSet.getMetaData();
        for (int i = 0; i < metaData.getColumnCount(); i++) {
            DataRow row = dataTable.NewRow();
            row.Set(0, metaData.getColumnName(i + 1));
            row.Set(1, metaData.getColumnDisplaySize(i + 1) + "");
            row.Set(2, metaData.getColumnTypeName(i + 1));
            dataTable.Rows().Add(row);
        }
        return dataTable;
    }

    /**
     * GetParameterCollection
     * @param cs
     * @param parameterCollection
     * @return ParameterCollection
     */
    private ParameterCollection GetParameterCollection(CallableStatement cs,
        ParameterCollection parameterCollection) throws SQLException {
        for (int i = 0; i < parameterCollection.Count(); i++) {
            Parameter parameter = parameterCollection.Item(i);
            if (parameter.Direction == ParameterDirection.Output ||
                parameter.Direction == ParameterDirection.InputOutput) {
                parameter.Value = cs.getObject(i + 1);
            }
        }
        return parameterCollection;
    }

    /**
     * PrepareParameters
     * @param cs
     * @param parameterList
     * @param valueList
     * @return ParameterCollection
     */
    private ParameterCollection PrepareParameters(CallableStatement cs,
                                                  ArrayList parameterList,
                                                  ArrayList valueList) throws
        SQLException {
        ParameterCollection parameterCollection = new ParameterCollection();
        for (int i = 0; i < parameterList.size(); i++) {
            Parameter parameter = (Parameter) (parameterList.get(i));
            if (parameter.Direction == ParameterDirection.Input ||
                parameter.Direction == ParameterDirection.InputOutput) {
                parameter.Value = valueList.get(i);
            }
            parameterCollection.Add(parameter);
        }
        PrepareParameters(cs, parameterCollection);
        return parameterCollection;
    }

    /**
     * PrepareParameters
     * @param cs
     * @param parameterCollection
     * @return 
     */
    private void PrepareParameters(CallableStatement cs,
                                   ParameterCollection parameterCollection) throws
        SQLException {
        for (int i = 0; i < parameterCollection.Count(); i++) {
            Parameter parameter = parameterCollection.Item(i);
            if (parameter.Direction == ParameterDirection.Output ||
                parameter.Direction == ParameterDirection.InputOutput) {
                cs.registerOutParameter(i + 1,
                                        parameter.DbType);
            }
            else {
                cs.setObject(i + 1, parameter.Value,
                             parameter.DbType);
            }
        }
    }

    /**
     * BuildSQL
     * @param procedureName
     * @param parameterList
     * @return String
     */
    private String BuildSQL(String procedureName, ArrayList parameterList) {
        String sql = "{ call " + procedureName + " (";
        for (int i = 0; i < parameterList.size(); i++) {
            sql += "?,";
        }
        if (sql.endsWith(",")) {
            sql = sql.substring(0, sql.length() - 1);
        }
        sql += ") }";

        return sql;
    }

    /**
     * BuildSQL
     * @param procedureName
     * @param parameterCollection
     * @return String
     */
    private String BuildSQL(String procedureName,
                            ParameterCollection parameterCollection) {
        String sql = "{ call " + procedureName + " (";
        for (int i = 0; i < parameterCollection.Count(); i++) {
            sql += "?,";
        }
        if (sql.endsWith(",")) {
            sql = sql.substring(0, sql.length() - 1);
        }
        sql += ") }";

        return sql;
    }
    /**
     * getDefaultRowPrefetch
     * @param 
     * @return int
     */
    public int getDefaultRowPrefetch() {
        return defaultRowPrefetch;
    }
    /**
     * setDefaultRowPrefetch
     * @param defaultRowPrefetch
     * @return 
     */
    public void setDefaultRowPrefetch(int defaultRowPrefetch) {
        this.defaultRowPrefetch = defaultRowPrefetch;
    }
    /**
     * isBKeepConnection
     * @param 
     * @return boolean
     */
    public boolean isBKeepConnection() {
        return bKeepConnection;
    }


}
