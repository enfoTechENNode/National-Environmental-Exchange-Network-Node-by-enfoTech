package Node.Biz.Interfaces.Download;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Authenticate Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param transID The transaction ID
	 * @param dataFlow The data flow name
	 * @param docs The download file object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return ClsNodeDocument[] The download file object array
	 */
  public ClsNodeDocument[] Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException;
}
