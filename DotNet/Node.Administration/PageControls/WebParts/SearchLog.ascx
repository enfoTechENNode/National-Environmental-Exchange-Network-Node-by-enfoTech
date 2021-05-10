<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchLog.ascx.cs" Inherits="PageControls_WebParts_SearchLog" %>
<a id="LOG" name="LOG" />
<asp:UpdatePanel ID="updPnlGridView" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="log">
                    Total of search result: <span class="count">
                        <asp:Label ID="TotalLog" runat="server" Text="N/A"></asp:Label>
                    </span>
                </td>
                <td class="log">
                    <asp:ImageButton ID="lkbTop5List" runat="server" ImageUrl="~/App_Images/Node/Gen/file_img.gif"
                        ToolTip="Top 5" ImageAlign="Right" CausesValidation="false" OnClick="lkbTop5List_Click"
                        AlternateText="Top 5"></asp:ImageButton>
                    <asp:ImageButton ID="lnkBtnSearch" runat="server" ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif"
                        ToolTip="Search for transaction logs" ImageAlign="Right" CausesValidation="false"
                        AlternateText="Search for transaction logs"></asp:ImageButton>
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="mdlTransform" runat="server" TargetControlID="lnkBtnSearch"
            PopupControlID="pnlLog" DropShadow="true" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
            PopupDragHandleControlID="pnlLog" />
        <asp:Panel ID="pnlLog" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Node Log Search</div>
            <div class="Content">
                <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
                    Visible="false" />
                <eaf:FormSectionBlock ID="sec1" Caption="Node Monitoring Search Criteria" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" width="600px" border="0">
                        <tr class="fld" valign="top">
                            <td class="fld">
                                Operation Name:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlOpName" runat="server" CssClass="fld" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                Operation Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlOpType" runat="server">
                                    <asp:ListItem Text="" Value="" />
                                    <asp:ListItem Text="Web Service" Value="WEB_SERVICE" />
                                    <asp:ListItem Text="Task" Value="SCHEDULED_TASK" />
                                </asp:DropDownList>
                            </td>
                            <td class="fld">
                                Web Service:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlWebService" runat="server" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                Status:
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlStatus" runat="server" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                User Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="fld" Width="100px" />
                            </td>
                            <td class="fld">
                                Domain Name:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDomain" runat="server" CssClass="fld" />
                            </td>
                        </tr>
                        <tr class="fld" valign="top">
                            <td class="fld">
                                Security Token:
                            </td>
                            <td>
                                <asp:TextBox ID="txtToken" runat="server" Width="100px" />
                            </td>
                            <td class="fld">
                                Transaction ID:
                            </td>
                            <td>
                                <asp:TextBox ID="txtTransID" runat="server" Width="100px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                Search Date Range:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtStart" />
                                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dtStart" />
                            </td>
                            <td class="fld">
                                To:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtEnd" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtEnd" />
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
                    <eaf:EAFButton ID="btnClose" runat="server" CssClass="s_BtnGrey" Text="Close" CausesValidation="false">
                    </eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSearch" runat="server" CssClass="s_BtnGreen" Text="Search"
                        CausesValidation="false" OnClick="btnSearch_Click"></eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
        <asp:DataList ID="dataLstLog" runat="server" Width="100%" DataKeyField="OPERATION_LOG_ID"
            CssClass="s_DLFrame">
            <AlternatingItemStyle CssClass="s_DLItmDB" />
            <ItemStyle CssClass="s_DLItmDB" />
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_task.gif" runat="server" alt='<%# GetOperationId("OPERATION_LOG_ID") %>' />
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table class="mt_lstBlock" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="link">
                                        <asp:LinkButton ID="domainLink" runat="server" CausesValidation="false" Text='<%# Eval("OPERATION_NAME") %>'
                                            OnClick="ShowLogInfo_Click" CommandArgument='<%# Eval("OPERATION_LOG_ID")  %>'
                                            ToolTip='<%#Eval("WEB_SERVICE_NAME")%>'> 
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="facB">
                                        Operation Type:
                                    </td>
                                    <td class="val" colspan="3">
                                        <%# Eval("OPERATION_TYPE")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="facB" style="width: 25%">
                                        Web Service:
                                    </td>
                                    <td class="val" style="width: 25%">
                                        <%# Eval("WEB_SERVICE_NAME")%>
                                    </td>
                                    <td class="facB" style="width: 25%">
                                        Domain Name:
                                    </td>
                                    <td class="val">
                                        <%# Eval("DOMAIN_NAME")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="facB">
                                        Status:
                                    </td>
                                    <td class="val" colspan="3">
                                        <%# Eval("STATUS_CD")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="facB">
                                        Last Updated:
                                    </td>
                                    <td class="val" colspan="3">
                                        <%# Eval("END_DTTM")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="facB">
                                        User:
                                    </td>
                                    <td class="val" colspan="3">
                                        <%# Eval("USER_NAME")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <eaf:EAFGridView ID="grvLog" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%" AlternatingRowStyle-CssClass="asp_GVRowAltDB"
            RowStyle-CssClass="">
            <Columns>
                <asp:BoundField HeaderText="Operation Log ID" Visible="false" DataField="OPERATION_LOG_ID" />
                <asp:ButtonField ButtonType="Image" CommandName="Select" CausesValidation="false"
                    HeaderText="View" ImageUrl="~/App_Images/Node/Gen/file_blank.gif" />
                <asp:BoundField HeaderText="Operation" DataField="OPERATION_NAME" SortExpression="OPERATION_NAME" />
                <asp:BoundField HeaderText="Web Service" DataField="WEB_SERVICE_NAME" SortExpression="WEB_SERVICE_NAME" />
                <asp:BoundField HeaderText="Domain" DataField="DOMAIN_NAME" SortExpression="DOMAIN_NAME" />
                <asp:BoundField HeaderText="Status" DataField="STATUS_CD" SortExpression="STATUS_CD" />
                <asp:BoundField HeaderText="User" DataField="USER_NAME" SortExpression="USER_NAME" />
                <asp:BoundField HeaderText="Last Updated" DataField="END_DTTM" SortExpression="END_DTTM" />
            </Columns>
        </eaf:EAFGridView>
        <asp:Button ID="btnShowPopup" runat="server" CausesValidation="false" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
            DropShadow="true" PopupControlID="pnlPopup" CancelControlID="btnCloseDetail"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopup" />
        <asp:Panel ID="pnlPopup" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Node Log Detail Information</div>
            <div class="Content" style="height: 400px">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Operation Transaction Details"
                    runat="server" Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr>
                            <td class="lftfld">
                                <asp:Label ID="Label1" runat="server" Text="Operation Name:" />
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:Literal runat="server" ID="mlOperationName"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="lftfld">
                                Transaction ID:
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:Literal runat="server" ID="mlTransactionID"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="lftfld">
                                Domain Name:
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlDomainName"></asp:Literal>
                            </td>
                            <td class="lftfld">
                                Host Name:
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlHostName"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="lftfld">
                                Start Date:
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlStartDate"></asp:Literal>
                            </td>
                            <td class="lftfld">
                                <asp:Literal ID="lEndDate" runat="server" Text="End Date:" />
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlEndDate"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="lftfld">
                                Operation Type:
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlOperationType"></asp:Literal>
                            </td>
                            <td class="lftfld">
                                <asp:Literal ID="lWebServiceName" runat="server" Text="Web Service Name:" />
                            </td>
                            <td class="lftfld">
                                <asp:Literal runat="server" ID="mlWebServiceName"></asp:Literal>
                            </td>
                        </tr>
                        <asp:Literal ID="lToken" runat="server" />
                        <tr>
                            <td class="lftfld">
                                <asp:Literal ID="lUserName" runat="server" Text="User Name:" />
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:Literal runat="server" ID="mlUserName"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="lftfld">
                                Requestor IP:
                            </td>
                            <td class="lftfld" colspan="3">
                                <asp:Literal runat="server" ID="mlRequestorIP"></asp:Literal>
                            </td>
                        </tr>
                        <asp:Literal ID="lSuppliedTransactionID" runat="server" />
                        <asp:Literal ID="lServiceType" runat="server" />
                        <asp:Literal ID="lURLs" runat="server" />
                    </table>
                </eaf:FormSectionBlock>
                <eaf:FormSectionBlock ID="sec2" Caption="Node Operation Transaction Parameters" runat="server"
                    Visible="true">
                    <asp:Literal ID="lParameters" runat="server" />
                </eaf:FormSectionBlock>
                <eaf:FormSectionBlock ID="sec3" Caption="Node Operation Transaction Status History"
                    runat="server" Visible="true">
                    <asp:Literal ID="lStatuses" runat="server" />
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
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnCloseDetail" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" />
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
