package Node.NAAS.Requestor;

import com.enfotech.basecomponent.utility.security.WebProxy;
import java.math.BigInteger;
import java.net.URL;
import java.rmi.RemoteException;
import org.apache.axis.components.net.SocketFactoryFactory;
import org.apache.axis.AxisProperties;
import org.apache.axis.components.net.SecureSocketFactory;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.NAAS.Requestor.Auth.*;
import Node.NAAS.Requestor.Policy.*;
import Node.NAAS.Requestor.UserMgr.*;
import Node.NAAS.Types.Auth.AuthMethod;
import Node.NAAS.Types.Auth.PasswordType;
import Node.NAAS.Types.Policy.ActionType;
import Node.NAAS.Types.Policy.PolicyInfo;
import Node.NAAS.Types.Policy.MethodName;
import Node.NAAS.Types.Policy.NodeId;
import Node.NAAS.Types.UserMgr.StateId;
import Node.NAAS.Types.UserMgr.UserInfo;
import Node.NAAS.Types.UserMgr.UserType;
/**
 * <p>This class create NAASRequestor.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class NAASRequestor {
    private String url;

    // User Types
    public static final String USER_USER_TYPE = UserType._user;
    public static final String OPERATOR_USER_TYPE = UserType._operator;
    public static final String ADMIN_USER_TYPE = UserType._admin;

    // Method Names
    public static final String METHOD_ANY = MethodName._Any;
    public static final String METHOD_SUBMIT = MethodName._Submit;
    public static final String METHOD_DOWNLOAD = MethodName._Download;
    public static final String METHOD_AUTHENTICATE = MethodName._Authenticate;
    public static final String METHOD_QUERY = MethodName._Query;
    public static final String METHOD_GETSTATUS = MethodName._GetStatus;
    public static final String METHOD_NOTIFY = MethodName._Notify;
    public static final String METHOD_SOLICIT = MethodName._Solicit;
    public static final String METHOD_GETSERVICES = MethodName._GetServices;
    public static final String METHOD_EXECUTE = MethodName._Execute;

    // Action Types
    public static final String ACTION_DENY = ActionType._Deny;
    public static final String ACTION_PERMIT = ActionType._Permit;

    public static void main (String[] args)
    {
      NAASRequestor requestor = new NAASRequestor();
      String result = null;
      try {
        result = requestor.AddUser("ryan_teising@enfotech.com","enf0Tech","rjacoby@tceq.state.tx.us",NAASRequestor.USER_USER_TYPE,"Peoria25","Peoria25","ENFO");
      } catch (RemoteException e) {
        System.out.println("toString(): " + e.toString());
        System.out.println("getMessage(): " + e.getMessage());
        try {
          result = requestor.VerifyPolicy("jacoby@tceq.state.tx.us","ENFO",NAASRequestor.METHOD_ANY,null,null);
        } catch (RemoteException ex) {
          System.out.println("toString(): " + ex.toString());
          System.out.println("getMessage(): " + ex.getMessage());
        }
      }
      System.out.println(result);
    }

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
    protected NAASRequestor ()
    {
      this.Init();
    }

	  /**
	   * Init
	   * @param 
	   * @return 
	   */
    private void Init ()
    {
        try {
            SocketFactoryFactory.getFactory(null, null);
        }
        catch (Exception ex) {
            //ignore exception
        }
        AxisProperties.setClassDefault(SecureSocketFactory.class,
                                       "org.apache.axis.components.net.SunFakeTrustSocketFactory");

        try
        {
          //get url
          this.url = "https://4.21.155.125";
          //get proxy setting
          this.setProxy("192.168.88.8", "28318", "teisingr", "59Qvis4p");
        }
        catch(Exception e)
        {
            e.printStackTrace();
        }
    }

    /**
     * NodeRequestor constructor
     * @param nodeUrl
	 * @return 
     */
    public NAASRequestor(String loggerName) throws Exception {
        this.url = null;
        try {
            SocketFactoryFactory.getFactory(null, null);
        }
        catch (Exception ex) {
            //ignore exception
        }
        AxisProperties.setClassDefault(SecureSocketFactory.class,
                                       "org.apache.axis.components.net.SunFakeTrustSocketFactory");

        try
        {
          ISystemConfiguration config = DBManager.GetSystemConfiguration(loggerName);
          //get url
          this.url = config.GetNAASURL();
          if((this.url+"").equalsIgnoreCase("null") || (this.url+"").equalsIgnoreCase(""))
            throw new Exception ("CDX endpoint is not defined. Please check system.config file.");
          //get proxy setting
          if (config.GetProxyStatus()) {
            String sHost = config.GetProxyHost();
            String sPort = config.GetProxyPort();
            String sUser = config.GetProxyUID();
            String sPassword = config.GetProxyPWD();
            this.setProxy(sHost, sPort, sUser, sPassword);
          }
        }
        catch(Exception e)
        {
            throw e;
        }
    }

    /**
     * get node url
     * @return String address
     */
    public String getUrl() {
        return url;
    }

    /**
     * set node url address
     * @param url address
     */
    public void setUrl(String url) {
        this.url = url;
    }

    /**
     * set proxy server value
     * @param proxyAddress String proxy server address
     * @param proxyPort String proxy server port number
     * @param proxyUser String proxy server user name
     * @param proxyPassword String proxy server password
     */
    public void setProxy(String proxyAddress, String proxyPort,
                         String proxyUser,
                         String proxyPassword) throws Exception {
        boolean isSSL = false;
        try {
          if (proxyAddress != null && proxyAddress.startsWith("https"))
            isSSL = true;
          /*
            if (this.url != null &&
                this.url.getProtocol().equalsIgnoreCase("https")) {
                isSSL = true;
            }
           */
            WebProxy.SetProxy(proxyAddress, proxyPort, proxyUser, proxyPassword,
                              isSSL);
        }
        catch (Exception e) {
            throw e;
        }
    }

    /**
     * get networkNodeBindingStub
     * @return A networkNodeBindingStub
     * @throws RemoteException
     */
    private NetworkSecurityBindingStub getStub() throws RemoteException {
      NetworkSecurityBindingStub stub = null;
      try {
        CdxSecurityLocator locator = new CdxSecurityLocator();
        stub = new NetworkSecurityBindingStub(new URL(this.url + "/xml/auth.wsdl"), locator);
      } catch (RemoteException e) {
        throw e;
      } catch (Exception e) {

      }
      return stub;

    }

    /**
     * getUserMgrStub
     * @param 
	 * @return UserMgrBindingStub
     */
    private UserMgrBindingStub getUserMgrStub () throws RemoteException {
      UserMgrBindingStub stub = null;
      try {
        UsermgrLocator locator = new UsermgrLocator();
        stub = new UserMgrBindingStub(new URL(this.url+"/xml/usermgr.wsdl"),locator);
      } catch (RemoteException e) {
        throw e;
      } catch (Exception e) {
      }
      return stub;
    }

    /**
     * getPolicyStub
     * @param 
	 * @return NetworkPolicyBindingStub
     */
    private NetworkPolicyBindingStub getPolicyStub () throws RemoteException
    {
      NetworkPolicyBindingStub stub = null;
      try {
        AuthorizationPolicyLocator locator = new AuthorizationPolicyLocator();
        stub = new NetworkPolicyBindingStub(new URL(this.url+"/xml/policy.wsdl"),locator);
      } catch (RemoteException e) {
        throw e;
      } catch (Exception e) {
      }
      return stub;
    }

    /**
     * Authenticate method
     * @param userId String
     * @param credential PasswordType
     * @param authenticationMethod AuthMethod
     * @throws RemoteException
     * @return String
     */
    public String authenticate(String userId, PasswordType credential,
                               AuthMethod authenticationMethod) throws
      RemoteException {
        String response = null;
//calling private method to get the port
        NetworkSecurityBindingStub port = this.getStub();

        response = port.authenticate(userId, credential, authenticationMethod);
        return response;
    }

    /**
     * centralAuth method
     * @param uid String
     * @param cred String
     * @param authMethod AuthMethod
     * @param clientHost String
     * @throws RemoteException
     * @return String
     */
    public String centralAuth(String uid, String cred, AuthMethod authMethod,
                              String clientHost) throws RemoteException {
        String response = null;
//calling private method to get the port
        NetworkSecurityBindingStub port = this.getStub();

        response = port.centralAuth(uid, cred, authMethod, clientHost);
        return response;
    }

    /**
     * validate method
     * @param authToken String
     * @param clientHost String
     * @param resourceUri String
     * @throws RemoteException
     * @return String
     */
    public String validate(String authToken, String clientHost,
                           String resourceUri) throws RemoteException {
        String response = null;
//calling private method to get the port
        NetworkSecurityBindingStub port = this.getStub();
        response = port.validate(authToken, clientHost, resourceUri);
        return response;
    }

    /**
     * Add a NAAS User
     * @param adminName String Administrator UID
     * @param adminPWD String Administrator PWD
     * @param userEmail String User Subject
     * @param userType String Type of User (from static constants)
     * @param userPWD String User PWD
     * @param confirmUserPWD String Confirmed User PWD
     * @param affiliate String Node Affiliate (State Node)
     * @throws RemoteException on Error
     * @return String on Success
     */
    public String AddUser (String adminName, String adminPWD, String userEmail, String userType, String userPWD, String confirmUserPWD, String affiliate) throws RemoteException
    {
      UserMgrBindingStub stub = this.getUserMgrStub();
      Node.NAAS.Types.UserMgr.PasswordType admPWD = new Node.NAAS.Types.UserMgr.PasswordType(adminPWD);
      Node.NAAS.Types.UserMgr.PasswordType pwd = new Node.NAAS.Types.UserMgr.PasswordType(userPWD);
      Node.NAAS.Types.UserMgr.PasswordType confPWD = new Node.NAAS.Types.UserMgr.PasswordType(confirmUserPWD);
      return stub.addUser(adminName, admPWD, userEmail, UserType.fromValue(userType), pwd, confPWD, StateId.fromValue(affiliate));
    }

    /**
     * Change a User's PWD
     * @param userEmail String User ID
     * @param oldPWD String Old Password
     * @param newPWD String New Password
     * @throws RemoteException on Error
     * @return String on Success
     */
    public String ChangePWD (String userEmail, String oldPWD, String newPWD) throws RemoteException
    {
      UserMgrBindingStub stub = this.getUserMgrStub();
      Node.NAAS.Types.UserMgr.PasswordType old = new Node.NAAS.Types.UserMgr.PasswordType(oldPWD);
      Node.NAAS.Types.UserMgr.PasswordType newP = new Node.NAAS.Types.UserMgr.PasswordType(newPWD);
      return stub.changePwd(userEmail, old, newP);
    }

    /**
     * Update a Users Information (leave field null if not want to change)
     * @param adminID String
     * @param adminPWD String
     * @param subject String
     * @param userPWD String
     * @param owner String
     * @param state String
     * @throws RemoteException
     * @return String
     */
    public String UpdateUser (String adminID, String adminPWD, String subject, String userPWD, String owner, String state) throws RemoteException
    {
      UserMgrBindingStub stub = this.getUserMgrStub();
      Node.NAAS.Types.UserMgr.PasswordType admPWD = new Node.NAAS.Types.UserMgr.PasswordType(adminPWD);
      Node.NAAS.Types.UserMgr.PasswordType pwd = new Node.NAAS.Types.UserMgr.PasswordType(userPWD);
      return stub.updateUser(adminID,admPWD,subject,UserType.user,pwd,owner,StateId.fromValue(state));
    }

    /**
     * Delete a Users Information (leave field null if not want to change)
     * @param adminID String
     * @param adminPWD String
     * @param userEmail String
     * @throws RemoteException
     * @return String
     */
    public String DeleteUser (String adminID, String adminPWD, String userEmail) throws RemoteException
    {
      UserMgrBindingStub stub = this.getUserMgrStub();
      Node.NAAS.Types.UserMgr.PasswordType admPWD = new Node.NAAS.Types.UserMgr.PasswordType(adminPWD);
      return stub.deleteUser(adminID,admPWD,userEmail);
    }

    /**
     * Set a Policy
     * @param adminID String Admin UID
     * @param adminPWD String Admin PWD
     * @param subject String User
     * @param method String Method Name (from static String Constants)
     * @param request String Request Name
     * @param params String[] Parameters
     * @param action String Action Type (NAASRequest.ACTION_DENY, NAASRequestor.ACTION_PERMIT)
     * @throws RemoteException on Error
     * @return String on Success
     */
    public String SetPolicy (String adminID, String adminPWD, String subject, String method, String request, String[] params, String action) throws RemoteException
    {
      NetworkPolicyBindingStub stub = this.getPolicyStub();
      Node.NAAS.Types.Policy.PasswordType admin = new Node.NAAS.Types.Policy.PasswordType(adminPWD);
      String paramList = null;
      if (params != null && params.length > 0) {
        paramList = "";
        for (int i = 0; i < params.length; i++) {
          if (i != 0)
            paramList += ";";
          paramList += params[i];
        }
      }
      return stub.setPolicy(adminID,admin,subject,MethodName.fromValue(method),request,paramList,ActionType.fromValue(action));
    }

    /**
     * DeletePolicy
     * @param adminID
     * @param adminPWD
     * @param subject
     * @param method
     * @param request
     * @param params
	 * @return String
     */
    public String DeletePolicy (String adminID, String adminPWD, String subject, String method, String request, String[] params) throws Exception
    {
      NetworkPolicyBindingStub stub = this.getPolicyStub();
      Node.NAAS.Types.Policy.PasswordType admin = new Node.NAAS.Types.Policy.PasswordType(adminPWD);
      String paramList = null;
      if (params != null && params.length > 0) {
        paramList = "";
        for (int i = 0; i < params.length; i++) {
          if (i != 0)
            paramList += ";";
          paramList += params[i];
        }
      }
      return stub.deletePolicy(adminID,admin,subject,MethodName.fromValue(method),request,paramList);
    }

    /**
     * Get List of Policies
     * @param adminID String Administrator UID
     * @param adminPWD String Administrator PWD
     * @param subject String Query User (emtpy for entire list)
     * @throws RemoteException on Error
     * @return PolicyInfo[] Array of Policies Assigned on Success
     */
    public PolicyInfo[] GetPolicyList (String adminID, String adminPWD, String subject) throws RemoteException
    {
      NetworkPolicyBindingStub stub = this.getPolicyStub();
      Node.NAAS.Types.Policy.PasswordType admin = new Node.NAAS.Types.Policy.PasswordType(adminPWD);
      return stub.getPolicyList(adminID,admin,subject,BigInteger.ZERO,new BigInteger(""+Integer.MAX_VALUE));
    }

    /**
     * VerifyPolicy
     * @param subject
     * @param nodeName
     * @param method
     * @param request
     * @param params
	 * @return String
     */
    public String VerifyPolicy (String subject, String nodeName, String method, String request, String[] params) throws RemoteException
    {
      NetworkPolicyBindingStub stub = this.getPolicyStub();
      String paramList = null;
      if (params != null && params.length > 0) {
        paramList = "";
        for (int i = 0; i < params.length; i++) {
          if (i != 0)
            paramList += ";";
          paramList += params[i];
        }
      }
      return stub.verifyPolicy(subject,NodeId.fromValue(nodeName),MethodName.fromValue(method),request,paramList);
    }

    /**
     * GetUserList
     * @param adminID
     * @param adminPWD
     * @param userID
     * @param affiliation
     * @param rowID
     * @param maxRows
	 * @return UserInfo[]
     */
    public UserInfo[] GetUserList (String adminID, String adminPWD, String userID, String affiliation, int rowID, int maxRows) throws Exception
    {
      UserMgrBindingStub stub = this.getUserMgrStub();
      Node.NAAS.Types.UserMgr.PasswordType admin = new Node.NAAS.Types.UserMgr.PasswordType(adminPWD);
      return stub.getUserList(adminID,admin,userID,affiliation!=null?StateId.fromValue(affiliation):null,new BigInteger(rowID+""),new BigInteger(maxRows+""));
    }

    /**
     * GetMethodName
     * @param method
	 * @return String
     */
    public static String GetMethodName (String method)
    {
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_ANY))
        return NAASRequestor.METHOD_ANY;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_SUBMIT))
        return NAASRequestor.METHOD_SUBMIT;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_DOWNLOAD))
        return NAASRequestor.METHOD_DOWNLOAD;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_AUTHENTICATE))
        return NAASRequestor.METHOD_AUTHENTICATE;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_QUERY))
        return NAASRequestor.METHOD_QUERY;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_GETSTATUS))
        return NAASRequestor.METHOD_GETSTATUS;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_NOTIFY))
        return NAASRequestor.METHOD_NOTIFY;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_SOLICIT))
        return NAASRequestor.METHOD_SOLICIT;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_GETSERVICES))
        return NAASRequestor.METHOD_GETSERVICES;
      if (method.equalsIgnoreCase(NAASRequestor.METHOD_EXECUTE))
        return NAASRequestor.METHOD_EXECUTE;
      return null;
    }
}
