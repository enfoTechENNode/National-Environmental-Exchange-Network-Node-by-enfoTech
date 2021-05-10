using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using NodeLib = Node.Lib.UI.Elements;


namespace Node.Lib.UI.WebUtils
{
	/// <summary>
	/// Static utility class for using PageFlow activity.
	/// </summary>
	public class PageFlow
	{
		//***********************************************************************
		//  private members
		//***********************************************************************

		private const string cacheKey = "EAF.Lib.UI.WebUtils.PageFlow";
		private const string loadErrorMsg = "PageFlow Initialized Error!";

		//***********************************************************************
		//  constructor
		//***********************************************************************

		private PageFlow() { }

		//***********************************************************************
		//  public methods
		//***********************************************************************

		/// <summary>
		/// Initilzation action of PageFlow. 
		/// </summary>
		public static void Init()
		{
			string file = Properties.Settings.Default.PageFlowFile;
			if (file!=null && file != "")
			{
				object o = HttpContext.Current.Cache[cacheKey];
                if (o == null || !(o is NodeLib.PageFlowProvider))
				{
					string filepath = HttpContext.Current.Request.PhysicalApplicationPath + file;
                    NodeLib.PageFlowProvider pfProvider = new NodeLib.PageFlowProvider(filepath);
					CacheDependency cacheDep = new CacheDependency(filepath);
					HttpContext.Current.Cache.Insert(cacheKey, pfProvider, cacheDep);
				}
			}
		}

		/// <summary>
		/// Get global action object.
		/// </summary>
		/// <param name="actionName">Action name from globalActions section.</param>
		/// <returns>Action object</returns>
        public static NodeLib.Action GetGlobalAction(string actionName)
		{
			return pfProvider == null ? null : pfProvider.GetGlobalAction(actionName);
		}


		/// <summary>
		/// Get global action path (url) defined in XML file.
		/// </summary>
		/// <param name="actionName">Action name from globalActions section.</param>
		/// <returns>Path (url) will be redirected to.</returns>
		public static string GetGlobalActionPath(string actionName)
		{
			return pfProvider == null ? loadErrorMsg : pfProvider.GetGlobalActionPath(actionName);
		}

		/// <summary>
		/// Get global action path (url) defined in XML file, and pass in parameters.
		/// </summary>
		/// <param name="actionName">Action name from globalActions section.</param>
		/// <param name="parmStr">Query string you want to append to the page.</param>
		/// <returns>Path (url) will be redirected to.</returns>
		public static string GetGlobalActionPath(string actionName, string parmStr)
		{
			return pfProvider == null ? loadErrorMsg : pfProvider.GetGlobalActionPath(actionName,parmStr);
		}

		/// <summary>
		/// Get page path (url) from page ID defined in XML file.
		/// </summary>
		/// <param name="pageID">Page ID</param>
		/// <returns>Path (url) will be redirected to.</returns>
		public static string GetPagePath(string pageID)
		{
			return pfProvider == null ? loadErrorMsg : pfProvider.GetPagePath(pageID);
		}

		/// <summary>
		/// Get Action object from a page ID and it's action name.
		/// </summary>
		/// <param name="pageID">Page ID of the page.</param>
		/// <param name="actionName">Action name of the page.</param>
		/// <returns>Action object represents the action.</returns>
        public static NodeLib.Action GetPageAction(string pageID, string actionName)
		{
            return pfProvider == null ? null : (NodeLib.Action)pfProvider.GetPageAction(pageID, actionName);
		}

		/// <summary>
		/// Get path (url) where you page will be redirect to.
		/// </summary>
		/// <param name="pageID">Page ID of the page.</param>
		/// <param name="actionName">Action name of the page.</param>
		/// <returns>Path (url) will be redirected to.</returns>
		public static string GetPageActionPath(string pageID, string actionName)
		{
			return pfProvider == null ? loadErrorMsg : pfProvider.GetPageActionPath(pageID, actionName);
		}

