package Node.Biz.Custom;

import java.util.Hashtable;

import Node.Phrase;
import Node.API.NodeUtils;

/**
 * This class is passed in as an argument to any class that implements a Pre Process.
 * This class contains functions for accessing values available to a class implementing a Pre Process.
 * <p>The object passed in as a parameter should returned from the class.  Any attempt to instantiate
 * a new instance of this object and return it would cause the whole transaction to fail.</p>
 * <p>Values can be added to the Hashtable in order to reference in the any following processes or post processes.</p>
 * <p>The name of this operation's log4j logger can be accessed in order to log to the application server.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author Ryan Teising
 * @version 1.8
 */
public class PreParam {
  private String UniqueKey = null;
  private String TransID = null;
  private String RequestorIP = null;
  private String LoggerName = null;
  private String UserName = null;
  private String Password = null;
  private Hashtable Table = null;

  /**
   * Constructs a new PreParam object for passing into a Pre Process.
   * <p>This object should never be constructed from inside operation specific code.
   * Newly instantiated PreParams returned from a Pre Process will cause the operation
   * to exit immediately.</p>
   * @param transID transaction id.
   * @param requestorIP requestor ip address.
   * @param loggerName log4j logger name.
   * @param userName user name of the request
   * @param password encrypted password of the original request (if authenticated through the same node)
   */
  public PreParam(String transID, String requestorIP, String loggerName, String userName, String password) {
    try {
      NodeUtils nodeUtils = new NodeUtils();
      this.UniqueKey = nodeUtils.GenerateTransID(Phrase.UUID);
    } catch (Exception e) {}
    this.TransID = transID;
    this.RequestorIP = requestorIP;
    this.LoggerName = loggerName;
    this.UserName = userName;
    this.Password = password;
    this.Table = new Hashtable();
  }

  /**
   * Get the Unique Random Key that identifies this PostParam Object.
   * @return unique key
   */
  public String GetUniqueKey ()
  {
    return this.UniqueKey;
  }

  /**
   * Get the Transaction ID of this Transaction.
   * @return transaction id.
   */
  public String GetTransID ()
  {
    return this.TransID;
  }

  /**
   * Get the Requestor IP Address.
   * @return requestor IP Address.
   */
  public String GetRequestorIP ()
  {
    return this.RequestorIP;
  }

  /**
   * Get the log4j Logger Name of this operation.
   * @return logger name.
   */
  public String GetLoggerName ()
  {
    return this.LoggerName;
  }

  /**
   * Add a key-value pair to the Hashtable contained in this object.
   * @param key.
   * @param value.
   */
  public void AddKeyValue (String key, Object value)
  {
    this.Table.put(key,value);
  }

  /**
   * Get the value of a key in the Hashtable contained in this object.
   * @param key lookup key.
   * @return value.
   */
  public Object GetValue (String key)
  {
    return this.Table.get(key);
  }

  /**
   * Get the entire Hashtable object contained in this object.
   * @return the Hashtable.
   */
  public Hashtable GetHashtable ()
  {
    return this.Table;
  }

  /**
   * Get User Name of the Request
   * @return String User Name
   */
  public String GetUserName ()
  {
    return this.UserName;
  }

  /**
   * Get (encrypted) Password of the Request
   * @return String Password
   */
  public String GetPassword ()
  {
    return this.Password;
  }
}
