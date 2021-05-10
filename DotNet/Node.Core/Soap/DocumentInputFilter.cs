using System;
using System.Collections;
using System.Xml;
using Microsoft.Web.Services2;

namespace Node.Core.Soap
{
    /// <summary>
    /// Modify inbound soap Message for submit operation.
    /// </summary>
    public class DocumentInputFilter : SoapInputFilter
    {
        /// <summary>
        /// The indicator to process header of soap message. 
        /// </summary>
        /// <param name="header">The header of soap Message.</param>
        /// <param name="context">The constext of soap message.</param>
        /// <returns>True</returns>
        protected override bool CanProcessHeader(XmlElement header, SoapContext context)
        {
            return true;
        }
        /// <summary>
        /// The entry point of soap message modification.
        /// </summary>
        /// <param name="envelope">The soapenvelope of soap message.</param>
        public override void ProcessMessage(SoapEnvelope envelope)
        {
            Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger();
            try
            {

                XmlNodeList nodes = envelope.SelectNodes(".//content");
                if (nodes != null)
                {
                    Hashtable hrefs = new Hashtable();
                    foreach (XmlNode node in nodes)
                    {
                        XmlAttribute xmlAttr = node.Attributes["href"];
                        if (xmlAttr != null)
                        {
                            XmlAttribute newAttr = node.OwnerDocument.CreateAttribute(node.ParentNode.Prefix, "href", node.ParentNode.NamespaceURI);
                            newAttr.Value = xmlAttr.Value;
                            node.Attributes.Remove(xmlAttr);
                            //node.ParentNode.Attributes.Append(newAttr);
                            foreach (XmlNode child in node.ParentNode.ChildNodes)
                            {
                                if (child.LocalName.ToLower() == "name")
                                {
                                    hrefs.Add(child.InnerText, newAttr.Value);
                                    break;
                                }
                            }
                        }
                    }
                    envelope.Context.Add("hrefs", hrefs);
                }
            }
            catch (Exception e)
            {
                logger.Log(e);
            }

            //Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger();
            //try
            //{
            //    XmlNodeList contentNodes = envelope.GetElementsByTagName("content");
            //    Hashtable hrefs = new Hashtable();
            //    foreach (XmlNode content in contentNodes)
            //    {
            //        if (content.Attributes["href"] != null && content.Attributes["href"].Value.Trim() != "")
            //        {
            //            foreach (XmlNode child in content.ParentNode.ChildNodes)
            //            {
            //                if (child.LocalName.ToLower() == "name")
            //                {
            //                    hrefs.Add(child.InnerText, content.Attributes["href"].Value);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    envelope.Context.Add("hrefs", hrefs);
            //}
            //catch (Exception e)
            //{
            //    logger.Log(e);
            //}
        }
    }
}
