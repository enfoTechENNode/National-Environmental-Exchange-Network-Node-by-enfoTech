package Node.Biz.Handler;

import java.util.Hashtable;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.API.NodeUtils;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.typelib.xml.XmlNode;
/**
 * <p>This class create PostProcThread Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class PostProcThread extends Thread {
  private String WebMethod = null;
  private XmlNode PostProcNode = null;
  private Object[] Params = null;
  private String TransID = null;
  private String RequestorIP = null;
  private String LoggerName = null;
  private String UserName = null;
  private String Password = null;
  private Hashtable Table = null;
  private Object Result = null;

  /**
   * Constructor.
   * @param webMethod Web method
   * @param node Xml file node
   * @param params Input parameters
   * @param transID Transaction ID
   * @param requestorIP Requester IP address
   * @param loggerName logger Name
   * @param userName User Name
   * @param password Password
   * @param hash Hash table for extra input parameters
   * @param result Result object
   * @return 
   */
  public PostProcThread(String webMethod, XmlNode node, Object[] params, String transID, String requestorIP, String loggerName, String userName, String password, Hashtable hash, Object result) {
    this.WebMethod = webMethod;
    this.PostProcNode = node;
    this.Params = params;
    this.TransID = transID;
    this.RequestorIP = requestorIP;
    this.LoggerName = loggerName;
    this.UserName = userName;
    this.Password = password;
    this.Table = hash;
    this.Result = result;
  }

  /**
   * run.
   * @param 
   * @return
   */
  public void run ()
  {
    try {
      if (this.PostProcNode == null)
        throw new Exception("PostProcNode is null.");
      ExecuteOperation execute = new ExecuteOperation();
      execute.ExecutePostProcesses(this.WebMethod,this.PostProcNode,this.Params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password,this.Table,this.Result);
      //INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
      //String status = logDB.GetStatus(this.TransID);
      //if (status != null && status.equalsIgnoreCase(Phrase.ReceivedStatus)) {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, this.TransID, Phrase.CompleteStatus, Phrase.CompleteMessage,true);
      //}
    } catch (Exception e) {
      LoggingUtils.Log("Could not execute PostProcess: " + e.toString(),Level.ERROR,Phrase.WebServicesLoggerName);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
    }
  }
}
