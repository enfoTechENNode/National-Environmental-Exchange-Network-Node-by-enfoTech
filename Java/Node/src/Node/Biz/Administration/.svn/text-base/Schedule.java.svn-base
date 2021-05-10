package Node.Biz.Administration;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.GregorianCalendar;
/**
 * <p>This class create Schedule class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Schedule {
  public String TYPE_INACTIVE = "INACTIVE";
  public String TYPE_SECONDS = "SECONDS";
  public String TYPE_DAYS = "DAYS";
  public String TYPE_WEEKS = "WEEKLY";
  public String TYPE_MONTHS = "MONTHLY";
  public String TYPE_YEARS = "YEARLY";
  public String TYPE_ONCE = "ONCE";

  private String Type = null;
  private Date StartDate = null;
  private Date EndDate = null;
  private String Interval = null;
  private String Time = null;
  private String[] DayOfWeek = null;
  private String[] DayOfMonth = null;
  private String[] MonthOfYear = null;

  /**
   * Constructor.
   * @param type .
   * @return 
   */
  public Schedule(String type) {
    if (type.equalsIgnoreCase(this.TYPE_INACTIVE))
      this.Type = this.TYPE_INACTIVE;
    if (type.equalsIgnoreCase(this.TYPE_SECONDS))
      this.Type = this.TYPE_SECONDS;
    if (type.equalsIgnoreCase(this.TYPE_DAYS))
      this.Type = this.TYPE_DAYS;
    if (type.equalsIgnoreCase(this.TYPE_WEEKS))
      this.Type = this.TYPE_WEEKS;
    if (type.equalsIgnoreCase(this.TYPE_MONTHS))
      this.Type = this.TYPE_MONTHS;
    if (type.equalsIgnoreCase(this.TYPE_YEARS))
      this.Type = this.TYPE_YEARS;
    if (type.equalsIgnoreCase(this.TYPE_ONCE))
      this.Type = this.TYPE_ONCE;
  }

  public String GetType ()
  {
    return this.Type;
  }

  public void SetStartDate (Date input)
  {
    this.StartDate = input;
    if (this.Type != null && this.StartDate != null) {
      if (this.Type.equals(this.TYPE_WEEKS) || this.Type.equals(this.TYPE_MONTHS) || this.Type.equals(this.TYPE_YEARS) || this.Type.equals(this.TYPE_ONCE)) {
        SimpleDateFormat format = new SimpleDateFormat("HH:mm");
        this.SetTime(format.format(this.StartDate));
      }
      if (this.Type.equals(this.TYPE_ONCE)) {
        GregorianCalendar cal = new GregorianCalendar();
        cal.setTime(this.StartDate);
        cal.add(GregorianCalendar.MONTH,6);
        this.EndDate = cal.getTime();
        SimpleDateFormat dateFormat = new SimpleDateFormat("dd");
        this.DayOfMonth = new String[1];
        this.DayOfMonth[0] = dateFormat.format(this.StartDate);
        if (this.DayOfMonth[0].startsWith("0") && this.DayOfMonth[0].length() == 2)
          this.DayOfMonth[0] = this.DayOfMonth[0].substring(1);
        SimpleDateFormat monthFormat = new SimpleDateFormat("MM");
        this.MonthOfYear = new String[1];
        this.MonthOfYear[0] = monthFormat.format(this.StartDate);
        if (this.MonthOfYear[0].startsWith("0") && this.MonthOfYear[0].length() == 2)
          this.MonthOfYear[0] = this.MonthOfYear[0].substring(1);
      }
    }
  }
  public Date GetStartDate ()
  {
    return this.StartDate;
  }

  public void SetEndDate (Date input)
  {
    this.EndDate = input;
  }
  public Date GetEndDate ()
  {
    return this.EndDate;
  }

  public void SetInterval (String input)
  {
    this.Interval = input;
  }
  public String GetInterval ()
  {
    return this.Interval;
  }

  public void SetTime (String time)
  {
    this.Time = time;
  }
  public String GetTime ()
  {
    return this.Time;
  }

  public void SetDayOfWeek (String[] input)
  {
    this.DayOfWeek = input;
  }
  public String[] GetDayOfWeek ()
  {
    return this.DayOfWeek;
  }

  public void SetDayOfMonth (String[] input)
  {
    this.DayOfMonth = input;
  }
  public String[] GetDayOfMonth ()
  {
    return this.DayOfMonth;
  }

  public void SetMonthOfYear (String[] input)
  {
    this.MonthOfYear = input;
  }
  public String[] GetMonthOfYear ()
  {
    return this.MonthOfYear;
  }

  /**
   * Validate Schedule.
   * @return success
   */
  public boolean ValidateSchedule ()
  {
    boolean retBool = false;
    if (this.Type != null) {
      if (this.Type.equals(this.TYPE_INACTIVE))
        retBool = true;
      else {
        if (this.StartDate != null && this.EndDate != null) {
          if (this.Type.equals(this.TYPE_SECONDS) || this.Type.equals(this.TYPE_DAYS)) {
            if (this.Interval != null)
              retBool = true;
          }
          else if (this.Type.equals(this.TYPE_WEEKS)) {
            if (this.DayOfWeek != null && this.DayOfWeek.length > 0)
              retBool = true;
          }
          else {
            if (this.DayOfMonth != null && this.DayOfMonth.length > 0) {
              if (this.Type.equals(this.TYPE_MONTHS))
                retBool = true;
              else if (this.MonthOfYear != null && this.MonthOfYear.length > 0)
                  retBool = true;
            }
          }
        }
      }
    }
    return retBool;
  }
}
