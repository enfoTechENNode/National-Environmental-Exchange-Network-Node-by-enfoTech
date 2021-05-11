package Node.Biz.Interfaces.Notify;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Notify Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param nodeAddress The node address of notification
     * @param dataFlow The data flow name
     * @param docs The notification file object array 
	 * @param param PostParam object
	 * @throws RemoteException
	 * @return PostParam object
	 */
  public PostParam Execute (String token, String nodeAddress, String dataFlow, ClsNodeDocument[] docs, PostParam param) throws RemoteException;
}
