using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Web;
using System.Net;

namespace Node.Lib.UI.HttpModules
{	
	public class HttpPerformanceMonitor: IHttpModule
	{
		private DateTime start;
		
		
		private Hashtable pageMonList
		{
			get
			{
				Hashtable ht;
				object o = HttpContext.Current.Application["EAF.Lib.UI.HttpModules.HttpPerformanceMonitor.pageMonList"];
				if (o == null)
				{
					ht = new Hashtable();
					HttpContext.Current.Application["EAF.Lib.UI.HttpModules.HttpPerformanceMonitor.pageMonList"] = ht;
				}
				else
				{
					ht = (Hashtable)o;
				}
				return ht;
			}
			//set { HttpContext.Current.Application["EAF.Lib.UI.HttpModules.HttpPerformanceMonitor.pageMonList"] = value; }
		}



		void IHttpModule.Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(context_BeginRequest);
			context.EndRequest += new EventHandler(context_EndRequest);
		}

		void context_BeginRequest(object sender, EventArgs e)
		{
			start = DateTime.Now;
		}

		void context_EndRequest(object sender, EventArgs e)
		{
			RecordInfo();

			//long bytes = CountBytesReceived();
			
		}

		void IHttpModule.Dispose() { }


		private void RecordInfo()
		{
			TimeSpan ts = DateTime.Now - start;
			HttpContext ctx = HttpContext.Current;
			PageMonitorInfo pmi;


			if (IsRecord(ctx.Request.Url.AbsoluteUri))
			{
				//long bytes = CountBytesReceived(ctx.Request.Url.AbsoluteUri);

				if (pageMonList.ContainsKey(ctx.Request.Url.AbsoluteUri))
				{
					pmi = (PageMonitorInfo)pageMonList[ctx.Request.Url.AbsoluteUri];
					if (ts.Milliseconds > pmi.RequestTime) pmi.RequestTime = ts.Milliseconds;
					//if (bytes > pmi.ContentLength) pmi.ContentLength = bytes;
					pmi.RequestCount++;
				}
				else
				{
					pmi = new PageMonitorInfo();
					pageMonList[ctx.Request.Url.AbsoluteUri] = pmi;

					pmi.RequestTime = ts.Milliseconds;
					pmi.Uri = ctx.Request.Url;
					pmi.RequestCount++;
					//pmi.ContentLength = bytes;
				}
			}
		}

		private bool IsRecord(string uri)
		{
			if (uri.LastIndexOf(".aspx") > 0) return true;
			if (uri.LastIndexOf(".ashx") > 0) return true;
			return false;
		}

		private long CountBytesReceived(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream s = response.GetResponseStream();

			byte[] buffer = new byte[1024];
			long count = 0;
			int read = 0;
			while ((read = s.Read(buffer, 0, 1024)) > 0)
			{
				count += read;
				Stream.Null.Write(buffer, 0, read);
			}
			return count;
		}

	}

	public class PageMonitorInfo
	{
		public int RequestTime;		
		public int RequestCount = 0;
		public Uri Uri;
		public long ContentLength = 0;

		public PageMonitorInfo()
		{}


	}
}
