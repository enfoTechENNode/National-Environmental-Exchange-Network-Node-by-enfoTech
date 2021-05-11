using System;
using System.Collections;
using System.Xml;
using NodeLib = Node.Lib.AppSystem;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// TaskSchedule is used to stored task schedule information.
    /// </summary>
    public class TaskSchedule
    {
        #region Public Fields
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_ONCE.
        /// </summary>
        public const int SCHEDULE_TYPE_ONCE = 0;
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_MINUTES.
        /// </summary>
        public const int SCHEDULE_TYPE_MINUTES = 1;
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_DAILY.
        /// </summary>
        public const int SCHEDULE_TYPE_DAILY = 2;
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_WEEKLY.
        /// </summary>
        public const int SCHEDULE_TYPE_WEEKLY = 3;
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_MONTHLY_DAYS.
        /// </summary>
        public const int SCHEDULE_TYPE_MONTHLY_DAYS = 4;
        /// <summary>
        /// Constant value for SCHEDULE_TYPE_MONTHLY_WEEKS.
        /// </summary>
        public const int SCHEDULE_TYPE_MONTHLY_WEEKS = 5;

        #endregion

        #region Public Properties
        /// <summary>
        /// status of task schedule.
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set
            {
                string input = value;
                if (value == null || !value.Equals("A"))
                    input = "I";
                this.status = input;
            }
        }
        /// <summary>
        /// Start Date of task schedule.
        /// </summary>
        public DateTime StartDate
        {
            get { return this.startDate; }
            set { this.startDate = value; }
        }
       /// <summary>
       /// End Date of task scheuled.
       /// </summary>
        public DateTime EndDate
        {
            get { return this.endDate; }
            set { this.endDate = value; }
        }
        /// <summary>
        /// Type of task schedule.
        /// </summary>
        public int Type
        {
            get { return this.type; }
            set
            {
                switch (value)
                {
                    case TaskSchedule.SCHEDULE_TYPE_ONCE:
                        this.type = value;
                        break;
                    case TaskSchedule.SCHEDULE_TYPE_DAILY:
                        this.type = value;
                        break;
                    case TaskSchedule.SCHEDULE_TYPE_MINUTES:
                        this.type = value;
                        break;
                    case TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS:
                        this.type = value;
                        break;
                    case TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS:
                        this.type = value;
                        break;
                    case TaskSchedule.SCHEDULE_TYPE_WEEKLY:
                        this.type = value;
                        break;
                }
            }
        }
        /// <summary>
        /// Interval Minutes of task schedule.
        /// </summary>
        public int IntervalMinutes
        {
            get { return this.intervalMinutes; }
            set { this.intervalMinutes = value; }
        }
        /// <summary>
        /// Interval Days of task schedule.
        /// </summary>
        public int IntervalDays
        {
            get { return this.intervalDays; }
            set { this.intervalDays = value; }
        }
        /// <summary>
        /// Interval Weeks of task schedule.
        /// </summary>
        public int IntervalWeeks
        {
            get { return this.intervalWeeks; }
            set { this.intervalWeeks = value;
            }
        }
        /// <summary>
        /// Week of Month of task schedule.
        /// </summary>
        public int WeekOfMonth
        {
            get { return this.weekOfMonth; }
            set
            {
                if (value >= 0 && value <= 4)
                    this.weekOfMonth = value;
                else
                    this.weekOfMonth = 5;
            }
        }
        /// <summary>
        /// Days of week of task schedule.
        /// </summary>
        public string DaysOfWeek
        {
            get
            {
                string ret = "";
                for (int i = 0; i < this.daysOfWeek.Count; i++)
                {
                    if (i != 0) ret += ",";
                    ret += this.daysOfWeek[i].ToString();
                }
                return ret;
            }
        }
        /// <summary>
        /// Days of Month of task schedule.
        /// </summary>
        public string DaysOfMonth
        {
            get
            {
                string ret = "";
                for (int i = 0; i < this.daysOfMonth.Count; i++)
                {
                    if (i != 0) ret += ",";
                    ret += this.daysOfMonth[i].ToString();
                }
                return ret;
            }
        }
        /// <summary>
        /// Month of Year of task schedule.
        /// </summary>
        public string MonthsOfYear
        {
            get
            {
                string ret = "";
                for (int i = 0; i < this.monthsOfYear.Count; i++)
                {
                    if (i != 0) ret += ",";
                    ret += this.monthsOfYear[i].ToString();
                }
                return ret;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of TaskSchedule.
        /// </summary>
        /// <param name="status">task schedule status.</param>
        /// <param name="start">task schedule start date.</param>
        /// <param name="end">task schedule end date.</param>
        /// <param name="type">task schedule type.</param>
        /// <param name="intervalMinutes">task schedule interval in minutes.</param>
        /// <param name="intervalDays">task schedule interval in days.</param>
        /// <param name="intervalWeeks">task schedule interval in weeks.</param>
        /// <param name="daysOfWeek">task schedule in days of weeks.</param>
        /// <param name="daysOfMonth">task schedule in days of months.</param>
        /// <param name="weekOfMonth">task schedule in week of months.</param>
        /// <param name="monthsOfYear">task schedule in month of years.</param>
        public TaskSchedule(string status, DateTime start, DateTime end, int type, int intervalMinutes, int intervalDays, int intervalWeeks, string daysOfWeek, string daysOfMonth, int weekOfMonth, string monthsOfYear)
        {
            this.Status = status;
            this.Type = type;
            switch (this.Type)
            {
                case TaskSchedule.SCHEDULE_TYPE_ONCE:
                    this.IntervalMinutes = 2;
                    break;
                case TaskSchedule.SCHEDULE_TYPE_MINUTES:
                    this.IntervalMinutes = intervalMinutes;
                    break;
                case TaskSchedule.SCHEDULE_TYPE_DAILY:
                    this.IntervalDays = intervalDays;
                    break;
                case TaskSchedule.SCHEDULE_TYPE_WEEKLY:
                    this.IntervalWeeks = intervalWeeks;
                    if (daysOfWeek != null)
                    {
                        string[] split = daysOfWeek.Split(new char[] { ',' });
                        foreach (string s in split)
                            this.AddDayOfWeek(s);
                    }
                    break;
                case TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS:
                    if (daysOfMonth != null)
                    {
                        string[] split = daysOfMonth.Split(new char[] { ',' });
                        foreach (string s in split)
                            this.AddDayOfMonth(s);
                    }
                    if (monthsOfYear != null)
                    {
                        string[] split = monthsOfYear.Split(new char[] { ',' });
                        foreach (string s in split)
                            this.AddMonthOfYear(s);
                    }
                    break;
                case TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS:
                    this.WeekOfMonth = weekOfMonth;
                    if (daysOfWeek != null)
                    {
                        string[] split = daysOfWeek.Split(new char[] { ',' });
                        foreach (string s in split)
                            this.AddDayOfWeek(s);
                    }
                    if (monthsOfYear != null)
                    {
                        string[] split = monthsOfYear.Split(new char[] { ',' });
                        foreach (string s in split)
                            this.AddMonthOfYear(s);
                    }
                    break;
                default:
                    throw new Exception("Invalid Schedule Type: " + type);
            }
            this.StartDate = start;
            this.EndDate = end;
        }
        /// <summary>
        /// Constructor of TaskSchedule.
        /// </summary>
        /// <param name="schedule"></param>
        public TaskSchedule(NodeLib.TaskSchedule schedule)
        {
            this.Status = schedule.Status;
            if (schedule.IsDailySchedule && schedule.IsDailyMinutes)
            {
                this.Type = TaskSchedule.SCHEDULE_TYPE_MINUTES;
                this.IntervalMinutes = int.Parse(schedule.DailyMinutes);
            }
            else if (schedule.IsDailySchedule && schedule.IsDailyDays)
            {
                this.Type = TaskSchedule.SCHEDULE_TYPE_DAILY;
                this.IntervalDays = int.Parse(schedule.DailyDays);
            }
            else if (schedule.IsWeeklySchedule)
            {
                this.Type = TaskSchedule.SCHEDULE_TYPE_WEEKLY;
                this.IntervalWeeks = int.Parse(schedule.WeeklyWeeks);
                if (schedule.WeeklyDayOfWeek != null)
                {
                    string[] split = schedule.WeeklyDayOfWeek.Split(new char[] { ',' });
                    foreach (string s in split)
                        this.AddDayOfWeek(s);
                }
            }
            else if (schedule.IsMonthlySchedule && schedule.IsMonthlyDays)
            {
                this.Type = TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS;
                if (schedule.MonthlyDays != null)
                {
                    string[] split = schedule.MonthlyDays.Split(new char[] { ',' });
                    foreach (string s in split)
                        this.AddDayOfMonth(s);
                }
                if (schedule.MonthlyMonthOfYear != null)
                {
                    string[] split = schedule.MonthlyMonthOfYear.Split(new char[] { ',' });
                    foreach (string s in split)
                        this.AddMonthOfYear(s);
                }
            }
            else if (schedule.IsMonthlySchedule && schedule.IsMonthlyWeekdays)
            {
                this.Type = TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS;
                this.WeekOfMonth = int.Parse(schedule.MonthlyWeekOfMonth);
                if (schedule.WeeklyDayOfWeek != null)
                {
                    string[] split = schedule.WeeklyDayOfWeek.Split(new char[] { ',' });
                    foreach (string s in split)
                        this.AddDayOfWeek(s);
                }
                if (schedule.MonthlyMonthOfYear != null)
                {
                    string[] split = schedule.MonthlyMonthOfYear.Split(new char[] { ',' });
                    foreach (string s in split)
                        this.AddMonthOfYear(s);
                }
            }
            this.StartDate = DateTime.Parse(schedule.StartDateTime);
            this.EndDate = DateTime.Parse(schedule.EndDateTime);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">Pass in XML file with task schedule information</param>
        /// <returns>Parsing xml in XmlElement</returns>
        public XmlElement GetScheduleNode(XmlDocument config)
        {
            XmlElement scheduleElement = config.CreateElement("Schedule");
            scheduleElement.Attributes.Append(config.CreateAttribute("status"));
            scheduleElement.Attributes["status"].Value = this.Status;

            XmlElement startTimeElement = config.CreateElement("StartDateTime");
            startTimeElement.InnerText = this.StartDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            scheduleElement.AppendChild(startTimeElement);

            XmlElement endTimeElement = config.CreateElement("EndDateTime");
            endTimeElement.InnerText = this.EndDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            scheduleElement.AppendChild(endTimeElement);

            XmlElement dailyScheduleElement = config.CreateElement("DailySchedule");
            dailyScheduleElement.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_ONCE || this.Type == TaskSchedule.SCHEDULE_TYPE_MINUTES || this.Type == TaskSchedule.SCHEDULE_TYPE_DAILY)
                dailyScheduleElement.Attributes["status"].Value = "A";
            else
                dailyScheduleElement.Attributes["status"].Value = "I";

            XmlElement intervalMinutesElement = config.CreateElement("IntervalMinutes");
            intervalMinutesElement.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_MINUTES || this.Type == TaskSchedule.SCHEDULE_TYPE_ONCE)
            {
                intervalMinutesElement.Attributes["status"].Value = "A";
                intervalMinutesElement.InnerText = "" + this.IntervalMinutes;
            }
            else
                intervalMinutesElement.Attributes["status"].Value = "I";
            dailyScheduleElement.AppendChild(intervalMinutesElement);

            XmlElement intervalDaysElement = config.CreateElement("IntervalDays");
            intervalDaysElement.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_DAILY)
            {
                intervalDaysElement.Attributes["status"].Value = "A";
                intervalDaysElement.InnerText = "" + this.IntervalDays;
            }
            else
                intervalDaysElement.Attributes["status"].Value = "I";
            dailyScheduleElement.AppendChild(intervalDaysElement);

            scheduleElement.AppendChild(dailyScheduleElement);

            XmlElement weeklyScheduleElement = config.CreateElement("WeeklySchedule");
            weeklyScheduleElement.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_WEEKLY)
                weeklyScheduleElement.Attributes["status"].Value = "A";
            else
                weeklyScheduleElement.Attributes["status"].Value = "I";

            XmlElement intervalWeeksElement = config.CreateElement("IntervalWeeks");
            XmlElement dayOfWeekElement = config.CreateElement("DayOfWeek");
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_WEEKLY)
            {
                intervalWeeksElement.InnerText = "" + this.IntervalWeeks;
                dayOfWeekElement.InnerText = this.DaysOfWeek;
            }
            weeklyScheduleElement.AppendChild(intervalWeeksElement);
            weeklyScheduleElement.AppendChild(dayOfWeekElement);

            scheduleElement.AppendChild(weeklyScheduleElement);

            XmlElement monthlyScheduleElement = config.CreateElement("MonthlySchedule");
            monthlyScheduleElement.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS || this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS)
                monthlyScheduleElement.Attributes["status"].Value = "A";
            else
                monthlyScheduleElement.Attributes["status"].Value = "I";

            XmlElement intervalDaysElement2 = config.CreateElement("IntervalDays");
            XmlElement dayOfMonthElement = config.CreateElement("DayOfMonth");
            intervalDaysElement2.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS)
            {
                intervalDaysElement2.Attributes["status"].Value = "A";
                dayOfMonthElement.InnerText = this.DaysOfMonth;
            }
            else
                intervalDaysElement2.Attributes["status"].Value = "I";
            intervalDaysElement2.AppendChild(dayOfMonthElement);
            monthlyScheduleElement.AppendChild(intervalDaysElement2);

            XmlElement intervalWeeksElement2 = config.CreateElement("IntervalWeeks");
            XmlElement weekOfMonthElement = config.CreateElement("WeekOfMonth");
            XmlElement dayOfWeekElement2 = config.CreateElement("DayOfWeek");
            intervalWeeksElement2.Attributes.Append(config.CreateAttribute("status"));
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS)
            {
                intervalWeeksElement2.Attributes["status"].Value = "A";
                weekOfMonthElement.InnerText = "" + this.WeekOfMonth;
                dayOfWeekElement2.InnerText = this.DaysOfWeek;
            }
            else
                intervalWeeksElement2.Attributes["status"].Value = "I";
            intervalWeeksElement2.AppendChild(weekOfMonthElement);
            intervalWeeksElement2.AppendChild(dayOfWeekElement2);
            monthlyScheduleElement.AppendChild(intervalWeeksElement2);

            XmlElement monthOfYearElement = config.CreateElement("MonthOfYear");
            if (this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS || this.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS)
                monthOfYearElement.InnerText = this.MonthsOfYear;
            monthlyScheduleElement.AppendChild(monthOfYearElement);

            scheduleElement.AppendChild(monthlyScheduleElement);

            return scheduleElement;
        }
        /// <summary>
        /// Add Day of Week value to task schedule.
        /// </summary>
        /// <param name="dayOfWeek">string value for Day of Week</param>
        public void AddDayOfWeek(string dayOfWeek)
        {
            if (dayOfWeek != null && !dayOfWeek.Trim().Equals(""))
            {
                switch (dayOfWeek)
                {
                    case "0":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "1":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "2":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "3":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "4":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "5":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                    case "6":
                        this.daysOfWeek.Add(dayOfWeek);
                        break;
                }
            }
        }
        /// <summary>
        /// Add day of Month  to task schedule. 
        /// </summary>
        /// <param name="dayOfMonth">string value for day of month</param>
        public void AddDayOfMonth(string dayOfMonth)
        {
            if (dayOfMonth != null && !dayOfMonth.Trim().Equals(""))
            {
                int day = int.Parse(dayOfMonth);
                if (day >= 1 && day <= 31)
                    this.daysOfMonth.Add(dayOfMonth);
            }
        }
        /// <summary>
        /// Add month of year to task schedule.
        /// </summary>
        /// <param name="monthOfYear">string value for month of year</param>
        public void AddMonthOfYear(string monthOfYear)
        {
            if (monthOfYear != null && !monthOfYear.Trim().Equals(""))
            {
                switch (monthOfYear)
                {
                    case "1":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "2":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "3":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "4":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "5":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "6":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "7":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "8":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "9":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "10":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "11":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                    case "12":
                        this.monthsOfYear.Add(monthOfYear);
                        break;
                }
            }
        }

        #endregion

        #region Private Fields

        private string status = "I";
        private DateTime startDate;
        private DateTime endDate;
        private int type = -1;
        private int intervalMinutes = 1;
        private int intervalDays = -1;
        private int intervalWeeks = -1;
        private ArrayList daysOfWeek = new ArrayList();
        private ArrayList daysOfMonth = new ArrayList();
        private int weekOfMonth = -1;
        private ArrayList monthsOfYear = new ArrayList();

        #endregion
    }
}
