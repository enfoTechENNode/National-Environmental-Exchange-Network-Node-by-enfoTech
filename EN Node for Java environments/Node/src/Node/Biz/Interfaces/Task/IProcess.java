package Node.Biz.Interfaces.Task;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create Submit Task Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
	  /**
	   * Execute
	   * @param parameters Input parameters
	   * @param param PostParam object
	   * @throws
	   * @return Status
	   */
  public String Execute(String[] parameters, ProcParam param);
}
