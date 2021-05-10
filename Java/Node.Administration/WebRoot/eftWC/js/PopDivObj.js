
//*********************************************
//	the following code should be use in the page
//*********************************************

var popDivObj = new PopDiv('popDivObj');
popDivObj.DivCSS = 'wcErr';
popDivObj.ctor();

function popDivMD(e) { popDivObj.mouseDownEvent(e); };
function popDivMM(e) { popDivObj.mouseMoveEvent(e); };

if(document.attachEvent)
{
	document.attachEvent('onmousedown',popDivMD);
	document.attachEvent('onmousemove',popDivMM);
}
else if(document.addEventListener)
{
	document.addEventListener('mousedown',popDivMD,false);	
	document.addEventListener('mousemove',popDivMM,false);	
}	



