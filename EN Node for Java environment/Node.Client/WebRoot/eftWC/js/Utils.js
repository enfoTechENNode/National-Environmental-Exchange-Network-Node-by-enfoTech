//**************************************************************************
//
// javascript BrowserObj function
//
//**************************************************************************

function BrowserObj() { return this; };


BrowserObj.IE6 = (document.documentElement)? true:false;									
BrowserObj.IE = (document.all)? true:false;					// IE	
BrowserObj.DOM = (document.getElementById) ? true : false;	// NS7 , also IE


BrowserObj.GetViewWidth = function()
{
    if (BrowserObj.IE6)	        return parseInt(document.documentElement.clientWidth,10);
	else if (BrowserObj.IE)	    return parseInt(document.body.clientWidth,10);
	else if (BrowserObj.DOM)    return parseInt(window.innerWidth,10);
};

BrowserObj.GetViewHeight = function()
{
    if (BrowserObj.IE6)	        return parseInt(document.documentElement.clientHeight,10);
	else if (BrowserObj.IE)	    return parseInt(document.body.clientHeight,10);
	else if (BrowserObj.DOM)    return parseInt(window.innerHeight,10);
};

BrowserObj.GetCurrentLeftPos = function()
{
    if (BrowserObj.IE6)	        return parseInt(document.documentElement.scrollLeft,10);
	else if (BrowserObj.IE)	    return parseInt(document.body.scrollLeft,10);
	else if (BrowserObj.DOM)    return parseInt(window.pageXOffset,10);
};

BrowserObj.GetCurrentTopPos = function()
{
    if (BrowserObj.IE6)	        return parseInt(document.documentElement.scrollTop,10);
	else if (BrowserObj.IE)	    return parseInt(document.body.scrollTop,10);
	else if (BrowserObj.DOM)    return parseInt(window.pageYOffset,10);
};

//**************************************************************************
//
// javascript utility function
// all of them are working fine with IE5 and above,
// but not of of them working with NS.
// please becareful.
//
//**************************************************************************


Utils.DIALOG_WIN = null;
Utils.NUMBER_DASH = null;
Utils.TIMER_ID = 0;

// define Utils class
//---------------------
function Utils()
{
	return this;
}

//=======================================================================
// generic functions
//=======================================================================


// Build array function
//----------------------
Utils.makeArray = function()
{
	var ary = new Array(Utils.makeArray.arguments.length);
	for(i=0; i<Utils.makeArray.arguments.length; i++)
		ary[i] = Utils.makeArray.arguments[i];

	return ary;

	/*
	for(i=0; i<Utils.makeArray.arguments.length; i++)
		this[i] = Utils.makeArray.arguments[i];
	*/
};

// Build array function
//----------------------
Utils.appendZero = function(str, digi)
{
	var rtn = str;
	if(str.length<digi)
	{
		var cnt = eval(digi-str.length);
		for(i=0; i<cnt; i++)
		{
			rtn = '0'+rtn;
		};
	};
	return rtn;
};

//=======================================================================
// working on form (input) element
//=======================================================================


// auto fix phone format(XXX-XXX-XXXX)
// please use onKeyup action
//------------------------------------
Utils.checkPhoneFormat = function(fldObj)
{
	if(Utils.NUMBER_DASH==null)
		Utils.NUMBER_DASH = new Utils.makeArray('0','1','2','3','4','5','6','7','8','9');

	var str = fldObj.value;

	if(str.length>0)
	{
		/*
		if(str.substring(str.length-1)==' ') fldObj.value = str.substring(0,str.length-1);
		else if(str.substring(str.length-1)=='(') fldObj.value = str.substring(0,str.length-1);
		else if(str.substring(str.length-1)==')') fldObj.value = str.substring(0,str.length-1);
		*/

		if( !Utils.foundChar(str.substring(str.length-1),Utils.NUMBER_DASH) )
			fldObj.value = str.substring(0,str.length-1);
	}

	str = fldObj.value;
	if(str.length>=4 && str.substring(3,4)!="-")
	{
		fldObj.value = str.substring(0,3) + "-" + str.substring(3);
	}

	str = fldObj.value;
	if(str.length>=8 && str.substring(7,8)!="-")
	{
		fldObj.value = str.substring(0,7) + "-" + str.substring(7);
	}

	str = fldObj.value;
	if(str.length>12)
		fldObj.value = str.substring(0,12);
}


