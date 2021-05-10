using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Event arguments used for Ajax related controls
	/// </summary>
	public class AjaxContentEventArgs : EventArgs
	{
		/// <summary>
		/// Passed in parameter
		/// </summary>
		public string Parameter = "";

		/// <summary>
		/// HtmlTextWriter object
		/// </summary>
		public HtmlTextWriter Output = null;
	}
}
