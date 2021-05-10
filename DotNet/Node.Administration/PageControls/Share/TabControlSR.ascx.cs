using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageControls_Share_TabControlSR : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        char[] separate = { '/' };
        string[] Pages = Request.AppRelativeCurrentExecutionFilePath.Split(separate);

        string sPage = Pages[Pages.Length - 1];
        int i = -1;
        switch (sPage)
        {

            case "NodeRegistration.aspx":
                this.TabCtl.SelectedIndex = 0;
                break;

            case "DEDLConfig.aspx":
                this.TabCtl.SelectedIndex = 1;
                break;

            default:
                i = -1;
                break;
        }
        if (i != -1)
        {
            this.TabCtl.SelectedIndex = i;
        }
    }
}