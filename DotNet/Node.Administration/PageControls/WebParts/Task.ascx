<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Task.ascx.cs" Inherits="PageControls_WebParts_Task" %>
<a id="TSK" name="TSK" />
<asp:UpdatePanel ID="udpTask" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="announce" cellspacing="0">
            <tr>
                <td class="task">
                    Total of search result: <span class="count">
                        <asp:Label ID="TotalTasks" runat="server" Text="N/A"></asp:Label>
                    </span>
                </td>
                <td class="task">
                    <asp:ImageButton ID="imgTop5Task" runat="server" ToolTip="Top 5" ImageUrl="~/App_Images/Node/Gen/file_img.gif"
                        ImageAlign="Right" CausesValidation="false" OnClick="imgTop5Task_Click" AlternateText="Top 5">
                    </asp:ImageButton>
                    <asp:ImageButton ID="imgSearchTask" runat="server" ToolTip="Search for scheduled tasks"
                        ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif" ImageAlign="Right" CausesValidation="false"
                        AlternateText="Search for scheduled tasks"></asp:ImageButton>
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupTaskSearch" runat="server" TargetControlID="imgSearchTask"
            DropShadow="true" PopupControlID="pnlPopupTask" CancelControlID="btnCloseTask"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupTask" />
        <asp:Panel ID="pnlPopupTask" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Search Tasks</div>
            <div class="Content">
                <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
                    Visible="false" />
                <eaf:FormSectionBlock ID="sec1" Caption="Node Task Search Criteria" runat="server"
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
                    <eaf:EAFButton ID="btnCloseTask" runat="server" CssClass="s_BtnGrey" Text="Close"
                        CausesValidation="false" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSearchTask" runat="server" CssClass="s_BtnGreen" Text="Search"
                        CausesValidation="false" OnClick="btnSearchTask_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <asp:DataList ID="dataLstTask" runat="server" Width="100%" DataKeyField="OPERATION_LOG_STATUS_ID"
            CssClass="s_DLFrame">
            <AlternatingItemStyle CssClass="s_DLItmDB" />
            <ItemStyle CssClass="s_DLItmDB" />
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td>
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_scheduledtask.gif" runat="server"
                                alt="Task">
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table class="mt_lstBlock" cellspacing="0">
                                <tr>
                                    <td align="left" colspan="2" class="link">
                                        <asp:LinkButton ID="taskLink" runat="server" CausesValidation="false" Text='<%# Eval("OPERATION_NAME") %>'
                                            OnClick="ShowTaskInfo_Click" CommandArgument='<%# Eval("LOG_AND_STATUS_IDS") %>'> 
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Domain Name:
                                    </td>
                                    <td class="val">
                                        <%# Eval("DOMAIN_NAME")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">
                                        Status:
                                    </td>
                                    <td class="val">
                                        <%# Eval("STATUS_CD")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        Date Finished:
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
        <eaf:EAFGridView ID="grvTask" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%" Visible="false">
            <Columns>
                <asp:BoundField HeaderText="Operation Log/Status ID" Visible="false" DataField="LOG_AND_STATUS_IDS" />
                <asp:ButtonField ButtonType="Image" CausesValidation="false" CommandName="Select"
                    HeaderText="View" ImageUrl="~/App_Images/Node/Gen/file_blank.gif" />
                <asp:BoundField HeaderText="Operation" DataField="OPERATION_NAME" SortExpression="OPERATION_NAME" />
                <asp:BoundField HeaderText="Domain" DataField="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
                <asp:BoundField HeaderText="Status" DataField="STATUS_CD" SortExpression="STATUS_CD" />
                <asp:BoundField HeaderText="Date Finished" DataField="CREATED_DTTM" SortExpression="CREATED_DTTM" />
            </Columns>
        </eaf:EAFGridView>
        <asp:Button ID="btnShowPopup" runat="server" CausesValidation="false" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeTaskDetail" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlPopupTaskInfo" CancelControlID="btnCloseTaskInfo" DropShadow="true"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupTaskInfo" />
        <asp:Panel ID="pnlPopupTaskInfo" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Scheduled Task Information</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="View Task Information" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" width="600px">
                        <tr>
                            <td class="fld" width="150px">
                                <asp:Label ID="Label9" runat="server" AssociatedControlID="lblOpName">Operation Name:</asp:Label>
                            </td>
                            <td class="val">
                                <asp:Label ID="lblOpName" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label10" runat="server" AssociatedControlID="lblDomainName">Domain Name:</asp:Label>
                            </td>
                            <td class="val">
                                <asp:Label ID="lblDomainName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="Label11" runat="server" AssociatedControlID="lblStatus">Status:</asp:Label>
                            </td>
                            <td class="val">
                                <asp:Label ID="lblStatus" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label12" runat="server" AssociatedControlID="lblDateFinished">Date Finished:</asp:Label>
                            </td>
                            <td class="val">
                                <asp:Label ID="lblDateFinished" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label13" runat="server" AssociatedControlID="lblMessage">Status Message:</asp:Label>
                            </td>
                            <td class="val">
                                <asp:Label ID="lblMessage" runat="server" />
                            </td>
                        </tr>
                    </table>
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
                </div>
                <div class="Right"><eaf:EAFButton ID="btnCloseTaskInfo" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                    Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
