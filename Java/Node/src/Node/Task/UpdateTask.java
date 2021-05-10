package Node.Task;

import EnfoTech.Task.Task;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import java.util.*;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.Phrase;
import Node.Utils.LoggingUtils;
import EnfoTech.Task.TaskScheduler;
/**
 * <p>This class create UpdateTask.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class UpdateTask
    extends Task {
  private String status = RunnableTask.STATUS_INITIAL;
  //private TaskLogWriter logWriter = null;

  public UpdateTask() {
  }

  /**
   * synchronized run task
   */
  public synchronized void run() {
    ScheduleManager sm = null;
    try {
      sm = new ScheduleManager();
      // debug

      java.text.SimpleDateFormat sdf = new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
      Calendar cal = new GregorianCalendar();

      LoggingUtils.Log("Update Task>>>updateTask runs: " + sdf.format(cal.getTime()), Level.DEBUG
                       , Phrase.TaskLoggerName);
      cal = null;

      // end debug

      /* Add by charlie zhang 1007-9-13 begin */
      /*if (this.IsConfigUpdated()) {
        LoggingUtils.Log("UpdateTask: Canceling Tasks",Level.INFO,Phrase.TaskLoggerName);
        sm.cancelTasks();
        sm.scheduleTasks();
        LoggingUtils.Log("UpdateTask: Scheduled Tasks",Level.INFO,Phrase.TaskLoggerName);
               }*/
      if (this.IsConfigUpdated()) {
        LoggingUtils.Log("UpdateTask>>> before go into scheduleTask", Level.DEBUG, Phrase.TaskLoggerName);
        sm.scheduleTask();
        LoggingUtils.Log("UpdateTask>>> after go into ScheduledTask", Level.DEBUG, Phrase.TaskLoggerName);
      }

      /* Add by charlie zhang 1007-9-13 end */

      // debug

      TaskScheduler scheduler = TaskScheduler.getInstance();
      Task[] tasks = scheduler.getRunningTasks();
      if (tasks != null) {
        for (int i = 0; i < tasks.length; i++) {
          LoggingUtils.Log("UpdateTask>>>  running tasks are: "+ tasks[i].getName() + ", "
                             + tasks[i].getStatus() +
                             ", " + sdf.format(new Date(tasks[i].getScheduledFirstTime())) + ", "
                             + tasks[i].getScheduledInterval() +
                             ", " + sdf.format(new Date(tasks[i].lastExecutionTime())), Level.DEBUG, Phrase.TaskLoggerName);
        }
      }
      else {
        LoggingUtils.Log("UpdateTask>>> no running task.", Level.DEBUG, Phrase.TaskLoggerName);
      }
      sdf = null;

      // end debug
      this.SetLoggerLevel();
    }
    catch (Exception e) {
      LoggingUtils.Log("Could Not Schedule Tasks: " + e.toString(), Level.ERROR
                       , Phrase.TaskLoggerName);
    }
  }

  /**
   * get runnable task status
   * @return
   */
  public String getStatus() {
    //synchronized( semaphore )
    //{
    return status;
    //}
  }

  /**
   * check if config has been changed
   * @return true if config last update date has changed otherwise return false
   */
  protected boolean IsConfigUpdated() {
    boolean retBool = false;
    LastUpdateTime time = LastUpdateTime.GetInstant();
    try {
      ITaskConfiguration configDB = DBManager.GetTaskConfiguration(Phrase.TaskLoggerName);
      if (time.GetLastUpdatedTime() < configDB.GetUpdatedDTTM()) {
        // debug
        LoggingUtils.Log("*** IsConfigUpdate==true(GetLastUpdatedTime/GetUpdatedDTTM):"
                         + time.GetLastUpdatedTime() + "/" + configDB.GetUpdatedDTTM() + " ***", Level.DEBUG
                       , Phrase.TaskLoggerName);
        // end debug
        time.SetLastUpdatedTime();
        retBool = true;
      }
    }
    catch (Exception e) {
      LoggingUtils.Log("Could Not Check Updated Time File: " + e.toString(), Level.ERROR
                       , Phrase.TaskLoggerName);
    }
    return retBool;
  }

  /**
   * SetLoggerLevel
   * @param 
   * @return 
   */
  private void SetLoggerLevel() {
    Logger logger = Logger.getLogger(Phrase.TaskLoggerName);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.TaskLoggerName);
      if (config != null) {
        Level level = config.GetTaskLogLevel();
        if (level != null) {
          logger.setLevel(level);
        }
        else {
          logger.error("Could Not Find Task Log Level in system.config");
        }
      }
      else {
        logger.error("Could not get system.config from Configuration File");
      }
    }
    catch (Exception e) {
      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }
}