Utils.foundChar = function(ch,ary)
{
	var fnd = false;

	for(i=0; i<ary.length; i++)
	{
		if(ch==ary[i]){
			fnd = true;
			break;
		}
	}

	return fnd;
}

// auto zip code format(XXXXX-XXXX)
// please use onKeyup action
//------------------------------------
Utils.checkZIPFormat = function(fldObj)
{
	if(Utils.NUMBER_DASH==null)
		Utils.NUMBER_DASH = new Utils.makeArray('0','1','2','3','4','5','6','7','8','9','0','-');

	var str = fldObj.value;

	if(str.length>0)
	{
		if( !Utils.foundChar(str.substring(str.length-1),Utils.NUMBER_DASH) )
			fldObj.value = str.substring(0,str.length-1);
	}

	str = fldObj.value;
	if(str.length>=6 && str.substring(5,6)!="-")
	{
		fldObj.value = str.substring(0,5) + "-" + str.substring(5);
	}

	str = fldObj.value;
	if(str.length>10)
		fldObj.value = str.substring(0,10);
}

// auto check date format after onBlur
// please use onBlur action
// ex:
//		onBlur="Utils.CheckDateFormat(this,'YMD','-',2004,2020)"
// dType:
//		YMD, DMY, MDY
//-----------------------------------------------------------
Utils.checkDateFormat = function(fldObj,dType,spliter,startY, endY)
{
	var msg = "Date format is not valid. ";

	if(dType.toUpperCase()=='MDY')
		msg = msg+"(MM"+spliter+"DD"+spliter+"YYYY)";
	else if(dType.toUpperCase()=='DMY')
		msg = msg+"(DD"+spliter+"MM"+spliter+"YYYY)";
	else
		msg = msg+"(YYYY"+spliter+"MM"+spliter+"DD)";

	if(fldObj!=null && fldObj.value!="")
	{
		var dteStr = fldObj.value;
		var sAry = dteStr.split(spliter);
		if(sAry.length!=3)
		{
			alert(msg);
			fldObj.focus();
			fldObj.value = "";
		}
		else
		{
			var y,m,d;
			var showAlert = false;
			var leapYear = false;
                        var months = new Array(31,28,31,33,31,30,31,31,30,31,30,31);

			if(dType.toUpperCase()=='MDY')
			{
				y = sAry[2]; m = sAry[0]; d = sAry[1];
			}
			else if(dType.toUpperCase()=='DMY')
			{
				y = sAry[2]; m = sAry[1]; d = sAry[2];
			}
			else
			{
				y = sAry[0]; m = sAry[1]; d = sAry[2];
			}

			if(y < startY || y > endY)
			{
				msg = msg+"\nPlease input Year between "+startY+" and " +endY+".";
				showAlert = true;
			}

                        if((y % 4 == 0) && (y % 100 != 0) || (y % 400 == 0))
                        {
                          leapYear = true;
                        }

			if(m < 1 || m > 12)
			{
				msg = msg+"\nPlease input Month between 1 and 12.";
				showAlert = true;
			}

			if(d < 1 || d > 31)
			{
				msg = msg+"\nPlease input Date between 1 and 31.";
				showAlert = true;
			}

                        if(d > months[m-1] && !((m == 2) && (d > 28))){
                          	msg = msg+"\nPlease input the correct format of Date/Month.";
				showAlert = true;
                        }

                        if(!leapYear && m == 2 && d > 28){
                        	msg = msg+"\nPlease input the correct format of Date/Month.(Year " + m + " is not a leap year)";
				showAlert = true;
                        }

                        if(leapYear && m == 2 && d > 29){
                        	msg = msg+"\nPlease input the correct format of Date/Month.";
				showAlert = true;
                        }

			if(showAlert)
			{
				alert(msg);
				fldObj.focus();
				fldObj.value = "";
			}
		}
	}
}

