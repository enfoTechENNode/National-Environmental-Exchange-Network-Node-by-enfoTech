<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="ManageOperation.aspx.cs" Inherits="Pages_OperationManager_ManageOperation"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leftContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <%--<asp:UpdatePanel ID="udpDocu" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlOperations" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnUploadFile" />
            <asp:PostBackTrigger ControlID="btnGenerate" />
            <asp:PostBackTrigger ControlID="btn_ExGenerate" />
            <asp:PostBackTrigger ControlID="btnTransform" />
        </Triggers>
        <ContentTemplate>--%>
    <eaf:MessageLabel ID="msg" runat="server" />
    <eaf:FormSectionBlock ID="FormSectionBlockManageOperations" Caption="Manage Operations"
        runat="server" Visible="true">
        <eaf:InputFormTable ID="OperationTable" runat="server" FieldNameWidth="200px">
            <eaf:FormInputField ID="FormInputField1" runat="server" FieldName="Selected Operation">
                <asp:DropDownList ID="ddlOperations" OnSelectedIndexChanged="ddlOperations_SelectedIndexChanged"
                    runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </eaf:FormInputField>
        </eaf:InputFormTable>
    </eaf:FormSectionBlock>
    <eaf:FormSectionBlock ID="FormSectionopFilter" Caption="Filter" runat="server" Visible="true">
        <eaf:InputFormTable ID="opFilter" runat="server" FieldNameWidth="200px">
        </eaf:InputFormTable>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="BtnTable" runat="server" Visible="true">
        <eaf:LeftButtons ID="LeftButtonsFunctions" runat="server">
            <eaf:EAFButton ID="btnRefresh" runat="server" Text="Refresh" Enabled="true" CssClass="s_BtnBlue"
                OnClick="btnRefresh_Click" />&nbsp;&nbsp;
            <eaf:EAFButton ID="btnUpload" runat="server" Text="Upload" Enabled="false" CssClass="s_BtnGreen" />&nbsp;&nbsp;
            <eaf:EAFButton ID="btnGenerate" runat="server" Text="Generate" Enabled="false" CssClass="s_BtnGreen" />&nbsp;&nbsp;
            <eaf:EAFButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                Enabled="false" CssClass="s_BtnRed" />
        </eaf:LeftButtons>
    </eaf:ButtonTable>
<%--    <table class="eaf_FormSecTab1" runat="server" id="FormSectionBlockGridView">
        <tr>
            <td class="eaf_ttl">
                Files Ready for Submission
            </td>
        </tr>
    </table>--%>
    <br />
    <div style="margin-left: 30px">
        <eaf:EAFGridView ID="egvDocumentGrid" runat="server" PageSize="10" AllowPaging="true"
            AllowSorting="true" AllowMultiColumnSorting="false" Width="100%" OnRowDataBound="egvDocumentGrid_OnRowDataBound">
            <Columns>
                <eaf:GridCheckBoxField ID="gcbfDocuments" HeaderText="" Visible="true" DataField="FILE_ID" />
