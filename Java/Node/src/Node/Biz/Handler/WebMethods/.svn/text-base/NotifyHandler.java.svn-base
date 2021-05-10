package Node.Biz.Handler.WebMethods;

import java.rmi.RemoteException;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.*;

import Node.API.NodeUtils;
import Node.Biz.Handler.Handler;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create NotifyHandler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NotifyHandler extends Handler {
	protected String NodeAddress = null;
	protected String DataFlow = null;
	protected ClsNodeDocument[] Docs = null;
	protected int OpID = -1;

	  /**
	   * Constructor.
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param nodeAddress Remote node end point
	   * @param dataFlow Data flow name
	   * @param docs Document objects
	   * @return 
	   */
  public NotifyHandler(String requestorIP, String hostName, String token, String nodeAddress, String dataFlow, ClsNodeDocument[] docs) {
    super(requestorIP);
    this.HostName = hostName;
    this.Token = token;
    this.NodeAddress = nodeAddress;
    this.DataFlow = dataFlow;
    this.Docs = docs;
  }

  /**
   * Initialize.
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.DataFlow,Phrase.WEB_METHOD_NOTIFY);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String docsParamName = null;
        String docsValue = null;
        if (this.Docs != null) {
          if (this.Docs.length > 1) {
            docsParamName = "Number of Documents";
            docsValue = this.Docs.length+"";
          }
          else if (this.Docs.length == 1) {
            docsParamName = "Document Name";
            if (this.Docs[0] != null)
              docsValue = this.Docs[0].getName();
            else
              docsValue = "null";
          }
          else {
            docsParamName = "Number of Documents";
            docsValue = "0";
          }
        }
        else {
          docsParamName = "Number of Documents";
          docsValue = "0";
        }
        String[] names = new String[] { "DataFlow",docsParamName };
        Object[] values = new Object[] { this.DataFlow,docsValue };
        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,this.Token,this.NodeAddress,null,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize Notify Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize Notify Handler",e);
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
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient Notify Permission",true);
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
      //data flow
      IActionProcess process = GetActionProcess(Phrase.ver_1);
      process.getActionOperationLog().setDocuments(this.Docs);

      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.nodeAddress, this.NodeAddress);
      process.CreateActionParameter(WebServiceParameter.dataflow, this.DataFlow);
      process.CreateActionParameter(WebServiceParameter.documents, this.Docs);

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
        Object[] params = new Object[] { this.Token,this.NodeAddress,this.DataFlow,this.Docs };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_NOTIFY,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
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
