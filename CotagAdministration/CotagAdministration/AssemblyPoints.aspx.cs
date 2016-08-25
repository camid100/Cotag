using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;

public partial class AssemblyPoints : System.Web.UI.Page
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
                ddlSite.Items.Add(new ListItem("Please select a Site...","-1"));
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
        chkIsActive.Enabled = true;
        txtDescription.Enabled = true;
        ddlSite.Enabled = true;
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
                DataLayer.tb_AssemblyPoint TempAssemblyPoint;
                TempAssemblyPoint = new DataLayer.tb_AssemblyPoint();
                TempAssemblyPoint.ID = Int32.Parse(txtId.Value);
                TempAssemblyPoint.Updatedby = Common.getUsername();
                TempAssemblyPoint.Updatedon = DateTime.Now;
                TempAssemblyPoint.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempAssemblyPoint.Description = txtDescription.Text.Trim();
                TempAssemblyPoint.IsActive = chkIsActive.Checked;
                BusinessLayer.BLAssemblyPoints blap = new BusinessLayer.BLAssemblyPoints();


                switch (blap.UpdateAssemblyPoint(TempAssemblyPoint))
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
                DataLayer.tb_AssemblyPoint TempAssemblyPoint;
                TempAssemblyPoint = new DataLayer.tb_AssemblyPoint();
                TempAssemblyPoint.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempAssemblyPoint.Createdby = Common.getUsername();
                TempAssemblyPoint.Createdon = DateTime.Now;
                TempAssemblyPoint.Description = txtDescription.Text.Trim();
                TempAssemblyPoint.IsActive = chkIsActive.Checked;
                TempAssemblyPoint.RecordVersion = 1;
                BusinessLayer.BLAssemblyPoints blap = new BusinessLayer.BLAssemblyPoints();


                switch (blap.CreateAssemblyPoint(TempAssemblyPoint))
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

    protected void gvSite_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            btnEdit.Visible = true;
            btnNew.Visible = false;
            deleteButton.Enabled = true;
            txtId.Value = gvAssemblyPoint.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLAssemblyPoints blap = new BLAssemblyPoints();
            tb_AssemblyPoint TempAssemblyPoint;
            TempAssemblyPoint = blap.GetAssemblyPointNo(Int32.Parse(txtId.Value));
            txtDescription.Text = TempAssemblyPoint.Description;
            ddlSite.SelectedValue = TempAssemblyPoint.Site_ID.ToString();
            chkIsActive.Checked = TempAssemblyPoint.IsActive;
            if (TempAssemblyPoint.IsLogicallyDeleted)
            {
                btnNew.Visible = false;
                btnEdit.Visible = false;
                deleteButton.Enabled = false;
                chkIsActive.Enabled = false;
                txtDescription.Enabled = false;
                ddlSite.Enabled = false;

            }
            lblMsg.Text = "";
        }

    }

    protected void fillGridView()
    {
        if (chkShow.Checked)
        {
            chkShow.Text = "Hide Deleted";
            BLAssemblyPoints AP = new BLAssemblyPoints();
            gvAssemblyPoint.DataSource = AP.GetLogicallyDeletedAssemblyPoints();
            gvAssemblyPoint.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLAssemblyPoints AP = new BLAssemblyPoints();
            gvAssemblyPoint.DataSource = AP.GetAssemblyPoints();
            gvAssemblyPoint.DataBind();
        }
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvSite_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssemblyPoint.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_AssemblyPoint TempAssemblyPoint;
                TempAssemblyPoint = new DataLayer.tb_AssemblyPoint();
                TempAssemblyPoint.ID = Int32.Parse(txtId.Value);
                TempAssemblyPoint.Updatedby = Common.getUsername();
                TempAssemblyPoint.Updatedon = DateTime.Now;
                TempAssemblyPoint.Site_ID = Int32.Parse(ddlSite.SelectedValue);
                TempAssemblyPoint.Description = txtDescription.Text.Trim();
                TempAssemblyPoint.IsActive = false;
                TempAssemblyPoint.IsLogicallyDeleted = true;
                BusinessLayer.BLAssemblyPoints blap = new BusinessLayer.BLAssemblyPoints();


                switch (blap.UpdateAssemblyPoint(TempAssemblyPoint))
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