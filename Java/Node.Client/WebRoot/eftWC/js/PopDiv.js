
//=========================================
//	This is for a pop div of a page
//=========================================


//*************************
//	Global members
//*************************

PopDiv.FADE_TIMER = null;

//*************************
//	Constructor
//*************************

function PopDiv(instancename)
{
	// properties
	//--------------------
	this.DivCSS = '';
	this.OnTop = false;
	this.Shadow = true;
	this.FixedPop = true;
	this.OnTopAlign = 'bottom-right';
	this.Width = '180';
	this.OffsetX = 0;
	this.offsetY = 0;
	this.MarginOffset = 10;
	this.Opacity = 90;
	this.Zindex = 100;
	
	this.FadeStep = 5;
	this.FadeRate = 10;
	
	// internal members
	//-------------------------------------
	this.InstanceName = instancename;
	this.ShowDiv = false;
	this.RunOnceFlag = false;
	this.DivObj = null;
	
	this.DOC_IE = (document.all)? true:false;
	this.DOC_DOM = (document.getElementById) ? true : false; // DOM browser, also IE
	
	return this;
}

//*********************************************
//	Methods
//*********************************************
	
//	Write the DIV tag BEGIN
//----------------------------------------------
PopDiv.prototype.writeDivHeader = function()
{
	document.write('<div id="'+this.InstanceName+'" class="'+this.DivCSS+'" ');
	document.write('style="position:absolute; width:'+this.Width+'; z-index:'+this.Zindex+'; padding:5px; visibility:hidden; ');
	document.write('filter:');
	if(this.Shadow) document.write('progid:DXImageTransform.Microsoft.Shadow(color=#666666,Direction=120,Strength=2,positive=true) ');
	document.write('progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0) ');
	document.write('">');
}

//	Write the DIV tag END
//----------------------------------------------
PopDiv.prototype.writeDivFooter = function()
{
	document.write('</div>');
}

// Initial functions
//------------------------------------------
PopDiv.prototype.init = function()
{
	if(this.DOC_IE) this.DivObj = eval('document.all.' + this.InstanceName);
	if(this.DOC_DOM) this.DivObj = document.getElementById(this.InstanceName);
	
	if(this.OnTop==true) this.alwaysOnTop();
}

// Initial functions
//------------------------------------------
PopDiv.prototype.ctor = function()
{
	this.writeDivHeader();
	this.writeDivFooter();
	this.init();
}

//	Detecting if inside DIV tag of a event X,Y
//-------------------------------------------------
PopDiv.prototype.isInDiv = function(x,y)
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

	if ( x>divLeft && x<(divLeft+this.DivObj.offsetWidth) && 
		 y>divTop && y<(divTop+this.DivObj.offsetHeight))
	{
		return true;
	}
	return false;
}

//  Hide DIV
//-----------------------------------------
PopDiv.prototype.divHidden = function()
{
	if(this.DOC_IE)
		this.fadeOutEffect(0);
	else
		this.DivObj.style.visibility='hidden';
		
	this.RunOnceFlag = false;
	this.ShowDiv = false;
}

//  Show DIV
//-----------------------------------------
PopDiv.prototype.divVisible = function(cnt,w,offsetX,offsetY)
{
	if(this.OnTop==false)
	{
		if(typeof cnt != 'undefined') this.DivObj.innerHTML = cnt;
		if(typeof offsetX != 'undefined') this.OffsetX = offsetX;
		if(typeof offsetY != 'undefined') this.OffsetY = offsetY;
		if(typeof w != 'undefined') 
			this.setWidth(w);
		else
			this.setWidth(this.Width);
		
		this.ShowDiv = true;
	}
	else
	{
		if(this.DOC_IE)
			this.fadeInEffect(this.Opacity);
		else
			this.DivObj.style.visibility = 'visible';
	}
}

// make div always on top
//-----------------------------------------
PopDiv.prototype.alwaysOnTop = function()
{
	if (this.DOC_IE)
	{
		fixWinW = document.body.clientWidth;
		fixWinH = document.body.clientHeight;
		
		relWinW = document.body.scrollLeft;
		relWinH = document.body.scrollTop;
	}	
	else if (this.DOC_DOM)
	{
		fixWinW = window.innerWidth;
		fixWinH = window.innerHeight;
		
		relWinW = window.pageXOffset;
		relWinH = window.pageYOffset;
	} 
	
	if(this.OnTopAlign=="center")
	{
		this.DivObj.style.left = relWinW+fixWinW/2 - this.DivObj.offsetWidth/2;
		this.DivObj.style.top = relWinH+fixWinH/2 - this.DivObj.offsetHeight/2;
	}
	else if(this.OnTopAlign=="bottom-center")
	{
		this.DivObj.style.left = relWinW+fixWinW/2 - this.DivObj.offsetWidth/2;
		this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.MarginOffset;
	}
	else if(this.OnTopAlign=="bottom-right")
	{
		this.DivObj.style.left = (relWinW+fixWinW) - this.DivObj.offsetWidth - this.MarginOffset;
		this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.MarginOffset;
	}
	else if(this.OnTopAlign=="bottom-left")
	{
		this.DivObj.style.left = this.MarginOffset;
		this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.MarginOffset;
	}
	else if(this.OnTopAlign=="top-center")
	{
		this.DivObj.style.left = relWinW+fixWinW/2 - this.DivObj.offsetWidth/2;
		this.DivObj.style.top = (relWinH) + this.MarginOffset;
	}
	else if(this.OnTopAlign=="top-left")
	{
		this.DivObj.style.left = this.MarginOffset;
		this.DivObj.style.top = (relWinH) + this.MarginOffset;
	}
	else if(this.OnTopAlign=="top-right")
	{
		this.DivObj.style.left = (relWinW+fixWinW) - this.DivObj.offsetWidth - this.MarginOffset;
		this.DivObj.style.top = (relWinH) + this.MarginOffset;
	}
	
	setTimeout(this.InstanceName+".alwaysOnTop()",500);
}

