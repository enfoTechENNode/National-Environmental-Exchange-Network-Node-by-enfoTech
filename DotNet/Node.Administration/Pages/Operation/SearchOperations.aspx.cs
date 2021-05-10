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

using System.Text;
using System.Xml;
using System.Xml.Linq;

using Node.Core;
using Node.Core.Data;
using Node.Core.Biz.Objects;
using DataFlow.Component.Interface;
using DataFlow.Engine;


using System.Collections.Generic;


public partial class SearchOperations_aspx : Node.Core.UI.Base.AdminPageBase
{
    private string domainName = null; 

    public SearchOperations_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["DOMAIN_NAME"] != null)
            this.domainName = this.Session["DOMAIN_NAME"].ToString();
        else
            this.domainName = "N/A";

        if (!this.IsPostBack)
            this.PageControlsInit();
    }

    #region Event Handlers

    protected void btnBackToDashBoard_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.egvOperationGrid.CachedDataTable = Operation.SearchOperations(this.domainName, int.Parse(this.ddlOperationName.SelectedValue), this.ddlOpType.SelectedValue, int.Parse(this.ddlWebService.SelectedValue), this.ddlStatus.SelectedValue);
            this.egvOperationGrid.DataBind();
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

    protected void btnCreate_Click(object sender, EventArgs e)
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
        this.Response.Redirect("~/Pages/Operation/NewOperation.aspx", false);
    }

    protected void egvOperationGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditOperation")
            {
                this.lblError.Text = "";
                this.lblError.Visible = false;

                string opID = e.CommandArgument.ToString();
                Operation op = new Operation(int.Parse(opID));
                if (op != null)
                {
                    if (op.Config.DocumentElement.Name.ToUpper() == "PROCESS")
                        Response.Redirect("~/Pages/Operation/ViewOperationWizard.aspx?OPID=" + opID);
                    else
                        Response.Redirect("~/Pages/Operation/ViewOperation.aspx?OPID=" + opID);
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

    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        string xmlstring = new UTF8Encoding().GetString(fuUpload.FileBytes);
        XElement root = XElement.Parse(xmlstring);
        IEnumerator ie = root.Descendants("operation").GetEnumerator();
        ie.MoveNext();
        XElement operation = ie.Current as XElement;

        if (operation.Attribute("name") != null && operation.Attribute("type") != null)
        {
            if (operation.Attribute("type").Value == OperationType.WEB_SERVICE.ToString()
                && Enum.IsDefined(typeof(OperationWebMethod), operation.Attribute("webservice").Value))
            {
                SaveOperationWS(root);
            }
            else if (operation.Attribute("type").Value == OperationType.SCHEDULED_TASK.ToString()
                && Enum.IsDefined(typeof(OperationWebMethod), operation.Attribute("webservice").Value))
            {
                SaveOperationTask(root);
            }
        }
    }

    private void SaveOperationWS(XElement OpConfig)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            IEnumerator ie = OpConfig.Descendants("operation").GetEnumerator();
            ie.MoveNext();
            XElement operation = ie.Current as XElement;

            Operation newOp = new Operation(operation.Attribute("name").Value,operation.Attribute("webservice").Value);

            if (newOp.ID < 0)
            {
                newOp.DomainName = this.Session["DOMAIN_NAME"].ToString();
                operation.Attribute("domain").Value = this.Session["DOMAIN_NAME"].ToString();
                newOp.Status = Phrase.STATUS_RUNNING;
                newOp.Message = "";
                newOp.Description = "";
                newOp.Type = Phrase.OPERATION_TYPE_WEB_SERVICE;
                newOp.Version = this.Session[Phrase.VERSION_NO].ToString();
                operation.Attribute("version").Value = this.Session[Phrase.VERSION_NO].ToString();
                newOp.RemovePreProcesses();
                newOp.RemovePostProcesses();
                newOp.Process = new OpProcess(ProcessType.PROCESS, "CLASS_NAME", "DLL_NAME", operation.Attribute("webservice").Value);
                newOp.PublishInd = "Y";

                newOp.RemoveParameters();

                string opMessage = newOp.Save(this.LoggedInUser);

                if (opMessage != null && opMessage.Trim() != "")
                {
                    this.lblError.Text = opMessage + "<br/>&nbsp;";
                    this.lblError.Visible = true;
                }
                else
                {
                    operation.Attribute("id").Value = newOp.ID + "";
                    DBManager dbMgr = new DBManager();
                    if (dbMgr.GetOperationsDB().UpdateOperationConfig("" + newOp.ID, OpConfig.ToString()))
                    {
                        this.Response.Redirect("~/Pages/Operation/ViewOperationWizard.aspx?OPID=" + newOp.ID);
                    }
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
    private void SaveOperationTask(XElement OpConfig)
    {
        this.lblError.Visible = false;
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            IEnumerator ie = OpConfig.Descendants("operation").GetEnumerator();
            ie.MoveNext();
            XElement operation = ie.Current as XElement;

            Operation newOp = new Operation(operation.Attribute("name").Value);
            if (newOp.ID < 0)
            {
                newOp.Type = Phrase.OPERATION_TYPE_SCHEDULED_TASK;
                newOp.DomainName = this.Session["DOMAIN_NAME"].ToString();
                operation.Attribute("domain").Value = this.Session["DOMAIN_NAME"].ToString();
                newOp.Status = Phrase.STATUS_RUNNING; ;
                newOp.Message = "";
                newOp.Description = "";
                newOp.Process = new OpProcess(ProcessType.PROCESS, "", "");
                newOp.RemoveParameters();
                newOp.PublishInd = "Y";
                DataTable parameters = (DataTable)this.Session["TASK_PARAMETERS"];

                TaskSchedule schedule = new TaskSchedule("I", DateTime.MinValue, DateTime.MaxValue, TaskSchedule.SCHEDULE_TYPE_MINUTES, 1, -1, -1, null, null, -1, null);

                newOp.TaskSchedule = schedule;

                string opMessage = newOp.Save(this.LoggedInUser);

                if (opMessage != null && opMessage.Trim() != "")
                {
                    this.lblError.Text = opMessage + "<br/>&nbsp;";
                    this.lblError.Visible = true;
                }
                else
                {
                    operation.Attribute("id").Value = newOp.ID + "";
                    DBManager dbMgr = new DBManager();
                    if (dbMgr.GetOperationsDB().UpdateOperationConfig("" + newOp.ID, OpConfig.ToString()))
                    {
                        this.Response.Redirect("~/Pages/Operation/ViewOperationWizard.aspx?OPID=" + newOp.ID);
                    }
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
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
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

            this.ddlOperationName.DataSource = Operation.GetOperationsList(this.domainName);
            this.ddlOperationName.DataValueField = "OPERATION_ID";
            this.ddlOperationName.DataTextField = "OPERATION_NAME";
            this.ddlOperationName.DataBind();

            this.ddlWebService.DataSource = WebService.GetWebServicesList();
            this.ddlWebService.DataValueField = "WEB_SERVICE_ID";
            this.ddlWebService.DataTextField = "WEB_SERVICE_NAME";
            this.ddlWebService.DataBind();

            this.btnSearch_Click(null, null);
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
