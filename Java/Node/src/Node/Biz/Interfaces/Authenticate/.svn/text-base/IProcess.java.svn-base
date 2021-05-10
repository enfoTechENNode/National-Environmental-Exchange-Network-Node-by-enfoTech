package Node.Biz.Interfaces.Authenticate;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create Authenticate Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IProcess {
	/**
	 * Execute
	 * @param userID The user ID
	 * @param credential The user credential
	 * @param authenticationMethod The authenticationMethod
	 * @param param The input parameters
	 * @throws RemoteException
	 * @return String The token which is returned from NAAS
	 */
  public String Execute (String userID, String credential, String authenticationMethod, ProcParam param) throws RemoteException;
}
