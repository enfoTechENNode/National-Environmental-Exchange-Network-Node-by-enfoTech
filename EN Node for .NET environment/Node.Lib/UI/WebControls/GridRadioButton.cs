using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// GroupHtmlRadioButton control is a standard radio-button with the extended 
	/// abilities to be used in groups.
	/// </summary>
	/// <remarks>
	/// Standard <see cref="System.Web.UI.HtmlControls.HtmlInputRadioButton"/> controls 
	/// cannot be grouped when are placed at the different rows of the DataGrid, 
	/// DataList, Repeater, etc. controls. 
	/// 
	/// The "name" attribute of the radio button HTML control that is rendered 
	/// at the web form after RadioButton control has been executed is depend 
	/// on the UniqueID of the RadioButton. So for the different rows of the 
	/// DataGrid/DataList/Repeater these attributes are different and radio 
	/// buttons do not belong to the same group.
	/// </remarks>
	public class GridRadioButton : RadioButton, IPostBackDataHandler
	{
		/// <summary>
		/// Used by <see cref="EAF.Lib.UI.WebControls.GridRadioButtonField" />
		/// </summary>
		public string Data
		{
			get	{ return ""+ViewState["data"]; }
			set { ViewState["data"] = value; }
		}

        public string Value
		{
			get
			{
				string val = Attributes["value"];
				if (val == null)
					val = UniqueID;
				else
					val = UniqueID + "_" + val;
				return val;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.GroupName);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Value);

			if (this.Checked)
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			if (!this.Enabled)
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");

			//string onClick = Attributes["onclick"];
			//if (AutoPostBack)
			//{
			//    if (onClick != null)
			//        onClick = String.Empty;
			//    onClick += this.Page.ClientScript.GetPostBackEventReference(this, String.Empty);
			//    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			//    writer.AddAttribute("language", "javascript");
			//}
			//else
			//{
			//    if (onClick != null)
			//        writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			//}

			//if (AccessKey.Length > 0)
			//    writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, AccessKey);
			//if (TabIndex != 0)
			//    writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, TabIndex.ToString(NumberFormatInfo.InvariantInfo));

			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}
		
		/// <summary>
		/// 
		/// </summary>
		public new void RaisePostDataChangedEvent()
		{
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="postDataKey"></param>
		/// <param name="postCollection"></param>
		/// <returns></returns>
		public new bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = false;
			string value = postCollection[this.GroupName];
			if ((value != null) && (value == this.Value))
			{
				if (!Checked)
				{
					Checked = true;
					result = true;
				}
			}
			else
			{
				if (Checked)
					Checked = false;
			}
			return result;
		}

	}
}
