<%@ page import="Node.Biz.Administration.Domain,Node.Biz.Administration.User,Node.Phrase" %>
<%@ taglib uri="/WEB-INF/struts-logic.tld" prefix="logic" %>
<%@ taglib uri="http://www.enfotech.com/eftWC" prefix="eftwc" %>
<jsp:useBean id="DomainsBean" class="Node.Web.Administration.Bean.Domains.DomainsBean" scope="request" />

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
          <form action="/Node.Administration/Page/Domains/Domains.do" method="POST" name="form1">
            <input type="hidden" name="act" />
            <table width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table width="100%">
                    <tr>
                      <td class="formTitle" nowrap="nowrap" width="100%" height="20" background="/Node.Administration/eftWC/skin/img/pnl/tskpnl_bar_bg.gif">&nbsp;<a href="/Node.Administration/Page/Domains/Domains.do">Domain Management</a></td>
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
                            <td class="formTitle">Searching Criteria</td>
                          </tr>
                          <tr>
                            <td>
                              <table width="100%">
                                <tr>
                                  <td class="formFldLabel">Domain Name:</td>
                                  <td>
                                    <select name="domainName">
                                      <%
                                      	String[] selDomainList = DomainsBean.getSelDomainList();
                                        String domainName = DomainsBean.getDomainName();
                                      %>
                                      <option <%if(domainName.equals("")){%>selected="selected"<%}%>></option>
                                      <%
                                        if (selDomainList != null) {
                                          for (int i = 0; i < selDomainList.length; i++) {
                                      %>
                                      <option <%if(selDomainList[i].equals(domainName)){%>selected="selected"<%}%>><%=selDomainList[i]%></option>
                                      <%
                                          }
                                        }
                                      %>
                                    </select>
                                  </td>
                                  <td class="formFldLabel">Status:</td>
                                  <td>
                                    <select name="status">
                                      <option <%if(DomainsBean.getStatus().equals("")){%>selected="selected"<%}%>></option>
                                      <option <%if(DomainsBean.getStatus().equals("Running")){%>selected="selected"<%}%>>Running</option>
                                      <option <%if(DomainsBean.getStatus().equals("Stopped")){%>selected="selected"<%}%>>Stopped</option>
                                      <option <%if(DomainsBean.getStatus().equals("Active")){%>selected="selected"<%}%>>Active</option>
                                      <option <%if(DomainsBean.getStatus().equals("Inactive")){%>selected="selected"<%}%>>Inactive</option>
                                    </select>
                                  </td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td><eftwc:AquaButton buttonText="Search" onClickScript="submitForm('SEARCH')" /></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <table width="100%">
                          <tr>
                            <td colspan="2">
                              <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                  <td class="gridHead1" nowrap="nowrap" width="10%" align="center">View/Edit</td>
                                  <td class="gridHead1" nowrap="nowrap" width="15%" align="center">Domain Operations</td>
                                  <td class="gridHead1" nowrap="nowrap" width="20%">Domain Name</td>
                                  <td class="gridHead1" nowrap="nowrap" width="20%">Domain Admins</td>
                                  <td class="gridHead1" nowrap="nowrap" width="10%">Status</td>
                                  <td class="gridHead1" nowrap="nowrap" width="25%">Status Message</td>
                                </tr>
                                <%
                                  Domain[] domains = DomainsBean.getDomainList();
                                  if (domains != null) {
                                    for (int i = 0; i < domains.length; i++) {
                                      String status = domains[i].GetDomainStatusCD();
                                %>
                          	<tr id="<%="domain"+domains[i].GetDomainID()%>" class="trOut" onmouseover="Utils.setIDClass('<%="domain"+domains[i].GetDomainID()%>','trOver')" onmouseout="Utils.setIDClass('<%="domain"+domains[i].GetDomainID()%>','trOut')">
                                    <td class="RowPlain" align="center"><a href="/Node.Administration/Page/Domains/DomainsEdit.do?domainid=<%=domains[i].GetDomainID()%>">View</a></td>
                                    <td class="RowPlain" align="center">
                                    <%
                                      if (status != null && !status.equals(Phrase.INACTIVE_STATUS)) {
                                    %>
                                      <a href="/Node.Administration/Page/Domains/Operations.do?domain=<%=domains[i].GetDomainName()%>" onclick="submitForm('OPERATIONS')">Operations</a>
                                    <%
                                      }
                                    %>
                                    </td>
                                    <td class="RowPlain"><%=domains[i].GetDomainName()%></td>
                                    <td class="RowPlain">
                                      <%
                                        String[] admins = domains[i].GetAdmins();
                                        if (admins != null) {
                                          for (int j = 0; j < admins.length; j++) {
                                            if (j != 0) {
                                      %>
                                      ,
                                      <%
                                            }
                                      %>
                                      <%=admins[j]%>
                                      <%
                                          }
                                        }
                                      %>
                                    </td>
                                    <td class="RowPlain"><%=domains[i].GetDomainStatusCD()%></td>
                                    <td class="RowPlain"><%=domains[i].GetDomainStatusMsg()%></td>
                                </tr>
                                <%
                                    }
                                  }
                                %>
                              </table>
                            </td>
                          </tr>
                          <%
                            if (DomainsBean.getIsNodeAdmin()) {
                          %>
                          <tr>
                            <td>
                              <eftwc:AquaButton buttonText="Create New Domain" onClickScript="submitForm('NEW_DOMAIN')" linkBase="/Node.Administration/eftWC" />
                            </td>
                          </tr>
                          <%
                            }
                          %>
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
