<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="OperationConfig.aspx.cs" Inherits="Pages_OperationManager_OperationConfig" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leftContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="udpAddOpp" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnViewDelete" />
        </Triggers>
        <ContentTemplate>
            <eaf:MessageLabel ID="msg" runat="server" />
            <eaf:FormSectionBlock ID="secOPManager" Caption="Operation Manager" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvOperationGrid" runat="server" AllowPaging="false" AllowSorting="true"
                    AllowMultiColumnSorting="true" Width="550px">
                    <Columns>
                        <asp:ButtonField ButtonType="Image" CommandName="Edit" HeaderText="Edit" ImageUrl="~/App_Images/Node/Gen/file_blank.gif" />
                        <asp:BoundField HeaderText="Operation ID" DataField="ID" />
                        <asp:BoundField HeaderText="Operation Name" DataField="NAME" />
                    </Columns>
                </eaf:EAFGridView>
            </eaf:FormSectionBlock>
            <%--Operation Functions--%>
            <eaf:FormSectionBlock ID="FormSectionBlockOperation" Caption="Configure Operation"
                runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="fld" style="vertical-align: middle">
                            <asp:Label ID="lblAddOperationID" runat="server" AssociatedControlID="lblAddOperationIDValue">Operation ID</asp:Label>
                        </td>
                        <td class="lftfld" style="vertical-align: middle">
                            <asp:Label ID="lblAddOperationIDValue" ForeColor="Blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fld" style="vertical-align: middle">
                            <asp:Label ID="lblOPName" runat="server" AssociatedControlID="lblOPNameValue">Operation Name</asp:Label>
                        </td>
                        <td class="lftfld" style="vertical-align: middle">
                            <asp:Label ID="lblOPNameValue" ForeColor="Blue" runat="server">Operation Name</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="fld" style="vertical-align: middle">
                            <asp:Label ID="lblUploadEnable" runat="server" Text="Enable Upload / Generate" AssociatedControlID="rdoOperation"></asp:Label>
                        </td>
                        <td class="lftfld" style="vertical-align: middle">
                            <asp:RadioButtonList runat="server" ID="rdoOperation" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rdoOperation_OnSelectedIndexChanged">
                                <asp:ListItem Text="Upload" Value="submit"></asp:ListItem>
                                <asp:ListItem Text="Generate" Value="solicit"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fld" style="vertical-align: middle">
                            <asp:Label ID="lblEnableSubmit" runat="server" AssociatedControlID="chkAddEnableSubmit">Enable Submit</asp:Label>
                        </td>
                        <td class="lftfld" style="vertical-align: middle">
                            <asp:CheckBox ID="chkAddEnableSubmit" AutoPostBack="true" OnCheckedChanged="chkAddEnableSubmit_CheckedChanged"
                                runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblEnableGetStatus" runat="server" AssociatedControlID="chkEnableGetStatus">Enable GetStatus</asp:Label>
                        </td>
                        <td class="lftfld">
                            <asp:CheckBox ID="chkEnableGetStatus" AutoPostBack="true" OnCheckedChanged="chkEnableGetStatus_CheckedChanged"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <%--Upload Section  --%>
            <eaf:FormSectionBlock ID="FormSectionBlockAddUpload" Caption="Upload" runat="server"
                Visible="false">
                <eaf:EAFGridView ID="egvParameter" runat="server" AllowPaging="true" AllowSorting="true"
                    AllowMultiColumnSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <eaf:GridRadioButton ID="radSelectParameter" runat="server" Visible="true" GroupName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Parameter ID" DataField="id" SortExpression="id" />
                        <asp:BoundField HeaderText="Parameter Name" DataField="ParameterName" SortExpression="ParameterName" />
                        <asp:BoundField HeaderText="XPath" DataField="XPath" SortExpression="XPath" />
                    </Columns>
                </eaf:EAFGridView>
                <eaf:ButtonTable ID="ButtonTable2" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtons3" runat="server">
                        <eaf:EAFButton ID="btnAddOppParsingParameter" runat="server" CssClass="s_BtnGreen"
                            Text="Add" CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                        <eaf:EAFButton ID="btnDeleteParameter" runat="server" CssClass="s_BtnRed" Text="Delete"
                            CausesValidation="false" OnClick="btnDeleteParameter_Click"></eaf:EAFButton>
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <%--Submit Section--%>
            <eaf:FormSectionBlock ID="FormSectionBlockAddSubmit" Caption="Submit" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAddSubmitURL" runat="server" AssociatedControlID="txtAddSubmitURL">URL</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSubmitURL" Width="200px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAddSubmitUsername" runat="server" AssociatedControlID="txtAddSubmitUsername">User Name</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSubmitUsername" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAddSubmitPassword" runat="server" AssociatedControlID="txtAddSubmitPassword">Password</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSubmitPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="Label6" runat="server" AssociatedControlID="txtDomainName">Domain Name</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomainName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblDataFlow" runat="server" AssociatedControlID="txtDataFlow">Data Flow Name</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataFlow" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="Label7" runat="server" AssociatedControlID="txtFlowOp">Flow Operation Name</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFlowOp" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <%--GetStatus Section --%>
            <eaf:FormSectionBlock ID="FormSectionBlockGetStatus" Caption="GetStatus" runat="server"
                Visible="false">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label8" runat="server" AssociatedControlID="txtGetStatusComplete">Complete Status:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGetStatusComplete" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label9" runat="server" AssociatedControlID="txtGetStatusError">Error Status:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtGetStatusError" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <%--View Section --%>
            <eaf:FormSectionBlock ID="FormSectionBlockViewAddViewStyle" Caption="Style Sheets"
                runat="server" Visible="true">
                <eaf:EAFGridView ID="grvViewStyleSheets" runat="server" AllowPaging="true" AllowSorting="true"
                    AllowMultiColumnSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <eaf:GridRadioButton ID="radSelect" runat="server" Visible="true" GroupName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Template ID" DataField="Template ID" SortExpression="Template ID" />
                        <asp:BoundField HeaderText="Template Name" DataField="Template Name" SortExpression="Template Name" />
                    </Columns>
                </eaf:EAFGridView>
                <eaf:ButtonTable ID="btnTableViewStyle" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtonsViewStyleSheets" runat="server">
                        <eaf:EAFButton ID="btnAddOppViewUpload" runat="server" CssClass="s_BtnGreen" Text="Upload"
                            CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                        <eaf:EAFButton ID="btnViewDelete" OnClick="btnViewDelete_Click" runat="server" CssClass="s_BtnRed"
                            Text="Delete" CausesValidation="false"></eaf:EAFButton>
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <%--Validation Section --%>
            <eaf:FormSectionBlock ID="FormSectionBlockValidation" Caption="Validation Rule" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="grvViewValidationRule" runat="server" AllowPaging="true" AllowSorting="true"
                    AllowMultiColumnSorting="true">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <eaf:GridRadioButton ID="radSelectValidationRule" runat="server" Visible="true" GroupName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Validation Rule ID" DataField="Template ID" SortExpression="Template ID" />
                        <asp:BoundField HeaderText="Validation Name" DataField="Template Name" SortExpression="Template Name" />
                    </Columns>
                </eaf:EAFGridView>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtons2" runat="server">
                        <eaf:EAFButton ID="btnAddOppValidation" runat="server" CssClass="s_BtnGreen" Text="Upload"
                            CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                        <eaf:EAFButton ID="btnValidationDelete" OnClick="btnValidationDelete_Click" runat="server"
                            CssClass="s_BtnRed" Text="Delete" CausesValidation="false"></eaf:EAFButton>
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="btnTableAddOperation" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="LeftButtonsAddClose" runat="server">
                    <eaf:EAFButton ID="btnAddOppBack" OnClick="btnAddOppBack_Click" runat="server" CssClass="s_BtnGrey"
                        Text="Back" CausesValidation="false"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="rightBtnPanelAddSave" runat="server">
                    <eaf:EAFButton ID="btnAddOperation" runat="server" CssClass="s_BtnGold" Text="Add Operation"
                        CausesValidation="true" OnClick="btnAddOperation_Click"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnDelete" runat="server" CssClass="s_BtnRed" Text="Delete Operation"
                        CausesValidation="true" OnClick="btnDelete_Click"></eaf:EAFButton>
                    <ajaxToolkit:ConfirmButtonExtender ID="ajxConfirmDelete" runat="server" TargetControlID="btnDelete"
                        ConfirmText="Are you sure to delete this operation?">
                    </ajaxToolkit:ConfirmButtonExtender>
                    <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnBlue" Text="Save Operation"
                        CausesValidation="true" OnClick="btnSave_Click"></eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
            <%--PANELS--%>
            <%--Upload View Style Sheet Panel--%>
            <ajaxToolkit:ModalPopupExtender ID="mpeUploadViewUpload" runat="server" TargetControlID="btnAddOppViewUpload"
                DropShadow="true" PopupControlID="pnlViewUpload" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="pnlViewUpload" runat="server" CssClass="detailPopup" Style="display: none;">
                <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
                    Document Upload Information</div>
                <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;">
                    <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Style Sheet Document Upload"
                        runat="server" Visible="true">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="lblStyleFileName" runat="server">Style Sheet Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStyleFileName" runat="server" Width="200px" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label5" runat="server" AssociatedControlID="fuStyleUpload">File Content:</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:FileUpload ID="fuStyleUpload" runat="server" Width="350px" />
                                </td>
                            </tr>
                        </table>
                    </eaf:FormSectionBlock>
                    <eaf:ButtonTable ID="ButtonTable3" runat="server" TableWidth="600px">
                        <eaf:LeftButtons ID="LeftButtons1" runat="server">
                            <eaf:EAFButton ID="EAFButton3" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                                Text="Close"></eaf:EAFButton>
                        </eaf:LeftButtons>
                        <eaf:RightButtons ID="RightButtons3" runat="server">
                            <eaf:EAFButton ID="btnUpload" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                                Text="Upload" OnClick="btnUpload_Click"></eaf:EAFButton>
                        </eaf:RightButtons>
                    </eaf:ButtonTable>
                </div>
            </asp:Panel>
            <%--Generate Parameters Panel --%>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddParameters" runat="server" TargetControlID="btnAddOppParsingParameter"
                DropShadow="true" PopupControlID="pnlAddParameters" BackgroundCssClass="modalBackground"
                CancelControlID="btnCloseParamters" />
            <asp:Panel ID="pnlAddParameters" runat="server" CssClass="detailPopup" Style="display: none;">
                <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
                    Add Parameters Information</div>
                <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;">
                    <eaf:FormSectionBlock ID="FormSectionBlock4" Caption="Validation Rule Upload" runat="server"
                        Visible="true">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label3" runat="server">Parameters Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtParameterName" runat="server" Width="200px" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label4" runat="server">XML XPath:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtXPath" runat="server" Width="200px" />
                                </td>
                            </tr>
                        </table>
                    </eaf:FormSectionBlock>
                    <eaf:ButtonTable ID="ButtonTable5" runat="server" TableWidth="600px">
                        <eaf:LeftButtons ID="LeftButtons5" runat="server">
                            <eaf:EAFButton ID="btnCloseParamters" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                                Text="Close"></eaf:EAFButton>
                        </eaf:LeftButtons>
                        <eaf:RightButtons ID="RightButtons2" runat="server">
                            <eaf:EAFButton ID="btnAddParameter" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                                Text="Add" OnClick="btnAddParameter_Click"></eaf:EAFButton>
                        </eaf:RightButtons>
                    </eaf:ButtonTable>
                </div>
            </asp:Panel>
            <%--Upload Validation Panel --%>
            <ajaxToolkit:ModalPopupExtender ID="mpeUploadValidationUpload" runat="server" TargetControlID="btnAddOppValidation"
                DropShadow="true" PopupControlID="pnlValidationUpload" BackgroundCssClass="modalBackground"
                CancelControlID="btnCloseUpload2" />
            <asp:Panel ID="pnlValidationUpload" runat="server" CssClass="detailPopup" Style="display: none;">
                <div style="padding: 5px; background-color: #32528E; font-size: large; color: #FFFFFF;">
                    Document Upload Information</div>
                <div style="padding: 20px; border: 1px solid #CCCCCC; background-color: #ffffdd;">
                    <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Style Sheet Document Upload"
                        runat="server" Visible="true">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label1" runat="server">Validation Rule Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValidationName" runat="server" Width="200px" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                </td>
                                <td colspan="3">
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="fuStyleUpload">File Content:</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:FileUpload ID="FileUploadValidation" runat="server" Width="350px" />
                                </td>
                            </tr>
                        </table>
                    </eaf:FormSectionBlock>
                    <eaf:ButtonTable ID="ButtonTable4" runat="server" TableWidth="600px">
                        <eaf:LeftButtons ID="LeftButtons4" runat="server">
                            <eaf:EAFButton ID="btnCloseUpload2" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
                                Text="Close"></eaf:EAFButton>
                        </eaf:LeftButtons>
                        <eaf:RightButtons ID="RightButtons1" runat="server">
                            <eaf:EAFButton ID="btnUpload2" runat="server" CssClass="s_BtnGreen" CausesValidation="false"
                                Text="Upload" OnClick="btnUpload2_Click"></eaf:EAFButton>
                        </eaf:RightButtons>
                    </eaf:ButtonTable>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
