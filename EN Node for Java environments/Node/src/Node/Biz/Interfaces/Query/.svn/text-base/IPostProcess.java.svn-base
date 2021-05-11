package Node.Biz.Interfaces.Query;

import java.math.BigInteger;
import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
/**
 * <p>This class create Query Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param request The request name
     * @param rowID The row ID
     * @param maxRows The max row
	 * @param params The parameter object array
	 * @param param PostParam object
	 * @throws RemoteException
	 * @return PostParam object
	 */
  public PostParam Execute (String userID, String credential, BigInteger rowID, BigInteger maxRows, Object[] params, PostParam param) throws RemoteException;
}
