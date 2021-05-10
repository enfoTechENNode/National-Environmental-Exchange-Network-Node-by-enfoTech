
//***************************************************************
//*																															*
//*  The PopupTip.js only support IE5 and above, and NS 7.		
//*	 Use with your own risk if run in other broswers...			
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function PopupTip(instancename, divname)
	{
		//** Important attributes
		this.ImgBaseUrl = "../../WCSkin/images";
		this.ColorStyle = "_Blue";
		
		//** Basic attributes
		this.TableWidth = -1;
		this.OffsetX = -5;
		this.OffsetY = 20;
		this.Opacity = 100;
		
		this.TipTitle = "Title";
		this.TipContent = "......";
		//this.TipAlign = "left";

		//** Css attributes
		this.CssTipTable = "eaf_PopupTipTable";
		this.CssTipTtl = "eaf_PopupTipTtl";
		this.CssTipCnt = "eaf_PopupTipCnt";

		//** Internal members
		this.InstanceName = instancename;
		this.DivName = instancename+"DivName";
		
		this.DOC_IE6 = (document.documentElement)? true:false;
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
	PopupTip.prototype.init = function()
	{
		this.writeCalDiv();
		
		if(this.DOC_IE)
		{
			this.DivObj = eval('document.all.' + this.DivName);
			this.IframeObj = eval('document.all.' + this.DivName + 'iframe');
		}
		else if(this.DOC_DOM) 
		{
			this.DivObj = document.getElementById(this.DivName);
			this.IframeObj = document.getElementById(this.DivName + 'iframe');
		}	
		
		//this.DivObj.innerHTML = this.genHtml('L');
	}

	//-----------------------
	//	Write the DIV tag
	//-----------------------
	PopupTip.prototype.writeCalDiv = function()
	{

		document.write('<div id="' + this.DivName + '" ');
		document.write(' style="position:absolute; z-index:999999;  visibility:hidden; ');
		document.write(' filter:');
		document.write(' progid:DXImageTransform.Microsoft.Shadow(color=#999999,Direction=130,Strength=3,positive=true)');
		document.write(' progid:DXImageTransform.Microsoft.Alpha( style=0,opacity='+this.Opacity+');');
		document.write('">');
		document.write('</div>');
		
		// iframe for block selectbox
		document.write('<iframe id="' + this.DivName + 'iframe" ');
		document.write(' src="javascript:false;" scrolling="no" frameborder="0" ');
		document.write(' style="position:absolute; top:0px; left:0px; display:none;');
	    document.write(' filter:progid:DXImageTransform.Microsoft.Alpha( style=0,opacity=0);');
	    document.write('">');	
		document.write('</iframe>');		

	}

	PopupTip.prototype.adjustIframe = function()
	{
		this.IframeObj.style.width = this.DivObj.offsetWidth;
		this.IframeObj.style.height = this.DivObj.offsetHeight;
		this.IframeObj.style.top = this.DivObj.style.top;
		this.IframeObj.style.left = this.DivObj.style.left;
		this.IframeObj.style.zIndex = this.DivObj.style.zIndex-1;
	}

	//-------------------------------------------
	//	Main method for generate HTML
	//  Align is for deciding the arrow images
	//-------------------------------------------
	/*
	PopupTip.prototype.genHtml = function(align)
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
    */
    //--------------------------------------------------------------
	PopupTip.prototype.genTipHtml = function()
	{
		var output = '';

        output += '<table cellspacing="0" class="'+this.CssTipTable+'';
        if(this.TableWidth>0) output += ' style="width:'+this.TableWidth+'" ';
        output += '">';
        output += '<tr><td class="'+this.CssTipTtl+'">'+ this.TipTitle +'</td></tr>';
        output += '<tr><td class="'+this.CssTipCnt+'">'+ this.TipContent +'</td></tr>';
	    output += '</table>';

		return output;
	}	
	//-----------------------------------------
	//	Set the DIV layer ready to show
	//	can be fixed or moving with mouse
	//-----------------------------------------
	PopupTip.prototype.showTip = function(ttl, content, tabW, isFixed)
	{
		if(typeof tabW != 'undefined')  this.TableWidth = tabW;
		if(typeof isFixed != 'undefined')  this.FixedTip = isFixed;
		
		this.TipTitle = ttl;
		this.TipContent = content;
		
		this.ShowTip = true;
		
		//** reall show action will be in _showTip()
	}
	
	//-----------------------------------------
	// Hide DIV layer
	//-----------------------------------------
	PopupTip.prototype.hideTip = function()
	{
		this.ShowTip = false;
		this.RunOnceFlag = false;
		
		this.DivObj.style.visibility = 'hidden';
		this.IframeObj.style.display = "none";
	}

	//-----------------------------------------
	//  Move to X,Y 
	//-----------------------------------------
	PopupTip.prototype._showTip = function(x,y)
	{
		
		//
		//** Testing the position of the DIV tip
		//** for both IE and NS7
		//
		var testX = eval(document.body.clientWidth-x);
		this.DivObj.innerHTML = this.genTipHtml();
		
		if(testX<this.TableWidth)
		{
			//this.DivObj.innerHTML = this.genHtml('R');
			this.DivObj.style.left = eval(x-this.TableWidth-this.OffsetX) + "px";
			this.DivObj.style.top = eval(y+this.OffsetY) + "px";
		}
		else
		{
			//this.DivObj.innerHTML = this.genHtml('L');
			this.DivObj.style.left = eval(x+this.OffsetX) + "px";
			this.DivObj.style.top = eval(y+this.OffsetY) + "px";
		}

		this.adjustIframe();
		
		this.DivObj.style.visibility = 'visible';
		this.IframeObj.style.display = "block";
	}

	//-----------------------------------------
	//	Move Move event
	//-----------------------------------------
	PopupTip.prototype.MouseMoveEvent = function(e)
	{
		if(this.ShowTip==true)
		{
			if(this.RunOnceFlag==false)
			{
				var x = null;
				var y = null;
				
				if (this.DOC_IE6)
				{
					x = eval(event.clientX+document.documentElement.scrollLeft);
					y = eval(event.clientY+document.documentElement.scrollTop);
				}
				else if (this.DOC_IE)
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
								
				if(this.FixedTip==true)  this.RunOnceFlag = true;
			}		
		}
	}

//*****************************************************
//	External methods : Mouse Event for DivCal
//*****************************************************

	//** Create onject
	//----------------------
	var eaf_PopupTip = new PopupTip('eaf_PopupTip');
	
	eaf_PopupTip.init();
	
	
	//** Set mouse move event
	//---------------------------
	document.onmousemove = function(e)
	{
		eaf_PopupTip.MouseMoveEvent(e);
	};
