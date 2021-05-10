<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Configuration.ascx.cs"
    Inherits="PageControls_WebParts_Configuration" %>
<table cellspacing="0" class="announce">
    <tr>
        <td class="ttl">
            Allows Node Administrators to configure basic node settings.
        </td>
    </tr>
</table>
<asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed"
    Visible="false" />
<asp:Panel ID="PnlConfig" runat="server">
    <table cellspacing="0" class="announce">
        <tr>
            <td class="msg">
                <img id="Img1" src="~/App_Images/Node/PnlIco/pnlico_No1.gif" style="vertical-align: middle;"
                    runat="server" alt="Node Configuration" />
                <asp:Label ID="lblNodeConfig" runat="server">Node Configuration</asp:Label>
            </td>
            <td>
                <%--                <a name="CNF" />--%>
                <asp:ImageButton ID="ImgNode" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="Node Configuration" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img2" src="~/App_Images/Node/PnlIco/pnlico_No2.gif" style="vertical-align: middle;"
                    runat="server" alt="NAAS Configuration" />
                <asp:Label ID="lblNAASConfig" runat="server">NAAS Configuration</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgNAAS" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="NAAS Configuration" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img3" src="~/App_Images/Node/PnlIco/pnlico_No3.gif" style="vertical-align: middle;"
                    runat="server" alt="Email Configuration" />
                <asp:Label ID="lblEmailConfig" runat="server">Email Configuration</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgEmail" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="Email Configuration" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img4" src="~/App_Images/Node/PnlIco/pnlico_No4.gif" style="vertical-align: middle;"
                    runat="server" alt="AppServer Configuration" />
                <asp:Label ID="lblAppServerConfig" runat="server">AppServer Configuration</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgAppServer" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="AppServer Configuration" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img5" src="~/App_Images/Node/PnlIco/pnlico_No5.gif" style="vertical-align: middle;"
                    runat="server" alt="Web Client Configuration" />
                <asp:Label ID="lblWebClientConfig" runat="server">Web Client Configuration</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgWebClient" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="Web Client Configuration" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img6" src="~/App_Images/Node/PnlIco/pnlico_No6.gif" style="vertical-align: middle;"
                    runat="server" alt="Default Settings" />
                <asp:Label ID="lblWebClient" runat="server">Default Settings</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgDefaultSettings" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="Default Settings" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img7" src="~/App_Images/Node/PnlIco/pnlico_No7.gif" style="vertical-align: middle;"
                    runat="server" alt="Operation Manager" />
                <asp:Label ID="Label31" runat="server">Operation Manager</asp:Label>
            </td>
            <td>
                <%--                <a name="CNF" />--%>
                <asp:ImageButton ID="ImgOPManager" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="Operation Manager"
                    OnClick="ImgOPManager_onClick" />
            </td>
        </tr>
        <tr>
            <td class="msg">
                <img id="Img8" src="~/App_Images/Node/PnlIco/pnlico_No8.gif" style="vertical-align: middle;"
                    runat="server" alt="RESTful Services" />
                <asp:Label ID="Label33" runat="server">RESTful Configuration</asp:Label>
            </td>
            <td>
                <asp:ImageButton ID="ImgRESTfulConfig" ImageUrl="~/App_Images/Node/PnlIco/pnlico_settings.gif"
                    Style="vertical-align: middle; cursor: hand" runat="server" AlternateText="RESTful Configuration" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNode" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        Node Configuration</div>
    <div class="Content">
        <eaf:FormSectionBlock ID="sec1" Caption="Node Configuration" runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="lblNodeName" runat="server" AssociatedControlID="txtNodeName">Node Identifier:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeName" runat="server" Width="100px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label4" runat="server" AssociatedControlID="rblNodeStatus">Node Status:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td class="lftfld" valign="top">
                        <asp:RadioButtonList ID="rblNodeStatus" runat="server" RepeatColumns="2" RepeatLayout="Flow">
                            <asp:ListItem Text="Running" />
                            <asp:ListItem Text="Stopped" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr valign="top">
                    <td />
                    <td class="lftfld">
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label5" runat="server" AssociatedControlID="txtStatusMessage">Node Status Message:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStatusMessage" runat="server" Width="400px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td />
                    <td class="lftfld">
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label6" runat="server" AssociatedControlID="txtTokenLifetime">Token Life Time:</asp:Label>
                    </td>
                    <td class="lftfld">
                        <asp:TextBox ID="txtTokenLifetime" runat="server" Width="60px" />
                        (seconds)
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label7" runat="server" AssociatedControlID="txtNodeAddress">Node Address:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeAddress" runat="server" Width="400px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvNodeName" runat="server" ControlToValidate="txtNodeName"
                Display="None" ErrorMessage="Node Identifier must be non-empty" ValidationGroup="NodeConfig" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NodeNameE" TargetControlID="rfvNodeName"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvNodeStatus" runat="server" ControlToValidate="rblNodeStatus"
                Display="None" ErrorMessage="A Node Status Must be Selected" ValidationGroup="NodeConfig" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNodeStatusE" TargetControlID="rfvNodeStatus"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvStatusMessage" runat="server" ControlToValidate="txtStatusMessage"
                Display="None" ErrorMessage="Node Status Message must be non-empty" ValidationGroup="NodeConfig" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvStatusMessageE" TargetControlID="rfvStatusMessage"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RegularExpressionValidator ID="revTokenLifetime" runat="server" ControlToValidate="txtTokenLifetime"
                ValidationGroup="NodeConfig" Display="None" ErrorMessage="Token Life Time must be empty (in order to disable the Token Life Time) or a numeric value"
                ValidationExpression="[0-9]*" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="revTokenLifetimeE" TargetControlID="revTokenLifetime"
                HighlightCssClass="validatorCalloutHighlight" />
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
            <eaf:EAFButton ID="btnCloseNode" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false" OnClientClick="fnSetFocus('SkipLinkTop');"></eaf:EAFButton></div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveNode" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="true" ValidationGroup="NodeConfig" OnClick="btnSaveNode_Click">
            </eaf:EAFButton></div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mdlNode" runat="server" TargetControlID="ImgNode"
    BackgroundCssClass="modalBackground" PopupControlID="pnlNode" DropShadow="true"
    CancelControlID="btnCloseNode" PopupDragHandleControlID="pnlNode" />
