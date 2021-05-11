#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		EmailTemplate.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/27/2005 Danwen Sun Creation
// 
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;

using Node.Lib.Utility;
//using EAF.Domain.DataSearch;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Represents the EmailTemplate class.
	/// </summary>
	public class EmailTemplate
	{
		/// <summary>
		/// Constant value for HTML format.
		/// </summary>
		public const string MAIL_FORMAT_HTML = Email.MAIL_FORMAT_HTML;
		/// <summary>
		/// Constant value for Text format.
		/// </summary>
		public const string MAIL_FORMAT_TEXT = Email.MAIL_FORMAT_TEXT;
		/// <summary>
		/// Constant value for SPLIT.
		/// </summary>
		public const string SPLIT = ";";
        /// <summary>
        /// Constant value for active status.
        /// </summary>
        public const string STATUS_ACTIVE = "A";
        /// <summary>
        /// Constant value for inactive status.
        /// </summary>
        public const string STATUS_INACTIVE = "I";
	
		private ArrayList arProperties = null;
		private ArrayList arColumns = null;
		private ArrayList arAttachments = null;
		private XmlNode tempNode = null;
		private Hashtable htBookMarks = null;

		/// <summary>
		/// Initializes an EmailTemplate object.
		/// </summary>
		protected internal EmailTemplate(XmlNode node)
		{
			this.tempNode = node;
            //XmlNode dataSearch = this.tempNode.SelectSingleNode(".//DataSearch");
            //if (dataSearch != null)
            //{
            //    SearchEngine engine = new SearchEngine(dataSearch);
            //    this.arProperties = engine.GetAllSearchProperties();
            //    this.arColumns = engine.SearchColumns;
            //}
            //else
            //{
				this.arProperties = new ArrayList();
				this.arColumns = new ArrayList();
            //}
		}

		#region public properties
		/// <summary>
		/// Gets the SearchProperties.
		/// </summary>
		public ArrayList SearchProperties
		{
			get { return this.arProperties; }
		}

		/// <summary>
		/// Gets the SearchColumns.
		/// </summary>
		public ArrayList SearchColumns
		{
			get { return this.arColumns; }
		}

		/// <summary>
		/// Gets or sets the name of template.
		/// </summary>
		public string TemplateName
		{
			get { return this.tempNode.Attributes.GetNamedItem("name").Value; }
			set { this.tempNode.Attributes.GetNamedItem("name").Value = value; }
		}

		/// <summary>
		/// Gets or sets the status of template.
		/// </summary>
		public string Status
		{
			get { return this.tempNode.Attributes.GetNamedItem("status").Value; }
			set { this.tempNode.Attributes.GetNamedItem("status").Value = value; }
		}

		/// <summary>
		/// Gets or sets the From email account.
		/// </summary>
		public string From
		{
			get { return this.tempNode.SelectSingleNode("./From").InnerText; }
			set { this.tempNode.SelectSingleNode("./From").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the to list. Use ';' as spliter.
		/// </summary>
		public string ToList
		{
			get { return this.tempNode.SelectSingleNode("./ToList").InnerText; }
			set { this.tempNode.SelectSingleNode("./ToList").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the cc list. Use ';' as spliter.
		/// </summary>
		public string CcList
		{
			get { return this.tempNode.SelectSingleNode("./CcList").InnerText; }
			set { this.tempNode.SelectSingleNode("./CcList").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the bcc list. Use ';' as spliter.
		/// </summary>
		public string BccList
		{
			get { return this.tempNode.SelectSingleNode("./BccList").InnerText; }
			set { this.tempNode.SelectSingleNode("./BccList").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		public string Subject
		{
			get { return this.tempNode.SelectSingleNode("./Subject").InnerText; }
			set { this.tempNode.SelectSingleNode("./Subject").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		public string Content
		{
			get { return this.tempNode.SelectSingleNode("./Content").InnerText; }
			set { this.tempNode.SelectSingleNode("./Content").InnerText = value; }
		}

		/// <summary>
		/// Gets or sets the body format. The value should be like 'TEXT','HTML'.
		/// </summary>
		public string BodyFormat
		{
			get
			{
				XmlNode format = this.tempNode.SelectSingleNode("./Content").Attributes.GetNamedItem("format");
				if (format != null)
					return format.Value;
				else
					return MAIL_FORMAT_TEXT;
			}
			set
			{
				XmlNode format = this.tempNode.SelectSingleNode("./Content").Attributes.GetNamedItem("format");
				if (format != null)
					format.Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the email attachments. The elements in ArrayList should be file path or <see cref="System.Net.Mail.Attachment">Attachment</see> objects.
		/// </summary>
		public ArrayList Attachment
		{
			get { return this.arAttachments; }
			set { this.arAttachments = value; }
		}

		/// <summary>
		/// Gets or sets the Book marks in Hashtable. The key in the Book Marks is the HeaderText defined in the SearchResult column.
		/// </summary>
		public Hashtable BookMarks
		{
			get
			{
				if (this.htBookMarks != null)
					return this.htBookMarks;

				this.htBookMarks = new Hashtable();
                //// uses data search to search data.
                //if (this.SearchProperties.Count > 0 && this.SearchColumns.Count > 0)
                //{
                //    SearchEngine engine = new SearchEngine(this.SearchProperties, this.SearchColumns);
                //    DataSet ds = engine.Search();
                //    foreach (SearchColumn column in this.SearchColumns)
                //    {
                //        string val = "";
                //        if (ds.Tables[0].Rows.Count > 0)
                //        {
                //            val = ds.Tables[0].Rows[0][column.ColumnName] + "";
                //            if (column.IsDateTime)
                //                val = DateTime.Parse(val).ToString(column.DatetimeFormat);
                //        }
                //        this.htBookMarks.Add(column.HeaderText, val);
                //    }
                //}
				return this.htBookMarks;
			}
		}

        /// <summary>
        /// Gets TemplateNode. It is an XmlNode.
        /// </summary>
        public XmlNode TemplateNode
        {
            get { return this.tempNode; }
        }
		#endregion

	}
}
