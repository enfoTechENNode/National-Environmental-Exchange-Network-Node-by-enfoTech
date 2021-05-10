<jsp:useBean id="GetServicesBean" class="Node2.web.Services.GetServicesBean" scope="session" />
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  document.form1.act.value = action;
                  document.form1.submit();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Entry/GetServices.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Download Metadata File</td>
              </tr>
                    <%
                      String message = GetServicesBean.getMessage();
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
                    	  <td class="formFldLabel">Please click 'Download XML File' to download the data service registration information and save it to your hard drive.</td>
                  	</tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td><a class="wcBtn" href="#" onClick="submitForm('download')" style="cursor:hand;"><table class="wcBtn" cellspacing="0" style="filter:alpha(opacity=100);"><tr><td><img src="../../eftWC/skin/img/btn/aqua_blue_L.gif"></td><td style="background-image:url(../../eftWC/skin/img/btn/aqua_blue_BG.gif)" class="txt" nowrap="nowrap">Download XML File</td><td><img src="../../eftWC/skin/img/btn/aqua_blue_R.gif"></td></tr></table></a></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
</html>
