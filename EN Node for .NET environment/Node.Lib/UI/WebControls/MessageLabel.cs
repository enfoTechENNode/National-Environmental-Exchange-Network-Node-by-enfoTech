using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// MessageIcon enum
	/// </summary>
	public enum MessageIcon
	{
		/// <summary>
		/// no icon
		/// </summary>
		None,
		/// <summary>
		/// OK icon
		/// </summary>
		OK,
		/// <summary>
		/// Error icon
		/// </summary>
		Error,
		/// <summary>
		/// Warning icon
		/// </summary>
		Warning,
		/// <summary>
		/// unknown icon
		/// </summary>
		Unknown,
		/// <summary>
		/// Info icon
		/// </summary>
		Info
	}

	/// <summary>
	/// Message display type
	/// </summary>
	public enum MessageDisplayType
	{
		/// <summary>
		/// Display message in page
		/// </summary>
		Page,
		/// <summary>
		/// Display message as javascript alert.
		/// </summary>
		Alert,
		/// <summary>
		/// Display message in page and javascript alert.
		/// </summary>
		Both
	}

	/// <summary>
	/// Message text colro.
	/// </summary>
	public enum MessageColor
	{
		/// <summary>
		/// dark blue.
		/// </summary>
		DarkBlue,
		/// <summary>
		/// red
		/// </summary>
		Red,
		/// <summary>
		/// green
		/// </summary>
		Green
	}

	/// <summary>
	/// Label for display message on page.
	/// </summary>
	public class MessageLabel : WebControl
	{
		//***********************************************************************
		// Public constants
		//***********************************************************************

		/// <summary>
		/// Constant for display no icon.
		/// </summary>
		public const string ICON_NONE = "NONE";
		/// <summary>
		/// Constant for display OK icon.
		/// </summary>
		public const string ICON_OK = "OK";
		/// <summary>
		/// Constant for display error icon.
		/// </summary>
		public const string ICON_ERROR = "ERROR";
		/// <summary>
		/// Constant for display warning icon.
		/// </summary>
		public const string ICON_WARNING = "WARNING";
		/// <summary>
		/// Constant for display unknown icon.
		/// </summary>
		public const string ICON_UNKNOWN = "UNKNOWN";
		/// <summary>
		/// Constant for display information icon.
		/// </summary>
		public const string ICON_INFO = "INFO";

		/// <summary>
		/// Constant for defining message type: In page.
		/// </summary>
		public const string TYPE_PAGE = "PAGE";
		/// <summary>
		/// Constant for defining message type: Javascript alert.
		/// </summary>
		public const string TYPE_ALERT = "ALERT";
		/// <summary>
		/// Constant for defining message type: In page and javascript alert.
		/// </summary>
		public const string TYPE_BOTH = "BOTH";

		/// <summary>
		/// Constant for defining message color: Default is dark blue.
		/// </summary>
		public const string MSGCLR_DEFAULT = "#000099";
		/// <summary>
		/// Constant for defining message color: Red.
		/// </summary>
		public const string MSGCLR_RED = "#CC0000";
		/// <summary>
		/// Constant for defining message color: Green.
		/// </summary>
		public const string MSGCLR_GREEN = "#006600";

		//***********************************************************************
		// Private members
		//***********************************************************************
		private string msgIcon = ICON_NONE;
		private string msgType = TYPE_PAGE;
		//private string imageBase = "~/Images/EAF";
		private string msgIconSrc = "";
		private string msgContent = "";
		private string msgCSS = "eaf_MsgLbl";
		private string msgClr = MSGCLR_DEFAULT;

		private string ImageBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************
		
		/// <summary>
		/// Get or set message icon.
		/// </summary>
		public string MsgIcon
		{
			get { return this.msgIcon; }
			set { this.msgIcon = value; }	 
		}

		/// <summary>
		/// Get or set message type.
		/// </summary>
		public string MsgType
		{
			get { return this.msgType; }
			set { this.msgType = value; }
		}

		/*
		/// <summary>
		/// Get or set image base of related images. Default is "~/Images/EAF".
		/// </summary>
		public string ImageBase
		{
			get { return this.imageBase; }
			set { this.imageBase = value; }
		}
		*/
		
		/// <summary>
		/// Get or set your own message icon. Image icon source url.
		/// </summary>
		public string MsgIconSrc
		{
			get { return this.msgIconSrc; }
			set { this.msgIconSrc = value; }
		}

		/// <summary>
		/// Get or set message text.
		/// </summary>
		public string MsgContent
		{
			get { return this.msgContent; }
			set { this.msgContent = value; }
		}

		/// <summary>
		/// Get or set message style.
		/// </summary>
		public string MsgCSS
		{
			get { return this.msgCSS; }
			set { this.msgCSS = value; }
		}

		/// <summary>
		/// Get or set message color.
		/// </summary>
		public string MsgColor
		{
			get { return this.msgClr; }
			set { this.msgClr = value; }
		}

		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// set message, no icon. if message is null or empty string, ShowMsg will be set false.
		/// </summary>
		/// <param name="msg">message string</param>
		public void setMessage(string msg)
		{
			setMessage(ICON_NONE, msg);
		}

		/// <summary>
		/// set message with icon. if message is null or empty string, ShowMsg will be set false.
		/// </summary>
		/// <param name="icon">icon style</param>
		/// <param name="msg">message string</param>
		public void setMessage(string icon, string msg)
		{
			setMessage(icon, msg, MSGCLR_DEFAULT);
		}

		/// <summary>
		/// set message with icon. if message is null or empty string, ShowMsg will be set false.
		/// </summary>
		/// <param name="icon">Icon style</param>
		/// <param name="msg">Message string</param>
		/// <param name="color">Message color.</param>
		public void setMessage(string icon, string msg, string color)
		{
			this.MsgContent = msg;
			this.MsgIcon = icon;
			this.MsgColor = color;
		}

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			if (this.Visible && this.MsgContent != null && this.MsgContent.Trim() != "")
			{
				if (this.MsgType.ToUpper() == TYPE_ALERT || this.MsgType.ToUpper() == TYPE_BOTH)
				{
					StringBuilder s = new StringBuilder();

					s.Append("<script type=\"text/javascript\" language=\"javascript\"><!--");
					s.Append("alert('" + this.MsgContent.Replace("'", "\\'") + "');");
					s.Append("--></script>");

					this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), this.ClientID + "_msg", s.ToString());
				}
			}
		}
		
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.MsgContent != null && this.MsgContent.Trim() != "")
			{
				if (this.MsgType.ToUpper() == TYPE_PAGE || this.MsgType.ToUpper() == TYPE_BOTH)
				{
					StringBuilder s = new StringBuilder();

					s.Append("<div class=\"" + this.MsgCSS + "\" style=\"color:" + this.MsgColor + "\">");

					if (this.MsgIconSrc.Trim() != "")
						s.Append("<img src=\"" + ResolveUrl(this.MsgIconSrc) + "\" align=\"middle\" alt=\"\" />");
					else if (this.MsgIcon.ToUpper() == ICON_OK)
                        s.Append("<img src=\"" + ResolveUrl(this.ImageBase) + "msgico_OK.gif\" align=\"middle\" alt=\"\" />");
					else if (this.MsgIcon.ToUpper() == ICON_ERROR)
                        s.Append("<img src=\"" + ResolveUrl(this.ImageBase) + "msgico_Error.gif\" align=\"middle\" alt=\"\" />");
					else if (this.MsgIcon.ToUpper() == ICON_INFO)
                        s.Append("<img src=\"" + ResolveUrl(this.ImageBase) + "msgico_Info.gif\" align=\"middle\" alt=\"\" />");
					else if (this.MsgIcon.ToUpper() == ICON_WARNING)
                        s.Append("<img src=\"" + ResolveUrl(this.ImageBase) + "msgico_Warning.gif\" align=\"middle\" alt=\"\" />");
					else if (this.MsgIcon.ToUpper() == ICON_UNKNOWN)
                        s.Append("<img src=\"" + ResolveUrl(this.ImageBase) + "msgico_Unknown.gif\" align=\"middle\" alt=\"\" />");

					s.Append(this.MsgContent);

					s.Append("</div>");

					output.Write(s.ToString());
				}
			}
		}

	}
}
