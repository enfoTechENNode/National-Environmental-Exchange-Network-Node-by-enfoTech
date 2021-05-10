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


public partial class GetServices : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;

    public GetServices()
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
            ArrayList list = Operation.RetrieveGetServicesOperationNames();
            list.Insert(0, "");
            list.Insert(1, "ServiceType");
            list.Insert(2, "Interfaces");
            list.Insert(3, "Query");
            list.Insert(4, "Solicit");
            this.ddlServiceType.DataSource = list;
            this.ddlServiceType.DataBind();
        }
        this.ClientLeftPanel.HighLighter(2);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }
    protected void btnGetServices_Click(object sender, EventArgs e)
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
                string serviceType = this.ddlServiceType.SelectedValue;
                if (serviceType.Trim().Equals(""))
                    serviceType = this.txtServiceType.Text;
                string[] services = requestor.GetServices(this.txtToken.Text, serviceType);
                if (services != null && services.Length > 0)
                {
                    this.lblResult.Text = services[0];
                    for (int i = 1; i < services.Length; i++)
                        this.lblResult.Text += ", " + services[i];
                }
                else
                    this.lblResult.Text = "No Services Found";
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


    protected void btnGetServices2_Click(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            try
            {
                this.SaveNodeURL(url);
                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);
                Node.Core2.Requestor.GetServices getSvr = new Node.Core2.Requestor.GetServices();
                getSvr.securityToken = this.txtToken2.Text;
                getSvr.serviceCategory = this.drpServiceGetegory.SelectedValue.ToString();

                Node.Core2.Requestor.GenericXmlType xml = req.GetServices(getSvr);
                if (xml.Any.Length > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                   
                    foreach (System.Xml.XmlNode xNode in xml.Any)
                    {
                        sb.Append(xNode.OuterXml);     
                    }
                    this.lblResult2.Text = Server.HtmlEncode(sb.ToString());
                }
                else
                {
                    this.lblResult2.Text = "No Services Found";
                }
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
