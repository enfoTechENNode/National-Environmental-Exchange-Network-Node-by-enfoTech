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
	/// To create vertical colum layout. (Use Table HTML element underneath.)
	/// </summary>
	public class MultiColumnPanel : PlaceHolder
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		private string panelCss = "";
		private string allColumnCss = "";
		private string leftColumnCss = "";
		private string rightColumnCss = "";

		//***********************************************************************
		// Attributes
		//***********************************************************************
		/// <summary>
		/// Get or set CSS of outside panel. (Apply to Table element.)
		/// </summary>
		public string PanelCss
		{
			get { return this.panelCss; }
			set { this.panelCss = value; }
		}

		/// <summary>
		/// Get or set CSS of every column. (Apply to TD element);
		/// </summary>
		public string AllColumnCss
		{
			get { return this.allColumnCss; }
			set { this.allColumnCss = value; }
		}

		/// <summary>
		/// Get or set CSS of most left column. (Apply to TD element);
		/// </summary>
		public string LeftColumnCss
		{
			get { return this.leftColumnCss; }
			set { this.leftColumnCss = value; }
		}

		/// <summary>
		/// Get or set CSS of most right column. (Apply to TD element);
		/// </summary>
		public string RightColumnCss
		{
			get { return this.rightColumnCss; }
			set { this.rightColumnCss = value; }
		}

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.Controls.Count > 0)
			{
				output.WriteLine("<table cellspacing=\"0\" class=\"" + this.PanelCss + "\"><tr>");

				for (int i = 0; i < this.Controls.Count; i++)
				{
					Control c = this.Controls[i];
					if (c is Panel)
					{
						if (i == 0)
							output.WriteLine("<td class=\"" + this.AllColumnCss + " " + this.LeftColumnCss + "\">");
						else if (i == this.Controls.Count - 1)
							output.WriteLine("<td class=\"" + this.AllColumnCss + " " + this.RightColumnCss + "\">");
						else
							output.WriteLine("<td class=\"" + this.AllColumnCss + "\">");

						c.RenderControl(output);
						output.WriteLine("</td>");
					}
				}

				output.WriteLine("</tr></table>");
			}
		}

	}
}
