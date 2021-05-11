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

public partial class PageControls_WebParts_FavoriteLink : System.Web.UI.UserControl
{
    //private int numOfUserLinkToList = 5;

    void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadData();
        }

        lkbNewUserLink.Attributes.Add("onclick", "fnSetFocus('" + txtLinkName.ClientID + "');");
    }

    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        SystemConfiguration config = new SystemConfiguration();
        if (!string.IsNullOrEmpty(txtLinkName.Text) && !string.IsNullOrEmpty(txtURL.Text))
        {
            config.SetFavoriteLink(txtLinkName.Text.Trim(), txtURL.Text.Trim(), txtDescNew.Text.Trim());
            config.SaveConfiguration();
        }
        LoadData();
    }

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        SystemConfiguration config = new SystemConfiguration();
        if (!string.IsNullOrEmpty(txtURLEdit.Text))
        {
            config.SetFavoriteLink(lblLinkName.Text.Trim(), txtURLEdit.Text.Trim(), txtDesc.Text.Trim());
            config.SaveConfiguration();
        }
        LoadData();
    }

    protected void ShowLinkInfo_Click(object sender, EventArgs e)
    {
        this.mpeEditLink.Show();
        ImageButton btnInfo = (ImageButton)sender;
        if (btnInfo != null)
        {
            SetLinkInfo(btnInfo.CommandArgument);
        }
        Page.SetFocus(this.txtURLEdit);
    }

    protected void btnDeleteLink_Click(object sender, EventArgs e)
    {
        DeleteLinkInfo(lblLinkName.Text);
    }

    # region Protected Methods

    protected string GetLinkName(string linkName)
    {
        return string.Format("Favorite Link {0}", Eval(linkName).ToString());
    }

    protected string GetClickToName(string linkName)
    {
        return string.Format("Click to link to {0}", Eval(linkName).ToString());
    }

    # endregion

    # region Private Methods

    private void LoadData()
    {
        SystemConfiguration config = new SystemConfiguration();
        ArrayList links = config.GetAllFavoriteLinks();
        DataTable dt = new DataTable();
        DataRow dr;

        bool bNodeDomainAdmin = (bool)Session[Phrase.NODE_DOMAIN_ADMIN];

        if (links != null)
        {
            dt.Columns.Add("Name");
            dt.Columns.Add("Link");
            dt.Columns.Add("Description");
            dt.Columns.Add("Target");
            foreach (string[] link in links)
            {
                if (!string.IsNullOrEmpty(link[0]) && !string.IsNullOrEmpty(link[1]))
                {
                    if (link[0].Trim() == "AQS Data Management" && !this.CheckAQSClientCode())
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "Node Registration" && !bNodeDomainAdmin)
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "Node User" && !bNodeDomainAdmin)
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "Operation Manager" && !bNodeDomainAdmin)
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "ICIS Data Management" && !this.CheckICISManagement())
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "Submit Operation Manager" && !this.CheckSubmitOperation())
                    {
                        continue;
                    }
                    else if (link[0].Trim() == "Waste/Open Dump Management" && !this.CheckOpenDumpClient())
                    {
                        continue;
                    }

                    dr = dt.NewRow();
                    dr["Name"] = link[0];
                    dr["Link"] = link[1];
                    dr["Description"] = link[2];
                    if (dr["Name"].ToString() == "Node Registration"
                        || dr["Name"].ToString() == "Node User"
                        || dr["Name"].ToString() == "Operation Manager"
                        || dr["Name"].ToString() == "AQS Data Management"
                        || dr["Name"].ToString() == "Data Viewer"
                        || dr["Name"].ToString() == "ICIS Data Management"
                        || dr["Name"].ToString() == "Submit Operation Manager"
                        || dr["Name"].ToString() == "Waste/Open Dump Management")
                    {
                        dr["Target"] = "_parent";
                    }
                    else
                    {
                        dr["Target"] = "_blank";
                    }

                    dt.Rows.Add(dr);

                }
            }

            this.UserLinkDataList.DataSource = dt;
            this.UserLinkDataList.DataBind();
        }
    }
    private void SetLinkInfo(string linkName)
    {
        SystemConfiguration config = new SystemConfiguration();
        if (!string.IsNullOrEmpty(linkName))
        {
            this.lblLinkName.Text = linkName;
            string[] link = config.GetFavoriteLink(linkName);
            this.txtURLEdit.Text = link[0];
            this.txtDesc.Text = link[1];
            if (linkName == "Node Registration" || linkName == "Node User" 
                                                || linkName == "Operation Manager"
                                                || linkName == "AQS Data Management"
                                                || linkName == "ICIS Data Management"
                                                || linkName == "Submit Operation Manager"
                                                || linkName == "Waste/Open Dump Management"
                                                || linkName == "Data Viewer")
            {
                this.btnDeleteLink.Visible = false;
                this.txtURLEdit.Enabled = false;
            }
            else
            {
                this.btnDeleteLink.Visible = true;
                this.txtURLEdit.Enabled = true;
            }
        }
    }
    private void DeleteLinkInfo(string linkName)
    {
        SystemConfiguration config = new SystemConfiguration();
        if (!string.IsNullOrEmpty(linkName))
        {
            config.RemoveFavoriteLink(linkName);
            config.SaveConfiguration();
        }
        this.LoadData();
    }

    private bool CheckAQSClientCode()
    {
        bool bFlag = false;
        if ("" + ConfigurationManager.AppSettings["ClientCode"] != "")
        {
            if ("" + ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ClientCode"].ToString()] != "")
            {
                bFlag = true;
            }
        }
        return bFlag;
    }

    private bool CheckICISManagement()
    {
        bool bFlag = false;
        if ("" + ConfigurationManager.AppSettings["ICISMgrPlugin"] != "")
        {
            if ("" + ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["ICISMgrPlugin"].ToString()] != "")
            {
                bFlag = true;
            }
        }
        return bFlag;
    }

    private bool CheckSubmitOperation()
    {
        bool bFlag = false;
        if ("" + ConfigurationManager.AppSettings["SubmitOpPlugin"] != "")
        {
            if ("" + ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["SubmitOpPlugin"].ToString()] != "")
            {
                bFlag = true;
            }
        }
        return bFlag;
    }
   
    private bool CheckOpenDumpClient()
    {
        bool bFlag = false;
        if ("" + ConfigurationManager.AppSettings["OPClient"] != "")
        {
            if ("" + ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["OPClient"].ToString()] != "")
            {
                bFlag = true;
            }
        }
        return bFlag;
    }

    # endregion
}
