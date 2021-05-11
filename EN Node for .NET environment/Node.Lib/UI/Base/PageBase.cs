using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using Node.Lib.UI.Elements;
using Node.Lib.UI.WebUtils;
using Node.Lib.Data;

namespace Node.Lib.UI.Base
{
	/// <summary>
	/// This is abstract class and for you to inherit.
	/// </summary>
	public abstract class PageBase: System.Web.UI.Page
	{
		//***********************************************************************
		// private members
		//***********************************************************************
		
		//private string cacheKeyBase = "EAF.Lib.UI.Base.PageBase.";
		private string pageID = null;
		private string textResourcePageKey = null;

		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// Unique ID of the page.
		/// Default value will be the folder structure of current page (Pages.Folder1.Folder2.MyPage.aspx).
		/// </summary>
		public string PageID
		{
			get { return this.pageID; }
			set { this.pageID = value; }
		}

		/// <summary>
		/// textResourcePageKey of the page.
		/// Default value will be equal to ID.
		/// </summary>
		public string TextResourcePageKey
		{
			get { return this.textResourcePageKey; }
			set { this.textResourcePageKey = value; }
		}

		//***********************************************************************
		// virtual methods
		//***********************************************************************

		// Security checking methods
		//---------------------------------

		/// <summary>
		/// For you to implement the code and check if current user is login.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>true if user is login; otherwise, false.</returns>
		protected virtual bool IsLogin(EventArgs e) { return true; }	
		
		/// <summary>
		/// This will be excuted when IsLogin() return false. 
		/// Do some clean up if needed.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>URL which will be redirect to</returns>
		protected virtual string LoginFailedAction(EventArgs e) { return ""; }

		/// <summary>
		/// For you to implement the code and check if current user has access right to this page.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>true if user has right; otherwise, false.</returns>
		protected virtual bool IsServiceAllowed(EventArgs e) { return true; }

		/// <summary>
		/// This will be excuted when IsServiceAllowed() return false.
		/// Do some clean up if needed.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>URL which will be redirect to</returns>
		protected virtual string ServiceDeniedAction(EventArgs e) { return ""; }

		/// <summary>
		/// For you to implement the code and check if enough condition is set to run this page.
		/// Ex: Is necessary parameters is passed in to the page.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>true if parameters are collected; otherwise, false.</returns>
		protected virtual bool IsContinuable(EventArgs e) { return true; }
		
		/// <summary>
		/// This will be excuted when IsContinuable() return false.
		/// Do some clean up if needed.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		/// <returns>URL which will be redirect to</returns>
		protected virtual string ContinueDeniedAction(EventArgs e) { return ""; }


		// Simply page flow
		//---------------------------------
		
		/// <summary>
		/// For you to implement generating dynamic controls of the page.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void GenerateDynamicControls(EventArgs e) { }

		/// <summary>
		/// Replacement for ASP page OnLoad() event, since PageBase use it to do something else.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void OnLoadAction(EventArgs e) { }

		/// <summary>
		/// Run every time when Page is loaded.
		/// Usually for you to Collect variables from Request or Session objects and 
		/// initialize stateless variables, or stateful variables needed to be clear up every time.
		/// 
		/// PS. Do not use Page_Load if you want to use this method.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void PageLoadBegin(EventArgs e) { }

		/// <summary>
		/// Run only when page is not Post Back, i.e. First time page is requested.
		/// Usually for you to initialize most stateful ASP.NET or EAF Web controls. 
		/// Since most of these controls will be remembered in ViewState object, you only need to initialize them once.
		/// 
		/// PS. Do not use Page_Load if you want to use this method.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		[Obsolete("This is obsolete method, instead use InitControlValues()", false)]
		protected virtual void InitControls(EventArgs e) { }

		/// <summary>
		/// Run only when page is not Post Back, i.e. First time page is requested.
		/// Usually for you to initialize most stateful ASP.NET or EAF Web controls. 
		/// Since most of these controls will be remembered in ViewState object, you only need to initialize them once.
		/// 
		/// PS. Do not use Page_Load if you want to use this method.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void InitControlValues(EventArgs e) { }

		/// <summary>
		/// Run Only when page is Post Back, will not execute when page is requested first time.
		/// Usually for you to implement major functions of ASP.NET pages.
		/// 
		/// PS. Do not use Page_Load if you want to use this method.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void PageLoadPostBack(EventArgs e) { }

