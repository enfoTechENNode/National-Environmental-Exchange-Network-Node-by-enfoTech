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

public partial class NewWSOperation_aspx : Node.Core.UI.Base.AdminPageBase
{
    public NewWSOperation_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

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

    protected void btnAddPreProcess_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnRemovePreProcesses_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnAddPostProcess_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnRemovePostProcesses_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void ddlWebService_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.Session["WEB_SERVICE_ID"] != null)
            this.Session.Remove("WEB_SERVICE_ID");
        if (this.Session["WEB_SERVICE_NAME"] != null)
            this.Session.Remove("WEB_SERVICE_NAME");
        this.Session.Add("WEB_SERVICE_ID", int.Parse(this.ddlWebService.SelectedValue));
        this.Session.Add("WEB_SERVICE_NAME", this.ddlWebService.SelectedItem.Text);
        switch (this.ddlWebService.SelectedItem.Text)
        {
            case Phrase.WEB_SERVICE_QUERY:
                this.mvProcess.SetActiveView(this.vQuery);
                break;
            case Phrase.WEB_SERVICE_SOLICIT:
                this.mvProcess.SetActiveView(this.vSolicit);
                break;
            default:
                this.mvProcess.SetActiveView(this.vGeneric);
                break;
        }
    }

    protected void btnAddQueryParameter_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnRemoveQueryParameters_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnAddSolicitParameter_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
        }
    }

    protected void btnRemoveSolicitParameters_Click(object sender, EventArgs e)
    {
        try
        {
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
            throw ex;
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

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DataTable preProcessesTable = new DataTable();
            if (this.Session["PRE_PROCESS_TABLE"] != null)
                preProcessesTable = (DataTable)this.Session["PRE_PROCESS_TABLE"];
            else
                preProcessesTable.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("DLL_NAME"), new DataColumn("CLASS_NAME") });
            this.egvPreProcesses.CachedDataTable = preProcessesTable;
            this.egvPreProcesses.DataBind();
            if (this.Session["PRE_PROCESS_TABLE"] == null)
                this.Session.Add("PRE_PROCESS_TABLE", preProcessesTable);

            this.ddlWebService.DataSource = WebService.GetWebServicesListVer11(this.Session["DOMAIN_NAME"].ToString());            
            this.ddlWebService.DataValueField = "WEB_SERVICE_ID";
            this.ddlWebService.DataTextField = "WEB_SERVICE_NAME";
            this.ddlWebService.DataBind();

            string wsName = null;
            if (this.Session["WEB_SERVICE_NAME"] != null)
                wsName = this.Session["WEB_SERVICE_NAME"].ToString();
            else if (this.ddlWebService.Items.Count > 0)
                wsName = this.ddlWebService.Items[0].Text;
            if (wsName != null)
            {
                switch (wsName)
                {
                    case Phrase.WEB_SERVICE_QUERY:
                        this.mvProcess.SetActiveView(this.vQuery);
                        break;
                    case Phrase.WEB_SERVICE_SOLICIT:
                        this.mvProcess.SetActiveView(this.vSolicit);
                        break;
                    default:
                        this.mvProcess.SetActiveView(this.vGeneric);
                        break;
                }
                if (this.Session["WEB_SERVICE_ID"] != null)
                    this.ddlWebService.SelectedValue = this.Session["WEB_SERVICE_ID"].ToString();
            }

            DataTable queryParametersTable = new DataTable();
            if (this.Session["QUERY_PARAMS"] != null)
                queryParametersTable = (DataTable)this.Session["QUERY_PARAMS"];
            else
            {
                queryParametersTable.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), 
                    new DataColumn("DEDLEncoding"), new DataColumn("DEDLOccurenceNumber"),
                    new DataColumn("DEDLType"), new DataColumn("DEDLRequiredIndicator"), new DataColumn("DEDLTypeDescriptor")});
                this.Session.Add("QUERY_PARAMS", queryParametersTable);
            }
            this.egvQueryParameters.CachedDataTable = queryParametersTable;
            this.egvQueryParameters.DataBind();

            DataTable solicitParametersTable = new DataTable();
            if (this.Session["SOLICIT_PARAMS"] != null)
                solicitParametersTable = (DataTable)this.Session["SOLICIT_PARAMS"];
            else
            {
                solicitParametersTable.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("SEQUENCE"), new DataColumn("PARAM_NAME"), 
                    new DataColumn("DEDLEncoding"), new DataColumn("DEDLOccurenceNumber"),
                    new DataColumn("DEDLType"), new DataColumn("DEDLRequiredIndicator"), new DataColumn("DEDLTypeDescriptor")});
                this.Session.Add("SOLICIT_PARAMS", solicitParametersTable);
            }
            this.egvSolicitParameters.CachedDataTable = solicitParametersTable;
            this.egvSolicitParameters.DataBind();

            DataTable postProcessesTable = new DataTable();
            if (this.Session["POST_PROCESS_TABLE"] != null)
                postProcessesTable = (DataTable)this.Session["POST_PROCESS_TABLE"];
            else
                postProcessesTable.Columns.AddRange(new DataColumn[] { new DataColumn("SEQUENCE"), new DataColumn("DLL_NAME"), new DataColumn("CLASS_NAME") });
            this.egvPostProcesses.CachedDataTable = postProcessesTable;
            this.egvPostProcesses.DataBind();
            if (this.Session["POST_PROCESS_TABLE"] == null)
                this.Session.Add("POST_PROCESS_TABLE", postProcessesTable);
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
        string wsName = null;
        if (this.ddlWebService.SelectedIndex >= 0)
            wsName = this.ddlWebService.SelectedItem.Text;
        else if (this.ddlWebService.Items.Count > 0)
            wsName = this.ddlWebService.Items[0].Text;
        if (wsName != null)
        {
            if (this.Session["WEB_SERVICE_ID"] != null)
                this.Session.Remove("WEB_SERVICE_ID");
            if (this.Session["WEB_SERVICE_NAME"] != null)
                this.Session.Remove("WEB_SERVICE_NAME");
            if (this.ddlWebService.SelectedIndex >= 0)
                this.Session.Add("WEB_SERVICE_ID", int.Parse(this.ddlWebService.SelectedValue));
            else
                this.Session.Add("WEB_SERVICE_ID", int.Parse(this.ddlWebService.Items[0].Value));
            this.Session.Add("WEB_SERVICE_NAME", wsName);
            switch (wsName)
            {
                case Phrase.WEB_SERVICE_QUERY:
                    this.SaveQueryView();
                    break;
                case Phrase.WEB_SERVICE_SOLICIT:
                    this.SaveSolicitView();
                    break;
                default:
                    this.SaveGenericView();
                    break;
            }
        }
    }

    private void SaveGenericView()
    {
        string dll = null;
        string className = null;
        if (this.cbDefault.Checked)
        {
            dll = Phrase.DEFAULT_DLL;
            switch (this.Session["WEB_SERVICE_NAME"].ToString())
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
        }
        else
        {
            if (!this.txtProcessDllPath.Text.Trim().Equals(""))
                dll = this.txtProcessDllPath.Text;
            if (!this.txtProcessClassName.Text.Trim().Equals(""))
                className = this.txtProcessClassName.Text;
        }
        if (dll != null)
        {
            if (this.Session["DLL_NAME"] != null)
                this.Session.Remove("DLL_NAME");
            this.Session.Add("DLL_NAME", dll);
        }
        if (className != null)
        {
            if (this.Session["CLASS_NAME"] != null)
                this.Session.Remove("CLASS_NAME");
            this.Session.Add("CLASS_NAME", className);
        }
        else if (this.Session["CLASS_NAME"] != null)
            this.Session.Remove("CLASS_NAME");
    }

    private void SaveQueryView()
    {
        if (!this.txtQueryProcessDllPath.Text.Trim().Equals(""))
        {
            if (this.Session["DLL_NAME"] != null)
                this.Session.Remove("DLL_NAME");
            this.Session.Add("DLL_NAME", this.txtQueryProcessDllPath.Text);
        }
        else if (this.Session["DLL_NAME"] != null)
            this.Session.Remove("DLL_NAME");
        if (!this.txtQueryProcessClassName.Text.Trim().Equals(""))
        {
            if (this.Session["CLASS_NAME"] != null)
                this.Session.Remove("CLASS_NAME");
            this.Session.Add("CLASS_NAME", this.txtQueryProcessClassName.Text);
        }
        else if (this.Session["CLASS_NAME"] != null)
            this.Session.Remove("CLASS_NAME");
    }

    private void SaveSolicitView()
    {
        if (!this.txtSolicitProcessDllPath.Text.Trim().Equals(""))
        {
            if (this.Session["DLL_NAME"] != null)
                this.Session.Remove("DLL_NAME");
            this.Session.Add("DLL_NAME", this.txtSolicitProcessDllPath.Text);
        }
        else if (this.Session["DLL_NAME"] != null)
            this.Session.Remove("DLL_NAME");
        if (!this.txtSolicitProcessClassName.Text.Trim().Equals(""))
        {
            if (this.Session["CLASS_NAME"] != null)
                this.Session.Remove("CLASS_NAME");
            this.Session.Add("CLASS_NAME", this.txtSolicitProcessClassName.Text);
        }
        else if (this.Session["CLASS_NAME"] != null)
            this.Session.Remove("CLASS_NAME");

        //if (this.cbSolicitAnytime.Checked)
        //{
        //    if (this.Session["SOLICIT_BEGIN"] != null)
        //        this.Session.Remove("SOLICIT_BEGIN");
        //    this.Session.Add("SOLICIT_BEGIN", this.solBegHour.SelectedValue + ":" + this.solBegMin.SelectedValue);
        //    if (this.Session["SOLICIT_END"] != null)
        //        this.Session.Remove("SOLICIT_END");
        //    this.Session.Add("SOLICIT_END", this.solEndHour.SelectedValue + ":" + this.solEndMin.SelectedValue);
        //}
        //else
        //{
        //    if (this.Session["SOLICIT_BEGIN"] != null)
        //        this.Session.Remove("SOLICIT_BEGIN");
        //    if (this.Session["SOLICIT_END"] != null)
        //        this.Session.Remove("SOLICIT_END");
        //}
        //if (this.cbSubmit.Checked)
        //{
        //    if (this.Session["SOLICIT_USER"] != null)
        //        this.Session.Remove("SOLICIT_USER");
        //    if (this.Session["SOLICIT_PWD"] != null)
        //        this.Session.Remove("SOLICIT_PWD");
        //    if (!this.txtSubmitUserID.Text.Trim().Equals(""))
        //    {
        //        this.Session.Add("SOLICIT_USER", this.txtSubmitUserID.Text);
        //        if (!this.txtSubmitPassword.Text.Trim().Equals(""))
        //            this.Session.Add("SOLICIT_PWD", this.txtSubmitPassword.Text);
        //    }
        //    if (this.Session["SOLICIT_DATAFLOW"] != null)
        //        this.Session.Remove("SOLICIT_DATAFLOW");
        //    if (!this.txtDataFlow.Text.Trim().Equals(""))
        //        this.Session.Add("SOLICIT_DATAFLOW", this.txtDataFlow.Text);
        //}
        //else
        //{
        //    if (this.Session["SOLICIT_USER"] != null)
        //        this.Session.Remove("SOLICIT_USER");
        //    if (this.Session["SOLICIT_PWD"] != null)
        //        this.Session.Remove("SOLICIT_PWD");
        //    if (this.Session["SOLICIT_DATAFLOW"] != null)
        //        this.Session.Remove("SOLICIT_DATAFLOW");
        //}
    }

    private void SaveOperation()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            Operation newOp = new Operation(this.Session["OPERATION_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString());
            if (newOp.ID < 0)
            {
                string error = "";
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
                if (!error.Trim().Equals(""))
                {
                    this.lblError.Text = error;
                    this.lblError.Visible = true;
                    return;
                }
                newOp.DomainName = this.Session["DOMAIN_NAME"].ToString();
                newOp.Status = this.Session["OPERATION_STATUS_CD"].ToString();
                newOp.Message = this.Session["OPERATION_STATUS_MSG"].ToString();
                newOp.Description = this.Session["OPERATION_DESCRIPTION"].ToString();
                newOp.Type = Phrase.OPERATION_TYPE_WEB_SERVICE;
                newOp.RemovePreProcesses();
                DataTable preProcs = (DataTable)this.Session["PRE_PROCESS_TABLE"];
                foreach (DataRow dr in preProcs.Rows)
                    newOp.AddPreProcess(new OpProcess(ProcessType.PRE_PROCESS, dr["CLASS_NAME"].ToString(), dr["DLL_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString(), int.Parse(dr["SEQUENCE"].ToString())));
                if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_QUERY))
                {
                    newOp.Process = new OpProcess(ProcessType.PROCESS, this.Session["CLASS_NAME"].ToString(), this.Session["DLL_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString());
                    newOp.RemoveParameters();
                    DataTable queryParams = (DataTable)this.Session["QUERY_PARAMS"];
                    foreach (DataRow dr in queryParams.Rows)
                    {
                        OpParameter newOpPara = new OpParameter(dr["PARAM_NAME"].ToString());
                        newOpPara.DEDLType = "" + dr["DEDLRequiredIndicator"];
                        newOpPara.DEDLEncoding = ""+dr["DEDLEncoding"];
                        newOpPara.DEDLOccurenceNumber = ""+ dr["DEDLOccurenceNumber"];
                        newOpPara.DEDLRequiredIndicator =""+ dr["DEDLType"];
                        newOpPara.DEDLTypeDescriptor = "" + dr["DEDLTypeDescriptor"];
                        newOp.AddParameter(newOpPara);
                    }
                }
                else if (this.ddlWebService.SelectedItem.Text.Equals(Phrase.WEB_SERVICE_SOLICIT))
                {
                    OpProcess proc = new OpProcess(ProcessType.PROCESS, this.Session["CLASS_NAME"].ToString(), this.Session["DLL_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString());
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
                    newOp.Process = proc;
                    newOp.RemoveParameters();
                    DataTable solicitParams = (DataTable)this.Session["SOLICIT_PARAMS"];
                    foreach (DataRow dr in solicitParams.Rows)
                    {
                        OpParameter newOpPara = new OpParameter(dr["PARAM_NAME"].ToString());
                        newOpPara.DEDLType = "" + dr["DEDLRequiredIndicator"];
                        newOpPara.DEDLEncoding = "" + dr["DEDLEncoding"];
                        newOpPara.DEDLOccurenceNumber = "" + dr["DEDLOccurenceNumber"];
                        newOpPara.DEDLRequiredIndicator = "" + dr["DEDLType"];
                        newOpPara.DEDLTypeDescriptor = "" + dr["DEDLTypeDescriptor"];
                        newOp.AddParameter(newOpPara);
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
                        newOp.Process = new OpProcess(ProcessType.PROCESS, className, dllName, this.Session["WEB_SERVICE_NAME"].ToString());
                    }
                    else
                        newOp.Process = new OpProcess(ProcessType.PROCESS, this.Session["CLASS_NAME"].ToString(), this.Session["DLL_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString());
                    newOp.RemoveParameters();
                }
                newOp.RemovePostProcesses();
                DataTable postProcs = (DataTable)this.Session["POST_PROCESS_TABLE"];
                foreach (DataRow dr in postProcs.Rows)
                    newOp.AddPostProcess(new OpProcess(ProcessType.POST_PROCESS, dr["CLASS_NAME"].ToString(), dr["DLL_NAME"].ToString(), this.Session["WEB_SERVICE_NAME"].ToString(), int.Parse(dr["SEQUENCE"].ToString())));

                newOp.PublishInd = (chkPublish.Checked ? "Y" : "N");

                newOp.RESTInd = (chkREST.Checked ? "Y" : "N");
                
                string opMessage = newOp.Save(this.LoggedInUser);
                if (opMessage != null && opMessage.Trim() != "")
                {
                    this.lblError.Text = opMessage + "<br/>&nbsp;";
                    this.lblError.Visible = true;
                }
                else
                {
                    this.ClearSavedState();
                    this.Response.Redirect("~/Pages/Operation/ViewOperation.aspx?OPID=" + newOp.ID);
                }
            }
            else
            {
                this.lblError.Text = "An Operation by the Operation Name and Web Service has already been created on this Node.  Choose a Different Operation Name or Choose a Different Web Service if you want to save this operation.";
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
        if (this.Session["DLL_NAME"] != null)
            this.Session.Remove("DLL_NAME");
        if (this.Session["CLASS_NAME"] != null)
            this.Session.Remove("CLASS_NAME");
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

    #endregion
}
