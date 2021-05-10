using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

using Node.Core.Logging;

namespace Node.Core.UI.Base
{
    /// <summary>
    /// User Control's ControlBase for the Admin WebSite. 
    /// </summary>
    public class AdminUserControlBase : UserControl
    {
        /// <summary>
        /// Constructor of AdminUserControlBase.
        /// </summary>
        public AdminUserControlBase()
        {
            //
        }
        /// <summary>
        /// Exception Hanlder for UserControl.
        /// </summary>
        /// <param name="e"></param>
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
    }
}
