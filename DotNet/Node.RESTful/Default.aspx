<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE HTML>
<html>
<head>
    <title>Node RESTful WebServices</title>
    <script type="text/javascript" src="<%=Request.ApplicationPath%>/Scripts/jquery-2.0.3.min.js"></script>
    <script type="text/javascript" src="<%=Request.ApplicationPath%>/Scripts/ENNodeRESTFul.js"></script>
    <link rel="stylesheet" type="text/css" href="<%=Request.ApplicationPath%>/App_Themes/Local.css" />
</head>
<body>
    <%--    <form id="form1" runat="server"></form>--%>
    <div class="TopColumn">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr valign="top">
                <td colspan="2">
                    <a href="http://www.enfotech.com/">
                        <img src="App_Images/Node/Header/Header.gif" alt="enfoTech &#38; Consulting Inc."
                            style="border-width: 0px" /></a>
                </td>
            </tr>
            <tr class="HeaderText" valign="top" style="background-color: #396EA0">
                <td>
                    <span style="color: White; margin-left: 10px">Node RESTful Web Services</span>
                </td>
                <td align="right">
                    <a href="http://www.enfotech.com/" target="_blank" style="color: White;">enfoTech &amp;
                        Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx"
                            style="color: White; margin-right: 10px" target="_blank">Contact Us</a>
                </td>
            </tr>
        </table>
    </div>
    <div class="PageDesc">
    </div>
    <div class="LeftColumn">
        <div class="ServicesHeader">
            Service Lists</div>
        <%--        <div class="DFBlock">
            <div class="DataFlow">NCT</div>
            <div class="ctlBox">
                <div class="Service" id="ServiceID-15">
                    Query_v1.0</div>
            </div>
        </div>
        <div class="DFBlock">
            <div class="DataFlow">
                TTS</div>
            <div class="ctlBox">
                <div class="Service" id="ServiceID-15">
                    Query_v1.0</div>
                <div class="Service" id="ServiceID-36">
                    GetTransactionDetail</div>
                <div class="Service" id="ServiceID-35">
                    GetTransactionList</div>
            </div>
        </div>--%>
    </div>
    <div class="CenterColumn" id="MainContent">
        <%--        <div class="ServiceDtl" id="SerDtl-1">
            <div class="FieldRow">
                <div>
                    <img src="App_Images/window-close.png" alt="Close" class="CtlRemove" /></div>
                <div>
                    <img src="App_Images/zoom.png" alt="Toggle" class="CtlToggle" /></div>
            </div>
            <div class="FieldRow">
                <div class="FieldLabel">
                    DataFlow Name:</div>
                <div class="FieldInput">
                    FRS</div>
            </div>
            <div class="CtlBLock">
                <hr />
                <div class="FieldRowService">
                    <div class="FieldLabel">
                        Service Name:</div>
                    <div class="FieldInput">
                        GetFacilityByChangeDate</div>
                </div>
                <div class="FieldRow">
                    <div class="FieldLabel">
                        Service Description:</div>
                    <div class="FieldInput">
                        11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                        555555555555555 11111111111111111 2222222222222222 555555555555555</div>
                </div>
                <div class="FieldRow">
                    <div class="FieldLabel">
                        Parameters:</div>
                </div>
                <div id="ParameterList">
                    <div class="FieldRowPara">
                        <div class="FieldLabel">
                            RowNumber</div>
                        <div class="FieldInput">
                            <input class="ParameterInput" type="text" />
                        </div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                    </div>
                    <div class="FieldRowPara">
                        <div class="FieldLabel">
                            MaxRow</div>
                        <div class="FieldInput">
                            <input class="ParameterInput" type="text" />
                        </div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                    </div>
                    <div class="FieldRowPara">
                        <div class="FieldLabel">
                            Format</div>
                        <div class="FieldInput">
                            <select class="ParameterInput">
                                <option value="XML" selected>XML</option>
                                <option value="ZIP">ZIP</option>
                            </select>
                        </div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                        <div class="FieldInput">
                            11111111111111111 2222222222222222 555555555555555 11111111111111111 2222222222222222
                            555555555555555</div>
                    </div>
                </div>
                <hr>
                <div class="FieldRow">
                    <div class="FieldLabel">
                        RESTFul URL:</div>
                    <div class="FieldInput">
                        <textarea readonly rows='4' cols='50' class='RESTFulResul'></textarea>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</body>
</html>
