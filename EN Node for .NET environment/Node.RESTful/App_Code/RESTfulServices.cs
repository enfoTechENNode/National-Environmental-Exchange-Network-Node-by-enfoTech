using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Text;
using System.Collections.Specialized;
using System.ServiceModel.Channels;

using Node.Core.Biz.Objects;
using Node.Core;
using NodeCoreHW = Node.Core.Biz.Handler.WebMethods;
using NodeCoreHW2 = Node.Core2.Biz.Handler.WebMethods;
using System.Collections;
using Node.Lib.Utility;
using Node.Core.Biz.Manageable;
using DataFlow.Component.Interface; 


/// <summary>
/// Summary description for IRESTfulServices
/// </summary>
[ServiceContract]
public interface IRESTfulServices
{
    [OperationContract]
    Stream Authenticate(string userId, string credential, string domain, string authenticationMethod);

    [OperationContract]
    Stream Query();
}

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class RESTfulServices: IRESTfulServices
{
    [WebGet(UriTemplate = "Authenticate?userID={userId}&credential={credential}&domain={domain}&authenticationMethod={authenticationMethod}")]
    public Stream Authenticate(string userId, string credential, string domain, string authenticationMethod)
    {

        //OperationContext context = OperationContext.Current;
        //MessageProperties prop = context.IncomingMessageProperties;
        //RemoteEndpointMessageProperty endpoint =
        //    prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
        //string ip = endpoint.Address;
        //ip = HttpContext.Current.Request.UserHostAddress;

        Node.Core2.Requestor.Authenticate auth = new Node.Core2.Requestor.Authenticate();
        auth.userId = userId;
        auth.credential = credential;
        auth.domain = (string.IsNullOrEmpty(domain)?"default":domain);
        auth.authenticationMethod = (string.IsNullOrEmpty(authenticationMethod)?"password":authenticationMethod);
        NodeCoreHW2.AuthenticateHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;
        NodeCoreHW2.AuthenticateHandler handler = new NodeCoreHW2.AuthenticateHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, auth);
        string sToken = (string)handler.Invoke();

        MemoryStream ms = new MemoryStream();
        byte[] content = Encoding.ASCII.GetBytes(sToken);
        ms.Write(content, 0, content.Length);
        WebOperationContext.Current.OutgoingResponse.ContentType = "text/plain";
        ms.Position = 0;
        return ms; 
    }
    [WebGet(UriTemplate = "Query?")]
    public Stream Query()
    {
       
        string sResult = "";
        NameValueCollection paras = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters;

        //for (int i = 0; i < paras.Count; i++)
        //{
        //    sResult = sResult + paras.GetKey(i);
        //    sResult = sResult + ":" + paras[i] + ";";
        //}

        string sOpName = paras["Request"];
        string rowId = paras["RowId"];
        string maxRows = paras["MaxRows"];
        string format = paras["Format"];
        string securityToken = (string.IsNullOrEmpty(paras["SecurityToken"]) ? "PublicUsers" : paras["SecurityToken"]);
        int rid = 0;
        int max = -1;
        int iTest = 0;

        Operation op = new Operation(sOpName, Phrase.WEB_SERVICE_QUERY);

        if (op != null && op.ID >= 0)
        {
            if (op.DomainStatus != null && op.DomainStatus.Trim().Equals(Phrase.STATUS_RUNNING))
            {
                if (op.Status != null && op.Status.Trim().Equals(Phrase.STATUS_RUNNING))
                {
                    if (op.RESTInd != null && op.RESTInd.Trim().ToUpper().Equals("Y"))
                    {
                        if (!string.IsNullOrEmpty(rowId))
                            rid = (int.TryParse(rowId, out iTest) ? iTest : rid);
                        if (!string.IsNullOrEmpty(maxRows))
                            max = (int.TryParse(maxRows, out iTest) ? iTest : max);

                        if (op.Version == Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11.ToString())
                            NodeCoreHW.QueryHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_11;
                        else
                            NodeCoreHW2.QueryHandler.NodeVersion = Node.Core.Biz.Handler.BaseHandler.NodeVer.VER_20;

                        string[] parameters = null;

                        if (op.Config.DocumentElement.Name.ToUpper() == "PROCESS")
                        {
                            DllManager dllMgr = new DllManager();
                            IActionProcess process = dllMgr.GetActionProcess();
                            IActionOperation actionOp = process.GetActionOperation(op.Config.OuterXml);

                            parameters = new string[actionOp.Variables.Count];

                            for (int i = 0; i < actionOp.Variables.Count; i++)
                            {
                                IActionParameter param = (IActionParameter)actionOp.Variables[i];
                                parameters[i] = (string.IsNullOrEmpty(paras[param.ParameterName]) ? "" : paras[param.ParameterName]);
                            }
                        }
                        else
                        {
                            parameters = new string[op.Parameters.Count];

                            for (int i = 0; i < op.Parameters.Count; i++)
                            {
                                OpParameter para = op.Parameters[0] as OpParameter;
                                parameters[i] = (string.IsNullOrEmpty(paras[para.Name]) ? "" : paras[para.Name]);
                            }
                        }


                        NodeCoreHW.QueryHandler handler = new NodeCoreHW.QueryHandler(HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Server.MachineName, securityToken, sOpName, rid, max, parameters);

                        sResult = "" + handler.Invoke();

                    }
                    else
                        sResult = Phrase.E_SERVICE_UNAVAILABLE;
                }
                else
                    sResult = Phrase.E_SERVICE_UNAVAILABLE;
            }
            else
                sResult = Phrase.E_SERVICE_UNAVAILABLE;
        }
        else
        {
            sResult = Phrase.E_SERVICE_UNAVAILABLE;
        }

        byte[] content = Encoding.ASCII.GetBytes(sResult);

        string formatType = "text/plain";
        formatType = "application/xml";
        if (!string.IsNullOrEmpty(format) && format.ToUpper().Equals("ZIP"))
        {
            WinZip wz = new WinZip();
            Hashtable ht = new Hashtable();
            ht.Add(sOpName + ".xml", content);
            content = wz.CreateZip(ht);
            formatType = "application/zip";
        }

        MemoryStream ms = new MemoryStream();
        ms.Write(content, 0, content.Length);
        WebOperationContext.Current.OutgoingResponse.ContentType = formatType;
        ms.Position = 0;



        return ms;

    }
}