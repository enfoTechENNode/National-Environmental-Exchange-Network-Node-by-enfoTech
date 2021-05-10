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

public partial class Client : System.Web.UI.MasterPage
{
    private string pgTitle = "Client";
    private string imgUrl = "/App_Images/Node/Node_gen.gif";

    public string PageTitle
    {
        get { return this.pgTitle; }
        set
        {
            this.pgTitle = value;
            this.Page.Header.Title = "Node - " + value;
        }
    }

    public string PageDescription
    {
        get { return this.pageDesc.InnerHtml; }
        set { this.pageDesc.InnerHtml = value; }
    }

    public string ImageURL
    {
        get { return Request.ApplicationPath + this.imgUrl; }
        set { this.imgUrl = value; }
    }

    public Client()
    {
		Load += new EventHandler(Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.PageDescription == null || this.PageDescription == "")
        {
            this.yellowBub.Visible = false;
        }
    }

}
