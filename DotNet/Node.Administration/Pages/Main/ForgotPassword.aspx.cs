using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Node.Lib.UI.WebUtils;

using Node.Core.Biz.NAAS;
using Node.Core.Biz.Objects;

public partial class ForgotPassword_aspx : Node.Core.UI.Base.AdminPageBase
{
    public ForgotPassword_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sText = "";
            string KeyPrefix = "";
            string sPath = Request.AppRelativeCurrentExecutionFilePath;
            sPath = sPath.Replace("~/", "");
            sPath = sPath.Replace(".aspx", "");
            KeyPrefix = sPath.Replace("/", ".");
            sText = TextResource.GetValue(KeyPrefix + ".YellowBubble");
            this.forgotPWDInstructions.InnerHtml = sText;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblMessage.MsgContent = "";

            if (this.txtName.Text.Trim() != "")
            {
                User user = new ConsoleUser(this.txtName.Text);
                if (user.UserID < 0)
                    user = new LocalUser(this.txtName.Text);
                if (user.UserID < 0)
                    user = new NAASUser(this.txtName.Text, false);
                if (user.UserID >= 0)
                {
                    if (this.txtNewPWD.Text.Trim() != "" && this.txtNewPWD.Text == this.txtNewPWD2.Text)
                    {
                        if (user.UserType == Node.Core.Biz.Objects.User.NAAS_NODE_USER)
                        {
                            UserManager manager = new UserManager();
                            if (manager.ChangePassword(this.txtName.Text, this.txtOldPWD.Text, this.txtNewPWD.Text))
                                this.lblMessage.MsgContent = "Successful Password Change";
                            else
                                this.lblMessage.MsgContent = "Failed Password Change";
                        }
                        else if (this.txtOldPWD.Text == user.Password)
                        {
                            if (user.UserType == Node.Core.Biz.Objects.User.CONSOLE_USER)
                            {
                                ConsoleUser cu = (ConsoleUser)user;
                                cu.Password = this.txtNewPWD.Text;
                                cu.ChangePWDFlag = false;
                                cu.Save(null);
                            }
                            else if (user.UserType == Node.Core.Biz.Objects.User.LOCAL_NODE_USER)
                            {
                                LocalUser lu = (LocalUser)user;
                                lu.Password = this.txtNewPWD.Text;
                                lu.ChangePWDFlag = false;
                                lu.Save(null);
                            }
                            this.lblMessage.MsgContent = "Successful Password Change";
                        }
                        else
                            this.lblMessage.MsgContent = "The User's Old Password does not match the User's Current Password";
                    }
                    else
                        this.lblMessage.MsgContent = "The New Passwords must be non-empty and match";
                }
                else
                    this.lblMessage.MsgContent = "The User is not in our records.  Please enter a new User ID or contact the Node Administrator";
            }
            else
                this.lblMessage.MsgContent = "Please Enter a User ID";
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //this.lblMessage.MsgContent = "System can not process your request. Please contact system administrator." + Environment.NewLine + ex.ToString();
            this.lblMessage.MsgContent = "System can not process your request. Please contact system administrator." + Environment.NewLine + ex.Message;
        }
    }    
}
