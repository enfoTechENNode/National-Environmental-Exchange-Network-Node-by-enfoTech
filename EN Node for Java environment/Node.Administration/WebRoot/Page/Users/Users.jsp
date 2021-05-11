<%@ page import="Node.Biz.Administration.User" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="UsersBean" class="Node.Web.Administration.Bean.Users.UsersBean" scope="request" />

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
                  waitingBlock.switchFloatDiv();
                  if (action != "NEW_NODE_USER" && action != "NEW_ADMIN_CONSOLE_USER"
                  	&& action != "SEARCH" && action != "REFRESH")
                  {
                    Utils.gotoURL('self',action);
                  }
                  else
                  {
                    document.form1.act.value = action;
                    document.form1.submit();
                  }
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Users/Users.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a onclick="submitForm('/Node.Administration/Page/Users/Users.do')" href="#">User Management</a></td>
                    </tr>
                    <%
                      String message = UsersBean.getMessage();
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
                        <!-- Searching -->
                        <table>
                          <tr>
                            <td class="formTitle">Searching Criteria</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Login Name:</td>
                                  <td><eftwc:HighlightTextBox id="login" text="<%=UsersBean.getLogin()%>" maxLength="50"/></td>
                                  <td class="formFldLabel">User Type:</td>
                                  <td>
                                    <select name="type">
                                      <%
                                        String type = UsersBean.getType();
                                      %>
                                      <option <%if(type.equals("")){%>selected="selected"<%}%>></option>
                                      <option <%if(type.equals("CONSOLE_USER")){%>selected="selected"<%}%> value="CONSOLE_USER">Console User</option>
                                      <option <%if(type.equals("NAAS_NODE_USER")){%>selected="selected"<%}%> value="NAAS_NODE_USER">NAAS Node User</option>
                                      <option <%if(type.equals("LOCAL_NODE_USER")){%>selected="selected"<%}%> value="LOCAL_NODE_USER">Local Node User</option>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Associated Domain:</td>
                                  <td>
                                    <select name="domain">
                                      <%
                                        String domain = UsersBean.getDomain();
                                      %>
                                      <option <%if(domain.equals("")){%>selected="selected"<%}%>></option>
                                      <%
                                        String[] domains = UsersBean.getDomains();
                                        if (domains != null) {
                                          for (int i = 0; i < domains.length; i++) {
                                      %>
                                      <option <%if(domain.equals(domains[i])){%>selected="selected"<%}%>><%=domains[i]%></option>
                                      <%
                                          }
                                        }
                                      %>
                                    </select>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">First Name:</td>
                                  <td><eftwc:HighlightTextBox id="firstName" text="<%=UsersBean.getFirstName()%>" maxLength="60" /></td>
                                  <td class="formFldLabel">Last Name:</td>
                                  <td><eftwc:HighlightTextBox id="lastName" text="<%=UsersBean.getLastName()%>" maxLength="60"/></td>
                                  <td class="formFldLabel" colspan="2"><input type="checkbox" name="showNAAS" <%if(UsersBean.getShowNAAS().equals("on")){%>checked="checked"<%}%> /> Search All NAAS Users</td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td><eftwc:AquaButton buttonText="Search" onClickScript="submitForm('SEARCH')" /></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <!-- Users -->
                        <table width="100%">
                          <tr>
                            <td colspan="2">
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">View/Edit</td>
                                  <td class="gridHead1" nowrap="nowrap" width="25%">User Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="25%">Full Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">User Type</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Created Date</td>
                                </tr>
                                  <%
                                    User[] users = UsersBean.getUserList();
                                    if (users != null && users.length > 0) {
                                      for (int i = 0; i < users.length; i++) {
                                        String first = users[i].GetFirstName();
                                        String mid = users[i].GetMiddleInitial();
                                        String last = users[i].GetLastName();
                                        String name = "";
                                      	if (first != null)
                                          name += first;
                                        if (mid != null && !mid.equals("")) {
                                          if (!name.equals(""))
                                            name += " ";
                                          name += mid;
                                        }
                                        if (last != null && !last.equals("")) {
                                          if (!name.equals(""))
                                            name += " ";
                                          name += last;
                                        }
                                  %>
                          	<tr id="<%=users[i].GetUserID()%>" class="trOut" onmouseover="Utils.setIDClass('<%=users[i].GetUserID()%>','trOver')" onmouseout="Utils.setIDClass('<%=users[i].GetUserID()%>','trOut')">
                                    <td class="RowPlain" align="center">
                                      <%
                                        if (users[i].IsConsoleUser()) {
                                      %>
                                      <a onclick="submitForm('/Node.Administration/Page/Users/ConsoleUsers.do?userid=<%=users[i].GetUserID()%>')" href="#">View</a>
                                      <%
                                        }
                                        else {
                                      %>
                                      <a onclick="submitForm('/Node.Administration/Page/Users/NodeUsers.do?userid=<%=users[i].GetUserID()%>')" href="#">View</a>
                                      <%
                                        }
                                      %>
                                    </td>
                                    <td class="RowPlain"><%=users[i].GetLoginName()%></td>
                                    <td class="RowPlain"><%=name%></td>
                                    <td class="RowPlain"><%=users[i].GetStatus()%></td>
                                    <td class="RowPlain"><%=users[i].GetAccountType()%></td>
                                    <td class="RowPlain"><%=users[i].GetCreatedDate()%></td>
                                </tr>
                                  <%
                                      }
                                    }
                                    User[] naasUsers = UsersBean.getNaasUserList();
                                    if (naasUsers != null && naasUsers.length > 0) {
                                      for (int i = 0; i < naasUsers.length; i++) {
                                  %>
                          	<tr id="<%=naasUsers[i].GetLoginName()%>" class="trOut" onmouseover="Utils.setIDClass('<%=naasUsers[i].GetLoginName()%>','trOver')" onmouseout="Utils.setIDClass('<%=naasUsers[i].GetLoginName()%>','trOut')">
                                    <td class="RowPlain" align="center"><a onclick="submitForm('/Node.Administration/Page/Users/NodeUsers.do?username=<%=naasUsers[i].GetLoginName()%>')" href="#">View</a></td>
                                    <td class="RowPlain"><%=naasUsers[i].GetLoginName()%></td>
                                    <td class="RowPlain"></td>
                                    <td class="RowPlain"></td>
                                    <td class="RowPlain">NAAS_NODE_USER</td>
                                    <td class="RowPlain"></td>
                                </tr>
                                  <%
                                      }
                                    }
                                    String act = UsersBean.getAct();
                                    if (act != null && act.equals("INITIAL"))
                                    {
                                  %>
                                  <tr>
                                    <td class="RowPlain" colspan="5">Please Search to View Users</td>
                                  </tr>
                                  <%
                                    }
                                  %>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table width="30%">
                                <tr>
                                  <td>
                                    <eftwc:AquaButton buttonText="Create New Admin Console User" onClickScript="submitForm('NEW_ADMIN_CONSOLE_USER')" linkBase="/Node.Administration/eftWC" />
                                  </td>
                                  <td>
                                    <eftwc:AquaButton buttonText="Create New Node User" onClickScript="submitForm('NEW_NODE_USER')" linkBase="/Node.Administration/eftWC" />
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
