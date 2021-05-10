using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

using Node.Lib.UI.WebControls;
using Node.Lib.Data;
using System.IO;
using Node.Core.Biz.Objects;
using Node.Lib.Utility;
using System.Xml.Xsl;
using System.Xml;
using Node.Core;

public partial class Pages_DataViewer_DataViewer : Node.Core.UI.Base.AdminPageBase
{
    private DataViewerConfiguration _PDConfig = null;
    private List<string> _deletesqlstatement = new List<string>();
    private const string _dataflow = "DataFlow";
    private const string _dataflowtable = "DataFlowTable";
    private const string _DataProvider = "DataProvider";
    private const string _DataConStr = "DataConStr";
    private const string _TransID = "TransID";
    private const string _ListOfID = "ListOfID";
    private const string _KeyField = "KeyField";
    private const string _TRANSFORMRESULT = "TRANSFORMRESULT";


    protected void Page_Init(object sender, EventArgs e)
    {
        egvData.AutoGenerateColumns = true;
        //egvData.AllowPaging = false;
        egvDataDetail.AutoGenerateColumns = true;
        //btnDelete.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitControl();
            ViewState.Add(_dataflow, "");
            ViewState.Add(_dataflowtable, "");
            ViewState.Add(_TransID, Request.QueryString["TransID"]);
            ViewState.Add(_ListOfID, new List<string>());
            ViewState.Add(_KeyField, 0);

            if (this.DropDataFlow.SelectedValue != "")
                ViewState[_dataflow] = this.DropDataFlow.SelectedValue;
            if (this.DropTables.SelectedValue != "")
                ViewState[_dataflowtable] = this.DropTables.SelectedValue;
        }
        else
        {
            GetCheckBoxID();
            egvData.CachedDataTable = egvData.CachedDataTable;
            egvData.DataBind();

            if (ViewState[_dataflow].ToString() != this.DropDataFlow.SelectedValue)
            {
                ViewState[_dataflow] = this.DropDataFlow.SelectedValue;
                DropDataFlow_OnSelectedIndexChanged(this.DropDataFlow, null);
                DropTables_OnSelectedIndexChanged(this.DropTables, null);
                ViewState[_dataflowtable] = "";
            }
            else
            {
                ViewState[_dataflowtable] = this.DropTables.SelectedValue;
            }
        }

        this.BuildDataFilter();