//=======================================================================
// browser actions
//=======================================================================

// go to url
//--------------------------------
Utils.gotoURL = function(target,url)
{
	eval(target+".location='"+url+"'");
}

// open new browser window
//-------------------------------
Utils.openNewWindow = function(theURL,width,height,winName,tbar,stsbar,resize)
{
	var winTitle = '';
	var w = 600;
	var h = 400;
	var tb = 'no';
	var rz = 'yes';
	var sts = 'yes';

	if(typeof winName!='undefined')	winTitle = winName;
	if(typeof width!='undefined')	w = width;
	if(typeof height!='undefined')	h = height;
	if(typeof tbar!='undefined')	tb = tbar;
	if(typeof resize!='undefined')	rz = resize;
	if(typeof stsbar!='undefined')	sts = stsbar;

	var str = 'menubar=no,location=no,toolbar='+tb+',scrollbars=yes,status='+sts+',resizable='+rz+',width='+w+',height='+h;
	return window.open(theURL,winTitle,str);
}

// open new dialog browser window (IE)
//--------------------------------------
Utils.showModalDialog = function(theURL,width,height,resize,stsbar)
{
	var w = 600;
	var h = 400;
	var rz = 'yes';
	var sts = 'yes';

	if(typeof width!='undefined')	w = width;
	if(typeof height!='undefined')	h = height;
	if(typeof resize!='undefined')	rz = resize;
	if(typeof stsbar!='undefined')	sts = stsbar;

	var str="dialogHeight:"+h+"px; dialogWidth:"+w+"px; edge:Raised; center:No; help:No; resizable:"+rz+"; status:"+sts;
	return window.showModalDialog(theURL,"",str);
}

// open javascript dialog window
//--------------------------------------
Utils.onWinFocus = function()
{
	if(Utils.DIALOG_WIN!=null && !Utils.DIALOG_WIN.closed)
		Utils.DIALOG_WIN.focus();
};

Utils.onWinBlur = function()
{
	if(Utils.DIALOG_WIN!=null && !Utils.DIALOG_WIN.closed)
		Utils.DIALOG_WIN.close();
};

Utils.openDialogWin = function(theURL,width,height,winName,tbar,stsbar,resize)
{
	if(Utils.DIALOG_WIN==null || Utils.DIALOG_WIN.closed)
		Utils.DIALOG_WIN = Utils.openNewWindow(theURL,width,height,winName,tbar,stsbar,resize);
	else
		Utils.DIALOG_WIN.focus();

	window.onfocus = Utils.onWinFocus;
	window.onunload = Utils.onWinBlur;

	return Utils.DIALOG_WIN;
};

Utils.closeDialogWin = function(reloadP)
{
	if(reloadP) self.opener.location.reload();
	self.close();
}



// timer function
// you have to implement Utils.timeupAction() function
//---------------------------------------------------------
Utils.setCounter = function(sec)
{
	if(sec>0)
	{
		sec--;
		Utils.TIMER_ID = setTimeout("Utils.setCounter("+sec+")",1000);
	}
	else
	{
		if(Utils.TIMER_ID!=null) clearTimeout(Utils.TIMER_ID);
		Utils.timeupAction();
	}
}

Utils.txtFldCounter = function(fldName)
{
	var obj = eval(fldName);
	var sec = parseInt(obj.value);
	if(sec>0)
	{
		obj.value = sec-1;
		Utils.TIMER_ID = setTimeout("Utils.txtFldCounter('"+fldName+"')",1000);
	}
	else
	{
		if(Utils.TIMER_ID!=null) clearTimeout(Utils.TIMER_ID);
		Utils.timeupAction();
	}
}

