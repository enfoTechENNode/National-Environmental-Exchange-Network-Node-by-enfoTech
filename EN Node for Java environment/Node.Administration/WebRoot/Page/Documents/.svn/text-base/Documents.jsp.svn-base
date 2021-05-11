<%@ page import="Node.Biz.Administration.Document,Node.Phrase" %>
<%@ taglib uri="/WEB-INF/struts-bean.tld" prefix="bean" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="DocumentsBean" class="Node.Web.Administration.Bean.Documents.DocumentsBean" scope="session" />


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
                function openDownload(docID)
                {
                  popWin = open('/Node.Administration/Page/Documents/Download.do?doc='+docID,'Download','status=no,resizable=no,width=500,height=120');
                  popWin.focus();
                }
              	</script>
	</head>
      	<body>
          <form action="/Node.Administration/Page/Documents/Documents.do" method="POST" name="form1">
            <eftwc:DivCalendar dateFormat="YMD" dateSpliter="-" yearNavigate="true" />
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;Document Management</td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td>
                  <table width="100%" frame="box">
                    <tr>
                      <td>
                        <!-- Searching -->
                        <table>
                          <tr>
                            <td class="formTitle" colspan="6">Searching Criteria</td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Document Name:</td>
                            <td><eftwc:HighlightTextBox id="docName" maxLength="200" text="<%=DocumentsBean.getDocName()%>"/></td>
                            <td class="formFldLabel">Transaction ID:</td>
                            <td><eftwc:HighlightTextBox id="transID" maxLength="50" text="<%=DocumentsBean.getTransID()%>"/></td>
                            <td class="formFldLabel">Domain Name:</td>
                            <td>
                              <select name="domainName">
                                <%
                                  String domain = DocumentsBean.getDomainName();
                                %>
                                <option <%if(domain.equals("")){%>selected="selected"<%}%>></option>
                                <%
                                  String[] domains = DocumentsBean.getDomainNames();
                                  if (domains != null) {
                                    for (int i = 0; i < domains.length; i++) {
                                %>
                                <option <%if(domain.equals(domains[i])){%>selected="selected"<%}%>><%=domains[i]%></option>
                                <%
                                    }
                                  }
                                %>
                              </select>
                            </td>
                          </tr>
                          <tr>
                            <td class="formFldLabel">Operation Name:</td>
                            <td>
                              <select name="opName">
                                <%
                                  String opName = DocumentsBean.getOpName();
                                %>
                                <option <%if(opName.equals("")){%>selected="selected"<%}%>></option>
                                <%
                                  String[] opNames = DocumentsBean.getOpNames();
                                  if (opNames != null) {
                                    for (int i = 0; i < opNames.length; i++) {
                                %>
                                <option <%if(opName.equals(opNames[i])){%>selected="selected"<%}%>><%=opNames[i]%></option>
                                <%
                                    }
                                  }
                                %>
                              </select>
                            </td>
                            <td class="formFldLabel">Date Submitted Range:</td>
                            <td><eftwc:DatePicker id="beginDate" text="<%=DocumentsBean.getBeginDate()%>" linkBase="/Node.Administration/eftWC" /></td>
                            <td colspan="2"><eftwc:DatePicker id="endDate" text="<%=DocumentsBean.getEndDate()%>" linkBase="/Node.Administration/eftWC" /></td>
                          </tr>
                          <tr>
                            <td colspan="6"><eftwc:AquaButton buttonText="Search" onClickScript="submitForm('SEARCH')" linkBase="/Node.Administration/eftWC" /></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <!-- Documents -->
                        <table width="100%">
                          <tr>
                            <td colspan="2">
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="5%">Select</td>
                                  <td class="gridHead1" nowrap="nowrap" width="5%">View</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Document<br />Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Document<br />Type</td>
                                  <td class="gridHead1" nowrap="nowrap" width="7%">Size (KB)</td>
                                  <td class="gridHead1" nowrap="nowrap" width="23%">Transaction ID</td>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Domain<br />Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Operation<br />Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%">Submitted Date</td>
                                </tr>
                                <%
                                  Document[] docs = DocumentsBean.getDocs();
                                  if (docs != null) {
                                    for (int i = 0; i < docs.length; i++) {
                                      String sizeStr = "";
                                      int size = docs[i].GetSize();
                                      if (size > 1000) {
                                        int remain = size % 1000;
                                        int kbSize = size / 1000;
                                        if (remain >= 500)
                                          kbSize++;
                                        sizeStr = ""+kbSize;
                                      }
                                      else
                                        sizeStr = "< 1";
                                %>
                          	<tr id="<%="docs"+docs[i].GetFileID()%>" class="trOut" onmouseover="Utils.setIDClass('<%="docs"+docs[i].GetFileID()%>','trOver')" onmouseout="Utils.setIDClass('<%="docs"+docs[i].GetFileID()%>','trOut')">
                                    <td class="RowPlain" align="center"><input type="checkbox" name="<%="remove"+docs[i].GetFileID()%>" /></td>
                                    <td class="RowPlain" align="center"><a href="/Node.Administration/Page/Documents/DocumentsEdit.do?documentid=<%=docs[i].GetFileID()%>">View</a></td>
                                    <td class="RowPlain"><a href="javascript:openDownload('<%=docs[i].GetFileID()%>')"><%=docs[i].GetName()%></a></td>
                                    <td class="RowPlain"><%=docs[i].GetType()%></td>
                                    <td class="RowPlain"><%=sizeStr%></td>
                                    <td class="RowPlain"><%=docs[i].GetTransID()%></td>
                                    <td class="RowPlain">
                                      <%if (docs[i].GetDomain()!=null) {%>
                                      <%=docs[i].GetDomain()%>
                                      <%}%>
                                    </td>
                                    <td class="RowPlain">
                                      <%if (docs[i].GetOperation()!=null) {%>
                                      <%=docs[i].GetOperation()%>
                                      <%}%>
                                    </td>
                                    <td class="RowPlain"><%=docs[i].GetSubmitDate()%></td>
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
                    <tr>
                      <td>
	                      <table width="100%">
	                          <tr>
	                            <td>
	                              <table width="40%">
	                                <tr>
	                                  <td align="left"><eftwc:AquaButton buttonText="Remove Selected" onClickScript="submitForm('REMOVE')" linkBase="/Node.Administration/eftWC" /></td>
	                                  <td align="left"><eftwc:AquaButton buttonText="Upload New File" onClickScript="submitForm('UPLOAD_FILE')" linkBase="/Node.Administration/eftWC" /></td>
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
            </table>
          </form>
        </body>
</html>
