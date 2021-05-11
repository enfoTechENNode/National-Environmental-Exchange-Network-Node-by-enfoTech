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
using Node.Core.Biz.Objects;


public partial class NodePing : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;
    private string sLineBreak = "<br />";

    public NodePing()
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
        this.ClientLeftPanel.HighLighter(0);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void btnNodePing_Click(object sender, EventArgs e)
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
                //this.lblResult.Text = requestor.NodePing(this.txtHello.Text);
                this.lblResult.Text = "Node Status: Ready" + sLineBreak + "Node Detail: " + requestor.NodePing(this.txtHello.Text);
            }
            catch (Exception ex)
            {
                //this.lblResult.Text = "System error." + sLineBreak + ex.ToString();
                //this.lblResult.Text = "Node Status: Unavailable" + sLineBreak + "Node Detail: " + ex.ToString();
                this.lblResult.Text = "Node Status: Unavailable" + sLineBreak + "Node Detail: " + ex.Message;
            }
        }
        else
            this.lblResult.Text = "Please Enter a Node URL";
        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    protected void btnNodePing2_Click(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {

            Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);
            Node.Core2.Requestor.NodePing nodePing = new Node.Core2.Requestor.NodePing();
            nodePing.hello = this.txtHello2.Text;
            try
            {
                Node.Core2.Requestor.NodePingResponse resp = req.NodePing(nodePing);

                switch (resp.nodeStatus)
                {
                    case Node.Core2.Requestor.NodeStatusCode.Ready:
                        this.lblResult2.Text = "Node Status: " + resp.nodeStatus + sLineBreak + "Node Detail: " + resp.statusDetail;
                        break;
                    case Node.Core2.Requestor.NodeStatusCode.Busy:
                    case Node.Core2.Requestor.NodeStatusCode.Unknown:
                        this.lblResult2.Text = "Node Status: " + resp.nodeStatus + sLineBreak + "Node Detail: " + resp.statusDetail;
                        break;
                    case Node.Core2.Requestor.NodeStatusCode.Offline:
                        SystemConfiguration config = new SystemConfiguration();
                        this.lblResult2.Text = "Node Status: " + resp.nodeStatus + sLineBreak + "Node Detail: " + resp.statusDetail;
                        this.lblResult2.Text += sLineBreak + config.GetNodeStatusMessage();
                        break;
                }

                //if (resp.nodeStatus == Node.Core2.Requestor.NodeStatusCode.Ready)
                //{
                //    this.lblResult2.Text = "Node Status:"+resp.nodeStatus+"<br>"+"Node Detail:"+resp.statusDetail;
                //}
                //else
                //{
                //    SystemConfiguration config = new SystemConfiguration();
                //    this.lblResult2.Text = config.GetNodeStatusMessage();
                //}
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
