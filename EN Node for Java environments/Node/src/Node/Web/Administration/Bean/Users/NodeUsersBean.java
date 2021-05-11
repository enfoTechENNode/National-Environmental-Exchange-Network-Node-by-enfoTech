package Node.Web.Administration.Bean.Users;

import java.util.ArrayList;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create NodeUsersBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */


public class NodeUsersBean extends BaseBean {
  private String userID = "";
  private String userIDError = "";
  private String status = "";
  private String firstName = "";
  private String firstNameError = "";
  private String midInitial = "";
  private String lastName = "";
  private String lastNameError = "";
  private String email = "";
  private String emailError = "";
  private String phone = "";
  private String address = "";
  private String suppAddress = "";
  private String city = "";
  private String state = "";
  private String zipCode = "";
  private String country = "";
  private String comments = "";
  private String userType = "";
  private String userTypeError = "";
  private String[] domains = null;
  private String domain = "";
  private ArrayList ops = null;
  private String[] operationsAvailable = null;
  private String[] operationsAssigned = null;
  private String opAvailSel = "";
  private String opAssSel = "";
  private String message = "";
  private String title = "";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public NodeUsersBean() {
  }

  /**
   * setUserID
   * @param input
   * @return 
   */
  public void setUserID (String input)
  {
    this.userID = input;
  }
  /**
   * getUserID
   * @param 
   * @return String
   */
  public String getUserID ()
  {
    return this.userID;
  }

  /**
   * setUserIDError
   * @param input
   * @return 
   */
  public void setUserIDError (String input)
  {
    this.userIDError = input;
  }
  /**
   * getUserIDError
   * @param 
   * @return String
   */
  public String getUserIDError ()
  {
    return this.userIDError;
  }

  /**
   * setStatus
   * @param input
   * @return 
   */
  public void setStatus (String input)
  {
    this.status = input;
  }
  /**
   * getStatus
   * @param 
   * @return String
   */
  public String getStatus ()
  {
    return this.status;
  }

  /**
   * setFirstName
   * @param input
   * @return 
   */
  public void setFirstName (String input)
  {
    this.firstName = input;
  }
  /**
   * getFirstName
   * @param 
   * @return String
   */
  public String getFirstName ()
  {
    return this.firstName;
  }

  /**
   * setFirstNameError
   * @param input
   * @return 
   */
  public void setFirstNameError (String input)
  {
    this.firstNameError = input;
  }
  /**
   * getFirstNameError
   * @param 
   * @return String
   */
  public String getFirstNameError ()
  {
    return this.firstNameError;
  }

  /**
   * setMidInitial
   * @param input
   * @return 
   */
  public void setMidInitial (String input)
  {
    this.midInitial = input;
  }
  /**
   * getMidInitial
   * @param 
   * @return String
   */
  public String getMidInitial ()
  {
    return this.midInitial;
  }

  /**
   * setLastName
   * @param input
   * @return 
   */
  public void setLastName (String input)
  {
    this.lastName = input;
  }
  /**
   * getLastName
   * @param 
   * @return String
   */
  public String getLastName ()
  {
    return this.lastName;
  }

  /**
   * setLastNameError
   * @param input
   * @return 
   */
  public void setLastNameError (String input)
  {
    this.lastNameError = input;
  }
  /**
   * getLastNameError
   * @param 
   * @return String
   */
  public String getLastNameError ()
  {
    return this.lastNameError;
  }

  /**
   * setEmail
   * @param input
   * @return 
   */
  public void setEmail (String input)
  {
    this.email = input;
  }
  /**
   * getEmail
   * @param 
   * @return String
   */
  public String getEmail ()
  {
    return this.email;
  }

  /**
   * setEmailError
   * @param input
   * @return 
   */
  public void setEmailError (String input)
  {
    this.emailError = input;
  }
  /**
   * getEmailError
   * @param 
   * @return String
   */
  public String getEmailError ()
  {
    return this.emailError;
  }

  /**
   * setPhone
   * @param input
   * @return 
   */
  public void setPhone (String input)
  {
    this.phone = input;
  }
  /**
   * getPhone
   * @param 
   * @return String
   */
  public String getPhone ()
  {
    return this.phone;
  }

  /**
   * setAddress
   * @param input
   * @return 
   */
  public void setAddress (String input)
  {
    this.address = input;
  }
  /**
   * getAddress
   * @param 
   * @return String
   */
  public String getAddress ()
  {
    return this.address;
  }

  /**
   * setSuppAddress
   * @param input
   * @return 
   */
  public void setSuppAddress (String input)
  {
    this.suppAddress = input;
  }
  /**
   * getSuppAddress
   * @param 
   * @return String
   */
  public String getSuppAddress ()
  {
    return this.suppAddress;
  }

