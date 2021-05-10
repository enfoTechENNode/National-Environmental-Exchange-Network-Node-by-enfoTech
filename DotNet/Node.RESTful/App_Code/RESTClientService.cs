using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Node.Lib.Data;
using System.Data;
using Node.Core.Biz.Objects;
using Node.Core.Biz.Manageable;
using DataFlow.Component.Interface;

[ServiceContract]
public interface IRESTClientService
{
    [OperationContract]
    List<ENNodeDataFlow> GetDataFlow();

    [OperationContract]
    ENNodeService GetServiceDetail(string serviceID);

    [OperationContract]
    ENNodeRESTfulPageInfo GetRESTfulPageDesc();
}
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class RESTClientService : IRESTClientService
{
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDataFlow")]
    public List<ENNodeDataFlow> GetDataFlow()
    {
        #region debug
        //List<ENNodeDataFlow> result = new List<ENNodeDataFlow>();
        //for (int i = 0; i < 5; i++)
        //{
        //    ENNodeDataFlow df = new ENNodeDataFlow();
        //    df.DataFlowName = "FRS" + i;

        //    List<ENNodeServiceName> services = new List<ENNodeServiceName>();
        //    for (int j=0;j<5;j++)
        //    {
        //        ENNodeServiceName sn = new ENNodeServiceName();
        //        sn.ServiceID = j;
        //        sn.ServiceName = "ServiceName" + j;
        //        services.Add(sn);
        //    }

        //    df.Services = services;
        //    result.Add(df);
        //}
        #endregion
        RESTDBHandler dh = new RESTDBHandler();
        List<ENNodeDataFlow>  result = dh.GetDataFlowServices();

        return result;
    }
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetServiceDetail?serviceID={serviceID}")]
    public ENNodeService GetServiceDetail(string serviceID)
    {
        ENNodeService result = new ENNodeService();

        serviceID = serviceID.Substring(serviceID.LastIndexOf("-")+1);
        int iServiceID = int.Parse(serviceID);

        Operation op = new Operation(iServiceID);

        result.ServiceID = op.ID;
        result.DataFlowName = op.DomainName;
        result.ServiceName = op.Name;
        result.Description = op.Description;
        result.ServiceBaseURL = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri.AbsoluteUri.Replace("Clients", "");

        List<ENNodeServiceParameter> lstPara = new List<ENNodeServiceParameter>();
        result.Parameters = lstPara;

        if (op.Config.DocumentElement.Name.ToUpper() == "PROCESS")
        {
            DllManager dllMgr = new DllManager();
            IActionProcess process = dllMgr.GetActionProcess();
            IActionOperation actionOp = process.GetActionOperation(op.Config.OuterXml);

            for (int i = 0; i < actionOp.Variables.Count; i++)
            {
                IActionParameter param = (IActionParameter)actionOp.Variables[i];
                ENNodeServiceParameter para = new ENNodeServiceParameter();
                para.ParaName = param.ParameterName;
                lstPara.Add(para);
            }
        
        }else
        {

            for (int i = 0; i < op.Parameters.Count; i++)
            {
                ENNodeServiceParameter para = new ENNodeServiceParameter();
                OpParameter opPara = op.Parameters[i] as OpParameter;
                para.ParaName = opPara.Name;
                lstPara.Add(para);
            }
        }

        return result;
    }
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRESTfulPageDesc")]
    public ENNodeRESTfulPageInfo GetRESTfulPageDesc()
    {
        ENNodeRESTfulPageInfo result = new ENNodeRESTfulPageInfo();

        #region debug
        //result.PageTitle = "ENNode RESTful Services";
        //result.PageDescription = "1111111111111111111 222222222222222222222 333333333333333333333 555555555555555555 akljflkdsjlajdlasjkd";
        //for (int i = 0; i < 3; i++)
        //    result.PageDescription = result.PageDescription + result.PageDescription;
        #endregion

        SystemConfiguration sysconfig = new SystemConfiguration();
        result.PageTitle = sysconfig.GetRESTfulPageHeader();
        result.PageDescription = sysconfig.GetRESTfulPageContent();

        return result;
    }

}

