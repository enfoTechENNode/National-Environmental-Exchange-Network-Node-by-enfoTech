<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NodeClient.ascx.cs" Inherits="PageControls_WebParts_NodeClient" %>
<table cellspacing="0" class="announce">
    <tr>
        <td class="ttl">
            Brief description of the Node Client & Node Registration Pages.
        </td>
    </tr>
</table>
<asp:DataList ID="UserLinkDataList" runat="server" Width="100%" CssClass="s_DLFrame">
    <FooterStyle CssClass="s_DLFooter"></FooterStyle>
    <AlternatingItemStyle CssClass="s_DLItmDB"></AlternatingItemStyle>
    <ItemStyle CssClass="s_DLItmDB"></ItemStyle>
    <HeaderStyle CssClass="s_DLHeader"></HeaderStyle>
    <ItemTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="NodeClientLink">
                    <img id="Img1" src="~/App_Images/Node/Gen/mnu_tool.gif" style="vertical-align: middle;"
                        runat="server" alt="Node Client" />
                    <a href="<%# Eval("PageURL")%>" target="<%# Eval("Target")%>">
                        <%# Eval("Name")%></a>
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:DataList>
