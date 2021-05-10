package Node.DB.Interfaces;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import java.sql.Date;
import java.sql.Timestamp;

import Node.Biz.Administration.Document;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create INodeFileCabin interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeFileCabin {
  /**
   * Get Documents According to TransID or DataFlow
   * @param tIDorDataFlow String TRANS_ID or DATAFLOW_NAME
   * @param isTransID true if TransID, false if DataFlow
   * @return ClsNodeDocument[] FILE_CONTENT
   */
  public ClsNodeDocument[] GetDocuments (String tIDorDataFlow, boolean isTransID);

  /**
   * Get Documents According to Names in ClsNodeDocument[]
   * @param searchDocs ClsNodeDocument[] Search Criteria
   * @return ClsNodeDocument[] FILE_CONTENT
   */
  public ClsNodeDocument[] GetDocuments (ClsNodeDocument[] searchDocs);

  /**
   * Get Documents According to TransID and DataFlow
   * @param transID String TRANS_ID
   * @param dataFlow String DATAFLOW_NAME
   * @return ClsNodeDocument[] FILE_CONTENT
   */
  public ClsNodeDocument[] GetDocuments (String transID, String dataFlow);

  /**
   * Get Documents According to TransID and DataFlow
   * @param transID String TRANS_ID
   * @param dataFlow String DATAFLOW_NAME
   * @param searchDocs ClsNodeDocument[] FILE_NAME
   * @return ClsNodeDocument[] FILE_CONTENT
   */
  public ClsNodeDocument[] GetDocuments (String transID, String dataFlow, ClsNodeDocument[] searchDocs);

  /**
   * Get Documents According to TransID and DataFlow
   * @param transID String TRANS_ID
   * @param dataFlow String DATAFLOW_NAME
   * @param operation String OPERATION_NAME
   * @param searchDocs ClsNodeDocument[] FILE_NAME
   * @return ClsNodeDocument[] FILE_CONTENT
   */
  public ClsNodeDocument[] GetDocuments (String transID, String dataFlow,String[] operationArr, ClsNodeDocument[] searchDocs);
  /**
   * Upload Documents to the Database
   * @param docs Documents to be upload (required)
   * @param transID String TRANS_ID (optional, although strongly recommended)
   * @param status String STATUS (optional)
   * @param dataFlow String DATAFLOW_NAME (optional)
   * @param submitURL String SUBMIT_URL (optional)
   * @param token String SUBMIT_TOKEN (optional)
   * @param submitted Date SUBMIT_DATE (optional)
   * @param submittedTS Timestamp SUBMIT_DATE (optional)
   * @return boolean true if successful, false otherwise
   */
  public boolean UploadDocuments (ClsNodeDocument[] docs, String transID, String status, String dataFlow, String submitURL, String token, Date submitted, Timestamp submittedTS, String user);

  /**
   * Upload Documents to the Database
   * @param docs Documents to be upload (required)
   * @param transID String TRANS_ID (optional, although strongly recommended)
   * @param status String STATUS (optional)
   * @param dataFlow String DATAFLOW_NAME (optional)
   * @param submitURL String SUBMIT_URL (optional)
   * @param token String SUBMIT_TOKEN (optional)
   * @param submitted Date SUBMIT_DATE (optional)
   * @param submittedTS Timestamp SUBMIT_DATE (optional)
   * @return boolean true if successful, false otherwise
   */
  public boolean UploadHugeDocuments (ClsNodeDocument[] docs, String transID, String status, String dataFlow, String submitURL, String token, Date submitted, Timestamp submittedTS, String user);
	// WI 22695
  /**
   * Upload Documents to the Database without delete the temp file
   * @param docs Documents to be upload (required)
   * @param transID String TRANS_ID (optional, although strongly recommended)
   * @param status String STATUS (optional)
   * @param dataFlow String DATAFLOW_NAME (optional)
   * @param submitURL String SUBMIT_URL (optional)
   * @param token String SUBMIT_TOKEN (optional)
   * @param submitted Date SUBMIT_DATE (optional)
   * @param submittedTS Timestamp SUBMIT_DATE (optional)
   * @return boolean true if successful, false otherwise
   */
  public boolean UploadHugeDocumentsWithoutDelete (ClsNodeDocument[] docs, String transID, String status, String dataFlow, String submitURL, String token, Date submitted, Timestamp submittedTS, String user);
  /**
   * Query Documents
   * @param transID String TRANS_ID
   * @param dataFlow String DATAFLOW_NAME
   * @param names String[] Search Names
   * @return XmlDocument Return Result, null if no documents found
   */
  public XmlDocument QueryDocs (String transID, String dataFlow, String[] names);

  /**
   * Search Documents
   * @param docName String
   * @param transID String
   * @param domainName String
   * @param opName String
   * @param start Date
   * @param end Date
   * @return Document[]
   */
  public Document[] SearchDocuments (String docName, String transID, String domainName, String opName, Date start, Date end, String[] adminDomains, String version_no);

  /**
   * Get Unique Operations
   * @param domains String[] Domains Admin has rights to, null for all operations
   * @return String[]
   */
  public String[] GetOperationNames (String[] domains);

  /**
   * Get Document
   * @param fileID int
   * @return Document
   */
  public Document GetDocument (int fileID);

  /**
   * Get Document
   * @param fileID int
   * @return Document
   * The content of Document object is the temporary file path, not real data
   */
  public Document GetHugeDocument (int fileID);

  /**
   * Remove Documents
   * @param fileIDs int[]
   * @return boolean
   */
  public boolean RemoveDocuments (int[] fileIDs);

  /**
   * Remove Documents
   * @param transID String
   * @param names String[]
   * @return boolean
   */
  public boolean RemoveDocuments (String transID, String[] names);

  /**
   * Get Document Transanction ID
   * @param fileID int
   * @return Document
   */
  public String GetDocumentTransactionID (int fileID);

  /**
   * SaveDocument
   * @param fileID
   * @param transID
   * @param fileName
   * @param fileType
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param submitToken
   * @param submitDate
   * @param content
   * @param user
   * @return String
   */
  public String SaveDocument (int fileID, String transID, String fileName, String fileType, String status, String dataFlow,
          String submitURL, String submitToken, Date submitDate, byte[] content, String user);

  /**
   * SaveDocument
   * @param fileID
   * @param documentID
   * @param transID
   * @param fileName
   * @param fileType
   * @param status
   * @param dataFlow
   * @param submitURL
   * @param submitToken
   * @param submitDate
   * @param content
   * @param user
   * @return String
   */
 public String SaveDocument (int fileID, String documentID,String transID, String fileName, String fileType, String status, String dataFlow,
                               String submitURL, String submitToken, Date submitDate, byte[] content, String user);
}
