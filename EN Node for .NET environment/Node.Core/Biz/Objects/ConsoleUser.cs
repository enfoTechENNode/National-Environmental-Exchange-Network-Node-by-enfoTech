using System;
using System.Collections;

using Node.Core.Data;
using Node.Core.Data.Interfaces;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// ConsoleUser contains Console user information.
    /// </summary>
    public class ConsoleUser : User
    {
        #region Public Properties
        /// <summary>
        /// Indicator for Node Administration Role.
        /// </summary>
        public bool IsNodeAdmin
        {
            get { return this.isNodeAdmin; }
            set { this.isNodeAdmin = value; }
        }
        /// <summary>
        /// A List of Domain belong to user.
        /// </summary>
        public ArrayList DomainIDs
        {
            get { return this.domainIDs; }
            set
            {
                if (value == null)
                    this.domainIDs = new ArrayList();
                else
                    this.domainIDs = value;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of ConsoleUser.
        /// </summary>
        public ConsoleUser()
        {
            this.UserType = User.CONSOLE_USER;
        }
        /// <summary>
        /// Constructor of ConsoleUser. 
        /// </summary>
        /// <param name="loginName">User Name created by Node Administration</param>
        public ConsoleUser(string loginName)
        {
            this.UserName = loginName;
            this.Init(new DBManager().GetUsersDB().GetUser(loginName, User.CONSOLE_USER));
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Save User Information.
        /// </summary>
        /// <param name="domainAdmin">The Domain Administrator who is logged in.</param>
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
        ///  The method initiates the ConsoleUser by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">Node.Core.Biz.Objects.User</param>
        protected override void Init(User u)
        {
            base.Init(u);
            if (u != null && u.UserType == User.CONSOLE_USER)
            {
                ConsoleUser cu = (ConsoleUser)u;
                this.domainIDs = cu.domainIDs;
                this.isNodeAdmin = cu.isNodeAdmin;
            }
        }

        #endregion

        #region Private Fields

        private ArrayList domainIDs = new ArrayList();
        private bool isNodeAdmin = false;

        #endregion
    }
}
