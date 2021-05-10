
//***************************************************************
//*																															*
//*  The DivPopupTip.js only support IE5 and above, and NS 7.		*
//*	 Use with your own risk if run in other broswers...					*
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function DivPopupTip(instancename, divname)
	{
		//** Important attributes
		this.ImgBaseUrl = "../../WCSkin/images";
		this.ColorStyle = "_Blue";
		
		//** Basic attributes
		this.TableWidth = 200;
		this.OffsetX = -10;
		this.OffsetY = 18;
		this.Opacity = 95;
		this.TipContent = "......";
		this.TipAlign = "left";

		//** Css attributes
		this.CssTipFont = "popupTipFont";

		//** Internal members
		this.InstanceName = instancename;
		this.DivName = divname;
		
		this.DOC_IE = (document.all)? true:false;									// IE	
		this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE, so becareful
		
		this.ShowTip = false;
		this.FixedTip = false;
		this.RunOnceFlag = false;
		
		//** Run initial process
		//this.init();
		
		return this;
	}

//*********************************************
//	Public methods
//*********************************************

	//--------------------
	// Initial functions
	//--------------------
	DivPopupTip.prototype.init = function()
	{
		this.writeCalDiv();
		
		if(this.DOC_IE) this.DivObj = eval('document.all.' + this.DivName);
		if(this.DOC_DOM) this.DivObj = document.getElementById(this.DivName);
		
		this.DivObj.innerHTML = this.genHtml('L');
	}

	//-----------------------
	//	Write the DIV tag
	//-----------------------
	DivPopupTip.prototype.writeCalDiv = function()
	{

		document.write('<div id="' + this.DivName + '" ');
		document.write(' style="position:absolute; z-index:999999;  visibility:hidden; ');
		document.write(' filter:');
		document.write(' progid:DXImageTransform.Microsoft.Shadow(color=#666666,Direction=120,Strength=3,positive=true)');
		document.write(' progid:DXImageTransform.Microsoft.Alpha( style=0,opacity='+this.Opacity+');');
		document.write('">');
		document.write('</div>');
	}

	//-------------------------------------------
	//	Main method for generate HTML
	//  Align is for deciding the arrow images
	//-------------------------------------------
	DivPopupTip.prototype.genHtml = function(align)
	{
		var output = '';
		
		output += '<table width="'+this.TableWidth+'" border="0" cellpadding="0" cellspacing="0">';
		output += '<tr>';
		
    output += '<td><img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpLeft.gif" /></td>';
    
		if(align.toUpperCase()=='R')
		{
			output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpBG.gif" align="right">';
			output += '<img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpArrowRight.gif" /></td>';
    }
    else
		{
			output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpBG.gif" align="left">';
			output += '<img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpArrowLeft.gif" /></td>';
    }
    
    output += '<td><img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_UpRight.gif" /></td>';
		
		output +='</tr><tr>';
		
    output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_LeftBG.gif">';
    output += '<img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_LeftBG.gif" /></td>';
    
    output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_CenterBG.gif" width="100%">';
    

    output += '<table border="0" cellspacing="0" cellpadding="2"  align="'+this.TipAlign+'"><tr>';
    output += '<td class="'+this.CssTipFont+'">'+ this.TipContent +'</td>';
		output += '</tr></table>';

      
    output += '</td>';
    
    output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_RightBG.gif">';
    output += '<img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_RightBG.gif" /></td>';
		    
    output += '</tr>';
    
    output += '<tr>';
    
    output += '<td><img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_BtmLeft.gif" /></td>';
    output += '<td background="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_BtmBG.gif">';
    output += '<img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_BtmBG.gif" /></td>';
    
    output += '<td><img src="'+this.ImgBaseUrl+'/PopTip'+this.ColorStyle+'_BtmRight.gif" /></td>';
		
		output += '</tr></table>';

		return output;
	}
	
	//-----------------------------------------
	//	Set the DIV layer ready to show
	//	can be fixed or moving with mouse
	//-----------------------------------------
	DivPopupTip.prototype.showTip = function(isFixed, content, tabW, tipAlign, style)
	{
		if(typeof style != 'undefined')		this.ColorStyle = style;
		if(typeof tabW != 'undefined')		this.TableWidth = tabW;
		if(typeof tipAlign != 'undefined')		this.TipAlign = tipAlign;
		
		this.TipContent = content;
		this.FixedTip = isFixed;
		this.ShowTip = true;
		//** reall show action will be in _showTip()
	}
	
	//-----------------------------------------
	// Hide DIV layer
	//-----------------------------------------
	DivPopupTip.prototype.hideTip = function()
	{
		this.ShowTip = false;
		this.RunOnceFlag = false;
		this.DivObj.style.visibility = 'hidden';
	}

	//-----------------------------------------
	//  Move to X,Y 
	//-----------------------------------------
	DivPopupTip.prototype._showTip = function(x,y)
	{
		
		//
		//** Testing the position of the DIV tip
		//** for both IE and NS7
		//
		var testX = eval(document.body.clientWidth-x);
		
		if(testX<this.TableWidth)
		{
			this.DivObj.innerHTML = this.genHtml('R');
			this.DivObj.style.left = eval(x-this.TableWidth-this.OffsetX) + "px";
			this.DivObj.style.top = eval(y+this.OffsetY) + "px";
		}
		else
		{
			this.DivObj.innerHTML = this.genHtml('L');
			this.DivObj.style.left = eval(x+this.OffsetX) + "px";
			this.DivObj.style.top = eval(y+this.OffsetY) + "px";
		}

		this.DivObj.style.visibility = 'visible';
	}

	//-----------------------------------------
	//	Move Move event
	//-----------------------------------------
	DivPopupTip.prototype.MouseMoveEvent = function(e)
	{
		if(this.ShowTip==true)
		{
			
			if(this.RunOnceFlag==false)
			{
				var x = null;
				var y = null;
				
				if (this.DOC_IE)
				{
					x = eval(event.clientX+document.body.scrollLeft);
					y = eval(event.clientY+document.body.scrollTop);
				}
				else if (this.DOC_DOM && !this.DOC_IE) // NS7
				{
					x = e.pageX;
					y = e.pageY;
				}
				
				this._showTip(x,y);
				
				if(this.FixedTip==true)
				{
					
					this.RunOnceFlag = true;
				}
			}
			
		}
	}

//*****************************************************
//	External methods : Mouse Event for DivCal
//*****************************************************

	//
	//** Create onject
	//
	var divPopupTipInstance = new DivPopupTip('divPopupTipInstance','myPopupTip');
	
	//
	//** Set mouse move event
	//
	document.onmousemove = function(e)
	{
		divPopupTipInstance.MouseMoveEvent(e);
	}
