using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;

namespace CotagAdministration
{
    public partial class Utils : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillGridView();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                DataLayer.tb_contacts_generation_log TempCotagDetail;
                TempCotagDetail = new DataLayer.tb_contacts_generation_log();
                TempCotagDetail.request_date = DateTime.Now;
                TempCotagDetail.status = "P";

                BusinessLayer.BLContactsGeneration bls = new BusinessLayer.BLContactsGeneration();

                lblMsg.Visible = true;
                switch (bls.CreateContactsGeneration(TempCotagDetail))
                {
                    case Util.OperationStatus.successful:
                        lblMsg.Text = "Task Created";
                        fillGridView();
                        Response.Redirect(Request.RawUrl);
                        break;
                    case Util.OperationStatus.Unsuccessful:
                        lblMsg.Text = "Task could not be carried out";
                        break;
                    case Util.OperationStatus.Exists:
                        lblMsg.Text = "There is already a pending task";
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void fillGridView()
        {
            BusinessLayer.BLContactsGeneration CotagDetail = new BusinessLayer.BLContactsGeneration();
            gvContacts.DataSource = CotagDetail.GetAssemblyPoints();
            gvContacts.DataBind();
        }
    }
}