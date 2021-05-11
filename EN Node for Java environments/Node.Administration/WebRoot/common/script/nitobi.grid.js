if(typeof (nitobi)=="undefined"||typeof (nitobi.lang)=="undefined"){
alert("The Nitobi framework source could not be found. Is it included before any other Nitobi components?");
}
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.build="6257";
nitobi.grid.version="3.5.6257";
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.Scrollbar=function(){
this.uid="scroll"+nitobi.base.getUid();
};
nitobi.ui.Scrollbar.prototype.render=function(){
};
nitobi.ui.Scrollbar.prototype.attachToParent=function(_1,_2,_3){
this.UiContainer=_1;
this.element=_2||nitobi.html.getFirstChild(this.UiContainer);
if(this.element==null){
this.render();
}
this.surface=_3||nitobi.html.getFirstChild(this.element);
this.element.onclick="";
this.element.onmouseover="";
this.element.onmouseout="";
this.element.onscroll="";
nitobi.html.attachEvent(this.element,"scroll",this.scrollByUser,this);
};
nitobi.ui.Scrollbar.prototype.align=function(){
var vs=document.getElementById("vscroll"+this.uid);
var dx=-1;
if(nitobi.browser.MOZ){
dx=-3;
}
nitobi.drawing.align(vs,this.UiContainer.childNodes[0],269484288,-42,0,24,dx,false);
};
nitobi.ui.Scrollbar.prototype.scrollByUser=function(){
this.fire("ScrollByUser",this.getScrollPercent());
};
nitobi.ui.Scrollbar.prototype.setScroll=function(_6){
};
nitobi.ui.Scrollbar.prototype.getScrollPercent=function(){
};
nitobi.ui.Scrollbar.prototype.setRange=function(_7){
};
nitobi.ui.Scrollbar.prototype.getWidth=function(){
return nitobi.html.getScrollBarWidth();
};
nitobi.ui.Scrollbar.prototype.getHeight=function(){
return nitobi.html.getScrollBarWidth();
};
nitobi.ui.Scrollbar.prototype.fire=function(_8,_9){
return nitobi.event.notify(_8+this.uid,_9);
};
nitobi.ui.Scrollbar.prototype.subscribe=function(_a,_b,_c){
if(typeof (_c)=="undefined"){
_c=this;
}
return nitobi.event.subscribe(_a+this.uid,nitobi.lang.close(_c,_b));
};
nitobi.ui.VerticalScrollbar=function(){
this.uid="vscroll"+nitobi.base.getUid();
};
nitobi.lang.extend(nitobi.ui.VerticalScrollbar,nitobi.ui.Scrollbar);
nitobi.ui.VerticalScrollbar.prototype.setScrollPercent=function(_d){
this.element.scrollTop=(this.surface.offsetHeight-this.element.offsetHeight)*_d;
return false;
};
nitobi.ui.VerticalScrollbar.prototype.getScrollPercent=function(){
return (this.element.scrollTop/(this.surface.offsetHeight-this.element.offsetHeight));
};
nitobi.ui.VerticalScrollbar.prototype.setRange=function(_e){
var st=this.element.scrollTop;
this.surface.style.height=Math.floor(this.element.offsetHeight/_e)+"px";
this.element.scrollTop=st;
this.element.scrollTop=this.element.scrollTop;
};
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.HorizontalScrollbar=function(){
this.uid="hscroll"+nitobi.base.getUid();
};
nitobi.lang.extend(nitobi.ui.HorizontalScrollbar,nitobi.ui.Scrollbar);
nitobi.ui.HorizontalScrollbar.prototype.getScrollPercent=function(){
return (this.element.scrollLeft/(this.surface.clientWidth-this.element.clientWidth));
};
nitobi.ui.HorizontalScrollbar.prototype.setScrollPercent=function(_10){
this.element.scrollLeft=(this.surface.clientWidth-this.element.clientWidth)*_10;
return false;
};
nitobi.ui.HorizontalScrollbar.prototype.setRange=function(_11){
this.surface.style.width=Math.floor(this.element.offsetWidth/_11)+"px";
};
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.IDataBoundList=function(){
};
nitobi.ui.IDataBoundList.prototype.getGetHandler=function(){
return this.getHandler;
};
nitobi.ui.IDataBoundList.prototype.setGetHandler=function(_12){
this.column.ModelNode.setAttribute("GetHandler",_12);
this.getHandler=_12;
};
nitobi.ui.IDataBoundList.prototype.getDataSourceId=function(){
return this.datasourceId;
};
nitobi.ui.IDataBoundList.prototype.setDataSourceId=function(_13){
this.column.ModelNode.setAttribute("DatasourceId",_13);
this.datasourceId=_13;
};
nitobi.ui.IDataBoundList.prototype.getDisplayFields=function(){
return this.displayFields;
};
nitobi.ui.IDataBoundList.prototype.setDisplayFields=function(_14){
this.column.ModelNode.setAttribute("DisplayFields",_14);
this.displayFields=_14;
};
nitobi.ui.IDataBoundList.prototype.getValueField=function(){
return this.valueField;
};
nitobi.ui.IDataBoundList.prototype.setValueField=function(_15){
this.column.ModelNode.setAttribute("ValueField",_15);
this.valueField=_15;
};
if(typeof (nitobi.collections)=="undefined"){
nitobi.collections={};
}
nitobi.collections.CacheMap=function(){
this.tail=null;
this.debug=new Array();
};
nitobi.collections.CacheMap.prototype.insert=function(low,_17){
low=Number(low);
_17=Number(_17);
this.debug.push("insert("+low+","+_17+")");
var _18=new nitobi.collections.CacheNode(low,_17);
if(this.head==null){
this.debug.push("empty cache, adding first node");
this.head=_18;
this.tail=_18;
}else{
var n=this.head;
while(n!=null&&low>n.high+1){
n=n.next;
}
if(n==null){
this.debug.push("appending node to end");
this.tail.next=_18;
_18.prev=this.tail;
this.tail=_18;
}else{
this.debug.push("inserting new node before "+n.toString());
if(n.prev!=null){
_18.prev=n.prev;
n.prev.next=_18;
}
_18.next=n;
n.prev=_18;
while(_18.mergeNext()){
}
if(_18.prev==null){
this.head=_18;
}
if(_18.next==null){
this.tail=_18;
}
}
}
};
nitobi.collections.CacheMap.prototype.remove=function(low,_1b){
low=Number(low);
_1b=Number(_1b);
this.debug.push("insert("+low+","+_1b+")");
if(this.head==null){
}else{
if(_1b<this.head.low||low>this.tail.high){
return;
}
var _1c=this.head;
while(_1c!=null&&low>_1c.high){
_1c=_1c.next;
}
if(_1c==null){
this.debug.push("the range was not found");
}else{
var end=_1c;
var _1e=null;
while(end!=null&&_1b>end.high){
if((end.next!=null&&_1b<end.next.low)||end.next==null){
break;
}
_1e=end.next;
if(end!=_1c){
this.removeNode(end);
}
end=_1e;
}
if(_1c!=end){
if(_1b>=end.high){
this.removeNode(end);
}
if(low<=_1c.low){
this.removeNode(_1c);
}
}else{
if(_1c.low>=low&&_1c.high<=_1b){
this.removeNode(_1c);
return;
}else{
if(low>_1c.low&&_1b<_1c.high){
var _1f=_1c.low;
var _20=_1c.high;
this.removeNode(_1c);
this.insert(_1f,low-1);
this.insert(_1b+1,_20);
return;
}
}
}
if(end!=null&&_1b<end.high){
end.low=_1b+1;
}
if(_1c!=null&&low>_1c.low){
_1c.high=low-1;
}
}
}
};
nitobi.collections.CacheMap.prototype.gaps=function(low,_22){
var g=new Array();
var n=this.head;
if(n==null||n.low>_22||this.tail.high<low){
g.push(new nitobi.collections.Range(low,_22));
return g;
}
var _25=0;
while(n!=null&&n.high<low){
_25=n.high+1;
n=n.next;
}
if(n!=null){
do{
if(g.length==0){
if(low<n.low){
g.push(new nitobi.collections.Range(Math.max(low,_25),Math.min(n.low-1,_22)));
}
}
if(_22>n.high){
if(n.next==null||n.next.low>_22){
g.push(new nitobi.collections.Range(n.high+1,_22));
}else{
g.push(new nitobi.collections.Range(n.high+1,n.next.low-1));
}
}
n=n.next;
}while(n!=null&&n.high<_22);
}else{
g.push(new nitobi.collections.Range(this.tail.high+1,_22));
}
return g;
};
nitobi.collections.CacheMap.prototype.ranges=function(low,_27){
var g=new Array();
var n=this.head;
if(n==null||n.low>_27||this.tail.high<low){
return g;
}
while(n!=null&&n.high<low){
minLow=n.high+1;
n=n.next;
}
if(n!=null){
do{
g.push(new nitobi.collections.Range(n.low,n.high));
n=n.next;
}while(n!=null&&n.high<_27);
}
return g;
};
nitobi.collections.CacheMap.prototype.gapsString=function(low,_2b){
var gs=this.gaps(low,_2b);
var a=new Array();
for(var i=0;i<gs.length;i++){
a.push(gs[i].toString());
}
return a.join(",");
};
nitobi.collections.CacheMap.prototype.removeNode=function(_2f){
if(_2f.prev!=null){
_2f.prev.next=_2f.next;
}else{
this.head=_2f.next;
}
if(_2f.next!=null){
_2f.next.prev=_2f.prev;
}else{
this.tail=_2f.prev;
}
_2f=null;
};
nitobi.collections.CacheMap.prototype.toString=function(){
var n=this.head;
var s=new Array();
while(n!=null){
s.push(n.toString());
n=n.next;
}
return s.join(",");
};
nitobi.collections.CacheMap.prototype.flush=function(){
var _32=this.head;
while(Boolean(_32)){
var _33=_32.next;
delete (_32);
_32=_33;
}
this.head=null;
this.tail=null;
};
nitobi.collections.CacheMap.prototype.insertIntoRange=function(_34){
var n=this.head;
var inc=0;
while(n!=null){
if(_34>=n.low&&_34<=n.high){
inc=1;
n.high+=inc;
}else{
n.low+=inc;
n.high+=inc;
}
n=n.next;
}
if(inc==0){
this.insert(_34,_34);
}
};
nitobi.collections.CacheMap.prototype.removeFromRange=function(_37){
var n=this.head;
var inc=0;
while(n!=null){
if(_37>=n.low&&_37<=n.high){
inc=-1;
if(n.low==n.high){
this.remove(_37,_37);
}else{
n.high+=inc;
}
}else{
n.low+=inc;
n.high+=inc;
}
n=n.next;
}
};
nitobi.lang.defineNs("nitobi.collections");
nitobi.collections.BlockMap=function(){
this.head=null;
this.tail=null;
this.debug=new Array();
};
nitobi.lang.extend(nitobi.collections.BlockMap,nitobi.collections.CacheMap);
nitobi.collections.BlockMap.prototype.insert=function(low,_3b){
low=Number(low);
_3b=Number(_3b);
this.debug.push("insert("+low+","+_3b+")");
if(this.head==null){
var _3c=new nitobi.collections.CacheNode(low,_3b);
this.debug.push("empty cache, adding first node");
this.head=_3c;
this.tail=_3c;
}else{
var n=this.head;
while(n!=null&&low>n.high){
n=n.next;
}
if(n==null){
var _3c=new nitobi.collections.CacheNode(low,_3b);
this.debug.push("appending node to end");
this.tail.next=_3c;
_3c.prev=this.tail;
this.tail=_3c;
}else{
this.debug.push("inserting new node into or before "+n.toString());
if(low<n.low||_3b>n.high){
if(low<n.low){
var _3c=new nitobi.collections.CacheNode(low,_3b);
_3c.prev=n.prev;
_3c.next=n;
if(n.prev!=null){
n.prev.next=_3c;
}
n.prev=_3c;
_3c.high=Math.min(_3c.high,n.low-1);
}else{
var _3c=new nitobi.collections.CacheNode(n.high+1,_3b);
_3c.prev=n;
_3c.next=n.next;
if(n.next!=null){
n.next.prev=_3c;
_3c.high=Math.min(_3b,_3c.next.low-1);
}
n.next=_3c;
}
if(_3c.prev==null){
this.head=_3c;
}
if(_3c.next==null){
this.tail=_3c;
}
}
}
}
};
nitobi.collections.BlockMap.prototype.blocks=function(low,_3f){
var g=new Array();
var n=this.head;
if(n==null||n.low>_3f||this.tail.high<low){
g.push(new nitobi.collections.Range(low,_3f));
return g;
}
var _42=0;
while(n!=null&&n.high<low){
_42=n.high+1;
n=n.next;
}
if(n!=null){
do{
if(g.length==0){
if(low<n.low){
g.push(new nitobi.collections.Range(Math.max(low,_42),Math.min(n.low-1,_3f)));
}
}
if(_3f>n.high){
if(n.next==null||n.next.low>_3f){
g.push(new nitobi.collections.Range(n.high+1,_3f));
}else{
g.push(new nitobi.collections.Range(n.high+1,n.next.low-1));
}
}
n=n.next;
}while(n!=null&&n.high<_3f);
}else{
g.push(new nitobi.collections.Range(this.tail.high+1,_3f));
}
return g;
};
nitobi.lang.defineNs("nitobi.collections");
nitobi.collections.CellSet=function(_43,_44,_45,_46,_47){
this.owner=_43;
if(_44!=null&&_45!=null&&_46!=null&&_47!=null){
this.setRange(_44,_45,_46,_47);
}else{
this.setRange(0,0,0,0);
}
};
nitobi.collections.CellSet.prototype.toString=function(){
var str="";
for(var i=this._topRow;i<=this._bottomRow;i++){
str+="[";
for(var j=this._leftColumn;j<=this._rightColumn;j++){
str+="("+i+","+j+")";
}
str+="]";
}
return str;
};
nitobi.collections.CellSet.prototype.setRange=function(_4b,_4c,_4d,_4e){
this._startRow=_4b;
this._startColumn=_4c;
this._endRow=_4d;
this._endColumn=_4e;
this._leftColumn=Math.min(_4c,_4e);
this._rightColumn=Math.max(_4c,_4e);
this._topRow=Math.min(_4b,_4d);
this._bottomRow=Math.max(_4b,_4d);
};
nitobi.collections.CellSet.prototype.changeStartCell=function(_4f,_50){
this._startRow=_4f;
this._startColumn=_50;
this._leftColumn=Math.min(_50,this._endColumn);
this._rightColumn=Math.max(_50,this._endColumn);
this._topRow=Math.min(_4f,this._endRow);
this._bottomRow=Math.max(_4f,this._endRow);
};
nitobi.collections.CellSet.prototype.changeEndCell=function(_51,_52){
this._endRow=_51;
this._endColumn=_52;
this._leftColumn=Math.min(_52,this._startColumn);
this._rightColumn=Math.max(_52,this._startColumn);
this._topRow=Math.min(_51,this._startRow);
this._bottomRow=Math.max(_51,this._startRow);
};
nitobi.collections.CellSet.prototype.getRowCount=function(){
return this._bottomRow-this._topRow+1;
};
nitobi.collections.CellSet.prototype.getColumnCount=function(){
return this._rightColumn-this._leftColumn+1;
};
nitobi.collections.CellSet.prototype.getCoords=function(){
return {"top":new nitobi.drawing.Point(this._leftColumn,this._topRow),"bottom":new nitobi.drawing.Point(this._rightColumn,this._bottomRow)};
};
nitobi.collections.CellSet.prototype.getCellObjectByOffset=function(_53,_54){
return this.owner.getCellObject(this._topRow+_53,this._leftColumn+_54);
};
if(typeof (nitobi.collections)=="undefined"){
nitobi.collections={};
}
nitobi.collections.CacheNode=function(low,_56){
this.low=low;
this.high=_56;
this.next=null;
this.prev=null;
};
nitobi.collections.CacheNode.prototype.isIn=function(val){
return ((val>=this.low)&&(val<=this.high));
};
nitobi.collections.CacheNode.prototype.mergeNext=function(){
var _58=this.next;
if(_58!=null&&_58.low<=this.high+1){
this.high=Math.max(this.high,_58.high);
this.low=Math.min(this.low,_58.low);
var _59=_58.next;
this.next=_59;
if(_59!=null){
_59.prev=this;
}
_58.clear();
return true;
}else{
return false;
}
};
nitobi.collections.CacheNode.prototype.clear=function(){
this.next=null;
this.prev=null;
};
nitobi.collections.CacheNode.prototype.toString=function(){
return "["+this.low+","+this.high+"]";
};
if(typeof (nitobi.collections)=="undefined"){
nitobi.collections={};
}
nitobi.collections.Range=function(low,_5b){
this.low=low;
this.high=_5b;
};
nitobi.collections.Range.prototype.isIn=function(val){
return ((val>=this.low)&&(val<=this.high));
};
nitobi.collections.Range.prototype.toString=function(){
return "["+this.low+","+this.high+"]";
};
nitobi.prepare=function(){
ebagdl=0;
ebagd1=9999999999999;
s="var d = new Date().getTime();if ((d<"+ebagdl+") || (d>"+ebagd1+")) {alert('Evaluation period has expired.\\n\\nPlease notify your system administrator.\\n\\nPurchase Information:\\n       NITOBI SOFTWARE\\n\\n       www.nitobi.com\\n       sales@nitobi.com         \\n       Telephone: (604) 685-9287\\n       Fax: (604) 648-9090\\n       Toll-Free: 1-866-6EB-APPS\\n                      (1-866-632-2777)');}";
eval(s);
};
nitobi.lang.defineNs("nitobi.grid");
if(false){
nitobi.grid=function(){
};
}
nitobi.grid.PAGINGMODE_NONE="none";
nitobi.grid.PAGINGMODE_STANDARD="standard";
nitobi.grid.PAGINGMODE_LIVESCROLLING="livescrolling";
nitobi.grid.Grid=function(uid){
nitobi.prepare();
EBAAutoRender=false;
this.disposal=[];
this.uid=uid||nitobi.base.getUid();
if(typeof (this.Interface)=="undefined"){
this.API=nitobi.grid.apiDoc;
this.accessorGeneratorXslProc=nitobi.xml.createXslProcessor(nitobi.grid.accessorGeneratorXslProc.stylesheet);
this.Interface=this.API.selectSingleNode("interfaces/interface[@name='nitobi.grid.Grid']");
eval(nitobi.xml.transformToString(this.Interface,this.accessorGeneratorXslProc));
}
this.configureDefaults();
nitobi.html.addUnload(nitobi.lang.close(this,this.dispose));
this.subscribe("AttachToParent",this.initialize);
this.subscribe("DataReady",this.layout);
this.subscribe("AfterCellEdit",this.autoSave);
this.subscribe("AfterRowInsert",this.autoSave);
this.subscribe("AfterRowDelete",this.autoSave);
this.subscribe("AfterPaste",this.autoSave);
this.subscribe("AfterPaste",this.focus);
this.subscribeOnce("HtmlReady",this.adjustHorizontalScrollBars);
this.subscribe("AfterGridResize",this.adjustHorizontalScrollBars);
this.events=[];
this.keyEvents=[];
};
nitobi.grid.Grid.prototype.initialize=function(){
this.fire("Preinitialize");
this.initializeFromCss();
this.createChildren();
this.fire("AfterInitialize");
this.fire("CreationComplete");
};
nitobi.grid.Grid.prototype.initializeFromCss=function(){
try{
var _5e=nitobi.html.getClass("ntbrow");
if(_5e!=null&&_5e.height!=null&&_5e.height!=""){
this.setRowHeight(parseInt(_5e.height));
}
var _5f=nitobi.html.getClass("ntbheaderrow");
if(_5f!=null&&_5f.height!=null&&_5f.height!=""){
this.setHeaderHeight(parseInt(_5f.height));
}
}
catch(e){
}
};
nitobi.grid.Grid.prototype.connectRenderersToDataSet=function(_60){
this.TopLeftRenderer.xmlDataSource=_60;
this.TopCenterRenderer.xmlDataSource=_60;
this.MidLeftRenderer.xmlDataSource=_60;
this.MidCenterRenderer.xmlDataSource=_60;
};
nitobi.grid.Grid.prototype.connectToDataSet=function(_61,_62){
this.data=_61;
if(this.TopLeftRenderer){
this.connectRenderersToDataSet(_61);
}
this.connectToTable(_62);
};
nitobi.grid.Grid.prototype.connectToTable=function(_63){
if(typeof (_63)=="string"){
this.datatable=this.data.getTable(_63);
}else{
if(typeof (_63)=="object"){
this.datatable=_63;
}else{
if(this.data.getTable("_default")+""!="undefined"){
this.datatable=this.data.getTable("_default");
}else{
return false;
}
}
}
this.connected=true;
this.updateStructure();
this.datatable.subscribe("DataReady",nitobi.lang.close(this,this.handleHandlerError));
this.datatable.subscribe("DataReady",nitobi.lang.close(this,this.syncWithData));
this.datatable.subscribe("DataSorted",nitobi.lang.close(this,this.syncWithData));
this.datatable.subscribe("RowInserted",nitobi.lang.close(this,this.syncWithData));
this.datatable.subscribe("RowDeleted",nitobi.lang.close(this,this.syncWithData));
this.datatable.subscribe("RowCountChanged",nitobi.lang.close(this,this.setRowCount));
this.datatable.subscribe("PastEndOfData",nitobi.lang.close(this,this.adjustRowCount));
this.datatable.subscribe("RowCountKnown",nitobi.lang.close(this,this.finalizeRowCount));
this.datatable.subscribe("StructureChanged",nitobi.lang.close(this,this.updateStructure));
this.datatable.subscribe("ColumnsInitialized",nitobi.lang.close(this,this.updateStructure));
this.dataTableId=this.datatable.id;
this.datatable.setOnGenerateKey(this.getKeyGenerator());
this.fire("TableConnected",this.datatable);
return true;
};
nitobi.grid.Grid.prototype.ensureConnected=function(){
if(this.data==null){
this.data=new nitobi.data.DataSet();
this.data.initialize();
this.datatable=new nitobi.data.DataTable(this.getDataMode(),this.getPagingMode()==nitobi.grid.PAGINGMODE_LIVESCROLLING,{GridId:this.getID()},{GridId:this.getID()},this.isAutoKeyEnabled());
this.datatable.initialize("_default",this.getGetHandler(),this.getSaveHandler());
this.data.add(this.datatable);
this.connectToDataSet(this.data);
}
if(this.datatable==null){
this.datatable=this.data.getTable("_default");
if(this.datatable==null){
this.datatable=new nitobi.data.DataTable(this.getDataMode(),this.getPagingMode()==nitobi.grid.PAGINGMODE_LIVESCROLLING,{GridId:this.getID()},{GridId:this.getID()},this.isAutoKeyEnabled());
this.datatable.initialize("_default",this.getGetHandler(),this.getSaveHandler());
this.data.add(this.datatable);
}
this.connectToDataSet(this.data);
}
this.connected=true;
};
nitobi.grid.Grid.prototype.updateStructure=function(){
if(this.inferredColumns){
this.defineColumns(this.datatable);
}
this.mapColumns();
if(this.TopLeftRenderer){
this.defineColumnBindings();
this.defineColumnsFinalize();
}
};
nitobi.grid.Grid.prototype.mapColumns=function(){
this.fieldMap=this.datatable.fieldMap;
};
nitobi.grid.Grid.prototype.configureDefaults=function(){
this.initializeModel();
this.displayedFirstRow=0;
this.displayedRowCount=0;
this.localFilter=null;
this.columns=[];
this.fieldMap={};
this.frameRendered=false;
this.connected=false;
this.inferredColumns=true;
this.selectedRows=[];
this.minHeight=20;
this.minWidth=20;
this.setRowCount(0);
this.layoutValid=false;
this.oldVersion=false;
this.frameCssXslProc=nitobi.xml.createXslProcessor(nitobi.grid.frameCssXslProc.stylesheet);
this.rowXslGeneratorXslProc=nitobi.xml.createXslProcessor(nitobi.grid.rowGeneratorXslProc.stylesheet);
this.frameXslProc=nitobi.xml.createXslProcessor(nitobi.grid.frameXslProc.stylesheet);
this.CellHoverColor=nitobi.html.getClassStyle("ntbcellhover","backgroundColor")||"#C0C0FF";
this.RowHoverColor=nitobi.html.getClassStyle("ntbrowhover","backgroundColor")||"#FFFFC0";
this.CellActiveColor=nitobi.html.getClassStyle("ntb-grid-cellactive","backgroundColor")||"#F0C0FF";
this.RowActiveColor=nitobi.html.getClassStyle("ntb-grid-rowactive","backgroundColor")||"#FFC0FF";
this.CellSelectColor=nitobi.html.getClassStyle("ntb-grid-cellselect","backgroundColor")||"#F0C000";
this.RowSelectColor=nitobi.html.getClassStyle("ntb-grid-rowselect","backgroundColor")||"#FF00FF";
var _64=0;
var _65=0;
var _66=nitobi.html.getClass("ntbgrid");
if(_66!=null){
if(_66.borderTopWidth!=null){
_65+=nitobi.lang.parseNumber(_66.borderTopWidth);
}
if(_66.borderLeftWidth!=null){
_64+=nitobi.lang.parseNumber(_66.borderLeftWidth);
}
}
nitobi.form.EDITOR_OFFSETX=_64;
nitobi.form.EDITOR_OFFSETY=_65;
};
nitobi.grid.Grid.prototype.attachDomEvents=function(){
var _67=nitobi.html.getFirstChild(this.UiContainer);
this.events=[{"type":"contextmenu","handler":this.handleContextMenu},{"type":"mousedown","handler":this.handleMouseDown},{"type":"mouseup","handler":this.handleMouseUp},{"type":"mousemove","handler":this.handleMouseMove},{"type":"mouseout","handler":this.handleMouseOut},{"type":"mouseover","handler":this.handleMouseOver}];
if(nitobi.browser.IE){
this.keyNav=$("grid"+this.uid);
}else{
this.keyNav=$("ntb-grid-keynav"+this.uid);
}
this.keyEvents=[{"type":"keydown","handler":this.handleKey},{"type":"keyup","handler":this.handleKeyUp},{"type":"keypress","handler":this.handleKeyPress}];
nitobi.html.attachEvents(this.keyNav,this.keyEvents,this,false);
if(nitobi.browser.MOZ){
nitobi.html.attachEvent($("vscrollclip"+this.uid),"mousedown",this.focus,this);
nitobi.html.attachEvent($("hscrollclip"+this.uid),"mousedown",this.focus,this);
this.events.push({"type":"DOMMouseScroll","handler":this.handleMouseWheel});
}else{
if(nitobi.browser.IE){
this.events.push({"type":"mousewheel","handler":this.handleMouseWheel});
}
}
nitobi.html.attachEvents(_67,this.events,this,false);
_67.onselectstart=function(){
return false;
};
};
nitobi.grid.Grid.prototype.hoverCell=function(_68){
if(this.hovered){
this.hovered.style.backgroundColor=this.hoveredbg;
}
if(_68==this.activeCell||_68==null){
return;
}
this.hoveredbg=_68.style.backgroundColor;
this.hovered=_68;
_68.style.backgroundColor=this.CellHoverColor;
};
nitobi.grid.Grid.prototype.hoverView=function(row){
this.rowhoveredbg=row.style.backgroundColor;
this.rowhovered=row;
row.style.backgroundColor=this.RowHoverColor;
};
nitobi.grid.Grid.prototype.hoverRow=function(row){
if(!this.isRowHighlightEnabled()){
return;
}
if(this.leftrowhovered&&this.leftrowhovered!=this.leftActiveRow){
this.leftrowhovered.style.backgroundColor=this.leftrowhoveredbg;
}
if(this.midrowhovered&&this.midrowhovered!=this.midActiveRow){
this.midrowhovered.style.backgroundColor=this.midrowhoveredbg;
}
if(row==this.activeRow||row==null){
return;
}
var _6b=-1;
var _6c=nitobi.html.getFirstChild(row);
var _6d=nitobi.grid.Row.getRowNumber(row);
var _6e=nitobi.grid.Row.getRowElements(this,_6d);
if(_6e.left!=null&&_6e.left!=this.leftActiveRow){
this.leftrowhoveredbg=_6e.left.style.backgroundColor;
this.leftrowhovered=_6e.left;
_6e.left.style.backgroundColor=this.RowHoverColor;
}
if(_6e.mid!=null&&_6e.mid!=this.midActiveRow){
this.midrowhoveredbg=_6e.mid.style.backgroundColor;
this.midrowhovered=_6e.mid;
_6e.mid.style.backgroundColor=this.RowHoverColor;
}
};
nitobi.grid.Grid.prototype.clearHover=function(){
this.hoverCell();
this.hoverRow();
};
nitobi.grid.Grid.prototype.handleMouseOver=function(evt){
this.fire("MouseOver",evt);
};
nitobi.grid.Grid.prototype.handleMouseOut=function(evt){
this.clearHover();
this.fire("MouseOut",evt);
};
nitobi.grid.Grid.prototype.handleMouseDown=function(evt){
if(this.overResizeGrabby(evt)){
this.gridResizer.startResize(this,evt);
}
var _72=this.findActiveCell(evt.srcElement);
if(_72==null){
return;
}
var _73=_72.getAttribute("ebatype");
if(_73=="columnheader"){
this.handleHeaderMouseDown(_72,evt);
}
if(_73=="cell"){
this.handleCellMouseDown(_72,evt);
}
};
nitobi.grid.Grid.prototype.handleHeaderMouseDown=function(_74,evt){
var x=evt.clientX;
var _77=0;
if(nitobi.browser.MOZ){
_77=this.Scroller.scrollLeft;
}
var _78=_74.getBoundingClientRect().right-_77;
var _79=nitobi.grid.Cell.getColumnNumber(_74);
if((x<_78&&x>_78-10)){
this.columnResizer.startResize(this,this.getColumnObject(_79),_74,evt);
return false;
}else{
this.headerClicked(_79);
this.fire("HeaderDown",_79);
}
};
nitobi.grid.Grid.prototype.handleCellMouseDown=function(_7a,evt){
if(!evt.shiftKey){
var _7c=this.getSelectedColumnObject();
var _7d=new nitobi.grid.OnCellClickEventArgs(this,this.getSelectedCellObject());
if(!this.fire("BeforeCellClick",_7d)||(!!_7c&&!nitobi.event.evaluate(_7c.getOnBeforeCellClickEvent(),_7d))){
return;
}
this.setActiveCell(_7a,evt.ctrlKey);
this.selection.selecting=true;
var _7c=this.getSelectedColumnObject();
var _7d=new nitobi.grid.OnCellClickEventArgs(this,this.getSelectedCellObject());
this.fire("CellClick",_7d);
if(!!_7c){
nitobi.event.evaluate(_7c.getOnCellClickEvent(),_7d);
}
}
};
nitobi.grid.Grid.prototype.handleMouseUp=function(_7e){
var _7f=this.findActiveCell(_7e.srcElement);
if(!_7f){
return;
}
if(_7f.getAttribute("ebatype")=="columnheader"){
var _80=parseInt(_7f.getAttribute("xi"));
this.fire("HeaderUp",_80);
}
this.getSelection().handleGrabbyMouseUp(_7e);
};
nitobi.grid.Grid.prototype.handleMouseMove=function(evt){
var _82=this.findActiveCell(evt.srcElement);
if(_82!=null){
var _83=_82.getAttribute("ebatype");
var __x=evt.clientX;
var __y=evt.clientY;
if(_83=="columnheader"){
this.handleHeaderMouseMove(_82,evt.button,__x,__y);
}else{
if(_83=="cell"){
this.handleCellMouseMove(_82,evt.button,__x,__y);
}
}
}else{
var _86=nitobi.html.getFirstChild(this.UiContainer);
if(this.overResizeGrabby(evt)){
_86.style.cursor="nw-resize";
}else{
_86.style.cursor="auto";
}
}
this.fire("MouseMove",evt);
nitobi.html.cancelEvent(evt);
return false;
};
nitobi.grid.Grid.prototype.overResizeGrabby=function(evt){
if(this.isGridResizeEnabled()){
var _88=nitobi.html.getFirstChild(this.UiContainer);
var _89=nitobi.html.getEventCoords(evt);
var x=_89.x;
var y=_89.y;
var _8c=0;
var _8d=0;
if(nitobi.browser.MOZ){
var _8e=this.Scroller;
_8c=_8e.scrollLeft;
_8d=_8e.scrollTop;
}
var _8f=nitobi.html.getBox(_88);
if((x<(_8f.right-_8c)&&x>(_8f.right-_8c)-20)&&(y<(_8f.bottom)&&y>(_8f.bottom)-20)){
return true;
}
}
return false;
};
nitobi.grid.Grid.prototype.handleHeaderMouseMove=function(_90,_91,x,y){
var _94=_90.getBoundingClientRect(0,(nitobi.grid.Cell.getColumnNumber(_90)>this.getFrozenLeftColumnCount()?this.Scroller.scrollLeft:0));
if((x<_94.right&&x>_94.right-10)){
_90.style.cursor="w-resize";
}else{
(nitobi.browser.IE?_90.style.cursor="hand":_90.style.cursor="pointer");
}
};
nitobi.grid.Grid.prototype.handleHeaderMouseOver=function(_95){
var _96=_95.getAttribute("col");
var col=this.getColumnObject(_96);
var _98=col.getSortDirection();
nitobi.html.Css.addClass(_95,_95.className.split(" ")[0]+"hover");
};
nitobi.grid.Grid.prototype.handleHeaderMouseOut=function(_99){
var _9a=_99.getAttribute("col");
var col=this.getColumnObject(_9a);
var _9c=col.getSortDirection();
_99.className=_99.className.split(" ")[0];
};
nitobi.grid.Grid.prototype.handleCellMouseMove=function(_9d,_9e,x,y){
var sel=this.selection;
if(sel.selecting){
if(_9e==1||(_9e==0&&!nitobi.browser.IE)){
if(!sel.expanding){
sel.redraw(_9d);
this.ensureCellInView(_9d);
}else{
var _a2=sel.expandStartCoords;
var _a3=0;
if(x>_a2.right){
_a3=Math.abs(x-_a2.right);
}else{
if(x<_a2.left){
_a3=Math.abs(x-_a2.left);
}
}
var _a4=0;
if(y>_a2.bottom){
_a4=Math.abs(y-_a2.bottom);
}else{
if(y<_a2.top){
_a4=Math.abs(y-_a2.top);
}
}
if(_a4>_a3){
expandDir="vert";
}else{
expandDir="horiz";
}
sel.expand(_9d,expandDir);
}
}else{
this.selection.selecting=false;
}
}else{
this.hoverCell(_9d);
this.hoverRow(_9d.parentNode);
}
};
nitobi.grid.Grid.prototype.handleMouseWheel=function(_a5){
this.focus();
var _a6=0;
if(_a5.wheelDelta){
_a6=_a5.wheelDelta/120;
}else{
if(_a5.detail){
_a6=-_a5.detail/3;
}
}
this.scrollVerticalRelative(-20*_a6);
nitobi.html.cancelEvent(_a5);
};
nitobi.grid.Grid.prototype.setActiveCell=function(_a7,_a8){
if(!_a7){
return;
}
var _a9=this.getSelectedColumnObject();
var _aa=new nitobi.grid.OnCellBlurEventArgs(this,this.getSelectedCellObject());
if(!!_a9){
if(!this.fire("CellBlur",_aa)||!nitobi.event.evaluate(_a9.getOnCellBlurEvent(),_aa)){
return;
}
}
this.oldCell=this.activeCell;
this.activeCell=_a7;
var row=_a7.parentNode;
this.setActiveRow(row,_a8);
this.selection.collapse(this.activeCell);
this.ensureCellInView(this.activeCell);
this.focus();
_a9=this.getSelectedColumnObject();
var _ac=new nitobi.grid.OnCellFocusEventArgs(this,this.getSelectedCellObject());
this.fire("CellFocus",_ac);
if(!!_a9){
nitobi.event.evaluate(_a9.getOnCellFocusEvent(),_ac);
}
};
nitobi.grid.Grid.prototype.getRowNodes=function(row){
return nitobi.grid.Row.getRowElements(this,nitobi.grid.Row.getRowNumber(row));
};
nitobi.grid.Grid.prototype.setActiveRow=function(row,_af){
var Row=nitobi.grid.Row;
var _b1=Row.getRowNumber(row);
var _b2=-1;
if(this.oldCell!=null){
_b2=Row.getRowNumber(this.oldCell);
}
if(this.selectedRows[0]!=null){
_b2=Row.getRowNumber(this.selectedRows[0]);
}
if(!_af||!this.isMultiRowSelectEnabled()){
if(_b1!=_b2&&_b2!=-1){
var _b3=new nitobi.grid.OnRowBlurEventArgs(this,this.getRowObject(_b2));
if(!this.fire("RowBlur",_b3)||!nitobi.event.evaluate(this.getOnRowBlurEvent(),_b3)){
return;
}
}
this.clearActiveRows();
}
if(this.isRowSelectEnabled()){
var _b4=Row.getRowElements(this,_b1);
this.midActiveRow=_b4.mid;
this.leftActiveRow=_b4.left;
if(row.getAttribute("select")=="1"){
this.clearActiveRow(row);
}else{
this.selectedRows.push(row);
if(this.leftActiveRow!=null){
this.leftActiveRow.setAttribute("select","1");
this.applyRowStyle(this.leftActiveRow);
}
if(this.midActiveRow!=null){
this.midActiveRow.setAttribute("select","1");
this.applyRowStyle(this.midActiveRow);
}
}
}
if(_b1!=_b2){
var _b5=new nitobi.grid.OnRowFocusEventArgs(this,this.getRowObject(_b1));
this.fire("RowFocus",_b5);
nitobi.event.evaluate(this.getOnRowFocusEvent(),_b5);
}
};
nitobi.grid.Grid.prototype.getSelectedRows=function(){
return this.selectedRows;
};
nitobi.grid.Grid.prototype.clearActiveRows=function(){
for(var i=0;i<this.selectedRows.length;i++){
var row=this.selectedRows[i];
this.clearActiveRow(row);
}
this.selectedRows=[];
};
nitobi.grid.Grid.prototype.selectAllRows=function(){
this.clearActiveRows();
for(var i=0;i<this.getDisplayedRowCount();i++){
var _b9=this.getCellElement(i,0);
if(_b9!=null){
var row=_b9.parentNode;
this.setActiveRow(row,true);
}
}
return this.selectedRows;
};
nitobi.grid.Grid.prototype.clearActiveRow=function(row){
var _bc=nitobi.grid.Row.getRowNumber(row);
var _bd=nitobi.grid.Row.getRowElements(this,_bc);
if(_bd.left!=null){
_bd.left.removeAttribute("select");
this.removeRowStyle(_bd.left);
}
if(_bd.mid!=null){
_bd.mid.removeAttribute("select");
this.removeRowStyle(_bd.mid);
}
};
nitobi.grid.Grid.prototype.applyCellStyle=function(_be){
if(_be==null){
return;
}
_be.style.background=this.CellActiveColor;
};
nitobi.grid.Grid.prototype.removeCellStyle=function(_bf){
if(_bf==null){
return;
}
_bf.style.background="";
};
nitobi.grid.Grid.prototype.applyRowStyle=function(row){
if(row==null){
return;
}
row.style.background=this.RowActiveColor;
};
nitobi.grid.Grid.prototype.removeRowStyle=function(row){
if(row==null){
return;
}
row.style.background="";
};
nitobi.grid.Grid.prototype.findActiveCell=function(_c2){
var _c3=5;
_c2==null;
for(var i=0;i<_c3&&_c2.getAttribute;i++){
var t=_c2.getAttribute("ebatype");
if(t=="cell"||t=="columnheader"){
return _c2;
}
_c2=_c2.parentNode;
}
return null;
};
nitobi.grid.Grid.prototype.attachToParentDomElement=function(_c6){
this.UiContainer=_c6;
this.fire("AttachToParent");
};
nitobi.grid.Grid.prototype.getToolbars=function(){
return this.toolbars;
};
nitobi.grid.Grid.prototype.adjustHorizontalScrollBars=function(){
var _c7=this.calculateWidth();
if((_c7<=parseInt(this.getWidth()))){
var _c8=this.hScrollbar.element.parentNode;
_c8.style.display="none";
}else{
var _c8=this.hScrollbar.element.parentNode;
_c8.style.display="block";
this.resizeScroller();
var _c9=this.Scroller.width/this.calculateWidth();
this.hScrollbar.setRange(_c9);
}
};
nitobi.grid.Grid.prototype.createChildren=function(){
if(this.UiContainer!=null&&nitobi.html.getFirstChild(this.UiContainer)==null){
this.renderFrame();
}
this.generateFrameCss();
this.loadingScreen=new nitobi.grid.LoadingScreen(this);
this.subscribe("Preinitialize",nitobi.lang.close(this.loadingScreen,this.loadingScreen.show));
this.subscribe("HtmlReady",nitobi.lang.close(this.loadingScreen,this.loadingScreen.hide));
this.subscribe("AfterGridResize",nitobi.lang.close(this.loadingScreen,this.loadingScreen.resize));
this.loadingScreen.initialize();
this.loadingScreen.attachToElement($("ntb-grid-overlay"+this.uid));
this.loadingScreen.show();
this.columnResizer=new nitobi.grid.ColumnResizer(this);
var gr=new nitobi.grid.GridResizer(this);
gr.widthFixed=this.isWidthFixed();
gr.heightFixed=this.isHeightFixed();
gr.minWidth=this.getMinWidth();
gr.minHeight=Math.max(this.getMinHeight(),(this.getHeaderHeight()+this.getscrollbarHeight()));
this.gridResizer=gr;
var _cb=new nitobi.grid.Scroller3x3(this,this.getWidth(),this.getHeight(),this.gettop(),this.getright(),this.getbottom(),this.getleft(),this.getcontentWidth(),this.getcontentHeight(),this.getDisplayedRowCount(),this.getColumnCount(),this.getfreezetop(),this.getFrozenLeftColumnCount(),this.getfreezebottom(),this.getfreezeright());
_cb.setRowHeight(this.getRowHeight());
_cb.setHeaderHeight(this.getHeaderHeight());
this.Scroller=this.scroller=_cb;
this.Scroller.onHtmlReady.subscribe(this.handleHtmlReady,this);
this.subscribe("TableConnected",nitobi.lang.close(this.Scroller,this.Scroller.setDataTable));
this.Scroller.setDataTable(this.datatable);
this.initializeSelection();
this.createRenderers();
var sv=this.Scroller.view;
sv.midleft.rowRenderer=this.MidLeftRenderer;
sv.midcenter.rowRenderer=this.MidCenterRenderer;
this.mapToHtml();
var vs=$("vscroll"+this.uid);
var hs=$("hscroll"+this.uid);
this.vScrollbar=new nitobi.ui.VerticalScrollbar();
this.vScrollbar.attachToParent(this.element,vs);
this.vScrollbar.subscribe("ScrollByUser",nitobi.lang.close(this,this.scrollVertical));
this.subscribe("PercentHeightChanged",nitobi.lang.close(this.vScrollbar,this.vScrollbar.setRange));
this.subscribe("ScrollVertical",nitobi.lang.close(this.vScrollbar,this.vScrollbar.setScrollPercent));
this.setscrollbarWidth(this.vScrollbar.getWidth());
this.hScrollbar=new nitobi.ui.HorizontalScrollbar();
this.hScrollbar.attachToParent(this.element,hs);
this.hScrollbar.subscribe("ScrollByUser",nitobi.lang.close(this,this.scrollHorizontal));
this.subscribe("PercentWidthChanged",nitobi.lang.close(this.hScrollbar,this.hScrollbar.setRange));
this.subscribe("ScrollHorizontal",nitobi.lang.close(this.hScrollbar,this.hScrollbar.setScrollPercent));
this.setscrollbarHeight(this.hScrollbar.getHeight());
};
nitobi.grid.Grid.prototype.createToolbars=function(_cf){
this.toolbars=new nitobi.ui.Toolbars((this.isToolbarEnabled()?_cf:0));
var _d0=document.getElementById("toolbarContainer"+this.uid);
this.toolbars.setWidth(this.getWidth());
this.toolbars.setRowInsertEnabled(this.isRowInsertEnabled());
this.toolbars.setRowDeleteEnabled(this.isRowDeleteEnabled());
this.toolbars.attachToParent(_d0);
this.setToolbarContainerEmpty(false);
this.toolbars.subscribe("ToolbarsContainerNotEmpty",this.toolbarsContainerNotEmpty,this);
this.toolbars.subscribe("ToolbarsContainerEmpty",this.toolbarsContainerEmpty,this);
this.toolbars.subscribe("InsertRow",nitobi.lang.close(this,this.insertAfterCurrentRow));
this.toolbars.subscribe("DeleteRow",nitobi.lang.close(this,this.deleteCurrentRow));
this.toolbars.subscribe("Save",nitobi.lang.close(this,this.save));
this.toolbars.subscribe("Refresh",nitobi.lang.close(this,this.refresh));
this.subscribe("AfterGridResize",nitobi.lang.close(this,this.resizeToolbars));
};
nitobi.grid.Grid.prototype.resizeToolbars=function(){
this.toolbars.setWidth(this.getWidth());
this.toolbars.resize();
};
nitobi.grid.Grid.prototype.toolbarsContainerEmpty=function(){
this.setToolbarContainerEmpty(true);
this.generateCss();
this.resizeScroller();
};
nitobi.grid.Grid.prototype.toolbarsContainerNotEmpty=function(){
this.setToolbarContainerEmpty(false);
this.generateCss();
this.resizeScroller();
};
nitobi.grid.Grid.prototype.scrollVerticalRelative=function(_d1){
var _d2=this.getScrollSurface();
var st=_d2.scrollTop+_d1;
var MC=this.Scroller.view.midcenter;
percent=st/(MC.container.offsetHeight-MC.element.offsetHeight);
this.scrollVertical(percent);
};
nitobi.grid.Grid.prototype.scrollVertical=function(_d5){
this.clearHover();
var _d6=this.Scroller.scrollTopPercent;
this.Scroller.setScrollTopPercent(_d5);
this.fire("ScrollVertical",_d5);
if(_d5>0.99&&_d6<0.99){
this.fire("ScrollHitBottom",_d5);
}
if(_d5<0.01){
this.fire("ScrollHitTop",_d5);
}
};
nitobi.grid.Grid.prototype.scrollHorizontalRelative=function(_d7){
var _d8=this.getScrollSurface();
var sl=_d8.scrollLeft+_d7;
var MC=this.Scroller.view.midcenter;
percent=sl/(MC.container.offsetWidth-MC.element.offsetWidth);
this.scrollHorizontal(percent);
};
nitobi.grid.Grid.prototype.scrollHorizontal=function(_db){
this.clearHover();
this.Scroller.setScrollLeftPercent(_db);
this.fire("ScrollHorizontal",_db);
if(_db>0.99){
this.fire("ScrollHitRight",_db);
}
if(_db<0.01){
this.fire("ScrollHitLeft",_db);
}
};
nitobi.grid.Grid.prototype.getScrollSurface=function(){
if(this.Scroller!=null){
return this.Scroller.view.midcenter.element;
}
};
nitobi.grid.Grid.prototype.getActiveView=function(){
return this.Scroller.getViewportByCoords(nitobi.grid.Cell.getRowNumber(this.activeCell),nitobi.grid.Cell.getColumnNumber(this.activeCell));
};
nitobi.grid.Grid.prototype.ensureCellInView=function(_dc){
var SS=this.getScrollSurface();
var AC=_dc||this.activeCell;
if(AC==null){
return;
}
var sct=0;
var scl=0;
if(nitobi.browser.MOZ){
sct=SS.scrollTop;
scl=SS.scrollLeft;
}
var R1=AC.getClientRects()[0];
var R2=SS.getClientRects()[0];
var B=EBA_SELECTION_BUFFER||0;
var up=R1.top-R2.top-B-sct;
var _e5=R1.bottom-R2.bottom+B-sct;
var _e6=R1.left-R2.left-B-scl;
var _e7=R1.right-R2.right+B-scl;
if(up<0){
this.scrollVerticalRelative(up);
}
if(_e5>0){
this.scrollVerticalRelative(_e5);
}
if(nitobi.grid.Cell.getColumnNumber(AC)>this.getFrozenLeftColumnCount()-1){
if(_e6<0){
this.scrollHorizontalRelative(_e6);
}
if(_e7>0){
this.scrollHorizontalRelative(_e7);
}
}
this.fire("CellCoordsChanged",R1[0]);
};
nitobi.grid.Grid.prototype.invalidate=function(){
this.invalidateProperties();
this.invalidateSize();
this.invalidateDisplayList();
};
nitobi.grid.Grid.prototype.updateCellRanges=function(){
if(this.frameRendered){
var _e8=this.getRowCount();
this.Scroller.updateCellRanges(this.getColumnCount(),_e8,this.getFrozenLeftColumnCount(),this.getfreezetop(),this.getfreezeright(),this.getfreezebottom());
var h=this.calculateHeight();
var w=this.calculateWidth();
this.measure();
this.resizeScroller();
var _eb=this.Scroller.height/h;
var _ec=this.Scroller.width/w;
this.fire("PercentHeightChanged",_eb);
this.fire("PercentWidthChanged",_ec);
}
};
nitobi.grid.Grid.prototype.measure=function(){
this.measureViews();
this.sizeValid=true;
};
nitobi.grid.Grid.prototype.measureViews=function(){
this.measureRows();
this.measureColumns();
};
nitobi.grid.Grid.prototype.measureColumns=function(){
var fL=this.getFrozenLeftColumnCount();
var fR=this.getfreezeright();
var wL=0;
var wR=0;
var wT=0;
var _f2=this.getColumnDefinitions();
var _f3=_f2.length;
for(var i=0;i<_f3;i++){
if(_f2[i].getAttribute("Visible")=="1"||_f2[i].getAttribute("visible")=="1"){
var w=Number(_f2[i].getAttribute("Width"));
wT+=w;
if(i<fL){
wL+=w;
}
if(i>=_f3-fR){
wR+=w;
}
}
}
this.setcontentWidth(wT);
this.setleft(wL);
this.setright(wR);
};
nitobi.grid.Grid.prototype.measureRows=function(){
var _f6=this.isColumnIndicatorsEnabled()?this.getHeaderHeight():0;
this.setcontentHeight((this.calculateHeight())+_f6);
this.settop(this.calculateHeight(0,this.getfreezetop()-1)+_f6);
this.setbottom(0);
};
nitobi.grid.Grid.prototype.resizeScroller=function(){
var _f7=(this.getToolbars()!=null&&this.getToolbars().areAnyToolbarsDocked()?25:0);
var _f8=this.isColumnIndicatorsEnabled()?this.getHeaderHeight():0;
this.Scroller.resize(this.getWidth(),this.getHeight()-_f7-_f8,this.gettop(),this.getright(),this.getbottom(),this.getleft(),this.getcontentWidth(),this.getcontentHeight(),this.getDisplayedRowCount(),this.getColumnCount(),this.getfreezetop(),this.getFrozenLeftColumnCount(),this.getfreezebottom(),this.getfreezeright());
};
nitobi.grid.Grid.prototype.resize=function(_f9,_fa){
this.setWidth(_f9);
this.setHeight(_fa);
this.generateCss();
this.fire("AfterGridResize");
};
nitobi.grid.Grid.prototype.initializeModel=function(){
this.model=nitobi.xml.createXmlDoc(nitobi.xml.serialize(nitobi.grid.modelDoc));
var _fb=nitobi.html.getScrollBarWidth();
if(_fb){
this.setscrollbarWidth(_fb);
this.setscrollbarHeight(_fb);
}
var _fc=this.model.selectSingleNode("state/nitobi.grid.Columns");
if(_fc==null){
var _fc=this.model.createElement("nitobi.grid.Columns");
this.model.documentElement.appendChild(_fc);
}
var _fd=this.getColumnCount();
if(_fd>0){
this.defineColumns(_fd);
}else{
this.columnsDefined=false;
this.inferredColumns=true;
}
this.model.documentElement.setAttribute("ID",this.uid);
this.model.documentElement.setAttribute("uniqueID",this.uid);
};
nitobi.grid.Grid.prototype.clearDefaultData=function(_fe){
for(var i=0;i<_fe;i++){
var e=this.model.createElement("e");
e.setAttribute("xi",i+1);
xDec.appendChild(e);
}
};
nitobi.grid.Grid.prototype.createRenderers=function(){
var _101=this.uid;
var _102=this.getRowHeight();
this.TopLeftRenderer=new nitobi.grid.RowRenderer(this.data,null,_102,null,null,_101);
this.TopCenterRenderer=new nitobi.grid.RowRenderer(this.data,null,_102,null,null,_101);
this.MidLeftRenderer=new nitobi.grid.RowRenderer(this.data,null,_102,null,null,_101);
this.MidCenterRenderer=new nitobi.grid.RowRenderer(this.data,null,_102,null,null,_101);
};
nitobi.grid.Grid.prototype.bind=function(){
if(this.isBound()){
this.clear();
this.datatable.descriptor.reset();
}
};
nitobi.grid.Grid.prototype.dataBind=function(){
this.bind();
};
nitobi.grid.Grid.prototype.getDataSource=function(_103){
var _104=this.dataTableId||"_default";
if(_103){
_104=_103;
}
return this.data.getTable(_104);
};
nitobi.grid.Grid.prototype.getChangeLogXmlDoc=function(_105){
return this.getDataSource(_105).getChangeLogXmlDoc();
};
nitobi.grid.Grid.prototype.getComplete=function(_106){
if(null==_106.dataSource.xmlDoc){
ebaErrorReport("evtArgs.dataSource.xmlDoc is null or not defined. Likely the gethandler failed use fiddler to check the response","",EBA_ERROR);
this.fire("LoadingError");
return;
}
var _107=_106.dataSource.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+_106.dataSource.id+"']");
};
nitobi.grid.Grid.prototype.bindComplete=function(){
if(this.inferredColumns&&!this.columnsDefined){
this.defineColumns(this.datatable);
}
this.setRowCount(this.datatable.remoteRowCount);
this.setBound(true);
this.syncWithData();
};
nitobi.grid.Grid.prototype.syncWithData=function(_108){
if(this.isBound()){
this.Scroller.render(true);
this.fire("DataReady",{"source":this});
}
};
nitobi.grid.Grid.prototype.finalizeRowCount=function(rows){
this.rowCountKnown=true;
this.setRowCount(rows);
};
nitobi.grid.Grid.prototype.adjustRowCount=function(pct){
this.scrollVertical(pct);
};
nitobi.grid.Grid.prototype.setRowCount=function(rows){
this.xSET("RowCount",arguments);
if(this.getPagingMode()==nitobi.grid.PAGINGMODE_STANDARD){
if(this.getDataMode()==nitobi.data.DATAMODE_LOCAL){
this.setDisplayedRowCount(this.getRowsPerPage());
}
}else{
this.setDisplayedRowCount(rows);
}
this.rowCount=rows;
this.updateCellRanges();
};
nitobi.grid.Grid.prototype.getRowCount=function(){
return this.rowCount;
};
nitobi.grid.Grid.prototype.layout=function(_10c){
if(this.prevHeight!=this.getHeight()){
this.prevHeight=this.getHeight();
this.prevWidth=this.getWidth();
this.layoutValid=false;
}
if(!this.layoutValid){
this.layoutFrame();
this.generateFrameCss();
this.layoutValid=true;
}
};
nitobi.grid.Grid.prototype.layoutFrame=function(_10d){
this.minHeight=20;
this.minWidth=20;
var _10e=false;
var _10f=false;
var tbH=this.getToolbarHeight();
var rowH=this.getRowHeight();
var colW=20;
var sbH=this.getscrollbarHeight();
var sbW=this.getscrollbarWidth();
var hdrH=this.getHeaderHeight();
tbH=this.getToolbars().areAnyToolbarsDocked()?tbH:0;
hdrH=this.isColumnIndicatorsEnabled?hdrH:0;
var minH=Math.max(this.minHeight,tbH+rowH+sbH+hdrH);
var maxH=this.Height;
var minW=Math.max(this.minWidth,colW+sbW);
var maxW=this.Width;
if(_10e){
var _11a=this.Scroller.minSurfaceWidth;
var _11b=this.Scroller.maxSurfaceWidth;
}else{
var _11a=this.Scroller.SurfaceWidth;
var _11b=_11a;
}
if(_10f){
var _11c=this.Scroller.minSurfaceHeight;
var _11d=this.Scroller.maxSurfaceHeight;
}else{
var _11c=this.Scroller.SurfaceHeight;
var _11d=_11c;
}
var _11e=_11c+(tbH)+(hdrH);
var _11f=_11a;
var _120=(_11e>maxH);
var _121=(_11f>maxW);
var _120=(_121&&((_11e+20)>maxH))||_120;
var _121=(_120&&((_11f+20)>maxW))||_121;
sbH=_121?sbH:0;
sbV=_120?sbV:0;
var vpH=_11e-hdrH-tbH-sbH;
var vpW=_11f-sbW;
this.resize();
};
nitobi.grid.Grid.prototype.defineColumns=function(_124){
this.fire("BeforeColumnsDefined");
this.resetColumns();
var _125=null;
var _126=nitobi.lang.typeOf(_124);
this.inferredColumns=false;
if(_126=="string"){
_125=this.defineColumnsFromString(_124);
}
if(_126==nitobi.lang.type.XMLNODE||_126==nitobi.lang.type.XMLDOC){
_125=this.defineColumnsFromXml(_124);
}
if(_126==nitobi.lang.type.ARRAY){
_125=this.defineColumnsFromArray(_124);
}
if(_126=="object"){
this.inferredColumns=true;
_125=this.defineColumnsFromData(_124);
}
if(_126=="number"){
_125=this.defineColumnsCollection(_124);
}
this.fire("AfterColumnsDefined");
this.defineColumnsFinalize();
return _125;
};
nitobi.grid.Grid.prototype.defineColumnsFromXml=function(_127){
if(_127==null||_127.childNodes.length==0){
return this.defineColumnsCollection(0);
}
if(_127.childNodes[0].nodeName==nitobi.xml.nsPrefix+"columndefinition"){
var _128=nitobi.xml.createXslDoc(nitobi.grid.declarationConverterXslProc.stylesheet);
_127=nitobi.xml.transformToXml(_127,_128);
}
var wL=0,wT=0,wR=0;
var _12c=this.model.selectSingleNode("/state/Defaults/nitobi.grid.Column");
var _12d=this.getColumnDefinitions().length;
var cols=_127.childNodes.length;
var xDec=this.model.selectSingleNode("state/nitobi.grid.Columns");
var _130=_127.childNodes;
var fL=this.getFrozenLeftColumnCount();
var fR=this.getfreezeright();
if(_12d==0){
var cols=_130.length;
for(var i=0;i<cols;i++){
var e=_12c.cloneNode(true);
this.setModelDefaults(e,_130[i],"interfaces/interface[@name='nitobi.grid.Column']/properties/property");
this.setModelDefaults(e,_130[i],"interfaces/interface[@name='nitobi.grid.Column']/events/event");
var _135="";
var _136=_130[i].nodeName;
if(_136.indexOf("numbercolumn")!=-1){
_135="EBANumberColumn";
}else{
if(_136.indexOf("datecolumn")!=-1){
_135="EBADateColumn";
}else{
_135="EBATextColumn";
}
}
e.setAttribute("DataType",_135.replace("EBA","").replace("Column","").toLowerCase());
this.setModelDefaults(e,_130[i],"interfaces/interface[@name='"+_135+"']/properties/property");
this.setModelDefaults(e,_130[i],"interfaces/interface[@name='"+_135+"']/events/event");
this.defineColumnEditor(e,_130[i]);
this.defineColumnDatasource(e);
this.defineColumnBinding(e);
xDec.appendChild(e);
var _137=e.getAttribute("GetHandler");
if(_137){
var _138=e.getAttribute("DatasourceId");
if(!_138||_138==""){
_138="columnDatasource_"+i+"_"+this.uid;
e.setAttribute("DatasourceId",_138);
}
var dt=new nitobi.data.DataTable("local",this.getPagingMode()==nitobi.grid.PAGINGMODE_LIVESCROLLING,{GridId:this.getID()},{GridId:this.getID()},this.isAutoKeyEnabled());
dt.initialize(_138,_137,null);
dt.async=false;
this.data.add(dt);
var _13a=[];
_13a[0]=e;
var _13b=e.getAttribute("editor");
var _13c=null;
var _13d=null;
if(e.getAttribute("editor")=="LOOKUP"){
_13c=0;
_13d=1;
dt.async=true;
}
dt.get(_13c,_13d,this,nitobi.lang.close(this,this.editorDataReady,[e]),function(){
});
}
}
this.measureColumns();
this.setcontentHeight((this.calculateHeight())+Number(this.getHeaderHeight()));
this.setColumnCount(cols);
}
var _13e;
_13e=_127.selectSingleNode("/"+nitobi.xml.nsPrefix+"grid/"+nitobi.xml.nsPrefix+"datasources");
if(_13e){
this.Declaration.datasources=nitobi.xml.createXmlDoc(_13e.xml);
}
return xDec;
};
nitobi.grid.Grid.prototype.defineColumnsFinalize=function(){
this.setColumnsDefined(true);
if(this.connected){
if(this.frameRendered){
this.makeXSL();
this.generateColumnCss();
this.renderColumns();
}
}
};
nitobi.grid.Grid.prototype.defineColumnDatasource=function(_13f){
var val=_13f.getAttribute("Datasource");
if(val!=null){
var ds=new Array();
try{
ds=eval(val);
}
catch(e){
var _142=val.split(",");
if(_142.length>0){
for(var i=0;i<_142.length;i++){
var item=_142[i];
ds[i]={text:item.split(":")[0],display:item.split(":")[1]};
}
}
return;
}
if(typeof (ds)=="object"&&ds.length>0){
var _145=new nitobi.data.DataTable("unbound",this.getPagingMode()==nitobi.grid.PAGINGMODE_LIVESCROLLING,{GridId:this.getID()},{GridId:this.getID()},this.isAutoKeyEnabled());
var _146="columnDatasource"+new Date().getTime();
_145.initialize(_146);
_13f.setAttribute("DatasourceId",_146);
var _147="";
for(var item in ds[0]){
_147+=item+"|";
}
_147=_147.substring(0,_147.length-1);
_145.initializeColumns(_147);
for(var i=0;i<ds.length;i++){
_145.createRecord(null,i);
for(var item in ds[i]){
_145.updateRecord(i,item,ds[i][item]);
}
}
this.data.add(_145);
this.editorDataReady(_13f);
}
}
};
nitobi.grid.Grid.prototype.defineColumnEditor=function(_148,_149){
var len=_149.childNodes.length;
if(len>0){
var _14b=_149.selectSingleNode("ntb:texteditor|ntb:numbereditor|ntb:textareaeditor|ntb:imageeditor|ntb:linkeditor|ntb:dateeditor|ntb:lookupeditor|ntb:listboxeditor|ntb:checkboxeditor|ntb:passwordeditor");
if(_14b!=null){
var _14c="EBATextEditor";
var _14d=_14b.nodeName;
if(_14d.indexOf("numbereditor")!=-1){
_14c="EBANumberEditor";
}else{
if(_14d.indexOf("textareaeditor")!=-1){
_14c="EBATextareaEditor";
}else{
if(_14d.indexOf("imageeditor")!=-1){
_14c="EBAImageEditor";
}else{
if(_14d.indexOf("linkeditor")!=-1){
_14c="EBALinkEditor";
}else{
if(_14d.indexOf("dateeditor")!=-1){
_14c="EBADateEditor";
}else{
if(_14d.indexOf("lookupeditor")!=-1){
_14c="EBALookupEditor";
}else{
if(_14d.indexOf("listboxeditor")!=-1){
_14c="EBAListboxEditor";
}else{
if(_14d.indexOf("passwordeditor")!=-1){
_14c="EBAPasswordEditor";
}else{
if(_14d.indexOf("checkboxeditor")!=-1){
_14c="EBACheckboxEditor";
}
}
}
}
}
}
}
}
}
this.setModelDefaults(_148,_14b,"interfaces/interface[@name='"+_14c+"']/properties/property");
this.setModelDefaults(_148,_14b,"interfaces/interface[@name='"+_14c+"']/events/event");
_148.setAttribute("type",_14d.substring(4,_14d.indexOf("editor")).toUpperCase());
_148.setAttribute("editor",_14d.substring(4,_14d.indexOf("editor")).toUpperCase());
}
}else{
var _14e=_149;
var _14c="";
var _14d=_14e.nodeName;
if(_14d.indexOf("numbercolumn")){
_14c="EBANumberEditor";
}else{
if(_14e.nodeName.indexOf("dateeditor")){
_14c="EBADateEditor";
}
}
this.setModelDefaults(_148,_14e,"interfaces/interface[@name='"+_14c+"']/properties/property");
this.setModelDefaults(_148,_14e,"interfaces/interface[@name='"+_14c+"']/events/event");
_148.setAttribute("type",_14d.substring(4,_14d.indexOf("column")).toUpperCase());
}
};
nitobi.grid.Grid.prototype.defineColumnsFromData=function(_14f){
if(_14f==null){
_14f=this.datatable;
}
var _150=_14f.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasourcestructure");
if(_150==null){
return this.defineColumnsCollection(0);
}
var _151=_150.getAttribute("FieldNames");
if(_151.length==0){
return this.defineColumnsCollection(0);
}
var _152=_150.getAttribute("defaults");
var _153=this.defineColumnsFromString(_151);
for(var i=0;i<_153.length;i++){
if(_152&&i<_152.length){
_153[i].setAttribute("initial",_152[i]||"");
}
_153[i].setAttribute("width",100);
}
this.inferredColumns=true;
return _153;
};
nitobi.grid.Grid.prototype.defineColumnsFromString=function(_155){
return this.defineColumnsFromArray(_155.split("|"));
};
nitobi.grid.Grid.prototype.defineColumnsFromArray=function(_156){
var cols=_156.length;
var _158=this.defineColumnsCollection(cols);
for(var i=0;i<cols;i++){
var col=_158[i];
if(typeof (_156[i])=="string"){
col.setAttribute("ColumnName",_156[i]);
col.setAttribute("xdatafld_orig",_156[i]);
col.setAttribute("DataField_orig",_156[i]);
col.setAttribute("Label",_156[i]);
if(typeof (this.fieldMap[_156[i]])!="undefined"){
col.setAttribute("xdatafld",this.fieldMap[_156[i]]);
col.setAttribute("DataField",this.fieldMap[_156[i]]);
}else{
col.setAttribute("xdatafld","unbound");
col.setAttribute("DataField","unbound");
}
}else{
if(_156[i].name!="_xk"){
col.setAttribute("ColumnName",col.name);
col.setAttribute("xdatafld_orig",col.name);
col.setAttribute("DataField_orig",col.name);
col.setAttribute("xdatafld",this.fieldMap[_156[i].name]);
col.setAttribute("DataField",this.fieldMap[_156[i].name]);
col.setAttribute("Width",col.width);
col.setAttribute("Label",col.label);
col.setAttribute("Initial",col.initial);
col.setAttribute("Mask",col.mask);
}
}
}
this.setColumnCount(cols);
return _158;
};
nitobi.grid.Grid.prototype.defineColumnBindings=function(){
var cols=this.getColumnDefinitions();
for(var i=0;i<cols.length;i++){
var e=cols[i];
this.defineColumnBinding(e);
e.setAttribute("xi",i);
}
};
nitobi.grid.Grid.prototype.defineColumnBinding=function(_15e){
if(this.fieldMap==null){
return;
}
var _15f=_15e.getAttribute("xdatafld");
var _160=_15e.getAttribute("xdatafld_orig");
if(_160==null||_160==""){
_15e.setAttribute("xdatafld_orig",_15f);
_15e.setAttribute("DataField_orig",_15f);
}
_15f=_15e.getAttribute("xdatafld_orig");
_15e.setAttribute("ColumnName",_15f);
if(typeof (this.fieldMap[_15f])!="undefined"){
_15e.setAttribute("xdatafld",this.fieldMap[_15f]);
_15e.setAttribute("DataField",this.fieldMap[_15f]);
}
this.formatBinding(_15e,"CssStyle");
this.formatBinding(_15e,"ClassName");
this.formatBinding(_15e,"Value");
};
nitobi.grid.Grid.prototype.formatBinding=function(_161,_162){
var _163=_161.getAttribute(_162);
var _164=_161.getAttribute(_162+"_orig");
if(_163==null||_163==""){
return;
}
if(_164==null||_164==""){
_161.setAttribute(_162+"_orig",_163);
}
_163=_161.getAttribute(_162+"_orig");
var _165=_163;
var re=new RegExp("\\{.[^}]*}","gi");
var _167=_163.match(re);
if(_167==null){
return;
}
for(var i=0;i<_167.length;i++){
var _169=_167[i];
var _16a=_169;
var _16b=new RegExp("\\$.*?[^0-9a-zA-Z_]","gi");
var _16c=_16a.match(_16b);
for(var j=0;j<_16c.length;j++){
var _16e=_16c[j].substring(0,_16c[j].length-1);
var _16f=_16e.substring(1);
var _170=this.fieldMap[_16f];
_16a=_16a.replace(_16e,"<xsl:value-of select=\""+_170+"\"/>"||"");
}
_16a=_16a.substring(1,_16a.length-1);
_163=_163.replace(_169,_16a).replace(/\{\}/g,"");
}
_161.setAttribute(_162,_163);
};
nitobi.grid.Grid.prototype.defineColumnsCollection=function(cols){
var xDec=this.model.selectSingleNode("state/nitobi.grid.Columns");
var _173=xDec.childNodes;
var _174=this.model.selectSingleNode("/state/Defaults/nitobi.grid.Column");
for(var i=0;i<cols;i++){
var e=_174.cloneNode(true);
xDec.appendChild(e);
e.setAttribute("xi",i);
e.setAttribute("title",(i>25?String.fromCharCode(Math.floor(i/26)+65):"")+(String.fromCharCode(i%26+65)));
}
this.setColumnCount(cols);
var _173=xDec.selectNodes("*");
return _173;
};
nitobi.grid.Grid.prototype.resetColumns=function(){
this.fire("BeforeClearColumns");
this.inferredColumns=true;
this.columnsDefined=false;
var _177=this.model.selectSingleNode("state/nitobi.grid.Columns");
var xDec=this.model.createElement("nitobi.grid.Columns");
if(_177==null){
this.model.documentElement.appendChild(xDec);
}else{
this.model.documentElement.replaceChild(xDec,_177);
}
this.setColumnCount(0);
this.fire("AfterClearColumns");
};
nitobi.grid.Grid.prototype.renderColumns=function(){
if(this.getColumnDefinitions().length>0){
this.clearHeader();
this.renderHeader();
}
};
nitobi.grid.Grid.prototype.initializeSelection=function(){
var sel=new nitobi.grid.Selection(this);
sel.setRowHeight(this.getRowHeight());
sel.onExpandSelectionStop.subscribe(this.expandSelectionStop,this);
this.selection=this.Selection=sel;
};
nitobi.grid.Grid.prototype.expandSelectionStop=function(evt){
var sel=this.selection;
var _17c=sel.getCoords();
var _17d=_17c.top.y;
var _17e=_17c.bottom.y;
var _17f=_17c.top.x;
var _180=_17c.bottom.x;
var _181=this.getTableForSelection({top:{x:sel.expandStartLeftColumn,y:sel.expandStartTopRow},bottom:{x:sel.expandStartRightColumn,y:sel.expandStartBottomRow}});
var data="",_183=this.getClipboard();
if(this.selection.expandingVertical){
if(sel.expandStartBottomRow>_17e&&_17d>=sel.expandStartTopRow){
for(var i=sel.expandStartLeftColumn;i<=sel.expandStartRightColumn;i++){
for(var j=_17e+1;j<sel.expandStartBottomRow+1;j++){
this.getCellObject(j,i).setValue("");
}
}
}else{
var _186=(sel.expandStartBottomRow<_17e);
var _187=(sel.expandStartTopRow>_17d);
var _188=(_186||_187);
if(_181.lastIndexOf("\n")==_181.length-1){
_181=_181.substring(0,_181.length-1);
}
var rep=(Math.floor((sel.getHeight()-!_188)/sel.expandStartHeight));
for(var i=0;i<rep;i++){
data+=_181+(nitobi.browser.MOZ?"\n":"");
}
_18a=_181.split("\n");
var mod=(sel.getHeight()-!_188)%sel.expandStartHeight;
var val="";
if(_188){
if(_186){
_18a.splice(mod,_18a.length-mod);
val=data+_18a.join("\n")+(_18a.length>0?"\n":"");
}else{
_18a.splice(0,_18a.length-mod);
val=_18a.join("\n")+(_18a.length>0?"\n":"")+data;
}
}else{
_18a=_18a.splice(0,mod);
val=_18a.join("\n")+(_18a.length>0?"\n":"")+data;
}
_183.value=val;
this.pasteDataReady(_183);
}
}else{
if(sel.expandStartRightColumn>_180&&_17f>=sel.expandStartLeftColumn){
for(var i=_17f+1;i<=sel.expandStartRightColumn+1;i++){
for(var j=sel.expandStartTopRow;j<sel.expandStartBottomRow;j++){
this.getCellObject(j,i).setValue("");
}
}
}else{
var _18d=sel.expandStartRightColumn<_180;
var _18e=sel.expandStartLeftColumn>_17f;
var _188=(_18d||_18e);
var mod=(sel.getWidth()-!_188)%sel.expandStartWidth;
var _18f=(nitobi.browser.MOZ?"\n":"\r\n");
if(_181.lastIndexOf(_18f)==_181.length-_18f.length){
_181=_181.substring(0,_181.length-_18f.length);
}
var _18a=_181.replace(/\r/g,"").split("\n");
var data=new Array(_18a.length);
var rep=(Math.floor((sel.getWidth()-!_188)/sel.expandStartWidth));
for(var i=0;i<_18a.length;i++){
var _190=_18a[i].split("\t");
for(var j=0;j<rep;j++){
data[i]=(data[i]==null?[]:data[i]).concat(_190);
}
if(mod!=0){
if(_18d){
data[i]=data[i].concat(_190.splice(0,mod));
}else{
data[i]=_190.splice(mod,_190.length-mod).concat(data[i]);
}
}
data[i]=data[i].join("\t");
}
_183.value=data.join("\n")+"\n";
this.pasteDataReady(_183);
}
}
};
nitobi.grid.Grid.prototype.calculateHeight=function(_191,end){
_191=(_191!=null)?_191:0;
var _193=this.getDisplayedRowCount();
end=(end!=null)?end:_193-1;
return (end-_191+1)*(this.getRowHeight()||23);
};
nitobi.grid.Grid.prototype.calculateWidth=function(_194,end){
var _196=this.getColumnDefinitions();
var cols=_196.length;
_194=_194||0;
end=(end!=null)?Math.min(end,cols):cols;
var wT=0;
for(var i=_194;i<end;i++){
if(_196[i].getAttribute("Visible")=="1"||_196[i].getAttribute("visible")=="1"){
wT+=Number(_196[i].getAttribute("Width"));
}
}
return (wT);
};
nitobi.grid.Grid.prototype.editorDataReady=function(_19a){
var _19b=_19a.getAttribute("DisplayFields").split("|");
var _19c=_19a.getAttribute("ValueField");
var _19d=this.data.getTable(_19a.getAttribute("DatasourceId"));
var _19e=_19a.getAttribute("Initial");
if(_19e==""){
var _19f=_19a.getAttribute("type").toLowerCase();
switch(_19f){
case "checkbox":
case "listbox":
var _1a0=_19d.fieldMap[_19c].substring(1);
var data=_19d.getDataXmlDoc();
if(data!=null){
var val=data.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@"+_1a0+"='"+_19e+"']");
if(val==null){
var _1a3=data.selectSingleNode("//"+nitobi.xml.nsPrefix+"e");
if(_1a3!=null){
_19e=_1a3.getAttribute(_1a0);
}
}
}
break;
}
_19a.setAttribute("Initial",_19e);
}
if((_19b.length==1&&_19b[0]=="")&&(_19c==null||_19c=="")){
for(var item in _19d.fieldMap){
_19b[0]=_19d.fieldMap[item].substring(1);
break;
}
}else{
for(var i=0;i<_19b.length;i++){
_19b[i]=_19d.fieldMap[_19b[i]].substring(1);
}
}
var _1a6=_19b.join("|");
if(_19c==null||_19c==""){
_19c=_19b[0];
}else{
_19c=_19d.fieldMap[_19c].substring(1);
}
_19a.setAttribute("DisplayFields",_1a6);
_19a.setAttribute("ValueField",_19c);
};
nitobi.grid.Grid.prototype.headerClicked=function(_1a7){
var _1a8=this.getColumnObject(_1a7);
var _1a9=new nitobi.grid.OnHeaderClickEventArgs(this,_1a8);
if(!this.fire("HeaderClick",_1a9)||!nitobi.event.evaluate(_1a8.getOnHeaderClickEvent(),_1a9)){
return;
}
this.sort(_1a7);
};
nitobi.grid.Grid.prototype.addFilter=function(){
this.dataTable.addFilter(arguments);
};
nitobi.grid.Grid.prototype.clearFilter=function(){
this.dataTable.clearFilter();
};
nitobi.grid.Grid.prototype.setSortStyle=function(_1aa,_1ab,_1ac){
var _1ad=this.getColumnObject(_1aa);
if(_1ac){
this.sortColumn=null;
this.sortColumnCell=null;
this.Scroller.setSort(_1aa,"");
this.setColumnHeaderSortOrder(_1aa,"");
}else{
_1ad.setSortDirection(_1ab);
this.setColumnHeaderSortOrder(_1aa,_1ab);
this.sortColumn=_1ad;
this.sortColumnCell=_1ad.getHeaderElement();
this.Scroller.setSort(_1aa,_1ab);
}
};
nitobi.grid.Grid.prototype.sort=function(_1ae,_1af){
var _1b0=this.getColumnObject(_1ae);
if(_1b0==null||!_1b0.isSortEnabled()||(!this.isSortEnabled())){
return;
}
var _1b1=new nitobi.grid.OnBeforeSortEventArgs(this,_1b0);
if(!this.fire("BeforeSort",_1b1)||!nitobi.event.evaluate(_1b0.getOnBeforeSortEvent(),_1b1)){
return;
}
this.startMouseWait();
if(_1af==null||typeof (_1af)=="undefined"){
_1af=(_1b0.getSortDirection()=="Asc")?"Desc":"Asc";
}
this.setSortStyle(_1ae,_1af);
var _1b2=_1b0.getColumnName();
var _1b3=_1b0.getDataType();
var _1b4=this.getSortMode()=="local"||(this.getDataMode()=="local"&&this.getSortMode()!="remote");
this.datatable.sort(_1b2,_1af,_1b3,_1b4);
if(!_1b4){
this.datatable.flush();
}
this.clearSurfaces();
this.render();
this.scrollVertical(0);
if(!_1b4){
this.loadDataPage(0);
}
this.stopMouseWait();
this.subscribeOnce("HtmlReady",this.handleAfterSort,this,[_1b0]);
};
nitobi.grid.Grid.prototype.handleAfterSort=function(_1b5){
var _1b6=new nitobi.grid.OnAfterSortEventArgs(this,_1b5);
this.fire("AfterSort",_1b6);
nitobi.event.evaluate(_1b5.getOnAfterSortEvent(),_1b6);
};
nitobi.grid.Grid.prototype.handleDblClick=function(evt){
var cell=new nitobi.grid.Cell(this,this.activeCell);
var _1b9=new nitobi.grid.OnCellDblClickEventArgs(this,cell);
return this.fire("CellDblClick",_1b9)&&nitobi.event.evaluate(cell.getColumnObject().getOnCellDblClickEvent(),_1b9);
};
nitobi.grid.Grid.prototype.clearData=function(){
if(this.getDataMode()!="local"){
this.datatable.flush();
}
};
nitobi.grid.Grid.prototype.clearColumnHeaderSortOrder=function(){
if(this.sortColumn){
var _1ba=this.sortColumn;
var _1bb=_1ba.getHeaderElement();
var css=_1bb.className;
css=css.replace(/ascending/gi,"").replace(/descending/gi,"");
_1bb.className=css;
this.sortColumn=null;
}
};
nitobi.grid.Grid.prototype.setColumnHeaderSortOrder=function(_1bd,_1be){
this.clearColumnHeaderSortOrder();
var _1bf=this.getColumnObject(_1bd);
var _1c0=_1bf.getHeaderElement();
var CSS=nitobi.html.Css;
var css=_1c0.className;
if(_1be==""){
_1c0.className="ntbcolumnindicatorborder";
_1be="Desc";
}else{
var _1c3=(_1be=="Desc"?"descending":"ascending");
nitobi.html.Css.swapClass(_1c0,"ntbcolumnindicatorborder","ntbcolumnindicatorborder"+_1c3);
nitobi.html.Css.removeClass(_1c0,"ntbcolumnindicatorborderhover");
}
_1bf.setSortDirection(_1be);
this.sortColumn=_1bf;
this.sortColumnCell=_1c0;
};
nitobi.grid.Grid.prototype.startMouseWait=function(){
this.mouseWait=document.getElementById("ntbmousewait_"+this.uid);
if(this.mouseWait==null){
this.mouseWait=document.createElement("div");
this.mouseWait.id="ntbmousewait_"+this.uid;
this.mouseWait.className="ntbmousewait";
document.body.appendChild(this.mouseWait);
}
this.mouseWait.style.display="block";
if(nitobi.browser.IE){
nitobi.drawing.align(this.mouseWait,this.element,nitobi.drawing.align.SAMEHEIGHT|nitobi.drawing.align.SAMEWIDTH|nitobi.drawing.align.ALIGNTOP|nitobi.drawing.align.ALIGNLEFT);
}else{
this.mouseWait.style.height=(this.getHeight()+20)+"px";
this.mouseWait.style.width=this.getWidth()+"px";
this.mouseWait.style.top=(this.UiContainer.getBoundingClientRect().top-2)+"px";
this.mouseWait.style.left=this.UiContainer.getBoundingClientRect().left+"px";
}
};
nitobi.grid.Grid.prototype.stopMouseWait=function(){
if(this.mouseWait!=null&&typeof (this.mouseWait)!="undefined"){
this.mouseWait.style.top="-1000px";
this.mouseWait.style.left="-1000px";
this.mouseWait.style.display="none";
}
};
nitobi.grid.Grid.prototype.initializeState=function(){
};
nitobi.grid.Grid.prototype.mapToHtml=function(_1c4){
if(_1c4==null){
_1c4=this.UiContainer;
}
this.Scroller.mapToHtml(_1c4);
this.element=document.getElementById("grid"+this.uid);
this.element.jsObject=this;
};
nitobi.grid.Grid.prototype.generateCss=function(){
this.generateFrameCss();
};
nitobi.grid.Grid.prototype.generateColumnCss=function(){
this.generateCss();
};
nitobi.grid.Grid.prototype.generateFrameCss=function(){
if(this.stylesheet==null){
this.stylesheet=document.createStyleSheet();
}
if(nitobi.browser.IE&&document.compatMode=="CSS1Compat"){
this.frameCssXslProc.addParameter("IE","true","");
}
var _1c5=nitobi.xml.transformToString(this.model,this.frameCssXslProc);
var vp=this.getScrollSurface();
var _1c7=0;
var _1c8=0;
if(vp!=null){
_1c7=vp.scrollTop;
_1c8=vp.scrollLeft;
}
if(this.oldFrameCss!=_1c5){
this.oldFrameCss=_1c5;
this.stylesheet.cssText=_1c5;
if(vp!=null){
if(nitobi.browser.MOZ){
this.scrollVerticalRelative(_1c7);
this.scrollHorizontalRelative(_1c8);
}
vp.style.top="0px";
vp.style.left="0px";
}
}
if(nitobi.grid.RowHoverColor==null){
var _1c9=nitobi.html.getClass("ntbrowhover");
if(_1c9!=null){
var _1ca=_1c9.backgroundColor.toString();
if(_1ca.indexOf("rgb")>-1){
_1ca=eval("nitobi.drawing."+_1ca);
}
nitobi.grid.RowHoverColor=_1ca;
}
}
if(nitobi.grid.CellHoverColor==null){
var _1c9=nitobi.html.getClass("ntbcellhover");
if(_1c9!=null){
var _1ca=_1c9.backgroundColor.toString();
if(_1ca.indexOf("rgb")>-1){
_1ca=eval("nitobi.drawing."+_1ca);
}
nitobi.grid.CellHoverColor=_1ca;
}
}
};
nitobi.grid.Grid.prototype.clearSurfaces=function(){
this.selection.clearBoxes();
this.Scroller.clearSurfaces();
};
nitobi.grid.Grid.prototype.clearHeader=function(){
this.Scroller.clearSurfaces(false,true);
};
nitobi.grid.Grid.prototype.renderFrame=function(){
if(nitobi.browser.IE){
this.frameXslProc.addParameter("IE","true","");
}
this.UiContainer.innerHTML=nitobi.xml.transformToString(this.model,this.frameXslProc);
this.attachDomEvents();
if(nitobi.browser.MOZ){
var _1cb=nitobi.html.getCoords($("grid"+this.uid));
this.keyNav.style.left=_1cb.x+"px";
this.keyNav.style.top=_1cb.y+"px";
}
this.frameRendered=true;
this.fire("AfterFrameRender");
};
nitobi.grid.Grid.prototype.renderHeader=function(){
var _1cc=0;
endRow=this.getfreezetop()-1;
var tl=this.Scroller.view.topleft;
tl.top=this.getHeaderHeight();
tl.left=0;
tl.rowRenderer=this.TopLeftRenderer;
tl.renderGap(_1cc,endRow,false,"*");
var tc=this.Scroller.view.topcenter;
tc.top=this.getHeaderHeight();
tc.left=0;
tc.rowRenderer=this.TopCenterRenderer;
tc.renderGap(_1cc,endRow,false);
};
nitobi.grid.Grid.prototype.renderMiddle=function(){
this.Scroller.view.midleft.flushCache();
this.Scroller.view.midcenter.flushCache();
};
nitobi.grid.Grid.prototype.refresh=function(){
var _1cf=null;
if(!this.fire("BeforeRefresh",_1cf)){
return;
}
this.clear();
this.syncWithData();
this.subscribeOnce("HtmlReady",this.handleAfterRefresh,this);
};
nitobi.grid.Grid.prototype.handleAfterRefresh=function(){
var _1d0=null;
this.fire("AfterRefresh",_1d0);
};
nitobi.grid.Grid.prototype.clear=function(){
this.selectedRows=[];
this.clearData();
this.clearSurfaces();
};
nitobi.grid.Grid.prototype.handleContextMenu=function(evt,obj){
var _1d3=this.getOnContextMenuEvent();
if(_1d3==null){
return true;
}else{
if(this.fire("ContextMenu")){
return true;
}else{
evt.cancelBubble=true;
evt.returnValue=false;
return false;
}
}
};
nitobi.grid.Grid.prototype.handleKeyPress=function(evt){
if(this.activeCell==null){
return;
}
var col=new nitobi.grid.Cell(this,this.activeCell).getColumnObject();
this.fire("KeyPress",evt);
nitobi.event.evaluate(col.getOnKeyPressEvent(),evt);
nitobi.html.cancelEvent(evt);
return false;
};
nitobi.grid.Grid.prototype.handleKeyUp=function(evt){
if(this.activeCell==null){
return;
}
var col=new nitobi.grid.Cell(this,this.activeCell).getColumnObject();
this.fire("KeyUp",evt);
nitobi.event.evaluate(col.getOnKeyUpEvent(),evt);
};
nitobi.grid.Grid.prototype.handleKey=function(evt,obj){
if(this.activeCell!=null){
var col=new nitobi.grid.Cell(this,this.activeCell).getColumnObject();
var _1db=new nitobi.base.EventArgs(this);
if(!this.fire("KeyDown",_1db)||!nitobi.event.evaluate(col.getOnKeyDownEvent(),_1db)){
return;
}
}
var k=evt.keyCode;
k=k+(evt.shiftKey?256:0)+(evt.ctrlKey?512:0);
switch(k){
case 529:
break;
case 35:
break;
case 36:
break;
case 547:
break;
case 548:
break;
case 34:
this.page(1);
break;
case 33:
this.page(-1);
break;
case 45:
this.insertAfterCurrentRow();
break;
case 46:
this.deleteCurrentRow();
break;
case 292:
this.selectHome();
break;
case 290:
this.pageSelect(1);
break;
case 289:
this.pageSelect(-1);
break;
case 296:
this.reselect(0,1);
break;
case 294:
this.reselect(0,-1);
break;
case 293:
this.reselect(-1,0);
break;
case 295:
this.reselect(1,0);
break;
case 577:
break;
case 579:
case 557:
this.copy(evt);
return true;
break;
case 600:
case 302:
break;
case 598:
case 301:
this.paste(evt);
return true;
break;
case 35:
break;
case 36:
break;
case 547:
break;
case 548:
break;
case 13:
var et=this.getEnterTab().toLowerCase();
var _1de=0;
var vert=1;
if(et=="left"){
_1de=-1;
vert=0;
}else{
if(et=="right"){
_1de=1;
vert=0;
}else{
if(et=="down"){
_1de=0;
vert=1;
}else{
if(et=="up"){
_1de=0;
vert=-1;
}
}
}
}
this.move(_1de,vert);
break;
case 40:
this.move(0,1);
break;
case 269:
case 38:
this.move(0,-1);
break;
case 265:
case 37:
this.move(-1,0);
break;
case 9:
case 39:
this.move(1,0);
break;
case 577:
break;
default:
this.edit(evt);
}
};
nitobi.grid.Grid.prototype.reselect=function(x,y){
var S=this.selection;
var row=nitobi.grid.Cell.getRowNumber(S.endCell)+y;
var _1e4=nitobi.grid.Cell.getColumnNumber(S.endCell)+x;
if(_1e4>=0&&_1e4<this.columnCount()&&row>=0){
var _1e5=this.getCellElement(row,_1e4);
if(!_1e5){
return;
}
S.changeEndCellWithDomNode(_1e5);
S.alignBoxes();
this.ensureCellInView(_1e5);
}
};
nitobi.grid.Grid.prototype.pageSelect=function(dir){
};
nitobi.grid.Grid.prototype.selectHome=function(){
var S=this.selection;
var row=nitobi.grid.Cell.getRowNumber(S.endCell);
this.reselect(0,-row);
};
nitobi.grid.Grid.prototype.edit=function(evt){
if(this.activeCell==null){
return;
}
var cell=new nitobi.grid.Cell(this,this.activeCell);
var _1eb=new nitobi.grid.OnBeforeCellEditEventArgs(this,cell);
if(!this.fire("BeforeCellEdit",_1eb)||!nitobi.event.evaluate(cell.getColumnObject().getOnBeforeCellEditEvent(),_1eb)){
return;
}
var _1ec=null;
var _1ed=null;
var ctrl=null;
if(evt){
_1ec=evt.keyCode||null;
_1ed=evt.shiftKey||null;
ctrl=evt.ctrlKey||null;
}
var _1ef="";
var _1f0=null;
if((_1ed&&(_1ec>64)&&(_1ec<91))||(!_1ed&&((_1ec>47)&&(_1ec<58)))){
_1f0=0;
}
if(!_1ed){
if((_1ec>64)&&(_1ec<91)){
_1f0=32;
}else{
if(_1ec>95&&_1ec<106){
_1f0=-48;
}else{
if((_1ec==189)||(_1ec==109)){
_1ef="-";
}else{
if((_1ec>186)&&(_1ec<188)){
_1f0=-126;
}
}
}
}
}else{
}
if(_1f0!=null){
_1ef=String.fromCharCode(_1ec+_1f0);
}
if((!ctrl)&&(""!=_1ef)||(_1ec==113)||(_1ec==0)||(_1ec==null)||(_1ec==32)){
this.cellEditor=nitobi.form.ControlFactory.instance.getEditor(this,cell.getColumnObject());
if(this.cellEditor==null){
return;
}
this.cellEditor.setEditCompleteHandler(this.editComplete);
this.cellEditor.bind(this,cell,_1ef);
this.cellEditor.mimic();
nitobi.html.cancelEvent(evt);
return false;
}else{
return;
}
};
nitobi.grid.Grid.prototype.editComplete=function(_1f1){
var cell=_1f1.cell;
var _1f3=cell.getColumnObject();
var _1f4=_1f1.databaseValue;
var _1f5=_1f1.displayValue;
var _1f6=new nitobi.grid.OnCellValidateEventArgs(this,cell,_1f4,cell.getValue());
if(!this.fire("CellValidate",_1f6)||!nitobi.event.evaluate(_1f3.getOnCellValidateEvent(),_1f6)){
return false;
}
cell.setValue(_1f4,_1f5);
_1f1.editor.hide();
var _1f7=new nitobi.grid.OnAfterCellEditEventArgs(this,cell);
this.fire("AfterCellEdit",_1f7);
nitobi.event.evaluate(_1f3.getOnAfterCellEditEvent(),_1f7);
this.focus();
};
nitobi.grid.Grid.prototype.autoSave=function(){
if(this.isAutoSaveEnabled()){
return this.save();
}
return false;
};
nitobi.grid.Grid.prototype.selectCellByCoords=function(row,_1f9){
this.setPosition(row,_1f9);
};
nitobi.grid.Grid.prototype.setPosition=function(row,_1fb){
if(row>=0&&_1fb>=0){
this.setActiveCell(this.getCellElement(row,_1fb));
}
};
nitobi.grid.Grid.prototype.save=function(){
if(this.datatable.log.selectNodes("//"+nitobi.xml.nsPrefix+"data/*").length==0){
return;
}
if(!this.fire("BeforeSave")){
return;
}
this.datatable.save(nitobi.lang.close(this,this.saveCompleteHandler),this.getOnBeforeSaveEvent());
};
nitobi.grid.Grid.prototype.saveCompleteHandler=function(_1fc){
if(this.getDataSource().getHandlerError()){
this.fire("HandlerError",_1fc);
}
this.fire("AfterSave",_1fc);
};
nitobi.grid.Grid.prototype.focus=function(){
try{
this.keyNav.focus();
this.fire("Focus",new nitobi.base.EventArgs(this));
nitobi.html.cancelEvent(nitobi.html.Event);
return false;
}
catch(e){
}
};
nitobi.grid.Grid.prototype.getRendererForColumn=function(col){
var _1fe=this.getColumnCount();
if(col>=_1fe){
col=_1fe-1;
}
var _1ff=this.getFrozenLeftColumnCount();
if(col<frozenLeft){
return this.MidLeftRenderer;
}else{
return this.MidCenterRenderer;
}
};
nitobi.grid.Grid.prototype.getColumnOuterTemplate=function(col){
return this.getRendererForColumn(col).xmlTemplate.selectSingleNode("//*[@match='ntb:e']/div/div["+col+"]");
};
nitobi.grid.Grid.prototype.getColumnInnerTemplate=function(col){
return this.getColumnOuterXslTemplate(col).selectSingleNode("*[2]");
};
nitobi.grid.Grid.prototype.makeXSL=function(){
var fL=this.getFrozenLeftColumnCount();
var fR=this.getfreezeright();
var cs=this.getColumnCount();
var rh=this.isRowHighlightEnabled();
var _206="_default";
if(this.datatable!=null){
_206=this.datatable.id;
}
var _207=0;
var _208=fL;
var sXml=nitobi.xml.serialize(this.model.selectSingleNode("state/nitobi.grid.Columns")).replace(/\#\&lt\;\#/g,"#<#").replace(/\#\&gt\;\#/g,"#>#").replace(/\#\&eq\;\#/g,"#=#").replace(/\#\&quot\;\#/g,"#\"#").replace(/\&/g,"&amp;").replace(/\#<\#/g,"&lt;").replace(/\#>\#/g,"&gt;").replace(/\#=\#/g,"&eq;").replace(/\#\"\#/g,"&quot;");
sXml=sXml.replace(/(\&amp;lt;xsl\:)(.*?)(\/&amp;gt;)/g,function(){
return "&lt;xsl:"+arguments[2].replace(/\&amp;/g,"&")+"/&gt;";
});
if(this.oldColDefs!=sXml){
this.oldColDefs=sXml;
var _20a=nitobi.xml.createXmlDoc(sXml);
this.TopLeftRenderer.generateXslTemplate(_20a,this.rowXslGeneratorXslProc,_207,_208,this.isColumnIndicatorsEnabled(),this.isRowIndicatorsEnabled(),rh);
this.TopLeftRenderer.dataTableId=_206;
_207=fL;
_208=cs-fR-fL;
this.TopCenterRenderer.generateXslTemplate(_20a,this.rowXslGeneratorXslProc,_207,_208,this.isColumnIndicatorsEnabled(),this.isRowIndicatorsEnabled(),rh);
this.TopCenterRenderer.dataTableId=_206;
this.MidLeftRenderer.generateXslTemplate(_20a,this.rowXslGeneratorXslProc,0,fL,0,this.isRowIndicatorsEnabled(),rh,"left");
this.MidLeftRenderer.dataTableId=_206;
this.MidCenterRenderer.generateXslTemplate(_20a,this.rowXslGeneratorXslProc,fL,cs-fR-fL,0,0,rh);
this.MidCenterRenderer.dataTableId=_206;
}
this.fire("AfterMakeXsl");
};
nitobi.grid.Grid.prototype.render=function(){
this.generateCss();
this.updateCellRanges();
return;
};
nitobi.grid.Grid.prototype.refilter=nitobi.grid.Grid.prototype.render;
nitobi.grid.Grid.prototype.getColumnDefinitions=function(){
return this.model.selectNodes("state/nitobi.grid.Columns/*");
};
nitobi.grid.Grid.prototype.initializeModelFromDeclaration=function(){
this.modelInitializerXslProc=nitobi.xml.createXslProcessor(nitobi.grid.modelFromDeclarationInitializerXslProc.stylesheet);
eval(nitobi.xml.transformToString(this.Interface,this.modelInitializerXslProc));
this.model.documentElement.setAttribute("ID",this.uid);
this.model.documentElement.setAttribute("uniqueID",this.uid);
};
nitobi.grid.Grid.prototype.setModelDefaults=function(_20b,_20c,_20d){
var _20e=this.API.selectNodes(_20d);
for(var j=0;j<_20e.length;j++){
var _210=_20e[j].getAttribute("htmltag")+"";
var _211=_20e[j].getAttribute("name")+"";
var _212=_20c.getAttribute(_210)||_20c.getAttribute(_211);
var _213=_20e[j].getAttribute("default").replace(/\"/g,"");
_212=_212?_212:_213;
if(_20e[j].getAttribute("type")=="bool"){
_212=nitobi.lang.boolToStr(nitobi.lang.toBool(_212));
}
_20b.setAttribute(_20e[j].getAttribute("name"),_212?_212:_213);
}
};
nitobi.grid.Grid.prototype.getNewRecordKey=function(){
var _214;
var key;
var _216;
do{
_214=new Date();
key=(_214.getTime()+"."+Math.round(Math.random()*99));
_216=this.datatable.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@xk = '"+key+"']");
}while(_216!=null);
return key;
};
nitobi.grid.Grid.prototype.insertAfterCurrentRow=function(){
if(this.activeCell){
var _217=nitobi.grid.Cell.getRowNumber(this.activeCell);
this.insertRow(_217+1);
}else{
this.insertRow();
}
};
nitobi.grid.Grid.prototype.insertRow=function(_218){
var rows=parseInt(this.getDisplayedRowCount());
var xi=0;
if(_218!=null){
xi=parseInt((_218==null?rows:parseInt(_218)));
xi--;
}
var _21b=new nitobi.grid.OnBeforeRowInsertEventArgs(this,this.getRowObject(xi));
if(!this.isRowInsertEnabled()||!this.fire("BeforeRowInsert",_21b)){
return;
}
var _21c=this.datatable.getTemplateNode();
for(var i=0;i<this.columnCount();i++){
var _21e=this.getColumnObject(i);
var _21f=_21e.getInitial();
if(_21f==null||_21f==""){
var _220=_21e.getDataType();
if(_220==null||_220==""){
_220="text";
}
switch(_220){
case "text":
_21f="";
break;
case "number":
_21f=0;
break;
case "date":
_21f="1900-01-01";
break;
}
}
var att=_21e.getxdatafld().substr(1);
if(att!=null&&att!=""){
_21c.setAttribute(att,_21f);
}
}
this.clearSurfaces();
this.datatable.createRecord(_21c,xi);
this.subscribeOnce("HtmlReady",this.handleAfterRowInsert,this,[xi]);
};
nitobi.grid.Grid.prototype.handleAfterRowInsert=function(xi){
this.fire("AfterRowInsert",new nitobi.grid.OnAfterRowInsertEventArgs(this,this.getRowObject(xi)));
this.setActiveCell(this.getCellElement(xi,0));
};
nitobi.grid.Grid.prototype.deleteCurrentRow=function(){
if(this.activeCell){
this.deleteRow(nitobi.grid.Cell.getRowNumber(this.activeCell));
}else{
alert("First select a record to delete.");
}
};
nitobi.grid.Grid.prototype.deleteRow=function(_223){
var _224=new nitobi.grid.OnBeforeRowDeleteEventArgs(this,this.getRowObject(_223));
if(!this.isRowDeleteEnabled()||!this.fire("BeforeRowDelete",_224)){
return;
}
this.clearSurfaces();
var rows=this.getDisplayedRowCount();
var xi=rows-1;
try{
this.datatable.deleteRecord(_223);
rows--;
if(rows<=0){
this.activeCell=null;
}
this.subscribeOnce("HtmlReady",this.handleAfterRowDelete,this,[_223]);
}
catch(err){
this.dataBind();
}
};
nitobi.grid.Grid.prototype.handleAfterRowDelete=function(xi){
this.fire("AfterRowDelete",new nitobi.grid.OnBeforeRowDeleteEventArgs(this,this.getRowObject(xi)));
this.setActiveCell(this.getCellElement(xi,0));
};
nitobi.grid.Grid.prototype.onNextPage=function(){
this.loadNextDataPage();
};
nitobi.grid.Grid.prototype.page=function(dir){
};
nitobi.grid.Grid.prototype.selectionMoved=function(h,v){
eval(this.onMove);
};
nitobi.grid.Grid.prototype.move=function(h,v){
if(this.activeCell!=null){
var hs=1;
var vs=1;
h=(h*hs);
v=(v*vs);
var cell=nitobi.grid.Cell;
var _230=cell.getColumnNumber(this.activeCell);
var _231=cell.getRowNumber(this.activeCell);
this.selectCellByCoords(_231+v,_230+h);
var _232=new nitobi.grid.CellEventArgs(this,this.activeCell);
if(_230+1==this.getColumnDefinitions().length&&h==1){
this.fire("HitRowEnd",_232);
}else{
if(_230==0&&h==-1){
this.fire("HitRowStart",_232);
}
}
}
};
nitobi.grid.Grid.prototype.handleClick=function(evt){
if(this.isSingleClickEditEnabled()){
this.edit(evt);
}
};
nitobi.grid.Grid.prototype.handleDblClick=function(evt){
var cell=new nitobi.grid.Cell(this,this.activeCell);
var _236=new nitobi.grid.OnCellDblClickEventArgs(this,cell);
return this.fire("CellDblClick",_236)&&nitobi.event.evaluate(cell.getColumnObject().getOnCellDblClickEvent(),_236);
};
nitobi.grid.Grid.prototype.loadNextDataPage=function(){
this.loadDataPage(this.getCurrentPageIndex()+1);
};
nitobi.grid.Grid.prototype.onPreviousPage=function(){
this.loadPreviousDataPage();
};
nitobi.grid.Grid.prototype.loadPreviousDataPage=function(){
this.loadDataPage(this.getCurrentPageIndex()-1);
};
nitobi.grid.Grid.prototype.GetPage=function(_237){
ebaErrorReport("GetPage is deprecated please use loadDataPage instead","",EBA_DEBUG);
this.loadDataPage(_237);
};
nitobi.grid.Grid.prototype.loadDataPage=function(_238){
};
nitobi.grid.Grid.prototype.getSelectedRow=function(rel){
try{
var nRow=-1;
var AC=this.activeCell;
if(AC!=null){
nRow=nitobi.grid.Cell.getRowNumber(AC);
if(rel){
nRow-=this.getfreezetop();
}
}
return nRow;
}
catch(err){
_ntbAssert(false,err.message);
}
};
nitobi.grid.Grid.prototype.handleHandlerError=function(){
var _23c=this.getDataSource().getHandlerError();
if(_23c){
this.fire("HandlerError");
}
};
nitobi.grid.Grid.prototype.getRowObject=function(_23d,_23e){
var _23f=_23e;
if(_23e==null&&_23d!=null){
_23f=_23d;
}
return new nitobi.grid.Row(this,_23f);
};
nitobi.grid.Grid.prototype.getSelectedColumn=function(rel){
try{
var nCol=-1;
var AC=this.activeCell;
if(AC!=null){
nCol=parseInt(AC.getAttribute("col"));
if(rel){
nCol-=this.getFrozenLeftColumnCount();
}
}
return nCol;
}
catch(err){
_ntbAssert(false,err.message);
}
};
nitobi.grid.Grid.prototype.getSelectedColumnObject=function(){
return this.getColumnObject(this.getSelectedColumn());
};
nitobi.grid.Grid.prototype.columnCount=function(){
try{
var _243=this.getColumnDefinitions();
return _243.length;
}
catch(err){
_ntbAssert(false,err.message);
}
};
nitobi.grid.Grid.prototype.getCellObject=function(row,col){
if(typeof (col)=="string"){
var node=this.model.selectSingleNode("state/nitobi.grid.Columns/nitobi.grid.Column[@xdatafld_orig='"+col+"']");
if(node!=null){
col=parseInt(node.getAttribute("xi"));
}
}
var cell=new nitobi.grid.Cell(this,row,col);
return cell;
};
nitobi.grid.Grid.prototype.getCellText=function(row,col){
return this.getCellObject(row,col).getHtml();
};
nitobi.grid.Grid.prototype.getCellValue=function(row,col){
return this.getCellObject(row,col).getValue();
};
nitobi.grid.Grid.prototype.getCellElement=function(row,_24d){
return document.getElementById("cell_"+row+"_"+_24d+"_"+this.uid);
};
nitobi.grid.Grid.prototype.getSelectedRowObject=function(xi){
var obj=null;
var r=nitobi.grid.Cell.getRowNumber(this.activeCell);
obj=new nitobi.grid.Row(this,r);
return obj;
};
nitobi.grid.Grid.prototype.getColumnObject=function(_251){
var _252=null;
if(_251>=0){
_252=this.columns[_251];
if(_252==null){
var _253=this.getColumnDefinitions()[_251].getAttribute("DataType");
switch(_253){
case "number":
_252=new nitobi.grid.NumberColumn(this,_251);
break;
case "date":
_252=new nitobi.grid.DateColumn(this,_251);
break;
default:
_252=new nitobi.grid.TextColumn(this,_251);
break;
}
this.columns[_251]=_252;
}
}
if(_252==null||_252.ModelNode==null){
return null;
}else{
return _252;
}
};
nitobi.grid.Grid.prototype.getSelectedCellObject=function(){
var obj=null;
var _255=this.activeCell;
if(_255!=null){
var r=nitobi.grid.Cell.getRowNumber(_255);
var c=nitobi.grid.Cell.getColumnNumber(_255);
obj=this.getCellObject(r,c);
}
return obj;
};
nitobi.grid.Grid.prototype.autoAddRow=function(){
if(this.activeCell.innerText.replace(/\s/g,"")!=""&&this.autoAdd){
this.deactivateCell();
if(this.active=="Y"){
this.freezeCell();
}
eval(this.getOnRowBlurEvent());
this.insertRow();
this.go("HOME");
this.editCell();
}
};
nitobi.grid.Grid.prototype.setDisplayedRowCount=function(_258){
if(this.Scroller){
this.Scroller.view.midcenter.rows=_258;
this.Scroller.view.midleft.rows=_258;
}
this.displayedRowCount=_258;
};
nitobi.grid.Grid.prototype.incrementDisplayedRowCount=function(_259){
this.setDisplayedRowCount(this.getDisplayedRowCount()+(_259||1));
this.updateCellRanges();
};
nitobi.grid.Grid.prototype.decrementDisplayedRowCount=function(_25a){
this.setDisplayedRowCount(this.getDisplayedRowCount()-(_25a||1));
this.updateCellRanges();
};
nitobi.grid.Grid.prototype.getDisplayedRowCount=function(){
return this.displayedRowCount;
};
nitobi.grid.Grid.prototype.copy=function(){
var _25b=this.selection.getCoords();
var data=this.getTableForSelection(_25b);
var _25d=new nitobi.grid.OnCopyEventArgs(this,data,_25b);
if(!this.isCopyEnabled()||!this.fire("BeforeCopy",_25d)){
return;
}
if(!nitobi.browser.IE){
var _25e=this.getClipboard();
_25e.onkeyup=nitobi.lang.close(this,this.focus);
_25e.value=data;
_25e.focus();
_25e.setSelectionRange(0,_25e.value.length);
}
if(nitobi.browser.IE){
window.clipboardData.setData("Text",data);
}
this.fire("AfterCopy",_25d);
};
nitobi.grid.Grid.prototype.getTableForSelection=function(_25f){
var _260=this.getColumnMap(_25f.top.x,_25f.bottom.x);
var _261=nitobi.data.FormatConverter.convertEbaXmlToTsv(this.getDataSource().getDataXmlDoc(),_260,_25f.top.y,_25f.bottom.y);
return _261;
};
nitobi.grid.Grid.prototype.getColumnMap=function(_262,_263){
var _264=this.getColumnDefinitions();
_262=(_262==null)?0:_262;
_263=(_263==null)?_264.length-1:_263;
var map=new Array();
for(var i=_262;i<=_263&&(null!=_264[i]);i++){
map.push(_264[i].getAttribute("xdatafld").substr(1));
}
return map;
};
nitobi.grid.Grid.prototype.paste=function(){
if(!this.isPasteEnabled()){
return;
}
var _267=this.getClipboard();
_267.onkeyup=nitobi.lang.close(this,this.pasteDataReady,[_267]);
_267.focus();
return _267;
};
nitobi.grid.Grid.prototype.pasteDataReady=function(_268){
_268.onkeyup=null;
var _269=this.selection;
var _26a=_269.getCoords();
var _26b=_26a.top.x;
var _26c=_26b+nitobi.data.FormatConverter.getDataColumns(_268.value)-1;
var _26d=true;
for(var i=_26b;i<=_26c;i++){
var _26f=this.getColumnObject(i);
if(_26f){
if(!_26f.isEditable()){
_26d=false;
break;
}
}
}
if(!_26d){
this.fire("PasteFailed",new nitobi.base.EventArgs(this));
this.handleAfterPaste();
return;
}else{
var _270=this.getColumnMap(_26b,_26c);
var _271=_26a.top.y;
var _272=Math.max(_271+nitobi.data.FormatConverter.getDataRows(_268.value)-1,0);
this.getSelection().selectWithCoords(_271,_26b,_272,_26b+_270.length-1);
var _273=new nitobi.grid.OnPasteEventArgs(this,_268.value,_26a);
if(!this.fire("BeforePaste",_273)){
return;
}
var _274=_268.value;
var _275=null;
if(_274.substr(0,1)=="<"){
_275=nitobi.data.FormatConverter.convertHtmlTableToEbaXml(_274,_270,_271);
}else{
_275=nitobi.data.FormatConverter.convertTsvToEbaXml(_274,_270,_271);
}
if(_275.documentElement!=null){
this.datatable.mergeFromXml(_275,nitobi.lang.close(this,this.pasteComplete,[_275,_271,_272,_273]));
}
}
};
nitobi.grid.Grid.prototype.pasteComplete=function(_276,_277,_278,_279){
this.Scroller.reRender(_277,_278);
this.subscribeOnce("HtmlReady",this.handleAfterPaste,this,[_279]);
};
nitobi.grid.Grid.prototype.handleAfterPaste=function(_27a){
this.fire("AfterPaste",_27a);
};
nitobi.grid.Grid.prototype.getClipboard=function(){
var _27b=document.getElementById(this.uid+"_ebaclipboard");
if(!_27b){
_27b=nitobi.html.createElement("textarea",{"name":this.uid+"_ebaclipboard","id":this.uid+"_ebaclipboard"},{"position":"absolute","top":"0px","left":"0px","visibility":"hidden"});
document.body.appendChild(_27b);
}
_27b.onkeyup=null;
_27b.value="";
return _27b;
};
nitobi.grid.Grid.prototype.handleHtmlReady=function(_27c){
this.fire("HtmlReady",new nitobi.base.EventArgs(this));
};
nitobi.grid.Grid.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.grid.Grid.prototype.subscribe=function(evt,func,_281){
if(this.subscribedEvents==null){
this.subscribedEvents={};
}
if(typeof (_281)=="undefined"){
_281=this;
}
var guid=nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_281,func));
this.subscribedEvents[guid]=evt+this.uid;
return guid;
};
nitobi.grid.Grid.prototype.subscribeOnce=function(evt,func,_285,_286){
var guid=null;
var _288=this;
var _289=function(){
func.apply(_285||this,_286||arguments);
_288.unsubscribe(evt,guid);
};
guid=this.subscribe(evt,_289);
};
nitobi.grid.Grid.prototype.unsubscribe=function(evt,guid){
return nitobi.event.unsubscribe(evt+this.uid,guid);
};
nitobi.grid.Grid.prototype.xGET=function(){
var val="";
if(this.model&&this.model.documentElement){
var node=this.model.documentElement.selectSingleNode(arguments[0]);
if(node!=null){
val=node.nodeValue;
}
}
return val;
};
nitobi.grid.Grid.prototype.xSET=function(){
if((arguments[1][0]!=null)&&(this.model)&&(this.model.documentElement)&&(this.model.documentElement.selectSingleNode(arguments[0]))){
var node=this.model.documentElement.selectSingleNode(arguments[0]);
if(typeof (arguments[1][0])=="boolean"){
node.nodeValue=nitobi.lang.boolToStr(arguments[1][0]);
}else{
node.nodeValue=arguments[1][0];
}
}
};
nitobi.grid.Grid.prototype.eSET=function(name,args){
var _291=args[0];
var _292=_291;
var _293=name.substr(2);
_293=_293.substr(0,_293.length-5);
if(typeof (_291)=="string"){
_292=function(){
return nitobi.event.evaluate(_291,arguments[0]);
};
}
if(this[name]!=null){
this.unsubscribe(_293,this[name]);
}
var guid=this.subscribe(_293,_292);
this.jSET(name,[guid]);
return guid;
};
nitobi.grid.Grid.prototype.jSET=function(name,val){
this[name]=val[0];
};
nitobi.grid.Grid.prototype.dispose=function(){
try{
this.element.jsObject=null;
editorXslProc=null;
var _297=nitobi.html.getFirstChild(this.UiContainer);
nitobi.html.detachEvents(_297,this.events,this,false);
nitobi.html.detachEvents(this.keyNav,this.keyEvents,this,false);
for(var item in this.subscribedEvents){
var _299=this.subscribedEvents[item];
this.unsubscribe(_299.substring(0,_299.length-this.uid.length),item);
}
this.UiContainer.parentNode.removeChild(this.UiContainer);
for(var item in this){
if(this[item]!=null){
if(this[item].dispose instanceof Function){
this[item].dispose();
}
this[item]=null;
}
}
nitobi.form.ControlFactory.instance.dispose();
}
catch(e){
}
};
nitobi.Grid=nitobi.grid.Grid;
nitobi.grid.Cell=function(grid,row,_29c){
if(row==null){
return null;
}
this.Interface=grid.API.selectSingleNode("interfaces/interface[@name='nitobi.grid.Cell']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
this.grid=grid;
var _29d=null;
if(typeof (row)=="object"){
var cell=row;
row=Number(cell.getAttribute("xi"));
_29c=cell.getAttribute("col");
_29d=cell;
}else{
_29d=this.grid.getCellElement(row,_29c);
}
this.DomNode=_29d;
this.row=Number(row);
this.Row=this.row;
this.column=Number(_29c);
this.Column=this.column;
this.columnObject=this.grid.getColumnObject(this.Column);
this.dataIndex=this.Row;
var _29f=this.grid.datatable;
this.DataNode=_29f.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@xi="+this.dataIndex+"]/"+_29f.fieldMap[this.columnObject.getColumnName()]);
this.ModelNode=this.grid.model.selectSingleNode("//nitobi.grid.Columns/nitobi.grid.Column[@xi='"+this.column+"']");
};
nitobi.grid.Cell.prototype.setValue=function(_2a0,_2a1){
if(_2a0==this.getValue()){
return;
}
var _2a2=this.columnObject;
var _2a3="";
switch(_2a2.getType()){
case "PASSWORD":
for(var i=0;i<_2a0.length;i++){
_2a3+="*";
}
break;
case "NUMBER":
if(this.numberXsl==null){
this.numberXsl=nitobi.form.numberXslProc.stylesheet;
}
if(_2a0==""){
_2a0=nitobi.form.Number.defaultValue;
}
if(this.DomNode!=null){
if(_2a0<0){
nitobi.html.Css.addClass(this.DomNode,"ntb-grid-numbercellnegative");
}else{
nitobi.html.Css.removeClass(this.DomNode,"ntb-grid-numbercellnegative");
}
}
var mask=_2a2.getMask();
var _2a6=_2a2.getNegativeMask();
var _2a7=_2a0;
if(_2a0<0&&_2a6!=""){
mask=_2a6;
_2a7=(_2a0+"").replace("-","");
}
var _2a8=nitobi.xml.createXmlDoc("<root><number>"+_2a7+"</number><mask>"+mask+"</mask><group>"+_2a2.getGroupingSeparator()+"</group><decimal>"+_2a2.getDecimalSeparator()+"</decimal></root>");
_2a3=nitobi.xml.transformToString(_2a8,this.numberXsl);
if(""==_2a3&&_2a0!=""){
_2a3=nitobi.html.getFirstChild(this.DomNode).innerHTML;
_2a0=this.getValue();
}
break;
case "DATE":
if(this.dateXsl==null){
this.dateXsl=nitobi.form.dateXslProc.stylesheet;
}
var _2a8=nitobi.xml.createXmlDoc("<root><date>"+_2a0+"</date><mask>"+_2a2.getMask()+"</mask></root>");
_2a3=nitobi.xml.transformToString(_2a8,this.dateXsl);
if(""==_2a3){
_2a3=nitobi.html.getFirstChild(this.DomNode).innerHTML;
_2a0=this.getValue();
}
break;
case "TEXTAREA":
_2a3=nitobi.html.encode(_2a0);
break;
case "LOOKUP":
var _2a9=_2a2.ModelNode.getAttribute("DatasourceId");
var _2aa=this.grid.data.getTable(_2a9);
var _2ab=_2a2.ModelNode.getAttribute("DisplayFields");
var _2ac=_2a2.ModelNode.getAttribute("ValueField");
var _2ad=_2aa.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@"+_2ac+"='"+_2a0+"']/@"+_2ab);
if(_2ad!=null){
_2a3=_2ad.nodeValue;
}else{
_2a3=_2a0;
}
break;
case "CHECKBOX":
var _2a9=_2a2.ModelNode.getAttribute("DatasourceId");
var _2aa=this.grid.data.getTable(_2a9);
var _2ab=_2a2.ModelNode.getAttribute("DisplayFields");
var _2ac=_2a2.ModelNode.getAttribute("ValueField");
var _2ae=_2a2.ModelNode.getAttribute("CheckedValue");
if(_2ae==""||_2ae==null){
_2ae=0;
}
var _2af=_2aa.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@"+_2ac+"='"+_2a0+"']/@"+_2ab).nodeValue;
var _2b0=(_2a0==_2ae)?"checked":"unchecked";
_2a3="<div style=\"overflow:hidden;\"><div style=\"float:left;\" class=\"ntbcheckbox ntbcheckbox"+_2b0+" checkbox"+_2b0+"\" checked=\""+_2a0+"\">&nbsp;</div><span>"+nitobi.html.encode(_2af)+"</span></div>";
break;
case "LISTBOX":
var _2a9=_2a2.ModelNode.getAttribute("DatasourceId");
var _2aa=this.grid.data.getTable(_2a9);
var _2ab=_2a2.ModelNode.getAttribute("DisplayFields");
var _2ac=_2a2.ModelNode.getAttribute("ValueField");
_2a3=_2aa.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@"+_2ac+"='"+_2a0+"']/@"+_2ab).nodeValue;
break;
case "IMAGE":
_2a3=nitobi.html.getFirstChild(this.DomNode).innerHTML;
if(nitobi.lang.typeOf(_2a0)==nitobi.lang.type.HTMLNODE){
_2a3="<img border=\"0\" src=\""+_2a0.getAttribute("src")+"\" />";
}else{
if(typeof (_2a0)=="string"){
_2a3="<img border=\"0\" src=\""+_2a0+"\" />";
}
}
break;
default:
_2a3=_2a0;
}
if(this.DomNode!=null){
nitobi.html.getFirstChild(this.DomNode).innerHTML=_2a3;
this.DomNode.setAttribute("value",_2a0);
}
this.grid.datatable.updateRecord(this.dataIndex,_2a2.getColumnName(),_2a0);
};
nitobi.grid.Cell.prototype.getValue=function(){
var _2b1=this.columnObject;
var val=this.GETDATA();
switch(_2b1.getType()){
case "NUMBER":
val=parseFloat(val);
break;
default:
}
return val;
};
nitobi.grid.Cell.prototype.getHtml=function(){
return nitobi.html.getFirstChild(this.DomNode).innerHTML;
};
nitobi.grid.Cell.prototype.edit=function(){
this.grid.setActiveCell(this.DomNode);
this.grid.edit();
};
nitobi.grid.Cell.prototype.GETDATA=function(){
var node=this.DataNode;
if(node!=null){
return node.value;
}
};
nitobi.grid.Cell.prototype.xGETMETA=function(){
if(this.MetaNode==null){
return null;
}
var node=this.MetaNode;
node=node.selectSingleNode("@"+arguments[0]);
if(node!=null){
return node.value;
}
};
nitobi.grid.Cell.prototype.xSETMETA=function(){
var node=this.MetaNode;
if(node!=null){
node.setAttribute(arguments[0],arguments[1][0]);
}else{
alert("Cannot set property: "+arguments[0]);
}
};
nitobi.grid.Cell.prototype.xSETCSS=function(){
var node=this.DomNode;
if(node!=null){
node.style.setAttribute(arguments[0],arguments[1][0]);
}else{
alert("Cannot set property: "+arguments[0]);
}
};
nitobi.grid.Cell.prototype.xGET=function(){
var node=this.ModelNode;
node=node.selectSingleNode(arguments[0]);
if(node!=null){
return node.value;
}
};
nitobi.grid.Cell.prototype.xSET=function(){
var node=this.ModelNode;
node=node.selectSingleNode(arguments[0]);
if(node!=null){
node.nodeValue=arguments[1][0];
}
};
nitobi.grid.Cell.prototype.getStyle=function(){
return this.DomNode.style;
};
nitobi.grid.Cell.prototype.getColumnObject=function(){
if(typeof (this.columnObject)=="undefined"){
this.columnObject=this.grid.getColumnObject(this.getColumn());
}
return this.columnObject;
};
nitobi.grid.Cell.getCellElement=function(grid,row,_2bb){
return $("cell_"+row+"_"+_2bb+"_"+grid.uid);
};
nitobi.grid.Cell.getRowNumber=function(_2bc){
return parseInt(_2bc.getAttribute("xi"));
};
nitobi.grid.Cell.getColumnNumber=function(_2bd){
return parseInt(_2bd.getAttribute("col"));
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.CellEventArgs=function(_2be,cell){
nitobi.grid.CellEventArgs.baseConstructor.call(this,_2be);
this.cell=cell;
};
nitobi.lang.extend(nitobi.grid.CellEventArgs,nitobi.base.EventArgs);
nitobi.grid.CellEventArgs.prototype.getCell=function(){
return this.cell;
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.RowEventArgs=function(_2c0,row){
this.grid=_2c0;
this.row=row;
this.event=nitobi.html.Event;
};
nitobi.grid.RowEventArgs.prototype.getSource=function(){
return this.grid;
};
nitobi.grid.RowEventArgs.prototype.getRow=function(){
return this.row;
};
nitobi.grid.RowEventArgs.prototype.getEvent=function(){
return this.event;
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.SelectionEventArgs=function(_2c2,data,_2c4){
this.source=_2c2;
this.coords=_2c4;
this.data=data;
};
nitobi.grid.SelectionEventArgs.prototype.getSource=function(){
return this.source;
};
nitobi.grid.SelectionEventArgs.prototype.getCoords=function(){
return this.coords;
};
nitobi.grid.SelectionEventArgs.prototype.getData=function(){
return this.data;
};
nitobi.grid.Column=function(grid,_2c6){
this.grid=grid;
this.column=_2c6;
this.uid=nitobi.base.getUid();
this.Interface=this.grid.API.selectSingleNode("interfaces/interface[@name='nitobi.grid.Column']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
var _2c7=null;
if(nitobi.browser.MOZ){
_2c7=this.grid.model.selectSingleNode("//state/nitobi.grid.Columns/nitobi.grid.Column["+(parseInt(_2c6)+1)+"]");
}else{
_2c7=this.grid.model.selectSingleNode("//state/nitobi.grid.Columns/nitobi.grid.Column["+(_2c6)+"]");
}
this.ModelNode=_2c7;
};
nitobi.grid.Column.prototype.getHeaderElement=function(){
return nitobi.grid.Column.getColumnHeaderElement(this.grid,this.column);
};
nitobi.grid.Column.prototype.getEditor=function(){
};
nitobi.grid.Column.prototype.getStyle=function(){
var _2c8=this.getClassName();
return nitobi.html.getClass(_2c8);
};
nitobi.grid.Column.prototype.getHeaderStyle=function(){
var _2c9="acolumnheader"+this.grid.uid+"_"+this.column;
return nitobi.html.getClass(_2c9);
};
nitobi.grid.Column.prototype.getDataStyle=function(){
var _2ca="ntbcolumndata"+this.grid.uid+"_"+this.column;
return nitobi.html.getClass(_2ca);
};
nitobi.grid.Column.prototype.getEditor=function(){
return nitobi.form.ControlFactory.instance.getEditor(this.grid,this);
};
nitobi.grid.Column.prototype.xGETMODEL=function(){
var node=this.ModelNode;
node=node.selectSingleNode("@"+arguments[0]);
if(node!=null){
return node.value;
}
};
nitobi.grid.Column.prototype.xSETMODEL=function(){
var node=this.ModelNode;
if(node!=null){
node.setAttribute(arguments[0],arguments[1][0]);
}else{
alert("Cannot set model property: "+arguments[0]);
}
};
nitobi.grid.Column.prototype.xGET=function(){
var node=this.grid.model.documentElement;
node=node.selectSingleNode(arguments[0]);
if(node!=null){
return node.value;
}
};
nitobi.grid.Column.prototype.xSET=function(){
var node=this.grid.model.documentElement;
node=node.selectSingleNode(arguments[0]);
if(node!=null){
node.nodeValue=arguments[1][0];
}
};
nitobi.grid.Column.prototype.eSET=function(name,_2d0){
var _2d1=_2d0[0];
var _2d2=_2d1;
var _2d3=name.substr(2);
_2d3=_2d3.substr(0,_2d3.length-5);
if(typeof (_2d1)=="string"){
_2d2=function(_2d4){
return eval(_2d1);
};
}
if(typeof (this[name])!="undefined"){
alert("unsubscribe");
this.unsubscribe(_2d3,this[name]);
}
var guid=this.subscribe(_2d3,_2d2);
this.jSET(name,[guid]);
};
nitobi.grid.Column.prototype.jSET=function(name,val){
this[name]=val[0];
};
nitobi.grid.Column.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.grid.Column.prototype.subscribe=function(evt,func,_2dc){
if(typeof (_2dc)=="undefined"){
_2dc=this;
}
return nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_2dc,func));
};
nitobi.grid.Column.prototype.unsubscribe=function(evt,func){
return nitobi.event.unsubscribe(evt+this.uid,func);
};
nitobi.grid.Column.getColumnHeaderElement=function(grid,_2e0){
return $("columnheader_"+_2e0+"_"+grid.uid);
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.ColumnEventArgs=function(_2e1,_2e2){
this.grid=_2e1;
this.column=_2e2;
this.event=nitobi.html.Event;
};
nitobi.grid.ColumnEventArgs.prototype.getSource=function(){
return this.grid;
};
nitobi.grid.ColumnEventArgs.prototype.getColumn=function(){
return this.column;
};
nitobi.grid.ColumnEventArgs.prototype.getEvent=function(){
return this.event;
};
nitobi.grid.ColumnEventArgs.prototype.getDirection=function(){
};
nitobi.grid.ColumnResizer=function(grid){
this.grid=grid;
this.hScrollClass=null;
this.surfaceClass=null;
this.columClass=null;
this.line=document.getElementById("ebagridresizeline_");
if(this.line==null){
this.line=document.createElement("div");
this.line.id="ebagridresizeline_";
document.body.appendChild(this.line);
this.line.className="ntbcolumnresizeline";
}
this.lineStyle=this.line.style;
if(nitobi.browser.IE){
this.surface=document.getElementById("ebagridresizesurface_");
if(this.surface==null){
this.surface=document.createElement("div");
this.surface.id="ebagridresizesurface_";
this.surface.className="ntbcolumnresizesurface";
document.body.appendChild(this.surface);
}
}
};
nitobi.grid.ColumnResizer.prototype.startResize=function(grid,_2e5,_2e6,_2e7){
this.grid=grid;
this.column=_2e5;
var _2e8=new nitobi.grid.OnBeforeColumnResizeEventArgs(this.grid,this.column);
if(!nitobi.event.evaluate(_2e5.getOnBeforeResizeEvent(),_2e8)){
return;
}
var x=nitobi.html.getEventCoords(_2e7).x;
if(nitobi.browser.IE){
this.surface.style.visibility="visible";
nitobi.drawing.align(this.surface,this.grid.element,nitobi.drawing.align.SAMEHEIGHT|nitobi.drawing.align.SAMEWIDTH|nitobi.drawing.align.ALIGNTOP|nitobi.drawing.align.ALIGNLEFT);
}
this.x=x;
nitobi.drawing.align(this.line,_2e6,nitobi.drawing.align.ALIGNTOP,0,0,nitobi.html.getHeight(_2e6)+1);
this.lineStyle.left=x+"px";
this.lineStyle.height=(parseInt(this.grid.Scroller.height)-parseInt(this.grid.getHeaderHeight()))+"px";
this.lineStyle.visibility="visible";
nitobi.ui.startDragOperation(this.line,_2e7,false,true,this,this.endResize);
};
nitobi.grid.ColumnResizer.prototype.endResize=function(_2ea){
var x=_2ea.x;
var Y=_2ea.y;
if(nitobi.browser.IE){
this.surface.style.visibility="hidden";
}
this.lineStyle.visibility="hidden";
this.lineStyle.top="0px";
this.lineStyle.left="0px";
var _2ed=this.column.getWidth();
var _2ee=parseInt(_2ed)+x-this.x;
if(isNaN(_2ee)){
return;
}
if(_2ee>10){
var _2ef=this.column.getWidth();
this.column.setWidth(_2ee);
this.grid.updateCellRanges();
this.grid.generateCss();
this.grid.adjustHorizontalScrollBars();
}
this.grid.Selection.collapse(this.grid.activeCell);
var _2f0=new nitobi.grid.OnAfterColumnResizeEventArgs(this.grid,this.column);
nitobi.event.evaluate(this.column.getOnAfterResizeEvent(),_2f0);
};
nitobi.grid.ColumnResizer.prototype.dispose=function(){
this.grid=null;
this.line=null;
this.lineStyle=null;
this.surface=null;
};
nitobi.grid.GridResizer=function(grid){
this.grid=grid;
this.widthFixed=false;
this.heightFixed=false;
this.minHeight=0;
this.minWidth=0;
this.box=document.getElementById("ebagridresizebox_");
if(this.box==null){
this.box=document.createElement("div");
this.box.id="ebagridresizebox_";
document.body.appendChild(this.box);
this.box.className="ntbcolumnresizeline";
}
};
nitobi.grid.GridResizer.prototype.startResize=function(grid,_2f3){
this.grid=grid;
var _2f4=null;
var x,y;
var _2f7=nitobi.html.getEventCoords(_2f3);
x=_2f7.x;
y=_2f7.y;
this.x=x;
this.y=y;
var w=grid.getWidth();
var h=grid.getHeight();
var L=grid.element.offsetLeft;
var T=grid.element.offsetTop;
this.resizeW=((Math.abs((x-L)-w)<3)||((Math.abs((y-T)-h)<16)&&(Math.abs((x-L)-w)<16)))&&!this.widthFixed;
this.resizeH=((Math.abs((y-T)-h)<3)||((Math.abs((y-T)-h)<16)&&(Math.abs((x-L)-w)<16)))&&!this.heightFixed;
if(this.resizeW||this.resizeH){
this.box.style.cursor=(this.resizeW&&this.resizeH)?"nw-resize":(this.resizeW)?"w-resize":"n-resize";
this.box.style.visibility="visible";
var _2fc=nitobi.drawing.align.SAMEWIDTH|nitobi.drawing.align.SAMEHEIGHT|nitobi.drawing.align.ALIGNTOP|nitobi.drawing.align.ALIGNLEFT;
nitobi.drawing.align(this.box,this.grid.element,_2fc,0,0,0,0,false);
this.dd=new nitobi.ui.DragDrop(this.box,false,false);
this.dd.onDragStop.subscribe(this.endResize,this);
this.dd.onMouseMove.subscribe(this.resize,this);
this.dd.startDrag(_2f3);
}
};
nitobi.grid.GridResizer.prototype.resize=function(){
var x=this.dd.x;
var y=this.dd.y;
var L=this.grid.element.offsetLeft;
var T=this.grid.element.offsetTop;
this.box.style.visibility="visible";
if(this.resizeW&&(x-L)>this.minWidth){
this.box.style.width=(x-L)+"px";
}
if(this.resizeH&&(y-T)>this.minHeight){
this.box.style.height=(y-T)+"px";
}
};
nitobi.grid.GridResizer.prototype.endResize=function(){
var x=this.dd.x;
var y=this.dd.y;
this.box.style.visibility="hidden";
var _303=this.grid.getWidth();
var _304=this.grid.getHeight();
this.newWidth=Math.max(parseInt(_303)+((this.resizeW)?x-this.x:0),this.minWidth);
this.newHeight=Math.max(parseInt(_304)+((this.resizeH)?y-this.y:0),this.minHeight);
if(isNaN(this.newWidth)||isNaN(this.newHeight)){
return;
}
if(this.newWidth>0&&this.newHeight>0){
this.grid.setWidth(this.newWidth);
this.grid.setHeight(this.newHeight);
this.grid.generateCss();
}
var _305=null;
this.grid.fire("AfterGridResize",{width:this.newWidth,height:this.newHeight});
};
nitobi.grid.GridResizer.prototype.dispose=function(){
this.grid=null;
};
nitobi.data.FormatConverter={};
nitobi.data.FormatConverter.convertHtmlTableToEbaXml=function(_306,_307,_308){
var s="<xsl:stylesheet version=\"1.0\" xmlns:ntb=\"http://www.nitobi.com\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:output encoding=\"UTF-8\" method=\"xml\" omit-xml-declaration=\"no\" />";
s+="<xsl:template match=\"//TABLE\"><ntb:data id=\"_default\">";
s+="<xsl:apply-templates /></ntb:data> </xsl:template>";
s+="<xsl:template match = \"//TR\">  <xsl:element name=\"ntb:e\"> <xsl:attribute name=\"xi\"><xsl:value-of select=\"position()-1+"+parseInt(_308)+"\"/></xsl:attribute>";
for(var _30a=0;_30a<_307.length;_30a++){
s+="<xsl:attribute name=\""+_307[_30a]+"\" ><xsl:value-of select=\"TD["+parseInt(_30a+1)+"]\"/></xsl:attribute>";
}
s+="</xsl:element></xsl:template>";
s+="</xsl:stylesheet>";
var _30b=nitobi.xml.createXmlDoc(_306);
var _30c=nitobi.xml.createXslProcessor(s);
var _30d=nitobi.xml.transformToXml(_30b,_30c);
return _30d;
};
nitobi.data.FormatConverter.convertTsvToEbaXml=function(tsv,_30f,_310){
var _311="<TABLE><TBODY>"+tsv.replace(/[\&\r]/g,"").replace(/([^\t\n]*)[\t]/g,"<TD>$1</TD>").replace(/([^\n]*?)\n/g,"<TR>$1</TR>").replace(/\>([^\<]*)\<\/TR/g,"><TD>$1</TD></TR")+"</TBODY></TABLE>";
if(_311.indexOf("<TBODY><TR>")==-1){
_311=_311.replace(/TBODY\>(.*)\<\/TBODY/,"TBODY><TR><TD>$1</TD></TR></TBODY");
}
return nitobi.data.FormatConverter.convertHtmlTableToEbaXml(_311,_30f,_310);
};
nitobi.data.FormatConverter.convertTsvToJs=function(tsv){
var _313="["+tsv.replace(/[\&\r]/g,"").replace(/([^\t\n]*)[\t]/g,"$1\",\"").replace(/([^\n]*?)\n/g,"[\"$1\"],")+"]";
return _313;
};
nitobi.data.FormatConverter.convertEbaXmlToHtmlTable=function(_314,_315,_316,_317){
var s="<xsl:stylesheet version=\"1.0\" xmlns:ntb=\"http://www.nitobi.com\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:output encoding=\"UTF-8\" method=\"html\" omit-xml-declaration=\"yes\" /><xsl:template match = \"*\"><xsl:apply-templates /></xsl:template><xsl:template match = \"/\">";
s+="<TABLE><TBODY><xsl:for-each select=\"//ntb:e[@xi>"+parseInt(_316-1)+" and @xi &lt; "+parseInt(_317+1)+"]\" ><TR>";
for(var _319=0;_319<_315.length;_319++){
s+="<TD><xsl:value-of select=\"@"+_315[_319]+"\" /></TD>";
}
s+="</TR></xsl:for-each></TBODY></TABLE></xsl:template></xsl:stylesheet>";
var _31a=nitobi.xml.createXslProcessor(s);
return nitobi.xml.transformToXml(_314,_31a).xml.replace(/xmlns:ntb="http:\/\/www.nitobi.com"/,"");
};
nitobi.data.FormatConverter.convertEbaXmlToTsv=function(_31b,_31c,_31d,_31e){
var s="<xsl:stylesheet version=\"1.0\" xmlns:ntb=\"http://www.nitobi.com\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:output encoding=\"UTF-8\" method=\"text\" omit-xml-declaration=\"yes\" /><xsl:template match = \"*\"><xsl:apply-templates /></xsl:template><xsl:template match = \"/\">";
s+="<xsl:for-each select=\"//ntb:e[@xi>"+parseInt(_31d-1)+" and @xi &lt; "+parseInt(_31e+1)+"]\" >\n";
for(var _320=0;_320<_31c.length;_320++){
s+="<xsl:value-of select=\"@"+_31c[_320]+"\" />";
if(_320<_31c.length-1){
s+="<xsl:text>&#x09;</xsl:text>";
}
}
s+="<xsl:text>&#xa;</xsl:text></xsl:for-each></xsl:template></xsl:stylesheet>";
var _321=nitobi.xml.createXslProcessor(s);
return nitobi.xml.transformToString(_31b,_321).replace(/xmlns:ntb="http:\/\/www.nitobi.com"/,"");
};
nitobi.data.FormatConverter.getDataColumns=function(data){
var _323=0;
if(data!=null&&data!=""){
if(data.substr(0,1)=="<"){
_323=data.toLowerCase().substr(0,data.toLowerCase().indexOf("</tr>")).split("</td>").length-1;
}else{
_323=data.substr(0,data.indexOf("\n")).split("\t").length;
}
}else{
_323=0;
}
return _323;
};
nitobi.data.FormatConverter.getDataRows=function(data){
var _325=0;
if(data!=null&&data!=""){
if(data.substr(0,1)=="<"){
_325=data.toLowerCase().split("</tr>").length-1;
}else{
retValArray=data.split("\n");
_325=retValArray.length;
if(retValArray[retValArray.length-1]==""){
_325--;
}
}
}else{
_325=0;
}
return _325;
};
nitobi.grid.DateColumn=function(grid,_327){
nitobi.grid.DateColumn.baseConstructor.call(this,grid,_327);
this.Interface=grid.API.selectSingleNode("interfaces/interface[@name='EBADateColumn']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
};
nitobi.lang.extend(nitobi.grid.DateColumn,nitobi.grid.Column);
nitobi.lang.defineNs("nitobi.grid.Declaration");
nitobi.grid.Declaration.parse=function(_328){
var _329={};
_329.grid=nitobi.xml.parseHtml(_328);
var _32a=_328.firstChild;
while(_32a!=null){
if(typeof (_32a.tagName)!="undefined"){
var tag=_32a.tagName.replace(/ntb\:/gi,"").toLowerCase();
if(tag=="inlinehtml"){
_329[tag]=_32a;
}else{
var _32c="http://www.nitobi.com";
if(tag=="columndefinition"){
var sXml;
if(nitobi.browser.IE){
sXml=("<"+nitobi.xml.nsPrefix+"grid xmlns:ntb=\""+_32c+"\"><"+nitobi.xml.nsPrefix+"columns>"+_32a.parentNode.innerHTML.substring(31).replace(/\=\s*([^\"^\s^\>]+)/g,"=\"$1\" ")+"</"+nitobi.xml.nsPrefix+"columns></"+nitobi.xml.nsPrefix+"grid>");
}else{
sXml="<"+nitobi.xml.nsPrefix+"grid xmlns:ntb=\""+_32c+"\"><"+nitobi.xml.nsPrefix+"columns>"+_32a.parentNode.innerHTML.replace(/\=\s*([^\"^\s^\>]+)/g,"=\"$1\" ")+"</"+nitobi.xml.nsPrefix+"columns></"+nitobi.xml.nsPrefix+"grid>";
}
sXml=sXml.replace(/\&nbsp\;/gi," ");
_329["columndefinitions"]=nitobi.xml.createXmlDoc();
_329["columndefinitions"].validateOnParse=false;
_329["columndefinitions"]=nitobi.xml.loadXml(_329["columndefinitions"],sXml);
break;
}else{
_329[tag]=nitobi.xml.parseHtml(_32a);
}
}
}
_32a=_32a.nextSibling;
}
return _329;
};
nitobi.grid.Declaration.loadDataSources=function(_32e,grid){
var _330=new Array();
if(_32e["datasources"]){
_330=_32e.datasources.selectNodes("//"+nitobi.xml.nsPrefix+"datasources/*");
}
if(_330.length>0){
for(var i=0;i<_330.length;i++){
var id=_330[i].getAttribute("id");
if(id!="_default"){
var _333=_330[i].xml.replace(/fieldnames=/g,"FieldNames=").replace(/keys=/g,"Keys=");
_333="<ntb:grid xmlns:ntb=\"http://www.nitobi.com\"><ntb:datasources>"+_333+"</ntb:datasources></ntb:grid>";
var _334=new nitobi.data.DataTable("local",grid.getPagingMode()!=nitobi.grid.PAGINGMODE_NONE,{GridId:grid.getID()},{GridId:grid.getID()},grid.isAutoKeyEnabled());
_334.initialize(id,_333);
_334.initializeXml(_333);
grid.data.add(_334);
var _335=grid.model.selectNodes("//nitobi.grid.Column[@DatasourceId='"+id+"']");
for(var j=0;j<_335.length;j++){
grid.editorDataReady(_335[j]);
}
}
}
}
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.EditCompleteEventArgs=function(obj,_338,_339,cell){
this.editor=obj;
this.cell=cell;
this.databaseValue=_339;
this.displayValue=_338;
this.status="ok";
};
nitobi.grid.EditCompleteEventArgs.prototype.dispose=function(){
this.editor=null;
this.cell=null;
this.metadata=null;
};
nitobi.data.GetCompleteEventArgs=function(_33b,_33c,_33d,_33e,_33f,_340,obj,_342,_343){
this.firstRow=_33b;
this.lastRow=_33c;
this.callback=_342;
this.dataSource=_340;
this.context=obj;
this.ajaxCallback=_33f;
this.startXi=_33d;
this.pageSize=_33e;
this.lastPage=false;
this.numRowsReturned=_343;
this.lastRowReturned=_33c;
};
nitobi.data.GetCompleteEventArgs.prototype.dispose=function(){
this.callback=null;
this.context=null;
this.dataSource=null;
this.ajaxCallback.clear();
this.ajaxCallback==null;
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.MODE_STANDARDPAGING="standard";
nitobi.grid.MODE_LOCALSTANDARDPAGING="localstandard";
nitobi.grid.MODE_LIVESCROLLING="livescrolling";
nitobi.grid.MODE_LOCALLIVESCROLLING="locallivescrolling";
nitobi.grid.MODE_NONPAGING="nonpaging";
nitobi.grid.MODE_LOCALNONPAGING="localnonpaging";
nitobi.grid.MODE_SMARTPAGING="smartpaging";
nitobi.grid.MODE_PAGEDLIVESCROLLING="pagedlivescrolling";
nitobi.grid.RENDERMODE_ONDEMAND="ondemand";
nitobi.lang.defineNs("nitobi.GridFactory");
nitobi.GridFactory.createGrid=function(_344,_345,_346){
var _347="";
var _348="";
var _349="";
_346=nitobi.html.getElement(_346);
if(_346!=null){
xDeclaration=nitobi.grid.Declaration.parse(_346);
_344=xDeclaration.grid.documentElement.getAttribute("mode");
var _34a=nitobi.GridFactory.isGetHandler(xDeclaration);
var _34b=nitobi.GridFactory.isDatasourceId(xDeclaration);
var _34c=false;
if(_344==nitobi.grid.MODE_LOCALLIVESCROLLING){
_347=nitobi.grid.PAGINGMODE_LIVESCROLLING;
_348=nitobi.data.DATAMODE_LOCAL;
}else{
if(_344==nitobi.grid.MODE_LIVESCROLLING){
_347=nitobi.grid.PAGINGMODE_LIVESCROLLING;
_348=nitobi.data.DATAMODE_CACHING;
}else{
if(_344==nitobi.grid.MODE_NONPAGING){
_34c=true;
_347=nitobi.grid.PAGINGMODE_NONE;
_348=nitobi.data.DATAMODE_LOCAL;
}else{
if(_344==nitobi.grid.MODE_LOCALNONPAGING){
_347=nitobi.grid.PAGINGMODE_NONE;
_348=nitobi.data.DATAMODE_LOCAL;
}else{
if(_344==nitobi.grid.MODE_LOCALSTANDARDPAGING){
_347=nitobi.grid.PAGINGMODE_STANDARD;
_348=nitobi.data.DATAMODE_LOCAL;
}else{
if(_344==nitobi.grid.MODE_STANDARDPAGING){
_347=nitobi.grid.PAGINGMODE_STANDARD;
_348=nitobi.data.DATAMODE_PAGING;
}else{
if(_344==nitobi.grid.MODE_PAGEDLIVESCROLLING){
_347=nitobi.grid.PAGINGMODE_STANDARD;
_348=nitobi.data.DATAMODE_PAGING;
_349=nitobi.grid.RENDERMODE_ONDEMAND;
}else{
}
}
}
}
}
}
}
}
var id=_346.getAttribute("id");
_344=(_344||nitobi.grid.MODE_STANDARDPAGING).toLowerCase();
var grid=null;
if(_344==nitobi.grid.MODE_LOCALSTANDARDPAGING){
grid=new nitobi.grid.GridLocalPage(id);
}else{
if(_344==nitobi.grid.MODE_LIVESCROLLING){
grid=new nitobi.grid.GridLiveScrolling(id);
}else{
if(_344==nitobi.grid.MODE_LOCALLIVESCROLLING){
grid=new nitobi.grid.GridLiveScrolling(id);
}else{
if(_344==nitobi.grid.MODE_NONPAGING||_344==nitobi.grid.MODE_LOCALNONPAGING){
grid=new nitobi.grid.GridNonpaging(id);
}else{
if(_344==nitobi.grid.MODE_STANDARDPAGING||_344==nitobi.grid.MODE_PAGEDLIVESCROLLING){
grid=new nitobi.grid.GridStandard(id);
}
}
}
}
}
grid.setPagingMode(_347);
grid.setDataMode(_348);
grid.setRenderMode(_349);
nitobi.GridFactory.processDeclaration(grid,_346,xDeclaration);
_346.jsObject=grid;
return grid;
};
nitobi.GridFactory.processDeclaration=function(grid,_350,_351){
if(_351!=null){
grid.setDeclaration(_351);
if(typeof (_351.inlinehtml)=="undefined"){
var _352=document.createElement("ntb:inlinehtml");
_352.setAttribute("parentid","grid"+grid.uid);
_350.insertAdjacentElement("beforeEnd",_352);
grid.Declaration.inlinehtml=_352;
}
if(this.data==null||this.data.tables==null||this.data.tables.length==0){
var _353=new nitobi.data.DataSet();
_353.initialize();
grid.connectToDataSet(_353);
}
grid.initializeModelFromDeclaration();
var _354=grid.Declaration.columndefinitions||grid.Declaration.columns;
if(typeof (_354)!="undefined"&&_354!=null&&_354.childNodes.length!=0&&_354.childNodes[0].childNodes.length!=0){
grid.defineColumns(_354.documentElement);
}
nitobi.grid.Declaration.loadDataSources(_351,grid);
grid.attachToParentDomElement(grid.Declaration.inlinehtml);
var _355=grid.getDataMode();
var _356=grid.getDatasourceId();
var _357=grid.getGetHandler();
if(_356!=null&&_356!=""){
grid.connectToTable(grid.data.getTable(_356));
}else{
grid.ensureConnected();
if(grid.mode.toLowerCase()==nitobi.grid.MODE_LIVESCROLLING&&_351!=null&&_351.datasources!=null){
var _358=_351.datasources.selectNodes("//ntb:datasource[@id='_default']/ntb:data/ntb:e").length;
if(_358>0){
var _359=grid.data.getTable("_default");
_359.initializeXmlData(_351.grid.xml);
_359.initializeXml(_351.grid.xml);
_359.descriptor.leap(0,_358*2);
_359.syncRowCount();
}
}
}
window.setTimeout(function(){
grid.bind();
},0);
}
};
nitobi.GridFactory.isLocal=function(_35a){
var _35b=_35a.grid.documentElement.getAttribute("datasourceid");
var _35c=_35a.grid.documentElement.getAttribute("gethandler");
if(_35c!=null&&_35c!=""){
return false;
}else{
if(_35b!=null&&_35b!=""){
return true;
}else{
throw ("Non-paging grid requires either a gethandler or a local datasourceid to be specified.");
}
}
};
nitobi.GridFactory.isGetHandler=function(_35d){
var _35e=_35d.grid.documentElement.getAttribute("gethandler");
if(_35e!=null&&_35e!=""){
return true;
}
return false;
};
nitobi.GridFactory.isDatasourceId=function(_35f){
var _360=_35f.grid.documentElement.getAttribute("datasourceid");
if(_360!=null&&_360!=""){
return true;
}
return false;
};
nitobi.grid.hover=function(_361,_362,_363){
if(!_363){
return;
}
var id=_361.getAttribute("id");
var _365=id.replace(/__/g,"||");
var _366=_365.split("_");
var row=_366[3];
var uid=_366[5].replace(/\|\|/g,"__");
var _369=document.getElementById("cell_"+row+"_0_"+uid);
var _36a=_369.parentNode;
var _36b=_36a.childNodes[_36a.childNodes.length-1];
var id=_36b.getAttribute("id");
var _366=id.split("_");
var _36c=document.getElementById("cell_"+row+"_"+(Number(_366[4])+1)+"_"+uid);
var _36d=null;
if(_36c!=null){
_36d=_36c.parentNode;
}
if(_362){
var _36e=nitobi.grid.RowHoverColor||"white";
_36a.style.backgroundColor=_36e;
if(_36d){
_36d.style.backgroundColor=_36e;
}
}else{
_36a.style.backgroundColor="";
if(_36d){
_36d.style.backgroundColor="";
}
}
if(_362){
nitobi.html.addClass(_361,"ntbcellhover");
}else{
nitobi.html.removeClass(_361,"ntbcellhover");
}
};
initEBAGrids=function(){
nitobi.initComponents();
};
nitobi.initGrids=function(){
var _36f=[];
var _370=window.document.getElementsByTagName(nitobi.browser.MOZ?"ntb:grid":"grid");
for(var i=0;i<_370.length;i++){
_370[i].jsObject=nitobi.GridFactory.createGrid(null,null,_370[i]);
_36f.push(_370[i].jsObject);
}
return _36f;
};
nitobi.initGrid=function(id){
var grid=nitobi.html.getElement(id);
if(grid!=null){
grid.jsObject=nitobi.GridFactory.createGrid(null,null,grid);
}
return grid.jsObject;
};
nitobi.initComponents=function(){
nitobi.initGrids();
};
nitobi.getGrid=function(_374){
return document.getElementById(_374).jsObject;
};
nitobi.base.Registry.getInstance().register(new nitobi.base.Profile("nitobi.initGrid",null,false,"ntb:grid"));
nitobi.grid.GridLiveScrolling=function(uid){
nitobi.grid.GridLiveScrolling.baseConstructor.call(this,uid);
this.mode="livescrolling";
this.setPagingMode(nitobi.grid.PAGINGMODE_LIVESCROLLING);
this.setDataMode(nitobi.data.DATAMODE_CACHING);
};
nitobi.lang.extend(nitobi.grid.GridLiveScrolling,nitobi.grid.Grid);
nitobi.grid.GridLiveScrolling.prototype.createChildren=function(){
var args=arguments;
nitobi.grid.GridLiveScrolling.base.createChildren.call(this,args);
nitobi.grid.GridLiveScrolling.base.createToolbars.call(this,nitobi.ui.Toolbars.VisibleToolbars.STANDARD);
};
nitobi.grid.GridLiveScrolling.prototype.bind=function(){
nitobi.grid.GridStandard.base.bind.call(this);
if(this.getGetHandler()!=""){
this.ensureConnected();
var rows=this.getRowsPerPage();
if(this.datatable.mode=="local"){
rows=null;
}
this.datatable.get(0,rows,this,this.getComplete);
}else{
this.finalizeRowCount(this.datatable.getRemoteRowCount());
this.bindComplete();
}
};
nitobi.grid.GridLiveScrolling.prototype.getComplete=function(_378){
nitobi.grid.GridLiveScrolling.base.getComplete.call(this,_378);
if(!this.columnsDefined){
this.defineColumnsFinalize();
}
this.bindComplete();
};
nitobi.grid.GridLiveScrolling.prototype.pageSelect=function(dir){
var _37a=this.Scroller.getUnrenderedBlocks();
var rows=_37a.last-_37a.first;
this.reselect(0,rows*dir);
};
nitobi.grid.GridLiveScrolling.prototype.page=function(dir){
var _37d=this.Scroller.getUnrenderedBlocks();
var rows=_37d.last-_37d.first;
this.move(0,rows*dir);
};
nitobi.grid.LoadingScreen=function(grid){
this.loadingScreen=null;
this.grid=grid;
this.loadingImg=null;
};
nitobi.grid.LoadingScreen.prototype.initialize=function(){
this.loadingScreen=document.createElement("div");
var _380=this.findCssUrl();
var msg="";
if(_380==null){
msg="Loading...";
}else{
msg="<img src='"+_380+"loading.gif'  class='ntbloadingIcon' valign='absmiddle'></img>";
}
this.loadingScreen.innerHTML="<table style='padding:0px;margin:0px;' border='0' width='100%' height='100%'><tr style='padding:0px;margin:0px;'><td style='padding:0px;margin:0px;text-align:center;font:verdana;font-size:10pt;'>"+msg+"</td></tr></table>";
this.loadingScreen.className="ntbloading";
var lss=this.loadingScreen.style;
lss.verticalAlign="middle";
lss.visibility="hidden";
lss.position="absolute";
lss.top="0px";
lss.left="0px";
};
nitobi.grid.LoadingScreen.prototype.attachToElement=function(_383){
_383.appendChild(this.loadingScreen);
};
nitobi.grid.LoadingScreen.prototype.findCssUrl=function(){
var _384=nitobi.html.findParentStylesheet(".ntbloadingIcon");
if(_384==null){
return null;
}
var _385=nitobi.html.normalizeUrl(_384.href);
if(nitobi.browser.IE){
while(_384.parentStyleSheet){
_384=_384.parentStyleSheet;
_385=nitobi.html.normalizeUrl(_384.href)+_385;
}
}
return _385;
};
nitobi.grid.LoadingScreen.prototype.show=function(){
try{
this.resize();
this.loadingScreen.style.visibility="visible";
this.loadingScreen.style.display="block";
}
catch(e){
}
};
nitobi.grid.LoadingScreen.prototype.resize=function(){
this.loadingScreen.style.width=this.grid.getWidth()+"px";
this.loadingScreen.style.height=this.grid.getHeight()+"px";
};
nitobi.grid.LoadingScreen.prototype.hide=function(){
this.loadingScreen.style.display="none";
};
nitobi.grid.GridLocalPage=function(uid){
nitobi.grid.GridLocalPage.baseConstructor.call(this,uid);
this.mode="localpaging";
this.setPagingMode(nitobi.grid.PAGINGMODE_STANDARD);
this.setDataMode("local");
};
nitobi.lang.extend(nitobi.grid.GridLocalPage,nitobi.grid.Grid);
nitobi.grid.GridLocalPage.prototype.createChildren=function(){
var args=arguments;
nitobi.grid.GridLocalPage.base.createChildren.call(this,args);
nitobi.grid.GridLiveScrolling.base.createToolbars.call(this,nitobi.ui.Toolbars.VisibleToolbars.STANDARD|nitobi.ui.Toolbars.VisibleToolbars.PAGING);
this.toolbars.subscribe("NextPage",nitobi.lang.close(this,this.pageNext));
this.toolbars.subscribe("PreviousPage",nitobi.lang.close(this,this.pagePrevious));
this.subscribe("EndOfData",function(pct){
this.toolbars.pagingToolbar.getUiElements()["nextPage"+this.toolbars.uid].disable();
});
this.subscribe("TopOfData",function(pct){
this.toolbars.pagingToolbar.getUiElements()["previousPage"+this.toolbars.uid].disable();
});
this.subscribe("NotTopOfData",function(pct){
this.toolbars.pagingToolbar.getUiElements()["previousPage"+this.toolbars.uid].enable();
});
this.subscribe("NotEndOfData",function(pct){
this.toolbars.pagingToolbar.getUiElements()["nextPage"+this.toolbars.uid].enable();
});
};
nitobi.grid.GridLocalPage.prototype.pagePrevious=function(){
this.fire("BeforeLoadPreviousPage");
this.loadDataPage(Math.max(this.getCurrentPageIndex()-1,0));
this.fire("AfterLoadPreviousPage");
};
nitobi.grid.GridLocalPage.prototype.pageNext=function(){
this.fire("BeforeLoadNextPage");
this.loadDataPage(this.getCurrentPageIndex()+1);
this.fire("AfterLoadNextPage");
};
nitobi.grid.GridLocalPage.prototype.loadDataPage=function(_38c){
this.fire("BeforeLoadDataPage");
if(_38c>-1){
this.setCurrentPageIndex(_38c);
this.setDisplayedRowCount(this.getRowsPerPage());
var _38d=this.getCurrentPageIndex()*this.getRowsPerPage();
var rows=this.getRowsPerPage()-this.getfreezetop()-this.getfreezebottom();
this.setDisplayedRowCount(rows);
var _38f=_38d+rows;
if(_38f>=this.getRowCount()){
this.fire("EndOfData");
}else{
this.fire("NotEndOfData");
}
if(_38d==0){
this.fire("TopOfData");
}else{
this.fire("NotTopOfData");
}
this.clearSurfaces();
this.updateCellRanges();
this.scrollVertical(0);
}
this.fire("AfterLoadDataPage");
};
nitobi.grid.GridLocalPage.prototype.setRowsPerPage=function(rows){
this.setDisplayedRowCount(this.getRowsPerPage());
this.data.table.pageSize=this.getRowsPerPage();
};
nitobi.grid.GridLocalPage.prototype.pageStartIndexChanges=function(){
};
nitobi.grid.GridLocalPage.prototype.hitFirstPage=function(){
this.fire("FirstPage");
};
nitobi.grid.GridLocalPage.prototype.hitLastPage=function(){
this.fire("LastPage");
};
nitobi.grid.GridLocalPage.prototype.bind=function(){
nitobi.grid.GridLocalPage.base.bind.call(this);
this.finalizeRowCount(this.datatable.getRemoteRowCount());
this.bindComplete();
};
nitobi.grid.GridLocalPage.prototype.pageUpKey=function(){
this.pagePrevious();
};
nitobi.grid.GridLocalPage.prototype.pageDownKey=function(){
this.pageNext();
};
nitobi.grid.GridLocalPage.prototype.renderMiddle=function(){
nitobi.grid.GridLocalPage.base.renderMiddle.call(this,arguments);
var _391=this.getfreezetop();
endRow=this.getRowsPerPage()-1;
this.Scroller.view.midcenter.renderGap(_391,endRow,false);
};
nitobi.grid.GridNonpaging=function(uid){
nitobi.grid.GridNonpaging.baseConstructor.call(this);
this.mode="nonpaging";
this.setPagingMode(nitobi.grid.PAGINGMODE_NONE);
this.setDataMode(nitobi.data.DATAMODE_LOCAL);
};
nitobi.lang.extend(nitobi.grid.GridNonpaging,nitobi.grid.Grid);
nitobi.grid.GridNonpaging.prototype.createChildren=function(){
var args=arguments;
nitobi.grid.GridNonpaging.base.createChildren.call(this,args);
nitobi.grid.GridNonpaging.base.createToolbars.call(this,nitobi.ui.Toolbars.VisibleToolbars.STANDARD);
};
nitobi.grid.GridNonpaging.prototype.bind=function(){
nitobi.grid.GridStandard.base.bind.call(this);
if(this.getGetHandler()!=""){
this.ensureConnected();
this.datatable.get(0,null,this,this.getComplete);
}else{
this.finalizeRowCount(this.datatable.getRemoteRowCount());
this.bindComplete();
}
};
nitobi.grid.GridNonpaging.prototype.getComplete=function(_394){
nitobi.grid.GridNonpaging.base.getComplete.call(this,_394);
this.finalizeRowCount(_394.numRowsReturned);
this.defineColumnsFinalize();
this.bindComplete();
};
nitobi.grid.GridNonpaging.prototype.renderMiddle=function(){
nitobi.grid.GridNonpaging.base.renderMiddle.call(this,arguments);
var _395=this.getfreezetop();
endRow=this.getRowCount();
this.Scroller.view.midcenter.renderGap(_395,endRow,false);
};
nitobi.grid.GridStandard=function(uid){
nitobi.grid.GridStandard.baseConstructor.call(this,uid);
this.mode="standard";
this.setPagingMode(nitobi.grid.PAGINGMODE_STANDARD);
this.setDataMode(nitobi.data.DATAMODE_PAGING);
};
nitobi.lang.extend(nitobi.grid.GridStandard,nitobi.grid.Grid);
nitobi.grid.GridStandard.prototype.createChildren=function(){
var args=arguments;
nitobi.grid.GridStandard.base.createChildren.call(this,args);
nitobi.grid.GridStandard.base.createToolbars.call(this,nitobi.ui.Toolbars.VisibleToolbars.STANDARD|nitobi.ui.Toolbars.VisibleToolbars.PAGING);
this.toolbars.subscribe("NextPage",nitobi.lang.close(this,this.pageNext));
this.toolbars.subscribe("PreviousPage",nitobi.lang.close(this,this.pagePrevious));
this.subscribe("EndOfData",this.disableNextPage);
this.subscribe("TopOfData",this.disablePreviousPage);
this.subscribe("NotTopOfData",this.enablePreviousPage);
this.subscribe("NotEndOfData",this.enableNextPage);
this.subscribe("TableConnected",nitobi.lang.close(this,this.subscribeToRowCountReady));
};
nitobi.grid.GridStandard.prototype.connectToTable=function(_398){
if(nitobi.grid.GridStandard.base.connectToTable.call(this,_398)!=false){
this.datatable.subscribe("RowInserted",nitobi.lang.close(this,this.incrementDisplayedRowCount));
this.datatable.subscribe("RowDeleted",nitobi.lang.close(this,this.decrementDisplayedRowCount));
}
};
nitobi.grid.GridStandard.prototype.subscribeToRowCountReady=function(){
};
nitobi.grid.GridStandard.prototype.updateDisplayedRowCount=function(_399){
this.setDisplayedRowCount(_399.numRowsReturned);
};
nitobi.grid.GridStandard.prototype.disableNextPage=function(){
this.disableButton("nextPage");
};
nitobi.grid.GridStandard.prototype.disablePreviousPage=function(){
this.disableButton("previousPage");
};
nitobi.grid.GridStandard.prototype.disableButton=function(_39a){
var t=this.getToolbars().pagingToolbar;
if(t!=null){
t.getUiElements()[_39a+this.toolbars.uid].disable();
}
};
nitobi.grid.GridStandard.prototype.enableNextPage=function(){
this.enableButton("nextPage");
};
nitobi.grid.GridStandard.prototype.enablePreviousPage=function(){
this.enableButton("previousPage");
};
nitobi.grid.GridStandard.prototype.enableButton=function(_39c){
var t=this.getToolbars().pagingToolbar;
if(t!=null){
t.getUiElements()[_39c+this.toolbars.uid].enable();
}
};
nitobi.grid.GridStandard.prototype.pagePrevious=function(){
this.fire("BeforeLoadPreviousPage");
this.loadDataPage(Math.max(this.getCurrentPageIndex()-1,0));
this.fire("AfterLoadPreviousPage");
};
nitobi.grid.GridStandard.prototype.pageNext=function(){
this.fire("BeforeLoadNextPage");
this.loadDataPage(this.getCurrentPageIndex()+1);
this.fire("AfterLoadNextPage");
};
nitobi.grid.GridStandard.prototype.loadDataPage=function(_39e){
this.fire("BeforeLoadDataPage");
if(_39e>-1){
if(this.sortColumn){
if(this.datatable.sortColumn){
for(var i=0;i<this.getColumnCount();i++){
var _3a0=this.getColumnObject(i);
if(_3a0.getColumnName()==this.datatable.sortColumn){
this.setSortStyle(i,this.datatable.sortDir);
break;
}
}
}else{
this.setSortStyle(this.sortColumn.column,"",true);
}
}
this.setCurrentPageIndex(_39e);
var _3a1=this.getCurrentPageIndex()*this.getRowsPerPage();
var rows=this.getRowsPerPage()-this.getfreezetop()-this.getfreezebottom();
this.datatable.flush();
this.datatable.get(_3a1,rows,this,this.afterLoadDataPage);
}
this.fire("AfterLoadDataPage");
};
nitobi.grid.GridStandard.prototype.afterLoadDataPage=function(_3a3){
this.setDisplayedRowCount(_3a3.numRowsReturned);
this.setRowCount(_3a3.numRowsReturned);
if(_3a3.numRowsReturned!=this.getRowsPerPage()){
this.fire("EndOfData");
}else{
this.fire("NotEndOfData");
}
if(this.getCurrentPageIndex()==0){
this.fire("TopOfData");
}else{
this.fire("NotTopOfData");
}
this.clearSurfaces();
this.updateCellRanges();
this.scrollVertical(0);
};
nitobi.grid.GridStandard.prototype.bind=function(){
nitobi.grid.GridStandard.base.bind.call(this);
this.setCurrentPageIndex(0);
this.disablePreviousPage();
this.enableNextPage();
this.ensureConnected();
this.datatable.get(0,this.getRowsPerPage(),this,this.getComplete);
};
nitobi.grid.GridStandard.prototype.getComplete=function(_3a4){
this.afterLoadDataPage(_3a4);
nitobi.grid.GridStandard.base.getComplete.call(this,_3a4);
this.defineColumnsFinalize();
this.bindComplete();
};
nitobi.grid.GridStandard.prototype.renderMiddle=function(){
nitobi.grid.GridStandard.base.renderMiddle.call(this,arguments);
var _3a5=this.getfreezetop();
endRow=this.getRowsPerPage()-1;
this.Scroller.view.midcenter.renderGap(_3a5,endRow,false);
};
nitobi.grid.NumberColumn=function(grid,_3a7){
nitobi.grid.NumberColumn.baseConstructor.call(this,grid,_3a7);
this.Interface=grid.API.selectSingleNode("interfaces/interface[@name='EBANumberColumn']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
};
nitobi.lang.extend(nitobi.grid.NumberColumn,nitobi.grid.Column);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCopyEventArgs=function(_3a8,data,_3aa){
nitobi.grid.OnCopyEventArgs.baseConstructor.apply(this,arguments);
};
nitobi.lang.extend(nitobi.grid.OnCopyEventArgs,nitobi.grid.SelectionEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnPasteEventArgs=function(_3ab,data,_3ad){
nitobi.grid.OnPasteEventArgs.baseConstructor.apply(this,arguments);
};
nitobi.lang.extend(nitobi.grid.OnPasteEventArgs,nitobi.grid.SelectionEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnAfterCellEditEventArgs=function(_3ae,cell){
nitobi.grid.OnAfterCellEditEventArgs.baseConstructor.call(this,_3ae,cell);
};
nitobi.lang.extend(nitobi.grid.OnAfterCellEditEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnAfterColumnResizeEventArgs=function(_3b0,_3b1){
nitobi.grid.OnAfterColumnResizeEventArgs.baseConstructor.call(this,_3b0,_3b1);
};
nitobi.lang.extend(nitobi.grid.OnAfterColumnResizeEventArgs,nitobi.grid.ColumnEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnAfterRowDeleteEventArgs=function(_3b2,row){
nitobi.grid.OnAfterRowDeleteEventArgs.baseConstructor.call(this,_3b2,row);
};
nitobi.lang.extend(nitobi.grid.OnAfterRowDeleteEventArgs,nitobi.grid.RowEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnAfterRowInsertEventArgs=function(_3b4,row){
nitobi.grid.OnAfterRowInsertEventArgs.baseConstructor.call(this,_3b4,row);
};
nitobi.lang.extend(nitobi.grid.OnAfterRowInsertEventArgs,nitobi.grid.RowEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnAfterSortEventArgs=function(_3b6,_3b7,_3b8){
nitobi.grid.OnAfterSortEventArgs.baseConstructor.call(this,_3b6,_3b7);
this.direction=_3b8;
};
nitobi.lang.extend(nitobi.grid.OnAfterSortEventArgs,nitobi.grid.ColumnEventArgs);
nitobi.grid.OnAfterSortEventArgs.prototype.getDirection=function(){
return this.direction;
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeCellEditEventArgs=function(_3b9,cell){
nitobi.grid.OnBeforeCellEditEventArgs.baseConstructor.call(this,_3b9,cell);
};
nitobi.lang.extend(nitobi.grid.OnBeforeCellEditEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeColumnResizeEventArgs=function(_3bb,_3bc){
nitobi.grid.OnBeforeColumnResizeEventArgs.baseConstructor.call(this,_3bb,_3bc);
};
nitobi.lang.extend(nitobi.grid.OnBeforeColumnResizeEventArgs,nitobi.grid.ColumnEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeRowDeleteEventArgs=function(_3bd,row){
nitobi.grid.OnBeforeRowDeleteEventArgs.baseConstructor.call(this,_3bd,row);
};
nitobi.lang.extend(nitobi.grid.OnBeforeRowDeleteEventArgs,nitobi.grid.RowEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeRowInsertEventArgs=function(_3bf,row){
nitobi.grid.OnBeforeRowInsertEventArgs.baseConstructor.call(this,_3bf,row);
};
nitobi.lang.extend(nitobi.grid.OnBeforeRowInsertEventArgs,nitobi.grid.RowEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeSortEventArgs=function(_3c1,_3c2,_3c3){
nitobi.grid.OnBeforeSortEventArgs.baseConstructor.call(this,_3c1,_3c2);
this.direction=_3c3;
};
nitobi.lang.extend(nitobi.grid.OnBeforeSortEventArgs,nitobi.grid.ColumnEventArgs);
nitobi.grid.OnBeforeSortEventArgs.prototype.getDirection=function(){
return this.direction;
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnBeforeCellClickEventArgs=function(_3c4,cell){
nitobi.grid.OnBeforeCellClickEventArgs.baseConstructor.call(this,_3c4,cell);
};
nitobi.lang.extend(nitobi.grid.OnBeforeCellClickEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCellBlurEventArgs=function(_3c6,cell){
nitobi.grid.OnCellBlurEventArgs.baseConstructor.call(this,_3c6,cell);
};
nitobi.lang.extend(nitobi.grid.OnCellBlurEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCellClickEventArgs=function(_3c8,cell){
nitobi.grid.OnCellClickEventArgs.baseConstructor.call(this,_3c8,cell);
};
nitobi.lang.extend(nitobi.grid.OnCellClickEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCellDblClickEventArgs=function(_3ca,cell){
nitobi.grid.OnCellDblClickEventArgs.baseConstructor.call(this,_3ca,cell);
};
nitobi.lang.extend(nitobi.grid.OnCellDblClickEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCellFocusEventArgs=function(_3cc,cell){
nitobi.grid.OnCellFocusEventArgs.baseConstructor.call(this,_3cc,cell);
};
nitobi.lang.extend(nitobi.grid.OnCellFocusEventArgs,nitobi.grid.CellEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnCellValidateEventArgs=function(_3ce,cell,_3d0,_3d1){
nitobi.grid.OnCellValidateEventArgs.baseConstructor.call(this,_3ce,cell);
this.oldValue=_3d1;
this.newValue=_3d0;
};
nitobi.lang.extend(nitobi.grid.OnCellValidateEventArgs,nitobi.grid.CellEventArgs);
nitobi.grid.OnCellValidateEventArgs.prototype.getOldValue=function(){
return this.oldValue;
};
nitobi.grid.OnCellValidateEventArgs.prototype.getNewValue=function(){
return this.newValue;
};
nitobi.grid.OnContextMenuEventArgs=function(){
};
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnHeaderClickEventArgs=function(_3d2,_3d3){
nitobi.grid.OnHeaderClickEventArgs.baseConstructor.call(this,_3d2,_3d3);
};
nitobi.lang.extend(nitobi.grid.OnHeaderClickEventArgs,nitobi.grid.ColumnEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnRowBlurEventArgs=function(_3d4,row){
nitobi.grid.OnRowBlurEventArgs.baseConstructor.call(this,_3d4,row);
};
nitobi.lang.extend(nitobi.grid.OnRowBlurEventArgs,nitobi.grid.RowEventArgs);
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.OnRowFocusEventArgs=function(_3d6,row){
nitobi.grid.OnRowFocusEventArgs.baseConstructor.call(this,_3d6,row);
};
nitobi.lang.extend(nitobi.grid.OnRowFocusEventArgs,nitobi.grid.RowEventArgs);
nitobi.grid.Row=function(grid,row){
this.grid=grid;
this.row=row;
this.Row=row;
this.Interface=this.grid.API.selectSingleNode("interfaces/interface[@name='nitobi.grid.Row']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
this.DomNode=nitobi.grid.Row.getRowElement(grid,row);
this.DataNode=this.grid.datatable.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e[@xi="+row+"]");
};
nitobi.grid.Row.prototype.getStyle=function(){
return this.DomNode.style;
};
nitobi.grid.Row.prototype.getCell=function(_3da){
return this.grid.getCellObject(this.row,_3da);
};
nitobi.grid.Row.prototype.getKey=function(_3db){
return this.grid.getCellObject(this.row,_3db);
};
nitobi.grid.Row.getRowElement=function(grid,row){
return nitobi.grid.Row.getRowElements(grid,row).mid;
};
nitobi.grid.Row.getRowElements=function(grid,row){
var _3e0=grid.getFrozenLeftColumnCount();
if(!_3e0){
return {left:null,mid:$("row_"+row+"_"+grid.uid)};
}
var rows={};
rows.left=nitobi.grid.Cell.getCellElement(grid,row,0).parentNode;
var cell=nitobi.grid.Cell.getCellElement(grid,row,_3e0);
rows.mid=cell?cell.parentNode:null;
return rows;
};
nitobi.grid.Row.getRowNumber=function(_3e3){
return parseInt(_3e3.getAttribute("xi"));
};
nitobi.grid.Row.prototype.xGETMETA=function(){
var node=this.MetaNode;
node=node.selectSingleNode("@"+arguments[0]);
if(node!=null){
return node.value;
}
};
nitobi.grid.Row.prototype.xSETMETA=function(){
var node=this.MetaNode;
if(null==node){
var meta=this.grid.data.selectSingleNode("//root/gridmeta");
var _3e7=this.MetaNode=this.grid.data.createNode(1,"r","");
_3e7.setAttribute("xi",this.row);
meta.appendChild(_3e7);
node=this.MetaNode=_3e7;
}
if(node!=null){
node.setAttribute(arguments[0],arguments[1][0]);
}else{
alert("Cannot set property: "+arguments[0]);
}
};
nitobi.grid.RowRenderer=function(_3e8,_3e9,_3ea,_3eb,_3ec,_3ed){
this.rowHeight=_3ea||23;
this.xmlDataSource=_3e8;
this.dataTableId="";
this.firstColumn=_3eb;
this.columns=_3ec;
this.firstColumn=_3eb;
this.uniqueId=_3ed;
};
nitobi.grid.RowRenderer.prototype.render=function(_3ee,rows,_3f0,_3f1,_3f2,_3f3){
if(this.xslTemplate==null){
return "";
}
var _3ee=Number(_3ee)||0;
var rows=Number(rows)||0;
this.xslTemplate.addParameter("start",_3ee,"");
this.xslTemplate.addParameter("end",_3ee+rows,"");
this.xslTemplate.addParameter("activeColumn",_3f0,"");
this.xslTemplate.addParameter("activeRow",_3f1,"");
this.xslTemplate.addParameter("sortColumn",_3f2,"");
this.xslTemplate.addParameter("sortDirection",_3f3,"");
this.xslTemplate.addParameter("dataTableId",this.dataTableId,"");
var data=this.xmlDataSource.xmlDoc();
s2=nitobi.xml.transformToString(data,this.xslTemplate,"xml");
s2=s2.replace(/ATOKENTOREPLACE/g,"&nbsp;");
return s2;
};
nitobi.grid.RowRenderer.prototype.generateXslTemplate=function(_3f5,_3f6,_3f7,_3f8,_3f9,_3fa,_3fb,id){
_3f9=_3f9||0;
_3fa=_3fa||0;
_3fb=_3fb||0;
this.columns=_3f8;
this.firstColumn=_3f7;
_3f6.addParameter("showIndicators",_3fa,"");
_3f6.addParameter("showHeaders",_3f9,"");
_3f6.addParameter("firstColumn",_3f7,"");
_3f6.addParameter("lastColumn",_3f7+_3f8,"");
_3f6.addParameter("uniqueId",this.uniqueId,"");
_3f6.addParameter("rowHover",_3fb,"");
_3f6.addParameter("frozenColumnId",(id?id:""),"");
this.xmlTemplate=nitobi.xml.transformToXml(_3f5,_3f6);
try{
var path=(typeof (gApplicationPath)=="undefined"?window.location.href.substr(0,window.location.href.lastIndexOf("/")+1):gApplicationPath);
var imp=this.xmlTemplate.selectNodes("//xsl:import");
for(var i=0;i<imp.length;i++){
imp[i].setAttribute("href",path+"xsl/"+imp[i].getAttribute("href"));
}
}
catch(e){
}
this.xslTemplate=nitobi.xml.createXslProcessor(this.xmlTemplate);
};
nitobi.grid.RowRenderer.prototype.dispose=function(){
this.xslTemplate=null;
this.xmlDataSource=null;
};
EBAScroller_RENDERTIMEOUT=100;
EBAScroller_VIEWPANES=new Array("topleft","topcenter","midleft","midcenter");
nitobi.grid.Scroller3x3=function(_400,_401,_402,top,_404,_405,left,_407,_408,rows,_40a,_40b,_40c,_40d,_40e,_40f){
this.disposal=[];
this.scrollTop=0;
this.scrollLeft=0;
this.height=_402;
this.width=_401;
this.top=Math.min(Math.max(0,top),_402);
this.bottom=Math.min(Math.max(0,_405),_402-this.top);
this.mid=Math.max(0,_402-this.top-this.bottom);
this.left=Math.min(Math.max(0,left),_401);
this.right=Math.min(Math.max(0,_404),_401-this.left);
this.center=Math.max(0,_401-this.left-this.right);
this.rows=rows;
this.columns=_40a;
this.freezetop=_40b;
this.freezeleft=_40c;
this.freezebottom=_40d;
this.freezeright=_40e;
this.lastScrollTop=-1;
this.uid=nitobi.base.getUid();
this.onRenderComplete=new nitobi.base.Event();
this.onRangeUpdate=new nitobi.base.Event();
this.onHtmlReady=new nitobi.base.Event();
this.renderAll=_40f;
this.owner=_400;
var VP=nitobi.grid.Viewport;
this.view={topleft:new VP(this.owner,0,this.top,this.left),topcenter:new VP(this.owner,1,this.top,this.center),midleft:new VP(this.owner,3,this.mid,this.left,top,_404,_405,0),midcenter:new VP(this.owner,4,this.mid,this.center,top,_404,_405,left)};
this.view.midleft.onHtmlReady.subscribe(this.handleHtmlReady,this);
this.setCellRanges();
this.scrollSurface=null;
this.startRow=_40b;
this.headerHeight=23;
this.rowHeight=23;
this.lastTimeoutId=0;
this.scrollTopPercent=0;
this.dataTable=null;
this.cacheMap=new nitobi.collections.CacheMap(-1,-1);
};
nitobi.grid.Scroller3x3.prototype.updateCellRanges=function(cols,rows,frzL,frzT,frzR,frzB){
this.columns=cols;
this.rows=rows;
this.freezetop=frzT;
this.freezeleft=frzL;
this.setCellRanges();
};
nitobi.grid.Scroller3x3.prototype.setCellRanges=function(){
var _417=null;
if(this.implementsStandardPaging()){
_417=this.getDisplayedRowCount();
}
this.view.topleft.setCellRanges(0,this.freezetop,0,this.freezeleft);
this.view.topcenter.setCellRanges(0,this.freezetop,this.freezeleft,this.columns-this.freezeleft-this.freezeright);
this.view.midleft.setCellRanges(this.freezetop,(_417?_417:this.rows)-this.freezebottom-this.freezetop,0,this.freezeleft);
this.view.midcenter.setCellRanges(this.freezetop,(_417?_417:this.rows)-this.freezebottom-this.freezetop,this.freezeleft,this.columns-this.freezeleft-this.freezeright);
};
nitobi.grid.Scroller3x3.prototype.resize=function(_418,_419,top,_41b,_41c,left,_41e,_41f){
this.height=_419;
this.width=_418;
this.top=Math.min(Math.max(0,top),_419);
this.bottom=Math.min(Math.max(0,_41c),_419-this.top);
this.mid=Math.max(0,_419-this.top-this.bottom);
this.left=Math.min(Math.max(0,left),_418);
this.right=Math.min(Math.max(0,_41b),_418-this.left);
this.center=Math.max(0,_418-this.left-this.right);
this.view.topleft.setPosition(this.top,this.left);
this.view.topcenter.setPosition(this.top,this.center);
this.view.midleft.setPosition(this.mid,this.left,top,_41b,_41c,left);
this.view.midcenter.setPosition(this.top,this.left,top,_41b,_41c,left);
};
nitobi.grid.Scroller3x3.prototype.setScrollLeftRelative=function(_420){
this.setScrollLeft(this.scrollLeft+_420);
};
nitobi.grid.Scroller3x3.prototype.setScrollLeftPercent=function(_421){
this.setScrollLeft(Math.round((this.view.midcenter.element.scrollWidth-this.view.midcenter.element.clientWidth)*_421));
};
nitobi.grid.Scroller3x3.prototype.setScrollLeft=function(_422){
this.scrollLeft=_422;
this.view.midcenter.element.scrollLeft=_422;
this.view.topcenter.element.scrollLeft=_422;
};
nitobi.grid.Scroller3x3.prototype.setScrollTopRelative=function(_423){
this.setScrollTop(this.scrollTop+_423);
};
nitobi.grid.Scroller3x3.prototype.setScrollTopPercent=function(_424){
this.scrollTopPercent=_424;
this.setScrollTop(Math.round((this.view.midcenter.element.scrollHeight-this.view.midcenter.element.clientHeight)*_424));
};
nitobi.grid.Scroller3x3.prototype.setScrollTop=function(_425){
this.scrollTop=_425;
this.view.midcenter.element.scrollTop=_425;
this.view.midleft.element.scrollTop=_425;
this.render();
};
nitobi.grid.Scroller3x3.prototype.clearSurfaces=function(_426,_427,_428,_429){
this.flushCache();
_428=true;
if(_426){
_427=true;
_428=true;
_429=true;
}
if(_427){
this.view.topleft.clear(true);
this.view.topcenter.clear(true);
}
if(_428){
this.view.midleft.clear(true,true,false,false);
this.view.midcenter.clear(false,false,true);
}
if(_429){
}
};
nitobi.grid.Scroller3x3.prototype.mapToHtml=function(_42a){
var uid=this.owner.uid;
for(var i=0;i<4;i++){
var node=$("gridvp_"+i+"_"+uid);
this.view[EBAScroller_VIEWPANES[i]].mapToHtml(node,nitobi.html.getFirstChild(node),null);
}
this.scrollSurface=$("gridvp_3_"+uid);
};
nitobi.grid.Scroller3x3.prototype.getUnrenderedBlocks=function(){
var pair={first:this.freezetop,last:this.rows-1-this.freezetop-this.freezebottom};
if(!this.implementsShowAll()){
var _42f=this.scrollSurface.scrollTop+this.top-this.headerHeight;
var MC=this.view.midcenter;
var b0=MC.findBlockAtCoord(_42f);
var b1=MC.findBlockAtCoord(_42f+this.height);
var _433=null;
var _434=null;
var _435=20;
if(b0==null){
return;
}
_433=b0.top+Math.floor((_42f-b0.offsetTop)/this.rowHeight);
if(b1){
_434=b1.top+Math.floor((_42f+this.height-b1.offsetTop)/this.rowHeight)+_435;
}else{
_434=_433+Math.floor(this.height/this.rowHeight)+_435;
}
_434=Math.min(_434,this.rows);
if(this.implementsStandardPaging()){
var _436=0;
if(this.owner.getRenderMode()==nitobi.grid.RENDERMODE_ONDEMAND){
var _437=_433+_436;
var last=Math.min(_434+_436,_436+this.getDisplayedRowCount()-1);
pair={first:_437,last:last};
}else{
var _437=_436;
var last=_437+this.getDisplayedRowCount()-1;
pair={first:_437,last:last};
}
}else{
pair={first:_433,last:_434};
}
this.onRangeUpdate.notify(pair);
}
return pair;
};
nitobi.grid.Scroller3x3.prototype.render=function(_439){
if(this.owner.isBound()&&(this.scrollSurface.scrollTop!=this.lastScrollTop||_439||this.scrollTopPercent>0.9)){
var _43a=nitobi.lang.close(this,this.performRender,[]);
window.clearTimeout(this.lastTimeoutId);
this.lastTimeoutId=window.setTimeout(_43a,EBAScroller_RENDERTIMEOUT);
}
};
nitobi.grid.Scroller3x3.prototype.performRender=function(){
var _43b=this.getUnrenderedBlocks();
if(_43b==null){
return;
}
var _43c=this.scrollSurface.scrollTop;
var mc=this.view.midcenter;
var ml=this.view.midleft;
var _43f=this.getDataTable();
var _440=_43b.first;
var last=_43b.last;
if(last>=_43f.remoteRowCount-1&&!_43f.rowCountKnown){
last+=2;
}
var gaps=this.cacheMap.gaps(_440,last);
var _443=(this.owner.mode=="livescrolling"?(_440+last<=0):(_440+last<=-1));
if(_443){
this.onHtmlReady.notify();
}else{
if(gaps[0]!=null){
var low=gaps[0].low;
var high=gaps[0].high;
var rows=high-low+1;
if(!_43f.inCache(low,rows)){
if(low==null||rows==null){
alert("low or rows =null");
}
_43f.get(low,rows);
var _447=_43f.cachedRanges(low,high);
for(var i=0;i<_447.length;i++){
var _449=this.cacheMap.gaps(_447[i].low,_447[i].high);
for(var j=0;j<_449.length;j++){
_43b.first=_449[j].low;
_43b.last=_449[j].high;
this.renderGap(_449[j].low,_449[j].high);
}
}
return false;
}else{
this.renderGap(low,high);
}
}
}
this.onRenderComplete.notify();
};
nitobi.grid.Scroller3x3.prototype.renderGap=function(low,high){
var gaps=this.cacheMap.gaps(low,high);
var mc=this.view.midcenter;
var ml=this.view.midleft;
if(gaps[0]!=null){
var low=gaps[0].low;
var high=gaps[0].high;
var rows=high-low+1;
this.cacheMap.insert(low,high);
mc.renderGap(low,high);
ml.renderGap(low,high);
}
};
nitobi.grid.Scroller3x3.prototype.flushCache=function(){
if(Boolean(this.cacheMap)){
this.cacheMap.flush();
}
};
nitobi.grid.Scroller3x3.prototype.reRender=function(_451,_452){
var _453=this.view.midleft.clearBlocks(_451,_452);
this.view.midcenter.clearBlocks(_451,_452);
this.cacheMap.remove(_453.top,_453.bottom);
this.render();
};
nitobi.grid.Scroller3x3.prototype.getViewportByCoords=function(row,_455){
var _456=0;
if(row>=_456&&row<this.owner.getfreezetop()&&_455>=0&&_455<this.owner.frozenLeftColumnCount()){
return this.view.topleft;
}
if(row>=_456&&row<this.owner.getfreezetop()&&_455>=this.owner.getFrozenLeftColumnCount()&&_455<this.owner.getColumnCount()){
return this.view.topcenter;
}
if(row>=this.owner.getfreezetop()+_456&&row<this.owner.getDisplayedRowCount()+_456&&_455>=0&&_455<this.owner.getFrozenLeftColumnCount()){
return this.view.midleft;
}
if(row>=this.owner.getfreezetop()+_456&&row<this.owner.getDisplayedRowCount()+_456&&_455>=this.owner.getFrozenLeftColumnCount()&&_455<this.owner.getColumnCount()){
return this.view.midcenter;
}
};
nitobi.grid.Scroller3x3.prototype.getRowsPerPage=function(){
return this.owner.getRowsPerPage();
};
nitobi.grid.Scroller3x3.prototype.getDisplayedRowCount=function(){
return this.owner.getDisplayedRowCount();
};
nitobi.grid.Scroller3x3.prototype.getCurrentPageIndex=function(){
return this.owner.getCurrentPageIndex();
};
nitobi.grid.Scroller3x3.prototype.implementsStandardPaging=function(){
return Boolean(this.owner.getPagingMode().toLowerCase()=="standard");
};
nitobi.grid.Scroller3x3.prototype.implementsShowAll=function(){
return Boolean(this.owner.getPagingMode().toLowerCase()==nitobi.grid.PAGINGMODE_NONE);
};
nitobi.grid.Scroller3x3.prototype.setDataTable=function(_457){
this.dataTable=_457;
};
nitobi.grid.Scroller3x3.prototype.getDataTable=function(){
return this.dataTable;
};
nitobi.grid.Scroller3x3.prototype.handleHtmlReady=function(){
this.onHtmlReady.notify();
};
nitobi.grid.Scroller3x3.prototype.setSort=function(col,dir){
this.view.topleft.setSort(col,dir);
this.view.topcenter.setSort(col,dir);
this.view.midleft.setSort(col,dir);
this.view.midcenter.setSort(col,dir);
};
nitobi.grid.Scroller3x3.prototype.setRowHeight=function(_45a){
this.rowHeight=_45a;
this.setViewportProperty("RowHeight",_45a);
};
nitobi.grid.Scroller3x3.prototype.setHeaderHeight=function(_45b){
this.headerHeight=_45b;
this.setViewportProperty("HeaderHeight",_45b);
};
nitobi.grid.Scroller3x3.prototype.setViewportProperty=function(_45c,_45d){
var sv=this.view;
for(var i=0;i<EBAScroller_VIEWPANES.length;i++){
sv[EBAScroller_VIEWPANES[i]]["set"+_45c](_45d);
}
};
nitobi.grid.Scroller3x3.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.grid.Scroller3x3.prototype.subscribe=function(evt,func,_464){
if(typeof (_464)=="undefined"){
_464=this;
}
return nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_464,func));
};
nitobi.grid.Scroller3x3.prototype.dispose=function(){
try{
(this.cacheMap!=null?this.cacheMap.flush():"");
this.cacheMap=null;
var _465=this.disposal.length;
for(var i=0;i<_465;i++){
if(typeof (this.disposal[i])=="function"){
this.disposal[i].call(this);
}
this.disposal[i]=null;
}
for(var v in this.view){
this.view[v].dispose();
}
for(var item in this){
if(this[item]!=null&&this[item].dispose instanceof Function){
this[item].dispose();
}
}
}
catch(e){
}
};
nitobi.grid.Selection=function(_469){
nitobi.grid.Selection.baseConstructor.call(this,_469);
this.owner=_469;
var t=new Date();
this.selecting=false;
this.expanding=false;
this.resizingRow=false;
this.created=false;
this.freezeTop=this.owner.getfreezetop();
this.freezeLeft=this.owner.getFrozenLeftColumnCount();
this.rowHeight=23;
this.onExpandSelectionStop=new nitobi.base.Event();
this.expandEndCell=null;
this.expandStartCell=null;
};
nitobi.lang.extend(nitobi.grid.Selection,nitobi.collections.CellSet);
nitobi.grid.Selection.prototype.setRange=function(_46b,_46c,_46d,_46e){
nitobi.grid.Selection.base.setRange.call(this,_46b,_46c,_46d,_46e);
this.startCell=this.owner.getCellElement(_46b,_46c);
this.endCell=this.owner.getCellElement(_46d,_46e);
};
nitobi.grid.Selection.prototype.setRangeWithDomNodes=function(_46f,_470){
this.setRange(nitobi.grid.Cell.getRowNumber(_46f),nitobi.grid.Cell.getColumnNumber(_46f),nitobi.grid.Cell.getRowNumber(_470),nitobi.grid.Cell.getColumnNumber(_470));
};
nitobi.grid.Selection.prototype.createBoxes=function(){
if(!this.created){
var _471=nitobi.html.createElement("div",{"class":"ntb-grid-selectionexpandergrabby"},{});
this.boxexpanderBorder=nitobi.html.createElement("div",{"id":"expander"+this.owner.uid,"class":"ntb-grid-selectionexpanderborder"});
this.boxexpanderBorder.appendChild(_471);
this.expanderGrabbyEvents=[{type:"mousedown",handler:this.handleGrabbyMouseDown},{type:"mouseup",handler:this.handleGrabbyMouseUp},{type:"click",handler:this.handleGrabbyClick}];
nitobi.html.attachEvents(_471,this.expanderGrabbyEvents,this);
this.boxexpanderGrabby=_471;
this.box=this.createBox("selectbox"+this.owner.uid);
this.boxtl=this.createBox("selectboxtl"+this.owner.uid);
this.boxt=this.createBox("selectboxt"+this.owner.uid);
this.boxl=this.createBox("selectboxl"+this.owner.uid);
this.events=[{type:"mousemove",handler:this.shrink},{type:"mouseup",handler:this.handleSelectionMouseUp},{type:"mousedown",handler:this.handleSelectionMouseDown},{type:"click",handler:this.handleSelectionClick},{type:"dblclick",handler:this.handleDblClick}];
nitobi.html.attachEvents(this.box,this.events,this);
nitobi.html.attachEvents(this.boxl,this.events,this);
nitobi.html.attachEvents(this.boxt,this.events,this);
var sv=this.owner.Scroller.view;
sv.midcenter.surface.appendChild(this.box);
sv.topleft.element.appendChild(this.boxtl);
sv.topcenter.container.appendChild(this.boxt);
sv.midleft.container.firstChild.appendChild(this.boxl);
this.clear();
this.created=true;
}
};
nitobi.grid.Selection.prototype.createBox=function(id){
var _474;
var cell;
if(nitobi.browser.IE){
cell=_474=document.createElement("div");
}else{
_474=nitobi.html.createTable({"cellpadding":0,"cellspacing":0,"border":0},{"backgroundColor":"transparent"});
cell=_474.rows[0].cells[0];
}
_474.className="ntb-grid-selection";
var _476=nitobi.html.createElement("div",{"id":id,"class":"ntb-grid-selectionbackground"});
cell.appendChild(_476);
cell.style.position="relative";
cell.style.padding="0px";
var bs=_474.style;
bs.border="1px solid black";
return _474;
};
nitobi.grid.Selection.prototype.clearBoxes=function(){
if(this.box!=null){
this.clearBox(this.box);
}
if(this.box1!=null){
this.clearBox(this.box1);
}
if(this.boxt!=null){
this.clearBox(this.boxt);
}
this.created=false;
};
nitobi.grid.Selection.prototype.clearBox=function(box){
nitobi.html.detachEvents(box,this.events);
if(box.parentNode!=null){
box.parentNode.removeChild(box);
}
box=null;
};
nitobi.grid.Selection.prototype.handleGrabbyMouseDown=function(evt){
this.selecting=true;
this.setExpanding(true,"vert");
nitobi.html.Css.addClass(this.boxexpanderBorder,"ntb-grid-selectionexpanderborder-active");
var _47a=this.getTopLeftCell();
var _47b=this.getBottomRightCell();
this.expandStartCell=_47a;
this.expandEndCell=_47b;
var _47c=this.owner.getScrollSurface();
this.expandStartCoords=this.box.getBoundingClientRect(_47c.scrollTop+document.body.scrollTop,_47c.scrollLeft+document.body.scrollLeft);
this.expandStartHeight=Math.abs(_47a.getRow()-_47b.getRow())+1;
this.expandStartWidth=Math.abs(_47a.getColumn()-_47b.getColumn())+1;
this.expandStartTopRow=_47a.getRow();
this.expandStartBottomRow=_47b.getRow();
this.expandStartLeftColumn=_47a.getColumn();
this.expandStartRightColumn=_47b.getColumn();
var Cell=nitobi.grid.Cell;
if(Cell.getRowNumber(this.startCell)>Cell.getRowNumber(this.endCell)){
var _47e=this.startCell;
this.startCell=this.endCell;
this.endCell=_47e;
}
nitobi.html.cancelEvent(evt);
return false;
};
nitobi.grid.Selection.prototype.handleGrabbyMouseUp=function(evt){
if(this.expanding){
this.expanding=false;
this.selecting=false;
nitobi.html.Css.removeClass(this.boxexpanderBorder,"ntb-grid-selectionexpanderborder-active");
this.onExpandSelectionStop.notify(this);
}
};
nitobi.grid.Selection.prototype.handleGrabbyClick=function(evt){
nitobi.html.cancelEvent(evt);
};
nitobi.grid.Selection.prototype.expand=function(cell,dir){
this.setExpanding(true,dir);
var Cell=nitobi.grid.Cell;
var _484;
var _485=this.expandStartTopRow,_486=this.expandStartLeftColumn;
var _487=this.expandStartBottomRow,_488=this.expandStartRightColumn;
var _489=Cell.getRowNumber(this.endCell),_48a=Cell.getColumnNumber(this.endCell);
var _48b=Cell.getRowNumber(this.startCell),_48c=Cell.getColumnNumber(this.startCell);
var _48d=Cell.getColumnNumber(cell);
var _48e=Cell.getRowNumber(cell);
var _48f=_48c,_490=_48b;
if(dir=="horiz"){
if(_48c<_48a&_48d<_48c){
this.changeEndCellWithDomNode(this.owner.getCellElement(_487,_48d));
this.changeStartCellWithDomNode(this.owner.getCellElement(_485,_488));
}else{
if(_48c>_48a&&_48d>_48c){
this.changeEndCellWithDomNode(this.owner.getCellElement(_487,_48d));
this.changeStartCellWithDomNode(this.owner.getCellElement(_485,_486));
}else{
this.changeEndCellWithDomNode(this.owner.getCellElement((_48b==_487?_485:_487),_48d));
}
}
}else{
if(_48b<_489&_48e<_48b){
this.changeEndCellWithDomNode(this.owner.getCellElement(_48e,_488));
this.changeStartCellWithDomNode(this.owner.getCellElement(_487,_486));
}else{
if(_48b>_489&&_48e>_48b){
this.changeEndCellWithDomNode(this.owner.getCellElement(_48e,_488));
this.changeStartCellWithDomNode(this.owner.getCellElement(_485,_486));
}else{
this.changeEndCellWithDomNode(this.owner.getCellElement(_48e,(_48c==_488?_486:_488)));
}
}
}
this.alignBoxes();
};
nitobi.grid.Selection.prototype.shrink=function(evt){
if(nitobi.html.Css.hasClass(evt.srcElement,"ntb-grid-selectionexpanderborder")||nitobi.html.Css.hasClass(evt.srcElement,"ntb-grid-selectionexpandergrabby")){
return;
}
if(this.endCell!=this.startCell&&this.selecting){
var _492=this.owner.getScrollSurface();
var Cell=nitobi.grid.Cell;
var _494=Cell.getRowNumber(this.endCell),_495=Cell.getColumnNumber(this.endCell);
var _496=Cell.getRowNumber(this.startCell),_497=Cell.getColumnNumber(this.startCell);
var _498=nitobi.html.getEventCoords(evt);
var evtY=_498.y,evtX=_498.x;
var _49b=this.endCell.getBoundingClientRect(_492.scrollTop+document.body.scrollTop,_492.scrollLeft+document.body.scrollLeft);
var _49c=_49b.top,_49d=_49b.left;
if(_494>_496&&evtY<_49c){
_494=_494-Math.floor(((_49c-4)-evtY)/this.rowHeight)-1;
}else{
if(evtY>_49b.bottom){
_494=_494+Math.floor((evtY-_49c)/this.rowHeight);
}
}
if(_495>_497&&evtX<_49d){
_495--;
}else{
if(evtX>_49b.right){
_495++;
}
}
if(this.expanding){
var _49e=this.expandStartCell.getRow(),_49f=this.expandStartCell.getColumn();
var _4a0=this.expandEndCell.getRow(),_4a1=this.expandEndCell.getColumn();
if(_495>=this.expandStartLeftColumn&&_495<=this.expandStartRightColumn){
if(_495>=_497&&_495<_4a1){
_495=_4a1;
}else{
if(_495<=_497&&_495>_49f){
_495=_49f;
}
}
if(_495>=_497&&_495<=this.expandStartRightColumn){
_495=this.expandStartRightColumn;
}
}
if(_494>=this.expandStartTopRow&&_494<=this.expandStartBottomRow){
if(_496<_494&&_494<=_4a0){
_494=_4a0;
}else{
if(_496>_494&&_494>=_49e){
_494=_49e;
}else{
if(_496==_494){
_494=(_496==_49e?_4a0:_49e);
}
}
}
}
}
var _4a2=this.owner.getCellElement(_494,_495);
var _4a3=this.owner.getCellElement(_496,_497);
if(_4a2!=null&&_4a2!=this.endCell||_4a3!=null&&_4a3!=this.startCell){
this.changeEndCellWithDomNode(_4a2);
this.changeStartCellWithDomNode(_4a3);
this.alignBoxes();
this.owner.ensureCellInView(_4a2);
}
}
};
nitobi.grid.Selection.prototype.getHeight=function(){
var rect=this.box.getBoundingClientRect();
return rect.top-rect.bottom;
};
nitobi.grid.Selection.prototype.collapse=function(cell){
if(!cell){
cell=this.startCell;
}
if(!cell){
return;
}
this.setRangeWithDomNodes(cell,cell);
if((this.box==null)||(this.box.parentNode==null)||(this.boxl==null)||(this.boxl.parentNode==null)){
this.created=false;
this.createBoxes();
}
if(null==this.boxt.parentNode){
this.owner.Scroller.view.topcenter.container.appendChild(this.boxt);
}
this.alignBoxes();
this.selecting=false;
};
nitobi.grid.Selection.prototype.startSelecting=function(_4a6,_4a7){
this.selecting=true;
this.setRangeWithDomNodes(_4a6,_4a7);
this.shrink();
document.body.attachEvent("onselectstart",function(){
return false;
});
};
nitobi.grid.Selection.prototype.clearSelection=function(cell){
this.collapse(cell);
};
nitobi.grid.Selection.prototype.resizeSelection=function(cell){
this.endCell=cell;
this.shrink();
};
nitobi.grid.Selection.prototype.moveSelection=function(cell){
this.collapse(cell);
};
nitobi.grid.Selection.prototype.alignBoxes=function(){
var _4ab=this.endCell||this.startCell;
var sc=this.getCoords();
var _4ad=sc.top.y;
var _4ae=sc.top.x;
var _4af=sc.bottom.y;
var _4b0=sc.bottom.x;
var _4b1=(document.compatMode=="CSS1Compat");
var ox=oy=(nitobi.browser.IE?-1:0);
var ow=oh=(nitobi.browser.IE&&_4b1?-1:1);
if(_4b0>=this.freezeLeft&&_4af>=this.freezeTop){
var e=this.box;
e.style.display="block";
this.align(e,this.startCell,_4ab,286265344,oh,ow,oy,ox);
(e.rows!=null?e.rows[0].cells[0]:e).appendChild(this.boxexpanderBorder);
}else{
this.box.style.display="none";
}
if(_4ae<=this.freezeLeft&&_4ad<this.freezeTop){
this.boxtl.style.display="block";
this.align(this.boxtl,this.startCell,_4ab,286265344,oh,ow,oy,ox);
}else{
this.boxtl.style.display="none";
}
if(_4ad<this.freezeTop){
this.boxt.style.display="block";
this.align(this.boxt,this.startCell,_4ab,286265344,oh,ow,oy,ox);
}else{
this.boxt.style.display="none";
}
if(_4b0<this.freezeLeft||_4ae<this.freezeLeft){
var e=this.boxl;
e.style.display="block";
this.align(e,this.startCell,_4ab,286265344,oh,ow,oy,ox);
if(this.box.style.display=="none"){
(e.rows!=null?e.rows[0].cells[0]:e).appendChild(this.boxexpanderBorder);
}
}else{
this.boxl.style.display="none";
}
};
nitobi.grid.Selection.prototype.redraw=function(cell){
if(!this.selecting){
this.setRangeWithDomNodes(cell,cell);
}else{
this.changeEndCellWithDomNode(cell);
}
this.alignBoxes();
};
nitobi.grid.Selection.prototype.changeStartCellWithDomNode=function(cell){
this.startCell=cell;
var Cell=nitobi.grid.Cell;
this.changeStartCell(Cell.getRowNumber(cell),Cell.getColumnNumber(cell));
};
nitobi.grid.Selection.prototype.changeEndCellWithDomNode=function(cell){
this.endCell=cell;
var Cell=nitobi.grid.Cell;
this.changeEndCell(Cell.getRowNumber(cell),Cell.getColumnNumber(cell));
};
nitobi.grid.Selection.prototype.init=function(cell){
this.createBoxes();
var t=new Date();
this.selecting=true;
this.setRangeWithDomNodes(cell,cell);
};
nitobi.grid.Selection.prototype.clear=function(){
if(!this.box){
return;
}
this.box.style.display="none";
this.box.style.top="-1000px";
this.box.style.left="-1000px";
this.box.style.width="1px";
this.box.style.height="1px";
this.boxtl.style.display="none";
this.boxtl.style.top="-1000px";
this.boxtl.style.left="-1000px";
this.boxtl.style.width="1px";
this.boxtl.style.height="1px";
this.boxt.style.display="none";
this.boxt.style.top="-1000px";
this.boxt.style.left="-1000px";
this.boxt.style.width="1px";
this.boxt.style.height="1px";
this.boxl.style.display="none";
this.boxl.style.top="-1000px";
this.boxl.style.left="-1000px";
this.boxl.style.width="1px";
this.boxl.style.height="1px";
this.selecting=false;
};
nitobi.grid.Selection.prototype.handleSelectionClick=function(evt){
if(!this.selected()){
if(NTB_SINGLECLICK==null){
evt=nitobi.lang.copy(evt);
NTB_SINGLECLICK=window.setTimeout(nitobi.lang.close(this,this.edit,[evt]),150);
}
}else{
this.collapse();
this.owner.focus();
}
};
nitobi.grid.Selection.prototype.handleDblClick=function(evt){
if(!this.selected()){
window.clearTimeout(NTB_SINGLECLICK);
NTB_SINGLECLICK=null;
if(this.owner.handleDblClick(evt)){
this.edit(evt);
}
}else{
this.collapse();
}
};
nitobi.grid.Selection.prototype.edit=function(evt){
NTB_SINGLECLICK=null;
this.owner.edit(evt);
};
nitobi.grid.Selection.prototype.select=function(_4bf,_4c0){
this.selectWithCoords(_4bf.getRowNumber(),_4bf.getColumnNumber(),_4c0.getRowNumber(),_4c0.getColumnNumber());
};
nitobi.grid.Selection.prototype.selectWithCoords=function(_4c1,_4c2,_4c3,_4c4){
this.setRange(_4c1,_4c2,_4c3,_4c4);
this.createBoxes();
this.alignBoxes();
};
nitobi.grid.Selection.prototype.handleSelectionMouseUp=function(evt){
if(this.expanding){
this.handleGrabbyMouseUp(evt);
}
this.stopSelecting();
this.owner.handleClick(evt);
};
nitobi.grid.Selection.prototype.handleSelectionMouseDown=function(evt){
nitobi.html.cancelEvent(evt);
};
nitobi.grid.Selection.prototype.stopSelecting=function(){
this.selecting=true;
if(!this.selected()){
this.collapse(this.startCell);
}
this.selecting=false;
};
nitobi.grid.Selection.prototype.getStartCell=function(){
return this.startCell;
};
nitobi.grid.Selection.prototype.getEndCell=function(){
return this.endCell;
};
nitobi.grid.Selection.prototype.getTopLeftCell=function(){
var _4c7=this.getCoords();
return new nitobi.grid.Cell(this.owner,_4c7.top.y,_4c7.top.x);
};
nitobi.grid.Selection.prototype.getBottomRightCell=function(){
var _4c8=this.getCoords();
return new nitobi.grid.Cell(this.owner,_4c8.bottom.y,_4c8.bottom.x);
};
nitobi.grid.Selection.prototype.getHeight=function(){
var _4c9=this.getCoords();
return _4c9.bottom.y-_4c9.top.y+1;
};
nitobi.grid.Selection.prototype.getWidth=function(){
var _4ca=this.getCoords();
return _4ca.bottom.x-_4ca.top.x+1;
};
nitobi.grid.Selection.prototype.getRowByCoords=function(_4cb){
return (_4cb.parentNode.offsetTop/_4cb.parentNode.offsetHeight);
};
nitobi.grid.Selection.prototype.getColumnByCoords=function(_4cc){
var _4cd=(this.indicator?-2:0);
if(_4cc.parentNode.parentNode.getAttribute("id").substr(0,6)!="freeze"){
_4cd+=2-(this.freezeColumn*3);
}else{
_4cd+=2;
}
return Math.floor((_4cc.sourceIndex-_4cc.parentNode.sourceIndex-_4cd)/3);
};
nitobi.grid.Selection.prototype.selected=function(){
return (this.endCell==this.startCell)?false:true;
};
nitobi.grid.Selection.prototype.setRowHeight=function(_4ce){
this.rowHeight=_4ce;
};
nitobi.grid.Selection.prototype.getRowHeight=function(){
return this.rowHeight;
};
nitobi.grid.Selection.prototype.setExpanding=function(val,dir){
this.expanding=val;
this.expandingVertical=(dir=="horiz"?false:true);
};
nitobi.grid.Selection.prototype.dispose=function(){
};
nitobi.grid.Selection.prototype.align=function(_4d1,_4d2,_4d3,_4d4,oh,ow,oy,ox,show){
oh=oh||0;
ow=ow||0;
oy=oy||0;
ox=ox||0;
var a=_4d4;
var td,sd,tt,tb,tl,tr,th,tw,st,sb,sl,sr,sh,sw;
if(!_4d2||!(_4d2.getBoundingClientRect)){
return;
}
ad=_4d2.getBoundingClientRect();
bd=_4d3.getBoundingClientRect();
sd=_4d1.getBoundingClientRect();
at=ad.top;
ab=ad.bottom;
al=ad.left;
ar=ad.right;
bt=bd.top;
bb=bd.bottom;
bl=bd.left;
br=bd.right;
tt=ad.top;
tb=bd.bottom;
tl=ad.left;
tr=bd.right;
th=Math.abs(tb-tt);
tw=Math.abs(tr-tl);
st=sd.top;
sb=sd.bottom;
sl=sd.left;
sr=sd.right;
sh=Math.abs(sb-st);
sw=Math.abs(sr-sl);
if(a&268435456){
_4d1.style.height=(Math.max(bb-at,ab-bt)+oh)+"px";
}
if(a&16777216){
_4d1.style.width=(Math.max(br-al,ar-bl)+ow)+"px";
}
if(a&1048576){
_4d1.style.top=(nitobi.html.getStyleTop(_4d1)+Math.min(tt,bt)-st+oy)+"px";
}
if(a&65536){
_4d1.style.top=(nitobi.html.getStyleTop(_4d1)+tt-st+th-sh+oy)+"px";
}
if(a&4096){
_4d1.style.left=(nitobi.html.getStyleLeft(_4d1)-sl+Math.min(tl,bl)+ox)+"px";
}
if(a&256){
_4d1.style.left=(nitobi.html.getStyleLeft(_4d1)-sl+tl+tw-sw+ox)+"px";
}
if(a&16){
_4d1.style.top=(nitobi.html.getStyleTop(_4d1)+tt-st+oy+Math.floor((th-sh)/2))+"px";
}
if(a&1){
_4d1.style.left=(nitobi.html.getStyleLeft(_4d1)-sl+tl+ox+Math.floor((tw-sw)/2))+"px";
}
};
nitobi.grid.Surface=function(_4e9,_4ea,_4eb){
this.height=_4ea;
this.width=_4e9;
this.element=_4eb;
};
nitobi.grid.Surface.prototype.dispose=function(){
this.element=null;
};
nitobi.grid.TextColumn=function(grid,_4ed){
nitobi.grid.TextColumn.baseConstructor.call(this,grid,_4ed);
this.Interface=grid.API.selectSingleNode("interfaces/interface[@name='EBATextColumn']");
eval(nitobi.xml.transformToString(this.Interface,grid.accessorGeneratorXslProc));
};
nitobi.lang.extend(nitobi.grid.TextColumn,nitobi.grid.Column);
nitobi.lang.defineNs("nitobi.ui");
nitobi.ui.Toolbars=function(_4ee){
this.uid="nitobiToolbar_"+nitobi.base.getUid();
this.toolbars={};
this.visibleToolbars=_4ee;
};
nitobi.ui.Toolbars.VisibleToolbars={};
nitobi.ui.Toolbars.VisibleToolbars.STANDARD=1;
nitobi.ui.Toolbars.VisibleToolbars.PAGING=1<<1;
nitobi.ui.Toolbars.prototype.initialize=function(){
this.enabled=true;
this.toolbarXml=nitobi.xml.createXmlDoc(nitobi.xml.serialize(nitobi.grid.toolbarDoc));
this.toolbarPagingXml=nitobi.xml.createXmlDoc(nitobi.xml.serialize(nitobi.grid.pagingToolbarDoc));
};
nitobi.ui.Toolbars.prototype.attachToParent=function(_4ef){
this.initialize();
this.container=_4ef;
if(this.standardToolbar==null&&this.visibleToolbars){
this.makeToolbar();
this.render();
}
};
nitobi.ui.Toolbars.prototype.setWidth=function(_4f0){
this.width=_4f0;
};
nitobi.ui.Toolbars.prototype.getWidth=function(){
return this.width;
};
nitobi.ui.Toolbars.prototype.setRowInsertEnabled=function(_4f1){
this.rowInsertEnabled=_4f1;
};
nitobi.ui.Toolbars.prototype.isRowInsertEnabled=function(){
return this.rowInsertEnabled;
};
nitobi.ui.Toolbars.prototype.setRowDeleteEnabled=function(_4f2){
this.rowDeleteEnabled=_4f2;
};
nitobi.ui.Toolbars.prototype.isRowDeleteEnabled=function(){
return this.rowDeleteEnabled;
};
nitobi.ui.Toolbars.prototype.makeToolbar=function(){
var _4f3=this.findCssUrl();
this.toolbarXml.documentElement.setAttribute("id","toolbar"+this.uid);
this.toolbarXml.documentElement.setAttribute("image_directory",_4f3);
var _4f4=this.toolbarXml.selectNodes("/toolbar/items/*");
for(var i=0;i<_4f4.length;i++){
if(_4f4[i].nodeType!=8){
_4f4[i].setAttribute("id",_4f4[i].getAttribute("id")+this.uid);
}
}
this.standardToolbar=new nitobi.ui.Toolbar(this.toolbarXml,"toolbar"+this.uid);
this.toolbarPagingXml.documentElement.setAttribute("id","toolbarpaging"+this.uid);
this.toolbarPagingXml.documentElement.setAttribute("image_directory",_4f3);
_4f4=(this.toolbarPagingXml.selectNodes("/toolbar/items/*"));
for(var i=0;i<_4f4.length;i++){
if(_4f4[i].nodeType!=8){
_4f4[i].setAttribute("id",_4f4[i].getAttribute("id")+this.uid);
}
}
this.pagingToolbar=new nitobi.ui.Toolbar(this.toolbarPagingXml,"toolbarpaging"+this.uid);
};
nitobi.ui.Toolbars.prototype.getToolbar=function(id){
return eval("this."+id);
};
nitobi.ui.Toolbars.prototype.findCssUrl=function(){
var _4f7=nitobi.html.Css.findParentStylesheet(".EbaToolbar");
if(_4f7==null){
_4f7=nitobi.html.Css.findParentStylesheet(".ntbgrid");
if(_4f7==null){
nitobi.lang.throwError("The CSS for the toolbar could not be found.  Try moving the nitobi.grid.css file to a location accessible to the browser's javascript or moving it to the top of the stylesheet list. findParentStylesheet returned "+_4f7);
}
}
return nitobi.html.Css.getPath(_4f7);
};
nitobi.ui.Toolbars.prototype.isToolbarEnabled=function(){
return this.enabled;
};
nitobi.ui.Toolbars.prototype.render=function(){
var _4f8=this.container;
_4f8.style.visibility="hidden";
var xsl=nitobi.ui.ToolbarXsl;
if(xsl.indexOf("xsl:stylesheet")==-1){
xsl="<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:output method=\"xml\" version=\"4.0\" />"+xsl+"</xsl:stylesheet>";
}
var _4fa=nitobi.xml.createXslDoc(xsl);
var _4fb=nitobi.xml.transformToString(this.standardToolbar.getXml(),_4fa,"xml");
_4f8.innerHTML=_4fb;
_4f8.style.zIndex="1000";
var _4fc=nitobi.xml.transformToString(this.pagingToolbar.getXml(),_4fa,"xml");
_4f8.innerHTML+=_4fc;
_4fa=null;
xmlDoc=null;
this.standardToolbar.attachToTag();
this.standardToolbar.dockEvent=nitobi.lang.close(this,this.onToolbarDock);
this.standardToolbar.undockEvent=nitobi.lang.close(this,this.onToolbarUnDock);
this.pagingToolbar.attachToTag();
this.pagingToolbar.dockEvent=nitobi.lang.close(this,this.onToolbarDock);
this.pagingToolbar.undockEvent=nitobi.lang.close(this,this.onToolbarUnDock);
this.resize();
var _4fd=this;
var _4fe=this.standardToolbar.getUiElements();
for(eachbutton in _4fe){
switch(eachbutton){
case "save"+this.uid:
_4fe[eachbutton].onClick=function(){
_4fd.fire("Save");
};
break;
case "newRecord"+this.uid:
_4fe[eachbutton].onClick=function(){
_4fd.fire("InsertRow");
};
if(!this.isRowInsertEnabled()){
_4fe[eachbutton].disable();
}
break;
case "deleteRecord"+this.uid:
_4fe[eachbutton].onClick=function(){
_4fd.fire("DeleteRow");
};
if(!this.isRowDeleteEnabled()){
_4fe[eachbutton].disable();
}
break;
case "refresh"+this.uid:
_4fe[eachbutton].onClick=function(){
var _4ff=confirm("Refreshing will discard any changes you have made. Is it OK to refresh?");
if(_4ff){
_4fd.fire("Refresh");
}
};
break;
default:
}
}
var _500=this.pagingToolbar.getUiElements();
var _4fd=this;
for(eachPbutton in _500){
switch(eachPbutton){
case "previousPage"+this.uid:
_500[eachPbutton].onClick=function(){
_4fd.fire("PreviousPage");
};
_500[eachPbutton].disable();
break;
case "nextPage"+this.uid:
_500[eachPbutton].onClick=function(){
_4fd.fire("NextPage");
};
break;
default:
}
}
if(this.visibleToolbars&nitobi.ui.Toolbars.VisibleToolbars.STANDARD){
this.standardToolbar.show();
}else{
this.standardToolbar.hide();
}
if(this.visibleToolbars&nitobi.ui.Toolbars.VisibleToolbars.PAGING){
this.pagingToolbar.show();
}else{
this.pagingToolbar.hide();
}
_4f8.style.visibility="visible";
};
nitobi.ui.Toolbars.prototype.resize=function(){
var _501=this.getWidth();
if(this.visibleToolbars&nitobi.ui.Toolbars.VisibleToolbars.PAGING){
_501=_501-2-parseInt(this.pagingToolbar.getWidth());
}
if(this.visibleToolbars&nitobi.ui.Toolbars.VisibleToolbars.STANDARD){
this.standardToolbar.setWidth(_501);
}
};
nitobi.ui.Toolbars.prototype.onToolbarDock=function(){
if(this.containerEmpty&&!this.areAllToolbarsDocked()){
this.fire("ToolbarsContainerNotEmpty");
}
this.containerEmpty=false;
};
nitobi.ui.Toolbars.prototype.areAllToolbarsDocked=function(){
return ((this.pagingToolbar!=null&&this.pagingToolbar.isFloating()||!this.pagingToolbar.isVisible())&&(this.standardToolbar!=null&&this.standardToolbar.isFloating()||!this.standardToolbar.isVisible()));
};
nitobi.ui.Toolbars.prototype.areAnyToolbarsDocked=function(){
return ((this.pagingToolbar!=null&&!this.pagingToolbar.isFloating()&&this.pagingToolbar.isVisible())||(this.standardToolbar!=null&&!this.standardToolbar.isFloating()&&this.standardToolbar.isVisible()));
};
nitobi.ui.Toolbars.prototype.onToolbarUnDock=function(){
if(this.areAllToolbarsDocked()){
this.fire("ToolbarsContainerEmpty");
this.containerEmpty=true;
}else{
this.containerEmpty=false;
}
};
nitobi.ui.Toolbars.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.ui.Toolbars.prototype.subscribe=function(evt,func,_506){
if(typeof (_506)=="undefined"){
_506=this;
}
return nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_506,func));
};
nitobi.ui.Toolbars.prototype.dispose=function(){
this.toolbarXml=null;
this.toolbarPagingXml=null;
if(this.toolbar&&this.toolbar.dispose){
this.toolbar.dispose();
this.toolbar=null;
}
if(this.toolbarPaging&&this.toolbarPaging.dispose){
this.toolbarPaging.dispose();
this.toolbarPaging=null;
}
};
var EBA_SELECTION_BUFFER=15;
var NTB_SINGLECLICK=null;
nitobi.grid.Viewport=function(grid,_508,_509,_50a,top,_50c,_50d,left,_50f,_510,_511){
this.disposal=[];
this.height=_509;
this.width=_50a;
this.surface=_510||new nitobi.grid.Surface();
this.element=_50f;
this.rowHeight=23;
this.headerHeight=23;
this.sortColumn=0;
this.sortDir=1;
this.uid=nitobi.base.getUid();
this.region=_508;
top=(Boolean(top)&&!isNaN(top)?top:0);
this.top=Math.min(Math.max(0,top),_509);
this.bottom=Math.min(Math.max(0,_50d),_509-this.top);
this.mid=Math.max(0,_509-this.top-this.bottom);
this.left=Math.min(Math.max(0,left),_50a);
this.right=Math.min(Math.max(0,_50c),_50a-this.left);
this.center=Math.max(0,_50a-this.left-this.right);
this.scrollIncrement=0;
this.grid=grid;
this.startRow=0;
this.rows=0;
this.startColumn=0;
this.columns=0;
this.rowRenderer=null;
this.onHtmlReady=new nitobi.base.Event();
};
nitobi.grid.Viewport.prototype.mapToHtml=function(_512,_513,_514){
this.surface=_513;
this.element=_512;
this.container=nitobi.html.getFirstChild(_513);
this.makeLastBlock(0,this.grid.getRowsPerPage()*5);
};
nitobi.grid.Viewport.prototype.makeLastBlock=function(low,high){
if(this.lastEmptyBlock==null&&this.grid&&this.region>2&&this.region<5&&this.container){
if(this.container.lastChild){
low=Math.max(low,this.container.lastChild.bottom);
}
this.lastEmptyBlock=this.renderEmptyBlock(low,high);
}
};
nitobi.grid.Viewport.prototype.setCellRanges=function(_517,rows,_519,_51a){
this.startRow=_517;
this.rows=rows;
this.startColumn=_519;
this.columns=_51a;
this.makeLastBlock(this.startRow,this.startRow+rows-1);
if(this.lastEmptyBlock!=null&&this.region>2&&this.region<5&&this.rows>0){
var _51b=this.startRow+this.rows-1;
if(this.lastEmptyBlock.top>_51b){
this.container.removeChild(this.lastEmptyBlock);
this.lastEmptyBlock=null;
}else{
this.lastEmptyBlock.bottom=_51b;
this.lastEmptyBlock.style.height=(this.rowHeight*(this.lastEmptyBlock.bottom-this.lastEmptyBlock.top+1))+"px";
if(this.lastEmptyBlock.bottom<this.lastEmptyBlock.top){
throw "blocks are miss aligned.";
}
}
}
};
nitobi.grid.Viewport.prototype.setPosition=function(_51c,_51d,top,_51f,_520,left){
this.height=_51c;
this.width=_51d;
if(this.region==3){
ntbAssert(top>=0&&_51c>=0,"top and height are incorrectly defined in viewport.setPosition. Viewport region: "+this.region+". (top,height) = "+top+","+_51c);
}
this.top=Math.min(Math.max(0,top),_51c);
this.bottom=Math.min(Math.max(0,_520),_51c-this.top);
this.mid=Math.max(0,_51c-this.top-this.bottom);
this.left=Math.min(Math.max(0,left),_51d);
this.right=Math.min(Math.max(0,_51f),_51d-this.left);
this.center=Math.max(0,_51d-this.left-this.right);
};
nitobi.grid.Viewport.prototype.clear=function(_522,_523,_524,_525){
var uid=this.grid.uid;
if(this.surface&&_522){
this.surface.innerHTML="<div id=\"gridvpcontainer_"+this.region+"_"+uid+"\"></div>";
}
if(this.element&&_525){
this.element.innerHTML="<div id=\"gridvpsurface_"+this.region+"_"+uid+"\"><div id=\"gridvpcontainer_"+this.region+"_"+uid+"\"></div></div>";
}
if(this.surface&&_524){
this.surface.innerHTML="<div id=\"gridvpcontainer_"+this.region+"_"+uid+"\"></div>";
}
this.surface=nitobi.html.getFirstChild(this.element);
this.container=nitobi.html.getFirstChild(this.surface);
if(this.grid&&this.region>2&&this.region<5){
this.lastEmptyBlock=null;
}
this.makeLastBlock(0,this.grid.getRowsPerPage()*5);
};
nitobi.grid.Viewport.prototype.setSort=function(_527,_528){
this.sortColumn=_527;
this.sortDir=_528;
};
nitobi.grid.Viewport.prototype.renderGap=function(top,_52a){
var _52b=this.grid.activeCell;
var _52c=0,_52d=0;
if(_52b!=null){
_52c=nitobi.grid.Cell.getColumnNumber(_52b);
_52d=nitobi.grid.Cell.getRowNumber(_52b);
}
var _52e=this.findBlock(top);
var o=this.renderInsideEmptyBlock(top,_52a,_52e);
if(o==null){
return;
}
o.setAttribute("rendered","true");
var rows=_52a-top+1;
o.innerHTML=this.rowRenderer.render(top,rows,_52c,_52d,this.sortColumn,this.sortDir);
this.onHtmlReady.notify(this);
};
nitobi.grid.Viewport.prototype.findBlock=function(row){
var blk=this.container.childNodes;
for(var i=0;i<blk.length;i++){
if(row>=blk[i].top&&row<=blk[i].bottom){
return blk[i];
}
}
};
nitobi.grid.Viewport.prototype.findBlockAtCoord=function(top){
var blk=this.container.childNodes;
for(var i=0;i<blk.length;i++){
var rt=blk[i].offsetTop;
var rb=rt+blk[i].offsetHeight;
if(top>=rt&&top<=rb){
return blk[i];
}
}
};
nitobi.grid.Viewport.prototype.getBlocks=function(_539,_53a){
var _53b=[];
var _53c=this.findBlock(_539);
var _53d=_53c;
_53b.push(_53c);
while(_53a>_53d.bottom){
var _53e=_53d.nextSibling;
if(_53e!=null){
_53d=_53e;
}else{
break;
}
_53b.push(_53d);
}
return _53b;
};
nitobi.grid.Viewport.prototype.clearBlocks=function(_53f,_540){
var _541=this.getBlocks(_53f,_540);
var len=_541.length;
var top=_541[0].top;
var _544=_541[len-1].bottom;
var _545=_541[len-1].nextSibling;
for(var i=0;i<len;i++){
_541[i].parentNode.removeChild(_541[i]);
}
this.renderEmptyBlock(top,_544,_545);
return {"top":top,"bottom":_544};
};
nitobi.grid.Viewport.prototype.renderInsideEmptyBlock=function(top,_548,_549){
if(_549==null){
return this.renderBlock(top,_548);
}
if(top==_549.top&&_548>=_549.bottom){
var _54a=this.renderBlock(top,_548,_549);
this.container.replaceChild(_54a,_549);
if(_549.bottom<_549.top){
throw "Render error";
}
return _54a;
}
if(top==_549.top&&_548<_549.bottom){
_549.top=_548+1;
_549.style.height=(this.rowHeight*(_549.bottom-_549.top+1))+"px";
_549.rows=_549.bottom-_549.top+1;
if(_549.bottom<_549.top){
throw "Render error";
}
return this.renderBlock(top,_548,_549);
}
if(top>_549.top&&_548>=_549.bottom){
_549.bottom=top-1;
_549.style.height=(this.rowHeight*(_549.bottom-_549.top+1))+"px";
if(_549.bottom<_549.top){
throw "Render error";
}
return this.renderBlock(top,_548,_549.nextSibling);
}
if(top>_549.top&&_548<_549.bottom){
var _54b=this.renderEmptyBlock(_549.top,top-1,_549);
_549.top=_548+1;
_549.style.height=(this.rowHeight*(_549.bottom-_549.top+1))+"px";
if(_549.bottom<_549.top){
throw "Render error";
}
return this.renderBlock(top,_548,_549);
}
throw "Could not insert "+top+"-"+_548+_549.outerHTML;
};
nitobi.grid.Viewport.prototype.renderEmptyBlock=function(top,_54d,_54e){
var o=this.renderBlock(top,_54d,_54e);
o.setAttribute("id","eba_grid_emptyblock_"+this.region+"_"+top+"_"+_54d+"_"+this.grid.uid);
if(top==0&&_54d==99){
crash;
}
o.setAttribute("rendered","false");
o.style.height=((_54d-top+1)*this.rowHeight)+"px";
return o;
};
nitobi.grid.Viewport.prototype.renderBlock=function(top,_551,_552){
var o=document.createElement("div");
o.setAttribute("id","eba_grid_block_"+this.region+"_"+top+"_"+_551+"_"+this.grid.uid);
o.top=top;
o.bottom=_551;
o.left=this.startColumn;
o.right=this.startColumn+this.columns;
o.rows=_551-top+1;
o.columns=this.columns;
if(_552){
this.container.insertBefore(o,_552);
}else{
this.container.insertBefore(o,null);
}
return o;
};
nitobi.grid.Viewport.prototype.setHeaderHeight=function(_554){
this.headerHeight=_554;
};
nitobi.grid.Viewport.prototype.setRowHeight=function(_555){
this.rowHeight=_555;
};
nitobi.grid.Viewport.prototype.dispose=function(){
this.element=null;
this.container=null;
nitobi.lang.dispose(this,this.disposal);
return;
};
nitobi.grid.Viewport.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.grid.Viewport.prototype.subscribe=function(evt,func,_55a){
if(typeof (_55a)=="undefined"){
_55a=this;
}
return nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_55a,func));
};
nitobi.grid.Viewport.prototype.attach=function(evt,func,_55d){
return nitobi.html.attachEvent(_55d,evt,nitobi.lang.close(this,func));
};
nitobi.lang.defineNs("nitobi.data");
if(false){
nitobi.data=function(){
};
}
nitobi.data.DATAMODE_UNBOUND="unbound";
nitobi.data.DATAMODE_LOCAL="local";
nitobi.data.DATAMODE_REMOTE="remote";
nitobi.data.DATAMODE_CACHING="caching";
nitobi.data.DATAMODE_STATIC="static";
nitobi.data.DATAMODE_PAGING="paging";
nitobi.data.DataSet=function(){
var _55e="http://www.nitobi.com";
this.doc=nitobi.xml.createXmlDoc("<"+nitobi.xml.nsPrefix+"datasources xmlns:ntb=\""+_55e+"\"></"+nitobi.xml.nsPrefix+"datasources>");
};
nitobi.data.DataSet.prototype.initialize=function(){
this.tables=new Array();
};
nitobi.data.DataSet.prototype.add=function(_55f){
this.tables[_55f.id]=_55f;
};
nitobi.data.DataSet.prototype.getTable=function(_560){
return this.tables[_560];
};
nitobi.data.DataSet.prototype.xmlDoc=function(){
var root=this.doc.documentElement;
while(root.hasChildNodes()){
root.removeChild(root.firstChild);
}
for(var i in this.tables){
if(this.tables[i].xmlDoc&&this.tables[i].xmlDoc.documentElement){
var _563=this.tables[i].xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource").cloneNode(true);
this.doc.selectSingleNode("/"+nitobi.xml.nsPrefix+"datasources").appendChild(_563);
}
}
return this.doc;
};
nitobi.data.DataSet.prototype.dispose=function(){
for(var _564 in this.tables){
this.tables[_564].dispose();
}
};
nitobi.lang.defineNs("nitobi.data");
nitobi.data.DataTable=function(mode,_566,_567,_568,_569){
if(_566==null){
}
this.estimateRowCount=_566;
this.version=3;
this.uid=nitobi.base.getUid();
this.mode=mode||"caching";
this.setAutoKeyEnabled(_569);
this.columns=new Array();
this.keys=new Array();
this.types=new Array();
this.defaults=new Array();
this.columnsConfigured=false;
this.pagingConfigured=false;
this.id="_default";
this.fieldMap={};
if(_567){
this.saveHandlerArgs=_567;
}else{
this.saveHandlerArgs={};
}
if(_568){
this.getHandlerArgs=_568;
}else{
this.getHandlerArgs={};
}
this.setGetHandlerParameter("RequestType","GET");
this.setSaveHandlerParameter("RequestType","SAVE");
this.batchInsert=false;
this.batchInsertRowCount=0;
};
nitobi.data.DataTable.DEFAULT_LOG="<"+nitobi.xml.nsPrefix+"grid "+nitobi.xml.nsDecl+"><"+nitobi.xml.nsPrefix+"datasources id='id'><"+nitobi.xml.nsPrefix+"datasource id=\"{id}\"><"+nitobi.xml.nsPrefix+"datasourcestructure /><"+nitobi.xml.nsPrefix+"data id=\"_default\"></"+nitobi.xml.nsPrefix+"data></"+nitobi.xml.nsPrefix+"datasource></"+nitobi.xml.nsPrefix+"datasources></"+nitobi.xml.nsPrefix+"grid>";
nitobi.data.DataTable.DEFAULT_DATA="<"+nitobi.xml.nsPrefix+"datasource "+nitobi.xml.nsDecl+" id=\"{id}\"><"+nitobi.xml.nsPrefix+"datasourcestructure FieldNames=\"{fields}\" Keys=\"{keys}\" types=\"{types}\" defaults=\"{defaults}\"></"+nitobi.xml.nsPrefix+"datasourcestructure><"+nitobi.xml.nsPrefix+"data id=\"{id}\"></"+nitobi.xml.nsPrefix+"data></"+nitobi.xml.nsPrefix+"datasource>";
nitobi.data.DataTable.prototype.initialize=function(_56a,_56b,_56c,_56d,_56e,sort,_570,_571,_572){
this.setGetHandlerParameter("TableId",_56a);
this.setSaveHandlerParameter("TableId",_56a);
this.id=_56a;
this.datastructure=null;
this.descriptor=new nitobi.data.DataTableDescriptor(this,nitobi.lang.close(this,this.syncRowCount),this.estimateRowCount);
this.pageFirstRow=0;
this.pageRowCount=0;
this.pageSize=_56e;
this.minPageSize=10;
this.requestCache=new nitobi.collections.CacheMap(-1,-1);
this.dataCache=new nitobi.collections.CacheMap(-1,-1);
this.flush();
this.sortColumn=sort;
this.sortDir=_570||"Asc";
this.filter=new Array();
this.onGenerateKey=_571;
this.remoteRowCount=0;
this.setRowCountKnown(false);
if(_56d==null){
_56d=0;
}
if(this.mode!="unbound"){
if(_56b!=null){
this.ajaxCallbackPool=new nitobi.ajax.HttpRequestPool(nitobi.ajax.HttpRequestPool_MAXCONNECTIONS);
this.ajaxCallbackPool.context=this;
this.setGetHandler(_56b);
this.setSaveHandler(_56c);
}
this.ajaxCallback=new nitobi.ajax.HttpRequest();
this.ajaxCallback.responseType="xml";
}else{
if(_56b!=null&&typeof (_56b)!="string"){
this.initializeXml(_56b);
}
}
this.sortXslProc=nitobi.xml.createXslProcessor(nitobi.data.sortXslProc.stylesheet);
this.requestQueue=new Array();
this.async=true;
};
nitobi.data.DataTable.prototype.setOnGenerateKey=function(_573){
this.onGenerateKey=_573;
};
nitobi.data.DataTable.prototype.getOnGenerateKey=function(){
return this.onGenerateKey;
};
nitobi.data.DataTable.prototype.setAutoKeyEnabled=function(val){
this.autoKeyEnabled=val;
};
nitobi.data.DataTable.prototype.isAutoKeyEnabled=function(){
return this.autoKeyEnabled;
};
nitobi.data.DataTable.prototype.initializeXml=function(oXml){
this.replaceData(oXml);
var rows=this.xmlDoc.selectNodes("//"+nitobi.xml.nsPrefix+"e").length;
if(rows>0){
var s=this.xmlDoc.xml;
s=nitobi.xml.transformToString(this.xmlDoc,this.sortXslProc,"xml");
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,s);
this.dataCache.insert(0,rows-1);
if(this.mode=="local"){
this.setRowCountKnown(true);
}
}
this.setRemoteRowCount(rows);
this.fire("DataInitalized");
};
nitobi.data.DataTable.prototype.initializeXmlData=function(oXml){
var sXml=oXml;
if(typeof (oXml)=="object"){
sXml=oXml.xml;
}
sXml=sXml.replace(/fieldnames=/g,"FieldNames=").replace(/keys=/g,"Keys=");
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,sXml);
this.datastructure=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"datasourcestructure");
};
nitobi.data.DataTable.prototype.replaceData=function(oXml){
this.initializeXmlData(oXml);
var _57b=this.datastructure.getAttribute("FieldNames");
var keys=this.datastructure.getAttribute("Keys");
var _57d=this.datastructure.getAttribute("Defaults");
var _57e=this.datastructure.getAttribute("Types");
this.initializeColumns(_57b,keys,_57e,_57d);
};
nitobi.data.DataTable.prototype.initializeSchema=function(){
var _57f=this.columns.join("|");
var keys=this.keys.join("|");
var _581=this.defaults.join("|");
var _582=this.types.join("|");
this.dataCache.flush();
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,nitobi.data.DataTable.DEFAULT_DATA.replace(/\{id\}/g,this.id).replace(/\{fields\}/g,_57f).replace(/\{keys\}/g,keys).replace(/\{defaults\}/g,_581).replace(/\{types\}/g,_582));
this.datastructure=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"datasourcestructure");
};
nitobi.data.DataTable.prototype.initializeColumns=function(_583,keys,_585,_586){
if(null!=_583){
var _587=this.columns.join("|");
if(_587==_583){
return;
}
this.columns=_583.split("|");
}
if(null!=keys){
this.keys=keys.split("|");
}
if(null!=_585){
this.types=_585.split("|");
}
if(null!=_586){
this.defaults=_586.split("|");
}
if(this.xmlDoc.documentElement==null){
this.initializeSchema();
}
this.datastructure=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"datasourcestructure");
var ds=this.datastructure;
if(_583){
ds.setAttribute("FieldNames",_583);
}
if(keys){
ds.setAttribute("Keys",keys);
}
if(_586){
ds.setAttribute("Defaults",_586);
}
if(_585){
ds.setAttribute("Types",_585);
}
this.makeFieldMap();
this.fire("ColumnsInitialized");
};
nitobi.data.DataTable.prototype.getTemplateNode=function(_589){
var _58a=null;
if(_589==null){
_589=this.defaults;
}
_58a=nitobi.xml.createElement(this.xmlDoc,"e");
for(var i=0;i<this.columns.length;i++){
var _58c=(i>25?String.fromCharCode(Math.floor(i/26)+97):"")+(String.fromCharCode(i%26+97));
if(this.defaults[i]==null){
_58a.setAttribute(_58c,"");
}else{
_58a.setAttribute(_58c,this.defaults[i]);
}
}
return _58a;
};
nitobi.data.DataTable.prototype.commitProperties=function(){
if(this.mode=="unbound"){
}
};
nitobi.data.DataTable.prototype.flush=function(){
if(this.mode=="caching"||this.mode=="paging"){
this.dataCache.flush();
}
if(this.mode!="unbound"){
this.requestCache.flush();
}
this.flushLog();
this.xmlDoc=nitobi.xml.createXmlDoc();
};
nitobi.data.DataTable.prototype.join=function(_58d,_58e,_58f,_590){
};
nitobi.data.DataTable.prototype.merge=function(xd){
};
nitobi.data.DataTable.prototype.getField=function(_592,_593){
var r=this.getRecord(_592);
var a=this.fieldMap[_593];
if(a&&r){
return r.getAttribute(a.substring(1));
}else{
return null;
}
};
nitobi.data.DataTable.prototype.getRecord=function(_596){
var data=this.xmlDoc.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e[@xi='"+_596+"']");
if(data.length==0){
return null;
}
return data[0];
};
nitobi.data.DataTable.prototype.beginBatchInsert=function(){
this.batchInsert=true;
this.batchInsertRowCount=0;
};
nitobi.data.DataTable.prototype.commitBatchInsert=function(){
this.batchInsert=false;
var _598=this.batchInsertRowCount;
this.batchInsertRowCount=0;
this.setRemoteRowCount(this.remoteRowCount+_598);
if(_598>0){
this.fire("RowInserted",_598);
}
};
nitobi.data.DataTable.prototype.createRecord=function(_599,_59a){
var xi=_59a;
this.adjustXi(parseInt(xi),1);
var data=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
var _59d=_599||this.getTemplateNode();
var _59e=nitobi.component.getUniqueId();
var _59f=_59d.cloneNode(true);
_59f.setAttribute("xi",xi);
_59f.setAttribute("xid",_59e);
_59f.setAttribute("xac","i");
if(this.onGenerateKey){
var _5a0=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasourcestructure").getAttribute("Keys").split("|");
var xml=null;
for(var j=0;j<_5a0.length;j++){
var _5a3=this.fieldMap[_5a0[j]].substring(1);
var _5a4=_59f.getAttribute(_5a3);
if(!_5a4||_5a4==""){
if(!xml){
xml=eval(this.onGenerateKey);
}
if(typeof (xml)=="string"||typeof (xml)=="number"){
_59f.setAttribute(_5a3,xml);
}else{
try{
var ck1=j%26;
var ck2=Math.floor(j/26);
var _5a7=(ck2>0?String.fromCharCode(96+ck2):"")+String.fromCharCode(97+ck1);
_59f.setAttribute(_5a3,xml.selectSingleNode("//"+nitobi.xml.nsPrefix+"e").getAttribute(_5a7));
}
catch(e){
}
}
}
}
}
data.appendChild(_59f);
if(this.log!=null){
var _5a8=_59f.cloneNode(true);
_5a8.setAttribute("xac","i");
_5a8.setAttribute("xid",_59e);
this.logData.appendChild(_5a8);
}
this.dataCache.insertIntoRange(_59a);
this.batchInsertRowCount++;
if(!this.batchInsert){
this.commitBatchInsert();
}
return _59f;
};
nitobi.data.DataTable.prototype.updateRecord=function(xi,_5aa,_5ab){
var _5ac=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@xi='"+xi+"']");
var xid=_5ac.getAttribute("xid")||"error - unknown xid";
var _5ae=(_5ac.getAttribute(_5aa)!=_5ab);
if(!_5ae){
return;
}
var _5af="";
var _5b0=_5aa;
if(_5ac.getAttribute(_5aa)==null&&this.fieldMap[_5aa]!=null){
_5b0=this.fieldMap[_5aa].substring(1);
}
_5af=_5ac.getAttribute(_5b0);
_5ac.setAttribute(_5b0,_5ab);
var _5b1="u";
var _5b2="u";
if(null==this.log){
this.flushLog();
}
var _5b3=_5ac.cloneNode(true);
_5b3.setAttribute("xac","u");
this.logData=this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
var _5b4=this.logData.selectSingleNode("./"+nitobi.xml.nsPrefix+"e[@xid='"+xid+"']");
if(null==_5b4){
this.logData.appendChild(_5b3);
_5b3.setAttribute("xid",xid);
}else{
_5b3.setAttribute("xac",_5b4.getAttribute("xac"));
this.logData.replaceChild(_5b3,_5b4);
}
if((true==this.AutoSave)){
this.save();
}
this.fire("RowUpdated",{"field":_5aa,"newValue":_5ab,"oldValue":_5af,"record":_5b3});
};
nitobi.data.DataTable.prototype.deleteRecord=function(_5b5){
var data=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
this.logData=this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
var _5b7=data.selectSingleNode("*[@xi = '"+_5b5+"']");
if(_5b7==null){
throw "Index out of bounds in delete.";
}
var xid=_5b7.getAttribute("xid");
var xDel=this.logData.selectSingleNode("*[@xid='"+xid+"']");
var sTag="";
if(xDel!=null){
sTag=xDel.getAttribute("xac");
this.logData.removeChild(xDel);
}
if(sTag!="i"){
var _5bb=_5b7.cloneNode(true);
_5bb.setAttribute("xac","d");
this.logData.appendChild(_5bb);
}
data.removeChild(_5b7);
this.adjustXi(parseInt(_5b5)+1,-1);
this.dataCache.removeFromRange(_5b5);
this.setRemoteRowCount(this.remoteRowCount-1);
this.fire("RowDeleted");
};
nitobi.data.DataTable.prototype.adjustXi=function(_5bc,_5bd){
nitobi.data.adjustXiXslProc.addParameter("startingIndex",_5bc,"");
nitobi.data.adjustXiXslProc.addParameter("adjustment",_5bd,"");
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,nitobi.xml.transformToString(this.xmlDoc,nitobi.data.adjustXiXslProc,"xml"));
if(this.log!=null){
this.log=nitobi.xml.loadXml(this.log,nitobi.xml.transformToString(this.log,nitobi.data.adjustXiXslProc,"xml"));
this.logData=this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
}
};
nitobi.data.DataTable.prototype.setGetHandler=function(val){
this.getHandler=val;
for(var name in this.getHandlerArgs){
this.setGetHandlerParameter(name,this.getHandlerArgs[name]);
}
};
nitobi.data.DataTable.prototype.getGetHandler=function(){
return this.getHandler;
};
nitobi.data.DataTable.prototype.setSaveHandler=function(val){
this.postHandler=val;
for(var name in this.saveHandlerArgs){
this.setSaveHandlerParameter(name,this.saveHandlerArgs[name]);
}
};
nitobi.data.DataTable.prototype.getSaveHandler=function(){
return this.postHandler;
};
nitobi.data.DataTable.prototype.save=function(_5c2,_5c3){
if(!eval(_5c3||"true")){
return;
}
try{
if(this.version==2.8){
var _5c4=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasourcestructure").getAttribute("FieldNames").split("|");
var _5c5=this.log.selectNodes("//"+nitobi.xml.nsPrefix+"e[@xac = 'i']");
for(var i=0;i<_5c5.length;i++){
for(var j=0;j<_5c4.length;j++){
var _5c8=_5c5[i].getAttribute(this.fieldMap[_5c4[j]].substring(1));
if(!_5c8){
_5c5[i].setAttribute(this.fieldMap[_5c4[j]].substring(1),"");
}
}
}
var _5c9=this.log.selectNodes("//"+nitobi.xml.nsPrefix+"e[@xac = 'u']");
for(var i=0;i<_5c9.length;i++){
for(var j=0;j<_5c4.length;j++){
var _5c8=_5c9[i].getAttribute(this.fieldMap[_5c4[j]].substring(1));
if(!_5c8){
_5c9[i].setAttribute(this.fieldMap[_5c4[j]].substring(1),"");
}
}
}
nitobi.data.updategramTranslatorXslProc.addParameter("xkField",this.fieldMap["_xk"].substring(1),"");
nitobi.data.updategramTranslatorXslProc.addParameter("fields",_5c4.join("|").replace(/\|_xk/,""));
this.log=nitobi.xml.transformToXml(this.log,nitobi.data.updategramTranslatorXslProc);
}
var _5ca=this.getSaveHandler();
(_5ca.indexOf("?")==-1)?_5ca+="?":_5ca+="&";
_5ca+="TableId="+this.id;
_5ca+="&uid="+(new Date().getTime());
var _5cb=this.ajaxCallbackPool.reserve();
_5cb.handler=_5ca;
_5cb.responseType="xml";
_5cb.context=this;
_5cb.completeCallback=nitobi.lang.close(this,this.saveComplete);
_5cb.params=new nitobi.data.SaveCompleteEventArgs(_5c2);
if(this.version>2.8&&this.log.selectNodes("//"+nitobi.xml.nsPrefix+"e[@xac='i']").length>0&&this.isAutoKeyEnabled()){
_5cb.async=false;
}
if(this.log.documentElement.nodeName=="root"){
this.log=nitobi.xml.loadXml(this.log,this.log.xml.replace(/xmlns:ntb=\"http:\/\/www.nitobi.com\"/g,""));
var _5c4=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasourcestructure").getAttribute("FieldNames").split("|");
_5c4.splice(_5c4.length-1,1);
_5c4=_5c4.join("|");
this.log.documentElement.setAttribute("fields",_5c4);
this.log.documentElement.setAttribute("keys",_5c4);
}
if(this.isAutoKeyEnabled()&&this.version<3){
alert("AutoKey is not supported in this schema version. You must upgrade to Nitobi Grid Xml Schema version 3 or greater.");
}
_5cb.post(this.log);
this.flushLog();
}
catch(err){
throw err;
}
};
nitobi.data.DataTable.prototype.flushLog=function(){
this.log=nitobi.xml.createXmlDoc(nitobi.data.DataTable.DEFAULT_LOG.replace(/\{id\}/g,this.id).replace(/\{fields\}/g,this.columns).replace(/\{keys\}/g,this.keys).replace(/\{defaults\}/g,this.defaults).replace(/\{types\}/g,this.types));
this.logData=this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
};
nitobi.data.DataTable.prototype.updateAutoKeys=function(_5cc){
try{
var _5cd=_5cc.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e[@xac='i']");
if(typeof (_5cd)=="undefined"||_5cd==null){
nitobi.lang.throwError("When updating keys from the server for AutoKey support, the inserts could not be parsed.");
}
var keys=_5cc.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"datasourcestructure")[0].getAttribute("keys").split("|");
if(typeof (keys)=="undefined"||keys==null||keys.length==0){
nitobi.lang.throwError("When updating keys from the server for AutoKey support, no keys could be found. Ensure that the keys are sent in the request response.");
}
for(var i=0;i<_5cd.length;i++){
var _5d0=this.getRecord(_5cd[i].getAttribute("xi"));
for(var j=0;j<keys.length;j++){
var att=this.fieldMap[keys[j]].substring(1);
_5d0.setAttribute(att,_5cd[i].getAttribute(att));
}
}
}
catch(err){
nitobi.lang.throwError("When updating keys from the server for AutoKey support, the inserts could not be parsed.",err);
}
};
nitobi.data.DataTable.prototype.saveComplete=function(_5d3){
var xd=_5d3.response;
var _5d3=_5d3.params;
try{
if(this.isAutoKeyEnabled()&&this.version>2.8){
this.updateAutoKeys(xd);
}
if(this.version==2.8&&!this.onGenerateKey){
var rows=xd.selectNodes("//insert");
for(var i=0;i<rows.length;i++){
var xk=rows[i].getAttribute("xk");
if(xk!=null){
var _5d8=this.findWithoutMap("xid",rows[i].getAttribute("xid"))[0];
var key=this.fieldMap["_xk"].substring(1);
_5d8.setAttribute(key,xk);
}
}
}
if(null!=_5d3.result){
}
var node=xd.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource")||xd.selectSingleNode("/root");
var e=null;
if(node){
e=node.getAttribute("error");
}
if(e){
this.setHandlerError(e);
}else{
this.setHandlerError(null);
}
var _5dc=new nitobi.data.OnAfterSaveEventArgs(this,xd);
_5d3.callback.call(this,_5dc);
}
catch(err){
ebaErrorReport(err,"",EBA_ERROR);
}
};
nitobi.data.DataTable.prototype.makeFieldMap=function(){
var _5dd=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource");
var cf=0;
var ck=0;
this.fieldMap=new Array();
var cF=this.columns.length;
for(var i=0;i<cF;i++){
var _5e2=this.columns[i];
ck1=ck%26;
ck2=Math.floor(ck/26);
this.fieldMap[_5e2]="@"+(ck2>0?String.fromCharCode(96+ck2):"")+String.fromCharCode(97+ck1);
ck++;
}
};
nitobi.data.DataTable.prototype.find=function(_5e3,_5e4){
var _5e5=this.fieldMap[_5e3];
if(_5e5){
return this.findWithoutMap(_5e5,_5e4);
}else{
return new Array();
}
};
nitobi.data.DataTable.prototype.findWithoutMap=function(_5e6,_5e7){
if(_5e6.charAt(0)!="@"){
_5e6="@"+_5e6;
}
return this.xmlDoc.selectNodes("//"+nitobi.xml.nsPrefix+"e["+_5e6+"=\""+_5e7+"\"]");
};
nitobi.data.DataTable.prototype.sort=function(_5e8,dir,type,_5eb){
if(_5eb){
_5e8=this.fieldMap[_5e8];
_5e8=_5e8.substring(1);
dir=(dir=="Desc")?"descending":"ascending";
type=(type=="number")?"number":"text";
this.sortXslProc.addParameter("column",_5e8,"");
this.sortXslProc.addParameter("dir",dir,"");
this.sortXslProc.addParameter("type",type,"");
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,nitobi.xml.transformToString(this.xmlDoc,this.sortXslProc,"xml"));
this.fire("DataSorted");
}else{
this.sortColumn=_5e8;
this.sortDir=dir||"Asc";
}
};
nitobi.data.DataTable.prototype.syncRowCount=function(){
this.setRemoteRowCount(this.descriptor.estimatedRowCount);
};
nitobi.data.DataTable.prototype.setRemoteRowCount=function(rows){
var _5ed=this.remoteRowCount;
this.remoteRowCount=rows;
if(this.remoteRowCount!=_5ed){
this.fire("RowCountChanged",rows);
}
};
nitobi.data.DataTable.prototype.getRemoteRowCount=function(){
return this.remoteRowCount;
};
nitobi.data.DataTable.prototype.getRows=function(){
return this.xmlDoc.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e").length;
};
nitobi.data.DataTable.prototype.getXmlDoc=function(){
return this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']");
};
nitobi.data.DataTable.prototype.getRowNodes=function(){
return this.xmlDoc.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e");
};
nitobi.data.DataTable.prototype.getColumns=function(){
return this.fieldMap.length;
};
nitobi.data.DataTable.prototype.setGetHandlerParameter=function(name,_5ef){
if(this.getHandler!=null&&this.getHandler!=""){
this.getHandler=nitobi.html.setUrlParameter(this.getHandler,name,_5ef);
}
this.getHandlerArgs[name]=_5ef;
};
nitobi.data.DataTable.prototype.setSaveHandlerParameter=function(name,_5f1){
if(this.postHandler!=null&&this.postHandler!=""){
this.postHandler=nitobi.html.setUrlParameter(this.getSaveHandler(),name,_5f1);
}
this.saveHandlerArgs[name]=_5f1;
};
nitobi.data.DataTable.prototype.getChangeLogSize=function(){
if(null==this.log){
return 0;
}
return this.log.selectNodes("//"+nitobi.xml.nsPrefix+"e").length;
};
nitobi.data.DataTable.prototype.getChangeLogXmlDoc=function(){
return this.log;
};
nitobi.data.DataTable.prototype.getDataXmlDoc=function(){
return this.xmlDoc;
};
nitobi.data.DataTable.prototype.dispose=function(){
this.flush();
this.ajaxCallbackPool.context=null;
for(var item in this){
if(this[item]!=null&&this[item].dispose instanceof Function){
this[item].dispose();
}
this[item]=null;
}
};
nitobi.data.DataTable.prototype.getTable=function(_5f3,_5f4,_5f5){
this.errorCallback=_5f5;
var _5f6=this.ajaxCallbackPool.reserve();
var _5f7=this.getGetHandler();
_5f6.handler=_5f7;
_5f6.responseType="xml";
_5f6.context=this;
_5f6.completeCallback=nitobi.lang.close(this,this.getComplete);
_5f6.async=this.async;
_5f6.params=new nitobi.data.GetCompleteEventArgs(null,null,0,null,_5f6,this,_5f3,_5f4);
if(typeof (_5f4)!="function"||this.async==false){
_5f6.async=false;
return this.getComplete({"response":_5f6.get(),"params":_5f6.params});
}else{
_5f6.get();
}
};
nitobi.data.DataTable.prototype.getComplete=function(_5f8){
var xd=_5f8.response;
var _5fa=_5f8.params;
if(this.mode!="caching"){
this.xmlDoc=nitobi.xml.createXmlDoc();
}
if(null==xd||null==xd.xml||""==xd.xml){
var _5fb="No parse error.";
if(nitobi.xml.hasParseError(xd)){
if(xd==null){
_5fb="Blank Response was Given";
}else{
_5fb=nitobi.xml.getParseErrorReason(xd);
}
}
if(this.errorCallback){
this.errorCallback.call(this.context);
}
this.fire("DataReady",_5fa);
return _5fa;
}else{
if(typeof (this.successCallback)=="function"){
this.successCallback.call(this.context);
}
}
if(!this.configured){
this.configureFromData(xd);
}
xd=this.parseResponse(xd,_5fa);
xd=this.assignRowIds(xd);
var _5fc=null;
_5fc=xd.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e");
var _5fd;
var _5fe=_5fc.length;
if(_5fa.pageSize==null){
_5fa.pageSize=_5fe;
_5fa.lastRow=_5fa.startXi+_5fa.pageSize-1;
_5fa.firstRow=_5fa.startXi;
}
if(0!=_5fe){
_5fd=parseInt(_5fc[_5fc.length-1].getAttribute("xi"));
if(this.mode=="paging"){
this.dataCache.insert(0,_5fa.pageSize-1);
}else{
this.dataCache.insert(_5fa.firstRow,_5fd);
}
}else{
_5fd=-1;
_5fa.pageSize=0;
var pct=this.descriptor.lastKnownRow/this.descriptor.estimatedRowCount||0;
this.fire("PastEndOfData",pct);
}
_5fa.numRowsReturned=_5fe;
_5fa.lastRowReturned=_5fd;
var _600=_5fa.startXi;
var _601=_5fa.pageSize;
if(!isNaN(_600)&&!isNaN(_601)){
this.requestCache.remove(_600,_600+_601-1);
}
if(this.mode!="caching"){
this.replaceData(xd);
}else{
this.mergeData(xd);
}
this.updateFromDescriptor(_5fa);
this.fire("RowCountReady",_5fa);
if(null!=_5fa.ajaxCallback){
this.ajaxCallbackPool.release(_5fa.ajaxCallback);
}
this.executeRequests();
var node=xd.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource");
var e=null;
if(node){
e=node.getAttribute("error");
}
if(e){
this.setHandlerError(e);
}else{
this.setHandlerError(null);
}
this.fire("DataReady",_5fa);
if(null!=_5fa.callback&&null!=_5fa.context){
_5fa.callback.call(_5fa.context,_5fa);
_5fa.dispose();
_5fa=null;
}else{
return _5fa;
}
};
nitobi.data.DataTable.prototype.executeRequests=function(){
var _604=this.requestQueue;
this.requestQueue=new Array();
for(var i=0;i<_604.length;i++){
_604[i].call();
}
};
nitobi.data.DataTable.prototype.updateFromDescriptor=function(_606){
this.descriptor.update(_606);
if(this.mode=="paging"){
this.setRemoteRowCount(_606.numRowsReturned);
}else{
this.setRemoteRowCount(this.descriptor.estimatedRowCount);
}
this.setRowCountKnown(this.descriptor.isAtEndOfTable);
};
nitobi.data.DataTable.prototype.setRowCountKnown=function(_607){
var _608=this.rowCountKnown;
this.rowCountKnown=_607;
if(_607&&this.rowCountKnown!=_608){
this.fire("RowCountKnown",this.remoteRowCount);
}
};
nitobi.data.DataTable.prototype.getRowCountKnown=function(){
return this.rowCountKnown;
};
nitobi.data.DataTable.prototype.configureFromData=function(xd){
this.version=this.inferDataVersion(xd);
if(this.mode=="unbound"){
}
if(this.mode=="static"){
}
if(this.mode=="paging"){
}
if(this.mode=="caching"){
}
};
nitobi.data.DataTable.prototype.mergeData=function(xd){
if(this.xmlDoc.xml==""){
this.initializeXml(xd);
return;
}
var _60b=xd.selectNodes("//"+nitobi.xml.nsPrefix+"datasource[@id = '"+this.id+"']//"+nitobi.xml.nsPrefix+"e");
var _60c=this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data");
var len=_60b.length;
for(var i=0;i<len;i++){
if(this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e[@xi='"+_60b[i].getAttribute("xi")+"']")){
continue;
}
_60c.appendChild(_60b[i]);
}
};
nitobi.data.DataTable.prototype.assignRowIds=function(xd){
nitobi.data.addXidXslProc.addParameter("guid",nitobi.component.getUniqueId(),"");
var doc=nitobi.xml.loadXml(xd,nitobi.xml.transformToString(xd,nitobi.data.addXidXslProc,"xml"));
return doc;
};
nitobi.data.DataTable.prototype.inferDataVersion=function(xd){
if(xd.selectSingleNode("/root")){
return 2.8;
}
return 3;
};
nitobi.data.DataTable.prototype.parseResponse=function(xd,_613){
if(this.version==2.8){
return this.parseLegacyResponse(xd,_613);
}else{
return this.parseStructuredResponse(xd,_613);
}
};
nitobi.data.DataTable.prototype.parseLegacyResponse=function(xd,_615){
var _616=this.mode=="paging"?0:_615.startXi;
nitobi.data.dataTranslatorXslProc.addParameter("start",_616,"");
nitobi.data.dataTranslatorXslProc.addParameter("id",this.id,"");
var _617=xd.selectSingleNode("/root").getAttribute("fields");
var _618=_617.split("|");
var i=_618.length;
var _61a=(i>25?String.fromCharCode(Math.floor(i/26)+96):"")+(String.fromCharCode(i%26+97));
nitobi.data.dataTranslatorXslProc.addParameter("xkField",_61a,"");
xd=nitobi.xml.transformToXml(xd,nitobi.data.dataTranslatorXslProc);
return xd;
};
nitobi.data.DataTable.prototype.parseStructuredResponse=function(xd,_61c){
xd=nitobi.xml.loadXml(xd,"<ntb:grid xmlns:ntb=\"http://www.nitobi.com\"><ntb:datasources>"+xd.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']").xml+"</ntb:datasources></ntb:grid>");
var _61d=xd.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.id+"']/"+nitobi.xml.nsPrefix+"data/"+nitobi.xml.nsPrefix+"e");
var _61e=this.mode=="paging"?0:_61c.startXi;
if(_61d){
if(_61d.getAttribute("xi")!=_61e){
nitobi.data.adjustXiXslProc.addParameter("startingIndex","0","");
nitobi.data.adjustXiXslProc.addParameter("adjustment",_61e,"");
xd=nitobi.xml.loadXml(xd,nitobi.xml.transformToString(xd,nitobi.data.adjustXiXslProc,"xml"));
}
}
return xd;
};
nitobi.data.DataTable.prototype.forceGet=function(_61f,_620,_621,_622,_623,_624){
this.errorCallback=_623;
this.successCallback=_624;
this.context=_621;
var _625=this.getGetHandler();
(_625.indexOf("?")==-1)?_625+="?":_625+="&";
_625+="StartRecordIndex=0&start=0&PageSize="+_620+"&SortColumn="+(this.sortColumn||"")+"&SortDirection="+this.sortDir+"&TableId="+this.id+"&uid="+(new Date().getTime());
var _626=this.ajaxCallbackPool.reserve();
_626.handler=_625;
_626.responseType="xml";
_626.context=this;
_626.completeCallback=nitobi.lang.close(this,this.getComplete);
_626.params=new nitobi.data.GetCompleteEventArgs(0,_620-1,0,_620,_626,this,_621,_622);
_626.get();
return;
};
nitobi.data.DataTable.prototype.getPage=function(_627,_628,_629,_62a,_62b,_62c){
var _62d=_627+_628-1;
var _62e=this.dataCache.gaps(0,_628-1);
var _62f=_62e.length;
if(_62f){
var _630=this.requestCache.gaps(_627,_62d);
if(_630.length==0){
var _631=nitobi.lang.close(this,this.get,arguments);
this.requestQueue.push(_631);
return;
}
this.getFromServer(_627,_62d,_627,_62d,_629,_62a,_62b);
}else{
this.getFromCache(_627,_628,_629,_62a,_62b);
}
};
nitobi.data.DataTable.prototype.get=function(_632,_633,_634,_635,_636){
this.errorCallback=_636;
var _637=null;
if(this.mode=="caching"){
_637=this.getCached(_632,_633,_634,_635,_636);
}
if(this.mode=="local"||this.mode=="static"){
_637=this.getTable(_634,_635,_636);
}
if(this.mode=="paging"){
_637=this.getPage(_632,_633,_634,_635,_636);
}
return _637;
};
nitobi.data.DataTable.prototype.inCache=function(_638,_639){
if(this.mode=="local"){
return true;
}
var _63a=_638,_63b=_638+_639-1;
var _63c=this.getRemoteRowCount()-1;
if(this.getRowCountKnown()&&_63c<_63b){
_63b=_63c;
}
var _63d=this.dataCache.gaps(_63a,_63b);
var _63e=_63d.length;
return !(_63e>0);
};
nitobi.data.DataTable.prototype.cachedRanges=function(_63f,_640){
return this.dataCache.ranges(_63f,_640);
};
nitobi.data.DataTable.prototype.getCached=function(_641,_642,_643,_644,_645,_646){
if(_642==null){
return this.getFromServer(_647,null,_641,null,_643,_644,_645);
}
var _647=_641,_648=_641+_642-1;
var _649=this.dataCache.gaps(_647,_648);
var _64a=_649.length;
if(this.mode!="unbound"&&_64a>0){
var low=_649[_64a-1].low;
var high=_649[_64a-1].high;
var _64d=this.requestCache.gaps(low,high);
if(_64d.length==0){
var _64e=nitobi.lang.close(this,this.get,arguments);
this.requestQueue.push(_64e);
return;
}
return this.getFromServer(_647,_648,low,high,_643,_644,_645);
}else{
this.getFromCache(_641,_642,_643,_644,_645);
}
};
nitobi.data.DataTable.prototype.getFromServer=function(_64f,_650,low,high,_653,_654,_655){
this.requestCache.insert(low,high);
var _656=(_650==null?null:(high-low+1));
var _657=(_656==null?"":_656);
var _658=this.getGetHandler();
(_658.indexOf("?")==-1)?_658+="?":_658+="&";
_658+="StartRecordIndex="+low+"&start="+low+"&PageSize="+(_657)+"&SortColumn="+(this.sortColumn||"")+"&SortDirection="+this.sortDir+"&uid="+(new Date().getTime());
var _659=this.ajaxCallbackPool.reserve();
_659.handler=_658;
_659.responseType="xml";
_659.context=this;
_659.completeCallback=nitobi.lang.close(this,this.getComplete);
_659.async=this.async;
_659.params=new nitobi.data.GetCompleteEventArgs(_64f,_650,low,_656,_659,this,_653,_654);
return _659.get();
};
nitobi.data.DataTable.prototype.getFromCache=function(_65a,_65b,_65c,_65d,_65e){
var _65f=_65a,_660=_65a+_65b-1;
if(_65f>0||_660>0){
if(typeof (_65d)=="function"){
var _661=new nitobi.data.GetCompleteEventArgs(_65f,_660,_65f,_660-_65f+1,null,this,_65c,_65d);
_661.callback.call(_661.context,_661);
}
}
};
nitobi.data.DataTable.prototype.mergeFromXml=function(_662,_663){
var _664=Number(_662.documentElement.firstChild.getAttribute("xi"));
var _665=Number(_662.documentElement.lastChild.getAttribute("xi"));
var _666=this.dataCache.gaps(_664,_665);
if(this.mode=="local"&&_666.length==1){
this.dataCache.insert(_666[0].low,_666[0].high);
this.mergeFromXmlGetComplete(_662,_663,_664,_665);
this.batchInsertRowCount=(_666[0].high-_666[0].low+1);
this.commitBatchInsert();
return;
}
if(_666.length==0){
this.mergeFromXmlGetComplete(_662,_663,_664,_665);
}else{
if(_666.length==1){
this.get(_666[0].low,_666[0].high-_666[0].low+1,this,nitobi.lang.close(this,this.mergeFromXmlGetComplete,[_662,_663,_664,_665]));
}else{
this.forceGet(_664,_665,this,nitobi.lang.close(this,this.mergeFromXmlGetComplete,[_662,_663,_664,_665]));
}
}
};
nitobi.data.DataTable.prototype.mergeFromXmlGetComplete=function(_667,_668,_669,_66a){
var _66b=nitobi.xml.createElement(this.xmlDoc,"newdata");
this.xmlDoc.documentElement.appendChild(_66b);
_66b.appendChild(_667.documentElement.cloneNode(true));
nitobi.data.mergeEbaXmlXslProc.addParameter("startRowIndex",_669,"");
nitobi.data.mergeEbaXmlXslProc.addParameter("endRowIndex",_66a,"");
nitobi.data.mergeEbaXmlXslProc.addParameter("guid",nitobi.component.getUniqueId(),"");
this.xmlDoc=nitobi.xml.loadXml(this.xmlDoc,nitobi.xml.transformToString(this.xmlDoc,nitobi.data.mergeEbaXmlXslProc,"xml"));
_66b=nitobi.xml.createElement(this.log,"newdata");
this.log.documentElement.appendChild(_66b);
_66b.appendChild(this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"newdata").firstChild.cloneNode(true));
this.log=nitobi.xml.loadXml(this.log,nitobi.xml.transformToString(this.log,nitobi.data.mergeEbaXmlToLogXslProc,"xml"));
this.xmlDoc.documentElement.removeChild(this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"newdata"));
this.log.documentElement.removeChild(this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"newdata"));
_668.call();
};
nitobi.data.DataTable.prototype.fillColumn=function(_66c,_66d){
nitobi.data.fillColumnXslProc.addParameter("column",this.fieldMap[_66c].substring(1));
nitobi.data.fillColumnXslProc.addParameter("value",_66d);
this.xmlDoc.loadXML(nitobi.xml.transformToString(this.xmlDoc,nitobi.data.fillColumnXslProc,"xml"));
var _66e=parseFloat((new Date()).getTime());
var _66f=nitobi.xml.createElement(this.log,"newdata");
this.log.documentElement.appendChild(_66f);
_66f.appendChild(this.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"data").cloneNode(true));
nitobi.data.mergeEbaXmlToLogXslProc.addParameter("defaultAction","u");
this.log.loadXML(nitobi.xml.transformToString(this.log,nitobi.data.mergeEbaXmlToLogXslProc,"xml"));
nitobi.data.mergeEbaXmlToLogXslProc.addParameter("defaultAction","");
this.log.documentElement.removeChild(this.log.selectSingleNode("//"+nitobi.xml.nsPrefix+"newdata"));
};
nitobi.data.DataTable.prototype.setHandlerError=function(_670){
this.handlerError=_670;
};
nitobi.data.DataTable.prototype.getHandlerError=function(){
return this.handlerError;
};
nitobi.data.DataTable.prototype.dispose=function(){
this.sortXslProc=null;
this.requestQueue=null;
this.fieldMap=null;
};
nitobi.data.DataTable.prototype.fire=function(evt,args){
return nitobi.event.notify(evt+this.uid,args);
};
nitobi.data.DataTable.prototype.subscribe=function(evt,func,_675){
if(typeof (_675)=="undefined"){
_675=this;
}
return nitobi.event.subscribe(evt+this.uid,nitobi.lang.close(_675,func));
};
nitobi.lang.defineNs("nitobi.data");
nitobi.data.DataTableDescriptor=function(_676,_677,_678){
this.disposal=[];
this.estimatedRowCount=0;
this.leapMultiplier=2;
this.estimateRowCount=(_678==null?true:_678);
this.lastKnownRow=0;
this.isAtEndOfTable=false;
this.table=_676;
this.lowestEmptyRow=0;
this.tableProjectionUpdatedEvent=_677;
this.disposal.push(this.tableProjectionUpdatedEvent);
};
nitobi.data.DataTableDescriptor.prototype.startPeek=function(){
this.enablePeek=true;
this.peek();
};
nitobi.data.DataTableDescriptor.prototype.peek=function(){
var _679;
if(this.lowestEmptyRow>0){
var _67a=this.lowestEmptyRow-this.lastKnownRow;
_679=this.lastKnownRow+Math.round(_67a/2);
}else{
_679=(this.estimatedRowCount*this.leapMultiplier);
}
this.table.get(Math.round(_679),1,this,this.peekComplete);
};
nitobi.data.DataTableDescriptor.prototype.peekComplete=function(_67b){
if(this.enablePeek){
window.setTimeout(nitobi.lang.close(this,this.peek),1000);
}
};
nitobi.data.DataTableDescriptor.prototype.stopPeek=function(){
this.enablePeek=false;
};
nitobi.data.DataTableDescriptor.prototype.leap=function(_67c,_67d){
if(this.lowestEmptyRow>0){
var _67e=this.lowestEmptyRow-this.lastKnownRow;
this.estimatedRowCount=this.lastKnownRow+Math.round(_67e/2);
}else{
if(_67c==null||_67d==null){
this.estimatedRowCount=0;
}else{
if(this.estimateRowCount){
this.estimatedRowCount=(this.estimatedRowCount*_67c)+_67d;
}
}
}
this.fireProjectionUpdatedEvent();
};
nitobi.data.DataTableDescriptor.prototype.update=function(_67f,_680){
if(null==_680){
_680=false;
}
if(this.isAtEndOfTable&&!_680){
return false;
}
var _681=(_67f!=null&&_67f.numRowsReturned==0&&_67f.startXi==0);
var _682=(_67f!=null&&_67f.lastRow!=_67f.lastRowReturned);
if(null==_67f){
_67f={lastPage:false,pageSize:1,firstRow:0,lastRow:0,startXi:0};
}
var _683=(_681)||(_682)||(this.isAtEndOfTable)||((this.lastKnownRow==this.estimatedRowCount-1)&&(this.estimatedRowCount==this.lowestEmptyRow));
if(_67f.pageSize==0&&!_683){
this.lowestEmptyRow=this.lowestEmptyRow>0?Math.min(_67f.startXi,this.lowestEmptyRow):_67f.startXi;
this.leap();
return true;
}
this.lastKnownRow=Math.max(_67f.lastRowReturned,this.lastKnownRow);
if(_683&&!_680){
if(_67f.lastRowReturned>=0){
this.estimatedRowCount=_67f.lastRowReturned+1;
this.isAtEndOfTable=true;
}else{
if(_681){
this.estimatedRowCount=0;
this.isAtEndOfTable=true;
}else{
this.estimatedRowCount=this.lastKnownRow+Math.ceil((_67f.lastRow-this.lastKnownRow)/2);
}
}
this.fireProjectionUpdatedEvent();
this.stopPeek();
return true;
}
if(!this.estimateRowCount){
this.estimatedRowCount=this.lastKnownRow+1;
}
if(this.estimatedRowCount==0){
this.estimatedRowCount=(_67f.lastRow+1)*(this.estimateRowCount?2:1);
}
if((this.estimatedRowCount>(_67f.lastRow+1)&&!_680)||!this.estimateRowCount){
return false;
}
if(!this.isAtEndOfTable){
this.leap(this.leapMultiplier,0);
return true;
}
return false;
};
nitobi.data.DataTableDescriptor.prototype.reset=function(){
this.estimatedRowCount=0;
this.leapMultiplier=2;
this.lastKnownRow=0;
this.isAtEndOfTable=false;
this.lowestEmptyRow=0;
this.fireProjectionUpdatedEvent();
};
nitobi.data.DataTableDescriptor.prototype.fireProjectionUpdatedEvent=function(_684){
if(this.tableProjectionUpdatedEvent!=null){
this.tableProjectionUpdatedEvent(_684);
}
};
nitobi.data.DataTableDescriptor.prototype.dispose=function(){
nitobi.lang.dispose(this,this.disposal);
};
nitobi.lang.defineNs("nitobi.data");
if(false){
nitobi.data=function(){
};
}
nitobi.data.DataTableEventArgs=function(_685){
this.source=_685;
this.event=nitobi.html.Event;
};
nitobi.data.DataTableEventArgs.prototype.getSource=function(){
return this.source;
};
nitobi.data.DataTableEventArgs.prototype.getEvent=function(){
return this.event;
};
nitobi.data.GetCompleteEventArgs=function(_686,_687,_688,_689,_68a,_68b,obj,_68d){
this.firstRow=_686;
this.lastRow=_687;
this.callback=_68d;
this.dataSource=_68b;
this.context=obj;
this.ajaxCallback=_68a;
this.startXi=_688;
this.pageSize=_689;
this.lastPage=false;
this.status="success";
};
nitobi.data.GetCompleteEventArgs.prototype.dispose=function(){
this.callback=null;
this.context=null;
this.dataSource=null;
this.ajaxCallback.clear();
this.ajaxCallback==null;
};
nitobi.data.SaveCompleteEventArgs=function(_68e){
this.callback=_68e;
};
nitobi.data.SaveCompleteEventArgs.prototype.initialize=function(){
};
nitobi.data.OnAfterSaveEventArgs=function(_68f,_690,_691){
nitobi.data.OnAfterSaveEventArgs.baseConstructor.call(this,_68f);
this.success=_691;
this.responseData=_690;
};
nitobi.lang.extend(nitobi.data.OnAfterSaveEventArgs,nitobi.data.DataTableEventArgs);
nitobi.data.OnAfterSaveEventArgs.prototype.getResponseData=function(){
return this.responseData;
};
nitobi.data.OnAfterSaveEventArgs.prototype.getSuccess=function(){
return this.success;
};
nitobi.lang.defineNs("nitobi.form");
if(false){
nitobi.form=function(){
};
}
nitobi.form.Control=function(){
this.owner=null;
this.cell=null;
this.element=null;
this.blur=false;
this.onKeyUp=new nitobi.base.Event();
this.onKeyDown=new nitobi.base.Event();
this.onKeyPress=new nitobi.base.Event();
this.onChange=new nitobi.base.Event();
this.onCancel=new nitobi.base.Event();
this.onTab=new nitobi.base.Event();
this.onEnter=new nitobi.base.Event();
};
nitobi.form.Control.attachToParent=function(_692){
};
nitobi.form.Control.prototype.mimic=function(){
};
nitobi.form.Control.prototype.deactivate=function(){
if(this.blur){
return false;
}
this.blur=true;
};
nitobi.form.Control.prototype.bind=function(_693,cell){
this.owner=_693;
this.cell=cell;
this.blur=false;
};
nitobi.form.Control.prototype.hide=function(){
this.placeholder.style.left="-2000px";
};
nitobi.form.Control.prototype.show=function(){
this.placeholder.style.display="block";
};
nitobi.form.Control.prototype.focus=function(){
this.control.focus();
this.blur=false;
};
nitobi.form.Control.prototype.deactivate=function(){
};
nitobi.form.Control.prototype.handleKey=function(evt){
var k=evt.keyCode;
this.lastKeyCode=k;
if(this.onKeyDown.notify(evt)==false){
return;
}
var y=1;
var x=1;
switch(k){
case 27:
nitobi.html.detachEvent(this.control,"blur",this.deactivate);
this.hide();
this.owner.focus();
this.onCancel.notify(this);
break;
case 9:
var _699=this.deactivate();
if(_699==false){
nitobi.html.cancelBubble(evt);
break;
}
if(nitobi.browser.IE){
evt.keyCode="";
}
if(evt.shiftKey){
x=-1;
}
this.owner.move(x,0);
nitobi.html.cancelBubble(evt);
this.onTab.notify(this);
break;
case 38:
y=-1;
case 40:
case 13:
this.control.blur();
if(evt.shiftKey){
y=-1;
}
this.owner.move(0,y);
nitobi.html.cancelBubble(evt);
this.onEnter.notify(this);
break;
default:
}
};
nitobi.form.Control.prototype.handleKeyUp=function(evt){
this.onKeyUp.notify(evt);
};
nitobi.form.Control.prototype.handleKeyPress=function(evt){
this.onKeyPress.notify(evt);
};
nitobi.form.Control.prototype.handleChange=function(evt){
this.onChange.notify(evt);
};
nitobi.form.Control.prototype.render=function(){
};
nitobi.form.Control.prototype.setEditCompleteHandler=function(_69d){
this.editCompleteHandler=_69d;
};
nitobi.form.Control.prototype.eSET=function(name,args){
var _6a0=args[0];
var _6a1=_6a0;
var _6a2=name.substr(2);
_6a2=_6a2.substr(0,_6a2.length-5);
if(typeof (_6a0)=="string"){
_6a1=function(){
return nitobi.event.evaluate(_6a0,arguments[0]);
};
}
if(this[_6a2]!=null){
this[name].unSubscribe(this[_6a2]);
}
var guid=this[name].subscribe(_6a1);
this.jSET(_6a2,[guid]);
return guid;
};
nitobi.form.Control.prototype.jSET=function(name,val){
this[name]=val[0];
};
nitobi.form.Control.prototype.dispose=function(){
for(var item in this){
}
};
nitobi.form.IBlurable=function(_6a7,_6a8){
this.selfBlur=false;
this.elements=_6a7;
for(var i=0;i<this.elements.length;i++){
nitobi.html.attachEvent(this.elements[i],"mousedown",this.handleMouseDown,this);
nitobi.html.attachEvent(this.elements[i],"blur",this.handleBlur,this);
nitobi.html.attachEvent(this.elements[i],"focus",this.handleFocus,this);
nitobi.html.attachEvent(this.elements[i],"mouseup",this.handleMouseUp,this);
}
this.blurFunc=_6a8;
this.lastFocus=null;
};
nitobi.form.IBlurable.prototype.removeBlurable=function(){
for(var i=0;i<elems.length;i++){
nitobi.html.detachEvent(elems[i],"mousedown",this.handleMouseDown,this);
}
};
nitobi.form.IBlurable.prototype.handleMouseDown=function(evt){
if(this.lastFocus!=evt.srcElement){
this.selfBlur=true;
}else{
this.selfBlur=false;
}
this.lastFocus=evt.srcElement;
};
nitobi.form.IBlurable.prototype.handleBlur=function(){
if(!this.selfBlur){
this.blurFunc();
}
this.selfBlur=false;
};
nitobi.form.IBlurable.prototype.handleFocus=function(){
this.selfBlur=false;
};
nitobi.form.IBlurable.prototype.handleMouseUp=function(){
this.selfBlur=false;
};
nitobi.form.Text=function(){
nitobi.form.Text.baseConstructor.call(this);
var div=document.createElement("div");
div.innerHTML="<table border='0' cellpadding='0' cellspacing='0' class='ntbinputborder'><tr><td></td></table>";
var _6ad=div.firstChild;
_6ad.object=this;
_6ad.style.position="absolute";
_6ad.style.top="-3000px";
_6ad.style.zIndex=2000;
_6ad.style.left="-3000px";
this.placeholder=_6ad;
var _6ae=document.createElement("input");
_6ae.style.width="100%";
_6ae.style.border="0px";
_6ae.className="ntbinput ntbcell";
_6ae.setAttribute("maxlength",255);
this.control=_6ae;
this.events=[{"type":"keydown","handler":this.handleKey},{"type":"keyup","handler":this.handleKeyUp},{"type":"keypress","handler":this.handleKeyPress},{"type":"change","handler":this.handleChange},{"type":"blur","handler":this.deactivate}];
};
nitobi.lang.extend(nitobi.form.Text,nitobi.form.Control);
nitobi.form.Text.prototype.initialize=function(){
this.placeholder.rows[0].cells[0].appendChild(this.control);
document.body.appendChild(this.placeholder);
nitobi.html.attachEvents(this.control,this.events,this);
};
nitobi.form.Text.prototype.attachToParent=function(_6af){
_6af.appendChild(this.placeholder);
};
nitobi.form.Text.prototype.bind=function(_6b0,cell,_6b2){
nitobi.form.Text.base.bind.apply(this,arguments);
if(_6b2!=null&&_6b2!=""){
this.control.value=_6b2;
}else{
this.control.value=cell.getValue();
}
var _6b3=this.cell.getColumnObject().ModelNode;
this.eSET("onKeyPress",[_6b3.getAttribute("OnKeyPressEvent")]);
this.eSET("onKeyDown",[_6b3.getAttribute("OnKeyDownEvent")]);
this.eSET("onKeyUp",[_6b3.getAttribute("OnKeyUpEvent")]);
this.eSET("onChange",[_6b3.getAttribute("OnChangeEvent")]);
this.control.setAttribute("maxlength",_6b3.getAttribute("MaxLength"));
nitobi.html.Css.addClass(this.control,"ntbcolumndata"+this.owner.uid+"_"+(this.cell.getColumn()+1));
};
nitobi.form.Text.prototype.render=function(){
this.domNode.appendChild(this.placeholder);
};
nitobi.form.Text.prototype.mimic=function(){
var oY=0;
var oX=0;
if(nitobi.browser.MOZ){
var _6b6=this.context.getScrollSurface();
var _6b7=this.context.getActiveView().region;
if(_6b7==3||_6b7==4){
oY=_6b6.scrollTop-nitobi.form.EDITOR_OFFSETY;
}
if(_6b7==1||_6b7==4){
oX=_6b6.scrollLeft-nitobi.form.EDITOR_OFFSETX;
}
}
nitobi.drawing.align(this.placeholder,this.cell.getDomNode(),286265344,1,1,-oY-1,-oX-1);
if(document.compatMode=="CSS1Compat"){
var Css=nitobi.html.Css;
var _6b9=Css.getClass(".ntbinput");
var dw=(parseInt(_6b9.paddingLeft)+parseInt(_6b9.paddingRight));
this.control.style.width=(this.placeholder.offsetWidth-2*dw)+"px";
var dh=(parseInt(_6b9.paddingTop)+parseInt(_6b9.paddingBottom));
this.control.style.height=(this.placeholder.offsetHeight-2*dh)+"px";
}
window.setTimeout(nitobi.lang.close(this,this.focus),10);
if(this.control.createTextRange){
var _6bc=this.control.createTextRange();
_6bc.collapse(false);
_6bc.select();
}
};
nitobi.form.Text.prototype.focus=function(){
this.control.focus();
};
nitobi.form.Text.prototype.deactivate=function(){
if(this.lastKeyCode==27){
return;
}
if(nitobi.form.Text.base.deactivate.apply(this,arguments)==false){
return;
}
var _6bd=this.control.value;
nitobi.html.Css.removeClass(this.control,"ntbcolumndata"+this.owner.uid+"_"+(this.cell.getColumn()+1));
if(this.editCompleteHandler!=null){
var _6be=new nitobi.grid.EditCompleteEventArgs(this,_6bd,_6bd,this.cell);
var _6bf=this.editCompleteHandler.call(this.owner,_6be);
if(!_6bf){
this.blur=false;
}
return _6bf;
}
};
nitobi.form.Text.prototype.dispose=function(){
this.control.object=null;
nitobi.html.detachEvents(this.control,this.events);
var _6c0=this.placeholder.parentNode;
_6c0.removeChild(this.placeholder);
this.domNode=null;
this.control=null;
this.owner=null;
this.cell=null;
};
nitobi.form.Checkbox=function(){
};
nitobi.lang.extend(nitobi.form.Checkbox,nitobi.form.Control);
nitobi.form.Checkbox.prototype.initialize=function(){
this.DataSourceId="";
this.UnCheckedValue="0";
this.CheckedValue="1";
this.DisplayFields="";
this.ValueField="";
};
nitobi.form.Checkbox.prototype.bind=function(_6c1,cell,_6c3){
this.blur=false;
this.cell=cell;
this.owner=_6c1;
var _6c4=this.cell.getColumnObject();
this.DataSourceId=_6c4.ModelNode.getAttribute("DatasourceId");
this.dataTable=this.owner.data.getTable(this.DataSourceId);
};
nitobi.form.Checkbox.prototype.mimic=function(){
if(false==eval(this.owner.getOnCellValidateEvent())){
return;
}
this.toggle();
this.deactivate();
};
nitobi.form.Checkbox.prototype.deactivate=function(){
if(this.editCompleteHandler!=null){
var _6c5=new nitobi.grid.EditCompleteEventArgs(this,this.value,this.value,this.cell);
this.editCompleteHandler.call(this.context,_6c5);
}
this.context=null;
};
nitobi.form.Checkbox.prototype.toggle=function(){
var _6c6=this.cell.getColumnObject();
this.DataSourceId=_6c6.ModelNode.getAttribute("DatasourceId");
var _6c7=this.owner.data.getTable(this.DataSourceId);
var _6c8=_6c6.ModelNode.getAttribute("DisplayFields");
var _6c9=_6c6.ModelNode.getAttribute("ValueField");
var _6ca=_6c6.ModelNode.getAttribute("CheckedValue");
if(_6ca==""||_6ca==null){
_6ca=1;
}
var _6cb=_6c6.ModelNode.getAttribute("UnCheckedValue");
if(_6cb==""||_6cb==null){
_6cb=0;
}
this.value=(this.cell.getDomNode().getAttribute("value")==_6ca)?_6cb:_6ca;
};
nitobi.form.Checkbox.prototype.hide=function(){
};
nitobi.form.Checkbox.prototype.dispose=function(){
this.element=null;
this.metadata=null;
this.owner=null;
this.context=null;
};
nitobi.form.Date=function(){
nitobi.form.Date.baseConstructor.call(this);
};
nitobi.lang.extend(nitobi.form.Date,nitobi.form.Text);
nitobi.lang.defineNs("nitobi.form");
nitobi.form.EDITOR_OFFSETX=null;
nitobi.form.EDITOR_OFFSETY=null;
nitobi.form.ControlFactory=function(){
this.editors={};
};
nitobi.form.ControlFactory.prototype.getEditor=function(_6cc,_6cd,_6ce){
var _6cf=null;
if(null==_6cd){
ebaErrorReport("getEditor: column parameter is null","",EBA_DEBUG);
return _6cf;
}
if(false==_6cd.isEditable()){
return _6cf;
}
var _6d0=_6cd.getType();
var _6d1=_6cd.getType();
var _6d2="nitobi.Grid"+_6d0+_6d1+"Editor";
if(this.editors[_6d2]!=null){
_6cf=this.editors[_6d2];
}else{
switch(_6d0){
case "LINK":
case "HYPERLINK":
_6cf=new nitobi.form.Link;
break;
case "IMAGE":
return null;
case "BUTTON":
return null;
case "LOOKUP":
_6cf=new nitobi.form.Lookup();
break;
case "LISTBOX":
_6cf=new nitobi.form.ListBox();
break;
case "PASSWORD":
_6cf=new nitobi.form.Password();
break;
case "TEXTAREA":
_6cf=new nitobi.form.TextArea();
break;
case "CHECKBOX":
_6cf=new nitobi.form.Checkbox();
break;
default:
if(_6d1=="DATE"){
if(_6cd.isCalendarEnabled()){
_6cf=new nitobi.form.Calendar();
}else{
_6cf=new nitobi.form.Date();
}
}else{
if(_6d1=="NUMBER"){
_6cf=new nitobi.form.Number();
}else{
_6cf=new nitobi.form.Text();
}
}
break;
}
}
_6cf.initialize();
_6cf.context=_6cc;
this.editors[_6d2]=_6cf;
return _6cf;
};
nitobi.form.ControlFactory.prototype.dispose=function(){
for(var _6d3 in this.editors){
this.editors[_6d3].dispose();
}
};
nitobi.form.ControlFactory.instance=new nitobi.form.ControlFactory();
nitobi.form.Link=function(){
};
nitobi.lang.extend(nitobi.form.Link,nitobi.form.Control);
nitobi.form.Link.prototype.initialize=function(){
this.url="";
};
nitobi.form.Link.prototype.bind=function(_6d4,cell,_6d6){
this.cell=cell;
this.url=this.cell.getValue();
this.blur=false;
this.owner=_6d4;
};
nitobi.form.Link.prototype.mimic=function(){
if(false==eval(this.owner.getOnCellValidateEvent())){
return;
}
this.click();
this.deactivate();
};
nitobi.form.Link.prototype.deactivate=function(){
if(this.editCompleteHandler!=null){
var _6d7=new nitobi.grid.EditCompleteEventArgs(this,this.value,this.value,this.cell);
this.editCompleteHandler.call(this.context,_6d7);
}
this.context=null;
};
nitobi.form.Link.prototype.click=function(){
window.open(this.url);
this.value=this.url;
};
nitobi.form.Link.prototype.hide=function(){
};
nitobi.form.Link.prototype.dispose=function(){
this.element=null;
this.metadata=null;
this.owner=null;
this.context=null;
};
nitobi.form.ListBox=function(){
nitobi.form.ListBox.baseConstructor.call(this);
this.editCompleteHandler=null;
this.context=null;
this.element=null;
this.metadata=null;
this.blur=false;
this.keypress=false;
this.events=[{"type":"change","handler":this.deactivate},{"type":"keydown","handler":this.handleKey},{"type":"keyup","handler":this.handleKeyUp},{"type":"keypress","handler":this.handleKeyPress},{"type":"blur","handler":this.deactivate}];
};
nitobi.lang.extend(nitobi.form.ListBox,nitobi.form.Control);
nitobi.form.ListBox.prototype.initialize=function(){
var div=document.createElement("div");
div.innerHTML="<table border='0' cellpadding='0' cellspacing='0' style='table-layout:fixed;'><tr><td></td></tr></table>";
this.placeholder=div.firstChild;
this.placeholder.object=this;
this.placeholder.style.position="absolute";
this.placeholder.style.top="-1000px";
this.placeholder.style.left="-1000px";
document.body.appendChild(this.placeholder);
};
nitobi.form.ListBox.prototype.bind=function(_6d9,cell,_6db){
this.blur=false;
this.cell=cell;
this.owner=_6d9;
var _6dc=cell.getColumnObject().ModelNode;
var _6dd=_6dc.getAttribute("DatasourceId");
this.dataTable=this.owner.data.getTable(_6dd);
this.eSET("onKeyPress",[_6dc.getAttribute("OnKeyPressEvent")]);
this.eSET("onKeyDown",[_6dc.getAttribute("OnKeyDownEvent")]);
this.eSET("onKeyUp",[_6dc.getAttribute("OnKeyUpEvent")]);
this.eSET("onChange",[_6dc.getAttribute("OnChangeEvent")]);
this.bindComplete(cell.getValue());
};
nitobi.form.ListBox.prototype.bindComplete=function(_6de){
var _6df=this.dataTable.xmlDoc.selectSingleNode("//"+nitobi.xml.nsPrefix+"datasource[@id='"+this.dataTable.id+"']");
var _6e0=this.cell.getColumnObject();
var _6e1=_6e0.ModelNode.getAttribute("DisplayFields");
var _6e2=_6e0.ModelNode.getAttribute("ValueField");
nitobi.form.listboxXslProc.addParameter("DisplayFields",_6e1,"");
nitobi.form.listboxXslProc.addParameter("ValueField",_6e2,"");
nitobi.form.listboxXslProc.addParameter("val",_6de,"");
this.listXml=nitobi.xml.transformToXml(nitobi.xml.createXmlDoc(_6df.xml),nitobi.form.listboxXslProc);
this.placeholder.rows[0].cells[0].innerHTML=nitobi.xml.serialize(this.listXml);
this.control=this.placeholder.rows[0].cells[0].childNodes[0];
this.control.style.width="100%";
this.control.style.height=(this.cell.DomNode.offsetHeight-2)+"px";
nitobi.html.attachEvents(this.control,this.events,this);
nitobi.html.Css.addClass(this.control.className,this.cell.DomNode.className);
var oY=0;
var oX=0;
if(nitobi.browser.MOZ){
var _6e5=this.context.getScrollSurface();
var _6e6=this.context.getActiveView().region;
if(_6e6==3||_6e6==4){
oY=_6e5.scrollTop-nitobi.form.EDITOR_OFFSETY;
}
if(_6e6==1||_6e6==4){
oX=_6e5.scrollLeft-nitobi.form.EDITOR_OFFSETX;
}
}
nitobi.drawing.align(this.placeholder,this.cell.DomNode,286265344,0,0,-oY,-oX);
this.control.focus();
if(this.control.createTextRange){
var _6e7=this.control.createTextRange();
_6e7.collapse(false);
_6e7.select();
}
};
nitobi.form.ListBox.prototype.deactivate=function(ok){
if(this.blur||this.keypress){
this.keypress=false;
return;
}
this.blur=true;
if(this.onChange.notify(this)==false){
return;
}
var c=this.control;
var text="",_6eb="";
if(ok||ok==null){
text=c.options[c.selectedIndex].text;
_6eb=c.options[c.selectedIndex].value;
}else{
_6eb=this.cell.getValue();
var len=c.options.length;
for(var i=0;i<len;i++){
if(c.options[i].value==_6eb){
text=c.options[i].text;
}
}
}
c.object=null;
if(this.editCompleteHandler!=null){
var _6ee=new nitobi.grid.EditCompleteEventArgs(this,nitobi.html.encode(text),_6eb,this.cell);
_6ee.status=(_6eb==this.cell.getValue()?false:true);
this.editCompleteHandler.call(this.context,_6ee);
}
};
nitobi.form.ListBox.prototype.handleKey=function(evt){
var k=evt.keyCode;
if(this.onKeyDown.notify(evt)==false){
return;
}
this.keypress=false;
switch(k){
case 27:
this.deactivate(false);
break;
case 40:
if(this.control.selectedIndex<this.control.options.length-1){
this.keypress=true;
}
break;
case 38:
if(this.control.selectedIndex>0){
this.keypress=true;
}
break;
case 37:
case 39:
case 13:
case 27:
this.deactivate(true);
break;
default:
}
};
nitobi.form.ListBox.prototype.dispose=function(){
nitobi.html.detachEvents(this.control,this.events);
this.placeholder=null;
this.control=null;
this.listXml=null;
this.element=null;
this.metadata=null;
this.owner=null;
};
nitobi.form.Lookup=function(){
nitobi.form.Lookup.baseConstructor.call(this);
this.bVisible=false;
var div=document.createElement("div");
div.innerHTML="<table class='ntbinputborder' border='1px' cellpadding='0' cellspacing='0' style='overflow:hidden;'><tr><td></td></tr><tr><td></td></tr></table>";
this.placeholder=div.firstChild;
this.placeholder.setAttribute("id","lookup_span");
this.placeholder.style.position="absolute";
this.placeholder.style.zIndex=2000;
this.placeholder.style.top="-2000px";
this.placeholder.style.left="-2000px";
this.placeholder.style.tableLayout="fixed";
document.body.appendChild(this.placeholder);
this.textControl=document.createElement("input");
this.textControl.className="ntbinput ntblookuptext";
this.textControl.style.borderWidth="0px";
this.textControl.autocomplete="off";
this.textControl.style.zIndex=2000;
this.events=[{"type":"keydown","handler":this.handleKey},{"type":"keyup","handler":this.filter},{"type":"keypress","handler":this.handleKeyPress},{"type":"change","handler":this.handleChange}];
nitobi.html.attachEvents(this.textControl,this.events,this);
this.placeholder.rows[0].cells[0].appendChild(this.textControl);
this.selectPlaceholder=this.placeholder.rows[1].cells[0];
this.selectEvents=[{"type":"click","handler":this.handleSelectClicked}];
nitobi.html.attachEvents(this.selectPlaceholder,this.selectEvents,this);
this.firstKeyup=false;
this.autocompleted=false;
this.listXml=null;
this.listXmlLower=null;
this.editCompleteHandler=null;
this.delay=0;
this.timeoutId=null;
var xsl="<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">";
xsl+="<xsl:output method=\"text\" version=\"4.0\"/><xsl:param name='searchValue'/>";
xsl+="<xsl:template match=\"/\"><xsl:apply-templates select='//option[starts-with(.,$searchValue)][1]' /></xsl:template>";
xsl+="<xsl:template match=\"option\"><xsl:value-of select='@rn' /></xsl:template></xsl:stylesheet>";
var _6f3=nitobi.xml.createXslDoc(xsl);
this.searchXslProc=nitobi.xml.createXslProcessor(_6f3);
_6f3=null;
};
nitobi.lang.extend(nitobi.form.Lookup,nitobi.form.Control);
nitobi.lang.implement(nitobi.form.Lookup,nitobi.ui.IDataBoundList);
nitobi.lang.implement(nitobi.form.Lookup,nitobi.form.IBlurable);
nitobi.form.Lookup.prototype.initialize=function(){
this.firstKeyup=false;
};
nitobi.form.Lookup.prototype.mimic=function(){
};
nitobi.form.Lookup.prototype.hide=function(){
this.placeholder.style.top="-2000px";
};
nitobi.form.Lookup.prototype.hideSelect=function(){
this.selectControl.style.display="none";
this.bVisible=false;
};
nitobi.form.Lookup.prototype.bind=function(_6f4,cell,_6f6){
nitobi.form.Text.base.bind.apply(this,arguments);
this.column=this.cell.getColumnObject();
var _6f7=this.column.ModelNode;
this.datasourceId=_6f7.getAttribute("DatasourceId");
this.getHandler=_6f7.getAttribute("GetHandler");
this.pageSize=_6f7.getAttribute("PageSize");
this.delay=parseInt(_6f7.getAttribute("Delay"));
this.size=_6f7.getAttribute("Size");
this.displayFields=_6f7.getAttribute("DisplayFields");
this.valueField=_6f7.getAttribute("ValueField");
this.eSET("onKeyPress",[_6f7.getAttribute("OnKeyPressEvent")]);
this.eSET("onKeyDown",[_6f7.getAttribute("OnKeyDownEvent")]);
this.eSET("onKeyUp",[_6f7.getAttribute("OnKeyUpEvent")]);
this.eSET("onChange",[_6f7.getAttribute("OnChangeEvent")]);
nitobi.form.listboxXslProc.addParameter("DisplayFields",this.displayFields,"");
nitobi.form.listboxXslProc.addParameter("ValueField",this.valueField,"");
this.dataTable=this.owner.data.getTable(this.datasourceId);
this.dataTable.setGetHandler(this.getHandler);
this.dataTable.async=false;
if(_6f6.length<=0){
_6f6=this.cell.getValue();
}
this.get(_6f6,true);
};
nitobi.form.Lookup.prototype.bindComplete=function(_6f8){
var _6f9=this.dataTable.getXmlDoc();
nitobi.form.listboxXslProc.addParameter("DisplayFields",this.displayFields,"");
nitobi.form.listboxXslProc.addParameter("ValueField",this.valueField,"");
nitobi.form.listboxXslProc.addParameter("val",nitobi.xml.constructValidXpathQuery(this.cell.getValue(),false),"");
this.listXml=nitobi.xml.transformToXml(nitobi.xml.createXmlDoc(_6f9.xml),nitobi.form.listboxXslProc);
this.listXmlLower=nitobi.xml.createXmlDoc(this.listXml.xml.toLowerCase());
this.selectPlaceholder.innerHTML=nitobi.xml.serialize(this.listXml);
this.selectControl=nitobi.html.getFirstChild(this.selectPlaceholder);
this.selectControl.setAttribute("size",this.size);
this.selectControl.style.display="none";
if(nitobi.browser.IE6){
this.selectControl.style.height="100%";
}
nitobi.form.IBlurable.call(this,[this.textControl,this.selectControl],this.deactivate);
this.bVisible=false;
var rn=this.search(_6f8);
var oY=0;
var oX=0;
if(nitobi.browser.MOZ){
var _6fd=this.context.getScrollSurface();
var _6fe=this.context.getActiveView().region;
if(_6fe==3||_6fe==4){
oY=_6fd.scrollTop-nitobi.form.EDITOR_OFFSETY;
}
if(_6fe==1||_6fe==4){
oX=_6fd.scrollLeft-nitobi.form.EDITOR_OFFSETX;
}
}
var _6ff=this.cell.getDomNode();
nitobi.drawing.align(this.placeholder,_6ff,286265344,0,0,-oY,-oX);
if(rn>0){
this.selectControl.selectedIndex=rn-1;
this.textControl.value=this.selectControl[this.selectControl.selectedIndex].text;
nitobi.html.highlight(this.textControl,this.textControl.value.length-(this.textControl.value.length-_6f8.length));
this.autocompleted=true;
}else{
var row=_6f9.selectSingleNode("//"+nitobi.xml.nsPrefix+"e[@"+this.valueField+"='"+_6f8+"']");
if(row!=null){
this.textControl.value=row.getAttribute(this.displayFields);
var rn=this.search(this.textControl.value);
this.selectControl.selectedIndex=parseInt(rn)-1;
}else{
this.textControl.value=_6f8;
this.selectControl.selectedIndex=-1;
}
}
this.textControl.style.height=nitobi.html.getHeight(_6ff)+"px";
var _701=this.placeholder.clientWidth;
this.selectControl.style.display="inline";
this.textControl.style.width="100%";
this.textControl.focus();
};
nitobi.form.Lookup.prototype.handleSelectClicked=function(evt){
this.textControl.value=this.selectControl.selectedIndex!=-1?this.selectControl.options[this.selectControl.selectedIndex].text:"";
this.deactivate(true);
};
nitobi.form.Lookup.prototype.focus=function(evt){
this.textControl.focus();
};
nitobi.form.Lookup.prototype.deactivate=function(evt,o){
var sc=this.selectControl;
var tc=this.textControl;
var text="",_709="";
if(evt!=null&&evt!=false){
if(sc.selectedIndex>=0){
_709=sc.options[sc.selectedIndex].value;
text=sc.options[sc.selectedIndex].text;
}else{
if(this.column.ModelNode.getAttribute("ForceValidOption")!="true"){
_709=tc.value;
text=_709;
}else{
_709=this.cell.getValue();
var len=sc.options.length;
for(var i=0;i<len;i++){
if(sc.options[i].value==_709){
text=sc.options[i].text;
}
}
}
}
}else{
_709=this.cell.getValue();
var len=sc.options.length;
for(var i=0;i<len;i++){
if(sc.options[i].value==_709){
text=sc.options[i].text;
}
}
}
nitobi.html.detachEvents(this.selectControl,this.events);
sc=null;
window.clearTimeout(this.timeoutId);
if(this.editCompleteHandler!=null){
var _70c=new nitobi.grid.EditCompleteEventArgs(this,nitobi.html.encode(text),_709,this.cell);
_70c.status=true;
this.editCompleteHandler.call(this.owner,_70c);
}
};
nitobi.form.Lookup.prototype.handleKey=function(evt,_70e){
var k=evt.keyCode;
if(this.onKeyDown.notify(evt)==false){
return;
}
if(k==27){
this.deactivate(false);
return;
}
if(evt.ctrlKey&&k==86){
return;
}
if(evt.ctrlKey){
return;
}
switch(k){
case 9:
this.deactivate(true);
break;
case 13:
nitobi.html.cancelEvent(evt);
if(nitobi.browser.IE){
evt.keyCode=32;
}else{
nitobi.html.cancelEvent(evt);
nitobi.html.createEvent("KeyEvents","keydown",evt,{keyCode:0,charCode:32});
}
this.deactivate(true);
break;
case 8:
default:
this.autocompleted=false;
if(!this.bVisible){
this.selectControl.style.display="inline";
}
}
};
nitobi.form.Lookup.prototype.search=function(_710){
_710=nitobi.xml.constructValidXpathQuery(_710,false);
this.searchXslProc.addParameter("searchValue",_710.toLowerCase(),"");
var _711=nitobi.xml.transformToString(this.listXmlLower,this.searchXslProc);
if(""==_711){
_711=0;
}else{
_711=parseInt(_711);
}
return _711;
};
nitobi.form.Lookup.prototype.filter=function(evt,o){
if(this.onKeyUp.notify(evt)==false){
return;
}
if(!this.firstKeyup){
this.firstKeyup=true;
return;
}
var k=evt.keyCode;
var tc=this.textControl;
var sc=this.selectControl;
switch(k){
case 38:
if(sc.selectedIndex==-1){
sc.selectedIndex=0;
}
if(sc.selectedIndex>0){
sc.selectedIndex--;
}
tc.value=sc.options[sc.selectedIndex].text;
nitobi.html.highlight(tc,tc.value.length);
tc.select();
break;
case 40:
if(sc.selectedIndex<(sc.length-1)){
sc.selectedIndex++;
}
tc.value=sc.options[sc.selectedIndex].text;
nitobi.html.highlight(tc,tc.value.length);
tc.select();
break;
default:
if(k<193&&k>46){
var _717=tc.value;
this.get(_717);
}
}
};
nitobi.form.Lookup.prototype.get=function(_718,_719){
if(this.getHandler!=null&&this.getHandler!=""){
if(_719||!this.delay){
this.doGet(_718);
}else{
if(this.timeoutId){
window.clearTimeout(this.timeoutId);
this.timeoutId=null;
}
this.timeoutId=window.setTimeout(nitobi.lang.close(this,this.doGet,[_718]),this.delay);
}
}
};
nitobi.form.Lookup.prototype.doGet=function(_71a){
if(_71a){
this.dataTable.setGetHandlerParameter("SearchString",_71a);
}
this.dataTable.get(null,this.pageSize,this);
this.timeoutId=null;
this.bindComplete(_71a);
};
nitobi.form.Lookup.prototype.dispose=function(){
this.placeholder=null;
nitobi.html.detachEvents(this.events,this);
this.textControl=null;
this.owner=null;
};
nitobi.form.Number=function(){
nitobi.form.Number.baseConstructor.call(this);
};
nitobi.lang.extend(nitobi.form.Number,nitobi.form.Text);
nitobi.form.Number.prototype.handleKey=function(evt){
nitobi.form.Number.base.handleKey.call(this,evt);
var k=evt.keyCode;
this.lastKeyCode=k;
if((k<48||k>57)&&(k<37||k>40)&&(k<96||k>105)&&k!=190&&k!=110&&k!=189&&k!=109&&k!=9&&k!=45&&k!=46&&k!=8){
nitobi.html.cancelEvent(evt);
return false;
}
};
nitobi.form.Number.defaultValue=0;
nitobi.form.Password=function(){
nitobi.form.Password.baseConstructor.call(this,true);
this.control.type="password";
};
nitobi.lang.extend(nitobi.form.Password,nitobi.form.Text);
nitobi.form.TextArea=function(){
nitobi.form.TextArea.baseConstructor.call(this);
var div=document.createElement("div");
div.innerHTML="<table border='0' cellpadding='0' cellspacing='0' class='ntbinputborder'><tr><td></td></table>";
this.placeholder=div.firstChild;
this.control=document.createElement("textarea");
this.control.style.border="0px";
this.control.className="ntbinput";
this.control.style.width="100%";
this.placeholder.style.position="absolute";
this.placeholder.style.top="-2000px";
this.placeholder.style.left="-2000px";
this.placeholder.style.zIndex=2000;
};
nitobi.lang.extend(nitobi.form.TextArea,nitobi.form.Text);
nitobi.form.TextArea.prototype.initialize=function(){
this.placeholder.rows[0].cells[0].appendChild(this.control);
document.body.appendChild(this.placeholder);
nitobi.html.attachEvents(this.control,this.events,this);
};
nitobi.form.TextArea.prototype.mimic=function(){
nitobi.form.TextArea.base.mimic.call(this);
this.placeholder.style.height=parseInt(this.placeholder.style.height)*2+"px";
this.placeholder.style.width=parseInt(this.placeholder.style.width)*1.5+"px";
this.control.style.height=this.placeholder.style.height;
this.control.style.width=this.placeholder.style.width;
};
nitobi.form.TextArea.prototype.handleKey=function(evt,o){
var k=evt.keyCode;
if(this.onKeyDown.notify(evt)==false){
return;
}
switch(k){
case 40:
break;
case 38:
break;
case 37:
break;
case 39:
break;
case 13:
nitobi.html.cancelEvent(evt);
if(nitobi.browser.IE){
evt.keyCode=32;
}
if(!evt.shiftKey){
if(nitobi.browser.MOZ){
nitobi.html.createEvent("KeyEvents","keydown",evt,{"keyCode":0,"charCode":32});
}
this.deactivate();
}else{
if(nitobi.browser.MOZ){
nitobi.html.createEvent("KeyEvents","keypress",evt,{"keyCode":13,"charCode":0});
}
if(this.control.createTextRange){
this.control.focus();
var _721=document.selection.createRange();
_721.text="\n";
_721.collapse(false);
_721.select();
}
}
break;
case 9:
break;
case 27:
nitobi.html.detachEvent(this.control,"blur",this.deactivate);
this.hide();
this.owner.focus();
break;
default:
}
};
nitobi.form.Calendar=function(){
nitobi.form.Calendar.baseConstructor.call(this);
var div=document.createElement("div");
div.innerHTML="<table border='0' cellpadding='0' cellspacing='0' style='table-layout:fixed;' class='ntb-dp-input'><tr><td></td><td>"+"<input type='text' style='width:100%;' />"+"</td><td class='ntb-dp-inputbutton'><a href='#' onclick='return false;'></a></td></tr><tr><td colspan='3'><div style='width:1px;height:1px;position:relative;'><!-- --></div></td></tr><colgroup><col style='width:4px;'></col><col></col><col style='width:20px;'></col></colgroup></table>";
this.control=div.getElementsByTagName("input")[0];
this.container=div.firstChild;
this.container.object=this;
this.container.style.position="absolute";
this.container.style.top="-3000px";
this.container.style.zIndex=2000;
this.container.style.left="-3000px";
this.control.setAttribute("maxlength",255);
this.pickerDiv=document.createElement("div");
this.pickerDiv.style.position="absolute";
this.pickerDiv.style.top="2px";
this.pickerDiv.style.left="-1px";
this.isPickerVisible=false;
nitobi.html.Css.addClass(this.pickerDiv,NTB_CSS_HIDE);
this.container.rows[1].cells[0].firstChild.appendChild(this.pickerDiv);
};
nitobi.lang.extend(nitobi.form.Calendar,nitobi.form.Control);
nitobi.form.Calendar.prototype.initialize=function(){
document.body.appendChild(this.container);
this.datePicker=new nitobi.calendar.DatePicker(nitobi.component.getUniqueId());
this.datePicker.setContainer(this.pickerDiv);
this.datePicker.onSetDate.subscribe(this.handlePick,this);
nitobi.html.attachEvent(this.control,"keydown",this.handleKey,this,false);
nitobi.html.attachEvent(this.control,"blur",this.deactivate,this,false);
nitobi.html.attachEvent(this.pickerDiv,"mousedown",function(){
this.dontBlur=true;
},this);
nitobi.html.attachEvent(this.pickerDiv,"mouseup",function(){
this.control.focus();
},this);
var a=this.container.getElementsByTagName("a")[0];
nitobi.html.attachEvent(a,"mousedown",this.handleClick,this);
nitobi.html.attachEvent(a,"mouseup",function(){
this.control.focus();
},this);
};
nitobi.form.Calendar.prototype.attachToParent=function(_724){
_724.appendChild(this.container);
};
nitobi.form.Calendar.prototype.bind=function(_725,cell,_727){
this.isPickerVisible=false;
nitobi.html.Css.addClass(this.pickerDiv,NTB_CSS_HIDE);
nitobi.form.Calendar.base.bind.apply(this,arguments);
if(_727!=null&&_727!=""){
this.control.value=_727;
}else{
this.control.value=cell.getValue();
}
this.column=this.cell.getColumnObject();
this.control.maxlength=this.column.ModelNode.getAttribute("MaxLength");
};
nitobi.form.Calendar.prototype.render=function(){
this.domNode.appendChild(this.container);
};
nitobi.form.Calendar.prototype.mimic=function(){
var oY=0;
var oX=0;
if(nitobi.browser.MOZ){
oY=this.owner.Scroller.scrollSurface.scrollTop-nitobi.form.EDITOR_OFFSETY;
oX=this.owner.Scroller.scrollSurface.scrollLeft-nitobi.form.EDITOR_OFFSETX;
}
nitobi.drawing.align(this.container,this.cell.getDomNode(),286265344,0,0,-oY,-oX);
this.control.focus();
if(this.control.createTextRange){
var _72a=this.control.createTextRange();
_72a.collapse(false);
_72a.select();
}
};
nitobi.form.Calendar.prototype.hide=function(){
this.container.style.left="-2000px";
};
nitobi.form.Calendar.prototype.deactivate=function(){
if(this.dontBlur){
this.dontBlur=false;
this.control.focus();
return;
}
var node=this.container.rows[0].cells[2].firstChild;
if(this.lastKeyCode==27){
return;
}
if(nitobi.form.Calendar.base.deactivate.apply(this,arguments)==false){
return;
}
var _72c=this.control.value;
if(this.editCompleteHandler!=null){
var _72d=new nitobi.grid.EditCompleteEventArgs(this,_72c,_72c,this.cell);
var _72e=this.editCompleteHandler.call(this.owner,_72d);
if(!_72e){
this.blur=false;
}
return _72e;
}
};
nitobi.form.Calendar.prototype.handleClick=function(evt,_730){
if(!this.isPickerVisible){
this.datePicker.setDate(nitobi.base.DateMath.parseIso8601(this.control.value));
this.datePicker.render();
}
this.dontBlur=true;
var node=this.container.rows[0].cells[2].firstChild;
nitobi.ui.Effects.setVisible(this.pickerDiv,!this.isPickerVisible,"none",this.setVisibleComplete,this);
this.control.focus();
};
nitobi.form.Calendar.prototype.setVisibleComplete=function(){
this.isPickerVisible=!this.isPickerVisible;
};
nitobi.form.Calendar.prototype.handlePick=function(){
var date=this.datePicker.getDate();
var _733=nitobi.base.DateMath.toIso8601(date);
this.control.value=_733;
};
nitobi.form.Calendar.prototype.handleKey=function(evt){
var k=evt.keyCode;
this.lastKeyCode=k;
switch(k){
case 27:
this.control.onblur=null;
this.hide();
this.owner.focus();
break;
case 9:
var _736=this.deactivate();
if(!_736){
nitobi.html.cancelBubble(evt);
break;
}
if(nitobi.browser.IE){
evt.keyCode="";
}
var x=1;
if(evt.shiftKey){
x=-1;
}
this.owner.move(x,0);
nitobi.html.cancelBubble(evt);
break;
case 40:
case 38:
break;
case 13:
this.control.blur();
evt.returnValue=false;
break;
default:
}
};
nitobi.form.Calendar.prototype.dispose=function(){
this.container.object=null;
nitobi.html.detachEvent(this.control,"keydown",this.handleKey);
nitobi.html.detachEvent(this.control,"blur",this.deactivate);
var _738=this.container.parentNode;
_738.removeChild(this.container);
this.domNode=null;
this.control=null;
this.container=null;
this.owner=null;
this.cell=null;
};
nitobi.ui.UiElement=function(xml,xsl,id){
if(arguments.length>0){
this.initialize(xml,xsl,id);
}
};
nitobi.ui.UiElement.prototype.initialize=function(xml,xsl,id){
this.m_Xml=xml;
this.m_Xsl=xsl;
this.m_Id=id;
this.m_HtmlElementHandle=null;
};
nitobi.ui.UiElement.prototype.getHeight=function(){
return this.getHtmlElementHandle().style.height;
};
nitobi.ui.UiElement.prototype.setHeight=function(_73f){
this.getHtmlElementHandle().style.height=_73f+"px";
};
nitobi.ui.UiElement.prototype.getId=function(){
return this.m_Id;
};
nitobi.ui.UiElement.prototype.setId=function(id){
this.m_Id=id;
};
nitobi.ui.UiElement.prototype.getWidth=function(){
return this.getHtmlElementHandle().style.width;
};
nitobi.ui.UiElement.prototype.setWidth=function(_741){
this.getHtmlElementHandle().style.width=_741+"px";
};
nitobi.ui.UiElement.prototype.getXml=function(){
return this.m_Xml;
};
nitobi.ui.UiElement.prototype.setXml=function(xml){
this.m_Xml=xml;
};
nitobi.ui.UiElement.prototype.getXsl=function(){
return this.m_Xsl;
};
nitobi.ui.UiElement.prototype.setXsl=function(xsl){
this.m_Xsl=xsl;
};
nitobi.ui.UiElement.prototype.getHtmlElementHandle=function(){
if(!this.m_HtmlElementHandle){
this.m_HtmlElementHandle=document.getElementById(this.m_Id);
}
return this.m_HtmlElementHandle;
};
nitobi.ui.UiElement.prototype.setHtmlElementHandle=function(_744){
this.m_HtmlElementHandle=_744;
};
nitobi.ui.UiElement.prototype.hide=function(){
var tag=this.getHtmlElementHandle();
tag.style.visibility="hidden";
tag.style.position="absolute";
};
nitobi.ui.UiElement.prototype.show=function(){
var tag=this.getHtmlElementHandle();
tag.style.visibility="visible";
};
nitobi.ui.UiElement.prototype.isVisible=function(){
var tag=this.getHtmlElementHandle();
return tag.style.visibility=="visible";
};
nitobi.ui.UiElement.prototype.beginFloatMode=function(){
var tag=this.getHtmlElementHandle();
tag.style.position="absolute";
};
nitobi.ui.UiElement.prototype.isFloating=function(){
var tag=this.getHtmlElementHandle();
return tag.style.position=="absolute";
};
nitobi.ui.UiElement.prototype.setX=function(x){
var tag=this.getHtmlElementHandle();
tag.style.left=x+"px";
};
nitobi.ui.UiElement.prototype.getX=function(){
var tag=this.getHtmlElementHandle();
return tag.style.left;
};
nitobi.ui.UiElement.prototype.setY=function(y){
var tag=this.getHtmlElementHandle();
tag.style.top=y+"px";
};
nitobi.ui.UiElement.prototype.getY=function(){
var tag=this.getHtmlElementHandle();
return tag.style.top;
};
nitobi.ui.UiElement.prototype.render=function(_750,_751,_752){
var xsl=this.m_Xsl;
if(xsl!=null&&xsl.indexOf("xsl:stylesheet")==-1){
xsl="<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"><xsl:output method=\"html\" version=\"4.0\" />"+xsl+"</xsl:stylesheet>";
}
if(null==_751){
_751=nitobi.xml.createXslDoc(xsl);
}
if(null==_752){
_752=nitobi.xml.createXmlDoc(this.m_Xml);
}
Eba.Error.assert(nitobi.xml.isValidXml(_752),"Tried to render invalid XML according to Mozilla. The XML is "+_752.xml);
var html=nitobi.xml.transform(_752,_751);
if(html.xml){
html=html.xml;
}
if(null==_750){
document.body.insertAdjacentHTML("beforeEnd",html);
}else{
_750.innerHTML=html;
}
this.attachToTag();
};
nitobi.ui.UiElement.prototype.attachToTag=function(){
var _755=this.getHtmlElementHandle();
if(_755!=null){
_755.object=this;
_755.jsobject=this;
_755.javascriptObject=this;
}
};
nitobi.ui.UiElement.prototype.dispose=function(){
var _756=this.getHtmlElementHandle();
if(_756!=null){
_756.object=null;
}
this.m_Xml=null;
this.m_Xsl=null;
this.m_HtmlElementHandle=null;
};
nitobi.ui.InteractiveUiElement=function(_757){
this.enable();
};
nitobi.lang.extend(nitobi.ui.InteractiveUiElement,nitobi.ui.UiElement);
nitobi.ui.InteractiveUiElement.prototype.enable=function(){
this.m_Enabled=true;
};
nitobi.ui.InteractiveUiElement.prototype.disable=function(){
this.m_Enabled=false;
};
nitobi.ui.ButtonXsl="<xsl:template match=\"button\">"+"<div class=\"EbaButton\" onmousemove=\"return false;\" onmousedown=\"if (this.object.m_Enabled) this.className='EbaButtonDown';\" onmouseup=\"this.className='EbaButton';\" onmouseover=\"if (this.object.m_Enabled) this.className='EbaButtonHighlight';\" onmouseout=\"this.className='EbaButton';\" align=\"center\">"+"<xsl:attribute name=\"image_disabled\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image_disabled)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image_disabled\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"image_enabled\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"title\">"+"<xsl:value-of select=\"@tooltip_text\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"onclick\">"+"<xsl:value-of select='concat(&quot;v&quot;,&quot;a&quot;,&quot;r&quot;,&quot; &quot;,&quot;e&quot;,&quot;=&quot;,&quot;&apos;&quot;,@onclick_event,&quot;&apos;&quot;,&quot;;&quot;,&quot;e&quot;,&quot;v&quot;,&quot;a&quot;,&quot;l&quot;,&quot;(&quot;,&quot;t&quot;,&quot;h&quot;,&quot;i&quot;,&quot;s&quot;,&quot;.&quot;,&quot;o&quot;,&quot;b&quot;,&quot;j&quot;,&quot;e&quot;,&quot;c&quot;,&quot;t&quot;,&quot;.&quot;,&quot;o&quot;,&quot;n&quot;,&quot;C&quot;,&quot;l&quot;,&quot;i&quot;,&quot;c&quot;,&quot;k&quot;,&quot;H&quot;,&quot;a&quot;,&quot;n&quot;,&quot;d&quot;,&quot;l&quot;,&quot;e&quot;,&quot;r&quot;,&quot;(&quot;,&quot;e&quot;,&quot;)&quot;,&quot;)&quot;,&quot;;&quot;,&apos;&apos;)' />"+"</xsl:attribute>"+"<xsl:attribute name=\"id\">"+"<xsl:value-of select=\"@id\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">"+"<xsl:choose>"+"<xsl:when test=\"../../@height\">"+"<xsl:value-of select=\"concat('float:left;width:',../../@height,'px;height:',../../@height - 1,'px')\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"concat('float:left;width:',@width,'px;height:',@height,'px')\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<img border=\"0\">"+"<xsl:attribute name=\"src\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">"+"<xsl:variable name=\"top_offset\">"+"<xsl:choose>"+"<xsl:when test=\"@top_offset\">"+"<xsl:value-of select=\"@top_offset\" />"+"</xsl:when>"+"<xsl:otherwise>"+"0"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:variable>"+"<xsl:choose>"+"<xsl:when test=\"../../@height\">"+"<xsl:value-of select=\"concat('MARGIN-TOP:',((../../@height - @height) div 2) - 1 + number($top_offset),'px;MARGIN-BOTTOM:0px')\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"concat('MARGIN-TOP:',(@height - @image_height) div 2,'px;MARGIN-BOTTOM:0','px')\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"</img><![CDATA[ ]]>"+"</div>"+"</xsl:template>";
nitobi.ui.Button=function(xml,id){
this.initialize(xml,nitobi.ui.ButtonXsl,id);
this.enable();
};
nitobi.lang.extend(nitobi.ui.Button,nitobi.ui.InteractiveUiElement);
nitobi.ui.Button.prototype.onClickHandler=function(_75a){
if(this.m_Enabled){
eval(_75a);
}
};
nitobi.ui.Button.prototype.disable=function(){
nitobi.ui.Button.base.disable.call(this);
var _75b=this.getHtmlElementHandle();
_75b.childNodes[0].src=_75b.getAttribute("image_disabled");
};
nitobi.ui.Button.prototype.enable=function(){
nitobi.ui.Button.base.enable.call(this);
var _75c=this.getHtmlElementHandle();
_75c.childNodes[0].src=_75c.getAttribute("image_enabled");
};
nitobi.ui.Button.prototype.dispose=function(){
nitobi.ui.Button.base.dispose.call(this);
};
nitobi.ui.BinaryStateButtonXsl="<xsl:template match=\"binarystatebutton\">"+"<div class=\"EbaBinaryStateButton\" onmousemove=\"return false;\" onmousedown=\"if (this.object.m_Enabled) this.className='EbaButtonDown';\" onmouseup=\"(this.object.isChecked()?this.object.check():this.object.uncheck())\" onmouseover=\"if (this.object.m_Enabled) this.className='EbaButtonHighlight';\" onmouseout=\"(this.object.isChecked()?this.object.check():this.object.uncheck())\" align=\"center\">"+"<xsl:attribute name=\"image_disabled\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image_disabled)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image_disabled\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"image_enabled\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"title\">"+"<xsl:value-of select=\"@tooltip_text\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"onclick\">"+"<xsl:value-of select='concat(\"this.object.toggle();\",&quot;v&quot;,&quot;a&quot;,&quot;r&quot;,&quot; &quot;,&quot;e&quot;,&quot;=&quot;,&quot;&apos;&quot;,@onclick_event,&quot;&apos;&quot;,&quot;;&quot;,&quot;e&quot;,&quot;v&quot;,&quot;a&quot;,&quot;l&quot;,&quot;(&quot;,&quot;t&quot;,&quot;h&quot;,&quot;i&quot;,&quot;s&quot;,&quot;.&quot;,&quot;o&quot;,&quot;b&quot;,&quot;j&quot;,&quot;e&quot;,&quot;c&quot;,&quot;t&quot;,&quot;.&quot;,&quot;o&quot;,&quot;n&quot;,&quot;C&quot;,&quot;l&quot;,&quot;i&quot;,&quot;c&quot;,&quot;k&quot;,&quot;H&quot;,&quot;a&quot;,&quot;n&quot;,&quot;d&quot;,&quot;l&quot;,&quot;e&quot;,&quot;r&quot;,&quot;(&quot;,&quot;e&quot;,&quot;)&quot;,&quot;)&quot;,&quot;;&quot;,&apos;&apos;)' />"+"</xsl:attribute>"+"<xsl:attribute name=\"id\">"+"<xsl:value-of select=\"@id\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">"+"<xsl:choose>"+"<xsl:when test=\"../../@height\">"+"<xsl:value-of select=\"concat('float:left;width:',../../@height,'px;height:',../../@height - 1,'px')\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"concat('float:left;width:',@width,'px;height:',@height,'px')\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<img border=\"0\">"+"<xsl:attribute name=\"src\">"+"<xsl:choose>"+"<xsl:when test=\"../../@image_directory\">"+"<xsl:value-of select=\"concat(../../@image_directory,@image)\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"@image\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">"+"<xsl:variable name=\"top_offset\">"+"<xsl:choose>"+"<xsl:when test=\"@top_offset\">"+"<xsl:value-of select=\"@top_offset\" />"+"</xsl:when>"+"<xsl:otherwise>"+"0"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:variable>"+"<xsl:choose>"+"<xsl:when test=\"../../@height\">"+"<xsl:value-of select=\"concat('MARGIN-TOP:',((../../@height - @height) div 2) - 1 + number($top_offset),'px;MARGIN-BOTTOM:0px')\" />"+"</xsl:when>"+"<xsl:otherwise>"+"<xsl:value-of select=\"concat('MARGIN-TOP:',(@height - @image_height) div 2,'px;MARGIN-BOTTOM:0','px')\" />"+"</xsl:otherwise>"+"</xsl:choose>"+"</xsl:attribute>"+"</img><![CDATA[ ]]>"+"</div>"+"</xsl:template>";
nitobi.ui.BinaryStateButton=function(xml,id){
this.initialize(xml,nitobi.ui.BinaryStateButtonXsl,id);
this.m_Checked=false;
};
nitobi.lang.extend(nitobi.ui.BinaryStateButton,nitobi.ui.Button);
nitobi.ui.BinaryStateButton.prototype.isChecked=function(){
return this.m_Checked;
};
nitobi.ui.BinaryStateButton.prototype.check=function(){
var _75f=this.getHtmlElementHandle();
_75f.className="EbaButtonChecked";
this.m_Checked=true;
};
nitobi.ui.BinaryStateButton.prototype.uncheck=function(){
var _760=this.getHtmlElementHandle();
_760.className="EbaButton";
this.m_Checked=false;
};
nitobi.ui.BinaryStateButton.prototype.toggle=function(){
var _761=this.getHtmlElementHandle();
if(_761.className=="EbaButtonChecked"){
this.uncheck();
}else{
this.check();
}
};
nitobi.ui.ToolbarXsl="<xsl:template match=\"//toolbar\">"+"<div class=\"EbaToolbar\" style=\"z-index:800\">"+"<xsl:attribute name=\"id\">"+"<xsl:value-of select=\"@id\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">float:left;position:relative;"+"<xsl:value-of select=\"concat('width:',@width,'px;height:',@height,'px')\" />"+"</xsl:attribute>"+"<div id=\"ToolbarTitle\" onmousedown=\"this.parentNode.jsobject.dragWindow(event)\" ondblclick=\"this.parentNode.jsobject.dock()\" style=\"width:100%;position:absolute;visibility:hidden\">"+"<div class=\"EbaToolbarTitle\" >"+"<div style=\"float:right;\" onclick=\"this.parentNode.parentNode.parentNode.jsobject.dock();\">^</div>"+"<xsl:value-of select=\"@title\"/>"+"</div>"+"</div> "+"<div onmousedown=\"this.parentNode.jsobject.startDrag(event)\" id=\"handle\" style=\"width:10px;height:100%;float:left;\" class=\"EbaToolbarHandle\"><span></span></div>"+"<xsl:apply-templates />"+"</div>"+"</xsl:template>"+nitobi.ui.ButtonXsl+nitobi.ui.BinaryStateButtonXsl+"<xsl:template match=\"separator\">"+"<div align='center'>"+"<xsl:attribute name=\"style\">"+"<xsl:value-of select=\"concat('float:left;width:',@width,';height:',@height)\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"id\">"+"<xsl:value-of select=\"@id\" />"+"</xsl:attribute>"+"<img border='0'>"+"<xsl:attribute name=\"src\">"+"<xsl:value-of select=\"concat(//@image_directory,@image)\" />"+"</xsl:attribute>"+"<xsl:attribute name=\"style\">"+"<xsl:value-of select=\"concat('MARGIN-TOP:3','px;MARGIN-BOTTOM:0','px')\" />"+"</xsl:attribute>"+"</img>"+"</div>"+"</xsl:template>";
nitobi.ui.Toolbar=function(xml,id){
nitobi.ui.Toolbar.baseConstructor.call(this);
this.initialize(xml,nitobi.ui.ToolbarXsl,id);
this.m_isFloating=false;
};
nitobi.lang.extend(nitobi.ui.Toolbar,nitobi.ui.InteractiveUiElement);
nitobi.ui.Toolbar.prototype.getUiElements=function(){
return this.m_UiElements;
};
nitobi.ui.Toolbar.prototype.setUiElements=function(_764){
this.m_UiElements=_764;
};
nitobi.ui.Toolbar.prototype.attachButtonObjects=function(){
if(!this.m_UiElements){
this.m_UiElements=new Array();
var tag=this.getHtmlElementHandle();
var _766=tag.childNodes;
for(var i=0;i<_766.length;i++){
var _768=_766[i];
if(_768.nodeType!=3&&_768.className!="EbaToolbarTitle"&&_768.className!="EbaToolbarHandle"){
var _769;
switch(_768.className){
case ("EbaButton"):
_769=new nitobi.ui.Button(null,_768.id);
break;
case ("EbaBinaryStateButton"):
_769=new nitobi.ui.BinaryStateButton(null,_768.id);
break;
default:
_769=new nitobi.ui.UiElement(null,null,_768.id);
break;
}
_769.attachToTag();
this.m_UiElements[_768.id]=_769;
}
}
}
};
nitobi.ui.Toolbar.prototype.render=function(_76a){
nitobi.ui.Toolbar.base.base.render.call(this,_76a);
this.attachButtonObjects();
};
nitobi.ui.Toolbar.prototype.disableAllElements=function(){
for(var i in this.m_UiElements){
if(this.m_UiElements[i].disable){
this.m_UiElements[i].disable();
}
}
};
nitobi.ui.Toolbar.prototype.enableAllElements=function(){
for(var i in this.m_UiElements){
if(this.m_UiElements[i].enable){
this.m_UiElements[i].enable();
}
}
};
nitobi.ui.Toolbar.prototype.attachToTag=function(){
nitobi.ui.Toolbar.base.base.attachToTag.call(this);
this.attachButtonObjects();
};
nitobi.ui.Toolbar.prototype.getGrabbyElement=function(){
var tag=this.getHtmlElementHandle();
return tag.childNodes[1];
};
nitobi.ui.Toolbar.prototype.dragStart=function(){
var tag=this.getHtmlElementHandle();
return tag.childNodes[1];
};
nitobi.ui.Toolbar.prototype.startDrag=function(_76f){
var evt;
if(nitobi.browser.IE){
evt=window.event;
}else{
evt=_76f;
}
var tag=this.getHtmlElementHandle();
var _772=this.getGrabbyElement();
_772.style.visibility="hidden";
_772.style.position="absolute";
this.dragDiv=document.getElementById("toolbar_window"+tag.id);
if(null==this.dragDiv){
this.dragDiv=document.createElement("toolbar_window"+tag.id);
document.body.appendChild(this.dragDiv);
this.dragDiv.jsobject=this;
}
tag.swapNode(this.dragDiv);
tag.style.position="absolute";
var This=this;
if(nitobi.browser.IE){
x=window.event.clientX+document.documentElement.scrollLeft+document.body.scrollLeft;
y=window.event.clientY+document.documentElement.scrollTop+document.body.scrollTop;
}else{
x=_76f.clientX+window.scrollX;
y=_76f.clientY+window.scrollY;
}
tag.style.top=y-5;
tag.style.left=x-5;
var _774=tag.childNodes[0].style;
_774.visibility="visible";
_774.position="";
tag.style.height="41px";
tag.className="EbaToolbarFloating";
nitobi.ui.startDragOperation(tag,_76f);
if(!this.m_isFloating&&this.undockEvent){
this.m_isFloating=true;
this.undockEvent();
}else{
if(!this.m_isFloating){
this.m_isFloating=true;
}
}
};
nitobi.ui.Toolbar.prototype.dragWindow=function(_775){
var evt;
if(nitobi.browser.IE){
evt=window.event;
}else{
evt=_775;
}
nitobi.ui.startDragOperation(this.getHtmlElementHandle(),_775);
};
nitobi.ui.Toolbar.prototype.dock=function(){
var tag=this.getHtmlElementHandle();
tag.style.position="";
tag.style.height="23px";
tag.className="EbaToolbar";
var _778=tag.childNodes[0].style;
_778.position="absolute";
_778.visibility="hidden";
tag.swapNode(this.dragDiv);
var _779=this.getGrabbyElement();
_779.style.visibility="visible";
_779.style.position="";
this.m_isFloating=false;
if(this.dockEvent){
this.dockEvent();
}
tag=null;
_778=null;
};
nitobi.ui.Toolbar.prototype.dispose=function(){
if(typeof (this.m_UiElements)!="undefined"){
for(var _77a in this.m_UiElements){
this.m_UiElements[_77a].dispose();
}
this.m_UiElements=null;
}
nitobi.ui.Toolbar.base.dispose.call(this);
};
nitobi.lang.defineNs("nitobi.calendar");
if(false){
nitobi.calendar=function(){
};
}
nitobi.calendar.DatePicker=function(_77b){
nitobi.prepare();
nitobi.calendar.DatePicker.baseConstructor.call(this,_77b);
this.renderer=new nitobi.calendar.Renderer();
this.onSetDate=new nitobi.base.Event();
this.eventMap["setdate"]=this.onSetDate;
if(!this.getStartDate()){
var date=nitobi.base.DateMath.getMonthStart(this.getDate()||new Date());
this.setStartDate(date);
}
this.subscribeDeclarationEvents();
};
nitobi.lang.extend(nitobi.calendar.DatePicker,nitobi.ui.Element);
nitobi.base.Registry.getInstance().register(new nitobi.base.Profile("nitobi.calendar.DatePicker",null,false,"ntb:datepicker"));
nitobi.calendar.DatePicker.prototype.getDate=function(){
return this.getDateAttribute("date");
};
nitobi.calendar.DatePicker.prototype.setDate=function(date){
if(arguments.length<1){
date=new Date();
}else{
if(arguments.length>1){
date=eval("new Date("+nitobi.lang.toArray(arguments).join(",")+")");
}else{
if(typeof date!="object"){
date=new Date(date);
}
}
}
if(nitobi.base.DateMath.invalid(date)){
date=null;
}
this.setDateAttribute("date",date);
var _77e=this.getHtmlNode("value");
if(_77e){
_77e.value=this.getFormatter()(date);
}
this.onSetDate.notify(new nitobi.ui.ElementEventArgs(this,this.onSetDate));
};
nitobi.calendar.DatePicker.prototype.getStartDate=function(){
return this.getDateAttribute("startdate");
};
nitobi.calendar.DatePicker.prototype.setStartDate=function(date){
date=nitobi.base.DateMath.subtract(date,"d",date.getDay());
this.setDateAttribute("startdate",date);
};
nitobi.calendar.DatePicker.prototype.isTimePickerEnabled=function(){
return this.getBoolAttribute("timepickerenabled",false);
};
nitobi.calendar.DatePicker.prototype.setTimePickerEnabled=function(_780){
this.setBoolAttribute("timepickerenabled",_780);
};
nitobi.calendar.DatePicker.prototype.getWidth=function(){
return this.getIntAttribute("width",180);
};
nitobi.calendar.DatePicker.prototype.getHeight=function(){
return this.getIntAttribute("height",192);
};
nitobi.calendar.DatePicker.prototype.getCssClass=function(){
return this.getAttribute("cssclass","");
};
nitobi.calendar.DatePicker.prototype.getState=function(){
return this;
};
nitobi.calendar.DatePicker.prototype.getFormattedDate=function(){
return this.getFormatter().call(this,this.getDate());
};
nitobi.calendar.DatePicker.prototype.nextMonth=function(){
var date=this.getStartDate();
date=nitobi.base.DateMath.getMonthStart(nitobi.base.DateMath.add(date,"d",42));
this.setStartDate(date);
this.render();
};
nitobi.calendar.DatePicker.prototype.prevMonth=function(){
var date=this.getStartDate();
date=nitobi.base.DateMath.getMonthStart(nitobi.base.DateMath.add(date,"d",-12));
this.setStartDate(date);
this.render();
};
nitobi.calendar.DatePicker.prototype.getFormatter=function(){
if(this.formatter){
return this.formatter;
}
eval("var f = "+this.getAttribute("formatter","nitobi.base.DateMath.toIso8601"));
return this.formatter=f;
};
nitobi.calendar.DatePicker.prototype.setFormatter=function(_783){
this.formatter=_783;
};
nitobi.calendar.DatePicker.prototype.handleClick=function(evt,_785){
var td=evt.srcElement;
if(td.nodeName!="TD"){
return;
}
var _787=this.getDate();
if(_787){
var days=nitobi.base.DateMath.getNumberOfDays(this.getStartDate(),_787)-1;
if(days>=0&&days<42){
var row=2+Math.floor(days/7);
var col=days%7;
var _78b=this.getHtmlNode("table");
nitobi.html.Css.removeClass(_78b.rows[row].cells[col],"ntb-dp-currentday");
}
}
var tr=_785;
nitobi.html.Css.addClass(td,"ntb-dp-currentday");
var date=nitobi.base.DateMath.add(nitobi.base.DateMath.clone(this.getStartDate()),"d",(tr.rowIndex-2)*7+td.cellIndex);
this.setDate(date);
};
nitobi.calendar.DatePicker.prototype.render=function(){
nitobi.calendar.DatePicker.base.render.call(this);
var rows=this.getHtmlNode().getElementsByTagName("tr");
for(var i=2;i<8;i++){
nitobi.html.attachEvent(rows[i],"click",this.handleClick,this);
}
nitobi.html.attachEvent(this.getHtmlNode("nextmonth"),"anyclick",this.nextMonth,this);
nitobi.html.attachEvent(this.getHtmlNode("prevmonth"),"anyclick",this.prevMonth,this);
};
nitobi.calendar.DatePicker.prototype.getMonthNames=function(){
return this.monthNames||(this.monthNames=nitobi.calendar.DatePicker.defaultMonthNames);
};
nitobi.calendar.DatePicker.prototype.setMonthNames=function(_790){
this.monthNames=_790;
};
nitobi.calendar.DatePicker.prototype.getDayNames=function(){
return this.dayNames||(this.dayNames=nitobi.calendar.DatePicker.defaultDayNames);
};
nitobi.calendar.DatePicker.prototype.setDayNames=function(_791){
this.dayNames=_791;
};
nitobi.calendar.DatePicker.defaultMonthNames=["January","February","March","April","May","June","July","August","September","October","November","December"];
nitobi.calendar.DatePicker.defaultDayNames=["S","M","T","W","T","F","S"];
nitobi.lang.defineNs("nitobi.calendar");
nitobi.calendar.Renderer=function(){
nitobi.html.IRenderer.call(this);
};
nitobi.lang.implement(nitobi.calendar.Renderer,nitobi.html.IRenderer);
nitobi.calendar.Renderer.prototype.renderToString=function(_792){
var _793=nitobi.base.DateMath;
var _794=_792.getDate();
var _795=_792.getStartDate();
var date=_795.getDate();
var _797=_794?_793.getNumberOfDays(_795,_794)-1:1000;
var _798=_793.getMonthDays(_795)-_795.getDate()+1;
var _799=_793.add(_793.clone(_795),"d",_798);
var _79a=_793.getMonthDays(_799);
_79a=_79a+_798>42?42-_798:_79a;
var id=_792.getId();
var _79c=_798>_79a;
var year=_79c?_795.getFullYear():_799.getFullYear();
var _79e=_792.getMonthNames();
var _79f=_79e[(_795.getMonth()+!_79c)%12];
var _7a0=_792.getDayNames();
var str=new nitobi.lang.StringBuilder();
var _7a2=true;
var _7a3=false;
var _7a4=false;
str.append("<div onselectstart=\"return false;\" id=\""+id+"\" class=\"ntb-dp\" style=\"width:"+_792.getWidth()+"px;height:"+_792.getHeight()+"px;\"><div id=\""+id+".themer\" style=\"width:100%;height:100%;\" class=\""+_792.getCssClass()+"\">");
str.append("<table id=\""+id+".table\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:100%;height:100%;\" class=\"ntb-dp-table\">");
str.append("<thead><tr class=\"ntb-dp-monthheader\">");
str.append("<th><a id=\""+id+".prevmonth\" class=\"ntb-dp-prevmonth\" href=\"#\" onclick=\"return false;\">&#9650;</a></th>");
str.append("<th colspan=\"5\" style=\"width:70%;overflow-x:hidden;\">");
str.append("<a id=\""+id+".month\" class=\"ntb-dp-month\" href=\"#\" onclick=\"return false;\">"+_79f+"</a> ");
str.append("<a id=\""+id+".year\" class=\"ntb-dp-year\" href=\"#\" onclick=\"return false;\">"+year+"</a>");
str.append("</th>");
str.append("<th><a id=\""+id+".nextmonth\" class=\"ntb-dp-nextmonth\" href=\"#\" onclick=\"return false;\">&#9660;</a></th>");
str.append("</tr><tr>");
for(var i=0;i<7;i++){
str.append("<th class=\"ntb-dp-dayheader\">"+_7a0[i]+"</th>");
}
str.append("</tr></thead><tbody>");
for(var i=0;i<6;i++){
str.append("<tr>");
for(var j=0;j<7;j++){
str.append("<td class=\"");
str.append(!_797--?"ntb-dp-currentday ":"");
if(!_79c&&_7a2){
str.append("ntb-dp-lastmonth ");
}else{
if((_79c&&_7a2)||(!_79c&&_7a3)){
str.append("ntb-dp-thismonth ");
}else{
str.append("ntb-dp-nextmonth");
}
}
str.append(" ntb-dp-day\">"+date+"</td>");
if(_798){
if(!--_798){
_7a2=false;
_7a3=true;
date=1;
}else{
date++;
}
}else{
if(date==_79a){
_7a3=false;
_7a4=true;
}
date=date==_79a?1:date+1;
}
}
str.append("</tr>");
}
str.append("</tbody><colgroup span=\"7\" style=\"width:1/7%\"></colgroup></table><input id=\""+id+".value\" name=\""+id+"\" type=\"hidden\" value=\""+_792.getFormattedDate()+"\" /></div></div>");
return str.toString();
};


var temp_ntb_apiDoc='<?xml version="1.0" ?><interfaces>	<interface name="nitobi.grid.Cell" tagname="ntb:cell" 			remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaCellApiDocumentation" 			examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 			summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaCellApiDocumentation">		<summary>nitobi.grid.Cell represents a single data cell in a Grid.</summary>		<properties>			<property name="Row" type="int" access="public" persist="js" default=""				readwrite="read" impact="xsl row" testvalue="1">			</property>			<property name="Column" type="int" access="public" persist="js" default=""				readwrite="read" impact="xsl row" testvalue="1">			</property>			<property name="DomNode" type="xml" access="public" persist="js" default=""				readwrite="read" impact="xsl row" testvalue="1">			</property>			<property name="DataNode" type="xml" access="public" persist="js" default=""				readwrite="read" impact="xsl row" testvalue="1">			</property>		</properties>		<methods>                                   <method name="getCellElement" access="private"></method>            <method name="getRowNumber" access="private"></method>            <method name="getColumnNumber" access="private"></method>                                   <method name="Focus" access="public"></method>		</methods>	</interface>		<interface name="nitobi.grid.Columns" ></interface>		<interface name="nitobi.grid.Column" tagname="ntb:column" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaColumnApiDocumentation" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaColumnApiDocumentation">		<summary>nitobi.grid.Column represents a single column of data in a Grid.</summary>		<properties>			<property name="Align" type="string" access="private" persist="model" model="Align" default="&quot;left&quot;"				readwrite="readwrite" impact="xsl row " htmltag="align" testvalue="&quot;&quot;">							</property>			<property name="ClassName" type="string" access="private" persist="model" model="ClassName" default="&quot;&quot;"				readwrite="readwrite" impact="xsl row " htmltag="classname" testvalue="&quot;&quot;">							</property>			<property name="CssStyle" type="string" access="private" persist="model" model="CssStyle" default="&quot;&quot;"				readwrite="readwrite" impact="xsl row " htmltag="cssstyle" testvalue="&quot;&quot;">							</property>			<property name="ColumnName" type="string" access="private" persist="model" model="ColumnName" default="&quot;&quot;"				readwrite="readwrite" impact="xsl row" htmltag="columnname" testvalue="&quot;&quot;">			</property>			<property name="Type" type="string" access="private" persist="model" model="type" code="" default="&quot;text&quot;"				impact="row" readwrite="readwrite" htmltag="type" testvalue="&quot;pcm&quot;">			</property>			<property name="DataType" type="string" access="public" persist="model" model="DataType" default="text" 				readwrite="readwrite" impact="xsl row" code="" testvalue="&quot;text&quot;">			</property>			<property name="Editable" type="bool" access="public" persist="model" model="Editable" default="true" 				readwrite="readwrite" impact="model" htmltag="editable" testvalue="false">			</property>			<property name="Initial" type="string" access="public" default="&quot;&quot;" htmltag="initial"				readwrite="readwrite" persist="model" model="Initial" testvalue="&quot;test&quot;">			</property>			<property name="Label" model="Label" type="string" access="public" default="&quot;&quot;"				htmltag="label" readwrite="read" persist="model meta" impact="xsl row" testvalue="&quot;test&quot;">			</property>			<property name="GetHandler" type="string" access="private" default="&quot;&quot;"				persist="model" model="GetHandler" htmltag="gethandler" readwrite="readwrite" impact="xsl row" testvalue="&quot;test&quot;">			</property>						<property name="DataSource" type="string" access="private" default="&quot;&quot;"				persist="model" model="DataSource" htmltag="datasource" readwrite="readwrite" impact="xsl row" testvalue="&quot;test&quot;">			</property>			<property name="Template" type="string" access="private" default="&quot;&quot;"				persist="model" model="Template" htmltag="template" readwrite="readwrite" impact="xsl row" testvalue="&quot;test&quot;">			</property>			<property name="TemplateUrl" type="string" access="private" default="&quot;&quot;"				persist="model" model="TemplateUrl" htmltag="templateurl" readwrite="readwrite" impact="xsl row" testvalue="&quot;test&quot;">			</property>			<property name="MaxLength" type="int" access="public" default="255" htmltag="maxlength" readwrite="readwrite"				persist="model meta" impact="xsl row" model="maxlength" testvalue="200">			</property>			<property name="SortDirection" model="SortDirection" type="string" access="public"				default="&quot;Desc&quot;" htmltag="sortdirection" readwrite="readwrite" persist="model" impact="sort"				testvalue="&quot;Desc&quot;">			</property>			<property name="SortEnabled" model="SortEnabled" type="bool" access="public"				default="true" htmltag="sortenabled" readwrite="readwrite" persist="model" impact="sort"				testvalue="true">			</property>			<property name="Width" model="Width" type="int" access="public" default="100" htmltag="width"				readwrite="readwrite" persist="model" impact="size css row" testvalue="200">				<include path="//*[@id=\'widthsample\']" type="example"/>			</property>			<property name="Visible" model="Visible" type="bool" access="private" default="true" htmltag="visible"				readwrite="readwrite" persist="model" impact="size css row" testvalue="true">			</property>			<property name="xdatafld" type="string" access="public" default="&quot;&quot;" readwrite="read"				persist="meta model" model="xdatafld" htmltag="xdatafld">			</property>			<property name="Value" type="string" access="public" default="&quot;&quot;" readwrite="read"				persist="meta model" model="Value" htmltag="value">			</property>			<property name="xi" type="int" access="private" default="100" htmltag="xi" readwrite="read"				persist="meta model" model="xi" short="xi">			</property>			<property name="Editor" model="Editor" namespace="Eba.Grid" type="Editor" access="private" default="Eba.Grid.TextEditor" htmltag="editor"				readwrite="readwrite" persist="model" impact="" testvalue="true">			</property>		</properties>		<events>			<event name="OnCellClickEvent" model="OnCellClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellclickevent"				persist="model"></event>			<event name="OnBeforeCellClickEvent" model="OnBeforeCellClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforecellclickevent"				persist="model"></event>			<event name="OnCellDblClickEvent" model="OnCellDblClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncelldblclickevent"				persist="model"></event>			<event name="OnHeaderDoubleClickEvent" model="OnHeaderDoubleClickEvent" type="string" access="private" default="&quot;&quot;"				readwrite="readwrite" htmltag="onheaderdoubleclickevent" persist="model"></event>			<event name="OnHeaderClickEvent" model="OnHeaderClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onheaderclickevent"				persist="model"></event>			<event name="OnBeforeResizeEvent" model="OnBeforeResizeEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforeresizeevent"				persist="model"></event>			<event name="OnAfterResizeEvent" model="OnAfterResizeEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterresizeevent"				persist="model"></event>						<event name="OnCellValidateEvent" model="OnCellValidateEvent" type="string" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellvalidateevent"				persist="model"></event>			<event name="OnBeforeCellEditEvent" model="OnBeforeCellEditEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforecelleditevent"				persist="model"></event>			<event name="OnAfterCellEditEvent" model="OnAfterCellEditEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftercelleditevent"				persist="model"></event>			<event name="OnCellBlurEvent" model="OnCellBlurEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellblurevent"				persist="model"></event>			<event name="OnCellFocusEvent" model="OnCellFocusEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellfocusevent"				persist="model"></event>			<event name="OnBeforeSortEvent" model="OnBeforeSortEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforesortevent"				persist="model"></event>			<event name="OnAfterSortEvent" model="OnAfterSortEvent" type="String" access="private" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftersortevent"				persist="model"></event>			<event name="OnCellUpdateEvent" model="OnCellUpdateEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellupdateevent"				persist="model"></event>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent" persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent" persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent" persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent" persist="model"></event>		</events>	</interface>	<interface name="EBADateColumn" tagname="ntb:datecolumn" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaDateColumnApiDocumentation" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaDateColumnApiDocumentation">		<properties>			<property name="Mask" htmltag="mask" type="string" persist="model" model="Mask"				access="public" readwrite="readwrite" default="&quot;M/d/yyyy&quot;">							</property>			<property name="CalendarEnabled" htmltag="calendarenabled" type="bool" persist="model" model="CalendarEnabled"				access="public" readwrite="readwrite" default="true">						</property>		</properties>	</interface>	<interface name="EBANumberColumn" tagname="ntb:numbercolumn" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaDateColumnApiDocumentation" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaDateColumnApiDocumentation">		<properties>			<property name="Align" type="string" access="private" persist="model" model="Align" default="&quot;right&quot;"				readwrite="readwrite" impact="xsl row " htmltag="align" testvalue="&quot;&quot;">							</property>			<property name="Mask" htmltag="mask" type="string" persist="model" model="Mask" access="public" readwrite="readwrite" default="&quot;#,###.00&quot;" />			<property name="NegativeMask" htmltag="negativemask" type="string" persist="model" model="NegativeMask" access="public" readwrite="readwrite" default="&quot;&quot;" />			<property name="GroupingSeparator" htmltag="groupingseparator" type="string" persist="model" model="GroupingSeparator"				access="public" readwrite="readwrite" default="&quot;,&quot;">							</property>			<property name="DecimalSeparator" htmltag="decimalseparator" type="string" persist="model" model="DecimalSeparator"				access="public" readwrite="readwrite" default="&quot;.&quot;">							</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBATextColumn" tagname="ntb:textcolumn" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextApiDocumentation" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextApiDocumentation">	</interface>	<interface name="EBALookupEditor" tagname="ntb:lookupeditor" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaLookupEditor" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaLookupEditor">				<properties>			<property name="DatasourceId" htmltag="datasourceid" type="string" persist="model" model="DatasourceId" 				access="public" readwrite="readwrite" default="">			</property>			<property name="Datasource" htmltag="datasource" type="string" persist="model" model="Datasource" 				access="public" readwrite="readwrite" default="">			</property>						<property name="GetHandler" htmltag="gethandler" type="string" persist="model" model="GetHandler" 				access="public" readwrite="readwrite" default="">			</property>			<property name="DisplayFields" htmltag="displayfields" type="string" persist="model" model="DisplayFields"				access="public" readwrite="readwrite" default="">			</property>			<property name="ValueField" htmltag="valuefield" type="string" persist="model" model="ValueField"				access="public" readwrite="readwrite" default="">			</property>			<property name="Delay" htmltag="delay" type="string" persist="model" model="Delay"				access="public" readwrite="readwrite" default="">			</property>			<property name="Size" htmltag="size" type="string" persist="model" model="Size"				access="public" readwrite="readwrite" default="6">			</property>			<property name="ForceValidOption" htmltag="forcevalidoption" type="bool" model="ForceValidOption"				access="public" readwrite="readwrite" default="false">			</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBACheckboxEditor" tagname="ntb:checkboxeditor" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaCheckboxEditor" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaCheckboxEditor">		<properties>			<property name="DatasourceId" htmltag="datasourceid" type="string" persist="model" model="DatasourceId" 				access="public" readwrite="readwrite" default="">								<include path="//*[@id=\'staticdatacheckboxeditor\']" type="example" />							</property>			<property name="Datasource" htmltag="datasource" type="string" persist="model" model="Datasource" 				access="public" readwrite="readwrite" default="">							</property>			<property name="GetHandler" htmltag="gethandler" type="string" persist="model" model="GetHandler" 				access="public" readwrite="readwrite" default="">				<summary>Specifies the URL of the CheckboxEditor\'s gethandler.  The gethandler must return valid XML data in the EBA format.</summary>								<include path="//*[@id=\'staticdatacheckboxeditor\']" type="example" />							</property>			<property name="DisplayFields" htmltag="displayfields" type="string" persist="model" model="DisplayFields"				access="public" readwrite="readwrite" default="">				<summary>Specifies what fields from the datasource specified by DatasourceId or by the GetHandler will populate the CheckboxEditor\'s listbox.</summary>				<remarks>DisplayFields is a pipe-delimited list of data fields (eg. "field1|field2|field3").</remarks>								<include path="//*[@id=\'staticdatacheckboxeditor\']" type="example" />			</property>			<property name="ValueField" htmltag="valuefield" type="string" persist="model" model="ValueField"				access="public" readwrite="readwrite" default="">				<summary>Specifies the field of the CheckboxEditor\'s data source that will populate the cell.</summary>								<include path="//*[@id=\'staticdatacheckboxeditor\']" type="example" />			</property>			<property name="CheckedValue" htmltag="checkedvalue" type="string" persist="model" model="CheckedValue"				access="public" readwrite="readwrite" default="">							</property>			<property name="UnCheckedValue" htmltag="uncheckedvalue" type="string" persist="model" model="UnCheckedValue"				access="public" readwrite="readwrite" default="">							</property>		</properties>	</interface>	<interface name="EBAImageEditor" tagname="ntb:imageeditor" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaImageEditor" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaImageEditor">				<properties>			<property name="ImageUrl" htmltag="imageurl" type="string" persist="model" model="ImageUrl"				access="public" readwrite="readwrite" default="">			</property>		</properties>	</interface>	<interface name="EBALinkEditor" tagname="ntb:linkeditor" 		remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaLinkEditor" 		examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets" 		summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaLinkEditor">		<properties>		</properties>	</interface>		<interface name="EBATextEditor" tagname="ntb:texteditor" 	remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextEditor"     examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets"     summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextEditor">		<properties>			<property name="MaxLength" htmltag="maxlength" type="int" persist="model" model="MaxLength"				access="public" readwrite="readwrite" default="255">							</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBANumberEditor" tagname="ntb:numbereditor">		<properties>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBATextareaEditor" tagname="ntb:textareaeditor" namespace="Eba.Grid" type="Eba.Grid.TextareaEditor" inherits="Editor" jstype="object" 	remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextAreaEditor"     examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets"     summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaTextAreaEditor">				<properties>			<property name="MaxLength" htmltag="maxlength" type="int" persist="model" model="MaxLength"				access="public" readwrite="readwrite" default="255">							</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBALinkEditor" tagname="ntb:linkeditor">		<properties>			<property name="OpenWindow" htmltag="openwindow" type="bool" persist="model" model="OpenWindow"				access="public" readwrite="readwrite" default="true">							</property>		</properties>	</interface>	<interface name="EBADateEditor" tagname="ntb:dateeditor" namespace="Eba.Grid" type="Eba.Grid.DateEditor" inherits="Editor" jstype="object">		<properties>			<property name="Mask" htmltag="mask" type="string" persist="model" model="Mask"				access="public" readwrite="readwrite" default="&quot;M/d/yyyy&quot;">							</property>			<property name="CalendarEnabled" htmltag="calendarenabled" type="bool" persist="model" model="CalendarEnabled"				access="public" readwrite="readwrite" default="true">						</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="EBAListboxEditor" tagname="ntb:listboxeditor" >			<properties>			<property name="DatasourceId" htmltag="datasourceid" type="string" persist="model" model="DatasourceId" 				access="public" readwrite="readwrite" default="">							</property>			<property name="Datasource" htmltag="datasource" type="string" persist="model" model="Datasource" 				access="public" readwrite="readwrite" default="">							</property>			<property name="GetHandler" htmltag="gethandler" type="string" persist="model" model="GetHandler" 				access="public" readwrite="readwrite" default="">							</property>			<property name="DisplayFields" htmltag="displayfields" type="string" persist="model" model="DisplayFields"				access="public" readwrite="readwrite" default="">							</property>			<property name="ValueField" htmltag="valuefield" type="string" persist="model" model="ValueField"				access="public" readwrite="readwrite" default="">							</property>		</properties>		<events>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent"				persist="model"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent"				persist="model"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent"				persist="model"></event>			<event name="OnChangeEvent" model="OnChangeEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onchangeevent"				persist="model"></event>		</events>	</interface>	<interface name="nitobi.grid.Row" tagname="ntb:e" namespace="Eba.Grid" type="Eba.Grid.Row" jstype="object">		<elements>		</elements>		<properties>			<property name="ClassName" type="string" access="private" persist="meta" default="&quot;&quot;"				readwrite="readwrite" impact="xsl row " htmltag="ClassName" testvalue="&quot;&quot;">							</property>			<property name="Height" default="23" code="" type="int" persist="meta" impact="row"				access="public" readwrite="readwrite" htmltag="Height" testvalue="50">							</property>		</properties>	</interface>			<interface name="nitobi.grid.Grid" tagname="ntb:grid" namespace="Eba" type="Eba.Grid.Grid" jstype="object" 	remarkfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaGridApiDocumentation"     examplefile="http://portal:8090/cgi-bin/trac.cgi/wiki/SharedCodeSnippets"     summaryfile="http://portal:8090/cgi-bin/trac.cgi/wiki/EbaGridApiDocumentation">		<elements>			<element name="EBADatasources" minoccurs="0" maxoccurs="1" />			<element name="nitobi.grid.Columns" minoccurs="0" maxoccurs="1" />		</elements>                <methods>                        <method name="selectRowByIndex" access="private">                                <summary>Selects a grid cell by row index.</summary>                                <param name="index" type="int">the row index</param>                                <returns type="nitobi.grid.Row"></returns>                                <include path="//*[@id=\'work1\']" type="example"/>                        </method>                                        <method name="selectRowByKey" access="private">                                                         <summary>Selects a grid cell by key.</summary>                                                          <param name="key" type="string">the key of grid data</param>                                                                                            <returns type="nitobi.grid.Row"></returns>                                                       </method>                                                       <method name="selectCellByCoords" access="public">                                <summary>Activates a grid cell. The activated cell is highlighted.                                 Subsequent function calls such as deleteCurrentRow, insertAfterCurrentRow, getActiveColumnObject, getActiveRowObject depends on the new active cell.                                  The row and colum index starts at 0.                                </summary>                                <param name="column" type="int">column number</param>                                <param name="row" type="int">row number</param>                                <returns type=""></returns>                                     <include type="remark" />                                               </method>                                        <method name="save" access="public">                                <summary>Saves data in the grid.</summary>                                <returns type=""></returns>                        </method>                                                       <method name="insertAfterCurrentRow" access="public">                                <summary>Insert a new row after the row of the active cell.</summary>                                <returns type=""></returns>                        </method>                                        <method name="deleteCurrentRow" access="public">                                <summary>Deletes currently selected row.</summary>                                <returns type=""></returns>                        </method>                                                <method name="insertRow" access="public">                                <summary>Inserts a new row into the grid</summary>                                <returns type=""></returns>                        </method>                                                                       <method name="getCellObject" access="public">                                <summary>Returns the cell object of a grid.</summary>                                <param name="column" type="int">column number</param>                                <param name="row" type="int">row number</param>                                <returns type="nitobi.grid.Cell"></returns>                        </method>                        <method name="getRowObject" access="public">                                <summary>Returns a row object.</summary>                                <param></param>                                <returns type="nitobi.grid.Row"></returns>                        </method>                                                <method name="getRowCount" access="public">                                <summary>Returns the number of rows in the grid.</summary>                                <returns type="int"></returns>                        </method>                                                                       <method name="getSelectedLookupKey" access="public">                                <summary>Returns the selected lookup key</summary>                                <returns type="string"></returns>                        </method>                                                <method name="getSelectedColumnNumber" access="public">                                <summary>Returns the column index of the selected cell. </summary>                                <param name="rel" type="bool">Specifies whether to compensate for frozen columns.</param>                                <returns type="int">Column index of the selected cell.</returns>                        </method>                        <method name="getSelectedColumnObject" access="public">                                <summary>Returns the nitobi.grid.Column object that the selected cell is part of.</summary>                                <returns type="nitobi.grid.Column">nitobi.grid.Column object of the selected cell.</returns>                                                        </method>                        <method name="getSelectedRow" access="private">                                <summary>Returns the row index of the selected cell. </summary>                                <param name="rel" type="bool">Specifies whether to compensate for frozen rows.</param>                                <returns type="int">Row index of the selected cell.</returns>                                                        </method>                        <method name="getSelectedRowObject" access="public">                                <summary>Returns the nitobi.grid.Row object that the selected cell is part of.</summary>                                <returns type="nitobi.grid.Column">nitobi.grid.Row object of the selected cell.</returns>                                                        </method>                        <method name="getSelectedCellObject" access="public">                                <summary>Returns a reference to the nitobi.grid.Cell object representing the currently selected cell in the Grid. </summary>                                                                <returns type="nitobi.grid.Cell">selected nitobi.grid.Cell object.</returns>                                <include  path="//*[@id=\'ebaxml_fielddef_getvalue\']" type="remarks" />                        </method>                        <method name="GridSelection" access="private">                                <summary>The selection object is used during select operations by the user. Its members provide the functionality for displaying the selected(highlighted blocks</summary>                                <param name="oGrid" type="object">A reference to the grid containing the selection</param>                        </method>                        <method name="selectionhighlight" access="private">                                <summary>Highlights the selected area</summary>                        </method>                        <method name="deselect" access="private">                                <summary>Acts as the opposite of highlight</summary>                        </method>                        <method name="containsSelection" access="private">                                <summary>Returns true if the grid contains a valid selection</summary>                        </method>                        <method name="cellIsInSelection" access="private">                                <summary>Returns true if the given Cell is situated inside the active grid selection and the selection is bigger than just one cell.</summary>                        </method>                        <method name="copy" access="private">                                <summary>Copys the current selection into the clipboard. This method stores the data as text with a tab for every column. This is the same format MS Excel uses and therefore the paste method also works with data copied from a MSExcel spreadsheet.</summary>                        </method>                        <method name="paste" access="private">                                <summary>Pasts data from the clipboard into the grid if it contains tabular data. Also pasts data from MSExcel as Excel places data to the clipboard in the form of tabular data as well.</summary>                        </method>                        <method name="getPendingSortColumn" access="public">                                <summary>Retrieves the pending sort column number.</summary>                                <returns type="int">Pending sort column number of the Grid.</returns>                                                        </method>                        <method name="loadNextDataPage" access="public">                                <summary>Loads the next page of data from the database.</summary>                                <remarks>This method requests the data from a getHandler which is a server-side script designed to deliver the requested data.</remarks>                                                                <include path="//*[@id=\'pagingexample\']" type="example" />                        </method>                        <method name="loadPreviousDataPage" access="public">                                <summary>Loads the previous page of data from the database.</summary>                                <remarks>This method requests the data from a getHandler which is a server-side script designed to deliver the requested data.</remarks>                                                                <include path="//*[@id=\'pagingexample\']" type="example" />                        </method>                        <method name="loadDataPage" access="public">                                <summary>Loads the specified page of data from the database.</summary>                                <param name="nStart" type="int">Recordnumber of record which should be display on top of the page.</param>                                                                <include path="//*[@id=\'pagingexample\']" type="example" />                        </method>                        <method name="makeXSL" access="private">                                <summary>Makes the main XSL</summary>                                <remarks>The makeXSL() method is normally called automatically when the grid is first instantiated.</remarks>                        </method>                </methods>		<properties>			<property name="ID" htmltag="id" type="string" access="public" persist="js" readwrite="read"></property>			<property name="uid" type="string" access="public" persist="xml" readwrite="readwrite"></property>			<property name="ToolbarHeight" htmltag="toolbarHeight" type="int" access="public" persist="js" readwrite="readwrite" default="25"></property>						<property name="Selection" type="EBASelection" access="public" persist="js" readwrite="read" default="null"></property>			<property name="Bound" type="bool" access="public" persist="js" readwrite="readwrite" default="false"></property>			<property name="RegisteredTo" htmltag="registeredto" type="string" access="public" persist="js" default="true"				readwrite="read" testvalue="test"></property>			<property name="LicenseKey" htmltag="licensekey" type="string" access="public" persist="js" default="true"				readwrite="read" testvalue="test"></property>			<property name="ToolbarContainerEmpty" type="bool" access="private" persist="xml" default="false"				readwrite="readwrite" testvalue="test">			</property>			<property name="Columns" htmltag="columns" namespace="Eba.Grid" type="Column" access="public" persist="js" default="true"				readwrite="read" testvalue="test"></property>			<property name="ColumnsDefined" htmltag="columnsdefined" type="bool" access="public" persist="js" default="false"				readwrite="readwrite" testvalue="true"></property>			<property name="Declaration" htmltag="declaration" type="xml" access="private" persist="js" default="&quot;&quot;"				readwrite="readwrite" testvalue="&quot;&quot;"></property>			<property name="Datasource" htmltag="datasource" namespace="Eba.Data" type="DatasourceManager" access="public" persist="js" default="true"				readwrite="read" testvalue="test"></property>			<property name="DatasourceId" htmltag="datasourceid" type="string" access="public" persist="xml" default=""				readwrite="read" testvalue="testds"></property>			<property name="CurrentPageIndex" htmltag="currentpageindex" type="int" access="public" persist="xml" default="0"				readwrite="read" testvalue="0"></property>			<property name="ColumnIndicatorsEnabled" htmltag="columnindicatorsenabled" type="bool" access="public" persist="xml" default="true"				readwrite="readwrite" testvalue="false"></property>			<property name="RowIndicatorsEnabled" type="bool" access="private" persist="xml" default="false"				readwrite="readwrite" testvalue="false">			</property>			<property name="ToolbarEnabled" htmltag="toolbarenabled" type="bool" access="public" persist="xml" default="true" readwrite="readwrite"				testvalue="false"></property>			<property name="RowHighlightEnabled" htmltag="rowhighlightenabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false"></property>			<property name="RowSelectEnabled" htmltag="rowselectenabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false" >			</property>			<property name="GridResizeEnabled" htmltag="gridresizeenabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false"></property>			<property name="WidthFixed" htmltag="widthfixed" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="true"></property>						<property name="HeightFixed" htmltag="heightfixed" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="true"></property>									<property name="MinWidth" htmltag="minwidth" type="int" access="public" persist="xml" default="20" readwrite="readwrite"				testvalue="100"></property>						<property name="MinHeight" htmltag="minheight" type="int" access="public" persist="xml" default="0" readwrite="readwrite"				testvalue="100"></property>												<property name="SingleClickEditEnabled" htmltag="singleclickeditenabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false"></property>			<property name="AutoKeyEnabled" htmltag="autokeyenabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false">			</property>			<property name="ToolTipsEnabled" type="bool" access="private" persist="xml" default="true" readwrite="readwrite"				testvalue="false">			</property>			<property name="EnterTab" type="string" access="public" persist="xml" default="down" readwrite="readwrite"				htmltag="entertab" testvalue="up">			</property>			<property name="HScrollbarEnabled" type="bool" access="private" persist="xml" default="true" readwrite="readwrite"				testvalue="false">			</property>			<property name="VScrollbarEnabled" type="bool" access="private" persist="xml" default="true" readwrite="readwrite"				testvalue="false">			</property>			<property name="RowHeight" type="int" access="private" persist="xml" default="23" readwrite="read"				htmltag="rowheight" testvalue="50">			</property>			<property name="HeaderHeight" type="int" persist="xml" access="private" default="23" readwrite="readwrite"				htmltag="headerheight" testvalue="50">			</property>			<property name="cellWidth" type="int" persist="xml" access="private" default="100" readwrite="read"				testvalue="200">			</property>			<property name="top" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				impact="css xsl row" testvalue="200">			</property>			<property name="bottom" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				impact="css xsl row" testvalue="200">			</property>			<property name="left" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				impace="css xsl row" testvalue="200">			</property> 			<property name="right" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				impact="css xsl row" testvalue="200">			</property>			<property name="indicatorWidth" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="50">			</property>			<property name="scrollbarWidth" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="22" default="22">			</property>			<property name="scrollbarHeight" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="22" default="22">			</property>			<property name="freezetop" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				impact="size css xsl row" testvalue="2">			</property>			<property name="freezebottom" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="2">			</property>			<property name="FrozenLeftColumnCount" htmltag="frozenleftcolumncount" default="0" type="int" persist="xml" access="public" readwrite=""				testvalue="2">							</property>			<property name="freezeright" default="0" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="2">							</property>			<property name="active" type="string" access="private" default="&quot;&quot;">			</property>			<property name="activeCell" type="nitobi.grid.Cell" access="private" default="null" readwrite="readwrite">			</property>			<property name="activeRow" type="object" access="private" persist="xml" default="null">							</property>			<property name="RowInsertEnabled" type="bool" access="public" persist="xml" default="true" htmltag="rowinsertenabled"				readwrite="readwrite">							</property>			<property name="RowDeleteEnabled" type="bool" persist="xml" access="public" default="true" htmltag="rowdeleteenabled" readwrite="readwrite">							</property>			<property name="Asynchronous" type="bool" access="private" persist="xml" default="true" readwrite="readwrite"				htmltag="asynchronous" testvalue="false">							</property>			<property name="AutoAdd" type="bool" access="private" default="false" htmltag="autoadd">							</property>			<property name="AutoSaveEnabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				htmltag="autosaveenabled" testvalue="true">							</property>			<property name="contentHeight" default="1000" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="2000">							</property>			<property name="contentWidth" default="1000" type="int" persist="xml" access="private" readwrite="readwrite"				testvalue="2000">							</property>			<property name="ColumnCount" type="int" access="public" persist="xml" default="0" readwrite="read"				 testvalue="20">							</property>			<property name="RowsPerPage" type="int" access="public" persist="xml" default="20" readwrite="readwrite"				htmltag="rowsperpage" testvalue="20">								<include path="//*[@id=\'pagingexample\']" type="example" />			</property>			<property name="element" code="" type="Span" persist="dom" access="private" readwrite="read">							</property>			<property name="entertab" type="string" access="private" persist="xml" default="&quot;RT&quot;"				htmltag="entertab">			</property>			<property name="forceValidate" type="bool" access="private" persist="xml" default="false"				readwrite="readwrite">			</property>			<property name="gridColor" type="string" access="private" persist="xml" default="&quot;#F0F0F0&quot;"				htmltag="gridColor">			</property>			<property name="Height" code="" persist="xml" type="int" access="public" default="100" readwrite="read"				htmltag="height" testvalue="200">							</property>			<property name="hwrap" type="bool" access="private" persist="xml" default="true" htmltag="hwrap">			</property>			<property name="keymode" type="string" access="private" default="&quot;&quot;" htmltag="keymode">			</property>			<property name="KeyGenerator" type="string" access="public" default="&quot;&quot;"				readwrite="readwrite" htmltag="keygenerator" persist="js">				<include path="//*[@id=\'keygeneration\']" type="example" />							</property>			<property name="LastError" type="string" access="public" default="&quot;&quot;" readwrite="read"				persist="xml" testvalue="&quot;testError&quot;">							</property>			<property name="lastSaveHandlerResponse" type="string" access="private" default="&quot;&quot;">							</property>			<property name="MultiRowSelectEnabled" type="bool" access="public" persist="xml" default="false" readwrite="readwrite"				testvalue="false" htmltag="multirowselectenabled">			</property>			<property name="MultiRowSelectField" type="string" access="public" persist="xml" default="" readwrite="readwrite"			 	testvalue="" htmltag="multirowselectfield">			</property>			<property name="MultiRowSelectAttr" type="string" access="private" persist="xml" default="" readwrite="readwrite"			 	>			</property>			<property name="scrolling" type="bool" access="private" default="false">			</property>			<property name="GetHandler" type="string" access="public" persist="xml" default="&quot;&quot;" htmltag="gethandler">								<include path="//*[@id=\'saveget\']" type="example" />			</property>			<property name="SaveHandler" type="string" access="public" persist="xml" default="&quot;&quot;" htmltag="savehandler">								<include path="//*[@id=\'saveget\']" type="example" />			</property>			<property name="scrollX" type="string" access="private" persist="xml" code="" readwrite="readwrite"				htmltag="scrollX" testvalue="&quot;scroll&quot;">							</property>			<property name="scrollY" type="string" access="private" persist="xml" default="&quot;auto&quot;"				readwrite="readwrite" htmltag="scrollY" testvalue="&quot;visible&quot;">							</property>			<property name="showErrors" type="bool" access="private" default="false" readwrite="readwrite"				htmltag="showErrors">							</property>			<property name="uniqueID" default="&quot;&quot;" code="" type="object" access="public" readwrite="read">							</property>			<property name="Version" default="3.01" code="" type="string" persist="js" access="public"				readwrite="read" htmltag="version">							</property>			<property name="vwrap" type="bool" access="private" persist="xml" default="true" htmltag="vwrap">			</property>			<property name="Width" type="int" access="public" persist="xml" readwrite="read" htmltag="width"				testvalue="1000">							</property>			<property name="PagingMode" type="string" access="public" persist="xml" readwrite="read" htmltag="pagingmode" default="&quot;LiveScrolling&quot;">							</property>			<property name="DataMode" type="string" access="public" persist="xml" readwrite="read" htmltag="datamode" default="&quot;Caching&quot;">							</property>			<property name="RenderMode" type="string" access="public" persist="xml" readwrite="read" htmltag="rendermode" default="&quot;&quot;">							</property>			<property name="CopyEnabled" type="bool" access="public" persist="xml" readwrite="readwrite" htmltag="copyenabled" default="true">			</property>			<property name="PasteEnabled" type="bool" access="public" persist="xml" readwrite="readwrite" htmltag="pasteenabled" default="true">			</property>			<property name="SortEnabled" model="SortEnabled" type="bool" access="public"				default="true" htmltag="sortenabled" readwrite="readwrite" persist="xml" impact="sort"				testvalue="true"></property>			<property name="SortMode" model="SortMode" type="string" access="public"				default="default" htmltag="sortmode" readwrite="readwrite" persist="xml" impact="sort"				testvalue="default"></property>		</properties>		<events>			<event name="OnCellClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellclickevent" persist="event"></event>			<event name="OnBeforeCellClickEvent" model="OnBeforeCellClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforecellclickevent"				persist="event"></event>			<event name="OnCellDblClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncelldblclickevent" persist="event"></event>			<event name="OnDataReadyEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="ondatareadyevent" persist="event"></event>			<event name="OnHtmlReadyEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onhtmlreadyevent" persist="event"></event>			<event name="OnDataRenderedEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="ondatarenderevent" persist="event"></event>			<event name="OnCellDoubleClickEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncelldoubleclickevent"				persist="event">			</event>			<event name="OnAfterLoadDataPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterloaddatapageevent"				persist="event">			</event>			<event name="OnBeforeLoadDataPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforeloaddatapageevent"				persist="event">			</event>			<event name="OnAfterLoadPreviousPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterloadpreviouspageevent"				persist="event">			</event>			<event name="OnBeforeLoadPreviousPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforeloadpreviouspageevent"				persist="event">			</event>			<event name="OnAfterLoadNextPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterloadnextpageevent"				persist="event">			</event>			<event name="OnBeforeLoadNextPageEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforeloadnextpageevent"				persist="event">			</event>			<event name="OnBeforeCellEditEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforecelleditevent"				persist="event">							</event>			<event name="OnAfterCellEditEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftercelleditevent"				persist="event">							</event>			<event name="OnBeforeRowInsertEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforerowinsertevent"				persist="event">							</event>			<event name="OnAfterRowInsertEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterrowinsertevent"				persist="event">							</event>			<event name="OnBeforeSortEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforesortevent"				persist="event">			</event>			<event name="OnAfterSortEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftersortevent"				persist="event">			</event>			<event name="OnBeforeRefreshEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforerefreshevent"				persist="event">			</event>			<event name="OnAfterRefreshEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterrefreshevent"				persist="event">			</event>						<event name="OnBeforeSaveEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforesaveevent"				persist="event">							</event>			<event name="OnAfterSaveEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftersaveevent"				persist="event">							</event>			<event name="OnHandlerErrorEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onhandlererrorevent"				persist="event">							</event>						<event name="OnRowBlurEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onrowblurevent"				persist="event">							</event>			<event name="OnCellFocusEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellfocusevent"				persist="event">			</event>			<event name="OnFocusEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onfocusevent"				persist="event">			</event>						<event name="OnCellBlurEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellblurevent"				persist="event">							</event>			<event name="OnAfterRowDeleteEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterrowdeleteevent"				persist="event">							</event>			<event name="OnBeforeRowDeleteEvent" type="string" access="public" default="&quot;true&quot;" readwrite="readwrite" htmltag="onbeforerowdeleteevent"				persist="event">							</event>			<event name="OnCellUpdateEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncellupdateevent"				persist="event">			</event>			<event name="OnRowFocusEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onrowfocusevent"				persist="event">							</event>			<event name="OnBeforeCopyEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforecopyevent"				persist="event">							</event>			<event name="OnAfterCopyEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onaftercopyevent"				persist="event">							</event>			<event name="OnBeforePasteEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onbeforepasteevent"				persist="event">							</event>			<event name="OnAfterPasteEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onafterpasteevent"				persist="event">							</event>			<event name="OnErrorEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onerrorevent"				persist="event">							</event>			<event name="OnContextMenuEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="oncontextmenuevent"				persist="event">			</event>			<event name="OnFocusEvent" type="string" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onfocusvent"				persist="event">							</event>			<event name="OnCellValidateEvent" type="string" access="private" default="&quot;&quot;"				readwrite="readwrite" persist="event" htmltag="oncellvalidateevent">				<include path="//*[@id=\'datavalidation\']" type="example" />			</event>			<event name="OnKeyDownEvent" model="OnKeyDownEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeydownevent" persist="event"></event>			<event name="OnKeyUpEvent" model="OnKeyUpEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeyupevent" persist="event"></event>			<event name="OnKeyPressEvent" model="OnKeyPressEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onkeypressevent" persist="event"></event>			<event name="OnMouseOverEvent" model="OnMouseOverEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onmouseoverevent" persist="event"></event>			<event name="OnMouseOutEvent" model="OnMouseOutEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onmouseoutevent" persist="event"></event>			<event name="OnMouseMoveEvent" model="OnMouseMoveEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onmousemoveevent" persist="event"></event>			<event name="OnHitRowEndEvent" model="OnHitRowEndEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onhitrowendevent" persist="event"></event>			<event name="OnHitRowStartEvent" model="OnHitRowStartEvent" type="String" access="public" default="&quot;&quot;" readwrite="readwrite" htmltag="onhitrowstartevent" persist="event"></event>		</events>	</interface></interfaces>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.apiDoc = nitobi.xml.createXmlDoc(temp_ntb_apiDoc);

var temp_ntb_modelDoc='<state	 xmlns:ntb="http://www.nitobi.com"	ID="mySheet"	Version="3.01" 	element="grid" 		uniqueID="_hkj342">    <nitobi.grid.Grid		Height="300"		Width="700"		skin="default"		cellWidth="100"			RowHeight="23"					indicatorHeight="23"		HeaderHeight="23"		indicatorWidth="30"		scrollX="0"		scrollY="0"		scrollbarWidth="26"		scrollbarHeight="26"		toolbarHeight="22"				top="23"		bottom="23"		left="100"		right="100"				minHeight="60"		minWidth="250"		PrimaryDatasourceSize="0" 		contentHeight="1000"		contentWidth="1500"				containerHeight=""		containerWidth=""		columnsdefined="0"		renderframe="0"		renderindicators="0"		renderheader="0"		renderfooter="0"		renderleft="0"		renderright="0"		rendercenter="0"		active="1"		selected="1"		activeRow=""		activeCell=""		activeView=""		activeBlock=""		highlightCell=""		scrolling="0"		prevCell=""		prevText=""		prevData=""		FrozenLeftColumnCount="0"		DatasourceSizeEstimate="0"    	DatasourceId=""  		freezeright="0"		freezetop="0"		freezebottom="0"		ToolbarEnabled="1"			GridResizeEnabled="0"		RowHighlightEnabled="0"		RowSelectEnabled="0"		MultiRowSelectEnabled="0"		AutoKeyEnabled="0"			ToolbarContainerEmpty="false"			ToolTipsEnabled="1"		RowIndicatorsEnabled="0"		ColumnIndicatorsEnabled="1"		HScrollbarEnabled="1"		VScrollbarEnabled="1"		rowselect="0"		AutoSaveEnabled="0"		autoAdd="0"		remoteSort="0"		forceValidate="0"		showErrors="0"		columnGraying="0"		hwrap="0"		vwrap="0"		keymode=""				entertab="RT"		keyboardPaging="0"		RowInsertEnabled="1"		RowDeleteEnabled="1"		allowEdit="1"		allowFormula="1"		PasteEnabled="1"		CopyEnabled="1"				expandRowsOnPaste="1"		expandColumnsOnPast="1"		datalog="myXMLLog"		xselect="//root"		xorder="@a"		asynchronous="1"		fieldMap=""    	GetHandler="" 		getHandler=""		SaveHandler=""		lastSaveHandlerResponse=""		sortColumn="0"		curSortColumn="0"		descending="0"		curSortColumnDesc="0"		RowCount="0"		ColumnCount="0"		nextXK="32"		CurrentPageIndex="0"		PagingMode="standard"		DataMode="caching"		RenderMode=""    	LiveScrollingMode="Leap"		RowsPerPage="20"		pageStart="0"		normalColor="#FFFFFF"		normalColor2="#FFFFFF"		activeColor="#FFFFFF"		selectionColor="#FFFFFF"		highlightColor="#FFFFFF"		columnGrayingColor="#FFFFFF"		gridColor="#FFFFFF"		SingleClickEditEnabled="0"		LastError=""		SortEnabled="1"    	SortMode="default"    	EnterTab="down"    	    	WidthFixed="0"     	HeightFixed="0"    	MinWidth="20"     	MinHeight="0"	>    </nitobi.grid.Grid>    <nitobi.grid.Columns>    </nitobi.grid.Columns>    <Defaults>    	<nitobi.grid.Grid></nitobi.grid.Grid>		<nitobi.grid.Column 			Width="100"			type="TEXT"			Visible="1"			SortEnabled="1"			/>		<nitobi.grid.Row></nitobi.grid.Row>		<nitobi.grid.Cell></nitobi.grid.Cell>		<ntb:e />    </Defaults>    	<declaration>	</declaration>	<columnDefinitions>	</columnDefinitions></state>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.modelDoc = nitobi.xml.createXmlDoc(temp_ntb_modelDoc);

var temp_ntb_toolbarDoc='<?xml version="1.0" encoding="utf-8"?><toolbar id="toolbarthis.uid" title="Grid" height="23" width="110" image_directory="http://localhost/vss/EBALib/v13/Common/Toolbar/Styles/default">	<items>		<button id="save" onclick_event="this.onClick()" height="14" width="14" image="save.gif"			image_disabled="save_disabled.gif" tooltip_text="Save Changes" />		<!-- <button id="discardChanges" onclick_event="testclick(this);" height="17" width="16" top_offset="-2"			image="cancelsave.gif" image_disabled="cancelsave_disabled.gif" tooltip_text="Discard Changes" /> -->		<separator id="toolbar1_separator1" height="20" width="5" image="separator.jpg" />		<button id="newRecord" onclick_event="this.onClick()" height="11" width="14" image="newrecord.gif"			image_disabled="newrecord_disabled.gif" tooltip_text="New Record" />		<button id="deleteRecord" onclick_event="this.onClick()" height="11" width="14" image="deleterecord.gif"			image_disabled="deleterecord_disabled.gif" tooltip_text="Delete Record" />		<separator id="toolbar1_separator2" height="20" width="5" image="separator.jpg" />		<button id="refresh" onclick_event="this.onClick()" height="14" width="16" image="refresh.gif"			image_disabled="refresh_disabled.gif" tooltip_text="Refresh" />		<!--<separator id="toolbar1_separator3" height="20" width="5" image="separator.jpg" />		<button id="toolbar1_button4" onclick_event="testclick(this);" height="11" width="10" image="left.gif"			image_disabled="left_disabled.gif" tooltip_text="Previous Page" />		<button id="toolbar1_button5" onclick_event="testclick(this);" height="11" width="10" image="right.gif"			image_disabled="right_disabled.gif" tooltip_text="Next Page" />		-->	</items></toolbar>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.toolbarDoc = nitobi.xml.createXmlDoc(temp_ntb_toolbarDoc);

var temp_ntb_pagingToolbarDoc='<?xml version="1.0" encoding="utf-8"?><toolbar id="toolbarpagingthis.uid" title="Paging" height="23" width="60" image_directory="http://localhost/vss/EBALib/v13/Common/Toolbar/Styles/default">	<items>		<button id="previousPage" onclick_event="this.onClick()" height="14" width="14" image="left.gif"			image_disabled="left_disabled.gif" tooltip_text="Previous Page" />		<button id="nextPage" onclick_event="this.onClick()" height="14" width="16" image="right.gif"			image_disabled="right_disabled.gif" tooltip_text="Next Page" />	</items></toolbar>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.pagingToolbarDoc = nitobi.xml.createXmlDoc(temp_ntb_pagingToolbarDoc);


var temp_ntb_accessorGeneratorXslProc='<?xml version="1.0"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> <xsl:output method="text" encoding="utf-8" omit-xml-declaration="yes"/> <x:t- match="interface"> <x:ct-x:n-initJSDefaults"/> <x:at-/> </x:t-> <x:t-x:n-initJSDefaults"> </x:t-> <x:t- match="interface/properties"> <x:va-x:n-object"><x:v-x:s-ancestor::interface/@name" /></x:va-> <xsl:for-eachx:s-property"> <x:ct-x:n-generate-accessors"> <x:w-x:n-object"x:s-$object"></x:w-> </x:ct-> </xsl:for-each> </x:t-> <x:t- match="interface/methods"> <xsl:for-eachx:s-method"> <xsl:if test="@code"> this.<x:v-x:s-@name"/>= function(<xsl:for-eachx:s-parameters/parameter"><x:v-x:s-@name" /><xsl:if test="not(last())">,</xsl:if></xsl:for-each>) {<x:v-x:s-@code"/>}; </xsl:if> </xsl:for-each> </x:t-> <x:t- match="interface/events"> <x:va-x:n-object"><x:v-x:s-ancestor::interface/@name" /></x:va-> <xsl:for-eachx:s-event"> <x:ct-x:n-generate-accessors"> <x:w-x:n-object"x:s-$object"></x:w-> </x:ct-> </xsl:for-each> </x:t-> <x:t-x:n-generate-accessors"> <x:p-x:n-object"></x:p-> <x:va-x:n-name"> <xsl:if test="@xml"><x:v-x:s-$object"/>/<x:v-x:s-@xml" /></xsl:if> <xsl:if test="not(@xml)"><x:v-x:s-$object"/>/@<x:v-x:s-@name" /></xsl:if> </x:va-> <xsl:if test="\'a\'=\'a\'"> this.set<x:v-x:s-@name"/> = function() { <x:v-x:s-@precode"/> <xsl:if test="contains(@persist,\'event\')">this.eSET("<x:v-x:s-@name"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'js\')">this.jSET("<x:v-x:s-@name"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'xml\')">this.xSET("<x:v-x:s-$name"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'data\')">this.SETDATA("<x:v-x:s-$name"/>",arguments);</xsl:if> <!-- <xsl:if test="contains(@persist,\'meta\')">this.xSETMETA("<x:v-x:s-@short"/>",arguments);</xsl:if> --> <xsl:if test="contains(@persist,\'model\')">this.xSETMODEL("<x:v-x:s-@model"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'css\')">this.xSETCSS("<x:v-x:s-@htmltag"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'dom\')">this.SETDOM("<x:v-x:s-@name"/>",arguments);</xsl:if> <xsl:if test="contains(@persist,\'tag\')">this.SETTAG("<x:v-x:s-@name"/>",arguments);</xsl:if> <x:v-x:s-@code"/> if (EBAAutoRender) { <xsl:if test="not($object=\'nitobi.grid.Grid\')"> <xsl:if test="contains(@impact,\'config\')">this.grid.initializeModelFromDeclaration();</xsl:if> <xsl:if test="contains(@impact,\'bind\')">this.grid.bind();</xsl:if> <xsl:if test="contains(@impact,\'css\')">this.grid.generateCss();</xsl:if> <xsl:if test="contains(@impact,\'frame\')">this.grid.renderFrame();</xsl:if> <xsl:if test="contains(@impact,\'align\')">this.grid.Scroller.alignSurfaces();</xsl:if> <xsl:if test="contains(@impact,\'size\')">this.grid.resize();</xsl:if> <xsl:if test="contains(@impact,\'xsl\')">this.grid.makeXSL();</xsl:if> <xsl:if test="contains(@impact,\'row\')">this.grid.refilter();</xsl:if> </xsl:if> <xsl:if test="$object=\'nitobi.grid.Grid\'"> <xsl:if test="contains(@impact,\'config\')">this.initializeModelFromDeclaration();</xsl:if> <xsl:if test="contains(@impact,\'bind\')">this.bind();</xsl:if> <xsl:if test="contains(@impact,\'css\')">this.generateCss();</xsl:if> <xsl:if test="contains(@impact,\'frame\')">this.renderFrame();</xsl:if> <xsl:if test="contains(@impact,\'xsl\')">this.makeXSL();</xsl:if> <xsl:if test="contains(@impact,\'row\')">this.refilter();</xsl:if> </xsl:if> }; }; </xsl:if> <x:va-x:n-accessor-prefix"> <x:c-> <x:wh- test="@type=\'bool\'"> <x:v-x:s-\'is\'"/> </x:wh-> <x:o-> <x:v-x:s-\'get\'"/> </x:o-> </x:c-> </x:va-> <xsl:if test="contains(@persist,\'js\') or contains(@persist,\'event\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return this.<x:v-x:s-@name"/>;};</xsl:if> <xsl:if test="contains(@persist,\'xml\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return <x:ct-x:n-cast-type"><x:w-x:n-type"x:s-@type"/><x:w-x:n-expression">this.xGET("<x:v-x:s-$name"/>",arguments)</x:w-><x:w-x:n-default"x:s-@default" /></x:ct->;};</xsl:if> <xsl:if test="contains(@persist,\'data\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return <x:ct-x:n-cast-type"><x:w-x:n-type"x:s-@type"/><x:w-x:n-expression">this.GETDATA("<x:v-x:s-$name"/>",arguments)</x:w-><x:w-x:n-default"x:s-@default" /></x:ct->;};</xsl:if> <xsl:if test="contains(@persist,\'meta\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return <x:ct-x:n-cast-type"><x:w-x:n-type"x:s-@type"/><x:w-x:n-expression">this.xGETMETA("<x:v-x:s-@short"/>",arguments)</x:w-><x:w-x:n-default"x:s-@default" /></x:ct->;};</xsl:if> <xsl:if test="contains(@persist,\'model\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return <x:ct-x:n-cast-type"><x:w-x:n-type"x:s-@type"/><x:w-x:n-expression">this.xGETMODEL("<x:v-x:s-@model"/>",arguments)</x:w-><x:w-x:n-default"x:s-@default" /></x:ct->;};</xsl:if> <xsl:if test="contains(@persist,\'dom\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return this.dGET("<x:v-x:s-@name"/>",arguments);};</xsl:if> <xsl:if test="contains(@persist,\'tag\')">this.<x:v-x:s-$accessor-prefix"/><x:v-x:s-@name"/> = function() {return this.GETTAG("<x:v-x:s-@name"/>",arguments);};</xsl:if> </x:t-> <x:t-x:n-cast-type"> <x:p-x:n-type"/> <x:p-x:n-expression"/> <x:p-x:n-default"x:s-\'true\'"/> <x:c-> <x:wh- test="$type=\'int\'">Number(<x:v-x:s-$expression"/>)</x:wh-> <x:wh- test="$type=\'bool\'">nitobi.lang.toBool(<x:v-x:s-$expression"/>, <x:v-x:s-$default"/>)</x:wh-> <x:o-><x:v-x:s-$expression"/></x:o-> </x:c-> </x:t-> <x:t- match="text()"/></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.accessorGeneratorXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_accessorGeneratorXslProc));

var temp_ntb_addXidXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <x:p-x:n-guid"x:s-0"/><x:t- match="/"> <x:at-/></x:t-><x:t- match="node()|@*"> <xsl:copy> <xsl:if test="not(@xid)"> <x:a-x:n-xid" ><x:v-x:s-generate-id(.)"/><x:v-x:s-position()"/><x:v-x:s-$guid"/></x:a-> </xsl:if> <x:at-x:s-./* | text() | @*"> </x:at-> </xsl:copy></x:t-> <x:t- match="text()"> <x:v-x:s-."/></x:t-></xsl:stylesheet> ';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.addXidXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_addXidXslProc));

var temp_ntb_adjustXiXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes" /> <x:p-x:n-startingIndex"x:s-5"></x:p-> <x:p-x:n-startingGroup"x:s-5"></x:p-> <x:p-x:n-adjustment"x:s--1"></x:p-> <x:t- match="*|@*"> <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:t-> <!--[@id=\'_default\']--> <x:t- match="//ntb:data/ntb:e|@*"> <x:c-> <x:wh- test="number(@xi) &gt;= number($startingIndex)"> <xsl:copy> <x:at-x:s-@*|node()" /> <x:ct-x:n-increment-xi" /> </xsl:copy> </x:wh-> <x:o-> <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:o-> </x:c-> </x:t-> <x:t-x:n-increment-xi"> <x:a-x:n-xi"> <x:v-x:s-number(@xi) + number($adjustment)" /> </x:a-> </x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.adjustXiXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_adjustXiXslProc));

var temp_ntb_dataTranslatorXslProc='<?xml version="1.0"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes" /> <x:p-x:n-start"x:s-0"></x:p-> <x:p-x:n-id"x:s-\'_default\'"></x:p-> <x:p-x:n-xkField"x:s-\'a\'"></x:p-> <x:t- match="//root"> <ntb:grid xmlns:ntb="http://www.nitobi.com"> <ntb:datasources> <ntb:datasource id="{$id}"> <xsl:if test="@error"> <x:a-x:n-error"><x:v-x:s-@error" /></x:a-> </xsl:if> <ntb:datasourcestructure id="{$id}"> <x:a-x:n-FieldNames"><x:v-x:s-@fields" />|_xk</x:a-> <x:a-x:n-Keys">_xk</x:a-> </ntb:datasourcestructure> <ntb:data id="{$id}"> <xsl:for-eachx:s-//e"> <x:at-x:s-."> <x:w-x:n-xi"x:s-position()-1"></x:w-> </x:at-> </xsl:for-each> </ntb:data> </ntb:datasource> </ntb:datasources> </ntb:grid> </x:t-> <x:t- match="e"> <x:p-x:n-xi"x:s-0"></x:p-> <ntb:e> <xsl:copy-ofx:s-@*[not(name() = \'xk\')]"></xsl:copy-of> <xsl:if test="not(@xi)"><x:a-x:n-xi"><x:v-x:s-$start + $xi" /></x:a-></xsl:if> <x:a-x:n-{$xkField}"><x:v-x:s-@xk" /></x:a-> </ntb:e> </x:t-> <x:t- match="lookups"></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.dataTranslatorXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_dataTranslatorXslProc));

var temp_ntb_dateFormatTemplatesXslProc='<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" xmlns:n="http://www.nitobi.com/exslt/numbers" extension-element-prefixes="d n"> <!-- http://java.sun.com/j2se/1.3/docs/api/java/text/SimpleDateFormat.html --><d:ms> <d:m l="31" a="Jan">January</d:m> <d:m l="28" a="Feb">February</d:m> <d:m l="31" a="Mar">March</d:m> <d:m l="30" a="Apr">April</d:m> <d:m l="31" a="May">May</d:m> <d:m l="30" a="Jun">June</d:m> <d:m l="31" a="Jul">July</d:m> <d:m l="31" a="Aug">August</d:m> <d:m l="30" a="Sep">September</d:m> <d:m l="31" a="Oct">October</d:m> <d:m l="30" a="Nov">November</d:m> <d:m l="31" a="Dec">December</d:m></d:ms><d:ds> <d:d a="Sun">Sunday</d:d> <d:d a="Mon">Monday</d:d> <d:d a="Tue">Tuesday</d:d> <d:d a="Wed">Wednesday</d:d> <d:d a="Thu">Thursday</d:d> <d:d a="Fri">Friday</d:d> <d:d a="Sat">Saturday</d:d></d:ds><x:t-x:n-d:format-date"> <x:p-x:n-date-time" /> <x:p-x:n-mask"x:s-\'MMM d, yy\'"/> <x:va-x:n-formatted"> <x:va-x:n-date-time-length"x:s-string-length($date-time)" /> <x:va-x:n-timezone"x:s-\'\'" /> <x:va-x:n-dt"x:s-substring($date-time, 1, $date-time-length - string-length($timezone))" /> <x:va-x:n-dt-length"x:s-string-length($dt)" /> <x:c-> <x:wh- test="substring($dt, 3, 1) = \':\' and substring($dt, 6, 1) = \':\'"> <!--that means we just have a time--> <x:va-x:n-hour"x:s-substring($dt, 1, 2)" /> <x:va-x:n-min"x:s-substring($dt, 4, 2)" /> <x:va-x:n-sec"x:s-substring($dt, 7)" /> <xsl:if test="$hour &lt;= 23 and $min &lt;= 59 and $sec &lt;= 60"> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-\'NaN\'" /> <x:w-x:n-month"x:s-\'NaN\'" /> <x:w-x:n-day"x:s-\'NaN\'" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$min" /> <x:w-x:n-second"x:s-$sec" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </xsl:if> </x:wh-> <x:o-> <!--($neg * -2)--> <x:va-x:n-year"x:s-substring($dt, 1, 4) * (0 + 1)" /> <x:va-x:n-month"x:s-substring($dt, 6, 2)" /> <x:va-x:n-day"x:s-substring($dt, 9, 2)" /> <x:c-> <x:wh- test="$dt-length = 10"> <!--that means we just have a date--> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </x:wh-> <x:wh- test="substring($dt, 14, 1) = \':\' and substring($dt, 17, 1) = \':\'"> <!--that means we have a date + time--> <x:va-x:n-hour"x:s-substring($dt, 12, 2)" /> <x:va-x:n-min"x:s-substring($dt, 15, 2)" /> <x:va-x:n-sec"x:s-substring($dt, 18)" /> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$min" /> <x:w-x:n-second"x:s-$sec" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </x:wh-> </x:c-> </x:o-> </x:c-> </x:va-> <x:v-x:s-$formatted" /> </x:t-><x:t-x:n-d:_format-date"> <x:p-x:n-year" /> <x:p-x:n-month"x:s-1" /> <x:p-x:n-day"x:s-1" /> <x:p-x:n-hour"x:s-0" /> <x:p-x:n-minute"x:s-0" /> <x:p-x:n-second"x:s-0" /> <x:p-x:n-timezone"x:s-\'Z\'" /> <x:p-x:n-mask"x:s-\'\'" /> <x:va-x:n-char"x:s-substring($mask, 1, 1)" /> <x:c-> <x:wh- test="not($mask)" /> <!--replaced escaping with \' here/--> <x:wh- test="not(contains(\'GyMdhHmsSEDFwWakKz\', $char))"> <x:v-x:s-$char" /> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$minute" /> <x:w-x:n-second"x:s-$second" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-substring($mask, 2)" /> </x:ct-> </x:wh-> <x:o-> <x:va-x:n-next-different-char"x:s-substring(translate($mask, $char, \'\'), 1, 1)" /> <x:va-x:n-mask-length"> <x:c-> <x:wh- test="$next-different-char"> <x:v-x:s-string-length(substring-before($mask, $next-different-char))" /> </x:wh-> <x:o-> <x:v-x:s-string-length($mask)" /> </x:o-> </x:c-> </x:va-> <x:c-> <!--took our the era designator--> <x:wh- test="$char = \'M\'"> <x:c-> <x:wh- test="$mask-length >= 3"> <x:va-x:n-month-node"x:s-document(\'\')/*/d:ms/d:m[number($month)]" /> <x:c-> <x:wh- test="$mask-length >= 4"> <x:v-x:s-$month-node" /> </x:wh-> <x:o-> <x:v-x:s-$month-node/@a" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$mask-length = 2"> <x:v-x:s-format-number($month, \'00\')" /> </x:wh-> <x:o-> <x:v-x:s-$month" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'E\'"> <x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &lt; $month]/@l)" /> <x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &gt; 2)" /> <x:va-x:n-y-1"x:s-$year - 1" /> <x:va-x:n-dow"x:s-(($y-1 + floor($y-1 div 4) - floor($y-1 div 100) + floor($y-1 div 400) + $days) mod 7) + 1" /> <x:va-x:n-day-node"x:s-document(\'\')/*/d:ds/d:d[number($dow)]" /> <x:c-> <x:wh- test="$mask-length >= 4"> <x:v-x:s-$day-node" /> </x:wh-> <x:o-> <x:v-x:s-$day-node/@a" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'a\'"> <x:c-> <x:wh- test="$hour >= 12">PM</x:wh-> <x:o->AM</x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'z\'"> <x:c-> <x:wh- test="$timezone = \'Z\'">UTC</x:wh-> <x:o->UTC<x:v-x:s-$timezone" /></x:o-> </x:c-> </x:wh-> <x:o-> <x:va-x:n-padding"x:s-\'00\'" /> <!--removed padding--> <x:c-> <x:wh- test="$char = \'y\'"> <x:c-> <x:wh- test="$mask-length &gt; 2"><x:v-x:s-format-number($year, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(substring($year, string-length($year) - 1), $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'d\'"> <x:v-x:s-format-number($day, $padding)" /> </x:wh-> <x:wh- test="$char = \'h\'"> <x:va-x:n-h"x:s-$hour mod 12" /> <x:c-> <x:wh- test="$h"><x:v-x:s-format-number($h, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(12, $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'H\'"> <x:v-x:s-format-number($hour, $padding)" /> </x:wh-> <x:wh- test="$char = \'k\'"> <x:c-> <x:wh- test="$hour"><x:v-x:s-format-number($hour, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(24, $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'K\'"> <x:v-x:s-format-number($hour mod 12, $padding)" /> </x:wh-> <x:wh- test="$char = \'m\'"> <x:v-x:s-format-number($minute, $padding)" /> </x:wh-> <x:wh- test="$char = \'s\'"> <x:v-x:s-format-number($second, $padding)" /> </x:wh-> <x:wh- test="$char = \'S\'"> <x:v-x:s-format-number(substring-after($second, \'.\'), $padding)" /> </x:wh-> <x:wh- test="$char = \'F\'"> <x:v-x:s-floor($day div 7) + 1" /> </x:wh-> <x:o-> <x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &lt; $month]/@l)" /> <x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &gt; 2)" /> <x:v-x:s-format-number($days, $padding)" /> <!--removed week in year--> <!--removed week in month--> </x:o-> </x:c-> </x:o-> </x:c-> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$minute" /> <x:w-x:n-second"x:s-$second" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-substring($mask, $mask-length + 1)" /> </x:ct-> </x:o-> </x:c-></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.dateFormatTemplatesXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_dateFormatTemplatesXslProc));

var temp_ntb_dateXslProc='<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" extension-element-prefixes="d"> <xsl:output method="text" version="4.0" omit-xml-declaration="yes" /> <!-- http://java.sun.com/j2se/1.3/docs/api/java/text/SimpleDateFormat.html --><d:ms> <d:m l="31" a="Jan">January</d:m> <d:m l="28" a="Feb">February</d:m> <d:m l="31" a="Mar">March</d:m> <d:m l="30" a="Apr">April</d:m> <d:m l="31" a="May">May</d:m> <d:m l="30" a="Jun">June</d:m> <d:m l="31" a="Jul">July</d:m> <d:m l="31" a="Aug">August</d:m> <d:m l="30" a="Sep">September</d:m> <d:m l="31" a="Oct">October</d:m> <d:m l="30" a="Nov">November</d:m> <d:m l="31" a="Dec">December</d:m></d:ms><d:ds> <d:d a="Sun">Sunday</d:d> <d:d a="Mon">Monday</d:d> <d:d a="Tue">Tuesday</d:d> <d:d a="Wed">Wednesday</d:d> <d:d a="Thu">Thursday</d:d> <d:d a="Fri">Friday</d:d> <d:d a="Sat">Saturday</d:d></d:ds><x:t-x:n-d:format-date"> <x:p-x:n-date-time" /> <x:p-x:n-mask"x:s-\'MMM d, yy\'"/> <x:va-x:n-formatted"> <x:va-x:n-date-time-length"x:s-string-length($date-time)" /> <x:va-x:n-timezone"x:s-\'\'" /> <x:va-x:n-dt"x:s-substring($date-time, 1, $date-time-length - string-length($timezone))" /> <x:va-x:n-dt-length"x:s-string-length($dt)" /> <x:c-> <x:wh- test="substring($dt, 3, 1) = \':\' and substring($dt, 6, 1) = \':\'"> <!--that means we just have a time--> <x:va-x:n-hour"x:s-substring($dt, 1, 2)" /> <x:va-x:n-min"x:s-substring($dt, 4, 2)" /> <x:va-x:n-sec"x:s-substring($dt, 7)" /> <xsl:if test="$hour &lt;= 23 and $min &lt;= 59 and $sec &lt;= 60"> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-\'NaN\'" /> <x:w-x:n-month"x:s-\'NaN\'" /> <x:w-x:n-day"x:s-\'NaN\'" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$min" /> <x:w-x:n-second"x:s-$sec" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </xsl:if> </x:wh-> <x:o-> <!--($neg * -2)--> <x:va-x:n-year"x:s-substring($dt, 1, 4) * (0 + 1)" /> <x:va-x:n-month"x:s-substring($dt, 6, 2)" /> <x:va-x:n-day"x:s-substring($dt, 9, 2)" /> <x:c-> <x:wh- test="$dt-length = 10"> <!--that means we just have a date--> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </x:wh-> <x:wh- test="substring($dt, 14, 1) = \':\' and substring($dt, 17, 1) = \':\'"> <!--that means we have a date + time--> <x:va-x:n-hour"x:s-substring($dt, 12, 2)" /> <x:va-x:n-min"x:s-substring($dt, 15, 2)" /> <x:va-x:n-sec"x:s-substring($dt, 18)" /> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$min" /> <x:w-x:n-second"x:s-$sec" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-$mask" /> </x:ct-> </x:wh-> </x:c-> </x:o-> </x:c-> </x:va-> <x:v-x:s-$formatted" /> </x:t-><x:t-x:n-d:_format-date"> <x:p-x:n-year" /> <x:p-x:n-month"x:s-1" /> <x:p-x:n-day"x:s-1" /> <x:p-x:n-hour"x:s-0" /> <x:p-x:n-minute"x:s-0" /> <x:p-x:n-second"x:s-0" /> <x:p-x:n-timezone"x:s-\'Z\'" /> <x:p-x:n-mask"x:s-\'\'" /> <x:va-x:n-char"x:s-substring($mask, 1, 1)" /> <x:c-> <x:wh- test="not($mask)" /> <!--replaced escaping with \' here/--> <x:wh- test="not(contains(\'GyMdhHmsSEDFwWakKz\', $char))"> <x:v-x:s-$char" /> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$minute" /> <x:w-x:n-second"x:s-$second" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-substring($mask, 2)" /> </x:ct-> </x:wh-> <x:o-> <x:va-x:n-next-different-char"x:s-substring(translate($mask, $char, \'\'), 1, 1)" /> <x:va-x:n-mask-length"> <x:c-> <x:wh- test="$next-different-char"> <x:v-x:s-string-length(substring-before($mask, $next-different-char))" /> </x:wh-> <x:o-> <x:v-x:s-string-length($mask)" /> </x:o-> </x:c-> </x:va-> <x:c-> <!--took our the era designator--> <x:wh- test="$char = \'M\'"> <x:c-> <x:wh- test="$mask-length >= 3"> <x:va-x:n-month-node"x:s-document(\'\')/*/d:ms/d:m[number($month)]" /> <x:c-> <x:wh- test="$mask-length >= 4"> <x:v-x:s-$month-node" /> </x:wh-> <x:o-> <x:v-x:s-$month-node/@a" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$mask-length = 2"> <x:v-x:s-format-number($month, \'00\')" /> </x:wh-> <x:o-> <x:v-x:s-$month" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'E\'"> <x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &lt; $month]/@l)" /> <x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &gt; 2)" /> <x:va-x:n-y-1"x:s-$year - 1" /> <x:va-x:n-dow"x:s-(($y-1 + floor($y-1 div 4) - floor($y-1 div 100) + floor($y-1 div 400) + $days) mod 7) + 1" /> <x:va-x:n-day-node"x:s-document(\'\')/*/d:ds/d:d[number($dow)]" /> <x:c-> <x:wh- test="$mask-length >= 4"> <x:v-x:s-$day-node" /> </x:wh-> <x:o-> <x:v-x:s-$day-node/@a" /> </x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'a\'"> <x:c-> <x:wh- test="$hour >= 12">PM</x:wh-> <x:o->AM</x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'z\'"> <x:c-> <x:wh- test="$timezone = \'Z\'">UTC</x:wh-> <x:o->UTC<x:v-x:s-$timezone" /></x:o-> </x:c-> </x:wh-> <x:o-> <x:va-x:n-padding"x:s-\'00\'" /> <!--removed padding--> <x:c-> <x:wh- test="$char = \'y\'"> <x:c-> <x:wh- test="$mask-length &gt; 2"><x:v-x:s-format-number($year, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(substring($year, string-length($year) - 1), $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'d\'"> <x:v-x:s-format-number($day, $padding)" /> </x:wh-> <x:wh- test="$char = \'h\'"> <x:va-x:n-h"x:s-$hour mod 12" /> <x:c-> <x:wh- test="$h"><x:v-x:s-format-number($h, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(12, $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'H\'"> <x:v-x:s-format-number($hour, $padding)" /> </x:wh-> <x:wh- test="$char = \'k\'"> <x:c-> <x:wh- test="$hour"><x:v-x:s-format-number($hour, $padding)" /></x:wh-> <x:o-><x:v-x:s-format-number(24, $padding)" /></x:o-> </x:c-> </x:wh-> <x:wh- test="$char = \'K\'"> <x:v-x:s-format-number($hour mod 12, $padding)" /> </x:wh-> <x:wh- test="$char = \'m\'"> <x:v-x:s-format-number($minute, $padding)" /> </x:wh-> <x:wh- test="$char = \'s\'"> <x:v-x:s-format-number($second, $padding)" /> </x:wh-> <x:wh- test="$char = \'S\'"> <x:v-x:s-format-number(substring-after($second, \'.\'), $padding)" /> </x:wh-> <x:wh- test="$char = \'F\'"> <x:v-x:s-floor($day div 7) + 1" /> </x:wh-> <x:o-> <x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &lt; $month]/@l)" /> <x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &gt; 2)" /> <x:v-x:s-format-number($days, $padding)" /> <!--removed week in year--> <!--removed week in month--> </x:o-> </x:c-> </x:o-> </x:c-> <x:ct-x:n-d:_format-date"> <x:w-x:n-year"x:s-$year" /> <x:w-x:n-month"x:s-$month" /> <x:w-x:n-day"x:s-$day" /> <x:w-x:n-hour"x:s-$hour" /> <x:w-x:n-minute"x:s-$minute" /> <x:w-x:n-second"x:s-$second" /> <x:w-x:n-timezone"x:s-$timezone" /> <x:w-x:n-mask"x:s-substring($mask, $mask-length + 1)" /> </x:ct-> </x:o-> </x:c-></x:t-> <x:t- match="/"> <x:ct-x:n-d:format-date"> <x:w-x:n-date-time"x:s-//date" /> <x:w-x:n-mask"x:s-//mask" /> </x:ct-></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.form");
nitobi.form.dateXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_dateXslProc));

var temp_ntb_declarationConverterXslProc='<?xml version="1.0" encoding="utf-8" ?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes" /> <x:t- match="/"> <ntb:grid xmlns:ntb="http://www.nitobi.com"> <ntb:columns> <x:at-x:s-//ntb:columndefinition" mode="columndef" /> </ntb:columns> <ntb:datasources> <x:at-x:s-//ntb:columndefinition" mode="datasources" /> </ntb:datasources> </ntb:grid> </x:t-> <x:t- match="ntb:columndefinition" mode="columndef"> <x:c-> <x:wh- test="@type=\'TEXT\' or @type=\'TEXTAREA\' or @type=\'LISTBOX\' or @type=\'LOOKUP\' or @type=\'CHECKBOX\' or @type=\'LINK\' or @type=\'IMAGE\' or @type=\'\' or not(@type)"> <ntb:textcolumn> <xsl:copy-ofx:s-@*" /> <x:c-> <x:wh- test="@type=\'TEXT\'"> <ntb:texteditor><xsl:copy-ofx:s-@*" /></ntb:texteditor> </x:wh-> <x:wh- test="@type=\'TEXTAREA\'"> <ntb:textareaeditor><xsl:copy-ofx:s-@*" /></ntb:textareaeditor> </x:wh-> <x:wh- test="@type=\'LISTBOX\'"> <ntb:listboxeditor> <xsl:copy-ofx:s-@*" /> <x:a-x:n-DatasourceId">id_<x:v-x:s-position()"/></x:a-> <x:a-x:n-DisplayFields"> <x:c-> <x:wh- test="@show=\'value\'">b</x:wh-> <x:wh- test="@show=\'key\'">a</x:wh-> <x:o-></x:o-> </x:c-> </x:a-> <x:a-x:n-ValueField"> <x:c-> <x:wh- test="@show">a</x:wh-> <x:o-></x:o-> </x:c-> </x:a-> </ntb:listboxeditor> </x:wh-> <x:wh- test="@type=\'CHECKBOX\'"> <ntb:checkboxeditor> <xsl:copy-ofx:s-@*" /> <x:a-x:n-DatasourceId">id_<x:v-x:s-position()"/></x:a-> <x:a-x:n-DisplayFields"> <x:c-> <x:wh- test="@show=\'value\'">b</x:wh-> <x:wh- test="@show=\'key\'">a</x:wh-> <x:o-></x:o-> </x:c-></x:a-> <x:a-x:n-ValueField">a</x:a-> </ntb:checkboxeditor> </x:wh-> <x:wh- test="@type=\'LOOKUP\'"> <ntb:lookupeditor> <xsl:copy-ofx:s-@*" /> <x:a-x:n-DatasourceId">id_<x:v-x:s-position()"/></x:a-> <x:a-x:n-DisplayFields"> <x:c-> <x:wh- test="@show=\'key\'">a</x:wh-> <x:wh- test="@show=\'value\'">b</x:wh-> <x:o-></x:o-> </x:c-></x:a-> <x:a-x:n-ValueField"> <x:c-> <x:wh- test="@show">a</x:wh-> <x:o-></x:o-> </x:c-> </x:a-> </ntb:lookupeditor> </x:wh-> <x:wh- test="@type=\'LINK\'"> <ntb:linkeditor><xsl:copy-ofx:s-@*" /></ntb:linkeditor> </x:wh-> <x:wh- test="@type=\'IMAGE\'"> <ntb:imageeditor><xsl:copy-ofx:s-@*" /></ntb:imageeditor> </x:wh-> </x:c-> </ntb:textcolumn> </x:wh-> <x:wh- test="@type=\'NUMBER\'"> <ntb:numbercolumn><xsl:copy-ofx:s-@*" /></ntb:numbercolumn> </x:wh-> <x:wh- test="@type=\'DATE\' or @type=\'CALENDAR\'"> <ntb:datecolumn> <xsl:copy-ofx:s-@*" /> <x:c-> <x:wh- test="@type=\'DATE\'"> <ntb:dateeditor><xsl:copy-ofx:s-@*" /></ntb:dateeditor> </x:wh-> <x:wh- test="@type=\'CALENDAR\'"> <ntb:calendareditor><xsl:copy-ofx:s-@*" /></ntb:calendareditor> </x:wh-> </x:c-> </ntb:datecolumn> </x:wh-> </x:c-> </x:t-> <x:t- match="ntb:columndefinition" mode="datasources"> <xsl:if test="@values and @values!=\'\'"> <ntb:datasource> <x:a-x:n-id">id_<x:v-x:s-position()" /></x:a-> <ntb:datasourcestructure> <x:a-x:n-id">id_<x:v-x:s-position()" /></x:a-> <x:a-x:n-FieldNames">a|b</x:a-> <x:a-x:n-Keys">a</x:a-> </ntb:datasourcestructure> <ntb:data> <x:a-x:n-id">id_<x:v-x:s-position()" /></x:a-> <x:ct-x:n-values"> <x:w-x:n-valuestring"x:s-@values" /> </x:ct-> </ntb:data> </ntb:datasource> </xsl:if> </x:t-> <x:t-x:n-values"> <x:p-x:n-valuestring" /> <x:va-x:n-bstring"> <x:c-> <x:wh- test="contains($valuestring,\',\')"><x:v-x:s-substring-after(substring-before($valuestring,\',\'),\':\')" /></x:wh-> <x:o-><x:v-x:s-substring-after($valuestring,\':\')" /></x:o-> </x:c-> </x:va-> <ntb:e> <x:a-x:n-a"><x:v-x:s-substring-before($valuestring,\':\')" /></x:a-> <x:a-x:n-b"><x:v-x:s-$bstring" /></x:a-> </ntb:e> <xsl:if test="contains($valuestring,\',\')"> <x:ct-x:n-values"> <x:w-x:n-valuestring"x:s-substring-after($valuestring,\',\')" /> </x:ct-> </xsl:if> </x:t-> </xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.declarationConverterXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_declarationConverterXslProc));

var temp_ntb_frameCssXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:user="http://mycompany.com/mynamespace" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"><xsl:output method="text" omit-xml-declaration="yes"/><x:p-x:n-IE"x:s-\'false\'"/><xsl:keyx:n-style" match="//s" use="@k" /><x:t- match = "/"> <x:va-x:n-g"x:s-//state/nitobi.grid.Grid"></x:va-> <x:va-x:n-u"x:s-//state/@uniqueID"></x:va-> <x:va-x:n-showvscroll"><x:c-><x:wh- test="($g/@VScrollbarEnabled=\'true\' or $g/@VScrollbarEnabled=1)">1</x:wh-><x:o->0</x:o-></x:c-></x:va-> <x:va-x:n-showhscroll"><x:c-><x:wh- test="($g/@HScrollbarEnabled=\'true\' or $g/@HScrollbarEnabled=1)">1</x:wh-><x:o->0</x:o-></x:c-></x:va-> <x:va-x:n-showtoolbar"><x:c-><x:wh- test="($g/@ToolbarEnabled=\'true\' or $g/@ToolbarEnabled=1)">1</x:wh-><x:o->0</x:o-></x:c-></x:va-> <x:va-x:n-frozen-columns-width"> <x:ct-x:n-get-pane-width"> <x:w-x:n-start-column"x:s-number(1)"/> <x:w-x:n-end-column"x:s-number($g/@FrozenLeftColumnCount)"/> <x:w-x:n-current-width"x:s-number(0)"/> </x:ct-> </x:va-> <x:va-x:n-unfrozen-columns-width"> <x:ct-x:n-get-pane-width"> <x:w-x:n-start-column"x:s-number($g/@FrozenLeftColumnCount)+1"/> <x:w-x:n-end-column"x:s-number($g/@ColumnCount)"/> <x:w-x:n-current-width"x:s-number(0)"/> </x:ct-> </x:va-> <x:va-x:n-total-columns-width"> <x:v-x:s-number($frozen-columns-width) + number($unfrozen-columns-width)"/> </x:va-> <x:va-x:n-scrollerHeight"x:s-number($g/@Height)-(number($g/@scrollbarHeight)*$showhscroll)-(number($g/@toolbarHeight)*$showtoolbar)" /> <x:va-x:n-scrollerWidth"x:s-number($g/@Width)-(number($g/@scrollbarWidth)*number($g/@VScrollbarEnabled))" /> <x:va-x:n-midHeight"x:s-number($g/@Height)-(number($g/@scrollbarHeight)*$showhscroll)-(number($g/@toolbarHeight)*$showtoolbar)-number($g/@top)"/> #grid<x:v-x:s-$u" /> { height:<x:v-x:s-$g/@Height" />px; width:<x:v-x:s-$g/@Width" />px; overflow:hidden;text-align:left; <xsl:if test="$IE=\'true\'"> position:relative; </xsl:if> -moz-user-select: none; -khtml-user-select: none; user-select: none; } .hScrollbarRange<x:v-x:s-$u" /> { width:<x:v-x:s-$total-columns-width"/>px; } .vScrollbarRange<x:v-x:s-$u" /> {} .ntb-grid-datablock, .ntb-grid-headerblock { table-layout:fixed; <xsl:if test="$IE=\'true\'"> width:0px; </xsl:if> } .ntbrowindicator {overflow:hidden;height:<x:v-x:s-$g/@RowHeight" />px;width:<x:v-x:s-$g/@indicatorWidth" />px;float:left;} .ntbcellborder<x:v-x:s-$u" /> {overflow:hidden;text-decoration:none;margin:0px;border-right:1px solid #c0c0c0;border-bottom:1px solid #c0c0c0;white-space:nowrap;<xsl:if test="$IE=\'true\'">height:auto;</xsl:if>} .ntb-grid-headershow<x:v-x:s-$u" /> {padding:0px;spacing:0px;<xsl:if test="not($g/@ColumnIndicatorsEnabled=1)">display:none;</xsl:if>} .ntb-grid-vscrollshow<x:v-x:s-$u" /> {padding:0px;spacing:0px;<xsl:if test="not($g/@VScrollbarEnabled=1)">display:none;</xsl:if>} .ntb-grid-hscrollshow<x:v-x:s-$u" /> {padding:0px;spacing:0px;<xsl:if test="not($g/@HScrollbarEnabled=1)">display:none;</xsl:if>} .ntb-grid-toolbarshow<x:v-x:s-$u" /> {<xsl:if test="not($g/@ToolbarEnabled=1) and not($g/@ToolbarEnabled=\'true\')">display:none;</xsl:if>} .ntb-grid-height<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@Height" />px;overflow:hidden;} .ntb-grid-width<x:v-x:s-$u" /> {width:<x:v-x:s-$g/@Width" />px;overflow:hidden;} .ntb-grid-overlay<x:v-x:s-$u" /> {position:relative;z-index:1000;top:0px;left:0px;} .ntb-grid-scroller<x:v-x:s-$u" /> {overflow:hidden;text-align:left;} .ntb-grid-scrollerheight<x:v-x:s-$u" /> {height: <x:c-><x:wh- test="($total-columns-width &gt; $g/@Width)"><x:v-x:s-$scrollerHeight"/></x:wh-><x:o-><x:v-x:s-number($scrollerHeight) + number($g/@scrollbarHeight)"/></x:o-></x:c->px;} .ntb-grid-scrollerwidth<x:v-x:s-$u" /> {width:<x:v-x:s-$scrollerWidth"/>px;} .ntb-grid-topheight<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@top" />px;overflow:hidden;<xsl:if test="$g/@top=0">display:none;</xsl:if>} .ntb-grid-midheight<x:v-x:s-$u" /> {overflow:hidden;height:<x:c-><x:wh- test="($total-columns-width &gt; $g/@Width)"><x:v-x:s-$midHeight"/></x:wh-><x:o-><x:v-x:s-number($midHeight) + number($g/@scrollbarHeight)"/></x:o-></x:c->px;} .ntb-grid-bottomheight<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@bottom" />px;overflow:hidden;} .ntb-grid-leftwidth<x:v-x:s-$u" /> { width:<x:v-x:s-$g/@left" />px;overflow:hidden;text-align:left; } .ntb-grid-centerwidth<x:v-x:s-$u" /> {width:<x:v-x:s-number($g/@Width)-number($g/@left)-(number($g/@scrollbarWidth)*$showvscroll)" />px;} .ntb-grid-rightwidth<x:v-x:s-$u" /> {width:<x:v-x:s-$g/@right" />px;} .ntb-grid-scrollbarheight<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@scrollbarHeight" />px;} .ntb-grid-scrollbarwidth<x:v-x:s-$u" /> {width:<x:v-x:s-$g/@scrollbarWidth" />px;} .ntb-grid-toolbarheight<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@toolbarHeight" />px;} .ntb-grid-surfacewidth<x:v-x:s-$u" /> {width:<x:v-x:s-number($unfrozen-columns-width)"/>px;} .ntb-grid-surfaceheight<x:v-x:s-$u" /> {height:100px;} .ntb-grid {padding:0px;margin:0px;border:1px solid #cccccc} .ntb-scroller {padding:0px;spacing:0px;} .ntb-scrollcorner {padding:0px;spacing:0px;} .ntb-hscrollbar {<x:c-><x:wh- test="($total-columns-width &gt; $g/@Width)">display:block;</x:wh-><x:o->display:none;</x:o-></x:c->} .ntbinputborder { table-layout:fixed; overflow:hidden; } .ntbcolumnresizesurface { filter:alpha(opacity=1); background-color:white; position:absolute; visibility:hidden; top:0; left:0; width:100; height:100; z-index:800; } .ntbcolumnindicator { overflow:hidden; white-space: nowrap; } .ntbrow<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@RowHeight" />px;margin:0px;} .ntbheaderrow<x:v-x:s-$u" /> {height:<x:v-x:s-$g/@HeaderHeight" />px;} <x:at-x:s-state/nitobi.grid.Columns" /></x:t-><x:t-x:n-get-pane-width"> <x:p-x:n-start-column"/> <x:p-x:n-end-column"/> <x:p-x:n-current-width"/> <x:c-> <x:wh- test="$start-column &lt;= $end-column"> <x:ct-x:n-get-pane-width"> <x:w-x:n-start-column"x:s-$start-column+1"/> <x:w-x:n-end-column"x:s-$end-column"/> <x:w-x:n-current-width"x:s-number($current-width) + number(//state/nitobi.grid.Columns/nitobi.grid.Column[$start-column]/@Width)"/> </x:ct-> </x:wh-> <x:o-> <x:v-x:s-$current-width"/> </x:o-> </x:c-> </x:t-><x:t- match="nitobi.grid.Columns"> <xsl:for-eachx:s-*"> <x:va-x:n-p"><x:v-x:s-position()"/></x:va-> <x:va-x:n-w"><x:v-x:s-@Width"/></x:va-> .ntbcolumn<x:v-x:s-/state/@uniqueID" />_<xsl:number value="$p" /> {width:<x:v-x:s-$w" />px;} .ntbcolumndata<x:v-x:s-/state/@uniqueID" />_<xsl:number value="$p" /> {text-align:<x:v-x:s-@Align"/>;} </xsl:for-each></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.frameCssXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_frameCssXslProc));

var temp_ntb_frameXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:ntb="http://www.nitobi.com" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"><xsl:output method="text" omit-xml-declaration="yes"/><x:p-x:n-IE"x:s-\'false\'"/><x:p-x:n-scrollbarWidth"x:s-17" /><x:t- match = "/"><x:va-x:n-uniqueId"x:s-state/@uniqueID" /><x:va-x:n-Id"x:s-state/@ID" /><x:va-x:n-resizeEnabled"x:s-state/nitobi.grid.Grid/@GridResizeEnabled" /><x:va-x:n-offset"> <x:c-> <x:wh- test="$IE=\'true\'">1</x:wh-> <x:o->0</x:o-> </x:c-></x:va-> &lt;table <xsl:if test="$IE=\'true\'">tabindex="1"</xsl:if> cellpadding="0" cellspacing="0" id="grid<x:v-x:s-$uniqueId" />" class="ntb-grid <x:v-x:s-@theme" />" &gt; &lt;tr&gt; &lt;td colspan="2"&gt; &lt;div id="ntb-grid-overlay<x:v-x:s-$uniqueId" />" class="ntb-grid-overlay<x:v-x:s-$uniqueId" />"&gt;&lt;/div&gt; <xsl:if test="$IE=\'false\'">&lt;div id="ntb-grid-keynav<x:v-x:s-$uniqueId" />" tabindex="1" style="position:absolute;width:0px;height:0px;"&gt;&lt;/div&gt;</xsl:if> &lt;/td&gt; &lt;/tr&gt; &lt;tr&gt; &lt;td id="ntb-grid-scroller<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollerheight<x:v-x:s-$uniqueId" /> ntb-grid-scrollerwidth<x:v-x:s-$uniqueId" />" &gt; &lt;div id="ntb-grid-scrollerarea<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollerheight<x:v-x:s-$uniqueId" />" style="overflow:hidden;" &gt; &lt;div tabindex="2" id="ntb-grid-scroller<x:v-x:s-$uniqueId" />" class="ntb-grid-scroller<x:v-x:s-$uniqueId" /> ntb-grid-scrollerheight<x:v-x:s-$uniqueId" />" &gt; &lt;table class="ntb-grid-scroller" cellpadding="0" cellspacing="0" border="0" &gt; &lt;tr class="ntb-grid-topheight<x:v-x:s-$uniqueId" /> " &gt; &lt;td class="ntb-scroller ntb-grid-topheight<x:v-x:s-$uniqueId" />" &gt; &lt;div id="gridvp_0_<x:v-x:s-$uniqueId" />" class="ntb-grid-topheight<x:v-x:s-$uniqueId" /> ntb-grid-leftwidth<x:v-x:s-$uniqueId" />"&gt; &lt;div id="gridvpsurface_0_<x:v-x:s-$uniqueId" />" &gt; &lt;div id="gridvpcontainer_0_<x:v-x:s-$uniqueId" />" &gt;&lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/td&gt; &lt;td class="ntb-scroller" &gt; &lt;div id="gridvp_1_<x:v-x:s-$uniqueId" />" class="ntb-grid-topheight<x:v-x:s-$uniqueId" /> ntb-grid-centerwidth<x:v-x:s-$uniqueId" /> ntbgridheader"&gt; &lt;div id="gridvpsurface_1_<x:v-x:s-$uniqueId" />" class="ntb-grid-surfacewidth<x:v-x:s-$uniqueId" />" &gt; &lt;div id="gridvpcontainer_1_<x:v-x:s-$uniqueId" />" &gt;&lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/td&gt; &lt;/tr&gt; &lt;tr class="ntb-grid-scroller" &gt; &lt;td class="ntb-scroller" &gt; &lt;div style="position:relative;"&gt; <!--&lt;div id="ntb-frozenshadow<x:v-x:s-$uniqueId" />" class="ntb-frozenshadow"&gt;&lt;/div&gt;--> &lt;div id="gridvp_2_<x:v-x:s-$uniqueId" />" class="ntb-grid-midheight<x:v-x:s-$uniqueId" /> ntb-grid-leftwidth<x:v-x:s-$uniqueId" />" style="position:relative;"&gt; &lt;div id="gridvpsurface_2_<x:v-x:s-$uniqueId" />" &gt; &lt;div id="gridvpcontainer_2_<x:v-x:s-$uniqueId" />" &gt;&lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/td&gt; &lt;td class="ntb-scroller" &gt; &lt;div id="gridvp_3_<x:v-x:s-$uniqueId" />" class="ntb-grid-midheight<x:v-x:s-$uniqueId"/> ntb-grid-centerwidth<x:v-x:s-$uniqueId" />" style="position:relative;"&gt; &lt;div id="gridvpsurface_3_<x:v-x:s-$uniqueId" />" class="ntb-grid-surfacewidth<x:v-x:s-$uniqueId" />" &gt; &lt;div id="gridvpcontainer_3_<x:v-x:s-$uniqueId" />" &gt;&lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/td&gt; &lt;/tr&gt; &lt;/table&gt; &lt;/div&gt; &lt;/div&gt; &lt;/td&gt; &lt;td id="ntb-grid-vscrollshow<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollerheight<x:v-x:s-$uniqueId" />"&gt;&lt;div id="vscrollclip<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollerheight<x:v-x:s-$uniqueId" /> ntb-grid-scrollbarwidth<x:v-x:s-$uniqueId"/> ntb-scrollbar" style="overflow:hidden;" &gt;&lt;div id="vscroll<x:v-x:s-$uniqueId" />" class="ntb-scrollbar" style="height:100%;width:<x:v-x:s-number($offset)+number(state/nitobi.grid.Grid/@scrollbarWidth)"/>px;position:relative;top:0px;left:-<x:v-x:s-$offset"/>px;overflow-x:hidden;overflow-y:scroll;" &gt;&lt;div class="vScrollbarRange<x:v-x:s-$uniqueId" />" style="WIDTH:1px;overflow:hidden;"&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/td&gt; &lt;/tr&gt; &lt;tr id="ntb-grid-hscrollshow<x:v-x:s-$uniqueId" />" &gt; &lt;td &gt;&lt;div id="hscrollclip<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollbarheight<x:v-x:s-$uniqueId" /> ntb-grid-scrollerwidth<x:v-x:s-$uniqueId" /> ntb-hscrollbar" style="overflow:hidden;" &gt; &lt;div id="hscroll<x:v-x:s-$uniqueId" />" class="ntb-grid-scrollbarheight<x:v-x:s-$uniqueId" /> ntb-grid-scrollerwidth<x:v-x:s-$uniqueId" /> ntb-scrollbar" style="overflow-x:scroll;overflow-y:hidden;height:<x:v-x:s-number($offset)+number(state/nitobi.grid.Grid/@scrollbarHeight)"/>px;position:relative;top:-<x:v-x:s-$offset"/>px;left:0px;" &gt; &lt;div class="hScrollbarRange<x:v-x:s-$uniqueId" />" style="HEIGHT:1px;overflow:hidden;"&gt; &lt;/div&gt; &lt;/td&gt; &lt;td class="ntb-grid-vscrollshow<x:v-x:s-$uniqueId" /> ntb-scrollcorner" &gt;&lt;/td&gt; &lt;/tr&gt; &lt;tr id="ntb-grid-toolbarshow<x:v-x:s-$uniqueId" />" &gt;&lt;td colspan="2" class="ntbtoolbarcontainer" &gt;&lt;div id="toolbarContainer<x:v-x:s-$uniqueId" />" style="overflow:hidden;" class="ntb-grid-toolbarshow<x:v-x:s-$uniqueId" /> ntb-grid-toolbarheight<x:v-x:s-$uniqueId" /> ntb-grid-width<x:v-x:s-$uniqueId" />"&gt;&lt;/div&gt;&lt;/td&gt;&lt;/tr&gt; &lt;tr id="ntb-resize-container<x:v-x:s-$uniqueId" />" &gt; &lt;td colspan="2"&gt; <xsl:if test="$resizeEnabled = \'true\'"> &lt;div style="position:relative;"&gt; &lt;div id="resizecornercontainer<x:v-x:s-$uniqueId" />" style="visibility:visible;position:absolute;right:0px;width:20px;height:20px;border:0px;bottom:0px;" onmouseover="nitobi.html.Css.setStyle($(\'resizecorner<x:v-x:s-$uniqueId" />\'), \'visibility\', \'visible\')" onmouseout="nitobi.html.Css.setStyle($(\'resizecorner<x:v-x:s-$uniqueId" />\'), \'visibility\', \'hidden\')"&gt; &lt;div id="resizecorner<x:v-x:s-$uniqueId" />" style="visibility:hidden;"&gt; &lt;div class="ntbresizeindicatorright"&gt; &lt;/div&gt; &lt;div class="ntbresizeindicatorbottom"&gt; &lt;/div&gt; &lt;/div&gt; &lt;/div&gt; &lt;/div&gt; </xsl:if> &lt;textarea id="<x:v-x:s-$uniqueId"/>_ebaclipboard" class="ntb-grid-copybuffer"&gt;&lt;/textarea&gt; &lt;/td&gt; &lt;/tr&gt; &lt;/table&gt;</x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.frameXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_frameXslProc));

var temp_ntb_listboxXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes"/> <x:p-x:n-DisplayFields"x:s-\'\'"></x:p-> <x:p-x:n-ValueField"x:s-\'\'"></x:p-> <x:p-x:n-val"x:s-\'\'"></x:p-> <x:t- match="/"> <!--<x:va-x:n-cell"x:s-/root/metadata/r[@xi=$row]/*[@xi=$col]"></x:va->--> <select class="ntbinput ntblookupoptions"> <!--<x:c-> <x:wh- test="$DatasourceId">--> <xsl:for-eachx:s-/ntb:datasource/ntb:data/*"> <xsl:sortx:s-@*[name(.)=substring-before($DisplayFields,\'|\')]" data-type="text" order="ascending" /> <option> <x:a-x:n-value"> <x:v-x:s-@*[name(.)=$ValueField]"></x:v-> </x:a-> <x:a-x:n-rn"> <x:v-x:s-position()"></x:v-> </x:a-> <xsl:if test="@*[name(.)=$ValueField and .=$val]"> <x:a-x:n-selected">true</x:a-> </xsl:if> <x:ct-x:n-print-displayfields"> <x:w-x:n-field"x:s-$DisplayFields" /> </x:ct-> </option> </xsl:for-each> <!--</x:wh-> <x:o-> </x:o-> </x:c->--> </select> </x:t-> <x:t-x:n-print-displayfields"> <x:p-x:n-field" /> <x:c-> <x:wh- test="contains($field,\'|\')" > <!-- Here we hardcode a spacer \', \' - this should probably be moved elsewhere. --> <x:v-x:s-concat(@*[name(.)=substring-before($field,\'|\')],\', \')"></x:v-> <x:ct-x:n-print-displayfields"> <x:w-x:n-field"x:s-substring-after($field,\'|\')" /> </x:ct-> </x:wh-> <x:o-> <x:v-x:s-@*[name(.)=$field]"></x:v-> </x:o-> </x:c-> </x:t-> </xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.form");
nitobi.form.listboxXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_listboxXslProc));

var temp_ntb_mergeEbaXmlToLogXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes"/> <x:p-x:n-defaultAction"></x:p-> <x:p-x:n-startXid"x:s-100" ></x:p-> <xsl:keyx:n-newData" match="/ntb:grid/ntb:newdata/ntb:data/ntb:e" use="@xid" /> <xsl:keyx:n-oldData" match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data/ntb:e" use="@xid" /> <x:t- match="@* | node()" > <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:t-> <x:t- match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data/ntb:e"> <xsl:if test="not(key(\'newData\',@xid))"> <xsl:copy> <xsl:copy-ofx:s-@*" /> </xsl:copy> </xsl:if> </x:t-> <x:t- match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data"> <xsl:copy> <x:at-x:s-@*|node()" /> <xsl:for-eachx:s-/ntb:grid/ntb:newdata/ntb:data/ntb:e"> <xsl:copy> <xsl:copy-ofx:s-@*" /> <xsl:if test="$defaultAction"> <x:va-x:n-oldNode"x:s-key(\'oldData\',@xid)" /> <x:c-> <x:wh- test="$oldNode"> <x:va- name=\'xid\'x:s-@xid" /> <x:a-x:n-xac"><x:v-x:s-$oldNode/@xac" /></x:a-> </x:wh-> <x:o-> <x:a-x:n-xac"><x:v-x:s-$defaultAction" /></x:a-> </x:o-> </x:c-> </xsl:if> </xsl:copy> </xsl:for-each> </xsl:copy> </x:t-></xsl:stylesheet> ';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.mergeEbaXmlToLogXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_mergeEbaXmlToLogXslProc));

var temp_ntb_mergeEbaXmlXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="no" /> <x:p-x:n-startRowIndex"x:s-100" ></x:p-> <x:p-x:n-endRowIndex"x:s-200" ></x:p-> <x:p-x:n-guid"x:s-1"></x:p-> <xsl:keyx:n-newData" match="/ntb:grid/ntb:newdata/ntb:data/ntb:e" use="@xi" /> <xsl:keyx:n-oldData" match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data/ntb:e" use="@xi" /> <x:t- match="@* | node()" > <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:t-> <x:t- match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data/ntb:e"> <x:c-> <x:wh- test="(number(@xi) &gt;= $startRowIndex) and (number(@xi) &lt;= $endRowIndex)"> <xsl:copy> <xsl:copy-ofx:s-@*" /> <xsl:copy-ofx:s-key(\'newData\',@xi)/@*" /> </xsl:copy> </x:wh-> <x:o-> <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:o-> </x:c-> </x:t-> <x:t- match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data"> <xsl:copy> <x:at-x:s-@*|node()" /> <xsl:for-eachx:s-/ntb:grid/ntb:newdata/ntb:data/ntb:e"> <xsl:if test="not(key(\'oldData\',@xi))"> <xsl:elementx:n-ntb:e" namespace="http://www.nitobi.com"> <xsl:copy-ofx:s-@*" /> <x:a-x:n-xid"><x:v-x:s-generate-id(.)"/><x:v-x:s-position()"/><x:v-x:s-$guid"/></x:a-> </xsl:element> </xsl:if> </xsl:for-each> </xsl:copy> </x:t-> <x:t- match="/ntb:grid/ntb:newdata/ntb:data/ntb:e"> <xsl:copy> <xsl:copy-ofx:s-@*" /> <x:va-x:n-oldData"x:s-key(\'oldData\',@xi)"/> <x:c-> <x:wh- test="$oldData"> <xsl:copy-ofx:s-$oldData/@*" /> <xsl:copy-ofx:s-@*" /> <x:a-x:n-xac">u</x:a-> <xsl:if test="$oldData/@xac=\'i\'"> <x:a-x:n-xac">i</x:a-> </xsl:if> </x:wh-> <x:o-> <x:a-x:n-xid"><x:v-x:s-generate-id(.)"/><x:v-x:s-position()"/><x:v-x:s-$guid"/></x:a-> <x:a-x:n-xac">i</x:a-> </x:o-> </x:c-> </xsl:copy> </x:t-> </xsl:stylesheet> ';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.mergeEbaXmlXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_mergeEbaXmlXslProc));

var temp_ntb_mergeUpdateAttributesXslProc='<?xml version="1.0" encoding="UTF-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/> <x:t-x:n-xmlUpdate"> <update></update> </x:t-> <x:t- match="@*|node()"> <xsl:copy> <x:at-x:s-@*|node()"/> </xsl:copy> </x:t-> <x:t- match="//update//@*"> <xsl:copy> <x:at-x:s-node()|@*"/> </xsl:copy> </x:t-> <!-- update the number of rows does not account for inserts! --> <x:t- match="//metadata/@numrows"> <x:a-x:n-{name(.)}"><x:v-x:s-. - count((document(\'\')//data[@id=\'_default\']/e[@xac=\'d\']))" /></x:a-> </x:t-> <!-- merge the updated attributes for each row --> <x:t- match="@*"> <x:va-x:n-currentXI"x:s-../@xi"/> <x:va-x:n-parentID"x:s-../../@id"/> <x:va-x:n-parentXI"x:s-../../@xi"/> <x:va-x:n-targetNode"x:s-(document(\'\')//*[@id=$parentID or @xi=$parentXI]/*[@xi=$currentXI and @xac=\'u\'])" /> <x:c-> <x:wh- test="($targetNode) and (name($targetNode)=name(..)) and (../@xi = $targetNode/@xi) and (name(../..) = name($targetNode/..))"> <xsl:copy> <x:at-x:s-node()|@*"/> </xsl:copy> <x:at-x:s-$targetNode/@*" /> </x:wh-> <x:o-> <xsl:copy> <x:at-x:s-node()|@*"/> </xsl:copy> </x:o-> </x:c-> </x:t-> <!-- delete rows --> <x:t- match="//root/*//node()"> <x:va-x:n-currentXI"x:s-@xi"/> <x:va-x:n-parentID"x:s-../@id"/> <x:va-x:n-parentXI"x:s-../@xi"/> <x:va-x:n-targetNode"x:s-(document(\'\')//*[@id=$parentID or @xi=$parentXI]/*[@xi=$currentXI and @xac=\'d\'])" /> <x:c-> <x:wh- test="($targetNode) and (name($targetNode/..)=name(..)) and (name() = name($targetNode))"> </x:wh-> <x:o-> <xsl:copy> <x:at-x:s-node()|@*"/> </xsl:copy> </x:o-> </x:c-> </x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.mergeUpdateAttributesXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_mergeUpdateAttributesXslProc));

var temp_ntb_modelFromDeclarationInitializerXslProc='<?xml version="1.0"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> <xsl:output method="text" encoding="utf-8" omit-xml-declaration="yes"/> <x:t- match="interface"> <x:ct-x:n-initJSDefaults"/> <x:at-/> </x:t-> <x:t-x:n-initJSDefaults"> var elem = this.Declaration.grid.documentElement; var valueFromHtml; </x:t-> <x:t- match="properties | events"> <xsl:for-eachx:s-*"> valueFromHtml = <x:c-><x:wh- test="@htmltag">elem.getAttribute("<x:v-x:s-@htmltag"/>")</x:wh-><x:o->elem.getAttribute("<x:v-x:s-@name"/>")</x:o-></x:c->; if (valueFromHtml) { this.set<x:v-x:s-@name"/>(valueFromHtml); } </xsl:for-each> </x:t-> <x:t- match="text()"/></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.modelFromDeclarationInitializerXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_modelFromDeclarationInitializerXslProc));

var temp_ntb_numberFormatTemplatesXslProc='<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" xmlns:n="http://www.nitobi.com/exslt/numbers" extension-element-prefixes="d n"> <!--http://www.w3schools.com/xsl/func_formatnumber.asp--><!-- <xsl:decimal-formatx:n-name" decimal-separator="char" grouping-separator="char" infinity="string" minus-sign="char" NaN="string" percent="char" per-mille="char" zero-digit="char" digit="char" pattern-separator="char"/> --><xsl:decimal-formatx:n-NA" decimal-separator="." grouping-separator="," /><xsl:decimal-formatx:n-EU" decimal-separator="." grouping-separator="," /><x:t-x:n-n:format"> <x:p-x:n-number"x:s-0" /> <x:p-x:n-mask"x:s-\'#.00\'" /> <x:p-x:n-group"x:s-\',\'" /> <x:p-x:n-decimal"x:s-\'.\'" /> <x:va-x:n-formattedNumber"> <x:c-> <x:wh- test="$group=\'.\' and $decimal=\',\'"> <x:v-x:s-format-number($number, $mask, \'EU\')" /> </x:wh-> <x:o-> <x:v-x:s-format-number($number, $mask, \'NA\')" /> </x:o-> </x:c-> </x:va-> <xsl:if test="not(string($formattedNumber) = \'NaN\')"> <x:v-x:s-$formattedNumber" /> </xsl:if></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.numberFormatTemplatesXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_numberFormatTemplatesXslProc));

var temp_ntb_numberXslProc='<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" xmlns:n="http://www.nitobi.com/exslt/numbers" extension-element-prefixes="d n"><xsl:output method="text" version="4.0" omit-xml-declaration="yes" />  <!--http://www.w3schools.com/xsl/func_formatnumber.asp--><!-- <xsl:decimal-formatx:n-name" decimal-separator="char" grouping-separator="char" infinity="string" minus-sign="char" NaN="string" percent="char" per-mille="char" zero-digit="char" digit="char" pattern-separator="char"/> --><xsl:decimal-formatx:n-NA" decimal-separator="." grouping-separator="," /><xsl:decimal-formatx:n-EU" decimal-separator="." grouping-separator="," /><x:t-x:n-n:format"> <x:p-x:n-number"x:s-0" /> <x:p-x:n-mask"x:s-\'#.00\'" /> <x:p-x:n-group"x:s-\',\'" /> <x:p-x:n-decimal"x:s-\'.\'" /> <x:va-x:n-formattedNumber"> <x:c-> <x:wh- test="$group=\'.\' and $decimal=\',\'"> <x:v-x:s-format-number($number, $mask, \'EU\')" /> </x:wh-> <x:o-> <x:v-x:s-format-number($number, $mask, \'NA\')" /> </x:o-> </x:c-> </x:va-> <xsl:if test="not(string($formattedNumber) = \'NaN\')"> <x:v-x:s-$formattedNumber" /> </xsl:if></x:t-><x:t- match="/"> <x:ct-x:n-n:format"> <x:w-x:n-number"x:s-//number" /> <x:w-x:n-mask"x:s-//mask" /> <x:w-x:n-group"x:s-//group" /> <x:w-x:n-decimal"x:s-//decimal" /> </x:ct-></x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.form");
nitobi.form.numberXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_numberXslProc));

var temp_ntb_rowGeneratorXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" xmlns:n="http://www.nitobi.com/exslt/numbers" extension-element-prefixes="d n"><xsl:output method="text" omit-xml-declaration="yes"/><x:p-x:n-showIndicators"x:s-\'0\'" /><x:p-x:n-showHeaders"x:s-\'0\'" /><x:p-x:n-firstColumn"x:s-\'0\'" /><x:p-x:n-lastColumn"x:s-\'0\'" /><x:p-x:n-uniqueId"x:s-\'0\'" /><x:p-x:n-rowHover"x:s-\'0\'" /><x:p-x:n-frozenColumnId"x:s-\'\'" /><x:t- match = "/">&lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com" xmlns:d="http://exslt.org/dates-and-times" xmlns:n="http://www.nitobi.com/exslt/numbers" extension-element-prefixes="d n"&gt; &lt;!-- http://java.sun.com/j2se/1.3/docs/api/java/text/SimpleDateFormat.html --&gt;&lt;d:ms&gt; &lt;d:m l="31" a="Jan"&gt;January&lt;/d:m&gt; &lt;d:m l="28" a="Feb"&gt;February&lt;/d:m&gt; &lt;d:m l="31" a="Mar"&gt;March&lt;/d:m&gt; &lt;d:m l="30" a="Apr"&gt;April&lt;/d:m&gt; &lt;d:m l="31" a="May"&gt;May&lt;/d:m&gt; &lt;d:m l="30" a="Jun"&gt;June&lt;/d:m&gt; &lt;d:m l="31" a="Jul"&gt;July&lt;/d:m&gt; &lt;d:m l="31" a="Aug"&gt;August&lt;/d:m&gt; &lt;d:m l="30" a="Sep"&gt;September&lt;/d:m&gt; &lt;d:m l="31" a="Oct"&gt;October&lt;/d:m&gt; &lt;d:m l="30" a="Nov"&gt;November&lt;/d:m&gt; &lt;d:m l="31" a="Dec"&gt;December&lt;/d:m&gt;&lt;/d:ms&gt;&lt;d:ds&gt; &lt;d:d a="Sun"&gt;Sunday&lt;/d:d&gt; &lt;d:d a="Mon"&gt;Monday&lt;/d:d&gt; &lt;d:d a="Tue"&gt;Tuesday&lt;/d:d&gt; &lt;d:d a="Wed"&gt;Wednesday&lt;/d:d&gt; &lt;d:d a="Thu"&gt;Thursday&lt;/d:d&gt; &lt;d:d a="Fri"&gt;Friday&lt;/d:d&gt; &lt;d:d a="Sat"&gt;Saturday&lt;/d:d&gt;&lt;/d:ds&gt;&lt;x:t-x:n-d:format-date"&gt; &lt;x:p-x:n-date-time" /&gt; &lt;x:p-x:n-mask"x:s-\'MMM d, yy\'"/&gt; &lt;x:va-x:n-formatted"&gt; &lt;x:va-x:n-date-time-length"x:s-string-length($date-time)" /&gt; &lt;x:va-x:n-timezone"x:s-\'\'" /&gt; &lt;x:va-x:n-dt"x:s-substring($date-time, 1, $date-time-length - string-length($timezone))" /&gt; &lt;x:va-x:n-dt-length"x:s-string-length($dt)" /&gt; &lt;x:c-&gt; &lt;x:wh- test="substring($dt, 3, 1) = \':\' and substring($dt, 6, 1) = \':\'"&gt; &lt;!--that means we just have a time--&gt; &lt;x:va-x:n-hour"x:s-substring($dt, 1, 2)" /&gt; &lt;x:va-x:n-min"x:s-substring($dt, 4, 2)" /&gt; &lt;x:va-x:n-sec"x:s-substring($dt, 7)" /&gt; &lt;xsl:if test="$hour &amp;lt;= 23 and $min &amp;lt;= 59 and $sec &amp;lt;= 60"&gt; &lt;x:ct-x:n-d:_format-date"&gt; &lt;x:w-x:n-year"x:s-\'NaN\'" /&gt; &lt;x:w-x:n-month"x:s-\'NaN\'" /&gt; &lt;x:w-x:n-day"x:s-\'NaN\'" /&gt; &lt;x:w-x:n-hour"x:s-$hour" /&gt; &lt;x:w-x:n-minute"x:s-$min" /&gt; &lt;x:w-x:n-second"x:s-$sec" /&gt; &lt;x:w-x:n-timezone"x:s-$timezone" /&gt; &lt;x:w-x:n-mask"x:s-$mask" /&gt; &lt;/x:ct-&gt; &lt;/xsl:if&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;!--($neg * -2)--&gt; &lt;x:va-x:n-year"x:s-substring($dt, 1, 4) * (0 + 1)" /&gt; &lt;x:va-x:n-month"x:s-substring($dt, 6, 2)" /&gt; &lt;x:va-x:n-day"x:s-substring($dt, 9, 2)" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$dt-length = 10"&gt; &lt;!--that means we just have a date--&gt; &lt;x:ct-x:n-d:_format-date"&gt; &lt;x:w-x:n-year"x:s-$year" /&gt; &lt;x:w-x:n-month"x:s-$month" /&gt; &lt;x:w-x:n-day"x:s-$day" /&gt; &lt;x:w-x:n-timezone"x:s-$timezone" /&gt; &lt;x:w-x:n-mask"x:s-$mask" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="substring($dt, 14, 1) = \':\' and substring($dt, 17, 1) = \':\'"&gt; &lt;!--that means we have a date + time--&gt; &lt;x:va-x:n-hour"x:s-substring($dt, 12, 2)" /&gt; &lt;x:va-x:n-min"x:s-substring($dt, 15, 2)" /&gt; &lt;x:va-x:n-sec"x:s-substring($dt, 18)" /&gt; &lt;x:ct-x:n-d:_format-date"&gt; &lt;x:w-x:n-year"x:s-$year" /&gt; &lt;x:w-x:n-month"x:s-$month" /&gt; &lt;x:w-x:n-day"x:s-$day" /&gt; &lt;x:w-x:n-hour"x:s-$hour" /&gt; &lt;x:w-x:n-minute"x:s-$min" /&gt; &lt;x:w-x:n-second"x:s-$sec" /&gt; &lt;x:w-x:n-timezone"x:s-$timezone" /&gt; &lt;x:w-x:n-mask"x:s-$mask" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;/x:c-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:v-x:s-$formatted" /&gt; &lt;/x:t-&gt;&lt;x:t-x:n-d:_format-date"&gt; &lt;x:p-x:n-year" /&gt; &lt;x:p-x:n-month"x:s-1" /&gt; &lt;x:p-x:n-day"x:s-1" /&gt; &lt;x:p-x:n-hour"x:s-0" /&gt; &lt;x:p-x:n-minute"x:s-0" /&gt; &lt;x:p-x:n-second"x:s-0" /&gt; &lt;x:p-x:n-timezone"x:s-\'Z\'" /&gt; &lt;x:p-x:n-mask"x:s-\'\'" /&gt; &lt;x:va-x:n-char"x:s-substring($mask, 1, 1)" /&gt; &lt;x:c-&gt; &lt;x:wh- test="not($mask)" /&gt; &lt;!--replaced escaping with \' here/--&gt; &lt;x:wh- test="not(contains(\'GyMdhHmsSEDFwWakKz\', $char))"&gt; &lt;x:v-x:s-$char" /&gt; &lt;x:ct-x:n-d:_format-date"&gt; &lt;x:w-x:n-year"x:s-$year" /&gt; &lt;x:w-x:n-month"x:s-$month" /&gt; &lt;x:w-x:n-day"x:s-$day" /&gt; &lt;x:w-x:n-hour"x:s-$hour" /&gt; &lt;x:w-x:n-minute"x:s-$minute" /&gt; &lt;x:w-x:n-second"x:s-$second" /&gt; &lt;x:w-x:n-timezone"x:s-$timezone" /&gt; &lt;x:w-x:n-mask"x:s-substring($mask, 2)" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:va-x:n-next-different-char"x:s-substring(translate($mask, $char, \'\'), 1, 1)" /&gt; &lt;x:va-x:n-mask-length"&gt; &lt;x:c-&gt; &lt;x:wh- test="$next-different-char"&gt; &lt;x:v-x:s-string-length(substring-before($mask, $next-different-char))" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-string-length($mask)" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:c-&gt; &lt;!--took our the era designator--&gt; &lt;x:wh- test="$char = \'M\'"&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask-length &gt;= 3"&gt; &lt;x:va-x:n-month-node"x:s-document(\'\')/*/d:ms/d:m[number($month)]" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask-length &gt;= 4"&gt; &lt;x:v-x:s-$month-node" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$month-node/@a" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$mask-length = 2"&gt; &lt;x:v-x:s-format-number($month, \'00\')" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$month" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'E\'"&gt; &lt;x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &amp;lt; $month]/@l)" /&gt; &lt;x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &amp;gt; 2)" /&gt; &lt;x:va-x:n-y-1"x:s-$year - 1" /&gt; &lt;x:va-x:n-dow"x:s-(($y-1 + floor($y-1 div 4) - floor($y-1 div 100) + floor($y-1 div 400) + $days) mod 7) + 1" /&gt; &lt;x:va-x:n-day-node"x:s-document(\'\')/*/d:ds/d:d[number($dow)]" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask-length &gt;= 4"&gt; &lt;x:v-x:s-$day-node" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$day-node/@a" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'a\'"&gt; &lt;x:c-&gt; &lt;x:wh- test="$hour &gt;= 12"&gt;PM&lt;/x:wh-&gt; &lt;x:o-&gt;AM&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'z\'"&gt; &lt;x:c-&gt; &lt;x:wh- test="$timezone = \'Z\'"&gt;UTC&lt;/x:wh-&gt; &lt;x:o-&gt;UTC&lt;x:v-x:s-$timezone" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:va-x:n-padding"x:s-\'00\'" /&gt; &lt;!--removed padding--&gt; &lt;x:c-&gt; &lt;x:wh- test="$char = \'y\'"&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask-length &amp;gt; 2"&gt;&lt;x:v-x:s-format-number($year, $padding)" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-format-number(substring($year, string-length($year) - 1), $padding)" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'d\'"&gt; &lt;x:v-x:s-format-number($day, $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'h\'"&gt; &lt;x:va-x:n-h"x:s-$hour mod 12" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$h"&gt;&lt;x:v-x:s-format-number($h, $padding)" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-format-number(12, $padding)" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'H\'"&gt; &lt;x:v-x:s-format-number($hour, $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'k\'"&gt; &lt;x:c-&gt; &lt;x:wh- test="$hour"&gt;&lt;x:v-x:s-format-number($hour, $padding)" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-format-number(24, $padding)" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'K\'"&gt; &lt;x:v-x:s-format-number($hour mod 12, $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'m\'"&gt; &lt;x:v-x:s-format-number($minute, $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'s\'"&gt; &lt;x:v-x:s-format-number($second, $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'S\'"&gt; &lt;x:v-x:s-format-number(substring-after($second, \'.\'), $padding)" /&gt; &lt;/x:wh-&gt; &lt;x:wh- test="$char = \'F\'"&gt; &lt;x:v-x:s-floor($day div 7) + 1" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:va-x:n-month-days"x:s-sum(document(\'\')/*/d:ms/d:m[position() &amp;lt; $month]/@l)" /&gt; &lt;x:va-x:n-days"x:s-$month-days + $day + boolean(((not($year mod 4) and $year mod 100) or not($year mod 400)) and $month &amp;gt; 2)" /&gt; &lt;x:v-x:s-format-number($days, $padding)" /&gt; &lt;!--removed week in year--&gt; &lt;!--removed week in month--&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;x:ct-x:n-d:_format-date"&gt; &lt;x:w-x:n-year"x:s-$year" /&gt; &lt;x:w-x:n-month"x:s-$month" /&gt; &lt;x:w-x:n-day"x:s-$day" /&gt; &lt;x:w-x:n-hour"x:s-$hour" /&gt; &lt;x:w-x:n-minute"x:s-$minute" /&gt; &lt;x:w-x:n-second"x:s-$second" /&gt; &lt;x:w-x:n-timezone"x:s-$timezone" /&gt; &lt;x:w-x:n-mask"x:s-substring($mask, $mask-length + 1)" /&gt; &lt;/x:ct-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt; &lt;!--http://www.w3schools.com/xsl/func_formatnumber.asp--&gt;&lt;!-- &lt;xsl:decimal-formatx:n-name" decimal-separator="char" grouping-separator="char" infinity="string" minus-sign="char" NaN="string" percent="char" per-mille="char" zero-digit="char" digit="char" pattern-separator="char"/&gt; --&gt;&lt;xsl:decimal-formatx:n-NA" decimal-separator="." grouping-separator="," /&gt;&lt;xsl:decimal-formatx:n-EU" decimal-separator="." grouping-separator="," /&gt;&lt;x:t-x:n-n:format"&gt; &lt;x:p-x:n-number"x:s-0" /&gt; &lt;x:p-x:n-mask"x:s-\'#.00\'" /&gt; &lt;x:p-x:n-group"x:s-\',\'" /&gt; &lt;x:p-x:n-decimal"x:s-\'.\'" /&gt; &lt;x:va-x:n-formattedNumber"&gt; &lt;x:c-&gt; &lt;x:wh- test="$group=\'.\' and $decimal=\',\'"&gt; &lt;x:v-x:s-format-number($number, $mask, \'EU\')" /&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-format-number($number, $mask, \'NA\')" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;xsl:if test="not(string($formattedNumber) = \'NaN\')"&gt; &lt;x:v-x:s-$formattedNumber" /&gt; &lt;/xsl:if&gt;&lt;/x:t-&gt;&lt;xsl:output method="xml" omit-xml-declaration="yes"/&gt;&lt;x:p-x:n-start" /&gt;&lt;x:p-x:n-end" /&gt;&lt;x:p-x:n-activeColumn"x:s-\'0\'" /&gt;&lt;x:p-x:n-activeRow"x:s-\'0\'" /&gt;&lt;x:p-x:n-sortColumn"x:s-\'0\'" /&gt;&lt;x:p-x:n-sortDirection"x:s-\'Asc\'" /&gt;&lt;x:p-x:n-dataTableId"x:s-\'_default\'" /&gt;&lt;xsl:keyx:n-data-source" match="//ntb:datasources/ntb:datasource" use="@id" /&gt;&lt;x:t- match = "/"&gt; &lt;div&gt; <xsl:if test="$showHeaders"> &lt;table cellpadding="0" cellspacing="0" border="0" class="ntb-grid-headerblock" &gt; &lt;tr class="ntbheaderrow<x:v-x:s-$uniqueId" />"&gt; <xsl:if test="$showIndicators"> &lt;td ebatype="columnheader" xi="<x:v-x:s-position()-1"/>" class="ntbcolumn"&gt; &lt;a href="#" class="ntbrowindicator" onclick="return false;" style=";float:left;"&gt; &lt;x:v-x:s-@xi"/&gt; &lt;/a&gt; &lt;/td&gt; </xsl:if> <xsl:for-eachx:s-*/*"> <xsl:if test="@Visible = \'1\' and (position() &gt; $firstColumn and position() &lt;= $lastColumn)"> &lt;td id="columnheader_<x:v-x:s-position()-1"/>_<x:v-x:s-$uniqueId" />" ebatype="columnheader" xi="<x:v-x:s-position()-1"/>" col="<x:v-x:s-position()-1"/>" onmouseover="$(\'grid<x:v-x:s-$uniqueId" />\').jsObject.handleHeaderMouseOver(this);" onmouseout="$(\'grid<x:v-x:s-$uniqueId" />\').jsObject.handleHeaderMouseOut(this);"&gt; &lt;x:va-x:n-sortString<x:v-x:s-position()-1"/>"&gt; &lt;x:c-&gt; &lt;x:wh- test="$sortColumn=<x:v-x:s-position()-1"/> and $sortDirection=\'Asc\'"&gt;ascending&lt;/x:wh-&gt; &lt;x:wh- test="$sortColumn=<x:v-x:s-position()-1"/> and $sortDirection=\'Desc\'"&gt;descending&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:a-x:n-class"&gt;ntbcolumnindicatorborder&lt;x:v-x:s-$sortString<x:v-x:s-position()-1"/>" /&gt;&lt;/x:a-&gt; &lt;div class="ntbcolumnindicator"&gt; <x:c-> <x:wh- test="@Label and not(@Label = \'\') and not(@Label = \' \')"><x:v-x:s-@Label" /></x:wh-> <x:wh- test="ntb:label and not(ntb:label = \'\') and not(ntb:label = \' \')"><x:v-x:s-ntb:label" /></x:wh-> <x:o->ATOKENTOREPLACE</x:o-> </x:c-> &lt;/div&gt; &lt;/td&gt; </xsl:if> </xsl:for-each> &lt;/tr&gt; &lt;colgroup&gt; <xsl:for-eachx:s-*/*"> <xsl:if test="@Visible = \'1\' and (position() &gt; $firstColumn and position() &lt;= $lastColumn)"> &lt;col class="ntbcolumn<x:v-x:s-$uniqueId" />_<x:v-x:s-position()" />"&gt;&lt;/col&gt; </xsl:if> </xsl:for-each> &lt;/colgroup&gt; &lt;/table&gt; </xsl:if> &lt;table cellpadding="0" cellspacing="0" border="0" class="ntb-grid-datablock"&gt; &lt;x:at-x:s-key(\'data-source\', $dataTableId)/ntb:data/ntb:e[@xi&amp;gt;=$start and @xi&amp;lt; $end]" &gt; &lt;xsl:sortx:s-@xi" data-type="number" /&gt; &lt;/x:at-&gt; &lt;colgroup&gt; <xsl:for-eachx:s-*/*"> <xsl:if test="@Visible = \'1\' and (position() &gt; $firstColumn and position() &lt;= $lastColumn)"> &lt;col class="ntbcolumn<x:v-x:s-$uniqueId"/>_<x:v-x:s-position()" />"&gt;&lt;/col&gt; </xsl:if> </xsl:for-each> &lt;/colgroup&gt; &lt;/table&gt; &lt;/div&gt;&lt;/x:t-&gt;&lt;x:t- match="ntb:e"&gt; &lt;x:va-x:n-xi"x:s-@xi" /&gt; &lt;x:va-x:n-rowClass"&gt; &lt;xsl:if test="@xi mod 2 = 0"&gt;ntbalternaterow&lt;/xsl:if&gt; &lt;xsl:if test="<x:v-x:s-@rowselectattr=1"/>"&gt;ebarowselected&lt;/xsl:if&gt; &lt;/x:va-&gt; &lt;tr class="ntbrow {$rowClass} ntbrow<x:v-x:s-$uniqueId"/>" xi="{$xi}"&gt; &lt;x:a-x:n-id"&gt;row_&lt;x:v-x:s-$xi" /&gt;<x:v-x:s-$frozenColumnId"/>_<x:v-x:s-$uniqueId" />&lt;/x:a-&gt; <xsl:for-eachx:s-*/*"> <xsl:if test="@Visible = \'1\' and (position() &gt; $firstColumn and position() &lt;= $lastColumn)"> &lt;x:va-x:n-sortString<x:v-x:s-position()-1"/>"&gt; &lt;x:c-&gt; &lt;x:wh- test="$sortColumn=<x:v-x:s-position()-1"/> and $sortDirection=\'Asc\'"&gt;ascending&lt;/x:wh-&gt; &lt;x:wh- test="$sortColumn=<x:v-x:s-position()-1"/> and $sortDirection=\'Desc\'"&gt;descending&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:va-x:n-value<x:v-x:s-position()"/>" &gt; <x:c-> <x:wh- test="not(@xdatafld = \'\')">&lt;x:v-x:s-<x:v-x:s-@xdatafld" />" /&gt;</x:wh-> <!-- @Value will actuall have some escaped XSLT in it like any other bound property --> <x:o-><x:v-x:s-@Value" /></x:o-> </x:c-> &lt;/x:va-&gt; &lt;td ebatype="cell" xi="{$xi}" col="<x:v-x:s-position()-1"/>" value="{$value<x:v-x:s-position()"/>}" &gt; &lt;x:a-x:n-style"&gt;<x:v-x:s-@CssStyle"/>;&lt;/x:a-&gt; &lt;x:a-x:n-id"&gt;cell_&lt;x:v-x:s-$xi" /&gt;_<x:v-x:s-position()-1" />_<x:v-x:s-$uniqueId" />&lt;/x:a-&gt; &lt;x:a-x:n-class"&gt;ntbcellborder<x:v-x:s-$uniqueId"/> ntbcolumndata<x:v-x:s-$uniqueId"/>_<x:v-x:s-position()" /> ntbcolumn&lt;x:v-x:s-$sortString<x:v-x:s-position()-1"/>" /&gt;<xsl:text> </xsl:text>&lt;xsl:text&gt; &lt;/xsl:text&gt;<x:v-x:s-@ClassName"/><xsl:text> </xsl:text>&lt;xsl:text&gt; &lt;/xsl:text&gt;<xsl:if test="@type = \'NUMBER\'">&lt;xsl:if test="$value<x:v-x:s-position()"/> &amp;lt; 0"&gt;ntb-grid-numbercellnegative&lt;/xsl:if&gt;</xsl:if>&lt;/x:a-&gt; &lt;div style="overflow:hidden;white-space:nowrap;"&gt; &lt;x:a-x:n-class"&gt;ntbcell&lt;/x:a-&gt; &lt;x:a-x:n-title"&gt;&lt;x:v-x:s-$value<x:v-x:s-position()"/>"/&gt;&lt;/x:a-&gt; &lt;x:ct-x:n-<x:c-><x:wh- test="@type and not(@type=\'\')"><x:v-x:s-@type" /></x:wh-><x:o->TEXT</x:o-></x:c->"&gt;&lt;x:w-x:n-value"x:s-$value<x:v-x:s-position()"/>" /&gt;&lt;x:w-x:n-mask" &gt;<x:v-x:s-@Mask"/>&lt;/x:w-&gt;&lt;x:w-x:n-negativeMask" &gt;<x:v-x:s-@NegativeMask"/>&lt;/x:w-&gt;&lt;x:w-x:n-datasource" &gt;<x:v-x:s-@DatasourceId"/>&lt;/x:w-&gt;&lt;x:w-x:n-valuefield" &gt;<x:v-x:s-@ValueField"/>&lt;/x:w-&gt;&lt;x:w-x:n-displayfields" &gt;<x:v-x:s-@DisplayFields"/>&lt;/x:w-&gt;&lt;x:w-x:n-checkedvalue" &gt;<x:v-x:s-@CheckedValue"/>&lt;/x:w-&gt;&lt;x:w-x:n-imageurl" &gt;<x:v-x:s-@ImageUrl"/>&lt;/x:w-&gt; &lt;/x:ct-&gt; &lt;/div&gt; &lt;/td&gt; </xsl:if> </xsl:for-each> &lt;/tr&gt;&lt;/x:t-&gt;&lt;x:t-x:n-replaceblank"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:c-&gt; &lt;x:wh- test="not($value) or $value = \'\' or $value = \' \'"&gt;ATOKENTOREPLACE&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-$value" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-replace"&gt; &lt;x:p-x:n-text"/&gt; &lt;x:p-x:n-search"/&gt; &lt;x:p-x:n-replacement"/&gt; &lt;x:c-&gt; &lt;x:wh- test="contains($text, $search)"&gt; &lt;x:v-x:s-substring-before($text, $search)"/&gt; &lt;x:v-x:s-$replacement"/&gt; &lt;x:ct-x:n-replace"&gt; &lt;x:w-x:n-text"x:s-substring-after($text,$search)"/&gt; &lt;x:w-x:n-search"x:s-$search"/&gt; &lt;x:w-x:n-replacement"x:s-$replacement"/&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$text"/&gt; &lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-print-displayfields"&gt; &lt;x:p-x:n-field" /&gt; &lt;x:c-&gt; &lt;x:wh- test="contains($field,\'|\')" &gt; &lt;!-- Here we hardcode a spacer \', \' - this should probably be moved elsewhere. --&gt; &lt;x:v-x:s-concat(@*[name(.)=substring-before($field,\'|\')],\', \')" /&gt; &lt;x:ct-x:n-print-displayfields"&gt; &lt;x:w-x:n-field"x:s-substring-after($field,\'|\')" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-@*[name(.)=$field]" /&gt; &lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-replace-break"&gt; &lt;x:p-x:n-text"/&gt; &lt;x:ct-x:n-replace"&gt; &lt;x:w-x:n-text"x:s-$text"/&gt; &lt;x:w-x:n-search"x:s-\'&amp;amp;#xa;\'"/&gt; &lt;x:w-x:n-replacement"x:s-\'&amp;lt;br/&amp;gt;\'"/&gt; &lt;/x:ct-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-TEXT"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$value" /&gt; &lt;/x:ct-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-PASSWORD"&gt; &lt;x:p-x:n-value" /&gt; *********&lt;/x:t-&gt;&lt;x:t-x:n-TEXTAREA"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:ct-x:n-replace-break"&gt; &lt;x:w-x:n-text"&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$value" /&gt; &lt;/x:ct-&gt; &lt;/x:w-&gt; &lt;/x:ct-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-NUMBER"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-mask" /&gt; &lt;x:p-x:n-negativeMask" /&gt; &lt;x:va-x:n-number-mask"&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask"&gt;&lt;x:v-x:s-$mask" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;#,###.00&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:va-x:n-negative-number-mask"&gt; &lt;x:c-&gt; &lt;x:wh- test="$negativeMask"&gt;&lt;x:v-x:s-$negativeMask" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-$mask" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:va-x:n-number"&gt; &lt;x:c-&gt; &lt;x:wh- test="$value &amp;lt; 0"&gt; &lt;x:ct-x:n-n:format"&gt; &lt;x:w-x:n-number"x:s-translate($value,\'-\',\'\')" /&gt; &lt;x:w-x:n-mask"x:s-$negative-number-mask" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:ct-x:n-n:format"&gt; &lt;x:w-x:n-number"x:s-$value" /&gt; &lt;x:w-x:n-mask"x:s-$number-mask" /&gt; &lt;/x:ct-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$number" /&gt; &lt;/x:ct-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-IMAGE"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-imageurl" /&gt; &lt;x:va-x:n-url"&gt; &lt;x:c-&gt; &lt;x:wh- test="$imageurl"&gt;&lt;x:v-x:s-$imageurl" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;&lt;x:v-x:s-$value" /&gt;&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; <!-- image editor --> &lt;img border="0" src="{$url}" /&gt;&lt;/x:t-&gt;&lt;x:t-x:n-DATE"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-mask" /&gt; &lt;x:va-x:n-date-mask"&gt; &lt;x:c-&gt; &lt;x:wh- test="$mask"&gt;&lt;x:v-x:s-$mask" /&gt;&lt;/x:wh-&gt; &lt;x:o-&gt;MMM d, yy&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;x:va-x:n-date"&gt; &lt;x:ct-x:n-d:format-date"&gt; &lt;x:w-x:n-date-time"x:s-$value" /&gt; &lt;x:w-x:n-mask"x:s-$date-mask" /&gt; &lt;/x:ct-&gt; &lt;/x:va-&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$date" /&gt; &lt;/x:ct-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-LISTBOX"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-datasource" /&gt; &lt;x:p-x:n-valuefield" /&gt; &lt;x:p-x:n-displayfields" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$datasource"&gt; &lt;xsl:for-eachx:s-key(\'data-source\',$datasource)//*"&gt; &lt;xsl:if test="@*[name(.)=$valuefield and .=$value]"&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"&gt; &lt;x:ct-x:n-print-displayfields"&gt; &lt;x:w-x:n-field"x:s-$displayfields" /&gt; &lt;/x:ct-&gt; &lt;/x:w-&gt; &lt;/x:ct-&gt; &lt;/xsl:if&gt; &lt;/xsl:for-each&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$value" /&gt; &lt;/x:ct-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-LOOKUP"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-datasource" /&gt; &lt;x:p-x:n-valuefield" /&gt; &lt;x:p-x:n-displayfields" /&gt; &lt;x:c-&gt; &lt;x:wh- test="$valuefield = $displayfields"&gt; &lt;x:ct-x:n-TEXT"&gt; &lt;x:w-x:n-value"x:s-$value" /&gt; &lt;/x:ct-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"&gt; &lt;x:c-&gt; &lt;x:wh- test="$datasource"&gt; &lt;x:va-x:n-preset-value" &gt; &lt;xsl:for-eachx:s-key(\'data-source\',$datasource)//*"&gt; &lt;xsl:if test="@*[name(.)=$valuefield and .=$value]"&gt; &lt;x:ct-x:n-print-displayfields"&gt; &lt;x:w-x:n-field"x:s-$displayfields" /&gt; &lt;/x:ct-&gt; &lt;/xsl:if&gt; &lt;/xsl:for-each&gt; &lt;/x:va-&gt; &lt;x:c-&gt; &lt;x:wh- test="$preset-value=\'\'"&gt; &lt;x:v-x:s-$value"/&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$preset-value"/&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:wh-&gt; &lt;x:o-&gt; &lt;x:v-x:s-$value"/&gt; &lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:w-&gt; &lt;/x:ct-&gt; &lt;/x:o-&gt; &lt;/x:c-&gt;&lt;/x:t-&gt;&lt;x:t-x:n-CHECKBOX"&gt; &lt;x:p-x:n-value" /&gt; &lt;x:p-x:n-datasource" /&gt; &lt;x:p-x:n-valuefield" /&gt; &lt;x:p-x:n-displayfields" /&gt; &lt;x:p-x:n-checkedvalue" /&gt; &lt;xsl:for-eachx:s-key(\'data-source\',$datasource)//*"&gt; &lt;xsl:if test="@*[name(.)=$valuefield and .=$value]"&gt; &lt;x:va-x:n-checkString"&gt; &lt;x:c-&gt; &lt;x:wh- test="$value=$checkedvalue"&gt;checked&lt;/x:wh-&gt; &lt;x:o-&gt;unchecked&lt;/x:o-&gt; &lt;/x:c-&gt; &lt;/x:va-&gt; &lt;div style="overflow:hidden;"&gt; &lt;div style="float:left;" class="ntbcheckbox ntbcheckbox{$checkString} checkbox{$checkString}" checked="{$value}" width="10" &gt;ATOKENTOREPLACE&lt;/div&gt;&lt;span&gt;&lt;x:v-x:s-@*[name(.)=$displayfields]" /&gt;&lt;/span&gt; &lt;/div&gt; &lt;/xsl:if&gt; &lt;/xsl:for-each&gt;&lt;/x:t-&gt;&lt;x:t-x:n-LINK"&gt; &lt;x:p-x:n-value" /&gt; &lt;span class="ntbhyperlinkeditor"&gt; &lt;x:ct-x:n-replaceblank"&gt; &lt;x:w-x:n-value"x:s-$value" /&gt; &lt;/x:ct-&gt; &lt;/span&gt;&lt;/x:t-&gt;<!--This can be used as an insertion point for column templates--> &lt;!--COLUMN-TYPE-TEMPLATES--&gt;&lt;/xsl:stylesheet&gt;</x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.grid");
nitobi.grid.rowGeneratorXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_rowGeneratorXslProc));

var temp_ntb_sortXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="yes" /> <x:p-x:n-column"x:s-@xi"> </x:p-> <x:p-x:n-dir"x:s-\'ascending\'"> </x:p-> <x:p-x:n-type"x:s-\'text\'"> </x:p-> <x:t- match="*|@*"> <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:t-> <x:t- match="ntb:data"> <xsl:copy> <x:at-x:s-@*"/> <xsl:for-eachx:s-ntb:e"> <xsl:sortx:s-@*[name() =$column]" order="{$dir}" data-type="{$type}"/> <xsl:copy> <x:a-x:n-xi"> <x:v-x:s-position()-1" /> </x:a-> <x:at-x:s-@*" /> </xsl:copy> </xsl:for-each> </xsl:copy> </x:t-><x:t- match="@xi" /></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.sortXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_sortXslProc));

var temp_ntb_fillColumnXslProc='<?xml version="1.0" encoding="utf-8"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" omit-xml-declaration="no" /> <x:p-x:n-startRowIndex"x:s-0" ></x:p-> <x:p-x:n-endRowIndex"x:s-10000" ></x:p-> <x:p-x:n-value"x:s-test"></x:p-> <x:p-x:n-column"x:s-a"></x:p-> <x:t- match="@* | node()" > <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:t-> <x:t- match="/ntb:grid/ntb:datasources/ntb:datasource/ntb:data/ntb:e"> <x:c-> <x:wh- test="(number(@xi) &gt;= $startRowIndex) and (number(@xi) &lt;= $endRowIndex)"> <xsl:copy> <xsl:copy-ofx:s-@*" /> <x:a-x:n-{$column}"><x:v-x:s-$value" /></x:a-> </xsl:copy> </x:wh-> <x:o-> <xsl:copy> <x:at-x:s-@*|node()" /> </xsl:copy> </x:o-> </x:c-> </x:t-></xsl:stylesheet> ';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.fillColumnXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_fillColumnXslProc));

var temp_ntb_updategramTranslatorXslProc='<?xml version="1.0"?><xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ntb="http://www.nitobi.com"> <xsl:output method="xml" encoding="utf-8" omit-xml-declaration="yes"/> <x:p-x:n-datasource-id"x:s-\'_default\'"></x:p-> <x:p-x:n-xkField" ></x:p-> <x:t- match="/"> <root> <x:at-x:s-//ntb:datasource[@id=$datasource-id]/ntb:data/ntb:e" /> </root> </x:t-> <x:t- match="ntb:e"> <x:c-> <x:wh- test="@xac=\'d\'"> <delete xi="{@xi}" xk="{@*[name() = $xkField]}"></delete> </x:wh-> <x:wh- test="@xac=\'i\'"> <insert><xsl:copy-ofx:s-@*[not(name() = $xkField) and not(name() = \'xac\')]" /><x:a-x:n-xk"><x:v-x:s-@*[name() = $xkField]" /></x:a-></insert> </x:wh-> <x:wh- test="@xac=\'u\'"> <update><xsl:copy-ofx:s-@*[not(name() = $xkField) and not(name() = \'xac\')]" /><x:a-x:n-xk"><x:v-x:s-@*[name() = $xkField]" /></x:a-></update> </x:wh-> </x:c-> </x:t-></xsl:stylesheet>';
nitobi.lang.defineNs("nitobi.data");
nitobi.data.updategramTranslatorXslProc = nitobi.xml.createXslProcessor(nitobiXmlDecodeXslt(temp_ntb_updategramTranslatorXslProc));


