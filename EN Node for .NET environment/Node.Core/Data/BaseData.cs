using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Node;
using Node.Lib.Data;

namespace Node.Core.Data
{
    /// <summary>
    /// The base class of data access layer.
    /// </summary>
    public class BaseData
    {
        #region Table Names
        /// <summary>
        /// NODE_ACCOUNT_TYPE
        /// </summary>
        protected string TblAccType = "NODE_ACCOUNT_TYPE";
        /// <summary>
        /// NODE_ACCOUNT_TYPE_XREF
        /// </summary>
        protected string TblAccTypeXREF = "NODE_ACCOUNT_TYPE_XREF";
        /// <summary>
        /// NODE_DOMAIN
        /// </summary>
        protected string TblDomain = "NODE_DOMAIN";
        /// <summary>
        /// NODE_DOMAIN_WEB_SERVICE_XREF
        /// </summary>
        protected string TblDomainWS = "NODE_DOMAIN_WEB_SERVICE_XREF";
        /// <summary>
        /// NODE_FILE_CABIN
        /// </summary>
        protected string TblFileCabin = "NODE_FILE_CABIN";
        /// <summary>
        /// NODE_OPERATION
        /// </summary>
        protected string TblOperation = "NODE_OPERATION";
        /// <summary>
        /// NODE_OPERATION_LOG
        /// </summary>
        protected string TblOperationLog = "NODE_OPERATION_LOG";
        /// <summary>
        /// NODE_OPERATION_LOG_PARAMETER
        /// </summary>
        protected string TblOperationLogParam = "NODE_OPERATION_LOG_PARAMETER";
        /// <summary>
        /// NODE_OPERATION_LOG_STATUS
        /// </summary>
        protected string TblOperationLogStatus = "NODE_OPERATION_LOG_STATUS";
        /// <summary>
        /// NODE_TASK_CONFIG
        /// </summary>
        protected string TblTaskConfig = "NODE_TASK_CONFIG";
        /// <summary>
        /// NODE_USER_OPERATION_XREF
        /// </summary>
        protected string TblUserOp = "NODE_USER_OPERATION_XREF";
        /// <summary>
        /// NODE_WEB_SERVICE
        /// </summary>
        protected string TblWebService = "NODE_WEB_SERVICE";
        /// <summary>
        /// SYS_ADDRESS
        /// </summary>
        protected string TblAddress = "SYS_ADDRESS";
        /// <summary>
        /// SYS_CONFIG
        /// </summary>
        protected string TblConfig = "SYS_CONFIG";
        /// <summary>
        /// SYS_EMAIL
        /// </summary>
        protected string TblEmail = "SYS_EMAIL";
        /// <summary>
        /// SYS_SEQUENCE_NO
        /// </summary>
        protected string TblSequence = "SYS_SEQUENCE_NO";
        /// <summary>
        /// SYS_USER_ADDRESS
        /// </summary>
        protected string TblUserAddress = "SYS_USER_ADDRESS";
        /// <summary>
        /// SYS_USER_EMAIL
        /// </summary>
        protected string TblUserEmail = "SYS_USER_EMAIL";
        /// <summary>
        /// SYS_USER_INFO
        /// </summary>
        protected string TblUser = "SYS_USER_INFO";

        #endregion

        #region Column Names

        /***** Common Names *****/
        /// <summary>
        /// TRANS_ID
        /// </summary>
        protected string TransID = "TRANS_ID";
        /// <summary>
        /// STATUS_CD
        /// </summary>
        protected string Status = "STATUS_CD";
        /// <summary>
        /// MESSAGE
        /// </summary>
        protected string Message = "MESSAGE";
        /// <summary>
        /// CREATED_DTTM
        /// </summary>
        protected string CreatedDate = "CREATED_DTTM";
        /// <summary>
        /// CREATED_BY
        /// </summary>
        protected string CreatedBy = "CREATED_BY";
        /// <summary>
        /// UPDATED_DTTM
        /// </summary>
        protected string UpdatedDate = "UPDATED_DTTM";
        /// <summary>
        /// UPDATED_DTTM
        /// </summary>
        protected string UpdatedBy = "UPDATED_BY";

        /***** NODE_ACCOUNT_TYPE *****/
        /// <summary>
        /// ACCOUNT_TYPE_ID
        /// </summary>
        protected string AccTypeID = "ACCOUNT_TYPE_ID";
        /// <summary>
        /// ACCOUNT_TYPE
        /// </summary>
        protected string AccType = "ACCOUNT_TYPE";
        /// <summary>
        /// ACCOUNT_DESC
        /// </summary>
        protected string AccTypeDesc = "ACCOUNT_DESC";

        /***** NODE_ACCOUNT_TYPE_XREF *****/
        /// <summary>
        /// ACCOUNT_TYPE_XREF_ID
        /// </summary>
        protected string AccTypeXREFID = "ACCOUNT_TYPE_XREF_ID";

