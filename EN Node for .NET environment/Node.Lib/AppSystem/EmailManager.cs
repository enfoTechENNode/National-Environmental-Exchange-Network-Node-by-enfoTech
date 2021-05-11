#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		EmailManager.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/27/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;

using Node.Lib.Utility;
//using EAF.Domain.DataSearch;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Represents the EmainManager class.
	/// </summary>
	public class EmailManager
	{
		private string filename = null;
		private XmlDocument doc = null;
		private Email email = null;
		private Hashtable ht = null;
		private string content = null;
		private string subject = null;

		/// <summary>
		/// Initializes an EmailManager object.
		/// </summary>
		/// <param name="name">The file name or key name of the email template.</param>
		public EmailManager(string name)
		{
			this.filename = name;
			this.doc = SystemConfig.GetInstance().GetSystemConfig(this.filename);
			Initialize();
		}

		/// <summary>
		/// Initializes an EmailManager object by specified template document.
		/// </summary>
		/// <param name="templateDoc">An XmlDocument object contains the Email Template.</param>
		public EmailManager(XmlDocument templateDoc)
		{
			this.doc = templateDoc;
			Initialize();
		}

		/// <summary>
		/// Initializes an EmailManager object by using default EmailTemplateSetting value.
		/// </summary>
		public EmailManager()
		{
			this.filename = Properties.Settings.Default.EmailTemplateSetting;
			this.doc = SystemConfig.GetInstance().GetSystemConfig(this.filename);
			Initialize();
		}

		#region public properties
		/// <summary>
		/// Gets or sets the smtp server name.
		/// </summary>
		public string HostName
		{
			get { return this.email.Host; }
			set { this.email.Host = value; }
		}

		/// <summary>
		/// Gets or sets the smtp server port number.
		/// </summary>
		public string Port
		{
			get { return this.email.Port; }
			set { this.email.Port = value; }
		}

		/// <summary>
		/// Gets or sets the user name for authentication.
		/// </summary>
		public string UserName
		{
			get { return this.email.UserName; }
			set { this.email.UserName = value; }
		}

		/// <summary>
		/// Sets the password for authentication.
		/// </summary>
		public string Password
		{
			set { this.email.Password = value; }
		}

		/// <summary>
		/// Gets the Email Template Document.
		/// </summary>
		public XmlDocument EmailTemplateDocument
		{
			get { return this.doc; }
		}

		/// <summary>
		/// Gets the subject of the email just sent out.
		/// </summary>
		public string CurrentSubject
		{
			get { return this.subject; }
		}

		/// <summary>
		/// Gets the content of the email just sent out.
		/// </summary>
		public string CurrentContent
		{
			get { return this.content; }
		}
		#endregion

		#region public functions
		/// <summary>
		/// Saves the EmailTemplate.
		/// </summary>
		public void SaveEmailTemplate()
		{
			if (this.filename == null)
				throw new ApplicationException("The file name or key name is null. Can not save it!");
			SystemConfig.GetInstance().SetSystemConfig(this.filename, this.doc);
		}

		/// <summary>
		/// Gets EmailTemplate object by specified template name.
		/// </summary>
		/// <param name="templatename">The template name.</param>
		/// <returns>An EmailTemplate object returned. It is null, if can not find it by specified template name.</returns>
		public EmailTemplate GetEmailTemplate(string templatename)
		{
			object obj = this.ht[templatename];
			return (obj == null) ? null : (EmailTemplate)obj;
		}

		/// <summary>
		/// Gets all names of email template.
		/// </summary>
		/// <returns>A Collection object contains the names.</returns>
		public ICollection GetAllEmailTemplateNames()
		{
			return this.ht.Keys;
		}

		/// <summary>
		/// Gets all EmailTempate objects defined in the email template config.
		/// </summary>
		/// <returns>A Collection object contains the EmailTemplate objects.</returns>
		public ICollection GetAllEmailTemplates()
		{
			return this.ht.Values;
		}

		/// <summary>
		/// Sends email by specified email template name.
		/// </summary>
		/// <param name="templateName">The name of email template.</param>
		/// <returns>The error message if it has.</returns>		
		public string SendEmail(string templateName)
		{
			return SendEmail(templateName, false);
		}

		/// <summary>
		/// Sends email by specified email template name.
		/// </summary>
		/// <param name="templateName">The name of email template.</param>
		/// <param name="bAsync">It is true, if send email using async method; otherwise, false.</param>
		/// <returns>The error message if it has.</returns>		
		public string SendEmail(string templateName, bool bAsync)
		{
			object obj = this.ht[templateName];
			if (obj == null)
				throw new ApplicationException("Can not find email template with template name as '" + templateName + "'.");
			return SendEmail((EmailTemplate)obj, bAsync);
		}

		/// <summary>
		/// Sends email by specified EmailTemplate object.
		/// </summary>
		/// <param name="template">The EmailTemplate object contains the information.</param>
		/// <returns>The error message if it has.</returns>
		public string SendEmail(EmailTemplate template)
		{
			return SendEmail(template, false);
		}

		/// <summary>
		/// Sends email by specified EmailTemplate object.
		/// </summary>
		/// <param name="template">The EmailTemplate object contains the information.</param>
		/// <param name="bAsync">It is true, if send email using async method; otherwise, false.</param>
		/// <returns>The error message if it has.</returns>
		public string SendEmail(EmailTemplate template, bool bAsync)
		{
            return Send(template, bAsync, false);
		}

        /// <summary>
        /// Resends email by specified EmailTemplate.
        /// </summary>
        /// <param name="template">An EmailTemplate object with last merged data.</param>
        /// <returns>The error message if it has.</returns>
        public string ResendEmail(EmailTemplate template)
        {
            return ResendEmail(template, false);
        }

        /// <summary>
        /// Resends email by specified EmailTemplate.
        /// </summary>
        /// <param name="template">The EmailTemplate object contains the last merged information.</param>
        /// <param name="bAsync">It is true, if send email using async method; otherwise, false.</param>
        /// <returns>The error message if it has.</returns>
        public string ResendEmail(EmailTemplate template, bool bAsync)
        {
            return Send(template, bAsync, true);
        }

        /// <summary>
        /// Gets Email logs by specified log start time and end time and email status.
        /// </summary>
        /// <param name="starttime">The start time of logs.</param>
        /// <param name="endtime">The end time of logs.</param>
        /// <param name="status">The status value of logs.</param>
        /// <returns>A List entities of EmailTemplate.</returns>
        public List<EmailTemplate> GetEmailLogs(string starttime, string endtime, EmailLog.EmailStatus status)
        {
            EmailLog log = new EmailLog();
            DataSet ds = log.GetLogs(starttime, endtime, status);

            List<EmailTemplate> listET = new List<EmailTemplate>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string detail = row[Properties.Settings.Default.EmailLogDetail] + "";
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("EmailTemplateSetting");
                root.InnerXml = detail;
                doc.AppendChild(root);

                XmlNode tempNode = doc.SelectSingleNode(".//Template");
                if (tempNode != null) 
                    listET.Add(new EmailTemplate(tempNode));
            }
            return listET;
        }
		#endregion

		#region private functions
		private void Initialize()
		{
			this.ht = new Hashtable();
			this.email = new Email();
			if (this.doc != null)
			{
				XmlNodeList templates = this.doc.SelectNodes(".//Template");
				foreach (XmlNode temp in templates)
				{
					EmailTemplate emailTemp = new EmailTemplate(temp);
					this.ht.Add(emailTemp.TemplateName, emailTemp);
				}
			}
		}

        private string Send(EmailTemplate template, bool bAsync, bool bResend)
        {
            if (template.Status.ToUpper().Trim() == EmailTemplate.STATUS_INACTIVE)
                return "";

            // converts content of email template 
            this.content = template.Content;
            this.subject = template.Subject;
            if (!bResend)
            {
                foreach (string keyword in template.BookMarks.Keys)
                {
                    this.content = this.content.Replace("%" + keyword + "%", template.BookMarks[keyword] + "");
                    this.subject = this.subject.Replace("%" + keyword + "%", template.BookMarks[keyword] + "");
                }
            }
            // sends email
            this.email.Sender = template.From;
            this.email.ToList = (template.ToList.Trim() == "") ? null : new ArrayList(template.ToList.Split(EmailTemplate.SPLIT.ToCharArray()));
            this.email.CcList = (template.CcList.Trim() == "") ? null : new ArrayList(template.CcList.Split(EmailTemplate.SPLIT.ToCharArray()));
            this.email.BccList = (template.BccList.Trim() == "") ? null : new ArrayList(template.BccList.Split(EmailTemplate.SPLIT.ToCharArray()));
            this.email.BodyFormat = template.BodyFormat;
            this.email.Attachments = template.Attachment;
            this.email.Subject = this.subject.Trim();
            this.email.Body = this.content.Trim();
            this.email.Send(bAsync);

            if (Properties.Settings.Default.EmailLogAllowed)
            {
                template.Content = this.content;
                template.Subject = this.subject;

                EmailLog log = new EmailLog();
                if (this.email.ErrorMessage.Trim() == String.Empty)
                    log.WriteEmailLog("", template.TemplateNode.OuterXml, EmailLog.EmailStatus.Success, null);
                else
                    log.WriteEmailLog(this.email.ErrorMessage, template.TemplateNode.OuterXml, EmailLog.EmailStatus.Failure, null);
            }
            return this.email.ErrorMessage;
        }
		#endregion
	}
}
