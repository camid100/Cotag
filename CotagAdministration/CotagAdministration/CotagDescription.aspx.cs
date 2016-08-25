using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;

public partial class CotagDescription : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        deleteButton.Enabled = false;
        txtId.Value = "";
        txtDescription.Text = "";
        chkIsActive.Enabled = true;
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
                DataLayer.tb_CotagDescription TempCotagDescription;
                TempCotagDescription = new DataLayer.tb_CotagDescription();
                TempCotagDescription.ID = Int32.Parse(txtId.Value);
                TempCotagDescription.Updatedby = Common.getUsername();
                TempCotagDescription.Updatedon = DateTime.Now;
                TempCotagDescription.Description = txtDescription.Text.Trim();
                TempCotagDescription.IsActive = chkIsActive.Checked;
                TempCotagDescription.IsLogicallyDeleted = false;
                BusinessLayer.BLCotagDescription bls = new BusinessLayer.BLCotagDescription();


                switch (bls.UpdateCotagDescription(TempCotagDescription))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "CotagDescription Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "CotagDescription already exists";
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
                DataLayer.tb_CotagDescription TempCotagDescription;
                TempCotagDescription = new DataLayer.tb_CotagDescription();
                TempCotagDescription.Createdby = Common.getUsername();
                TempCotagDescription.Createdon = DateTime.Now;
                TempCotagDescription.Description = txtDescription.Text.Trim();
                TempCotagDescription.IsActive = chkIsActive.Checked;
                TempCotagDescription.IsLogicallyDeleted = false;
                TempCotagDescription.RecordVersion = 1;
                BusinessLayer.BLCotagDescription bls = new BusinessLayer.BLCotagDescription();


                switch (bls.CreateCotagDescription(TempCotagDescription))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "CotagDescription Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "CotagDescription already exists";
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

    protected void gvCotagDescription_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            btnEdit.Visible = true;
            btnNew.Visible = false;
            deleteButton.Enabled = true;
            txtId.Value = gvCotagDescription.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLCotagDescription bls = new BLCotagDescription();
            tb_CotagDescription TempCotagDescription;
            TempCotagDescription = bls.GetCotagDescriptionNo(Int32.Parse(txtId.Value));
            txtDescription.Text = TempCotagDescription.Description;
            chkIsActive.Checked = TempCotagDescription.IsActive;
            if (TempCotagDescription.IsLogicallyDeleted)
            {
                btnEdit.Visible = false;
                btnNew.Visible = false;
                deleteButton.Enabled = false;
                chkIsActive.Enabled = false;
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
            BLCotagDescription CotagDescription = new BLCotagDescription();
            gvCotagDescription.DataSource = CotagDescription.GetLogicallyDeletedCotagDescription();
            gvCotagDescription.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLCotagDescription CotagDescription = new BLCotagDescription();
            gvCotagDescription.DataSource = CotagDescription.GetCotagDescription();
            gvCotagDescription.DataBind();
        }
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvCotagDescription_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLCotagDescription bls = new BLCotagDescription();
        gvCotagDescription.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_CotagDescription TempCotagDescription;
                TempCotagDescription = new DataLayer.tb_CotagDescription();
                TempCotagDescription.ID = Int32.Parse(txtId.Value);
                TempCotagDescription.Updatedby = Common.getUsername();
                TempCotagDescription.Updatedon = DateTime.Now;
                TempCotagDescription.Description = txtDescription.Text.Trim();
                TempCotagDescription.IsActive = false;
                TempCotagDescription.IsLogicallyDeleted = true;
                BusinessLayer.BLCotagDescription bls = new BusinessLayer.BLCotagDescription();


                switch (bls.UpdateCotagDescription(TempCotagDescription))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "CotagDescription Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "CotagDescription already exists";
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