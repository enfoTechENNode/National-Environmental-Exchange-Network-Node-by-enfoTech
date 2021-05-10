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
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

using Node.Lib.Utility;
using Node.Lib.Utility.Zip;
using Node.Core.Biz.Objects;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;
using Node.Lib.UI.WebControls;


public partial class Pages_OperationManager_ManageOperation : Node.Core.UI.Base.AdminPageBase
{
    private ManageOperation objManageOP = null;
    private SystemConfiguration sysConfig = null;
    private int ParameterCount = 0;
    private string opID = string.Empty;
    private List<NodeDocument> lstNodeDoc = null;
    private string submittedURL = "";
    
    protected void Page_Init(object sender, EventArgs e)
    {
        string[] sFormateType = Enum.GetNames(typeof(Node.Core2.Requestor.DocumentFormatType));
        foreach (string str in sFormateType)
        {
            this.drpFileType1.Items.Add(new ListItem(str, str));
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.PageDescription = "This page allows you to manage existing operations";

        if (!IsPostBack)
        {
            this.InitControls();

            this.GenerateTemplateGV();
            this.GenerateGridView();

            if (Session[Phrase.TRANSFORMRESULT] == null)
            {
                Session.Add(Phrase.TRANSFORMRESULT, "");
            }
        }
        
        this.egvDocumentGrid.RowCommand += new GridViewCommandEventHandler(this.egvDocumentGrid_RowCommand);
        this.egvDocumentGrid.AutoGenerateColumns = true;

        BuildFilters();
    }

    private void BuildFilters()
    {
        if (this.ddlOperations.SelectedValue != "")
        {
            string id = this.ddlOperations.SelectedValue;
            IOperationManager db = new DBManager().GetOperationManagerDB();
            List<string> listPara = db.GetParametersName(id);

            foreach (string paraname in listPara)
            {
                FormInputField lbl = new FormInputField();
                DropDownList drp = new DropDownList();
                drp.ID = paraname;
                lbl.FieldName = paraname;

                DataTable dt = db.GetParameterValue(id, paraname);
                drp.DataSource = dt;
                drp.DataTextField = dt.Columns[0].ColumnName;
                drp.DataValueField = dt.Columns[0].ColumnName;
                drp.DataBind();
                drp.Items.Insert(0, new ListItem(""));

                lbl.Controls.Add(drp);
                opFilter.Controls.Add(lbl);           
            }
        }
    }

    private void InitControls()
    {
        string version = (string)Session[Phrase.VERSION_NO];
        this.objManageOP = new ManageOperation();
        DataTable dtOPConfig = objManageOP.GetConfigOperations(version);

        DataRow dr = dtOPConfig.NewRow();
        dr["ID"] = "";
        dr["NAME"] = "";
        dtOPConfig.Rows.InsertAt(dr, 0);

        ddlOperations.DataSource = dtOPConfig;
        ddlOperations.DataTextField = "NAME";
        ddlOperations.DataValueField = "ID";
        ddlOperations.DataBind();
    }

    private void GenerateGridView()
    {
        this.objManageOP = new ManageOperation();
        string opName = ddlOperations.SelectedValue;
        string version = (string)Session[Phrase.VERSION_NO];

        if (opName.Trim().Length > 0)
        {
            DataTable dt = objManageOP.GetDocumentsByOperationID(ddlOperations.SelectedValue,ddlOperations.SelectedItem.Text);

            if (dt != null)
            {
                //ViewState["Document"] = dt;
                dt = AddFilter(dt);
                this.egvDocumentGrid.Columns[2].Visible = true;
                if (!btnSubmit.Enabled && dt.Columns.Contains("Status"))
                {
                    dt.Columns.Remove("Status");
                    this.egvDocumentGrid.Columns[2].Visible = false;
                }
                this.egvDocumentGrid.CachedDataTable = dt;
                this.egvDocumentGrid.DataBind();
                this.egvDocumentGrid.Visible = true;
                //this.FormSectionBlockGridView.Visible = true;
            }
            else
            {
                this.egvDocumentGrid.Visible = false;
                //this.FormSectionBlockGridView.Visible = false;
            }
        }
        else
        {
            this.egvDocumentGrid.Visible = false;
            //this.FormSectionBlockGridView.Visible = false;
        }

    }
    private void GenerateTemplateGV()
    {
        this.objManageOP = new ManageOperation();
        this.opID = ddlOperations.SelectedItem.Value;
        if (opID.Trim().Length > 0)
        {
            ArrayList arrTemID = new ArrayList();
            ArrayList arrTemName = new ArrayList();

            this.objManageOP = new ManageOperation();

            Hashtable ht = this.objManageOP.GetStyleSheetByID(opID);

            if (ht != null)
            {
                arrTemID = (ArrayList)ht["ID"];
                arrTemName = (ArrayList)ht["NAME"];

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                dt.Columns.Add("Template ID", typeof(System.String));
                dt.Columns.Add("Template Name", typeof(System.String));


                for (int i = 0; i < arrTemID.Count; i++)
                {
                    dt.Rows.Add(new object[] { arrTemID[i], arrTemName[i] });
                }
                ds.Tables.Add(dt);

                if (ds != null)
                {
                    this.grvViewStyleSheets.CachedDataTable = ds.Tables[0];
                    this.grvViewStyleSheets.DataBind();
                    grvViewStyleSheets.Visible = true;
                    grvViewStyleSheets.PageSize = 5;
                }

            }
            else
            {
                grvViewStyleSheets.Visible = false;

            }
        }
    }
    private void GenerateParameters()
    {
        this.dcpDynamicParams2.Controls.Clear();

        if (this.ddlOperations.SelectedValue.Trim().Equals(""))
        {
            this.ParameterCount = 0;
        }
        else
        {
            try
            {
                Hashtable pairs = Operation.RetrieveSolicitParameterNames();
                string hashValue = "" + pairs[ddlOperations.SelectedItem.Text.Trim()];
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
                        this.ParameterCount = 0;
                }
                else
                    this.ParameterCount = 0;
            }
            catch (Exception )
            {
            }
        }
        //save to session
        //this.Session.Add("ParamCount", this.ParameterCount);
    }

