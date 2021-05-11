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

using Node.Lib.UI.WebUtils;

public partial class GenericHelp_aspx : Node.Core.UI.Base.AdminPageBase
{
    public GenericHelp_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.txtTitle.Text = TextResource.GetValue(this.ID, "title." + this.Request["helpKey"]);
            this.txtHelp.Text = TextResource.GetValue(this.ID, "message." + this.Request["helpKey"]);
        }
    }
}
