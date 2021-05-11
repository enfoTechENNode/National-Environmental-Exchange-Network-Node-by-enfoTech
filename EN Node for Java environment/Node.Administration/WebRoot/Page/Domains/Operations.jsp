<%@ page import="Node.Biz.Administration.Operation" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="OperationsBean" class="Node.Web.Administration.Bean.Domains.OperationsBean" scope="session" />

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
                  waitingBlock.switchFloatDiv();
                  document.form1.act.value = action;
                  document.form1.submit();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Domains/Operations.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Operations.do">Operation Management</a> - <%=OperationsBean.getDomain()%>&nbsp;&nbsp;(<%=OperationsBean.getVersion()%>)</td>
                    </tr>
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
                                  <td class="formFldLabel">Operation Name:</td>
                                  <td><eftwc:HighlightTextBox id="operationsName" text="<%=OperationsBean.getOperationsName()%>" /></td>
                                  <td class="formFldLabel">Operation Type:</td>
                                  <td>
                                    <select name="operationsType">
                                      <%
                                        String type = OperationsBean.getOperationsType();
                                      %>
                                      <option <%if(type.equals("")){%>selected="selected"<%}%>></option>
                                      <option value="WEB_SERVICE" <%if(type.equals("WEB_SERVICE")){%>selected="selected"<%}%>>Web Service</option>
                                      <option value="SCHEDULED_TASK" <%if(type.equals("SCHEDULED_TASK")){%>selected="selected"<%}%>>Scheduled Task</option>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Web Method</td>
                                  <td>
                                    <select name="webMethod">
                                      <%
                                      	String method = OperationsBean.getWebMethod();
                                      %>
                                      <option <%if(method.equals("")){%>selected="selected"<%}%>></option>
                                      <option <%if(method.equals("AUTHENTICATE")){%>selected="selected"<%}%>>AUTHENTICATE</option>
                                      <option <%if(method.equals("NODEPING")){%>selected="selected"<%}%>>NODEPING</option>
                                      <option <%if(method.equals("GETSTATUS")){%>selected="selected"<%}%>>GETSTATUS</option>
                                      <option <%if(method.equals("GETSERVICE")){%>selected="selected"<%}%>>GETSERVICE</option>
                                      <option <%if(method.equals("SUBMIT")){%>selected="selected"<%}%>>SUBMIT</option>
                                      <option <%if(method.equals("DOWNLOAD")){%>selected="selected"<%}%>>DOWNLOAD</option>
                                      <option <%if(method.equals("QUERY")){%>selected="selected"<%}%>>QUERY</option>
                                      <option <%if(method.equals("SOLICIT")){%>selected="selected"<%}%>>SOLICIT</option>
                                      <option <%if(method.equals("NOTIFY")){%>selected="selected"<%}%>>NOTIFY</option>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Status:</td>
                                  <td>
                                    <select name="status">
                                      <%
                                        String status = OperationsBean.getStatus();
                                      %>
                                      <option <%if(status.equals("")){%>selected="selected"<%}%>></option>
                                      <option <%if(status.equals("Running")){%>selected="selected"<%}%>>Running</option>
                                      <option <%if(status.equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                      <option <%if(status.equals("Active")){%>selected="selected"<%}%>>Active</option>
                                      <option <%if(status.equals("Inactive")){%>selected="selected"<%}%>>Inactive</option>
                                    </select>
                                  </td>
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
                        <!-- Documents -->
                        <table width="100%">
                          <tr>
                            <td colspan="2">
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%" align="center">View/Edit</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Operation Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Operation Type</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Web Service Method</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Operation Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="30%">Operation Status Message</td>
                                </tr>
                                <%
                                  Operation[] ops = OperationsBean.getOperationList();
                                  if (ops != null) {
                                    for (int i = 0; i < ops.length; i++) {
                                %>
                          	<tr id="op<%=ops[i].GetOperationID()%>" class="trOut" onmouseover="Utils.setIDClass('op<%=ops[i].GetOperationID()%>','trOver')" onmouseout="Utils.setIDClass('op<%=ops[i].GetOperationID()%>','trOut')">
                                    <td class="RowPlain" align="center"><a href="/Node.Administration/Page/Domains/OperationsWizard.do?opID=<%=ops[i].GetOperationID()%>">Edit</a></td>
                                    <td class="RowPlain"><%=ops[i].GetOpName()%></td>
                                    <td class="RowPlain"><%=ops[i].GetType()%></td>
                                    <td class="RowPlain"><%=ops[i].GetWebService()%></td>
                                    <td class="RowPlain"><%=ops[i].GetStatus()%></td>
                                    <td class="RowPlain"><%=ops[i].GetMessage()%></td>
                                </tr>
                                <%
                                    }
                                  }
                                %>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table width="40%">
                                <tr>
                                  <!-- <td><eftwc:AquaButton buttonText="Create New Operation" onClickScript="submitForm('NEW_OPERATION')" linkBase="/Node.Administration/eftWC" /></td> -->
                                  <td><eftwc:AquaButton buttonText="Create Operation" onClickScript="submitForm('WIZARD')" linkBase="/Node.Administration/eftWC" /></td>
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
