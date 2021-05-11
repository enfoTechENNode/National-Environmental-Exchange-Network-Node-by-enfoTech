using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Core.Biz.Objects;

public partial class Pages_Utilities_Error : Node.Core.UI.Base.AdminPageBase
{
    public Pages_Utilities_Error()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    }

}