//=======================================================================
// browser UI actions
//=======================================================================

// go to url
//--------------------------------
Utils.setDivText = function(divId,txt)
{
	var obj = document.getElementById(divId);
	obj.innerHTML = txt;
}

// toggle DIV element
//------------------------
Utils.toggleDiv = function(name)
{
	var StyleObj = document.getElementById(name).style;

	if (StyleObj.display=="none")
	{
		StyleObj.display="block";
	}
	else
	{
		StyleObj.display="none";
	}
}

// show DIV element
//------------------------
Utils.showDiv = function(name)
{
	var StyleObj = document.getElementById(name).style;
	StyleObj.display="block";
}

// hide DIV element
//------------------------
Utils.hideDiv = function(name)
{
	var StyleObj = document.getElementById(name).style;
	StyleObj.display="none";
}

// check if a point(x.y) inside a DIV
//------------------------
Utils.isInDiv = function(name,x,y)
{
	var divObj = document.getElementById(name);
	var divTop,divLeft;

	divTop = parseInt(divObj.offsetTop);
	divLeft = parseInt(divObj.offsetLeft);

	if( x>divLeft && x<(divLeft+divObj.offsetWidth) &&  y>divTop && y<(divTop+divObj.offsetHeight) )
		return true;
	else
		return false;
}

// toggle DIV element with image properties
//----------------------------------------------
Utils.toggleDivImg = function(divName, imgName, expImgSrc, clpImgSrc)
{
	var styleObj = document.getElementById(divName).style;
	var imgObj = document.getElementById(imgName);


	if (styleObj.display=="none")
	{
		styleObj.display="block";
		imgObj.src = expImgSrc;
	}
	else
	{
		styleObj.display="none";
		imgObj.src = clpImgSrc;
	}
}
// set object class
//------------------------
Utils.setObjClass = function(obj, clsName)
{
	obj.className = clsName;
}


// set class by object ID
//------------------------------
Utils.setIDClass = function(id, clsName)
{
	var obj = document.getElementById(id);
	obj.className = clsName;
}

// show/hide select box (IE only)
//---------------------------------
Utils.hideAllSelectBox = function(frm,bool)
{
	var fm = eval('document.'+frm);

	for (i=0; i<fm.elements.length; i++)
	{
          	if (fm.elements[i].type.indexOf('select')>=0)
		{
			if(bool)
				fm.elements[i].style.visibility = "hidden";
			else
				fm.elements[i].style.visibility = "visible";
		}
	}
}


Utils.showHideObject = function(formName, ary, show)
{
	if(ary!=null)
	{
		for(i=0; i<ary.length; i++)
		{
			var fld = eval('document.' + formName + '.' + ary[i]);
			if(show)
				fld.style.visibility = "visible";
			else
				fld.style.visibility = "hidden";
		}
	}
}

//=======================================================================
// Gradual-Highlight obj
//=======================================================================
/*
Gradual-Highlight Image Script II-
By J. Mark Birenbaum (birenbau@ugrad.cs.ualberta.ca)
Permission granted to Dynamicdrive.com to feature script in archive
For full source to script, visit http://dynamicdrive.com
*/

WCJS_GradualFadeObjects = new Object();
WCJS_GradualTimers = new Object();

/* object - (actual object, not name);
* destop - destination transparency level (ie 80, for mostly solid)
* rate   - time in milliseconds between trasparency changes (best under 100)
* delta  - amount of change each time (ie 5, for 5% change in transparency)
*/

