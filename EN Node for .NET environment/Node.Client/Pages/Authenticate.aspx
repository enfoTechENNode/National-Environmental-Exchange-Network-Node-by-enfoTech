<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="Authenticate.aspx.cs"
    Inherits="Authenticate" Title="Node Client Utility" AutoEventWireup="false" %>

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
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" >
        <ajaxToolkit:TabPanel ID="TabNode1" runat="server" HeaderText="Node 1.1">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="sec1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs ID="NodeURLsPanel" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Authenticate Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblUserId1" AssociatedControlID="txtUserId1" runat="server">User ID</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserId1" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblPassword1" AssociatedControlID="txtPassword1" runat="server">Password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword1" TextMode="password" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblAuthenticationMethod1" AssociatedControlID="txtAuthenticationMethod1" runat="server">Authentication Method</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAuthenticationMethod1" Text="PASSWORD" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnAuthenticate" runat="server" CssClass="s_BtnGreen" Text="Authenticate"
                            OnClick="btnAuthenticate_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Authenticate Result
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
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs2 ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Authenticate Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblUserId2" AssociatedControlID="txtUserId2" runat="server">User ID</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserId2" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblPassword2" AssociatedControlID="txtPassword2" runat="server">Password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword2" TextMode="password" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblAuthenticationMethod2" AssociatedControlID="txtAuthenticationMethod2" runat="server">Authentication Method</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAuthenticationMethod2" Text="PASSWORD" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDomain" AssociatedControlID="txtDomain" runat="server">Domain Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDomain" Text="" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnAuthenticate2" runat="server" CssClass="s_BtnGreen" Text="Authenticate"
                            OnClick="btnAuthenticate2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Authenticate Result
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
