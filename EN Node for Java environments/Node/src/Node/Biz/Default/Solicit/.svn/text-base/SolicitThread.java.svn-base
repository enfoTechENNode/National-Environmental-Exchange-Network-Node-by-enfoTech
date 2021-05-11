package Node.Biz.Default.Solicit;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.rmi.RemoteException;
import org.apache.log4j.Level;

import Node.API.NodeUtils;
import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create Solicit Thread Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SolicitThread extends Thread {
  private String ReturnURL = null;
  private XmlDocument Config = null;
  private String TransID = null;
  private String RequestorIP = null;
  private String Token = null;
  private String Dataflow = null;
  private String[] Recipients = null;
  private String Request = null;
  private Object[] NotificationURIType = null;
  private Object[] Params = null;
  private String LoggerName = null;
  private String UserName = null;
  private String Password = null;
  private boolean version2 = false;

  /**
   * Constructor.
   * @param doc The configuration xml file
   * @param transID The transaction ID
   * @param requestorIP The requestor IP
   * @param token The token
   * @param returnURL The return URL
   * @param request The request type
   * @param params The parameter object array
   * @param loggerName The logger name
   * @param userName The user name
   * @param password The pass word
   * @return 
   */
  public SolicitThread(XmlDocument doc, String transID, String requestorIP, String token, String returnURL, String request,
                       Object[] params, String loggerName, String userName, String password) throws Exception
  {
    if (doc == null)
      throw new Exception("Could Not Find Configuration File");
    this.Config = doc;
    this.TransID = transID;
    this.RequestorIP = requestorIP;
    this.Token = token;
    this.ReturnURL = returnURL;
    this.Request = request;
    this.Params = params;
    this.LoggerName = loggerName;
    this.UserName = userName;
    this.Password = password;
  }

  /**
   * Constructor.
   * @param doc The configuration xml file
   * @param transID The transaction ID
   * @param requestorIP The requestor IP
   * @param token The token
   * @param dataflow The data flow name
   * @param request The request type
   * @param recipients The recipient array
   * @param notificationURIType The notificationURIType array
   * @param params The parameter object array
   * @param loggerName The logger name
   * @param userName The user name
   * @param password The pass word
   * @return 
   */
  public SolicitThread(XmlDocument doc, String transID, String requestorIP,
			String token, String dataflow, String request, String[] recipients,
			Object[] notificationURIType, Object[] params, String loggerName,
			String userName, String password) throws Exception {
		if (doc == null)
			throw new Exception("Could Not Find Configuration File");
		this.Config = doc;
		this.TransID = transID;
		this.RequestorIP = requestorIP;
		this.Token = token;
		this.Dataflow = dataflow;
		this.Request = request;
		this.Recipients = recipients;
		this.NotificationURIType = notificationURIType;
		this.Params = params;
		this.LoggerName = loggerName;
		this.UserName = userName;
		this.Password = password;
		this.version2 = true;
	}


  /**
   * run.
   * @param 
   * @return 
   */
   public void run ()
  {
    try {
      SolicitProcess process = new SolicitProcess(this.Config,this.TransID,this.RequestorIP);
      if(this.version2){
          process.ExecuteSolicit(this.Token,this.Dataflow,this.Request,this.Recipients,this.NotificationURIType,this.Params,this.LoggerName,this.UserName,this.Password);    	  
      }else  process.ExecuteSolicit(this.Token,this.ReturnURL,this.Request,this.Params,this.LoggerName,this.UserName,this.Password);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,"Remote Exception: "+sw.toString(),true);
    } catch (Exception e) {
      LoggingUtils.Log("Error Executing Solicit: "+e.toString(),Level.ERROR,Phrase.WebServicesLoggerName);
      NodeUtils nodeUtils = new NodeUtils();
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,"Error Executing Solicit: "+sw.toString(),true);
    }
  }
}
