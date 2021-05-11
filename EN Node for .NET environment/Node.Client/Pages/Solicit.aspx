<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="Solicit.aspx.cs"
    Inherits="Solicit" Title="Node Client Utility" AutoEventWireup="false" %>

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
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Solicit Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken" AssociatedControlID="txtToken" runat="server">Security Token</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblReturnURL" AssociatedControlID="txtReturnURL" runat="server">Return URL</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtReturnURL" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblRequest" AssociatedControlID="ddlRequest" runat="server">Request</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRequest" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRequest_SelectedIndexChanged"
                                    Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTextRequest" AssociatedControlID="txtRequest" runat="server">Or</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequest" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <eaf:DynamicControlPanel ID="dcpDynamicParams" ControlsWithoutIDs="DontPersist" runat="server">
                        </eaf:DynamicControlPanel>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnSolicit" runat="server" CssClass="s_BtnGreen" Text="Solicit"
                            OnClick="btnSolicit_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Solicit Result
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
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Solicit Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken2" AssociatedControlID="txtToken2" runat="server">Security Token</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken2" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right">
                                <asp:Label ID="lblDataFlow" AssociatedControlID="txtDataFlow" runat="server">Data Flow</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataFlow" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblRequest2" AssociatedControlID="ddlRequest2" runat="server">Request</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRequest2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRequest2_SelectedIndexChanged"
                                    Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTextRequest2" AssociatedControlID="txtRequest2" runat="server">Or</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequest2" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right">
                                <asp:Label ID="lblRecipient" AssociatedControlID="txtRecipient" runat="server">Recipient</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRecipient" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right">
                                <asp:Label ID="lblNotificationURI" AssociatedControlID="txtNotificationURI" runat="server">NotificationURI</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNotificationURI" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblNotifyType4" AssociatedControlID="DropDownList4" runat="server">Notification Type</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <eaf:DynamicControlPanel ID="dcpDynamicParams2" ControlsWithoutIDs="DontPersist"
                            runat="server">
                        </eaf:DynamicControlPanel>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnSolicit2" runat="server" CssClass="s_BtnGreen" Text="Solicit"
                            OnClick="btnSolicit2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Solicit Result
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
