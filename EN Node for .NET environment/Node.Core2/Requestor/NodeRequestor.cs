using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using Node.Lib.Security;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Document;

namespace Node.Core2.Requestor
{
    public class NodeRequestor : NetworkNode2
    {
        //***********************************************************************
        // Public Members
        //***********************************************************************
        #region Public Members

        #endregion

        //***********************************************************************
        // Private Members
        //***********************************************************************
        #region Private Members

        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors
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

            this.Url = url;
            this.SetPolicy("ClientPolicy");
            if (proxyURL != null && !proxyURL.Trim().Equals(""))
            {
                WebProxy wp = new WebProxy();
                wp.Address = new Uri(proxyURL);
                wp.BypassProxyOnLocal = true;
                if (proxyUID != null && !proxyUID.Trim().Equals("") && proxyPWD != null && !proxyPWD.Trim().Equals(""))
                {
                    Cryptography crypt = new Cryptography();
                    wp.Credentials = new NetworkCredential(proxyUID, crypt.Decrypting(proxyPWD, Phrase.CryptKey));
                }
                this.Proxy = wp;
            }
            if (url.ToLower().Trim().StartsWith("https:"))
            {
                ServicePointManager.ServerCertificateValidationCallback +=
                    delegate(object sender, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                    {
                        bool validationResult = true;
                        //
                        // policy code here ...
                        //
                        return validationResult;
                    };
            }
        }
        #endregion

        //***********************************************************************
        // Delegate Events
        //***********************************************************************
        #region Delegate Events

        #endregion

        //***********************************************************************
        // Public Properties
        //***********************************************************************
        #region Public Properties

        #endregion

        //***********************************************************************
        // Protected Properties
        //***********************************************************************
        #region Protected Properties

        #endregion

        //***********************************************************************
        // Public Methods
        //***********************************************************************
        #region Public Methods
        public new StatusResponseType Submit(Submit submit1)
        {
            //this.RequireMtom = true;
            return base.Submit(submit1);
        }

        public new NodeDocumentType[] Download(Download download1)
        {
            //this.RequireMtom = true;
            return base.Download(download1);
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static NodeDocumentType[] Convert(NodeDocument[] retDocs)
        {
            NodeDocumentType[] output = null;

            if (retDocs != null && retDocs.Length > 0)
            {
                output = new NodeDocumentType[retDocs.Length];

                for (int i = 0; i < retDocs.Length; i++)
                {
                    output[i] = new NodeDocumentType();
                    output[i].documentName = retDocs[i].name;
                    string type = (retDocs[i].type == null) ? "" : retDocs[i].type.ToUpper();
                    switch (type)
                    {
                        case "XML":
                            output[i].documentFormat = DocumentFormatType.XML;
                            break;
                        case "FLAT":
                            output[i].documentFormat = DocumentFormatType.FLAT;
                            break;
                        case "BIN":
                            output[i].documentFormat = DocumentFormatType.BIN;
                            break;
                        case "ZIP":
                            output[i].documentFormat = DocumentFormatType.ZIP;
                            break;
                        case "ODF":
                            output[i].documentFormat = DocumentFormatType.ODF;
                            break;
                        default:
                            output[i].documentFormat = DocumentFormatType.OTHER;
                            break;
                    }
                    if (retDocs[i].href == null || retDocs[i].href.Trim().Equals(""))
                        retDocs[i].href = System.Guid.NewGuid().ToString();

                    output[i].documentContent = new AttachmentType();
                    output[i].documentContent.Value = retDocs[i].content;
                    output[i].documentId = retDocs[i].href;
                }
            }
            return output;
        }

        public static NodeDocument[] Convert(NodeDocumentType[] docs)
        {
            NodeDocument[] output = null;
            if (docs != null && docs.Length > 0)
            {
                output = new NodeDocument[docs.Length];

                for (int i = 0; i < docs.Length; i++)
                {
                    output[i] = new NodeDocument();
                    output[i].name = docs[i].documentName;
                    output[i].type = docs[i].documentFormat.ToString().ToUpper();
                    output[i].content = docs[i].documentContent.Value;
                    output[i].href = docs[i].documentId;
                }
            }
            return output;
        }
        #endregion

        //***********************************************************************
        // Protected Methods
        //***********************************************************************
        #region Protected Methods

        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods

        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion
    }
}
