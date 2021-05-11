package Node.Task;

import com.enfotech.basecomponent.utility.Utility;
import EnfoTech.Task.*;
import java.lang.reflect.*;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.InetAddress;
import java.util.ArrayList;
import java.util.Hashtable;
import org.apache.log4j.Level;

import Node.API.Email;
import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.OperationLog;
import Node.Biz.Administration.Schedule;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Task.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.DB.Interfaces.INodeDomain;
import Node.Phrase;
import Node.Utils.AppUtils;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create RunnableTask.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class RunnableTask
  extends Task {
    private Boolean semaphore = new Boolean(true);
    private String status = RunnableTask.STATUS_INITIAL;
    private String className = null;
    private String classMethod = null;
    private Class constructorParms[] = null;
    private String[] methodParmNames = null;
    private Class methodParms[] = null;
    private String parmValues[] = null;
    private String returnParm = "";
    private Xschedule schedule = null;
    private String taskDBID = "";
    private String configContent = "";
    private boolean runNowFlag = false;

    public static final String STATUS_RUNNING = "RUNNING";
    public static final String STATUS_STOP = "STOPPED";
    public static final String STATUS_INITIAL = "INITIAL";
    public static final String STATUS_ERROR = "ERROR";
    public static final String STATUS_COMPLETED = "COMPLETED";

    /**
     * Constructor
     * @param schedule
     * @return 
     */
    public RunnableTask(Xschedule schedule) {
        name = "Test task (" + getID() + ")";

        //set schedule configuration
        this.schedule = schedule;

        //set day of week
        if (schedule.getType().equalsIgnoreCase(Xschedule.WEEKLY_INTERVAL)) {
            String days[] = null;
            if (!schedule.getDaysOfWeek().equalsIgnoreCase("")) {
                days = schedule.getDaysOfWeek().split(",");
                this.setDayOfWeek(days);
            }
        }
        else if (schedule.getType().equalsIgnoreCase(Xschedule.MONTHLY_INTERVAL)) {
            String days[] = null;
            if (!schedule.getDaysOfMonth().equalsIgnoreCase("")) {
                days = schedule.getDaysOfMonth().split(",");
                this.setDayOfMonth(days);
            }
        }
        else if (schedule.getType().equalsIgnoreCase(Xschedule.YEARLY_INTERVAL) || schedule.getType().equalsIgnoreCase("once")) {
            String days[] = null;
            //System.out.println(schedule.getDaysOfMonth());
            if (!schedule.getDaysOfMonth().trim().equalsIgnoreCase("")) {
                days = schedule.getDaysOfMonth().split(",");
                this.setDayOfMonth(days);
            }
            String months[] = null;
            //System.out.println(schedule.getMonthsOfYear());
            if (!schedule.getMonthsOfYear().trim().equalsIgnoreCase("")) {
                months = schedule.getMonthsOfYear().split(",");
                this.setMonthOfYear(months);
            }

        }
    }

    /**
     * get runnable task status
     * @return
     */
    public String getStatus() {
        synchronized (semaphore) {
            return status;
        }
    }

    /**
     * set class name
     * @param name
     */
    public void setClassName(String name) {
        this.className = name;
    }

    /**
     * set class method name
     * @param method
     */
    public void setClassMethod(String method) {
        this.classMethod = method;
    }

    /**
     * set config content in string format
     * @param content String
     */
    public void setConfigContent(String content) {
        this.configContent = content;
    }

    /**
     * set run now flag
     * @param runNow boolean run now flag
     */
    public void setRunNowFlag(boolean runNow) {
        this.runNowFlag = runNow;
    }

    /**
     * setReturnParm
     * @param parm
     * @return boolean
     */
    public boolean setReturnParm(XreturnParm parm) {
        try {
            if (parm == null) {
                return true;
            }
            this.returnParm = parm.getType();
        }
        catch (Exception e) {
            return false;
        }
        return true;

    }

    /**
     * set method paramaters
     * @param paramaters array
     * @return true if set successfully, otherwise return false;
     */
    public boolean setMethodParms(XserviceParm[] parms) {
        try {
            if ( (parms == null) || (parms.length == 0)) {
                return true;
            }
            this.parmValues = new String[parms.length];
            for (int i = 0; i < parms.length; i++)
            {
              this.parmValues[i] = parms[i].getValue();
            }
            /*
            this.methodParmNames = new String[parms.length];
            this.methodParms = new Class[parms.length];
            this.parmValues = new Object[parms.length];
            for (int i = 0; i < parms.length; i++) {
                this.methodParmNames[i] = parms[i].getName();
                String type = parms[i].getType();
                if (type.equalsIgnoreCase("int")) {
                    this.methodParms[i] = Integer.TYPE;
                    this.parmValues[i] = new Integer(parms[i].getValue());
                }
                else if (type.equalsIgnoreCase("string")) {
                    this.methodParms[i] = Class.forName("java.lang.String");
                    this.parmValues[i] = new String(parms[i].getValue());
                }
                else if (type.equalsIgnoreCase("long")) {
                    this.methodParms[i] = Long.TYPE;
                    this.parmValues[i] = new Long(parms[i].getValue());
                }
                else if (type.equalsIgnoreCase("bool")) {
                    this.methodParms[i] = Boolean.TYPE;
                    this.parmValues[i] = new Boolean(parms[i].getValue());
                }
                else if (type.equalsIgnoreCase("double")) {
                    this.methodParms[i] = Double.TYPE;
                    this.parmValues[i] = new Double(parms[i].getValue());
                }
                else if (type.equalsIgnoreCase("ArrayList")) {
                  this.methodParms[i] = Class.forName("java.util.ArrayList");
                  String value = parms[i].getValue();
                  ArrayList list = new ArrayList();
                  if (value != null) {
                    String[] split = value.split(",");
                    if (split != null && split.length > 0 && !(split.length == 1 && split[0].equals("")))
                      for (int j = 0; j < split.length; j++)
                        list.add(new String(split[j]));
                  }
                  this.parmValues[i] = list;
                }
            }
*/
        }
        catch (Exception e) {
            return false;
        }
        return true;
    }

    /**
     * validate class definition
     * @return true if class and classmethod found, otherwise return false
     */
    public boolean validate() throws Exception {
        boolean valid = false;
        try {
            Class newClass = Class.forName(this.className);
            Class[] interfaces = newClass.getInterfaces();
            if (interfaces != null && interfaces.length > 0)
            {
              boolean implementsInterface = false;
              for (int i = 0; i < interfaces.length; i++)
              {
                if (interfaces[i].getName().equals("Node.Biz.Interfaces.Task.IProcess"))
                {
                  implementsInterface = true;
                  break;
                }
              }
              if (!implementsInterface)
                throw new Exception("Does Not Implement Task Interface");
            }
            else
              throw new Exception("Does Not Implement any Interfaces");
            //Constructor ct = newClass.getConstructor(this.constructorParms);
            //Object newObj = ct.newInstance(new Object[0]);
            /*
            Method aMethod = newClass.getMethod(this.classMethod,  this.methodParms);
            if (aMethod == null) {
                throw new Exception("No such method found.");
            }*/
            newClass = null;
            //newObj = null;
            //aMethod = null;
            valid = true;
        }
        catch (Exception e) {
            throw new Exception("Could Not Validate Task", e);
        }
        return valid;
    }

    /**
     * get validate message
     * @return message
     */
    public String getValidateMessage() {
        return "";
    }

    /**
     * synchronized run task
     */
    public synchronized void run() {
      Operation op = null;
      NodeUtils nodeUtils = new NodeUtils();
      int opLogID = -1;
      Object returnObj = null;
      String transID = null;
      boolean writeLog = true;
      String opName = null;
      String[] opParamName = null;
      String[] opParamValue = null;
      
      try {
        op = new Operation(this.name, null, Phrase.TaskLoggerName);
        if (!super.canRun(this.schedule))
          return;
        if (op.GetOperationID() >= 0 && !op.CanRunTask())
          return;
      } catch (Exception e) {
        StringWriter sw = new StringWriter();
        e.printStackTrace(new PrintWriter(sw));
        try
        {
          sw.close();
        }
        catch (Exception ex) { }
        LoggingUtils.Log("Could Not Check Run Status of Task: " + sw.toString(), Level.ERROR, Phrase.TaskLoggerName);
      }
      try {
        try {
          if (op != null && this.schedule.getType().equalsIgnoreCase("once")) {
            op.SetTaskSchedule(new Schedule("INACTIVE"));
            op.SaveTask(Phrase.TaskLoggerName);
          }
        } catch (Exception ex) {
          LoggingUtils.Log("Could Not Unschedule Task: "+ex.toString(),Level.ERROR,Phrase.TaskLoggerName);
        }
          this.status = RunnableTask.STATUS_RUNNING;
          if (op.GetOperationID() >= 0) {
            op.StartTask(Phrase.TaskLoggerName);

            // Log Starting
            transID = nodeUtils.GenerateTransID(Phrase.UUID);
            // Check if it is built in GetStatusTask
            opName = op.GetOpName();
            opParamName = op.GetParamNames();
            opParamValue = op.GetParamValues();
            if(opName!=null && opName.equalsIgnoreCase(Phrase.GetStatusTask)){
				if(opParamName!=null && opParamValue!=null){
					if(opParamValue[0]!=null && !opParamValue[0].equalsIgnoreCase("true")){
			            writeLog = false;
					}
				}
            }
            if(writeLog){
                opLogID = nodeUtils.CreateOperationLog(Phrase.TaskLoggerName,op.GetOperationID(),"task",transID,
                        this.status,"Task Started",null,null,null,null,null,null,
                        InetAddress.getLocalHost().getHostName(),this.methodParmNames,this.parmValues);            	
            }
          }
          Class cls = Class.forName(this.className);
          Constructor ct = cls.getConstructor(this.constructorParms);
          IProcess process = (IProcess)ct.newInstance(new Object[0]);
          //Method aMethod = cls.getMethod(this.classMethod, this.methodParms);
          //returnObj = aMethod.invoke(newObj, this.parmValues);
          Hashtable ht = new Hashtable();
          ht.put("opLogID", new Integer(opLogID));
          ht.put("operation", op);
          ProcParam param = new ProcParam(transID, null, Phrase.TaskLoggerName, "system", null, ht );
          returnObj = process.Execute(this.parmValues, param);

          this.status = this.STATUS_STOP;
      }
      catch (Exception e) {
        StringWriter sw = new StringWriter();
        e.printStackTrace(new PrintWriter(sw));
        returnObj = sw.toString();
        LoggingUtils.Log("Problem Executing Task: " + returnObj, Level.ERROR, Phrase.TaskLoggerName);
        this.status = this.STATUS_ERROR;
      } finally {
        this.setLastRunTime(System.currentTimeMillis());
        this.status = RunnableTask.STATUS_STOP;

        try {
          if (op != null && op.GetOperationID() >= 0)
            op.StopTask(Phrase.TaskLoggerName);
        } catch (Exception ex) {
          StringWriter sw = new StringWriter();
          ex.printStackTrace(new PrintWriter(sw));
          LoggingUtils.Log("Could Not Stop Task: "+sw.toString(),Level.ERROR,Phrase.TaskLoggerName);
        }
        finally
        {
          if (opLogID >= 0)
            nodeUtils.UpdateOperationLog(Phrase.TaskLoggerName,opLogID,this.status,"Return:["+returnObj+"]",true);
          try
          {
            // Send Email
	          if(writeLog){
	              this.sendEmail(op, transID, this.status == this.STATUS_ERROR);
	          }
           }
          catch (Exception e)
          {
            StringWriter sw = new StringWriter();
            e.printStackTrace(new PrintWriter(sw));
            try { sw.close(); } catch (Exception ex) { }
            LoggingUtils.Log("Could Not Send Email: " + sw.toString(), Level.ERROR, Phrase.TaskLoggerName);
          }
        }
      }
    }

    /**
     * get log file root
     * @return
     */
    /*
    protected String getLogRoot() {
        String root = "";
        try {
            String mFile = MessageLog.GetInstant().getLogName();
            File mF = new File(mFile);
            root = mF.getParent();
            if (! (root.endsWith("/") || root.endsWith("\\"))) {
                root += "/";
            }
        }
        catch (Exception e) {
            root = "";
        }
        return root;
    }
*/

    /**
     * Update last run time
     * @param time
     */
    public void updateLastRunTime(long time) {
        this.setLastRunTime(time);
    }

    /**
     * get task database id
     * @return
     */
    public String getTaskDBID() {
        return this.taskDBID;
    }

    /**
     * set task database id
     * @param TaskId
     */
    public void setTaskDBID(String TaskId) {
        this.taskDBID = TaskId;
    }

    /**
     * send email to admin
     * @return boolean
     */
    protected boolean sendEmail(Operation op, String transID, boolean endedInError) throws Exception {
      ISystemConfiguration sysDB = DBManager.GetSystemConfiguration(Phrase.TaskLoggerName);
      ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(Phrase.TaskLoggerName);
      INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.TaskLoggerName);
      String[] admins = domainDB.GetAdminEmailAddresses(op.GetDomain());
      String[] ccArray = taskDB.GetTaskEmailCCList();
      if ((admins != null && admins.length > 0) || (ccArray != null && ccArray.length > 0))
      {
        Email email = new Email(sysDB.GetEmailServerHost(), sysDB.GetEmailServerPort(), taskDB.GetTaskEmailUID(), taskDB.GetTaskEmailPWD());

        String content = Utility.ReadToEnd(AppUtils.TaskRoot + "/WEB-INF/config/" + taskDB.GetEmailTempate());
        content = Utility.replace(content,"<%var_systime%>",Utility.GetNow());
        content = Utility.replace(content,"<%var_trans_id%>",transID);
        content = Utility.replace(content,"<%var_task_name%>",op.GetOpName());
        String status = "STOPPED";
        if (endedInError)
          status = "ERROR";
        content = Utility.replace(content,"<%var_status%>",status);
        OperationLog log = new OperationLog(transID, Phrase.TaskLoggerName);
        content = Utility.replace(content,"<%var_start_dttm%>",log.GetStartDate());
        content = Utility.replace(content,"<%var_end_dttm%>",log.GetEndDate());
        String receiver = null;
        int startIndex = 0;
        ArrayList ccList = new ArrayList();
        if (admins != null && admins.length > 0)
        {
          receiver = admins[0];
          for (int i = 1; i < admins.length; i++)
            ccList.add(admins[i]);
        }
        else
        {
          receiver = ccArray[0];
          startIndex = 1;
        }
        if (ccArray != null && ccArray.length > startIndex)
          for (int i = startIndex; i < ccArray.length; i++)
            ccList.add(ccArray[i]);
        ArrayList bccList = new ArrayList();
        String[] bccArray = taskDB.GetTaskEmailBCCList();
        if (bccArray != null && bccArray.length > 0)
          for (int i = 0; i < bccArray.length; i++)
            bccList.add(bccArray[i]);
        return email.SendEmail("Task " + op.GetOpName() + " Status", content, taskDB.GetTaskEmailSenderEmail(), receiver, ccList, bccList);
      }
      return true;
    }
}
