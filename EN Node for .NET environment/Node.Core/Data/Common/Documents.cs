using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web;

using Node.Core.Data.Interfaces;
using Node.Core.Document;
using Node.Lib.Data;

namespace Node.Core.Data.Common
{
    /// <summary>
    /// node file cabin database object for document
    /// </summary>
    public class Documents : BaseData, IDocuments
    {
        /// <summary>
        /// node file cabin function for document
        /// </summary>
        public Documents()
        {
        }
        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="fileID">The internal database key for file</param>
        /// <returns>return System.Data.DataTable</returns>
        public DataTable GetDocument(int fileID)
        {
            DataTable retDT = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileID + ", A." + this.TransID + ", A." + this.FileName;
                command += ", A." + this.FileType + ", A." + this.Status + ", A." + this.DataFlowName;
                command += ", A." + this.SubmitURL + ", A." + this.SubmitToken;
                command += ", A." + this.SubmitDate + ", A." + this.Content;
                command += ", A." + this.Size + ", A." + this.CreatedDate + ", A." + this.CreatedBy;
                command += ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                command += ", C." + this.DomName;
                command += " from " + this.TblFileCabin + " A left join " + this.TblOperation;
                command += " B on A." + this.DataFlowName + " = B." + this.OpName;
                command += " left join " + this.TblDomain + " C on B." + this.DomID + " = C." + this.DomID;
                command += " where A." + this.FileID + " = @" + this.FileID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.FileID, fileID));
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                    retDT = dt;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retDT;
        }
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
        public int UploadDocuments(string name, string type, Stream stream, string transID, string opName, string status, DateTime date, string user)
        {
            int fileID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                string command = "insert into " + this.TblFileCabin + " (" + this.FileID;
                command += ", " + this.DataFlowName;
                command += ", " + this.TransID + ", " + this.FileName + ", " + this.FileType;
                command += ", " + this.Status + ", " + this.SubmitDate + ", " + this.Content;
                command += ", " + this.Size + ", " + this.CreatedDate + ", " + this.CreatedBy;
                command += ", " + this.UpdatedDate;
                if (user != null)
                    command += ", " + this.UpdatedBy;
                command += ")";
                int newID = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                command += " values (" + newID + ", @" + this.DataFlowName;
                command += ", @" + this.TransID + ", @" + this.FileName + ", @" + this.FileType;
                command += ", @" + this.Status + ", @" + this.SubmitDate + ", @" + this.Content;
                if (stream != null)
                    command += ", " + stream.Length;
                else
                    command += ", 0";
                command += ", @" + this.CreatedDate + ", @" + this.CreatedBy;
                command += ", @" + this.UpdatedDate;
                if (user != null)
                    command += ", @" + this.UpdatedBy;
                command += ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DataFlowName, opName));
                parameters.Add(new Parameter(this.TransID, transID));
                parameters.Add(new Parameter(this.FileName, name));
                parameters.Add(new Parameter(this.FileType, type));
                parameters.Add(new Parameter(this.Status, status));
                parameters.Add(new Parameter(this.SubmitDate, date));

                byte[] content = null;
                if (stream != null && stream.Length > 0)
                {
                    content = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(content, 0, (int)stream.Length);
                }
                parameters.Add(new Parameter(this.Content, content));
                parameters.Add(new Parameter(this.CreatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.CreatedBy, user != null ? user : ""));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                if (user != null)
                    parameters.Add(new Parameter(this.UpdatedBy, user));

                int count = db.ExecuteNonQuery(command, parameters);
                if (count > 0)
                {
                    fileID = newID;
                    db.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return fileID;
        }
        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="names">Names of the Documents</param>
        /// <param name="dataFlows">DataFlows of the Documents</param>
        /// <returns></returns>
        public NodeDocument[] GetDocuments(string transID, string[] names, string[] dataFlows)
        {
            if ((transID == null || transID.Trim().Equals("")) && (names == null || names.Length <= 0) && (dataFlows == null || dataFlows.Length <= 0))
                return null;
            NodeDocument[] retDocs = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileName + ", A." + this.FileType + ", A." + this.Content;
                command += " from " + this.TblFileCabin + " A";
                command += " where";
                if (transID != null && !transID.Trim().Equals(""))
                    command += " A." + this.TransID + " = @" + this.TransID;
                //command += " A." + this.TransID + " = '" + transID + "'";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));
                if (names != null && names.Length > 0)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " A." + this.FileName + " in (";
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] != null)
                        {
                            if (!command.EndsWith("(")) command += ", ";
                                command += "@" + this.FileName + i;
                            //command += "'" + names[i] + "'";
                            parameters.Add(new Parameter(this.FileName + i, names[i]));
                        }
                    }
                    command += ")";
                }
                bool foundDataFlow = false;
                if (dataFlows != null)
                {
                    foreach (string df in dataFlows)
                    {
                        if (df != null && !df.Trim().Equals(""))
                        {
                            foundDataFlow = true;
                            break;
                        }
                    }
                }
                if (foundDataFlow)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " A." + this.DataFlowName + " in (";
                    for (int i = 0; i < dataFlows.Length; i++)
                    {
                        if (dataFlows[i] != null)
                        {
                            if (!command.EndsWith("(")) command += ", ";
                            command += "@" + this.DataFlowName + i;
                            parameters.Add(new Parameter(this.DataFlowName + i, dataFlows[i]));
                        }
                    }
                    command += ")";
                }
                if (command.EndsWith("where"))
                    return null;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDocs = new NodeDocument[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retDocs[i] = new NodeDocument();
                        object obj = dt.Rows[i][this.FileName];
                        if (obj != null)
                            retDocs[i].name = (string)obj;
                        obj = dt.Rows[i][this.FileType];
                        if (obj != null)
                            retDocs[i].type = (string)obj;
                        obj = dt.Rows[i][this.Content];
                        if (obj != null)
                            retDocs[i].content = (byte[])obj;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retDocs;
        }
        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="ids">Names of the Documents</param>
        /// <returns></returns>
        public NodeDocument[] GetDocuments(string transID, string[] ids)
        {
            if ((transID == null || transID.Trim().Equals("")) && (ids == null || ids.Length <= 0))
                return null;
            NodeDocument[] retDocs = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileName + ", A." + this.FileType + ", A." + this.Content;
                command += ", A." + this.DocID;
                command += " from " + this.TblFileCabin + " A";
                command += " where";
                if (transID != null && !transID.Trim().Equals(""))
                    command += " A." + this.TransID + " = @" + this.TransID;

                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));
                if (ids != null && ids.Length > 0)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " A." + this.DocID + " in (";
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (ids[i] != null)
                        {
                            if (!command.EndsWith("(")) command += ",";
                            command += "@" + this.DocID + i;
                            //command += "'" + names[i] + "'";
                            parameters.Add(new Parameter(this.DocID + i, ids[i]));
                        }
                    }
                    command += ")";
                }
                
                if (command.EndsWith("where"))
                    return null;
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDocs = new NodeDocument[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retDocs[i] = new NodeDocument();
                        object obj = dt.Rows[i][this.FileName];
                        if (obj != null)
                            retDocs[i].name = (string)obj;
                        obj = dt.Rows[i][this.FileType];
                        if (obj != null)
                            retDocs[i].type = (string)obj;
                        obj = dt.Rows[i][this.Content];
                        if (obj != null)
                            retDocs[i].content = (byte[])obj;
                        obj = dt.Rows[i][this.DocID];
                        if (obj != null)
                            retDocs[i].href = (string)obj;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retDocs;
        }

        /// <summary>
        /// Gets Documents from Node Database
        /// Only retrieves those documents that meet the search criteria of the input parameters
        /// If any of the input parameters are null or not present, they are not included in the search criteria
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <returns></returns>
        public NodeDocument[] GetDocuments(string transID)
        {
            if ((transID == null || transID.Trim().Equals("")))
                return null;
            NodeDocument[] retDocs = null;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileName + ", A." + this.FileType + ", A." + this.Content;
                command += " from " + this.TblFileCabin + " A";
                command += " where";
                if (transID != null && !transID.Trim().Equals(""))
                    command += " A." + this.TransID + " = @" + this.TransID;
                //command += " A." + this.TransID + " = '" + transID + "'";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.TransID, transID));

                if (command.EndsWith("where"))
                    return null;

                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    retDocs = new NodeDocument[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        retDocs[i] = new NodeDocument();
                        object obj = dt.Rows[i][this.FileName];
                        if (obj != null)
                            retDocs[i].name = (string)obj;
                        obj = dt.Rows[i][this.FileType];
                        if (obj != null)
                            retDocs[i].type = (string)obj;
                        obj = dt.Rows[i][this.Content];
                        if (obj != null)
                            retDocs[i].content = (byte[])obj;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return retDocs;
        }
        /// <summary>
        /// Removes Documents from the Node Database
        /// If an input parameter is null or not present, they are not included in the search query
        /// </summary>
        /// <param name="transID">Transaction ID of the Documents</param>
        /// <param name="names">Names of the Documents</param>
        /// <param name="dataFlows">DataFlows of the Documents</param>
        /// <returns>number of rows affected</returns>
        public int RemoveDocuments(string transID, string[] names, string[] dataFlows)
        {
            int numRows = 0;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                if ((transID != null && !transID.Trim().Equals("")) ||
                    (names != null && names.Length > 0) ||
                    (dataFlows != null && dataFlows.Length > 0))
                {
                    string command = "delete from " + this.TblFileCabin;
                    ArrayList parameters = new ArrayList();
                    if (transID != null && !transID.Trim().Equals(""))
                    {
                        command += " where " + this.TransID + " = @" + this.TransID;
                        parameters.Add(new Parameter(this.TransID, transID));
                    }
                    if (names != null && names.Length > 0)
                    {
                        if (command.Contains("where"))
                            command += " and ";
                        else
                            command += " where ";
                        command += this.FileName + " in (";
                        for (int i = 0; i < names.Length; i++)
                        {
                            if (i != 0) command += ", ";
                            command += "'" + names[i] + "'";
                            parameters.Add(new Parameter(this.FileName + i, names[i]));
                        }
                        command += ")";
                    }
                    if (dataFlows != null && dataFlows.Length > 0)
                    {
                        if (command.Contains("where"))
                            command += " and ";
                        else
                            command += " where ";
                        command += this.DataFlowName + " in (";
                        for (int i = 0; i < dataFlows.Length; i++)
                        {
                            if (i != 0) command += ", ";
                            command += "'" + dataFlows[i] + "'";
                            parameters.Add(new Parameter(this.DataFlowName + i, dataFlows[i]));
                        }
                        command += ")";
                    }
                    numRows = db.ExecuteNonQuery(command, parameters);
                }
            }
            catch (Exception e)
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                    db = null;
                }
                throw e;
            }
            finally
            {
                if (db != null)
                {
                    db.CommitTransaction();
                    db.Close();
                }
            }
            return numRows;
        }
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
        public int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows,
            string name, string type, Stream stream, string status, string user)
        {
            int numRows = 0;
            if ((queryTransID == null || queryTransID.Trim().Equals("")) && (queryNames == null || queryNames.Length <= 0) && (queryDataFlows == null || queryDataFlows.Length <= 0))
                return numRows;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileID + " from " + this.TblFileCabin + " A";
                ArrayList parameters = new ArrayList();
                command += " where";
                if (queryTransID != null && !queryTransID.Trim().Equals(""))
                {
                    command += " A." + this.TransID + " = @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, queryTransID));
                }
                bool foundFileName = false;
                if (queryNames != null && queryNames.Length > 0)
                {
                    foreach (string qn in queryNames)
                    {
                        if (qn != null && !qn.Trim().Equals(""))
                        {
                            foundFileName = true;
                            break;
                        }
                    }
                }
                if (foundFileName)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " A." + this.FileName + " in (";
                    for (int i = 0; i < queryNames.Length; i++)
                    {
                        if (i != 0) command += ", ";
                        command += "@" + this.FileName + i;
                        parameters.Add(new Parameter(this.FileName + i, queryNames[i]));
                    }
                    command += ")";
                }
                bool foundDataFlow = false;
                if (queryDataFlows != null && queryDataFlows.Length > 0)
                {
                    foreach (string df in queryDataFlows)
                    {
                        if (df != null && !df.Trim().Equals(""))
                        {
                            foundDataFlow = true;
                            break;
                        }
                    }
                }
                if (foundDataFlow)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " B." + this.DataFlowName + " in (";
                    for (int i = 0; i < queryDataFlows.Length; i++)
                    {
                        if (i != 0) command += ", ";
                        command += "@" + this.DataFlowName + i;
                        parameters.Add(new Parameter(this.DataFlowName + i, queryDataFlows[i]));
                    }
                    command += ")";
                }
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string updateCommand = "select A." + this.FileID + ", A." + this.FileName;
                    updateCommand += ", A." + this.FileType + ", A." + this.Status;
                    updateCommand += ", A." + this.Content + ", A." + this.Size;
                    updateCommand += ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                    updateCommand += " from " + this.TblFileCabin + " A";
                    updateCommand += " where A." + this.FileID + " in (";
                    ArrayList updateParameters = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0) updateCommand += ", ";
                        updateCommand += "@" + this.FileID + i;
                        updateParameters.Add(new Parameter(this.FileID + i, dt.Rows[i][this.FileID]));
                    }
                    DataTable updateDT = new DataTable();
                    db.GetDataTable(this.TblFileCabin, updateCommand, updateParameters, updateDT);
                    if (updateDT != null && updateDT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in updateDT.Rows)
                        {
                            dr[this.FileName] = name;
                            dr[this.FileType] = type;
                            dr[this.Status] = status;
                            if (stream != null && stream.Length > 0)
                            {
                                byte[] content = new byte[stream.Length];
                                stream.Position = 0;
                                stream.Read(content, 0, (int)stream.Length);
                                dr[this.Content] = content;
                                dr[this.Size] = Content.Length;
                            }
                            else
                            {
                                dr[this.Content] = DBNull.Value;
                                dr[this.Size] = 0;
                            }
                            dr[this.UpdatedDate] = DateTime.Now;
                            dr[this.UpdatedBy] = user;
                        }
                        numRows = db.UpdateDataTableBatch(this.TblFileCabin, updateDT, updateDT.Rows.Count);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return numRows;
        }
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
        public int UpdateDocuments(string queryTransID, string[] queryNames, string[] queryDataFlows,
            string name, string type, byte[] content, string status, string user)
        {
            int numRows = 0;
            if ((queryTransID == null || queryTransID.Trim().Equals("")) && (queryNames == null || queryNames.Length <= 0) && (queryDataFlows == null || queryDataFlows.Length <= 0))
                return numRows;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                string command = "select A." + this.FileID + " from " + this.TblFileCabin + " A";
                ArrayList parameters = new ArrayList();
                command += " where";
                if (queryTransID != null && !queryTransID.Trim().Equals(""))
                {
                    command += " A." + this.TransID + " = @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, queryTransID));
                }
                bool foundFileName = false;
                if (queryNames != null && queryNames.Length > 0)
                {
                    foreach (string qn in queryNames)
                    {
                        if (qn != null && !qn.Trim().Equals(""))
                        {
                            foundFileName = true;
                            break;
                        }
                    }
                }
                if (foundFileName)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " A." + this.FileName + " in (";
                    for (int i = 0; i < queryNames.Length; i++)
                    {
                        if (i != 0)
                            command += ", ";
                        command += "@" + this.FileName + i;
                        parameters.Add(new Parameter(this.FileName + i, queryNames[i]));
                    }
                    command += ")";
                }
                bool foundDataFlow = false;
                if (queryDataFlows != null && queryDataFlows.Length > 0)
                {
                    foreach (string df in queryDataFlows)
                    {
                        if (df != null && !df.Trim().Equals(""))
                        {
                            foundDataFlow = true;
                            break;
                        }
                    }
                }
                if (foundDataFlow)
                {
                    if (!command.EndsWith("where"))
                        command += " and";
                    command += " B." + this.DataFlowName + " in (";
                    for (int i = 0; i < queryDataFlows.Length; i++)
                    {
                        if (i != 0)
                            command += ", ";
                        command += "@" + this.DataFlowName + i;
                        parameters.Add(new Parameter(this.DataFlowName + i, queryDataFlows[i]));
                    }
                    command += ")";
                }
                DataTable dt = new DataTable();
                db.GetDataTable(this.TblFileCabin, command, parameters, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string updateCommand = "select A." + this.FileID + ", A." + this.FileName;
                    updateCommand += ", A." + this.FileType + ", A." + this.Status;
                    updateCommand += ", A." + this.Content + ", A." + this.Size;
                    updateCommand += ", A." + this.UpdatedDate + ", A." + this.UpdatedBy;
                    updateCommand += " from " + this.TblFileCabin + " A";
                    updateCommand += " where A." + this.FileID + " in (";
                    ArrayList updateParameters = new ArrayList();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i != 0)
                            updateCommand += ", ";
                        updateCommand += "@" + this.FileID + i;
                        updateParameters.Add(new Parameter(this.FileID + i, dt.Rows[i][this.FileID]));
                    }
                    DataTable updateDT = new DataTable();
                    db.GetDataTable(this.TblFileCabin, updateCommand, updateParameters, updateDT);
                    if (updateDT != null && updateDT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in updateDT.Rows)
                        {
                            dr[this.FileName] = name;
                            dr[this.FileType] = type;
                            dr[this.Status] = status;
                            if (content != null && content.Length > 0)
                            {
                                dr[this.Content] = content;
                                dr[this.Size] = content.Length;
                            }
                            else
                            {
                                dr[this.Content] = DBNull.Value;
                                dr[this.Size] = 0;
                            }
                            dr[this.UpdatedDate] = DateTime.Now;
                            dr[this.UpdatedBy] = user;
                        }
                        numRows = db.UpdateDataTableBatch(this.TblFileCabin, updateDT, updateDT.Rows.Count);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return numRows;
        }
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
        public DataTable SearchDocuments(string docName, string transID, int domID, string opName, DateTime start, DateTime end, string domainAdmin)
        {
            DataTable table = null;
            DBAdapter db = null;
            string versionNo = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
                    versionNo = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
                else
                    versionNo = Phrase.VERSION_11;

                string sql = "select distinct A." + this.FileID + ", A." + this.FileName + ", A." + this.FileType + ", A." + this.Size;
                sql += ", A." + this.TransID + ", C." + this.DomName + ", A." + this.DataFlowName + ", A." + this.SubmitDate;
                sql += " from " + this.TblFileCabin + " A left join " + this.TblOperation + " B on A." + this.DataFlowName + " = B." + this.OpName;
                sql += " left join " + this.TblDomain + " C on B." + this.DomID + " = C." + this.DomID;
                sql += ", " + this.TblUser + " D, " + this.TblAccTypeXREF + " E, " + this.TblAccType + " F";
                sql += ", " + this.TblDomain + " G";
                sql += " where A." + this.SubmitDate + " > @START_DATE and A." + this.SubmitDate + " < @END_DATE";
                sql += " and B." + this.OpVersionNo + " = '" + versionNo + "'";
                sql += " and D." + this.LoginName + " = @" + this.LoginName + " and D." + this.UserID + " = E." + this.UserID;
                sql += " and E." + this.AccTypeID + " = F." + this.AccTypeID + " and F." + this.AccType + " = '" + Phrase.CONSOLE_USER + "'";
                sql += " and E." + this.DomID + " = G." + this.DomID;
                //sql += " and ((G." + this.DomName + " = 'NODE') or (G." + this.DomID + " = C." + this.DomID;
                sql += " and G." + this.DomID + " = C." + this.DomID;
                sql += " and C." + this.DomID + " = B." + this.DomID + " and B." + this.OpName + " = A." + this.DataFlowName;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter("START_DATE", start));
                parameters.Add(new Parameter("END_DATE", end.AddDays(1)));
                parameters.Add(new Parameter(this.LoginName, domainAdmin));
                if (docName != null && !docName.Trim().Equals(""))
                {
                    sql += " and A." + this.FileName + " like @" + this.FileName;
                    parameters.Add(new Parameter(this.FileName, docName));
                }
                if (transID != null && !transID.Trim().Equals(""))
                {
                    sql += " and A." + this.TransID + " like @" + this.TransID;
                    parameters.Add(new Parameter(this.TransID, transID));
                }
                if (domID >= 0)
                {
                    sql += " and C." + this.DomID + " = @" + this.DomID;
                    parameters.Add(new Parameter(this.DomID, domID));
                }
                if (opName != null && !opName.Trim().Equals(""))
                {
                    sql += " and A." + this.DataFlowName + " like @" + this.DataFlowName;
                    parameters.Add(new Parameter(this.DataFlowName, opName));
                }
                sql += " group by A." + this.FileID + ", A." + this.FileName + ", A." + this.FileType + ", A." + this.Size;
                sql += ", A." + this.TransID + ", C." + this.DomName + ", A." + this.DataFlowName + ", A." + this.SubmitDate;
                sql += " order by A." + this.SubmitDate + " desc";
                DataSet ds = new DataSet();

                db = this.GetNodeDB();
                if (HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM] != null
                    && HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString() != "-1")
                {
                    string rownum = HttpContext.Current.Session[Phrase.DEFAULT_ROWNUM].ToString();
                    if (db.ProviderName.Trim() == DBAdapter.MSSQL_Provider)
                    {
                        sql = sql.Replace("select distinct", "select distinct top " + rownum + " ");
                    }
                    else
                    {
                        sql = "SELECT * FROM (" + sql + ")WHERE ROWNUM <=" + rownum;
                    }
                }
                db.GetDataSet(this.TblFileCabin, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblFileCabin))
                    table = ds.Tables[this.TblFileCabin];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return table;
        }
        /// <summary>
        /// Delete Documents with the Document ID's in the input string array parameter
        /// </summary>
        /// <param name="ids">string version of ids to be deleted</param>
        public void DeleteDocuments(string[] ids)
        {
            if (ids == null || ids.Length <= 0)
                return;
            DBAdapter db = null;
            try
            {
                string sql = "delete from " + this.TblFileCabin;
                sql += " where " + this.FileID + " in (";
                ArrayList parameters = new ArrayList();
                for (int i = 0; i < ids.Length; i++)
                {
                    if (i != 0) sql += ", ";
                    sql += "@" + this.FileID + i;
                    parameters.Add(new Parameter(this.FileID + i, int.Parse(ids[i])));
                }
                sql += ")";
                db = this.GetNodeDB();
                db.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
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
        public int UpdateDocument(int fileID, string transID, string fileName, string fileType, string statusCD, string dataFlowName, string submitURL,
            string submitToken, DateTime submitDate, Stream content, string domainAdmin)
        {
            int id = -1;
            DBAdapter db = null;
            try
            {
                string sql = "select * from " + this.TblFileCabin;
                sql += " where " + this.FileID + " = @" + this.FileID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.FileID, fileID));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblFileCabin, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblFileCabin))
                {
                    DataTable dt = ds.Tables[this.TblFileCabin];
                    DataRow dr = null;
                    bool isInsert = false;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        id = fileID;
                    }
                    else
                    {
                        isInsert = true;
                        dr = dt.NewRow();
                        id = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                        dr[this.FileID] = id;
                    }
                    dr[this.TransID] = transID;
                    dr[this.FileName] = fileName;
                    dr[this.FileType] = fileType;
                    dr[this.Status] = statusCD;
                    dr[this.DataFlowName] = dataFlowName;
                    dr[this.SubmitURL] = submitURL;
                    dr[this.SubmitToken] = submitToken;
                    dr[this.SubmitDate] = submitDate;
                    byte[] bytes = null;
                    if (content.Length > 0)
                    {
                        bytes = new byte[(int)content.Length];
                        content.Position = 0;
                        content.Read(bytes, 0, bytes.Length);
                        dr[this.Content] = bytes;
                        dr[this.Size] = bytes.Length;
                    }
                    else
                    {
                        dr[this.Content] = DBNull.Value;
                        dr[this.Size] = 0;
                    }
                    dr[this.UpdatedDate] = DateTime.Now;
                    dr[this.UpdatedBy] = domainAdmin;
                    if (isInsert)
                    {
                        dr[this.CreatedDate] = DateTime.Now;
                        dr[this.CreatedBy] = domainAdmin;
                        dt.Rows.Add(dr);
                    }
                    db.UpdateDataSet(this.TblFileCabin, ds);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return id;
        }
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
        public int UpdateDocument(int fileID, string transID, string fileName, string fileType, string statusCD, string dataFlowName, string submitURL,
            string submitToken, DateTime submitDate, byte[] content, string domainAdmin)
        {
            int id = -1;
            DBAdapter db = null;
            try
            {
                string sql = "select * from " + this.TblFileCabin;
                sql += " where " + this.FileID + " = @" + this.FileID;
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.FileID, fileID));
                DataSet ds = new DataSet();
                db = this.GetNodeDB();
                db.GetDataSet(this.TblFileCabin, sql, parameters, ds);
                if (ds.Tables.Contains(this.TblFileCabin))
                {
                    DataTable dt = ds.Tables[this.TblFileCabin];
                    DataRow dr = null;
                    bool isInsert = false;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        id = fileID;
                    }
                    else
                    {
                        isInsert = true;
                        dr = dt.NewRow();
                        id = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                        dr[this.FileID] = id;
                    }
                    dr[this.TransID] = transID;
                    dr[this.FileName] = fileName;
                    dr[this.FileType] = fileType;
                    dr[this.Status] = statusCD;
                    dr[this.DataFlowName] = dataFlowName;
                    dr[this.SubmitURL] = submitURL;
                    dr[this.SubmitToken] = submitToken;
                    dr[this.SubmitDate] = submitDate;
                    //byte[] bytes = null;
                    if (content.Length > 0)
                    {
                        dr[this.Content] = content;
                        dr[this.Size] = content.Length;
                    }
                    else
                    {
                        dr[this.Content] = DBNull.Value;
                        dr[this.Size] = 0;
                    }
                    dr[this.UpdatedDate] = DateTime.Now;
                    dr[this.UpdatedBy] = domainAdmin;
                    if (isInsert)
                    {
                        dr[this.CreatedDate] = DateTime.Now;
                        dr[this.CreatedBy] = domainAdmin;
                        dt.Rows.Add(dr);
                    }
                    db.UpdateDataSet(this.TblFileCabin, ds);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return id;
        }
        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="content">Byte array representing the Content of the Document</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="dataFlowName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        public int UploadDocuments(string name, string type, byte[] content, string transID, string dataFlowName, string status, DateTime date, string user)
        {
            int fileID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                string command = "insert into " + this.TblFileCabin + " (" + this.FileID;
                command += ", " + this.DataFlowName;
                command += ", " + this.TransID + ", " + this.FileName + ", " + this.FileType;
                command += ", " + this.Status + ", " + this.SubmitDate + ", " + this.Content;
                command += ", " + this.Size + ", " + this.CreatedDate + ", " + this.CreatedBy;
                command += ", " + this.UpdatedDate;
                if (user != null)
                    command += ", " + this.UpdatedBy;
                command += ")";
                int newID = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                command += " values (" + newID + ", @" + this.DataFlowName;
                command += ", @" + this.TransID + ", @" + this.FileName + ", @" + this.FileType;
                command += ", @" + this.Status + ", @" + this.SubmitDate + ", @" + this.Content;
                command += ", @" + this.Size;
                command += ", @" + this.CreatedDate + ", @" + this.CreatedBy;
                command += ", @" + this.UpdatedDate;
                if (user != null)
                    command += ", @" + this.UpdatedBy;
                command += ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DataFlowName, dataFlowName));
                parameters.Add(new Parameter(this.TransID, transID));
                parameters.Add(new Parameter(this.FileName, name));
                parameters.Add(new Parameter(this.FileType, type));
                parameters.Add(new Parameter(this.Status, status));
                parameters.Add(new Parameter(this.SubmitDate, date));

                if (content.Length > 0)
                {
                    parameters.Add(new Parameter(this.Content, content));
                    parameters.Add(new Parameter(this.Size, content.Length));
                }
                else
                {
                    parameters.Add(new Parameter(this.Content, DBNull.Value));
                    parameters.Add(new Parameter(this.Size, 0));
                }
                parameters.Add(new Parameter(this.CreatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.CreatedBy, user != null ? user : ""));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                if (user != null)
                    parameters.Add(new Parameter(this.UpdatedBy, user));

                int count = db.ExecuteNonQuery(command, parameters);
                if (count > 0)
                {
                    fileID = newID;
                    db.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return fileID;
        }
        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="name">Name of the Document</param>
        /// <param name="type">Type of the Document</param>
        /// <param name="token">Token from EPA</param>
        /// <param name="submitURL">The Web services URL</param>
        /// <param name="content">Byte array representing the Content of the Document</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="dataFlowName">Operation Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        public int UploadDocument(string name, string type, string token, string submitURL, byte[] content, string transID, string dataFlowName, string status, DateTime date, string user)
        {
            int fileID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                string command = "insert into " + this.TblFileCabin + " (" + this.FileID;
                command += ", " + this.DataFlowName;
                command += ", " + this.TransID + ", " + this.FileName + ", " + this.FileType;
                command += ", " + this.Status + ", " + this.SubmitDate + ", " + this.Content;
                command += ", " + this.SubmitToken + ", " + this.SubmitURL;
                command += ", " + this.Size + ", " + this.CreatedDate + ", " + this.CreatedBy;
                command += ", " + this.UpdatedDate;
                if (user != null)
                    command += ", " + this.UpdatedBy;
                command += ")";
                int newID = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                command += " values (" + newID + ", @" + this.DataFlowName;
                command += ", @" + this.TransID + ", @" + this.FileName + ", @" + this.FileType;
                command += ", @" + this.Status + ", @" + this.SubmitDate + ", @" + this.Content;
                command += ", @" + this.SubmitToken + ", @" + this.SubmitURL;
                if (content != null)
                    command += ", " + content.Length;
                else
                    command += ", 0";
                command += ", @" + this.CreatedDate + ", @" + this.CreatedBy;
                command += ", @" + this.UpdatedDate;
                if (user != null)
                    command += ", @" + this.UpdatedBy;
                command += ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DataFlowName, dataFlowName));
                parameters.Add(new Parameter(this.TransID, transID));
                parameters.Add(new Parameter(this.FileName, name));
                parameters.Add(new Parameter(this.FileType, type));
                parameters.Add(new Parameter(this.Status, status));
                parameters.Add(new Parameter(this.SubmitURL, submitURL));
                parameters.Add(new Parameter(this.SubmitToken, token));
                parameters.Add(new Parameter(this.SubmitDate, date));

                if (content.Length > 0)
                {
                    parameters.Add(new Parameter(this.Content, content));
                    parameters.Add(new Parameter(this.Size, content.Length));
                }
                else
                {
                    parameters.Add(new Parameter(this.Content, DBNull.Value));
                    parameters.Add(new Parameter(this.Size, 0));
                }
                parameters.Add(new Parameter(this.CreatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.CreatedBy, user != null ? user : ""));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                if (user != null)
                    parameters.Add(new Parameter(this.UpdatedBy, user));

                int count = db.ExecuteNonQuery(command, parameters);
                if (count > 0)
                {
                    fileID = newID;
                    db.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return fileID;
        }
        /// <summary>
        /// Uploads Document to the Node Database
        /// </summary>
        /// <param name="doc">Node.Core.Document.NodeDocument</param>
        /// <param name="transID">Transaction ID of the Document, cannot be null</param>
        /// <param name="dataFlowName">DataFlow Name Associated with the Document</param>
        /// <param name="status">Status of the Document</param>
        /// <param name="date">DateTime of the Submission</param>
        /// <param name="user">User of the Issued Upload</param>
        /// <returns>File ID just uploaded</returns>
        public int UploadDocuments(NodeDocument doc, string transID, string dataFlowName, string status, DateTime date, string user)
        {
            int fileID = -1;
            DBAdapter db = null;
            try
            {
                db = this.GetNodeDB();
                db.BeginTransaction();
                string command = "insert into " + this.TblFileCabin + " (" + this.FileID;
                command += ", " + this.DataFlowName;
                command += ", " + this.TransID + ", " + this.FileName + ", " + this.FileType;
                command += ", " + this.Status + ", " + this.SubmitDate + ", " + this.Content;
                command += ", " + this.Size + ", " + this.DocID + ", " + this.CreatedDate + ", " + this.CreatedBy;
                command += ", " + this.UpdatedDate;
                if (user != null)
                    command += ", " + this.UpdatedBy;
                command += ")";
                int newID = this.GetSequenceNumber(this.TblFileCabin, this.FileID);
                command += " values (" + newID + ", @" + this.DataFlowName;
                command += ", @" + this.TransID + ", @" + this.FileName + ", @" + this.FileType;
                command += ", @" + this.Status + ", @" + this.SubmitDate + ", @" + this.Content;
                command += ", @" + this.Size;
                command += ", @" + this.DocID;
                command += ", @" + this.CreatedDate + ", @" + this.CreatedBy;
                command += ", @" + this.UpdatedDate;
                if (user != null)
                    command += ", @" + this.UpdatedBy;
                command += ")";
                ArrayList parameters = new ArrayList();
                parameters.Add(new Parameter(this.DataFlowName, dataFlowName));
                parameters.Add(new Parameter(this.TransID, transID));
                parameters.Add(new Parameter(this.FileName, doc.name));
                parameters.Add(new Parameter(this.FileType, doc.type));
                parameters.Add(new Parameter(this.DocID, (doc.href == null) ? DBNull.Value : (object)doc.href));
                parameters.Add(new Parameter(this.Status, status));
                parameters.Add(new Parameter(this.SubmitDate, date));

                if (doc.content.Length > 0)
                {
                    parameters.Add(new Parameter(this.Content, doc.content));
                    parameters.Add(new Parameter(this.Size, doc.content.Length));
                }
                else
                {
                    parameters.Add(new Parameter(this.Content, DBNull.Value));
                    parameters.Add(new Parameter(this.Size, 0));
                }
                parameters.Add(new Parameter(this.CreatedDate, DateTime.Now));
                parameters.Add(new Parameter(this.CreatedBy, user != null ? user : ""));
                parameters.Add(new Parameter(this.UpdatedDate, DateTime.Now));
                if (user != null)
                    parameters.Add(new Parameter(this.UpdatedBy, user));

                int count = db.ExecuteNonQuery(command, parameters);
                if (count > 0)
                {
                    fileID = newID;
                    db.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                if (db != null)
                    db.RollbackTransaction();
                throw e;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return fileID;
        }

    }
}
