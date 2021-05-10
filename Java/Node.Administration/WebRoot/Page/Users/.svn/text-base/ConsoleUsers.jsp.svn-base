<%@ page import="java.util.ArrayList" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="ConsoleUsersBean" class="Node.Web.Administration.Bean.Users.ConsoleUsersBean" scope="session" />

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
					if (action != "SAVE" && action != "NEW_PASSWORD" && action != "DELETE") {
							Utils.gotoURL('self', action);
					} else {
							if (action == "DELETE") {
								var r=confirm("Do you really want to delete this user?");
								if (r!=true){
									return;
								}
							}
							document.form1.act.value = action;
							document.form1.submit();
					}
					waitingBlock.switchFloatDiv();
				}
</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Users/ConsoleUsers.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a onclick="submitForm('/Node.Administration/Page/Users/Users.do')" href="#">User Management</a> - <%=ConsoleUsersBean.getTitle()%></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table>
                    <tr>
                      <td></td>
                      <%
                      	String title = ConsoleUsersBean.getTitle();
                        if (!title.equalsIgnoreCase("New Administration Console User")) {
                      %>
                      <td></td>
                      <%
                        }
                      %>
                    </tr>
                      <%
                        String message = ConsoleUsersBean.getMessage();
                        if (message != null && !message.equals("")) {
                      %>
                    <tr>
                      <td colspan="2" class="formFldLabel errFont"><%=message%></td>
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
                    <%
                      if (ConsoleUsersBean.isEditable()) {
                    %>
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Administration Console User Information</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                        <td class="formFldLabel" nowrap="nowrap">User ID:&nbsp;<font color="red">*</font></td>
                                          <%
                                            String userID = ConsoleUsersBean.getUserID();
                                            String error = ConsoleUsersBean.getUserIDError();
                                            if (!ConsoleUsersBean.getTitle().equals("New Administration Console User")) {
                                          %>
                                        <td><eftwc:HighlightTextBox id="userID" size="30" text="<%=userID%>" disabled="true" /></td>
                                          <%
                                            }
                                            else {
                                              if (error != null && !error.equals("")) {
                                          %>
                                        <td class="formFldLabel"><eftwc:HighlightTextBox id="userID" size="30" text="<%=userID%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="50" /></td>
                                          <%
                                              }
                                              else {
                                          %>
                                        <td><eftwc:HighlightTextBox id="userID" size="30" text="<%=userID%>" maxLength="50" /></td>
                                          <%
                                              }
                                            }
                                          %>
                                        <td class="formFldLabel" nowrap="nowrap">Status&nbsp;<font color="red">*</font></td>
                                        <td>
                                          <select name="status">
                                            <%
                                              String status = ConsoleUsersBean.getStatus();
                                            %>
                                            <option <%if(status.equals("A")){%>selected="selected"<%}%> value="A">Active</option>
                                            <option <%if(status.equals("I")){%>selected="selected"<%}%> value="I">Inactive</option>
                                          </select>
                                        </td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel" nowrap="nowrap">First Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = ConsoleUsersBean.getFirstNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="firstName" size="30" text="<%=ConsoleUsersBean.getFirstName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="60" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="firstName" size="30" text="<%=ConsoleUsersBean.getFirstName()%>" maxLength="60" />
                                          <%
                                            }
                                          %>
                                        </td>
                                        <td class="formFldLabel" nowrap="nowrap">Middle Initial:</td>
                                        <td><eftwc:HighlightTextBox id="midInitial" size="1" text="<%=ConsoleUsersBean.getMidInitial()%>" maxLength="1" /></td>
                                        <td class="formFldLabel" nowrap="nowrap">Last Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel" colspan="3">
                                          <%
                                            error = ConsoleUsersBean.getLastNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="lastName" text="<%=ConsoleUsersBean.getLastName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="60" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="lastName" text="<%=ConsoleUsersBean.getLastName()%>" maxLength="60" />
                                          <%
                                            }
                                          %>
                                        </td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel">Email:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = ConsoleUsersBean.getEmailError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="email" size="30" text="<%=ConsoleUsersBean.getEmail()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="100" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="email" size="30" text="<%=ConsoleUsersBean.getEmail()%>" maxLength="100" />
                                          <%
                                            }
                                          %>
                                        </td>
                                        <td class="formFldLabel">Phone:</td>
                                        <td><eftwc:HighlightTextBox id="phone" size="12" text="<%=ConsoleUsersBean.getPhone()%>" maxLength="15" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Address:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="address" size="38" text="<%=ConsoleUsersBean.getAddress()%>" maxLength="100" /></td>
                                  <td class="formFldLabel">Supplemental Address:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="suppAddress" size="38" text="<%=ConsoleUsersBean.getSuppAddress()%>" maxLength="100" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">City:</td>
                                  <td><eftwc:HighlightTextBox id="city" size="30" maxLength="100" text="<%=ConsoleUsersBean.getCity()%>" /></td>
                                  <td class="formFldLabel">State:</td>
                                  <td><eftwc:HighlightTextBox id="state" size="2" maxLength="2" text="<%=ConsoleUsersBean.getState()%>" /></td>
                                  <td class="formFldLabel">Zip Code:</td>
                                  <td><eftwc:HighlightTextBox id="zipCode" size="10" maxLength="15" text="<%=ConsoleUsersBean.getZipCode()%>" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Country:</td>
                                  <td><eftwc:HighlightTextBox id="country" size="30" maxLength="25" text="<%=ConsoleUsersBean.getCountry()%>" /></td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel" colspan="8">Comments:<br />
                                          <eftwc:HighlightTextBox id="comments" size="100%" rows="4" text="<%=ConsoleUsersBean.getComments()%>" /></td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <%
                      }
                    %>
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Console User Privileges</td>
                          </tr>
                          <tr>
                            <td>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Assigned</td>
                                  <td class="gridHead1" nowrap="nowrap">Console User</td>
                                </tr>
                                <%
                                  ArrayList list = ConsoleUsersBean.getDomains();
                                  if (list != null) {
                                    for (int i = 0; i < list.size(); i++) {
                                      ArrayList temp = (ArrayList)list.get(i);
                                      String domID = (String)temp.get(1);
                                %>
                          	<tr id="<%=domID%>" class="trOut" onmouseover="Utils.setIDClass('<%=domID%>','trOver')" onmouseout="Utils.setIDClass('<%=domID%>','trOut')">
                                  <td class="RowPlain"><input type="checkbox" name="<%="domain"+domID%>" <%if(((String)temp.get(0)).equalsIgnoreCase("true")){%>checked="checked"<%}%> /></td>
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
                  <table width="20%">
                    <tr>
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
                      <%
                      	title = ConsoleUsersBean.getTitle();
                        if (!title.equalsIgnoreCase("New Administration Console User")) {
                      %>
                      <td><eftwc:AquaButton buttonText="New Password" onClickScript="submitForm('NEW_PASSWORD')" /></td>
                      <%
                        }
                      %>
                      <td><eftwc:AquaButton buttonText="Back" onClickScript="submitForm('Users.jsp')" /></td>
                      <td><eftwc:AquaButton buttonText="Remove User" buttonStyle="aquaRed"  onClickScript="submitForm('DELETE')" /></td>
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
  </html>
