package Node2.webservice.Handler.WebMethods;

import java.rmi.RemoteException;
import java.util.ArrayList;

import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.StatusResponseType;
import net.exchangenetwork.www.schema.node._2.TransactionStatusCode;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.Utility;
/**
 * <p>This class create NotifyHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NotifyHandler extends Node.Biz.Handler.WebMethods.NotifyHandler {
	private NotificationMessageType[] Messages = null;
	private int OpID = -1;

	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param nodeAddress Remote node end point
	   * @param dataFlow Data flow name
	   * @param messages Notification message type objects
	   * @return 
	   */
	public NotifyHandler(String requestorIP, String hostName, String token,
			String nodeAddress, String dataFlow, NotificationMessageType[] messages) {
		super( requestorIP,  hostName,  token,  nodeAddress,  dataFlow, null);
		this.Messages = messages;
	}

	  /**
	   * Initialize
	   * @param 
	   * @return 
	   */
	protected void Initialize() throws RemoteException {
		String[] objectId = null;
		String[] messageType = null;
		String[] messageName = null;
		String[] status = null;
		String[] statusDetail = null;

		try {
			this.OpID = this.Initialize(this.DataFlow, Phrase.WEB_METHOD_NOTIFY);
			if (this.OpID >= 0) {
				NodeUtils utils = new NodeUtils();
				String messageParamName = null;
				String messageValue = null;
				if (this.Messages != null) {
					if (this.Messages.length > 1) {
						messageParamName = "Number of Messages";
						messageValue = this.Messages.length + "";
					} else if (this.Messages.length == 1) {
						messageParamName = "Message Name";
						if (this.Messages[0] != null)
							messageValue = this.Messages[0].getMessageName();
						else
							messageValue = "null";
					} else {
						messageParamName = "Number of Messages";
						messageValue = "0";
					}
				} else {
					messageParamName = "Number of Messages";
					messageValue = "0";
				}
				// add multiple messages
				objectId = new String[this.Messages.length];
				messageType = new String[this.Messages.length];
				messageName = new String[this.Messages.length];
				status = new String[this.Messages.length];
				statusDetail = new String[this.Messages.length];
				for (int i = 0; i < this.Messages.length; i++) {
					objectId[i] = this.Messages[i].getObjectId()==null?"":this.Messages[i].getObjectId().toString();
					messageType[i] = this.Messages[i].getMessageCategory()==null?"":this.Messages[i].getMessageCategory().getValue();
					messageName[i] = this.Messages[i]==null?"":this.Messages[i].getMessageName();
					status[i] = this.Messages[i]==null?"":this.Messages[i].getStatus().getValue();
					statusDetail[i] = this.Messages[i]==null?"":this.Messages[i].getStatusDetail();
				}
				
				String[] names = new String[] { "Data Flow", messageParamName,
						"Security Token", "Node Address", };
				Object[] values = new Object[] { this.DataFlow, messageValue,
						this.Token, this.NodeAddress };
				
				ArrayList tNames = new ArrayList();
				ArrayList tValues = new ArrayList();
				
				for(int j=0;j<names.length;j++){
					tNames.add(names[j]);
				}
				for(int j=0;j<values.length;j++){
					tValues.add(values[j]);
				}
				for(int j=0;j<this.Messages.length;j++){
					tNames.add("Object Id");
					tNames.add("Message Type");
					tNames.add("Status");
					tNames.add("Status Detail");
					tValues.add(objectId[j]);
					tValues.add(messageType[j]);
					tValues.add(status[j]);
					tValues.add(statusDetail[j]);
				}
				names = new String[tNames.size()];				
				for(int j=0;j<tNames.size();j++){
					names[j] = (String)tNames.get(j);
				};
				utils.CreateOperationLog(Phrase.WebServicesLoggerName,
						this.OpID, this.UserName, this.TransID, Phrase.ReceivedStatus,
						Phrase.ReceivedMessage, this.RequestorIP, null,
						this.Token, this.NodeAddress, null, null,
						this.HostName, names, tValues.toArray());
			} else
				throw new RemoteException(Phrase.ServiceUnavailable);
		} catch (RemoteException e) {
			NodeUtils nodeUtils = new NodeUtils();
			nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,
					this.TransID, Phrase.FailedStatus, e.toString(), true);
			throw e;
		} catch (Exception e) {
			this.Log("Could Not Initialize Notify Handler: " + e.toString(),
					Level.ERROR);
			try {
				NodeUtils nodeUtils = new NodeUtils();
				nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,
						this.TransID, Phrase.FailedStatus, e.toString(), true);
			} catch (Exception ex) {
			}
			throw new RemoteException("Could Not Initilize Notify Handler", e);
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
							+ ": Insufficient Notify Permission", true);
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
        process.CreateActionParameter(WebServiceParameter.nodeAddress, this.NodeAddress);
        process.CreateActionParameter(WebServiceParameter.dataflow, this.DataFlow);
        process.CreateActionParameter(WebServiceParameter.messages, this.Messages);

        return process.Execute(dataflowConfig);
    }
    
    /**
     * Execute
     * @param 
     * @return Object
     */
	protected Object Execute() throws RemoteException {
		Object retObj = null;
        StatusResponseType statusResponseType = new StatusResponseType();
		try {
			if (this.OpID >= 0) {
				INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
				ExecuteOperation exeOP = new ExecuteOperation(this);
				Object[] params = new Object[] { this.Token, this.NodeAddress,this.DataFlow,null,this.Messages };
				retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_NOTIFY, opDB
						.GetOperationConfig(this.OpID), params, this.TransID,
						this.RequestorIP, this.LoggerName, this.UserName,
						this.Password);
	        	if (retObj != null && !retObj.equals("")) {
	                statusResponseType.setTransactionId(this.TransID);
	                statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue((String)retObj));
	                statusResponseType.setStatusDetail(Utility.getPhraseMsgByContent((String)retObj));    		
	        	}else{
	                statusResponseType.setTransactionId(this.TransID);
	                statusResponseType.setStatus(TransactionStatusCode.Factory.fromValue(Phrase.FailedStatus));
	                statusResponseType.setStatusDetail("There are no any messages provided. "+Phrase.FailedMessage);    		    		
	        	}
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
		return statusResponseType;
	}
}
