using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Node.Lib.UI.Elements
{
	/// <summary>
	/// XmlTreeNodeCollection
	/// </summary>
	[Serializable]
	public class XmlTreeNodeCollection : ICollection, IEnumerable
	{
		//***********************************************************************
		// Private methods
		//***********************************************************************

		private ArrayList ItemAry;

		//***********************************************************************
		// Constructors
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the XmlTreeNodeCollection class.
		/// </summary>
		public XmlTreeNodeCollection()
		{
			this.ItemAry = new ArrayList();
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/// <summary>
		/// Gets the number of elements contained in the XmlTreeNodeCollection.
		/// </summary>
		public int Count
		{
			get { return this.ItemAry.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether access to the XmlTreeNodeCollection is synchronized (thread safe).
		/// </summary>
		public bool IsSynchronized
		{
			get { return this.ItemAry.IsSynchronized; }
		}

		/// <summary>
		/// Gets an object that can be used to synchronize access to the XmlTreeNodeCollection.
		/// </summary>
		public object SyncRoot
		{
			get { return this.ItemAry.SyncRoot; }
		}

		/// <summary>
		/// Get XmlTreeNode object by index
		/// </summary>
		/// <param name="index">Index of the colletion.</param>
		/// <returns></returns>
		public XmlTreeNode this[int index]
		{
			get { return (XmlTreeNode)this.ItemAry[index]; }
		}

		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// Adds an object to the end of the XmlTreeNodeCollection. 
		/// </summary>
		/// <param name="item">The Object to be added to the end of the XmlTreeNodeCollection. The value can be null.</param>
		public void Add(XmlTreeNode item)
		{
			this.ItemAry.Add(item);
		}

		/// <summary>
		/// Removes all elements from the XmlTreeNodeCollection. 
		/// </summary>
		public void Clear()
		{
			this.ItemAry.Clear();
		}


		/// <summary>
		/// Determines whether an element is in the XmlTreeNodeCollection. 
		/// </summary>
		/// <param name="node">The Object to locate in the XmlTreeNodeCollection. The value can be null. </param>
		/// <returns>true if item is found in the Collection; otherwise, false. </returns>
		public bool Contains(XmlTreeNode node)
		{
			bool bFlag = false;
			foreach (XmlTreeNode aItem in this)
				if (aItem.Equals(node))
					return true;
			return bFlag;
		}


		/// <summary>
		/// Copies the entire XmlTreeNodeCollection to a compatible one-dimensional Array, starting at the specified index of the target array. 
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from ArrayList. The Array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(Array array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>
		/// Copies the entire ArrayList to a compatible one-dimensional Array, starting at the specified index of the target array. 
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from ArrayList. The Array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		public void CopyTo(XmlTreeNode[] array, int index)
		{
			this.CopyTo((Array)array, index);
		}

		/// <summary>
		/// Returns an enumerator for the entire XmlTreeNodeCollection. 
		/// </summary>
		/// <returns>An IEnumerator for the entire XmlTreeNodeCollection. </returns>
		public IEnumerator GetEnumerator()
		{
			return this.ItemAry.GetEnumerator();
		}

		/// <summary>
		/// Searches for the specified Object and returns the zero-based index of the first occurrence within the entire XmlTreeNodeCollection. 
		/// </summary>
		/// <param name="node">The Object to locate in the ArrayList. The value can be null. </param>
		/// <returns>The zero-based index of the first occurrence of value within the entire ArrayList, if found; otherwise, -1.</returns>
		public int IndexOf(XmlTreeNode node)
		{
			int i = -1;
			foreach (XmlTreeNode aItem in this)
			{
				i++;
				if (aItem.Equals(node))
					return i;
			}
			return i = -1;
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the XmlTreeNodeCollection.
		/// </summary>
		/// <param name="item">The Object to remove from the XmlTreeNodeCollection. The value can be null.</param>
		public void Remove(XmlTreeNode item)
		{
			this.ItemAry.Remove(item);
		}

		/// <summary>
		/// Removes the element at the specified index of the XmlTreeNodeCollection. 
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		public void RemoveAt(int index)
		{
			this.ItemAry.RemoveAt(index);
		}

	}
}
