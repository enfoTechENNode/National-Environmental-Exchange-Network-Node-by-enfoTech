using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

using Node.Lib.Utility;
using Node.Core;
using Node.Core2.Requestor;
using Node.Core2.Biz.Handler.WebMethods;
using Node.Core.Document;
using Node.Core.Util;
using Microsoft.Web.Services3;
using System.IO;
using Node.Core.Data.Interfaces;
using Node.Core.Data;

namespace Node.WebServices
{
    /// <summary>
    /// Summary description for NodeService2
    /// </summary>
    [WebService(Description = "Network Node 2.0 definitions for the Environmental Information Exchange Network", Namespace = "http://www.exchangenetwork.net/schema/node/2", Name = "NetworkNode2")]
    [WebServiceBinding(Name = "NetworkNodeBinding2", Namespace = "http://www.exchangenetwork.net/schema/node/2/node_v20.wsdl", ConformsTo = WsiProfiles.BasicProfile1_1)]
    [Microsoft.Web.Services3.Messaging.SoapActor("*")]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [Policy("ClientPolicy")]
    public class NodeServices2 : Microsoft.Web.Services3.Messaging.SoapService
    {
        public NodeServices2()
        {
        }

        #region INetworkNodeBinding2 Members

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Authenticate", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("AuthenticateResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public AuthenticateResponse Authenticate(Authenticate Authenticate)
        {
            AuthenticateHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            AuthenticateHandler handler = new AuthenticateHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Authenticate);
            AuthenticateResponse resp = new AuthenticateResponse();
            resp.securityToken = (string)handler.Invoke();
            return resp;  
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Submit", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("SubmitResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public StatusResponseType Submit(Submit Submit)
        {
            NodeDocument[] input = NodeRequestor.Convert(Submit.documents);
            SubmitHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            SubmitHandler handler = new SubmitHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Submit, input);
            StatusResponseType resp = new StatusResponseType();
            resp.transactionId = (string)handler.Invoke();
            resp.status = TransactionStatusCode.Received;
            resp.statusDetail = TransactionStatusCode.Received.ToString();
            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/GetStatus", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("GetStatusResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public StatusResponseType GetStatus(GetStatus GetStatus)
        {
            StatusResponseType resp = new StatusResponseType();
            string sStatus = "";
            //if (this.Context.Request.UserHostAddress != "65.242.154.17")
            //{
                GetStatusHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
                GetStatusHandler handler = new GetStatusHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, GetStatus);
                sStatus = "" + handler.Invoke();
            //}
            //else
            //{
            //    ILogging logDB = new DBManager().GetLoggingDB();
            //    sStatus = logDB.GetLatestStatus(GetStatus.transactionId);
            //    if (sStatus == null)
            //    {
            //        ThrowSoapException(Phrase.E_TRANSACTION_NOT_FOUND, Phrase.E_TRANSACTION_NOT_FOUND, "Transaction ID not found");
            //    }

            //}

            switch (sStatus)
            {
                case Phrase.STATUS_COMPLETED:
                    resp.status = TransactionStatusCode.Completed;
                    resp.statusDetail = Phrase.MESSAGE_COMPLETED;
                    break;
                case Phrase.STATUS_RECEIVED:
                    resp.status = TransactionStatusCode.Received;
                    resp.statusDetail = Phrase.MESSAGE_RECEIVED;
                    break;
                case Phrase.STATUS_FAILED:
                    resp.status = TransactionStatusCode.Failed;
                    resp.statusDetail = Phrase.MESSAGE_FAILED;
                    break;
                case Phrase.STATUS_DONE:
                    resp.status = TransactionStatusCode.Processed;
                    resp.statusDetail = Phrase.MESSAGE_PROCESSED;
                    break;
            }
            resp.transactionId = GetStatus.transactionId;
            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Notify", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("NotifyResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public StatusResponseType Notify(Notify Notify)
        {
            NotifyHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            NotifyHandler handler = new NotifyHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Notify);
            string transid = "" + handler.Invoke();
            StatusResponseType resp = new StatusResponseType();
            resp.status = TransactionStatusCode.Received;
            resp.transactionId = transid;
            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Download", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("DownloadResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("documents", IsNullable = false)]
        public NodeDocumentType[] Download(Download Download)
        {
            NodeDocument[] input = null;
            if (Download.documents != null && Download.documents.Length > 0)
            {
                input = new NodeDocument[Download.documents.Length];
                for (int i = 0; i < Download.documents.Length; i++)
                {
                    input[i] = new NodeDocument();

                    if (Download.documents[i].documentName != null)
                    {
                        if (Download.documents[i].documentName.Equals(Phrase.DN_NODE20_ORIGINAL, StringComparison.CurrentCultureIgnoreCase))
                            input[i].name = Download.documents[i].documentId;
                        else
                            input[i].name = Download.documents[i].documentName;
                    }
                    input[i].type = Download.documents[i].documentFormat.ToString().ToUpper();
                    input[i].href = Download.documents[i].documentId;
                }
            }
            DownloadHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            DownloadHandler handler = new DownloadHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Download, input);
            NodeDocument[] retDocs = (NodeDocument[])handler.Invoke();

            NodeDocumentType[] output = NodeRequestor.Convert(retDocs);
            return output;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Query", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("QueryResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public ResultSetType Query(Query Query)
        {
            ResultSetType resultSetType = new ResultSetType();
            QueryHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            QueryHandler handler = new QueryHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Query);
            string xmlresult = "" + handler.Invoke();

