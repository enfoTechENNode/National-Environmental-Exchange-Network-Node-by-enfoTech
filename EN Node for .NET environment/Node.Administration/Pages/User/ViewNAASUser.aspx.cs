using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Node.Core;
using Node.Core.Biz.Manageable;
using Node.Core.Biz.Objects;
using Node.Core.Logging;

public partial class ViewNAASUser_aspx : Node.Core.UI.Base.AdminPageBase
{
    private NAASUser user = null;

    public ViewNAASUser_aspx()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.egvOperations.RowDataBound += new GridViewRowEventHandler(egvOperations_RowDataBound);
        if (!this.IsPostBack)
        {
            this.Session[Phrase.USER_SELECTED_PRIVILEDGES] = null;
            this.Session[Phrase.USER_UNSELECTED_PRIVILEDGES] = null;
            this.PageControlsInit();
        }
        if (this.user == null && !this.txtLoginName.Text.Trim().Equals(""))
            this.user = new NAASUser(this.txtLoginName.Text, true);
        setCurrentCheckedHT();
    }

    #region Event Handlers

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            this.lblError.Visible = false;
            NAASUser u = new NAASUser(this.txtLoginName.Text, true);
            u.UserName = this.txtLoginName.Text;
            u.FirstName = this.txtFirstName.Text;
            u.MiddleInitial = this.txtMidInitial.Text;
            u.LastName = this.txtLastName.Text;
            u.Address = this.txtAddress.Text;
            u.LocalityName = this.txtCity.Text;
            u.StateUSPSCode = this.txtState.Text;
            u.ZipCode = this.zctbZip.Text;
            u.CountryCode = this.txtCountry.Text;
            u.PhoneNumber = this.ptbPhone.Text;
            string[] names = this.egvOperations.GetCheckedValue("gcbfOperation");

            ArrayList operations = new ArrayList();
            Hashtable checkedHT = new Hashtable();

            if (this.Session[Phrase.USER_SELECTED_PRIVILEDGES] != null)
                checkedHT = (Hashtable)this.Session[Phrase.USER_SELECTED_PRIVILEDGES];
            foreach (Decimal ckey in checkedHT.Keys)
                operations.Add(int.Parse(ckey.ToString()));

            u.OperationIDs = operations;
            u.Save();
            this.lblError.Text = "Saved Successfully";
            this.lblError.Visible = true;
            //ArrayList operations = new ArrayList();
            //Hashtable checkedHT = this.getCheckedHT();
            //Hashtable uncheckedHT = this.getUncheckedHT();

            //// Get current view checked list
            //if (names != null && names.Length > 0)
            //    foreach (string name in names)
            //    {
            //        //operations.Add(int.Parse(name));

            //        if (!checkedHT.ContainsKey(name))
            //            checkedHT.Add(name, name);
            //        if (uncheckedHT.ContainsKey(name))
            //            uncheckedHT.Remove(name);
            //    }

            //// Remove current view unchecked list
            //foreach (string uckey in uncheckedHT.Keys)
            //{
            //    if (checkedHT.ContainsKey(uckey))
            //        checkedHT.Remove(uckey);
            //}

            //this.Session[Phrase.USER_SELECTED_PRIVILEDGES] = checkedHT;

            //foreach (string ckey in checkedHT.Keys)
            //    operations.Add(int.Parse(ckey));

            //u.OperationIDs = operations;
            //u.Save();
            //this.lblError.Text = "Saved Successfully";
            //this.lblError.Visible = true;
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Session[Phrase.USER_SELECTED_PRIVILEDGES] = null;
        this.Session[Phrase.USER_UNSELECTED_PRIVILEDGES] = null;
        this.Response.Redirect("~/Pages/User/SearchUsers.aspx");
    }

    protected void btnPasswordChange_Click(object sender, EventArgs e)
    {
        this.sec2.Visible = true;
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            NAASUser nu = new NAASUser(this.txtLoginName.Text, true);
            if (txtPassword.Text.Trim() != "")
            {
                nu.Password = txtPassword.Text.Trim();
            }
            nu.ChangePWDFlag = true;
            nu.Save();
            EmailManager manager = new EmailManager();
            string custom = "The NAAS Node User Account Password has been regenerated for this you.";
            string error = manager.SendUserEmail(nu.UserName, nu.UpdatedDate, Phrase.NAAS_NODE_USER, nu.UserName, nu.Password, custom);
            if (error != null && !error.Trim().Equals(""))
            {
                this.lblError.Text = "Password was updated, but the email failed to be sent: " + error;
                this.lblError.Visible = true;
            }
            else
            {
                this.lblError.Text = "Password was updated and the email was sent to user: " + this.txtLoginName.Text;
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string deleteResult = "";
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            NAASUser nu = new NAASUser(this.txtLoginName.Text, true);
            if (this.txtLoginName.Text.Trim() != "")
                deleteResult = nu.Delete();
            if (deleteResult.Trim() != "")
            {
                this.lblError.Text = deleteResult;
                this.lblError.Visible = true;
            }
            if (deleteResult.Trim().ToLower().EndsWith("deleted successfully."))
            {
                this.sec2.Visible = false;
                this.btnPasswordChange.Visible = false;
                this.btnDelete.Visible = false;
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

    protected void egvOperations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void egvOperations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        this.lblError.Text = "";
        this.lblError.Visible = false;
        
        Hashtable cht = this.getCheckedHT();
        Hashtable uht = new Hashtable();
        if (e.Row.RowIndex > -1)
        {
            HtmlInputCheckBox test = (HtmlInputCheckBox)e.Row.Cells[0].Controls[0];
            if (test != null)
            {
                string checkedId = "" + this.egvOperations.GetCurrentDataViewData(e, "OPERATION_ID");
                if (cht.ContainsKey(Decimal.Parse(checkedId)))
                {
                    test.Checked = true;
                }
            }
        }
    }

    #endregion

    #region Private Method

    /// <summary>
    /// Get Checked Priviledge List Hashtable
    /// </summary>
    /// <returns>Hashtable</returns>
    private Hashtable getCheckedHT()
    {
        if (this.Session[Phrase.USER_SELECTED_PRIVILEDGES] != null)
            return (Hashtable)this.Session[Phrase.USER_SELECTED_PRIVILEDGES];
        else
            return new Hashtable();
    }

    /// <summary>
    /// Get Unchecked Priviledge List Hashtable
    /// </summary>
    /// <returns>Hashtable</returns>
    private Hashtable getUncheckedHT()
    {
        Hashtable ht = new Hashtable();
        string[] names = this.egvOperations.GetCheckedValue("gcbfOperation");
        DataTable dt = this.egvOperations.CachedDataTable;
        int rowIndex = this.egvOperations.PageIndex * this.egvOperations.PageSize;
        if (dt == null) return ht;

        if (names != null && names.Length > 0)
        {
            for (int i = 0; i < this.egvOperations.Rows.Count; i++)
            {
                bool inList = false;
                foreach (string name in names)
                {
                    if (name == dt.Rows[rowIndex + i][0].ToString())
                    {
                        inList = true;
                        break;
                    }
                }
                if (!inList)
                    ht.Add(Decimal.Parse(dt.Rows[rowIndex + i][0].ToString()), Decimal.Parse(dt.Rows[rowIndex + i][0].ToString()));
            }
        }
        else
        {
            for (int i = 0; i < this.egvOperations.Rows.Count; i++)
            {
                ht.Add(Decimal.Parse(dt.Rows[rowIndex + i][0].ToString()), Decimal.Parse(dt.Rows[rowIndex + i][0].ToString()));
            }
        }
        return ht;
    }

    #endregion

    #region Initialization

    private void PageControlsInit()
    {
        try
        {
            this.lblError.Text = "";
            this.lblError.Visible = false;

            object obj = this.Request["error"];
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "EMAIL":
                        this.lblError.Text = "There was a problem sending the email, but the user was successfully created.";
                        this.lblError.Visible = true;
                        break;
                    default:
                        this.lblError.Text = "User Created Successfully";
                        this.lblError.Visible = true;
                        break;
                }
            }

            this.user = new NAASUser(this.Request["loginName"].ToString(), true);
            this.txtLoginName.Text = this.user.UserName;
            this.txtFirstName.Text = this.user.FirstName;
            this.txtMidInitial.Text = this.user.MiddleInitial;
            this.txtLastName.Text = this.user.LastName;
            this.txtAddress.Text = this.user.Address;
            this.txtCity.Text = this.user.LocalityName;
            this.txtState.Text = this.user.StateUSPSCode;
            this.zctbZip.Text = this.user.ZipCode;
            this.txtCountry.Text = this.user.CountryCode;
            this.ptbPhone.Text = this.user.PhoneNumber;

            // Initial Checked Operation List Hashtable
            Hashtable ht = new Hashtable();
            foreach (object ckey in this.user.OperationIDs)
                if (!ht.ContainsKey(Decimal.Parse(ckey + "")))
                    ht.Add(Decimal.Parse(ckey.ToString()), Decimal.Parse(ckey.ToString()));
            this.Session[Phrase.USER_SELECTED_PRIVILEDGES] = ht;

            this.egvOperations.CachedDataTable = Operation.GetOperationsDataGrid(this.LoggedInUser);
            this.egvOperations.DataBind();
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
    private Hashtable setCurrentCheckedHT()
    {
        string[] names = this.egvOperations.GetCheckedValue("gcbfOperation");

        Hashtable checkedHT = this.getCheckedHT();
        Hashtable uncheckedHT = this.getUncheckedHT();

        // Get current view checked list
        if (names != null && names.Length > 0)
        {
            foreach (string name in names)
            {
                if (!checkedHT.ContainsKey(Decimal.Parse(name)))
                    checkedHT.Add(Decimal.Parse(name), Decimal.Parse(name));
                if (uncheckedHT.ContainsKey(Decimal.Parse(name)))
                    uncheckedHT.Remove(Decimal.Parse(name));
            }
        }

        // Remove current view unchecked list
        foreach (Decimal uckey in uncheckedHT.Keys)
        {
            if (checkedHT.ContainsKey(uckey))
                checkedHT.Remove(uckey);
        }

        this.Session[Phrase.USER_SELECTED_PRIVILEDGES] = checkedHT;
        return checkedHT;
    }

    #endregion
}
