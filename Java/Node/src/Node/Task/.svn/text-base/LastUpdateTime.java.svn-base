package Node.Task;

import java.util.Date;
/**
 * <p>This class create LastUpdateTime.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class LastUpdateTime {
  private static LastUpdateTime LastUpdatedTime = new LastUpdateTime();
  private long Time = 0;

   /**
   * Constructor
   * @param 
   * @return 
   */
  private LastUpdateTime() {
    this.Time = 0;
  }

  /**
   * LastUpdateTime
   * @param 
   * @return LastUpdatedTime
   */
  public static LastUpdateTime GetInstant()
  {
    return LastUpdateTime.LastUpdatedTime;
  }

  /**
   * LastUpdateTime
   * @param 
   * @return long
   */
  public long GetLastUpdatedTime ()
  {
    return this.Time;
  }

  /**
   * SetLastUpdatedTime
   * @param 
   * @return 
   */
  public void SetLastUpdatedTime ()
  {
    Date temp = new Date();
    this.Time = temp.getTime();
  }
}
