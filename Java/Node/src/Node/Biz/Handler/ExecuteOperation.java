package Node.Biz.Handler;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import java.rmi.RemoteException;
import java.util.Hashtable;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionParameter;

import Node.API.NodeUtils;
import Node.Biz.Custom.PostParam;
import Node.Biz.Custom.PreParam;
import Node.Biz.Custom.ProcParam;
import Node.Phrase;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create ExecuteOperation Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ExecuteOperation {
  private Handler handler = null;	
  /**
   * Constructor.
   * @param 
   * @return 
   */
  public ExecuteOperation()
  {
  }
  
  /**
   * Constructor.
   * @param webMethodHandler The handler object
   * @return 
   */
  public ExecuteOperation(Handler webMethodHandler) {
	  this.handler = webMethodHandler;
  }

  /**
   * ExecuteOperation.
   * @param webMethod Web method name
   * @param xml Operation configuration file
   * @param params Input parameters
   * @param transID Transaction ID
   * @param requestorIP Requester IP address
   * @param loggerName Logger name
   * @param userName User name
   * @param password Password
   * @return Object Operation Object
   */
  public Object ExecuteOperation (String webMethod, String xml, Object[] params, String transID, String requestorIP, String loggerName, String userName, String password) throws RemoteException
  {
    Object retObject = null;
    try {
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(xml);
      
      if (this.handler != null && doc.DocumentElement().Name().toLowerCase().trim().equals("process"))
      {
    	  Object outputobject = this.handler.ExecuteDataflow(doc.OuterXml());
    	  if (outputobject instanceof IActionParameter)
    	  {
    		  IActionParameter output = (IActionParameter)outputobject;
    		  retObject = (output == null)? null : output.getParameterValue();
    	  } else {
    		retObject = outputobject; 
    	  }
      }
      else
      {
	      LoggingUtils.Log("ExecuteOperation: Calling PreProcesses",Level.DEBUG,loggerName);
	
	      // PreProcess
	      PreParam retPreParam = this.ExecutePreProcesses(webMethod, doc.SelectSingleNode("/Operation/PreProcess"),params,transID,requestorIP, loggerName, userName, password);
	
	      LoggingUtils.Log("ExecuteOperation: Calling Process",Level.DEBUG,loggerName);
	
	      // Process
	      XmlNode procNode = doc.SelectSingleNode("/Operation/Process");
	      Object[] objs = this.AddNewProcParam(params, transID, requestorIP, loggerName, userName, password, retPreParam != null ? retPreParam.GetHashtable() : new Hashtable());
	      ProcParam procParam = (ProcParam)objs[objs.length-1];
	      retObject = Reflection.Execute(webMethod,Phrase.PROCESS,procNode, objs);
	
	      LoggingUtils.Log("ExecuteOperation: Calling PostProcesses",Level.DEBUG,loggerName);
	
	      // Post-Process
	      XmlNode postNode = doc.SelectSingleNode("/Operation/PostProcess");
	      if (postNode != null) {
	        PostProcThread thread = new PostProcThread(webMethod, postNode, params, transID, requestorIP,loggerName,userName,password,
	                                                   procParam != null ? procParam.GetHashtable() : new Hashtable(), retObject);
	        thread.start();
	        return retObject;
	      }
      }
      //INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
      //String status = logDB.GetStatus(transID);
      //if (status != null && status.equalsIgnoreCase(Phrase.ReceivedStatus)) {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName, transID, Phrase.CompleteStatus, Phrase.CompleteMessage, true);
      //}

    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retObject;
  }

  /**
   * ExecutePreProcesses.
   * @param webMethod  Web method name
   * @param preNode Operation configuration file
   * @param params  Input parameters
   * @param transID Transaction ID
   * @param requestorIP Requester IP address
   * @param loggerName Logger name
   * @param userName User name
   * @param password Password
   * @return PreParam Output parameter object
   */
  public PreParam ExecutePreProcesses (String webMethod, XmlNode preNode, Object[] params, String transID, String requestorIP, String loggerName, String userName, String password) throws RemoteException
  {
    PreParam retPreParam = null;
    try {
      // Pre-Processes
      if (preNode != null) {
        XmlNodeList sequences = preNode.SelectNodes("Sequence");
        XmlNode[] sequence = new XmlNode[sequences.Count()];
        for (int i = 0; i < sequences.Count(); i++) {
          XmlNode temp = sequences.ItemOf(i);
          int num = Integer.parseInt(temp.SelectSingleNode("@number").GetInnerText());
          sequence[num - 1] = temp;
        }
        Object[] input = this.AddNewPreParam(params, transID, requestorIP, loggerName, userName, password);
        String uniqueKey = ((PreParam)input[input.length-1]).GetUniqueKey();
        for (int i = 0; i < sequence.length; i++) {
          LoggingUtils.Log("Executing PreProcess "+i,Level.DEBUG,loggerName);
          retPreParam = (PreParam)Reflection.Execute(webMethod, Phrase.PRE_PROCESS,sequence[i], input);
          if (retPreParam == null || !uniqueKey.equals(retPreParam.GetUniqueKey()))
            throw new RemoteException(Phrase.InvalidPreParam);
          if (i != sequence.length - 1)
            input = this.AddExistPreParam(params, retPreParam);
        }
      }
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retPreParam;
  }

  /**
   * ExecutePreProcesses.
   * @param webMethod  Web method name
   * @param preNode Operation configuration file
   * @param params  Input parameters
   * @param transID Transaction ID
   * @param requestorIP Requester IP address
   * @param loggerName Logger name
   * @param userName User name
   * @param password Password
   * @return PreParam Output parameter object
   */
  /**
   * ExecutePostProcesses.
   * @param webMethod  Web method name
   * @param postNode Operation configuration file
   * @param params Input parameters
   * @param transID Transaction ID
   * @param requestorIP Requester IP address
   * @param loggerName Logger name
   * @param userName User name
   * @param password Password
   * @param table Parameters table
   * @param result Result
   * @return PostParam Output parameter object
   */
  public PostParam ExecutePostProcesses (String webMethod, XmlNode postNode, Object[] params, String transID, String requestorIP, String loggerName, String userName, String password, Hashtable table, Object result) throws RemoteException
  {
    PostParam retPostParam = null;
    try {
      if (postNode != null) {
        XmlNodeList sequences = postNode.SelectNodes("Sequence");
        XmlNode[] sequence = new XmlNode[sequences.Count()];
        for (int i = 0; i < sequences.Count(); i++) {
          XmlNode temp = sequences.ItemOf(i);
          int num = Integer.parseInt(temp.SelectSingleNode("@number").GetInnerText());
          sequence[num - 1] = temp;
        }
        Object[] input = this.AddNewPostParam(params, transID, requestorIP, loggerName, userName, password, table, result);
        String uniqueKey = ((PostParam)input[input.length-1]).GetUniqueKey();
        for (int i = 0; i < sequence.length; i++) {
          LoggingUtils.Log("Executing PostProcess "+i,Level.DEBUG,loggerName);
          retPostParam = (PostParam)Reflection.Execute(webMethod,Phrase.POST_PROCESS,sequence[i], input);
          if (retPostParam == null || !uniqueKey.equals(retPostParam.GetUniqueKey()))
            throw new RemoteException(Phrase.InvalidPostParam);
          if (i != sequence.length - 1)
            input = this.AddExistPostParam(params, retPostParam);
        }
      }
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retPostParam;
  }

  /**
   * AddNewProcParam.
   * @param exist Exist parameters
   * @param transID Transaction Id
   * @param requestorIP Requester IP address
   * @param loggerName Logger Name
   * @param userName User name
   * @param password Password
   * @param hash New parameters
   * @return Object[] Parameter Objects
   */
  private Object[] AddNewProcParam (Object[] exist, String transID, String requestorIP, String loggerName, String userName, String password, Hashtable hash)
  {
    ProcParam param = new ProcParam(transID, requestorIP, loggerName, userName, password, hash);
    Object[] retArray = new Object[exist.length + 1];
    for (int i = 0; i < exist.length; i++)
      retArray[i] = exist[i];
    retArray[exist.length] = param;
    return retArray;
  }

  /**
   * AddNewPreParam.
   * @param exist Exist parameters
   * @param transID Transaction Id
   * @param requestorIP Requester IP address
   * @param loggerName Logger Name
   * @param userName User name
   * @param password Password
   * @return Object[] Parameter Objects
   */
  private Object[] AddNewPreParam (Object[] exist, String transID, String requestorIP, String loggerName, String userName, String password)
  {
    PreParam param = new PreParam(transID, requestorIP, loggerName, userName, password);
    Object[] retArray = new Object[exist.length + 1];
    for (int i = 0; i < exist.length; i++)
      retArray[i] = exist[i];
    retArray[exist.length] = param;
    return retArray;
  }

  /**
   * AddExistPreParam.
   * @param exist Exist parameters
   * @param param PreParam object
   * @return Object[] Parameter Objects
   */
  private Object[] AddExistPreParam (Object[] exist, PreParam param)
  {
    Object[] retArray = new Object[exist.length + 1];
    for (int i = 0; i < exist.length; i++)
      retArray[i] = exist[i];
    retArray[exist.length] = param;
    return retArray;
  }

  /**
   * AddNewPostParam.
   * @param exist Exist parameters
   * @param transID Transaction Id
   * @param requestorIP Requester IP address
   * @param loggerName Logger Name
   * @param userName User name
   * @param password Password
   * @param hash New parameters
   * @param result Result
   * @return Object[] Parameter Objects
   */
  private Object[] AddNewPostParam (Object[] exist, String transID, String requestorIP, String loggerName, String userName, String password, Hashtable hash, Object result)
  {
    PostParam param = new PostParam(transID, requestorIP, loggerName, userName, password, hash, result);
    return this.AddExistPostParam(exist, param);
  }

  /**
   * AddExistPostParam.
   * @param exist Exist parameters
   * @param param PostParam object
   * @return Object[]
   */
  private Object[] AddExistPostParam (Object[] exist, PostParam param)
  {
    Object[] retArray = new Object[exist.length + 1];
    for (int i = 0; i < exist.length; i++)
      retArray[i] = exist[i];
    retArray[exist.length] = param;
    return retArray;
  }
}
