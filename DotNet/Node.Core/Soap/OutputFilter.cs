using System;
using System.Collections;
using System.Xml;
using Microsoft.Web.Services2;

namespace Node.Core.Soap
{
    /// <summary>
    /// Modify outbound soap Message for download operation.
    /// </summary>
    public class OutputFilter : SoapOutputFilter
    {
        /// <summary>
        /// The entry point of soap message modification.
        /// </summary>
        /// <param name="envelope">The soapenvelope of soap message.</param>
        public override void ProcessMessage(SoapEnvelope envelope)
        {
            envelope.Header.RemoveAll();

            /*
            XmlNode nodeMethodResultNode = envelope.Body.SelectSingleNode(".//NodeMethodResult");
            if (nodeMethodResultNode != null)
            {
                XmlDocument temp = new XmlDocument();
                temp.LoadXml(nodeMethodResultNode.InnerText);

                XmlNode responseNode = envelope.Body.ChildNodes[0];
                envelope.Body.RemoveAll();

                XmlElement nodeResponseNode = envelope.CreateElement(responseNode.Prefix, temp.DocumentElement.Name, responseNode.NamespaceURI);
                this.RecursiveAddElement(envelope, temp.DocumentElement, nodeResponseNode, responseNode.Prefix);
                envelope.Body.AppendChild(nodeResponseNode);
            }

            // Query Response
            foreach (XmlNode child in envelope.Body.ChildNodes)
            {
                if (child.LocalName.Equals("QueryResponse"))
                {
                    XmlNode retNode = child.ChildNodes[0];
                    XmlAttribute type = null;
                    if (retNode.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute attr in retNode.Attributes)
                        {
                            if (attr.LocalName.Equals("type"))
                            {
                                type = attr;
                                break;
                            }
                        }
                    }
                    if (type == null)
                    {
                        type = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                        type.Value = "xsd:string";
                        retNode.Attributes.Append(type);
                    }
                }
            }
             */

            if (envelope.InnerXml.Contains("<content xsi:nil=\"true\" />"))
            {
                envelope.InnerXml = envelope.InnerXml.Replace("<content xsi:nil=\"true\" />", "");
            }

            Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger();
            SoapContext context = envelope.Context;
            if (context != null)
            {
                XmlElement element = envelope.DocumentElement;
                XmlNodeList list = element.GetElementsByTagName("NodeDocument", "http://www.ExchangeNetwork.net/schema/v1.0/node.xsd");
                object obj = context["hrefs"];
                if (obj != null)
                {
                    Hashtable hrefs = (Hashtable)obj;
                    XmlNamespaceManager nsMgr = new XmlNamespaceManager(envelope.NameTable);
                    foreach (XmlNode node in list)
                    {
                        XmlNode nameNode = node.SelectSingleNode("name", nsMgr);
                        if (nameNode != null && hrefs.ContainsKey(nameNode.InnerText))
                        {
                            string contentPre = nsMgr.LookupPrefix("http://xml.apache.org/xml-soap");
                            if (contentPre == null)
                            {
                                contentPre = "ap";
                                nsMgr.AddNamespace("ap", "http://xml.apache.org/xml-soap");
                            }
                            XmlNode contentNode = node.SelectSingleNode(contentPre + ":content", nsMgr);
                            if (contentNode == null)
                            {
                                contentNode = envelope.CreateNode(XmlNodeType.Element, "content", "http://xml.apache.org/xml-soap");
                                node.AppendChild(contentNode);
                            }
                            if (contentNode.Attributes["href"] == null)
                            {
                                XmlAttribute hrefAttr = envelope.CreateAttribute("href");
                                hrefAttr.Value = "" + hrefs[nameNode.InnerText];
                                contentNode.Attributes.Append(hrefAttr);
                            }
                            else
                                contentNode.Attributes["href"].Value = "" + hrefs[nameNode.InnerText];
                        }
                    }
                }
            }
        }

        private void RecursiveAddElement(SoapEnvelope envelope, XmlNode parentToRead, XmlNode parentToAddTo, string prefix)
        {
            foreach (XmlNode toRead in parentToRead.ChildNodes)
            {
                XmlElement toAdd = envelope.CreateElement(toRead.Name);
                switch (toRead.Name)
                {
                    case "documents":
                        XmlNodeList items = toRead.SelectNodes("item");
                        XmlAttribute documentsType = envelope.CreateAttribute("soapenc", "arrayType", "http://schemas.xmlsoap.org/soap/encoding/");
                        documentsType.Value = prefix + ":NodeDocument[" + items.Count + "]";
                        toAdd.Attributes.Append(documentsType);
                        break;
                    case "return":
                        if (parentToRead.Name.Equals("GetServicesResponse"))
                        {
                            XmlAttribute typeAttr = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                            typeAttr.Value = "soapenc:Array";
                            toAdd.Attributes.Append(typeAttr);
                            items = toRead.SelectNodes("Item");
                            XmlAttribute arrayTypeAttr = envelope.CreateAttribute("soapenc", "arrayType", "http://schemas.xmlsoap.org/soap/encoding/");
                            arrayTypeAttr.Value = "xsd:string[" + items.Count + "]";
                            toAdd.Attributes.Append(arrayTypeAttr);
                        }
                        break;
                }
                if (toRead.ChildNodes.Count == 0 || (toRead.ChildNodes.Count == 1 && toRead.ChildNodes[0].NodeType == XmlNodeType.Text))
                {
                    toAdd.InnerText = toRead.InnerText;
                    XmlAttribute stringType = null;
                    switch (toRead.Name)
                    {
                        case "return":
                            if (!parentToRead.Name.Equals("GetServicesResponse"))
                            {
                                stringType = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                                stringType.Value = "xsd:string";
                                toAdd.Attributes.Append(stringType);
                            }
                            break;
                        case "name":
                            stringType = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                            stringType.Value = "xsd:string";
                            toAdd.Attributes.Append(stringType);
                            break;
                        case "type":
                            stringType = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                            stringType.Value = "xsd:string";
                            toAdd.Attributes.Append(stringType);
                            break;
                        case "content":
                            if (toRead.Attributes["href"] != null)
                            {
                                XmlAttribute hrefAttr = envelope.CreateAttribute("href");
                                hrefAttr.Value = toRead.Attributes["href"].Value;
                                toAdd.Attributes.Append(hrefAttr);
                            }
                            XmlAttribute binType = envelope.CreateAttribute("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance");
                            binType.Value = "xsd:base64Binary";
                            toAdd.Attributes.Append(binType);
                            break;
                    }
                }
                else if (toRead.ChildNodes.Count > 0)
                    this.RecursiveAddElement(envelope, toRead, toAdd, prefix);
                parentToAddTo.AppendChild(toAdd);
            }
        }
    }
}
