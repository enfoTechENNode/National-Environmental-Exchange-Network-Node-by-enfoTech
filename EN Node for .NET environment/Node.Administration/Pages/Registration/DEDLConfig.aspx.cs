using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Core.Biz.Objects;
using System.IO;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

public partial class Pages_Registration_DEDLConfig : Node.Core.UI.Base.AdminPageBase
{
    private DEDL _DEDL
    {
        get
        {
            return (DEDL)Session["DEDL"];
        }
        set
        {
            if (Session["DEDL"] != null)
            {
                Session["DEDL"] = value;
            }
            else
            {
                Session.Add("DEDL", value);
            }
        }

    }

    private DEDLDataElement _DataElement
    {
        get
        {
            return (DEDLDataElement)Session["DataElement"];
        }
        set
        {
            if (Session["DataElement"] != null)
            {
                Session["DataElement"] = value;
            }
            else
            {
                Session.Add("DataElement", value);
            }
        }

    }

    private DEDLProperty _Property
    {
        get
        {
            return (DEDLProperty)Session["DEDLElementValue"];
        }
        set
        {
            if (Session["DEDLElementValue"] != null)
            {
                Session["DEDLElementValue"] = value;
            }
            else
            {
                Session.Add("DEDLElementValue", value);
            }
        }

    }

    private DEDLElementValue _ElementValue
    {
        get
        {
            return (DEDLElementValue)Session["DEDLElementValue"];
        }
        set
        {
            if (Session["DEDLElementValue"] != null)
            {
                Session["DEDLElementValue"] = value;
            }
            else
            {
                Session.Add("DEDLElementValue", value);
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _DEDL = null;
            _DataElement = null;
            InitialDEDL();
        }
    }

    private void InitialDEDL()
    {
        _DEDL = new DEDL();

        List<DEDLDataElement> DataElementList = _DEDL.GetDataElements();
        egvDEList.DataSource = DataElementList;
        egvDEList.DataBind();
        FormSectionViewable(false);
    }

    private void SetValueForDataElement()
    {
        if (_DataElement == null)
            return;

        lblElementIdentifier2.Text = _DataElement.ElementIdentifier;
        txtApplicationDomain.Text = _DataElement.ApplicationDomain;
        txtElementType.Text = _DataElement.ElementType;
        txtDescription.Text = _DataElement.Description;
        txtKeywords.Text = _DataElement.Keywords;
        txtOwner.Text = _DataElement.Owner;
        txtElementLabel.Text = _DataElement.ElementLabel;
        txtDefaultValue.Text = _DataElement.DefaultValue;
        txtLastUpdated.Text = _DataElement.LastUpdated;

        txtAllowMultiSelect.Text = _DataElement.AllowMultiSelect;
        txtAdditionalValuesIndicator.Text = _DataElement.AdditionalValuesIndicator;
        txtOptionality.Text = _DataElement.Optionality;
        txtWildcard.Text = _DataElement.Wildcard;
        txtFormatString.Text = _DataElement.FormatString;
        txtValidationRules.Text = _DataElement.ValidationRules;

        egvProperty.DataSource = _DataElement.Properties;
        egvProperty.DataBind();

        egvElementValue.DataSource = _DataElement.ElementValues;
        egvElementValue.DataBind();
    }

    private void GetValueForDataElement()
    {
        if (_DataElement == null)
            return;
        _DataElement.ApplicationDomain = txtApplicationDomain.Text;
        _DataElement.ElementType = txtElementType.Text;
        _DataElement.Description = txtDescription.Text;
        _DataElement.Keywords = txtKeywords.Text;
        _DataElement.Owner = txtOwner.Text;
        _DataElement.ElementLabel = txtElementLabel.Text;
        _DataElement.DefaultValue = txtDefaultValue.Text;
        _DataElement.LastUpdated = txtLastUpdated.Text;

        _DataElement.AllowMultiSelect = txtAllowMultiSelect.Text;
        _DataElement.AdditionalValuesIndicator = txtAdditionalValuesIndicator.Text;
        _DataElement.Optionality = txtOptionality.Text;
        _DataElement.Wildcard = txtWildcard.Text;
        _DataElement.FormatString = txtFormatString.Text;
        _DataElement.ValidationRules = txtValidationRules.Text;
    }

    private void FormSectionViewable(bool view)
    {
        this.FormSectionBlock3.Visible = view;
        this.FormSectionBlock4.Visible = view;
        this.FormSectionBlock5.Visible = view;
        this.FormSectionBlock6.Visible = view;
        this.pnlAddElementValue.Visible = view;
        this.pnlAddProperty.Visible = view;

    }

    private void CleanScreen()
    {
        lblElementIdentifier2.Text = string.Empty;
        txtApplicationDomain.Text = string.Empty;
        txtElementType.Text = string.Empty;
        txtDescription.Text = string.Empty;
        txtKeywords.Text = string.Empty;
        txtOwner.Text = string.Empty;
        txtElementLabel.Text = string.Empty;
        txtDefaultValue.Text = string.Empty;
        txtLastUpdated.Text = string.Empty;

        txtAllowMultiSelect.Text = string.Empty;
        txtAdditionalValuesIndicator.Text = string.Empty;
        txtOptionality.Text = string.Empty;
        txtWildcard.Text = string.Empty;
        txtFormatString.Text = string.Empty;
        txtValidationRules.Text = string.Empty;

        egvProperty.DataSource = null;
        egvProperty.DataBind();

        egvElementValue.DataSource = null;
        egvElementValue.DataBind();

        txtAddElementIdentifier.Text = string.Empty;
        txtPropertyName.Text = string.Empty;
        txtPropertyValue.Text = string.Empty;
        txtValueLabel.Text = string.Empty;
        txtElementValue.Text = string.Empty;
        MsgError.setMessage("");
        MsgDE.setMessage("");
        msgEV.setMessage("");
        msgP.setMessage("");
    }

    protected void btnBackToDashboard_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        GetValueForDataElement();
        if (_DEDL.Save())
        {
            MsgError.setMessage("Saved Successfully");
        }
        else
        {
            MsgError.setMessage("System Error: DEDL info is not saved"); ;
        }

    }

