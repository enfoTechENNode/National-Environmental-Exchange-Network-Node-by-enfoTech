using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;

namespace Node.Lib.Utility
{

	/// <summary>
	/// Static utility class for Page activity.
	/// </summary>
	public class XmlUtility
	{
		public static XmlNode GetRequiredXmlNode(string xmlfile, string nodeName)
		{
			if (xmlfile == null || xmlfile == "") return null;
			
			XmlDocument xdoc = new XmlDocument();
			try
			{
				xdoc.Load(xmlfile);
			}
			catch (Exception e)
			{
				throw new Exception("Load Xml File Error: " + xmlfile + " -- " + e.Message);
			}

			return GetRequiredXmlNode(xdoc, nodeName);
		}

		public static XmlNode GetRequiredXmlNode(XmlDocument xdoc, string nodeName)
		{
			if (xdoc == null) return null;

			return GetRequiredXmlNode(xdoc.DocumentElement, nodeName);
		}

		public static XmlNode GetRequiredXmlNode(XmlNode xn, string nodeName)
		{
			if (xn == null) return null;

			if (xn.Name == nodeName)
				return xn;
			else
				return xn.SelectSingleNode(".//" + nodeName);
		}

	}
}
