package Node.Biz.Interfaces.Download;

import java.rmi.RemoteException;

import Node.Biz.Custom.PostParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Download Post Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public interface IPostProcess {
	/**
	 * Execute
	 * @param token The authentication token
	 * @param transID The transaction ID
	 * @param dataFlow The data flow name
	 * @param docs The download file object array
	 * @param param The PostParam object 
	 * @throws RemoteException
	 * @return PostParam object 
	 */
  public PostParam Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, PostParam param) throws RemoteException;
}
