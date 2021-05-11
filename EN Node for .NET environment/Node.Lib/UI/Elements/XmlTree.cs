using System;
using System.Collections;
using System.Xml;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;
using Node.Lib.UI.WebControls;
using Node.Lib.UI.WebUtils;
using Node.Lib.Utility;

namespace Node.Lib.UI.Elements
{
	/// <summary>
	/// XmlTree
	/// </summary>
	[Serializable]
	public class XmlTree
	{
		//***********************************************************************
		// private members
		//***********************************************************************

		//private int seqCount = 1;		
		//private XmlTreeNode root = new XmlTreeNode(0);
		private XmlTreeNode root = new XmlTreeNode();
		private XmlDocument xmlDoc = new XmlDocument();
		private string valuePathKey = "";
		private string valuePathSplitter = ".";

		

		/*
		private int GetNextSeq
		{
			get { return this.seqCount++; }
		}
		*/

		//***********************************************************************
		// constructors
		//***********************************************************************

		/// <summary>
		/// constructor
		/// </summary>
		public XmlTree() 
		{}

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="xmlFile">Tree structure xml file.</param>
		public XmlTree(string xmlFile)
		{
			try
			{
				this.xmlDoc.Load(xmlFile);
			}
			catch (Exception e)
			{
				throw new Exception("EAF.Lib.UI.Elements.XmlTree ERROR: " + xmlFile + " -- " + e.Message);
			}
			BuildTree();
		}

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="xmlDoc">XmlDocument object of a tree structure.</param>
		public XmlTree(XmlDocument xmlDoc)
		{
			this.xmlDoc = xmlDoc;
			BuildTree();
		}

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="XmlNode">XmlNode object of a tree root node.</param>
		public XmlTree(XmlNode xmlNode)
		{
			BuildTree(xmlNode);
		}

		/// <summary>
		/// constructor
		/// </summary>
        /// <param name="xmlFile">Tree structure xml file.</param>
		/// <param name="valuePathKey">Attribute used to construct valuepath</param>
		/// <param name="valuePathSplitter">A splitting character used to construct valuepath</param>
        public XmlTree(string xmlFile, string valuePathKey, string valuePathSplitter)
		{
			this.xmlDoc.Load(xmlFile);
			this.valuePathKey = valuePathKey;
			this.valuePathSplitter = valuePathSplitter;
			BuildTree();
		}

		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// XmlDocument object
		/// </summary>
		public XmlDocument XmlDoc
		{
			get { return this.xmlDoc; }
			set { this.xmlDoc = value; }
		}

		/// <summary>
		/// Root node of the xml tree.
		/// </summary>
		public XmlTreeNode RootNode
		{
			get { return this.root; }
		}

		//***********************************************************************
		// public methods
		//***********************************************************************

		/// <summary>
		/// Create XmlTree from Web TreeView
		/// </summary>
		/// <param name="tv">TreeView object</param>
		/// <returns>XmlTree object</returns>
		public static XmlTree CreateFrom(TreeView tv)
		{
			XmlTree tree = new XmlTree();
			tree.RootNode.NodeName = "Nodes";

			for (int i = 0; i < tv.Nodes.Count; i++)
			{
				TreeNode tn = tv.Nodes[i];
				XmlTreeNode xn = new XmlTreeNode("TreeNode");
				tree.RootNode.AddChild(xn);
				AssignXmlTreeAttributes(tn, xn);

				RcrsvGenXmlTreeNode(tn, xn);
			}
			
			return tree;
		}

		/*
		/// <summary>
		/// Overloaded. Find node based on sequence # of the node.
		/// </summary>
		/// <param name="seq">sequence number of the node you want to find.</param>
		/// <returns>Return the node of first occurrence of a specific sequence found.</returns>
		public XmlTreeNode FindNode(int seq)
		{
			if(seq==0) 
				return this.RootNode;

			return RcrsvFindNode(this.RootNode.ChildNodes, seq);
		}
		*/

