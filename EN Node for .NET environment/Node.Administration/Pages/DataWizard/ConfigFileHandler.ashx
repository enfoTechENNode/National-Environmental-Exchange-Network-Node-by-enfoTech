<%@ WebHandler Language="C#" Class="ConfigFileHandler" %>

using System;
using System.Web;

public class ConfigFileHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string fName = context.Request.QueryString["FileName"];
        string fType = context.Request.QueryString["FileType"];
        DataWizardService ds = new DataWizardService();
        string sText = ds.GetFileContent(fName, fType);

                
        context.Response.Clear();
        context.Response.Buffer = true;
        //context.Response.ContentType = "application/octet-stream";        
        context.Response.AddHeader("Content-Disposition", "attachment;filename="+fName);        
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