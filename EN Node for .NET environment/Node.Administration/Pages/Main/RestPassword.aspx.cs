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
using Node.Lib.Data;
using Node.Core.Biz.Manageable;
using Node.Core;


public partial class RestPassword_aspx : Node.Core.UI.Base.AdminPageBase
{
    public RestPassword_aspx()
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

            if (this.txtEmail.Text.Trim() != "")
            {

                DBAdapter db = new DBAdapter("node");
                DataSet ds = new DataSet();
                string sSql = "SELECT SYS_EMAIL.EMAIL_ADDRESS, SYS_USER_INFO.LOGIN_NAME ";
                        sSql = sSql + "FROM SYS_EMAIL INNER JOIN SYS_USER_EMAIL ON SYS_EMAIL.EMAIL_ID = SYS_USER_EMAIL.EMAIL_ID ";
                sSql = sSql + "INNER JOIN SYS_USER_INFO ON SYS_USER_EMAIL.USER_ID = SYS_USER_INFO.USER_ID ";
                        sSql = sSql + "WHERE SYS_EMAIL.EMAIL_ADDRESS = '"+this.txtEmail.Text+"'";

                db.GetDataSet("email", sSql, ds);

                if (ds.Tables["email"].Rows.Count > 0)
                {
                    ConsoleUser u = new ConsoleUser("" + ds.Tables["email"].Rows[0]["LOGIN_NAME"]);
                    if (u.UserID >= 0)
                    {

                        u.ChangePWDFlag = true;
                        u.Save(this.LoggedInUser);
                        EmailManager manager = new EmailManager();
                        string custom = "The Node Administration Console User Account Password has been regenerated for this user.";
                        string error = manager.SendUserEmail(u.EmailAddress, u.UpdatedDate, Phrase.CONSOLE_USER, u.UserName, u.Password, custom);
                        if (error != null && !error.Trim().Equals(""))
                        {
                            this.lblMessage.MsgContent = "Password was generated, but the email failed to be sent: " + error;
                        }
                        else
                        {
                            this.lblMessage.MsgContent = "Password was generated and the email was sent";
                        }

                    }else
                        this.lblMessage.MsgContent = "The email address does not connect to any user in the system";
                }
                else
                    this.lblMessage.MsgContent = "The e-Mail address does not exist in the system";
          
            }
            else
                this.lblMessage.MsgContent = "Please Enter a e-Mail address";
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //this.lblMessage.MsgContent = "System can not process your request. Please contact system administrator." + Environment.NewLine + ex.ToString();
            this.lblMessage.MsgContent = "System can not process your request. Please contact system administrator." + Environment.NewLine + ex.Message;
        }
    }    
}
