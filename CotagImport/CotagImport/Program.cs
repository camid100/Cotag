using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer;
using System.Data;
using System.Globalization;
using System.IO;

namespace CotagImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportData id = new ImportData();
            string connectionString = Properties.Settings.Default.connectionString;
            DateTime dt = new DateTime();
            DateTime tm = new DateTime();
            //id.processDCData(1910, "not exists", "valid", DateTime.ParseExact(new DateTime().ToString(), "yyyyMMdd", CultureInfo.InvariantCulture), DateTime.ParseExact(new DateTime().ToString(), "Hmmss", CultureInfo.InvariantCulture), "Data Center", connectionString);
            if (Properties.Settings.Default.Site == 1)
            {
                #region DataCenter
                string filePath = Properties.Settings.Default.FilePath;
                string fileName = "20111129";
                //string fileName = DateTime.Now.ToString("yyyyMMdd");
                fileName = fileName + ".sqlarc";
                //*****First Run to disable****//
                foreach (string s in System.IO.Directory.GetFiles(filePath))
                {
                    fileName = s;
                  //  if (File.Exists(filePath + fileName) == true)
                  //  {
                        DataTable data = id.getDCData(filePath, fileName);
                        foreach (DataRow dr in data.Rows)
                        {
                            string loc = dr["pt_id"].ToString();
                            long cotag = Convert.ToInt32(dr["udf4"].ToString().Replace(".",""));
                            //string loc = dr["fln_no"].ToString() + ":" + dr["device_no"].ToString();
                            string status = dr["Message"].ToString();
                            int date = Convert.ToInt32(dr["date_occurred"].ToString());
                            string time = dr["time_occurred"].ToString();
                            string site = dr["udf3"].ToString();

                            if (site.Contains("ACC") == true)
                            {
                                site = "Data Center";
                            }
                            if(site.Contains("MITA GOZO"))
                            {
                                site = "Gozo";
                            }

                            dt = DateTime.ParseExact(date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            tm = DateTime.ParseExact(time, "Hmmss", CultureInfo.InvariantCulture);
                            id.processDCData(cotag, loc, status, dt, tm, site, connectionString);
                        }
                    //temp disabled
                        File.Delete(fileName);
                   // }
                  //  else
                  //  {
                        //Disabled for first run
                        //throw new Exception("File: " + fileName + " does not exist");
                  //  }
                }
                #endregion
            }
            else
            {
                #region HeadOffice

                SiteManagement sm = new SiteManagement(connectionString);
                int siteID = sm.getSite("Head Office");
                string HOfilePath = Properties.Settings.Default.HOFilePath;
                foreach (string st in System.IO.Directory.GetFiles(HOfilePath))
                {
                    //string HOfileName = DateTime.Now.ToString("ddMMyy");
                    // string HOfileName = "070813";
                    //  HOfileName = "LG" + HOfileName + ".dbf";
                    string HOfileName = st.Substring(HOfilePath.Length);
                    Console.WriteLine(st);
                    Console.WriteLine(HOfileName);
                    if (HOfileName.StartsWith("LG") == true)
                    {
                        string sts = "Valid Card";
                        //First run to disable
                        //  foreach (string s in System.IO.Directory.GetFiles(HOfilePath))
                        // {
                        if (File.Exists(HOfilePath + HOfileName) == true)
                        {
                            DataTable data = id.getHOData(HOfilePath, HOfileName);
                            foreach (DataRow dr in data.Rows)
                            {
                                string type = dr["TYPE"].ToString();
                                string loc = dr["SYSADDR"].ToString();
                                string time = dr["TIME"].ToString();
                                string date = dr["DDMMYY"].ToString();
                                string[] msg = dr["Message"].ToString().Split(',');
                                dt = DateTime.ParseExact(date, "ddMMyy", CultureInfo.InvariantCulture);
                                tm = DateTime.ParseExact(time, "Hmmss", CultureInfo.InvariantCulture);
                                if (type != "V")
                                    sts = "Soft APB Error";

                                id.processHOData(Convert.ToInt64(msg[0]), loc, sts, dt, tm, siteID, connectionString);
                            }
                            File.Delete(HOfilePath + "/" + HOfileName);
                        }
                        else
                        {
                            //Disabled for first run
                            //throw new Exception("File: " + HOfileName + " does not exist");
                        }
                    }
                }
                #endregion
            }
        }
    }
}
