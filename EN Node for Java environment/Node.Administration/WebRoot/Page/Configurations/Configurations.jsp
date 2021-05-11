<%@ page import="com.enfotech.component.UI.webControl.beans.MessageLabelBean, com.enfotech.component.UI.webControl.beans.TabControlBean" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="ConfigurationsBean" class="Node.Web.Administration.Bean.Configurations.ConfigurationsBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node</title>
               <link href="../../eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="../../css/core.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="../../eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  if (action == "REMOVE_CLIENT_URL" && confirm("This will delete these entries from the database.  Do you want to continue to remove these Client URL's?")) {
                    document.form1.act.value = action;
                    document.form1.submit();
                  }
                  else if ((action == "REMOVE_USER_CC" || action == "REMOVE_USER_BCC" ||
                            action == "REMOVE_TASK_CC" || action == "REMOVE_TASK_BCC")
                            && confirm("This will delete these entries from the database.  Do you want to continue to remove these Email Addresses?")) {
                    document.form1.act.value = action;
                    document.form1.submit();
                  }
                  else if (action != "REMOVE_CLIENT_URL" && action != "REMOVE_USER_CC" && action != "REMOVE_USER_BCC" &&
                            action != "REMOVE_TASK_CC" && action != "REMOVE_TASK_BCC") {
                    document.form1.act.value = action;
                    document.form1.submit();
                  }
                }
                function toggleProxy ()
                {
                  if (document.form1.hasProxy.checked)
                    Utils.showDiv('ProxyDiv');
                  else
                    Utils.hideDiv('ProxyDiv');
                }
               </script>
	</head>
      	<body >
          <form action="../../Page/Configurations/Configurations.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <input type="hidden" name="selectedIndex" />
            <div id="divConfig">
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td></td>
                    </tr>
                      <%
                        String message = ConfigurationsBean.getMessage();
                        if (message != null && !message.equals("")) {
                      %>
                    <tr>
                      <td class="formFldLabel errFont"><%=message%></td>
                    </tr>
                      <%
                        }
                      %>
                  </table>
                  <table width="100%">
                    <tr>
                      <td class="formFldLabel"><font color="red">*</font>&nbsp;Required Fields</td>
                    </tr>
                  </table>
                  <table width="100%">
                    <tr valign="top">
                      <td>
                        <table>
                          <tr>
                            <td class="formTitle" width="50%" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">General Node Settings</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td>
                                    <table width="100%">
                                <tr>
                                  <td class="formFldLabel" nowrap="nowrap">
                                    Node Status:&nbsp;<font color="red">*</font>
                                  </td>
                                  <td class="formFldLabel">
                                    <%
                                      String error = ConfigurationsBean.getNodeRunningError();
                                      if (!error.equals("")) {
                                    %>
                                    <eftwc:MessageLabel msgText="<%=error%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" ></eftwc:MessageLabel>
                                    <%
                                      }
                                    %>
                                    <logic:equal name="ConfigurationsBean" property="nodeRunning" value="Running">
                                    <input type="radio" name="nodeRunning" value="Running" checked="checked"/> Running
                                    </logic:equal>
                                    <logic:notEqual name="ConfigurationsBean" property="nodeRunning" value="Running">
                                    <input type="radio" name="nodeRunning" value="Running"/> Running
                                    </logic:notEqual>
                                    <logic:equal name="ConfigurationsBean" property="nodeRunning" value="Stopped">
                                    <input type="radio" name="nodeRunning" value="Stopped" checked="checked"/> Stopped
                                    </logic:equal>
                                    <logic:notEqual name="ConfigurationsBean" property="nodeRunning" value="Stopped">
                                    <input type="radio" name="nodeRunning" value="Stopped"/> Stopped
                                    </logic:notEqual>
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">Token Life Time:
                                    <eftwc:HighlightTextBox id="tokenLifeTime" text="<%=ConfigurationsBean.getTokenLifeTime()%>" size="5" maxLength="5"/>&nbsp;seconds
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">Node Name:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel">
                                    <%
                                      error = ConfigurationsBean.getNodeNameError();
                                      if (!error.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeName" size="5" text="<%=ConfigurationsBean.getNodeName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                    <%
                                      }
                                      else {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeName" size="5" text="<%=ConfigurationsBean.getNodeName()%>" />
                                    <%
                                      }
                                    %>
                                  </td>
                                </tr>
                                    </table>
                                  </td>
                                </tr>
                                <tr align="left">
                                  <td colspan="3">
                                    <table width="100%">
                                <tr>
                                  <td class="formFldLabel" align="left">Node Message:</td>
                                  <td><eftwc:HighlightTextBox id="nodeMessage" size="94" text="<%=ConfigurationsBean.getNodeMessage()%>" /></td>
                                </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">NAAS Server Address:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = ConfigurationsBean.getNaasAddressError();
                                            if (!error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="naasAddress" size="94" text="<%=ConfigurationsBean.getNaasAddress()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="naasAddress" size="94" text="<%=ConfigurationsBean.getNaasAddress()%>" />
                                          <%
                                            }
                                          %>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Node Address (Version 1):&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = ConfigurationsBean.getNodeAddressError();
                                            if (!error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="nodeAddress" size="94" text="<%=ConfigurationsBean.getNodeAddress()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="nodeAddress" size="94" text="<%=ConfigurationsBean.getNodeAddress()%>" />
                                          <%
                                            }
                                          %>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Node Address (Version 2):&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = ConfigurationsBean.getNodeAddressErrorV2();
                                            if (!error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="nodeAddressV2" size="94" text="<%=ConfigurationsBean.getNodeAddressV2()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="nodeAddressV2" size="94" text="<%=ConfigurationsBean.getNodeAddressV2()%>" />
                                          <%
                                            }
                                          %>
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
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle" width="50%" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Server Settings</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td>
                                    <logic:equal name="ConfigurationsBean" property="hasProxy" value="on">
                                      <input type="checkbox" name="hasProxy" onclick="toggleProxy()" checked="checked" />
                                    </logic:equal>
                                    <logic:notEqual name="ConfigurationsBean" property="hasProxy" value="on">
                                      <input type="checkbox" name="hasProxy" onclick="toggleProxy()" />
                                    </logic:notEqual>
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">Use Proxy Server</td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                  <td>
                                    <div id="ProxyDiv">
                                      <table width="100%">
                                        <tr>
                                          <td class="formFldLabel">Proxy Server Address:&nbsp;<font color="red">*</font></td>
                                          <td class="formFldLabel">
                                            <%
                                              error = ConfigurationsBean.getProxyServerError();
                                              if (!error.equals("")){
                                            %>
                                            <eftwc:HighlightTextBox id="proxyServer" size="50" text="<%=ConfigurationsBean.getProxyServer()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                            <%
                                              }
                                              else {
                                            %>
                                            <eftwc:HighlightTextBox id="proxyServer" size="50" text="<%=ConfigurationsBean.getProxyServer()%>" />
                                            <%
                                              }
                                            %>
                                          </td>
                                          <td class="formFldLabel">Proxy Port:</td>
                                          <td><eftwc:HighlightTextBox id="proxyPort" size="5" text="<%=ConfigurationsBean.getProxyPort()%>" /></td>
                                        </tr>
                                        <tr>
                                          <td class="formFldLabel">Proxy User ID:</td>
                                          <td><eftwc:HighlightTextBox id="proxyUser" text="<%=ConfigurationsBean.getProxyUser()%>" /></td>
                                          <td class="formFldLabel">Proxy Password:</td>
                                          <td><eftwc:HighlightTextBox id="proxyPassword" text="<%=ConfigurationsBean.getProxyPassword()%>" type="password" /></td>
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
                    <tr>
                      <td>
                      	<table width="100%">
                          <tr>
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">NAAS Administrator Account</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel" nowrap="nowrap">Name:
                                    <eftwc:HighlightTextBox id="nodeAdminName" text="<%=ConfigurationsBean.getNodeAdminName()%>" />
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">User ID:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel">
                                    <%
                                      error = ConfigurationsBean.getNodeAdminUIDError();
                                      if (!error.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeAdminUID" text="<%=ConfigurationsBean.getNodeAdminUID()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                    <%
                                      }
                                      else {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeAdminUID" text="<%=ConfigurationsBean.getNodeAdminUID()%>" />
                                    <%
                                      }
                                    %>
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">Password:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel">
                                    <%
                                      error = ConfigurationsBean.getNodeAdminPWDError();
                                      if (!error.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeAdminPWD" text="<%=ConfigurationsBean.getNodeAdminPWD()%>" type="password" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                    <%
                                      }
                                      else {
                                    %>
                                    <eftwc:HighlightTextBox id="nodeAdminPWD" text="<%=ConfigurationsBean.getNodeAdminPWD()%>" type="password" />
                                    <%
                                      }
                                    %>
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
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Application Logging Levels</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Node.Administration:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="nodeAdminLog">
                                      <logic:equal name="ConfigurationsBean" property="nodeAdminLog" value="DEBUG">
                                      <option selected="selected">DEBUG</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeAdminLog" value="DEBUG">
                                      <option>DEBUG</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeAdminLog" value="INFO">
                                      <option selected="selected">INFO</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeAdminLog" value="INFO">
                                      <option>INFO</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeAdminLog" value="WARN">
                                      <option selected="selected">WARN</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeAdminLog" value="WARN">
                                      <option>WARN</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeAdminLog" value="ERROR">
                                      <option selected="selected">ERROR</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeAdminLog" value="ERROR">
                                      <option>ERROR</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeAdminLog" value="FATAL">
                                      <option selected="selected">FATAL</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeAdminLog" value="FATAL">
                                      <option>FATAL</option>
                                      </logic:notEqual>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Node.Client:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="nodeClientLog">
                                      <logic:equal name="ConfigurationsBean" property="nodeClientLog" value="DEBUG">
                                      <option selected="selected">DEBUG</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeClientLog" value="DEBUG">
                                      <option>DEBUG</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeClientLog" value="INFO">
                                      <option selected="selected">INFO</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeClientLog" value="INFO">
                                      <option>INFO</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeClientLog" value="WARN">
                                      <option selected="selected">WARN</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeClientLog" value="WARN">
                                      <option>WARN</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeClientLog" value="ERROR">
                                      <option selected="selected">ERROR</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeClientLog" value="ERROR">
                                      <option>ERROR</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeClientLog" value="FATAL">
                                      <option selected="selected">FATAL</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeClientLog" value="FATAL">
                                      <option>FATAL</option>
                                      </logic:notEqual>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Node.Task:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="nodeTaskLog">
                                      <logic:equal name="ConfigurationsBean" property="nodeTaskLog" value="DEBUG">
                                      <option selected="selected">DEBUG</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeTaskLog" value="DEBUG">
                                      <option>DEBUG</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeTaskLog" value="INFO">
                                      <option selected="selected">INFO</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeTaskLog" value="INFO">
                                      <option>INFO</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeTaskLog" value="WARN">
                                      <option selected="selected">WARN</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeTaskLog" value="WARN">
                                      <option>WARN</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeTaskLog" value="ERROR">
                                      <option selected="selected">ERROR</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeTaskLog" value="ERROR">
                                      <option>ERROR</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeTaskLog" value="FATAL">
                                      <option selected="selected">FATAL</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeTaskLog" value="FATAL">
                                      <option>FATAL</option>
                                      </logic:notEqual>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Node.WebServices:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <select name="nodeWebServicesLog">
                                      <logic:equal name="ConfigurationsBean" property="nodeWebServicesLog" value="DEBUG">
                                      <option selected="selected">DEBUG</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeWebServicesLog" value="DEBUG">
                                      <option>DEBUG</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeWebServicesLog" value="INFO">
                                      <option selected="selected">INFO</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeWebServicesLog" value="INFO">
                                      <option>INFO</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeWebServicesLog" value="WARN">
                                      <option selected="selected">WARN</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeWebServicesLog" value="WARN">
                                      <option>WARN</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeWebServicesLog" value="ERROR">
                                      <option selected="selected">ERROR</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeWebServicesLog" value="ERROR">
                                      <option>ERROR</option>
                                      </logic:notEqual>
                                      <logic:equal name="ConfigurationsBean" property="nodeWebServicesLog" value="FATAL">
                                      <option selected="selected">FATAL</option>
                                      </logic:equal>
                                      <logic:notEqual name="ConfigurationsBean" property="nodeWebServicesLog" value="FATAL">
                                      <option>FATAL</option>
                                      </logic:notEqual>
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
                  <br>
                  <table width="40%">
                    <tr>
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE_CONFIG')" /></td>
                      <td><eftwc:AquaButton buttonText="Upload" onClickScript="submitForm('UPLOAD')" /></td>
                      <td><eftwc:AquaButton buttonText="Download" onClickScript="submitForm('DOWNLOAD')" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            </div>
            <div id="divEmail">
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td></td>
                    </tr>
                      <%
                        message = ConfigurationsBean.getMessage();
                        if (!message.equals("")) {
                      %>
                    <tr>
                      <td class="formFldLabel errFont"><%=message%></td>
                    </tr>
                      <%
                        }
                      %>
                  </table>
                  <table width="100%">
                    <tr>
                      <td class="formFldLabel"><font color="red">*</font>&nbsp;Required Fields</td>
                    </tr>
                  </table>
                  <table width="100%">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Email Server (IP Address or Fully Qualified Domain Name)</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Host:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel" colspan="2">
                                    <%
                                      error = ConfigurationsBean.getHostError();
                                      if (!error.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="host" size="80" text="<%=ConfigurationsBean.getHost()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                    <%
                                      }
                                      else {
                                    %>
                                    <eftwc:HighlightTextBox id="host" size="80" text="<%=ConfigurationsBean.getHost()%>" />
                                    <%
                                      }
                                    %>
                                  </td>
                                  <td class="formFldLabel">Port:&nbsp;
                                    <eftwc:HighlightTextBox id="port" size="5" text="<%=ConfigurationsBean.getPort()%>" />
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
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">User Management Email Settings</td>
                          </tr>
                          <tr>
                            <td>
                            <table width="100%">
                              <tr>
                                <td class="formFldLabel">Sender Name:&nbsp;<font color="red">*</font></td>
                                <td class="formFldLabel">
                                  <%
                                    error = ConfigurationsBean.getUserSenderNameError();
                                    if (!error.equals("")) {
                                  %>
                                  <eftwc:HighlightTextBox id="userSenderName" size="35" text="<%=ConfigurationsBean.getUserSenderName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                  <%
                                    }
                                    else {
                                  %>
                                  <eftwc:HighlightTextBox id="userSenderName" size="35" text="<%=ConfigurationsBean.getUserSenderName()%>" />
                                  <%
                                    }
                                  %>
                                </td>
                                <td class="formFldLabel" nowrap="nowrap">Sender Email Address:&nbsp;<font color="red">*</font></td>
                                <td class="formFldLabel">
                                  <%
                                    error = ConfigurationsBean.getUserEmailAddressError();
                                    if (!error.equals("")) {
                                  %>
                                  <eftwc:HighlightTextBox id="userEmailAddress" size="35" text="<%=ConfigurationsBean.getUserEmailAddress()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                  <%
                                    }
                                    else {
                                  %>
                                  <eftwc:HighlightTextBox id="userEmailAddress" size="35" text="<%=ConfigurationsBean.getUserEmailAddress()%>" />
                                  <%
                                    }
                                  %>
                                </td>
                              </tr>
                              <tr>
                                <td class="formFldLabel">Sender User ID:</td>
                                <td><eftwc:HighlightTextBox id="userUID" size="35" text="<%=ConfigurationsBean.getUserUID()%>" /></td>
                                <td class="formFldLabel">Sender Password:</td>
                                <td><eftwc:HighlightTextBox id="userPWD" type="password" size="35" text="<%=ConfigurationsBean.getUserPWD()%>" /></td>
                              </tr>
                              <tr>
                                <td class="formFldLabel" colspan="4">CC List</td>
                              </tr>
                              <%
                                String[] userCC = ConfigurationsBean.getUserCC();
                                String name = "";
                                String value = "";
                                if (userCC != null && userCC.length > 0) {
                                  for (int i = 0; i < userCC.length; i++) {
                                    name = "userCCList" + i;
                                    value = userCC[i];
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="userCCSelect"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                              </tr>
                              <%
                                  }
                                }
                                name = "userCCList" + userCC.length;
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="userCCSelect"+userCC.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                              </tr>
                              <tr>
                                <td colspan="4"><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE_USER_CC')" /></td>
                              </tr>
                              <tr>
                                <td class="formFldLabel" colspan="4">BCC List</td>
                              </tr>
                              <%
                                String[] userBCC = ConfigurationsBean.getUserBCC();
                                if (userCC != null && userBCC.length > 0) {
                                  for (int i = 0; i < userBCC.length; i++) {
                                    name = "userBCCList" + i;
                                    value = userBCC[i];
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="userBCCSelect"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                              </tr>
                              <%
                                  }
                                }
                                name = "userBCCList" + userBCC.length;
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="userBCCSelect"+userBCC.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                              </tr>
                              <tr>
                                <td colspan="4"><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE_USER_BCC')" /></td>
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
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Scheduled Task Email Settings</td>
                          </tr>
                          <tr>
                            <td>
                            <table width="100%">
                              <tr>
                                <td class="formFldLabel">Sender Name:&nbsp;<font color="red">*</font></td>
                                <td class="formFldLabel">
                                  <%
                                    error = ConfigurationsBean.getTaskSenderNameError();
                                    if (!error.equals("")) {
                                  %>
                                  <eftwc:HighlightTextBox id="taskSenderName" size="35" text="<%=ConfigurationsBean.getTaskSenderName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                  <%
                                    }
                                    else {
                                  %>
                                  <eftwc:HighlightTextBox id="taskSenderName" size="35" text="<%=ConfigurationsBean.getTaskSenderName()%>" />
                                  <%
                                    }
                                  %>
                                </td>
                                <td class="formFldLabel" nowrap="nowrap">Sender Email Address:&nbsp;<font color="red">*</font></td>
                                <td class="formFldLabel">
                                  <%
                                    error = ConfigurationsBean.getTaskEmailAddressError();
                                    if (!error.equals("")) {
                                  %>
                                  <eftwc:HighlightTextBox id="taskEmailAddress" size="35" text="<%=ConfigurationsBean.getTaskEmailAddress()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" />
                                  <%
                                    }
                                    else {
                                  %>
                                  <eftwc:HighlightTextBox id="taskEmailAddress" size="35" text="<%=ConfigurationsBean.getTaskEmailAddress()%>" />
                                  <%
                                    }
                                  %>
                                </td>
                              </tr>
                              <tr>
                                <td class="formFldLabel">Sender User ID:</td>
                                <td><eftwc:HighlightTextBox id="taskUID" size="35" text="<%=ConfigurationsBean.getTaskUID()%>" /></td>
                                <td class="formFldLabel">Sender Password:</td>
                                <td><eftwc:HighlightTextBox id="taskPWD" type="password" size="35" text="<%=ConfigurationsBean.getTaskPWD()%>" /></td>
                              </tr>
                              <tr>
                                <td class="formFldLabel" colspan="4">CC List</td>
                              </tr>
                              <%
                                String[] taskCC = ConfigurationsBean.getTaskCC();
                                if (taskCC != null && taskCC.length > 0) {
                                  for (int i = 0; i < taskCC.length; i++) {
                                    name = "taskCCList" + i;
                                    value = taskCC[i];
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="taskCCSelect"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                              </tr>
                              <%
                                  }
                                }
                                name = "taskCCList" + taskCC.length;
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="taskCCSelect"+taskCC.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                              </tr>
                              <tr>
                                <td colspan="4"><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE_TASK_CC')" /></td>
                              </tr>
                              <tr>
                                <td class="formFldLabel" colspan="4">BCC List</td>
                              </tr>
                              <%
                                String[] taskBCC = ConfigurationsBean.getTaskBCC();
                                if (taskBCC != null && taskBCC.length > 0) {
                                  for (int i = 0; i < taskBCC.length; i++) {
                                    name = "taskBCCList" + i;
                                    value = taskBCC[i];
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="taskBCCSelect"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                              </tr>
                              <%
                                  }
                                }
                                name = "taskBCCList" + taskBCC.length;
                              %>
                              <tr>
                                <td colspan="4"><input type="checkbox" name="<%="taskBCCSelect"+taskBCC.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                              </tr>
                              <tr>
                                <td colspan="4"><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE_TASK_BCC')" /></td>
                              </tr>
                            </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                  <br>
                  <table width="100%">
                    <tr>
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE_EMAIL')" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
            </div>
            <div id="divClient">
            <table width="100%">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td></td>
                    </tr>
                    <%
                      message = ConfigurationsBean.getMessage();
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
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Client Settings</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">List of Pre-Defined Node URLs (Version 1.1)</td>
                                </tr>
                                <%
                                  String[] clientURLs = ConfigurationsBean.getClientURLs();
                                  if (clientURLs != null && clientURLs.length > 0) {
                                    for (int i = 0; i < clientURLs.length; i++) {
                                      name = "clientURLV1_" + i;
                                      value = clientURLs[i];
                                %>
                                <tr>
                                  <td><input type="checkbox" name="<%="removeClientURLV1_"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                                </tr>
                                <%
                                    }
                                  }
                                  name = "clientURLV1_" + clientURLs.length;
                                %>
                                <tr>
                                  <td><input type="checkbox" name="<%="removeClientURLV2_"+clientURLs.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">List of Pre-Defined Node URLs (Version 2.0)</td>
                                </tr>
                                <%
                                  String[] clientURLs2 = ConfigurationsBean.getClientURLs2();
                                  if (clientURLs2 != null && clientURLs2.length > 0) {
                                    for (int i = 0; i < clientURLs2.length; i++) {
                                      name = "clientURLV2_" + i;
                                      value = clientURLs2[i];
                                %>
                                <tr>
                                  <td><input type="checkbox" name="<%="removeClientURLV2_"+i%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" text="<%=value%>" /></td>
                                </tr>
                                <%
                                    }
                                  }
                                  name = "clientURLV2_" + clientURLs2.length;
                                %>
                                <tr>
                                  <td><input type="checkbox" name="<%="removeClientURLV2_"+clientURLs2.length%>" value="on"/><eftwc:HighlightTextBox id="<%=name%>" size="80" /></td>
                                </tr>
                            	<tr>
                                  <td>
                                    <table width="30%">
                                      <tr>
						                <td><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE_CLIENT_URL')" /></td>
						                <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE_CLIENT')" /></td>
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
            </table>
            </div>
          </form>
       	<script language="javascript">
          if (document.form1.hasProxy.checked)
            Utils.showDiv('ProxyDiv');
          else
            Utils.hideDiv('ProxyDiv');

          // Tabs
          <%
            if (ConfigurationsBean.getSelectedIndex() == 2) {
          %>
            Utils.hideDiv('divConfig');
            Utils.hideDiv('divEmail');
            Utils.showDiv('divClient');
          <%
            }
            else if (ConfigurationsBean.getSelectedIndex() == 1) {
          %>
            Utils.hideDiv('divConfig');
            Utils.showDiv('divEmail');
            Utils.hideDiv('divClient');
          <%
            }
            else {
          %>
            Utils.showDiv('divConfig');
            Utils.hideDiv('divEmail');
            Utils.hideDiv('divClient');
          <%
            }
          %>
      	</script>
      	</body>
</html>
