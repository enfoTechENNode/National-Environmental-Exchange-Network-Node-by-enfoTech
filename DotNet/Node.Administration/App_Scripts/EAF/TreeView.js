//***************************************************************
//																															
// The TreeView.js only support IE5 and above, and NS 7.
// Use with your own risk if run in other broswers...					
//																															
//***************************************************************


//	globle varibles
//===============================================================
TreeView.SEQ = 0;

//***************************************************************
//	Tree Node Class
//***************************************************************
function TreeNode(name, link, imgIdx1, imgIdx2)
{
	// required fields
	this.Name= name;
	this.Url= link;
	
	// optional fields
	if(typeof(imgIdx1)=='undefined')
		this.ImgCollapseIdx= -1;
	else
		this.ImgCollapseIdx= imgIdx1;

	if(typeof(imgIdx2)=='undefined') 
		this.ImgExpandIdx= this.ImgCollapseIdx;
	else
		this.ImgExpandIdx= imgIdx2;

	this.Desc= '';
	this.Expand= false;
	this.AllowCheckBox= true;
	
	// internal fields	
	this.Seq= TreeView.SEQ++;
	this.Path= -1;
	this.Parent= null;
	this.ChildNodes= new Array();
	
	this.CustomId= '';

	return this;
};


//	add chile node to a node
//===============================================================
TreeNode.prototype.addChildNode = function(child)
{
	this.ChildNodes[this.ChildNodes.length]= child;
	
	if(child.Parent!=null)
		child.Parent.removeChildNode(child);
	
	child.Parent = this;
};

//	add chile node to a node
//===============================================================
TreeNode.prototype.removeChildNode = function(child)
{
	var i=0;
	for(i=0; i<this.ChildNodes.length; i++)
		if(this.ChildNodes[i]==child)
			break;
	
	for(;i<this.ChildNodes.length-1;i++)
		this.ChildNodes[i]=this.ChildNodes[i+1];
	
	this.ChildNodes.length=this.ChildNodes.length-1;
};

//***************************************************************
//	Tree view class
//***************************************************************
function TreeView(instancename)
{
	// require fields
	this.InstanceName = instancename;
	
	this.SkinBase = "../../WCSkin";
	this.ValueSplitter = "|";
	this.ShowCheckBox = false;
	this.ShowRowLine = false;
	this.RowLineColor = '#FFFFFF';
	this.RowPadding = 0;
	this.AllowWrap= false;
	this.ImageArray= new Array();
	
	this.DefaultExpImgIdx = -1;
	this.DefaultColImgIdx = -1;
	
	// internal
	this.OnNodeSeq = -1; //current select node Seq
	this.ClickedNode= null;
	this.RootNodes = new Array();
	this.TreeType = '';
	this.TreeParentFrame = '';
	this.TreeFrame = '';
	this.TreeLinkFrame = '';
	this.TreeDivWidth = '';
	this.TreeDivHeight = '';
	this.TreeDocObj = null;
	
	this.OldLvl= 0;
	this.LvlCount= new Array();
	this.ScrollSpeed= 10;
	this.TIMER= null;

	this.ImgNodeOnly = new Array();
	this.ImgFolderExp = new Array();
	this.ImgFolderCol = new Array();
	this.ImgNodeLeft = new Array();
	
	this.ImgRootExp = new Array();
	this.ImgRootCol = new Array();
	
	this.setNodeStyle('');

	// CSS
	this.FolderCSS = "folderNodeStyle";
	this.RootCSS = "rootNodeStyle";
	this.NodeOnCSS = "onNodeStyle";
	this.TreeRowOff = "treeRowOff";
	this.TreeRowOver = "treeRowOver";
	this.TreeRowOn = "treeRowOn";
	
	return this;
};

// add root nodes
//===============================================================
TreeView.prototype.addImage = function(imgSrc)
{
	this.ImageArray.push(imgSrc);
};


// add root nodes
//===============================================================
TreeView.prototype.addRoot = function(node)
{
	this.RootNodes.push(node);
};


