package Node.Biz.Default.Authorization;

import java.rmi.RemoteException;

import Node.Biz.Administration.Operation;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Authorization.IProcess;
import Node.Biz.Handler.NAASIntegration;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.Phrase;

/**
 * <p>This class create Authorization Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */
public class Process implements IProcess {
	  /**
	   * Constructor.
	   * @return 
	   */
  public Process() {
  }

	/**
	 * Execute
	 * @param token The authentication token
	 * @param nodeName The node name
	 * @param webMethod The web method name
	 * @param request The request type
	 * @param parameters The parameter array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The token which is returned from NAAS
	 */
  public String Execute(String token, String nodeName, String webMethod, String request, String[] parameters, ProcParam param) throws RemoteException
  {
    String user = null;
    try
    {
      if (token.startsWith("ndlc:")) {
        INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
        user = logDB.AuthorizeToken(token, webMethod, request);
        if (user == null)
          throw new RemoteException(Phrase.InvalidToken);
      }
      else {
        NAASIntegration naas = new NAASIntegration(Phrase.WebServicesLoggerName);
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        Operation op = opDB.GetOperation(request, webMethod);
        // WI 33641
        if (op != null && token != null && token.equalsIgnoreCase(Phrase.PUBLIC_USERS) && op.getIsRest() != null && op.getIsRest().equalsIgnoreCase(Phrase.SIMPLE_YES)){
        	user = Phrase.PUBLIC_USERS;
        }else if (op != null && token != null){
        	user = naas.Authorize(token, param.GetRequestorIP(), op.GetWebService(), op.GetOpName(), null);
        }else
        	throw new RemoteException(Phrase.ServiceUnavailable);
      }
    }
    catch (RemoteException e)
    {
      throw e;
    }
    catch (Exception e)
    {
      throw new RemoteException("Failed to Authorize", e);
    }
    return user;
  }
}
