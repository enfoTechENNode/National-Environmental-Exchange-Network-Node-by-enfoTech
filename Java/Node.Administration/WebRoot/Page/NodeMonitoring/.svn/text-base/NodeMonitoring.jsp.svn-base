<%@ page import="Node.Phrase,Node.Biz.Administration.OperationLog,java.text.SimpleDateFormat,java.util.ArrayList" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="NodeMonitoringBean" class="Node.Web.Administration.Bean.NodeMonitoring.NodeMonitoringBean" scope="session" />

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
          <form action="/Node.Administration/Page/NodeMonitoring/NodeMonitoring.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <eftwc:DivCalendar dateFormat="YMD" yearNavigate="true" dateSpliter="-" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Node Monitoring</td>
                    </tr>
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
                            <td class="formTitle">Searching Criteria</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel" nowrap="nowrap">Operation Name:</td>
                                  <td>
                                    <select name="opName">
                                      <%
                                        String opName = NodeMonitoringBean.getOpName();
                                      %>
                                      <option <%if(opName.equals("")){%>selected="selected"<%}%>></option>
                                      <%
                                        String[] opNameList = NodeMonitoringBean.getOpNameList();
                                        if (opNameList != null) {
                                          for (int i = 0; i < opNameList.length; i++) {
                                      %>
                                      <option <%if(opName.equals(opNameList[i])){%>selected="selected"<%}%>><%=opNameList[i]%></option>
                                      <%
                                          }
                                        }
                                      %>
                                    </select>
                                  </td>
                                  <td class="formFldLabel" nowrap="nowrap">Operation Type:</td>
                                  <td>
                                    <select name="opType">
                                      <%
                                        String type = NodeMonitoringBean.getOpType();
                                      %>
                                      <option <%if(type.equals("")){%>selected="selected"<%}%>></option>
                                      <option <%if(type.equals(Phrase.WEB_SERVICE_OPERATION)){%>selected="selected"<%}%>><%=Phrase.WEB_SERVICE_OPERATION%></option>
                                      <option <%if(type.equals(Phrase.SCHEDULED_TASK_OPERATION)){%>selected="selected"<%}%>><%=Phrase.SCHEDULED_TASK_OPERATION%></option>
                                    </select>
                                  </td>
                                        <td class="formFldLabel">Web Service:</td>
                                        <td>
                                          <select name="webService">
                                            <%
                                              String wsName = NodeMonitoringBean.getWebService();
                                            %>
                                            <option <%if(wsName.equals("")){%>selected="selected"<%}%>></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_AUTHENTICATE)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_AUTHENTICATE%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_DOWNLOAD)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_DOWNLOAD%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_GETSERVICES)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_GETSERVICES%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_GETSTATUS)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_GETSTATUS%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_NODEPING)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_NODEPING%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_NOTIFY)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_NOTIFY%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_QUERY)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_QUERY%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_SOLICIT)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_SOLICIT%></option>
                                            <option <%if(wsName.equals(Phrase.WEB_METHOD_SUBMIT)){%>selected="selected"<%}%>><%=Phrase.WEB_METHOD_SUBMIT%></option>
                                          </select>
                                        </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Status:</td>
                                        <td>
                                          <select name="status">
                                            <option></option>
                                            <%
                                              String status = NodeMonitoringBean.getStatus();
                                              String[] statusList = NodeMonitoringBean.getStatusList();
                                              if (statusList != null) {
                                                for (int i = 0; i < statusList.length; i++) {
                                            %>
                                            <option <%if(status.equals(statusList[i])){%>selected="selected"<%}%>><%=statusList[i]%></option>
                                            <%
                                                }
                                              }
                                            %>
                                          </select>
                                        </td>
                                        <td class="formFldLabel">Domain:</td>
                                        <td>
                                          <select name="domain">
                                            <option></option>
                                            <%
                                              String domain = NodeMonitoringBean.getDomain();
                                              String[] domains = NodeMonitoringBean.getDomainList();
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
                                  <td class="formFldLabel">User ID:</td>
                                  <td><eftwc:HighlightTextBox id="userID" text="<%=NodeMonitoringBean.getUserID()%>" maxLength="50"/></td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">Security Token:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="token" size="35" text="<%=NodeMonitoringBean.getToken()%>" /></td>
                                  <td class="formFldLabel" align="right">Transaction ID:</td>
                                  <td colspan="2"><eftwc:HighlightTextBox id="transID" size="35" text="<%=NodeMonitoringBean.getTransID()%>" maxLength="50"/></td>
                                </tr>
                                <tr>
                                        <td class="formFldLabel">Search Date:</td>
                                        <td><eftwc:DatePicker id="start" text="<%=NodeMonitoringBean.getStart()%>" /></td>
                                        <td class="formFldLabel">And:</td>
                                        <td><eftwc:DatePicker id="end" text="<%=NodeMonitoringBean.getEnd()%>" /></td>
                                </tr>
                                <tr>
                                  <td><eftwc:AquaButton buttonText="Search" onClickScript="submitForm('SEARCH')" /></td>
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
                            <td colspan="2">
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="5%" align="center">View</td>
                                  <td class="gridHead1" nowrap="nowrap" width="8%">Operation</td>
                                  <td class="gridHead1" nowrap="nowrap" width="12%">Web Service</td>
                                  <td class="gridHead1" nowrap="nowrap" width="6%">Domain</td>
                                  <td class="gridHead1" nowrap="nowrap" width="17%">User ID</td>
                                  <td class="gridHead1" nowrap="nowrap" width="28%">Transaction ID</td>
                                  <td class="gridHead1" nowrap="nowrap" width="9%">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Date</td>
                                </tr>
                                <%
                                  OperationLog[] list = NodeMonitoringBean.getSearchedLogs();
                                  if (list != null) {
                                    SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
                                    for (int i = 0; i < list.length; i++) {
                                %>
                          	<tr id="<%="op"+list[i].GetOpLogID()%>" class="trOut" onmouseover="Utils.setIDClass('<%="op"+list[i].GetOpLogID()%>','trOver')" onmouseout="Utils.setIDClass('<%="op"+list[i].GetOpLogID()%>','trOut')">
                                    <td class="RowPlain" align="center"><a href="/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID=<%=list[i].GetOpLogID()%>">View</a></td>
                                    <td class="RowPlain"><%=list[i].GetOperationName()%></td>
                                    <td class="RowPlain">
                                      <%
                                        if (list[i].GetOperationType().equals(Phrase.WEB_SERVICE_OPERATION)) {
                                      %>
                                      <%=list[i].GetWebServiceName()%>
                                      <%
                                        }
                                        else {
                                      %>
                                      Task
                                      <%
                                        }
                                      %>
                                    </td>
                                    <td class="RowPlain"><%=list[i].GetDomain()%></td>
                                    <td class="RowPlain"><%=list[i].GetUserName()%></td>
                                    <td class="RowPlain"><%=list[i].GetTransID()%></td>
                                    <td class="RowPlain"><%=(String)((ArrayList)(list[i].GetStatus().get(0))).get(0)%></td>
                                    <td class="RowPlain">
                                      <%=list[i].GetStartDate()%><br />
                                      <%=list[i].GetEndDate()%>
                                    </td>
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
            </table>
          </form>
        </body>
</html>
