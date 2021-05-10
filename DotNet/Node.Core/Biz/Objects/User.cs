using System;
using System.Collections;
using System.Data;

using Node.Lib.Security;
using Node.Core.Biz.NAAS;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.NAASPolicy;
using Node.Core.NAASUserManager;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// Represents a Business Object that Handles Users
    /// </summary>
    public class User
    {
        #region Public Constants

        /// <summary>
        /// Console User Type (either Node Administrator or Domain Administrator)
        /// </summary>
        public const int CONSOLE_USER = 1;
        /// <summary>
        /// Locally Managed Node User Type
        /// </summary>
        public const int LOCAL_NODE_USER = 2;

        /// <summary>
        /// NAAS Managed Node User Type
        /// </summary>
        public const int NAAS_NODE_USER = 3;

        /// <summary>
        /// Node User Type (could be either Locally Managed or NAAS Managed
        /// </summary>
        public const int NODE_USER = 4;

        #endregion

        #region Public Properties

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName
        {
            get { return this.login; }
            set
            {
                if (value == null || value.Trim().Equals(""))
                    throw new Exception("User.UserName cannot be null or the empty string");
                this.login = value;
            }
        }

        /// <summary>
        /// Type of User
        /// </summary>
        public int UserType
        {
            get { return this.userType; }
            set
            {
                if (value < User.CONSOLE_USER || value > User.NODE_USER)
                    throw new Exception("User.UserType must be one of User.CONSOLE_USER, User.LOCAL_NODE_USER, User.NAAS_NODE_USER, or User.NODE_USER");
                this.userType = value;
            }
        }

        /// <summary>
        /// User ID in database
        /// </summary>
        public int UserID 
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// First Name of User
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                string input = value;
                if (value != null && value.Length > 60)
                    input = value.Substring(0, 60);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.firstName = input;
            }
        }

        /// <summary>
        /// Middle Initial of User
        /// </summary>
        public string MiddleInitial
        {
            get { return this.middleInitial; }
            set
            {
                string input = value;
                if (value != null && value.Length > 1)
                    input = value.Substring(0, 1);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.middleInitial = input;
            }
        }

        /// <summary>
        /// Last Name of User
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }
            set
            {
                string input = value;
                if (value != null && value.Length > 60)
                    input = value.Substring(0, 60);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.lastName = input;
            }
        }

        /// <summary>
        /// Password for User (unencrypted)
        /// Cannot be null or empty unless user is a NAAS Managed User (User.NAAS_NODE_USER)
        /// </summary>
        public string Password
        {
            get
            {
                if (this.loginPassword != null && !this.loginPassword.Trim().Equals(""))
                {
                    Cryptography crypt = new Cryptography();
                    string encrypted = crypt.Encrypting("password", Phrase.CryptKey);
                    return crypt.Decrypting(this.loginPassword, Phrase.CryptKey);
                }
                return null;
            }
            set
            {
                if (this.userType == User.NAAS_NODE_USER || (value != null && !value.Trim().Equals("")))
                {
                    Cryptography crypt = new Cryptography();
                    string input = crypt.Encrypting(value, Phrase.CryptKey);
                    if (input != null && input.Length < 100)
                        this.loginPassword = input;
                    else
                        throw new Exception("Encrypted Version of User.Password does not meet Database Requirements");
                }
                else
                    throw new Exception("User.Password cannot be null or empty");
            }
        }

        /// <summary>
        /// Encrypted Password for User
        /// Cannot be null or empty unless user is a NAAS Managed User (User.NAAS_NODE_USER)
        /// </summary>
        public string EncryptedPassword
        {
            get { return this.loginPassword; }
            set
            {
                if (this.userType == User.NAAS_NODE_USER || (value != null && !value.Trim().Equals("")))
                {
                    if (value != null && value.Length > 100)
                        throw new Exception("Encrypted Version of User.Password does not meet Database Requirements");
                    this.loginPassword = value;
                }
                else
                    throw new Exception("User.Password cannot be null or empty");
            }
        }

        /// <summary>
        /// Status of User
        /// Must be 'A' or 'I'
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set
            {
                if (value != null && (value.Equals("A") || value.Equals("I")))
                {
                    this.status = value;
                }
                else
                {
                    string input = value;
                    if (value == null)
                        input = "null";
                    throw new Exception(input + " is an Invalid User.Status");
                }
            }
        }

        /// <summary>
        /// Last 4 Social Security Numbers of User
        /// </summary>
        public string Last4SSN
        {
            get { return this.last4SSN; }
            set
            {
                string input = value;
                if (value != null && value.Length > 4)
                    input = value.Substring(0, 4);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.last4SSN = input;
            }
        }

        /// <summary>
        /// boolean value indicating whether or not Console User needs to changed password
        /// when Console User Logs in to the Node Administration Application
        /// </summary>
        public bool ChangePWDFlag
        {
            get { return this.changePWDFlag; }
            set { this.changePWDFlag = value; }
        }

        /// <summary>
        /// Phone Number of User
        /// </summary>
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set
            {
                string input = value;
                if (value != null && value.Length > 15)
                    input = value.Substring(0, 15);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.phoneNumber = input;
            }
        }

        /// <summary>
        /// Comments Describing User
        /// </summary>
        public string Comments
        {
            get { return this.comments; }
            set
            {
                string input = value;
                if (value != null && value.Length > 255)
                    input = value.Substring(0, 255);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.comments = input;
            }
        }

        /// <summary>
        /// Address ID of the User's Address
        /// </summary>
        public int AddressID
        {
            get { return this.addressID; }
            set { this.addressID = value; }
        }

        /// <summary>
        /// Address of the User
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set
            {
                string input = value;
                if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.address = input;
            }
        }

        /// <summary>
        /// Supplemental Address
        /// </summary>
        public string SupplementalAddress
        {
            get { return this.supplAddress; }
            set
            {
                string input = value;
                if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.supplAddress = input;
            }
        }

        /// <summary>
        /// Locality (City) Name of the User
        /// </summary>
        public string LocalityName
        {
            get { return this.localityName; }
            set
            {
                string input = value;
                if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.localityName = input;
            }
        }

        /// <summary>
        /// State USPS Code (2 Characters) of User
        /// Example: NJ
        /// </summary>
        public string StateUSPSCode
        {
            get { return this.stateCD; }
            set
            {
                string input = value;
                if (value != null && value.Length > 2)
                    input = value.Substring(0, 2);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.stateCD = input;
            }
        }

        /// <summary>
        /// Zip Code of User
        /// </summary>
        public string ZipCode
        {
            get { return this.zipCD; }
            set
            {
                string input = value;
                if (value != null && value.Length > 15)
                    input = value.Substring(0, 15);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.zipCD= input;
            }
        }

        /// <summary>
        /// Country Code of User
        /// </summary>
        public string CountryCode
        {
            get { return this.countryCD; }
            set
            {
                string input = value;
                if (value != null && value.Length > 25)
                    input = value.Substring(0, 25);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.countryCD = input;
            }
        }

        /// <summary>
        /// Address Description of User
        /// </summary>
        public string AddressDescription
        {
            get { return this.addressDesc; }
            set
            {
                string input = value;
                if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.addressDesc = input;
            }
        }

        /// <summary>
        /// Email ID of the User's Email
        /// </summary>
        public int EmailID
        {
            get { return this.emailID; }
            set { this.emailID = value; }
        }

        /// <summary>
        /// Email Address of the User
        /// </summary>
        public string EmailAddress
        {
            get { return this.email; }
            set
            {
                string input = value;
                if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                if (input == null || input.Trim().Equals(""))
                    input = null;
                this.email = input;
            }
        }

        /// <summary>
        /// Created Date of User
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }

        /// <summary>
        /// The Console User that Created this User
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }

        /// <summary>
        /// Updated Date of User
        /// </summary>
        public DateTime UpdatedDate
        {
            get { return this.updatedDate; }
            set { this.updatedDate = value; }
        }

        /// <summary>
        /// The Console user that Updated this User
        /// </summary>
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set { this.updatedBy = value; }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Create a New User with No Information
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Get the business object the represents a user.
        /// Can be either a Console User or a Node User
        /// </summary>
        /// <param name="loginName">The login name or user name of the user.</param>
        /// <param name="functionalityType">One of User.CONSOLE_USER or User.NODE_USER</param>
        public User(string loginName, int functionalityType)
        {
            this.login = loginName;
            this.userType = functionalityType;
            this.Init(new DBManager().GetUsersDB().GetUser(loginName, functionalityType));
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Search the Node Database and NAAS Users for all Users associated with this Node and meeting the input criteria.
        /// The domain, firstName, and lastName input parameters are ignored for NAAS User Searches
        /// </summary>
        /// <param name="loginName">The login name or Node User Name of the user.
        /// If a NAAS user, either the portion before the @ symbol, the portion after the @ symbol, or the entire user name</param>
        /// <param name="userType">Type of user, either Phrase.CONSOLE_USER, Phrase.NAAS_NODE_USER, or Phrase.LOCAL_NODE_USER</param>
        /// <param name="domainID">Associated domain id of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="allNAASsers">true if all NAAS users should be searched, or falst if just NAAS users owned by this Node Administrator or have policies on this node should be returned</param>
        /// <returns></returns>
        public static DataTable SearchUsers(string loginName, string userType, int domainID, string firstName, string lastName, bool allNAASsers)
        {
            DataTable dt = new DBManager().GetUsersDB().SearchUsers(loginName, userType, domainID, firstName, lastName);
            dt.Columns["CREATED_DTTM"].AllowDBNull = true;
            if (userType == null || userType.Trim().Equals("") || userType.Equals(Phrase.NAAS_NODE_USER))
            {
                UserInfo[] users = User.GetUserInfoList(loginName, allNAASsers);
                if (users != null && users.Length > 0)
                {
                    foreach (UserInfo info in users)
                    {
                        DataRow dr = dt.NewRow();
                        dr["LOGIN_NAME"] = info.UserId;
                        dr["ACCOUNT_TYPE"] = Phrase.NAAS_NODE_USER;
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        
        /// <summary>
        /// Get a List of Console Users
        /// </summary>
        /// <returns>Columns: USER_ID, LOGIN_NAME</returns>
        public static DataTable GetConsoleUsersList()
        {
            return new DBManager().GetUsersDB().GetConsoleUserList();
        }

        #endregion

        #region Protected Virtual Methods
        /// <summary>
        ///  The method initiates the User by Node.Core.Biz.Objects.User.
        /// </summary>
        /// <param name="u">Node.Core.Biz.Objects.User</param>
        protected virtual void Init(User u)
        {
            if (u != null)
            {
                this.id = u.id;
                this.userType = u.userType;
                this.firstName = u.firstName;
                this.middleInitial = u.middleInitial;
                this.lastName = u.lastName;
                this.loginPassword = u.loginPassword;
                this.status = u.status;
                this.last4SSN = u.last4SSN;
                this.changePWDFlag = u.changePWDFlag;
                this.phoneNumber = u.phoneNumber;
                this.comments = u.comments;
                this.addressID = u.addressID;
                this.address = u.address;
                this.supplAddress = u.supplAddress;
                this.localityName = u.localityName;
                this.stateCD = u.stateCD;
                this.zipCD = u.zipCD;
                this.countryCD = u.countryCD;
                this.emailID = u.emailID;
                this.email = u.email;
                this.createdDate = u.createdDate;
                this.createdBy = u.createdBy;
                this.updatedDate = u.updatedDate;
                this.updatedBy = u.updatedBy;
            }
        }

        #endregion

        #region Private Fields

        private int id = -1;
        private string login = null;
        private int userType = -1;
        private string firstName = null;
        private string middleInitial = null;
        private string lastName = null;
        private string loginPassword = null;
        private string status = null;
        private string last4SSN = null;
        private bool changePWDFlag = false;
        private string phoneNumber = null;
        private string comments = null;
        private int addressID = -1;
        private string address = null;
        private string supplAddress = null;
        private string localityName = null;
        private string stateCD = null;
        private string zipCD = null;
        private string countryCD = null;
        private string addressDesc = null;
        private int emailID = -1;
        private string email = null;
        private DateTime createdDate;
        private string createdBy = null;
        private DateTime updatedDate;
        private string updatedBy = null;

        #endregion

        #region Private Static Methods

        private static UserInfo[] GetUserInfoList(string loginName, bool allNAAS)
        {
            UserManager manager = new UserManager();
            SortedList sort = new SortedList();
            if (allNAAS)
            {
                UserInfo[] temp = manager.GetUserList(loginName, true);
                foreach (UserInfo info in temp)
                    sort.Add(info.UserId, info);
            }
            else
            {
                UserInfo[] temp = manager.GetUserList(loginName, false);
                if (temp != null && temp.Length > 0)
                {
                    foreach (UserInfo info in temp)
                        sort.Add(info.UserId, info);
                }
                //PolicyManager polManager = new PolicyManager();
                //PolicyInfo[] temp2 = polManager.GetPolicyList(loginName);
                //if (temp2 != null && temp2.Length > 0)
                //{
                //    foreach (PolicyInfo info in temp2)
                //    {
                //        if (!sort.ContainsKey(info.Subject))
                //        {
                //            UserInfo uInfo = new UserInfo();
                //            uInfo.UserId = info.Subject;
                //            sort.Add(uInfo.UserId, uInfo);
                //        }
                //    }
                //}
            }
            UserInfo[] retList = new UserInfo[sort.Count];
            for (int i = 0; i < sort.Count; i++)
                retList[i] = (UserInfo)sort.GetByIndex(i);
            return retList;
        }

        #endregion
    }
}
