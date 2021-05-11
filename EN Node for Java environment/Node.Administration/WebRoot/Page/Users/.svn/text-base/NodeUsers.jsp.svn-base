<%@ page import="Node.Phrase,java.util.ArrayList" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="NodeUsersBean" class="Node.Web.Administration.Bean.Users.NodeUsersBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">

		function submitForm(action) {	
			if (action != "SAVE" && action != "NEW_PASSWORD" && action != "DELETE")
				Utils.gotoURL('self', action);
			else {
				if (action == "DELETE") {
					var r = confirm("Do you really want to delete this user?");
					if (r != true) {
						return;
					}
				}
				document.form1.act.value = action;
				document.form1.submit();
				waitingBlock.switchFloatDiv();
			}
		}
		function changeNodeUserType() {
			if (document.form1.userType[0].checked) {
				Utils.hideDiv('divStatus');
				Utils.hideDiv('divFields');
			} else {
				Utils.showDiv('divStatus');
				Utils.showDiv('divFields');
			}
		}
		function assignOps() {
			if (document.form1.opAvailSel.options != null) {
				for ( var i = 0; i < document.form1.opAvailSel.length; i++) {
					if (document.form1.opAvailSel.options[i].selected) {
						l = document.form1.opAssSel.length;
						document.form1.opAssSel.options[l] = new Option(
								document.form1.opAvailSel.options[i].text,
								document.form1.opAvailSel.options[i].value, false,
								true);
						document.form1.opAvailSel.options[i] = null;
					}
				}
			}
		}
		function unAssignOps() {
			if (document.form1.opAssSel.options != null) {
				for ( var i = 0; i < document.form1.opAssSel.length; i++) {
					if (document.form1.opAssSel.options[i].selected) {
						l = document.form1.opAvailSel.length;
						document.form1.opAvailSel.options[l] = new Option(
								document.form1.opAssSel.options[i].text,
								document.form1.opAssSel.options[i].value, false,
								false);
						document.form1.opAssSel.options[i] = null;
					} else
						document.form1.opAssSel.options[i].selected = true;
				}
			}
		}