        /***** NODE_DOMAIN *****/
        /// <summary>
        /// DOMAIN_ID
        /// </summary>
        protected string DomID = "DOMAIN_ID";
        /// <summary>
        /// DOMAIN_NAME
        /// </summary>
        protected string DomName = "DOMAIN_NAME";
        /// <summary>
        /// DOMAIN_STATUS_CD
        /// </summary>
        protected string DomStatus = "DOMAIN_STATUS_CD";
        /// <summary>
        /// DOMAIN_STATUS_MSG
        /// </summary>
        protected string DomMessage = "DOMAIN_STATUS_MSG";
        /// <summary>
        /// DOMAIN_DESC
        /// </summary>
        protected string DomDesc = "DOMAIN_DESC";

        /***** NODE_FILE_CABIN *****/
        /// <summary>
        /// FILE_ID
        /// </summary>
        protected string FileID = "FILE_ID";
        /// <summary>
        /// FILE_NAME
        /// </summary>
        protected string FileName = "FILE_NAME";
        /// <summary>
        /// FILE_TYPE
        /// </summary>
        protected string FileType = "FILE_TYPE";
        /// <summary>
        /// DATAFLOW_NAME
        /// </summary>
        protected string DataFlowName = "DATAFLOW_NAME";
        /// <summary>
        /// SUBMIT_URL
        /// </summary>
        protected string SubmitURL = "SUBMIT_URL";
        /// <summary>
        /// SUBMIT_TOKEN
        /// </summary>
        protected string SubmitToken = "SUBMIT_TOKEN";
        /// <summary>
        /// SUBMIT_DTTM
        /// </summary>
        protected string SubmitDate = "SUBMIT_DTTM";
        /// <summary>
        /// FILE_CONTENT
        /// </summary>
        protected string Content = "FILE_CONTENT";
        /// <summary>
        /// FILE_SIZE
        /// </summary>
        protected string Size = "FILE_SIZE";
        /// <summary>
        /// DOCUMENT_ID
        /// </summary>
        protected string DocID = "DOCUMENT_ID";

        /***** NODE_OPERATION *****/
        /// <summary>
        /// OPERATION_ID
        /// </summary>
        protected string OpID = "OPERATION_ID";
        /// <summary>
        /// OPERATION_NAME
        /// </summary>
        protected string OpName = "OPERATION_NAME";
        /// <summary>
        /// OPERATION_DESC
        /// </summary>
        protected string OpDesc = "OPERATION_DESC";
        /// <summary>
        /// OPERATION_TYPE
        /// </summary>
        protected string OpType = "OPERATION_TYPE";
        /// <summary>
        /// OPERATION_CONFIG
        /// </summary>
        protected string OpConfig = "OPERATION_CONFIG";
        /// <summary>
        /// OPERATION_STATUS_CD
        /// </summary>
        protected string OpStatus = "OPERATION_STATUS_CD";
        /// <summary>
        /// OPERATION_STATUS_MSG
        /// </summary>
        protected string OpMessage = "OPERATION_STATUS_MSG";
        /// <summary>
        /// VERSION_NO
        /// </summary>
        protected string OpVersionNo = "VERSION_NO";
        /// <summary>
        /// PUBLISH_IND
        /// </summary>
        protected string OpPublishInd = "PUBLISH_IND";
        /// <summary>
        /// REST_IND
        /// </summary>
        protected string OpRESTInd = "REST_IND";

        /***** NODE_OPERATION_LOG *****/
        /// <summary>
        /// OPERATION_LOG_ID
        /// </summary>
        protected string OpLogID = "OPERATION_LOG_ID";
        /// <summary>
        /// USER_NAME
        /// </summary>
        protected string UserName = "USER_NAME";
        /// <summary>
        /// REQUESTOR_IP
        /// </summary>
        protected string ReqIP = "REQUESTOR_IP";
        /// <summary>
        /// SUPPLIED_TRANS_ID
        /// </summary>
        protected string SupplTransID = "SUPPLIED_TRANS_ID";
        /// <summary>
        /// TOKEN
        /// </summary>
        protected string Token = "TOKEN";
        /// <summary>
        /// NODE_ADDRESS
        /// </summary>
        protected string NodeAddr = "NODE_ADDRESS";
        /// <summary>
        /// RETURN_URL
        /// </summary>
        protected string RetURL = "RETURN_URL";
        /// <summary>
        /// SERVICE_TYPE
        /// </summary>
        protected string ServiceType = "SERVICE_TYPE";
        /// <summary>
        /// START_DTTM
        /// </summary>
        protected string StartDate = "START_DTTM";
        /// <summary>
        /// END_DTTM
        /// </summary>
        protected string EndDate = "END_DTTM";
        /// <summary>
        /// HOST_NAME
        /// </summary>
        protected string HostName = "HOST_NAME";

