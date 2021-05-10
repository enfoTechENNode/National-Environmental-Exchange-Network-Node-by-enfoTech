<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NewConsoleUser.aspx.cs" Inherits="NewConsoleUser_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" Width="500px" />
    <asp:ValidationSummary ID="vsError" runat="server" DisplayMode="bulletList" ForeColor="darkred"
        HeaderText="There are errors on the page - Console User has not been saved" ShowMessageBox="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="New Console User Information" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td />
                <td class="lftfld">
                    <asp:RequiredFieldValidator ID="rfvLoginName" runat="server" ControlToValidate="txtLoginName"
                        Display="dynamic" ErrorMessage="A Login Name must be specified" ForeColor="darkred"
                        SetFocusOnError="true" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtLoginName">User Name:</asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server" MaxLength="50" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Text="Active" Value="A" />
                        <asp:ListItem Text="Inactive" Value="I" />
                    </asp:DropDownList>
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
                <td colspan="3">
                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="60" Width="150px" />
                </td>
            </tr>
            <tr valign="top">
                <td />
                <td class="lftfld" colspan="3">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        Display="dynamic" ErrorMessage="An Email Address must be specified" ForeColor="darkred"
                        SetFocusOnError="true" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        Display="dynamic" ErrorMessage="The Email Address is not a valid Email Address"
                        ForeColor="darkred" SetFocusOnError="true" ValidationExpression=".*@.*" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="txtEmail">Email Address:</asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="ptbPhone">Phone Number:</asp:Label>
                </td>
                <td>
                    <eaf:PhoneTextBox ID="ptbPhone" runat="server" Width="100px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="txtAddress">Address 1:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="txtSuppAddress">Address 2:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtSuppAddress" runat="server" MaxLength="100" Width="300px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label9" runat="server" AssociatedControlID="txtCity">City:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" MaxLength="100" Width="150px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="txtState">State USPS Code:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" MaxLength="2" Width="30px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label12" runat="server" AssociatedControlID="zctbZip">Zip Code:</asp:Label>
                </td>
                <td>
                    <eaf:ZipCodeTextBox ID="zctbZip" runat="server" Width="50px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label13" runat="server" AssociatedControlID="txtCountry">Country:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server" MaxLength="25" Width="100px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label14" runat="server" AssociatedControlID="txtComments">Comments:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtComments" runat="server" MaxLength="255" Rows="5" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:FormSectionBlock ID="sec2" Caption="Console User Domain Priviledges" runat="server"
        Visible="true">
        <eaf:EAFGridView ID="egvDomains" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="200px" OnRowCommand="egvDomains_RowCommand">
            <Columns>
                <eaf:GridCheckBoxField ID="gcbfDomain" HeaderText="Select" Visible="true" DataField="DOMAIN_ID" />
                <asp:BoundField HeaderText="Domain" DataField="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
            </Columns>
        </eaf:EAFGridView>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="lbtn1" runat="server">
            <eaf:EAFButton ID="btnCancel" runat="server" CssClass="s_BtnGrey" Text="Cancel" CausesValidation="false"
                OnClick="btnCancel_Click"></eaf:EAFButton>
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
