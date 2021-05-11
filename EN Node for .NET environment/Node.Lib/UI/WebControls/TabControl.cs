using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	//***********************************************************
	// Control Builder Class
	//***********************************************************
	public class TabControlBuilder : ControlBuilder
	{
		public override Type GetChildControlType(string tabName, IDictionary attributes)
		{
			if (tabName.ToLower().EndsWith("tabitem"))
			{
				return typeof(TabItem);
			}
			return null;
		}

		public override void AppendLiteralString(string s)
		{
			// do nothing to ignore literial strings
		}

	}

	//***********************************************************
	// Control Class
	//***********************************************************
	
	[ControlBuilderAttribute(typeof(TabControlBuilder)), ParseChildren(false)]

	public class TabControl : WebControl
	{
		public const string ALIGN_LEFT = "left";
		public const string ALIGN_RIGHT = "right";
		public const string ALIGN_CENTER = "center";

		public const string TYPE_HORIZONTAL = "horizontal";
		public const string TYPE_VERTICAL = "vertical";
		
		//==============================
		// Members
		//==============================
		private TabItemCollection items = new TabItemCollection();
		private int selectedIndex = -1;
		private string tabAlign = ALIGN_CENTER;
		private string tabType = TYPE_HORIZONTAL;
		private bool onTabLink = true;

		//==============================
		//  Attributes
		//==============================
		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { selectedIndex = value; }
		}

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

		public bool OnTabLink
		{
			get { return onTabLink; }
			set { onTabLink = value; }
		}

		
		public TabItemCollection Items
		{
			get { return items; }
		}

		public void AddItem(TabItem obj)
		{
			if (obj != null)
				items.Add(obj);
		}

		public void RemoveItem(int i)
		{
			if (i >= 0 && i < items.Count)
				items.RemoveAt(i);
		}

		//========================
		// Methods
		//========================
		protected override void AddParsedSubObject(Object obj)
		{
			if (obj is TabItem)
			{
				items.Add((TabItem)obj);
			}
		}

		protected override void Render(HtmlTextWriter output)
		{
			// draw nothing if there is not items
			if (this.items.Count < 0) return;

			// default selected index will be 0
			if (this.selectedIndex >= this.items.Count) this.selectedIndex = -1;

			if(this.tabType.ToLower()==TYPE_VERTICAL)
				output.Write(GetVerticalTab());
			else
				output.Write(GetFloatTab());
		}

		//========================
		// private methods
		//========================
		private string GetMacTabs()
		{
			StringBuilder s = new StringBuilder();

			s.Append("<table class=\"eaf_NewTab ");
			
			if (this.tabAlign.ToLower() == ALIGN_CENTER) 
				s.Append("toCenter");
			else if (this.tabAlign.ToLower() == ALIGN_RIGHT)
				s.Append("toRight");
			else
				s.Append("toLeft");

			s.Append("\" cellspacing=\"0\">");
            s.Append("<tr>");

			for(int i=0; i<this.items.Count; i++)
			{
				TabItem tab = items[i];

				if(tab.Visible)
				{
					if(i==this.selectedIndex) 
					{					
						// on tab
						if (i==0 || !HasPreviousVisibleTab(i))
							s.Append("<td class=\"headOn\"><div>&nbsp;</div></td>");

						s.Append("<td class=\"on\" nowrap>");
						
						if(this.onTabLink)	s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append(tab.Caption);
						if (this.onTabLink)	s.Append("</a>");
						
						s.Append("</td>");

						if (i == this.items.Count - 1 || !HasNextVisibleTab(i))
							s.Append("<td class=\"tailOn\"><div>&nbsp;</div></td>");
						else
							s.Append("<td class=\"sep\"><div>&nbsp;</div></td>");
					}
					else
					{
						// off tab
						if (i == 0 || !HasPreviousVisibleTab(i))
							s.Append("<td class=\"headOff\"><div>&nbsp;</div></td>");

						s.Append("<td class=\"off\" nowrap>");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">" + tab.Caption + "</a>");
						s.Append("</td>");

						if (i == this.items.Count - 1 || !HasNextVisibleTab(i))
							s.Append("<td class=\"tailOff\"><div>&nbsp;</div></td>");
						else
							s.Append("<td class=\"sep\"><div>&nbsp;</div></td>");
					}
				}
			}
            s.Append("</tr>");
			s.Append("</table>");

			return s.ToString();
		}




		private string GetVerticalTab()
		{
			StringBuilder s = new StringBuilder();

			s.Append("<table cellspacing=\"0\" class=\"eaf_SideTab\">");

			for(int i=0; i<this.items.Count; i++)
			{
				TabItem tab = items[i];

				if(tab.Visible)
				{
					if(i==this.selectedIndex) 
					{
						s.Append("<tr><td class=\"headOn\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"on\" nowrap>");
						
						if (this.onTabLink) s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append(tab.Caption);
						if (this.onTabLink) s.Append("</a>");

						s.Append("</td></tr>");
					}
					else
					{
						s.Append("<tr><td class=\"headOff\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"off\" nowrap>");

						s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append(tab.Caption);
						s.Append("</a>");

						s.Append("</td></tr>");

					}
				}
			}
			
			s.Append("</table>");
			return s.ToString();
		}


		private string GetFloatTab()
		{
			StringBuilder s = new StringBuilder();

			s.Append("<table class=\"eaf_FloatTab ");

			if (this.tabAlign.ToLower() == ALIGN_CENTER)
				s.Append("toCenter");
			else if (this.tabAlign.ToLower() == ALIGN_RIGHT)
				s.Append("toRight");
			else
				s.Append("toLeft");

			s.Append("\" cellspacing=\"0\">");

			s.Append("<tr>");
			s.Append("<td class=\"head\"><div>&nbsp;</div></td>");
			s.Append("<td class=\"sep\"><div>&nbsp;</div></td>");

			for (int i = 0; i < this.items.Count; i++)
			{
				TabItem tab = items[i];

				if (tab.Visible)
				{
					if (i == this.selectedIndex)
					{
						s.Append("<td class=\"onLeft\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"on\" nowrap>");

						if (this.onTabLink) s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append(tab.Caption);
						if (this.onTabLink) s.Append("</a>");

						s.Append("</td>");
						s.Append("<td class=\"onRight\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"sep\"><div>&nbsp;</div></td>");
					}
					else
					{
						s.Append("<td class=\"offLeft\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"off\" nowrap>");
						
						s.Append("<a href=\"" + ResolveUrl(tab.Link) + "\" title=\"" + tab.Description + "\">");
						if (tab.ImageSrc != "") s.Append("<img src=\"" + ResolveUrl(tab.ImageSrc) + "\">");
						s.Append(tab.Caption);
						s.Append("</a>");
						
						s.Append("</td>");
						s.Append("<td class=\"offRight\"><div>&nbsp;</div></td>");
						s.Append("<td class=\"sep\"><div>&nbsp;</div></td>");
					}
				}
			}
			
			s.Append("<td class=\"tail\"><div>&nbsp;</div></td>");
			s.Append("</tr></table>");
			return s.ToString();
		}


		private bool HasPreviousVisibleTab(int current)
		{
			for(int i=current-1; i>=0; i--)
			{
				TabItem tab = items[i];
				if(tab.Visible) return true;
			}
			return false;
		}

		private bool HasNextVisibleTab(int current)
		{
			for (int i = current; i < this.items.Count; i++)
			{
				TabItem tab = items[i];
				if (tab.Visible) return true;
			}
			return false;
		}

	}
}
