<%@ page import="java.util.ArrayList" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="TransactionViewBean" class="Node.Web.Administration.Bean.NodeMonitoring.TransactionViewBean" scope="session" />

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
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Node Monitoring - Transaction View</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%" >
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">OperationDetails</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <%
                                  ArrayList details = TransactionViewBean.getDetails();
                                  if (details != null) {
                                    for (int i = 0; i < details.size(); i++) {
                                      ArrayList temp = (ArrayList)details.get(i);
                                      if (temp != null && temp.size() == 3) {
                                %>
                                <tr id="<%=(String)temp.get(0)%>" class="trOut" onmouseover="Utils.setIDClass('<%=(String)temp.get(0)%>','trOver')" onmouseout="Utils.setIDClass('<%=(String)temp.get(0)%>','trOut')">
                                  <td class="RowPlain"><%=(String)temp.get(1)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(2)%></td>
                                </tr>
                                <%
                                      }
                                    }
                                  }
                                  ArrayList params = TransactionViewBean.getParameters();
                                  if (params != null && params.size() > 0) {
                                %>
                                <tr>
                                  <td class="RowPlain"><u>Parameters</u></td>
                                  <td class="RowPlain"><u>Parameter Values</u></td>
                                </tr>
                                <%
                                    for (int i = 0; i < params.size(); i++) {
                                      ArrayList temp = (ArrayList)params.get(i);
                                      if (temp != null && temp.size() >= 2) {
                                %>
                                <tr id="<%="param"+i%>" class="trOut" onmouseover="Utils.setIDClass('<%="param"+i%>','trOver')" onmouseout="Utils.setIDClass('<%="param"+i%>','trOut')">
                                  <td class="RowPlain"><%=(String)temp.get(0)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(1)%></td>
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
                            <td class="formTitle">Status History</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="15%" align="center">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="70%">Message</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Date</td>
                                </tr>
                                <%
                                  ArrayList status = TransactionViewBean.getStatus();
                                  if (status != null) {
                                    for (int i = 0; i < status.size(); i++) {
                                      ArrayList temp = (ArrayList)status.get(i);
                                      if (temp != null && temp.size() >= 4) {
                                %>
                                <tr id="<%=(String)temp.get(0)%>" class="trOut" onmouseover="Utils.setIDClass('<%=(String)temp.get(0)%>','trOver')" onmouseout="Utils.setIDClass('<%=(String)temp.get(0)%>','trOut')">
                                  <td class="RowPlain"><%=(String)temp.get(1)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(2)%></td>
                                  <td class="RowPlain"><%=(String)temp.get(3)%></td>
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
                  </table>
                </td>
              </tr>
              <tr>
                <td><!--<eftwc:AquaButton buttonText="Back" onClickScript="submitForm('BACK')" />--></td>
              </tr>
            </table>
          </form>
        </body>
</html>
