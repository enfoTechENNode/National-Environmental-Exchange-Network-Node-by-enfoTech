using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class PageControls_NodeTabLink : System.Web.UI.UserControl
{
    private AjaxControlToolkit.TabContainer ajaxTabContrl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ("" + ConfigurationManager.AppSettings["SkipNavLink"] != "" && ConfigurationManager.AppSettings["SkipNavLink"].ToString().Equals("True"))
        {
            this.LnkNode1.Visible = true;
            this.LnkNode2.Visible = true;
        }
        else
        {
            this.LnkNode2.Visible = false;
            this.LnkNode1.Visible = false;
        }
    }
    protected void LnkNode1_Click(object sender, EventArgs e)
    {
        ajaxTabContrl.ActiveTabIndex = 0;
    }
    protected void LnkNode2_Click(object sender, EventArgs e)
    {
        ajaxTabContrl.ActiveTabIndex = 1;
    }

    public AjaxControlToolkit.TabContainer SetPageAjaxTabControl
    {
        set { ajaxTabContrl = value; }
    }
}
