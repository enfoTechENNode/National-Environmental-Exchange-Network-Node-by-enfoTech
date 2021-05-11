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
	/// PlacceHolder for your WebControls of entry form.
	/// </summary>
	public class FormInputField : FormFieldBase
	{
		private bool showRequired = false;
		private bool withText = false;
		private bool errorOn = false;

		/// <summary>
		/// Set true if you want to show a required asteroid icon.
		/// Default is false.
		/// </summary>
		public bool ShowRequired
		{
			get { return this.showRequired; }
			set { this.showRequired = value; }
		}

		/// <summary>
		/// Set ture if you want to display extra text along with the webcontrols.
		/// Default is false.
		/// </summary>
		public bool WithText
		{
			get { return this.withText; }
			set { this.withText = value; }
		}
		
		/// <summary>
		/// Set true if you want to highlight the field to express error.
		/// Default is false.
		/// </summary>
		public bool ErrorOn
		{
			get { return this.errorOn; }
			set { this.errorOn = value; }
		}
	}
}
