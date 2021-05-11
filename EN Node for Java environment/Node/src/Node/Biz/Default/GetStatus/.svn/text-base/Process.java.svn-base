package Node.Biz.Default.GetStatus;

import java.rmi.RemoteException;

import org.apache.log4j.Level;

import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.GetStatus.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperationLog;
import Node.Utils.LoggingUtils;
import Node.Phrase;
/**
 * <p>This class create GetStatus Process.</p>
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
	 * @param transId The transaction ID
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The status
	 */
  public String Execute (String token, String transID, ProcParam param) throws RemoteException
  {
	LoggingUtils.Log("GetStatus>>>Process>>> The token is: " + token + " The transID is:"+transID,Level.DEBUG,Phrase.WebServicesLoggerName);
    if (transID == null)
      throw new RemoteException(Phrase.InvalidParameter);
    String retString = null;
    try {
      INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
      retString = logDB.GetStatus(transID);
  	  LoggingUtils.Log("GetStatus>>>Process>>> The return status is: " + retString,Level.DEBUG,Phrase.WebServicesLoggerName);
      if (retString == null){
        throw new RemoteException(Phrase.InvalidParameter);
      }else if (retString.equalsIgnoreCase(Phrase.DoneStatus)){ // WI 25161
    	  retString = Phrase.ProcessingStatus;
      }
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retString;
  }
}
