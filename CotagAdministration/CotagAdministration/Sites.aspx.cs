using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;

public partial class Sites : System.Web.UI.Page
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
        TextBox1.Text = "";
        TextBox2.Text = "";
        txtDescription.Text = "";
        deleteButton.Enabled = false;
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
                DataLayer.tb_Site TempSite;
                TempSite = new DataLayer.tb_Site();
                TempSite.SiteNo = Int32.Parse(txtId.Value);
                TempSite.Updatedby = Common.getUsername();
                TempSite.Updatedon = DateTime.Now;
                TempSite.EndLevel =Convert.ToInt32(TextBox2.Text);
                TempSite.StartLevel = Convert.ToInt32(TextBox1.Text);
                TempSite.SiteDescription = txtDescription.Text.Trim();
                TempSite.IsActive = chkIsActive.Checked;
                BusinessLayer.BLSites bls = new BusinessLayer.BLSites();


                switch (bls.UpdateSite(TempSite))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Site Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Site already exists";
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
                DataLayer.tb_Site TempSite;
                TempSite = new DataLayer.tb_Site();
                TempSite.Createdby = Common.getUsername();
                TempSite.Createdon = DateTime.Now;
                TempSite.SiteDescription = txtDescription.Text.Trim();
                TempSite.IsActive = chkIsActive.Checked;
                TempSite.RecordVersion = 1;
                TempSite.EndLevel = Convert.ToInt32(TextBox2.Text);
                TempSite.StartLevel = Convert.ToInt32(TextBox1.Text);
                BusinessLayer.BLSites bls = new BusinessLayer.BLSites();


                switch (bls.CreateSite(TempSite))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Site Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Site already exists";
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
            txtId.Value = gvSite.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLSites bls = new BLSites();
            tb_Site TempSite;
            TempSite = bls.GetSiteNo(Int32.Parse(txtId.Value));
            txtDescription.Text = TempSite.SiteDescription;
            chkIsActive.Checked = TempSite.IsActive;
            TextBox1.Text = TempSite.StartLevel.ToString();
            TextBox2.Text = TempSite.EndLevel.ToString();

            if (TempSite.IsLogicallyDeleted)
            {
                deleteButton.Enabled = false;
                btnNew.Visible = false;
                chkIsActive.Enabled = false;
                btnEdit.Visible = false;
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
            BLSites site = new BLSites();
            gvSite.DataSource = site.GetLogicallyDeletedSites();
            gvSite.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLSites site = new BLSites();
            gvSite.DataSource = site.GetSites();
            gvSite.DataBind();
        }
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvSite_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLSites bls = new BLSites();
        gvSite.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_Site TempSite;
                TempSite = new DataLayer.tb_Site();
                TempSite.SiteNo = Int32.Parse(txtId.Value);
                TempSite.Updatedby = Common.getUsername();
                TempSite.Updatedon = DateTime.Now;
                TempSite.SiteDescription = txtDescription.Text.Trim();
                TempSite.EndLevel = Convert.ToInt32(TextBox2.Text);
                TempSite.StartLevel = Convert.ToInt32(TextBox1.Text);
                TempSite.IsActive = false;
                TempSite.IsLogicallyDeleted = true;
                BusinessLayer.BLSites bls = new BusinessLayer.BLSites();


                switch (bls.UpdateSite(TempSite))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Site Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Site already exists";
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