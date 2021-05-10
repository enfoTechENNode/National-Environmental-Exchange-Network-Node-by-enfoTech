using System;
using System.Collections.Generic;
using System.Text;

using Node.Lib.UI.WebUtils;
using Node.Core.Logging;

namespace Node.Core.UI.Base
{
    /// <summary>
    /// Pagebase for Admin Web Site.
    /// </summary>
    public class AdminPageBase : Node.Lib.UI.Base.PageBase
    {
        /// <summary>
        /// Constant Value of NODE_URL_SESSION_KEY.
        /// </summary>
        public const string NODE_URL_SESSION_KEY = "NODE_URL";
        /// <summary>
        /// Constant Value of NODE_TOKEN_SESSION_KEY.
        /// </summary>
        public const string NODE_TOKEN_SESSION_KEY = "NODE_TOKEN";

        /// <summary>
        /// Constructor of AdminPageBase.
        /// </summary>
        public AdminPageBase()
        {/*
            this.PreInit += new EventHandler(PageBase_PreInit);
            this.Init += new EventHandler(PageBase_Init);
            this.PreLoad += new EventHandler(PageBase_PreLoad);
            this.Load += new EventHandler(PageBase_Load);
            this.PreRender += new EventHandler(PageBase_PreRender);*/
        }
        /// <summary>
        /// Replacement for ASP page OnLoad() event, since PageBase use it to do something else.
        /// </summary>
        /// <param name="e">EventArgs of Page.</param>
        protected override void OnLoadAction(EventArgs e)
        {
            if (!this.IsPostBack && this.Session[Phrase.USER_SESSION_KEY] == null)
            {
                switch (this.PageID)
                {
                    case "Pages.Help.GenericHelp":
                        break;
                    case "Pages.Main.ForgotPassword":
                        break;
                    case "Pages.Main.RestPassword":
                        break;
                    case "Pages.Main.Login":
                        break;
                    default:
                        this.Response.Redirect("~/Pages/Main/Login.aspx");
                        break;
                }
            }
            else
                this.SetYellowBubble();
        }

        private void PageBase_PreInit(object sender, EventArgs e)
        {

        }

        private void PageBase_Load(object sender, EventArgs e)
        {
        }

        private void PageBase_PreRender(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Set YellowBuble on the Page.
        /// </summary>
        protected void SetYellowBubble()
        {
            if (this.Master != null)
            {
                string sText = "";
                string KeyPrefix = "";
                string sPath = Request.AppRelativeCurrentExecutionFilePath;
                sPath = sPath.Replace("~/", "");
                sPath = sPath.Replace(".aspx", "");
                KeyPrefix = sPath.Replace("/", ".");
                sText = TextResource.GetValue(KeyPrefix + ".bbl.YellowBubble");
                this.Master.GetType().GetProperty("PageDescription").SetValue(this.Master, sText, null);
            }
        }
        /// <summary>
        /// Exception handler for the Page.
        /// </summary>
        /// <param name="e">exception.</param>
        protected void HandleException(Exception e)
        {
            Logger logger = new Logger(Phrase.LoggerPath, Phrase.LoggerLevel);
            logger.Log(e);
        }
        /// <summary>
        /// Get Logged in User.
        /// </summary>
        protected string LoggedInUser
        {
            get
            {
                object user = this.Session[Phrase.USER_SESSION_KEY];
                if (user == null)
                    return null;
                return "" + user;
            }
            set
            {
                this.Session[Phrase.USER_SESSION_KEY] = value;
            }
        }
        /// <summary>
        /// Save Node URL.
        /// </summary>
        /// <param name="url">Thr url string</param>
        protected void SaveNodeURL(string url)
        {
            this.Session[AdminPageBase.NODE_URL_SESSION_KEY] = url;
        }
        /// <summary>
        /// Save Security Token. 
        /// </summary>
        /// <param name="token">The security token string.</param>
        protected void SaveSecurityToken(string token)
        {
            this.Session[AdminPageBase.NODE_TOKEN_SESSION_KEY] = token;
        }
    }
}
