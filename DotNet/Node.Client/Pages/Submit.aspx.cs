using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services.Protocols;

using Node.Core.API;
using Node.Core.Document;
using System.Xml.Serialization;
using System.Text;

public partial class Submit : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;

    public Submit()
	{
		Load += new EventHandler(Page_Load);
        Init += new EventHandler(Submit_Init);
	}

    void Submit_Init(object sender, EventArgs e)
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

        string[] sNotificationType = Enum.GetNames(typeof(Node.Core2.Requestor.NotificationTypeCode));
        foreach (string str in sNotificationType)
        {
            this.DropDownList4.Items.Add(new ListItem(str, str));
        }
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
        }
        this.ClientLeftPanel.HighLighter(4);
        this.TabContainer1.ActiveTabIndex = GetLastActiveTab();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.lblResult.Text = "";
        this.ShowResult = true;
        bool fuValid = true;

        if (this.file1.HasFile && this.file1.PostedFile.ContentType == "application/octet-stream")
        {
            this.file1Validate.IsValid = false;
            fuValid = false;
        }
        if (this.file2.HasFile && this.file2.PostedFile.ContentType == "application/octet-stream")
        {
            this.file2Validate.IsValid = false;
            fuValid = false;
        }
        if (this.file3.HasFile && this.file3.PostedFile.ContentType == "application/octet-stream")
        {
            this.file3Validate.IsValid = false;
            fuValid = false;
        }
        if (this.FileUpload1.HasFile && this.FileUpload1.PostedFile.ContentType == "application/octet-stream")
        {
            this.fu1Validate.IsValid = false;
            fuValid = false;
        }
        if (this.FileUpload2.HasFile && this.FileUpload2.PostedFile.ContentType == "application/octet-stream")
        {
            this.fu2Validate.IsValid = false;
            fuValid = false;
        }
        if (this.FileUpload3.HasFile && this.FileUpload3.PostedFile.ContentType == "application/octet-stream")
        {
            this.fu3Validate.IsValid = false;
            fuValid = false;
        }

        if (fuValid)
        {
            string url = this.NodeURLsPanel.GetNodeURL();
            if (url != null)
            {
                this.SaveNodeURL(url);
                NodeRequestor requestor = new NodeRequestor(url);
                try
                {
                    this.lblResult.Text = requestor.Submit(this.txtToken.Text, this.txtTransID.Text, this.txtDataFlow.Text, this.GetInputDocuments());
                }
                catch (Exception ex)
                {
                    //this.lblResult.Text = "System error." + Environment.NewLine + ex.ToString();
                    this.lblResult.Text = "System error." + Environment.NewLine + ex.Message;
                }
            }
            else
                this.lblResult.Text = "Please Enter a Node URL";

            this.TabContainer1.ActiveTabIndex = 0;
            SetCurrentActiveTab(0);
        }
        else
        {
            this.lblResult.Text = "Please do not upload the executable file!";
            this.lblResult.Visible = true;
        }
    }

    protected Node.Core.Document.NodeDocument[] GetInputDocuments()
    {
        Node.Core.Document.NodeDocument[] retDocs = null;
        Stream stream1 = this.file1.FileContent;
        Stream stream2 = this.file2.FileContent;
        Stream stream3 = this.file3.FileContent;
        byte[] content1 = this.file1.FileBytes;
        byte[] content2 = this.file2.FileBytes;
        byte[] content3 = this.file3.FileBytes;
        if (content1 != null && content1.Length > 0)
        {
            Node.Core.Document.NodeDocument doc = new Node.Core.Document.NodeDocument();
            doc.name = this.file1.FileName;
            if (!this.DropDownList1.SelectedValue.ToString().Trim().Equals(""))
                doc.type = this.DropDownList1.SelectedValue.ToString();
            else
                doc.type = this.file1.PostedFile.ContentType;
            //doc.Stream = stream1;
            doc.content = content1;
            retDocs = new Node.Core.Document.NodeDocument[] { doc };
        }
        if (content2 != null && content2.Length > 0)
        {
            Node.Core.Document.NodeDocument doc = new Node.Core.Document.NodeDocument();
            doc.name = this.file2.FileName;
            if (!this.DropDownList2.SelectedValue.ToString().Trim().Equals(""))
                doc.type = this.DropDownList2.SelectedValue.ToString();
            else
                doc.type = this.file2.PostedFile.ContentType;
            //doc.Stream = stream2;
            doc.content = content2;
            if (retDocs == null)
                retDocs = new Node.Core.Document.NodeDocument[] { doc };
            else
            {
                Node.Core.Document.NodeDocument[] temp = retDocs;
                retDocs = new Node.Core.Document.NodeDocument[] { temp[0], doc };
            }
        }
        if (content3 != null && content3.Length > 0)
        {
            Node.Core.Document.NodeDocument doc = new Node.Core.Document.NodeDocument();
            doc.name = this.file3.FileName;
            if (!this.DropDownList3.SelectedValue.ToString().Trim().Equals(""))
                doc.type = this.DropDownList3.SelectedValue.ToString();
            else
                doc.type = this.file3.PostedFile.ContentType;
            //doc.Stream = stream3;
            doc.content = content3;
            if (retDocs == null)
                retDocs = new Node.Core.Document.NodeDocument[] { doc };
            else
            {
                Node.Core.Document.NodeDocument[] temp = retDocs;
                retDocs = new Node.Core.Document.NodeDocument[temp.Length + 1];
                for (int i = 0; i < temp.Length; i++)
                    retDocs[i] = temp[i];
                retDocs[temp.Length] = doc;
            }
        }
        return retDocs;
    }

    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        this.lblResult2.Text = "";
        this.ShowResult2 = true;
        string url = this.NodeURLsPanel2.GetNodeURL();
        if (url != null)
        {
            this.SaveNodeURL(url);
            Node.Core2.Requestor.NodeRequestor requestor = new Node.Core2.Requestor.NodeRequestor(url);
            try
            {
                Node.Core2.Requestor.Submit submit = new Node.Core2.Requestor.Submit();
                submit.securityToken = this.txtToken2.Text;
                submit.transactionId = this.txtTransID2.Text;
                submit.dataflow = this.txtDataFlow2.Text;
                submit.flowOperation = this.txtFlowOperation.Text;

                submit.recipient = this.txtRecipient.Text.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                submit.notificationURI = new Node.Core2.Requestor.NotificationURIType[1];
                submit.notificationURI[0] = new Node.Core2.Requestor.NotificationURIType();
                submit.notificationURI[0].notificationType = (Node.Core2.Requestor.NotificationTypeCode)Enum.Parse(typeof(Node.Core2.Requestor.NotificationTypeCode), this.DropDownList4.SelectedValue.ToString());
                submit.notificationURI[0].Value = this.txtNotificationURI.Text.Trim();

                submit.documents = GetInputNodeDocumentType();
                Node.Core2.Requestor.StatusResponseType resp = requestor.Submit(submit);
                this.lblResult2.Text = resp.transactionId;
            }
            catch (SoapException sex)
            {
                if (sex.Detail != null)
                {
                    this.lblResult2.Text = "System error." + Environment.NewLine + sex.Detail.InnerText;
                }
                else
                {
                    this.lblResult2.Text = "System error." + Environment.NewLine + sex.Message;
                }

            }
            catch (Exception ex)
            {
                //this.lblResult2.Text = "System error." + Environment.NewLine + ex.ToString();
                this.lblResult2.Text = "System error." + Environment.NewLine + ex.Message;
            }
        }
        else
            this.lblResult2.Text = "Please Enter a Node URL";

        this.TabContainer1.ActiveTabIndex = 1;
        SetCurrentActiveTab(1);
    }

    protected Node.Core2.Requestor.NodeDocumentType[] GetInputNodeDocumentType()
    {
        Node.Core2.Requestor.NodeDocumentType[] retDocs = null;
        ArrayList arr = new ArrayList();
        byte[] content1 = this.FileUpload1.FileBytes;
        byte[] content2 = this.FileUpload2.FileBytes;
        byte[] content3 = this.FileUpload3.FileBytes;

        if (content1 != null && content1.Length > 0)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.FileUpload1.FileName;
            if (!this.drpFileType1.SelectedValue.ToString().Equals(""))
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType1.SelectedValue.ToString());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            doc.documentContent = new Node.Core2.Requestor.AttachmentType();
            doc.documentContent.Value = content1;
            arr.Add(doc);
        }
        if (content2 != null && content2.Length > 0)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.FileUpload2.FileName;
            if (!this.drpFileType2.SelectedValue.ToString().Equals(""))
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType2.SelectedValue.ToString());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            doc.documentContent = new Node.Core2.Requestor.AttachmentType();
            doc.documentContent.Value = content2;
            arr.Add(doc);
        }
        if (content3 != null && content3.Length > 0)
        {
            Node.Core2.Requestor.NodeDocumentType doc = new Node.Core2.Requestor.NodeDocumentType();
            doc.documentName = this.FileUpload3.FileName;
            if (!this.drpFileType3.SelectedValue.ToString().Equals(""))
                doc.documentFormat = (Node.Core2.Requestor.DocumentFormatType)Enum.Parse(typeof(Node.Core2.Requestor.DocumentFormatType), this.drpFileType3.SelectedValue.ToString());
            else
                doc.documentFormat = Node.Core2.Requestor.DocumentFormatType.XML;
            doc.documentContent = new Node.Core2.Requestor.AttachmentType();
            doc.documentContent.Value = content3;
            arr.Add(doc);
        }

        int i = 0;
        retDocs = new Node.Core2.Requestor.NodeDocumentType[arr.Count];
        foreach (Node.Core2.Requestor.NodeDocumentType type in arr)
            retDocs[i++] = type;

        return retDocs;
    }

    public string GetXML(Node.Core2.Requestor.Submit obj)
    {
        string sXML = "";
        XmlSerializer mySerializer = new XmlSerializer(typeof(Node.Core2.Requestor.Submit));

        MemoryStream ms = new MemoryStream();
        StreamWriter myWriter = new StreamWriter(ms, UTF8Encoding.UTF8);
        mySerializer.Serialize(myWriter, obj);

        ms.Position = 0;
        StreamReader sr = new StreamReader(ms);
        sXML = sr.ReadToEnd();
        myWriter.Close();
        return sXML;
    }



}
