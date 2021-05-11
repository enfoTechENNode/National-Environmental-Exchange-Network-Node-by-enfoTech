using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;
using Node.Lib.Utility;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.Elements
{
	/// <summary>
	/// This class is meant to be used by <see cref="EAF.Lib.UI.WebUtils.TextResource"/> class. You are not supposed to use this class directly in your page.
	/// </summary>
	[Serializable]
	public class TextResourceProvider
	{
		//***********************************************************************
		// private members
		//***********************************************************************
		private string _rootNodeName = "textResource";
		private Hashtable _hash = new Hashtable();
		private Hashtable _glbHash = new Hashtable();
		private string _splitter = ".";
		private bool _isIgnoreKeyCase = false;

		//***********************************************************************
		// constructor
		//***********************************************************************

		/// <summary>
		/// Initializes a new instance of the TextResourceProvider class.
		/// </summary>
		/// <param name="filepath">TextResource.xml filepath</param>
		/// <param name="ignoreKeyCase">Ignore Key Cases</param>
		public TextResourceProvider(string filepath, bool ignoreKeyCase)
		{
			_isIgnoreKeyCase = ignoreKeyCase;

			if (Path.GetFileName(filepath) != String.Empty)
			{
				InitializeByNode(XmlUtility.GetRequiredXmlNode(filepath, _rootNodeName));
				//XmlTree tree = new XmlTree(filepath);
				//_splitter = tree.RootNode.GetAttribute("splitter");
				//RcrsvGenerateHash(tree.RootNode, "");
			}
			else
			{
				// this is when filepath is a directory
				DirectoryInfo di = new DirectoryInfo(filepath);
				FileInfo[] fis = di.GetFiles("*.txtsrc");
				foreach (FileInfo fi in fis)
				{
					InitializeByNode(XmlUtility.GetRequiredXmlNode(fi.FullName, _rootNodeName));
					//XmlTree tree = new XmlTree(fi.FullName);
					//_splitter = tree.RootNode.GetAttribute("splitter");
					//RcrsvGenerateHash(tree.RootNode, "");
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the TextResourceProvider class.
		/// </summary>
		/// <param name="xmlRootNode">XmlNode of TextResource.xml</param>
		public TextResourceProvider(XmlNode xmlRootNode)
		{
			InitializeByNode(xmlRootNode);
		}

		//***********************************************************************
		// attributes
		//***********************************************************************

		/// <summary>
		/// Gets the splitter string, read from TextResource.xml file.
		/// </summary>
		public string KeySplitter
		{
			get { return _splitter; }
		}

		//***********************************************************************
		// public methods
		//***********************************************************************

		/// <summary>
		/// Get value based on key
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>Value</returns>
		public string GetValue(string key)
		{
			if (_isIgnoreKeyCase) key = key.ToUpper();

			object o = _hash[key];
			if (o == null)
				return "(KEY ERROR ==> " + key + ")";
			else
				return (string)o;
		}

		/// <summary>
		/// Get global value based on key
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>Value</returns>
		public string GetGlobalValue(string key)
		{
			if (_isIgnoreKeyCase) key = key.ToUpper();

			object o = _glbHash[key];
			if (o == null)
				return "(KEY ERROR ==> " + key + ")";
			else
				return (string)o;
		}

		//***********************************************************************
		// private methods
		//***********************************************************************
		private void InitializeByNode(XmlNode xmlRootNode)
		{
			XmlTree tree = new XmlTree(xmlRootNode);
			string splitter = tree.RootNode.GetAttribute("splitter");

			GenerateHash(tree.RootNode, "", splitter);
			
			//GenerateHash();
			//RcrsvGenerateHash(tree.RootNode, "");


		}
		//private void GenerateHash(XmlTreeNode node)
		//{
		//    RcrsvGenerateHash(this.tree.RootNode, "");
		//}

		private void GenerateHash(XmlTreeNode node, string lvlKey, string splitChar)
		{
			if (node.ChildNodes != null && node.ChildNodes.Count > 0)
			{
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					XmlTreeNode xtn = node.ChildNodes[i];
					if ("" + xtn.NodeName.ToLower() == "globalscope")
					{
						RcrsvGenerateHash(xtn, _glbHash, "", splitChar);
					}
					else if ("" + xtn.NodeName.ToLower() == "pagescope")
					{
						RcrsvGenerateHash(xtn, _hash, "", splitChar);
					}
				}
			}
		}


		private void RcrsvGenerateHash(XmlTreeNode node,  Hashtable ht, string lvlKey, string splitChar)
		{
			if (node.ChildNodes != null && node.ChildNodes.Count > 0)
			{
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					string currentLvlKey = lvlKey;
					XmlTreeNode xtn = node.ChildNodes[i];

					if (xtn.ChildNodes == null || xtn.ChildNodes.Count == 0)
					{
						string tmpKey = "";
						if (currentLvlKey == "")
							tmpKey = xtn.GetAttribute("key");
						else
							tmpKey = currentLvlKey + splitChar + xtn.GetAttribute("key");

						if (_isIgnoreKeyCase) tmpKey = tmpKey.ToUpper();
						
						if (ht.ContainsKey(tmpKey))
							throw (new Exception("(TextResourceProvider ERROR) Duplicated Key Found: " + tmpKey));
						else
							ht.Add(tmpKey, xtn.GetAttribute("value"));
					}
					else
					{
						if (currentLvlKey == "")
							currentLvlKey = xtn.GetAttribute("key");
						else
							currentLvlKey += splitChar + xtn.GetAttribute("key");
					}

					RcrsvGenerateHash(xtn, ht, currentLvlKey, splitChar);
				}
			}
		}

	}
}
