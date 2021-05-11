package Node.Biz.Handler.WebMethods;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.rmi.RemoteException;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.Biz.Handler.ExecuteOperation;
import Node.Biz.Handler.Handler;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
/**
 * <p>This class create ExecuteHandler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ExecuteHandler extends Handler {
	protected String interfaceName = null;
	protected String methodName = null;
	protected Object[] Params = null;
	protected int OpID = -1;
	protected Operation ExecuteOp;
	

	  /**
	   * Constructor.
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param interfaceName Interface name
	   * @param methodName Method Name
	   * @param params parameters objects
	   * @return 
	   */
  public ExecuteHandler(String requestorIP,String hostName,String token, String interfaceName, String methodName, Object[] params) {
    super(requestorIP);
    this.HostName = hostName;
    this.Token = token;
    this.interfaceName = interfaceName;
    this.methodName = methodName;
    this.Params = params;
    this.ExecuteOp = new Operation(interfaceName, methodName);
  }

  /**
   * Initialize.
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.interfaceName,this.methodName);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String paramValues = "";
        if (this.Params != null && this.Params.length > 0) {
          for (int i = 0; i < this.Params.length; i++) {
            if (i != 0) paramValues += ",";
            paramValues += this.Params[i]+"";
          }
        }
        String[] names = new String[] { "Interface Name","Method Name","Parameter Values" };
        String[] values = new String[] { this.interfaceName,this.methodName,paramValues };
        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,this.Token,null,null,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize Execute Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize Query Handler",e);
    }
  }

  /**
   * Authorize.
   * @param 
   * @return String
   */
  protected String Authorize () throws RemoteException
  {
    String userID = null;
    NodeUtils utils = new NodeUtils();
    try {
      userID = this.AuthorizeRequest(this.OpID);
      utils.UpdateOperationLogUserName(Phrase.WebServicesLoggerName,this.TransID,userID);
    } catch (RemoteException e) {
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient Execute Permission",true);
      throw new RemoteException(Phrase.InvalidToken);
    }
    return userID;
  }

  /**
   * ExecuteDataflow.
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_1);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.interfaceName, this.interfaceName);
      process.CreateActionParameter(WebServiceParameter.methodName, this.methodName);
      process.CreateActionParameter(WebServiceParameter.parameters, this.Params);

      return process.Execute(dataflowConfig);
  }
  
  
/*  protected Object Execute () throws RemoteException
  {
	    Object retObj = null;
      ExecuteOperation exeOP = new ExecuteOperation();
	  return exeOP.ExecuteOperation(this.ExecuteOp);
  }
*/
  
  /**
   * Execute.
   * @param 
   * @return Object
   */
  protected Object Execute () throws RemoteException
  {
    Object retObj = null;
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation(this);
        Object[] params = new Object[] { this.Token,this.interfaceName,this.methodName,this.Params };

        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_EXECUTE,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
      }
      else
        throw new RemoteException("Operation is Not Available");
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try
      {
        sw.close();
      } catch (Exception ex) { }
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,sw.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Execute Operation: "+e.toString(),Level.ERROR);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(this.methodName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retObj;
  }

/*  protected Object Execute () throws RemoteException
  {
    Object retObj = null;
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation();
        Object[] params = new Object[] { this.Token,this.interfaceName,this.methodName,this.Params };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_EXECUTE,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
      }
      else
        throw new RemoteException("Operation is Not Available");
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try
      {
        sw.close();
      } catch (Exception ex) { }
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,sw.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Execute Operation: "+e.toString(),Level.ERROR);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retObj;
  }
*/
  
}
