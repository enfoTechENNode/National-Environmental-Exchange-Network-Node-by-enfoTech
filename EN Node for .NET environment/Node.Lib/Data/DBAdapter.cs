#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		DBAdapter.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.41115)
// History:		2005-01-26, Danwen Sun Creation
//
//				2006-08-07, JW add monitoring counter, can be use later on.
//						    For example: EAF.Lib.UI.Base.PageBase implement a DBTrace function.
// 
#endregion

using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Deployment.Application;

namespace Node.Lib.Data
{
    /// <summary>
    /// Represents a data access to a database.
    /// </summary>
    public class DBAdapter
    {
        #region Variable definition
        /// <summary>
        /// Constant variable of ODBC_Provider.
        /// </summary>
        public const string ODBC_Provider = "System.Data.Odbc";
        /// <summary>
        /// Constant variable of OleDB_Provider.
        /// </summary>
        public const string OleDB_Provider = "System.Data.OleDb";
        /// <summary>
        /// Constant variable of MSSQL_Provider.
        /// </summary>
        public const string MSSQL_Provider = "System.Data.SqlClient";
        /// <summary>
        /// Constant variable of Oracle_Provider.
        /// </summary>
        public const string Oracle_Provider = "System.Data.OracleClient";

        private static string key = "eafCrypto888";
        private const string SQLExpress = @".\SQLEXPRESS";
        private const string DBSpliter = ";";
        private const string DBServer = "SERVER";
        private const string DBDatabase = "DATABASE";
        private const string DBDataSource = "DATA SOURCE";
        private const string DBCatalog = "INITIAL CATALOG";
        private const string DBUserID = "USER ID";
        private const string DBPassword = "PASSWORD";
        private const string DBUID = "UID";
        private const string DBPWD = "PWD";
        private const string SelectClause = "SELECT";
        private const string UpdateClause = "UPDATE";
        private const string DeleteClause = "DELETE";
        private const string InsertClause = "INSERT";
        private const string Last_In_Win = "LAST_IN_WIN";
        private const string First_In_Win = "FIRST_IN_WIN";

        private string dbProviderName = null;
        private string dbConnectionString = null;
        private string dbServerName = null;
        private string dbDatabaseName = null;
        private bool bKeepConnection = false;
        private bool bInTransaction = false;
        private int iCommandTimeout = 300;

        private Hashtable dbCommandHash = new Hashtable();
        private DbProviderFactory provider = null;
        private DbConnection connection = null;
        private DbTransaction transaction = null;
        private DbDataReader dataReader = null;
        private XmlReader xmlReader = null;

        #endregion

        // 2006-08-07, JW, BEGIN
        // Please use FIND to search where these counters are used.

        /// <summary>
        /// For counting Query statement
        /// </summary>
        public static int QueryCounter = -1;
        /// <summary>
        /// For counting Update statement
        /// </summary>
        public static int UpdateCounter = -1;

        // 2006-08-07, JW, END

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EAF.Lib.Data.DBAdapter">DBAdapter</see> class.
        /// </summary>
        /// <param name="name">The name of connection string in the web.config or app.config</param>
        public DBAdapter(string name)
        {
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings[name];
            if (setting != null)
            {
                //throw new DataExceptionHandler("Can not find connection setting for '" + name + "'.");
                this.dbProviderName = setting.ProviderName;
                this.dbConnectionString = BuildConnectionString(setting.ConnectionString);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EAF.Lib.Data.DBAdapter">DBAdapter</see> class.
        /// </summary>
        /// <param name="providerName">The provider name, like "System.Data.SqlClient"</param>
        /// <param name="connectionString">The connection string used to connect to specified database.</param>
        public DBAdapter(string providerName, string connectionString)
        {
            this.dbProviderName = providerName;
            this.dbConnectionString = BuildConnectionString(connectionString);
        }

        /// <summary>
        /// Deconstructor DBAdapter object.
        /// </summary>
        ~DBAdapter()
        {
            this.Close();
        }
        #endregion

        #region Public Static functions
        /// <summary>
        /// A static method to get database connection string.
        /// </summary>
        /// <param name="datasource">The server name or datasource name.</param>
        /// <param name="database">The database name.</param>
        /// <param name="userid">The user id.</param>
        /// <param name="password">The password.</param>
        /// <param name="dbprovider">The database provider name. The value should be DBAdapter.MSSQL_Provider and DBAdapter.Oracle_Provider</param>
        /// <param name="bWinAuth">Indicator whether using windows authentication or not.</param>
        /// <returns>A database connection string.</returns>
        public static string GetConnectionString(string datasource, string database, string userid, string password, string dbprovider, bool bWinAuth)
        {
            if (dbprovider.Trim() == MSSQL_Provider)
            {
                if (bWinAuth && datasource != null && datasource.ToUpper().Trim() == SQLExpress)
                    return @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + database + ";Integrated Security=True;User Instance=True";
                if (bWinAuth && database != null && database.Trim() != String.Empty)
                    return "Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=SSPI";
                if (bWinAuth && (database == null || database.Trim() == String.Empty))
                    return "Data Source=" + datasource + ";Integrated Security=SSPI";
                if (!bWinAuth && database != null && database.Trim() != String.Empty)
                    return "server=" + datasource + ";database=" + database + ";User ID=" + userid + "; password=" + password;
                if (!bWinAuth && (database == null || database.Trim() == String.Empty))
                    return "server=" + datasource + ";User ID=" + userid + "; password=" + password;
                else
                    throw new DataExceptionHandler("Failed to build connection string for datasource '" + datasource + "' and database '" + database + "'.");
            }
            else if (dbprovider.Trim() == Oracle_Provider)
            {
                if (String.IsNullOrEmpty(datasource) || String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(password))
                    throw new DataExceptionHandler("DataSource, user id and password are required.");
                return "Data Source=" + datasource + ";User ID=" + userid + ";Password=" + password;
            }
            else
                throw new DataExceptionHandler("The data provider '" + dbprovider + "' is not supported!");
        }
        #endregion

        #region Properties
        /// <summary>
        /// Determines if to keep the database connection or not after execute a query.
        /// </summary>
        public bool KeepConnection
        {
            get { return this.bKeepConnection; }
            set { this.bKeepConnection = value; }
        }

        /// <summary>
        /// Gets or sets the command time out.
        /// </summary>
        public int CommandTimeout
        {
            get { return this.iCommandTimeout; }
            set { this.iCommandTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the Database Provider name.
        /// </summary>
        public string ProviderName
        {
            get { return this.dbProviderName; }
            set { this.dbProviderName = value; }
        }

        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return this.dbConnectionString; }
            set { this.dbConnectionString = BuildConnectionString(value); }
        }

        /// <summary>
        /// Gets the server name to be connected.
        /// </summary>
        public string ServerName
        {
            get
            {
                if (this.dbConnectionString == null)
                    return "";
                if (this.dbServerName != null)
                    return this.dbServerName;

                Open();
                this.dbServerName = this.connection.DataSource;
                Close();
                return this.dbServerName;
            }
        }

        /// <summary>
        /// Gets the database name to be connected.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                if (this.dbConnectionString == null)
                    return "";
                if (this.dbDatabaseName != null)
                    return this.dbDatabaseName;

                Open();
                this.dbDatabaseName = this.connection.Database;
                Close();
                return this.dbDatabaseName;
            }
        }

