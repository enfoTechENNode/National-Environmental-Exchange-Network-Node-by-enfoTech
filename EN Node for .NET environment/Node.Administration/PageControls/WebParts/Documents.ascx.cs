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
using System.IO;

using Node.Lib.Utility;
using Node.Lib.Utility.Zip;
using Node.Core.Biz.Objects;
using Node.Core;

public partial class PageControls_WebParts_Documents : Node.Core.UI.Base.AdminUserControlBase
{
    #region Private Variables
    private string loggedUser = string.Empty;
    private int defaultTopNo = 5;
    private int defaultPageSize = 10;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Phrase.DEFAULT_TOPNUM] != null && !int.TryParse(Session[Phrase.DEFAULT_TOPNUM].ToString(), out defaultTopNo))
        {
            defaultTopNo = 5;
        }
        if (Session[Phrase.DEFAULT_PAGESIZE] != null && !int.TryParse(Session[Phrase.DEFAULT_PAGESIZE].ToString(), out defaultPageSize))
        {
            defaultPageSize = 10;
        }
        this.egvDocumentGrid.PageSize = defaultPageSize;
        this.lkbTop5Docu.ToolTip = "Top " + defaultTopNo + " documents";
        this.egvDocumentGrid.RowCommand += new GridViewCommandEventHandler(this.egvDocumentGrid_RowCommand);
        if (!this.IsPostBack)
        {
            this.InitializeDocu();
        }

        lnkBtnSearchDocu.Attributes.Add("onclick", "fnSetFocus('" + txtDocumentName.ClientID + "');");
        btnUpload.Attributes.Add("onclick", "fnSetFocus('" + txtDocuNameUpload.ClientID + "');");
    }
    #region Event Handlers

    protected void btnSearchDocu_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            {
                DateTime start;
                if (!string.IsNullOrEmpty(this.dtStart.Text))
                {
                    if (!DateTime.TryParse((this.dtStart.Text), out start))
                    {
                        start = DateTime.Parse("01/01/2000");
                    }
                }
                else
                {
                    start = DateTime.Parse("01/01/2000");
                }
                DateTime end;
                if (!string.IsNullOrEmpty(this.dtEnd.Text))
                {
                    if (!DateTime.TryParse((this.dtEnd.Text), out end))
                    {
                        end = DateTime.Now;
                    }
                }
                else
                {
                    end = DateTime.Now;
                }

                int domainName;
                if (!int.TryParse(this.ddlDomainName.SelectedValue, out domainName))
                    domainName = -1;

                DataTable dt = NodeDocument.SearchDocuments(this.txtDocumentName.Text, this.txtTransID.Text, domainName, this.ddlOperationName.SelectedValue, start, end, this.LoggedInUser);

                if (dt != null && dt.Rows.Count >= 0)
                {
                    this.btnDownload.Visible = true;
                    this.btnRemove.Visible = true;
                    if (dt.Rows.Count > defaultPageSize)
                    {
                        ViewState["Document"] = dt;
                        this.egvDocumentGrid.CachedDataTable = dt;
                        this.egvDocumentGrid.DataBind();
                        this.egvDocumentGrid.Visible = true;
                        this.dataLstDocu.Visible = false;
                        this.btnDownload.Visible = true;
                        this.btnRemove.Visible = true;

                    }
                    else
                    {
                        this.dataLstDocu.DataSource = dt;
                        this.dataLstDocu.DataBind();
                        this.egvDocumentGrid.Visible = false;
                        this.dataLstDocu.Visible = true;
                        this.btnDownload.Visible = false;
                        this.btnRemove.Visible = false;

                    }
                }
                else
                {
                    this.btnDownload.Visible = false;
                    this.btnRemove.Visible = false;
                }
                this.TotalDocument.Text = dt.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    protected void lkbTop5Docu_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;
            this.btnDownload.Visible = false;
            this.btnRemove.Visible = false;

            DataTable dt = NodeDocument.SearchDocuments("", "", -1, "", DateTime.Now.AddMonths(-6), DateTime.Now, this.LoggedInUser);
            DataTable newDT = dt.Clone();
            //DataRow newRow = null;

            if (dt != null && dt.Rows.Count >= 0)
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i < defaultTopNo)
                    {
                        newDT.ImportRow(dr);
                        i++;
                    }
                    else
                        break;
                }

                this.dataLstDocu.DataSource = newDT;
                this.dataLstDocu.DataBind();
                this.dataLstDocu.Visible = true;
                this.egvDocumentGrid.Visible = false;
                this.TotalDocument.Text = newDT.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    void egvDocumentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            this.mdlPopupDocu.Show();
            if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                int rowNo = this.egvDocumentGrid.PageSize * this.egvDocumentGrid.PageIndex + Convert.ToInt32(e.CommandArgument);
                if (ViewState["Document"] != null)
                {
                    this.ShowDetail(((DataTable)ViewState["Document"]).Rows[rowNo]["FILE_ID"].ToString());
                }
                else if (this.egvDocumentGrid.CachedDataTable != null)
                {
                    this.ShowDetail(this.egvDocumentGrid.CachedDataTable.Rows[rowNo]["FILE_ID"].ToString());
                }
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            // Check for file content type
            if (fuFileContent.PostedFile.ContentType == "application/octet-stream")
            {
                this.lblError.Text = "Please do not upload the executable file!";
                this.lblError.Visible = true;
                this.customValidate.IsValid = false;
            }
            // Check for file size
            else if (fuFileContent.PostedFile.ContentLength > 4000)
            {
                this.lblError.Text = "Please do not upload the file size larger than 4 MB!";
                this.lblError.Visible = true;
                this.customValidate.IsValid = false;
            }
            else
            {
                NodeDocument doc = new NodeDocument(-1);
                doc.FileName = this.txtDocuNameUpload.Text;
                if (doc.FileName == null || doc.FileName.Trim().Equals(""))
                    doc.FileName = this.fuUpload.FileName;
                doc.FileType = this.txtDocuTypeUpload.Text;
                if (doc.FileType == null || doc.FileType.Trim().Equals(""))
                    doc.FileType = this.fuUpload.PostedFile.ContentType;
                doc.TransactionID = this.txtTransIDUpload.Text;
                doc.DataFlow = this.ddlDataFlow.SelectedValue;
                doc.FileContent = this.fuUpload.FileContent;
                doc.SaveDocument(this.LoggedInUser);

                this.lkbTop5Docu_Click(null, null);
                this.lblError.Text = "Saved Successfully";
                this.lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            if (ViewState["FileID"] != null)
            {
                NodeDocument doc = new NodeDocument(int.Parse(ViewState["FileID"].ToString()));
                doc.RefreshFromDB();
                doc.FileName = this.txtDocumentNameDetail.Text;
                doc.FileType = this.txtDocumentType.Text;
                doc.TransactionID = this.txtTransIDDetail.Text;
                doc.Status = this.txtStatus.Text;
                doc.DataFlow = this.ddlDataFlowName.SelectedValue;
                if (this.fuFileContent.FileContent.Length > 0)
                    doc.FileContent = this.fuFileContent.FileContent;
                doc.SaveDocument(this.LoggedInUser);

                this.lblError.Text = "Saved Successfully";
                this.lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            string[] ids = this.egvDocumentGrid.GetCheckedValue("gcbfDocuments");
            if (ids != null && ids.Length > 0)
            {
                byte[] content = null;
                string name = null;
                string type = null;
                if (ids.Length > 1)
                {
                    content = this.GetZippedContent(ids);
                    name = "Download.zip";
                    type = "application/zip";
                }
                else
                {
                    NodeDocument doc = new NodeDocument(int.Parse(ids[0]));
                    doc.RefreshFromDB();
                    Stream ms = doc.FileContent;
                    content = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(content, 0, content.Length);
                    name = doc.FileName;
                    type = doc.FileType;
                }
                if (content != null && content.Length > 0)
                {
                    Response.Clear();
                    Response.ContentType = type;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + name);
                    Response.OutputStream.Write(content, 0, content.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void btnDownloadDetail_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            byte[] content = null;
            if (ViewState["FileID"] != null)
            {
                NodeDocument doc = new NodeDocument(int.Parse(ViewState["FileID"].ToString()));
                doc.RefreshFromDB();
                if (doc.FileSize > 0)
                {
                    Response.Clear();
                    Response.ContentType = doc.FileType;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + doc.FileName);
                    content = new byte[doc.FileSize];
                    doc.FileContent.Read(content, 0, content.Length);
                    Response.OutputStream.Write(content, 0, content.Length);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            NodeDocument.DeleteDocuments(this.egvDocumentGrid.GetCheckedValue("gcbfDocuments"));
            if (this.egvDocumentGrid.Rows.Count > defaultPageSize)
                this.btnSearchDocu_Click(sender, e);
            else
                this.lkbTop5Docu_Click(sender, e);
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void imgBtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            ImageButton btnDelete = (ImageButton)sender;
            if (btnDelete != null)
            {
                NodeDocument.DeleteDocuments(new string[1] { btnDelete.CommandArgument });
                if (this.btnRemove.Visible)
                    this.btnSearchDocu_Click(sender, e);
                else
                    this.lkbTop5Docu_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }
    protected void ShowDocuInfo_Click(object sender, EventArgs e)
    {
        this.mdlPopupDocu.Show();
        LinkButton btnInfo = (LinkButton)sender;
        if (btnInfo != null)
        {
            this.ShowDetail(btnInfo.CommandArgument);
        }
        Page.SetFocus(txtDocumentNameDetail);
    }
    #endregion

    # region Protected Methods

    protected string GetDocumentName(string documentName)
    {
        return string.Format("Document {0}", Eval(documentName).ToString());
    }

    protected string GetDeleteDocName(string documentName)
    {
        return string.Format("Click to delete document {0}", Eval(documentName).ToString());
    }

    # endregion

    #region Private Methods

    private void InitializeDocu()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.ddlDomainName.DataSource = Domain.GetDomainsDropDownList(this.LoggedInUser);
            this.ddlDomainName.DataValueField = "DOMAIN_ID";
            this.ddlDomainName.DataTextField = "DOMAIN_NAME";
            this.ddlDomainName.DataBind();

            this.ddlDataFlow.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            this.ddlDataFlow.DataBind();

            this.ddlOperationName.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            this.ddlOperationName.DataBind();

            this.dtStart.Text = DateTime.Now.AddDays(-7.0).ToShortDateString();
            this.dtEnd.Text = DateTime.Now.ToShortDateString(); ;

            //this.btnDocPanel.Visible = false;

            this.ddlDataFlowName.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            this.ddlDataFlowName.DataBind();

            this.lkbTop5Docu_Click(null, null);
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = ex.ToString();
            this.lblError.Text = ex.Message;
            this.lblError.Visible = true;
        }
    }
    private void ShowDetail(string fileID)
    {
        if (!string.IsNullOrEmpty(fileID))
        {
            ViewState["FileID"] = fileID;
        }
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            //this.ddlDataFlowName.DataSource = Operation.GetUniqueOperationNames(this.LoggedInUser);
            //this.ddlDataFlowName.DataBind();

            int fID = 0;
            if (Int32.TryParse(fileID, out fID))
            {
                NodeDocument doc = new NodeDocument(fID);
                doc.RefreshFromDB();
                this.txtDocumentNameDetail.Text = doc.FileName;
                this.txtDocumentType.Text = doc.FileType;
                this.txtTransIDDetail.Text = doc.TransactionID;
                this.txtStatus.Text = doc.Status;
                if (doc.DataFlow != null && !doc.DataFlow.Trim().Equals("") && this.ddlDataFlowName.Items.FindByValue(doc.DataFlow) != null)
                {
                    this.ddlDataFlowName.SelectedValue = doc.DataFlow;
                }
                    this.mlDomainName.MsgContent = doc.Domain;
                this.mlSubmitURL.MsgContent = doc.SubmitURL;
                this.mlSubmitToken.MsgContent = doc.SubmitToken;
                this.mlSubmitDate.MsgContent = doc.SubmitDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                this.mlFileSize.MsgContent = "" + doc.FileSize + " bytes";
            }
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //throw ex;
            //this.lblError.Text = "System error." + Environment.NewLine + ex.ToString();
            this.lblError.Text = "System error." + Environment.NewLine + ex.Message;
            this.lblError.Visible = true;
        }
    }

    private byte[] GetZippedContent(string[] ids)
    {
        byte[] content = null;
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            MemoryStream ms = new MemoryStream();
            ZipOutputStream zos = new ZipOutputStream(ms);
            foreach (string id in ids)
            {
                NodeDocument doc = new NodeDocument(int.Parse(id));
                doc.RefreshFromDB();
                ZipEntry entry = new ZipEntry(doc.FileName);
                zos.PutNextEntry(entry);
                byte[] temp = new byte[(int)doc.FileContent.Length];
                doc.FileContent.Position = 0;
                doc.FileContent.Read(temp, 0, temp.Length);
                zos.Write(temp, 0, temp.Length);
            }
            zos.Finish();
            content = new byte[(int)ms.Length];
            ms.Position = 0;
            ms.Read(content, 0, content.Length);
            zos.Close();
        }
        catch (Exception ex)
        {
            this.HandleException(ex);
            //this.lblError.Text = ex.ToString();
            this.lblError.Text = ex.Message;
            this.lblError.Visible = true;
        }

        return content;
    }

    #endregion
}
