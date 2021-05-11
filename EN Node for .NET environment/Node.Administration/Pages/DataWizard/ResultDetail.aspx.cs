using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using EAF.Domain.AppSystem;
using EAF.Lib.UI.WebControls;
using Node.Core;
using Node.Core.Biz.Objects;
using Node.ID.Kootenai.AQS;

public partial class ResultDetail : Node.Core.UI.Base.AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialControls();
            //please get protocol id while getting gridview data, it is for developer updating
            BindGridView();
        }
        else
        {
            GridViewDetail.PageIndex = 1;
        }
    }

    protected void initialControls()
    {
        // Initial the private parameters
        this.initialParameters();

        txtLocation.Text = this.stateCode + "-" + this.countyCode + "-" + this.aqsSiteNumber;
        txtParameterId.Text = this.parameterId;
        txtParameterName.Text = this.parameterName;

        //hide error type textbox if it is ready data detail
        if (this.errorMessage != null)
        {
            Errortype.Text = this.errorMessage;
            txterror.Visible = true;
        }
        else
        {
            txterror.Visible = false;
        }

    }
    
    protected void BindGridView()
    {
        AQSDatabase aDB = new AQSDatabase("node");
        string query = GenerateQuery();
        //DataTable dt = ExecuteQuery(query,string.Empty);
        DataTable dt = aDB.GetSingleTableByQuery("detailView", query);

        //testing data
        //DataRow dr = dt.NewRow();
        //dr["observation_date_time"] = "2008/1/1";
        //dr["sample_value"] = "0.12";
        //dr["measure_unit_name"] = "ppm";
        //dr["node_trans_id"] = "5";
        //dr["null_data_code"] = "1";
        //dr["action_code"] = "U";
        //dr["raw_data_hour_id"] = "5111";
        //dr["include_in_sub_ind"] = "N";
        //dt.Rows.Add(dr);

        //DataRow dr2 = dt.NewRow();
        //dr2["observation_date_time"] = "2008/1/1";
        //dr2["sample_value"] = "0.13";
        //dr2["measure_unit_name"] = "ppm";
        //dr2["node_trans_id"] = "6";
        //dr2["null_data_code"] = "2";
        //dr2["action_code"] = "I";
        //dr2["include_in_sub_ind"] = "Y";
        //dr2["raw_data_hour_id"] = "5111";
        //dt.Rows.Add(dr2);
        //

        this.GridViewDetail.DataSource = dt;
        this.GridViewDetail.DataBind();

        //set dropdown defalut selected value of each row
        for (int i = 0; i < this.GridViewDetail.Rows.Count; i++)
        {
            GridViewRow GVR = this.GridViewDetail.Rows[i];
            Label lblUnit = (Label)GVR.FindControl("lblUnit");
            // Display the special characte with beginning of '&' and ending of ';'
            lblUnit.Text = dt.Rows[i]["measure_unit_name"].ToString();
            Label lblPreSent = (Label)GVR.FindControl("lblPreSent");
            if (dt.Rows[i]["node_trans_id"] == null || string.IsNullOrEmpty(dt.Rows[i]["node_trans_id"].ToString()) 
                || dt.Rows[i]["node_trans_id"].ToString().ToUpper() == "NO")
            {
                lblPreSent.Text = "NO";
            }
            else
            {
                lblPreSent.Text = "YES";
            }

            DropDownList ddlacCode = (DropDownList)GVR.FindControl("ddlActionCode");
            ddlacCode.SelectedValue = dt.Rows[i]["action_code"].ToString();

            DropDownList ddlinc = (DropDownList)GVR.FindControl("ddlInclude");
            ddlinc.SelectedValue = dt.Rows[i]["include_in_sub_ind"].ToString();
        }
    }

    #region Event Handler

    //protected void GridViewDetail_PageChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridViewDetail.PageIndex = e.NewPageIndex + 1;
    //}

    protected void BtnSaveOnClick(object sender, EventArgs e)
    {
        string protocolID = string.Empty;
        string actionCode = string.Empty;
        string nullCode = string.Empty;
        string includeinSub = string.Empty;

        //get dropdownlist control value
        //update data row by row
        foreach (GridViewRow GVR in this.GridViewDetail.Rows)
        {
            protocolID = ((Label)GVR.FindControl("lblrawid")).Text;
            TextBox temptxb = (TextBox)GVR.FindControl("txtNullCode");
            nullCode = temptxb.Text;
            DropDownList tempddl = (DropDownList)GVR.FindControl("ddlActionCode");
            actionCode = tempddl.SelectedItem.Value;
            tempddl = (DropDownList)GVR.FindControl("ddlInclude");
            includeinSub = tempddl.SelectedItem.Value;

            string query = GenerateUpdateQuery(nullCode, actionCode, includeinSub, protocolID);
            ExecuteQuery(query,"save");
        }
    }

    protected void BtnBackOnClick(object sender, EventArgs e)
    {
        //Server.Transfer("~/Pages/PreValidation/PreValidationResult.aspx", true);
        Response.Redirect("~/Pages/PreValidation/PreValidationResult.aspx", true);
    }
    #endregion

    #region DataBase Connection -- DAL -- remove this part once DAL and BLL is added

    #region private member
    private SqlDataAdapter sqlAdapter;

    private string aqsSiteNumber = string.Empty;
    private string connectionstring = string.Empty;
    private string countyCode = string.Empty;
    private string errorMessage;
    private string parameterId = string.Empty;
    private string parameterName = string.Empty;
    private string stateCode = string.Empty;
    private string substanceOccuranceCode = string.Empty;

    private void initialParameters()
    {
        // Use Server.Transfer technique
        //this.aqsSiteNumber = Context.Items["aqs_site_number"].ToString();
        //this.countyCode = Context.Items["county_code"].ToString();
        //if (Context.Items["errormsg"] != null && !string.IsNullOrEmpty(Context.Items["errormsg"].ToString()))
        //{
        //    this.errorMessage = Context.Items["errormsg"].ToString();
        //}
        //this.parameterId = Context.Items["parameter_id"].ToString();
        //this.parameterName = Context.Items["parameter_name"].ToString();
        //this.stateCode = Context.Items["state_code"].ToString();
        //this.substanceOccuranceCode = Context.Items["substance_occurance_code"].ToString();

        // Use Response.Redirect technique
        if (Session["newContext"] != null)
        {
            Dictionary<string, string> pageContext = new Dictionary<string, string>();
            pageContext = (Dictionary<string, string>)Session["newContext"];
            this.aqsSiteNumber = pageContext["aqs_site_number"];
            this.countyCode = pageContext["county_code"];
            if (pageContext["errormsg"] != null && !string.IsNullOrEmpty(pageContext["errormsg"].ToString()))
            {
                this.errorMessage = pageContext["errormsg"];
            }
            this.parameterId = pageContext["parameter_id"];
            this.parameterName = pageContext["parameter_name"];
            this.stateCode = pageContext["state_code"];
            this.substanceOccuranceCode = pageContext["substance_occurance_code"];
        }

    }

    private string GetErrorCode()
    {
        string errorCode = "0";
        AQSDatabase aDB = new AQSDatabase("node");
        string query = "select pre_val_err_cd from aq_pre_val_err_cd where pre_val_err_msg = '" + this.errorMessage + "'";
        DataTable dt = aDB.GetSingleTableByQuery("errorMag", query);
        errorCode = dt.Rows[0]["pre_val_err_cd"].ToString();
        aDB.Close();
        return errorCode;

    }

    #endregion

    protected void GetConnectionString()
    {
        connectionstring = ConfigurationManager.ConnectionStrings["node"].ConnectionString;
    }

    protected DataTable ExecuteQuery(string query, string type)
    {
        DataSet result = new DataSet();
        if (this.sqlAdapter == null)
        {
            GetConnectionString();
        }
        if (type.Equals("save"))
        {
            string AQSdb = ConfigurationManager.ConnectionStrings["node"].ConnectionString;
            SqlConnection dbconnection = new SqlConnection(AQSdb);
            dbconnection.Open();

            SqlCommand command = new SqlCommand(query, dbconnection);
            command.CommandType = CommandType.Text;
            int i = command.ExecuteNonQuery();
            command.Dispose();
            dbconnection.Close();
        }
        else
        {
            SqlDataAdapter temp_adapter = new SqlDataAdapter(query, connectionstring);
            sqlAdapter = temp_adapter;

            
            sqlAdapter.Fill(result, query);
            sqlAdapter.Dispose();

            if (result != null && result.Tables.Count != 0)
            {
                return result.Tables[0];
            }
        }
        return new DataTable();
    }
    protected string GenerateQuery()
    {
        StringBuilder query = null;
        try
        {
            query = new StringBuilder("select distinct observation_date_time, sample_value, measure_unit_name,node_trans_id, null_data_code, action_code, include_in_sub_ind, aq_raw_data_hour.raw_data_hour_id as raw_data_hour_id");
            query.Append(" from aq_site_data, aq_monitor_data, aq_monitor_protocol, aq_raw_data_hour, aq_ref_unit, aq_pre_val_err_cd ");
            query.Append("where aq_site_data.site_id = aq_monitor_data.site_id and aq_monitor_data.monitor_id = aq_monitor_protocol.monitor_id and aq_monitor_protocol.protocol_id = aq_raw_data_hour.protocol_id and aq_monitor_protocol.measure_unit_code = aq_ref_unit.measure_unit_code ");
            query.Append(" and state_code = '" + this.stateCode + "'");
            query.Append(" and county_code = '" + this.countyCode + "'");
            query.Append(" and aqs_site_number = '" + this.aqsSiteNumber + "'");
            query.Append(" and parameter_id = '" + this.parameterId + "'");
            query.Append(" and substance_occurance_code = '" + this.substanceOccuranceCode + "' ");
            if (this.errorMessage != null && !string.IsNullOrEmpty(this.errorMessage))
            {
                query.Append(" and aq_raw_data_hour.pre_val_err_cd = '" + this.GetErrorCode() + "' ");
            }
            else
            {
                query.Append(" and aq_raw_data_hour.pre_val_err_cd is null ");
            }
            string[] temp = Session["observation_date_time"].ToString().Trim().Split('-');
            query.Append("and observation_date_time >= '" + temp[0].Trim() + "'");
            query.Append(" and observation_date_time <= '" + temp[1].Trim() +" 11:59:59 PM '");
        }
        catch (NullReferenceException ex)
        {
            throw ex;
        }
        //TEST DATA
        //query.Append(" and state_code = '16' and county_code = '021' and aqs_site_number = '0002' and parameter_id = '62101' and substance_occurance_code = '1'");
        //query.Append(" and observation_date_time >= '2/1/2003'");
        //query.Append(" and observation_date_time <= '2/1/2008'");
        return query.ToString();
    }
    protected string GenerateUpdateQuery(string NullCode, string ActionCode, string IncludeSubIND, string protocolID)
    {
        StringBuilder query = new StringBuilder("UPDATE AQ_RAW_DATA_HOUR SET");
        if (NullCode.Equals(""))
        {
            query.Append(" NULL_DATA_CODE = NULL ");
        }
        else
        {
            query.Append(" NULL_DATA_CODE = '" + NullCode.Trim() + "'");
        }
        query.Append (", ACTION_CODE = '" + ActionCode + "'");
        query.Append(", INCLUDE_IN_SUB_IND = '" + IncludeSubIND + "'");
        query.Append(" WHERE RAW_DATA_HOUR_ID = '" + protocolID + "'");
        return query.ToString();
    }
    
    #endregion
   
}