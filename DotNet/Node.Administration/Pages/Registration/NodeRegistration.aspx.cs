using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Core.Biz.Objects;
using Node.Core;

public partial class Pages_Registration_NodeRegistration : Node.Core.UI.Base.AdminPageBase
{
    private ENDSServiceRegistration _ENDS 
    {
        get
        {
            return (ENDSServiceRegistration)Session["ENDSV20SR"];
        }
        set
        {
            if (Session["ENDSV20SR"] != null)
            {
                Session["ENDSV20SR"] = value;
            }
            else
            {
                Session.Add("ENDSV20SR", value);   
            }
        }
 
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialENDS();
        }
    }

    private void InitialENDS()
    {
        _ENDS = new ENDSServiceRegistration();

        switch (this.Session[Phrase.VERSION_NO].ToString())
        { 
            case Phrase.VERSION_11:
                _ENDS.NodeVersionIdentifier = "1.1";
                break;
            case Phrase.VERSION_20:
                _ENDS.NodeVersionIdentifier = "2.0";
                break;
        }

        txtNodeIdentifier.Text = _ENDS.NodeIdentifier;
        txtNodeName.Text = _ENDS.NodeName;
        txtNodeURL.Text = _ENDS.NodeAddress;
        txtOrgIdentifier.Text = _ENDS.OrganizationIdentifier;
        txtNodeContact.Text = _ENDS.NodeContact;
        txtDeployTypeCD.Text = _ENDS.NodeDeploymentTypeCode;
        txtStatus.Text =  _ENDS.NodeStatus ;
        txtEast.Text = _ENDS.BoundingCoordinateEast;
        txtNorth.Text = _ENDS.BoundingCoordinateNorth;
        txtSouth.Text =_ENDS.BoundingCoordinateSouth;
        txtWest.Text = _ENDS.BoundingCoordinateWest;
    }

    protected void btnBackToDashboard_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/Pages/Main/Home.aspx", false);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _ENDS.NodeIdentifier = txtNodeIdentifier.Text;
        _ENDS.NodeName = txtNodeName.Text;
        _ENDS.NodeAddress =txtNodeURL.Text;
        _ENDS.OrganizationIdentifier =txtOrgIdentifier.Text;
        _ENDS.NodeContact = txtNodeContact.Text ;
        _ENDS.NodeDeploymentTypeCode = txtDeployTypeCD.Text;
        _ENDS.NodeStatus = txtStatus.Text;
        _ENDS.BoundingCoordinateEast = txtEast.Text;
        _ENDS.BoundingCoordinateNorth = txtNorth.Text;
        _ENDS.BoundingCoordinateSouth = txtSouth.Text;
        _ENDS.BoundingCoordinateWest = txtWest.Text;

        if (_ENDS.Save())
        {
            msgError.setMessage("Saved Successfully"); 
        }
        else
        {
            msgError.setMessage("System Error: Service Registration info is not saved");
        }

    }

    protected void btnDownLoad_OnClick(object sender, EventArgs e)
    {
        ENDSServiceRegistration ends = new ENDSServiceRegistration();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("Content-Disposition", "attachment;filename=ends.xml");
        Response.ContentType = "application/xml";
        Response.Charset = "";
        Response.Write(ends.BuildENDS());
        Response.Flush();
        Response.End();
        Response.Write( ends.BuildENDS());           
    }
}