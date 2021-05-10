package Node.Biz.Interfaces.Query;

import java.math.BigInteger;
import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
/**
 * <p>This class create Query Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param request The request name
     * @param rowID The row ID
     * @param maxRows The max row
	 * @param params The parameter object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The query result
	 */
  public String Execute (String token, String request, BigInteger rowID, BigInteger maxRows, Object[] params, ProcParam param) throws RemoteException;
}
