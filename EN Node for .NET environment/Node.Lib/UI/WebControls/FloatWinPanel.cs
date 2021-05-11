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
	public class FloatWinPanel : PlaceHolder
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
		//private string pageLink = "";
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

		///// <summary>
		///// Page url of popup window.
		///// </summary>
		//public string PageLink
		//{
		//    get { return this.pageLink; }
		//    set { this.pageLink = value; }
		//}

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
		/// Show glass DIV in background
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			StringBuilder s = new StringBuilder();

			output.WriteLine("<script language=\"JavaScript\" type=\"text/javascript\" ><!--");
			output.WriteLine("var " + this.ClientID + "Obj = new FloatWin('" + this.ClientID + "Obj');");
			output.WriteLine(this.ClientID + "Obj.IsRefreshOnClosed=" + (this.refreshOnClosed ? "true" : "false") + ";");
			output.WriteLine(this.ClientID + "Obj.IsDivScroll=true;");
			output.WriteLine(this.ClientID + "Obj.ShowGlassBg=" + (this.showGlassBg ? "true" : "false") + ";");
			output.WriteLine(this.ClientID + "Obj.AlignCenter=" + (this.alignCenter ? "true" : "false") + ";");
			output.WriteLine(this.ClientID + "Obj.Title='" + this.winTitle +"';");
			output.WriteLine(this.ClientID + "Obj.OffX=" + this.offsetX + ";");
			output.WriteLine(this.ClientID + "Obj.OffY=" + this.offsetY + ";");
			output.WriteLine(this.ClientID + "Obj.W=" + this.winWidth + ";");
			output.WriteLine(this.ClientID + "Obj.H=" + this.winHeight + ";");
			output.WriteLine(this.ClientID + "Obj.writeFloatWinDivHead();");
			output.WriteLine("--></script>");

			base.Render(output);

            output.WriteLine("<script language=\"JavaScript\" type=\"text/javascript\"><!--");
			output.WriteLine(this.ClientID + "Obj.writeFloatWinDivTail();");
			output.WriteLine(this.ClientID + "Obj.initObjs();");

			if (this.allowMove)
			{
				output.WriteLine("function " + this.ClientID + "ObjMouseDown() { " + this.ClientID + "Obj.MouseDownEvent(); };");
				output.WriteLine("function " + this.ClientID + "ObjMouseUp() { " + this.ClientID + "Obj.MouseUpEvent(); };");
				output.WriteLine("function " + this.ClientID + "ObjMouseMove() { " + this.ClientID + "Obj.MouseMoveEvent(); };");

				output.WriteLine("if(document.attachEvent){");
				output.WriteLine("document.attachEvent('onmousedown'," + this.ClientID + "ObjMouseDown);");
				output.WriteLine("document.attachEvent('onmouseup'," + this.ClientID + "ObjMouseUp);");
				output.WriteLine("document.attachEvent('onmousemove'," + this.ClientID + "ObjMouseMove);");
				output.WriteLine("}else if(document.addEventListener){");
				output.WriteLine("document.addEventListener('mousedown'," + this.ClientID + "ObjMouseDown,false);");
				output.WriteLine("document.addEventListener('mouseup'," + this.ClientID + "ObjMouseUp,false);");
				output.WriteLine("document.addEventListener('mousemove'," + this.ClientID + "ObjMouseMove,false);");
				output.WriteLine("}");
			}
			output.WriteLine("--></script>");
		}

	}
}
