
//***************************************************************
//*																															*
//*  The FloatDiv.js only support IE5 and above, and NS 7.		*
//*	 Use with your own risk if run in other broswers...					*
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function FloatDiv(instancename, divname, zidx)
	{
		//** public members
		this.OnTop = false;
		this.Shadow = true;
		this.OnTopAlign = "bottom-right";
		this.AlignOffset = 10;
		this.Opacity = 50;
		
		//** internal members
		this.InstanceName = instancename;
		this.DivName = divname;
		this.Zindex = zidx;
		
		this.DOC_IE = (document.all)? true:false;									// IE	
		this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE
		
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
	FloatDiv.prototype.writeDivHeader = function()
	{
		document.write('<div id="' + this.DivName + '" ');
		document.write(' style="position:absolute; z-index:'+this.Zindex+';  visibility:hidden; ');
		document.write(' filter:');
		if(this.Shadow) document.write(' progid:DXImageTransform.Microsoft.Shadow(color=#666666,Direction=120,Strength=5,positive=true)');
		document.write(' progid:DXImageTransform.Microsoft.Alpha( style=0,opacity='+this.Opacity+');');
		document.write('">');
	}

	//--------------------------
	//	Write the DIV tag END
	//--------------------------
	FloatDiv.prototype.writeDivFooter = function()
	{
		document.write('</div>');
	}


	//-------------------
	// Initial functions
	//--------------------
	FloatDiv.prototype.init = function()
	{
		if(this.DOC_IE) this.DivObj = eval('document.all.' + this.DivName);
		if(this.DOC_DOM) this.DivObj = document.getElementById(this.DivName);
		
		if(this.OnTop==true) this.alwaysOnTop();
	}
	

	//----------------------------------
	//	Detecting if inside DIV tag
	//-----------------------------------
	FloatDiv.prototype.isInDivLayer = function(x,y)
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

		if ( x>divLeft && x<(divLeft+this.DivObj.offsetWidth) &&  y>divTop && y<(divTop+this.DivObj.offsetHeight) )
		{
			return true;
		}
		return false;
	}

	//-----------------------------------------
	//  Hide DIV
	//-----------------------------------------
	FloatDiv.prototype.hideDiv = function()
	{
			this.DivObj.style.visibility='hidden';
	}

	//-----------------------------------------
	//  Show DIV
	//-----------------------------------------
	FloatDiv.prototype.showDiv = function()
	{
			this.DivObj.style.visibility='visible';
	}

	//-----------------------------------------
	// Major function for javascript to call
	//-----------------------------------------
	FloatDiv.prototype.switchDiv = function(imgName,offsetX,offsetY)
	{
		if(this.DivObj.style.visibility=='visible')
		{
			this.DivObj.style.visibility='hidden';
		}
		else
		{
			if(this.OnTop==false)
			{
				var imgObj = eval('document.' + imgName);
				this.DivObj.style.left = eval(findPosX(imgObj)+offsetX) + "px";
				this.DivObj.style.top = eval(findPosY(imgObj)+imgObj.height+offsetY) + "px";
			}
			this.DivObj.style.visibility = 'visible';
		}

	}


	FloatDiv.prototype.switchFloatDiv = function()
	{
		if(this.DivObj.style.visibility=='visible')
		{
			this.DivObj.style.visibility='hidden';
		}
		else
		{
			this.DivObj.style.visibility = 'visible';
		}

	}

	//-----------------------------------------
	// Major function for javascript to call
	//-----------------------------------------

	FloatDiv.prototype.alwaysOnTop = function()
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
				this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="bottom-right")
			{
				this.DivObj.style.left = (relWinW+fixWinW) - this.DivObj.offsetWidth - this.AlignOffset;
				this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="bottom-left")
			{
				this.DivObj.style.left = this.AlignOffset;
				this.DivObj.style.top = (relWinH+fixWinH) - this.DivObj.offsetHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-center")
			{
				this.DivObj.style.left = relWinW+fixWinW/2 - this.DivObj.offsetWidth/2;
				this.DivObj.style.top = (relWinH) + this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-left")
			{
				this.DivObj.style.left = this.AlignOffset;
				this.DivObj.style.top = (relWinH) + this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-right")
			{
				this.DivObj.style.left = (relWinW+fixWinW) - this.DivObj.offsetWidth - this.AlignOffset;
				this.DivObj.style.top = (relWinH) + this.AlignOffset;
			}
			
			setTimeout(this.InstanceName+".alwaysOnTop()",50);
	}

	//----------------------------------------------
	//  Mouse Down Event
	//----------------------------------------------
	FloatDiv.prototype.MouseDownEvent = function(e)
	{
		if(this.OnTop==false)
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
					}
				}
		}
	}


//*********************************************
//	Run this when windows loaded
//*********************************************

	//
	//** Instance name will be use in other place, do not change.
	//
	var floatDivInstance = new FloatDiv('floatDivInstance','floatlDiv');

	//
	//** Mouse Event for DivCal
	//
	document.onmousedown = function(e)
	{
		//floatDivInstance.MouseDownEvent(e);
	}

