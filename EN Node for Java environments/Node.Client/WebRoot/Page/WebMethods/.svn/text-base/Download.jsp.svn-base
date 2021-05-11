<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<%@ page import="Node.WebServices.Document.ClsNodeDocument" %>
<jsp:useBean id="DownloadBean" class="Node.Web.Client.Bean.WebMethods.DownloadBean" scope="session" />

<html>
	<head>
		<meta http-Equiv="Cache-Control" Content="no-cache">
		<meta http-Equiv="Pragma" Content="no-cache">
		<meta http-Equiv="Expires" Content="0">
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Exchange Network Node</title>
               <link href="/Node.Client/eftWC/skin/css/core.css" type="text/css" rel="stylesheet" />
               <link href="/Node.Client/css/core.css" type="text/css" rel="stylesheet" />
               <script language="javascript" src="/Node.Client/eftWC/js/Utils.js" type="text/javascript"></script>
               <script language="javascript" src="/Node.Client/eftWC/js/FloatDiv.js" type="text/javascript" ></script>
               <script language="JavaScript">
               	function submitForm (action)
                {
                  waitingBlock.switchFloatDiv();
                  document.form1.act.value = action;
                  document.form1.submit();
                }
                function openDownload(docID)
                {
                  popWin = open('/Node.Client/Page/WebMethods/Download/Download.do?doc='+docID,'Download','status=no,resizable=no,width=500,height=120');
                  popWin.focus();
                }
               </script>
        </head>
      	<body >
          <form action="/Node.Client/Page/WebMethods/Download.do?version=VER_11" method="POST" name="form1" enctype="multipart/form-data">
            <input type="hidden" name="act" />
            <table class="formFldLabel" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Client/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Download</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%" frame="box">
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td class="formTitle">Node Address</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Node Address:</td>
                                  <td>
                                    <select name="nodeAddress1">
                                      <%
                                      	String[] clientURLs = DownloadBean.getClientURLs();
                                      	boolean	isSelected = false;
                                       if (clientURLs != null) {
                                         for (int i = 0; i < clientURLs.length; i++) {
                                           if (clientURLs[i].equals(DownloadBean.getNodeAddress1()))
                                             isSelected = true;
                                         }
                                       }
                                       if (isSelected) {
                                       %>
                                       <option></option>
                                       <%
                                       }
                                       else {
                                       %>
                                       <option selected="selected"></option>
                                       <%
                                       }
                                       if (clientURLs != null) {
                                      	for (int i = 0; i < clientURLs.length; i++) {
                                          if (clientURLs[i].equals(DownloadBean.getNodeAddress1())) {
                                      %>
                                      <option selected="selected"><%=clientURLs[i]%></option>
                                      <%
                                    	  }
                                          else {
                                      %>
                                      <option><%=clientURLs[i]%></option>
                                      <%
                                      	  }
                                      	}
                                       }
                                      %>
                                    </select>
                                  </td>
                                </tr>
                                <tr>
                                  <td class="formFldLabel">or:</td>
                                  <td><eftwc:HighlightTextBox id="nodeAddress2" size="80" text="<%=DownloadBean.getNodeAddress2()%>" /></td>
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
                            <td class="formTitle">Download Parameters</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Security Token:</td>
                                        <td><eftwc:HighlightTextBox id="token" size="80" text="<%=DownloadBean.getToken()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Transaction ID:</td>
                                        <td><eftwc:HighlightTextBox id="transID" size="80" text="<%=DownloadBean.getTransID()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">Dataflow:</td>
                                        <td><eftwc:HighlightTextBox id="dataflow" size="40" text="<%=DownloadBean.getDataflow()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">File Name:</td>
                                        <td><eftwc:HighlightTextBox id="file1" size="40" text="<%=DownloadBean.getFile1()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">File Name:</td>
                                        <td><eftwc:HighlightTextBox id="file2" size="40" text="<%=DownloadBean.getFile2()%>" /></td>
                                      </tr>
                                      <tr>
                                        <td class="formFldLabel" nowrap="nowrap">File Name:</td>
                                        <td><eftwc:HighlightTextBox id="file3" size="40" text="<%=DownloadBean.getFile3()%>" /></td>
                                      </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td><eftwc:AquaButton buttonText="Download" onClickScript="submitForm('DOWNLOAD')" /></td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <table width="30%">
                          <tr>
                            <td class="formTitle" valign="top">Result:</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <%
                                  String error = DownloadBean.getError();
                                  if (error != null && !error.trim().equals(""))
                                  {
                                %>
                                <tr>
                                  <td class="formFldLabel"><%=error%></td>
                                </tr>
                                <%
                                  }
                                  ClsNodeDocument[] docs = DownloadBean.getDownloadDocs();
                                  if (docs != null) {
                                  for (int i = 0; i < docs.length; i++) {
                                %>
                                <tr>
                                  <td class="formFldLabel"><a href="javascript:openDownload('<%=i%>')"><%=docs[i].getName()%></a></td>
                                </tr>
                                <%
                                    }
                                  }
                                %>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
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