        /***** NODE_OPERATION_LOG_PARAMETER *****/
        /// <summary>
        /// PARAMETER_NAME
        /// </summary>
        protected string ParamName = "PARAMETER_NAME";
        /// <summary>
        /// PARAMETER_VALUE
        /// </summary>
        protected string ParamValue = "PARAMETER_VALUE";

        /***** NODE_OPERAITON_LOG_STATUS *****/
        /// <summary>
        /// OPERATION_LOG_STATUS_ID
        /// </summary>
        protected string OpLogStatusID = "OPERATION_LOG_STATUS_ID";
        /// <summary>
        /// MESSAGE
        /// </summary>
        protected string OpLogStatusMsg = "MESSAGE";

        /***** NODE_WEB_SERVICE *****/
        /// <summary>
        /// WEB_SERVICE_ID
        /// </summary>
        protected string WSID = "WEB_SERVICE_ID";
        /// <summary>
        /// WEB_SERVICE_NAME
        /// </summary>
        protected string WSName = "WEB_SERVICE_NAME";

        /***** SYS_ADDRESS *****/
        /// <summary>
        /// ADDRESS_ID
        /// </summary>
        protected string AddressID = "ADDRESS_ID";
        /// <summary>
        /// ADDRESS
        /// </summary>
        protected string Address = "ADDRESS";
        /// <summary>
        /// SUPPLEMENTAL_ADDRESS
        /// </summary>
        protected string SuppAddress = "SUPPLEMENTAL_ADDRESS";
        /// <summary>
        /// LOCALITY_NAME
        /// </summary>
        protected string LocalityName = "LOCALITY_NAME";
        /// <summary>
        /// STATE_CD
        /// </summary>
        protected string StateCD = "STATE_CD";
        /// <summary>
        /// ZIP_CODE
        /// </summary>
        protected string ZipCode = "ZIP_CODE";
        /// <summary>
        /// COUNTRY_CD
        /// </summary>
        protected string CountryCD = "COUNTRY_CD";
        /// <summary>
        /// ADDRESS_DESC
        /// </summary>
        protected string AddrDesc = "ADDRESS_DESC";

        /***** SYS_CONFIG *****/
        /// <summary>
        /// CONFIG_ID
        /// </summary>
        protected string ConfigID = "CONFIG_ID";
        /// <summary>
        /// CONFIG_NAME
        /// </summary>
        protected string ConfigName = "CONFIG_NAME";
        /// <summary>
        /// CONFIG_XML
        /// </summary>
        protected string ConfigXml = "CONFIG_XML";
        /// <summary>
        /// CONFIG_TYPE_CD
        /// </summary>
        protected string ConfigType = "CONFIG_TYPE_CD";

        /***** SYS_EMAIL *****/
        /// <summary>
        /// EMAIL_ID
        /// </summary>
        protected string EmailID = "EMAIL_ID";
        /// <summary>
        /// EMAIL_ADDRESS
        /// </summary>
        protected string Email = "EMAIL_ADDRESS";
        /// <summary>
        /// EMAIL_TYPE
        /// </summary>
        protected string EmailType = "EMAIL_TYPE";

        /***** SYS_SEQUENCE_NO *****/
        /// <summary>
        /// SEQUENCE_ID
        /// </summary>
        protected string SeqID = "SEQUENCE_ID";
        /// <summary>
        /// TABLE_NAME
        /// </summary>
        protected string TableName = "TABLE_NAME";
        /// <summary>
        /// COLUMN_NAME
        /// </summary>
        protected string ColName = "COLUMN_NAME";
        /// <summary>
        /// LAST_USED_NUMBER
        /// </summary>
        protected string LastUsedNo = "LAST_USED_NUMBER";

        /***** SYS_USER_INFO *****/
        /// <summary>
        /// USER_ID
        /// </summary>
        protected string UserID = "USER_ID";
        /// <summary>
        /// LOGIN_NAME
        /// </summary>
        protected string LoginName = "LOGIN_NAME";
        /// <summary>
        /// LOGIN_PASSWORD
        /// </summary>
        protected string LoginPWD = "LOGIN_PASSWORD";
        /// <summary>
        /// USER_STATUS_CD
        /// </summary>
        protected string UserStatus = "USER_STATUS_CD";
        /// <summary>
        /// FIRST_NAME
        /// </summary>
        protected string FirstName = "FIRST_NAME";
        /// <summary>
        /// MIDDLE_INITIAL
        /// </summary>
        protected string MidInitial = "MIDDLE_INITIAL";
        /// <summary>
        /// LAST_NAME
        /// </summary>
        protected string LastName = "LAST_NAME";
        /// <summary>
        /// LAST_4_SSN
        /// </summary>
        protected string Last4SSN = "LAST_4_SSN";
        /// <summary>
        /// CHANGE_PWD_FLAG
        /// </summary>
        protected string ChangePWD = "CHANGE_PWD_FLAG";
        /// <summary>
        /// PHONE_NUMBER
        /// </summary>
        protected string Phone = "PHONE_NUMBER";
        /// <summary>
        /// COMMENTS
        /// </summary>
        protected string Comments = "COMMENTS";

