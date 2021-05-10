package Node.Biz.Interfaces.Notify;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Notify Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
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
  public String Execute (String token, String nodeAddress, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException;
}
