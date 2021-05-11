using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Extending ASP.NET TreeNode
	/// </summary>
	public class EAFTreeNode : TreeNode
	{
		private string postBackNavUrl = "";
		private object tag;

		/// <summary>
		/// Make orginal NavigateUrl as protected to prevent from user access.
		/// will be use internally by EAFTreeView
		/// </summary>
		protected new string NavigateUrl
		{
			get { return this.NavigateUrl; }
			set { this.NavigateUrl = value; }
		}


		/// <summary>
		/// Get or set related object, must be Serializable
		/// </summary>
		public object Tag
		{
			get { return this.tag; }
			set { this.tag = value; }
		}

		/// <summary>
		/// Replacement of NavigateUrl of ASP.NET TreeView
		/// </summary>
		public string PostBackNavigateUrl
		{
			get { return this.postBackNavUrl; }
			set { this.postBackNavUrl = value; }
		}

	}
}
