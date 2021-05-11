using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;
using System.Configuration; //natalius

using Node.Lib.Data;
using Node.Lib.Utility;
using Node.Xml;

namespace Node.Core.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class NodeXMLResult
    {
        /*natalius*/
        private string dataFlow = null;
        private string pathMapper = null;
        private string pathTemplate = null;
        private readonly string postfixMapperFile = "ToXMLMapper.xml";
        private readonly string postfixTemplateFile = "ToXMLTemplate.xml";
        private string mapperFileName = null;
        private string templateFileName = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataFlowParam"></param>
        public NodeXMLResult(string dataFlowParam)
        {
            this.dataFlow = dataFlowParam;
            string path = ConfigurationManager.AppSettings[dataFlow].Trim();
            path = (path.EndsWith("\\")) ? path : path += "\\";
            this.pathMapper = path;
            this.pathTemplate = path;
            this.mapperFileName = this.dataFlow + this.postfixMapperFile;
            this.templateFileName = this.dataFlow + this.postfixTemplateFile;
        }
        /// <summary>
        /// The method set file name for mapper file and template file
        /// </summary>
        /// <param name="fileName"></param>
        public void setFileName(string fileName) {
            this.mapperFileName = (String.IsNullOrEmpty(fileName)) ? this.mapperFileName : (fileName + this.mapperFileName);
            this.templateFileName = (String.IsNullOrEmpty(fileName)) ? this.templateFileName : (fileName + this.templateFileName);
        }
        /// <summary>
        /// Get XML output result.
        /// </summary>
        /// <param name="mtQueryVariable">Hashtable Query Condition for Mapper/Template</param>
        /// <returns></returns>
        public string getXMLOutput(Hashtable mtQueryVariable)
        {
            StreamReader sr = null;
            String mapperString = null;
            XmlDocument template = null;
            string xmlResultString = null;
            try
            {
                // Read Mapper
                sr = new StreamReader(this.pathMapper + this.mapperFileName);
                mapperString = sr.ReadToEnd();
                sr.Close();
                // Read Template
                sr = new StreamReader(this.pathTemplate + this.templateFileName);
                template = new XmlDocument();
                template.LoadXml(sr.ReadToEnd());
            }
            catch (Exception e)
            {
                //throw new SoapException("Could not load Mapper and Template files." + e.Message, SoapException.ServerFaultCode);
                xmlResultString = "Could not load Mapper and Template files." + Environment.NewLine + e.ToString();
            }
            finally
            {
                if (sr != null)
                    sr.Close();

                if (mapperString != null && template != null)
                {
                    try
                    {
                        XmlDataComposer composer = new XmlDataComposer(mapperString, template);
                        // Set Mapper Variable
                        composer.KeyValues = mtQueryVariable;
                        // Set DB / DataSource
                        composer.DB = new DBAdapter(this.dataFlow);
                        // Generate XML Result
                        xmlResultString = composer.Generate();
                    }
                    catch (Exception e)
                    {
                        //throw new SoapException("Could not Generate XML Files." + e.ToString(), SoapException.ServerFaultCode);
                        xmlResultString = "Could not Generate XML Files." + Environment.NewLine + e.ToString();
                    }
                    finally
                    {
                        mapperString = null;
                        template = null;
                    }
                }
            }
            return xmlResultString;
        }

        ///// <summary>
        ///// Get XML Result
        ///// </summary>
        ///// <param name="dataFlow">String Data Flow Name</param>
        ///// <param name="queryCondition">Hashtable Query Condition for Mapper/Template</param>
        ///// <param name="spName">String Stored Procedure Name</param>
        ///// <param name="spParameters">ArrayList Stored Procedure Parameter List</param>
        ///// <param name="useMapper">Boolean Use Mapper/Template or Stored Procedure</param>
        ///// <returns>String XML formatted</returns>
        //public static string GetXMLResult(string dataFlow, Hashtable mtQueryVariable,
        //    string spName, ArrayList spParameters, bool useMapper)
        //{
        //    string xmlResultString = "";
        //    if (useMapper)
        //    {
        //        string mapper = null;
        //        XmlDocument template = null;
        //        StreamReader sr = null;

        //        try
        //        {
        //            // Read Mapper
        //            string[] mtPath = NodeUtility.GetMTFilePath(dataFlow);
        //            sr = new StreamReader(mtPath[0].ToString());
        //            mapper = sr.ReadToEnd();
        //            sr.Close();
        //            // Read Template
        //            sr = new StreamReader(mtPath[1].ToString());
        //            template = new XmlDocument();
        //            template.LoadXml(sr.ReadToEnd());
        //        }
        //        catch (Exception e)
        //        {
        //            //throw new SoapException("Could not load Mapper and Template files." + e.Message, SoapException.ServerFaultCode);
        //            xmlResultString = "Could not load Mapper and Template files." + Environment.NewLine + e.ToString();
        //        }
        //        finally
        //        {
        //            if (sr != null)
        //                sr.Close();
        //        }

        //        if (mapper != null && template != null)
        //        {
        //            try
        //            {
        //                XmlDataComposer composer = new XmlDataComposer(mapper, template);
        //                // Set Mapper Variable
        //                composer.KeyValues = mtQueryVariable;
        //                // Set DB / DataSource
        //                composer.DB = new DBAdapter(dataFlow);
        //                // Generate XML Result
        //                xmlResultString = composer.Generate();
        //            }
        //            catch (Exception e)
        //            {
        //                //throw new SoapException("Could not Generate XML Files." + e.ToString(), SoapException.ServerFaultCode);
        //                xmlResultString = "Could not Generate XML Files." + Environment.NewLine + e.ToString();
        //            }
        //            finally
        //            {
        //                mapper = null;
        //                template = null;
        //            }
        //        }
        //        else
        //        {
        //            //throw new SoapException("Could not Load Mapper and Template Files.", SoapException.ServerFaultCode);
        //            xmlResultString = "Could not Load Mapper and Template Files.";
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            // Set DB / DataSource
        //            DBAdapter dba = new DBAdapter(dataFlow);
        //            DataSet ds = new DataSet();
        //            ds = dba.CallProcedureDataSet(spName.ToUpper(), spParameters);
        //            if (ds != null && ds.Tables.Count > 0)
        //                xmlResultString = ds.Tables[0].Rows[0][0].ToString();
        //            else
        //                xmlResultString = "No XML file generated.";
        //            //throw new SoapException("No XML file generated.", SoapException.ServerFaultCode);
        //        }
        //        catch (Exception e)
        //        {
        //            //throw new SoapException("No XML file generated.", SoapException.ServerFaultCode);
        //            xmlResultString = "Could not Generate XML Files." + Environment.NewLine + e.ToString();
        //        }
        //    }

        //    if (string.IsNullOrEmpty(xmlResultString))
        //        xmlResultString = "No XML file generated.";
        //    //throw new SoapException("Could not Generate XML Files.", SoapException.ServerFaultCode);

        //    return xmlResultString;
        //}

        /// <summary>
        /// Get XML Result.
        /// </summary>
        /// <param name="dataFlow">String Data Flow Name</param>
        /// <param name="mtQueryVariable">Hashtable Query Condition for Mapper/Template</param>
        /// <param name="spName">String Stored Procedure Name</param>
        /// <param name="spParameters">ArrayList Stored Procedure Parameter List</param>
        /// <param name="useMapper">Boolean Use Mapper/Template or Stored Procedure</param>
        /// <returns>String XML formatted</returns>
        public static string GetXMLResult(string dataFlow, Hashtable mtQueryVariable,
            string spName, ArrayList spParameters, bool useMapper)
        {
            string xmlResultString = "";
            if (useMapper)
            {
                string mapper = null;
                XmlDocument template = null;
                StreamReader sr = null;
                string sPath = "";
                try
                {
                    // Read Mapper
                    string[] mtPath = NodeUtility.GetMTFilePath(dataFlow);
                    sPath = mtPath[0] + ":" + mtPath[1];
                    sr = new StreamReader(mtPath[0].ToString());
                    mapper = sr.ReadToEnd();
                    sr.Close();
                    // Read Template
                    sr = new StreamReader(mtPath[1].ToString());
                    template = new XmlDocument();
                    template.LoadXml(sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    //throw new SoapException("Could not load Mapper and Template files." + e.Message, SoapException.ServerFaultCode);
                    xmlResultString = "Could not load Mapper and Template files:" + sPath + Environment.NewLine + e.ToString();
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }

                if (mapper != null && template != null)
                {
                    try
                    {
                        XmlDataComposer composer = new XmlDataComposer(mapper, template);
                        // Set Mapper Variable
                        composer.KeyValues = mtQueryVariable;
                        // Set DB / DataSource
                        composer.DB = new DBAdapter(dataFlow);
                        // Generate XML Result
                        xmlResultString = composer.Generate();
                    }
                    catch (Exception e)
                    {
                        //throw new SoapException("Could not Generate XML Files." + e.ToString(), SoapException.ServerFaultCode);
                        xmlResultString = "Could not Generate XML Files." + Environment.NewLine + e.ToString();
                    }
                    finally
                    {
                        mapper = null;
                        template = null;
                    }
                }
                else
                {
                    //throw new SoapException("Could not Load Mapper and Template Files.", SoapException.ServerFaultCode);
                    xmlResultString = "Could not Load Mapper and Template Files.";
                }
            }
            else
            {
                try
                {
                    // Set DB / DataSource
                    DBAdapter dba = new DBAdapter(dataFlow);
                    DataSet ds = new DataSet();
                    ds = dba.CallProcedureDataSet(spName.ToUpper(), spParameters);
                    if (ds != null && ds.Tables.Count > 0)
                        xmlResultString = ds.Tables[0].Rows[0][0].ToString();
                    else
                        xmlResultString = "No XML file generated.";
                    //throw new SoapException("No XML file generated.", SoapException.ServerFaultCode);
                }
                catch (Exception e)
                {
                    //throw new SoapException("No XML file generated.", SoapException.ServerFaultCode);
                    xmlResultString = "Could not Generate XML Files." + Environment.NewLine + e.ToString();
                }
            }

            if (string.IsNullOrEmpty(xmlResultString))
                xmlResultString = "No XML file generated.";
            //throw new SoapException("Could not Generate XML Files.", SoapException.ServerFaultCode);

            return xmlResultString;
        }

        /// <summary>
        /// Check content whether valid well-formatted XML content
        /// </summary>
        /// <param name="originalString">String Original string to be validate</param>
        /// <returns>Boolean validation result</returns>
        public static bool isValidXMLContent(string originalString)
        {
            bool isValidXML = false;

            XmlDocument xd = new XmlDocument();
            try
            {
                //try to load original string as XML
                xd.LoadXml(originalString);
                isValidXML = true;
            }
            catch (Exception)
            {
                isValidXML = false;
                //throw new SoapException("No XML files generated." + ex.ToString(), SoapException.ServerFaultCode);
            }
            finally
            {
                xd = null;
            }

            return isValidXML;
        }

        /// <summary>
        /// Get Well-Formatted XML string
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string GetWellFormatXML(string originalString)
        {
            string wellFormatXMLString = "";

            StringWriter sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);
            xtw.Indentation = 4;
            xtw.Formatting = Formatting.Indented;
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(originalString);
                xd.WriteTo(xtw);
                xtw.Flush();
                wellFormatXMLString = sw.ToString();
            }
            catch (Exception ex)
            {
                wellFormatXMLString = ex.ToString();
                //throw new SoapException("No XML files generated." + ex.ToString(), SoapException.ServerFaultCode);
            }
            finally
            {
                xd = null;
                xtw.Close();
                xtw = null;
                sw.Close();
                sw = null;
            }

            return wellFormatXMLString;
        }

        /// <summary>
        /// Get zipped result
        /// </summary>
        /// <param name="unzippedResult">String Unzipped string to be zipped</param>
        /// <param name="requestName">String Request name</param>
        /// <param name="transID">String Transactio ID</param>
        /// <returns>Byte[] Zipped byte array</returns>
        public static byte[] GetZippedResult(string unzippedResult, string requestName, string transID)
        {
            byte[] zippedContent = null;

            ASCIIEncoding ae = new ASCIIEncoding();
            Hashtable ht = new Hashtable();
            WinZip wz = new WinZip();

            if (!string.IsNullOrEmpty(unzippedResult))
            {
                if (isValidXMLContent(unzippedResult))
                    ht.Add(requestName + "_" + transID + ".xml", 
                        ae.GetBytes(GetWellFormatXML(unzippedResult)));
                else
                    ht.Add(requestName + "_" + transID + ".txt", 
                        ae.GetBytes(unzippedResult));
            }
            else
            {
                ht.Add(requestName + "_" + transID + ".txt", 
                    ae.GetBytes("No XML file generated."));
            }
            zippedContent = wz.CreateZip(ht);

            return zippedContent;
        }
    }
}
