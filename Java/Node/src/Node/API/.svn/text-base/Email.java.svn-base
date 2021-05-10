package Node.API;

import com.enfotech.basecomponent.utility.email.SendMail;
import java.util.ArrayList;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;

/**
 * <p>API Email</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * 
 * @author enfoTech
 * @version 2.0
 */

public class Email {
  private String Host = null;
  private String Port = null;
  private String UserID = null;
  private String Credential = null;

  /**
   * Constructs an Email API Class using the specified Email Host Settings
   * If userID or credential are null or the empty string, then the email will be sent with Credentials supplied to Email Server Host
   * @param host String Host Name of the Email Server (IP address or Domain Name)
   * @param port String Port Number for the Email Server (usually 25)
   * @param userID String User ID needed for authentication in order to send Email
   * @param credential String Password needed for authentication in order to send Email
   */
  public Email(String host, String port, String userID, String credential) {
    this.Host = host;
    this.Port = port;
    this.UserID = userID;
    this.Credential = credential;
  }

  /**
   * Constructs an Email API Class using the Email Served Specified in the Node Configuration
   * @param loggerName String loggerName used in case of error
   * @throws Exception
   */
  public Email (String loggerName) throws Exception
  {
    ISystemConfiguration configDB = DBManager.GetSystemConfiguration(loggerName);
    this.Host = configDB.GetEmailServerHost();
    this.Port = configDB.GetEmailServerPort();
    String uid = configDB.GetUserEmailUID();
    String pwd = configDB.GetUserEmailPWD();
    if (uid != null && !uid.trim().equals("") && pwd != null && !pwd.trim().equals(""))
    {
      this.UserID = uid;
      this.Credential = pwd;
    }
  }

  /**
   * Text Body Format
   */
  public static final String MAIL_FORMAT_TEXT = SendMail.MAIL_FORMAT_TEXT;

  /**
   * HTML Body Format
   */
  public static final String MAIL_FORMAT_HTML = SendMail.MAIL_FORMAT_HTML;

  /**
   * Low Priority
   */
  public static final String PRIORITY_LOW = SendMail.PRIORITY_LOW;

  /**
   * Normal Priority
   */
  public static final String PRIORITY_NORMAL = SendMail.PRIORITY_NORMAL;

  /**
   * High Priority
   */
  public static final String PRIORITY_HIGH = SendMail.PRIORITY_HIGH;

  /**
   * Send an Email
   * Uses the default values for Body Format and Priority (Email.MAIL_FORMAT_TEXT, Email.PRIORITY_NORMAL)
   * Sends the Email with no one on the CC List or BCC List
   * @param subject String Suject of the Email
   * @param content String Content of the Body of the Email
   * @param sender String Sender (usually an Email Address, but not required) of the Email
   * @param receiver String Receiver (should be an Email Address) of the Email
   * @return boolean true if successful, false otherwise
   */
  public boolean SendEmail (String subject, String content, String sender, String receiver)
  {
    return this.SendEmail(subject, content, sender, receiver, new ArrayList(), new ArrayList(), this.MAIL_FORMAT_TEXT, this.PRIORITY_NORMAL);
  }

  /**
   * Send an Email
   * Uses the default values for Body Format and Priority (Email.MAIL_FORMAT_TEXT, Email.PRIORITY_NORMAL)
   * @param subject String Suject of the Email
   * @param content String Content of the Body of the Email
   * @param sender String Sender (usually an Email Address, but not required) of the Email
   * @param receiver String Receiver (should be an Email Address) of the Email
   * @param ccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @param bccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @return boolean true if successful, false otherwise
   */
  public boolean SendEmail (String subject, String content, String sender, String receiver, ArrayList bccList, ArrayList ccList)
  {
    return this.SendEmail(subject, content, sender, receiver, bccList, ccList, this.MAIL_FORMAT_TEXT, this.PRIORITY_NORMAL);
  }

  /**
   * Send an Email
   * @param subject String Suject of the Email
   * @param content String Content of the Body of the Email
   * @param sender String Sender (usually an Email Address, but not required) of the Email
   * @param receiver String Receiver (should be an Email Address) of the Email
   * @param ccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @param bccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @param bodyFormat String Format of Body (Should be one Email.MAIL_FORMAT_TEXT or Email.MAIL_FORMAT_HTML)
   * @param priority String Priority of Email (Should be one of Email.PRIORITY_LOW, Email.PRIORITY_NORMAL, Email.PRIORITY_HIGH)
   * @return boolean true if successful, false otherwise
   */
  public boolean SendEmail (String subject, String content, String sender, String receiver, ArrayList ccList, ArrayList bccList, String bodyFormat, String priority)
  {
    boolean isSent = false;
    SendMail mail = new SendMail();
    mail.Host = this.Host;
    mail.Port = this.Port;
    if (this.UserID != null && !this.UserID.equals("") && this.Credential != null && !this.Credential.equals("")) {
      mail.UserName = this.UserID;
      mail.Password = this.Credential;
    }
    mail.Subject = subject;
    mail.Body = content;
    mail.BodyFormat = bodyFormat;
    mail.Priority = priority;
    mail.Sender = sender;
    mail.Receiver = receiver;
    ArrayList cc = ccList;
    if (cc == null)
      cc = new ArrayList();
    ArrayList bcc = bccList;
    if (bcc == null)
      bcc = new ArrayList();
    mail.CcList = cc;
    mail.BccList = bcc;
    try {
      mail.Send();
      isSent = true;
    } catch (Exception e) {
      e.printStackTrace();
    }
    return isSent;
  }

  /**
   * Send an Email
   * @param subject String Suject of the Email
   * @param content String Content of the Body of the Email
   * @param sender String Sender (usually an Email Address, but not required) of the Email
   * @param receiver String Receiver (should be an Email Address) of the Email
   * @param ccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @param bccList ArrayList An ArrayList of Strings that Comprise the Email List of the CC List
   * @param bodyFormat String Format of Body (Should be one Email.MAIL_FORMAT_TEXT or Email.MAIL_FORMAT_HTML)
   * @param attachmentsList ArrayList An ArrayList of attachments objects
   * @param priority String Priority of Email (Should be one of Email.PRIORITY_LOW, Email.PRIORITY_NORMAL, Email.PRIORITY_HIGH)
   * @return boolean true if successful, false otherwise
   */
  public boolean SendEmail (String subject, String content, String sender, String receiver, ArrayList ccList, ArrayList bccList, String bodyFormat,ArrayList attachmentsList, String priority)
  {
    boolean isSent = false;
    SendMail mail = new SendMail();
    mail.Host = this.Host;
    mail.Port = this.Port;
    if (this.UserID != null && !this.UserID.equals("") && this.Credential != null && !this.Credential.equals("")) {
      mail.UserName = this.UserID;
      mail.Password = this.Credential;
    }
    mail.Subject = subject;
    mail.Body = content;
    mail.BodyFormat = bodyFormat;
    mail.Priority = priority;
    mail.Sender = sender;
    mail.Receiver = receiver;
    ArrayList cc = ccList;
    if (cc == null)
      cc = new ArrayList();
    ArrayList bcc = bccList;
    if (bcc == null)
      bcc = new ArrayList();
    mail.CcList = cc;
    mail.BccList = bcc;
    if (attachmentsList == null)
    	attachmentsList = new ArrayList();
    mail.Attachments = attachmentsList;
    try {
      mail.Send();
      isSent = true;
    } catch (Exception e) {
      e.printStackTrace();
    }
    return isSent;
  }

}
