<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Notify.ascx.cs" Inherits="PageControls_WebParts_Notify" %>
<a id="NOF" name="NOF" />
<asp:UpdatePanel ID="udpNotify" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="announce" cellspacing="0">
            <tr>
                <td class="task">
                    Total of search result: <span class="count">
                        <asp:Label ID="TotalNotifications" runat="server" Text="N/A"></asp:Label>
                    </span>
                </td>
                <td class="task">
                    <asp:ImageButton ID="imgTop5Notify" runat="server" ToolTip="Top 5" ImageUrl="~/App_Images/Node/Gen/file_img.gif"
                        ImageAlign="Right" CausesValidation="false" OnClick="imgTop5Notify_Click" AlternateText="Top 5">
                    </asp:ImageButton>
                    <asp:ImageButton ID="imgSearchNodify" runat="server" ToolTip="Search for Nodifications"
                        ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif" ImageAlign="Right" CausesValidation="false"
                        AlternateText="Search for the notifications"></asp:ImageButton>
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupNodifySearch" runat="server" TargetControlID="imgSearchNodify"
            DropShadow="true" PopupControlID="pnlPopupNodify" CancelControlID="btnCloseNotify"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupNodify" />
        <asp:Panel ID="pnlPopupNodify" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Search for Nodifications</div>
            <div class="Content">
                <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
                    Visible="false" />
                <eaf:FormSectionBlock ID="sec1" Caption="Node Domain Search Criteria" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlOpName">Operation Name:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlOpName" runat="server" CssClass="fld" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlDomainName">Domain Name:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDomainName" runat="server">
                                    <asp:ListItem Text="" Value="" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlStatus" runat="server" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="txtUserName">User Name:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="fld" Width="100px" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                <asp:Label ID="Label5" runat="server" AssociatedControlID="txtToken">Security Token:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtToken" runat="server" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="Label6" runat="server" AssociatedControlID="txtTransID">Transaction ID:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID" runat="server" Width="100px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label7" runat="server" AssociatedControlID="dtStart">Search Date Range:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtStart" /><br />
                                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dtStart" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label8" runat="server" AssociatedControlID="dtEnd">To:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtEnd" /><br />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtEnd" />
                            </td>
                        </tr>
                    </table>
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
                    <eaf:EAFButton ID="btnCloseNotify" runat="server" CssClass="s_BtnGrey" Text="Close"
                        CausesValidation="false" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSearchNotify" runat="server" CssClass="s_BtnGreen" Text="Search"
                        CausesValidation="false" OnClick="btnSearchNotify_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <asp:DataList ID="dataLstNotify" runat="server" Width="100%" DataKeyField="OPERATION_LOG_ID"
            CssClass="s_DLFrame">
            <AlternatingItemStyle CssClass="s_DLItmDB" />
            <ItemStyle CssClass="s_DLItmDB" />
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td>
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_import.gif" runat="server" alt="Notify" />
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table class="mt_lstBlock" cellspacing="0">
                                <tr>
                                    <td align="left" colspan="2" class="link">
                                        <asp:LinkButton ID="notifyLink" runat="server" CausesValidation="false" Text='<%# Eval("DATA_FLOW") %>'
                                            OnClick="ShowNotifyInfo_Click" CommandArgument='<%# Eval("OPERATION_LOG_ID") %>'> 
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" nowrap>
                                        Node Address:
                                    </td>
                                    <td class="val">
                                        <%# Eval("NODE_ADDRESS")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" nowrap>
                                        Date Created:
                                    </td>
                                    <td class="val">
                                        <%# Eval("CREATED_DTTM")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <eaf:EAFGridView ID="grvNotify" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%" Visible="false">
            <Columns>
                <asp:BoundField HeaderText="Operation Log ID" Visible="false" DataField="OPERATION_LOG_ID" />
                <asp:ButtonField ButtonType="Image" CausesValidation="false" CommandName="Select"
                    HeaderText="View" ImageUrl="~/App_Images/Node/Gen/file_blank.gif" />
                <asp:BoundField HeaderText="Data Flow" DataField="DATA_FLOW" SortExpression="DATA_FLOW" />
                <asp:BoundField HeaderText="Node Address" DataField="NODE_ADDRESS" SortExpression="NODE_ADDRESS" />
                <asp:BoundField HeaderText="Date Created" DataField="CREATED_DTTM" SortExpression="CREATED_DTTM" />
            </Columns>
        </eaf:EAFGridView>
        <asp:Button ID="btnShowPopup" runat="server" CausesValidation="false" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeNotifyDetail" runat="server" 
            TargetControlID="btnShowPopup"
            PopupControlID="pnlPopupNotifyInfo" 
            CancelControlID="btnCloseNotifyInfo" DropShadow="true"
            BackgroundCssClass="modalBackground"
            PopupDragHandleControlID="pnlPopupNotifyInfo" />
        <asp:Panel ID="pnlPopupNotifyInfo" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Notification Information</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="View Notification Information"
                    runat="server" Visible="true">
                    <asp:DataList ID="lstNotifyDetail" runat="server" Width="100%" CssClass="s_DLFrame">
                        <AlternatingItemStyle CssClass="s_DLItmAlt" />
                        <ItemStyle CssClass="s_DLItm" />
                        <ItemTemplate>
                            <table class="mt_lst" cellspacing="0">
                                <tr>
                                    <td>
                                        <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_import.gif" runat="server" alt="Nodtify" />
                                    </td>
                                    <td style="vertical-align: top; font-size: 100%; width: 100%;">
                                        <table class="mt_lstBlock" cellspacing="0">
                                            <tr>
                                                <td style="text-align: left">
                                                    Data Flow:
                                                </td>
                                                <td class="val">
                                                    <%# Eval("DataFlow")%>
                                                </td>
                                                <td style="text-align: left;">
                                                    Message Category:
                                                </td>
                                                <td class="val">
                                                    <%# Eval("MessageCategory")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left">
                                                    Message Name:
                                                </td>
                                                <td class="val">
                                                    <%# Eval("MessageName")%>
                                                </td>
                                                <td style="text-align: left;">
                                                    Message Status:
                                                </td>
                                                <td class="val">
                                                    <%# Eval("MessageStatus")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" nowrap>
                                                    ObjectId:
                                                </td>
                                                <td class="val" colspan="3">
                                                    <%# Eval("ObjectId")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" nowrap>
                                                    Message Detail
                                                </td>
                                                <td class="val" colspan="3">
                                                    <%# Eval("MessageDetail")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
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
                    <eaf:EAFButton ID="btnCloseNotifyInfo" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
