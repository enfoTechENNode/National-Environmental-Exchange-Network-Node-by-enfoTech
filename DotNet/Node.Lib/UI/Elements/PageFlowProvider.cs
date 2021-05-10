using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using Node.Lib.Utility;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.Elements
{
	//#######################################################################################
	// class Action
	//#######################################################################################

	/// <summary>
	/// Action class
	/// </summary>	
	[Serializable]
	public class Action
	{
		//***********************************************************************
		//  private members
		//***********************************************************************

		private string name = "";
		private string forwardPageID = "";
		private string parm = "";
		private bool redirect = true;

		//***********************************************************************
		// constructor
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the Action class.
		/// </summary>
		public Action()	
		{}

		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Action name</param>
		/// <param name="frdPgID">forwarded page ID</param>
		/// <param name="parm">parameters string (querystring)</param>
		/// <param name="redir">true if you want to use Response.Redirect(), otherwise use Server.Transfer()</param>	
		public Action(string name, string frdPgID, string parm, bool redir)
		{
			this.name = name;
			this.forwardPageID = frdPgID;
			this.parm = parm;
			this.redirect = redir;
		}
		
		/// <summary>
		/// Action name
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// forwarded page ID
		/// </summary>
		public string ForwardPageID
		{
			get { return forwardPageID; }
			set { forwardPageID = value; }
		}

		/// <summary>
		/// parameters string (querystring)
		/// </summary>
		public string ParmString
		{
			get { return parm; }
			set { parm = value; }
		}

		/// <summary>
		/// true if you want to use Response.Redirect(), otherwise use Server.Transfer()
		/// </summary>
		public bool Redirect
		{
			get { return redirect; }
			set { redirect = value; }
		}

	}

	//#######################################################################################
	// class PageAction
	//#######################################################################################
	
	/// <summary>
	/// PageAction class.
	/// </summary>	
	[Serializable]
	public class PageAction
	{
		//***********************************************************************
		//  private members
		//***********************************************************************

		private string id = "";
		private string path = "";
		private Hashtable actions = new Hashtable();

		//***********************************************************************
		//  constructor
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the PageAction class.
		/// </summary>
		public PageAction()
		{ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="id">Page ID</param>
		/// <param name="path">Redirect to path (url)</param>
		public PageAction(string id, string path)
		{
			this.id = id;
			this.path = path;
		}

		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// Gets or sets the page ID.
		/// </summary>
		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// Gets or sets the redirect path (url).
		/// </summary>
		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		/// <summary>
		/// Gets or sets the list of action objects.
		/// </summary>
		public Hashtable Actions
		{
			get { return actions; }
			set { actions = value; }
		}

	}

	//#######################################################################################
	// class PageFlowProvider
	//#######################################################################################

	/// <summary>
	/// This class is meant to be used by <see cref="EAF.Lib.UI.WebUtils.PageFlow"/> class. You are not shpposed to use this class directly in your page.
	/// </summary>	
	[Serializable]
	public class PageFlowProvider
	{
		//***********************************************************************
		//  private members
		//***********************************************************************
		private string rootNodeName = "pageFlow";
		private bool redirect = true;
		private Hashtable globalActions = new Hashtable();
		private Hashtable pageActions = new Hashtable();

		//***********************************************************************
		//  constructor
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the PageFlowProvider class.
		/// </summary>
		/// <param name="filepath">PageFlow.xml file.</param>
		public PageFlowProvider(string filepath)
		{
			if (Path.GetFileName(filepath) != String.Empty)
			{
				XmlTree tree = new XmlTree(XmlUtility.GetRequiredXmlNode(filepath, this.rootNodeName));
				GenerateHash(tree.RootNode);
			}
			else
			{
				// this is when filepath is a directory
				DirectoryInfo di = new DirectoryInfo(filepath);
				FileInfo[] fis = di.GetFiles("*.pgflow");
				foreach (FileInfo fi in fis)
				{
					XmlTree tree = new XmlTree(XmlUtility.GetRequiredXmlNode(fi.FullName, this.rootNodeName));
					GenerateHash(tree.RootNode);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the PageFlowProvider class.
		/// </summary>
		/// <param name="xmlRootNode">XmlNode of PageFlow.xml.</param>
		public PageFlowProvider(XmlNode xmlRootNode)
		{
			XmlTree tree = new XmlTree(xmlRootNode);
			GenerateHash(tree.RootNode);
		}


		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// true if you want to use Response.Redirect(), otherwise use Server.Transfer()
		/// </summary>
		public bool Redirect
		{
			get { return this.redirect; }
			set { this.redirect = value; }
		}

		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// Get page path(url) based on page ID.
		/// </summary>
		/// <param name="pageID">Page ID</param>
		/// <returns>Page path (url)</returns>
		public string GetPagePath(string pageID)
		{
			Object obj = this.pageActions[pageID];
			if (obj == null) return null;

			PageAction pgAct = (PageAction)obj;
			return pgAct.Path;
		}
		
		/// <summary>
		/// Get page path(url) based on global action.
		/// </summary>
		/// <param name="actionName">Global action name</param>
		/// <returns>Page path (url)</returns>
		public string GetGlobalActionPath(string actionName)
		{
			return GetGlobalActionPath(actionName, null);
		}

		/// <summary>
		/// Get page path(url) based on global action, and append extra parameters (querystring).
		/// </summary>
		/// <param name="actionName">Global action name</param>
		/// <param name="parmStr">Extra parameters (QuesrString)</param>
		/// <returns>Page path (url)</returns>
		public string GetGlobalActionPath(string actionName, string parmStr)
		{
			Object actObj = this.globalActions[actionName];
			if (actObj == null) return null;

			Action act = (Action)actObj;

			string rtnPath = null;
			
			if (act.ParmString != null && act.ParmString != "")
				rtnPath = GetPagePath(act.ForwardPageID) + "?" + act.ParmString;
			else
				rtnPath = GetPagePath(act.ForwardPageID);

			if (parmStr != null && parmStr != "")
			{
				if (act.ParmString != null && act.ParmString != "")
					rtnPath += "&";
				else
					rtnPath += "?";

				rtnPath += parmStr;
			}
			return rtnPath;
		}

		/// <summary>
		/// Get global action object
		/// </summary>
		/// <param name="actionName">action name</param>
		/// <returns>action object</returns>
		public Action GetGlobalAction(string actionName)
		{
			Object actObj = this.globalActions[actionName];
			return actObj == null ? null : (Action)actObj;
		}


		/// <summary>
		/// Get Action object based on page ID and action name
		/// </summary>
		/// <param name="pageID">Page ID</param>
		/// <param name="actionName">Action name</param>
		/// <returns>Action object</returns>
		public Action GetPageAction(string pageID, string actionName)
		{
			Object obj = this.pageActions[pageID];
			if (obj == null) return null;

			PageAction pgAct = (PageAction)obj;
			return (Action)pgAct.Actions[actionName];
		}

		/// <summary>
		/// Get page path (url) based on page ID and action name.
		/// </summary>
		/// <param name="pageID">Page ID</param>
		/// <param name="actionName">Action name</param>
		/// <returns>Page path (url)</returns>
		public string GetPageActionPath(string pageID, string actionName)
		{
			return GetPageActionPath(pageID, actionName, null);
		}

		/// <summary>
		/// Get page path (url) based on page ID and action name.
		/// </summary>
		/// <param name="pageID">Page ID</param>
		/// <param name="actionName">Action name</param>
		/// <param name="parmStr">Extra parameters (QuesrString)</param>
		/// <returns>Page path (url)</returns>
		public string GetPageActionPath(string pageID, string actionName, string parmStr)
		{
			Object obj = this.pageActions[pageID];
			if (obj == null) return null;

			PageAction pgAct = (PageAction)obj;
			Action act = (Action)pgAct.Actions[actionName];

			string rtnPath = null;

			if (act.ParmString != null && act.ParmString != "")
				rtnPath = GetPagePath(act.ForwardPageID) + "?" + act.ParmString;
			else
				rtnPath = GetPagePath(act.ForwardPageID);

			if (parmStr != null && parmStr != "")
			{
				if (act.ParmString != null && act.ParmString != "")
					rtnPath += "&";
				else
					rtnPath += "?";

				rtnPath += parmStr;
			}
			
			return rtnPath;
		}

		// private methods
		//-------------------------------------------------------------------------------------
		private void GenerateHash(XmlTreeNode root)
		{
			string redir = root.GetAttribute("redirect");
			if (redir != null && redir != "")
			{
				if (redir.ToLower() == "false")
					this.redirect = false;
				else
					this.redirect = true;
			}

			for (int i = 0; i < root.ChildNodes.Count; i++)
			{
				XmlTreeNode node = (XmlTreeNode)root.ChildNodes[i];

				if ("" + node.NodeName.ToLower() == "globalactions")
				{
					for (int k = 0; k < node.ChildNodes.Count; k++)
					{
						XmlTreeNode xtn = (XmlTreeNode)node.ChildNodes[k];

						string actName = "" + xtn.GetAttribute("name");
						
						Action act = new Action();
						act.Name = actName;
						act.ForwardPageID = "" + xtn.GetAttribute("forwardPageID");
						act.ParmString = "" + xtn.GetAttribute("parm");
						string boolRedir = "" + xtn.GetAttribute("redirect");
						act.Redirect = (boolRedir == "" ? this.Redirect : (boolRedir == "false" ? false : true));

						if (!this.globalActions.ContainsKey(actName))
							this.globalActions[actName] = act;
						else
							throw (new Exception("(PageFlowProvider ERROR) Duplicated Global Action Name Found: " + actName));
					}
				}
				else if ("" + node.NodeName.ToLower() == "pageactions")
				{
					for (int k = 0; k < node.ChildNodes.Count; k++)
					{
						XmlTreeNode xtn = (XmlTreeNode)node.ChildNodes[k];
						string pgid = "" + xtn.GetAttribute("id");
						PageAction pgAct = new PageAction(pgid, "" + xtn.GetAttribute("path"));

						for (int l = 0; l < xtn.ChildNodes.Count; l++)
						{
							XmlTreeNode xtn2 = (XmlTreeNode)xtn.ChildNodes[l];
							string actName = "" + xtn2.GetAttribute("name");

							Action act = new Action();
							act.Name = actName;
							act.ForwardPageID = "" + xtn2.GetAttribute("forwardPageID");
							act.ParmString = "" + xtn2.GetAttribute("parm");
							string boolRedir = "" + xtn2.GetAttribute("redirect");
							act.Redirect = (boolRedir == "" ? this.Redirect : (boolRedir == "false" ? false : true));

							pgAct.Actions[actName] = act;
						}
						
						if (!this.pageActions.ContainsKey(pgid))
							this.pageActions[pgid] = pgAct;
						else
							throw (new Exception("(PageFlowProvider ERROR) Duplicated Page ID Found: " + pgid));
					}
				}
			}
		}

	}
}
