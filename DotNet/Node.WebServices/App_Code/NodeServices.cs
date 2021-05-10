using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

using Node.Core.Document;
using Node.Core.Biz.Handler.WebMethods;
using Node.Core;

namespace Node.WebServices
{
    [WebService(Description = "A set of services for the National Environmental Informationi Exchange Network (NEIEN)", Namespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", Name = "NetworkNode")]
    [WebServiceBinding(Name = "NetworkNodeBinding", Namespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.wsdl", ConformsTo = WsiProfiles.BasicProfile1_1)]
    [SoapRpcService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    public class NodeServices : System.Web.Services.WebService
    {

        public NodeServices()
        {
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string Authenticate(string userId, string credential, string authenticationMethod)
        {
            AuthenticateHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            AuthenticateHandler handler = new AuthenticateHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, userId, credential, authenticationMethod);
            return (string)handler.Invoke();
        }


        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("documents")]
        public Node.Core.CDX.NodeDocument[] Download(string securityToken, string transactionId, string dataflow, Node.Core.CDX.NodeDocument[] documents)
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
            DownloadHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            DownloadHandler handler = new DownloadHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, transactionId, dataflow, input);
            NodeDocument[] retDocs = (NodeDocument[])handler.Invoke();
            Node.Core.CDX.NodeDocument[] output = null;
            if (retDocs != null && retDocs.Length > 0)
            {
                output = new Node.Core.CDX.NodeDocument[retDocs.Length];
                SoapContext context = ResponseSoapContext.Current;
                Hashtable hrefs = new Hashtable();
                object obj = context["hrefs"];
                if (obj != null)
                    hrefs = (Hashtable)obj;
                else
                    context.Add("hrefs", hrefs);
                for (int i = 0; i < retDocs.Length; i++)
                {
                    output[i] = new Node.Core.CDX.NodeDocument();
                    output[i].name = retDocs[i].name;
                    output[i].type = retDocs[i].type;

                    if (retDocs[i].href == null || retDocs[i].href.Trim().Equals(""))
                        retDocs[i].href = System.Guid.NewGuid().ToString();
                    DimeAttachment attach = new DimeAttachment(retDocs[i].href, retDocs[i].type, TypeFormat.MediaType, retDocs[i].Stream);
                    context.Attachments.Add(attach);

                    hrefs.Add(retDocs[i].name, retDocs[i].href);
                }
            }
            return output;
        }


        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string[] GetServices(string securityToken, string serviceType)
        {
            GetServicesHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            GetServicesHandler handler = new GetServicesHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, serviceType);
            return (string[])handler.Invoke();
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string GetStatus(string securityToken, string transactionId)
        {
            GetStatusHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            GetStatusHandler handler = new GetStatusHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, transactionId);
            string sStatus = "" + handler.Invoke();
            switch (sStatus)
            {
                case Phrase.STATUS_DONE:
                    sStatus = Phrase.STATUS_PROCESSED;
                    break;
            }
            return sStatus;
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string NodePing(string Hello)
        {
            NodePingHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            NodePingHandler handler = new NodePingHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, Hello);
            return "" + handler.Invoke();
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string Notify(string securityToken, string nodeAddress, string dataflow, Node.Core.CDX.NodeDocument[] documents)
        {
            NodeDocument[] input = null;
            if (documents != null && documents.Length > 0)
            {
                input = new NodeDocument[documents.Length];
                SoapContext context = RequestSoapContext.Current;
                Hashtable hrefs = new Hashtable();
                if (context.Contains("hrefs"))
                    hrefs = (Hashtable)context["hrefs"];
                for (int i = 0; i < documents.Length; i++)
                {
                    input[i] = new NodeDocument();
                    input[i].name = documents[i].name;
                    input[i].type = documents[i].type;

                    if (hrefs.ContainsKey(documents[i].name))
                    {
                        Attachment attach = context.Attachments["" + hrefs[documents[i].name]];
                        if (attach != null)
                        {
                            input[i].Stream = attach.Stream;
                            input[i].href = "" + hrefs[documents[i].name];
                        }
                    }
                }
            }
            NotifyHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            NotifyHandler handler = new NotifyHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, nodeAddress, dataflow, input);
            return "" + handler.Invoke();
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        //public string Query(string securityToken, string request, [System.Xml.Serialization.SoapElementAttribute(DataType = "integer")] string rowId, [System.Xml.Serialization.SoapElementAttribute(DataType = "integer")] string maxRows, string[] parameters)
        public string Query(string securityToken, string request, string rowId, string maxRows, string[] parameters)
        {
            int rid = 0; int max = -1;
            if (rowId != null && !rowId.Trim().Equals(""))
                rid = System.Convert.ToInt32(rowId);
            if (maxRows != null && !maxRows.Trim().Equals(""))
                max = System.Convert.ToInt32(maxRows);
            QueryHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            QueryHandler handler = new QueryHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, request, rid, max, parameters);
            return "" + handler.Invoke();
        }

        [WebMethod]
        [SoapRpcMethod(RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return", DataType = "string")]
        public string Solicit(string securityToken, string returnURL, string request, string[] parameters)
        {
            SolicitHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            SolicitHandler handler = new SolicitHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, returnURL, request, parameters);
            return "" + handler.Invoke();
        }

        [WebMethod]
        [SoapRpcMethod("", RequestNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", ResponseNamespace = "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd")]
        [return: System.Xml.Serialization.SoapElement("return")]
        public string Submit(string securityToken, string transactionId, string dataflow, Node.Core.CDX.NodeDocument[] documents)
        {
            NodeDocument[] input = null;
            if (documents != null && documents.Length > 0)
            {
                input = new NodeDocument[documents.Length];
                SoapContext context = RequestSoapContext.Current;
                Hashtable hrefs = new Hashtable();
                if (context.Contains("hrefs"))
                    hrefs = (Hashtable)context["hrefs"];
                for (int i = 0; i < documents.Length; i++)
                {
                    input[i] = new NodeDocument();
                    input[i].name = documents[i].name;
                    input[i].type = documents[i].type;

                    if (hrefs.ContainsKey(documents[i].name))
                    {
                        Attachment attach = context.Attachments["" + hrefs[documents[i].name]];
                        if (attach != null)
                        {
                            input[i].Stream = attach.Stream;
                            input[i].href = "" + hrefs[documents[i].name];
                        }
                    }
                    else
                    {
                        input[i].content = (byte[])documents[i].content;
                    }
                }
            }
            SubmitHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
            SubmitHandler handler = new SubmitHandler(this.Context.Request.UserHostAddress, this.Server.MachineName, securityToken, transactionId, dataflow, input);
            return (string)handler.Invoke();
        }
    }
}
