package Node2.webservice.Handler.WebMethods;

import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.NotificationURIType;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionParameter;
import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create SubmitHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class SubmitHandler extends Node.Biz.Handler.WebMethods.SubmitHandler {
	private String flowOperation = null;
	private String[] recipients = null;
	private NotificationURIType[] notificationURIType = null;
	private String notificationType = null;
	
	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param transID Transaction ID
	   * @param dataFlow Data flow name
	   * @param flowOperation flow operation name
	   * @param recipients Recipient array
	   * @param notificationURIType NotificationURIType Objects
	   * @param docs Submit documents
	   * @return 
	   */
	public SubmitHandler(String requestorIP, String hostName, String token, String transID, String dataFlow,String flowOperation,String[] recipients,NotificationURIType[] notificationURIType, ClsNodeDocument[] docs) {
		super(requestorIP, hostName, token, transID, dataFlow, docs);
		this.flowOperation = flowOperation;
		this.recipients = recipients;
		this.notificationURIType = notificationURIType;
		if(notificationURIType!=null && notificationURIType[0]!=null)
			this.notificationType = notificationURIType[0].getNotificationType().getValue();
	}

	  /**
	   * Initialize
	   * @param 
	   * @return 
	   */
	  protected void Initialize () throws RemoteException
	  {
	    try {
	    	// WI 20786
	      this.OpID = this.Initialize(this.DataFlow, this.flowOperation,Phrase.WEB_METHOD_SUBMIT);
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
	        String[] names = new String[] { "FlowOperation",docsParamName };
	        Object[] values = new Object[] { this.flowOperation,docsValue };
	        
	        // add search parameter 
	        Object[] ret = new Object[2] ;
	        ret = utils.AddSearchParameter(Integer.toString(this.OpID), names, values);

	        if(ret != null && ret[0] != null && ret[1] != null){
	            Object[] tmpName = (Object[])ret[0];
	            names = new String[tmpName.length];
	            for(int i=0;i<names.length;i++){
	                names[i] = tmpName[i].toString();                  
	            }
	            values = (Object[])ret[1];        	
	        }
	        
	        // Create log file with new added search parameters
	        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
	                                 this.RequestorIP,this.SupplTransID,this.Token,null,null,null,this.HostName,names,values);
	      }
	      else
	        throw new RemoteException(Phrase.ServiceUnavailable);
	    } catch (RemoteException e) {
	      NodeUtils nodeUtils = new NodeUtils();
	      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
	      throw e;
	    } catch (Exception e) {
	      this.Log("Could Not Initialize Submit Handler: "+e.toString(),Level.ERROR);
	      try {
	        NodeUtils nodeUtils = new NodeUtils();
	        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
	      } catch (Exception ex) { }
	      throw new RemoteException("Could Not Initilize Submit Handler",e);
	    }
	  }

	  /**
	   * Authorize
	   * @param 
	   * @return String
	   */
	protected String Authorize() throws RemoteException {
		String userID = null;
		NodeUtils utils = new NodeUtils();
		try {
			userID = this.AuthorizeRequest(this.OpID);
			utils.UpdateOperationLogUserName(Phrase.WebServicesLoggerName,
					this.TransID, userID);
		} catch (RemoteException e) {
			utils.UpdateOperationLog(Phrase.WebServicesLoggerName,
					this.TransID, Phrase.FailedStatus, Phrase.InvalidToken
							+ ": Insufficient Submit Permission", true);
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
        process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
        process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
        process.CreateActionParameter(WebServiceParameter.dataflow, this.DataFlow);
        process.CreateActionParameter(WebServiceParameter.flowOperation, this.flowOperation);
        process.CreateActionParameter(WebServiceParameter.documents, this.Docs);
        process.CreateActionParameter(WebServiceParameter.recipient, this.recipients);
        process.CreateActionParameter(WebServiceParameter.notificationURI, this.notificationURIType);

        this.TransID = (String)((IActionParameter)process.Execute(dataflowConfig)).getParameterValue();
        return this.TransID;
    }

    /**
     * Execute
     * @param 
     * @return Object
     */
	protected Object Execute() throws RemoteException {
		Object retObj = null;
		try {
			if (this.OpID >= 0) {
				INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
				ExecuteOperation exeOP = new ExecuteOperation(this);
				Object[] params = new Object[] { this.Token, this.SupplTransID,
						this.DataFlow, this.flowOperation, this.recipients,
						this.notificationURIType, this.notificationType,this.Docs };
				retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_SUBMIT, opDB
						.GetOperationConfig(this.OpID), params, this.TransID,
						this.RequestorIP, this.LoggerName, this.UserName,
						this.Password);
			} else
				throw new RemoteException("Operation is Not Available");
		} catch (RemoteException e) {
			NodeUtils utils = new NodeUtils();
			utils.UpdateOperationLog(Phrase.WebServicesLoggerName,
					this.TransID, Phrase.FailedStatus, e.toString(), true);
			throw e;
		} catch (Exception e) {
			this.Log("Could Not Execute Operation: " + e.toString(),
					Level.ERROR);
			NodeUtils nodeUtils = new NodeUtils();
			nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,
					this.TransID, Phrase.FailedStatus, e.toString(), true);
			throw new RemoteException(Phrase.InternalError, e);
		}
		return retObj;
	}
}
