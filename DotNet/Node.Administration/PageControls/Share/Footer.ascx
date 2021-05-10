<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Footer" %>

<table id="cc_NodeFooter" cellspacing="0" width="800">
    <tr>
        <td align="left" style="font-family:Arial; font-size:8pt; color:#666666;">
	        Copyright © <%=GetBuildYear()%> by enfoTech &#38; Consulting Inc. All Rights Reserved. 
	        <br/>
        </td>
        <td class="logo"><a href="http://www.enfotech.com" target="_blank"><asp:Image ID="Image1" ImageUrl="~/App_Images/Node/Gen/TailLogo.gif" runat="server" AlternateText="enfoTech &amp; Consulting Inc." /></a></td>
    </tr>
</table>