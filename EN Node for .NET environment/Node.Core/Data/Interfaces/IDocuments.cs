using System;
using System.Data;
using System.IO;

using Node.Core.Document;

namespace Node.Core.Data.Interfaces
{   
    /// <summary>
    /// node file cabin interface for document
    /// </summary>
    public interface IDocuments
    {
        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="fileID">The internal database key for file</param>
        /// <returns>return System.Data.DataTable</returns>
        DataTable GetDocument(int fileID);

        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="stream">InputStream representing the Content of the Document</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="opName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        int UploadDocuments(string name, string type, Stream stream, string transID, string opName, string status, DateTime date, string user);

        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="content">Byte array representing the Content of the Document</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="opName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        int UploadDocuments(string name, string type, byte[] content, string transID, string opName, string status, DateTime date, string user);

        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="doc">Node.Core.Document.NodeDocument</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="opName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        int UploadDocuments(NodeDocument doc, string transID, string opName, string status, DateTime date, string user);

        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="token">Token from EPA</param>
        /// <param name="submitUrl">The Web services URL</param>
        /// <param name="content">Byte array representing the Content of the Document</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="opName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        int UploadDocument(string name, string type, string token, string submitUrl, byte[] content, string transID, string opName, string status, DateTime date, string user);

        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="names">Names of the Documents</param>
        /// <param name="dataFlows">DataFlows of the Documents</param>
        /// <returns></returns>
        NodeDocument[] GetDocuments(string transID, string[] names, string[] dataFlows);

        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="ids">ids of the Documents</param>
        /// <returns></returns>
        NodeDocument[] GetDocuments(string transID, string[] ids);

        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <returns></returns>
        NodeDocument[] GetDocuments(string transID);

        /// <summary>
        /// Removes Documents from the Node Database
        /// If an input parameter is null or not present, they are not included in the search query
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="names">Names of the Documents</param>
        /// <param name="dataFlows">DataFlows of the Documents</param>
        /// <returns>number of rows affected</returns>
        int RemoveDocuments(string transID, string[] names, string[] dataFlows);

        /// <summary>
        /// Update Existing Document(s) in Node Database
        /// If any input query parameter is not null or not present, they are not included in the search query
        /// </summary>
        /// <param name="queryTransID">Transaction ID to Query By</param>
        /// <param name="queryNames">Names of the Documents to Query By</param>
        /// <param name="queryDataFlows">Names of the Dataflows to Query By</param>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="stream">InputStream representing the Content of the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>Number of Rows Affected</returns>
        int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows,
            string name, string type, Stream stream, string status, string user);

        /// <summary>
        /// Update Existing Document(s) in Node Database
        /// If any input query parameter is not null or not present, they are not included in the search query
        /// </summary>
        /// <param name="queryTransID">Transaction ID to Query By</param>
        /// <param name="queryNames">Names of the Documents to Query By</param>
        /// <param name="queryDataFlows">Names of the Dataflows to Query By</param>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="content">Byte array representing the Content of the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>Number of Rows Affected</returns>
        int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows,
            string name, string type, byte[] content, string status, string user);

        /// <summary>
        /// Search the Node Document Database for Documents
        /// </summary>
        /// <param name="docName">The Document Name, null or empty string if not searchable</param>
        /// <param name="transID">The Transaction ID of the Document, null or empty string if not searchable</param>
        /// <param name="domID">The Domain of the Document, -1 if not searchable</param>
        /// <param name="opName">The Operation Name of the Document, null or empty string if not searchable</param>
        /// <param name="start">The Starting Range of the Submit Date</param>
        /// <param name="end">The Ending Range of the Submit Date</param>
        /// <param name="domainAdmin">Name of the Logged in Domain Administrator</param>
        /// <returns>
        /// DataTable with Columns: FILE_ID, FILE_NAME, FILE_TYPE, FILE_SIZE, TRANS_ID, DOMAIN_NAME, DATAFLOW_NAME, SUBMIT_DTTM
        /// </returns>
        DataTable SearchDocuments(string docName, string transID, int domID, string opName, DateTime start, DateTime end, string domainAdmin);

        /// <summary>
        /// Delete Documents with the Document ID's in the input string array parameter
        /// </summary>
        /// <param name="ids">string version of ids to be deleted</param>
        void DeleteDocuments(string[] ids);

        /// <summary>
        /// Update a Document based on the File ID
        /// </summary>
        /// <param name="fileID">FILE_ID. if fileID &lt; 0, then Insert a new Node Document</param>
        /// <param name="transID">TRANS_ID</param>
        /// <param name="fileName">FILE_NAME</param>
        /// <param name="fileType">FILE_TYPE</param>
        /// <param name="statusCD">STATUS_CD</param>
        /// <param name="dataFlowName">DATAFLOW_NAME</param>
        /// <param name="submitURL">SUBMIT_URL</param>
        /// <param name="submitToken">SUBMIT_TOKEN</param>
        /// <param name="submitDate">SUBMIT_DTTM</param>
        /// <param name="content">FILE_CONTENT</param>
        /// <param name="domainAdmin">Name of Administrator Logged In</param>
        /// <returns>FILE_ID that is updated</returns>
        int UpdateDocument(int fileID, string transID, string fileName, string fileType, string statusCD, string dataFlowName, string submitURL,
            string submitToken, DateTime submitDate, Stream content, string domainAdmin);

        /// <summary>
        /// Update a Document based on the File ID
        /// </summary>
        /// <param name="fileID">FILE_ID. if fileID &lt; 0, then Insert a new Node Document</param>
        /// <param name="transID">TRANS_ID</param>
        /// <param name="fileName">FILE_NAME</param>
        /// <param name="fileType">FILE_TYPE</param>
        /// <param name="statusCD">STATUS_CD</param>
        /// <param name="dataFlowName">DATAFLOW_NAME</param>
        /// <param name="submitURL">SUBMIT_URL</param>
        /// <param name="submitToken">SUBMIT_TOKEN</param>
        /// <param name="submitDate">SUBMIT_DTTM</param>
        /// <param name="content">FILE_CONTENT</param>
        /// <param name="domainAdmin">Name of Administrator Logged In</param>
        /// <returns>FILE_ID that is updated</returns>
        int UpdateDocument(int fileID, string transID, string fileName, string fileType, string statusCD, string dataFlowName, string submitURL,
            string submitToken, DateTime submitDate, byte[] content, string domainAdmin);
    }
}
