package Node.Utils;

import org.apache.log4j.Level;
import org.apache.log4j.Logger;
/**
 * <p>This class create LoggingUtils.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LoggingUtils {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  private LoggingUtils() {
  }

  /**
   * Log
   * @param message
   * @param level
   * @param loggerName
   * @return 
   */
  public static void Log (String message, Level level, String loggerName)
  {
    Logger logger = Logger.getLogger(loggerName);
    if (level.equals(Level.FATAL))
      logger.fatal(message);
    if (level.equals(Level.ERROR))
      logger.error(message);
    if (level.equals(Level.WARN))
      logger.warn(message);
    if (level.equals(Level.INFO))
      logger.info(message);
    if (level.equals(Level.DEBUG))
      logger.debug(message);
  }

  /**
   * ParseLevel
   * @param input
   * @return Level
   */
  public static Level ParseLevel (String input)
  {
    if (input.equalsIgnoreCase(Level.FATAL.toString()))
      return Level.FATAL;
    if (input.equalsIgnoreCase(Level.ERROR.toString()))
      return Level.ERROR;
    if (input.equalsIgnoreCase(Level.WARN.toString()))
      return Level.WARN;
    if (input.equalsIgnoreCase(Level.INFO.toString()))
      return Level.INFO;
    else
      return Level.DEBUG;
  }
}
