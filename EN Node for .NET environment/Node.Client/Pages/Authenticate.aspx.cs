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

using Node.Core.API;
using System.Web.Services.Protocols;

public partial class Authenticate : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;

    public Authenticate()
	{
		Load += new EventHandler(Page_Load);
	}

    protected void Page_Load(object sender, EventArgs e)
    {
        NodeTab1.SetPageAjaxTabControl = this.TabContainer1;
        if (!this.IsPostBack)
        {
            this.ShowResult = false;
            this.ShowResult2 = false;
        }
        this.ClientLeftPanel.HighLighter(1);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void btnAuthenticate_Click(object sender, EventArgs e)
    {
        this.lblResult.Text = "";
        this.ShowResult = true;
        string url = this.NodeURLsPanel.GetNodeURL();
        if (url != null)
        {
            this.SaveNodeURL(url);
            NodeRequestor requestor = new NodeRequestor(url);
            try
            {
                this.lblResult.Text = requestor.Authenticate(this.txtUserId1.Text, this.txtPassword1.Text, this.txtAuthenticationMethod1.Text);
                this.SaveSecurityToken(this.lblResult.Text);
            }
            catch (Exception ex)
            {
                //this.lblResult.Text = "System error." + Environment.NewLine + ex.ToString();
                this.lblResult.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResult.Text = "Please Enter a Node URL";
        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    protected void btnAuthenticate2_Click(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            this.SaveNodeURL(url);

            Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);
            Node.Core2.Requestor.Authenticate auth = new Node.Core2.Requestor.Authenticate();
            auth.userId = this.txtUserId2.Text;
            auth.credential = this.txtPassword2.Text;
            auth.authenticationMethod = this.txtAuthenticationMethod2.Text;
            auth.domain = this.txtDomain.Text;
            try
            {
                Node.Core2.Requestor.AuthenticateResponse resp = req.Authenticate(auth);
                this.lblResult2.Text = resp.securityToken;
                this.SaveSecurityToken(resp.securityToken);
            }
            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    this.lblResult2.Text = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    this.lblResult2.Text = "System error." + Environment.NewLine + sex.Message;
                }
            }
            catch (Exception ex)
            {
                this.lblResult2.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResult2.Text = "Please Enter a Node URL";
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }
}