<asp:Panel ID="pnlNAAS" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        NAAS Configuration</div>
    <div class="Content">
        <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="NAAS Node Administrator Account"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="lbl" runat="server" AssociatedControlID="txtNodeAdminName">Node Administrator Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeAdminName" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label8" runat="server" AssociatedControlID="txtNodeAdminUserID">Node Administrator User ID:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeAdminUserID" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label9" runat="server" AssociatedControlID="txtNodeAdminPassword">Node Administrator Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNodeAdminPassword" runat="server" Width="300px" TextMode="password" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label10" runat="server" AssociatedControlID="txtNAASServerAddress">NAAS Authentication Server Address:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNAASServerAddress" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label11" runat="server" AssociatedControlID="txtNAASUserMgmAddress">NAAS User Management Server Address:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNAASUserMgmAddress" runat="server" Width="300px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label12" runat="server" AssociatedControlID="txtNAASPolicyMgmAddress">NAAS Policy Management Server Address:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNAASPolicyMgmAddress" runat="server" Width="300px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvNodeAdminUserID" runat="server" ControlToValidate="txtNodeAdminUserID"
                Display="None" ErrorMessage="Node Administrator User ID must be non-empty" ValidationGroup="NodeNAAS" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNodeAdminUserIDE" TargetControlID="rfvNodeAdminUserID"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvNAASServerAddress" runat="server" ControlToValidate="txtNAASServerAddress"
                Display="None" ErrorMessage="NAAS Authentication/Authorization Server Address must be non-empty"
                ValidationGroup="NodeNAAS" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNAASServerAddressE" TargetControlID="rfvNAASServerAddress"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvNAASUserMgmAddress" runat="server" ControlToValidate="txtNAASUserMgmAddress"
                Display="None" ErrorMessage="NAAS User Management Server Address must be non-empty"
                ValidationGroup="NodeNAAS" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNAASUserMgmAddressE"
                TargetControlID="rfvNAASUserMgmAddress" HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvNAASPolicyMgmAddress" runat="server" ControlToValidate="txtNAASPolicyMgmAddress"
                Display="None" ErrorMessage="NAAS Policy Management Server Address must be non-empty"
                ValidationGroup="NodeNAAS" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvNAASPolicyMgmAddressE"
                TargetControlID="rfvNAASPolicyMgmAddress" HighlightCssClass="validatorCalloutHighlight" />
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
            <eaf:EAFButton ID="btnCloseNAAS" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveNAAS" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="true" ValidationGroup="NodeNAAS" OnClick="btnSaveNAAS_Click">
            </eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeNAAS" runat="server" TargetControlID="ImgNAAS"
    PopupControlID="pnlNAAS" DropShadow="true" CancelControlID="btnCloseNAAS" BackgroundCssClass="modalBackground"
    PopupDragHandleControlID="pnlNAAS" />
