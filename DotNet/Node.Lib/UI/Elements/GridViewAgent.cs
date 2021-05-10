using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.Utility;
using Node.Lib.UI.WebUtils;
using System.Globalization;

namespace Node.Lib.UI.Elements
{
    /// <summary>
    /// EAFGridViewProvider
    /// </summary>
    [Serializable]
    public class GridViewAgent
    {
        //***********************************************************************
        //  private members
        //***********************************************************************
		private string rootNodeName = "gridViewMapping";
        //private string xmlFile = "";
        private Hashtable gridViewViews = new Hashtable();
		//private XmlNode xmlRootNode = null;
		//private XmlTree tree = null;
		private XmlNode xmlRootNode;
		private XmlTree tree;

        //***********************************************************************
        //  constructor
        //***********************************************************************

        /// <summary>
		/// Initializes a new instance of the GridViewAgent class
        /// </summary>
		/// <param name="file">GridViewAgent xml file.</param>
        public GridViewAgent(string file)
        {
            //this.xmlFile = file;
			this.xmlRootNode = XmlUtility.GetRequiredXmlNode(file, this.rootNodeName);
        }

		/// <summary>
		/// Initializes a new instance of the GridViewAgent class
		/// </summary>
		/// <param name="xdoc">XmlDocument object.</param>
		public GridViewAgent(XmlDocument xdoc)
		{
			this.xmlRootNode = XmlUtility.GetRequiredXmlNode(xdoc, this.rootNodeName);
		}

		/// <summary>
		/// Initializes a new instance of the GridViewAgent class
		/// </summary>
		/// <param name="root">XmlNode of GridViewAgent Xml object.</param>
		public GridViewAgent(XmlNode root)
		{
			this.xmlRootNode = XmlUtility.GetRequiredXmlNode(root, this.rootNodeName);
		}

        /// <summary>
        /// list of GridViewView
        /// </summary>
        public Hashtable GridViewViews
        {
            get { return this.gridViewViews; }
        }

        /// <summary>
        /// Get GridViewView object.
        /// </summary>
        /// <param name="viewID">View ID defined in XML file.</param>
        /// <returns>GridViewView object</returns>
        public GridViewView GetGridViewView(string viewID)
        {
            return (GridViewView)this.GridViewViews[viewID];
        }

        /// <summary>
        /// invoke control createion of the GridViewView.
        /// </summary>
        /// <param name="viewID">View ID</param>
		/// <param name="userControl">Control where contatin delegate event method.</param>
        public void CreateControls(string viewID, Control userControl)
        {
            GridViewView f = (GridViewView)this.GridViewViews[viewID];
            if (f != null) f.CreateFields(userControl);
        }

        /// <summary>
        /// Initialize the GridViewView
        /// </summary>
        /// <param name="viewID"> View ID defined in XML file</param>
        /// <returns>GridViewView object</returns>
        public GridViewView InitGridView(string viewID)
        {
			this.tree = new XmlTree(this.xmlRootNode);

			GridViewView gvView = null;
			if (this.tree != null)
			{
				XmlTreeNode node = this.tree.FindNode("ID", viewID, "view");

				if (node != null)
				{
					string vid = "" + node.GetAttribute("ID");
					if ((viewID == null || viewID == "") || (viewID != null && viewID != "" && viewID == vid))
					{
						gvView = new GridViewView(vid);
						this.GridViewViews[vid] = gvView;

						for (int k = 0; k < node.ChildNodes.Count; k++)
						{
							XmlTreeNode n1 = (XmlTreeNode)node.ChildNodes[k];
							if ("" + n1.NodeName.ToLower(CultureInfo.CurrentCulture) == "actionfields")
							{
								for (int m = 0; m < n1.ChildNodes.Count; m++)
								{
									XmlTreeNode n2 = (XmlTreeNode)n1.ChildNodes[m];
									gvView.GridViewActionFields.Add(CreateGridViewField(n2));
								}
							}

							if ("" + n1.NodeName.ToLower(CultureInfo.CurrentCulture) == "datafields")
							{
								for (int m = 0; m < n1.ChildNodes.Count; m++)
								{
									XmlTreeNode n3 = (XmlTreeNode)n1.ChildNodes[m];
									gvView.GridViewDataFields.Add(CreateGridViewField(n3));
								}
							}
						}
					}
				}
			}
            return gvView;
        }

        /// <summary>
        /// Initialize all gridviewview with the XML file.
        /// </summary>
        public void InitAllGridView()
        {
			this.tree = new XmlTree(this.xmlRootNode);

			if (this.tree != null)
			{
				for (int i = 0; i < this.tree.RootNode.ChildNodes.Count; i++)
				{
					XmlTreeNode node = (XmlTreeNode)this.tree.RootNode.ChildNodes[i];

					if ("" + node.NodeName.ToLower(CultureInfo.CurrentCulture) == "form")
					{
						string vid = "" + node.GetAttribute("ID");
						GridViewView gvView = new GridViewView(vid);
						this.GridViewViews.Add(vid, gvView);

						for (int k = 0; k < node.ChildNodes.Count; k++)
						{
							XmlTreeNode n1 = (XmlTreeNode)node.ChildNodes[k];
							if ("" + n1.NodeName.ToLower(CultureInfo.CurrentCulture) == "actionFields")
							{
								for (int m = 0; m < n1.ChildNodes.Count; m++)
								{
									XmlTreeNode n2 = (XmlTreeNode)n1.ChildNodes[m];
									gvView.GridViewActionFields.Add(CreateGridViewField(n2));
								}
							}

							if ("" + n1.NodeName.ToLower(CultureInfo.CurrentCulture) == "dataFields")
							{
								for (int m = 0; m < n1.ChildNodes.Count; m++)
								{
									XmlTreeNode n3 = (XmlTreeNode)n1.ChildNodes[m];
									gvView.GridViewDataFields.Add(CreateGridViewField(n3));
								}
							}
						}
					}
				}
			}
        }

		//------------------------------------------------------------------------------------------

		private GridViewField CreateGridViewField(XmlTreeNode n)
        {
            GridViewField gvFieldCtrl = new GridViewField(this);

            string[] ss = n.NodeName.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (ss[0].ToLower(CultureInfo.CurrentCulture) == "asp")
                gvFieldCtrl.AsmType = GridViewField.AssemblyType.System;
            else if (ss[0].ToLower(CultureInfo.CurrentCulture) == "eaf")
                gvFieldCtrl.AsmType = GridViewField.AssemblyType.EAF;

            gvFieldCtrl.FieldType = ss[1].Trim();

            ICollection props = n.Attributes.Keys;
            foreach (string prop in props)
                gvFieldCtrl.FieldAttributes[prop] = n.GetAttribute(prop);

            return gvFieldCtrl;
        }
    }
}
