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

using Node.Core;
using Node.Core.Biz.Objects;

public partial class LeftPanel : System.Web.UI.UserControl
{
    public LeftPanel()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        object user = this.Session[Phrase.USER_SESSION_KEY];
        if (user != null)
        {
            ConsoleUser cu = new ConsoleUser(user.ToString());
            if (!cu.IsNodeAdmin)
                this.PanelItem_Configurations.Visible = false;
            else
                this.PanelItem_Configurations.Visible = true;
        }
    }

    public void HighLighter(int item)
    {
        this.AdminLeftPanel.HighlightIndex = item;
    }
}
