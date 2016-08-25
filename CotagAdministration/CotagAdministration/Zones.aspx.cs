using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;


public partial class Zones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillGridView();
        deleteButton.Enabled = false;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearTextBoxes();
    }
    protected void ClearTextBoxes()
    {
        btnNew.Visible = true;
        btnEdit.Visible = false;
        txtId.Value = "";
        txtDescription.Text = "";
        deleteButton.Enabled = false;
        txtDescription.Enabled = true;
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
                DataLayer.tb_Zones TempZone;
                TempZone = new DataLayer.tb_Zones();
                TempZone.ID = Int32.Parse(txtId.Value);
                TempZone.Description = txtDescription.Text.Trim();
                BusinessLayer.BLZones bls = new BusinessLayer.BLZones();


                switch (bls.UpdateZone(TempZone))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Zone Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Zone already exists";
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
                DataLayer.tb_Zones TempZone;
                TempZone = new DataLayer.tb_Zones();
                TempZone.Description = txtDescription.Text.Trim();
                TempZone.CreatedBy = Common.getUsername();
                TempZone.CreatedOn = DateTime.Now;
                TempZone.IsActive = true;
                TempZone.RecordVersion = 1;

                BusinessLayer.BLZones bls = new BusinessLayer.BLZones();


                switch (bls.CreateZone(TempZone))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Zone Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Zone already exists";
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

    protected void fillGridView()
    {
        BLZones zone = new BLZones();
        gvZone.DataSource = zone.GetZones();
        gvZone.DataBind();

    }

    protected void gvZone_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLZones bls = new BLZones();
        gvZone.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_Zones TempZone;
                TempZone = new DataLayer.tb_Zones();
                TempZone.ID = Int32.Parse(txtId.Value);
                TempZone.Description = txtDescription.Text.Trim();
                BusinessLayer.BLZones bls = new BusinessLayer.BLZones();


                switch (bls.DeleteZone(TempZone.ID))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Zone Deleted";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Deletion could not be carried out";
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

    protected void gvZone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            btnEdit.Visible = true;
            btnNew.Visible = false;
            deleteButton.Enabled = true;
            txtId.Value = gvZone.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLZones zon = new BLZones();
            tb_Zones tempZones;

            tempZones = zon.GetTimeByID(Int32.Parse(txtId.Value));
            txtDescription.Text = tempZones.Description;

            if (tempZones.IsLogicallyDeleted)
            {
                btnEdit.Visible = false;
                btnNew.Visible = false;
                deleteButton.Enabled = false;
                txtDescription.Enabled = false;
            }
            lblMsg.Text = "";
        }
    }




}
