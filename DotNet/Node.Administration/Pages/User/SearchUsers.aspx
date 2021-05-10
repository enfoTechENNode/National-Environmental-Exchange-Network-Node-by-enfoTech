<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="SearchUsers.aspx.cs" Inherits="SearchUsers_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
        Visible="false" Width="500px" />
    <eaf:FormSectionBlock ID="sec1" Caption="User Search Criteria" runat="server" Visible="true">
        <table class="cc_EntryForm" cellspacing="0" cellpadding="0" width="550">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtLoginName">User Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server" Width="150px" /><br />
                    <asp:RegularExpressionValidator ID="regLoginName" ControlToValidate="txtLoginName"
                        Display="dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9@._\\-]*" ErrorMessage='no special character allowed'
                        CssClass="fld" ValidationGroup="SearchButton"></asp:RegularExpressionValidator>
                </td>
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlUserType">User Type:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_Change">
                        <asp:ListItem Text="" Value="" />
                        <asp:ListItem Text="Console User" Value="CONSOLE_USER" />
                        <asp:ListItem Text="NAAS Node User" Value="NAAS_NODE_USER" />
                        <asp:ListItem Text="Local Node User" Value="LOCAL_NODE_USER" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="top" id="firstLastNameRow" runat="server">
                <td class="fld">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="txtFirstName">First Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" Width="150px" /><br />
                    <asp:RegularExpressionValidator ID="regFirstName" ControlToValidate="txtFirstName"
                        Display="dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9]*" ErrorMessage='no special character allowed'
                        CssClass="fld" ValidationGroup="SearchButton"></asp:RegularExpressionValidator>
                </td>
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtLastName">Last Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" Width="150px" /><br />
                    <asp:RegularExpressionValidator ID="regLastName" ControlToValidate="txtLastName"
                        Display="dynamic" runat="server" ValidationExpression="^[a-zA-Z0-9]*" ErrorMessage='no special character allowed'
                        CssClass="fld" ValidationGroup="SearchButton"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="ddlDomain">Associated Domain:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDomain" runat="server" />
                </td>
                <td class="lftfld" colspan="2">
                    <asp:CheckBox ID="cbAllNAAS" runat="server" Text="Search All NAAS Users" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnBackToDashBoard" runat="server" Text="Back To Dashboard" OnClick="btnBackToDashboard_Click"
                CssClass="s_BtnGrey" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSearch" runat="server" CssClass="s_BtnGreen" Text="Search"
                OnClick="btnSearch_Click" ValidationGroup="SearchButton" Width="150"></eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
    <eaf:ButtonTable ID="btnNewAccountPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftNewAccountBtnPanel" runat="server">
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightNewAccountBtnPanel" runat="server">
            <eaf:EAFButton ID="btnNewNAAS" runat="server" CssClass="s_BtnGreen" Text="Add New NAAS User"
                OnClick="btnNewNAAS_Click" CausesValidation="false" />
            <eaf:EAFButton ID="btnNewConsole" runat="server" CssClass="s_BtnGreen" Text="Add New Console User"
                OnClick="btnNewConsole_Click" CausesValidation="false" />
            <eaf:EAFButton ID="btnNewLocal" runat="server" CssClass="s_BtnGreen" Text="Add New Local User"
                OnClick="btnNewLocal_Click" CausesValidation="false" />
        </eaf:RightButtons>
    </eaf:ButtonTable>
    <br />
    <%--    <eaf:FloatWinLink ID="floatwinlnk" runat="server" PageLink="" WinTitle="" WinWidth="400" WinHeight="400"></eaf:FloatWinLink>
--%>
    <eaf:EAFGridView ID="egvUserGrid" runat="server" AllowPaging="false" AllowSorting="true"
        AllowMultiColumnSorting="true" Width="600px" OnRowEditing="egvUserGrid_RowEditing"
        OnRowCommand="egvUserGrid_RowCommand">
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Edit" HeaderText="View" ImageUrl="~/App_Images/Node/Gen/file_blank.gif" />
            <asp:BoundField HeaderText="User Name" DataField="LOGIN_NAME" SortExpression="LOGIN_NAME" />
            <asp:BoundField HeaderText="Full Name" DataField="USER_FULL_NAME" SortExpression="USER_FULL_NAME" />
            <asp:BoundField HeaderText="Status" DataField="USER_STATUS_CD" SortExpression="USER_STATUS_CD" />
            <asp:BoundField HeaderText="Type" DataField="ACCOUNT_TYPE" SortExpression="ACCOUNT_TYPE" />
            <asp:BoundField HeaderText="Created Date" DataField="CREATED_DTTM" SortExpression="CREATED_DTTM" />
        </Columns>
    </eaf:EAFGridView>
</asp:Content>
