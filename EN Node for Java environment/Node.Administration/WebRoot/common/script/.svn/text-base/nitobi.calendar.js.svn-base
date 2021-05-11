if(typeof (nitobi)=="undefined"||typeof (nitobi.lang)=="undefined"){
alert("The Nitobi framework source could not be found. Is it included before any other Nitobi components?");
}
nitobi.lang.defineNs("nitobi.calendar");
nitobi.calendar.build="6257";
nitobi.calendar.version="1.0.6257";
nitobi.prepare=function(){
ebagdl=0;
ebagd1=9999999999999;
s="var d = new Date().getTime();if ((d<"+ebagdl+") || (d>"+ebagd1+")) {alert('Evaluation period has expired.\\n\\nPlease notify your system administrator.\\n\\nPurchase Information:\\n       NITOBI SOFTWARE\\n\\n       www.nitobi.com\\n       sales@nitobi.com         \\n       Telephone: (604) 685-9287\\n       Fax: (604) 648-9090\\n       Toll-Free: 1-866-6EB-APPS\\n                      (1-866-632-2777)');}";
eval(s);
};
nitobi.lang.defineNs("nitobi.calendar");
if(false){
nitobi.calendar=function(){
};
}
nitobi.calendar.DatePicker=function(_1){
nitobi.prepare();
nitobi.calendar.DatePicker.baseConstructor.call(this,_1);
this.renderer=new nitobi.calendar.Renderer();
this.onSetDate=new nitobi.base.Event();
this.eventMap["setdate"]=this.onSetDate;
if(!this.getStartDate()){
var _2=nitobi.base.DateMath.getMonthStart(this.getDate()||new Date());
this.setStartDate(_2);
}
this.subscribeDeclarationEvents();
};
nitobi.lang.extend(nitobi.calendar.DatePicker,nitobi.ui.Element);
nitobi.base.Registry.getInstance().register(new nitobi.base.Profile("nitobi.calendar.DatePicker",null,false,"ntb:datepicker"));
nitobi.calendar.DatePicker.prototype.getDate=function(){
return this.getDateAttribute("date");
};
nitobi.calendar.DatePicker.prototype.setDate=function(_3){
if(arguments.length<1){
_3=new Date();
}else{
if(arguments.length>1){
_3=eval("new Date("+nitobi.lang.toArray(arguments).join(",")+")");
}else{
if(typeof _3!="object"){
_3=new Date(_3);
}
}
}
if(nitobi.base.DateMath.invalid(_3)){
_3=null;
}
this.setDateAttribute("date",_3);
var _4=this.getHtmlNode("value");
if(_4){
_4.value=this.getFormatter()(_3);
}
this.onSetDate.notify(new nitobi.ui.ElementEventArgs(this,this.onSetDate));
};
nitobi.calendar.DatePicker.prototype.getStartDate=function(){
return this.getDateAttribute("startdate");
};
nitobi.calendar.DatePicker.prototype.setStartDate=function(_5){
_5=nitobi.base.DateMath.subtract(_5,"d",_5.getDay());
this.setDateAttribute("startdate",_5);
};
nitobi.calendar.DatePicker.prototype.isTimePickerEnabled=function(){
return this.getBoolAttribute("timepickerenabled",false);
};
nitobi.calendar.DatePicker.prototype.setTimePickerEnabled=function(_6){
this.setBoolAttribute("timepickerenabled",_6);
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
var _7=this.getStartDate();
_7=nitobi.base.DateMath.getMonthStart(nitobi.base.DateMath.add(_7,"d",42));
this.setStartDate(_7);
this.render();
};
nitobi.calendar.DatePicker.prototype.prevMonth=function(){
var _8=this.getStartDate();
_8=nitobi.base.DateMath.getMonthStart(nitobi.base.DateMath.add(_8,"d",-12));
this.setStartDate(_8);
this.render();
};
nitobi.calendar.DatePicker.prototype.getFormatter=function(){
if(this.formatter){
return this.formatter;
}
eval("var f = "+this.getAttribute("formatter","nitobi.base.DateMath.toIso8601"));
return this.formatter=f;
};
nitobi.calendar.DatePicker.prototype.setFormatter=function(_9){
this.formatter=_9;
};
nitobi.calendar.DatePicker.prototype.handleClick=function(_a,_b){
var td=_a.srcElement;
if(td.nodeName!="TD"){
return;
}
var _d=this.getDate();
if(_d){
var _e=nitobi.base.DateMath.getNumberOfDays(this.getStartDate(),_d)-1;
if(_e>=0&&_e<42){
var _f=2+Math.floor(_e/7);
var col=_e%7;
var _11=this.getHtmlNode("table");
nitobi.html.Css.removeClass(_11.rows[_f].cells[col],"ntb-dp-currentday");
}
}
var tr=_b;
nitobi.html.Css.addClass(td,"ntb-dp-currentday");
var _13=nitobi.base.DateMath.add(nitobi.base.DateMath.clone(this.getStartDate()),"d",(tr.rowIndex-2)*7+td.cellIndex);
this.setDate(_13);
};
nitobi.calendar.DatePicker.prototype.render=function(){
nitobi.calendar.DatePicker.base.render.call(this);
var _14=this.getHtmlNode().getElementsByTagName("tr");
for(var i=2;i<8;i++){
nitobi.html.attachEvent(_14[i],"click",this.handleClick,this);
}
nitobi.html.attachEvent(this.getHtmlNode("nextmonth"),"anyclick",this.nextMonth,this);
nitobi.html.attachEvent(this.getHtmlNode("prevmonth"),"anyclick",this.prevMonth,this);
};
nitobi.calendar.DatePicker.prototype.getMonthNames=function(){
return this.monthNames||(this.monthNames=nitobi.calendar.DatePicker.defaultMonthNames);
};
nitobi.calendar.DatePicker.prototype.setMonthNames=function(_16){
this.monthNames=_16;
};
nitobi.calendar.DatePicker.prototype.getDayNames=function(){
return this.dayNames||(this.dayNames=nitobi.calendar.DatePicker.defaultDayNames);
};
nitobi.calendar.DatePicker.prototype.setDayNames=function(_17){
this.dayNames=_17;
};
nitobi.calendar.DatePicker.defaultMonthNames=["January","February","March","April","May","June","July","August","September","October","November","December"];
nitobi.calendar.DatePicker.defaultDayNames=["S","M","T","W","T","F","S"];
nitobi.lang.defineNs("nitobi.calendar");
nitobi.calendar.Renderer=function(){
nitobi.html.IRenderer.call(this);
};
nitobi.lang.implement(nitobi.calendar.Renderer,nitobi.html.IRenderer);
nitobi.calendar.Renderer.prototype.renderToString=function(_18){
var _19=nitobi.base.DateMath;
var _1a=_18.getDate();
var _1b=_18.getStartDate();
var _1c=_1b.getDate();
var _1d=_1a?_19.getNumberOfDays(_1b,_1a)-1:1000;
var _1e=_19.getMonthDays(_1b)-_1b.getDate()+1;
var _1f=_19.add(_19.clone(_1b),"d",_1e);
var _20=_19.getMonthDays(_1f);
_20=_20+_1e>42?42-_1e:_20;
var id=_18.getId();
var _22=_1e>_20;
var _23=_22?_1b.getFullYear():_1f.getFullYear();
var _24=_18.getMonthNames();
var _25=_24[(_1b.getMonth()+!_22)%12];
var _26=_18.getDayNames();
var str=new nitobi.lang.StringBuilder();
var _28=true;
var _29=false;
var _2a=false;
str.append("<div onselectstart=\"return false;\" id=\""+id+"\" class=\"ntb-dp\" style=\"width:"+_18.getWidth()+"px;height:"+_18.getHeight()+"px;\"><div id=\""+id+".themer\" style=\"width:100%;height:100%;\" class=\""+_18.getCssClass()+"\">");
str.append("<table id=\""+id+".table\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:100%;height:100%;\" class=\"ntb-dp-table\">");
str.append("<thead><tr class=\"ntb-dp-monthheader\">");
str.append("<th><a id=\""+id+".prevmonth\" class=\"ntb-dp-prevmonth\" href=\"#\" onclick=\"return false;\">&#9650;</a></th>");
str.append("<th colspan=\"5\" style=\"width:70%;overflow-x:hidden;\">");
str.append("<a id=\""+id+".month\" class=\"ntb-dp-month\" href=\"#\" onclick=\"return false;\">"+_25+"</a> ");
str.append("<a id=\""+id+".year\" class=\"ntb-dp-year\" href=\"#\" onclick=\"return false;\">"+_23+"</a>");
str.append("</th>");
str.append("<th><a id=\""+id+".nextmonth\" class=\"ntb-dp-nextmonth\" href=\"#\" onclick=\"return false;\">&#9660;</a></th>");
str.append("</tr><tr>");
for(var i=0;i<7;i++){
str.append("<th class=\"ntb-dp-dayheader\">"+_26[i]+"</th>");
}
str.append("</tr></thead><tbody>");
for(var i=0;i<6;i++){
str.append("<tr>");
for(var j=0;j<7;j++){
str.append("<td class=\"");
str.append(!_1d--?"ntb-dp-currentday ":"");
if(!_22&&_28){
str.append("ntb-dp-lastmonth ");
}else{
if((_22&&_28)||(!_22&&_29)){
str.append("ntb-dp-thismonth ");
}else{
str.append("ntb-dp-nextmonth");
}
}
str.append(" ntb-dp-day\">"+_1c+"</td>");
if(_1e){
if(!--_1e){
_28=false;
_29=true;
_1c=1;
}else{
_1c++;
}
}else{
if(_1c==_20){
_29=false;
_2a=true;
}
_1c=_1c==_20?1:_1c+1;
}
}
str.append("</tr>");
}
str.append("</tbody><colgroup span=\"7\" style=\"width:1/7%\"></colgroup></table><input id=\""+id+".value\" name=\""+id+"\" type=\"hidden\" value=\""+_18.getFormattedDate()+"\" /></div></div>");
return str.toString();
};


