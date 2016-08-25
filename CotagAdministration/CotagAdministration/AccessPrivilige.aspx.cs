using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Collections;
using DataLayer;
using System.Text;
using System.Data;
using System.Reflection;


    public partial class AccessPrivilige : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnNew);
            scriptManager.RegisterPostBackControl(this.btnExport);
            scriptManager.RegisterPostBackControl(this.gvCotagDetail);
            if (!Page.IsPostBack)
            {
                BusinessLayer.BLSites loc = new BusinessLayer.BLSites();

                ddlSite.DataSource = loc.GetSites();

                ddlSite.DataTextField = "SiteDescription";
                ddlSite.DataValueField = "SiteNo";
                ddlSite.DataBind();
                ddlSite.Items.Add(new ListItem("Please select a Location...", "-1"));
                ddlSite.SelectedValue = "-1";             
            }
            BLAccessPriviliges accessPriv = new BLAccessPriviliges();
            gvCotagDetail.DataSource = accessPriv.GetAccessPriviligesUsers();
            gvCotagDetail.DataBind();

            if (gvCotagDetail.Rows.Count == 0)
            {
                btnExport.Visible = false;
            }
            else
            {
                btnExport.Visible = true;
            }

        }

        protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessLayer.BLLocations loc = new BusinessLayer.BLLocations();
            cclAccessPoints.DataSource = loc.GetLocationBySiteID(Convert.ToInt32(ddlSite.SelectedValue));
            cclAccessPoints.DataTextField = "Description";
            cclAccessPoints.DataValueField = "ID";
            cclAccessPoints.DataBind();

            foreach (string s in AccessPoint.Split(';'))
            {
                ListItem listItem = cclAccessPoints.Items.FindByValue(s);
                if (listItem != null)
                {
                    listItem.Selected = true;
                }
            }

            
        }
        string _accessPoints = ";";
      
        private string AccessPoint
        {
            get
            {
                if (ViewState[_accessPoints] == null)
                    return "";
                return (string)ViewState[_accessPoints];
            }
            set
            {
                ViewState[_accessPoints] = value;
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            TimeSpan from = new TimeSpan();
            TimeSpan to = new TimeSpan();
            if (Page.IsValid)
            {
                try
                {  
                    foreach (string s in AccessPoint.Split(';'))
                    {
                        if (s != "")
                        {
                            DataLayer.tb_AccessPriviliges TempAccessPrivilige = new tb_AccessPriviliges();
                            TempAccessPrivilige.CotagNo = Convert.ToInt32(txtDescription.Text);
                            TempAccessPrivilige.AccessPointID = Convert.ToInt32(s);
                            if (TextBox2.Text != "")
                            {
                                TimeSpan.TryParse(TextBox2.Text, out from);
                                TempAccessPrivilige.accessFrom = from;
                            }
                            if (TextBox1.Text != "")
                            {
                                TimeSpan.TryParse(TextBox1.Text, out to);
                                TempAccessPrivilige.accessTo = to;
                            }
                            TempAccessPrivilige.Createdby = Common.getUsername();
                            TempAccessPrivilige.Createdon = DateTime.Now;
                            TempAccessPrivilige.IsActive = chkIsActive.Checked;
                            TempAccessPrivilige.RecordVersion = 1;

                            BusinessLayer.BLAccessPriviliges blap = new BusinessLayer.BLAccessPriviliges();

                            switch (blap.CreateAccessPrivilige(TempAccessPrivilige))
                            {
                                case Util.OperationStatus.successful:
                                    lblMsg.Text = "Access Privilige Created";
                                    // ClearTextBoxes();
                                    // fillGridView();
                                    break;
                                case Util.OperationStatus.Unsuccessful:
                                    lblMsg.Text = "Creation could not be carried out";
                                    break;
                                case Util.OperationStatus.Exists:
                                    lblMsg.Text = "Access Privilige already exists";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    BLAccessPriviliges accessPriv = new BLAccessPriviliges();
                    gvCotagDetail.DataSource = accessPriv.GetAccessPriviligesUsers();
                    gvCotagDetail.DataBind();

                    if (gvCotagDetail.Rows.Count == 0)
                    {
                        btnExport.Visible = false;
                    }
                    else
                    {
                        btnExport.Visible = true;
                    }

                }
                catch (Exception exc)
                {
                    lblMsg.Text = "An error Occured. Please Contact the system Administrator";
                    ExceptionHandler.write(exc);
                }
            }
        }

        protected void cclAccessPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in cclAccessPoints.Items)
            {
                if (li.Selected == true)
                {
                    if (AccessPoint.Contains(li.Value) == false)
                    {
                        AccessPoint = AccessPoint+li.Value+";";
                    }
                }
                else
                {
                    if (AccessPoint.Contains(li.Value) == true)
                    {
                        AccessPoint = AccessPoint.Replace(li.Value + ";", "");
                    }

                }
            }  
        }

        protected void gvCotagDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            BLAccessPriviliges bls = new BLAccessPriviliges();
            gvCotagDetail.PageIndex = e.NewPageIndex;
            fillGridView();
        }

        protected void fillGridView()
        {


            BLAccessPriviliges bls = new BLAccessPriviliges();
            gvCotagDetail.DataSource = bls.GetAccessPriviligesUsers();
            gvCotagDetail.DataBind();

        }


        protected void gvCotagDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                btnEdit.Visible = true;
                btnNew.Visible = false;
                deleteButton.Enabled = true;
                txtId.Value = gvCotagDetail.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString();
                BLAccessPriviliges bls = new BLAccessPriviliges();
                txtDescription.Text = txtId.Value;
                foreach (DataLayer.Views.AccessPriviligesView acv in bls.GetAllAccessPriviligesByUser(Int32.Parse(txtId.Value)))
                {
                    AccessPoint = AccessPoint + acv.AccesPointsID+ ";";
                }
                TextBox2.Text = gvCotagDetail.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[3].Text.ToString().Substring(0,5);
                TextBox1.Text = gvCotagDetail.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[4].Text.ToString().Substring(0, 5); 
                
                lblMsg.Text = "";
            }
            if (e.CommandName == "Export")
            {
                BLAccessPriviliges accessPriv = new BLAccessPriviliges();
                myDataTable = ToDataTable(accessPriv.GetAccessPriviligesUsersDetailed(Convert.ToInt32(gvCotagDetail.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text.ToString())).ToList());
                Response.ContentType = "Application/x-msexcel";
                Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
                Response.Write(ExportToCSVFile(myDataTable));
                Response.End();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearTextboxes();
        }

        public void clearTextboxes()
        {
            txtDescription.Text = "";
            AccessPoint = ";";
            TextBox2.Text = "";
            TextBox1.Text = "";
            ddlSite.SelectedIndex = -1;
            
        }
        string commonError = "An error Occured. Please Contact the system Administrator";
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                deleteButton.Enabled = false;
                try
                {
                    if ((TextBox2.Text == "") && (TextBox1.Text == ""))
                    {
                        commonError = "Access From/To is required";
                        throw new Exception("");
                    }

                    if ((TextBox2.Text == "") && (TextBox1.Text != ""))
                    {
                        commonError = "Access From is required";
                        throw new Exception("");
                    }
                    if ((TextBox1.Text == "") && (TextBox2.Text != ""))
                    {
                        commonError = "Access To is required";
                        throw new Exception("");
                    }

                    BLAccessPriviliges bls = new BLAccessPriviliges();
                    txtDescription.Text = txtId.Value;
                    ArrayList accessp = new ArrayList();
                   
                    foreach (string s in AccessPoint.Split(';'))
                    {
                        accessp.Add(s);
                    }
                    int id = 0;
                    foreach (DataLayer.Views.AccessPriviligesView acv in bls.GetAllAccessPriviligesByUser(Int32.Parse(txtId.Value)))
                    {
                        id = acv.ID;
                        if (accessp.Contains(acv.AccesPointsID.ToString()) == false)
                        {
                            DataLayer.tb_AccessPriviliges TempAccess;
                            TempAccess = new DataLayer.tb_AccessPriviliges();
                            TempAccess.ID = acv.ID;
                            TempAccess.CotagNo = Convert.ToInt32(txtDescription.Text);
                            TempAccess.AccessPointID = acv.AccesPointsID;
                            TempAccess.IsActive = false;
                            TempAccess.IsLogicallyDeleted = true;

                            switch (bls.UpdateAccessPrivilige(TempAccess))
                            {
                                case Util.OperationStatus.successful:
                                    lblMsg.Text = "Access Priviliges Updated";
                                    break;
                                case Util.OperationStatus.Unsuccessful:
                                    lblMsg.Text = "Update could not be carried out";
                                    break;
                                case Util.OperationStatus.Exists:
                                    lblMsg.Text = "Access Privilige already exists";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }








                 
                    string hour = TextBox2.Text.Split(':')[0];
                    string minute = TextBox2.Text.Split(':')[1];
                    TimeSpan ts = new TimeSpan(Convert.ToInt32(hour), Convert.ToInt32(minute),0);

                    string hour2 = TextBox1.Text.Split(':')[0];
                    string minute2 = TextBox1.Text.Split(':')[1];
                    TimeSpan ts2 = new TimeSpan(Convert.ToInt32(hour2), Convert.ToInt32(minute2), 0);


                    foreach (string s in AccessPoint.Split(';'))
                    {
                        DataLayer.tb_AccessPriviliges TempAccess;
                        TempAccess = new DataLayer.tb_AccessPriviliges();
                        TempAccess.ID = id;
                        TempAccess.CotagNo = Convert.ToInt32(txtDescription.Text);
                        TempAccess.AccessPointID = Convert.ToInt32(s);
                        TempAccess.accessFrom = ts;
                        TempAccess.accessTo = ts2;
                    


                    switch (bls.UpdateAccessPrivilige(TempAccess))
                    {
                        case Util.OperationStatus.successful:
                            lblMsg.Text = "Access Priviliges Updated";
                           
                            break;
                        case Util.OperationStatus.Unsuccessful:
                            lblMsg.Text = "Update could not be carried out";
                            break;
                        case Util.OperationStatus.Exists:
                            lblMsg.Text = "Access Privilige already exists";
                            break;
                        default:
                            break;
                    }
                    }
                    clearTextboxes();
                    fillGridView();

                }
                catch (Exception exc)
                {
                    lblMsg.Text = commonError;
                    ExceptionHandler.write(exc);
                }
            }
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            BLAccessPriviliges accessPriv = new BLAccessPriviliges();
            myDataTable =ToDataTable(accessPriv.GetAccessPriviligesUsersDetailed(0).ToList());
            Response.ContentType = "Application/x-msexcel";
            Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
            Response.Write(ExportToCSVFile(myDataTable));
            Response.End();
        }
        private string ExportToCSVFile(DataTable dtTable)
        {
            //ReorderTable(ref dtTable, "CotagNo", "idCard", "name","surname", "accessFrom", "accessTo", "telephone", "mobile");
            ReorderTable(ref dtTable, "CotagNo", "idCard", "name", "surname","siteDescription","accessDescription", "accessFrom", "accessTo", "telephone", "mobile");
           // List<string> toDelete = new List<string> { "IsActive", "AssemblyPoint_ID", "IsLogicallyDeleted", "CotagDesc_ID", "Createdby", "Createdon", "Updatedby", "Updatedon", "RecordVersion", "Type", "service_ID", "company_ID", "projectManager_Cotag", "departmentGUID", "ImagePath" };
           // foreach (string s in toDelete)
            //{
            //    dtTable.Columns.Remove(s);
           // }

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

        public static void ReorderTable(ref DataTable table, params String[] columns)
        {
            if (columns.Length != table.Columns.Count)
                throw new ArgumentException("Count of columns must be equal to table.Column.Count", "columns");

            for (int i = 0; i < columns.Length; i++)
            {
                table.Columns[columns[i]].SetOrdinal(i);
            }
        }

        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked == true)
            {
                foreach (ListItem li in cclAccessPoints.Items)
                {
                    li.Selected = true;
                    AccessPoint = AccessPoint + li.Value + ";";
                }
            }
            else
            {
                foreach (ListItem li in cclAccessPoints.Items)
                {
                    li.Selected = false;
                    AccessPoint = AccessPoint.Replace(li.Value + ";", "");
                }
            }
        }
        
    }
