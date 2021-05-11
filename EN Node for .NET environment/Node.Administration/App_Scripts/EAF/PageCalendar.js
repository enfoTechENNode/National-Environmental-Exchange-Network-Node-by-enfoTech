
//***************************************************************
//																															
// The PageCalendar.js only support IE5 and above, and NS 7.		
// Use with your own risk if run in other broswers...					
//																															
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

function PageCalendar(instancename)
{
	// Important Attributes
	this.CalType = "All";		// All, Future, and Pass
	this.DateSpliter = "-";		// date format 2000-12-12
	this.DateFormat = "YMD";	// will reconize YMD, MDY, DMY
	this.FormName = "forms[0]";	// form name of the page
	
	this.SkinBase = "/Website/App_Scripts/";
	this.ImgLeft = "";
	this.ImgRight = "";
	this.ImgClose = "";
	this.ImgYearLeft = "";
	this.ImgYearRight = "";
	this.DayClickedScript = "/iPACS.Website/Images/EAF";
	
	// Basic attributes
	this.CalStartYear = 1900;
	this.CalEndYear = 2100;
	this.ShowMonth = PageCalendar.TODAY_OBJ.getMonth();
	this.ShowYear = PageCalendar.TODAY_OBJ.getYear();
	this.FixedSize = true;		// will generate the 6th row if is true

	this.TDWidth = 26;
	this.TDHeight = 18;

	this.MonthNav = true;	
	this.YearNav = true;		// will show arrow for year

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
	this.CssTitleFont = "eaf_CalTitleFont";
	this.CssCurrentDateFont = "eaf_CalDateFont";
	this.CssOtherDateFont = "eaf_CalOtherDateFont";
	this.CssTodayIsFont = "eaf_CalTodayFont";
	this.CssDOWFont = "eaf_CalDOWFont";	
	this.CssWeekdayBG = "eaf_CalWeekdayBG";
	this.CssWeekendBG = "eaf_CalWeekendBG";
	this.CssCalBG = "eaf_CalBG";
	this.CssCrossBG = "eaf_CalCrossBG";
	this.CssTodayBG = "eaf_CalTodayBG";
	this.CssSelectDateBG = "eaf_CalSelectDateBG";

	// internal members
	this.InstanceName = instancename;
	this.DivName = this.InstanceName;
	this.RtnTextboxObj = null;
	this.ShowMonth = null;
	this.ShowYear = null;
	this.CheckedDates = "";
	this.DOC_IE6 = (document.documentElement)? true:false;		// IE6
	this.DOC_IE = (document.all)? true:false;					// IE	
	this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE
	
	return this;
}

//*********************************************
//	Class level constants
//*********************************************

