using System;
using System.Collections;

using Node.Core.Data;
using Node.Core.Data.Interfaces;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// LocalUser contains Local user information.
    /// </summary>
    public class LocalUser : User
    {
        #region Public Properties
        /// <summary>
        /// List of Operation ID belong to the User.
        /// </summary>
        public ArrayList OperationIDs
        {
            get { return this.operationIDs; }
            set
            {
                if (value == null)
                    this.operationIDs = new ArrayList();
                else
                    this.operationIDs = value;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of LocalUser.
        /// </summary>
        public LocalUser()
        {
            this.UserType = User.LOCAL_NODE_USER;
        }
        /// <summary>
        /// Constructor of LocalUser. 
        /// </summary>
        /// <param name="loginName">The User ID of the Local User</param>
        public LocalUser(string loginName)
        {
            this.UserType = User.LOCAL_NODE_USER;
            this.UserName = loginName;
            this.Init(new DBManager().GetUsersDB().GetUser(loginName, User.LOCAL_NODE_USER));
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Save a User
        /// </summary>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        public void Save(string domainAdmin)
        {
            IUsers userDB = new DBManager().GetUsersDB();
            userDB.SaveUser(this, domainAdmin);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainAdmin">The Domain Administrator who is logged in.</param>
        /// <returns>0 if false to delete the user.</returns>
        public int Delete(string domainAdmin)
        {
            IUsers userDB = new DBManager().GetUsersDB();
            return userDB.DeleteUser(this, domainAdmin);
        }

        #endregion

        #region Protected Methods
        /// <summary>
        ///  The method initiates the LocalUser by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">Node.Core.Biz.Objects.User</param>
        protected override void Init(User u)
        {
            base.Init(u);
            if (u != null && u.UserType == User.LOCAL_NODE_USER)
            {
                LocalUser lu = (LocalUser)u;
                this.operationIDs = lu.operationIDs;
            }
        }

        #endregion

        #region Private Fields

        private ArrayList operationIDs = new ArrayList();

        #endregion
    }
}
