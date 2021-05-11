using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Core.Biz.Objects;

public partial class Pages_Error : Node.Core.UI.Base.ClientPageBase
{
    public Pages_Error()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
