using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

using Node.Lib.UI.DataDictionary;

namespace Node.Lib.UI.WebUtils
{
	/// <summary>
	/// HTTP Content type enum
	/// </summary>
	public enum HttpContentType
	{
		/// <summary>
		/// Binary
		/// </summary>
		Binary, 
		/// <summary>
		/// Excel
		/// </summary>
		Excel, 
		/// <summary>
		/// Word
		/// </summary>
		Word, 
		/// <summary>
		/// PDF
		/// </summary>
		PDF, 
		/// <summary>
		/// PowerPoint
		/// </summary>
		PowerPoint, 
		/// <summary>
		/// XML
		/// </summary>
		XML, 
		/// <summary>
		/// Text
		/// </summary>
		Text, 
		/// <summary>
		/// Gif
		/// </summary>
		GIF, 
		/// <summary>
		/// Jpeg
		/// </summary>
		JPEG, 
		/// <summary>
		/// Html
		/// </summary>
		HTML, 
		/// <summary>
		/// ZIP
		/// </summary>
		ZIP
	}

	/// <summary>
	/// Static utility class for Page activity.
	/// </summary>
	public class WebUtility
	{
		//***********************************************************************
		//  private members
		//***********************************************************************
		private static Assembly sysWebCtrlAsm = null;
		private static Assembly eafWebCtrlAsm = null;

		//***********************************************************************
		//  constructor
		//***********************************************************************

		private WebUtility() 
		{ }

		//***********************************************************************
		//  public methods
		//***********************************************************************

		/// <summary>
		/// Stream binary content
		/// </summary>
		/// <param name="cntType">content type</param>
		/// <param name="filename">filename</param>
		/// <param name="content">content byte array</param>
		public static void StreamFile(HttpContentType cntType, string filename, byte[] content)
		{
			string type;

			switch(cntType)
			{
				case HttpContentType.PDF:
					type = "application/pdf"; break;
				case HttpContentType.Excel:
					type = "application/vnd.ms-excel"; break;
				case HttpContentType.Word:
					type = "application/msword"; break;
				case HttpContentType.PowerPoint:
					type = "application/vnd.ms-powerpoint"; break;
				case HttpContentType.GIF:
					type = "image/gif"; break;
				case HttpContentType.JPEG:
					type = "image/jpeg"; break;
				case HttpContentType.XML:
					type = "text/xml"; break;
				case HttpContentType.HTML:
					type = "text/html"; break;
				case HttpContentType.Text:
					type = "text/plain"; break;
				case HttpContentType.ZIP:
					type = "application/zip"; break;
				default:
					type = "application/octet-stream";	break;
			}
			
			StreamFile(type, filename, content);
		}

		/// <summary>
		/// Stream binary content
		/// </summary>
		/// <param name="cntType">content type</param>
		/// <param name="filename">filename</param>
		/// <param name="content">content byte array</param>
		public static void StreamFile(string cntType, string filename, byte[] content)
		{
			HttpResponse rep = HttpContext.Current.Response;
			rep.Clear();
			rep.ContentType = cntType;
			rep.AddHeader("Content-Disposition", "filename=" + filename);
			rep.OutputStream.Write(content, 0, content.Length);
			rep.Flush();
			rep.End();
		}

		/// <summary>
		/// Get System.Web dll.
		/// </summary>
		public static Assembly SystemWebControlAssembly
		{
			get
			{
				if (sysWebCtrlAsm == null)
				{
					Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();

					for (int i = 0; i < asm.Length; i++)
					{
						if (asm[i].FullName.IndexOf("System.Web") >= 0)
						{
							sysWebCtrlAsm = asm[i];
							break;
						}
					}
				}
				return sysWebCtrlAsm;
			}
		}

		/// <summary>
		/// Get EAF.Lib.UI dll
		/// </summary>
		public static Assembly EAFWebControlAssembly
		{
			get
			{
				if (eafWebCtrlAsm == null)
				{
					eafWebCtrlAsm = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "\\Bin\\EAF.Lib.UI.dll");
				}
				return eafWebCtrlAsm;
			}
		}

		public static DataTable GetPerformanceMonitorInfo
		{
			get
			{
				object o = HttpContext.Current.Application["EAF.Lib.UI.HttpModules.HttpPerformanceMonitor.pageMonList"];
				if (o == null) return null;
				
				Hashtable ht = (Hashtable)o;
				if (ht.Count == 0) return null;
				
				DataTable dt = new DataTable();

				dt.Columns.Add("REQ_URL");
				dt.Columns.Add("REQ_TIME", Type.GetType("System.Int32"));
				dt.Columns.Add("REQ_COUNT", Type.GetType("System.Int32"));
				dt.Columns.Add("REQ_LENGTH", Type.GetType("System.Int64"));

				foreach (string url in ht.Keys)
				{
					HttpModules.PageMonitorInfo pmi = (HttpModules.PageMonitorInfo)ht[url];
				
					DataRow dr = dt.NewRow();
					dr[0] = pmi.Uri.AbsoluteUri;
					dr[1] = pmi.RequestTime;
					dr[2] = pmi.RequestCount;
					dr[3] = pmi.ContentLength;
					dt.Rows.Add(dr);
				}
				return dt;
			}
		}
		//***********************************************************************
		//  private methods
		//***********************************************************************

	}
}