// Set root node of the tree
// will no draw anything if not set
//===============================================================
TreeView.prototype.setNodeStyle = function(style)
{
	if(style == 'Classic')
	{
		this.setRootImage(true,'','','old');
		this.setNodeImage(true,'','','old','dot');
	}
	else if(style == 'XP.NoLine')
	{
		this.setRootImage(false,'node_exp_xp.gif','node_col_xp.gif');
		this.setNodeImage(false,'node_exp_xp.gif','node_col_xp.gif');
	}
	else if(style == 'XP.Line')
	{
		this.setRootImage(true,'','','xp_line');
		this.setNodeImage(true,'','','xp_line','line');
	}
	else if(style == 'Square.Line')
	{
		this.setRootImage(true,'','','sqr_line');
		this.setNodeImage(true,'','','sqr_line','line');
	}
	else if(style == 'Square.Dot')
	{
		this.setRootImage(true,'','','sqr');
		this.setNodeImage(true,'','','sqr','dot');
	}
	else if(style == 'Metal')
	{
		this.setRootImage(true,'','','metal');
		this.setNodeImage(true,'','','metal','metal');
	}
	else if(style == 'Metal.NoLine')
	{
		this.setRootImage(false,'node_exp_metal.gif','node_col_metal.gif');
		this.setNodeImage(false,'node_exp_metal.gif','node_col_metal.gif');
	}
	else if(style == 'Triangle')
	{
		this.setRootImage(false,'node_exp_triangle.gif','node_col_triangle.gif');
		this.setNodeImage(false,'node_exp_triangle.gif','node_col_triangle.gif');
	}
	else 
	{
		this.setRootImage(true,'','','xp_dot');
		this.setNodeImage(true,'','','xp_dot','dot');
	};
};

//	set root expand/collapse image
//===============================================================
TreeView.prototype.setRootImage= function(hasLine,typeImg1,typeImg2,typeStr)
{
	if(hasLine)
	{
		this.ImgRootExp[0] = 'firstnode_exp_'+typeStr+'.gif';
		this.ImgRootExp[1] = 'midnode_exp_'+typeStr+'.gif';
		this.ImgRootExp[2] = 'lastnode_exp_'+typeStr+'.gif';
		this.ImgRootExp[3] = 'onlynode_exp_'+typeStr+'.gif';
		
		this.ImgRootCol[0] = 'firstnode_col_'+typeStr+'.gif';
		this.ImgRootCol[1] = 'midnode_col_'+typeStr+'.gif';
		this.ImgRootCol[2] = 'lastnode_col_'+typeStr+'.gif';
		this.ImgRootCol[3] = 'onlynode_col_'+typeStr+'.gif';
	}
	else
	{
		this.ImgRootExp[0] = typeImg1;
		this.ImgRootExp[1] = typeImg1;
		this.ImgRootExp[2] = typeImg1;
		this.ImgRootExp[3] = typeImg1;
		
		this.ImgRootCol[0] = typeImg2;
		this.ImgRootCol[1] = typeImg2;
		this.ImgRootCol[2] = typeImg2;
		this.ImgRootCol[3] = typeImg2;
	};
};

//	set folder expand/collapse image
//===============================================================
TreeView.prototype.setNodeImage= function(hasLine,typeImg1,typeImg2,typeStr,lineType)
{
	if(hasLine)
	{
		this.ImgNodeOnly[0] = 'midnode_'+lineType+'.gif';
		this.ImgNodeOnly[1] = 'lastnode_'+lineType+'.gif';
		this.ImgNodeOnly[2] = 'firstnode_'+lineType+'.gif';
		this.ImgNodeOnly[3] = 'onlynode_'+lineType+'.gif';

		this.ImgFolderExp[0] = 'midnode_exp_'+typeStr+'.gif';
		this.ImgFolderExp[1] = 'lastnode_exp_'+typeStr+'.gif';

		this.ImgFolderCol[0] = 'midnode_col_'+typeStr+'.gif';
		this.ImgFolderCol[1] = 'lastnode_col_'+typeStr+'.gif';

		this.ImgNodeLeft[0] = 'vline_'+lineType+'.gif';
		this.ImgNodeLeft[1] = 'blank.gif';
	}
	else
	{
		this.ImgNodeOnly[0] = 'blank.gif';
		this.ImgNodeOnly[1] = 'blank.gif';
		this.ImgNodeOnly[2] = 'blank.gif';
		this.ImgNodeOnly[3] = 'blank.gif';

		this.ImgFolderExp[0] = typeImg1;
		this.ImgFolderExp[1] = typeImg1;

		this.ImgFolderCol[0] = typeImg2;
		this.ImgFolderCol[1] = typeImg2;

		this.ImgNodeLeft[0] = 'blank.gif';
		this.ImgNodeLeft[1] = 'blank.gif';
	};
};

