package Node.Biz.Default.Authenticate;

import java.rmi.RemoteException;
import org.apache.log4j.Level;

import Node.API.NodeUtils;
import Node.Biz.Administration.User;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Handler.NAASIntegration;
import Node.Biz.Interfaces.Authenticate.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeUser;
import Node.Phrase;
import Node.Utils.LoggingUtils;

/**
 * <p>
 * This class create Authenticate Process.
 * </p>
 * <p>
 * Company: enfoTech & Consulting, Inc.
 * </p>
 * 
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
	 * @param userID The user ID
	 * @param credential The user credential
	 * @param authenticationMethod The authenticationMethod
	 * @param param The input parameters
	 * @throws RemoteException
	 * @return String The token which is returned from NAAS
	 */

	public String Execute(String userID, String credential,
			String authenticationMethod, ProcParam param)
			throws RemoteException {
		if (userID == null || credential == null)
			throw new RemoteException(Phrase.InvalidParameter);
		NAASIntegration naas = new NAASIntegration(Phrase.WebServicesLoggerName);
		String token = null;
		String domainName = (String) param.GetHashtable().get("domainName");
		String[] domainNameList = null;
		try {
			INodeDomain domainDB = DBManager
					.GetNodeDomain(Phrase.WebServicesLoggerName);
			domainNameList = domainDB.GetDomains();
			boolean validDomain = false;
			if (domainName != null && !domainName.trim().equalsIgnoreCase("")
					&& !domainName.equalsIgnoreCase("default")
					&& !domainName.equalsIgnoreCase("null")) {
				for (int i = 0; i < domainNameList.length; i++) {
					if (domainNameList[i].equalsIgnoreCase(domainName)) {
						validDomain = true;
						break;
					}
				}
			} else {
				validDomain = true;
			}
			if (validDomain) {
				User user = new User(userID, Phrase.WebServicesLoggerName);
				if (user.IsLocalNodeUser()) {
					token = this.LocalAuthenticate(userID, credential, param);
				} else {
					token = naas.Authenticate(userID, credential, param
							.GetRequestorIP());
				}
			} else {
				LoggingUtils.Log("The domain is invalid " + domainName,
						Level.ERROR, Phrase.WebServicesLoggerName);
				LoggingUtils.Log(Phrase.UnknownUser + ": "
						+ Phrase.UnknownUserMSG, Level.ERROR,
						Phrase.WebServicesLoggerName);
				throw new RemoteException(Phrase.UnknownUser);
			}
		} catch (Exception e) {
			if (e.getMessage().equalsIgnoreCase(
					"Unable to authenticate the user")) {
				throw new RemoteException(Phrase.InvalidCredential);
			} else
				throw new RemoteException(e.getMessage());
			/*
			 * try{ token = this.LocalAuthenticate(userID, credential, param);
			 * }catch(RemoteException re){ throw re; }
			 */
		}
		return token;
	}

	/**
	 * LocalAuthenticate
	 * @param userID The user ID
	 * @param credential The user credential
	 * @param param The input parameters
	 * @throws RemoteException
	 * @return String The token which is returned from NAAS
	 */
	private String LocalAuthenticate(String userID, String credential,
			ProcParam param) throws RemoteException {
		String token = null;
		String[] domainNameList = null;
		String domainName = (String) param.GetHashtable().get("domainName");
		try {
			INodeUser userDB = DBManager
					.GetNodeUser(Phrase.WebServicesLoggerName);
			INodeDomain domainDB = DBManager
					.GetNodeDomain(Phrase.WebServicesLoggerName);
			domainNameList = domainDB.GetDomains();
			boolean validDomain = false;
			if (domainName != null && !domainName.trim().equalsIgnoreCase("")
					&& !domainName.equalsIgnoreCase("default")
					&& !domainName.equalsIgnoreCase("null")) {
				for (int i = 0; i < domainNameList.length; i++) {
					if (domainNameList[i].equalsIgnoreCase(domainName)) {
						validDomain = true;
						break;
					}
				}
			} else {
				validDomain = true;
			}
			int validate = userDB.AuthenticateLogin(userID, credential);
			if (validate == userDB.SUCCESSFUL && validDomain) {
				NodeUtils nodeUtils = new NodeUtils();
				token = "ndlc:" + nodeUtils.GenerateTransID(Phrase.UUID);
			} else if (validate == userDB.INVALID_PASSWORD) {
				LoggingUtils.Log(Phrase.InvalidCredential + ": "
						+ Phrase.InvalidCredentialMSG, Level.ERROR,
						Phrase.WebServicesLoggerName);
				throw new RemoteException(Phrase.InvalidCredential);
			} else if (validate == userDB.INACTIVE_USER
					|| validate == userDB.INVALID_PERMISSION) {
				LoggingUtils.Log(Phrase.AccessDenied + ": "
						+ Phrase.AccessDeniedMSG, Level.ERROR,
						Phrase.WebServicesLoggerName);
				throw new RemoteException(Phrase.AccessDenied);
			} else if (!validDomain) {
				LoggingUtils.Log("The domain is invalid " + domainName,
						Level.ERROR, Phrase.WebServicesLoggerName);
				LoggingUtils.Log(Phrase.UnknownUser + ": "
						+ Phrase.UnknownUserMSG, Level.ERROR,
						Phrase.WebServicesLoggerName);
				throw new RemoteException(Phrase.UnknownUser);
			} else {
				LoggingUtils.Log(Phrase.InvalidCredential + ": "
						+ Phrase.InvalidCredentialMSG, Level.ERROR,
						Phrase.WebServicesLoggerName);
				throw new RemoteException(Phrase.InvalidCredential);
			}
		} catch (RemoteException e) {
			throw e;
		} catch (Exception e) {
			LoggingUtils.Log("Could Not Locally Authenticate User " + userID
					+ ": " + e.toString(), Level.ERROR,
					Phrase.WebServicesLoggerName);
			throw new RemoteException(Phrase.InternalError, e);
		}
		return token;
	}
}
