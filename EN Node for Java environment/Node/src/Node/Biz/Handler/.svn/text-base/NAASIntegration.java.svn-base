package Node.Biz.Handler;

import java.rmi.RemoteException;
import org.apache.log4j.Level;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.NAAS.Requestor.NAASRequestor;
import Node.NAAS.Types.Auth.AuthMethod;
import Node.NAAS.Types.Auth.PasswordType;
import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create NAASIntegration Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NAASIntegration {
  private String LoggerName = null;

  /**
   * Constructor.
   * @param loggerName
   * @return 
   */
  public NAASIntegration(String loggerName) {
    this.LoggerName = loggerName;
  }

  /**
   * Authenticate.
   * @param user User name
   * @param password Password
   * @param requestorIP Requester IP address
   * @return Token
   */
  public String Authenticate (String user, String password, String requestorIP) throws RemoteException
  {
    String token = null;
    try {
      NAASRequestor requestor = null;
      try {
        requestor = new NAASRequestor(this.LoggerName);
      } catch (Exception e) {
        throw new RemoteException(Phrase.ServiceUnavailable);
      }
      token = requestor.centralAuth(user, password, AuthMethod.password, requestorIP);
    } catch (RemoteException e) {
      throw e;
    }
    return token;
  }

  /**
   * Authenticate.
   * @param user User name
   * @param password Password
   * @return Token 
   */
  public String Authenticate (String user, String password) throws RemoteException
  {
    String token = null;
    try {
      NAASRequestor requestor = null;
      try {
        requestor = new NAASRequestor(this.LoggerName);
      } catch (Exception e) {
        throw new RemoteException(Phrase.ServiceUnavailable);
      }
      PasswordType pwd = new PasswordType(password);
      token = requestor.authenticate(user,pwd,AuthMethod.password);
    } catch (RemoteException e) {
      throw e;
    }
    return token;
  }

  /**
   * Authorize.
   * @param token Token
   * @param requestorIP Requester IP address
   * @param methodName Method name
   * @param requestName request Name
   * @param paramNames parameter Names
   * @return String
   */
  public String Authorize (String token, String requestorIP, String methodName, String requestName, String[] paramNames) throws RemoteException
  {
    String userID = null;
    try {
      if (token == null || token.equals("") || requestorIP == null || requestorIP.equals("") || methodName == null || methodName.equals(""))
        throw new Exception("Please supply token, requestorIP, and methodName");
      // Get Node Name
      ISystemConfiguration config = DBManager.GetSystemConfiguration(this.LoggerName);
      String nodeURL = config.GetNodeURL();
      String nodeName = config.GetNodeName();
      // Construct RequestURI
      String requestURI = nodeURL + "?node=" + nodeName + "&method=" + methodName;
      if (requestName != null && !requestName.equals("")) {
        requestURI += "&request=" + requestName;
        if (paramNames != null && paramNames.length > 0) {
          requestURI += "&Params=";
          for (int i = 0; i < paramNames.length; i++) {
            requestURI += paramNames[i];
            if (i < paramNames.length - 1)
              requestURI += ";";
          }
        }
      }
      //Authorize
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      userID = requestor.validate(token,requestorIP,requestURI);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not authorize: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return userID;
  }
}