<asp:Panel ID="pnlEmail" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        Email Configuration</div>
    <div class="Content" style="height: 350px">
        <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Email Server Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="lblHost" runat="server" Text="Host: " Width="150px" AssociatedControlID="txtEmailServerAddress"></asp:Label><span
                            style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailServerAddress" runat="server" Width="200px" />
                    </td>
                    <td class="fld">
                        <asp:Label ID="Label13" runat="server" AssociatedControlID="txtEmailPort">Port:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailPort" runat="server" Width="100px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label14" runat="server" AssociatedControlID="txtEmailUserID">User ID:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailUserID" runat="server" Width="200px" />
                    </td>
                    <td class="fld">
                        <asp:Label ID="Label15" runat="server" AssociatedControlID="txtEmailPassword">Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailPassword" runat="server" Width="100px" TextMode="password" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label32" runat="server" AssociatedControlID="txtEmailFrom">From:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailFrom" runat="server" Width="200px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvEmailServerAddress" runat="server" ControlToValidate="txtEmailServerAddress"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The Email Server Host field must specify the Email Server Host Name or IP Address" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvEmailServerAddressE"
                TargetControlID="rfvEmailServerAddress" HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvEmailPort" runat="server" ControlToValidate="txtEmailPort"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The Email Server Port field must specify the Email Server Port Number" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvEmailPortE" TargetControlID="rfvEmailPort"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RegularExpressionValidator ID="revEmailPort" runat="server" ControlToValidate="txtEmailPort"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The Email Server Port field must specify the Email Server Port Number"
                ValidationExpression="[1-9][0-9]*" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="revEmailPortE" TargetControlID="revEmailPort"
                HighlightCssClass="validatorCalloutHighlight" />
        </eaf:FormSectionBlock>
        <eaf:FormSectionBlock ID="sec2" Caption="User Account Activity Notification Email Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label2" runat="server" Text="Sender: " Width="150px" AssociatedControlID="txtUserEmailSender"></asp:Label><span
                            style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserEmailSender" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label16" runat="server" AssociatedControlID="txtUserCCList">CC List:</asp:Label><br />
                        (Semicolon (;) Separated)
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserCCList" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label17" runat="server" AssociatedControlID="txtUserBCCList">BCC List:</asp:Label><br />
                        (Semicolon (;) Separated)
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserBCCList" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label18" runat="server" AssociatedControlID="txtUserTemplate">Email Template:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserTemplate" runat="server" TextMode="multiLine" Rows="3" Width="350px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvUserEmailSender" runat="server" ControlToValidate="txtUserEmailSender"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The User Name of the sender of an User Account Email must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvUserEmailSenderE" TargetControlID="rfvUserEmailSender"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvUserTemplate" runat="server" ControlToValidate="txtUserTemplate"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The User Email Template must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvUserTemplateE" TargetControlID="rfvUserTemplate"
                HighlightCssClass="validatorCalloutHighlight" />
        </eaf:FormSectionBlock>
        <eaf:FormSectionBlock ID="sec3" Caption="Task Status Notification Email Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label3" runat="server" Text="Sender: " Width="150px" AssociatedControlID="txtTaskEmailSender"></asp:Label><span
                            style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTaskEmailSender" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label19" runat="server" AssociatedControlID="txtTaskCCList">CC List:</asp:Label><br />
                        (Comma Separated)
                    </td>
                    <td>
                        <asp:TextBox ID="txtTaskCCList" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label20" runat="server" AssociatedControlID="txtTaskBCCist">BCC List:</asp:Label><br />
                        (Comma Separated)
                    </td>
                    <td>
                        <asp:TextBox ID="txtTaskBCCist" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td />
                    <td class="lftfld">
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label21" runat="server" AssociatedControlID="txtTaskTemplate">Email Template:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTaskTemplate" runat="server" TextMode="multiLine" Rows="3" Width="350px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvTaskEmailSender" runat="server" ControlToValidate="txtTaskEmailSender"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The User Name of the sender of an Task Status Email must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvTaskEmailSenderE" TargetControlID="rfvTaskEmailSender"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvTaskTemplate" runat="server" ControlToValidate="txtTaskTemplate"
                ValidationGroup="NodeEmail" Display="None" ErrorMessage="The Task Email Template must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvTaskTemplateE" TargetControlID="rfvTaskTemplate"
                HighlightCssClass="validatorCalloutHighlight" />
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
            <eaf:EAFButton ID="btnCloseEmail" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveEmail" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="true" ValidationGroup="NodeEmail" OnClick="btnSaveEmailTemplate_Click">
            </eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeEmail" runat="server" TargetControlID="ImgEmail"
    PopupDragHandleControlID="pnlEmail" PopupControlID="pnlEmail" DropShadow="true"
    CancelControlID="btnCloseEmail" BackgroundCssClass="modalBackground" />
