package Node.Biz.Interfaces.Solicit;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
/**
 * <p>This class create Solicit Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPreProcess {
	  /**
	   * Execute
	   * @param token The token
	   * @param returnURL The return URL or the data flow name
	   * @param request The request
	   * @param parameters The input parameters
	   * @param param PreParam object
	   * @throws RemoteException
	   * @return PreParam object
	   */
  public PreParam Execute (String token, String returnURL, String request, Object[] params, PreParam param) throws RemoteException;
}
