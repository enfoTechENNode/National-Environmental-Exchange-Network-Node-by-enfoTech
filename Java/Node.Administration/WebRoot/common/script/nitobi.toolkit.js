if(typeof (nitobi)=="undefined"){
nitobi=function(){
};
}
if(false){
nitobi.lang=function(){
};
}
if(typeof (nitobi.lang)=="undefined"){
nitobi.lang={};
}
nitobi.lang.defineNs=function(_1){
var _2=_1.split(".");
var _3="";
var _4="";
for(var i=0;i<_2.length;i++){
_3+=_4+_2[i];
_4=".";
if(eval("typeof("+_3+")")=="undefined"){
eval(_3+"={}");
}
}
};
nitobi.lang.extend=function(_6,_7){
function inheritance(){
}
inheritance.prototype=_7.prototype;
_6.prototype=new inheritance();
_6.prototype.constructor=_6;
_6.baseConstructor=_7;
if(_7.base){
_7.prototype.base=_7.base;
}
_6.base=_7.prototype;
};
nitobi.lang.implement=function(_8,_9){
if(typeof (_9)=="undefined"||_9==null){
var _a="nitobi.lang.implement argument interface_ is null or undefined.  The most likely cause of this is that a js file has not been included, or has been included in the wrong order.";
nitobi.lang.throwError(_a);
}
for(var _b in _9.prototype){
if(typeof (_8.prototype[_b])=="undefined"||_8.prototype[_b]==null){
_8.prototype[_b]=_9.prototype[_b];
}
}
};
nitobi.lang.isDefined=function(a){
return (typeof (a)!="undefined");
};
nitobi.lang.getBool=function(a){
if(null==a){
return null;
}
if(typeof (a)=="boolean"){
return a;
}
return a.toLowerCase()=="true";
};
nitobi.lang.type={XMLNODE:0,HTMLNODE:1,ARRAY:2,XMLDOC:3};
nitobi.lang.typeOf=function(_e){
var t=typeof (_e);
if(t=="object"){
if(_e.blur){
return nitobi.lang.type.HTMLNODE;
}
if(_e.nodeName&&_e.nodeName.toLowerCase()==="#document"){
return nitobi.lang.type.XMLDOC;
}
if(_e.nodeName){
return nitobi.lang.type.XMLNODE;
}
if(_e instanceof Array){
return nitobi.lang.type.ARRAY;
}
}
return t;
};
nitobi.lang.toBool=function(_10,_11){
if(typeof (_11)!="undefined"){
if((typeof (_10)=="undefined")||(_10=="")||(_10==null)){
_10=_11;
}
}
_10=_10.toString()||"";
_10=_10.toUpperCase();
if((_10=="Y")||(_10=="1")||(_10=="TRUE")){
return true;
}else{
return false;
}
};
nitobi.lang.boolToStr=function(_12){
if(typeof (_12)=="boolean"){
if(_12){
return "1";
}else{
return "0";
}
}else{
return _12;
}
};
nitobi.lang.close=function(_13,_14,_15){
if(null==_15){
return function(){
return _14.apply(_13,arguments);
};
}else{
return function(){
return _14.apply(_13,_15);
};
}
};
nitobi.lang.after=function(_16,_17,_18,_19){
var _1a=_16[_17];
var _1b=_18[_19];
if(_19 instanceof Function){
_1b=_19;
}
_16[_17]=function(){
_1a.apply(_16,arguments);
_1b.apply(_18,arguments);
};
_16[_17].orig=_1a;
};
nitobi.lang.before=function(_1c,_1d,_1e,_1f){
var _20=_1c[_1d];
var _21=function(){
};
if(_1e!=null){
_21=_1e[_1f];
}
if(_1f instanceof Function){
_21=_1f;
}
_1c[_1d]=function(){
_21.apply(_1e,arguments);
_20.apply(_1c,arguments);
};
_1c[_1d].orig=_20;
};
nitobi.lang.forEach=function(arr,_23){
var len=arr.length;
for(var i=0;i<len;i++){
_23.call(this,arr[i],i);
}
_23=null;
};
nitobi.lang.throwError=function(_26,_27){
var msg=_26;
if(_27!=null){
msg+="\n - because "+nitobi.lang.getErrorDescription(_27);
}
throw msg;
};
nitobi.lang.getErrorDescription=function(_29){
var _2a=(typeof (_29.description)=="undefined")?_29:_29.description;
return _2a;
};
nitobi.lang.newObject=function(_2b,_2c,_2d){
var a=_2c;
if(null==_2d){
_2d=0;
}
var e="new "+_2b+"(";
var _30="";
for(var i=_2d;i<a.length;i++){
e+=_30+"a["+i+"]";
_30=",";
}
e+=")";
return eval(e);
};
nitobi.lang.getLastFunctionArgs=function(_32,_33){
var a=new Array(_32.length-_33);
for(var i=_33;i<_32.length;i++){
a[i-_33]=_32[i];
}
return a;
};
nitobi.lang.getFirstHashKey=function(_36){
for(var x in _36){
return x;
}
};
nitobi.lang.getFirstFunction=function(obj){
for(var x in obj){
if(obj[x]!=null&&typeof (obj[x])=="function"&&typeof (obj[x].prototype)!="undefined"){
return {name:x,value:obj[x]};
}
}
return null;
};
nitobi.lang.copy=function(obj){
var _3b={};
for(var _3c in obj){
_3b[_3c]=obj[_3c];
}
return _3b;
};
nitobi.lang.dispose=function(_3d,_3e){
try{
if(_3e!=null){
var _3f=_3e.length;
for(var i=0;i<_3f;i++){
if(typeof (_3e[i].dispose)=="function"){
_3e[i].dispose();
}
if(typeof (_3e[i])=="function"){
_3e[i].call(_3d);
}
_3e[i]=null;
}
}
for(var _41 in _3d){
if(_3d[_41].dispose instanceof Function){
_3d[_41].dispose();
}
_3d[_41]=null;
}
}
catch(e){
}
};
nitobi.lang.parseNumber=function(val){
var num=parseInt(val);
return (isNaN(num)?0:num);
};
nitobi.lang.numToAlpha=function(num){
if(typeof (nitobi.lang.numAlphaCache[num])==="string"){
return nitobi.lang.numAlphaCache[num];
}
var ck1=num%26;
var ck2=Math.floor(num/26);
var _47=(ck2>0?String.fromCharCode(96+ck2):"")+String.fromCharCode(97+ck1);
nitobi.lang.alphaNumCache[_47]=num;
nitobi.lang.numAlphaCache[num]=_47;
return _47;
};
nitobi.lang.alphaToNum=function(_48){
if(typeof (nitobi.lang.alphaNumCache[_48])==="number"){
return nitobi.lang.alphaNumCache[_48];
}
var j=0;
var num=0;
for(var i=_48.length-1;i>=0;i--){
num+=(_48.charCodeAt(i)-96)*Math.pow(26,j++);
}
num=num-1;
nitobi.lang.alphaNumCache[_48]=num;
nitobi.lang.numAlphaCache[num]=_48;
return num;
};
nitobi.lang.alphaNumCache={};
nitobi.lang.numAlphaCache={};
nitobi.lang.toArray=function(obj,_4d){
return Array.prototype.splice.call(obj,_4d||0);
};
nitobi.lang.merge=function(_4e,_4f){
var r={};
for(var i=0;i<arguments.length;i++){
var a=arguments[i];
for(var x in arguments[i]){
r[x]=a[x];
}
}
return r;
};
nitobi.lang.xor=function(){
var b=false;
for(var j=0;j<arguments.length;j++){
if(arguments[j]&&!b){
b=true;
}else{
if(arguments[j]&&b){
return false;
}
}
}
return b;
};
nitobi.lang.zeros="00000000000000000000000000000000000000000000000000000000000000000000";
nitobi.lang.padZeros=function(num,_57){
_57=_57||2;
num=num+"";
return nitobi.lang.zeros.substr(0,Math.max(_57-num.length,0))+num;
};
nitobi.lang.noop=function(){
};
nitobi.lang.defineNs("nitobi.lang");
nitobi.lang.Math=function(){
};
nitobi.lang.Math.sinTable=Array();
nitobi.lang.Math.cosTable=Array();
nitobi.lang.Math.rotateCoords=function(_58,_59,_5a){
var _5b=_5a*0.01745329277777778;
if(nitobi.lang.Math.sinTable[_5b]==null){
nitobi.lang.Math.sinTable[_5b]=Math.sin(_5b);
nitobi.lang.Math.cosTable[_5b]=Math.cos(_5b);
}
var cR=nitobi.lang.Math.cosTable[_5b];
var sR=nitobi.lang.Math.sinTable[_5b];
var x=_58*cR-_59*sR;
var y=_59*cR+_58*sR;
return {x:x,y:y};
};
nitobi.lang.Math.returnAngle=function(_60,_61,_62,_63){
return Math.atan2(_63-_61,_62-_60)/0.01745329277777778;
};
nitobi.lang.Math.returnDistance=function(x1,y1,x2,y2){
return Math.sqrt(((x2-x1)*(x2-x1))+((y2-y1)*(y2-y1)));
};
nitobi.lang.defineNs("nitobi.toolkit");
nitobi.toolkit.build="6257";
nitobi.toolkit.version="1.0.6257";
nitobi.lang.defineNs("nitobi");
nitobi.Object=function(){
this.disposal=new Array();
};
nitobi.Object.prototype.setValues=function(_68){
for(var _69 in _68){
if(this[_69]!=null){
if(this[_69].subscribe!=null){
}else{
this[_69]=_68[_69];
}
}else{
if(this[_69] instanceof Function){
this[_69](_68[_69]);
}else{
if(this["set"+_69] instanceof Function){
this["set"+_69](_68[_69]);
}else{
this[_69]=_68[_69];
}
}
}
}
};
nitobi.Object.prototype.dispose=function(){
if(this.disposing){
return;
}
this.disposing=true;
var _6a=this.disposal.length;
for(var i=0;i<_6a;i++){
if(disposal[i] instanceof Function){
disposal[i].call(context);
}
disposal[i]=null;
}
for(var _6c in this){
if(this[_6c].dispose instanceof Function){
this[_6c].dispose.call(this[_6c]);
}
this[_6c]=null;
}
};
if(false){
nitobi.base=function(){
};
}
nitobi.lang.defineNs("nitobi.base");
nitobi.base.uid=1;
nitobi.base.getUid=function(){
return "ntb__"+(nitobi.base.uid++);
};
nitobi.lang.defineNs("nitobi.browser");
if(false){
nitobi.browser=function(){
};
}
nitobi.browser.UNKNOWN=true;
nitobi.browser.IE=false;
nitobi.browser.IE6=false;
nitobi.browser.IE7=false;
nitobi.browser.MOZ=false;
nitobi.browser.SAFARI=false;
nitobi.browser.OPERA=false;
nitobi.browser.XHR_ENABLED;
nitobi.browser.detect=function(){
var _6d=[{string:navigator.vendor,subString:"Apple",identity:"Safari"},{prop:window.opera,identity:"Opera"},{string:navigator.vendor,subString:"iCab",identity:"iCab"},{string:navigator.vendor,subString:"KDE",identity:"Konqueror"},{string:navigator.userAgent,subString:"Firefox",identity:"Firefox"},{string:navigator.userAgent,subString:"Netscape",identity:"Netscape"},{string:navigator.userAgent,subString:"MSIE",identity:"Explorer",versionSearch:"MSIE"},{string:navigator.userAgent,subString:"Gecko",identity:"Mozilla",versionSearch:"rv"},{string:navigator.userAgent,subString:"Mozilla",identity:"Netscape",versionSearch:"Mozilla"}];
var _6e="Unknown";
for(var i=0;i<_6d.length;i++){
var _70=_6d[i].string;
var _71=_6d[i].prop;
if(_70){
if(_70.indexOf(_6d[i].subString)!=-1){
_6e=_6d[i].identity;
break;
}
}else{
if(_71){
_6e=_6d[i].identity;
break;
}
}
}
nitobi.browser.IE=(_6e=="Explorer");
nitobi.browser.IE6=(nitobi.browser.IE&&!window.XMLHttpRequest);
nitobi.browser.IE7=(nitobi.browser.IE&&window.XMLHttpRequest);
nitobi.browser.MOZ=(_6e=="Netscape"||_6e=="Firefox");
nitobi.browser.SAFARI=(_6e=="Safari");
nitobi.browser.OPERA=(_6e=="Opera");
nitobi.browser.XHR_ENABLED=nitobi.browser.OPERA||nitobi.browser.SAFARI||nitobi.browser.MOZ||nitobi.browser.IE;
nitobi.browser.UNKNOWN=!(nitobi.browser.IE||nitobi.browser.MOZ||nitobi.browser.SAFARI);
};
nitobi.browser.detect();
if(nitobi.browser.IE6){
try{
document.execCommand("BackgroundImageCache",false,true);
}
catch(e){
}
}
nitobi.lang.defineNs("nitobi.browser");
nitobi.browser.Cookies=function(){
};
nitobi.lang.extend(nitobi.browser.Cookies,nitobi.Object);
nitobi.browser.Cookies.get=function(id){
var _73,end;
if(document.cookie.length>0){
_73=document.cookie.indexOf(id+"=");
if(_73!=-1){
_73+=id.length+1;
end=document.cookie.indexOf(";",_73);
if(end==-1){
end=document.cookie.length;
}
return unescape(document.cookie.substring(_73,end));
}
}
return null;
};
nitobi.browser.Cookies.set=function(id,_76,_77){
var _78=new Date();
_78.setTime(_78.getTime()+(_77*24*3600*1000));
document.cookie=id+"="+escape(_76)+((_77==null)?"":"; expires="+_78.toGMTString());
};
nitobi.browser.Cookies.remove=function(id){
if(nitobi.browser.Cookies.get(id)){
document.cookie=id+"="+"; expires=Thu, 01-Jan-70 00:00:01 GMT";
}
};
nitobi.lang.defineNs("nitobi.browser");
nitobi.browser.History=function(){
this.lastPage="";
this.currentPage="";
this.onChange=new nitobi.base.Event();
this.iframeObject=nitobi.html.createElement("iframe",{"name":"ntb_history","id":"ntb_history"},{"display":"none"});
document.body.appendChild(this.iframeObject);
this.iframe=frames["ntb_history"];
this.monitor();
};
nitobi.browser.History.prototype.add=function(_7a){
this.lastPage=this.currentPage=_7a.substr(_7a.indexOf("#")+1);
this.iframe.location.href=_7a;
};
nitobi.browser.History.prototype.monitor=function(){
var _7b=this.iframe.location.href.split("#");
this.currentPage=_7b[1];
if(this.currentPage!=this.lastPage){
this.onChange.notify(_7b[0].substring(_7b[0].lastIndexOf("/")+1),this.currentPage);
this.lastPage=this.currentPage;
}
window.setTimeout(nitobi.lang.close(this,this.monitor),100);
};
nitobi.lang.defineNs("nitobi.xml");
nitobi.xml=function(){
};
nitobi.xml.nsPrefix="ntb:";
nitobi.xml.nsDecl="xmlns:ntb=\"http://www.nitobi.com\"";
if(nitobi.browser.IE){
var inUse=false;
nitobi.xml.XslTemplate=new ActiveXObject("MSXML2.XSLTemplate.3.0");
}
if(nitobi.browser.MOZ){
nitobi.xml.Serializer=new XMLSerializer();
nitobi.xml.DOMParser=new DOMParser();
}
if(!nitobi.browser.IE&&!nitobi.browser.MOZ){
}
nitobi.xml.getChildNodes=function(_7c){
if(nitobi.browser.IE){
return _7c.childNodes;
}else{
return _7c.selectNodes("./*");
}
};
nitobi.xml.indexOfChildNode=function(_7d,_7e){
var _7f=nitobi.xml.getChildNodes(_7d);
for(var i=0;i<_7f.length;i++){
if(_7f[i]==_7e){
return i;
}
}
return -1;
};
nitobi.xml.createXmlDoc=function(xml){
if(xml!=null&&xml.documentElement!=null){
return xml;
}
var doc=null;
if(nitobi.browser.IE){
doc=new ActiveXObject("Msxml2.DOMDocument.3.0");
doc.setProperty("SelectionNamespaces","xmlns:ntb='http://www.nitobi.com'");
}else{
if(nitobi.browser.MOZ){
doc=document.implementation.createDocument("","",null);
}
}
if(xml!=null&&typeof xml=="string"){
doc=nitobi.xml.loadXml(doc,xml);
}
return doc;
};
nitobi.xml.loadXml=function(doc,xml,_85){
doc.async=false;
if(nitobi.browser.IE){
doc.loadXML(xml);
}else{
var _86=nitobi.xml.DOMParser.parseFromString(xml,"text/xml");
if(_85){
while(doc.hasChildNodes()){
doc.removeChild(doc.firstChild);
}
for(var i=0;i<_86.childNodes.length;i++){
doc.appendChild(doc.importNode(_86.childNodes[i],true));
}
}else{
doc=_86;
}
_86=null;
}
return doc;
};
nitobi.xml.hasParseError=function(_88){
if(nitobi.browser.IE){
return (_88.parseError!=0);
}else{
if(_88==null||_88.documentElement==null){
return true;
}
var _89=_88.documentElement;
if((_89.tagName=="parserError")||(_89.namespaceURI=="http://www.mozilla.org/newlayout/xml/parsererror.xml")){
return true;
}
return false;
}
};
nitobi.xml.getParseErrorReason=function(_8a){
if(!nitobi.xml.hasParseError(_8a)){
return "";
}
if(nitobi.browser.IE){
return (_8a.parseError.reason);
}else{
return (new XMLSerializer().serializeToString(_8a));
}
};
nitobi.xml.createXslDoc=function(xsl){
var doc=null;
if(nitobi.browser.IE){
doc=new ActiveXObject("MSXML2.FreeThreadedDOMDocument.3.0");
}else{
if(nitobi.browser.MOZ){
doc=nitobi.xml.createXmlDoc();
}
}
doc=nitobi.xml.loadXml(doc,xsl||"<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns:ntb=\"http://www.nitobi.com\" />");
return doc;
};
nitobi.xml.createXslProcessor=function(xsl){
var _8e=null;
var xt=null;
if(typeof (xsl)!="string"){
xsl=nitobi.xml.serialize(xsl);
}
if(nitobi.browser.IE){
_8e=new ActiveXObject("MSXML2.FreeThreadedDOMDocument.3.0");
xt=new ActiveXObject("MSXML2.XSLTemplate.3.0");
_8e.async=false;
_8e.loadXML(xsl);
xt.stylesheet=_8e;
return xt.createProcessor();
}else{
_8e=nitobi.xml.createXmlDoc(xsl);
xt=new XSLTProcessor();
xt.importStylesheet(_8e);
xt.stylesheet=_8e;
return xt;
}
};
nitobi.xml.parseHtml=function(_90){
if(typeof (_90)=="string"){
_90=document.getElementById(_90);
}
var _91=nitobi.html.getOuterHtml(_90);
var _92="";
if(nitobi.browser.IE){
var _93=new RegExp("(\\s+.[^=]*)='(.*?)'","g");
_91=_91.replace(_93,function(m,_1,_2){
return _1+"=\""+_2.replace(/"/g,"&quot;")+"\"";
});
_92=(_91.substring(_91.indexOf("/>")+2).replace(/(\s+.[^\=]*)\=\s*([^\"^\s^\>]+)/g,"$1=\"$2\" ")).replace(/\n/gi,"").replace(/(.*?:.*?\s)/i,"$1  ");
var _97=new RegExp("=\"([^\"]*)(<)(.*?)\"","gi");
var _98=new RegExp("=\"([^\"]*)(>)(.*?)\"","gi");
while(true){
_92=_92.replace(_97,"=\"$1&lt;$3\" ");
_92=_92.replace(_98,"=\"$1&gt;$3\" ");
var x=(_97.test(_92));
if(!_97.test(_92)){
break;
}
}
}else{
if(nitobi.browser.MOZ){
_92=_91.replace(/(\s+.[^\=]*)\=\s*([^\"^\s^\>]+)/g,"$1=\"$2\" ").replace(/\n/gi,"").replace(/\>\s*\</gi,"><").replace(/(.*?:.*?\s)/i,"$1  ");
_92=_92.replace(/\&/g,"&amp;");
_92=_92.replace(/\&amp;gt;/g,"&gt;").replace(/\&amp;lt;/g,"&lt;").replace(/\&amp;apos;/g,"&apos;").replace(/\&amp;quot;/g,"&quot;").replace(/\&amp;amp;/g,"&amp;").replace(/\&amp;eq;/g,"&eq;");
}
}
if(_92.indexOf("xmlns:ntb=\"http://www.nitobi.com\"")<1){
_92=_92.replace(/\<(.*?)(\s|\>|\\)/,"<$1 xmlns:ntb=\"http://www.nitobi.com\"$2");
}
_92=_92.replace(/\&nbsp\;/gi," ");
return nitobi.xml.createXmlDoc(_92);
};
nitobi.xml.transform=function(xml,xsl,_9c){
if(xsl.documentElement){
xsl=nitobi.xml.createXslProcessor(xsl);
}
if(nitobi.browser.IE){
xsl.input=xml;
xsl.transform();
return xsl.output;
}else{
var doc=xsl.transformToDocument(xml);
var _9e=doc.documentElement;
if(_9e&&_9e.nodeName.indexOf("ntb:")==0){
_9e.setAttributeNS("http://www.w3.org/2000/xmlns/","xmlns:ntb","http://www.nitobi.com");
}
return doc;
}
};
nitobi.xml.transformToString=function(xml,xsl,_a1){
var _a2=nitobi.xml.transform(xml,xsl,"text");
if(nitobi.browser.MOZ){
if(_a1=="xml"){
_a2=nitobi.xml.Serializer.serializeToString(_a2);
}else{
if(_a2.documentElement.childNodes[0]==null){
nitobi.lang.throwError("The transformToString fn could not find any valid output");
}
if(_a2.documentElement.childNodes[0].data!=null){
_a2=_a2.documentElement.childNodes[0].data;
}else{
if(_a2.documentElement.childNodes[0].textContent!=null){
_a2=_a2.documentElement.childNodes[0].textContent;
}else{
nitobi.lang.throwError("The transformToString fn could not find any valid output");
}
}
}
}
return _a2;
};
nitobi.xml.transformToXml=function(xml,xsl){
var _a5=nitobi.xml.transform(xml,xsl,"xml");
if(nitobi.browser.IE){
_a5=nitobi.xml.createXmlDoc(_a5);
}else{
if(nitobi.browser.MOZ){
if(_a5.documentElement.nodeName=="transformiix:result"){
_a5=nitobi.xml.createXmlDoc(_a5.documentElement.firstChild.data);
}
}
}
return _a5;
};
nitobi.xml.serialize=function(xml){
if(nitobi.browser.IE){
return xml.xml;
}else{
return (new XMLSerializer()).serializeToString(xml);
}
};
nitobi.xml.createXmlHttp=function(){
if(nitobi.browser.IE){
var _a7=null;
try{
_a7=new ActiveXObject("Msxml2.XMLHTTP");
}
catch(e){
try{
_a7=new ActiveXObject("Microsoft.XMLHTTP");
}
catch(ee){
}
}
return _a7;
}else{
if(nitobi.browser.MOZ){
return new XMLHttpRequest();
}
}
};
nitobi.xml.createElement=function(_a8,_a9,ns){
ns=ns||"http://www.nitobi.com";
var _ab=null;
if(nitobi.browser.IE){
_ab=_a8.createNode(1,nitobi.xml.nsPrefix+_a9,ns);
}else{
if(_a8.createElementNS){
_ab=_a8.createElementNS(ns,nitobi.xml.nsPrefix+_a9);
}
}
return _ab;
};
function nitobiXmlDecodeXslt(xsl){
return xsl.replace(/x:c-/g,"xsl:choose").replace(/x\:wh\-/g,"xsl:when").replace(/x\:o\-/g,"xsl:otherwise").replace(/x\:n\-/g," name=\"").replace(/x\:s\-/g," select=\"").replace(/x\:va\-/g,"xsl:variable").replace(/x\:v\-/g,"xsl:value-of").replace(/x\:ct\-/g,"xsl:call-template").replace(/x\:w\-/g,"xsl:with-param").replace(/x\:p\-/g,"xsl:param").replace(/x\:t\-/g,"xsl:template").replace(/x\:at\-/g,"xsl:apply-templates").replace(/x\:a\-/g,"xsl:attribute");
}
if(nitobi.browser.MOZ){
Document.prototype.__defineGetter__("xml",function(){
return (new XMLSerializer()).serializeToString(this);
});
Node.prototype.__defineGetter__("xml",function(){
return (new XMLSerializer()).serializeToString(this);
});
XPathResult.prototype.__defineGetter__("length",function(){
return this.snapshotLength;
});
XSLTProcessor.prototype.addParameter=function(_ad,_ae,_af){
if(_ae==null){
this.removeParameter(_af,_ad);
}else{
this.setParameter(_af,_ad,_ae);
}
};
XMLDocument.prototype.selectNodes=function(_b0,_b1){
try{
if(this.nsResolver==null){
this.nsResolver=this.createNSResolver(this.documentElement);
}
var _b2=this.evaluate(_b0,(_b1?_b1:this),this.nsResolver,XPathResult.ORDERED_NODE_SNAPSHOT_TYPE,null);
var _b3=new Array(_b2.snapshotLength);
_b3.expr=_b0;
var j=0;
for(i=0;i<_b2.snapshotLength;i++){
var _b5=_b2.snapshotItem(i);
if(_b5.nodeType!=3){
_b3[j++]=_b5;
}
}
return _b3;
}
catch(e){
}
};
XMLDocument.prototype.selectSingleNode=function(_b6,_b7){
var _b8=_b6.match(/\[\d+\]/ig);
if(_b8!=null){
var x=_b8[_b8.length-1];
if(_b6.lastIndexOf(x)+x.length!=_b6.length){
_b6+="[1]";
}
}
var _ba=this.selectNodes(_b6,_b7||null);
return ((_ba!=null&&_ba.length>0)?_ba[0]:null);
};
Element.prototype.selectNodes=function(_bb){
var doc=this.ownerDocument;
return doc.selectNodes(_bb,this);
};
Element.prototype.selectSingleNode=function(_bd){
var doc=this.ownerDocument;
return doc.selectSingleNode(_bd,this);
};
}
nitobi.xml.getLocalName=function(_bf){
var _c0=_bf.indexOf(":");
if(_c0==-1){
return _bf;
}else{
return _bf.substr(_c0+1);
}
};
nitobi.xml.encode=function(str){
str+="";
str=str.replace(/&/g,"&amp;");
str=str.replace(/'/g,"&apos;");
str=str.replace(/\"/g,"&quot;");
str=str.replace(/</g,"&lt;");
str=str.replace(/>/g,"&gt;");
str=str.replace(/\n/g,"&#xa;");
return str;
};
nitobi.xml.constructValidXpathQuery=function(_c2,_c3){
var _c4=_c2.match(/(\"|\')/g);
if(_c4!=null){
var _c5="concat(";
var _c6="";
var _c7;
for(var i=0;i<_c2.length;i++){
if(_c2.substr(i,1)=="\""){
_c7="&apos;";
}else{
_c7="&quot;";
}
_c5+=_c6+_c7+nitobi.xml.encode(_c2.substr(i,1))+_c7;
_c6=",";
}
_c5+=_c6+"&apos;&apos;";
_c5+=")";
_c2=_c5;
}else{
var _c9=(_c3?"\"":"");
_c2=_c9+nitobi.xml.encode(_c2)+_c9;
}
return _c2;
};
nitobi.lang.defineNs("nitobi.html");
nitobi.html.Url=function(){
};
nitobi.html.Url.setParameter=function(url,key,_cc){
var reg=new RegExp("(\\?|&)("+encodeURIComponent(key)+")=(.*?)(&|$)");
if(url.match(reg)){
return url.replace(reg,"$1$2="+encodeURIComponent(_cc)+"$4");
}
if(url.match(/\?/)){
url=url+"&";
}else{
url=url+"?";
}
return url+encodeURIComponent(key)+"="+encodeURIComponent(_cc);
};
nitobi.html.Url.removeParameter=function(url,key){
var reg=new RegExp("(\\?|&)("+encodeURIComponent(key)+")=(.*?)(&|$)");
return url.replace(reg,function(str,p1,p2,p3,p4,_d6,s){
if(((p1)=="?")&&(p4!="&")){
return "";
}else{
return p1;
}
});
};
nitobi.html.Url.normalize=function(url,_d9){
if(_d9){
if(_d9.indexOf("http://")==0||_d9.indexOf("https://")==0||_d9.indexOf("/")==0){
return _d9;
}
}
var _da=(url.match(/.*\//)||"")+"";
if(_d9){
return _da+_d9;
}
return _da;
};
nitobi.html.Url.randomize=function(url){
return nitobi.html.Url.setParameter(url,"ntb-random",(new Date).getTime());
};
nitobi.lang.defineNs("nitobi.base");
nitobi.base.Event=function(_dc){
this.type=_dc;
this.handlers={};
this.guid=0;
this.setEnabled(true);
};
nitobi.base.Event.prototype.subscribe=function(_dd,_de,_df){
if(_dd==null){
return;
}
var _e0=_dd;
if(typeof (_dd)=="string"){
var s=_dd;
s=s.replace(/eventArgs/g,"arguments[0]");
_dd=nitobi.lang.close(_de,function(){
eval(s);
});
}
if(typeof _de=="object"&&_dd instanceof Function){
_e0=nitobi.lang.close(_de,_dd);
}
_df=_df||_e0.observer_guid||_dd.observer_guid||this.guid++;
_e0.observer_guid=_df;
_dd.observer_guid=_df;
this.handlers[_df]=_e0;
return _df;
};
nitobi.base.Event.prototype.subscribeOnce=function(_e2,_e3){
var _e4=null;
var _e5=this;
var _e6=function(){
_e2.apply(_e3||null,arguments);
_e5.unSubscribe(_e4);
};
_e4=this.subscribe(_e6);
return _e4;
};
nitobi.base.Event.prototype.unSubscribe=function(_e7){
if(_e7 instanceof Function){
_e7=_e7.observer_guid;
}
this.handlers[_e7]=null;
delete this.handlers[_e7];
};
nitobi.base.Event.prototype.notify=function(_e8){
if(this.enabled){
if(arguments.length==0){
arguments=new Array();
arguments[0]=new nitobi.base.EventArgs(null,this);
arguments[0].event=this;
arguments[0].source=null;
}else{
if(typeof (arguments[0].event)!="undefined"&&arguments[0].event==null){
arguments[0].event=this;
}
}
var _e9=false;
for(var _ea in this.handlers){
var _eb=this.handlers[_ea];
if(_eb instanceof Function){
var rv=(_eb.apply(this,arguments)==false);
_e9=_e9||rv;
}
}
return !_e9;
}
return true;
};
nitobi.base.Event.prototype.dispose=function(){
for(var _ed in this.handlers){
this.handlers[_ed]=null;
}
this.handlers={};
};
nitobi.base.Event.prototype.setEnabled=function(_ee){
this.enabled=_ee;
};
nitobi.base.Event.prototype.isEnabled=function(){
return this.enabled;
};
nitobi.lang.defineNs("nitobi.html");
nitobi.html.Css=function(){
};
nitobi.html.Css.onPrecached=new nitobi.base.Event();
nitobi.html.Css.swapClass=function(_ef,_f0,_f1){
if(_ef.className){
var reg=new RegExp("(\\s|^)"+_f0+"(\\s|$)");
_ef.className=_ef.className.replace(reg,"$1"+_f1+"$2");
}
};
nitobi.html.Css.replaceOrAppend=function(_f3,_f4,_f5){
if(nitobi.html.Css.hasClass(_f3,_f4)){
nitobi.html.Css.swapClass(_f3,_f4,_f5);
}else{
nitobi.html.Css.addClass(_f3,_f5);
}
};
nitobi.html.Css.hasClass=function(_f6,_f7){
if(!_f7||_f7===""){
return false;
}
return (new RegExp("(\\s|^)"+_f7+"(\\s|$)")).test(_f6.className);
};
nitobi.html.Css.addClass=function(_f8,_f9){
if(!nitobi.html.Css.hasClass(_f8,_f9)){
_f8.className=_f8.className?_f8.className+" "+_f9:_f9;
}
};
nitobi.html.Css.removeClass=function(_fa,_fb){
if(nitobi.html.Css.hasClass(_fa,_fb)){
var reg=new RegExp("(\\s|^)"+_fb+"(\\s|$)");
_fa.className=_fa.className.replace(reg,"$2");
}
};
nitobi.html.Css.getRules=function(_fd){
var _fe=null;
if(typeof (_fd)=="number"){
_fe=document.styleSheets[_fd];
}else{
_fe=_fd;
}
if(_fe==null){
return null;
}
try{
if(_fe.cssRules){
return _fe.cssRules;
}
if(_fe.rules){
return _fe.rules;
}
}
catch(e){
}
return null;
};
nitobi.html.Css.getStyleSheetsByName=function(_ff){
var arr=new Array();
var ss=document.styleSheets;
var _102=new RegExp(_ff.replace(".",".")+"($|\\?)");
for(var i=0;i<ss.length;i++){
arr=nitobi.html.Css._getStyleSheetsByName(_102,ss[i],arr);
}
return arr;
};
nitobi.html.Css._getStyleSheetsByName=function(_104,_105,arr){
if(_104.test(_105.href)){
arr=arr.concat([_105]);
}
var _107=nitobi.html.Css.getRules(_105);
if(_105.href!=""&&_105.imports){
for(var i=0;i<_105.imports.length;i++){
arr=nitobi.html.Css._getStyleSheetsByName(_104,_105.imports[i],arr);
}
}else{
for(var i=0;i<_107.length;i++){
var s=_107[i].styleSheet;
if(s){
arr=nitobi.html.Css._getStyleSheetsByName(_104,s,arr);
}
}
}
return arr;
};
nitobi.html.Css.imageCache={};
nitobi.html.Css.imageCacheDidNotify=false;
nitobi.html.Css.trackPrecache=function(_10a){
nitobi.html.Css.precacheArray[_10a]=true;
var _10b=false;
for(var i in nitobi.html.Css.precacheArray){
if(!nitobi.html.Css.precacheArray[i]){
_10b=true;
}
}
if((!nitobi.html.Css.imageCacheDidNotify)&&(!_10b)){
nitobi.html.Css.imageCacheDidNotify=true;
nitobi.html.Css.isPrecaching=false;
nitobi.html.Css.onPrecached.notify();
}
};
nitobi.html.Css.precacheArray={};
nitobi.html.Css.isPrecaching=false;
nitobi.html.Css.precacheImages=function(_10d){
nitobi.html.Css.isPrecaching=true;
if(!_10d){
var ss=document.styleSheets;
for(var i=0;i<ss.length;i++){
nitobi.html.Css.precacheImages(ss[i]);
}
return;
}
var _110=/.*?url\((.*?)\).*?/;
var _111=nitobi.html.Css.getRules(_10d);
var url=nitobi.html.Css.getPath(_10d);
for(var i=0;i<_111.length;i++){
var rule=_111[i];
if(rule.styleSheet){
nitobi.html.Css.precacheImages(rule.styleSheet);
}else{
var s=rule.style;
var _115=s?s.backgroundImage:null;
if(_115){
_115=_115.replace(_110,"$1");
_115=nitobi.html.Url.normalize(url,_115);
if(!nitobi.html.Css.imageCache[_115]){
var _116=new Image();
_116.src=_115;
nitobi.html.Css.precacheArray[_115]=false;
var _117=nitobi.lang.close({},nitobi.html.Css.trackPrecache,[_115]);
_116.onload=_117;
_116.onerror=_117;
_116.onabort=_117;
nitobi.html.Css.imageCache[_115]=_116;
try{
if(_116.width>0){
nitobi.html.Css.precacheArray[_115]=true;
}
}
catch(e){
}
}
}
}
}
if(_10d.href!=""&&_10d.imports){
for(var i=0;i<_10d.imports.length;i++){
nitobi.html.Css.precacheImages(_10d.imports[i]);
}
}
};
nitobi.html.Css.getPath=function(_118){
var href=_118.href;
href=nitobi.html.Url.normalize(href);
if(_118.parentStyleSheet&&href.indexOf("/")!=0&&href.indexOf("http://")!=0&&href.indexOf("https://")!=0){
href=nitobi.html.Css.getPath(_118.parentStyleSheet)+href;
}
return href;
};
nitobi.html.Css.getSheetUrl=nitobi.html.Css.getPath;
nitobi.html.Css.findParentStylesheet=function(_11a){
var rule=nitobi.html.Css.getRule(_11a);
if(rule){
return rule.parentStyleSheet;
}
return null;
};
nitobi.html.Css.findInSheet=function(_11c,_11d,_11e){
if(nitobi.browser.IE6&&typeof _11e=="undefined"){
_11e=0;
}else{
if(_11e>4){
return null;
}
}
_11e++;
var _11f=nitobi.html.Css.getRules(_11d);
for(var rule=0;rule<_11f.length;rule++){
var _121=_11f[rule];
if(_121.styleSheet){
var _122=nitobi.html.Css.findInSheet(_11c,_121.styleSheet,_11e);
if(_122){
return _122;
}
}else{
if(_121.selectorText!=null&&_121.selectorText.toLowerCase().indexOf(_11c)>-1){
if(nitobi.browser.IE){
_121={selectorText:_121.selectorText,style:_121.style,readOnly:_121.readOnly,parentStyleSheet:_11d};
}
return _121;
}
}
}
if(_11d.href!=""&&_11d.imports){
for(var i=0;i<_11d.imports.length;i++){
var _122=nitobi.html.Css.findInSheet(_11c,_11d.imports[i],_11e);
if(_122){
return _122;
}
}
}
return null;
};
nitobi.html.Css.getClass=function(_124){
_124=_124.toLowerCase();
if(_124.indexOf(".")!==0){
_124="."+_124;
}
if(nitobi.html.Css.classCache[_124]==null){
var rule=nitobi.html.Css.getRule(_124);
if(rule!=null){
nitobi.html.Css.classCache[_124]=rule.style;
}else{
return null;
}
}
return nitobi.html.Css.classCache[_124];
};
nitobi.html.Css.classCache={};
nitobi.html.Css.getStyleBySelector=function(_126){
var rule=nitobi.html.Css.getRule(_126);
if(rule!=null){
return rule.style;
}
return null;
};
nitobi.html.Css.getRule=function(_128){
_128=_128.toLowerCase();
if(_128.indexOf(".")!==0){
_128="."+_128;
}
var _129=document.styleSheets;
for(var ss=0;ss<_129.length;ss++){
try{
var _12b=nitobi.html.Css.findInSheet(_128,_129[ss]);
if(_12b){
return _12b;
}
}
catch(err){
}
}
return null;
};
nitobi.html.Css.getClassStyle=function(_12c,_12d){
var _12e=nitobi.html.Css.getClass(_12c);
if(_12e!=null){
return _12e[_12d];
}else{
return null;
}
};
nitobi.html.Css.setStyle=function(el,rule,_131){
rule=rule.replace(/\-(\w)/g,function(_132,p1){
return p1.toUpperCase();
});
el.style[rule]=_131;
};
nitobi.html.Css.getStyle=function(oElm,_135){
var _136="";
if(document.defaultView&&document.defaultView.getComputedStyle){
_135=_135.replace(/([A-Z])/g,function($1){
return "-"+$1.toLowerCase();
});
_136=document.defaultView.getComputedStyle(oElm,"").getPropertyValue(_135);
}else{
if(oElm.currentStyle){
_135=_135.replace(/\-(\w)/g,function(_138,p1){
return p1.toUpperCase();
});
_136=oElm.currentStyle[_135];
}
}
return _136;
};
nitobi.html.Css.setOpacities=function(_13a,_13b){
if(_13a.length){
for(var i=0;i<_13a.length;i++){
nitobi.html.Css.setOpacity(_13a[i],_13b);
}
}else{
nitobi.html.Css.setOpacity(_13a,_13b);
}
};
nitobi.html.Css.setOpacity=function(_13d,_13e){
var s=_13d.style;
if(_13e>100){
_13e=100;
}
if(_13e<0){
_13e=0;
}
if(s.filter!=null){
var _140=s.filter.match(/alpha\(opacity=[\d\.]*?\)/ig);
if(_140!=null&&_140.length>0){
s.filter=s.filter.replace(/alpha\(opacity=[\d\.]*?\)/ig,"alpha(opacity="+_13e+")");
}else{
s.filter+="alpha(opacity="+_13e+")";
}
}else{
s.opacity=(_13e/100);
}
};
nitobi.html.Css.getOpacity=function(_141){
if(_141==null){
nitobi.lang.throwError(nitobi.error.ArgExpected+" for nitobi.html.Css.getOpacity");
}
if(nitobi.browser.IE){
if(_141.style.filter==""){
return 100;
}
var s=_141.style.filter;
s.match(/opacity=([\d\.]*?)\)/ig);
if(RegExp.$1==""){
return 100;
}
return parseInt(RegExp.$1);
}else{
return Math.abs(_141.style.opacity?_141.style.opacity*100:100);
}
};
nitobi.html.Css.getCustomStyle=function(_143,_144){
if(nitobi.browser.IE){
return nitobi.html.getClassStyle(_143,_144);
}else{
var rule=nitobi.html.Css.getRule(_143);
var re=new RegExp("(.*?)({)(.*?)(})","gi");
var _147=rule.cssText.match(re);
re=new RegExp("("+_144+")(:)(.*?)(;)","gi");
_147=re.exec(RegExp.$3);
}
};
if(nitobi.browser.MOZ){
Document.prototype.createStyleSheet=function(){
var _148=this.createElement("style");
this.documentElement.childNodes[0].appendChild(_148);
return _148;
};
HTMLStyleElement.prototype.__defineSetter__("cssText",function(_149){
this.innerHTML=_149;
});
HTMLStyleElement.prototype.__defineGetter__("cssText",function(){
return this.innerHTML;
});
}
nitobi.lang.defineNs("nitobi.drawing");
nitobi.drawing.Point=function(x,y){
this.x=x;
this.y=y;
};
nitobi.drawing.Point.prototype.toString=function(){
return "("+this.x+","+this.y+")";
};
nitobi.drawing.rgb=function(r,g,b){
return "#"+((r*65536)+(g*256)+b).toString(16);
};
nitobi.drawing.align=function(_14f,_150,_151,oh,ow,oy,ox){
oh=oh||0;
ow=ow||0;
oy=oy||0;
ox=ox||0;
var a=_151;
var td,sd,tt,tb,tl,tr,th,tw,st,sb,sl,sr,sh,sw;
if(nitobi.browser.IE){
td=_150.getBoundingClientRect();
sd=_14f.getBoundingClientRect();
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
}else{
if(nitobi.browser.MOZ){
td=document.getBoxObjectFor(_150);
sd=document.getBoxObjectFor(_14f);
tt=td.y;
tl=td.x;
tw=td.width;
th=td.height;
st=sd.y;
sl=sd.x;
sw=sd.width;
sh=sd.height;
}else{
td=nitobi.html.getCoords(_150);
sd=nitobi.html.getCoords(_14f);
tt=td.y;
tl=td.x;
tw=td.width;
th=td.height;
st=sd.y;
sl=sd.x;
sw=sd.width;
sh=sd.height;
}
}
var s=_14f.style;
if(a&268435456){
s.height=(th+oh)+"px";
}
if(a&16777216){
s.width=(tw+ow)+"px";
}
if(a&1048576){
s.top=(nitobi.html.getStyleTop(_14f)+tt-st+oy)+"px";
}
if(a&65536){
s.top=(nitobi.html.getStyleTop(_14f)+tt-st+th-sh+oy)+"px";
}
if(a&4096){
s.left=(nitobi.html.getStyleLeft(_14f)-sl+tl+ox)+"px";
}
if(a&256){
s.left=(nitobi.html.getStyleLeft(_14f)-sl+tl+tw-sw+ox)+"px";
}
if(a&16){
s.top=(nitobi.html.getStyleTop(_14f)+tt-st+oy+Math.floor((th-sh)/2))+"px";
}
if(a&1){
s.left=(nitobi.html.getStyleLeft(_14f)-sl+tl+ox+Math.floor((tw-sw)/2))+"px";
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
nitobi.drawing.alignOuterBox=function(_166,_167,_168,oh,ow,oy,ox,show){
oh=oh||0;
ow=ow||0;
oy=oy||0;
ox=ox||0;
if(nitobi.browser.moz){
td=document.getBoxObjectFor(_167);
sd=document.getBoxObjectFor(_166);
var _16e=parseInt(document.defaultView.getComputedStyle(_167,"").getPropertyValue("border-left-width"));
var _16f=parseInt(document.defaultView.getComputedStyle(_167,"").getPropertyValue("border-top-width"));
var _170=parseInt(document.defaultView.getComputedStyle(_166,"").getPropertyValue("border-top-width"));
var _171=parseInt(document.defaultView.getComputedStyle(_166,"").getPropertyValue("border-bottom-width"));
var _172=parseInt(document.defaultView.getComputedStyle(_166,"").getPropertyValue("border-left-width"));
var _173=parseInt(document.defaultView.getComputedStyle(_166,"").getPropertyValue("border-right-width"));
oy=oy+_170-_16f;
ox=ox+_172-_16e;
}
nitobi.drawing.align(_166,_167,_168,oh,ow,oy,ox,show);
};
nitobi.lang.defineNs("nitobi.html");
if(false){
nitobi.html=function(){
};
}
nitobi.html.createElement=function(_174,_175,_176){
var elem=document.createElement(_174);
for(var attr in _175){
if(attr.toLowerCase().substring(0,5)=="class"){
elem.className=_175[attr];
}else{
elem.setAttribute(attr,_175[attr]);
}
}
for(var _179 in _176){
elem.style[_179]=_176[_179];
}
return elem;
};
nitobi.html.createTable=function(_17a,_17b){
var _17c=nitobi.html.createElement("table",_17a,_17b);
var _17d=document.createElement("tbody");
var _17e=document.createElement("tr");
var _17f=document.createElement("td");
_17c.appendChild(_17d);
_17d.appendChild(_17e);
_17e.appendChild(_17f);
return _17c;
};
nitobi.html.setBgImage=function(elem,src){
var s=nitobi.html.Css.getStyle(elem,"background-image");
if(s!=""&&nitobi.browser.IE){
s=s.replace(/(^url\(")(.*?)("\))/,"$2");
}
};
nitobi.html.getDomNodeByPath=function(Node,Path){
if(nitobi.browser.IE){
}
var _185=Node;
var _186=Path.split("/");
var len=_186.length;
for(var i=0;i<len;i++){
if(_185.childNodes[Number(_186[i])]!=null){
_185=_185.childNodes[Number(_186[i])];
}else{
alert("Path expression failed."+Path);
}
var s="";
}
return _185;
};
nitobi.html.indexOfChildNode=function(_18a,_18b){
var _18c=_18a.childNodes;
for(var i=0;i<_18c.length;i++){
if(_18c[i]==_18b){
return i;
}
}
return -1;
};
nitobi.html.evalScriptBlocks=function(node){
for(var i=0;i<node.childNodes.length;i++){
var _190=node.childNodes[i];
if(_190.nodeName.toLowerCase()=="script"){
eval(_190.text);
}else{
nitobi.html.evalScriptBlocks(_190);
}
}
};
nitobi.html.position=function(node){
var pos=nitobi.html.getStyle($(node),"position");
if(pos=="static"){
node.style.position="relative";
}
};
nitobi.html.setOpacity=function(_193,_194){
var _195=_193.style;
_195.opacity=(_194/100);
_195.MozOpacity=(_194/100);
_195.KhtmlOpacity=(_194/100);
_195.filter="alpha(opacity="+_194+")";
};
nitobi.html.highlight=function(o,x){
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
};
nitobi.html.setCursor=function(o,x){
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
};
nitobi.html.encode=function(str){
str+="";
str=str.replace(/&/g,"&amp;");
str=str.replace(/\"/g,"&quot;");
str=str.replace(/</g,"&lt;");
str=str.replace(/>/g,"&gt;");
str=str.replace(/\n/g,"<br>");
return str;
};
nitobi.html.getElement=function(_19d){
if(typeof (_19d)=="string"){
return document.getElementById(_19d);
}
return _19d;
};
if(typeof ($)=="undefined"){
$=nitobi.html.getElement;
}
if(typeof ($F)=="undefined"){
$F=function(id){
var _19f=$(id);
if(_19f!=null){
return _19f.value;
}
return "";
};
}
nitobi.html.getTagName=function(elem){
if(nitobi.browser.IE&&elem.scopeName!=""){
return (elem.scopeName+":"+elem.nodeName).toLowerCase();
}else{
return elem.nodeName.toLowerCase();
}
};
nitobi.html.getStyleTop=function(elem){
return nitobi.lang.parseNumber(elem.style.top);
};
nitobi.html.getStyleLeft=function(elem){
return nitobi.lang.parseNumber(elem.style.left);
};
nitobi.html.getHeight=function(elem){
return elem.offsetHeight;
};
nitobi.html.getWidth=function(elem){
return elem.offsetWidth;
};
if(nitobi.browser.IE){
nitobi.html.getBox=function(elem){
var _1a6=nitobi.lang.parseNumber(nitobi.html.getStyle(document.body,"border-top-width"));
var _1a7=nitobi.lang.parseNumber(nitobi.html.getStyle(document.body,"border-left-width"));
var _1a8=nitobi.lang.parseNumber(document.body.scrollTop)-(_1a6==0?2:_1a6);
var _1a9=nitobi.lang.parseNumber(document.body.scrollLeft)-(_1a7==0?2:_1a7);
var rect=elem.getBoundingClientRect();
return {top:rect.top+_1a8,left:rect.left+_1a9,bottom:rect.bottom,right:rect.right,height:rect.bottom-rect.top,width:rect.right-rect.left};
};
}else{
nitobi.html.getBox=function(elem){
var _1ac=0;
var _1ad=0;
var _1ae=elem.parentNode;
while(_1ae.nodeType==1&&_1ae!=document.body){
_1ac+=nitobi.lang.parseNumber(_1ae.scrollTop)-(nitobi.html.getStyle(_1ae,"overflow")=="auto"?nitobi.lang.parseNumber(nitobi.html.getStyle(_1ae,"border-top-width")):0);
_1ad+=nitobi.lang.parseNumber(_1ae.scrollLeft)-(nitobi.html.getStyle(_1ae,"overflow")=="auto"?nitobi.lang.parseNumber(nitobi.html.getStyle(_1ae,"border-left-width")):0);
_1ae=_1ae.parentNode;
}
var _1af=elem.ownerDocument.getBoxObjectFor(elem);
var _1b0=nitobi.lang.parseNumber(nitobi.html.getStyle(elem,"border-left-width"));
var _1b1=nitobi.lang.parseNumber(nitobi.html.getStyle(elem,"border-right-width"));
var _1b2=nitobi.lang.parseNumber(nitobi.html.getStyle(elem,"border-top-width"));
var top=nitobi.lang.parseNumber(_1af.y)-_1ac-_1b2;
var left=nitobi.lang.parseNumber(_1af.x)-_1ad-_1b0;
var _1b5=left+nitobi.lang.parseNumber(_1af.width);
var _1b6=top+_1af.height;
var _1b7=nitobi.lang.parseNumber(_1af.height);
var _1b8=nitobi.lang.parseNumber(_1af.width);
return {top:top,left:left,bottom:_1b6,right:_1b5,height:_1b7,width:_1b8};
};
nitobi.html.getBox.cache={};
}
nitobi.html.getBox2=nitobi.html.getBox;
nitobi.html.getUniqueId=function(elem){
if(elem.uniqueID){
return elem.uniqueID;
}else{
var t=(new Date()).getTime();
elem.uniqueID=t;
return t;
}
};
nitobi.html.getChildNodeById=function(elem,_1bc,_1bd){
return nitobi.html.getChildNodeByAttribute(elem,"id",_1bc,_1bd);
};
nitobi.html.getChildNodeByAttribute=function(elem,_1bf,_1c0,_1c1){
for(var i=0;i<elem.childNodes.length;i++){
if(elem.nodeType!=3&&Boolean(elem.childNodes[i].getAttribute)){
if(elem.childNodes[i].getAttribute(_1bf)==_1c0){
return elem.childNodes[i];
}
}
}
if(_1c1){
for(var i=0;i<elem.childNodes.length;i++){
var _1c3=nitobi.html.getChildNodeByAttribute(elem.childNodes[i],_1bf,_1c0,_1c1);
if(_1c3!=null){
return _1c3;
}
}
}
return null;
};
nitobi.html.getParentNodeById=function(elem,_1c5){
return nitobi.html.getParentNodeByAtt(elem,"id",_1c5);
};
nitobi.html.getParentNodeByAtt=function(elem,att,_1c8){
while(elem.parentNode!=null){
if(elem.parentNode.getAttribute(att)==_1c8){
return elem.parentNode;
}
elem=elem.parentNode;
}
return null;
};
if(nitobi.browser.IE){
nitobi.html.getFirstChild=function(node){
return node.firstChild;
};
}else{
if(nitobi.browser.MOZ){
nitobi.html.getFirstChild=function(node){
var i=0;
while(i<node.childNodes.length&&node.childNodes[i].nodeType==3){
i++;
}
return node.childNodes[i];
};
}
}
nitobi.html.getScroll=function(){
var _1cc,_1cd=0;
if((nitobi.browser.OPERA==false)&&(document.documentElement.scrollTop>0)){
_1cc=document.documentElement.scrollTop;
_1cd=document.documentElement.scrollLeft;
}else{
_1cc=document.body.scrollTop;
_1cd=document.body.scrollLeft;
}
if(((_1cc==0)&&(document.documentElement.scrollTop>0))||((_1cd==0)&&(document.documentElement.scrollLeft>0))){
_1cc=document.documentElement.scrollTop;
_1cd=document.documentElement.scrollLeft;
}
return {"left":_1cd,"top":_1cc};
};
nitobi.html.getCoords=function(_1ce){
var ew,eh;
try{
var _1d1=_1ce;
ew=_1ce.offsetWidth;
eh=_1ce.offsetHeight;
for(var lx=0,ly=0;_1ce!=null;lx+=_1ce.offsetLeft,ly+=_1ce.offsetTop,_1ce=_1ce.offsetParent){
}
for(;_1d1!=document.body;lx-=_1d1.scrollLeft,ly-=_1d1.scrollTop,_1d1=_1d1.parentNode){
}
}
catch(e){
}
return {"x":lx,"y":ly,"height":eh,"width":ew};
};
nitobi.html.scrollBarWidth=0;
nitobi.html.getScrollBarWidth=function(_1d4){
if(nitobi.html.scrollBarWidth){
return nitobi.html.scrollBarWidth;
}
try{
if(null==_1d4){
var d=document.getElementById("eba.sb.div");
if(null==d){
d=document.createElement("div");
d.id="eba.sb.div";
d.style.width="100px";
d.style.height="100px";
d.style.overflow="auto";
d.innerHTML="<div style='height:200px;'></div>";
d.style.backgroundColor="black";
d.style.position="absolute";
d.style.top="-200px";
document.body.appendChild(d);
}
_1d4=d;
}
if(nitobi.browser.IE){
nitobi.html.scrollBarWidth=Math.abs(_1d4.offsetWidth-_1d4.clientWidth-(_1d4.clientLeft?_1d4.clientLeft*2:0));
}else{
var b=document.getBoxObjectFor(_1d4);
nitobi.html.scrollBarWidth=Math.abs((b.width-_1d4.clientWidth));
}
}
catch(err){
}
return nitobi.html.scrollBarWidth;
};
nitobi.html.align=nitobi.drawing.align;
nitobi.html.emptyElements={HR:true,BR:true,IMG:true,INPUT:true};
nitobi.html.specialElements={TEXTAREA:true};
nitobi.html.permHeight=0;
nitobi.html.permWidth=0;
nitobi.html.getBodyArea=function(){
var _1d7,_1d8,_1d9,_1da;
var x,y;
var _1dd=navigator.userAgent.toLowerCase();
var _1de=false;
var _1df=false;
var _1e0=false;
var ie=false;
var _1e2=false;
if(_1dd.indexOf("opera")>=0){
_1de=true;
}
if(_1dd.indexOf("firefox")>=0){
_1df=true;
}
if(_1dd.indexOf("msie")>=0){
ie=true;
}
if(_1dd.indexOf("safari")>=0){
_1e0=true;
}
if(document.compatMode=="CSS1Compat"){
_1e2=true;
}
var de=document.documentElement;
var db=document.body;
if(self.innerHeight){
x=self.innerWidth;
y=self.innerHeight;
}else{
if(de&&de.clientHeight){
x=de.clientWidth;
y=de.clientHeight;
}else{
if(db){
x=db.clientWidth;
y=db.clientHeight;
}
}
}
_1d9=x;
_1da=y;
if(self.pageYOffset){
x=self.pageXOffset;
y=self.pageYOffset;
}else{
if(de&&de.scrollTop){
x=de.scrollLeft;
y=de.scrollTop;
}else{
if(db){
x=db.scrollLeft;
y=db.scrollTop;
}
}
}
_1d7=x;
_1d8=y;
var _1e5=db.scrollHeight;
var _1e6=db.offsetHeight;
if(_1e5>_1e6){
x=db.scrollWidth;
y=db.scrollHeight;
}else{
x=db.offsetWidth;
y=db.offsetHeight;
}
nitobi.html.permHeight=y;
nitobi.html.permWidth=x;
if(nitobi.html.permHeight<_1da){
nitobi.html.permHeight=_1da;
if(ie&&_1e2){
_1d9+=20;
}
}
if(_1d9<nitobi.html.permWidth){
_1d9=nitobi.html.permWidth;
}
if(nitobi.html.permHeight>_1da){
_1d9+=20;
}
var _1e7,_1e8;
_1e7=de.scrollHeight;
_1e8=de.scrollWidth;
return {scrollWidth:_1e8,scrollHeight:_1e7,scrollLeft:_1d7,scrollTop:_1d8,clientWidth:_1d9,clientHeight:_1da,bodyWidth:nitobi.html.permWidth,bodyHeight:nitobi.html.rrPermHeight};
};
nitobi.html.getOuterHtml=function(node){
if(nitobi.browser.IE){
return node.outerHTML;
}else{
var html="";
switch(node.nodeType){
case Node.ELEMENT_NODE:
html+="<";
html+=node.nodeName.toLowerCase();
if(!nitobi.html.specialElements[node.nodeName]){
for(var a=0;a<node.attributes.length;a++){
if(node.attributes[a].nodeName.toLowerCase()!="_moz-userdefined"){
html+=" "+node.attributes[a].nodeName.toLowerCase()+"=\""+node.attributes[a].nodeValue+"\"";
}
}
html+=">";
if(!nitobi.html.emptyElements[node.nodeName]){
html+=node.innerHTML;
html+="</"+node.nodeName.toLowerCase()+">";
}
}else{
switch(node.nodeName){
case "TEXTAREA":
for(var a=0;a<node.attributes.length;a++){
if(node.attributes[a].nodeName.toLowerCase()!="value"){
html+=" "+node.attributes[a].nodeName.toUpperCase()+"=\""+node.attributes[a].nodeValue+"\"";
}else{
var _1ec=node.attributes[a].nodeValue;
}
}
html+=">";
html+=_1ec;
html+="</"+node.nodeName+">";
break;
}
}
break;
case Node.TEXT_NODE:
html+=node.nodeValue;
break;
case Node.COMMENT_NODE:
html+="<!"+"--"+node.nodeValue+"--"+">";
break;
}
return html;
}
};
try{
Node.prototype.swapNode=function(node){
var _1ee=this.nextSibling;
var _1ef=this.parentNode;
node.parentNode.replaceChild(this,node);
_1ef.insertBefore(node,_1ee);
};
HTMLElement.prototype.getBoundingClientRect=function(_1f0,_1f1){
_1f0=_1f0||0;
_1f1=_1f1||0;
var td=document.getBoxObjectFor(this);
var top=td.y-_1f0;
var left=td.x-_1f1;
return {top:top,left:left,bottom:(top+td.height),right:(left+td.width)};
};
HTMLElement.prototype.getClientRects=function(_1f5,_1f6){
_1f5=_1f5||0;
_1f6=_1f6||0;
var td=document.getBoxObjectFor(this);
return new Array({top:(td.y-_1f5),left:(td.x-_1f6),bottom:(td.y+td.height-_1f5),right:(td.x+td.width-_1f6)});
};
HTMLElement.prototype.insertAdjacentElement=function(pos,node){
switch(pos){
case "beforeBegin":
this.parentNode.insertBefore(node,this);
break;
case "afterBegin":
this.insertBefore(node,this.firstChild);
break;
case "beforeEnd":
this.appendChild(node);
break;
case "afterEnd":
if(this.nextSibling){
this.parentNode.insertBefore(node,this.nextSibling);
}else{
this.parentNode.appendChild(node);
}
break;
}
};
HTMLElement.prototype.insertAdjacentHTML=function(_1fa,_1fb,_1fc){
var df;
var r=this.ownerDocument.createRange();
switch(String(_1fa).toLowerCase()){
case "beforebegin":
r.setStartBefore(this);
df=r.createContextualFragment(_1fb);
this.parentNode.insertBefore(df,this);
break;
case "afterbegin":
r.selectNodeContents(this);
r.collapse(true);
df=r.createContextualFragment(_1fb);
this.insertBefore(df,this.firstChild);
break;
case "beforeend":
if(_1fc==true){
this.innerHTML=this.innerHTML+_1fb;
}else{
r.selectNodeContents(this);
r.collapse(false);
df=r.createContextualFragment(_1fb);
this.appendChild(df);
}
break;
case "afterend":
r.setStartAfter(this);
df=r.createContextualFragment(_1fb);
this.parentNode.insertBefore(df,this.nextSibling);
break;
}
};
HTMLElement.prototype.insertAdjacentText=function(pos,s){
var node=document.createTextNode(s);
this.insertAdjacentElement(pos,node);
};
}
catch(e){
}
nitobi.html.Event=function(){
this.srcElement=null;
this.fromElement=null;
this.toElement=null;
this.eventSrc=null;
};
nitobi.html.handlerId=0;
nitobi.html.elementId=0;
nitobi.html.elements=[];
nitobi.html.unload=[];
nitobi.html.unloadCalled=false;
nitobi.html.attachEvents=function(_202,_203,_204){
var _205=[];
for(var i=0;i<_203.length;i++){
var e=_203[i];
_205.push(nitobi.html.attachEvent(_202,e.type,e.handler,_204,e.capture||false));
}
return _205;
};
nitobi.html.attachEvent=function(_208,type,_20a,_20b,_20c,_20d){
if(type=="anyclick"){
if(nitobi.browser.IE){
nitobi.html.attachEvent(_208,"dblclick",_20a,_20b,_20c,_20d);
}
type="click";
}
if(!(_20a instanceof Function)){
nitobi.lang.throwError("Event handler needs to be a Function");
}
_208=$(_208);
if(type.toLowerCase()=="unload"&&_20d!=true){
var _20e=_20a;
if(_20b!=null){
_20e=function(){
_20a.call(_20b);
};
}
return this.addUnload(_20e);
}
var _20f=this.handlerId++;
var _210=this.elementId++;
if(typeof (_20a.ebaguid)!="undefined"){
_20f=_20a.ebaguid;
}else{
_20a.ebaguid=_20f;
}
if(typeof (_208.ebaguid)=="undefined"){
_208.ebaguid=_210;
nitobi.html.elements[_210]=_208;
}
if(typeof (_208.eba_events)=="undefined"){
_208.eba_events={};
}
if(_208.eba_events[type]==null){
_208.eba_events[type]={};
if(_208.attachEvent){
_208["eba_event_"+type]=function(){
nitobi.html.notify.call(_208,window.event);
};
_208.attachEvent("on"+type,_208["eba_event_"+type]);
if(_20c&&_208.setCapture!=null){
_208.setCapture(true);
}
}else{
if(_208.addEventListener){
_208["eba_event_"+type]=function(){
nitobi.html.notify.call(_208,arguments[0]);
};
_208.addEventListener(type,_208["eba_event_"+type],_20c);
}
}
}
_208.eba_events[type][_20f]={handler:_20a,context:_20b};
return _20f;
};
nitobi.html.notify=function(e){
if(!nitobi.browser.IE){
e.srcElement=e.target;
e.fromElement=e.relatedTarget;
e.toElement=e.relatedTarget;
}
var _212=this;
e.eventSrc=_212;
nitobi.html.Event=e;
for(var _213 in _212.eba_events[e.type]){
var _214=_212.eba_events[e.type][_213];
if(typeof (_214.context)=="object"){
_214.handler.call(_214.context,e,_212);
}else{
_214.handler.call(_212,e,_212);
}
}
};
nitobi.html.detachEvents=function(_215,_216){
for(var i=0;i<_216.length;i++){
var e=_216[i];
nitobi.html.detachEvent(_215,e.type,e.handler);
}
};
nitobi.html.detachEvent=function(_219,type,_21b){
_219=$(_219);
var _21c=_21b;
if(_21b instanceof Function){
_21c=_21b.ebaguid;
}
if(type=="unload"){
this.unload.splice(ebaguid,1);
}
if(_219.eba_events!=null&&_219.eba_events[type]!=null&&_219.eba_events[type][_21c]!=null){
var _21d=_219.eba_events[type];
_21d[_21c]=null;
delete _21d[_21c];
if(nitobi.collections.isHashEmpty(_21d)){
this.m_detach(_219,type,_219["eba_event_"+type]);
_219["eba_event_"+type]=null;
_219.eba_events[type]=null;
_21d=null;
if(_219.nodeType==1){
_219.removeAttribute("eba_event_"+type);
}
}
}
return true;
};
nitobi.html.m_detach=function(_21e,type,_220){
if(_220!=null&&_220 instanceof Function){
if(_21e.detachEvent){
_21e.detachEvent("on"+type,_220);
}else{
if(_21e.removeEventListener){
_21e.removeEventListener(type,_220,false);
}
}
_21e["on"+type]=null;
if(type=="unload"){
for(var i=0;i<this.unload.length;i++){
this.unload[i].call(this);
this.unload[i]=null;
}
}
}
};
nitobi.html.detachAllEvents=function(){
for(var i=0;i<nitobi.html.elements.length;i++){
if(typeof (nitobi.html.elements[i])!="undefined"){
for(var _223 in nitobi.html.elements[i].eba_events){
nitobi.html.m_detach(nitobi.html.elements[i],_223,nitobi.html.elements[i]["eba_event_"+_223]);
if(typeof (nitobi.html.elements[i])!="undefined"&&nitobi.html.elements[i].eba_events[_223]!=null){
for(var j=0;j<nitobi.html.elements[i].eba_events[_223].length;j++){
nitobi.html.elements[i].eba_events[_223][j]=null;
}
}
nitobi.html.elements[i]["eba_event_"+_223]=null;
}
}
}
nitobi.html.elements=null;
};
nitobi.html.addUnload=function(_225){
this.unload.push(_225);
return this.unload.length-1;
};
nitobi.html.cancelEvent=function(evt,v){
nitobi.html.stopPropagation(evt);
nitobi.html.preventDefault(evt);
};
nitobi.html.stopPropagation=function(evt){
if(evt==null){
return;
}
if(nitobi.browser.MOZ){
evt.stopPropagation();
}else{
if(nitobi.browser.IE){
evt.cancelBubble=true;
}
}
};
nitobi.html.preventDefault=function(evt,v){
if(evt==null){
return;
}
if(nitobi.browser.MOZ){
evt.preventDefault();
}else{
if(nitobi.browser.IE){
evt.returnValue=false;
}
}
if(v!=null){
e.keyCode=v;
}
};
nitobi.html.getEventCoords=function(evt){
var _22c={"x":evt.clientX,"y":evt.clientY};
if(nitobi.browser.IE){
_22c.x+=document.documentElement.scrollLeft+document.body.scrollLeft;
_22c.y+=document.documentElement.scrollTop+document.body.scrollTop;
}else{
_22c.x+=window.scrollX;
_22c.y+=window.scrollY;
}
return _22c;
};
nitobi.html.getEvent=function(_22d){
if(nitobi.browser.IE){
return window.event;
}else{
_22d.srcElement=_22d.target;
_22d.fromElement=_22d.relatedTarget;
_22d.toElement=_22d.relatedTarget;
return _22d;
}
};
nitobi.html.createEvent=function(_22e,_22f,_230,_231){
if(nitobi.browser.IE){
_230.target.fireEvent("on"+_22f);
}else{
var _232=document.createEvent(_22e);
_232.initKeyEvent(_22f,true,true,document.defaultView,_230.ctrlKey,_230.altKey,_230.shiftKey,_230.metaKey,_231.keyCode,_231.charCode);
_230.target.dispatchEvent(_232);
}
};
nitobi.html.unloadEventId=nitobi.html.attachEvent(window,"unload",nitobi.html.detachAllEvents,nitobi.html,false,true);
nitobi.lang.defineNs("nitobi.event");
nitobi.event=function(){
};
nitobi.event.keys={};
nitobi.event.guid=0;
nitobi.event.subscribe=function(key,_234){
nitobi.event.publish(key);
var guid=this.guid++;
this.keys[key].add(_234,guid);
return guid;
};
nitobi.event.unsubscribe=function(key,guid){
if(this.keys[key]==null){
return true;
}
if(this.keys[key].remove(guid)){
this.keys[key]=null;
delete this.keys[key];
}
};
nitobi.event.evaluate=function(func,_239){
var _23a=true;
if(typeof func=="string"){
func=func.replace(/eventArgs/gi,"arguments[1]");
var _23b=eval(func);
_23a=(typeof (_23b)=="undefined"?true:_23b);
}
return _23a;
};
nitobi.event.publish=function(key){
if(this.keys[key]==null){
this.keys[key]=new nitobi.event.Key();
}
};
nitobi.event.notify=function(key,_23e){
if(this.keys[key]!=null){
return this.keys[key].notify(_23e);
}else{
return true;
}
};
nitobi.event.dispose=function(){
for(var key in this.keys){
if(typeof (this.keys[key])=="function"){
this.keys[key].dispose();
}
}
this.keys=null;
};
nitobi.event.Key=function(){
this.handlers={};
};
nitobi.event.Key.prototype.add=function(_240,guid){
this.handlers[guid]=_240;
};
nitobi.event.Key.prototype.remove=function(guid){
this.handlers[guid]=null;
delete this.handlers[guid];
var i=true;
for(var item in this.handlers){
i=false;
break;
}
return i;
};
nitobi.event.Key.prototype.notify=function(_245){
var fail=false;
for(var item in this.handlers){
var _248=this.handlers[item];
if(_248 instanceof Function){
var rv=(_248.apply(this,arguments)==false);
fail=fail||rv;
}else{
}
}
return !fail;
};
nitobi.event.Key.prototype.dispose=function(){
for(var _24a in this.handlers){
this.handlers[_24a]=null;
}
};
nitobi.event.Args=function(src){
this.source=src;
};
nitobi.event.Args.prototype.callback=function(){
};
nitobi.html.cancelBubble=nitobi.html.cancelEvent;
nitobi.html.getCssRules=nitobi.html.Css.getRules;
nitobi.html.findParentStylesheet=nitobi.html.Css.findParentStylesheet;
nitobi.html.getClass=nitobi.html.Css.getClass;
nitobi.html.getStyle=nitobi.html.Css.getStyle;
nitobi.html.addClass=nitobi.html.Css.addClass;
nitobi.html.removeClass=nitobi.html.Css.removeClass;
nitobi.html.getClassStyle=nitobi.html.Css.getClassStyle;
nitobi.html.normalizeUrl=nitobi.html.Url.normalize;
nitobi.html.setUrlParameter=nitobi.html.Url.setParameter;
nitobi.lang.defineNs("nitobi.base.XmlNamespace");
nitobi.base.XmlNamespace.prefix="ntb";
nitobi.base.XmlNamespace.uri="http://www.nitobi.com";
nitobi.lang.defineNs("nitobi.collections");
if(false){
nitobi.collections=function(){
};
}
nitobi.collections.IEnumerable=function(){
this.list=new Array();
this.length=0;
};
nitobi.collections.IEnumerable.prototype.add=function(obj){
this.list[this.getLength()]=obj;
this.length++;
};
nitobi.collections.IEnumerable.prototype.insert=function(_24d,obj){
this.list.splice(_24d,0,obj);
this.length++;
};
nitobi.collections.IEnumerable.createNewArray=function(obj,_250){
var _251;
_250=_250||0;
if(obj.count){
_251=obj.count;
}
if(obj.length){
_251=obj.length;
}
var x=new Array(_251-_250);
for(var i=_250;i<_251;i++){
x[i-_250]=obj[i];
}
return x;
};
nitobi.collections.IEnumerable.prototype.get=function(_254){
if(_254<0||_254>=this.getLength()){
nitobi.lang.throwError(nitobi.error.OutOfBounds);
}
return this.list[_254];
};
nitobi.collections.IEnumerable.prototype.set=function(_255,_256){
if(_255<0||_255>=this.getLength()){
nitobi.lang.throwError(nitobi.error.OutOfBounds);
}
this.list[_255]=_256;
};
nitobi.collections.IEnumerable.prototype.indexOf=function(obj){
for(var i=0;i<this.getLength();i++){
if(this.list[i]===obj){
return i;
}
}
return -1;
};
nitobi.collections.IEnumerable.prototype.remove=function(_259){
var i;
if(typeof (_259)!="number"){
i=this.indexOf(_259);
}else{
i=_259;
}
if(-1==i||i<0||i>=this.getLength()){
nitobi.lang.throwError(nitobi.error.OutOfBounds);
}
this.list[i]=null;
this.list.splice(i,1);
this.length--;
};
nitobi.collections.IEnumerable.prototype.getLength=function(){
return this.length;
};
nitobi.collections.IEnumerable.prototype.each=function(func){
var l=this.length;
var list=this.list;
for(var i=0;i<l;i++){
func(list[i]);
}
};
nitobi.lang.defineNs("nitobi.base");
nitobi.base.ISerializable=function(_25f,id,xml,_262){
nitobi.Object.call(this);
if(typeof (this.ISerializableInitialized)=="undefined"){
this.ISerializableInitialized=true;
}else{
return;
}
this.xmlNode=null;
this.setXmlNode(_25f);
if(_25f!=null){
this.profile=nitobi.base.Registry.getInstance().getCompleteProfile({idField:null,tagName:_25f.nodeName});
}else{
this.profile=nitobi.base.Registry.getInstance().getProfileByInstance(this);
}
this.onDeserialize=new nitobi.base.Event();
this.onSetParentObject=new nitobi.base.Event();
this.factory=nitobi.base.Factory.getInstance();
this.objectHash={};
this.onCreateObject=new nitobi.base.Event();
if(_25f!=null){
this.deserializeFromXmlNode(this.getXmlNode());
}else{
if(this.factory!=null&&this.profile.tagName!=null){
this.createByProfile(this.profile,this.getXmlNode());
}else{
if(xml!=null&&_25f!=null){
this.createByXml(xml);
}
}
}
this.disposal.push(this.xmlNode);
};
nitobi.lang.extend(nitobi.base.ISerializable,nitobi.Object);
nitobi.base.ISerializable.guidMap={};
nitobi.base.ISerializable.prototype.ISerializableImplemented=true;
nitobi.base.ISerializable.prototype.getProfile=function(){
return this.profile;
};
nitobi.base.ISerializable.prototype.createByProfile=function(_263,_264){
if(_264==null){
var xml="<"+_263.tagName+" xmlns:"+nitobi.base.XmlNamespace.prefix+"=\""+nitobi.base.XmlNamespace.uri+"\" />";
var _266=nitobi.xml.createXmlDoc(xml);
this.setXmlNode(_266.firstChild);
this.deserializeFromXmlNode(this.xmlNode);
}else{
this.deserializeFromXmlNode(_264);
this.setXmlNode(_264);
}
};
nitobi.base.ISerializable.prototype.createByXml=function(xml){
this.deserializeFromXml(xml);
};
nitobi.base.ISerializable.prototype.getParentObject=function(){
return this.parentObj;
};
nitobi.base.ISerializable.prototype.setParentObject=function(_268){
this.parentObj=_268;
this.onSetParentObject.notify();
};
nitobi.base.ISerializable.prototype.addChildObject=function(_269){
this.addToCache(_269);
_269.setParentObject(this);
var _26a=_269.getXmlNode();
if(!this.areGuidsGenerated(_26a)){
_26a=this.generateGuids(_26a);
_269.setXmlNode(_26a);
}
this.xmlNode.appendChild(_26a);
};
nitobi.base.ISerializable.prototype.insertBeforeChildObject=function(obj,_26c){
_26c=_26c?_26c.getXmlNode():null;
this.addToCache(obj);
obj.setParentObject(this);
var _26d=obj.getXmlNode();
if(!this.areGuidsGenerated(_26d)){
_26d=this.generateGuids(_26d);
obj.setXmlNode(_26d);
}
this.xmlNode.insertBefore(_26d,_26c);
};
nitobi.base.ISerializable.prototype.createElement=function(name){
var _26f;
if(this.xmlNode==null||this.xmlNode.ownerDocument==null){
_26f=nitobi.xml.createXmlDoc();
}else{
_26f=this.xmlNode.ownerDocument;
}
if(nitobi.browser.IE){
return _26f.createNode(1,name,nitobi.base.XmlNamespace.uri);
}else{
if(_26f.createElementNS){
return _26f.createElementNS(nitobi.base.XmlNamespace.uri,name);
}else{
nitobi.lang.throwError("Unable to create a new xml node on this browser.");
}
}
};
nitobi.base.ISerializable.prototype.deleteChildObject=function(id){
this.removeFromCache(id);
var e=this.getElement(id);
if(e!=null){
e.parentNode.removeChild(e);
}
};
nitobi.base.ISerializable.prototype.addToCache=function(obj){
this.objectHash[obj.getId()]=obj;
};
nitobi.base.ISerializable.prototype.removeFromCache=function(id){
this.objectHash[id]=null;
};
nitobi.base.ISerializable.prototype.inCache=function(id){
return (this.objectHash[id]!=null);
};
nitobi.base.ISerializable.prototype.flushCache=function(){
this.objectHash={};
};
nitobi.base.ISerializable.prototype.areGuidsGenerated=function(_275){
if(_275==null||_275.ownerDocument==null){
return false;
}
if(nitobi.browser.IE){
var node=_275.ownerDocument.documentElement;
if(node==null){
return false;
}else{
var id=node.getAttribute("id");
if(id==null||id==""){
return false;
}else{
return (nitobi.base.ISerializable.guidMap[id]!=null);
}
}
}else{
return (_275.ownerDocument.generatedGuids==true);
}
};
nitobi.base.ISerializable.prototype.setGuidsGenerated=function(_278,_279){
if(_278==null||_278.ownerDocument==null){
return;
}
if(nitobi.browser.IE){
var node=_278.ownerDocument.documentElement;
if(node!=null){
var id=node.getAttribute("id");
if(id!=null&&id!=""){
nitobi.base.ISerializable.guidMap[id]=true;
}
}
}else{
_278.ownerDocument.generatedGuids=true;
}
};
nitobi.base.ISerializable.prototype.generateGuids=function(_27c){
nitobi.base.uniqueIdGeneratorProc.addParameter("guid",nitobi.component.getUniqueId(),"");
var doc=nitobi.xml.transformToXml(_27c,nitobi.base.uniqueIdGeneratorProc);
this.saveDocument=doc;
this.setGuidsGenerated(doc.documentElement,true);
return doc.documentElement;
};
nitobi.base.ISerializable.prototype.deserializeFromXmlNode=function(_27e){
if(!this.areGuidsGenerated(_27e)){
_27e=this.generateGuids(_27e);
}
this.setXmlNode(_27e);
this.flushCache();
if(this.profile==null){
this.profile=nitobi.base.Registry.getInstance().getCompleteProfile({idField:null,tagName:_27e.nodeName});
}
this.onDeserialize.notify();
};
nitobi.base.ISerializable.prototype.deserializeFromXml=function(xml){
var doc=nitobi.xml.createXmlDoc(xml);
var node=this.generateGuids(doc.firstChild);
this.setXmlNode(node);
this.onDeserialize.notify();
};
nitobi.base.ISerializable.prototype.getChildObject=function(id){
var obj=null;
obj=this.objectHash[id];
if(obj==null){
var _284=this.getElement(id);
if(_284==null){
return null;
}else{
obj=this.factory.createByNode(_284);
this.onCreateObject.notify(obj);
this.addToCache(obj);
}
obj.setParentObject(this);
}
return obj;
};
nitobi.base.ISerializable.prototype.getChildObjectById=function(id){
return this.getChildObject(id);
};
nitobi.base.ISerializable.prototype.getElement=function(id){
try{
var node=this.xmlNode.selectSingleNode("*[@id='"+id+"']");
return node;
}
catch(err){
nitobi.lang.throwError(nitobi.error.Unexpected,err);
}
};
nitobi.base.ISerializable.prototype.getFactory=function(){
return this.factory;
};
nitobi.base.ISerializable.prototype.setFactory=function(_288){
this.factory=factory;
};
nitobi.base.ISerializable.prototype.getXmlNode=function(){
return this.xmlNode;
};
nitobi.base.ISerializable.prototype.setXmlNode=function(_289){
if(nitobi.lang.typeOf(_289)==nitobi.lang.type.XMLDOC&&_289!=null){
this.ownerDocument=_289;
_289=nitobi.html.getFirstChild(_289);
}else{
if(_289!=null){
this.ownerDocument=_289.ownerDocument;
}
}
if(_289!=null&&nitobi.browser.MOZ&&_289.ownerDocument==null){
nitobi.lang.throwError(nitobi.error.OrphanXmlNode+" ISerializable.setXmlNode");
}
this.xmlNode=_289;
};
nitobi.base.ISerializable.prototype.serializeToXml=function(){
return nitobi.xml.serialize(this.xmlNode);
};
nitobi.base.ISerializable.prototype.getAttribute=function(name,_28b){
if(this[name]!=null){
return this[name];
}
var _28c=this.xmlNode.getAttribute(name);
return _28c===null?_28b:_28c;
};
nitobi.base.ISerializable.prototype.setAttribute=function(name,_28e){
this[name]=_28e;
this.xmlNode.setAttribute(name.toLowerCase(),_28e!=null?_28e.toString():"");
};
nitobi.base.ISerializable.prototype.setIntAttribute=function(name,_290){
var n=parseInt(_290);
if(_290!=null&&(typeof (n)!="number"||isNaN(n))){
nitobi.lang.throwError(name+" is not an integer and therefore cannot be set. It's value was "+_290);
}
this.setAttribute(name,_290);
};
nitobi.base.ISerializable.prototype.getIntAttribute=function(name,_293){
var x=this.getAttribute(name,_293);
if(x==null||x==""){
return 0;
}
var tx=parseInt(x);
if(isNaN(tx)){
nitobi.lang.throwError("ISerializable attempting to get "+name+" which was supposed to be an int but was actually NaN");
}
return tx;
};
nitobi.base.ISerializable.prototype.setBoolAttribute=function(name,_297){
_297=nitobi.lang.getBool(_297);
if(_297!=null&&typeof (_297)!="boolean"){
nitobi.lang.throwError(name+" is not an boolean and therefore cannot be set. It's value was "+_297);
}
this.setAttribute(name,(_297?"true":"false"));
};
nitobi.base.ISerializable.prototype.getBoolAttribute=function(name,_299){
var x=this.getAttribute(name,_299);
if(typeof (x)=="string"&&x==""){
return null;
}
var tx=nitobi.lang.getBool(x);
if(tx==null){
nitobi.lang.throwError("ISerializable attempting to get "+name+" which was supposed to be a bool but was actually "+x);
}
return tx;
};
nitobi.base.ISerializable.prototype.setDateAttribute=function(name,_29d){
this.setAttribute(name,_29d);
};
nitobi.base.ISerializable.prototype.getDateAttribute=function(name,_29f){
if(this[name]){
return this[name];
}
var _2a0=this.getAttribute(name,_29f);
return _2a0?new Date(_2a0):null;
};
nitobi.base.ISerializable.prototype.getId=function(){
return this.getAttribute("id");
};
nitobi.base.ISerializable.prototype.getChildObjectId=function(_2a1,_2a2){
var _2a3=(typeof (_2a1.className)=="string"?_2a1.tagName:_2a1.getXmlNode().nodeName);
var _2a4=_2a3;
if(_2a2){
_2a4+="[@instancename='"+_2a2+"']";
}
var node=this.getXmlNode().selectSingleNode(_2a4);
if(null==node){
return null;
}else{
return node.getAttribute("id");
}
};
nitobi.base.ISerializable.prototype.setObject=function(_2a6,_2a7){
if(_2a6.ISerializableImplemented!=true){
nitobi.lang.throwError(nitobi.error.ExpectedInterfaceNotFound+" ISerializable");
}
var id=this.getChildObjectId(_2a6,_2a7);
if(null!=id){
this.deleteChildObject(id);
}
if(_2a7){
_2a6.setAttribute("instancename",_2a7);
}
this.addChildObject(_2a6);
};
nitobi.base.ISerializable.prototype.getObject=function(_2a9,_2aa){
var id=this.getChildObjectId(_2a9,_2aa);
if(null==id){
return id;
}
return this.getChildObject(id);
};
nitobi.base.ISerializable.prototype.getObjectById=function(id){
return this.getChildObject(id);
};
nitobi.base.ISerializable.prototype.isDescendantExists=function(id){
var node=this.getXmlNode();
var _2af=node.selectSingleNode("//*[@id='"+id+"']");
return (_2af!=null);
};
nitobi.base.ISerializable.prototype.getPathToLeaf=function(id){
var node=this.getXmlNode();
var _2b2=node.selectSingleNode("//*[@id='"+id+"']");
if(nitobi.browser.IE){
_2b2.ownerDocument.setProperty("SelectionLanguage","XPath");
}
var _2b3=_2b2.selectNodes("./ancestor-or-self::*");
var _2b4=this.getId();
var _2b5=0;
for(var i=0;i<_2b3.length;i++){
if(_2b3[i].getAttribute("id")==_2b4){
_2b5=i+1;
break;
}
}
var arr=nitobi.collections.IEnumerable.createNewArray(_2b3,_2b5);
return arr.reverse();
};
nitobi.base.ISerializable.prototype.isDescendantInstantiated=function(id){
var node=this.getXmlNode();
var _2ba=node.selectSingleNode("//*[@id='"+id+"']");
if(nitobi.browser.IE){
_2ba.ownerDocument.setProperty("SelectionLanguage","XPath");
}
var _2bb=_2ba.selectNodes("ancestor::*");
var _2bc=false;
var obj=this;
for(var i=0;i<_2bb.length;i++){
if(_2bc){
var _2bf=_2bb[i].getAttribute("id");
instantiated=obj.inCache(_2bf);
if(!instantiated){
return false;
}
obj=this.getObjectById(_2bf);
}
if(_2bb[i].getAttribute("id")==this.getId()){
_2bc=true;
}
}
return obj.inCache(id);
};
nitobi.lang.defineNs("nitobi.base");
if(!nitobi.base.Registry){
nitobi.base.Registry=function(){
this.classMap={};
this.tagMap={};
};
if(!nitobi.base.Registry.instance){
nitobi.base.Registry.instance=null;
}
nitobi.base.Registry.getInstance=function(){
if(nitobi.base.Registry.instance==null){
nitobi.base.Registry.instance=new nitobi.base.Registry();
}
return nitobi.base.Registry.instance;
};
nitobi.base.Registry.prototype.getProfileByClass=function(_2c0){
return this.classMap[_2c0];
};
nitobi.base.Registry.prototype.getProfileByInstance=function(_2c1){
var _2c2=nitobi.lang.getFirstFunction(_2c1);
var p=_2c2.value.prototype;
var _2c4=null;
var _2c5=0;
for(var _2c6 in this.classMap){
var _2c7=this.classMap[_2c6].classObject;
var _2c8=0;
while(_2c7&&_2c1 instanceof _2c7){
_2c7=_2c7.baseConstructor;
_2c8++;
}
if(_2c8>_2c5){
_2c5=_2c8;
_2c4=_2c6;
}
}
if(_2c4){
return this.getProfileByClass(_2c4);
}else{
return null;
}
};
nitobi.base.Registry.prototype.getProfileByTag=function(_2c9){
return this.tagMap[_2c9];
};
nitobi.base.Registry.prototype.getCompleteProfile=function(_2ca){
if(nitobi.lang.isDefined(_2ca.className)&&_2ca.className!=null){
return this.classMap[_2ca.className];
}
if(nitobi.lang.isDefined(_2ca.tagName)&&_2ca.tagName!=null){
return this.tagMap[_2ca.tagName];
}
nitobi.lang.throwError("A complete class profile could not be found. Insufficient information was provided.");
};
nitobi.base.Registry.prototype.register=function(_2cb){
if(!nitobi.lang.isDefined(_2cb.tagName)||null==_2cb.tagName){
nitobi.lang.throwError("Illegal to register a class without a tagName.");
}
if(!nitobi.lang.isDefined(_2cb.className)||null==_2cb.className){
nitobi.lang.throwError("Illegal to register a class without a className.");
}
this.tagMap[_2cb.tagName]=_2cb;
this.classMap[_2cb.className]=_2cb;
};
}
nitobi.lang.defineNs("nitobi.base");
nitobi.base.Factory=function(){
this.registry=nitobi.base.Registry.getInstance();
};
nitobi.lang.extend(nitobi.base.Factory,nitobi.Object);
nitobi.base.Factory.instance=null;
nitobi.base.Factory.prototype.createByClass=function(_2cc){
try{
return nitobi.lang.newObject(_2cc,arguments,1);
}
catch(err){
nitobi.lang.throwError("The Factory (createByClass) could not create the class "+_2cc+".",err);
}
};
nitobi.base.Factory.prototype.createByNode=function(_2cd){
try{
if(null==_2cd){
nitobi.lang.throwError(nitobi.error.ArgExpected);
}
if(nitobi.lang.typeOf(_2cd)==nitobi.lang.type.XMLDOC){
_2cd=nitobi.xml.getChildNodes(_2cd)[0];
}
var _2ce=this.registry.getProfileByTag(_2cd.nodeName).className;
var _2cf=_2cd.ownerDocument;
var _2d0=Array.prototype.slice.call(arguments,0);
var obj=nitobi.lang.newObject(_2ce,_2d0,0);
return obj;
}
catch(err){
nitobi.lang.throwError("The Factory (createByNode) could not create the class "+_2ce+".",err);
}
};
nitobi.base.Factory.prototype.createByProfile=function(_2d2){
try{
return nitobi.lang.newObject(_2d2.className,arguments,1);
}
catch(err){
nitobi.lang.throwError("The Factory (createByProfile) could not create the class "+_2d2.className+".",err);
}
};
nitobi.base.Factory.prototype.createByTag=function(_2d3){
try{
var _2d4=this.registry.getProfileByTag(_2d3).className;
var _2d5=Array.prototype.slice.call(arguments,0);
return nitobi.lang.newObject(_2d4,_2d5,1);
}
catch(err){
nitobi.lang.throwError("The Factory (createByTag) could not create the class "+_2d4+".",err);
}
};
nitobi.base.Factory.getInstance=function(){
if(nitobi.base.Factory.instance==null){
nitobi.base.Factory.instance=new nitobi.base.Factory();
}
return nitobi.base.Factory.instance;
};
nitobi.lang.defineNs("nitobi.base");
nitobi.base.Profile=function(_2d6,_2d7,_2d8,_2d9,_2da){
this.className=_2d6;
this.classObject=eval(_2d6);
this.schema=_2d7;
this.singleton=_2d8;
this.tagName=_2d9;
this.idField=_2da||"id";
};
nitobi.lang.defineNs("nitobi.base");
if(false){
nitobi.base=function(){
};
}
nitobi.base.Declaration=function(){
nitobi.base.Declaration.baseConstructor.call(this);
this.xmlDoc=null;
};
nitobi.lang.extend(nitobi.base.Declaration,nitobi.Object);
nitobi.base.Declaration.prototype.loadHtml=function(_2db){
try{
_2db=$(_2db);
this.xmlDoc=nitobi.xml.parseHtml(_2db);
return this.xmlDoc;
}
catch(err){
nitobi.lang.throwError(nitobi.error.DeclarationParseError,err);
}
};
nitobi.base.Declaration.prototype.getXmlDoc=function(){
return this.xmlDoc;
};
nitobi.base.Declaration.prototype.serializeToXml=function(){
return nitobi.xml.serialize(this.xmlDoc);
};
nitobi.lang.defineNs("nitobi.base");
nitobi.base.DateMath={DAY:"d",WEEK:"w",MONTH:"m",YEAR:"y",ONE_DAY_MS:86400000};
nitobi.base.DateMath._add=function(date,unit,_2de){
if(unit==this.DAY){
date.setDate(date.getDate()+_2de);
}else{
if(unit==this.WEEK){
date.setDate(date.getDate()+7*_2de);
}else{
if(unit==this.MONTH){
date.setMonth(date.getMonth()+_2de);
}else{
if(unit==this.YEAR){
date.setFullYear(date.getFullYear()+_2de);
}
}
}
}
return date;
};
nitobi.base.DateMath.add=function(date,unit,_2e1){
return this._add(date,unit,_2e1);
};
nitobi.base.DateMath.subtract=function(date,unit,_2e4){
return this._add(date,unit,-1*_2e4);
};
nitobi.base.DateMath.after=function(date,_2e6){
return (date-_2e6)>0;
};
nitobi.base.DateMath.between=function(date,_2e8,end){
return (date-_2e8)>=0&&(end-date)>0;
};
nitobi.base.DateMath.before=function(date,_2eb){
return (date-_2eb)<0;
};
nitobi.base.DateMath.clone=function(date){
var n=new Date(date.toString());
return n;
};
nitobi.base.DateMath.isLeapYear=function(date){
var y=date.getFullYear();
var _1=String(y/4).indexOf(".")==-1;
var _2=String(y/100).indexOf(".")==-1;
var _3=String(y/400).indexOf(".")==-1;
return (_3)?true:(_1&&!_2)?true:false;
};
nitobi.base.DateMath.getMonthDays=function(date){
return [31,(this.isLeapYear(date))?29:28,31,30,31,30,31,31,30,31,30,31][date.getMonth()];
};
nitobi.base.DateMath.getMonthEnd=function(date){
return new Date(date.getFullYear(),date.getMonth(),this.getMonthDays(date));
};
nitobi.base.DateMath.getMonthStart=function(date){
return new Date(date.getFullYear(),date.getMonth(),1);
};
nitobi.base.DateMath.isToday=function(date){
var _2f7=this.resetTime(new Date());
var end=this.add(this.clone(_2f7),this.DAY,1);
return this.between(date,_2f7,end);
};
nitobi.base.DateMath.parse=function(str){
};
nitobi.base.DateMath.getWeekNumber=function(date){
var _2fb=this.getJanuary1st(date);
return Math.ceil(this.getNumberOfDays(_2fb,date)/7);
};
nitobi.base.DateMath.getNumberOfDays=function(_2fc,end){
var _2fe=this.resetTime(this.clone(end)).getTime()-this.resetTime(this.clone(_2fc)).getTime();
return Math.round(_2fe/this.ONE_DAY_MS)+1;
};
nitobi.base.DateMath.getJanuary1st=function(date){
return new Date(date.getFullYear(),0,1);
};
nitobi.base.DateMath.resetTime=function(date){
date.setHours(0);
date.setMinutes(0);
date.setSeconds(0);
date.setMilliseconds(0);
return date;
};
nitobi.base.DateMath.parseIso8601=function(date){
return new Date(date.replace(/^(....).(..).(..).(.*)$/,"$1/$2/$3 $4"));
};
nitobi.base.DateMath.toIso8601=function(date){
if(nitobi.base.DateMath.invalid(date)){
return "";
}
var pz=nitobi.lang.padZeros;
return date.getFullYear()+"-"+pz(date.getMonth()+1)+"-"+pz(date.getDate())+" "+pz(date.getHours())+":"+pz(date.getMinutes())+":"+pz(date.getSeconds());
};
nitobi.base.DateMath.invalid=function(date){
return (!date)||(date.toString()=="Invalid Date");
};
nitobi.lang.defineNs("nitobi.base");
nitobi.base.EventArgs=function(_305,_306){
this.source=_305;
this.event=_306||nitobi.html.Event;
};
nitobi.base.EventArgs.prototype.getSource=function(){
return this.source;
};
nitobi.base.EventArgs.prototype.getEvent=function(){
return this.event;
};
nitobi.lang.defineNs("nitobi.collections");
nitobi.collections.IList=function(){
nitobi.base.ISerializable.call(this);
nitobi.collections.IEnumerable.call(this);
};
nitobi.lang.implement(nitobi.collections.IList,nitobi.base.ISerializable);
nitobi.lang.implement(nitobi.collections.IList,nitobi.collections.IEnumerable);
nitobi.collections.IList.prototype.IListImplemented=true;
nitobi.collections.IList.prototype.add=function(obj){
nitobi.collections.IEnumerable.prototype.add.call(this,obj);
if(obj.ISerializableImplemented==true&&obj.profile!=null){
this.addChildObject(obj);
}
};
nitobi.collections.IList.prototype.insert=function(_308,obj){
var _30a=this.get(_308);
nitobi.collections.IEnumerable.prototype.insert.call(this,_308,obj);
if(obj.ISerializableImplemented==true&&obj.profile!=null){
this.insertBeforeChildObject(obj,_30a);
}
};
nitobi.collections.IList.prototype.addToCache=function(obj,_30c){
nitobi.base.ISerializable.prototype.addToCache.call(this,obj);
this.list[_30c]=obj;
};
nitobi.collections.IList.prototype.removeFromCache=function(_30d){
nitobi.base.ISerializable.prototype.removeFromCache.call(this,this.list[_30d].getId());
};
nitobi.collections.IList.prototype.flushCache=function(){
nitobi.base.ISerializable.prototype.flushCache.call(this);
this.list=new Array();
};
nitobi.collections.IList.prototype.get=function(_30e){
if(typeof (_30e)=="object"){
return _30e;
}
if(_30e<0||_30e>=this.getLength()){
nitobi.lang.throwError(nitobi.error.OutOfBounds);
}
var obj=null;
if(this.list[_30e]!=null){
obj=this.list[_30e];
}
if(obj==null){
var _310=nitobi.xml.getChildNodes(this.xmlNode)[_30e];
if(_310==null){
return null;
}else{
obj=this.factory.createByNode(_310);
this.onCreateObject.notify(obj);
nitobi.collections.IList.prototype.addToCache.call(this,obj,_30e);
}
obj.setParentObject(this);
}
return obj;
};
nitobi.collections.IList.prototype.getById=function(id){
var node=this.xmlNode.selectSingleNode("*[@id='"+id+"']");
var _313=nitobi.xml.indexOfChildNode(node.parentNode,node);
return this.get(_313);
};
nitobi.collections.IList.prototype.set=function(_314,_315){
if(_314<0||_314>=this.getLength()){
nitobi.lang.throwError(nitobi.error.OutOfBounds);
}
try{
if(_315.ISerializableImplemented==true){
var obj=this.get(_314);
if(obj.getXmlNode()!=_315.getXmlNode()){
var _317=this.xmlNode.insertBefore(_315.getXmlNode(),obj.getXmlNode());
this.xmlNode.removeChild(obj.getXmlNode());
obj.setXmlNode(_317);
}
}
_315.setParentObject(this);
nitobi.collections.IList.prototype.addToCache.call(this,_315,_314);
}
catch(err){
nitobi.lang.throwError(nitobi.error.Unexpected,err);
}
};
nitobi.collections.IList.prototype.remove=function(_318){
var i;
if(typeof (_318)!="number"){
i=this.indexOf(_318);
}else{
i=_318;
}
var obj=this.get(i);
nitobi.collections.IEnumerable.prototype.remove.call(this,_318);
this.xmlNode.removeChild(obj.getXmlNode());
};
nitobi.collections.IList.prototype.getLength=function(){
return nitobi.xml.getChildNodes(this.xmlNode).length;
};
nitobi.lang.defineNs("nitobi.collections");
nitobi.collections.List=function(_31b){
nitobi.collections.List.baseConstructor.call(this);
nitobi.collections.IList.call(this);
};
nitobi.lang.extend(nitobi.collections.List,nitobi.Object);
nitobi.lang.implement(nitobi.collections.List,nitobi.collections.IList);
nitobi.base.Registry.getInstance().register(new nitobi.base.Profile("nitobi.collections.List",null,false,"ntb:list"));
nitobi.lang.defineNs("nitobi.collections");
nitobi.collections.isHashEmpty=function(hash){
var _31d=true;
for(var item in hash){
if(hash[item]!=null&&hash[item]!=""){
_31d=false;
break;
}
}
return _31d;
};
nitobi.collections.hashLength=function(hash){
var _320=0;
for(var item in hash){
_320++;
}
return _320;
};
nitobi.collections.serialize=function(hash){
var s="";
for(var item in hash){
var _325=hash[item];
var type=typeof (_325);
if(type=="string"||type=="number"){
s+="'"+item+"':'"+_325+"',";
}
}
s=s.substring(0,s.length-1);
return "{"+s+"}";
};
nitobi.lang.defineNs("nitobi.ui");
if(false){
nitobi.ui=function(){
};
}
nitobi.ui.setWaitScreen=function(_327){
if(_327){
var sc=nitobi.html.getBodyArea();
var me=nitobi.html.createElement("div",{"id":"NTB_waitDiv"},{"verticalAlign":"middle","color":"#000000","font":"12px Trebuchet MS, Georgia, Verdana","textAlign":"center","background":"#ffffff","border":"1px solid #000000","padding":"0px","position":"absolute","top":(sc.clientHeight/2)+sc.scrollTop-30+"px","left":(sc.clientWidth/2)+sc.scrollLeft-100+"px","width":"200px","height":"60px"});
me.innerHTML="<table height=60 width=200><tr><td valign=center height=60 align=center>Please wait..</td></tr></table>";
document.getElementsByTagName("body").item(0).appendChild(me);
}else{
var me=$("NTB_waitDiv");
try{
document.getElementsByTagName("body").item(0).removeChild(me);
}
catch(e){
}
}
};
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.IStyleable=function(_32a){
this.htmlNode=_32a||null;
this.onBeforeSetStyle=new nitobi.base.Event();
this.onSetStyle=new nitobi.base.Event();
};
nitobi.ui.IStyleable.prototype.getHtmlNode=function(){
return this.htmlNode;
};
nitobi.ui.IStyleable.prototype.setHtmlNode=function(node){
this.htmlNode=node;
};
nitobi.ui.IStyleable.prototype.setStyle=function(name,_32d){
if(this.onBeforeSetStyle.notify(new nitobi.ui.StyleEventArgs(this,this.onBeforeSetStyle,name,_32d))&&this.getHtmlNode()!=null){
nitobi.html.Css.setStyle(this.getHtmlNode(),name,_32d);
this.onSetStyle.notify(new nitobi.ui.StyleEventArgs(this,this.onSetStyle,name,_32d));
}
};
nitobi.ui.IStyleable.prototype.getStyle=function(name){
return nitobi.html.Css.getStyle(this.getHtmlNode(),name);
};
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.StyleEventArgs=function(_32f,_330,_331,_332){
nitobi.ui.ElementEventArgs.baseConstructor.apply(this,arguments);
this.property=_331||null;
this.value=_332||null;
};
nitobi.lang.extend(nitobi.ui.StyleEventArgs,nitobi.base.EventArgs);
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.IScrollable=function(_333){
this.scrollableElement=_333;
};
nitobi.ui.IScrollable.prototype.setScrollableElement=function(el){
this.scrollableElement=el;
};
nitobi.ui.IScrollable.prototype.getScrollableElement=function(){
return this.scrollableElement;
};
nitobi.ui.IScrollable.prototype.getScrollLeft=function(){
return this.scrollableElement.scrollLeft;
};
nitobi.ui.IScrollable.prototype.setScrollLeft=function(left){
this.scrollableElement.scrollLeft=left;
};
nitobi.ui.IScrollable.prototype.scrollLeft=function(_336){
_336=_336||25;
this.scrollableElement.scrollLeft-=_336;
};
nitobi.ui.IScrollable.prototype.scrollRight=function(_337){
_337=_337||25;
this.scrollableElement.scrollLeft+=_337;
};
nitobi.ui.IScrollable.prototype.isOverflowed=function(_338){
_338=_338||this.scrollableElement.childNodes[0];
return !(parseInt(nitobi.html.getBox(this.scrollableElement).width)>=parseInt(nitobi.html.getBox(_338).width));
};
nitobi.lang.defineNs("nitobi.ui");
if(false){
nitobi.ui=function(){
};
}
nitobi.ui.startDragOperation=function(_339,_33a,_33b,_33c,_33d,_33e){
var ddo=new nitobi.ui.DragDrop(_339,_33b,_33c);
ddo.onDragStop.subscribe(_33e,_33d);
ddo.startDrag(_33a);
};
nitobi.ui.DragDrop=function(_340,_341,_342){
this.allowVertDrag=(_341!=null?_341:true);
this.allowHorizDrag=(_342!=null?_342:true);
if(nitobi.browser.IE){
this.surface=document.getElementById("ebadragdropsurface_");
if(this.surface==null){
this.surface=nitobi.html.createElement("div",{"id":"ebadragdropsurface_"},{"filter":"alpha(opacity=1)","backgroundColor":"white","position":"absolute","display":"none","top":"0px","left":"0px","width":"100px","height":"100px","zIndex":"899"});
document.body.appendChild(this.surface);
}
}
if(_340.nodeType==3){
alert("Text node not supported. Use parent element");
}
this.element=_340;
this.zIndex=this.element.style.zIndex;
this.element.style.zIndex=900;
this.onMouseMove=new nitobi.base.Event();
this.onDragStart=new nitobi.base.Event();
this.onDragStop=new nitobi.base.Event();
this.events=[{"type":"mouseup","handler":this.handleMouseUp,"capture":true},{"type":"mousemove","handler":this.handleMouseMove,"capture":true}];
};
nitobi.ui.DragDrop.prototype.startDrag=function(_343){
this.elementOriginTop=parseInt(this.element.style.top,10);
this.elementOriginLeft=parseInt(this.element.style.left,10);
if(isNaN(this.elementOriginLeft)){
this.elementOriginLeft=0;
}
if(isNaN(this.elementOriginTop)){
this.elementOriginTop=0;
}
var _344=nitobi.html.getEventCoords(_343);
x=_344.x;
y=_344.y;
this.originX=x;
this.originY=y;
nitobi.html.attachEvents(document,this.events,this);
nitobi.html.cancelEvent(_343);
this.onDragStart.notify();
};
nitobi.ui.DragDrop.prototype.handleMouseMove=function(_345){
var x,y;
var _348=nitobi.html.getEventCoords(_345);
x=_348.x;
y=_348.y;
if(nitobi.browser.IE){
this.surface.style.display="block";
if(document.compat=="CSS1Compat"){
var _349=nitobi.html.getBodyArea();
var _34a=0;
if(document.compatMode=="CSS1Compat"){
_34a=25;
}
this.surface.style.width=(_349.clientWidth-_34a)+"px";
this.surface.style.height=(_349.clientHeight)+"px";
}else{
this.surface.style.width=document.body.clientWidth;
this.surface.style.height=document.body.clientHeight;
}
}
if(this.allowHorizDrag){
this.element.style.left=(this.elementOriginLeft+x-this.originX)+"px";
}
if(this.allowVertDrag){
this.element.style.top=(this.elementOriginTop+y-this.originY)+"px";
}
this.x=x;
this.y=y;
this.onMouseMove.notify(this);
nitobi.html.cancelEvent(_345);
};
nitobi.ui.DragDrop.prototype.handleMouseUp=function(_34b){
this.onDragStop.notify({"event":_34b,"x":this.x,"y":this.y});
nitobi.html.detachEvents(document,this.events);
if(nitobi.browser.IE){
this.surface.style.display="none";
}
this.element.style.zIndex=this.zIndex;
this.element.object=null;
this.element=null;
};
if(typeof (nitobi.ajax)=="undefined"){
nitobi.ajax=function(){
};
}
nitobi.ajax.createXmlHttp=function(){
if(nitobi.browser.IE){
var _34c=null;
try{
_34c=new ActiveXObject("Msxml2.XMLHTTP");
}
catch(e){
try{
_34c=new ActiveXObject("Microsoft.XMLHTTP");
}
catch(ee){
}
}
return _34c;
}else{
if(nitobi.browser.XHR_ENABLED){
return new XMLHttpRequest();
}
}
};
nitobi.lang.defineNs("nitobi.ajax");
nitobi.ajax.HttpRequest=function(){
this.handler="";
this.async=true;
this.responseType=null;
this.httpObj=nitobi.ajax.createXmlHttp();
this.onPostComplete=new nitobi.base.Event();
this.onGetComplete=new nitobi.base.Event();
this.onError=new nitobi.base.Event();
this.timeout=0;
this.timeoutId=null;
this.params=null;
this.data="";
this.completeCallback=null;
this.errorCallback=null;
this.status="complete";
this.preventCache=true;
};
nitobi.lang.extend(nitobi.ajax.HttpRequest,nitobi.Object);
nitobi.ajax.HttpRequestPool_MAXCONNECTIONS=64;
nitobi.ajax.HttpRequest.prototype.handleResponse=function(){
var _34d=null;
var _34e=null;
if((this.httpObj.responseXML!=null&&this.httpObj.responseXML.documentElement!=null)&&this.responseType!="text"){
_34d=this.httpObj.responseXML;
}else{
if(this.responseType=="xml"){
_34d=nitobi.xml.createXmlDoc(this.httpObj.responseText);
}else{
_34d=this.httpObj.responseText;
}
}
if(this.httpObj.status!=200){
this.onError.notify({"source":this,"status":this.httpObj.status,"message":"An error occured retrieving the data from the server. "+"Expected response type was '"+this.responseType+"'."});
}
return _34d;
};
nitobi.ajax.HttpRequest.prototype.post=function(data,url){
this.handler=url||this.handler;
this.data=data;
this.status="pending";
this.httpObj.open("POST",this.handler,this.async,"","");
if(this.async){
this.httpObj.onreadystatechange=nitobi.lang.close(this,this.postComplete);
}
if(this.responseType=="xml"){
this.httpObj.setRequestHeader("Content-Type","text/xml");
}else{
this.httpObj.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
}
this.httpObj.send(data);
if(!this.async){
return this.handleResponse();
}
};
nitobi.ajax.HttpRequest.prototype.postComplete=function(){
if(this.httpObj.readyState==4){
this.status="complete";
var _351={"response":this.handleResponse(),"params":this.params};
this.onPostComplete.notify(_351);
if(this.completeCallback){
this.completeCallback.call(this,_351);
}
}
};
nitobi.ajax.HttpRequest.prototype.postXml=function(_352){
this.setTimeout();
if(("undefined"==typeof (_352.documentElement))||(null==_352.documentElement)||("undefined"==typeof (_352.documentElement.childNodes))||(1>_352.documentElement.childNodes.length)){
ebaErrorReport("updategram is empty. No request sent. xmlData["+_352+"]\nxmlData.xml["+_352.xml+"]");
return;
}
if(null==_352.xml){
var _353=new XMLSerializer();
_352.xml=_353.serializeToString(_352);
}
var sync=this.post(_352.xml);
if(!this.async){
return sync;
}
};
nitobi.ajax.HttpRequest.prototype.get=function(url){
this.handler=url||this.handler;
this.setTimeout();
this.status="pending";
try{
this.httpObj.open("GET",(this.preventCache?this.cacheBust(this.handler):this.handler),this.async);
}
catch(e){
throw (e);
return;
}
if(this.async){
this.httpObj.onreadystatechange=nitobi.lang.close(this,this.getComplete);
}
if(this.responseType=="xml"){
this.httpObj.setRequestHeader("Content-Type","text/xml");
}
this.httpObj.send(null);
if(!this.async){
return this.handleResponse();
}
};
nitobi.ajax.HttpRequest.prototype.setTimeout=function(){
if(this.timeout>0){
this.timeoutId=window.setTimeout(nitobi.lang.close(this,this.abort),this.timeout);
}
};
nitobi.ajax.HttpRequest.prototype.getComplete=function(){
if(this.httpObj.readyState==4){
this.status="complete";
var _356={"response":this.handleResponse(),"params":this.params,"status":this.httpObj.status,"statusText":this.httpObj.statusText};
this.onGetComplete.notify(_356);
if(this.completeCallback){
this.completeCallback.call(this,_356);
}
}
};
nitobi.ajax.HttpRequest.isError=function(code){
return (code>=400&&code<600);
};
nitobi.ajax.HttpRequest.prototype.abort=function(){
this.httpObj.onreadystatechange=function(){
};
this.httpObj.abort();
};
nitobi.ajax.HttpRequest.prototype.clear=function(){
this.handler="";
this.async=true;
this.onPostComplete.dispose();
this.onGetComplete.dispose();
this.params=null;
};
nitobi.ajax.HttpRequest.prototype.cacheBust=function(url){
var _359=url.split("?");
var _35a="nitobi_cachebust="+(new Date().getTime());
if(_359.length==1){
url+="?"+_35a;
}else{
url+="&"+_35a;
}
return url;
};
nitobi.ajax.HttpRequestPool=function(_35b){
this.inUse=new Array();
this.free=new Array();
this.max=_35b||nitobi.ajax.HttpRequestPool_MAXCONNECTIONS;
this.locked=false;
this.context=null;
};
nitobi.ajax.HttpRequestPool.prototype.reserve=function(){
this.locked=true;
var _35c;
if(this.free.length){
_35c=this.free.pop();
_35c.clear();
this.inUse.push(_35c);
}else{
if(this.inUse.length<this.max){
try{
_35c=new nitobi.ajax.HttpRequest();
}
catch(e){
_35c=null;
}
this.inUse.push(_35c);
}else{
throw "No request objects available";
}
}
this.locked=false;
return _35c;
};
nitobi.ajax.HttpRequestPool.prototype.release=function(_35d){
var _35e=false;
this.locked=true;
if(null!=_35d){
for(var i=0;i<this.inUse.length;i++){
if(_35d==this.inUse[i]){
this.free.push(this.inUse[i]);
this.inUse.splice(i,1);
_35e=true;
break;
}
}
}
this.locked=false;
return null;
};
nitobi.ajax.HttpRequestPool.prototype.dispose=function(){
for(var i=0;i<this.inUse.length;i++){
this.inUse[i].dispose();
}
this.inUse=null;
for(var j=0;j<this.free.length;j++){
this.free[i].dispose();
}
this.free=null;
};
nitobi.ajax.HttpRequestPool.instance=null;
nitobi.ajax.HttpRequestPool.getInstance=function(){
if(nitobi.ajax.HttpRequestPool.instance==null){
nitobi.ajax.HttpRequestPool.instance=new nitobi.ajax.HttpRequestPool();
}
return nitobi.ajax.HttpRequestPool.instance;
};
nitobi.lang.defineNs("nitobi.data");
nitobi.data.UrlConnector=function(url,_363){
this.url=url||null;
this.transformer=_363||null;
this.async=true;
};
nitobi.data.UrlConnector.prototype.get=function(_364,_365){
var _366=nitobi.data.UrlConnector.requestPool.reserve();
var _367=this.url;
for(var p in _364){
_367=nitobi.html.Url.setParameter(_367,p,_364[p]);
}
_366.handler=_367;
_366.async=this.async;
_366.responseType="xml";
_366.params={dataReadyCallback:_365};
_366.completeCallback=nitobi.lang.close(this,this.getComplete);
_366.get();
};
nitobi.data.UrlConnector.prototype.getComplete=function(_369){
if(_369.params.dataReadyCallback){
var _36a=_369.response;
var _36b=_369.params.dataReadyCallback;
var _36c=_36a;
if(this.transformer){
if(typeof (this.transformer)==="function"){
_36c=this.transformer.call(null,_36a);
}else{
_36c=nitobi.xml.transform(_36a,this.transformer,"xml");
}
}
if(_36b){
_36b.call(null,{result:_36c,response:_369.response});
}
}
};
nitobi.data.UrlConnector.requestPool=new nitobi.ajax.HttpRequestPool();
function ntbAssert(_36d,_36e,_36f,_370){
}
nitobi.lang.defineNs("console");
nitobi.lang.defineNs("nitobi.debug");
if(typeof (console.log)=="undefined"){
console.log=function(s){
nitobi.debug.addDebugTools();
var t=$("nitobi.log");
t.value=s+"\n"+t.value;
};
console.evalCode=function(){
var _373=(eval($("nitobi.consoleEntry").value));
};
}
nitobi.debug.addDebugTools=function(){
var sId="nitobi_debug_panel";
var div=document.getElementById(sId);
var html="<table width=100%><tr><td width=50%><textarea style='width:100%' cols=125 rows=25 id='nitobi.log'></textarea></td><td width=50%><textarea style='width:100%' cols=125 rows=25 id='nitobi.consoleEntry'></textarea><br/><button onclick='console.evalCode()'>Eval</button></td></tr></table>";
if(div==null){
var div=document.createElement("div");
div.setAttribute("id",sId);
div.innerHTML=html;
document.body.appendChild(div);
}else{
if(div.innerHTML==""){
div.innerHTML=html;
}
}
};
nitobi.debug.assert=function(){
};
EBA_EM_ATTRIBUTE_ERROR=1;
EBA_XHR_RESPONSE_ERROR=2;
EBA_DEBUG="debug";
EBA_WARN="warn";
EBA_ERROR="error";
EBA_THROW="throw";
EBA_DEBUG_MODE=false;
EBA_ON_ERROR="";
EBA_LAST_ERROR="";
_ebaDebug=false;
NTB_EM_ATTRIBUTE_ERROR=1;
NTB_XHR_RESPONSE_ERROR=2;
NTB_DEBUG="debug";
NTB_WARN="warn";
NTB_ERROR="error";
NTB_THROW="throw";
NTB_DEBUG_MODE=false;
NTB_ON_ERROR="";
NTB_LAST_ERROR="";
_ebaDebug=false;
function _ntbAssert(_377,_378){
}
function ebaSetOnErrorEvent(_379){
nitobi.debug.setOnErrorEvent.apply(this,arguments);
}
nitobi.debug.setOnErrorEvent=function(_37a){
NTB_ON_ERROR=_37a;
};
function ebaReportError(_37b,_37c,_37d){
nitobi.debug.errorReport("dude stop calling this method it is now called nitobi.debug.errorReport","");
nitobi.debug.errorReport(_37b,_37c,_37d);
}
function ebaErrorReport(_37e,_37f,_380){
nitobi.debug.errorReport.apply(this,arguments);
}
nitobi.debug.errorReport=function(_381,_382,_383){
_383=(_383)?_383:NTB_DEBUG;
if(NTB_DEBUG==_383&&!NTB_DEBUG_MODE){
return;
}
var _384=_381+"\nerror code    ["+_382+"]\nerror Severity["+_383+"]";
LastError=_384;
if(eval(NTB_ON_ERROR||"true")){
switch(_382){
case NTB_EM_ATTRIBUTE_ERROR:
confirm(_381);
break;
case NTB_XHR_RESPONSE_ERROR:
confirm(_381);
break;
default:
window.status=_381;
break;
}
}
if(NTB_THROW==_383){
throw (_384);
}
};
if(false){
nitobi.error=function(){
};
}
nitobi.lang.defineNs("nitobi.error");
nitobi.error.onError=new nitobi.base.Event();
if(nitobi){
if(nitobi.testframework){
if(nitobi.testframework.initEventError){
nitobi.testframework.initEventError();
}
}
}
nitobi.error.ErrorEventArgs=function(_385,_386,type){
nitobi.error.ErrorEventArgs.baseConstructor.call(this,_385);
this.description=_386;
this.type=type;
};
nitobi.lang.extend(nitobi.error.ErrorEventArgs,nitobi.base.EventArgs);
nitobi.error.isError=function(err,_389){
return (err.indexOf(_389)>-1);
};
nitobi.error.OutOfBounds="Array index out of bounds.";
nitobi.error.Unexpected="An unexpected error occurred.";
nitobi.error.ArgExpected="The argument is null and not optional.";
nitobi.error.BadArgType="The argument is not of the correct type.";
nitobi.error.BadArg="The argument is not a valid value.";
nitobi.error.XmlParseError="The XML did not parse correctly.";
nitobi.error.DeclarationParseError="The HTML declaration could not be parsed.";
nitobi.error.ExpectedInterfaceNotFound="The object does not support the properties or methods of the expected interface. Its class must implement the required interface.";
nitobi.error.NoHtmlNode="No HTML node found with id.";
nitobi.error.OrphanXmlNode="The XML node has no owner document.";
nitobi.error.HttpRequestError="The HTML page could not be loaded.";
nitobi.lang.defineNs("nitobi.html");
nitobi.html.IRenderer=function(_38a){
this.setTemplate(_38a);
this.parameters={};
};
nitobi.html.IRenderer.prototype.renderAfter=function(_38b,data){
_38b=$(_38b);
var _38d=_38b.parentNode;
_38b=_38b.nextSibling;
return this._renderBefore(_38d,_38b,data);
};
nitobi.html.IRenderer.prototype.renderBefore=function(_38e,data){
_38e=$(_38e);
return this._renderBefore(_38e.parentNode,_38e,data);
};
nitobi.html.IRenderer.prototype._renderBefore=function(_390,_391,data){
var s=this.renderToString(data);
var _394=document.createElement("div");
_394.innerHTML=s;
var _395=new Array();
if(_394.childNodes){
var i=0;
while(_394.childNodes.length){
_395[i++]=_394.firstChild;
_390.insertBefore(_394.firstChild,_391);
}
}else{
}
return _395;
};
nitobi.html.IRenderer.prototype.renderIn=function(_397,data){
_397=$(_397);
var s=this.renderToString(data);
_397.innerHTML=s;
return _397.childNodes;
};
nitobi.html.IRenderer.prototype.renderToString=function(data){
};
nitobi.html.IRenderer.prototype.setTemplate=function(_39b){
this.template=_39b;
};
nitobi.html.IRenderer.prototype.getTemplate=function(){
return this.template;
};
nitobi.html.IRenderer.prototype.setParameters=function(_39c){
for(var p in _39c){
this.parameters[p]=_39c[p];
}
};
nitobi.html.IRenderer.prototype.getParameters=function(){
return this.parameters;
};
nitobi.lang.defineNs("nitobi.html");
nitobi.html.XslRenderer=function(_39e){
nitobi.html.IRenderer.call(this,_39e);
};
nitobi.lang.implement(nitobi.html.XslRenderer,nitobi.html.IRenderer);
nitobi.html.XslRenderer.prototype.setTemplate=function(_39f){
if(typeof (_39f)==="string"){
_39f=nitobi.xml.createXslProcessor(_39f);
}
this.template=_39f;
};
nitobi.html.XslRenderer.prototype.renderToString=function(data){
if(typeof (data)==="string"){
data=nitobi.xml.createXmlDoc(data);
}
if(nitobi.lang.typeOf(data)===nitobi.lang.type.XMLNODE){
data=nitobi.xml.createXmlDoc(nitobi.xml.serialize(data));
}
var _3a1=this.getTemplate();
var _3a2=this.getParameters();
for(var p in _3a2){
_3a1.addParameter(p,_3a2[p],"");
}
var s=nitobi.xml.transformToString(data,_3a1,"xml");
for(var p in _3a2){
_3a1.addParameter(p,"","");
}
return s;
};
nitobi.lang.defineNs("nitobi.ui");
NTB_CSS_HIDE="nitobi-hide";
nitobi.ui.Element=function(id){
nitobi.ui.Element.baseConstructor.call(this);
nitobi.ui.IStyleable.call(this);
if(id!=null){
if(nitobi.lang.typeOf(id)==nitobi.lang.type.XMLNODE){
nitobi.base.ISerializable.call(this,id);
}else{
if($(id)!=null){
var decl=new nitobi.base.Declaration();
var _3a7=decl.loadHtml($(id));
var _3a8=$(id);
var _3a9=_3a8.parentNode;
var _3aa=_3a9.ownerDocument.createElement("ntb:component");
_3a9.insertBefore(_3aa,_3a8);
_3a9.removeChild(_3a8);
this.setContainer(_3aa);
nitobi.base.ISerializable.call(this,_3a7);
}else{
nitobi.base.ISerializable.call(this);
this.setId(id);
}
}
}else{
nitobi.base.ISerializable.call(this);
}
this.eventMap={};
this.onCreated=new nitobi.base.Event("created");
this.eventMap["created"]=this.onCreated;
this.onBeforeRender=new nitobi.base.Event("beforerender");
this.eventMap["beforerender"]=this.onBeforeRender;
this.onRender=new nitobi.base.Event("render");
this.eventMap["render"]=this.onRender;
this.onBeforeSetVisible=new nitobi.base.Event("beforesetvisible");
this.eventMap["beforesetvisible"]=this.onBeforeSetVisible;
this.onSetVisible=new nitobi.base.Event("setvisible");
this.eventMap["setvisible"]=this.onSetVisible;
this.onBeforePropagate=new nitobi.base.Event("beforepropagate");
this.onEventNotify=new nitobi.base.Event("eventnotify");
this.onBeforeEventNotify=new nitobi.base.Event("beforeeventnotify");
this.onBeforePropagateToChild=new nitobi.base.Event("beforepropogatetochild");
this.subscribeDeclarationEvents();
this.setEnabled(true);
this.renderer=new nitobi.html.XslRenderer();
};
nitobi.lang.extend(nitobi.ui.Element,nitobi.Object);
nitobi.lang.implement(nitobi.ui.Element,nitobi.base.ISerializable);
nitobi.lang.implement(nitobi.ui.Element,nitobi.ui.IStyleable);
nitobi.ui.Element.htmlNodeCache={};
nitobi.ui.Element.prototype.setHtmlNode=function(_3ab){
var node=$(_3ab);
this.htmlNode=node;
};
nitobi.ui.Element.prototype.getRootId=function(){
var _3ad=this.getParentObject();
if(_3ad==null){
return this.getId();
}else{
return _3ad.getRootId();
}
};
nitobi.ui.Element.prototype.getId=function(){
return this.getAttribute("id");
};
nitobi.ui.Element.parseId=function(id){
var ids=id.split(".");
return {localName:ids[1],id:ids[0]};
};
nitobi.ui.Element.prototype.setId=function(id){
this.setAttribute("id",id);
};
nitobi.ui.Element.prototype.notify=function(_3b1,id,_3b3,_3b4){
try{
_3b1=nitobi.html.getEvent(_3b1);
if(_3b4!==false){
nitobi.html.cancelEvent(_3b1);
}
var _3b5=nitobi.ui.Element.parseId(id).id;
if(!this.isDescendantExists(_3b5)){
return false;
}
var _3b6=!(_3b5==this.getId());
var _3b7=new nitobi.ui.ElementEventArgs(this,null,id);
var _3b8=new nitobi.ui.EventNotificationEventArgs(this,null,id,_3b1);
_3b6=_3b6&&this.onBeforePropagate.notify(_3b8);
var _3b9=true;
if(_3b6){
if(_3b3==null){
_3b3=this.getPathToLeaf(_3b5);
}
var _3ba=this.onBeforeEventNotify.notify(_3b8);
var _3bb=(_3ba?this.onEventNotify.notify(_3b8):true);
var _3bc=_3b3.pop().getAttribute("id");
var _3bd=this.getObjectById(_3bc);
var _3b9=this.onBeforePropagateToChild.notify(_3b8);
if(_3bd.notify&&_3b9&&_3bb){
_3b9=_3bd.notify(_3b1,id,_3b3,_3b4);
}
}else{
_3b9=this.onEventNotify.notify(_3b8);
}
var _3be=this.eventMap[_3b1.type];
if(_3be!=null&&_3b9){
_3be.notify(this.getEventArgs(_3b1,id));
}
return _3b9;
}
catch(err){
nitobi.lang.throwError(nitobi.error.Unexpected+" Element.notify encountered a problem.",err);
}
};
nitobi.ui.Element.prototype.getEventArgs=function(_3bf,_3c0){
var _3c1=new nitobi.ui.ElementEventArgs(this,null,_3c0);
return _3c1;
};
nitobi.ui.Element.prototype.subscribeDeclarationEvents=function(){
for(var name in this.eventMap){
var ev=this.getAttribute("on"+name);
if(ev!=null&&ev!=""){
this.eventMap[name].subscribe(ev,this,name);
}
}
};
nitobi.ui.Element.prototype.getHtmlNode=function(name){
var id=this.getId();
id=(name!=null?id+"."+name:id);
var node=nitobi.ui.Element.htmlNodeCache[name];
if(node==null){
node=$(id);
nitobi.ui.Element.htmlNodeCache[id]=node;
}
return node;
};
nitobi.ui.Element.prototype.flushHtmlNodeCache=function(){
nitobi.ui.Element.htmlNodeCache={};
};
nitobi.ui.Element.prototype.hide=function(_3c7,_3c8){
this.setVisible(false,_3c7,_3c8);
};
nitobi.ui.Element.prototype.show=function(_3c9,_3ca){
this.setVisible(true,_3c9,_3ca);
};
nitobi.ui.Element.prototype.isVisible=function(){
var node=this.getHtmlNode();
return node&&!nitobi.html.Css.hasClass(node,NTB_CSS_HIDE);
};
nitobi.ui.Element.prototype.setVisible=function(_3cc,_3cd,_3ce){
var _3cf=this.getHtmlNode();
if(_3cf&&this.isVisible()!=_3cc&&this.onBeforeSetVisible.notify({source:this,event:this.onBeforeSetVisible,args:arguments})!==false){
if(this.effect){
this.effect.end();
}
if(_3cc){
if(_3cd){
var _3d0=new _3cd(_3cf);
_3d0.callback=nitobi.lang.close(this,this.handleSetVisible,[_3ce]);
this.effect=_3d0;
_3d0.onFinish.subscribeOnce(nitobi.lang.close(this,function(){
this.effect=null;
}));
_3d0.start();
}else{
nitobi.html.Css.removeClass(_3cf,NTB_CSS_HIDE);
this.handleSetVisible(_3ce);
}
}else{
if(_3cd){
var _3d0=new _3cd(_3cf);
_3d0.callback=nitobi.lang.close(this,this.handleSetVisible,[_3ce]);
this.effect=_3d0;
_3d0.onFinish.subscribeOnce(nitobi.lang.close(this,function(){
this.effect=null;
}));
_3d0.start();
}else{
nitobi.html.Css.addClass(this.getHtmlNode(),NTB_CSS_HIDE);
this.handleSetVisible(_3ce);
}
}
}
};
nitobi.ui.Element.prototype.handleSetVisible=function(_3d1){
if(_3d1){
_3d1();
}
this.onSetVisible.notify(new nitobi.ui.ElementEventArgs(this,this.onSetVisible));
};
nitobi.ui.Element.prototype.setEnabled=function(_3d2){
this.enabled=_3d2;
};
nitobi.ui.Element.prototype.isEnabled=function(){
return this.enabled;
};
nitobi.ui.Element.prototype.render=function(_3d3,_3d4){
this.flushHtmlNodeCache();
_3d4=_3d4||this.getState();
_3d3=$(_3d3)||this.getContainer();
if(_3d3==null){
var _3d3=document.createElement("span");
document.body.appendChild(_3d3);
this.setContainer(_3d3);
}
this.htmlNode=this.renderer.renderIn(_3d3,_3d4)[0];
this.htmlNode.jsObject=this;
};
nitobi.ui.Element.prototype.getContainer=function(){
return this.container;
};
nitobi.ui.Element.prototype.setContainer=function(_3d5){
this.container=$(_3d5);
};
nitobi.ui.Element.prototype.getState=function(){
return this.getXmlNode();
};
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.ElementEventArgs=function(_3d6,_3d7,_3d8){
nitobi.ui.ElementEventArgs.baseConstructor.apply(this,arguments);
this.targetId=_3d8||null;
};
nitobi.lang.extend(nitobi.ui.ElementEventArgs,nitobi.base.EventArgs);
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.EventNotificationEventArgs=function(_3d9,_3da,_3db,_3dc){
nitobi.ui.EventNotificationEventArgs.baseConstructor.apply(this,arguments);
this.htmlEvent=_3dc||null;
};
nitobi.lang.extend(nitobi.ui.EventNotificationEventArgs,nitobi.ui.ElementEventArgs);
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.Container=function(id){
nitobi.ui.Container.baseConstructor.call(this,id);
nitobi.collections.IList.call(this);
};
nitobi.lang.extend(nitobi.ui.Container,nitobi.ui.Element);
nitobi.lang.implement(nitobi.ui.Container,nitobi.collections.IList);
nitobi.base.Registry.getInstance().register(new nitobi.base.Profile("nitobi.ui.Container",null,false,"ntb:container"));
nitobi.lang.defineNs("nitobi.ui");
NTB_CSS_SMALL="ntb-effects-small";
NTB_CSS_HIDE="nitobi-hide";
if(false){
nitobi.ui.Effects=function(){
};
}
nitobi.ui.Effects={};
nitobi.ui.Effects.setVisible=function(_3de,_3df,_3e0,_3e1,_3e2){
_3e1=(_3e2?nitobi.lang.close(_3e2,_3e1):_3e1)||nitobi.lang.noop;
_3de=$(_3de);
if(typeof _3e0=="string"){
_3e0=nitobi.effects.families[_3e0];
}
if(!_3e0){
_3e0=nitobi.effects.families["none"];
}
if(_3df){
var _3e3=_3e0.show;
}else{
var _3e3=_3e0.hide;
}
if(_3e3){
var _3e4=new _3e3(_3de);
_3e4.callback=_3e1;
_3e4.start();
}else{
if(_3df){
nitobi.html.Css.removeClass(_3de,NTB_CSS_HIDE);
}else{
nitobi.html.Css.addClass(_3de,NTB_CSS_HIDE);
}
_3e1();
}
};
nitobi.ui.Effects.shrink=function(_3e5,_3e6,_3e7,_3e8){
var rect=_3e6.getClientRects()[0];
_3e5.deltaHeight_Doctype=0-parseInt("0"+nitobi.html.getStyle(_3e6,"border-top-width"))-parseInt("0"+nitobi.html.getStyle(_3e6,"border-bottom-width"))-parseInt("0"+nitobi.html.getStyle(_3e6,"padding-top"))-parseInt("0"+nitobi.html.getStyle(_3e6,"padding-bottom"));
_3e5.deltaWidth_Doctype=0-parseInt("0"+nitobi.html.getStyle(_3e6,"border-left-width"))-parseInt("0"+nitobi.html.getStyle(_3e6,"border-right-width"))-parseInt("0"+nitobi.html.getStyle(_3e6,"padding-left"))-parseInt("0"+nitobi.html.getStyle(_3e6,"padding-right"));
_3e5.oldHeight=Math.abs(rect.top-rect.bottom)+_3e5.deltaHeight_Doctype;
_3e5.oldWidth=Math.abs(rect.right-rect.left)+_3e5.deltaWidth_Doctype;
if(!(typeof (_3e5.width)=="undefined")){
_3e5.deltaWidth=Math.floor(Math.ceil(_3e5.width-_3e5.oldWidth)/(_3e7/nitobi.ui.Effects.ANIMATION_INTERVAL));
}else{
_3e5.width=_3e5.oldWidth;
_3e5.deltaWidth=0;
}
if(!(typeof (_3e5.height)=="undefined")){
_3e5.deltaHeight=Math.floor(Math.ceil(_3e5.height-_3e5.oldHeight)/(_3e7/nitobi.ui.Effects.ANIMATION_INTERVAL));
}else{
_3e5.height=_3e5.oldHeight;
_3e5.deltaHeight=0;
}
nitobi.ui.Effects.resize(_3e5,_3e6,_3e7,_3e8);
};
nitobi.ui.Effects.resize=function(_3ea,_3eb,_3ec,_3ed){
var rect=_3eb.getClientRects()[0];
var _3ef=Math.abs(rect.top-rect.bottom);
var _3f0=Math.max(_3ef+_3ea.deltaHeight+_3ea.deltaHeight_Doctype,0);
if(Math.abs(_3ef-_3ea.height)<Math.abs(_3ea.deltaHeight)){
_3f0=_3ea.height;
_3ea.deltaHeight=0;
}
var _3f1=Math.abs(rect.right-rect.left);
var _3f2=Math.max(_3f1+_3ea.deltaWidth+_3ea.deltaWidth_Doctype,0);
_3f2=(_3f2>=0)?_3f2:0;
if(Math.abs(_3f1-_3ea.width)<Math.abs(_3ea.deltaWidth)){
_3f2=_3ea.width;
_3ea.deltaWidth=0;
}
_3ec-=nitobi.ui.Effects.ANIMATION_INTERVAL;
if(_3ec>0){
window.setTimeout(nitobi.lang.closeLater(this,nitobi.ui.Effects.resize,[_3ea,_3eb,_3ec,_3ed]),nitobi.ui.Effects.ANIMATION_INTERVAL);
}
var _3f3=function(){
_3eb.height=_3f0+"px";
_3eb.style.height=_3f0+"px";
_3eb.width=_3f2+"px";
_3eb.style.width=_3f2+"px";
if(_3ec<=0){
if(_3ed){
window.setTimeout(_3ed,0);
}
}
};
nitobi.ui.Effects.executeNextPulse.push(_3f3);
};
nitobi.ui.Effects.executeNextPulse=new Array();
nitobi.ui.Effects.pulse=function(){
var p;
while(p=nitobi.ui.Effects.executeNextPulse.pop()){
p.call();
}
};
nitobi.ui.Effects.PULSE_INTERVAL=20;
nitobi.ui.Effects.ANIMATION_INTERVAL=40;
window.setInterval(nitobi.ui.Effects.pulse,nitobi.ui.Effects.PULSE_INTERVAL);
window.setTimeout(nitobi.ui.Effects.pulse,nitobi.ui.Effects.PULSE_INTERVAL);
nitobi.ui.Effects.fadeIntervalId={};
nitobi.ui.Effects.fadeIntervalTime=10;
nitobi.ui.Effects.cube=function(_3f5){
return _3f5*_3f5*_3f5;
};
nitobi.ui.Effects.cubeRoot=function(_3f6){
var T=0;
var N=parseFloat(_3f6);
if(N<0){
N=-N;
T=1;
}
var M=Math.sqrt(N);
var ctr=1;
while(ctr<101){
var M=M*N;
var M=Math.sqrt(Math.sqrt(M));
ctr++;
}
return M;
};
nitobi.ui.Effects.linear=function(_3fb){
return _3fb;
};
nitobi.ui.Effects.fade=function(_3fc,_3fd,time,_3ff,_400){
_400=_400||nitobi.ui.Effects.linear;
var _401=(new Date()).getTime()+time;
var id=nitobi.component.getUniqueId();
var _403=(new Date()).getTime();
var el=_3fc;
if(_3fc.length){
el=_3fc[0];
}
var _405=nitobi.html.Css.getOpacity(el);
var _406=(_3fd-_405<0?-1:0);
nitobi.ui.Effects.fadeIntervalId[id]=window.setInterval(function(){
nitobi.ui.Effects.stepFade(_3fc,_3fd,_403,_401,id,_3ff,_400,_406);
},nitobi.ui.Effects.fadeIntervalTime);
};
nitobi.ui.Effects.stepFade=function(_407,_408,_409,_40a,id,_40c,_40d,_40e){
var ct=(new Date()).getTime();
var _410=_40a-_409;
var nct=((ct-_409)/(_40a-_409));
if(nct<=0||nct>=1){
nitobi.html.Css.setOpacities(_407,_408);
window.clearInterval(nitobi.ui.Effects.fadeIntervalId[id]);
_40c();
return;
}else{
nct=Math.abs(nct+_40e);
}
var no=_40d(nct);
nitobi.html.Css.setOpacities(_407,no*100);
};
nitobi.lang.defineNs("nitobi.component");
if(false){
nitobi.component=function(){
};
}
nitobi.loadComponent=function(el){
var id=el;
el=$(el);
if(el==null){
nitobi.lang.throwError("nitobi.loadComponent could not load the component because it could not be found on the page. The component may not have a declaration, node, or it may have a duplicated id. Id: "+id);
}
if(el.jsObject!=null){
return el.jsObject;
}
var _415;
var _416=nitobi.html.getTagName(el);
if(_416=="ntb:grid"){
_415=nitobi.initGrid(el.id);
}else{
if(_416==="ntb:combo"){
_415=nitobi.initCombo(el.id);
}else{
if(el.jsObject==null){
_415=nitobi.base.Factory.getInstance().createByTag(_416,el.id,nitobi.component.renderComponent);
if(_415.render&&!_415.onLoadCallback){
_415.render();
}
}else{
_415=el.jsObject;
}
}
}
return _415;
};
nitobi.component.renderComponent=function(_417){
_417.source.render();
};
nitobi.getComponent=function(id){
var el=$(id);
if(el==null){
return null;
}
return el.jsObject;
};
nitobi.component.uniqueId=0;
nitobi.component.getUniqueId=function(){
return "ntbcmp_"+(nitobi.component.uniqueId++);
};
nitobi.component.findNitobiComponents=function(_41a,_41b){
if(nitobi.component.isNitobiElement(_41a)){
_41b.push(_41a);
return;
}
var _41c=_41a.childNodes;
for(var i=0;i<_41c.length;i++){
nitobi.component.findNitobiComponents(_41c[i],_41b);
}
return;
};
nitobi.component.isNitobiElement=function(_41e){
var _41f=nitobi.html.getTagName(_41e);
if(_41f.substr(0,3)=="ntb"){
return true;
}else{
return false;
}
};
nitobi.component.loadComponentsFromNode=function(_420){
var _421=new Array();
nitobi.component.findNitobiComponents(_420,_421);
for(var i=0;i<_421.length;i++){
nitobi.loadComponent(_421[i].getAttribute("id"));
}
};
nitobi.lang.defineNs("nitobi.effects");
if(false){
nitobi.effects=function(){
};
}
nitobi.effects.Effect=function(_423,_424){
this.element=$(_423);
this.transition=_424.transition||nitobi.effects.Transition.sinoidal;
this.duration=_424.duration||1;
this.fps=_424.fps||50;
this.from=typeof (_424.from)==="number"?_424.from:0;
this.to=typeof (_424.from)==="number"?_424.to:1;
this.delay=_424.delay||0;
this.callback=typeof (_424.callback)==="function"?_424.callback:nitobi.lang.noop;
this.queue=_424.queue||nitobi.effects.EffectQueue.globalQueue;
this.onBeforeFinish=new nitobi.base.Event();
this.onFinish=new nitobi.base.Event();
this.onBeforeStart=new nitobi.base.Event();
};
nitobi.effects.Effect.prototype.start=function(){
var now=new Date().getTime();
this.startOn=now+this.delay*1000;
this.finishOn=this.startOn+this.duration*1000;
this.deltaTime=this.duration*1000;
this.totalFrames=this.duration*this.fps;
this.frame=0;
this.delta=this.from-this.to;
this.queue.add(this);
};
nitobi.effects.Effect.prototype.render=function(pos){
if(!this.running){
this.onBeforeStart.notify(new nitobi.base.EventArgs(this,this.onBeforeStart));
this.setup();
this.running=true;
}
this.update(this.transition(pos*this.delta+this.from));
};
nitobi.effects.Effect.prototype.step=function(now){
if(this.startOn<=now){
if(now>=this.finishOn){
this.end();
return;
}
var pos=(now-this.startOn)/(this.deltaTime);
var _429=Math.floor(pos*this.totalFrames);
if(this.frame<_429){
this.render(pos);
this.frame=_429;
}
}
};
nitobi.effects.Effect.prototype.setup=function(){
};
nitobi.effects.Effect.prototype.update=function(pos){
};
nitobi.effects.Effect.prototype.finish=function(){
};
nitobi.effects.Effect.prototype.end=function(){
this.onBeforeFinish.notify(new nitobi.base.EventArgs(this,this.onBeforeFinish));
this.cancel();
this.render(1);
this.running=false;
this.finish();
this.callback();
this.onFinish.notify(new nitobi.base.EventArgs(this,this.onAfterFinish));
};
nitobi.effects.Effect.prototype.cancel=function(){
this.queue.remove(this);
};
nitobi.effects.factory=function(_42b,_42c,etc){
var args=nitobi.lang.toArray(arguments,2);
return function(_42f){
var f=function(){
_42b.apply(this,[_42f,_42c].concat(args));
};
nitobi.lang.extend(f,_42b);
return new f();
};
};
nitobi.effects.families={none:{show:null,hide:null}};
nitobi.lang.defineNs("nitobi.effects");
if(false){
nitobi.effects.Transition=function(){
};
}
nitobi.effects.Transition={};
nitobi.effects.Transition.sinoidal=function(x){
return (-Math.cos(x*Math.PI)/2)+0.5;
};
nitobi.effects.Transition.linear=function(x){
return x;
};
nitobi.effects.Transition.reverse=function(x){
return 1-x;
};
nitobi.lang.defineNs("nitobi.effects");
nitobi.effects.Scale=function(_434,_435,_436){
nitobi.effects.Scale.baseConstructor.call(this,_434,_435);
this.scaleX=typeof (_435.scaleX)=="boolean"?_435.scaleX:true;
this.scaleY=typeof (_435.scaleY)=="boolean"?_435.scaleY:true;
this.scaleFrom=typeof (_435.scaleFrom)=="number"?_435.scaleFrom:100;
this.scaleTo=_436;
};
nitobi.lang.extend(nitobi.effects.Scale,nitobi.effects.Effect);
nitobi.effects.Scale.prototype.setup=function(){
var _437=this.element.style;
this.originalStyle={"top":_437.top,"left":_437.left,"width":_437.width,"height":_437.height,"overflow":_437.overflow};
this.factor=(this.scaleTo-this.scaleFrom)/100;
this.dims=[this.element.scrollWidth,this.element.scrollHeight];
_437.width=this.dims[0]+"px";
_437.height=this.dims[1]+"px";
_437.overflow="hidden";
};
nitobi.effects.Scale.prototype.finish=function(){
for(var s in this.originalStyle){
this.element.style[s]=this.originalStyle[s];
}
};
nitobi.effects.Scale.prototype.update=function(pos){
var _43a=(this.scaleFrom/100)+(this.factor*pos);
this.setDimensions(Math.floor(_43a*this.dims[0])||1,Math.floor(_43a*this.dims[1])||1);
};
nitobi.effects.Scale.prototype.setDimensions=function(x,y){
if(this.scaleX){
this.element.style.width=x+"px";
}
if(this.scaleY){
this.element.style.height=y+"px";
}
};
nitobi.lang.defineNs("nitobi.effects");
nitobi.effects.EffectQueue=function(){
nitobi.effects.EffectQueue.baseConstructor.call(this);
nitobi.collections.IEnumerable.call(this);
this.intervalId=0;
};
nitobi.lang.extend(nitobi.effects.EffectQueue,nitobi.Object);
nitobi.lang.implement(nitobi.effects.EffectQueue,nitobi.collections.IEnumerable);
nitobi.effects.EffectQueue.prototype.add=function(_43d){
nitobi.collections.IEnumerable.prototype.add.call(this,_43d);
if(!this.intervalId){
this.intervalId=window.setInterval(nitobi.lang.close(this,this.step),15);
}
};
nitobi.effects.EffectQueue.prototype.step=function(){
var now=new Date().getTime();
this.each(function(e){
e.step(now);
});
};
nitobi.effects.EffectQueue.globalQueue=new nitobi.effects.EffectQueue();
nitobi.lang.defineNs("nitobi.effects");
nitobi.effects.BlindUp=function(_440,_441){
_441=nitobi.lang.merge({scaleX:false,duration:Math.min(0.2*(_440.scrollHeight/100),0.5)},_441||{});
nitobi.effects.BlindUp.baseConstructor.call(this,_440,_441,0);
};
nitobi.lang.extend(nitobi.effects.BlindUp,nitobi.effects.Scale);
nitobi.effects.BlindUp.prototype.setup=function(){
nitobi.effects.BlindUp.base.setup.call(this);
};
nitobi.effects.BlindUp.prototype.finish=function(){
nitobi.html.Css.addClass(this.element,NTB_CSS_HIDE);
nitobi.effects.BlindUp.base.finish.call(this);
this.element.style.height="";
};
nitobi.effects.BlindDown=function(_442,_443){
nitobi.html.Css.swapClass(_442,NTB_CSS_HIDE,NTB_CSS_SMALL);
_443=nitobi.lang.merge({scaleX:false,scaleFrom:0,duration:Math.min(0.2*(_442.scrollHeight/100),0.5)},_443||{});
nitobi.effects.BlindDown.baseConstructor.call(this,_442,_443,100);
};
nitobi.lang.extend(nitobi.effects.BlindDown,nitobi.effects.Scale);
nitobi.effects.BlindDown.prototype.setup=function(){
nitobi.effects.BlindDown.base.setup.call(this);
this.element.style.height="1px";
nitobi.html.Css.removeClass(this.element,NTB_CSS_SMALL);
};
nitobi.effects.BlindDown.prototype.finish=function(){
nitobi.effects.BlindDown.base.finish.call(this);
this.element.style.height="";
};
nitobi.effects.families.blind={show:nitobi.effects.BlindDown,hide:nitobi.effects.BlindUp};
nitobi.lang.defineNs("nitobi.effects");
nitobi.effects.ShadeUp=function(_444,_445){
_445=nitobi.lang.merge({scaleX:false,duration:Math.min(0.2*(_444.scrollHeight/100),0.3)},_445||{});
nitobi.effects.ShadeUp.baseConstructor.call(this,_444,_445,0);
};
nitobi.lang.extend(nitobi.effects.ShadeUp,nitobi.effects.Scale);
nitobi.effects.ShadeUp.prototype.setup=function(){
nitobi.effects.ShadeUp.base.setup.call(this);
var _446=nitobi.html.getFirstChild(this.element);
this.originalStyle.position=this.element.style.position;
nitobi.html.position(this.element);
if(_446){
var _447=_446.style;
this.fnodeStyle={position:_447.position,bottom:_447.bottom,left:_447.left};
this.fnode=_446;
_447.position="absolute";
_447.bottom="0px";
_447.left="0px";
}
};
nitobi.effects.ShadeUp.prototype.finish=function(){
nitobi.effects.ShadeUp.base.finish.call(this);
nitobi.html.Css.addClass(this.element,NTB_CSS_HIDE);
this.element.style.height="";
this.element.style.position=this.originalStyle.position;
this.element.style.overflow=this.originalStyle.overflow;
for(var x in this.fnodeStyle){
this.fnode.style[x]=this.fnodeStyle[x];
}
};
nitobi.effects.ShadeDown=function(_449,_44a){
nitobi.html.Css.swapClass(_449,NTB_CSS_HIDE,NTB_CSS_SMALL);
_44a=nitobi.lang.merge({scaleX:false,scaleFrom:0,duration:Math.min(0.2*(_449.scrollHeight/100),0.3)},_44a||{});
nitobi.effects.ShadeDown.baseConstructor.call(this,_449,_44a,100);
};
nitobi.lang.extend(nitobi.effects.ShadeDown,nitobi.effects.Scale);
nitobi.effects.ShadeDown.prototype.setup=function(){
nitobi.effects.ShadeDown.base.setup.call(this);
this.element.style.height="1px";
nitobi.html.Css.removeClass(this.element,NTB_CSS_SMALL);
var _44b=nitobi.html.getFirstChild(this.element);
this.originalStyle.position=this.element.style.position;
nitobi.html.position(this.element);
if(_44b){
var _44c=_44b.style;
this.fnodeStyle={position:_44c.position,bottom:_44c.bottom,left:_44c.left,right:_44c.right,top:_44c.top};
this.fnode=_44b;
_44c.position="absolute";
_44c.top="";
_44c.right="";
_44c.bottom="0px";
_44c.left="0px";
}
};
nitobi.effects.ShadeDown.prototype.finish=function(){
nitobi.effects.ShadeDown.base.finish.call(this);
this.element.style.height="";
this.element.style.position=this.originalStyle.position;
this.element.style.overflow=this.originalStyle.overflow;
for(var x in this.fnodeStyle){
this.fnode.style[x]=this.fnodeStyle[x];
}
this.fnode.style.top="0px";
this.fnode.style.left="0px";
this.fnode.style.bottom="";
this.fnode.style.right="";
return;
this.fnode.style["position"]="";
};
nitobi.effects.families.shade={show:nitobi.effects.ShadeDown,hide:nitobi.effects.ShadeUp};
nitobi.lang.defineNs("nitobi.lang");
nitobi.lang.StringBuilder=function(_44e){
if(_44e){
if(typeof (_44e)==="string"){
this.strings=[_44e];
}else{
this.strings=_44e;
}
}else{
this.strings=new Array();
}
};
nitobi.lang.StringBuilder.prototype.append=function(_44f){
if(_44f){
this.strings.push(_44f);
}
return this;
};
nitobi.lang.StringBuilder.prototype.clear=function(){
this.strings.length=0;
};
nitobi.lang.StringBuilder.prototype.toString=function(){
return this.strings.join("");
};


var temp_ntb_uniqueIdGeneratorProc='<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" /> <x:p-x:n-guid"x:s-0"/><x:t- match="/"> <x:at-/></x:t-><x:t- match="node()|@*"> <xsl:copy> <xsl:if test="not(@id)"> <x:a-x:n-id" ><x:v-x:s-generate-id(.)"/><x:v-x:s-position()"/><x:v-x:s-$guid"/></x:a-> </xsl:if> <x:at-x:s-./* | text() | @*"> </x:at-> </xsl:copy></x:t-> <x:t- match="text()"> <x:v-x:s-."/></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.base");
nitobi.base.uniqueIdGeneratorProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_uniqueIdGeneratorProc));


