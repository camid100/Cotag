using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using CotagAdministration.TMSService;
using System.Configuration;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections;

public partial class CotagDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExport);
        scriptManager.RegisterPostBackControl(this.btnGenerate);
        scriptManager.RegisterPostBackControl(this.btnEdit);
        scriptManager.RegisterPostBackControl(this.btnNew);
        if (!Page.IsPostBack)
        {
            try
            {
                chkVoda.Text = ConfigurationManager.AppSettings.Get("ProviderName");
                BusinessLayer.BLAssemblyPoints s = new BusinessLayer.BLAssemblyPoints();

                ddlAssemblyPoint.DataSource = s.GetAssemblyPoints();
                ddlAssemblyPoint.DataTextField = "Description";
                ddlAssemblyPoint.DataValueField = "ID";
                ddlAssemblyPoint.DataBind();
                ddlAssemblyPoint.Items.Add(new ListItem("Please select an Assembly Point...", "-1"));

                BusinessLayer.BLCotagDescription cd = new BusinessLayer.BLCotagDescription();

                ddlCotagDescription.DataSource = cd.GetCotagDescription();

                ddlCotagDescription.DataTextField = "Description";
                ddlCotagDescription.DataValueField = "ID";
                ddlCotagDescription.DataBind();
                ddlCotagDescription.Items.Add(new ListItem("Please select a Description...", "-1"));
                ddlAssemblyPoint.SelectedValue = "-1";
                ddlCotagDescription.SelectedValue = "-1";

                //location

                BusinessLayer.BLSites loc = new BusinessLayer.BLSites();

                DropDownList2.DataSource = loc.GetSites();

                DropDownList2.DataTextField = "SiteDescription";
                DropDownList2.DataValueField = "SiteNo";
                DropDownList2.DataBind();
                DropDownList2.Items.Add(new ListItem("Please select a Location...", "-1"));
                DropDownList2.SelectedValue = "-1";

                
                BusinessLayer.BLCompanies com = new BusinessLayer.BLCompanies();

                ddlCompanies.DataSource = com.GetCompanies();
                ddlCompanies.DataTextField = "Description";
                ddlCompanies.DataValueField = "ID";
                ddlCompanies.DataBind();
                ddlCompanies.Items.Add(new ListItem("Please select a Company...", "-1"));
                ddlCompanies.SelectedValue = "-1";
                BusinessLayer.BLDepartments dep = new BusinessLayer.BLDepartments();

                ddlDepartment.DataSource = dep.GetDepartments();
                ddlDepartment.DataTextField = "Description";
                ddlDepartment.DataValueField = "ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Add(new ListItem("Please select a Department...", "-1"));
                ddlDepartment.SelectedValue = "-1";

                BusinessLayer.BLServices serv = new BusinessLayer.BLServices();

                ddlServices.DataSource = serv.GetServices();
                ddlServices.DataTextField = "Description";
                ddlServices.DataValueField = "ID";
                ddlServices.DataBind();
                ddlServices.Items.Add(new ListItem("Please select a Service...", "-1"));
                ddlServices.SelectedValue = "-1";
                
                BusinessLayer.BLZones zone = new BusinessLayer.BLZones();
                ddlZone.DataSource = zone.GetZones();
                ddlZone.DataTextField = "Description";
                ddlZone.DataValueField = "ID";
                ddlZone.DataBind();
                ddlZone.Items.Add(new ListItem("Please select a Zone...", "-1"));
                ddlZone.SelectedValue = "-1";


                toggleEnable(false);
                txtIDNo.Enabled = false;
                btnCheck.Enabled = false;
            }
            catch (Exception exc)
            {
                lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                ExceptionHandler.write(exc);
            }
            fillGridView();

        }

        deleteButton.Enabled = false;
    }
    private string _myDataTable = "myDataTable";
    private DataTable myDataTable
    {
        get
        {
            if (ViewState[_myDataTable] == null)
                return new DataTable();
            return (DataTable)ViewState[_myDataTable];
        }
        set
        {
            ViewState[_myDataTable] = value;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();
        lblMsg.Text = "";
        Response.Redirect(Request.RawUrl);
    }

    protected void ClearTextBoxes()
    {
        btnNew.Visible = true;
        btnEdit.Visible = false;
        deleteButton.Enabled = false;
        txtCotagNo.Enabled = true;
        txtTelephone.Enabled = true;
        txtStartDate.Enabled = true;
        txtMobile.Enabled = true;
        txtIDNo.Enabled = true;
        txtEndDate.Enabled = true;
        ddlAssemblyPoint.Enabled = true;
        ddlCotagDescription.Enabled = true;
        btnCheck.Enabled = true;
        chkIsActive.Enabled = true;
        txtCotagNo.Text = "";
        txtTelephone.Text = "";
        txtStartDate.Text = "";
        txtMobile.Text = "";
        txtIDNo.Text = "";
        txtEndDate.Text = "";
        ddlAssemblyPoint.SelectedValue = "-1";
        ddlZone.SelectedValue = "-1";
        ddlCotagDescription.SelectedValue = "-1";
        txtName.Text = "";
        txtSurname.Text = "";
        ddlServices.SelectedValue = "-1";

        ddlCompanies.SelectedValue = "-1";
        TextBox2.Text = "";

        ddlDepartment.SelectedValue = "-1";
        DropDownList2.SelectedValue = "-1";
        DropDownList1.SelectedValue = "-1";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        btnNew.Visible = true;
        btnEdit.Visible = false;
        deleteButton.Enabled = false;
        try
        {
            DataLayer.tb_CotagDetail TempCotagDetail;
            TempCotagDetail = new DataLayer.tb_CotagDetail();
            TempCotagDetail.CotagNo = Int32.Parse(txtCotagNo.Text);
            TempCotagDetail.AssemblyPoint_ID = Int32.Parse(ddlAssemblyPoint.SelectedValue);
            TempCotagDetail.cotagDesc_ID = Int32.Parse(ddlCotagDescription.SelectedValue);
            TempCotagDetail.telephone = txtTelephone.Text;
            TempCotagDetail.mobile = txtMobile.Text;
            TempCotagDetail.IDNo = txtIDNo.Text;
            TempCotagDetail.Name = txtName.Text;
            TempCotagDetail.Surname = txtSurname.Text;
            TempCotagDetail.Type = ddlType.SelectedValue;

            if (ddlZone.SelectedValue != "-1")
            {
                TempCotagDetail.zoneID = Int32.Parse(ddlZone.SelectedValue);
            }


            DateTime dt = new DateTime();
            if (DateTime.TryParse(txtEndDate.Text, out dt))
            {
                TempCotagDetail.EndDate = DateTime.Parse(txtEndDate.Text);
            }
            if (DateTime.TryParse(txtStartDate.Text, out dt))
            {
                TempCotagDetail.StartDate = DateTime.Parse(txtStartDate.Text);
            }


            TempCotagDetail.isProvider = chkVoda.Checked;
            TempCotagDetail.DepartmentGUID = new Guid(ddlDepartment.SelectedValue.ToString());
            TempCotagDetail.Site_ID = Convert.ToInt32(DropDownList2.SelectedValue.ToString());
            TempCotagDetail.site_level = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            if (ddlCompanies.Enabled == true)
            {
                TempCotagDetail.companyDesc_ID = Convert.ToInt32(ddlCompanies.SelectedValue.ToString());
            }

            if (TextBox2.Enabled == true)
            {
                TempCotagDetail.projectManager_Cotag = Convert.ToInt32(TextBox2.Text);
            }
            if (Convert.ToInt32(ddlServices.SelectedValue.ToString()) != -1)
            {
                TempCotagDetail.serviceDesc_ID = Convert.ToInt32(ddlServices.SelectedValue.ToString());
            }





            TempCotagDetail.Updatedby = Common.getUsername();
            TempCotagDetail.Updatedon = DateTime.Now;
            TempCotagDetail.IsActive = chkIsActive.Checked;


            if (photoUpload.PostedFile != null)
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string path = Server.MapPath("~/Assets/UserImages/" + fileName);
                photoUpload.PostedFile.SaveAs(path);
                TempCotagDetail.image_path = "~/Assets/UserImages/" + fileName;
            }
            
            BusinessLayer.BLCotagDetail bls = new BusinessLayer.BLCotagDetail();


            switch (bls.UpdateCotagDetail(TempCotagDetail))
            {
                case Util.OperationStatus.successful:
                    lblMsg.Text = "Cotag Detail Updated";
                    ClearTextBoxes();
                    fillGridView();
                    Response.Redirect(Request.RawUrl);
                    break;
                case Util.OperationStatus.Unsuccessful:
                    lblMsg.Text = "Update could not be carried out";
                    break;
                case Util.OperationStatus.Exists:
                    lblMsg.Text = "Cotag Detail already exists";
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
    public void generateCotag(string imagePath,string name,string assemblyPoint,string cotagNo,bool contractor)
    {

        string formattedText = readFile();
        formattedText = formattedText.Replace("{0}", name);
        formattedText = formattedText.Replace("{1}", imagePath);
        formattedText = formattedText.Replace("{2}", assemblyPoint.Replace("Assembly Point ",""));
        formattedText = formattedText.Replace("{3}", cotagNo.ToString());

        formattedText = formattedText.Replace("{4}", Server.MapPath("~/Assets/cotagTemplate/mitaLogo2.png"));
        formattedText = formattedText.Replace("{5}", Server.MapPath("~/Assets/cotagTemplate/1.png"));
        formattedText = formattedText.Replace("{6}", Server.MapPath("~/Assets/cotagTemplate/2.png"));
        formattedText = formattedText.Replace("{7}", Server.MapPath("~/Assets/cotagTemplate/3.png"));
        formattedText = formattedText.Replace("{8}", Server.MapPath("~/Assets/cotagTemplate/4.png"));
        formattedText = formattedText.Replace("{9}", Server.MapPath("~/Assets/cotagTemplate/5.png"));

        if (contractor == false)
        {
            formattedText = formattedText.Replace("{10}", "17365D");
        }
        else
        {
            formattedText = formattedText.Replace("{10}", "FF9900");
        }
        formattedText = formattedText.Replace("�", "");
        StringBuilder strBody = new StringBuilder("");
        strBody.Append(formattedText);    

        Response.AppendHeader("Content-Type", "application/msword");
        Response.AppendHeader("Content-disposition", "attachment; filename=Cotag - "+name+"("+cotagNo.ToString()+").doc");

        Response.Write(strBody);
    }

    public string readFile()
    {
        return System.IO.File.ReadAllText(Server.MapPath("~/Assets/cotagTemplate/template.htm"));
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {

            DataLayer.tb_CotagDetail TempCotagDetail;
            TempCotagDetail = new DataLayer.tb_CotagDetail();
            TempCotagDetail.CotagNo = Int32.Parse(txtCotagNo.Text);
            TempCotagDetail.AssemblyPoint_ID = Int32.Parse(ddlAssemblyPoint.SelectedValue);
            TempCotagDetail.cotagDesc_ID = Int32.Parse(ddlCotagDescription.SelectedValue);
            TempCotagDetail.telephone = txtTelephone.Text;
            TempCotagDetail.mobile = txtMobile.Text;
            TempCotagDetail.IDNo = txtIDNo.Text;
            TempCotagDetail.Name = txtName.Text;
            TempCotagDetail.Surname = txtSurname.Text;
            TempCotagDetail.Type = ddlType.SelectedValue;

            if (ddlZone.SelectedValue != "-1")
            {
                TempCotagDetail.zoneID = Int32.Parse(ddlZone.SelectedValue);
            }

            DateTime dt = new DateTime();
            if (DateTime.TryParse(txtEndDate.Text, out dt))
            {
                TempCotagDetail.EndDate = DateTime.Parse(txtEndDate.Text);
            }
            if (DateTime.TryParse(txtStartDate.Text, out dt))
            {
                TempCotagDetail.StartDate = DateTime.Parse(txtStartDate.Text);
            }


            TempCotagDetail.isProvider = chkVoda.Checked;
            TempCotagDetail.DepartmentGUID = new Guid(ddlDepartment.SelectedValue.ToString());
            TempCotagDetail.Site_ID = Convert.ToInt32(DropDownList2.SelectedValue.ToString());
            TempCotagDetail.site_level = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            if (ddlCompanies.Enabled == true)
            {
                TempCotagDetail.companyDesc_ID = Convert.ToInt32(ddlCompanies.SelectedValue.ToString());
            }

            if (TextBox2.Enabled == true)
            {
                TempCotagDetail.projectManager_Cotag = Convert.ToInt32(TextBox2.Text);
            }
            if (Convert.ToInt32(ddlServices.SelectedValue.ToString()) != -1)
            {
                TempCotagDetail.serviceDesc_ID = Convert.ToInt32(ddlServices.SelectedValue.ToString());
            }

            TempCotagDetail.Createdby = Common.getUsername();
            TempCotagDetail.Createdon = DateTime.Now;
            TempCotagDetail.RecordVersion = 1;
            TempCotagDetail.IsActive = chkIsActive.Checked;

           

            if (photoUpload.PostedFile != null)
            {
                string fileName = Guid.NewGuid() + ".jpg";
                string path = Server.MapPath("~/Assets/UserImages/" + fileName);
                photoUpload.PostedFile.SaveAs(path);
                TempCotagDetail.image_path = "~/Assets/UserImages/" + fileName;
            }
            
            BusinessLayer.BLCotagDetail bls = new BusinessLayer.BLCotagDetail();

            switch (bls.CreateCotagDetail(TempCotagDetail))
            {
                case Util.OperationStatus.successful:
                    lblMsg.Text = "Cotag Detail Created";
                    ClearTextBoxes();
                    fillGridView();
                    Response.Redirect(Request.RawUrl);
                    break;
                case Util.OperationStatus.Unsuccessful:
                    lblMsg.Text = "Creation could not be carried out";
                    break;
                case Util.OperationStatus.Exists:
                    lblMsg.Text = "Cotag  Detail already exists";
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

    protected void gvCotagDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {

            deleteButton.Enabled = true;
            btnGenerate.Visible = true;
            btnGenerate.Enabled = true;

            BLCotagDetail bls = new BLCotagDetail();
            tb_CotagDetail TempCotagDetail;
            TempCotagDetail = bls.GetCotagDetailNo(Int32.Parse(gvCotagDetail.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString()));
            ddlType.SelectedValue = TempCotagDetail.Type.ToString();
            ddlType_SelectedIndexChanged(sender, e);
            btnEdit.Visible = true;
            btnNew.Visible = false;
            txtCotagNo.Text = TempCotagDetail.CotagNo.ToString();
            chkIsActive.Checked = TempCotagDetail.IsActive;
            txtEndDate.Text = TempCotagDetail.EndDate.ToString();
            txtIDNo.Text = TempCotagDetail.IDNo;
            txtMobile.Text = TempCotagDetail.mobile;
            txtStartDate.Text = TempCotagDetail.StartDate.ToString();
            txtTelephone.Text = TempCotagDetail.telephone;
            txtName.Text = TempCotagDetail.Name;
            txtSurname.Text = TempCotagDetail.Surname;
            ddlAssemblyPoint.SelectedValue = TempCotagDetail.AssemblyPoint_ID.ToString();
            ddlCotagDescription.SelectedValue = TempCotagDetail.cotagDesc_ID.ToString();

            if (TempCotagDetail.zoneID != null)
            {
                ddlZone.SelectedValue = TempCotagDetail.zoneID.ToString();
            }


            chkVoda.Checked = TempCotagDetail.isProvider;
            if (TempCotagDetail.serviceDesc_ID != null)
            {
                ddlServices.SelectedValue = TempCotagDetail.serviceDesc_ID.ToString();
            }
            ddlDepartment.SelectedValue = TempCotagDetail.DepartmentGUID.ToString();
            DropDownList2.SelectedValue = TempCotagDetail.Site_ID.ToString();

            if (DropDownList1.DataSource == null)
            {
                DropDownList1.Enabled = true;
                BLSites sites = new BLSites();
                tb_Site tbS = sites.GetSiteNo(Convert.ToInt32(TempCotagDetail.Site_ID.ToString()));
                int end = tbS.EndLevel;
                int start = tbS.StartLevel;
                for (int i = start; i <= end; i++)
                {
                    DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }

            DropDownList1.SelectedValue = TempCotagDetail.site_level.ToString();

            if (TempCotagDetail.companyDesc_ID != null)
            {
                ddlCompanies.SelectedValue = TempCotagDetail.companyDesc_ID.ToString();
            }

            if (TempCotagDetail.projectManager_Cotag != null)
            {
                TextBox2.Text = TempCotagDetail.projectManager_Cotag.ToString();
            }
            if (TempCotagDetail.IsLogicallyDeleted)
            {
                btnEdit.Visible = false;
                btnNew.Visible = false;
                deleteButton.Enabled = false;
                txtCotagNo.Enabled = false;
                txtTelephone.Enabled = false;
                txtStartDate.Enabled = false;
                txtMobile.Enabled = false;
                txtIDNo.Enabled = false;
                txtEndDate.Enabled = false;
                ddlAssemblyPoint.Enabled = false;
                ddlCotagDescription.Enabled = false;
                btnCheck.Enabled = false;
                chkIsActive.Enabled = false;
            }
            lblMsg.Text = "";
        }

    }
    private DataTable ToDataTable<T>(List<T> items)
    {
        var tb = new DataTable(typeof(T).Name);

        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in props)
        {
            Type t = GetCoreType(prop.PropertyType);
            tb.Columns.Add(prop.Name, t);
        }


        foreach (T item in items)
        {
            var values = new object[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(item, null);
            }

            tb.Rows.Add(values);
        }

        return tb;
    }

    /// <summary>
    /// Determine of specified type is nullable
    /// </summary>
    public static bool IsNullable(Type t)
    {
        return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
    }

    /// <summary>
    /// Return underlying type if type is Nullable otherwise return the type
    /// </summary>
    public static Type GetCoreType(Type t)
    {
        if (t != null && IsNullable(t))
        {
            if (!t.IsValueType)
            {
                return t;
            }
            else
            {
                return Nullable.GetUnderlyingType(t);
            }
        }
        else
        {
            return t;
        }
    }

    protected void fillGridView()
    {
        if (chkShow.Checked)
        {
            chkShow.Text = "Hide Deleted";
            BLCotagDetail CotagDetail = new BLCotagDetail();
            gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail();
            gvCotagDetail.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLCotagDetail CotagDetail = new BLCotagDetail();
            gvCotagDetail.DataSource = CotagDetail.GetCotagDetail();
            gvCotagDetail.DataBind();

        }
        myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagDetailView>)gvCotagDetail.DataSource).ToList());
    }

    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void gvCotagDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BLCotagDetail bls = new BLCotagDetail();
        gvCotagDetail.PageIndex = e.NewPageIndex;

        if (HttpContext.Current.Session["SortColumn"] != null)
        {
            string sortcolumn = HttpContext.Current.Session["SortColumn"].ToString();
            SortDirection sortdirection = (SortDirection)HttpContext.Current.Session["SortColumnDirection"];

            if (chkShow.Checked)
            {
                chkShow.Text = "Hide Deleted";
                BLCotagDetail CotagDetail = new BLCotagDetail();


                if (sortdirection == SortDirection.Ascending)
                {
                    if (sortcolumn == "CotagNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.CotagNo);
                    }
                    else if (sortcolumn == "AssemblyPoint_Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.AssemblyPoint_Name);
                    }
                    else if (sortcolumn == "Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.Name);
                    }
                    else if (sortcolumn == "Surname")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.Surname);
                    }
                    else if (sortcolumn == "Telephone")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.telephone);
                    }
                    else if (sortcolumn == "Mobile")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.mobile);
                    }
                    else if (sortcolumn == "isProvider")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.isPovider);
                    }
                    else if (sortcolumn == "IDNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.IDNo);
                    }
                }
                else
                {
                    if (sortcolumn == "CotagNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.CotagNo);
                    }
                    else if (sortcolumn == "AssemblyPoint_Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.AssemblyPoint_Name);
                    }
                    else if (sortcolumn == "Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.Name);
                    }
                    else if (sortcolumn == "Surname")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.Surname);
                    }
                    else if (sortcolumn == "Telephone")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.telephone);
                    }
                    else if (sortcolumn == "Mobile")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.mobile);
                    }
                    else if (sortcolumn == "isProvider")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.isPovider);
                    }
                    else if (sortcolumn == "IDNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.IDNo);
                    }
                }



                //gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => sortcolumn);
                gvCotagDetail.DataBind();
            }
            else
            {
                chkShow.Text = "Show Deleted";
                BLCotagDetail CotagDetail = new BLCotagDetail();
                if (sortdirection == SortDirection.Ascending)
                {
                    if (sortcolumn == "CotagNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.CotagNo);
                    }
                    else if (sortcolumn == "AssemblyPoint_Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.AssemblyPoint_Name);
                    }
                    else if (sortcolumn == "Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.Name);
                    }
                    else if (sortcolumn == "Surname")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.Surname);
                    }
                    else if (sortcolumn == "Telephone")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.telephone);
                    }
                    else if (sortcolumn == "Mobile")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.mobile);
                    }
                    else if (sortcolumn == "isProvider")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.isPovider);
                    }
                    else if (sortcolumn == "IDNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.IDNo);
                    }
                }
                else
                {
                    if (sortcolumn == "CotagNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.CotagNo);
                    }
                    else if (sortcolumn == "AssemblyPoint_Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.AssemblyPoint_Name);
                    }
                    else if (sortcolumn == "Name")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.Name);
                    }
                    else if (sortcolumn == "Surname")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.Surname);
                    }
                    else if (sortcolumn == "Telephone")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.telephone);
                    }
                    else if (sortcolumn == "Mobile")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.mobile);
                    }
                    else if (sortcolumn == "isProvider")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.isPovider);
                    }
                    else if (sortcolumn == "IDNo")
                    {
                        gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.IDNo);
                    }
                }
                //  gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x=>x.AssemblyPoint_Name );
                gvCotagDetail.DataBind();

            }
            myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagDetailView>)gvCotagDetail.DataSource).ToList());
        }
        else
        {
            fillGridView();
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        TaskManagementService svc = new TaskManagementService();
        UserDetailsViewRequest req = new UserDetailsViewRequest();
        req.Criteria = new UserDetailsViewCriteria();
        req.Criteria.IDCard = txtIDNo.Text;
        req.ClientTag = "C6F19590-B6AE-423A-A19B-45F5AE2B2569";
        UserDetailsViewResponse resp = svc.GetUserDetails(req);
        if (resp != null)
        {
            if (resp.Acknowledge == AcknowledgeType.Success)
            {
                try
                {

                    if (resp.UserDetails[0] != null)
                    {
                        if (resp.UserDetails[0].PhoneNumber != null)
                            txtTelephone.Text = resp.UserDetails[0].PhoneNumber;
                        if (resp.UserDetails[0].Name != null)
                            txtName.Text = resp.UserDetails[0].Name;
                        if (resp.UserDetails[0].Surname != null)
                            txtSurname.Text = resp.UserDetails[0].Surname;
                        toggleEnable(true);
                        lblMsg.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = resp.Message;
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "User does not exist.";
                    ExceptionHandler.write(ex);
                    toggleEnable(false);
                }
            }
        }
        System.Threading.Thread.Sleep(3000);
    }

    protected void toggleEnable(bool enable)
    {
        txtCotagNo.Enabled = enable;
        txtEndDate.Enabled = enable;
        txtMobile.Enabled = enable;
        txtStartDate.Enabled = enable;
        txtTelephone.Enabled = enable;
        ddlAssemblyPoint.Enabled = enable;
        ddlCotagDescription.Enabled = enable;
        btnClear.Enabled = enable;
        btnEdit.Enabled = enable;
        btnNew.Enabled = enable;
        deleteButton.Enabled = enable;
        chkVoda.Enabled = enable;
        ddlServices.Enabled = enable;
        ddlDepartment.Enabled = enable;
        DropDownList1.Enabled = enable;
        DropDownList2.Enabled = enable;
        ddlCompanies.Enabled = false;
        TextBox2.Enabled = false;
        btnGenerate.Enabled = enable;
        btnGenerate.Visible = enable;
        ddlZone.Enabled = enable;
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        btnNew.Visible = true;
        btnEdit.Visible = false;
        deleteButton.Enabled = false;
        try
        {
            DataLayer.tb_CotagDetail TempCotagDetail;
            TempCotagDetail = new DataLayer.tb_CotagDetail();
            TempCotagDetail.CotagNo = Int32.Parse(txtCotagNo.Text);
            TempCotagDetail.AssemblyPoint_ID = Int32.Parse(ddlAssemblyPoint.SelectedValue);
            TempCotagDetail.cotagDesc_ID = Int32.Parse(ddlCotagDescription.SelectedValue);
            TempCotagDetail.telephone = txtTelephone.Text;
            TempCotagDetail.mobile = txtMobile.Text;
            TempCotagDetail.IDNo = txtIDNo.Text;
            TempCotagDetail.Name = txtName.Text;
            TempCotagDetail.Surname = txtSurname.Text;
            TempCotagDetail.Type = ddlType.SelectedValue;

            if (ddlZone.SelectedValue != "-1")
            {
                TempCotagDetail.zoneID = Int32.Parse(ddlZone.SelectedValue);
            }

            DateTime dt = new DateTime();
            if (DateTime.TryParse(txtEndDate.Text, out dt))
            {
                TempCotagDetail.EndDate = DateTime.Parse(txtEndDate.Text);
            }
            if (DateTime.TryParse(txtStartDate.Text, out dt))
            {
                TempCotagDetail.StartDate = DateTime.Parse(txtStartDate.Text);
            }
            TempCotagDetail.Updatedby = Common.getUsername();
            TempCotagDetail.Updatedon = DateTime.Now;
            TempCotagDetail.IsActive = false;
            TempCotagDetail.IsLogicallyDeleted = true;
            BusinessLayer.BLCotagDetail bls = new BusinessLayer.BLCotagDetail();


            switch (bls.UpdateCotagDetail(TempCotagDetail))
            {
                case Util.OperationStatus.successful:
                    lblMsg.Text = "Cotag Detail Updated";
                    ClearTextBoxes();
                    fillGridView();
                    Response.Redirect(Request.RawUrl);
                    break;
                case Util.OperationStatus.Unsuccessful:
                    lblMsg.Text = "Update could not be carried out";
                    break;
                case Util.OperationStatus.Exists:
                    lblMsg.Text = "Cotag Detail already exists";
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

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex == 0)
        {
            ClearTextBoxes();
            toggleEnable(false);
            txtIDNo.Enabled = false;
            btnCheck.Enabled = false;
            txtName.Enabled = false;
            txtSurname.Enabled = false;
        }
        else if (ddlType.SelectedIndex == 1)
        {
            ClearTextBoxes();
            toggleEnable(true);
            txtCotagNo.Text = generateNewCotagNo().ToString();
            txtIDNo.Enabled = true;
            btnCheck.Enabled = true;
            txtName.Enabled = false;
            txtSurname.Enabled = false;
        }
        else if (ddlType.SelectedIndex == 2)
        {
            ClearTextBoxes();
            txtCotagNo.Text = generateNewCotagNo().ToString();
            toggleEnable(true);
            txtIDNo.Enabled = true;
            btnCheck.Enabled = false;
            txtName.Enabled = true;
            txtSurname.Enabled = true;
        }
        DropDownList1.Enabled = false;
    }

    protected void txtIDNo_TextChanged(object sender, EventArgs e)
    {
        if (txtIDNo.Text.Length < 8)
        {
            txtIDNo.Text = txtIDNo.Text.PadLeft(8, '0');
            txtIDNo.Text = txtIDNo.Text.ToUpper();
        }
    }

    protected void ddlCotagDescription_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCotagDescription.SelectedItem.Text.Contains("Contractor") == true)
        {
            ddlCompanies.Enabled = true;
            TextBox2.Enabled = true;
        }
        else
        {
            ddlCompanies.Enabled = false;
            TextBox2.Enabled = false;
        }
    }

    protected void gvCotagDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1.Enabled = true;
        BLSites sites = new BLSites();
        tb_Site tbS = sites.GetSiteNo(Convert.ToInt32(DropDownList2.SelectedValue));
        int end = tbS.EndLevel;
        int start = tbS.StartLevel;
        for (int i = start; i <= end; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }



    }

    protected void DropDownList2_TextChanged(object sender, EventArgs e)
    {
        DropDownList1.Enabled = true;
        BLSites sites = new BLSites();
        tb_Site tbS = sites.GetSiteNo(Convert.ToInt32(DropDownList2.SelectedValue));
        int end = tbS.EndLevel;
        int start = tbS.StartLevel;
        for (int i = start; i <= end; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void DropDownList2_DataBinding(object sender, EventArgs e)
    {
        DropDownList1.Enabled = true;
        BLSites sites = new BLSites();
        tb_Site tbS = sites.GetSiteNo(Convert.ToInt32(DropDownList2.SelectedValue));
        int end = tbS.EndLevel;
        int start = tbS.StartLevel;
        for (int i = start; i <= end; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    public int generateNewCotagNo()
    {
        BLCotagDetail cd = new BLCotagDetail();
        ArrayList cdd = new ArrayList();
        foreach (DataLayer.Views.CotagDetailView cdv in cd.GetCotagDetail())
        {
            cdd.Add(cdv.CotagNo);
        }
        var range = Enumerable.Range(1, 3000).Where(i => !cdd.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(0, 3000 - cdd.Count);
        return range.ElementAt(index);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ClearTextBoxes();
        toggleEnable(true);
        DropDownList1.Enabled = false;
        txtIDNo.Enabled = true;
        txtName.Enabled = true;
        txtCotagNo.ReadOnly = false;
        txtSurname.Enabled = true;
        btnNew.Enabled = false;
        btnEdit.Enabled = false;
        deleteButton.Enabled = false;
        btnFind.Visible = true;
        chkBypass.Visible = true;
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        BLCotagDetail CotagDetail = new BLCotagDetail();
        int site = -1;
        int level = -1;
        if (DropDownList2.SelectedValue != "")
        {
            site = Convert.ToInt32(DropDownList2.SelectedValue);
        }
        if (DropDownList1.SelectedValue != "")
        {
            level = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
        }
        int company = -1;
        if (ddlCompanies.Enabled == true)
        {
            company = Convert.ToInt32(ddlCompanies.SelectedValue);
        }
        gvCotagDetail.DataSource = CotagDetail.GetCotagDetailAdvanced(txtIDNo.Text, txtCotagNo.Text, chkIsActive.Checked, txtName.Text, txtSurname.Text, ddlType.SelectedValue, txtStartDate.Text, txtEndDate.Text, Convert.ToInt32(ddlAssemblyPoint.SelectedValue), txtTelephone.Text, txtMobile.Text, chkVoda.Checked, Convert.ToInt32(ddlServices.SelectedValue), new Guid(), site, level, Convert.ToInt32(ddlCotagDescription.SelectedValue), company, TextBox2.Text,chkBypass.Checked);
        gvCotagDetail.DataBind();
        myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagDetailView>)gvCotagDetail.DataSource).ToList());
        ClearTextBoxes();
        lblMsg.Text = "";
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
        Response.Write(ExportToCSVFile(myDataTable));
        Response.End();

    }

    public static void ReorderTable(ref DataTable table, params String[] columns)
    {
        if (columns.Length != table.Columns.Count)
            throw new ArgumentException("Count of columns must be equal to table.Column.Count", "columns");

        for (int i = 0; i < columns.Length; i++)
        {
            table.Columns[columns[i]].SetOrdinal(i);
        }
    }

    private string ExportToCSVFile(DataTable dtTable)
    {
        ReorderTable(ref dtTable, "CotagNo", "IDNo", "Name", "Surname", "StartDate", "EndDate", "AssemblyPoint_Name", "telephone", "mobile", "isPovider", "CotagDesc_Name", "site_ID", "site_level", "IsActive", "AssemblyPoint_ID", "IsLogicallyDeleted", "CotagDesc_ID", "Createdby", "Createdon", "Updatedby", "Updatedon", "RecordVersion", "Type", "service_ID", "company_ID", "projectManager_Cotag", "departmentGUID","ImagePath");

        List<string> toDelete = new List<string>{"IsActive","AssemblyPoint_ID","IsLogicallyDeleted","CotagDesc_ID","Createdby","Createdon","Updatedby","Updatedon","RecordVersion","Type","service_ID","company_ID","projectManager_Cotag","departmentGUID","ImagePath"};
        foreach (string s in toDelete)
        {
            dtTable.Columns.Remove(s);
        }

        BusinessLayer.BLSites loc = new BusinessLayer.BLSites();
        StringBuilder sbldr = new StringBuilder();
        
        if (dtTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dtTable.Columns)
            {

                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dtTable.Rows)
            {
                foreach (DataColumn column in dtTable.Columns)
                {
                    if (column.ColumnName == "site_ID")
                    {
                        string siteName = loc.GetSiteNo(Convert.ToInt32(row[column].ToString())).SiteDescription;
                        sbldr.Append(siteName + ',');
                    }
                    else
                    {
                        sbldr.Append(row[column].ToString() + ',');
                    }
                }
                sbldr.Append("\r\n");
            }
        }
        return sbldr.ToString();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        bool contractor = false;
        if (ddlCotagDescription.SelectedItem.Text.ToString().ToLower().Contains("contractor") == true)
            contractor = true;


        BLCotagDetail bls = new BLCotagDetail();
        tb_CotagDetail TempCotagDetail;
        TempCotagDetail = bls.GetCotagDetailNo(Int32.Parse(txtCotagNo.Text));


        generateCotag(Server.MapPath(TempCotagDetail.image_path),txtName.Text+" "+txtSurname.Text,ddlAssemblyPoint.SelectedItem.Text.ToString(),txtCotagNo.Text,contractor);
    }

    protected void gvCotagDetail_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (e.SortExpression == (string)HttpContext.Current.Session["SortColumn"])
        {
            // We are resorting the same column, so flip the sort direction
            e.SortDirection =
                ((SortDirection)HttpContext.Current.Session["SortColumnDirection"] == SortDirection.Ascending) ?
                SortDirection.Descending : SortDirection.Ascending;
        }
        HttpContext.Current.Session["SortColumn"] = e.SortExpression;
        HttpContext.Current.Session["SortColumnDirection"] = e.SortDirection;

        if (chkShow.Checked)
        {
            chkShow.Text = "Hide Deleted";
            BLCotagDetail CotagDetail = new BLCotagDetail();
            if (e.SortDirection == SortDirection.Ascending)
            {
                if (e.SortExpression == "CotagNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.CotagNo);
                }
                else if (e.SortExpression == "AssemblyPoint_Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.AssemblyPoint_Name);
                }
                else if (e.SortExpression == "Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.Name);
                }
                else if (e.SortExpression == "Surname")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.Surname);
                }
                else if (e.SortExpression == "Telephone")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.telephone);
                }
                else if (e.SortExpression == "Mobile")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.mobile);
                }
                else if (e.SortExpression == "isProvider")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.isPovider);
                }
                else if (e.SortExpression == "IDNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => x.IDNo);
                }
            }
            else
            {
                if (e.SortExpression == "CotagNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.CotagNo);
                }
                else if (e.SortExpression == "AssemblyPoint_Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.AssemblyPoint_Name);
                }
                else if (e.SortExpression == "Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.Name);
                }
                else if (e.SortExpression == "Surname")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.Surname);
                }
                else if (e.SortExpression == "Telephone")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.telephone);
                }
                else if (e.SortExpression == "Mobile")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.mobile);
                }
                else if (e.SortExpression == "isProvider")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.isPovider);
                }
                else if (e.SortExpression == "IDNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderByDescending(x => x.IDNo);
                }
            }



            //gvCotagDetail.DataSource = CotagDetail.GetLogicallyDeletedCotagDetail().OrderBy(x => e.SortExpression);
            gvCotagDetail.DataBind();
        }
        else
        {
            chkShow.Text = "Show Deleted";
            BLCotagDetail CotagDetail = new BLCotagDetail();
            if(e.SortDirection == SortDirection.Ascending)
            {
                if(e.SortExpression == "CotagNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.CotagNo);
                }
                else if (e.SortExpression == "AssemblyPoint_Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.AssemblyPoint_Name);
                }
                else if(e.SortExpression == "Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.Name);
                }
                else if(e.SortExpression == "Surname")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.Surname);
                }
                else if(e.SortExpression == "Telephone")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.telephone);
                }
                else if (e.SortExpression == "Mobile")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.mobile);
                }
                else if (e.SortExpression == "isProvider")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.isPovider);
                }
                else if (e.SortExpression == "IDNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderBy(x => x.IDNo);
                }
            }
            else
            {
                if (e.SortExpression == "CotagNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.CotagNo);
                }
                else if (e.SortExpression == "AssemblyPoint_Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.AssemblyPoint_Name);
                }
                else if (e.SortExpression == "Name")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.Name);
                }
                else if (e.SortExpression == "Surname")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.Surname);
                }
                else if (e.SortExpression == "Telephone")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.telephone);
                }
                else if (e.SortExpression == "Mobile")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.mobile);
                }
                else if (e.SortExpression == "isProvider")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.isPovider);
                }
                else if (e.SortExpression == "IDNo")
                {
                    gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x => x.IDNo);
                }
            }
          //  gvCotagDetail.DataSource = CotagDetail.GetCotagDetail().OrderByDescending(x=>x.AssemblyPoint_Name );
            gvCotagDetail.DataBind();

        }
        myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagDetailView>)gvCotagDetail.DataSource).ToList());
    }

}