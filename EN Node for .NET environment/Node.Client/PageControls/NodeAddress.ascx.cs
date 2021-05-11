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

using Node.Core.Biz.Objects;
using Node.Core.UI.Base;

public partial class NodeAddress : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SystemConfiguration config = new SystemConfiguration();
            ArrayList urls = config.GetClientWebServiceURLs();
            urls.Insert(0, "");
            this.ddlNodeAddress.DataSource = urls;
            this.ddlNodeAddress.DataBind();

            if (this.Session[ClientPageBase.NODE_URL_SESSION_KEY] != null)
            {
                string savedURL = (string)this.Session[ClientPageBase.NODE_URL_SESSION_KEY];
                for (int i = 0; i < this.ddlNodeAddress.Items.Count; i++)
                {
                    if (savedURL.Equals(this.ddlNodeAddress.Items[i].Value))
                    {
                        this.ddlNodeAddress.SelectedIndex = i;
                        break;
                    }
                }
                if (this.ddlNodeAddress.SelectedValue.Trim().Equals(""))
                    this.txtNodeAddress.Text = savedURL;
            }
        }
    }

    public string GetNodeURL()
    {
        if (!this.ddlNodeAddress.SelectedValue.Trim().Equals(""))
            return this.ddlNodeAddress.SelectedValue;
        else if (!this.txtNodeAddress.Text.Trim().Equals(""))
            return this.txtNodeAddress.Text;
        return null;
    }
}
