package Node.Biz.Interfaces.Solicit;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Solicit Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
	  /**
	   * Execute
	   * @param token The token
	   * @param returnURLorDataFlow The return URL or the data flow name
	   * @param request The request
	   * @param parameters The input parameters
	   * @param ProcParam ProcParam object
	   * @throws RemoteException
	   * @return The document
	   */
	/* For version 2.0 the ProcParam includes recipients and notificationURI
	 * 
	 * 
	 */
  public ClsNodeDocument Execute (String token, String returnURLorDataFlow, String request, Object[] parameters, ProcParam param) throws RemoteException;
}
