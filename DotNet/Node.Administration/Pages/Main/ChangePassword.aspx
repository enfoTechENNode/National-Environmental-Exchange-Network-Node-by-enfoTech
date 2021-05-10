<%@ Page Language="C#" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword_aspx"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="Node" TagName="Footer" Src="~/PageControls/Share/Footer.ascx" %>
<%--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Node Administration Change Password</title>
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
</head>
<body>
    <form id="form1" runat="server">

    <script src="<%= Request.ApplicationPath %>/App_Scripts/EAF/Utils.js" language="javascript"
        type="text/javascript"></script>

    <div style="border: 1px solid #ffffff; background-color: #0055A6;">
        <table border="0" cellpadding="0" cellspacing="0" bgcolor="#0055A6" width="100%">
            <tr valign="top">
                <td colspan="2">
                    <a href="http://www.enfotech.com/">
                        <asp:Image ID="imageHeader" runat="server" ImageUrl="~/App_Images/Node/Header/Header.gif"
                            AlternateText="enfoTech &amp; Consulting Inc." /></a>
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
            <table width="250" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_UpLeft.gif" width="17" height="45"
                            alt="" />
                    </td>
                    <td align="center" style="background-image: url(../../App_Images/Node/Gen/loginblock_UpBG.gif);">
                        <img src="../../App_Images/Node/Gen/loginblock_Title.gif" width="107" height="45"
                            alt="" />
                    </td>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_UpRight.gif" width="21" height="45"
                            alt="" />
                    </td>
                </tr>
                <tr>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_LeftBG.gif);">
                        &nbsp;
                    </td>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_BG.gif);">
                        <table class="cc_EntryForm" cellspacing="0" cellpadding="3" style="margin-left: auto;
                            margin-right: auto;">
                            <tr>
                                <td colspan="2" class="txt">
                                    <strong>To access the Node Administration Utility, please change your password.</strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="fld">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtPassword1">Enter New Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword1" runat="server" Columns="15" TextMode="password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="fld">
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword2">Reenter Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword2" runat="server" Columns="15" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <eaf:EAFButton ID="btnLogin" runat="server" Text=" login " CssClass="s_BtnBlue" OnClick="btnLogin_Click" />
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
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_RightBG.gif);">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_BtmLeft.gif" width="17" height="21"
                            alt="" />
                    </td>
                    <td style="background-image: url(../../App_Images/Node/Gen/loginblock_BtmBG.gif);">
                        <img src="../../App_Images/Node/Gen/loginblock_BtmBG.gif" width="2" height="21" alt="" />
                    </td>
                    <td>
                        <img src="../../App_Images/Node/Gen/loginblock_BtmRight.gif" width="21" height="21"
                            alt="" />
                    </td>
                </tr>
            </table>
            <br />
            <eaf:StylerPanel ID="spHelp" StyleWidth="100%" runat="server">
                <div style="font-size: 75%; padding: 10px;">
                    <table cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="cmtFont2">
                                <strong>Help</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="javascript:showPassword();">
                                    <img src="../../App_Images/Node/PnlIco/pnlico_help.gif" border="0" align="middle"
                                        alt="" />&nbsp;&nbsp;<b>Forgot Your Password&#63;</b></a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <a href="javascript:showHelp('new');">
                                    <img src="../../App_Images/Node/PnlIco/pnlico_help.gif" border="0" align="middle"
                                        alt="" />&nbsp;&nbsp;<b>New to Node Administration&#63;</b></a>
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
                                        height="33" border="0" /></a> <a href="http://www.adobe.com/products/acrobat/readermain.html"
                                            target="_new">
                                            <img src="../../App_Images/logo_acrobat.gif" alt="Click here to download Adobe Reader"
                                                width="91" height="33" border="0" /></a>
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
