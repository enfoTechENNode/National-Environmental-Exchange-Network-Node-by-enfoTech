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

public partial class LeftPanel : System.Web.UI.UserControl
{
	public LeftPanel()
	{
		Load += new EventHandler(Page_Load);
	}

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void HighLighter(int item)
    {
        this.ClientLeftPanel.HighlightIndex = item;
    }
}