        /// <summary>
        /// Overloaded. Find node based on the valuePathKey property of the node. 
        /// It will return first one if it's multiple.
        /// </summary>
        /// <param name="valuePath">value path of the node</param>
        /// <returns>Return the node of first occurrence of a specific value path found.</returns>
        public XmlTreeNode FindNode(string valuePath)
        {
            if (this.RootNode.ValuePath == valuePath)
                return this.RootNode;

            return RcrsvFindNode(this.RootNode.ChildNodes, valuePath);
        }

		/*
		public XmlTreeNodeCollection FindNodesByNodePath(string nodePath)
		{
			XmlTreeNodeCollection xnc = new XmlTreeNodeCollection();

			if (this.RootNode.NodePath == nodePath)
			{
				xnc.Add(this.RootNode);
				return xnc;
			}

			RcrsvFindNodeByNodePath(this.RootNode.ChildNodes, nodePath, xnc);
			return xnc;
		}
		*/

        /// <summary>
		/// Overloaded. Find node based on value attributes of the node. 
		/// It will return first one if it's multiple.
		/// </summary>
		/// <param name="key">Attribute name of the node</param>
		/// <param name="val">Attribute value of the node</param>
		/// <returns>Return the node of first occurrence of a specific key/value found.</returns>
		public XmlTreeNode FindNode(string key, string val)
		{
			return FindNode(key, val, "");
		}

		/// <summary>
		/// Overloaded. Find node based on value attributes of the node. 
		/// It will return first one if it's multiple.
		/// </summary>
		/// <param name="key">Attribute name of the node</param>
		/// <param name="val">Attribute value of the node</param>
		/// <param name="nodeName">Name of the node</param>
		/// <returns>Return the node of first occurrence of a specific key/value found.</returns>
		public XmlTreeNode FindNode(string key, string val, string nodeName)
		{
			if (this.RootNode.GetAttribute(key) == val)
				return this.RootNode;

			return RcrsvFindNode(this.RootNode.ChildNodes, key, val, nodeName);
		}

		/// <summary>
		/// Save XmlTree to an Xml file.
		/// </summary>
		/// <param name="filepath">filepath</param>
		public void Save(string filepath)
		{
			this.xmlDoc = GenerateXmlDoc();
			this.xmlDoc.Save(filepath);
		}

		/// <summary>
		/// Generate XmlDocument based on the XmlTree object.
		/// </summary>
		/// <returns>XmlDocument object</returns>
		public XmlDocument GenerateXmlDoc()
		{
			XmlDocument xdoc = new XmlDocument();
			XmlDeclaration xmlDeclaration = this.xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
			xdoc.AppendChild(xdoc.ImportNode(xmlDeclaration,false));

			if (this.RootNode != null)
			{
				XmlElement root = xdoc.CreateElement(this.RootNode.NodeName);
				xdoc.AppendChild(root);
				AssignXmlDocAttributes(this.RootNode, root);

				RcrsvGenXmlDocNode(xdoc, this.RootNode, root);
			}
			return xdoc;
		}


		/// <summary>
		/// Populate XmlTree data to ASP.NET TreeView
		/// </summary>
		/// <param name="tv">TreeView reference object</param>
		public void PopulateWebTreeView(TreeView tv)
		{
			for (int i = 0; i < this.RootNode.ChildNodes.Count; i++)
			{
				XmlTreeNode xtn = this.RootNode.ChildNodes[i];
				TreeNode tn = new TreeNode(xtn.NodeName);
				tv.Nodes.Add(tn);
				AssignWebTreeAttributes(xtn, tn);

				RcrsvGenWebTreeView(xtn, tn);
			}
		}

		/// <summary>
		/// Populate XmlTree data to ASP.NET TreeView
		/// </summary>
		/// <param name="tv">TreeView reference object</param>
		public void PopulateWebTreeViewWithDataBinding(TreeView tv)
		{
			for (int i = 0; i < this.RootNode.ChildNodes.Count; i++)
			{
				XmlTreeNode xn = this.RootNode.ChildNodes[i];				
				//EAFTreeNode tn = new EAFTreeNode();
                TreeNode tn = new TreeNode();
				tv.Nodes.Add(tn);

				//tn.Tag = xn;
				TreeNodeBinding tnBind = GetTreeNodeBinding(tv.DataBindings, xn.NodeName);
				if (tnBind != null)
					AssignTreeNodeBindings(tn, xn, tnBind);
				else
					tn.Text = xn.NodeName;

				RcrsvGenWebTreeViewWithDataBinding(xn, tn, tv.DataBindings);
			}
		}

