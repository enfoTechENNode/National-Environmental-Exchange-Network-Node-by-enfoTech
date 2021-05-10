//***************************************************************
//*																															*
//*  The DivScroller.js only support IE5 and above, and NS 7.		*
//*	 Use with your own risk if run in other broswers...					*
//*																															*
//***************************************************************

//*********************************************
//	Constructor
//*********************************************

	function DivScroller(instancename, div1, div2, span1)
	{
		//** Attributes
		this.ContainerWidth = 300;
		this.ContainerHeight = 100;
		this.ScrollSpeed = 3;
		this.IncreasingSpeed = 0.2;
		
		//** Internal members
		this.InstanceName = instancename;
		this.DivContainer = div1;
		this.DivContent = div2;
		this.SpanContent = span1;
		
		this.InternalScrollSpeed = 3;
		this.DOC_IE = (document.all)? true:false;									// IE	
		this.DOC_DOM = (document.getElementById) ? true : false;	// NS7 , also IE, so becareful
		this.DOC_NS4 = (document.layers)? true:false;
		
		return this;
	}

//*********************************************
//	Public methods
//*********************************************

	//--------------------------
	//	Write the DIV tag BEGIN
	//--------------------------
	DivScroller.prototype.writeDivHeader = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			document.write('<div id="'+this.DivContainer+'" ');
			document.write(' style="position:relative; overflow:hidden; ');
			document.write(' width:'+this.ContainerWidth+'px; height:'+this.ContainerHeight+'px;');
			document.write('">');

			document.write('<div id="'+this.DivContent+'" ');
			document.write(' style="position:absolute; left:0px; top:0px;');
			document.write('">');
			
			document.write('<span id="'+this.SpanContent+'">');
		}
		else if(this.DOC_NS4)
		{
			document.write('<ilayer name="'+this.DivContainer+'" width="'+this.ContainerWidth+'" height="'+this.ContainerHeight+'" clip="0,0,'+this.ContainerWidth+','+this.ContainerHeight+'">');
			document.write('<layer name="'+this.DivContent+'" width="'+this.ContainerWidth+'" height="'+this.ContainerHeight+'" visibility="hidden">');
		}
	}

	//--------------------------
	//	Write the DIV tag END
	//--------------------------
	DivScroller.prototype.writeDivFooter = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			document.write('<\/span>');
			document.write('<\/div><\/div>');
		}
		else if(this.DOC_NS4)
		{
			document.write('</layer></ilayer>');
		}
		
	}

	//--------------------------
	//	Generate DIV Object
	//--------------------------
	DivScroller.prototype.init = function()
	{
		this.InternalScrollSpeed = this.ScrollSpeed;
		
		if(this.DOC_IE)
		{
			this.DivObj = eval('document.all.' + this.DivContent);
			
			this.ContentWidth = this.DivObj.offsetWidth;
			this.ContentHeight = this.DivObj.offsetHeight;
		}
		else if(this.DOC_DOM)
		{
			this.DivObj = document.getElementById(this.DivContent);
			this.ContentWidth = document.getElementById(this.SpanContent).offsetWidth;
			this.ContentHeight = document.getElementById(this.SpanContent).offsetHeight;
		}
		else if(this.DOC_NS4)
		{
			this.DivObj = eval('document.'+ this.DivContainer +'.document.' + this.DivContent);
			this.ContentWidth = this.DivObj.document.width;
			this.ContentHeight = this.DivObj.document.offsetHeight;
		}
	}
	
	//--------------------
	// Move layer left
	//--------------------
	DivScroller.prototype.scrollLeft = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			var dLeft = parseInt(this.DivObj.style.left);
			
			if(dLeft>(this.ContainerWidth-this.ContentWidth))
			{
				if((dLeft-this.InternalScrollSpeed)>(this.ContainerWidth-this.ContentWidth))
					this.DivObj.style.left = dLeft - this.InternalScrollSpeed;
				else
					this.DivObj.style.left = this.ContainerWidth-this.ContentWidth;
			}
			this.InternalScrollSpeed += this.IncreasingSpeed;
		}
		else if(this.DOC_NS4)
		{
			/*
			if(parseInt(this.DivObj.left)>(this.ContainerWidth-this.ContentWidth))
			{
				this.DivObj.left = parseInt(this.DivObj.left) - v.ScrollSpeed;
			}
			*/
		}		
		
		DivScroller.LeftTimeVar = setTimeout(this.InstanceName+".scrollLeft()",50);
	}
	
	//--------------------
	// Move layer right
	//--------------------
	DivScroller.prototype.scrollRight = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			var dLeft = parseInt(this.DivObj.style.left);
			
			if(dLeft<0)
			{
				if((dLeft+this.InternalScrollSpeed)<0)
					this.DivObj.style.left = parseInt(this.DivObj.style.left) + this.InternalScrollSpeed;
				else
					this.DivObj.style.left = 0;
			}
			this.InternalScrollSpeed += this.IncreasingSpeed;
		}
		else if(this.DOC_NS4)
		{
			/*
			if(parseInt(this.DivObj.left)<0)
				this.DivObj.left = parseInt(this.DivObj.left) + this.ScrollSpeed;
			*/
		}		
		
		DivScroller.RightTimeVar = setTimeout(this.InstanceName+".scrollRight()",50);
	}

	//--------------------
	// Move layer Up
	//--------------------
	DivScroller.prototype.scrollUp = function()
	{
		var dTop = parseInt(this.DivObj.style.top);
		
		if(this.DOC_IE || this.DOC_DOM)
		{
			if(dTop>(this.ContainerHeight-this.ContentHeight))
			{
				if((dTop-this.InternalScrollSpeed)>(this.ContainerHeight-this.ContentHeight))
					this.DivObj.style.top = dTop-this.InternalScrollSpeed;
				else
					this.DivObj.style.top = this.ContainerHeight-this.ContentHeight;
			}
			this.InternalScrollSpeed += this.IncreasingSpeed;
		}
		else if(this.DOC_NS4)
		{
			if(parseInt(this.DivObj.top)>(this.ContainerHeight-this.ContentHeight))
				this.DivObj.top = parseInt(this.DivObj.top) - this.ScrollSpeed;
		}		
		
		DivScroller.UpTimeVar = setTimeout(this.InstanceName+".scrollUp()",50);
	}

	//--------------------
	// Move layer Down
	//--------------------
	DivScroller.prototype.scrollDown = function()
	{
		var dTop = parseInt(this.DivObj.style.top);
		
		if(this.DOC_IE || this.DOC_DOM)
		{
			if(dTop<0)
			{
				if((dTop+this.InternalScrollSpeed)<0)
					this.DivObj.style.top = dTop+this.InternalScrollSpeed;
				else
					this.DivObj.style.top = 0;
			}
			this.InternalScrollSpeed += this.IncreasingSpeed;
		}
		else if(this.DOC_NS4)
		{
			if(parseInt(this.DivObj.top)<0)
				this.DivObj.top = parseInt(this.DivObj.top) + this.ScrollSpeed;
		}		
		
		DivScroller.DownTimeVar = setTimeout(this.InstanceName+".scrollDown()",50);
	}


	//--------------------
	// Clear Time out
	//--------------------
	DivScroller.prototype.clearMove = function(dir)
	{
		if(dir.toUpperCase()=="LEFT")	clearTimeout(DivScroller.LeftTimeVar);
		else if(dir.toUpperCase()=="RIGHT")	clearTimeout(DivScroller.RightTimeVar);
		else if(dir.toUpperCase()=="UP")	clearTimeout(DivScroller.UpTimeVar);
		else if(dir.toUpperCase()=="DOWN")	clearTimeout(DivScroller.DownTimeVar);
		
		this.InternalScrollSpeed = this.ScrollSpeed;
	}

	//--------------------
	// Move Layer directly
	//--------------------
	DivScroller.prototype.scrollPixelLeft = function(pix)
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			var dLeft = parseInt(this.DivObj.style.left);
			
			if((dLeft-pix)>(this.ContainerWidth-this.ContentWidth))
				this.DivObj.style.left = dLeft - pix;
			else
				this.DivObj.style.left = this.ContainerWidth-this.ContentWidth;
		}
	}

	//--------------------
	// Move Layer directly
	//--------------------
	DivScroller.prototype.scrollPixelLeftAnime = function(pix)
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			var dLeft = parseInt(this.DivObj.style.left);
			
			if(dLeft>(0-pix))
			{
				if((dLeft-this.ScrollSpeed)>(0-pix))
					this.DivObj.style.left = dLeft - this.ScrollSpeed;
				else
					this.DivObj.style.left = 0-pix;
			}
		}
		
		if(parseInt(this.DivObj.style.left)>(0-pix))
			DivScroller.LeftTimeVar = setTimeout(this.InstanceName+".scrollPixelLeftAnime("+pix+")",50);
		else
			clearTimeout(DivScroller.LeftTimeVar);
	}

	//--------------------
	// Move Layer directly
	//--------------------
	DivScroller.prototype.setContentLeft = function(pix)
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			this.DivObj.style.left = pix;
		}
	}

	//--------------------
	// Boolean function
	//--------------------
	DivScroller.prototype.mostLeft = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			if(parseInt(this.DivObj.style.left)>=0)
				return true
		}
		else if(this.DOC_NS4)
		{
			if(parseInt(this.DivObj.left)>=0)
				return true;
		}		
		
		return false;
	}

	//--------------------
	// Boolean function
	//--------------------
	DivScroller.prototype.mostRight = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
		{
			if(parseInt(this.DivObj.style.left)<=(this.ContainerWidth-this.ContentWidth))
				return true
		}
		else if(this.DOC_NS4)
		{
			if(parseInt(this.DivObj.left)<=(this.ContainerWidth-this.ContentWidth))
				return true;
		}		
		
		return false;
	}

	//--------------------
	// Get Layer Value
	//--------------------
	DivScroller.prototype.getContentLeft = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
			return this.DivObj.style.left;
		else
			return 0;
	}

	//--------------------
	// Get Layer Value
	//--------------------
	DivScroller.prototype.getContentTop = function()
	{
		if(this.DOC_IE || this.DOC_DOM)
			return this.DivObj.style.top;
		else
			return 0;
	}
