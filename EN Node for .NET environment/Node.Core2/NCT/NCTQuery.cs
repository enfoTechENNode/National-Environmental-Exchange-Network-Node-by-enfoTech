using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

using Node.Core.API;
using Node.Core.Document;
using Node.Core.Biz.Interfaces.Query;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core2.Requestor;
using Node.Lib.Utility;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace Node.Core2.NCT
{
    /// <summary>
    /// TestQuery class provides a web service sample to process the Query
    /// </summary>
    public class NCTQuery : IProcess
    {

        #region IProcess Members

        public string Execute(string token, string request, int rowID, int maxRows, string[] parameters, ProcParam param)
        {
            string xmlResult = "";
            
            XmlDocument doc = new XmlDocument();
            XmlNode result = doc.CreateElement("QueryResult", "http://www.exchangenetwork.net/schema/NCT/1");

            int iStart = rowID;
            int iEnd = 10;
            int iCount = 0;

            if (maxRows > 0 && maxRows < 11)
            {
                iEnd = maxRows;
            }
            else
            {
                iEnd = 10;
            }

            for (int i = iStart; i < iEnd ; i++)
            {
                int j = i + 1;
                XmlNode row = doc.CreateElement("row");
                row.InnerText = "Row " + j + " text";
                row.Attributes.RemoveAll();
                result.AppendChild(row);
                iCount++;
            }

            doc.AppendChild(result);

            ResultSetType ResultSet = new ResultSetType();
            DocumentFormatType docType = DocumentFormatType.XML;
            bool bFormat = false;

            foreach (string p in parameters)
            {
                if (p == "zipped")
                {
                    bFormat = true;
                    docType = DocumentFormatType.ZIP;
                    break;
                }
            }

            ResultSet.rowId = rowID.ToString();
            ResultSet.results = new GenericXmlType();
            ResultSet.results.format = docType;

            XmlNode[] resultDoc = new XmlNode[1];

            if (!bFormat)
            {
                resultDoc[0] = (XmlNode)doc.DocumentElement;
            }
            else
            {
                UTF8Encoding en = new UTF8Encoding();
                WinZip zip = new WinZip();
                Hashtable ht = new Hashtable();
                ht.Add(request, en.GetBytes(doc.OuterXml));
                byte[] content = zip.CreateZip(ht);
                string s = System.Convert.ToBase64String(content);

                resultDoc[0] = doc.CreateElement("ZippedContent");
                //System.Security.Cryptography.ToBase64Transform ec = new System.Security.Cryptography.ToBase64Transform();                             
                //resultDoc[0].InnerText = zip.CreateZip(ht);
                resultDoc[0].InnerText = s;
            }

            ResultSet.results.Any = resultDoc;
            ResultSet.rowCount = iCount.ToString();

            if (maxRows < 10 && maxRows > 0)
                ResultSet.lastSet = false;
            else
                ResultSet.lastSet = true;

            xmlResult = GetXML(ResultSet);

            return xmlResult;                
        }   

        #endregion

        private string GetXML(object obj)
        {

            string sXML = "";
            XmlSerializer mySerializer = new XmlSerializer(obj.GetType());

            MemoryStream ms = new MemoryStream();
            StreamWriter myWriter = new StreamWriter(ms);
            mySerializer.Serialize(myWriter, obj);

            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            sXML = sr.ReadToEnd();
            myWriter.Close();
            return sXML;
        }
    }
}

