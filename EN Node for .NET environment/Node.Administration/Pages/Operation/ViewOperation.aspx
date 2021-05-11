<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="ViewOperation.aspx.cs" Inherits="ViewOperation_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <eaf:FloatWinPanel ID="addPreProcess" runat="server" WinTitle="Add Pre Process" WinWidth="420"
        WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder1" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate2" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDllPath" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtClassName">Class Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClassName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <eaf:EAFButton ID="btnAddPreProcess" runat="server" Text="Add" OnClick="btnAddPreProcess_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:AjaxContentTemplate>
        </eaf:AjaxContentHolder>
    </eaf:FloatWinPanel>
    <eaf:FloatWinPanel ID="addPostProcess" runat="server" WinTitle="Add Post Process"
        WinWidth="420" WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder2" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate1" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="txtPostDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostDllPath" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label4" runat="server" AssociatedControlID="txtPostClassName">Class Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostClassName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <eaf:EAFButton ID="btnAddPostProcess" runat="server" Text="Add" OnClick="btnAddPostProcess_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:AjaxContentTemplate>
        </eaf:AjaxContentHolder>
    </eaf:FloatWinPanel>
    <eaf:FloatWinPanel ID="addQueryParameter" runat="server" WinTitle="Add Query Parameter"
        WinWidth="500" WinHeight="200">
        <eaf:AjaxContentHolder ID="AjaxContentHolder3" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate3" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label5" runat="server" AssociatedControlID="txtQueryParam">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQueryParam" runat="server" Width="300px" />
                        </td>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label21" runat="server" AssociatedControlID="txtQueryParamDEDLType">Parameter Type:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLType" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label23" runat="server" AssociatedControlID="txtQueryParamDEDLTypeDesc">Parameter Type Descriptor:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLTypeDesc" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label33" runat="server" AssociatedControlID="txtQueryParamDEDLEncoding">Parameter Encoding:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLEncoding" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label34" runat="server" AssociatedControlID="txtQueryParamDEDLOccurance">Parameter Occurrence:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLOccurance" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label35" runat="server" AssociatedControlID="txtQueryParam">Required Indicator:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLRequiredInd" runat="server" Width="300px" />
                            </td>
                        </tr>
                    </tr>
                    <tr valign="top">
                        <td colspan="2" align="right">
                            <eaf:EAFButton ID="btnAddQueryParameter" runat="server" Text="Add" OnClick="btnAddQueryParameter_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:AjaxContentTemplate>
        </eaf:AjaxContentHolder>
    </eaf:FloatWinPanel>
    <eaf:FloatWinPanel ID="addSolicitParameter" runat="server" WinTitle="Add Solicit Parameter"
        WinWidth="500" WinHeight="200">
        <eaf:AjaxContentHolder ID="AjaxContentHolder4" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate4" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label6" runat="server" AssociatedControlID="txtSolicitParam">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParam" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label22" runat="server" AssociatedControlID="txtSolicitParamDEDLType">Parameter Type:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLType" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label36" runat="server" AssociatedControlID="txtSolicitParamDEDLTypeDesc">Parameter Type Descriptor.:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLTypeDesc" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label37" runat="server" AssociatedControlID="txtSolicitParamDEDLEncoding">Parameter Encoding:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLEncoding" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label38" runat="server" AssociatedControlID="txtSolicitParamDEDLOccurance">Parameter Occurrence:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLOccurance" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label39" runat="server" AssociatedControlID="txtSolicitParamDEDLRequiredInd">Required Indicator:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLRequiredInd" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2" align="right">
                            <eaf:EAFButton ID="btnAddSolicitParameter" runat="server" Text="Add" OnClick="btnAddSolicitParameter_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:AjaxContentTemplate>
        </eaf:AjaxContentHolder>
    </eaf:FloatWinPanel>
    <eaf:FloatWinPanel ID="addParameter" runat="server" WinTitle="Add Task Parameter"
        WinWidth="500" WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder5" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate5" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label7" runat="server" AssociatedControlID="txtParamName">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtParamName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label8" runat="server" AssociatedControlID="txtParamValue">Parameter Value:</asp:Label>
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
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" />
    <asp:ValidationSummary ID="vsError" runat="server" DisplayMode="bulletList" ForeColor="darkred"
        HeaderText="There are errors on the page - Operation has not been saved" ShowMessageBox="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="Operation Information" runat="server" Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label9" runat="server" AssociatedControlID="txtOperationName">Operation Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtOperationName" runat="server" MaxLength="50" ReadOnly="true"
                        Width="200px" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="Running">Running</asp:ListItem>
                        <asp:ListItem Value="Stopped">Stopped</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="txtStatusMessage">Operation Status Message:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStatusMessage" runat="server" MaxLength="1000" Rows="5" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label12" runat="server" AssociatedControlID="txtDescription">Operation Description:</asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Rows="2" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label13" runat="server" AssociatedControlID="ddlOpType">Operation Type:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOpType" runat="server" Enabled="false">
                        <asp:ListItem Value="WEB_SERVICE">Web Service</asp:ListItem>
                        <asp:ListItem Value="SCHEDULED_TASK">Scheduled Task</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <asp:MultiView ID="mvOpType" runat="server">
        <asp:View ID="vWebService" runat="server">
            <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Pre Processes" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvPreProcesses" runat="server" Width="600px" OnRowCommand="egvPreProcesses_RowCommand">
                    <Columns>
                        <eaf:GridCheckBoxField ID="gcbfPreSequence" HeaderText="Remove" DataField="SEQUENCE" />
                        <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
                        <asp:BoundField HeaderText="Dll Path" DataField="DLL_NAME" />
                        <asp:BoundField HeaderText="Class Name" DataField="CLASS_NAME" />
                    </Columns>
                </eaf:EAFGridView>
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <input type="button" value="Add Pre Process" onclick="<%=addPreProcess.ClientID%>Obj.showPanel(this);" />
                        </td>
                        <td>
                            <eaf:EAFButton ID="btnRemovePreProcesses" runat="server" Text="Remove Selected Pre Processes"
                                OnClick="btnRemovePreProcesses_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Process" runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label14" runat="server" AssociatedControlID="ddlWebService">Web Service:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWebService" runat="server" AutoPostBack="true" Enabled="false" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label41" runat="server" AssociatedControlID="chkPublish">Publish Service:</asp:Label>
                        </td>
                        <td style="padding-left: 3px">
                            <asp:CheckBox ID="chkPublish" runat="server" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label43" runat="server" AssociatedControlID="chkREST">Enable RESTful Service:</asp:Label>
                        </td>
                        <td style="padding-left: 3px">
                            <asp:CheckBox ID="chkREST" runat="server" />
                        </td>
                    </tr>
                </table>
                <table class="cc_EntryForm" cellspacing="0" runat="server" id="pnlDenyPolicy" visible="false">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label40" runat="server" AssociatedControlID="chkDenyPolicy">Require explicit right to execute this operation:</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDenyPolicy" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:MultiView ID="mvProcess" runat="server">
                    <asp:View ID="vGeneric" runat="server">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label42" runat="server" AssociatedControlID="cbDefault">Default Process:</asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbDefault" runat="server" AutoPostBack="true" OnCheckedChanged="cbDefault_CheckedChanged"
                                        ToolTip="Default Process" />
                                </td>
                            </tr>
                            <tr valign="top" id="dllPath" runat="server">
                                <td class="fld">
                                    <asp:Label ID="Label15" runat="server" AssociatedControlID="txtProcessDllPath">Dll Path:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProcessDllPath" runat="server" Width="350px" />
                                </td>
                            </tr>
                            <tr valign="top" id="ClsName" runat="server">
                                <td class="fld">
                                    <asp:Label ID="Label16" runat="server" AssociatedControlID="txtProcessClassName">Class Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProcessClassName" runat="server" Width="350px" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="vQuery" runat="server">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label17" runat="server" AssociatedControlID="txtQueryProcessDllPath">Dll Path:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQueryProcessDllPath" runat="server" Width="350px" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label18" runat="server" AssociatedControlID="txtQueryProcessClassName">Class Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQueryProcessClassName" runat="server" Width="350px" />
                                </td>
                            </tr>
                        </table>
                        <eaf:EAFGridView ID="egvQueryParameters" runat="server" Width="550px" OnRowCommand="egvQueryParameters_RowCommand">
                            <Columns>
                                <eaf:GridCheckBoxField ID="gcbfQueryParameters" HeaderText="Remove" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Parameter Name" DataField="PARAM_NAME" />
                                <asp:BoundField HeaderText="Parameter Type" DataField="DEDLType" />
                                <asp:BoundField HeaderText="Parameter Type Descriptor" DataField="DEDLTypeDescriptor" />
                                <asp:BoundField HeaderText="Parameter Encoding" DataField="DEDLEncoding" />
                                <asp:BoundField HeaderText="Parameter Occurrence" DataField="DEDLOccurenceNumber" />
                                <asp:BoundField HeaderText="Required Indicator" DataField="DEDLRequiredIndicator" />
                            </Columns>
                        </eaf:EAFGridView>
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <input type="button" value="Add Query Parameters" onclick="<%=addQueryParameter.ClientID%>Obj.showPanel(this);" />
                                </td>
                                <td>
                                    <eaf:EAFButton ID="btnRemoveQueryParameters" runat="server" Text="Remove Selected Query Parameters"
                                        OnClick="btnRemoveQueryParameters_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="vSolicit" runat="server">
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label19" runat="server" AssociatedControlID="txtSolicitProcessDllPath">Dll Path:</asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtSolicitProcessDllPath" runat="server" Width="350px" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="fld">
                                    <asp:Label ID="Label20" runat="server" AssociatedControlID="txtSolicitProcessClassName">Class Name:</asp:Label>
                                </td>
                                <td colspan="4">
                                    <asp:TextBox ID="txtSolicitProcessClassName" runat="server" Width="350px" />
                                </td>
                            </tr>
                            <%--                            <tr valign="top">
                                <td>
                                </td>
                                <td class="lftfld" colspan="4">
                                    <asp:CheckBox ID="cbSolicitAnytime" runat="server" Text="Execute Solicit Between:"
                                        ToolTip="Execute Solicit Between" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                </td>
                                <td nowrap="nowrap" align="left">
                                    <asp:DropDownList ID="solBegHour" runat="server" ToolTip="Hours">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    :
                                    <asp:DropDownList ID="solBegMin" runat="server" ToolTip="Minutes">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>31</asp:ListItem>
                                        <asp:ListItem>32</asp:ListItem>
                                        <asp:ListItem>33</asp:ListItem>
                                        <asp:ListItem>34</asp:ListItem>
                                        <asp:ListItem>35</asp:ListItem>
                                        <asp:ListItem>36</asp:ListItem>
                                        <asp:ListItem>37</asp:ListItem>
                                        <asp:ListItem>38</asp:ListItem>
                                        <asp:ListItem>39</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>41</asp:ListItem>
                                        <asp:ListItem>42</asp:ListItem>
                                        <asp:ListItem>43</asp:ListItem>
                                        <asp:ListItem>44</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                        <asp:ListItem>46</asp:ListItem>
                                        <asp:ListItem>47</asp:ListItem>
                                        <asp:ListItem>48</asp:ListItem>
                                        <asp:ListItem>49</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>51</asp:ListItem>
                                        <asp:ListItem>52</asp:ListItem>
                                        <asp:ListItem>53</asp:ListItem>
                                        <asp:ListItem>54</asp:ListItem>
                                        <asp:ListItem>55</asp:ListItem>
                                        <asp:ListItem>56</asp:ListItem>
                                        <asp:ListItem>57</asp:ListItem>
                                        <asp:ListItem>58</asp:ListItem>
                                        <asp:ListItem>59</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="lftfld">
                                    and:
                                </td>
                                <td nowrap="nowrap" align="left">
                                    <asp:DropDownList ID="solEndHour" runat="server" ToolTip="Hours">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    :
                                    <asp:DropDownList ID="solEndMin" runat="server" ToolTip="Minutes">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>31</asp:ListItem>
                                        <asp:ListItem>32</asp:ListItem>
                                        <asp:ListItem>33</asp:ListItem>
                                        <asp:ListItem>34</asp:ListItem>
                                        <asp:ListItem>35</asp:ListItem>
                                        <asp:ListItem>36</asp:ListItem>
                                        <asp:ListItem>37</asp:ListItem>
                                        <asp:ListItem>38</asp:ListItem>
                                        <asp:ListItem>39</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>41</asp:ListItem>
                                        <asp:ListItem>42</asp:ListItem>
                                        <asp:ListItem>43</asp:ListItem>
                                        <asp:ListItem>44</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                        <asp:ListItem>46</asp:ListItem>
                                        <asp:ListItem>47</asp:ListItem>
                                        <asp:ListItem>48</asp:ListItem>
                                        <asp:ListItem>49</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>51</asp:ListItem>
                                        <asp:ListItem>52</asp:ListItem>
                                        <asp:ListItem>53</asp:ListItem>
                                        <asp:ListItem>54</asp:ListItem>
                                        <asp:ListItem>55</asp:ListItem>
                                        <asp:ListItem>56</asp:ListItem>
                                        <asp:ListItem>57</asp:ListItem>
                                        <asp:ListItem>58</asp:ListItem>
                                        <asp:ListItem>59</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                </td>
                                <td class="lftfld" colspan="3">
                                    <asp:CheckBox ID="cbSubmit" runat="server" Text="Submit Result to Return URL" ToolTip="Submit Result to Return URL" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="lftfld">
                                    <asp:Label ID="Label21" runat="server" AssociatedControlID="txtSubmitUserID">Submitter User ID:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubmitUserID" runat="server" />
                                </td>
                                <td class="lftfld">
                                    <asp:Label ID="Label22" runat="server" AssociatedControlID="txtSubmitPassword">Password:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubmitPassword" runat="server" TextMode="password" />
                                </td>
                            </tr>
                            <tr valign="top">
                                <td class="lftfld">
                                    <asp:Label ID="Label23" runat="server" AssociatedControlID="txtDataFlow">Data Flow Name</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDataFlow" runat="server" />
                                </td>
                            </tr>--%>
                        </table>
                        <eaf:EAFGridView ID="egvSolicitParameters" runat="server" Width="600px" OnRowCommand="egvSolicitParameters_RowCommand">
                            <Columns>
                                <eaf:GridCheckBoxField ID="gcbfSolicitParameters" HeaderText="Remove" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Parameter Name" DataField="PARAM_NAME" />
                                <asp:BoundField HeaderText="Parameter Type" DataField="DEDLType" />
                                <asp:BoundField HeaderText="Parameter Type Descriptor" DataField="DEDLTypeDescriptor" />
                                <asp:BoundField HeaderText="Parameter Encoding" DataField="DEDLEncoding" />
                                <asp:BoundField HeaderText="Parameter Occurrence" DataField="DEDLOccurenceNumber" />
                                <asp:BoundField HeaderText="Required Indicator" DataField="DEDLRequiredIndicator" />
                            </Columns>
                        </eaf:EAFGridView>
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <input type="button" value="Add Solicit Parameters" onclick="<%=addSolicitParameter.ClientID%>Obj.showPanel(this);" />
                                </td>
                                <td>
                                    <eaf:EAFButton ID="btnRemoveSolicitParameters" runat="server" Text="Remove Selected Solicit Parameters"
                                        OnClick="btnRemoveSolicitParameters_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Post Processes" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvPostProcesses" runat="server" Width="600px" OnRowCommand="egvPostProcesses_RowCommand">
                    <Columns>
                        <eaf:GridCheckBoxField ID="gcbfPostSequence" HeaderText="Remove" DataField="SEQUENCE" />
                        <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
                        <asp:BoundField HeaderText="Dll Path" DataField="DLL_NAME" />
                        <asp:BoundField HeaderText="Class Name" DataField="CLASS_NAME" />
                    </Columns>
                </eaf:EAFGridView>
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td>
                            <input type="button" value="Add Post Process" onclick="<%=addPostProcess.ClientID%>Obj.showPanel(this);" />
                        </td>
                        <td>
                            <eaf:EAFButton ID="EAFButton1" runat="server" Text="Remove Selected Post Processes"
                                OnClick="btnRemovePostProcesses_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
        </asp:View>
        <asp:View ID="vTask" runat="server">
            <eaf:FormSectionBlock ID="FormSectionBlock4" Caption="Task Dll and Class" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label24" runat="server" AssociatedControlID="txtTaskDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskDllPath" runat="server" Width="350px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label25" runat="server" AssociatedControlID="txtTaskClassName">Class Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTaskClassName" runat="server" Width="350px" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="fsbParameters" Caption="Task Parameters" runat="server"
                Visible="true">
                <eaf:EAFGridView ID="egvParameters" runat="server" Width="600px" OnRowCommand="egvParameters_RowCommand">
                    <Columns>
                        <eaf:GridCheckBoxField ID="gcbfParamSequence" HeaderText="Remove" DataField="SEQUENCE" />
                        <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
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
            <eaf:FormSectionBlock ID="FormSectionBlock5" Caption="Task Schedule" runat="server"
                Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label26" runat="server" AssociatedControlID="ddlScheduleType">Task Schedule Type:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlScheduleType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlScheduleType_SelectedIndexChanged">
                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                <asp:ListItem Value="1">Once</asp:ListItem>
                                <asp:ListItem Value="2">Daily</asp:ListItem>
                                <asp:ListItem Value="3">Weekly</asp:ListItem>
                                <asp:ListItem Value="4">Monthly</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label27" runat="server" AssociatedControlID="dpStart">Start Date:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="dpStart" /><br />
                            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dpStart" />
                        </td>
                        <td class="fld">
                            <asp:Label ID="Label28" runat="server" AssociatedControlID="ddlStartHour">Start Time:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStartHour" runat="server" ToolTip="Hours">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            :
                            <asp:DropDownList ID="ddlStartMin" runat="server" ToolTip="Minutes">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>
                            :
                            <asp:DropDownList ID="ddlStartSec" runat="server" ToolTip="Seconds">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label29" runat="server" AssociatedControlID="dpEnd">End Date:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="dpEnd" /><br />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dpEnd" />
                        </td>
                        <td class="fld">
                            <asp:Label ID="Label30" runat="server" AssociatedControlID="ddlEndHour">End Time:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEndHour" runat="server" ToolTip="Hours">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            :
                            <asp:DropDownList ID="ddlEndMin" runat="server" ToolTip="Minutes">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>
                            :
                            <asp:DropDownList ID="ddlEndSec" runat="server" ToolTip="Seconds">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:MultiView ID="mvTypes" runat="server">
                    <asp:View ID="vInactive" runat="server">
                    </asp:View>
                    <asp:View ID="vDaily" runat="server">
                        <eaf:FormSectionBlock ID="FormSectionBlock6" Caption="Daily Schedule" runat="server"
                            Visible="true" SectionType="Frame">
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="fld">
                                        <asp:Label ID="Label31" runat="server" AssociatedControlID="txtInterval">Interval:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInterval" runat="server" />
                                    </td>
                                    <td class="lftfld">
                                        <asp:RadioButtonList ID="rblInterval" runat="server" RepeatColumns="2" ToolTip="Interval Type">
                                            <asp:ListItem Value="M">Minutes</asp:ListItem>
                                            <asp:ListItem Value="D">Daily</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </eaf:FormSectionBlock>
                    </asp:View>
                    <asp:View ID="vWeekly" runat="server">
                        <eaf:FormSectionBlock ID="FormSectionBlock7" Caption="Weekly Schedule" runat="server"
                            Visible="true" SectionType="Frame">
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="lftfld">
                                        Every
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWeekInterval" runat="server" ToolTip="Week Interval" />
                                    </td>
                                    <td class="lftfld">
                                        Weeks
                                    </td>
                                </tr>
                            </table>
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="lftfld">
                                        <asp:CheckBoxList ID="cblWeeks" runat="server" RepeatColumns="4" RepeatLayout="Table"
                                            ToolTip="Every Week of">
                                            <asp:ListItem Value="0">Sunday</asp:ListItem>
                                            <asp:ListItem Value="1">Monday</asp:ListItem>
                                            <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                            <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                            <asp:ListItem Value="4">Thursday</asp:ListItem>
                                            <asp:ListItem Value="5">Friday</asp:ListItem>
                                            <asp:ListItem Value="6">Saturday</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </eaf:FormSectionBlock>
                    </asp:View>
                    <asp:View ID="vMonthly" runat="server">
                        <eaf:FormSectionBlock ID="FormSectionBlock8" Caption="Monthly Schedule" runat="server"
                            Visible="true" SectionType="Frame">
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="lftfld">
                                        <asp:RadioButton ID="rbDayOfMonth" runat="server" GroupName="rbgMonthlyType" ToolTip="Day of Month" />
                                        Day
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDayOfMonth" runat="server" />
                                    </td>
                                    <td class="lftfld">
                                        of
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td class="lftfld">
                                        <asp:RadioButton ID="rbWeekOfMonth" runat="server" GroupName="rbgMonthlyType" ToolTip="Week of Month" />
                                        The
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWeekOfMonth" runat="server" ToolTip="Week of Month">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="0">First</asp:ListItem>
                                            <asp:ListItem Value="1">Second</asp:ListItem>
                                            <asp:ListItem Value="2">Third</asp:ListItem>
                                            <asp:ListItem Value="3">Fourth</asp:ListItem>
                                            <asp:ListItem Value="4">Last</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDayOfWeek" runat="server" ToolTip="Day of Week">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="0">Sunday</asp:ListItem>
                                            <asp:ListItem Value="1">Monday</asp:ListItem>
                                            <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                            <asp:ListItem Value="3">Wedsneday</asp:ListItem>
                                            <asp:ListItem Value="4">Thursday</asp:ListItem>
                                            <asp:ListItem Value="5">Friday</asp:ListItem>
                                            <asp:ListItem Value="6">Saturday</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="lftfld">
                                        of
                                    </td>
                                </tr>
                            </table>
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="lftfld" colspan="3">
                                        <asp:CheckBoxList ID="cblMonthOfYear" runat="server" RepeatColumns="4" ToolTip="Month of Year">
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </eaf:FormSectionBlock>
                    </asp:View>
                </asp:MultiView>
            </eaf:FormSectionBlock>
            <eaf:FormSectionBlock ID="FormSectionBlock9" Caption="Email Receivers" runat="server"
                Visible="true" SectionType="Frame">
                <table class="cc_EntryForm">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label32" runat="server" AssociatedControlID="txtEmailReceiver">Email Receiver:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailReceiver" runat="server" Width="200px" />
                        </td>
                        <td>
                            <eaf:EAFButton ID="btnAddEmailReceiver" runat="server" Text="Add Email Receiver"
                                OnClick="btnAddEmailReceiver_Click" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="3">
                            <asp:ListBox ID="lbEmailReceiverList" runat="server" SelectionMode="multiple" Width="300" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="3">
                            <eaf:EAFButton ID="btnRemoveEmailReceiver" runat="server" Text="Remove Selected Email Receivers"
                                OnClick="btnRemoveEmailReceiver_Click" />
                        </td>
                    </tr>
                </table>
            </eaf:FormSectionBlock>
        </asp:View>
    </asp:MultiView>
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnBack" runat="server" CssClass="s_BtnGrey" Text="Back" OnClick="btnBack_Click"
                CausesValidation="false" />
            <eaf:EAFButton ID="btnDeleteOperation" runat="server" CssClass="s_BtnRed" Text="Delete Operation"
                OnClick="btnDeleteOperation_Click" CausesValidation="false" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
