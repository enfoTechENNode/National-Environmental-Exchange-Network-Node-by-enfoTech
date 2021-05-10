using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;

using Node.Lib.Utility;

using Node.Core.API;
using Node.Core.Biz.Objects;
using Node.Core.Util;
using Node.Core.Biz.Handler;

public partial class Query : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;
    protected int ParameterCount = 30;
    protected int ParameterCount2 = 30;

    public Query()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        NodeTab1.SetPageAjaxTabControl = this.TabContainer1;
        if (!this.IsPostBack)
        {
            this.ShowResult = false;
            this.ShowResult2 = false;

            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken.Text = token;
                this.txtToken2.Text = token;
            }
            ArrayList list = Operation.RetrieveQueryOperationNames(BaseHandler.NodeVer.VER_11.ToString());
            list.Insert(0, "");
            this.ddlRequest.Items.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                this.ddlRequest.Items.Add(new ListItem("" + list[i], "" + list[i]));
            }

            list = Operation.RetrieveQueryOperationNames(BaseHandler.NodeVer.VER_20.ToString());
            list.Insert(0, "");

            this.ddlRequest2.Items.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                this.ddlRequest2.Items.Add(new ListItem("" + list[i], "" + list[i]));
            }

        }
        this.ClientLeftPanel.HighLighter(6);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void ddlRequest_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtQueryResult.Text = "";
        this.btnDownloadPanel.Visible = false;
        this.dcpDynamicParams.Controls.Clear();

        if (this.ddlRequest.SelectedValue.Trim().Equals(""))
        {
            this.ParameterCount = 0;
        }
        else
        {
            try
            {
                Hashtable pairs = Operation.RetrieveQueryParameterNames();
                string hashValue = "" + pairs[this.ddlRequest.SelectedValue];
                if (hashValue != null && !hashValue.Trim().Equals(""))
                {
                    string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    if (split != null && split.Length > 0)
                    {
                        this.ParameterCount = split.Length;
                        //loop parameters
                        for (int i = 0; i < split.Length; i++)
                        {
                            LiteralControl lcPrefix = new LiteralControl();
                            lcPrefix.ID = "lcPrefix" + i;
                            dcpDynamicParams.Controls.Add(lcPrefix);
                            lcPrefix.Text = "<tr class=\"alt1\" valign=\"top\"><td></td><td>" + split[i] + "<br />";
                            TextBox txtParam = new TextBox();
                            txtParam.ID = "txtParam" + i;
                            dcpDynamicParams.Controls.Add(txtParam);
                            txtParam.Text = "";
                            txtParam.Width = 300;
                            LiteralControl lcPostfix = new LiteralControl();
                            lcPostfix.ID = "lcPostfix" + i;
                            dcpDynamicParams.Controls.Add(lcPostfix);
                            lcPostfix.Text = "</td></tr>";
                        }
                    }
                    else
                        this.ParameterCount = 0;
                }
                else
                    this.ParameterCount = 0;
            }
            catch(Exception ex)
            {
                //this.txtQueryResult.Text = "System error." + Environment.NewLine + ex.ToString();
                this.txtQueryResult.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        //save to session
        this.Session.Add("ParamCount", this.ParameterCount);
        this.TabContainer1.ActiveTabIndex = 0;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string queryResult = "";

        this.txtQueryResult.Text = "";
        this.btnDownloadPanel.Visible = false;
        this.ShowResult = true;
        string url = this.NodeURLsPanel.GetNodeURL();

        if (url != null)
        {
            try
            {
                this.SaveNodeURL(url);
                NodeRequestor requestor = new NodeRequestor(url);
                string request = this.ddlRequest.SelectedValue;
                if (request == null || request.Trim().Equals(""))
                    request = this.txtRequest.Text;
                if (this.txtRowID.Text.Trim() == String.Empty)
                    this.txtRowID.Text = "0";
                if (this.txtMaxRows.Text.Trim() == String.Empty)
                    this.txtMaxRows.Text = "-1";
                queryResult = requestor.Query(this.txtToken.Text, request, 
                    this.txtRowID.Text, this.txtMaxRows.Text, this.GetParameters());
                this.btnDownloadPanel.Visible = true;
            }
            catch (Exception ex)
            {
                //queryResult = "System error." + Environment.NewLine + ex.ToString();
                queryResult = "System error." + Environment.NewLine + ex.Message;
                this.btnDownloadPanel.Visible = false;
            }
        }
        else
        {
            queryResult = "Please Enter a Node URL";
            this.btnDownloadPanel.Visible = false;
        }
        this.ddlRequest_SelectedIndexChanged(null, null);
        this.txtQueryResult.Text = NodeXMLResult.GetWellFormatXML(queryResult);

        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    private string[] GetParameters()
    {
        string[] retParams = null;
        int totalParamCount = this.dcpDynamicParams.Controls.Count / 3;
        if (totalParamCount > 0)
        {
            retParams = new string[totalParamCount];
            for (int i = 0; i < totalParamCount; i++)
                retParams[i] = ((TextBox)this.dcpDynamicParams.FindControl("txtParam" + i)).Text.Trim();
        }
        return retParams;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string filename = this.ddlRequest.SelectedValue + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss_tt");
        ASCIIEncoding ae = new ASCIIEncoding();
        Hashtable ht = new Hashtable();

        if (this.txtQueryResult.Text != null && this.txtQueryResult.Text != "")
        {
            ht.Add(filename + ".xml", ae.GetBytes(this.txtQueryResult.Text));
            WinZip wz = new WinZip();
            byte[] content = wz.CreateZip(ht);

            this.Response.Clear();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Length", content.Length.ToString());
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");
            this.Response.BinaryWrite(content);
            this.Response.End();
        }
        else
            this.txtQueryResult.Text = "No record found.";

        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    private string[] GetParameters2()
    {
        string[] retParams = null;
        int totalParamCount = this.dcpDynamicParams2.Controls.Count / 3;
        if (totalParamCount > 0)
        {
            retParams = new string[totalParamCount];
            for (int i = 0; i < totalParamCount; i++)
                retParams[i] = ((TextBox)this.dcpDynamicParams2.FindControl("txtParam" + i)).Text.Trim();
        }
        return retParams;
    }

    private string[] GetParameters2_Name()
    {
        string[] retParams = null;

        Hashtable pairs = Operation.RetrieveQueryParameterNames();
        string hashValue = "" + pairs[this.ddlRequest2.SelectedValue];
        if (hashValue != null && !hashValue.Trim().Equals(""))
        {
            string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (split != null && split.Length > 0)
            {
                retParams = new string[ split.Length];
                //loop parameters
                for (int i = 0; i < split.Length; i++)
                {
                    retParams[i] = split[i];
                }
            }
        }
        return retParams;
    }

    protected void ddlRequest2_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.txtQueryResult2.Text = "";
        this.btnDownloadPanel2.Visible = false;
        this.dcpDynamicParams2.Controls.Clear();

        if (this.ddlRequest2.SelectedValue.Trim().Equals(""))
        {
            this.ParameterCount2 = 0;
        }
        else
        {
            try
            {
                Hashtable pairs = Operation.RetrieveQueryParameterNames();
                string hashValue = "" + pairs[this.ddlRequest2.SelectedValue];
                if (hashValue != null && !hashValue.Trim().Equals(""))
                {
                    string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    if (split != null && split.Length > 0)
                    {
                        this.ParameterCount2 = split.Length;
                        //loop parameters
                        for (int i = 0; i < split.Length; i++)
                        {
                            LiteralControl lcPrefix = new LiteralControl();
                            lcPrefix.ID = "lcPrefix" + i;
                            dcpDynamicParams2.Controls.Add(lcPrefix);
                            lcPrefix.Text = "<tr class=\"alt1\" valign=\"top\"><td></td><td>" + split[i] + "<br />";
                            TextBox txtParam = new TextBox();
                            txtParam.ID = "txtParam" + i;
                            dcpDynamicParams2.Controls.Add(txtParam);
                            txtParam.Text = "";
                            txtParam.Width = 300;
                            LiteralControl lcPostfix = new LiteralControl();
                            lcPostfix.ID = "lcPostfix" + i;
                            dcpDynamicParams2.Controls.Add(lcPostfix);
                            lcPostfix.Text = "</td></tr>";
                        }
                    }
                    else
                        this.ParameterCount2 = 0;
                }
                else
                    this.ParameterCount2 = 0;
            }
            catch (Exception ex)
            {
                //this.txtQueryResult.Text = "System error." + Environment.NewLine + ex.ToString();
                this.txtQueryResult.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        //save to session
        this.Session.Add("ParamCount2", this.ParameterCount2);
        this.TabContainer1.ActiveTabIndex = 1;
    }

    protected void btnQuery2_Click(object sender, EventArgs e)
    {
        string queryResult = "";

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
                req.Timeout = 600000000;
                
                string request = this.ddlRequest2.SelectedValue;
                if (request == null || request.Trim().Equals(""))
                    request = this.txtRequest2.Text;
                if (this.txtRowID2.Text.Trim() == String.Empty)
                    this.txtRowID2.Text = "0";
                if (this.txtMaxRows2.Text.Trim() == String.Empty)
                    this.txtMaxRows2.Text = "-1";

                Node.Core2.Requestor.Query qry = new Node.Core2.Requestor.Query();
                qry.dataflow = this.txtDataFlow.Text;
                qry.request = request;
                qry.securityToken = this.txtToken2.Text;
                qry.rowId = this.txtRowID2.Text;
                qry.maxRows = this.txtMaxRows2.Text;

                Node.Core2.Requestor.ParameterType[] parameters = new Node.Core2.Requestor.ParameterType[this.ParameterCount2];
                string[] ParaName = this.GetParameters2_Name();
                string[] ParaValue = this.GetParameters2();
                if (ParaName != null)
                {
                    for (int i = 0; i < ParaName.Length; i++)
                    {
                        parameters[i] = new Node.Core2.Requestor.ParameterType();
                        parameters[i].parameterName = ParaName[i];
                        parameters[i].Value = ParaValue[i];
                    }
                }

                qry.parameters = parameters;

                Node.Core2.Requestor.ResultSetType result = req.Query(qry);

                //queryResult = "Row ID:" + result.rowId + "<br>Row Count:" + result.rowCount + "<br>Format:"+ ((result.results == null)? Node.Core2.Requestor.DocumentFormatType.FLAT : result.results.format);

                if (NodeXMLResult.isValidXMLContent(result.results.Any[0].OuterXml))
                    queryResult =  NodeXMLResult.GetWellFormatXML(result.results.Any[0].OuterXml);
                else
                    queryResult = result.results.Any[0].OuterXml;
                //need to write <<result.results.any>> to file system for download purpose

                //this.btnDownloadPanel2.Visible = true;
            }
            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    queryResult = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    queryResult = "System error." + Environment.NewLine + sex.Message;
                }

            }
            catch (Exception ex)
            {
                //queryResult = "System error." + Environment.NewLine + ex.ToString();
                queryResult = "System error." + Environment.NewLine + ex.Message;
                this.btnDownloadPanel2.Visible = false;
            }
        }
        else
        {
            queryResult = "Please Enter a Node URL";
            this.btnDownloadPanel2.Visible = false;
        }
        this.ddlRequest2_SelectedIndexChanged(null, null);

        this.txtQueryResult2.Text = queryResult;
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

    protected void btnDownload2_Click(object sender, EventArgs e)
    {
        //string filename = this.ddlRequest.SelectedValue + "_" + DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss_tt");
        //ASCIIEncoding ae = new ASCIIEncoding();
        //Hashtable ht = new Hashtable();

        //if (this.txtQueryResult.Text != null && this.txtQueryResult.Text != "")
        //{
        //    ht.Add(filename + ".xml", ae.GetBytes(this.txtQueryResult.Text));
        //    WinZip wz = new WinZip();
        //    byte[] content = wz.CreateZip(ht);

        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Length", content.Length.ToString());
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".zip");
        //    this.Response.BinaryWrite(content);
        //    this.Response.End();
        //}
        //else
        //    this.lblResult.Text = "No record found.";

        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

}
