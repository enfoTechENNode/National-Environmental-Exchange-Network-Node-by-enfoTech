package Node.Biz.Handler;

import java.rmi.RemoteException;
import java.util.Hashtable;

import org.apache.log4j.DailyRollingFileAppender;
import org.apache.log4j.FileAppender;
import org.apache.log4j.Layout;
import org.apache.log4j.Level;
import org.apache.log4j.LogManager;
import org.apache.log4j.Logger;
import org.apache.log4j.PatternLayout;

import DataFlow.Component.Interface.IActionOperationLog;
import DataFlow.Component.Interface.IActionProcess;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Authorization.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
/**
 * <p>This class create Handler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public abstract class Handler {
  
  protected String TransID = null;
  protected String RequestorIP = null;
  protected String Token = null;
  protected String LoggerName = null;
  protected String HostName = null;
  protected String UserName = null;
  protected String Password = null;
  protected int OpLogID = -1;
  /*
  private String WSName = null;
  private String OPName = null;
  private boolean IsAuthorizable = true;
  private String[] ParamNames = null;
  private String RequestorIP = null;
  private int OpLogID = -1;
  private int WSID = -1;
  private int OPID = -1;
  private int DomainID = -1;
  private int UserID = -1;
  private String Token = null;
  */
  
  /**
   * Constructor.
   * @param requestorIP Requester IP address
   * @return 
   */
  public Handler (String requestorIP)
  {
    this.RequestorIP = requestorIP;
    this.SetLoggerLevel();
  }

  /**
   * Invoke.
   * @param 
   * @return Object Operation return result
   */
  public Object Invoke () throws RemoteException
  {
    Object retObj = null;
    try {
      this.Log("Handler: Invoking Handler",Level.INFO);
      // Check if Node Status is Running or Stopped
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.WebServicesLoggerName);
      if (!configDB.GetNodeStatus().equalsIgnoreCase("Running"))
        throw new RemoteException(Phrase.AccessDenied, new Exception("Node is Currently Stopped"));

      this.Log("Handler: Generating Token",Level.DEBUG);

      // Generate Token ID
      NodeUtils nodeUtils = new NodeUtils();
      this.TransID = nodeUtils.GenerateTransID(Phrase.UUID);

      this.Log("Handler: Initializing Operation",Level.DEBUG);

      // Log Incoming Request
      this.Initialize();

      this.Log("Handler: Authorizing Operation",Level.DEBUG);

      // Authorize && Execute
      this.UserName = this.Authorize();
      if (this.UserName != null) {
        this.Log("Handler: Executing Operation",Level.DEBUG);
        if (this.Token != null && !this.Token.trim().equals(""))
        {
          INodeOperationLog opLogDB = DBManager.GetNodeOperationLog(this.LoggerName);
          this.Password = opLogDB.GetPassword(this.Token);
        }
        retObj = this.Execute();
      }

      this.Log("Handler: Return from Handler",Level.DEBUG);

    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,"Error Handling Operation"+e.toString(),true);
      this.Log("Error Handling Operation "+e.toString(),Level.ERROR);
      throw new RemoteException("Error Handling Operation",e);
    }
    return retObj;
  }

  /**
   * Initialize
   * @param transID String Transaction ID
   * @throws RemoteException If Initialization Fails
   */
  protected abstract void Initialize () throws RemoteException;

  /**
   * Authorize Request
   * @throws RemoteException If Authorization Fails
   * @return String userName
   */
  protected abstract String Authorize () throws RemoteException;

  /**
   * Execute PreProcess(es), Process, and PostProcess(es)
   * @throws RemoteException
   * @return Object
   */
  protected abstract Object Execute () throws RemoteException;
  
  /**
   * Does Custom execution by using data flow wizard.
   * @param dataflowConfig The config of data flow.
   * @return retured result.
   */
  protected abstract Object ExecuteDataflow(String dataflowConfig) throws Exception;

  /**
   * Log.
   * @param message Input message
   * @param level Log level
   * @return 
   */
  protected void Log (String message, Level level)
  {
    LoggingUtils.Log(message,level,this.LoggerName != null ? this.LoggerName : Phrase.WebServicesLoggerName);
  }

  /**
   * Look Up Operation
   * @param opName Operation Name
   * @param wsName WebService Name
   * @return int Operation Id
   */
  protected int Initialize (String opName, String wsName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
    Operation op = opDB.GetActiveOperation(opName,wsName);
    int opID = -1;
    if ((op == null || op.GetOperationID() < 0) && (opName == null || !opName.equalsIgnoreCase("DEFAULT")))
      op = opDB.GetOperation("DEFAULT",wsName);
    if (op != null && op.GetStatus() != null && op.GetStatus().equals(Phrase.RUNNING_STATUS)) {
      String loggerName = op.GetLoggerName();
      Logger logger = null;
      Level level = null;
      if (loggerName != null) {
        this.LoggerName = loggerName;
        level = op.GetLoggingLevel();
        LogManager manager = new LogManager();
        logger = manager.exists(loggerName);
        if (logger == null) {
          logger = manager.getLogger(loggerName);
          Layout layout = new PatternLayout("%-15d [%-5p]: %m\n\r");
          String temp = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIWebServicesLogLocation, false)+"/Operations/"+op.GetLoggerName()+".txt";
          String datePattern = "'.'yyyy-MM-dd";
          DailyRollingFileAppender appender = new DailyRollingFileAppender(layout, temp, datePattern);
          logger.addAppender(appender);
        }
      }
      else {
        this.LoggerName = Phrase.WebServicesLoggerName;
        ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
        logger = Logger.getLogger(Phrase.WebServicesLoggerName);
        level = configDB.GetWebServicesLogLevel();
      }
      logger.setLevel(level);
      INodeDomain domainDB = DBManager.GetNodeDomain(this.LoggerName);
      String domStatus = domainDB.GetDomainStatus(op.GetDomain());
      if (domStatus != null && domStatus.equals(Phrase.RUNNING_STATUS))
        opID = op.GetOperationID();
    }
    return opID;
  }

  // WI 20786
  /**
   * Look Up Operation
   * @param domain Domain Name
   * @param opName Operation Name
   * @param wsName WebService Name
   * @return int Operation Id
   */
  protected int Initialize (String domain, String opName, String wsName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
    String domainName = opDB.GetDomainName(opName, wsName);
    Operation op = opDB.GetActiveOperation(opName,wsName);
    int opID = -1;
    
    if(domainName != null && !domainName.equalsIgnoreCase("") && domainName.equalsIgnoreCase(domain)){
        if ((op == null || op.GetOperationID() < 0) && (opName == null || !opName.equalsIgnoreCase("DEFAULT")))
            op = opDB.GetOperation("DEFAULT",wsName);
          if (op != null && op.GetStatus() != null && op.GetStatus().equals(Phrase.RUNNING_STATUS)) {
            String loggerName = op.GetLoggerName();
            Logger logger = null;
            Level level = null;
            if (loggerName != null) {
              this.LoggerName = loggerName;
              level = op.GetLoggingLevel();
              LogManager manager = new LogManager();
              logger = manager.exists(loggerName);
              if (logger == null) {
                logger = manager.getLogger(loggerName);
                Layout layout = new PatternLayout("%-15d [%-5p]: %m\n\r");
                String temp = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIWebServicesLogLocation, false)+"/Operations/"+op.GetLoggerName()+".txt";
                String datePattern = "'.'yyyy-MM-dd";
                DailyRollingFileAppender appender = new DailyRollingFileAppender(layout, temp, datePattern);
                logger.addAppender(appender);
              }
            }
            else {
              this.LoggerName = Phrase.WebServicesLoggerName;
              ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
              logger = Logger.getLogger(Phrase.WebServicesLoggerName);
              level = configDB.GetWebServicesLogLevel();
            }
            logger.setLevel(level);
            INodeDomain domainDB = DBManager.GetNodeDomain(this.LoggerName);
            String domStatus = domainDB.GetDomainStatus(op.GetDomain());
            if (domStatus != null && domStatus.equals(Phrase.RUNNING_STATUS))
              opID = op.GetOperationID();
          }    	
    }
    return opID;
  }

  /**
   * AuthorizeRequest
   * @param opID Operation Id
   * @return User Name
   */
  protected String AuthorizeRequest (int opID) throws RemoteException
  {
    String user = null;
    try {
      INodeOperationLog opLogDB = DBManager.GetNodeOperationLog(this.LoggerName);
      Operation authenticateOp = opLogDB.GetAuthenticationOperation(this.Token);
      String xml = authenticateOp.GetConfig();
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      XmlNode authorize = doc.SelectSingleNode("/Operation/Process/Authorization");
      if (authorize != null)
      {
        Class authorizeClass = Class.forName(authorize.SelectSingleNode("ClassName").GetInnerText());
        if (authorizeClass != null)
        {
          Class[] authorizeInterfaces = authorizeClass.getInterfaces();
          if (authorizeInterfaces != null && authorizeInterfaces.length > 0)
          {
            for (int i = 0; i < authorizeInterfaces.length; i++)
            {
              if (authorizeInterfaces[i].getName().equalsIgnoreCase("Node.Biz.Interfaces.Authorization.IProcess"))
              {
                IProcess process = (IProcess)authorizeClass.newInstance();
                ISystemConfiguration sysConfigDB = DBManager.GetSystemConfiguration(this.LoggerName);
                INodeOperation opDB = DBManager.GetNodeOperation(this.LoggerName);
                Operation op = opDB.GetOperation(opID);
                ProcParam param = new ProcParam(this.TransID, this.RequestorIP, this.LoggerName, null, null, new Hashtable());
                user = process.Execute(this.Token, sysConfigDB.GetNodeName(), op.GetWebService(), op.GetOpName(), null, param);
              }
            }
            if (user == null)
              throw new RemoteException("Authorization Failed");
          }
          else
            throw new RemoteException("Authorization Class does not implement any interfaces");
        }
        else
          throw new RemoteException("Authorization Class Not Found");
      }
      else
      {
          String userName = opLogDB.GetUserName(this.Token);
          if (userName != null)
            user = userName;
          else
            user = "No Authorization Plugged In";
      }
    } catch (RemoteException e) {
      try {
        INodeOperationLog logDB = DBManager.GetNodeOperationLog(this.LoggerName);
        String userName = logDB.GetUserName(this.Token);
        if (userName == null || userName.trim().equals(""))
          userName = "Authenticated Elsewhere";
        NodeUtils utils = new NodeUtils();
        utils.UpdateOperationLogUserName(this.LoggerName,this.TransID,userName);
      } catch (Exception ex) { }
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return user;
  }

  /**
   * GetActionProcess
   * @param NodeVersion Node version
   * @return IActionProcess Action process object
   */
  protected IActionProcess GetActionProcess(String NodeVersion) throws Exception
  {
	  IActionProcess process = null;
      String dfClass = (String)JNDIAccess.GetJNDIValue("DataFlowPlugIn", false);
      try
      {
	        Class executeClass = Class.forName(dfClass);
	        if (executeClass != null)
	        {
	        	process = (IActionProcess)executeClass.newInstance();
	        }
      }
      catch (Exception ex)
      {
      	throw new Exception("Failed to load class file '" + dfClass + "'!");
      }
      ISystemConfiguration sys = DBManager.GetSystemConfiguration(Phrase.WebServicesLoggerName);
      if (NodeVersion == Phrase.ver_2)
      {
          process.setNodeURI(sys.GetNodeURL_V2());
      }
      else
      {
          process.setNodeURI(sys.GetNodeURL());
      }
      IActionOperationLog opLog = process.getActionOperationLog();
      opLog.setOperationLogID(this.OpLogID + "");
      opLog.setTransactionID(this.TransID);
      opLog.setUserName(this.UserName);
      opLog.setCredential(this.Password);
      opLog.setRequestedIP(this.RequestorIP);
      opLog.setSecureToken(this.Token);

      return process;
  }

  /**
   * SetLoggerLevel
   * @param 
   * @return 
   */
  private void SetLoggerLevel()
  {
    Logger logger = Logger.getLogger(Phrase.WebServicesLoggerName);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.WebServicesLoggerName);
      if (config != null) {
        Level level = config.GetWebServicesLogLevel();
        if (level != null)
          logger.setLevel(level);
        else
          logger.error("Could Not Find Web Services Log Level in system.config");
      }
      else
        logger.error("Could not get system.config from Configuration File");
    } catch (Exception e) {
      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }
}
