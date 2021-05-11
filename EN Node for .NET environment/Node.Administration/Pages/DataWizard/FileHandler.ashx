<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;

public class FileHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        int sID = int.Parse(context.Request.QueryString["OPID"].ToString());
        DataWizardService ds = new DataWizardService();
        string sText = ds.GetOperationData(sID);

                
        context.Response.Clear();
        context.Response.Buffer = true;
        //context.Response.ContentType = "application/octet-stream";        
        context.Response.AddHeader("Content-Disposition", "attachment;filename=Export_Operation_"+sID+".xml");        
        context.Response.Write(sText);        
        context.Response.Flush();
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}