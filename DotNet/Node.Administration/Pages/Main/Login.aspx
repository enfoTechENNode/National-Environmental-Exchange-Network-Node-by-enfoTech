<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="Login_aspx" AutoEventWireup="false" %>

<%@ Register TagPrefix="Node" TagName="Footer" Src="~/PageControls/Share/Footer.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Node Administration Login</title>
    <%--    <meta http-equiv="X-UA-Compatible" content="IE=9,8" />--%>
    <style type="text/css">
        body
        {
            margin: 0;
        }
        .MultiPnl
        {
            margin: 0;
            padding: 0;
            width: 100%;
        }
        .MultiPnlCol
        {
            margin: 0;
            padding: 0;
            vertical-align: top;
            padding: 15px 20px;
            width: 100%;
            height: 100%;
        }
        .MultiPnlColLeft
        {
            border-right: 1px solid #CCCCCC;
            background-color: #FAFAFA;
            width: auto;
        }
        .HeaderText
        {
            font: 8pt Arial,Helvetica,sans-serif;
            color: #ffffff;
        }
    </style>
    <%--    <script src="http://ie.microsoft.com/testdrive/HTML5/CompatInspector/inspector.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid #ffffff; background-color: #0055A6;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr valign="top">
                <td colspan="2">
                    <a href="http://www.enfotech.com/">
                        <asp:Image ID="imageHeader" runat="server" ImageUrl="~/App_Images/Node/Header/Header.gif"
                            AlternateText="enfoTech &#38; Consulting Inc." /></a>
                </td>
            </tr>
            <tr class="HeaderText" valign="top" style="background-color: #396EA0">
                <td>
                    Node Administration
                </td>
                <td align="right">
                    <a href="http://www.enfotech.com/" target="_blank" style="color: White;">enfoTech &amp;
                        Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx"
                            style="color: White;" target="_blank">Contact Us</a>
                </td>
            </tr>
        </table>
    </div>
    <eaf:MultiColumnPanel ID="multiColPnl" PanelCss="MultiPnl" AllColumnCss="MultiPnlCol"
        LeftColumnCss="MultiPnlColLeft" runat="server">
        <asp:Panel ID="leftPnl" runat="server" Style="padding: 0px;">
            <script type="text/javascript">
                function showPassword() {
                    Utils.openDialogWin('<%= Request.ApplicationPath %>/Pages/Main/ForgotPassword.aspx', 600, 325, 'RetrievePassword');
                }
                function showHelp(key) {
                    Utils.openDialogWin('<%= Request.ApplicationPath %>/Pages/Help/GenericHelp.aspx?helpKey=' + key, 500, 400, 'SystemHelp');
                }
                function showRestPassword() {
                    Utils.openDialogWin('<%= Request.ApplicationPath %>/Pages/Main/RestPassword.aspx', 600, 325, 'RetrievePassword');
                }

            </script>
            <table width="250" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_UpLeft.gif" width="17" height="45"
                            alt="" />
                    </td>
                    <td align="center" style="background-image: url(../../App_Images/Node/Gen/loginblock_UpBG.gif);
                        background-repeat: repeat-x;">
                        <img src="../../App_Images/Node/Gen/loginblock_Title.gif" width="107" height="45"
                            alt="" />
                    </td>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_UpRight.gif" width="21" height="45"
                            alt="" />
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_LeftBG.gif)">
                        &nbsp;
                    </td>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_BG.gif);">
                        <table class="cc_EntryForm" cellspacing="0" cellpadding="3" style="margin-left: auto;
                            margin-right: auto;">
                            <tr>
                                <td colspan="2" class="txt">
                                    <strong>To access the Node Administration Utility, please enter your username and password.</strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="fld">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtUsername">Username</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsername" Columns="15" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fld">
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Columns="15" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <eaf:EAFButton ID="btnLogin" runat="server" Text=" Login " CssClass="s_BtnBlue" OnClick="btnLogin_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: small; font-family: Arial, Courier; font-weight: bold;
                                    color: Red;">
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_RightBG.gif)">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_BtmLeft.gif" width="17" height="21"
                            alt="" />
                    </td>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_BtmBG.gif);
                        background-repeat: repeat-x">
                        <img src="../../App_Images/Node/Gen/loginblock_BtmBG.gif" width="2" height="21" alt="" />
                    </td>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_BtmRight.gif" width="21" height="21"
                            alt="" />
                    </td>
                </tr>
            </table>
            <br />
            <eaf:StylerPanel ID="StylerPanel1" StyleWidth="100%" runat="server" PanelStyle="smoky">
                <div style="font-size: 75%; padding: 10px;">
                    <table cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="cmtFont2" colspan="2">
                                <strong>System Info</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="cmtFont2">
                                Version:
                            </td>
                            <td class="cmtFont2">
                                <%=GetBuildVersion()%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cmtFont2">
                                Build Date:
                            </td>
                            <td class="cmtFont2">
                                <%=GetBuildDate()%>
                            </td>
                        </tr>
                    </table>
                </div>
            </eaf:StylerPanel>
            <br />
            <eaf:StylerPanel ID="spHelp" StyleWidth="100%" runat="server" PanelStyle="smoky">
                <div style="font-size: 75%; padding: 10px;">
                    <table cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="cmtFont2">
                                <strong>Help</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="javascript:showPassword();" >&nbsp;&nbsp;<b>Change
                                        Password</b></a>   
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="javascript:showRestPassword();">
                                   <%-- <img src="../../App_Images/Node/PnlIco/pnlico_help.gif" alt="" />--%>&nbsp;&nbsp;<b>Forgot
                                        Password</b></a>
                            </td>
                        </tr>
                    </table>
                </div>
            </eaf:StylerPanel>
            <br />
            <eaf:StylerPanel ID="spOther" StyleWidth="100%" runat="server">
                <div style="font-size: 75%; padding: 10px;">
                    <table cellspacing="0" cellpadding="2">
                        <tr>
                            <td>
                                <a href="http://www.microsoft.com/windows/ie/" target="_new">
                                    <img src="../../App_Images/logo_ie.gif" alt="Click here to download IE browser" width="91"
                                        height="33" />
                                </a><a href="http://www.adobe.com/products/acrobat/readermain.html" target="_new">
                                    <img src="../../App_Images/logo_acrobat.gif" alt="Click here to download Adobe Reader"
                                        width="91" height="33" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </div>
            </eaf:StylerPanel>
        </asp:Panel>
        <asp:Panel ID="rightPnl" runat="server" Style="font-size: 75%; padding: 0 10px;">
            <eaf:TextResourceLabel ID="txtInformation" runat="server" PageKey="main" />
        </asp:Panel>
    </eaf:MultiColumnPanel>
    <Node:Footer ID="footer" runat="server" />
    </form>
</body>
</html>
