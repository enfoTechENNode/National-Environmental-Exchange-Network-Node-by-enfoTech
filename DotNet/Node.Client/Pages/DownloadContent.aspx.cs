using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Node.Lib.Utility;
using Node.Core.Document;

public partial class DownloadContent : Node.Core.UI.Base.ClientPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string returnDocI = "" + this.Request["returnDocI"];
            string returnDocTypeI = "" + this.Request["returnDocTypeI"];
            if (returnDocI.Trim() != String.Empty)
            {
                object obj = this.Session["RETURN_DOCS" + returnDocI];
                if (obj != null)
                {
                    NodeDocument doc = (NodeDocument)obj;
                    byte[] content = (byte[])doc.content;
                    Response.Clear();
                    Response.ContentType = doc.type;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + doc.name);
                    if (content != null && content.Length > 0)
                        Response.OutputStream.Write(content, 0, content.Length);
                    Response.Flush();
                    Response.End();
                }
            }
            else if (returnDocTypeI.Trim() != String.Empty)
            {
                object obj = this.Session["RETURN_DOCS_TYPE" + returnDocTypeI];
                if (obj != null)
                {
                    Node.Core2.Requestor.NodeDocumentType doc = (Node.Core2.Requestor.NodeDocumentType)obj;
                    byte[] content = (byte[])doc.documentContent.Value;
                    Response.Clear();
                    if (doc.documentFormat.ToString().ToUpper() == "XML")
                        Response.ContentType = "text/xml";
                    else if (doc.documentFormat.ToString().ToUpper() == "ZIP")
                        Response.ContentType = "application/zip";
                    else if (doc.documentFormat.ToString().ToUpper() == "Image")
                        Response.ContentType = "image/png";
                    else if (doc.documentFormat.ToString().ToUpper() == "TEXT")
                        Response.ContentType = "text/plain";

                    Response.AppendHeader("content-disposition", "attachment; filename=" + doc.documentName);
                    if (content != null && content.Length > 0)
                        Response.OutputStream.Write(content, 0, content.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ASCIIEncoding ae = new ASCIIEncoding();
            Hashtable ht = new Hashtable();
            ht.Add("Download.txt", ae.GetBytes("System error." + Environment.NewLine + ex.ToString()));
            WinZip wz = new WinZip();
            byte[] content = wz.CreateZip(ht);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", "attachment; filename=Download.zip");
            if (content != null && content.Length > 0)
                Response.OutputStream.Write(content, 0, content.Length);
            Response.Flush();
            Response.End();
        }
    }
}
