package Node.Biz.Interfaces.Notify;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Notify Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPreProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param nodeAddress The node address of notification
     * @param dataFlow The data flow name
     * @param docs The notification file object array 
	 * @param param PreParam object
	 * @throws RemoteException
	 * @return PreParam object
	 */
  public PreParam Execute (String token, String nodeAddress, String dataFlow, ClsNodeDocument[] docs, PreParam param) throws RemoteException;
}
