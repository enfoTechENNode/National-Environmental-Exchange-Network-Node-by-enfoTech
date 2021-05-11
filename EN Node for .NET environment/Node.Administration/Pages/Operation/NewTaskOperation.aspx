<%@ Page Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true"
    CodeFile="NewTaskOperation.aspx.cs" Inherits="NewTaskOperation_aspx" %>

<%@ MasterType VirtualPath="~/MasterPages/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Label ID="Label1" runat="server" CssClass="eaf_MsgLbl" ForeColor="DarkRed" Visible="false" />
    <eaf:FloatWinPanel ID="addParameter" runat="server" WinTitle="Add Task Parameter"
        WinWidth="720" WinHeight="120">
        <eaf:AjaxContentHolder ID="AjaxContentHolder1" runat="server">
            <eaf:AjaxContentTemplate ID="AjaxContentTemplate2" runat="server">
                <table class="cc_EntryForm" cellspacing="0">
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtParamName">Parameter Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtParamName" runat="server" Width="300px" />
                        </td>
                    </tr>
                    <tr valign="top">
                        <td class="fld">
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="txtParamValue">Parameter Value:</asp:Label>
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
    <eaf:FormSectionBlock ID="FormSectionBlock2" Caption="Task Dll and Class" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="txtDllPath">Dll Path:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDllPath" runat="server" Width="350px" />
                </td>
            </tr>
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="txtClassName">Class Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtClassName" runat="server" Width="350px" />
                </td>
            </tr>
        </table>
    </eaf:FormSectionBlock>
    <eaf:FormSectionBlock ID="fsbParameters" Caption="Task Parameters" runat="server"
        Visible="true">
        <eaf:EAFGridView ID="egvParameters" runat="server" Width="500px" OnRowCommand="egvParameters_RowCommand">
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
    <eaf:FormSectionBlock ID="FormSectionBlock3" Caption="Task Schedule" runat="server"
        Visible="true">
        <table class="cc_EntryForm" cellspacing="0">
            <tr valign="top">
                <td class="fld">
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="ddlScheduleType">Task Schedule Type:</asp:Label>
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
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="dpStart">Start Date:</asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dpStart" /><br />
                    <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="dpStart" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="ddlStartHour">Start Time:</asp:Label>
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
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="dpEnd">End Date:</asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="dpEnd" /><br />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dpEnd" />
                </td>
                <td class="fld">
                    <asp:Label ID="Label12" runat="server" AssociatedControlID="ddlEndHour">End Time:</asp:Label>
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
                    <asp:DropDownList ID="ddlEndMin" runat="server" ToolTip="Minutes>
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
                <eaf:FormSectionBlock ID="FormSectionBlock1" Caption="Daily Schedule" runat="server"
                    Visible="true" SectionType="Frame">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="fld">
                                <asp:Label ID="Label15" runat="server" AssociatedControlID="txtInterval">Interval:</asp:Label>
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
                <eaf:FormSectionBlock ID="FormSectionBlock4" Caption="Weekly Schedule" runat="server"
                    Visible="true" SectionType="Frame">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="lftfld">
                                Every
                            </td>
                            <td>
                                <asp:TextBox ID="txtWeekInterval" runat="server"  ToolTip="Week Interval"/>
                            </td>
                            <td class="lftfld">
                                Weeks
                            </td>
                        </tr>
                    </table>
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="lftfld">
                                <asp:CheckBoxList ID="cblWeeks" runat="server" RepeatColumns="4" RepeatLayout="Table" ToolTip="Every Week of">
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
            <asp:View ID="vMonthly" runat="server" >
                <eaf:FormSectionBlock ID="FormSectionBlock5" Caption="Monthly Schedule" runat="server"
                    Visible="true" SectionType="Frame">
                    <table class="cc_EntryForm" cellspacing="0">
                        <tr valign="top">
                            <td class="lftfld">
                                <asp:RadioButton ID="rbDayOfMonth" runat="server" GroupName="rbgMonthlyType" />
                                Day
                            </td>
                            <td>
                                <asp:TextBox ID="txtDayOfMonth" runat="server" ToolTip="Day of Month"/>
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
                                <asp:Label ID="Label21" runat="server" AssociatedControlID="">of</asp:Label>
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
                    <asp:Label ID="Label22" runat="server" AssociatedControlID="txtEmailReceiver">Email Receiver:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmailReceiver" runat="server" Width="300" />
                </td>
            </tr>
            <tr valign="top">
                <td>
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
    <eaf:ButtonTable ID="btnPanel" runat="server" TableWidth="600px">
        <eaf:LeftButtons ID="leftBtnPanel" runat="server">
            <eaf:EAFButton ID="btnCancel" runat="server" CssClass="s_BtnGrey" Text="Cancel" OnClick="btnCancel_Click"
                ConfirmMessage="Are you sure?" />
            <eaf:EAFButton ID="btnPrevious" runat="server" CssClass="s_BtnGreen" Text="Previous"
                OnClick="btnPrevious_Click" />
        </eaf:LeftButtons>
        <eaf:RightButtons ID="rightBtnPanel" runat="server">
            <eaf:EAFButton ID="btnSave" runat="server" CssClass="s_BtnGreen" Text="Save" OnClick="btnSave_Click">
            </eaf:EAFButton>
        </eaf:RightButtons>
    </eaf:ButtonTable>
</asp:Content>