  /**
   * setCity
   * @param input
   * @return 
   */
  public void setCity (String input)
  {
    this.city = input;
  }
  /**
   * getCity
   * @param 
   * @return String
   */
  public String getCity ()
  {
    return this.city;
  }

  /**
   * setState
   * @param input
   * @return 
   */
  public void setState (String input)
  {
    this.state = input;
  }
  /**
   * getState
   * @param 
   * @return String
   */
  public String getState ()
  {
    return this.state;
  }

  /**
   * setZipCode
   * @param input
   * @return 
   */
  public void setZipCode (String input)
  {
    this.zipCode = input;
  }
  /**
   * getZipCode
   * @param 
   * @return String
   */
  public String getZipCode ()
  {
    return this.zipCode;
  }

  /**
   * setCountry
   * @param input
   * @return 
   */
  public void setCountry (String input)
  {
    this.country = input;
  }
  /**
   * getCountry
   * @param 
   * @return String
   */
  public String getCountry ()
  {
    return this.country;
  }

  /**
   * setComments
   * @param input
   * @return 
   */
  public void setComments (String input)
  {
    this.comments = input;
  }
  /**
   * getComments
   * @param 
   * @return String
   */
  public String getComments ()
  {
    return this.comments;
  }

  /**
   * setDomains
   * @param input
   * @return 
   */
  public void setDomains (String[] input)
  {
    this.domains = input;
  }
  /**
   * getDomains
   * @param 
   * @return String[]
   */
  public String[] getDomains ()
  {
    return this.domains;
  }

  /**
   * setDomain
   * @param input
   * @return 
   */
  public void setDomain (String input)
  {
    this.domain = input;
  }
  /**
   * getDomain
   * @param 
   * @return String
   */
  public String getDomain ()
  {
    return this.domain;
  }

  /**
   * setOps
   * @param input
   * @return 
   */
  public void setOps (ArrayList input)
  {
    this.ops = input;
  }
  /**
   * getOps
   * @param 
   * @return ArrayList
   */
  public ArrayList getOps ()
  {
    return this.ops;
  }

  /**
   * setOperationsAvailable
   * @param input
   * @return 
   */
  public void setOperationsAvailable (String[] input)
  {
    this.operationsAvailable = input;
  }
  /**
   * getOperationsAvailable
   * @param 
   * @return String[]
   */
  public String[] getOperationsAvailable ()
  {
    return this.operationsAvailable;
  }

  /**
   * setOpAvailSel
   * @param input
   * @return 
   */
  public void setOpAvailSel (String input)
  {
    this.opAvailSel = input;
  }
  /**
   * getOpAvailSel
   * @param 
   * @return String
   */
  public String getOpAvailSel ()
  {
    return this.opAvailSel;
  }

  /**
   * setOperationsAssigned
   * @param input
   * @return 
   */
  public void setOperationsAssigned (String[] input)
  {
    this.operationsAssigned = input;
  }
  /**
   * getOperationsAssigned
   * @param 
   * @return String[]
   */
  public String[] getOperationsAssigned ()
  {
    return this.operationsAssigned;
  }

  /**
   * setOpAssSel
   * @param input
   * @return 
   */
  public void setOpAssSel (String input)
  {
    this.opAssSel = input;
  }
  /**
   * getOpAssSel
   * @param 
   * @return String
   */
  public String getOpAssSel ()
  {
    return this.opAssSel;
  }

  /**
   * setUserType
   * @param input
   * @return 
   */
  public void setUserType (String input)
  {
    this.userType = input;
  }
  /**
   * getUserType
   * @param 
   * @return String
   */
  public String getUserType ()
  {
    return this.userType;
  }

  /**
   * setUserTypeError
   * @param input
   * @return 
   */
  public void setUserTypeError (String input)
  {
    this.userTypeError = input;
  }
  /**
   * getUserTypeError
   * @param 
   * @return String
   */
  public String getUserTypeError ()
  {
    return this.userTypeError;
  }

  /**
   * setMessage
   * @param input
   * @return 
   */
  public void setMessage (String input)
  {
    this.message = input;
  }
  /**
   * getMessage
   * @param 
   * @return String
   */
  public String getMessage ()
  {
    return this.message;
  }

  /**
   * setTitle
   * @param input
   * @return 
   */
  public void setTitle (String input)
  {
    this.title = input;
  }
  /**
   * getTitle
   * @param 
   * @return String
   */
  public String getTitle ()
  {
    return this.title;
  }
}
