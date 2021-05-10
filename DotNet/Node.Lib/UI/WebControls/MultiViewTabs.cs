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
	/// Use this class with <see cref="EAF.Lib.UI.WebControls.ViewTab"/> class.
	/// </summary>
	public class MultiViewTabs : MultiView, IPostBackEventHandler
	{
		//***********************************************************************
		// Public constants
		//***********************************************************************

		/*
		public const string ALIGN_LEFT = "left";
		public const string ALIGN_RIGHT = "right";
		public const string ALIGN_CENTER = "center";
		public const string TYPE_HORIZONTAL = "horizontal";
		*/
		/// <summary>
		/// Constant for defining showing vertical tabs.
		/// </summary>
		public const string TYPE_VERTICAL = "vertical";

		//***********************************************************************
		// Private members
		//***********************************************************************

		//private string tabAlign = ALIGN_CENTER;
		private string tabType = TYPE_VERTICAL;

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/*
	
		public string TabAlignment
		{
			get { return tabAlign; }
			set { tabAlign = value; }
		}

		
		public string TabType
		{
			get { return tabType; }
			set { tabType = value; }
		}
		*/


		//***********************************************************************
		// Delegate event functions
		//***********************************************************************

		/// <summary>
		/// Event TabClick Handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="tabIdx"></param>
		public delegate void TabClickHandler(object sender, int tabIdx);

		/// <summary>
		/// Occurs when the control is clicked on the tab.
		/// </summary>
		public event TabClickHandler TabClick;

		/// <summary>
		/// Raise the TabClick event.
		/// </summary>
		/// <param name="tabIdx"></param>
		protected virtual void OnTabClick(int tabIdx)
		{
			if (TabClick != null) TabClick(this, tabIdx);
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="eventArgument"></param>
		public void RaisePostBackEvent(String eventArgument)
		{
			//this.selectedTab = int.Parse(eventArgument);			
			this.ActiveViewIndex = int.Parse(eventArgument);
			OnTabClick(this.ActiveViewIndex);
		}

		//***********************************************************************
		// Control events
		//***********************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{

			if (this.Views.Count <= 0) return;

			if (this.tabType.ToLower() == TYPE_VERTICAL)
			{
				output.WriteLine("<table class=\"eaf_STTable\" cellspacing=\"0\"><tr>");

				output.WriteLine("<td class=\"eaf_STTab\">");
				output.WriteLine(GetVerticalTab());
				output.WriteLine("</td>");

				output.WriteLine("<td class=\"eaf_STCnt\">");				
				base.Render(output);
				output.WriteLine("</td>");

				output.WriteLine("</tr></table>");				
			}			

		}

		//***********************************************************************
		// Private methods
		//***********************************************************************

		private string GetVerticalTab()
		{
			StringBuilder s = new StringBuilder();
			AdjustSelectedTab();

			s.Append("<table cellspacing=\"0\" class=\"eaf_SideTab\">");

			for(int i=0; i<this.Views.Count; i++)
			{
				View v = this.Views[i];

				if (v is ViewTab)
				{
					ViewTab tab = (ViewTab)v;

					if (tab.ShowTab)
					{

						if (i == this.ActiveViewIndex)
						{
							s.Append("<tr><td class=\"headOn\"><div>&nbsp;</div></td>");
                            s.Append("<td class=\"on\" nowrap=\"nowrap\" >");

							if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
							s.Append(tab.Caption);

							s.Append("</td></tr>");
						}
						else
						{
							s.Append("<tr><td class=\"headOff\"><div>&nbsp;</div></td>");
							s.Append("<td class=\"off\" nowrap=\"nowrap\" >");

							s.Append("<a href=\"javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "" + i) + "\" title=\"" + tab.Description + "\">");
							if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
							s.Append(tab.Caption);
							s.Append("</a>");

							s.Append("</td></tr>");
						}
					}
				}
			}
			
			s.Append("</table>");
			return s.ToString();
		}

		
		private void AdjustSelectedTab()
		{
			for (int i = this.Views.Count-1; i >= 0; i--)
			{
				View v = this.Views[i];
				if (!(v is ViewTab))
					this.Views.RemoveAt(i);
			}

			if (this.ActiveViewIndex < 0 || 
				this.ActiveViewIndex >= this.Views.Count)
				this.ActiveViewIndex = 0;


			if (!((ViewTab)this.Views[this.ActiveViewIndex]).ShowTab)
			{
				this.ActiveViewIndex = FindViewableTab();
			}
		}

		private int FindViewableTab()
		{
			// find next one
			for (int i = this.ActiveViewIndex + 1; i < this.Views.Count; i++)
			{
				if (((ViewTab)this.Views[i]).ShowTab)
					return i;
			}

			// find previous one
			for (int i = this.ActiveViewIndex - 1; i >= 0; i--)
			{
				if (((ViewTab)this.Views[i]).ShowTab)
					return i;
			}

			return -1;
		}
	}
}
