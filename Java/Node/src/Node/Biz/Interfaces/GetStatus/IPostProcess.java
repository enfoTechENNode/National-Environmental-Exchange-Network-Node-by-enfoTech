package Node.Biz.Interfaces.GetStatus;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
/**
 * <p>This class create GetStatus Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param transId The transaction ID
	 * @param param PostParam Object
	 * @throws RemoteException
	 * @return PostParam object
	 */
  public PostParam Execute (String token, String transID, PostParam param) throws RemoteException;
}
