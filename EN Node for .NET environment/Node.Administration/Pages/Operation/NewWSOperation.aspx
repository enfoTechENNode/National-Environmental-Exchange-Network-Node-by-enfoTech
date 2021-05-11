<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NewWSOperation.aspx.cs" Inherits="NewWSOperation_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="Label1" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed" Visible="false" />
    <eaf:FloatWinPanel ID="addPreProcess" runat="server" WinTitle="Add Pre Process" WinWidth="420"
        WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder1" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate2" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDllPath" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="txtClassName">Class Name:</asp:Label>
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
                            <asp:Label ID="Label4" runat="server" AssociatedControlID="txtPostDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostDllPath" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label5" runat="server" AssociatedControlID="txtPostClassName">Class Name:</asp:Label>
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
                            <asp:Label ID="Label6" runat="server" AssociatedControlID="txtQueryParam">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQueryParam" runat="server" Width="300px" />
                        </td>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label20" runat="server" AssociatedControlID="txtQueryParamDEDLType">Parameter Type:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLType" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label25" runat="server" AssociatedControlID="txtQueryParamDEDLTypeDesc">Parameter Type Descriptor:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLTypeDesc" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label21" runat="server" AssociatedControlID="txtQueryParamDEDLEncoding">Parameter Encoding:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLEncoding" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label22" runat="server" AssociatedControlID="txtQueryParamDEDLOccurance">Parameter Occurrence:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQueryParamDEDLOccurance" runat="server" Width="300px" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label23" runat="server" AssociatedControlID="txtQueryParam">Required Indicator:</asp:Label>
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
                            <asp:Label ID="Label7" runat="server" AssociatedControlID="txtSolicitParam">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParam" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label15" runat="server" AssociatedControlID="txtSolicitParamDEDLType">Parameter Type:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLType" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label26" runat="server" AssociatedControlID="txtSolicitParamDEDLTypeDesc">Parameter Type Descriptor.:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLTypeDesc" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label16" runat="server" AssociatedControlID="txtSolicitParamDEDLEncoding">Parameter Encoding:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLEncoding" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label17" runat="server" AssociatedControlID="txtSolicitParamDEDLOccurance">Parameter Occurrence:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSolicitParamDEDLOccurance" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label18" runat="server" AssociatedControlID="txtSolicitParamDEDLRequiredInd">Required Indicator:</asp:Label>
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
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" />
    <asp:ValidationSummary ID="vsError" runat="server" DisplayMode="bulletList" ForeColor="darkred"
        HeaderText="There are errors on the page - Operation has not been saved" ShowMessageBox="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="Pre Processes" runat="server" Visible="true">
        <eaf:EAFGridView ID="egvPreProcesses" runat="server" Width="100%" OnRowCommand="egvPreProcesses_RowCommand">
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
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="ddlWebService">Web Service:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWebService" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWebService_SelectedIndexChanged" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label19" runat="server" AssociatedControlID="chkPublish">Publish Service:</asp:Label>
                </td>
                <td style="padding-left: 3px">
                    <asp:CheckBox ID="chkPublish" runat="server" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label27" runat="server" AssociatedControlID="chkREST">Enable RESTful Service:</asp:Label>
                </td>
                <td style="padding-left: 3px">
                    <asp:CheckBox ID="chkREST" runat="server" />
                </td>
            </tr>
        </table>
        <asp:MultiView ID="mvProcess" runat="server">
            <asp:View ID="vGeneric" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label24" runat="server" AssociatedControlID="chkPublish">Default Process:</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="cbDefault" runat="server" AutoPostBack="true" OnCheckedChanged="cbDefault_CheckedChanged"
                                ToolTip="Default Process:" />
                        </td>
                    </tr>
                    <tr valign="top" id="dllPath" runat="server">
                        <td class="fld">
                            <asp:Label ID="Label9" runat="server" AssociatedControlID="txtProcessDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProcessDllPath" runat="server" Width="350px" />
                        </td>
                    </tr>
                    <tr valign="top" id="ClsName" runat="server">
                        <td class="fld">
                            <asp:Label ID="Label10" runat="server" AssociatedControlID="txtProcessClassName">Class Name:</asp:Label>
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
                            <asp:Label ID="Label11" runat="server" AssociatedControlID="txtQueryProcessDllPath">Dll Path:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQueryProcessDllPath" runat="server" Width="350px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label12" runat="server" AssociatedControlID="txtQueryProcessClassName">Class Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQueryProcessClassName" runat="server" Width="350px" />
                        </td>
                    </tr>
                </table>
                <eaf:EAFGridView ID="egvQueryParameters" runat="server" Width="500px" OnRowCommand="egvQueryParameters_RowCommand">
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
                            <asp:Label ID="Label13" runat="server" AssociatedControlID="txtSolicitProcessDllPath">Dll Path:</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSolicitProcessDllPath" runat="server" Width="350px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label14" runat="server" AssociatedControlID="txtSolicitProcessClassName">Class Name:</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtSolicitProcessClassName" runat="server" Width="350px" />
                        </td>
                    </tr>
                    <%--                    <tr valign="top">
                        <td>
                        </td>
                        <td class="lftfld" colspan="3">
                            <asp:CheckBox ID="cbSolicitAnytime" runat="server" Text="Execute Solicit Between:" ToolTip="Execute Solicit Between:"/>
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
                            <asp:CheckBox ID="cbSubmit" runat="server" Text="Submit Result to Return URL" ToolTip="Submit Result to Return URL"/>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="lftfld">
                            <asp:Label ID="Label15" runat="server" AssociatedControlID="txtSubmitUserID">Submitter User ID:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubmitUserID" runat="server" />
                        </td>
                        <td class="lftfld">
                            <asp:Label ID="Label16" runat="server" AssociatedControlID="txtSubmitPassword">Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSubmitPassword" runat="server" TextMode="password" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="lftfld">
                            <asp:Label ID="Label17" runat="server" AssociatedControlID="txtDataFlow">Data Flow Name</asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDataFlow" runat="server" />
                        </td>
                    </tr>--%>
                </table>
                <eaf:EAFGridView ID="egvSolicitParameters" runat="server" Width="500px" OnRowCommand="egvSolicitParameters_RowCommand">
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
    <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Post Processes" runat="server"
        Visible="true">
        <eaf:EAFGridView ID="egvPostProcesses" runat="server" Width="500px" OnRowCommand="egvPostProcesses_RowCommand">
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
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnCancel" runat="server" CssClass="s_BtnGreen" Text="Cancel"
                OnClick="btnCancel_Click" ConfirmMessage="Are you sure?" />
            <eaf:EAFButton ID="btnPrevious" runat="server" CssClass="s_BtnGreen" Text="Previous"
                OnClick="btnPrevious_Click" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
