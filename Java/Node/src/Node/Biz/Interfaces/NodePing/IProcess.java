package Node.Biz.Interfaces.NodePing;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create NodePing Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
	/**
	 * Execute
	 * @param hello the ping message
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The return message
	 */
  public String Execute (String hello, ProcParam param) throws RemoteException;
}
