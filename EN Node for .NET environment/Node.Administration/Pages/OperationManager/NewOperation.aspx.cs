using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using Node.Lib.Utility;
using Node.Lib.Utility.Zip;
using Node.Core.Biz.Objects;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Lib.AppSystem;
using Node.Core.Data.SQLServer;

public partial class Pages_OperationManager_NewOperation : Node.Core.UI.Base.AdminPageBase
{
    protected ManageOperation config = null;
    private string domainName = string.Empty;
    private string opID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.PageDescription = "This page allows you to add an operation";

        if (this.Session["DOMAIN_NAME"] != null)
            this.domainName = this.Session["DOMAIN_NAME"].ToString();
        else
            this.domainName = "N/A";

        if (!IsPostBack)
        {
            this.InitControls();
            // this.GenerateTemplateGV();
        }
    }

    #region Private Methods

    private void InitControls()
    {
        string versionNo = string.Empty;

        if (System.Web.HttpContext.Current.Session[Phrase.VERSION_NO] != null)
        {
            versionNo = System.Web.HttpContext.Current.Session[Phrase.VERSION_NO].ToString().ToUpper();
        }
        else
        {
            versionNo = Phrase.VERSION_11;
        }

        DataTable dt = Operation.GetOperationsByUserForOperationMgr(this.LoggedInUser, versionNo);
        this.config = new ManageOperation();
        DataTable opcondt = this.config.GetConfigOperations(versionNo);

        foreach (DataRow aRow in opcondt.Rows)
        {
            
            if (dt.Select("OPERATION_ID=" + aRow["ID"] ).Length > 0)
            {
                dt.Rows.Remove(dt.Select("OPERATION_ID=" + aRow["ID"])[0]);     
            }
        }
        
        DataRow dr = dt.NewRow();
        dr["OPERATION_ID"] = -1;
        dr["OPERATION_NAME"] = "";
        dt.Rows.InsertAt(dr, 0);

        this.ddlAddOperationName.DataSource = dt;

        this.ddlAddOperationName.DataValueField = "OPERATION_ID";
        this.ddlAddOperationName.DataTextField = "OPERATION_NAME";
        this.ddlAddOperationName.DataBind();

    }
    private bool SaveOperationXML()
    {
        bool bFlag = false;

        string strOPID = lblAddOperationIDValue.Text;
        string strOPName = ddlAddOperationName.SelectedItem.Text;
        string strDataFlow = txtDataFlow.Text;

        string version = (string)Session[Phrase.VERSION_NO];

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

        //if (this.chkAddEnableGenerate.Checked && this.chkAddEnableUpload.Checked)
        //{
        //    this.msg.MsgContent = "Operation can only be linked with either upload or generate!!";
        //    return bFlag;
        //}


        ManageOperation.OpMgrData objOpMgrData = new ManageOperation.OpMgrData();
        DBManager dbmgr = new DBManager();
        Operation op = dbmgr.GetOperationsDB().GetOperation(int.Parse(strOPID));

        bool bWS = (op.Type == Phrase.OPERATION_TYPE_WEB_SERVICE ? true : false);

        objOpMgrData.opID = strOPID;
        objOpMgrData.opName = strOPName;
        objOpMgrData.version = version;
        objOpMgrData.upload = bolUpload & bWS;
        objOpMgrData.generate = bolGenerate & bWS;
        objOpMgrData.submit = bolSubmit & bWS;
        objOpMgrData.view = true;
        objOpMgrData.GetStatus = bolGetStatus;

        if (objOpMgrData.submit || objOpMgrData.GetStatus)
        {
            objOpMgrData.submitURL = strSubURL;
            objOpMgrData.submitUsername = strSubUsername;
            objOpMgrData.submitPassword = strSubPassword;
            objOpMgrData.submitDomainName = txtDomainName.Text;
            objOpMgrData.dataFlow = strDataFlow;
            objOpMgrData.dataFlowOperation = txtFlowOp.Text;
        }

        objOpMgrData.GetStatusComplete = txtGetStatusComplete.Text;
        objOpMgrData.GetStatusError = txtGetStatusError.Text;
        
        string strAdd = this.config.AddOperation(objOpMgrData);

        bFlag =this.config.SaveOperationManager();

        opID = strOPID;

        return bFlag;
    }

    #endregion Private Methods

    #region Event Handlers

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

    protected void ddlAddOperationName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAddOperationName.SelectedValue.Trim().Length > 0)
        {
            lblAddOperationIDValue.Text = ddlAddOperationName.SelectedValue;
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
        this.config = new ManageOperation();
        if (ddlAddOperationName.SelectedValue != "-1")
        {
            if (SaveOperationXML())
                Response.Redirect("~/Pages/OperationManager/OperationConfig.aspx?opID=" + opID);
        }
        else
        {
            this.msg.MsgContent = "Please Select One of Operation before Save";
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        this.config = new ManageOperation();
        if (ddlAddOperationName.SelectedValue != "-1")
        {
            if (SaveOperationXML())
            {
                string fileName = txtStyleFileName.Text;
                string fileType = Phrase.CONFIG_TYPE_XSLT;
                byte[] inputFile = this.fuStyleUpload.FileBytes;

                int fileID = this.config.AddUploadFile(fileName, fileType, inputFile);
                this.config = new ManageOperation();
                this.config.AddStyleSheet(opID, fileID.ToString(), fileName);
                this.config.SaveOperationManager();
            }
            Response.Redirect("~/Pages/OperationManager/EditOperation.aspx?opID=" + opID);
        }
        else
        {
            this.msg.MsgContent = "Please Select One of Operation before Upload Style Sheet";
        }
    }
    protected void btnUpload2_Click(object sender, EventArgs e)
    {
        this.config = new ManageOperation();
        if (ddlAddOperationName.SelectedValue != "-1")
        {
            if (SaveOperationXML())
            {
                string fileName = txtValidationName.Text;
                string fileType = Phrase.CONFIG_TYPE_RULE;
                byte[] inputFile = this.FileUploadValidation.FileBytes;

                int fileID = this.config.AddUploadFile(fileName, fileType, inputFile);
                this.config = new ManageOperation();
                this.config.AddValidationRule(opID, fileID.ToString(), fileName);
                this.config.SaveOperationManager();
            }
            Response.Redirect("~/Pages/OperationManager/EditOperation.aspx?opID=" + opID);
        }
        else
        {
            this.msg.MsgContent = "Please Select One of Operation before Upload Validation File";
        }
    }
    protected void btnAddParameter_Click(object sender, EventArgs e)
    {
        this.config = new ManageOperation();
        if (ddlAddOperationName.SelectedValue != "-1")
        {
            if (SaveOperationXML())
            {
                string paraName = txtParameterName.Text;
                string paraValue = txtXPath.Text;

                this.config = new ManageOperation();
                this.config.AddParameters(opID, paraName ,paraValue);
                this.config.SaveOperationManager();
            }
            Response.Redirect("~/Pages/OperationManager/OperationConfig.aspx?opID=" + opID);
        }
        else
        {
            this.msg.MsgContent = "Please Select One of Operation before Upload Validation File";
        }
    }


    protected void btnAddOppBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/OperationManager/OperationConfig.aspx");
    }

    #endregion Event Handlers


}
