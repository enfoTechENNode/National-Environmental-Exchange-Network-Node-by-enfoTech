package Node.Biz.Interfaces.GetServices;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
/**
 * <p>This class create GetServices Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param serviceType The service type name
	 * @param param PostParam object
	 * @throws RemoteException
	 * @return PostParam object
	 */
  public PostParam Execute (String token, String serviceType, PostParam param) throws RemoteException;
}
