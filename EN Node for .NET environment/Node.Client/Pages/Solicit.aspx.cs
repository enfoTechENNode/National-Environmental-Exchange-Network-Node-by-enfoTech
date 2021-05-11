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
using Node.Core.Biz.Handler;


public partial class Solicit : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected int ParameterCount = 0;

    protected bool ShowResult2 = false;
    protected int ParameterCount2 = 0;

    public Solicit()
    {
        this.Load += new EventHandler(this.Page_Load);
        this.Init += new EventHandler(Solicit_Init);
    }

    protected void Solicit_Init(object sender, EventArgs e)
    {
        string[] sNotificationType = Enum.GetNames(typeof(Node.Core2.Requestor.NotificationTypeCode));
        foreach (string str in sNotificationType)
        {
            this.DropDownList4.Items.Add(new ListItem(str, str));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        NodeTab1.SetPageAjaxTabControl = this.TabContainer1;
        if (!this.IsPostBack)
        {
            this.ShowResult = false;

            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken.Text = token;
                this.txtToken2.Text = token;
            }
            ArrayList list = Operation.RetrieveSolicitOperationNames(BaseHandler.NodeVer.VER_11.ToString());
            list.Insert(0, "");
            this.ddlRequest.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.ddlRequest.Items.Add(new ListItem("" + list[i], "" + list[i]));
            }

            list = Operation.RetrieveSolicitOperationNames(BaseHandler.NodeVer.VER_20.ToString());
            list.Insert(0, "");
            this.ddlRequest2.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                this.ddlRequest2.Items.Add(new ListItem("" + list[i], "" + list[i]));
            }
        }
        this.ClientLeftPanel.HighLighter(7);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void ddlRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblResult.Text = "";
        dcpDynamicParams.Controls.Clear();
        if (this.ddlRequest.SelectedValue.Trim().Equals(""))
        {
            this.ParameterCount = 0;
        }
        else
        {
            try
            {
                Hashtable pairs = Operation.RetrieveSolicitParameterNames();
                string hashValue = "" + pairs[this.ddlRequest.SelectedValue];
                if (hashValue != null && !hashValue.Trim().Equals(""))
                {
                    string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    if (split != null && split.Length > 0)
                    {
                        this.ParameterCount = split.Length;
                        //loop parameters
                        for (int i = 0; i < split.Length; i++)
                        {
                            LiteralControl lcPrefix = new LiteralControl();
                            lcPrefix.ID = "lcPrefix" + i;
                            dcpDynamicParams.Controls.Add(lcPrefix);
                            lcPrefix.Text = "<tr class=\"alt1\" valign=\"top\"><td></td><td>" + split[i] + "<br />";
                            TextBox txtParam = new TextBox();
                            txtParam.ID = "txtParam" + i;
                            dcpDynamicParams.Controls.Add(txtParam);
                            txtParam.Text = "";
                            txtParam.Width = 300;
                            LiteralControl lcPostfix = new LiteralControl();
                            lcPostfix.ID = "lcPostfix" + i;
                            dcpDynamicParams.Controls.Add(lcPostfix);
                            lcPostfix.Text = "</td></tr>";
                        }
                    }
                    else
                        this.ParameterCount = 0;
                }
                else
                    this.ParameterCount = 0;
            }
            catch (Exception ex)
            {
                //this.lblResult.Text = "System error." + Environment.NewLine + ex.ToString();
                this.lblResult.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        //save to session
        this.Session.Add("ParamCount", this.ParameterCount);
        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    protected void btnSolicit_Click(object sender, EventArgs e)
    {
        string solicitResult = "";

        this.lblResult.Text = "";
        this.ShowResult = true;
        string url = this.NodeURLsPanel.GetNodeURL();
        if (url != null)
        {
            try
            {
                this.SaveNodeURL(url);
                NodeRequestor requestor = new NodeRequestor(url);
                string request = this.ddlRequest.SelectedValue;
                if (request == null || request.Trim().Equals(""))
                    request = this.txtRequest.Text;
                solicitResult = requestor.Solicit(this.txtToken.Text, 
                    this.txtReturnURL.Text, request, this.GetParameters());
            }
            catch (Exception ex)
            {
                //solicitResult = "System error." + Environment.NewLine + ex.ToString();
                solicitResult = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            solicitResult = "Please Enter a Node URL";
        this.ddlRequest_SelectedIndexChanged(null, null);

        this.lblResult.Text = solicitResult;
        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    private string[] GetParameters()
    {
        string[] retParams = null;
        int totalParamCount = this.dcpDynamicParams.Controls.Count / 3;
        if (totalParamCount > 0)
        {
            retParams = new string[totalParamCount];
            for (int i = 0; i < totalParamCount; i++)
                retParams[i] = ((TextBox)this.dcpDynamicParams.FindControl("txtParam" + i)).Text.Trim();
        }
        return retParams;
    }


    protected void ddlRequest2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.dcpDynamicParams2.Controls.Clear();

        if (this.ddlRequest2.SelectedValue.Trim().Equals(""))
        {
            this.ParameterCount2 = 0;
        }
        else
        {
            try
            {
                Hashtable pairs = Operation.RetrieveSolicitParameterNames();
                string hashValue = "" + pairs[this.ddlRequest2.SelectedValue];
                if (hashValue != null && !hashValue.Trim().Equals(""))
                {
                    string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    if (split != null && split.Length > 0)
                    {
                        this.ParameterCount2 = split.Length;
                        //loop parameters
                        for (int i = 0; i < split.Length; i++)
                        {
                            LiteralControl lcPrefix = new LiteralControl();
                            lcPrefix.ID = "lcPrefix" + i;
                            dcpDynamicParams2.Controls.Add(lcPrefix);
                            lcPrefix.Text = "<tr class=\"alt1\" valign=\"top\"><td></td><td>" + split[i] + "<br />";
                            TextBox txtParam = new TextBox();
                            txtParam.ID = "txtParam" + i;
                            dcpDynamicParams2.Controls.Add(txtParam);
                            txtParam.Text = "";
                            txtParam.Width = 300;
                            LiteralControl lcPostfix = new LiteralControl();
                            lcPostfix.ID = "lcPostfix" + i;
                            dcpDynamicParams2.Controls.Add(lcPostfix);
                            lcPostfix.Text = "</td></tr>";
                        }
                    }
                    else
                        this.ParameterCount2 = 0;
                }
                else
                    this.ParameterCount2 = 0;
            }
            catch (Exception ex)
            {
                //this.lblResult.Text = "System error." + Environment.NewLine + ex.ToString();
                this.lblResult.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        //save to session
        this.Session.Add("ParamCount2", this.ParameterCount2);
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

    protected void btnSolicit2_Click(object sender, EventArgs e)
    {
        string solicitResult = "";

        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            try
            {
                this.SaveNodeURL(url);
                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);

                string request = this.ddlRequest2.SelectedValue;
                if (request == null || request.Trim().Equals(""))
                    request = this.txtRequest2.Text;

                Node.Core2.Requestor.Solicit sol = new Node.Core2.Requestor.Solicit();
                sol.securityToken = this.txtToken2.Text;
                sol.dataflow = this.txtDataFlow.Text;
                sol.request = request;
                sol.recipient = this.txtRecipient.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                sol.notificationURI = new Node.Core2.Requestor.NotificationURIType[1];
                sol.notificationURI[0] = new Node.Core2.Requestor.NotificationURIType();
                sol.notificationURI[0].notificationType = (Node.Core2.Requestor.NotificationTypeCode)Enum.Parse(typeof(Node.Core2.Requestor.NotificationTypeCode), this.DropDownList4.SelectedValue.ToString());
                sol.notificationURI[0].Value = this.txtNotificationURI.Text.Trim();
                
                Node.Core2.Requestor.NotificationURIType []notifyURI = new Node.Core2.Requestor.NotificationURIType[1];
                notifyURI[0] = new Node.Core2.Requestor.NotificationURIType();
                sol.notificationURI = notifyURI;
                sol.notificationURI[0].Value = this.txtNotificationURI.Text;

                string[] paraName = this.GetParameters2_Name();
                string[] paraValue = this.GetParameters2();

                if (paraName != null)
                {
                    sol.parameters = new Node.Core2.Requestor.ParameterType[paraName.Length];

                    for (int i = 0; i < paraName.Length; i++)
                    {
                        sol.parameters[i] = new Node.Core2.Requestor.ParameterType();
                        sol.parameters[i].parameterName = paraName[i];
                        sol.parameters[i].Value = paraValue[i];
                    }
                }
                else
                {
                    sol.parameters = null;
                }
                Node.Core2.Requestor.StatusResponseType resp = req.Solicit(sol);

                solicitResult = "Transation ID:" + resp.transactionId + "<br />Status:" + resp.status + "<br />Status Detail:" + resp.statusDetail;

            }

            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    solicitResult = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    solicitResult = "System error." + Environment.NewLine + sex.Message;
                }

            }

            catch (Exception ex)
            {
                //solicitResult = "System error." + Environment.NewLine + ex.ToString();
                solicitResult = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            solicitResult = "Please Enter a Node URL";
        this.ddlRequest2_SelectedIndexChanged(null, null);

        this.lblResult2.Text = solicitResult;
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

    private string[] GetParameters2()
    {
        string[] retParams = null;
        int totalParamCount = this.dcpDynamicParams2.Controls.Count / 3;
        if (totalParamCount > 0)
        {
            retParams = new string[totalParamCount];
            for (int i = 0; i < totalParamCount; i++)
                retParams[i] = ((TextBox)this.dcpDynamicParams2.FindControl("txtParam" + i)).Text.Trim();
        }
        return retParams;
    }

    private string[] GetParameters2_Name()
    {
        string[] retParams = null;

        Hashtable pairs = Operation.RetrieveSolicitParameterNames();
        string hashValue = "" + pairs[this.ddlRequest2.SelectedValue];
        if (hashValue != null && !hashValue.Trim().Equals(""))
        {
            string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (split != null && split.Length > 0)
            {
                retParams = new string[split.Length];
                //loop parameters
                for (int i = 0; i < split.Length; i++)
                {
                    retParams[i] = split[i];
                }
            }
        }
        return retParams;
    }

}
