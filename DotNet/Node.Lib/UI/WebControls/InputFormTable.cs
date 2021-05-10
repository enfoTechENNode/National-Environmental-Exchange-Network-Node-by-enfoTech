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
	/// An HTML table struct helps you format the layout of a entry form.
	/// Use with the following controls: 
	/// <see cref="EAF.Lib.UI.WebControls.FormInputField"/>,
	/// <see cref="EAF.Lib.UI.WebControls.FormDisplayField"/>,
	/// <see cref="EAF.Lib.UI.WebControls.FormDescField"/>,
	/// <see cref="EAF.Lib.UI.WebControls.FormSepLineField"/>.	
	/// </summary>
	public class InputFormTable : PlaceHolder
	{
		private string fieldNameWidth = "";
		private string fieldNameCss = "";

		/// <summary>
		/// CSS style width of field name. Default is "".
		/// Note this property will override what you define in FieldNameCss.
		/// </summary>
		public string FieldNameWidth
		{
			get { return this.fieldNameWidth; }
			set { this.fieldNameWidth = value; }
		}

		/// <summary>
		/// CSS class width of field name.
		/// </summary>
		public string FieldNameCss
		{
			get { return this.fieldNameCss; }
			set { this.fieldNameCss = value; }
		}

		/// <summary>
		/// Render controls.
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			output.WriteLine("<table class=\"eaf_InputForm\" cellspacing=\"0\">");

			for(int i=0; i<this.Controls.Count; i++)
			{
				Control c = this.Controls[i];
				
				if (c.Visible == true)
				{
					output.WriteLine("<tr>");

					if (c is FormInputField)
					{
						FormInputField fld = (FormInputField)c;

						int w;
						if (int.TryParse(this.fieldNameWidth, out w))
						{
							this.fieldNameWidth += "px";
						}

						output.Write("<td ");
						if (this.fieldNameWidth != "") output.Write(" style=\"width:" + this.fieldNameWidth + "\" ");
						output.WriteLine("class=\"fld " + this.fieldNameCss + " " + (fld.ErrorOn ? "bgOn" : "") + "\">" + fld.FieldValue + "</td>");

						if (fld.ShowRequired)
							output.WriteLine("<td class=\"req " + (fld.ErrorOn ? "bgOn" : "") + "\"><div>&nbsp;</div></td>");
						else
							output.WriteLine("<td class=\"noreq " + (fld.ErrorOn ? "bgOn" : "") + "\"><div>&nbsp;</div></td>");

						output.Write("<td class=\"inp" + (fld.ErrorOn ? " bgOn" : "") + "\">");
						if (fld.WithText) output.Write("<span class=\"fld\">");
						fld.RenderControl(output);
						if (fld.WithText) output.Write("</span>");
						output.WriteLine("</td>");
					}
					else if (c is FormDisplayField)
					{
						FormDisplayField fld = (FormDisplayField)c;

						output.WriteLine("<td style=\"width:" + this.fieldNameWidth + "\" class=\"fld\">" + fld.FieldValue + "</td>");
						output.WriteLine("<td>&nbsp;</td>");
						output.Write("<td class=\"val\">");
						fld.RenderControl(output);
						output.WriteLine("</td>");
					}
					else if (c is FormDescField)
					{
						output.WriteLine("<td style=\"width:" + this.fieldNameWidth + "\">&nbsp;</td>");
						output.WriteLine("<td>&nbsp;</td>");
						output.Write("<td class=\"cmt\">");
						c.RenderControl(output);
						output.WriteLine("</td>");
					}
					else if (c is FormSepLineField)
					{
						output.Write("<td colspan=\"3\" class=\"sep\">");
						output.Write("<div>&nbsp;</div>");
						output.WriteLine("</td>");
					}

					output.WriteLine("</tr>");
				}
			}

			output.WriteLine("</table>");
		}

	}
}
