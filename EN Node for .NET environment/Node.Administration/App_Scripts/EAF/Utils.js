

//##########################################################################
//
// javascript utility function
// all of them are working fine with IE5 and above,
// but not all of them working with NS.
// please becareful.
//
//##########################################################################


//**************************************************************************
// global vars
//**************************************************************************

Utils.DIALOG_WIN = null;
Utils.NUMBER_DASH = null;
Utils.IMG_ARY = null;
Utils.IMG_SWAP_SRC = null;
Utils.TIMER_ID = 0;

//**************************************************************************
// constructor
//**************************************************************************

function Utils() {
    return this;
};

//**************************************************************************
// generic functions
//**************************************************************************

//--------------------------------------------------------------------------
// Build array function
//--------------------------------------------------------------------------
Utils.makeArray = function() {
    var ary = new Array(Utils.makeArray.arguments.length);
    for (i = 0; i < Utils.makeArray.arguments.length; i++)
        ary[i] = Utils.makeArray.arguments[i];

    return ary;

    /*	
    for(i=0; i<Utils.makeArray.arguments.length; i++)
    this[i] = Utils.makeArray.arguments[i];
    */
};


//--------------------------------------------------------------------------
// append front zero to a string
//--------------------------------------------------------------------------
Utils.appendZero = function(str, digi) {
    var rtn = str;
    if (str.length < digi) {
        var cnt = eval(digi - str.length);
        for (i = 0; i < cnt; i++) {
            rtn = '0' + rtn;
        };
    };
    return rtn;
};

//--------------------------------------------------------------------------
// show comfirm message
//--------------------------------------------------------------------------
Utils.confirmMsg = function(msg) {
    return confirm(msg);
}

//--------------------------------------------------------------------------
// show comfirm message
//--------------------------------------------------------------------------
Utils.trimString = function(str) {
    str = str.replace(/^\s+/g, ""); // strip leading
    return str.replace(/\s+$/g, ""); // strip trailing

}


//--------------------------------------------------------------------------
// find character in a ary
//--------------------------------------------------------------------------
Utils.foundChar = function(ch, ary) {
    var fnd = false;

    for (i = 0; i < ary.length; i++) {
        if (ch == ary[i]) {
            fnd = true;
            break;
        }
    }

    return fnd;
}

//--------------------------------------------------------------------------
// check max chars
//--------------------------------------------------------------------------
Utils.maxLength = function(txtObj, len) {
    var str = txtObj.value;
    if (str.length > len) {
        txtObj.value = str.substring(0, len - 1);
    };
};

//**************************************************************************
// DIV functions
//**************************************************************************

//--------------------------------------------------------------------------
// change div text
//--------------------------------------------------------------------------
Utils.setDivText = function(divId, txt) {
    document.getElementById(divId).innerHTML = txt;
}

Utils.setDivHTML = function(divId, txt) {
    document.getElementById(divId).innerHTML = txt;
}

//--------------------------------------------------------------------------
// toggle DIV element
//--------------------------------------------------------------------------
Utils.toggleDiv = function(name) {
    var StyleObj = document.getElementById(name).style;

    if (StyleObj.display == "none") {
        StyleObj.display = "block";
    }
    else {
        StyleObj.display = "none";
    }
}

//--------------------------------------------------------------------------
// show DIV element
//--------------------------------------------------------------------------
Utils.showDiv = function(name) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.display = "block";
}

//--------------------------------------------------------------------------
// hide DIV element
//--------------------------------------------------------------------------
Utils.hideDiv = function(name) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.display = "none";
}

//--------------------------------------------------------------------------
// set DIV position
//--------------------------------------------------------------------------
Utils.setDivTL = function(name, t, l) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.left = l + "px";
    StyleObj.top = t + "px";
}


Utils.insertContent = function(fldID, val) {
    var fldobj = document.getElementById(fldID);

    //IE support
    if (document.selection) {
        fldobj.focus();
        var sel = document.selection.createRange();
        sel.text = val + ' ';
        fldobj.focus();
    }
    //MOZILLA/NETSCAPE support
    else if (fldobj.selectionStart || fldobj.selectionStart == '0') {
        var startPos = fldobj.selectionStart;
        var endPos = fldobj.selectionEnd;
        fldobj.value = fldobj.value.substring(0, startPos) + fldobj + fldobj.value.substring(endPos, fldobj.value.length);
        fldobj.focus();
        fldobj.selectionStart = startPos + val.length;
        fldobj.selectionEnd = startPos + val.length;
    }
    else {
        fldobj.value += val;
        fldobj.focus();
    }
}

