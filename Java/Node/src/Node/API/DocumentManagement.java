package Node.API;

import java.sql.Date;
import java.sql.Timestamp;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeFileCabin;
import Node.WebServices.Document.ClsNodeDocument;

/**
 * <p>This API is used to Download, Upload, and Remove Documents from the Node Database.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */
public class DocumentManagement {
  private String LoggerName = null;

  /**
   * Constructs a manager for uploading, downloading, and removing documents from the Node database.
   * @param loggerName (Required) Name of the log4j Logger Name (in case Logging should take place).  See Node.Phrase for details.
   */
  public DocumentManagement(String loggerName) {
    this.LoggerName = loggerName;
  }

  /**
   * Retrieve a Document or set of Documents stored in the Node Database that have a transaction id with the given input.
   * <p>If the transaction id is null or not found in the Node Database, null is returned.</p>
   * @param transID transaction id of file(s) to download.
   * @throws Exception if some database error occurs.
   * @return array of documents downloaded from Node database.
   */
  public ClsNodeDocument[] Download (String transID) throws Exception {
    if (transID != null) {
      INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
      return cabinDB.GetDocuments(transID, true);
    }
    else
      return null;
  }

  /**
   * Retrieve a Document or set of Documents stored in the Node Database.
   * <p>If either the transID parameter or the dataFlow parameter is null, the retrieval of the Document
   * is still attempted as long as at least one of the parameters is not null.  If only one of the
   * parameters is present, it attempts to retrive the documents with that transaction id or that
   * data flow name.  If both are present, it attempts to retrieve documents with that match both
   * input parameters.</p>
   * <p>In case that both input parameters are null or the search fails to find any documents, null is returned.</p>
   * @param transID transaction id of file(s) to download.
   * @param dataFlow data flow of file(s) to download.
   * @throws Exception if some database error occurs.
   * @return array of documents downloaded from Node database.
   */
  public ClsNodeDocument[] Download (String transID, String dataFlow) throws Exception {
    ClsNodeDocument[] retDocs = null;
    INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
    if (transID != null) {
      if (dataFlow != null)
        retDocs = cabinDB.GetDocuments(transID, dataFlow);
      else
        retDocs = cabinDB.GetDocuments(transID,true);
    }
    else if (dataFlow != null)
      retDocs = cabinDB.GetDocuments(dataFlow, false);
    return retDocs;
  }

  /**
   * Retrieve a Document or set of Documents stored in the Node Database.
   * <p>If all three input parameters are null or the names array has length 0 and the other two
   * input parameters are null, then null is returned.  Otherwise, the method attempts to download
   * documents by all of the input parameters that are supplied.</p>
   * <p>If an input parameter is null (or the names array has length 0), then that input parameter
   * is not used to query the Node< database.</p>
   * <p>If the retrieval fails to find any documents, null is returned.</p>
   * @param transID transaction id of file(s) to download.
   * @param dataFlow data flow of file(s) to download.
   * @param names array of file names to download
   * @throws Exception if some database error occurs.
   * @return array of documents downloaded from Node database.
   */
  public ClsNodeDocument[] Download (String transID, String dataFlow, String[] names) throws Exception {
    INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
    ClsNodeDocument[] temp = null;
    if (names != null && names.length > 0) {
      temp = new ClsNodeDocument[names.length];
      for (int i = 0; i < names.length; i++) {
        temp[i] = new ClsNodeDocument();
        temp[i].setName(names[i]);
      }
    }
    return cabinDB.GetDocuments(transID, dataFlow, temp);
  }

