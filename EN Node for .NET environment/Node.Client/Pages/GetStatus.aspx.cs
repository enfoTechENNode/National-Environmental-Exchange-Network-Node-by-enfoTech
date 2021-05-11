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
using System.Web.Services.Protocols;

using Node.Core.API;

public partial class GetStatus : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;

    public GetStatus()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.ShowResult = false;
            this.ShowResult2 = false;

            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken.Text = token;
                this.txtToken2.Text = token;
            }
        }
        this.ClientLeftPanel.HighLighter(3);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }
    protected void btnGetStatus_Click(object sender, EventArgs e)
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
                this.lblResult.Text = requestor.GetStatus(this.txtToken.Text, this.txtTransID.Text);
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

    protected void btnGetStatus2_Click(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            this.SaveNodeURL(url);

            Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);
            Node.Core2.Requestor.GetStatus getsts = new Node.Core2.Requestor.GetStatus();
            getsts.securityToken = txtToken2.Text;
            getsts.transactionId = txtTransID2.Text;


            try
            {
                Node.Core2.Requestor.StatusResponseType resp = req.GetStatus(getsts);
                this.lblResult2.Text = resp.status.ToString() + " : " + resp.statusDetail; 
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
                //this.lblResult2.Text = "System error." + Environment.NewLine + ex.ToString();
                this.lblResult2.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResult2.Text = "Please Enter a Node URL";

        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }
}