		/// <summary>
		/// This function generates XmlTree based on the provided xml file
		/// </summary>
        public void BuildTree()
		{
			if (this.XmlDoc != null)	BuildTree(this.XmlDoc.DocumentElement);
		}

		/// <summary>
		/// This function generates XmlTree based on the provided xml file
		/// </summary>
		public void BuildTree(XmlNode rootNode)
		{
			if (rootNode != null)
			{
				int lvlCount = 0;
				string valPath = "";

				this.RootNode.NodeName = rootNode.Name;
				this.RootNode.Level = lvlCount;
				XmlAttributeCollection atts = rootNode.Attributes;

				for (int i = 0; i < atts.Count; i++)
				{
					XmlAttribute att = atts[i];
					this.RootNode.SetAttribute(att.Name, att.Value);
				}
				//if (this.valuePathKey != "")
				//{
				//    valPath = this.RootNode.GetAttribute(this.valuePathKey);
				//    this.RootNode.ValuePath = valPath;
				//}

				lvlCount++;
				this.RootNode.NodePath = this.RootNode.NodeName;
				RcrsvBuildNode(this.RootNode, rootNode.ChildNodes, lvlCount, valPath, this.RootNode.NodeName);
			}
		}

		private void RcrsvBuildNode(XmlTreeNode parent, XmlNodeList nodeList, int lvlCount, string valPath, string nodePath)
		{
			if(nodeList!=null && nodeList.Count>0)
			{
				int oldNodeNameCount = 0;
				string oldNodeName = "";		

				for (int i=0; i<nodeList.Count; i++)
				{
					XmlNode xn = nodeList[i];
					if(xn.NodeType == XmlNodeType.Element)
					{
						//XmlTreeNode tn = new XmlTreeNode(this.GetNextSeq);
						XmlTreeNode tn = new XmlTreeNode(xn.Name);
						tn.Level = lvlCount;

						XmlAttributeCollection atts = xn.Attributes;
						for (int j=0; j<atts.Count; j++)
						{
							XmlAttribute att = atts[j];
							tn.SetAttribute(att.Name, att.Value);
						}

						string newValPath = valPath;
                        if (this.valuePathKey != "")
                        {
                            if (valPath == "")
                                newValPath += tn.GetAttribute(this.valuePathKey);
                            else
                                newValPath += this.valuePathSplitter + tn.GetAttribute(this.valuePathKey);
							
							tn.ValuePath = newValPath;
                        }

						//build node path
						string newNodePath = nodePath;

						if (tn.NodeName == oldNodeName)	
							oldNodeNameCount++;
						else 
							oldNodeNameCount = 0;

						if (nodePath == "")	
							newNodePath += tn.NodeName;
						else 
							newNodePath += this.valuePathSplitter + tn.NodeName;
						
						if (oldNodeNameCount > 0) 
							newNodePath += "(" + oldNodeNameCount + ")";

						tn.NodePath = newNodePath;
						oldNodeName = tn.NodeName;	

						lvlCount++;
						RcrsvBuildNode(tn, xn.ChildNodes, lvlCount, newValPath, newNodePath);
						lvlCount--;

						parent.AddChild(tn);
						tn.Parent = parent;
					}
					else if(xn.NodeType == XmlNodeType.Text)
					{
						parent.SetValues("Text", xn.Value);
					}
					//else if (xn.NodeType == XmlNodeType.Comment)
					//{
					//    parent.SetValues("Comment", xn.Value);
					//}
					else if (xn.NodeType == XmlNodeType.CDATA)
					{
						parent.SetValues("CDATA", xn.Value);
					}
				}
			}
		}

