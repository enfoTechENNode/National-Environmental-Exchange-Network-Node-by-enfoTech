
//***************************************************************
//*																															*
//*  The FloatDiv.js only support IE5 and above, and NS 7.		
//*	 Use with your own risk if run in other broswers...					
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function FloatDiv(instancename, zidx)
	{
		//** public members
		this.OnTop = false;
		this.Shadow = false;
		this.OnTopAlign = "bottom-right";
		this.AlignOffset = 10;
		this.Opacity = 50;
		
		this.ShowGlassPane = false;
		this.GlassOpacity = 80;
		this.GlassBGImg = '/Website/App_Images/EAF/black_rock.jpg';

		
		//** internal members
		this.InstanceName = instancename;
		this.DivName = this.InstanceName+"Div";
		this.Zindex = zidx;
		
		this.DOC_IE6 = (document.documentElement)? true:false;									
		this.DOC_IE = (document.all)? true:false;					// IE	
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
		document.write(' style="position:absolute; z-index:'+this.Zindex+';  visibility:hidden;');
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

	    // iframe for block selectbox
	    document.write('<iframe id="' + this.DivName + 'iframe" ');
	    document.write(' src="javascript:false;" scrolling="no" frameborder="0" ');
	    document.write(' style="position:absolute; top:0px; left:0px; display:none;');
	    document.write(' filter:progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0);');
	    document.write('">');		
	    document.write('</iframe>');
	    
   	    // glass pane
   	    document.write('<div id="' + this.DivName + 'glass" ');
	    document.write(' style="position:absolute; top:0px; left:0px; visibility:hidden; background:#000033 url('+this.GlassBGImg+');');
	    document.write(' filter:progid:DXImageTransform.Microsoft.Alpha( style=0,opacity='+this.GlassOpacity+');');
	    document.write('">');		
	    document.write('</div>');
	}

	//-------------------
	// Initial functions
	//--------------------
	FloatDiv.prototype.init = function()
	{
		if(this.DOC_IE)
		{
			this.DivObj = eval('document.all.' + this.DivName);
			this.DivObjGlass = eval('document.all.' + this.DivName + 'glass');
			this.IframeObj = eval('document.all.' + this.DivName + 'iframe');			
		}		
		else if(this.DOC_DOM) 
		{
			this.DivObj = document.getElementById(this.DivName);
			this.DivObjGlass = document.getElementById(this.DivName + 'glass');
			this.IframeObj = document.getElementById(this.DivName + 'iframe');
		}	
		
		if(this.OnTop==true) this.alwaysOnTop();
	}
	
	
	FloatDiv.prototype.adjustGlass = function()
	{
		this.DivObjGlass.style.width = document.documentElement.clientWidth;
		this.DivObjGlass.style.height = document.documentElement.clientHeight;
		this.DivObjGlass.style.left = '0px';
		this.DivObjGlass.style.top = document.documentElement.scrollTop;
		this.DivObjGlass.style.zIndex = this.DivObj.style.zIndex-1;
	}
	

	FloatDiv.prototype.adjustIframe = function()
	{
		var obj = this.DivObj;
		if(this.ShowGlassPane) obj = this.DivObjGlass;
		
		this.IframeObj.style.width = obj.offsetWidth;
		this.IframeObj.style.height = obj.offsetHeight;
		this.IframeObj.style.top = obj.style.top;
		this.IframeObj.style.left = obj.style.left;
		this.IframeObj.style.zIndex = obj.style.zIndex-2;
	}
	
	//----------------------------------
	//	Detecting if inside DIV tag
	//-----------------------------------
	FloatDiv.prototype.isInDivLayer = function(x,y)
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

	//-----------------------------------------
	//  Hide DIV
	//-----------------------------------------
	FloatDiv.prototype.hideDiv = function()
	{
		this.DivObj.style.visibility='hidden';
		this.DivObjGlass.style.visibility='hidden';
		this.IframeObj.style.display = "none";
	}

	//-----------------------------------------
	//  Show DIV
	//-----------------------------------------
	FloatDiv.prototype.showDiv = function()
	{
		this.DivObj.style.visibility='visible';
		this.DivObjGlass.style.visibility='visible';
		this.IframeObj.style.display = "block";
	}

	//-----------------------------------------
	// Major function for javascript to call
	//-----------------------------------------
	FloatDiv.prototype.switchDiv = function(imgName,offsetX,offsetY)
	{
		if(this.DivObj.style.visibility=='visible')
		{
			this.hideDiv();
		}
		else
		{
			if(this.OnTop==false)
			{
				var imgObj = eval('document.' + imgName);
				this.DivObj.style.left = eval(findPosX(imgObj)+offsetX) + "px";
				this.DivObj.style.top = eval(findPosY(imgObj)+imgObj.height+offsetY) + "px";
			}			

			
			if(this.ShowGlassPane) this.adjustGlass();
			this.adjustIframe();
			
			this.showDiv();
		}

	}


	FloatDiv.prototype.switchFloatDiv = function()
	{
		if(this.DivObj.style.visibility=='visible')
			this.hideDiv();
		else
			this.showDiv();
	}

	//-----------------------------------------
	// Major function for javascript to call
	//-----------------------------------------

	FloatDiv.prototype.alwaysOnTop = function()
	{
			if (this.DOC_IE6)
			{
				fixWinW = parseInt(document.documentElement.clientWidth,10);
				fixWinH = parseInt(document.documentElement.clientHeight,10);
				
				relWinW = parseInt(document.documentElement.scrollLeft,10);
				relWinH = parseInt(document.documentElement.scrollTop,10);
			}	
			else if (this.DOC_IE)
			{
				fixWinW = parseInt(document.body.clientWidth,10);
				fixWinH = parseInt(document.body.clientHeight,10);
				
				relWinW = parseInt(document.body.scrollLeft,10);
				relWinH = parseInt(document.body.scrollTop,10);
			}	
			else if (this.DOC_DOM)
			{
				fixWinW = parseInt(window.innerWidth,10);
				fixWinH = parseInt(window.innerHeight,10);
				
				relWinW = parseInt(window.pageXOffset,10);
				relWinH = parseInt(window.pageYOffset,10);
			} 
					
			var ll = 0;
			var tt = 0;
			
			var objWidth = parseInt(this.DivObj.offsetWidth,10);
			var objHeight = parseInt(this.DivObj.offsetHeight,10);
			
			if(this.OnTopAlign=="center")
			{
				ll = relWinW+fixWinW/2 - objWidth/2;
				tt = relWinH+fixWinH/2 - objHeight/2;
			}
			else if(this.OnTopAlign=="bottom-center")
			{
				ll = relWinW+fixWinW/2 - objWidth/2;
				tt = (relWinH+fixWinH) - objHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="bottom-right")
			{
				ll = (relWinW+fixWinW) - objWidth - this.AlignOffset;
				tt = (relWinH+fixWinH) - objHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="bottom-left")
			{
				ll = this.AlignOffset;
				tt = (relWinH+fixWinH) - objHeight - this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-center")
			{
				ll = relWinW+fixWinW/2 - objWidth/2;
				tt = (relWinH) + this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-left")
			{
				ll = this.AlignOffset;
				tt = (relWinH) + this.AlignOffset;
			}
			else if(this.OnTopAlign=="top-right")
			{
				ll = (relWinW+fixWinW) - objWidth - this.AlignOffset;
				tt = (relWinH) + this.AlignOffset;
			}
			
			this.DivObj.style.left = ll+"px";
			this.DivObj.style.top = tt+"px";
			
			if(this.ShowGlassPane) this.adjustGlass();
			this.adjustIframe();
			
			setTimeout(this.InstanceName+".alwaysOnTop()",30);
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
					
					if (this.DOC_IE6)
					{
						// alert(event.button);
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
						this.hideDiv();
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
	//var floatDivInstance = new FloatDiv('floatDivInstance');

	//
	//** Mouse Event for DivCal
	//
	/*
	document.onmousedown = function(e)
	{
		//floatDivInstance.MouseDownEvent(e);
	}
	*/

