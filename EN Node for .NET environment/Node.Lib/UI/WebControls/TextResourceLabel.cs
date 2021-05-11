using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using Node.Lib.UI.Base;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Use this class only when you implement TextResource File.
	/// </summary>
	public class TextResourceLabel : Label
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		private string fullKey = "";
		private string pageKey = "";
		private string globalKey = "";
		//private bool isInheritedPageBase = true;

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Get or set global key for retrieving global section value.
		/// </summary>
		public string GlobalKey
		{
			get { return this.globalKey; }
			set { this.globalKey = value; }
		}

		/// <summary>
		/// Get or set full key for retrieving value.
		/// </summary>
		public string FullKey
		{
			get { return this.fullKey; }
			set { this.fullKey = value; }
		}

		/// <summary>
		/// If your page inherits PageBase, you can use only key of page level. This page key will be concatenated with PageID to form a full key.
		/// For Exmple: Your full key is something like Pages.Folder.MyPage.aspx.MyLabel, the PageID is Pages.Folder.MyPage.aspx, and PageKey is MyLabel.
		/// </summary>
		public string PageKey
		{
			get { return this.pageKey; }
			set { this.pageKey = value; }
		}

		/*
		public bool IsInheritedPageBase
		{
			get { return this.isInheritedPageBase; }
			set { this.isInheritedPageBase = value; }
		}
		*/

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			if (this.FullKey != null && this.FullKey != "")
			{
				this.Text = TextResource.GetValue(this.FullKey);
			}
			else if (this.PageKey != null && this.PageKey != "")
			{
				if (!(this.Page is PageBase))
					this.Text = "(ERROR! You have to inherit EAF.Lib.UI.Base.PageBase, or use PageKey property.)";
				else
				{
					PageBase pgBase = (PageBase)this.Page;

					try
					{
						this.Text = TextResource.GetValue(pgBase.TextResourcePageKey, this.PageKey);
					}
					catch (Exception e)
					{
						this.Text = "(ERROR ==> " + e.Message + ")";
					}
				}
			}
			else if (this.GlobalKey != null && this.GlobalKey != "")
			{
				this.Text = TextResource.GetGlobalValue(this.GlobalKey);
			}

			base.Render(output);
		}
	}
}
