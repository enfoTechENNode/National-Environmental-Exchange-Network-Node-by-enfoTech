package Node.Biz.Interfaces.Execute;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create Excute Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param interfaceName The interface name of process
	 * @param methodName The method name
	 * @param params The parameter object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String[] The return messages array
	 */
  public String[] Execute (String token, String interfaceName, String methodName, Object[] params, ProcParam param) throws RemoteException;
}
