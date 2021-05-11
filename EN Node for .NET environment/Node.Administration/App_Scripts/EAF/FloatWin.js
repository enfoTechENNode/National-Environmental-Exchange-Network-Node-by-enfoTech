
//***************************************************************
//*																															*
//*  The FloatDiv.js only support IE5 and above, and NS 7.		
//*	 Use with your own risk if run in other broswers...					
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

function FloatWin(insName)
{
	this.InsName = insName;
	this.IsRefreshOnClosed = false;
	this.IsDivScroll = false;
	
	this.Title = "";
	this.PageSrc = "";
	this.OX = 0;
	this.OY = 0;
	this.W = 400;
	this.H = 400;
	
	this.IsDraging = false;
	this.IFrameDivObj = null;
	return this;
}

//*********************************************
//	Class level constants
//*********************************************


//*********************************************
//	Public methods
//*********************************************

//--------------------------
//	Write the DIV tag BEGIN
//--------------------------

FloatWin.prototype.writeFloatWinDivHead = function()
{
    document.write('<div id="'+this.InsName+'Glass" class="eaf_FloatWinGlass"></div>');
    document.write('<div id="'+this.InsName+'Div" class="eaf_FloatWinDiv">');
    
	document.write('<table id="'+this.InsName+'Tab" class="eaf_FloatWinTab" cellspacing="0">');
	document.write('<tr>');
	document.write('<td class="Head"><div>&nbsp;</div></td>');
    document.write('<td class="Ttl"><div id="'+this.InsName+'Ttl">Win Title</div></td>');
	document.write('<td class="Btn"><div><a href="javascript:void(0);" onClick="'+this.InsName+'.close();" title="Close panel">x</a></div></td>');
	document.write('<td class="Tail"><div>&nbsp;</div></td>');
	document.write('</tr>');
	document.write('</table>');
	
	document.write('<div id="'+this.InsName+'Cnt" class="eaf_FloatWinCnt" style="height:'+this.H+'px;');
	if(this.IsDivScroll) document.write(' overflow:auto; ');
	document.write('">');
}

FloatWin.prototype.writeFloatWinDivTail = function()
{
	document.write('</div>');
	
    document.write('</div>');
    
    // iframe for block selectbox
	document.write('<iframe id="'+this.InsName+'GlassBGIFrame" ');
	document.write(' src="javascript:false;" scrolling="no" frameborder="0"');
	document.write(' style="position:absolute; top:0px; left:0px; display:none;">');
	document.write('</iframe>');		
}

FloatWin.prototype.initIFrame = function()
{
    this.writeFloatWinDivHead();   
    document.write('<iframe id="'+this.InsName+'IFrame" class="eaf_FloatWinIFrame" src="" frameborder="0"></iframe>');
    this.writeFloatWinDivTail();   
    
    this.IFrameDivObj = document.getElementById(''+this.InsName+'IFrame');
}

FloatWin.prototype.initObjs = function()
{
    this.DivGlassObj = document.getElementById(''+this.InsName+'Glass');
    this.DivObj = document.getElementById(this.InsName+'Div');
    this.DivCntObj = document.getElementById(this.InsName+'Cnt');
    //this.IFrameDivObj = document.getElementById(''+this.InsName+'IFrame');
    this.IFrameBG = document.getElementById(''+this.InsName+'GlassBGIFrame');
}

FloatWin.prototype.adjustIframe = function()
{
	this.IFrameBG.style.width = this.DivObj.offsetWidth;
	this.IFrameBG.style.height = this.DivObj.offsetHeight;
	this.IFrameBG.style.top = this.DivObj.style.top;
	this.IFrameBG.style.left = this.DivObj.style.left;
	this.IFrameBG.style.zIndex = this.DivObj.style.zIndex+1;
}

FloatWin.prototype.showPanel = function(lnkObj)
{
    this.show(lnkObj,this.Title,this.PageSrc,this.OffX,this.OffY,this.W,this.H);
}

