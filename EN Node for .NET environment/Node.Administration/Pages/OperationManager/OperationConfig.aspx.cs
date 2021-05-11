using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Node.Core.Biz.Objects;
using Node.Core;
using System.Collections;
using Node.Core.Data;


public partial class Pages_OperationManager_OperationConfig : Node.Core.UI.Base.AdminPageBase
{
    protected ManageOperation objManageOP = null;
    private string opID
    {
        get { return (string)ViewState["opID"]; }
        set { ViewState["opID"] = value;}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.egvOperationGrid.RowEditing += new GridViewEditEventHandler(this.egvOperationGrid_RowEditing);

        this.Master.PageDescription = "This page allows you to edit an operation";
        if (!IsPostBack)
        {
            ViewState.Add("opID", "");
            this.GenerateOperationGV();
            if (Request.QueryString["opID"] != null)
            {
                this.opID = Request.QueryString["opID"].ToString();
                this.InitControls();
            }
            else
            {
                ResetView();
            }
        }
    }

    private void GenerateOperationGV()
    {
        string version = (string)Session[Phrase.VERSION_NO];
        this.objManageOP = new ManageOperation();
        DataTable dtOPConfig = objManageOP.GetConfigOperations(version);

        this.egvOperationGrid.CachedDataTable = dtOPConfig;
        this.egvOperationGrid.DataBind();
    }
    protected void egvOperationGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.opID = this.egvOperationGrid.GetCurrentDataViewData(e, "ID").ToString();
        InitControls();
    }

    #region Private Methods

    private void ResetView()
    {
        this.FormSectionBlockOperation.Visible = false;
        this.FormSectionBlockAddUpload.Visible = false;
        this.FormSectionBlockAddSubmit.Visible = false;
        this.FormSectionBlockViewAddViewStyle.Visible = false;
        this.FormSectionBlockValidation.Visible = false;
        this.btnSave.Visible = false;
        this.btnDelete.Visible = false;
    }

    private void InitControls()
    {
        objManageOP = new ManageOperation();
        ManageOperation.OpMgrData objOpMgrData = null;

        string opName = string.Empty;

        objOpMgrData = objManageOP.GetOperation(opID);
        lblAddOperationIDValue.Text = objOpMgrData.opID;
        lblOPNameValue.Text = objOpMgrData.opName;
        txtDataFlow.Text = objOpMgrData.dataFlow;

        this.FormSectionBlockOperation.Visible = Visible;

        if (objOpMgrData.upload)
        {
            this.rdoOperation.SelectedIndex = 0;
            this.FormSectionBlockAddUpload.Visible = true;
        }
        else if (objOpMgrData.generate)
        {
            this.rdoOperation.SelectedIndex = 1;
        }
        else
        {
            this.rdoOperation.SelectedIndex = -1;
        }

        chkAddEnableSubmit.Checked = objOpMgrData.submit;

        txtAddSubmitURL.Text = objOpMgrData.submitURL;
        txtAddSubmitUsername.Text = objOpMgrData.submitUsername;
        txtAddSubmitPassword.Text = objOpMgrData.submitPassword;
        txtDomainName.Text = objOpMgrData.submitDomainName;
        txtFlowOp.Text = objOpMgrData.dataFlowOperation;

        if (chkAddEnableSubmit.Checked == true)
        {
            FormSectionBlockAddSubmit.Visible = true;
        }
        else
        {
            FormSectionBlockAddSubmit.Visible = false;
        }

        GenerateTemplateGV();
        GenerateValidationRuleGV();
        GenerateParametersGV();

        this.FormSectionBlockViewAddViewStyle.Visible = true;
        this.FormSectionBlockValidation.Visible = true;
        this.btnSave.Visible = true;
        this.btnDelete.Visible = true;


        if (objOpMgrData.GetStatus)
        {
            chkEnableGetStatus.Checked = true;
            FormSectionBlockGetStatus.Visible = true;
            txtGetStatusComplete.Text = objOpMgrData.GetStatusComplete;
            txtGetStatusError.Text = objOpMgrData.GetStatusError;
            FormSectionBlockAddSubmit.Visible = true;
        }
        else
        {
            chkEnableGetStatus.Checked = false;
            FormSectionBlockGetStatus.Visible = false;
            txtGetStatusComplete.Text = "";
            txtGetStatusError.Text = "";
        }

        if (objOpMgrData.opType == Phrase.OPERATION_TYPE_SCHEDULED_TASK)
        {
            rdoOperation.Enabled = false;
            chkAddEnableSubmit.Enabled = false;
            FormSectionBlockAddUpload.Visible = false;
            //FormSectionBlockAddSubmit.Visible = false;
        }
    }

    private void GenerateTemplateGV()
    {
        ArrayList arrTemID = new ArrayList();
        ArrayList arrTemName = new ArrayList();

        this.objManageOP = new ManageOperation();
        opID = lblAddOperationIDValue.Text;

        Hashtable ht = this.objManageOP.GetStyleSheetByID(opID);

        if (ht != null)
        {

            arrTemID = (ArrayList)ht["ID"];
            arrTemName = (ArrayList)ht["NAME"];

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("Template ID", typeof(System.String));
            dt.Columns.Add("Template Name", typeof(System.String));


            for (int i = 0; i < arrTemID.Count; i++)
            {
                dt.Rows.Add(new object[] { arrTemID[i], arrTemName[i] });
            }
            ds.Tables.Add(dt);

            //   DataTable dtConfig = this.objManageOP.GetConfigNames("XSLT");
            if (ds != null)
            {
                this.grvViewStyleSheets.CachedDataTable = ds.Tables[0];
                this.grvViewStyleSheets.DataBind();
                grvViewStyleSheets.Visible = true;
                grvViewStyleSheets.PageSize = 5;
            }
            btnViewDelete.Visible = true;
        }
        else
        {
            grvViewStyleSheets.Visible = false;
            btnViewDelete.Visible = false;
        }
    }

    private void GenerateValidationRuleGV()
    {
        ArrayList arrTemID = new ArrayList();
        ArrayList arrTemName = new ArrayList();

        this.objManageOP = new ManageOperation();
        opID = lblAddOperationIDValue.Text;

        Hashtable ht = this.objManageOP.GetValidationRuleByID(opID);

        if (ht != null)
        {

            arrTemID = (ArrayList)ht["ID"];
            arrTemName = (ArrayList)ht["NAME"];

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt.Columns.Add("Template ID", typeof(System.String));
            dt.Columns.Add("Template Name", typeof(System.String));


            for (int i = 0; i < arrTemID.Count; i++)
            {
                dt.Rows.Add(new object[] { arrTemID[i], arrTemName[i] });
            }
            ds.Tables.Add(dt);

            //   DataTable dtConfig = this.objManageOP.GetConfigNames("XSLT");
            if (ds != null)
            {
                this.grvViewValidationRule.CachedDataTable = ds.Tables[0];
                this.grvViewValidationRule.DataBind();
                grvViewValidationRule.Visible = true;
                grvViewValidationRule.PageSize = 5;
            }
            btnValidationDelete.Visible = true;
        }
        else
        {
            grvViewValidationRule.Visible = false;
            btnValidationDelete.Visible = false;
        }
    }

    private void GenerateParametersGV()
    {
        this.objManageOP = new ManageOperation();
        opID = lblAddOperationIDValue.Text;

        DataTable dt = this.objManageOP.GetParametersByID(opID);
        this.egvParameter.CachedDataTable = dt;
        this.egvParameter.DataBind();
        this.egvParameter.Visible = true;
        this.egvParameter.PageSize = 5;
        if (dt != null && dt.Rows.Count == 0)
        {
            btnDeleteParameter.Visible = false;
        }
        else
        {
            btnDeleteParameter.Visible = true;
        }
    }

    private Hashtable GetAllTemplates()
    {
        ArrayList arTemID = new ArrayList();
        ArrayList arTemName = new ArrayList();
        Hashtable haTemplates = new Hashtable();

        foreach (GridViewRow gr in grvViewStyleSheets.Rows)
        {
            string strTemID = gr.Cells[1].Text.Trim();
            arTemID.Add(strTemID);

            string strTemName = gr.Cells[2].Text.Trim();
            arTemName.Add(strTemName);
        }

        haTemplates.Add("ID", arTemID);
        haTemplates.Add("Name", arTemName);

        return haTemplates;
    }
    private Hashtable GetAllValidationRule()
    {
        ArrayList arTemID = new ArrayList();
        ArrayList arTemName = new ArrayList();
        Hashtable haTemplates = new Hashtable();

        foreach (GridViewRow gr in grvViewValidationRule.Rows)
        {
            string strTemID = gr.Cells[1].Text.Trim();
            arTemID.Add(strTemID);

            string strTemName = gr.Cells[2].Text.Trim();
            arTemName.Add(strTemName);
        }

        haTemplates.Add("ID", arTemID);
        haTemplates.Add("Name", arTemName);

        return haTemplates;
    }
    private DataTable GetAllParameter()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("id", typeof(string)));
        dt.Columns.Add(new DataColumn("ParameterName", typeof(string)));
        dt.Columns.Add(new DataColumn("XPath", typeof(string)));

        foreach (GridViewRow gr in egvParameter.Rows)
        {
            DataRow newRow = dt.NewRow();
            newRow["id"] = gr.Cells[1].Text.Trim();
            newRow["ParameterName"] = gr.Cells[2].Text.Trim();
            newRow["XPath"] = gr.Cells[3].Text.Trim();
            dt.Rows.Add(newRow);
        }

        return dt;
    }

    private ArrayList GetSelectedTemplateID()
    {

        ArrayList arrTemID = new ArrayList();
        RadioButton radSelect;
        int radIndex = 0;
        //Loop through each row in the GridView
        foreach (GridViewRow row in grvViewStyleSheets.Rows)
        {
            //Get the index of the current CheckBox
            radIndex = row.RowIndex;
            radSelect = (RadioButton)row.FindControl("radSelect");

            //Now see if the current Radio is checked
            if (radSelect != null)
            {
                if (radSelect.Checked)
                {
                    arrTemID.Add(grvViewStyleSheets.Rows[radIndex].Cells[1].Text);
                }
            }

        }
        return arrTemID;
    }
    private ArrayList GetSelectedValidationRuleID()
    {

        ArrayList arrTemID = new ArrayList();
        RadioButton radSelect;
        int radIndex = 0;
        //Loop through each row in the GridView
        foreach (GridViewRow row in grvViewValidationRule.Rows)
        {
            //Get the index of the current CheckBox
            radIndex = row.RowIndex;
            radSelect = (RadioButton)row.FindControl("radSelectValidationRule");

            //Now see if the current Radio is checked
            if (radSelect != null)
            {
                if (radSelect.Checked)
                {
                    arrTemID.Add(grvViewValidationRule.Rows[radIndex].Cells[1].Text);
                }
            }

        }
        return arrTemID;
    }
    private ArrayList GetSelectedParametersID()
    {

        ArrayList arrTemID = new ArrayList();
        RadioButton radSelect;
        int radIndex = 0;
        //Loop through each row in the GridView
        foreach (GridViewRow row in egvParameter.Rows)
        {
            //Get the index of the current CheckBox
            radIndex = row.RowIndex;
            radSelect = (RadioButton)row.FindControl("radSelectParameter");

            //Now see if the current Radio is checked
            if (radSelect != null)
            {
                if (radSelect.Checked)
                {
                    arrTemID.Add(egvParameter.Rows[radIndex].Cells[1].Text);
                }
            }

        }
        return arrTemID;
    }


    #endregion

    #region Event Handelers

    protected void rdoOperation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoOperation.SelectedIndex == 0)
        {
            this.FormSectionBlockAddUpload.Visible = true;
        }
        else
        {
            this.FormSectionBlockAddUpload.Visible = false;
        }
    }
    protected void chkAddEnableSubmit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAddEnableSubmit.Checked == true)
        {
            FormSectionBlockAddSubmit.Visible = true;
        }
        else
        {
            FormSectionBlockAddSubmit.Visible = false;
        }
    }
    protected void chkEnableGetStatus_CheckedChanged(object sender, EventArgs e)
    {
        if (chkEnableGetStatus.Checked == true)
        {
            FormSectionBlockGetStatus.Visible = true;
            FormSectionBlockAddSubmit.Visible = true;
        }
        else
        {
            FormSectionBlockGetStatus.Visible = false;
            if (chkAddEnableSubmit.Checked != true)
            {
                FormSectionBlockAddSubmit.Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.objManageOP = new ManageOperation();
        if (lblOPNameValue.Text.Trim().Length > 0)
        {
            string strOPID = lblAddOperationIDValue.Text;
            string strOPName = lblOPNameValue.Text;
            string strDataFlow = txtDataFlow.Text;

            bool bolUpload = false;
            bool bolGenerate = false;

            if (rdoOperation.SelectedIndex == 0)
            {
                bolUpload = true;
            }
            else if (rdoOperation.SelectedIndex == 1)
            {
                bolGenerate = true;
            }

            bool bolSubmit = false;

            if (chkAddEnableSubmit.Checked == true)
            {
                bolSubmit = true;
            }
            string strSubURL = txtAddSubmitURL.Text;
            string strSubUsername = txtAddSubmitUsername.Text;
            string strSubPassword = txtAddSubmitPassword.Text;

            bool bolGetStatus = false;
            if (chkEnableGetStatus.Checked)
            {
                bolGetStatus = true;
            }

            //Hashtable haTemplates = this.GetAllTemplates();
            //Hashtable haValidation = this.GetAllValidationRule();
            //DataTable paraTable = this.GetAllParameter();

            //if (this.chkAddEnableGenerate.Checked && this.chkAddEnableUpload.Checked)
            //{
            //    this.msg.MsgContent = "Operation can only be linked with either upload or generate!!";
            //    return;
            //}

            ManageOperation.OpMgrData objOpMgrData = this.objManageOP.GetOperation(this.opID);
            bool bWS = (objOpMgrData.opType == Phrase.OPERATION_TYPE_WEB_SERVICE ? true : false);
            objOpMgrData.opID = strOPID;
            objOpMgrData.opName = strOPName;
            objOpMgrData.upload = bolUpload & bWS;
            objOpMgrData.generate = bolGenerate & bWS;
            objOpMgrData.submit = bolSubmit & bWS;
            objOpMgrData.view = true;
            objOpMgrData.submitURL = strSubURL;
            objOpMgrData.submitUsername = strSubUsername;
            objOpMgrData.submitPassword = strSubPassword;
            objOpMgrData.submitDomainName = txtDomainName.Text;
            objOpMgrData.dataFlow = strDataFlow;
            objOpMgrData.dataFlowOperation = txtFlowOp.Text;
            objOpMgrData.GetStatus = bolGetStatus;
            objOpMgrData.GetStatusComplete = txtGetStatusComplete.Text;
            objOpMgrData.GetStatusError = txtGetStatusError.Text;

            string strEdit = this.objManageOP.EditOperation(objOpMgrData);

            if (this.objManageOP.SaveOperationManager())
            {
                this.msg.MsgContent = "Save Successful";
            }
            GenerateOperationGV();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        opID = lblAddOperationIDValue.Text;
        string fileName = txtStyleFileName.Text;
        string fileType = Phrase.CONFIG_TYPE_XSLT;
        byte[] inputFile = this.fuStyleUpload.FileBytes;

        this.objManageOP = new ManageOperation();
        int fileID = this.objManageOP.AddUploadFile(fileName, fileType, inputFile);
        this.objManageOP = new ManageOperation();
        this.objManageOP.AddStyleSheet(opID, fileID.ToString(), fileName);
        this.objManageOP.SaveOperationManager();

        this.GenerateTemplateGV();
    }
    protected void btnUpload2_Click(object sender, EventArgs e)
    {
        opID = lblAddOperationIDValue.Text;
        string fileName = txtValidationName.Text;
        string fileType = Phrase.CONFIG_TYPE_RULE;
        byte[] inputFile = this.FileUploadValidation.FileBytes;

        this.objManageOP = new ManageOperation();
        int fileID = this.objManageOP.AddUploadFile(fileName, fileType, inputFile);
        this.objManageOP = new ManageOperation();
        this.objManageOP.AddValidationRule(opID, fileID.ToString(), fileName);
        this.objManageOP.SaveOperationManager();

        this.GenerateValidationRuleGV();
    }
    protected void btnAddParameter_Click(object sender, EventArgs e)
    {
        this.objManageOP = new ManageOperation();

        string paraName = txtParameterName.Text;
        string paraValue = txtXPath.Text;

        this.objManageOP = new ManageOperation();
        this.objManageOP.AddParameters(opID, paraName, paraValue);
        this.objManageOP.SaveOperationManager();

        this.GenerateParametersGV();
    }

    protected void btnAddOperation_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/OperationManager/NewOperation.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        this.objManageOP = new ManageOperation();
        opID = (string)ViewState["opID"];
        if (opID.Trim().Length > 0)
        {
            try
            {
                ArrayList arrID = null;
                Hashtable ht = this.objManageOP.GetStyleSheetByID(opID);
                if (ht != null)
                {
                    arrID = (ArrayList)ht["ID"];
                    for (int i = 0; i < arrID.Count; i++)
                    {
                        ManageOperation.DeleteConfig((string)arrID[i]);
                    }
                }
                ht = this.objManageOP.GetValidationRuleByID(opID);
                if (ht != null)
                {
                    arrID = (ArrayList)ht["ID"];
                    for (int i = 0; i < arrID.Count; i++)
                    {
                        ManageOperation.DeleteConfig((string)arrID[i]);
                    }
                }
                objManageOP.DeleteOperationByID(opID);
                this.objManageOP.SaveOperationManager();
                Response.Redirect("~/Pages/OperationManager/OperationConfig.aspx");
            }
            catch
            {
            }
        }
    }
    protected void btnViewDelete_Click(object sender, EventArgs e)
    {
        ArrayList arrTemID = this.GetSelectedTemplateID();
        this.objManageOP = new ManageOperation();
        this.opID = lblAddOperationIDValue.Text;

        if (arrTemID.Count > 0)
        {
            string temID = arrTemID[0].ToString();
            ManageOperation.DeleteConfig(temID);

            if (this.objManageOP.DeleteStyleSheet(opID, temID))
            {
                this.objManageOP.SaveOperationManager();
            }

            this.GenerateTemplateGV();
        }
    }
    protected void btnValidationDelete_Click(object sender, EventArgs e)
    {
        ArrayList arrTemID = this.GetSelectedValidationRuleID();
        this.objManageOP = new ManageOperation();
        this.opID = lblAddOperationIDValue.Text;

        if (arrTemID.Count > 0)
        {
            string temID = arrTemID[0].ToString();
            ManageOperation.DeleteConfig(temID);

            if (this.objManageOP.DeleteValidationRule(opID, temID))
            {
                this.objManageOP.SaveOperationManager();
            }

            this.GenerateValidationRuleGV();
        }
    }
    protected void btnDeleteParameter_Click(object sender, EventArgs e)
    {
        ArrayList arrTemID = this.GetSelectedParametersID();
        this.objManageOP = new ManageOperation();
        this.opID = lblAddOperationIDValue.Text;

        if (arrTemID.Count > 0)
        {
            string temID = arrTemID[0].ToString();

            if (this.objManageOP.DeleteParameters(opID, temID))
            {
                this.objManageOP.SaveOperationManager();
            }

            this.GenerateParametersGV();
        }
    }
    protected void btnAddOppBack_Click(object sender, EventArgs e)
    {
        Session["OpenForm"] = "OperationMgr";
        Response.Redirect("~/Pages/Main/Home.aspx");
    }
    #endregion

}
