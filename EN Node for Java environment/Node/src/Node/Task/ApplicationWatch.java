package Node.Task;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.apache.log4j.DailyRollingFileAppender;
import org.apache.log4j.FileAppender;
import org.apache.log4j.Layout;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.PatternLayout;

import EnfoTech.Task.TaskScheduler;
import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.AppUtils;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.jndi.JNDIAccess;
/**
 * <p>This class create ApplicationWatch.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ApplicationWatch
    implements ServletContextListener {
	  /**
	   * Constructor
	   * @param 
	   * @return
	   */
  public ApplicationWatch() {
  }

  //application startup
  /**
   * contextInitialized
   * @param e
   * @return
   */
  public void contextInitialized(ServletContextEvent e) {
    try {
      ServletContext application = null;
      application = e.getServletContext();
      String appRoot = application.getRealPath("/");
      AppUtils.TaskRoot = appRoot;
      boolean dbconFlag = true;

      this.SetLogLevel();
      //initial task
      ScheduleManager sm = new ScheduleManager();
      /* if start tasks fail then loop here to wait
       do {
         if(sm.scheduleTasks()){
           dbconFlag=true;
         }else{
           LoggingUtils.Log("Could Not Schedule Tasks: " + e.toString(), Level.ERROR
                            , Phrase.TaskLoggerName);
           dbconFlag=false;
         }
         //Pause for 30 seconds
         Thread.sleep(30*1000);
       } while(!dbconFlag);
         //new scheduler to schedule updateTask
         //TaskScheduler scheduler = new TaskScheduler();
         TaskScheduler scheduler = TaskScheduler.getInstance();

         UpdateTask uTask = new UpdateTask();
         uTask.setSchedule(System.currentTimeMillis(), 30 * 1000);
         scheduler.schedule(uTask);*/
      /* Start all tasks without waiting */
      if (sm.scheduleTasks()) {
        //new scheduler to schedule updateTask
        //TaskScheduler scheduler = new TaskScheduler();
        TaskScheduler scheduler = TaskScheduler.getInstance();

        UpdateTask uTask = new UpdateTask();
        uTask.setSchedule(System.currentTimeMillis(), 30 * 1000);
        scheduler.schedule(uTask);
      }
      else {
        LoggingUtils.Log("Could Not Schedule Tasks: " + e.toString(), Level.WARN
                         , Phrase.TaskLoggerName);
      }

    }
    catch (Exception ex) {
      LoggingUtils.Log("Could Not Initialize Task Application: " + ex.getMessage(), Level.ERROR
                       , Phrase.TaskLoggerName);
    }
  }

  //application shutdown
  /**
   * contextDestroyed
   * @param ce
   * @return
   */
  public void contextDestroyed(ServletContextEvent ce) {
    try {
      //save task
      TaskScheduler scheduler = TaskScheduler.getInstance();
      scheduler.shutdown();
    }
    catch (Exception e) {
      LoggingUtils.Log("Could Not Shut Down Task Application: " + e.toString(), Level.ERROR
                       , Phrase.TaskLoggerName);
    }
  }

  /**
   * SetLogLevel
   * @param 
   * @return
   */
  private void SetLogLevel() {
    //BasicConfigurator.configure();

    // Set Initial Log
    Logger logger = Logger.getLogger(Phrase.TaskLoggerName);
    Layout layout = new PatternLayout("%-15d [%-5p]: %m\n\r");
    String temp = (String) JNDIAccess.GetJNDIValue(Phrase.JNDITaskLogLocation, false)
        + "/NodeTaskLog.txt";
    String datePattern = "'.'yyyy-MM-dd";
    // Get Configuration - Set Log Level
    try {
      DailyRollingFileAppender appender = new DailyRollingFileAppender(layout, temp, datePattern);
      logger.addAppender(appender);
      logger.setLevel(Level.DEBUG);

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