PageCalendar.MONTH_NAMES = Utils.makeArray('January','February','March','April','May','June','July','August','September','October','November','December');
//PageCalendar.MONTH_NAMES = Utils.makeArray('JANUARY','FEBRUARY','MARCH','APRIL','MAY','JUNE','JULY','AUGUST','SEPTEMBER','OCTOBER','NOVEMBER','DECEMBER');
PageCalendar.MONTH_DAYS = Utils.makeArray(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
PageCalendar.DAY_OF_WEEK = Utils.makeArray('Su','Mo','Tu','We','Th','Fr','Sa');
PageCalendar.TODAY_OBJ = new Date();
PageCalendar.SELECT_DATE_OBJ = null;

//** Convert YY to YYYY
PageCalendar.Y2K = function(number)
{
	return (number<1000) ? number+1900 : number;
}

//*********************************************
//	Public methods
//*********************************************

//-------------------
// Initial functions
//--------------------
PageCalendar.prototype.init = function()
{
	if(this.ImgLeft=="") this.ImgLeft = this.SkinBase + "arrow_left.gif";
	if(this.ImgRight=="") this.ImgRight = this.SkinBase + "arrow_right.gif";
	if(this.ImgClose=="") this.ImgClose = this.SkinBase + "Btn_Close.gif";
	if(this.ImgYearLeft=="") this.ImgYearLeft = this.SkinBase + "arrow_dbl_left.gif";
	if(this.ImgYearRight=="") this.ImgYearRight = this.SkinBase + "arrow_dbl_right.gif";

	this.writeCalDiv();
	
	this.DivObj = document.getElementById(this.DivName);
	this.hidTxtObj = document.getElementById(this.DivName+'Hidden');	
	this.hidTxtObj.value = this.CheckedDates;
	
	this.resetCal(this.ShowMonth, this.ShowYear);
}

//-------------------------
// reset calendar to today
//-------------------------
PageCalendar.prototype.resetCal = function(M,Y)
{
	this.DivObj.innerHTML = this.genCalHtml(M,Y);
}

//---------------------
//	Write the DIV tag
//---------------------
PageCalendar.prototype.writeCalDiv = function()
{
	document.write('<div id="'+this.DivName+'"></div>');
	document.write('<input type="hidden" name="'+this.DivName+'Hidden" id="'+this.DivName+'Hidden">');
}

//----------------------------------
//	Detecting if inside DIV tag
//-----------------------------------

PageCalendar.prototype.isInDivLayer = function(x,y)
{
	var divTop,divLeft;
	
	if (this.DOC_IE)
	{
		divTop = parseInt(this.DivObj.style.top);
		divLeft = parseInt(this.DivObj.style.left);
	}
	else if(this.DOC_DOM)
	{
		divTop = parseInt(this.DivObj.offsetTop);
		divLeft = parseInt(this.DivObj.offsetLeft);
	}

	/*
	if ( x>divLeft && x<(divLeft+this.DivObj.offsetWidth) &&  y>divTop && y<(divTop+this.DivObj.offsetHeight) )
	{
		return true;
	}
	return false;
	*/
	var xx = x;
	var yy = y;
	
	/*
	if (this.DOC_IE6)
	{
		xx = x + document.documentElement.scrollLeft;
		yy = y + document.documentElement.scrollTop;
	}
	else if (this.DOC_IE)
	{
		xx = x + document.body.scrollLeft;
		yy = y + document.body.scrollTop;		
	}
	*/
	
	if ( xx>divLeft && xx<(divLeft+this.DivObj.offsetWidth) &&  
		 yy>divTop && yy<(divTop+this.DivObj.offsetHeight) )
	{
		return true;
	}
	return false;	
}

//---------------------------------------------------------------------------------------
//	Main method for generate HTML Cal
//---------------------------------------------------------------------------------------
PageCalendar.prototype.genCalHtml = function(inMonth, inYear)
{
	this.ShowMonth = inMonth;
	this.ShowYear = inYear;
	
	var today_day   = PageCalendar.TODAY_OBJ.getDate();
	var today_month = PageCalendar.TODAY_OBJ.getMonth();
	var today_year  = PageCalendar.Y2K(PageCalendar.TODAY_OBJ.getYear());

	//
	// a Date object of the first day of the month
	//
	var firstDay = new Date(this.ShowYear,this.ShowMonth,1);
	var startDOW = firstDay.getDay(); //*** a Day of week: Mon, Tue.....

	//
	// For February, change the days of month
	//
	if( ((this.ShowYear%4==0)&&(this.ShowYear%100!=0)) || (this.ShowYear%400==0) )
		PageCalendar.MONTH_DAYS[1] = 29;
	else
		PageCalendar.MONTH_DAYS[1] = 28;

	//
	// calendar HTML string
	//
	var output = '';

	output += '<table cellspacing="5" class="'+this.CssCalBG+'" style="background-color:'+this.ClrDivBg+'; width:'+(this.TDWidth*7+20)+'px; padding:0px; border:1px solid #006600; border-right-width:2px; border-bottom-width:2px;">';
	
	// row #1 
	output += '<tr><td>';
	//
	// YearNav and MonthNav table
	//	
	output += '<table class="eaf_CalMonthYear" cellspacing="0">';
	output += '<tr>';
	
	if(this.YearNav==true)
	{
		output += '<td><a href="javascript:'+this.InstanceName+'.goYear(-1);"><img src="' + this.ImgYearLeft + '" border="0" alt="Previous Year"></a></td>';
		output += '<td>&nbsp;</td>';
	}
	if(this.MonthNav==true)
		output += '<td><a href="javascript:'+this.InstanceName+'.goMonth(-1);"><img src="' + this.ImgLeft + '" border="0" alt="Previous Month"></a></td>';
	
	output += '<td width="100%"><span class="' + this.CssTitleFont + '">&nbsp;'+ PageCalendar.MONTH_NAMES[this.ShowMonth] + '&nbsp;&nbsp;' + this.ShowYear + '&nbsp;</span></td>';
	
	if(this.MonthNav==true)
		output += '<td><a href="javascript:'+this.InstanceName+'.goMonth(1);"><img src="' + this.ImgRight + '" border="0" alt="Next Month"></a></td>';
	
	if(this.YearNav==true)
	{
		output += '<td>&nbsp;</td>';
		output += '<td><a href="javascript:'+this.InstanceName+'.goYear(1);"><img src="' + this.ImgYearRight + '" border="0" alt="Next Year"></a></td>';
	}

	output += '</tr>';
	output += '</table>';
	
	output += '</td></tr>';
	
	// row #2 
	output += '<tr><td>';
	
	//
	// DOW and Day table
	//	
	output += '<table class="eaf_CalDate" cellspacing="2">';
	output += '<tr>';

	//
	// loop for day of week (DOW): Mon, Tue.....
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
		output += '<span class="' + this.CssDOWFont + '">' + PageCalendar.DAY_OF_WEEK[i] +'</span>';
		output += '</td>';
	}

	output += '</tr>';

	//
	// prepare to loop Days
	//
	var column = 0;
	var row = 0;

	var prevMonth = this.ShowMonth-1;
	if(prevMonth==-1) prevMonth = 11;  //** Jan is 0, Feb is 1.....

	var prevYear = this.ShowYear;
	if (prevMonth==11) prevYear = prevYear-1;

	output += '<tr align="center" valign="middle">';
	
	//
	// Loop for previous month days
	//
	for(i=0; i<startDOW; i++, column++)
	{
		output += '<td width="' + this.TDWidth + '" height="' + this.TDHeight + '" bgcolor="' + this.ClrOtherMonthBg + '">';
		output += '<span class="'+this.CssOtherDateFont+'">' + (PageCalendar.MONTH_DAYS[prevMonth]-startDOW+i+1) + '</span>';
		output += '</td>';
	}

	//
	// Loop for current month days
	//
	for(i=1; i<=PageCalendar.MONTH_DAYS[this.ShowMonth]; i++, column++)
	{
		var tdColor = this.ClrCurrentMonthBg;
	
		//
		// set TD background CSS based on some properties
		//		
		output += '<td id="td'+this.DivName+i+'" style="width:'+this.TDWidth+'px; height:'+this.TDHeight+'px;" ';		
		
		// when show "only-after" date
		if(this.CalType.toUpperCase()=='FUTURE' && ((i<today_day && this.ShowMonth<=today_month && this.ShowYear<=today_year) || (this.ShowMonth<today_month && this.ShowYear<=today_year) || this.ShowYear<today_year) )
		{
			output += 'class="' +this.CssCrossBG+ '" bgcolor="' +tdColor+ '">';
		}
		// when show "today" date
		else if(i==today_day && this.ShowMonth==today_month && this.ShowYear==today_year)
		{
			if(this.isDateChecked(i))   output += 'class="'+this.CssSelectDateBG+'" '; // add a circle background css
			output += 'bgcolor="' +this.ClrTodayBg+ '">';
		}
		// when the day is checked
		else if(this.isDateChecked(i))
		{
			output += 'class="' +this.CssSelectDateBG+ '" bgcolor="' +tdColor+ '">';
		}
		//** when show "the rest" date
		else
		{
			output += 'bgcolor="' +tdColor+ '">';
		}
		
		
		if(this.CalType.toUpperCase()=='FUTURE' && ((i<today_day&&this.ShowMonth<=today_month&&this.ShowYear<=today_year) || (this.ShowMonth<today_month&&this.ShowYear<=today_year) || this.ShowYear<today_year) ) // show after link
		{
			output += '<span class="' +this.CssOtherDateFont+ '">' +i+ '</span>';
		}
		else if((this.CalType.toUpperCase()=='PAST' || this.CalType.toUpperCase()=='PASS') && ((i>today_day&&this.ShowMonth>=today_month&&this.ShowYear>=today_year) || (this.ShowMonth>today_month&&this.ShowYear>=today_year) || this.ShowYear>today_year) ) // show after link
		{
			output += '<span class="' +this.CssOtherDateFont+ '">' +i+ '</span>';
		}
		else
		{
			output += '<a href="javascript:' +this.InstanceName+ '.clickDay(' +i+ ');">';
			output += '<span class="' +this.CssCurrentDateFont+ '">' +i+ '</span>';
			output += '</a>';
		}
	    
	    output += '</td>';

	    if(column==6)
	    {
	     	output += '</tr>';
			if((i+1)<=PageCalendar.MONTH_DAYS[this.ShowMonth])  output += '<tr align="center" valign="middle">';
	        column = -1;
	        row++;
	    }
	}

	//
	// Loop for next month days if needed
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
		output += '</tr>';
	}
	
	//
	// generate the 6th row, for Look & Feel
	//	  
	if(this.FixedSize)
	{
		if(row<5 || (row==5 && column==0))
		{
			output += '<tr align="center" valign="middle">';
			for(i=1; i<=7; i++)
			{
				output += '<td style="width:' +this.TDWidth+ 'px; height:' +this.TDHeight+ 'px; background-color:' +this.ClrOtherMonthBg+ ';" >';
				output += '<span class="' + this.CssOtherDateFont + '">' +parseInt((i+iRcd),10)+ '</span>';
				output += '</td>';
			}
			output += '</tr>';
		}
	}

	output += '</table>';
	
	output += '</td></tr>';
	
	output += '</table>';

	return output;
}

