using System;
using System.Data;

using Node.Core.Biz.Objects;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// The Interface Class for retrieving User Information stored in Node Application.
    /// </summary>
    public interface IUsers
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
        int LocalAuthenticate(string userName, string credential);

        /// <summary>
        /// Authorize a Local Token
        /// </summary>
        /// <param name="token">Security Token</param>
        /// <param name="opName">Operation (DataFlow or Request) Name</param>
        /// <param name="wsName">Web Service Name</param>
        /// <returns></returns>
        string LocalAuthorize(string token, string opName, string wsName);

        /// <summary>
        /// Get User Information from Database
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="type">Type, one of User.CONSOLE_USER, User.NODE_USER</param>
        /// <returns><see cref="Node.Core.Biz.Objects.User">Node.Core.Biz.Objects.User</see></returns>
        User GetUser(string userName, int type);

        /// <summary>
        /// Save a User to the Database
        /// </summary>
        /// <param name="u">The user to save</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        void SaveUser(User u, string domainAdmin);

        /// <summary>
        /// Delete a User from the Database
        /// </summary>
        /// <param name="u">The user to delete</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        int DeleteUser(User u, string domainAdmin);

        /// <summary>
        /// Search the Node Database for Users
        /// </summary>
        /// <param name="loginName">LOGIN_NAME</param>
        /// <param name="type">ACCOUNT_TYPE, either Phrase.CONSOLE_USER, or Phrase.LOCAL_NODE_USER, or empty</param>
        /// <param name="domainID">DOMAIN_ID</param>
        /// <param name="firstName">FIRST_NAME</param>
        /// <param name="lastName">LAST_NAME</param>
        /// <returns>Columns: LOGIN_NAME, USER_FULL_NAME, STATUS_CD, ACCOUNT_TYPE, CREATED_DTTM</returns>
        DataTable SearchUsers(string loginName, string type, int domainID, string firstName, string lastName);

        /// <summary>
        /// Get a List of Console Users
        /// </summary>
        /// <returns>Columns: USER_ID, LOGIN_NAME</returns>
        DataTable GetConsoleUserList();
    }
}
