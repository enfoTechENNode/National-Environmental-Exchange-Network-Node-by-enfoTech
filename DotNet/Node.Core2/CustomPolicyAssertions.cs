using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.Services3.Design;
using Microsoft.Web.Services3;
using System.Xml;

namespace Node.Core2
{
    public class CustomPolicyAssertions:PolicyAssertion
    {
        public override void ReadXml(System.Xml.XmlReader reader, IDictionary<string, Type> extensions)
        {
            reader.Read(); 
        }
        
        public override Microsoft.Web.Services3.SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return new CustomFilter();
        }

        public override Microsoft.Web.Services3.SoapFilter CreateClientOutputFilter(FilterCreationContext context)
        {
            return new CustomFilter();
        }

        public override Microsoft.Web.Services3.SoapFilter CreateServiceInputFilter(FilterCreationContext context)
        {
            return new NCTParameterFilter();
        }

        public override Microsoft.Web.Services3.SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
        {
            return new NCTQueryZippFilter();
        }
    }

    public class CustomHeaderFilter : SoapFilter
    {
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            if (envelope.Body.FirstChild != null && envelope.Header != null  && envelope.Body.FirstChild.Name != "Download" && envelope.Body.FirstChild.Name != "Submit")
            {
                envelope.Header.RemoveAll();
            }
            return SoapFilterResult.Continue;            
        }
    }

    public class NCTParameterFilter : SoapFilter
    {
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            if (envelope.Header != null)
            {
                envelope.Header.RemoveAll();
            }

            if (envelope.Body.FirstChild != null &&(envelope.Body.FirstChild.Name == "Query" || envelope.Body.FirstChild.Name == "Solicit"))
            {
                foreach (XmlNode aNode in envelope.Body.FirstChild.ChildNodes)
                {
                    string[] sName = aNode.Name.Split(':');
                    string pName = "";
                    if (sName.Length == 2)
                    {
                        pName = sName[1];
                    }
                    else
                    {
                        pName = sName[0];
                    }

                    if (pName == "parameters")
                    {
                        foreach (XmlAttribute xa in aNode.Attributes)
                        {
                            if (xa.Name == "parameterType")
                            {
                                xa.InnerText = "";
                                break;
                            }
                        }

                    }
                }
            }
            //else if (envelope.Body.FirstChild.Name == "Download")
            //{
    
            //    foreach (XmlNode aNode in envelope.Body.FirstChild.ChildNodes)
            //    {
            //        if (aNode.Name == "documents")
            //        {
            //            bool bFlag = false;
            //            foreach (XmlNode aNode2 in aNode.ChildNodes)
            //            {
            //                if (aNode2.Name == "documentName")
            //                {
            //                    bFlag = true;
            //                    break;
            //                }   
            //            }
            //            if (!bFlag)
            //            {
            //                XmlNode docname = envelope.CreateElement("documentName");
            //                docname.InnerText = "documentName";
            //                aNode.AppendChild(docname);
            //            }
            //        }
            //    }
            //}
            return SoapFilterResult.Continue;
        }
    }

    public class NCTQueryZippFilter : SoapFilter
    {
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            if (envelope.Header != null)
            {
                envelope.Header.RemoveAll();
            }
            if (envelope.Body.FirstChild != null && envelope.Body.FirstChild.Name == "QueryResponse")
            {
                foreach (XmlNode aNode in envelope.Body.FirstChild.ChildNodes)
                {
                    if (aNode.Name == "results"
                        && aNode.Attributes["format"] != null
                        && aNode.Attributes["format"].InnerText == "ZIP")
                    {
                        string content = aNode.FirstChild.InnerText;
                        aNode.InnerXml = "";
                        aNode.InnerText = content;
                    }
                }
            }
            return SoapFilterResult.Continue;
        }
    }

    public class CustomFilter : SoapFilter
    {
        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            if (envelope.Header != null)
            {
                envelope.RemoveHeader();
            }
            return SoapFilterResult.Continue;
        }
    }


}
