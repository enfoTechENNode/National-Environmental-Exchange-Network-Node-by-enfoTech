using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using System;
using System.Collections;
using System.Net;

using Node.Lib.Security;

using Node.Core.CDX;

namespace Node.Core.Requestor
{
    /// <summary>
    /// The client class to be used invoking web service in Node Spec. v1.1
    /// </summary>
    public class NodeRequestor : NetworkNode
    {
        private SoapContext responseSoapContext;
        private SoapContext requestSoapContext;
        /// <summary>
        /// Constructor of NodeRequestor.
        /// </summary>
        /// <param name="url">The endpoint url.</param>
        public NodeRequestor(string url)
        {
            this.Url = url;
            ServicePointManager.ServerCertificateValidationCallback = NodeRequestor.ValidateServerCertificate;
        }
        /// <summary>
        /// Constructor of NodeRequestor.
        /// </summary>
        /// <param name="url">The endpoint url.</param>
        /// <param name="proxyServer">The ip address of proxy server.</param>
        /// <param name="proxyUID">The user id to proxy server.</param>
        /// <param name="proxyPWD">The password to proxy server.</param>
        public NodeRequestor(string url, string proxyServer, string proxyUID, string proxyPWD)
        {
            this.Url = url;
            if (proxyServer != null && !proxyServer.Trim().Equals(""))
            {
                WebProxy wp = new WebProxy(proxyServer);
                wp.Address = new Uri(proxyServer);
                wp.BypassProxyOnLocal = true;
                if (proxyUID != null && !proxyUID.Trim().Equals("") && proxyPWD != null && !proxyPWD.Trim().Equals(""))
                {
                    Cryptography crypt = new Cryptography();
                    wp.Credentials = new NetworkCredential(proxyUID, crypt.Decrypting(proxyPWD, Phrase.CryptKey));
                }
                this.Proxy = wp;
            }
            ServicePointManager.ServerCertificateValidationCallback = NodeRequestor.ValidateServerCertificate;
        }
        /// <summary>
        /// Set proxy information for NodeRequestor.
        /// </summary>
        /// <param name="proxyServer">The ip address of proxy server.</param>
        /// <param name="proxyUID">The user id to proxy server.</param>
        /// <param name="proxyPWD">The password to proxy server.</param>
        /// <returns>True if the provided information pass proxy server.</returns>
        public bool SetProxy(string proxyServer, string proxyUID, string proxyPWD)
        {
            bool retBool = false;
            if (proxyServer != null && !proxyServer.Trim().Equals(""))
            {
                WebProxy wp = new WebProxy(proxyServer);
                wp.Address = new Uri(proxyServer);
                wp.BypassProxyOnLocal = true;
                if (proxyUID != null && !proxyUID.Trim().Equals("") && proxyPWD != null && !proxyPWD.Trim().Equals(""))
                    wp.Credentials = new NetworkCredential(proxyUID, proxyPWD);
                this.Proxy = wp;
                retBool = true;
            }
            return retBool;
        }
        /// <summary>
        /// The method invokes submit operation.
        /// </summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="transactionId">The transation id.</param>
        /// <param name="dataflow">The name of dataflow.</param>
        /// <param name="documents">The upload content.</param>
        /// <returns>The unique transaction id.</returns>
        public string Submit(string securityToken, string transactionId, string dataflow, Node.Core.Document.NodeDocument[] documents)
        {
            NodeDocument[] input = null;
            Hashtable hrefs = new Hashtable();
            //requestSoapContext = RequestSoapContext.Current;
            requestSoapContext = RequestSoapContext;

            if (documents != null && documents.Length > 0)
            {
                input = new NodeDocument[documents.Length];
                for (int i = 0; i < documents.Length; i++)
                {
                    Node.Core.Document.NodeDocument doc = documents[i];
                    if (doc.href == null || doc.href.Trim().Equals(""))
                    {
                        doc.href = Guid.NewGuid().ToString();
                    }

                    input[i] = new NodeDocument();
                    input[i].name = doc.name;
                    input[i].type = doc.type;
                    input[i].content = doc.content;

                    if (this.requestSoapContext != null)
                    {
                        DimeAttachment attach = new DimeAttachment(doc.href, doc.type, TypeFormat.MediaType, doc.Stream);
                        this.requestSoapContext.Attachments.Add(attach);
                        hrefs.Add(doc.name, doc.href);
                    }
                }
                if (this.requestSoapContext != null)
                {
                    this.requestSoapContext.Add("hrefs", hrefs);
                }
            }
            return base.Submit(securityToken, transactionId, dataflow, input);
        }
        /// <summary>
        /// The method invokes download operation.
        /// </summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="transactionId">The transation id.</param>
        /// <param name="dataflow">The name of dataflow.</param>
        /// <param name="documents">The download content.</param>
        /// <returns>The content of request.</returns>
        public Node.Core.Document.NodeDocument[] Download(string securityToken, string transactionId, string dataflow, Node.Core.Document.NodeDocument[] documents)
        {
            NodeDocument[] input = null;
            if (documents != null && documents.Length > 0)
            {
                input = new NodeDocument[documents.Length];
                for (int i = 0; i < documents.Length; i++)
                {
                    input[i] = new NodeDocument();
                    input[i].name = documents[i].name;
                    input[i].type = documents[i].type;
                }
            }

            NodeDocument[] output = base.Download(securityToken, transactionId, dataflow, input);

            responseSoapContext = ResponseSoapContext;

            Node.Core.Document.NodeDocument[] retDocs = null;
            if (output != null && output.Length > 0)
            {
                retDocs = new Node.Core.Document.NodeDocument[output.Length];
                for (int i = 0; i < output.Length; i++)
                {
                    retDocs[i] = new Node.Core.Document.NodeDocument();
                    retDocs[i].name = output[i].name;
                    retDocs[i].type = output[i].type;

                    Hashtable ht = (Hashtable)this.responseSoapContext["hrefs"];

                    string key = (string)ht[retDocs[i].name];
                    if (key != null && !key.Trim().Equals(string.Empty))
                    {
                        retDocs[i].Stream = this.responseSoapContext.Attachments[key].Stream;
                    }
                    else if (this.responseSoapContext.Attachments != null && this.responseSoapContext.Attachments.Count > 0)
                    {
                        retDocs[i].Stream = this.responseSoapContext.Attachments[i].Stream;
                    }
                    else
                    {
                        retDocs[i].Stream = new System.IO.MemoryStream((byte[])output[i].content);
                    }

                }
            }

            return retDocs;
        }
        /// <summary>
        /// The method invokes notify operation.
        /// </summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="dataflow">The name of dataflow.</param>
        /// <param name="nodeAddress">The node address.</param>
        /// <param name="documents">The download content.</param>
        /// <returns></returns>
        public string Notify(string securityToken, string nodeAddress, string dataflow, Node.Core.Document.NodeDocument[] documents)
        {
            NodeDocument[] input = null;
            Hashtable hrefs = new Hashtable();
            //requestSoapContext = RequestSoapContext.Current;
            requestSoapContext = RequestSoapContext;

            if (documents != null && documents.Length > 0)
            {
                input = new NodeDocument[documents.Length];
                for (int i = 0; i < documents.Length; i++)
                {
                    Node.Core.Document.NodeDocument doc = documents[i];
                    if (doc.href == null || doc.href.Trim().Equals(""))
                        doc.href = Guid.NewGuid().ToString();
                    DimeAttachment attach = new DimeAttachment(doc.href, doc.type, TypeFormat.MediaType, doc.Stream);
                    this.requestSoapContext.Attachments
                        .Add(attach);

                    input[i] = new NodeDocument();
                    input[i].name = doc.name;
                    input[i].type = doc.type;

                    hrefs.Add(doc.name, doc.href);
                }
                this.requestSoapContext.Add("hrefs", hrefs);
            }
            return base.Notify(securityToken, nodeAddress, dataflow, input);
        }
        /// <summary>
        /// The method validate Server Certificate.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="certificate">certificate</param>
        /// <param name="chain">chain</param>
        /// <param name="errors">errors</param>
        /// <returns>True</returns>
        public static bool ValidateServerCertificate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }
    }
}