//	set root expand/collapse image
//===============================================================
TreeView.prototype.setRowLine= function(isShow,lineColor,padding)
{
	this.ShowRowLine= isShow;
	if(lineColor) this.RowLineColor= lineColor;
	if(padding) this.RowPadding= padding;
};

//	draw a tree in a frame's parent page
//===============================================================
TreeView.prototype.drawFrameTree = function(parentFrm,treeFrm,linkFrm)
{
	this.TreeParentFrame = parentFrm;
	this.TreeFrame = treeFrm;
	this.TreeLinkFrame = linkFrm;
	
	if(this.TreeFrame!='') 
		this.TreeDocObj= eval(this.TreeFrame+'.document');
	else
		this.TreeDocObj= document;
	
	var doc = eval(this.TreeFrame+'.document');
	doc.open();
	this.drawDocHeader(doc);
	this.drawTreeDiv(doc);
	this.drawDocFooter(doc);
	doc.close();
	
	this.showTree(doc);
};


//	draw a tree in a page
//===============================================================
TreeView.prototype.drawPageTree = function(isFixPanel,linkFrm,width,height)
{
	this.TreeParentFrame = '';
	this.TreeFrame = '';
	this.TreeLinkFrame = linkFrm;
	this.TreeDocObj= document;
	
	if(isFixPanel)
	{
		this.TreeDivWidth = width;
		this.TreeDivHeight = height;
	}
	else
	{
		this.TreeDivWidth = '';
		this.TreeDivHeight = '';
	};
	
	this.drawTreeDiv(document);
	this.showTree(document);

};

//	draw doc header
//===============================================================
TreeView.prototype.drawDocHeader = function(doc) 
{
	doc.writeln('<html>');
	doc.writeln('<head>');
	doc.writeln('<meta http-equiv="Content-Type" content="text/html">');
	doc.writeln('<link href="'+this.SkinBase+'css/treeview.css" rel="stylesheet" type="text/css">');
	doc.writeln('</head>');
	doc.writeln('<body>');
};

//	draw doc footer
//===============================================================
TreeView.prototype.drawDocFooter = function(doc)
{
	doc.writeln('</body>');
	doc.writeln('</html>');
};

//	draw tree DIV
//===============================================================
TreeView.prototype.drawTreeDiv = function(doc)
{
	//doc.writeln('<input type="hidden" id="clickedNode'+this.InstanceName+'" name="clickedNode'+this.InstanceName+'">');
	doc.writeln('<div id="tree'+this.InstanceName+'" style="padding:0px;');
	
	if(this.TreeDivWidth!='' && this.TreeDivHeight!='')	
		doc.writeln('width:'+this.TreeDivWidth+';height:'+this.TreeDivHeight+';overflow:auto;');
		
	doc.writeln('"></div>');
};

//	draw tree
//===============================================================
TreeView.prototype.showTree = function(doc)
{
	var treeDivObj = doc.getElementById('tree'+this.InstanceName);
	
	var leftImgStr = '';
	var lvl = 0;
	
	var str = '';
	var i;
	
	for(i=0; i<this.RootNodes.length; i++)
	{
		str += this.getTreeHtml(this.RootNodes[i],lvl,false,leftImgStr);
	};
	
	treeDivObj.innerHTML= str;
};

