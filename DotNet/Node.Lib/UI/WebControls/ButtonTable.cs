using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	public enum ButtonTableType
	{
		Frame, SepLine
	}

	//***********************************************************
	// Control Class
	//***********************************************************
	/// <summary>
	/// Button Table Layout, use with LeftButtons and RightButtons.
	/// </summary>
	public class ButtonTable : PlaceHolder
	{
		private string tableWidth = "";
		private ButtonTableType tableType = ButtonTableType.Frame;
		

		/// <summary>
		/// CSS Style width of table, can be px or %.
		/// </summary>
		public string TableWidth
		{
			get { return tableWidth; }
			set { tableWidth = value; }
		}

		/// <summary>
		/// width of section block, can be px or %.
		/// </summary>
		public ButtonTableType TableType
		{
			get { return tableType; }
			set { tableType = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			switch (this.tableType)
			{
				case ButtonTableType.SepLine:
					RenderSimpleTable(output);
					break;
				default:
					RenderFramedTable(output);
					break;
			}
		}

		private void RenderFramedTable(HtmlTextWriter output)
		{
			output.WriteLine("<table class=\"eaf_BtnBlock\" cellspacing=\"0\" ");
			if(this.tableWidth!="") output.WriteLine(" style=\"width:" + this.TableWidth + "\" ");
			output.WriteLine(">");
			output.WriteLine("<tr>");
			output.WriteLine("<td class=\"L\" >&nbsp;</td>");

			for (int i = 0; i < this.Controls.Count; i++)
			{
				Control c = this.Controls[i];
				if (c is LeftButtons)
				{
					output.WriteLine("<td class=\"btnL\" >");
					c.RenderControl(output);
					output.WriteLine("</td>");
				}
				else if (c is RightButtons)
				{
					output.WriteLine("<td class=\"btnR\" >");
					c.RenderControl(output);
					output.WriteLine("</td>");
				}
			}
			output.WriteLine("<td class=\"R\" >&nbsp;</td>");
			output.WriteLine("</tr></table>");
		}

		private void RenderSimpleTable(HtmlTextWriter output)
		{
			output.WriteLine("<table class=\"eaf_BtnBlock2\" cellspacing=\"0\" ");
			if (this.tableWidth != "") output.WriteLine(" style=\"width:" + this.TableWidth + "\" ");
			output.WriteLine(">");
			output.WriteLine("<tr>");


			for (int i = 0; i < this.Controls.Count; i++)
			{
				Control c = this.Controls[i];
				if (c is LeftButtons)
				{
					output.WriteLine("<td class=\"btnL\" >");
					c.RenderControl(output);
					output.WriteLine("</td>");
				}
				else if (c is RightButtons)
				{
					output.WriteLine("<td class=\"btnR\" >");
					c.RenderControl(output);
					output.WriteLine("</td>");
				}
			}
			output.WriteLine("</tr></table>");
		}

	}
}
