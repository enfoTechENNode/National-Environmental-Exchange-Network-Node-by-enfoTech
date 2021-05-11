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

using Node.Core.Biz.Objects;
using Node.Core;

public partial class PageControls_WebParts_SearchLog : Node.Core.UI.Base.AdminUserControlBase
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
        this.grvLog.PageSize = defaultPageSize;
        this.lkbTop5List.ToolTip = "Top " + defaultTopNo + " Transaction Logs";

        this.grvLog.RowCommand += new GridViewCommandEventHandler(grvLog_RowCommand);
        if (!this.IsPostBack)
        {
            this.Initialize();
        }
        lnkBtnSearch.Attributes.Add("onclick", "fnSetFocus('" + ddlOpName.ClientID + "');");
    }
    #region Event Handlers

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DateTime start;
            if (!string.IsNullOrEmpty(this.dtStart.Text))
            {
                if (!DateTime.TryParse((this.dtStart.Text), out start))
                {
                    start = DateTime.Parse("01/01/2000");
                }
            }
            else
            {
                start = DateTime.Parse("01/01/2000");
            }
            DateTime end;
            if (!string.IsNullOrEmpty(this.dtEnd.Text))
            {
                if (!DateTime.TryParse((this.dtEnd.Text), out end))
                {
                    end = DateTime.Now.AddMonths(6);
                }
            }
            else
            {
                end = DateTime.Now;
            }
            DataTable dt = OperationLog.SearchLogs(this.ddlOpName.SelectedValue, this.ddlOpType.SelectedValue, int.Parse(this.ddlWebService.SelectedValue), this.ddlStatus.SelectedValue, int.Parse(this.ddlDomain.SelectedValue), this.txtUserName.Text, this.txtToken.Text, this.txtTransID.Text, start, end, this.LoggedInUser);
            if (dt != null && dt.Rows.Count > defaultPageSize)
            {
                ViewState["LOG"] = dt;
                this.grvLog.CachedDataTable = dt;
                this.grvLog.DataBind();
                this.dataLstLog.Visible = false;
                this.grvLog.Visible = true;
            }
            else
            {
                this.dataLstLog.DataSource = dt;
                this.dataLstLog.DataBind();
                this.dataLstLog.Visible = true;
                this.grvLog.Visible = false;
            }
            this.TotalLog.Text = dt.Rows.Count.ToString();
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

    protected void lkbTop5List_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            DataTable dt = OperationLog.SearchLogs("", "", -1, "", -1, "", "", "", DateTime.MinValue, DateTime.Now, this.LoggedInUser);
            DataTable newDT = dt.Clone();
            //DataRow newRow = null;
            if (dt != null && dt.Rows.Count > 0)
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
                this.dataLstLog.DataSource = newDT;
                this.dataLstLog.DataBind();
                this.dataLstLog.Visible = true;
                this.grvLog.Visible = false;
                this.TotalLog.Text = newDT.Rows.Count.ToString();
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

    # region Protected Methods

    protected string GetOperationId(string operationId)
    {
        return string.Format("Operation ID {0}", Eval(operationId).ToString());
    }

    # endregion

    #region Private Methods
    protected void grvLog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            this.mdlPopup.Show();
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowNo = this.grvLog.PageSize * this.grvLog.PageIndex + Convert.ToInt32(e.CommandArgument);
                if (ViewState["LOG"] != null)
                {
                    this.ShowDetail(((DataTable)ViewState["LOG"]).Rows[rowNo]["OPERATION_LOG_ID"].ToString());
                }
                else if (this.grvLog.CachedDataTable != null)
                {
                    this.ShowDetail(this.grvLog.CachedDataTable.Rows[rowNo]["OPERATION_LOG_ID"].ToString());
                }
            }
        }
    }
    protected void ShowLogInfo_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;
            this.mdlPopup.Show();
            LinkButton btnShowLogInfo = (LinkButton)sender;
            if (btnShowLogInfo != null)
            {
                this.ShowDetail(btnShowLogInfo.CommandArgument);
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
        Page.SetFocus(btnCloseDetail);
    }
    private void Initialize()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            //this.InitializeEvents();

            this.ddlOpName.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            this.ddlOpName.DataBind();

            this.ddlWebService.DataSource = OperationLog.GetWebServiceIDNamePairs();
            this.ddlWebService.DataTextField = "WEB_SERVICE_NAME";
            this.ddlWebService.DataValueField = "WEB_SERVICE_ID";
            this.ddlWebService.DataBind();

            this.ddlStatus.DataSource = OperationLog.GetUniqueOperationStatuses(this.LoggedInUser);
            this.ddlStatus.DataBind();

            this.ddlDomain.DataSource = Domain.GetDomainsDropDownList(this.LoggedInUser);
            this.ddlDomain.DataTextField = "DOMAIN_NAME";
            this.ddlDomain.DataValueField = "DOMAIN_ID";
            this.ddlDomain.DataBind();

            this.dtStart.Text = DateTime.Now.AddDays(-7.0).ToShortDateString();
            this.dtEnd.Text = DateTime.Now.ToShortDateString();

            this.lkbTop5List_Click(null, null);
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
    private void ShowDetail(string opLogID)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            OperationLog opLog = new OperationLog(Convert.ToInt32(opLogID));
            this.mlOperationName.Text = opLog.OperationName;
            this.mlDomainName.Text = opLog.DomainName;
            this.mlTransactionID.Text = opLog.TransactionID;
            this.mlHostName.Text = opLog.HostName;
            this.mlStartDate.Text = opLog.StartDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            if (opLog.EndDate.CompareTo(DateTime.MaxValue) != 0)
            {
                this.lEndDate.Visible = true;
                this.mlEndDate.Visible = true;
                this.mlEndDate.Text = opLog.EndDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            }
            else
            {
                this.lEndDate.Visible = false;
                this.lEndDate.Visible = false;
            }
            this.mlOperationType.Text = opLog.OperationType;
            if (opLog.OperationType.Equals(Phrase.OPERATION_TYPE_SCHEDULED_TASK))
            {
                this.lWebServiceName.Visible = false;
                this.mlWebServiceName.Visible = false;
            }
            else
            {
                this.lWebServiceName.Visible = true;
                this.mlWebServiceName.Visible = true;
                this.mlWebServiceName.Text = opLog.WebServiceName;
                if (opLog.Token != null && !opLog.Token.Trim().Equals(""))
                {
                    string output = "<tr><td class=\"lftfld\">Security Token:</td>";
                    output += "<td class=\"lftfld\" colspan=\"3\">";
                    output += "<div class=\"eaf_MsgLbl\" style=\"color:#000099\">" + opLog.Token;
                    output += "</div></td></tr>";
                    this.lToken.Visible = true;
                    this.lToken.Text = output;
                }
                else
                    this.lToken.Visible = false;
                this.mlRequestorIP.Text = opLog.RequestorIP;
                if (opLog.UserName != null)
                {
                    this.lUserName.Visible = true;
                    this.mlUserName.Visible = true;
                    this.mlUserName.Text = opLog.UserName;
                }
                else
                {
                    this.lUserName.Visible = false;
                    this.mlUserName.Visible = false;
                }
                if (opLog.SuppliedTransactionID != null)
                {
                    string output = "<tr><td class=\"lftfld\">Supplied Transaction ID:</td>";
                    output += "<td class=\"lftfld\" colspan=\"3\">";
                    output += "<div class=\"eaf_MsgLbl\" style=\"color:#000099\">" + opLog.SuppliedTransactionID;
                    output += "</div></td></tr>";
                    this.lSuppliedTransactionID.Visible = true;
                    this.lSuppliedTransactionID.Text = output;
                }
                else
                    this.lSuppliedTransactionID.Visible = false;
                if (opLog.ServiceType != null)
                {
                    string output = "<tr><td class=\"lftfld\">Service Type:</td>";
                    output += "<td class=\"lftfld\" colspan=\"3\">";
                    output += "<div class=\"eaf_MsgLbl\" style=\"color:#000099\">" + opLog.ServiceType;
                    output += "</div></td></tr>";
                    this.lServiceType.Visible = true;
                    this.lServiceType.Text = output;
                }
                else
                    this.lServiceType.Visible = false;
                if (opLog.NodeAddress != null || opLog.ReturnURL != null)
                {
                    string output = "";
                    if (opLog.NodeAddress != null)
                    {
                        output += "<tr><td class=\"lftfld\">Node Address:</td>";
                        output += "<td class=\"lftfld\" colspan=\"3\">";
                        output += "<div class=\"eaf_MsgLbl\" style=\"color:#000099\">" + opLog.NodeAddress;
                        output += "</div></td></tr>";
                    }
                    if (opLog.ReturnURL != null)
                    {
                        output += "<tr><td class=\"lftfld\">Return URL:</td>";
                        output += "<td class=\"lftfld\" colspan=\"3\">";
                        output += "<div class=\"eaf_MsgLbl\" style=\"color:#000099\">" + opLog.ReturnURL;
                        output += "</div></td></tr>";
                    }
                    this.lURLs.Visible = true;
                    this.lURLs.Text = output;
                }
                else
                    this.lURLs.Visible = false;
            }

            // Parameters
            ArrayList parameters = opLog.Parameters;
            if (parameters.Count > 0)
            {
                string output = "";
                foreach (object obj in parameters)
                {
                    OperationLogParameter param = (OperationLogParameter)obj;
                    output += "<table class=\"cc_EntryForm\" cellspacing=\"0\">";
                    output += "<tr><td class=\"lftfld\">" + param.ParameterName + ":</td>";
                    output += "<td class=\"lftfld\"><div class=\"eaf_MsgLbl\" style=\"color:#000099\">";
                    output += param.ParameterValue + "</div></td></tr>";
                    output += "</table>";
                }
                this.lParameters.Text = output;
                this.lParameters.Visible = true;
            }
            else
                this.lParameters.Visible = false;

            // Statuses
            ArrayList statuses = opLog.Statuses;
            if (statuses.Count > 0)
            {
                string output = "";
                foreach (object obj in statuses)
                {
                    OperationLogStatus status = (OperationLogStatus)obj;
                    output += "<table class=\"cc_EntryForm\" cellspacing=\"0\">";
                    output += "<tr><td class=\"lftfld\">" + status.CreatedDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                    output += "</td><td class=\"lftfld\"><div class=\"eaf_MsgLbl\" style=\"color:#000099\">";
                    output += status.Status + "</div></td><td class=\"lftfld\">" + Server.HtmlEncode(status.Message) + "</td></tr>";
                    output += "</table>";
                }
                this.lStatuses.Text = output;
                this.lStatuses.Visible = true;
            }
            else
                this.lStatuses.Visible = false;
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
}