//--------------------------------------------------------------------------
// select box functions
//--------------------------------------------------------------------------

Utils.getSelectedItem = function(sbID) {
    var sbObj = document.getElementById(sbID);
    var optObj = null;

    for (i = 0; i < sbObj.options.length; i++) {
        if (sbObj.options[i].selected) {
            optObj = sbObj.options[i];
            break;
        }
    }
    return optObj;
}

Utils.getSelectedValue = function(sbID) {
    return Utils.getSelectedItem(sbID).value;
}

Utils.setSelectedItemValue = function(sbID, val) {
    var sbObj = document.getElementById(sbID);

    for (i = 0; i < sbObj.options.length; i++) {
        if (sbObj.options[i].value == val) {
            sbObj.options[i].selected = true;
            break;
        }
    }
}

Utils.setSelectedItemText = function(sbID, txt) {
    var sbObj = document.getElementById(sbID);

    for (i = 0; i < sbObj.options.length; i++) {
        if (sbObj.options[i].text == txt) {
            sbObj.options[i].selected = true;
            break;
        }
    }
}

Utils.clearSelect = function(sbID) {
    var sbObj = document.getElementById(sbID);
    while (sbObj.length > 0) {
        sbObj.remove(0);
    }
}

Utils.appendToSelect = function(sbObj, txt, val) {
    var opt;
    opt = document.createElement("option");
    opt.value = val;
    opt.appendChild(document.createTextNode(txt));
    sbObj.appendChild(opt);
}

Utils.populateSelect = function(sbID, domObj) {
    var selObj = document.getElementById(sbID);
    var rows = domObj.getElementsByTagName("Table");

    for (var i = 0; i < rows.length; i++) {
        var txt = rows[i].getElementsByTagName("Text")[0].firstChild.nodeValue;
        var val = rows[i].getElementsByTagName("Value")[0].firstChild.nodeValue;
        Utils.appendToSelect(selObj, txt, val)
    }
}

//--------------------------------------------------------------------------
// set DIV position
//--------------------------------------------------------------------------

Utils.setDivWidth = function(name, w) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.width = w + "px";
}

Utils.setDivHeight = function(name, h) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.height = h + "px";
}

Utils.setDivWidthHeight = function(name, w, h) {
    var StyleObj = document.getElementById(name).style;
    StyleObj.width = w + "px";
    StyleObj.height = h + "px";
}

//--------------------------------------------------------------------------
// check if a point(x.y) inside a DIV
//--------------------------------------------------------------------------
Utils.isInDiv = function(name, x, y) {
    var divObj = document.getElementById(name);
    var divTop, divLeft;

    divTop = parseInt(divObj.offsetTop);
    divLeft = parseInt(divObj.offsetLeft);

    if (x > divLeft &&
		x < (divLeft + divObj.offsetWidth) &&
		y > divTop &&
		y < (divTop + divObj.offsetHeight))
        return true;
    else
        return false;
};

