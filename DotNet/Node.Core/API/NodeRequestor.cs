using System;
using System.Xml;

using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Document;

namespace Node.Core.API
{
    /// <summary>
    /// The client class to be used invoking web service in Node Spec. v1.1
    /// </summary>
    public class NodeRequestor
    {
        Node.Core.Requestor.NodeRequestor Requestor = null;

        /// <summary>
        /// Creates a new instance of a node web service requestor.
        /// This constructor will automatically set the proxy if the proxy is configured through the Node.Administration application.
        /// </summary>
        /// <param name="url">The Node URL of the Node being requested.</param>
        public NodeRequestor(string url)
        {
            string proxyURL = null;
            string proxyUID = null;
            string proxyPWD = null;
            try
            {
                IConfigurations configDB = new DBManager().GetConfigurationsDB();
                if (configDB != null)
                {
                    XmlDocument doc = configDB.GetSystemConfig();
                    XmlNode proxyNode = doc.SelectSingleNode("/Configuration/ProxySettings");
                    if (proxyNode != null)
                    {
                        XmlAttribute proxyStatus = proxyNode.Attributes["status"];
                        if (proxyStatus != null && proxyStatus.Value.Equals("A"))
                        {
                            proxyURL = proxyNode.Attributes["host"].Value;
                            XmlNode proxyCredentials = proxyNode.SelectSingleNode("Credentials");
                            if (proxyCredentials != null)
                            {
                                proxyUID = proxyCredentials.SelectSingleNode("UserID").InnerText;
                                proxyPWD = proxyCredentials.SelectSingleNode("Password").InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            if (proxyURL != null)
                this.Requestor = new Node.Core.Requestor.NodeRequestor(url, proxyURL, proxyUID, proxyPWD);
            else
                this.Requestor = new Node.Core.Requestor.NodeRequestor(url);
        }

        /// <summary>
        /// This method calls the Node Authenticate method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="userName">The Node User (usually NAAS User) that is being authenticated.</param>
        /// <param name="password">The password for the Node User being authenticated.</param>
        /// <param name="authMethod">The method of authenication (usally PASSWORD).</param>
        /// <returns>The security token if successful.</returns>
        /// <exception>If the user fails authenication on the target Node.</exception>
        public string Authenticate(string userName, string password, string authMethod)
        {
            return this.Requestor.Authenticate(userName, password, authMethod);
        }

        /// <summary>
        /// This method calls the Node Download method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="transactionID">The transaction ID of the document to download.</param>
        /// <param name="dataFlow">The data flow name of the document to download.</param>
        /// <param name="docs">An array of <see cref="Node.Core.Document.NodeDocument">NodeDocument</see>s that contain the names and types of documents to download.</param>
        /// <returns>An array of docwnloaded <see cref="Node.Core.Document.NodeDocument">NodeDocument</see>s.</returns>
        public Node.Core.Document.NodeDocument[] Download(string token, string transactionID, string dataFlow, Node.Core.Document.NodeDocument[] docs)
        {
            return this.Requestor.Download(token, transactionID, dataFlow, docs);
        }

        /// <summary>
        /// This method calls the Node GetServices method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="serviceType">The service type of the services to retrieve (ServiceType, Interfaces, Query, Solicit).</param>
        /// <returns>A string array of provided services.</returns>
        public string[] GetServices(string token, string serviceType)
        {
            return this.Requestor.GetServices(token, serviceType);
        }

        /// <summary>
        /// This method calls the Node GetStatus method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="transactionID">The Node transaction ID to query the status of.</param>
        /// <returns>The status of the Node transaction.</returns>
        public string GetStatus(string token, string transactionID)
        {
            return this.Requestor.GetStatus(token, transactionID);
        }

        /// <summary>
        /// This method calls the Node NodePing method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="hello">A string to test connectivity.</param>
        /// <returns>A string that indicates that the Node is alive.</returns>
        public string NodePing(string hello)
        {
            return this.Requestor.NodePing(hello);
        }

        /// <summary>
        /// This method calls the Node Notify method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="nodeAddress">The node address that is making the Notify request.</param>
        /// <param name="dataFlow">The data flow the Notify request is associated with.</param>
        /// <param name="docs">The array of <see cref="Node.Core.Document.NodeDocument">NodeDocument</see>s that indicate tye content and type of Notify.</param>
        /// <returns>A transaction ID of this Notify request.</returns>
        public string Notify(string token, string nodeAddress, string dataFlow, Node.Core.Document.NodeDocument[] docs)
        {
            return this.Requestor.Notify(token, nodeAddress, dataFlow, docs);
        }

        /// <summary>
        /// This method calls the Node Query method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="request">The request (type of Query) being requested.</param>
        /// <param name="rowID">The row ID the query should begin returning records of.</param>
        /// <param name="maxRows">The max number of rows that query should return.</param>
        /// <param name="parameters">The list of parameters that are used to narrow the search criteria.</param>
        /// <returns>A string with the result of the query, usually in XML format.</returns>
        public string Query(string token, string request, string rowID, string maxRows, string[] parameters)
        {
            return this.Requestor.Query(token, request, rowID, maxRows, parameters);
        }

        /// <summary>
        /// This method calls the Node Solicit method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="returnURL">The URL of the Node to invoke a Submit on when the result of the solicit is ready.</param>
        /// <param name="request">The request (type of Solicit) being requested.</param>
        /// <param name="parameters">The list of parameters that are used to narrow the search criteria.</param>
        /// <returns>A transaction ID of this Solicit request.</returns>
        public string Solicit(string token, string returnURL, string request, string[] parameters)
        {
            return this.Requestor.Solicit(token, returnURL, request, parameters);
        }

        /// <summary>
        /// This method calls the Node Solicit method on the Node URL supplied in the constructor.
        /// </summary>
        /// <param name="token">The security token returned from a previous Authenticate request.</param>
        /// <param name="transactionID">The transaction ID this Submit request is associated with.</param>
        /// <param name="dataFlow">The data flow the documents are associated with.</param>
        /// <param name="docs">The names, types, and content of the data provided.</param>
        /// <returns>A transaction ID of this Submit request.</returns>
        public string Submit(string token, string transactionID, string dataFlow, Node.Core.Document.NodeDocument[] docs)
        {
            return this.Requestor.Submit(token, transactionID, dataFlow, docs);
        }
    }
}
