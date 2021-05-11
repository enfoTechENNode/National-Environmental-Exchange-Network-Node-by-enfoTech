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

public partial class PageControls_WebParts_NodeClient : System.Web.UI.UserControl
{
    void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadData();
        }
    }


    private void LoadData()
    {
        string nodeClientURL = "http://localhost:56408/Node.Client/Pages/";
        DataTable dt = new DataTable();
        dt.Columns.Add("Name");
        dt.Columns.Add("PageURL");
        dt.Columns.Add("Target");
        DataRow dr = dt.NewRow();
        dr["Name"] = "Node Client";//"Node Ping";
        dr["PageURL"] = nodeClientURL + "NodePing.aspx";
        dr["Target"] = "_blank";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Name"] = "Node Registration";
        dr["PageURL"] = "../Registration/NodeRegistration.aspx";
        dr["Target"] = "_parent";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Name"] = "Node Users";
        dr["PageURL"] = Request.ApplicationPath + "/Pages/User/SearchUsers.aspx";
        dr["Target"] = "_parent";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["Name"] = "Task Wizard";
        dr["PageURL"] = Request.ApplicationPath + "/Pages/DataWizard/DataWizardTestPage.aspx";
        dr["Target"] = "_parent";
        dt.Rows.Add(dr);

        this.UserLinkDataList.DataSource = dt;
        this.UserLinkDataList.DataBind();
    }
}
