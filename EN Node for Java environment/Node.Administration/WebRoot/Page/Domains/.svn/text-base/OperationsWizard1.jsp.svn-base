<%@ page import="Node.Phrase, java.util.ArrayList, com.enfotech.component.UI.webControl.beans.TabControlBean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="OperationsWizardBean" class="Node.Web.Administration.Bean.Domains.OperationsWizardBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO 8859-1">
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
                function changeUsingWizard()
                {
                  index = document.form1.usingWizard.selectedIndex;
                  document.form1.selectWizard.value = document.form1.usingWizard.options[index].text;
                }
                 function changeOperationType()
                {
                  index = document.form1.OperationType.selectedIndex;
                  document.form1.operationType.value = document.form1.OperationType.options[index].text;
                }
         
                
                function toggleDefaultProcess ()
                {
                  if (document.form1.useDefault.checked)
                    Utils.hideDiv('divProcess');
                  else
                    Utils.showDiv('divProcess');
                }
                function toggleAnytime ()
                {
                  if (document.form1.anytime.checked)
                    Utils.hideDiv('divAnytime');
                  else
                    Utils.showDiv('divAnytime');
                }
                function toggleUseSubmit ()
                {
                  if (document.form1.useSubmit.checked)
                    Utils.showDiv('divUseSubmit');
                  else
                    Utils.hideDiv('divUseSubmit');
                }
                function toggleUseAuthorization ()
                {
                  if (document.form1.useAuthorization.checked)
                    Utils.showDiv('divUseAuthorization');
                  else
                    Utils.hideDiv('divUseAuthorization');
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Domains/OperationsWizard.do" method="POST" name="form1">
            <eftwc:DivCalendar dateFormat="MDY" dateSpliter="/" yearNavigate="true" />
            <input type="hidden" name="act" />
            <input type="hidden" name="selectWizard" />
            <input type="hidden" name="operationType" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Operations.do">Operation Management</a> - <%=OperationsWizardBean.getTitle()%></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td>
                        <table width="20%">
                          <tr>
                      <td></td>
                        <%
                          String title = OperationsWizardBean.getTitle();
                          if (title == null || !title.equals("New Operation")) {
                        %>
                      <td></td>
                        <%
                          }
                        %>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <%
                      String message = OperationsWizardBean.getMessage();
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
                      <td class="formFldLabel"><font color="red">*</font>&nbsp;Required Fields</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td>
                        <table>
                          <tr>
                            <td class="formTitle">Operation Details</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Operation Name:&nbsp;<font color="red">*</font></td>
                                  <td class="formFldLabel">
                                    <%
                                      if (!OperationsWizardBean.getTitle().equals("New Operation")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" disabled="true" maxLength="50" />
                                    <%
                                      }
                                      else {
                                        String nameError = OperationsWizardBean.getNameError();
                                        if (nameError != null && !nameError.equals("")) {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" errorMsg="<%=nameError%>" errorMsgCSS="errFont" errorMsgStyle="inline" maxLength="50" />
                                    <%
                                        }
                                        else {
                                    %>
                                    <eftwc:HighlightTextBox id="name" size="50" text="<%=OperationsWizardBean.getName()%>" maxLength="50" />
                                    <%
                                        }
                                      }
                                    %>
                                  </td>
                                  <td class="formFldLabel">Operation Status:&nbsp;<font color="red">*</font></td>
                                  <td>
                                    <%
                                      String statusError = OperationsWizardBean.getStatusError();
                                      if (statusError != null && !statusError.equals("")) {
                                    %>
                                    <eftwc:MessageLabel msgText="<%=statusError%>" msgCSS="errFont" msgCustomImage="/Node.Administration/eftWC/skin/img/gen/icon_err.gif" ></eftwc:MessageLabel>
                                    <%
                                      }
                                    %>
                                    <select name="status">
                                      <%
                                        String status = OperationsWizardBean.getStatus();
                                      %>
                                      <option <%if(status.equals("Running")){%>selected="selected"<%}%>>Running</option>
                                      <option <%if(status.equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                      <option <%if(status.equals("Inactive")){%>selected="selected"<%}%>>Inactive</option>
                                    </select>
                                  </td>
                                </tr>
		                       <%
		                         String isWizard = OperationsWizardBean.getIsWizard();
		                       %>
								<%if(isWizard.equalsIgnoreCase("no")){%>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Status Message:<br />
										<eftwc:HighlightTextBox id="statusMessage" rows="4" size="100%" text="<%=OperationsWizardBean.getStatusMessage()%>" />
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel" colspan="4">Operation Description:<br />
										<eftwc:HighlightTextBox id="description" rows="4" size="100%" text="<%=OperationsWizardBean.getDescription()%>" />
                                  </td>
                                </tr>
                                <%}%>
			                    <tr>
			                      <td class="formFldLabel" colspan="4">Operation Type:<br/>
			                      </td>
			                    </tr>
				                <tr>
				                  <td>
				                       <%
				                         String type = OperationsWizardBean.getType();
				                       %>
                                    <%if (!OperationsWizardBean.getTitle().equals("New Operation")) {%>
										<select name="OperationType" disabled="true" onchange="changeOperationType()">
										<%}else{%><select name="OperationType" onchange="changeOperationType()">	<%}%>
				                       <option <%if(type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)){%>selected="selected"<%}%>><%=Phrase.WEB_SERVICE_OPERATION %></option>
				                       <option <%if(type.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){%>selected="selected"<%}%>><%=Phrase.SCHEDULED_TASK_OPERATION%></option>
				                     </select>
				                  </td>
				                </tr>
			                    <tr>
			                      <td class="formFldLabel" colspan="4">Using Data Flow Wizard?<br/>
			                      </td>
			                    </tr>
				                <tr>
				                  <td>
				                       <%
				                         isWizard = OperationsWizardBean.getIsWizard();
				                       %>
										<%if(isWizard.equalsIgnoreCase("no")){%>
										<select name="usingWizard" onchange="changeUsingWizard()">
										<%}else if(OperationsWizardBean.getTitle().equals("") || OperationsWizardBean.getTitle().equalsIgnoreCase("New Operation")){%><select name="usingWizard" onchange="changeUsingWizard()">
										<%}else{%><select name="usingWizard" disabled="true" onchange="changeUsingWizard()"<%}%>
				                       <option <%if(isWizard.equalsIgnoreCase("no")){%>selected="selected"<%}%>><%="no"%></option>
				                       <option <%if(isWizard.equalsIgnoreCase("yes")){%>selected="selected"<%}%>><%="yes" %></option>
				                     </select>
				                  </td>
				                </tr>
				                <tr>
				                	<td><br/>
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
              <tr>
                <td>
                  <table width="20%">
                    <tr>
		                <td><eftwc:AquaButton buttonText="Cancel" onClickScript="submitForm('CANCEL')" /></td>
		                <td><eftwc:AquaButton buttonText="Next" onClickScript="submitForm('NEXT')" /></td>
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
