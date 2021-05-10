package Node.Biz.Administration;

import java.text.SimpleDateFormat;
import java.sql.Date;
import java.util.ArrayList;

import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.Phrase;
/**
 * <p>This class create OperationLog class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationLog{
  private int ID = -1;
  private String TransID = null;
  private String OperationName = null;
  private String OperationType = null;
  private String WebServiceName = null;
  private String Domain = null;
  private String UserName = null;
  private String RequestorIP = null;
  private String SupplTransID = null;
  private String Token = null;
  private String NodeAddress = null;
  private String ReturnURL = null;
  private String ServiceType = null;
  private String StartDate = null;
  private String EndDate = null;
  private String HostName = null;
  private String CreatedDate = null;
  private String CreatedBy = null;
  private String UpdatedDate = null;
  private String UpdatedBy = null;
  private ArrayList Status = null;
  private ArrayList Parameters = null;

  /**
   * Constructor.
   * @param logID .
   * @return 
   */
  public OperationLog(int logID) {
    this.ID = logID;
  }

  /**
   * Constructor.
   * @param transID .
   * @return 
   */
  public OperationLog(String transID) {
    this.TransID = transID;
  }

  /**
   * Constructor.
   * @param logID .
   * @param loggerName .
   * @return 
   */
  public OperationLog(int logID, String loggerName) throws Exception {
    OperationLog log = null;
    if (logID >= 0) {
      INodeOperationLog logDB = DBManager.GetNodeOperationLog(loggerName);
      log = logDB.GetOperationLog(logID);
    }
    if (log != null)
      this.Init(log);
    else
      this.ID = logID;
  }

  /**
   * Constructor.
   * @param transID .
   * @param loggerName .
   * @return 
   */
  public OperationLog(String transID, String loggerName) throws Exception {
    OperationLog log = null;
    if (transID != null) {
      INodeOperationLog logDB = DBManager.GetNodeOperationLog(loggerName);
      log = logDB.GetOperationLog(transID);
    }
    if (log != null)
      this.Init(log);
    else
      this.TransID = transID;
  }

  /**
   * Initial Class.
   * @param doc.
   * @return 
   */
  private void Init (OperationLog log)
  {
    if (log != null) {
      this.ID = log.GetOpLogID();
      this.TransID = log.GetTransID();
      this.OperationName = log.GetOperationName();
      this.OperationType = log.GetOperationType();
      this.Domain = log.GetDomain();
      this.UserName = log.GetUserName();
      this.StartDate = log.GetStartDate();
      this.EndDate = log.GetEndDate();
      this.HostName = log.GetHostName();
      this.CreatedDate = log.GetCreatedDate();
      this.CreatedBy = log.GetCreatedBy();
      this.UpdatedDate = log.GetUpdatedDate();
      this.UpdatedBy = log.GetUpdatedBy();
      this.Status = log.GetStatus();
      this.Parameters = log.GetParameters();
      if (this.OperationType != null) {
        if (this.OperationType.equals(Phrase.WEB_SERVICE_OPERATION)) {
          this.WebServiceName = log.GetWebServiceName();
          this.RequestorIP = log.GetRequestorIP();
          if (!this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING))
            this.Token = log.GetToken();
          if (this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_DOWNLOAD) ||
              this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_GETSTATUS) ||
              this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT))
            this.SupplTransID = log.GetSupplTransID();
          if (this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_NOTIFY))
            this.NodeAddress = log.GetNodeAddress();
          if (this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT))
            this.ReturnURL = log.GetReturnURL();
          if (this.WebServiceName.equalsIgnoreCase(Phrase.WEB_METHOD_GETSERVICES))
            this.ServiceType = log.GetServiceType();
        }
      }
    }
  }

  public void SetOpLogID (int input)
  {
    this.ID = input;
  }
  public int GetOpLogID ()
  {
    return this.ID;
  }

  public void SetTransID (String input)
  {
    this.TransID = input;
  }
  public String GetTransID ()
  {
    return this.TransID;
  }

  public void SetOperationName (String input)
  {
    this.OperationName = input;
  }
  public String GetOperationName ()
  {
    return this.OperationName;
  }

  public void SetOperationType (String input)
  {
    this.OperationType = input;
  }
  public String GetOperationType ()
  {
    return this.OperationType;
  }

  public void SetWebServiceName (String input)
  {
    this.WebServiceName = input;
  }
  public String GetWebServiceName ()
  {
    return this.WebServiceName;
  }

  public void SetDomain (String input)
  {
    this.Domain = input;
  }
  public String GetDomain ()
  {
    return this.Domain;
  }

  public void SetUserName (String input)
  {
    this.UserName = input;
  }
  public String GetUserName ()
  {
    return this.UserName;
  }

  public void SetRequestorIP (String input)
  {
    this.RequestorIP = input;
  }
  public String GetRequestorIP ()
  {
    return this.RequestorIP;
  }

  public void SetSupplTransID (String input)
  {
    this.SupplTransID = input;
  }
  public String GetSupplTransID ()
  {
    return this.SupplTransID;
  }

  public void SetToken (String input)
  {
    this.Token = input;
  }
  public String GetToken ()
  {
    return this.Token;
  }

  public void SetNodeAddress (String input)
  {
    this.NodeAddress = input;
  }
  public String GetNodeAddress ()
  {
    return this.NodeAddress;
  }

  public void SetReturnURL (String input)
  {
    this.ReturnURL = input;
  }
  public String GetReturnURL ()
  {
    return this.ReturnURL;
  }

  public void SetServiceType (String input)
  {
    this.ServiceType = input;
  }
  public String GetServiceType ()
  {
    return this.ServiceType;
  }

  public void SetStartDate (String input) throws Exception
  {
    if (input != null && !input.equals("")) {
      SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = dateFormat.parse(input);
      this.StartDate = dateFormat.format(date);
    }
    else
      this.StartDate = null;
  }
  public String GetStartDate ()
  {
    return this.StartDate;
  }

  public void SetEndDate (String input) throws Exception
  {
    if (input != null && !input.equals("")) {
      SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
      java.util.Date date = dateFormat.parse(input);
      this.EndDate = dateFormat.format(date);
    }
    else
      this.EndDate = null;
  }
  public String GetEndDate ()
  {
    return this.EndDate;
  }

  public void SetHostName (String input)
  {
    this.HostName = input;
  }
  public String GetHostName ()
  {
    return this.HostName;
  }

  public void SetCreatedDate (String input)
  {
    this.CreatedDate = input;
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

  public void SetUpdatedDate (String input)
  {
    this.UpdatedDate = input;
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

  public void SetStatus (ArrayList list)
  {
    this.Status = list;
  }
  public ArrayList GetStatus ()
  {
    return this.Status;
  }

  public void SetParameters (ArrayList list)
  {
    this.Parameters = list;
  }
  public ArrayList GetParameters ()
  {
    return this.Parameters;
  }

  /**
   * Search Operation Log.
   * @param loggerName.
   * @param admin.
   * @param opName.
   * @param opType.
   * @param wsName.
   * @param status.
   * @param domains.
   * @param userName.
   * @param token.
   * @param transID.
   * @param start.
   * @param end.
   * @param version_no.
   * @return OperationLog array
   */
  public static OperationLog[] SearchOperationLog (String loggerName, User admin, String opName, String opType, String wsName,
      String status, String[] domains, String userName, String token, String transID, Date start, Date end, String version_no) throws Exception
  {
    if (domains == null && !admin.IsNodeAdmin())
      domains = admin.GetAssignedDomains();
    INodeOperationLog logDB = DBManager.GetNodeOperationLog(loggerName);
 //   return logDB.SearchOperationLog(opName,opType,wsName,status,domains,userName,token,transID,start,end);

    return logDB.SearchOperationLog(opName!=null&&!opName.equals("")?opName:null,
                                    opType!=null&&!opType.equals("")?opType:null,
                                    wsName!=null&&!wsName.equals("")?wsName:null,
                                    status!=null&&!status.equals("")?status:null,
                                    domains,
                                    userName!=null&&!userName.equals("")?userName:null,
                                    token!=null&&!token.equals("")?token:null,
                                    transID!=null&&!transID.equals("")?transID:null,start,end,
                                    version_no);
  }

  /**
   * Get Unique Status List.
   * @param loggerName.
   * @param admin.
   * @return Unique Status array
   */
  public static String[] GetUniqueStatusList (String loggerName, User admin) throws Exception
  {
    String[] retArray = null;
    if (admin != null) {
      String[] domains = null;
      if (!admin.IsNodeAdmin())
        domains = admin.GetAssignedDomains();
      INodeOperationLog logDB = DBManager.GetNodeOperationLog(loggerName);
      retArray = logDB.GetUniqueStatusList(domains);
    }
    return retArray;
  }

  /**
   * Get Unique Operation Name List.
   * @param loggerName.
   * @param admin.
   * @return Unique Status array
   */
  public static String[] GetUniqueOperationNameList (String loggerName, User admin) throws Exception
  {
    String[] retArray = null;
    if (admin != null) {
      String[] domains = null;
      if (!admin.IsNodeAdmin())
        domains = admin.GetAssignedDomains();
      INodeOperationLog logDB = DBManager.GetNodeOperationLog(loggerName);
      retArray = logDB.GetUniqueOperationNameList(domains);
    }
    return retArray;
  }
}
