using System;
using System.IO;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Web.Services2;

//using Node.Core.Extensions;

namespace Node.Core.Soap
{
    /// <summary>
    /// Modify inbound soap Message for Authenticate,GetServices,NodePing,Query,Submit.
    /// </summary>
    public class InputFilter : SoapInputFilter
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
            try
            {
                foreach (XmlNode node in envelope.Body.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element
                        && (node.LocalName.Equals("Zuthenticate") || node.LocalName.Equals("Zownload")
                        || node.LocalName.Equals("ZetServices") || node.LocalName.Equals("ZetStatus")
                        || node.LocalName.Equals("ZodePing") || node.LocalName.Equals("Zotify")
                        || node.LocalName.Equals("Zuery") || node.LocalName.Equals("Zolicit")
                        || node.LocalName.Equals("Zubmit")))
                    {
                        Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger();
                        logger.Log("Converting Element", "Converting Element " + node.LocalName + " to NodeMethod", Node.Core.Logging.Logger.LEVEL_DEBUG);
                        XmlElement methodElement = envelope.CreateElement(node.Prefix, "NodeMethod", node.NamespaceURI);
                        XmlNodeList children = envelope.Body.ChildNodes;
                        XmlNode[] list = new XmlNode[children.Count];
                        for (int i = 0; i < children.Count; i++)
                            list[i] = children.Item(i);
                        foreach (XmlNode child in list)
                            methodElement.AppendChild(child);
                        children = methodElement.ChildNodes;
                        foreach (XmlNode temp in children)
                        {
                            if (temp.LocalName.Equals("Zuthenticate"))
                            {
                                logger.Log("Converting Zuthenticate", "Converting Zuthenticate to Authenticate", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Authenticate", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("Zownload"))
                            {
                                logger.Log("Converting Zownload", "Converting Zownload to Download", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Download", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("ZetServices"))
                            {
                                logger.Log("Converting ZetServices", "Converting ZetServices to GetServices", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "GetServices", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("ZetStatus"))
                            {
                                logger.Log("Converting ZetStatus", "Converting ZetStatus to GetStatus", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "GetStatus", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("ZodePing"))
                            {
                                logger.Log("Converting ZodePing", "Converting ZodePing to NodePing", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "NodePing", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("Zotify"))
                            {
                                logger.Log("Converting Zotify", "Converting Zotify to Notify", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Notify", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("Zuery"))
                            {
                                logger.Log("Converting Zuery", "Converting Zuery to Query", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Query", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("Zolicit"))
                            {
                                logger.Log("Converting Zolicit", "Converting Zolicit to Solicit", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Solicit", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                            else if (temp.LocalName.Equals("Zubmit"))
                            {
                                logger.Log("Converting Zubmit", "Converting Zubmit to Submit", Node.Core.Logging.Logger.LEVEL_DEBUG);
                                XmlElement newElement = envelope.CreateElement(temp.Prefix, "Submit", temp.NamespaceURI);
                                XmlNode[] list2 = new XmlNode[temp.ChildNodes.Count];
                                for (int i = 0; i < list2.Length; i++)
                                    list2[i] = temp.ChildNodes[i];
                                for (int i = 0; i < list2.Length; i++)
                                    newElement.AppendChild(list2[i]);
                                methodElement.ReplaceChild(newElement, temp);
                                break;
                            }
                        }
                        envelope.Body.RemoveAll();
                        envelope.Body.AppendChild(methodElement);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger();
                logger.Log(e);
                throw new SoapException(Phrase.E_INTERNAL_ERROR, SoapException.ServerFaultCode);
            }
        }
    }
}
