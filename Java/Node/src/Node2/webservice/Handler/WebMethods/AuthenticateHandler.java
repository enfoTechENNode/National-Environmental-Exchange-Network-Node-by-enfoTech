package Node2.webservice.Handler.WebMethods;

import com.enfotech.basecomponent.utility.security.Cryptography;
import java.rmi.RemoteException;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.*;

import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
/**
 * <p>This class create AuthenticateHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AuthenticateHandler extends Node.Biz.Handler.WebMethods.AuthenticateHandler {


	private String domainName = null;

	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param userID User Id
	   * @param password User password
	   * @param domainName Domain Name
	   * @param method Method type
	   * @return 
	   */
	public AuthenticateHandler(String requestorIP, String hostName, String userID, String password, String domainName, String method) {
		super(requestorIP, hostName, userID, password, method);
		this.domainName = domainName;
	}

	  /**
	   * Initialize
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
        utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,this.UserID,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
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
   * Authorize
   * @param 
   * @return String
   */
  protected String Authorize () throws RemoteException
  {
    return "";
  }

  /**
   * ExecuteDataflow
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_2);
      process.CreateActionParameter(WebServiceParameter.userId, this.UserID);
      process.CreateActionParameter(WebServiceParameter.credential, this.Password);
      process.CreateActionParameter(WebServiceParameter.domain, this.domainName);
      process.CreateActionParameter(WebServiceParameter.authenticationMethod, this.Method);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);

      return process.Execute(dataflowConfig);
  }
  
  /**
   * Execute
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
        Object[] params = new Object[] { this.UserID,this.Password,this.Method,this.domainName };
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
