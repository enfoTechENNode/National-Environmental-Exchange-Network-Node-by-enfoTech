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

using Node.Lib.UI.Elements;
using Node.Core;

public partial class PageControls_Share_DashboardTab : System.Web.UI.UserControl
{
    private ArrayList headTabsAry;
    //private Hashtable headItemsHash;

    public string FocusTabID
    {
        get { return "" + Session[Phrase.VERSION_NO]; }
        set { Session[Phrase.VERSION_NO] = value; }
    }

    //public string FocusItemID
    //{
    //    get { return "" + ViewState["FocusItemID"]; }
    //    set { ViewState["FocusItemID"] = value; }
    //}

    //public bool HasLeftPanel
    //{
    //    get { return (ViewState["HasLeftPanel"] == null) ? true : ((bool)ViewState["HasLeftPanel"]); }
    //    set { ViewState["HasLeftPanel"] = value; }
    //}

    protected ArrayList headTabs
    {
        get
        {
            return this.headTabsAry;
        }
    }

    //protected Hashtable headItems
    //{
       
    //    get
    //    {
    //        return this.headItemsHash;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        CreateObjects();
    }

    //protected ArrayList GetItemList(XmlTreeNode headTab)
    //{
    //    if (headTab == null) return new ArrayList();
    //    return (ArrayList)this.headItems[headTab.GetAttribute("id")];
    //}

    protected bool IsTabFocus(string headTab)
    {
        if (headTab == FocusTabID)
            return true;
        else
            return false;
    }

    //protected bool IsItemFocus(XmlTreeNode headItem)
    //{
    //    if (headItem.GetAttribute("id") == FocusItemID)
    //        return true;
    //    else
    //        return false;
    //}

    private void CreateObjects()
    {
        this.headTabsAry = new ArrayList();
        //this.headItemsHash = new Hashtable();

        for (int i = 0; i < 2; i++)
        {
            if (!this.headTabsAry.Contains("VER_11"))
                this.headTabsAry.Add("VER_11");
            else if (!this.headTabsAry.Contains("VER_20"))
                this.headTabsAry.Add("VER_20");
        }
    }
}
