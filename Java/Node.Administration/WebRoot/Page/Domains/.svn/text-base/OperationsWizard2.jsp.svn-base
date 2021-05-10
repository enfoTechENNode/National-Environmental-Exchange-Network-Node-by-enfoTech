<%@ page import="Node.Phrase, java.util.ArrayList, com.enfotech.component.UI.webControl.beans.TabControlBean, DataFlow.Component.Interface.WebServiceParameter,Node.Utils.Utility" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="OperationsWizardBean" class="Node.Web.Administration.Bean.Domains.OperationsWizardBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO 8859-1">
		<title>Exchange Network Node</title>
			   <link rel="stylesheet" type="text/css" href="../../ext/resources/css/ext-all.css" />
			   <link rel="stylesheet" type="text/css" href="../../ext/resources/css/portal.css" />
			   <script type="text/javascript" src="../../ext/adapter/ext/ext-base.js"></script>
			   <script type="text/javascript" src="../../ext/ext-all.js"></script>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript" type="text/javascript">
               	function submitForm (action)
                {
                    waitingBlock.switchFloatDiv();
                	/*var isWizard = '<%=OperationsWizardBean.getIsWizard()%>'
	                if(action=='SAVE' && isWizard=='yes'){
						Ext.MessageBox.confirm('Confirm', 'Before goto Wizard, would you like to save your changes?', function(btn, opt){
							if(btn=='yes'){
					            document.form1.act.value = action;
					            document.form1.submit();						    	                													
							}
						} );
	                }else{
			            document.form1.act.value = action;
			            document.form1.submit();						    	                
	                }*/
	                document.form1.act.value = action;
			        document.form1.submit();						    	                
	                
                }
                function toggleDefaultProcess ()
                {
	                if(document.form1.useDefault != null){
	                  if (document.form1.useDefault.checked)
	                    Utils.hideDiv('divProcess');
	                  else
	                    Utils.showDiv('divProcess');	                
	                }
                }
                function toggleAnytime ()
                {
                  if (document.form1.anytime.checked)
                    Utils.hideDiv('divAnytime');
                  else
                    Utils.showDiv('divAnytime');
                }
                function toggleUseSubmit ()
                {
                  if (document.form1.useSubmit.checked)
                    Utils.showDiv('divUseSubmit');
                  else
                    Utils.hideDiv('divUseSubmit');
                }
                function toggleUseAuthorization ()
                {
                  if (document.form1.useAuthorization.checked)
                    Utils.showDiv('divUseAuthorization');
                  else
                    Utils.hideDiv('divUseAuthorization');
                }
                function changeWebService ()
                {
                  index = document.form1.webService.selectedIndex;
                  text = document.form1.webService.options[index].text;
                  if (text != "QUERY" && text != "SOLICIT") { // Submit, Download, Notify
                    Utils.showDiv('divDefault');
                    toggleDefaultProcess();
                    Utils.hideDiv('divSolicit');
                    Utils.hideDiv('divParameter');
                  }
                  if (text == "QUERY") { // Query
                    Utils.hideDiv('divDefault');
                    Utils.showDiv('divProcess');
                    Utils.showDiv('divParameter');
                    Utils.hideDiv('divSolicit');
                  }
                  if (text == "SOLICIT") { // Solicit
                    Utils.hideDiv('divDefault');
                    Utils.showDiv('divProcess');
                    Utils.showDiv('divParameter');
                    Utils.showDiv('divSolicit');
		            <%
		              if (OperationsWizardBean.getIsWizard().equalsIgnoreCase("no")) {
		            %>
                    toggleAnytime();
                    toggleUseSubmit();
		            <%
		              }
		            %>
                  }
                  if (text == "AUTHENTICATE") { // Authenticate
                    Utils.showDiv('divAuthorize');
                  }
                  else {
                    Utils.hideDiv('divAuthorize');
                  }
                }
                function changeScheduleType ()
                {
                  index = document.form1.scheduleType.selectedIndex;
                  text = document.form1.scheduleType.options[index].text;
                  if (text == "Inactive") {
                    Utils.hideDiv('divRun');
                    Utils.hideDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Once") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Seconds") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Days") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Weeks") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.showDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Months") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Years") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.showDiv('divMonthOfYear');
                  }
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Domains/OperationsWizard.do" method="POST" name="form1">
            <eftwc:DivCalendar dateFormat="MDY" dateSpliter="/" yearNavigate="true" />
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Operations.do">Operation Management</a> - <%=OperationsWizardBean.getTitle()%></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td>
                        <table width="20%">
                          <tr>
                      <td></td>
                        <%
                          String title = OperationsWizardBean.getTitle();
                          if (title == null || !title.equals("New Operation")) {
                        %>
                      <td></td>
                        <%
                          }
                        %>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <%
                      String message = OperationsWizardBean.getMessage();
                      if (message != null && !message.equals("")) {
                    %>
                    <tr>
                      <td class="formFldLabel errFont"><%=message%></td>
                    </tr>
                    <%
                      }
                    %>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formFldLabel"><font color="red">*</font>&nbsp;Required Fields</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <logic:notEqual name="OperationsWizardBean" property="title" value="New Operation">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td>
                        <table>
                          <tr>
                            <td class="formTitle">Operation Details</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Operation Name:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel">
                                    <%
                                      if (!OperationsWizardBean.getTitle().equals("New Operation")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" disabled="true" maxLength="50" />
                                    <%
                                      }
                                      else {
                                        String nameError = OperationsWizardBean.getNameError();
                                        if (nameError != null && !nameError.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" errorMsg="<%=nameError%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="50" />
                                    <%
                                        }
                                        else {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" maxLength="50" />
                                    <%
                                        }
                                      }
                                    %>
                                  </td>
                                  <td class="formFldLabel">Operation Status:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <%
                                      String statusError = OperationsWizardBean.getStatusError();
                                      if (statusError != null && !statusError.equals("")) {
                                    %>
                                    <eftwc:MessageLabel msgText="<%=statusError%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" ></eftwc:MessageLabel>
                                    <%
                                      }
                                    %>
                                    <select name="status">
                                      <%
                                        String status = OperationsWizardBean.getStatus();
                                      %>
                                      <option <%if(status.equals("Running")){%>selected="selected"<%}%>>Running</option>
                                      <option <%if(status.equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                      <option <%if(status.equals("Inactive")){%>selected="selected"<%}%>>Inactive</option>
                                    </select>
                                  </td>
                                </tr>
		                       <%
		                         String isWizard = OperationsWizardBean.getIsWizard();
		                       %>
								<%//if(isWizard.equalsIgnoreCase("no")){%>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Status Message:<br />
                                    <eftwc:HighlightTextBox id="statusMessage" rows="4" size="100%" text="<%=OperationsWizardBean.getStatusMessage()%>" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Description:<br />
                                    <eftwc:HighlightTextBox id="description" rows="4" size="100%" text="<%=OperationsWizardBean.getDescription()%>" />
                                  </td>
                                </tr>
                                <%//}%>
			                    <tr>
			                      <td class="formFldLabel" colspan="4">Operation Type:<br/>
			                      </td>
			                    </tr>
				                <tr>
				                  <td>
				                     <select name="OperationType" onchange="changeOperationType()" disabled="true">
				                       <%
				                         String type = OperationsWizardBean.getType();
				                       %>
				                       <option <%if(type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)){%>selected="selected"<%}%>><%=Phrase.WEB_SERVICE_OPERATION %></option>
				                       <option <%if(type.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){%>selected="selected"<%}%>><%=Phrase.SCHEDULED_TASK_OPERATION%></option>
				                     </select>
				                  </td>
				                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              </logic:notEqual>
              <logic:equal name="OperationsWizardBean" property="type" value="<%=Phrase.WEB_SERVICE_OPERATION%>">
                <tr>
                  <td>
                    <table width="100%">
                      <tr>
                        <td>
                          <table width="100%">
                            <tr>
                              <td class="formTitle">Web Service Details</td>
                            </tr>
                            <tr>
                              <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Web Service:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="webService"<%if(!OperationsWizardBean.getTitle().equals("New Operation")){%> disabled="disabled"<%}else{%> onchange="changeWebService()"<%}%>>
                                      <%
                                        String webService = OperationsWizardBean.getWebService();
                                        String[] availWS = OperationsWizardBean.getAvailWebServices();
                                        if (availWS != null) {
                                          for (int i = 0; i < availWS.length; i++) {
                                      %>
                                      <option <%if(availWS[i].equalsIgnoreCase(webService)){%>selected="selected"<%}%>><%=availWS[i]%></option>
                                      <%
                                          }
                                        }
                                      %>
                                    </select>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                     <tr>
                        <td>
                          <table width="100%">
                            <tr>
                              <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Include in publishing to ENDS:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="isPublish" >
                                      <%
                                        String isPublish = OperationsWizardBean.getIsPublish();
                                      %>
                                      <option <%if(isPublish.equalsIgnoreCase("")){%>selected="selected"<%}%> value=""></option>
                                      <option <%if(isPublish.equalsIgnoreCase("Y")){%>selected="selected"<%}%> value="Y">Yes</option>
                                      <option <%if(isPublish.equalsIgnoreCase("N")){%>selected="selected"<%}%> value="N">No</option>
                                    </select>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Require explicit NAAS rights to execute this operation:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="isPolicyDeny" >
                                      <%
                                        String isPolicyDeny = OperationsWizardBean.getIsPolicyDeny();
                                      %>
                                      <option <%if(isPolicyDeny.equalsIgnoreCase("") || isPolicyDeny.equalsIgnoreCase("permit")){%>selected="selected"<%}%> value="N">No</option>
                                      <option <%if(isPolicyDeny.equalsIgnoreCase("deny")){%>selected="selected"<%}%> value="Y">Yes</option>
                                    </select>
                                  </td>
                                </tr>
                                       <%
                                        String isRest = OperationsWizardBean.getIsRest();
                                       if(!Utility.isNullOrEmpty(isRest)){
                                       %>
                                <tr>
                                  <td class="formFldLabel">Enable RESTful Service:&nbsp;</td>
                                  <td>
                                     <select name="isRest" >
                                     	<option <%if(isRest.equalsIgnoreCase("")){%>selected="selected"<%}%> value=""></option>
                                       	<option <%if(isRest.equalsIgnoreCase("Y")){%>selected="selected"<%}%> value="Y">Yes</option>
                                   		<option <%if(isRest.equalsIgnoreCase("N")){%>selected="selected"<%}%> value="N">No</option>
                                    </select>                                          
                                  </td>
                                </tr>
									<%                                   	   
                                       }
                                     %>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <%
                      String name = "";
                      String value = "";
					  String error;
                    %>
	              	<logic:equal name="OperationsWizardBean" property="isWizard" value="no">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Pre-Process(es)</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                      <tr>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Select</td>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Sequence</td>
                                        <td class="gridHead1" nowrap="nowrap" width="80%">Class Name</td>
                                      </tr>
                                      <%
                                        ArrayList preList = OperationsWizardBean.getPreProcesses();
                                        if (preList != null) {
                                          for (int i = 0; i < preList.size(); i++) {
                                            ArrayList pre = (ArrayList)preList.get(i);
                                            if (pre != null && pre.size() == 2) {
                                              name = "preClass" + i;
                                              value = (String)pre.get(1);
                                      %>
                                      <tr id="pre<%=i%>" class="trOut" onmouseover="Utils.setIDClass('pre<%=i%>','trOver')" onmouseout="Utils.setIDClass('pre<%=i%>','trOut')">
                                        <td class="RowPlain" align="center"><input type="checkbox" name="preCheck<%=i%>"/></td>
                                        <td class="RowPlain" align="center"><%=(String)pre.get(0)%></td>
                                        <td class="RowPlain"><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>"  /></td>
                                      </tr>
                                      <%
                                            }
                                          }
                                        }
                                      %>
                                      <tr id="preAdd" class="trOut" onmouseover="Utils.setIDClass('preAdd','trOver')" onmouseout="Utils.setIDClass('preAdd','trOut')">
                                        <td class="RowPlain" align="center"></td>
                                        <td class="RowPlain" align="center"><eftwc:HighlightTextBox id="preSequenceAdd" size="2" maxLength="2" /></td>
                                        <td class="RowPlain"><eftwc:HighlightTextBox id="preClassAdd" size="80"  /></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <table width="50%">
                                      <tr>
                                        <td><input type="button" value="Add Pre-Process" onclick="submitForm('ADD_PRE_PROCESS')"/></td>
                                        <td><input type="button" value="Remove Selected Pre-Processes" onclick="submitForm('REMOVE_PRE_PROCESS')"/></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
	                </logic:equal>
                    <tr>
                      <td>
                        <table>
	              		  <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
                          <tr>
                            <td class="formTitle">Process</td>
                          </tr>
	              		  </logic:equal>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">
                                    <div id="divDefault">
				              		  <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
	                                      <input type="checkbox" name="useDefault" onclick="toggleDefaultProcess()" <%if(OperationsWizardBean.getUseDefault().equals("on")){%>checked="checked"<%}%> />
	                                      Use Default Process
	              		  			  </logic:equal>
                                    </div>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td>
                                    <div id="divProcess">
	              					  <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
	                                      <table width="100%">
	                                        <tr>
	                                          <td class="formFldLabel">Class Name:&nbsp;<font color="red">*</font></td>
	                                          <td class="formFldLabel">
	                                            <%
	                                              error = OperationsWizardBean.getProcClassError();
	                                              if (error != null && !error.equals("")) {
	                                            %>
	                                            <eftwc:HighlightTextBox id="procClass" size="60" text="<%=OperationsWizardBean.getProcClass()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
	                                            <%
	                                              }
	                                              else {
	                                            %>
	                                            <eftwc:HighlightTextBox id="procClass" size="60" text="<%=OperationsWizardBean.getProcClass()%>" />
	                                            <%
	                                              }
	                                            %>
	                                          </td>
	                                        </tr>
	                                       </table>
	              					   </logic:equal>
	              					   <div id="divParameter">
					                      <table width="100%">
					                        <tr>
					                          <td class="formFldLabel" colspan="8">Parameter Names</td>
					                        </tr>
					                        <tr>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Select</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center" width="70px">Sequence</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Name</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Type</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Type Descriptor</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Occurence Number</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Encoding</td>
					                          <td class="gridHead1" nowrap="nowrap" align="center">Parameter Required Indicator</td>
					                        </tr>
					                            <%
					                            // WI 21296
					                              ArrayList webServiceparams = OperationsWizardBean.getWebServiceParameters();
					                              if (webServiceparams != null && webServiceparams.size() > 0) {
					                                for (int i = 0; i < webServiceparams.size(); i++) {
														ArrayList temp = (ArrayList)webServiceparams.get(i);
														String seqName = "sequence" + i;
														String paramName = "paramName" + i;
														String paramType = "paramType" + i;
														String paramTypeDesc = "paramTypeDesc" + i;
														String paramOccNo = "paramOccNo" + i;
														String paramEncoding = "paramEncoding" + i;
														String paramReqInd = "paramReqInd" + i;
					                            		if (temp != null && WebServiceParameter.getParameters().contains((String)temp.get(1))){
					                            %>
				                            <%			}else{%>
                                     		<tr id="preWebService<%=i%>" class="trOut" onmouseover="Utils.setIDClass('preWebService<%=i%>','trOver')" onmouseout="Utils.setIDClass('preWebService<%=i%>','trOut')">
					                          <td class="formFldLabel"><input type="checkbox" name="removeParam<%=i%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=seqName%>" size="2" maxLength="2" text="<%=(String)temp.get(0)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramName%>" size="35" text="<%=(String)temp.get(1)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramType%>" size="10" text="<%=(String)temp.get(2)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramTypeDesc%>" size="21" text="<%=(String)temp.get(3)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramOccNo%>" size="25" text="<%=(String)temp.get(4)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramEncoding%>" size="5" text="<%=(String)temp.get(5)%>" /></td>
					                          <td class="RowPlain"><eftwc:HighlightTextBox id="<%=paramReqInd%>" size="5" text="<%=(String)temp.get(6)%>" /></td>
				                            </tr>
				                            	
				                            <%			}
				                                }
				                              }
				                            %>
                                      		<tr id="preWebServiceAdd" class="trOut" onmouseover="Utils.setIDClass('preWebServiceAdd','trOver')" onmouseout="Utils.setIDClass('preWebServiceAdd','trOut')">
				                              <td class="formFldLabel"></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="sequenceAdd" size="2" maxLength="2" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramNameAdd" size="35" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramTypeAdd" size="10" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramTypeDescAdd" size="21" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramOccNoAdd" size="25" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramEncodingAdd" size="5" /></td>
				                              <td class="RowPlain"><eftwc:HighlightTextBox id="paramReqIndAdd" size="5" /></td>
				                            </tr>
				                            <tr>
				                              <td colspan="8"><input type="button" value="Add Parameter" onclick="submitForm('ADD_WEBSERVICE_PARAMETERS')"/>
				                              <input type="button" value="Remove Selected Parameters" onclick="submitForm('REMOVE_WEBSERVICE_PARAMETERS')"/></td>
				                            </tr>
				                          </table>
				                       </div>
                                    </div>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
	                          <div id="divSolicit">
	              				  <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
	                                <table width="100%">
	                                  <tr>
	                                    <td>
	                                      <table width="100%">
	                                        <tr>
	                                          <td class="formFldLabel">
	                                            <input type="checkbox" name="anytime" onclick="toggleAnytime()" <%if(OperationsWizardBean.getAnytime().equals("on")){%>checked="checked"<%}%>/>
	                                            &nbsp;Execute Solicit Anytime
	                                          </td>
	                                        </tr>
	                                        <tr>
	                                          <td class="formFldLabel">
	                                            <div id="divAnytime">Execute Between: <eftwc:HighlightTextBox id="beginHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getBeginHour()%>" />:<eftwc:HighlightTextBox id="beginMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getBeginMinute()%>" />:<eftwc:HighlightTextBox id="beginSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getBeginSecond()%>" /> And: <eftwc:HighlightTextBox id="endHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getEndHour()%>" />:<eftwc:HighlightTextBox id="endMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getEndMinute()%>" />:<eftwc:HighlightTextBox id="endSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getEndSecond()%>" /></div>
	                                          </td>
	                                        </tr>
	                                      </table>
	                                    </td>
	                                  </tr>
	                                  <tr>
	                                    <td>
	                                      <table width="100%">
	                                        <tr>
	                                          <td class="formFldLabel"><input type="checkbox" name="useSubmit" onclick="toggleUseSubmit()" <%if(OperationsWizardBean.getUseSubmit().equals("on")){%>checked="checked"<%}%> />
	                                            &nbsp;Attempt Submit if Return URL is Present
	                                          </td>
	                                        </tr>
	                                        <tr>
	                                          <td class="formFldLabel">
	                                            <div id="divUseSubmit">
	                                              User:&nbsp;<eftwc:HighlightTextBox id="submitUserID" size="40" text="<%=OperationsWizardBean.getSubmitUserID()%>" />&nbsp;Password:&nbsp;<eftwc:HighlightTextBox id="submitPassword" size="40" type="password" text="<%=OperationsWizardBean.getSubmitPassword()%>" />
	                                            </div>
	                                          </td>
	                                        </tr>
	                                      </table>
	                                    </td>
	                                  </tr>
	                                </table>
	                           	  </logic:equal>
	                          </div>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <div id="divAuthorize">
	              		    <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
	                          <table>
	                            <tr>
	                              <td class="formTitle">Authorization</td>
	                            </tr>
	                            <tr>
	                              <td>
	                                <table width="100%">
	                                  <tr>
	                                    <td class="formFldLabel">
	                                      <input type="checkbox" name="useAuthorization" onclick="toggleUseAuthorization()" <%if(OperationsWizardBean.getUseAuthorization().equals("on")){%>checked="checked"<%}%> />
	                                      Use Authorization
	                                    </td>
	                                  </tr>
	                                </table>
	                              </td>
	                            </tr>
	                            <tr>
	                              <td>
	                                <table>
	                                  <tr>
	                                    <td>
	                                      <div id="divUseAuthorization">
	                                        <table width="100%">
	                                          <tr>
	                                            <td class="formFldLabel">Class Name:&nbsp;<font color="red">*</font></td>
	                                            <td class="formFldLabel">
	                                              <%
	                                                error = OperationsWizardBean.getAuthorizationClassNameError();
	                                                if (error != null && !error.equals("")) {
	                                              %>
	                                              <eftwc:HighlightTextBox id="authorizationClassName" size="60" text="<%=OperationsWizardBean.getAuthorizationClassName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
	                                              <%
	                                                }
	                                                else {
	                                              %>
	                                              <eftwc:HighlightTextBox id="authorizationClassName" size="60" text="<%=OperationsWizardBean.getAuthorizationClassName()%>" />
	                                              <%
	                                                }
	                                              %>
	                                            </td>
	                                          </tr>
	                                        </table>
	                                      </div>
	                                    </td>
	                                  </tr>
	                                </table>
	                              </td>
	                            </tr>
	                          </table>
	                      </logic:equal>
                        </div>
                      </td>
                    </tr>
	              	<logic:equal name="OperationsWizardBean" property="isWizard" value="no">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Post-Process(es)</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                      <tr>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Select</td>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Sequence</td>
                                        <td class="gridHead1" nowrap="nowrap" width="80%">Class Name</td>
                                      </tr>
                                      <%
                                        ArrayList postList = OperationsWizardBean.getPostProcesses();
                                        if (postList != null) {
                                          for (int i = 0; i < postList.size(); i++) {
                                            ArrayList post = (ArrayList)postList.get(i);
                                            if (post != null && post.size() == 2) {
                                               name = "postClass" + i;
                                               value = (String)post.get(1);
                                      %>
                                      <tr id="post<%=i%>" class="trOut" onmouseover="Utils.setIDClass('post<%=i%>','trOver')" onmouseout="Utils.setIDClass('post<%=i%>','trOut')">
                                        <td class="RowPlain" align="center"><input type="checkbox" name="postCheck<%=i%>"/></td>
                                        <td class="RowPlain" align="center"><%=(String)post.get(0)%></td>
                                        <td class="RowPlain"><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>"  /></td>
                                      </tr>
                                      <%
                                            }
                                          }
                                        }
                                      %>
                                      <tr id="postAdd" class="trOut" onmouseover="Utils.setIDClass('postAdd','trOver')" onmouseout="Utils.setIDClass('postAdd','trOut')">
                                        <td class="RowPlain" align="center"></td>
                                        <td class="RowPlain" align="center"><eftwc:HighlightTextBox id="postSequenceAdd" size="2" maxLength="2" /></td>
                                        <td class="RowPlain"><eftwc:HighlightTextBox id="postClassAdd" size="80"  /></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <table width="50%">
                                      <tr>
                                        <td><input type="button" value="Add Post-Process" onclick="submitForm('ADD_POST_PROCESS')"/></td>
                                        <td><input type="button" value="Remove Selected Post-Processes" onclick="submitForm('REMOVE_POST_PROCESS')" /></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
	              	</logic:equal>
                  </table>
                </td>
              </tr>
              </logic:equal>
              <logic:equal name="OperationsWizardBean" property="type" value="<%=Phrase.SCHEDULED_TASK_OPERATION%>">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Task Plug-in</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
			        <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
                                <tr>
                                  <td>
	              					<%String error; %>
                                    <table width="100%">
                                      <tr>
                                        <td class="formFldLabel">Class Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = OperationsWizardBean.getTaskClassNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="taskClassName" size="40" text="<%=OperationsWizardBean.getTaskClassName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="taskClassName" size="40" text="<%=OperationsWizardBean.getTaskClassName()%>" />
                                          <%
                                            }
                                          %>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                    </logic:equal>
                                <tr>
                                  <td>
                                    <table width="100%">
                                      <tr>
                                        <td class="formFldLabel" colspan="4">Parameter Values</td>
                                      </tr>
                                      <tr>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Select</td>
                                        <td class="gridHead1" nowrap="nowrap" width="10%" align="center">Sequence</td>
                                        <td class="gridHead1" nowrap="nowrap" width="30%" align="center">Parameter Name</td>
                                        <td class="gridHead1" nowrap="nowrap" width="30%">Parameter Value</td>
                                      </tr>
                                        <%
                                          ArrayList params = OperationsWizardBean.getParameters();
                                          if (params != null && params.size() > 0) {
                                            for (int i = 0; i < params.size(); i++) {
                                              ArrayList temp = (ArrayList)params.get(i);
                                              String seqName = "sequence" + i;
                                              String paramName = "paramName" + i;
                                              String paramValue = "paramValue" + i;
                                        %>
                                      <tr>
                                        <td class="formFldLabel"><input type="checkbox" name="removeParam<%=i%>" /></td>
                                        <td><eftwc:HighlightTextBox id="<%=seqName%>" size="2" maxLength="2" text="<%=(String)temp.get(0)%>" /></td>
                                        <td><eftwc:HighlightTextBox id="<%=paramName%>" size="35" text="<%=(String)temp.get(1)%>" /></td>
                                        <td><eftwc:HighlightTextBox id="<%=paramValue%>" size="35" text="<%=(String)temp.get(2)%>" /></td>
                                      </tr>
                                        <%
                                            }
                                          }
                                        %>
                                      <tr>
                                        <td class="formFldLabel"></td>
                                        <td><eftwc:HighlightTextBox id="sequenceAdd" size="2" maxLength="2" /></td>
                                        <td><eftwc:HighlightTextBox id="paramNameAdd" size="35" /></td>
                                        <td><eftwc:HighlightTextBox id="paramValueAdd" size="35" /></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <table width="50%">
                                      <tr>
                                        <td><input type="button" value="Add Parameter" onclick="submitForm('ADD_TASK_PARAMETERS')"/></td>
                                        <td><input type="button" value="Remove Selected Parameters" onclick="submitForm('REMOVE_TASK_PARAMETERS')"/></td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Scheduling</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Type&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="scheduleType" onchange="changeScheduleType()">
                                      <%
                                        String scheduleType = OperationsWizardBean.getScheduleType();
                                      %>
                                      <option value="INACTIVE" <%if(scheduleType.equals("INACTIVE")){%>selected="selected"<%}%>>Inactive</option>
                                      <option value="ONCE" <%if(scheduleType.equals("ONCE")){%>selected="selected"<%}%>>Once</option>
                                      <option value="SECONDS" <%if(scheduleType.equals("SECONDS")){%>selected="selected"<%}%>>Seconds</option>
                                      <option value="DAYS" <%if(scheduleType.equals("DAYS")){%>selected="selected"<%}%>>Days</option>
                                      <option value="WEEKLY" <%if(scheduleType.equals("WEEKLY")){%>selected="selected"<%}%>>Weeks</option>
                                      <option value="MONTHLY" <%if(scheduleType.equals("MONTHLY")){%>selected="selected"<%}%>>Months</option>
                                      <option value="YEARLY" <%if(scheduleType.equals("YEARLY")){%>selected="selected"<%}%>>Years</option>
                                    </select>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td colspan="2">
                                    <div id="divStartDate">
                                      <table width="50%">
                                        <tr>
                                          <td class="formFldLabel" nowrap="nowrap"><div id="divStartDate">Starting Date and Time:</div></td>
                                          <td class="formFldLabel" nowrap="nowrap"><div id="divStartDate">
                                            <%
                                              String error = OperationsWizardBean.getStartDateError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:DatePicker id="startDate" text="<%=OperationsWizardBean.getStartDate()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:DatePicker id="startDate" text="<%=OperationsWizardBean.getStartDate()%>" />
                                            <%
                                              }
                                            %>
                                          </div></td>
                                          <td class="formFldLabel" nowrap="nowrap"><div id="divStartDate">
                                            <%
                                              error = OperationsWizardBean.getTaskStartTimeError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="taskStartHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartHour()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />:<eftwc:HighlightTextBox id="taskStartMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartMinute()%>" />:<eftwc:HighlightTextBox id="taskStartSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartSecond()%>" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="taskStartHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartHour()%>" />:<eftwc:HighlightTextBox id="taskStartMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartMinute()%>" />:<eftwc:HighlightTextBox id="taskStartSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskStartSecond()%>" />
                                            <%
                                              }
                                            %>
                                          </div></td>
                                        </tr>
                                      </table>
                                    </div>
                                  </td>
                                </tr>
                                <tr>
                                  <td colspan="2">
                                    <div id="divEndDate">
                                      <table width="50%">
                                        <tr>
                                          <td class="formFldLabel" nowrap="nowrap">Ending Date and Time:</td>
                                          <td class="formFldLabel" nowrap="nowrap">
                                            <%
                                              error = OperationsWizardBean.getEndDateError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:DatePicker id="endDate" text="<%=OperationsWizardBean.getEndDate()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:DatePicker id="endDate" text="<%=OperationsWizardBean.getEndDate()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                          <td class="formFldLabel" nowrap="nowrap">
                                            <%
                                              error = OperationsWizardBean.getTaskEndTimeError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="taskEndHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndHour()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />:<eftwc:HighlightTextBox id="taskEndMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndMinute()%>" />:<eftwc:HighlightTextBox id="taskEndSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndSecond()%>" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="taskEndHour" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndHour()%>" />:<eftwc:HighlightTextBox id="taskEndMinute" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndMinute()%>" />:<eftwc:HighlightTextBox id="taskEndSecond" size="2" maxLength="2" text="<%=OperationsWizardBean.getTaskEndSecond()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                  </td>
                                  </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td>
                                    <div id="divInterval">
                                      <table width="25%">
                                        <tr>
                                          <td class="formFldLabel" nowrap="nowrap">Interval:</td>
                                          <td class="formFldLabel" nowrap="nowrap">
                                            <%
                                              error = OperationsWizardBean.getIntervalError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="interval" text="<%=OperationsWizardBean.getInterval()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="interval" text="<%=OperationsWizardBean.getInterval()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <div id="divDayOfWeek">
                                      <table width="100%">
                                        <%
                                          error = OperationsWizardBean.getDayError();
                                          if (error != null && !error.equals("")) {
                                        %>
                                        <tr>
                                          <td class="formFldLabel"><eftwc:MessageLabel msgText="<%=error%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" /></td>
                                        </tr>
                                        <%
                                          }
                                        %>
                                        <tr>
                                          <td class="formFldLabel">Day of Week</td>
                                          <td class="formFldLabel">
                                            <input type="checkbox" name="sunday" value="1" <%if(OperationsWizardBean.getSunday().equals("1")){%>checked="checked"<%}%> />Sunday
                                            <input type="checkbox" name="monday" value="2" <%if(OperationsWizardBean.getMonday().equals("2")){%>checked="checked"<%}%> />Monday
                                            <input type="checkbox" name="tuesday" value="3" <%if(OperationsWizardBean.getTuesday().equals("3")){%>checked="checked"<%}%> />Tuesday
                                            <input type="checkbox" name="wednesday" value="4" <%if(OperationsWizardBean.getWednesday().equals("4")){%>checked="checked"<%}%> />Wednesday
                                            <input type="checkbox" name="thursday" value="5" <%if(OperationsWizardBean.getThursday().equals("5")){%>checked="checked"<%}%> />Thursday
                                            <input type="checkbox" name="friday" value="6" <%if(OperationsWizardBean.getFriday().equals("6")){%>checked="checked"<%}%> />Friday
                                            <input type="checkbox" name="saturday" value="7" <%if(OperationsWizardBean.getSaturday().equals("7")){%>checked="checked"<%}%> />Saturday
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                  </td>
                                </tr>
                                <tr>
                                  <td>
                                    <table width="100%">
                                      <tr>
                                        <td>
                                          <div id="divDayOfMonth">
                                            <table width="25%">
                                              <tr>
                                                <td class="formFldLabel" nowrap="nowrap">Day of Month:</td>
                                                <td class="formFldLabel" nowrap="nowrap">
                                                  <%
                                                    error = OperationsWizardBean.getDayOfMonthError();
                                                    if (error != null && !error.equals("")) {
                                                  %>
                                                  <eftwc:HighlightTextBox id="dayOfMonth" text="<%=OperationsWizardBean.getDayOfMonth()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                                  <%
                                                    }
                                                    else {
                                                  %>
                                                  <eftwc:HighlightTextBox id="dayOfMonth" text="<%=OperationsWizardBean.getDayOfMonth()%>" />
                                                  <%
                                                    }
                                                  %>
                                                </td>
                                              </tr>
                                            </table>
                                          </div>
                                        </td>
                                        <td>
                                          <div id="divMonthOfYear">
                                            <table width="25%">
                                              <tr>
                                                <td class="formFldLabel" nowrap="nowrap">Month of Year:</td>
                                                <td class="formFldLabel" nowrap="nowrap">
                                                  <%
                                                    error = OperationsWizardBean.getMonthOfYearError();
                                                    if (error != null && !error.equals("")) {
                                                  %>
                                                  <eftwc:HighlightTextBox id="monthOfYear" text="<%=OperationsWizardBean.getMonthOfYear()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                                  <%
                                                    }
                                                    else {
                                                  %>
                                                  <eftwc:HighlightTextBox id="monthOfYear" text="<%=OperationsWizardBean.getMonthOfYear()%>" />
                                                  <%
                                                    }
                                                  %>
                                                </td>
                                              </tr>
                                            </table>
                                          </div>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              </logic:equal>
              <tr>
                <td>
                  <table width="20%">
                    <tr>
			            <logic:equal name="OperationsWizardBean" property="isWizard" value="no">
			            <logic:equal name="OperationsWizardBean" property="title" value="New Operation">
		                <td><eftwc:AquaButton buttonText="Previous" onClickScript="submitForm('BACK')" /></td>
		                <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
		                <td><div id="divRun"></div></td>
		               	</logic:equal>                
			            <logic:notEqual name="OperationsWizardBean" property="title" value="New Operation">
		                <td><eftwc:AquaButton buttonText="Previous" onClickScript="submitForm('CANCEL')" /></td>
		                <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
		                <td><div id="divRun"></div></td>
		               	</logic:notEqual>                
		               	</logic:equal>                
			            <logic:equal name="OperationsWizardBean" property="isWizard" value="yes">
		                <td><eftwc:AquaButton buttonText="Previous" onClickScript="submitForm('BACK')" /></td>
		                <td><eftwc:AquaButton buttonText="Goto Wizard" onClickScript="submitForm('SAVE')" /></td>
		                <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('OnlySAVE')" /></td>
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
	            <%
	              }
	              else {
	            %>
		                <td><div id="divRun"><eftwc:AquaButton buttonText="Run Task" onClickScript="submitForm('RUN')" /></div></td>
	            <%
	              }
	            %>
 			            </logic:equal>                
		                  <%
		                    title = OperationsWizardBean.getTitle();
		                    if (title == null || !title.equals("New Operation")) {
		                  %>
		                <td><eftwc:AquaButton buttonText="Delete" buttonStyle="aquaRed" onClickScript="submitForm('DELETE')" /></td>
		                  <%
		                    }
		                  %>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
           <!-- begin of wait block -->
          <link href="/Node.Administration/eftWC/skin/css/core.css" rel="stylesheet" type="text/css">
            <script src="/Node.Administration/eftWC/js/FloatDiv.js"></script>
            <script language="javascript">
              var waitingBlock = new FloatDiv('waitingBlock','waitingBlockName',99);
              waitingBlock.Opacity = 100;
              waitingBlock.OnTop = true;
              waitingBlock.OnTopAlign = "center";
              waitingBlock.writeDivHeader();
            </script>
            <table style="width:250; border-width:0; margin:0; padding:0; border:1px solid CCCCCC;" cellspacing="0">
              <tr>
                <td style="text-align:center; padding:10px; background-color:#EAEAEA;">
                  <span class="waitBlockFont">Please Wait While the Request is Made...</span>
                  <p><img src="/Node.Administration/eftWC/skin/img/gen/wait_bar.gif"></p>
                </td>
              </tr>
            </table>
            <script language="javascript">
              waitingBlock.writeDivFooter();
              waitingBlock.init();
            </script>
            <!-- end of wait block -->
        </body>
        <script language="javascript" type="text/javascript">
          <!-- Java Script -->
          if (document.form1.webService != null) {
          index = document.form1.webService.selectedIndex;
          text = document.form1.webService.options[index].text;
          if (text != "QUERY" && text != "SOLICIT") { // Submit, Download, Notify
            Utils.showDiv('divDefault');
            <%
              if (OperationsWizardBean.getUseDefault().equals("on")) {
            %>
              Utils.hideDiv('divProcess');
              Utils.hideDiv('divParameter');
            <%
              }
              else {
            %>
              Utils.showDiv('divProcess');
              Utils.hideDiv('divParameter');
            <%
              }
            %>
            Utils.hideDiv('divSolicit');
          }
          if (text == "QUERY") { // Query
            Utils.hideDiv('divDefault');
            Utils.showDiv('divProcess');
            Utils.showDiv('divParameter');
            Utils.hideDiv('divSolicit');
          }
          if (text == "SOLICIT") { // Solicit
            Utils.hideDiv('divDefault');
            Utils.showDiv('divProcess');
            Utils.showDiv('divParameter');
            Utils.showDiv('divSolicit');
          <%
            if (OperationsWizardBean.getIsWizard().equalsIgnoreCase("no")) {
          %>
                toggleAnytime();
                toggleUseSubmit();
          <%
            }
          %>
          }
          if (text == "AUTHENTICATE") { // Authenticate
            Utils.showDiv('divAuthorize');
            <%
              if (OperationsWizardBean.getUseAuthorization().equals("on")) {
            %>
            Utils.showDiv('divUseAuthorization');
            <%
              }
              else {
            %>
            Utils.hideDiv('divUseAuthorization');
            <%
              }
            %>
          }
          else if (text != "AUTHENTICATE") {
            Utils.hideDiv('divAuthorize');
          }
          if (document.form1.scheduleType != null) {
            changeScheduleType();
          }
          }
          if (document.form1.scheduleType != null) {
            index = document.form1.scheduleType.selectedIndex;
            text = document.form1.scheduleType.options[index].text;
                  if (text == "Inactive") {
                    Utils.hideDiv('divRun');
                    Utils.hideDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Once") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Seconds") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Days") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Weeks") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.showDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Months") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Years") {
	            <%
	              if (OperationsWizardBean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
	            %>
                    Utils.hideDiv('divRun');
	            <%
	              }
	              else {
	            %>
                    Utils.showDiv('divRun');
	            <%
	              }
	            %>
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.showDiv('divMonthOfYear');
                  }
          }
        </script>
</html>
