<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Pages_Utilities_Error" %>
<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <eaf:FormSectionBlock ID="sec1" Caption="Generic Error Information" runat="server" SectionType="frame">
        <eaf:TextResourceLabel ID="txtInformation" runat="server" PageKey="main" CssClass="fld" />
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server">
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnHome" runat="server" CssClass="s_BtnGrey" Text="Back to Dashboard" OnClick="btnHome_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
    
</asp:Content>