</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Users/NodeUsers.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a onclick="submitForm('/Node.Administration/Page/Users/Users.do')" href="#">User Management</a> - <%=NodeUsersBean.getTitle()%></td>
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
                        String title = NodeUsersBean.getTitle();
                        if (!title.equals("New Node User")) {
                      %>
                      <td></td>
                      <%
                        }
                      %>
                    </tr>
                    <%
                      String message = NodeUsersBean.getMessage();
                      if (message != null && !message.equals("")) {
                    %>
                    <tr>
                      <td class="formFldLabel errFont" colspan="2"><%=message%></td>
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
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Node User Information</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="50%">
                                <tr>
                                        <td class="formFldLabel">User ID (Email):&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            String error = NodeUsersBean.getUserIDError();
                                            if (!NodeUsersBean.getTitle().equals("New Node User")) {
                                          %>
                                          <eftwc:HighlightTextBox id="userID" size="30" text="<%=NodeUsersBean.getUserID()%>" disabled="true"  maxLength="50"/>
                                          <%
                                            }
                                            else {
                                              if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="userID" size="30" text="<%=NodeUsersBean.getUserID()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline"  maxLength="50"/>
                                          <%
                                              }
                                              else {
                                          %>
                                          <eftwc:HighlightTextBox id="userID" size="30" text="<%=NodeUsersBean.getUserID()%>" maxLength="50"/>
                                          <%
                                              }
                                            }
                                          %>
                                        </td>
                                  <td>
                                    <table>
                                      <tr>
                                        <td class="formFldLabel">User Type:&nbsp;<font color="red">*</font></td>
                                      </tr>
                                      <tr>
                                        <td>
                                          <table>
                                              <%
                                                String userType = NodeUsersBean.getUserType();
                                                error = NodeUsersBean.getUserTypeError();
                                                if (error != null && !error.equals("")) {
                                              %>
                                            <tr>
                                              <td class="formFldLabel" colspan="4"><eftwc:MessageLabel msgText="<%=error%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" /></td>
                                            </tr>
                                              <%
                                                }
                                              %>
                                            <tr>
                                              <%
                                                boolean isDisabled = false;
                                                if (!NodeUsersBean.getTitle().equals("New Node User"))
                                                  isDisabled = true;
                                              %>
                                              <td><input type="radio" name="userType" value="<%=Phrase.NAASNodeUser%>" <%if(userType.equals(Phrase.NAASNodeUser)){%>checked="checked"<%}%> <%if(isDisabled){%>disabled="disabled"<%}%> onclick="changeNodeUserType()" /></td>
                                              <td class="formFldLabel" nowrap="nowrap">NAAS User</td>
                                              <td><input type="radio" name="userType" value="<%=Phrase.LocalNodeUser%>" <%if(userType.equals(Phrase.LocalNodeUser)){%>checked="checked"<%}%> <%if(isDisabled){%>disabled="disabled"<%}%> onclick="changeNodeUserType()" /></td>
                                              <td class="formFldLabel" nowrap="nowrap">Local User</td>
                                            </tr>
                                          </table>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                        <%
                                          if (!NodeUsersBean.getUserType().equals(Phrase.NAASNodeUser)) {
                                        %>
                                        <td>
                                          <div id="divStatus">
                                            <table width="100%">
                                              <tr>
                                                <td class="formFldLabel">Status:&nbsp;<font color="red">*</font></td>
                                                <td>
                                                <%
                                                  String status = NodeUsersBean.getStatus();
                                                %>
                                                  <select name="status">
                                                    <option <%if(status.equals("A")){%>selected="selected"<%}%> value="A">Active</option>
                                                    <option <%if(status.equals("I")){%>selected="selected"<%}%> value="I">Inactive</option>
                                                  </select>
                                                </td>
                                              </tr>
                                            </table>
                                          </div>
                                        </td>
                                        <%
                                          }
                                        %>
                                </tr>
                                <%
                                  if (!NodeUsersBean.getUserType().equals(Phrase.NAASNodeUser)) {
                                %>
                                <tr>
                                  <td colspan="4">
                                    <div id="divFields">
                                      <table width="100%">
                                <tr>
                                        <td class="formFldLabel" nowrap="nowrap">First Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel">
                                          <%
                                            error = NodeUsersBean.getFirstNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="firstName" size="30" text="<%=NodeUsersBean.getFirstName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline"  maxLength="60"/>
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="firstName" size="30" text="<%=NodeUsersBean.getFirstName()%>" maxLength="60"/>
                                          <%
                                            }
                                          %>
                                        </td>
                                        <td class="formFldLabel" nowrap="nowrap">Middle Initial:</td>
                                        <td><eftwc:HighlightTextBox id="midInitial" size="1" text="<%=NodeUsersBean.getMidInitial()%>" maxLength="1"/></td>
                                        <td class="formFldLabel" nowrap="nowrap">Last Name:&nbsp;<font color="red">*</font></td>
                                        <td class="formFldLabel" colspan="3">
                                          <%
                                            error = NodeUsersBean.getLastNameError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="lastName" text="<%=NodeUsersBean.getLastName()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="60" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="lastName" text="<%=NodeUsersBean.getLastName()%>" maxLength="60" />
                                          <%
                                            }
                                          %>
                                        </td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel">Email:&nbsp;<font color="red">*</font></td>
                                        <td>
                                          <%
                                            error = NodeUsersBean.getEmailError();
                                            if (error != null && !error.equals("")) {
                                          %>
                                          <eftwc:HighlightTextBox id="email" size="30" text="<%=NodeUsersBean.getEmail()%>" errorMsg="<%=error%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="100" />
                                          <%
                                            }
                                            else {
                                          %>
                                          <eftwc:HighlightTextBox id="email" size="30" text="<%=NodeUsersBean.getEmail()%>" maxLength="100" />
                                          <%
                                            }
                                          %>
                                        </td>
                                        <td class="formFldLabel">Phone:</td>
                                        <td><eftwc:HighlightTextBox id="phone" size="12" text="<%=NodeUsersBean.getPhone()%>" maxLength="15" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Address:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="address" size="38" text="<%=NodeUsersBean.getAddress()%>" maxLength="100" /></td>
                                  <td class="formFldLabel">Supplemental Address:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="suppAddress" size="38" text="<%=NodeUsersBean.getSuppAddress()%>" maxLength="100" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">City:</td>
                                  <td><eftwc:HighlightTextBox id="city" maxLength="100" size="30" text="<%=NodeUsersBean.getCity()%>" /></td>
                                  <td class="formFldLabel">State:</td>
                                  <td><eftwc:HighlightTextBox id="state" size="2" maxLength="2" text="<%=NodeUsersBean.getState()%>" /></td>
                                  <td class="formFldLabel">Zip Code:</td>
                                  <td><eftwc:HighlightTextBox id="zipCode" size="10" maxLength="15" text="<%=NodeUsersBean.getZipCode()%>" /></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Country:</td>
                                  <td><eftwc:HighlightTextBox id="country" maxLength="25" size="30" text="<%=NodeUsersBean.getCountry()%>" /></td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel" colspan="8">Comments:<br />
                                          <eftwc:HighlightTextBox id="comments" size="100%" rows="4" text="<%=NodeUsersBean.getComments()%>" maxLength="255" /></td>
                                </tr>
                                      </table>
                                    </div>
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
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Node User Privileges</td>
                          </tr>
                          <tr>
                            <td>
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Assigned</td>
                                  <td class="gridHead1" nowrap="nowrap">Domain</td>
                                  <td class="gridHead1" nowrap="nowrap">Web Service</td>
                                  <td class="gridHead1" nowrap="nowrap">Operation</td>
                                </tr>
                                <%
                                  ArrayList list = NodeUsersBean.getOps();
                                  if (list != null) {
                                    for (int i = 0; i < list.size(); i++) {
                                      ArrayList temp = (ArrayList)list.get(i);
                                      String opID = (String)temp.get(1);
                                %>
                          	<tr id="<%=opID%>" class="trOut" onmouseover="Utils.setIDClass('<%=opID%>','trOver')" onmouseout="Utils.setIDClass('<%=opID%>','trOut')">
                                  <td class="RowPlain"><input type="checkbox" name="<%="operation"+opID%>" <%if(((String)temp.get(0)).equalsIgnoreCase("true")){%>checked="checked"<%}%> /></td>
                                  <td class="RowPlain"><%=(String)temp.get(2)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(3)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(4)%></td>
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
                  <table width="30%">
                    <tr>
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
                      <%
                        if (!title.equals("New Node User")) {
                      %>
                      <td><eftwc:AquaButton buttonText="New Password" onClickScript="submitForm('NEW_PASSWORD')" /></td>
                      <%
                        }
                      %>
                      <td><eftwc:AquaButton buttonText="Back" onClickScript="submitForm('Users.jsp')" /></td>
                      <td><eftwc:AquaButton buttonText="Remove User" buttonStyle="aquaRed" onClickScript="submitForm('DELETE')" /></td>
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
