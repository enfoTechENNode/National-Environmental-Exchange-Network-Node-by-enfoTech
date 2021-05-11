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
using Node.Core.Data;

public partial class PageControls_WebParts_NodeStatus : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfiguration config = new SystemConfiguration();
        // Node Status Message
        this.lblNodeStatus.Text = config.GetNodeStatus();
        this.lblNodeStatusMsg.Text = config.GetNodeStatusMessage();

        if (!Page.IsPostBack)
        {
            this.PageControlsInit();
        }
    }

    protected void Timer_Tick(object sender, EventArgs args)
    {        
        //  refresh the grid
        this.PageControlsInit();
    }

    protected void lkbTurnOff_Click(object sender, EventArgs args)
    {
        if (this.lkbTurnOff.ToolTip.Contains("Turn off"))
        {
            this.lkbTurnOff.ToolTip = this.lkbTurnOff.ToolTip.Replace("Turn off", "Turn on");
            this.Timer1.Interval = 3600 * 12 * 1000;
            this.TotalProcess.Visible = false;
            this.egvProcessGrid.Visible = false;
            this.lkbTurnOff.ImageUrl = "~/App_Images/Node/PnlIco/pnlico_processClose.gif";

        }
        else
        {
            this.lkbTurnOff.ToolTip = this.lkbTurnOff.ToolTip.Replace("Turn on", "Turn off");
            this.Timer1.Interval = 1500;
            this.TotalProcess.Visible = true;
            this.egvProcessGrid.Visible = true;
            this.lkbTurnOff.ImageUrl = "~/App_Images/Node/PnlIco/pnlico_process.gif";
        }
    }

    private void PageControlsInit()
    {
        DBManager dbMgr = new DBManager();
        this.egvProcessGrid.CachedDataTable = dbMgr.GetOperationsDB().GetProcesses();
        this.egvProcessGrid.DataBind();
        this.TotalProcess.Text = "Current running processes: " + this.egvProcessGrid.CachedDataTable.Rows.Count + ".";
    }
}
