using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
public partial class Companies : System.Web.UI.Page
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
                DataLayer.tb_Companies TempCompany;
                TempCompany = new DataLayer.tb_Companies();
                TempCompany.ID= Int32.Parse(txtId.Value);
                TempCompany.Description = txtDescription.Text.Trim();
                BusinessLayer.BLCompanies bls = new BusinessLayer.BLCompanies();

                
                switch (bls.UpdateCompany(TempCompany))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Company Updated";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Update could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Company already exists";
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
                DataLayer.tb_Companies TempCompany;
                TempCompany = new DataLayer.tb_Companies();
                TempCompany.Description = txtDescription.Text.Trim();
                BusinessLayer.BLCompanies bls = new BusinessLayer.BLCompanies();


                switch (bls.CreateCompany(TempCompany))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Company Created";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Creation could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "Company already exists";
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
            txtId.Value = gvCompany.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
            BLCompanies bls = new BLCompanies();
            tb_Companies TempCompanies;
            TempCompanies = bls.GetCompanyNo(Int32.Parse(txtId.Value));
            txtDescription.Text = TempCompanies.Description;
            lblMsg.Text = "";
        }

    }

    protected void fillGridView()
    {


        BLCompanies company = new BLCompanies();
        gvCompany.DataSource = company.GetCompanies();
        gvCompany.DataBind();
      
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLCompanies bls = new BLCompanies();
        gvCompany.PageIndex = e.NewPageIndex;
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
                DataLayer.tb_Companies TempCompany;
                TempCompany = new DataLayer.tb_Companies();
                TempCompany.ID = Int32.Parse(txtId.Value);
                TempCompany.Description = txtDescription.Text.Trim();
                BusinessLayer.BLCompanies bls = new BusinessLayer.BLCompanies();


                switch (bls.DeleteCompany(TempCompany.ID))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Company Deleted";
                        ClearTextBoxes();
                        fillGridView();
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Deletetion could not be carried out";
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