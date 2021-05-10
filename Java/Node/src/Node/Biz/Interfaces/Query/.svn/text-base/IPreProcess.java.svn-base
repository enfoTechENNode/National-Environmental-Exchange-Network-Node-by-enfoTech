package Node.Biz.Interfaces.Query;

import java.math.BigInteger;
import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
/**
 * <p>This class create Query Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPreProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param request The request name
     * @param rowID The row ID
     * @param maxRows The max row
	 * @param params The parameter object array
	 * @param param PreParam object
	 * @throws RemoteException
	 * @return PreParam object
	 */
  public PreParam Execute (String userID, String credential, BigInteger rowID, BigInteger maxRows, Object[] params, PreParam param) throws RemoteException;
}
