
//***************************************************************
//
// The DivCalendar.js only support IE5 and above, and NS 7.
// Use with your own risk if run in other broswers...
//
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

function DivCalendar(instancename)
{
	// Important Attributes
	this.CalType = "All";		// All, Future, and Past
	this.DateSpliter = "-";		// date format 2000-12-12
	this.DateFormat = "YMD";	// YMD, MDY, DMY
	this.FormName = "forms[0]";	// form name of the page, default first form
	this.HideFields = null;		// page element you want to hide when DIV calendar show
	this.SkinBase = "../../eftWC/skin";

	// Basic attributes
	this.CalStartYear = 1900;
	this.CalEndYear = 2100;
	this.FixedSize = false;		// will generate the 6th row if is true
	this.YearNav = false;		// will reconize arrow for year
	this.ShowTwoDigit = true;

    this.TDWidth = 25;
	this.TDHeight = 20;
	this.Opacity = 95;

	// Color attributes
	this.ClrDivBg = "#CDCDCD";
	this.ClrCalBg = "#EAEAEA";
	this.ClrTitleBg = "#FFFFFF";
	this.ClrTodayIsBg = "#EAEAEA";

	this.ClrCurrentMonthBg = "#FFFF99";
	this.ClrOtherMonthBg = "#EAEAEA";
	this.ClrTodayBg = "#FFCC33";
	this.ClrDOWWeekendBg = "#990000";
	this.ClrDOWWeekdayBg = "#003399";

	// CSS attributes
	this.CssTitleFont = "divCalTitleFont";
	this.CssCurrentDateFont = "divCalDateFont";
	this.CssOtherDateFont = "divCalOtherDateFont";
	this.CssTodayIsFont = "divCalTodayFont";
	this.CssDOWFont = "divCalDOWFont";

	this.CssWeekdayBG = "divCalWeekdayBG";
	this.CssWeekendBG = "divCalWeekendBG";
	this.CssCalBG = "divCalBG";
	this.CssCrossBG = "divCalCrossBG";
	this.CssTodayBG = "divCalTodayBG";
	this.CssSelectDateBG = "divCalSelectDateBG";

	// internal members
	this.InstanceName = instancename;
	this.RtnTextboxObj = null;
	this.ShowMonth = null;
	this.ShowYear = null;
	this.DOC_IE = (document.all)? true:false;					// IE
	this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE

	//this.init();

	return this;
}

//*********************************************
//	Class level constants
//*********************************************

