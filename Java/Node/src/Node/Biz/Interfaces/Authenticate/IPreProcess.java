package Node.Biz.Interfaces.Authenticate;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
/**
 * <p>This class create Authenticate Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPreProcess {
	/**
	 * Execute
	 * @param userID The user ID
	 * @param credential The user credential
	 * @param authenticationMethod The authenticationMethod
	 * @param param PreParam object
	 * @throws RemoteException
	 * @return PreParam object
	 */
  public PreParam Execute (String userID, String credential, String authenticationMethod, PreParam param) throws RemoteException;
}
