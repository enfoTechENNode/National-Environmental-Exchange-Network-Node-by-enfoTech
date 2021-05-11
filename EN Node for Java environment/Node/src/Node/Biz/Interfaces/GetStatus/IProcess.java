package Node.Biz.Interfaces.GetStatus;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create GetStatus Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param transId The transaction ID
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The status
	 */
  public String Execute (String token, String transID, ProcParam param) throws RemoteException;
}
