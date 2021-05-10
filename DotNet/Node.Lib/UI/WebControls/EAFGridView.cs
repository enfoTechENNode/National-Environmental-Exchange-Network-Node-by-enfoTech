#region Refenence
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

using Node.Lib.UI.Elements;
#endregion

#region WebResource
[assembly: WebResource("Node.Lib.UI.Resources.img.icon-arrowdown.gif", "img/gif")]
[assembly: WebResource("Node.Lib.UI.Resources.img.icon-arrowup.gif", "img/gif")]
[assembly: WebResource("Node.Lib.UI.Resources.img.icon-arrowdown_y.gif", "img/gif")]
[assembly: WebResource("Node.Lib.UI.Resources.img.icon-arrowup_y.gif", "img/gif")]
#endregion

namespace Node.Lib.UI.WebControls
{
    //#######################################################################################
    // class SortedField
    //#######################################################################################

    /// <summary>
    /// Sorted field structure
    /// </summary>
    [Serializable]
    public class SortAttribute
    {
        /// <summary>
        /// Sort field name
        /// </summary>
        public string Field;

        /// <summary>
        /// Sort field direction
        /// </summary>
        public SortDirection Direction;

        /// <summary>
        /// Constructor
        /// </summary>
        public SortAttribute()
        { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="fld">field name</param>
        /// <param name="dir">sort direction</param>
        public SortAttribute(string fld, SortDirection dir)
        {
            this.Field = fld;
            this.Direction = dir;
        }
    }

    //#######################################################################################
    // class EAFGridView
    //#######################################################################################

    /// <summary>
    /// Extending ASP.NET GridView and add multi column sorting and paging.
    /// </summary>
    public class EAFGridView : GridView, IPostBackEventHandler
    {
        //***********************************************************************
        // private members
        //***********************************************************************
        private int groupFieldCellNo = -1;
        //private GridViewAgent gvAgent = null;
        //private ICollection _cacheContainer = null;
        private GridViewAgent gvAgent;
        private ICollection _cacheContainer;
        private string _sessionUniqueID = null;

        private string ImageBase
        {
            get { return this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase; }
        }

        //private Hashtable colCapMapping
        //{
        //    get
        //    {
        //        object o = this.Page.Session[this.UniqueID + "mapping"];
        //        if (o == null)
        //        {
        //            Hashtable ht = new Hashtable();
        //            this.Page.Session[this.UniqueID + "mapping"] = ht;
        //            return ht;				
        //        }
        //        else
        //        {
        //            return (Hashtable)o;
        //        }
        //    }
        //}

        

        /// <summary>
        /// cached GridViewView object
        /// </summary>
        private GridViewView GVView
        {
            get
            {
                object o = null;
                if (this.CacheContainer is HttpSessionState)
                {
                    o = ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "GVView"];
                }
                else if (this.CacheContainer is IDictionary)
                {
                    o = ((IDictionary)this.CacheContainer)[SessionUniqueID + "GVView"];
                }
                return (o != null ? (GridViewView)o : null);
            }
            set
            {
                if (this.CacheContainer is HttpSessionState)
                {
                    ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "GVView"] = value;
                }
                else if (this.CacheContainer is IDictionary)
                {
                    ((IDictionary)this.CacheContainer)[SessionUniqueID + "GVView"] = value;
                }
            }
        }

        /// <summary>
        /// Need to cache use Session since it will be remember if leaving the page
        /// </summary>
        private int SessionCachedPageIndex
        {
            get
            {
                object o = HttpContext.Current.Session[SessionUniqueID + "SessionCachedPageIndex"];
                return (o == null) ? -1 : (int)o;
            }
            set
            {
                HttpContext.Current.Session[SessionUniqueID + "SessionCachedPageIndex"] = value;
            }
        }

        private ArrayList SessionCachedSortAttributeList
        {
            get
            {
                object o = HttpContext.Current.Session[SessionUniqueID + "SessionCachedSortAttributeList"];
                return (o == null) ? null : (ArrayList)o;
            }
            set
            {
                HttpContext.Current.Session[SessionUniqueID + "SessionCachedSortAttributeList"] = value;
            }
        }



        //***********************************************************************
        // public members/Properties
        //***********************************************************************

