using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.OleDb;
using System.IO;

namespace BusinessLayer
{
    public class ImportData:ConnectionClass
    {
        #region BaseClass
        public ImportData()
            : base()
        {
        }
        public ImportData(string str)
            : base(str)
        {
        }
        #endregion
        #region DataCenter/Gozo
        public DataTable getDCData(string path, string fileName)
        {
            SqlCeConnection conn = null;

            try
            {
                conn = new SqlCeConnection("Data Source = " + fileName + ";");
                //conn = new SqlCeConnection("Data Source = " + path + fileName + ";");
                conn.Open();

                SqlCeCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * from AuditTrail WHERE udf4 != ''";
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
        public void processDCData(long cotag, string location, string status, DateTime date, DateTime time, string site, string connectionString)
        {
            try
            {
                CotagTimeLogManagement ct = new CotagTimeLogManagement(connectionString);
                if (ct.checkTimeLog(date, time, cotag,location) == false)
                {
                    SiteManagement sm = new SiteManagement(connectionString);
                    int siteID = sm.getSite(site);
                    if (siteID != 0)
                    {
                        LocationsManagement lm = new LocationsManagement(connectionString);
                        int locationID = lm.getLocation(location, siteID);
                        ct.insertCotagTimeLog(cotag, locationID, status, date, time, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Head Office

        public DataTable getHOData(string path, string fileName)
        {
            System.Data.OleDb.OleDbConnection conn = null;
            try
            {
                string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBase IV";
                conn = new System.Data.OleDb.OleDbConnection(connString);
                conn.Open();
                var cmd = new OleDbCommand("SELECT * FROM [" + fileName + "]", conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable data = new DataTable();
                da.Fill(data);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        public void processHOData(long cotag,string location,string status,DateTime date,DateTime time,int siteID,string connectionString)
        {
            try
            {
                CotagTimeLogManagement ct = new CotagTimeLogManagement(connectionString);
                if (ct.checkTimeLog(date, time, cotag,location) == false)
                {
                    if (siteID != 0)
                    {
                        LocationsManagement lm = new LocationsManagement(connectionString);
                        int locationID = lm.getLocation(location, siteID);

                        ct.insertCotagTimeLog(cotag, locationID, status, date, time, System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