<asp:Panel ID="pnlAppServer" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        AppServer Configuration</div>
    <div class="Content">
        <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Proxy Server Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label30" runat="server" AssociatedControlID="cbProxy">Use Proxy Server</asp:Label>
                    </td>
                    <td class="lftfld">
                        <asp:CheckBox ID="cbProxy" runat="server" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label22" runat="server" AssociatedControlID="txtProxyServerAddress">Proxy Server Address</asp:Label><br />
                        (including port):
                    </td>
                    <td>
                        <asp:TextBox ID="txtProxyServerAddress" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label23" runat="server" AssociatedControlID="txtProxyUserID">Proxy Server User ID:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProxyUserID" runat="server" Width="200px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label24" runat="server" AssociatedControlID="txtProxyPassword">Proxy Server Password:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProxyPassword" runat="server" Width="200px" TextMode="password" />
                    </td>
                </tr>
            </table>
        </eaf:FormSectionBlock>
        <eaf:FormSectionBlock ID="FormSectionBlock4" Caption="Application Server Logs Configuration"
            runat="server" Visible="false">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="lblLevel" runat="server" Text="Admin Console Log Level:" Width="150px"
                            AssociatedControlID="ddlAdminLogLevel"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAdminLogLevel" runat="server" Width="250px">
                            <asp:ListItem Text="DEBUG" Value="5" />
                            <asp:ListItem Text="INFO" Value="4" />
                            <asp:ListItem Text="WARN" Value="3" />
                            <asp:ListItem Text="ERROR" Value="2" />
                            <asp:ListItem Text="FATAL" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label1" runat="server" Text="Client Log Level:" AssociatedControlID="ddlClientLogLevel"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClientLogLevel" runat="server" Width="250px">
                            <asp:ListItem Text="DEBUG" Value="5" />
                            <asp:ListItem Text="INFO" Value="4" />
                            <asp:ListItem Text="WARN" Value="3" />
                            <asp:ListItem Text="ERROR" Value="2" />
                            <asp:ListItem Text="FATAL" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fld">
                        <asp:Label ID="Label25" runat="server" AssociatedControlID="ddlTaskLogLevel">Task Log Level:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTaskLogLevel" runat="server" Width="250px">
                            <asp:ListItem Text="DEBUG" Value="5" />
                            <asp:ListItem Text="INFO" Value="4" />
                            <asp:ListItem Text="WARN" Value="3" />
                            <asp:ListItem Text="ERROR" Value="2" />
                            <asp:ListItem Text="FATAL" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fld">
                        <asp:Label ID="Label26" runat="server" AssociatedControlID="ddlWebServicesLogLevel">Web Services Log Level:</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlWebServicesLogLevel" runat="server" Width="250px">
                            <asp:ListItem Text="DEBUG" Value="5" />
                            <asp:ListItem Text="INFO" Value="4" />
                            <asp:ListItem Text="WARN" Value="3" />
                            <asp:ListItem Text="ERROR" Value="2" />
                            <asp:ListItem Text="FATAL" Value="1" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </eaf:FormSectionBlock>
        <table class="eaf_FormSecTab1" runat="server" id="Table3">
            <tr>
                <td class="eaf_ttl">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="Footer">
        <div class="Left">
            <eaf:EAFButton ID="btnCloseAppServer" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveAppServer" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="false" OnClick="btnSaveAppServer_Click"></eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeAppServer" runat="server" TargetControlID="ImgAppServer"
    PopupControlID="pnlAppServer" DropShadow="true" CancelControlID="btnCloseAppServer"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlAppServer" />