    protected void btnAddElement_OnClick(object sender, EventArgs e)
    {
        MsgDE.setMessage("");
        mpeAddDataElement.Show();
    }

    protected void btnAddProperty_OnClick(object sender, EventArgs e)
    {
        PopHeaderProperty.InnerText = "Add Property";
        msgP.setMessage("");
        btnSaveProperty.Visible = false;
        btnSaveAddProperty.Visible = true;
        txtPropertyName.ReadOnly = false;

        txtPropertyName.Text = "";
        txtPropertyValue.Text = "";

        mpeAddProperty.Show();
    }

    protected void btnAddElementValue_OnClick(object sender, EventArgs e)
    {
        PopHeaderElementValue.InnerText = "Add Element Value";
        msgEV.setMessage("");
        btnSaveElementValue.Visible = false;
        btnSaveAddElementValue.Visible = true;
        txtValueLabel.ReadOnly = false;
        txtValueLabel.Text = "";
        txtElementValue.Text = "";

        mpeAddElementValue.Show();

    }

    protected void btnSaveAddDataElement_OnClick(object sender, EventArgs e)
    {
        GetValueForDataElement();
        if (txtAddElementIdentifier.Text != "" && _DEDL.AddDataElement(txtAddElementIdentifier.Text))
        {
            egvDEList.DataSource = _DEDL.GetDataElements();
            egvDEList.DataBind();
            _DataElement = _DEDL.GetDataElement(txtAddElementIdentifier.Text);
            SetValueForDataElement();
            FormSectionViewable(true);
        }
        else
        {
            MsgDE.setMessage("The Data Element is existed in the current DEDL file!");
            mpeAddDataElement.Show();
        }
        txtAddElementIdentifier.Text = "";

    }

    protected void btnSaveAddProperty_OnClick(object sender, EventArgs e)
    {
        if (_DataElement == null)
            return;

        if (txtPropertyName.Text != "" && _DataElement.AddProperty(txtPropertyName.Text, txtPropertyValue.Text))
        {
            egvProperty.DataSource = _DataElement.Properties;
            egvProperty.DataBind();
        }
        else
        {
            msgP.setMessage("The property is existed in the current Data Element!");
            mpeAddProperty.Show();
        }
        txtPropertyName.Text = "";
        txtPropertyValue.Text = "";
    }

