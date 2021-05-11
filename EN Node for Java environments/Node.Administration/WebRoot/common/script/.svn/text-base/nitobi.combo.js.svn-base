if(typeof (nitobi)=="undefined"||typeof (nitobi.lang)=="undefined"){
alert("The Nitobi framework source could not be found. Is it included before any other Nitobi components?");
}
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.build="6257";
nitobi.combo.version="3.55.6257";
if(typeof (nitobi)=="undefined"){
nitobi={};
}
if(typeof (nitobi.Browser)=="undefined"){
nitobi.Browser={};
}
nitobi.Browser.Unload=function(){
};
nitobi.Browser.GetScrollBarWidth=nitobi.html.getScrollBarWidth;
nitobi.Browser.GetBrowserType=function(){
try{
return (navigator.appName=="Microsoft Internet Explorer"?this.nitobi.Browser.IE:this.nitobi.Browser.UNKNOWN);
}
catch(err){
}
};
nitobi.Browser.GetBrowserDetails=function(){
try{
return (this.GetBrowserType()==this.nitobi.Browser.IE?window.clientInformation:null);
}
catch(err){
}
};
nitobi.Browser.IsObjectInView=function(_1,_2,_3,_4){
try{
var _5=_1.getBoundingClientRect();
var _6=_2.getBoundingClientRect();
if(nitobi.browser.MOZ){
_6.top+=_2.scrollTop;
_6.bottom+=_2.scrollTop;
_6.left+=_2.scrollLeft;
_6.right+=_2.scrollLeft;
}
var _7=((true==_3?(_5.top==_6.top):(_5.top>=_6.top)&&(_5.bottom<=_6.bottom))&&(_4?true:(_5.right<=_6.right)&&(_5.left>=_6.left)));
return _7;
}
catch(err){
}
};
nitobi.Browser.VAdjust=function(_8,_9){
var v=(_8.offsetParent?_8.offsetParent.offsetTop:0);
var id=_8.id;
var _c=id.substring(0,1+id.lastIndexOf("_"))+"0";
var _d=_9.ownerDocument;
if(null==_d){
_d=_9.document;
}
var oF=_d.getElementById(_c);
return v-(oF.offsetParent?oF.offsetParent.offsetTop:0);
};
nitobi.Browser.WheelUntil=function(_f,inc,_11,idx,_13,_14){
var min=(inc?-1:0);
var max=(inc?_13:_13+1);
while(idx>min&&idx<max){
if(inc){
idx++;
}else{
idx--;
}
var r=_11.GetRow(idx);
var _18=this.IsObjectInView(r,_14,false,true);
if(_18==_f){
return idx;
}
}
return idx;
};
nitobi.Browser.WheelUp=function(_19){
var top=_19.GetRow(0);
var _1b=_19.GetXmlDataSource().GetNumberRows()-1;
var _1c=_19.GetRow(_1b);
var _1d=_19.GetSectionHTMLTagObject(EBAComboBoxListBody);
var i=parseInt(_1d.scrollTop/top.offsetHeight);
var r=(i>_1b?_1c:_19.GetRow(i));
var _20=r.offsetTop-_1d.scrollTop+nitobi.Browser.VAdjust(r,_1d);
if(this.IsObjectInView(r,_1d,false,true)){
i=this.WheelUntil(false,false,_19,i,_1b,_1d);
}else{
if(_20<0){
i=this.WheelUntil(true,true,_19,i,_1b,_1d);
i--;
}else{
i=this.WheelUntil(true,false,_19,i,_1b,_1d);
i=this.WheelUntil(false,false,_19,i,_1b,_1d);
}
}
this.ScrollIntoView(_19.GetRow(i),_1d,true,false);
};
nitobi.Browser.WheelDown=function(_21){
var top=_21.GetRow(0);
var _23=_21.GetXmlDataSource().GetNumberRows()-1;
var _24=_21.GetRow(_23);
var _25=_21.GetSectionHTMLTagObject(EBAComboBoxListBody);
var i=parseInt(_25.scrollTop/top.offsetHeight);
var r=(i>_23?_24:_21.GetRow(i));
var _28=r.offsetTop-_25.scrollTop+nitobi.Browser.VAdjust(r,_25);
if(this.IsObjectInView(r,_25,false,true)){
i=1+this.WheelUntil(false,false,_21,i,_23,_25);
}else{
if(_28<0){
i=this.WheelUntil(true,true,_21,i,_23,_25);
}else{
i=this.WheelUntil(true,false,_21,i,_23,_25);
i=1+this.WheelUntil(false,false,_21,i,_23,_25);
}
}
r=_21.GetRow(i);
_28=r.offsetTop-_25.scrollTop+nitobi.Browser.VAdjust(r,_25);
if(0==_28&&i!=_23){
r=_21.GetRow(1+i);
}
this.ScrollIntoView(r,_25,true,false);
};
nitobi.Browser.ScrollIntoView=function(_29,_2a,Top,_2c){
try{
var _2d=_29.getBoundingClientRect();
var _2e=_2a.getBoundingClientRect();
var _2f=_29.offsetTop-_2a.scrollTop;
var v=nitobi.Browser.VAdjust(_29,_2a);
_2f+=v;
var _31=_29.offsetLeft-_2a.scrollLeft;
var _32=_31+_29.offsetWidth-_2a.offsetWidth;
var _33=_2f+_29.offsetHeight-_2a.offsetHeight;
var _34=0;
var _35=0;
var _36=this.GetScrollBarWidth(_2a);
if(this.GetVerticalScrollBarStatus(_2a)==true){
_34=_36;
}
if(_31<0){
_2a.scrollLeft+=_31;
}else{
if(_32>0){
if(_2d.left-_32>_2e.left){
_2a.scrollLeft+=_32+_34;
}else{
_2a.scrollLeft+=_31;
}
}
}
if((_2f<0||true==Top)&&true!=_2c){
_2a.scrollTop+=_2f;
}else{
if(_33>0||true==_2c){
if(_2d.top-_33>_2e.top||true==_2c){
_2a.scrollTop+=_33+_35;
}else{
_2a.scrollTop+=_2f;
}
}
}
}
catch(err){
}
};
nitobi.Browser.GetVerticalScrollBarStatus=function(_37){
try{
return this.GetScrollBarWidth(_37)>0;
}
catch(err){
}
};
nitobi.Browser.GetHorizontalScrollBarStatus=function(_38){
try{
return (_38.scrollWidth>_38.offsetWidth-this.GetScrollBarWidth(_38));
}
catch(err){
}
};
nitobi.Browser.HTMLUnencode=function(_39){
try{
var _3a=_39;
var _3b=new Array(/&amp;/g,/&lt;/g,/&quot;/g,/&gt;/g,/&nbsp;/g);
var _3c=new Array("&","<","\"",">"," ");
for(var i=0;i<_3b.length;i++){
_3a=_3a.replace(_3b[i],_3c[i]);
}
return (_3a);
}
catch(err){
}
};
nitobi.Browser.EncodeAngleBracketsInTagAttributes=function(str,_3f){
str=str.replace(/'"'/g,"\"&quot;\"");
try{
_3f.ShowWarning(str.match(/ComboValue .*?[a-z]='/g)==null,"cw002");
}
catch(err){
}
var _40=str.match(/".*?"/g);
if(_40){
for(var i=0;i<_40.length;i++){
val=_40[i];
val=val.replace(/</g,"&lt;");
val=val.replace(/>/g,"&gt;");
str=str.replace(_40[i],val);
}
}
return str;
};
nitobi.Browser.LoadPageFromUrl=function(Url,_43){
if(_43==null){
_43="GET";
}
var _44=xbXMLHTTP.create();
_44.abort();
_44.open(_43,Url,false,"","");
_44.send("EBA Combo Box Get Page Request");
return (_44.responseText);
};
nitobi.Browser.GetMeasurementUnitType=function(_45){
try{
if(_45==null||_45==""){
return "";
}
var _46=_45.search(/\D/g);
var _47=_45.substring(_46);
return (_47);
}
catch(err){
}
};
nitobi.Browser.GetMeasurementUnitValue=function(_48){
try{
var _49=_48.search(/\D/g);
var _4a=_48.substring(0,_49);
return Number(_4a);
}
catch(err){
}
};
nitobi.Browser.GetElementWidth=function(_4b){
try{
if(_4b==null){
throw ("Element in GetElementWidth is null");
}
var _4c=_4b.style;
var top=_4c.top;
var _4e=_4c.display;
var _4f=_4c.position;
var _50=_4c.visibility;
var _51=nitobi.html.Css.getStyle(_4b,"visibility");
var _52=nitobi.html.Css.getStyle(_4b,"display");
var _53=0;
if(_52=="none"||_51=="hidden"){
_4c.position="absolute";
_4c.top=-1000;
_4c.display="inline";
_4c.visibility="visible";
}
var _54=nitobi.html.getWidth(_4b);
if(_4c.display=="inline"){
_4c.position=_4f;
_4c.top=top;
_4c.display=_4e;
_4c.visibility=_50;
}
return parseInt(_54);
}
catch(err){
}
};
nitobi.Browser.GetElementHeight=function(_55){
try{
if(_55==null){
throw ("Element in GetElementHeight is null");
}
var _56=_55.style;
var top=_56.top;
var _58=_56.display;
var _59=_56.position;
var _5a=_56.visibility;
if(_56.display=="none"||_56.visibility!="visible"){
_56.position="absolute";
_56.top="-1000px";
_56.display="inline";
_56.visibility="visible";
}
var _5b=nitobi.html.getHeight(_55);
if(_56.display=="inline"){
_56.position=_59;
_56.top=top;
_56.display=_58;
_56.visibility=_5a;
}
return parseInt(_5b);
}
catch(err){
}
};
nitobi.Browser.GetParentElementByTagName=function(_5c,_5d){
try{
_5d=_5d.toLowerCase();
var _5e;
do{
_5c=_5c.parentElement;
if(_5c!=null){
_5e=_5c.tagName.toLowerCase();
}
}while((_5e!=_5d)&&(_5c!=null));
return _5c;
}
catch(err){
return null;
}
};
nitobi.lang.defineNs("nitobi.drawing");
nitobi.drawing.rgb=function(r,g,b){
return "#"+((r*65536)+(g*256)+b).toString(16);
};
nitobi.drawing.align=function(_62,_63,_64,oh,ow,oy,ox,_69){
oh=oh||0;
ow=ow||0;
oy=oy||0;
ox=ox||0;
var a=_64;
var td,sd,tt,tb,tl,tr,th,tw,st,sb,sl,sr,sh,sw;
if(nitobi.browser.IE){
td=_63.getBoundingClientRect();
sd=_62.getBoundingClientRect();
tt=td.top;
tb=td.bottom;
tl=td.left;
tr=td.right;
th=Math.abs(tb-tt);
tw=Math.abs(tr-tl);
st=sd.top;
sb=sd.bottom;
sl=sd.left;
sr=sd.right;
sh=Math.abs(sb-st);
sw=Math.abs(sr-sl);
}
if(nitobi.browser.MOZ){
td=document.getBoxObjectFor(_63);
sd=document.getBoxObjectFor(_62);
tt=td.y;
tl=td.x;
tw=td.width;
th=td.height;
st=sd.y;
sl=sd.x;
sw=sd.width;
sh=sd.height;
}
if(a&268435456){
_62.style.height=th+oh;
}
if(a&16777216){
_62.style.width=tw+ow;
}
if(a&1048576){
_62.style.top=nitobi.html.getStyleTop(_62)+tt-st+oy;
}
if(a&65536){
_62.style.top=nitobi.html.getStyleTop(_62)+tt-st+th-sh+oy;
}
if(a&4096){
_62.style.left=nitobi.html.getStyleLeft(_62)-sl+tl+ox;
}
if(a&256){
_62.style.left=nitobi.html.getStyleLeft(_62)-sl+tl+tw-sw+ox;
}
if(a&16){
_62.style.top=nitobi.html.getStyleTop(_62)+tt-st+oy+Math.floor((th-sh)/2);
}
if(a&1){
_62.style.left=nitobi.html.getStyleLeft(_62)-sl+tl+ox+Math.floor((tw-sw)/2);
}
if(_69){
src.style.top=st-2;
src.style.left=sl-2;
src.style.height=sh;
src.style.width=sw;
tgt.style.top=tt-2;
tgt.style.left=tl-2;
tgt.style.height=th;
tgt.style.width=tw;
if(document.getBoundingClientRect){
sd=_62.getBoundingClientRect();
st=sd.top;
sb=sd.bottom;
sl=sd.left;
sr=sd.right;
sh=Math.abs(sb-st);
sw=Math.abs(sr-sl);
}
if(document.getBoxObjectFor){
sd=document.getBoxObjectFor(_62);
st=sd.screenY;
sl=sd.screenX;
sw=sd.width;
sh=sd.height;
}
src2.style.top=st-2;
src2.style.left=sl-2;
src2.style.height=sh;
src2.style.width=sw;
}
};
nitobi.drawing.align.SAMEHEIGHT=268435456;
nitobi.drawing.align.SAMEWIDTH=16777216;
nitobi.drawing.align.ALIGNTOP=1048576;
nitobi.drawing.align.ALIGNBOTTOM=65536;
nitobi.drawing.align.ALIGNLEFT=4096;
nitobi.drawing.align.ALIGNRIGHT=256;
nitobi.drawing.align.ALIGNMIDDLEVERT=16;
nitobi.drawing.align.ALIGNMIDDLEHORIZ=1;
nitobi.drawing.alignOuterBox=function(_79,_7a,_7b,oh,ow,oy,ox,_80){
oh=oh||0;
ow=ow||0;
oy=oy||0;
ox=ox||0;
if(nitobi.browser.MOZ){
td=document.getBoxObjectFor(_7a);
sd=document.getBoxObjectFor(_79);
var _81=parseInt(document.defaultView.getComputedStyle(_7a,"").getPropertyValue("border-left-width"));
var _82=parseInt(document.defaultView.getComputedStyle(_7a,"").getPropertyValue("border-top-width"));
var _83=parseInt(document.defaultView.getComputedStyle(_79,"").getPropertyValue("border-top-width"));
var _84=parseInt(document.defaultView.getComputedStyle(_79,"").getPropertyValue("border-bottom-width"));
var _85=parseInt(document.defaultView.getComputedStyle(_79,"").getPropertyValue("border-left-width"));
var _86=parseInt(document.defaultView.getComputedStyle(_79,"").getPropertyValue("border-right-width"));
oy=oy+_83-_82;
ox=ox+_85-_81;
}
nitobi.drawing.align(_79,_7a,_7b,oh,ow,oy,ox,_80);
};
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.Button=function(_87,_88){
try{
var _89="ComboBoxButton";
var _8a="ComboBoxButtonPressed";
var _8b="";
var _8c="";
this.SetCombo(_88);
var _8d=(_87?_87.getAttribute("Width"):null);
((null==_8d)||(_8d==""))?this.SetWidth(_8b):this.SetWidth(_8d);
var _8e=(_87?_87.getAttribute("Height"):null);
((null==_8e)||(_8e==""))?this.SetHeight(_8c):this.SetHeight(_8e);
var _8f=(_87?_87.getAttribute("DefaultCSSClassName"):null);
((null==_8f)||(_8f==""))?this.SetDefaultCSSClassName(_89):this.SetDefaultCSSClassName(_8f);
var _90=(_87?_87.getAttribute("PressedCSSClassName"):null);
((null==_90)||(_90==""))?this.SetPressedCSSClassName(_8a):this.SetPressedCSSClassName(_90);
this.SetCSSClassName(this.GetDefaultCSSClassName());
this.m_userTag=_87;
this.m_prevImgClass="ComboBoxButtonImg";
}
catch(err){
}
};
nitobi.combo.Button.prototype.Unload=Button_Unload;
function Button_Unload(){
}
nitobi.combo.Button.prototype.GetDefaultCSSClassName=Button_GetDefaultCSSClassName;
function Button_GetDefaultCSSClassName(){
try{
return this.m_DefaultCSSClassName;
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetDefaultCSSClassName=Button_SetDefaultCSSClassName;
function Button_SetDefaultCSSClassName(_91){
try{
this.m_DefaultCSSClassName=_91;
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetPressedCSSClassName=Button_GetPressedCSSClassName;
function Button_GetPressedCSSClassName(){
try{
return this.m_PressedCSSClassName;
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetPressedCSSClassName=Button_SetPressedCSSClassName;
function Button_SetPressedCSSClassName(_92){
try{
this.m_PressedCSSClassName=_92;
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetHeight=Button_GetHeight;
function Button_GetHeight(){
try{
return (null==this.m_HTMLTagObject?this.m_Height:this.m_HTMLTagObject.style.height);
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetHeight=Button_SetHeight;
function Button_SetHeight(_93){
try{
if(null==this.m_HTMLTagObject){
this.m_Height=_93;
}else{
this.m_HTMLTagObject.style.height=_93;
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetWidth=Button_GetWidth;
function Button_GetWidth(){
try{
if(null==this.m_HTMLTagObject){
return this.m_Width;
}else{
return this.m_HTMLTagObject.style.width;
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetWidth=Button_SetWidth;
function Button_SetWidth(_94){
try{
if(null==this.m_HTMLTagObject){
this.m_Width=_94;
}else{
this.m_HTMLTagObject.style.width=_94;
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetHTMLTagObject=Button_GetHTMLTagObject;
function Button_GetHTMLTagObject(){
try{
return this.m_HTMLTagObject;
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetHTMLTagObject=Button_SetHTMLTagObject;
function Button_SetHTMLTagObject(_95){
try{
this.m_HTMLTagObject=_95;
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetCombo=Button_GetCombo;
function Button_GetCombo(){
try{
return this.m_Combo;
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetCombo=Button_SetCombo;
function Button_SetCombo(_96){
try{
this.m_Combo=_96;
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetCSSClassName=Button_GetCSSClassName;
function Button_GetCSSClassName(){
try{
return (null==this.m_HTMLTagObject?this.m_CSSClassName:this.m_HTMLTagObject.className);
}
catch(err){
}
}
nitobi.combo.Button.prototype.SetCSSClassName=Button_SetCSSClassName;
function Button_SetCSSClassName(_97){
try{
if(null==this.m_HTMLTagObject){
this.m_CSSClassName=_97;
}else{
this.m_HTMLTagObject.className=_97;
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.OnMouseOver=Button_OnMouseOver;
function Button_OnMouseOver(_98,_99){
try{
if(this.GetCombo().GetEnabled()){
if(null==_98){
_98=this.m_Img;
}
this.m_prevImgClass="ComboBoxButtonImgOver";
_98.className=this.m_prevImgClass;
if(_99){
this.GetCombo().GetTextBox().OnMouseOver(false);
}
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.OnMouseOut=Button_OnMouseOut;
function Button_OnMouseOut(_9a,_9b){
try{
if(null==_9a){
_9a=this.m_Img;
}
this.m_prevImgClass="ComboBoxButtonImg";
_9a.className=this.m_prevImgClass;
if(_9b){
this.GetCombo().GetTextBox().OnMouseOut(false);
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.OnMouseDown=Button_OnMouseDown;
function Button_OnMouseDown(_9c){
try{
if(this.GetCombo().GetEnabled()){
if(null!=_9c){
_9c.className="ComboBoxButtonImgPressed";
}
this.OnClick();
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.OnMouseUp=Button_OnMouseUp;
function Button_OnMouseUp(_9d){
try{
if(this.GetCombo().GetEnabled()){
if(null!=_9d){
_9d.className=this.m_prevImgClass;
}
}
}
catch(err){
}
}
nitobi.combo.Button.prototype.OnClick=Button_OnClick;
function Button_OnClick(){
try{
var _9e=this.GetCombo();
var _9f=window.document.getElementsByTagName(nitobi.browser.MOZ?"ntb:Combo":"combo");
for(var i=0;i<_9f.length;i++){
var _a1=_9f[i].object;
try{
if(_9e.GetId()!=_a1.GetId()){
_a1.GetList().Hide();
}
}
catch(err){
}
}
var l=_9e.GetList();
l.Toggle();
var t=_9e.GetTextBox();
var tb=t.GetHTMLTagObject();
if(t.focused){
t.m_skipFocusOnce=true;
}
tb.focus();
}
catch(err){
}
}
nitobi.combo.Button.prototype.GetHTMLRenderString=Button_GetHTMLRenderString;
function Button_GetHTMLRenderString(){
try{
var _a5=this.GetCombo().GetId();
var uid=this.GetCombo().GetUniqueId();
var w=this.GetWidth();
var h=this.GetHeight();
if(nitobi.browser.MOZ){
var _a9="<span id='EBAComboBoxButton"+uid+"' "+"class='"+this.GetDefaultCSSClassName()+"' "+"style='"+(null!=w&&""!=w?"width:"+w+";":"")+(null!=h&&""!=h?"height:"+h+";":"")+"'>"+"<img src='javascript:void(0);' class='ComboBoxButtonImg' id='EBAComboBoxButtonImg"+uid+"' "+"onmouseover='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseOver(this, true)' "+"onmouseout='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseOut(this, true)' "+"onmousedown='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseDown(this);return false;' "+"onmouseup='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseUp(this)' "+"onmousemove='return false;' "+"></img></span>";
}else{
var _a9="<span id='EBAComboBoxButton"+uid+"' "+"class='"+this.GetDefaultCSSClassName()+"' "+"style='"+(null!=w&&""!=w?"width:"+w+";":"")+(null!=h&&""!=h?"height:"+h+";":"")+"'>"+"<img class='ComboBoxButtonImg' id='EBAComboBoxButtonImg"+uid+"' "+"onmouseover='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseOver(this, true)' "+"onmouseout='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseOut(this, true)' "+"onmousedown='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseDown(this);return false;' "+"onmouseup='window.document.getElementById(\""+_a5+"\").object.GetButton().OnMouseUp(this)' "+"onmousemove='return false;' "+"></img></span>";
}
return _a9;
}
catch(err){
}
}
nitobi.combo.Button.prototype.Initialize=Button_Initialize;
function Button_Initialize(){
try{
var _aa=this.GetCombo();
var uid=_aa.GetUniqueId();
this.SetHTMLTagObject(window.document.getElementById("EBAComboBoxButton"+uid));
var img=window.document.getElementById("EBAComboBoxButtonImg"+uid);
var _ad=nitobi.html.Css.getStyle(img,"background-image");
_ad=_ad.replace(/button\.gif/g,"blank.gif");
if(nitobi.browser.IE){
_ad=_ad.substr(5,_ad.length-7);
}else{
_ad=_ad.substr(4,_ad.length-5);
_ad=_ad.replace(/\\\(/g,"(");
_ad=_ad.replace(/\\\)/g,")");
}
img.src=_ad;
this.m_Img=img;
this._onmouseover=img.onmouseover;
this._onmouseout=img.onmouseout;
this._onclick=img.onclick;
this._onmousedown=img.onmousedown;
this._onmouseup=img.onmouseup;
if(!this.GetCombo().GetEnabled()){
this.Disable();
}
this.m_userTag=null;
}
catch(err){
}
}
nitobi.combo.Button.prototype.Disable=Button_Disable;
function Button_Disable(){
var img=this.m_Img;
img.onmouseover=null;
img.onmouseout=null;
img.onclick=null;
img.onmousedown=null;
img.onmouseup=null;
img.className="ComboBoxButtonImgDisabled";
}
nitobi.combo.Button.prototype.Enable=Button_Enable;
function Button_Enable(){
var img=this.m_Img;
img.onmouseover=this._onmouseover;
img.onmouseout=this._onmouseout;
img.onclick=this._onclick;
img.onmousedown=this._onmousedown;
img.onmouseup=this._onmouseup;
img.className="ComboBoxButtonImg";
}
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.numCombosToLoad=0;
nitobi.combo.numCombosToLoadInitially=4;
nitobi.combo.loadDelayMultiplier=10;
nitobi.getCombo=function(id){
return document.getElementById(id).jsObject;
};
nitobi.combo.initBase=function(){
if(nitobi.combo.initBase.done==false){
Debug=new Debug;
var _b1=[];
var _b2=window.document.getElementsByTagName(nitobi.browser.MOZ?"eba:ComboPanel":"combopanel");
var _b3=(nitobi.browser.MOZ?window.document.getElementsByTagName("ntb:ComboPanel"):[]);
for(var i=0;i<_b3.length;i++){
_b1.push(_b3[i]);
}
for(var i=0;i<_b2.length;i++){
_b1.push(_b2[i]);
}
for(var i=0;i<_b1.length;i++){
_b1[i].style.display="none";
}
g_EbaTimer=null;
nitobi.combo.createLanguagePack();
try{
g_EbaTimer=new Timer();
}
catch(err){
g_EbaTimer=null;
}
if(nitobi.browser.IE){
nitobi.combo.iframeBacker=window.document.createElement("IFRAME");
nitobi.combo.iframeBacker.style.position="absolute";
nitobi.combo.iframeBacker.style.zindex="1000";
nitobi.combo.iframeBacker.style.visibility="hidden";
nitobi.combo.iframeBacker.name="nitobi.combo.iframeBacker_Id";
nitobi.combo.iframeBacker.id="nitobi.combo.iframeBacker_Id";
nitobi.combo.iframeBacker.frameBorder=0;
nitobi.combo.iframeBacker.src="javascript:true";
window.document.body.insertAdjacentElement("afterBegin",nitobi.combo.iframeBacker);
}
nitobi.combo.initBase.done=true;
}
};
nitobi.combo.initBase.done=false;
nitobi.initCombo=function(el){
nitobi.combo.initBase();
var tag;
if(typeof (el)=="string"){
tag=document.getElementById(el);
}else{
tag=el;
}
tag.object=new nitobi.combo.Combo(tag);
tag.object.Initialize();
tag.object.GetList().Render();
return tag.object;
};
nitobi.initCombos=function(){
nitobi.combo.initBase();
var _b7=[];
var _b8=window.document.getElementsByTagName(nitobi.browser.MOZ?"eba:Combo":"combo");
var _b9=(nitobi.browser.MOZ?window.document.getElementsByTagName("ntb:Combo"):[]);
for(var i=0;i<_b9.length;i++){
_b7.push(_b9[i]);
}
for(var i=0;i<_b8.length;i++){
_b7.push(_b8[i]);
}
if(0==window.document.styleSheets.length){
alert("You are missing a link to the Web ComboBoxes' style sheet.");
}else{
nitobi.combo.numCombosToLoad=_b7.length;
for(var i=0;i<_b7.length;i++){
try{
if(i>=nitobi.combo.numCombosToLoadInitially){
var _bb=i*nitobi.combo.loadDelayMultiplier;
window.setTimeout("try{document.getElementById('"+_b7[i].id+"').object = new nitobi.combo.Combo(document.getElementById('"+_b7[i].id+"'));document.getElementById('"+_b7[i].id+"').object.Initialize();}catch(err){alert(err.message);}",_bb);
}else{
nitobi.initCombo(_b7[i]);
}
}
catch(err){
alert(err.message);
}
}
}
};
function InitializeEbaCombos(){
nitobi.initCombos();
}
nitobi.combo.finishInit=function(){
nitobi.combo.resize();
nitobi.html.attachEvent(window,"resize",nitobi.combo.resize);
if(window.addEventListener){
window.addEventListener("unload",nitobi.combo.unloadAll,false);
}else{
if(document.addEventListener){
document.addEventListener("unload",nitobi.combo.unloadAll,false);
}else{
if(window.attachEvent){
window.attachEvent("onunload",nitobi.combo.unloadAll);
}else{
if(window.onunload){
window.XTRonunload=window.onunload;
}
window.onunload=nitobi.combo.unloadAll;
}
}
}
try{
eval("try{OnAfterIntializeEbaCombos()} catch(err){}");
}
catch(err){
}
};
nitobi.combo.unloadAll=function(){
try{
var _bc=[];
var _bd=window.document.getElementsByTagName(nitobi.browser.MOZ?"eba:Combo":"combo");
var _be=(nitobi.browser.MOZ?window.document.getElementsByTagName("ntb:Combo"):[]);
for(var i=0;i<_be.length;i++){
_bc.push(_be[i]);
}
for(var i=0;i<_bd.length;i++){
_bc.push(_bd[i]);
}
if(_bc){
for(var i=0;i<_bc.length;i++){
if((_bc[i])&&(_bc[i].object)){
_bc[i].object.Unload();
_bc[i].object=null;
}
}
_bc=null;
}
if(nitobi.browser.IE){
if(nitobi.combo.iframeBacker){
delete nitobi.combo.iframeBacker;
nitobi.combo.iframeBacker=null;
}
}
}
catch(e){
}
};
nitobi.combo.resize=function(){
var _c0=[];
var _c1=window.document.getElementsByTagName(nitobi.browser.MOZ?"eba:Combo":"combo");
var _c2=(nitobi.browser.MOZ?window.document.getElementsByTagName("ntb:Combo"):[]);
for(var i=0;i<_c2.length;i++){
_c0.push(_c2[i]);
}
for(var i=0;i<_c1.length;i++){
_c0.push(_c1[i]);
}
for(var i=0;i<_c0.length;i++){
var _c4=_c0[i].object;
if("smartlist"!=_c4.mode){
if(_c4.GetWidth()!=null){
var _c5=_c4.GetUniqueId();
var _c6=_c4.GetTextBox();
var _c7=_c4.GetList();
var _c8=document.getElementById(_c4.GetId());
var _c9=parseInt(_c4.GetWidth());
if(nitobi.browser.MOZ&&nitobi.Browser.GetMeasurementUnitType(_c4.GetWidth())=="px"){
_c9=parseInt(_c4.GetWidth());
}
var _ca=document.getElementById("EBAComboBoxButtonImg"+_c5);
var _cb;
if(null!=_ca){
_cb=nitobi.html.getWidth(_ca);
}else{
_cb=0;
}
_c6.SetWidth((_c9-_cb)+"px");
_c7.OnWindowResized();
}
}
}
};
if(false){
nitobi.combo=function(){
};
}
nitobi.combo.Combo=function(_cc){
try{
nitobi.prepare();
var _cd="";
var _ce="GET";
this.Timer=null;
try{
this.Timer=new Timer();
}
catch(err){
this.Timer=null;
}
var _cf="You must specify an Id for the combo box";
var _d0="ntb:Combo could not correctly transform XML data. Do you have the MS XML libraries installed? These are typically installed with your browser and are freely available from Microsoft.";
this.Version="3.5";
((null==_cc.id)||(""==_cc.id))?alert(_cf):this.SetId(_cc.id);
var _d1=null;
var _d2=null;
var _d3=null;
var _d4=null;
_cc.object=this;
_cc.jsObject=this;
this.m_userTag=_cc;
var _d5=null;
this.BuildWarningList();
var _d6=this.m_userTag.getAttribute("DisabledWarningMessages");
if(!((null==_d6)||(""==_d6))){
this.SetDisabledWarningMessages(_d6);
}
var _d7=this.m_userTag.getAttribute("ErrorLevel");
((null==_d7)||(""==_d7))?this.SetErrorLevel(""):this.SetErrorLevel(_d7);
_cc.innerHTML=_cc.innerHTML.replace(/>\s+</g,"><").replace(/^\s+</,"<").replace(/>\s+$/,">");
var dtf=_cc.getAttribute("DataTextField");
var dvf=_cc.getAttribute("DataValueField");
if((null==dtf)||(""==dtf)){
dtf=dvf;
_cc.setAttribute("DataTextField",dtf);
}
this.SetDataTextField(dtf);
this.SetDataValueField(dvf);
if((null!=dtf)&&(""!=dtf)){
if((null==dvf)||(""==dvf)){
dvf=dtf;
}
this.SetDataValueField(dvf);
}
for(var i=0;i<_cc.childNodes.length;i++){
var _db=_cc.childNodes[i];
var n=_db.tagName;
if(n){
n=n.toLowerCase().replace(/^eba:/,"").replace(/^ntb:/,"");
switch(n){
case "combobutton":
_d3=_db;
break;
case "combotextbox":
_d4=_db;
break;
case "combolist":
_d2=_db;
break;
case "xmldatasource":
_d1=_db;
break;
case "combovalues":
_d5=_db;
}
}
}
var _dd="default";
var _de=this.m_userTag.getAttribute("Mode");
if(null!=_de){
_de=_de.toLowerCase();
}
switch(_de){
case "smartsearch":
case "smartlist":
case "compact":
case "filter":
case "unbound":
this.mode=_de;
break;
default:
this.mode=_dd;
}
var _df=(_d2==null?null:_d2.getAttribute("DatasourceUrl"));
if((_d5==null&&_df==null)&&this.mode!="compact"){
this.mode=_dd;
}
var _e0=25;
if(null!=_d2){
var ps=_d2.getAttribute("PageSize");
if(ps!=null&&ps!=""){
_e0=ps;
}
}
var _e2=_cc.getAttribute("InitialSearch");
this.m_InitialSearch="";
if((null==_e2)||(""==_e2)){
this.m_InitialSearch=_cd;
}else{
this.m_InitialSearch=_e2;
}
var rt=_cc.getAttribute("HttpRequestMethod");
((null==rt)||(""==rt))?this.SetHttpRequestMethod(_ce):this.SetHttpRequestMethod(rt);
this.m_NoDataIsland=_d5==null&&_df!=null&&_d1==null;
if(this.m_NoDataIsland){
var id=_cc.id+"XmlDataSource";
_d2.setAttribute("XmlId",id);
_d1=_d2;
var d=xbDOM.create();
d.async=false;
_df+=(_df.indexOf("?")==-1?"?":"&");
_df+="PageSize="+_e0;
_df+="&StartingRecordIndex=0"+"&ComboId="+encodeURI(this.GetId())+"&LastString=";
if(this.m_InitialSearch!=null&&this.m_InitialSearch!=""){
_df+="&SearchSubstring="+encodeURI(this.m_InitialSearch);
}
var _e6=nitobi.Browser.LoadPageFromUrl(_df,this.GetHttpRequestMethod());
var _e7=_e6.indexOf("<?xml");
if(_e7!=-1){
d.loadXML(_e6.substr(_e7));
}else{
d.loadXML(_e6);
}
try{
this.ShowWarning(d.xml!=""&&d.parseError==0,"cw003",d.parseError.reason);
}
catch(err){
this.ShowWarning(d.xml!="","cw003");
}
var d2=xbDOM.create();
d2.loadXML(d.xml.replace(/>\s+</g,"><"));
d2=xbClipXml(d2,"root","e",_e0);
eval("window.document."+id+"=d2;");
}
var _e9=(this.mode==_dd||this.mode=="unbound");
if(_e9){
this.SetButton(new nitobi.combo.Button(_d3,this));
}
this.SetList(new nitobi.combo.List(_d2,_d1,_d5,this));
this.SetTextBox(new nitobi.combo.TextBox(_d4,this,_e9));
this.m_Over=false;
}
catch(err){
}
};
nitobi.combo.Combo.prototype.BuildWarningList=Combo_BuildWarningList;
function Combo_BuildWarningList(){
this.m_WarningMessagesEnabled=new Array();
this.m_DisableAllWarnings=false;
this.m_WarningMessages=new Array();
this.m_WarningMessages["cw001"]="The combo tried to search the server datasource for data.  "+"The server returned data, but no match was found within this data by the combo. The most "+"likely cause for this warning is that the combo mode does not match the gethandler SQL query type: "+"the sql query is not matching in the same way the combo is. Consult the documentation to see what "+"matches to use given the combo's mode.";
this.m_WarningMessages["cw002"]="The combo tried to load XML data from the page. However, it encountered a tag attribute of the form <tag att='___'/> instead"+" of the form <tag att=\"___\"/>. A possible reason for this is encoding ' as &apos;. To fix this error correct the tag to use "+"<tag att=\"__'___\"/>. If you are manually encoding data, eg. for an unbound combo, do not encode ' as &apos; and do not use ' as your string literal. If you believe, "+"this warning was generated in error, you can disable it.";
this.m_WarningMessages["cw003"]="The combo failed to load and parse the XML sent by the gethandler. Check your gethandler to ensure that it is delivering valid XML.";
}
nitobi.combo.Combo.prototype.SetDisabledWarningMessages=Combo_SetDisabledWarningMessages;
function Combo_SetDisabledWarningMessages(_ea){
if(_ea=="*"){
this.m_DisableAllWarnings=true;
}else{
this.m_DisableAllWarnings=false;
_ea=_ea.toLowerCase();
_ea=_ea.split(",");
for(var i=0;i<_ea.length;i++){
this.m_WarningMessagesEnabled[_ea[i]]=false;
}
}
}
nitobi.combo.Combo.prototype.IsWarningEnabled=Combo_IsWarningEnabled;
function Combo_IsWarningEnabled(_ec){
if(this.m_ErrorLevel==""){
return;
}else{
if(this.m_WarningMessagesEnabled[_ec]==null){
this.m_WarningMessagesEnabled[_ec]=true;
}
return this.m_WarningMessagesEnabled[_ec]&&this.m_DisableAllWarnings==false;
}
}
nitobi.combo.Combo.prototype.ShowWarning=Combo_ShowWarning;
function Combo_ShowWarning(_ed,_ee,_ef){
if(_ed==false&&this.IsWarningEnabled(_ee)){
var s="NTB:Combo Warning "+_ee+" from "+this.GetId()+"\n\n"+this.m_WarningMessages[_ee]+"\n\nTo disable this and other warnings "+"use the Combo.DisableWarnings tag attribute, e.g., DisableWarnings='"+_ee+",cw101,cw102'";
if(_ef!=null){
s+="\n\nExtra Information\n\t"+_ef;
}
alert(s);
this.m_WarningMessagesEnabled[_ee]=false;
}
}
nitobi.combo.Combo.prototype.SetErrorLevel=Combo_SetErrorLevel;
function Combo_SetErrorLevel(_f1){
this.m_ErrorLevel=_f1.toLowerCase();
}
nitobi.combo.Combo.prototype.GetWidth=Combo_GetWidth;
function Combo_GetWidth(){
return this.m_Width;
}
nitobi.combo.Combo.prototype.SetWidth=Combo_SetWidth;
function Combo_SetWidth(_f2){
this.m_Width=_f2;
}
nitobi.combo.Combo.prototype.GetHeight=Combo_GetHeight;
function Combo_GetHeight(){
return this.m_Height;
}
nitobi.combo.Combo.prototype.SetHeight=Combo_SetHeight;
function Combo_SetHeight(_f3){
this.m_Height=_f3;
}
function _EBAMemScrub(_f4){
for(var _f5 in _f4){
if((_f5.indexOf("m_")==0)||(_f5.indexOf("$")==0)){
_f4[_f5]=null;
}
}
}
nitobi.combo.Combo.prototype.Unload=Combo_Unload;
function Combo_Unload(){
if(this.m_Callback){
delete this.m_Callback;
this.m_Callback=null;
}
if(this.m_TextBox){
this.m_TextBox.Unload();
delete this.m_TextBox;
this.m_TextBox=null;
}
if(this.m_List){
this.m_List.Unload();
delete this.m_List;
this.m_List=null;
}
if(this.m_Button){
this.m_Button.Unload();
delete m_Button;
}
var _f6=this.GetHTMLTagObject();
_EBAMemScrub(this);
_EBAMemScrub(_f6);
}
nitobi.combo.Combo.prototype.GetHttpRequestMethod=Combo_GetHttpRequestMethod;
function Combo_GetHttpRequestMethod(){
try{
return this.m_HttpRequestMethod;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetHttpRequestMethod=Combo_SetHttpRequestMethod;
function Combo_SetHttpRequestMethod(_f7){
try{
if(null==this.m_HTMLTagObject){
this.m_HttpRequestMethod=_f7;
}else{
this.m_HTMLTagObject.className=_f7;
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetCSSClassName=Combo_GetCSSClassName;
function Combo_GetCSSClassName(){
try{
return (null==this.m_HTMLTagObject?this.m_CSSClassName:this.m_HTMLTagObject.className);
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetCSSClassName=Combo_SetCSSClassName;
function Combo_SetCSSClassName(_f8){
try{
if(null==this.m_HTMLTagObject){
this.m_CSSClassName=_f8;
}else{
this.m_HTMLTagObject.className=_f8;
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetInitialSearch=Combo_GetInitialSearch;
function Combo_GetInitialSearch(){
return this.m_InitialSearch;
}
nitobi.combo.Combo.prototype.SetInitialSearch=Combo_SetInitialSearch;
function Combo_SetInitialSearch(_f9){
this.m_InitialSearch=_f9;
}
nitobi.combo.Combo.prototype.GetListZIndex=Combo_GetListZIndex;
function Combo_GetListZIndex(){
return this.m_ListZIndex;
}
nitobi.combo.Combo.prototype.SetListZIndex=Combo_SetListZIndex;
function Combo_SetListZIndex(_fa){
this.m_ListZIndex=_fa;
}
nitobi.combo.Combo.prototype.GetMode=Combo_GetMode;
function Combo_GetMode(){
return this.mode;
}
nitobi.combo.Combo.prototype.GetOnBlurEvent=Combo_GetOnBlurEvent;
function Combo_GetOnBlurEvent(){
try{
return this.m_OnBlurEvent;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnBlurEvent=Combo_SetOnBlurEvent;
function Combo_SetOnBlurEvent(_fb){
try{
this.m_OnBlurEvent=_fb;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.OnBlurEvent=Combo_OnBlurEvent;
function Combo_OnBlurEvent(){
try{
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetFocus=Combo_SetFocus;
function Combo_SetFocus(){
this.GetTextBox().m_HTMLTagObject.focus();
}
nitobi.combo.Combo.prototype.GetOnFocusEvent=Combo_GetOnFocusEvent;
function Combo_GetOnFocusEvent(){
try{
return this.m_OnFocusEvent;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnFocusEvent=Combo_SetOnFocusEvent;
function Combo_SetOnFocusEvent(_fc){
try{
this.m_OnFocusEvent=_fc;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetOnLoadEvent=Combo_GetOnLoadEvent;
function Combo_GetOnLoadEvent(){
if("void"==this.m_OnLoadEvent){
return "";
}
return this.m_OnLoadEvent;
}
nitobi.combo.Combo.prototype.SetOnLoadEvent=Combo_SetOnLoadEvent;
function Combo_SetOnLoadEvent(_fd){
this.m_OnLoadEvent=_fd;
}
nitobi.combo.Combo.prototype.GetOnSelectEvent=Combo_GetOnSelectEvent;
function Combo_GetOnSelectEvent(){
try{
if("void"==this.m_OnSelectEvent){
return "";
}
return this.m_OnSelectEvent;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnSelectEvent=Combo_SetOnSelectEvent;
function Combo_SetOnSelectEvent(_fe){
try{
this.m_OnSelectEvent=_fe;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetOnBeforeSelectEvent=Combo_GetOnBeforeSelectEvent;
function Combo_GetOnBeforeSelectEvent(){
try{
if("void"==this.m_OnBeforeSelectEvent){
return "";
}
return this.m_OnBeforeSelectEvent;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnBeforeSelectEvent=Combo_SetOnBeforeSelectEvent;
function Combo_SetOnBeforeSelectEvent(_ff){
try{
this.m_OnBeforeSelectEvent=_ff;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetHTMLTagObject=Combo_GetHTMLTagObject;
function Combo_GetHTMLTagObject(){
try{
return this.m_HTMLTagObject;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetHTMLTagObject=Combo_SetHTMLTagObject;
function Combo_SetHTMLTagObject(_100){
try{
this.m_HTMLTagObject=_100;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetUniqueId=Combo_GetUniqueId;
function Combo_GetUniqueId(){
try{
return this.m_UniqueId;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetUniqueId=Combo_SetUniqueId;
function Combo_SetUniqueId(_101){
try{
this.m_UniqueId=_101;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetId=Combo_GetId;
function Combo_GetId(){
try{
return this.m_Id;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetId=Combo_SetId;
function Combo_SetId(Id){
try{
this.m_Id=Id;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetButton=Combo_GetButton;
function Combo_GetButton(){
try{
return this.m_Button;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetButton=Combo_SetButton;
function Combo_SetButton(_103){
try{
this.m_Button=_103;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetList=Combo_GetList;
function Combo_GetList(){
try{
return this.m_List;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetList=Combo_SetList;
function Combo_SetList(List){
try{
this.m_List=List;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetTextBox=Combo_GetTextBox;
function Combo_GetTextBox(){
try{
return this.m_TextBox;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetTextBox=Combo_SetTextBox;
function Combo_SetTextBox(_105){
try{
this.m_TextBox=_105;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetDebug=Combo_GetDebug;
function Combo_GetDebug(){
try{
return this.m_Debug;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetDebug=Combo_SetDebug;
function Combo_SetDebug(_106){
try{
this.m_Debug=_106;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetTextValue=Combo_GetTextValue;
function Combo_GetTextValue(){
try{
return this.GetTextBox().GetValue();
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetTextValue=Combo_SetTextValue;
function Combo_SetTextValue(_107){
try{
this.GetTextBox().SetValue(_107);
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetSelectedRowValues=Combo_GetSelectedRowValues;
function Combo_GetSelectedRowValues(){
try{
return this.GetList().GetSelectedRowValues();
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetSelectedRowValues=Combo_SetSelectedRowValues;
function Combo_SetSelectedRowValues(_108){
try{
this.GetList().SetSelectedRowValues(_108);
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetSelectedRowIndex=Combo_GetSelectedRowIndex;
function Combo_GetSelectedRowIndex(){
try{
return this.GetList().GetSelectedRowIndex();
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetSelectedRowIndex=Combo_SetSelectedRowIndex;
function Combo_SetSelectedRowIndex(_109){
try{
this.GetList().SetSelectedRowIndex(_109);
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetDataTextField=Combo_GetDataTextField;
function Combo_GetDataTextField(){
try{
return this.m_DataTextField;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetDataTextField=Combo_SetDataTextField;
function Combo_SetDataTextField(_10a){
try{
this.m_DataTextField=_10a;
var _10b=window.document.getElementById(this.GetId()+"DataTextFieldIndex");
if(null!=_10b){
var _10c=this.GetList().GetXmlDataSource().GetColumnIndex(_10a);
_10b.value=_10c;
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetDataValueField=Combo_GetDataValueField;
function Combo_GetDataValueField(){
try{
return this.m_DataValueField;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetDataValueField=Combo_SetDataValueField;
function Combo_SetDataValueField(_10d){
try{
this.m_DataValueField=_10d;
var _10e=window.document.getElementById(this.GetId()+"DataValueFieldIndex");
if(null!=_10e){
var _10f=this.GetList().GetXmlDataSource().GetColumnIndex(_10d);
_10e.value=_10f;
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetSelectedItem=Combo_GetSelectedItem;
function Combo_GetSelectedItem(){
try{
var _110=new Object;
_110.Value=null;
_110.Text=null;
var _111=this.GetList().GetSelectedRowIndex();
if(-1!=_111){
var _112=this.GetList().GetXmlDataSource();
var row=_112.GetRow(_111);
var _114=_112.GetColumnIndex(this.GetDataValueField());
if(-1!=_114){
_110.Value=row[_114];
}
_114=_112.GetColumnIndex(this.GetDataTextField());
if(-1!=_114){
_110.Text=row[_114];
}
}
return _110;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetOnHideEvent=Combo_GetOnHideEvent;
function Combo_GetOnHideEvent(){
try{
return this.GetList().GetOnHideEvent();
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnHideEvent=Combo_SetOnHideEvent;
function Combo_SetOnHideEvent(_115){
try{
this.GetList().SetOnHideEvent(_115);
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetOnTabEvent=Combo_GetOnTabEvent;
function Combo_GetOnTabEvent(){
try{
return this.m_OnTabEvent;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetOnTabEvent=Combo_SetOnTabEvent;
function Combo_SetOnTabEvent(_116){
try{
this.m_OnTabEvent=_116;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetEventObject=Combo_GetEventObject;
function Combo_GetEventObject(){
try{
return this.m_EventObject;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetEventObject=Combo_SetEventObject;
function Combo_SetEventObject(_117){
try{
this.m_EventObject=_117;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetSmartListSeparator=Combo_GetSmartListSeparator;
function Combo_GetSmartListSeparator(){
return this.SmartListSeparator;
}
nitobi.combo.Combo.prototype.SetSmartListSeparator=Combo_SetSmartListSeparator;
function Combo_SetSmartListSeparator(_118){
this.SmartListSeparator=_118;
}
nitobi.combo.Combo.prototype.GetTabIndex=Combo_GetTabIndex;
function Combo_GetTabIndex(){
try{
return this.m_TabIndex;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetTabIndex=Combo_SetTabIndex;
function Combo_SetTabIndex(_119){
try{
this.m_TabIndex=_119;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.GetEnabled=Combo_GetEnabled;
function Combo_GetEnabled(){
try{
return this.m_Enabled;
}
catch(err){
}
}
nitobi.combo.Combo.prototype.SetEnabled=Combo_SetEnabled;
function Combo_SetEnabled(_11a){
try{
this.m_Enabled=_11a;
var t=this.GetTextBox();
if(null!=t.GetHTMLTagObject()){
if(_11a){
t.Enable();
}else{
t.Disable();
}
}
var b=this.GetButton();
if(null!=b&&null!=b.m_Img){
if(_11a){
b.Enable();
}else{
b.Disable();
}
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.Initialize=Combo_Initialize;
function Combo_Initialize(){
try{
var _11d="ComboBox";
var _11e="";
var _11f="";
var _120="";
var _121="";
var _122="";
var _123="";
var _124="0";
var _125=true;
var _126="default";
var _127=1000;
var _128=",";
var _129="";
var _12a="";
var _12b=this.m_userTag.getAttribute("ListZIndex");
((null==_12b)||(""==_12b))?this.SetListZIndex(_127):this.SetListZIndex(_12b);
this.SetWidth(this.m_userTag.getAttribute("Width"));
this.SetHeight(this.m_userTag.getAttribute("Height"));
var sls=this.m_userTag.getAttribute("SmartListSeparator");
((null==sls)||(""==sls))?this.SetSmartListSeparator(_128):this.SetSmartListSeparator(sls);
var _12d=this.m_userTag.getAttribute("Enabled");
((null==_12d)||(""==_12d))?this.SetEnabled(_125):this.SetEnabled("true"==_12d.toLowerCase());
var _12e=this.m_userTag.getAttribute("TabIndex");
((null==_12e)||(""==_12e))?this.SetTabIndex(_124):this.SetTabIndex(_12e);
var _12f=this.m_userTag.getAttribute("OnTabEvent");
((null==_12f)||(""==_12f))?this.SetOnTabEvent(_123):this.SetOnTabEvent(_12f);
this.SetEventObject(null);
var _130=this.m_userTag.getAttribute("OnFocusEvent");
((null==_130)||(""==_130))?this.SetOnFocusEvent(_122):this.SetOnFocusEvent(_130);
var _131=this.m_userTag.getAttribute("OnBlurEvent");
((null==_131)||(""==_131))?this.SetOnBlurEvent(_121):this.SetOnBlurEvent(_131);
var ose=this.m_userTag.getAttribute("OnSelectEvent");
((null==ose)||(""==ose))?this.SetOnSelectEvent(_11e):this.SetOnSelectEvent(ose);
var ole=this.m_userTag.getAttribute("OnLoadEvent");
((null==ole)||(""==ole))?this.SetOnLoadEvent(_11f):this.SetOnLoadEvent(ole);
var obse=this.m_userTag.getAttribute("OnBeforeSelectEvent");
((null==obse)||(""==obse))?this.SetOnBeforeSelectEvent(_120):this.SetOnBeforeSelectEvent(obse);
var css=this.m_userTag.getAttribute("CSSClassName");
((null==css)||(""==css))?this.SetCSSClassName(_11d):this.SetCSSClassName(css);
var _136=this.m_userTag.uniqueID;
this.SetUniqueId(_136);
if(this.GetWidth()!=null){
if("smartlist"==this.mode){
this.m_TextBox.SetWidth(this.GetWidth());
this.m_TextBox.SetHeight(this.GetHeight());
}
if(nitobi.Browser.GetMeasurementUnitType(this.GetWidth())=="%"){
this.m_userTag.style.display="block";
}else{
this.m_userTag.style.display="inline";
}
if("smartlist"==this.mode){
this.m_userTag.style.height=this.GetHeight();
}else{
this.m_userTag.style.overflow="hidden";
}
}
var html="<span id='EBAComboBox"+_136+"' class='"+this.GetCSSClassName()+"' "+"onMouseOver='window.document.getElementById(\""+this.GetId()+"\").object.m_Over=true' "+"onMouseOut='window.document.getElementById(\""+this.GetId()+"\").object.m_Over=false'>"+"<span id='EBAComboBoxTextAndButton"+_136+"' class='ComboBoxTextAndButton'><nobr>";
var id="";
var _139=this.GetId();
for(var i=0,n=this.GetList().GetXmlDataSource().GetNumberColumns();i<n;i++){
id=_139+"SelectedValue"+i;
html+="<input type='HIDDEN' id='"+id+"' name='"+id+"'></input>";
}
id=_139+"SelectedRowIndex";
html+="<input type='HIDDEN' id='"+id+"' name='"+id+"' value='"+this.GetSelectedRowIndex()+"'></input>";
var _13c=this.GetDataTextField();
id=_139+"DataTextFieldIndex";
var _13d=this.GetList().m_XmlDataSource.GetColumnIndex(_13c);
html+="<input type='HIDDEN' id='"+id+"' name='"+id+"' value='"+_13d+"'></input>";
id=_139+"DataValueFieldIndex";
var _13e=this.GetDataValueField();
_13d=this.GetList().m_XmlDataSource.GetColumnIndex(_13e);
html+="<input type='HIDDEN' id='"+id+"' name='"+id+"' value='"+_13d+"'></input>";
html+="<div>";
html+=this.GetTextBox().GetHTMLRenderString();
var _13f=(this.mode=="default"||this.mode=="unbound");
if(_13f){
html+=this.GetButton().GetHTMLRenderString();
}
html+="<div style=\"overflow: hidden; display: block; clear: both; float: none; height: 0px; width: auto;\"><!-- --></div>";
html+="</div>";
html+="</nobr></span></span>";
this.m_userTag.insertAdjacentHTML("beforeEnd",html);
this.SetHTMLTagObject(window.document.getElementById("EBAComboBox"+_136));
this.GetTextBox().Initialize();
if(_13f){
this.GetButton().Initialize();
}
var is=this.m_InitialSearch;
if(null!=is&&""!=is){
this.InitialSearch(is);
}
eval(this.GetOnLoadEvent());
this.m_userTag=null;
nitobi.combo.numCombosToLoad--;
if(nitobi.combo.numCombosToLoad==0){
nitobi.combo.finishInit();
}
}
catch(err){
}
}
nitobi.combo.Combo.prototype.InitialSearch=Combo_InitialSearch;
function Combo_InitialSearch(_141){
try{
var list=this.GetList();
var tb=this.GetTextBox();
var dfi=tb.GetDataFieldIndex();
list.SetDatabaseSearchTimeoutStatus(EBADatabaseSearchTimeoutStatus_EXPIRED);
list.InitialSearchOnce=true;
this.m_Callback=_EbaComboCallback;
list.Search(_141,dfi,this.m_Callback,this.m_NoDataIsland);
}
catch(err){
}
}
function _EbaComboCallback(_145,list){
if(_145>=0){
var tb=list.GetCombo().GetTextBox();
var row=list.GetRow(_145);
list.SetActiveRow(row);
list.SetSelectedRow(_145);
tb.SetValue(list.GetSelectedRowValues()[tb.GetDataFieldIndex()]);
list.scrollOnce=true;
list.InitialSearchOnce=false;
}else{
var _149=list.GetCombo();
_149.SetTextValue(_149.GetInitialSearch());
}
}
nitobi.combo.Combo.prototype.GetFieldFromActiveRow=Combo_GetFieldFromActiveRow;
function Combo_GetFieldFromActiveRow(_14a){
try{
var l=this.GetList();
if(null!=l){
var r=l.GetActiveRow();
if(null!=r){
var y=l.GetRowIndex(r);
var d=l.GetXmlDataSource();
var x=d.GetColumnIndex(_14a);
return d.GetRowCol(y,x);
}
}
return null;
}
catch(err){
}
}
function Debug(){
this.m_CallStack=new Array;
this.m_CallStackMarker=0;
try{
_ebaWatch.value="";
}
catch(err){
}
}
Debug.prototype.GetCallStack=Debug_GetCallStack;
function Debug_GetCallStack(){
return this.m_CallStack;
}
Debug.prototype.SetCallStack=Debug_SetCallStack;
function Debug_SetCallStack(_150){
this.m_CallStack=_150;
}
Debug.prototype.GetCurrentFunction=Debug_GetCurrentFunction;
function Debug_GetCurrentFunction(){
return this.m_CallStack[this.m_CallStackMarker-1];
}
Debug.prototype.GetState=Debug_GetState;
function Debug_GetState(){
return this.m_State;
}
Debug.prototype.SetState=Debug_SetState;
function Debug_SetState(_151){
this.m_State=_151;
}
Debug.prototype.Assert=Debug_Assert;
function Debug_Assert(_152,_153){
if(this.GetState()&&!_152){
alert("Assert ("+this.GetCurrentFunction()+"): "+_153+"\nStack trace: \n"+this.ShowCallStack());
}
}
Debug.prototype.EnterFunction=Debug_EnterFunction;
function Debug_EnterFunction(_154){
this.m_CallStack[this.m_CallStackMarker++]=_154;
}
Debug.prototype.ExitFunction=Debug_ExitFunction;
function Debug_ExitFunction(){
this.m_CallStack[--this.m_CallStackMarker];
}
Debug.prototype.ShowCallStack=Debug_ShowCallStack;
function Debug_ShowCallStack(){
var s="";
var tabs="\t";
for(var i=0;i<this.m_CallStackMarker;i++){
s+=tabs+this.m_CallStack[i]+"\n";
tabs+="\t";
}
return s;
}
Debug.prototype.SetWatch=Debug_SetWatch;
function Debug_SetWatch(_158,_159){
this.EnterFunction("SetWatch");
try{
_ebaWatch.value=_158+" = "+_159+"\n"+_ebaWatch.value;
this.ExitFunction();
}
catch(err){
this.ExitFunction();
}
}
Debug.prototype.Echo=Debug_Echo;
function Debug_Echo(Msg){
this.EnterFunction("Echo");
try{
_ebaWatch.value="**"+Msg+"\n"+_ebaWatch.value;
this.ExitFunction();
}
catch(err){
this.ExitFunction();
}
}
Debug.prototype.StartTimer=Debug_StartTimer;
function Debug_StartTimer(_15b,_15c){
try{
_15b.Start(_15c);
}
catch(err){
}
}
Debug.prototype.StopTimer=Debug_StopTimer;
function Debug_StopTimer(_15d,_15e){
try{
_15d.Stop(_15e);
}
catch(err){
}
}
Debug.prototype.ShowTimer=Debug_ShowTimer;
function Debug_ShowTimer(_15f,_160,_161){
try{
}
catch(err){
}
}
Debug.prototype.WriteLog=Debug_WriteLog;
function Debug_WriteLog(_162){
try{
writeLog(_162);
}
catch(err){
}
}
Debug.prototype.StopAndShowTimer=Debug_StopAndShowTimer;
function Debug_StopAndShowTimer(_163,_164,_165){
try{
this.StopTimer(_164,_165);
this.ShowTimer(_163,_164,_165);
}
catch(err){
}
}
Debug.printGlobals=function(){
for(var o in window){
writeLog(o);
}
};
function Iframe(_167,h,w,_16a){
if(!_167){
var msg="Iframe constructor: attachee is null!";
alert(msg);
throw msg;
}
var d=window.document;
var oIF=d.createElement("IFRAME");
var s=oIF.style;
this.oIFStyle=s;
this.attachee=_167;
this.attach();
s.position="absolute";
w=w||_167.offsetWidth;
s.width=w;
s.height=h||0;
s.display="none";
s.overflow="hidden";
var name="IFRAME"+oIF.uniqueID;
oIF.name=name;
oIF.id=name;
oIF.frameBorder=0;
oIF.src="javascript:true";
var _170=Browser_GetParentElementByTagName(_16a,"form");
if(null==_170){
_170=d.body;
}
_170.appendChild(oIF);
var oF=window.frames[name];
var oD=oF.window.document;
oD.open();
oD.write("<html><head></head><body style=\"margin:0;background-color:white;\"><span id=\"bodySpan\" class=\"ComboBoxListOuterBorder\" style=\"overflow:hidden;float:left;border-width:1px;border-style:solid;width:"+(w-(nitobi.browser.MOZ?2:0))+";height:"+(h-(nitobi.browser.MOZ?2:0))+";\"></span></body></html>");
oD.close();
var dss=d.styleSheets;
var ss=oD.createElement("LINK");
for(var i=0,n=dss.length;i<n;i++){
var ss2=ss.cloneNode(true);
ss2.rel=(nitobi.browser.IE?dss[i].owningElement.rel:dss[i].ownerNode.rel);
ss2.type="text/css";
ss2.href=dss[i].href;
ss2.title=dss[i].title;
oD.body.appendChild(ss2);
}
var head=oD.getElementsByTagName("head")[0];
var ds=(d.scripts?d.scripts:d.getElementsByTagName("script"));
var st=oD.createElement("SCRIPT");
var src=null;
for(var i=0,n=ds.length;i<n;i++){
src=ds[i].src;
if(""!=src){
var st2=st.cloneNode(true);
st2.language=ds[i].language;
st2.src=src;
head.appendChild(st2);
}
}
this.oIF=oIF;
this.oF=oF;
this.d=oD;
this.bodySpan=oD.getElementById("bodySpan");
this.bodySpanStyle=this.bodySpan.style;
if(window.addEventListener){
window.addEventListener("resize",this,false);
}else{
if(window.attachEvent){
if(!window.g_Iframe_oIFs){
window.g_Iframe_oIFs=new Array;
window.g_Iframe_onresize=window.onresize;
Iframe_oResize();
window.onresize=window.oResize.check1;
}
window.g_Iframe_oIFs[name]=this;
}
}
}
Iframe.prototype.Unload=Iframe_Unload;
function Iframe_Unload(){
if(this.oIF){
delete this.oIF;
}
}
var g_Iframe_oIFs=null;
var g_Iframe_onresize=null;
function Iframe_onafterresize(){
for(var f in window.g_Iframe_oIFs){
var oIF=window.g_Iframe_oIFs[f];
oIF.attach();
}
if(window.g_Iframe_onresize){
window.g_Iframe_onresize();
}
}
function Iframe_dfxWinXY(w){
var b,d,x,y;
x=y=0;
var d=window.document;
if(d.body){
b=d.documentElement.clientWidth?d.documentElement:d.body;
x=b.clientWidth||0;
y=b.clientHeight||0;
}
return {x:x,y:y};
}
function Iframe_oResize(){
window.oResize={CHECKTIME:500,oldXY:Iframe_dfxWinXY(window),timerId:0,check1:function(){
window.oResize.check2();
},check2:function(){
if(this.timerId){
window.clearTimeout(this.timerId);
}
this.timerId=setTimeout("window.oResize.check3()",this.CHECKTIME);
},check3:function(){
var _184=Iframe_dfxWinXY(window);
this.timerId=0;
if((_184.x!=this.oldXY.x)||(_184.y!=this.oldXY.y)){
this.oldXY=_184;
Iframe_onafterresize();
}
}};
}
Iframe.prototype.handleEvent=Iframe_handleEvent;
function Iframe_handleEvent(evt){
switch(evt.type){
case "resize":
if(this.isVisible()){
this.attach();
}
break;
}
}
Iframe.prototype.offset=Iframe_offset;
function Iframe_offset(o,attr,a){
var x=(a?o[attr]:0);
var _o=o;
while(o){
x+=(a?0:o[attr]);
if(nitobi.browser.IE&&"TABLE"==o.tagName&&"0"!=o.border&&""!=o.border){
x++;
}
o=o.offsetParent;
}
return x;
}
Iframe.prototype.setHeight=Iframe_setHeight;
function Iframe_setHeight(h,_18c){
h=parseInt(h);
this.oIFStyle.height=h;
if(_18c!=true){
this.bodySpanStyle.height=(h-(nitobi.browser.MOZ?parseInt(this.bodySpanStyle.borderTopWidth)+parseInt(this.bodySpanStyle.borderBottomWidth):0));
}
}
Iframe.prototype.setWidth=Iframe_setWidth;
function Iframe_setWidth(w){
w=parseInt(w);
this.oIFStyle.width=w;
this.bodySpanStyle.width=(w-(nitobi.browser.MOZ?parseInt(this.bodySpanStyle.borderLeftWidth)+parseInt(this.bodySpanStyle.borderRightWidth):0));
}
Iframe.prototype.show=Iframe_show;
function Iframe_show(){
this.attach();
this.oIFStyle.display="inline";
}
Iframe.prototype.hide=Iframe_hide;
function Iframe_hide(){
this.oIFStyle.display="none";
}
Iframe.prototype.toggle=Iframe_toggle;
function Iframe_toggle(){
if(this.isVisible()){
this.hide();
}else{
this.show();
}
}
Iframe.prototype.isVisible=Iframe_isVisible;
function Iframe_isVisible(){
return "inline"==this.oIFStyle.display;
}
Iframe.prototype.attach=Iframe_attach;
function Iframe_attach(){
var _18e=this.attachee;
var a=(_18e.offsetParent&&"absolute"==_18e.offsetParent.style.position);
this.oIFStyle.top=this.offset(_18e,"offsetTop",a)+_18e.offsetHeight-1+(a?parseInt(_18e.offsetParent.style.top):0);
this.oIFStyle.left=this.offset(_18e,"offsetLeft",a)+(a?parseInt(_18e.offsetParent.style.left):0);
}
var EbaComboUiServerError=0;
var EbaComboUiNoRecords=1;
var EbaComboUiEndOfRecords=2;
var EbaComboUiNumRecords=3;
var EbaComboUiPleaseWait=4;
nitobi.combo.createLanguagePack=function(){
try{
if(typeof (EbaComboUi)=="undefined"){
EbaComboUi=new Array();
EbaComboUi[EbaComboUiServerError]="The ComboBox tried to retrieve information from the server, but an error occured. Please try again later.";
EbaComboUi[EbaComboUiNoRecords]="No new records.";
EbaComboUi[EbaComboUiEndOfRecords]="End of records.";
EbaComboUi[EbaComboUiNumRecords]=" records.";
EbaComboUi[EbaComboUiPleaseWait]="Please Wait...";
}
}
catch(err){
alert("The default language pack could not be loaded.  "+err.message);
}
};
nitobi.lang.defineNs("nitobi.combo");
EBAComboBoxListHeader=0;
EBAComboBoxListBody=1;
EBAComboBoxListFooter=2;
EBAComboBoxListBodyTable=3;
EBAComboBoxListNumSections=4;
EBAComboBoxList=5;
EBADatabaseSearchTimeoutStatus_WAIT=0;
EBADatabaseSearchTimeoutStatus_EXPIRED=1;
EBADatabaseSearchTimeoutStatus_NONE=2;
EBADatabaseSearchTimeoutWait=200;
EBAMoveAction_UP=0;
EBAMoveAction_DOWN=1;
EBAScrollToNone=0;
EBAScrollToTop=1;
EBAScrollToBottom=2;
EBAScrollToNewTop=3;
EBAScrollToTypeAhead=4;
EBAScrollToNewBottom=5;
EBAComboSearchNoRecords=0;
EBAComboSearchNewRecords=1;
EBADefaultScrollbarSize=18;
nitobi.combo.List=function(_190,_191,_192,_193){
try{
this.m_Rendered=false;
var _194="ComboBoxButton";
var _195="150px";
var _196=new Array("50px","100px","50px");
var _197=new Array("ComboBoxListHeader","ComboBoxListBody","ComboBoxListFooter","ComboBoxListBodyTable");
var _198="ComboBoxListBodyTableRowHighlighted";
var _199="highlight";
var _19a="highlighttext";
var _19b="";
var _19c=-1;
var _19d=_193.mode=="default";
var _19e="hidden";
var _19f=false;
var _1a0=_193.mode!="default";
var _1a1;
if(_193.mode!="classic"){
_1a1=10;
}else{
_1a1=25;
}
var _1a2="";
var _1a3="";
var _1a4="";
var _1a5="";
var _1a6=0;
var _1a7=0;
var _1a8="EBA:Combo could not correctly transform XML data. Do you have the MS XML libraries installed? These are typically installed with your browser and are freely available from Microsoft.";
var _1a9="<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0' xmlns:eba='http://developer.ebusiness-apps.com' xmlns:ntb='http://www.nitobi.com' exclude-result-prefixes='eba ntb'>"+"<xsl:output method='xml' version='4.0' omit-xml-declaration='yes' />"+"<xsl:template match='/'>"+"<xsl:apply-templates select='eba:ComboValues|ntb:ComboValues'/>"+"</xsl:template>"+"<xsl:template match='/eba:ComboValues|ntb:ComboValues'>"+"<root>"+"<xsl:attribute name='fields'><xsl:value-of select='@fields' /></xsl:attribute>"+"\t<xsl:apply-templates/>"+"</root>"+"</xsl:template>"+"<xsl:template match='eba:ComboValue|eba:combovalue|ntb:ComboValue|ntb:combovalue'>"+"\t<e><xsl:for-each select='@*'><xsl:attribute name='{name()}'><xsl:value-of select=\".\"/></xsl:attribute></xsl:for-each></e>"+"</xsl:template>"+"</xsl:stylesheet>";
this.SetCombo(_193);
var ps=(_190?_190.getAttribute("PageSize"):null);
((null==ps)||(""==ps))?this.SetPageSize(_1a1):this.SetPageSize(parseInt(ps));
this.clip=(_193.mode=="smartsearch"||_193.mode=="smartlist"||_193.mode=="filter");
var _1ab=(_190?_190.getAttribute("ClipLength"):null);
((null==_1ab)||(""==_1ab))?this.SetClipLength(this.GetPageSize()):this.SetClipLength(_1ab);
this.SetXmlDataSource(new nitobi.combo.XmlDataSource(_191,this.clip,this.clipLength,_193));
this.m_httpRequest=xbXMLHTTP.create();
this.unboundMode=false;
if(!_191){
this.unboundMode=true;
var _1ac=null;
var _1ad="<eba:ComboValues fields='"+_192.getAttribute("fields")+"' xmlns:eba='http://developer.ebusiness-apps.com' xmlns:ntb='http://www.nitobi.com'>";
_1ad+=_192.innerHTML.substr(nitobi.browser.IE?31:0)+"</eba:ComboValues>";
_1ad=nitobi.Browser.EncodeAngleBracketsInTagAttributes(_1ad,_193).replace(/&nbsp;/g,"&#160;").replace(/>\s+</g,"><");
try{
var oXSL=xbDOM.create();
oXSL.loadXML(_1a9);
tmp=xbDOM.create();
tmp.loadXML(_1ad);
xmlObject=xbDOM.create();
tmp.transformNodeToObject(oXSL,xmlObject);
this.GetXmlDataSource().SetXmlObject(xmlObject);
this.GetXmlDataSource().m_Dirty=false;
}
catch(err){
alert(_1a8);
}
}
this.m_SectionHTMLTagObjects=new Array;
this.m_SectionCSSClassNames=new Array;
this.m_SectionHeights=new Array;
this.m_ListColumnDefinitions=new Array;
var _1af=null;
var _1b0=0;
var _1b1=null;
var _1b2=this.GetCombo().GetDataTextField();
var _1b3=false;
var _1b4=true;
while(_1b4){
if(_1b2!=null||_1b3==true){
var _1b5=new Object;
_1b5.DataFieldIndex=this.GetXmlDataSource().GetColumnIndex(_1b2);
_1b5.DataValueIndex=this.GetXmlDataSource().GetColumnIndex(_193.GetDataValueField());
_1b5.HeaderLabel="";
_1b5.Width="100%";
this.m_ListColumnDefinitions[0]=new nitobi.combo.ListColumnDefinition(_1b5);
_1b4=false;
}else{
var _1b6=_190;
if((null==_190)||(0==_190.childNodes.length)){
_1b6=_193.m_userTag;
}
var _1b7=null;
for(var i=0;i<_1b6.childNodes.length;i++){
_1af=_1b6.childNodes[i];
_1b7=_1af.tagName;
if(_1b7){
_1b7=_1b7.toLowerCase().replace(/^eba:/,"").replace(/^ntb:/,"");
if(_1b7=="combocolumndefinition"){
this.m_ListColumnDefinitions[_1b0]=new nitobi.combo.ListColumnDefinition(_1af);
_1b0++;
_1b4=false;
}
}
}
_1b3=true;
}
}
var _1b9=(_190?_190.getAttribute("Width"):null);
((null==_1b9)||(""==_1b9))?this.SetWidth(_195):this.SetWidth(_1b9);
var _1ba=(_190?_190.getAttribute("Overflow-y"):null);
this.m_overflowy=((null==_1ba)||(""==_1ba))?_19e:_1ba;
var chh=(_190?_190.getAttribute("CustomHTMLHeader"):null);
((null==chh)||(""==chh))?this.SetCustomHTMLHeader(""):this.SetCustomHTMLHeader(chh);
for(var i=0;i<EBAComboBoxListNumSections;i++){
this.SetSectionCSSClassName(i,_197[i]);
}
for(var i=0;i<=EBAComboBoxListFooter;i++){
this.SetSectionHeight(i,_196[i]);
}
var _1bc=(_190?_190.getAttribute("Height"):null);
((null==_1bc)||(""==_1bc))?null:this.SetHeight(parseInt(_1bc));
var hccn=(_190?_190.getAttribute("HighlightCSSClassName"):null);
if((null==hccn)||(""==hccn)){
this.SetHighlightCSSClassName(_198);
this.m_UseHighlightClass=false;
}else{
this.SetHighlightCSSClassName(hccn);
this.m_UseHighlightClass=true;
}
var bhc=(_190?_190.getAttribute("BackgroundHighlightColor"):null);
((null==bhc)||(""==bhc))?this.SetBackgroundHighlightColor(_199):this.SetBackgroundHighlightColor(bhc);
var ohe=(_190?_190.getAttribute("OnHideEvent"):null);
((null==ohe)||(""==ohe))?this.SetOnHideEvent(_1a2):this.SetOnHideEvent(ohe);
var ose=(_190?_190.getAttribute("OnShowEvent"):null);
((null==ose)||(""==ose))?this.SetOnShowEvent(_1a3):this.SetOnShowEvent(ose);
var onbs=(_190?_190.getAttribute("OnBeforeSearchEvent"):null);
((null==onbs)||(""==onbs))?this.SetOnBeforeSearchEvent(_1a4):this.SetOnBeforeSearchEvent(onbs);
var onas=(_190?_190.getAttribute("OnAfterSearchEvent"):null);
((null==onas)||(""==onas))?this.SetOnAfterSearchEvent(_1a5):this.SetOnAfterSearchEvent(onas);
var fhc=(_190?_190.getAttribute("ForegroundHighlightColor"):null);
((null==fhc)||(""==fhc))?this.SetForegroundHighlightColor(_19a):this.SetForegroundHighlightColor(fhc);
var offx=(_190?_190.getAttribute("OffsetX"):null);
((null==offx)||(""==offx))?this.SetOffsetX(_1a6):this.SetOffsetX(offx);
var offy=(_190?_190.getAttribute("OffsetY"):null);
((null==offy)||(""==offy))?this.SetOffsetY(_1a7):this.SetOffsetY(offy);
var sri=(_190?_190.parentNode.getAttribute("SelectedRowIndex"):null);
((null==sri)||(""==sri))?this.SetSelectedRowIndex(_19c):this.SetSelectedRowIndex(parseInt(sri));
var chd=(_190?_190.getAttribute("CustomHTMLDefinition"):null);
((null==chd)||(""==chd))?this.SetCustomHTMLDefinition(_19b):this.SetCustomHTMLDefinition(chd);
var ap=(_190?_190.getAttribute("AllowPaging"):null);
((null==ap)||(""==ap))?this.SetAllowPaging(_19d):this.SetAllowPaging(ap.toLowerCase()=="true");
var fz=(_190?_190.getAttribute("FuzzySearchEnabled"):null);
((null==fz)||(""==fz))?this.SetFuzzySearchEnabled(_19f):this.SetFuzzySearchEnabled(fz.toLowerCase()=="true");
var eds=(_190?_190.getAttribute("EnableDatabaseSearch"):null);
((null==eds)||(""==eds))?this.SetEnableDatabaseSearch(this.unboundMode==false&&_1a0):this.SetEnableDatabaseSearch(this.unboundMode==false&&eds.toLowerCase()=="true");
if(_193.mode=="default"&&this.GetAllowPaging()==true){
this.SetClipLength(this.GetPageSize());
this.clip=true;
}
this.widestColumn=new Array(this.m_ListColumnDefinitions.length);
for(var i=0;i<this.widestColumn.length;i++){
this.widestColumn[i]=0;
}
this.SetDatabaseSearchTimeoutStatus(EBADatabaseSearchTimeoutStatus_NONE);
var durl=(_190?_190.getAttribute("DatasourceUrl"):null);
if((null==durl)||(""==durl)||this.unboundMode==true){
this.SetDatasourceUrl(window.document.location.toString());
this.SetEnableDatabaseSearch(false);
this.unboundMode=true;
}else{
this.SetDatasourceUrl(durl);
this.SetEnableDatabaseSearch(true);
}
this.m_httpRequestReady=true;
this.SetNumPagesLoaded(0);
this.m_userTag=_190;
}
catch(err){
}
};
nitobi.combo.List.prototype.Unload=List_Unload;
function List_Unload(){
if(this.IF){
this.IF.Unload();
delete this.IF;
}
_EBAMemScrub(this);
}
nitobi.combo.List.prototype.SetClipLength=List_SetClipLength;
function List_SetClipLength(_1cc){
this.clipLength=_1cc;
}
nitobi.combo.List.prototype.GetHTMLTagObject=List_GetHTMLTagObject;
function List_GetHTMLTagObject(){
try{
this.Render();
return this.m_HTMLTagObject;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetHTMLTagObject=List_SetHTMLTagObject;
function List_SetHTMLTagObject(_1cd){
try{
this.m_HTMLTagObject=_1cd;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetHighlightCSSClassName=List_GetHighlightCSSClassName;
function List_GetHighlightCSSClassName(){
try{
return this.m_HighlightCSSClassName;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetHighlightCSSClassName=List_SetHighlightCSSClassName;
function List_SetHighlightCSSClassName(_1ce){
try{
this.m_HighlightCSSClassName=_1ce;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetListColumnDefinitions=List_GetListColumnDefinitions;
function List_GetListColumnDefinitions(){
try{
return this.m_ListColumnDefinitions;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetListColumnDefinitions=List_SetListColumnDefinitions;
function List_SetListColumnDefinitions(_1cf){
try{
this.m_ListColumnDefinitions=_1cf;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetCustomHTMLDefinition=List_GetCustomHTMLDefinition;
function List_GetCustomHTMLDefinition(){
try{
return this.m_CustomHTMLDefinition;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetCustomHTMLDefinition=List_SetCustomHTMLDefinition;
function List_SetCustomHTMLDefinition(_1d0){
try{
this.m_CustomHTMLDefinition=_1d0;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetCustomHTMLHeader=List_GetCustomHTMLHeader;
function List_GetCustomHTMLHeader(){
try{
return this.m_CustomHTMLHeader;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetCustomHTMLHeader=List_SetCustomHTMLHeader;
function List_SetCustomHTMLHeader(_1d1){
try{
this.m_CustomHTMLHeader=_1d1;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetCombo=List_GetCombo;
function List_GetCombo(){
try{
return this.m_Combo;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetCombo=List_SetCombo;
function List_SetCombo(_1d2){
try{
this.m_Combo=_1d2;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetXmlDataSource=List_GetXmlDataSource;
function List_GetXmlDataSource(){
try{
return this.m_XmlDataSource;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetXmlDataSource=List_SetXmlDataSource;
function List_SetXmlDataSource(_1d3){
try{
this.m_XmlDataSource=_1d3;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetWidth=List_GetWidth;
function List_GetWidth(){
try{
return this.m_Width;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetWidth=List_SetWidth;
function List_SetWidth(_1d4){
try{
this.m_Width=_1d4;
if(this.m_Rendered){
this.GetHTMLTagObject().style.width=this.GetDesiredPixelWidth();
for(var i=0;i<=EBAComboBoxListFooter;i++){
if(i!=EBAComboBoxListBodyTable){
var _1d6=this.GetSectionHTMLTagObject(i);
if(_1d6!=null){
_1d6.style.width=this.GetDesiredPixelWidth();
}
}
}
this.GenerateCss();
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetDesiredPixelWidth=List_GetDesiredPixelWidth;
function List_GetDesiredPixelWidth(){
var _1d7=this.GetCombo();
var _1d8=document.getElementById(_1d7.GetId());
var _1d9=nitobi.html.getWidth(_1d8);
var _1da=this.GetWidth();
if(nitobi.Browser.GetMeasurementUnitType(_1da)=="%"){
var w=(_1d7.GetWidth()==null?_1d7.GetTextBox().GetWidth():_1d7.GetWidth());
var _1dc=1/(parseInt(w)/100);
var _1da=parseInt(_1da)/100;
return (Math.floor(_1d9*_1dc*_1da-2)+"px");
}else{
return _1da;
}
}
nitobi.combo.List.prototype.GetActualPixelWidth=List_GetActualPixelWidth;
function List_GetActualPixelWidth(){
try{
var tag=this.GetHTMLTagObject();
if(null==tag){
return this.GetDesiredPixelWidth();
}else{
return nitobi.Browser.GetElementWidth(tag);
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetCSSClassName=List_GetCSSClassName;
function List_GetCSSClassName(){
try{
return (null==this.m_HTMLTagObject?this.m_CSSClassName:this.GetHTMLTagObject().className);
}
catch(err){
}
}
nitobi.combo.List.prototype.SetCSSClassName=List_SetCSSClassName;
function List_SetCSSClassName(_1de){
try{
if(null==this.m_HTMLTagObject){
this.m_CSSClassName=_1de;
}else{
this.GetHTMLTagObject().className=_1de;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetSectionHTMLTagObject=List_GetSectionHTMLTagObject;
function List_GetSectionHTMLTagObject(_1df){
try{
this.Render();
return this.m_SectionHTMLTagObjects[_1df];
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSectionHTMLTagObject=List_SetSectionHTMLTagObject;
function List_SetSectionHTMLTagObject(_1e0,_1e1){
try{
this.m_SectionHTMLTagObjects[_1e0]=_1e1;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetSectionCSSClassName=List_GetSectionCSSClassName;
function List_GetSectionCSSClassName(_1e2){
try{
return (null==this.m_HTMLTagObject?this.m_SectionCSSClassNames[_1e2]:this.GetSectionHTMLTagObject(_1e2).className);
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSectionCSSClassName=List_SetSectionCSSClassName;
function List_SetSectionCSSClassName(_1e3,_1e4){
try{
if(null==this.m_HTMLTagObject){
this.m_SectionCSSClassNames[_1e3]=_1e4;
}else{
this.GetSectionHTMLTagObject(_1e3).className=_1e4;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetSectionHeight=List_GetSectionHeight;
function List_GetSectionHeight(_1e5){
try{
if(this.m_HTMLTagObject==null){
return parseInt(this.m_SectionHeights[_1e5]);
}else{
var _1e6=this.m_HTMLTagObject.style;
var top=_1e6.top;
var _1e8=_1e6.display;
var _1e9=_1e6.position;
var _1ea=_1e6.visibility;
if(_1e6.display=="none"||_1e6.visibility!="visible"){
_1e6.position="absolute";
_1e6.top="-1000px";
_1e6.display="inline";
}
var _1eb=null;
if(this.m_SectionHTMLTagObjects[_1e5]!=null){
_1eb=nitobi.html.getHeight(this.m_SectionHTMLTagObjects[_1e5]);
}
if(_1e6.display=="inline"){
_1e6.position=_1e9;
_1e6.display=_1e8;
_1e6.top=top;
}
return _1eb;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSectionHeight=List_SetSectionHeight;
function List_SetSectionHeight(_1ec,_1ed){
try{
if(null==this.m_HTMLTagObject){
this.m_SectionHeights[_1ec]=_1ed;
}else{
this.GetSectionHTMLTagObject(_1ec).style.height=_1ed;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetSelectedRowIndex=List_GetSelectedRowIndex;
function List_GetSelectedRowIndex(){
try{
if(null==this.m_HTMLTagObject){
return parseInt(this.m_SelectedRowIndex);
}else{
return parseInt(window.document.getElementById(this.GetCombo().GetId()+"SelectedRowIndex").value);
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSelectedRowIndex=List_SetSelectedRowIndex;
function List_SetSelectedRowIndex(_1ee){
try{
if(null==this.m_HTMLTagObject){
this.m_SelectedRowIndex=_1ee;
}else{
window.document.getElementById(this.GetCombo().GetId()+"SelectedRowIndex").value=_1ee;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetAllowPaging=List_GetAllowPaging;
function List_GetAllowPaging(){
try{
return this.m_AllowPaging;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetAllowPaging=List_SetAllowPaging;
function List_SetAllowPaging(_1ef){
try{
if(this.m_HTMLTagObject!=null){
if(_1ef){
this.ShowFooter();
}else{
this.HideFooter();
}
}
this.m_AllowPaging=_1ef;
}
catch(err){
}
}
nitobi.combo.List.prototype.IsFuzzySearchEnabled=List_IsFuzzySearchEnabled;
function List_IsFuzzySearchEnabled(){
try{
return this.m_FuzzySearchEnabled;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetFuzzySearchEnabled=List_SetFuzzySearchEnabled;
function List_SetFuzzySearchEnabled(_1f0){
try{
this.m_FuzzySearchEnabled=_1f0;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetPageSize=List_GetPageSize;
function List_GetPageSize(){
try{
return this.m_PageSize;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetPageSize=List_SetPageSize;
function List_SetPageSize(_1f1){
try{
this.m_PageSize=_1f1;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetNumPagesLoaded=List_GetNumPagesLoaded;
function List_GetNumPagesLoaded(){
try{
return this.m_NumPagesLoaded;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetNumPagesLoaded=List_SetNumPagesLoaded;
function List_SetNumPagesLoaded(_1f2){
try{
this.m_NumPagesLoaded=_1f2;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetActiveRow=List_GetActiveRow;
function List_GetActiveRow(){
try{
return this.m_ActiveRow;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetActiveRow=List_SetActiveRow;
function List_SetActiveRow(_1f3){
try{
var _1f4;
if(null!=this.m_ActiveRow){
_1f4=document.getElementById("ContainingTableFor"+this.m_ActiveRow.id);
if(this.m_UseHighlightClass){
_1f4.className=this.m_OriginalRowClass;
}else{
_1f4.style.backgroundColor=this.m_OriginalBackgroundHighlightColor;
_1f4.style.color=this.m_OriginalForegroundHighlightColor;
}
var _1f5=this.GetListColumnDefinitions();
for(var i=0,n=_1f5.length;i<n;i++){
var _1f8=document.getElementById("ContainingSpanFor"+this.m_ActiveRow.id+"_"+i);
if(_1f8!=null){
_1f8.style.color=_1f8.savedColor;
_1f8.style.backgroundColor=_1f8.savedBackgroundColor;
}
}
}
this.m_ActiveRow=_1f3;
if(null!=_1f3){
if("compact"==this.GetCombo().mode&&_1f3!=null){
var _1f9=this.GetRowIndex(_1f3);
this.SetSelectedRow(_1f9);
}
_1f4=document.getElementById("ContainingTableFor"+_1f3.id);
_1f8=document.getElementById("ContainingSpanFor"+this.m_ActiveRow.id);
if(this.m_UseHighlightClass){
this.m_OriginalRowClass=_1f4.className;
_1f4.className=this.GetHighlightCSSClassName();
}else{
this.m_OriginalBackgroundHighlightColor=_1f4.style.backgroundColor;
this.m_OriginalForegroundHighlightColor=_1f4.style.color;
_1f4.style.backgroundColor=this.m_BackgroundHighlightColor;
_1f4.style.color=this.m_ForegroundHighlightColor;
}
var _1f5=this.GetListColumnDefinitions();
for(var i=0,n=_1f5.length;i<n;i++){
var _1f8=document.getElementById("ContainingSpanFor"+this.m_ActiveRow.id+"_"+i);
if(_1f8!=null){
_1f8.savedColor=_1f8.style.color;
_1f8.savedBackgroundColor=_1f8.style.backgroundColor;
_1f8.style.color=_1f4.style.color;
_1f8.style.backgroundColor=_1f4.style.backgroundColor;
}
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetSelectedRowValues=List_GetSelectedRowValues;
function List_GetSelectedRowValues(){
try{
var _1fa=new Array;
for(var i=0;i<this.GetXmlDataSource().GetNumberColumns();i++){
_1fa[i]=window.document.getElementById(this.GetCombo().GetId()+"SelectedValue"+i).value;
}
return _1fa;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSelectedRowValues=List_SetSelectedRowValues;
function List_SetSelectedRowValues(_1fc,Row){
try{
this.m_SelectedRowValues=_1fc;
var _1fe=this.GetCombo().GetId();
var _1ff=this.GetXmlDataSource().GetNumberColumns();
if((null==_1fc)&&(null==Row)){
for(var i=0;i<_1ff;i++){
window.document.getElementById(_1fe+"SelectedValue"+i).value="";
}
}else{
if(null==Row){
for(var i=0;i<_1ff;i++){
window.document.getElementById(_1fe+"SelectedValue"+i).value=_1fc[i];
}
}else{
var _201=this.GetCombo().GetUniqueId();
var _202=this.GetRowIndex(Row);
var _203=this.GetXmlDataSource().GetRow(_202);
this.SetSelectedRowValues(_203,null);
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetEnableDatabaseSearch=List_GetEnableDatabaseSearch;
function List_GetEnableDatabaseSearch(){
try{
return this.m_EnableDatabaseSearch;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetEnableDatabaseSearch=List_SetEnableDatabaseSearch;
function List_SetEnableDatabaseSearch(_204){
try{
this.m_EnableDatabaseSearch=_204;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetFooterText=List_GetFooterText;
function List_GetFooterText(){
try{
if(null==this.m_HTMLTagObject){
return this.m_FooterText;
}else{
var _205=document.getElementById("EBAComboBoxListFooterPageNextButton"+this.GetCombo().GetUniqueId());
return (null!=_205?_205.innerHTML:"");
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetFooterText=List_SetFooterText;
function List_SetFooterText(_206){
try{
if(null==this.m_HTMLTagObject){
this.m_FooterText=_206;
}else{
var _207=this.GetSectionHTMLTagObject(EBAComboBoxListFooter);
if(null!=_207){
_207=document.getElementById("EBAComboBoxListFooterPageNextButton"+this.GetCombo().GetUniqueId());
if(null!=_207){
_207.innerHTML=_206;
}
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetDatabaseSearchTimeoutStatus=List_GetDatabaseSearchTimeoutStatus;
function List_GetDatabaseSearchTimeoutStatus(){
try{
return this.m_DatabaseSearchTimeoutStatus;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetDatabaseSearchTimeoutStatus=List_SetDatabaseSearchTimeoutStatus;
function List_SetDatabaseSearchTimeoutStatus(_208){
try{
this.m_DatabaseSearchTimeoutStatus=_208;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetDatabaseSearchTimeoutId=List_GetDatabaseSearchTimeoutId;
function List_GetDatabaseSearchTimeoutId(){
try{
return this.m_DatabaseSearchTimeoutId;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetDatabaseSearchTimeoutId=List_SetDatabaseSearchTimeoutId;
function List_SetDatabaseSearchTimeoutId(_209){
try{
this.m_DatabaseSearchTimeoutId=_209;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetHeight=List_GetHeight;
function List_GetHeight(){
try{
return this.GetSectionHeight(EBAComboBoxListBody);
}
catch(err){
}
}
nitobi.combo.List.prototype.SetHeight=List_SetHeight;
function List_SetHeight(_20a){
try{
this.SetSectionHeight(EBAComboBoxListBody,parseInt(_20a));
}
catch(err){
}
}
nitobi.combo.List.prototype.GetActualHeight=List_GetActualPixelHeight;
nitobi.combo.List.prototype.GetActualPixelHeight=List_GetActualPixelHeight;
function List_GetActualPixelHeight(){
try{
var uid=this.GetCombo().GetUniqueId();
var tag=this.GetHTMLTagObject();
var _20d=nitobi.Browser.GetElementHeight(tag);
return _20d;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetBackgroundHighlightColor=List_GetBackgroundHighlightColor;
function List_GetBackgroundHighlightColor(){
try{
return this.m_BackgroundHighlightColor;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetBackgroundHighlightColor=List_SetBackgroundHighlightColor;
function List_SetBackgroundHighlightColor(_20e){
try{
this.m_BackgroundHighlightColor=_20e;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetForegroundHighlightColor=List_GetForegroundHighlightColor;
function List_GetForegroundHighlightColor(){
try{
return this.m_ForegroundHighlightColor;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetForegroundHighlightColor=List_SetForegroundHighlightColor;
function List_SetForegroundHighlightColor(_20f){
try{
this.m_ForegroundHighlightColor=_20f;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetDatasourceUrl=List_GetDatasourceUrl;
function List_GetDatasourceUrl(){
try{
return this.m_DatasourceUrl;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetDatasourceUrl=List_SetDatasourceUrl;
function List_SetDatasourceUrl(_210){
try{
this.m_DatasourceUrl=_210;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOnHideEvent=List_GetOnHideEvent;
function List_GetOnHideEvent(){
try{
return this.m_OnHideEvent;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOnHideEvent=List_SetOnHideEvent;
function List_SetOnHideEvent(_211){
try{
this.m_OnHideEvent=_211;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOnShowEvent=List_GetOnShowEvent;
function List_GetOnShowEvent(){
try{
return this.m_OnShowEvent;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOnShowEvent=List_SetOnShowEvent;
function List_SetOnShowEvent(_212){
try{
this.m_OnShowEvent=_212;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOnBeforeSearchEvent=List_GetOnBeforeSearchEvent;
function List_GetOnBeforeSearchEvent(){
try{
return this.m_OnBeforeSearchEvent;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOnBeforeSearchEvent=List_SetOnBeforeSearchEvent;
function List_SetOnBeforeSearchEvent(_213){
try{
this.m_OnBeforeSearchEvent=_213;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOnAfterSearchEvent=List_GetOnAfterSearchEvent;
function List_GetOnAfterSearchEvent(){
try{
return this.m_OnAfterSearchEvent;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOnAfterSearchEvent=List_SetOnAfterSearchEvent;
function List_SetOnAfterSearchEvent(_214){
try{
this.m_OnAfterSearchEvent=_214;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOffsetX=List_GetOffsetX;
function List_GetOffsetX(){
try{
return this.m_OffsetX;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOffsetX=List_SetOffsetX;
function List_SetOffsetX(_215){
try{
this.m_OffsetX=parseInt(_215);
}
catch(err){
}
}
nitobi.combo.List.prototype.GetOffsetY=List_GetOffsetY;
function List_GetOffsetY(){
try{
return this.m_OffsetY;
}
catch(err){
}
}
nitobi.combo.List.prototype.SetOffsetY=List_SetOffsetY;
function List_SetOffsetY(_216){
try{
this.m_OffsetY=parseInt(_216);
}
catch(err){
}
}
nitobi.combo.List.prototype.AdjustSize=List_AdjustSize;
function List_AdjustSize(){
var list=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
var tag=this.GetHTMLTagObject();
var _219=tag.style;
if(true==nitobi.Browser.GetVerticalScrollBarStatus(list)){
if(nitobi.Browser.GetMeasurementUnitType(this.GetWidth())!="%"){
listWidth=parseInt(this.GetWidth())+nitobi.Browser.GetScrollBarWidth(list)-(nitobi.browser.MOZ?EBADefaultScrollbarSize:0);
listWidth=this.GetDesiredPixelWidth();
}else{
listWidth=this.GetDesiredPixelWidth();
}
list.style.width=listWidth;
header=this.GetSectionHTMLTagObject(EBAComboBoxListHeader);
footer=this.GetSectionHTMLTagObject(EBAComboBoxListFooter);
if(header!=null){
header.style.width=listWidth;
}
if(footer!=null){
footer.style.width=listWidth;
}
_219.width=(listWidth);
if(nitobi.browser.IE){
var _21a=nitobi.combo.iframeBacker.style;
_21a.width=_219.width;
}
}
if(nitobi.browser.IE){
var _21a=nitobi.combo.iframeBacker.style;
_21a.height=_219.height;
}
}
nitobi.combo.List.prototype.IsVisible=List_IsVisible;
function List_IsVisible(){
if(!this.m_Rendered){
return false;
}
var tag=this.GetHTMLTagObject();
var _21c=tag.style;
return (_21c.visibility=="visible");
}
nitobi.combo.List.prototype.Show=List_Show;
function List_Show(){
try{
var _21d=this.GetCombo();
var mode=_21d.mode;
this.Render();
if(!this.m_HTMLTagObject||this.IsVisible()||mode=="compact"||this.GetXmlDataSource().GetNumberRows()==0||((mode!="default"&&mode!="unbound")&&_21d.GetTextBox().m_HTMLTagObject.value=="")){
return;
}
var tag=this.GetHTMLTagObject();
var _220=_21d.GetTextBox().GetHTMLContainerObject();
var _221=tag.style;
this.AdjustSize();
var _222=nitobi.html.getHeight(_220);
var _223=(nitobi.browser.MOZ?0:(document.parentWindow.self.frameElement==null?0:0));
var top=nitobi.html.getCoords(_220).y+_222-_223;
var left=nitobi.html.getCoords(_220).x-_223;
var _226=parseInt(this.GetActualPixelHeight());
var _227=parseInt(this.GetActualPixelWidth());
_221.top=top+"px";
_221.left=left+"px";
_221.zIndex=_21d.m_ListZIndex;
var _228=nitobi.html.getBodyArea().clientWidth;
var _229=nitobi.html.getBodyArea().clientHeight;
var _22a=(document.body.scrollTop==""||parseInt(document.documentElement.scrollTop==0)?0:parseInt(document.body.scrollTop));
var _22b=(document.body.scrollLeft==""||parseInt(document.documentElement.scrollLeft==0)?0:parseInt(document.body.scrollLeft));
if(parseInt(top)-_22a+_226>_229){
var _22c=parseInt(_221.top)-_226-_222;
if(_22c>=0){
_221.top=_22c+"px";
}
}
if(parseInt(left)-parseInt(_22b)+_227>_228){
var _22d=document.getElementById(_21d.GetId());
var _22e=nitobi.html.getWidth(_22d);
if(_227>_22e){
var _22f=_227-_22e;
var _230=left-_22f;
if(_230>=0){
_221.left=_230+"px";
}
}
}
_221.position="absolute";
_221.display="inline";
this.GenerateCss();
_221.visibility="visible";
this.SetIFrameDimensions();
this.ShowIFrame();
eval(this.GetOnShowEvent());
}
catch(err){
}
}
nitobi.combo.List.prototype.SetX=function(x){
var tag=this.GetHTMLTagObject();
tag.style.left=x;
};
nitobi.combo.List.prototype.GetX=function(){
var _233=this.GetCombo();
var _234=nitobi.html.getCoords(_233.GetHTMLTagObject());
return _234.x;
};
nitobi.combo.List.prototype.SetY=function(y){
var tag=this.GetHTMLTagObject();
tag.style.top=y;
};
nitobi.combo.List.prototype.GetY=function(){
var _237=this.GetCombo().GetTextBox().GetHTMLContainerObject();
var _238=nitobi.html.getHeight(_237);
var y=nitobi.html.getCoords(_237).y+_238;
return y;
};
nitobi.combo.List.prototype.SetFrameX=function(x){
if(nitobi.browser.ie){
nitobi.combo.iframeBacker.style.left=x;
}
};
nitobi.combo.List.prototype.SetFrameY=function(y){
if(nitobi.browser.ie){
nitobi.combo.iframeBacker.style.top=y;
}
};
nitobi.combo.List.prototype.GetFrame=function(){
if(nitobi.browser.ie){
return nitobi.combo.iframeBacker;
}else{
return null;
}
};
nitobi.combo.List.prototype.ShowIFrame=List_ShowIFrame;
function List_ShowIFrame(){
try{
if(nitobi.browser.IE){
var _23c=nitobi.combo.iframeBacker.style;
_23c.visibility="visible";
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetIFrameDimensions=List_SetIFrameDimensions;
function List_SetIFrameDimensions(){
try{
if(nitobi.browser.IE){
var tag=this.GetHTMLTagObject();
var _23e=nitobi.combo.iframeBacker.style;
var _23f=tag.style;
_23e.top=_23f.top;
_23e.left=_23f.left;
_23e.width=nitobi.Browser.GetElementWidth(tag);
_23e.height=nitobi.Browser.GetElementHeight(tag);
_23e.zIndex=parseInt(_23f.zIndex)-1;
}
}
catch(err){
}
}
nitobi.combo.List.prototype.Hide=List_Hide;
function List_Hide(){
try{
if(!this.m_Rendered){
return false;
}
var tag=this.GetHTMLTagObject();
var _241=tag.style;
_241.visibility="hidden";
if(nitobi.browser.MOZ){
_241.display="none";
}
if(nitobi.browser.IE){
var _242=nitobi.combo.iframeBacker.style;
_242.visibility="hidden";
}
eval(this.GetOnHideEvent());
}
catch(err){
}
}
nitobi.combo.List.prototype.Toggle=List_Toggle;
function List_Toggle(){
try{
if(this.IsVisible()){
this.Hide();
this.GetCombo().GetTextBox().ToggleHidden();
}else{
this.Show();
this.GetCombo().GetTextBox().ToggleShow();
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetActiveRowAsSelected=List_SetActiveRowAsSelected;
function List_SetActiveRowAsSelected(){
try{
var _243=this.GetCombo();
var t=_243.GetTextBox();
var row=null;
row=this.GetActiveRow();
if(null!=row){
eval(_243.GetOnBeforeSelectEvent());
}
if(row!=null){
this.SetSelectedRow(this.GetRowIndex(row));
if(_243.mode!="smartlist"){
t.SetValue(this.GetSelectedRowValues()[t.GetDataFieldIndex()]);
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.SetSelectedRow=List_SetSelectedRow;
function List_SetSelectedRow(_246){
this.SetSelectedRowIndex(_246);
var _247=this.GetXmlDataSource().GetRow(_246);
this.SetSelectedRowValues(_247,null);
}
nitobi.combo.List.prototype.OnClick=List_OnClick;
function List_OnClick(Row){
try{
eval(this.GetCombo().GetOnBeforeSelectEvent());
var _249=this.GetRowIndex(Row);
this.SetSelectedRowIndex(_249);
var _24a=this.GetXmlDataSource().GetRow(_249);
this.SetSelectedRowValues(_24a,null);
var _24b=this.GetCombo();
var tb=_24b.GetTextBox();
var _24d=tb.GetDataFieldIndex();
if(_24a.length<=_24d){
alert("You have bound the textbox to a column that does not exist.\nThe textboxDataFieldIndex is "+_24d+".\nThe number of values in the selected row is "+_24a.length+".");
}else{
tb.SetValue(_24a[_24d],_24b.mode=="smartlist");
}
this.Hide();
eval(_24b.GetOnSelectEvent());
}
catch(err){
}
}
nitobi.combo.List.prototype.OnMouseWheel=List_OnMouseWheel;
function List_OnMouseWheel(evt){
try{
if(nitobi.browser.IE){
var b=nitobi.Browser;
var lb=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
var top=this.GetRow(0);
var bot=this.GetRow(this.GetXmlDataSource().GetNumberRows()-1);
if(null!=top){
if(evt.wheelDelta>=120){
b.WheelUp(this);
}else{
if(evt.wheelDelta<=-120){
b.WheelDown(this);
}
}
evt.cancelBubble=true;
evt.returnValue=false;
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.Render=List_Render;
function List_Render(){
try{
if(!this.m_Rendered){
this.m_Rendered=true;
var _253=this.GetCombo();
var _254=document.body;
_254.insertAdjacentHTML("afterBegin",this.GetHTMLRenderString());
this.Initialize(window.document.getElementById("EBAComboBoxText"+_253.GetId()));
this.OnWindowResized();
this.GenerateCss();
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetHTMLRenderString=List_GetHTMLRenderString;
function List_GetHTMLRenderString(){
try{
var _255=this.GetCombo();
var _256=_255.GetUniqueId();
var _257=_255.GetId();
var _258=parseInt(this.GetDesiredPixelWidth());
var _259=false;
var _25a="";
if(this.m_XmlDataSource.GetXmlObject()){
var xml=null;
if(_255.mode=="default"||_255.mode=="unbound"){
xml=this.m_XmlDataSource.GetXmlObject().xml;
}else{
xml="<root></root>";
}
_25a=this.GetRowHTML(xml);
}
var _25c=this.GetListColumnDefinitions();
var s="";
s="<span id=\"EBAComboBoxList"+_256+"\" class=\"ComboBoxList"+"\" style=\"width: "+_258+"px;\" "+"onMouseOver=\"document.getElementById('"+this.GetCombo().GetId()+"').object.m_Over=true\" "+"onMouseOut=\"document.getElementById('"+this.GetCombo().GetId()+"').object.m_Over=false\" "+"onClick=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnFocus()\">\n";
var tag=this.m_userTag;
var _25f=tag.childNodes;
var _260="<span class='ComboMenus ComboListWidth"+_256+"'>";
var _261=false;
for(var i=0;i<_25f.length;i++){
if(_25f[i].nodeName.toLowerCase().replace(/^eba:/,"").replace(/^ntb:/,"")=="combopanel"){
s+=_25f[i].innerHTML;
}
if(_25f[i].nodeName.toLowerCase().replace(/^eba:/,"").replace(/^ntb:/,"")=="combomenu"){
_261=true;
var icon=_25f[i].getAttribute("icon");
_260+="<div style='"+(nitobi.browser.MOZ&&i==0?"":"")+"' class='ComboMenu ComboListWidth"+_256+"' onMouseOver=\"this.className='ComboMenuHighlight ComboListWidth"+_256+"'\" onmouseout=\"this.className='ComboMenu ComboListWidth"+_256+"'\" onclick=\""+_25f[i].getAttribute("OnClickEvent")+"\">";
if(icon!=""){
_260+="<img class='ComboMenuIcon' align='absmiddle' src='"+icon+"'>";
}
_260+=_25f[i].getAttribute("text")+"</div>";
}
}
_260+="</span>";
if(_255.mode=="default"||_255.mode=="filter"||_255.mode=="unbound"){
for(var i=0;i<_25c.length;i++){
if(_25c[i].GetHeaderLabel()!=""){
_259=true;
}
}
var _264=this.GetCustomHTMLHeader();
if((_259==true)||(_264!="")){
s+="<span id='EBAComboBoxListHeader"+_256+"' class='ComboBoxListHeader' style='padding:0px; margin:0px; width: "+_258+"px;' >\n";
if(_264!=""){
s+=_264;
}else{
s+="<table cellspacing='0' cellpadding='0' style='border-collapse:collapse;' class='ComboHeader"+_256+"'>\n";
s+="<tr style='width:100%' id='EBAComboBoxColumnLabels"+_256+"' class='ComboBoxColumnLabels'>\n";
var _265="";
var _266=false;
for(var i=0;i<_25c.length;i++){
var _267=_25c[i].GetWidth();
_265="";
if(_25c[i].GetColumnType().toLowerCase()=="hidden"){
_265+="style='display: none;'";
_25c[i].SetWidth("0%");
}
var _268="comboColumn_"+i+"_"+_256;
var _269=(i>0?"style='padding-left:0px'":"");
s+="<td "+_269+" align='"+_25c[i].GetAlign()+"' class='ComboBoxColumnLabel "+_268+"' "+_265+">"+_25c[i].GetHeaderLabel()+"</td>\n";
}
s+="</tr>\n";
s+="</table>\n";
}
s+="</span><br>\n";
}
}
if(_261){
s+=_260;
}
s+="<span id='EBAComboBoxListBody"+_256+"' class='ComboBoxListBody"+"' style='width:"+_258+"px;"+(_255.mode=="default"||_255.mode=="unbound"||(_255.mode=="smartsearch"&&this.GetAllowPaging())?"height: "+this.GetSectionHeight(EBAComboBoxListBody)+"px"+(this.m_overflowy=="auto"?";_overflow-y:;_overflow:auto":""):"overflow:visible")+";' onscroll=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetTextBox().GetHTMLTagObject().focus()\" "+"onmousewheel=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnMouseWheel(event)\" "+"onfocus=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnFocus()\">\n";
s+=_25a+"</table>\n"+"</span>\n";
s+="<br><span id='EBAComboBoxListFooter"+_256+"' style='width:"+_258+"px; display:"+(this.GetAllowPaging()?"inline":"none")+"' class='ComboBoxListFooter'>\n";
s+="<span id=\"EBAComboBoxListFooterPageNextButton"+_256+"\" style=\"width:100%\""+" class=\"ComboBoxListFooterPageNextButton\" "+"onMouseOver='this.className=\"ComboBoxListFooterPageNextButtonHighlight\"' "+"onMouseOut='this.className=\"ComboBoxListFooterPageNextButton\"'"+"onClick=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnGetNextPage(null, true);\"></span>\n";
s+="</span>\n"+"</span>\n";
s+="</span>\n";
return s;
}
catch(err){
}
}
nitobi.combo.List.prototype.Initialize=List_Initialize;
function List_Initialize(_26a){
try{
this.attachee=_26a;
var c=this.GetCombo();
var d=document;
var _26d=c.GetUniqueId();
this.SetHTMLTagObject(d.getElementById("EBAComboBoxList"+_26d));
this.SetSectionHTMLTagObject(EBAComboBoxListHeader,d.getElementById("EBAComboBoxListHeader"+_26d));
this.SetSectionHTMLTagObject(EBAComboBoxListBody,d.getElementById("EBAComboBoxListBody"+_26d));
this.SetSectionHTMLTagObject(EBAComboBoxListFooter,d.getElementById("EBAComboBoxListFooter"+_26d));
this.SetSectionHTMLTagObject(EBAComboBoxListBodyTable,d.getElementById("EBAComboBoxListBodyTable"+_26d));
this.SetSectionHTMLTagObject(EBAComboBoxList,d.getElementById("EBAComboBoxList"+_26d));
if(c.mode=="default"&&true==this.GetAllowPaging()){
this.SetFooterText(this.GetXmlDataSource().GetNumberRows()+EbaComboUi[EbaComboUiNumRecords]);
}
this.Hide();
}
catch(err){
}
}
nitobi.combo.List.prototype.OnMouseOver=List_OnMouseOver;
function List_OnMouseOver(Row){
try{
this.SetActiveRow(Row);
}
catch(err){
}
}
nitobi.combo.List.prototype.OnMouseOut=List_OnMouseOut;
function List_OnMouseOut(Row){
try{
this.SetActiveRow(null);
}
catch(err){
}
}
nitobi.combo.List.prototype.OnFocus=List_OnFocus;
function List_OnFocus(){
try{
var t=this.GetCombo().GetTextBox();
t.m_skipFocusOnce=true;
t.m_HTMLTagObject.focus();
}
catch(err){
}
}
nitobi.combo.List.prototype.OnGetNextPage=List_OnGetNextPage;
function List_OnGetNextPage(_271,_272){
try{
if(this.m_httpRequestReady){
var _273=this.GetXmlDataSource();
var last=null;
if(_272==true){
var n=_273.GetNumberRows();
if(n>0){
last=_273.GetRowCol(n-1,this.GetCombo().GetTextBox().GetDataFieldIndex());
}
}
this.GetPage(_273.GetNumberRows(),this.GetPageSize(),this.GetCombo().GetTextBox().GetIndexSearchTerm(),_271,last);
this.GetCombo().GetTextBox().GetHTMLTagObject().focus();
}
}
catch(err){
}
}
nitobi.combo.List.prototype.OnWindowResized=List_OnWindowResized;
function List_OnWindowResized(){
if(!this.m_Rendered){
return;
}
if(nitobi.Browser.GetMeasurementUnitType(this.GetWidth())=="%"){
this.SetWidth(this.GetWidth());
}
}
nitobi.combo.List.prototype.GenerateCss=List_GenerateCss;
function List_GenerateCss(){
var _276=this.GetListColumnDefinitions();
var uid=this.GetCombo().GetUniqueId();
var _278="";
var _279=-1;
var list=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
var sb=nitobi.Browser.GetScrollBarWidth(list);
var _27c=(nitobi.browser.MOZ?6:0);
var _27d=0;
for(var i=0;i<this.widestColumn.length;i++){
_27d+=this.widestColumn[i];
}
if(_27d<parseInt(this.GetDesiredPixelWidth())){
_27d=parseInt(this.GetDesiredPixelWidth());
}
var _27f=_27d-sb-_27c;
var _280=_27d-sb-_27c;
_278+=".ComboRow"+uid+"{width:"+(_27d-sb)+"px;}";
_278+=".ComboHeader"+uid+"{width:"+(_27d-sb+3)+"px;}";
_278+=".ComboListWidth"+uid+"{width:"+(_27d)+"px;}";
for(var i=0;i<_276.length;i++){
var _281=_276[i].GetWidth();
if(nitobi.Browser.GetMeasurementUnitType(_281)=="%"&&_281!="*"){
_281=Math.floor((parseInt(_281)/100)*_280);
}else{
if(_281!="*"){
_281=parseInt(_281);
}
}
if(_281=="*"||(i==_276.length-1&&_279==-1)){
_279=i;
}else{
if(_281<this.widestColumn[i]){
_281=this.widestColumn[i];
}
_27f-=parseInt(_281);
_278+=".comboColumn_"+i+"_"+uid+"{ width: "+(_281)+"px;}";
}
}
if(_279!=-1){
_278+=".comboColumn_"+_279+"_"+uid+"{ width: "+_27f+"px;}";
}
if(this.stylesheet==null){
this.stylesheet=document.createStyleSheet();
}
this.stylesheet.cssText=_278;
}
nitobi.combo.List.prototype.ClearCss=List_ClearCss;
function List_ClearCss(){
if(this.stylesheet==null){
this.stylesheet=document.createStyleSheet();
}
this.stylesheet.cssText="";
}
nitobi.combo.List.prototype.GetRowHTML=List_GetRowHTML;
function List_GetRowHTML(XML,_283){
try{
var _284=this.GetCombo();
var _285=_284.GetId();
var _286=_284.GetUniqueId();
var _287=this.GetListColumnDefinitions();
var _288=parseInt(this.GetWidth());
var xsl="";
if(nitobi.browser.IE){
xsl="<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" xmlns:jstring=\"http://www.ebusiness-apps.com/comboxsl\"  extension-element-prefixes=\"msxsl\" exclude-result-prefixes=\"jstring\">";
}else{
xsl="<xsl:stylesheet xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" version=\"1.0\"  >";
}
xsl+="<xsl:output method='xml' version='4.0' omit-xml-declaration='yes' />\n"+"<xsl:template match='/'>"+"<table cellspacing='0' cellpadding='0' id='EBAComboBoxListBodyTable"+_286+"_"+this.GetNumPagesLoaded()+"' class='ComboBoxListBodyTable ComboRow"+_286+"'>\n"+"<xsl:apply-templates>"+"</xsl:apply-templates>"+"</table>"+"</xsl:template>";
xsl+="<xsl:template match='e'>";
xsl+="<tr onclick=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnClick(this)\" "+"onmouseover=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnMouseOver(this)\" "+"onmouseout=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnMouseOut(this)\">";
xsl+="<xsl:attribute name='id'>";
var _28a="position()+"+(this.GetXmlDataSource().GetNumberRows()-this.GetXmlDataSource().GetLastPageSize())+"-1";
var _28b="EBAComboBoxRow"+_286+"_<xsl:value-of select='"+_28a+"'/>";
xsl+=_28b+"</xsl:attribute>"+"<td class='ComboRowContainerParent'><table cellspacing='0' cellpadding='0' class='ComboBoxListBodyTableRow "+"ComboRow"+_286+"'>"+"<xsl:attribute name='id'>"+"ContainingTableFor"+_28b+"</xsl:attribute>"+"<tr class='ComboRowContainer'>";
var _28c=this.GetCustomHTMLDefinition();
var _28d;
if(""==_28c){
for(var i=0;i<_287.length;i++){
var _28f="";
var _290=_287[i].GetColumnType().toLowerCase();
if(_290=="hidden"){
_28f+="style='display: none;'";
}
var _291="comboColumn_"+i+"_"+_286;
xsl+="<td align='"+_287[i].GetAlign()+"' "+"class='"+_291+" "+_287[i].GetCSSClassName()+"' "+_28f+">";
xsl+="<span class=\""+_291+"\" style=\"color:"+_287[i].GetTextColor()+";\" onfocus=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnFocus()\""+" onmouseover=\"document.getElementById('"+this.GetCombo().GetId()+"').object.GetList().OnFocus()\">";
xsl+="<xsl:attribute name='id'>"+"ContainingSpanFor"+_28b+"_"+i+"</xsl:attribute>"+"<xsl:text disable-output-escaping='yes'>"+"<![CDATA["+_287[i].GetHTMLPrefix()+""+"]]>"+"</xsl:text>";
_28d=_287[i].GetDataFieldIndex();
if(null==_28d){
_28d=i;
}
_28d=parseInt(_28d);
var _292="";
if(_290=="image"){
_292=_287[i].GetImageHandlerURL();
_292.indexOf("?")==-1?_292+="?":_292+="&";
_292+="image=";
xsl+="<img> <xsl:attribute name='align'><xsl:value-of  select='absmiddle'/></xsl:attribute>"+"<xsl:attribute name='src'><xsl:value-of select=\"concat('"+(_287[i].ImageUrlFromData?"":_292)+"',"+"@"+String.fromCharCode(97+_28d)+")\"/></xsl:attribute>"+"</img>";
}
if((_283!=null)&&(_290!="image")){
xsl+="<xsl:call-template name=\"bold\"><xsl:with-param name=\"string\">";
}
if(_290!="image"){
xsl+="<xsl:value-of select=\"@"+String.fromCharCode(97+_28d)+"\"></xsl:value-of>";
}
if((_283!=null)&&(_290!="image")){
xsl+="</xsl:with-param><xsl:with-param name=\"pattern\" select='"+EbaConstructValidXpathQuery(_283,true)+"'></xsl:with-param></xsl:call-template>";
}
xsl+="<xsl:text disable-output-escaping='yes'>"+"<![CDATA["+_287[i].GetHTMLSuffix()+""+"]]>"+"</xsl:text>";
xsl+="</span>";
xsl+="</td>";
}
}else{
xsl+="<td width='100%'>";
var done=false;
var _294=0;
var _295=0;
var _296=0;
var _297;
while(!done){
_294=_28c.indexOf("${",_295);
if(_294!=-1){
_295=_28c.indexOf("}",_294);
_297=_28c.substr(_294+2,_295-_294-2);
xsl+="<xsl:text disable-output-escaping='yes'>"+"<![CDATA["+_28c.substr(_296,_294-_296)+"]]>"+"</xsl:text>";
xsl+="<xsl:value-of select=\"@"+String.fromCharCode(parseInt(_297)+97)+"\"></xsl:value-of>";
_296=_295+1;
}else{
xsl+="<xsl:text disable-output-escaping='yes'>"+"<![CDATA["+_28c.substr(_296)+"]]>"+"</xsl:text>";
done=true;
}
}
xsl+="</td>";
}
xsl+="</tr></table></td></tr>\n"+"</xsl:template>";
if(_283!=null){
if(nitobi.browser.IE){
xsl+="<msxsl:script language=\"javascript\" implements-prefix=\"jstring\">"+"<![CDATA["+"f"+"unction lowerCase(s) "+"{"+"\treturn s.toLowerCase();"+"}"+"]]>"+"</msxsl:script>";
}
xsl+="<xsl:template name=\"bold\">"+"<xsl:param name=\"string\" select=\"''\" /><xsl:param name=\"pattern\" select=\"''\" /><xsl:param name=\"carryover\" select=\"''\" />";
if(nitobi.browser.IE){
xsl+="<xsl:variable name=\"lcstring\" select=\"jstring:lowerCase(string($string))\"/>"+"<xsl:variable name=\"lcpattern\" select=\"jstring:lowerCase(string($pattern))\"/>";
}else{
xsl+="<xsl:variable name=\"lcstring\" select=\"translate($string,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')\"/>"+"<xsl:variable name=\"lcpattern\" select=\"translate($pattern,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')\"/>";
}
xsl+="<xsl:choose>"+"<xsl:when test=\"$pattern != '' and $string != '' and contains($lcstring,$lcpattern)\">"+"<xsl:variable name=\"newpattern\" select=\"substring($string,string-length(substring-before($lcstring,$lcpattern)) + 1, string-length($pattern))\"/>"+"<xsl:variable name=\"before\" select=\"substring-before($string, $newpattern)\" />"+"<xsl:variable name=\"len\" select=\"string-length($before)\" />"+"<xsl:variable name=\"newcarryover\" select=\"boolean($len&gt;0 and contains(substring($before,$len,1),'%'))\" />"+"<xsl:value-of select=\"$before\" />"+"<xsl:choose>"+"<xsl:when test=\"($len=0 and $carryover) or $newcarryover or ($len&gt;1 and contains(substring($before,$len - 1,1),'%'))\">"+"<xsl:copy-of select=\"$newpattern\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<b><xsl:copy-of select=\"$newpattern\" /></b>"+"</xsl:otherwise></xsl:choose>"+"<xsl:call-template name=\"bold\">"+"<xsl:with-param name=\"string\" select=\"substring-after($string, $newpattern)\" />"+"<xsl:with-param name=\"pattern\" select=\"$pattern\" />"+"<xsl:with-param name=\"carryover\" select=\"$newcarryover\" />"+"</xsl:call-template>"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"$string\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:template>";
}
xsl+="</xsl:stylesheet>";
oXSL=xbDOM.create();
oXSL.loadXML(xsl);
tmp=xbDOM.create();
tmp.loadXML(XML.replace(/>\s+</g,"><"));
var html=tmp.transformNode(oXSL);
return html;
}
catch(err){
}
}
nitobi.combo.List.prototype.ScrollIntoView=List_ScrollIntoView;
function List_ScrollIntoView(Row,Top,_29b){
try{
if(Row&&this.GetCombo().mode!="compact"){
var _29c=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
if(nitobi.Browser.IsObjectInView(Row,_29c,Top,_29b)==false){
nitobi.Browser.ScrollIntoView(Row,_29c,Top);
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.GetRowIndex=List_GetRowIndex;
function List_GetRowIndex(Row){
try{
var vals=Row.id.split("_");
var _29f=vals[vals.length-1];
return _29f;
}
catch(err){
}
}
EBAComboListDatasourceAccessStatus_BUSY=0;
EBAComboListDatasourceAccessStatus_READY=1;
nitobi.combo.List.prototype.GetDatasourceAccessStatus=List_GetDatasourceAccessStatus;
function List_GetDatasourceAccessStatus(){
if(this.m_httpRequestReady){
return EBAComboListDatasourceAccessStatus_READY;
}else{
return EBAComboListDatasourceAccessStatus_BUSY;
}
}
nitobi.combo.List.prototype.Eval=List_Eval;
function List_Eval(_2a0){
eval(_2a0);
}
nitobi.combo.List.prototype.GetPage=List_GetPage;
function List_GetPage(_2a1,_2a2,_2a3,_2a4,_2a5,_2a6,_2a7,_2a8){
try{
this.SetFooterText(EbaComboUi[EbaComboUiPleaseWait]);
if(_2a5==null){
_2a5="";
}
this.m_httpRequest.abort();
if(null==_2a4){
_2a4=EBAScrollToNone;
}
var _2a9=nitobi.Browser;
this.m_OriginalSearchSubstring=_2a3;
var _2aa=this.GetDatasourceUrl();
_2aa.indexOf("?")==-1?_2aa+="?":_2aa+="&";
_2aa+="StartingRecordIndex="+_2a1+"&PageSize="+_2a2+"&SearchSubstring="+encodeURIComponent(_2a3)+"&ComboId="+encodeURI(this.GetCombo().GetId())+"&LastString="+encodeURIComponent(_2a5);
this.m_httpRequest.open(this.GetCombo().GetHttpRequestMethod(),_2aa,true,"","");
var _2ab=this.GetCombo().GetId();
this.m_httpRequest.onreadystatechange=function(){
try{
var _2ac=window.document.getElementById(_2ab);
var co=_2ac.object;
if((_2ac==null)||(co==null)){
alert(EbaComboUi[EbaComboUiServerError]);
}
var t=co.GetTextBox();
var list=co.GetList();
if(list==null){
alert(EbaComboUi[EbaComboUiServerError]);
}
if(list.m_httpRequest.readyState==4){
var _2b0=list.m_httpRequest.responseText;
var _2b1=_2b0.indexOf("<?xml");
if(_2b1!=-1){
_2b0=_2b0.substr(_2b1);
}
var _2b2=list.GetXmlDataSource();
var _2b3=_2b2.GetNumberRows();
var tmp=xbDOM.create();
tmp.loadXML(_2b0);
if(true==list.clip){
tmp=xbClipXml(tmp,"root","e",list.clipLength);
_2b0=tmp.xml;
}
var _2b5=tmp.selectNodes("//e").length;
var _2b6=co.mode!="default"&&!(co.mode=="smartsearch"&&list.GetAllowPaging());
if((_2b5>0)&&(_2a1==0)||_2b6){
list.Clear();
_2b2.Clear();
}
if(_2b5==0&&_2b6){
list.Hide();
}
if(_2b5>0){
_2b2.AddPage(_2b0);
var ss=null;
if(co.mode=="smartsearch"||co.mode=="smartlist"){
ss=list.searchSubstring;
}
list.AddPage(_2b0,ss);
if((_2a1==0)&&(list.GetCombo().GetTextBox().GetSearchTerm()!="")){
list.SetActiveRow(list.GetRow(0));
}
var _2b8=false;
try{
if(!list.IsFuzzySearchEnabled()){
var _2b9=_2b2.Search(list.m_OriginalSearchSubstring,t.GetDataFieldIndex(),co.mode=="smartsearch"||co.mode=="smartlist");
_2b8=(_2b9==-1);
co.ShowWarning(_2b9!=-1,"cw001");
}
}
catch(err){
}
var _2ba;
_2ba=list.IsVisible();
if(EBAScrollToBottom==_2a4){
var r=list.GetRow(_2b3-1);
list.SetActiveRow(r);
list.ScrollIntoView(r,false);
}else{
if(EBAScrollToNewTop==_2a4||EBAScrollToNewBottom==_2a4){
var r=list.GetRow(_2b3);
list.SetActiveRow(r);
list.ScrollIntoView(r,EBAScrollToNewTop==_2a4);
var tb=t.m_HTMLTagObject;
tb.value=list.GetXmlDataSource().GetRowCol(_2b3,t.GetDataFieldIndex());
xbPutCur(tb,tb.value.length);
t.Paging=false;
}else{
if(_2ba){
list.ScrollIntoView(list.GetActiveRow(),true);
}
}
}
try{
if(!_2b8&&_2a6){
_2a6(EBAComboSearchNewRecords,list,_2a3,_2a7,_2a8);
}
}
catch(err){
}
}else{
try{
if(_2a6){
_2a6(EBAComboSearchNoRecords,list,_2a3,_2a7,_2a8);
}
}
catch(err){
}
list.SetFooterText(EbaComboUi[EbaComboUiNoRecords]);
list.SetActiveRow(null);
}
if(list.InitialSearchOnce==true&&_2b5>0){
list.InitialSearchOnce=false;
var row=list.GetRow(0);
list.SetActiveRow(row);
list.SetSelectedRowValues(null,row);
list.SetSelectedRowIndex(0);
var tb=co.GetTextBox();
tb.SetValue(list.GetSelectedRowValues()[tb.GetDataFieldIndex()]);
}
}
list.m_httpRequestReady=true;
t.Paging=false;
}
catch(err){
alert(EbaComboUi[EbaComboUiServerError]+" "+err.message);
}
};
this.m_httpRequest.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
var vs=document.getElementsByName("__VIEWSTATE");
if((vs!=null)&&(vs["__VIEWSTATE"]!=null)){
var _2bf="__VIEWSTATE="+encodeURI(vs["__VIEWSTATE"].value).replace(/\+/g,"%2B");
var _2c0="__EVENTTARGET="+encodeURI(this.GetCombo().GetId());
var args="__EVENTARGUMENT=GetPage";
var _2c2=_2c0+"&"+args+"&"+_2bf;
this.m_httpRequest.send(_2c2);
}else{
this.m_httpRequest.send("EBA Combo Box Get Page Request");
}
this.m_httpRequestReady=false;
return true;
}
catch(err){
alert(EbaComboUi[EbaComboUiServerError]);
}
}
nitobi.combo.List.prototype.Search=List_Search;
function List_Search(_2c3,_2c4,_2c5,_2c6){
try{
var _2c7=this.GetCombo();
var _2c8=this.GetXmlDataSource();
if(_2c7.mode!="default"&&_2c3==""){
this.Hide();
return;
}
if(null==_2c6){
_2c6=false;
}
eval(this.GetOnBeforeSearchEvent());
var _2c9=-1;
if(!this.GetEnableDatabaseSearch()||!_2c8.m_Dirty||_2c7.mode=="unbound"){
_2c9=_2c8.Search(_2c3,_2c4,_2c7.mode=="smartsearch"||_2c7.mode=="smartlist");
if(_2c9>-1&&this.InitialSearchOnce!=true){
this.Show();
}
if(-1!=_2c9){
if(_2c5){
try{
_2c5(_2c9,this);
}
catch(err){
}
}
eval(this.GetOnAfterSearchEvent());
}
if(-1==_2c9&&(false==this.GetEnableDatabaseSearch()||_2c6)){
if(_2c5){
try{
_2c5(_2c9,this);
}
catch(err){
}
}
eval(this.GetOnAfterSearchEvent());
}
}
this.searchSubstring=_2c3;
if((-1==_2c9)&&(this.GetEnableDatabaseSearch()==true&&(_2c6==false))){
var _2ca=this.GetDatabaseSearchTimeoutStatus();
var _2cb="var list = window.document.getElementById('"+_2c7.GetId()+"').object.GetList(); "+"list.SetDatabaseSearchTimeoutStatus(EBADatabaseSearchTimeoutStatus_EXPIRED);"+"var textbox = window.document.getElementById('"+_2c7.GetId()+"').object.GetTextBox();"+"list.Search(textbox.GetSearchTerm(),textbox.GetDataFieldIndex(),textbox.m_Callback);";
var _2cc=this.GetDatabaseSearchTimeoutId();
_2c7.GetTextBox().SetIndexSearchTerm(_2c3);
switch(_2ca){
case (EBADatabaseSearchTimeoutStatus_EXPIRED):
if(_2cc!=null){
window.clearTimeout(_2cc);
}
this.SetDatabaseSearchTimeoutStatus(EBADatabaseSearchTimeoutStatus_NONE);
var _2cd=_EbaListGetPageCallback;
this.GetPage(0,this.GetPageSize(),_2c3,EBAScrollToTypeAhead,null,_2cd,_2c4,_2c5);
break;
case (EBADatabaseSearchTimeoutStatus_WAIT):
if(_2cc!=null){
window.clearTimeout(_2cc);
}
var _2cc=window.setTimeout(_2cb,EBADatabaseSearchTimeoutWait);
this.SetDatabaseSearchTimeoutId(_2cc);
case (EBADatabaseSearchTimeoutStatus_NONE):
this.SetDatabaseSearchTimeoutStatus(EBADatabaseSearchTimeoutStatus_WAIT);
var _2cc=window.setTimeout(_2cb,EBADatabaseSearchTimeoutWait);
this.SetDatabaseSearchTimeoutId(_2cc);
}
}
}
catch(err){
}
}
function _EbaListGetPageCallback(_2ce,list,_2d0,_2d1,_2d2){
if((list==null)){
alert(EbaComboUi[EbaComboUiServerError]);
}
if(_2ce==EBAComboSearchNewRecords){
if(!list.IsFuzzySearchEnabled()){
list.Search(_2d0,_2d1,_2d2);
}else{
list.Show();
}
}else{
_2d2(-1,list);
list.Eval(list.GetOnAfterSearchEvent());
}
}
nitobi.combo.List.prototype.Clear=List_Clear;
function List_Clear(){
try{
var _2d3=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
_2d3.innerHTML="";
this.SetSelectedRowIndex(-1);
this.SetSelectedRowValues(null);
}
catch(err){
}
}
nitobi.combo.List.prototype.FitContent=List_FitContent;
function List_FitContent(){
try{
var _2d4=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
var _2d5=_2d4.childNodes[_2d4.childNodes.length-1];
var row=_2d5;
while(row.childNodes[0]!=null&&row.childNodes[0].className.indexOf("ComboBoxListColumnDefinition")==-1){
row=row.childNodes[0];
}
for(var i=0;i<row.childNodes.length;i++){
var _2d8=nitobi.html.getWidth(row.childNodes[0]);
if(this.widestColumn[i]<_2d8){
this.widestColumn[i]=_2d8;
}
}
}
catch(err){
}
}
nitobi.combo.List.prototype.AddPage=List_AddPage;
function List_AddPage(_2d9,_2da){
try{
var _2db=this.GetXmlDataSource();
var tmp=xbDOM.create();
tmp.loadXML(_2d9);
var _2dd=tmp.selectNodes("//e").length;
if(_2dd>0){
var html=this.GetRowHTML(_2d9,_2da);
var _2df=this.GetSectionHTMLTagObject(EBAComboBoxListBody);
_2df.insertAdjacentHTML("beforeEnd",html,true);
this.GenerateCss();
}
var _2e0=_2db.GetLastPageSize();
if(0==_2dd){
this.SetFooterText(EbaComboUi[EbaComboUiEndOfRecords]);
}else{
this.SetFooterText(_2db.GetNumberRows()+EbaComboUi[EbaComboUiNumRecords]);
}
this.AdjustSize();
this.SetIFrameDimensions();
}
catch(err){
}
}
nitobi.combo.List.prototype.HideFooter=List_HideFooter;
function List_HideFooter(){
try{
var _2e1=this.GetSectionHTMLTagObject(EBAComboBoxListFooter);
var _2e2=_2e1.style;
_2e2.display="none";
}
catch(err){
}
}
nitobi.combo.List.prototype.ShowFooter=List_ShowFooter;
function List_ShowFooter(){
try{
var _2e3=this.GetSectionHTMLTagObject(EBAComboBoxListFooter);
var _2e4=_2e3.style;
_2e4.display="inline";
}
catch(err){
}
}
nitobi.combo.List.prototype.AddRow=List_AddRow;
function List_AddRow(_2e5){
try{
var xml="<root><e ";
for(var i=0;i<_2e5.length;i++){
xml+=String.fromCharCode(i+97)+"='"+EbaXmlEncode(_2e5[i])+"' ";
}
xml+="/></root>";
this.GetXmlDataSource().AddPage(xml);
this.AddPage(xml);
}
catch(err){
alert("Error. The values must be valid XML attributes.");
}
}
nitobi.combo.List.prototype.Move=List_Move;
function List_Move(_2e8){
try{
var _2e9=this.GetCombo();
var mode=_2e9.mode;
if(mode=="compact"||this.GetXmlDataSource().GetNumberRows()==0||(mode!="default"&&mode!="unbound"&&_2e9.GetTextBox().m_HTMLTagObject.value=="")){
return false;
}
var _2eb=this.GetActiveRow();
this.Show();
if(null==_2eb){
_2eb=this.GetRow(0,null);
}else{
var _2ec=this.GetRowIndex(this.GetActiveRow());
switch(_2e8){
case (EBAMoveAction_UP):
_2ec--;
break;
case (EBAMoveAction_DOWN):
_2ec++;
break;
default:
}
if((_2ec>=0)&&(_2ec<this.GetXmlDataSource().GetNumberRows())){
_2eb=this.GetRow(_2ec,null);
}
}
this.SetActiveRow(_2eb);
this.ScrollIntoView(_2eb,false,true);
return true;
}
catch(err){
}
}
nitobi.combo.List.prototype.GetRow=List_GetRow;
function List_GetRow(_2ed,Id){
try{
if(null!=_2ed){
return document.getElementById("EBAComboBoxRow"+this.GetCombo().GetUniqueId()+"_"+_2ed);
}
if(null!=Id){
return document.getElementById(Id);
}
}
catch(err){
}
}
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.ListColumnDefinition=function(_2ef){
try{
if(!_2ef.getAttribute){
_2ef.getAttribute=function(a){
return this[a];
};
}
var _2f1="50px";
var _2f2="ComboBoxListColumnDefinition";
var _2f3="text";
var _2f4="";
var _2f5="left";
var _2f6="#000";
var _2f7=(_2ef?_2ef.getAttribute("TextColor"):null);
((null==_2f7)||(""==_2f7))?this.SetTextColor(_2f6):this.SetTextColor(_2f7);
var _2f8=(_2ef?_2ef.getAttribute("Align"):null);
((null==_2f8)||(""==_2f8))?this.SetAlign(_2f5):this.SetAlign(_2f8);
var _2f9=(_2ef?_2ef.getAttribute("Width"):null);
((null==_2f9)||(""==_2f9))?this.SetWidth(_2f1):this.SetWidth(_2f9);
var ihu=(_2ef?_2ef.getAttribute("ImageHandlerURL"):null);
((null==ihu)||(""==ihu))?this.SetImageHandlerURL(_2f4):this.SetImageHandlerURL(ihu);
var ct=(_2ef?_2ef.getAttribute("ColumnType"):null);
((null==ct)||(""==ct))?this.SetColumnType(_2f3):this.SetColumnType(ct.toLowerCase());
this.ImageUrlFromData=((this.GetColumnType()=="image")&&((null==ihu)||(""==ihu)));
var ccn=(_2ef?_2ef.getAttribute("CSSClassName"):null);
((null==ccn)||(""==ccn))?this.SetCSSClassName(_2f2):this.SetCSSClassName(ccn);
var hp=(_2ef?_2ef.getAttribute("HTMLPrefix"):null);
((null==hp)||(""==hp))?this.SetHTMLPrefix(""):this.SetHTMLPrefix(hp);
var hs=(_2ef?_2ef.getAttribute("HTMLSuffix"):null);
((null==hs)||(""==hs))?this.SetHTMLSuffix(""):this.SetHTMLSuffix(hs);
var hl=(_2ef?_2ef.getAttribute("HeaderLabel"):null);
((null==hl)||(""==hl))?this.SetHeaderLabel(""):this.SetHeaderLabel(hl);
var dfi=(_2ef?_2ef.getAttribute("DataFieldIndex"):null);
((null==dfi)||(""==dfi))?this.SetDataFieldIndex(0):this.SetDataFieldIndex(dfi);
}
catch(err){
}
};
nitobi.combo.ListColumnDefinition.prototype.GetAlign=ListColumnDefinition_GetAlign;
function ListColumnDefinition_GetAlign(){
try{
return this.m_Align;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetAlign=ListColumnDefinition_SetAlign;
function ListColumnDefinition_SetAlign(_301){
try{
_301=_301.toLowerCase();
if("right"!=_301&&"left"!=_301&&"center"!=_301){
_301="left";
}
this.m_Align=_301;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetTextColor=ListColumnDefinition_GetTextColor;
function ListColumnDefinition_GetTextColor(){
try{
return this.m_TextColor;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetTextColor=ListColumnDefinition_SetTextColor;
function ListColumnDefinition_SetTextColor(_302){
try{
this.m_TextColor=_302;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetHTMLSuffix=ListColumnDefinition_GetHTMLSuffix;
function ListColumnDefinition_GetHTMLSuffix(){
try{
return this.m_HTMLSuffix;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetHTMLSuffix=ListColumnDefinition_SetHTMLSuffix;
function ListColumnDefinition_SetHTMLSuffix(_303){
try{
this.m_HTMLSuffix=_303;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetHTMLPrefix=ListColumnDefinition_GetHTMLPrefix;
function ListColumnDefinition_GetHTMLPrefix(){
try{
return this.m_HTMLPrefix;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetHTMLPrefix=ListColumnDefinition_SetHTMLPrefix;
function ListColumnDefinition_SetHTMLPrefix(_304){
try{
this.m_HTMLPrefix=_304;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetCSSClassName=ListColumnDefinition_GetCSSClassName;
function ListColumnDefinition_GetCSSClassName(){
try{
return this.m_CSSClassName;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetCSSClassName=ListColumnDefinition_SetCSSClassName;
function ListColumnDefinition_SetCSSClassName(_305){
try{
this.m_CSSClassName=_305;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetColumnType=ListColumnDefinition_GetColumnType;
function ListColumnDefinition_GetColumnType(){
try{
return this.m_ColumnType;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetColumnType=ListColumnDefinition_SetColumnType;
function ListColumnDefinition_SetColumnType(_306){
try{
this.m_ColumnType=_306;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetHeaderLabel=ListColumnDefinition_GetHeaderLabel;
function ListColumnDefinition_GetHeaderLabel(){
try{
return this.m_HeaderLabel;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetHeaderLabel=ListColumnDefinition_SetHeaderLabel;
function ListColumnDefinition_SetHeaderLabel(_307){
try{
this.m_HeaderLabel=_307;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetWidth=ListColumnDefinition_GetWidth;
function ListColumnDefinition_GetWidth(){
try{
return this.m_Width;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetWidth=ListColumnDefinition_SetWidth;
function ListColumnDefinition_SetWidth(_308){
try{
this.m_Width=_308;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetDataFieldIndex=ListColumnDefinition_GetDataFieldIndex;
function ListColumnDefinition_GetDataFieldIndex(){
try{
return this.m_DataFieldIndex;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetDataFieldIndex=ListColumnDefinition_SetDataFieldIndex;
function ListColumnDefinition_SetDataFieldIndex(_309){
try{
this.m_DataFieldIndex=_309;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.GetImageHandlerURL=ListColumnDefinition_GetImageHandlerURL;
function ListColumnDefinition_GetImageHandlerURL(){
try{
return this.m_ImageHandlerURL;
}
catch(err){
}
}
nitobi.combo.ListColumnDefinition.prototype.SetImageHandlerURL=ListColumnDefinition_SetImageHandlerURL;
function ListColumnDefinition_SetImageHandlerURL(_30a){
try{
this.m_ImageHandlerURL=_30a;
}
catch(err){
}
}
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.TextBox=function(_30b,_30c,_30d){
try{
var _30e="";
if(nitobi.browser.IE){
_30e="ComboBoxTextIE";
}else{
_30e="ComboBoxTextMoz";
}
var _30f="100px";
var _310="";
var _311=true;
var _312="";
var _313=0;
var _314="";
var _315="";
this.SetCombo(_30c);
var oeku=(_30b?_30b.getAttribute("OnEditKeyUpEvent"):null);
((null==oeku)||(""==oeku))?this.SetOnEditKeyUpEvent(_315):this.SetOnEditKeyUpEvent(oeku);
var _317=(_30b?_30b.getAttribute("Width"):null);
((null==_317)||(""==_317))?this.SetWidth(_30f):this.SetWidth(_317);
var _318=(_30b?_30b.getAttribute("Height"):null);
((null==_318)||(""==_318))?this.SetHeight(_310):this.SetHeight(_318);
var ccn=(_30b?_30b.getAttribute("CSSClassName"):null);
((null==ccn)||(""==ccn))?this.SetCSSClassName(_30e):this.SetCSSClassName(ccn);
var _31a=(_30b?_30b.getAttribute("Editable"):null);
((null==_31a)||(""==_31a))?this.SetEditable(_311):this.SetEditable(_31a);
var _31b=(_30b?_30b.getAttribute("Value"):null);
((null==_31b)||(""==_31b))?this.SetValue(_312):this.SetValue(_31b);
var _31c=_30c.GetDataTextField();
if(_31c!=null){
this.SetDataFieldIndex(_30c.GetList().GetXmlDataSource().GetColumnIndex(_31c));
}else{
var dfi=(_30b?_30b.getAttribute("DataFieldIndex"):null);
((null==dfi)||(""==dfi))?this.SetDataFieldIndex(_313):this.SetDataFieldIndex(dfi);
}
var st=(_30b?_30b.getAttribute("SearchTerm"):null);
if((null==st)||(""==st)){
this.SetSearchTerm(_314);
this.SetIndexSearchTerm(_314);
}else{
this.SetSearchTerm(st);
this.SetIndexSearchTerm(st);
}
this.hasButton=_30d;
this.m_userTag=_30b;
}
catch(err){
}
};
nitobi.combo.TextBox.prototype.Unload=TextBox_Unload;
function TextBox_Unload(){
if(this.m_List){
delete this.m_List;
this.m_List=null;
}
if(this.m_Callback){
delete this.m_Callback;
this.m_Callback=null;
}
_EBAMemScrub(this);
}
nitobi.combo.TextBox.prototype.GetCSSClassName=TextBox_GetCSSClassName;
function TextBox_GetCSSClassName(){
try{
return (null==this.m_HTMLTagObject?this.m_CSSClassName:this.m_HTMLTagObject.className);
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetCSSClassName=TextBox_SetCSSClassName;
function TextBox_SetCSSClassName(_31f){
try{
if(null==this.m_HTMLTagObject){
this.m_CSSClassName=_31f;
}else{
this.m_HTMLTagObject.className=_31f;
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetHeight=TextBox_GetHeight;
function TextBox_GetHeight(){
try{
return (null==this.m_HTMLTagObject?this.m_Height:nitobi.html.Css.getStyle(this.m_HTMLTagObject,"height"));
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetHeight=TextBox_SetHeight;
function TextBox_SetHeight(_320){
try{
if(null==this.m_HTMLTagObject){
this.m_Height=_320;
}else{
this.m_HTMLTagObject.style.height=_320;
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetWidth=TextBox_GetWidth;
function TextBox_GetWidth(){
try{
if(null==this.m_HTMLTagObject){
return this.m_Width;
}else{
return nitobi.html.Css.getStyle(this.GetHTMLContainerObject(),"width");
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetWidth=TextBox_SetWidth;
function TextBox_SetWidth(_321){
try{
this.m_Width=_321;
if(null!=this.m_HTMLTagObject){
this.m_HTMLTagObject.style.width=_321;
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetHTMLTagObject=TextBox_GetHTMLTagObject;
function TextBox_GetHTMLTagObject(){
try{
return this.m_HTMLTagObject;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetHTMLTagObject=TextBox_SetHTMLTagObject;
function TextBox_SetHTMLTagObject(_322){
try{
this.m_HTMLTagObject=_322;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetHTMLContainerObject=function(){
return document.getElementById("EBAComboBoxTextContainer"+this.GetCombo().GetUniqueId());
};
nitobi.combo.TextBox.prototype.GetEditable=TextBox_GetEditable;
function TextBox_GetEditable(){
try{
if(null==this.m_HTMLTagObject){
return this.m_Editable;
}else{
return this.m_HTMLTagObject.getAttribute("contentEditable");
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetEditable=TextBox_SetEditable;
function TextBox_SetEditable(_323){
try{
if(null==this.m_HTMLTagObject){
this.m_Editable=_323;
}else{
this.m_HTMLTagObject.setAttribute("contentEditable",_323);
if(_323==true){
this.m_HTMLTagObject.removeAttribute("readonly");
}else{
this.m_HTMLTagObject.setAttribute("readonly","true");
}
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetValue=TextBox_GetValue;
function TextBox_GetValue(){
try{
if(null==this.m_HTMLTagObject){
return this.m_Value;
}else{
return this.m_HTMLTagObject.value;
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetValue=TextBox_SetValue;
function TextBox_SetValue(_324,_325){
try{
if(null==this.m_HTMLTagObject){
this.m_Value=_324;
}else{
if(this.GetCombo().mode=="smartlist"){
this.SmartSetValue(_324,_325);
}else{
this.m_HTMLTagObject.value=_324;
this.m_TextValueTag.value=_324;
}
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SmartSetValue=TextBox_SmartSetValue;
function TextBox_SmartSetValue(_326,_327){
try{
var t=this.m_HTMLTagObject;
var _329=this.GetCombo();
var lio=t.value.lastIndexOf(_329.SmartListSeparator);
if(lio>-1){
_326=t.value.substring(0,lio)+_329.SmartListSeparator+" "+_326;
}
if(_327){
_326+=_329.SmartListSeparator+" ";
}
t.value=_326;
this.m_TextValueTag.value=_326;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetDataFieldIndex=TextBox_GetDataFieldIndex;
function TextBox_GetDataFieldIndex(){
try{
return this.m_DataFieldIndex;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetDataFieldIndex=TextBox_SetDataFieldIndex;
function TextBox_SetDataFieldIndex(_32b){
try{
this.m_DataFieldIndex=parseInt(_32b);
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetCombo=TextBox_GetCombo;
function TextBox_GetCombo(){
try{
return this.m_Combo;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetCombo=TextBox_SetCombo;
function TextBox_SetCombo(_32c){
try{
this.m_Combo=_32c;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetSearchTerm=TextBox_GetSearchTerm;
function TextBox_GetSearchTerm(){
try{
return this.m_SearchTerm;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetSearchTerm=TextBox_SetSearchTerm;
function TextBox_SetSearchTerm(_32d){
try{
this.m_SearchTerm=_32d;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetIndexSearchTerm=TextBox_GetIndexSearchTerm;
function TextBox_GetIndexSearchTerm(){
try{
return this.m_IndexSearchTerm;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetIndexSearchTerm=TextBox_SetIndexSearchTerm;
function TextBox_SetIndexSearchTerm(_32e){
try{
this.m_IndexSearchTerm=_32e;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.OnChanged=TextBox_OnChanged;
function TextBox_OnChanged(e){
try{
this.m_skipBlur=true;
var _330=this.GetCombo();
var list=_330.GetList();
list.SetActiveRow(null);
var _332=this.GetValue();
this.m_TextValueTag.value=_332;
var _333=this.GetSearchTerm();
if(_330.mode=="smartsearch"||_330.mode=="smartlist"||_330.mode=="filter"||_330.mode=="compact"){
list.GetXmlDataSource().m_Dirty=true;
}
if(_330.mode=="smartlist"){
var lio=_332.lastIndexOf(_330.SmartListSeparator);
if(lio>-1){
_332=_332.substring(lio+_330.SmartListSeparator.length).replace(/^\s+/,"");
}
}
if((_333.indexOf(_332)==0&&_333!=_332)){
list.GetXmlDataSource().m_Dirty=true;
}
this.SetSearchTerm(_332);
if(e!=null){
this.prevKeyCode=e.keyCode;
}
var dfi=this.GetDataFieldIndex();
var This=this;
var _337=(e!=null?e.keyCode:0);
this.m_CurrentKeyCode=_337;
this.m_List=list;
this.m_Event=e;
this.m_Callback=_EbaTextboxCallback;
this.m_skipBlur=false;
this.m_List.Search(_332,dfi,this.m_Callback);
}
catch(err){
}
}
function _EbaTextboxCallback(_338,list){
try{
var _33a=list.GetCombo();
var tb=_33a.GetTextBox();
var e=tb.m_Event;
var _33d=tb.m_CurrentKeyCode;
list.SetSelectedRowValues(null);
list.SetSelectedRowIndex(-1);
var _33e=tb.GetSearchTerm();
var tb=list.GetCombo().GetTextBox();
var row=null;
if(_338>-1){
var _340="EBAComboBoxRow"+_33a.GetUniqueId()+"_"+_338;
row=document.getElementById(_340);
if(""!=tb.searchValue&&(null==e||(_33d!=46&&_33d!=8))&&(null!=e||(tb.prevKeyCode!=46&&tb.prevKeyCode!=8))&&_33a.mode!="smartlist"&&_33a.mode!="smartsearch"){
tb.TypeAhead(list.GetXmlDataSource().GetRowCol(_338,tb.GetDataFieldIndex()),tb.GetSearchTerm().length,tb.GetSearchTerm());
list.SetSelectedRow(_338);
}
list.SetActiveRow(row);
}
if(e!=null&&_338>-1&&list.InitialSearchOnce!=true){
list.Show();
list.ScrollIntoView(row,true);
}
tb.m_skipBlur=false;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.TypeAhead=TextBox_TypeAhead;
function TextBox_TypeAhead(txt){
var t=this.m_HTMLTagObject;
var x=xbGetCurPos(t);
if(txt.toLowerCase().indexOf(t.value.toLowerCase())!=0){
return;
}
this.SetValue(txt);
xbHighlight(t,x);
}
nitobi.combo.TextBox.prototype.OnMouseOver=TextBox_OnMouseOver;
function TextBox_OnMouseOver(_344){
try{
if(this.GetCombo().GetEnabled()){
if(this.GetHeight()!="100%"){
nitobi.html.Css.swapClass(this.GetHTMLContainerObject(),"ComboBoxTextDynamic","ComboBoxTextDynamicOver");
nitobi.html.Css.addClass(this.m_HTMLTagObject,"ComboBoxInputDynamic");
}
if(_344){
var b=this.GetCombo().GetButton();
if(null!=b){
b.OnMouseOver(null,false);
}
}
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.OnMouseOut=TextBox_OnMouseOut;
function TextBox_OnMouseOut(_346){
try{
if(this.GetCombo().GetEnabled()){
if(this.GetHeight()!="100%"){
nitobi.html.Css.swapClass(this.GetHTMLContainerObject(),"ComboBoxTextDynamicOver","ComboBoxTextDynamic");
nitobi.html.Css.removeClass(this.m_HTMLTagObject,"ComboBoxInputDynamic");
}
if(_346){
var b=this.GetCombo().GetButton();
if(null!=b){
b.OnMouseOut(null,false);
}
}
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.OnClick=TextBox_OnClick;
function TextBox_OnClick(){
try{
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.ToggleHidden=TextBox_ToggleHidden;
function TextBox_ToggleHidden(){
try{
this.m_ToggleHidden=true;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.ToggleShow=TextBox_ToggleShow;
function TextBox_ToggleShow(){
try{
this.m_ToggleShow=true;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.Render=TextBox_Render;
function TextBox_Render(){
try{
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetHTMLRenderString=TextBox_GetHTMLRenderString;
function TextBox_GetHTMLRenderString(){
try{
var c=this.GetCombo();
var _349=c.GetId();
var _34a=this.GetValue().replace(/\'/g,"&#39;").replace(/\"/g,"&quot;");
var w=this.GetWidth();
var h=this.GetHeight();
var _34d=c.mode=="smartlist";
var html="";
var _34f;
_34f=(null!=w&&""!=w?"width:"+w+";":"")+(null!=h&&""!=h?"height:"+h+";":"");
html+="<div id=\"EBAComboBoxTextContainer"+this.GetCombo().GetUniqueId()+"\" class=\"ComboBoxTextContainer ComboBoxTextDynamic\""+(this.hasButton?" style=\"border-right:0px solid white;\"":"")+">";
if(_34d&&nitobi.browser.IE){
html+="<span style='"+_34f+"'>";
_34f="width:100%;height:"+h+";";
}
html+="<"+(_34d==true?"textarea":"input")+" id=\"EBAComboBoxText"+_349+"\" name=\"EBAComboBoxText"+_349+"\" type=\"TEXT\" class='"+this.GetCSSClassName()+"' contentEditable='"+this.GetEditable()+"' "+(this.GetEditable().toString().toLowerCase()=="true"?"":"readonly='true'")+" AUTOCOMPLETE='OFF' value='"+_34a+"'  "+"style=\""+_34f+"\" "+"onblur='var combo=window.document.getElementById(\""+_349+"\").object; if(!(combo.m_Over || combo.GetList().m_skipBlur)) window.document.getElementById(\""+_349+"\").object.GetTextBox().OnBlur(event)' "+"onkeyup='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnKeyOperation(event,0)' "+"onkeypress='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnKeyOperation(event,1)' "+"onkeydown='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnKeyOperation(event,2)' "+"onmouseover='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnMouseOver(true)' "+"onmouseout='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnMouseOut(true)' "+"onpaste='window.setTimeout(\"window.document.getElementById(\\\""+_349+"\\\").object.GetTextBox().OnChanged()\",0)' "+"oninput='window.setTimeout(\"window.document.getElementById(\\\""+_349+"\\\").object.GetTextBox().OnChanged()\",0)' "+"onfocus='window.document.getElementById(\""+_349+"\").object.GetTextBox().OnFocus()' "+"tabindex='"+c.GetTabIndex()+"'>"+(_34d==true?_34a:"")+"</"+(_34d==true?"textarea>":"input>")+"<input id=\"EBAComboBoxTextValue"+_349+"\" name=\""+_349+"\" type=\"HIDDEN\" value=\""+_34a+"\">";
html+="</div>";
if(_34d&&nitobi.browser.IE){
html+="</span>";
}
return html;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.Initialize=TextBox_Initialize;
function TextBox_Initialize(){
try{
this.m_ToggleHidden=false;
this.m_ToggleShow=false;
this.focused=false;
this.m_skipBlur=false;
this.m_skipFocusOnce=false;
this.prevKeyCode=-1;
this.skipKeyUp=false;
this.SetHTMLTagObject(document.getElementById("EBAComboBoxText"+this.GetCombo().GetId()));
this.m_TextValueTag=document.getElementById("EBAComboBoxTextValue"+this.GetCombo().GetId());
if(!this.GetCombo().GetEnabled()){
this.Disable();
}
this.m_userTag=null;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.Disable=TextBox_Disable;
function TextBox_Disable(){
nitobi.html.Css.swapClass(this.GetHTMLContainerObject(),"ComboBoxTextContainer","ComboBoxTextContainerDisabled");
nitobi.html.Css.addClass(this.m_HTMLTagObject,"ComboBoxInputDisabled");
this.m_HTMLTagObject.disabled=true;
}
nitobi.combo.TextBox.prototype.Enable=TextBox_Enable;
function TextBox_Enable(){
nitobi.html.Css.swapClass(this.GetHTMLContainerObject(),"ComboBoxTextContainerDisabled","ComboBoxTextContainer");
nitobi.html.Css.removeClass(this.m_HTMLTagObject,"ComboBoxInputDisabled");
this.m_HTMLTagObject.disabled=false;
}
nitobi.combo.TextBox.prototype.OnBlur=TextBox_OnBlur;
function TextBox_OnBlur(e){
try{
var _351=this.GetCombo();
var list=_351.GetList();
if(this.m_skipBlur||_351.m_Over){
return;
}
this.focused=false;
list.Hide();
eval(_351.GetOnBlurEvent());
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.OnFocus=TextBox_OnFocus;
function TextBox_OnFocus(){
try{
if(this.m_skipBlur||this.m_skipFocusOnce){
this.m_skipFocusOnce=false;
return;
}
this.focused=true;
var _353;
_353=this.GetCombo().GetList().IsVisible();
if(!_353||this.m_ToggleShow){
this.m_ToggleShow=false;
if(this.m_ToggleHidden){
this.m_ToggleHidden=false;
}else{
eval(this.GetCombo().GetOnFocusEvent());
}
}
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.SetOnEditKeyUpEvent=TextBox_SetOnEditKeyUpEvent;
function TextBox_SetOnEditKeyUpEvent(_354){
try{
this.m_OnEditKeyUpEvent=_354;
}
catch(err){
}
}
nitobi.combo.TextBox.prototype.GetOnEditKeyUpEvent=TextBox_GetOnEditKeyUpEvent;
function TextBox_GetOnEditKeyUpEvent(){
try{
return this.m_OnEditKeyUpEvent;
}
catch(err){
}
}
function TextBox_CancelBubble(e){
if(nitobi.browser.IE){
e.cancelBubble=true;
e.returnValue=false;
}else{
if(nitobi.browser.MOZ){
e.stopPropagation();
e.preventDefault();
}
}
}
nitobi.combo.TextBox.prototype.OnKeyOperation=TextBox_OnKeyOperation;
function TextBox_OnKeyOperation(e,_357){
if(this.GetEditable()=="false"){
return;
}
e=e?e:window.event;
try{
var _358=0;
var _359=1;
var _35a=2;
var _35b=13;
var _35c=27;
var _35d=9;
var _35e=65;
var _35f=90;
var _360=48;
var _361=57;
var _362=40;
var _363=38;
var _364=46;
var _365=8;
var _366=32;
var _367=96;
var _368=105;
var _369=36;
var _36a=35;
var _36b=37;
var _36c=39;
var _36d=112;
var _36e=123;
var _36f=16;
var _370=17;
var _371=18;
var _372=33;
var _373=34;
var t=this.m_HTMLTagObject;
var _375=this.GetCombo();
var list=_375.GetList();
var _377=e.keyCode;
_375.SetEventObject(e);
var dfi=this.GetDataFieldIndex();
switch(_357){
case (_358):
if(_35b!=_377&&_35c!=_377&&_35d!=_377&&(_377<_372||_377>_362)&&(_377<_36d||_377>_36e)&&(_377<_36f||_377>_371)){
if(_375.mode=="smartsearch"||_375.mode=="smartlist"||_375.mode=="filter"||_375.mode=="compact"){
list.GetXmlDataSource().m_Dirty=true;
}
this.OnChanged(e);
eval(this.GetOnEditKeyUpEvent());
}
if(_377==_363||_377==_362||_377==_372||_377==_373||_377==_35b){
if(this.smartlistWA==true){
this.smartlistWA=false;
}else{
if(nitobi.browser.IE){
t.value=t.value;
}else{
xbPutCur(t,t.value.length);
}
}
}
if(_375.mode=="smartlist"&&_377==_35b&&list.GetActiveRow()!=null){
this.SetValue(list.GetSelectedRowValues()[this.GetDataFieldIndex()],true);
list.SetActiveRow(null);
}
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(this.lio!=lio){
list.Hide();
}
this.lio=lio;
}
break;
case (_35a):
switch(_377){
case (_35b):
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(lio!=this.lio){
list.Hide();
break;
}
}
this.m_skipBlur=true;
list.SetActiveRowAsSelected();
list.Hide();
t.focus();
eval(_375.GetOnSelectEvent());
TextBox_CancelBubble(e);
this.m_skipBlur=false;
break;
case (_35d):
list.Hide();
eval(_375.GetOnTabEvent());
if(this.m_skipBlur||_375.m_Over){
this.m_skipBlur=false;
_375.m_Over=false;
}
list.SetActiveRowAsSelected();
eval(_375.GetOnSelectEvent());
break;
case (_35c):
list.Hide();
break;
case (_363):
if(this.Paging==true){
break;
}
var _37a;
_37a=list.IsVisible();
if(_375.mode=="smartlist"&&!_37a){
this.smartlistWA=true;
break;
}
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(lio!=this.lio){
list.Hide();
break;
}
}
this.m_skipBlur=true;
this.cursor=xbGetCurPos(t);
if(true==list.Move(EBAMoveAction_UP)){
t.focus();
this.SetValue(list.GetXmlDataSource().GetRowCol(list.GetRowIndex(list.GetActiveRow()),dfi));
}
this.m_skipBlur=false;
break;
case (_362):
if(this.Paging==true){
break;
}
var _37a;
_37a=list.IsVisible();
if(_375.mode=="smartlist"&&!_37a){
this.smartlistWA=true;
break;
}
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(lio!=this.lio){
list.Hide();
break;
}
}
this.m_skipBlur=true;
this.cursor=xbGetCurPos(t);
var r=list.GetActiveRow();
if(null!=r&&list.GetRowIndex(r)==list.GetXmlDataSource().GetNumberRows()-1&&true==list.GetAllowPaging()&&_375.mode=="default"){
list.SetActiveRow(null);
this.Paging=true;
list.OnGetNextPage(EBAScrollToNewBottom,true);
}else{
if(true==list.Move(EBAMoveAction_DOWN)){
t.focus();
this.SetValue(list.GetXmlDataSource().GetRowCol(list.GetRowIndex(list.GetActiveRow()),dfi));
}
}
this.m_skipBlur=false;
break;
case (_372):
if(this.Paging==true){
break;
}
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(lio!=this.lio){
list.Hide();
break;
}
}
this.m_skipBlur=true;
var b=nitobi.Browser;
var lb=list.GetSectionHTMLTagObject(EBAComboBoxListBody);
var _37a;
_37a=list.IsVisible();
if(_37a){
var r=list.GetActiveRow()||list.GetRow(0);
if(null!=r){
var idx=list.GetRowIndex(r);
while(0!=idx){
r=list.GetRow(--idx);
if(!b.IsObjectInView(r,lb)){
break;
}
}
b.ScrollIntoView(r,lb,false,true);
list.SetActiveRow(r);
this.SetValue(list.GetXmlDataSource().GetRowCol(idx,dfi));
}
}
this.m_skipBlur=false;
break;
case (_373):
if(this.Paging==true){
break;
}
if(_375.mode=="smartlist"){
var lio=t.value.lastIndexOf(_375.SmartListSeparator);
if(lio!=this.lio){
list.Hide();
break;
}
}
var _37a;
_37a=list.IsVisible();
if(!_37a){
if(_375.mode!="smartlist"){
list.Show();
}
}else{
this.m_skipBlur=true;
var b=nitobi.Browser;
var lb=list.GetSectionHTMLTagObject(EBAComboBoxListBody);
var r=list.GetActiveRow()||list.GetRow(0);
var idx=list.GetRowIndex(r);
var end=list.GetXmlDataSource().GetNumberRows()-1;
while(idx!=end){
r=list.GetRow(++idx);
if(!b.IsObjectInView(r,lb)){
break;
}
}
if(idx==end&&true==list.GetAllowPaging()&&_375.mode=="default"){
list.SetActiveRow(null);
this.Paging=true;
list.OnGetNextPage(EBAScrollToNewTop,true);
}else{
b.ScrollIntoView(r,lb,true,false);
list.SetActiveRow(r);
this.SetValue(list.GetXmlDataSource().GetRowCol(idx,dfi));
}
this.m_skipBlur=false;
}
break;
default:
}
break;
case (_359):
if(_377==_35b){
TextBox_CancelBubble(e);
}
break;
default:
}
_375.SetEventObject(null);
}
catch(err){
}
}
nitobi.prepare=function(){
ebagdl=0;
ebagd1=9999999999999;
s="var d = new Date().getTime();if ((d<"+ebagdl+") || (d>"+ebagd1+")) {alert('Evaluation period has expired.\\n\\nPlease notify your system administrator.\\n\\nPurchase Information:\\n       NITOBI SOFTWARE\\n\\n       www.nitobi.com\\n       sales@nitobi.com         \\n       Telephone: (604) 685-9287\\n       Fax: (604) 648-9090\\n       Toll-Free: 1-866-6EB-APPS\\n                      (1-866-632-2777)');}";
eval(s);
};
if(typeof (nitobi)=="undefined"){
nitobi={};
}
function xbDOM(){
}
nitobi.lang.defineNs("nitobi.browser");
if(nitobi.browser.MOZ){
Document.prototype.loadXML=_Document_loadXML;
Node.prototype.__defineGetter__("xml",_Node_getXML);
Document.prototype.readyState=0;
Document.prototype.__load__=Document.prototype.load;
Document.prototype.load=_Document_load;
Document.prototype.onreadystatechange=null;
Node.prototype._uniqueID=null;
Node.prototype.__defineGetter__("uniqueID",_Node_getUniqueID);
XMLDocument.prototype.transformNode=_XMLDocument_transformNode;
XMLDocument.prototype.transformNodeToObject=_XMLDocument_transformNodeToObject;
}
function _Document_loadXML(_380){
changeReadyState(this,1);
var p=new DOMParser();
var d=p.parseFromString(_380,"text/xml");
while(this.hasChildNodes()){
this.removeChild(this.lastChild);
}
for(var i=0;i<d.childNodes.length;i++){
this.appendChild(this.importNode(d.childNodes[i],true));
}
changeReadyState(this,4);
}
function _Node_getXML(){
return new XMLSerializer().serializeToString(this);
}
function _Document_load(_384){
changeReadyState(this,1);
try{
this.__load__(_384);
}
catch(e){
changeReadyState(this,4);
}
}
function changeReadyState(oDOM,_386){
oDOM.readyState=_386;
if(oDOM.onreadystatechange!=null&&(typeof oDOM.onreadystatechange)=="function"){
oDOM.onreadystatechange();
}
}
_Node_getUniqueID.i=1;
function _Node_getUniqueID(){
if(null==this._uniqueID){
this._uniqueID="mz__id"+_Node_getUniqueID.i++;
}
return this._uniqueID;
}
function EbaConstructValidXpathQuery(_387,_388){
try{
var _389=_387.match(/(\"|\')/g);
if(_389!=null){
var _38a="concat(";
var _38b="";
var _38c;
for(var i=0;i<_387.length;i++){
if(_387.substr(i,1)=="\""){
_38c="&apos;";
}else{
_38c="&quot;";
}
_38a+=_38b+_38c+EbaXmlEncode(_387.substr(i,1))+_38c;
_38b=",";
}
_38a+=_38b+"&apos;&apos;";
_38a+=")";
_387=_38a;
}else{
var quot=(_388?"\"":"");
_387=quot+EbaXmlEncode(_387)+quot;
}
return _387;
}
catch(err){
}
}
function _XMLDocument_transformNode(_38f){
var xs=new XMLSerializer();
var d=xbDOM.create();
this.transformNodeToObject(_38f,d);
if(d.childNodes.length>0&&d.childNodes[0].tagName=="transformiix:result"){
return d.childNodes[0].textContent;
}
return nitobi.Browser.HTMLUnencode(xs.serializeToString(d));
}
function _XMLDocument_transformNodeToObject(_392,_393){
var p=new XSLTProcessor();
p.importStylesheet(_392);
var f=p.transformToFragment(this,_393);
while(_393.hasChildNodes()){
_393.removeChild(_393.firstChild);
}
if(_392.xml.match(/<xsl:output method=("text"|'text')/)){
var n=this.createElement("transformiix:result");
n.appendChild(f.childNodes[0]);
_393.appendChild(n);
return;
}
var d=xbDOM.create();
d.loadXML(f.xml);
f=d;
var cn=f.childNodes;
for(var i=0;i<cn.length;i++){
_393.appendChild(this.importNode(cn[i],true));
}
}
function EbaXmlEncode(str){
str=str.replace(/&/g,"&amp;");
str=str.replace(/'/g,"&apos;");
str=str.replace(/\"/g,"&quot;");
str=str.replace(/</g,"&lt;");
str=str.replace(/>/,"&gt;");
return str;
}
function XmlDataIslands(){
}
AX=["Msxml4.DOMDocument","Msxml3.DOMDocument","Msxml2.DOMDocument","Msxml.DOMDocument","Microsoft.XmlDom"];
xbDOM.create=xbDOM_create;
function xbDOM_create(_39b,_39c){
var d=null;
if(nitobi.browser.MOZ){
d=window.document.implementation.createDocument(_39b,_39c,null);
d.addEventListener("load",function(){
changeReadyState(d,4);
},false);
}else{
for(var i=0;!d&&i<AX.length;i++){
try{
d=new ActiveXObject(AX[i]);
}
catch(e){
}
}
if(_39c){
if(_39b){
d.loadXML("<a0:"+_39c+" xmlns:a0=\""+_39b+"\" />");
}else{
d.loadXML("<"+_39c+"/>");
}
}
}
return d;
}
function xbXMLHTTP(){
}
xbXMLHTTP.create=xbXMLHTTP_create;
function xbXMLHTTP_create(){
var x=null;
try{
x=new ActiveXObject("Msxml2.XMLHTTP");
}
catch(e){
try{
x=new ActiveXObject("Microsoft.XMLHTTP");
}
catch(oc){
x=null;
}
}
if(!x&&(typeof XMLHttpRequest)!="undefined"){
x=new XMLHttpRequest();
}
return x;
}
function xbGetCurPos(o){
if(o.createTextRange){
o.focus();
var r=document.selection.createRange().duplicate();
r.moveEnd("textedit",1);
return o.value.length-r.text.length;
}else{
if(o.setSelectionRange){
return o.selectionStart;
}
}
return -1;
}
function xbPutCur(o,x){
if(o.createTextRange){
o.focus();
var r=document.selection.createRange().duplicate();
r.move("character",0-o.value.length);
r.move("character",x);
r.select();
}else{
if(o.setSelectionRange){
o.setSelectionRange(x,x);
}
}
}
function xbHighlight(o,x){
if(o.createTextRange){
o.focus();
var r=document.selection.createRange().duplicate();
r.move("character",0-o.value.length);
r.move("character",x);
r.moveEnd("textedit",1);
r.select();
}else{
if(o.setSelectionRange){
o.setSelectionRange(x,o.value.length);
}
}
}
function xbClipXml(oXml,_3a9,_3aa,_3ab){
var xsl="<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'><xsl:template match='"+_3a9+"'><xsl:copy><xsl:copy-of select='@*'></xsl:copy-of><xsl:apply-templates select='"+_3aa+"'></xsl:apply-templates></xsl:copy></xsl:template><xsl:template match='"+_3aa+"'><xsl:choose><xsl:when test='position()&lt;="+_3ab+"'><xsl:copy-of select='.'></xsl:copy-of></xsl:when></xsl:choose></xsl:template></xsl:stylesheet>";
var x=xbDOM.create();
x.loadXML(xsl);
var newx=xbDOM.create();
oXml.transformNodeToObject(x,newx);
return newx;
}
if(!nitobi.browser.IE){
Document.prototype.createStyleSheet=function(){
var _3af=this.createElement("style");
this.documentElement.childNodes[0].appendChild(_3af);
return _3af;
};
HTMLStyleElement.prototype.__defineSetter__("cssText",function(_3b0){
this.innerHTML=_3b0;
});
HTMLElement.prototype.getBoundingClientRect=function(_3b1,_3b2){
var td=document.getBoxObjectFor(this);
return {top:td.y,left:td.x,bottom:(td.y+td.height),right:(td.x+td.width)};
};
}
if(typeof (Eba)=="undefined"){
Eba={};
}
if(typeof (Eba.Dom)=="undefined"){
Eba.Dom={};
}
nitobi.Browser.ConvertXmlDataIsland=function(_3b4,_3b5){
if(null!=_3b4&&""!=_3b4){
var xmls=window.document.getElementById(_3b4);
if(null!=xmls){
var id=xmls.getAttribute("id");
var src=xmls.getAttribute("src");
var d=xbDOM.create();
if(null==src){
d.loadXML(this.EncodeAngleBracketsInTagAttributes(xmls.innerHTML.replace(/>\s+</g,"><"),_3b5));
}else{
d.async=false;
var _3ba=nitobi.Browser.LoadPageFromUrl(src,_3b5.GetHttpRequestMethod());
var _3bb=_3ba.indexOf("<?xml");
if(_3bb!=-1){
d.loadXML(_3ba.substr(_3bb));
}else{
d.loadXML(_3ba);
}
var d2=xbDOM.create();
d2.loadXML(this.EncodeAngleBracketsInTagAttributes(d.xml.replace(/>\s+</g,"><"),_3b5));
d=d2;
}
eval("window.document."+id+"=d;");
var p=(xmls.parentNode?xmls.parentNode:xmls.parentElement);
p.removeChild(xmls);
}
}
};
nitobi.lang.defineNs("nitobi.combo");
nitobi.combo.XmlDataSource=function(_3be,clip,_3c0,_3c1){
try{
this.combo=null;
this.m_Dirty=null;
this.m_LowerCaseXml=null;
if(nitobi.browser.MOZ){
this.m_LowerCaseXml=xbDOM.create();
}
if(_3be!=null){
this.combo=_3c1;
var x=(_3be?_3be.getAttribute("XmlId"):"");
this.SetXmlId(x);
var _3c3=document.getElementById(x);
if(nitobi.browser.MOZ||null==_3c3){
nitobi.Browser.ConvertXmlDataIsland(x,_3c1);
this.SetXmlObject(eval("window.document."+x),clip,_3c0);
}else{
this.SetXmlObject(_3c3);
}
this.SetLastPageSize(this.GetNumberRows());
this.m_Dirty=false;
}else{
this.m_Dirty=true;
this.SetLastPageSize(0);
this.SetNumberColumns(0);
}
}
catch(err){
}
};
nitobi.combo.XmlDataSource.prototype.GetXmlId=XmlDataSource_GetXmlId;
function XmlDataSource_GetXmlId(){
try{
return this.m_XmlId;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.SetXmlId=XmlDataSource_SetXmlId;
function XmlDataSource_SetXmlId(_3c4){
try{
this.m_XmlId=_3c4;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetXmlObject=XmlDataSource_GetXmlObject;
function XmlDataSource_GetXmlObject(){
try{
return this.m_XmlObject;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.SetXmlObject=XmlDataSource_SetXmlObject;
function XmlDataSource_SetXmlObject(_3c5,clip,_3c7){
try{
if(null==_3c5.documentElement){
return;
}
if(nitobi.browser.MOZ){
var d=xbDOM.create();
var xml=_3c5.xml.replace(/>\s+</g,"><");
d.loadXML(xml);
_3c5=d;
this.m_LowerCaseXml.loadXML(xml.toLowerCase());
}
if(clip==true){
_3c5=xbClipXml(_3c5,"root","e",_3c7);
}
this.m_XmlObject=_3c5;
this.SetLastPageSize(this.GetNumberRows());
var _3ca=_3c5.documentElement.getAttribute("fields");
if(null==_3ca){
}else{
var _3cb=_3ca.split("|");
this.SetColumnNames(_3cb);
this.SetNumberColumns(_3cb.length);
}
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetNumberRows=XmlDataSource_GetNumberRows;
function XmlDataSource_GetNumberRows(){
try{
var _3cc=this.GetXmlObject().selectNodes("//e").length;
return parseInt(_3cc);
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetLastPageSize=XmlDataSource_GetLastPageSize;
function XmlDataSource_GetLastPageSize(){
try{
return this.m_LastPageSize;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.SetLastPageSize=XmlDataSource_SetLastPageSize;
function XmlDataSource_SetLastPageSize(_3cd){
try{
this.m_LastPageSize=_3cd;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetNumberColumns=XmlDataSource_GetNumberColumns;
function XmlDataSource_GetNumberColumns(){
try{
return this.m_NumberColumns;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.SetNumberColumns=XmlDataSource_SetNumberColumns;
function XmlDataSource_SetNumberColumns(_3ce){
try{
this.m_NumberColumns=parseInt(_3ce);
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetColumnNames=XmlDataSource_GetColumnNames;
function XmlDataSource_GetColumnNames(){
try{
return this.m_ColumnNames;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.SetColumnNames=XmlDataSource_SetColumnNames;
function XmlDataSource_SetColumnNames(_3cf){
try{
this.m_ColumnNames=_3cf;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.Search=XmlDataSource_Search;
function XmlDataSource_Search(_3d0,_3d1,_3d2){
try{
_3d0=_3d0.toLowerCase();
_3d0=EbaConstructValidXpathQuery(_3d0,true);
var xsl;
var xsl;
if(nitobi.browser.IE){
xsl="<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" xmlns:jstring=\"http://www.ebusiness-apps.com/comboxsl\"  extension-element-prefixes=\"msxsl\" exclude-result-prefixes=\"jstring\">";
}else{
xsl="<xsl:stylesheet xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" version=\"1.0\"  >";
}
xsl+="<xsl:output method=\"text\" version=\"4.0\"/>";
if(nitobi.browser.IE){
xsl+="<msxsl:script language=\"javascript\" implements-prefix=\"jstring\">"+"<![CDATA["+"f"+"unction lowerCase(s) "+"{"+"\treturn s.toLowerCase();"+"}"+"]]>"+"</msxsl:script>";
}
xsl+="<xsl:template match=\"/\">"+"<xsl:apply-templates/>"+"</xsl:template>";
if(nitobi.browser.IE){
xsl+="<xsl:template match='//e["+(_3d2==true?"contains":"starts-with")+"(jstring:lowerCase(string(@"+String.fromCharCode(97+parseInt(_3d1))+")),"+_3d0+")][1]'>";
}else{
xsl+="<xsl:template match='//e["+(_3d2==true?"contains":"starts-with")+"(@"+String.fromCharCode(97+parseInt(_3d1))+","+_3d0+")][1]'>";
}
xsl+="<xsl:value-of select='position()-1' />"+"</xsl:template>"+"</xsl:stylesheet>";
var oXSL=xbDOM.create();
oXSL.loadXML(xsl);
var _3d5=oXSL.documentElement;
if((_3d5.tagName.toLowerCase()=="parsererror")||(_3d5.namespaceURI=="http://www.mozilla.org/newlayout/xml/parsererror.xml")){
parseError=new XMLSerializer().serializeToString(oXSL);
}
var _3d6;
if(nitobi.browser.IE){
_3d6=this.GetXmlObject();
}else{
_3d6=this.m_LowerCaseXml;
}
var _3d7=_3d6.transformNode(oXSL);
if(""==_3d7){
_3d7=-1;
}
return parseInt(_3d7);
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.AddPage=XmlDataSource_AddPage;
function XmlDataSource_AddPage(XML){
try{
var _3d9;
var _3da;
if(nitobi.browser.MOZ){
var _3db;
_3db=xbDOM.create();
_3db.loadXML(XML.toLowerCase());
_3da=_3db.selectNodes("//e");
_3d9=this.m_LowerCaseXml.documentElement;
}
var tmp=xbDOM.create();
tmp.loadXML(XML);
var _3dd=tmp.selectNodes("//e");
var root=this.GetXmlObject().documentElement;
this.SetLastPageSize(tmp.selectNodes("//e").length);
for(var i=0;i<_3dd.length;i++){
root.appendChild(_3dd[i]);
if(nitobi.browser.MOZ){
_3d9.appendChild(_3da[i]);
}
}
this.m_Dirty=false;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.Clear=XmlDataSource_Clear;
function XmlDataSource_Clear(){
try{
this.GetXmlObject().loadXML("<root/>");
if(nitobi.browser.MOZ){
this.m_LowerCaseXml.loadXML("<root/>");
}
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetRow=XmlDataSource_GetRow;
function XmlDataSource_GetRow(_3e0){
try{
_3e0=parseInt(_3e0);
var row=this.GetXmlObject().documentElement.childNodes.item(_3e0);
var _3e2=new Array;
for(var i=0;i<this.GetNumberColumns();i++){
_3e2[i]=row.getAttribute(String.fromCharCode(97+i));
}
return _3e2;
}
catch(err){
}
}
nitobi.combo.XmlDataSource.prototype.GetRowCol=XmlDataSource_GetRowCol;
function XmlDataSource_GetRowCol(Row,Col){
try{
var row=this.GetXmlObject().documentElement.childNodes.item(parseInt(Row));
var val=row.getAttribute(String.fromCharCode(97+parseInt(Col)));
return val;
}
catch(err){
return "";
}
}
nitobi.combo.XmlDataSource.prototype.GetColumnIndex=XmlDataSource_GetColumnIndex;
function XmlDataSource_GetColumnIndex(Name){
try{
if(Name==null){
return 0;
}
Name=Name.toLowerCase();
var _3e9=this.GetColumnNames();
if(_3e9!=null){
for(var i=0;i<_3e9.length;i++){
if(Name==_3e9[i].toLowerCase()){
return parseInt(i);
}
}
}
return -1;
}
catch(err){
}
}


