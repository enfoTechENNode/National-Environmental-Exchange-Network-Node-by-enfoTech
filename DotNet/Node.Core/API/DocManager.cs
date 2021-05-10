using System;
using System.IO;

using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Core.Document;
using Node.Core.Util;

namespace Node.Core.API
{
    
    /// <summary>
    /// Database Object for accessing NODE_FILE_CABIN.
    /// </summary>
    public class DocManager
    {
        /// <summary>
        /// E_INTERNAL_ERROR = -1.
        /// </summary>
        public static int E_INTERNAL_ERROR = -1;
        /// <summary>
        /// E_NO_FILE = -2
        /// </summary>
        public static int E_NO_FILE = -2;
        /// <summary>
        /// E_NO_NAME = -3
        /// </summary>
        public static int E_NO_NAME = -3;
        /// <summary>
        /// E_NO_TYPE = -4
        /// </summary>
        public static int E_NO_TYPE = -4;

        /// <summary>
        /// This method stores data into the Node Database.
        /// Any files uploaded to the Node Database can be retrieved through the Document Management module of the Node.Administration application.
        /// Files on the Node Database also can be retrieved via the GetDocuments() method of this class.
        /// </summary>
        /// <param name="name">Name of the document.</param>
        /// <param name="type">Type of the document, such as "XML", "ZIP", or "OTHER".</param>
        /// <param name="stream">The stream that contains the content of the document.</param>
        /// <param name="transID">The transaction ID this document is associated with.  If null or empty, a transaction ID is generated before the document is stored.</param>
        /// <param name="opName">The name of the Node Operation that this document is associated with.</param>
        /// <param name="status">The status of the document.</param>
        /// <param name="date">The date the document is associated with.</param>
        /// <param name="user">The user who uploads the document.  Should indicate which piece of code uploads the document if this call is made through operation code.</param>
        /// <returns>The document ID that is stored in the database of the new document.</returns>
        public int UploadDocuments(string name, string type, Stream stream, string transID, string opName, string status, DateTime date, string user)
        {
            int retInt = DocManager.E_INTERNAL_ERROR;
            if (stream != null && stream.Length > 0)
            {
                if (name != null && !name.Trim().Equals(""))
                {
                    if (type != null && !type.Trim().Equals(""))
                    {
                        string tid = transID != null && !transID.Trim().Equals("") ? transID : new NodeUtility().GenerateTransactionID();
                        string stat = status != null && !status.Trim().Equals("") ? status : "Ready";
                        IDocuments docDB = new DBManager().GetDocumentsDB();
                        try
                        {
                            retInt = docDB.UploadDocuments(name, type, stream, tid, opName, stat, date, user);
                        }
                        catch (Exception e)
                        {
                            retInt = DocManager.E_INTERNAL_ERROR;
                            throw e;
                        }
                    }
                    else
                        retInt = DocManager.E_NO_TYPE;
                }
                else
                    retInt = DocManager.E_NO_NAME;
            }
            else
                retInt = DocManager.E_NO_FILE;
            return retInt;
        }

