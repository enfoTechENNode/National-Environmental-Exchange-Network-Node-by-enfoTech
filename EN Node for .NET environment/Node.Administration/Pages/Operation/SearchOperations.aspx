<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="SearchOperations.aspx.cs" Inherits="SearchOperations_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
        Visible="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="Node Operation Search Criteria" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlOperationName">Operation Name:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlOperationName" runat="server" Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="ddlOpType">Operation Type:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOpType" runat="server">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="WEB_SERVICE">Web Service</asp:ListItem>
                        <asp:ListItem Value="SCHEDULED_TASK">Scheduled Task</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="fld">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="ddlStatus">Operation Status:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="Running">Running</asp:ListItem>
                        <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="ddlWebService">Web Method:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlWebService" runat="server" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnBackToDashBoard" runat="server" OnClick="btnBackToDashBoard_Click"
                Text="Back to DashBoard" CssClass="s_BtnGrey" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSearch" runat="server" CssClass="s_BtnGreen" Text="Search"
                OnClick="btnSearch_Click"></eaf:EAFButton>
            <eaf:EAFButton ID="btnCreate" runat="server" CssClass="s_BtnGreen" Text="Create New Operation"
                OnClick="btnCreate_Click" CausesValidation="false" />
            <eaf:EAFButton ID="btnUpload" runat="server" CssClass="s_BtnGreen" Text="Create Operation by XML"
                CausesValidation="false" />
        </eaf:RightButtons>
    </eaf:ButtonTable>
    <br />
    <div style="margin-left: 30px">
        <eaf:EAFGridView ID="egvOperationGrid" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="550px" OnRowCommand="egvOperationGrid_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="DownLoad">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnTrans" CommandName="EditOperation" ImageUrl="~/App_Images/Node/Gen/file_blank.gif"
                            runat="server" CommandArgument='<%# Eval("OPERATION_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Operation" DataField="OPERATION_NAME" SortExpression="OPERATION_NAME" />
                <asp:BoundField HeaderText="Type" DataField="OPERATION_TYPE" SortExpression="OPERATION_TYPE" />
                <asp:BoundField HeaderText="Web Service Name" DataField="WEB_SERVICE_NAME" SortExpression="WEB_SERVICE_NAME" />
                <asp:BoundField HeaderText="Status" DataField="OPERATION_STATUS_CD" SortExpression="OPERATION_STATUS_CD" />
                <asp:BoundField HeaderText="Status Message" DataField="OPERATION_STATUS_MSG" SortExpression="OPERATION_STATUS_MSG" />
            </Columns>
        </eaf:EAFGridView>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="mpeUpload" runat="server" TargetControlID="btnUpload"
        DropShadow="true" PopupControlID="pnlUpload" CancelControlID="btnCloseUpload"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlUpload" runat="server" CssClass="detailPopup" Style="display: none;">
        <div class="Header">
            Document Upload Information</div>
        <div class="Content">
            <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Operation XML file Upload"
                runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
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
                    Text="Close"></eaf:EAFButton>
            </div>
            <div class="Right">
                <eaf:EAFButton ID="btnUploadFile" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                    Text="Upload" OnClick="btnFileUpload_Click"></eaf:EAFButton>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
