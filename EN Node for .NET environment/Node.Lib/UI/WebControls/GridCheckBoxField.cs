using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// CheckBox control used in GridView only.
	/// Note: this is different from ASP.NET <see cref="System.Web.UI.WebControls.CheckBoxField"/>
	/// </summary>
	public class GridCheckBoxField : EAFBoundField
	{
		/// <summary>
		/// Please refer to <see cref="System.Web.UI.WebControls.BoundField"/>
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="rowState"></param>
		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			HtmlInputCheckBox c = new HtmlInputCheckBox();
			c.ID = this.ID;
			c.DataBinding += new EventHandler(this.OnDataBindField);
			cell.Controls.Add(c);
		}

		/// <summary>
		/// Please refer to <see cref="System.Web.UI.WebControls.BoundField"/>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			Control container = control.NamingContainer;
			object value = this.GetValue(container);

			((HtmlInputCheckBox)control).Value = "" + value;

			this.AddKeyAttribute(sender, e);
		}
	}
}