FloatWin.prototype.show = function(lnkObj,ttl,src,offX,offY,w,h)
{
	this.Title = ttl;
	this.PageSrc = src;
	this.OffX = offX;
	this.OffY = offY;
	this.W = w;
	this.H = h;
	
	Utils.setDivText(this.InsName+'Ttl',this.Title);
	if(this.IFrameDivObj!=null) this.IFrameDivObj.src = this.PageSrc;
	
	var left = eval(Utils.findPosX(lnkObj)+this.OffX);
	var top = eval(Utils.findPosY(lnkObj)+this.OffY);
	
	this.DivObj.style.width = this.W + 'px';
	this.DivObj.style.height = this.H + 'px';
	this.DivObj.style.left = left + "px";
	this.DivObj.style.top = top + "px";

	this.DivCntObj.style.width = eval(this.W-4) + 'px';
	this.DivCntObj.style.height = eval(this.H-4) + 'px';
	this.DivCntObj.style.left = left + "px";
	this.DivCntObj.style.top = top + "px";
	
	this.DivGlassObj.style.width = this.W + 'px';
	this.DivGlassObj.style.height = this.H + 'px';
	this.DivGlassObj.style.left = left + "px";
	this.DivGlassObj.style.top = top + "px";	
	
	if(this.IFrameDivObj!=null) this.IFrameDivObj.style.width = eval(w-6) + 'px';
	
	this.adjustIframe();
	
	this.DivGlassObj.style.visibility='hidden';
	this.DivObj.style.visibility='visible';
	this.IFrameBG.style.display = "block";	
	//document.frames[this.InsName+'IFrame'].location.reload(true);
}

FloatWin.prototype.hide = function()
{
    //this.IFrameDivObj.src = '';
    this.DivGlassObj.style.visibility='hidden';
    this.DivObj.style.visibility='hidden';
    this.IFrameBG.style.display = "none";
}

FloatWin.prototype.close = function()
{
    this.hide();
    if(this.IsRefreshOnClosed) window.location.reload();
}


FloatWin.prototype.isInHeadBarDiv = function(x,y)
{
	var HBHeight = 30;
	var divTop,divLeft;
	
	divTop = parseInt(this.DivGlassObj.offsetTop);
	divLeft = parseInt(this.DivGlassObj.offsetLeft);
	
	if ( x>divLeft && x<(divLeft+this.DivGlassObj.offsetWidth) &&  
		 y>divTop && y<(divTop+HBHeight) )
	{
		return true;
	}
	
	return false;	
}

//----------------------------------------------
//  Mouse Down Event
//----------------------------------------------
FloatWin.prototype.MouseUpEvent = function()
{
    this.IsDraging = false;
}

FloatWin.prototype.MouseDownEvent = function()
{
	if(this.DivObj.style.visibility=='visible')
	{
		var x = eval(event.clientX+document.documentElement.scrollLeft);
		var y = eval(event.clientY+document.documentElement.scrollTop);			

		if(this.isInHeadBarDiv(x,y))
		{
            this.IsDraging = true;

            var divTop = parseInt(this.DivObj.offsetTop);
	        var divLeft = parseInt(this.DivObj.offsetLeft);
	    	        
            this.diffX = eval(divLeft-x);
            this.diffY = eval(divTop-y);
		}
	}
}

FloatWin.prototype.MouseMoveEvent = function()
{
	if(this.IsDraging)
	{
		var x = eval(event.clientX+document.documentElement.scrollLeft);
		var y = eval(event.clientY+document.documentElement.scrollTop);
	    
		this.DivObj.style.left = eval(x+this.diffX) + 'px';
        this.DivObj.style.top = eval(y+this.diffY) + 'px';
        
        this.DivGlassObj.style.left = this.DivObj.style.left;
        this.DivGlassObj.style.top = this.DivObj.style.top;
        
        this.adjustIframe();
	}
}

//---------------------------------------------------------------------------------------------
//** invoke in the page

////var eaf_FloatWin = new FloatWin('eaf_FloatWin');
////eaf_FloatWin.init();

////// Mouse Event for DivCal
//////--------------------------
////function FloatWinMouseDown() { eaf_FloatWin.MouseDownEvent(); };
////function FloatWinMouseUp() { eaf_FloatWin.MouseUpEvent(); };
////function FloatWinMouseMove() { eaf_FloatWin.MouseMoveEvent(); };


////if(document.attachEvent)
////{
////	document.attachEvent('onmousedown',FloatWinMouseDown);
////	document.attachEvent('onmouseup',FloatWinMouseUp);
////	document.attachEvent('onmousemove',FloatWinMouseMove);
////}
////else if(document.addEventListener)
////{
////	document.addEventListener('mousedown',FloatWinMouseDown,false);
////	document.addEventListener('mouseup',FloatWinMouseUp,false);
////	document.addEventListener('mousemove',FloatWinMouseMove,false);
////}
