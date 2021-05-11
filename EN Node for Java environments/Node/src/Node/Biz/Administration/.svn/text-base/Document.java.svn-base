package Node.Biz.Administration;

import java.sql.Date;
import java.text.SimpleDateFormat;

import Node.DB.DBManager;
import Node.DB.Interfaces.INodeFileCabin;
/**
 * <p>This class create Document class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Document {
  private int FileID = -1;
  private String documentID = null;
  private String TransID = null;
  private String Name = null;
  private String Type = null;
  private String Status = null;
  private String DataFlow = null;
  private String Domain = null;
  private String Operation = null;
  private String WebService = null;
  private String SubmitURL = null;
  private String Token = null;
  private String SubmitDate = null;
  private byte[] Content = null;
  private int Size = -1;
  private String CreatedDate = null;
  private String CreatedBy = null;
  private String UpdatedDate = null;
  private String UpdatedBy = null;

  /**
   * Constructor.
   * @param fileID .
   * @return 
   */
  public Document(int fileID)
  {
    this.FileID = fileID;
  }

  /**
   * Constructor.
   * @param fileID.
   * @param loggerName.
   * @return 
   */
  public Document (int fileID, String loggerName) throws Exception
  {
    Document doc = null;
    if (fileID >= 0) {
      INodeFileCabin fileDB = DBManager.GetNodeFileCabin(loggerName);
      doc = fileDB.GetDocument(fileID);
    }
    if (doc != null)
      this.Init(doc);
    else
      this.FileID = fileID;
  }

  /**
   * Initial Class.
   * @param doc.
   * @return 
   */
  private void Init (Document doc)
  {
    if (doc != null) {
      this.Content = doc.GetContent();
      this.CreatedBy = doc.GetCreatedBy();
      this.CreatedDate = doc.GetCreatedDate();
      this.DataFlow = doc.GetDataFlow();
      this.Domain = doc.GetDomain();
      this.FileID = doc.GetFileID();
      this.documentID = doc.getDocumentID();
      this.Name = doc.GetName();
      this.Operation = doc.GetOperation();
      this.WebService = doc.GetWebService();
      this.Size = doc.GetSize();
      this.Status = doc.GetStatus();
      this.SubmitDate = doc.GetSubmitDate();
      this.SubmitURL = doc.GetSubmitURL();
      this.Token = doc.GetToken();
      this.TransID = doc.GetTransID();
      this.Type = doc.GetType();
      this.UpdatedBy = doc.GetUpdatedBy();
      this.UpdatedDate = doc.GetUpdatedDate();
    }
  }

  public void SetFileID (int input)
  {
    this.FileID = input;
  }
  public int GetFileID ()
  {
    return this.FileID;
  }

  /**
 * @return the documentID
 */
public String getDocumentID() {
	return documentID;
}

/**
 * @param documentID the documentID to set
 */
public void setDocumentID(String documentID) {
	this.documentID = documentID;
}

