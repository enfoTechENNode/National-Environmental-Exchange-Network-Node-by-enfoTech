<jsp:useBean id="OperationMgrBean" class="Node2.web.OperationMgr.model.OperationMgrBean" scope="session" />
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  waitingBlock.switchFloatDiv();
                  document.form1.act.value = action;
                  document.form1.submit();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Entry/OperationMgr.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Upload Submission File</td>
              </tr>
                    <%
                      String message = OperationMgrBean.getMessage();
                      if (message != null && !message.equals("")) {
                    %>
                    <tr>
                      <td class="formFldLabel errFont"><br/><%=message%><br/></td>
                    </tr>
                    <%
                      }
                    %>
              <tr>
                <td><br/>
                  <table width="100%">
                  	<tr>
                    	  <td class="formFldLabel">File Location:</td>
                          <td colspan="6"><input type="file" name="uploadFile" size="50" /></td>
                  	</tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td><a class="wcBtn" href="#" onClick="submitForm('validateSubmissionFile')" style="cursor:hand;"><table class="wcBtn" cellspacing="0" style="filter:alpha(opacity=100);"><tr><td><img src="../../eftWC/skin/img/btn/aqua_blue_L.gif"></td><td style="background-image:url(../../eftWC/skin/img/btn/aqua_blue_BG.gif)" class="txt" nowrap="nowrap">Upload</td><td><img src="../../eftWC/skin/img/btn/aqua_blue_R.gif"></td></tr></table></a></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
          <!-- begin of wait block -->
          <link href="/Node.Administration/eftWC/skin/css/core.css" rel="stylesheet" type="text/css"/>
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
                  <p><img src="/Node.Administration/eftWC/skin/img/gen/wait_bar.gif"/></p>
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
