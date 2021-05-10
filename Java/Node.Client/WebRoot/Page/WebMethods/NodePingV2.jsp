<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="NodePingBean" class="Node.Web.Client.Bean.WebMethods.NodePingBean" scope="request" />

<html>
	<head>
		<meta http-Equiv="Cache-Control" Content="no-cache">
		<meta http-Equiv="Pragma" Content="no-cache">
		<meta http-Equiv="Expires" Content="0">
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node</title>
               <link href="/Node.Client/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Client/css/core.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Client/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="javascript" src="/Node.Client/eftWC/js/FloatDiv.js" type="text/javascript" ></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  waitingBlock.switchFloatDiv();
                  document.form1.act.value = action;
                  document.form1.submit();
                }
                <%
                String error = NodePingBean.getError();
                if (error != null && !error.equals("")) { %>
                     alert('<%=error%>');
                <% } %>
               </script>
        </head>
      	<body >
          <form action="/Node.Client/Page/WebMethods/NodePing.do?version=VER_20" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Client/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;NodePing</td>
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
                            <td class="formTitle">Node Address</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Node Address:</td>
                                  <td>
                                    <select name="nodeAddress3">
                                      <%
                                      	String[] clientURLs = NodePingBean.getClientURLs_V2();
                                      	boolean	isSelected = false;
                                       if (clientURLs != null) {
                                         for (int i = 0; i < clientURLs.length; i++) {
                                           if (clientURLs[i].equals(NodePingBean.getNodeAddress3()))
                                             isSelected = true;
                                         }
                                       }
                                       if (isSelected) {
                                       %>
                                       <option></option>
                                       <%
                                       }
                                       else {
                                       %>
                                       <option selected="selected"></option>
                                       <%
                                       }
                                       if (clientURLs != null) {
                                      	for (int i = 0; i < clientURLs.length; i++) {
                                          if (clientURLs[i].equals(NodePingBean.getNodeAddress3())) {
                                      %>
                                      <option selected="selected"><%=clientURLs[i]%></option>
                                      <%
                                    	  }
                                          else {
                                      %>
                                      <option><%=clientURLs[i]%></option>
                                      <%
                                      	  }
                                      	}
                                       }
                                      %>
                                    </select>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">or:</td>
                                  <td><eftwc:HighlightTextBox id="nodeAddress4" size="80" text="<%=NodePingBean.getNodeAddress4()%>" /></td>
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
                            <td class="formTitle">NodePing Parameters</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="30%">
                                <tr>
                                  <td class="formFldLabel">String:</td>
                                  <td><eftwc:HighlightTextBox id="string" size="80" text="<%=NodePingBean.getString()%>" /></td>
                                </tr>
                                <tr>
                                  <td><eftwc:AquaButton buttonText="NodePing" onClickScript="submitForm('NODE_PING')" /></td>
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
                            <td class="formTitle">Result:</td>
                            <td class="formFldLabel"><eftwc:HighlightTextBox id="result" text="<%=NodePingBean.getResult()%>" size="80" /></td>
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
          <link href="/Node.Client/eftWC/skin/css/core.css" rel="stylesheet" type="text/css">
            <script src="/Node.Client/eftWC/js/FloatDiv.js"></script>
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
                  <span class="waitBlockFont">Please Wait While the Node Request is Made...</span>
                  <p><img src="/Node.Client/eftWC/skin/img/gen/wait_bar.gif"></p>
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
