using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;

public partial class Status : System.Web.UI.Page
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
        txtDescription.Enabled = true;
        chkIsActive.Enabled = true;
        txtId.Value = "";
        txtDescription.Text = "";
        deleteButton.Enabled = false;
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
                DataLayer.tb_Status TempStatus;
                TempStatus = new DataLayer.tb_Status();
                TempStatus.ID = Int32.Parse(txtId.Value);
                TempStatus.Updatedby = Common.getUsername();
                TempStatus.Updatedon = DateTime.Now;
                TempStatus.StatusDescription = txtDescription.Text.Trim();
                TempStatus.IsActive = chkIsActive.Checked;
                BusinessLayer.BLStatus bls = new BusinessLayer.BLStatus();


                switch (bls.UpdateStatus(TempStatus))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Status Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Status already exists";
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
                DataLayer.tb_Status TempStatus;
                TempStatus = new DataLayer.tb_Status();
                TempStatus.Createdby = Common.getUsername();
                TempStatus.Createdon = DateTime.Now;
                TempStatus.StatusDescription = txtDescription.Text.Trim();
                TempStatus.IsActive = chkIsActive.Checked;
                TempStatus.RecordVersion = 1;
                BusinessLayer.BLStatus bls = new BusinessLayer.BLStatus();


                switch (bls.CreateStatus(TempStatus))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Status Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Status already exists";
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

    protected void gvStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            btnEdit.Visible = true;
            btnNew.Visible = false;
            deleteButton.Enabled = true;
            txtId.Value = gvStatus.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLStatus bls = new BLStatus();
            tb_Status TempStatus;
            TempStatus = bls.GetStatusNo(Int32.Parse(txtId.Value));
            txtDescription.Text = TempStatus.StatusDescription;
            chkIsActive.Checked = TempStatus.IsActive;
            if (TempStatus.IsLogicallyDeleted)
            {
                btnNew.Visible = false;
                btnEdit.Visible = false;
                txtDescription.Enabled = false;
                chkIsActive.Enabled = false;
                deleteButton.Enabled = false;
            }
            lblMsg.Text = "";
        }

    }

    protected void fillGridView()
    {
        if (chkShow.Checked)
        {
            chkShow.Text = "Hide Deleted";
            BLStatus Status = new BLStatus();
            gvStatus.DataSource = Status.GetLogicallyDeletedStatus();
            gvStatus.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLStatus Status = new BLStatus();
            gvStatus.DataSource = Status.GetStatus();
            gvStatus.DataBind();
        }
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLStatus bls = new BLStatus();
        gvStatus.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_Status TempStatus;
                TempStatus = new DataLayer.tb_Status();
                TempStatus.ID = Int32.Parse(txtId.Value);
                TempStatus.Updatedby = Common.getUsername();
                TempStatus.Updatedon = DateTime.Now;
                TempStatus.StatusDescription = txtDescription.Text.Trim();
                TempStatus.IsActive = false;
                TempStatus.IsLogicallyDeleted = true;
                BusinessLayer.BLStatus bls = new BusinessLayer.BLStatus();


                switch (bls.UpdateStatus(TempStatus))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Status Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Status already exists";
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