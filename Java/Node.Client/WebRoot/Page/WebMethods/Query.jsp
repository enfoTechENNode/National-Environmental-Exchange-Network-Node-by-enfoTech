<%@ page import="Node.Utils.Utility" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="QueryBean" class="Node.Web.Client.Bean.WebMethods.QueryBean" scope="session" />

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
                function showHideParams ()
                {
                  var num = <%=QueryBean.getNumParams()%>;
                  testDiv(num, 1);
                  testDiv(num, 2);
                  testDiv(num, 3);
                  testDiv(num, 4);
                  testDiv(num, 5);
                  testDiv(num, 6);
                  testDiv(num, 7);
                  testDiv(num, 8);
                  testDiv(num, 9);
                  testDiv(num, 10);
                  testDiv(num, 11);
                  testDiv(num, 12);
                  testDiv(num, 13);
                  testDiv(num, 14);
                  testDiv(num, 15);
                  testDiv(num, 16);
                  testDiv(num, 17);
                  testDiv(num, 18);
                  testDiv(num, 19);
                  testDiv(num, 20);
                  testDiv(num, 21);
                  testDiv(num, 22);
                  testDiv(num, 23);
                  testDiv(num, 24);
                  testDiv(num, 25);
                  testDiv(num, 26);
                  testDiv(num, 27);
                  testDiv(num, 28);
                  testDiv(num, 29);
                  testDiv(num, 30);
                }
                function testDiv (num, divNum)
                {
                  if (num >= divNum) {
                    //alert('show' + divNum);
                    Utils.showDiv('paramTitle' + divNum);
                    Utils.showDiv('param' + divNum);
                  }
                  else {
                    //alert('hide' + divNum);
                    Utils.hideDiv('paramTitle' + divNum);
                    Utils.hideDiv('param' + divNum);
                  }
                }
                <%
                String error = QueryBean.getError();
                if (error != null && !error.equals("")) { %>
                     alert('<%=error%>');
                <% } %>
               </script>
        </head>
      	<body >
          <form action="/Node.Client/Page/WebMethods/Query.do?version=VER_11" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Client/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Query</td>
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
                                    <select name="nodeAddress1">
                                      <%
                                      	String[] clientURLs = QueryBean.getClientURLs();
                                      	boolean	isSelected = false;
                                       if (clientURLs != null) {
                                         for (int i = 0; i < clientURLs.length; i++) {
                                           if (clientURLs[i].equals(QueryBean.getNodeAddress1()))
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
                                          if (clientURLs[i].equals(QueryBean.getNodeAddress1())) {
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
                                  <td><eftwc:HighlightTextBox id="nodeAddress2" size="80" text="<%=QueryBean.getNodeAddress2()%>" /></td>
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
                            <td class="formTitle">Query Parameters</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Security Token:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="token" size="80" text="<%=QueryBean.getToken()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Request Name:</td>
                                        <td>
                                          <select name="request1" onchange="submitForm('')">
                                            <%
                                              String request1 = QueryBean.getRequest1();
                                            %>
                                            <option <%if(request1 == null || request1.equals("")){%>selected="selected"<%}%>></option>
                                            <%
                                              String[] availableRequests = QueryBean.getAvailableRequests();
                                              if (availableRequests != null && availableRequests.length > 0) {
                                                for (int i = 0; i < availableRequests.length; i++) {
                                            %>
                                            <option <%if(availableRequests[i] != null && availableRequests[i].equals(request1)){%>selected="selected"<%}%>><%=availableRequests[i]%></option>
                                            <%
                                                }
                                              }
                                            %>
                                          </select>
                                        </td>
                                        <td class="formFldLabel">or:</td>
                                        <td><eftwc:HighlightTextBox id="request2" size="35" text="<%=QueryBean.getRequest2()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Row ID:</td>
                                        <td><eftwc:HighlightTextBox id="rowID" size="5" text="<%=QueryBean.getRowID()%>" /></td>
                                        <td class="formFldLabel" nowrap="nowrap">Max Rows:</td>
                                        <td><eftwc:HighlightTextBox id="maxRows" size="5" text="<%=QueryBean.getMaxRows()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle1"><%=QueryBean.getParamName1()%></div></td>
                                        <td><div id="param1"><eftwc:HighlightTextBox id="parameter1" size="35" text="<%=QueryBean.getParameter1()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle2"><%=QueryBean.getParamName2()%></div></td>
                                        <td><div id="param2"><eftwc:HighlightTextBox id="parameter2" size="35" text="<%=QueryBean.getParameter2()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle3"><%=QueryBean.getParamName3()%></div></td>
                                        <td><div id="param3"><eftwc:HighlightTextBox id="parameter3" size="35" text="<%=QueryBean.getParameter3()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle4"><%=QueryBean.getParamName4()%></div></td>
                                        <td><div id="param4"><eftwc:HighlightTextBox id="parameter4" size="35" text="<%=QueryBean.getParameter4()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle5"><%=QueryBean.getParamName5()%></div></td>
                                        <td><div id="param5"><eftwc:HighlightTextBox id="parameter5" size="35" text="<%=QueryBean.getParameter5()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle6"><%=QueryBean.getParamName6()%></div></td>
                                        <td><div id="param6"><eftwc:HighlightTextBox id="parameter6" size="35" text="<%=QueryBean.getParameter6()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle7"><%=QueryBean.getParamName7()%></div></td>
                                        <td><div id="param7"><eftwc:HighlightTextBox id="parameter7" size="35" text="<%=QueryBean.getParameter7()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle8"><%=QueryBean.getParamName8()%></div></td>
                                        <td><div id="param8"><eftwc:HighlightTextBox id="parameter8" size="35" text="<%=QueryBean.getParameter8()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle9"><%=QueryBean.getParamName9()%></div></td>
                                        <td><div id="param9"><eftwc:HighlightTextBox id="parameter9" size="35" text="<%=QueryBean.getParameter9()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle10"><%=QueryBean.getParamName10()%></div></td>
                                        <td><div id="param10"><eftwc:HighlightTextBox id="parameter10" size="35" text="<%=QueryBean.getParameter10()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle11"><%=QueryBean.getParamName11()%></div></td>
                                        <td><div id="param11"><eftwc:HighlightTextBox id="parameter11" size="35" text="<%=QueryBean.getParameter11()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle12"><%=QueryBean.getParamName12()%></div></td>
                                        <td><div id="param12"><eftwc:HighlightTextBox id="parameter12" size="35" text="<%=QueryBean.getParameter12()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle13"><%=QueryBean.getParamName13()%></div></td>
                                        <td><div id="param13"><eftwc:HighlightTextBox id="parameter13" size="35" text="<%=QueryBean.getParameter13()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle14"><%=QueryBean.getParamName14()%></div></td>
                                        <td><div id="param14"><eftwc:HighlightTextBox id="parameter14" size="35" text="<%=QueryBean.getParameter14()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle15"><%=QueryBean.getParamName15()%></div></td>
                                        <td><div id="param15"><eftwc:HighlightTextBox id="parameter15" size="35" text="<%=QueryBean.getParameter15()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle16"><%=QueryBean.getParamName16()%></div></td>
                                        <td><div id="param16"><eftwc:HighlightTextBox id="parameter16" size="35" text="<%=QueryBean.getParameter16()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle17"><%=QueryBean.getParamName17()%></div></td>
                                        <td><div id="param17"><eftwc:HighlightTextBox id="parameter17" size="35" text="<%=QueryBean.getParameter17()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle18"><%=QueryBean.getParamName18()%></div></td>
                                        <td><div id="param18"><eftwc:HighlightTextBox id="parameter18" size="35" text="<%=QueryBean.getParameter18()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle19"><%=QueryBean.getParamName19()%></div></td>
                                        <td><div id="param19"><eftwc:HighlightTextBox id="parameter19" size="35" text="<%=QueryBean.getParameter19()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle20"><%=QueryBean.getParamName20()%></div></td>
                                        <td><div id="param20"><eftwc:HighlightTextBox id="parameter20" size="35" text="<%=QueryBean.getParameter20()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle21"><%=QueryBean.getParamName21()%></div></td>
                                        <td><div id="param21"><eftwc:HighlightTextBox id="parameter21" size="35" text="<%=QueryBean.getParameter21()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle22"><%=QueryBean.getParamName22()%></div></td>
                                        <td><div id="param22"><eftwc:HighlightTextBox id="parameter22" size="35" text="<%=QueryBean.getParameter22()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle23"><%=QueryBean.getParamName23()%></div></td>
                                        <td><div id="param23"><eftwc:HighlightTextBox id="parameter23" size="35" text="<%=QueryBean.getParameter23()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle24"><%=QueryBean.getParamName24()%></div></td>
                                        <td><div id="param24"><eftwc:HighlightTextBox id="parameter24" size="35" text="<%=QueryBean.getParameter24()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle25"><%=QueryBean.getParamName25()%></div></td>
                                        <td><div id="param25"><eftwc:HighlightTextBox id="parameter25" size="35" text="<%=QueryBean.getParameter25()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle26"><%=QueryBean.getParamName26()%></div></td>
                                        <td><div id="param26"><eftwc:HighlightTextBox id="parameter26" size="35" text="<%=QueryBean.getParameter26()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle27"><%=QueryBean.getParamName27()%></div></td>
                                        <td><div id="param27"><eftwc:HighlightTextBox id="parameter27" size="35" text="<%=QueryBean.getParameter27()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle28"><%=QueryBean.getParamName28()%></div></td>
                                        <td><div id="param28"><eftwc:HighlightTextBox id="parameter28" size="35" text="<%=QueryBean.getParameter28()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle29"><%=QueryBean.getParamName29()%></div></td>
                                        <td><div id="param29"><eftwc:HighlightTextBox id="parameter29" size="35" text="<%=QueryBean.getParameter29()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle30"><%=QueryBean.getParamName30()%></div></td>
                                        <td><div id="param30"><eftwc:HighlightTextBox id="parameter30" size="35" text="<%=QueryBean.getParameter30()%>" /></div></td>
                                      </tr>
                                      <tr>
                                          <%
                                            if (QueryBean.getAquaColor().equals("off")) {
                                          %>
                                        <td><eftwc:AquaButton buttonText="Add Parameter" onClickScript="submitForm('ADD_PARAMETER')" buttonStyle="off" /></td>
                                        <td colspan="3"><eftwc:AquaButton buttonText="Remove Parameter" onClickScript="submitForm('REMOVE_PARAMETER')" buttonStyle="off" /></td>
                                          <%
                                            }
                                            else {
                                          %>
                                        <td><eftwc:AquaButton buttonText="Add Parameter" onClickScript="submitForm('ADD_PARAMETER')" /></td>
                                        <td colspan="3"><eftwc:AquaButton buttonText="Remove Parameter" onClickScript="submitForm('REMOVE_PARAMETER')" /></td>
                                          <%
                                            }
                                          %>
                                      </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td><eftwc:AquaButton buttonText="Query" onClickScript="submitForm('QUERY')" /></td>
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
                            <td class="formTitle" valign="top">Result:</td>
                          </tr>
                          <tr>
                            <td><eftwc:HighlightTextBox id="result" rows="7" size="120" text="<%=Utility.changeSpecialCharacterForPage(QueryBean.getResult())%>" /></td>
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
       <script language="javascript">
         showHideParams();
       </script>
</html>
