using System;
using System.Configuration;
using System.Text;

namespace Node.Core.Util
{
    /// <summary>
    /// NodeUtility contains reusable function for Node Application.
    /// </summary>
    public class NodeUtility
    {
        /// <summary>
        /// Constructor or NodeUtility
        /// </summary>
        public NodeUtility()
        {
        }

        /// <summary>
        /// Generate Transaction ID
        /// </summary>
        /// <returns></returns>
        public string GenerateTransactionID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            Random rand = new Random();
            UTF8Encoding utf = new UTF8Encoding();
            return "ndlc:" + Convert.ToBase64String(utf.GetBytes("" + rand.Next() + rand.Next() + rand.Next()));
        }

        #region Data Flow
        /// <summary>
        /// Convert Input DateTime String (YYYYMMDD) To DB comparable DateTime String
        /// </summary>
        /// <param name="dateString">User input DateTime String (YYYYMMDD)</param>
        /// <param name="isOracle"></param>
        /// <returns>DB comparable DateTime String</returns>
        public static string DBDateString(string dateString, bool isOracle)
        {
            if (dateString.Length != 8)
            {
                return null;
            }
            else
            {
                string tempDate = "";
                if (isOracle) // Convert to Oracle DataTime type
                {
                    // Get Day
                    tempDate = dateString.Substring(6, 2);
                    // Get Month
                    switch (dateString.Substring(4, 2))
                    {
                        case "01":
                            {
                                tempDate += "-JAN-";
                                break;
                            }
                        case "02":
                            {
                                tempDate += "-FEB-";
                                break;
                            }
                        case "03":
                            {
                                tempDate += "-MAR-";
                                break;
                            }
                        case "04":
                            {
                                tempDate += "-APR-";
                                break;
                            }
                        case "05":
                            {
                                tempDate += "-MAY-";
                                break;
                            }
                        case "06":
                            {
                                tempDate += "-JUN-";
                                break;
                            }

                        case "07":
                            {
                                tempDate += "-JUL-";
                                break;
                            }
                        case "08":
                            {
                                tempDate += "-AUG-";
                                break;
                            }
                        case "09":
                            {
                                tempDate += "-SEP-";
                                break;
                            }
                        case "10":
                            {
                                tempDate += "-OCT-";
                                break;
                            }
                        case "11":
                            {
                                tempDate += "-NOV-";
                                break;
                            }
                        case "12":
                            {
                                tempDate += "-DEC-";
                                break;
                            }
                    }
                }
                else // Convert to MS SQL DataTime type
                {
                    tempDate = dateString.Substring(4, 2) + "/" + dateString.Substring(6, 2) + "/";
                }
                // Get Year
                tempDate += dateString.Substring(0, 4);
                return tempDate;
            }
        }
        
        /// <summary>
        /// Get Mapper file and Template file path
        /// </summary>
        /// <param name="dataFlow">Data Flow defined in web.config file</param>
        /// <returns>String Array</returns>
        public static string[] GetMTFilePath(string dataFlow)
        {
            string path = ConfigurationManager.AppSettings[dataFlow].Trim();

            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }

            string[] temp = new string[] { path + dataFlow + "ToXMLMapper.xml", 
                                            path + dataFlow + "ToXMLTemplate.xml" };
            return temp;
        }

        /// <summary>
        /// Get Prefix and Postfix for Mapper file
        /// </summary>
        /// <param name="rowid">Start point.</param>
        /// <param name="maxrows">Maximum nuber of rows.</param>
        /// <param name="dbProvider">The instance of DBAdaptor.</param>
        /// <param name="sortColumn">The colume to be sorted.</param>
        /// <param name="subQuery">SQL text for sub query.</param>
        /// <returns></returns>
        public static string[] GetPrefixPostfix(int rowid, int maxrows, string dbProvider, string sortColumn, string subQuery)
        {
            string[] temp = new string[] { "", "" };
            string prefix = "";
            string postfix = "";
            int startRowNum = 0;
            int maxTotalTows = 0;
            if (rowid > 0)
                startRowNum += rowid;
            if (maxrows > 0)
                maxTotalTows = startRowNum + maxrows;

            switch (dbProvider)
            {
                case Phrase.DB_PROVIDER_MSSQL2000:
                    {
                        // Need to remove "SELECT" or "SELECT DISTINCT" from Mapper file
                        if (startRowNum != 0 && maxTotalTows != 0)
                        {
                            prefix = " select top " + maxrows.ToString() + " * from (select top " + maxTotalTows.ToString() + " * from (select distinct top " + maxTotalTows.ToString() + " ";
                            postfix = " order by " + sortColumn + " asc) tempresult order by " + sortColumn + " desc) finalresult ";
                        }
                        else if (startRowNum == 0 && maxTotalTows != 0)
                        {
                            prefix = " select distinct top " + maxTotalTows.ToString() + " ";
                            postfix = " order by " + sortColumn + " asc ";
                        }
                        else if (startRowNum != 0 && maxTotalTows == 0)
                        {
                            prefix = " select distinct ";
                            postfix = " and " + sortColumn + " not in ( ";
                            postfix += subQuery;
                            postfix += " order by " + sortColumn + " asc)";
                        }
                        else
                        {
                            prefix = " select distinct ";
                            postfix = "";
                        }
                        break;
                    }
                case Phrase.DB_PROVIDER_MSSQL2005:
                    {
                        if (startRowNum != 0 && maxTotalTows != 0)
                        {
                            prefix = "select top " + maxTotalTows.ToString() +
                                " * from ( select row_number() over(order by " +
                                sortColumn + " asc) as rownum, ";
                            postfix = " ) rs where rs.rownum > " + startRowNum.ToString();
                        }
                        else if (startRowNum == 0 && maxTotalTows != 0)
                        {
                            prefix = "select top " + maxTotalTows.ToString();
                            postfix = " order by " + sortColumn + " asc";
                        }
                        else if (startRowNum != 0 && maxTotalTows == 0)
                        {
                            prefix = "select * from ( select row_number() over(order by " +
                                sortColumn + " asc) as rownum, ";
                            postfix = " ) rs where rs.rownum > " + startRowNum.ToString();
                        }
                        else
                        {
                            prefix = "select ";
                            postfix = "";
                        }
                        break;
                    }
                case Phrase.DB_PROVIDER_ORACLE:
                    {
                        if (startRowNum != 0 && maxTotalTows != 0)
                        {
                            prefix = "select * from (select p.*, rownum rnum from (";
                            postfix = ") p where rownum <= " + maxTotalTows.ToString() +
                                ") where rnum >= " + startRowNum.ToString();
                        }
                        else if (startRowNum == 0 && maxTotalTows != 0)
                        {
                            prefix = "select p.*, rownum rnum from (";
                            postfix = ") p where rownum <= " + maxTotalTows.ToString();
                        }
                        else if (startRowNum != 0 && maxTotalTows == 0)
                        {
                            prefix = "select * from (select p.*, rownum rnum from (";
                            postfix = ") p ) where rnum >= " + startRowNum.ToString();
                        }
                        break;
                    }

            }
            temp[0] = prefix;
            temp[1] = postfix;
            return temp;
        }
        #endregion
    }
}
