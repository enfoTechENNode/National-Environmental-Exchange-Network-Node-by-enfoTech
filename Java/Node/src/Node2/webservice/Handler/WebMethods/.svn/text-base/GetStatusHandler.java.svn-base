package Node2.webservice.Handler.WebMethods;

import java.rmi.RemoteException;
import net.exchangenetwork.www.schema.node._2.StatusResponseType;
import net.exchangenetwork.www.schema.node._2.TransactionStatusCode;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.Utility;
import Node.Phrase;
/**
 * <p>This class create GetStatusHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class GetStatusHandler extends Node.Biz.Handler.WebMethods.GetStatusHandler {
	
	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param transID Transaction ID
	   * @return 
	   */
  public GetStatusHandler(String requestorIP, String hostName, String token, String transID) {
		super(requestorIP, hostName, token, transID);
  }

  /**
   * Initialize
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize("DEFAULT",Phrase.WEB_METHOD_GETSTATUS);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,this.SupplTransID,this.Token,null,null,null,this.HostName,null,null);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize GetStatus Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize GetStatus Handler",e);
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
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient GetStatus Permission",true);
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
      //data flow
      IActionProcess process = GetActionProcess(Phrase.ver_2);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
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
    StatusResponseType statusResponseType = new StatusResponseType();
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation(this);
        Object[] params = new Object[] { this.Token,this.SupplTransID };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_GETSTATUS,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
    	if (retObj != null && !retObj.equals("")) {
            statusResponseType.setTransactionId(this.TransID);
            statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue((String)retObj));
            statusResponseType.setStatusDetail(Utility.mapStatusMessage((String)retObj));    		
    	}
    	else{
            statusResponseType.setTransactionId("No transaction ID");
            statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue(Phrase.FailedStatus));
            statusResponseType.setStatusDetail("There are no any messages provided. "+Phrase.FailedMessage);    		    		
    	}
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
    return statusResponseType;
  }
}
