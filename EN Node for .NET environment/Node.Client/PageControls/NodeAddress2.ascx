<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NodeAddress2.ascx.cs" Inherits="NodeAddress2" %>
<table class="cc_EntryForm" cellspacing="0" border="0" width="600">
    <tr valign="top">
        <td class="fld">
            <asp:Label ID="lblNodeAddress1" AssociatedControlID="ddlNodeAddress" runat="server" Text="Select a Node Address"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlNodeAddress" runat="server" width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr valign="top">
        <td class="fld">
            <asp:Label ID="lblNodeAddress2" AssociatedControlID="txtNodeAddress" runat="server" Text="Or Enter a Node Address"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNodeAddress" runat="server" Width="350px"></asp:TextBox>
        </td>
    </tr>
</table>
