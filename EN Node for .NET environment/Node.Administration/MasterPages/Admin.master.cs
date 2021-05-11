using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin : System.Web.UI.MasterPage
{
    public string PageDescription
    {
        get { return this.pageDesc.InnerHtml; }
        set { this.pageDesc.InnerHtml = value; }
    }

    public Admin()
    {
        Load += new EventHandler(Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.PageDescription == null || this.PageDescription == "")
        {
            this.yellowBub.Visible = false;
        }
        this.lblVersion.Text = Session[Node.Core.Phrase.VERSION_NO] + "" == Node.Core.Phrase.VERSION_11 ? "(Node Version: 1.1)" : "(Node Version: 2.0)";
    }


    //protected void image0_Click(object sender, EventArgs e)
    //{
    //    this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    //}
}
