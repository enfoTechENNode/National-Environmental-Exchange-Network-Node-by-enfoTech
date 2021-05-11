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

public partial class NewOperation_aspx : Node.Core.UI.Base.AdminPageBase
{
    public NewOperation_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.PageControlsInit();
        
        if ("" + ConfigurationManager.AppSettings["DataWizard"] != "" && ConfigurationManager.AppSettings["DataWizard"].ToString().Equals("True"))
        {
            this.secDataWizard.Visible = true;
        }
        else
        {
            this.secDataWizard.Visible = false;
        }
    }

    #region Event Handlers

    protected void btnCancel_Click(object sender, EventArgs e)
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
        this.Response.Redirect("~/Pages/Operation/SearchOperations.aspx", false);
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (this.Session["OPERATION_NAME"] != null)
            this.Session.Remove("OPERATION_NAME");
        this.Session.Add("OPERATION_NAME", this.txtOperationName.Text.Trim());

        if (this.Session["OPERATION_STATUS_CD"] != null)
            this.Session.Remove("OPERATION_STATUS_CD");
        this.Session.Add("OPERATION_STATUS_CD", this.ddlStatus.SelectedValue);

        if (this.Session["OPERATION_STATUS_MSG"] != null)
            this.Session.Remove("OPERATION_STATUS_MSG");
        this.Session.Add("OPERATION_STATUS_MSG", this.txtStatusMessage.Text);

        if (this.Session["OPERATION_DESCRIPTION"] != null)
            this.Session.Remove("OPERATION_DESCRIPTION");
        this.Session.Add("OPERATION_DESCRIPTION", this.txtDescription.Text);

        if (this.Session["OPERATION_TYPE"] != null)
            this.Session.Remove("OPERATION_TYPE");
        this.Session.Add("OPERATION_TYPE", this.ddlOpType.SelectedValue);
         
        if (!chkDataFlowWizard.Checked)
        {
            if (this.ddlOpType.SelectedValue.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
                this.Response.Redirect("~/Pages/Operation/NewWSOperation.aspx", false);
            else
                this.Response.Redirect("~/Pages/Operation/NewTaskOperation.aspx", false);
        }
        else
        {
            if (this.ddlOpType.SelectedValue.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE))
                this.Response.Redirect("~/Pages/Operation/NewWSOperationForWizard.aspx", false);
            else
                this.Response.Redirect("~/Pages/Operation/NewTaskOperationForWizard.aspx", false);
        }
    }

    #endregion

    #region Initialization

    private void PageControlsInit()
    {
        if (this.Session[Phrase.VERSION_NO] != null)
            this.lblVersion.Text = this.Session[Phrase.VERSION_NO].ToString();
        if (this.Session["OPERATION_NAME"] != null)
            this.txtOperationName.Text = this.Session["OPERATION_NAME"].ToString();
        if (this.Session["OPERATION_STATUS_CD"] != null)
            this.ddlStatus.SelectedValue = this.Session["OPERATION_STATUS_CD"].ToString();
        if (this.Session["OPERATION_STATUS_MSG"] != null)
            this.txtStatusMessage.Text = this.Session["OPERATION_STATUS_MSG"].ToString();
        if (this.Session["OPERATION_DESCRIPTION"] != null)
            this.txtDescription.Text = this.Session["OPERATION_DESCRIPTION"].ToString();
        if (this.Session["OPERATION_TYPE"] != null)
            this.ddlOpType.SelectedValue = this.Session["OPERATION_TYPE"].ToString();
    }

    #endregion
}