            if (Query.dataflow == "NCT")
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(ResultSetType));
                UTF8Encoding en = new UTF8Encoding();
                MemoryStream ms = new MemoryStream(en.GetBytes(xmlresult)); 
                StreamReader myReader = new StreamReader(ms);
                resultSetType =  mySerializer.Deserialize(myReader) as ResultSetType;               
            }
            else
            {
                DocumentFormatType docType = DocumentFormatType.XML;
                if (NodeXMLResult.isValidXMLContent(xmlresult))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(xmlresult);
                    resultSetType.rowId = Query.rowId;
                    resultSetType.results = new GenericXmlType();
                    resultSetType.results.format = docType;
                    XmlNode[] resultDoc = new XmlNode[1];
                    resultDoc[0] = (XmlNode)xDoc.DocumentElement;
                    resultSetType.results.Any = resultDoc;

                    if (Query.maxRows != "-1")
                    {
                        int iRowCount = int.Parse(Query.maxRows) - int.Parse(Query.rowId);
                        resultSetType.rowCount = iRowCount.ToString();
                        resultSetType.lastSet = false;
                    }
                    else
                    {
                        resultSetType.rowCount = "" + xDoc.ChildNodes.Count;
                        resultSetType.lastSet = true;
                    }

                }
                else
                {
                    XmlDocument xDoc2 = new XmlDocument();
                    xDoc2.LoadXml("<ErrorMessage>Not Well Format XML</ErrorMessage>");
                    resultSetType.rowId = Query.rowId;
                    resultSetType.results = new GenericXmlType();
                    resultSetType.results.format = docType;

                    XmlNode[] resultDoc = new XmlNode[1];
                    resultDoc[0] = (XmlNode)xDoc2.DocumentElement;
                    resultSetType.results.Any = resultDoc;
                    resultSetType.rowCount = "" + xDoc2.ChildNodes.Count;
                }

            }

            return resultSetType;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Solicit", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("SolicitResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public StatusResponseType Solicit(Solicit Solicit)
        {
            SolicitHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            SolicitHandler handler = new SolicitHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Solicit);
            StatusResponseType resp = new StatusResponseType();
            try
            {
                resp.transactionId = "" + handler.Invoke();
                resp.status = TransactionStatusCode.Received;
                resp.statusDetail = TransactionStatusCode.Received.ToString();
            }
            catch (Exception)
            {
                resp.status = TransactionStatusCode.Failed;
                resp.statusDetail = TransactionStatusCode.Failed.ToString();
            }

            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/Execute", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ExecuteResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public ExecuteResponse Execute(Execute Execute)
        {
            ExecuteHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            ExecuteHandler handler = new ExecuteHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, Execute);
            string xmlresult = "" + handler.Invoke();
            ExecuteResponse resp = new ExecuteResponse();

            if (NodeXMLResult.isValidXMLContent(xmlresult))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlresult);
                XmlNode[] resultDoc = new XmlNode[] { xDoc };

                resp.results.Any = resultDoc;
                resp.results.format = DocumentFormatType.XML;
                resp.transactionId = handler.TransactionId;
                resp.status = TransactionStatusCode.Completed;
            }
            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/NodePing", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("NodePingResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public NodePingResponse NodePing(NodePing NodePing)
        {
            NodePingHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            NodePingHandler handler = new NodePingHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, NodePing);
            NodePingResponse resp = new NodePingResponse();
            try
            {

                string sResult = (string)handler.Invoke();
                if (sResult.Equals(NodePing.hello))
                    resp.nodeStatus = NodeStatusCode.Ready;
                else
                    resp.nodeStatus = NodeStatusCode.Unknown;
                resp.statusDetail = sResult;
                resp.statusDetail = "ENNode(" + System.Reflection.Assembly.Load("Node.Core2").GetName().Version+")";
            }
            catch (Exception)
            {
                resp.nodeStatus = NodeStatusCode.Offline;
                resp.statusDetail = Phrase.E_SERVICE_UNAVAILABLE;
            }
            return resp;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.exchangenetwork.net/schema/node/2/GetServices", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("GetServicesResponse", Namespace = "http://www.exchangenetwork.net/schema/node/2")]
        public GenericXmlType GetServices(GetServices GetServices)
        {
            GetStatusHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
            GetServicesHandler handler = new GetServicesHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, GetServices);
            object obj = handler.Invoke();
            GenericXmlType resp = new GenericXmlType();

            resp.format = DocumentFormatType.XML;
            resp.Any = new XmlNode[] { ((XmlDocument)obj).DocumentElement };
            return resp;
        }

        #endregion
        #region SOAP EXCEPTION HANDLING
        private void ThrowSoapException(string message, string errorcode, string description)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element, "soap", "Detail", "http://www.w3.org/2003/05/soap-envelope");

            string sNameSpace = "";
            sNameSpace = "http://www.exchangenetwork.net/schema/node/2";

            System.Xml.XmlNode details = doc.CreateNode(XmlNodeType.Element, "NodeFaultDetailType", sNameSpace);
            System.Xml.XmlNode errorCode = doc.CreateNode(XmlNodeType.Element, "errorCode", sNameSpace);
            errorCode.InnerText = errorcode;
            details.AppendChild(errorCode);
            System.Xml.XmlNode desc = doc.CreateNode(XmlNodeType.Element, "description", sNameSpace);
            desc.InnerText = description;
            details.AppendChild(desc);

            // Append the two child elements to the detail node.
            node.AppendChild(details);

            //Throw the exception.    
            SoapException e = new SoapException("Fault occured while processing", SoapException.ClientFaultCode, HttpContext.Current.Request.UserHostAddress, node);
            string s = e.Detail.OuterXml;
            throw e;
        }
#endregion
    }
}