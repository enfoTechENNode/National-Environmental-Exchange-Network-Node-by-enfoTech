
//***************************************************************
//*																															
//*  The CollapsibleDiv.js only support IE5 and above.
//*																														
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function CollapsibleDiv(divname)
	{
		//** public members
		
		//** internal members
		this.DivName = divname;
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
	CollapsibleDiv.prototype.writeDivHeader = function()
	{
		document.write('<div id="' + this.DivName + '" ');
		document.write(' style="display:none; ');
		document.write('">');
	}

	//--------------------------
	//	Write the DIV tag END
	//--------------------------
	CollapsibleDiv.prototype.writeDivFooter = function()
	{
		document.write('</div>');
	}


	//-----------------------------------------
	//  Collapse/Expand DIV
	//-----------------------------------------
	CollapsibleDiv.prototype.CollapseDiv = function(imgName, expSrc, clpSrc)
	{
				if(this.DOC_IE) this.DivObj = eval('document.all.' + this.DivName);
				if(this.DOC_DOM) this.DivObj = document.getElementById(this.DivName);
				
				this.DivObj.style.display = (this.DivObj.style.display=="none") ? "block" : "none";
				
				var imgobj;
				
				if(this.DOC_IE) imgobj = eval('document.all.' + imgName);
				if(this.DOC_DOM) imgobj = document.getElementById(imgName);
				
				imgobj.src = (this.DivObj.style.display=="none") ? expSrc : clpSrc;
	}

