using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
	public class TabItem : Control
	{

		//================================
		//  Members
		//================================
		private string link="#";
		private string caption="TabItem";
		private string desc = "";
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
		public string ImageSrc
		{
			get { return imgSrc; }
			set { imgSrc = value; }
		}
	}


	public class TabItemCollection : ICollection, IEnumerable
	{
		private ArrayList ItemAry;

		public TabItemCollection()
		{
			this.ItemAry = new ArrayList();
		}

		public int Count
		{
			get { return this.ItemAry.Count; }
		}

		public bool IsSynchronized
		{
			get { return this.ItemAry.IsSynchronized; }
		}

		public object SyncRoot
		{
			get { return this.ItemAry.SyncRoot; }
		}

		public TabItem this[int index]
		{
			get { return (TabItem)this.ItemAry[index]; }
		}

		public void Add(TabItem item)
		{
			this.ItemAry.Add(item);
		}

		public void Clear()
		{
			this.ItemAry.Clear();
		}

		public bool Contains(TabItem c)
		{
			bool bFlag = false;
			foreach (TabItem aItem in this)
				if (aItem.ID == c.ID)
					return true;
			return bFlag;
		}

		public void CopyTo(Array array, int index)
		{
			this.ItemAry.CopyTo(array, index);
		}

		public void CopyTo(TabItem[] array, int index)
		{
			this.CopyTo((Array)array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return this.ItemAry.GetEnumerator();
		}

		public int IndexOf(TabItem c)
		{
			int i = -1;
			foreach (TabItem aItem in this)
			{
				i++;
				if (aItem.ID == c.ID)
					return i;
			}
			return i = -1;
		}

		public void Remove(TabItem item)
		{
			this.ItemAry.Remove(item);
		}

		public void RemoveAt(int index)
		{
			this.ItemAry.RemoveAt(index);
		}

	}
}