//--------------------------------------------------------------------------
// Floating DIV Win
//--------------------------------------------------------------------------
/*
Utils.writeFloatWinDiv = function()
{
document.write('<div id="eaf_FloatWin">');
    
document.write('<div id="eaf_FloatWinHB">');
document.write('<table id="eaf_FloatWinTab" cellspacing="0">');
document.write('<tr>');
document.write('<td class="Head"><div>&nbsp;</div></td>');
document.write('<td class="Ttl"><div id="eaf_FloatWinTtl">Win Title</div></td>');
document.write('<td class="Btn"><div><a href="javascript:void(0);" onClick="Utils.hideDiv(\'eaf_FloatWin\');" title="Close panel">x</a></div></td>');
document.write('<td class="Tail"><div>&nbsp;</div></td>');
document.write('</tr>');
document.write('</table>');
document.write('</div>');
	
document.write('<div id="eaf_FloatWinCnt"><iframe id="eaf_FloatWinIFrame" src="" frameborder="0"></iframe></div>');
	
document.write('</div>');
};

Utils.showFloatWin = function(lnkObj,ttl,src,offX,offY,w,h)
{
Utils.setDivText('eaf_FloatWinTtl',ttl);
document.getElementById('eaf_FloatWinIFrame').src = src;
	
var left = eval(Utils.findPosX(lnkObj)+offX);
var top = eval(Utils.findPosY(lnkObj)+offY);
	
Utils.setDivWidthHeight('eaf_FloatWin',w,h)
Utils.setDivWidth('eaf_FloatWinIFrame',w-6)
Utils.setDivTL('eaf_FloatWin',top,left);
Utils.showDiv('eaf_FloatWin');
}
*/
//--------------------------------------------------------------------------
// toggle DIV element with image properties
//--------------------------------------------------------------------------
Utils.toggleDivImg = function(divName, imgName, expImgSrc, clpImgSrc) {
    var styleObj = document.getElementById(divName).style;
    var imgObj = document.getElementById(imgName);


    if (styleObj.display == "none") {
        styleObj.display = "block";
        imgObj.src = expImgSrc;
    }
    else {
        styleObj.display = "none";
        imgObj.src = clpImgSrc;
    }
}

Utils.togglePnlList = function(id, expImg, clpImg) {
    var divObj = document.getElementById(id + "ItemPnl");
    var imgObj = document.getElementById(id + "ImgBtn");
    var expTxtObj = document.getElementById(id + "Expanded");

    if (divObj.style.display == "none") {
        divObj.style.display = "block";
        imgObj.src = expImg;
        expTxtObj.value = "true";
    }
    else {
        divObj.style.display = "none";
        imgObj.src = clpImg;
        expTxtObj.value = "false";
    }
}

//--------------------------------------------------------------------------
// set object class
//--------------------------------------------------------------------------
Utils.setObjClass = function(obj, clsName) {
    obj.className = clsName;
}

//--------------------------------------------------------------------------
// set class by object ID
//--------------------------------------------------------------------------
Utils.setIDClass = function(id, clsName) {
    var obj = document.getElementById(id);
    obj.className = clsName;
}


Utils.toggleIDClass = function(id, clsName1, clsName2) {
    var obj = document.getElementById(id);
    if (obj.className == clsName1)
        obj.className = clsName2;
    else
        obj.className = clsName1;
}

//--------------------------------------------------------------------------
// show/hide select box (IE only)
//--------------------------------------------------------------------------
Utils.hideAllSelectBox = function(frm, bool) {
    var fm = eval('document.' + frm);
    for (i = 0; i < fm.elements.length; i++) {
        if (fm.elements[i].type == 'select') {
            if (bool)
                fm.elements[i].style.visibility = "hidden";
            else
                fm.elements[i].style.visibility = "visible";
        }
    }
}

//--------------------------------------------------------------------------
// show/hide select box (IE only)
//--------------------------------------------------------------------------
Utils.showHideObject = function(formName, ary, show) {
    if (ary != null) {
        for (i = 0; i < ary.length; i++) {
            var fld = eval('document.' + formName + '.' + ary[i]);
            if (show)
                fld.style.visibility = "visible";
            else
                fld.style.visibility = "hidden";
        }
    }
}

//--------------------------------------------------------------------------
// show/hide element by setting its style
//--------------------------------------------------------------------------
Utils.showHideElements = function(boo, ary) {
    if (document.all && ary != null) {
        var i;
        for (i = 0; i < ary.length; i++) {
            var fld = eval('document.forms[0].' + ary[i]);
            if (boo == true)
                fld.style.visibility = "visible";
            else
                fld.style.visibility = "hidden";
        }
    }
}






//**************************************************************************
// form validation functions
//**************************************************************************

