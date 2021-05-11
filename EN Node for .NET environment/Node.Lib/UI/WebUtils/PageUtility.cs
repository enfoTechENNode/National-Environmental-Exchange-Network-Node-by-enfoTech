using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Node.Lib.UI.DataDictionary;

namespace Node.Lib.UI.WebUtils
{
	/// <summary>
	/// Static utility class for Page activity.
	/// </summary>
	public class PageUtility
	{
		//***********************************************************************
		//  private members
		//***********************************************************************

		private const string sessionBagKey = "EAF.Lib.UI.WebUtils.PageUtility.SessionBag";

		//***********************************************************************
		//  constructor
		//***********************************************************************

		private PageUtility() { }

		//***********************************************************************
		//  public methods
		//***********************************************************************

		/// <summary>
		/// return App_Script virtual path.
		/// </summary>
		public static string EAFScriptBasePath
		{
			get { return HttpContext.Current.Request.ApplicationPath + Properties.Settings.Default.ScriptBase; }
		}

		/// <summary>
		/// Register Utils.js script to the page.
		/// </summary>
		/// <param name="thePage">The Page object.</param>
		public static void RegisterUtilsScript(Page thePage)
		{
			if (!thePage.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_Utils))
			{
				StringBuilder s = new StringBuilder();
				s.AppendLine("<script type=\"text/javascript\" src=\"" + EAFScriptBasePath + "Utils.js\" ></script>");
				thePage.ClientScript.RegisterClientScriptBlock(thePage.GetType(), ClientScriptRegID.EAF_Utils, s.ToString());
			}
		}

