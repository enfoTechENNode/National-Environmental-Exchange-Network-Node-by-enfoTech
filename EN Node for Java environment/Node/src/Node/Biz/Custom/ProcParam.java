package Node.Biz.Custom;

import java.util.Hashtable;

import Node.Phrase;
import Node.API.NodeUtils;

/**
 * This class is passed in as an argument to any class that implements a Process.
 * This class contains functions for accessing values available to a class implementing a Process.
 * <p>Values can be added to the Hashtable in order to reference in the any following post processes.</p>
 * <p>The name of this operation's log4j logger can be accessed in order to log to the application server.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author Ryan Teising
 * @version 1.8
 */
public class ProcParam {
  private String UniqueKey = null;
  private String TransID = null;
  private String RequestorIP = null;
  private String LoggerName = null;
  private String UserName = null;
  private String Password = null;
  private Hashtable Table = null;

  /**
   * Constructs a new ProcParam object for passing into a Process.
   * @param transID transactio id.
   * @param requestorIP requestor ip address.
   * @param loggerName log4j logger name.
   * @param userName user name of the request
   * @param password encrypted password of the original request (if authenticated through the same node)
   * @param hash Hashtable.
   */
  public ProcParam(String transID, String requestorIP, String loggerName, String userName, String password, Hashtable hash) {
    try {
      NodeUtils nodeUtils = new NodeUtils();
      this.UniqueKey = nodeUtils.GenerateTransID(Phrase.UUID);
    } catch (Exception e) {}
    this.TransID = transID;
    this.RequestorIP = requestorIP;
    this.LoggerName = loggerName;
    this.UserName = userName;
    this.Password = password;
    this.Table = hash;
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