public void SetTransID (String input)
  {
    this.TransID = input;
  }
  public String GetTransID ()
  {
    return this.TransID;
  }

  public void SetName (String input)
  {
    this.Name = input;
  }
  public String GetName ()
  {
    return this.Name;
  }

  public void SetType (String input)
  {
    this.Type = input;
  }
  public String GetType ()
  {
    return this.Type;
  }

  public void SetStatus (String input)
  {
    this.Status = input;
  }
  public String GetStatus ()
  {
    return this.Status;
  }

  public void SetDataFlow (String input)
  {
    this.DataFlow = input;
  }
  public String GetDataFlow ()
  {
    return this.DataFlow;
  }

  public void SetDomain (String input)
  {
    this.Domain = input;
  }
  public String GetDomain ()
  {
    return this.Domain;
  }

  public void SetOperation (String input)
  {
    this.Operation = input;
  }
  public String GetOperation ()
  {
    return this.Operation;
  }

  public void SetWebService (String input)
  {
    this.WebService = input;
  }
  public String GetWebService ()
  {
    return this.WebService;
  }

  public void SetSubmitURL (String input)
  {
    this.SubmitURL = input;
  }
  public String GetSubmitURL ()
  {
    return this.SubmitURL;
  }

  public void SetToken (String input)
  {
    this.Token = input;
  }
  public String GetToken ()
  {
    return this.Token;
  }

  public void SetSubmitDate (String input) throws Exception
  {
    if (input != null && !input.equals("")) {
      SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = sdf.parse(input);
      this.SubmitDate = sdf.format(date);
    }
    else
      this.SubmitDate = null;
  }
  public String GetSubmitDate ()
  {
    return this.SubmitDate;
  }

  public void SetContent (byte[] input)
  {
    this.Content = input;
  }
  public byte[] GetContent ()
  {
    return this.Content;
  }

  public void SetSize (int input)
  {
    this.Size = input;
  }
  public int GetSize ()
  {
    return this.Size;
  }

  public void SetCreatedDate (String input) throws Exception
  {
    if (input != null && !input.equals("")) {
      SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = sdf.parse(input);
      this.CreatedDate = sdf.format(date);
    }
    else
      this.CreatedDate = null;
  }
  public String GetCreatedDate ()
  {
    return this.CreatedDate;
  }

  public void SetCreatedBy (String input)
  {
    this.CreatedBy = input;
  }
  public String GetCreatedBy ()
  {
    return this.CreatedBy;
  }

  public void SetUpdatedDate (String input) throws Exception
  {
    if (input != null && !input.equals("")) {
      SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = sdf.parse(input);
      this.UpdatedDate = sdf.format(date);
    }
    else
      this.UpdatedDate = null;
  }
  public String GetUpdatedDate ()
  {
    return this.UpdatedDate;
  }

  public void SetUpdatedBy (String input)
  {
    this.UpdatedBy = input;
  }
  public String GetUpdatedBy ()
  {
    return this.UpdatedBy;
  }

  /**
   * Save Document.
   * @param loggerName.
   * @return result
   */
  public String Save (String loggerName) throws Exception
  {
    INodeFileCabin fileDB = DBManager.GetNodeFileCabin(loggerName);
    Date submitDate = null;
    if (this.SubmitDate != null && !this.SubmitDate.trim().equals(""))
    {
      SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = sdf.parse(this.SubmitDate);
      submitDate = new Date(date.getTime());
    }
    String result = fileDB.SaveDocument(this.FileID, this.documentID,  this.TransID, this.Name, this.Type, this.Status, this.DataFlow, this.SubmitURL,
                                        this.Token, submitDate, this.Content, this.UpdatedBy);
    if (result == null || result.trim().equals(""))
    {
      Document doc = fileDB.GetDocument(this.FileID);
      if (doc != null)
        this.Init(doc);
    }
    return result;
  }

  /**
   * Save Document.
   * @param loggerName.
   * @param name.
   * @param transID.
   * @param domain.
   * @param operation.
   * @param start.
   * @param end.
   * @param admin.
   * @param version_no.
   * @return Document array
   */
  public static Document[] SearchDocuments (String loggerName, String name, String transID, String domain, String operation,
                                            Date start, Date end, User admin, String version_no) throws Exception
  {
    String[] adminDomains = null;
    if (!admin.IsNodeAdmin())
      adminDomains = admin.GetAssignedDomains();
    INodeFileCabin fileDB = DBManager.GetNodeFileCabin(loggerName);
    return fileDB.SearchDocuments(name,transID,domain,operation,start,end,adminDomains, version_no);
  }

  /**
   * Save Document.
   * @param loggerName.
   * @param admin.
   * @return Document array
   */
  public static String[] GetUniqueOperationNames (String loggerName, User admin) throws Exception
  {
    String[] retArray = null;
    if (admin != null) {
      String[] domains = null;
      if (!admin.IsNodeAdmin())
        domains = admin.GetAssignedDomains();
      INodeFileCabin logDB = DBManager.GetNodeFileCabin(loggerName);
      retArray = logDB.GetOperationNames(domains);
    }
    return retArray;
  }

  /**
   * Remove Document.
   * @param loggerName.
   * @param fileIDs.
   * @return if move file successfully
   */
  public static boolean RemoveDocuments (String loggerName, int[] fileIDs) throws Exception
  {
    INodeFileCabin fileDB = DBManager.GetNodeFileCabin(loggerName);
    return fileDB.RemoveDocuments(fileIDs);
  }
}