//--------------------------------------------------------------------------
// auto fix phone format(XXX-XXX-XXXX)
// please use onKeyup action
//--------------------------------------------------------------------------
Utils.checkPhoneFormat = function(fldObj) {
    if (Utils.NUMBER_DASH == null)
        Utils.NUMBER_DASH = new Utils.makeArray('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-');

    var str = fldObj.value;

    if (str.length > 0) {
        /*
        if(str.substring(str.length-1)==' ') fldObj.value = str.substring(0,str.length-1);
        else if(str.substring(str.length-1)=='(') fldObj.value = str.substring(0,str.length-1);
        else if(str.substring(str.length-1)==')') fldObj.value = str.substring(0,str.length-1);
        */

        if (!Utils.foundChar(str.substring(str.length - 1), Utils.NUMBER_DASH))
            fldObj.value = str.substring(0, str.length - 1);
    }

    str = fldObj.value;
    if (str.length >= 4 && str.substring(3, 4) != "-") {
        fldObj.value = str.substring(0, 3) + "-" + str.substring(3);
    }

    str = fldObj.value;
    if (str.length >= 8 && str.substring(7, 8) != "-") {
        fldObj.value = str.substring(0, 7) + "-" + str.substring(7);
    }

    str = fldObj.value;
    if (str.length > 12)
        fldObj.value = str.substring(0, 12);
}

//--------------------------------------------------------------------------
// auto zip code format(XXXXX-XXXX)
// please use onKeyup action
//--------------------------------------------------------------------------
Utils.checkZIPFormat = function(fldObj) {
    if (Utils.NUMBER_DASH == null)
        Utils.NUMBER_DASH = new Utils.makeArray('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-');

    var str = fldObj.value;

    if (str.length > 0) {
        if (!Utils.foundChar(str.substring(str.length - 1), Utils.NUMBER_DASH))
            fldObj.value = str.substring(0, str.length - 1);
    }

    str = fldObj.value;
    if (str.length >= 6 && str.substring(5, 6) != "-") {
        fldObj.value = str.substring(0, 5) + "-" + str.substring(5);
    }

    str = fldObj.value;
    if (str.length > 10)
        fldObj.value = str.substring(0, 10);
}

//--------------------------------------------------------------------------
// auto check date format after onBlur
// please use onBlur action
// ex: 
//		onBlur="Utils.CheckDateFormat(this,'YMD','-',2004,2020)"
// dType:
//		YMD, DMY, MDY
//--------------------------------------------------------------------------
Utils.checkDateFormat = function(fldObj, dType, spliter, startY, endY) {
    var msg = "Date format is not valid. ";

    if (dType.toUpperCase() == 'MDY')
        msg = msg + "(MM" + spliter + "DD" + spliter + "YYYY)";
    else if (dType.toUpperCase() == 'DMY')
        msg = msg + "(DD" + spliter + "MM" + spliter + "YYYY)";
    else
        msg = msg + "(YYYY" + spliter + "MM" + spliter + "DD)";

    if (fldObj != null && fldObj.value != "") {
        var dteStr = fldObj.value;
        var sAry = dteStr.split(spliter);
        if (sAry.length != 3) {
            showAlert = true;
        }
        else {
            var y, m, d;
            var showAlert = false;

            if (dType.toUpperCase() == 'MDY') {
                y = sAry[2]; m = sAry[0]; d = sAry[1];
            }
            else if (dType.toUpperCase() == 'DMY') {
                y = sAry[2]; m = sAry[1]; d = sAry[2];
            }
            else {
                y = sAry[0]; m = sAry[1]; d = sAry[2];
            }

            var intY = parseInt(y, 10);
            var intM = parseInt(m, 10);
            var intD = parseInt(d, 10);

            if (intY < startY || intY > endY) {
                msg = msg + "\nPlease input Year betweem " + startY + " and " + endY + ".";
                showAlert = true;
            }


            if (intM < 1 || intM > 12) {
                msg = msg + "\nPlease input Month betweem 1 and 12.";
                showAlert = true;
            }

            if (intD < 1 || intD > 31) {
                msg = msg + "\nPlease input Date betweem 1 and 31.";
                showAlert = true;
            }
            else {
                if (intM == 4 || intM == 6 || intM == 9 || intM == 11) {
                    if (intD == 31) {
                        msg = msg + "\nThe Date cannot be 31 for the month.";
                        showAlert = true;
                    }
                }
                else if (intM == 2) {
                    if (((intY % 4 == 0) && (intY % 100 != 0)) || (intY % 400 == 0)) {
                        if (intD > 29) {
                            msg = msg + "\nThe Date cannot be greater than 29 for the month and year.";
                            showAlert = true;
                        }
                    }
                    else
                        if (intD > 28) {
                        msg = msg + "\nThe Date cannot be greater than 28 for the month and year.";
                        showAlert = true;
                    }
                }
            }
        }

        if (showAlert) {
            alert(msg);
            fldObj.focus();
            fldObj.select();
            //fldObj.value = "";
        }

    }
}