        /// <summary>
        /// This method stores data into the Node Database.
        /// Any files uploaded to the Node Database can be retrieved through the Document Management module of the Node.Administration application.
        /// Files on the Node Database also can be retrieved via the GetDocuments() method of this class.
        /// </summary>
        /// <param name="name">Name of the document.</param>
        /// <param name="type">Type of the document, such as "XML", "ZIP", or "OTHER".</param>
        /// <param name="content">The array of byte that contains the content of the document.</param>
        /// <param name="transID">The transaction ID this document is associated with.  If null or empty, a transaction ID is generated before the document is stored.</param>
        /// <param name="opName">The name of the Node Operation that this document is associated with.</param>
        /// <param name="status">The status of the document.</param>
        /// <param name="date">The date the document is associated with.</param>
        /// <param name="user">The user who uploads the document.  Should indicate which piece of code uploads the document if this call is made through operation code.</param>
        /// <returns>The document ID that is stored in the database of the new document.</returns>
        public int UploadDocuments(string name, string type, byte[] content, string transID, string opName, string status, DateTime date, string user)
        {
            int retInt = DocManager.E_INTERNAL_ERROR;
            if (content != null && content.Length > 0)
            {
                if (name != null && !name.Trim().Equals(""))
                {
                    if (type != null && !type.Trim().Equals(""))
                    {
                        string tid = transID != null && !transID.Trim().Equals("") ? transID : new NodeUtility().GenerateTransactionID();
                        string stat = status != null && !status.Trim().Equals("") ? status : "Ready";
                        IDocuments docDB = new DBManager().GetDocumentsDB();
                        try
                        {
                            retInt = docDB.UploadDocuments(name, type, content, tid, opName, stat, date, user);
                        }
                        catch (Exception e)
                        {
                            retInt = DocManager.E_INTERNAL_ERROR;
                            throw e;
                        }
                    }
                    else
                        retInt = DocManager.E_NO_TYPE;
                }
                else
                    retInt = DocManager.E_NO_NAME;
            }
            else
                retInt = DocManager.E_NO_FILE;
            return retInt;
        }

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
        public int UploadDocuments(NodeDocument doc, string transID, string opName, string status, DateTime date, string user)
        {
            int retInt = DocManager.E_INTERNAL_ERROR;
            if ((doc.content != null && doc.content.Length > 0) || (doc.Stream != null && doc.Stream.Length >0))
            {
                if (doc.name != null && !doc.name.Trim().Equals(""))
                {
                    if (doc.type != null && !doc.type.Trim().Equals(""))
                    {
                        string tid = transID != null && !transID.Trim().Equals("") ? transID : new NodeUtility().GenerateTransactionID();
                        string stat = status != null && !status.Trim().Equals("") ? status : "Ready";
                        IDocuments docDB = new DBManager().GetDocumentsDB();
                        try
                        {
                            retInt = docDB.UploadDocuments(doc,tid, opName, stat, date, user);
                        }
                        catch (Exception e)
                        {
                            retInt = DocManager.E_INTERNAL_ERROR;
                            throw e;
                        }
                    }
                    else
                        retInt = DocManager.E_NO_TYPE;
                }
                else
                    retInt = DocManager.E_NO_NAME;
            }
            else
                retInt = DocManager.E_NO_FILE;
            return retInt;
        }

        /// <summary>
        /// This method queries the Node Database for documents stored on the Node Database.
        /// If parameters are null or empty, they are not included in the search criteria.
        /// </summary>
        /// <param name="transID">The transaction ID of the document to search by.</param>
        /// <param name="names">A list of document names to search by.</param>
        /// <param name="dataFlows">A list of data flows to search by.</param>
        /// <returns>The <see cref="Node.Core.Document.NodeDocument">NodeDocument</see> array that is returned via the query.</returns>
        public NodeDocument[] GetDocuments(string transID, string[] names, string[] dataFlows)
        {
            return new DBManager().GetDocumentsDB().GetDocuments(transID, names, dataFlows);
        }

        /// <summary>
        /// This method queries the Node Database for documents stored on the Node Database.
        /// If parameters are null or empty, they are not included in the search criteria.
        /// </summary>
        /// <param name="transID">The transaction ID of the document to search by.</param>
        /// <param name="ids">A list of document ids to search by.</param>
        /// <returns>The <see cref="Node.Core.Document.NodeDocument">NodeDocument</see> array that is returned via the query.</returns>
        public NodeDocument[] GetDocuments(string transID, string[] ids)
        {
            return new DBManager().GetDocumentsDB().GetDocuments(transID, ids);
        }

        /// <summary>
        /// This method queries the Node Database for documents stored on the Node Database.
        /// If parameters are null or empty, they are not included in the search criteria.
        /// </summary>
        /// <param name="transID">The transaction ID of the document to search by.</param>
        /// <returns>The <see cref="Node.Core.Document.NodeDocument">NodeDocument</see> array that is returned via the query.</returns>
        public NodeDocument[] GetDocuments(string transID)
        {
            return new DBManager().GetDocumentsDB().GetDocuments(transID);
        }