        /// <summary>
        /// Gets the user id to be used to connect data source.
        /// </summary>
        public string UserID
        {
            get
            {
                object obj = GetConnectionInfo(this.dbConnectionString)[DBUserID];
                if (obj == null)
                    obj = GetConnectionInfo(this.dbConnectionString)[DBUID];
                return (obj == null) ? "" : obj + "";
            }
        }

        /// <summary>
        /// Gets the password to be used to connect data source.
        /// </summary>
        public string Password
        {
            get
            {
                object obj = GetConnectionInfo(this.dbConnectionString)[DBPassword];
                if (obj == null)
                    obj = GetConnectionInfo(this.dbConnectionString)[DBPWD];
                return (obj == null) ? "" : obj + "";
            }
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Gets the information in connection string.
        /// </summary>
        /// <param name="connectionstring">A string vaulue of connection string.</param>
        /// <returns>A Hashtable contains the key and value pair information about the input connection string.</returns>
        public static Hashtable GetConnectionInfo(string connectionstring)
        {
            Hashtable ht = new Hashtable();
            if (connectionstring == null)
                return ht;

            string[] ss = connectionstring.Split(DBSpliter.ToCharArray());
            foreach (string s in ss)
            {
                string[] aa = s.Split('=');
                if (aa.Length != 2)
                    continue;
                ht.Add(aa[0].Trim().ToUpper(), aa[1].Trim());
            }
            return ht;
        }

        /// <summary>
        /// Open a database connection. If the connection is already opened, then do nothing.
        /// </summary>
        public void Open()
        {
            if (this.connection != null &&
                this.connection.State != ConnectionState.Closed &&
                this.connection.State != ConnectionState.Broken)
                return;

            this.provider = DbProviderFactories.GetFactory(this.dbProviderName);
            if (this.provider != null)
            {
                this.connection = this.provider.CreateConnection();
                this.connection.ConnectionString = this.dbConnectionString;
                this.connection.Open();

                // for testing to remove padding
                //if (this.ProviderName == MSSQL_Provider)
                //{
                //    bool bKeep = this.KeepConnection;
                //    this.KeepConnection = true;
                //    this.ExecuteNonQuery("SET ANSI_PADDING OFF");
                //    this.KeepConnection = bKeep;
                //}
            }
        }

        /// <summary>
        /// Close database connection. Before close database connection, 
        /// it will close other objects used by this class automatically.
        /// </summary>
        public void Close()
        {
            this.bInTransaction = false;
            this.transaction = null;

            if (this.dataReader != null)
            {
                if (!this.dataReader.IsClosed)
                    this.dataReader.Close();
                this.dataReader = null;
            }
            if (this.xmlReader != null)
            {
                this.xmlReader.Close();
                this.xmlReader = null;
            }
            if (this.connection != null)
            {
                if (this.connection.State != ConnectionState.Closed)
                    this.connection.Close();
                this.connection = null;
            }
        }

        /// <summary>
        /// Begins a transaction with specified Isolation Level.
        /// </summary>
        /// <param name="isolationLevel">The specified Isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                //use this line to disable locked transaction.
                isolationLevel = IsolationLevel.ReadCommitted;

                this.Open();
                this.bInTransaction = true;
                if (this.transaction == null)
                    this.transaction = this.connection.BeginTransaction(isolationLevel);
            }
            catch (Exception ex)
            {
                this.transaction = null;
                this.bInTransaction = false;
                if (!this.bKeepConnection)
                    this.Close();
                throw new DataExceptionHandler(ex.Message, ex);
            }
        }