//**************************************************************************
// browser and windows
//**************************************************************************

//--------------------------------------------------------------------------
// go to url
//--------------------------------------------------------------------------
Utils.gotoURL = function(target, url) {
    eval(target + ".location='" + url + "'");
}

//--------------------------------------------------------------------------
// open new browser window
//--------------------------------------------------------------------------
Utils.openNewWindow = function(theURL, width, height, winName, tbar, stsbar, resize) {
    var winTitle = '';
    var w = 600;
    var h = 400;
    var tb = 'no';
    var rz = 'yes';
    var sts = 'yes';

    if (typeof winName != 'undefined') winTitle = winName;
    if (typeof width != 'undefined') w = width;
    if (typeof height != 'undefined') h = height;
    if (typeof tbar != 'undefined') tb = tbar;
    if (typeof resize != 'undefined') rz = resize;
    if (typeof stsbar != 'undefined') sts = stsbar;

    var str = 'menubar=no,location=no,toolbar=' + tb +
			  ',scrollbars=yes,status=' + sts +
			  ',resizable=' + rz +
			  ',width=' + w +
			  ',height=' + h;
    return window.open(theURL, winTitle, str);
}

//--------------------------------------------------------------------------
// open new dialog browser window (IE only)
//--------------------------------------------------------------------------
Utils.showModalDialog = function(theURL, width, height, resize, stsbar) {
    var w = 600;
    var h = 400;
    var rz = 'yes';
    var sts = 'no';

    if (typeof width != 'undefined') w = width;
    if (typeof height != 'undefined') h = height;
    if (typeof resize != 'undefined') rz = resize;
    if (typeof stsbar != 'undefined') sts = stsbar;

    var str = "dialogHeight:" + h +
			  "px; dialogWidth:" + w +
			  "px; edge:Raised; center:Yes; help:No; resizable:" + rz +
			  "; status:" + sts;
    return window.showModalDialog(theURL, "", str);
}

Utils.DialogWinAction = function(theURL, tbObjID, width, height, resize, stsbar) {
    var rtn = Utils.showModalDialog(theURL, width, height, resize, stsbar);

    if (typeof rtn != 'undefined') {
        var str = rtn.split(',');
        var tbObj = document.getElementById(tbObjID);
        var tbHiddenObj = document.getElementById(tbObjID + 'Hidden');

        tbObj.value = str[0];
        tbHiddenObj.value = str[1];
    }
}

Utils.DialogWinReturn = function(rtnTxt, rtnVal) {
    window.returnValue = rtnTxt + ',' + rtnVal;
    window.close();
}


//--------------------------------------------------------------------------
// javascript dialog window
// 4 functions to invoke
//--------------------------------------------------------------------------
Utils.openDialogWin = function(theURL, width, height, winName, tbar, stsbar, resize) {
    if (Utils.DIALOG_WIN == null || Utils.DIALOG_WIN.closed)
        Utils.DIALOG_WIN = Utils.openNewWindow(theURL, width, height, winName, tbar, stsbar, resize);
    else
        Utils.DIALOG_WIN.focus();

    window.onfocus = Utils.onWinFocus;
    window.onunload = Utils.onWinBlur;

    return Utils.DIALOG_WIN;
};

Utils.closeDialogWin = function(reloadP) {
    if (reloadP)
        self.opener.location.reload();
    self.close();
};

Utils.onWinFocus = function() {
    if (Utils.DIALOG_WIN != null && !Utils.DIALOG_WIN.closed)
        Utils.DIALOG_WIN.focus();
};

Utils.onWinBlur = function() {
    if (Utils.DIALOG_WIN != null && !Utils.DIALOG_WIN.closed)
        Utils.DIALOG_WIN.close();
};

