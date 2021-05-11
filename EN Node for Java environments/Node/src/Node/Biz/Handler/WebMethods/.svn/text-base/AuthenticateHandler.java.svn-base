package Node.Biz.Handler.WebMethods;

import java.rmi.RemoteException;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.Biz.Handler.Handler;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;

import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create AuthenticateHandler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AuthenticateHandler extends Handler {
  protected String UserID = null;
  protected String Password = null;
  protected String Method = null;
  protected int OpID = -1;

  /**
   * Constructor.
   * @param requestorIP Requester IP address
   * @param hostName Host name
   * @param userID User Id
   * @param password User password
   * @param method Method type
   * @return 
   */
  public AuthenticateHandler(String requestorIP, String hostName, String userID, String password, String method) {
    super(requestorIP);
    this.HostName = hostName;
    this.UserID = userID;
    this.Password = password;
    this.Method = method;
  }

  /**
   * Initialize.
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.Method,Phrase.WEB_METHOD_AUTHENTICATE);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String[] names = new String[] { "User ID",Phrase.ParamCredential,"Authentication Method" };
        Cryptography crypt = new Cryptography();
        String password = this.Password != null ? crypt.Encrypting(this.Password,Phrase.CryptKey) : null;
        Object[] values = new Object[] { this.UserID,password,this.Method };
        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,this.UserID,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,null,null,null,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize Authentication Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initialize Authenticate Handler",e);
    }
  }

  /**
   * Authorize.
   * @param 
   * @return String
   */
 protected String Authorize () throws RemoteException
  {
    return "";
  }

 /**
  * ExecuteDataflow.
  * @param dataflowConfig Data flow configuration content
  * @return Object
  */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      //for dataflow
      IActionProcess process = GetActionProcess(Phrase.ver_1);
      process.getActionOperationLog().setCredential(this.Password);
      process.getActionOperationLog().setAuthMethod(this.Method);
      
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.userId, this.UserID);
      process.CreateActionParameter(WebServiceParameter.credential, this.Password);
      process.CreateActionParameter(WebServiceParameter.authenticationMethod, this.Method);

      return process.Execute(dataflowConfig);
  }

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
        Object[] params = new Object[] { this.UserID,this.Password,this.Method };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_AUTHENTICATE,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserID,this.Password);
        NodeUtils utils = new NodeUtils();
        utils.UpdateOperationLogToken(Phrase.WebServicesLoggerName,this.TransID,(String)retObj);
      }
      else
        throw new RemoteException("Operation is Not Available");
    } catch (RemoteException e) {
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Execute Operation: "+e.toString(),Level.ERROR);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retObj;
  }
}