Utils.loopFade = function(object, destOpFrom, destOpTo, rate, delta)
{
	if (!document.all)	return; // for IE only

	if (object != "[object]")
	{  //do this so I can take a string too
		setTimeout("Utils.loopFade("+object+","+destOpFrom+","+destOpTo+","+rate+","+delta+")",0);
		return;
	}

	clearTimeout(WCJS_GradualTimers[object.sourceIndex]);

	if (object.filters.alpha.opacity==destOpFrom)
		diff = destOpTo-object.filters.alpha.opacity;
	else if (object.filters.alpha.opacity==destOpTo)
		diff = destOpFrom-object.filters.alpha.opacity;

	direction = 1;
	if (diff<0)
	{
		direction = -1;
	}

	delta = Math.min(direction*diff,delta);
	object.filters.alpha.opacity += direction*delta;

	WCJS_GradualFadeObjects[object.sourceIndex] = object;
	WCJS_GradualTimers[object.sourceIndex] = setTimeout("Utils.loopFade(WCJS_GradualFadeObjects["+object.sourceIndex+"],"+destOpFrom+","+destOpTo+","+rate+","+delta+")",rate);
}


Utils.objFade = function(object, destOp, rate, delta)
{
	if (!document.all)	return; // for IE only

	if (object != "[object]")
	{  //do this so I can take a string too
		setTimeout("Utils.objFade("+object+","+destOp+","+rate+","+delta+")",0);
		return;
	}

	clearTimeout(WCJS_GradualTimers[object.sourceIndex]);

	diff = destOp-object.filters.alpha.opacity;

	direction = 1;
	if (object.filters.alpha.opacity>destOp) direction = -1;

	delta = Math.min(direction*diff,delta);
	object.filters.alpha.opacity += direction*delta;

	if (object.filters.alpha.opacity!=destOp)
	{
		WCJS_GradualFadeObjects[object.sourceIndex] = object;
		WCJS_GradualTimers[object.sourceIndex] = setTimeout("Utils.objFade(WCJS_GradualFadeObjects["+object.sourceIndex+"],"+destOp+","+rate+","+delta+")",rate);
	}
}

// Submit when hit enter
//----------------------------------------------------
Utils.enterSubmit = function(actionType){
  if(window.event && window.event.keyCode == 13){
    submitForm(actionType);
  }else
    return true;
}


//=======================================================================
// behind the scene actions
//=======================================================================

// Find obj X in the page: Image, and A HREF......
//-----------------------------------------------------
Utils.findPosX = function(obj)
{
	var curleft = 0;
	if (document.getElementById || document.all)
	{
		while (obj.offsetParent)
		{
			//alert("obj.offsetParent:"+obj.offsetParent+" toleft:"+obj.offsetLeft);
			curleft += obj.offsetLeft
			obj = obj.offsetParent;
		}
	}
	else if (document.layers)
	{
		curleft += obj.x;
	}
	return curleft;
}

// Find obj Y in the page: Image, and A HREF......
//---------------------------------------------------
Utils.findPosY = function(obj)
{
	var curtop = 0;
	if (document.getElementById || document.all)
	{
		while (obj.offsetParent)
		{
			curtop += obj.offsetTop
			obj = obj.offsetParent;
		}
	}
	else if (document.layers)
	{
		curtop += obj.y;
	}
	return curtop;
}

// Find obj by name
// from MM v4.01
//----------------------------
Utils.findObj = function(name, doc)
{
	var p,i;
	var rtnObj;

	if(!doc) doc = document;

	if((p=name.indexOf("?"))>0 && parent.frames.length)
	{
		doc = parent.frames[name.substring(p+1)].document;
		name = name.substring(0,p);
	}
	if(!(rtnObj=doc[name]) && doc.all)
		rtnObj = doc.all[name];

	for (i=0; !rtnObj && i<doc.forms.length; i++)
		rtnObj = doc.forms[i][name];

	for(i=0; !rtnObj && doc.layers&&i<doc.layers.length; i++)
		rtnObj = findObj(name,doc.layers[i].document);

	if(!rtnObj && doc.getElementById)
		rtnObj = doc.getElementById(name);

	return rtnObj;
}
