//***************************************************************
//																															
// The NavigatePnl.js only support IE5 and above,
// no NS 7 ...........
// Use with your own risk if run in other broswers...					
//
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

function NavigatePnl(instanceName,pnlTitle,pnlWidth)
{
	//** Attributes
	this.Style = 1; // there are 1 and 2
	this.TitleImg = "";
	this.TitleLink = "";
	this.LinkTarget = "";
	this.TableAlign = "";
	this.AutoResize = false;
	this.AutoResizeMargin = 20;
	
	this.SkinBase = "/App_Scripts/";
	this.ImgUp = this.SkinBase + "images/tskpnl_btn_up.gif";
	this.ImgDown = this.SkinBase + "images/tskpnl_btn_down.gif";
	this.ImgPnlTitleLeft = this.SkinBase + "images/tskpnl_left.gif";
	this.ImgPnlTitleBG = this.SkinBase + "images/tskpnl_title_bg.gif";
	
	this.CssTaskPnlTitle = "taskPnlTitle";
	this.CssTaskPnlTitleFocus = "taskPnlTitleFocus";
	this.CssTaskPnlContentBG = "taskPnlContentBG";
	
	this.ScrollSpeed = 7;
	this.TimeoutMS = 10;
	this.ExpandPnl = true;
	this.ShowAnime = true;
	this.AdjustAnime = true;
	
	this.PnlTitleDesc = "";
	
	//** Internal members
	this.InstanceName = instanceName;
	this.ImgName = "img_"+this.InstanceName;
	this.PnlTitle = pnlTitle;
	
	this.ContainerWidth = pnlWidth;		
	this.ContentWidth = pnlWidth;		
	this.RealTabWidth = 0;
	
	this.TIMER_DivAnime = null;
	
	this.DOC_IE = (document.all)? true:false;									// IE	
	this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE, so becareful
	
	return this;
}

//*********************************************
//	Public methods
//*********************************************

//--------------------------
//	Write the DIV tag BEGIN
//--------------------------
NavigatePnl.prototype.writeDivHeader = function()
{
	if(this.Style==2)
	{
		this.ImgUp = this.SkinBase + "images/tskpnl_btn_up_hl.gif";
		this.ImgDown = this.SkinBase + "images/tskpnl_btn_down_hl.gif";
		this.ImgPnlTitleLeft = this.SkinBase + "images/tskpnl_left_hl.gif";
		this.ImgPnlTitleBG = this.SkinBase + "images/tskpnl_title_bg_hl.gif";
		
		this.CssTaskPnlTitle = "taskPnlTitle_hl";
		this.CssTaskPnlTitleFocus = "taskPnlTitleFocus_hl";
	}

	if(this.ExpandPnl)
	{
		this.initImg = this.ImgUp;
		this.initSts = "block";
	}
	else
	{
		this.initImg = this.ImgDown;
		this.initSts = "none";
	}
	
	
	if(this.DOC_IE || this.DOC_DOM)
	{
		//document.write('<div id="TAB_'+this.DivContainer+'" style="width='+this.ContainerWidth+'" align="'+this.TableAlign+'">');
		
		document.write('<table id="TAB_'+this.InstanceName+'" width="'+this.ContainerWidth+'" border="0" cellpadding="0" cellspacing="0" align="'+this.TableAlign+'" style="border-bottom:2px solid #000033;">');
		document.write('<tr><td>');
		
		document.write('<table width="100%"  border="0" cellpadding="0" cellspacing="0" ');
		document.write(' onMouseOver="Utils.setIDClass(\''+this.InstanceName+'_td\',\''+this.CssTaskPnlTitleFocus+'\')" ');
		document.write(' onMouseOut="Utils.setIDClass(\''+this.InstanceName+'_td\',\''+this.CssTaskPnlTitle+'\')" ');
		document.write(' onClick="'+this.InstanceName+'.toggleTaskPnl()');
		if(this.TitleLink!="") document.write(';Utils.gotoURL(\''+this.LinkTarget+'\',\''+this.TitleLink+'\')');
		document.write('" ');
		document.write(' style="cursor:hand;" ');
		document.write(' title="'+this.PnlTitleDesc+'" ');
		document.write('>');
		document.write('<tr>');
		document.write('<td><img src="'+this.ImgPnlTitleLeft+'" width="12" height="25"></td>');
		
		document.write('<td width="100%" background="'+this.ImgPnlTitleBG+'">');
		document.write('<table width="100%"  border="0" cellpadding="0" cellspacing="0" ><tr>');
		document.write('<td><img src="'+this.ImgPnlTitleBG+'"></td>');
		document.write('<td class="'+this.CssTaskPnlTitle+'" id="'+this.InstanceName+'_td">');
		if(this.TitleImg!="") document.write('<img src="'+this.TitleImg+'" align="absmiddle">&nbsp;');
		document.write(this.PnlTitle+'</td>');
		document.write('</tr></table>');
		document.write('</td>');
		
		document.write('<td valign="bottom"><img src="'+this.initImg+'" name="'+this.ImgName+'" width="27" height="25" id="'+this.ImgName+'"></td>');
		document.write('</tr>');
		document.write('</table>');
		
		document.write('</td></tr>');
		document.write('<tr>');
		document.write('<td bgcolor="#FFFFFF">');
		
		//document.write('<div id="Container_'+this.InstanceName+'" style="position:relative;">');
		document.write('<div id="Content_'+this.InstanceName+'" ');
		document.write(' style="display:'+this.initSts+'; visibility:visible; width:'+this.ContentWidth+'px; z-index:999;');
		document.write(' filter:progid:DXImageTransform.Microsoft.GradientWipe(GradientSize=0.75,wipestyle=1,motion=forward);"');
		document.write(' class="'+this.CssTaskPnlContentBG+'"');
		document.write('>');
	}
}

