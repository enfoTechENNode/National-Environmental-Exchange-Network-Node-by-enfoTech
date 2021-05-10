using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// DataViewerConfiguration retrieves information for DataView funcationality for Node.Administration.
    /// </summary>
    public class DataViewerConfiguration
    {
        private XElement _Config ;
        private IEnumerable<XElement> _DataFlow;
        /// <summary>
        /// Constructor of DataViewerConfiguration.
        /// </summary>
        public DataViewerConfiguration()
		{
            string sPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data";
            StreamReader sr = new StreamReader(sPath + @"\Config\DataViewerConfig.xml");
            _Config = XElement.Parse(sr.ReadToEnd());
            _DataFlow = _Config.Elements("DataFlow");
            sr.Close();
		}
        /// <summary>
        /// Get Table Configuration By DataFlow Name
        /// </summary>
        /// <param name="sDataFlow">Name of DataFlow.</param>
        /// <returns>XElement contains partial XML related to specified dataflow.</returns>
        public XElement GetTablesConfigByDataFlow(string sDataFlow)
        {
            XElement result = null;

            IEnumerable<XElement> dataflow = 
            from el in _DataFlow
            where (string)el.Attribute("Name") == sDataFlow
            select el;

            foreach (XElement el in dataflow)
            {
                result = el;
                break;
            }

            return result;
        }
        /// <summary>
        /// Get Tables information by specified DataFlow Name.
        /// </summary>
        /// <param name="sDataFlow">Name of DataFlow.</param>
        /// <returns>A list of XElement contains table information</returns>
        public IEnumerable<XElement> GetTablesByDataFlow(string sDataFlow)
        {
            XElement dataflow = GetTablesConfigByDataFlow(sDataFlow);
            IEnumerable<XElement> tables = dataflow.Elements("Tables").Elements("Table");
            return tables;
        }
        /// <summary>
        /// Get Cascade Tables by specified table.
        /// </summary>
        /// <param name="table">XElemnt contains table information</param>
        /// <returns>A list XElement contains cascade table information</returns>
        public IEnumerable<XElement> GetCascadeTables(XElement table)
        {
            return table.Element("CascadeTables").Elements("table");
        }
        /// <summary>
        /// Get Table Configuration by DataFlow and Table Name.
        /// </summary>
        /// <param name="sDataFlow">Name of DataFlow</param>
        /// <param name="sTableName">Name of Table</param>
        /// <returns>XElement contain partial XML realted to Table.</returns>
        public XElement GetTableByDataFlow(string sDataFlow, string sTableName)
        {
            XElement outputTable = null;
            IEnumerable<XElement> tables = GetTablesByDataFlow(sDataFlow);
            foreach(XElement ex in tables)
                if (ex.Element("Name").Value == sTableName)
                {
                    outputTable = ex;
                    break;
                }
            return outputTable;
        }
        /// <summary>
        /// Indicator for Table has Child Table.
        /// </summary>
        /// <param name="sDataFlow">Name of DataFlow</param>
        /// <param name="sTableName">Name of Table</param>
        /// <returns>True if the table has child table.</returns>
        public bool IsTableHasChild(string sDataFlow,string sTableName)
        {
            bool bFlag = false;
            IEnumerable<XElement>  tables = GetTablesByDataFlow(sDataFlow);

            IEnumerable<XElement> tests =
            from el in tables
            where (string)el.Element("Name") == sTableName
            select el;
            int i = 0;
            foreach (XElement el in tests)
                i++;
            if (i > 0)
                bFlag = true;
 
            return bFlag;
        }
        /// <summary>
        /// Get Connection String by DataFlow
        /// </summary>
        /// <param name="sDataFlow">Name of DataFlow</param>
        /// <returns>Database Connection string</returns>
        public List<string> GetConnectionStringByDataFlow(string sDataFlow)
        {
            List<string> connStr = new List<string>();
            XElement dataflow = GetTablesConfigByDataFlow(sDataFlow);
            connStr.Add(dataflow.Element("DBConnection").Element("ProviderName").Value);
            connStr.Add(dataflow.Element("DBConnection").Element("ConnectionString").Value);
            return connStr;
        }
        /// <summary>
        /// Get DataFlow Collection.    
        /// </summary>
        public IEnumerable<XElement> DataFlowCollection
        {
            get { return _DataFlow; }
        }
    }
}