//  Mouse Down Event
//----------------------------------------------
PopDiv.prototype.mouseDownEvent = function(e)
{
	if(!e) e = window.event;
	
	if(this.OnTop==false)
	{
		if(this.DivObj.style.visibility=='visible')
		{
			var x,y;
			
			if (this.DOC_IE)
			{
				x = eval(e.clientX+document.body.scrollLeft);
				y = eval(e.clientY+document.body.scrollTop);
			}
			else if(this.DOC_DOM)
			{
				x = e.pageX;
				y = e.pageY;
			}

			if(!this.isInDiv(x,y))
			{
				this.divHidden();
			}
		}
	}
}

//  Mouse Down Event
//----------------------------------------------
PopDiv.prototype.mouseMoveEvent = function(e)
{
	if(!e) e = window.event;
	
	if(this.OnTop==false)
	{
		if(this.ShowDiv)
		{
			if(this.RunOnceFlag==false)
			{
				var x,y;
				
				if (this.DOC_IE)
				{
					x = eval(e.clientX+document.body.scrollLeft);
					y = eval(e.clientY+document.body.scrollTop);
				}
				else if(this.DOC_DOM)
				{
					x = e.pageX;
					y = e.pageY;
				}
				this.showDivXY(x,y);
				
				if(this.Fixed==true) 
					this.RunOnceFlag = true;
			}
		}
	}
}

//  Show DIV
//-----------------------------------------
PopDiv.prototype.showDivXY = function(x,y)
{
	var testX = eval(document.body.clientWidth-(x+this.OffsetX)); //IE

	if(testX<this.Width)
	{
		this.DivObj.style.left = eval(x-(this.Width-testX)) + "px";
		this.DivObj.style.top = eval(y+this.OffsetY) + "px";
	}
	else
	{
		this.DivObj.style.left = eval(x+this.OffsetX) + "px";
		this.DivObj.style.top = eval(y+this.OffsetY) + "px";
	}
	
	if(this.DOC_IE)
		this.fadeInEffect(this.Opacity);
	else
		this.DivObj.style.visibility = 'visible';
}

PopDiv.prototype.fadeInEffect = function(destOp)
{
	if(PopDiv.FADE_TIMER!=null) clearTimeout(PopDiv.FADE_TIMER);
	this.DivObj.style.visibility = 'visible';
	
	var o = this.DivObj.filters.item('DXImageTransform.Microsoft.Alpha');
	
	if(o.opacity<destOp)
	{
		o.opacity += this.FadeStep;
		PopDiv.FADE_TIMER = setTimeout(this.InstanceName+".fadeInEffect("+destOp+")",this.FadeRate);
	}
	else
	{
		o.opacity = destOp;
		if(PopDiv.FADE_TIMER!=null) clearTimeout(PopDiv.FADE_TIMER);
	}
}

PopDiv.prototype.fadeOutEffect = function(destOp)
{
	if(PopDiv.FADE_TIMER!=null) clearTimeout(PopDiv.FADE_TIMER);
	
	var o = this.DivObj.filters.item('DXImageTransform.Microsoft.Alpha');
	
	if(o.opacity>destOp)
	{
		o.opacity -= this.FadeStep;
		PopDiv.FADE_TIMER = setTimeout(this.InstanceName+".fadeOutEffect("+destOp+")",this.FadeRate);
	}
	else
	{
		o.opacity = destOp;
		if(PopDiv.FADE_TIMER!=null) clearTimeout(PopDiv.FADE_TIMER);
		this.DivObj.style.visibility = 'hidden';
	}
}

PopDiv.prototype.setOpacity = function(op) 
{ 
	this.DivObj.filters.item('DXImageTransform.Microsoft.Alpha').opacity = op; 
};

PopDiv.prototype.setWidth = function(w) 
{ 
	this.DivObj.style.width = w;
};

