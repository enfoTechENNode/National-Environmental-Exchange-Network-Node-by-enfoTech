using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using Node.Lib.UI.Elements;

namespace Node.Lib.UI.WebUtils
{
	/// <summary>
	/// Static utility class for using TextResource activity.
	/// </summary>
	public class TextResource
	{
		//***********************************************************************
		//  private members
		//***********************************************************************
		
		private const string cacheKey = "EAF.Lib.UI.WebUtils.TextResource";

		//***********************************************************************
		//  constructor
		//***********************************************************************

		private TextResource() { }

		//***********************************************************************
		//  public methods
		//***********************************************************************

		/// <summary>
		/// Initilzation action of TextResource. 
		/// </summary>
		public static void Init()
		{
			string file = Properties.Settings.Default.TextResourceFile;
			bool ignoreCase = Properties.Settings.Default.TextResourceIgnoreKeyCases;

			if (file != null && file != "")
			{
				Cache cache = HttpContext.Current.Cache;

				object o = cache[cacheKey];
				if (o==null || !(o is TextResourceProvider))
				{
					string filepath = HttpContext.Current.Request.PhysicalApplicationPath + file;
					TextResourceProvider trProvider = new TextResourceProvider(filepath, ignoreCase);
					CacheDependency cacheDep = new CacheDependency(filepath);
					cache.Insert(cacheKey, trProvider, cacheDep);
				}
			}
		}
		
		/// <summary>
		/// Get value by key defined in XML file.
		/// </summary>
		/// <param name="fullKey">Full key</param>
		/// <returns>Value of the key</returns>
		public static string GetValue(string fullKey)
		{
			return GetValue(null, fullKey);
		}

		/// <summary>
		/// Get value by global key defined in XML file.
		/// </summary>
		/// <param name="fullKey">Full key</param>
		/// <returns>Value of the key</returns>
		public static string GetGlobalValue(string fullKey)
		{
			if (trProvider == null)
				return "TextResource Initialized Error.";
			else
			{
				return trProvider.GetGlobalValue(fullKey);
			}
		}

		/// <summary>
		/// Get value by key, seperated by pageID and pageKey defined in XML file. 
		/// </summary>
		/// <param name="pageID"></param>
		/// <param name="pageKey"></param>
		/// <returns>Value of the key</returns>
		public static string GetValue(string pageID, string pageKey)
		{
			if (trProvider == null)
				return "TextResource Initialized Error.";
			else
			{
				string key = "";
				if (pageID != null && pageID != "")
				{
					key = pageID + trProvider.KeySplitter + pageKey;

				}
				else
				{
					key = pageKey;
				}

				return trProvider.GetValue(key);
			}
		}

		//----------------------------------------------------------
		private static TextResourceProvider trProvider
		{
			get
			{
				object o = HttpContext.Current.Cache[cacheKey];
				if (o == null) 
				{
					Init();
					o = HttpContext.Current.Cache[cacheKey];
				}
				return (TextResourceProvider)o;
			}
		}
	}
}
