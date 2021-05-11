using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Create extra KeyField of existing BoundField.
	/// </summary>
	[Serializable]
	public class EAFBoundField: BoundField
	{
		private string keyField = "";
		private string id = "";

		/// <summary>
		/// Get or set key field name.
		/// </summary>
		public string KeyField
		{
			get { return this.keyField; }
			set { this.keyField = value; }
		}

		/// <summary>
		/// BoundField ID
		/// </summary>
		public string ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		/// <summary>
		/// Please refer to <see cref="System.Web.UI.WebControls.BoundField"/>.
		/// Note: Remember to call base.OnDataBindField() when you inherit the control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnDataBindField(object sender, EventArgs e)
		{
			AddKeyAttribute(sender, e);
			base.OnDataBindField(sender, e);
		}

		protected void AddKeyAttribute(object sender, EventArgs e)
		{
			if (this.keyField != "")
			{
				WebControl control = sender as WebControl;
				GridViewRow grv = control.NamingContainer as GridViewRow;
				DataRowView drv = grv.DataItem as DataRowView;
				string value = "" + drv[this.keyField];
				control.Attributes.Add("KeyFieldVal", value);
			}
		}
	}
}
