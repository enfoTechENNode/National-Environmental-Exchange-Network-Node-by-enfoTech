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

using Node.Core;
using Node.Core.Biz.Objects;

public partial class PageControls_WebParts_Task : Node.Core.UI.Base.AdminUserControlBase
{
    #region Private Variables
    private string loggedUser = string.Empty;
    private int defaultTopNo = 5;
    private int defaultPageSize = 10;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Phrase.DEFAULT_TOPNUM] != null && !int.TryParse(Session[Phrase.DEFAULT_TOPNUM].ToString(), out defaultTopNo))
        {
            defaultTopNo = 5;
        }
        if (Session[Phrase.DEFAULT_PAGESIZE] != null && !int.TryParse(Session[Phrase.DEFAULT_PAGESIZE].ToString(), out defaultPageSize))
        {
            defaultPageSize = 10;
        }
        this.grvTask.PageSize = defaultPageSize;
        this.imgTop5Task.ToolTip = "Top " + defaultTopNo + " Scheduled Tasks";

        this.grvTask.RowCommand += new GridViewCommandEventHandler(grvTask_RowCommand);
        if (!this.IsPostBack)
        {
            ViewState["TASK"] = null;
            this.PageControlsInit();
        }

        imgSearchTask.Attributes.Add("onclick", "fnSetFocus('" + ddlOpName.ClientID + "');");
    }

    protected void ShowTaskInfo_Click(object sender, EventArgs e)
    {
        this.mpeTaskDetail.Show();
        LinkButton btnInfo = (LinkButton)sender;
        if (btnInfo != null)
        {
            SetTaskInfo(btnInfo.CommandArgument);
        }
        Page.SetFocus(btnCloseTaskInfo);
    }

    void grvTask_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            this.mpeTaskDetail.Show();
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowNo = this.grvTask.PageSize * this.grvTask.PageIndex + Convert.ToInt32(e.CommandArgument);
                if (ViewState["TASK"] != null)
                {
                    this.SetTaskInfo(((DataTable)ViewState["TASK"]).Rows[rowNo]["LOG_AND_STATUS_IDS"].ToString());
                }
                else if (this.grvTask.CachedDataTable != null)
                {
                    this.SetTaskInfo(this.grvTask.CachedDataTable.Rows[rowNo]["LOG_AND_STATUS_IDS"].ToString());
                }
            }
        }
    }

    protected void imgTop5Task_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;
            this.grvTask.Visible = false;

            DataTable dt = OperationLog.SearchTasks("", "", -1, "", "", "", DateTime.Parse("01/01/2000"), DateTime.Now, this.LoggedInUser);
            DataTable newDT = dt.Clone();
            //DataRow newRow = null;
            if (dt != null)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i < defaultTopNo)
                    {
                        newDT.ImportRow(dr);
                        i++;
                    }
                    else
                        break;
                }
                this.dataLstTask.Visible = true;
                this.dataLstTask.DataSource = newDT;
                this.dataLstTask.DataBind();
                TotalTasks.Text = newDT.Rows.Count.ToString();
            }
            ViewState["TASK"] = null;
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

    protected void btnSearchTask_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;
            
            DateTime start = new DateTime();
            DateTime end = new DateTime();
            if (!DateTime.TryParse(this.dtStart.Text.Trim(), out start))
                start = DateTime.Parse("01/01/2000");
            if (!DateTime.TryParse(this.dtEnd.Text.Trim(), out end))
                end = DateTime.Now.AddMonths(6);

            DataTable dt = OperationLog.SearchTasks(ddlOpName.SelectedValue, ddlStatus.SelectedValue, int.Parse(ddlDomainName.SelectedValue), txtUserName.Text.Trim(), txtToken.Text.Trim(), txtTransID.Text.Trim(), start, end, this.LoggedInUser);
            if (dt != null && dt.Rows.Count > defaultPageSize)
            {
                this.dataLstTask.Visible = false;
                this.grvTask.Visible = true;
                this.grvTask.CachedDataTable = dt;
                this.grvTask.DataBind();
                ViewState["TASK"] = dt;
            }
            else
            {
                this.dataLstTask.Visible = true;
                this.grvTask.Visible = false;
                this.dataLstTask.DataSource = dt;
                this.dataLstTask.DataBind();
                ViewState["TASK"] = null;
            }
            this.TotalTasks.Text = dt.Rows.Count.ToString();
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

    private void SetTaskInfo(string statusIDAndLogID)
    {
        if (string.IsNullOrEmpty(statusIDAndLogID) || statusIDAndLogID == "_") return;

        string[] ids = statusIDAndLogID.Split('_');
        OperationLog logObj = new OperationLog(int.Parse(ids[1]));
        if (logObj != null)
        {
            this.lblOpName.Text = logObj.OperationName;
            this.lblDomainName.Text = logObj.DomainName;

            ArrayList statuses = logObj.Statuses;
            if (statuses.Count > 0)
            {
                //string output = "";
                foreach (object obj in statuses)
                {
                    OperationLogStatus status = (OperationLogStatus)obj;
                    if (status.OperationLogStatusID.ToString() == ids[0])
                    {
                        this.lblStatus.Text = status.Status;
                        this.lblDateFinished.Text = status.CreatedDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                        this.lblMessage.Text = status.Message;
                    }
                }
            }
        }
    }
    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.ddlDomainName.DataSource = Domain.GetDomainsDropDownList(this.LoggedInUser);
            this.ddlDomainName.DataValueField = "DOMAIN_ID";
            this.ddlDomainName.DataTextField = "DOMAIN_NAME";
            this.ddlDomainName.DataBind();

            this.ddlOpName.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            this.ddlOpName.DataBind();

            this.ddlStatus.DataSource = OperationLog.GetUniqueOperationStatuses(this.LoggedInUser);
            this.ddlStatus.DataBind(); 
            
            this.dtStart.Text = DateTime.Now.AddDays(-7.0).ToShortDateString();
            this.dtEnd.Text = DateTime.Now.ToShortDateString();

            this.imgTop5Task_Click(null, null);
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
}
