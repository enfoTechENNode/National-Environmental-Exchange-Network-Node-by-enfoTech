<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Header" %>
<%--<table cellpadding="0" cellspacing="0" border="0" width="100%" bgcolor="#0055a6">
    <tr valign="top">
        <td><a href="http://www.enfotech.com">
       <asp:Image id="image0" runat="server" ImageUrl="~/App_Images/Node/Header/Header.jpg" AlternateText="Node Administration Utility" borderwidth="0" /></a>
</td>
    </tr>
</table>--%>
<div style=" border:1px solid #ffffff; background-color:#0055A6;">
    <table border="0" cellpadding="0" cellspacing="0" bgcolor="#0055A6" width="100%">
        <tr valign="top">
            <td colspan="4"><a href="http://www.enfotech.com/"><asp:Image ID="imageHeader" runat="server" ImageUrl="~/App_Images/Node/Header/Header.gif" AlternateText="Node Administration Utility"/></a></td>
        </tr>
        <tr class="HeaderText" valign="top" style="background-color: #396EA0">
            <td width="6px"></td>
            <td><a href="<%= Request.ApplicationPath %>/Pages/Main/Login.aspx?Type=SignOut" style="color:White; ">Logout</a></td>
            <td align="right"><a href="http://www.enfotech.com/" target="_blank" style="color:White; ">enfoTech &amp; Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx" style="color:White;" target="_blank">Contact Us</a></td>
            <td width="6px"></td>
        </tr>
    </table>
</div>