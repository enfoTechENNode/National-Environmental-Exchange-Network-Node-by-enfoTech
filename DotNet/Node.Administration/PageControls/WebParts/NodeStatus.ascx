<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NodeStatus.ascx.cs" Inherits="PageControls_WebParts_NodeStatus" %>
<a id="STS" name="STS"/>
<asp:UpdatePanel ID="udpprocess" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="ttl" colspan="2">
                    <img id="Img1" height="30" src="~/App_Images/Node/PnlIco/pnlico_home.gif" style="vertical-align: middle;"
                        runat="server" alt="Node Status"/>
                    <asp:Label ID="lblNodeStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="ttl">
                    <asp:Label ID="lblNodeStatusMsg" runat="server"></asp:Label>
                </td>
                <td class="ttl">
                    <asp:ImageButton ID="lkbTurnOff" runat="server" ImageUrl="~/App_Images/Node/PnlIco/pnlico_processClose.gif"
                        ToolTip="click here to turn on displaying running processes." ImageAlign="Right"
                        CausesValidation="false" OnClick="lkbTurnOff_Click" AlternateText="Click here to turn on displaying running process"></asp:ImageButton>
                </td>
            </tr>
        </table>
        <table cellspacing="0" class="announce">
            <tr>
                <td class="log">
                    <span class="count">
                        <asp:Label ID="TotalProcess" runat="server" Text="0"></asp:Label>
                    </span>
                </td>
            </tr>
        </table>
        <eaf:EAFGridView ID="egvProcessGrid" runat="server" AllowPaging="true" AllowSorting="true"
            AllowMultiColumnSorting="true" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Operation" DataField="OPERATION_NAME" SortExpression="OPERATION_NAME" />
                <asp:BoundField HeaderText="Process Name" DataField="PROCESS_NAME" SortExpression="PROCESS_NAME" />
                <asp:BoundField HeaderText="Updated Date" DataField="UPDATED_DTTM" SortExpression="UPDATED_DTTM" />
            </Columns>
        </eaf:EAFGridView>
        <asp:Timer ID="Timer1" runat="server" Interval="43200000" OnTick="Timer_Tick" />
    </ContentTemplate>
</asp:UpdatePanel>
