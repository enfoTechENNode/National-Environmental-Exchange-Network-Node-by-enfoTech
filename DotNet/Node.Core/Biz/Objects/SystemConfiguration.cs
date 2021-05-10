using System;
using System.Collections;
using System.Xml;
using System.Web;

using Node.Lib.Security;

using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Logging;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// SystemConfiguration is used to access System Configuration XML file. 
    /// </summary>
    public class SystemConfiguration
    {
        private XmlDocument Config = null;
        /// <summary>
        /// Constructor of SystemConfiguration.
        /// </summary>
        public SystemConfiguration()
        {
            IConfigurations configDB = new DBManager().GetConfigurationsDB();
            this.Config = configDB.GetSystemConfig();
            bool bAdd = false;

            if (this.Config.GetElementsByTagName("NodeSettings2").Count == 0)
            {
                XmlNode newNode = this.Config.CreateNode(XmlNodeType.Element, "NodeSettings2", "");
                this.Config.DocumentElement.AppendChild(newNode);
                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration/NodeSettings");
                foreach (XmlNode aNode in sourceNode.ChildNodes)
                {
                    newNode.AppendChild(aNode.CloneNode(true));
                }
                bAdd = true;
            }
            if (this.Config.GetElementsByTagName("ClientSettings2").Count == 0)
            {
                XmlNode newNode = this.Config.CreateNode(XmlNodeType.Element, "ClientSettings2", "");
                this.Config.DocumentElement.AppendChild(newNode);
                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration/ClientSettings");
                //foreach (XmlNode aNode in sourceNode.ChildNodes)
                //{
                //    newNode.AppendChild(aNode.CloneNode(true));
                //}
                bAdd = true;
            }
            if (this.Config.GetElementsByTagName("DefaultSettings").Count == 0)
            {
                XmlNode newNode = this.Config.CreateNode(XmlNodeType.Element, "DefaultSettings", "");
                XmlNode DefaultRowNumberNode = this.Config.CreateElement("DefaultRowNumber");
                DefaultRowNumberNode.InnerText = "100";
                newNode.AppendChild(DefaultRowNumberNode);
                XmlNode DefaultTopNumberNode = this.Config.CreateElement("DefaultTopNumber");
                DefaultTopNumberNode.InnerText = "5";
                newNode.AppendChild(DefaultTopNumberNode);
                XmlNode DefaultPageSizeNode = this.Config.CreateElement("DefaultPageSize");
                DefaultPageSizeNode.InnerText = "15";
                newNode.AppendChild(DefaultPageSizeNode);

                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration");
                sourceNode.AppendChild(newNode);
                bAdd = true;
            }
            if (this.Config.GetElementsByTagName("FavoriteLinks").Count == 0)
            {
                XmlNode root = this.Config.SelectSingleNode("/Configuration");
                XmlNode sourceNode = this.Config.CreateElement("FavoriteLinks");
                
                XmlNode fLink = this.Config.CreateElement("FavoriteLink");
                XmlAttribute fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Node Registration";
                fLink.Attributes.Append(fLinkName);
                XmlAttribute fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\Registration\NodeRegistration.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Node Registration";
                sourceNode.AppendChild(fLink);

                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Node User";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\User\SearchUsers.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Node User Management";
                sourceNode.AppendChild(fLink);

                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Node Client";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"http://www.enfotech.com";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Node Client";
                sourceNode.AppendChild(fLink);

                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Operation Manager";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\OperationManager\ManageOperation.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Manage Operations";
                sourceNode.AppendChild(fLink);

                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "AQS Data Management";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\Clients\AQS\PreValidationSelection.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "AQS Data Management";
                sourceNode.AppendChild(fLink);

                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Data Viewer";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\DataViewer\DataViewer.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Data Viewer";
                sourceNode.AppendChild(fLink);

                // WI 20965
                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "ICIS Data Management";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\Clients\ICIS\ManageICIS.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "ICIS Data Management";
                sourceNode.AppendChild(fLink);

                // DNRC Enhancement
                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Submit Operation Manager";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\Clients\DNREC\TasksList.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Submit Operation Manager";
                sourceNode.AppendChild(fLink);

                root.AppendChild(sourceNode);

                // Open Dump Enhancement
                fLink = this.Config.CreateElement("FavoriteLink");
                fLinkName = this.Config.CreateAttribute("name");
                fLinkName.InnerText = "Waste/Open Dump Management";
                fLink.Attributes.Append(fLinkName);
                fLinkURL = this.Config.CreateAttribute("url");
                fLinkURL.InnerText = @"..\Clients\OD\ODSearch.aspx";
                fLink.Attributes.Append(fLinkURL);
                fLink.InnerText = "Waste/Open Dump Management";
                sourceNode.AppendChild(fLink);

                root.AppendChild(sourceNode);

                bAdd = true;

            }
            else if (this.Config.GetElementsByTagName("FavoriteLinks").Count > 0) 
            {
                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration/FavoriteLinks");
                XmlNode fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Node Registration']");
                XmlAttribute fLinkName = null;
                XmlAttribute fLinkURL = null;
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Node Registration";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\Registration\NodeRegistration.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Node Registration";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Node User']");
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Node User";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\User\SearchUsers.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Node User Management";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Node Client']");
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Node Client";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"http://www.enfotech.com";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Node Client";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Operation Manager']");
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Operation Manager";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\OperationManager\ManageOperation.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Manage Operations";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='AQS Data Management']");
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "AQS Data Management";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\Clients\AQS\PreValidationSelection.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "AQS Data Management";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Data Viewer']");
                if (fLink == null)
                { 
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Data Viewer";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\DataViewer\DataViewer.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Data Viewer";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }

                // WI 20965
                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='ICIS Data Management']");
                if (fLink == null)
                {
                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "ICIS Data Management";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\Clients\ICIS\ManageICIS.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "ICIS Data Management";
                    sourceNode.AppendChild(fLink);
                    bAdd = true;
                }

                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Submit Operation Manager']");
                if (fLink == null)
                {

                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Submit Operation Manager";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\Clients\DNREC\TasksList.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Submit Operation Manager";
                    sourceNode.AppendChild(fLink);
                    
                    bAdd = true;
                }

                fLink = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name='Waste/Open Dump Management']");
                if (fLink == null)
                {

                    fLink = this.Config.CreateElement("FavoriteLink");
                    fLinkName = this.Config.CreateAttribute("name");
                    fLinkName.InnerText = "Waste/Open Dump Management";
                    fLink.Attributes.Append(fLinkName);
                    fLinkURL = this.Config.CreateAttribute("url");
                    fLinkURL.InnerText = @"..\Clients\OD\ODSearch.aspx";
                    fLink.Attributes.Append(fLinkURL);
                    fLink.InnerText = "Waste/Open Dump Management";
                    sourceNode.AppendChild(fLink);

                    bAdd = true;
                }

            }

            if (this.Config.GetElementsByTagName("RestfulService").Count == 0)
            {
                XmlNode newNode = this.Config.CreateNode(XmlNodeType.Element, "RestfulService", "");
                XmlNode IntroductionNode = this.Config.CreateElement("Introduction");
                newNode.AppendChild(IntroductionNode);

                XmlNode HeaderNode = this.Config.CreateElement("Header");
                HeaderNode.InnerText = "RESTful Services Page Header";
                IntroductionNode.AppendChild(HeaderNode);
                XmlNode ContentNode = this.Config.CreateElement("Content");
                ContentNode.InnerText = "RESTful Services Page Description";
                IntroductionNode.AppendChild(ContentNode);

                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration");
                sourceNode.AppendChild(newNode);
                bAdd = true;

            }

            if (this.Config.GetElementsByTagName("From").Count == 0)
            {
                
                XmlNode sourceNode = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer");
                XmlNode newNode = this.Config.CreateElement("From");
                newNode.InnerText = "nodeadmin@enfotech.com";
                sourceNode.AppendChild(newNode);

                bAdd = true;
            }

            if (bAdd)
            {
                configDB.UpdateSystemConfig(this.Config);
            }

        }
        /// <summary>
        /// Save SystemConfiguration.
        /// </summary>
        /// <returns>True if success.</returns>
        public bool SaveConfiguration()
        {
            return new DBManager().GetConfigurationsDB().UpdateSystemConfig(this.Config);
        }

        //public ArrayList GetClientURLs()
        //{
        //    ArrayList retList = new ArrayList();
        //    XmlNodeList clientNodeList = this.Config.SelectNodes("/Configuration/ClientSettings/WebServicesURL");
        //    foreach (XmlNode clientNode in clientNodeList)
        //    {
        //        retList.Add(clientNode.InnerText);
        //    }
        //    return retList;
        //}  

        #region ProxySettings
        private XmlNode GetProxyNode()
        {
            XmlNode proxyNode = this.Config.SelectSingleNode("/Configuration/ProxySettings");
            if (proxyNode != null)
            {
                XmlAttribute statusAttr = proxyNode.Attributes["status"];
                if (statusAttr != null && statusAttr.Value.Equals("A"))
                    return proxyNode;
            }
            return null;
        }
        /// <summary>
        /// Returns proxy host.
        /// </summary>
        /// <returns></returns>
        public string GetProxyHost()
        {
            string retHost = null;
            XmlNode proxyNode = this.GetProxyNode();
            if (proxyNode != null)
                retHost = proxyNode.Attributes["host"].Value;
            return retHost;
        }
        /// <summary>
        /// Returns proxy user id.
        /// </summary>
        /// <returns></returns>
        public string GetProxyUID()
        {
            string uid = null;
            XmlNode proxyNode = this.GetProxyNode();
            if (proxyNode != null)
            {
                XmlNode uidNode = proxyNode.SelectSingleNode("Credentials/UserID");
                if (uidNode != null)
                    uid = uidNode.InnerText;
            }
            return uid;
        }
        /// <summary>
        /// Returns proxy password.
        /// </summary>
        /// <returns></returns>
        public string GetProxyPWD()
        {
            string pwd = null;
            XmlNode proxyNode = this.GetProxyNode();
            if (proxyNode != null)
            {
                XmlNode pwdNode = proxyNode.SelectSingleNode("Credentials/Password");
                if (pwdNode != null)
                    pwd = new Cryptography().Decrypting(pwdNode.InnerText, Phrase.CryptKey);
            }
            return pwd;
        }
        /// <summary>
        /// Enable proxy option.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="uid">The user id.</param>
        /// <param name="pwd">The password.</param>
        /// <returns></returns>
        public string EnableProxy(string host, string uid, string pwd)
        {
            if (host == null || host.Trim().Equals(""))
                return "Proxy Host must be non-empty";
            XmlNode proxyNode = this.Config.SelectSingleNode("/Configuration/ProxySettings");
            proxyNode.Attributes["status"].Value = "A";
            proxyNode.Attributes["host"].Value = host;
            if (uid == null || uid.Trim().Equals(""))
                uid = "";
            proxyNode.SelectSingleNode("Credentials/UserID").InnerText = uid;
            if (pwd != null && !pwd.Trim().Equals(""))
            {
                Cryptography crypt = new Cryptography();
                proxyNode.SelectSingleNode("Credentials/Password").InnerText = crypt.Encrypting(pwd, Phrase.CryptKey);
            }
            return "";
        }
        /// <summary>
        /// Disable proxy option.
        /// </summary>
        public void DisableProxy()
        {
            XmlNode proxyNode = this.Config.SelectSingleNode("/Configuration/ProxySettings");
            proxyNode.Attributes["status"].Value = "I";
            proxyNode.Attributes["host"].Value = "";
            proxyNode.SelectSingleNode("Credentials/UserID").InnerText = "";
            proxyNode.SelectSingleNode("Credentials/Password").InnerText = "";
        }

        #endregion ProxySettings

        #region NodeSetting
        /// <summary>
        /// Returns node status.
        /// </summary>
        /// <returns>node status value</returns>
        public string GetNodeStatus()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Status").InnerText;
        }
        /// <summary>
        /// Updates node status.
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeStatus(string status)
        {
            if (status == null || (!status.Equals(Phrase.STATUS_RUNNING) && !status.Equals(Phrase.STATUS_STOPPED)))
                return "Illegal Node Status";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Status").InnerText = status;
            return "";
        }
        /// <summary>
        /// Returns node token life time.
        /// </summary>
        /// <returns>node token life time.</returns>
        public string GetTokenLifeTime()
        {
            string tokenLifeTime = "";
            XmlNode tokenLifeTimeNode = this.Config.SelectSingleNode("/Configuration/NodeSettings/TokenLifeTime");
            if (tokenLifeTimeNode != null && tokenLifeTimeNode.Attributes["Enabled"] != null && tokenLifeTimeNode.Attributes["Enabled"].Value.Equals("true"))
                tokenLifeTime = tokenLifeTimeNode.Attributes["time"].Value;
            return tokenLifeTime;
        }
        /// <summary>
        /// Updates node token life time.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Error message if fail</returns>
        public string SetTokenLifeTime(string value)
        {
            int tokenLifeTime = -1;
            try
            {
                if (value != null && !value.Trim().Equals(""))
                    tokenLifeTime = int.Parse(value);
            }
            catch (Exception)
            {
                return "Token Life Time must be an integer";
            }
            XmlNode tokenLifeTimeNode = this.Config.SelectSingleNode("/Configuration/NodeSettings/TokenLifeTime");
            XmlAttribute enabledAttr = tokenLifeTimeNode.Attributes["Enabled"];
            if (enabledAttr == null)
            {
                enabledAttr = this.Config.CreateAttribute("Enabled");
                tokenLifeTimeNode.Attributes.Append(enabledAttr);
            }
            XmlAttribute timeAttr = tokenLifeTimeNode.Attributes["time"];
            if (timeAttr == null)
            {
                timeAttr = this.Config.CreateAttribute("time");
                tokenLifeTimeNode.Attributes.Append(timeAttr);
            }
            if (tokenLifeTime >= 0)
            {
                enabledAttr.Value = "true";
                timeAttr.Value = "" + tokenLifeTime;
            }
            else
            {
                enabledAttr.Value = "false";
                timeAttr.Value = "";
            }
            return "";
        }
        /// <summary>
        /// Returns node name.
        /// </summary>
        /// <returns>node name.</returns>
        public string GetNodeName()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings").Attributes["name"].Value;
        }
        /// <summary>
        /// Updates node name.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeName(string name)
        {
            string result = "";
            if (name != null && !name.Trim().Equals(""))
            {
                XmlNode nodeSettingsNode = this.Config.SelectSingleNode("/Configuration/NodeSettings");
                XmlAttribute nodeNameAttr = nodeSettingsNode.Attributes["name"];
                if (nodeNameAttr == null)
                {
                    nodeNameAttr = this.Config.CreateAttribute("name");
                    nodeSettingsNode.Attributes.Append(nodeNameAttr);
                }
                nodeNameAttr.Value = name;
            }
            else
                result = "Illegal Node Name";
            return result;
        }
        /// <summary>
        /// Returns node status message.
        /// </summary>
        /// <returns>node status message.</returns>
        public string GetNodeStatusMessage()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Message").InnerText;
        }
        /// <summary>
        /// Updates node status message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Error message if fail</returns>
        public string SetNodeStatusMessage(string message)
        {
            if (message != null && !message.Trim().Equals(""))
                this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Message").InnerText = message;
            else
                this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeStatus/Message").InnerText = "";
            return "";
        }
        /// <summary>
        /// Returns node address.
        /// </summary>
        /// <returns>node address.</returns>
        public string GetNodeAddress()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeURL").InnerText;
        }
        /// <summary>
        /// Updates node address
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAddress(string url)
        {
            if (url != null && !url.Trim().Equals(""))
                this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeURL").InnerText = url;
            else
                this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeURL").InnerText = "";
            return "";
        }
        /// <summary>
        /// Returns NAAS authentication address.
        /// </summary>
        /// <returns>NAAS authentication address.</returns>
        public string GetNAASAuthenticationAddress()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'authentication']").InnerText;
        }
        /// <summary>
        /// Updates NAAS authentication address.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASAuthenticationAddress(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS Authentication Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'authentication']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns NAASUser Management Address.
        /// </summary>
        /// <returns>NAASUser Management Address</returns>
        public string GetNAASUserManagementAddress()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'user']").InnerText;
        }
        /// <summary>
        /// Updates NAASUser Management Address.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASUserManagementAddress(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS User Management Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'user']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns NAASPolicy Management Address.
        /// </summary>
        /// <returns>NAASPolicy Management Address</returns>
        public string GetNAASPolicyManagementAddress()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'policy']").InnerText;
        }
        /// <summary>
        /// Updates NAASPolicy Management Address.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASPolicyManagementAddress(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS Policy Management Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NAAS/URL[@name = 'policy']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator Name.
        /// </summary>
        /// <returns></returns>
        public string GetNodeAdministratorName()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Name").InnerText;
        }
        /// <summary>
        /// Updates Node Administrator Name.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorName(string name)
        {
            if (name == null || name.Trim().Equals(""))
                name = "";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Name").InnerText = name;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator UserID.
        /// </summary>
        /// <returns>Node Administrator UserID</returns>
        public string GetNodeAdministratorUserID()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Credentials/UserID").InnerText;
        }
        /// <summary>
        /// Updates Node Administrator UserID.
        /// </summary>
        /// <param name="userID">userID</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorUserID(string userID)
        {
            if (userID == null || userID.Trim().Equals(""))
                return "Node Administrator User ID must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Credentials/UserID").InnerText = userID;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator Password .
        /// </summary>
        /// <returns>Node Administrator Password</returns>
        public string GetNodeAdministratorPassword()
        {
            string retPwd = "";
            string pwd = this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Credentials/Password").InnerText;
            if (pwd != null && !pwd.Trim().Equals(""))
            {
                try
                {
                    Cryptography crypt = new Cryptography();
                    retPwd = crypt.Decrypting(pwd, Phrase.CryptKey);
                }
                catch (Exception)
                {
                }
            }
            return retPwd;
        }
        /// <summary>
        /// Updates Node Administrator Password.
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorPassword(string pwd)
        {
            if (pwd == null || pwd.Trim().Equals(""))
                return "";
            Cryptography crypt = new Cryptography();
            this.Config.SelectSingleNode("/Configuration/NodeSettings/NodeAdministrator/Credentials/Password").InnerText = crypt.Encrypting(pwd, Phrase.CryptKey);
            return "";
        }

        #endregion NodeSetting

        #region NodeSetting2
        /// <summary>
        /// Returns Node Status V2.
        /// </summary>
        /// <returns>Node Status V2</returns>
        public string GetNodeStatus_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeStatus/Status").InnerText;
        }
        /// <summary>
        /// Updates Node Status V2.
        /// </summary>
        /// <param name="status">status</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeStatus_V2(string status)
        {
            if (status == null || (!status.Equals(Phrase.STATUS_RUNNING) && !status.Equals(Phrase.STATUS_STOPPED)))
                return "Illegal Node Status";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeStatus/Status").InnerText = status;
            return "";
        }
        /// <summary>
        /// Returns Token LifeTime V2.
        /// </summary>
        /// <returns>Token LifeTime V2</returns>
        public string GetTokenLifeTime_V2()
        {
            string tokenLifeTime = "";
            XmlNode tokenLifeTimeNode = this.Config.SelectSingleNode("/Configuration/NodeSettings2/TokenLifeTime");
            if (tokenLifeTimeNode != null && tokenLifeTimeNode.Attributes["Enabled"] != null && tokenLifeTimeNode.Attributes["Enabled"].Value.Equals("true"))
                tokenLifeTime = tokenLifeTimeNode.Attributes["time"].Value;
            return tokenLifeTime;
        }
        /// <summary>
        /// Updates  Token LifeTime V2.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Error message if fail</returns>
        public string SetTokenLifeTime_V2(string value)
        {
            int tokenLifeTime = -1;
            try
            {
                if (value != null && !value.Trim().Equals(""))
                    tokenLifeTime = int.Parse(value);
            }
            catch (Exception)
            {
                return "Token Life Time must be an integer";
            }
            XmlNode tokenLifeTimeNode = this.Config.SelectSingleNode("/Configuration/NodeSettings2/TokenLifeTime");
            XmlAttribute enabledAttr = tokenLifeTimeNode.Attributes["Enabled"];
            if (enabledAttr == null)
            {
                enabledAttr = this.Config.CreateAttribute("Enabled");
                tokenLifeTimeNode.Attributes.Append(enabledAttr);
            }
            XmlAttribute timeAttr = tokenLifeTimeNode.Attributes["time"];
            if (timeAttr == null)
            {
                timeAttr = this.Config.CreateAttribute("time");
                tokenLifeTimeNode.Attributes.Append(timeAttr);
            }
            if (tokenLifeTime >= 0)
            {
                enabledAttr.Value = "true";
                timeAttr.Value = "" + tokenLifeTime;
            }
            else
            {
                enabledAttr.Value = "false";
                timeAttr.Value = "";
            }
            return "";
        }
        /// <summary>
        /// Returns Node Name V2.
        /// </summary>
        /// <returns>Node Name V2</returns>
        public string GetNodeName_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings").Attributes["name"].Value;
        }
        /// <summary>
        /// Updates Node Name V2.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeName_V2(string name)
        {
            string result = "";
            if (name != null && !name.Trim().Equals(""))
            {
                XmlNode nodeSettingsNode = this.Config.SelectSingleNode("/Configuration/NodeSettings");
                XmlAttribute nodeNameAttr = nodeSettingsNode.Attributes["name"];
                if (nodeNameAttr == null)
                {
                    nodeNameAttr = this.Config.CreateAttribute("name");
                    nodeSettingsNode.Attributes.Append(nodeNameAttr);
                }
                nodeNameAttr.Value = name;
            }
            else
                result = "Illegal Node Name";
            return result;
        }
        /// <summary>
        /// Returns Node Status Message V2.
        /// </summary>
        /// <returns>Node Status Message V2</returns>
        public string GetNodeStatusMessage_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeStatus/Message").InnerText;
        }
        /// <summary>
        /// Updates Node Status Message V2.
        /// </summary>
        /// <param name="message">message</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeStatusMessage_V2(string message)
        {
            if (message != null && !message.Trim().Equals(""))
                this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeStatus/Message").InnerText = message;
            else
                this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeStatus/Message").InnerText = "";
            return "";
        }
        /// <summary>
        /// Returns Node Address V2.
        /// </summary>
        /// <returns>Node Address V2</returns>
        public string GetNodeAddress_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeURL").InnerText;
        }
        /// <summary>
        /// Updates Node Address V2.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAddress_V2(string url)
        {
            if (url != null && !url.Trim().Equals(""))
                this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeURL").InnerText = url;
            else
                this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeURL").InnerText = "";
            return "";
        }
        /// <summary>
        /// Returns NAAS Authentication Address V2.
        /// </summary>
        /// <returns>NAAS Authentication Address V2</returns>
        public string GetNAASAuthenticationAddress_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'authentication']").InnerText;
        }
        /// <summary>
        /// Updates NAAS Authentication Address V2.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASAuthenticationAddress_V2(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS Authentication Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'authentication']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns NAAS User Management Address V2.
        /// </summary>
        /// <returns>NAAS User Management Address V2</returns>
        public string GetNAASUserManagementAddress_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'user']").InnerText;
        }
        /// <summary>
        /// Updates NAAS User Management Address V2.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASUserManagementAddress_V2(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS User Management Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'user']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns NAAS Policy Management Address V2.
        /// </summary>
        /// <returns>NAAS Policy Management Address V2</returns>
        public string GetNAASPolicyManagementAddress_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'policy']").InnerText;
        }
        /// <summary>
        /// Updates NAAS Policy Management Address V2.
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>Error message if fail</returns>
        public string SetNAASPolicyManagementAddress_V2(string url)
        {
            if (url == null || url.Trim().Equals(""))
                return "NAAS Policy Management Server Address must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NAAS/URL[@name = 'policy']").InnerText = url;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator Name V2.
        /// </summary>
        /// <returns>Node Administrator Name V2</returns>
        public string GetNodeAdministratorName_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Name").InnerText;
        }
        /// <summary>
        /// Updates Node Administrator Name V2.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorName_V2(string name)
        {
            if (name == null || name.Trim().Equals(""))
                name = "";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Name").InnerText = name;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator UserID V2.
        /// </summary>
        /// <returns>Node Administrator UserID V2</returns>
        public string GetNodeAdministratorUserID_V2()
        {
            return this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Credentials/UserID").InnerText;
        }
        /// <summary>
        /// Updates Node Administrator UserID V2.
        /// </summary>
        /// <param name="userID">userID</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorUserID_V2(string userID)
        {
            if (userID == null || userID.Trim().Equals(""))
                return "Node Administrator User ID must be non-empty";
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Credentials/UserID").InnerText = userID;
            return "";
        }
        /// <summary>
        /// Returns Node Administrator Password V2.
        /// </summary>
        /// <returns>Node Administrator Password V2.</returns>
        public string GetNodeAdministratorPassword_V2()
        {
            string retPwd = "";
            string pwd = this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Credentials/Password").InnerText;
            if (pwd != null && !pwd.Trim().Equals(""))
            {
                try
                {
                    Cryptography crypt = new Cryptography();
                    retPwd = crypt.Decrypting(pwd, Phrase.CryptKey);
                }
                catch (Exception)
                {
                }
            }
            return retPwd;
        }
        /// <summary>
        /// Updates Node Administrator Password V2.
        /// </summary>
        /// <param name="pwd">pwd</param>
        /// <returns>Error message if fail</returns>
        public string SetNodeAdministratorPassword_V2(string pwd)
        {
            if (pwd == null || pwd.Trim().Equals(""))
                return "";
            Cryptography crypt = new Cryptography();
            this.Config.SelectSingleNode("/Configuration/NodeSettings2/NodeAdministrator/Credentials/Password").InnerText = crypt.Encrypting(pwd, Phrase.CryptKey);
            return "";
        }

        #endregion NodeSetting2

        #region eMail Server
        /// <summary>
        /// Returns Email Server Host.
        /// </summary>
        /// <returns>Email Server Host.</returns>
        public string GetEmailServerHost()
        {
            XmlNode emailServerNode = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']");
            return emailServerNode.Attributes["host"].Value;
        }
        /// <summary>
        /// Updates Email Server Host.
        /// </summary>
        /// <param name="host">host</param>
        /// <returns>Error message if fail</returns>
        public string SetEmailServerHost(string host)
        {
            if (host == null || host.Trim().Equals(""))
                return "Email Server Host must be non-empty";
            XmlNode emailServerNode = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']");
            emailServerNode.Attributes["host"].Value = host;
            return "";
        }
        /// <summary>
        /// Returns Email Server Port.
        /// </summary>
        /// <returns>Email Server Port.</returns>
        public string GetEmailServerPort()
        {
            XmlNode emailServerNode = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']");
            return emailServerNode.Attributes["port"].Value;
        }
        /// <summary>
        /// Updates Email Server Port.
        /// </summary>
        /// <param name="port">port</param>
        /// <returns>Error message if fail</returns>
        public string SetEmailServerPort(string port)
        {
            if (port == null || port.Trim().Equals(""))
                return "Email Server Port must be non-empty";
            XmlNode emailServerNode = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']");
            emailServerNode.Attributes["port"].Value = port;
            return "";
        }
        /// <summary>
        /// Returns Email Server From.
        /// </summary>
        /// <returns>Email Server From.</returns>
        public string GetEmailServerFrom()
        {
            return this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/From").InnerText;
        }
        /// <summary>
        /// Updates Email Server From.
        /// </summary>
        /// <param name="from">from</param>
        /// <returns>Error message if fail</returns>
        public string SetEmailServerFrom(string from)
        {
            if (from == null || from.Trim().Equals(""))
                from = "";
            this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/From").InnerText = from;
            return "";
        }
        /// <summary>
        /// Returns Email ServerUser ID.
        /// </summary>
        /// <returns>Email ServerUser ID.</returns>
        public string GetEmailServerUserID()
        {
            return this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/Credentials/UserID").InnerText;
        }
        /// <summary>
        /// Updates Email ServerUser ID.
        /// </summary>
        /// <param name="userID">userID</param>
        /// <returns>Error message if fail</returns>
        public string SetEmailServerUserID(string userID)
        {
            if (userID == null || userID.Trim().Equals(""))
                userID = "";
            this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/Credentials/UserID").InnerText = userID;
            return "";
        }
        /// <summary>
        /// Returns Email Server Password.
        /// </summary>
        /// <returns>Email Server Password.</returns>
        public string GetEmailServerPassword()
        {
            string pwd = this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/Credentials/Password").InnerText;
            if (pwd != null && !pwd.Trim().Equals(""))
            {
                Cryptography crypt = new Cryptography();
                return crypt.Decrypting(pwd, Phrase.CryptKey);
            }
            return "";
        }
        /// <summary>
        /// Updates Email Server Password.
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>Error message if fail</returns>
        public string SetEmailServerPassword(string password)
        {
            if (password == null || password.Trim().Equals(""))
                password = "";
            if (!password.Equals(""))
            {
                Cryptography crypt = new Cryptography();
                this.Config.SelectSingleNode("/Configuration/AutoMail/EmailServer[@name='NODE' and @status='A']/Credentials/Password").InnerText = crypt.Encrypting(password, Phrase.CryptKey);
            }
            return "";
        }

        #endregion eMail Server

        #region LoggingSettings
        /// <summary>
        /// Gets logging level.
        /// </summary>
        /// <param name="application">Name of application.</param>
        /// <returns></returns>
        public int GetLoggingLevel(string application)
        {
            int level = Node.Core.Logging.Logger.LEVEL_FATAL;
            XmlNode levelNode = this.Config.SelectSingleNode("/Configuration/LoggingSettings/" + application + "Level");
            if (levelNode != null)
            {
                try
                {
                    level = int.Parse(levelNode.InnerText);
                }
                catch (Exception) { }
            }
            return level;
        }
        /// <summary>
        /// Returns Admin Log Level.
        /// </summary>
        /// <returns>Admin Log Level.</returns>
        public int GetAdminLogLevel()
        {
            int retInt = Logger.LEVEL_DEBUG;
            string s = this.Config.SelectSingleNode("/Configuration/LoggingSettings/AdminLevel").InnerText;
            if (s != null)
            {
                switch (s)
                {
                    case "1":
                        retInt = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        retInt = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        retInt = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        retInt = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        retInt = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            return retInt;
        }
        /// <summary>
        /// Updates Admin Log Level.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>Error message if fail</returns>
        public string SetAdminLogLevel(string input)
        {
            int level = Logger.LEVEL_DEBUG;
            if (input != null)
            {
                switch (input)
                {
                    case "1":
                        level = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        level = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        level = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        level = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        level = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            this.Config.SelectSingleNode("/Configuration/LoggingSettings/AdminLevel").InnerText = "" + level;
            return "";
        }
        /// <summary>
        /// Returns Client Log Level.
        /// </summary>
        /// <returns>Client Log Level.</returns>
        public int GetClientLogLevel()
        {
            int retInt = Logger.LEVEL_DEBUG;
            string s = this.Config.SelectSingleNode("/Configuration/LoggingSettings/ClientLevel").InnerText;
            if (s != null)
            {
                switch (s)
                {
                    case "1":
                        retInt = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        retInt = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        retInt = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        retInt = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        retInt = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            return retInt;
        }
        /// <summary>
        /// Updates Client Log Level.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>Error message if fail</returns>
        public string SetClientLogLevel(string input)
        {
            int level = Logger.LEVEL_DEBUG;
            if (input != null)
            {
                switch (input)
                {
                    case "1":
                        level = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        level = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        level = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        level = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        level = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            this.Config.SelectSingleNode("/Configuration/LoggingSettings/ClientLevel").InnerText = "" + level;
            return "";
        }
        /// <summary>
        /// Returns Task Log Level.
        /// </summary>
        /// <returns>Task Log Level.</returns>
        public int GetTaskLogLevel()
        {
            int retInt = Logger.LEVEL_DEBUG;
            string s = this.Config.SelectSingleNode("/Configuration/LoggingSettings/TaskLevel").InnerText;
            if (s != null)
            {
                switch (s)
                {
                    case "1":
                        retInt = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        retInt = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        retInt = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        retInt = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        retInt = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            return retInt;
        }
        /// <summary>
        /// Updates Task Log Level.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>Error message if fail</returns>
        public string SetTaskLogLevel(string input)
        {
            int level = Logger.LEVEL_DEBUG;
            if (input != null)
            {
                switch (input)
                {
                    case "1":
                        level = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        level = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        level = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        level = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        level = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            this.Config.SelectSingleNode("/Configuration/LoggingSettings/TaskLevel").InnerText = "" + level;
            return "";
        }
        /// <summary>
        /// Returns WS Log Level.
        /// </summary>
        /// <returns>WS Log Level.</returns>
        public int GetWSLogLevel()
        {
            int retInt = Logger.LEVEL_DEBUG;
            string s = this.Config.SelectSingleNode("/Configuration/LoggingSettings/WebServicesLevel").InnerText;
            if (s != null)
            {
                switch (s)
                {
                    case "1":
                        retInt = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        retInt = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        retInt = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        retInt = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        retInt = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            return retInt;
        }
        /// <summary>
        /// Updates WS Log Level.
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>Error message if fail</returns>
        public string SetWSLogLevel(string input)
        {
            int level = Logger.LEVEL_DEBUG;
            if (input != null)
            {
                switch (input)
                {
                    case "1":
                        level = Logger.LEVEL_FATAL;
                        break;
                    case "2":
                        level = Logger.LEVEL_ERROR;
                        break;
                    case "3":
                        level = Logger.LEVEL_WARN;
                        break;
                    case "4":
                        level = Logger.LEVEL_INFO;
                        break;
                    case "5":
                        level = Logger.LEVEL_DEBUG;
                        break;
                }
            }
            this.Config.SelectSingleNode("/Configuration/LoggingSettings/WebServicesLevel").InnerText = "" + level;
            return "";
        }

        #endregion LoggingSettings

        #region ClientSettings
        /// <summary>
        /// Returns Client WebService URLs.
        /// </summary>
        /// <returns>Client WebService URLs.</returns>
        public ArrayList GetClientWebServiceURLs()
        {
            ArrayList list = new ArrayList();
            XmlNodeList nodes = this.Config.SelectNodes("/Configuration/ClientSettings/WebServicesURL");
            foreach (XmlNode node in nodes)
                list.Add(node.InnerText);
            return list;
        }
        /// <summary>
        /// Updates Client WebService URLs.
        /// </summary>
        /// <param name="list">list</param>
        /// <returns>Error message if fail</returns>
        public string SetClientWebServicesURLs(ArrayList list)
        {
            XmlNode clientSettingsNode = this.Config.SelectSingleNode("/Configuration/ClientSettings");
            clientSettingsNode.RemoveAll();
            foreach (object obj in list)
            {
                XmlNode newNode = this.Config.CreateElement("WebServicesURL");
                newNode.InnerText = "" + obj;
                clientSettingsNode.AppendChild(newNode);
            }
            return "";
        }
        #endregion ClientSettings

        #region ClientSettings2
        /// <summary>
        /// Returns Client WebService URLs V2.
        /// </summary>
        /// <returns>Client WebService URLs V2.</returns>
        public ArrayList GetClientWebServiceURLs_V2()
        {
            ArrayList list = new ArrayList();
            XmlNodeList nodes = this.Config.SelectNodes("/Configuration/ClientSettings2/WebServicesURL");
            foreach (XmlNode node in nodes)
                list.Add(node.InnerText);
            return list;
        }
        /// <summary>
        /// Updates Client WebService URLs V2.
        /// </summary>
        /// <param name="list">list</param>
        /// <returns>Error message if fail</returns>
        public string SetClientWebServicesURLs_V2(ArrayList list)
        {
            XmlNode clientSettingsNode = this.Config.SelectSingleNode("/Configuration/ClientSettings2");
            clientSettingsNode.RemoveAll();
            foreach (object obj in list)
            {
                XmlNode newNode = this.Config.CreateElement("WebServicesURL");
                newNode.InnerText = "" + obj;
                clientSettingsNode.AppendChild(newNode);
            }
            return "";
        }
        #endregion ClientSettings2

        #region FavoriteLinks
        /// <summary>
        /// Gets All favorite links.
        /// </summary>
        /// <returns>A list of Favorite link.</returns>
        public ArrayList GetAllFavoriteLinks()
        {
            ArrayList retList = new ArrayList();
            XmlNodeList favoriteLinks = this.Config.SelectNodes("/Configuration/FavoriteLinks/FavoriteLink");

            foreach (XmlNode link in favoriteLinks)
            {
                string[] objLink = new string[3];
                objLink[0] = link.Attributes["name"].Value;
                objLink[1] = link.Attributes["url"].Value;
                objLink[2] = link.InnerText;
                retList.Add(objLink);
            }
            return retList;
        }
        /// <summary>
        /// Returns Favorite Link.
        /// </summary>
        /// <param name="linkName">The name of link</param>
        /// <returns>url,name</returns>
        public string[] GetFavoriteLink(string linkName)
        {
            string[] link = new string[2];
            XmlNode node = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name = '" + linkName + "']");
            if (node != null)
            {
                link[0] = node.Attributes["url"].Value;
                link[1] = node.InnerText;
            }
            return link;
        }
        /// <summary>
        /// Remove Favorite link.
        /// </summary>
        /// <param name="linkName">The name of link</param>
        public void RemoveFavoriteLink(string linkName)
        {
            XmlNode nodes = this.Config.SelectSingleNode("/Configuration/FavoriteLinks");
            XmlNode node = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name = '" + linkName + "']");
            if (nodes != null && node != null)
            {
                nodes.RemoveChild(node);
            }
        }
        /// <summary>
        /// Updates Favorite Link.
        /// </summary>
        /// <param name="linkName">The name of link</param>
        /// <param name="linkURL">The URL of link</param>
        /// <param name="desc">The description of link</param> 
        /// <returns>Error message if fail</returns>
        public void SetFavoriteLink(string linkName, string linkURL, string desc)
        {
            if (string.IsNullOrEmpty(linkName) || string.IsNullOrEmpty(linkURL))
            {
                return;
            }

            XmlNode theNode = this.Config.SelectSingleNode("/Configuration/FavoriteLinks");
            if (theNode != null)
            {
                XmlNode node = this.Config.SelectSingleNode("/Configuration/FavoriteLinks/FavoriteLink[@name = '" + linkName + "']");
                if (node != null)
                {
                    node.Attributes["url"].Value = linkURL;
                    node.InnerText = desc;
                }
                else
                {
                    XmlNode newNode = this.Config.CreateElement("FavoriteLink");
                    XmlAttribute newLinkAttr = this.Config.CreateAttribute("name");
                    newNode.Attributes.Append(newLinkAttr);
                    newLinkAttr.Value = linkName;

                    XmlAttribute newLinkAttrURL = this.Config.CreateAttribute("url");
                    newNode.Attributes.Append(newLinkAttrURL);
                    newLinkAttrURL.Value = linkURL;

                    newNode.InnerText = desc;
                    this.Config.SelectSingleNode("/Configuration/FavoriteLinks").AppendChild(newNode);
                }
            }
            else
            {
                XmlNode theNewNode = this.Config.CreateElement("FavoriteLinks");
                this.Config.SelectSingleNode("/Configuration").AppendChild(theNewNode);
                XmlNode newNode = this.Config.CreateElement("FavoriteLink");
                theNewNode.AppendChild(newNode);
                XmlAttribute newLinkAttr = this.Config.CreateAttribute("name");
                newNode.Attributes.Append(newLinkAttr);
                newLinkAttr.Value = linkName;

                XmlAttribute newLinkAttrURL = this.Config.CreateAttribute("url");
                newNode.Attributes.Append(newLinkAttrURL);
                newLinkAttrURL.Value = linkURL;

                newNode.InnerText = desc;
            }
        }
        #endregion FavoriteLinks

        #region DefaultSettings
        /// <summary>
        /// Returns Default Row number.
        /// </summary>
        /// <returns>Default Row number.</returns>
        public string GetDefaultRownum()
        {
            XmlNode rownumNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultRowNumber");
            return rownumNode.InnerText;
        }
        /// <summary>
        /// Updates Default Row number.
        /// </summary>
        /// <param name="rownum">rownum</param>
        /// <returns>Error message if fail</returns>
        public string SetDefaultRownum(string rownum)
        {
            if (rownum == null || rownum.Trim().Equals(""))
                return "Row number must be non-empty";
            XmlNode rownumNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultRowNumber");
            rownumNode.InnerText = rownum;
            return "";
        }
        /// <summary>
        /// Returns Default Top number.
        /// </summary>
        /// <returns>Default Top number.</returns>
        public string GetDefaultTopnum()
        {
            XmlNode topnumNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultTopNumber");
            return topnumNode.InnerText;
        }
        /// <summary>
        /// Updates Default Top number.
        /// </summary>
        /// <param name="topnum">topnum</param>
        /// <returns>Error message if fail</returns>
        public string SetDefaultTopnum(string topnum)
        {
            if (topnum == null || topnum.Trim().Equals(""))
                return "Top number must be non-empty";
            XmlNode topnumNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultTopNumber");
            topnumNode.InnerText = topnum;
            return "";
        }
        /// <summary>
        /// Returns Default Page Size.
        /// </summary>
        /// <returns>Default Page Size.</returns>
        public string GetDefaultPageSize()
        {
            XmlNode pageSizeNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultPageSize");
            return pageSizeNode.InnerText;
        }
        /// <summary>
        /// Updates Default Page Size.
        /// </summary>
        /// <param name="pageSize">pageSize</param>
        /// <returns>Error message if fail</returns>
        public string SetDefaultPageSize(string pageSize)
        {
            if (pageSize == null || pageSize.Trim().Equals(""))
                return "Page Size must be non-empty";
            XmlNode pageSizeNode = this.Config.SelectSingleNode("/Configuration/DefaultSettings/DefaultPageSize");
            pageSizeNode.InnerText = pageSize;
            return "";
        }
        #endregion DefaultSettings

        #region RESTfulService
        /// <summary>
        /// Returns RESTful Service Page Header.
        /// </summary>
        /// <returns>RESTful Service Page Header.</returns>
        public string GetRESTfulPageHeader()
        {
            XmlNode headerNode = this.Config.SelectSingleNode("/Configuration/RestfulService/Introduction/Header");
            return headerNode.InnerText;
        }
        /// <summary>
        /// Updates RESTful Service Page Header.
        /// </summary>
        /// <param name="pageHeader">pageHeader</param>
        /// <returns>Error message if fail</returns>
        public string SetRESTfulPageHeader(string pageHeader)
        {
            XmlNode headerNode = this.Config.SelectSingleNode("/Configuration/RestfulService/Introduction/Header");
            headerNode.InnerText = pageHeader;
            return "";
        }
        /// <summary>
        /// Returns RESTful Service Page Content.
        /// </summary>
        /// <returns>RESTful Service Page Content.</returns>
        public string GetRESTfulPageContent()
        {
            XmlNode contentNode = this.Config.SelectSingleNode("/Configuration/RestfulService/Introduction/Content");
            return contentNode.InnerText;
        }
        /// <summary>
        /// Updates RESTful Service Page Content.
        /// </summary>
        /// <param name="pageContent">pageContent</param>
        /// <returns>Error message if fail</returns>
        public string SetRESTfulPageContent(string pageContent)
        {
            XmlNode contentNode = this.Config.SelectSingleNode("/Configuration/RestfulService/Introduction/Content");
            contentNode.InnerText = pageContent;
            return "";
        }

        #endregion
    }
}
