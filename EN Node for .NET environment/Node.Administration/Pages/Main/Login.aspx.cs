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

using Node.Lib.UI.WebUtils;
using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Data;

public partial class Login_aspx : Node.Core.UI.Base.AdminPageBase
{
    public Login_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    void form1_PreRender(object sender, EventArgs e)
    {
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if ("" + Request.QueryString["Type"] == "SignOut")
            {
                FormsAuthentication.SignOut();
                //Session.Clear();
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", string.Empty));
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (this.txtUsername.Text.Trim().Equals("") || this.txtPassword.Text.Trim().Equals(""))
        {
            this.lblError.Text = "Please Enter a User Name and a Password";
            return;
        }
        try
        {
            this.lblError.Text = "";
            Node.Core.Biz.Objects.User u = new Node.Core.Biz.Objects.ConsoleUser(this.txtUsername.Text);
            if (u != null && u.UserType == Node.Core.Biz.Objects.User.CONSOLE_USER)
            {
                if (this.txtPassword.Text.Equals(u.Password))
                {
                    if (u.Status != "A")
                        this.lblError.Text = "Invalid Login";
                }
                else
                    this.lblError.Text = "Invalid Login";
            }
            else
                this.lblError.Text = "Invalid Login";
            if (this.lblError.Text.Equals(""))
            {
                
                this.Session[Phrase.USER_SESSION_KEY] = this.txtUsername.Text;
                NodeDomainSecurity(this.txtUsername.Text);

                SystemConfiguration config = new SystemConfiguration();
                string rownum = config.GetDefaultRownum();
                if (!string.IsNullOrEmpty(rownum))
                    this.Session[Phrase.DEFAULT_ROWNUM] = rownum;
                else
                    this.Session[Phrase.DEFAULT_ROWNUM] = "-1";

                string topnum = config.GetDefaultTopnum();
                if (!string.IsNullOrEmpty(topnum))
                    this.Session[Phrase.DEFAULT_TOPNUM] = topnum;
                else
                    this.Session[Phrase.DEFAULT_TOPNUM] = "5";

                this.Session[Phrase.VERSION_NO] = Phrase.VERSION_20;
                string pagesize = config.GetDefaultPageSize();
                if (!string.IsNullOrEmpty(pagesize))
                    this.Session[Phrase.DEFAULT_PAGESIZE] = pagesize;
                else
                    this.Session[Phrase.DEFAULT_PAGESIZE] = "5";

                if (u.ChangePWDFlag)
                    this.Response.Redirect("~/Pages/Main/ChangePassword.aspx");
                else
                {
                    FormsAuthentication.SetAuthCookie(this.txtUsername.Text,true);
                    if (Session["OpenFrom"] == null)
                    {
                        Session.Add("OpenForm", "");
                    }
                    SystemConfiguration sysconfg = new SystemConfiguration();
                    Response.Redirect("~/Pages/Main/Home.aspx", false);
                    //Response.Redirect("~/Pages/Monitoring/SearchLogs.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //this.lblError.Text = "System Error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System Error." + Environment.NewLine + "The database could not be connected.";
        }
    }

    protected string GetBuildDate()
    {
        string sText = System.Reflection.Assembly.LoadFrom(this.Request.PhysicalApplicationPath + @"\Bin\Node.Core2.dll").GetName().Version.ToString();
        string[] sTexts = sText.Split('.');
        int iDays = int.Parse(sTexts[2]);
        DateTime ndateStart = DateTime.Parse("2000/01/01");
        ndateStart = ndateStart.AddDays(iDays);
        return ndateStart.ToShortDateString();

    }
    protected string GetBuildVersion()
    {
        return System.Reflection.Assembly.LoadFrom(this.Request.PhysicalApplicationPath + @"\Bin\Node.Core2.dll").GetName().Version.ToString();
    }
    private void NodeDomainSecurity(string userName)
    {
        bool bFlag = new DBManager().GetDomainsDB().IsNodeDomainAdmin(userName);
        if (bFlag)
        {
            Session.Add(Phrase.NODE_DOMAIN_ADMIN, true);
        }
        else
        {
            Session.Add(Phrase.NODE_DOMAIN_ADMIN,false);
        }
    }
}
