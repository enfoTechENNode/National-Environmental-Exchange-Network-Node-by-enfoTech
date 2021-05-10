<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Client.master" CodeFile="Error.aspx.cs" Inherits="Pages_Error" %>

<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leftContent" runat="Server">
    <Node:LeftPanel ID="ClientLeftPanel" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <eaf:FormSectionBlock ID="sec1" Caption="Generic Error Information" runat="server" SectionType="frame">
        <eaf:TextResourceLabel ID="txtInformation" runat="server" PageKey="main" CssClass="fld" />
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server">
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton />
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
