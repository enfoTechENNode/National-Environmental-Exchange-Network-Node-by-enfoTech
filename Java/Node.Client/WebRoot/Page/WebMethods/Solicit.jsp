<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="SolicitBean" class="Node.Web.Client.Bean.WebMethods.SolicitBean" scope="session" />

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
                  var num = <%=SolicitBean.getNumParams()%>;
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
                  testDiv(num, 31);
                  testDiv(num, 32);
                  testDiv(num, 33);
                  testDiv(num, 34);
                  testDiv(num, 35);
                  testDiv(num, 36);
                }
                function testDiv (num, divNum)
                {
                  if (num >= divNum) {
                    Utils.showDiv('paramTitle' + divNum);
                    Utils.showDiv('param' + divNum);
                  }
                  else {
                    Utils.hideDiv('paramTitle' + divNum);
                    Utils.hideDiv('param' + divNum);
                  }
                }
                <%
                String error = SolicitBean.getError();
                if (error != null && !error.equals("")) { %>
                     alert('<%=error%>');
                <% } %>
               </script>
        </head>
      	<body >
          <form action="/Node.Client/Page/WebMethods/Solicit.do?version=VER_11" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Client/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Solicit</td>
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
                                      	String[] clientURLs = SolicitBean.getClientURLs();
                                      	boolean	isSelected = false;
                                       if (clientURLs != null) {
                                         for (int i = 0; i < clientURLs.length; i++) {
                                           if (clientURLs[i].equals(SolicitBean.getNodeAddress1()))
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
                                          if (clientURLs[i].equals(SolicitBean.getNodeAddress1())) {
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
                                  <td><eftwc:HighlightTextBox id="nodeAddress2" size="80" text="<%=SolicitBean.getNodeAddress2()%>" /></td>
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
                            <td class="formTitle">Solicit Parameters</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Security Token:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="token" size="80" text="<%=SolicitBean.getToken()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Return URL:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="returnURL" size="80" text="<%=SolicitBean.getReturnURL()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Request Name:</td>
                                        <td>
                                          <select name="request1" onchange="submitForm('')">
                                            <%
                                              String request1 = SolicitBean.getRequest1();
                                            %>
                                            <option <%if(request1 == null || request1.equals("")){%>selected="selected"<%}%>></option>
                                            <%
                                              String[] availableRequests = SolicitBean.getAvailableRequests();
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
                                        <td><eftwc:HighlightTextBox id="request2" size="35" text="<%=SolicitBean.getRequest2()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle1"><%=SolicitBean.getParamName1()%></div></td>
                                        <td><div id="param1"><eftwc:HighlightTextBox id="parameter1" size="35" text="<%=SolicitBean.getParameter1()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle2"><%=SolicitBean.getParamName2()%></div></td>
                                        <td><div id="param2"><eftwc:HighlightTextBox id="parameter2" size="35" text="<%=SolicitBean.getParameter2()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle3"><%=SolicitBean.getParamName3()%></div></td>
                                        <td><div id="param3"><eftwc:HighlightTextBox id="parameter3" size="35" text="<%=SolicitBean.getParameter3()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle4"><%=SolicitBean.getParamName4()%></div></td>
                                        <td><div id="param4"><eftwc:HighlightTextBox id="parameter4" size="35" text="<%=SolicitBean.getParameter4()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle5"><%=SolicitBean.getParamName5()%></div></td>
                                        <td><div id="param5"><eftwc:HighlightTextBox id="parameter5" size="35" text="<%=SolicitBean.getParameter5()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle6"><%=SolicitBean.getParamName6()%></div></td>
                                        <td><div id="param6"><eftwc:HighlightTextBox id="parameter6" size="35" text="<%=SolicitBean.getParameter6()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle7"><%=SolicitBean.getParamName7()%></div></td>
                                        <td><div id="param7"><eftwc:HighlightTextBox id="parameter7" size="35" text="<%=SolicitBean.getParameter7()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle8"><%=SolicitBean.getParamName8()%></div></td>
                                        <td><div id="param8"><eftwc:HighlightTextBox id="parameter8" size="35" text="<%=SolicitBean.getParameter8()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle9"><%=SolicitBean.getParamName9()%></div></td>
                                        <td><div id="param9"><eftwc:HighlightTextBox id="parameter9" size="35" text="<%=SolicitBean.getParameter9()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle10"><%=SolicitBean.getParamName10()%></div></td>
                                        <td><div id="param10"><eftwc:HighlightTextBox id="parameter10" size="35" text="<%=SolicitBean.getParameter10()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle11"><%=SolicitBean.getParamName11()%></div></td>
                                        <td><div id="param11"><eftwc:HighlightTextBox id="parameter11" size="35" text="<%=SolicitBean.getParameter11()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle12"><%=SolicitBean.getParamName12()%></div></td>
                                        <td><div id="param12"><eftwc:HighlightTextBox id="parameter12" size="35" text="<%=SolicitBean.getParameter12()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle13"><%=SolicitBean.getParamName13()%></div></td>
                                        <td><div id="param13"><eftwc:HighlightTextBox id="parameter13" size="35" text="<%=SolicitBean.getParameter13()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle14"><%=SolicitBean.getParamName14()%></div></td>
                                        <td><div id="param14"><eftwc:HighlightTextBox id="parameter14" size="35" text="<%=SolicitBean.getParameter14()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle15"><%=SolicitBean.getParamName15()%></div></td>
                                        <td><div id="param15"><eftwc:HighlightTextBox id="parameter15" size="35" text="<%=SolicitBean.getParameter15()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle16"><%=SolicitBean.getParamName16()%></div></td>
                                        <td><div id="param16"><eftwc:HighlightTextBox id="parameter16" size="35" text="<%=SolicitBean.getParameter16()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle17"><%=SolicitBean.getParamName17()%></div></td>
                                        <td><div id="param17"><eftwc:HighlightTextBox id="parameter17" size="35" text="<%=SolicitBean.getParameter17()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle18"><%=SolicitBean.getParamName18()%></div></td>
                                        <td><div id="param18"><eftwc:HighlightTextBox id="parameter18" size="35" text="<%=SolicitBean.getParameter18()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle19"><%=SolicitBean.getParamName19()%></div></td>
                                        <td><div id="param19"><eftwc:HighlightTextBox id="parameter19" size="35" text="<%=SolicitBean.getParameter19()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle20"><%=SolicitBean.getParamName20()%></div></td>
                                        <td><div id="param20"><eftwc:HighlightTextBox id="parameter20" size="35" text="<%=SolicitBean.getParameter20()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle21"><%=SolicitBean.getParamName21()%></div></td>
                                        <td><div id="param21"><eftwc:HighlightTextBox id="parameter21" size="35" text="<%=SolicitBean.getParameter21()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle22"><%=SolicitBean.getParamName22()%></div></td>
                                        <td><div id="param22"><eftwc:HighlightTextBox id="parameter22" size="35" text="<%=SolicitBean.getParameter22()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle23"><%=SolicitBean.getParamName23()%></div></td>
                                        <td><div id="param23"><eftwc:HighlightTextBox id="parameter23" size="35" text="<%=SolicitBean.getParameter23()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle24"><%=SolicitBean.getParamName24()%></div></td>
                                        <td><div id="param24"><eftwc:HighlightTextBox id="parameter24" size="35" text="<%=SolicitBean.getParameter24()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle25"><%=SolicitBean.getParamName25()%></div></td>
                                        <td><div id="param25"><eftwc:HighlightTextBox id="parameter25" size="35" text="<%=SolicitBean.getParameter25()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle26"><%=SolicitBean.getParamName26()%></div></td>
                                        <td><div id="param26"><eftwc:HighlightTextBox id="parameter26" size="35" text="<%=SolicitBean.getParameter26()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle27"><%=SolicitBean.getParamName27()%></div></td>
                                        <td><div id="param27"><eftwc:HighlightTextBox id="parameter27" size="35" text="<%=SolicitBean.getParameter27()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle28"><%=SolicitBean.getParamName28()%></div></td>
                                        <td><div id="param28"><eftwc:HighlightTextBox id="parameter28" size="35" text="<%=SolicitBean.getParameter28()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle29"><%=SolicitBean.getParamName29()%></div></td>
                                        <td><div id="param29"><eftwc:HighlightTextBox id="parameter29" size="35" text="<%=SolicitBean.getParameter29()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle30"><%=SolicitBean.getParamName30()%></div></td>
                                        <td><div id="param30"><eftwc:HighlightTextBox id="parameter30" size="35" text="<%=SolicitBean.getParameter30()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle31"><%=SolicitBean.getParamName31()%></div></td>
                                        <td><div id="param31"><eftwc:HighlightTextBox id="parameter31" size="35" text="<%=SolicitBean.getParameter31()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle32"><%=SolicitBean.getParamName32()%></div></td>
                                        <td><div id="param32"><eftwc:HighlightTextBox id="parameter32" size="35" text="<%=SolicitBean.getParameter32()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle33"><%=SolicitBean.getParamName33()%></div></td>
                                        <td><div id="param33"><eftwc:HighlightTextBox id="parameter33" size="35" text="<%=SolicitBean.getParameter33()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle34"><%=SolicitBean.getParamName34()%></div></td>
                                        <td><div id="param34"><eftwc:HighlightTextBox id="parameter34" size="35" text="<%=SolicitBean.getParameter34()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle35"><%=SolicitBean.getParamName35()%></div></td>
                                        <td><div id="param35"><eftwc:HighlightTextBox id="parameter35" size="35" text="<%=SolicitBean.getParameter35()%>" /></div></td>
                                        <td class="formFldLabel" nowrap="nowrap"><div id="paramTitle36"><%=SolicitBean.getParamName36()%></div></td>
                                        <td><div id="param36"><eftwc:HighlightTextBox id="parameter36" size="35" text="<%=SolicitBean.getParameter36()%>" /></div></td>
                                      </tr>
                                      <tr>
                                          <%
                                            if (SolicitBean.getAquaColor().equals("off")) {
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
                                  <td><eftwc:AquaButton buttonText="Solicit" onClickScript="submitForm('SOLICIT')" /></td>
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
                            <td><eftwc:HighlightTextBox id="result" text="<%=SolicitBean.getResult()%>" size="80" /></td>
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
