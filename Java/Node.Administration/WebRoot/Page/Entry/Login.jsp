<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-html.tld" prefix="html" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="LoginBean" class="Node.Web.Administration.Bean.Entry.LoginBean" scope="request" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node Admin Console</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  document.form1.act.value = action;
                  document.form1.submit();
                }
               </script>
				<style type="text/css">
			    	.HeaderText { font: 8pt Arial,Helvetica,sans-serif; color: #ffffff;}
			    	.logo { padding:0 15px 0 0;}
				</style>
	</head>
        <body leftmargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
          <form action="/Node.Administration/Page/Entry/Login.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
              	<td colspan="2" bgcolor="#0055A6" height="58px" width="300px"><a href="http://www.enfotech.com/"><img src="../../images/Header/header.gif" style="border-width:0px;" /></a></td>
              </tr>
		        <tr class="HeaderText" valign="top" style="background-color: #396EA0">
		            <td>Node Administration</td>
		            <td align="right"><a href="http://www.enfotech.com/" target="_blank" style="color:White; ">enfoTech & Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx" style="color:White;" target="_blank">Contact Us</a></td>
		        </tr>
            </table>
			<table width="100%" border="0" cellspacing="20" cellpadding="1">
				<tr valign="top">
					<td><table width="250" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td><img src="/Node.Administration/skin/images/loginblock_UpLeft.gif" width="17" height="45"></td>
								<td align="center" background="/Node.Administration/skin/images/loginblock_UpBG.gif"><img src="/Node.Administration/skin/images/loginblock_Title.gif" width="107" height="45"></td>
								<td><img src="/Node.Administration/skin/images/loginblock_UpRight.gif" width="21" height="45"></td>
							</tr>
							<tr>
								<td background="/Node.Administration/skin/images/loginblock_LeftBG.gif">&nbsp;</td>
								<td background="/Node.Administration/skin/images/loginblock_BG.gif"><table border="0" align="center" cellpadding="12" cellspacing="0" class="loginBG">
										<tr>
											<td><table border="0" cellspacing="0" cellpadding="3">
								                          <%
								                          // WI 33893
								                            boolean isResetPWD = LoginBean.isResetPWD();
								                            if (isResetPWD) {
								                          %>
													<tr>
														<td colspan="2" class="descFont">Please input your email address, and the Node will send you a new temporary password.
														</td>
													</tr>
													<%		}else{ %>
													<tr>
														<td colspan="2" class="descFont">Please enter in your username, old password, and new password.
														</td>
													</tr>
													
													<%		} %>
								                    <logic:notEqual name="LoginBean" property="message" value="">
								                      <tr>
								                        <td align="left" class="errFont"><bean:write name="LoginBean" property="message"/></td>
								                      </tr>
								                      <tr>
								                        <td>&nbsp;</td>
								                      </tr>
								                    </logic:notEqual>                                                                                                        
								                    <tr>
								                      <td align="center">
								                        <table>
								                          <%
								                          // WI 33893
								                            if (isResetPWD) {
								                          %>
									                          <tr>
									                            <td align="right" class="formFldLabel"><b>Email&nbsp;Address:</b></td>
									                            <td><eftwc:HighlightTextBox id="email" maxLength="150" size="25"/></td>
									                          </tr>								                           
									                       <%
								                            }else{
								                           %>
									                          <tr>
									                            <td align="right" class="formFldLabel"><b>User&nbsp;Name:</b></td>
									                            <td><eftwc:HighlightTextBox id="loginName" maxLength="50" /></td>
									                          </tr>
									                          <tr>
									                            <td align="right" class="formFldLabel"><b>Old&nbsp;Password:</b></td>
									                            <td><eftwc:HighlightTextBox id="loginPassword" type="password" maxLength="100" /></td>
									                          </tr>
									                          <%
									                            boolean isNewPWD = LoginBean.getChangePWD();
									                            if (isNewPWD) {
									                          %>
									                          <tr>
									                            <td align="right" class="formFldLabel"><b>New&nbsp;Password:</b></td>
									                            <td><eftwc:HighlightTextBox id="newPWD1" type="password" maxLength="100" /></td>
									                          </tr>
									                          <tr>
									                            <td align="right" class="formFldLabel"><b>Confirm&nbsp;Password:</b></td>
									                            <td><eftwc:HighlightTextBox id="newPWD2" type="password" maxLength="100" /></td>
									                          </tr>
								                           <%
									                        	}
								                            }
								                           %>
								                        </table>
								                      </td>
								                    </tr>
								                    <tr>
								                      <td>&nbsp;</td>
								                    </tr>
								                    <tr>
								                      <%
								                      	boolean isNewPWD = LoginBean.getChangePWD();
								                        if (isNewPWD) {
								                      %>
								                      <td align="left">
								                        <eftwc:AquaButton buttonText="Login" onClickScript="submitForm('CHANGE_PWD')" />
								                      </td>
								                      <td align="right">
								                        <eftwc:AquaButton buttonText="Cancel" onClickScript="submitForm('CANCEL')" />
								                      </td>
								                      <%
								                        }
								                        else if(isResetPWD){
								                      %>
								                      <td align="left">
								                        <eftwc:AquaButton buttonText="Reset" onClickScript="submitForm('RESET_PWD')" />
								                      </td>
								                      <td align="right">
								                        <eftwc:AquaButton buttonText="Cancel" onClickScript="submitForm('CANCEL')" />
								                      </td>
								                      <%
								                        }
								                        else {
								                      %>
								                      <td align="center">
								                        <eftwc:AquaButton buttonText="Login" onClickScript="submitForm('LOGIN')" />
								                      </td>
								                      <%
								                        }
								                      %>
								                    </tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
								<td background="/Node.Administration/skin/images/loginblock_RightBG.gif">&nbsp;</td>
							</tr>
							<tr>
								<td><img src="/Node.Administration/skin/images/loginblock_BtmLeft.gif" width="17" height="21"></td>
								<td background="/Node.Administration/skin/images/loginblock_BtmBG.gif"><img src="/Node.Administration/skin/images/loginblock_BtmBG.gif" width="2" height="21"></td>
								<td><img src="/Node.Administration/skin/images/loginblock_BtmRight.gif" width="21" height="21"></td>
							</tr>
						</table>
						<br>
						<table width="95%" border="0" align="center" cellpadding="5" cellspacing="1" bgcolor="#cccccc">
							<tr>
								<td bgcolor="#eaeaea">
									<table width="100%" border="0" align="center" cellpadding="4" cellspacing="0">
										<tr>
											<td colspan="2" class="cmtFont2"><strong>System Info:</strong></td>
										</tr>
										<tr>
											<td  class="cmtFont2">Version:  <%=LoginBean.getVersion()%></td>
										</tr>
										<tr>
											<td  class="cmtFont2">Build Date: <%=LoginBean.getBuildDate()%></td>
										</tr>
									</table>
								</td>
							</tr>

							<tr>
								<td bgcolor="#eaeaea"><table width="100%" border="0" align="center" cellpadding="4" cellspacing="0">
										<tr>
											<td colspan="2" class="cmtFont2"><strong>Help</strong></td>
										</tr>
										<tr>
											<td align="center" class="descFont"><img src="/Node.Administration/skin/images/1pix.gif" width="1" height="1"></td>
											<td class="descFont"><img src="/Node.Administration/skin/images/hb_ico_question.gif" width="20" height="20" border="0" align="absMiddle">
												<a href="javascript:submitForm('changePWD');">Change your password? </A>
											</td>
										</tr>
										<tr>
											<td align="center" class="descFont"><img src="/Node.Administration/skin/images/1pix.gif" width="1" height="1"></td>
											<td class="descFont"><img src="/Node.Administration/skin/images/hb_ico_question.gif" width="20" height="20" border="0" align="absMiddle">
												<a href="javascript:submitForm('resetPWD');">Forgot your password </A>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table width="95%" border="0" align="center" cellpadding="8" cellspacing="0">
							<tr>
								<td bgcolor="#ffffff" class="boxStyle"><table width="100%" border="0" cellspacing="0" cellpadding="5">
										<tr>
											<td class="cmtFont1"><span class="darkRed">Note: The Node and related systems are
													configured to work best with Internet Explorer 6.0 and above. For online
													documents, you will need Adobe Reader to view or print these materials. Please
													use the links below to obtain or update these applications as needed. </span>
											</td>
										</tr>
										<tr>
											<td><a href="http://www.microsoft.com/windows/ie/" target="_new"><img src="/Node.Administration/skin/images/logo_ie.gif" alt="Click here to download IE browser" width="91"
														height="33" border="0"></a> <a href="http://www.adobe.com/products/acrobat/readermain.html" target="_new">
													<img src="/Node.Administration/skin/images/logo_acrobat.gif" alt="Click here to download Adobe Reader" width="91"
														height="33" border="0"></a></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
					</td>
					<td bgcolor="#efefef">&nbsp;</td>
					<td width="100%">
						<p class="title1">Overview</p>
						<p class="contextFont">Welcome to the Node Login Page!</p>
						<p class="cmtFont3">
						</p>
					</td>
				</tr>
			</table>
            <!-- begin foot -->
		<table id="cc_NodeFooter" cellspacing="0" width="100%">
		    <tr>
		        <td align="left"><font style="font-family:Arial; font-size:8pt; color:#666666;">
			        Copyright © 2013 by enfoTech & Consulting Inc. All Rights Reserved. </font>
			        <br>
		        </td>
		        <td class="logo" align="right"><a href="http://www.enfotech.com" target="_blank"><img id="ctl00_footerContent_footer_Image1" src="/Node.Administration/images/TailLogo.gif" style="border-width:0px;" /></a></td>
		    </tr>
		</table>
            <script language="javascript">
              window.onfocus = function()
              {
                document.form1.loginName.focus();
              }

              document.onkeydown = function()
              {
		 if (event.keyCode==13)
		 {
                   <%if (LoginBean.getMessage().equals("Please Provide a New Password")  || LoginBean.getMessage().equals("Enter the Same Password in Both Text Boxes")) {%>
                   submitForm('CHANGE_PWD');
                   <%}
                     else {
                   %>
                   submitForm('LOGIN');
                   <%
                     }
                   %>
                   event.keyCode=0;
		 }
              }
            </script>
          </form>
        </body>
</html>