//--------------------------------------------------------------------------
// timer function:
//	you have to implement Utils.timeupAction() function in your page
//
//    Utils.timeupAction = function()
//    {       
//    }
//--------------------------------------------------------------------------
Utils.setCounter = function(sec) {
    if (sec > 0) {
        sec--;
        Utils.TIMER_ID = setTimeout("Utils.setCounter(" + sec + ")", 1000);
    }
    else {
        if (Utils.TIMER_ID != null) clearTimeout(Utils.TIMER_ID);
        Utils.timeupAction();
    }
}

Utils.txtFldCounter = function(fldName) {
    var obj = eval(fldName);
    var sec = parseInt(obj.value);
    if (sec > 0) {
        obj.value = sec - 1;
        Utils.TIMER_ID = setTimeout("Utils.txtFldCounter('" + fldName + "')", 1000);
    }
    else {
        if (Utils.TIMER_ID != null) clearTimeout(Utils.TIMER_ID);
        Utils.timeupAction();
    }
}

//--------------------------------------------------------------------------
// Load XMLHttpREquest into a DIV
//--------------------------------------------------------------------------
Utils.getXMLHtmlReq = function() {
    var req = null;
    if (window.XMLHttpRequest) {
        try { req = new XMLHttpRequest(); }
        catch (e) { alert(e); }
    }
    else if (window.ActiveXObject) {
        try {
            req = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try { req = new ActiveXObject("Microsoft.XMLHTTP"); }
            catch (e) { alert(e); }
        }
    }
    return req;
}


Utils.runCallback = function(url, callbackHandler, parm, context) {
    var req = Utils.getXMLHtmlReq();

    var newUrl = url;
    if (parm != null && parm != "") newUrl = url + "&p=" + parm;

    if (req != null) {
        try {
            req.onreadystatechange = function() { Utils._reqHandler(req, callbackHandler, context); };
            req.open("GET", newUrl, true);
            req.send(null);
        }
        catch (e) {
            alert(e);
        }
    }
    return req;
};

Utils.loadAjaxDiv = function(divName, url, parm) {
    return Utils.runCallback(url, Utils.setAjaxDiv, null, divName);
};

Utils.setAjaxDiv = function(txtResult, domObj, context) {
    document.getElementById(context).innerHTML = txtResult;
};

Utils._reqHandler = function(req, callbackHandler, context) {
    // only if req shows "complete"
    if (req.readyState == 4) {
        // only if "OK"
        if (req.status == 200) {
            callbackHandler(req.responseText, req.responseXML, context);
            req = null;
        }
        else {
            alert("There was a problem retrieving the XML data:\n\n" + req.statusText);
        }
    }
};


//**************************************************************************
// Gradual-Highlight obj
//**************************************************************************
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

Utils.loopFade = function(object, destOpFrom, destOpTo, rate, delta) {
    if (!document.all) return; // for IE only

    if (object != "[object]") {  //do this so I can take a string too
        setTimeout("Utils.loopFade(" + object + "," + destOpFrom + "," + destOpTo + "," + rate + "," + delta + ")", 0);
        return;
    }

    clearTimeout(WCJS_GradualTimers[object.sourceIndex]);

    if (object.filters.alpha.opacity == destOpFrom)
        diff = destOpTo - object.filters.alpha.opacity;
    else if (object.filters.alpha.opacity == destOpTo)
        diff = destOpFrom - object.filters.alpha.opacity;

    direction = 1;
    if (diff < 0) {
        direction = -1;
    }

    delta = Math.min(direction * diff, delta);
    object.filters.alpha.opacity += direction * delta;

    WCJS_GradualFadeObjects[object.sourceIndex] = object;
    WCJS_GradualTimers[object.sourceIndex] = setTimeout("Utils.loopFade(WCJS_GradualFadeObjects[" + object.sourceIndex + "]," + destOpFrom + "," + destOpTo + "," + rate + "," + delta + ")", rate);
}


