package Node.Biz.Default.Notify;

import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.NotificationMessageType;

import Node.API.DocumentManagement;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Notify.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.Phrase;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Notify Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Process implements IProcess {
	  /**
	   * Constructor.
	   * @param
	   * @return 
	   */
  public Process() {
  }

	/**
	 * Execute
	 * @param token The authentication token
	 * @param nodeAddress The node address of notification
     * @param dataFlow The data flow name
     * @param docs The notification file object array 
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The status
	 */
 public String Execute (String token, String nodeAddress, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException
  {
  //  if (docs == null || docs.length <= 0)
  //    throw new RemoteException(Phrase.InvalidParameter);
    String retString = null;
    try {
		if(param.GetHashtable().containsKey("messageType")){	// handle 2.0
			NotificationMessageType[] messageType = (NotificationMessageType[])param.GetHashtable().get("messageType");
			if (messageType[0].getObjectId() == null || messageType[0].getObjectId().toString().equals(""))
				throw new RemoteException(Phrase.InvalidParameter+" ObjectID is empty.");
			if(messageType[0].getMessageCategory().getValue().equalsIgnoreCase(Phrase.NOTIFY_DOCUMENT)){
				INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
				retString = logDB.GetStatus(messageType[0].getObjectId().toString());
				if(!retString.equalsIgnoreCase(messageType[0].getStatus().getValue())){
					retString = null;
				}				
			}else if(messageType[0].getMessageCategory().getValue().equalsIgnoreCase(Phrase.NOTIFY_EVENT)){
				INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
				retString = logDB.GetStatus(messageType[0].getObjectId().toString());
				if(!retString.equalsIgnoreCase(messageType[0].getStatus().getValue())){
					retString = null;
				}				
			}else if(messageType[0].getMessageCategory().getValue().equalsIgnoreCase(Phrase.NOTIFY_STATUS)){
				INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
				retString = logDB.GetStatus(messageType[0].getObjectId().toString());
				if(!retString.equalsIgnoreCase(messageType[0].getStatus().getValue())){
					retString = null;
				}				
			}
		}else{	// handle 1.1
			if (docs != null && docs.length > 0) {
				DocumentManagement docManage = new DocumentManagement(Phrase.WebServicesLoggerName);
				if (docManage.Upload(docs,param.GetTransID(),"Notified",dataFlow,nodeAddress,token,Utility.GetNowDate(),Utility.GetNowTimeStamp(), null))
					retString = param.GetTransID();
				else
					throw new RemoteException(Phrase.InternalError);
			}			
		}
     } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retString;
  }
}
