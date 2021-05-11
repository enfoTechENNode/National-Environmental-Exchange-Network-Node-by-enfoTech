using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.UI.WebUtils;
using Node.Lib.UI.Base;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Web form field base class.
	/// This is only a container for beautified webform.
	/// </summary>
	public abstract class FormFieldBase : PlaceHolder
	{
		private string _fieldName = "";
		private string _fieldKey = "";
		private string _fieldPageKey = "";
		private string _fieldGlobalKey = "";

		/// <summary>
		/// Get or set field name.
		/// </summary>
		public string FieldName
		{
			get { return _fieldName; }
			set { _fieldName = value; }
		}

		/// <summary>
		/// Get or set the key of field name which you define in TextResource XML file. Pass in Full Key.
		/// Use this property only when you've already implemented TextResource.
		/// </summary>
		public string FieldKey
		{
			get { return _fieldKey; }
			set { _fieldKey = value; }
		}

		/// <summary>
		/// Get or set the key of field name which you define in TextResource XML file.  Pass in Page (Partial) Key.
		/// Use this property only when you've already implemented TextResource.
		/// </summary>
		public string FieldPageKey
		{
			get { return _fieldPageKey; }
			set { _fieldPageKey = value; }
		}

		/// <summary>
		/// Get or set the key of field name which you define in TextResource XML file.  Pass in Page (Partial) Key.
		/// Use this property only when you've already implemented TextResource.
		/// </summary>
		public string FieldGlobalKey
		{
			get { return _fieldGlobalKey; }
			set { _fieldGlobalKey = value; }
		}

		/// <summary>
		/// Get value of field name. FieldValue will be equal to FieldName if FieldName is set already.
		/// Use this property only when you've already implemented TextResource.
		/// </summary>
		public string FieldValue
		{
			get
			{
				if (_fieldName != "")
					return _fieldName;
				else
					if(_fieldKey!="")
						return TextResource.GetValue(_fieldKey);
					else if(_fieldPageKey!="")
					{
						string txt = "";
						if (!(this.Page is PageBase))
							txt = "(ERROR! You have to inherit EAF.Lib.UI.Base.PageBase, or use PageKey property.)";
						else
						{
							PageBase pgBase = (PageBase)this.Page;
							try
							{
								txt = TextResource.GetValue(pgBase.TextResourcePageKey, _fieldPageKey);
							}
							catch (Exception e)
							{
								txt = "(ERROR ==> " + e.Message + ")";
							}
						}
						return txt;
					}
					else if (_fieldGlobalKey != "")
					{
						return TextResource.GetGlobalValue(_fieldGlobalKey);
					}

				return "";

			}
		}
	}
}