[DataContract]
public class ENNodeRESTfulPageInfo
{
    [DataMember]
    public string PageTitle { get; set; }
    [DataMember]
    public string PageDescription { get; set; }
}
[DataContract]
public class ENNodeDataFlow
{
    [DataMember]
    public string DataFlowName { get; set; }
    [DataMember]
    public List<ENNodeServiceName> Services { get; set; }
}
[DataContract]
public class ENNodeServiceName
{
    [DataMember]
    public int ServiceID { get; set; }
    [DataMember]
    public string ServiceName { get; set; }
}
[DataContract]
public class ENNodeService
{
    [DataMember]
    public int ServiceID { get; set; }
    [DataMember]
    public string ServiceName { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public string DataFlowName { get; set; }
    [DataMember]
    public string ServiceBaseURL { get; set; }
    [DataMember]
    public List<ENNodeServiceParameter> Parameters { get; set; }
}
[DataContract]
public class ENNodeServiceParameter
{
    /// <summary>
    /// Name of Parameter.
    /// </summary>
    /// 
      [DataMember]
    public string ParaName { get; set; }
    /// <summary>
    /// Value of Parameter.
    /// </summary>
      [DataMember]
    public string ParaValue { get; set; }

    /// <summary>
    /// Gets or sets the DEDL Encoding value.
    /// </summary>
      [DataMember]
    public string DEDLEncoding { get; set; }

    /// <summary>
    /// Gets or sets the DEDL Occurence Number value.
    /// </summary>
      [DataMember]
    public string DEDLOccurenceNumber { get; set; }

    /// <summary>
    /// Gets or sets the DEDL type value.
    /// </summary>
      [DataMember]
    public string DEDLType { get; set; }

    /// <summary>
    /// Gets or sets the DEDL RequiredIndicator value.
    /// </summary>
      [DataMember]
    public string DEDLRequiredIndicator { get; set; }

    /// <summary>
    /// Gets or sets the DEDL TypeDescriptor value.
    /// </summary>
     [DataMember]
    public string DEDLTypeDescriptor { get; set; }

}

public class RESTDBHandler
{
    private DBAdapter _db=null;
    public RESTDBHandler()
    {
        _db = new DBAdapter("node");
    }

    public List<ENNodeDataFlow> GetDataFlowServices()
    {
        List<ENNodeDataFlow> result = new List<ENNodeDataFlow>();

        string sSql = "SELECT  distinct NODE_DOMAIN.DOMAIN_NAME, NODE_OPERATION.OPERATION_NAME, NODE_OPERATION.OPERATION_ID "+
                        "FROM  NODE_OPERATION INNER JOIN NODE_DOMAIN ON NODE_OPERATION.DOMAIN_ID = NODE_DOMAIN.DOMAIN_ID "+
                        "WHERE NODE_OPERATION.WEB_SERVICE_ID = 6 AND NODE_OPERATION.REST_IND = 'Y'" + 
                        "order by NODE_DOMAIN.DOMAIN_NAME, NODE_OPERATION.OPERATION_NAME";
        DataSet ds = new DataSet();
 
        _db.GetDataSet("dataflow",sSql,ds);
        
        DataTable dt = ds.Tables[0];
        string dataflow = ""; 
        ENNodeDataFlow df = new ENNodeDataFlow();
        List<ENNodeServiceName> services = new List<ENNodeServiceName>();
        df.Services = services;
        
        foreach(DataRow dr in dt.Rows)
        {
            if (string.IsNullOrEmpty(dataflow))
            {
                dataflow = "" + dr["DOMAIN_NAME"];
                df.DataFlowName = dataflow;
                result.Add(df);
            }
            else if ((""+dr["DOMAIN_NAME"]) != dataflow)
            {
                dataflow = ""+dr["DOMAIN_NAME"];
                df = new ENNodeDataFlow();
                df.DataFlowName = dataflow;
                services = new List<ENNodeServiceName>();
                df.Services = services;
                result.Add(df);
            }
        
            ENNodeServiceName serName = new ENNodeServiceName();
            serName.ServiceName = ""+dr["OPERATION_NAME"];
            serName.ServiceID = (int)dr["OPERATION_ID"];

            df.Services.Add(serName);
        }

        return result;
    
    }

}