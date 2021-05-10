<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<xsl:output method="html"/>
	<xsl:param name="rootDir" select="''"/>
	<xsl:template match="/">
		<html>
			<head>
				<TITLE>Submission Error Report</TITLE>
				<link href="{$rootDir}skin/css/core.css" rel="stylesheet" type="text/css"/>
				<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
				<style type="text/css">.darkRed { FONT-WEIGHT: bold; COLOR: #990000 }</style>
			</head>
			<body>
          		<form action="/Node.Administration/Page/Entry/OperationMgr.do" method="POST" name="form1">
				<table border="0" align="center" cellpadding="0" cellspacing="1">
					<TBODY>
						<tr>
							<td bgcolor="#ffffff">
								<!-- <table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td background="{$rootDir}skin/images/headbar_low_bg.gif">
											<IMG src="/Node.Administration/skin/images/e2_logo_big.gif" width="125" height="35"/>
										</td>
									</tr>
								</table>
								<br/>  -->
								<!-- begin blue -->
								<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
									<tr>
										<td>
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_UpLeft.gif" width="11"/>
										</td>
										<td width="100%" background="/Node.Administration/skin/images/collapse_frm_BG.gif">
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_BG.gif" width="12"/>
										</td>
										<td>
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_UpRight.gif" width="13"/>
										</td>
									</tr>
									<tr>
										<td background="/Node.Administration/skin/images/collapse_frm_BG.gif">&#xA0;</td>
										<td class="descFont" background="/Node.Administration/skin/images/collapse_frm_BG.gif">
											<xsl:choose>
												<xsl:when test="/ErrorMessages/@submissionID=''">
													<p>A detailed report is provided below.  Please use this report to correct any possible validation warnings or errors.</p>
												</xsl:when>
												<xsl:otherwise>
													<p>A detailed report for this submission is provided below.  Please use this report to correct any warnings or errors and resubmit the data.</p>
												</xsl:otherwise>
											</xsl:choose>
										</td>
										<td background="/Node.Administration/skin/images/collapse_frm_RightBG.gif">&#xA0;</td>
									</tr>
									<tr>
										<td>
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_BtmLeft.gif" width="11"/>
										</td>
										<td background="/Node.Administration/skin/images/collapse_frm_BtmBG.gif">
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_BtmBG.gif" width="11"/>
										</td>
										<td>
											<IMG height="8" src="/Node.Administration/skin/images/collapse_frm_BtmRight.gif" width="13"/>
										</td>
									</tr>
								</table>
								<!-- end blue -->
								<br/>
								<!--begin Lab Header-->
								<xsl:for-each select="/ErrorMessages">
									<TABLE cellSpacing="0" cellPadding="0" border="0">
										<xsl:choose>
											<xsl:when test="@labStateIdentifier=''">&#xA0;</xsl:when>
											<xsl:otherwise>
												<TR>
													<TD class="formTitle" noWrap="nowrap" height="20">
														<B>Lab Cert Number:&#xA0;&#xA0;</B>
													</TD>
													<TD class="formTitle" nowrap="nowrap" height="20">
														<B>
															<FONT color="#990000">
																<xsl:value-of select="@labStateIdentifier"/>
															</FONT>
														</B>
													</TD>
												</TR>
												<tr>
													<td class="s" vAlign="middle" noWrap="nowrap" bgColor="#000000" colSpan="2">
														<IMG height="1" src="/Node.Administration/skin/images/1PIX.gif" width="1"/>
													</td>
												</tr>
												<tr>
													<td class="s" vAlign="middle" noWrap="nowrap" bgColor="#000000" colSpan="2">
														<IMG height="1" src="/Node.Administration/skin/images/1PIX.gif" width="1"/>
													</td>
												</tr>
											</xsl:otherwise>
										</xsl:choose>
										<xsl:choose>
											<xsl:when test="@submissionID=''">&#xA0;</xsl:when>
											<xsl:otherwise>
												<TR>
													<TD class="formTitle" vAlign="middle" noWrap="nowrap" height="20">
														<B>	Submission ID:&#xA0;&#xA0;</B>
													</TD>
													<TD class="formTitle" noWrap="nowrap" height="20">
														<xsl:value-of select="@submissionID"/>
													</TD>
												</TR>
												<tr>
													<td class="s" vAlign="middle" noWrap="nowrap" bgColor="#000000" colSpan="2">
														<IMG height="1" src="/Node.Administration/skin/images/1PIX.gif" width="1"/>
													</td>
												</tr>
												<tr>
													<td class="s" vAlign="middle" noWrap="nowrap" bgcolor="#000000" colSpan="2">
														<IMG height="1" src="/Node.Administration/skin/images/1PIX.gif" width="1"/>
													</td>
												</tr>
											</xsl:otherwise>
										</xsl:choose>
										<xsl:choose>
											<xsl:when test="@submissionID=''">&#xA0;</xsl:when>
											<xsl:otherwise>
												<TR>
													<TD class="formTitle" vAlign="middle" noWrap="nowrap" height="20">
														<B>	Report Form ID:&#xA0;&#xA0;</B>
													</TD>
													<TD class="formTitle" noWrap="nowrap" height="20">
														<xsl:value-of select="@reportFormID"/>
													</TD>
												</TR>
											</xsl:otherwise>
										</xsl:choose>
									</TABLE>
									<!-- end Lab Header -->
									<br/>
								</xsl:for-each>
								<!-- begin Error Detail -->
								<table borderColor="#cccccc" cellSpacing="0" borderColorDark="#ffffff" cellPadding="4" width="100%" align="left" borderColorLight="#cccccc" border="l" bgcolor="#ffffff">
									<tr>
										<td nowrap="nowrap" class="gridHead" align="center">Record ID</td>
										<td nowrap="nowrap" class="gridHead" align="center">Provided Value</td>
										<td nowrap="nowrap" class="gridHead" align="center">Error type</td>
										<td nowrap="nowrap" class="gridHead" align="center">Message</td>
									</tr>
									<xsl:for-each select="/ErrorMessages/ErrorMessage">
										<tr class="trOut">
											<td class="RowPlain" align="center">
												<xsl:choose>
													<xsl:when test="Location=''">&#xA0;</xsl:when>
													<xsl:otherwise>
														<xsl:value-of select="Location"/>
													</xsl:otherwise>
												</xsl:choose>
											</td>
											
											<td class="RowPlain" align="left">
												<xsl:choose>
													<!-- Customize for Less Than Indicator -->
													<xsl:when test="Value='' or (Value='N' and XPath='EN:Submission.EN:LabReport.EN:Sample.EN:AnalysisResultInformation.EN:AnalysisResult.EN:DetectionLevelIndicator')">&#xA0;</xsl:when>
													<xsl:when test="Value='Y' and XPath='EN:Submission.EN:LabReport.EN:Sample.EN:AnalysisResultInformation.EN:AnalysisResult.EN:DetectionLevelIndicator'">&lt;</xsl:when>
													<!-- End -->
													<xsl:otherwise>
														<xsl:value-of select="Value"/>
													</xsl:otherwise>
												</xsl:choose>
											</td>
											<td class="RowPlain" align="center">
												<xsl:choose>
													<xsl:when test="Category='warning'">
														Warning													
													</xsl:when>
													<xsl:otherwise>
														Error
													</xsl:otherwise>
												</xsl:choose>
											</td>
											<td class="RowPlain" align="left">
												<xsl:choose>
													<xsl:when test="ErrorMessageText=''">&#xA0;</xsl:when>
													<xsl:otherwise>
														<xsl:value-of select="ErrorMessageText"/>
													</xsl:otherwise>
												</xsl:choose>
											</td>
										</tr>
									</xsl:for-each>
								</table>
								<!-- end Error Detail -->
							</td>
						</tr>
					</TBODY>
				</table>
			</form>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
