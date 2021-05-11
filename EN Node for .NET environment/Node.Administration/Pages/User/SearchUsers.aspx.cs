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

public partial class SearchUsers_aspx : Node.Core.UI.Base.AdminPageBase
{
    public SearchUsers_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.egvUserGrid.RowEditing += new GridViewEditEventHandler(this.egvUserGrid_RowEditing);
        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Event Handlers

    protected void btnNewConsole_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/User/NewConsoleUser.aspx", false);
    }

    protected void btnNewNAAS_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/User/NewNAASUser.aspx", false);
    }

    protected void btnNewLocal_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/User/NewLocalUser.aspx", false);
    }

    protected void btnBackToDashboard_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    }

    protected void ddlUserType_Change(object sender, EventArgs e)
    {
        if (ddlUserType.SelectedValue == Node.Core.Phrase.NAAS_NODE_USER)
        {
            firstLastNameRow.Visible = false;
            cbAllNAAS.Checked = true;
        }
        else
        {
            firstLastNameRow.Visible = true;
            cbAllNAAS.Checked = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.egvUserGrid.CachedDataTable = Node.Core.Biz.Objects.User.SearchUsers(this.txtLoginName.Text, this.ddlUserType.SelectedValue, int.Parse(this.ddlDomain.SelectedValue), this.txtFirstName.Text, this.txtLastName.Text, this.cbAllNAAS.Checked);
            if (this.egvUserGrid.CachedDataTable.Rows.Count > 0)
            {
                this.egvUserGrid.DataBind();
                this.egvUserGrid.Visible = true;
            }
            else
            {
                this.lblError.Text = "No such user.";
                this.lblError.Visible = true;
                this.egvUserGrid.Visible = false;
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

    protected void egvUserGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvUserGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            this.lblError.Text = string.Empty;
            this.lblError.Visible = false;

            string userName = this.egvUserGrid.GetCurrentDataViewData(e, "LOGIN_NAME").ToString();
            Node.Core.Biz.Objects.User u = new ConsoleUser(userName);
            if (u.UserID >= 0)
                this.Response.Redirect("~/Pages/User/ViewConsoleUser.aspx?loginName=" + u.UserName, true);
            else
            {
                u = new LocalUser(userName);
                if (u.UserID >= 0)
                    this.Response.Redirect("~/Pages/User/ViewLocalUser.aspx?loginName=" + u.UserName, true);
                else
                {
                    u = new NAASUser(userName, false);
                    if (u.UserID >= 0)
                        this.Response.Redirect("~/Pages/User/ViewNAASUser.aspx?loginName=" + u.UserName, true);
                }
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

    #endregion

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.ddlDomain.DataSource = Domain.GetDomainsDropDownList(this.LoggedInUser);
            this.ddlDomain.DataTextField = "DOMAIN_NAME";
            this.ddlDomain.DataValueField = "DOMAIN_ID";
            this.ddlDomain.DataBind();
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

    #endregion
}
