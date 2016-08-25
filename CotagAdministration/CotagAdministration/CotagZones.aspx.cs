using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Reflection;
using DataLayer;
using System.Collections;

namespace CotagAdministration
{
    public partial class CotagZones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExport);

            if (!Page.IsPostBack)
            {
                try
                {
                    BusinessLayer.BLCotagDetail cotagDetail = new BusinessLayer.BLCotagDetail();
                    BusinessLayer.BLZones zones = new BusinessLayer.BLZones();

                    ddlCotagNos.DataSource = cotagDetail.GetCotagDetail();
                    ddlCotagNos.DataTextField = "CotagNo";
                    ddlCotagNos.DataValueField = "CotagNo";
                    ddlCotagNos.DataBind();
                    ddlCotagNos.Items.Add(new ListItem("Please select a Cotag No.", "-1"));
                    ddlCotagNos.SelectedValue = "-1";

                    lbZone.DataSource = zones.GetZones();
                    lbZone.DataTextField = "Description";
                    lbZone.DataValueField = "ID";
                    lbZone.DataBind();
                    
                    ddSearchZoneID.DataSource = zones.GetZones();
                    ddSearchZoneID.DataTextField = "Description";
                    ddSearchZoneID.DataValueField = "ID";
                    ddSearchZoneID.DataBind();
                    ddSearchZoneID.Items.Add(new ListItem("Please select a Zone", "-1"));
                    ddSearchZoneID.SelectedValue = "-1";

                    ddSearchCotagNo.DataSource = cotagDetail.GetCotagDetail();
                    ddSearchCotagNo.DataTextField = "CotagNo";
                    ddSearchCotagNo.DataValueField = "CotagNo";
                    ddSearchCotagNo.DataBind();
                    ddSearchCotagNo.Items.Add(new ListItem("Please select a Cotag No.", "-1"));
                    ddSearchCotagNo.SelectedValue = "-1";

                    BusinessLayer.BLCotagDetail CotagDetail = new BusinessLayer.BLCotagDetail();
                    GridView1.DataSource = CotagDetail.GetCotagZones();
                    GridView1.DataBind();

                    myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagZonesView>)GridView1.DataSource).ToList());

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }        

        }

        protected void lbZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCotagNos.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select a cotag number";                
            }
            else
            {
                int idx = lbZone.SelectedIndex;
                ListItem item = lbZone.SelectedItem;
                lbZone.Items.Remove(item);
                lbCurrentZone.SelectedIndex = -1;
                lbCurrentZone.Items.Add(item);
                lblMessage.Text = "Zone saved";

              
                    try
                    {
                        DataLayer.tb_CotagZones tempCotagZone;
                        tempCotagZone = new DataLayer.tb_CotagZones();
                        tempCotagZone.CotagNo = Int32.Parse(ddlCotagNos.SelectedValue);
                        tempCotagZone.CreatedBy = Common.getUsername();
                        tempCotagZone.CreatedOn = DateTime.Now;
                        tempCotagZone.IsActive = true;
                        tempCotagZone.IsLogicallyDeleted = false;
                        tempCotagZone.RecordVersion = 1;
                        tempCotagZone.ZoneID = Convert.ToInt32(item.Value.ToString());

                        BusinessLayer.BLCotagZones blap = new BusinessLayer.BLCotagZones();


                        switch (blap.CreateCotagZone(tempCotagZone))
                        {
                            case Util.OperationStatus.successful:
                                
                                BusinessLayer.BLCotagDetail CotagDetail = new BusinessLayer.BLCotagDetail();
                    GridView1.DataSource = CotagDetail.GetCotagZones();
                    GridView1.DataBind();

                    myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagZonesView>)GridView1.DataSource).ToList());

                                break;
                            case Util.OperationStatus.Unsuccessful:
                                lblMessage.Text = "Creation could not be carried out";
                                break;
                         
                            default:
                                break;
                        }
                    }
                    catch (Exception exc)
                    {
                        lblMessage.Text = "An error Occured. Please Contact the system Administrator";
                        ExceptionHandler.write(exc);
                    }
                
            }

        }

        protected void lbCurrentZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCotagNos.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select a cotag number";
            }
            else
            {
                int idx = lbCurrentZone.SelectedIndex;
                ListItem item = lbCurrentZone.SelectedItem;
                lbCurrentZone.Items.Remove(item);
                lbZone.SelectedIndex = -1;
                lbZone.Items.Add(item);
                lblMessage.Text = "Zone Deleted";

                try
                {
                    DataLayer.tb_CotagZones tempCotagZone;
                    tempCotagZone = new DataLayer.tb_CotagZones();
                    tempCotagZone.CotagNo = Int32.Parse(ddlCotagNos.SelectedValue);
                    tempCotagZone.IsActive = false;
                    tempCotagZone.IsLogicallyDeleted = true;
                    tempCotagZone.RecordVersion = 1;
                    tempCotagZone.ZoneID = Convert.ToInt32(item.Value.ToString());
                    tempCotagZone.UpdatedBy = Common.getUsername();
                    tempCotagZone.UpdatedOn = DateTime.Now;

                    BusinessLayer.BLCotagZones blap = new BusinessLayer.BLCotagZones();


                    switch (blap.LogicallyDeleteCotagZone(tempCotagZone))
                    {
                        case Util.OperationStatus.successful:

                            BusinessLayer.BLCotagDetail CotagDetail = new BusinessLayer.BLCotagDetail();
                            GridView1.DataSource = CotagDetail.GetCotagZones();
                            GridView1.DataBind();
                            myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagZonesView>)GridView1.DataSource).ToList());
                            break;
                        case Util.OperationStatus.Unsuccessful:
                            lblMessage.Text = "Creation could not be carried out";
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception exc)
                {
                    lblMessage.Text = "An error Occured. Please Contact the system Administrator";
                    ExceptionHandler.write(exc);
                }
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int cotagno = Convert.ToInt32(ddSearchCotagNo.SelectedValue.ToString());
            int zoneid = Convert.ToInt32(ddSearchZoneID.SelectedValue.ToString());
            BusinessLayer.BLCotagDetail CotagDetail = new BusinessLayer.BLCotagDetail();
            GridView1.DataSource = CotagDetail.SearchCotagZones(zoneid,cotagno);
            GridView1.DataBind();

            myDataTable = ToDataTable(((IQueryable<DataLayer.Views.CotagZonesView>)GridView1.DataSource).ToList());
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/x-msexcel";
            Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
            Response.Write(ExportToCSVFile(myDataTable));
            Response.End();
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
            ReorderTable(ref dtTable, "CotagNo", "IDNo", "Name", "Surname", "CotagDesc_Name", "Zone", "ZoneID");

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
                        sbldr.Append(row[column].ToString() + ',');                      
                    }
                    sbldr.Append("\r\n");
                }
            }
            return sbldr.ToString();
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
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

        protected void ddlCotagNos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCotagNos.SelectedValue != "-1")
            {
                BusinessLayer.BLZones zones = new BusinessLayer.BLZones();
                BusinessLayer.BLCotagDetail czones = new BusinessLayer.BLCotagDetail();
                int cotagno = Convert.ToInt32(ddlCotagNos.SelectedValue);
                lbCurrentZone.DataSource = czones.GetCotagZones().Where(x => x.CotagNo == cotagno);
                lbCurrentZone.DataTextField = "Zone";
                lbCurrentZone.DataValueField = "ZoneID";
                lbCurrentZone.DataBind();
                
                lbZone.DataSource = zones.GetZones();
                lbZone.DataTextField = "Description";
                lbZone.DataValueField = "ID";
                lbZone.DataBind();

                foreach (ListItem l in lbCurrentZone.Items)
                {
                    lbZone.Items.Remove(l);
                }
     
            }
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = myDataTable;
            GridView1.DataBind();
        }

    }
}