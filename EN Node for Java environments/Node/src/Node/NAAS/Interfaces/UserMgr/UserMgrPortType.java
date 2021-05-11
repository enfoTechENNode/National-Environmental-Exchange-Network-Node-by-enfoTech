/**
 * UserMgrPortType.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Interfaces.UserMgr;

public interface UserMgrPortType extends java.rmi.Remote {

    // Add a new user.The system uses email address as the user id. All parameters
    // are required.Password should contain a mix of lower case, upper case
    // and numeric characters
	/**
	 * addUser.
	 * @param adminName.
	 * @param adminPwd.
	 * @param userEmail.
	 * @param userType.
	 * @param userPwd.
	 * @param confirmUserPwd.
	 * @param affiliate.
	 * @return String
	 */
    public java.lang.String addUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.UserType userType, Node.NAAS.Types.UserMgr.PasswordType userPwd, Node.NAAS.Types.UserMgr.PasswordType confirmUserPwd, Node.NAAS.Types.UserMgr.StateId affiliate) throws java.rmi.RemoteException;

    // Remove a user, you must be an administrator and the owner of the account.
	/**
	 * deleteUser.
	 * @param adminName.
	 * @param adminPwd.
	 * @param userEmail.
	 * @return String
	 */
    public java.lang.String deleteUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail) throws java.rmi.RemoteException;

    // Update a user account information. The Password should contain a mix
    // of lower case, upper case and numeric characters
	/**
	 * updateUser.
	 * @param adminName.
	 * @param adminPwd.
	 * @param userEmail.
	 * @param userType.
	 * @param userPwd.
	 * @param owner.
	 * @param affiliate.
	 * @return String
	 */
    public java.lang.String updateUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.UserType userType, Node.NAAS.Types.UserMgr.PasswordType userPwd, java.lang.String owner, Node.NAAS.Types.UserMgr.StateId affiliate) throws java.rmi.RemoteException;

    // Get a list of users
	/**
	 * getUserList.
	 * @param adminName.
	 * @param adminPwd.
	 * @param userEmail.
	 * @param userState.
	 * @param rowId.
	 * @param maxRows.
	 * @return UserInfo[]
	 */
    public Node.NAAS.Types.UserMgr.UserInfo[] getUserList(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.StateId userState, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException;

    // The new password should contain a mix of lower case, upper case and
    // numeric characters. The minimum password length is 8
	/**
	 * changePwd.
	 * @param userEmail.
	 * @param password.
	 * @param newPwd.
	 * @return String
	 */
    public java.lang.String changePwd(java.lang.String userEmail, Node.NAAS.Types.UserMgr.PasswordType password, Node.NAAS.Types.UserMgr.PasswordType newPwd) throws java.rmi.RemoteException;
}