    protected void btnSaveAddElementValue_OnClick(object sender, EventArgs e)
    {
        if (_DataElement == null)
            return;
        if (txtValueLabel.Text != "" && _DataElement.AddElementValue(txtValueLabel.Text, txtElementValue.Text))
        {
            egvElementValue.DataSource = _DataElement.ElementValues;
            egvElementValue.DataBind();
        }
        else
        {
            msgEV.setMessage("The element value is existed in the current Data Element!");
            mpeAddElementValue.Show();
        }
        txtValueLabel.Text = "";
        txtElementValue.Text = "";
    }

    protected void egvDEList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewDataElement":
                GetValueForDataElement();
                _DataElement = _DEDL.GetDataElement("" + e.CommandArgument);
                SetValueForDataElement();
                if (!this.FormSectionBlock3.Visible)
                    FormSectionViewable(true);
                break;
            case "DeleteDataElement":
                _DataElement = _DEDL.GetDataElement("" + e.CommandArgument);
                _DataElement.Delete();
                _DataElement = null;

                egvDEList.DataSource = _DEDL.GetDataElements(); ;
                egvDEList.DataBind();
                FormSectionViewable(false);
                CleanScreen();

                break;
        }
    }

    protected void egvProperty_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewProerty":
                PopHeaderProperty.InnerText = "Property Info";
                txtPropertyName.ReadOnly = true;
                btnSaveProperty.Visible = true;
                btnSaveAddProperty.Visible = false;
                _Property = _DataElement.GetProperty("" + e.CommandArgument);
                txtPropertyName.Text = _Property.PropertyName;
                txtPropertyValue.Text = _Property.PropertyValue;
                mpeAddProperty.Show();
                break;
            case "DeleteProperty":
                _Property = _DataElement.GetProperty("" + e.CommandArgument);
                _Property.Delete();
                _Property = null;
                egvProperty.DataSource = _DataElement.Properties;
                egvProperty.DataBind();
                break;
        }

    }

    protected void egvElementValue_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "ViewElement":
                PopHeaderElementValue.InnerText = "Element Value Info";
                btnSaveElementValue.Visible = true;
                btnSaveAddElementValue.Visible = false;
                txtValueLabel.ReadOnly = true;
                _ElementValue = _DataElement.GetElementValue("" + e.CommandArgument);
                txtValueLabel.Text = _ElementValue.ValueLabel;
                txtElementValue.Text = _ElementValue.ElementValue;

                mpeAddElementValue.Show();
                break;
            case "DeleteElement":
                _ElementValue = _DataElement.GetElementValue("" + e.CommandArgument);
                _ElementValue.Delete();
                _ElementValue = null;

                egvElementValue.DataSource = _DataElement.ElementValues;
                egvElementValue.DataBind();

                break;
        }

    }

    protected void btnSaveProperty_OnClick(object sender, EventArgs e)
    {
        _Property.PropertyValue = txtPropertyValue.Text;
        _Property = null;
        egvProperty.DataSource = _DataElement.Properties;
        egvProperty.DataBind();

    }

    protected void btnSaveElementValue_OnClick(object sender, EventArgs e)
    {
        _ElementValue.ElementValue = txtElementValue.Text;
        _ElementValue = null;
        egvElementValue.DataSource = _DataElement.ElementValues;
        egvElementValue.DataBind();

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        if (fuUpload.FileBytes.Length > 0)
        {
            MemoryStream ms = new MemoryStream(fuUpload.FileBytes);
            StreamReader sr = new StreamReader(ms);
            ms.Position = 0;
            IConfigurations db = new DBManager().GetConfigurationsDB();
            string dedl = sr.ReadToEnd();
            db.UpdateDEDL(dedl);
            ms.Close();
            sr.Close();

            this.Response.Redirect("~/Pages/Registration/DEDLConfig.aspx", false);
        }
    }
    
}
