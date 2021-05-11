#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		Email.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/25/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace Node.Lib.Utility
{
    /// <summary>
    /// Provides properties and methods for sending message using the <see cref="System.Net.Mail">System.Net.Mail</see> namespace.
    /// </summary>
    public class Email
    {
		/// <summary>
		/// Constant value for HTML format.
		/// </summary>
		public const string MAIL_FORMAT_HTML = "HTML";
		/// <summary>
		/// Constant value for Text format.
		/// </summary>
		public const string MAIL_FORMAT_TEXT = "TEXT";
		/// <summary>
		/// Constant value for UTF7 encoding.
		/// </summary>
		public const string ENCODING_UTF7 = "UTF7";
		/// <summary>
		/// Constant value for UTF8 encoding.
		/// </summary>
		public const string ENCODING_UTF8 = "UTF8";
		/// <summary>
		/// Constant value for Ascii encoding.
		/// </summary>
		public const string ENCODING_ASCII = "ASCII";
        /// <summary>
        /// Constant value for Unicode encoding.
        /// </summary>
        public const string ENCODING_UNICODE = "UNICODE";
        
		private const string DEFAULT_PORT = "25";

        private bool isCompleted = false;
        private bool isSSL = false;

        private string errorMessage = null;
		private string host = null;
		private string port = null;
		private string sender = null;
		private string subject = null;
		private string body = null;
		private string userName = null;
		private string password = null;
		private string mailFormat = null;
		private string encoding = null;

		private ArrayList toList = null;
		private ArrayList ccList = null;
		private ArrayList bccList = null;
		private ArrayList attachments = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Utility.Email">Email</see> class.
		/// </summary>
		public Email()
		{
        }

        #region public function
        /// <summary>
        /// Sends an email.
        /// </summary>
        public void Send()
        {
            Send(false);
        }

        /// <summary>
		/// Sends an e-mail.
		/// </summary>
        /// <param name="bAsync">It is true, if send email using async method; otherwise, false.</param>
        public void Send(bool bAsync)
		{
			if (this.sender == null || this.toList == null)
				throw new Exception("There is sender or receiver information!");

			MailMessage mail = new MailMessage();

            mail.From = new MailAddress(this.sender);
			if (this.subject != null)
				mail.Subject = this.subject;
			
			if (this.body != null)
				mail.Body = this.body;

			if (this.toList != null)
			{
				foreach (object to in this.toList)
                    mail.To.Add(new MailAddress(to + ""));
			}
			if (this.ccList != null)
			{
				foreach (object cc in this.ccList)
                    mail.CC.Add(new MailAddress(cc + ""));
			}
			if (this.bccList != null)
			{
				foreach (object bcc in this.bccList)
                    mail.Bcc.Add(new MailAddress(bcc + ""));
			}
			if (this.mailFormat != null)
			{
				switch (this.mailFormat.ToUpper())
				{
					case MAIL_FORMAT_HTML: mail.IsBodyHtml = true; break;
					default: mail.IsBodyHtml = false; break;
				}
			}
			if (this.encoding != null)
			{
				switch (this.encoding.ToUpper())
				{
					case ENCODING_UTF7: 
                        mail.BodyEncoding = Encoding.UTF7;
                        mail.SubjectEncoding = Encoding.UTF7;
                        break;
					case ENCODING_UTF8: 
                        mail.BodyEncoding = Encoding.UTF8;
                        mail.SubjectEncoding = Encoding.UTF8;
                        break;
                    case ENCODING_UNICODE: 
                        mail.BodyEncoding = Encoding.Unicode;
                        mail.SubjectEncoding = Encoding.Unicode;
                        break;
					default: 
                        mail.BodyEncoding = Encoding.ASCII;
                        mail.SubjectEncoding = Encoding.ASCII;
                        break;
				}
			}
			if (this.attachments != null)
			{
                foreach (object file in attachments)
                {
                    if (file is String)
                        mail.Attachments.Add(new Attachment(file + ""));
                    else if (file is Attachment)
                        mail.Attachments.Add((Attachment)file);
                }
			}
            SmtpClient smtp = new SmtpClient();
            if (this.host != null)
                smtp.Host = this.host;
            if (this.port != null)
                smtp.Port = int.Parse(this.port);

            if (this.userName != null && this.password != null)
            {
                NetworkCredential cred = new NetworkCredential(this.userName, this.password);
                smtp.Credentials = cred;
            }
            smtp.EnableSsl = this.isSSL;
            if (bAsync)
            {
                smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                smtp.SendAsync(mail, "sending message");
            }
            else
            {
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    this.errorMessage = ex.ToString();
                }
                this.isCompleted = true;
            }
        }
        #endregion

        #region properties
        /// <summary>
		/// Gets or Sets a SMTP Host.
		/// </summary>
		public string Host
		{
			get { return this.host; }
			set { this.host = value; }
		}

		/// <summary>
		/// Gets or Sets a SMTP Port. Default is <b>25</b>.
		/// </summary>
		public string Port
		{
			get { return this.port; }
			set { this.port = value; }
		}

		/// <summary>
		/// Gets or Sets a sender email address.
		/// </summary>
		public string Sender
		{
			get { return this.sender; }
			set { this.sender = value; }
		}

		/// <summary>
		/// Gets or Sets a collection of to email addresses.
		/// </summary>
		public ArrayList ToList
		{
			get { return this.toList; }
			set { this.toList = value; }
		}

		/// <summary>
		/// Gets or Sets an email subject.
		/// </summary>
		public string Subject
		{
			get { return this.subject; }
			set { this.subject = value; }
		}

		/// <summary>
		/// Gets or Sets an email body content.
		/// </summary>
		public string Body
		{
			get { return this.body; }
			set { this.body = value; }
		}

		/// <summary>
		/// Gets or Sets the user name for SMTP Host, for security email sending.
		/// </summary>
		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; }
		}

		/// <summary>
		/// Gets or Sets the password for SMTP Host, for security email sending.
		/// </summary>
		public string Password
		{
			set { this.password = value; }
		}

		/// <summary>
		/// Gets or Sets an email body format.
		/// </summary>
		public string BodyFormat
		{
			get { return this.mailFormat; }
			set { this.mailFormat = value; }
		}

		/// <summary>
		/// Gets or Sets an email encoding.
		/// </summary>
		public string BodyEncoding
		{
			get { return this.encoding; }
			set { this.encoding = value; }
		}

        /// <summary>
        /// Gets the returned error message. If the message is empty string, that means sent successfully.
        /// </summary>
        public string ErrorMessage
        {
            get { return this.errorMessage + ""; }
        }

        /// <summary>
        /// Gets the indicator whether message is sent completely.
        /// </summary>
        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }

        /// <summary>
        /// Gets or sets the indicator for enable SSL
        /// </summary>
        public bool IsSSL
        {
            get { return this.isSSL; }
            set { this.isSSL = value; }
        }

		/// <summary>
		/// Gets or Sets a collection of CC lists.
		/// </summary>
		public ArrayList CcList
		{
			get { return this.ccList; }
			set { this.ccList = value; }
		}

		/// <summary>
		/// Gets or Sets a collection of BCC lists.
		/// </summary>
		public ArrayList BccList
		{
			get { return this.bccList; }
			set { this.bccList = value; }
		}

		/// <summary>
		/// Gets or Sets a collection of Attachments.
		/// </summary>
		public ArrayList Attachments
		{
			get { return this.attachments; }
			set { this.attachments = value; }
        }
        #endregion

        #region private function
        private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                this.errorMessage = e.Error.ToString();
            this.isCompleted = true;
        }
        #endregion

    }
}