Utils.objFade = function(object, destOp, rate, delta) {
    if (!document.all) return; // for IE only

    if (object != "[object]") {  //do this so I can take a string too
        setTimeout("Utils.objFade(" + object + "," + destOp + "," + rate + "," + delta + ")", 0);
        return;
    }

    clearTimeout(WCJS_GradualTimers[object.sourceIndex]);

    diff = destOp - object.filters.alpha.opacity;

    direction = 1;
    if (object.filters.alpha.opacity > destOp) direction = -1;

    delta = Math.min(direction * diff, delta);
    object.filters.alpha.opacity += direction * delta;

    if (object.filters.alpha.opacity != destOp) {
        WCJS_GradualFadeObjects[object.sourceIndex] = object;
        WCJS_GradualTimers[object.sourceIndex] = setTimeout("Utils.objFade(WCJS_GradualFadeObjects[" + object.sourceIndex + "]," + destOp + "," + rate + "," + delta + ")", rate);
    }
}


//**************************************************************************
// behind the scene actions
//**************************************************************************

//--------------------------------------------------------------------------
// Find obj X in the page: Image, and A HREF......
//--------------------------------------------------------------------------
Utils.findPosX = function(obj) {
    var curleft = 0;
    if (document.getElementById || document.all) {
        while (obj.offsetParent) {
            //alert("obj.offsetParent:"+obj.offsetParent+" toleft:"+obj.offsetLeft);
            curleft += obj.offsetLeft
            obj = obj.offsetParent;
        }
    }
    else if (document.layers) {
        curleft += obj.x;
    }
    return curleft;
}

//--------------------------------------------------------------------------
// Find obj Y in the page: Image, and A HREF......
//--------------------------------------------------------------------------
Utils.findPosY = function(obj) {
    var curtop = 0;
    if (document.getElementById || document.all) {
        while (obj.offsetParent) {
            curtop += obj.offsetTop
            obj = obj.offsetParent;
        }
    }
    else if (document.layers) {
        curtop += obj.y;
    }
    return curtop;
}

//--------------------------------------------------------------------------
// Find obj by name
// from MM v4.01
//--------------------------------------------------------------------------
Utils.findObj = function(name, doc) {
    var p, i;
    var rtnObj;

    if (!doc) doc = document;

    if ((p = name.indexOf("?")) > 0 && parent.frames.length) {
        doc = parent.frames[name.substring(p + 1)].document;
        name = name.substring(0, p);
    }
    if (!(rtnObj = doc[name]) && doc.all)
        rtnObj = doc.all[name];

    for (i = 0; !rtnObj && i < doc.forms.length; i++)
        rtnObj = doc.forms[i][name];

    for (i = 0; !rtnObj && doc.layers && i < doc.layers.length; i++)
        rtnObj = findObj(name, doc.layers[i].document);

    if (!rtnObj && doc.getElementById)
        rtnObj = doc.getElementById(name);

    return rtnObj;
}

//--------------------------------------------------------------------------
// preload image
// by MM 3.0
//--------------------------------------------------------------------------
Utils.preloadImages = function() {
    var doc = document;
    var args = Utils.preloadImages.arguments;

    if (doc.images) {
        if (Utils.IMG_ARY == null) Utils.IMG_ARY = new Array();

        var i, len = Utils.IMG_ARY.length;

        for (i = 0; i < args.length; i++) {
            if (args[i].indexOf("#") != 0) {
                Utils.IMG_ARY[len] = new Image;
                Utils.IMG_ARY[len].src = args[i];
                len++;
            }
        }
    }
}

//--------------------------------------------------------------------------
// swap image restore
// by MM 3.0
//--------------------------------------------------------------------------
Utils.swapImgRestore = function() {
    var i, x, a = Utils.IMG_SWAP_SRC;

    for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++)
        x.src = x.oSrc;

    Utils.IMG_SWAP_SRC = null;
}

//--------------------------------------------------------------------------
// swap image
// by MM 3.0
//--------------------------------------------------------------------------
Utils.swapImage = function(imgName, imgSrc) {
    var i, j = 0, x;
    if (Utils.IMG_SWAP_SRC == null) Utils.IMG_SWAP_SRC = new Array;
    if ((x = Utils.findObj(imgName)) != null) {
        Utils.IMG_SWAP_SRC[Utils.IMG_SWAP_SRC.length] = x;
        if (!x.oSrc) x.oSrc = x.src;
        x.src = imgSrc;
    }
}