        if (ViewState[_TransID] != null && ViewState[_TransID].ToString() != "")
        {
            this.sec2.Caption = "Data Filter by Transaction ID:" + ViewState["TransID"];
        }
    }

    protected void egvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string[] cmd = e.CommandName.Split(':');
        _PDConfig = new DataViewerConfiguration();
        XElement xTable = _PDConfig.GetTableByDataFlow(this.ViewState[_dataflow].ToString(), this.ViewState[_dataflowtable].ToString());
        DBAdapter db = new DBAdapter(ViewState[_DataProvider].ToString(), ViewState[_DataConStr].ToString());
        string sSql = "";
        DataSet ds = new DataSet();
        switch (cmd[0])
        {
            case "ChildTable":
                IEnumerable<XElement> xCTables = xTable.Descendants("ChildTable").Where(x => x.Element("Column").Value == cmd[1]).Select(x => x);
                XElement xCTable = xCTables.First<XElement>();


                sSql = xCTable.Element("SQL").Value + xCTable.Element("SQLSuffix").Value;
                sSql = sSql.Replace("@" + cmd[1] + "@", e.CommandArgument.ToString());

                ds = new DataSet();
                db.GetDataSet("Table", sSql, ds);

                egvDataDetail.CachedDataTable = ds.Tables[0];
                egvDataDetail.DataBind();

                ModalChildTable.Show();
                break;
            case "Trans":
                IEnumerable<XElement> xTrans = xTable.Descendants("Transform").Where(x => x.Element("Column").Value == cmd[1]).Select(x => x);
                XElement xTran = xTrans.First<XElement>();

                sSql = xTran.Element("SQL").Value + xTran.Element("SQLSuffix").Value;
                sSql = sSql.Replace("@" + cmd[1] + "@", e.CommandArgument.ToString());

                ds = new DataSet();
                db.GetDataSet("Table", sSql, ds);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow aRow = ds.Tables[0].Rows[0];
                    if (aRow[0] != DBNull.Value && aRow[1] != DBNull.Value)
                    {
                        string sXML = "";
                        byte[] content = null;
                        if (aRow[0].ToString().ToUpper() == "ZIP")
                        {
                            WinZip wp = new WinZip();
                            Hashtable ht = wp.ExtractZip((byte[])aRow[1]);
                            if (ht.Keys.Count > 0)
                            {
                                foreach (string key in ht.Keys)
                                {
                                    content = (byte[])ht[key];
                                    break;
                                }
                            }
                        }
                        else if (aRow[0].ToString().ToUpper() == "XML")
                        {
                            content = aRow[1] as byte[];
                        }

                        if (content != null)
                        {
                            MemoryStream ms = new MemoryStream(content);
                            StreamReader sr = new StreamReader(ms);
                            ms.Position = 0;
                            sXML = sr.ReadToEnd();
                            ms.Close();
                            sr.Close();

                            if (xTran.Element("XSLT") != null && xTran.Element("XSLT").Value != "")
                            {
                                sr = new StreamReader(Request.PhysicalApplicationPath + xTran.Element("XSLT").Value);
                                string strXslt = sr.ReadToEnd();

                                XslCompiledTransform transformer = new XslCompiledTransform();
                                XmlTextReader r = new XmlTextReader(new StringReader(strXslt));
                                transformer.Load(r);

                                XmlTextReader reader = new XmlTextReader(new StringReader(sXML));
                                StringWriter stringWriter = new StringWriter();
                                XmlTextWriter writer = new XmlTextWriter(stringWriter);

                                transformer.Transform(reader, writer);
                                string contents = stringWriter.ToString();
                                Session[_TRANSFORMRESULT] = contents;

                            }
                            else
                            {
                                Session[_TRANSFORMRESULT] = sXML;
                            }

                            string url = Request.Url.OriginalString;
                            url = url.Replace("DataViewer/DataViewer.aspx", "OperationManager/ViewDocument.aspx");
                            string myScript = "window.open('" + url + "',null,'height=600,width=800,status=yes,toolbar=no,menubar=no,scrollbars=yes');";
                            //myScript = "alert('hello');";
                            if (!Page.ClientScript.IsClientScriptBlockRegistered("OpenWindows"))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "OpenWindows", myScript, true);
                            }

                        }

                    }

                }

                break;
            case "Downloads":
                IEnumerable<XElement> xDownloads = xTable.Descendants("Download").Where(x => x.Element("Column").Value == cmd[1]).Select(x => x);
                XElement xDownload = xDownloads.First<XElement>();

                sSql = xDownload.Element("SQL").Value + xDownload.Element("SQLSuffix").Value;
                sSql = sSql.Replace("@" + cmd[1] + "@", e.CommandArgument.ToString());

                ds = new DataSet();
                db.GetDataSet("Table", sSql, ds);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0][0] != DBNull.Value)
                {
                    DataRow aRow = ds.Tables[0].Rows[0];
                    byte[] content = aRow[1] as byte[];

                    if (content != null)
                    {
                        Response.Clear();
                        //Response.ContentType = "application/zip";
                        Response.AppendHeader("content-disposition", "attachment; filename=" + "Download." + aRow[0]);
                        Response.OutputStream.Write(content, 0, content.Length);
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        this.MsgDtl.InnerHtml = "The downloaded file is not available.";
                        mdlPopup.Show();
                    }

                }

                break;
        }

    }

    protected void egvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = e.Row.DataItem as DataRowView;
            _PDConfig = new DataViewerConfiguration();
            XElement xTable = _PDConfig.GetTableByDataFlow(this.ViewState[_dataflow].ToString(), this.ViewState[_dataflowtable].ToString());
            if (xTable != null)
            {
                IEnumerable<XElement> xCTable = xTable.Descendants("ChildTable");
                foreach (XElement xe in xCTable)
                {
                    Button btn = new Button();
                    btn.CommandName = "ChildTable:" + xe.Element("Column").Value;
                    btn.CommandArgument = row[xe.Element("Column").Value].ToString();
                    int i = row.DataView.Table.Columns.IndexOf(xe.Element("Column").Value);
                    btn.Text = "View " + xe.Element("Column").Value;
                    e.Row.Cells[i + 1].Controls.Add(btn);
                }

                IEnumerable<XElement> xTrans = xTable.Descendants("Transform");
                foreach (XElement xe in xTrans)
                {
                    Button btn = new Button();
                    btn.CommandName = "Trans:" + xe.Element("Column").Value;
                    btn.CommandArgument = row[xe.Element("Column").Value].ToString();
                    int i = row.DataView.Table.Columns.IndexOf(xe.Element("Column").Value);
                    btn.Text = "View " + xe.Element("Column").Value;
                    e.Row.Cells[i + 1].Controls.Add(btn);
                }

                IEnumerable<XElement> xDownloads = xTable.Descendants("Download");
                foreach (XElement xe in xDownloads)
                {
                    Button btn = new Button();
                    btn.CommandName = "Downloads:" + xe.Element("Column").Value;
                    btn.CommandArgument = row[xe.Element("Column").Value].ToString();
                    int i = row.DataView.Table.Columns.IndexOf(xe.Element("Column").Value);
                    btn.Text = "Download " + xe.Element("Column").Value;
                    e.Row.Cells[i + 1].Controls.Add(btn);
                }

                IEnumerable<XElement> xMap = xTable.Descendants("URLs");
                foreach (XElement xe in xMap)
                {

                    string sLate = "" + row[xe.Element("LatColumn").Value];
                    string sLong = "" + row[xe.Element("LongColumn").Value];
                    HyperLink btn = new HyperLink();
                    btn.NavigateUrl = "https://edep.dep.mass.gov/gis/GISLocatorWPts.aspx?Editable=Yes&Lat=" + sLate + "&Lon=" + sLong;
                    btn.Text = xe.Element("Label").Value;
                    btn.Target = "_Blank";
                    int i = row.DataView.Table.Columns.IndexOf(xe.Element("Column").Value);
                    e.Row.Cells[i + 1].Controls.Add(btn);
                }

            }

            if (((List<string>)ViewState[_ListOfID]).Count > 0 && ((int)ViewState[_KeyField]) != 0)
            {
                if (((List<string>)ViewState[_ListOfID]).Contains(e.Row.Cells[(int)ViewState[_KeyField]].Text))
                {
                    CheckBox chk = e.Row.Cells[0].FindControl("checked") as CheckBox;
                    chk.Checked = true;
                }
            }


        }
    }

    private void InitControl()
    {
        _PDConfig = new DataViewerConfiguration();

        IEnumerable<XElement> dataflows = _PDConfig.DataFlowCollection;

        foreach (XElement dataflow in dataflows)
        {
            DropDataFlow.Items.Add(new ListItem(dataflow.Attribute("Name").Value, dataflow.Attribute("Name").Value));
        }

        DropDataFlow.SelectedIndex = 0;

        DropDataFlow.DataBind();

        DropDataFlow_OnSelectedIndexChanged(null, null);
    }

    protected void DropDataFlow_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropTables.Items.Clear();

        _PDConfig = new DataViewerConfiguration();
        IEnumerable<XElement> tables = _PDConfig.GetTablesByDataFlow(DropDataFlow.SelectedItem.Value);
        foreach (XElement table in tables)
        {
            DropTables.Items.Add(new ListItem(table.Element("Name").Value, table.Element("Name").Value));
        }
        DropTables.Items.Insert(0, "");
        if (DropTables.Items.Count == 0)
        {
            btnSearch.Enabled = false;
            //btnDelete.Enabled = false;
            btnExport.Enabled = false;
        }
        else
        {
            btnSearch.Enabled = true;
            //btnDelete.Enabled = true;
            btnExport.Enabled = true;
        }


        List<string> listCnnStr = _PDConfig.GetConnectionStringByDataFlow(this.DropDataFlow.SelectedValue);
        ViewState[_DataProvider] = listCnnStr[0];
        ViewState[_DataConStr] = listCnnStr[1];
        ViewState[_ListOfID] = new List<string>();
    }

    protected void DropTables_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        egvData.CachedDataTable = null;
        egvData.DataBind();
        this.chklistColumns.Items.Clear();

        ViewState[_ListOfID] = new List<string>();
        //this.BuildDataFilter();
        //if (Session["table"] == null)
        //{
        //    Session.Add("table", "");
        //}
        //Session["table"] = this.DropTables.SelectedItem.Value;
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        msgNoRecordFound.Visible = false;

        if (this.DropTables.SelectedItem.Value.Equals(""))
            return;

        DBAdapter db = new DBAdapter(ViewState[_DataProvider].ToString(), ViewState[_DataConStr].ToString());

        string sWhere = this.BuildWhereSQL((db.ProviderName == Node.Lib.Data.DBAdapter.Oracle_Provider ? true : false));
        //Response.Write(sWhere+"<br>");

        _PDConfig = new DataViewerConfiguration();

        XElement xTable = _PDConfig.GetTableByDataFlow(DropDataFlow.SelectedItem.Value, DropTables.SelectedItem.Value);

        string sSql = xTable.Element("SQL").Value;

        if (sWhere != "")
        {
            if (!sSql.ToLower().Contains("where"))
            {
                sSql = sSql + " where " + sWhere;
            }
            else
            {
                sSql = sSql + " and " + sWhere;
            }
        }

        if (xTable.Element("SQLSuffix") != null)
        {
            sSql = sSql + xTable.Element("SQLSuffix").Value;
        }

        DataSet ds = new DataSet();
        db.GetDataSet(this.DropTables.SelectedItem.Value, sSql, ds);

        DataView dv = new DataView(ds.Tables[0]);
        if (ViewState["TransID"] != null
            && ViewState["TransID"].ToString() != ""
            && dv.Table.Columns.Contains("TRANSACTION_ID"))
        {
            dv.RowFilter = "TRANSACTION_ID='" + ViewState[_TransID] + "'";
        }

        egvData.CachedDataTable = dv.ToTable();
        egvData.DataBind();

        this.chklistColumns.Items.Clear();
        foreach (DataColumn col in ds.Tables[0].Columns)
        {
            ListItem newItem = new ListItem(col.ColumnName);
            newItem.Selected = true;
            this.chklistColumns.Items.Add(newItem);
        }

        if (!xTable.Element("Key").Value.Equals(""))
        {
            int i = 1;
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                if (col.ColumnName == xTable.Element("Key").Value)
                {
                    break;
                }
                i++;
            }
            ViewState[_KeyField] = i;
        }

        if (egvData.CachedDataTable.Rows.Count == 0)
        {
            msgNoRecordFound.Visible = true;
        }

    }

    protected void btnSelection_OnClick(object sender, EventArgs e)
    {
        string sKeyField = "";
        string sKeyValue = "";
        if (egvData.CachedDataTable != null)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter ohw = new HtmlTextWriter(sw);

            GridView gv = new GridView();
            DataView dv = new DataView(egvData.CachedDataTable.Copy());

            foreach (DataColumn aCol in dv.Table.Columns)
                aCol.ColumnName = aCol.Caption;

            List<string> listkey = ViewState[_ListOfID] as List<string>;
            foreach (string s in listkey)
                sKeyValue = sKeyValue + "'" + s + "',";

            if (!sKeyValue.Equals(""))
            {
                _PDConfig = new DataViewerConfiguration();
                XElement xTable = _PDConfig.GetTableByDataFlow(DropDataFlow.SelectedItem.Value, DropTables.SelectedItem.Value);
                sKeyField = xTable.Element("Key").Value;

                dv.RowFilter = sKeyField + " in (" + sKeyValue.Substring(0, sKeyValue.Length - 1) + ")";
            }

            foreach (ListItem aItem in chklistColumns.Items)
            {
                if (dv.Table.Columns.Contains(aItem.Text))
                {
                    if (!aItem.Selected)
                        dv.Table.Columns.Remove(aItem.Text);
                }
            }

            gv.DataSource = dv;
            gv.DataBind();
            gv.RenderControl(ohw);

            sw.GetStringBuilder().Insert(0, @"<style>td {mso-number-format:\@;}</style>");

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=Export.xls");
            Response.ContentType = "application/octet-stream";

            Response.Charset = "";
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void btnBackToOp_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/OperationManager/ManageOperation.aspx");
    }

    private List<string> GetChildTableKeys(XElement xParent, XElement xChild, List<string> IDs)
    {
        List<string> ids = new List<string>();

        DBAdapter db = new DBAdapter(ViewState[_DataProvider].ToString(), ViewState[_DataConStr].ToString());
        string sSql = " select " + xChild.Element("Key").Value + " from " + xChild.Element("Name").Value;
        sSql = sSql + " where " + xParent.Element("Key").Value + " in (" + BuildSelectInSQL(IDs) + ")";
        DataSet ds = new DataSet();
        db.GetDataSet(xChild.Element("Name").Value, sSql, ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow aRow in ds.Tables[0].Rows)
            {
                ids.Add(aRow[0].ToString());
            }
        }
        return ids;
    }

    private void BuildDataFilter()
    {
        if (!this.ViewState[_dataflow].ToString().Equals("") && !this.ViewState[_dataflowtable].ToString().Equals(""))
        {
            DataFilterTable.Controls.Clear();

            _PDConfig = new DataViewerConfiguration();
            XElement xTable = _PDConfig.GetTableByDataFlow(this.ViewState[_dataflow].ToString(), this.ViewState[_dataflowtable].ToString());
            if (xTable != null)
            {
                XElement xFilters = xTable.Element("Filters");
                if (xFilters != null)
                {
                    IEnumerable<XElement> xFilter = xFilters.Elements("Filter");
                    foreach (XElement xe in xFilter)
                    {
                        FormInputField lbl = new FormInputField();
                        lbl.FieldName = xe.Element("Column").Value;
                        if (xe.Element("Label") != null)
                        {
                            lbl.FieldName = xe.Element("Label").Value;
                        }

                        switch (xe.Element("ControlType").Value)
                        {
                            case "DropDown":
                                DropDownList drp = new DropDownList();
                                drp.ID = xe.Element("Control").Value;
                                DBAdapter db = new DBAdapter(ViewState[_DataProvider].ToString(), ViewState[_DataConStr].ToString());
                                string sSql = xe.Element("SQL").Value;
                                //sSql = sSql.Replace("@USER@", Session[Phrase.SESSION_USER_ID].ToString());
                                DataSet ds = new DataSet();
                                db.GetDataSet(this.DropTables.SelectedItem.Value, sSql, ds);
                                drp.DataSource = ds.Tables[0];
                                if (ds.Tables[0].Columns.Count == 2)
                                {
                                    drp.DataTextField = ds.Tables[0].Columns[0].ColumnName;
                                    drp.DataValueField = ds.Tables[0].Columns[1].ColumnName;
                                }
                                else
                                {
                                    drp.DataTextField = ds.Tables[0].Columns[0].ColumnName;
                                    drp.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                                }
                                drp.DataBind();
                                drp.Items.Insert(0, "");
                                lbl.Controls.Add(drp);
                                break;
                            case "Date":
                                DatePicker dtp = new DatePicker();
                                dtp.ID = xe.Element("Control").Value + "1";
                                lbl.Controls.Add(dtp);
                                dtp = new DatePicker();
                                dtp.ID = xe.Element("Control").Value + "2";
                                lbl.Controls.Add(dtp);
                                break;
                            case "Text":
                                TextBox txt = new TextBox();
                                txt.ID = xe.Element("Control").Value;
                                lbl.Controls.Add(txt);
                                break;
                        }
                        DataFilterTable.Controls.Add(lbl);
                    }
                }
            }
        }
    }

    private string BuildWhereSQL(bool oracle)
    {
        string sResult = "";

        _PDConfig = new DataViewerConfiguration();
        XElement xTable = _PDConfig.GetTableByDataFlow(DropDataFlow.SelectedItem.Value, DropTables.SelectedItem.Value);
        XElement xFilters = xTable.Element("Filters");
        if (xFilters != null)
        {
            IEnumerable<XElement> xFilter = xFilters.Elements("Filter");
            Control ctl = null;
            foreach (XElement xe in xFilter)
            {
                if (!oracle)
                {
                    switch (xe.Element("ControlType").Value)
                    {
                        case "DropDown":
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value);
                            if (ctl != null)
                            {
                                DropDownList drp = ctl as DropDownList;
                                if (drp.SelectedValue != "")
                                    sResult = sResult + " and [" + xe.Element("Column").Value + "] = '" + drp.SelectedValue + "'";
                            }
                            break;
                        case "Date":
                            DatePicker dtp1 = null;
                            DatePicker dtp2 = null;
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value + "1");
                            if (ctl != null)
                                dtp1 = ctl as DatePicker;
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value + "2");
                            if (ctl != null)
                                dtp2 = ctl as DatePicker;

                            if (dtp1.Text != "" && dtp2.Text != "")
                                sResult = sResult + " and [" + xe.Element("Column").Value + "] >= '" + dtp1.Text + "' and [" + xe.Element("Column").Value + "] <= '" + dtp2.Text + "'";
                            if (dtp1.Text != "" && dtp2.Text == "")
                                sResult = sResult + " and [" + xe.Element("Column").Value + "] >= '" + dtp1.Text + "'";
                            if (dtp1.Text == "" && dtp2.Text != "")
                                sResult = sResult + " and [" + xe.Element("Column").Value + "] <= '" + dtp2.Text + "'";

                            break;
                        case "Text":
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value);
                            if (ctl != null)
                            {
                                TextBox txt = ctl as TextBox;
                                if (txt.Text != "")
                                {
                                    sResult = sResult + " and [" + xe.Element("Column").Value + "] like '" + txt.Text + "%'";
                                }
                            }
                            break;
                    }
                }
                else
                {
                    switch (xe.Element("ControlType").Value)
                    {
                        case "DropDown":
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value);
                            if (ctl != null)
                            {
                                DropDownList drp = ctl as DropDownList;
                                if (drp.SelectedValue != "")
                                    sResult = sResult + " and " + xe.Element("Column").Value + " = '" + drp.SelectedValue + "'";
                            }
                            break;
                        case "Date":
                            DatePicker dtp1 = null;
                            DatePicker dtp2 = null;
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value + "1");
                            if (ctl != null)
                                dtp1 = ctl as DatePicker;
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value + "2");
                            if (ctl != null)
                                dtp2 = ctl as DatePicker;

                            if (dtp1.Text != "" && dtp2.Text != "")
                                sResult = sResult + " and " + xe.Element("Column").Value + " >= to_date('" + dtp1.Text + "','mm/dd/yyyy') and " + xe.Element("Column").Value + " <= to_date('" + dtp2.Text + "','mm/dd/yyyy')";
                            if (dtp1.Text != "" && dtp2.Text == "")
                                sResult = sResult + " and " + xe.Element("Column").Value + " >= to_date('" + dtp1.Text + "','mm/dd/yyyy')";
                            if (dtp1.Text == "" && dtp2.Text != "")
                                sResult = sResult + " and " + xe.Element("Column").Value + " <= to_date('" + dtp2.Text + "','mm/dd/yyyy')";

                            break;
                        case "Text":
                            ctl = DataFilterTable.FindControl(xe.Element("Control").Value);
                            if (ctl != null)
                            {
                                TextBox txt = ctl as TextBox;
                                if (txt.Text != "")
                                {
                                    sResult = sResult + " and upper(" + xe.Element("Column").Value + ") like upper('" + txt.Text + "%')";
                                }
                            }
                            break;
                    }
                }
            }
        }
        if (sResult.Length > 0)
            sResult = sResult.Substring(4);
        return sResult;
    }

    private string BuildSelectInSQL(List<string> IDs)
    {
        string skey = "";
        foreach (string str in IDs)
        {
            skey = skey + str + ",";
        }
        if (skey.Length > 0)
            skey = skey.Substring(0, skey.Length - 1);
        return skey;
    }

    private void GetCheckBoxID()
    {
        List<string> listID = ViewState[_ListOfID] as List<string>;
        if ((int)ViewState[_KeyField] != 0)
        {
            foreach (GridViewRow aRow in egvData.Rows)
            {
                CheckBox chk = aRow.Cells[0].FindControl("checked") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    string id = "" + aRow.Cells[(int)ViewState[_KeyField]].Text;
                    if (!listID.Contains("" + aRow.Cells[(int)ViewState[_KeyField]].Text))
                        listID.Add("" + aRow.Cells[(int)ViewState[_KeyField]].Text);
                }
            }
            ViewState[_ListOfID] = listID;
        }
    }




    //private void DeleteTable(string sDataFlow, XElement xeTable, List<string> IDs)
    //{
    //    IEnumerable<XElement> tables = _PDConfig.GetCascadeTables(xeTable);

    //    foreach (XElement xe in tables)
    //    {
    //        if (_PDConfig.IsTableHasChild(sDataFlow, xe.Value))
    //        {
    //            XElement xeTable2 = _PDConfig.GetTableByDataFlow(sDataFlow, xe.Value);
    //            List<string> ids = GetChildTableKeys(xeTable, xeTable2, IDs);
    //            if (ids.Count > 0)
    //            {
    //                DeleteTable(sDataFlow, xeTable2, ids);
    //            }
    //        }
    //        else
    //        {
    //            BuildDeleteSQL(xe.Value, xeTable.Element("Key").Value, IDs);
    //        }
    //    }

    //    BuildDeleteSQL(xeTable.Element("Name").Value, xeTable.Element("Key").Value, IDs);

    //}
    //private void BuildDeleteSQL(string sTable, string sKey, List<string> IDs)
    //{
    //    string skey = "";
    //    foreach (string str in IDs)
    //    {
    //        skey = skey + str + ",";
    //    }
    //    if (skey.Length > 0)
    //    {
    //        skey = skey.Substring(0, skey.Length - 1);

    //        string sSql = " delete " + sTable + " where " + sKey + " in (" + skey + ")";
    //        //ListTables.Items.Add(new ListItem(sSql));
    //        _deletesqlstatement.Add(sSql);
    //    }
    //}
    //protected void btnDelete_OnClick(object sender, EventArgs e)
    //{
    //    //ListTables.Items.Clear();
    //    _deletesqlstatement.Clear();

    //    _PDConfig = new DataViewerConfiguration();
    //    string sDataFlow = "WQX";
    //    string sTable = this.DropTables.SelectedItem.Text;
    //    XElement xeTable = _PDConfig.GetTableByDataFlow(sDataFlow, sTable);
    //    List<string> IDs = new List<string>();

    //    foreach (GridViewRow aRow in egvData.Rows)
    //    {
    //        CheckBox chk = aRow.Cells[0].FindControl("checked") as CheckBox;
    //        if (chk != null && chk.Checked)
    //        {
    //            IDs.Add(aRow.Cells[1].Text);
    //        }
    //    }

    //    DeleteTable(sDataFlow, xeTable, IDs);
    //    ExecuteDeleteStatement();
    //    btnSearch_OnClick(null, null);
    //}
}
