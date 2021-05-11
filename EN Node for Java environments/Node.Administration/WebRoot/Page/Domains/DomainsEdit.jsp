<%@ page import="Node.Phrase,java.util.ArrayList" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="DomainsEditBean" class="Node.Web.Administration.Bean.Domains.DomainsEditBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  document.form1.act.value = action;
                  document.form1.submit();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Domains/DomainsEdit.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Domains.do">Domain Management</a> - <%=DomainsEditBean.getTitle()%></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td></td>
                    </tr>
                    <%
                      String message = DomainsEditBean.getMessage();
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
                        <table>
                          <tr>
                            <td class="formFldLabel"><B>Domain Details</B>&nbsp;&nbsp;&nbsp;&nbsp;(<font color="red">*</font>&nbsp;Required Fields)</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td>
                                    <table>
                                      <tr>
                                        <td class="formFldLabel">Domain Name:&nbsp;<font color="red">*</font></td>
                                        <td>
                                          <%
                                            String name = DomainsEditBean.getName();
                                            String error = DomainsEditBean.getNameError();
                                            if (name != null && !name.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="name" text="<%=name%>" disabled="true" maxLength="50" />
                                          <%
                                            }
                                            else {
                                              if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="name" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="50" />
                                          <%
                                              }
                                              else {
                                          %>
                                          <eftwc:HighlightTextBox id="name" maxLength="50" />
                                          <%
                                              }
                                            }
                                          %>
                                        </td>
                                        <td class="formFldLabel">Domain Status:</td>
                                        <td>
                                          <select name="status">
                                            <%
                                              String status = DomainsEditBean.getStatus();
                                            %>
                                            <option <%if(status.equals("Running")){%>selected="selected"<%}%>>Running</option>
                                            <option <%if(status.equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                          </select>
                                        </td>
                                        <%
                                          if (DomainsEditBean.getIsNodeAdmin()) {
                                            String active = DomainsEditBean.getActiveStatus();
                                        %>
                                        <td class="formFldLabel">Domain Active Status:</td>
                                        <td>
                                          <select name="activeStatus">
                                            <option <%if(active.equals(Phrase.ACTIVE_STATUS)){%>selected="selected"<%}%>><%=Phrase.ACTIVE_STATUS%></option>
                                            <option <%if(active.equals(Phrase.INACTIVE_STATUS)){%>selected="selected"<%}%>><%=Phrase.INACTIVE_STATUS%></option>
                                          </select>
                                        </td>
                                        <%
                                          }
                                        %>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel">Allowed Web Services:&nbsp;<font color="red">*</font></td>
                                        <td colspan="5">
                                          <table>
                                            <tr>
                                              <%
                                                if (DomainsEditBean.getIsNodeAdmin()) {
                                              %>
                                              <td><input type="checkbox" name="allowSubmit" <%if(DomainsEditBean.getAllowSubmit().equals("on")){%>checked="checked"<%}%> /></td>
                                              <td class="formFldLabel">Submit</td>
                                              <td><input type="checkbox" name="allowDownload" <%if(DomainsEditBean.getAllowDownload().equals("on")){%>checked="checked"<%}%> /></td>
                                              <td class="formFldLabel">Download</td>
                                              <td><input type="checkbox" name="allowQuery" <%if(DomainsEditBean.getAllowQuery().equals("on")){%>checked="checked"<%}%> /></td>
                                              <td class="formFldLabel">Query</td>
                                              <td><input type="checkbox" name="allowSolicit" <%if(DomainsEditBean.getAllowSolicit().equals("on")){%>checked="checked"<%}%> /></td>
                                              <td class="formFldLabel">Solicit</td>
                                              <td><input type="checkbox" name="allowNotify" <%if(DomainsEditBean.getAllowNotify().equals("on")){%>checked="checked"<%}%> /></td>
                                              <td class="formFldLabel">Notify</td>
                                              <%
                                                }
                                                else {
                                              %>
                                              <td><input type="checkbox" name="allowSubmit" <%if(DomainsEditBean.getAllowSubmit().equals("on")){%>checked="checked"<%}%> disabled="disabled" /></td>
                                              <td class="formFldLabel">Submit</td>
                                              <td><input type="checkbox" name="allowDownload" <%if(DomainsEditBean.getAllowDownload().equals("on")){%>checked="checked"<%}%> disabled="disabled" /></td>
                                              <td class="formFldLabel">Download</td>
                                              <td><input type="checkbox" name="allowQuery" <%if(DomainsEditBean.getAllowQuery().equals("on")){%>checked="checked"<%}%> disabled="disabled" /></td>
                                              <td class="formFldLabel">Query</td>
                                              <td><input type="checkbox" name="allowSolicit" <%if(DomainsEditBean.getAllowSolicit().equals("on")){%>checked="checked"<%}%> disabled="disabled" /></td>
                                              <td class="formFldLabel">Solicit</td>
                                              <td><input type="checkbox" name="allowNotify" <%if(DomainsEditBean.getAllowNotify().equals("on")){%>checked="checked"<%}%> disabled="disabled" /></td>
                                              <td class="formFldLabel">Notify</td>
                                              <%
                                                }
                                              %>
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
                                        <td class="formFldLabel">Domain Status Message:<br />
                                          <eftwc:HighlightTextBox id="statusMsg" rows="5" size="100%" text="<%=DomainsEditBean.getStatusMsg()%>" />
                                        </td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel">Domain Description:<br />
                                          <eftwc:HighlightTextBox id="description" rows="2" size="100%" text="<%=DomainsEditBean.getDescription()%>" />
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
                            <td class="formTitle">Domain Admins/Privileges</td>
                          </tr>
                          <tr>
                            <td>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Assigned</td>
                                  <td class="gridHead1" nowrap="nowrap">Admin</td>
                                </tr>
                                <%
                                  ArrayList list = DomainsEditBean.getAdmins();
                                  if (list != null) {
                                    for (int i = 0; i < list.size(); i++) {
                                      ArrayList temp = (ArrayList)list.get(i);
                                      String adminID = (String)temp.get(1);
                                %>
                          	<tr id="<%=adminID%>" class="trOut" onmouseover="Utils.setIDClass('<%=adminID%>','trOver')" onmouseout="Utils.setIDClass('<%=adminID%>','trOut')">
                                  <td class="RowPlain"><input type="checkbox" name="<%="admin"+adminID%>" <%if(((String)temp.get(0)).equalsIgnoreCase("true")){%>checked="checked"<%}%> /></td>
                                  <td class="RowPlain"><%=(String)temp.get(2)%></td>
                                </tr>
                                <%
                                    }
                                  }
                                %>
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
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
</html>
