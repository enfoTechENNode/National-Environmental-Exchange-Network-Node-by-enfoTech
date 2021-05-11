using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Node.Lib.UI.DataDictionary;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// TextBox with droplist functions and popup dialog window.
	/// </summary>
	public class SelectTextBox : TextBox, ICallbackEventHandler
	{
		//***********************************************************************
		// Constants
		//***********************************************************************

		/// <summary>
		/// When you want to use Callback solution.
		/// </summary>
		public const string ACTION_CALLBACK = "CALLBACK";
		/// <summary>
		/// When you want to use popup dialog window solution.
		/// </summary>
		public const string ACTION_DIALOGWIN = "DIALOGWIN";
		/// <summary>
		/// For both Callback and popup dialog window solution.
		/// </summary>
		public const string ACTION_BOTH = "BOTH";

		//***********************************************************************
		// Private members
		//***********************************************************************

		private string resultStr = "";

		private int showRcds = -1;
		private string argSplitter = "|";
		private string rowSplitter = "_||_";
		private string columnSplitter = "|";
		private string dialogLink = "";
		private string dialogWinIcon = "";
		private string callbackJavascript = "";
		private string actionType = ACTION_BOTH;
		private int dialogWidth = 600;
		private int dialogHeight = 400;
		private int callBackWidth = 300;
		private bool checkSelected = true;
		//private string scriptBase = "~/Scripts/EAF";
		private string focusCss = "eaf_SelectTBFocus";
		private string blurCss = "eaf_SelectTBBlur";
		private string blurCssRegular = "asp_TBBlur";

		private string initValue
		{
			get
			{
				object o = ViewState["initValue"];
				return (o != null ? o.ToString() : "");
			}
			set
			{
				ViewState["initValue"] = value;
			}
		}
		private bool isInitCalled = false;

		private string ScriptBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ScriptBase; }
		}

		private string ImageBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Records number shown in DHTML dropdown list. Default value is -1, and it will show all records of result.
		/// </summary>
		public int ShowRecords
		{
			get { return this.showRcds; }
			set { this.showRcds = value; }
		}


		/// <summary>
		/// URL link for your dialog page
		/// </summary>
		public string DialogWinLink
		{
			get { return this.dialogLink; }
			set { this.dialogLink = value; }
		}

		/// <summary>
		/// Image link for popup the dialog window.
		/// </summary>
		public string DialogWinIcon
		{
			get { return this.dialogWinIcon; }
			set { this.dialogWinIcon = value; }
		}

		/// <summary>
		/// Call back argument splitter, default is "|".
		/// </summary>
		public string ArgSplitter
		{
			get { return this.argSplitter; }
			set { this.argSplitter = value; }
		}

		/// <summary>
		/// Data row splitter of result string, default is "_||_".
		/// </summary>
		public string RowSplitter
		{
			get { return this.rowSplitter; }
			set { this.rowSplitter = value; }
		}

		/// <summary>
		/// Data field splitter of result string, default is "|".
		/// </summary>
		public string ColumnSplitter
		{
			get { return this.columnSplitter; }
			set { this.columnSplitter = value; }
		}

		/// <summary>
		/// Call back javascript function name, default is Control's ClientID + "CallBack" .
		/// function will be looked like this callbackfun(result,context)
		/// </summary>
		public string CallbackJavascript
		{
			get
			{
				if (this.callbackJavascript == "")
					return this.ClientID + "CallBack";
				else
					return this.callbackJavascript;
			}
			set { this.callbackJavascript = value; }
		}

		/// <summary>
		/// Action type of the control, can be 
		/// 1. Popup dialog window
		/// 2. Callback dropdown list
		/// 3. Both
		/// Refer to constants
		/// </summary>
		public string ActionType
		{
			get { return this.actionType; }
			set { this.actionType = value; }
		}

		/// <summary>
		/// Dialog window width
		/// </summary>
		public int DialogWidth
		{
			get { return this.dialogWidth; }
			set { this.dialogWidth = value; }
		}

		/// <summary>
		/// Dialog window height
		/// </summary>
		public int DialogHeight
		{
			get { return this.dialogHeight; }
			set { this.dialogHeight = value; }
		}

		/// <summary>
		/// Callback dropdown list width (height is automatically generated)
		/// </summary>
		public int CallBackWidth
		{
			get { return this.callBackWidth; }
			set { this.callBackWidth = value; }
		}

		/// <summary>
		/// Get or set CSS when the TextBox receive focus.
		/// </summary>
		public string FocusCss
		{
			get { return this.focusCss; }
			set { this.focusCss = value; }
		}

		/// <summary>
		/// Get or set CSS when the TextBox lose focus. Default is a special CSS define for SelectTextBox.
		/// </summary>
		public string BlurCss
		{
			get { return this.blurCss; }
			set { this.blurCss = value; }
		}

		/// <summary>
		/// Get or set CSS when the TextBox lose focus. Default is a special CSS define for regualr TextBox.
		/// </summary>
		public string BlurCssRegular
		{
			get { return this.blurCssRegular; }
			set { this.blurCssRegular = value; }
		}

		/// <summary>
		/// Get or set if the control have to check hidden value. Default is true.
		/// </summary>
		public bool CheckSelected
		{
			get { return this.checkSelected; }
			set { this.checkSelected = value; }
		}

		/// <summary>
		/// Hidden field client ID for you to use with javascript
		/// </summary>
		public string HiddenFieldClientID
		{
			get { return this.ClientID + "Hidden"; }
		}

		/// <summary>
		/// Return hidden field value (usually your ID)
		/// </summary>
		public string ReturnValue
		{
			get
			{
				if (this.isInitCalled)
					return this.initValue;
				else
					return "" + this.Page.Request[this.HiddenFieldClientID];
			}
		}


		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// Set default value of SelectTextBox
		/// </summary>
		/// <param name="text"></param>
		/// <param name="val"></param>
		public void Initialize(string text, string val)
		{
			this.Text = text;
			this.initValue = val;
			this.isInitCalled = true;
		}

		/// <summary>
		/// Get textbox entered text from pass in argument.
		/// </summary>
		/// <param name="eArg">passed in argument by callback javascript</param>
		/// <returns>text user entered</returns>
		public string GetEnteredText(string eArg)
		{
			string[] args = eArg.Split(this.ArgSplitter.ToCharArray());
			string ctrlUniqID = args[0];
			string ctrlValue = args[1];

			return ctrlValue;
		}

		/// <summary>
		/// Generate DHTML dropdown list based on the DataRow collection.
		/// </summary>
		/// <param name="col">DataRow collection or Array of search result.</param>
		/// <param name="txtFld">field name shown in dropdown list Name</param>
		/// <param name="valFld">field name will be placed in hidden field once user select one. You can get the value by ReturnValue property.</param>
		/// <param name="descFld">field name shown in dropdown description.</param>
		/// <returns></returns>
		public string GenerateResult(ICollection col, string txtFld, string valFld, string descFld)
		{
			if (col.Count <= 0) return "";

			StringBuilder s = new StringBuilder();

			int i = 0;
			foreach (DataRow dr in col)
			{
				s.Append(dr[txtFld] + this.columnSplitter + dr[valFld] + this.columnSplitter + dr[descFld] + this.rowSplitter);

				if (this.ShowRecords == 0)
				{
					s = new StringBuilder();
				}
				else if (this.ShowRecords > 0)
				{
					i++;
					if (i >= this.ShowRecords) break;
				}
			}

			if (s.ToString().Length <= 0)
				return "";
			else
				return s.ToString().Substring(0, s.ToString().Length - this.rowSplitter.Length);
		}


		//***********************************************************************
		// Delegate methods
		//***********************************************************************

		/// <summary>
		/// CallBackAction handler
		/// </summary>
		/// <param name="eArg"></param>
		/// <returns></returns>
		public delegate string CallBackActionHandler(string eArg);

		/// <summary>
		/// Occurs when Callback action is triggered..
		/// </summary>
		public event CallBackActionHandler CallBackAction;

		/// <summary>
		/// Raise the CallBackAction event.
		/// </summary>
		/// <param name="eArg"></param>
		/// <returns></returns>
		protected virtual string OnCallBackAction(string eArg)
		{
			if (CallBackAction == null) return "";

			return CallBackAction(eArg);
		}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="eArg"></param>
		///// <returns></returns>
		//public string RaiseCallbackEvent(string eArg)
		//{
		//    return OnCallBackAction(eArg);
		//}

		/// <summary>
		/// RaiseCallbackEvent of interface ICallbackEventHandler
		/// </summary>
		/// <param name="eventArgument"></param>
		public void RaiseCallbackEvent(string eventArgument)
		{
			this.resultStr = OnCallBackAction(eventArgument);
		}

		/// <summary>
		/// GetCallbackResult of interface ICallbackEventHandler
		/// </summary>
		/// <returns></returns>
		public string GetCallbackResult()
		{
			return this.resultStr;
		}

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// Register javascript to page before rendering the page.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (this.actionType.ToUpper() == ACTION_CALLBACK || this.actionType.ToUpper() == ACTION_BOTH)
			{
				if (!this.Page.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_Utils))
				{
					StringBuilder s = new StringBuilder();
					s.AppendLine("<script type=\"text/javascript\" src=\"" + ResolveUrl(this.ScriptBase) + "Utils.js\" language=\"javascript\"></script>");
					this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), ClientScriptRegID.EAF_Utils, s.ToString());
				}

				if (!this.Page.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_CallBackDiv))
				{
					StringBuilder s = new StringBuilder();
					s.AppendLine("<script type=\"text/javascript\" src=\"" + ResolveUrl(this.ScriptBase) + "CallBackDiv.js\" language=\"javascript\"></script>");

					this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), ClientScriptRegID.EAF_CallBackDiv, s.ToString());
				}

				if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.CallbackJavascript))
				{
					StringBuilder s = new StringBuilder();
					s.AppendLine("<script type=\"text/javascript\" language=\"javascript\"><!--");
					s.AppendLine("function " + this.ClientID + "CallBack(result,context) { eaf_CallBackDiv.callBackHandler(result,context," + this.callBackWidth + ",'" + this.rowSplitter + "','" + this.columnSplitter + "'); };");
					s.AppendLine("--></script>");

					this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.CallbackJavascript, s.ToString());
				}
			}
		}

		/// <summary>
		/// Add attrubutes to the control
		/// </summary>
		/// <param name="writer"> The HTML writer to write out to </param>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.actionType.ToUpper() == ACTION_CALLBACK || this.actionType.ToUpper() == ACTION_BOTH)
			{
				writer.AddAttribute("onKeyUp", this.Page.ClientScript.GetCallbackEventReference(this, "eaf_CallBackDiv.getCallBackVal('" + this.ClientID + "','" + this.argSplitter + "')", this.CallbackJavascript, "'" + this.ClientID + "'"));
				if (this.checkSelected)
					writer.AddAttribute("onBlur", "return eaf_CallBackDiv.delayHide('" + this.ClientID + "',true);Utils.setObjClass(this,'" + this.BlurCss + "');");
				else
					writer.AddAttribute("onBlur", "return eaf_CallBackDiv.delayHide('" + this.ClientID + "',false);Utils.setObjClass(this,'" + this.BlurCss + "');");
				writer.AddAttribute("onFocus", "Utils.setObjClass(this,'" + this.FocusCss + "');");
			}

			base.AddAttributesToRender(writer);
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.actionType.ToUpper() == ACTION_CALLBACK || this.actionType.ToUpper() == ACTION_BOTH)
				this.CssClass = this.BlurCss;
			else
				this.CssClass = this.blurCssRegular;

			base.Render(output);

			if (this.actionType.ToUpper() == ACTION_DIALOGWIN || this.actionType.ToUpper() == ACTION_BOTH)
			{
				output.Write("<a href=\"javascript:void(0);\" title=\"" + this.ToolTip + "\" onClick=\"Utils.DialogWinAction('" + ResolveUrl(this.DialogWinLink) + "','" + this.ClientID + "'," + this.DialogWidth + "," + this.dialogHeight + ");\">");

				if (this.DialogWinIcon == "") this.DialogWinIcon = this.ImageBase + "InfoIcon.gif";

				output.Write("<img src=\"" + ResolveUrl(this.DialogWinIcon) + "\" border=\"0\" align=\"middle\">");
				output.Write("</a>");
			}

			output.Write("<input type=\"hidden\" name=\"" + this.HiddenFieldClientID + "\" id=\"" + this.HiddenFieldClientID + "\" value=\"" + this.ReturnValue + "\">");
			this.isInitCalled = false;
		}

	}
}
