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

public partial class NewLocalUser_aspx : Node.Core.UI.Base.AdminPageBase
{
    public NewLocalUser_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Event Handlers

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/User/SearchUsers.aspx", false);
    }
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

            LocalUser u = new LocalUser(this.txtLoginName.Text);
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
                string[] names = this.egvOperations.GetCheckedValue("gcbfOperation");
                ArrayList operations = new ArrayList();
                if (names != null && names.Length > 0)
                    foreach (string name in names)
                        operations.Add(int.Parse(name));
                u.OperationIDs = operations;
                u.Save(this.LoggedInUser);
                EmailManager manager = new EmailManager();
                string custom = "A Local Node User Account has been created for you.\r\n";
                custom += "You can use the account to call the Authenticate Web Service";
                string error = manager.SendUserEmail(u.EmailAddress, u.UpdatedDate, Phrase.LOCAL_NODE_USER, u.UserName, u.Password, custom);
                if (error != null && !error.Trim().Equals(""))
                {
                    Logger logger = new Logger();
                    logger.Log("Email Error", error, Logger.LEVEL_ERROR);
                    this.Response.Redirect("~/Pages/User/ViewLocalUser.aspx?loginName=" + u.UserName + "&error=EMAIL", false);
                }
                else
                    this.Response.Redirect("~/Pages/User/ViewLocalUser.aspx?loginName=" + u.UserName + "&error=", false);
            }
            else
            {
                this.lblError.Text = "There is already a Local Node User with the entered User Name, please choose a different User Name.";
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

    protected void egvOperations_RowCommand(object sender, GridViewCommandEventArgs e)
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

            this.egvOperations.CachedDataTable = Operation.GetOperationsDataGrid(this.LoggedInUser);
            this.egvOperations.DataBind();
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
