<jsp:useBean id="DataWizardConfigBean" class="Node2.web.Configurations.DataWizardConfigBean" scope="session" />
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO 8859-1">
		<title>Exchange Network Node</title>
			   <link rel="stylesheet" type="text/css" href="../../ext/resources/css/ext-all.css" />
			   <link rel="stylesheet" type="text/css" href="../../ext/resources/css/portal.css" />
			   <script type="text/javascript" src="../../ext/adapter/ext/ext-base.js"></script>
			   <script type="text/javascript" src="../../ext/ext-all.js"></script>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Administration/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  document.form1.opID.value = document.form1.operation.options[document.form1.operation.selectedIndex].value;
                  if(document.form1.opID.value == null || document.form1.opID.value == ""){
                  		Ext.MessageBox.alert('Status', 'Please select an operation.', function showResult(){});
                  }else{
	                  document.form1.act.value = action;
	                  document.form1.submit();                  
                  }
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Configurations/DataWizardConfig.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <input type="hidden" name="opID" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td class="formTitle" colspan="4" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">Download / Upload DataWizard Configuration File</td>
              </tr>
                    <%
                      String message = DataWizardConfigBean.getMessage();
                      if (message != null && !message.equals("")) {
                    %>
                    <tr>
                      <td colspan="4" class="formFldLabel errFont"><br/><%=message%><br/></td>
                    </tr>
                    <%
                      }
                    %>
			  <tr>
                 <td colspan="4"><br/></td>
              </tr>                 
			  <tr>
                 <td class="formFldLabel">Select Operation:</td>
                 <td/>
                 <td/>
                 <td>
                   <select name="operation">
                     <%
                       String opID = DataWizardConfigBean.getOpID();
                       String opName = DataWizardConfigBean.getOpName();
                     %>
                     <option <%if(opName.equals("")){%>selected="selected"<%}%>></option>
                     <%
                       String[] opIDs = DataWizardConfigBean.getOpIDs();
                       String[] opNames = DataWizardConfigBean.getOpNames();
                       if (opNames != null) {
                         for (int i = 0; i < opNames.length; i++) {
                     %>
                     <option value="<%=opIDs[i]%>" <%if(opName.equals(opNames[i])){%>selected="selected"<%}%>><%=opNames[i]%></option>
                     <%
                         }
                       }
                     %>
                   </select>
                 </td>
			  </tr>
              <tr>
                <td colspan="4"><br/>
                  <table width="100%">
                  	<tr>
                    	  <td class="formFldLabel">File Location:</td>
                          <td colspan="6"><input type="file" name="uploadFile" size="50" /></td>
                  	</tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td colspan="4" >
                  <table width="100%">
                    <tr>
                      <td><a class="wcBtn" href="#" onClick="submitForm('saveUpload')" style="cursor:hand;"><table class="wcBtn" cellspacing="0" style="filter:alpha(opacity=100);"><tr><td><img src="../../eftWC/skin/img/btn/aqua_blue_L.gif"></td><td style="background-image:url(../../eftWC/skin/img/btn/aqua_blue_BG.gif)" class="txt" nowrap="nowrap">Upload</td><td><img src="../../eftWC/skin/img/btn/aqua_blue_R.gif"></td></tr></table></a></td>
                      <td><a class="wcBtn" href="#" onClick="submitForm('download')" style="cursor:hand;"><table class="wcBtn" cellspacing="0" style="filter:alpha(opacity=100);"><tr><td><img src="../../eftWC/skin/img/btn/aqua_blue_L.gif"></td><td style="background-image:url(../../eftWC/skin/img/btn/aqua_blue_BG.gif)" class="txt" nowrap="nowrap">Download XML File</td><td><img src="../../eftWC/skin/img/btn/aqua_blue_R.gif"></td></tr></table></a></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
</html>
