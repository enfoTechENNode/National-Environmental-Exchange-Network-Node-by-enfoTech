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

public partial class ChangePassword_aspx : Node.Core.UI.Base.AdminPageBase
{
    public ChangePassword_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Event Handlers

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (this.txtPassword1.Text == this.txtPassword2.Text)
            {
                ConsoleUser u = new ConsoleUser(this.LoggedInUser);
                u.Password = this.txtPassword1.Text;
                u.ChangePWDFlag = false;
                u.Save(this.LoggedInUser);

                FormsAuthentication.SetAuthCookie(u.UserName, false);

                this.Response.Redirect("~/Pages/Main/Home.aspx", false);
            }
            else
            {
                this.lblError.Text = "Passwords must match";
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

    #endregion
}