        /// <summary>
        /// This method removes documents from the Node Database.
        /// If parameters are null or empty, they are not included in the search criteria.
        /// At least one parameter must be non-null and not empty for the removal to take place.
        /// </summary>
        /// <param name="transID">The transaction ID of the document to search by.</param>
        /// <param name="names">A list of document names to search by.</param>
        /// <param name="dataFlows">A list of data flows to search by.</param>
        /// <returns>The number of documents that were removed from the Node Database.</returns>
        public int RemoveDocuments(string transID, string[] names, string[] dataFlows)
        {
            return new DBManager().GetDocumentsDB().RemoveDocuments(transID, names, dataFlows);
        }

        /// <summary>
        /// This method updates existing documents on the Node Database.
        /// The first three parameters (queryTransID, queryNames, and qeuryDataFlows) are used to query the documents to update.
        /// The rest of the parameters are the values to update the documents with.
        /// </summary>
        /// <param name="queryTransID">The transaction ID the document is associated with.</param>
        /// <param name="queryNames">The list of document names.</param>
        /// <param name="queryDataFlows">The list of data flow names.</param>
        /// <param name="name">The updated name of the document.</param>
        /// <param name="type">The updated type of the document.</param>
        /// <param name="stream">The updated content of the document.</param>
        /// <param name="status">The updated status of the document.</param>
        /// <param name="user">The user who updates the document.  This should be some indication of the code that calls the update if called through operation code.</param>
        /// <returns>The number of documents that are updated.</returns>
        public int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows, string name, string type, Stream stream, string status, string user)
        {
            int retInt = DocManager.E_INTERNAL_ERROR;
            if (stream != null && stream.Length > 0)
            {
                if (name != null && !name.Trim().Equals(""))
                {
                    if (type != null && !type.Trim().Equals(""))
                    {
                        string stat = status != null && !status.Trim().Equals("") ? status : "Ready";
                        IDocuments docDB = new DBManager().GetDocumentsDB();
                        try
                        {
                            retInt = docDB.UpdateDocuments(queryTransID, queryNames, queryDataFlows,
                                name, type, stream, stat, user);
                        }
                        catch (Exception e)
                        {
                            retInt = DocManager.E_INTERNAL_ERROR;
                            throw e;
                        }
                    }
                    else
                        retInt = DocManager.E_NO_TYPE;
                }
                else
                    retInt = DocManager.E_NO_NAME;
            }
            else
                retInt = DocManager.E_NO_FILE;
            return retInt;
        }
        
        /// <summary>
        /// This method updates existing documents on the Node Database.
        /// The first three parameters (queryTransID, queryNames, and qeuryDataFlows) are used to query the documents to update.
        /// The rest of the parameters are the values to update the documents with.
        /// </summary>
        /// <param name="queryTransID">The transaction ID the document is associated with.</param>
        /// <param name="queryNames">The list of document names.</param>
        /// <param name="queryDataFlows">The list of data flow names.</param>
        /// <param name="name">The updated name of the document.</param>
        /// <param name="type">The updated type of the document.</param>
        /// <param name="content">The updated content of the document.</param>
        /// <param name="status">The updated status of the document.</param>
        /// <param name="user">The user who updates the document.  This should be some indication of the code that calls the update if called through operation code.</param>
        /// <returns>The number of documents that are updated.</returns>
        public int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows, string name, string type, byte[] content, string status, string user)
        {
            int retInt = DocManager.E_INTERNAL_ERROR;
            if (content != null && content.Length > 0)
            {
                if (name != null && !name.Trim().Equals(""))
                {
                    if (type != null && !type.Trim().Equals(""))
                    {
                        string stat = status != null && !status.Trim().Equals("") ? status : "Ready";
                        IDocuments docDB = new DBManager().GetDocumentsDB();
                        try
                        {
                            retInt = docDB.UpdateDocuments(queryTransID, queryNames, queryDataFlows,
                                name, type, content, stat, user);
                        }
                        catch (Exception e)
                        {
                            retInt = DocManager.E_INTERNAL_ERROR;
                            throw e;
                        }
                    }
                    else
                        retInt = DocManager.E_NO_TYPE;
                }
                else
                    retInt = DocManager.E_NO_NAME;
            }
            else
                retInt = DocManager.E_NO_FILE;
            return retInt;
        }

    }
}
