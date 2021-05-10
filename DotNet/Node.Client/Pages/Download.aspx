<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="Download.aspx.cs"
    Inherits="Download" Title="Node Client Utility" AutoEventWireup="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Client.master" %>
<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeTab" Src="~/PageControls/NodeTabLink.ascx" %>

<%@ Import Namespace="Node.Core.Document" %>
<asp:Content ID="LeftPanelContent" ContentPlaceHolderID="leftContent" runat="Server">
    <Node:LeftPanel ID="ClientLeftPanel" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <node:NodeTab ID="NodeTab1" runat="server"></node:NodeTab>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel ID="TabNode1" runat="server" HeaderText="Node 1.1">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="sec1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs ID="NodeURLsPanel" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="4" align="left">
                                Download Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken" AssociatedControlID="txtToken" runat="server">Security Token</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtToken" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTransID" AssociatedControlID="txtTransID" runat="server">Transaction ID</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDataFlow" AssociatedControlID="txtDataFlow" runat="server">Data Flow</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDataFlow" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileName1" AssociatedControlID="txtFileName1" runat="server">File 1 Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileName1" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDropDownList1" AssociatedControlID="DropDownList1" runat="server">File 1 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFileType1" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="50px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileName2" AssociatedControlID="txtFileName2" runat="server">File 2 Name</asp:Label>
                            </td>
                            <td>
                               <asp:TextBox ID="txtFileName2" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDropDownList2" AssociatedControlID="DropDownList2" runat="server">File 2 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFileType2" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="50px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileName3" AssociatedControlID="txtFileName3" runat="server">File 3 Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileName3" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDropDownList3" AssociatedControlID="DropDownList3" runat="server">File 3 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFileType3" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="50px" />
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnDownload" runat="server" CssClass="s_BtnGreen" Text="Download"
                            OnClick="btnDownload_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <asp:Label ID="lblResultN" runat="server"></asp:Label>
                <!--
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" style="width: 100%;">
                    <tr valign="top">
                        <th colspan="2" align="left">
                            Download Result
                        </th>
                    </tr>
                    <% if (this.lblResult.Visible)
                       { 
                    %>
                    <tr class="alt1" valign="top">
                        <td style="width: 100%">
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <% 
                        }
                       NodeDocument doc = (NodeDocument)this.Session["RETURN_DOCS0"];
                       int count = 0;
                       while (doc != null)
                       {
                    %>
                    <tr class="alt1" valign="top">
                        <td style="width: 100%">
                            <a href="DownloadContent.aspx?returnDocI=<%=count%>">
                                <%=doc.name%></a>
                        </td>
                    </tr>
                    <%
                        count++;
                        doc = (NodeDocument)this.Session["RETURN_DOCS" + count];
               }
                    %>
                </table>
                <% } %> -->
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabNode12" runat="server" HeaderText="Node 2.0">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters"
                    runat="server" Visible="true">
                    <Node:NodeURLs2 ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="4" align="left">
                                Download Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken2" AssociatedControlID="txtToken2" runat="server">Security Token</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtToken2" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTransID2" AssociatedControlID="txtTransID2" runat="server">Transaction ID</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID2" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDataFlow2" AssociatedControlID="txtDataFlow2" runat="server">Data Flow</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDataFlow2" runat="server" Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFile2Name1" AssociatedControlID="txtFile2Name1" runat="server">File 1 Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFile2Name1" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileType1" AssociatedControlID="drpFileType1" runat="server">File 1 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFile2Type1" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="drpFileType1" runat="server" Width="50px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFile2Name2" AssociatedControlID="txtFile2Name2" runat="server">File 2 Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFile2Name2" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileType2" AssociatedControlID="drpFileType2" runat="server">File 2 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFile2Type2" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="drpFileType2" runat="server" Width="50px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFile2Name3" AssociatedControlID="txtFile2Name3" runat="server">File 3 Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFile2Name3" runat="server" Width="300px" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFileType3" AssociatedControlID="drpFileType3" runat="server">File 3 Type</asp:Label>
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtFile2Type3" runat="server" Width="50px" />--%>
                                <asp:DropDownList ID="drpFileType3" runat="server" Width="50px" /> 
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnDownload2" runat="server" CssClass="s_BtnGreen" Text="Download"
                            OnClick="btnDownload2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <asp:Label ID="lblResultN2" runat="server"></asp:Label>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