//-----------------
//  Change Year
//-----------------
PageCalendar.prototype.goYear = function(num)
{
	var gotoYY = parseInt(this.ShowYear,10) + parseInt(num,10);

	if(gotoYY<this.CalStartYear) gotoYY = this.CalStartYear;
	if(gotoYY>this.CalEndYear) gotoYY = this.CalEndYear;

this.DivObj.innerHTML = this.genCalHtml(this.ShowMonth,gotoYY);
}

//-------------------
//  Change Month
//-------------------
PageCalendar.prototype.goMonth = function(num)
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
PageCalendar.prototype.goToday = function(m,y)
{
   	this.DivObj.innerHTML = this.genCalHtml(m,y);
}

//------------------------------------------
//  Set selected day back to the text field
//------------------------------------------
PageCalendar.prototype.changeDay = function(day)
{
	if(this.RtnTextboxObj!=null)
	{
		if(this.DateFormat.toUpperCase()=='MDY')
			this.RtnTextboxObj.value = parseInt(this.ShowMonth+1) + this.DateSpliter + day + this.DateSpliter + this.ShowYear;
		else if(this.DateFormat.toUpperCase()=='DMY')
			this.RtnTextboxObj.value = day + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + this.ShowYear;
		else
			this.RtnTextboxObj.value = this.ShowYear + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + day;
	}

	this.RtnTextboxObj = null;	
	this.hideDivCal();
}

