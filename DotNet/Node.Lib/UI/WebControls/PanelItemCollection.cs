using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Node.Lib.UI.WebControls
{
    public class PanelItemCollection : ICollection, IEnumerable
    {
        private ArrayList ItemAry;

        public PanelItemCollection()
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

        public PanelItem this[int index]
        {
            get
            {
                return (PanelItem)this.ItemAry[index];
            }
        }

        public void Add(PanelItem item)
        {
            this.ItemAry.Add(item);
        }

        public void AddAt(int index, PanelItem child)
        {

        }

        public void Clear()
        {
            this.ItemAry.Clear();
        }

        public bool Contains(PanelItem c)
        {
            bool bFlag = false;
            foreach (PanelItem aItem in this)
                if (aItem.ID == c.ID)
                    return true;
            return bFlag;
        }

        public void CopyTo(Array array, int index)
        {

        }

        public void CopyTo(PanelItem[] array, int index)
        {
            this.CopyTo((Array)array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.ItemAry.GetEnumerator();
        }

        public int IndexOf(PanelItem c)
        {
            int i = -1;
            foreach (PanelItem aItem in this)
            {
                i++;
                if (aItem.ID == c.ID)
                    return i;
            }
            return i = -1;
        }

        public void Remove(PanelItem item)
        {
            this.ItemAry.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this.ItemAry.RemoveAt(index);
        }
    }
}
