
//***************************************************************
//*																															*
//*  
//*	 
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

function CallBackDiv(instancename)
{
	this.DivName = instancename;
	this.KeySelect = -1;
	this.ResultSets = null;
	this.EleUniqID = "";
	this.RcdSplt = "_||_";
	this.FldSplt = "|";
	this.doCallBack = true;
	
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
CallBackDiv.prototype.writeDiv = function()
{
    document.write('<div id="' + this.DivName + '" name="'+ this.DivName +'" class="eaf_CallBackDiv"></div>');
    
    // iframe for block selectbox
	document.write('<iframe id="'+this.DivName+'IFrame"');
	document.write(' src="" scrolling="no" frameborder="0"');
	document.write(' style="position:absolute; width:20px; hright:20px; top:0px; left:0px; display:none;">');
	document.write('</iframe>');		
}

CallBackDiv.prototype.init = function()
{
    this.writeDiv();   
    
    this.DivObj = document.getElementById(this.DivName);
    this.IFrameBG = document.getElementById(this.DivName+'IFrame');
}

CallBackDiv.prototype.adjustIframe = function()
{
	this.IFrameBG.style.width = this.DivObj.offsetWidth;
	this.IFrameBG.style.height = this.DivObj.offsetHeight;
	this.IFrameBG.style.top = this.DivObj.style.top;
	this.IFrameBG.style.left = this.DivObj.style.left;
	this.IFrameBG.style.zIndex = this.DivObj.style.zIndex+1;
}

CallBackDiv.prototype.hide = function()
{
	this.DivObj.style.visibility='hidden';
	this.IFrameBG.style.display = "none";
	this.KeySelect = -1;
}

CallBackDiv.prototype.delayHide = function(id, checkSelected)
{
	if(checkSelected)
	   return setTimeout(this.DivName+".chkTextBoxVal('"+id+"')",200);
	else
	   return setTimeout(this.DivName+".hide()",200); 
}

CallBackDiv.prototype.show = function()
{
	this.adjustIframe();	
	this.DivObj.style.visibility='visible';
	this.IFrameBG.style.display = "block";
}

CallBackDiv.prototype.getCallBackVal = function(id,spltr)
{
    if(this.doCallBack)
        return id + spltr + document.getElementById(id).value;
    else
        return false;
}

CallBackDiv.prototype.setTextBoxVal = function(id,txt,val,isHide)
{
    document.getElementById(id).value = txt;
    document.getElementById(id+"Hidden").value = val;
    
    if(isHide) this.hide();        
}

CallBackDiv.prototype.chkTextBoxVal = function(id)
{
    var txtObj = document.getElementById(id);
    var hidTxtObj = document.getElementById(id+"Hidden");
    
    var t = Utils.trimString(txtObj.value);
    var v = hidTxtObj.value;
    
    if(t!="")
    { 
        if(v=="")
        {
            alert("Please select an item from the list.");
            txtObj.focus();
            txtObj.select();
            return false;
        }
        else
        {
            var found = false;
            for(i=0; i<this.ResultSets.length; i++)
	        {
		        var keyval = this.ResultSets[i].split(this.FldSplt);
		        if((typeof keyval[0]!="undefined") && keyval[0]==t)
		        {
		            found = true;
		            break;
		        }
		    }
		    if(!found)
		    {
                alert("Please make sure the item is in the list.");
                txtObj.focus();
                txtObj.select();
                return false;		        
		    }
		    else
		    {
                this.hide();        
                return true;
		    }
        }
    }
    else
    {
        hidTxtObj.value = "";
        this.hide();        
        return true;
    }
}

CallBackDiv.prototype.showResult = function()
{
	var s = "";
	
	s += "<table cellspacing=\"0\" class=\"eaf_CallBackTab\">";
	for(i=0; i<this.ResultSets.length; i++)
	{
		var keyval = this.ResultSets[i].split(this.FldSplt);
		if((typeof keyval[0]!="undefined") && (typeof keyval[1]!="undefined") && (typeof keyval[2]!="undefined"))
		{
			var curTRID = this.EleUniqID+"TR_"+this.KeySelect;
			var tmpTRID = this.EleUniqID+"TR_"+i;
			
			var jsTxt = keyval[0].replace(/'/,"\\\'");
			var jsVal = keyval[1].replace(/'/,"\\\'");
			
			var htmlTxt = keyval[0];
			var htmlDesc = keyval[2];
			
			s += "<tr id=\""+this.EleUniqID+"TR_"+i+"\" onclick=\""+this.DivName+".setTextBoxVal('"+this.EleUniqID + "','" + jsTxt + "','" + jsVal + "',true)\"";
			
			if(tmpTRID==curTRID)
			    s += "class=\"mover\"";
			else
			    s += "class=\"mout\"";
			    
			s += " onmouseover=\"Utils.setObjClass(this,'mover')\"";
			s += " onmouseout=\"Utils.setObjClass(this,'mout')\"";
			s += " >";
			
			s += "<td class=\"lft\">";
			s += "<a href=\"javascript:;\" onclick=\""+this.DivName+".setTextBoxVal('" +this.EleUniqID+ "','" + jsTxt + "','" + jsVal + "',true)\">";
			s += htmlTxt;
			s += "</a>";
			s += "</td>";
			s += "<td class=\"rgt\">" + htmlDesc + "</td>";
			s += "</tr>";
		}
	}
	s += "</table>";

	this.DivObj.innerHTML = s;	
	this.show();
}

CallBackDiv.prototype.highlightRow = function(oldID)
{
    var newID = this.getTRID(this.KeySelect);

    if(newID!=oldID)
    {
        if(newID!="") Utils.setIDClass(newID,'mover');
        if(oldID!="") Utils.setIDClass(oldID,'mout');
    }
    
    var keyval = this.ResultSets[this.KeySelect].split(this.FldSplt);
    this.setTextBoxVal(this.EleUniqID,keyval[0],keyval[1],false);
}

CallBackDiv.prototype.getTRID = function(idx)
{
    var id = "";
    if(idx>=0)
        id = this.EleUniqID+"TR_"+idx;
        
    return id;
}

CallBackDiv.prototype.callBackHandler = function(result,context,wid,rcdSplt,fldSplt)
{
	if(result!="")
	{	        
	    this.EleUniqID = context;
	    this.ResultSets = result.split(this.RcdSplt);	    

	    var txtObj = document.getElementById(this.EleUniqID);

	    var left = eval(Utils.findPosX(txtObj));
	    var top = eval(Utils.findPosY(txtObj) + txtObj.offsetHeight + 1 );
        
	    this.DivObj.style.left = left + "px";
	    this.DivObj.style.top = top + "px";    
	    this.DivObj.style.width = wid + "px";
	    
	    this.showResult();
	}
	else
	{
	    this.hide();
	}
}

//----------------------------------------------
//  Mouse Down Event
//----------------------------------------------
CallBackDiv.prototype.KeyDownEvent = function()
{    
    if(this.DivObj.style.visibility=='visible')
    {
        var oldID = this.getTRID(this.KeySelect);
        this.doCallBack = true;
        
        if(event.keyCode=='38')
        {
            this.doCallBack = false;
            
            // arrow move up
            this.KeySelect--;
            if (this.KeySelect<0) this.KeySelect = 0;
            
            this.highlightRow(oldID);
        }
        
        else if(event.keyCode=='40')
        {
            this.doCallBack = false;
            
            // arrow move down
            this.KeySelect++;
            if (this.KeySelect> this.ResultSets.length-1) this.KeySelect = this.ResultSets.length-1;
            
            this.highlightRow(oldID);
        }        
        else 
        {
            if(oldID!="") Utils.setIDClass(oldID,'mout');
            this.KeySelect = -1;
        }
    }   
}


//** invoke in the page

var eaf_CallBackDiv = new CallBackDiv('eaf_CallBackDiv');
eaf_CallBackDiv.init();

// Mouse Event for DivCal
//--------------------------

function CallBackDivKeyDown() 
{ 
    eaf_CallBackDiv.KeyDownEvent(); 
};

if(document.attachEvent)
{
	document.attachEvent('onkeydown',CallBackDivKeyDown);
}
else if(document.addEventListener)
{
	document.addEventListener('onkeydown',CallBackDivKeyDown,false);
};
