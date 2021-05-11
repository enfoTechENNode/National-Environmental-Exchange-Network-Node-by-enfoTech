<%@ Page Language="C#" MasterPageFile="~/MasterPages/Simple.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Pages_Main_Home" Title="Node Home" %>

<%@ MasterType VirtualPath="~/MasterPages/Simple.master" %>
<%@ Register TagPrefix="Node" TagName="Document" Src="~/PageControls/WebParts/Documents.ascx" %>
<%@ Register TagPrefix="Node" TagName="Domain" Src="~/PageControls/WebParts/NodeDomain.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeStatus" Src="~/PageControls/WebParts/NodeStatus.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeLog" Src="~/PageControls/WebParts/SearchLog.ascx" %>
<%@ Register TagPrefix="Node" TagName="Task" Src="~/PageControls/WebParts/Task.ascx" %>
<%@ Register TagPrefix="Node" TagName="FavoriteLink" Src="~/PageControls/WebParts/FavoriteLink.ascx" %>
<%@ Register TagPrefix="Node" TagName="Notify" Src="~/PageControls/WebParts/Notify.ascx" %>
<%@ Register TagPrefix="Node" TagName="Configuration" Src="~/PageControls/WebParts/Configuration.ascx" %>
<%@ Register TagPrefix="Node" TagName="DashboardTab" Src="~/PageControls/Share/DashboardTab.ascx" %>
<%@ Import Namespace="Node.Lib.UI.Elements" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Literal ID="SkipNavCtl" runat="server">
    </asp:Literal>
    <asp:WebPartManager ID="webPartMgr" runat="server" Personalization-Enabled="true" 
        OnDisplayModeChanged="webPartMgr_DisplayModeChanged">
    </asp:WebPartManager>
    <eaf:MultiColumnPanel ID="mPnl" PanelCss="cc_MultiClmTable" AllColumnCss="cc_MultiClmTd"
        LeftColumnCss="cc_MultiClmLeft" runat="server">
        <asp:Panel ID="leftPnl" runat="server">
            <asp:CatalogZone ID="catZone" runat="server" HeaderText="Add Content" Width="200px"
                EmptyZoneText="Catalog Zone contains no Catalog Parts" SelectTargetZoneText="Add to"
                HeaderCloseVerb-ImageUrl="~/App_Images/EAF/Btn_Close.gif">
                <AddVerb Description="Adds a Content to a Zone"></AddVerb>
                <ZoneTemplate>
                    <asp:PageCatalogPart ID="pgCat" Title="Content List" runat="server" />
                </ZoneTemplate>
            </asp:CatalogZone>
        </asp:Panel>
        <asp:Panel ID="rightPnl" runat="server" Style="padding: 2px 2px 2px 2px;">
            <div style="clear:both;">
                <div style="float: left;">
                    <Node:DashboardTab ID="HeaderTab" runat="Server"></Node:DashboardTab>
                </div>
                <div id="tbAddContent" runat="server" style="float: right">
                    <asp:ImageButton ID="ibCat" runat="server" ImageUrl="~/App_Images/Node/btnEditLayout.gif"
                        OnClick="Cat_Click" AlternateText="Modify this page contents"></asp:ImageButton>
                </div>
            </div>
            <eaf:StylerPanel ID="descPanel" PanelStyle="yellowbubble" runat="server" Visible="false">
                <div style="font-size: 8pt;">
                    Here you can have SIMPLE description or instruction for this page. If you need a
                    full length document, place a link and pop up a new windows.
                </div>
            </eaf:StylerPanel>
            <table cellspacing="0" class="cc_WPTab">
                <tr>
                    <td style="width: 33%;">
                        <asp:WebPartZone ID="leftPartzone" runat="server" HeaderText="Left">
                            <ZoneTemplate>
                                <Node:NodeStatus ID="status" title="Node Status" runat="server"></Node:NodeStatus>
                                <Node:Domain ID="domain" title="Node Domains" runat="server"></Node:Domain>
                                <Node:Document ID="document" title="Node Document" runat="server"></Node:Document>
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td style="width: 33%;">
                        <asp:WebPartZone ID="middlePartZone" runat="server" HeaderText="Middle">
                            <ZoneTemplate>
                                <Node:NodeLog ID="log" title="Node Transaction Log" runat="server"></Node:NodeLog>
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td style="width: 33%;">
                        <asp:WebPartZone ID="rightPartZone" runat="server" HeaderText="Right">
                            <ZoneTemplate>
                                <Node:FavoriteLink ID="userlink" title="Favorite Links" runat="server"></Node:FavoriteLink>
                                <Node:Configuration ID="config" title="Node Configuration" runat="server"></Node:Configuration>
                                <Node:Task ID="task" title="Scheduled Tasks" runat="server"></Node:Task>
                                <Node:Notify ID="notify" title="Node Notifications" runat="server"></Node:Notify>
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </eaf:MultiColumnPanel>
</asp:Content>
