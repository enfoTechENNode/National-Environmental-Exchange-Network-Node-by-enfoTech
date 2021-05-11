package Node.Biz.Interfaces.Download;

import java.rmi.RemoteException;

import Node.Biz.Custom.PreParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Authenticate Pre Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPreProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param transID The transaction ID
	 * @param dataFlow The data flow name
	 * @param docs The download file object array
	 * @param param The PreParam object 
	 * @throws RemoteException
	 * @return PreParam object 
	 */
  public PreParam Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, PreParam param) throws RemoteException;
}
