<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NodeDomain.ascx.cs" Inherits="PageControls_WebParts_NodeDomain" %>
<a id="DOM" name="DOM" />
<%--<asp:Panel ID="pnlDomain" runat="server">--%>
<asp:UpdatePanel ID="udpDomain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgTop5Domain" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="imgSearchDomain" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="domain">
                    Total of search result: <span class="count">
                        <asp:Label ID="TotalDomain" runat="server" Text="N/A"></asp:Label>
                    </span>
                </td>
                <td class="domain">
                    <asp:ImageButton ID="imgTop5Domain" runat="server" ImageAlign="Right" ImageUrl="~/App_Images/Node/Gen/file_img.gif"
                        CausesValidation="false" ToolTip="Top 5" OnClick="imgTop5Domain_Click" AlternateText="Top 5">
                    </asp:ImageButton>
                    <asp:ImageButton ID="imgSearchDomain" runat="server" ImageAlign="Right" ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif"
                        ToolTip="Search for Domain" CausesValidation="false" AlternateText="Search for Domain">
                    </asp:ImageButton>
                </td>
            </tr>
        </table>
        <asp:DataList ID="dataLstDomain" runat="server" Width="100%" DataKeyField="DOMAIN_NAME"
            CssClass="s_DLFrame">
            <AlternatingItemStyle CssClass="s_DLItmDB" />
            <ItemStyle CssClass="s_DLItmDB" />
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_pinup.gif" runat="server" alt='<%# GetDomainName("DOMAIN_NAME") %>' />
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table class="mt_lstBlock" cellspacing="0">
                                <tr>
                                    <td class="link">
                                        <asp:LinkButton ID="domainLink" runat="server" CausesValidation="false" Text='<%# Eval("DOMAIN_NAME") %>'
                                            OnClick="ShowDomainInfo_Click" CommandArgument='<%# Eval("DOMAIN_NAME")  %>'> 
                                        </asp:LinkButton>
                                    </td>
                                    <td class="link">
                                        <asp:LinkButton ID="btnGoToOperation" runat="server" Text="Go To Operations" OnClick="btnGoToOperation_Click"
                                            CausesValidation="false" CommandArgument='<%# Eval("DOMAIN_NAME")  %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Status:
                                    </td>
                                    <td class="val">
                                        <%# Eval("DOMAIN_STATUS_CD")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Status Message:
                                    </td>
                                    <td class="val">
                                        <%# Eval("DOMAIN_STATUS_MSG")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <eaf:EAFGridView ID="egvDomainGrid" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%">
            <Columns>
                <asp:ButtonField ButtonType="Image" CausesValidation="false" CommandName="Select"
                    HeaderText="Edit" ImageUrl="~/App_Images/Node/PnlIco/pnlico_edit.gif" />
                <asp:BoundField HeaderText="Domain Name" DataField="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
                <asp:BoundField HeaderText="Domain Status" DataField="DOMAIN_STATUS_CD" SortExpression="DOMAIN_STATUS_CD" />
                <asp:BoundField HeaderText="Status Message" DataField="DOMAIN_STATUS_MSG" SortExpression="DOMAIN_STATUS_MSG" />
            </Columns>
        </eaf:EAFGridView>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" CausesValidation="false" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlPopupDomainInfo" CancelControlID="btnCloseDomainInfo" DropShadow="true"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupDomainInfo" />
        <asp:Panel ID="pnlPopupDomainInfo" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Node Domain Information</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="View/Edit Domain Information"
                    runat="server" Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblDomainName" runat="server" AssociatedControlID="txtDomainName">Domain Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDomainName" runat="server" ReadOnly="true" Width="200px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="lblStatusDetail" runat="server" AssociatedControlID="ddlStatusDetail">Status:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatusDetail" runat="server">
                                    <asp:ListItem Value="Running">Running</asp:ListItem>
                                    <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblAllowedWS" runat="server" AssociatedControlID="">Allowed Web Services:</asp:Label>
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:CheckBoxList ID="cblAllowedWS" runat="server" RepeatColumns="5">
                                    <asp:ListItem Value="SUBMIT">Submit</asp:ListItem>
                                    <asp:ListItem Value="DOWNLOAD">Download</asp:ListItem>
                                    <asp:ListItem Value="QUERY">Query</asp:ListItem>
                                    <asp:ListItem Value="SOLICIT">Solicit</asp:ListItem>
                                    <asp:ListItem Value="NOTIFY">Notify</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblStatusMessage" runat="server" AssociatedControlID="txtStatusMessage">Domain Status Message:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtStatusMessage" runat="server" MaxLength="1000" Rows="5" TextMode="multiLine"
                                    Width="350px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lbltxtDescription" runat="server" AssociatedControlID="txtDescription">Domain Description:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Rows="2" TextMode="multiLine"
                                    Width="350px" />
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:FormSectionBlock ID="sec2" Caption="Domain Administrators" runat="server" Visible="true">
                    <eaf:EAFGridView ID="egvAdmins" runat="server" AllowPaging="true" AllowSorting="true"
                        PageSize="3" AllowMultiColumnSorting="true" Width="500px" OnRowCommand="egvAdmins_RowCommand">
                        <Columns>
                            <eaf:GridCheckBoxField ID="gcbfAdmin" HeaderText="Select" Visible="true" DataField="USER_ID" />
                            <asp:BoundField HeaderText="User Name" DataField="LOGIN_NAME" SortExpression="LOGIN_NAME" />
                        </Columns>
                    </eaf:EAFGridView>
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
                    <eaf:EAFButton ID="btnCloseDomainInfo" runat="server" CssClass="s_BtnGrey" Text="Close"
                        CausesValidation="false" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" CausesValidation="false"
                        OnClick="btnSaveDomainInfo_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <asp:ImageButton ID="btnAddNewDomain" runat="server" ImageUrl="~/App_Images/Node/NewDomain.gif"
            CausesValidation="false" AlternateText="Add new domain" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnAddNewDomain"
            PopupControlID="pnlNewDomain" CancelControlID="btnCancelNew" DropShadow="true"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlNewDomain" />
        <asp:Panel ID="pnlNewDomain" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                New Node Domain Information</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="New Domain Information" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr>
                            <td class="fld">
                                <asp:Label ID="lblDomainNameNew" runat="server" AssociatedControlID="txtDomainNameNew">Domain Name:</asp:Label>
                                <span style="color: Red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDomainNameNew" runat="server" MaxLength="50" Width="200px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="lblStatusNew" runat="server" AssociatedControlID="ddlStatusNew">Status:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatusNew" runat="server">
                                    <asp:ListItem Value="Running">Running</asp:ListItem>
                                    <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="lblWS" runat="server" AssociatedControlID="chkWS">Allowed Web Services:</asp:Label>
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:CheckBoxList ID="chkWS" runat="server" RepeatColumns="5">
                                    <asp:ListItem Value="SUBMIT">Submit</asp:ListItem>
                                    <asp:ListItem Value="DOWNLOAD">Download</asp:ListItem>
                                    <asp:ListItem Value="QUERY">Query</asp:ListItem>
                                    <asp:ListItem Value="SOLICIT">Solicit</asp:ListItem>
                                    <asp:ListItem Value="NOTIFY">Notify</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="lblStatusMessageNew" runat="server" AssociatedControlID="txtStatusMessageNew">Domain Status Message:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtStatusMessageNew" runat="server" MaxLength="1000" Rows="5" TextMode="multiLine"
                                    Width="350px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="lblDescriptionNew" runat="server" AssociatedControlID="txtDescriptionNew">Domain Description:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescriptionNew" runat="server" MaxLength="100" Rows="2" TextMode="multiLine"
                                    Width="350px" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvDomainName" runat="server" ValidationGroup="NewDomain"
                        ControlToValidate="txtDomainNameNew" Display="None" ErrorMessage="A Domain Name must be specified" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvDomainNameE" TargetControlID="rfvDomainName"
                        HighlightCssClass="validatorCalloutHighlight" />
                </eaf:FormSectionBlock>
                <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Domain Administrators" runat="server"
                    Visible="true">
                    <eaf:EAFGridView ID="egvAdminsNew" runat="server" AllowPaging="true" AllowSorting="true"
                        AllowMultiColumnSorting="true" Width="500px" OnRowCommand="egvAdmins_RowCommand">
                        <Columns>
                            <eaf:GridCheckBoxField ID="gcbfAdminNew" HeaderText="Select" Visible="true" DataField="USER_ID" />
                            <asp:BoundField HeaderText="User Name" DataField="LOGIN_NAME" SortExpression="LOGIN_NAME" />
                        </Columns>
                    </eaf:EAFGridView>
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
                    <eaf:EAFButton ID="btnCancelNew" runat="server" CssClass="s_BtnGrey" Text="Cancel"
                        CausesValidation="false" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSaveNew" runat="server" CssClass="s_BtnGreen" Text="Save" ValidationGroup="NewDomain"
                        CausesValidation="true" OnClick="btnAddNewDomain_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupDomain" runat="server" TargetControlID="imgSearchDomain"
            DropShadow="true" PopupControlID="pnlPopupDomain" CancelControlID="btnCloseDomain"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupDomain" />
        <asp:Panel ID="pnlPopupDomain" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Domain Search</div>
            <div class="Content">
                <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
                    Visible="false" />
                <eaf:FormSectionBlock ID="sec1" Caption="Node Domain Search Criteria" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lbldomain" runat="server" AssociatedControlID="ddlDomainName">Domain Name:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDomainName" runat="server" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="lblddlStatus" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Running">Running</asp:ListItem>
                                    <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <table class="eaf_FormSecTab1" runat="server" id="Table2">
                    <tr>
                        <td class="eaf_ttl">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Footer">
                <div class="Left">
                    <eaf:EAFButton ID="btnCloseDomain" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');" />
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSearchDomain" runat="server" CssClass="s_BtnGreen" Text="Search"
                        CausesValidation="false" OnClick="btnSearchDomain_Click" OnClientClick="fnSetFocus('SkipLinkTop');" />
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