<asp:Panel ID="pnlWebClient" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        Web Client Configuration</div>
    <div class="Content" style="height: 350px">
        <eaf:FormSectionBlock ID="FormSectionBlock5" Caption="Web Services Client Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td colspan="2" class="lftfld">
                        <asp:Label ID="lblWebService" runat="server" Text="Web Service URLS (one per line):"
                            Width="450px" AssociatedControlID="txtClientURLs" />
                    </td>
                </tr>
                <tr valign="top">
                    <td colspan="2">
                        <asp:TextBox ID="txtClientURLs" runat="server" Rows="15" TextMode="multiline" Width="450px" />
                    </td>
                </tr>
            </table>
        </eaf:FormSectionBlock>
        <table class="eaf_FormSecTab1" runat="server" id="Table4">
            <tr>
                <td class="eaf_ttl">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="Footer">
        <div class="Left">
            <eaf:EAFButton ID="btnCloseWebClient" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveWebClient" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="false" OnClick="btnSaveWebClient_Click"></eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeWebClient" runat="server" TargetControlID="ImgWebClient"
    PopupControlID="pnlWebClient" DropShadow="true" CancelControlID="btnCloseWebClient"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlWebClient" />
<asp:Panel ID="pnlDefaultSettings" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        System Default Settings</div>
    <div class="Content">
        <eaf:FormSectionBlock ID="FormSectionBlock6" Caption="Default Settings" runat="server"
            Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label27" runat="server" AssociatedControlID="txtRowNo">Default Row Number for Searching:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRowNo" runat="server" Width="60px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label28" runat="server" AssociatedControlID="txtTopNo">Default Top Number:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTopNo" runat="server" Width="60px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="fld">
                        <asp:Label ID="Label29" runat="server" AssociatedControlID="txtPageSize">Default Page Size:</asp:Label>
                        <span style="color: red">*</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPageSize" runat="server" Width="60px" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="rfvRowNo" runat="server" ControlToValidate="txtRowNo"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default row number for searching must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvRowNoE" TargetControlID="rfvRowNo"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RegularExpressionValidator ID="rxvRowNo" runat="server" ControlToValidate="txtRowNo"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default row number for searching must be a numeric value"
                ValidationExpression="[0-9]*" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rxvRowNoE" TargetControlID="rxvRowNo"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvTopNo" runat="server" ControlToValidate="txtTopNo"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default top number must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvTopNoE" TargetControlID="rfvTopNo"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RegularExpressionValidator ID="rxvTopNo" runat="server" ControlToValidate="txtTopNo"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default top number must be a numeric value"
                ValidationExpression="[0-9]*" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rxvTopNoE" TargetControlID="rxvTopNo"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RequiredFieldValidator ID="rfvPageSize" runat="server" ControlToValidate="txtPageSize"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default page size must be specified" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="rfvPageSizeE" TargetControlID="rfvPageSize"
                HighlightCssClass="validatorCalloutHighlight" />
            <asp:RegularExpressionValidator ID="revPageSize" runat="server" ControlToValidate="txtPageSize"
                ValidationGroup="DefaultSettings" Display="None" ErrorMessage="The default page size must be a numeric value"
                ValidationExpression="[0-9]*" />
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="revPageSizeE" TargetControlID="revPageSize"
                HighlightCssClass="validatorCalloutHighlight" />
        </eaf:FormSectionBlock>
        <table class="eaf_FormSecTab1" runat="server" id="Table5">
            <tr>
                <td class="eaf_ttl">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="Footer">
        <div class="Left">
            <eaf:EAFButton ID="btnCancelSettings" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveSettings" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="true" ValidationGroup="DefaultSettings" OnClick="btnSaveSettings_Click">
            </eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImgDefaultSettings"
    PopupControlID="pnlDefaultSettings" DropShadow="true" CancelControlID="btnCancelSettings"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlDefaultSettings" />
