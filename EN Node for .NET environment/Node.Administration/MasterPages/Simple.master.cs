using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using Node.Core;

public partial class MasterPages_Simple : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack && this.Session[Phrase.USER_SESSION_KEY] == null)
        {
            this.Response.Redirect("~/Pages/Main/Login.aspx");
        }
    }
}
