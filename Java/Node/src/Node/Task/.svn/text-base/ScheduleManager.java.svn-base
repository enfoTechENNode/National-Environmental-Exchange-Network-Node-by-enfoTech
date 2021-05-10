package Node.Task;

import EnfoTech.Task.*;
import java.io.StringWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.util.*;
import org.apache.log4j.Level;

import Node.API.NodeUtils;
import Node.Biz.Administration.Operation;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
import Node.Utils.LoggingUtils;
import Node.DB.Interfaces.INodeOperationLog;
/**
 * <p>This class create ScheduleManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ScheduleManager {
  public ScheduleManager() {
  }

  /**
   * cancel all of tasks
   * @return
   */
  public boolean cancelTasks() {
    TaskScheduler scheduler = TaskScheduler.getInstance();
    scheduler.cancelAll(true);
    return true;
  }

  /* Add by charlie zhang 2007-9-13 begin  */
  /**
   * schedule single task that define in the task.config file
   * @return true if schedule successfully, otherwise return false
   */
  public boolean scheduleTask() throws Exception {
    boolean bReturn = false;
    try {
      LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>get into scheduleTask()", Level.DEBUG, Phrase.TaskLoggerName);
      TaskScheduler scheduler = TaskScheduler.getInstance();
      ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.TaskLoggerName);
      INodeOperation opearation = DBManager.GetNodeOperation(Phrase.TaskLoggerName);

      Xtask xTasks[] = configDB.GetTasks();
      //no active tasks
      if ( (xTasks == null) || (xTasks.length <= 0)) {
        return bReturn = true;
      }
      //schedule active tasks
      for (int i = 0; i < xTasks.length; i++) {
        String status = xTasks[i].getStatus();
        LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>The status of task "+xTasks[i].getName()+" is: "+xTasks[i].getStatus(), Level.DEBUG, Phrase.TaskLoggerName);
        if(status.length()>2){
          if (status.substring(status.indexOf("_") + 1).equalsIgnoreCase("C")) {
            /* cancel old task */
            Task task[] = scheduler.getRunningTasks();
            for(int j=0;j<task.length;j++){
              if((task[j].getName()).equalsIgnoreCase(xTasks[i].getName())&&(!task[j].isCancelled())){
                  LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>The old " + task[j].getName()+" is canceled."
                                   , Level.DEBUG, Phrase.TaskLoggerName);
                  task[j].cancel();
              }
            }
            if(opearation.GetOperationStatus(xTasks[i].getName())!=null){
              if((!opearation.GetOperationStatus(xTasks[i].getName()).equalsIgnoreCase(Phrase.INACTIVE_STATUS))
                 &&(!opearation.GetOperationStatus(xTasks[i].getName()).equalsIgnoreCase(Phrase.STOPPED_STATUS))){
                bReturn = this.schedule(scheduler, xTasks[i]);
                LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>The " + xTasks[i].getName()+" has been rescheduled."
                                 , Level.DEBUG, Phrase.TaskLoggerName);
              }
            }
            // rewrite the status
            configDB.SaveTaskStatus(xTasks[i]);
            /* reschedule same task if operation is running  */
            LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>The operation status is " + opearation.GetOperationStatus(xTasks[i].getName()), Level.DEBUG, Phrase.TaskLoggerName);
          }
        }
      }
      bReturn = true;
    }
    catch (Exception e) {
      LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>Get error: " + e.toString()
                      , Level.ERROR, Phrase.TaskLoggerName);
      e.printStackTrace();
      bReturn = false;
      throw e;
    }
    return bReturn;
  }

  /* Add by charlie zhang 2007-9-13 end */

  /**
   * schedule tasks that define in the task.config file
   * @return true if schedule successfully, otherwise return false
   */
  public boolean scheduleTasks() {
    boolean bReturn = false;
    try {
      //add FilePersistStrategy
      //FilePersistStrategy persister = new FilePersistStrategy( new java.io.File( "C:\\TASK" )

      /* Add by charlie zhang 2007-9-13 begin */
      TaskScheduler scheduler = TaskScheduler.getInstance();
      //TaskScheduler scheduler = new TaskScheduler();
      /* Add by charlie zhang 2007-9-13 end */

      //scheduler.setPersistStrategy( persister );
      ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.TaskLoggerName);
      /* Add by charlie zhang 2007-9-20 begin */
      INodeOperation operation = DBManager.GetNodeOperation(Phrase.TaskLoggerName);
      /* Add by charlie zhang 2007-9-20 end */
      Xtask tasks[] = configDB.GetTasks();
      //no active tasks
      if ( (tasks == null) || (tasks.length <= 0)) {
        return bReturn = true;
      }
      //schedule active tasks
      LoggingUtils.Log("Scheduling active tasks ", Level.DEBUG, Phrase.TaskLoggerName);
      for (int i = 0; i < tasks.length; i++) {
        if(operation!=null && operation.GetOperationStatus(tasks[i].getName())!=null)
          bReturn = this.schedule(scheduler, tasks[i]);  // schedule active task
        /* Add by charlie zhang 2007-9-20 begin */
        if(operation!=null && operation.GetOperationStatus(tasks[i].getName())!=null && operation.GetOperationStatus(tasks[i].getName()).equalsIgnoreCase(Phrase.EXECUTING_STATUS)){
          Operation tempOp = new Operation(operation.GetOperationID(tasks[i].getName()));
          tempOp.SetStatus(Phrase.RUNNING_STATUS);
          operation.SaveOperation(tempOp.GetOperationID(),tempOp.GetDescription(),tempOp.GetConfig(),tempOp.GetStatus(),tempOp.GetMessage());
          LoggingUtils.Log("ScheduleManager>>>scheduleTasks>>>Changed  " +tasks[i].getName()+" status from Executing to Running."
                          , Level.ERROR, Phrase.TaskLoggerName);
        }
        /* Add by charlie zhang 2007-9-20 end */
      }
      bReturn = true;
    }
    catch (Exception e) {
      LoggingUtils.Log("ScheduleManager>>>scheduleTask>>>Get error: " + e.toString()
                      , Level.ERROR, Phrase.TaskLoggerName);
      e.printStackTrace();
      bReturn = false;
      //throw e;
    }
    return bReturn;
  }

  /**
   * schedule task
   * @param scheduler
   * @param task
   * @return true is schedule added successfully, otherwise return false
   * @throws Exception
   */
  protected boolean schedule(TaskScheduler scheduler, Xtask task) throws Exception {
    LoggingUtils.Log("schedule: Scheduling Task " + task.getName(), Level.INFO
                     , Phrase.TaskLoggerName);
    boolean bReturn = false;
    String className = "";
    String classMethod = "";
    XserviceParm serviceParms[] = null;
    XreturnParm returnParm = null;
    String status = null;
    try {
      //get related information
      ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.TaskLoggerName);
      Xservice service = configDB.GetService(task.getService());
      if (service != null) {
        serviceParms = service.getServiceParms();
        classMethod = service.getMethod();
        XjavaClass javaClass = configDB.GetJavaClass(service.getService());
        if (javaClass != null) {
          className = javaClass.getClassName();
        }
        returnParm = service.getReturnParm();
      }
      //get schedule
      Xschedule schedule = configDB.GetSchedule(task.getSchedule());
      if (schedule == null) {
        throw new Exception("Task schedule [" + task.getSchedule() + "] is not defined.");
      }
      LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task schedule.getStartDateTime is: " + schedule.getStartDateTime(), Level.DEBUG
                       , Phrase.TaskLoggerName);
      LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task schedule.getEndDateTime() is: " + schedule.getEndDateTime(), Level.DEBUG
                       , Phrase.TaskLoggerName);
      LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task schedule.getInterval is: " + schedule.getInterval(), Level.DEBUG
                       , Phrase.TaskLoggerName);
      LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task schedule.getType is: " + schedule.getType(), Level.DEBUG
                       , Phrase.TaskLoggerName);
      LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task schedule.calculateInterval is: " + schedule.calculateInterval(), Level.DEBUG
                       , Phrase.TaskLoggerName);

      //schedule
      if (!className.equalsIgnoreCase("") && !classMethod.equalsIgnoreCase("")) {
        RunnableTask newTask = new RunnableTask(schedule);
        newTask.setClassName(className);
        newTask.setClassMethod(classMethod);
        newTask.setMethodParms(serviceParms);
        newTask.setName(task.getName());
        newTask.setReturnParm(returnParm);
        // debug
        /*
         java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                         Calendar cal = new GregorianCalendar();
                         System.out.println("*-*-*-*-*");
                         System.out.println("schedule task: " + className + ", " +
                           classMethod + ", " + newTask.getName() + ", " +
                           schedule.calculateInterval() + ", " + sdf.format(cal.getTime()));
                         System.out.println("*-*-*-*-*");
                         sdf = null;
                         cal = null;
         */
        // end debug
        /* changed by charlie zhang 2007-9-14 begin */
        if (newTask.validate()) {
          INodeOperation opDB = DBManager.GetNodeOperation(Phrase.TaskLoggerName);
          INodeOperationLog opLogDB = DBManager.GetNodeOperationLog(Phrase.TaskLoggerName);
          Calendar c = opDB.GetLastStartTime(newTask.getName());
          Calendar accurateC = opLogDB.GetAccurateLastStartTime(opDB.GetOperationID(newTask.getName()));
          long systemTimeMillis = System.currentTimeMillis();
          if (c != null) {
            newTask.updateLastRunTime(c.getTimeInMillis());
          }
          LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task LastStartTime is: " + accurateC.getTime(), Level.DEBUG
                           , Phrase.TaskLoggerName);
          LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task system time is: " + new Date(systemTimeMillis), Level.DEBUG
                           , Phrase.TaskLoggerName);
          LoggingUtils.Log("SheduleManager>>>schedule>>>Leaving internal is: " + (systemTimeMillis-accurateC.getTimeInMillis()), Level.DEBUG
                           , Phrase.TaskLoggerName);
          if(schedule.getType()!= null && schedule.getType().equalsIgnoreCase("once")){
            newTask.setSchedule(schedule.getStartDateTimeInMillis(), 0);
            LoggingUtils.Log("SheduleManager>>>schedule>>>new once Task first time is: " + new Date(systemTimeMillis), Level.DEBUG
                             , Phrase.TaskLoggerName);
          }else if(schedule.getStartDateTimeInMillis() > accurateC.getTimeInMillis()){
            newTask.setSchedule(schedule.getStartDateTimeInMillis(), schedule.calculateInterval());
            LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task first time is: " + new Date(schedule.getStartDateTimeInMillis()), Level.DEBUG
                               , Phrase.TaskLoggerName);
          }else if(systemTimeMillis-accurateC.getTimeInMillis() >= schedule.calculateInterval()){
            newTask.setSchedule(systemTimeMillis, schedule.calculateInterval());
            LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task first time is: " + new Date(systemTimeMillis), Level.DEBUG
                             , Phrase.TaskLoggerName);
          }else if(systemTimeMillis-accurateC.getTimeInMillis() < schedule.calculateInterval()){
            newTask.setSchedule(accurateC.getTimeInMillis()+schedule.calculateInterval(), schedule.calculateInterval());
            LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task first time is: " + new Date(systemTimeMillis), Level.DEBUG
                               , Phrase.TaskLoggerName);
          }/*else if(opDB!=null && opDB.GetOperationStatus(task.getName())!=null && opDB.GetOperationStatus(task.getName()).equalsIgnoreCase(Phrase.EXECUTING_STATUS)){
            newTask.setSchedule(systemTimeMillis+schedule.calculateInterval()-(systemTimeMillis-accurateC.getTimeInMillis()), schedule.calculateInterval());
            LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task first time is: " + new Date((systemTimeMillis+schedule.calculateInterval()-(systemTimeMillis-accurateC.getTimeInMillis()))), Level.DEBUG
                        	, Phrase.TaskLoggerName);        	  
          }*/
          if (task.getStatus().length() > 2) {
            status = task.getStatus().substring(0, task.getStatus().indexOf("_"));
          }
          else {
            status = task.getStatus();
          }
          LoggingUtils.Log("SheduleManager>>>schedule>>>new scheduling Task status is: " + status, Level.DEBUG
                           , Phrase.TaskLoggerName);
          if (status.equalsIgnoreCase("A")) {
            NodeUtils nodeUtils = new NodeUtils();
            newTask.setTaskDBID(nodeUtils.GenerateTransID(Phrase.UUID));
            scheduler.schedule(newTask);
          }
          /* changed by charlie zhang 2007-9-14 end */
          bReturn = true;
        }
        else {
          throw new Exception("Invalid Task");
        }
      }
    }
    catch (Exception e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      sw.close();
      String message = sw.toString();
      if (message != null && message.length() > 4000) {
        message = message.substring(0, 4000);

      }
      NodeUtils utils = new NodeUtils();
      String[] paramNames = null;
      String[] paramValues = null;
      if (serviceParms != null && serviceParms.length > 0) {
        paramNames = new String[serviceParms.length];
        paramValues = new String[serviceParms.length];
        for (int i = 0; i < serviceParms.length; i++) {
          paramNames[i] = serviceParms[i].getName();
          paramValues[i] = serviceParms[i].getValue();
        }
      }
      Operation op = new Operation(task.getName(), null, Phrase.TaskLoggerName);
      int opLogID = utils.CreateOperationLog(Phrase.TaskLoggerName, op.GetOperationID(), "task"
                                             , utils.GenerateTransID(Phrase.UUID), "Error", message, null, null,
                                             null, null, null, null
                                             , InetAddress.getLocalHost().getHostName(), paramNames
                                             , paramValues);
      utils.UpdateOperationLog(Phrase.TaskLoggerName, opLogID, "ERROR"
                               , "Task Stopped after Failure", true);
    }
    return bReturn;
  }

  //  public static void main( String[] argv )
  //   {
  /* test date time functions */
  //get Millis
  /*
           try
           {
      System.out.println(Calendar.MONDAY);
      Date d = new Date();
      System.out.println(d.getTime());
      //using formater
      SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd H:mm:ss");
      System.out.println(df.format(d));
      Calendar c = Calendar.getInstance();
      c.setTime(d);
      System.out.println(c.getActualMaximum(c.DAY_OF_MONTH)+"");
      String dString = "5/4/2003";
      SimpleDateFormat readF = new SimpleDateFormat("MM/dd/yyyy");
      d = readF.parse(dString);
      System.out.println(df.format(d));
           }
           catch(Exception e)
           {
      e.printStackTrace(System.out);
           }
   */
  /* test date time functions */
  /*
          boolean initOK = false;
          try
          {
              ScheduleManager sm = new ScheduleManager();
              String from = ApplicationWatch.getProperty("resource.type");
              if(from.equalsIgnoreCase("FILE"))
                  initOK = sm.init(ApplicationWatch.getProperty("task.config"));
              else if(from.equalsIgnoreCase("DB"))
              {
                  String dbType = ApplicationWatch.getProperty("DB.type");
                  String dbUrl = ApplicationWatch.getProperty("DB.Source");
                  String dbUser = ApplicationWatch.getProperty("DB.User");
                  String dbPassword = ApplicationWatch.getProperty("DB.Password");
                  initOK = sm.init(dbType, dbUrl, dbUser, dbPassword);
              }
              if(initOK)
              {
                  if(sm.scheduleTasks())
                  {
                      System.out.println("Initialize Node.Task application successfully.");
                      //new scheduler to schedule updateTask
                      TaskScheduler scheduler = new TaskScheduler();
                      UpdateTask uTask = new UpdateTask();
                      uTask.setSchedule(System.currentTimeMillis(), 30*1000);
                      scheduler.schedule(uTask);
                  }
                  else
                      System.out.println("Initialize Node.Task application end with error.");
              }
          }
          catch(Exception e)
          {
              System.out.println("Initialize Node.Task application throws exception as below.");
              e.printStackTrace(System.out);
          }

      }
   */

}
