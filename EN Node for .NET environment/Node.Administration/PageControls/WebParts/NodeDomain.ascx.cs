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

using Node.Core;
using Node.Core.Biz.Objects;

public partial class PageControls_WebParts_NodeDomain : Node.Core.UI.Base.AdminUserControlBase
{
    #region Private Variables
    private string loggedUser = string.Empty;
    private int defaultTopNo = 5;
    private int defaultPageSize = 10;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Phrase.DEFAULT_TOPNUM] != null && !int.TryParse(Session[Phrase.DEFAULT_TOPNUM].ToString(), out defaultTopNo))
        {
            defaultTopNo = 5;
        }
        if (Session[Phrase.DEFAULT_PAGESIZE] != null && !int.TryParse(Session[Phrase.DEFAULT_PAGESIZE].ToString(), out defaultPageSize))
        {
            defaultPageSize = 10;
        }
        this.egvDomainGrid.PageSize = defaultPageSize;
        this.imgTop5Domain.ToolTip = "Top " + defaultTopNo + " Domains";
        this.egvDomainGrid.RowCommand += new GridViewCommandEventHandler(this.egvDomainGrid_RowCommand);
        this.egvAdmins.RowDataBound += new GridViewRowEventHandler(this.egvAdmins_RowDataBound);
        if (!this.IsPostBack)
            this.PageControlsInit();

        imgSearchDomain.Attributes.Add("onclick", "fnSetFocus('" + ddlDomainName.ClientID + "');");
        btnAddNewDomain.Attributes.Add("onclick", "fnSetFocus('" + txtDomainNameNew.ClientID + "');");
        egvAdmins.PageIndexChanged += new EventHandler(egvAdmins_PageIndexChanged);
    }

    void egvAdmins_PageIndexChanged(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Show();
    }




    protected void btnSearchDomain_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DataTable dt = Domain.SearchDomains(this.ddlDomainName.SelectedValue, this.ddlStatus.SelectedValue, this.LoggedInUser);
            if (dt != null && dt.Rows.Count > defaultPageSize)
            {
                ViewState["Domain"] = dt;
                this.egvDomainGrid.CachedDataTable = dt;
                this.egvDomainGrid.DataBind();
                this.egvDomainGrid.Visible = true;
                this.dataLstDomain.Visible = false;
            }
            else
            {
                this.dataLstDomain.DataSource = dt;
                this.dataLstDomain.DataBind();
                this.dataLstDomain.Visible = true;
                this.egvDomainGrid.Visible = false;
            }
            TotalDomain.Text = dt.Rows.Count.ToString();
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

    protected void imgTop5Domain_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DataTable dt = Domain.SearchDomains("", "", this.LoggedInUser);
            DataTable newDT = dt.Clone();
            //DataRow newRow = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i < defaultTopNo)
                    {
                        newDT.ImportRow(dr);
                        i++;
                    }
                    else
                        break;
                }
            }
            this.dataLstDomain.DataSource = newDT;
            this.dataLstDomain.DataBind();
            this.dataLstDomain.Visible = true;
            this.egvDomainGrid.Visible = false;
            TotalDomain.Text = newDT.Rows.Count.ToString();
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

    protected void ShowDomainInfo_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender2.Show();
        LinkButton btnInfo = (LinkButton)sender;
        if (btnInfo != null)
        {
            SetDomainInfo(btnInfo.CommandArgument);
        }
        Page.SetFocus(ddlStatusDetail);
    }
    protected void btnGoToOperation_Click(object sender, EventArgs e)
    {
        LinkButton btnInfo = (LinkButton)sender;
        if (btnInfo != null)
        {            
            if (this.Session["DOMAIN_NAME"] != null)
                this.Session.Remove("DOMAIN_NAME");
            this.Session.Add("DOMAIN_NAME", btnInfo.CommandArgument);
            this.Response.Redirect("~/Pages/Operation/SearchOperations.aspx", false);
        }
    }
    protected void btnAddNewDomain_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            Domain d = new Domain(this.txtDomainNameNew.Text);
            if (d.ID < 0)
            {
                d.Status = this.ddlStatusNew.SelectedValue;
                foreach (ListItem item in this.chkWS.Items)
                {
                    switch (item.Value)
                    {
                        case Phrase.WEB_SERVICE_SUBMIT:
                            d.AllowSubmit = item.Selected;
                            break;
                        case Phrase.WEB_SERVICE_DOWNLOAD:
                            d.AllowDownload = item.Selected;
                            break;
                        case Phrase.WEB_SERVICE_QUERY:
                            d.AllowQuery = item.Selected;
                            break;
                        case Phrase.WEB_SERVICE_SOLICIT:
                            d.AllowSolicit = item.Selected;
                            break;
                        case Phrase.WEB_SERVICE_NOTIFY:
                            d.AllowNotify = item.Selected;
                            break;
                    }
                }
                d.StatusMessage = this.txtStatusMessageNew.Text;
                d.Description = this.txtDescriptionNew.Text;
                string[] ids = this.egvAdminsNew.GetCheckedValue("gcbfAdminNew");
                ArrayList adminIDs = new ArrayList();
                foreach (string s in ids)
                    adminIDs.Add(int.Parse(s));
                d.AdminIDs = adminIDs;
                d.Save(this.LoggedInUser);

                imgTop5Domain_Click(null, null);
            }
            else
            {
                this.lblError.Text = "There is already a Domain Name by this name, please choose a different Domain Name";
                this.lblError.Visible = true;
            }
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

    protected void btnSaveDomainInfo_Click(object sender, EventArgs e)
    {
        try
        {
            Domain d = new Domain(this.txtDomainName.Text);
            d.Status = this.ddlStatusDetail.SelectedValue;
            foreach (ListItem item in this.cblAllowedWS.Items)
            {
                switch (item.Value)
                {
                    case Phrase.WEB_SERVICE_SUBMIT:
                        d.AllowSubmit = item.Selected;
                        break;
                    case Phrase.WEB_SERVICE_DOWNLOAD:
                        d.AllowDownload = item.Selected;
                        break;
                    case Phrase.WEB_SERVICE_QUERY:
                        d.AllowQuery = item.Selected;
                        break;
                    case Phrase.WEB_SERVICE_SOLICIT:
                        d.AllowSolicit = item.Selected;
                        break;
                    case Phrase.WEB_SERVICE_NOTIFY:
                        d.AllowNotify = item.Selected;
                        break;
                }
            }
            d.StatusMessage = this.txtStatusMessage.Text;
            d.Description = this.txtDescription.Text;
            string[] ids = this.egvAdmins.GetCheckedValue("gcbfAdmin");
            ArrayList adminIDs = new ArrayList();
            foreach (string s in ids)
                adminIDs.Add(int.Parse(s));
            d.AdminIDs = adminIDs;
            d.Save(this.LoggedInUser);
            if (this.dataLstDomain.Visible)
                this.imgTop5Domain_Click(null, null);
            else
                this.btnSearchDomain_Click(null, null);

            Page.SetFocus(this.imgTop5Domain);
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
        }
    }

    void egvDomainGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            this.ModalPopupExtender2.Show();
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowNo = this.egvDomainGrid.PageSize * this.egvDomainGrid.PageIndex + Convert.ToInt32(e.CommandArgument);
                if (ViewState["Domain"] != null)
                {
                    this.SetDomainInfo(((DataTable)ViewState["Domain"]).Rows[rowNo]["DOMAIN_NAME"].ToString());
                }
                else if (this.egvDomainGrid.CachedDataTable != null)
                {
                    this.SetDomainInfo(this.egvDomainGrid.CachedDataTable.Rows[rowNo]["FILE_ID"].ToString());
                }
            }
        }
    }
    protected void egvAdmins_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvAdmins_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Domain dom = new Domain(this.txtDomainName.Text);
            ArrayList list = dom.AdminIDs;
            if (e.Row.RowIndex > -1)
            {
                HtmlInputCheckBox test = (HtmlInputCheckBox)e.Row.Cells[0].Controls[0];
                if (test != null)
                {
                    int id = 0;
                    if (int.TryParse(this.egvAdmins.GetCurrentDataViewData(e, "USER_ID").ToString(), out id))
                    {
                        if (list.Contains(id))
                            test.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
        }
    }

    # region Protected methods

    protected string GetDomainName(string domainName)
    {
        return string.Format("Domain {0}", Eval(domainName).ToString());
    }

    # endregion

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.egvAdminsNew.CachedDataTable = Node.Core.Biz.Objects.User.GetConsoleUsersList();
            this.egvAdminsNew.DataBind();

            ConsoleUser user = new ConsoleUser(this.LoggedInUser);
            
            this.ddlDomainName.DataSource = Domain.GetDomainsDropDownList(this.LoggedInUser);
            this.ddlDomainName.DataValueField = "DOMAIN_ID";
            this.ddlDomainName.DataTextField = "DOMAIN_NAME";
            this.ddlDomainName.DataBind();

            this.imgTop5Domain_Click(null, null);

            bool bNodeDomainAdmin = (bool)Session[Phrase.NODE_DOMAIN_ADMIN];
            if (!bNodeDomainAdmin)
            {
                btnAddNewDomain.Visible = false;
            }

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

    private void SetDomainInfo(string domainName)
    {
        try
        {
            Domain dom = new Domain(domainName);
            this.txtDomainName.Text = dom.Name;
            this.ddlStatus.SelectedValue = dom.Status;
            foreach (ListItem item in this.cblAllowedWS.Items)
            {
                switch (item.Value)
                {
                    case Phrase.WEB_SERVICE_SUBMIT:
                        item.Selected = dom.AllowSubmit;
                        break;
                    case Phrase.WEB_SERVICE_DOWNLOAD:
                        item.Selected = dom.AllowDownload;
                        break;
                    case Phrase.WEB_SERVICE_QUERY:
                        item.Selected = dom.AllowQuery;
                        break;
                    case Phrase.WEB_SERVICE_SOLICIT:
                        item.Selected = dom.AllowSolicit;
                        break;
                    case Phrase.WEB_SERVICE_NOTIFY:
                        item.Selected = dom.AllowNotify;
                        break;
                }
            }
            this.txtStatusMessage.Text = dom.StatusMessage;
            this.txtDescription.Text = dom.Description;

            ConsoleUser user = new ConsoleUser(this.LoggedInUser);
            if (!user.IsNodeAdmin)
                this.cblAllowedWS.Enabled = false;

            this.egvAdmins.CachedDataTable = Node.Core.Biz.Objects.User.GetConsoleUsersList();
            this.egvAdmins.DataBind();
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
        }
    }

    #endregion
}
