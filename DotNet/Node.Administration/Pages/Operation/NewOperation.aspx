<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NewOperation.aspx.cs" Inherits="NewOperation_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" />
    <asp:ValidationSummary ID="vsError" runat="server" DisplayMode="bulletList" ForeColor="darkred"
        HeaderText="There are errors on the page - Operation has not been saved" ShowMessageBox="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="New Operation Information" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr>
                <td />
                <td class="lftfld">
                    <asp:RequiredFieldValidator ID="rfvOperationName" runat="server" ControlToValidate="txtOperationName"
                        Display="dynamic" ErrorMessage="An Operation Name must be specified" ForeColor="darkred"
                        SetFocusOnError="true" />
                </td>
            </tr>
            <tr>
                <td class="fld">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="lblVersion">Node Version:</asp:Label>
                </td>
                <td colspan="3" class="val">
                    <asp:Label ID="lblVersion" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtOperationName">Operation Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOperationName" runat="server" MaxLength="50" Width="200px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="Running">Running</asp:ListItem>
                        <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtStatusMessage">Operation Status Message:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStatusMessage" runat="server" MaxLength="1000" Rows="5" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr>
                <td class="fld">
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="txtDescription">Operation Description:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Rows="2" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr>
                <td class="fld">
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="ddlOpType">Operation Type:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOpType" runat="server">
                        <asp:ListItem Value="WEB_SERVICE">Web Service</asp:ListItem>
                        <asp:ListItem Value="SCHEDULED_TASK">Scheduled Task</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr runat="server" id="secDataWizard">
                <td class="fld">
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="chkDataFlowWizard">Using Data Flow Wizard? </asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkDataFlowWizard" runat="server" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnCancel" runat="server" CssClass="s_BtnGrey" Text="Cancel" OnClick="btnCancel_Click"
                ConfirmMessage="Are you sure?" CausesValidation="false" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnNext" runat="server" CssClass="s_BtnGreen" Text="Next" OnClick="btnNext_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
