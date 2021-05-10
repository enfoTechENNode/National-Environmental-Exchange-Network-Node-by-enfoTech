package Node.Web.Administration;

import javax.servlet.ServletContext;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import org.apache.log4j.DailyRollingFileAppender;
import org.apache.log4j.FileAppender;
import org.apache.log4j.Layout;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.PatternLayout;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.AppUtils;
import Node.Utils.Utility;

import com.enfotech.basecomponent.jndi.JNDIAccess;
/**
 * <p>This class create ApplicationWatch.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

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
      context.setAttribute(Phrase.LoggerSessionKey, Phrase.AdministrationLoggerName);
      //AppUtils.AppRoot = context.getRealPath("/");
      AppUtils.setAppRoot(context.getRealPath("/"));
      String path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIAdministrationLogLocation, false);
      String tmpFilepath = Utility.GetTempFilePath();
      Utility.newFolder(path+"/Operations");
      Utility.newFolder(path+"/temp");
      Utility.newFolder(tmpFilepath+"/temp");
      Utility.delAllFile(path+"/temp");
      if(!tmpFilepath.equalsIgnoreCase(path)){
    	  Utility.delAllFile(tmpFilepath);
      }
      Utility.delAllFile(tmpFilepath+"/temp");
    } catch (Exception e) {
      e.printStackTrace();
    }
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
    Logger logger = Logger.getLogger(Phrase.AdministrationLoggerName);
    Layout layout = new PatternLayout("%-15d [%-5p]: %m\n\r");
    String temp = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIAdministrationLogLocation, false)+"/NodeAdministrationLog.txt";
    String datePattern = "'.'yyyy-MM-dd";
    DailyRollingFileAppender appender = new DailyRollingFileAppender(layout, temp, datePattern);
    logger.addAppender(appender);
    logger.setLevel(Level.DEBUG);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
      if (config != null) {
        Level level = config.GetAdministrationLogLevel();
        if (level != null)
          logger.setLevel(level);
//        else
//          logger.error("Could Not Find Administration Log Level in system.config");
      }
//      else
//        logger.error("Could not get system.config from Configuration File");
    } catch (Exception e) {
//      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }
}
