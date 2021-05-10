<%@ page import="java.util.ArrayList,Node.Phrase" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="StatusBean" class="Node.Web.WebServices.Bean.StatusBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.WebServices/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.WebServices/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.WebServices/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.WebServices/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  document.form1.act.value = action;
                  document.form1.submit();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.WebServices/Page/Status.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.WebServices/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Node Status</td>
                    </tr>
                    <%
                      String message = StatusBean.getMessage();
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
                  <table width="100%" frame="box">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Domains Status</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">View</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Domain</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="60%">Message</td>
                                </tr>
                                <%
                                  ArrayList domains = StatusBean.getDomains();
                                  if (domains != null) {
                                    for (int i = 0; i < domains.size(); i++) {
                                      ArrayList temp = (ArrayList)domains.get(i);
                                      if (temp != null && temp.size() >= 3) {
                                %>
                          	<tr>
                                    <td class="RowPlain"><a href="<%="/Node.WebServices/Page/Status.do?selDomain="+(String)temp.get(0)%>">View</a></td>
                                    <td class="RowPlain"><%=(String)temp.get(0)%></td>
                                    <td class="RowPlain">
                                      <%
                                        String status = (String)temp.get(1);
                                        if (status != null && status.equals(Phrase.RUNNING_STATUS)) {
                                      %>
                                      <img src="/Node.WebServices/skin/images/checkmark2_green.gif" height="15" width="15" />&nbsp;<font color="green">
                                      <%
                                        }
                                        else {
                                      %>
                                      <img src="/Node.WebServices/skin/images/redx.jpg" height="15" width="15" />&nbsp;<font color="red">
                                      <%
                                        }
                                      %>
                                      <%=status%></font>
                                    </td>
                                    <td class="RowPlain"><%=(String)temp.get(2)%></td>
                                </tr>
                                <%
                                      }
                                    }
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
                            <td class="formTitle"><%=StatusBean.getSelDomain()%> Domain - Operations Status</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Operation</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="70%">Message</td>
                                </tr>
                                <%
                                  String selDomain = StatusBean.getSelDomain();
                                  ArrayList ops = StatusBean.getOperations();
                                  if (ops != null) {
                                    for (int i = 0; i < ops.size(); i++) {
                                      ArrayList temp = (ArrayList)ops.get(i);
                                      if (temp != null && temp.size() >= 4) {
                                        String dom = (String)temp.get(0);
                                        if (selDomain != null && selDomain.equals(dom)) {
                                %>
                          	<tr>
                                    <td class="RowPlain"><%=(String)temp.get(1)%></td>
                                    <td class="RowPlain">
                                      <%
                                        String status = (String)temp.get(2);
                                        if (status != null && status.equals(Phrase.RUNNING_STATUS)) {
                                      %>
                                      <img src="/Node.WebServices/skin/images/checkmark2_green.gif" height="15" width="15" />&nbsp;<font color="green">
                                      <%
                                        }
                                        else {
                                      %>
                                      <img src="/Node.WebServices/skin/images/redx.jpg" height="15" width="15" />&nbsp;<font color="red">
                                      <%
                                        }
                                      %>
                                      <%=status%></font></td>
                                    <td class="RowPlain"><%=(String)temp.get(3)%></td>
                                </tr>
                                <%
                                        }
                                      }
                                    }
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
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle" colspan="2">Change Password</td>
                          </tr>
                          <tr>
                            <td class="formFldLabel errFont" colspan="2">If user is a NAAS Node User, the password should consist of at least 8 characeters, including one upper case letter, one lower case letter, and one numeric character.<br />
                              Note: No password restrictions apply for Local Node Users or Administration Console Users.
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <table width="40%">
                          <tr>
                            <td class="formFldLabel">New Password:</td>
                            <td class="formFldLabel"><eftwc:HighlightTextBox id="newPWD1" type="password" text="<%=StatusBean.getNewPWD1()%>" /></td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Confirm New Password:</td>
                            <td class="formFldLabel"><eftwc:HighlightTextBox id="newPWD2" type="password" text="<%=StatusBean.getNewPWD2()%>" /></td>
                          </tr>
                          <tr>
                            <td class="formFldLabel" colspan="2"><eftwc:AquaButton buttonText="Change Password" onClickScript="submitForm('CHANGE_PWD')" linkBase="/Node.WebServices/eftWC" /></td>
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
        </body>
</html>
