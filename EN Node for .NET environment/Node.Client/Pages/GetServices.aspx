<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="GetServices.aspx.cs"
    Inherits="GetServices" Title="Node Client Utility" AutoEventWireup="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Client.master" %>
<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeTab" Src="~/PageControls/NodeTabLink.ascx" %>

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
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Get Services Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken" AssociatedControlID="txtToken" runat="server">Security Token</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblServiceType" AssociatedControlID="ddlServiceType" runat="server">Service Type (on host node)</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlServiceType" runat="server" Width="350px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblServiceTyping" AssociatedControlID="txtServiceType" runat="server">Service Type (free text)</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtServiceType" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnGetServices" runat="server" CssClass="s_BtnGreen" Text="GetServices"
                            OnClick="btnGetServices_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Get Services Result
                        </td>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td>
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabNode12" runat="server" HeaderText="Node 2.0">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters"
                    runat="server" Visible="true">
                    <Node:NodeURLs2 ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Get Services Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken2" AssociatedControlID="txtToken2" runat="server">Security Token</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken2" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblServiceGetegory" AssociatedControlID="drpServiceGetegory" runat="server">Service Category</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpServiceGetegory" runat="server" Width="350px">
                                    <asp:ListItem Text="All Service" Value="AllServices" />
                                    <asp:ListItem Text="Query" Value="Query" />
                                    <asp:ListItem Text="Solicit" Value="Solicit" />
                                    <asp:ListItem Text="Execute" Value="Execute" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnGetServices2" runat="server" CssClass="s_BtnGreen" Text="GetServices"
                            OnClick="btnGetServices2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Get Services Result
                        </td>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td>
                            <asp:Label ID="lblResult2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
