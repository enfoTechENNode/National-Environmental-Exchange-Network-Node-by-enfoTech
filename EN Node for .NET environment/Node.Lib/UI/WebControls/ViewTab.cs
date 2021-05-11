using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// To use with <see cref="EAF.Lib.UI.WebControls.MultiViewTabs"/> class.
	/// </summary>
	public class ViewTab : View
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		private string caption="TabItem";
		private string desc = "";
		private string imgSrc = "";

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// [Stateless] Get or set caprtion of the tab.
		/// </summary>
		public string Caption
		{
			get { return caption; }
			set	{ caption = value; }
		}

		/// <summary>
		/// [Stateless] Get or set description of the tab.
		/// </summary>
		public string Description
		{
			get { return desc; }
			set { desc = value; }
		}

		/// <summary>
		/// [Stateless] Get or set image icon of the tab.
		/// </summary>
		public string ImageSrc
		{
			get { return imgSrc; }
			set { imgSrc = value; }
		}
		
		/// <summary>
		/// [Stateful] Ture if you want to show this tab, otherwise false. Default is true.
		/// 
		/// Note: Even if the ShowTab set to false, the ViewTab will be still kept in ViewState, 
		/// and the tab index remains unchanged. You can still show it use ActiveViewIndex properties of its parent control.
		/// </summary>
		public bool ShowTab
		{
			get
			{
				object o = ViewState["ShowTab"];
				return (o == null) ? true : (bool)o;
			}
			set
			{
				ViewState["ShowTab"] = value;
			}
		}
	}
}
