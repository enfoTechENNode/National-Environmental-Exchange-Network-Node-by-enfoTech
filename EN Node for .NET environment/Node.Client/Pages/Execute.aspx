<%@ Page Title="Node Client Utility" Language="C#" MasterPageFile="~/MasterPages/Client.master"
    AutoEventWireup="true" CodeFile="Execute.aspx.cs" Inherits="Pages_Execute" %>

<%@ Register TagPrefix="Node" TagName="LeftPanel" Src="~/PageControls/LeftPanel.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs" Src="~/PageControls/NodeAddress.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeURLs2" Src="~/PageControls/NodeAddress2.ascx" %>
<%@ Register TagPrefix="Node" TagName="NodeTab" Src="~/PageControls/NodeTabLink.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="leftContent" runat="Server">
    <Node:LeftPanel ID="ClientLeftPanel" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:ScriptManager ID="scriptManager" runat="server" />
    <node:NodeTab ID="NodeTab1" runat="server"></node:NodeTab>
    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
        <ajaxToolkit:TabPanel ID="TabNode1" runat="server" HeaderText="Node 1.1">
            <ContentTemplate>
                <Node:NodeURLs2 ID="NodeURLs1" runat="server" />
                <br />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="TabNode12" runat="server" HeaderText="Node 2.0">
            <ContentTemplate>
                <eaf:FloatWinPanel ID="addParameter" runat="server" WinTitle="Add Parameter" WinWidth="500"
                    WinHeight="120">
                    <eaf:AjaxContentHolder ID="AjaxContentHolder5" runat="server">
                        <eaf:AjaxContentTemplate ID="AjaxContentTemplate5" runat="server">
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="fld">
                                        <asp:Label ID="lblParamName" AssociatedControlID="txtParamName" runat="server">Parameter Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtParamName" runat="server" Width="300px" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td class="fld">
                                        <asp:Label ID="lblParamValue" AssociatedControlID="txtParamValue" runat="server">Parameter Value:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtParamValue" runat="server" Width="300px" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>
                                        <eaf:EAFButton ID="btnAddParameter" runat="server" Text="Add" OnClick="btnAddParameter_Click" />
                                    </td>
                                </tr>
                            </table>
                        </eaf:AjaxContentTemplate>
                    </eaf:AjaxContentHolder>
                </eaf:FloatWinPanel>
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Node Web Request Input Parameters"
                    runat="server" Visible="true">
                    <Node:NodeURLs ID="NodeURLsPanel2" runat="server" />
                    <br />
                    <table class="cc_ResultTable" width="550">
                        <tr valign="top">
                            <th colspan="2" align="left">
                                Execute Input Parameters
                            </th>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblToken2" AssociatedControlID="txtToken2" runat="server">Security Token</asp:Label>
                            </td>
                            <td width="100%">
                                <asp:TextBox ID="txtToken2" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right">
                                <asp:Label ID="lblInterfaceName" AssociatedControlID="txtInterfaceName" runat="server">Interface Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInterfaceName" runat="server" Width="400px" />
                            </td>
                        </tr>
                        <tr class="alt1" valign="top">
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblMethodName" AssociatedControlID="txtMethodName" runat="server">Method Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMethodName" runat="server" Width="400px" />
                            </td>
                        </tr>
                    </table>
                    <eaf:EAFGridView ID="egvParameters" runat="server" Width="400px">
                        <Columns>
                            <eaf:GridCheckBoxField ID="gcbfParamSequence" HeaderText="Remove" DataField="CHECK"  />
                            <asp:BoundField HeaderText="Parameter Name" DataField="PARAM_NAME" />
                            <asp:BoundField HeaderText="Parameter Value" DataField="PARAM_VALUE" />
                        </Columns>
                    </eaf:EAFGridView>
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td>
                                <input type="button" value="Add Parameter" onclick="<%=addParameter.ClientID%>Obj.showPanel(this);" />
                            </td>
                            <td>
                                <eaf:EAFButton ID="btnRemoveParameter" runat="server" Text="Remove Selected Parameters"
                                    OnClick="btnRemoveParameter_Click" />
                            </td>
                        </tr>
                    </table>
                </eaf:FormSectionBlock>
                <eaf:ButtonTable ID="ButtonTable1" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons1" runat="server">
                        <eaf:EAFButton ID="btnQuery2" runat="server" CssClass="s_BtnGreen" Text="Execute" OnClick="btnQuery2_Click">
                        </eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% if (this.ShowResult2)
                   { %>
                <br />
                <table class="cc_ResultTable" width="550">
                    <tr class="alt1" valign="top">
                        <td>
                            Execute Result
                        </td>
                    </tr>
                    <tr class="alt1" valign="top">
                        <td>
                            <asp:TextBox ID="txtQueryResult2" runat="server" Rows="15" TextMode="MultiLine" Width="550px"
                                ReadOnly="true" Visible="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <eaf:ButtonTable ID="btnDownloadPanel2" runat="server" TableWidth="600px">
                    <eaf:RightButtons ID="RightButtons2" runat="server">
                        <eaf:EAFButton ID="btnDownload2" runat="server" CssClass="s_BtnGreen" Text="Save to >>"
                            OnClick="btnDownload2_Click"></eaf:EAFButton>
                    </eaf:RightButtons>
                </eaf:ButtonTable>
                <% } %>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
