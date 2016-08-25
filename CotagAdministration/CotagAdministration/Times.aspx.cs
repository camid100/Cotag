using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;


    public partial class Times : System.Web.UI.Page
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
                    DataLayer.tb_Times TempTime;
                    TempTime = new DataLayer.tb_Times();
                    TempTime.ID = Int32.Parse(txtId.Value);
                    TempTime.Description = txtDescription.Text.Trim();
                    BusinessLayer.BLTimes bls = new BusinessLayer.BLTimes();


                    switch (bls.UpdateTime(TempTime))
                    {
                        case Util.OperationStatus.successful:
                            lblMsg.Text = "Time Updated";
                            ClearTextBoxes();
                            fillGridView();
                            break;
                        case Util.OperationStatus.Unsuccessful:
                            lblMsg.Text = "Update could not be carried out";
                            break;
                        case Util.OperationStatus.Exists:
                            lblMsg.Text = "Time already exists";
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
                    DataLayer.tb_Times TempTime;
                    TempTime = new DataLayer.tb_Times();
                    TempTime.Description = txtDescription.Text.Trim();
                    TempTime.CreatedBy= Common.getUsername();
                    TempTime.CreatedOn = DateTime.Now;
                    TempTime.IsActive = true;
                    TempTime.RecordVersion = 1;
                    BusinessLayer.BLTimes bls = new BusinessLayer.BLTimes();


                    switch (bls.CreateTime(TempTime))
                    {
                        case Util.OperationStatus.successful:
                            lblMsg.Text = "Time Created";
                            ClearTextBoxes();
                            fillGridView();
                            break;
                        case Util.OperationStatus.Unsuccessful:
                            lblMsg.Text = "Creation could not be carried out";
                            break;
                        case Util.OperationStatus.Exists:
                            lblMsg.Text = "Time already exists";
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
            BLTimes time = new BLTimes();
            gvTime.DataSource = time.GetTimes();
            gvTime.DataBind();

        }

        protected void gvTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BLTimes bls = new BLTimes();
            gvTime.PageIndex = e.NewPageIndex;
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
                    DataLayer.tb_Times TempTime;
                    TempTime = new DataLayer.tb_Times();
                    TempTime.ID = Int32.Parse(txtId.Value);
                    TempTime.Description = txtDescription.Text.Trim();
                    BusinessLayer.BLTimes bls = new BusinessLayer.BLTimes();


                    switch (bls.DeleteTime(TempTime.ID))
                    {
                        case Util.OperationStatus.successful:
                            lblMsg.Text = "Time Deleted";
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

        protected void gvTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvTime_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                btnEdit.Visible = true;
                btnNew.Visible = false;
                deleteButton.Enabled = true;
                txtId.Value =gvTime.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
                BLTimes tim = new BLTimes();
                tb_Times tempTimes;
                tempTimes = tim.GetTimeByID(Int32.Parse(txtId.Value));
                txtDescription.Text = tempTimes.Description;

                if (tempTimes.IsLogicallyDeleted)
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
