package Node.Biz.Interfaces.Execute;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
/**
 * <p>This class create Execute Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPreProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param interfaceName The interface name of process
	 * @param methodName The method name
	 * @param params The parameter object array
	 * @param param The PreParam object 
	 * @throws RemoteException
	 * @return PreParam object 
	 */
  public PreParam Execute (String Token, String interfaceName, String methodName, Object[] params, PreParam param) throws RemoteException;
}
