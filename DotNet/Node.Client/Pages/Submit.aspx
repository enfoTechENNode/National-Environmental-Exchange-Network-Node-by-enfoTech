<%@ Page Language="C#" MasterPageFile="~/MasterPages/Client.master" CodeFile="Submit.aspx.cs"
    Inherits="Submit" Title="Node Client Utility" AutoEventWireup="false" %>

<%@ MasterType VirtualPath="~/MasterPages/Client.master" %>
<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeTab" Src="~/PageControls/NodeTabLink.ascx" %>

<asp:Content ID="LeftPanelContent" ContentPlaceHolderID="leftContent" runat="Server">
    <Node:LeftPanel ID="ClientLeftPanel" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <node:NodeTab ID="NodeTab1" runat="server"></node:NodeTab>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel ID="TabNode1" runat="server" HeaderText="Node 1.1">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="sec1" Caption="Node Web Request Input Parameters" runat="server"
                    Visible="true">
                    <Node:NodeURLs ID="NodeURLsPanel" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="4" align="left">
                                Submit Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken" AssociatedControlID="txtToken" runat="server">Security Token</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtToken" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTransID" AssociatedControlID="txtTransID" runat="server">Transaction ID</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDataFlow" AssociatedControlID="txtDataFlow" runat="server">Data Flow</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDataFlow" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 1
                            </td>
                            <td>
                                <asp:FileUpload ID="file1" runat="server" Width="300px" />
                                <asp:CustomValidator ID="file1Validate" runat="server"
                                    ControlToValidate="file1" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 1 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="50px" />                        
                                <%--<asp:TextBox ID="txtFileType1" runat="server" Width="50px" />--%>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 2
                            </td>
                            <td>
                                <asp:FileUpload ID="file2" runat="server" Width="300px" />
                                <asp:CustomValidator ID="file2Validate" runat="server"
                                    ControlToValidate="file2" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 2 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" Width="50px" />
                                <%--<asp:TextBox ID="txtFileType2" runat="server" Width="50px" />--%>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 3
                            </td>
                            <td>
                                <asp:FileUpload ID="file3" runat="server" Width="300px" />
                                <asp:CustomValidator ID="file3Validate" runat="server"
                                    ControlToValidate="file3" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 3 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" Width="50px" />
                                <%--<asp:TextBox ID="txtFileType3" runat="server" Width="50px" />--%>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="rightBtnPanel" runat="server">
                        <eaf:EAFButton ID="btnSubmit" runat="server" CssClass="s_BtnGreen" Text="Submit"
                            OnClick="btnSubmit_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Submit Result
                        </td>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td>
                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabNode12" runat="server" HeaderText="Node 2.0">
            <ContentTemplate>
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters"
                    runat="server" Visible="true">
                    <Node:NodeURLs2 ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="500">
                        <tr valign="top">
                            <th colspan="4" align="left">
                                Submit Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken2" AssociatedControlID="txtToken2" runat="server">Security Token</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtToken2" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblTransID2" AssociatedControlID="txtTransID2" runat="server">Transaction ID</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtTransID2" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblDataFlow2" AssociatedControlID="txtDataFlow2" runat="server">Data Flow</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDataFlow2" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblFlowOperation" AssociatedControlID="txtFlowOperation" runat="server">Flow Operation</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtFlowOperation" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblRecipient" AssociatedControlID="txtRecipient" runat="server">Recipient</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtRecipient" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblNotificationURI" AssociatedControlID="txtNotificationURI" runat="server">Notification URI</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNotificationURI" runat="server" Width="350px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblNotifyType5" AssociatedControlID="DropDownList4" runat="server">Notification Type</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="DropDownList4" runat="server" Width="200px" /> 
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 1
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                                <asp:CustomValidator ID="fu1Validate" runat="server"
                                    ControlToValidate="FileUpload1" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 1 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFileType1" runat="server" Width="50px" >                        
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 2
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload2" runat="server" Width="300px" />
                                <asp:CustomValidator ID="fu2Validate" runat="server"
                                    ControlToValidate="FileUpload2" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 2 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFileType2" runat="server" Width="50px" >                        
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                File 3
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload3" runat="server" Width="300px" />
                                <asp:CustomValidator ID="fu3Validate" runat="server"
                                    ControlToValidate="FileUpload3" Display="Dynamic" />
                            </td>
                            <td align="right" nowrap="nowrap">
                                File 3 Type
                            </td>
                            <td>
                                <asp:DropDownList ID="drpFileType3" runat="server" Width="50px" >                        
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnSubmit2" runat="server" CssClass="s_BtnGreen" Text="Submit"
                            OnClick="btnSubmit2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="600">
                    <tr class="alt1" valign="top">
                        <td>
                            Submit Result
                        </td>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td>
                            <asp:Label ID="lblResult2" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