		private void RcrsvGenWebTreeView(XmlTreeNode xNode, TreeNode tNode)
		{
			if (xNode.ChildNodes != null && xNode.ChildNodes.Count > 0)
			{
				for (int i = 0; i < xNode.ChildNodes.Count; i++)
				{
					XmlTreeNode xtn = xNode.ChildNodes[i];
					TreeNode tn = new TreeNode(xtn.NodeName);
					tNode.ChildNodes.Add(tn);
					AssignWebTreeAttributes(xtn, tn);

					RcrsvGenWebTreeView(xtn,tn);
				}
			}
		}

		private void RcrsvGenWebTreeViewWithDataBinding(XmlTreeNode xNode, TreeNode tNode, TreeNodeBindingCollection tnbc)
		{
			if (xNode.ChildNodes != null && xNode.ChildNodes.Count > 0)
			{
				for (int i = 0; i < xNode.ChildNodes.Count; i++)
				{
					XmlTreeNode xn = xNode.ChildNodes[i];
					EAFTreeNode tn = new EAFTreeNode();
					tNode.ChildNodes.Add(tn);

					tn.Tag = xn;
					TreeNodeBinding tnBind = GetTreeNodeBinding(tnbc, xn.NodeName);
					if (tnBind != null)
						AssignTreeNodeBindings(tn, xn, tnBind);
					else
						tn.Text = xn.NodeName;

					RcrsvGenWebTreeViewWithDataBinding(xn, tn, tnbc);
				}
			}
		}

		/*
		private XmlTreeNode RcrsvFindNode(XmlTreeNodeCollection nodeList, int seq)
		{
			XmlTreeNode rtnNode = null;
			if(nodeList!=null && nodeList.Count>0)
			{
				for (int i=0; i<nodeList.Count; i++)
				{
					XmlTreeNode tn = nodeList[i];
					if(tn.Seq==seq)
					{
						return tn;
					}
					rtnNode = RcrsvFindNode(tn.ChildNodes,seq);

					if(rtnNode!=null) break;
				}
			}
			return rtnNode;
		}
		*/

		private XmlTreeNode RcrsvFindNode(XmlTreeNodeCollection nodeList, string key, string val, string nodeName)
		{
			XmlTreeNode rtnNode = null;
			if(nodeList!=null && nodeList.Count>0)
			{
				for (int i=0; i<nodeList.Count; i++)
				{
					XmlTreeNode tn = nodeList[i];
					if(tn.GetAttribute(key)==val)
					{
						if (nodeName == null || nodeName == "" || nodeName == tn.NodeName)
							return tn;
					}
					rtnNode = RcrsvFindNode(tn.ChildNodes,key,val,nodeName);

					if(rtnNode!=null) break;
				}
			}
			return rtnNode;
		}

        /// <summary>
        /// Recursively search for an XmlTreeNode based on the specified valuepath
        /// </summary>
        /// <param name="nodeList">Collection of XMLTreeNode</param>
        /// <param name="valuePath">valuePathKey of an XMLTreeNode</param>
        /// <returns>Return the node of first occurrence of a specific value path found.</returns>
        private XmlTreeNode RcrsvFindNode(XmlTreeNodeCollection nodeList, string valuePath)
        {
            XmlTreeNode rtnNode = null;
            if (nodeList != null && nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlTreeNode tn = nodeList[i];
                    if (tn.ValuePath == valuePath)
                    {
                        return tn;
                    }
                    rtnNode = RcrsvFindNode(tn.ChildNodes, valuePath);

                    if (rtnNode != null) break;
                }
            }
            return rtnNode;
        }