  /**
   * Upload a Document to the Node Database.
   * <p>Parameters that are not required and not supplied are entered as null into the Node database.</p>
   * <p>If a transaction id is not supplied, one is generated by the Node Application.
   * If a Submit Date OR a Timestamp is not supplied, the Node Application uses the present Date and Timestamp
   * to populate the Node Database.</p>
   * <p>If names, content, or types are null, or if names.length != content.length or
   * names.length != types.length, then false is returned.  Otherwise, true is returned if the documents are
   * successfully uploaded to the Node Database.</p>
   * <p>The index of a name, content, and type should all reference each other.  For example, names[0] should be
   * the name of the document for content[0] with type[0]</p>
   * @param names (Required)
   * @param content (Required)
   * @param types (Required)
   * @param transID (Optional, see Note above)
   * @param status (Optional)
   * @param dataFlow (Optional)
   * @param submitURL (Optional)
   * @param token (Optional)
   * @param submitted (Optional, see Note above)
   * @param submittedTS (Optional, see Note above)
   * @param user (Optional, user used to Update Database)
   * @throws Exception if some database error occurs
   * @return boolean true if the upload succeeds, false otherwise
   */
  public boolean Upload (String[] names, byte[][] content, String[] types, String transID, String status, String dataFlow, String submitURL, String token, Date submitted, Timestamp submittedTS, String user) throws Exception
  {
    boolean retBool = false;
    if (names != null && names.length > 0 && content != null && content.length == names.length && types != null && types.length == names.length) {
      ClsNodeDocument[] docs = new ClsNodeDocument [names.length];
      for (int i = 0; i < names.length; i++) {
        docs[i] = new ClsNodeDocument();
        docs[i].setName(names[i]);
        docs[i].setType(types[i]);
        docs[i].setContent(content[i]);
      }
      if (transID == null || transID.equals("")) {
        NodeUtils utils = new NodeUtils();
        transID = utils.GenerateTransID(Phrase.UUID);
      }
      INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
      retBool = cabinDB.UploadDocuments(docs,transID,status,dataFlow,submitURL,token,submitted,submittedTS, user);
    }
    return retBool;
  }

  /**
   * Upload a Document to the Node Database.
   * <p>Parameters that are not required and not supplied are entered as null into the Node database.</p>
   * <p>If a transaction id is not supplied, one is generated by the Node Application.
   * If a Submit Date OR a Timestamp is not supplied, the Node Application uses the present Date and Timestamp
   * to populate the Node Database.</p>
   * <p>If docs is null or docs.length <= 0, the false is returned.  Otherwise, true is returned if the Upload
   * succeeds.</p>
   * @param docs (Required)
   * @param transID (Optional, see Note above)
   * @param status (Optional)
   * @param dataFlow (Optional)
   * @param submitURL (Optional)
   * @param token (Optional)
   * @param submitted (Optional, see Note above)
   * @param submittedTS (Optional, see Note above)
   * @param user (Optional, user used to Update Database)
   * @throws Exception if some database error occurs.
   * @return boolean true if the upload succeeds, false otherwise.
   */
  public boolean Upload (ClsNodeDocument[] docs, String transID, String status, String dataFlow, String submitURL, String token, Date submitted, Timestamp submittedTS, String user) throws Exception
  {
    boolean retBool = false;
    if (docs != null && docs.length > 0) {
      INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
      retBool = cabinDB.UploadDocuments(docs,transID,status,dataFlow,submitURL,token,submitted,submittedTS, user);
    }
    return retBool;
  }

  /**
   * Remove a Document from the Node Database
   * <p>If transID is null and names is either null or names.length == 0, then true is returned,
   * but nothing is returned from the Node database.  If one of the input parameters is supplied,
   * then the method attempts to remove any documents that match the transaction id (if supplied)
   * and names in the String array (if supplied).</p>
   * <p>This method will return false if the removal fails.</p>
   * @param transID transaction id of file(s) to remove.
   * @param names list of file names to remove.
   * @throws Exception if some database error occurs.
   * @return boolean true if removal succeeds or if there were no documents to be found, false otherwise.
   */
  public boolean Remove (String transID, String[] names) throws Exception
  {
    boolean retBool = false;
    if (transID != null || (names != null && names.length > 0)) {
      INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(this.LoggerName);
      retBool = cabinDB.RemoveDocuments(transID,names);
    }
    else
      retBool = true;
    return retBool;
  }
}
