<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="ViewOperationWizard.aspx.cs" Inherits="Pages_Operation_ViewOperationWizard"
    Title="View Operation" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <eaf:FloatWinPanel ID="addExecuteParameter" runat="server" WinTitle="Add Query Parameter"
        WinWidth="420" WinHeight="100">
        <eaf:AjaxContentHolder ID="AjaxContentHolder1" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate1" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            Parameter Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtExecuteParam" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <eaf:EAFButton ID="btnAddExecuteParameter" runat="server" Text="Add" OnClick="btnAddExecuteParameter_Click" />
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
    <eaf:FloatWinPanel ID="addParameter" runat="server" WinTitle="Add Task Parameter"
        WinWidth="500" WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder5" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate5" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            Parameter Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtParamName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            Parameter Value:
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
    <asp:Label ID="Label1" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred" Visible="false" />
    <asp:Label ID="lblError" runat="server" CssClass="eaf_MsgLbl" ForeColor="darkred"
        Visible="false" />
    <eaf:FormSectionBlock ID="sec1" Caption="Operation Information" runat="server" Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td class="fld">
                    Operation Name:
                </td>
                <td>
                    <asp:TextBox ID="txtOperationName" runat="server" MaxLength="50" Enabled="false"
                        Width="200px" />
                </td>
                <td class="fld">
                    Status:
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
                    Operation Status Message:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStatusMessage" runat="server" MaxLength="1000" Rows="5" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    Operation Description:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Rows="2" TextMode="multiLine"
                        Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    Operation Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlOpType" runat="server" Enabled="false">
                        <asp:ListItem Value="WEB_SERVICE">Web Service</asp:ListItem>
                        <asp:ListItem Value="SCHEDULED_TASK">Scheduled Task</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>                
                <td class="fld">Web Service:</td>
                <td colspan="3"><asp:DropDownList ID="ddlWebService" runat="server" AutoPostBack="true" Enabled="false" /></td>
            </tr>--%>
        </table>
    </eaf:FormSectionBlock>
    <asp:MultiView ID="mvOpType" runat="server">
        <asp:View ID="vWebService" runat="server">
            <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Process" runat="server" Visible="true">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            Web Service:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWebService" runat="server" AutoPostBack="true" Enabled="false" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label19" runat="server" AssociatedControlID="chkPublish">Publish Service:</asp:Label>
                        </td>
                        <td>
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
                <table class="cc_EntryForm" cellspacing="0" runat="server" id="pnlDenyPolicy" visible="false">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="chkDenyPolicy">Require explicit right to execute this operation:</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDenyPolicy" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:MultiView ID="mvProcess" runat="server">
                    <asp:View ID="vExecute" runat="server">
                        <eaf:EAFGridView ID="egvExecuteParameters" runat="server" Width="550px" OnRowCommand="egvExecuteParameters_RowCommand">
                            <Columns>
                                <eaf:GridCheckBoxField ID="gcbfExecuteParameters" HeaderText="Remove" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Sequence" DataField="SEQUENCE" />
                                <asp:BoundField HeaderText="Parameter Name" DataField="PARAM_NAME" />
                            </Columns>
                        </eaf:EAFGridView>
                        <table class="cc_EntryForm" cellspacing="0">
                            <tr valign="top">
                                <td>
                                    <input type="button" value="Add Query Parameters" onclick="<%=addExecuteParameter.ClientID%>Obj.showPanel(this);" />
                                </td>
                                <td>
                                    <eaf:EAFButton ID="btnRemoveExecuteParameters" runat="server" Text="Remove Selected Execute Parameters"
                                        OnClick="btnRemoveExecuteParameters_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="vQuery" runat="server">
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
        </asp:View>
        <asp:View ID="vTask" runat="server">
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
                            Task Schedule Type:
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
                            Start Date:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="dpStart" /><br />
                            <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dpStart" />
                        </td>
                        <td class="fld">
                            Start Time:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStartHour" runat="server">
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
                            <asp:DropDownList ID="ddlStartMin" runat="server">
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
                            <asp:DropDownList ID="ddlStartSec" runat="server">
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
                            End Date:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="dpEnd" /><br />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dpEnd" />
                        </td>
                        <td class="fld">
                            End Time:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEndHour" runat="server">
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
                            <asp:DropDownList ID="ddlEndMin" runat="server">
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
                            <asp:DropDownList ID="ddlEndSec" runat="server">
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
                                        Interval:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInterval" runat="server" />
                                    </td>
                                    <td class="lftfld">
                                        <asp:RadioButtonList ID="rblInterval" runat="server" RepeatColumns="2">
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
                                        <asp:TextBox ID="txtWeekInterval" runat="server" />
                                    </td>
                                    <td class="lftfld">
                                        Weeks
                                    </td>
                                </tr>
                            </table>
                            <table class="cc_EntryForm" cellspacing="0">
                                <tr valign="top">
                                    <td class="lftfld">
                                        <asp:CheckBoxList ID="cblWeeks" runat="server" RepeatColumns="4" RepeatLayout="Table">
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
                                        <asp:RadioButton ID="rbDayOfMonth" runat="server" GroupName="rbgMonthlyType" />
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
                                        <asp:RadioButton ID="rbWeekOfMonth" runat="server" GroupName="rbgMonthlyType" />
                                        The
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWeekOfMonth" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="0">First</asp:ListItem>
                                            <asp:ListItem Value="1">Second</asp:ListItem>
                                            <asp:ListItem Value="2">Third</asp:ListItem>
                                            <asp:ListItem Value="3">Fourth</asp:ListItem>
                                            <asp:ListItem Value="4">Last</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlDayOfWeek" runat="server">
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
                                        <asp:CheckBoxList ID="cblMonthOfYear" runat="server" RepeatColumns="4">
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
            <eaf:EAFButton ID="btnGoToWizard" runat="server" CssClass="s_BtnGreen" Text="Save/Go to Data Flow Wizard"
                OnClick="btnGoToWizard_Click"></eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