		/// <summary>
		/// Run every time when Page is loaded, and after InitControls() or PageLoadPostBack() events.
		/// Most of the situation, implementation of this method is not needed.
		/// 
		/// PS. Do not use Page_Load if you want to use this method.
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void PageLoadEnd(EventArgs e) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e">EventArgs of Page.</param>
		protected virtual void PagePreRender(EventArgs e) { }


		//***********************************************************************
		// protected methods
		//***********************************************************************

		/// <summary>
		/// Navigate to default action page.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		protected void NavigateNext()
		{
			PageFlow.NavigateNext(this.pageID);
		}

		/// <summary>
		/// Navigate to the page based on action name defined in PageFlow.xml file.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="actionName">Action name defined in PageFlow.xml file.</param>
		protected void NavigateNext(string actionName)
		{
			PageFlow.NavigateNext(this.pageID,actionName);
		}
		
		/// <summary>
		/// Navigate to the page based on action name defined in PageFlow.xml file, 
		/// and add parameters (query string) for next page.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="actionName">Action name defined in PageFlow.xml file.</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		protected void NavigateNext(string actionName, string parm)
		{
			PageFlow.NavigateNext(this.pageID, actionName,parm);
		}

		//-----------------------------------------------------
		
		/// <summary>
		/// Navigate to the page defined in global action.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="actionName">Global Action name defined in PageFlow.xml file.</param>
		protected void NavigateGlobalAction(string actionName)
		{
			PageFlow.NavigateGlobalAction(actionName);
		}

		/// <summary>
		/// Navigate to the page defined in global action.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="actionName">Global Action name defined in PageFlow.xml file.</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		protected void NavigateGlobalAction(string actionName, string parm)
		{
			PageFlow.NavigateGlobalAction(actionName, parm);
		}

		//-----------------------------------------------------
		/// <summary>
		/// Navigate to the page based on pageID.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="toPageID">pageID you want to jump to</param>
		protected void NavigateToPage(string toPageID)
		{
			PageFlow.NavigateToPage(toPageID);
		}

		/// <summary>
		/// Navigate to the page based on pageID.
		/// 
		/// PS. Use this methods only when you implement PageFlow framework.
		/// </summary>
		/// <param name="toPageID">pageID you want to jump to</param>
		/// <param name="parm">Extra parameters (query string) for next page.</param>
		protected void NavigateToPage(string toPageID, string parm)
		{
			PageFlow.NavigateToPage(toPageID, parm);
		}

		//***********************************************************************
		// page events
		//***********************************************************************

		protected override void OnPreInit(EventArgs e)
		{
			if (Properties.Settings.Default.DBTrace)
			{
				DBAdapter.QueryCounter = 0;
				DBAdapter.UpdateCounter = 0;
			}
			else
			{
				DBAdapter.QueryCounter = -1;
				DBAdapter.UpdateCounter = -1;
			} 
			
			base.OnPreInit(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			this.pageID = PageUtility.GetCurrentPagePath(".");
			if (this.textResourcePageKey == null) this.textResourcePageKey = this.pageID;
			
			base.OnInit(e);

			//if (this.PageID == null) this.pageID = PageUtility.GetCurrentPagePath(".");
			
			GenerateDynamicControls(e);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			if (!IsLogin(e))
				Response.Redirect(LoginFailedAction(e));
			if (!IsServiceAllowed(e))
				Response.Redirect(ServiceDeniedAction(e));
			if (!IsContinuable(e))
				Response.Redirect(ContinueDeniedAction(e));

			// Register Utils.js Javascript for each page.
			PageUtility.RegisterUtilsScript(this);
			PageUtility.RegisterTimeupAlertScript(this);
			
			OnLoadAction(e);

			//** major page flow

			PageLoadBegin(e);
			if (!IsPostBack)
			{
				InitControlValues(e);
				//InitControls(e);
			}
			else
				PageLoadPostBack(e);
			PageLoadEnd(e);

			base.OnLoad(e); 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			PagePreRender(e);
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			base.Render(writer);

			if (Properties.Settings.Default.DBTrace)
			{
				string s = "";
				s = "<div style=\"padding:8px; color:#FFFFFF; background-color:#990000; font-size:9pt; font-weight:bold;\">";
				s += "[DB Query Count= " + DBAdapter.QueryCounter + "]&nbsp;&nbsp;";
				s += "[DB Update Count= " + DBAdapter.UpdateCounter + "]";
				s += "</div>";

				writer.Write(s);
			}
		}

        protected bool IsRealPostback()
        {
            bool bFlag = true;
            if (this.Request["x"] == null || this.Request["y"] == null)
                bFlag = false;
            return bFlag;
        }
	}
}
