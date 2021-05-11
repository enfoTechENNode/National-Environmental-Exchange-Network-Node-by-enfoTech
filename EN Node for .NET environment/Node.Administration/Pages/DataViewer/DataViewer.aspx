<%@ Page Title="Data Viewer" Language="C#" MasterPageFile="~/MasterPages/Admin.master"
    AutoEventWireup="true" CodeFile="DataViewer.aspx.cs" Inherits="Pages_DataViewer_DataViewer" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <eaf:FormSectionBlock ID="sec1" Caption="Data Flow Selection" runat="server">
        <eaf:InputFormTable ID="inputForm" runat="server" FieldNameWidth="200px">
            <eaf:FormInputField ID="FormInputField2" runat="server" FieldName="Data Flow:">
                <asp:DropDownList ID="DropDataFlow" runat="server" Width="300px" AutoPostBack="true"
                    OnSelectedIndexChanged="DropDataFlow_OnSelectedIndexChanged">
                </asp:DropDownList>
            </eaf:FormInputField>
            <eaf:FormInputField ID="FormInputField1" runat="server" FieldName="Table:">
                <asp:DropDownList ID="DropTables" runat="server" Width="300px" AutoPostBack="true"
                    OnSelectedIndexChanged="DropTables_OnSelectedIndexChanged">
                </asp:DropDownList>
            </eaf:FormInputField>
        </eaf:InputFormTable>
    </eaf:FormSectionBlock>
    <eaf:FormSectionBlock ID="sec2" Caption="Data Filter" runat="server">
        <eaf:InputFormTable ID="DataFilterTable" runat="server" FieldNameWidth="200px">
        </eaf:InputFormTable>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server">
        <eaf:LeftButtons ID="RightButtons1" runat="server">
            <eaf:EAFButton ID="btnSearch" runat="server" Text="Search" CssClass="s_BtnGreen"
                OnClick="btnSearch_OnClick"></eaf:EAFButton>
            <%--            <eaf:EAFButton ID="btnDelete" runat="server" Text="Delete" CssClass="s_BtnRed" OnClick="btnDelete_OnClick">
            </eaf:EAFButton>--%>
            <eaf:EAFButton ID="btnExport" runat="server" Text="Export" CssClass="s_BtnGold">
            </eaf:EAFButton>
            <eaf:EAFButton ID="btnBackToOp" runat="server" Text="Back To Operation Manager" CssClass="s_BtnGreen"
                OnClick="btnBackToOp_OnClick"></eaf:EAFButton>
        </eaf:LeftButtons>
        <eaf:RightButtons ID="RightButtons2" runat="server">
        </eaf:RightButtons>
    </eaf:ButtonTable>
    <p>
    </p>
    <div style="margin-left: 30px">
        <eaf:MessageLabel ID="msgNoRecordFound" runat="server" MsgContent="No Record Found"
            Visible="false" />
        <eaf:EAFGridView ID="egvData" runat="server" OnRowDataBound="egvData_RowDataBound"
            OnRowCommand="egvData_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="checked" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </eaf:EAFGridView>
    </div>
    <!-- Table Column Selection -->
    <ajaxToolkit:ModalPopupExtender ID="ModalColSelection" runat="server" TargetControlID="btnExport"
        PopupControlID="pnlPopupColSelection" DropShadow="true" BackgroundCssClass="modalBackground"
        CancelControlID="btnClosepnlPopupColSelection" />
    <asp:Panel ID="pnlPopupColSelection" runat="server" CssClass="detailPopupColSel"
        Style="display: none">
        <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
            Table Column Selection</div>
        <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;
            overflow: auto; height: 250px; font-size: small">
            <asp:CheckBoxList runat="server" ID="chklistColumns">
            </asp:CheckBoxList>
        </div>
        <div style="padding: 5px; background-color: #32528E; color: #FFFFFF; text-align: right;">
            <eaf:EAFButton ID="EAFButton1" runat="server" CausesValidation="false" CssClass="s_BtnGreen"
                Text="Select" OnClick="btnSelection_OnClick"></eaf:EAFButton>
            <eaf:EAFButton ID="btnClosepnlPopupColSelection" runat="server" CausesValidation="false"
                CssClass="s_BtnGrey" Text="Close"></eaf:EAFButton>
        </div>
    </asp:Panel>
    <input type="button" runat="server" id="btnDump" style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalChildTable" runat="server" TargetControlID="btnDump"
        PopupControlID="pnlPopupChildTable" DropShadow="true" CancelControlID="btnClosepnlPopupChildTable"
        PopupDragHandleControlID="pnlPopupChildTable" />
    <asp:Panel ID="pnlPopupChildTable" runat="server" CssClass="MsgPopup">
        <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
            Detail Information</div>
        <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;
            overflow: auto; height: 250px;">
            <eaf:EAFGridView ID="egvDataDetail" runat="server" AllowSorting="true" AutoGenerateColumns="true"
                AllowMultiColumnSorting="true" PageSize="25" />
        </div>
        <div style="padding: 5px; background-color: #32528E; color: #FFFFFF; text-align: right;">
            <eaf:EAFButton ID="btnClosepnlPopupChildTable" runat="server" CausesValidation="false"
                CssClass="s_BtnGrey" Text="Close"></eaf:EAFButton>
        </div>
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" CausesValidation="false" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
        DropShadow="true" PopupControlID="pnlPopup" CancelControlID="btnCloseDetail"
        PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup" runat="server" CssClass="detailPopup" Style="display: none;
        width: 400px">
        <div class="Header">Data Viewer</div>
        <div class="Content">
            <div id="MsgDtl" runat="server" style="width: 360px; font: normal 80% Tahoma,Arial,Helvetica,sans-serif;" />
        </div>
        <div class="Footer">
            <div class="Right">
                <eaf:EAFButton ID="btnCloseDetail" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                    Text="Close"></eaf:EAFButton>
            </div>
        </div>
    </asp:Panel>
    <%--<script type="text/javascript">        Utils.FixGVHeader('<%=egvData.ClientID %>');</script>--%>
</asp:Content>