        /// <summary>
        /// Begins a transaction with default Isolation level setting. 
        /// The default Isolation level is IsolationLevel.ReadCommitted.
        /// </summary>
        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (this.transaction != null)
                    this.transaction.Commit();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler(ex.Message, ex);
            }
            finally
            {
                this.transaction = null;
                this.bInTransaction = false;
                if (!this.bKeepConnection)
                    this.Close();
            }
        }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (this.transaction != null)
                    this.transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler(ex.Message, ex);
            }
            finally
            {
                this.transaction = null;
                this.bInTransaction = false;
                if (!this.bKeepConnection)
                    this.Close();
            }
        }

        /// <summary>
        /// Executes a query with specified parameters.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>The number of rows effected.</returns>
        public int ExecuteNonQuery(string command, ArrayList parameters)
        {
            if (QueryCounter >= 0) QueryCounter++;

            int iRowNum = 0;
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = command;
                cmd.CommandTimeout = this.CommandTimeout;
                cmd = GetParameters(cmd, parameters);

                iRowNum = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler("SQL:" + command + "\r\n", ex);
            }
            finally
            {
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            return iRowNum;
        }

        /// <summary>
        /// Executes a query without parameters.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <returns>The number of rows effected.</returns>
        public int ExecuteNonQuery(string command)
        {
            return ExecuteNonQuery(command, null);
        }

        /// <summary>
        /// Execute a query and return the first column of the first row.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>The object represents the first column of the first row.</returns>
        public object ExecuteScalar(string command, ArrayList parameters)
        {
            if (QueryCounter >= 0) QueryCounter++;

            object obj = null;
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = command;
                cmd.CommandTimeout = this.CommandTimeout;
                cmd = GetParameters(cmd, parameters);

                obj = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler("SQL:" + command + "\r\n", ex);
            }
            finally
            {
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            return obj;
        }

        /// <summary>
        /// Execute a query and return the first column of the first row.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <returns>The object represents the first column of the first row.</returns>
        public object ExecuteScalar(string command)
        {
            return ExecuteScalar(command, null);
        }

        /// <summary>
        /// Execute a query and return a <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.
        /// </summary>
        /// <param name="cmd">The <see cref="System.Data.Common.DbCommand">DbCommand</see> object contains query information.</param>
        /// <returns>A <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.</returns>
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            if (QueryCounter >= 0) QueryCounter++;

            try
            {
                if (this.bInTransaction && this.transaction != null)
                    cmd.Transaction = this.transaction;
                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();
                if (this.bKeepConnection || this.bInTransaction)
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                else
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();

                this.dataReader = null;
                throw new DataExceptionHandler(ex.Message, ex);
            }
            return this.dataReader;
        }

        /// <summary>
        /// Execute a query and return a <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>A <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.</returns>
        public DbDataReader ExecuteReader(string command, ArrayList parameters)
        {
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = command;
                cmd.CommandTimeout = this.CommandTimeout;
                cmd = GetParameters(cmd, parameters);
                return ExecuteReader(cmd);
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler("SQL:" + command + "\r\n", ex);
            }
        }

        /// <summary>
        /// Execute a query and return a <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <returns>A <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.</returns>
        public DbDataReader ExecuteReader(string command)
        {
            return ExecuteReader(command, null);
        }

        /// <summary>
        /// Executes a query and return a <see cref="System.Xml.XmlReader">XmlReader</see> object.
        /// </summary>
        /// <param name="command">The query to run.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>A <see cref="System.Xml.XmlReader">XmlReader</see> object.</returns>
        public XmlReader ExecuteXmlReader(string command, ArrayList parameters)
        {
            if (QueryCounter >= 0) QueryCounter++;

            if (this.dbProviderName.Trim() != MSSQL_Provider)
                throw new DataExceptionHandler("Provider " + this.dbProviderName + " is not supported for ExecuteXmlReader() function.");
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = command;
                cmd.CommandTimeout = this.iCommandTimeout;
                cmd = GetParameters(cmd, parameters);

                this.xmlReader = ((SqlCommand)cmd).ExecuteXmlReader();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler("SQL:" + command + "\r\n", ex);
            }
            finally
            {
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            return this.xmlReader;
        }

        /// <summary>
        /// Runs a stored procedure with specified parameters.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>A <see cref="System.Data.Common.DbParameterCollection">DbParameterCollection</see> object contains the output parameters.</returns>
        public DbParameterCollection CallProcedureNonQuery(string procedureName, ArrayList parameters)
        {
            if (QueryCounter >= 0) QueryCounter++;

            DbParameterCollection pars = null;
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.UpdatedRowSource = UpdateRowSource.None;
                cmd.CommandTimeout = this.iCommandTimeout;
                cmd = GetParameters(cmd, parameters);

                cmd.ExecuteNonQuery();
                pars = cmd.Parameters;
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler("Procedure:" + procedureName + "\r\n", ex);
            }
            finally
            {
                if (!this.bInTransaction && !this.KeepConnection)
                    this.Close();
            }
            return pars;
        }

        /// <summary>
        /// Runs a stored procedure without parameters.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <returns>A <see cref="System.Data.Common.DbParameterCollection">DbParameterCollection</see> object contains the output parameters.</returns>
        public DbParameterCollection CallProcedureNonQuery(string procedureName)
        {
            return CallProcedureNonQuery(procedureName, null);
        }

        /// <summary>
        /// Runs a stored procedure and return a <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>A <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object contains the returned data information.</returns>
        public DbDataReader CallProcedureReader(string procedureName, ArrayList parameters)
        {
            if (QueryCounter >= 0) QueryCounter++;

            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
                cmd.CommandTimeout = this.iCommandTimeout;
                cmd = GetParameters(cmd, parameters);

                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();
                if (this.KeepConnection || this.bInTransaction)
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                else
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();

                this.dataReader = null;
                throw new DataExceptionHandler("Procedure:" + procedureName + "\r\n", ex);
            }
            return this.dataReader;
        }

        /// <summary>
        /// Runs a stored procedure and return a <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <returns>A <see cref="System.Data.Common.DbDataReader">DbDataReader</see> object contains the returned data information.</returns>
        public DbDataReader CallProcedureReader(string procedureName)
        {
            return CallProcedureReader(procedureName, null);
        }

        /// <summary>
        /// Runs a stored procedure and return a <see cref="System.Data.DataSet">DataSet</see> object.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <returns>A <see cref="System.Data.DataSet">DataSet</see> object.</returns>
        public DataSet CallProcedureDataSet(string procedureName, ArrayList parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();

                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = procedureName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.UpdatedRowSource = UpdateRowSource.OutputParameters;
                cmd.CommandTimeout = this.iCommandTimeout;
                cmd = GetParameters(cmd, parameters);

                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();
                if (this.KeepConnection || this.bInTransaction)
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                else
                    this.dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ArrayList arr = new ArrayList();
                if (cmd.Parameters != null && cmd.Parameters.Count > 0)
                {
                    foreach (DbParameter par in cmd.Parameters)
                    {
                        if (par.Direction == ParameterDirection.Input)
                            continue;

                        if (this.dbProviderName == Oracle_Provider && ((OracleParameter)par).OracleType == OracleType.Cursor)
                        {
                            DataTable dtCursor = (DataTable)(par.Value);
                            dtCursor.TableName = par.ParameterName;
                            ds.Tables.Add(dtCursor);
                        }
                        else
                            arr.Add(par);
                    }
                }
                if (arr.Count > 0)
                {
                    foreach (DbParameter par in arr)
                    {
                        if (this.dbProviderName == Oracle_Provider)
                        {
                            if (((OracleParameter)par).OracleType == OracleType.Clob || ((OracleParameter)par).OracleType == OracleType.Blob)
                            {
                                dt.Columns.Add(par.ParameterName, typeof(OracleLob));
                            }
                        }
                        else
                        {
                            dt.Columns.Add(par.ParameterName);
                        }
                    }
                    if (arr.Count > 0)
                    {
                        DataRow row = dt.NewRow();
                        foreach (DbParameter par in arr)
                        {
                            row[par.ParameterName] = par.Value;
                        }
                        dt.Rows.Add(row);
                    }
                }
                else
                    dt.Load(this.dataReader);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();

                this.dataReader = null;
                throw new DataExceptionHandler("Procedure:" + procedureName + "\r\n", ex);
            }
            finally
            {
                if (this.dataReader != null && !this.dataReader.IsClosed)
                    this.dataReader.Close();

                if (!this.bInTransaction && !this.KeepConnection)
                    this.Close();
            }
            return ds;
        }

        /// <summary>
        /// Runs a stored procedure and return a <see cref="System.Data.DataSet">DataSet</see> object.
        /// </summary>
        /// <param name="procedureName">The name of stored procedure.</param>
        /// <returns>A <see cref="System.Data.DataSet">DataSet</see> object.</returns>
        public DataSet CallProcedureDataSet(string procedureName)
        {
            return CallProcedureDataSet(procedureName, null);
        }

        /// <summary>
        /// Executes a query and put result into a <see cref="System.Data.DataTable">DataTable</see> object.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="command">The select statement query.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <param name="dataTable">A <see cref="System.Data.DataTable">DataTable</see> object contains result information.</param>
        public void GetDataTable(string tableName, string command, ArrayList parameters, DataTable dataTable)
        {
            if (QueryCounter >= 0) QueryCounter++;

            if (dataTable == null)
                dataTable = new DataTable(tableName);
            if (dataTable.TableName.Trim() == String.Empty)
                dataTable.TableName = tableName;

            DbDataReader reader = null;
            lock (this)
            {
                try
                {
                    this.Open();
                    DbCommand cmd = this.connection.CreateCommand();
                    cmd.CommandText = command;
                    cmd.CommandTimeout = this.CommandTimeout;
                    cmd = GetParameters(cmd, parameters);
                    //DbDataReader reader = cmd.ExecuteReader(CommandBehavior.KeyInfo); //query with keyinfo will cause missing data. I think it is bug.
                    reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    // keeps the DbCommand in Hashtable with tableName as key, this is used for update.
                    if (this.dbCommandHash.Contains(tableName))
                        this.dbCommandHash.Remove(tableName);
                    this.dbCommandHash.Add(tableName, cmd);
                }
                catch (Exception ex)
                {
                    throw new DataExceptionHandler("SQL:" + command + "\r\n", ex);
                }
                finally
                {
                    if (reader != null && !reader.IsClosed) reader.Close();
                    if (!this.bInTransaction && !this.bKeepConnection)
                        this.Close();
                }
            }
        }

        /// <summary>
        /// Executes a query and put result into a <see cref="System.Data.DataSet">DataSet</see> object.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="command">The select statement query.</param>
        /// <param name="parameters">The ArrayList contains <see cref="EAF.Lib.Data.Parameter">Parameter</see> array.</param>
        /// <param name="dataSet">A <see cref="System.Data.DataSet">DataSet</see> object contains result information.</param>
        public void GetDataSet(string tableName, string command, ArrayList parameters, DataSet dataSet)
        {
            if (dataSet == null)
                throw new DataExceptionHandler("DataSet can not be null.");

            tableName = (tableName == null) ? "Table" : tableName;
            string[] sqls = command.Split(DBSpliter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int i = 0;

            try
            {
                if (sqls.Length > 1)
                    this.BeginTransaction();

                foreach (string sql in sqls)
                {
                    string query = sql.Trim();
                    if (query == string.Empty)
                        continue;

                    if (query.StartsWith(SelectClause, StringComparison.OrdinalIgnoreCase))
                    {
                        string tbname = (i == 0) ? tableName : tableName + "i";
                        if (!dataSet.Tables.Contains(tbname))
                            dataSet.Tables.Add(tbname);
                        GetDataTable(tbname, query, parameters, dataSet.Tables[tbname]);
                        i++;
                    }
                    else if (query.StartsWith(UpdateClause, StringComparison.OrdinalIgnoreCase))
                        ExecuteNonQuery(query, parameters);
                    else if (query.StartsWith(DeleteClause, StringComparison.OrdinalIgnoreCase))
                        ExecuteNonQuery(query, parameters);
                    else if (query.StartsWith(InsertClause, StringComparison.OrdinalIgnoreCase))
                        ExecuteNonQuery(query, parameters);
                    else
                        throw new DataExceptionHandler("Can not run query for '" + query + "'");
                }
                if (sqls.Length > 1)
                    this.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (sqls.Length > 1)
                    this.RollbackTransaction();

                //if transaction is deadlocked, then re-run this transaction.
                if (ex.Message.IndexOf("deadlocked") != -1)
                    GetDataSet(tableName, command, parameters, dataSet);
                else
                    throw ex;
            }
        }

        /// <summary>
        /// Executes a query and put result into a <see cref="System.Data.DataTable">DataTable</see> object.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="command">The select statement query.</param>
        /// <param name="dataTable">A <see cref="System.Data.DataTable">DataTable</see> object contains result information.</param>
        public void GetDataTable(string tableName, string command, DataTable dataTable)
        {
            GetDataTable(tableName, command, null, dataTable);
        }

        /// <summary>
        /// Executes a query and put result into a <see cref="System.Data.DataSet">DataSet</see> object.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="command">The select statement query.</param>
        /// <param name="dataSet">A <see cref="System.Data.DataSet">DataSet</see> object contains result information.</param>
        public void GetDataSet(string tableName, string command, DataSet dataSet)
        {
            GetDataSet(tableName, command, null, dataSet);
        }

        /// <summary>
        /// Updates data in <see cref="System.Data.DataTable">DataTable</see> to database.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="dataTable">A <see cref="System.Data.DataTable">DataTable</see> object contains data information.</param>
        /// <returns>The number of rows successfully updated to database.</returns>
        public int UpdateDataTable(string tableName, DataTable dataTable)
        {
            return UpdateDataTableBatch(tableName, dataTable, 1);
        }

        /// <summary>
        /// Updates data in <see cref="System.Data.DataSet">DataSet</see> to database.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="dataSet">A <see cref="System.Data.DataSet">DataSet</see> object contains data information.</param>
        /// <returns>The number of rows successfully updated to database.</returns>
        public int UpdateDataSet(string tableName, DataSet dataSet)
        {
            return UpdateDataTable(tableName, dataSet.Tables[tableName]);
        }

        /// <summary>
        /// Updates batch data in <see cref="System.Data.DataTable">DataTable</see> to database.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <param name="dataTable">A <see cref="System.Data.DataTable">DataTable</see> object contains data information.</param>
        /// <param name="batchSize">If it is "0", it will use largest size that server can handler; if it is "1", 
        /// it will disable batch update; if it is larger than "1", it will send this number rows to batch update.</param>
        /// <returns>The number of rows successfully updated to database.</returns>
        public int UpdateDataTableBatch(string tableName, DataTable dataTable, int batchSize)
        {
            if (UpdateCounter >= 0) UpdateCounter++;

            int rowNum = 0;

            object obj = this.dbCommandHash[tableName];
            if (obj == null)
                throw new DataExceptionHandler("Update failed due to can not find sql statement for '" + tableName + "'.");

            lock (this)
            {
                BeginTransaction(IsolationLevel.Serializable);
                DbCommand cmd = (DbCommand)obj;

                if (this.connection != null)
                    cmd.Connection = this.connection;
                cmd.Transaction = this.transaction;
                cmd.UpdatedRowSource = UpdateRowSource.None;

                DbDataAdapter adapter = GetDataAdapter(cmd);
                adapter.UpdateBatchSize = batchSize;

                try
                {
                    rowNum = adapter.Update(dataTable);
                    CommitTransaction();
                }
                catch (DBConcurrencyException conex)
                {
                    string concurrencyMethod = Properties.Settings.Default.ConcurrencyMethod.ToUpper().Trim();
                    if (concurrencyMethod == Last_In_Win)
                    {
                        // if get concurrency error, then re-get dataset and merge it.
                        DataTable dt = new DataTable(dataTable.TableName);
                        DbDataReader reader = cmd.ExecuteReader();
                        dt.Load(reader);

                        // get primary key info for merging.
                        ArrayList keys = GetTableKeyInfo(dataTable.TableName);
                        DataColumn[] cols = new DataColumn[keys.Count];
                        for (int i = 0; i < keys.Count; i++)
                            cols[i] = dt.Columns[keys[i] + ""];
                        dt.PrimaryKey = cols;

                        //manually merge
                        foreach (DataRow row in dt.Rows)
                        {
                            string select = "";
                            foreach (DataColumn col in dt.PrimaryKey)
                                select += col.ColumnName + "='" + row[col.ColumnName] + "' and ";
                            select = select.Trim();
                            if (select.EndsWith("and"))
                                select = select.Substring(0, select.Length - 3);
                            DataRow[] rows = dataTable.Select(select);
                            if (rows.Length > 0)
                            {
                                foreach (DataColumn col1 in dt.Columns)
                                    row[col1.ColumnName] = rows[0][col1.ColumnName];
                            }
                        }
                        adapter.Update(dt);
                        CommitTransaction();
                    }
                    else if (concurrencyMethod == First_In_Win)
                    {
                        RollbackTransaction();
                    }
                    else
                        throw conex;
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    //if transaction is deadlocked, then re-run this transaction.
                    if (ex.Message.IndexOf("deadlocked") != -1)
                        rowNum = UpdateDataTableBatch(tableName, dataTable, batchSize);
                    else
                        throw new DataExceptionHandler("SQL:" + cmd.CommandText + Environment.NewLine + ex.Message, ex);
                }
                finally
                {
                    if (!this.bInTransaction && !this.bKeepConnection)
                        this.Close();
                }
            }
            return rowNum;
        }

        /// <summary>
        /// Gets database connection.
        /// </summary>
        /// <returns>An opened database connection.</returns>
        public DbConnection GetConnection()
        {
            this.Open();
            return this.connection;
        }

        /// <summary>
        /// Gets database schema information.
        /// </summary>
        /// <param name="collectionName">The name of collectin name, like "Tables", "Views", etc.</param>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetSchema(string collectionName)
        {
            DataTable dataTable = null;
            try
            {
                this.Open();
                dataTable = this.connection.GetSchema(collectionName, null);
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler(ex.Message, ex);
            }
            finally
            {
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Gets the list of tables.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetTableList()
        {
            return GetSchema("Tables");
        }

        /// <summary>
        /// Gets the list of views.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetViewList()
        {
            return GetSchema("Views");
        }

        /// <summary>
        /// Gets the list of procedures.
        /// </summary>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetProcedureList()
        {
            return GetSchema("Procedures");
        }

        /// <summary>
        /// Gets the schema of table, like column name, column size, etc.
        /// </summary>
        /// <param name="tableName">The name of table.</param>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetTableSchema(string tableName)
        {
            string sql = "select * from " + tableName + " where 1=-1";
            return GetTableSchemaBySQL(sql);
        }

        /// <summary>
        /// Gets the schema of sql, like column name, column size, etc.
        /// </summary>
        /// <param name="sql">The sql to execute.</param>
        /// <returns>A <see cref="System.Data.DataTable">DataTable</see> object contains required information.</returns>
        public DataTable GetTableSchemaBySQL(string sql)
        {
            DataTable dataTable = null;
            DbDataReader reader = null;
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandTimeout = this.CommandTimeout;

                reader = cmd.ExecuteReader();
                dataTable = reader.GetSchemaTable();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler(ex.Message, ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Gets a DataTable with empty DataRow. This DataTable can be used to insert new data.
        /// </summary>
        /// <param name="tablename">The table name.</param>
        /// <returns>A DataTable with empty DataRow.</returns>
        public DataTable GetEmptyDataTable(string tablename)
        {
            DataTable dt = new DataTable(tablename);

            string sql = "select * from " + tablename + " where 1=-1";
            GetDataTable(tablename, sql, dt);

            return dt;
        }

        /// <summary>
        /// Gets a primary key info for a specified table.
        /// </summary>
        /// <param name="tablename">The specified table name.</param>
        /// <returns>An ArrayList contains primary key column name information.</returns>
        public ArrayList GetTableKeyInfo(string tablename)
        {
            if (QueryCounter >= 0) QueryCounter++;

            ArrayList arr = new ArrayList();
            ArrayList parameters = new ArrayList();
            DataTable dataTable = new DataTable(tablename);
            string command = "select * from " + tablename + " where 1=-1";

            DbDataReader reader = null;
            try
            {
                this.Open();
                DbCommand cmd = this.connection.CreateCommand();
                cmd.CommandText = command;
                cmd.CommandTimeout = this.CommandTimeout;
                cmd = GetParameters(cmd, parameters);
                reader = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new DataExceptionHandler(ex.Message, ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                if (!this.bInTransaction && !this.bKeepConnection)
                    this.Close();
            }
            foreach (DataColumn col in dataTable.PrimaryKey)
                arr.Add(col.ColumnName);
            return arr;
        }

        /// <summary>
        /// Create a Database table from a DataTable schema.
        /// </summary>
        /// <param name="dt">A DataTable object.</param>
        public void CreateTableFromSchema(DataTable dt)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE " + dt.TableName + " ( " + Environment.NewLine);
            foreach (DataColumn col in dt.Columns)
            {
                sql.Append(col.ColumnName + " ");
                if (this.dbProviderName.Trim() == Oracle_Provider)
                    sql.Append(NetType2OracleType(col.DataType.ToString(), col.MaxLength) + " ");
                else
                    sql.Append(NetType2SqlType(col.DataType.ToString(), col.MaxLength) + " ");
                if (col.AutoIncrement)
                    sql.Append("IDENTITY ");
                sql.Append((col.AllowDBNull ? "" : "NOT ") + "NULL," + Environment.NewLine);
            }
            if (dt.PrimaryKey != null)
            {
                sql.Append("CONSTRAINT PK_" + dt.TableName + " PRIMARY KEY(" + Environment.NewLine);
                foreach (DataColumn col in dt.PrimaryKey)
                {
                    sql.Append(col.ColumnName + "," + Environment.NewLine);
                }
                // remove last comma.
                sql.Remove(sql.Length - (Environment.NewLine.Length + 1), 1);
                sql.Append(")," + Environment.NewLine);
            }
            // remove last comma.
            sql.Remove(sql.Length - (Environment.NewLine.Length + 1), 1);
            sql.Append(")" + Environment.NewLine);

            ExecuteNonQuery(sql.ToString());
        }
        #endregion

        #region Private functions
        private string BuildConnectionString(string connection)
        {
            // if the connection string is encrpted, then decrypt first
            if (connection.Trim().IndexOf(DBSpliter) == -1)
                connection = Decrypting(connection);
            // if the connection string has user app data directory, then replace it.
            if (connection.IndexOf("|UserAppDataDirectory|") != -1)
                connection = connection.Replace("|UserAppDataDirectory|", Application.UserAppDataPath);
            if (connection.IndexOf("|CommonAppDataDirectory|") != -1)
                connection = connection.Replace("|CommonAppDataDirectory|", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\" + Application.CompanyName + @"\" + Application.ProductName + @"\" + Application.ProductVersion);
            if (connection.IndexOf("|DataDirectory|") != -1)
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    connection = connection.Replace("|DataDirectory|", ApplicationDeployment.CurrentDeployment.DataDirectory);
                else
                    connection = connection.Replace("|DataDirectory|", AppDomain.CurrentDomain.BaseDirectory.TrimEnd(@"\".ToCharArray()));
            }
            return connection;
        }

        private DbCommand GetParameters(DbCommand cmd, ArrayList parameters)
        {
            if (this.bInTransaction && this.transaction != null)
                cmd.Transaction = this.transaction;

            if (parameters == null)
                return cmd;

            string sql = cmd.CommandText;
            // replace start with '#' parameter first.
            foreach (Parameter parameter in parameters)
            {
                if (parameter.ParameterName.StartsWith("#"))
                {
                    string name = "@" + parameter.ParameterName.Substring(1);
                    // using DBNull value to handle IS or IS NOT operator.
                    if (cmd.CommandText.IndexOf(name + "@") != -1)
                        cmd.CommandText = cmd.CommandText.Replace(name + "@", ((parameter.Value is DBNull) ? "null" : parameter.Value + ""));
                    else
                        cmd.CommandText = cmd.CommandText.Replace(name, ((parameter.Value is DBNull) ? "null" : parameter.Value + ""));
                }
            }
            foreach (Parameter parameter in parameters)
            {
                // you can not add no use parameters in ParameterCollection.
                int index = -1;
                string query = sql;
                if (cmd.CommandType != CommandType.StoredProcedure)
                {
                    while ((index = query.IndexOf(parameter.ParameterName, StringComparison.OrdinalIgnoreCase)) != -1)
                    {
                        if (query.Length > (index + parameter.ParameterName.Length))
                        {
                            string nextChar = query.Substring(index + parameter.ParameterName.Length, 1);
                            if (nextChar == "'" || nextChar == " " || nextChar == ")" || nextChar == ",")
                                break;
                            else
                                query = query.Substring(index + parameter.ParameterName.Length);
                        }
                        else
                            break;
                    }
                    if (index == -1)
                        continue;
                }
                switch (this.dbProviderName)
                {
                    case ODBC_Provider: cmd.Parameters.Add(parameter.ConvertToOdbcParameter()); break;
                    case OleDB_Provider: cmd.Parameters.Add(parameter.ConvertToOleDbParameter()); break;
                    case MSSQL_Provider: cmd.Parameters.Add(parameter.ConvertToSqlParameter()); break;
                    case Oracle_Provider:
                        cmd.Parameters.Add(parameter.ConvertToOracleParameter());
                        cmd.CommandText = cmd.CommandText.Replace("@", ":");  // convert to Oracle syntax.
                        break;
                    default:
                        throw (new DataExceptionHandler("Provider " + this.dbProviderName + " is not supported."));
                }
            }
            return cmd;
        }

        private DbDataAdapter GetDataAdapter(DbCommand cmd)
        {
            DbDataAdapter adapter = null;
            // gets DbDataAdapter based on provide name
            switch (this.dbProviderName)
            {
                case ODBC_Provider:
                    adapter = new OdbcDataAdapter((OdbcCommand)cmd);
                    OdbcCommandBuilder odbcCmdBuilder = new OdbcCommandBuilder((OdbcDataAdapter)adapter);
                    break;
                case OleDB_Provider:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    OleDbCommandBuilder oleDbCmdBuilder = new OleDbCommandBuilder((OleDbDataAdapter)adapter);
                    break;
                case MSSQL_Provider:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder((SqlDataAdapter)adapter);
                    break;
                case Oracle_Provider:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    OracleCommandBuilder oracleCmdBuilder = new OracleCommandBuilder((OracleDataAdapter)adapter);
                    break;
                default:
                    throw (new DataExceptionHandler("Provider " + this.dbProviderName + " is not supported."));
            }
            // sets transaction if needed.
            if (this.bInTransaction && this.transaction != null)
            {
                if (adapter.DeleteCommand != null)
                    adapter.DeleteCommand.Transaction = this.transaction;
                if (adapter.InsertCommand != null)
                    adapter.InsertCommand.Transaction = this.transaction;
                if (adapter.SelectCommand != null)
                    adapter.SelectCommand.Transaction = this.transaction;
                if (adapter.UpdateCommand != null)
                    adapter.UpdateCommand.Transaction = this.transaction;
            }
            return adapter;
        }

        /// <summary>
        /// Converts the .NET DataType to SQL-Server DataType.
        /// </summary>
        /// <param name="netType">The .NET DataType.</param>
        /// <param name="maxLength">The maximum length for specified datatype.</param>
        /// <returns>A SQL-Server DataType.</returns>
        private string NetType2SqlType(string netType, int maxLength)
        {
            string sqlType = "";

            switch (netType)
            {
                case "System.Boolean": sqlType = "bit"; break;
                case "System.Byte": sqlType = "tinyint"; break;
                case "System.Int16": sqlType = "smallint"; break;
                case "System.Int32": sqlType = "int"; break;
                case "System.Int64": sqlType = "bigint"; break;
                case "System.Byte[]": sqlType = "image"; break;
                case "System.Char[]": sqlType = "nchar(" + maxLength + ")"; break;
                case "System.String": sqlType = (maxLength == 0x7FFFFFFF) ? "ntext" : "nvarchar(" + maxLength + ")"; break;
                case "System.Single": sqlType = "real"; break;
                case "System.Double": sqlType = "float"; break;
                case "System.Decimal": sqlType = "decimal(22,4)"; break;
                case "System.DateTime": sqlType = "datetime"; break;
                case "System.Guid": sqlType = "uniqueidentifier"; break;
                case "System.Object": sqlType = "sql_variant"; break;
                default:
                    throw new DataExceptionHandler("Type '" + netType + "' is not supported.");
            }
            return sqlType;
        }

        /// <summary>
        /// Converts .NET DataType to oracle DataType.
        /// </summary>
        /// <param name="netType">The .NET DataType.</param>
        /// <param name="maxLength">The maximum length for specified datatype.</param>
        /// <returns>A oracle DataType.</returns>
        private string NetType2OracleType(string netType, int maxLength)
        {
            string sqlType = "";

            switch (netType)
            {
                case "System.Boolean": sqlType = "CHAR"; break;
                case "System.Byte": sqlType = "NUMBER(3)"; break;
                case "System.Int16": sqlType = "NUMBER(10)"; break;
                case "System.Int32": sqlType = "NUMBER(22)"; break;
                case "System.Int64": sqlType = "NUMBER(30)"; break;
                case "System.Byte[]": sqlType = "BLOB"; break;
                case "System.Char[]": sqlType = "NCHAR(" + maxLength + " byte)"; break;
                case "System.String": sqlType = (maxLength == 0x7FFFFFFF) ? "CLOB" : "NVARCHAR2(" + maxLength + " byte)"; break;
                case "System.Single": sqlType = "NUMBER(10,4)"; break;
                case "System.Double": sqlType = "NUMBER(18,4)"; break;
                case "System.Decimal": sqlType = "NUMBER(18,4)"; break;
                case "System.DateTime": sqlType = "DATE"; break;
                case "System.Guid": sqlType = "NVARCHAR2(100)"; break;
                default:
                    throw new DataExceptionHandler("Type '" + netType + "' is not supported.");
            }
            return sqlType;
        }
        #endregion

        #region Security
        /// <summary>
        /// Encrypts a source with the given security key.
        /// </summary>
        /// <param name="Source">A string which need to be encrypted.</param>
        /// <returns>The encrypting string.</returns>
        public static string Encrypting(string Source)
        {
            SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
            if ((Source == null) || (key == null) || (Source.Length == 0) || (key.Length == 0))
                return "";

            byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
            // create a MemoryStream so that the process can be done without I/O files
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            byte[] bytKey = GetLegalKey(mobjCryptoService, key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create an Encryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

            // create Crypto Stream that transforms a stream using the encryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            // write out encrypted content into MemoryStream
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            // convert into Base64 so that the result can be used in xml
            return System.Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        /// <summary>
        /// Decrypts a source with the given security key.
        /// </summary>
        /// <param name="Source">An encrypting string which need to be decrypted.</param>
        /// <returns>A decrypting string.</returns>
        public static string Decrypting(string Source)
        {
            SymmetricAlgorithm mobjCryptoService = new RijndaelManaged();
            if ((Source == null) || (key == null) || (Source.Length == 0) || (key.Length == 0))
                return "";


            // convert from Base64 to binary
            byte[] bytIn = Convert.FromBase64String(Source);
            // create a MemoryStream with the input
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);

            byte[] bytKey = GetLegalKey(mobjCryptoService, key);

            // set the private key
            mobjCryptoService.Key = bytKey;
            mobjCryptoService.IV = bytKey;

            // create a Decryptor from the Provider Service instance
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

            // create Crypto Stream that transforms a stream using the decryption
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

            // read out the result from the Crypto Stream
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        private static byte[] GetLegalKey(SymmetricAlgorithm mobjCryptoService, string Key)
        {
            string sTemp;

            if (mobjCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits
                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;

            // convert the secret key to byte array
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        #endregion
    }
}
