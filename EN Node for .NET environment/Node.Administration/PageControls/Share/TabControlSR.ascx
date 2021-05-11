<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControlSR.ascx.cs" Inherits="PageControls_Share_TabControlSR" %>
<div class="eaf_FloatTabBG">
<eaf:TabControl ID="TabCtl" runat="server" TabAlignment="left">
    <eaf:TabItem ID="TabItem1" runat="server" Caption="Service Registration" Link="~/Pages/Registration/NodeRegistration.aspx"></eaf:TabItem>
    <eaf:TabItem ID="TabItem2" runat="server" Caption="DEDL" Link="~/Pages/Registration/DEDLConfig.aspx"></eaf:TabItem>
</eaf:TabControl>
</div>
