<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="ViewNAASUser.aspx.cs" Inherits="ViewNAASUser_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" />
    <asp:ValidationSummary ID="vsError" runat="server" DisplayMode="bulletList" ForeColor="darkred"
        HeaderText="There are errors on the page - Console User has not been saved" ShowMessageBox="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="View NAAS User Information" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0" width="550px">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtLoginName">User Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server" MaxLength="50" ReadOnly="true" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" MaxLength="50" Width="150px" TextMode="Password" /><br />
                    <asp:RequiredFieldValidator ID="reqPassword" ValidationGroup="ChangePassword" runat="server"
                        Display="Dynamic" ControlToValidate="txtPassword" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="txtFirstName">First Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="60" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtMidInitial">Middle Initial:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMidInitial" runat="server" MaxLength="1" Width="10px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="txtLastName">Last Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="60" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="ptbPhone">Phone Number:</asp:Label>
                </td>
                <td colspan="2">
                    <eaf:PhoneTextBox ID="ptbPhone" runat="server" Width="100px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="txtAddress">Address:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="txtCity">City:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="100" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label9" runat="server" AssociatedControlID="txtState">State USPS Code:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" MaxLength="2" Width="30px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="zctbZip">Zip Code:</asp:Label>
                </td>
                <td>
                    <eaf:ZipCodeTextBox ID="zctbZip" runat="server" Width="50px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="txtCountry">Country:</asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtCountry" runat="server" MaxLength="25" Width="100px" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:FormSectionBlock ID="sec2" Caption="NAAS Node User Operation Priviledges" runat="server"
        Visible="true">
        <eaf:EAFGridView ID="egvOperations" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="550px" OnRowCommand="egvOperations_RowCommand">
            <Columns>
                <eaf:GridCheckBoxField ID="gcbfOperation" HeaderText="Select" Visible="true" DataField="OPERATION_ID" />
                <asp:BoundField HeaderText="Operation" DataField="OPERATION_NAME" SortExpression="OPERATION_NAME" />
                <asp:BoundField HeaderText="Domain" DataField="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
                <asp:BoundField HeaderText="Web Service Name" DataField="WEB_SERVICE_NAME" SortExpression="WEB_SERVICE_NAME" />
            </Columns>
        </eaf:EAFGridView>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="Leftbuttons1" runat="server">
            <eaf:EAFButton ID="btnBack" runat="server" CssClass="s_BtnGreen" Text="Back" OnClick="btnBack_Click"
                CausesValidation="false" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="RightButtons1" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnPasswordChange" runat="server" CssClass="s_BtnGreen" Text="Change Password"
                OnClick="btnPasswordChange_Click" ValidationGroup="ChangePassword"></eaf:EAFButton>&nbsp;&nbsp;&nbsp;&nbsp;
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnDelete" runat="server" CssClass="s_BtnRed" Text="Remove User"
                OnClick="btnDelete_Click" ConfirmMessage="Are you sure?" />
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
