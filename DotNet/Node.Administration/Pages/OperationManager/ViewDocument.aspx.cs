using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using Node.Lib.Utility;
using Node.Lib.Utility.Zip;
using Node.Core.Biz.Objects;
using Node.Core;

public partial class Pages_OperationManager_ViewDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = "" + Session[Phrase.TRANSFORMRESULT];
        //byte[] binaryContent = new System.Text.ASCIIEncoding().GetBytes(contents);
        //this.Response.Clear();
        //this.Response.ContentType = "application/octet-stream";
        //this.Response.AddHeader("Content-Disposition", "attachment; filename=" + "review.dat");
        //this.Response.BinaryWrite(binaryContent);
        this.Response.Output.Write(result);
        this.Response.End();
    }

}
