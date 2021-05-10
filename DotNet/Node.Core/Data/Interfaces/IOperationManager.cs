using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;

using Node.Core.Biz.Objects;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve OperationManager information.
    /// </summary>
    public interface IOperationManager
    {
        /// <summary>
        /// Get System Configuration File from Database
        /// </summary>
        /// <returns>System Configuration Xml Document</returns>
        XmlDocument GetOperationManagerConfig();
        /// <summary>
        /// Update System Configuration File in Database
        /// </summary>
        /// <param name="xml">The new System Configuration file</param>
        /// <returns>true if successful, false otherwise</returns>
        bool UpdateOperationManagerConfig(XmlDocument xml);
        /// <summary>
        /// Saves the config file.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <param name="content">The config content.</param>
        /// <returns>If successfully saved, return true; otherwise, return false.</returns>
        bool SaveOperationManagerConfig(string id, string filename, string type, string content);
        /// <summary>
        /// Get Documents by Operation ID
        /// </summary>                                      
        /// <param name="opID">Operation ID</param>
        /// <param name="opName">Operation Name</param> 
        /// <returns>Data Table of File Cabinet.</returns>
        DataTable GetDocumentsByOperationID(string opID,string opName);
        /// <summary>
        /// Get Operation Parameter Name
        /// </summary>
        /// <param name="opID">The operation ID</param>
        /// <returns>A list of paramters name</returns>
        List<string> GetParametersName(string opID);
        /// <summary>
        /// Get Operation Parameter value by Parameter name
        /// </summary>
        /// <param name="opID">The operation id</param>
        /// <param name="paraName">The name of parameter</param>
        /// <returns></returns>
        DataTable GetParameterValue(string opID, string paraName);
        /// <summary>
        /// Create OperationManagerTrans 
        /// </summary>    
        /// <returns>OperationManagerTrans object</returns>
        OperationManagerTrans CreateOperationManagerTrans();
        /// <summary>
        /// Get OperationManagerTrans by ID
        /// </summary>
        /// <param name="id">OperationManagerTrans id</param>       
        /// <returns>OperationManagerTrans object</returns>
        OperationManagerTrans GetOperationManagerTrans(int id);
        /// <summary>
        /// Create OperationManagerTrans by Operation
        /// </summary>
        /// <param name="omtObject">OperationManagerTrans object</param>       
        void UpdateOperationManagerTrans(OperationManagerTrans omtObject);
        /// <summary>
        /// Get OperationManagerTrans by ID
        /// </summary>
        /// <param name="opName">Operation Name</param>       
        /// <returns>DataTable object</returns>
        DataTable GetOperationManagerTransByOpName(string opName);
        /// <summary>
        /// Get OperationManagerTrans by Status
        /// </summary>
        /// <param name="Status">Status</param>       
        /// <returns>DataTable object</returns>
        DataTable GetOperationManagerTransByStatus(string Status);
        /// <summary>
        /// Get Check OperationManagerTrans for DownloadReport 
        /// </summary>
        /// <param name="opName">Operation Name</param> 
        /// <param name="transID">Transaction ID</param>
        /// <returns>boolean</returns>
        bool CheckOperationManagerTransDownloadReport(string opName, string transID);
        /// <summary>
        /// Get OperationManagerTrans by Operation Name and Transaction ID
        /// </summary>
        /// <param name="opName">Operation Name</param> 
        /// <param name="transID">Transaction ID</param>      
        /// <returns>OperationManagerTrans object</returns>
        OperationManagerTrans GetOperationManagerTrans(string opName, string transID);
    }
}
