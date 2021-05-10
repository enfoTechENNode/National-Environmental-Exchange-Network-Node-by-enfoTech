using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Node.Lib.AppSystem;
using Node.Core;
using Node.Core.Biz.Objects;

public partial class PageControls_WebParts_Configuration : Node.Core.UI.Base.AdminUserControlBase
{
    protected SystemConfiguration config = null;
    protected ManageOperation objManageOp = null;
    private string strDomainName = string.Empty;
    //private int intOPID = -1;
    private string strOPType = string.Empty;
    //private int intWebService = -1;
    private string strStatus = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        config = new SystemConfiguration();
        if (!this.IsPostBack)
            this.PageControlsInit();

        ImgNode.Attributes.Add("onclick", "fnSetFocus('" + txtNodeName.ClientID + "');");
        ImgNAAS.Attributes.Add("onclick", "fnSetFocus('" + txtNodeAdminName.ClientID + "');");
        ImgEmail.Attributes.Add("onclick", "fnSetFocus('" + txtEmailServerAddress.ClientID + "');");
        ImgAppServer.Attributes.Add("onclick", "fnSetFocus('" + cbProxy.ClientID + "');");
        ImgWebClient.Attributes.Add("onclick", "fnSetFocus('" + txtClientURLs.ClientID + "');");
        ImgDefaultSettings.Attributes.Add("onclick", "fnSetFocus('" + txtRowNo.ClientID + "');");

    }
    

    protected void btnSaveNode_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            //if (string.IsNullOrEmpty(this.rblNodeStatus.SelectedValue)
            //    || string.IsNullOrEmpty(this.txtTokenLifetime.Text.Trim())
            //    || string.IsNullOrEmpty(this.txtNodeName.Text.Trim())
            //    || string.IsNullOrEmpty(this.txtStatusMessage.Text.Trim()))
            //{
            //    this.lblError.Text = "Node Status, name, token life and status message are required.";
            //    this.lblError.Visible = true;
            //    return;
            //}

            if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
            {
                // Node Status
                config.SetNodeStatus_V2(this.rblNodeStatus.SelectedValue);

                // Token Life Time
                config.SetTokenLifeTime_V2(this.txtTokenLifetime.Text);

                // Node Name
                config.SetNodeName_V2(this.txtNodeName.Text);

                // Node Status Message
                config.SetNodeStatusMessage_V2(this.txtStatusMessage.Text);

                // Node Address
                config.SetNodeAddress_V2(this.txtNodeAddress.Text);
            }
            else
            {
                // Node Status
                config.SetNodeStatus(this.rblNodeStatus.SelectedValue);

                // Token Life Time
                config.SetTokenLifeTime(this.txtTokenLifetime.Text);

                // Node Name
                config.SetNodeName(this.txtNodeName.Text);

                // Node Status Message
                config.SetNodeStatusMessage(this.txtStatusMessage.Text);

                // Node Address
                config.SetNodeAddress(this.txtNodeAddress.Text);
            }
            
            config.SaveConfiguration();

            Response.Redirect("~/Pages/Main/Home.aspx", true);
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void btnSaveNAAS_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(this.txtNAASServerAddress.Text.Trim())
        //        || string.IsNullOrEmpty(this.txtNAASUserMgmAddress.Text.Trim())
        //        || string.IsNullOrEmpty(this.txtNAASPolicyMgmAddress.Text.Trim())
        //        || string.IsNullOrEmpty(this.txtNodeAdminUserID.Text.Trim()))
        //{
        //    this.lblError.Text = "Node Admin User ID, NAAS Authentication/Authorization Server Address, User Management Server Address and Policy Management Server Address are required.";
        //    this.lblError.Visible = true;
        //    return;
        //}

        config.SetNAASAuthenticationAddress(this.txtNAASServerAddress.Text);
        config.SetNAASUserManagementAddress(this.txtNAASUserMgmAddress.Text);
        config.SetNAASPolicyManagementAddress(this.txtNAASPolicyMgmAddress.Text);

        config.SetNodeAdministratorName(this.txtNodeAdminName.Text);
        config.SetNodeAdministratorUserID(this.txtNodeAdminUserID.Text);
        if (!string.IsNullOrEmpty(this.txtNodeAdminPassword.Text))
            config.SetNodeAdministratorPassword(this.txtNodeAdminPassword.Text);

        config.SaveConfiguration();                
    }
    protected void btnSaveEmailTemplate_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(this.txtEmailServerAddress.Text.Trim())
        //    || string.IsNullOrEmpty(this.txtEmailPort.Text.Trim())
        //    || string.IsNullOrEmpty(this.txtUserEmailSender.Text.Trim())
        //    || string.IsNullOrEmpty(this.txtUserTemplate.Text.Trim())
        //    || string.IsNullOrEmpty(this.txtTaskEmailSender.Text.Trim())
        //    || string.IsNullOrEmpty(this.txtTaskTemplate.Text.Trim()))
        //{
        //    this.lblError.Text = "The Email Server Host/Port, User Email Sender/Template and Task Email Sender/Template are required.";
        //    this.lblError.Visible = true;
        //    return;
        //}

        config.SetEmailServerHost(this.txtEmailServerAddress.Text);
        config.SetEmailServerPort(this.txtEmailPort.Text);
        config.SetEmailServerUserID(this.txtEmailUserID.Text);
        config.SetEmailServerPassword(this.txtEmailPassword.Text);
        config.SetEmailServerFrom(this.txtEmailFrom.Text);

        Node.Core.Biz.Manageable.EmailManager manager = new Node.Core.Biz.Manageable.EmailManager();

        EmailTemplate userTemplate = manager.GetUserTemplate();
        userTemplate.From = this.txtUserEmailSender.Text;
        userTemplate.CcList = this.txtUserCCList.Text;
        userTemplate.BccList = this.txtUserBCCList.Text;
        userTemplate.Content = this.txtUserTemplate.Text;
        manager.SaveUserTemplate(userTemplate);

        EmailTemplate taskTemplate = manager.GetTaskTemplate();
        taskTemplate.From = this.txtTaskEmailSender.Text;
        taskTemplate.CcList = this.txtTaskCCList.Text;
        taskTemplate.BccList = this.txtTaskBCCist.Text;
        taskTemplate.Content = this.txtTaskTemplate.Text;
        manager.SaveTaskTemplate(taskTemplate);

        config.SaveConfiguration(); 
    }
    protected void btnSaveAppServer_Click(object sender, EventArgs e)
    {
        if (this.cbProxy.Checked)
            config.EnableProxy(this.txtProxyServerAddress.Text, this.txtProxyUserID.Text, this.txtProxyPassword.Text);
        else
        {
            config.DisableProxy();
            this.txtProxyServerAddress.Text = "";
            this.txtProxyUserID.Text = "";
            this.txtProxyPassword.Text = "";
        }

        config.SetAdminLogLevel(this.ddlAdminLogLevel.SelectedValue);
        config.SetClientLogLevel(this.ddlClientLogLevel.SelectedValue);
        config.SetTaskLogLevel(this.ddlTaskLogLevel.SelectedValue);
        config.SetWSLogLevel(this.ddlWebServicesLogLevel.SelectedValue);

        config.SaveConfiguration(); 
    }
    protected void btnSaveWebClient_Click(object sender, EventArgs e)
    {
        ArrayList list = new ArrayList();
        string[] split = this.txtClientURLs.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in split)
            list.Add(s.Replace("\r", ""));

        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
        {
            config.SetClientWebServicesURLs_V2(list);
        }
        else
        {
            config.SetClientWebServicesURLs(list);
        }
        config.SaveConfiguration(); 

    }
    protected void btnSaveSettings_Click(object sender, EventArgs e)
    {
        config.SetDefaultRownum(this.txtRowNo.Text.Trim());
        config.SetDefaultTopnum(this.txtTopNo.Text.Trim());
        config.SetDefaultPageSize(this.txtPageSize.Text.Trim());
        config.SaveConfiguration();
        Session[Phrase.DEFAULT_ROWNUM] = this.txtRowNo.Text.Trim();
        Session[Phrase.DEFAULT_TOPNUM] = this.txtTopNo.Text.Trim();
        Session[Phrase.DEFAULT_PAGESIZE] = this.txtPageSize.Text.Trim();
        Response.Redirect("~/Pages/Main/Home.aspx", true);
    }

    protected void btnSaveRESTful_Click(object sender, EventArgs e)
    {
        string sTitle = "RESTful Services Page Header";
        string sContent = "RESTful Services Page Description";

        config.SetRESTfulPageHeader((string.IsNullOrEmpty(txtRESTfulTitle.Text)?sTitle:txtRESTfulTitle.Text));
        config.SetRESTfulPageContent((string.IsNullOrEmpty(txtRESTfulDesc.Text) ? sContent : txtRESTfulDesc.Text));

        config.SaveConfiguration();
    }

    protected void ImgOPManager_onClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/OperationManager/OperationConfig.aspx");
    }

    private void PageControlsInit()
    {
        // Initialize Controls
        //SystemConfiguration config = new SystemConfiguration();


        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
        {
            // Node Status
            string nodeStatus2 = config.GetNodeStatus_V2();

            if (nodeStatus2.Equals(Phrase.STATUS_RUNNING))
                this.rblNodeStatus.SelectedIndex = 0;
            else
                this.rblNodeStatus.SelectedIndex = 1;

            // Token Life Time
            this.txtTokenLifetime.Text = config.GetTokenLifeTime_V2();

            // Node Name
            this.txtNodeName.Text = config.GetNodeName_V2();

            // Node Status Message
            this.txtStatusMessage.Text = config.GetNodeStatusMessage_V2();

            // Node Address
            this.txtNodeAddress.Text = config.GetNodeAddress_V2();

        }
        else
        {
            // Node Status
            string nodeStatus = config.GetNodeStatus();

            if (nodeStatus.Equals(Phrase.STATUS_RUNNING))
                this.rblNodeStatus.SelectedIndex = 0;
            else
                this.rblNodeStatus.SelectedIndex = 1;

            // Token Life Time
            this.txtTokenLifetime.Text = config.GetTokenLifeTime();

            // Node Name
            this.txtNodeName.Text = config.GetNodeName();

            // Node Status Message
            this.txtStatusMessage.Text = config.GetNodeStatusMessage();

            // Node Address
            this.txtNodeAddress.Text = config.GetNodeAddress();
        }

        // Get NAAS URLs
        this.txtNAASServerAddress.Text = config.GetNAASAuthenticationAddress();
        this.txtNAASUserMgmAddress.Text = config.GetNAASUserManagementAddress();
        this.txtNAASPolicyMgmAddress.Text = config.GetNAASPolicyManagementAddress();

        // Get Node Administrator Details
        this.txtNodeAdminName.Text = config.GetNodeAdministratorName();
        this.txtNodeAdminUserID.Text = config.GetNodeAdministratorUserID();
        this.txtNodeAdminPassword.Text = config.GetNodeAdministratorPassword();

        this.txtEmailServerAddress.Text = config.GetEmailServerHost();
        this.txtEmailPort.Text = config.GetEmailServerPort();
        this.txtEmailUserID.Text = config.GetEmailServerUserID();
        this.txtEmailPassword.Text = config.GetEmailServerPassword();
        this.txtEmailFrom.Text = config.GetEmailServerFrom();

        Node.Core.Biz.Manageable.EmailManager manager = new Node.Core.Biz.Manageable.EmailManager();

        EmailTemplate userTemplate = manager.GetUserTemplate();
        this.txtUserEmailSender.Text = userTemplate.From;
        this.txtUserCCList.Text = userTemplate.CcList;
        this.txtUserBCCList.Text = userTemplate.BccList;
        this.txtUserTemplate.Text = userTemplate.Content;

        EmailTemplate taskTemplate = manager.GetTaskTemplate();
        this.txtTaskEmailSender.Text = taskTemplate.From;
        this.txtTaskCCList.Text = taskTemplate.CcList;
        this.txtTaskBCCist.Text = taskTemplate.BccList;
        this.txtTaskTemplate.Text = taskTemplate.Content;

        string host = config.GetProxyHost();
        if (host != null)
        {
            this.cbProxy.Checked = true;
            this.txtProxyServerAddress.Text = host;
        }
        else
            this.cbProxy.Checked = false;
        string uid = config.GetProxyUID();
        if (uid != null)
            this.txtProxyUserID.Text = uid;
        string pwd = config.GetProxyPWD();
        if (pwd != null)
            this.txtProxyPassword.Text = pwd;

        this.ddlAdminLogLevel.Text = "" + config.GetAdminLogLevel();
        this.ddlClientLogLevel.Text = "" + config.GetClientLogLevel();
        this.ddlTaskLogLevel.Text = "" + config.GetTaskLogLevel();
        this.ddlWebServicesLogLevel.Text = "" + config.GetWSLogLevel();

        ArrayList list = null;
        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
        {
            list = config.GetClientWebServiceURLs_V2();
        }
        else
        {
            list = config.GetClientWebServiceURLs();
        }
        string input = "";
        foreach (object obj in list)
        {
            if (input.Length > 0)
                input += "\n";
            input += "" + obj;
        }
        this.txtClientURLs.Text = input;

        //For Default Settings
        this.txtRowNo.Text = config.GetDefaultRownum();
        this.txtTopNo.Text = config.GetDefaultTopnum();
        this.txtPageSize.Text = config.GetDefaultPageSize();

        bool bNodeDomainAdmin = (bool)Session[Phrase.NODE_DOMAIN_ADMIN];
        if (!bNodeDomainAdmin)
        {
            PnlConfig.Visible = false;
        }

        this.txtRESTfulTitle.Text = config.GetRESTfulPageHeader();
        this.txtRESTfulDesc.Text = config.GetRESTfulPageContent();

    }
}
