package Node.WebServices;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;
import org.apache.log4j.BasicConfigurator;
import org.apache.log4j.DailyRollingFileAppender;
import org.apache.log4j.PatternLayout;
import org.apache.log4j.FileAppender;
import org.apache.log4j.Layout;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Phrase;
import Node.Utils.AppUtils;
import Node.Utils.Utility;

public class ApplicationWatch implements ServletContextListener {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public ApplicationWatch() {
  }

  /**
   * contextInitialized
   * @param event
   * @return 
   */
  public void contextInitialized (ServletContextEvent event)
  {
    try {
      this.SetLogLevel();
      ServletContext context = event.getServletContext();
      context.setAttribute(Phrase.LoggerSessionKey, Phrase.WebServicesLoggerName);
      AppUtils.WebServicesRoot = context.getRealPath("/");
      String path = Utility.GetTempFilePath();
      Utility.newFolder(path+"/temp");
      Utility.delAllFile(path+"/temp");
    } catch (Exception e) { }
  }

  /**
   * contextDestroyed
   * @param event
   * @return 
   */
  public void contextDestroyed (ServletContextEvent event)
  {

  }

  /**
   * SetLogLevel
   * @param 
   * @return 
   */
  private void SetLogLevel () throws Exception
  {
    //BasicConfigurator.configure();

    // Set Initial Log
    Logger logger = Logger.getLogger(Phrase.WebServicesLoggerName);
    Layout layout = new PatternLayout("%-15d [%-5p]: %m\n");
    String temp = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIWebServicesLogLocation, false)+"/NodeWebServicesLog.txt";
    String datePattern = "'.'yyyy-MM-dd";
    DailyRollingFileAppender appender = new DailyRollingFileAppender(layout, temp, datePattern);
    logger.addAppender(appender);
    logger.setLevel(Level.DEBUG);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.WebServicesLoggerName);
      if (config != null) {
        Level level = config.GetWebServicesLogLevel();
        if (level != null) {
          logger.setLevel(level);
          logger.info("Starting Web Services Application with Level " + logger.getLevel());
        }
//        else
//          logger.error("Could Not Find Web Services Log Level in system.config");
      }
//      else
//        logger.error("Could not get system.config from Configuration File");
    } catch (Exception e) {
//      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }
}
