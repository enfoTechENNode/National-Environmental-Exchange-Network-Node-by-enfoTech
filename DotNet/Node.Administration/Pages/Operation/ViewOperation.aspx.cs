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

using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Biz.NAAS;
using Node.Core.NAASPolicy;

public partial class ViewOperation_aspx : Node.Core.UI.Base.AdminPageBase
{
    public ViewOperation_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            Operation op = new Operation(int.Parse(this.Request["OPID"]));

            this.txtOperationName.Text = op.Name;
            this.ddlStatus.SelectedValue = op.Status;
            this.txtStatusMessage.Text = op.Message != null ? op.Message : "";
            this.txtDescription.Text = op.Description != null ? op.Description : "";
            this.ddlOpType.SelectedValue = op.Type;
            this.chkPublish.Checked = (op.PublishInd == "Y" ? true : false);
            this.chkREST.Checked = (op.RESTInd == "Y" ? true : false);
          
            if (op.Type.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
            {
                this.mvOpType.SetActiveView(this.vWebService);
                this.ddlWebService.DataSource = WebService.GetWebServicesList(this.Session["DOMAIN_NAME"].ToString());
                this.ddlWebService.DataValueField = "WEB_SERVICE_ID";
                this.ddlWebService.DataTextField = "WEB_SERVICE_NAME";
                this.ddlWebService.DataBind();
                this.ddlWebService.SelectedValue = "" + op.WebServiceID;

                DataTable preProcessesTable = new DataTable();
                preProcessesTable.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("DLL_NAME"), new DataColumn("CLASS_NAME") });
                if (this.Session["PRE_PROCESS_TABLE"] != null)
                    this.Session.Remove("PRE_PROCESS_TABLE");
                foreach (object obj in op.PreProcesses)
                {
                    OpProcess pre = (OpProcess)obj;
                    DataRow dr = preProcessesTable.NewRow();
                    dr["SEQUENCE"] = "" + pre.Sequence;
                    dr["DLL_NAME"] = pre.DllPath;
                    dr["CLASS_NAME"] = pre.ClassName;
                    preProcessesTable.Rows.Add(dr);
                }
                this.egvPreProcesses.CachedDataTable = preProcessesTable;
                this.egvPreProcesses.DataBind();
                this.Session.Add("PRE_PROCESS_TABLE", preProcessesTable);

