<%@ taglib uri="/WEB-INF/struts-html.tld" prefix="html" %>
<jsp:useBean id="OperationMgrBean" class="Node2.web.OperationMgr.model.OperationMgrBean" scope="session" />
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node</title>
               <link href="/Node.Administration/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Administration/skin/css/node.css" type="text/css" rel="stylesheet" />
               <meta http-equiv="refresh" content="1;URL=/Node.Administration/Page/Documents/Download.do?tempDoc=<%=OperationMgrBean.getView()%>">
	</head>
      	<body>
          <html:form action="/Page/Entry/OperationMgr" method="POST">
            <table width="100%">
              <tr>
                <td class="formFldLabel">Transforming......<br>
                   Download will start automatically.
                </td>
              </tr>
            </table>
          </html:form>
        </body>
</html>
