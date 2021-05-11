package Node.DB.Interfaces;

import Node.Biz.Administration.User;
/**
 * <p>This class create INodeUser interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeUser {
  // Interface Constants
  public static int SUCCESSFUL = 0;
  public static int CHANGE_PWD = 2;
  public static int USER_DOES_NOT_EXIST = -1;
  public static int DATABASE_UNAVAILABLE = -2;
  public static int INACTIVE_USER = -3;
  public static int INVALID_PERMISSION = -4;
  public static int INVALID_PASSWORD = -5;

  /********************** To be implemented **********************************/

  // Validate User Login and Password
  /**
   * ValidateLogin
   * @param user
   * @param pwd
   * @return int
   */
  public int ValidateLogin(String user, String pwd);

  // Authethenticate User
  /**
   * AuthenticateLogin
   * @param user
   * @param pwd
   * @return int
   */
  public int AuthenticateLogin(String user, String pwd);

  // Authethenticate User
  /**
   * AuthenticateAnyLogin
   * @param user
   * @param pwd
   * @return int
   */
  public int AuthenticateAnyLogin(String user, String pwd);

  /**
   * Get USER_ID when provided LOGIN_NAME
   * @param userName String
   * @return int USER_ID, -1 if not found
   */
  public int GetUserID(String loginName);

  /**
   * Get LOGIN_NAME when provided USER_ID
   * @param userID int USER_ID
   * @return String LOGIN_NAME, null if not found
   */
  public String GetUserLogin(int userID);

  // Get User
  public User GetUser(int userID);

  /**
   * Get User Object
   * @param loginName String Login Name of User
   * @return User User Object, null if not found
   */
  public User GetUser(String loginName);

  // Search User
  /**
   * SearchUser
   * @param loginName
   * @param userType
   * @param assocDomain
   * @param firstName
   * @param lastName
   * @return User[]
   */
  public User[] SearchUser(String loginName, String userType, String assocDomain, String firstName, String lastName);

  /**
   * Save a User
   * @param userID int userID if user exists, -1 otherwise
   * @param changePassword boolean true if need to change password, false otherwise (will be sent if userID == -1)
   * @param status String User Status
   * @param firstName String User First Name
   * @param midInit String User Middle Initial
   * @param lastName String User Last Name
   * @param email String User Email
   * @param phone String User Phone
   * @param last4SSN String User Last 4 Digits of Social Security Number
   * @param comments String User Comments
   * @param address String User Address
   * @param suppAddress String User Supplemental Address
   * @param city String User City
   * @param state String User State
   * @param zipCode String User Zip Code
   * @param country String User Country
   * @param accountTypes String[] User Admin Domains
   * @param operations String[] User Operations
   * @return int User ID if successful, -1 otherwise
   */
  public int SaveUser(int userID, String status, String firstName,
                      String midInit, String lastName,
                      String email, String phone, String last4SSN,
                      String comments, String address, String suppAddress,
                      String city, String state, String zipCode, String country,
                      String accType, String[] domains,
                      String[] opNames, String[] wsNames, String[] adminDomains);

  /**
   * SaveNewUser
   * @param loginName
   * @param status
   * @param firstName
   * @param firstName
   * @param midInit
   * @param lastName
   * @param email
   * @param phone
   * @param last4SSN
   * @param comments
   * @param address
   * @param suppAddress
   * @param city
   * @param state
   * @param zipCode
   * @param country
   * @param accType
   * @param domains
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return User[]
   */
  public int SaveNewUser(String loginName, String status, String firstName,
                         String midInit, String lastName,
                         String email, String phone, String last4SSN,
                         String comments, String address, String suppAddress,
                         String city, String state, String zipCode,
                         String country, String accType, String[] domains,
                         String[] opNames, String[] wsNames, String[] adminDomains);


  /**
   * SavePassword
   * @param loginName
   * @param password
   * @return int
   */
  public int SavePassword(String loginName, String password);

  /**
   * SaveOperations
   * @param userID
   * @param accType
   * @param opNames
   * @param wsNames
   * @param adminDomains
   * @return int
   */
  public boolean SaveOperations(int userID, String accType, String[] opNames, String[] wsNames, String[] adminDomains);

  /**
   * SaveOperations
   * @param userID
   * @param accType
   * @param adminDomains
   * @return int
   */
  public boolean SaveDomainAdmins(int userID, String accType, String[] adminDomains);

  /**
   * ChangePassword
   * @param loginName
   * @return int
   */
  public String ChangePassword(String loginName);

  /**
   * Change Password
   * @param loginName String Login Name
   * @param password String Unencrypted Password
   * @return String
   */
  public String ChangePassword(String loginName, String password);

  /**
   * Delete User
   * @param userID int
   * @return boolean true if successful, false otherwise
   */
  public boolean DeleteUser(int userID);
  
  /**
   * Delete User Layout
   * @param userID int
   * @return boolean true if successful, false otherwise
   */
  public boolean DeleteUserLayout(int userID);

  /**
   * Send User Email
   * @param receiverEmail String Email Address
   * @param firstName String
   * @param lastName String
   * @param loginName String
   * @param password String
   * @return boolean
   */
  public boolean SendEmail(String receiverEmail, String firstName, String lastName, String loginName, String password);

  /**
   * Get List of Console Users
   * @return User[]
   */
  public User[] GetConsoleUsers ();

  /**
   * Get List of Specified Console Users
   * @param loginNames String[]
   * @return User[]
   */
  public User[] GetConsoleUsers (String[] loginNames);
  
  // WI 33893

  /**
   * verifyEmail
   * @param email
   * @return int user ID
   */
  public int verifyEmail (String email);
}
