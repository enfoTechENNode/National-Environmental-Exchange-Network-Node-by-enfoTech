package Node2.webservice.Handler.WebMethods;

import java.rmi.RemoteException;
import org.apache.log4j.Level;
import DataFlow.Component.Interface.*;

import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.LoggingUtils;
import Node.Phrase;
/**
 * <p>This class create GetServicesHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetServicesHandler extends Node.Biz.Handler.WebMethods.GetServicesHandler {

	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param serviceType Service type
	   * @return 
	   */
  public GetServicesHandler(String requestorIP, String hostName, String token, String serviceType) {
    super( requestorIP,  hostName,  token,  serviceType);
  }

  /**
   * Initialize
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.ServiceType,Phrase.WEB_METHOD_GETSERVICES);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,this.Token,null,null,this.ServiceType,this.HostName,null,null);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize GetServices Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize GetServices Handler",e);
    }
  }

  /**
   * Authorize
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
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient GetServices Permission",true);
      throw new RemoteException(Phrase.InvalidToken);
    }
    return userID;
  }

  /**
   * ExecuteDataflow
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_2);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.serviceCategory, this.ServiceType);

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
    String version2 = "true";
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation(this);
        Object[] params = new Object[] { this.Token,this.ServiceType,version2 };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_GETSERVICES,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
        LoggingUtils.Log("GetServicesHandler>>> Return from process", Level.DEBUG, Phrase.WebServicesLoggerName);
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
