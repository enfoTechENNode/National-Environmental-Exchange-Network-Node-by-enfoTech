<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Documents.ascx.cs" Inherits="PageControls_WebParts_Documents" %>
<a id="DOC" name="DOC" />
<asp:UpdatePanel ID="udpDocu" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownloadDetail" />
        <asp:PostBackTrigger ControlID="btnDownload" />
        <asp:PostBackTrigger ControlID="btnUploadFile" />
    </Triggers>
    <ContentTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="docu">
                    Total of search result: <span class="count">
                        <asp:Label ID="TotalDocument" runat="server" Text="N/A"></asp:Label>
                    </span>
                </td>
                <td class="docu">
                    <asp:ImageButton ID="lkbTop5Docu" runat="server" ImageUrl="~/App_Images/Node/Gen/file_img.gif"
                        ImageAlign="Right" CausesValidation="false" ToolTip="Top 5" OnClick="lkbTop5Docu_Click"
                        AlternateText="Top 5"></asp:ImageButton>
                    <asp:ImageButton ID="lnkBtnSearchDocu" runat="server" ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif"
                        ImageAlign="Right" ToolTip="Search for Documents" CausesValidation="false" AlternateText="Search for Documents">
                    </asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
                        Visible="false" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:ModalPopupExtender ID="mdlDocu" runat="server" TargetControlID="lnkBtnSearchDocu"
            PopupControlID="pnlDocu" DropShadow="true" CancelControlID="btnCloseDocu" BackgroundCssClass="modalBackground"
            PopupDragHandleControlID="pnlDocu" />
        <asp:Panel ID="pnlDocu" runat="server" CssClass="detailPopup" Style="display: none">
            <div class="Header">
                Document Search</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="sec1" Caption="Node Document Search Criteria" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lbl" runat="server" AssociatedControlID="txtDocumentName">Document Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumentName" runat="server" Width="150px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlDomainName">Domain Name:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDomainName" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="txtTransID">Transaction ID:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlOperationName">Operation Name:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlOperationName" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label4" runat="server" AssociatedControlID="dtStart">Search Date Range:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtStart" />
                                <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dtStart" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label5" runat="server" AssociatedControlID="dtEnd">To:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="dtEnd" /><br />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtEnd" />
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
                    <eaf:EAFButton ID="btnCloseDocu" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"/>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnSearchDocu" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                        Text="Search" OnClick="btnSearchDocu_Click" OnClientClick="fnSetFocus('SkipLinkTop');"/>
                </div>
            </div>
        </asp:Panel>
        <asp:DataList ID="dataLstDocu" runat="server" Width="100%" DataKeyField="FILE_ID"
            CssClass="s_DLFrame">
            <AlternatingItemStyle CssClass="s_DLItmDB" />
            <ItemStyle CssClass="s_DLItmDB" />
            <ItemTemplate>
                <table class="mt_lst" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_new_edit.gif" runat="server"
                                alt='<%# GetDocumentName("FILE_NAME") %>' />
                        </td>
                        <td style="vertical-align: top; font-size: 100%; width: 100%;">
                            <table class="mt_lstBlock" cellspacing="0">
                                <tr>
                                    <td colspan="2" class="link">
                                        <asp:LinkButton ID="docuLink" runat="server" CausesValidation="false" Text='<%# Eval("DATAFLOW_NAME") %>'
                                            OnClick="ShowDocuInfo_Click" CommandArgument='<%# Eval("FILE_ID")  %>' ToolTip='<%# GetDocumentName("FILE_NAME") %>'>  
                                        </asp:LinkButton>
                                    </td>
                                    <td class="link">
                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/App_Images/Node/PnlIco/pnlico_rejected.gif"
                                            ImageAlign="Right" OnClick="imgBtnDelete_Click" OnClientClick="return confirm('Are you sure?');"
                                            CommandArgument='<%# Eval("FILE_ID")  %>' ToolTip="Delete Document" CausesValidation="false"
                                            AlternateText='<%# GetDeleteDocName("FILE_NAME") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Document Name:
                                    </td>
                                    <td class="val" colspan="2">
                                        <%# Eval("FILE_NAME")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Domain Name:
                                    </td>
                                    <td class="val" colspan="2">
                                        <%# Eval("DOMAIN_NAME")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Transaction ID:
                                    </td>
                                    <td class="val" colspan="2">
                                        <%# Eval("TRANS_ID")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Submitted Date:
                                    </td>
                                    <td class="val" colspan="2">
                                        <%# Eval("SUBMIT_DTTM")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <eaf:EAFGridView ID="egvDocumentGrid" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%">
            <Columns>
                <eaf:GridCheckBoxField ID="gcbfDocuments" HeaderText="Remove" Visible="true" DataField="FILE_ID" />
                <asp:ButtonField ButtonType="Image" CausesValidation="false" CommandName="Select"
                    HeaderText="Edit" ImageUrl="~/App_Images/Node/PnlIco/pnlico_edit.gif" />
                <asp:BoundField HeaderText="Trans. ID" DataField="TRANS_ID" SortExpression="TRANS_ID" />
                <asp:BoundField HeaderText="Data Flow" DataField="DATAFLOW_NAME" SortExpression="DATAFLOW_NAME" />
                <asp:BoundField HeaderText="Type" DataField="FILE_TYPE" SortExpression="FILE_TYPE" />
                <asp:BoundField HeaderText="Submitted Date" DataField="SUBMIT_DTTM" SortExpression="SUBMIT_DTTM" />
            </Columns>
        </eaf:EAFGridView>
        <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/App_Images/Node/RemoveFiles.gif"
            CausesValidation="false" OnClick="btnRemove_Click" confirmmessage="Are you sure?"
            AlternateText="Remove Document" />
        <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/App_Images/Node/Download.gif"
            CausesValidation="false" OnClick="btnDownload_Click" AlternateText="Download Document" />
        <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/App_Images/Node/UploadFile.gif"
            CausesValidation="false" AlternateText="Upload Document" />
        <asp:Button ID="btnShowPopupDocu" runat="server" CausesValidation="false" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupDocu" runat="server" TargetControlID="btnShowPopupDocu"
            DropShadow="true" PopupControlID="pnlPopupDetailDocu" CancelControlID="btnCloseDetailDocu"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlPopupDetailDocu" />
        <asp:Panel ID="pnlPopupDetailDocu" runat="server" CssClass="detailPopup" Style="display: none;">
            <div class="Header">
                Document Detail</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Operation Transaction Details"
                    runat="server" Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label6" runat="server" AssociatedControlID="txtDocumentNameDetail">Document Name:</asp:Label>
                                <span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumentNameDetail" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label7" runat="server" AssociatedControlID="txtDocumentType">Document Type:</asp:Label>
                                <span style="color: red">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumentType" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="fld">
                                <asp:Label ID="Label8" runat="server" AssociatedControlID="mlFileSize">File Size:</asp:Label>
                            </td>
                            <td class="lftfld">
                                <eaf:MessageLabel ID="mlFileSize" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label9" runat="server" AssociatedControlID="txtTransIDDetail">Transaction ID:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTransIDDetail" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label10" runat="server" AssociatedControlID="ddlDataFlowName">Data Flow Name:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDataFlowName" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label11" runat="server" AssociatedControlID="mlDomainName">Domain Name:</asp:Label>
                            </td>
                            <td class="lftfld">
                                <eaf:MessageLabel ID="mlDomainName" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label12" runat="server" AssociatedControlID="txtStatus">Status:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStatus" runat="server" MaxLength="10" Width="100px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label13" runat="server" AssociatedControlID="mlSubmitURL">Submit URL:</asp:Label>
                            </td>
                            <td class="lftfld">
                                <eaf:MessageLabel ID="mlSubmitURL" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label14" runat="server" AssociatedControlID="mlSubmitToken">Submit Token:</asp:Label>
                            </td>
                            <td class="lftfld">
                                <eaf:MessageLabel ID="mlSubmitToken" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label15" runat="server" AssociatedControlID="mlSubmitDate">Submitted Date:</asp:Label>
                            </td>
                            <td class="lftfld">
                                <eaf:MessageLabel ID="mlSubmitDate" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label16" runat="server" AssociatedControlID="fuFileContent">File Content:</asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fuFileContent" runat="server" Width="400px" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvDocumentName" runat="server" ValidationGroup="DocuDetail"
                        ControlToValidate="txtDocumentNameDetail" Display="None" ErrorMessage="A Document Name must be specified" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvDocumentNameE" TargetControlID="rfvDocumentName"
                        HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RequiredFieldValidator ID="rfvDocumentType" runat="server" ValidationGroup="DocuDetail"
                        ControlToValidate="txtDocumentType" Display="None" ErrorMessage="A Document Type must be specified" />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvDocumentTypeE" TargetControlID="rfvDocumentType"
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
                    <eaf:EAFButton ID="btnCloseDetailDocu" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');" />
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnDownloadDetail" runat="server" CausesValidation="false" CssClass="s_BtnGreen"
                        Text="Download" OnClick="btnDownloadDetail_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <eaf:EAFButton ID="btnUpdate" runat="server" CssClass="s_BtnGreen" CausesValidation="true"
                        ValidationGroup="DocuDetail" Text="Update" OnClick="btnUpdate_Click" OnClientClick="fnSetFocus('SkipLinkTop');" />
                </div>
            </div>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeUpload" runat="server" TargetControlID="btnUpload"
            DropShadow="true" PopupControlID="pnlUpload" CancelControlID="btnCloseUpload"
            BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlUpload" />
        <asp:Panel ID="pnlUpload" runat="server" CssClass="detailPopup" Style="display: none;">
            <div class="Header">
                Document Upload Information</div>
            <div class="Content">
                <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Node Document Upload" runat="server"
                    Visible="true">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label17" runat="server" AssociatedControlID="txtDocuNameUpload">Document Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocuNameUpload" runat="server" Width="200px" />
                            </td>
                            <td class="fld">
                                <asp:Label ID="Label18" runat="server" AssociatedControlID="txtDocuTypeUpload">Document Type:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocuTypeUpload" runat="server" Width="50px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label19" runat="server" AssociatedControlID="txtTransIDUpload">Transaction ID:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransIDUpload" runat="server" Width="350px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label20" runat="server" AssociatedControlID="ddlDataFlow">Data Flow:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDataFlow" runat="server" Width="350px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label21" runat="server" AssociatedControlID="fuUpload">File Content:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:FileUpload ID="fuUpload" runat="server" Width="350px" />
                                <asp:CustomValidator ID="customValidate" runat="server" ControlToValidate="fuFileContent"
                                    Display="Dynamic" />
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
                    <eaf:EAFButton ID="btnCloseUpload" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                        Text="Close" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton>
                </div>
                <div class="Right">
                    <eaf:EAFButton ID="btnUploadFile" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                        Text="Upload" OnClick="btnUpload_Click" OnClientClick="fnSetFocus('SkipLinkTop');">
                    </eaf:EAFButton>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
