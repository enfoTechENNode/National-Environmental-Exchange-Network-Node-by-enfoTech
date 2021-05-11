using System;
using System.Collections;

using Node.Lib.Security;

using Node.Core.Biz.Objects;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.NAASPolicy;
using Node.Core.NAASUserManager;

namespace Node.Core.Biz.NAAS
{
    /// <summary>
    /// This class is used to access NAAS User Management Web Service Interface
    /// </summary>
    public class UserManager
    {
        #region Public Constructors

        /// <summary>
        /// Creates an Instance of an Object that Can be used to call NAAS User Management Web Services
        /// </summary>
        public UserManager()
        {
            SystemConfiguration config = new SystemConfiguration();
            string naasURL = config.GetNAASUserManagementAddress();
            if (naasURL != null && !naasURL.Trim().Equals(""))
            {
                string proxyServer = config.GetProxyHost();
                string proxyUID = null;
                string proxyPWD = null;
                if (proxyServer != null && !proxyServer.Trim().Equals(""))
                {
                    proxyUID = config.GetProxyUID();
                    proxyPWD = config.GetProxyPWD();
                    if (proxyUID == null || proxyUID.Trim().Equals(""))
                        proxyUID = null;
                    if (proxyPWD == null || proxyPWD.Trim().Equals(""))
                        proxyPWD = null;
                }
                this.manager = new Node.Core.NAAS.UserManagment.UserManager(naasURL, proxyServer, proxyUID, proxyPWD);
            }
            this.adminUID = config.GetNodeAdministratorUserID();
            this.adminPWD = config.GetNodeAdministratorPassword();
            //this.state = this.GetStateID(config.GetNodeName());
            this.state = config.GetNodeName();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the List of NAAS Users
        /// </summary>
        /// <param name="searchUser">User ID of NAAS User</param>
        /// <param name="all">Indicates whether or not to get the complete list of NAAS Users, or just the ones associated with this node</param>
        /// <returns></returns>
        public UserInfo[] GetUserList(string searchUser, bool all)
        {
            string user = searchUser;
            if (user == null || user.Trim().Equals(""))
                user = "";
            //StateId temp = new StateId();
            //if (all)
            //{
            //    temp.Name = string.Empty;
            //}
            //else
            //{
            //    temp.Name = this.state;
            //}
            
            ////if (all)
            ////{
            ////    temp = StateId.Item;
            ////}
            ////else
            ////{
            ////    temp = this.state;
            ////}
            //return this.manager.GetUserList(this.adminUID, this.adminPWD, user, temp, "0", "-1");

            if (all)
                return this.manager.GetUserList(this.adminUID, this.adminPWD, user, StateId.Item, "0", "-1");
            else
                return this.manager.GetUserList(this.adminUID, this.adminPWD, user, (StateId)Enum.Parse(typeof(StateId),this.state,true), "0", "-1");

        }

        /// <summary>
        /// Get NAAS User
        /// </summary>
        /// <param name="loginName">The User ID of the NAAS User</param>
        /// <param name="complete">true if want polcies and user properties to be retrieved, false otherwise</param>
        /// <returns>A NAASUser Object</returns>
        public NAASUser GetUser(string loginName, bool complete)
        {
            NAASUser nu = new NAASUser();
            nu.UserName = loginName;
            ////UserInfo[] users = this.manager.GetUserList(this.adminUID, this.adminPWD, loginName, StateId.Item, "0", "200");
            //StateId term = new StateId();
            //term.Name = string.Empty;
            UserInfo[] users = this.manager.GetUserList(this.adminUID, this.adminPWD, loginName, StateId.Item, "0", "200");
            if (users != null && users.Length > 0)
                nu.UserID = 0;
            else
                nu.UserID = -1;
            /********************** Waiting for NAAS *************************/
            /*
            UserProperty[] properties = this.manager.GetUserProperty(this.adminUID, this.adminPWD, loginName);
            if (properties != null && properties.Length > 0)
            {
                UserProperty prop = properties[0];
                if (prop.CommonName != null && !prop.CommonName.Trim().Equals(""))
                {
                    string[] names = prop.CommonName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    nu.FirstName = names[0];
                    if (names.Length > 1)
                    {
                        if (names.Length > 2)
                        {
                            nu.MiddleInitial = names[1] != null && !names[1].Trim().Equals("") ? names[1].Substring(0, 1) : null;
                            nu.LastName = names[2] != null && !names[2].Trim().Equals("") ? names[2] : null;
                        }
                        else
                            nu.LastName = names[1] != null && !names[1].Trim().Equals("") ? names[1] : null;
                    }
                }
                nu.PhoneNumber = prop.Phone;
                nu.Address = prop.Address != null && !prop.Address.Trim().Equals("") ? prop.Address : null;
                nu.LocalityName = prop.City != null && !prop.City.Trim().Equals("") ? prop.City : null;
                nu.StateUSPSCode = prop.State.ToString();
                nu.ZipCode = prop.ZipCode;
                nu.UpdatedDate = prop.LastChange != null && !prop.LastChange.Trim().Equals("") ? (DateTime)prop.LastChange : null;
            }
            */
            /********************** Waiting for NAAS *************************/
            PolicyManager polManager = new PolicyManager();
            PolicyInfo[] policies = polManager.GetPolicyList(loginName);
            if (policies != null && policies.Length > 0)
            {
                string[] opNames = new string[policies.Length];
                string[] wsNames = new string[policies.Length];
                for (int i = 0; i < policies.Length; i++)
                {
                    opNames[i] = policies[i].Request;
                    wsNames[i] = policies[i].Method;
                }
                int[] ids = new DBManager().GetOperationsDB().GetOperationIDs(opNames, wsNames);
                ArrayList opIDs = new ArrayList();
                if (ids != null && ids.Length > 0)
                {
                    for (int i = 0; i < ids.Length; i++)
                        opIDs.Add(ids[i]);
                }
                nu.OperationIDs = opIDs;
            }
            return nu;
        }

        /// <summary>
        /// Save a NAAS User
        /// </summary>
        /// <param name="nu">The User to Save</param>
        public void SaveUser(NAASUser nu)
        {
            /********************** Waiting for NAAS *************************/
            /*
            string name = nu.FirstName;
            if (name == null)
                name = "";
            if (!name.Trim().Equals(""))
                name += " ";
            name += nu.MiddleInitial != null && !nu.MiddleInitial.Trim().Equals("") ? nu.MiddleInitial : "";
            if (!name.EndsWith(" "))
                name += " ";
            name += nu.LastName != null && !nu.LastName.Trim().Equals("") ? nu.LastName : "";
            this.manager.SetUserProperty(this.adminUID, this.adminPWD, nu.UserName, name != null ? name.Trim() : null, null, null,
                nu.Address, nu.LocalityName, this.GetStateID(nu.StateUSPSCode), nu.ZipCode, nu.PhoneNumber, null, null, null, null, null,
                DateTime.Now.ToString());
             */
            /********************** Waiting for NAAS *************************/
            //StateId term = new StateId();
            //term.Name = this.state;
            if (nu.UserID < 0)
            {
                string pwd = new PasswordGenerator().Generate();
                nu.Password = pwd;
                nu.ChangePWDFlag = true;
                this.manager.AddUser(this.adminUID, this.adminPWD, nu.UserName, UserType.user, pwd, pwd, (StateId)Enum.Parse(typeof(StateId), this.state, true));
            }
            else if (nu.ChangePWDFlag)
            {
                string pwd = "";
                if (nu.Password != null && nu.Password != "")
                    pwd = nu.Password;
                else
                    pwd = new PasswordGenerator().Generate();
                nu.Password = pwd;
                this.manager.UpdateUser(this.adminUID, this.adminPWD, nu.UserName, UserType.user, pwd, string.Empty, (StateId)Enum.Parse(typeof(StateId), this.state, true));
                nu.ChangePWDFlag = false;
            }
            ArrayList listToAdd = new ArrayList(nu.OperationIDs);
            ArrayList listToRemove = new ArrayList();
            PolicyManager polManager = new PolicyManager();
            PolicyInfo[] policies = polManager.GetPolicyList(nu.UserName);
            if (policies != null && policies.Length > 0)
            {
                string[] opNames = new string[policies.Length];
                string[] wsNames = new string[policies.Length];
                for (int i = 0; i < policies.Length; i++)
                {
                    opNames[i] = policies[i].Request;
                    wsNames[i] = policies[i].Method;
                }
                int[] ids = new DBManager().GetOperationsDB().GetOperationIDs(opNames, wsNames);
                for (int i = 0; ids != null && i < ids.Length; i++)
                {
                    if (listToAdd.Contains(ids[i]))
                        listToAdd.Remove(ids[i]);
                    else
                        listToRemove.Add(ids[i]);
                }
            }
            IOperations opDB = new DBManager().GetOperationsDB();
            if (listToAdd.Count > 0)
            {
                int[] ids = new int[listToAdd.Count];
                for (int i = 0; i < ids.Length; i++)
                    ids[i] = int.Parse(listToAdd[i].ToString());
                string[][] names = opDB.GetOpNamesWSNames(ids);
                for (int i = 0; i < names[0].Length; i++)
                    polManager.SetPolicy(nu.UserName, names[1][i], names[0][i]);
            }
            if (listToRemove.Count > 0)
            {
                int[] ids = new int[listToRemove.Count];
                for (int i = 0; i < ids.Length; i++)
                    ids[i] = int.Parse(listToRemove[i].ToString());
                string[][] names = opDB.GetOpNamesWSNames(ids);
                for (int i = 0; i < names[0].Length; i++)
                    polManager.RemovePolicy(nu.UserName, names[1][i], names[0][i]);
            }
        }

        /// <summary>
        /// Delete NAAS user
        /// </summary>
        /// <param name="nu"></param>
        /// <returns></returns>
        public string DeleteUser(NAASUser nu)
        {
            string deleteUserResult = "";
            if (nu.UserID >= 0)
            {
                try
                {
                    bool deletePolicyResult = this.DeletePolicy(nu);
                    if (deletePolicyResult)
                        deleteUserResult = this.manager.DeleteUser(this.adminUID, this.adminPWD, nu.UserName);
                    else
                        deleteUserResult = "Cannot delete NAAS user.";
                }
                catch (Exception ex)
                {
                    deleteUserResult = ex.ToString();
                }
            }
            else
            {
                deleteUserResult = "No such NAAS user existed.";
            }
            return deleteUserResult;
        }

        /// <summary>
        /// Change NAAS user password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPWD"></param>
        /// <param name="newPWD"></param>
        /// <returns></returns>
        public bool ChangePassword(string userID, string oldPWD, string newPWD)
        {
            bool success = false;
            try
            {
                this.manager.ChangePwd(userID, oldPWD, newPWD);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Delete NAAS user policy
        /// </summary>
        /// <param name="nu"></param>
        public bool DeletePolicy(NAASUser nu)
        {
            bool deleteResult = false;
            try
            {
                ArrayList listToRemove = new ArrayList(nu.OperationIDs);
                if (listToRemove.Count > 0)
                {
                    int[] ids = new int[listToRemove.Count];
                    for (int i = 0; i < ids.Length; i++)
                        ids[i] = int.Parse(listToRemove[i].ToString());
                    IOperations opDB = new DBManager().GetOperationsDB();
                    string[][] names = opDB.GetOpNamesWSNames(ids);
                    PolicyManager polManager = new PolicyManager();
                    for (int i = 0; i < names[0].Length; i++)
                        polManager.RemovePolicy(nu.UserName, names[1][i], names[0][i]);
                }
                deleteResult = true;
            }
            catch (Exception )
            {
                deleteResult = false;
            }
            return deleteResult;
        }

        #endregion

        #region Private Fields

        Node.Core.NAAS.UserManagment.UserManager manager = null;
        string adminUID = null;
        string adminPWD = null;
        //StateId state;
        string state;

        #endregion

        #region Private Methods

        ///// <summary>
        ///// Get Node.Core.NAASUserManager.StateId enum value
        ///// </summary>
        ///// <param name="stateName">State Name (ENFO, MI, NJ, TX)</param>
        ///// <returns></returns>
        //private StateId GetStateID(string stateName)
        //{
        //    if (stateName != null && !stateName.Trim().Equals(""))
        //    {
        //        Array list = Enum.GetValues(typeof(StateId));
        //        foreach (object obj in list)
        //        {
        //            StateId id = (StateId)obj;
        //            if (id.ToString().Equals(stateName))
        //                return id;
        //        }
        //    }
        //    return StateId.Item;
        //}

        #endregion
    }
}
