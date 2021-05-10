<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="NodePing.aspx.cs"
    Inherits="NodePing" Title="Node Client Utility" AutoEventWireup="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Client.master" %>
<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeTab" Src="~/PageControls/NodeTabLink.ascx" %>
<asp:Content ID="LeftPanelContent" ContentPlaceHolderID="leftContent" runat="Server">
    <Node:LeftPanel ID="ClientLeftPanel" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <node:NodeTab ID="NodeTab1" runat="server"></node:NodeTab>
    <asp:ScriptManager ID="scriptManager" runat="server" />
<%--    <a id="main" name="main"></a>
--%>    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel ID="TabNode1" runat="server" HeaderText="Node 1.1">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="sec1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs ID="NodeURLsPanel" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Node Ping Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblHello" AssociatedControlID="txtHello" runat="server">Hello Message</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHello" runat="server" Width="400px">hello</asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnNodePing" runat="server" CssClass="s_BtnGreen" Text="Node Ping"
                            OnClick="btnNodePing_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" width="550px">
                    <tr valign="top">
                        <th colspan="2" align="left">
                            Node Ping Result
                        </th>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td style="width: 100%">
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabNode12" runat="server" HeaderText="Node 2.0">
        <ContentTemplate>
        <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs2 ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Node Ping Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblHello2" AssociatedControlID="txtHello2" runat="server">Hello Message</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHello2" runat="server" Width="400px">hello</asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnNodePing2" runat="server" CssClass="s_BtnGreen" Text="Node Ping"
                            OnClick="btnNodePing2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="550px">
                    <tr valign="top">
                        <th colspan="2" align="left">
                            Node Ping Result
                        </th>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td style="width: 100%">
                            <asp:Literal ID="lblResult2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <% } %>
                </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
