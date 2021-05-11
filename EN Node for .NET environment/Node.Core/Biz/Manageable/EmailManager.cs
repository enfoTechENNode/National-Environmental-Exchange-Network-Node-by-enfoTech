using System;
using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Text;

using NodeLib = Node.Lib.AppSystem;
using Node.Core.Biz.Objects;

namespace Node.Core.Biz.Manageable
{
    /// <summary>
    /// The collection of functions related to eMail.
    /// </summary>
    public class EmailManager
    {
        #region Public Constructors

        /// <summary>
        /// Constructs a New Instance of this Class and Initializes the EAF EmailManager class
        /// </summary>
        public EmailManager()
        {
            this.manager = new NodeLib.EmailManager("EmailTemplate.xml");
            SystemConfiguration config = new SystemConfiguration();
            this.manager.HostName = config.GetEmailServerHost();
            this.manager.Port = config.GetEmailServerPort();
            string user = config.GetEmailServerUserID();
            if (user != null && !user.Trim().Equals(""))
                this.manager.UserName = user;
            string pwd = config.GetEmailServerPassword();
            if (pwd != null && !pwd.Trim().Equals(""))
                this.manager.Password = pwd;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the Values of the User Account Email Template
        /// </summary>
        /// <param name="template">The User Account Template</param>
        /// <returns>Any Validation Errors</returns>
        public string SaveUserTemplate(NodeLib.EmailTemplate template)
        {
            if (template == null)
                return "Template Must be Non-Null";
            string validation = "";
            if (template.From == null || template.From.Trim().Equals(""))
                validation = "User Account Sender Email must be non-empty";
            if (template.Content == null || template.Content.Trim().Equals(""))
            {
                if (validation.Length > 0)
                    validation += "\n";
                validation += "User Account Template must be non-empty";
            }
            if (validation.Equals(""))
                this.manager.SaveEmailTemplate();
            return validation;
        }

        /// <summary>
        /// Set the Values of the Task Status Email Template
        /// </summary>
        /// <param name="template">The Task Status Template</param>
        /// <returns>Any Validation Errors</returns>
        public string SaveTaskTemplate(NodeLib.EmailTemplate template)
        {
            if (template == null)
                return "Template Must be Non-Null";
            string validation = "";
            if (template.From == null || template.From.Trim().Equals(""))
                validation = "Task Status Sender Email must be non-empty";
            if (template.Content == null || template.Content.Trim().Equals(""))
            {
                if (validation.Length > 0)
                    validation += "\n";
                validation += "Task Status Template must be non-empty";
            }
            if (validation.Equals(""))
                this.manager.SaveEmailTemplate();
            return validation;
        }
        /// <summary>
        /// Gets User Account EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetUserTemplate()
        {
            return this.manager.GetEmailTemplate("User Account");
        }
        /// <summary>
        /// Gets Task Status EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetTaskTemplate()
        {
            return this.manager.GetEmailTemplate("Task Status");
        }
        /// <summary>
        /// Gets Submit Recipient EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetSubmitRecipient()
        {
            return this.manager.GetEmailTemplate("Submit Recipient");
        }
        /// <summary>
        /// Gets Submit Notification EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetSubmitNotification()
        {
            return this.manager.GetEmailTemplate("Submit Notification");
        }
        /// <summary>
        /// Gets Solicit Recipient EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetSolicitRecipient()
        {
            return this.manager.GetEmailTemplate("Solicit Recipient");
        }
        /// <summary>
        /// Gets Solicit Notification EmailTemplate object.
        /// </summary>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetSolicitNotification()
        {
            return this.manager.GetEmailTemplate("Solicit Notification");
        }
        /// <summary>
        /// Gets Task Status EmailTemplate object.
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
        public NodeLib.EmailTemplate GetEmailTemplateByName(string templateName)
        {
            return this.manager.GetEmailTemplate(templateName);
        }
        /// <summary>
        /// The method send email with user account email template. 
        /// </summary>
        /// <param name="to">The email address of receipt.</param>
        /// <param name="updatedDate">The updated date.</param>
        /// <param name="accountType">The account type.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="customMessage">The content of custom message.</param>
        /// <returns>Error message if fail.</returns>
        public string SendUserEmail(string to, DateTime updatedDate, string accountType, string userName, string password, string customMessage)
        {
            NodeLib.EmailTemplate template = this.GetUserTemplate();
            template.ToList = to;
            template.BookMarks["Updated Date"] = updatedDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            template.BookMarks["Account Type"] = accountType;
            template.BookMarks["Account User Name"] = userName;
            template.BookMarks["User Password"] = password;
            template.BookMarks["Custom Message"] = customMessage;
            return this.manager.SendEmail(template);
        }
        /// <summary>
        /// The method send email with Task Status email template.
        /// </summary>
        /// <param name="to">The email address of receipt.</param>
        /// <param name="opLogID">The operation log id.</param>
        /// <returns>Error message if fail.</returns>
        public string SendTaskEmail(string to, int opLogID)
        {
            NodeLib.EmailTemplate template = this.GetTaskTemplate();
            template.ToList = to;
            OperationLog log = new OperationLog(opLogID);
            Hashtable table = new Hashtable();
            template.BookMarks["Task Name"] = log.OperationName;
            OperationLogStatus status = (OperationLogStatus)log.Statuses[log.Statuses.Count - 1];
            template.BookMarks["Task Status"] = status.Status;
            template.BookMarks["Start Date"] = log.StartDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            template.BookMarks["End Date"] = log.EndDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            MemoryStream ms = new MemoryStream();
            StreamWriter writer = new StreamWriter(ms);
            writer.WriteLine("Task " + log.OperationName + " History");
            foreach (object obj in log.Statuses)
            {
                OperationLogStatus s = (OperationLogStatus)obj;
                writer.WriteLine();
                writer.WriteLine("[" + s.CreatedDate.ToString("MM/dd/yyyy hh:mm:ss tt") + "]");
                writer.WriteLine(s.Status + ": " + s.Message);
            }
            writer.Flush();
            ms.Position = 0;
            ArrayList list = new ArrayList();
            list.Add(new Attachment(ms, "Task " + log.OperationName + " Status - " + log.StartDate.ToString("MM/dd/yyyy hh:mm:ss tt") + ".txt"));
            template.Attachment = list;
            return this.manager.SendEmail(template);
        }
        /// <summary>
        /// The method send email with specified email template.
        /// </summary>
        /// <param name="template">Name of Template to be used.</param>
        /// <returns></returns>
        public string SendEmail(NodeLib.EmailTemplate template)
        {
            return this.manager.SendEmail(template);
        }

        #endregion

        #region Private Fields

        NodeLib.EmailManager manager = null;

        #endregion
    }
}
