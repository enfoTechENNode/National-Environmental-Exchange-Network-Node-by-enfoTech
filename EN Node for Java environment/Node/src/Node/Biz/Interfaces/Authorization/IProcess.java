package Node.Biz.Interfaces.Authorization;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create Authorization Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param nodeName The node name
	 * @param webMethod The web method name
	 * @param request The request type
	 * @param parameters The parameter array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The token which is returned from NAAS
	 */
  public String Execute(String token, String nodeName, String webMethod, String request, String[] parameters, ProcParam param) throws RemoteException;
}
