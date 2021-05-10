package Node.Biz.Interfaces.NodePing;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
/**
 * <p>This class create NodePing Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPreProcess {
	/**
	 * Execute
	 * @param hello the ping message
	 * @param param PreParam object
	 * @throws RemoteException
	 * @return PreParam object
	 */
  public PreParam Execute (String hello, PreParam param) throws RemoteException;
}