DivCalendar.MONTH_NAMES = Utils.makeArray('January','February','March','April','May','June','July','August','September','October','November','December');
DivCalendar.MONTH_DAYS = Utils.makeArray(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
DivCalendar.DAY_OF_WEEK = Utils.makeArray('Su','Mo','Tu','We','Th','Fr','Sa');
DivCalendar.TODAY_OBJ = new Date();
DivCalendar.SELECT_DATE_OBJ = null;

//** Convert YY to YYYY
DivCalendar.Y2K = function(number)
{
	return (number<1000) ? number+1900 : number;
}

//*********************************************
//	Public methods
//*********************************************

//-------------------
// Initial functions
//--------------------
DivCalendar.prototype.init = function()
{
	this.ImgLeft = this.SkinBase + "/img/gen/arrow_left.gif";
	this.ImgRight = this.SkinBase + "/img/gen/arrow_right.gif";
	this.ImgClose = this.SkinBase + "/img/gen/Btn_Close.gif";
	this.ImgYearLeft = this.SkinBase + "/img/gen/arrow_dbl_left.gif";
	this.ImgYearRight = this.SkinBase + "/img/gen/arrow_dbl_right.gif";
	
	this.writeCalDiv();

	if(this.DOC_IE) this.DivObj = eval('document.all.' + this.InstanceName);
	if(this.DOC_DOM) this.DivObj = document.getElementById(this.InstanceName);

	this.resetCal(DivCalendar.TODAY_OBJ.getMonth(),DivCalendar.TODAY_OBJ.getYear());
}

DivCalendar.prototype.setHideFields = function(ary)
{
	this.HideFields = ary;
}

//-------------------------
// reset calendar to today
//-------------------------
DivCalendar.prototype.resetCal = function(M,Y)
{
	this.DivObj.innerHTML = this.genCalHtml(M,Y);
}

//---------------------
//	Write the DIV tag
//---------------------
DivCalendar.prototype.writeCalDiv = function()
{
	document.write('<div id="' + this.InstanceName + '" ');
	document.write(' style="position:absolute; z-index:9999;  visibility:hidden;');
	document.write(' filter:');
	document.write(' progid:DXImageTransform.Microsoft.Shadow(color=#666666,Direction=120,Strength=5,positive=true)');
	document.write(' progid:DXImageTransform.Microsoft.Alpha( style=0,opacity='+this.Opacity+');');
	document.write('">');
	document.write('</div>');
}

//----------------------------------
//	Detecting if inside DIV tag
//-----------------------------------
DivCalendar.prototype.isInDivLayer = function(x,y)
{
	var divTop,divLeft;

	if (this.DOC_IE)
	{
		divTop = parseInt(this.DivObj.style.top,10);
		divLeft = parseInt(this.DivObj.style.left,10);
	}
	else if(this.DOC_DOM)
	{
		divTop = parseInt(this.DivObj.offsetTop,10);
		divLeft = parseInt(this.DivObj.offsetLeft,10);
	}

	if ( x>divLeft && x<(divLeft+this.DivObj.offsetWidth) &&  y>divTop && y<(divTop+this.DivObj.offsetHeight) )
	{
		return true;
	}
	return false;
}

//---------------------------------------
//	Main method for generate HTML Cal
//---------------------------------------
DivCalendar.prototype.genCalHtml = function(inMonth, inYear)
{
	this.ShowMonth = inMonth;
	this.ShowYear = inYear;

	var today_day   = DivCalendar.TODAY_OBJ.getDate();
	var today_month = DivCalendar.TODAY_OBJ.getMonth();
	var today_year  = DivCalendar.Y2K(DivCalendar.TODAY_OBJ.getYear());

	//
	//** a Date object of the first day of the month
	//
	var firstDay = new Date(this.ShowYear,this.ShowMonth,1);
	var startDOW = firstDay.getDay(); //*** a Day of week: Mon, Tue.....

	//
	//** For February, change the days of month
	//
	if( ((this.ShowYear%4==0)&&(this.ShowYear%100!=0)) || (this.ShowYear%400==0) )
		DivCalendar.MONTH_DAYS[1] = 29;
	else
		DivCalendar.MONTH_DAYS[1] = 28;

	var output = '';

	output += '<table cellspacing="5" class="'+this.CssCalBG+'" style="background-color:'+this.ClrDivBg+'; width:'+(this.TDWidth*7+20)+'px; padding:0px; border:1px solid #006600;">';
	output += '<tr><td>';

	//output += '<table cellspacing="0" cellpadding="0" border="0" bgcolor="'+this.ClrTitleBg+'">';
	output += '<table id="divCalMonthYear" cellspacing="0">';

	output += '<tr>';

	//
	//** YearNav means you can go to Next/Previous year directly
	//
	if(this.YearNav==true)
	{
		/* split year and month
		output += '<td><a href="javascript:'+this.InstanceName+'.goYear(-1);"><img src="' + this.ImgLeft + '" border="0" alt="Previous Year"></a></td>';
		output += '<td align="center" nowrap><span class="' + this.CssTitleFont + '">&nbsp;' + this.ShowYear + '&nbsp;</span></td>';
		output += '<td><a href="javascript:'+this.InstanceName+'.goYear(1);"><img src="' + this.ImgRight + '" border="0" alt="Next Year"></a></td>';
		output += '<td bgcolor="'+this.ClrDivBg+'">&nbsp;</td>';
		output += '<td><a href="javascript:'+this.InstanceName+'.goMonth(-1);"><img src="' + this.ImgLeft + '" border="0" alt="Previous Month"></a></td>';
		output += '<td align="center" width="100%"><span class="' + this.CssTitleFont + '">' + DivCalendar.MONTH_NAMES[this.ShowMonth] + '</span></td>';
		output += '<td><a href="javascript:'+this.InstanceName+'.goMonth(1);"><img src="' + this.ImgRight + '" border="0" alt="Next Month"></a></td>';
		*/


		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goYear(-1);"><img src="' + this.ImgYearLeft + '" border="0" alt="Previous Year"></a></td>';
		//output += '<td>&nbsp;</td>';
		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goMonth(-1);"><img src="' + this.ImgLeft + '" border="0" alt="Previous Month"></a></td>';
		output += '<td width="100%"><span class="' + this.CssTitleFont + '">'+ DivCalendar.MONTH_NAMES[this.ShowMonth] + ' ' + this.ShowYear + '</span></td>';
		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goMonth(1);"><img src="' + this.ImgRight + '" border="0" alt="Next Month"></a></td>';
		//output += '<td>&nbsp;</td>';
		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goYear(1);"><img src="' + this.ImgYearRight + '" border="0" alt="Next Year"></a></td>';
	}
	else
	{
		//output += '<td>&nbsp;</td>';
		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goMonth(-1);"><img src="' + this.ImgLeft + '" border="0" alt="Previous Month"></a></td>';
		output += '<td width="100%"><span class="' + this.CssTitleFont + '">&nbsp;'+ DivCalendar.MONTH_NAMES[this.ShowMonth] + '&nbsp;&nbsp;' + this.ShowYear + '&nbsp;</span></td>';
		output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.goMonth(1);"><img src="' + this.ImgRight + '" border="0" alt="Next Month"></a></td>';
		//output += '<td>&nbsp;</td>';
	}
	output += '</tr>';

	output += '</table>';

	output += '</td></tr>';
	output += '<tr><td>';

	//output += '<table cellspacing="2" cellpadding="0" border="1" bordercolordark="#FFFFFF" bordercolorlight="#999999" bgcolor="'+this.ClrCalBg+'" align="center">';
	output += '<table id="divCalDate" cellspacing="1">';

	output += '<tr>';

	//
	//** loop for day of week: Mon, Tue.....
	//
	for(i=0; i<7; i++)
	{
		var tdcss = this.CssWeekdayBG;
		var tdclr = this.ClrDOWWeekdayBg;
		if(i==0||i==6)
		{
		 	tdcss = this.CssWeekendBG;
		 	tdclr = this.ClrDOWWeekendBg;
		}

		output += '<td class="'+tdcss+'" bgcolor="'+tdclr+'" width="' + this.TDWidth + '" height="' + this.TDHeight + '">';
		output += '<span class="' + this.CssDOWFont + '">' + DivCalendar.DAY_OF_WEEK[i] +'</span>';
		output += '<\/td>';
	}

	output += '<\/tr>';


	//
	//** prepare to loop cal...
	//
	var column = 0;
	var row = 0;

	var prevMonth = this.ShowMonth-1;
	if(prevMonth==-1) prevMonth = 11;  //** Jan is 0, Feb is 1.....

	var prevYear = this.ShowYear;
	if (prevMonth==11) prevYear = prevYear-1;

	output += '<tr align="center" valign="middle">';

	//
	//** Loop for previous month
	//
	for(i=0; i<startDOW; i++, column++)
	{
		output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrOtherMonthBg + '">';
		output += '<span class="'+this.CssOtherDateFont+'">' + (DivCalendar.MONTH_DAYS[prevMonth]-startDOW+i+1) + '</span>';
		output += '</td>';
	}

	//
	//** Loop for current month
	//
	for(i=1; i<=DivCalendar.MONTH_DAYS[this.ShowMonth]; i++, column++)
	{
			var tdColor = this.ClrCurrentMonthBg;

			/*
			if(i==today_day && this.ShowMonth==today_month && this.ShowYear==today_year)
			 	tdColor = this.ClrTodayBg;
			*/

			//
			//** set TD background CSS based on some properties
			//
			//** when show "only-after" date
			if(this.CalType.toUpperCase()=='FUTURE' && ((i<today_day && this.ShowMonth<=today_month && this.ShowYear<=today_year) || (this.ShowMonth<today_month && this.ShowYear<=today_year) || this.ShowYear<today_year) )
			{
				output += '<td class="'+this.CssCrossBG+'" width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + tdColor + '">';
			}
			//** when show "today" date
		else if(i==today_day && this.ShowMonth==today_month && this.ShowYear==today_year)
		{
				//** when show "today" date
				if(DivCalendar.SELECT_DATE_OBJ!=null && i==DivCalendar.SELECT_DATE_OBJ.getDate() && this.ShowMonth==DivCalendar.SELECT_DATE_OBJ.getMonth() && this.ShowYear==DivCalendar.SELECT_DATE_OBJ.getYear())
				{
						output += '<td  class="'+this.CssSelectDateBG+'" width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrTodayBg + '">';
				}
				else
				{
						output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrTodayBg + '">';
				}
			}
			//** when show "selected" date
		else if(DivCalendar.SELECT_DATE_OBJ!=null && i==DivCalendar.SELECT_DATE_OBJ.getDate() && this.ShowMonth==DivCalendar.SELECT_DATE_OBJ.getMonth() && this.ShowYear==DivCalendar.SELECT_DATE_OBJ.getYear())
		{
				output += '<td  class="'+this.CssSelectDateBG+'" width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + tdColor + '">';
			}
		//** when show "the rest" date
		else
		{
				output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + tdColor + '">';
			}

			if(this.CalType.toUpperCase()=='FUTURE' && ((i<today_day&&this.ShowMonth<=today_month&&this.ShowYear<=today_year) || (this.ShowMonth<today_month&&this.ShowYear<=today_year) || this.ShowYear<today_year) ) // show after link
			{
				//if(this.DOC_IE)
					output += '<span class="' + this.CssOtherDateFont + '">' + i + '</span>';
				//else if(this.DOC_DOM)
					//output += '<span class="' + this.CssOtherDateFont + '">' + i + '</span>';

			}
			else if(this.CalType.toUpperCase()=='PAST' && ((i>today_day&&this.ShowMonth>=today_month&&this.ShowYear>=today_year) || (this.ShowMonth>today_month&&this.ShowYear>=today_year) || this.ShowYear>today_year) ) // show after link
			{
				output += '<span class="' + this.CssOtherDateFont + '">' + i + '</span>';
			}
			else
			{
				output += '<a href="javascript:' + this.InstanceName + '.changeDay(' + i + ');">';
				output += '<span class="' + this.CssCurrentDateFont + '">' + i + '</span>';
				output += '</a>';
			}

	    output += '</td>';

	    if(column==6)
	    {
	     	output += '<\/tr>';
			 	if((i+1)<=DivCalendar.MONTH_DAYS[this.ShowMonth])
			 			output += '<tr align="center" valign="middle">';
	    column = -1;
	    row++;
	    }
	}

	//
	//** Loop for next month if needed
	//
	var iRcd = 0;
	if(column>0)
	{
		for(i=1; column<7; i++, column++)
		{
		    output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrOtherMonthBg + '">';
				output += '<span class="' + this.CssOtherDateFont + '">' + i + '</span>';
		    output += '</td>';
		    iRcd = i;
		}
		output += '<\/tr>';
	}

	//
	//** generate the 6th row, for Look & Feel
	//
	if(this.FixedSize)
	{
			if(row<5 || (row==5 && column==0))
			{
				output += '<tr align="center" valign="middle">';
				for(i=1; i<=7; i++)
				{
					output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrOtherMonthBg + '">';
					output += '<span class="' + this.CssOtherDateFont + '">' + parseInt((i+iRcd),10) + '</span>';
					output += '</td>';
				}
				output += '<\/tr>';
			}
	}

	output += '<\/table>';
	output += '<\/td>';

	//
	//** Show today text and close button
	//
	var todayMonth = parseInt(today_month,10)+1

	//output += '<tr bgcolor="'+this.ClrTodayIsBg+'">';
	output += '<tr><td>';

	output += '<table id="divCalTodayLine" cellspacing="0"><tr>';

	output += '<td width="100%" align="center">';
	output += '<span class="' + this.CssTodayIsFont + '">Today is ';
	output += '<a class="today" href="javascript:' + this.InstanceName + '.goToday('+today_month+','+today_year+');">';

	if(this.DateFormat.toUpperCase()=='MDY')
		output += todayMonth + this.DateSpliter + today_day + this.DateSpliter + today_year;
	else if(this.DateFormat.toUpperCase()=='DMY')
		output += today_day + this.DateSpliter + todayMonth + this.DateSpliter + today_year;
	else
		output += today_year + this.DateSpliter + todayMonth + this.DateSpliter + today_day;

	output += '</a>';
	output += '</span>';
	output += '</td>';
	output += '<td><a class="offset" href="javascript:'+this.InstanceName+'.hideDivCal();"><img src="' + this.ImgClose + '" border="0" alt="Close Calendar"></a></td>';
	output += '</tr></table>';


	output += '</td></tr>';
	output += '</table>';

	return output;
}

//-----------------
//  Change Year
//-----------------
DivCalendar.prototype.goYear = function(num)
{
	var gotoYY = parseInt(this.ShowYear,10) + parseInt(num,10);

	if(gotoYY<this.CalStartYear) gotoYY = this.CalStartYear;
	if(gotoYY>this.CalEndYear) gotoYY = this.CalEndYear;

this.DivObj.innerHTML = this.genCalHtml(this.ShowMonth,gotoYY);
}

//-------------------
//  Change Month
//-------------------
DivCalendar.prototype.goMonth = function(num)
{
	var gotoMM = parseInt(this.ShowMonth,10) + parseInt(num,10);
	var gotoYY = this.ShowYear;

	if(gotoMM==-1)
	{
		gotoMM = 11;
		gotoYY = parseInt(this.ShowYear,10)-1;
		if(gotoYY<this.CalStartYear) gotoYY = this.CalStartYear;
	}
	else if(gotoMM==12)
	{
	gotoMM = 0;
		gotoYY = parseInt(this.ShowYear,10)+1;
		if(gotoYY>this.CalEndYear) gotoYY = this.CalEndYear;
	}

this.DivObj.innerHTML = this.genCalHtml(gotoMM,gotoYY);
}

//---------------------------------------
// Go to the month and year of Today
//---------------------------------------
DivCalendar.prototype.goToday = function(m,y)
{
   	this.DivObj.innerHTML = this.genCalHtml(m,y);
}

//------------------------------------------
//  Set selected day back to the text field
//------------------------------------------
DivCalendar.prototype.changeDay = function(day)
{
	if(this.RtnTextboxObj!=null)
	{
		var mxx,dxx;
		if(this.ShowTwoDigit)
		{
			mxx = Utils.appendZero(''+parseInt(this.ShowMonth+1,10),2);
			dxx = Utils.appendZero(''+day,2);
		}
		else
		{
			mxx = parseInt(this.ShowMonth+1,10);
			dxx = day;
		}
		
		if(this.DateFormat.toUpperCase()=='MDY')
			this.RtnTextboxObj.value = mxx + this.DateSpliter + dxx + this.DateSpliter + this.ShowYear;
		else if(this.DateFormat.toUpperCase()=='DMY')
			this.RtnTextboxObj.value = dxx + this.DateSpliter + mxx + this.DateSpliter + this.ShowYear;
		else
			this.RtnTextboxObj.value = this.ShowYear + this.DateSpliter + mxx + this.DateSpliter + dxx;
	}

	this.DivObj.style.visibility = 'hidden';
	this.RtnTextboxObj = null;
	this.ShowSelectItems(true);
}

//-----------------------------------------
//  Hide DIV calendar
//-----------------------------------------
DivCalendar.prototype.hideDivCal = function()
{
		this.DivObj.style.visibility='hidden';
		this.ShowSelectItems(true);
}

//-----------------------------------------
// Major function for javascript to call
//-----------------------------------------
DivCalendar.prototype.switchDivCal = function(calType,imgName,fldObj,offsetX,offsetY,reload)
{
		this.RtnTextboxObj = fldObj;
		this.CalType = calType;

		var dtestr = fldObj.value.split(this.DateSpliter);
		if(dtestr.length==3)
		{
				var iY, iM, iD;
				if(this.DateFormat.toUpperCase()=='MDY')
				{
					iM = dtestr[0];
					iD = dtestr[1];
					iY = dtestr[2];
				}
				else if(this.DateFormat.toUpperCase()=='DMY')
				{
					iD = dtestr[0];
					iM = dtestr[1];
					iY = dtestr[2];
				}
				else
				{
					iY = dtestr[0];
					iM = dtestr[1];
					iD = dtestr[2];
				}

				DivCalendar.SELECT_DATE_OBJ = new Date(parseInt(iY,10),parseInt(iM,10)-1,parseInt(iD,10),0,0,0,0);

				this.resetCal(parseInt(iM,10)-1,iY);
		}
		else
		{
				DivCalendar.SELECT_DATE_OBJ = null;
				if(reload) this.resetCal(DivCalendar.TODAY_OBJ.getMonth(),DivCalendar.TODAY_OBJ.getYear());
		}

		var imgObj = eval('document.' + imgName);

		// show calendar by imgobj
		// not working well when this image is deeply embedded in nested table

//		this.DivObj.style.left = eval(findPosX(imgObj)+offsetX) + "px";
//		this.DivObj.style.top = eval(findPosY(imgObj)+imgObj.height+offsetY) + "px";

		// show calendar by mouse click
		this.DivObj.style.left = eval(event.clientX+document.body.scrollLeft+offsetX) + "px";
		this.DivObj.style.top = eval(event.clientY+document.body.scrollTop+offsetY) + "px";

		this.DivObj.style.visibility = 'visible';
		this.ShowSelectItems(false);


	//** this is for no mouse event.
	/*
	if(this.RtnTextboxObj==fldObj)
	{
		if(this.DivObj.style.visibility=='visible')
		{
			this.DivObj.style.visibility='hidden';
			if (this.DOC_IE) document.all.select.style.visibility = "visible";
		}
		else
		{
			this.DivObj.style.visibility='visible';
			if (this.DOC_IE) document.all.select.style.visibility = "hidden";
		}
	}
	else
	{
		this.CalType = calType;
		if(reload==true) this.resetCal(DivCalendar.TODAY_OBJ.getMonth(),DivCalendar.TODAY_OBJ.getYear());

		var imgObj = eval('document.' + imgName);

		this.DivObj.style.left = eval(findPosX(imgObj)+offsetX) + "px";
		this.DivObj.style.top = eval(findPosY(imgObj)+imgObj.height+offsetY) + "px";

		this.RtnTextboxObj = fldObj;
		this.DivObj.style.visibility = 'visible';

		if (this.DOC_IE) document.all.select.style.visibility = "hidden";
	}
	*/
}


//=========================================
//  show/hide assigend select box element
//=========================================
DivCalendar.prototype.ShowSelectItems = function(boo)
{
	if (this.DOC_IE)
	{
		if(!(this.HideFields==null))
		{
			var i;
			for (i=0; i<this.HideFields.length; i++)
			{
				var fld = eval('document.'+this.FormName+'.'+this.HideFields[i]);
				if(boo==true)
					fld.style.visibility = "visible";
				else
					fld.style.visibility = "hidden";
			}
		}

		/*
		var fm = eval('document.'+this.FormName);
		for (i=0; i<fm.elements.length; i++)
		{
			if (fm.elements[i].type=='select-one')
				if(boo==true)
					fm.elements[i].style.visibility = "visible";
				else
					fm.elements[i].style.visibility = "hidden";
		}
		*/
	}
}

//----------------------------------------------
//  Mouse Down Event
//----------------------------------------------
DivCalendar.prototype.MouseDownEvent = function()
{
	if(this.DivObj.style.visibility=='visible')
	{
		var x,y;

		if (this.DOC_IE)
		{
			// alert(event.button);
			x = eval(event.clientX+document.body.scrollLeft);
			y = eval(event.clientY+document.body.scrollTop);
		}
		else if(this.DOC_DOM)
		{
			// alert(e.button);
			x = e.pageX;
			y = e.pageY;
		}

		if(!this.isInDivLayer(x,y))
		{
			this.DivObj.style.visibility='hidden';
			this.ShowSelectItems(true);
		}
	}
}


//***********************************************************
// Initialize methods
// Run this when windows loaded
// You can use put these codes in your page, instead here
//***********************************************************


// Instance name will be use in other place, do not change.
//-----------------------------------------------------------
var calObj = new DivCalendar('calObj');


// Mouse Event for DivCal
//--------------------------
function DivCalMouseDown()
{
	calObj.MouseDownEvent();
}

if(document.attachEvent)
	document.attachEvent('onmousedown',DivCalMouseDown);
else if(document.addEventListener)
	document.addEventListener('mousedown',DivCalMouseDown,false);

/*
document.onmousedown = function(e)
{
	divCalInstance.MouseDownEvent(e);
}

*/
