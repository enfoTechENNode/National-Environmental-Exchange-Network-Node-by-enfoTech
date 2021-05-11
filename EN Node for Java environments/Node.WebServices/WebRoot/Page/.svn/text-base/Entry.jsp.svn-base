<%@ page import="Node.Phrase" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="EntryBean" class="Node.Web.WebServices.Bean.EntryBean" scope="request" />

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
          <form action="/Node.WebServices/Page/Entry.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.WebServices/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Node Status</td>
                    </tr>
                    <%
                      String message = EntryBean.getMessage();
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
                        <table>
                          <tr>
                            <td class="formTitle">Node Status</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel" nowrap="nowrap">Node Status</td>
                                  <td class="formFldLabel">
                                    <%
                                      String status = EntryBean.getNodeStatus();
                                      if (status.equals(Phrase.RUNNING_STATUS)) {
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
                                </tr>
                                <tr>
                                  <td class="formFldLabel" nowrap="nowrap">Status Message:</td>
                                  <td class="formFldLabel"><%=EntryBean.getNodeMessage()%></td>
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
                            <td class="formTitle" colspan="2">User Login</td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">User Login:</td>
                            <td class="formFldLabel"><eftwc:HighlightTextBox id="userName" /></td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Password:</td>
                            <td class="formFldLabel"><eftwc:HighlightTextBox id="password" type="password" /></td>
                          </tr>
                          <tr>
                            <td class="formFldLabel" colspan="2"><eftwc:AquaButton buttonText="Login" onClickScript="submitForm('LOGIN')" linkBase="/Node.WebServices/eftWC" /></td>
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
