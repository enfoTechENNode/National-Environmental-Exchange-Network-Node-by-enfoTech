<%@ Page Language="C#" CodeFile="GenericHelp.aspx.cs" Inherits="GenericHelp_aspx" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Help Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="cc_PACSHB_2">
        <table id="cc_PACSHB_TB" cellspacing="2" cellpadding="2" border="0" width="500px">
            <tr>
                <td align="left">
                    <img src="../../App_Images/Node/HBIco_Qmark.gif" align="top" alt="Help" />
                </td>
                <td align="left">
                    <asp:Label ID="txtTitle" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <table width="500px" align="center" border="0" cellspacing="10" cellpadding="8" class="cc_EntryForm">
        <tr>
            <td align="left" class="desc">
                <asp:Label ID="txtHelp" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
