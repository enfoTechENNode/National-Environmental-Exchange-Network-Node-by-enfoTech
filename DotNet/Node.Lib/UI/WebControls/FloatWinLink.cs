using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.UI.DataDictionary;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Show a floating DHTML window in page.
	/// </summary>
	public class FloatWinLink : HyperLink
	{
		//***********************************************************************
		// private members
		//***********************************************************************

		//private string scriptBase = "~/Scripts/EAF";
		private string winTitle = "";
		private int winWidth = 400;
		private int winHeight = 400;
		private int offsetX = 0;
		private int offsetY = 0;
		private string pageLink = "";
		private bool refreshOnClosed = false;
		private bool allowMove = false;
		private bool showGlassBg = false;
		private bool alignCenter = false;

		private string ScriptBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ScriptBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Get or set title of popup window.
		/// </summary>
		public string WinTitle
		{
			get { return this.winTitle; }
			set { this.winTitle = value; }
		}

		/// <summary>
		/// Get or set width of popup window.
		/// </summary>
		public int WinWidth
		{
			get { return this.winWidth; }
			set { this.winWidth = value; }
		}

		/// <summary>
		/// Get or set height of popup window.
		/// </summary>
		public int WinHeight
		{
			get { return this.winHeight; }
			set { this.winHeight = value; }
		}

		/// <summary>
		/// Get or set X-axis offset popup window related to mouse click.
		/// </summary>
		public int OffsetX
		{
			get { return this.offsetX; }
			set { this.offsetX = value; }
		}

		/// <summary>
		/// Get or set Y-axis offset popup window related to mouse click.
		/// </summary>
		public int OffsetY
		{
			get { return this.offsetY; }
			set { this.offsetY = value; }
		}

		/// <summary>
		/// Page url of popup window.
		/// </summary>
		public string PageLink
		{
			get { return this.pageLink; }
			set { this.pageLink = value; }
		}

		/// <summary>
		/// Reload page when hit close FloatWin
		/// </summary>
		public bool RefreshOnClosed
		{
			get { return this.refreshOnClosed; }
			set { this.refreshOnClosed = value; }
		}

		/// <summary>
		/// Allow user drag DIV window
		/// </summary>
		public bool AllowMove
		{
			get { return this.allowMove; }
			set { this.allowMove = value; }
		}

		/// <summary>
		/// show glass
		/// </summary>
		public bool ShowGlassBackground
		{
			get { return this.showGlassBg; }
			set { this.showGlassBg = value; }
		}

		/// <summary>
		/// align to center, will ignore offset
		/// </summary>
		public bool AlignCenter
		{
			get { return this.alignCenter; }
			set { this.alignCenter = value; }
		}

		//***********************************************************************
		// Conrtol events
		//***********************************************************************

		/// <summary>
		/// PreRender.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			PageUtility.RegisterUtilsScript(this.Page);
			PageUtility.RegisterScript(this.Page, ClientScriptRegID.EAF_GlassBgDiv, "<script language=\"javascript\" type=\"text/javascript\" src=\"" + ResolveUrl(this.ScriptBase) + "GlassBgDiv.js\"></script>");
			PageUtility.RegisterScript(this.Page, ClientScriptRegID.EAF_FloatWin, "<script language=\"javascript\" type=\"text/javascript\" src=\"" + ResolveUrl(this.ScriptBase) + "FloatWin.js\"></script>");

			if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ClientID+"Obj"))
			{
				StringBuilder s = new StringBuilder();

                s.AppendLine("<script language=\"JavaScript\" type=\"text/javascript\"><!--");
				
				s.AppendLine("var "+this.ClientID+"Obj = new FloatWin('"+this.ClientID+"Obj');");
				s.AppendLine(this.ClientID + "Obj.IsRefreshOnClosed=" + (this.refreshOnClosed ? "true" : "false") + ";");
				s.AppendLine(this.ClientID + "Obj.IsDivScroll=false;");
				s.AppendLine(this.ClientID + "Obj.ShowGlassBg=" + (this.showGlassBg ? "true" : "false") + ";");
				s.AppendLine(this.ClientID + "Obj.AlignCenter=" + (this.alignCenter ? "true" : "false") + ";");
				s.AppendLine(this.ClientID + "Obj.initIFrame();");
				s.AppendLine(this.ClientID + "Obj.initObjs();");

				if (this.allowMove)
				{
					s.AppendLine("function " + this.ClientID + "ObjMouseDown() { " + this.ClientID + "Obj.MouseDownEvent(); };");
					s.AppendLine("function " + this.ClientID + "ObjMouseUp() { " + this.ClientID + "Obj.MouseUpEvent(); };");
					s.AppendLine("function " + this.ClientID + "ObjMouseMove() { " + this.ClientID + "Obj.MouseMoveEvent(); };");

					s.AppendLine("if(document.attachEvent){");
					s.AppendLine("document.attachEvent('onmousedown'," + this.ClientID + "ObjMouseDown);");
					s.AppendLine("document.attachEvent('onmouseup'," + this.ClientID + "ObjMouseUp);");
					s.AppendLine("document.attachEvent('onmousemove'," + this.ClientID + "ObjMouseMove);");
					s.AppendLine("}else if(document.addEventListener){");
					s.AppendLine("document.addEventListener('mousedown'," + this.ClientID + "ObjMouseDown,false);");
					s.AppendLine("document.addEventListener('mouseup'," + this.ClientID + "ObjMouseUp,false);");
					s.AppendLine("document.addEventListener('mousemove'," + this.ClientID + "ObjMouseMove,false);");
					s.AppendLine("}");
				}
				s.AppendLine("--></script>");
				this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.ClientID + "Obj", s.ToString());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute("onClick", this.ClientID + "Obj.show(this,'" + this.WinTitle + "','" + ResolveUrl(this.PageLink) + "'," + this.OffsetX + "," + this.OffsetY + "," + this.WinWidth + "," + this.WinHeight + ");");
			base.AddAttributesToRender(writer);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			this.NavigateUrl = "javascript:void(0);";
			base.Render(output);
		}

	}
}
