using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Text;

public partial class ZoneTimes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnExport);
        if (!Page.IsPostBack)
        {
            BusinessLayer.BLZones s = new BusinessLayer.BLZones();
            ddlZones.DataSource = s.GetZones();
            ddlZones.DataTextField = "Description";
            ddlZones.DataValueField = "ID";
            ddlZones.DataBind();
            ddlZones.Items.Add(new ListItem("Please select a Zone...", "-1"));

            BusinessLayer.BLSites site = new BusinessLayer.BLSites();
            ddlSites.DataSource = site.GetSites();
            ddlSites.DataTextField = "SiteDescription";
            ddlSites.DataValueField = "SiteNo";
            ddlSites.DataBind();

            BusinessLayer.BLLocations loc = new BusinessLayer.BLLocations();
            lbAccessPoints.DataSource = loc.GetLocationBySiteID(Convert.ToInt32(ddlSites.SelectedValue));
            lbAccessPoints.DataTextField = "Description";
            lbAccessPoints.DataValueField = "ID";
            lbAccessPoints.DataBind();

            BusinessLayer.BLTimes time = new BusinessLayer.BLTimes();
            lbTimes.DataSource = time.GetTimes();
            lbTimes.DataTextField = "Description";
            lbTimes.DataValueField = "ID";
            lbTimes.DataBind();

            BLZoneTimes zoneTimes = new BLZoneTimes();
            foreach (DataLayer.Views.ZoneTimesView zt in zoneTimes.GetZoneTimes())
            {
                AccessPoint+= zt.location_id + "-" + zt.time_id + "-" + zt.zone_id+";";
            }
            populateAccessList();

            cclZoneTimes.DataSource = s.GetZones();
            cclZoneTimes.DataTextField = "Description";
            cclZoneTimes.DataValueField = "ID";
            cclZoneTimes.DataBind();


        }
    }

    protected void ddlSites_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLayer.BLLocations loc = new BusinessLayer.BLLocations();
        lbAccessPoints.DataSource = loc.GetLocationBySiteID(Convert.ToInt32(ddlSites.SelectedValue));
        lbAccessPoints.DataTextField = "Description";
        lbAccessPoints.DataValueField = "ID";
        lbAccessPoints.DataBind();

        getTimes();
        //foreach (string s in AccessPoint.Split(';'))
        //{
        //    ListItem listItem = cclAccessPoints.Items.FindByValue(s);
        //    if (listItem != null)
        //    {
        //        listItem.Selected = true;
        //    }
        //}
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
    public void populateAccessList()
    {

        BusinessLayer.BLZoneTimes zt = new BLZoneTimes();
        //zt.GetZoneTimes
        bool insert = true;
        if ((lbAccessPoints.SelectedValue != "") || (lbTimes.SelectedValue != "") || (ddlZones.SelectedValue != ""))
        {
            insert = false;    
        }
        string selected = lbAccessPoints.SelectedValue + "-" + lbTimes.SelectedValue + "-" + Convert.ToInt32(ddlZones.SelectedValue.ToString());
        
        Parallel.ForEach(AccessPoint.Split(';'),
            new Action<string, ParallelLoopState>((string s, ParallelLoopState state) =>
            {
                if (s == selected)
                {
                    insert = false;
                    state.Break();
                }
            }));

        if (insert == true)
        {
            AccessPoint = AccessPoint + selected + ";";
            
        }
        getTimes();
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if ((lbAccessPoints.SelectedItem != null) && (lbTimes.SelectedItem != null))
        {
            string selected = lbAccessPoints.SelectedValue + "-" + lbTimes.SelectedValue + "-" + Convert.ToInt32(ddlZones.SelectedValue.ToString());
            bool insert = true;
            Parallel.ForEach(AccessPoint.Split(';'),
                new Action<string, ParallelLoopState>((string s, ParallelLoopState state) =>
                {
                    if (s == selected)
                    {
                        insert = false;
                        state.Break();
                    }
                }));

            if (insert == true)
            {
                AccessPoint = AccessPoint + selected + ";";
                getTimes();
            }
        }
        else
        {
            lblMsg.Text = "Please select Access Point and Time";
        }
    }

    public void getTimes()
    {
        ListItemCollection l = new ListItemCollection();
       
        Parallel.ForEach(AccessPoint.Split(';'), s =>
            {
                if (s != "")
                {
                    string zone = s.Split('-')[2];
                    if (zone == ddlZones.SelectedValue.ToString())
                    {
                        string site = s.Split('-')[0];
                        if (lbAccessPoints.Items.FindByValue(site) != null)
                        {
                            ListItem list = new ListItem();
                            string time = s.Split('-')[1];
                            string siteDesc = lbAccessPoints.Items.FindByValue(site).Text;
                            string timeDesc = lbTimes.Items.FindByValue(time).Text;
                            list.Text = siteDesc + " - " + timeDesc;
                            list.Value = s;
                            l.Add(list);
                        }
                    }
                }
            });

        lbCurrent.DataSource = l;
        lbCurrent.DataTextField = "Text";
        lbCurrent.DataValueField = "Value";
        lbCurrent.DataBind();
    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lbCurrent.SelectedItem != null)
        {
            AccessPoint = AccessPoint.Replace(lbCurrent.SelectedValue+";", "");
            getTimes();
        }
        else
        {
            lblMsg.Text = "Please select an item.";
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                foreach (string s in AccessPoint.Split(';'))
                {
                    if (s != "")
                    {
                        tb_AccessZones accessZone = new tb_AccessZones();
                        accessZone.zone_id = Convert.ToInt32(s.Split('-')[2]);
                            //Convert.ToInt32(ddlZones.SelectedValue.ToString());
                        accessZone.location_id = Convert.ToInt32(s.Split('-')[0]);
                        accessZone.time_id = Convert.ToInt32(s.Split('-')[1]);
                        accessZone.CreatedBy = Common.getUsername();
                        accessZone.CreatedOn = DateTime.Now;
                        accessZone.IsActive = true;
                        accessZone.RecordVersion = 1;

                        BLZoneTimes zoneTime = new BLZoneTimes();
                        tb_AccessZones az = zoneTime.GetZoneTimeByID(accessZone.zone_id, accessZone.location_id, accessZone.time_id);
                        if (az == null)
                        {
                            switch (zoneTime.CreateZoneTime(accessZone))
                            {
                                case Util.OperationStatus.successful:
                                    lblMsg.Text = "Access Zone Created";
                                    break;
                                case Util.OperationStatus.Unsuccessful:
                                    lblMsg.Text = "Creation could not be carried out";
                                    break;
                                case Util.OperationStatus.Exists:
                                    lblMsg.Text = "Access Zone already exists";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred. Kindly contact the Administrator";
            }
        }
    }

    protected void ddlZones_SelectedIndexChanged(object sender, EventArgs e)
    {
        getTimes();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ArrayList toExport = new ArrayList();
        foreach (ListItem li in cclZoneTimes.Items)
        {
            if (li.Selected == true)
            {
                toExport.Add(li.Text);
            }
        }
        BLZoneTimes zt = new BLZoneTimes();
       
        Response.ContentType = "Application/x-msexcel";
        Response.AddHeader("content-disposition", "attachment;filename=Report.csv");
        Response.Write(ExportToCSVFile(ToDataTable(((IQueryable<DataLayer.Views.ZoneTimesDetailsView>)zt.GetZoneTimeDetail(toExport)).ToList())));
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
        ReorderTable(ref dtTable, "Zone", "Location", "Site", "Time", "ZoneID");

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

   
  

  
}

