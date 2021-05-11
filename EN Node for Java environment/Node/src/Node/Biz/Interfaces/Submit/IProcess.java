package Node.Biz.Interfaces.Submit;

import java.rmi.RemoteException;

import Node.Biz.Custom.ProcParam;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Submit Process Interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IProcess {
	  /**
	   * Execute
	   * @param token The authentication token
	   * @param transID The transaction ID
	   * @param dataFlow The data flow name
	   * @param docs The submit file array
	   * @param param The process parameter 
	   * @throws RemoteException
	   * @return String The query result
	   */
  public String Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException;
}
