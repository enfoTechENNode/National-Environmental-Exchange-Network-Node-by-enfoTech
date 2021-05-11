package Node.Biz.Interfaces.Execute;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
/**
 * <p>This class create Excute Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param interfaceName The interface name of process
	 * @param methodName The method name
	 * @param params The parameter object array
	 * @param param The PostParam object 
	 * @throws RemoteException
	 * @return PostParam object 
	 */
  public PostParam Execute (String Token, String interfaceName, String methodName, Object[] params, PostParam param) throws RemoteException;
}