		/// <summary>
		/// Get path (url) where you page will be redirect to, and pass in parameters.
		/// </summary>
		/// <param name="pageID">Page ID of the page.</param>
		/// <param name="actionName">Action name of the page.</param>
		/// <param name="parmStr">Query string you want to append to the page.</param>
		/// <returns>Path (url) will be redirected to.</returns>
		public static string GetPageActionPath(string pageID, string actionName, string parmStr)
		{
			return pfProvider == null ? loadErrorMsg : pfProvider.GetPageActionPath(pageID, actionName, parmStr);
		}

		/// <summary>
		/// Navigate to default action page.
		/// </summary>
		/// <param name="fromPageID">Your current page ID.</param>
		public static void NavigateNext(string fromPageID)
		{
			NavigateNext(fromPageID, "default", null);
		}

		/// <summary>
		/// Navigate to the page based on action name defined in PageFlow.xml file.
		/// </summary>
		/// <param name="fromPageID">Your current page ID.</param>
		/// <param name="actionName">Action name defined in PageFlow.xml file.</param>
		public static void NavigateNext(string fromPageID, string actionName)
		{
			NavigateNext(fromPageID, actionName, null);
		}

		/// <summary>
		/// Navigate to the page based on action name defined in PageFlow.xml file.
		/// </summary>
		/// <param name="fromPageID">Your current page ID.</param>
		/// <param name="actionName">Action name defined in PageFlow.xml file.</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		public static void NavigateNext(string fromPageID, string actionName, string parm)
		{
            NodeLib.Action act = GetPageAction(fromPageID, actionName);
			if (act != null)
			{
				if (act.Redirect)
					HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath + GetPageActionPath(fromPageID, actionName, parm));
				else
					HttpContext.Current.Server.Transfer(HttpContext.Current.Request.ApplicationPath + GetPageActionPath(fromPageID, actionName, parm));
			}
		}

		/// <summary>
		/// Navigate to the page defined in global action.
		/// </summary>
		/// <param name="actionName">Global Action name defined in PageFlow.xml file.</param>
		public static void NavigateGlobalAction(string actionName)
		{
			NavigateGlobalAction(actionName, null);
		}

		/// <summary>
		/// Navigate to the page defined in global action.
		/// </summary>
		/// <param name="actionName">Global Action name defined in PageFlow.xml file.</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		public static void NavigateGlobalAction(string actionName, string parm)
		{
            NodeLib.Action act = GetGlobalAction(actionName);
			if (act != null)
			{
				if (act.Redirect)
					HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath + GetGlobalActionPath(actionName, parm));
				else
					HttpContext.Current.Server.Transfer(HttpContext.Current.Request.ApplicationPath + GetGlobalActionPath(actionName, parm));
			}
		}

		/// <summary>
		/// Navigate to the page based on pageID.
		/// </summary>
		/// <param name="toPageID">pageID you want to jump to</param>
		public static void NavigateToPage(string toPageID)
		{
			NavigateToPage(toPageID, null);
		}

		/// <summary>
		/// Navigate to the page based on pageID.
		/// </summary>
		/// <param name="toPageID">pageID you want to jump to</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		public static void NavigateToPage(string toPageID, string parm)
		{
			string finalUrl = GetPagePath(toPageID);

			if (parm != null && parm != "")
				finalUrl += parm;
			if(pfProvider.Redirect)
				HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath + finalUrl);
			else
				HttpContext.Current.Server.Transfer(HttpContext.Current.Request.ApplicationPath + finalUrl);
		}

		//***********************************************************************
		//  private methods
		//***********************************************************************

        private static NodeLib.PageFlowProvider pfProvider
		{
			get
			{
				object o = HttpContext.Current.Cache[cacheKey];
				//return (o == null) ? null : (PageFlowProvider)o;
				if (o == null)
				{
					Init();
					o = HttpContext.Current.Cache[cacheKey];
				}
                return (NodeLib.PageFlowProvider)o;

			}
		}

	}
}
