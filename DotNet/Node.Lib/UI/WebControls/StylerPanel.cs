using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Define enumerator of StylerPanel style.
	/// </summary>
	public enum StylerPanelType
	{
		/// <summary>
		/// Light-grey style
		/// </summary>
		Smoky, 
		/// <summary>
		/// Yellow round rectangle
		/// </summary>
		YellowBubble
	}

	/// <summary>
	/// Difine enumerator of StylerPanel alignment (only when your width of panel is not 100%).
	/// </summary>
	public enum StylerPanelAlign
	{
		/// <summary>
		/// To left
		/// </summary>
		Left,
		/// <summary>
		/// To center
		/// </summary>
		Center,
		/// <summary>
		/// To right
		/// </summary>
		Right
	}

	/// <summary>
	/// Display a styled HTML panel.
	/// </summary>
	public class StylerPanel : PlaceHolder
	{
		//***********************************************************************
		// Constants
		//***********************************************************************

		/// <summary>
		/// Constant for defining style: Smoky
		/// </summary>
		public const string STYLE_SMOKY = "smoky";
		/// <summary>
		/// Constant for defining style: YellowBubble
		/// </summary>
		public const string STYLE_YELLOW_BUBBLE = "yellowbubble";

		/// <summary>
		/// Constant for defining left alignment.
		/// </summary>
		public const string ALIGN_LEFT = "left";
		/// <summary>
		/// Constant for defining center alignment.
		/// </summary>
		public const string ALIGN_CENTER = "center";
		/// <summary>
		/// Constant for defining right alignment.
		/// </summary>
		public const string ALIGN_RIGHT = "right";

		//***********************************************************************
		// Private members
		//***********************************************************************
		private string sessIDPrefix = "EAF.Lib.UI.WebControls.StylerPanel.";

		private string styleWidth = "100%";
		private StylerPanelType panelStyle = StylerPanelType.Smoky;
		private string panelAlign = "";
		private int panelPadding = 0;

		private bool allowCollapsed = false;
		private bool initExpanded = true;
		private string collapsedTitle = "Title";
		private string collapsedTitleClass = "";

		private string hidFldName
		{
			get { return this.ClientID + "hidFldName"; }
		}
		private string hidValue
		{
			get { return (this.Page.Session[sessIDPrefix + this.UniqueID] == null) ? null : "" + this.Page.Session[sessIDPrefix + this.UniqueID]; }
			set { this.Page.Session[sessIDPrefix + this.UniqueID] = value; }
		}
		private string ImageBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Get or set panel style.
		/// </summary>
		public StylerPanelType PanelStyle
		{
			get { return panelStyle; }
			set	{ panelStyle = value; }
		}

		/// <summary>
		/// Get or set panel width, use pixel or percentage. Default is 100%.
		/// </summary>
		public string StyleWidth
		{
			get { return styleWidth; }
			set	{ styleWidth = value; }
		}
		
		/// <summary>
		/// Get or set alignment of the panel.
		/// </summary>
		public string PanelAlign
		{
			get	{ return panelAlign; }
			set	{ panelAlign = value; }
		}

		/// <summary>
		/// Get or set padding of the panel. Default is 5px.
		/// </summary>
		public int PanelPadding
		{
			get { return panelPadding; }
			set { panelPadding = value; }
		}

		/// <summary>
		/// Collapsed Title when allowing panel collapsed.
		/// </summary>
		public string CollapsedTitle
		{
			get { return collapsedTitle; }
			set { collapsedTitle = value; }
		}

		/// <summary>
		/// Collapsed Title class name.
		/// </summary>
		public string CollapsedTitleClass
		{
			get { return collapsedTitleClass; }
			set { collapsedTitleClass = value; }
		}

		/// <summary>
		/// Allow panel collapsed
		/// </summary>
		public bool AllowCollapsed
		{
			get{ return allowCollapsed; }
			set { allowCollapsed = value; }
		}

		/// <summary>
		/// Inital expanded value when allowing panel collapsed.
		/// </summary>
		public bool InitialExpanded
		{
			get { return initExpanded; }
			set { initExpanded = value; }
		}
		//***********************************************************************
		// Control events
		//***********************************************************************
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (this.allowCollapsed)
			{
				if (this.Page.IsPostBack)
				{
					string val = "" + this.Page.Request[this.hidFldName];
					this.hidValue = (val == "") ? "block" : val;
				}
				else
				{
					if (this.hidValue == null)
					{
						if (!this.initExpanded)
							this.hidValue = "none";
						else
							this.hidValue = "block";
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			output.Write(CapHtml());
			base.Render(output);
			output.Write(TailHtml());
		}

		//***********************************************************************
		// Private methods
		//***********************************************************************

		private string CapHtml()
		{
			StringBuilder s = new StringBuilder();

			if (this.allowCollapsed)
			{
				s.AppendLine("<input type=\"hidden\" id=\"" + this.hidFldName + "\" name=\"" + this.hidFldName + "\" value=\"" + this.hidValue + "\" />");
			}

			s.Append("<table class=\"" + GetStyleCss() + "\" cellspacing=\"0\" style=\"width:" + this.StyleWidth + "; " + GetPanelAlignCss() + "\">");
			s.Append("<tr>");
			s.Append("<td class=\"TL\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"TBG\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"TR\"><div>&nbsp;</div></td>");
			s.Append("</tr>");
			s.Append("<tr>");
			s.Append("<td class=\"L\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"Cnt\">");
			s.Append("<div class=\"Cnt\" style=\"padding:" + this.PanelPadding + "px\">");

			if (this.AllowCollapsed)
			{
				if (this.CollapsedTitleClass == "")
				{
					s.Append("<div style=\"padding:3px 0; font-size:75%;\">");
				}
				else
				{
					s.Append("<div class=\"" + this.CollapsedTitleClass + "\">");
				}

				s.Append("<a href=\"javascript:void(0);\" onclick=\"Utils.toggleDivImgTxt('" + this.ClientID + "SPCnt','" + this.ClientID + "SPImg','" + this.hidFldName + "','" + ImageBase + "node_exp_sqr.gif','" + ImageBase + "node_col_sqr.gif')\">");
				if (this.hidValue == "none")
					s.Append("<img style=\"border:0;vertical-align:middle;\" id=\"" + this.ClientID + "SPImg\" name=\"" + this.ClientID + "SPImg\" src=\"" + ImageBase + "node_col_sqr.gif\" />");
				else
					s.Append("<img style=\"border:0;vertical-align:middle;\" id=\"" + this.ClientID + "SPImg\" name=\"" + this.ClientID + "SPImg\" src=\"" + ImageBase + "node_exp_sqr.gif\" />");
				s.Append("</a> ");
				s.Append(this.collapsedTitle);
				s.Append("</div>");

				s.Append("<div id=\"" + this.ClientID + "SPCnt\" id=\"" + this.ClientID + "SPCnt\" style=\"display:" + this.hidValue + "\">");
			}

			return s.ToString();
		}

		private string TailHtml()
		{
			StringBuilder s = new StringBuilder();

			if (this.allowCollapsed)
			{
				s.Append("</div>");
			}

			s.Append("</div></td>");
			s.Append("<td class=\"R\"><div>&nbsp;</div></td>");
			s.Append("</tr>");
			s.Append("<tr>");
			s.Append("<td class=\"BL\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"BBG\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"BR\"><div>&nbsp;</div></td>");
			s.Append("</tr>");
			s.Append("</table>");

			return s.ToString();
		}

		private string GetStyleCss()
		{
			if (this.panelStyle ==  StylerPanelType.YellowBubble)
				return "eaf_YB";
			else
				return "eaf_Smoky";
		}

		private string GetPanelAlignCss()
		{
			if (this.panelAlign.ToLower() == ALIGN_CENTER)
				return "margin-left:auto; margin-right:auto;";
			else if (this.panelAlign.ToLower() == ALIGN_RIGHT)
				return "margin-left:auto; margin-right:0;";
			else
				return "margin-left:0; margin-right:auto;";
		}

	}
}
