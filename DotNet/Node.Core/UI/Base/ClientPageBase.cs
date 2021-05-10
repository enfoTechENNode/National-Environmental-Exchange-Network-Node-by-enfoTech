using System;
using System.Collections.Generic;
using System.Text;

using Node.Lib.UI.WebUtils;

namespace Node.Core.UI.Base
{
    /// <summary>
    /// Pagebase for node client Web Site.
    /// </summary>
    public class ClientPageBase : Node.Lib.UI.Base.PageBase
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
        /// Constant Value of CURRENT_TAB_INDEX.
        /// </summary>
        public const string CURRENT_TAB_INDEX = "CURRENT_TAB_INDEX";
        /// <summary>
        /// Constructor of ClientPageBase.
        /// </summary>
        public ClientPageBase()
		{
			this.PreInit += new EventHandler(PageBase_PreInit);
			this.Init += new EventHandler(PageBase_Init);
			this.PreLoad += new EventHandler(PageBase_PreLoad);
			this.Load += new EventHandler(PageBase_Load);
			this.PreRender += new EventHandler(PageBase_PreRender);
		}

        private void PageBase_Init(object sender, EventArgs e)
        {
        }

        private void PageBase_PreInit(object sender, EventArgs e)
        {

        }

        private void PageBase_PreLoad(object sender, EventArgs e)
        {
            this.SetYellowBubble();
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
        /// Save Node URL.
        /// </summary>
        /// <param name="url">url string</param>
        protected void SaveNodeURL(string url)
        {
            this.Session[ClientPageBase.NODE_URL_SESSION_KEY] = url;
        }
        /// <summary>
        /// Get Node URL.
        /// </summary>
        /// <returns>string of URL</returns>
        protected string GetSavedNodeURL()
        {
            if (this.Session[ClientPageBase.NODE_URL_SESSION_KEY] != null)
                return (string)this.Session[ClientPageBase.NODE_URL_SESSION_KEY];
            return null;
        }
        /// <summary>
        /// Save security token. 
        /// </summary>
        /// <param name="token">security token.</param>
        protected void SaveSecurityToken(string token)
        {
            this.Session[ClientPageBase.NODE_TOKEN_SESSION_KEY] = token;
        }
        /// <summary>
        /// Get saved security token.
        /// </summary>
        /// <returns>security token.</returns>
        protected string GetSavedToken()
        {
            if (this.Session[ClientPageBase.NODE_TOKEN_SESSION_KEY] != null)
                return (string)this.Session[ClientPageBase.NODE_TOKEN_SESSION_KEY];
            return null;
        }
        /// <summary>
        /// Get last active tab.
        /// </summary>
        /// <returns>The index of active tab</returns>
        protected int GetLastActiveTab()
        {
            if (this.Session[ClientPageBase.CURRENT_TAB_INDEX] == null)
                return 0;

            return (int)this.Session[ClientPageBase.CURRENT_TAB_INDEX];
        }
        /// <summary>
        /// Set last active tab.
        /// </summary>
        /// <param name="idx">The index of tab</param>
        protected void SetCurrentActiveTab(int idx)
        {
            this.Session[ClientPageBase.CURRENT_TAB_INDEX] = idx;
        }
    }
}
