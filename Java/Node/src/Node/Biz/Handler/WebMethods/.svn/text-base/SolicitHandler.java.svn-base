package Node.Biz.Handler.WebMethods;

import java.rmi.RemoteException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Random;
import java.util.TimeZone;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionParameter;
import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.Schedule;
import Node.Biz.Custom.PreParam;
import Node.Biz.Default.Solicit.SolicitThread;
import Node.Biz.Handler.ExecuteOperation;
import Node.Biz.Handler.Handler;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
/**
 * <p>This class create SolicitHandler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SolicitHandler extends Handler {
	protected String ReturnURL = null;
	protected String Request = null;
	protected Object[] Params = null;
	protected int OpID = -1;

	  /**
	   * Constructor.
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param returnURL Return node end point
	   * @param request Operation Name
	   * @param params Input parameters
	   * @return 
	   */
  public SolicitHandler(String requestorIP, String hostName, String token, String returnURL, String request, Object[] params) {
    super(requestorIP);
    this.HostName = hostName;
    this.Token = token;
    this.ReturnURL = returnURL;
    this.Request = request;
    this.Params = params;
  }

  /**
   * Initialize.
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.Request,Phrase.WEB_METHOD_SOLICIT);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String paramValues = null;
        if (this.Params != null && this.Params.length > 0) {
          paramValues = this.Params[0]+"";
          for (int i = 1; i < this.Params.length; i++)
            paramValues += ", "+this.Params[i];
        }
        String[] names = new String[] { "Request","Parameter Values" };
        String[] values = new String[] { this.Request,paramValues };

        // add search parameter 
        Object[] ret = new Object[2] ;
        ret = utils.AddSearchParameter(Integer.toString(this.OpID), names, values);

        if(ret != null && ret[0] != null && ret[1] != null){
            Object[] tmpName = (Object[])ret[0];
            names = new String[tmpName.length];
            for(int i=0;i<names.length;i++){
                names[i] = tmpName[i].toString();                  
            }
            Object[] tmpValue = (Object[])ret[0];
            values = new String[tmpValue.length];
            for(int i=0;i<values.length;i++){
            	values[i] = tmpValue[i].toString();                  
            }       	
        }
        // Create log file with new added search parameters
        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,this.Token,null,this.ReturnURL,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize Solicit Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize Solicit Handler",e);
    }
  }

  /**
   * Authorize.
   * @param 
   * @return String
   */
  protected String Authorize () throws RemoteException
  {
    String userID = null;
    NodeUtils utils = new NodeUtils();
    try {
      userID = this.AuthorizeRequest(this.OpID);
      utils.UpdateOperationLogUserName(Phrase.WebServicesLoggerName,this.TransID,userID);
    } catch (RemoteException e) {
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient Solicit Permission",true);
      throw new RemoteException(Phrase.InvalidToken);
    }
    return userID;
  }

  /**
   * ExecuteDataflow.
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_1);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.returnURL, this.ReturnURL);
      process.CreateActionParameter(WebServiceParameter.request, this.Request);

      INodeOperation nodeOpe = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
      String[] pars = nodeOpe.GetParameters(this.OpID);
      if (pars != null && pars.length == this.Params.length)
      {
          for (int i = 0; i < pars.length; i++)
              process.CreateActionParameter(pars[i].trim(), this.Params[i]);
      }
      process.Execute(dataflowConfig);
      return this.TransID;
  }

  /**
   * Execute.
   * @param 
   * @return Object
   */
  protected Object Execute () throws RemoteException
  {
    Object retObj = null;
    try {
      if (this.OpID >= 0) {
        // Get Config and Information
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        Operation op = opDB.GetOperation(this.OpID);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(op.GetConfig());
        Object[] params = new Object[] { this.Token,this.ReturnURL,this.Request,this.Params };
        ExecuteOperation execute = new ExecuteOperation(this);

        if (doc.DocumentElement().Name().toLowerCase().trim().equals("process"))// Execute dataflow wizard
        {
      	  Object outputobject = this.ExecuteDataflow(doc.OuterXml());
      	  if (outputobject instanceof IActionParameter)
      	  {
      		  IActionParameter output = (IActionParameter)outputobject;
      		retObj = (output == null)? null : output.getParameterValue();
      	  } else {
      		retObj = outputobject; 
      	  }
        }else{
            // Execute PreProcesses
            PreParam param = execute.ExecutePreProcesses(Phrase.WEB_METHOD_SOLICIT,doc,params,this.TransID,this.RequestorIP,op.GetLoggerName(),this.UserName,this.Password);
            if (!this.ScheduleSolicit(doc,op.GetLoggerName()))
              throw new RemoteException(Phrase.InternalError);        	
            retObj = this.TransID;
        }
      }
      else
        throw new RemoteException("Operation is Not Available");
    } catch (RemoteException e) {
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Execute Operation: "+e.toString(),Level.ERROR);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retObj;
  }

  /**
   * ScheduleSolicit
   * @param doc Schedule xml configuration file
   * @param loggerName logger name
   * @return boolean Set successful
   */
  private boolean ScheduleSolicit (XmlDocument doc, String loggerName) throws RemoteException
  {
    boolean retBool = false;
    try {
      boolean runNow = true;
      if (doc != null) {
        XmlNode startNode = doc.SelectSingleNode("/Operation/Process/Solicit/SolicitStartTime");
        XmlNode endNode = doc.SelectSingleNode("/Operation/Process/Solicit/SolicitEndTime");
        if (startNode != null && endNode != null) {
          Calendar now = this.IsInRange(startNode.GetInnerText(),endNode.GetInnerText());
          if (now != null) {
            runNow = false;
            this.ScheduleTask(doc,now,loggerName);
          }
        }
      }
      if (runNow) {
        SolicitThread thread = new SolicitThread(doc,this.TransID,this.RequestorIP,this.Token,this.ReturnURL,this.Request,
                                                 this.Params,loggerName,this.UserName,this.Password);
        thread.start();
      }
      retBool = true;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retBool;
  }

  /**
   * IsInRange
   * @param start Task start time
   * @param end Task end time
   * @return Calendar Current time
   */
  private Calendar IsInRange (String start, String end) throws Exception
  {
    Calendar retCal = Calendar.getInstance(TimeZone.getDefault());
    SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss");
    Date startDate = sdf.parse(start);
    Calendar temp = Calendar.getInstance(TimeZone.getDefault());
    temp.setTime(startDate);
    Calendar startCal = Calendar.getInstance(TimeZone.getDefault());
    startCal.set(Calendar.HOUR_OF_DAY,temp.get(Calendar.HOUR_OF_DAY));
    startCal.set(Calendar.MINUTE,temp.get(Calendar.MINUTE));
    startCal.set(Calendar.SECOND,temp.get(Calendar.SECOND));
    if (startCal.before(retCal)) {
      Date endDate = sdf.parse(end);
      temp.setTime(endDate);
      Calendar endCal = Calendar.getInstance(TimeZone.getDefault());
      endCal.set(Calendar.HOUR_OF_DAY,temp.get(Calendar.HOUR_OF_DAY));
      endCal.set(Calendar.MINUTE,temp.get(Calendar.MINUTE));
      endCal.set(Calendar.SECOND,temp.get(Calendar.SECOND));
      if (endCal.before(startCal))
        endCal.add(Calendar.DAY_OF_MONTH,1);
      if (endCal.after(retCal))
        retCal = null;
    }
    return retCal;
  }

  /**
   * ScheduleTask
   * @param doc Operation configuration file
   * @param now Current time
   * @param loggerName Logger name
   * @return 
   */
  private void ScheduleTask (XmlDocument doc, Calendar now, String loggerName) throws RemoteException
  {
    try {
      Operation op = new Operation("Solicit:"+this.Request+":"+this.TransID,null);
      op.SetType(Phrase.SCHEDULED_TASK_OPERATION);
      op.SetClassName(Phrase.SOLICIT_TASK);
      op.SetMethodName(Phrase.SOLICIT_TASK_METHOD);
      op.SetTaskParameters(this.GetParameters(this.OpID,loggerName));
      op.SetTaskSchedule(this.GetSchedule(doc.SelectSingleNode("/Operation/Process"),now));
      if (!op.SaveTask(Phrase.WebServicesLoggerName))
        throw new RemoteException(Phrase.InternalError);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
  }

  /**
   * GetParameters
   * @param opID Operation ID
   * @param loggerName Logger Name
   * @return ArrayList Parameters
   */
  private ArrayList GetParameters (int opID, String loggerName)
  {
    ArrayList retList = new ArrayList();

    ArrayList temp = new ArrayList();
    temp.add("1");
    temp.add("opID");
    temp.add(opID+"");
    retList.add(temp);

    temp = new ArrayList();
    temp.add("2");
    temp.add("transID");
    temp.add(this.TransID);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("3");
    temp.add("requestorIP");
    temp.add(this.RequestorIP);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("4");
    temp.add("token");
    temp.add(this.Token);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("5");
    temp.add("returnURL");
    temp.add(this.ReturnURL);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("6");
    temp.add("request");
    temp.add(this.Request);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("7");
    temp.add("params");
    String value = "";
    if (this.Params != null && this.Params.length > 0) {
      for (int i = 0; i < this.Params.length; i++) {
        if (i != 0) value += ",";
        value += this.Params[i].toString();
      }
    }
    temp.add(value);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("8");
    temp.add("loggerName");
    temp.add(loggerName);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("9");
    temp.add("userName");
    temp.add(this.UserName);
    retList.add(temp);

    temp = new ArrayList();
    temp.add("10");
    temp.add("password");
    temp.add(this.Password);
    retList.add(temp);

    return retList;
  }

  /**
   * GetSchedule
   * @param process Operation configuration file
   * @param now current time
   * @return Schedule Schedule object
   */
  private Schedule GetSchedule (XmlNode process, Calendar now) throws RemoteException
  {
    Schedule retSchedule = null;
    try {
      retSchedule = new Schedule("ONCE");
      SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss");
      Date start = sdf.parse(process.SelectSingleNode("Solicit/SolicitStartTime").GetInnerText());
      Calendar temp = Calendar.getInstance(TimeZone.getDefault());
      temp.setTime(start);
      Calendar startCal = Calendar.getInstance(TimeZone.getDefault());
      startCal.set(Calendar.HOUR_OF_DAY,temp.get(Calendar.HOUR_OF_DAY));
      startCal.set(Calendar.MINUTE,temp.get(Calendar.MINUTE));
      startCal.set(Calendar.SECOND,temp.get(Calendar.SECOND));
      Date end = sdf.parse(process.SelectSingleNode("Solicit/SolicitEndTime").GetInnerText());
      temp.setTime(end);
      Calendar endCal = Calendar.getInstance(TimeZone.getDefault());
      endCal.set(Calendar.HOUR_OF_DAY,temp.get(Calendar.HOUR_OF_DAY));
      endCal.set(Calendar.MINUTE,temp.get(Calendar.MINUTE));
      endCal.set(Calendar.SECOND,temp.get(Calendar.SECOND));
      if (startCal.before(now))
        startCal.add(Calendar.DAY_OF_MONTH,1);
      if (endCal.before(startCal))
        endCal.add(Calendar.DAY_OF_MONTH,1);
      start = startCal.getTime();
      end = endCal.getTime();
      Random random = new Random(new Date().getTime());
      int range = (int)(end.getTime() - start.getTime());
      int nextRandom = random.nextInt(range);
      Date schedDate = new Date(start.getTime()+(long)nextRandom);
      retSchedule.SetStartDate(schedDate);
    } catch (Exception e) {
      throw new RemoteException("Could Not Get Schedule",e);
    }
    return retSchedule;
  }
}
