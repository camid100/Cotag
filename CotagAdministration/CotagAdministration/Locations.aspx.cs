using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;

public partial class Locations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                BusinessLayer.BLSites s = new BusinessLayer.BLSites();

                ddlSite.DataSource = s.GetSites();

                ddlSite.DataTextField = "SiteDescription";
                ddlSite.DataValueField = "SiteNo";
                ddlSite.DataBind();
                ddlSite.Items.Add(new ListItem("Please select a Location...", "-1"));
                ddlSite.SelectedValue = "-1";
            }
            catch (Exception exc)
            {
                lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                ExceptionHandler.write(exc);
            }

        }
        fillGridView();
        deleteButton.Enabled = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();
        lblMsg.Text = "";
    }

    protected void ClearTextBoxes()
    {
        btnNew.Visible = true;
        btnEdit.Visible = false;
        txtId.Value = "";
        txtDescription.Text = "";
        chkIsActive.Enabled = false;
        deleteButton.Enabled = false;
        txtDescription.Text = "";
        txtSysAddress.Text = "";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            btnNew.Visible = true;
            btnEdit.Visible = false;
            deleteButton.Enabled = false;
            try
            {
                DataLayer.tb_Location TempLocation;
                TempLocation = new DataLayer.tb_Location();
                TempLocation.ID = Int32.Parse(txtId.Value);
                TempLocation.Updatedby = Common.getUsername();
                TempLocation.Updatedon = DateTime.Now;
                TempLocation.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempLocation.SysAddress = txtSysAddress.Text;
                TempLocation.Description = txtDescription.Text.Trim();
                TempLocation.IsEntry = chkIsEntry.Checked;
                TempLocation.IsExit = chkIsExit.Checked;
                TempLocation.IsActive = chkIsActive.Checked;
                BusinessLayer.BLLocations blap = new BusinessLayer.BLLocations();


                switch (blap.UpdateLocation(TempLocation))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Assembly Point Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Assembly Point already exists";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                ExceptionHandler.write(exc);
            }
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                DataLayer.tb_Location TempLocation;
                TempLocation = new DataLayer.tb_Location();
                TempLocation.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempLocation.Createdby = Common.getUsername();
                TempLocation.Createdon = DateTime.Now;
                TempLocation.SysAddress = txtSysAddress.Text;
                TempLocation.Description = txtDescription.Text.Trim();
                TempLocation.IsEntry = chkIsEntry.Checked;
                TempLocation.IsExit = chkIsExit.Checked;
                TempLocation.IsActive = chkIsActive.Checked;
                TempLocation.RecordVersion = 1;
                BusinessLayer.BLLocations blap = new BusinessLayer.BLLocations();


                switch (blap.CreateLocation(TempLocation))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Assembly Point Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Assembly Point already exists";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                ExceptionHandler.write(exc);
            }
        }
    }

    protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            btnEdit.Visible = true;
            btnNew.Visible = false;
            deleteButton.Enabled = true;
            txtId.Value = gvLocation.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLLocations blap = new BLLocations();
            tb_Location TempLocation;
            TempLocation = blap.GetLocationNo(Int32.Parse(txtId.Value));
            txtSysAddress.Text = TempLocation.SysAddress;
            txtDescription.Text = TempLocation.Description;
            chkIsActive.Checked = TempLocation.IsActive;
            chkIsEntry.Checked = TempLocation.IsEntry;
            chkIsExit.Checked = TempLocation.IsExit;
            ddlSite.SelectedValue = TempLocation.Site_ID.ToString();
            if (TempLocation.IsLogicallyDeleted)
            {
                btnNew.Visible = false;
                btnEdit.Visible = false;
                chkIsActive.Enabled = false;
                deleteButton.Enabled = false;
                txtDescription.Enabled = false;
            }
            lblMsg.Text = "";
        }

    }

    protected void fillGridView()
    {
        if (chkShow.Checked)
        {
            chkShow.Text = "Hide Deleted";
            BLLocations AP = new BLLocations();
            gvLocation.DataSource = AP.GetLogicallyDeletedLocations();
            gvLocation.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLLocations AP = new BLLocations();
            gvLocation.DataSource = AP.GetLocations();
            gvLocation.DataBind();
        }
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLocation.PageIndex = e.NewPageIndex;
        fillGridView();
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            btnNew.Visible = true;
            btnEdit.Visible = false;
            deleteButton.Enabled = false;
            try
            {
                DataLayer.tb_Location TempLocation;
                TempLocation = new DataLayer.tb_Location();
                TempLocation.ID = Int32.Parse(txtId.Value);
                TempLocation.Updatedby = Common.getUsername();
                TempLocation.Updatedon = DateTime.Now;
                TempLocation.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempLocation.SysAddress = txtSysAddress.Text;
                TempLocation.Description = txtDescription.Text.Trim();
                TempLocation.IsEntry = chkIsEntry.Checked;
                TempLocation.IsExit = chkIsExit.Checked;
                TempLocation.IsActive = false;
                TempLocation.IsLogicallyDeleted = true;
                BusinessLayer.BLLocations blap = new BusinessLayer.BLLocations();


                switch (blap.UpdateLocation(TempLocation))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Assembly Point Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Assembly Point already exists";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exc)
            {
                lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                ExceptionHandler.write(exc);
            }
        }
    }


}