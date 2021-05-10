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
                    Utils.showDiv('paramTypeDiv' + divNum);
                    Utils.showDiv('paramEncodeDiv' + divNum);
                    Utils.showDiv('param' + divNum);
                  }
                  else {
                    Utils.hideDiv('paramTitle' + divNum);
                    Utils.hideDiv('paramTypeDiv' + divNum);
                    Utils.hideDiv('paramEncodeDiv' + divNum);
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
          <form action="/Node.Client/Page/WebMethods/Solicit.do?version=VER_20" method="POST" name="form1">
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
                                    <select name="nodeAddress3">
                                      <%
                                      	String[] clientURLs = SolicitBean.getClientURLs_V2();
                                      	boolean	isSelected = false;
                                       if (clientURLs != null) {
                                         for (int i = 0; i < clientURLs.length; i++) {
                                           if (clientURLs[i].equals(SolicitBean.getNodeAddress3()))
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
                                          if (clientURLs[i].equals(SolicitBean.getNodeAddress3())) {
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
                                  <td><eftwc:HighlightTextBox id="nodeAddress4" size="80" text="<%=SolicitBean.getNodeAddress4()%>" /></td>
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
                                        <td class="formFldLabel" nowrap="nowrap">Data Flow:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="dataFlow" size="80" text="<%=SolicitBean.getDataFlow()%>" /></td>
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
                                        <td class="formFldLabel" nowrap="nowrap">Recipients:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="recipients" size="80" text="<%=SolicitBean.getRecipients()%>" />(emails or Node URIs Separated by comma)</td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">NotificationURI:</td>
                                        <td colspan="3"><eftwc:HighlightTextBox id="notificationURI" size="80" text="<%=SolicitBean.getNotificationURI()%>" />(emails or Node URIs Separated by comma)</td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Notification Type:</td>
                                        <td colspan="3">
		                                    <select name="notificationType">
												<%
													String[] notificationType = {"None",  "Warning", "Error", "Status","All"};
													for (int i = 0; i < notificationType.length; i++) {
														if (notificationType[i].equals(SolicitBean.getNotificationType())) {
														%>
														<option selected="selected"><%=notificationType[i]%></option>
														<%
															} else {
														%>
														<option><%=notificationType[i]%></option>
														<%
														}
													}
												%>
											</select>                                  
										</td>
                                      </tr>
		                            	<% String numParams = SolicitBean.getNumParams();
		                            	if(!numParams.equalsIgnoreCase("0")){ 
		                            	%>
                                      <tr>
                                        <td><div id="paramTitle1"><eftwc:HighlightTextBox id="paramName1" size="35" text="<%=SolicitBean.getParamName1()%>"/></div></td>
		                                <td><div id="paramTypeDiv1">
		                                    <select name="paramType1">
												<%
													String[] paramType1 = { "String", "XML"};
													for (int i = 0; i < paramType1.length; i++) {
														if (paramType1[i].equalsIgnoreCase(SolicitBean.getParamType1())) {
														%>
														<option selected="selected"><%=paramType1[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType1[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv1">
		                                    <select name="paramEncode1">
												<%
													String[] paramEncode1 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode1.length; i++) {
														if (paramEncode1[i].equalsIgnoreCase(SolicitBean.getParamEncode1())) {
														%>
														<option selected="selected"><%=paramEncode1[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode1[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param1"><eftwc:HighlightTextBox id="parameter1" size="35" rows="3" text="<%=SolicitBean.getParameter1()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle2"><eftwc:HighlightTextBox id="paramName2" size="35" text="<%=SolicitBean.getParamName2()%>"/></div></td>
		                                <td><div id="paramTypeDiv2">
		                                    <select name="paramType2">
												<%
													String[] paramType2 = { "String", "XML"};
													for (int i = 0; i < paramType2.length; i++) {
														if (paramType2[i].equalsIgnoreCase(SolicitBean.getParamType2())) {
														%>
														<option selected="selected"><%=paramType2[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType2[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv2">
		                                    <select name="paramEncode2">
												<%
													String[] paramEncode2 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode2.length; i++) {
														if (paramEncode2[i].equalsIgnoreCase(SolicitBean.getParamEncode2())) {
														%>
														<option selected="selected"><%=paramEncode2[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode2[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param2"><eftwc:HighlightTextBox id="parameter2" size="35"  rows="3" text="<%=SolicitBean.getParameter2()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle3"><eftwc:HighlightTextBox id="paramName3" size="35" text="<%=SolicitBean.getParamName3()%>"/></div></td>
		                                <td><div id="paramTypeDiv3">
		                                    <select name="paramType3">
												<%
													String[] paramType3 = { "String", "XML"};
													for (int i = 0; i < paramType3.length; i++) {
														if (paramType3[i].equalsIgnoreCase(SolicitBean.getParamType3())) {
														%>
														<option selected="selected"><%=paramType3[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType3[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv3">
		                                    <select name="paramEncode3">
												<%
													String[] paramEncode3 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode3.length; i++) {
														if (paramEncode3[i].equalsIgnoreCase(SolicitBean.getParamEncode3())) {
														%>
														<option selected="selected"><%=paramEncode3[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode3[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param3"><eftwc:HighlightTextBox id="parameter3" size="35"  rows="3" text="<%=SolicitBean.getParameter3()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle4"><eftwc:HighlightTextBox id="paramName4" size="35" text="<%=SolicitBean.getParamName4()%>"/></div></td>
		                                <td><div id="paramTypeDiv4">
		                                    <select name="paramType4">
												<%
													String[] paramType4 = { "String", "XML"};
													for (int i = 0; i < paramType4.length; i++) {
														if (paramType4[i].equalsIgnoreCase(SolicitBean.getParamType4())) {
														%>
														<option selected="selected"><%=paramType4[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType4[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv4">
		                                    <select name="paramEncode4">
												<%
													String[] paramEncode4 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode4.length; i++) {
														if (paramEncode4[i].equalsIgnoreCase(SolicitBean.getParamEncode4())) {
														%>
														<option selected="selected"><%=paramEncode4[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode4[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param4"><eftwc:HighlightTextBox id="parameter4" size="35"  rows="3" text="<%=SolicitBean.getParameter4()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle5"><eftwc:HighlightTextBox id="paramName5" size="35" text="<%=SolicitBean.getParamName5()%>"/></div></td>
		                                <td><div id="paramTypeDiv5">
		                                    <select name="paramType5">
												<%
													String[] paramType5 = { "String", "XML"};
													for (int i = 0; i < paramType5.length; i++) {
														if (paramType5[i].equalsIgnoreCase(SolicitBean.getParamType5())) {
														%>
														<option selected="selected"><%=paramType5[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType5[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv5">
		                                    <select name="paramEncode5">
												<%
													String[] paramEncode5 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode5.length; i++) {
														if (paramEncode5[i].equalsIgnoreCase(SolicitBean.getParamEncode5())) {
														%>
														<option selected="selected"><%=paramEncode5[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode5[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param5"><eftwc:HighlightTextBox id="parameter5" size="35"  rows="3" text="<%=SolicitBean.getParameter5()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle6"><eftwc:HighlightTextBox id="paramName6" size="35" text="<%=SolicitBean.getParamName6()%>"/></div></td>
		                                <td><div id="paramTypeDiv6">
		                                    <select name="paramType6">
												<%
													String[] paramType6 = { "String", "XML"};
													for (int i = 0; i < paramType6.length; i++) {
														if (paramType6[i].equalsIgnoreCase(SolicitBean.getParamType6())) {
														%>
														<option selected="selected"><%=paramType6[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType6[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv6">
		                                    <select name="paramEncode6">
												<%
													String[] paramEncode6 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode6.length; i++) {
														if (paramEncode6[i].equalsIgnoreCase(SolicitBean.getParamEncode6())) {
														%>
														<option selected="selected"><%=paramEncode6[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode6[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param6"><eftwc:HighlightTextBox id="parameter6" size="35"  rows="3" text="<%=SolicitBean.getParameter6()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle7"><eftwc:HighlightTextBox id="paramName7" size="35" text="<%=SolicitBean.getParamName7()%>"/></div></td>
		                                <td><div id="paramTypeDiv7">
		                                    <select name="paramType7">
												<%
													String[] paramType7 = { "String", "XML"};
													for (int i = 0; i < paramType7.length; i++) {
														if (paramType7[i].equalsIgnoreCase(SolicitBean.getParamType7())) {
														%>
														<option selected="selected"><%=paramType7[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType7[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv7">
		                                    <select name="paramEncode7">
												<%
													String[] paramEncode7 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode7.length; i++) {
														if (paramEncode7[i].equalsIgnoreCase(SolicitBean.getParamEncode7())) {
														%>
														<option selected="selected"><%=paramEncode7[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode7[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param7"><eftwc:HighlightTextBox id="parameter7" size="35"  rows="3" text="<%=SolicitBean.getParameter7()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle8"><eftwc:HighlightTextBox id="paramName8" size="35" text="<%=SolicitBean.getParamName8()%>"/></div></td>
		                                <td><div id="paramTypeDiv8">
		                                    <select name="paramType8">
												<%
													String[] paramType8 = { "String", "XML"};
													for (int i = 0; i < paramType8.length; i++) {
														if (paramType8[i].equalsIgnoreCase(SolicitBean.getParamType8())) {
														%>
														<option selected="selected"><%=paramType8[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType8[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv8">
		                                    <select name="paramEncode8">
												<%
													String[] paramEncode8 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode8.length; i++) {
														if (paramEncode8[i].equalsIgnoreCase(SolicitBean.getParamEncode8())) {
														%>
														<option selected="selected"><%=paramEncode8[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode8[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param8"><eftwc:HighlightTextBox id="parameter8" size="35"  rows="3" text="<%=SolicitBean.getParameter8()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle9"><eftwc:HighlightTextBox id="paramName9" size="35" text="<%=SolicitBean.getParamName9()%>"/></div></td>
		                                <td><div id="paramTypeDiv9">
		                                    <select name="paramType9">
												<%
													String[] paramType9 = { "String", "XML"};
													for (int i = 0; i < paramType9.length; i++) {
														if (paramType9[i].equalsIgnoreCase(SolicitBean.getParamType9())) {
														%>
														<option selected="selected"><%=paramType9[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType9[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv9">
		                                    <select name="paramEncode9">
												<%
													String[] paramEncode9 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode9.length; i++) {
														if (paramEncode9[i].equalsIgnoreCase(SolicitBean.getParamEncode9())) {
														%>
														<option selected="selected"><%=paramEncode9[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode9[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param9"><eftwc:HighlightTextBox id="parameter9" size="35"  rows="3" text="<%=SolicitBean.getParameter9()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle10"><eftwc:HighlightTextBox id="paramName10" size="35" text="<%=SolicitBean.getParamName10()%>"/></div></td>
		                                <td><div id="paramTypeDiv10">
		                                    <select name="paramType10">
												<%
													String[] paramType10 = { "String", "XML"};
													for (int i = 0; i < paramType10.length; i++) {
														if (paramType10[i].equalsIgnoreCase(SolicitBean.getParamType10())) {
														%>
														<option selected="selected"><%=paramType10[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType10[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv10">
		                                    <select name="paramEncode10">
												<%
													String[] paramEncode10 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode10.length; i++) {
														if (paramEncode10[i].equalsIgnoreCase(SolicitBean.getParamEncode10())) {
														%>
														<option selected="selected"><%=paramEncode10[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode10[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param10"><eftwc:HighlightTextBox id="parameter10" size="35"  rows="3" text="<%=SolicitBean.getParameter10()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle11"><eftwc:HighlightTextBox id="paramName11" size="35" text="<%=SolicitBean.getParamName11()%>"/></div></td>
		                                <td><div id="paramTypeDiv11">
		                                    <select name="paramType11">
												<%
													String[] paramType11 = { "String", "XML"};
													for (int i = 0; i < paramType11.length; i++) {
														if (paramType11[i].equalsIgnoreCase(SolicitBean.getParamType11())) {
														%>
														<option selected="selected"><%=paramType11[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType11[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv11">
		                                    <select name="paramEncode11">
												<%
													String[] paramEncode11 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode11.length; i++) {
														if (paramEncode11[i].equalsIgnoreCase(SolicitBean.getParamEncode11())) {
														%>
														<option selected="selected"><%=paramEncode11[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode11[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param11"><eftwc:HighlightTextBox id="parameter11" size="35"  rows="3" text="<%=SolicitBean.getParameter11()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle12"><eftwc:HighlightTextBox id="paramName12" size="35" text="<%=SolicitBean.getParamName12()%>"/></div></td>
		                                <td><div id="paramTypeDiv12">
		                                    <select name="paramType12">
												<%
													String[] paramType12 = { "String", "XML"};
													for (int i = 0; i < paramType12.length; i++) {
														if (paramType12[i].equalsIgnoreCase(SolicitBean.getParamType12())) {
														%>
														<option selected="selected"><%=paramType12[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType12[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv12">
		                                    <select name="paramEncode12">
												<%
													String[] paramEncode12 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode12.length; i++) {
														if (paramEncode12[i].equalsIgnoreCase(SolicitBean.getParamEncode12())) {
														%>
														<option selected="selected"><%=paramEncode12[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode12[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param12"><eftwc:HighlightTextBox id="parameter12" size="35"  rows="3" text="<%=SolicitBean.getParameter12()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle13"><eftwc:HighlightTextBox id="paramName13" size="35" text="<%=SolicitBean.getParamName13()%>"/></div></td>
		                                <td><div id="paramTypeDiv13">
		                                    <select name="paramType13">
												<%
													String[] paramType13 = { "String", "XML"};
													for (int i = 0; i < paramType13.length; i++) {
														if (paramType13[i].equalsIgnoreCase(SolicitBean.getParamType13())) {
														%>
														<option selected="selected"><%=paramType13[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType13[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv13">
		                                    <select name="paramEncode13">
												<%
													String[] paramEncode13 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode13.length; i++) {
														if (paramEncode13[i].equalsIgnoreCase(SolicitBean.getParamEncode13())) {
														%>
														<option selected="selected"><%=paramEncode13[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode13[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param13"><eftwc:HighlightTextBox id="parameter13" size="35"  rows="3" text="<%=SolicitBean.getParameter13()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle14"><eftwc:HighlightTextBox id="paramName14" size="35" text="<%=SolicitBean.getParamName14()%>"/></div></td>
		                                <td><div id="paramTypeDiv14">
		                                    <select name="paramType14">
												<%
													String[] paramType14 = { "String", "XML"};
													for (int i = 0; i < paramType14.length; i++) {
														if (paramType14[i].equalsIgnoreCase(SolicitBean.getParamType14())) {
														%>
														<option selected="selected"><%=paramType14[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType14[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv14">
		                                    <select name="paramEncode14">
												<%
													String[] paramEncode14 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode14.length; i++) {
														if (paramEncode14[i].equalsIgnoreCase(SolicitBean.getParamEncode14())) {
														%>
														<option selected="selected"><%=paramEncode14[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode14[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param14"><eftwc:HighlightTextBox id="parameter14" size="35"  rows="3" text="<%=SolicitBean.getParameter14()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle15"><eftwc:HighlightTextBox id="paramName15" size="35" text="<%=SolicitBean.getParamName15()%>"/></div></td>
		                                <td><div id="paramTypeDiv15">
		                                    <select name="paramType15">
												<%
													String[] paramType15 = { "String", "XML"};
													for (int i = 0; i < paramType15.length; i++) {
														if (paramType15[i].equalsIgnoreCase(SolicitBean.getParamType15())) {
														%>
														<option selected="selected"><%=paramType15[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType15[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv15">
		                                    <select name="paramEncode15">
												<%
													String[] paramEncode15 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode15.length; i++) {
														if (paramEncode15[i].equalsIgnoreCase(SolicitBean.getParamEncode15())) {
														%>
														<option selected="selected"><%=paramEncode15[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode15[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param15"><eftwc:HighlightTextBox id="parameter15" size="35"  rows="3" text="<%=SolicitBean.getParameter15()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle16"><eftwc:HighlightTextBox id="paramName16" size="35" text="<%=SolicitBean.getParamName16()%>"/></div></td>
		                                <td><div id="paramTypeDiv16">
		                                    <select name="paramType16">
												<%
													String[] paramType16 = { "String", "XML"};
													for (int i = 0; i < paramType16.length; i++) {
														if (paramType16[i].equalsIgnoreCase(SolicitBean.getParamType16())) {
														%>
														<option selected="selected"><%=paramType16[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType16[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv16">
		                                    <select name="paramEncode16">
												<%
													String[] paramEncode16 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode16.length; i++) {
														if (paramEncode16[i].equalsIgnoreCase(SolicitBean.getParamEncode16())) {
														%>
														<option selected="selected"><%=paramEncode16[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode16[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param16"><eftwc:HighlightTextBox id="parameter16" size="35"  rows="3" text="<%=SolicitBean.getParameter16()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle17"><eftwc:HighlightTextBox id="paramName17" size="35" text="<%=SolicitBean.getParamName17()%>"/></div></td>
		                                <td><div id="paramTypeDiv17">
		                                    <select name="paramType17">
												<%
													String[] paramType17 = { "String", "XML"};
													for (int i = 0; i < paramType17.length; i++) {
														if (paramType17[i].equalsIgnoreCase(SolicitBean.getParamType17())) {
														%>
														<option selected="selected"><%=paramType17[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType17[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv17">
		                                    <select name="paramEncode17">
												<%
													String[] paramEncode17 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode17.length; i++) {
														if (paramEncode17[i].equalsIgnoreCase(SolicitBean.getParamEncode17())) {
														%>
														<option selected="selected"><%=paramEncode17[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode17[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param17"><eftwc:HighlightTextBox id="parameter17" size="35"  rows="3" text="<%=SolicitBean.getParameter17()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle18"><eftwc:HighlightTextBox id="paramName18" size="35" text="<%=SolicitBean.getParamName18()%>"/></div></td>
		                                <td><div id="paramTypeDiv18">
		                                    <select name="paramType18">
												<%
													String[] paramType18 = { "String", "XML"};
													for (int i = 0; i < paramType18.length; i++) {
														if (paramType18[i].equalsIgnoreCase(SolicitBean.getParamType18())) {
														%>
														<option selected="selected"><%=paramType18[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType18[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv18">
		                                    <select name="paramEncode18">
												<%
													String[] paramEncode18 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode18.length; i++) {
														if (paramEncode18[i].equalsIgnoreCase(SolicitBean.getParamEncode18())) {
														%>
														<option selected="selected"><%=paramEncode18[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode18[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param18"><eftwc:HighlightTextBox id="parameter18" size="35"  rows="3" text="<%=SolicitBean.getParameter18()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle19"><eftwc:HighlightTextBox id="paramName19" size="35" text="<%=SolicitBean.getParamName19()%>"/></div></td>
		                                <td><div id="paramTypeDiv19">
		                                    <select name="paramType19">
												<%
													String[] paramType19 = { "String", "XML"};
													for (int i = 0; i < paramType19.length; i++) {
														if (paramType19[i].equalsIgnoreCase(SolicitBean.getParamType19())) {
														%>
														<option selected="selected"><%=paramType19[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType19[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv19">
		                                    <select name="paramEncode19">
												<%
													String[] paramEncode19 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode19.length; i++) {
														if (paramEncode19[i].equalsIgnoreCase(SolicitBean.getParamEncode19())) {
														%>
														<option selected="selected"><%=paramEncode19[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode19[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param19"><eftwc:HighlightTextBox id="parameter19" size="35"  rows="3" text="<%=SolicitBean.getParameter19()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle20"><eftwc:HighlightTextBox id="paramName20" size="35" text="<%=SolicitBean.getParamName20()%>"/></div></td>
		                                <td><div id="paramTypeDiv20">
		                                    <select name="paramType20">
												<%
													String[] paramType20 = { "String", "XML"};
													for (int i = 0; i < paramType20.length; i++) {
														if (paramType20[i].equalsIgnoreCase(SolicitBean.getParamType20())) {
														%>
														<option selected="selected"><%=paramType20[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType20[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv20">
		                                    <select name="paramEncode20">
												<%
													String[] paramEncode20 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode20.length; i++) {
														if (paramEncode20[i].equalsIgnoreCase(SolicitBean.getParamEncode20())) {
														%>
														<option selected="selected"><%=paramEncode20[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode20[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param20"><eftwc:HighlightTextBox id="parameter20" size="35"  rows="3" text="<%=SolicitBean.getParameter20()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle21"><eftwc:HighlightTextBox id="paramName21" size="35" text="<%=SolicitBean.getParamName21()%>"/></div></td>
		                                <td><div id="paramTypeDiv21">
		                                    <select name="paramType21">
												<%
													String[] paramType21 = { "String", "XML"};
													for (int i = 0; i < paramType21.length; i++) {
														if (paramType21[i].equalsIgnoreCase(SolicitBean.getParamType21())) {
														%>
														<option selected="selected"><%=paramType21[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType21[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv21">
		                                    <select name="paramEncode21">
												<%
													String[] paramEncode21 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode21.length; i++) {
														if (paramEncode21[i].equalsIgnoreCase(SolicitBean.getParamEncode21())) {
														%>
														<option selected="selected"><%=paramEncode21[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode21[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param21"><eftwc:HighlightTextBox id="parameter21" size="35"  rows="3" text="<%=SolicitBean.getParameter21()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle22"><eftwc:HighlightTextBox id="paramName22" size="35" text="<%=SolicitBean.getParamName22()%>"/></div></td>
		                                <td><div id="paramTypeDiv22">
		                                    <select name="paramType22">
												<%
													String[] paramType22 = { "String", "XML"};
													for (int i = 0; i < paramType22.length; i++) {
														if (paramType22[i].equalsIgnoreCase(SolicitBean.getParamType22())) {
														%>
														<option selected="selected"><%=paramType22[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType22[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv22">
		                                    <select name="paramEncode22">
												<%
													String[] paramEncode22 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode22.length; i++) {
														if (paramEncode22[i].equalsIgnoreCase(SolicitBean.getParamEncode22())) {
														%>
														<option selected="selected"><%=paramEncode22[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode22[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param22"><eftwc:HighlightTextBox id="parameter22" size="35"  rows="3" text="<%=SolicitBean.getParameter22()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle23"><eftwc:HighlightTextBox id="paramName23" size="35" text="<%=SolicitBean.getParamName23()%>"/></div></td>
		                                <td><div id="paramTypeDiv23">
		                                    <select name="paramType23">
												<%
													String[] paramType23 = { "String", "XML"};
													for (int i = 0; i < paramType23.length; i++) {
														if (paramType23[i].equalsIgnoreCase(SolicitBean.getParamType23())) {
														%>
														<option selected="selected"><%=paramType23[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType23[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv23">
		                                    <select name="paramEncode23">
												<%
													String[] paramEncode23 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode23.length; i++) {
														if (paramEncode23[i].equalsIgnoreCase(SolicitBean.getParamEncode23())) {
														%>
														<option selected="selected"><%=paramEncode23[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode23[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param23"><eftwc:HighlightTextBox id="parameter23" size="35"  rows="3" text="<%=SolicitBean.getParameter23()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle24"><eftwc:HighlightTextBox id="paramName24" size="35" text="<%=SolicitBean.getParamName24()%>"/></div></td>
		                                <td><div id="paramTypeDiv24">
		                                    <select name="paramType24">
												<%
													String[] paramType24 = { "String", "XML"};
													for (int i = 0; i < paramType24.length; i++) {
														if (paramType24[i].equalsIgnoreCase(SolicitBean.getParamType24())) {
														%>
														<option selected="selected"><%=paramType24[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType24[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv24">
		                                    <select name="paramEncode24">
												<%
													String[] paramEncode24 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode24.length; i++) {
														if (paramEncode24[i].equalsIgnoreCase(SolicitBean.getParamEncode24())) {
														%>
														<option selected="selected"><%=paramEncode24[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode24[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param24"><eftwc:HighlightTextBox id="parameter24" size="35"  rows="3" text="<%=SolicitBean.getParameter24()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle25"><eftwc:HighlightTextBox id="paramName25" size="35" text="<%=SolicitBean.getParamName25()%>"/></div></td>
		                                <td><div id="paramTypeDiv25">
		                                    <select name="paramType25">
												<%
													String[] paramType25 = { "String", "XML"};
													for (int i = 0; i < paramType25.length; i++) {
														if (paramType25[i].equalsIgnoreCase(SolicitBean.getParamType25())) {
														%>
														<option selected="selected"><%=paramType25[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType25[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv25">
		                                    <select name="paramEncode25">
												<%
													String[] paramEncode25 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode25.length; i++) {
														if (paramEncode25[i].equalsIgnoreCase(SolicitBean.getParamEncode25())) {
														%>
														<option selected="selected"><%=paramEncode25[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode25[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param25"><eftwc:HighlightTextBox id="parameter25" size="35"  rows="3" text="<%=SolicitBean.getParameter25()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle26"><eftwc:HighlightTextBox id="paramName26" size="35" text="<%=SolicitBean.getParamName26()%>"/></div></td>
		                                <td><div id="paramTypeDiv26">
		                                    <select name="paramType26">
												<%
													String[] paramType26 = { "String", "XML"};
													for (int i = 0; i < paramType26.length; i++) {
														if (paramType26[i].equalsIgnoreCase(SolicitBean.getParamType26())) {
														%>
														<option selected="selected"><%=paramType26[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType26[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv26">
		                                    <select name="paramEncode26">
												<%
													String[] paramEncode26 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode26.length; i++) {
														if (paramEncode26[i].equalsIgnoreCase(SolicitBean.getParamEncode26())) {
														%>
														<option selected="selected"><%=paramEncode26[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode26[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param26"><eftwc:HighlightTextBox id="parameter26" size="35"  rows="3" text="<%=SolicitBean.getParameter26()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle27"><eftwc:HighlightTextBox id="paramName27" size="35" text="<%=SolicitBean.getParamName27()%>"/></div></td>
		                                <td><div id="paramTypeDiv27">
		                                    <select name="paramType27">
												<%
													String[] paramType27 = { "String", "XML"};
													for (int i = 0; i < paramType27.length; i++) {
														if (paramType27[i].equalsIgnoreCase(SolicitBean.getParamType27())) {
														%>
														<option selected="selected"><%=paramType27[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType27[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv27">
		                                    <select name="paramEncode27">
												<%
													String[] paramEncode27 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode27.length; i++) {
														if (paramEncode27[i].equalsIgnoreCase(SolicitBean.getParamEncode27())) {
														%>
														<option selected="selected"><%=paramEncode27[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode27[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param27"><eftwc:HighlightTextBox id="parameter27" size="35"  rows="3" text="<%=SolicitBean.getParameter27()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle28"><eftwc:HighlightTextBox id="paramName28" size="35" text="<%=SolicitBean.getParamName28()%>"/></div></td>
		                                <td><div id="paramTypeDiv28">
		                                    <select name="paramType28">
												<%
													String[] paramType28 = { "String", "XML"};
													for (int i = 0; i < paramType28.length; i++) {
														if (paramType28[i].equalsIgnoreCase(SolicitBean.getParamType28())) {
														%>
														<option selected="selected"><%=paramType28[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType28[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv28">
		                                    <select name="paramEncode28">
												<%
													String[] paramEncode28 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode28.length; i++) {
														if (paramEncode28[i].equalsIgnoreCase(SolicitBean.getParamEncode28())) {
														%>
														<option selected="selected"><%=paramEncode28[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode28[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param28"><eftwc:HighlightTextBox id="parameter28" size="35"  rows="3" text="<%=SolicitBean.getParameter28()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle29"><eftwc:HighlightTextBox id="paramName29" size="35" text="<%=SolicitBean.getParamName29()%>"/></div></td>
		                                <td><div id="paramTypeDiv29">
		                                    <select name="paramType29">
												<%
													String[] paramType29 = { "String", "XML"};
													for (int i = 0; i < paramType29.length; i++) {
														if (paramType29[i].equalsIgnoreCase(SolicitBean.getParamType29())) {
														%>
														<option selected="selected"><%=paramType29[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType29[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv29">
		                                    <select name="paramEncode29">
												<%
													String[] paramEncode29 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode29.length; i++) {
														if (paramEncode29[i].equalsIgnoreCase(SolicitBean.getParamEncode29())) {
														%>
														<option selected="selected"><%=paramEncode29[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode29[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param29"><eftwc:HighlightTextBox id="parameter29" size="35"  rows="3" text="<%=SolicitBean.getParameter29()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle30"><eftwc:HighlightTextBox id="paramName30" size="35" text="<%=SolicitBean.getParamName30()%>"/></div></td>
		                                <td><div id="paramTypeDiv30">
		                                    <select name="paramType30">
												<%
													String[] paramType30 = { "String", "XML"};
													for (int i = 0; i < paramType30.length; i++) {
														if (paramType30[i].equalsIgnoreCase(SolicitBean.getParamType30())) {
														%>
														<option selected="selected"><%=paramType30[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType30[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv30">
		                                    <select name="paramEncode30">
												<%
													String[] paramEncode30 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode30.length; i++) {
														if (paramEncode30[i].equalsIgnoreCase(SolicitBean.getParamEncode30())) {
														%>
														<option selected="selected"><%=paramEncode30[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode30[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param30"><eftwc:HighlightTextBox id="parameter30" size="35"  rows="3" text="<%=SolicitBean.getParameter30()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle31"><eftwc:HighlightTextBox id="paramName31" size="35" text="<%=SolicitBean.getParamName31()%>"/></div></td>
		                                <td><div id="paramTypeDiv31">
		                                    <select name="paramType31">
												<%
													String[] paramType31 = { "String", "XML"};
													for (int i = 0; i < paramType31.length; i++) {
														if (paramType31[i].equalsIgnoreCase(SolicitBean.getParamType31())) {
														%>
														<option selected="selected"><%=paramType31[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType31[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv31">
		                                    <select name="paramEncode31">
												<%
													String[] paramEncode31 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode31.length; i++) {
														if (paramEncode31[i].equalsIgnoreCase(SolicitBean.getParamEncode31())) {
														%>
														<option selected="selected"><%=paramEncode31[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode31[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param31"><eftwc:HighlightTextBox id="parameter31" size="35"  rows="3" text="<%=SolicitBean.getParameter31()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle32"><eftwc:HighlightTextBox id="paramName32" size="35" text="<%=SolicitBean.getParamName32()%>"/></div></td>
		                                <td><div id="paramTypeDiv32">
		                                    <select name="paramType32">
												<%
													String[] paramType32 = { "String", "XML"};
													for (int i = 0; i < paramType32.length; i++) {
														if (paramType32[i].equalsIgnoreCase(SolicitBean.getParamType32())) {
														%>
														<option selected="selected"><%=paramType32[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType32[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv32">
		                                    <select name="paramEncode32">
												<%
													String[] paramEncode32 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode32.length; i++) {
														if (paramEncode32[i].equalsIgnoreCase(SolicitBean.getParamEncode32())) {
														%>
														<option selected="selected"><%=paramEncode32[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode32[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param32"><eftwc:HighlightTextBox id="parameter32" size="35"  rows="3" text="<%=SolicitBean.getParameter32()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle33"><eftwc:HighlightTextBox id="paramName33" size="35" text="<%=SolicitBean.getParamName33()%>"/></div></td>
		                                <td><div id="paramTypeDiv33">
		                                    <select name="paramType33">
												<%
													String[] paramType33 = { "String", "XML"};
													for (int i = 0; i < paramType33.length; i++) {
														if (paramType33[i].equalsIgnoreCase(SolicitBean.getParamType33())) {
														%>
														<option selected="selected"><%=paramType33[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType33[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv33">
		                                    <select name="paramEncode33">
												<%
													String[] paramEncode33 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode33.length; i++) {
														if (paramEncode33[i].equalsIgnoreCase(SolicitBean.getParamEncode33())) {
														%>
														<option selected="selected"><%=paramEncode33[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode33[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param33"><eftwc:HighlightTextBox id="parameter33" size="35"  rows="3" text="<%=SolicitBean.getParameter33()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle34"><eftwc:HighlightTextBox id="paramName34" size="35" text="<%=SolicitBean.getParamName34()%>"/></div></td>
		                                <td><div id="paramTypeDiv34">
		                                    <select name="paramType34">
												<%
													String[] paramType34 = { "String", "XML"};
													for (int i = 0; i < paramType34.length; i++) {
														if (paramType34[i].equalsIgnoreCase(SolicitBean.getParamType34())) {
														%>
														<option selected="selected"><%=paramType34[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType34[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv34">
		                                    <select name="paramEncode34">
												<%
													String[] paramEncode34 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode34.length; i++) {
														if (paramEncode34[i].equalsIgnoreCase(SolicitBean.getParamEncode34())) {
														%>
														<option selected="selected"><%=paramEncode34[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode34[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param34"><eftwc:HighlightTextBox id="parameter34" size="35"  rows="3" text="<%=SolicitBean.getParameter34()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle35"><eftwc:HighlightTextBox id="paramName35" size="35" text="<%=SolicitBean.getParamName35()%>"/></div></td>
		                                <td><div id="paramTypeDiv35">
		                                    <select name="paramType35">
												<%
													String[] paramType35 = { "String", "XML"};
													for (int i = 0; i < paramType35.length; i++) {
														if (paramType35[i].equalsIgnoreCase(SolicitBean.getParamType35())) {
														%>
														<option selected="selected"><%=paramType35[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType35[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv35">
		                                    <select name="paramEncode35">
												<%
													String[] paramEncode35 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode35.length; i++) {
														if (paramEncode35[i].equalsIgnoreCase(SolicitBean.getParamEncode35())) {
														%>
														<option selected="selected"><%=paramEncode35[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode35[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param35"><eftwc:HighlightTextBox id="parameter35" size="35"  rows="3" text="<%=SolicitBean.getParameter35()%>" /></div></td>
                                      </tr>
                                      <tr>
                                        <td><div id="paramTitle36"><eftwc:HighlightTextBox id="paramName36" size="35" text="<%=SolicitBean.getParamName36()%>"/></div></td>
		                                <td><div id="paramTypeDiv36">
		                                    <select name="paramType36">
												<%
													String[] paramType36 = { "String", "XML"};
													for (int i = 0; i < paramType36.length; i++) {
														if (paramType36[i].equalsIgnoreCase(SolicitBean.getParamType36())) {
														%>
														<option selected="selected"><%=paramType36[i]%></option>
														<%
															} else {
														%>
														<option><%=paramType36[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
		                                <td><div id="paramEncodeDiv36">
		                                    <select name="paramEncode36">
												<%
													String[] paramEncode36 = {"None","Base64", "ZIP", "Encrypt", "Digest", "XML"};
													for (int i = 0; i < paramEncode36.length; i++) {
														if (paramEncode36[i].equalsIgnoreCase(SolicitBean.getParamEncode36())) {
														%>
														<option selected="selected"><%=paramEncode36[i]%></option>
														<%
															} else {
														%>
														<option><%=paramEncode36[i]%></option>
														<%
														}
													}
												%>
											</select> 
											</div>                                 
		                                </td>
                                        <td><div id="param36"><eftwc:HighlightTextBox id="parameter36" size="35"  rows="3" text="<%=SolicitBean.getParameter36()%>" /></div></td>
                                      </tr>
                                      <%}else{%>
                                      <tr>
                                      <td><br/>
                                      </td>
                                      </tr>
                                      <%}
                                       %>
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
