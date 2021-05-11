<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NodeRegistration.aspx.cs" Inherits="Pages_Registration_NodeRegistration" %>

<%@ Register TagPrefix="TABControl" TagName="TAB" Src="~/PageControls/Share/TabControlSR.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <eaf:MessageLabel ID="msgError" runat="server" />
    <TABControl:TAB ID="TabControls" runat="server" />
    <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Identification" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtNodeIdentifier">Node Identifier:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodeIdentifier" runat="server" Width="300px" />
                    <asp:RequiredFieldValidator ID="reqNodeID" runat="server" ControlToValidate="txtNodeIdentifier"
                        ErrorMessage="Method Name required" Display="Dynamic" ValidationGroup="Node"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="txtNodeName">Node Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodeName" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtNodeURL">Node Address(URL):</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodeURL" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="txtOrgIdentifier">Organization Identifier:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOrgIdentifier" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="txtNodeContact">Node Contact:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodeContact" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="txtDeployTypeCD">Node Deployment Type Code:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDeployTypeCD" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label9" runat="server" AssociatedControlID="txtStatus">Node Status:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="txtNodePropName">Node Property Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodePropName" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="txtNodePropValue">Node Property Value:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNodePropValue" runat="server" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label12" runat="server" AssociatedControlID="">Bounding Box Details:</asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr valign="top">
                <td colspan="2">
                    <table class="cc_EntryForm">
                        <tr>
                            <td width="120px">
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label13" runat="server" AssociatedControlID="txtNorth">North:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNorth" runat="server" Width="100px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label14" runat="server" AssociatedControlID="txtEast">East:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEast" runat="server" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label15" runat="server" AssociatedControlID="txtSouth">South:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSouth" runat="server" Width="100px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label16" runat="server" AssociatedControlID="txtWest">West:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWest" runat="server" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="RightButtons1" runat="server">
            <eaf:EAFButton ID="btnBackToDashboard" runat="server" CssClass="s_BtnGrey" Text="Back To Dashboard"
                CausesValidation="false" OnClick="btnBackToDashboard_Click"></eaf:EAFButton>
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click"
                CausesValidation="true"></eaf:EAFButton>
            <eaf:EAFButton ID="btnDownLoad" runat="server" CssClass="s_BtnGold" Text="Download"
                OnClick="btnDownLoad_OnClick" CausesValidation="true"></eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
