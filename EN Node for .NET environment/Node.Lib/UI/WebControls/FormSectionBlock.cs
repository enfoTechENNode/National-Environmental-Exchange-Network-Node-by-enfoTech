using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	public enum FormSectionBlockType
	{
		Frame, TitleLine
	}

	/// <summary>
	/// Provides a beautified form block to sub-categorize your Web forms
	/// </summary>
	public class FormSectionBlock : PlaceHolder
	{
		private string caption = "Section Title";
		private string secWidth = "";
		private string secCntCss = "";
		private FormSectionBlockType secType = FormSectionBlockType.Frame;

		//---------------------------------------------------------------------------------------------------
		/// <summary>
		/// Title caption of the secion block
		/// </summary>
		public string Caption
		{
			get { return caption; }
			set { caption = value; }
		}

		/// <summary>
		/// width of section block, can be px or %.
		/// </summary>
		public string SectionWidth
		{
			get { return secWidth; }
			set { secWidth = value; }
		}

		/// <summary>
		/// Extra CSS for section content such text style
		/// </summary>
		public string SectionContentCss
		{
			get { return secCntCss; }
			set { secCntCss = value; }
		}

		/// <summary>
		/// width of section block, can be px or %.
		/// </summary>
		public FormSectionBlockType SectionType
		{
			get { return secType; }
			set { secType = value; }
		}

		//---------------------------------------------------------------------------------------------------
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

		//---------------------------------------------------------------------------------------------------
		
		private string CapHtml()
		{
			StringBuilder s = new StringBuilder();

			string type = "";
			switch (this.secType)
			{
				case FormSectionBlockType.TitleLine:
					type = "eaf_FormSecTab2";
					break;
				default:
					type = "eaf_FormSecTab1";
					break;
			}

			s.Append("<table cellspacing=\"0\" class=\""+type+"\" ");
			if (this.SectionWidth != "") s.Append(" style=\"width:" + this.secWidth + "\" ");
			s.Append(">");
			s.Append("<tr><td class=\"eaf_ttl\">" + this.caption + "</td></tr>");
			s.Append("<tr><td class=\"eaf_cnt " + this.SectionContentCss + "\">");

			return s.ToString();
		}

		private string TailHtml()
		{
			StringBuilder s = new StringBuilder();

			s.Append("</td></tr></table>");

			return s.ToString();
		}
	}
}
