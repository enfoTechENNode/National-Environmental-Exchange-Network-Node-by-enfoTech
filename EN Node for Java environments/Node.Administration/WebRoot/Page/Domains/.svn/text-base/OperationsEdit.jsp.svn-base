<%@ page import="Node.Phrase, java.util.ArrayList, com.enfotech.component.UI.webControl.beans.TabControlBean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="OperationsEditBean" class="Node.Web.Administration.Bean.Domains.OperationsEditBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript" type="text/javascript">
               	function submitForm (action)
                {
                  document.form1.act.value = action;
                  document.form1.submit();
                }
                function toggleDefaultProcess ()
                {
                  if (document.form1.useDefault.checked)
                    Utils.hideDiv('divProcess');
                  else
                    Utils.showDiv('divProcess');
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
                  }
                  if (text == "QUERY") { // Query
                    Utils.hideDiv('divDefault');
                    Utils.showDiv('divProcess');
                    Utils.hideDiv('divSolicit');
                  }
                  if (text == "SOLICIT") { // Solicit
                    Utils.hideDiv('divDefault');
                    Utils.showDiv('divProcess');
                    Utils.showDiv('divSolicit');
                    toggleAnytime();
                    toggleUseSubmit();
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
                    Utils.hideDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Once") {
                    Utils.showDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Seconds") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Days") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Weeks") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.showDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Months") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Years") {
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
          <form action="/Node.Administration/Page/Domains/OperationsEdit.do" method="POST" name="form1">
            <eftwc:DivCalendar dateFormat="MDY" dateSpliter="/" yearNavigate="true" />
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Operations.do">Operation Management</a> - <%=OperationsEditBean.getTitle()%></td>
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
                          String title = OperationsEditBean.getTitle();
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
                      String message = OperationsEditBean.getMessage();
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
                                      if (!OperationsEditBean.getTitle().equals("New Operation")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsEditBean.getName()%>" disabled="true" maxLength="50" />
                                    <%
                                      }
                                      else {
                                        String error = OperationsEditBean.getNameError();
                                        if (error != null && !error.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsEditBean.getName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="50" />
                                    <%
                                        }
                                        else {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsEditBean.getName()%>" maxLength="50" />
                                    <%
                                        }
                                      }
                                    %>
                                  </td>
                                  <td class="formFldLabel">Operation Status:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <%
                                      String error = OperationsEditBean.getStatusError();
                                      if (error != null && !error.equals("")) {
                                    %>
                                    <eftwc:MessageLabel msgText="<%=error%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" ></eftwc:MessageLabel>
                                    <%
                                      }
                                    %>
                                    <select name="status">
                                      <%
                                        String status = OperationsEditBean.getStatus();
                                      %>
                                      <option <%if(status.equals("Running")){%>selected="selected"<%}%>>Running</option>
                                      <option <%if(status.equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                      <option <%if(status.equals("Inactive")){%>selected="selected"<%}%>>Inactive</option>
                                    </select>
                                  </td>
                                  <%
                                    String type = OperationsEditBean.getType();
                                    if (type != null && type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
                                      String logLevel = OperationsEditBean.getLogLevel();
                                  %>
                                  <td class="formFldLabel">Logging Level</td>
                                  <td>
                                    <select name="logLevel">
                                      <option <%if(logLevel.equals("DEBUG")){%>selected="selected"<%}%>>DEBUG</option>
                                      <option <%if(logLevel.equals("INFO")){%>selected="selected"<%}%>>INFO</option>
                                      <option <%if(logLevel.equals("WARN")){%>selected="selected"<%}%>>WARN</option>
                                      <option <%if(logLevel.equals("ERROR")){%>selected="selected"<%}%>>ERROR</option>
                                      <option <%if(logLevel.equals("FATAL")){%>selected="selected"<%}%>>FATAL</option>
                                    </select>
                                  </td>
                                  <%
                                    }
                                  %>
                                </tr>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Status Message:<br />
                                    <eftwc:HighlightTextBox id="statusMessage" rows="4" size="100%" text="<%=OperationsEditBean.getStatusMessage()%>" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Description:<br />
                                    <eftwc:HighlightTextBox id="description" rows="4" size="100%" text="<%=OperationsEditBean.getDescription()%>" />
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
              <logic:equal name="OperationsEditBean" property="title" value="New Operation">
                <tr>
                  <td>
                    <table width="100%">
                      <tr>
                        <td>
                          <eftwc:TabControl selectedIndex="<%=OperationsEditBean.getSelectedIndex()%>" linkBase="/Node.Administration/eftWC" align="left" tabType="<%=TabControlBean.TABTYPE_AQUA_TRAP%>">
                            <eftwc:TabItem name="Web Service Operation" link="javascript:submitForm('WEB_SERVICE_TAB')" />
                            <eftwc:TabItem name="Scheduled Task Operation" link="javascript:submitForm('SCHEDULED_TASK_TAB')" />
                          </eftwc:TabControl>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </logic:equal>
              <logic:equal name="OperationsEditBean" property="type" value="<%=Phrase.WEB_SERVICE_OPERATION%>">
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
                                    <select name="webService"<%if(!OperationsEditBean.getTitle().equals("New Operation")){%> disabled="disabled"<%}else{%> onchange="changeWebService()"<%}%>>
                                      <%
                                        String webService = OperationsEditBean.getWebService();
                                        String[] availWS = OperationsEditBean.getAvailWebServices();
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
                                        ArrayList preList = OperationsEditBean.getPreProcesses();
                                        String name = "";
                                        String value = "";
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
                    <tr>
                      <td>
                        <table>
                          <tr>
                            <td class="formTitle">Process</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">
                                    <div id="divDefault">
                                      <input type="checkbox" name="useDefault" onclick="toggleDefaultProcess()" <%if(OperationsEditBean.getUseDefault().equals("on")){%>checked="checked"<%}%> />
                                      Use Default Process
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
                                      <table width="100%">
                                        <tr>
                                          <td class="formFldLabel">Class Name:&nbsp;<font color="red">*</font></td>
                                          <td class="formFldLabel">
                                            <%
                                              error = OperationsEditBean.getProcClassError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="procClass" size="60" text="<%=OperationsEditBean.getProcClass()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="procClass" size="60" text="<%=OperationsEditBean.getProcClass()%>" />
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
                              <div id="divSolicit">
                                <table width="100%">
                                  <tr>
                                    <td>
                                      <table width="100%">
                                        <tr>
                                          <td class="formFldLabel">
                                            <input type="checkbox" name="anytime" onclick="toggleAnytime()" <%if(OperationsEditBean.getAnytime().equals("on")){%>checked="checked"<%}%>/>
                                            &nbsp;Execute Solicit Anytime
                                          </td>
                                        </tr>
                                        <tr>
                                          <td class="formFldLabel">
                                            <div id="divAnytime">Execute Between: <eftwc:HighlightTextBox id="beginHour" size="2" maxLength="2" text="<%=OperationsEditBean.getBeginHour()%>" />:<eftwc:HighlightTextBox id="beginMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getBeginMinute()%>" />:<eftwc:HighlightTextBox id="beginSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getBeginSecond()%>" /> And: <eftwc:HighlightTextBox id="endHour" size="2" maxLength="2" text="<%=OperationsEditBean.getEndHour()%>" />:<eftwc:HighlightTextBox id="endMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getEndMinute()%>" />:<eftwc:HighlightTextBox id="endSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getEndSecond()%>" /></div>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <table width="100%">
                                        <tr>
                                          <td class="formFldLabel"><input type="checkbox" name="useSubmit" onclick="toggleUseSubmit()" <%if(OperationsEditBean.getUseSubmit().equals("on")){%>checked="checked"<%}%> />
                                            &nbsp;Attempt Submit if Return URL is Present
                                          </td>
                                        </tr>
                                        <tr>
                                          <td class="formFldLabel">
                                            <div id="divUseSubmit">
                                              User:&nbsp;<eftwc:HighlightTextBox id="submitUserID" size="40" text="<%=OperationsEditBean.getSubmitUserID()%>" />&nbsp;Password:&nbsp;<eftwc:HighlightTextBox id="submitPassword" size="40" type="password" text="<%=OperationsEditBean.getSubmitPassword()%>" />
                                            </div>
                                          </td>
                                        </tr>
                                      </table>
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
                        <div id="divAuthorize">
                          <table>
                            <tr>
                              <td class="formTitle">Authorization</td>
                            </tr>
                            <tr>
                              <td>
                                <table width="100%">
                                  <tr>
                                    <td class="formFldLabel">
                                      <input type="checkbox" name="useAuthorization" onclick="toggleUseAuthorization()" <%if(OperationsEditBean.getUseAuthorization().equals("on")){%>checked="checked"<%}%> />
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
                                                error = OperationsEditBean.getAuthorizationClassNameError();
                                                if (error != null && !error.equals("")) {
                                              %>
                                              <eftwc:HighlightTextBox id="authorizationClassName" size="60" text="<%=OperationsEditBean.getAuthorizationClassName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                              <%
                                                }
                                                else {
                                              %>
                                              <eftwc:HighlightTextBox id="authorizationClassName" size="60" text="<%=OperationsEditBean.getAuthorizationClassName()%>" />
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
                        </div>
                      </td>
                    </tr>
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
                                        ArrayList postList = OperationsEditBean.getPostProcesses();
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
                  </table>
                </td>
              </tr>
              </logic:equal>
              <logic:equal name="OperationsEditBean" property="type" value="<%=Phrase.SCHEDULED_TASK_OPERATION%>">
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
                                <tr>
                                  <td>
                                    <table width="100%">
                                      <tr>
                                        <td class="formFldLabel">Class Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = OperationsEditBean.getTaskClassNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="taskClassName" size="40" text="<%=OperationsEditBean.getTaskClassName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="taskClassName" size="40" text="<%=OperationsEditBean.getTaskClassName()%>" />
                                          <%
                                            }
                                          %>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
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
                                          ArrayList params = OperationsEditBean.getParameters();
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
                                        <td><input type="button" value="Add Parameter" onclick="submitForm('ADD_PARAMETERS')"/></td>
                                        <td><input type="button" value="Remove Selected Parameters" onclick="submitForm('REMOVE_PARAMETERS')"/></td>
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
                                        String scheduleType = OperationsEditBean.getScheduleType();
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
                                              error = OperationsEditBean.getStartDateError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:DatePicker id="startDate" text="<%=OperationsEditBean.getStartDate()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:DatePicker id="startDate" text="<%=OperationsEditBean.getStartDate()%>" />
                                            <%
                                              }
                                            %>
                                          </div></td>
                                          <td class="formFldLabel" nowrap="nowrap"><div id="divStartDate">
                                            <%
                                              error = OperationsEditBean.getTaskStartTimeError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="taskStartHour" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartHour()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />:<eftwc:HighlightTextBox id="taskStartMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartMinute()%>" />:<eftwc:HighlightTextBox id="taskStartSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartSecond()%>" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="taskStartHour" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartHour()%>" />:<eftwc:HighlightTextBox id="taskStartMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartMinute()%>" />:<eftwc:HighlightTextBox id="taskStartSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskStartSecond()%>" />
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
                                              error = OperationsEditBean.getEndDateError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:DatePicker id="endDate" text="<%=OperationsEditBean.getEndDate()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:DatePicker id="endDate" text="<%=OperationsEditBean.getEndDate()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                          <td class="formFldLabel" nowrap="nowrap">
                                            <%
                                              error = OperationsEditBean.getTaskEndTimeError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="taskEndHour" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndHour()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />:<eftwc:HighlightTextBox id="taskEndMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndMinute()%>" />:<eftwc:HighlightTextBox id="taskEndSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndSecond()%>" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="taskEndHour" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndHour()%>" />:<eftwc:HighlightTextBox id="taskEndMinute" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndMinute()%>" />:<eftwc:HighlightTextBox id="taskEndSecond" size="2" maxLength="2" text="<%=OperationsEditBean.getTaskEndSecond()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                        </tr>
                                      </table>
                                    </div>
                                  </td>
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
                                              error = OperationsEditBean.getIntervalError();
                                              if (error != null && !error.equals("")) {
                                            %>
                                            <eftwc:HighlightTextBox id="interval" text="<%=OperationsEditBean.getInterval()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="interval" text="<%=OperationsEditBean.getInterval()%>" />
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
                                          error = OperationsEditBean.getDayError();
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
                                            <input type="checkbox" name="sunday" value="1" <%if(OperationsEditBean.getSunday().equals("1")){%>checked="checked"<%}%> />Sunday
                                            <input type="checkbox" name="monday" value="2" <%if(OperationsEditBean.getMonday().equals("2")){%>checked="checked"<%}%> />Monday
                                            <input type="checkbox" name="tuesday" value="3" <%if(OperationsEditBean.getTuesday().equals("3")){%>checked="checked"<%}%> />Tuesday
                                            <input type="checkbox" name="wednesday" value="4" <%if(OperationsEditBean.getWednesday().equals("4")){%>checked="checked"<%}%> />Wednesday
                                            <input type="checkbox" name="thursday" value="5" <%if(OperationsEditBean.getThursday().equals("5")){%>checked="checked"<%}%> />Thursday
                                            <input type="checkbox" name="friday" value="6" <%if(OperationsEditBean.getFriday().equals("6")){%>checked="checked"<%}%> />Friday
                                            <input type="checkbox" name="saturday" value="7" <%if(OperationsEditBean.getSaturday().equals("7")){%>checked="checked"<%}%> />Saturday
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
                                                    error = OperationsEditBean.getDayOfMonthError();
                                                    if (error != null && !error.equals("")) {
                                                  %>
                                                  <eftwc:HighlightTextBox id="dayOfMonth" text="<%=OperationsEditBean.getDayOfMonth()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                                  <%
                                                    }
                                                    else {
                                                  %>
                                                  <eftwc:HighlightTextBox id="dayOfMonth" text="<%=OperationsEditBean.getDayOfMonth()%>" />
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
                                                    error = OperationsEditBean.getMonthOfYearError();
                                                    if (error != null && !error.equals("")) {
                                                  %>
                                                  <eftwc:HighlightTextBox id="monthOfYear" text="<%=OperationsEditBean.getMonthOfYear()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                                  <%
                                                    }
                                                    else {
                                                  %>
                                                  <eftwc:HighlightTextBox id="monthOfYear" text="<%=OperationsEditBean.getMonthOfYear()%>" />
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
                <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
                  <%
                    title = OperationsEditBean.getTitle();
                    if (title == null || !title.equals("New Operation")) {
                  %>
                <td><eftwc:AquaButton buttonText="Delete" onClickScript="submitForm('DELETE')" /></td>
                  <%
                    }
                  %>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
        <script language="javascript" type="text/javascript">
          <!-- Java Script -->
          if (document.form1.webService != null) {
          index = document.form1.webService.selectedIndex;
          text = document.form1.webService.options[index].text;
          if (text != "QUERY" && text != "SOLICIT") { // Submit, Download, Notify
            Utils.showDiv('divDefault');
            <%
              if (OperationsEditBean.getUseDefault().equals("on")) {
            %>
              Utils.hideDiv('divProcess');
            <%
              }
              else {
            %>
              Utils.showDiv('divProcess');
            <%
              }
            %>
            Utils.hideDiv('divSolicit');
          }
          if (text == "QUERY") { // Query
            Utils.hideDiv('divDefault');
            Utils.showDiv('divProcess');
            Utils.hideDiv('divSolicit');
          }
          if (text == "SOLICIT") { // Solicit
            Utils.hideDiv('divDefault');
            Utils.showDiv('divProcess');
            Utils.showDiv('divSolicit');
            toggleAnytime();
            toggleUseSubmit();
          }
          if (text == "AUTHENTICATE") { // Authenticate
            Utils.showDiv('divAuthorize');
            <%
              if (OperationsEditBean.getUseAuthorization().equals("on")) {
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
                    Utils.hideDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Once") {
                    Utils.showDiv('divStartDate');
                    Utils.hideDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Seconds") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Days") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.showDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Weeks") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.showDiv('divDayOfWeek');
                    Utils.hideDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Months") {
                    Utils.showDiv('divStartDate');
                    Utils.showDiv('divEndDate');
                    Utils.hideDiv('divInterval');
                    Utils.hideDiv('divDayOfWeek');
                    Utils.showDiv('divDayOfMonth');
                    Utils.hideDiv('divMonthOfYear');
                  }
                  if (text == "Years") {
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