//	Get tree html
//  Major function of tree generation
//===============================================================
TreeView.prototype.getTreeHtml = function(node,lvl,isLastNode,leftImgStr)
{
	if(node==null) return "";
	
	
	if(typeof this.LvlCount[lvl]=='undefined')
		this.LvlCount[lvl]= 0;
	else
		this.LvlCount[lvl]++;
	
	if(this.OldLvl!=lvl)
	{
		if(this.OldLvl>lvl)
		{
			for(i=this.OldLvl; i>lvl; i--)
				this.LvlCount[i]= -1;
		}
		this.OldLvl= lvl;
	};
	
	// generate node path
	var path= '';
	var i;
	
	for(i=0; i<=lvl; i++)
		if(path=='')
			path+= ''+this.LvlCount[i];
		else
			path+= ';'+this.LvlCount[i];
	
	node.Path= path;
	
	// geberate tree
	var htmlStr='';
	
	var onclickStr= 'onclick="'+(this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+this.InstanceName+ '.toggleNode('+node.Seq+')"';
	
	htmlStr+= '<div id="row'+this.InstanceName+'_'+node.Seq+'" style="width:100%; padding:'+this.RowPadding+'px;" ';
	htmlStr+= ' class="'+((node.Seq==this.OnNodeSeq)?"treeRowOn":"treeRowOff")+'" onMouseOver="Utils.setObjClass(this,\'treeRowOver\')" onMouseOut="'+this.InstanceName+'.cancelRowOver('+node.Seq+')">';
	
	
	htmlStr+= '<table border="0" cellspacing="0" cellpadding="0">';
	htmlStr+= '<tr><td nowrap>';
	htmlStr+= leftImgStr;
	
	// generate tree view base line
	var i1;
	
	if(lvl==0)
	{
		// draw root nodes
		if(this.RootNodes.length==1)
		{
			// only one root
			i1= 3;
			leftImgStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeLeft[1]+ '" >';
		}
		else if(node==this.RootNodes[0])
		{
			// first node
			i1= 0;
			leftImgStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeLeft[0]+ '" >';
		}
		else if(node==this.RootNodes[this.RootNodes.length-1])
		{
			// last node
			i1= 2;
			leftImgStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeLeft[1]+ '" >';
		}
		else
		{
			i1= 1;
			leftImgStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeLeft[0]+ '" >';
		};
		
		if(node.ChildNodes.length==0) 
		{
			if(this.RootNodes.length==1)
				htmlStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeOnly[3]+ '" >';
			else if(node==this.RootNodes[0])
				htmlStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeOnly[2]+ '" >';
			else if(node==this.RootNodes[this.RootNodes.length-1])
				htmlStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeOnly[1]+ '" >';
			else
				htmlStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeOnly[0]+ '" >';
		}
		else if(node.Expand)	
			htmlStr+= '<img id="img'+this.InstanceName+'_'+node.Seq+'" src="'+this.SkinBase+'/images/treeView/' +this.ImgRootExp[i1]+ '"  border="0" style="cursor:hand;" '+onclickStr+'>';
		else			
			htmlStr+= '<img id="img'+this.InstanceName+'_'+node.Seq+'" src="'+this.SkinBase+'/images/treeView/' +this.ImgRootCol[i1]+ '"  border="0" style="cursor:hand;" '+onclickStr+'>';
		
		
		htmlStr+= '</td>';
		
	}
	else
	{
		if(!isLastNode)
			i1= 0;
		else
			i1= 1;

		if(node.ChildNodes.length<=0) 
		{
			// no click 
			htmlStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeOnly[i1]+ '" >';
		}
		else 
		{
			var onclickStr = 'onclick="'+(this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+ this.InstanceName+ '.toggleNode('+node.Seq+')"';
			
			//htmlStr+= '<a href="javascript:;" '+onclickStr+'>';
			
			if(node.Expand)	
				htmlStr+= '<img id="img'+this.InstanceName+'_'+node.Seq+'" src="'+this.SkinBase+'/images/treeView/' +this.ImgFolderExp[i1]+ '"  border="0" style="cursor:hand;" '+onclickStr+'>';
			else			
				htmlStr+= '<img id="img'+this.InstanceName+'_'+node.Seq+'" src="'+this.SkinBase+'/images/treeView/' +this.ImgFolderCol[i1]+ '"  border="0" style="cursor:hand;" '+onclickStr+'>';
			
			//htmlStr+= '</a>';
		};
		
		leftImgStr+= '<img src="'+this.SkinBase+'/images/treeView/' +this.ImgNodeLeft[i1]+ '" >';
		htmlStr+= '</td>';
	}
	
	// generate node part
	htmlStr+= this.getNodeHtml(node);
	
	htmlStr+= '</tr></table>';
	
	htmlStr+= '</div>';

	if(this.ShowRowLine)
	{
		htmlStr+= '<div style="background-color:'+this.RowLineColor+'; height:1px; width:100%; overflow:hidden;"></div>';
	};
	
	htmlStr+= '<div id="container'+this.InstanceName+'_'+node.Seq+'">';
	htmlStr+= '<div id="'+this.InstanceName+'_'+node.Seq+'" style="position:relative; left:0px; top:0px; visibility:visible; display:'+(node.Expand?'block':'none')+';">';
	
	// only check if the node is Folder
	if (node.ChildNodes.length>0)
	{
		lvl++;
		for(i=0; i<node.ChildNodes.length; i++)
		{
			if (i==node.ChildNodes.length-1)
			{
				htmlStr+= this.getTreeHtml(node.ChildNodes[i], lvl, true, leftImgStr);
			}
			else
			{
				htmlStr+= this.getTreeHtml(node.ChildNodes[i], lvl, false, leftImgStr);
			};
		};
	};
	
	htmlStr+= '</div>';
	htmlStr+= '</div>';
	
	return htmlStr;
};


//===============================================================
//	get tree html
//===============================================================
TreeView.prototype.getNodeHtml = function(node)
{
	var rtn = '';

	var nodeStyle;
	
	if(node.Seq==this.OnNodeSeq)
		nodeStyle = this.NodeOnCSS;
	else if(this.isRootNode(node))
		nodeStyle = this.RootCSS;
	else
		nodeStyle = this.FolderCSS;
		
	// show checkbox
	if(this.ShowCheckBox)
	{
		rtn += '<td nowrap>';
		rtn += '<input type="checkbox" name="CheckBox' +this.InstanceName+ '" value="' +node.Seq+ '"';
		if(!node.AllowCheckBox) rtn += ' disabled';
		rtn += '>';
		rtn += '</td><td nowrap>';
	};
	
	// prepare onclick string
	var onclickStr ='';

	if (node.Url!='' && node.Url.toUpperCase().indexOf('JAVASCRIPT')<0)
	{
		onclickStr = (this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+this.InstanceName+'.expandNode('+node.Seq+');';
		onclickStr += (this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+this.InstanceName+'.showUrl(\''+node.Url+'\')';
	}
	else
	{
		onclickStr = (this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+this.InstanceName+'.expandNode('+node.Seq+')';
	};
	

	// show image if set
	if(node.ImgCollapseIdx!=-1 || this.DefaultColImgIdx!=-1)
	{
		var showImg;
		
		if(node.Expand)
			if(node.ImgExpandIdx!=-1) 
				showImg= this.ImageArray[node.ImgExpandIdx];
			else
				showImg= this.ImageArray[this.DefaultExpImgIdx];
		else 
			if(node.ImgCollapseIdx!=-1) 
				showImg= this.ImageArray[node.ImgCollapseIdx];
			else
				showImg= this.ImageArray[this.DefaultColImgIdx];
		
		rtn += '<td nowrap>';
		rtn += '<img id="imgIcon'+this.InstanceName+'_'+node.Seq+'" src="'+showImg+'" alt="'+node.Name+'" border="0" align="absmiddle">';
		//rtn += '<img id="imgIcon'+this.InstanceName+'_'+node.Seq+'" src="'+showImg+'" alt="'+node.Name+'" border="0" align="absmiddle" onclick="'+onclickStr+'">';
		
		rtn += '</td>';
		rtn += '<td>&nbsp;</td>';
	};
	
	// node name link
	rtn += '<td class="'+nodeStyle+'" '+(this.AllowWrap?'':'nowrap')+'>';
	rtn += '<a href="'+(node.Url==''?'javascript:;':node.Url) +'"';
	
	if(this.TreeLinkFrame!='') 
		rtn+= ' target="'+this.TreeLinkFrame+'"';
		
	if(node.Desc!='') 
		rtn+= ' title="'+node.Desc+'"';
		
	
	rtn += ' onclick="'+(this.TreeParentFrame==''?'':this.TreeParentFrame+'.')+this.InstanceName+'.expandNode('+node.Seq+')"';	
	rtn += ' >';
	
	rtn += '<span id="span'+this.InstanceName+'_'+node.Seq+'" class="'+nodeStyle+'">' +node.Name +'</span>';
	
	rtn += '</a>';
	rtn += '<input type="hidden" id="nodeExp'+this.InstanceName+'_'+node.Seq+'" name="nodeExp'+this.InstanceName+'_'+node.Seq+'">';

	rtn += '</td>';
	
	return rtn;
};


//	open a url in tree target frame
//===============================================================
TreeView.prototype.showUrl = function(url)
{
	var str = (this.TreeLinkFrame==''?'self':this.TreeLinkFrame)+'.location="'+url+'"';
	eval(str);
};


//	expand a node only
//===============================================================
TreeView.prototype.expandNode = function(seq)
{
	var i;
	var node = this.searchTree(seq);
	
	// change node view state(expand/collapse) 
	// only it has child
	if(node.ChildNodes.length>0)
	{
		// force expand
		/*
		this.setNodeViewState(node,true);
		node.Expand = true;
		*/
		
		// will do toggle action
		node.Expand= !node.Expand;
		this.setNodeViewState(node,node.Expand);
	};
	
	// change node style
	// include row, icon and highlight style
	
	// recover old selected node style
	if(this.OnNodeSeq!=-1)
	{
		var oldnode = this.searchTree(this.OnNodeSeq);
		this.setNodeViewStyle(oldnode,false);
		this.setNodeRowStyle(oldnode,false);
		
		if(oldnode.ChildNodes.length==0) 
			this.setNodeIconStyle(oldnode,false);
	};

	// apply new node of style
	this.OnNodeSeq = seq;
	
	this.setNodeRowStyle(node,true);
	this.setNodeViewStyle(node,true);
	
	if(node.ChildNodes.length>0)
		this.setNodeIconStyle(node,node.Expand);
	else
		this.setNodeIconStyle(node,true);
		
	// set clicked node
	this.setClickedNode(node);
};

//	toggle a node
//===============================================================
TreeView.prototype.toggleNode= function(seq)
{
	// the same node might appear more than once
	// use div for uniqueness
	var node= this.searchTree(seq);
	node.Expand= !node.Expand;
	
	// change view of the page
	this.setNodeViewState(node,node.Expand);
	this.setNodeIconStyle(node,node.Expand);
};


//	only refresh node expand/collapse view status
//  didn't change the node realy expand/collapse status
//===============================================================
TreeView.prototype.setNodeViewState= function(node,isExpand)
{
	// get objects of the node
	var containerObj = this.TreeDocObj.getElementById('container'+this.InstanceName+'_'+node.Seq);
	var divObj = this.TreeDocObj.getElementById(this.InstanceName+'_'+node.Seq);
	var imgObj = this.TreeDocObj.getElementById('img'+this.InstanceName+'_'+node.Seq);
	var nodeObj = this.TreeDocObj.getElementById('nodeExp'+this.InstanceName+'_'+node.Seq);
	
	// change node view state
	if(isExpand)
	{
		divObj.style.display="block";
		nodeObj.value = "true";
		
		if(imgObj)
			if(imgObj.src.indexOf(this.ImgFolderCol[0])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgFolderExp[0];
			else if(imgObj.src.indexOf(this.ImgFolderCol[1])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgFolderExp[1];
			else if(imgObj.src.indexOf(this.ImgRootCol[0])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgRootExp[0];
			else if(imgObj.src.indexOf(this.ImgRootCol[3])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgRootExp[3];
	}
	else
	{
		divObj.style.display="none";
		nodeObj.value = "false";
		
		if(imgObj)
			if(imgObj.src.indexOf(this.ImgFolderExp[0])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgFolderCol[0];
			else if(imgObj.src.indexOf(this.ImgFolderExp[1])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgFolderCol[1];
			else if(imgObj.src.indexOf(this.ImgRootExp[0])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgRootCol[0];
			else if(imgObj.src.indexOf(this.ImgRootExp[3])>=0)
				imgObj.src = this.SkinBase+'/images/treeView/'+this.ImgRootCol[3];
	};
};

/*
//	set node icon style
//===============================================================
TreeView.prototype.loopNodes= function(exp,seq,h)
{
	var n= this.searchTree(seq);
	while(n!=null)
	{
		var obj= this.TreeDocObj.getElementById('container'+this.InstanceName+'_'+n.Seq);
		var th= parseInt(obj.clientHeight);
		
		var rslt= 0;
		if(exp)
		{
			if((th+this.ScrollSpeed)<h)
			{
				rslt= th+this.ScrollSpeed;
			}
			else
			{
				rslt= h;
				if(this.TIMER!=null) clearTimeout(this.TIMER_DivAnime);
			};
		}
		else
		{
			if((th-this.ScrollSpeed)>(th-h))
			{
				rslt= th-this.ScrollSpeed;
			}
			else
			{
				rslt= th-h;
				if(this.TIMER!=null) clearTimeout(this.TIMER_DivAnime);
			};
		};
		obj.style.height= rslt+'px';
		n= n.Parent;
	};
};
*/

//	set node icon style
//===============================================================
TreeView.prototype.setNodeRowStyle= function(node,isClicked)
{
	// get object of the node
	var	obj= this.TreeDocObj.getElementById('row'+this.InstanceName+'_'+node.Seq);	
	
	// modify node properties
	if(obj)
		if(isClicked)
			obj.className= this.TreeRowOn;
		else
			obj.className= this.TreeRowOff;
};

//	set node icon style
//===============================================================
TreeView.prototype.cancelRowOver= function(seq)
{
	// get object of the node
	var	obj= this.TreeDocObj.getElementById('row'+this.InstanceName+'_'+seq);	
	
	// modify node properties
	if(obj)
		if(this.OnNodeSeq==seq)
			obj.className= this.TreeRowOn;
		else
			obj.className= this.TreeRowOff;
};

//	set node icon style
//===============================================================
TreeView.prototype.setNodeIconStyle= function(node,isExpand)
{
	// get object of the node
	var	imgIconObj= this.TreeDocObj.getElementById('imgIcon'+this.InstanceName+'_'+node.Seq);	
	
	// modify node properties
	if(imgIconObj)
	{
		var si;
		if(isExpand)
			si= (node.ImgExpandIdx==-1) ? this.DefaultExpImgIdx:node.ImgExpandIdx;
		else
			si= (node.ImgCollapseIdx==-1) ? this.DefaultColImgIdx:node.ImgCollapseIdx;
		
		imgIconObj.src= this.ImageArray[si];
	};
};

//	set node font(text) style
//===============================================================
TreeView.prototype.setNodeViewStyle = function(node,isExpand)
{
	// get object of the node
	var spanObj = this.TreeDocObj.getElementById('span'+this.InstanceName+'_'+node.Seq);
	
	// modify node properties
	if(isExpand)
		spanObj.className = this.NodeOnCSS;
	else
		if(this.isRootNode(node))
			spanObj.className = this.RootCSS;
		else
			spanObj.className = this.FolderCSS;
};

//==============================================
//	set clicked node to a hidden field
//==============================================
TreeView.prototype.setClickedNode = function(node)
{
	this.ClickedNode= node;
	
	// get object of the node
	/*
	var cn = this.TreeDocObj.getElementById('clickedNode'+this.InstanceName);
	cn.value = node.Seq;
	*/
};

//===============================================================
//	get tree html
//===============================================================
TreeView.prototype.isRootNode = function(node)
{
	var i;
	for(i=0; i<this.RootNodes.length; i++)
	{
		if(node==this.RootNodes[i])
			return true;
	};
	
	return false;
};

//===============================================================
//	draw tree
//===============================================================
TreeView.prototype.searchTree = function(seq)
{
	var fndNode = null;
	var i;
	
	for(i=0; i<this.RootNodes.length; i++)
	{
		fndNode = this.searchNode(this.RootNodes[i],seq);
		if(fndNode!=null) return fndNode;
	};
	
	return fndNode;
};

//===============================================================
//	get tree html
//===============================================================
TreeView.prototype.searchNode = function(node, seq)
{
	if(node.Seq==seq)
	{
		return node;
	}
	else
	{
		var fndNode = null;
		var i;
		for(i=0; i<node.ChildNodes.length; i++)
		{
			fndNode = this.searchNode(node.ChildNodes[i],seq);
			if(fndNode!=null) return fndNode;
		};
		
		return fndNode;
	};
};

//===============================================================
//	get tree html
//===============================================================
TreeView.prototype.searchAllNodes = function(node, seq, nAry)
{
	var i;
	
	if(node.Seq==seq)
		nAry[nAry.length]= node;
	else
		for(i=0; i<node.ChildNodes.length; i++)
			nAry = this.searchAllNodes(node.ChildNodes[i],seq,nAry);
	
	return nAry;
};

//===============================================================
//	get tree html
//===============================================================
TreeView.prototype.replaceStr = function(inS, oldStr, newStr)
{
	if(inS=='') return '';
	
	var rtn = '';
	var i;
	
	for(i=0; i<inS.length; i++)
		if(inS.charAt(i)==oldStr)
			rtn+= newStr;
		else
			rtn+= inS.charAt(i);
	
	return rtn;
};

