<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FavoriteLink.ascx.cs"
    Inherits="PageControls_WebParts_FavoriteLink" %>
<a id="LNK" name="LNK" />
<asp:UpdatePanel ID="updPnlGridView" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSaveEdit" />
        <asp:AsyncPostBackTrigger ControlID="btnDeleteLink" />
        <asp:PostBackTrigger ControlID="btnSaveLink" />
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="ttl">
                    Provides a quick link to frequently accessed pages
                </td>
                <td class="ttl">
                    <asp:ImageButton ID="lkbNewUserLink" runat="server" ToolTip="Add a new link" ImageAlign="Right"
                        ImageUrl="~/App_Images/Node/PnlIco/pnlico_newlink.gif" CausesValidation="false"
                        AlternateText="Add a new link"></asp:ImageButton>
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="mdlLinkInfo" runat="server" TargetControlID="lkbNewUserLink"
            PopupControlID="pnlLink" DropShadow="true" CancelControlID="btnCloseLink" BackgroundCssClass="modalBackground"
            PopupDragHandleControlID="pnlLink" />
        <asp:Panel ID="pnlLink" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                My Favorite Links</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="sec1" Caption="Node Monitoring Search Criteria" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" width="600px" border="0">
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="lbl" runat="server" AssociatedControlID="txtLinkName">Link Name:</asp:Label>
                                <span style="color: red">*</span>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtLinkName" runat="server" CssClass="fld" Width="200px" /><br />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="txtURL">URL:</asp:Label>
                                <span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtURL" runat="server" CssClass="fld" Width="350px" /><br />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="txtDescNew">Description:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescNew" runat="server" TextMode="MultiLine" Rows="4" CssClass="fld"
                                    Width="350px" /><br />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtLinkName"
                        Display="None" ValidationGroup="NewLink" ErrorMessage="Link Name is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNameE" TargetControlID="rfvName"
                        HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RequiredFieldValidator ID="rfvURLNew" runat="server" ControlToValidate="txtURL"
                        Display="None" ValidationGroup="NewLink" ErrorMessage="Link URL is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvURLNewE" TargetControlID="rfvURLNew"
                        HighlightCssClass="validatorCalloutHighlight" />
                </eaf:FormSectionBlock>
                <table class="eaf_FormSecTab1" runat="server" id="FormSectionBlockGridView">
                    <tr>
                        <td class="eaf_ttl">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Footer">
                <div class="Left">
                    <eaf:EAFButton ID="btnCloseLink" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSaveLink" runat="server" CausesValidation="true" ValidationGroup="NewLink"
                        CssClass="s_BtnGreen" Text="Save" OnClick="btnSaveNew_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <asp:DataList ID="UserLinkDataList" runat="server" Width="100%" CssClass="s_DLFrame">
            <FooterStyle CssClass="s_DLFooter"></FooterStyle>
            <AlternatingItemStyle CssClass="s_DLItmDB"></AlternatingItemStyle>
            <ItemStyle CssClass="s_DLItmDB"></ItemStyle>
            <HeaderStyle CssClass="s_DLHeader"></HeaderStyle>
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_link.gif" style="vertical-align: middle;"
                                runat="server" alt='<%# GetLinkName("Name") %>' />
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table cellspacing="0" class="mt_lstBlock">
                                <tr>
                                    <td width="80%" class="link">
                                        <a href="<%# Eval("Link")%>" target="<%# Eval("Target")%>">
                                            <%# Eval("Name")%></a>
                                    </td>
                                    <td class="link">
                                        <asp:ImageButton ID="imgEdit" runat="server" ImageAlign="Right" OnClick="ShowLinkInfo_Click"
                                            ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif" CommandArgument='<%# Eval("Name") %>'
                                            CausesValidation="false" AlternateText='<%# GetClickToName("Name") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <%# Eval("Description")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <asp:Button ID="btnShowLinkInfo" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeEditLink" runat="server" TargetControlID="btnShowLinkInfo"
            PopupControlID="pnlDetailLink" DropShadow="true" CancelControlID="btnCloseEdit"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlDetailLink" />
        <asp:Panel ID="pnlDetailLink" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                My Favorite Link Details</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Monitoring Search Criteria"
                    runat="server" Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" width="600px" border="0">
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="lblLinkName">Link Name:</asp:Label>
                            </td>
                            <td class="val" colspan="3">
                                <asp:Label ID="lblLinkName" runat="server" CssClass="fld" /><br />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="txtURLEdit">URL:</asp:Label>
                                <span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtURLEdit" runat="server" CssClass="fld" Width="350px" /><br />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label5" runat="server" AssociatedControlID="txtDesc">Description:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Rows="4" CssClass="fld"
                                    Width="350px" /><br />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvLink" runat="server" ControlToValidate="txtURLEdit"
                        Display="None" ValidationGroup="EditLink" ErrorMessage="Link URL is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvLinkE" TargetControlID="rfvLink"
                        HighlightCssClass="validatorCalloutHighlight" />
                </eaf:FormSectionBlock>
                <table class="eaf_FormSecTab1" runat="server" id="Table1">
                    <tr>
                        <td class="eaf_ttl">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Footer">
                <div class="Left">
                    <eaf:EAFButton ID="btnCloseEdit" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnDeleteLink" runat="server" CausesValidation="false" CssClass="s_BtnRed"
                        Text="Delete" OnClick="btnDeleteLink_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                    <eaf:EAFButton ID="btnSaveEdit" runat="server" CausesValidation="true" ValidationGroup="EditLink"
                        CssClass="s_BtnGreen" Text="Save" OnClick="btnSaveEdit_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
