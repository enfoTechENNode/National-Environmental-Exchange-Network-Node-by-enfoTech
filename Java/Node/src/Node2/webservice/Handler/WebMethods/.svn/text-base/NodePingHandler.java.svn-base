package Node2.webservice.Handler.WebMethods;

import java.rmi.RemoteException;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import Node.API.NodeUtils;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Biz.Handler.ExecuteOperation;
import Node.Phrase;
/**
 * <p>This class create NodePingHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodePingHandler extends Node.Biz.Handler.WebMethods.NodePingHandler {
	
	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param hello Input string
	   * @return 
	   */
  public NodePingHandler(String requestorIP, String hostName, String hello) {
		super(requestorIP, hostName, hello);
  }

  /**
   * Initialize
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize("DEFAULT",Phrase.WEB_METHOD_NODEPING);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String[] names = new String[] { "Hello" };
        Object[] values = new Object[] { this.Hello };
        utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,null,null,null,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize NodePing Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initialize NodePing Handler",e);
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
        Object[] params = new Object[] { this.Hello };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_NODEPING,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,null,null);
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
