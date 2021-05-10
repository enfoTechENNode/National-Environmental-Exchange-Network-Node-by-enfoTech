package Node.Biz.Default.Execute;

import java.rmi.RemoteException;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Execute.IProcess;
import Node.Phrase;
/**
 * <p>This class create Execute Process.</p>
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
	 * @param token The authentication token
	 * @param interfaceName The interface name of process
	 * @param methodName The method name
	 * @param params The parameter object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String[] The return messages array
	 */
  public String[] Execute (String token, String interfaceName, String methodName, Object[] params, ProcParam param) throws RemoteException{
	  String[] retStr = {param.GetTransID(),Phrase.CompleteStatus,"<Result>This result is returned by default EXECUTE.</Result>"};

	  return retStr;
  }


}
