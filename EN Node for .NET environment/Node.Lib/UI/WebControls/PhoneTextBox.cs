using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

//using Node.Lib.UI.DataDictionary;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// TextBox for input phone number. (no extension)
	/// </summary>
	public class PhoneTextBox : TextBox
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		//private string scriptBase = "~/Scripts/EAF";

		private string ScriptBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ScriptBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/*
		/// <summary>
		/// Get or set script base of EAF javascript. Default is "~/Scripts/EAF".
		/// </summary>
		public string ScriptBase
		{
			get { return this.scriptBase; }
			set { this.scriptBase = value; }
		}
		*/

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			PageUtility.RegisterUtilsScript(this.Page);
			base.OnPreRender(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
            //writer.AddAttribute("onKeyUp", "Utils.checkPhoneFormat(this);");
			base.AddAttributesToRender(writer);
		}
	}
}
