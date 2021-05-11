package Node.Biz.Interfaces.NodePing;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
/**
 * <p>This class create NodePing Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPostProcess {
	/**
	 * Execute
	 * @param hello the ping message
	 * @param param PostParam object
	 * @throws RemoteException
	 * @return PostParam object
	 */
  public PostParam Execute (String hello, PostParam param) throws RemoteException;
}
