<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="DocumentsEditBean" class="Node.Web.Administration.Bean.Documents.DocumentsEditBean" scope="session" />

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
          <form action="/Node.Administration/Page/Documents/DocumentsEdit.do" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Documents/Documents.do">Document Management</a> - <%=DocumentsEditBean.getTitle()%></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td></td>
                    </tr>
                    <%
                      String message = DocumentsEditBean.getMessage();
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
                        <table>
                          <tr>
                            <td class="formFldLabel"><B>Document Details</B>&nbsp;&nbsp;&nbsp;&nbsp;(<font color="red">*</font>&nbsp;Required Fields)</td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Note: If no file content is uploaded, the pre-existing file content will remain</td>
                          </tr>
                          <tr>
                            <td>
                              <table>
                                <tr>
                                  <td class="formFldLabel">Document Name:</td>
                                  <td><eftwc:HighlightTextBox id="name" text="<%=DocumentsEditBean.getName()%>" size="30" maxLength="200"/></td>
                                  <td class="formFldLabel">Document Type:</td>
                                  <td><eftwc:HighlightTextBox id="type" text="<%=DocumentsEditBean.getType()%>" maxLength="50"/></td>
                                  <td class="formFldLabel">Document Size:</td>
                                  <td class="formFldLabel"><%=DocumentsEditBean.getSize()%>&nbsp;KB</td>
                                </tr>
                                <tr>
                                   <td class="formFldLabel">Data Flow:</td>
                                   <td>
                                     <select name="dataFlow">
                                       <option></option>
                                     <%
                                     	String[] ops = DocumentsEditBean.getOps();
                                        String dataFlow = DocumentsEditBean.getDataFlow();
                                        if (ops != null)
                                        {
                                          for (int i = 0; i < ops.length; i++)
                                          {
                                     %>
                                       <option <%if(ops[i].equals(dataFlow)){%>selected="true"<%}%>><%=ops[i]%></option>
                                     <%
                                          }
                                        }
                                     %>
                                     </select>
                                   </td>
                                   <td class="formFldLabel">Transaction ID:</td>
                                   <td class="formFldLabel" colspan="3"><%=DocumentsEditBean.getTransID()%></td>
                                </tr>
                                <tr>
                            	  <td class="formFldLabel">Document Source:</td>
                            	  <td class="formFldLabel"><%=DocumentsEditBean.getSource()%></td>
                            	  <td class="formFldLabel">Operation/Administrator Name:</td>
                            	  <td class="formFldLabel"><%=DocumentsEditBean.getSourceName()%></td>
                                </tr>
                          	<tr>
                            	  <td class="formFldLabel">File Location:</td>
                                  <td colspan="6"><input type="file" name="upFile" size="90" /></td>
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
                  <table width="100%">
                    <tr>
                      <td><eftwc:AquaButton buttonText="Save" onClickScript="submitForm('SAVE')" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </form>
        </body>
</html>
