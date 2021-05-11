using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

//using Node.Lib.UI.DataDictionary;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// EAFButton control, implementing click once funtion.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:EAFButton runat=server></{0}:EAFButton>")]
	public class EAFButton : Button
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		private bool clickOnce = false;
		private string disabledCssClass = "";
		private string disabledText = "";
		private string confirmMessage = "";


		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Get or set ClickOnce attribute. Set true if you need this button only click once.
		/// </summary>
		public bool ClickOnce
		{
			get { return clickOnce; }
			set { clickOnce = value; }
		}

		/// <summary>
		/// Get or set confirm message when button is click.  
		/// Set empty string if you don't need confirm action.
		/// </summary>
		public string ConfirmMessage
		{
			get { return confirmMessage; }
			set { confirmMessage = value; }
		}

		/// <summary>
		/// CSS Class when the button is disabled
		/// </summary>
		public string DisabledCssClass
		{
			get	{ return disabledCssClass; }
			set	{ disabledCssClass = value;	}
		}
		
		/// <summary>
		/// Text to show when the button is disabled
		/// </summary>
		public string DisabledText
		{
			get	{ return disabledText; }
			set	{ disabledText = value;	}
		}

		private string OldCssClass
		{
			get { return ""+ViewState["OldCssClass"]; }
			set { ViewState["OldCssClass"] = value; }
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
			if (this.ClickOnce)
			{
				StringBuilder s = new StringBuilder();
				s.AppendLine("<script type=\"text/javascript\"><!-- ");
				s.AppendLine("function " + this.ClientID + "_ClickOnce(btnObj)");
				s.Append("{");
				if (this.DisabledText != null && this.DisabledText != "")
					s.Append("btnObj.value=\"" + this.DisabledText + "\";");
				if (this.DisabledCssClass != null && this.DisabledCssClass != "")
					s.Append("btnObj.className=\"" + this.DisabledCssClass + "\";");
				s.Append("btnObj.disabled=true;");
				s.Append(this.Page.ClientScript.GetPostBackEventReference(this, "") + ";");
				s.AppendLine("}");
				s.AppendLine(" //--></script>");
				this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.ClientID + "_ClickOnce", s.ToString());
				this.OnClientClick = this.ClientID + "_ClickOnce(this);";
			}

			if (this.confirmMessage != null && this.confirmMessage != "")
			{
				this.OnClientClick += ";return Utils.confirmMsg('" + this.confirmMessage.Replace("'", "\\'") + "')";
			}


			if (!this.Enabled && this.DisabledCssClass != "")
			{
				this.OldCssClass = this.CssClass;
				this.CssClass = this.DisabledCssClass;
			}
			else
			{
				if (this.OldCssClass != "") this.CssClass = this.OldCssClass;
			}
			
			base.OnPreRender(e);
		}
	}
}