<ajaxToolkit:ModalPopupExtender ID="mdlRESTful" runat="server" TargetControlID="ImgRESTfulConfig"
    PopupControlID="pnlRESTful" DropShadow="true" CancelControlID="btnCloseRESTful"
    BackgroundCssClass="modalBackground" PopupDragHandleControlID="pnlRESTful" />
<asp:Panel ID="pnlRESTful" runat="server" CssClass="detailPopup" Style="display: none">
    <div class="Header">
        RESTful Configuration</div>
    <div class="Content" style="height: 350px">
        <eaf:FormSectionBlock ID="FormSectionBlock7" Caption="RESTful Configuration"
            runat="server" Visible="true">
            <table class="cc_EntryForm" cellspacing="0">
                <tr valign="top">
                    <td class="lftfld">
                        <asp:Label ID="Label35" runat="server" AssociatedControlID="txtRESTfulTitle">Header:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRESTfulTitle" runat="server" Width="250px" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="lftfld">
                        <asp:Label ID="Label34" runat="server" Text="Content:" Width="450px"
                            AssociatedControlID="txtRESTfulDesc" />
                    </td>
                    <td>
                    <asp:TextBox ID="txtRESTfulDesc" runat="server" Rows="15" TextMode="multiline" Width="450px" />
                    </td>
                </tr>
            </table>
        </eaf:FormSectionBlock>
        <table class="eaf_FormSecTab1" runat="server" id="Table6">
            <tr>
                <td class="eaf_ttl">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="Footer">
        <div class="Left">
            <eaf:EAFButton ID="btnCloseRESTful" runat="server" CssClass="s_BtnGrey" Text="Close"
                CausesValidation="false"></eaf:EAFButton>
        </div>
        <div class="Right">
            <eaf:EAFButton ID="btnSaveRESTful" runat="server" CssClass="s_BtnGreen" Text="Save"
                CausesValidation="false" OnClick="btnSaveRESTful_Click"></eaf:EAFButton>
        </div>
    </div>
</asp:Panel>
