using System;
using System.Collections;

using Node.Core.Biz.NAAS;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// NAASUser contains NAAS user information. 
    /// </summary>
    public class NAASUser : User
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
        /// <summary>
        /// Indicator for New User.
        /// </summary>
        public bool IsNew
        {
            get { return this.isNewUser; }
            set { this.isNewUser = value; }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of NAASuser.
        /// </summary>
        public NAASUser()
        {
            this.UserType = User.NAAS_NODE_USER;
        }
        /// <summary>
        /// Constructor of NAASuser.
        /// </summary>
        /// <param name="loginName">The User ID of the NAAS User</param>
        /// <param name="complete">true if want polcies and user properties to be retrieved, false otherwise</param>
        public NAASUser(string loginName, bool complete)
        {
            this.UserType = User.NAAS_NODE_USER;
            this.UserName = loginName;
            this.Init(new UserManager().GetUser(loginName, complete));
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Save NAAS User info.
        /// </summary>
        public void Save()
        {
            UserManager manager = new UserManager();
            manager.SaveUser(this);
        }
        /// <summary>       
        /// Delete NAAS User info.
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            UserManager manager = new UserManager();
            return manager.DeleteUser(this);
        }

        #endregion

        #region Protected Methods
        /// <summary>
        ///  The method initiates the NAASUser by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">Node.Core.Biz.Objects.User</param>
        protected override void Init(User u)
        {
            base.Init(u);
            if (u != null && u.UserType == User.NAAS_NODE_USER)
            {
                NAASUser nu = (NAASUser)u;
                this.operationIDs = nu.operationIDs;
            }
        }

        #endregion

        #region Private Fields

        private ArrayList operationIDs = new ArrayList();
        private bool isNewUser = true;

        #endregion
    }
}
