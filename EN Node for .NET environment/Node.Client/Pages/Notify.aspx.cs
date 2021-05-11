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

public partial class Notify : Node.Core.UI.Base.ClientPageBase
{
    protected bool ShowResult = false;
    protected bool ShowResult2 = false;

    public Notify()
    {
        Load += new EventHandler(Page_Load);
        Init += new EventHandler(Notify_Init);
    }

    void Notify_Init(object sender, EventArgs e)
    {
        string[] sFormateType = Enum.GetNames(typeof(Node.Core2.Requestor.DocumentFormatType));
        foreach (string str in sFormateType)
        {
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
            this.ShowResult2 = false;
            // Set Token
            string token = this.GetSavedToken();
            if (token != null)
            {
                this.txtToken.Text = token;
                this.txtToken2.Text = token;
            }
        }
        this.ClientLeftPanel.HighLighter(8);
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
        if (fuValid)
        {
            string url = this.NodeURLsPanel.GetNodeURL();
            if (url != null)
            {
                this.SaveNodeURL(url);
                NodeRequestor requestor = new NodeRequestor(url);
                try
                {
                    this.lblResult.Text = requestor.Notify(this.txtToken.Text, this.txtNodeAddress.Text, this.txtDataFlow.Text, this.GetInputDocuments());
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

            Node.Core2.Requestor.NodeRequestor req = new Node.Core2.Requestor.NodeRequestor(url);
            Node.Core2.Requestor.Notify noftifyobj = new Node.Core2.Requestor.Notify();
            noftifyobj.securityToken = this.txtToken2.Text;
            noftifyobj.nodeAddress = this.txtNodeAddress2.Text;
            noftifyobj.dataflow = this.txtDataFlow2.Text;          

            Node.Core2.Requestor.NotificationMessageType nMsg = new Node.Core2.Requestor.NotificationMessageType();
            Node.Core2.Requestor.NotificationMessageType[] msgs = new Node.Core2.Requestor.NotificationMessageType[] { nMsg };
            noftifyobj.messages = msgs;


            switch(this.drpMsgType.SelectedValue.ToString())
            {
                case "DOCUMENT":
                    noftifyobj.messages[0].messageCategory = Node.Core2.Requestor.NotificationMessageCategoryType.Document;
                    break;
                case "EVENT":
                    noftifyobj.messages[0].messageCategory = Node.Core2.Requestor.NotificationMessageCategoryType.Event;
                    break;
                case "STATUS":
                    noftifyobj.messages[0].messageCategory = Node.Core2.Requestor.NotificationMessageCategoryType.Status;
                    break;
            }

            noftifyobj.messages[0].messageName = this.txtMsgName.Text;
            noftifyobj.messages[0].objectId = this.txtObjID.Text;

            switch (this.drpStatus.SelectedValue.ToString())
            {
                case "APPROVED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Approved;
                    break;
                case "CANCELLED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Cancelled;
                    break;
                case "COMPLETED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Completed;
                    break;
                case "FAILED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Failed;
                    break;
                case "PENDING":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Pending;
                    break;
                case "PROCESSED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Processed;
                    break;
                case "PROCESSING":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Processing;
                    break;
                case "RECEIVED":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Received;
                    break;
                case "UNKNOWN":
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Unknown;
                    break;
                default:
                    noftifyobj.messages[0].status = Node.Core2.Requestor.TransactionStatusCode.Unknown;
                    break;
            }

            try
            {
                Node.Core2.Requestor.StatusResponseType resp = req.Notify(noftifyobj);
                this.lblResult2.Text = "Transaction ID:"+ resp.transactionId +"<br>Response Status:" + resp.status + "<br>Detail:" + resp.statusDetail;
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
}