//--------------------------
//	Write the DIV tag END
//--------------------------
NavigatePnl.prototype.writeDivFooter = function()
{
	if(this.DOC_IE || this.DOC_DOM)
	{
		document.write('</div>');
		//document.write('</div>');
		document.write('</td></tr></table>');
		//document.write('</div>');
	}
}

//--------------------------
//	Generate DIV Object
//--------------------------
NavigatePnl.prototype.init = function()
{
	if(this.DOC_IE || this.DOC_DOM)
	{
		//this.DivContainerObj = document.getElementById('Container_'+this.InstanceName);
		this.DivContentObj = document.getElementById('Content_'+this.InstanceName);
		this.ImgObj = document.getElementById(this.ImgName);
	}
	this.adjustPnl();
	
	var obj = document.getElementById('TAB_'+this.InstanceName);
	this.RealTabWidth = obj.clientWidth;
}

//--------------------------
//	show and hide panel
//--------------------------
NavigatePnl.prototype.hidePnl = function()
{
	this.ImgObj.src = this.ImgDown;
	
	if(this.ShowAnime)
	{
		
		//this.DivContentObj.filters[0].Apply();
		this.DivContentObj.style.display = "none";
		//this.DivAnimeClose();
		//this.DivContentObj.filters[0].Play();
		
	}
	else
	{
		this.DivContentObj.style.display = "none";
		//this.DivContainerObj.style.height = 0;
	}
}

NavigatePnl.prototype.showPnl = function()
{
	this.ImgObj.src = this.ImgUp;
	
	if(this.ShowAnime)
	{
		//this.DivContentObj.filters[0].Apply();
		this.DivContentObj.style.display = "block";
		//this.DivAnimeMoveDown();
		//this.DivContentObj.filters[0].Play();
	}
	else
	{
		this.DivContentObj.style.display = "block";
		//this.DivContainerObj.style.height = this.DivContentObj.clientHeight;
		//this.DivContainerObj.style.height = this.DivContentObj.offsetHeight;
	}
}

