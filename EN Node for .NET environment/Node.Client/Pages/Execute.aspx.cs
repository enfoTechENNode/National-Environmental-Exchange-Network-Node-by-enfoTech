using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;
using System.Data;

using Node.Core.Util;


public partial class Pages_Execute : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult2 = false;
    private string sLineBreak = "<br>";
    protected void Page_Load(object sender, EventArgs e)
    {
        NodeTab1.SetPageAjaxTabControl = this.TabContainer1;
        if (!this.IsPostBack)
        {

            this.ShowResult2 = false;

            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken2.Text = token;
            }
        }
        this.ClientLeftPanel.HighLighter(9);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    void egvParameters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void btnQuery2_Click(object sender, EventArgs e)
    {
        string exeResult = "";

        this.txtQueryResult2.Text = "";
        this.btnDownloadPanel2.Visible = false;
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            try
            {
                this.SaveNodeURL(url);

                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);

                Node.Core2.Requestor.Execute exe = new Node.Core2.Requestor.Execute();
                exe.interfaceName = this.txtInterfaceName.Text;
                exe.methodName = this.txtMethodName.Text;
                exe.securityToken = this.txtToken2.Text;
                
                if ("" + this.ViewState["Parameters"] == "")
                {
                    DataTable pTable = (DataTable)this.ViewState["Parameters"];

                    Node.Core2.Requestor.ParameterType[] parameters = new Node.Core2.Requestor.ParameterType[pTable.Rows.Count];

                    int i = 0;
                    foreach (DataRow aRow in pTable.Rows)
                    {
                        parameters[i] = new Node.Core2.Requestor.ParameterType();
                        parameters[i].parameterName = aRow["PARAM_NAME"].ToString();
                        parameters[i].Value = aRow["PARAM_VALUE"].ToString();
                        i++;
                    }

                    exe.parameters = parameters;
                }

                Node.Core2.Requestor.ExecuteResponse result = req.Execute(exe);
                exeResult = "Transation ID: " + result.transactionId + sLineBreak;
                exeResult += "Status: " + result.status;
                exeResult += "XML Result: " +NodeXMLResult.GetWellFormatXML(result.results.Any[0].OuterXml);

            }
            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    exeResult = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    exeResult= "System error." + Environment.NewLine + sex.Message;
                }
            }
            catch (Exception ex)
            {
                //exeResult = "System error." + sLineBreak + ex.ToString();
                exeResult = "System error." + sLineBreak + ex.Message;
                this.btnDownloadPanel2.Visible = false;
            }
        }
        else
        {
            exeResult = "Please Enter a Node URL";
            this.btnDownloadPanel2.Visible = false;
        }

        this.txtQueryResult2.Text = exeResult;
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);   

    }

    protected void btnDownload2_Click(object sender, EventArgs e)
    {

    }

    protected void btnAddParameter_Click(object sender, EventArgs e)
    {
        if ("" + this.ViewState["Parameters"] == "")
        {
            DataTable aTable = new DataTable();
            aTable.Columns.Add(new DataColumn("CHECK", typeof(bool)));
            aTable.Columns.Add(new DataColumn("PARAM_NAME", typeof(string)));
            aTable.Columns.Add(new DataColumn("PARAM_VALUE", typeof(string)));
            this.ViewState.Add("Parameters", aTable);
        }
 
        DataTable pTable = (DataTable)this.ViewState["Parameters"];
        DataRow newRow = pTable.NewRow();
        newRow["CHECK"] = false;
        newRow["PARAM_NAME"] = this.txtParamName.Text;
        newRow["PARAM_VALUE"] = this.txtParamValue.Text;
        pTable.Rows.Add(newRow);
        this.egvParameters.DataSource = pTable;
        this.egvParameters.DataBind();

    }

    protected void btnRemoveParameter_Click(object sender, EventArgs e)
    {
        if ("" + this.ViewState["Parameters"] != "")
        {
            DataTable pTable = (DataTable)this.ViewState["Parameters"];
            foreach (GridViewRow aRow in egvParameters.Rows)
            {
                HtmlInputCheckBox chk = (HtmlInputCheckBox)aRow.Cells[0].Controls[0];
                if (chk.Checked)
                {
                    pTable.Rows[aRow.RowIndex]["CHECK"] = true;
                }
            }

            DataRow[] aRows = pTable.Select("CHECK=True");
            for (int i = 0; i < aRows.Length; i++)
                aRows[i].Delete();

            this.egvParameters.DataSource = pTable;
            this.egvParameters.DataBind();
        }
    }
}
