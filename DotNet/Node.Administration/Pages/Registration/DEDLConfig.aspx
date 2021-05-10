<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="DEDLConfig.aspx.cs" Inherits="Pages_Registration_DEDLConfig" %>

<%@ Register TagPrefix="TABControl" TagName="TAB" Src="~/PageControls/Share/TabControlSR.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:UpdatePanel ID="udpAddOpp" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <ContentTemplate>
            <TABControl:TAB ID="TabControls" runat="server" />
            <eaf:MessageLabel runat="server" ID="MsgError" />
            <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Upload File to Update DEDL Configuration" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td>
                            <asp:FileUpload ID="fuUpload" runat="server" Width="350px" />
                        </td>
                        <td>
                            <eaf:EAFButton ID="btnUpload" runat="server" CssClass="s_BtnGreen" Text="Upload"
                                OnClick="btnUpload_Click" CausesValidation="true"></eaf:EAFButton>
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Data Element List" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvDEList" runat="server" Width="550px" OnRowCommand="egvDEList_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewDataElement" CommandName="ViewDataElement" ImageUrl="~/App_Images/Node/Gen/file_blank.gif"
                                    runat="server" CommandArgument='<%# Eval("ElementIdentifier") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteDataElement" CommandName="DeleteDataElement" ImageUrl="~/App_Images/Node/PnlIco/pnlico_rejected.gif"
                                    runat="server" CommandArgument='<%# Eval("ElementIdentifier") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ElementIdentifier" DataField="ElementIdentifier" />
                        <asp:BoundField HeaderText="ApplicationDomain" DataField="ApplicationDomain" />
                        <asp:BoundField HeaderText="ElementType" DataField="ElementType" />
                        <asp:BoundField HeaderText="Keywords" DataField="Keywords" />
                        <asp:BoundField HeaderText="Owner" DataField="Owner" />
                        <asp:BoundField HeaderText="ElementLabel" DataField="ElementLabel" />
                        <asp:BoundField HeaderText="DefaultValue" DataField="DefaultValue" />
                        <asp:BoundField HeaderText="LastUpdated" DataField="LastUpdated" />
                    </Columns>
                </eaf:EAFGridView>
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td colspan="2">
                            <eaf:EAFButton ID="btnAddElement" runat="server" CssClass="s_BtnGreen" Text="Add Element"
                                CausesValidation="true" OnClick="btnAddElement_OnClick"></eaf:EAFButton>
                            <input type="button" runat="server" id="btnDummy3" style="display: none" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Data Element" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblElementIdentifier" runat="server" AssociatedControlID="lblElementIdentifier2">ElementIdentifier</asp:Label>
                        </td>
                        <td class="lftfld">
                            <asp:Label ID="lblElementIdentifier2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblApplicationDomain" runat="server" AssociatedControlID="txtApplicationDomain">ApplicationDomain</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApplicationDomain" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblElementType" runat="server" AssociatedControlID="txtElementType">ElementType</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtElementType" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription">Description</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblKeywords" runat="server" AssociatedControlID="txtKeywords">Keywords</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeywords" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblOwner" runat="server" AssociatedControlID="txtOwner">Owner</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOwner" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblElementLabel" runat="server" AssociatedControlID="txtElementLabel">ElementLabel</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtElementLabel" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblDefaultValue" runat="server" AssociatedControlID="txtDefaultValue">DefaultValue</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDefaultValue" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblLastUpdated" runat="server" AssociatedControlID="txtLastUpdated">LastUpdated</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastUpdated" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock4" Caption="DataConstrains" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAllowMultiSelect" runat="server" AssociatedControlID="txtApplicationDomain">AllowMultiSelect</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAllowMultiSelect" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblAdditionalValuesIndicator" runat="server" AssociatedControlID="txtAdditionalValuesIndicator">AdditionalValuesIndicator</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdditionalValuesIndicator" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblOptionality" runat="server" AssociatedControlID="txtOptionality">Optionality</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOptionality" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblWildcard" runat="server" AssociatedControlID="txtWildcard">Wildcard</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWildcard" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld" align="left">
                            <asp:Label ID="lblFormatString" runat="server" AssociatedControlID="txtFormatString">FormatString</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFormatString" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="lblValidationRules" runat="server" AssociatedControlID="txtValidationRules">ValidationRules</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidationRules" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock5" Caption="Properties" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvProperty" runat="server" Width="550px" OnRowCommand="egvProperty_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewProperty" CommandName="ViewProerty" ImageUrl="~/App_Images/Node/Gen/file_blank.gif"
                                    runat="server" CommandArgument='<%# Eval("PropertyName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteProperty" CommandName="DeleteProperty" ImageUrl="~/App_Images/Node/PnlIco/pnlico_rejected.gif"
                                    runat="server" CommandArgument='<%# Eval("PropertyName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="PropertyName" DataField="PropertyName" />
                        <asp:BoundField HeaderText="PropertyValue" DataField="PropertyValue" />
                    </Columns>
                </eaf:EAFGridView>
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td>
                        </td>
                        <td>
                            <eaf:EAFButton ID="btnAddProperty" runat="server" CssClass="s_BtnGreen" Text="Add Property"
                                CausesValidation="true" OnClick="btnAddProperty_OnClick"></eaf:EAFButton>
                            <input type="button" runat="server" id="BtnDummy1" style="display: none" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock6" Caption="ElementValues" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvElementValue" runat="server" Width="550px" OnRowCommand="egvElementValue_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewElement" CommandName="ViewElement" ImageUrl="~/App_Images/Node/Gen/file_blank.gif"
                                    runat="server" CommandArgument='<%# Eval("ValueLabel") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteElement" CommandName="DeleteElement" ImageUrl="~/App_Images/Node/PnlIco/pnlico_rejected.gif"
                                    runat="server" CommandArgument='<%# Eval("ValueLabel") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ValueLabel" DataField="ValueLabel" />
                        <asp:BoundField HeaderText="ElementValue" DataField="ElementValue" />
                    </Columns>
                </eaf:EAFGridView>
                <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                    <tr valign="top">
                        <td>
                        </td>
                        <td>
                            <eaf:EAFButton ID="btnAddElementValue" runat="server" CssClass="s_BtnGreen" Text="Add Element Value"
                                CausesValidation="true" OnClick="btnAddElementValue_OnClick"></eaf:EAFButton>
                            <input type="button" runat="server" id="BtnDummy2" style="display: none" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                <eaf:LeftButtons ID="RightButtons1" runat="server">
                    <eaf:EAFButton ID="btnBackToDashboard" runat="server" CssClass="s_BtnGrey" Text="Back To Dashboard"
                        CausesValidation="false" OnClick="btnBackToDashboard_Click"></eaf:EAFButton>
                </eaf:LeftButtons>
                <eaf:RightButtons ID="rightBtnPanel" runat="server">
                    <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click"
                        CausesValidation="true"></eaf:EAFButton>
                </eaf:RightButtons>
            </eaf:ButtonTable>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddDataElement" runat="server" TargetControlID="btnDummy3"
                DropShadow="true" PopupControlID="pnlAddDataElement" BackgroundCssClass="modalBackground"
                PopupDragHandleControlID="pnlAddDataElement" CancelControlID="btnCloseDataElement" />
            <asp:Panel ID="pnlAddDataElement" runat="server" CssClass="DEDLmodalPopup">
                <div class="modalPopupHeader">
                    Add Data Element
                </div>
                <div class="modalPopupContent" style="height: 50px; width: 300px">
                    <eaf:MessageLabel runat="server" ID="MsgDE" />
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblAddElementIdentifier" runat="server" AssociatedControlID="txtAddElementIdentifier">ElementIdentifier</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddElementIdentifier" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modalPopupFooter">
                    <eaf:EAFButton ID="btnSaveAddDataElement" runat="server" CausesValidation="false"
                        CssClass="s_BtnGrey" Text="Save" OnClick="btnSaveAddDataElement_OnClick"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnCloseDataElement" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close"></eaf:EAFButton>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddProperty" runat="server" TargetControlID="BtnDummy1"
                DropShadow="true" PopupControlID="pnlAddProperty" BackgroundCssClass="modalBackground"
                CancelControlID="btnCloseProperty" PopupDragHandleControlID="pnlAddProperty" />
            <asp:Panel ID="pnlAddProperty" runat="server" CssClass="DEDLmodalPopup">
                <div class="modalPopupHeader" runat="server" id="PopHeaderProperty">
                    Property Info
                </div>
                <div class="modalPopupContent" style="height: 50px; width: 300px">
                    <eaf:MessageLabel runat="server" ID="msgP" />
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblPropertyName" runat="server" AssociatedControlID="txtPropertyName">PropertyName</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPropertyName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblPropertyValue" runat="server" AssociatedControlID="txtPropertyValue">PropertyValue</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPropertyValue" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modalPopupFooter">
                    <eaf:EAFButton ID="btnSaveProperty" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Save" OnClick="btnSaveProperty_OnClick"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnSaveAddProperty" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Save" OnClick="btnSaveAddProperty_OnClick"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnCloseProperty" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Close"></eaf:EAFButton>
                </div>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeAddElementValue" runat="server" TargetControlID="BtnDummy2"
                DropShadow="true" PopupControlID="pnlAddElementValue" BackgroundCssClass="modalBackground"
                CancelControlID="btnCloseElementValue" PopupDragHandleControlID="pnlAddElementValue" />
            <asp:Panel ID="pnlAddElementValue" runat="server" CssClass="DEDLmodalPopup">
                <div class="modalPopupHeader" runat="server" id="PopHeaderElementValue">
                    Element Value Info
                </div>
                <div class="modalPopupContent" style="height: 50px; width: 300px">
                    <eaf:MessageLabel runat="server" ID="msgEV" />
                    <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblValueLabel" runat="server" AssociatedControlID="txtValueLabel">ElementValue Label</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtValueLabel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="lblElementValue" runat="server" AssociatedControlID="txtAddElementIdentifier">ElementValue</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtElementValue" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modalPopupFooter">
                    <eaf:EAFButton ID="btnSaveElementValue" runat="server" CausesValidation="false" CssClass="s_BtnGrey"
                        Text="Save" OnClick="btnSaveElementValue_OnClick"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnSaveAddElementValue" runat="server" CausesValidation="false"
                        CssClass="s_BtnGrey" Text="Save" OnClick="btnSaveAddElementValue_OnClick"></eaf:EAFButton>
                    <eaf:EAFButton ID="btnCloseElementValue" runat="server" CausesValidation="false"
                        CssClass="s_BtnGrey" Text="Close"></eaf:EAFButton>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
