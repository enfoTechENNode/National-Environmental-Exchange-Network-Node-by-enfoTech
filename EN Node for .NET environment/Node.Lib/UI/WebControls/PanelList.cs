using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Node.Lib.UI.WebControls
{
    //***********************************************************
    // Control Builder Class
    //***********************************************************
    public class PanelListlBuilder : ControlBuilder
    {
        public override Type GetChildControlType(string tabName, IDictionary attributes)
        {
            if (tabName.ToLower().EndsWith("panelitem"))
            {
                return typeof(PanelItem);
            }
            return null;
        }

        public override void AppendLiteralString(string s)
        {
            // do nothing to ignore literial strings
        }

    }

    //***********************************************************
    // Control Class
    //***********************************************************

    [ControlBuilderAttribute(typeof(PanelListlBuilder)), ParseChildren(false)]

    public class PanelList : WebControl
    {
        public const string TYPE_WIZARD = "wizard";
        public const string TYPE_REGULAR = "regular";

        public const string DISPLAY_BOTH = "both";
        public const string DISPLAY_TITLE = "title";
        public const string DISPLAY_CONTENT = "content";

        //==============================
        // Members
        //==============================
        private PanelItemCollection items = new PanelItemCollection();
        private int highlightIdx = -1;
        private string pnlStyleWidth = "";
        private string pnlTitle = "Title";
        private string titleImageSrc = "";
        private string itmImageSrc = "";
        private string pnlType = TYPE_REGULAR;
        private string dspType = DISPLAY_BOTH;
        private bool firstPanel = false;

        //==============================
        //  Attributes
        //==============================
        public int HighlightIndex
        {
            get { return highlightIdx; }
            set { highlightIdx = value; }
        }
        public string PanelStyleWidth
        {
            get { return pnlStyleWidth; }
            set { pnlStyleWidth = value; }
        }

        public string PanelTitle
        {
            get { return pnlTitle; }
            set { pnlTitle = value; }
        }
        public string TitleImageSrc
        {
            get { return titleImageSrc; }
            set { titleImageSrc = value; }
        }
        public string ItemImageSrc
        {
            get { return itmImageSrc; }
            set { itmImageSrc = value; }
        }
        public string PanelType
        {
            get { return pnlType; }
            set { pnlType = value; }
        }
        public string DisplayType
        {
            get { return dspType; }
            set { dspType = value; }
        }

        public bool FirstPanel
        {
            get { return firstPanel; }
            set { firstPanel = value; }
        }

        public PanelItemCollection Items
        {
            get { return items; }
        }

        public void AddItem(PanelItem obj)
        {
            if (obj != null)
                items.Add(obj);
        }

        public void RemoveItem(int i)
        {
            if (i >= 0 && i < items.Count)
                items.RemoveAt(i);
        }

        //========================
        // Methods
        //========================
        protected override void AddParsedSubObject(Object obj)
        {
            if (obj is PanelItem)
            {
                items.Add((PanelItem)obj);
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            // draw nothing if there is not items
            if (this.items.Count < 0) return;

            // default selected index will be 0
            if (this.highlightIdx >= this.items.Count) this.highlightIdx = -1;

            if (this.pnlType == TYPE_WIZARD)
                output.Write(GetWizardPanel());
            else
                output.Write(GetRegularPanel());
        }

        //========================
        // private methods
        //========================
        private string GetRegularPanel()
        {
            StringBuilder s = new StringBuilder();

            s.Append("<div class=\"wc_Pnl\"");
            if (this.pnlStyleWidth != "") s.Append(" style=\"width:" + this.pnlStyleWidth + "\" ");
            s.Append(" >");

            if (this.dspType.ToLower() == DISPLAY_BOTH || this.dspType.ToLower() == DISPLAY_TITLE)
            {
                if (this.firstPanel)
                    s.Append("<h2>");
                else
                    s.Append("<h1>");

                if (this.titleImageSrc != "")
                    s.Append("<img src=\"" + ResolveUrl(this.titleImageSrc) + "\" runat=\"server\" />");

                s.Append(this.pnlTitle);

                if (this.firstPanel)
                    s.Append("</h2>");
                else
                    s.Append("</h1>");
            }

            if (this.dspType.ToLower() == DISPLAY_BOTH || this.dspType.ToLower() == DISPLAY_CONTENT)
            {
                s.Append("<div class=\"wc_PnlList\">");

                for (int i = 0; i < this.items.Count; i++)
                {
                    PanelItem itm = items[i];

                    if (itm.Visible)
                    {
                        s.Append("<a class=\"" + ((i == this.highlightIdx) ? "on" : "") + "\" href=\"" + ResolveUrl(itm.Link) + "\"");
                        if (!itm.Target.Equals(""))
                            s.Append(" target=\"" + itm.Target + "\"");
                        if (!itm.Description.Equals(""))
                            s.Append(" title=\"" + itm.Description + "\">");
                        else
                            s.Append(">");

                        if (itm.ImageSrc != "")
                            s.Append("<span style=\"background-image:url(" + ResolveUrl(itm.ImageSrc) + ");\">" + itm.Caption + "</span>");
                        else if (this.itmImageSrc != "")
                            s.Append("<span style=\"background-image:url(" + ResolveUrl(this.itmImageSrc) + ");\">" + itm.Caption + "</span>");
                        else
                            s.Append("<span>" + itm.Caption + "</span>");

                        s.Append("</a>");
                    }
                }
                s.Append("</div>");
            }

            s.Append("</div>");

            return s.ToString();
        }


        private string GetWizardPanel()
        {
            StringBuilder s = new StringBuilder();

            s.Append("<div class=\"wc_Pnl\"");
            if (this.pnlStyleWidth != "") s.Append(" style=\"width:" + this.pnlStyleWidth + "\" ");
            s.Append(" >");

            if (this.dspType.ToLower() == DISPLAY_BOTH || this.dspType.ToLower() == DISPLAY_TITLE)
            {
                s.Append("<h2 class=\"green\">");

                if (this.titleImageSrc != "")
                    s.Append("<img src=\"" + ResolveUrl(this.titleImageSrc) + "\" />");

                s.Append(this.pnlTitle);
                s.Append("</h2>");
            }

            if (this.dspType.ToLower() == DISPLAY_BOTH || this.dspType.ToLower() == DISPLAY_CONTENT)
            {

                s.Append("<div class=\"wc_PnlList\">");

                for (int i = 0; i < this.items.Count; i++)
                {
                    PanelItem itm = items[i];

                    if (i < this.highlightIdx)
                        s.Append("<div class=\"done\" title=\"" + itm.Description + "\">");
                    else if (i == this.highlightIdx)
                        s.Append("<div class=\"on\" title=\"" + itm.Description + "\">");
                    else
                        s.Append("<div title=\"" + itm.Description + "\">");

                    int step = i + 1;
                    s.Append("<span class=\"step" + step + "\">" + itm.Caption + "</span>");

                    s.Append("</div>");
                }
                s.Append("</div>");
            }

            s.Append("</div>");

            return s.ToString();
        }

    }
}
