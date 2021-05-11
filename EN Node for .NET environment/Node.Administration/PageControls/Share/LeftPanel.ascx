<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftPanel.ascx.cs" Inherits="LeftPanel" %>

<eaf:PanelList ID="AdminLeftPanel" PanelTitle="Node Administration Functions" FirstPanel="true" runat="server" width="150px">
    <eaf:PanelItem ID="PanelItem_Monitoring" Caption="Node Monitoring" Link="~/Pages/Monitoring/SearchLogs.aspx" ImageSrc="~/App_Images/Node/PnlIco/pnlico_new.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Configurations" Caption="Node Configuration" Link="~/Pages/Configuration/NodeConfiguration.aspx" ImageSrc="~/App_Images/Node/PnlIco/pnlico_new.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Documents" Caption="Node Documents" Link="~/Pages/Document/SearchDocuments.aspx" ImageSrc="~/App_Images/Node/PnlIco/pnlico_new.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Users" Caption="Node Users" Link="~/Pages/User/SearchUsers.aspx" ImageSrc="~/App_Images/Node/PnlIco/pnlico_new.gif" runat="server"></eaf:PanelItem>
    <eaf:PanelItem ID="PanelItem_Domains" Caption="Node Domains" Link="~/Pages/Domain/SearchDomains.aspx" ImageSrc="~/App_Images/Node/PnlIco/pnlico_new.gif" runat="server"></eaf:PanelItem>
</eaf:PanelList>