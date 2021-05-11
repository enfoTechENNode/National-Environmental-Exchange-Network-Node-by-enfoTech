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

using Node.Core.API;
using Node.Core.Document;

public partial class Download : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected NodeDocument[] ReturnDocs = null;

    public Download()
	{
		Load += new EventHandler(Page_Load);
        Init += new EventHandler(Download_Init);
	}

    void Download_Init(object sender, EventArgs e)
    {
        string[] sFormateType = Enum.GetNames(typeof(Node.Core2.Requestor.DocumentFormatType));
        foreach (string str in sFormateType)
        {
            this.drpFileType1.Items.Add(new ListItem(str, str));
            this.drpFileType2.Items.Add(new ListItem(str, str));
            this.drpFileType3.Items.Add(new ListItem(str, str));

            this.DropDownList1.Items.Add(new ListItem(str, str));
            this.DropDownList2.Items.Add(new ListItem(str, str));
            this.DropDownList3.Items.Add(new ListItem(str, str));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        NodeTab1.SetPageAjaxTabControl = this.TabContainer1;
        if (!this.IsPostBack)
        {
            this.ShowResult = false;
            
            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken.Text = token;
                this.txtToken2.Text = token;
            }
        }
        this.ClientLeftPanel.HighLighter(5);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        this.ReturnDocs = null;
        this.lblResultN.Text = "";
        this.ShowResult = true;
        string url = this.NodeURLsPanel.GetNodeURL();
        
        if (url != null)
        {          
            this.SaveNodeURL(url);
            RemoveDocumentsFromSession();
            NodeRequestor requestor = new NodeRequestor(url);
            try
            {
                this.ReturnDocs = requestor.Download(this.txtToken.Text, this.txtTransID.Text, this.txtDataFlow.Text, this.GetInputDocuments());

                for (int i = 0; this.ReturnDocs != null && i < this.ReturnDocs.Length; i++)
                    this.Session["RETURN_DOCS" + i] = this.ReturnDocs[i];
              
                string result = "Downloaded ";
                if (this.ReturnDocs == null)
                    result += "0 Documents";
                else
                    result += this.ReturnDocs.Length + " Documents";
                //this.lblResult.Text = result;
                this.lblResultN.Text = this.DisplayResult(result);
                this.lblResultN.Visible = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper() == "INVALID TRANSACTION ID.")
                    this.lblResultN.Text = Environment.NewLine + ex.Message;
                else
                    //this.lblResultN.Text = "System error." + Environment.NewLine + ex.ToString();
                    this.lblResultN.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResultN.Text = "Please Enter a Node URL";

        this.TabContainer1.ActiveTabIndex = 0;
        SetCurrentActiveTab(0);
    }

    private string DisplayResult(string Result)
    {
        StringBuilder sb = new StringBuilder();
        if (this.ShowResult)
        {
            sb.Append("<br />");
            sb.Append("<table class=\"cc_ResultTable\" width=\"500\">");
            sb.Append("<tr valign=\"top\"><th colspan=\"2\" align=\"left\">Download Result</th></tr>");
            if (this.lblResult.Visible)
            {
                sb.Append("<tr class=\"alt1\"><td colspan=\"2\">");
                sb.AppendFormat("{0}", Result);
                sb.Append("</td></tr>");
            }
            sb.Append("<tr valign=\"top\"><td colspan=\"2\">&nbsp;</td></tr>");
            sb.Append("<tr class=\"alt1\"><td width=\"350\" align=\"center\">File Name</td><td width=\"50\" align=\"center\">Type</td></tr>");
            NodeDocument doc = (NodeDocument)this.Session["RETURN_DOCS0"];
            int count = 0;
            while (doc != null)
            {
                sb.Append("<tr class=\"alt1\">");
                sb.Append("<td>");
                sb.AppendFormat("<a href=\"DownloadContent.aspx?returnDocI={0}\">{1}</a>", count, doc.name);
                sb.Append("</td><td align=\"center\">");
                sb.AppendFormat("{0}", doc.type);
                sb.Append("</td></tr>");
                count++;
                doc = (NodeDocument)this.Session["RETURN_DOCS" + count];
            }
            sb.Append("</table>");
        }
        return sb.ToString();
    }

    private NodeDocument[] GetInputDocuments()
    {
        NodeDocument[] retDocs = null;
        if (!this.txtFileName1.Text.Trim().Equals("") && !this.DropDownList1.SelectedValue.ToString().Trim().Equals(""))
        {
            NodeDocument doc = new NodeDocument();
            if (!this.txtFileName1.Text.Trim().Equals("")) doc.name = this.txtFileName1.Text.Trim();
            if (!this.DropDownList1.SelectedValue.ToString().Trim().Equals("")) doc.type = this.DropDownList1.SelectedValue.ToString().Trim();
            retDocs = new NodeDocument[] { doc };
        }
        if (!this.txtFileName2.Text.Trim().Equals("") && !this.DropDownList2.SelectedValue.ToString().Trim().Equals(""))
        {
            NodeDocument doc = new NodeDocument();
            if (!this.txtFileName2.Text.Trim().Equals("")) doc.name = this.txtFileName2.Text.Trim();
            if (!this.DropDownList2.SelectedValue.ToString().Trim().Equals("")) doc.type = this.DropDownList2.SelectedValue.ToString().Trim();
            if (retDocs == null)
                retDocs = new NodeDocument[] { doc };
            else
            {
                NodeDocument[] old = retDocs;
                retDocs = new NodeDocument[2];
                retDocs[0] = old[0];
                retDocs[1] = doc;
            }
        }
        if (!this.txtFileName3.Text.Trim().Equals("") && !this.DropDownList3.SelectedValue.ToString().Trim().Equals(""))
        {
            NodeDocument doc = new NodeDocument();
            if (!this.txtFileName3.Text.Trim().Equals("")) doc.name = this.txtFileName3.Text.Trim();
            if (!this.DropDownList3.SelectedValue.ToString().Trim().Equals("")) doc.type = this.DropDownList3.SelectedValue.ToString().Trim();
            if (retDocs == null)
                retDocs = new NodeDocument[] { doc };
            else
            {
                NodeDocument[] old = retDocs;
                retDocs = new NodeDocument[old.Length + 1];
                for (int i = 0; i < old.Length; i++)
                    retDocs[i] = old[i];
                retDocs[old.Length] = doc;
            }
        }
        return retDocs;
    }

    private void RemoveDocumentsFromSession()
    {
        NodeDocument doc = null;
        int count = 0;
        //while (doc != null)
        //{
        //    doc = (NodeDocument)this.Session["RETURN_DOCS" + count];
        //    if (doc != null)
        //    { 
        //       this.Session.Remove("RETURN_DOCS" + count); 
        //    }
        //    count++;
        //}
        do
        {
            doc = (NodeDocument)this.Session["RETURN_DOCS" + count];
            if (doc != null)
            {
                this.Session.Remove("RETURN_DOCS" + count);
            }
            count++;

        } while (doc != null);
    }

    private void RemoveDocumentsFromSession2()
    {
        Node.Core2.Requestor.NodeDocumentType doc = null;
        int count = 0;
        //while (doc != null)
        //{
        //    doc = (NodeDocument)this.Session["RETURN_DOCS" + count];
        //    if (doc != null)
        //    { 
        //       this.Session.Remove("RETURN_DOCS" + count); 
        //    }
        //    count++;
        //}
        do
        {
            doc = this.Session["RETURN_DOCS_TYPE" + count] as Node.Core2.Requestor.NodeDocumentType;
            if (doc != null)
            {
                this.Session.Remove("RETURN_DOCS_TYPE" + count);
            }
            count++;

        } while (doc != null);
    }


    protected void btnDownload2_Click(object sender, EventArgs e)
    {
        Node.Core2.Requestor.NodeDocumentType[] retDocs = null;
        this.lblResultN2.Text = "";
        this.ShowResult = true;
        string url = this.NodeURLsPanel2.GetNodeURL();

        if (url != null)
        {
            this.SaveNodeURL(url);
            RemoveDocumentsFromSession2();
            Node.Core2.Requestor.NodeRequestor requestor = new Node.Core2.Requestor.NodeRequestor(url);
            try
            {
                Node.Core2.Requestor.Download download = new Node.Core2.Requestor.Download();
                download.dataflow = this.txtDataFlow2.Text;
                download.securityToken = this.txtToken2.Text;
                download.transactionId = this.txtTransID2.Text;
                download.documents = GetInputNodeDocumentType();
                //retDocs = new Node.Core2.Requestor.NodeDocumentType[1];
                //Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
                //retDocs[0] = doc;
                //doc.documentId = "5264cd69-f51a-42b2-8882-7b1331ddb9e4";
                //download.documents = retDocs;

                retDocs = requestor.Download(download);
                for (int i = 0; retDocs != null && i < retDocs.Length; i++)
                    this.Session["RETURN_DOCS_TYPE" + i] = retDocs[i];

                string result = "Downloaded ";
                if (retDocs == null)
                    result += "0 Documents";
                else
                    result += retDocs.Length + " Documents";
                //this.lblResult.Text = result;
                this.lblResultN2.Text = this.DisplayNodeDocTypeResult(result);
                this.lblResultN2.Visible = true;
            }
            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    this.lblResultN2.Text = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    this.lblResultN2.Text = "System error." + Environment.NewLine + sex.Message;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToUpper() == "INVALID TRANSACTION ID.")
                    this.lblResultN2.Text = Environment.NewLine + ex.Message;
                else
                    //this.lblResultN2.Text = "System error." + Environment.NewLine + ex.ToString();
                    this.lblResultN2.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResultN2.Text = "Please Enter a Node URL";
        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

    protected Node.Core2.Requestor.NodeDocumentType[] GetInputNodeDocumentType()
    {
        Node.Core2.Requestor.NodeDocumentType[] retDocs = null;
        ArrayList arr = new ArrayList();

        if (this.txtFile2Name1.Text.Trim() != String.Empty && this.drpFileType1.SelectedValue.ToString().Trim() != String.Empty)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.txtFile2Name1.Text.Trim();

            if (this.drpFileType1.SelectedValue.ToString().Trim() != String.Empty)
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType1.SelectedValue.ToString().Trim());
            else
            {
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            }
            GetDocumentContentType(doc);
            arr.Add(doc);
        }
        if (this.txtFile2Name2.Text.Trim() != String.Empty && this.drpFileType2.SelectedValue.ToString().Trim() != String.Empty)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.txtFile2Name2.Text.Trim();

            if (drpFileType2.SelectedValue.ToString().Trim() != String.Empty)
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType2.SelectedValue.ToString().Trim());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            GetDocumentContentType(doc);
            arr.Add(doc);
        }
        if (this.txtFile2Name3.Text.Trim() != String.Empty && this.drpFileType3.SelectedValue.ToString().Trim() != String.Empty)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.txtFile2Name3.Text.Trim();

            if (this.drpFileType3.SelectedValue.ToString().Trim() != String.Empty)
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType3.SelectedValue.ToString().Trim());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            GetDocumentContentType(doc);
            arr.Add(doc);
        }

        int i = 0;
        retDocs = new Node.Core2.Requestor.NodeDocumentType[arr.Count];
        foreach (Node.Core2.Requestor.NodeDocumentType type in arr)
            retDocs[i++] = type;

        return retDocs;
    }

    private void GetDocumentContentType(Node.Core2.Requestor.NodeDocumentType doc)
    {
        doc.documentContent = new Node.Core2.Requestor.AttachmentType();
        switch (doc.documentFormat)
        {
            case Node.Core2.Requestor.DocumentFormatType.BIN: doc.documentContent.contentType = "application/octet-stream"; break;
            case Node.Core2.Requestor.DocumentFormatType.FLAT: doc.documentContent.contentType = "text/plain"; break;
            case Node.Core2.Requestor.DocumentFormatType.XML: doc.documentContent.contentType = "text/xml"; break;
            case Node.Core2.Requestor.DocumentFormatType.ZIP: doc.documentContent.contentType = "application/zip"; break;
            default:
                doc.documentContent.contentType = "application/octet-stream"; break;
        }
    }

    private string DisplayNodeDocTypeResult(string Result)
    {
        StringBuilder sb = new StringBuilder();
        if (this.ShowResult)
        {
            sb.Append("<br />");
            sb.Append("<table class=\"cc_ResultTable\" width=\"500\">");
            sb.Append("<tr valign=\"top\"><th colspan=\"2\" align=\"left\">Download Result</th></tr>");
            if (this.lblResult.Visible)
            {
                sb.Append("<tr class=\"alt1\"><td colspan=\"2\">");
                sb.AppendFormat("{0}", Result);
                sb.Append("</td></tr>");
            }
            sb.Append("<tr valign=\"top\"><td colspan=\"2\">&nbsp;</td></tr>");
            sb.Append("<tr class=\"alt1\"><td width=\"350\" align=\"center\">File Name</td><td width=\"50\" align=\"center\">Type</td></tr>");
            Node.Core2.Requestor.NodeDocumentType doc = (Node.Core2.Requestor.NodeDocumentType)this.Session["RETURN_DOCS_TYPE0"];
            int count = 0;
            while (doc != null)
            {
                sb.Append("<tr class=\"alt1\">");
                sb.Append("<td>");
                sb.AppendFormat("<a href=\"DownloadContent.aspx?returnDocTypeI={0}\">{1}</a>", count, doc.documentName);
                sb.Append("</td><td align=\"center\">");
                sb.AppendFormat("{0}", doc.documentFormat);
                sb.Append("</td></tr>");
                count++;
                doc = (Node.Core2.Requestor.NodeDocumentType)this.Session["RETURN_DOCS_TYPE" + count];
            }
            sb.Append("</table>");
        }
        return sb.ToString();
    }

}