<%--                <asp:ButtonField ButtonType="Image" CausesValidation="false" HeaderText="View" CommandName="Transform"
                    ImageUrl="~/App_Images/Node/PnlIco/pnlico_format.gif" CommandArgument='<%# Eval("FILE_ID") %>'/>--%>
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnView" CommandName="Transform" ImageUrl="~/App_Images/Node/PnlIco/pnlico_format.gif"
                            runat="server" CommandArgument='<%# Eval("FILE_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnTrans" CommandName="GetDownLoadReport" ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif"
                            runat="server" CommandArgument='<%# Eval("TRANS_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data Viewer">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDataView" CommandName="GoToDataView" ImageUrl="~/App_Images/Node/PnlIco/pnlico_view.gif"
                            runat="server" CommandArgument='<%# Eval("TRANS_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </eaf:EAFGridView>
    </div>
    <asp:Button ID="btnShowPopup" runat="server" CausesValidation="false" Style="display: none" />
    <%-- PANELS--%>
    <%--   View Style Sheet Panel  --%>
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" PopupDragHandleControlID="pnlPopup"
        runat="server" TargetControlID="btnShowPopup" DropShadow="true" PopupControlID="pnlPopup"
        BackgroundCssClass="modalBackground" CancelControlID="btnClose" />
    <asp:Panel ID="pnlPopup" runat="server" CssClass="detailPopup" Style="display: none">
        <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
            View Documents</div>
        <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;
            overflow: auto; height: auto">
            <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="File Info" runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label6" runat="server" AssociatedControlID="mlFileName">Document Name:</asp:Label>
                        </td>
                        <td>
                            <eaf:MessageLabel ID="mlFileName" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label7" runat="server" AssociatedControlID="mlFileType">Document Type:</asp:Label>
                        </td>
                        <td>
                            <eaf:MessageLabel ID="mlFileType" runat="server" />
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
                            <asp:Label ID="Label9" runat="server" AssociatedControlID="mlTransID">Transaction ID:</asp:Label>
                        </td>
                        <td>
                            <eaf:MessageLabel ID="mlTransID" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label10" runat="server" AssociatedControlID="mlDataFlow">Data Flow Name:</asp:Label>
                        </td>
                        <td>
                            <eaf:MessageLabel ID="mlDataFlow" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label12" runat="server" AssociatedControlID="mlStatus">Status:</asp:Label>
                        </td>
                        <td>
                            <eaf:MessageLabel ID="mlStatus" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label15" runat="server" AssociatedControlID="mlSubmitDate">Created Date:</asp:Label>
                        </td>
                        <td class="lftfld">
                            <eaf:MessageLabel ID="mlSubmitDate" runat="server" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Select Style Sheet File" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="grvViewStyleSheets" runat="server" AllowPaging="true" AllowSorting="true"
                    AllowMultiColumnSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <eaf:GridRadioButton ID="radSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Template ID" DataField="Template ID" SortExpression="Template ID" />
                        <asp:BoundField HeaderText="Template Name" DataField="Template Name" SortExpression="Template Name" />
                    </Columns>
                </eaf:EAFGridView>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="ButtonTable4" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="LeftButtons4" runat="server">
                    <eaf:EAFButton ID="btnClose" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                        Text="Close"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="ButtonTableTransform" runat="server">
                    <eaf:EAFButton ID="btnTransform" OnClick="btnTransform_Click" runat="server" CssClass="s_BtnBlue"
                        CausesValidation="false" Text="Transform"></eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
        </div>
    </asp:Panel>
    <%--    Generate Parameter Panel   --%>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" PopupDragHandleControlID="pnlPara"
        runat="server" TargetControlID="btnGenerate" DropShadow="true" PopupControlID="pnlPara"
        BackgroundCssClass="modalBackground" CancelControlID="btnGenerateCancel" />
    <asp:Panel ID="pnlPara" runat="server" CssClass="detailPopup" Style="display: none">
        <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
            Enter Parameter Values</div>
        <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;
            overflow: auto; height: auto">
            <eaf:FormSectionBlock ID="FormSectionPara" Caption="Enter Parameters" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <eaf:DynamicControlPanel ID="dcpDynamicParams2" ControlsWithoutIDs="DontPersist"
                        runat="server">
                    </eaf:DynamicControlPanel>
                </table>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="LeftButtons1" runat="server">
                    <eaf:EAFButton ID="btnGenerateCancel" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                        Text="Close"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="RightButtons1" runat="server">
                    <eaf:EAFButton ID="btn_ExGenerate" OnClick="btn_ExGenerate_Click" runat="server"
                        CssClass="s_BtnBlue" CausesValidation="false" Text="Generate"></eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
        </div>
    </asp:Panel>
    <%--Upload Panel--%>
    <ajaxToolkit:ModalPopupExtender ID="mpeUpload" runat="server" TargetControlID="btnUpload"
        DropShadow="true" PopupControlID="pnlUpload" CancelControlID="btnCloseUpload"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlUpload" runat="server" CssClass="detailPopup" Style="display: none;">
        <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
            Document Upload Information</div>
        <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;">
            <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Node Document Upload" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label18" runat="server" AssociatedControlID="drpFileType1">Document Type:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpFileType1" runat="server" Width="50px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label21" runat="server" AssociatedControlID="fuUpload">File Content:</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:FileUpload ID="fuUpload" runat="server" Width="350px" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="ButtonTable2" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="LeftButtons3" runat="server">
                    <eaf:EAFButton ID="btnCloseUpload" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                        Text="Close"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="RightButtons2" runat="server">
                    <eaf:EAFButton ID="btnUploadFile" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                        Text="Upload" OnClick="btnFileUpload_Click"></eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
        </div>
    </asp:Panel>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerContent" runat="Server">
</asp:Content>
