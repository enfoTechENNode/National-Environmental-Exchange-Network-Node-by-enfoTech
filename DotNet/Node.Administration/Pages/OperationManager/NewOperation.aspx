<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NewOperation.aspx.cs" Inherits="Pages_OperationManager_NewOperation" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leftContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="udpAddOpp" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <ContentTemplate>
            <eaf:MessageLabel ID="msg" runat="server" />
            <%--Operation Functions--%>
            <eaf:FormSectionBlock ID="sec1" Caption="Configure Operation" runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblAddOperationID" runat="server" AssociatedControlID="lblAddOperationIDValue">Operation ID</asp:Label>
                        </td>
                        <td class="lftfld">
                            <asp:Label ID="lblAddOperationIDValue" ForeColor="Blue" Font-Size="Small" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblNodeName" runat="server" AssociatedControlID="ddlAddOperationName">Operation Name</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAddOperationName" OnSelectedIndexChanged="ddlAddOperationName_SelectedIndexChanged"
                                AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="top">
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
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblEnableSubmit" runat="server" AssociatedControlID="chkAddEnableSubmit">Enable Submit</asp:Label>
                        </td>
                        <td class="lftfld">
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
            <%--Upload Section--%>
            <eaf:FormSectionBlock ID="FormSectionBlockAddUpload" Caption="Upload" runat="server"
                Visible="false">
                <eaf:ButtonTable ID="ButtonTable2" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtons3" runat="server">
                        <eaf:EAFButton ID="btnAddOppParsingParameter" runat="server" CssClass="s_BtnGreen"
                            Text="Add" CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <%--Submit Section--%>
            <eaf:FormSectionBlock ID="FormSectionBlockAddSubmit" Caption="Submit" runat="server"
                Visible="false">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAddSubmitURL" runat="server" AssociatedControlID="txtAddSubmitURL">URL</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddSubmitURL" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="txtAddSubmitPassword" runat="server"  TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label6" runat="server" AssociatedControlID="txtDomainName">Domain Name</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDomainName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblDataFlow" runat="server" AssociatedControlID="txtDataFlow">Data Flow Name</asp:Label>
                            <span style="color: red">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataFlow" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
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
                <eaf:ButtonTable ID="btnTableViewStyle" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtonsViewStyleSheets" runat="server">
                        <eaf:EAFButton ID="btnAddOppViewUpload" runat="server" CssClass="s_BtnGreen" Text="Upload"
                            CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <%--Validation Section --%>
            <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Validation Rule" runat="server"
                Visible="true">
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="208px">
                    <eaf:LeftButtons ID="LeftButtons2" runat="server">
                        <eaf:EAFButton ID="btnAddOppValidation" runat="server" CssClass="s_BtnGreen" Text="Upload"
                            CausesValidation="false"></eaf:EAFButton>&nbsp;&nbsp;
                    </eaf:LeftButtons>
                </eaf:ButtonTable>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="btnTableAddOperation" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="LeftButtonsAddClose" runat="server">
                    <eaf:EAFButton ID="btnAddOppBack" OnClick="btnAddOppBack_Click" runat="server" CssClass="s_BtnGrey"
                        Text="Back" CausesValidation="false"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="rightBtnPanelAddSave" runat="server">
                    <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnBlue" Text="Save Operation"
                        CausesValidation="true" ValidationGroup="NodeConfig" OnClick="btnSave_Click">
                    </eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
            <%--Upload View Style Sheet Panel--%>
            <ajaxToolkit:ModalPopupExtender ID="mpeUploadViewUpload" runat="server" TargetControlID="btnAddOppViewUpload"
                DropShadow="true" PopupControlID="pnlViewUpload" BackgroundCssClass="modalBackground"
                CancelControlID="btnCloseUpload" />
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
                            <eaf:EAFButton ID="btnCloseUpload" runat="server" CssClass="s_BtnGrey" CausesValidation="false"
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
                    <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Validation Rule Upload" runat="server"
                        Visible="true">
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
<asp:Content ID="Content4" ContentPlaceHolderID="footerContent" runat="Server">
</asp:Content>
