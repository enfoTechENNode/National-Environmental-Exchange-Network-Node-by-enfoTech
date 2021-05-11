<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="UploadBean" class="Node.Web.Administration.Bean.Documents.UploadBean" scope="session" />

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<title>Exchange Network Node</title>
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
	</head>
      	<body>
          <form action="/Node.Administration/Page/Documents/Upload.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Document Management - Upload</td>
                    </tr>
                    <%
                      String message = UploadBean.getMessage();
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
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle" colspan="4">Upload File</td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Document Name</td>
                            <td><eftwc:HighlightTextBox id="upDocName" size="40" text="<%=UploadBean.getUpDocName()%>" /></td>
                            <td class="formFldLabel">Document Type</td>
                            <td><eftwc:HighlightTextBox id="upDocType" text="<%=UploadBean.getUpDocType()%>" /></td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Transaction ID:</td>
                            <td><eftwc:HighlightTextBox id="transID" text="<%=UploadBean.getTransID()%>" size="40" /></td>
                            <td class="formFldLabel">Data Flow:</td>
                            <td>
                              <select name="upOpName">
                                <%
                                  String dataFlow = UploadBean.getUpOpName();
                                  String[] dataFlows = UploadBean.getOps();
                                  if (dataFlows != null) {
                                    for (int i = 0; i < dataFlows.length; i++) {
                                %>
                                <option <%if(dataFlow.equals(dataFlows[i])){%>selected="selected"<%}%>><%=dataFlows[i]%></option>
                                <%
                                    }
                                  }
                                %>
                              </select>
                            </td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">File Location</td>
                            <td colspan="3"><input type="file" name="upFile" /></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                       <td>
	                      <table width="100%">
	                           <tr>
	                            <td><eftwc:AquaButton buttonText="Upload" onClickScript="submitForm('UPLOAD')" linkBase="/Node.Administration/eftWC" /></td>
	                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
</html>
