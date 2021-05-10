using System;
using System.Collections;
using System.Text;

namespace Node.Lib.UI.Elements
{
	//#######################################################################################
	// class XmlTreeNode
	//#######################################################################################

	/// <summary>
	/// XmlTreeNode
	/// </summary>
	[Serializable]
	public class XmlTreeNode
	{
		//***********************************************************************
		// Private members
		//***********************************************************************

		//private int seq = -1;
		/*
		private bool expand = false;
		private int imgExpandIdx = -1;
		private int imgCollapseIdx = -1;
		*/
		private string nodeName = "";		
		private XmlTreeNode parent = null;
		private XmlTreeNodeCollection childNodes = new XmlTreeNodeCollection();
		private Hashtable attrs = new Hashtable();
		private Hashtable vals = new Hashtable();
		private int level = -1;
		private string valuePath = "";
		private string nodePath = "";

		//***********************************************************************
		// Constructors
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the XmlTreeNode class.
		/// </summary>
		public XmlTreeNode()
		{}

		/*
		/// <summary>
		/// Initializes a new instance of the XmlTreeNode class.
		/// </summary>
		/// <param name="seq">Sequence number (id) of this node</param>
		public XmlTreeNode(int seq)
		{
			this.Seq = seq;
		}
		*/
		
		/*
		public XmlTreeNode(int seq, int exp, int clp)
		{
			this.Seq = seq;
			this.ImgExpandIdx = exp;
			this.ImgCollapseIdx = clp;
		}
		*/
		
		/// <summary>
		/// Initializes a new instance of the XmlTreeNode class.
		/// </summary>
		/// <param name="nodeName">NodeName</param>
		public XmlTreeNode(string nodeName)
		{
			this.nodeName = nodeName;
		}
		
		
		/*
		public XmlTreeNode(int seq, int exp, int clp)
		{
			this.Seq = seq;
			this.ImgExpandIdx = exp;
			this.ImgCollapseIdx = clp;
		}

		//***********************************************************************
		// Attrubites
		//***********************************************************************

		/*
		/// <summary>
		/// Sequence number, supposed to be unique of each node, like ID.
		/// </summary>
		public int Seq
		{
			get { return this.seq; }
			set { this.seq = value; }
		}
		*/

		/// <summary>
		/// Level of the TreeNode.
		/// </summary>
		public int Level
		{
			get { return this.level; }
			set { this.level = value; }
		}

		/// <summary>
		/// ValuePath can be used to uniquely identify an XmlTreeNode from an XmlTree
		/// </summary>
        public string ValuePath
		{
			get { return this.valuePath; }
			set { this.valuePath = value; }
		}

		/// <summary>
		/// NodePath can be used to uniquely identify an XmlTreeNode from an XmlTree
		/// </summary>
		public string NodePath
		{
			get { return this.nodePath; }
			set { this.nodePath = value; }
		}

		/*
		public bool Expand
		{
			get { return this.expand; }
			set { this.expand = value; }
		}

		public int ImgExpandIdx
		{
			get { return this.imgExpandIdx; }
			set { this.imgExpandIdx = value; }
		}

		public int ImgCollapseIdx
		{
			get { return this.imgCollapseIdx; }
			set { this.imgCollapseIdx = value; }
		}
		*/

		/// <summary>
		/// Node name.
		/// </summary>
		public string NodeName
		{
			get { return this.nodeName; }
			set { this.nodeName = value; }
		}

		/// <summary>
		/// Parent node object. Default is null.
		/// </summary>
		public XmlTreeNode Parent
		{
			get { return this.parent; }
			set { this.parent = value; }
		}

		/// <summary>
		/// Child nodes.
		/// </summary>
		public XmlTreeNodeCollection ChildNodes
		{
			get { return this.childNodes; }
			set { this.ChildNodes = value; }
		}
		
		/// <summary>
		/// Attributes (key/value format) of the node, read from xml.
		/// </summary>
		public Hashtable Attributes
		{
			get { return this.attrs; }
			set { this.attrs = value; }
		}

		/// <summary>
		/// Attributes (key/value format) of the node, read from xml.
		/// </summary>
		public Hashtable Values
		{
			get { return this.vals; }
			set { this.vals = value; }
		}

		/// <summary>
		/// Check if this node is a leaf node.
		/// </summary>
		public bool IsLeaf
		{
			get
			{
				if (childNodes == null || childNodes.Count == 0)
					return true;
				else
					return false;
			}
		}

		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// Get value based on attribute name.
		/// </summary>
		/// <param name="key">Attribute name</param>
		/// <returns>Attribute value, null if not found.</returns>
		public string GetAttribute(string key)
		{
			Object o = this.attrs[key];
			if(o!=null)
					return (String)o;
			else
					return null;
		}

		/// <summary>
		/// Set value of a attribute. If the attribute name is not exist, it will be created.
		/// </summary>
		/// <param name="key">Attribute name</param>
		/// <param name="val">Attribute value</param>
		public void SetAttribute(string key, string val)
		{
			this.attrs[key] = val;
		}

		/// <summary>
		/// Set value of a attribute. If the attribute name is not exist, it will be created.
		/// </summary>
		/// <param name="key">Attribute name</param>
		/// <param name="val">Attribute value</param>
		public void SetValues(string key, string val)
		{
			this.vals[key] = val;
		}

		/// <summary>
		/// Get value based on attribute name.
		/// </summary>
		/// <param name="key">Attribute name</param>
		/// <returns>Attribute value, null if not found.</returns>
		public string GetValues(string key)
		{
			Object o = this.vals[key];
			if (o != null)
				return (String)o;
			else
				return null;
		}


		/*
		public Object GetObject(string key)
		{
			return this.attrs[key];
		}
		public void SetObject(string key, Object obj)
		{
			this.attrs.Add(key,obj);
		}
		*/

		/// <summary>
		/// Add a child node to current node. 
		/// The child's parent node will be the current node.
		/// </summary>
		/// <param name="node">XmlTreeNode</param>
		public void AddChild(XmlTreeNode node)
		{
			if (childNodes == null)
				childNodes = new XmlTreeNodeCollection();

			childNodes.Add(node);
			node.Parent = this;
		}

		/// <summary>
		/// Remove a child node
		/// </summary>
		/// <param name="node">XmlTreeNode</param>
		public void RemoveChild(XmlTreeNode node)
		{
			if (childNodes != null && childNodes.Count > 0)
			{
				childNodes.Remove(node);
			}
		}

		/// <summary>
		/// Remove itself from the XmlTree.
		/// Void if this node is a root node of the tree (no parent).
		/// </summary>
		public void Remove()
		{
			if (this.Parent != null)
			{
				this.Parent.RemoveChild(this);
			}
		}

		/// <summary>
		/// deep clone itself.
		/// </summary>
		/// <returns>A deep cloned XmlTreeNode.</returns>
		public XmlTreeNode Clone()
		{
			return RcsvClone(this);
		}

		//***********************************************************************
		// private methods
		//***********************************************************************

		private XmlTreeNode RcsvClone(XmlTreeNode node)
		{
			XmlTreeNode clone = new XmlTreeNode();
			clone.NodeName = node.NodeName;
			// since all value in Hasktable is string, Clone method will do.
			clone.Attributes = (Hashtable)node.Attributes.Clone();
			clone.Values = (Hashtable)node.Values.Clone();

			foreach(XmlTreeNode cn in node.childNodes)
			{
				clone.ChildNodes.Add(RcsvClone(cn));
			}

			return clone;
		}

	}


}