        #endregion
        /// <summary>
        /// Constructor of BaseData;
        /// </summary>
        public BaseData()
        {
        }
        /// <summary>
        /// Get Node Database DBAdapter.
        /// </summary>
        /// <returns>DBAdapter for Database Access</returns>
        protected DBAdapter GetNodeDB()
        {
            DBAdapter dbAdapter = new DBAdapter("node");
            if (dbAdapter != null && !string.IsNullOrEmpty(dbAdapter.ConnectionString))
                return dbAdapter;
            else
            {
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                {
                    if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper() == Phrase.VERSION_11)
                        dbAdapter = new DBAdapter("node_v11");
                    else
                        dbAdapter = new DBAdapter("node_v20");

                    return dbAdapter;
                }
                else
                {
                    dbAdapter = new DBAdapter("node_v11");
                    if (dbAdapter != null && !string.IsNullOrEmpty(dbAdapter.ConnectionString))
                        return dbAdapter;
                    else
                    {
                        dbAdapter = new DBAdapter("node_v20");
                        if (dbAdapter != null && !string.IsNullOrEmpty(dbAdapter.ConnectionString))
                            return dbAdapter;
                        else
                            throw new DataExceptionHandler("Can not find connection setting for 'node', 'node_v11' and/or 'node_v20'.");
                    }
                }
            }
            //return new DBAdapter("node");
        }
        /// <summary>
        /// Database Table Key generator.
        /// </summary>
        /// <param name="table">Name of Table.</param>
        /// <param name="column">Name of Key Column.</param>
        /// <returns>key for the insert record.</returns>
        protected int GetSequenceNumber(string table, string column)
        {
            int seqNum = -1;
            DBAdapter db = null;
            bool isSuccessful = false;
            int count = 0;
            Exception lastEx = null;
            while (!isSuccessful)
            {
                if (count > 10)
                    break;
                count++;
                try
                {
                    db = this.GetNodeDB();
                    //db.BeginTransaction();
                    string command = "select " + this.SeqID + ", " + this.LastUsedNo;
                    command += " from " + this.TblSequence;
                    command += " where " + this.TableName + " = @" + this.TableName;
                    command += " and " + this.ColName + " = @" + this.ColName;
                    ArrayList parameters = new ArrayList();
                    parameters.Add(new Parameter(this.TableName, table));
                    parameters.Add(new Parameter(this.ColName, column));
                    DataTable dt = new DataTable();
                    db.GetDataTable(this.TblSequence, command, parameters, dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int lastUsed = int.Parse("" + dt.Rows[0][this.LastUsedNo]);
                        lastUsed++;
                        dt.Rows[0][this.LastUsedNo] = lastUsed;
                        if (db.UpdateDataTable(this.TblSequence, dt) == 1)
                            seqNum = lastUsed;
                    }
                }
                catch (Exception e)
                {
                    if (db != null)
                    {
                        //db.RollbackTransaction();
                        db = null;
                    }
                    lastEx = e;
                }
                finally
                {
                    //if (db != null)
                    //{
                        //try
                        //{
                            //db.CommitTransaction();
                    if (seqNum >= 0)
                    {
                        isSuccessful = true;
                        db = null;
                    }
                        //}
                        //catch (Exception ex)
                        //{
                        //}
                    //}
                }
            }
            if (seqNum < 0)
                throw new Exception("Could Not Get Sequence Number", lastEx);
            return seqNum;
        }
        /// <summary>
        /// Checking if NODE_OPERATION table contain Oracle database type XMLTYPE.
        /// </summary>
        /// <param name="db">DBAdapter</param>
        /// <returns>True if XMLTYPE is used.</returns>
        protected bool IsXMLTYPE(DBAdapter db)
        {
            bool bFlag = false;

            try
            {
                if (db.ProviderName == Node.Lib.Data.DBAdapter.Oracle_Provider)
                {
                    string sSql = "select data_type from user_tab_columns where table_name = 'NODE_OPERATION' and column_name='OPERATION_CONFIG'";
                    DataTable dt = new DataTable();
                    db.GetDataTable("user_tab_columns", sSql, dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["data_type"].ToString().ToUpper() == "XMLTYPE")
                            bFlag = true;
                    }
                }
            }
            catch (System.Exception )
            {
                bFlag = false;
            }
            return bFlag;
        }
    }
}