		private void RcrsvFindNodeByNodePath(XmlTreeNodeCollection nodeList, string nodePath, XmlTreeNodeCollection xnc)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				for (int i = 0; i < nodeList.Count; i++)
				{
					XmlTreeNode tn = nodeList[i];
					if (tn.NodePath == nodePath)
					{
						xnc.Add(tn);
					}
					RcrsvFindNodeByNodePath(tn.ChildNodes, nodePath, xnc);
				}
			}
		}

		private void RcrsvGenXmlDocNode(XmlDocument xDoc, XmlTreeNode xTreeNode, XmlElement xNode)
		{
			if (xTreeNode.ChildNodes != null)
			{
				for (int i = 0; i < xTreeNode.ChildNodes.Count; i++)
				{
					XmlTreeNode xtn = xTreeNode.ChildNodes[i];
					XmlElement xn = xDoc.CreateElement(xtn.NodeName);
					xNode.AppendChild(xn);

					AssignXmlDocAttributes(xtn, xn);

					RcrsvGenXmlDocNode(xDoc, xtn, xn);
				}
			}
		}

		private static void RcrsvGenXmlTreeNode(TreeNode tNode, XmlTreeNode xNode)
		{
			if (tNode.ChildNodes != null)
			{
				for (int i = 0; i < tNode.ChildNodes.Count; i++)
				{
					TreeNode tn = tNode.ChildNodes[i];

					XmlTreeNode xn = new XmlTreeNode("TreeNode");
					xNode.AddChild(xn);
					AssignXmlTreeAttributes(tn, xn);

					RcrsvGenXmlTreeNode(tn, xn);
				}
			}
		}

		private void AssignXmlDocAttributes(XmlTreeNode xtn, XmlElement xn)
		{
			ICollection ic = xtn.Attributes.Keys;

			foreach (string key in ic)
				xn.SetAttribute(key, xtn.GetAttribute(key));
		}


		private static void AssignXmlTreeAttributes(TreeNode tn, XmlTreeNode xtn)
		{
			PropertyInfo[] piList = tn.GetType().GetProperties();

			for (int i = 0; i < piList.Length; i++)
			{
				PropertyInfo pi = piList[i];
				Type pt = pi.PropertyType;
				if (pt == typeof(string) || pt == typeof(bool) || pt == typeof(int))
				{
					object o = pi.GetValue(tn, null);
					if (("" + o) != "")
						xtn.SetAttribute(pi.Name, "" + o);
				}
			}
		}

		private static void AssignWebTreeAttributes(XmlTreeNode xtn, TreeNode tn)
		{
			ICollection ic = xtn.Attributes.Keys;
			Type type = tn.GetType();

			foreach (string key in ic)
				ReflectionHelper.SetPropertyValue(tn, key, xtn.GetAttribute(key));
		}

		private TreeNodeBinding GetTreeNodeBinding(TreeNodeBindingCollection tnbc, string dataMember)
		{
			for (int i = 0; i < tnbc.Count; i++)
			{
				if (tnbc[i].DataMember == dataMember)
					return tnbc[i];
			}
			return null;
		}

		private void AssignTreeNodeBindings(TreeNode tn, XmlTreeNode xn, TreeNodeBinding tnBind)
		{
			// old way
			//----------------
			//tn.Text = "" + xn.GetAttribute(tnBind.TextField);
			//if (tn.Text == "") tn.Text = tnBind.Text;

			//tn.Value = "" + xn.GetAttribute(tnBind.ValueField);
			//if (tn.Value == "") tn.Value = tnBind.Value;

			// reflection way
			//------------------------
			PropertyInfo[] piList = tn.GetType().GetProperties();
			Type nodeBindingType = tnBind.GetType();

			for (int i = 0; i < piList.Length; i++)
			{
				PropertyInfo pi = piList[i];
				PropertyInfo mappingPI = nodeBindingType.GetProperty(pi.Name + "Field");
				PropertyInfo bindingPI = nodeBindingType.GetProperty(pi.Name);
				object val = null;

				if (bindingPI != null)
				{
					val = bindingPI.GetValue(tnBind, null);
					if (val != null && ("" + val) != "") 
						ReflectionHelper.SetPropertyValue(tn, pi.Name, val);
				}
				
				if (mappingPI != null)
				{
					string mapFld = "" + mappingPI.GetValue(tnBind, null);
					val = xn.GetAttribute(mapFld);
					if (val != null && ("" + val) != "") 
						ReflectionHelper.SetPropertyValue(tn, pi.Name, val);
				}
			}
		}

	}
}
