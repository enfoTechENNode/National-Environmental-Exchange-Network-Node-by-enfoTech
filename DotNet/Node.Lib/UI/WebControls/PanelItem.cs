using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
    public class PanelItem : Control
    {
        //================================
		//  Members
		//================================
		private string link="#";
		private string caption="TabItem";
		private string desc = "";
		private string target = "";
		private string imgSrc = "";
	
		[Bindable(true)]
		[Category("eafWC")]
		[DefaultValue("")]
		public string Link
		{
			get { return link; }
			set	{ link = value; }
		}

		[Bindable(true)]
		[Category("eafWC")]
		[DefaultValue("")]
		public string Caption
		{
			get { return caption; }
			set	{ caption = value; }
		}

		[Bindable(true)]
		[Category("eafWC")]
		[DefaultValue("")]
		public string Description
		{
			get { return desc; }
			set { desc = value; }
		}
		
		[Bindable(true)]
		[Category("eafWC")]
		[DefaultValue("")]
		public string Target
		{
			get { return target; }
			set { target = value; }
		}

		[Bindable(true)]
		[Category("eafWC")]
		[DefaultValue("")]
		public string ImageSrc
		{
			get { return imgSrc; }
			set { imgSrc = value; }
		}
	}
}
