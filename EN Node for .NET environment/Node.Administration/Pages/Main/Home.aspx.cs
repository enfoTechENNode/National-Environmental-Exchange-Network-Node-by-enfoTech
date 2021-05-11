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

public partial class Pages_Main_Home : Node.Lib.UI.Base.PageBase
{
    private string ver = Phrase.VERSION_11;
    #region Page events
    protected void Page_PreInit(object sender, EventArgs e)
    {        
    }  
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["ver"] != null)
            {
                ver = Request["ver"].ToString();
                if (ver == "1")
                    Session[Phrase.VERSION_NO] = Phrase.VERSION_11;
                else
                    Session[Phrase.VERSION_NO] = Phrase.VERSION_20;
            }
            else
            {
                if (Session[Phrase.VERSION_NO] == null)
                    Session[Phrase.VERSION_NO] = Phrase.VERSION_20;
            }
        }
        else
        {
            if (this.Session[Phrase.USER_SESSION_KEY] == null)
                Response.Redirect("~/Pages/Main/Login.aspx");
        }
        
        SetSkipNavigationLink();
        //OpenWebPartForm();
    }

    //private void OpenWebPartForm()
    //{
    //    if (Session["OpenForm"] != null)
    //    {
    //        if (Session["OpenForm"].ToString() == "OperationMgr")
    //        {
    //            PageControls_WebParts_Configuration a = (PageControls_WebParts_Configuration)webPartMgr.WebParts["config"].Controls[0];
    //            a.OpenOperationManager();
    //            Session["OpenForm"] = "";
    //        }
    //    }
    //}

    private void SetSkipNavigationLink()
    {
        this.SkipNavCtl.Text = "<a id =\"SkipLinkTop\" href=\"#\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">SkipLinkTop</a>";

        if ("" + ConfigurationManager.AppSettings["SkipNavLink"] != "" && ConfigurationManager.AppSettings["SkipNavLink"].ToString().Equals("True"))
        {
            foreach (WebPart aPart in leftPartzone.WebParts)
            {
                AddSkipNavLink(aPart.Title);
            }
            foreach (WebPart aPart in middlePartZone.WebParts)
            {
                AddSkipNavLink(aPart.Title);
            }
            foreach (WebPart aPart in rightPartZone.WebParts)
            {
                AddSkipNavLink(aPart.Title);
            }
        }
    }

    private void AddSkipNavLink(string name)
    {
        string sLink = "";
        switch (name)
        {
            case "Node Status":
                sLink = "<a href=\"#STS\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Status</a>";
                break;
            case "Node Domains":
                sLink = "<a href=\"#DOM\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Domains</a>";
                break;
            case "Node Document":
                sLink = "<a href=\"#DOC\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Document</a>";
                break;
            case "Node Transaction Log":
                sLink = "<a href=\"#LOG\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Transaction Log</a>";
                break;
            case "Scheduled Tasks":
                sLink = "<a href=\"#TSK\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Scheduled Tasks</a>";
                break;
            case "Node Notifications":
                sLink = "<a href=\"#NOF\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Notifications</a>";
                break;
            case "Favorite Links":
                sLink = "<a href=\"#LNK\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Favorite Links</a>";
                break;
            case "Node Configuratio":
                sLink = "<a href=\"#CNF\" style=\"color: #FFFFFF;text-decoration: none;font-size:xx-small;\">Node Configuratio</a>";
                break;

        }
        SkipNavCtl.Text = SkipNavCtl.Text+sLink;
    }

    protected override void PagePreRender(EventArgs e)
    {
        if (IsAllPartsClosed()){
            this.webPartMgr.DisplayMode = WebPartManager.CatalogDisplayMode;
        }
        ChangeCatTitle();
    }
    #endregion

    #region events
    //------------------------------------------------------------------
    protected void Cat_Click(object sender, EventArgs e)
    {
        this.webPartMgr.DisplayMode = WebPartManager.CatalogDisplayMode;
    }

    protected void webPartMgr_DisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
    {
        if (this.webPartMgr.DisplayMode == WebPartManager.BrowseDisplayMode)
        {
            this.tbAddContent.Visible = true;
        }
        else
        {
            this.tbAddContent.Visible = false;
        }
    }
    #endregion

    #region private methods
    private bool IsAllPartsClosed()
    {
        foreach (WebPart wp in this.webPartMgr.WebParts)
        {
            if (!wp.IsClosed)
            {
                return false;
            }
        }
        return true;
    }

    private void ChangeCatTitle()
    {
        if (this.pgCat != null)
        {
            if (this.pgCat.GetAvailableWebPartDescriptions().Count == 0)
            {
                this.pgCat.Title = "No Content Available.";
            }
            else
            {
                this.pgCat.Title = "Content List";
            }
        }
    }

    #endregion
}
