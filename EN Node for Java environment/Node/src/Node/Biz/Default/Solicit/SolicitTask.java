package Node.Biz.Default.Solicit;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.NotificationTypeCode;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Task.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
/**
 * <p>This class create Solicit Task Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SolicitTask implements IProcess {

	  /**
	   * Constructor.
	   * @param
	   * @return 
	   */
  public SolicitTask() {
  }

  /**
   * Execute
   * @param parameters The parameter array
   * @param procParam The process parameter
   * @throws
   * @return boolean The status of sending email
   */
  //public String Execute (int opID, String transID, String requestorIP, String token, String returnURL, String request, ArrayList params, String loggerName)
  public String Execute(String[] parameters, ProcParam param)
  {
    String retString = null;
    int opID = -1;
    try {
      Operation.DeleteTask(Phrase.WebServicesLoggerName,"Solicit:"+parameters[5]+":"+parameters[1]);
      opID = Integer.parseInt(parameters[0]);
      INodeOperation opDB = DBManager.GetNodeOperation(parameters[7]);
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(opDB.GetOperationConfig(opID));
      SolicitProcess process = new SolicitProcess(doc,parameters[1],parameters[2]);
      Object[] solicitParams = null;
      if (parameters[6] != null)
      {
        String[] split = parameters[6].split(",");
        if (split != null && split.length > 0)
        {
          solicitParams = new Object[split.length];
          for (int i = 0; split != null && i < split.length; i++)
            solicitParams[i] = split[i];
        }
      }
      if(parameters.length<10) process.ExecuteSolicit(parameters[3],parameters[4],parameters[5],solicitParams,parameters[7],parameters[8],parameters[9]);	// handle node 1.1
      else{	// handle node 2.0
          String[] recipientsList = null;
          String[] notificationURITypeStrList =null;
          NotificationURIType[] notificationURITypeList = null;
          if (parameters[6] != null){
        	  recipientsList = parameters[6].split(",");
          }
          if (parameters[7] != null){
        	  notificationURITypeStrList = parameters[7].split(",");
        	  if (notificationURITypeStrList != null && notificationURITypeStrList.length > 0){
        		  notificationURITypeList = new NotificationURIType[notificationURITypeStrList.length];
        		  for (int i = 0; notificationURITypeStrList != null && i < notificationURITypeStrList.length; i++){
        			  notificationURITypeList[i] = new NotificationURIType();
        			  NotificationTypeCode notificationTypeCode = NotificationTypeCode.Factory.fromValue(parameters[12]);
        			  notificationURITypeList[i].setNotificationType(notificationTypeCode);
        			  notificationURITypeList[i].setString(notificationURITypeStrList[i]);
        		  }
        		  
        	  }
          }
          if (parameters[8] != null){
            String[] split = parameters[8].split(",");
            if (split != null && split.length > 0)
            {
              solicitParams = new Object[split.length];
              for (int i = 0; split != null && i < split.length; i++)
                solicitParams[i] = split[i];
            }
          }
    	  process.ExecuteSolicit(parameters[3],parameters[4],parameters[5],recipientsList,notificationURITypeList,solicitParams,parameters[9],parameters[10],parameters[11]);
      }
      retString = "Solicit Executed Successfully";
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.TaskLoggerName,opID,Phrase.FailedStatus,"Remote Exception: "+e.toString(),true);
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      retString = "Remote Exception: "+sw.toString();
    } catch (Exception e) {
      LoggingUtils.Log("Error Executing Solicit: "+e.toString(),Level.ERROR,Phrase.WebServicesLoggerName);
      NodeUtils nodeUtils = new NodeUtils();
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      nodeUtils.UpdateOperationLog(Phrase.TaskLoggerName,opID,Phrase.FailedStatus,"Error Executing Solicit: "+sw.toString(),true);
      retString = "Error Executing Solicit: "+e.toString();
    }
    return retString;
  }
}