//--------------------------------------------------------------------------
// fix table header, IE8 wont' work
//--------------------------------------------------
Utils.GVHeadRow = null;
Utils.setFixHead = function(gvID) {
    var jqoName = "#" + gvID;
    var th = $(jqoName + " th:first");
    var gvWith = $(jqoName).width();
    if (th.length > 0) {
        var row = th.parent("tr");
        if (($(jqoName).position().top - $(document).scrollTop()) < 0) {
            if (Utils.GVHeadRow == null) {

                Utils.GVHeadRow = row.clone();
                //Utils.GVHeadRow.html("<table><tr>" + row.clone().html() + "</tr></table>");
                Utils.GVHeadRow.appendTo(jqoName);
                //Utils.GVHeadRow.wrap("<tr></tr>");
                //.wrap("<table></table>").appendTo(jqoName);
                //alert(Utils.GVHeadRow.html());
            }
            Utils.GVHeadRow.css({ "position": "absolute", "top": $(document).scrollTop() + "px", "width": gvWith + "px" });

        }
        else {
            if (Utils.GVHeadRow != null) {
                Utils.GVHeadRow.remove();
                Utils.GVHeadRow = null;
            }
        }
    }
}

Utils.resetFixHead = function(gvID) {
    Utils.GVHeadRow = null;
    Utils.setFixHead(gvID);
}


Utils.FixGVHeader = function(gvID) {
    $(function() {
        $(window).scroll(function() {
            Utils.setFixHead(gvID);
        });
    });

    //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
    //        Utils.resetFixHead(gvID);
    //    });
}





//////--------------------------------------------------------------------------
////// Build array function
//////--------------------------------------------------------------------------
////Utils.getDataTable = function(result,dtName)
////{
////    var dt = null;
////    var rowSplt = "_||_";
////	var fldSplt = "_|_";
////    
////    var rows = result.split(rowSplt);
////    if(rows.length>0) dt = new DataTable(dtName);
////    
////    for(i=0; i<rows.length; i++)
////	{
////		var dr = new DataRow();
////		var flds = rows[i].split(fldSplt);
////		for(k=0; k<flds.length; k++)
////	    {
////	        if(typeof flds[k]!="undefined")
////	            dr.addField(flds[k]);
////	        else
////	            dr.addField(null);
////	    }
////	    dt.addRow(dr);
////    }
////    
////    return dt;
////};




//////###########################################################################################




////function DataRow()
////{
////	this.FldNames = new Array();
////	this.Fields = new Array();
////	return this;
////}

////DataRow.prototype.setFldNameAry = function(names)
////{
////    this.FldNames = names;
////}

////DataRow.prototype.setFieldAry = function(flds)
////{
////    this.Fields = flds;
////}

////DataRow.prototype.addFldName = function(val)
////{
////    this.FldNames[this.FldNames.length] = val;
////}

////DataRow.prototype.addField = function(val)
////{
////    this.Fields[this.Fields.length] = val;
////}

////DataRow.prototype.getValByIdx = function(idx)
////{
////    return this.Fields[idx];
////}

////DataRow.prototype.getValByFldName = function(name)
////{
////    for(i=0; i<this.FldNames.length; i++)
////	{
////	    if(this.FldNames==name)
////	        return this.Fields[i];
////	}
////    return null;
////}

//////##############################################################################################

////function DataTable(tabName)
////{
////	this.TableName = tabName;
////	this.FldNames = new Array();
////	this.Rows = new Array();
////	return this;
////}

////DataTable.prototype.setFldNameAry = function(names)
////{
////    this.FldNames = names;
////}

////DataTable.prototype.addFldName = function(val)
////{
////    this.FldNames[this.FldNames.length] = val;
////}

////DataTable.prototype.addRow = function(row)
////{
////    if(this.FldNames.length>0) row.setFldNameAry = this.FldNames;
////    this.Rows[this.Rows.length] = row;
////}



//////##############################################################################################

////function DataSet()
////{
////	this.Tables = new Array();
////	return this;
////}

////DataSet.prototype.addTable = function(dt)
////{
////    this.Tables[this.Tables.length] = dt;
////}

////DataSet.prototype.getTableByName = function(name)
////{
////    for(i=0; i<this.Tables.length; i++)
////	{
////	    if(this.Tables[i].TableName==name)
////	        return this.Tables[i];
////	}
////    return null;
////}