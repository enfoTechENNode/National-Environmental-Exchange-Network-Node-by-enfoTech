using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Biz.Manageable;
using DataFlow.Component.Interface;

public partial class Pages_Operation_NewTaskOperationForWizard : Node.Core.UI.Base.AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Event Handlers

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearSavedState();
        this.Response.Redirect("~/Pages/Operation/SearchOperations.aspx", false);
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        this.SaveEntries();
        this.Response.Redirect("~/Pages/Operation/NewOperation.aspx", false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveEntries();
        this.SaveOperation();
    }

    protected void btnAddParameter_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (!this.txtParamName.Text.Trim().Equals(""))
            {
                DataTable dt = (DataTable)this.Session["TASK_PARAMETERS"];
                if (dt != null)
                {
                    DataRow[] drRow = dt.Select("PARAM_NAME = '" + this.txtParamName.Text.Trim() + "'");
                    if (drRow != null && drRow.Length > 0)
                    {
                        this.lblError.Text = "This parameter name has been used already, please input another one.";
                        this.lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["SEQUENCE"] = dt.Rows.Count + 1;
                        dr["PARAM_NAME"] = this.txtParamName.Text;
                        dr["PARAM_VALUE"] = this.txtParamValue.Text;
                        dt.Rows.Add(dr);
                        this.egvParameters.CachedDataTable = dt;
                        this.egvParameters.DataBind();
                    }
                }
            }
            this.txtParamName.Text = string.Empty;
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    protected void btnRemoveParameter_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvParameters.GetCheckedValue("gcbfParamSequence");
            DataTable dt = (DataTable)this.Session["TASK_PARAMETERS"];
            foreach (string id in ids)
            {
                DataRow[] drs = dt.Select("SEQUENCE = '" + id + "'");
                drs[0].Delete();
            }
            dt.AcceptChanges();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["SEQUENCE"] = "" + i;
            this.egvParameters.CachedDataTable = dt;
            this.egvParameters.DataBind();
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    protected void ddlScheduleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.Session["SCHEDULE_TYPE"] != null)
            this.Session.Remove("SCHEDULE_TYPE");
        this.Session.Add("SCHEDULE_TYPE", this.ddlScheduleType.SelectedValue);
        switch (this.ddlScheduleType.SelectedValue)
        {
            case "0":
                this.mvTypes.SetActiveView(this.vInactive);
                break;
            case "1":
                this.mvTypes.SetActiveView(this.vInactive);
                break;
            case "2":
                this.mvTypes.SetActiveView(this.vDaily);
                break;
            case "3":
                this.mvTypes.SetActiveView(this.vWeekly);
                break;
            case "4":
                this.mvTypes.SetActiveView(this.vMonthly);
                break;
            default:
                this.mvTypes.SetActiveView(this.vInactive);
                break;
        }
    }

    //protected void btnAddEmailReceiver_Click(object sender, EventArgs e)
    //{
    //    if (this.txtEmailReceiver.Text.Trim() != "")
    //    {
    //        ListItem item = this.lbEmailReceiverList.Items.FindByValue(this.txtEmailReceiver.Text);
    //        if (item == null)
    //            this.lbEmailReceiverList.Items.Add(new ListItem(this.txtEmailReceiver.Text));
    //    }
    //}

    //protected void btnRemoveEmailReceiver_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < this.lbEmailReceiverList.Items.Count; i++)
    //    {
    //        if (this.lbEmailReceiverList.Items[i].Selected)
    //        {
    //            this.lbEmailReceiverList.Items.RemoveAt(i);
    //            i--;
    //        }
    //    }
    //}

    protected void egvParameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    #endregion

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            //if (this.Session["CLASS_NAME"] != null)
            //    this.txtClassName.Text = this.Session["CLASS_NAME"].ToString();
            //if (this.Session["DLL_NAME"] != null)
            //    this.txtDllPath.Text = this.Session["DLL_NAME"].ToString();

            DataTable dt = new DataTable();
            if (this.Session["TASK_PARAMETERS"] != null)
                dt = (DataTable)this.Session["TASK_PARAMETERS"];
            else
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), new DataColumn("PARAM_VALUE") });
            this.egvParameters.CachedDataTable = dt;
            this.egvParameters.DataBind();
            if (this.Session["TASK_PARAMETERS"] != null)
                this.Session.Remove("TASK_PARAMETERS");
            this.Session.Add("TASK_PARAMETERS", dt);

            if (this.Session["TASK_START_DATE"] != null)
                this.dpStart.Text = ((DateTime)this.Session["TASK_START_DATE"]).ToShortDateString();
            if (this.Session["TASK_START_TIME"] != null)
            {
                string[] split = this.Session["TASK_START_TIME"].ToString().Split(new char[] { ':' });
                this.ddlStartHour.SelectedValue = split[0];
                this.ddlStartMin.SelectedValue = split[1];
                this.ddlStartSec.SelectedValue = split[2];
            }
            if (this.Session["TASK_END_DATE"] != null)
                this.dpEnd.Text = ((DateTime)this.Session["TASK_END_DATE"]).ToShortDateString();
            if (this.Session["TASK_END_TIME"] != null)
            {
                string[] split = this.Session["TASK_END_TIME"].ToString().Split(new char[] { ':' });
                this.ddlEndHour.SelectedValue = split[0];
                this.ddlEndMin.SelectedValue = split[1];
                this.ddlEndSec.SelectedValue = split[2];
            }

            if (this.Session["SCHEDULE_TYPE"] != null)
            {
                this.ddlScheduleType.SelectedValue = this.Session["SCHEDULE_TYPE"].ToString();
                switch (this.Session["SCHEDULE_TYPE"].ToString())
                {
                    case "0":
                        this.mvTypes.SetActiveView(this.vInactive);
                        break;
                    case "1":
                        this.mvTypes.SetActiveView(this.vInactive);
                        break;
                    case "2":
                        this.mvTypes.SetActiveView(this.vDaily);
                        if (this.Session["TASK_INTERVAL"] != null)
                            this.txtInterval.Text = this.Session["TASK_INTERVAL"].ToString();
                        if (this.Session["INTERVAL_TYPE"] != null)
                            this.rblInterval.SelectedValue = this.Session["INTERVAL_TYPE"].ToString();
                        break;
                    case "3":
                        this.mvTypes.SetActiveView(this.vWeekly);
                        if (this.Session["WEEK_INTERVAL"] != null)
                            this.txtWeekInterval.Text = this.Session["WEEK_INTERVAL"].ToString();
                        if (this.Session["DAY_OF_WEEK"] != null)
                        {
                            string[] selected = this.Session["DAY_OF_WEEK"].ToString().Split(new char[] { ',' });
                            ArrayList list = new ArrayList();
                            foreach (string s in selected)
                                list.Add(s);
                            for (int i = 0; i < this.cblWeeks.Items.Count; i++)
                            {
                                if (list.Contains(this.cblWeeks.Items[i].Value))
                                    this.cblWeeks.Items[i].Selected = true;
                            }
                        }
                        break;
                    case "4":
                        this.mvTypes.SetActiveView(this.vMonthly);
                        if (this.Session["MONTHLY_TYPE"] != null)
                        {
                            if (this.Session["MONTHLY_TYPE"].ToString().Equals("D"))
                            {
                                this.rbDayOfMonth.Checked = true;
                                if (this.Session["DAY_OF_MONTH"] != null)
                                    this.txtDayOfMonth.Text = this.Session["DAY_OF_MONTH"].ToString();
                            }
                            else if (this.Session["MONTHLY_TYPE"].ToString().Equals("W"))
                            {
                                this.rbWeekOfMonth.Checked = true;
                                if (this.Session["WEEK_OF_MONTH"] != null)
                                    this.ddlWeekOfMonth.SelectedValue = this.Session["WEEK_OF_MONTH"].ToString();
                                if (this.Session["DAY_OF_WEEK"] != null)
                                    this.ddlDayOfWeek.SelectedValue = this.Session["DAY_OF_WEEK"].ToString();
                            }
                        }
                        if (this.Session["MONTH_OF_YEAR"] != null)
                        {
                            string[] selected = this.Session["MONTH_OF_YEAR"].ToString().Split(new char[] { ',' });
                            ArrayList list = new ArrayList();
                            foreach (string s in selected)
                                list.Add(s);
                            for (int i = 0; i < this.cblMonthOfYear.Items.Count; i++)
                            {
                                if (list.Contains(this.cblMonthOfYear.Items[i].Value))
                                    this.cblMonthOfYear.Items[i].Selected = true;
                            }
                        }
                        break;
                    default:
                        this.mvTypes.SetActiveView(this.vInactive);
                        break;
                }
            }
            else
                this.mvTypes.SetActiveView(this.vInactive);
            //if (this.Session["EMAIL_RECEIVERS"] != null)
            //{
            //    ArrayList emailReceiversList = (ArrayList)this.Session["EMAIL_RECEIVERS"];
            //    foreach (object obj in emailReceiversList)
            //        this.lbEmailReceiverList.Items.Add(new ListItem("" + obj));
            //}
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    #endregion

    #region Private Methods

    private void SaveEntries()
    {
        //if (this.Session["CLASS_NAME"] != null)
        //    this.Session.Remove("CLASS_NAME");
        //this.Session.Add("CLASS_NAME", this.txtClassName.Text);
        //if (this.Session["DLL_NAME"] != null)
        //    this.Session.Remove("DLL_NAME");
        //this.Session.Add("DLL_NAME", this.txtDllPath.Text);
        if (this.Session["TASK_PARAMETERS"] != null)
            this.Session.Remove("TASK_PARAMETERS");
        this.Session.Add("TASK_PARAMETERS", this.egvParameters.CachedDataTable);
        if (this.Session["SCHEDULE_TYPE"] != null)
            this.Session.Remove("SCHEDULE_TYPE");
        this.Session.Add("SCHEDULE_TYPE", this.ddlScheduleType.SelectedValue);
        if (!string.IsNullOrEmpty(this.dpStart.Text.Trim()))
            this.Session.Add("TASK_START_DATE", DateTime.Parse(this.dpStart.Text.Trim()));
        if (this.Session["TASK_START_TIME"] != null)
            this.Session.Remove("TASK_START_TIME");
        this.Session.Add("TASK_START_TIME", this.ddlStartHour.SelectedValue + ":" + this.ddlStartMin.SelectedValue + ":" + this.ddlStartSec.SelectedValue);
        if (this.Session["TASK_END_DATE"] != null)
            this.Session.Remove("TASK_END_DATE");
        if (!string.IsNullOrEmpty(this.dpEnd.Text.Trim()))
            this.Session.Add("TASK_END_DATE", DateTime.Parse(this.dpEnd.Text.Trim()));
        if (this.Session["TASK_END_TIME"] != null)
            this.Session.Remove("TASK_END_TIME");
        this.Session.Add("TASK_END_TIME", this.ddlEndHour.SelectedValue + ":" + this.ddlEndMin.SelectedValue + ":" + this.ddlEndSec.SelectedValue);
        if (this.Session["TASK_INTERVAL"] != null)
            this.Session.Remove("TASK_INTERVAL");
        if (this.Session["INTERVAL_TYPE"] != null)
            this.Session.Remove("INTERVAL_TYPE");
        if (this.Session["WEEK_INTERVAL"] != null)
            this.Session.Remove("WEEK_INTERVAL");
        if (this.Session["DAY_OF_WEEK"] != null)
            this.Session.Remove("DAY_OF_WEEK");
        if (this.Session["MONTHLY_TYPE"] != null)
            this.Session.Remove("MONTHLY_TYPE");
        if (this.Session["DAY_OF_MONTH"] != null)
            this.Session.Remove("DAY_OF_MONTH");
        if (this.Session["WEEK_OF_MONTH"] != null)
            this.Session.Remove("WEEK_OF_MONTH");
        if (this.Session["DAY_OF_WEEK"] != null)
            this.Session.Remove("DAY_OF_WEEK");
        if (this.Session["MONTH_OF_YEAR"] != null)
            this.Session.Remove("MONTH_OF_YEAR");
        if (this.ddlScheduleType.SelectedValue.Equals("2"))
            this.SaveDaily();
        else if (this.ddlScheduleType.SelectedValue.Equals("3"))
            this.SaveWeekly();
        else if (this.ddlScheduleType.SelectedValue.Equals("4"))
            this.SaveMonthly();
        //ArrayList emailReceiversList = new ArrayList();
        //foreach (ListItem item in this.lbEmailReceiverList.Items)
        //    emailReceiversList.Add(item.Value);
        //if (this.Session["EMAIL_RECEIVERS"] != null)
        //    this.Session.Remove("EMAIL_RECEIVERS");
        //this.Session.Add("EMAIL_RECEIVERS", emailReceiversList);
    }

    private void SaveDaily()
    {
        if (!this.txtInterval.Text.Trim().Equals(""))
            this.Session.Add("TASK_INTERVAL", this.txtInterval.Text);
        if (!this.rblInterval.SelectedValue.Trim().Equals(""))
            this.Session.Add("INTERVAL_TYPE", this.rblInterval.SelectedValue);
    }

    private void SaveWeekly()
    {
        if (!this.txtWeekInterval.Text.Trim().Equals(""))
            this.Session.Add("WEEK_INTERVAL", this.txtWeekInterval.Text);
        string selected = "";
        for (int i = 0; i < this.cblWeeks.Items.Count; i++)
        {
            if (this.cblWeeks.Items[i].Selected)
            {
                if (!selected.Trim().Equals("")) selected += ",";
                selected += this.cblWeeks.Items[i].Value;
            }
        }
        if (!selected.Trim().Equals(""))
            this.Session.Add("DAY_OF_WEEK", selected);
    }

    private void SaveMonthly()
    {
        if (this.rbDayOfMonth.Checked)
        {
            this.Session.Add("MONTHLY_TYPE", "D");
            if (!this.txtDayOfMonth.Text.Trim().Equals(""))
                this.Session.Add("DAY_OF_MONTH", this.txtDayOfMonth.Text);
        }
        else if (this.rbWeekOfMonth.Checked)
        {
            this.Session.Add("MONTHLY_TYPE", "W");
            if (!this.ddlWeekOfMonth.SelectedValue.Trim().Equals(""))
                this.Session.Add("WEEK_OF_MONTH", this.ddlWeekOfMonth.SelectedValue);
            if (!this.ddlDayOfWeek.SelectedValue.Trim().Equals(""))
                this.Session.Add("DAY_OF_WEEK", this.ddlDayOfWeek.SelectedValue);
        }
        string selected = "";
        for (int i = 0; i < this.cblMonthOfYear.Items.Count; i++)
        {
            if (this.cblMonthOfYear.Items[i].Selected)
            {
                if (!selected.Trim().Equals("")) selected += ",";
                selected += this.cblMonthOfYear.Items[i].Value;
            }
        }
        if (!selected.Trim().Equals(""))
            this.Session.Add("MONTH_OF_YEAR", selected);
    }

    private void SaveOperation()
    {
        this.lblError.Visible = false;
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            Operation newOp = new Operation(this.Session["OPERATION_NAME"].ToString());
            if (newOp.ID < 0)
            {
                if (!this.ddlScheduleType.SelectedValue.Equals("0"))
                {
                    string error = "";
                    if (string.IsNullOrEmpty(this.dpStart.Text.Trim()))
                        error = "A Start Date must be selected for Schedule Type Once";
                    else if (!this.ddlScheduleType.SelectedValue.Equals("1") && string.IsNullOrEmpty(this.dpEnd.Text.Trim()))
                        error = "An End Date must be selected";
                    else if (!this.ddlScheduleType.SelectedValue.Equals("1") && !this.IsStartDateBeforeEndDate())
                        error = "The End Date and Time must be after the Start Date and Time";
                    else if (this.ddlScheduleType.SelectedValue.Equals("2"))
                    {
                        if (!this.rblInterval.SelectedValue.Equals("M") && !this.rblInterval.SelectedValue.Equals("D"))
                            error = "An Interval Type of Mintes or Daily must be selected";
                        else if (this.txtInterval.Text.Trim().Equals(""))
                            error = "An Interval Value > 0 must be entered";
                        else
                        {
                            int val = 0;
                            if (int.TryParse(this.txtInterval.Text, out val))
                            {
                                if (val <= 0)
                                {
                                    error = "An Interval Value > 0 must be entered";
                                }
                                else if (this.rblInterval.SelectedValue.Equals("M") && val > 1439)
                                {
                                    error = "An Interval Value must be entered between 1 to 1439 minutes";
                                }
                                else if (this.rblInterval.SelectedValue.Equals("D") && val > 365)
                                {
                                    error = "An Interval Value must be entered between 1 to 365 days";
                                }
                            }
                            else
                            {
                                error = "The Interval Value must be numeric";
                            }
                        }
                    }
                    else if (this.ddlScheduleType.SelectedValue.Equals("3"))
                    {
                        string selected = "";
                        for (int i = 0; i < this.cblWeeks.Items.Count; i++)
                        {
                            if (this.cblWeeks.Items[i].Selected)
                            {
                                if (!selected.Trim().Equals(""))
                                    selected += ",";
                                selected += this.cblWeeks.Items[i].Value;
                            }
                        }
                        if (selected.Trim().Equals(""))
                        {
                            error = "At least one day of the week must be selected";
                        }
                        else if (this.txtWeekInterval.Text.Trim().Equals(""))
                        {
                            error = "A Weekly Interval Value > 0 must be entered";
                        }
                        else
                        {
                            int val = 0;

                            if (int.TryParse(this.txtWeekInterval.Text, out val))
                            {
                                if (val < 1 || val > 52)
                                {
                                    error = "An Weekly Interval Value must be entered between 1 to 52 weeks";
                                }
                            }
                            else
                            {
                                error = "The Weekly Interval Value must be numeric";
                            }
                        }
                    }
                    else if (this.ddlScheduleType.SelectedValue.Equals("4"))
                    {
                        string selected = "";
                        for (int i = 0; i < this.cblMonthOfYear.Items.Count; i++)
                        {
                            if (this.cblMonthOfYear.Items[i].Selected)
                            {
                                if (!selected.Trim().Equals(""))
                                    selected += ",";
                                selected += this.cblMonthOfYear.Items[i].Value;
                            }
                        }
                        if (selected.Trim().Equals(""))
                            error = "At least one month of the year must be selected";
                        else if (!this.rbDayOfMonth.Checked && !this.rbWeekOfMonth.Checked)
                            error = "One of Day of or Week of Month must be selected";
                        else
                        {
                            if (this.rbDayOfMonth.Checked)
                            {
                                if (this.txtDayOfMonth.Text.Trim().Equals(""))
                                    error = "A Comma-Separated List of days of the month must be entered.  Each entry must be bewteen 1 and 31, inclusive.";
                                else
                                {
                                    string[] split = this.txtDayOfMonth.Text.Split(new char[] { ',' });
                                    foreach (string s in split)
                                    {
                                        if (s == null || s.Trim().Equals(""))
                                        {
                                            error = "A Comma-Separated List of days of the month must be entered.  Each entry must be bewteen 1 and 31, inclusive.";
                                            break;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                int val = int.Parse(s);
                                                if (val < 1 || val > 31)
                                                {
                                                    error = "A Comma-Separated List of days of the month must be entered.  Each entry must be bewteen 1 and 31, inclusive.";
                                                    break;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                error = "A Comma-Separated List of days of the month must be entered.  Each entry must be bewteen 1 and 31, inclusive.";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (this.ddlWeekOfMonth.SelectedValue.Equals(""))
                                    error = "A Week of the Month must be selected";
                                else if (this.ddlDayOfWeek.SelectedValue.Equals(""))
                                    error = "A Day of the Week must be selected";
                            }
                        }
                    }
                    if (!error.Equals(""))
                    {
                        this.lblError.Text = error;
                        this.lblError.Visible = true;
                        return;
                    }
                }
                newOp.Type = Phrase.OPERATION_TYPE_SCHEDULED_TASK;
                newOp.DomainName = this.Session["DOMAIN_NAME"].ToString();
                newOp.Status = this.Session["OPERATION_STATUS_CD"].ToString();
                newOp.Message = this.Session["OPERATION_STATUS_MSG"].ToString();
                newOp.Description = this.Session["OPERATION_DESCRIPTION"].ToString();
                newOp.PublishInd = "Y";
                newOp.RESTInd = "N";
                newOp.Process = new OpProcess(ProcessType.PROCESS, "", "");//this.txtClassName.Text, this.txtDllPath.Text);
                newOp.RemoveParameters();
                DataTable parameters = (DataTable)this.Session["TASK_PARAMETERS"];
                foreach (DataRow dr in parameters.Rows)
                    newOp.AddParameter(new OpParameter(dr["PARAM_NAME"].ToString(), dr["PARAM_VALUE"].ToString()));
                TaskSchedule schedule = null;
                DateTime start = DateTime.MinValue;
                if (!string.IsNullOrEmpty(this.dpStart.Text.Trim()))
                    start = DateTime.Parse(this.dpStart.Text + " " + this.ddlStartHour.SelectedValue + ":" + this.ddlStartMin.SelectedValue + ":" + this.ddlStartSec.SelectedValue);
                DateTime end = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(this.dpEnd.Text.Trim()))
                    end = DateTime.Parse(this.dpEnd.Text + " " + this.ddlEndHour.SelectedValue + ":" + this.ddlEndMin.SelectedValue + ":" + this.ddlEndSec.SelectedValue);
                if (this.ddlScheduleType.SelectedValue.Equals("0"))
                    schedule = new TaskSchedule("I", DateTime.MinValue, DateTime.MaxValue, TaskSchedule.SCHEDULE_TYPE_MINUTES, -1, -1, -1, null, null, -1, null);
                else if (this.ddlScheduleType.SelectedValue.Equals("1"))
                    schedule = new TaskSchedule("A", start, DateTime.MaxValue, TaskSchedule.SCHEDULE_TYPE_ONCE, 2, 1, -1, null, null, -1, null);
                else if (this.ddlScheduleType.SelectedValue.Equals("2"))
                {
                    int type = TaskSchedule.SCHEDULE_TYPE_MINUTES;
                    int minutes = int.Parse(this.txtInterval.Text);
                    int days = -1;
                    if (this.rblInterval.SelectedValue.Equals("D"))
                    {
                        type = TaskSchedule.SCHEDULE_TYPE_DAILY;
                        days = minutes;
                        minutes = -1;
                    }
                    schedule = new TaskSchedule("A", start, end, type,
                        minutes, days, -1, null, null, -1, null);
                }
                else if (this.ddlScheduleType.SelectedValue.Equals("3"))
                {
                    string selected = "";
                    for (int i = 0; i < this.cblWeeks.Items.Count; i++)
                    {
                        if (this.cblWeeks.Items[i].Selected)
                        {
                            if (!selected.Trim().Equals(""))
                                selected += ",";
                            selected += this.cblWeeks.Items[i].Value;
                        }
                    }
                    schedule = new TaskSchedule("A", start, end, TaskSchedule.SCHEDULE_TYPE_WEEKLY,
                        -1, -1, int.Parse(this.txtWeekInterval.Text.Trim()), selected, null, -1, null);
                }
                else if (this.ddlScheduleType.SelectedValue.Equals("4"))
                {
                    string selected = "";
                    for (int i = 0; i < this.cblMonthOfYear.Items.Count; i++)
                    {
                        if (this.cblMonthOfYear.Items[i].Selected)
                        {
                            if (!selected.Trim().Equals(""))
                                selected += ",";
                            selected += this.cblMonthOfYear.Items[i].Value;
                        }
                    }
                    if (this.rbDayOfMonth.Checked)
                        schedule = new TaskSchedule("A", start, end, TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS,
                            -1, -1, -1, null, this.txtDayOfMonth.Text, -1, selected);
                    else
                        schedule = new TaskSchedule("A", start, end, TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS,
                            -1, -1, -1, this.ddlDayOfWeek.SelectedValue, null, int.Parse(this.ddlWeekOfMonth.SelectedValue), selected);
                }
                newOp.TaskSchedule = schedule;
                //newOp.RemoveEmailReceivers();
                //foreach (ListItem item in this.lbEmailReceiverList.Items)
                //    newOp.AddEmailReceiver(item.Value);
                string opMessage = newOp.Save(this.LoggedInUser);

                #region This is for Data Flow Wizard only
                DllManager dllMgr = new DllManager();
                IActionProcess process = dllMgr.GetActionProcess();
                IActionOperation actionOp = process.CreateActionOperation();
                actionOp.OperationID = newOp.ID.ToString();
                actionOp.OperationName = this.Session["OPERATION_NAME"].ToString();
                if (this.Session[Phrase.VERSION_NO] != null)
                {
                    if (this.Session[Phrase.VERSION_NO].ToString() == Phrase.VERSION_11)
                        actionOp.OperationVersion = OperationVersion.VER_11;
                    else
                        actionOp.OperationVersion = OperationVersion.VER_20;
                }
                else
                    actionOp.OperationVersion = OperationVersion.VER_11;

                actionOp.OperationType = OperationType.SCHEDULED_TASK;
                actionOp.OperationDomain = this.Session["DOMAIN_NAME"].ToString();
                actionOp.Description = this.Session["OPERATION_DESCRIPTION"].ToString();
                actionOp.WebService = OperationWebMethod.NONE;
                
                System.Collections.Generic.List<IActionParameter> lstVariables = actionOp.Variables;
                DataTable taskParams = (DataTable)this.Session["TASK_PARAMETERS"];
                if (taskParams != null && taskParams.Rows.Count > 0)
                {
                    foreach (DataRow dr in taskParams.Rows)
                        actionOp.CreateActionParameter(dr["PARAM_NAME"].ToString(), dr["PARAM_VALUE"].ToString());
                }

                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(actionOp.GetOperationConfig());
                    newOp.Config = xmlDoc;
                    opMessage = newOp.Save(this.LoggedInUser);
                }
                catch (XmlException e)
                {
                    Console.WriteLine(e.Message);
                }
                #endregion

                if (opMessage != null && opMessage.Trim() != "")
                {
                    this.lblError.Text = opMessage + "<br/>&nbsp;";
                    this.lblError.Visible = true;
                }
                else
                {
                    this.ClearSavedState();
                    this.Response.Redirect("~/Pages/DataWizard/DataFlowWizard.aspx?OPID=" + newOp.ID);
                }
            }
            else
            {
                this.lblError.Text = "An Operation by the Operation Name has already been created on this Node.  Choose a Different Operation Name if you want to save this operation.";
                this.lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    private void ClearSavedState()
    {
        if (this.Session["OPERATION_NAME"] != null)
            this.Session.Remove("OPERATION_NAME");
        if (this.Session["OPERATION_STATUS_CD"] != null)
            this.Session.Remove("OPERATION_STATUS_CD");
        if (this.Session["OPERATION_STATUS_MSG"] != null)
            this.Session.Remove("OPERATION_STATUS_MSG");
        if (this.Session["OPERATION_DESCRIPTION"] != null)
            this.Session.Remove("OPERATION_DESCRIPTION");
        if (this.Session["OPERATION_TYPE"] != null)
            this.Session.Remove("OPERATION_TYPE");
        if (this.Session["WEB_SERVICE_ID"] != null)
            this.Session.Remove("WEB_SERVICE_ID");
        if (this.Session["WEB_SERVICE_NAME"] != null)
            this.Session.Remove("WEB_SERVICE_NAME");
        if (this.Session["PRE_PROCESS_TABLE"] != null)
            this.Session.Remove("PRE_PROCESS_TABLE");
        if (this.Session["POST_PROCESS_TABLE"] != null)
            this.Session.Remove("POST_PROCESS_TABLE");
        //if (this.Session["DLL_NAME"] != null)
        //    this.Session.Remove("DLL_NAME");
        //if (this.Session["CLASS_NAME"] != null)
        //    this.Session.Remove("CLASS_NAME");
        if (this.Session["SOLICIT_BEGIN"] != null)
            this.Session.Remove("SOLICIT_BEGIN");
        if (this.Session["SOLICIT_END"] != null)
            this.Session.Remove("SOLICIT_END");
        if (this.Session["SOLICIT_USER"] != null)
            this.Session.Remove("SOLICIT_USER");
        if (this.Session["SOLICIT_PWD"] != null)
            this.Session.Remove("SOLICIT_PWD");
        if (this.Session["SOLICIT_DATAFLOW"] != null)
            this.Session.Remove("SOLICIT_DATAFLOW");
        if (this.Session["QUERY_PARAMS"] != null)
            this.Session.Remove("QUERY_PARAMS");
        if (this.Session["SOLICIT_PARAMS"] != null)
            this.Session.Remove("SOLICIT_PARAMS");
        if (this.Session["TASK_PARAMETERS"] != null)
            this.Session.Remove("TASK_PARAMETERS");
        if (this.Session["SCHEDULE_TYPE"] != null)
            this.Session.Remove("SCHEDULE_TYPE");
        if (this.Session["TASK_START_DATE"] != null)
            this.Session.Remove("TASK_START_DATE");
        if (this.Session["TASK_START_TIME"] != null)
            this.Session.Remove("TASK_START_TIME");
        if (this.Session["TASK_END_DATE"] != null)
            this.Session.Remove("TASK_END_DATE");
        if (this.Session["TASK_END_TIME"] != null)
            this.Session.Remove("TASK_END_TIME");
        if (this.Session["TASK_INTERVAL"] != null)
            this.Session.Remove("TASK_INTERVAL");
        if (this.Session["INTERVAL_TYPE"] != null)
            this.Session.Remove("INTERVAL_TYPE");
        if (this.Session["WEEK_INTERVAL"] != null)
            this.Session.Remove("WEEK_INTERVAL");
        if (this.Session["DAY_OF_WEEK"] != null)
            this.Session.Remove("DAY_OF_WEEK");
        if (this.Session["MONTHLY_TYPE"] != null)
            this.Session.Remove("MONTHLY_TYPE");
        if (this.Session["DAY_OF_MONTH"] != null)
            this.Session.Remove("DAY_OF_MONTH");
        if (this.Session["WEEK_OF_MONTH"] != null)
            this.Session.Remove("WEEK_OF_MONTH");
        if (this.Session["DAY_OF_WEEK"] != null)
            this.Session.Remove("DAY_OF_WEEK");
        if (this.Session["MONTH_OF_YEAR"] != null)
            this.Session.Remove("MONTH_OF_YEAR");
        if (this.Session["EMAIL_RECEIVERS"] != null)
            this.Session.Remove("EMAIL_RECEIVERS");
    }

    private bool IsStartDateBeforeEndDate()
    {
        DateTime start = DateTime.Parse(this.dpStart.Text.Trim());
        DateTime end = DateTime.Parse(this.dpEnd.Text.Trim());
        if (start.CompareTo(end) > 0)
            return false;
        else if (start.CompareTo(end) == 0)
        {
            int startHour = int.Parse(this.ddlStartHour.SelectedValue);
            int endHour = int.Parse(this.ddlEndHour.SelectedValue);
            if (startHour > endHour)
                return false;
            else if (startHour == endHour)
            {
                int startMin = int.Parse(this.ddlStartMin.SelectedValue);
                int endMin = int.Parse(this.ddlEndMin.SelectedValue);
                if (startMin > endMin)
                    return false;
                else if (startMin == endMin)
                {
                    int startSec = int.Parse(this.ddlStartSec.SelectedValue);
                    int endSec = int.Parse(this.ddlEndSec.SelectedValue);
                    if (startSec >= endSec)
                        return false;
                }
            }
        }
        return true;
    }

    #endregion
}
