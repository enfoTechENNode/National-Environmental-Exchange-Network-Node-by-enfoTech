<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftPanel.ascx.cs" Inherits="LeftPanel" %>

<eaf:PanelList ID="ClientLeftPanel" PanelTitle="Web Services" FirstPanel="true" runat="server" width="150px">
    <eaf:PanelItem ID="PanelItem_NodePing" Caption="Node Ping" Link="~/Pages/NodePing.aspx" ImageSrc="~/Images/PnlIco/pnlico_new.gif" runat=server></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Authenticate" Caption="Authenticate" Link="~/Pages/Authenticate.aspx" ImageSrc="~/Images/PnlIco/pnlico_edit.gif" runat=server></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_GetServices" Caption="Get Services" Link="~/Pages/GetServices.aspx" ImageSrc="~/Images/PnlIco/pnlico_import.gif" runat=server></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_GetStatus" Caption="Get Status" Link="~/Pages/GetStatus.aspx" ImageSrc="~/Images/PnlIco/pnlico_import.gif" runat=server></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Submit" Caption="Submit" Link="~/Pages/Submit.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat=server></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Download" Caption="Download" Link="~/Pages/Download.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Query" Caption="Query" Link="~/Pages/Query.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Solicit" Caption="Solicit" Link="~/Pages/Solicit.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Notify" Caption="Notify" Link="~/Pages/Notify.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem1" Caption="Execute" Link="~/Pages/Execute.aspx" ImageSrc="~/Images/PnlIco/pnlico_fav.gif" runat="server"></eaf:PanelItem>
</eaf:PanelList>