//--------------------------
//	toggle panel
//--------------------------
NavigatePnl.prototype.toggleTaskPnl = function()
{
	if(this.DivContentObj.style.display=="block")
		this.hidePnl();
	else
		this.showPnl();
}

//----------------------------------------------------------
//	adjust panel if repopular content use javascript
//----------------------------------------------------------
NavigatePnl.prototype.adjustPnl = function()
{
	/*
	if(this.DivContentObj.style.display=="block")
	{
		if(this.AdjustAnime)
			if(parseInt(this.DivContainerObj.clientHeight)<parseInt(this.DivContentObj.clientHeight))
				this.DivAnimeMoveDown();
			else
				this.DivAnimeMoveUp();
		else
		{
			//this.DivContainerObj.style.height = this.DivContentObj.clientHeight;
			this.DivContainerObj.style.height = this.DivContentObj.offsetHeight;
			
		}
	}
	else
	{
		if(this.AdjustAnime)
			this.DivAnimeClose();
		else
			this.DivContainerObj.style.height = 0;
	}
	*/
}

//---------------------------------------
// animation code: close panel
//---------------------------------------
NavigatePnl.prototype.DivAnimeClose = function()
{
	/*
	var tmp = parseInt(this.DivContainerObj.clientHeight);
	if(tmp>0)
	{
		var px = tmp - this.ScrollSpeed;
		if(px<0) px = 0;
		
		this.DivContainerObj.style.height = px+"px";
		this.TIMER_DivAnime = setTimeout(this.InstanceName+".DivAnimeClose()",this.TimeoutMS);
	}
	else
	{
		if(!(this.TIMER_DivAnime==null)) clearTimeout(this.TIMER_DivAnime);
	}
	*/
}


//---------------------------------------
// animation code: move down
//---------------------------------------
NavigatePnl.prototype.DivAnimeMoveDown = function()
{
	/*
	var tmp1 = parseInt(this.DivContainerObj.clientHeight);
	var tmp2 = parseInt(this.DivContentObj.clientHeight);
	
	if(tmp1<tmp2)
	{
		var px = tmp1 + this.ScrollSpeed;
		if(px>tmp2) px = tmp2;
		
		this.DivContainerObj.style.height = px+"px";
		this.TIMER_DivAnime = setTimeout(this.InstanceName+".DivAnimeMoveDown()",this.TimeoutMS);
	}
	else
	{
		if(!(this.TIMER_DivAnime==null)) clearTimeout(this.TIMER_DivAnime);
	}
	*/
}



//---------------------------------------
// animation code: move up
//---------------------------------------
NavigatePnl.prototype.DivAnimeMoveUp = function()
{
	/*
	var tmp1 = parseInt(this.DivContainerObj.clientHeight);
	var tmp2 = parseInt(this.DivContentObj.clientHeight);
	
	if(tmp1>tmp2)
	{
		var px = tmp1 - this.ScrollSpeed;
		if(px<tmp2) px = tmp2;
		
		this.DivContainerObj.style.height = px+"px";
		this.TIMER_DivAnime = setTimeout(this.InstanceName+".DivAnimeMoveUp()",this.TimeoutMS);
	}
	else
	{
		if(!(this.TIMER_DivAnime==null)) clearTimeout(this.TIMER_DivAnime);
	}
	*/
}


//---------------------------------------
// Windows event
//---------------------------------------
NavigatePnl.prototype.WindowResizeHandler = function()
{
	var obj = document.getElementById('TAB_'+this.InstanceName);
	var w = parseInt(document.body.clientWidth-this.AutoResizeMargin);
	
	if(w<this.RealTabWidth)
	{
		obj.width = this.RealTabWidth;
		this.DivContentObj.style.width = obj.clientWidth + "px";
	}
	else
	{
		obj.width = w;
		this.DivContentObj.style.width = obj.clientWidth + "px";
	};
	//this.DivContainerObj.style.height = this.DivContentObj.clientHeight;
	this.DivContainerObj.style.height = this.DivContentObj.offsetHeight;
};





