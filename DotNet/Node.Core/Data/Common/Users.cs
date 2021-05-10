using Node.Lib.Security;
using System;
using System.Collections;
using System.Data;

using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// The Interface Class for retrieving User Information stored in Node Application.
    /// </summary>
    public class Users : BaseData, IUsers
    {
        
        /// <summary>
        /// Locally Authenticate a Local Node User
        /// </summary>
        /// <param name="userName">User Name of Node User</param>
        /// <param name="credential">Encrypted Password of Node User</param>
        /// <returns>
        ///     <list type="return"> 0 - Successful Authentication</list>
        ///     <list type="return">-1 - Unknown User</list>
        ///     <list type="return">-2 - Incorrect Password</list>
        ///     <list type="return">-3 - Inactive User</list>
        ///     <list type="return">-4 - Invalid Permission</list>
        /// </returns>
        public int LocalAuthenticate(string userName, string credential)
        {
            int retVal = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select distinct A." + this.LoginName + ", A." + this.LoginPWD;
                command += ", A." + this.UserStatus + ", B." + this.AccType;
                command += " from " + this.TblUser + " A, " + this.TblAccType + " B";
                command += ", " + this.TblAccTypeXREF + " C";
                command += " where A." + this.LoginName + " = @" + this.LoginName;
                command += " and A." + this.UserID + " = C." + this.UserID;
                command += " and C." + this.AccTypeID + " = B." + this.AccTypeID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, userName));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblUser, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (credential != null && credential.Equals("" + row[this.LoginPWD]))
                    {
                        string status = "" + row[this.UserStatus];
                        if (status != null && status.ToUpper().Equals("A"))
                        {
                            string accType = "" + row[this.AccType];
                            if (accType != null && accType.ToUpper().Equals(Phrase.LOCAL_NODE_USER))
                                retVal = 0;
                            else
                                retVal = -4;
                        }
                        else
                            retVal = -3;
                    }
                    else
                        retVal = -2;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retVal;
        }
        
        /// <summary>
        /// Authorize a Local Token
        /// </summary>
        /// <param name="token">Security Token</param>
        /// <param name="opName">Operation (DataFlow or Request) Name</param>
        /// <param name="wsName">Web Service Name</param>
        /// <returns></returns>
        public string LocalAuthorize(string token, string opName, string wsName)
        {
            if (token == null || opName == null || wsName == null)
                return null;
            string userName = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.LoginName;
                command += " from " + this.TblUser + " A, " + this.TblOperation + " B";
                command += ", " + this.TblWebService + " C, " + this.TblUserOp + " D";
                command += " where upper(B." + this.OpName + ") = @" + this.OpName;
                command += " and B." + this.WSID + " = C." + this.WSID;
                command += " and C." + this.WSName + " = @" + this.WSName;
                command += " and B." + this.OpID + " = D." + this.OpID;
                command += " and D." + this.UserID + " = A." + this.UserID;
                command += " and D." + this.UserID + " = (";
                command += "select distinct E." + this.UserID;
                command += " from " + this.TblUser + " E, " + this.TblOperationLog + " F";
                command += ", " + this.TblOperation + " G, " + this.TblWebService + " H";
                command += ", " + this.TblAccType + " I, " + this.TblAccTypeXREF + " J";
                command += " where F." + this.Token + " = @" + this.Token;
                command += " and F." + this.UserName + " = E." + this.LoginName;
                command += " and E." + this.UserID + " = J." + this.UserID;
                command += " and J." + this.AccTypeID + " = I." + this.AccTypeID;
                command += " and I." + this.AccType + " = '" + Phrase.LOCAL_NODE_USER + "'";
                command += " and F." + this.OpID + " = G." + this.OpID;
                command += " and G." + this.WSID + " = H." + this.WSID;
                command += " and H." + this.WSName + " = '" + Phrase.WEB_SERVICE_AUTHENTICATE + "'";
                command += ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.Token, token));
                parameters.Add(new Parameter(this.OpName, opName.ToUpper()));
                parameters.Add(new Parameter(this.WSName, wsName));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblUser, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                    userName = "" + dt.Rows[0][this.LoginName];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return userName;
        }
        
        /// <summary>
        /// Get User Information from Database
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="type">Type, one of User.CONSOLE_USER, User.NODE_USER</param>
        /// <returns>Node.Core.Biz.Objects.User</returns>
        public User GetUser(string userName, int type)
        {
            if (type != User.CONSOLE_USER && type != User.LOCAL_NODE_USER && type != User.NODE_USER)
            {
                NAASUser naasUser = new NAASUser(userName, false);
                return naasUser;
            }
            User retUser = null;
            DBAdapter db = null;
            try
            {
                string sql = "select A." + this.UserID + ", A." + this.LastName + ", A." + this.FirstName;
                sql += ", A." + this.MidInitial + ", A." + this.LoginName + ", A." + this.LoginPWD;
                sql += ", A." + this.UserStatus + ", A." + this.Last4SSN + ", A." + this.ChangePWD;
                sql += ", A." + this.Phone + ", A." + this.Comments + ", A." + this.CreatedDate;
                sql += ", A." + this.CreatedBy + ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                sql += ", C." + this.AddressID + ", C." + this.Address + ", C." + this.SuppAddress;
                sql += ", C." + this.LocalityName + ", C." + this.StateCD + ", C." + this.ZipCode;
                sql += ", C." + this.CountryCD + ", C." + this.Status + ", C." + this.AddrDesc;
                sql += ", E." + this.EmailID + ", E." + this.Email + ", G." + this.AccType;
                if (type == User.CONSOLE_USER)
                    sql += ", H." + this.DomID + ", H." + this.DomName;
                else if (type == User.LOCAL_NODE_USER)
                    sql += ", J." + this.OpID;
                sql += " from " + this.TblUser + " A";
                sql += " left join " + this.TblUserAddress + " B on A." + this.UserID + " = B." + this.UserID;
                sql += " left join " + this.TblAddress + " C on B." + this.AddressID + " = C." + this.AddressID;
                sql += " left join " + this.TblUserEmail + " D on A." + this.UserID + " = D." + this.UserID;
                sql += " left join " + this.TblEmail + " E on D." + this.EmailID + " = E." + this.EmailID;
                if (type == User.LOCAL_NODE_USER)
                {
                    sql += " left join " + this.TblUserOp + " I on A." + this.UserID + " = I." + this.UserID;
                    sql += " left join " + this.TblOperation + " J on I." + this.OpID + " = J." + this.OpID;
                }
                sql += ", " + this.TblAccTypeXREF + " F";
                if (type == User.CONSOLE_USER)
                    sql += " left join " + this.TblDomain + " H on F." + this.DomID + " = H." + this.DomID;
                sql += ", " + this.TblAccType + " G";
                sql += " where A." + this.LoginName + " = @" + this.LoginName;
                sql += " and A." + this.UserID + " = F." + this.UserID;
                sql += " and F." + this.AccTypeID + " = G." + this.AccTypeID;
                sql += " and G." + this.AccType + " = '";
                switch (type)
                {
                    case User.CONSOLE_USER:
                        sql += Phrase.CONSOLE_USER;
                        break;
                    case User.LOCAL_NODE_USER:
                        sql += Phrase.LOCAL_NODE_USER;
                        break;
                    case User.NODE_USER:
                        sql += Phrase.LOCAL_NODE_USER;
                        break;
                }
                sql += "'";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.LoginName, userName));
                db = this.GetNodeDB();
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblUser, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblUser))
                {
                    DataTable dt = ds.Tables[this.TblUser];
                    if (dt.Rows.Count > 0)
                    {
                        int rowIndex = -1;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][this.UserStatus].ToString().Equals("A"))
                            {
                                rowIndex = i;
                                break;
                            }
                        }
                        DataRow dr = dt.Rows[0];
                        if (rowIndex >= 0)
                            dr = dt.Rows[rowIndex];
                        if (dr[this.AccType].ToString().Equals(Phrase.CONSOLE_USER))
                            retUser = new ConsoleUser();
                        else
                        {
                            retUser = new LocalUser();
                            retUser.UserType = User.LOCAL_NODE_USER;
                        }
                        this.UserName = dr[this.LoginName].ToString();
                        object obj = dr[this.UserID];
                        retUser.UserID = int.Parse(obj.ToString());
                        obj = dr[this.FirstName];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.FirstName = obj.ToString();
                        obj = dr[this.MidInitial];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.MiddleInitial = obj.ToString();
                        obj = dr[this.LastName].ToString();
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.LastName = obj.ToString();
                        obj = dr[this.LoginPWD];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.EncryptedPassword = obj.ToString();
                        obj = dr[this.UserStatus];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.Status = obj.ToString();
                        obj = dr[this.Last4SSN];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.Last4SSN = obj.ToString();
                        obj = dr[this.ChangePWD];
                        retUser.ChangePWDFlag = obj != null && !obj.Equals(DBNull.Value) && obj.ToString().Equals("Y");
                        obj = dr[this.Phone];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.PhoneNumber = obj.ToString();
                        obj = dr[this.Comments];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.Comments = obj.ToString();
                        obj = dr[this.AddressID];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.AddressID = int.Parse(obj.ToString());
                        obj = dr[this.Address];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.Address = obj.ToString();
                        obj = dr[this.SuppAddress];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.SupplementalAddress = obj.ToString();
                        obj = dr[this.LocalityName];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.LocalityName = obj.ToString();
                        obj = dr[this.StateCD];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.StateUSPSCode = obj.ToString();
                        obj = dr[this.ZipCode];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.ZipCode = obj.ToString();
                        obj = dr[this.CountryCD];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.CountryCode = obj.ToString();
                        obj = dr[this.AddrDesc];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.AddressDescription = obj.ToString();
                        obj = dr[this.EmailID];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.EmailID = int.Parse(obj.ToString());
                        obj = dr[this.Email];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.EmailAddress = obj.ToString();
                        obj = dr[this.CreatedDate];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.CreatedDate = (DateTime)obj;
                        obj = dr[this.CreatedBy];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.CreatedBy = obj.ToString();
                        obj = dr[this.UpdatedDate];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.UpdatedDate = (DateTime)obj;
                        obj = dr[this.UpdatedBy];
                        if (obj != null && !obj.Equals(DBNull.Value))
                            retUser.UpdatedBy = obj.ToString();
                        if (type == User.CONSOLE_USER)
                        {
                            Hashtable domains = new Hashtable();
                            ArrayList input = new ArrayList();
                            foreach (DataRow r in dt.Rows)
                            {
                                object o = r[this.DomID];
                                if (o != null && !domains.ContainsKey(o))
                                {
                                    domains.Add(o, o);
                                    input.Add(o);
                                }
                                if (r[this.DomName].ToString().ToUpper().Equals("NODE"))
                                    ((ConsoleUser)retUser).IsNodeAdmin = true;
                            }
                            ((ConsoleUser)retUser).DomainIDs = input;
                        }
                        if (type == User.LOCAL_NODE_USER)
                        {
                            Hashtable operations = new Hashtable();
                            ArrayList input = new ArrayList();
                            foreach (DataRow r in dt.Rows)
                            {
                                object o = r[this.OpID];
                                if (o != null && !operations.ContainsKey(o))
                                {
                                    operations.Add(o, o);
                                    input.Add(o);
                                }
                            }
                            ((LocalUser)retUser).OperationIDs = input;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retUser;
        }

        /// <summary>
        /// Save a User to the Database
        /// </summary>
        /// <param name="u">The user to save</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        public void SaveUser(User u, string domainAdmin)
        {
            bool error = false;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                DataSet ds = new DataSet();
                string sql = "select * from " + this.TblUser + " where " + this.UserID + " = @" + this.UserID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.UserID, u.UserID));
                db.GetDataSet(this.TblUser, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblUser))
                {
                    DataTable dt = ds.Tables[this.TblUser];
                    bool isNewUserRow = false;
                    DataRow userRow = null;
                    int userID = -1;
                    string password = null;
                    if (dt.Rows.Count > 0)
                    {
                        userRow = dt.Rows[0];
                        userID = int.Parse(userRow[this.UserID].ToString());
                        if (u.ChangePWDFlag)
                        {
                            password = new PasswordGenerator().Generate();
                            userRow[this.LoginPWD] = new Cryptography().Encrypting(password, Phrase.CryptKey);
                            userRow[this.ChangePWD] = "Y";
                        }
                        else
                        {
                            userRow[this.LoginPWD] = u.EncryptedPassword;
                            userRow[this.ChangePWD] = "N";
                        }
                    }
                    else
                    {
                        isNewUserRow = true;
                        userRow = dt.NewRow();
                        userID = this.GetSequenceNumber(this.TblUser, this.UserID);
                        userRow[this.UserID] = userID;
                        password = new PasswordGenerator().Generate();
                        userRow[this.LoginPWD] = new Cryptography().Encrypting(password, Phrase.CryptKey);
                        userRow[this.ChangePWD] = "Y";
                        u.ChangePWDFlag = true;
                        userRow[this.CreatedDate] = DateTime.Now;
                        userRow[this.CreatedBy] = domainAdmin;
                    }
                    userRow[this.LoginName] = u.UserName;
                    userRow[this.LastName] = u.LastName;
                    userRow[this.FirstName] = u.FirstName;
                    userRow[this.MidInitial] = u.MiddleInitial;
                    userRow[this.UserStatus] = u.Status;
                    userRow[this.Phone] = u.PhoneNumber;
                    userRow[this.Comments] = u.Comments;
                    if (domainAdmin != null && domainAdmin.Trim() != "")
                    {
                        userRow[this.UpdatedDate] = DateTime.Now;
                        userRow[this.UpdatedBy] = domainAdmin;
                    }
                    if (isNewUserRow)
                        dt.Rows.Add(userRow);
                    db.UpdateDataSet(this.TblUser, ds);
                    this.UpdateAccountXREF(u, userID, db);
                    this.UpdateEmail(u, userID, db, domainAdmin);
                    this.UpdateAddress(u, userID, db, domainAdmin);
                    if (u.UserType == User.LOCAL_NODE_USER)
                        this.UpdateLocalNodeUserOperations((LocalUser)u, userID, db);
                    u.UserID = userID;
                    if (password != null)
                    {
                        u.Password = password;
                        u.ChangePWDFlag = true;
                    }
                    if (isNewUserRow)
                    {
                        u.CreatedDate = DateTime.Now;
                        u.CreatedBy = domainAdmin;
                    }
                    u.UpdatedDate = DateTime.Now;
                    u.UpdatedBy = domainAdmin;
                }
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                    error = true;
                }
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    if (!error)
                        db.CommitTransaction();
                    db.Close();
                }
            }
        }

        /// <summary>
        /// Delete a User from the Database
        /// </summary>
        /// <param name="u">The user to delete</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        public int DeleteUser(User u, string domainAdmin)
        {
            int numRows = 0;
            DBAdapter db = null;
            try
            {
                if (u != null && u.UserID > 0)
                {
                    db = this.GetNodeDB();
                    db.BeginTransaction();
                    string command = "";
                    ArrayList parameters = new ArrayList();
                    parameters.Add(new Parameter(this.UserID, u.UserID));
                    //delete NODE_USER_OPERATION_XREF
                    command = "delete from " + this.TblUserOp;
                    command += " where " + this.UserID + " = @" + this.UserID;
                    db.ExecuteNonQuery(command, parameters);
                    //delete SYS_USER_ADDRESS
                    command = "delete from " + this.TblUserAddress;
                    command += " where " + this.UserID + " = @" + this.UserID;
                    db.ExecuteNonQuery(command, parameters);
                    //delete SYS_USER_EMAIL
                    command = "delete from " + this.TblUserEmail;
                    command += " where " + this.UserID + " = @" + this.UserID;
                    db.ExecuteNonQuery(command, parameters);
                    //delete SYS_USER_INFO
                    command = "delete from " + this.TblUser;
                    command += " where " + this.UserID + " = @" + this.UserID;
                    numRows = db.ExecuteNonQuery(command, parameters);
                }
            }
            catch (Exception ex)
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                }
                throw ex;
            }
            finally
            {
                if (db != null)
                {
                    if (numRows > 0)
                        db.CommitTransaction();
                    else
                        db.RollbackTransaction();
                    db.Close();
                }
            }
            return numRows;
        }

        /// <summary>
        /// Search the Node Database for Users
        /// </summary>
        /// <param name="loginName">LOGIN_NAME</param>
        /// <param name="type">ACCOUNT_TYPE, either Phrase.CONSOLE_USER, or Phrase.LOCAL_NODE_USER, or empty</param>
        /// <param name="domainID">DOMAIN_ID</param>
        /// <param name="firstName">FIRST_NAME</param>
        /// <param name="lastName">LAST_NAME</param>
        /// <returns>Columns: LOGIN_NAME, USER_FULL_NAME, STATUS_CD, ACCOUNT_TYPE, CREATED_DTTM</returns>
        public DataTable SearchUsers(string loginName, string type, int domainID, string firstName, string lastName)
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string sql = "select distinct A." + this.LoginName + ", ";
                if (db.ProviderName.ToUpper().Equals(Phrase.DB_TYPE_SQL_SERVER))
                    sql += "A." + this.FirstName + " + ' ' + A." + this.LastName + " USER_FULL_NAME, ";
                else if (db.ProviderName.ToUpper().Equals(Phrase.DB_TYPE_ORACLE_9I))
                    sql += "A." + this.FirstName + " || ' ' || A." + this.LastName + " USER_FULL_NAME, ";
                sql += "A." + this.UserStatus + ", C." + this.AccType + ", A." + this.CreatedDate;
                sql += " from " + this.TblUser + " A, " + this.TblAccTypeXREF + " B, " + this.TblAccType + " C";
                sql += " where A." + this.UserID + " = B." + this.UserID + " and B." + this.AccTypeID + " = C." + this.AccTypeID;
                ArrayList parameters = new ArrayList();
                if (loginName != null && !loginName.Trim().Equals(""))
                {
                    sql += " and A." + this.LoginName + " like @" + this.LoginName;
                    parameters.Add(new Parameter(this.LoginName, "%" + loginName + "%"));
                }
                if (type != null && !type.Trim().Equals(""))
                {
                    sql += " and C." + this.AccType + " = @" + this.AccType;
                    parameters.Add(new Parameter(this.AccType, type));
                }
                if (domainID >= 0)
                {
                    sql += " and B." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domainID));
                }
                if (firstName != null && !firstName.Trim().Equals(""))
                {
                    sql += " and A." + this.FirstName + " like @" + this.FirstName;
                    parameters.Add(new Parameter(this.FirstName, "%" + firstName + "%"));
                }
                if (lastName != null && !lastName.Trim().Equals(""))
                {
                    sql += " and A." + this.LastName + " like @" + this.LastName;
                    parameters.Add(new Parameter(this.LastName, "%" + lastName + "%"));
                }
                DataSet ds = new DataSet();
                db.GetDataSet(this.TblUser, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblUser))
                    dt = ds.Tables[this.TblUser];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return dt;
        }

        /// <summary>
        /// Get a List of Console Users
        /// </summary>
        /// <returns>Columns: USER_ID, LOGIN_NAME</returns>
        public DataTable GetConsoleUserList()
        {
            DataTable dt = null;
            DBAdapter db = null;
            try
            {
                string sql = "select distinct A." + this.UserID + ", A." + this.LoginName;
                sql += " from " + this.TblUser + " A, " + this.TblAccTypeXREF + " B, " + this.TblAccType + " C";
                sql += " where A." + this.UserID + " = B." + this.UserID;
                sql += " and B." + this.AccTypeID + " = C." + this.AccTypeID;
                sql += " and C." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " group by A." + this.UserID + ", A." + this.LoginName;
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblUser, sql, ds);
                dt = ds.Tables[this.TblUser];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return dt;
        }
        /// <summary>
        /// The method updates NODE_ACCOUNT_TYPE_XREF by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">The User Object.</param>
        /// <param name="userID">User ID.</param>
        /// <param name="db">Node DBAdapter.</param>
        private void UpdateAccountXREF(User u, int userID, DBAdapter db)
        {
            string sql = "select " + this.AccTypeID + " from " + this.TblAccType + " where " + this.AccType + " = @" + this.AccType;
            ArrayList parameters = new ArrayList();
            if (u.UserType == User.LOCAL_NODE_USER)
                parameters.Add(new Parameter(this.AccType, Phrase.LOCAL_NODE_USER));
            else
                parameters.Add(new Parameter(this.AccType, Phrase.CONSOLE_USER));
            DataSet ds = new DataSet();
            db.GetDataSet(this.TblAccType, sql, parameters, ds);
            if (u.UserType == User.LOCAL_NODE_USER)
            {
                sql = "select " + this.AccTypeXREFID + ", " + this.UserID + ", " + this.AccTypeID + ", " + this.DomID;
                sql += " from " + this.TblAccTypeXREF + " where " + this.UserID + " = @" + this.UserID;
                parameters.Add(new Parameter(this.UserID, userID));
                db.GetDataSet(this.TblAccTypeXREF, sql, parameters, ds);
                bool isInsert = false;
                DataRow xrefRow = null;
                if (ds.Tables[this.TblAccTypeXREF].Rows.Count > 0)
                    xrefRow = ds.Tables[this.TblAccTypeXREF].Rows[0];
                else
                {
                    isInsert = true;
                    xrefRow = ds.Tables[this.TblAccTypeXREF].NewRow();
                    xrefRow[this.AccTypeXREFID] = this.GetSequenceNumber(this.TblAccTypeXREF, this.AccTypeXREFID);
                    xrefRow[this.UserID] = userID;
                }
                xrefRow[this.AccTypeID] = ds.Tables[this.TblAccType].Rows[0][this.AccTypeID];
                xrefRow[this.DomID] = DBNull.Value;
                if (isInsert)
                    ds.Tables[this.TblAccTypeXREF].Rows.Add(xrefRow);
                db.UpdateDataSet(this.TblAccTypeXREF, ds);
            }
            else
            {
                ConsoleUser cu = (ConsoleUser)u;
                sql = "select " + this.AccTypeXREFID + ", " + this.UserID + ", " + this.AccTypeID + ", " + this.DomID;
                sql += " from " + this.TblAccTypeXREF + " where " + this.UserID + " = @" + this.UserID;
                parameters = new ArrayList();
                parameters.Add(new Parameter(this.UserID, userID));
                db.GetDataSet(this.TblAccTypeXREF, sql, parameters, ds);
                if (cu.DomainIDs.Count > 0)
                {
                    if (ds.Tables[this.TblAccTypeXREF].Rows.Count > cu.DomainIDs.Count)
                    {
                        for (int i = ds.Tables[this.TblAccTypeXREF].Rows.Count - 1; i >= cu.DomainIDs.Count; i--)
                            ds.Tables[this.TblAccTypeXREF].Rows[i].Delete();
                    }
                    for (int i = 0; i < cu.DomainIDs.Count; i++)
                    {
                        bool isInsert = false;
                        DataRow xrefRow = null;
                        if (ds.Tables[this.TblAccTypeXREF].Rows.Count > i)
                            xrefRow = ds.Tables[this.TblAccTypeXREF].Rows[i];
                        else
                        {
                            isInsert = true;
                            xrefRow = ds.Tables[this.TblAccTypeXREF].NewRow();
                            xrefRow[this.AccTypeXREFID] = this.GetSequenceNumber(this.TblAccTypeXREF, this.AccTypeXREFID);
                            xrefRow[this.UserID] = userID;
                        }
                        xrefRow[this.AccTypeID] = ds.Tables[this.TblAccType].Rows[0][this.AccTypeID];
                        xrefRow[this.DomID] = int.Parse(cu.DomainIDs[i].ToString());
                        if (isInsert)
                            ds.Tables[this.TblAccTypeXREF].Rows.Add(xrefRow);
                    }
                }
                else
                {
                    bool isInsert = false;
                    DataRow xrefRow = null;
                    if (ds.Tables[this.TblAccTypeXREF].Rows.Count > 0)
                    {
                        for (int i = 1; i < ds.Tables[this.TblAccTypeXREF].Rows.Count; i++)
                            ds.Tables[this.TblAccTypeXREF].Rows[i].Delete();
                        xrefRow = ds.Tables[this.TblAccTypeXREF].Rows[0];
                    }
                    else
                    {
                        isInsert = true;
                        xrefRow = ds.Tables[this.TblAccTypeXREF].NewRow();
                        xrefRow[this.AccTypeXREFID] = this.GetSequenceNumber(this.TblAccTypeXREF, this.AccTypeXREFID);
                        xrefRow[this.UserID] = userID;
                    }
                    xrefRow[this.AccTypeID] = ds.Tables[this.TblAccType].Rows[0][this.AccTypeID];
                    xrefRow[this.DomID] = DBNull.Value;
                    if (isInsert)
                        ds.Tables[this.TblAccTypeXREF].Rows.Add(xrefRow);
                }
                db.UpdateDataSet(this.TblAccTypeXREF, ds);
            }
        }
        /// <summary>
        /// The method update specified user email address.
        /// </summary>
        /// <param name="u">The User Object.</param>
        /// <param name="userID">User ID.</param>
        /// <param name="db">Node DBAdapter.</param>
        /// <param name="domainAdmin">The Domain Admini account.</param>
        private void UpdateEmail(User u, int userID, DBAdapter db, string domainAdmin)
        {
            string sql = "select " + this.EmailID + ", " + this.UserID;
            sql += " from " + this.TblUserEmail + " where " + this.UserID + " = @" + this.UserID;
            ArrayList parameters = new ArrayList();
            parameters.Add(new Parameter(this.UserID, userID));
            DataSet ds = new DataSet();
            db.GetDataSet(this.TblUserEmail, sql, parameters, ds);
            DataTable userEmailDT = ds.Tables[this.TblUserEmail];
            int emailID = -1;
            if (userEmailDT.Rows.Count > 0)
                emailID = int.Parse(userEmailDT.Rows[0][this.EmailID].ToString());
            sql = "select * from " + this.TblEmail + " where " + this.EmailID + " = @" + this.EmailID;
            parameters = new ArrayList();
            parameters.Add(new Parameter(this.EmailID, emailID));
            db.GetDataSet(this.TblEmail, sql, parameters, ds);
            DataTable emailDT = ds.Tables[this.TblEmail];
            DataRow emailRow = null;
            bool isInsert = false;
            if (emailDT.Rows.Count > 0)
                emailRow = emailDT.Rows[0];
            else
            {
                isInsert = true;
                emailRow = emailDT.NewRow();
                emailID = this.GetSequenceNumber(this.TblEmail, this.EmailID);
                emailRow[this.EmailID] = emailID;
                emailRow[this.EmailType] = "NODE";
                emailRow[this.Status] = "A";
                emailRow[this.CreatedDate] = DateTime.Now;
                emailRow[this.CreatedBy] = domainAdmin;
            }
            emailRow[this.Email] = u.EmailAddress;
            if (domainAdmin != null && domainAdmin.Trim() != "")
            {
                emailRow[this.UpdatedDate] = DateTime.Now;
                emailRow[this.UpdatedBy] = domainAdmin;
            }
            if (isInsert)
                emailDT.Rows.Add(emailRow);
            db.UpdateDataSet(this.TblEmail, ds);
            DataRow userEmailRow = null;
            bool isUserEmailInsert = false;
            if (userEmailDT.Rows.Count > 0)
                userEmailRow = userEmailDT.Rows[0];
            else
            {
                isUserEmailInsert = true;
                userEmailRow = userEmailDT.NewRow();
                userEmailRow[this.UserID] = userID;
            }
            userEmailRow[this.EmailID] = emailID;
            if (isUserEmailInsert)
                userEmailDT.Rows.Add(userEmailRow);
            db.UpdateDataSet(this.TblUserEmail, ds);
        }
        /// <summary>
        /// The method update specified user address.
        /// </summary>
        /// <param name="u">The User Object.</param>
        /// <param name="userID">User ID.</param>
        /// <param name="db">Node DBAdapter.</param>
        /// <param name="domainAdmin">The Domain Admini account.</param>
        private void UpdateAddress(User u, int userID, DBAdapter db, string domainAdmin)
        {
            string sql = "select * from " + this.TblUserAddress + " where " + this.UserID + " = @" + this.UserID;
            ArrayList parameters = new ArrayList();
            parameters.Add(new Parameter(this.UserID, userID));
            DataSet ds = new DataSet();
            db.GetDataSet(this.TblUserAddress, sql, parameters, ds);
            DataTable userAddressDT = ds.Tables[this.TblUserAddress];
            int addressID = -1;
            if (userAddressDT.Rows.Count > 0)
                addressID = int.Parse(userAddressDT.Rows[0][this.AddressID].ToString());
            sql = "select * from " + this.TblAddress + " where " + this.AddressID + " = @" + this.AddressID;
            parameters = new ArrayList();
            parameters.Add(new Parameter(this.AddressID, addressID));
            db.GetDataSet(this.TblAddress, sql, parameters, ds);
            DataTable addressDT = ds.Tables[this.TblAddress];
            DataRow addressRow = null;
            bool isInsert = false;
            if (addressDT.Rows.Count > 0)
                addressRow = addressDT.Rows[0];
            else
            {
                isInsert = true;
                addressRow = addressDT.NewRow();
                addressID = this.GetSequenceNumber(this.TblAddress, this.AddressID);
                addressRow[this.AddressID] = addressID;
                addressRow[CreatedDate] = DateTime.Now;
                addressRow[this.CreatedBy] = domainAdmin;
            }
            addressRow[this.Address] = u.Address;
            addressRow[this.SuppAddress] = u.SupplementalAddress;
            addressRow[this.LocalityName] = u.LocalityName;
            addressRow[this.StateCD] = u.StateUSPSCode;
            addressRow[this.ZipCode] = u.ZipCode;
            addressRow[this.CountryCD] = u.CountryCode;
            addressRow[this.Status] = "A";
            if (domainAdmin != null && domainAdmin.Trim() != "")
            {
                addressRow[this.UpdatedDate] = DateTime.Now;
                addressRow[this.UpdatedBy] = domainAdmin;
            }
            if (isInsert)
                addressDT.Rows.Add(addressRow);
            db.UpdateDataSet(this.TblAddress, ds);
            bool isUserAddressInsert = false;
            DataRow userAddressRow = null;
            if (userAddressDT.Rows.Count > 0)
                userAddressRow = userAddressDT.Rows[0];
            else
            {
                isUserAddressInsert = true;
                userAddressRow = userAddressDT.NewRow();
                userAddressRow[this.UserID] = userID;
            }
            userAddressRow[this.AddressID] = addressID;
            if (isUserAddressInsert)
                userAddressDT.Rows.Add(userAddressRow);
            db.UpdateDataSet(this.TblUserAddress, ds);
        }
        /// <summary>
        /// The method update NODE_USER_OPERATION_XREF by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">The User Object.</param>
        /// <param name="userID">User ID.</param>
        /// <param name="db">Node DBAdapter.</param>
        private void UpdateLocalNodeUserOperations(LocalUser u, int userID, DBAdapter db)
        {
            string sql = "select " + this.UserID + ", " + this.OpID + " from " + this.TblUserOp + " where " + this.UserID + " = @" + this.UserID;
            ArrayList parameters = new ArrayList();
            parameters.Add(new Parameter(this.UserID, userID));
            DataSet ds = new DataSet();
            db.GetDataSet(this.TblUserOp, sql, parameters, ds);

            if (ds.Tables[this.TblUserOp].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[this.TblUserOp].Rows.Count; i++)
                    ds.Tables[this.TblUserOp].Rows[i].Delete();
            }

            if (u.OperationIDs.Count > 0)
            {
                DataRow newRow = null;
                for (int i = 0; i < u.OperationIDs.Count; i++)
                {                    
                    newRow = ds.Tables[this.TblUserOp].NewRow();
                    newRow[this.OpID] = int.Parse(u.OperationIDs[i].ToString());
                    newRow[this.UserID] = userID;

                    ds.Tables[this.TblUserOp].Rows.Add(newRow);
                }
            }

            db.UpdateDataSet(this.TblUserOp, ds);
        }
    }
}
