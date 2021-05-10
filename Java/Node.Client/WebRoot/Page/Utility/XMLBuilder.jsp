<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<%@ taglib uri="/WEB-INF/struts-html.tld" prefix="html"%>
<jsp:useBean id="XMLBuilderBean" class="Node.Web.Client.Bean.Utility.XMLBuilderBean" scope="request" />
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.Client/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Client/css/core.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Client/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="javascript" src="/Node.Client/eftWC/js/FloatDiv.js" type="text/javascript" ></script>
               <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
               <script language="JavaScript">
               	function submitForm (action)
                {
                  if(document.form1.mapFile.value == "")
                  	alert('Mapping file is missing.');
                  else if(document.form1.dbConn.value == "")
                  	alert('Database connection is missing.');
                  else
                  {
                  	//submit form
                  	waitingBlock.switchFloatDiv();
                  	document.form1.act.value = action;
                  	document.form1.submit();
                  }
                }
               </script>
        </head>
      	<body >
          <form action="/Node.Client/Page/Utility/XMLBuilder.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table align="left" width="70%" cellpadding="3" cellspacing="0">
              <tr>
                <td class="formTitle">Mapping File:</td>
                <td ><html:file property="mapFile" name="XMLBuilderBean" size="70"></html:file></td>
              </tr>
               <tr>
                <td class="formTitle" valign="top">Data Connection:</td>
                <td ><input type="text" id="dbConn" name="dbConn" size="100" value="<%=XMLBuilderBean.getDbConn()%>" /><br />
                  (example: jdbc:oracle:thin:username/password@server:1521:schema)
                </td>
              </tr>
              <tr>
                <td colspan="2">
                	<table width="100%">
                          <tr>
                          	<td width="20%" class="formTitle" valign="top" align="left">Input Key 1:</td>
                          	<td class="formTitle">Name:&nbsp;<input type="text" id="key1_name" name="key1_name" size="30" value="<%=XMLBuilderBean.getKey1_name()%>" /></td>
                                <td class="formTitle">Value:&nbsp;<input type="text" id="key1_value" name="key1_value" size="30" value="<%=XMLBuilderBean.getKey1_value()%>" /></td>
                          </tr>
                          <tr>
                          	<td width="20%" class="formTitle" valign="top" align="left">Input Key 2:</td>
                          	<td class="formTitle">Name:&nbsp;<input type="text" id="key2_name" name="key2_name" size="30" value="<%=XMLBuilderBean.getKey2_name()%>" /></td>
                                <td class="formTitle">Value:&nbsp;<input type="text" id="key2_value" name="key2_value" size="30" value="<%=XMLBuilderBean.getKey2_value()%>"/></td>
                          </tr>
                        </table>
                <td />
              </tr>
              <tr>
              	<td  colspan="2" align="right">
                        <eftwc:AquaButton buttonText="Build" onClickScript="javascript:submitForm('build')" /></td>
              </tr>
              <tr>
              	<td align="left" class="formTitle" valign="top">
                        XMLBuilder Resut:
                </td>
                <td align="left">
                        <textarea cols="110" rows="30" id="builderResult" name="builderResult" ><%=XMLBuilderBean.getBuilderResult()%></textarea>
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
