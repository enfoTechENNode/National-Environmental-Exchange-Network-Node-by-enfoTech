using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.UI.DataDictionary;
using Node.Lib.UI.Base;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.WebControls
{
	/// <summary>
	/// Calendar type
	/// </summary>
	public enum CalendarType
	{
		/// <summary>
		/// Show all dates
		/// </summary>
		All,
		/// <summary>
		/// Show past dates only
		/// </summary>
		Past,
		/// <summary>
		/// Show future dates only
		/// </summary>
		Future
	}

	/// <summary>
	/// String of Date format
	/// </summary>
	public enum CalendarDateFormat
	{
		/// <summary>
		/// yyyy mm dd (XML)
		/// </summary>
		YMD,
		/// <summary>
		/// mm dd yyyy (USA)
		/// </summary>
		MDY,
		/// <summary>
		/// dd mm yyyy (Rest of the world)
		/// </summary>
		DMY
	}
	
	/// <summary>
	/// DatePicker
	/// </summary>
	public class DatePicker : TextBox
	{
		//***********************************************************************
		// Constant
		//***********************************************************************
		
		/// <summary>
		/// Calendat type to show all dates.
		/// </summary>
		public const string TYPE_ALL = "ALL";
		/// <summary>
		/// Calendat type to show only future dates.
		/// </summary>
		public const string TYPE_FUTURE = "FUTURE";
		/// <summary>
		/// Calendat type to show only past dates.
		/// </summary>
		public const string TYPE_PAST = "PAST";

		/// <summary>
		/// Define date format. YYYY MM DD.
		/// You can set DateSpliter on top of this.
		/// </summary>
		public const string FORMAT_YMD = "YMD";
		/// <summary>
		/// Define date format. MM DD YYYY.
		/// </summary>
		public const string FORMAT_MDY = "MDY";
		/// <summary>
		/// Define date format. DD MM YYYY.
		/// </summary>
		public const string FORMAT_DMY = "DMY";

		//***********************************************************************
		// Private members
		//***********************************************************************

		//private string imageBase = "~/Images/EAF";
		//private string scriptBase = "~/Scripts/EAF";
		private string calIcon = "";
		private string calType = TYPE_ALL;
		private int calStartYear = 1900;
		private int calEndYear = 2100;		
		private string dateSpliter = "/";
		private string dateFormat = FORMAT_MDY;
		private string formName = "forms[0]";
		private int offsetX = -170;
		private int offsetY = 10;
		private bool fixedSize = false;
		private bool yearNav = true;
		private bool resetCal = true;


		// Layout and color

		private int TDWidth = 25;
		private int TDHeight = 15;
		private int Opacity = 95;

		// color
		private string ClrDivBg = "#CDCDCD";
		private string ClrCalBg = "#EAEAEA";
		private string ClrTitleBg = "#FFFFFF";
		private string ClrTodayIsBg = "#EAEAEA";

		private string ClrCurrentMonthBg = "#FFFF99";
		private string ClrOtherMonthBg = "#EAEAEA";
		private string ClrTodayBg = "#FFCC33";
		private string ClrDOWWeekendBg = "#990000";
		private string ClrDOWWeekdayBg = "#003399";

		// css
		private string CssTitleFont = "eaf_CalTitleFont";
		private string CssCurrentDateFont = "eaf_CalDateFont";
		private string CssOtherDateFont = "eaf_CalOtherDateFont";
		private string CssTodayIsFont = "eaf_CalTodayFont";
		private string CssDOWFont = "eaf_CalDOWFont";

		private string CssWeekdayBG = "eaf_CalWeekdayBG";
		private string CssWeekendBG = "eaf_CalWeekendBG";
		private string CssCalBG = "eaf_CalBG";
		private string CssCrossBG = "eaf_CalCrossBG";
		private string CssCircleBG = "eaf_CalCircleBG";

		private string ScriptBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ScriptBase; }
		}

		private string ImageBase
		{
			get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase; }
		}

		//***********************************************************************
		// Attributes
		//***********************************************************************

		/*
		/// <summary>
		/// Get or set image base of related images. Default is "~/Images/EAF".
		/// </summary>
		public string ImageBase
		{
			get { return this.imageBase; }
			set { this.imageBase = value; }
		}

		/// <summary>
		/// Get or set script base of EAF javascript. Default is "~/Scripts/EAF".
		/// </summary>
		
		public string ScriptBase
		{
			get { return this.scriptBase; }
			set { this.scriptBase = value; }
		}
		*/

		/// <summary>
		/// Get or set image url of the icon shown next to textbox.  Default is "~/Images/EAF/CalIcon2.gif".
		/// </summary>
		public string CalIcon
		{
			get { return this.calIcon; }
			set { this.calIcon = value; }
		}

		/// <summary>
		/// Get or set starting year of DatePicker calendar. Default is 1900.
		/// </summary>
		public int CalStartYear
		{
			get { return this.calStartYear; }
			set { this.calStartYear = value; }
		}

		/// <summary>
		/// Get or set ending year of DatePicker calendar. Default is 2100.
		/// </summary>
		public int CalEndYear
		{
			get { return this.calEndYear; }
			set { this.calEndYear = value; }
		}

		/// <summary>
		/// [Optional] Get or set Calendar type. 
		/// </summary>
		/// 
		/// <remarks>
		/// 
		/// <para>There are 3 type of calendar you can use:</para>
		/// 
		/// <list type="bullet">
		/// <item>
		/// <term>All</term>
		/// <description>Show full calendar dates for user to pick. No black out days.</description>
		/// </item>
		/// <item>
		/// <term>Future</term>
		/// <description>Show only future dates after today, including today.</description>
		/// </item>
		/// <item>
		/// <term>Past</term>
		/// <description>Show only passed dates before today, including today.</description>
		/// </item>
		/// </list>
		/// 
		/// Field information:
		/// <list type="bullet">
		/// <item>
		/// <term>Attribute</term>
		/// <description>Optional</description>
		/// </item>
		/// <item>
		/// <term>Default Value</term>
		/// <description>All</description>
		/// </item>
		/// </list>
		/// </remarks>
		public string CalType
		{
			get { return this.calType; }
			set { this.calType = value; }
		}

		/// <summary>
		/// true if you want to always the 6th row of the calenday. Otherwise, false. Default is false.
		/// </summary>
		public bool FixedSize  // will generate the 6th row
		{
			get { return this.fixedSize; }
			set { this.fixedSize = value; }
		}

		/// <summary>
		/// Set true to show year navigatation arrow. Otherwise, false. Default is true.
		/// </summary>
		public bool YearNav
		{
			get { return this.yearNav; }
			set { this.yearNav = value; }
		}
		
		/// <summary>
		/// Get or set date splitter between DD MM YYYY. Default is "-".
		/// </summary>
		public string DateSpliter
		{
			get { return this.dateSpliter; }
			set { this.dateSpliter = value; }
		}

		/// <summary>
		/// Get or set date format, YMD, MDY or DMY.
		/// </summary>
		public string DateFormat
		{
			get { return this.dateFormat; }
			set { this.dateFormat = value; }
		}

		/// <summary>
		/// Get or set form name of your HTML form. Defualt is "forms[0]".
		/// </summary>
		public string FormName
		{
			get { return this.formName; }
			set { this.formName = value; }
		}

		/// <summary>
		/// Get or set X-axis offset position of calendar when shown. Defualt is -170.
		/// </summary>
		public int OffsetX
		{
			get { return this.offsetX; }
			set { this.offsetX = value; }
		}

		/// <summary>
		/// /// Get or set Y-axis offset position of calendar when shown. Default is 10.
		/// </summary>
		public int OffsetY
		{
			get { return this.offsetY; }
			set { this.offsetY = value; }
		}

		/// <summary>
		/// Set true if you want to reset calendar to current month when calendar is shown. Otherwise, false. Default is true.
		/// </summary>
		public bool ResetCal
		{
			get { return this.resetCal; }
			set { this.resetCal = value; }
		}
		
		//***********************************************************************
		// private Attributes
		//***********************************************************************
		private string ImageName
		{
			get { return this.ClientID + "_img"; }
		}


		//***********************************************************************
		// Public methods
		//***********************************************************************

		/// <summary>
		/// Get DateTime object of DatePicker.
		/// </summary>
		/// <returns> <see cref="System.Nullable"/> class type DateTime object of the value you input. Null if the format is invalid.</returns>
		public DateTime? GetDateTime()
		{
			try
			{
				return DateTime.Parse(this.Text);
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Set DateTime of the TextBox.
		/// </summary>
		/// <param name="dt"><see cref="System.Nullable"/> class type DateTime object, can be null.</param>
		public void SetDateTime(DateTime dt)
		{
			if (this.DateFormat.ToUpper() == FORMAT_MDY)
				this.Text = dt.Month + this.DateSpliter + dt.Day + this.DateSpliter + dt.Year;
			else if (this.DateFormat.ToUpper() == FORMAT_DMY)
				this.Text = dt.Day + this.DateSpliter + dt.Month + this.DateSpliter + dt.Year;
			else
				this.Text = dt.Year + this.DateSpliter + dt.Month + this.DateSpliter + dt.Day;
		}

		/// <summary>
		/// Set DateTime of the TextBox.
		/// </summary>
		/// <param name="dateStr">DateTime format string. The TextBox value will be empty if the date format is invalid.</param>
		public void SetDateTime(String dateStr)
		{
			try
			{
				DateTime dt = DateTime.Parse(dateStr);
				SetDateTime(dt);
			}
			catch
			{
				this.Text = "";
			}
		}

		//***********************************************************************
		// Control events
		//***********************************************************************


		/// <summary>
		/// OnPrerender event of the control
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			PageUtility.RegisterUtilsScript(this.Page);
			PageUtility.RegisterScript(this.Page, ClientScriptRegID.EAF_DatePicker, CalInitScript());
		}
		

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			this.MaxLength = 10;
			base.Render(output);

			if (this.CalIcon == "") this.CalIcon = ResolveUrl(this.ImageBase) + "CalIcon2.gif";

			output.Write("<a href=\"javascript:void(0);\" onClick=\"eaf_DatePicker.switchDivCal('" + this.CalType + "','" + this.ImageName + "',document." + FormName + "." + this.ClientID + "," + this.OffsetX + "," + this.OffsetY + "," + (this.ResetCal?"true":"false") + ");\">");
			output.Write("<img src=\"" + ResolveUrl(this.CalIcon) + "\" border=\"0\" name=\"" + this.ImageName + "\"  id=\"" + this.ImageName + "\" border=\"0\" align=\"middle\">");			
			output.Write("</a>");
		}

		//***********************************************************************
		// Private methods
		//***********************************************************************

		private string CalInitScript()
		{
			StringBuilder s = new StringBuilder();

			s.AppendLine("<script language=\"javascript\" type=\"text/javascript\" src=\"" + ResolveUrl(this.ScriptBase) + "DivCalendar.js\"></script>");

            s.AppendLine("<script language=\"JavaScript\" type=\"text/javascript\"><!--");

			s.AppendLine("eaf_DatePicker.SkinBase=\"" + ResolveUrl(this.ImageBase) + "\";");
			s.AppendLine("eaf_DatePicker.FormName=\"" + this.FormName + "\";");
			s.AppendLine("eaf_DatePicker.CalStartYear=" + this.CalStartYear+";");
			s.AppendLine("eaf_DatePicker.CalEndYear=" + this.CalEndYear+";");
			s.AppendLine("eaf_DatePicker.CalType=\"" + this.CalType + "\";");
			s.AppendLine("eaf_DatePicker.FixedSize=" + (this.FixedSize?"true":"false")+";");
			s.AppendLine("eaf_DatePicker.YearNav=" + (this.YearNav?"true":"false")+";");
			s.AppendLine("eaf_DatePicker.DateSpliter=\"" + this.DateSpliter + "\";");
			s.AppendLine("eaf_DatePicker.DateFormat=\"" + this.DateFormat + "\";");

			s.AppendLine("eaf_DatePicker.TDWidth=" + this.TDWidth + ";");
			s.AppendLine("eaf_DatePicker.TDHeight=" + this.TDHeight + ";");
			s.AppendLine("eaf_DatePicker.Opacity=" + this.Opacity + ";");

			s.AppendLine("eaf_DatePicker.ClrDivBg=\"" + this.ClrDivBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrCalBg=\"" + this.ClrCalBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrTitleBg=\"" + this.ClrTitleBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrTodayIsBg=\"" + this.ClrTodayIsBg + "\";");

			s.AppendLine("eaf_DatePicker.ClrCurrentMonthBg=\"" + this.ClrCurrentMonthBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrOtherMonthBg=\"" + this.ClrOtherMonthBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrTodayBg=\"" + this.ClrTodayBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrDOWWeekendBg=\"" + this.ClrDOWWeekendBg + "\";");
			s.AppendLine("eaf_DatePicker.ClrDOWWeekdayBg=\"" + this.ClrDOWWeekdayBg + "\";");

			s.AppendLine("eaf_DatePicker.CssTitleFont=\"" + this.CssTitleFont + "\";");
			s.AppendLine("eaf_DatePicker.CssCurrentDateFont=\"" + this.CssCurrentDateFont + "\";");
			s.AppendLine("eaf_DatePicker.CssOtherDateFont=\"" + this.CssOtherDateFont + "\";");
			s.AppendLine("eaf_DatePicker.CssTodayIsFont=\"" + this.CssTodayIsFont + "\";");
			s.AppendLine("eaf_DatePicker.CssDOWFont=\"" + this.CssDOWFont + "\";");

			s.AppendLine("eaf_DatePicker.CssWeekdayBG=\"" + this.CssWeekdayBG + "\";");
			s.AppendLine("eaf_DatePicker.CssWeekendBG=\"" + this.CssWeekendBG + "\";");
			s.AppendLine("eaf_DatePicker.CssCalBG=\"" + this.CssCalBG + "\";");
			s.AppendLine("eaf_DatePicker.CssCrossBG=\"" + this.CssCrossBG + "\";");
			s.AppendLine("eaf_DatePicker.CssCircleBG=\"" + this.CssCircleBG + "\";");

			s.AppendLine("eaf_DatePicker.init();");

			s.AppendLine("--></script>");

			return s.ToString();
		}
	}
}
