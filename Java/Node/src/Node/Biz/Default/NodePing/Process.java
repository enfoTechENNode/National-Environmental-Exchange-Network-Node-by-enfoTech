package Node.Biz.Default.NodePing;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.NodePing.IProcess;
/**
 * <p>This class create NodePing Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Process implements IProcess {
	  /**
	   * Constructor.
	   * @param
	   * @return 
	   */
  public Process() {
  }

	/**
	 * Execute
	 * @param hello the ping message
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The return message
	 */
  public String Execute (String hello, ProcParam param) throws RemoteException
  {
    return "Ready";
  }
}
