#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		TaskSchedule.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		04/27/2005 Danwen Sun Creation
// 
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Node.Lib.AppSystem
{
	/// <summary>
	/// Summary description for TaskSchedule.
	/// </summary>
	public class TaskSchedule
	{
		/// <summary>
		/// Constant value of ACTIVE.
		/// </summary>
		public const string ACTIVE = "A";
		/// <summary>
		/// Constant value of INACTIVE.
		/// </summary>
		public const string INACTIVE = "I";
		/// <summary>
		/// Constant value of FIRST_WEEK.
		/// </summary>
		public const int FIRST_WEEK = 0;
		/// <summary>
		/// Constant value of SECOND_WEEK.
		/// </summary>
		public const int SECOND_WEEK = 1;
		/// <summary>
		/// Constant value of THIRD_WEEK.
		/// </summary>
		public const int THIRD_WEEK = 2;
		/// <summary>
		/// Constant value of FOURTH_WEEK.
		/// </summary>
		public const int FOURTH_WEEK = 3;
		/// <summary>
		/// Constant value of LAST_WEEK.
		/// </summary>
		public const int LAST_WEEK = 4;

		private XmlNode scheduleNode = null;

		/// <summary>
		/// Create a new TasskSchedule instance.
		/// </summary>
		protected internal TaskSchedule(XmlNode scheduleNode)
		{
			this.scheduleNode = scheduleNode;
		}

		/// <summary>
		/// Gets or Sets Schedule Status.
		/// </summary>
		public string Status
		{
			get { return this.scheduleNode.Attributes.GetNamedItem("status").Value; }
			set { this.scheduleNode.Attributes.GetNamedItem("status").Value = value; }
		}

		/// <summary>
		/// Gets or Sets Start DateTime. The format of start datetime should be parsed by System.DateTime.
		/// </summary>
		public string StartDateTime
		{
			get { return this.scheduleNode.SelectSingleNode(".//StartDateTime").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//StartDateTime").InnerText = DateTime.Parse(value).ToString(); }
		}

		/// <summary>
		/// Gets or Sets End DateTime. The format of end datetime should be parsed by System.DateTime.
		/// </summary>
		public string EndDateTime
		{
			get { return this.scheduleNode.SelectSingleNode(".//EndDateTime").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//EndDateTime").InnerText = DateTime.Parse(value).ToString(); }
		}

		/// <summary>
		/// Indicates that task is on Daily Schedule. To set this will automatically disable weekly and monthly schedule.
		/// </summary>
		public bool IsDailySchedule
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//DailySchedule").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//DailySchedule").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsWeeklySchedule = false;
					this.IsMonthlySchedule = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on Weekly Schedule. To set this will automatically disable daily and monthly schedule.
		/// </summary>
		public bool IsWeeklySchedule
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//WeeklySchedule").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//WeeklySchedule").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsDailySchedule = false;
					this.IsMonthlySchedule = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on Monthly Schedule. To set this will automatically disable daily and weekly schedule.
		/// </summary>
		public bool IsMonthlySchedule
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//MonthlySchedule").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//MonthlySchedule").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsDailySchedule = false;
					this.IsWeeklySchedule = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on every n minutes of daily schedule. To set this will automatically disable every day schedule of daily.
		/// </summary>
		public bool IsDailyMinutes
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalMinutes").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalMinutes").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsDailyDays = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on every n days of daily schedule. To set this will automatically disable every minutes schedule of daily.
		/// </summary>
		public bool IsDailyDays
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalDays").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalDays").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsDailyMinutes = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on n days of monthly schedule. To set this will automatically disable weekday schedule of monthly.
		/// </summary>
		public bool IsMonthlyDays
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalDays").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalDays").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsMonthlyWeekdays = false;
				}
			}
		}

		/// <summary>
		/// Indicates that task is on weekday of monthly schedule. To set this will automatically disable days schedule of monthly.
		/// </summary>
		public bool IsMonthlyWeekdays
		{
			get 
			{ 
				return (this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks").Attributes.GetNamedItem("status").Value.ToUpper().Trim() == ACTIVE)? true : false; 
			}
			set 
			{ 
				this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks").Attributes.GetNamedItem("status").Value = (value)? ACTIVE : INACTIVE; 
				if (value)
				{
					this.IsMonthlyDays = false;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the minutes in daily schedule. The value should be between 1 and 1439.
		/// </summary>
		public string DailyMinutes
		{
			get { return this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalMinutes").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalMinutes").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the days in daily schedule. The value should be between 1 and 365.
		/// </summary>
		public string DailyDays
		{
			get { return this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalDays").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//DailySchedule/IntervalDays").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the weeks in weekly schedule. The value should be between 1 and 52.
		/// </summary>
		public string WeeklyWeeks
		{
			get { return this.scheduleNode.SelectSingleNode(".//WeeklySchedule/IntervalWeeks").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//WeeklySchedule/IntervalWeeks").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the weekday in weekly schedule. The value should be 0 for sunday, 1 for monday, and so on.
		/// </summary>
		public string WeeklyDayOfWeek
		{
			get { return this.scheduleNode.SelectSingleNode(".//WeeklySchedule/DayOfWeek").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//WeeklySchedule/DayOfWeek").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the days in monthly schedule. The value should be 1 - 30 or 31 by using comma to separate them.
		/// </summary>
		public string MonthlyDays
		{
			get { return this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalDays/DayOfMonth").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalDays/DayOfMonth").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the weekth in monthly schedule. The value should be 0 for first week, 1 for second week, 
		/// 2 for third week, 3 for fourth week, 4 for last week.
		/// </summary>
		public string MonthlyWeekOfMonth
		{
			get { return this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks/WeekOfMonth").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks/WeekOfMonth").InnerText = value; }
		}
	
		/// <summary>
		/// Gets or Sets the weekday in monthly schedule. The value should be 0 for sunday, 1 for monday, and so on.
		/// </summary>
		public string MonthlyDayOfWeek
		{
			get { return this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks/DayOfWeek").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//MonthlySchedule/IntervalWeeks/DayOfWeek").InnerText = value; }
		}

		/// <summary>
		/// Gets or Sets the month of year in monthly schedule. The value shoulde be 1 for January, 2 for February and so on.
		/// </summary>
		public string MonthlyMonthOfYear
		{
			get { return this.scheduleNode.SelectSingleNode(".//MonthlySchedule/MonthOfYear").InnerText; }
			set { this.scheduleNode.SelectSingleNode(".//MonthlySchedule/MonthOfYear").InnerText = value; }
		}
	}
}
