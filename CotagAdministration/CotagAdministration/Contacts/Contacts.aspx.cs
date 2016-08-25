using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer.Views;

namespace CotagAdministration.Contacts
{
    public partial class Contacts : System.Web.UI.Page
    {
        List<ContactBookView> lst = new List<ContactBookView>();
        List<ContactBookView> lst1 = new List<ContactBookView>();
        List<ContactBookView> lst2 = new List<ContactBookView>();
        List<ContactBookView> lst4 = new List<ContactBookView>();
        List<ContactBookView> lst3
        {
            get {
                if (Session["lst3"] != null)
                    return (List<ContactBookView>)Session["lst3"];
                else return null;
            
            }
            set {
                if (value == null)
                    Session.Remove("lst3");
                else
                    Session["lst3"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_Command(object sender, CommandEventArgs e)
        {
            BusinessLayer.BLContactBook cb = new BusinessLayer.BLContactBook();
            lst3 = cb.GetContactBookSearchLetter(e.CommandArgument.ToString()).ToList<ContactBookView>();
            GridView1.DataSource = lst3;
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            if (txtSearch.Text.Trim().Length >= 3)
            {
                if (txtSearch.Text.Trim().Length == 3 && txtSearch.Text.Contains(' '))
                {
                    lblmsg.Text = "Search query must have 3 or more letters.";
                }
                else
                {
                    BusinessLayer.BLContactBook cb = new BusinessLayer.BLContactBook();
                    string[] search = txtSearch.Text.Trim().Split(' ');
                    if (search.Length == 1)
                    {
                        lst1 = cb.GetContactBookSearchName(search[0]).ToList();
                        lst2 = cb.GetContactBookSearchSurname(search[0]).ToList();
                        lst3 = lst1.Concat(lst2).ToList<ContactBookView>();
                        GridView1.DataSource = lst3;
                        GridView1.DataBind();
                    }
                    else if (search.Length == 2)
                    {
                        lst1 = cb.GetContactBookSearch(search[0], search[1]).ToList();
                        lst2 = cb.GetContactBookSearch(search[1], search[0]).ToList();
                        lst3 = lst1.Concat(lst2).ToList<ContactBookView>();
                        GridView1.DataSource = lst3;
                        GridView1.DataBind();
                    }
                    else if (search.Length == 3)
                    {
                        lst = cb.GetContactBookSearch(search[0] + search[1], search[2]).ToList();
                        lst1 = cb.GetContactBookSearch(search[0], search[1] + search[2]).ToList();
                        lst2 = cb.GetContactBookSearch(search[1] + search[2], search[0]).ToList();
                        lst4 = cb.GetContactBookSearch(search[0] + search[1], search[2]).ToList();
                        lst3 = lst1.Concat(lst2).Concat(lst).Concat(lst4).ToList<ContactBookView>();
                        GridView1.DataSource = lst3;
                        GridView1.DataBind();
                    }
                    else if (search.Length > 3)
                    {
                        lblmsg.Text = "Search query can't have more then 3 words.";
                    }
                }
            }
            else
            {
                lblmsg.Text = "Search query must have 3 or more letters.";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "wait('waitingDiv');wait('waiting');", true);
            int rowIndex = Convert.ToInt32(e.CommandArgument); // Get the current row index
            lblAssemblyPoint.Text = lst3[rowIndex].AssemblyPoint;
            lblDepartment.Text = lst3[rowIndex].Department;
            lblEmail.Text = lst3[rowIndex].Email;
            lblLocation.Text = lst3[rowIndex].Location;
            lblMobile.Text = lst3[rowIndex].mobile;
            lblName.Text = lst3[rowIndex].Name;
            lblSection.Text = lst3[rowIndex].Section;
            lblSite.Text = lst3[rowIndex].Site;
            lblSurname.Text = lst3[rowIndex].Surname;
            lblTelephone.Text = lst3[rowIndex].telephone;
        }
    }
}