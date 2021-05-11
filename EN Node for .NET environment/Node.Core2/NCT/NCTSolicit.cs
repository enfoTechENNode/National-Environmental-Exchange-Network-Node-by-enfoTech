using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Linq;
using System.Text;
using System.Xml;

using Node.Core.API;
using Node.Core.Document;
using Node.Core.Biz.Interfaces.Solicit;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Util;

namespace Node.Core2.NCT
{
    public class NCTSolicit : IProcess
    {
        #region IProcess Members

        public NodeDocument[] Execute(string token, string returnURL, string request, string[] parameters, ProcParam param)
        {

            XmlDocument doc = new XmlDocument();
            XmlNode result = doc.CreateElement("QueryResult", "http://www.exchangenetwork.net/schema/NCT/1");

            for (int i = 0; i < 11; i++)
            {
                int j = i + 1;
                XmlNode row = doc.CreateElement("row");
                row.InnerText = "Row " + j + " text";
                row.Attributes.RemoveAll();
                result.AppendChild(row);
            }

            doc.AppendChild(result);

            //bool bZip = true;
            //if (parameters.Length > 0 && parameters[0].Trim().ToUpper() == "ZIPPED")
            //{
            //    bZip = true;
            //}

            try
            {
                NodeDocument[] retDocs = new NodeDocument[2];
                retDocs[0] = new NodeDocument();
                retDocs[0].name = "Node20.Report";
                retDocs[0].type = "XML";
                retDocs[0].content = System.Text.ASCIIEncoding.UTF8.GetBytes(doc.OuterXml);
                retDocs[1] = new NodeDocument();
                retDocs[1].name = "Node20.Processed";
                retDocs[1].type = "XML";
                retDocs[1].content = System.Text.ASCIIEncoding.UTF8.GetBytes(doc.OuterXml);

                return retDocs;
            }
            catch (Exception e)
            {
                throw new SoapException(e.ToString(), SoapException.ServerFaultCode);
            }

        }

        #endregion
    }
}