                if (op.WebServiceName.Equals(Phrase.WEB_SERVICE_QUERY))
                {
                    this.mvProcess.SetActiveView(this.vQuery);
                    OpProcess proc = op.Process;
                    this.txtQueryProcessDllPath.Text = proc != null ? proc.DllPath : string.Empty;
                    this.txtQueryProcessClassName.Text = proc != null ? proc.ClassName : string.Empty;
                    DataTable queryParametersTable = new DataTable();
                    queryParametersTable.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), 
                    new DataColumn("DEDLEncoding"), new DataColumn("DEDLOccurenceNumber"),
                    new DataColumn("DEDLType"), new DataColumn("DEDLRequiredIndicator"), new DataColumn("DEDLTypeDescriptor")});
                    if (this.Session["QUERY_PARAMS"] != null)
                        this.Session.Remove("QUERY_PARAMS");
                    if (op.Parameters != null)
                    {
                        for (int i = 0; i < op.Parameters.Count; i++)
                        {
                            OpParameter param = (OpParameter)op.Parameters[i];
                            int temp = i + 1;
                            DataRow dr = queryParametersTable.NewRow();
                            dr["SEQUENCE"] = "" + temp;
                            dr["PARAM_NAME"] = param.Name;
                            dr["DEDLType"] = param.DEDLType;
                            dr["DEDLEncoding"] = param.DEDLEncoding;
                            dr["DEDLOccurenceNumber"] = param.DEDLOccurenceNumber;
                            dr["DEDLRequiredIndicator"] = param.DEDLRequiredIndicator;
                            dr["DEDLTypeDescriptor"] = param.DEDLTypeDescriptor;

                            queryParametersTable.Rows.Add(dr);
                        }
                    }
                    this.egvQueryParameters.CachedDataTable = queryParametersTable;
                    this.egvQueryParameters.DataBind();
                    this.Session.Add("QUERY_PARAMS", queryParametersTable);
                }
                else if (op.WebServiceName.Equals(Phrase.WEB_SERVICE_SOLICIT))
                {
                    this.mvProcess.SetActiveView(this.vSolicit);
                    OpProcess proc = op.Process;
                    this.txtSolicitProcessDllPath.Text = proc != null ? proc.DllPath : string.Empty;
                    this.txtSolicitProcessClassName.Text = proc != null ? proc.ClassName : string.Empty;
                    //this.cbSolicitAnytime.Checked = proc.IsSolicitRestrictedToTimeInterval;
                    //if (this.cbSolicitAnytime.Checked)
                    //{
                    //    if (proc.SolicitStartTime != null)
                    //    {
                    //        string[] split = proc.SolicitStartTime.Split(new char[] { ':' });
                    //        if (split.Length > 0)
                    //            this.solBegHour.SelectedValue = split[0];
                    //        if (split.Length > 1)
                    //            this.solBegMin.SelectedValue = split[1];
                    //    }
                    //    if (proc.SolicitEndTime != null)
                    //    {
                    //        string[] split = proc.SolicitEndTime.Split(new char[] { ':' });
                    //        if (split.Length > 0)
                    //            this.solEndHour.SelectedValue = split[0];
                    //        if (split.Length > 1)
                    //            this.solEndMin.SelectedValue = split[1];
                    //    }
                    //}
                    //this.cbSubmit.Checked = proc.HasSolicitSubmitCredentials;
                    //if (this.cbSubmit.Checked)
                    //{
                    //    if (proc.SolicitSubmitUID != null)
                    //        this.txtSubmitUserID.Text = proc.SolicitSubmitUID;
                    //    if (proc.SolicitSubmitDataFlow != null)
                    //        this.txtDataFlow.Text = proc.SolicitSubmitDataFlow;
                    //}
                    DataTable solicitParametersTable = new DataTable();
                    solicitParametersTable.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), 
                    new DataColumn("DEDLEncoding"), new DataColumn("DEDLOccurenceNumber"),
                    new DataColumn("DEDLType"), new DataColumn("DEDLRequiredIndicator"), new DataColumn("DEDLTypeDescriptor")});
                    if (this.Session["SOLICIT_PARAMS"] != null)
                        this.Session.Remove("SOLICIT_PARAMS");
                    if (op.Parameters != null)
                    {
                        for (int i = 0; i < op.Parameters.Count; i++)
                        {
                            OpParameter param = (OpParameter)op.Parameters[i];
                            int temp = i + 1;
                            DataRow dr = solicitParametersTable.NewRow();
                            dr["SEQUENCE"] = "" + temp;
                            dr["PARAM_NAME"] = param.Name;
                            dr["DEDLType"] = param.DEDLType;
                            dr["DEDLEncoding"] = param.DEDLEncoding;
                            dr["DEDLOccurenceNumber"] = param.DEDLOccurenceNumber;
                            dr["DEDLRequiredIndicator"] = param.DEDLRequiredIndicator;
                            dr["DEDLTypeDescriptor"] = param.DEDLTypeDescriptor;
                            solicitParametersTable.Rows.Add(dr);
                        }
                    }
                    this.egvSolicitParameters.CachedDataTable = solicitParametersTable;
                    this.egvSolicitParameters.DataBind();
                    this.Session.Add("SOLICIT_PARAMS", solicitParametersTable);
                }
                else
                {
                    this.mvProcess.SetActiveView(this.vGeneric);
                    bool isDefault = false;
                    OpProcess proc = op.Process;
                    if (proc != null && proc.DllPath.Equals(Phrase.DEFAULT_DLL))
                    {
                        switch (this.ddlWebService.SelectedItem.Text)
                        {
                            case Phrase.WEB_SERVICE_AUTHENTICATE:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_AUTHENTICATE))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_DOWNLOAD:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_DOWNLOAD))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_GETSERVICES:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_GETSERVICES))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_GETSTATUS:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_GETSTATUS))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_NODEPING:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_NODEPING))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_NOTIFY:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_NOTIFY))
                                    isDefault = true;
                                break;
                            case Phrase.WEB_SERVICE_SUBMIT:
                                if (proc.ClassName.Equals(Phrase.DEFAULT_SUBMIT))
                                    isDefault = true;
                                break;
                        }
                    }
                    if (isDefault)
                    {
                        this.cbDefault.Checked = true;
                        this.cbDefault_CheckedChanged(null, null);
                    }
                    else
                    {
                        this.txtProcessClassName.Text = proc != null ? proc.ClassName : string.Empty;
                        this.txtProcessDllPath.Text = proc != null ? proc.DllPath : string.Empty;
                    }
                }

                DataTable postProcessesTable = new DataTable();
                postProcessesTable.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("DLL_NAME"), new DataColumn("CLASS_NAME") });
                if (this.Session["POST_PROCESS_TABLE"] != null)
                    this.Session.Remove("POST_PROCESS_TABLE");
                foreach (object obj in op.PostProcesses)
                {
                    OpProcess post = (OpProcess)obj;
                    DataRow dr = postProcessesTable.NewRow();
                    dr["SEQUENCE"] = "" + post.Sequence;
                    dr["DLL_NAME"] = post.DllPath;
                    dr["CLASS_NAME"] = post.ClassName;
                    postProcessesTable.Rows.Add(dr);
                }
                this.egvPostProcesses.CachedDataTable = postProcessesTable;
                this.egvPostProcesses.DataBind();
                this.Session.Add("POST_PROCESS_TABLE", postProcessesTable);
            }
            else
            {
                this.mvOpType.SetActiveView(this.vTask);

                OpProcess proc = op.Process;
                this.txtTaskClassName.Text = proc.ClassName;
                this.txtTaskDllPath.Text = proc.DllPath;

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), new DataColumn("PARAM_VALUE") });
                ArrayList parameters = op.Parameters;
                for (int i = 1; i <= parameters.Count; i++)
                {
                    OpParameter param = (OpParameter)parameters[i - 1];
                    DataRow dr = dt.NewRow();
                    dr["SEQUENCE"] = "" + i;
                    dr["PARAM_NAME"] = param.Name;
                    dr["PARAM_VALUE"] = param.Value;
                    dt.Rows.Add(dr);
                }
                this.egvParameters.CachedDataTable = dt;
                this.egvParameters.DataBind();
                if (this.Session["TASK_PARAMETERS"] != null)
                    this.Session.Remove("TASK_PARAMETERS");
                this.Session.Add("TASK_PARAMETERS", dt);

                TaskSchedule schedule = op.TaskSchedule;
                if (schedule.Status.Equals("I"))
                {
                    this.ddlScheduleType.SelectedValue = "0";
                    this.mvTypes.SetActiveView(this.vInactive);
                }
                else
                {
                    this.dpStart.Text = op.TaskSchedule.StartDate.ToShortDateString();
                    this.ddlStartHour.SelectedValue = op.TaskSchedule.StartDate.ToString("HH");
                    this.ddlStartMin.SelectedValue = op.TaskSchedule.StartDate.ToString("mm");
                    this.ddlStartSec.SelectedValue = op.TaskSchedule.StartDate.ToString("ss");
                    if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_ONCE)
                    {
                        this.ddlScheduleType.SelectedValue = "1";
                        this.mvTypes.SetActiveView(this.vInactive);
                    }
                    else
                    {
                        this.dpEnd.Text = op.TaskSchedule.EndDate.ToShortDateString();
                        this.ddlEndHour.SelectedValue = op.TaskSchedule.EndDate.ToString("HH");
                        this.ddlEndMin.SelectedValue = op.TaskSchedule.EndDate.ToString("mm");
                        this.ddlEndSec.SelectedValue = op.TaskSchedule.EndDate.ToString("ss");
                        if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_MINUTES || schedule.Type == TaskSchedule.SCHEDULE_TYPE_DAILY)
                        {
                            this.mvTypes.SetActiveView(this.vDaily);
                            this.ddlScheduleType.SelectedValue = "2";
                            if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_MINUTES)
                            {
                                this.rblInterval.SelectedValue = "M";
                                this.txtInterval.Text = op.TaskSchedule.IntervalMinutes.ToString();
                            }
                            else
                            {
                                this.rblInterval.SelectedValue = "D";
                                this.txtInterval.Text = op.TaskSchedule.IntervalDays.ToString();
                            }
                        }
                        else if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_WEEKLY)
                        {
                            this.mvTypes.SetActiveView(this.vWeekly);
                            this.ddlScheduleType.SelectedValue = "3";
                            this.txtWeekInterval.Text = op.TaskSchedule.IntervalWeeks.ToString();
                            string[] split = op.TaskSchedule.DaysOfWeek.Split(new char[] { ',' });
                            ArrayList days = new ArrayList();
                            foreach (string s in split)
                                days.Add(s);
                            foreach (ListItem item in this.cblWeeks.Items)
                            {
                                if (days.Contains(item.Value))
                                    item.Selected = true;
                            }
                        }
                        else if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS || schedule.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_WEEKS)
                        {
                            this.mvTypes.SetActiveView(this.vMonthly);
                            string[] split = schedule.MonthsOfYear.Split(new char[] { ',' });
                            ArrayList months = new ArrayList();
                            foreach (string s in split)
                                months.Add(s);
                            foreach (ListItem item in this.cblMonthOfYear.Items)
                            {
                                if (months.Contains(item.Value))
                                    item.Selected = true;
                            }
                            if (schedule.Type == TaskSchedule.SCHEDULE_TYPE_MONTHLY_DAYS)
                            {
                                this.rbDayOfMonth.Checked = true;
                                this.rbWeekOfMonth.Checked = false;
                                this.txtDayOfMonth.Text = schedule.DaysOfMonth;
                            }
                            else
                            {
                                this.rbDayOfMonth.Checked = false;
                                this.rbWeekOfMonth.Checked = true;
                                this.ddlWeekOfMonth.SelectedValue = schedule.WeekOfMonth.ToString();
                                this.ddlDayOfWeek.SelectedValue = schedule.DaysOfWeek;
                            }
                        }
                    }
                }
                foreach (object obj in op.EmailReceivers)
                    this.lbEmailReceiverList.Items.Add(new ListItem("" + obj));
            }

            //DataTable dtOperationLogs = OperationLog.SearchLogs(op.Name, op.Type, op.WebServiceID, op.Status, op.DomainID, "", "", "", DateTime.MinValue.AddDays(1), DateTime.Now.AddMonths(6), this.LoggedInUser);

            Node.Core.Data.Common.OperationLogs oplog = new Node.Core.Data.Common.OperationLogs();

            if (oplog.IsOperationLogExisted(op.ID.ToString()))
            {
                this.btnDeleteOperation.Visible = false;
            }
            else
            {
                this.btnDeleteOperation.Visible = true;
            }

            if (op.Type.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE) && (op.WebServiceName.Equals(Phrase.WEB_SERVICE_QUERY) || op.WebServiceName.Equals(Phrase.WEB_SERVICE_SOLICIT)))
            {
                pnlDenyPolicy.Visible = true;

                string sResult = "";
                PolicyManager pmgr = new PolicyManager();
                bool bPolicyExisted = pmgr.GetExplicitRightFromNAAS(op, out sResult);
                if (bPolicyExisted)
                {
                    chkDenyPolicy.Checked = true;
                }
                else
                {
                    if (sResult.Equals(""))
                    {
                        chkDenyPolicy.Checked = false;
                    }
                    else
                    {
                        lblError.Text = sResult;
                        lblError.Visible = true;
                    }
                }
                
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
    #endregion

    #region Event Handlers

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Operation/SearchOperations.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.SaveOperation();
    }

    protected void btnAddPreProcess_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (!this.txtDllPath.Text.Trim().Equals("") && !this.txtClassName.Text.Trim().Equals(""))
            {
                DataTable dt = (DataTable)this.Session["PRE_PROCESS_TABLE"];
                DataRow dr = dt.NewRow();
                dr["SEQUENCE"] = dt.Rows.Count + 1;
                dr["DLL_NAME"] = this.txtDllPath.Text;
                dr["CLASS_NAME"] = this.txtClassName.Text;
                dt.Rows.Add(dr);
                this.egvPreProcesses.CachedDataTable = dt;
                this.egvPreProcesses.DataBind();
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

    protected void btnRemovePreProcesses_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvPreProcesses.GetCheckedValue("gcbfPreSequence");
            DataTable dt = (DataTable)this.Session["PRE_PROCESS_TABLE"];
            foreach (string id in ids)
            {
                DataRow[] drs = dt.Select("SEQUENCE = '" + id + "'");
                drs[0].Delete();
            }
            dt.AcceptChanges();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["SEQUENCE"] = "" + i;
            this.egvPreProcesses.CachedDataTable = dt;
            this.egvPreProcesses.DataBind();
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

    protected void btnAddPostProcess_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (!this.txtPostDllPath.Text.Trim().Equals("") && !this.txtPostClassName.Text.Trim().Equals(""))
            {
                DataTable dt = (DataTable)this.Session["POST_PROCESS_TABLE"];
                DataRow dr = dt.NewRow();
                dr["SEQUENCE"] = dt.Rows.Count + 1;
                dr["DLL_NAME"] = this.txtPostDllPath.Text;
                dr["CLASS_NAME"] = this.txtPostClassName.Text;
                dt.Rows.Add(dr);
                this.egvPostProcesses.CachedDataTable = dt;
                this.egvPostProcesses.DataBind();
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

    protected void btnRemovePostProcesses_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvPostProcesses.GetCheckedValue("gcbfPostSequence");
            DataTable dt = (DataTable)this.Session["POST_PROCESS_TABLE"];
            foreach (string id in ids)
            {
                DataRow[] drs = dt.Select("SEQUENCE = '" + id + "'");
                drs[0].Delete();
            }
            dt.AcceptChanges();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["SEQUENCE"] = "" + i;
            this.egvPostProcesses.CachedDataTable = dt;
            this.egvPostProcesses.DataBind();
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

    protected void btnAddQueryParameter_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (!this.txtQueryParam.Text.Trim().Equals(""))
            {
                DataTable dt = (DataTable)this.Session["QUERY_PARAMS"];
                if (dt != null)
                {
                    DataRow[] drRow = dt.Select("PARAM_NAME = '" + this.txtQueryParam.Text.Trim() + "'");
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
                        dr["PARAM_NAME"] = this.txtQueryParam.Text;
                        dr["DEDLType"] = this.txtQueryParamDEDLType.Text;
                        dr["DEDLEncoding"] = this.txtQueryParamDEDLEncoding.Text;
                        dr["DEDLOccurenceNumber"] = this.txtQueryParamDEDLOccurance.Text;
                        dr["DEDLRequiredIndicator"] = this.txtQueryParamDEDLRequiredInd.Text;
                        dr["DEDLTypeDescriptor"] = this.txtQueryParamDEDLTypeDesc.Text;
                        dt.Rows.Add(dr);
                        this.egvQueryParameters.CachedDataTable = dt;
                        this.egvQueryParameters.DataBind();
                    }
                }
            }
            this.txtQueryParam.Text = string.Empty;
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

    protected void btnRemoveQueryParameters_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvQueryParameters.GetCheckedValue("gcbfQueryParameters");
            DataTable dt = (DataTable)this.Session["QUERY_PARAMS"];
            foreach (string id in ids)
            {
                DataRow[] drs = dt.Select("SEQUENCE = '" + id + "'");
                drs[0].Delete();
            }
            dt.AcceptChanges();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["SEQUENCE"] = "" + i;
            this.egvQueryParameters.CachedDataTable = dt;
            this.egvQueryParameters.DataBind();
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

    protected void btnAddSolicitParameter_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (!this.txtSolicitParam.Text.Trim().Equals(""))
            {
                DataTable dt = (DataTable)this.Session["SOLICIT_PARAMS"];
                if (dt != null)
                {
                    DataRow[] drRow = dt.Select("PARAM_NAME = '" + this.txtSolicitParam.Text.Trim() + "'");
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
                        dr["PARAM_NAME"] = this.txtSolicitParam.Text;
                        dr["DEDLType"] = this.txtSolicitParamDEDLType.Text;
                        dr["DEDLEncoding"] = this.txtSolicitParamDEDLEncoding.Text;
                        dr["DEDLOccurenceNumber"] = this.txtSolicitParamDEDLOccurance.Text;
                        dr["DEDLRequiredIndicator"] = this.txtSolicitParamDEDLRequiredInd.Text;
                        dr["DEDLTypeDescriptor"] = this.txtSolicitParamDEDLTypeDesc.Text;
                        dt.Rows.Add(dr);
                        this.egvSolicitParameters.CachedDataTable = dt;
                        this.egvSolicitParameters.DataBind();
                    }
                }
            }
            this.txtSolicitParam.Text = string.Empty;
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

    protected void btnRemoveSolicitParameters_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvSolicitParameters.GetCheckedValue("gcbfSolicitParameters");
            DataTable dt = (DataTable)this.Session["SOLICIT_PARAMS"];
            foreach (string id in ids)
            {
                DataRow[] drs = dt.Select("SEQUENCE = '" + id + "'");
                drs[0].Delete();
            }
            dt.AcceptChanges();
            for (int i = 1; i <= dt.Rows.Count; i++)
                dt.Rows[i - 1]["SEQUENCE"] = "" + i;
            this.egvSolicitParameters.CachedDataTable = dt;
            this.egvSolicitParameters.DataBind();
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

    protected void btnAddEmailReceiver_Click(object sender, EventArgs e)
    {
        if (this.txtEmailReceiver.Text.Trim() != "")
        {
            ListItem item = this.lbEmailReceiverList.Items.FindByValue(this.txtEmailReceiver.Text);
            if (item == null)
                this.lbEmailReceiverList.Items.Add(new ListItem(this.txtEmailReceiver.Text));
        }
    }

    protected void btnRemoveEmailReceiver_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.lbEmailReceiverList.Items.Count; i++)
        {
            if (this.lbEmailReceiverList.Items[i].Selected)
            {
                this.lbEmailReceiverList.Items.RemoveAt(i);
                i--;
            }
        }
    }

    protected void egvPreProcesses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvPostProcesses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvQueryParameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvSolicitParameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvParameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void btnDeleteOperation_Click(object sender, EventArgs e)
    {
        Operation op = new Operation(int.Parse(this.Request["OPID"]));
        if (op != null)
        {
            if (op.Task != null && op.Task.Status.ToUpper() != "I")
            {
                this.lblError.Text = "The operation can not be deleted because its task status is Active. You have to inactivate the task before deleting task and operation.";
                this.lblError.Visible = true;
            }
            else
            {
                op.Delete();
                this.Response.Redirect("~/Pages/Operation/SearchOperations.aspx");
            }
        }
    }

    protected void cbDefault_CheckedChanged(object sender, EventArgs e)
    {
        if (cbDefault.Checked)
        {
            this.txtProcessDllPath.ReadOnly = true;
            this.txtProcessClassName.ReadOnly = true;
            this.txtProcessDllPath.Text = "";
            this.txtProcessClassName.Text = "";
            this.dllPath.Visible = false;
            this.ClsName.Visible = false;
        }
        else
        {
            this.txtProcessDllPath.ReadOnly = false;
            this.txtProcessClassName.ReadOnly = false;
            this.dllPath.Visible = true;
            this.ClsName.Visible = true;
        }
    }


    #endregion

    #region Private Methods

    private void SaveOperation()
    {
        this.lblError.Visible = false;
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            Operation op = null;
            if (this.ddlOpType.SelectedValue.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
                op = new Operation(this.txtOperationName.Text.Trim(), this.ddlWebService.SelectedItem.Text);
            else
                op = new Operation(this.txtOperationName.Text.Trim());
            if (op.ID >= 0)
            {
                string error = "";
                if (this.ddlOpType.SelectedValue.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
                {
                    if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_QUERY))
                    {
                        if (this.txtQueryProcessDllPath.Text.Trim().Equals("") || this.txtQueryProcessClassName.Text.Trim().Equals(""))
                            error = "A query dll and class name must be specified for the process";
                    }
                    else if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_SOLICIT))
                    {
                        if (this.txtSolicitProcessDllPath.Text.Trim().Equals("") || this.txtSolicitProcessClassName.Text.Trim().Equals(""))
                            error = "A solicit dll and class name must be specified for the process";
                    }
                    else
                    {
                        if (!this.cbDefault.Checked && (this.txtProcessDllPath.Text.Trim().Equals("") || this.txtProcessClassName.Text.Trim().Equals("")))
                            error = "The default process check box must be checked or a dll and class name must be specified for the process";
                    }
                }
                else if (!this.ddlScheduleType.SelectedValue.Equals("0"))
                {
                    if (string.IsNullOrEmpty(this.dpStart.Text.Trim()))
                        error = "A Start Date must be selected";
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
                }
                if (!error.Trim().Equals(""))
                {
                    this.lblError.Text = error;
                    this.lblError.Visible = true;
                    return;
                }
            }

            op.Status = this.ddlStatus.SelectedValue;
            op.Message = this.txtStatusMessage.Text;
            op.Description = this.txtDescription.Text;
            if (this.ddlOpType.SelectedValue.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
            {
                op.Type = Phrase.OPERATION_TYPE_WEB_SERVICE;
                op.RemovePreProcesses();
                DataTable preProcs = (DataTable)this.Session["PRE_PROCESS_TABLE"];
                foreach (DataRow dr in preProcs.Rows)
                    op.AddPreProcess(new OpProcess(ProcessType.PRE_PROCESS, dr["CLASS_NAME"].ToString(), dr["DLL_NAME"].ToString(), this.ddlWebService.SelectedItem.Text, int.Parse(dr["SEQUENCE"].ToString())));
                if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_QUERY))
                {
                    op.Process = new OpProcess(ProcessType.PROCESS, this.txtQueryProcessClassName.Text, this.txtQueryProcessDllPath.Text, Phrase.WEB_SERVICE_QUERY);
                    op.RemoveParameters();
                    DataTable queryParams = (DataTable)this.Session["QUERY_PARAMS"];
                    foreach (DataRow dr in queryParams.Rows)
                    {
                        OpParameter newOpPara = new OpParameter(dr["PARAM_NAME"].ToString());
                        newOpPara.DEDLType = "" + dr["DEDLRequiredIndicator"];
                        newOpPara.DEDLEncoding = "" + dr["DEDLEncoding"];
                        newOpPara.DEDLOccurenceNumber = "" + dr["DEDLOccurenceNumber"];
                        newOpPara.DEDLRequiredIndicator = "" + dr["DEDLType"];
                        newOpPara.DEDLTypeDescriptor = "" + dr["DEDLTypeDescriptor"];
                        op.AddParameter(newOpPara);
                    }
                }
                else if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_SOLICIT))
                {
                    OpProcess proc = op.Process;
                    proc.ClassName = this.txtSolicitProcessClassName.Text;
                    proc.DllPath = this.txtSolicitProcessDllPath.Text;
                    //proc.IsSolicitRestrictedToTimeInterval = this.cbSolicitAnytime.Checked;
                    //if (this.cbSolicitAnytime.Checked)
                    //{
                    //    proc.SolicitStartTime = this.solBegHour.SelectedValue + ":" + this.solBegMin.SelectedValue;
                    //    proc.SolicitEndTime = this.solEndHour.SelectedValue + ":" + this.solEndMin.SelectedValue;
                    //}
                    //proc.HasSolicitSubmitCredentials = this.cbSubmit.Checked;
                    //if (this.cbSubmit.Checked)
                    //{
                    //    proc.SolicitSubmitUID = this.txtSubmitUserID.Text.Trim();
                    //    if (!this.txtSubmitPassword.Text.Trim().Equals(""))
                    //        proc.SolicitSubmitPWD = this.txtSubmitPassword.Text;
                    //    proc.SolicitSubmitDataFlow = this.txtDataFlow.Text.Trim();
                    //}
                    op.Process = proc;
                    op.RemoveParameters();
                    DataTable solicitParams = (DataTable)this.Session["SOLICIT_PARAMS"];
                    foreach (DataRow dr in solicitParams.Rows)
                    {
                        OpParameter newOpPara = new OpParameter(dr["PARAM_NAME"].ToString());
                        newOpPara.DEDLType = "" + dr["DEDLRequiredIndicator"];
                        newOpPara.DEDLEncoding = ""+dr["DEDLEncoding"];
                        newOpPara.DEDLOccurenceNumber = ""+ dr["DEDLOccurenceNumber"];
                        newOpPara.DEDLRequiredIndicator =""+ dr["DEDLType"];
                        newOpPara.DEDLTypeDescriptor = "" + dr["DEDLTypeDescriptor"];
                        op.AddParameter(newOpPara);
                    }
                }
                else
                {
                    if (this.cbDefault.Checked)
                    {
                        string className = null;
                        string dllName = Phrase.DEFAULT_DLL;
                        switch (this.ddlWebService.SelectedItem.Text)
                        {
                            case Phrase.WEB_SERVICE_AUTHENTICATE:
                                className = Phrase.DEFAULT_AUTHENTICATE;
                                break;
                            case Phrase.WEB_SERVICE_DOWNLOAD:
                                className = Phrase.DEFAULT_DOWNLOAD;
                                break;
                            case Phrase.WEB_SERVICE_GETSERVICES:
                                className = Phrase.DEFAULT_GETSERVICES;
                                break;
                            case Phrase.WEB_SERVICE_GETSTATUS:
                                className = Phrase.DEFAULT_GETSTATUS;
                                break;
                            case Phrase.WEB_SERVICE_NODEPING:
                                className = Phrase.DEFAULT_NODEPING;
                                break;
                            case Phrase.WEB_SERVICE_NOTIFY:
                                className = Phrase.DEFAULT_NOTIFY;
                                break;
                            case Phrase.WEB_SERVICE_SUBMIT:
                                className = Phrase.DEFAULT_SUBMIT;
                                break;
                        }
                        op.Process = new OpProcess(ProcessType.PROCESS, className, dllName, this.ddlWebService.SelectedItem.Text);
                    }
                    else
                        op.Process = new OpProcess(ProcessType.PROCESS, this.txtProcessClassName.Text, this.txtProcessDllPath.Text, this.ddlWebService.SelectedItem.Text);
                    op.RemoveParameters();
                }
                op.RemovePostProcesses();
                DataTable postProcs = (DataTable)this.Session["POST_PROCESS_TABLE"];
                foreach (DataRow dr in postProcs.Rows)
                    op.AddPostProcess(new OpProcess(ProcessType.POST_PROCESS, dr["CLASS_NAME"].ToString(), dr["DLL_NAME"].ToString(), this.ddlWebService.SelectedItem.Text, int.Parse(dr["SEQUENCE"].ToString())));
            }
            else
            {
                op.Process = new OpProcess(ProcessType.PROCESS, this.txtTaskClassName.Text, this.txtTaskDllPath.Text);
                op.RemoveParameters();
                DataTable parameters = (DataTable)this.Session["TASK_PARAMETERS"];
                foreach (DataRow dr in parameters.Rows)
                    op.AddParameter(new OpParameter(dr["PARAM_NAME"].ToString(), dr["PARAM_VALUE"].ToString()));
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
                op.TaskSchedule = schedule;
                op.RemoveEmailReceivers();
                foreach (ListItem item in this.lbEmailReceiverList.Items)
                    op.AddEmailReceiver(item.Value);
            }

            op.PublishInd = (chkPublish.Checked ? "Y" : "N");
            op.RESTInd = (chkREST.Checked ? "Y" : "N");

            op.Save(this.LoggedInUser);
            this.lblError.Text = "Saved Successfully";
            this.lblError.Visible = true;

            if (pnlDenyPolicy.Visible )
            {
                string sResult = "";
                PolicyManager pmgr = new PolicyManager();
                bool bPolicyExisted = pmgr.GetExplicitRightFromNAAS(op, out sResult);
                if (!bPolicyExisted && sResult.Equals("")  && chkDenyPolicy.Checked)
                {
                    bool bSetPolicy = pmgr.SetExplicitRightFromNAAS(op, out sResult);
                    
                    if (!bSetPolicy)
                    {
                        lblError.Text = sResult;
                        lblError.Visible = true;
                    }
                }
                else if (bPolicyExisted && sResult.Equals("") && !chkDenyPolicy.Checked)
                {
                    pmgr.RemovePolicy("any", op.WebServiceName, op.Name);
                }
                else if (!bPolicyExisted && !sResult.Equals(""))
                {
                    lblError.Text = sResult;
                    lblError.Visible = true;
                }
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
