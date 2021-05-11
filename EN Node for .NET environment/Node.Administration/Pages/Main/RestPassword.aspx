<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestPassword.aspx.cs" Inherits="RestPassword_aspx" %>

<%@ Register TagPrefix="Node" TagName="Footer" Src="~/PageControls/Share/Footer.ascx" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forgot PWD</title>
</head>
<body>
    <form id="form1" runat="server">

    <script src="<%= Request.ApplicationPath %>/App_Scripts/Node/Utils.js" language="javascript"
        type="text/javascript"></script>

    <div id="cc_PACSHB_2">
        <table id="cc_PACSHB_TB" cellspacing="2" cellpadding="2" border="0">
            <tr>
                <td align="left">
                    <img src="<%= Request.ApplicationPath %>/App_Images/Node/HBIco_Qmark.gif" align="top"
                        alt="" />&nbsp;&nbsp;Forgot Your Password?
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="2" cellspacing="8" width="98%" align="center">
        <tr>
            <td>
                <eaf:StylerPanel ID="sp1" PanelStyle="yellowbubble" runat="server">
                    <div id="forgotPWDInstructions" runat="server" style="font-size: 8pt;">
                    </div>
                </eaf:StylerPanel>
                <br />
                <eaf:MessageLabel ID="lblMessage" runat="server" ForeColor="navy" />
                <eaf:FormSectionBlock ID="sec1" Caption="Account Information" runat="server" SectionType="frame">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr>
                            <td class="fld">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="txtEmail">Email Address:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" MaxLength="40" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnSubmit" runat="server" CssClass="s_BtnBlue" Text="Submit" OnClick="btnSubmit_Click">
                        </eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
