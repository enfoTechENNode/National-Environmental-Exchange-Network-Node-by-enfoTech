using System;

using Node.Core.Biz.Objects;
using Node.Core.NAASPolicy;
using Node.Core.Logging;

namespace Node.Core.Biz.NAAS
{
    /// <summary>
    /// This class is used to access NAAS Policy Management Web Service Interface
    /// </summary>
    public class PolicyManager
    {
        #region Public Constructors

        /// <summary>
        /// Creates an Instance of an Object that Can be used to call NAAS Policy Management Web Services
        /// </summary>
        public PolicyManager()
        {
            SystemConfiguration config = new SystemConfiguration();
            string naasURL = config.GetNAASPolicyManagementAddress();
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
                this.manager = new Node.Core.NAAS.PolicyManagement.PolicyManager(naasURL, proxyServer, proxyUID, proxyPWD);
            }
            this.adminUID = config.GetNodeAdministratorUserID();
            this.adminPWD = config.GetNodeAdministratorPassword();
            this.node = this.GetNodeID(config.GetNodeName());
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Gets Policy list from NAAS.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns></returns>
        public PolicyInfo[] GetPolicyList(string subject)
        {
            string temp = subject;
            if (temp == null || temp.Trim().Equals(""))
                temp = "";
            return this.manager.GetPolicyList(this.adminUID, this.adminPWD, temp, "0", "-1");
        }
        /// <summary>
        /// Updates NAAS Policy list.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="method">The web method.</param>
        /// <param name="request">The request name.</param>
        public void SetPolicy(string subject, string method, string request)
        {
            this.manager.SetPolicy(this.adminUID, this.adminPWD, subject, this.GetMethod(method), request, "", ActionType.Permit);
        }
        /// <summary>
        /// Remove Policy from NAAS.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="method">The web method.</param>
        /// <param name="request">The request name.</param>
        public void RemovePolicy(string subject, string method, string request)
        {
            this.manager.DeletePolicy(this.adminUID, this.adminPWD, subject, this.GetMethod(method), request, "");
        }
        /// <summary>
        /// Get Explicit Right from NAAS from NAAS.
        /// </summary>
        /// <param name="op">The operation.</param>
        /// <param name="Error">Retrun the error message when NAAS web servcie call is failed</param>
        public bool GetExplicitRightFromNAAS(Operation op, out string Error)
        {
            bool bPolicyExisted = false;
            Error = "";
            try
            {
                PolicyInfo[] policies = this.manager.GetPolicyList(this.adminUID, this.adminPWD, "any", "0", "-1");

                foreach (PolicyInfo p in policies)
                {
                    if (p.Request.Trim().ToLower() == op.Name.Trim().ToLower()
                        && p.Method.Trim().ToLower() == op.WebServiceName.Trim().ToLower()
                        && p.Action.Trim().ToLower() == ActionType.Deny.ToString().Trim().ToLower())
                    {
                        bPolicyExisted = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Error = "Unable to retireve current explicit NAAS settings form NAAS";
                Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
                logger.Log(ex);
            }
            return bPolicyExisted;
        }
        /// <summary>
        /// Set Explicit Right policy in the NAAS.
        /// </summary>
        /// <param name="op">The operation.</param>
        /// <param name="Error">Retrun the error message when NAAS web servcie call is failed</param>
        public bool SetExplicitRightFromNAAS(Operation op, out string Error)
        {
            bool bPolicyExisted = false;
            Error = "";

            try
            {
                this.manager.SetPolicy(this.adminUID,this.adminPWD,"any",this.GetMethod(op.WebServiceName),op.Name,"",ActionType.Deny);
                bPolicyExisted = true;
            }
            catch (Exception ex)
            {
                Error = "Unable to retireve current explicit NAAS settings form NAAS";
                Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
                logger.Log(ex);
            }

            return bPolicyExisted;
        }


        #endregion

        #region Private Fields

        private Node.Core.NAAS.PolicyManagement.PolicyManager manager = null;
        private string adminUID = null;
        private string adminPWD = null;
        private NodeId node;

        #endregion

        #region Private Methods

        /// <summary>
        /// Get Node.Core.NAASPolicyManager.NodeId enum value
        /// </summary>
        /// <param name="nodeName">Node Name (ENFO, MI, NJ, TX)</param>
        /// <returns></returns>
        private NodeId GetNodeID(string nodeName)
        {
            if (nodeName != null && !nodeName.Trim().Equals(""))
            {
                Array list = Enum.GetValues(typeof(NodeId));
                foreach (object obj in list)
                {
                    NodeId id = (NodeId)obj;
                    if (id.Equals(nodeName))
                        return id;
                }
            }
            return NodeId.Item;
        }

        private MethodName GetMethod(string method)
        {
            MethodName mn;
            switch (method.ToUpper())
            {
                case Phrase.WEB_SERVICE_AUTHENTICATE:
                    mn = MethodName.Authenticate;
                    break;
                case Phrase.WEB_SERVICE_DOWNLOAD:
                    mn = MethodName.Download;
                    break;
                case Phrase.WEB_SERVICE_GETSERVICES:
                    mn = MethodName.GetServices;
                    break;
                case Phrase.WEB_SERVICE_GETSTATUS:
                    mn = MethodName.GetStatus;
                    break;
                case Phrase.WEB_SERVICE_NOTIFY:
                    mn = MethodName.Notify;
                    break;
                case Phrase.WEB_SERVICE_QUERY:
                    mn = MethodName.Query;
                    break;
                case Phrase.WEB_SERVICE_SOLICIT:
                    mn = MethodName.Solicit;
                    break;
                case Phrase.WEB_SERVICE_SUBMIT:
                    mn = MethodName.Submit;
                    break;
                default:
                    mn = MethodName.Any;
                    break;
            }
            return mn;
        }

        #endregion
    }
}