PageCalendar.prototype.clickDay = function(day)
{
	var dtestr = "";
	
	if(this.DateFormat.toUpperCase()=='MDY')
		dtestr = parseInt(this.ShowMonth+1) + this.DateSpliter + day + this.DateSpliter + this.ShowYear + ";";
	else if(this.DateFormat.toUpperCase()=='DMY')
		dtestr = day + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + this.ShowYear + ";";
	else
		dtestr = this.ShowYear + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + day + ";";
    
    var tdobj = document.getElementById('td'+this.DivName+day);
    
    if(!this.isDateChecked(day))
    {  
        this.hidTxtObj.value += dtestr;
        tdobj.className = this.CssSelectDateBG;
    }
    else
    {
        var s = this.hidTxtObj.value.replace(dtestr,"");
        this.hidTxtObj.value = s;
        tdobj.className = "";
    }
}


PageCalendar.prototype.isDateChecked = function(day)
{
	var dtestr = "";
	
	if(this.DateFormat.toUpperCase()=='MDY')
		dtestr = parseInt(this.ShowMonth+1) + this.DateSpliter + day + this.DateSpliter + this.ShowYear + ";";
	else if(this.DateFormat.toUpperCase()=='DMY')
		dtestr = day + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + this.ShowYear + ";";
	else
		dtestr = this.ShowYear + this.DateSpliter + parseInt(this.ShowMonth+1) + this.DateSpliter + day + ";";
    
    if(this.hidTxtObj.value.indexOf(dtestr)<0)
        return false;
    else
        return true;
    
}

