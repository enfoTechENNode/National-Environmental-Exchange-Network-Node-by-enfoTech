package Node.Biz.Administration;

import java.rmi.RemoteException;
import org.apache.log4j.Level;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.INodeOperation;
import Node.NAAS.Requestor.NAASRequestor;
import Node.NAAS.Types.Policy.PolicyInfo;
import Node.NAAS.Types.UserMgr.UserInfo;
import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create NAASIntegration for 1.1 webservice class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NAASIntegration {
  private String LoggerName = null;

  public NAASIntegration(String loggerName) {
    this.LoggerName = loggerName;
  }

  /**
   * Add NAAS User.
   * @param userID
   * @param pwd
   * @return success
   */
  public boolean AddNAASUser (String userID, String pwd)
  {
    boolean retBool = false;
    String nodeName = null;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      nodeName = configDB.GetNodeName();
      if (adminUID == null || adminPWD == null || nodeName == null)
        throw new RemoteException(Phrase.AccessRight);
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      requestor.AddUser(adminUID,adminPWD,userID,NAASRequestor.USER_USER_TYPE,pwd,pwd,nodeName);
      retBool = true;
    }catch (Exception e) {
      LoggingUtils.Log("Could not Add NAAS User: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return retBool;
  }

  /**
   * Get Policy List.
   * @param subject
   * @return PolicyInfo array
   */
  public PolicyInfo[] GetPolicyList (String subject) throws RemoteException
  {
    PolicyInfo[] retArray = null;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      if (adminUID == null || adminPWD == null)
        throw new RemoteException(Phrase.AccessRight);
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      retArray = requestor.GetPolicyList(adminUID,adminPWD,subject);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Get Policy List: " + e.toString(),Level.ERROR,this.LoggerName);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retArray;
  }

  /**
   * Set Policy.
   * @param subject
   * @param webMethodName
   * @param opName
   * @param toPermit
   * @return success
   */
  public boolean SetPolicy (String subject, String webMethodName, String opName, boolean toPermit)
  {
    boolean retBool = false;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      if (adminUID == null || adminPWD == null)
        throw new RemoteException(Phrase.AccessRight);
      if (webMethodName != null) {
        NAASRequestor requestor = new NAASRequestor(this.LoggerName);
        if (toPermit)
          requestor.SetPolicy(adminUID,adminPWD,subject,this.ConvertWSNames(webMethodName),opName,null,NAASRequestor.ACTION_PERMIT);
        else
          requestor.DeletePolicy(adminUID,adminPWD,subject,this.ConvertWSNames(webMethodName),opName,null);
        retBool = true;
      }
    } catch (Exception e) {
      LoggingUtils.Log("Could not Set Policy: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return retBool;
  }

  // WI 22233
  /**
   * Set All Policy.
   * @param webMethodName
   * @param opName
   * @param toPermit
   * @return success
   */
	public boolean setAllPolicy(String webMethodName, String opName,
			String toPermit) {
		boolean retBool = false;
		String ret = null;
		try {
			ISystemConfiguration configDB = DBManager
					.GetSystemConfiguration(this.LoggerName);
			String adminUID = configDB.GetNodeAdminUID();
			String adminPWD = configDB.GetNodeAdminPWD();
			if (adminUID == null || adminPWD == null)
				throw new RemoteException(Phrase.AccessRight);
			if (webMethodName != null) {
				NAASRequestor requestor = new NAASRequestor(this.LoggerName);
				if (toPermit.equalsIgnoreCase(NAASRequestor.ACTION_PERMIT)){
					ret = requestor.SetPolicy(adminUID, adminPWD, "any", this
							.ConvertWSNames(webMethodName), opName, null,
							NAASRequestor.ACTION_PERMIT);
				}else if (toPermit.equalsIgnoreCase(NAASRequestor.ACTION_DENY)){
					ret = requestor.SetPolicy(adminUID, adminPWD, "any", this
							.ConvertWSNames(webMethodName), opName, null,
							NAASRequestor.ACTION_DENY);
				}else if (toPermit.equalsIgnoreCase("")){
					ret = requestor.DeletePolicy(adminUID, adminPWD, "any", this
							.ConvertWSNames(webMethodName), opName, null);
				}
				retBool = true;
			}
		} catch (Exception e) {
			LoggingUtils.Log("Could not Set All Policy: " + e.toString(),
					Level.ERROR, this.LoggerName);
		}
		return retBool;
	}

  // WI 22233
  /**
   * Verify Policy.
   * @param webMethodName
   * @param opName
   * @param toPermit
   * @return success
   */
	public String verifyPolicy(String subject,String webMethodName, String opName) {
		PolicyInfo[] retArray = null;
		String ret = "";
		try {
			ISystemConfiguration configDB = DBManager
					.GetSystemConfiguration(this.LoggerName);
			String adminUID = configDB.GetNodeAdminUID();
			String adminPWD = configDB.GetNodeAdminPWD();
		    
			if (adminUID == null || adminPWD == null)
				throw new RemoteException(Phrase.AccessRight);
			if (webMethodName != null) {
				NAASRequestor requestor = new NAASRequestor(this.LoggerName);
			    retArray = requestor.GetPolicyList(adminUID,adminPWD,subject);
		        if (retArray != null && retArray.length > 0) {
		        	for (int i = 0; i < retArray.length; i++) {
		        		if(retArray[i].getRequest().equalsIgnoreCase(opName) && retArray[i].getSubject().equalsIgnoreCase("any") && retArray[i].getParams().equalsIgnoreCase("any")){
		        			ret = retArray[i].getAction();
		        			break;
		        		}
		        	}
		        }
			}
		} catch (Exception e) {
			LoggingUtils.Log("Could not verify Policy: " + e.toString(),
					Level.ERROR, this.LoggerName);
		}
		return ret;
	}
	
  /**
   * Set Policy.
   * @param subject
   * @param opName
   * @param toPermit
   * @return success
   */
  public boolean SetPolicy (String subject, String opName, boolean toPermit)
  {
    boolean retBool = false;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      if (adminUID == null || adminPWD == null)
        throw new RemoteException(Phrase.AccessRight);
      INodeOperation opDB = DBManager.GetNodeOperation(this.LoggerName);
      String wsName = opDB.GetWebService(opName);
      if (wsName != null) {
        NAASRequestor requestor = new NAASRequestor(this.LoggerName);
        if (toPermit)
          requestor.SetPolicy(adminUID,adminPWD,subject,this.ConvertWSNames(wsName),opName,null,NAASRequestor.ACTION_PERMIT);
        else
          requestor.DeletePolicy(adminUID,adminPWD,subject,this.ConvertWSNames(wsName),opName,null);
        retBool = true;
      }
    } catch (Exception e) {
      LoggingUtils.Log("Could not Set Policy: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return retBool;
  }

  /**
   * Get User List.
   * @return UserInfo array
   */
  public UserInfo[] GetUserList ()
  {
    UserInfo[] retArray = null;
    try {
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      if (adminUID == null || adminPWD == null)
        throw new RemoteException(Phrase.AccessRight);
      retArray = requestor.GetUserList(adminUID,adminPWD,null,null,0,Integer.MAX_VALUE);
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Get User List: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return retArray;
  }

  /**
   * Get User List.
   * @param subject
   * @return UserInfo array
   */
  public UserInfo[] GetUserList (String subject) throws RemoteException
  {
    UserInfo[] retArray = null;
    try {
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      if (adminUID == null || adminPWD == null)
        throw new RemoteException(Phrase.AccessRight);
      retArray = requestor.GetUserList(adminUID,adminPWD,subject,null,0,Integer.MAX_VALUE);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Get User List: " + e.toString(),Level.ERROR,this.LoggerName);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retArray;
  }

  /**
   * Get Users Owned.
   * @return UserInfo array
   */
  public UserInfo[] GetUsersOwned () throws RemoteException
  {
    UserInfo[] retArray = null;
    try {
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      String nodeName = configDB.GetNodeName();
      if (adminUID == null || adminPWD == null || nodeName == null)
        throw new RemoteException(Phrase.AccessRight);
      retArray = requestor.GetUserList(adminUID,adminPWD,null,nodeName,0,Integer.MAX_VALUE);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      LoggingUtils.Log("Could Not Get User List: " + e.toString(),Level.ERROR,this.LoggerName);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retArray;
  }

  /**
   * Change Password.
   * @param email
   * @param newPWD
   * @return success
   */
  public boolean ChangePassword (String email, String newPWD)
  {
    boolean retBool = false;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(this.LoggerName);
      String adminUID = configDB.GetNodeAdminUID();
      String adminPWD = configDB.GetNodeAdminPWD();
      String nodeName = configDB.GetNodeName();
      if (adminUID == null || adminPWD == null || nodeName == null)
        throw new RemoteException(Phrase.AccessRight);
      NAASRequestor requestor = new NAASRequestor(this.LoggerName);
      requestor.UpdateUser(adminUID,adminPWD,email,newPWD,null,nodeName);
      retBool = true;
    } catch (Exception e) {
      LoggingUtils.Log("Could not Change Password: " + e.toString(),Level.ERROR,this.LoggerName);
    }
    return retBool;
  }

  /**
   * Convert WSNames.
   * @param input
   * @return webservice Name
   */
  private String ConvertWSNames (String input)
  {
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_ANY))
      return NAASRequestor.METHOD_ANY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_AUTHENTICATE))
      return NAASRequestor.METHOD_AUTHENTICATE;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_DOWNLOAD))
      return NAASRequestor.METHOD_DOWNLOAD;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_GETSERVICES))
      return NAASRequestor.METHOD_GETSERVICES;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_GETSTATUS))
      return NAASRequestor.METHOD_GETSTATUS;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_NOTIFY))
      return NAASRequestor.METHOD_NOTIFY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_QUERY))
      return NAASRequestor.METHOD_QUERY;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_SOLICIT))
      return NAASRequestor.METHOD_SOLICIT;
    if (input.equalsIgnoreCase(NAASRequestor.METHOD_SUBMIT))
      return NAASRequestor.METHOD_SUBMIT;
    return null;
  }
}
