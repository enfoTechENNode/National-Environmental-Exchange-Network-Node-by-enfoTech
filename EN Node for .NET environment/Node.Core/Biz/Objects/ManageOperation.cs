using System;
using System.Collections;
using System.Xml;
using System.Text;
using System.Data;

using Node.Lib.Security;

using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Logging;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// ManageOperation is used to access OperationManager Configuration XML file. 
    /// </summary>
    public class ManageOperation
    {
        private XmlDocument Config = null;
        /// <summary>
        /// Constructor of ManageOperation.
        /// </summary>
        public ManageOperation()
        {
            IOperationManager configDB = new DBManager().GetOperationManagerDB();           
            this.Config = configDB.GetOperationManagerConfig();

            //IConfigurations sysConfigDB = new DBManager().GetConfigurationsDB();
            //this.sysConfig = sysConfigDB.GetSystemConfig();
        }
        /// <summary>
        /// Gets Operation List
        /// </summary>
        /// <param name="version">Node Version</param>
        /// <returns>ID,NAME</returns>
        public DataTable GetConfigOperations(string version)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");

            if(this.Config != null)
            {
            XmlNodeList operationList = this.Config.SelectNodes(".//Operation[@version = '" + version + "']");

            foreach (XmlNode op in operationList)
            {
                string opID = op.Attributes.GetNamedItem("id").Value;
                string opName = op.Attributes.GetNamedItem("name").Value;
                DataRow dr = dt.NewRow();
                dr["ID"] = opID;
                dr["NAME"] = opName;
                dt.Rows.Add(dr);  
            }
            }
            return dt;
        }
        //Get DataFlow by ID
        //public string GetConfigDataFlowByID(string opID)
        //{
        //    string dataFlow = string.Empty;
        //    XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
        //    XmlNode ndOPDataFlow = ndOperation.SelectSingleNode(".//DataFlow");
        //    dataFlow = ndOPDataFlow.InnerText;

        //    return dataFlow;  
        //}

        //Get DataFlow by ID
        //public Hashtable GetConfigSubmitCredentialByID(string opID)
        //{
        //    Hashtable htSubCre = new Hashtable();
        //    try
        //    {
               
        //        string dataFlow = string.Empty;
        //        XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
        //        XmlNode ndOPSubmit = ndOperation.SelectSingleNode(".//Submit");

        //        XmlNode ndSubmitURL = ndOPSubmit.SelectSingleNode(".//URL");
        //        XmlNode ndSubmitUserName = ndOPSubmit.SelectSingleNode(".//UserName");
        //        XmlNode ndSubmitPassword = ndOPSubmit.SelectSingleNode(".//Password");
        //        XmlNode ndSubmitDomainName = ndOPSubmit.SelectSingleNode(".//DomainName");
        //        XmlNode ndSubmitDataFlow = ndOPSubmit.SelectSingleNode(".//DataFlow");
        //        XmlNode ndSubmitFlowOperation = ndOPSubmit.SelectSingleNode(".//FlowOperation");

        //        htSubCre.Add("URL", ndSubmitURL.InnerText.Trim());
        //        htSubCre.Add("UserName", ndSubmitUserName.InnerText.Trim());
        //        htSubCre.Add("Password", ndSubmitPassword.InnerText.Trim());
        //        htSubCre.Add("DomainName", ndSubmitDomainName.InnerText.Trim());
        //        htSubCre.Add("DataFlow", ndSubmitDataFlow.InnerText.Trim());
        //        htSubCre.Add("FlowOperation", ndSubmitFlowOperation.InnerText.Trim());
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //    return htSubCre;
        //}

        //Get All Operation Values by ID
        //public Hashtable  GetOperationNameByID(string opID)
        //{
        //    Hashtable htOPValues = new Hashtable();     
        //    XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
        //    string opName = ndOperation.Attributes.GetNamedItem("name").Value;
        //    htOPValues.Add("opName", opName);

        //    XmlNode ndUpload = ndOperation.SelectSingleNode(".//Upload");
        //    string upload = ndUpload.Attributes.GetNamedItem("Enable").Value;
        //    htOPValues.Add("upload", upload);

        //    XmlNode ndGenerate = ndOperation.SelectSingleNode(".//Generate");
        //    string generate = ndGenerate.Attributes.GetNamedItem("Enable").Value;
        //    htOPValues.Add("generate", generate);

        //    XmlNode ndSubmit = ndOperation.SelectSingleNode(".//Submit");
        //    string submit = ndSubmit.Attributes.GetNamedItem("Enable").Value;
        //    htOPValues.Add("submit", submit);

        //    //Get Submit URL
        //    XmlNode ndSubmitURL = ndSubmit.SelectSingleNode(".//URL");
        //    string submitURL = ndSubmitURL.InnerText;
        //    htOPValues.Add("submitURL", submitURL);

        //    //Get Submit UserName
        //    XmlNode ndSubmitUsername = ndSubmit.SelectSingleNode(".//UserName");
        //    string submitUsername = ndSubmitUsername.InnerText;
        //    htOPValues.Add("submitUsername", submitUsername);

        //    //Get Submit Password
        //    XmlNode ndSubmitPassword = ndSubmit.SelectSingleNode(".//Password");
        //    string submitPassword = ndSubmitPassword.InnerText;
        //    htOPValues.Add("submitPassword", submitPassword);

        //    //Get Submit DomainName
        //    XmlNode ndSubmitDomainName = ndSubmit.SelectSingleNode(".//DomainName");
        //    string submitDomainName = ndSubmitPassword.InnerText;
        //    htOPValues.Add("submitDomainName", submitDomainName);

        //    //Get Submit DataFlow
        //    XmlNode ndSubmitDataFlow = ndSubmit.SelectSingleNode(".//DataFlow");
        //    string submitDataFlow = ndSubmitPassword.InnerText;
        //    htOPValues.Add("submitDataFlow", submitDataFlow);

        //    //Get Submit FlowOperation
        //    XmlNode ndSubmitFlowOperation = ndSubmit.SelectSingleNode(".//FlowOperation");
        //    string submitFlowOperation = ndSubmitFlowOperation.InnerText;
        //    htOPValues.Add("submitFlowOperation", submitFlowOperation);
            
        //    return htOPValues;
        //}
        /// <summary>
        /// Gets Operation information in the OperationManager Configuration
        /// </summary>
        /// <param name="opID">The operation id</param>
        /// <returns></returns>
        public OpMgrData GetOperation(string opID)
        {
            OpMgrData objOpMgrData= new OpMgrData();

            objOpMgrData.opID = opID;

            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
            objOpMgrData.opName = ndOperation.Attributes.GetNamedItem("name").Value;

            XmlNode ndUpload = ndOperation.SelectSingleNode(".//Upload");
            objOpMgrData.upload = (ndUpload.Attributes.GetNamedItem("Enable").Value.ToUpper() == "TRUE"?true:false);

            XmlNode ndGenerate = ndOperation.SelectSingleNode(".//Generate");
            objOpMgrData.generate = (ndGenerate.Attributes.GetNamedItem("Enable").Value.ToUpper() == "TRUE" ? true : false);

            XmlNode ndSubmit = ndOperation.SelectSingleNode(".//Submit");
            objOpMgrData.submit = (ndSubmit.Attributes.GetNamedItem("Enable").Value.ToUpper() == "TRUE" ? true : false);

            XmlNode ndGetStatus = ndOperation.SelectSingleNode(".//GetStatus");
            if (ndGetStatus != null)
            {
                objOpMgrData.GetStatus = (ndGetStatus.Attributes.GetNamedItem("Enable").Value.ToUpper() == "TRUE" ? true : false);
                //Get GetStatus Complete

                XmlNode ndGetStatusComplete = ndGetStatus.SelectSingleNode(".//Complete");
                if (ndGetStatusComplete != null)
                {
                    objOpMgrData.GetStatusComplete = ndGetStatusComplete.InnerText;
                }
                //Get GetStatus Error
                XmlNode ndGetStatusError = ndGetStatus.SelectSingleNode(".//Error");
                if (ndGetStatusError != null)
                {
                    objOpMgrData.GetStatusError = ndGetStatusError.InnerText;
                }
            }
            else
            {
                objOpMgrData.GetStatus = false;
                objOpMgrData.GetStatusComplete = "";
                objOpMgrData.GetStatusError = "";
            }

            objOpMgrData.view = true;

            //Get Submit URL
            XmlNode ndSubmitURL = ndSubmit.SelectSingleNode(".//URL");
            objOpMgrData.submitURL = ndSubmitURL.InnerText;

            //Get Submit UserName
            XmlNode ndSubmitUsername = ndSubmit.SelectSingleNode(".//UserName");
            objOpMgrData.submitUsername = ndSubmitUsername.InnerText;

            //Get Submit Password
            XmlNode ndSubmitPassword = ndSubmit.SelectSingleNode(".//Password");
            objOpMgrData.submitPassword = ndSubmitPassword.InnerText;

            //Get Submit DomainName
            XmlNode ndSubmitDomainName = ndSubmit.SelectSingleNode(".//DomainName");
            if (ndSubmitDomainName != null)
            {
                objOpMgrData.submitDomainName = ndSubmitDomainName.InnerText;
            }
            //Get Submit DataFlow
            XmlNode ndSubmitDataFlow = ndSubmit.SelectSingleNode(".//DataFlow");
            if (ndSubmitDataFlow != null && !ndSubmitDataFlow.InnerText.Trim().Equals(""))
            {
                objOpMgrData.dataFlow = ndSubmitDataFlow.InnerText;
            }
            else if (ndSubmitDataFlow == null)
            {
                XmlNode ndDataFlow = this.Config.SelectSingleNode(".//Operation[@id = '" + objOpMgrData.opID + "']/DataFlow");
                if (!ndDataFlow.InnerText.Trim().Equals(""))
                {
                    objOpMgrData.dataFlow = ndDataFlow.InnerText;
                }
            }
            //Get Submit FlowOperation
            XmlNode ndSubmitFlowOperation = ndSubmit.SelectSingleNode(".//FlowOperation");
            if (ndSubmitFlowOperation != null)
            {
                objOpMgrData.dataFlowOperation = ndSubmitFlowOperation.InnerText;
            }

            DBManager dbmgr = new DBManager();
            Operation op = dbmgr.GetOperationsDB().GetOperation(int.Parse(opID));
            objOpMgrData.opType = op.Type;

            return objOpMgrData;
        }
        /// <summary>
        /// Adds Operation to the OperationManager Configuration
        /// </summary>
        /// <param name="objOpMgrData">objOpMgrData</param>
        /// <returns></returns>
        public string AddOperation(OpMgrData objOpMgrData)
        {
            string msgSave = string.Empty;

            try
            {
                //Operation
                XmlElement ndOP = this.Config.CreateElement("Operation");
                ndOP.SetAttribute("id", objOpMgrData.opID);
                ndOP.SetAttribute("name", objOpMgrData.opName);
                ndOP.SetAttribute("version", objOpMgrData.version);

                //Upload
                XmlElement ndOPUpload = this.Config.CreateElement("Upload");
                ndOPUpload.SetAttribute("Enable", objOpMgrData.upload.ToString());

                XmlElement ndOPUploadParameters = this.Config.CreateElement("Parameters");
                ndOPUpload.AppendChild(ndOPUploadParameters);
                ndOP.AppendChild(ndOPUpload);

                //Generate
                XmlElement ndOPGenerate = this.Config.CreateElement("Generate");
                ndOPGenerate.SetAttribute("Enable", objOpMgrData.generate.ToString());
                ndOP.AppendChild(ndOPGenerate);

                //Submit
                XmlElement ndOPSubmit = this.Config.CreateElement("Submit");
                ndOPSubmit.SetAttribute("Enable", objOpMgrData.submit.ToString());

                //Submit URL
                XmlElement ndOPSubURL = this.Config.CreateElement("URL");
                ndOPSubURL.InnerText = objOpMgrData.submitURL;
                ndOPSubmit.AppendChild(ndOPSubURL);

                //Submit Username
                XmlElement ndOPSubUsername = this.Config.CreateElement("UserName");
                ndOPSubUsername.InnerText = objOpMgrData.submitUsername;
                ndOPSubmit.AppendChild(ndOPSubUsername);

                //Submit Password
                XmlElement ndOPSubPassword = this.Config.CreateElement("Password");
                ndOPSubPassword.InnerText = objOpMgrData.submitPassword;
                ndOPSubmit.AppendChild(ndOPSubPassword);

                //Submit DomainName
                XmlElement ndOPSubDomainName = this.Config.CreateElement("DomainName");
                ndOPSubDomainName.InnerText = objOpMgrData.submitDomainName;
                ndOPSubmit.AppendChild(ndOPSubDomainName);

                //DataFlow
                XmlElement ndDataFlow = this.Config.CreateElement("DataFlow");
                ndDataFlow.InnerText = objOpMgrData.dataFlow;
                ndOPSubmit.AppendChild(ndDataFlow);

                //Submit FlowOperation  
                XmlElement ndOPSubFlowOperation = this.Config.CreateElement("FlowOperation");
                ndOPSubFlowOperation.InnerText = objOpMgrData.dataFlowOperation;
                ndOPSubmit.AppendChild(ndOPSubFlowOperation);

                ndOP.AppendChild(ndOPSubmit);

                //View
                XmlElement ndOPView = this.Config.CreateElement("View");
                ndOPView.SetAttribute("Enable", objOpMgrData.view.ToString());
                ndOP.AppendChild(ndOPView);
                ////View Template
                //Hashtable ha = ViewTemplate;
                //ArrayList arTemID = (ArrayList)ha["ID"];
                //ArrayList arTemName = (ArrayList)ha["Name"];

                //XmlElement ndOPViewTemplate = null;

                //for (int i = 0; i < arTemID.Count; i++)
                //{
                //    ndOPViewTemplate = this.Config.CreateElement("Template");
                //    ndOPViewTemplate.SetAttribute("id", arTemID[i].ToString());
                //    ndOPViewTemplate.InnerText = arTemName[i].ToString();
                //    ndOPView.AppendChild(ndOPViewTemplate);
                //}
                //Validation
                XmlElement ndOPValidation = this.Config.CreateElement("Validate");
                ndOP.AppendChild(ndOPValidation);

                //GetStatus
                XmlElement ndOPGetStatus = this.Config.CreateElement("GetStatus");
                ndOPGetStatus.SetAttribute("Enable", objOpMgrData.GetStatus.ToString());

                XmlElement ndOPGetStatusComplete = this.Config.CreateElement("Complete");
                ndOPGetStatusComplete.InnerText = objOpMgrData.GetStatusComplete.ToString();
                ndOPGetStatus.AppendChild(ndOPGetStatusComplete);

                XmlElement ndOPGetStatusError = this.Config.CreateElement("Error");
                ndOPGetStatusError.InnerText = objOpMgrData.GetStatusError.ToString();
                ndOPGetStatus.AppendChild(ndOPGetStatusError);

                ndOP.AppendChild(ndOPGetStatus);


                //Save Operation Node
                this.Config.DocumentElement.AppendChild(ndOP);
            }
            catch
            {
                msgSave = "Operation not Saved. There was an error in saving the operation";
            }
            return msgSave;            
        }
        /// <summary>
        /// Updates operation to the OperationManager Configuration.
        /// </summary>
        /// <param name="objOpMgrData">objOpMgrData</param>
        /// <returns></returns>
        public string EditOperation(OpMgrData objOpMgrData)
        {
            string msgSave = string.Empty;

            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + objOpMgrData.opID + "']");
            XmlNode ndOPSubmit = this.Config.SelectSingleNode(".//Operation[@id = '" + objOpMgrData.opID + "']/Submit");
            if (ndOperation != null)
            {
                try
                {
                    
                    //Edit DataFlow
                    //XmlNode ndDataFlow = ndOperation.SelectSingleNode(".//DataFlow");
                    //ndDataFlow.InnerText = dataFlow;
 
                    //Edit Upload
                    XmlNode ndUpload = ndOperation.SelectSingleNode(".//Upload");
                    ndUpload.Attributes.GetNamedItem("Enable").Value = objOpMgrData.upload.ToString();

                    //Edit Generate
                    XmlNode ndGenerate = ndOperation.SelectSingleNode(".//Generate");
                    ndGenerate.Attributes.GetNamedItem("Enable").Value = objOpMgrData.generate.ToString();

                    //XmlNode ndParameters = ndOperation.SelectSingleNode(".//Upload/Parameters");
                    //if (ndParameters != null)
                    //    ndGenerate.RemoveChild(ndParameters);
                    //ndParameters = this.Config.CreateElement("Parameters");

                    //if (Parameters.Rows.Count > 0)
                    //{
                    //    foreach (DataRow aRow in Parameters.Rows)
                    //    {
                    //        XmlElement xParameter = this.Config.CreateElement("Parameter");
                    //        xParameter.SetAttribute("id", (string)aRow[0]);
                    //        xParameter.SetAttribute("name", (string)aRow[1]);
                    //        xParameter.InnerText = (string)aRow[2];
                    //        ndParameters.AppendChild(xParameter);
                    //    }
                    //}
                    //ndGenerate.AppendChild(ndParameters);

                    //Edit Submit
                    XmlNode ndSubmit = ndOperation.SelectSingleNode(".//Submit");
                    ndSubmit.Attributes.GetNamedItem("Enable").Value = objOpMgrData.submit.ToString();

                    //Edit Submit URL
                    XmlNode ndSubmitURL = ndSubmit.SelectSingleNode(".//URL");
                    ndSubmitURL.InnerText = objOpMgrData.submitURL;

                    //Edit Submit UserName
                    XmlNode ndSubmitUsername = ndSubmit.SelectSingleNode(".//UserName");
                    ndSubmitUsername.InnerText = objOpMgrData.submitUsername;

                    //Edit Submit Password
                    XmlNode ndSubmitPassword = ndSubmit.SelectSingleNode(".//Password");
                    ndSubmitPassword.InnerText = objOpMgrData.submitPassword;

                    //Edit Submit DomainName
                    XmlNode ndSubmitDomainName = ndSubmit.SelectSingleNode(".//DomainName");
                    if (ndSubmitDomainName == null)
                    {
                        ndSubmitDomainName = this.Config.CreateElement("DomainName");
                        ndOPSubmit.AppendChild(ndSubmitDomainName);
                    }
                    ndSubmitDomainName.InnerText = objOpMgrData.submitDomainName;

                    //Edit Submit DataFlow
                    XmlNode ndSubmitDataFlow = ndSubmit.SelectSingleNode(".//DataFlow");
                    if (ndSubmitDataFlow == null)
                    {
                        ndSubmitDataFlow = this.Config.CreateElement("DataFlow");
                        ndOPSubmit.AppendChild(ndSubmitDataFlow);
                    }
                    ndSubmitDataFlow.InnerText = objOpMgrData.dataFlow;

                    //Edit Submit FlowOperation
                    XmlNode ndSubmitFlowOperation = ndSubmit.SelectSingleNode(".//FlowOperation");
                    if (ndSubmitFlowOperation == null)
                    {
                        ndSubmitFlowOperation = this.Config.CreateElement("FlowOperation");
                        ndOPSubmit.AppendChild(ndSubmitFlowOperation);
                    }
                    ndSubmitFlowOperation.InnerText = objOpMgrData.dataFlowOperation;

                    //Edit View    
                    XmlNode ndView = ndOperation.SelectSingleNode(".//View");
                    ndView.Attributes["Enable"].InnerText = objOpMgrData.view.ToString();
                    //ndOperation.RemoveChild(ndView);

                    //XmlElement ndViewTem = this.Config.CreateElement("View");
                    //ndViewTem.SetAttribute("Enable", view.ToString());

                    //Edit Template
                    //Hashtable ha = ViewTemplate;
                    //ArrayList arTemID = (ArrayList)ha["ID"];
                    //ArrayList arTemName = (ArrayList)ha["Name"];

                    //XmlElement ndOPViewTemplate = null;

                    //for (int i = 0; i < arTemID.Count; i++)
                    //{
                    //    ndOPViewTemplate = this.Config.CreateElement("Template");
                    //    ndOPViewTemplate.SetAttribute("id", arTemID[i].ToString());
                    //    ndOPViewTemplate.InnerText = arTemName[i].ToString();
                    //    ndViewTem.AppendChild(ndOPViewTemplate);
                    //}

                    //ndOperation.AppendChild(ndViewTem);

                    //Edit Validation 
                    //XmlNode ndValidation = ndOperation.SelectSingleNode(".//Validate");
                    //if (ndValidation != null)
                    //    ndOperation.RemoveChild(ndView);

                    //ndValidation = this.Config.CreateElement("Validate");

                    ////Edit Template
                    //ArrayList arValID = (ArrayList)Validation["ID"];
                    //ArrayList arValName = (ArrayList)Validation["Name"];

                    //XmlElement ndOPValidation= null;

                    //for (int i = 0; i < arTemID.Count; i++)
                    //{
                    //    ndOPValidation = this.Config.CreateElement("File");
                    //    ndOPValidation.SetAttribute("id", arTemID[i].ToString());
                    //    ndOPValidation.InnerText = arTemName[i].ToString();
                    //    ndValidation.AppendChild(ndOPValidation);
                    //}

                    //ndOperation.AppendChild(ndValidation);


                    //Edit GetStatus
                    XmlNode ndGetStatus = ndOperation.SelectSingleNode(".//GetStatus");
                    if (ndGetStatus == null)
                    {
                        ndGetStatus = this.Config.CreateElement("GetStatus");
                        XmlAttribute getstatuenable = this.Config.CreateAttribute("Enable");
                        ndGetStatus.Attributes.Append(getstatuenable);

                        XmlElement ndOPGetStatusComplete = this.Config.CreateElement("Complete");
                        ndGetStatus.AppendChild(ndOPGetStatusComplete);

                        XmlElement ndOPGetStatusError = this.Config.CreateElement("Error");
                        ndGetStatus.AppendChild(ndOPGetStatusError);

                        ndOperation.AppendChild(ndGetStatus);

                    }
                    ndGetStatus.Attributes.GetNamedItem("Enable").Value = objOpMgrData.GetStatus.ToString();
                    ndGetStatus.SelectSingleNode(".//Complete").InnerText = objOpMgrData.GetStatusComplete.ToString();
                    ndGetStatus.SelectSingleNode(".//Error").InnerText = objOpMgrData.GetStatusError.ToString();


                    //Save Operation Node
                    this.Config.DocumentElement.AppendChild(ndOperation);
                }
                catch
                {
                    msgSave = "Operation not Saved. There was an error in saving the operation";
                }
            }
            return msgSave;            
        }
        /// <summary>
        /// Removes operation from the OperationManager Configuration.
        /// </summary>
        /// <param name="opID"></param>
        public void DeleteOperationByID(string opID)
        {
            XmlNode ndRoot = this.Config.DocumentElement;
            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
            ndRoot.RemoveChild(ndOperation);             
        }
        /// <summary>
        /// Adds StyleSheet to the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <param name="temID">The stylesheet id.</param>
        /// <param name="temName">The name of stylesheet.</param>
        public void AddStyleSheet(string opID, string temID, string temName)
        {
            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

            XmlNode ndViewTem = ndOperation.SelectSingleNode(".//View");             

            XmlElement ndOPViewTemplate = this.Config.CreateElement("Template");
            ndOPViewTemplate.SetAttribute("id", temID);
            ndOPViewTemplate.InnerText = temName;   
               
            ndViewTem.AppendChild(ndOPViewTemplate);
            //ndOperation.AppendChild(ndViewTem);
        }
        /// <summary>
        /// Removes Stylesheet from the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <param name="temID">The stylesheet id.</param>
        /// <returns></returns>
        public bool DeleteStyleSheet(string opID, string temID)
        {
            bool bolDelete = true;
            try
            {
                XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

                XmlNode ndView = ndOperation.SelectSingleNode(".//View");

                XmlNode ndTemplate = ndOperation.SelectSingleNode(".//View/Template[@id = '" + temID + "']");

                ndView.RemoveChild(ndTemplate);
            }
            catch
            {
                bolDelete = false;
            }

            return bolDelete; 
        }
        /// <summary>
        /// Adds validation rule to the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <param name="fileID">The validation file id.</param>
        /// <param name="temName">The name of validation file.</param>
        public void AddValidationRule(string opID, string fileID, string temName)
        {
            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

            XmlNode ndValidation = ndOperation.SelectSingleNode(".//Validate");
            if (ndValidation == null)
            {
                ndValidation = this.Config.CreateElement("Validate");
                ndOperation.AppendChild(ndValidation);
            }
            
            XmlElement ndOPViewTemplate = this.Config.CreateElement("File");
            ndOPViewTemplate.SetAttribute("id", fileID);
            ndOPViewTemplate.InnerText = temName;

            ndValidation.AppendChild(ndOPViewTemplate);
        }
        /// <summary>
        /// Removes validation rule from the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id</param>
        /// <param name="fileID">The validation rule file id.</param>
        /// <returns></returns>
        public bool DeleteValidationRule(string opID, string fileID)
        {
            bool bolDelete = true;
            try
            {
                XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

                XmlNode ndValidation = ndOperation.SelectSingleNode(".//Validate");

                XmlNode ndTemplate = ndOperation.SelectSingleNode(".//Validate/File[@id = '" + fileID + "']");

                ndValidation.RemoveChild(ndTemplate);
            }
            catch
            {
                bolDelete = false;
            }

            return bolDelete;
        }
        /// <summary>
        /// Adds parameter to the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <param name="paraName">The name of parameter.</param>
        /// <param name="paraValue">The value of parameter.</param>
        public void AddParameters(string opID, string paraName, string paraValue)
        {
            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

            XmlNode ndParameters = ndOperation.SelectSingleNode(".//Upload/Parameters");
            
            int id = 0;

            if (ndParameters == null)
            {
                ndParameters = this.Config.CreateElement("Parameters");
                ndOperation.SelectSingleNode(".//Upload").AppendChild(ndParameters);
            }
            if (ndParameters.ChildNodes.Count > 0)
            {
                id = int.Parse(ndParameters.ChildNodes[ndParameters.ChildNodes.Count - 1].Attributes.GetNamedItem("id").Value.ToString()) + 1;
            }
            else
            {
                id++;
            }

            XmlElement ndParameter = this.Config.CreateElement("Parameter");
            ndParameter.SetAttribute("id", id.ToString());
            ndParameter.SetAttribute("name", paraName.ToString());
            ndParameter.InnerText = paraValue;
            ndParameters.AppendChild(ndParameter);

        }
        /// <summary>
        /// Removes parameter from the OperationManager Configuration.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <param name="paraid">The name of parameter.</param>
        /// <returns></returns>
        public bool DeleteParameters(string opID, string paraid)
        {
            bool bolDelete = true;
            try
            {
                XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

                XmlNode ndParameters = ndOperation.SelectSingleNode(".//Upload/Parameters");

                XmlNode ndParameter = ndOperation.SelectSingleNode(".//Upload/Parameters/Parameter[@id = '" + paraid + "']");

                ndParameters.RemoveChild(ndParameter);
            }
            catch
            {
                bolDelete = false;
            }

            return bolDelete;
        }
        /// <summary>
        /// Gets Stylesheets for specified operation.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <returns>ID,NAME</returns>
        public Hashtable GetStyleSheetByID(string opID)
        {
            ArrayList arrTemID = new ArrayList();
            ArrayList arrTemName = new ArrayList();
            Hashtable htTemplate = null;

            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

            XmlNode ndViewTem = ndOperation.SelectSingleNode(".//View");

            XmlNodeList ndOPViewTemplate = ndViewTem.ChildNodes;

            foreach (XmlNode nd in ndOPViewTemplate)
            {
                string temID = string.Empty;
                string temName = string.Empty;
                htTemplate = new Hashtable();

                temID = nd.Attributes.GetNamedItem("id").Value;
                temName = nd.InnerText;

                arrTemID.Add(temID);
                arrTemName.Add(temName);
                htTemplate.Add("ID", arrTemID);
                htTemplate.Add("NAME", arrTemName);                        
            }
            return htTemplate;
        }
        /// <summary>
        /// Gets Validations by Operation ID.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <returns>ID,NAME</returns>
        public Hashtable GetValidationRuleByID(string opID)
        {
            ArrayList arrTemID = new ArrayList();
            ArrayList arrTemName = new ArrayList();
            Hashtable htTemplate = null;

            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");

            XmlNode ndValidation = ndOperation.SelectSingleNode(".//Validate");
            
            if (ndValidation == null)
            {
                return null;
            }

            XmlNodeList ndOPValidations = ndValidation.ChildNodes;

            foreach (XmlNode nd in ndOPValidations)
            {
                string temID = string.Empty;
                string temName = string.Empty;
                htTemplate = new Hashtable();

                temID = nd.Attributes.GetNamedItem("id").Value;
                temName = nd.InnerText;

                arrTemID.Add(temID);
                arrTemName.Add(temName);
                htTemplate.Add("ID", arrTemID);
                htTemplate.Add("NAME", arrTemName);
            }
            return htTemplate;
        }
        /// <summary>
        /// Get Parameters by Operation ID.
        /// </summary>
        /// <param name="opID">The operation id.</param>
        /// <returns>ID,NAME</returns>
        public DataTable GetParametersByID(string opID)
        {
            XmlNode ndOperation = this.Config.SelectSingleNode(".//Operation[@id = '" + opID + "']");
            DataTable dt = null;
            if (ndOperation != null)
            {
                XmlNode ndParameters = ndOperation.SelectSingleNode(".//Upload/Parameters");

                if (ndParameters != null)
                {
                    dt = new DataTable();

                    dt.Columns.Add(new DataColumn("id", typeof(string)));
                    dt.Columns.Add(new DataColumn("ParameterName", typeof(string)));
                    dt.Columns.Add(new DataColumn("XPath", typeof(string)));

                    if (ndParameters == null)
                    {
                        return null;
                    }

                    XmlNodeList ndParameterslist = ndParameters.ChildNodes;

                    foreach (XmlNode nd in ndParameterslist)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["id"] = nd.Attributes.GetNamedItem("id").Value;
                        newRow["ParameterName"] = nd.Attributes.GetNamedItem("name").Value;
                        newRow["XPath"] = nd.InnerText;
                        dt.Rows.Add(newRow);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// Get Documents by Operation ID
        /// </summary>                                      
        /// <param name="opID">Operation ID</param>
        /// <param name="opName">Operation Name</param> 
        /// <returns>Data Table of File Cabinet.</returns>
        public DataTable GetDocumentsByOperationID(string opID,string opName)
        {
            return new DBManager().GetOperationManagerDB().GetDocumentsByOperationID(opID,opName);
        }
        /// <summary>
        ///Save Operation Operation Manager Config .
        /// </summary>
        /// <returns>True if success.</returns>
        public bool SaveOperationManager()
        {
            return new DBManager().GetOperationManagerDB().UpdateOperationManagerConfig(this.Config);
        }
        /// <summary>
        /// Upload Style Sheets / validation rule 
        /// </summary>
        /// <param name="filename">The name of file.</param>
        /// <param name="filetype">The type of file.</param>
        /// <param name="content">The byte[] of file</param>
        /// <returns>id of file upload.</returns>
        public int AddUploadFile(string filename, string filetype, byte[] content)
        {
            DBManager dbMgr = new DBManager();
            IConfigurations configDb = dbMgr.GetConfigurationsDB();
            return configDb.AddConfig(filename, filetype, new UTF8Encoding().GetString(content));
        }
        /// <summary>
        /// Gets the list of config names by specified config type.
        /// </summary>
        /// <param name="type">The config type.</param>
        /// <returns>A DataTable contains the list of config name.</returns>
        public DataTable GetConfigNames(string type)
        {
            DBManager dbMgr = new DBManager();
            IConfigurations configDb = dbMgr.GetConfigurationsDB();
            return configDb.GetConfigNames(type);
        }
        //Get Node Address for Ping
        //public string GetNodeAddress()
        //{
        //    return this.sysConfig.SelectSingleNode("/Configuration/NodeSettings/NodeURL").InnerText;
        //}
        /// <summary>
        /// Deletes a config by config id.
        /// </summary>
        /// <param name="temID">The config id.</param>
        /// <returns>If successfully deleted, return true; otherwise, return false.</returns>
        public static bool DeleteConfig(string temID)
        {
            DBManager dbMgr = new DBManager();
            IConfigurations configDb = dbMgr.GetConfigurationsDB();
            return configDb.DeleteConfig(temID + "");
        }
        /// <summary>
        /// The class store operation manager information.
        /// </summary>
        public class OpMgrData
        {
            /// <summary>
            /// operation id.
            /// </summary>
            public string opID { get;set;}
            /// <summary>
            /// operation name
            /// </summary>
            public string opName { get; set; }
            /// <summary>
            /// operation node version.
            /// </summary>
            public string version { get; set; }
            /// <summary>
            /// submit url.
            /// </summary>
            public string submitURL { get; set; }
            /// <summary>
            /// submit user name.
            /// </summary>
            public string submitUsername { get; set; }
            /// <summary>
            /// submit password.
            /// </summary>
            public string submitPassword { get; set; }
            /// <summary>
            /// submit domain name.
            /// </summary>
            public string submitDomainName { get; set; }
            /// <summary>
            /// submit dataflow name.
            /// </summary>
            public string dataFlow { get; set; }
            /// <summary>
            /// submit flow operation.
            /// </summary>
            public string dataFlowOperation { get; set; }
            /// <summary>
            /// The capable of upload. 
            /// </summary>
            public bool upload { get; set; }
            /// <summary>
            /// The capable of generate.
            /// </summary>
            public bool generate { get; set; }
            /// <summary>
            /// The capable of submit.
            /// </summary>
            public bool submit { get; set; }
            /// <summary>
            /// The cabable of view.
            /// </summary>
            public bool view { get; set; }
            /// <summary>
            /// The cabable of view.
            /// </summary>
            public bool GetStatus { get; set; }
            /// <summary>
            /// The complete status for GetStatus.
            /// </summary>
            public string GetStatusComplete { get; set; }
            /// <summary>
            /// The complete status for GetStatus.
            /// </summary>
            public string GetStatusError { get; set; }
            /// <summary>
            /// operation type
            /// </summary>
            public string opType { get; set; }
        }
    }
}
