
// Copyright 2004 Brian Gosselin of ScriptAsylum.com

//*********************************************
//	Class constructor
//*********************************************
function ProgressBar(instancename,w,h,blks,blkclr,bgcolor,s)
{
    //** public members
	this.Width = w;
	this.Height = h;
	this.Blocks = blks;
	this.BlockColor = blkclr;
	this.Speed = s;
	
	this.BGColor = bgcolor;
	
	//** internal members
	this.InstanceName = instancename;
	this.DivName = this.InstanceName+"Div";
	this.tid = null;
	
	this.DOC_IE6 = (document.documentElement)? true:false;									
	this.DOC_IE = (document.all)? true:false;					// IE	
	this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE
	
	this.create();
	
	return this;
}

//*********************************************
//	Public methods
//*********************************************

ProgressBar.prototype.create = function()
{
    var s = '';
    
    s+='<div id="'+this.DivName+'" ';
    s+=' style=" visibility:visible; position:relative; overflow:hidden; ';
    s+=' width:'+this.Width+'px; height:'+this.Height+'px; background:'+this.BGColor+'; border:0px solid #DDDDDD; font-size:1px; ';
    s+=' filter:progid:DXImageTransform.Microsoft.Alpha(style=2,opacity=100,FinishOpacity=35)';
    s+='">';    
    s+='<div id="'+this.DivName+'blocks" style="left:-'+(this.Height*2+1)+'px; position:absolute; font-size:1px">';
    for(i=0;i<this.Blocks;i++)
    {
        s+='<span style="background-color:'+this.BlockColor+'; left:-'+((this.Height*i)+i)+'px; font-size:1px; position:absolute; width:'+this.Height+'px; height:'+this.Height+'px; '
        s+='filter:progid:DXImageTransform.Microsoft.Alpha(style=0,opacity='+(100-i*(100/this.Blocks))+')';
        s+='">';
        s+='</span>';
    }
    s+='</div>';    
    s+='</div>';
    
    document.write(s);
    
	if(this.DOC_IE)
	{
		this.BarObj = eval('document.all.' + this.DivName);
		this.BlkObj = eval('document.all.' + this.DivName+'blocks');
	}		
	else if(this.DOC_DOM) 
	{
		this.BarObj = document.getElementById(this.DivName);
		this.BlkObj = document.getElementById(this.DivName+'blocks');
	}    
}

ProgressBar.prototype.showBar = function()	
{
    this.BarObj.style.visibility="visible";
}
	
ProgressBar.prototype.hideBar = function()	
{
    this.BarObj.style.visibility="hidden";
}
	
ProgressBar.prototype.start = function()
{
    this.tid = setInterval(this.InstanceName+'.run()',this.Speed);
}

ProgressBar.prototype.run = function()	
{
    if(parseInt(this.BlkObj.style.left,10)+this.Height+1-(this.Blocks*this.Height+this.Blocks) > this.Width)
        this.BlkObj.style.left = -(this.Height*2+1)+'px';
     else 
        this.BlkObj.style.left = (parseInt(this.BlkObj.style.left,10)+this.Height+1)+'px';
}
	
ProgressBar.prototype.stop = function()	
{
    if(this.tid!=null)
    {
        clearInterval(this.tid);
        this.tid = null;
    }
}