    protected void ddlOperations_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.objManageOP = new ManageOperation();
        string opID = ddlOperations.SelectedValue;
        string dataFlow = string.Empty;

        if (ddlOperations.SelectedValue.Trim().Length > 0)
        {
            ManageOperation.OpMgrData objOpMgrData = this.objManageOP.GetOperation(opID);

            btnUpload.Enabled = objOpMgrData.upload;
            btnGenerate.Enabled = objOpMgrData.generate;
            btnSubmit.Enabled = objOpMgrData.submit;

            this.GenerateTemplateGV();

        }
        else
        {
            btnSubmit.Enabled = false;
            btnGenerate.Enabled = false;
            btnUpload.Enabled = false;
        }

        if (opID.Trim().Length > 0)
        {
            ViewState["opID"] = opID;
            //try
            //{
            //    dataFlow = this.objManageOP.GetConfigDataFlowByID(opID);
            //    ViewState["dataFlow"] = dataFlow;
            //}
            //catch
            //{
            //    // msg.setMessage("Data Flow was not able to find for this operation.");
            //}
        }

        this.GenerateParameters();
        this.GenerateGridView();
    }
    protected void egvDocumentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Transform")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                //int rowNo = this.egvDocumentGrid.PageSize * this.egvDocumentGrid.PageIndex + Convert.ToInt32(e.CommandArgument);
                //if (ViewState["Document"] != null)
                //{
                //    this.ShowDetail(((DataTable)ViewState["Document"]).Rows[rowNo]["FILE_ID"].ToString());
                //}
                //else if (this.egvDocumentGrid.CachedDataTable != null)
                if (this.egvDocumentGrid.CachedDataTable != null)
                {
                    string sFileID = ""+e.CommandArgument;
                    this.ShowDetail(sFileID);
                }
            }
            this.mdlPopup.Show();
        }
        else if (e.CommandName == "GetDownLoadReport")
        {
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                //int rowNo = this.egvDocumentGrid.PageSize * this.egvDocumentGrid.PageIndex + Convert.ToInt32(e.CommandArgument);
                //string tranID = this.egvDocumentGrid.CachedDataTable.Rows[rowNo]["TRANS_ID"].ToString();
                //string opName = this.egvDocumentGrid.CachedDataTable.Rows[rowNo]["DATAFLOW_NAME"].ToString();

                IOperationManager opMgr = new DBManager().GetOperationManagerDB();
                OperationManagerTrans objOpMgrTran = opMgr.GetOperationManagerTrans(this.ddlOperations.SelectedItem.Text, e.CommandArgument.ToString());

                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "DownloadReport.zip");
                Response.OutputStream.Write(objOpMgrTran.FileContent, 0, objOpMgrTran.FileContent.Length);
                Response.Flush();
                Response.End();

                //Response.Redirect("~/Pages/OperationManager/FileHandler.ashx");

            }
        }
        else if (e.CommandName == "GoToDataView")
        {
            Response.Redirect("~/Pages/DataViewer/DataViewer.aspx?TransID="+e.CommandArgument);
        }

        this.GenerateParameters();
    }
    protected void egvDocumentGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView aRow = e.Row.DataItem as DataRowView;
            string sTranID = ""+aRow["TRANS_ID"];
            if (!CheckDownloadReport(sTranID))
            {
                ImageButton btn = e.Row.Cells[2].FindControl("btnTrans") as ImageButton;
                btn.Visible = false;
            }
        }
    }

    private bool CheckDownloadReport(string sTranID)
    {
        IOperationManager opMgr = new DBManager().GetOperationManagerDB();
        return opMgr.CheckOperationManagerTransDownloadReport(this.ddlOperations.SelectedItem.Text, sTranID);
    }

    private void ShowDetail(string fileID)
    {
        if (!string.IsNullOrEmpty(fileID))
        {
            ViewState["FileID"] = fileID;
        }
        try
        {

            int fID = 0;
            if (Int32.TryParse(fileID, out fID))
            {
                NodeDocument doc = new NodeDocument(fID);
                doc.RefreshFromDB();
                this.mlFileName.MsgContent = doc.FileName;
                this.mlFileType.MsgContent = doc.FileType;
                this.mlTransID.MsgContent = doc.TransactionID;
                this.mlStatus.MsgContent = doc.Status;
                this.mlDataFlow.MsgContent = doc.DataFlow;
                this.mlSubmitDate.MsgContent = doc.SubmitDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                this.mlFileSize.MsgContent = "" + doc.FileSize + " bytes";
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
        }
    }

    protected void btnClosed_Click(object sender, EventArgs e)
    {
         
    }

    //Upload
    protected void btnFileUpload_Click(object sender, EventArgs e)
    {
        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
        {
            this.Upload_V2();
        }
        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_11)
        {
            this.Upload_V1();
        }
        this.GenerateGridView();
        this.GenerateParameters();
    }

    private void Upload_V2()
    {
        this.objManageOP = new ManageOperation();
        this.sysConfig = new SystemConfiguration();

        string nodeURL = this.sysConfig.GetNodeAddress_V2();
        string opName = string.Empty;
        string secToken = string.Empty;

        if (ddlOperations.SelectedValue.Trim().Length > 0)
        {
            opName = ddlOperations.SelectedItem.Text;

            if (nodeURL.Trim().Length > 0)
            {
                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(nodeURL.Trim());
                Node.Core2.Requestor.NodePing nodePing = new Node.Core2.Requestor.NodePing();
                nodePing.hello = "Hello";
                try
                {
                    Node.Core2.Requestor.NodePingResponse pngRsp = req.NodePing(nodePing);

                    if (pngRsp.nodeStatus == Node.Core2.Requestor.NodeStatusCode.Ready)
                    {
                        Node.Core2.Requestor.Authenticate auth = new Node.Core2.Requestor.Authenticate();

                        auth.userId = this.sysConfig.GetNodeAdministratorUserID();
                        auth.credential = this.sysConfig.GetNodeAdministratorPassword();
                        auth.authenticationMethod = "PASSWORD";
                        auth.domain = null;
                        try
                        {
                            Node.Core2.Requestor.AuthenticateResponse authRsp = req.Authenticate(auth);
                            secToken = authRsp.securityToken;
                        }
                        catch (Exception ex)
                        {
                            this.msg.setMessage("Authentication Failed." + Environment.NewLine + ex.Message);
                        }

                        if (secToken.Trim().Length > 0 && opName.Trim().Length > 0)
                        {
                            try
                            {
                                Node.Core2.Requestor.Submit submit = new Node.Core2.Requestor.Submit();
                                submit.securityToken = secToken.Trim();
                                submit.dataflow = opName;
                                submit.documents = Get_Upload_V2_InputDocuments();

                                Node.Core2.Requestor.StatusResponseType resp = req.Submit(submit);
                                string transID = resp.transactionId;
                            }
                            catch (Exception ex)
                            {
                                msg.setMessage("System error." + Environment.NewLine + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        this.msg.setMessage("Node Ping Failed.");
                    }
                }
                catch (Exception ex)
                {
                    this.msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
        }
    }
    protected Node.Core2.Requestor.NodeDocumentType[] Get_Upload_V2_InputDocuments()
    {
        Node.Core2.Requestor.NodeDocumentType[] retDocs = new Node.Core2.Requestor.NodeDocumentType[1];
        byte[] content = this.fuUpload.FileBytes;

        if (content != null && content.Length > 0)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.fuUpload.FileName;

            if (!this.drpFileType1.SelectedValue.ToString().Equals(""))
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType1.SelectedValue.ToString());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;

            doc.documentContent = new Node.Core2.Requestor.AttachmentType();
            doc.documentContent.Value = content;
            retDocs[0] = doc;
        }
        return retDocs;
    }

    private void Upload_V1()
    {
        this.sysConfig = new SystemConfiguration();
        string nodeURL = this.sysConfig.GetNodeAddress();
        string secToken = string.Empty;
        string opID = (string)ViewState["opID"];
        Hashtable ht = new Hashtable();
        string opName = string.Empty;
        string transID = string.Empty;

        this.objManageOP = new ManageOperation();

        string subUsername = this.sysConfig.GetNodeAdministratorUserID();
        string subPassword = this.sysConfig.GetNodeAdministratorPassword();

        if (ddlOperations.SelectedValue.Trim().Length > 0)
        {
            opName = ddlOperations.SelectedItem.Text;

            if (nodeURL.Trim().Length > 0 && subUsername.Trim().Length > 0 && subPassword.Trim().Length > 0)
            {
                Node.Core.API.NodeRequestor requestor = new Node.Core.API.NodeRequestor(nodeURL.Trim());
                try
                {
                    string strToken = requestor.Authenticate(subUsername, subPassword, "PASSWORD");
                    if (strToken.Trim().Length > 0)
                    {
                        try
                        {
                            string strMsg = requestor.Submit(strToken, transID, opName, this.Get_Upload_V1_InputDocuments());
                        }
                        catch (Exception ex)
                        {
                            msg.setMessage("System error." + Environment.NewLine + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
        }
    }
    protected Node.Core.Document.NodeDocument[] Get_Upload_V1_InputDocuments()
    {
        Node.Core.Document.NodeDocument[] retDocs = null;
        ArrayList arrDoc = new ArrayList();

        Stream stream = this.fuUpload.FileContent;

        Node.Core.Document.NodeDocument newDoc = new Node.Core.Document.NodeDocument();

        byte[] content = this.fuUpload.FileBytes;

        if (content != null && content.Length > 0)
        {
            Node.Core.Document.NodeDocument doc = new Node.Core.Document.NodeDocument();
            doc.name = this.fuUpload.FileName;
            doc.type = "XML";
            doc.content = content;
            arrDoc.Add(doc);
        }

        int i = 0;
        retDocs = new Node.Core.Document.NodeDocument[arrDoc.Count];
        foreach (Node.Core.Document.NodeDocument type in arrDoc)
        {
            retDocs[i++] = type;
        }

        return retDocs;
    }

    //Generate 
    protected void btn_ExGenerate_Click(object sender, EventArgs e)
    {
        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_20)
        {
            this.Generate_V2();
        }
        if ((string)Session[Phrase.VERSION_NO] == Phrase.VERSION_11)
        {
            this.Generate_V1();
        }
        this.GenerateParameters();
    }

    private void Generate_V2()
    {
        this.objManageOP = new ManageOperation();
        this.sysConfig = new SystemConfiguration();
        string nodeURL = this.sysConfig.GetNodeAddress_V2();
        string secToken = string.Empty;
        string opName = string.Empty;

        if (ddlOperations.SelectedValue.Trim().Length > 0)
        {
            opName = ddlOperations.SelectedItem.Text;

            if (nodeURL.Trim().Length > 0)
            {
                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(nodeURL.Trim());
                Node.Core2.Requestor.NodePing nodePing = new Node.Core2.Requestor.NodePing();
                nodePing.hello = "Hello";
                try
                {
                    Node.Core2.Requestor.NodePingResponse pngRsp = req.NodePing(nodePing);

                    if (pngRsp.nodeStatus == Node.Core2.Requestor.NodeStatusCode.Ready)
                    {
                        Node.Core2.Requestor.Authenticate auth = new Node.Core2.Requestor.Authenticate();

                        auth.userId = this.sysConfig.GetNodeAdministratorUserID();
                        auth.credential = this.sysConfig.GetNodeAdministratorPassword();
                        auth.authenticationMethod = "PASSWORD";
                        auth.domain = null;
                        try
                        {
                            Node.Core2.Requestor.AuthenticateResponse authRsp = req.Authenticate(auth);
                            secToken = authRsp.securityToken;
                        }
                        catch (Exception ex)
                        {
                            this.msg.setMessage("Authentication Failed." + Environment.NewLine + ex.Message);
                        }

                        if (secToken.Trim().Length > 0)
                        {
                            try
                            {
                                Node.Core2.Requestor.Solicit sol = new Node.Core2.Requestor.Solicit();
                                sol.securityToken = secToken.Trim();
                                sol.request = opName;

                                string[] paraName = this.GetParameters_Name();
                                string[] paraValue = this.GetParameters_Value();

                                if (paraName != null)
                                {
                                    sol.parameters = new Node.Core2.Requestor.ParameterType[paraName.Length];

                                    for (int i = 0; i < paraName.Length; i++)
                                    {
                                        sol.parameters[i] = new Node.Core2.Requestor.ParameterType();
                                        sol.parameters[i].parameterName = paraName[i];
                                        sol.parameters[i].Value = paraValue[i];
                                    }
                                }
                                else
                                {
                                    sol.parameters = null;
                                }

                                Node.Core2.Requestor.StatusResponseType resp = req.Solicit(sol);

                                if (resp.status == Node.Core2.Requestor.TransactionStatusCode.Approved)
                                {
                                    msg.setMessage("Solicit operation approved. Transaction ID:"+resp.transactionId);
                                }
                                if (resp.status == Node.Core2.Requestor.TransactionStatusCode.Failed)
                                {
                                    msg.setMessage("Solicit operation Failed.");
                                }
                            }
                            catch (Exception ex)
                            {
                                msg.setMessage("System error." + Environment.NewLine + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        this.msg.setMessage("Node Ping Failed.");
                    }

                }
                catch (Exception ex)
                {
                    this.msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
            else
            {
                msg.setMessage("Node URL is not available");
            }
        }
        else
        {
            msg.setMessage("Please select a operation first");
        }
    }
    private void Generate_V1()
    {
        this.objManageOP = new ManageOperation();
        this.sysConfig = new SystemConfiguration();
        string nodeURL = this.sysConfig.GetNodeAddress();
        string secToken = string.Empty;
        string opName = string.Empty;

        this.objManageOP = new ManageOperation();

        string subUsername = this.sysConfig.GetNodeAdministratorUserID();
        string subPassword = this.sysConfig.GetNodeAdministratorPassword();

        if (ddlOperations.SelectedValue.Trim().Length > 0)
        {
            opName = ddlOperations.SelectedItem.Text;

            if (nodeURL.Trim().Length > 0 && subUsername.Trim().Length > 0 && subPassword.Trim().Length > 0)
            {
                Node.Core.API.NodeRequestor requestor = new Node.Core.API.NodeRequestor(nodeURL.Trim());
                try
                {
                    string strToken = requestor.Authenticate(subUsername, subPassword, "PASSWORD");

                    if (strToken.Trim().Length > 0)
                    {
                        string request = opName;
                        string returnURL = string.Empty;

                        try
                        {
                            string resultMeg = requestor.Solicit(strToken.Trim(), returnURL, request, this.GetParameters_Value());
                            msg.setMessage("Transaction ID: " + resultMeg);
                        }
                        catch (Exception ex)
                        {
                            msg.setMessage("System error." + Environment.NewLine + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
        }

    }

    private string[] GetParameters_Name()
    {
        string[] retParams = null;

        Hashtable pairs = Operation.RetrieveSolicitParameterNames();
        string hashValue = "" + pairs[this.ddlOperations.SelectedItem.Text];
        if (hashValue != null && !hashValue.Trim().Equals(""))
        {
            string[] split = hashValue.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (split != null && split.Length > 0)
            {
                retParams = new string[split.Length];
                //loop parameters
                for (int i = 0; i < split.Length; i++)
                {
                    retParams[i] = split[i];
                }
            }
        }
        return retParams;
    }
    private string[] GetParameters_Value()
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

    //Submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string test = (string)Session[Phrase.VERSION_NO];
        lstNodeDoc = new List<NodeDocument>();
        submittedURL = "";

        if (Session[Phrase.VERSION_NO].ToString() == Phrase.VERSION_20)
        {
            this.Submit_V2();
        }
        if (Session[Phrase.VERSION_NO].ToString() == Phrase.VERSION_11)
        {
            this.Submit_V1();
        }

        this.GenerateGridView();
        this.GenerateParameters();
    }

    private void Submit_V2()
    {
        this.objManageOP = new ManageOperation();
        string secToken = string.Empty;
        string opID = (string)ViewState["opID"];

        objManageOP = new ManageOperation();
        if (opID.Trim().Length > 0)
        {
            ManageOperation.OpMgrData objOpMgrData = objManageOP.GetOperation(opID);


            if (objOpMgrData.submitURL.Trim().Length > 0 && objOpMgrData.submitUsername.Trim().Length > 0 && objOpMgrData.submitPassword.Trim().Length > 0)
            {
                Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(objOpMgrData.submitURL.Trim());
                Node.Core2.Requestor.NodePing nodePing = new Node.Core2.Requestor.NodePing();
                nodePing.hello = "Hello";
                try
                {
                    Node.Core2.Requestor.NodePingResponse pngRsp = req.NodePing(nodePing);

                    if (pngRsp.nodeStatus == Node.Core2.Requestor.NodeStatusCode.Ready)
                    {
                        Node.Core2.Requestor.Authenticate auth = new Node.Core2.Requestor.Authenticate();

                        auth.userId = objOpMgrData.submitUsername;
                        auth.credential = objOpMgrData.submitPassword;
                        auth.authenticationMethod = "PASSWORD";
                        if (!objOpMgrData.submitDomainName.Trim().Equals(""))
                        {
                            auth.domain = objOpMgrData.submitDomainName;
                        }

                        try
                        {
                            Node.Core2.Requestor.AuthenticateResponse authRsp = req.Authenticate(auth);
                            secToken = authRsp.securityToken;
                        }
                        catch (Exception ex)
                        {
                            this.msg.setMessage("Authentication Failed." + Environment.NewLine + ex.Message);
                            return;
                        }

                        if (secToken.Trim().Length > 0 && ddlOperations.SelectedItem.Text.Trim().Length > 0 && objOpMgrData.dataFlow.Trim().Length > 0)
                        {
                            try
                            {
                                req = new Node.Core2.Requestor.NodeRequestor(objOpMgrData.submitURL.Trim());
                                Node.Core2.Requestor.Submit submit = new Node.Core2.Requestor.Submit();
                                submit.securityToken = secToken;
                                submit.dataflow = objOpMgrData.dataFlow;
                                submit.documents = Get_Submit_V2_InputDocuments();
                                submit.flowOperation = objOpMgrData.dataFlowOperation;
                                submit.transactionId = "";
                                
                                //string []Strrecipient = {""};
                                //submit.recipient = Strrecipient;
                                //submit.notificationURI = new Node.Core2.Requestor.NotificationURIType[1];
                                //submit.notificationURI[0] = new Node.Core2.Requestor.NotificationURIType();
                                //submit.notificationURI[0].notificationType = Node.Core2.Requestor.NotificationTypeCode.Warning;
                                //submit.notificationURI[0].Value = "";

                                Node.Core2.Requestor.StatusResponseType resp = req.Submit(submit);
                                string transID = resp.transactionId;
                                submittedURL = objOpMgrData.submitURL;
                                CreateSubmittedTrans(transID,objOpMgrData.dataFlow);
                            }
                            catch (Exception ex)
                            {
                                msg.setMessage("System error." + Environment.NewLine + ex.Message);
                            }
                        }
                        else
                        {
                            this.msg.setMessage("DataFlow name is not specified.");
                        }
                    }
                    else
                    {
                        this.msg.setMessage("Node Ping Failed.");
                    }
                }
                catch (Exception ex)
                {
                    this.msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
            else
            {
                this.msg.setMessage("Please specify credential for submission.");
            }
        }
        else
        {
            msg.setMessage("Please select an operation");
        }
    }

    protected Node.Core2.Requestor.NodeDocumentType[] Get_Submit_V2_InputDocuments()
    {
        Node.Core.Biz.Objects.NodeDocument doc = null;
        Node.Core2.Requestor.NodeDocumentType[] retDocs = null;
        Node.Core2.Requestor.NodeDocumentType newDoc = null;

        string[] ids = this.egvDocumentGrid.GetCheckedValue("gcbfDocuments");

        if (ids.Length > 0)
        {
            retDocs = new Node.Core2.Requestor.NodeDocumentType[ids.Length];

            for (int i = 0; i < ids.Length; i++)
            {
                doc = new NodeDocument(int.Parse(ids[i]));
                doc.RefreshFromDB();
                newDoc = new Node.Core2.Requestor.NodeDocumentType();
                if (doc.FileType == "XML")
                {
                    newDoc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
                }
                if (doc.FileType == "ZIP")
                {
                    newDoc.documentFormat = Node.Core2.Requestor.DocumentFormatType.ZIP;
                }

                newDoc.documentName = doc.FileName;
                byte[] content = new byte[doc.FileSize];
                doc.FileContent.Read(content, 0, content.Length);
                newDoc.documentContent = new Node.Core2.Requestor.AttachmentType();
                newDoc.documentContent.Value = content;
                retDocs[i] = newDoc;
                lstNodeDoc.Add(doc);
            }

        }
        return retDocs;
    }

    private void Submit_V1()
    {
        this.objManageOP = new ManageOperation();
        string secToken = string.Empty;
        string opID = (string)ViewState["opID"];

        objManageOP = new ManageOperation();
        if (opID.Trim().Length > 0)
        {
            ManageOperation.OpMgrData objOpMgrData = objManageOP.GetOperation(opID);

            if (objOpMgrData.submitURL.Trim().Length > 0 && objOpMgrData.submitUsername.Trim().Length > 0 && objOpMgrData.submitPassword.Trim().Length > 0)
            {
                Node.Core.API.NodeRequestor requestor = new Node.Core.API.NodeRequestor(objOpMgrData.submitURL);
                try
                {
                    string strToken = requestor.Authenticate(objOpMgrData.submitUsername, objOpMgrData.submitPassword, "password");

                    if (strToken.Trim().Length > 0)
                    {
                        try
                        {
                            string strMsg = requestor.Submit(strToken, "", objOpMgrData.dataFlow, this.Get_Submit_V1_InputDocuments());
                            submittedURL = objOpMgrData.submitURL;
                            CreateSubmittedTrans(strMsg,objOpMgrData.dataFlow);
                        }
                        catch (Exception ex)
                        {
                            msg.setMessage("System error." + Environment.NewLine + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg.setMessage("System error." + Environment.NewLine + ex.Message);
                }
            }
        }
    }

    protected Node.Core.Document.NodeDocument[] Get_Submit_V1_InputDocuments()
    {
        Node.Core.Document.NodeDocument[] retDocs = null;
        Node.Core.Document.NodeDocument newDoc = null;
        Node.Core.Biz.Objects.NodeDocument doc = null;

        string[] ids = this.egvDocumentGrid.GetCheckedValue("gcbfDocuments");

        if (ids.Length > 0)
        {
            retDocs = new Node.Core.Document.NodeDocument[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                doc = new NodeDocument(int.Parse(ids[i]));
                doc.RefreshFromDB();
                newDoc = new Node.Core.Document.NodeDocument();

                if (doc.FileType == "XML")
                {
                    newDoc.type = "XML";
                }
                if (doc.FileType == "ZIP")
                {
                    newDoc.type = "ZIP";
                }

                newDoc.name = doc.FileName;

                byte[] content = new byte[doc.FileSize];
                doc.FileContent.Read(content, 0, content.Length);
                newDoc.content = content;
                retDocs[i] = newDoc;
                lstNodeDoc.Add(doc);
            }
        }
        return retDocs;
    }

    private void CreateSubmittedTrans(string transid,string dataflow)
    {
        if (lstNodeDoc != null && submittedURL != "")
        {
            IOperationManager opMgr = new DBManager().GetOperationManagerDB();
            foreach (NodeDocument doc in lstNodeDoc)
            {
               OperationManagerTrans obj = opMgr.CreateOperationManagerTrans();
               obj.OperationName = this.ddlOperations.SelectedItem.Text;
               obj.NodeVersion = Session[Phrase.VERSION_NO].ToString();
               obj.Status = Phrase.STATUS_SUBMITTED;
               obj.SubmittedDate = DateTime.Now;
               obj.SubmittedURL = submittedURL;
               obj.TransID = doc.TransactionID;
               obj.TransIDSupplied = transid;
               obj.DataFlow = dataflow;
               opMgr.UpdateOperationManagerTrans(obj);
            }
        }

    }

    //View
    protected void btnTransform_Click(object sender, EventArgs e)
    {
        string fileID = "" + ViewState["FileID"];
        int fID = 0;
        if (Int32.TryParse(fileID, out fID))
        {
            NodeDocument doc = new NodeDocument(fID);
            doc.RefreshFromDB();

            byte[] content = new byte[doc.FileSize];
            doc.FileContent.Read(content, 0, content.Length);

            ArrayList arrTemID = this.GetSelectedTemplateID();

            if (arrTemID.Count > 0)
            {
                if (doc.FileType == "ZIP")
                {
                    WinZip wp = new WinZip();
                    Hashtable ht = wp.ExtractZip(content);
                    if (ht.Keys.Count > 1)
                    {
                        foreach (string key in ht.Keys)
                        {
                            content = (byte[])ht[key];
                            break;
                        }
                    }
                }
                IConfigurations sysConfigDB = new DBManager().GetConfigurationsDB();
                sysConfigDB.GetConfig("" + arrTemID[0], "XSLT");

                XslCompiledTransform transformer = new XslCompiledTransform();
                string xsltcontent = sysConfigDB.GetConfig("" + arrTemID[0], "XSLT");
                XmlTextReader r = new XmlTextReader(new StringReader(xsltcontent));
                transformer.Load(r);

                MemoryStream ms = new MemoryStream(content);
                StreamReader sr = new StreamReader(ms);
                ms.Position = 0;
                string xmlcontent = sr.ReadToEnd();

                XmlTextReader reader = new XmlTextReader(new StringReader(xmlcontent));
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter writer = new XmlTextWriter(stringWriter);

                transformer.Transform(reader, writer);
                string contents = stringWriter.ToString();
                Session[Phrase.TRANSFORMRESULT] = contents;

                string url = Request.Url.OriginalString;
                url = url.Replace("ManageOperation.aspx", "ViewDocument.aspx");
                string myScript = "window.open('" + url + "',null,'height=600,width=800,status=yes,toolbar=no,menubar=no,scrollbars=yes');";
                //myScript = "alert('hello');";
                if (!Page.ClientScript.IsClientScriptBlockRegistered("OpenWindows")) 
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "OpenWindows", myScript, true); 
                }
            }
            else
            {
              
                if (doc.FileType == "XML")
                {
                    Hashtable hash = new Hashtable();
                    hash.Add(doc.FileName, content);
                    WinZip wz = new WinZip();
                    content = wz.CreateZip(hash);
                }
                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AppendHeader("content-disposition", "attachment; filename=" + "Download.zip");
                Response.OutputStream.Write(content, 0, content.Length);
                Response.Flush();
                Response.End();
            }

            this.GenerateParameters();
        }
    }
    private ArrayList GetSelectedTemplateID()
    {

        ArrayList arrTemID = new ArrayList();
        RadioButton radSelect;
        int radIndex = 0;
        //Loop through each row in the GridView
        foreach (GridViewRow row in grvViewStyleSheets.Rows)
        {
            //Get the index of the current CheckBox
            radIndex = row.RowIndex;
            radSelect = (RadioButton)row.FindControl("radSelect");

            //Now see if the current Radio is checked
            if (radSelect != null)
            {
                if (radSelect.Checked)
                {
                    arrTemID.Add(grvViewStyleSheets.Rows[radIndex].Cells[2].Text);
                }
            }

        }
        return arrTemID;
    }

    //
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GenerateGridView();
    }
    private DataTable AddFilter(DataTable dt)
    {
        DataView dv = new DataView(dt);

        IOperationManager db = new DBManager().GetOperationManagerDB();
        List<string> listPara = db.GetParametersName(ddlOperations.SelectedValue);
        string sFilter = "";

        foreach (string para in listPara)
        {
            DropDownList drp = opFilter.FindControl(para) as DropDownList;
            if (drp != null && drp.SelectedValue != "")
            {
                sFilter = sFilter + "["+ drp.ID + "] = '" + drp.SelectedValue + "' and ";      
            }
        }

        if (sFilter.Length > 0)
        {
            sFilter = sFilter.Trim();
            sFilter = sFilter.Substring(0, sFilter.Length - 3);
        }
        dv.RowFilter = sFilter;

        return dv.ToTable();
    }
}
