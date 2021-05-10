using System;
using System.IO;

namespace Node.Core.Document
{
    /// <summary>
    /// This class represents a NodeDocument object that can be used in Node Web Service Requests.
    /// The NodeDocument object uses the DIME protocol when making Node Web Service Requests.
    /// </summary>
    public class NodeDocument
    {
        private string ID = null;
        private string Name = null;
        private string Type = null;
        private Stream Content = null;
        private byte[] docContent = null;

        /// <summary>
        /// Creates a new NodeDocument with empty properties.
        /// The ID of this document will be automaticall created when this constructor is called.
        /// </summary>
        public NodeDocument()
        {
            if (this.ID == null || this.ID.Trim().Equals(""))
                this.ID = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the href of the document that is passed in or supplied during the web service calls.
        /// This value matches the ID of the document.
        /// </summary>
        [System.Xml.Serialization.SoapAttribute("href")]
        public string href
        {
            get { return this.ID; }
            set { this.ID = value; }
        }

        /// <summary>
        /// Gets or sets the name of the document.
        /// </summary>
        [System.Xml.Serialization.SoapElementAttribute("name")]
        public string name
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        /// <summary>
        /// Gets or sets the type of the document.
        /// </summary>
        [System.Xml.Serialization.SoapElementAttribute("type")]
        public string type
        {
            get { return this.Type; }
            set { this.Type = value; }
        }

        /// <summary>
        /// Gets or sets the content of the document.
        /// This method can be used if the content is needed or needs to be set via a byte[] type.
        /// It is recommended that the content property or the Stream property is used instead.
        /// </summary>
        [System.Xml.Serialization.SoapElement("content")]
        public byte[] _content
        {
            get { return this.docContent; }
            set { this.docContent = value; }
        }

        /// <summary>
        /// Gets or sets the content of the document.
        /// This method can be used if the content is needed or needs to be set via a byte[] type.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [System.Xml.Serialization.SoapIgnore]
        public byte[] content
        {
            get
            {
                if (this.Content != null)
                {
                    this.Content.Position = 0;
                    byte[] ret = new byte[this.Content.Length];
                    this.Content.Read(ret, 0, (int)this.Content.Length);
                    return ret;
                }
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.Content = new MemoryStream(value);
                else
                    this.Content = null;
            }
        }


        /// <summary>
        /// Gets or sets the content of the document.
        /// This method can be used if the content is needed or needs to be set via a byte[] type.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        [System.Xml.Serialization.SoapIgnore]
        public Stream Stream
        {
            get { return this.Content; }
            set { this.Content = value; }
        }
    }
}