//-----------------------------------------
//  Hide DIV calendar
//-----------------------------------------
PageCalendar.prototype.hideDivCal = function()
{
	this.DivObj.style.visibility='hidden';
	//this.IframeObj.style.display = "none";
	//this.ShowSelectItems(true);
}

PageCalendar.prototype.showDivCal = function()
{
	this.DivObj.style.visibility='visible';
	//this.IframeObj.style.display = "block";
	//this.ShowSelectItems(false);
}

//-----------------------------------------
// Major function for javascript to call
//-----------------------------------------
PageCalendar.prototype.switchDivCal = function(calType,imgName,fldObj,offsetX,offsetY,reload)
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
				
				PageCalendar.SELECT_DATE_OBJ = new Date(parseInt(iY),parseInt(iM)-1,parseInt(iD),0,0,0,0);
				
				this.resetCal(parseInt(iM)-1,iY);
		}
		else
		{
				PageCalendar.SELECT_DATE_OBJ = null;
				if(reload) this.resetCal(PageCalendar.TODAY_OBJ.getMonth(),PageCalendar.TODAY_OBJ.getYear());
		}
		
		var imgObj = eval('document.' + imgName);
		
		// show calendar by imgobj
		// not working well when this image is deeply embedded in nested table
		
//		this.DivObj.style.left = eval(findPosX(imgObj)+offsetX) + "px";
//		this.DivObj.style.top = eval(findPosY(imgObj)+imgObj.height+offsetY) + "px";

		// show calendar by mouse click
		if(this.DOC_IE6)
		{
			this.DivObj.style.left = eval(event.clientX+document.documentElement.scrollLeft+offsetX) + "px";
			this.DivObj.style.top = eval(event.clientY+document.documentElement.scrollTop+offsetY) + "px";
		}
		else if(this.DOC_IE)
		{
			this.DivObj.style.left = eval(event.clientX+document.body.scrollLeft+offsetX) + "px";
			this.DivObj.style.top = eval(event.clientY+document.body.scrollTop+offsetY) + "px";
		}
	
		this.adjustIframe();		
		this.showDivCal();
		
	
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
		if(reload==true) this.resetCal(PageCalendar.TODAY_OBJ.getMonth(),PageCalendar.TODAY_OBJ.getYear());
		
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
/*
PageCalendar.prototype.ShowSelectItems = function(boo)
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
		
		
		var fm = eval('document.'+this.FormName);
		for (i=0; i<fm.elements.length; i++)
		{
			if (fm.elements[i].type=='select-one')
				if(boo==true)
					fm.elements[i].style.visibility = "visible";
				else
					fm.elements[i].style.visibility = "hidden";
		}
		
	}
}
*/
//----------------------------------------------
//  Mouse Down Event
//----------------------------------------------
PageCalendar.prototype.MouseDownEvent = function()
{
	if(this.DivObj.style.visibility=='visible')
	{
		var x,y;
		
		if (this.DOC_IE6)
		{
			x = eval(event.clientX+document.documentElement.scrollLeft);
			y = eval(event.clientY+document.documentElement.scrollTop);			
		}
		else if (this.DOC_IE)
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
			this.hideDivCal();
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
//var eaf_DatePicker = new PageCalendar('eaf_DatePicker');


// Mouse Event for DivCal
//--------------------------
/*
function DivCalMouseDown()
{
	eaf_DatePicker.MouseDownEvent();
}

if(document.attachEvent)
	document.attachEvent('onmousedown',DivCalMouseDown);
else if(document.addEventListener)
	document.addEventListener('mousedown',DivCalMouseDown,false);
*/

/*
document.onmousedown = function(e)
{
	divCalInstance.MouseDownEvent(e);
}
*/