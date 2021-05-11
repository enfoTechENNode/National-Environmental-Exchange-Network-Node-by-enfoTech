using System;
using System.Collections;
using System.Data;

using Node.Core.Biz.Objects;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieve Operation information.
    /// </summary>
    public interface IOperations
    {
        /// <summary>
        /// Get Operation class by operation identifier.
        /// </summary>
        /// <param name="opID">Identifier of operation</param>
        /// <returns>Operation Class</returns>
        Operation GetOperation(int opID);
        /// <summary>
        /// Get Operation class by operation name
        /// </summary>
        /// <param name="opName">Name of Operation</param>
        /// <returns>Operation Class</returns>
        Operation GetOperation(string opName);
        /// <summary>
        /// Get Operation class by operation name and web service name
        /// </summary>
        /// <param name="opName">Name of Operation</param>
        /// <param name="wsName">Name of Web Service</param>
        /// <returns></returns>
        Operation GetOperation(string opName, string wsName);
        /// <summary>
        /// Get unique opeation name under specified Domain
        /// </summary>
        /// <param name="domainAdmin">Name of Domain</param>
        /// <returns>Array of operation names</returns>
        string[] GetUniqueOperationNames(string domainAdmin);
        /// <summary>
        /// Get names of request under solicit web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        string[] GetSolicitNames();
        /// <summary>
        /// Get names of request under solicit web service and specified node version.
        /// </summary>
        /// <param name="version">Node Version. The value can be <see cref="Node.Core.Phrase.VERSION_11">VER_11</see> or <see cref="Node.Core.Phrase.VERSION_20">VER_20</see></param>
        /// <returns>Array of request names</returns>
        string[] GetSolicitNames(string version);
        /// <summary>
        /// Get a list of parameters under solicit web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        Hashtable GetSolicitNameParameterPairs();
        /// <summary>
        /// Get names of request under query web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        string[] GetQueryNames();
        /// <summary>
        /// Get names of request under query web service and specified node version.
        /// </summary>
        /// <returns>Array of request names</returns>
        string[] GetQueryNames(string version);
        /// <summary>
        /// Get a list of parameters under query web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        Hashtable GetQueryNameParameterPairs();

        /// <summary>
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, DOMAIN_NAME, WEB_SERVICE_NAME</returns>
        DataTable GetOperationsDataGrid(string domainAdmin);

        /// <summary>
        /// Get a DataTable for the Operations by UserName
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        DataTable GetOperationsByUser(string domainAdmin, string versionNo);

        /// <summary>
        /// Get the Operations of the IDs of the Operation Names and Web Service Names specified
        /// It is assumed that opNames[0] corresponds to wsNames[0], opNames[1] to wsNames[1], etc.
        /// If the input parameter arrays are different sizes, then only the first array.Length of lesser size input parameters are used in the query
        /// </summary>
        /// <param name="opNames">Array of Operation Names</param>
        /// <param name="wsNames">Array of Web Service Names</param>
        /// <returns></returns>
        int[] GetOperationIDs(string[] opNames, string[] wsNames);

        /// <summary>
        /// Get the Operation Names and Web Service Names from the list of Operation IDs
        /// </summary>
        /// <param name="opIDs"></param>
        /// <returns>A double string array, string[0] has opNames and string[1] has wsNames</returns>
        string[][] GetOpNamesWSNames(int[] opIDs);

        /// <summary>
        /// Get the Operations List for a Domain
        /// </summary>
        /// <param name="domainName">The Domain Name</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        DataTable GetOperationsList(string domainName);

        /// <summary>
        /// Search the Node Database for Operations
        /// </summary>
        /// <param name="domain">The Domain of the Operation(s)</param>
        /// <param name="opID">The Operation ID of the Operation</param>
        /// <param name="opType">The Operation Type of the Operation(s)</param>
        /// <param name="wsID">The Web Service ID of the Operation(s)</param>
        /// <param name="status">The Status of the Operation(s)</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, OPERATION_TYPE, WEB_SERVICE_NAME, OPERATION_STATUS_CD, OPERATION_STATUS_MSG</returns>
        DataTable SearchOperations(string domain, int opID, string opType, int wsID, string status);

        /// <summary>
        /// Save an Operation to the Database
        /// </summary>
        /// <param name="op">The Operation to Save</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged in</param>
        void SaveOperation(Operation op, string domainAdmin);

        /// <summary>
        /// Delete an Operation to the Database
        /// </summary>
        /// <param name="op">The Operation to Delete</param>
        void DeleteOperation(Operation op);

        /// <summary>
        /// Update operation config to the Database
        /// </summary>
        /// <param name="opID">The Operation to Delete</param>
        /// <param name="config">The Operation config XML String</param>
        bool UpdateOperationConfig(string opID, string config);

        /// <summary>
        /// Get a DataTable for the Current process information
        /// </summary>
        /// <returns>Columns: PROCESS_NAME, UPDATED_DTTM, OPERATION_NAME</returns>
        DataTable GetProcesses();

        /// <summary>
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        DataTable GetOperationsByUserForOperationMgr(string domainAdmin, string versionNo);
    }
}