        public string SessionUniqueID
        {
            get
            {
                if (string.IsNullOrEmpty(_sessionUniqueID))
                {
                    _sessionUniqueID = "EAFGridView." + this.UniqueID;
                }
                return _sessionUniqueID;
            }
            set
            {
                _sessionUniqueID = value;
            }
        }

        /// <summary>
        /// Set true to enable page index and sort expression to be stored in Session automatically
        /// </summary>
        public bool EnablePageSortSessionCache
        {
            get
            {
                object o = ViewState["EnablePageSortSessionCache"];
                return (o == null) ? false : (bool)o;
            }
            set
            {
                ViewState["EnablePageSortSessionCache"] = value; 
            }
        }


        /// <summary>
        /// if you have GridCheckBoxField in your GridView and you want to remember it while paging.
        /// </summary>
        public string RememberAllCheckedValueID {get; set;}

        public List<string> AllCheckedVals
        {
            get
            {
                object o = null;
                if (this.CacheContainer is HttpSessionState)
                {
                    o = ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "AllCheckedVals"];
                }
                else if (this.CacheContainer is IDictionary)
                {
                    o = ((IDictionary)this.CacheContainer)[SessionUniqueID + "AllCheckedVals"];
                }

                if (o == null)
                {
                    o = new List<string>();
                    if (this.CacheContainer is HttpSessionState)
                    {
                        ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "AllCheckedVals"] = o;
                    }
                    else if (this.CacheContainer is IDictionary)
                    {
                        ((IDictionary)this.CacheContainer)[SessionUniqueID + "AllCheckedVals"] = o;
                    }
                }
                return (List<string>)o;
            }
        }


        /// <summary>
        /// Gets or sets DataTable which will be cached in Session.
        /// </summary>
        public ICollection CacheContainer
        {
            get
            {
                return (_cacheContainer != null) ? _cacheContainer : HttpContext.Current.Session;
            }
            set
            {
                _cacheContainer = value;
            }
        }

        /// <summary>
        /// Data Table to be bind to GridView
        /// </summary>
        public DataTable CachedDataTable
        {
            get
            {
                object o = null;
                if (this.CacheContainer is HttpSessionState)
                {
                    o = ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "CachedDataTable"];
                }
                else if (this.CacheContainer is IDictionary)
                {
                    o = ((IDictionary)this.CacheContainer)[SessionUniqueID + "CachedDataTable"];
                }
                return (o != null ? (DataTable)o : null);
            }
            set
            {
                if (this.CacheContainer is HttpSessionState)
                {
                    ((HttpSessionState)this.CacheContainer)[SessionUniqueID + "CachedDataTable"] = value;
                }
                else if (this.CacheContainer is IDictionary)
                {
                    ((IDictionary)this.CacheContainer)[SessionUniqueID + "CachedDataTable"] = value;
                }

                this.DataSource = value;
                this.SortAttributeList.Clear();

                //foreach (DataColumn col in value.Columns)
                //    this.colCapMapping[col.ColumnName] = col.Caption;
            }
        }

        /// <summary>
        /// Gets or sets group field for row span.
        /// </summary>
        public string GroupField
        {
            get
            {
                object o = ViewState["GroupField"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["GroupField"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Whether Sorting On more than one column is enabled"),
        Category("Behavior"),
        DefaultValue("false"),
        ]
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["AllowMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                this.AllowSorting = true;
                ViewState["AllowMultiColumnSorting"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Get or Set the look and feel of Excel"),
        Category("Appearance"),
        ]
        public TableItemStyle ExcelStyle
        {
            get
            {
                object o = ViewState["ExcelStyle"];
                return (o != null ? (TableItemStyle)o : null);
            }
            set
            {
                ViewState["ExcelStyle"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Whether to expose Excel functionality or not"),
        Category("Behavior"),
        DefaultValue("false"),
        ]
        public bool ShowExcel
        {
            get
            {
                object o = ViewState["ShowExcel"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                //this.AllowSorting = true;
                ViewState["ShowExcel"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Excel Link Wording"),
        Category("Behavior"),
        DefaultValue("Excel"),
        ]
        public string ExcelText
        {
            get
            {
                object o = ViewState["ExcelText"];
                return (o != null ? (string)o : "Excel");
            }
            set
            {
                //this.AllowSorting = true;
                ViewState["ExcelText"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Excel File Name"),
        Category("Behavior"),
        DefaultValue("ExcelFile.xls"),
        ]
        public string ExcelFileName
        {
            get
            {
                object o = ViewState["ExcelFileName"];
                return (o != null ? (string)o : "ExcelFile.xls");
            }
            set
            {
                //this.AllowSorting = true;
                ViewState["ExcelFileName"] = value;
            }
        }

        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Excel Image link URL"),
        Category("Behavior"),
        DefaultValue("file_excel.gif"),
        ]
        public string ExcelImageFile
        {
            get
            {
                object o = ViewState["ExcelImageFile"];
                return (o != null ? (string)o : "file_excel.gif");
            }
            set
            {
                //this.AllowSorting = true;
                ViewState["ExcelImageFile"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Ascending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Descending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }

        /// <summary>
        /// Record sorted fields, especially for multi-column sorting.
        /// </summary>
        public ArrayList SortAttributeList
        {
            get
            {
                object o = ViewState["SortAttributeList"];
                if (o != null)
                {
                    return (ArrayList)o;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    ViewState["SortAttributeList"] = al;
                    return al;
                }
            }

            set
            {
                ViewState["SortAttributeList"] = value;
            }

        }

        /// <summary>
        /// Enable/Disable ResultMessage.
        /// </summary>
        [
        Description("Whether to expose ResultMessage or not"),
        Category("Behavior"),
        DefaultValue("false"),
        ]
        public bool ShowResultMessage
        {
            get
            {
                object o = ViewState["ShowResultMessage"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                //this.AllowSorting = true;
                ViewState["ShowResultMessage"] = value;
            }
        }

        /// <summary>
        /// Messaage: Results {0} of {1}
        /// </summary>
        public String ResultMessageTemplate
        {
            get
            {
                object o = ViewState["RecordMessageTemplate"];
                return (o != null ? o.ToString() : "Results {0} of {1}");
            }
            set
            {
                ViewState["RecordMessageTemplate"] = value;
            }
        }

        /// <summary>
        /// Result message style
        /// </summary>
        public String ResultMessageStyle
        {
            get
            {
                object o = ViewState["ResultMessageStyle"];
                return (o != null ? o.ToString() : String.Empty);
            }
            set
            {
                ViewState["ResultMessageStyle"] = value;
            }
        }

        /// <summary>
        /// Set to true if header background is dark color
        /// </summary>
        public bool DarkHeaderBackground
        {
            get
            {
                object o = ViewState["DarkHeaderBackground"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                ViewState["DarkHeaderBackground"] = value;
            }
        }


        /// <summary>
        /// Constructor, adding PafeIndexChangeing and Sorting events.
        /// </summary>
        public EAFGridView()
            : base()
        {
            this.PageIndexChanging += new GridViewPageEventHandler(EventPageIndexChanging);
            this.Sorting += new GridViewSortEventHandler(EventSorting);
        }

        //***********************************************************************
        // public methods
        //***********************************************************************

        /// <summary>
        /// Get generated sort expression from SortAttributeList
        /// </summary>
        public String GetSortAttributeExpression(ArrayList attrs)
        {
            if (attrs == null) { return ""; }

            string exp = "";
            for (int i = 0; i < attrs.Count; i++)
            {
                SortAttribute sa = (SortAttribute)attrs[i];
                exp += sa.Field + (sa.Direction == SortDirection.Ascending ? " ASC" : " DESC") + ",";
            }
            return exp.TrimEnd(",".ToCharArray());
        }

        /// <summary>
        /// Rebind cached data table and keep sort properties
        /// </summary>
        public void RefreshView()
        {
            RefreshView(false);
        }

        /// <summary>
        /// Rebind cached data table and reset page index and sort expression, if set to true.
        /// </summary>
        /// <param name="forceReset"></param>
        public void RefreshView(bool forceReset)
        {
            if (forceReset)
            {
                SortAttributeList.Clear();
                PageIndex = 0;
            }
            else if (EnablePageSortSessionCache)
            {
                SortAttributeList = SessionCachedSortAttributeList;
                if (SessionCachedPageIndex >= 0) { PageIndex = SessionCachedPageIndex; }
            }

            DataBindView();
        }



        /// <summary>
        /// Get current DataView of achedDataTable
        /// </summary>
        /// <returns>DataView object</returns>
        public DataView GetCurrentDataView()
        {
            DataView dv = new DataView(this.CachedDataTable);
            dv.Sort = GetSortAttributeExpression(this.SortAttributeList);

            return dv;
        }

        /// <summary>
        /// Get object of specified field.
        /// </summary>
        /// <param name="e">GridViewEditEventArgs</param>
        /// <param name="fldName">field name</param>
        /// <returns>object of the field</returns>
        public object GetCurrentDataViewData(GridViewEditEventArgs e, string fldName)
        {
            int rowIdx = this.PageIndex * this.PageSize + e.NewEditIndex;
            return GetCurrentDataView()[rowIdx].Row[fldName];
        }

        /// <summary>
        /// Get object of specified field.
        /// </summary>
        /// <param name="e">GridViewEditEventArgs</param>
        /// <param name="fldName">field name</param>
        /// <returns>object of the field</returns>
        public object GetCurrentDataViewData(GridViewRowEventArgs e, string fldName)
        {
            int rowIdx = this.PageIndex * this.PageSize + e.Row.RowIndex;
            return GetCurrentDataView()[rowIdx].Row[fldName];
        }

        /// <summary>
        /// Get object of specified field.
        /// </summary>
        /// <param name="e">GridViewCommandEventArgs</param>
        /// <param name="fldName">field name</param>
        /// <returns>object of the field</returns>
        public object GetCurrentDataViewData(GridViewCommandEventArgs e, string fldName)
        {
            int rowIdx = this.PageIndex * this.PageSize + Convert.ToInt32(e.CommandArgument + "", CultureInfo.CurrentCulture);
            return GetCurrentDataView()[rowIdx].Row[fldName];
        }

        /// <summary>
        /// Get checked value of <see cref="Node.Lib.UI.WebControls.GridCheckBoxField" />
        /// </summary>
        /// <param name="id">CheckBoxField control ID</param>
        /// <returns></returns>
        public string[] GetCheckedValue(string id)
        {
            ArrayList ary = new ArrayList();

            foreach (GridViewRow row in this.Rows)
            {
                //TableCell cell = row.Cells[cellIdx];

                HtmlInputCheckBox chk = row.FindControl(id) as HtmlInputCheckBox;

                if (chk != null && chk.Checked)
                {
                    ary.Add(chk.Value);
                }

                GridRadioButton rdo = row.FindControl(id) as GridRadioButton;

                if (rdo != null && rdo.Checked)
                {
                    ary.Add(rdo.Value);
                }
            }

            string[] ss = new string[ary.Count];
            for (int i = 0; i < ary.Count; i++)
            {
                ss[i] = "" + ary[i];
            }
            return ss;
        }

        /// <summary>
        /// Get TextBox controls of <see cref="Node.Lib.UI.WebControls.GridTextBoxField" /> control list based on TextBox ID
        /// </summary>
        /// <param name="id">TextBox control ID</param>
        /// <returns>Controls list of GridTextBoxField</returns>
        public ArrayList GetTextBoxFields(string id)
        {
            ArrayList ary = new ArrayList();

            foreach (GridViewRow row in this.Rows)
            {
                //foreach (TableCell cell in row.Cells)
                //{
                //    if (cell.Controls.Count > 0)
                //    {
                //        TextBox tb = cell.Controls[0] as TextBox;

                //        if (tb != null && tb.ID == id)
                //            ary.Add(tb);
                //    }
                //}

                TextBox tb = row.FindControl(id) as TextBox;
                if (tb != null && tb.ID == id) ary.Add(tb);
            }
            return ary;
        }

        /// <summary>
        /// Get DropDownList controls of <see cref="Node.Lib.UI.WebControls.GridDropDownListField" /> control list based on DropDownList ID
        /// </summary>
        /// <param name="id">DropDownList control ID</param>
        /// <returns>Controls list of GridDropDownListField</returns>
        public ArrayList GetDropDownListFields(string id)
        {
            ArrayList ary = new ArrayList();

            foreach (GridViewRow row in this.Rows)
            {
                DropDownList c = row.FindControl(id) as DropDownList;
                if (c != null && c.ID == id) ary.Add(c);
            }
            return ary;
        }

        /// <summary>
        /// Get PlaceHolder controls of <see cref="Node.Lib.UI.WebControls.GridContainerField" /> control list based on TextBox ID
        /// </summary>
        /// <param name="id">TextBox control ID</param>
        /// <returns>Controls list of GridContainerField</returns>
        public ArrayList GetContainerFields(string id)
        {
            ArrayList ary = new ArrayList();

            foreach (GridViewRow row in this.Rows)
            {
                PlaceHolder c = row.FindControl(id) as PlaceHolder;
                if (c != null && c.ID == id) ary.Add(c);
            }
            return ary;
        }

        /// <summary>
        /// Generate Excel CSV to Http Response
        /// </summary>
        /// <param name="response"></param>
        public void GenerateExcel(HttpResponse response)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter(CultureInfo.CurrentCulture);
            HtmlTextWriter ohw = new HtmlTextWriter(sw);

            GridView gv = new GridView();
            DataView dv = this.GetCurrentDataView();
            foreach (DataColumn aCol in dv.Table.Columns)
                aCol.ColumnName = aCol.Caption;
            gv.DataSource = dv;
            gv.DataBind();
            gv.RenderControl(ohw);

            //HttpResponse response = this.Page.Response;

            response.Clear();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + this.ExcelFileName);
            response.ContentType = "application/vnd.ms-excel";
            response.Charset = "";
            response.Write(sw.ToString());
            response.Flush();
            response.End();
        }


        /// <summary>
        /// Post back even handler method.
        /// </summary>
        /// <param name="eventArgument">event argument</param>
        public new void RaisePostBackEvent(String eventArgument)
        {
            if (eventArgument == "EXCEL")
            {
                System.IO.StringWriter sw = new System.IO.StringWriter(CultureInfo.CurrentCulture);
                HtmlTextWriter ohw = new HtmlTextWriter(sw);

                GridView gv = new GridView();
                DataView dv = this.GetCurrentDataView();
                foreach (DataColumn aCol in dv.Table.Columns)
                    aCol.ColumnName = aCol.Caption;
                gv.DataSource = dv;
                gv.DataBind();
                gv.RenderControl(ohw);

                HttpResponse response = this.Page.Response;

                response.Clear();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + this.ExcelFileName);
                response.ContentType = "application/vnd.ms-excel";
                response.Charset = "";
                response.Write(sw.ToString());
                response.Flush();
                response.End();
            }

            base.RaisePostBackEvent(eventArgument);
        }

        /// <summary>
        /// Initialize the gridview, usually called in page init event, 
        /// or GenerateDynamicControls() if you inherit EAF.ULib.UI.Base.PageBase
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="viewID"></param>
        public void InitView(GridViewAgent agent, string viewID)
        {
            InitView(agent, viewID, this.Parent);
        }

        /// <summary>
        /// Initialize the gridview, usually called in page init event, 
        /// or GenerateDynamicControls() if you inherit EAF.ULib.UI.Base.PageBase
        /// </summary>
        public void InitView(GridViewAgent agent, string viewID, Control userControl)
        {
            this.gvAgent = agent;

            if (GVView == null || !this.Page.IsPostBack)
            {
                if (this.gvAgent == null)
                    throw new Exception("GridViewAgent initializes error ==> GridViewAgent is NULL.");
                if (viewID == "")
                    throw new Exception("GridViewAgent initializes error ==> ViewID is empty.");

                GVView = this.gvAgent.InitGridView(viewID);

                if (GVView == null)
                    throw new Exception("GridViewAgent initialize error ==> ViewID (" + viewID + ") is not found.");

            }

            GVView.CreateFields(userControl);

            if (GVView.GridViewActionFields.Count > 0)
            {
                for (int i = 0; i < GVView.GridViewActionFields.Count; i++)
                {
                    DataControlField c = (DataControlField)(((GridViewField)GVView.GridViewActionFields[i]).FieldControl);
                    this.Columns.Add(c);
                }
            }

            if (GVView.GridViewDataFields.Count > 0)
            {
                DataTable dt = this.CachedDataTable;
                if (dt != null && dt.Columns.Count > 0)
                {
                    //clear datatable caption.
                    for (int i = 0; i < dt.Columns.Count; i++)
                        dt.Columns[i].Caption = "";
                }
                for (int i = 0; i < GVView.GridViewDataFields.Count; i++)
                {
                    DataControlField c = ((DataControlField)((GridViewField)GVView.GridViewDataFields[i]).FieldControl);
                    this.Columns.Add(c);

                    //change the datatable caption.
                    if (dt != null && dt.Columns.Count > 0)
                    {
                        Hashtable ht = ((Hashtable)((GridViewField)GVView.GridViewDataFields[i]).FieldAttributes);
                        dt.Columns[ht["DataField"] + ""].Caption = ht["HeaderText"] + "";
                    }
                }
            }
        }

        public void ClearView()
        {
            CachedDataTable = null;
            AllCheckedVals.Clear();

            RefreshView(true);
        }

        //***********************************************************************
        // Internal Methods
        //***********************************************************************

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EventPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PageIndex = e.NewPageIndex;
            DataBindView();
        }

        /// <summary>
        /// internal sorting event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EventSorting(object sender, GridViewSortEventArgs e)
        {
            DataBindView();
        }


        protected void DataBindView()
        {
            if (this.CachedDataTable != null)
            {
                DataView dv = new DataView(this.CachedDataTable);
                dv.Sort = GetSortAttributeExpression(SortAttributeList);
                DataSource = dv;
                DataBind();
            }
            else
            {
                DataSource = null;
                DataBind();
            }

            if (EnablePageSortSessionCache)
            {
                SessionCachedSortAttributeList = SortAttributeList;
                SessionCachedPageIndex = PageIndex;
            }
        }

        /// <summary>
        /// Sets and modify sorting attributes
        /// </summary>
        /// <param name="e">GridViewSortEventArgs</param>
        protected void SetSortAttribute(GridViewSortEventArgs e)
        {
            SortAttribute sa = RetrieveSortAttribute(e.SortExpression);

            SortAttribute newsa = new SortAttribute();
            newsa.Field = e.SortExpression;
            if (sa == null)
                newsa.Direction = SortDirection.Ascending;
            else
                newsa.Direction = (sa.Direction == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending);

            if (!this.AllowMultiColumnSorting)
                this.SortAttributeList.Clear();

            SortAttributeList.Insert(0, newsa);
        }

        /// <summary>
        /// Retrieve sort attribute object
        /// </summary>
        /// <param name="field">sort field name</param>
        /// <returns>SortAttribute object</returns>
        protected SortAttribute RetrieveSortAttribute(string field)
        {
            for (int i = 0; i < this.SortAttributeList.Count; i++)
            {
                SortAttribute sa = (SortAttribute)this.SortAttributeList[i];
                if (sa.Field == field)
                {
                    this.SortAttributeList.RemoveAt(i);
                    return sa;
                }
            }
            return null;
        }

        /// <summary>
        /// Get sort attribute object and order no.
        /// </summary>
        /// <param name="field">sort field name</param>
        /// <param name="sortSeq">(out) sort field sequence</param>
        /// <returns>SortAttribute object</returns>
        protected SortAttribute GetSortAttribute(string field, out int sortSeq)
        {
            sortSeq = -1;
            for (int i = 0; i < this.SortAttributeList.Count; i++)
            {
                SortAttribute sa = (SortAttribute)this.SortAttributeList[i];
                if (sa.Field == field)
                {
                    sortSeq = i + 1;
                    return sa;
                }
            }
            return null;
        }

        /// <summary>
        ///  Display a graphic image for the Sort Order along with the sort sequence no.
        /// </summary>
        protected void DisplaySortOrderImages(GridViewRow gvItem)
        {
            if (this.SortAscImageUrl == "" && this.SortDescImageUrl == "")
            {
                this.SortAscImageUrl = ResolveUrl(this.ImageBase) + "icon-arrowup.gif";
                this.SortDescImageUrl = ResolveUrl(this.ImageBase) + "icon-arrowdown.gif";
                /*if (this.DarkHeaderBackground)
                {
                    this.SortAscImageUrl = Page.ClientScript.GetWebResourceUrl(typeof(EAFGridView), "Node.Lib.UI.Resources.img.icon-arrowup_y.gif");
                    this.SortDescImageUrl = Page.ClientScript.GetWebResourceUrl(typeof(EAFGridView), "Node.Lib.UI.Resources.img.icon-arrowdown_y.gif");
                }
                else
                {
                    this.SortAscImageUrl = Page.ClientScript.GetWebResourceUrl(typeof(EAFGridView), "Node.Lib.UI.Resources.img.icon-arrowup.gif");
                    this.SortDescImageUrl = Page.ClientScript.GetWebResourceUrl(typeof(EAFGridView), "Node.Lib.UI.Resources.img.icon-arrowdown.gif");
                }*/
            }

            for (int i = 0; i < gvItem.Cells.Count; i++)
            {
                if (gvItem.Cells[i].Controls.Count > 0 && gvItem.Cells[i].Controls[0] is LinkButton)
                {
                    string column = ((LinkButton)gvItem.Cells[i].Controls[0]).CommandArgument;
                    int sortOrderNo;
                    SortAttribute sa = GetSortAttribute(column, out sortOrderNo);

                    if (sa != null)
                    {
                        string sortImgLoc = (sa.Direction == SortDirection.Ascending ? ResolveUrl(SortAscImageUrl) : ResolveUrl(SortDescImageUrl));

                        if (sortImgLoc != "")
                        {
                            Image imgSortDirection = new Image();
                            imgSortDirection.ImageUrl = sortImgLoc;
                            imgSortDirection.ImageAlign = ImageAlign.AbsMiddle;
                            gvItem.Cells[i].Controls.Add(imgSortDirection);
                        }
                        else
                        {
                            Label lblSortDirection = new Label();
                            //lblSortDirection.Font.Size = FontUnit.Smaller;
                            lblSortDirection.Font.Name = "webdings";
                            lblSortDirection.EnableTheming = false;
                            lblSortDirection.Text = " " + (sa.Direction == SortDirection.Ascending ? "5" : "6");
                            gvItem.Cells[i].Controls.Add(lblSortDirection);
                        }

                        if (AllowMultiColumnSorting)
                        {
                            Label srtSeq = new Label();
                            srtSeq.Font.Size = FontUnit.XXSmall;
                            srtSeq.Text = sortOrderNo.ToString(CultureInfo.CurrentCulture);
                            gvItem.Cells[i].Controls.Add(srtSeq);
                        }

                    }
                }
            }
        }


        //***********************************************************************
        // Override
        //***********************************************************************
        #region Override

        protected override void OnLoad(EventArgs e)
        {
            if (!string.IsNullOrEmpty(RememberAllCheckedValueID) && this.Page.IsPostBack)
            {
                foreach (GridViewRow row in this.Rows)
                {
                    HtmlInputCheckBox chk = row.FindControl(RememberAllCheckedValueID) as HtmlInputCheckBox;
                    if (chk != null) { ResetAllCheckedVals(chk.Value, chk.Checked); }

                    GridRadioButton rdo = row.FindControl(RememberAllCheckedValueID) as GridRadioButton;
                    if (rdo != null) { ResetAllCheckedVals(rdo.Value, rdo.Checked); }
                }
            }

            base.OnLoad(e);
        }

        private void ResetAllCheckedVals(string val, bool isChecked)
        {
            if (AllCheckedVals.Contains(val) && !isChecked)
            {
                AllCheckedVals.Remove(val);
            }
            else if (!AllCheckedVals.Contains(val) && isChecked)
            {
                AllCheckedVals.Add(val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSorting(GridViewSortEventArgs e)
        {
            SetSortAttribute(e);
            if (EnablePageSortSessionCache) { SessionCachedSortAttributeList = SortAttributeList; }
            base.OnSorting(e);
        }

        //protected override void OnDataBound(EventArgs e)
        //{
        //    foreach (DataControlField dfc in this.Columns)
        //    {
        //        object o = this.colCapMapping[dfc.HeaderText];
        //        if (o != null)
        //            dfc.HeaderText = "" + o;
        //    }
        //    base.OnDataBound(e);
        //}


        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {

            if (!string.IsNullOrEmpty(RememberAllCheckedValueID)) // && this.Page.IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HtmlInputCheckBox chk = e.Row.FindControl(RememberAllCheckedValueID) as HtmlInputCheckBox;
                    if (chk != null)
                    {
                        chk.Checked = AllCheckedVals.Contains(chk.Value);
                    }

                    GridRadioButton rdo = e.Row.FindControl(RememberAllCheckedValueID) as GridRadioButton;
                    if (rdo != null)
                    {
                        rdo.Checked = AllCheckedVals.Contains(rdo.Value);
                    }
                }
            }

            base.OnRowDataBound(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // add sort image
                if (this.SortAttributeList.Count > 0)
                    DisplaySortOrderImages(e.Row);

                // found group field
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    TableCell td = e.Row.Cells[i];

                    if (td.Controls.Count > 0)
                    {
                        Control c = td.Controls[0];
                        if (this.GroupField == ((LinkButton)e.Row.Cells[i].Controls[0]).Text)
                            this.groupFieldCellNo = i;
                    }
                    else if (td.Text == this.GroupField)
                    {
                        this.groupFieldCellNo = i;
                    }
                }


            }

            base.OnRowCreated(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.groupFieldCellNo >= 0)
            {
                this.GridLines = GridLines.Both;

                int startRow = 0;
                int endRow = 0;
                string rowData = this.Rows[0].Cells[this.groupFieldCellNo].Text;
                TableItemStyle tis = this.RowStyle;

                for (int i = 0; i < this.Rows.Count; i++)
                {
                    if (rowData != "" && rowData == this.Rows[i].Cells[this.groupFieldCellNo].Text)
                    {
                        endRow = i;
                    }
                    else
                    {
                        if ((endRow - startRow) > 0)
                        {
                            this.Rows[startRow].Cells[this.groupFieldCellNo].RowSpan = (endRow - startRow + 1);
                            this.Rows[startRow].Cells[this.groupFieldCellNo].ApplyStyle(tis);

                            for (int x = startRow + 1; x <= endRow; x++)
                            {
                                this.Rows[x].Cells.RemoveAt(this.groupFieldCellNo);
                            }
                        }

                        startRow = i;
                        endRow = i;
                        rowData = this.Rows[i].Cells[this.groupFieldCellNo].Text;

                        if (tis == this.RowStyle)
                            tis = this.AlternatingRowStyle;
                        else
                            tis = this.RowStyle;
                    }
                }

                if ((endRow - startRow) > 0)
                {
                    this.Rows[startRow].Cells[this.groupFieldCellNo].RowSpan = (endRow - startRow + 1);
                    this.Rows[startRow].Cells[this.groupFieldCellNo].ApplyStyle(tis);
                    for (int x = startRow + 1; x <= endRow; x++)
                    {
                        this.Rows[x].Cells.RemoveAt(this.groupFieldCellNo);
                    }

                }

            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.CachedDataTable != null && this.CachedDataTable.Rows.Count > 0)
            {
                // PageIndex is 0 based
                int total = this.CachedDataTable.Rows.Count;
                int start = this.PageSize * (this.PageIndex) + 1;
                int end = this.PageSize * (this.PageIndex + 1);
                end = (end > total) ? total : end;

                if (this.ShowResultMessage)
                {
                    string temp = String.Format(this.ResultMessageTemplate, "<span style=\"font-weight:bold\">" + start + " - " + end + "</span>", "<span style=\"font-weight:bold\">" + total + "</span>");
                    writer.Write("<div style=\"" + this.ResultMessageStyle + "\">" + temp + "</div>");
                }

            }

            base.Render(writer);


            //writer.Write("<table style=\"width:100%\">");
            //if (this.ShowExcel && this.CachedDataTable != null && this.CachedDataTable.Rows.Count > 0)
            //{
            //    writer.Write("<tr><td align=\"right\">");
            //    string defaultLink = "<a href=\"javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "EXCEL") + "\"";
            //    string defaultEnd = "</a>";
            //    //string cssClassString = " class=\"" + this.ExcelStyle.CssClass + "\"";
            //    string excelImage = "<img src=\"" + ResolveUrl(this.ImageBase) + this.ExcelImageFile + "\" align=\"absmiddle\" border=\"0\"/>";
            //    string resultLink = "";
            //    if (this.ExcelStyle != null && this.ExcelStyle.CssClass != "")
            //    {
            //        resultLink = defaultLink + " class=\"" + this.ExcelStyle.CssClass + "\">";
            //        //writer.Write(defaultLink + cssClassString + "Excel" + defaultEnd);
            //    }
            //    else
            //    {
            //        resultLink = defaultLink + ">";
            //        //writer.Write(defaultLink + "Excel" + defaultEnd);
            //    }
            //    if (this.ExcelImageFile != "")
            //    {
            //        resultLink += excelImage;
            //    }
            //    if (this.ExcelText != "")
            //    {
            //        resultLink += ExcelText;
            //    }
            //    resultLink += defaultEnd;
            //    writer.Write(resultLink);
            //    writer.Write("</td></tr>");
            //}
            //writer.Write("<tr><td>");
            //base.Render(writer);
            //writer.Write("</td></tr></table>");

        }

        #endregion



    }
}
