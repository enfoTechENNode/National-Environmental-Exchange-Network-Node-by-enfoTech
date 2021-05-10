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

public partial class PageControls_WebParts_Notify : Node.Core.UI.Base.AdminUserControlBase
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
        this.grvNotify.PageSize = defaultPageSize;
        this.imgTop5Notify.ToolTip = "Top " + defaultTopNo + " Notifications";

        this.grvNotify.RowCommand += new GridViewCommandEventHandler(grvNotify_RowCommand);
        if (!this.IsPostBack)
        {
            ViewState["NOTIFY"] = null;
            this.PageControlsInit();
        }

        imgSearchNodify.Attributes.Add("onclick", "fnSetFocus('" + ddlOpName.ClientID + "');");
    }

    protected void ShowNotifyInfo_Click(object sender, EventArgs e)
    {
        this.mpeNotifyDetail.Show();
        LinkButton btnInfo = (LinkButton)sender;
        if (btnInfo != null)
        {
            SetNotifyInfo(btnInfo.CommandArgument);
        }
    }

    void grvNotify_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            this.mpeNotifyDetail.Show();
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowNo = this.grvNotify.PageSize * this.grvNotify.PageIndex + Convert.ToInt32(e.CommandArgument);
                if (ViewState["NOTIFY"] != null)
                {
                    this.SetNotifyInfo(((DataTable)ViewState["NOTIFY"]).Rows[rowNo]["OPERATION_LOG_ID"].ToString());
                }
                else if (this.grvNotify.CachedDataTable != null)
                {
                    this.SetNotifyInfo(this.grvNotify.CachedDataTable.Rows[rowNo]["OPERATION_LOG_ID"].ToString());
                }
            }
        }

        Page.SetFocus(this.btnCloseNotifyInfo);
    }

    protected void imgTop5Notify_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;
            this.grvNotify.Visible = false;

            DataSet ds = OperationLog.SearchNotifications("", "", -1, "", "", "", DateTime.Parse("01/01/2000"), DateTime.Now.AddMonths(6), this.LoggedInUser);
            if (ds != null && ds.Tables.Contains(Phrase.TBL_OPERATION_LOG))
            {
                DataTable dt = ds.Tables[Phrase.TBL_OPERATION_LOG];
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
                    this.dataLstNotify.Visible = true;
                    this.dataLstNotify.DataSource = newDT;
                    this.dataLstNotify.DataBind();
                    TotalNotifications.Text = newDT.Rows.Count.ToString();
                }
            }
            if (ds != null && ds.Tables.Contains(Phrase.TBL_OPERATION_LOG_PARAMETER))
            {
                ViewState["NOTIFYDETAIL"] = ds.Tables[Phrase.TBL_OPERATION_LOG_PARAMETER];
            }
            ViewState["NOTIFY"] = null;
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

    protected void btnSearchNotify_Click(object sender, EventArgs e)
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

            DataSet ds = OperationLog.SearchNotifications(ddlOpName.SelectedValue, ddlStatus.SelectedValue, int.Parse(ddlDomainName.SelectedValue), txtUserName.Text.Trim(), txtToken.Text.Trim(), txtTransID.Text.Trim(), start, end, this.LoggedInUser);
            if (ds != null && ds.Tables.Contains(Phrase.TBL_OPERATION_LOG))
            {
                DataTable dt = ds.Tables[Phrase.TBL_OPERATION_LOG];
                if (dt != null && dt.Rows.Count > defaultPageSize)
                {
                    this.dataLstNotify.Visible = false;
                    this.grvNotify.Visible = true;
                    this.grvNotify.CachedDataTable = dt;
                    this.grvNotify.DataBind();
                    ViewState["NOTIFY"] = dt;
                }
                else
                {
                    this.dataLstNotify.Visible = true;
                    this.grvNotify.Visible = false;
                    this.dataLstNotify.DataSource = dt;
                    this.dataLstNotify.DataBind();
                    ViewState["NOTIFY"] = null;
                }
                this.TotalNotifications.Text = dt.Rows.Count.ToString();
            }
            if (ds != null && ds.Tables.Contains(Phrase.TBL_OPERATION_LOG_PARAMETER))
            {
                ViewState["NOTIFYDETAIL"] = ds.Tables[Phrase.TBL_OPERATION_LOG_PARAMETER];
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

    private void SetNotifyInfo(string opLogID)
    {
        if (string.IsNullOrEmpty(opLogID)) return;

        if (ViewState["NOTIFYDETAIL"] != null)
        {
            DataTable dt = (DataTable)ViewState["NOTIFYDETAIL"];
            DataRow[] rows = dt.Select("OPERATION_LOG_ID" + " = " + opLogID);
            if (rows != null && rows.Length > 0)
            {
                DataTable newDetail = new DataTable();
                newDetail.Columns.Add(Phrase.NP_DATA_FLOW);
                newDetail.Columns.Add(Phrase.NP_MESSAGE_CATEGORY);
                newDetail.Columns.Add(Phrase.NP_MESSAGE_NAME);
                newDetail.Columns.Add(Phrase.NP_MESSAGE_STATUS);
                newDetail.Columns.Add(Phrase.NP_OBJECT_ID);
                newDetail.Columns.Add(Phrase.NP_MESSAGE_DETAIL);

                DataRow newRow = null;
                int j = 0;
                for (int i = 0; i < rows.Length; i++)
                {
                    if (j == 0 )
                        newRow = newDetail.NewRow();
                    else if (j > 6)
                    {
                        j = 0;
                        newDetail.Rows.Add(newRow);
                        newRow = newDetail.NewRow();
                    }
                    switch (rows[i]["PARAMETER_NAME"].ToString().Trim())
                    {
                        case Phrase.NP_DATA_FLOW:
                            newRow[Phrase.NP_DATA_FLOW] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        case Phrase.NP_MESSAGE_CATEGORY:
                            newRow[Phrase.NP_MESSAGE_CATEGORY] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        case Phrase.NP_MESSAGE_NAME:
                            newRow[Phrase.NP_MESSAGE_NAME] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        case Phrase.NP_MESSAGE_STATUS:
                            newRow[Phrase.NP_MESSAGE_STATUS] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        case Phrase.NP_OBJECT_ID:
                            newRow[Phrase.NP_OBJECT_ID] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        case Phrase.NP_MESSAGE_DETAIL:
                            newRow[Phrase.NP_MESSAGE_DETAIL] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                            break;
                        //case "Parameters":
                        //    newRow[Phrase.NP_DATA_FLOW] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    newRow[Phrase.NP_MESSAGE_CATEGORY] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    newRow[Phrase.NP_MESSAGE_NAME] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    break;
                        //case "Request":
                        //    newRow[Phrase.NP_MESSAGE_STATUS] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    newRow[Phrase.NP_OBJECT_ID] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    newRow[Phrase.NP_MESSAGE_DETAIL] = rows[i]["PARAMETER_VALUE"].ToString().Trim();
                        //    break;
                    }
                    j++;
                }
                newDetail.Rows.Add(newRow);
                this.lstNotifyDetail.DataSource = newDetail;
                this.lstNotifyDetail.DataBind();
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

            this.imgTop5Notify_Click(null, null);
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