		/// <summary>
		/// Register Utils.js script to the page.
		/// </summary>
		/// <param name="thePage">The Page object.</param>
		public static void RegisterAjaxScript(Page thePage)
		{
			if (!thePage.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_AjaxObjects))
			{
				StringBuilder s = new StringBuilder();
				s.AppendLine("<script type=\"text/javascript\" src=\"" + EAFScriptBasePath + "AjaxObjects.js\" ></script>");
				thePage.ClientScript.RegisterClientScriptBlock(thePage.GetType(), ClientScriptRegID.EAF_AjaxObjects, s.ToString());
			}
		}

		/// <summary>
		/// Register SpellCheck.js script to the page.
		/// </summary>
		/// <param name="thePage">The Page object.</param>
		public static void RegisterSpellScript(Page thePage, string spellPgUrl)
		{
			if (!thePage.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_SpellCheck))
			{
				StringBuilder s = new StringBuilder();
				s.AppendLine("<script type=\"text/javascript\" src=\"" + EAFScriptBasePath + "SpellCheck.js\" ></script>");
				
				s.Append("<script type=\"text/javascript\">");
				s.Append("SpellCheck.spellURL = \"" + spellPgUrl + "\";");
				s.AppendLine("</script>");

				thePage.ClientScript.RegisterClientScriptBlock(thePage.GetType(), ClientScriptRegID.EAF_SpellCheck, s.ToString());
			}
		}

		/// <summary>
		/// Register js script to the page.
		/// </summary>
		/// <param name="thePage">The Page object.</param>
		public static void RegisterScript(Page thePage, string scriptID, string script)
		{
			if (!thePage.ClientScript.IsClientScriptBlockRegistered(scriptID))
			{
				thePage.ClientScript.RegisterClientScriptBlock(thePage.GetType(), scriptID, script);
			}
		}

		/// <summary>
		/// Register Session time up alert javascript to the page.
		/// </summary>
		/// <param name="thePage">The Page object.</param>
		public static void RegisterTimeupAlertScript(Page thePage)
		{
			if (Properties.Settings.Default.AlertScriptOn)
			{
				RegisterUtilsScript(thePage);

				int msec = Properties.Settings.Default.AlertScriptMin * 60;
				string msg = Properties.Settings.Default.AlertScriptMsg;
				string url = HttpContext.Current.Request.ApplicationPath + Properties.Settings.Default.AlertRefreshUrl;
				string errUrl = HttpContext.Current.Request.ApplicationPath + Properties.Settings.Default.AlertErrorUrl;

				if (!thePage.ClientScript.IsClientScriptBlockRegistered(ClientScriptRegID.EAF_TimeupAlert))
				{
					StringBuilder s = new StringBuilder();

					s.AppendLine("<script type=\"text/javascript\" >");
					//s.AppendLine("<!--");
					s.AppendLine("Utils.setCounter(" + msec + ");");

					s.AppendLine("Utils.timeupAction=function(){");
					s.AppendLine("if(confirm(\"" + msg.Replace("\"", "\\\"") + "\\n( Message Time: \"+(new Date()).toString()+\" )\"))");
					s.AppendLine("{ Utils.runCallback('" + url + "',null,''); Utils.setCounter(" + msec + "); }");
					s.AppendLine("}");

					if (errUrl != null && errUrl.Trim() != "")
					{
						s.AppendLine("Utils.callbackErrorAction = function(req){");
						s.AppendLine("Utils.gotoURL('self','" + errUrl + "');");
						s.AppendLine("}");
					}

					//s.AppendLine("-->");
					s.AppendLine("</script>");

					thePage.ClientScript.RegisterStartupScript(thePage.GetType(), ClientScriptRegID.EAF_TimeupAlert, s.ToString());
				}
			}
		}

		//-------------------------------------------------------------------------------------

		/// <summary>
		/// Get current page path structure.
		/// </summary>
		/// <param name="splitter">splitter</param>
		/// <returns>page path structure w/o file extension</returns>
		public static string GetCurrentPagePath(string splitter)
		{
			HttpRequest req = HttpContext.Current.Request;
			string s = "";
			string path = req.CurrentExecutionFilePath;
			s = (path.Substring(path.IndexOf(req.ApplicationPath) + req.ApplicationPath.Length));
			s = s.Substring(0, s.LastIndexOf("."));
			if(splitter!=null && splitter!="") 
				s = s.Substring(s.IndexOf("/") + 1).Replace("/", splitter);

			return s;
		}

		/// <summary>
		/// Get object from Session. IF the value is null, the default object will be returned and store in Session.
		/// </summary>
		/// <param name="sessionKey">Session key.</param>
		/// <param name="defaultObj">The default object returned, if value of the Session key is null.</param>
		/// <returns>The object found in Session, or default object if null.</returns>
		public static object GetSessionObj(string sessionKey, object defaultObj)
		{
			return GetSessionObj(sessionKey, defaultObj, true);
		}

		/// <summary>
		/// Get object from Session. IF the value is null, the default object will be returned, and add the object to Session if addToSession is true.
		/// </summary>
		/// <param name="sessionKey">Session key.</param>
		/// <param name="defaultObj">The default object returned, if value of the Session key is null.</param>
		/// <param name="addToSession">Set true to store default object in Session, if Session return null of the sessionkey you pass in.</param>
		/// <returns>The object found in Session, or default object if null.</returns>
		public static object GetSessionObj(string sessionKey, object defaultObj, bool addToSession)
		{
            HttpSessionState session = HttpContext.Current.Session;

			object o = session[sessionKey];
			if (o != null)
			{
				return o;
			}
			else
			{
				if (addToSession) session.Add(sessionKey, defaultObj);
				return defaultObj;
			}
		}

		/// <summary>
		/// Get object from Cache. IF the value is null, the default object will be returned, and add the object to Cache.
		/// </summary>
		/// <param name="cacheKey">Cache key.</param>
		/// <param name="defaultObj">The default object returned, if value of the Session key is null.</param>
		/// <returns>The object found in Cache, or default object if null.</returns>
		public static object GetCacheObj(string cacheKey, object defaultObj)
		{
			return GetCacheObj(cacheKey, defaultObj, true);
		}

		/// <summary>
		/// Get object from Cache. IF the value is null, the default object will be returned, and add the object to Cache if addToCache sets true.
		/// </summary>
		/// <param name="cacheKey">Cache key.</param>
		/// <param name="defaultObj">The default object returned, if value of the Session key is null.</param>
		/// <param name="addToCache">Set true to store default object in Cache, if Cache return null of the cachekey you pass in.</param>
		/// <returns>The object found in Cache, or default object if null.</returns>		
		public static object GetCacheObj(string cacheKey, object defaultObj, bool addToCache)
		{
			Cache cache = HttpContext.Current.Cache;

			object o = cache[cacheKey];
			if (o != null)
			{
				return o;
			}
			else
			{
				if (addToCache) cache.Insert(cacheKey, defaultObj);
				return defaultObj;
			}
		}

		/// <summary>
		/// Get object from Cache. IF the value is null, the default object will be returned, and add the object to Cache if addToCache sets true based on the cache dependency.
		/// </summary>
		/// <param name="cacheKey">Cache key.</param>
		/// <param name="defaultObj">The default object returned, if value of the Session key is null.</param>
		/// <param name="dependency">Cache dependency of the cache object.</param>
		/// <returns>The object found in Cache, or default object if null.</returns>				
		public static object GetCacheObj(string cacheKey, object defaultObj, CacheDependency dependency)
		{
			Cache cache = HttpContext.Current.Cache;

			object o = cache[cacheKey];
			if (o != null)
			{
				return o;
			}
			else
			{
				cache.Insert(cacheKey, defaultObj, dependency);
				return defaultObj;
			}
		}

		//---------------------------------------------------------------------------

		/// <summary>
		/// Save control values of a container control in a HashTable, such as Page, PlaceHolder or Panel.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be saved.)</param>
		public static void SavePageControlValues(string ctrlPrefix, Control ctrl)
		{
			SavePageControlValues(ctrlPrefix, ctrl, (Hashtable)GetSessionObj(sessionBagKey, new Hashtable()));
		}

		/// <summary>
		/// Save control values of a container control in a HashTable, such as Page, PlaceHolder or Panel.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be saved.)</param>
		/// <param name="hashTable">Pass in your own HashTable.</param>
		public static void SavePageControlValues(string ctrlPrefix, Control ctrl, Hashtable hashTable)
		{
			RcsvGetSetControls(ctrlPrefix, ctrl, hashTable, false);
		}

		/// <summary>
		/// Restore control values of a container control from a HashTable, such as Page, PlaceHolder or Panel.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be restored.)</param>
		public static void RestorePageControlValues(string ctrlPrefix, Control ctrl)
		{
			RestorePageControlValues(ctrlPrefix, ctrl, (Hashtable)GetSessionObj(sessionBagKey, new Hashtable()));
		}

		/// <summary>
		/// Restore control values of a container control from a HashTable, such as Page, PlaceHolder or Panel.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be restored.)</param>
		/// <param name="hashTable">Pass in your own HashTable.</param>
		public static void RestorePageControlValues(string ctrlPrefix, Control ctrl, Hashtable hashTable)
		{
			RcsvGetSetControls(ctrlPrefix, ctrl, hashTable, true);
		}

		/// <summary>
		/// Clear control values of a container control from a HashTable.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be cleared.)</param>
		public static void ClearPageControlValues(string ctrlPrefix, Control ctrl)
		{
			ClearPageControlValues(ctrlPrefix, ctrl, (Hashtable)GetSessionObj(sessionBagKey, new Hashtable()));
		}

		/// <summary>
		/// Clear control values of a container control from a HashTable.
		/// </summary>
		/// <param name="ctrlPrefix">A unique prefix key of the container control.</param>
		/// <param name="ctrl">The container control. (The value of the control, if any, won't be cleared.)</param>
		/// <param name="hashTable">Pass in your own HashTable.</param>
		public static void ClearPageControlValues(string ctrlPrefix, Control ctrl, Hashtable hashTable)
		{
			RcsvClearControls(ctrlPrefix, ctrl, hashTable);
		}

		/// <summary>
		/// Set selected value of a ListControl.
		/// </summary>
		/// <param name="lc">The ListControl</param>
		/// <param name="val">Value to be selected.</param>
		public static void SetSelectedValue(ListControl lc, string val)
		{
			for (int i = 0; i < lc.Items.Count; i++)
			{
				ListItem li = lc.Items[i];

				if (li.Value == val)
					li.Selected = true;
				else
					li.Selected = false;
			}
		}

		/// <summary>
		/// Set selected value of a HtmlSelect box control.
		/// </summary>
		/// <param name="hs">The HtmlSelect control.</param>
		/// <param name="val">Value to be selected.</param>
		public static void SetSelectedValue(HtmlSelect hs, string val)
		{
			for (int i = 0; i < hs.Items.Count; i++)
			{
				ListItem li = hs.Items[i];

				if (li.Value == val)
					li.Selected = true;
				else
					li.Selected = false;
			}
		}

		//***********************************************************************
		//  private methods
		//***********************************************************************

		private static void RcsvGetSetControls(String ctrlPrefix, Control ctrl, Hashtable ht, bool isGet)
		{
			if (ctrl == null) return;

			for (int i = 0; i < ctrl.Controls.Count; i++)
			{
				Control c = ctrl.Controls[i];
				string key = ctrlPrefix + c.ID;

				if ((c is TextBox) || (c is HtmlInputText) || (c is HtmlTextArea))
					GetSetTextBox(ctrlPrefix, c, ht, isGet);
				else if (c is ListBox || c is DropDownList || c is HtmlSelect)
					GetSetSelectBox(ctrlPrefix, c, ht, isGet);
				else if (c is GridView)
				{
					if (isGet)
						((GridView)c).PageIndex = (ht[key] == null ? 0 : (int)ht[key]);
					else
						ht[key] = ((GridView)c).PageIndex;
				}
					

				RcsvGetSetControls(ctrlPrefix, c, ht, isGet);
			}
		}

		private static void RcsvClearControls(String ctrlPrefix, Control ctrl, Hashtable ht)
		{
			if (ctrl == null) return;

			for (int i = 0; i < ctrl.Controls.Count; i++)
			{
				Control c = ctrl.Controls[i];

				if (c is TextBox)
					((TextBox)c).Text = "";
				else if (c is HtmlInputText)
					((HtmlInputText)c).Value = "";
				else if (c is HtmlTextArea)
					((HtmlTextArea)c).Value = "";
				else if (c is GridView)
					((GridView)c).PageIndex = 0;

				RcsvClearControls(ctrlPrefix, c, ht);
			}
		}

		//------------------------------------------------------------------------------------------
		private static void GetSetTextBox(String ctrlPrefix, Control ctrl, Hashtable ht, bool isGet)
		{
			if (ht != null)
			{
				string key = ctrlPrefix + ctrl.ID;

				if (ctrl is TextBox)
				{
					if (isGet)
						((TextBox)ctrl).Text = ("" + ht[key]).Trim();
					else
						ht[key] = ((TextBox)ctrl).Text.Trim();
				}
				else if (ctrl is HtmlInputText)
				{
					if (isGet)
						((HtmlInputText)ctrl).Value = ("" + ht[key]).Trim();
					else
						ht[key] = ((HtmlInputText)ctrl).Value.Trim();
				}
				else if (ctrl is HtmlTextArea)
				{
					if (isGet)
						((HtmlTextArea)ctrl).Value = ("" + ht[key]).Trim();
					else
						ht[key] = ((HtmlTextArea)ctrl).Value.Trim();
				}
			}
		}


		private static void GetSetSelectBox(String ctrlPrefix, Control ctrl, Hashtable ht, bool isGet)
		{
			if (ht != null)
			{
				if (ctrl is ListControl)
				{
					ListControl lc = (ListControl)ctrl;
					if (isGet)
					{
						SetSelectedValue(lc, "" + ht[ctrlPrefix + ctrl.ID]);
					}
					else
					{
						ListItem i = lc.SelectedItem;
						if (i != null)
							ht[ctrlPrefix + ctrl.ID] = i.Value;
					}
				}
				else if (ctrl is HtmlSelect)
				{
					HtmlSelect hs = (HtmlSelect)ctrl;
					if (isGet)
						SetSelectedValue(hs, "" + ht[ctrlPrefix + ctrl.ID]);
					else
						ht[ctrlPrefix + ctrl.ID] = hs.Items[hs.SelectedIndex].Value;
				}
			}
		}
	}
}
