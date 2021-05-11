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

using Node.Core;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Objects;
using Node.Core.Logging;

public partial class NewConsoleUser_aspx : Node.Core.UI.Base.AdminPageBase
{
    public NewConsoleUser_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Event Handlers

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            NAASUser nU = new NAASUser(this.txtLoginName.Text, true);
            if (nU.UserID == 0)
            {
                this.lblError.Text = "There is already a NAAS Node User with the entered User Name, please choose a different User Name.";
                this.lblError.Visible = true;
                return;
            }

            ConsoleUser u = new ConsoleUser(this.txtLoginName.Text);
            if (u.UserID < 0)
            {
                u.UserName = this.txtLoginName.Text;
                u.Status = this.ddlStatus.SelectedValue;
                u.FirstName = this.txtFirstName.Text;
                u.MiddleInitial = this.txtMidInitial.Text;
                u.LastName = this.txtLastName.Text;
                u.EmailAddress = this.txtEmail.Text;
                u.PhoneNumber = this.ptbPhone.Text;
                u.Address = this.txtAddress.Text;
                u.SupplementalAddress = this.txtSuppAddress.Text;
                u.LocalityName = this.txtCity.Text;
                u.StateUSPSCode = this.txtState.Text;
                u.ZipCode = this.zctbZip.Text;
                u.CountryCode = this.txtCountry.Text;
                u.Comments = this.txtComments.Text;
                string[] names = this.egvDomains.GetCheckedValue("gcbfDomain");
                ArrayList domains = new ArrayList();
                if (names != null && names.Length > 0)
                {
                    foreach (string name in names)
                        domains.Add(int.Parse(name));
                }
                else
                {
                    this.lblError.Text = "Console User is required to link with one domain!";
                    this.lblError.Visible = true;
                    return;
                }
                u.DomainIDs = domains;
                u.Save(this.LoggedInUser);
                EmailManager manager = new EmailManager();
                string custom = "A Node Administration Console User Account has been created for you.\r\n";
                custom += "To begin administering a Node Data Flow, please login to the Node Administration utility using the supplied credentials";
                string error = manager.SendUserEmail(u.EmailAddress, u.UpdatedDate, Phrase.CONSOLE_USER, u.UserName, u.Password, custom);
                if (error != null && !error.Trim().Equals(""))
                {
                    Logger logger = new Logger();
                    logger.Log("Email Error", error, Logger.LEVEL_ERROR);
                    this.Response.Redirect("~/Pages/User/ViewConsoleUser.aspx?loginName=" + u.UserName + "&error=EMAIL", false);
                }
                else
                    this.Response.Redirect("~/Pages/User/ViewConsoleUser.aspx?loginName=" + u.UserName + "&error=", false);
            }
            else
            {
                this.lblError.Text = "There is already a Console User with the entered Login Name, please choose a different Login Name.";
                this.lblError.Visible = false;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/User/SearchUsers.aspx", false);
    }
    protected void egvDomains_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    #endregion

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DataTable dt = Domain.GetDomainsDropDownList(this.LoggedInUser);
            object obj = dt.Rows[0]["DOMAIN_NAME"];
            if (obj == null || obj.ToString().Trim().Equals(""))
                dt.Rows.RemoveAt(0);
            this.egvDomains.CachedDataTable = dt;
            this.egvDomains.DataBind();
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
